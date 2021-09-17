using System;
using System.Collections.Generic;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class SpeedCollectEffect : Effect
	{
		public SpeedCollectEffect()
			: base(Effect.Type.TYPE_CUSTOMCLASS)
		{
		}

		public void init(SpeedBoard theSpeedBoard, Point theSrc, Point theTgt, Image theImage, int theTimeCollected, float theTimeMod)
		{
			base.init(Effect.Type.TYPE_CUSTOMCLASS);
			this.mTimeCollected = theTimeCollected;
			this.mBoard = theSpeedBoard;
			this.mImage = theImage;
			this.mX = (float)theSrc.mX;
			this.mY = (float)theSrc.mY;
			this.mLastPoint = theSrc;
			this.mDAlpha = 0f;
			this.mCurvedAlpha.SetConstant(1.0);
			this.mUpdateCnt = 0;
			this.mStartPoint = theSrc;
			this.mTargetPoint = theTgt;
			this.mLastRotation = 0.0;
			this.mCentering = false;
			this.mTimeMod = theTimeMod;
			this.mSparkles = null;
			this.mTimeBonusEffect = null;
		}

		public override void Dispose()
		{
			if (this.mSparkles != null)
			{
				this.mSparkles.mDeleteMe = true;
				this.mSparkles.mRefCount--;
			}
			if (this.mTimeBonusEffect != null)
			{
				this.mTimeBonusEffect.mDeleteMe = true;
				this.mTimeBonusEffect.mRefCount--;
			}
			base.Dispose();
		}

		public void Init(Piece thePiece)
		{
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_SPLINE_INTERP_A, this.mSplineInterp);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_ALPHA_OUT, this.mAlphaOut);
			this.mSplineInterp.SetInRange(0.0, this.mSplineInterp.mInMax * (double)this.mTimeMod);
			this.mAlphaOut.mIncRate = this.mSplineInterp.mIncRate;
			this.mAlphaOut.mInMax = this.mSplineInterp.mInMax + (double)GlobalMembers.M(0f);
			this.mScaleCv.SetConstant(1.0);
			this.mSpline = new BSpline();
			this.mSpline.AddPoint(this.mX, this.mY);
			this.mSpline.AddPoint((float)ConstantsWP.SPEEDBOARD_COLLECT_EFFECT_INTERMEDIATE_X, (float)ConstantsWP.SPEEDBOARD_COLLECT_EFFECT_INTERMEDIATE_Y);
			this.mSpline.AddPoint((float)this.mTargetPoint.mX, (float)this.mTargetPoint.mY);
			this.mSpline.CalculateSpline();
			this.mSparkles = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_COLLECT_GOLD);
			this.mSparkles.SetEmitAfterTimeline(true);
			this.mSparkles.mDoDrawTransform = false;
			this.mSparkles.mRefCount++;
			this.mFXManager.AddEffect(this.mSparkles);
			List<Effect> list = this.mFXManager.mBoard.mPostFXManager.mEffectList[21];
			foreach (Effect effect in list)
			{
				if (effect.mType == Effect.Type.TYPE_TIME_BONUS && effect.mPieceRel == thePiece)
				{
					this.mTimeBonusEffect = (TimeBonusEffect)effect;
					this.mTimeBonusEffect.mLightIntensity = GlobalMembers.M(6f);
					this.mTimeBonusEffect.mLightSize = GlobalMembers.M(300f);
					this.mTimeBonusEffect.mValue[0] = GlobalMembers.M(50f);
					this.mTimeBonusEffect.mValue[1] = GlobalMembers.M(-0.0005f);
					this.mTimeBonusEffect.mRefCount++;
					this.mTimeBonusEffect.mPieceRel = null;
					this.mTimeBonusEffect.mOverlay = true;
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CIRCLE_PCT_SPEED_BOARD_TIME_BONUS, this.mTimeBonusEffect.mCirclePct);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_RADIUS_SCALE_SPEED_BOARD_TIME_BONUS, this.mTimeBonusEffect.mRadiusScale);
					break;
				}
			}
		}

		public double CalcRotation()
		{
			if (this.mCentering)
			{
				return 0.0;
			}
			if (!this.mSplineInterp.HasBeenTriggered())
			{
				double num = Math.Atan2((double)((float)this.mLastPoint.mY - this.mY), (double)(this.mX - (float)this.mLastPoint.mX));
				double num2 = num - this.mLastRotation;
				num2 = ((num2 < 0.0) ? (-1.0) : 1.0) * Math.Min(GlobalMembers.M(0.03), Math.Abs(num2));
				this.mLastRotation += num2;
				this.mLastPoint.mX = (int)this.mX;
				this.mLastPoint.mY = (int)this.mY;
			}
			return this.mLastRotation;
		}

		public override void Update()
		{
			base.Update();
			this.mUpdateCnt++;
			if (this.mCentering)
			{
				this.mX = (float)((double)this.mStartPoint.mX + this.mSplineInterp * (double)(this.mFXManager.mBoard.GetBoardCenterX() - this.mStartPoint.mX));
				this.mY = (float)((double)this.mStartPoint.mY + this.mSplineInterp * (double)(this.mFXManager.mBoard.GetBoardCenterY() - this.mStartPoint.mY));
				if (!this.mSplineInterp.IncInVal())
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_QUEST_GET);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_SPLINE_INTERP_B, this.mSplineInterp);
					this.mSplineInterp.SetInRange(0.0, this.mSplineInterp.mInMax * (double)this.mTimeMod);
					this.mCentering = false;
					this.mSpline.AddPoint(this.mX, this.mY);
					this.mSpline.AddPoint(GlobalMembers.M(800f), GlobalMembers.M(150f));
					this.mSpline.AddPoint(GlobalMembers.M(600f), GlobalMembers.M(175f));
					this.mSpline.AddPoint(GlobalMembers.M(400f), GlobalMembers.M(150f));
					this.mSpline.AddPoint(GlobalMembers.M(200f), GlobalMembers.M(300f));
					this.mSpline.AddPoint((float)this.mTargetPoint.mX, (float)this.mTargetPoint.mY);
					this.mSpline.CalculateSpline();
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_SCALE_CV, this.mScaleCv);
					this.mScaleCv.SetInRange(0.0, this.mScaleCv.mInMax * (double)this.mTimeMod);
				}
			}
			else
			{
				this.mX = this.mSpline.GetXPoint((float)(this.mSplineInterp * (double)this.mSpline.GetMaxT()));
				this.mY = this.mSpline.GetYPoint((float)(this.mSplineInterp * (double)this.mSpline.GetMaxT()));
			}
			if (this.mSparkles != null)
			{
				this.mSparkles.mX = this.mX + GlobalMembers.M(-30f);
				this.mSparkles.mY = this.mY + GlobalMembers.M(-20f);
			}
			if (this.mTimeBonusEffect != null)
			{
				this.mTimeBonusEffect.mX = this.mX;
				this.mTimeBonusEffect.mY = this.mY;
			}
			this.mScaleCv.IncInVal();
			if (this.mCentering)
			{
				return;
			}
			this.mSplineInterp.IncInVal();
			if (this.mAlphaOut.IsDoingCurve())
			{
				if (this.mAlphaOut.CheckUpdatesFromEndThreshold(GlobalMembers.M(10)))
				{
					this.mBoard.TimeCollected(this.mTimeCollected);
				}
				if (!this.mAlphaOut.IncInVal())
				{
					this.mDeleteMe = true;
				}
				if (this.mSparkles != null)
				{
					this.mSparkles.mAlpha = (float)this.mAlphaOut;
				}
				if (this.mTimeBonusEffect != null)
				{
					this.mTimeBonusEffect.mAlpha = (float)this.mAlphaOut;
				}
			}
		}

		public override void Draw(Graphics g)
		{
		}

		public new static void initPool()
		{
			SpeedCollectEffect.thePool_ = new SimpleObjectPool(512, typeof(SpeedCollectEffect));
		}

		public static SpeedCollectEffect alloc(SpeedBoard theSpeedBoard, Point theSrc, Point theTgt, Image theImage, int theTimeCollected)
		{
			return SpeedCollectEffect.alloc(theSpeedBoard, theSrc, theTgt, theImage, theTimeCollected, 1f);
		}

		public static SpeedCollectEffect alloc(SpeedBoard theSpeedBoard, Point theSrc, Point theTgt, Image theImage, int theTimeCollected, float theTimeMod)
		{
			SpeedCollectEffect speedCollectEffect = (SpeedCollectEffect)SpeedCollectEffect.thePool_.alloc();
			speedCollectEffect.init(theSpeedBoard, theSrc, theTgt, theImage, theTimeCollected, theTimeMod);
			return speedCollectEffect;
		}

		public override void release()
		{
			this.Dispose();
			SpeedCollectEffect.thePool_.release(this);
		}

		public BSpline mSpline = new BSpline();

		public CurvedVal mSplineInterp = new CurvedVal();

		public CurvedVal mAlphaOut = new CurvedVal();

		public CurvedVal mScaleCv = new CurvedVal();

		public ParticleEffect mSparkles;

		public TimeBonusEffect mTimeBonusEffect;

		public int mUpdateCnt;

		public float mAccel;

		public SpeedBoard mBoard;

		public new Image mImage;

		public bool mCentering;

		public Point mStartPoint = default(Point);

		public Point mTargetPoint = default(Point);

		public Point mLastPoint = default(Point);

		public double mLastRotation;

		public float mTimeMod;

		public int mTimeCollected;

		private static SimpleObjectPool thePool_;
	}
}
