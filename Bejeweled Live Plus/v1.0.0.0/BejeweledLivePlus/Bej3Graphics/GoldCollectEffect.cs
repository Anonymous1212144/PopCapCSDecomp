using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class GoldCollectEffect : IDisposable
	{
		public GoldCollectEffect(DigGoal theGoal, DigGoal.TileData theData)
		{
			this.mGoal = theGoal;
			this.mTileData = new DigGoal.TileData(theData);
			this.mTreasureType = ETreasureType.eTreasure_Gold;
			this.mLayerOnTop = false;
			this.mSparkles = null;
			this.mVal = 0;
			this.mImageId = -1;
			this.mSrcImageId = -1;
			this.mUpdateCnt = 0;
			this.mLastRotation = 0.0;
			this.mCentering = false;
			this.mTimeMod = 0f;
			this.mAddedPoints = false;
			this.mDeleteMe = false;
			this.mIsNugget = false;
			this.mDisplayVal = 0;
			this.mGlowImageId = -1;
			this.mExtraScaling = 1.0;
			this.mStopGlowAtPct = 1.0;
			this.mGlowRGB = 16777215;
			this.mGlowRGB2 = 16777215;
			this.mUseBaseSparkles = true;
			this.mExtraSplineTime = 0;
			this.mStartedAtTick = theGoal.mQuestBoard.mGameTicks;
			this.mParticleEmitOverTime.SetConstant(1.0);
			this.mPointBlinkCv.SetConstant(0.0);
			this.mScaleCv.SetConstant(1.0);
		}

		public void Dispose()
		{
			if (this.mSparkles != null)
			{
				this.mSparkles.Dispose();
			}
		}

		public void Init()
		{
			this.mLastPoint = this.mStartPoint;
			this.mX = (double)this.mStartPoint.mX;
			this.mY = (double)this.mStartPoint.mY;
			if (this.mCentering)
			{
				this.mExtraSplineTime += (int)(this.mSplineInterp.mInMax * (double)this.mTimeMod * 100.0);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_SPLINE_INTERP_ARTIFACT, this.mSplineInterp);
				this.mSplineInterp.SetInRange(0.0, this.mSplineInterp.mInMax + (double)((float)this.mExtraSplineTime / 100f));
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_SCALE_CV_ARTIFACT, this.mScaleCv);
				this.mScaleCv.SetInRange(0.0, this.mScaleCv.mInMax);
				if (this.mDisplayVal > 0)
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_POINT_BLINK_CV, this.mPointBlinkCv);
					this.mPointBlinkCv.SetInRange(0.0, this.mPointBlinkCv.mInMax);
				}
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_STREAMER_MAG, this.mStreamerMag);
				this.mStreamerMag.SetInRange(0.0, this.mStreamerMag.mInMax);
				Image imageById = GlobalMembersResourcesWP.GetImageById(this.mSrcImageId);
				Image imageById2 = GlobalMembersResourcesWP.GetImageById(this.mImageId);
				this.mScaleCv.mOutMin = (double)imageById.GetWidth() / (double)imageById2.GetWidth();
			}
			else
			{
				this.mExtraSplineTime += (int)(this.mSplineInterp.mInMax * (double)this.mTimeMod * 100.0);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_SPLINE_INTERP, this.mSplineInterp);
				this.mSplineInterp.SetInRange(0.0, this.mSplineInterp.mInMax + (double)((float)this.mExtraSplineTime / 100f));
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_SCALE_CV, this.mScaleCv);
				this.mScaleCv.SetInRange(0.0, this.mScaleCv.mInMax + (double)((float)this.mExtraSplineTime / 100f));
				this.mSpline.AddPoint((float)this.mX, (float)this.mY);
				if (this.mIsNugget)
				{
					this.mLayerOnTopSwitchPct = GlobalMembers.M(1f);
				}
				else if (BejeweledLivePlus.Misc.Common.Rand() % 2 == 0)
				{
					this.mLayerOnTopSwitchPct = GlobalMembers.M(0.78f);
				}
				else
				{
					this.mLayerOnTopSwitchPct = GlobalMembers.M(0.78f);
				}
				this.mSpline.AddPoint((float)this.mTargetPoint.mX, (float)this.mTargetPoint.mY);
				this.mSpline.CalculateSpline();
			}
			this.mSparkles = (this.mUseBaseSparkles ? GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_COLLECT_BASE.Duplicate() : GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_COLLECT_GOLD.Duplicate());
			this.mSparkles.mDrawTransform.LoadIdentity();
			this.mSparkles.mDrawTransform.Scale(GlobalMembers.S(1f), GlobalMembers.S(1f));
			this.mSparkles.mEmitAfterTimeline = true;
		}

		public double CalcRotation()
		{
			if (this.mCentering)
			{
				return 0.0;
			}
			if (!this.mSplineInterp.HasBeenTriggered())
			{
				double num = Math.Atan2((double)this.mLastPoint.mY - this.mY, this.mX - (double)this.mLastPoint.mX);
				double num2 = num - this.mLastRotation;
				num2 = ((num2 < 0.0) ? (-1.0) : 1.0) * Math.Min(GlobalMembers.M(0.03), Math.Abs(num2));
				this.mLastRotation += num2;
				this.mLastPoint.mX = (int)this.mX;
				this.mLastPoint.mY = (int)this.mY;
			}
			return this.mLastRotation;
		}

		public void Update()
		{
			this.mUpdateCnt++;
			if (!this.mCentering || (this.mUpdateCnt > this.mExtraSplineTime && this.mSplineInterp.mInMax - this.mSplineInterp.mInVal <= GlobalMembers.M(0.15)))
			{
				this.mPointBlinkCv.IncInVal();
				this.mStreamerMag.IncInVal();
				this.mAlphaOut.IncInVal();
			}
			this.mScaleCv.IncInVal();
			if (this.mCentering)
			{
				this.mX = (double)this.mStartPoint.mX + this.mSplineInterp * (double)(GlobalMembers.RS(ConstantsWP.DIG_BOARD_ITEM_DEST_X) - this.mStartPoint.mX);
				this.mY = (double)this.mStartPoint.mY + this.mSplineInterp * (double)(GlobalMembers.RS(ConstantsWP.DIG_BOARD_ITEM_DEST_Y) - this.mStartPoint.mY);
				double num = this.mSplineInterp;
				this.mSplineInterp.IncInVal();
				if (num < GlobalMembers.M(0.95) && this.mSplineInterp >= GlobalMembers.M(0.95))
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_ARTIFACT_SHOWCASE, 0, GlobalMembers.M(1.0));
				}
				if (this.mStreamerMag.mInVal / this.mStreamerMag.mInMax > GlobalMembers.M(0.9))
				{
					this.mExtraSplineTime = 0;
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_SPLINE_INTERP, this.mSplineInterp);
					this.mCentering = false;
					this.mSpline.AddPoint((float)this.mX, (float)this.mY);
					this.mLayerOnTopSwitchPct = GlobalMembers.M(0.75f);
					this.mSpline.AddPoint((float)this.mTargetPoint.mX, (float)this.mTargetPoint.mY);
					this.mSpline.CalculateSpline();
					double mOutMin = this.mScaleCv.mOutMin;
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_SCALE_CV_UPDATE, this.mScaleCv);
					this.mScaleCv.mOutMin = mOutMin * this.mExtraScaling;
				}
			}
			else
			{
				this.mX = (double)this.mSpline.GetXPoint((float)(this.mSplineInterp * (double)this.mSpline.GetMaxT()));
				this.mY = (double)this.mSpline.GetYPoint((float)(this.mSplineInterp * (double)this.mSpline.GetMaxT()));
			}
			this.mLayerOnTop = this.mCentering || this.mSplineInterp < (double)this.mLayerOnTopSwitchPct;
			double num2 = GlobalMembers.M(0.75);
			if ((double)this.mLayerOnTopSwitchPct < 1.0)
			{
				num2 = (double)this.mLayerOnTopSwitchPct;
			}
			if (this.mSparkles != null)
			{
				if (this.mSplineInterp > num2 && !this.mCentering)
				{
					double num3;
					if (num2 >= this.mStopGlowAtPct)
					{
						num3 = 0.0;
					}
					else
					{
						num3 = Math.Max(0.0, 1.0 - (this.mSplineInterp - num2) / (this.mStopGlowAtPct - num2));
					}
					this.mSparkles.GetLayer(GlobalMembers.M(0)).mColor = new Color(this.mGlowRGB, (int)(255.0 * num3));
					this.mSparkles.GetLayer(GlobalMembers.M(1)).GetEmitter(0).mNumberScale = (float)num3;
				}
				else
				{
					this.mSparkles.GetLayer(GlobalMembers.M(0)).mColor = new Color(this.mGlowRGB);
					this.mSparkles.GetLayer(GlobalMembers.M(1)).GetEmitter(0).mNumberScale = 1f;
				}
				if (!this.mUseBaseSparkles)
				{
					this.mSparkles.GetLayer(GlobalMembers.M(1)).mColor = new Color(this.mGlowRGB2);
				}
				if (!this.mCentering)
				{
					this.mSparkles.GetLayer(GlobalMembers.M(1)).GetEmitter(0).mNumberScale *= (float)this.mParticleEmitOverTime.GetOutVal(this.mSplineInterp);
				}
				if (!GlobalMembers.gApp.Is3DAccelerated())
				{
					this.mSparkles.GetLayer(GlobalMembers.M(1)).GetEmitter(0).mNumberScale *= GlobalMembers.M(0.5f);
				}
				this.mSparkles.mEmitterTransform.LoadIdentity();
				this.mSparkles.mEmitterTransform.Scale((float)GlobalMembers.S(this.mScaleCv), (float)GlobalMembers.S(this.mScaleCv));
				this.mSparkles.mEmitterTransform.Translate((float)(this.mX + (double)GlobalMembers.M(-30)), (float)(this.mY + (double)GlobalMembers.M(-20)));
				this.mSparkles.Update();
			}
			if (this.mCentering)
			{
				return;
			}
			if (!this.mSplineInterp.IncInVal())
			{
				if (this.mSparkles != null)
				{
					this.mSparkles.mEmitAfterTimeline = false;
					this.mSparkles.mFrameNum = (float)(this.mSparkles.mLastFrameNum - 1);
				}
				this.mAlphaOut.mAppUpdateCountSrc = this.mGoal.mUpdateCnt;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eDIG_GOAL_ALPHA_OUT, this.mAlphaOut);
			}
			if (!this.mAddedPoints && (!this.mSplineInterp.IsDoingCurve() || this.mSplineInterp.GetInVal() / this.mSplineInterp.mInMax > GlobalMembers.M(0.85)))
			{
				this.mGoal.GoldAnimDoPoints(this.mTreasureType, this.mVal);
				this.mAddedPoints = true;
			}
			if (this.mAlphaOut.IsDoingCurve())
			{
				if (!this.mAlphaOut.IncInVal())
				{
					return;
				}
			}
			else if (this.mAlphaOut.HasBeenTriggered())
			{
				if (this.mSparkles != null)
				{
					if (!this.mSparkles.IsActive() || this.mSparkles.mCurNumParticles == GlobalMembers.M(1))
					{
						this.mDeleteMe = true;
						return;
					}
					if (this.mUpdateCnt > GlobalMembers.M(1000))
					{
						this.mDeleteMe = true;
						return;
					}
				}
				else
				{
					this.mDeleteMe = true;
				}
			}
		}

		public void Draw(Graphics g, int pass)
		{
			g.PushState();
			if (pass == 0)
			{
				if (this.mSparkles != null)
				{
					this.mSparkles.Draw(g);
				}
				return;
			}
			Image imageById = GlobalMembersResourcesWP.GetImageById(this.mImageId);
			if (this.mGlowImageId != -1 && this.mStreamerMag > 0.0)
			{
				this.DrawStreamer(g);
				Image imageById2 = GlobalMembersResourcesWP.GetImageById(this.mGlowImageId);
				g.PushState();
				g.SetColor(new Color(GlobalMembers.M(16777215), (int)((double)GlobalMembers.M(255) * (this.mStreamerMag / this.mStreamerMag.mOutMax))));
				g.SetColorizeImages(true);
				int num = 0;
				int num2 = 0;
				Utils.MyDrawImageRotated(g, imageById2, (float)GlobalMembers.S(this.mX + (double)num), (float)GlobalMembers.S(this.mY + (double)num2), this.CalcRotation(), (float)this.mScaleCv, (float)this.mScaleCv);
				g.PopState();
			}
			bool linearBlend = g.GetLinearBlend();
			if (imageById == GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_DMGEM_BIG)
			{
				g.SetLinearBlend(false);
			}
			Utils.MyDrawImageRotated(g, imageById, (float)GlobalMembers.S(this.mX), (float)GlobalMembers.S(this.mY), this.CalcRotation(), (float)this.mScaleCv, (float)this.mScaleCv);
			g.SetLinearBlend(linearBlend);
			if (!this.mLayerOnTop)
			{
				g.ClearClipRect();
			}
			if (this.mPointBlinkCv > 0.0)
			{
				if (this.mPointBlinkCv < 1.0)
				{
					g.SetColorizeImages(true);
					g.SetColor(new Color(GlobalMembers.M(16777215), (int)((double)GlobalMembers.M(255) * this.mPointBlinkCv)));
				}
				g.SetColor(new Color(-1));
				g.SetFont(GlobalMembersResources.FONT_HUGE);
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_HUGE, 0, Bej3Widget.COLOR_DIGBOARD_SCORE_GLOW);
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_HUGE, 1, Bej3Widget.COLOR_DIGBOARD_SCORE_STROKE);
				int num3 = (int)(this.mY + (double)ConstantsWP.DIG_BOARD_ITEM_DESC_OFFSET_Y);
				float mScaleX = g.mScaleX;
				float mScaleY = g.mScaleY;
				string theString = string.Format("+{0}", SexyFramework.Common.CommaSeperate(this.mDisplayVal));
				GlobalMembersResources.FONT_HUGE.StringWidth(theString);
				int num4 = (int)GlobalMembers.S(this.mX);
				int num5 = GlobalMembers.S(num3 + GlobalMembers.M(5)) + imageById.GetHeight() / 2;
				g.SetScale(ConstantsWP.DIG_BOARD_FLOATING_SCORE_SCALE, ConstantsWP.DIG_BOARD_FLOATING_SCORE_SCALE, (float)num4, (float)num5);
				g.WriteString(theString, num4, num5);
				g.mScaleX = mScaleX;
				g.mScaleY = mScaleY;
				g.SetColorizeImages(false);
			}
			g.PopState();
		}

		public void DrawStreamer(Graphics g)
		{
			g.PushState();
			g.Translate((int)GlobalMembers.S(this.mX), (int)GlobalMembers.S(this.mY));
			g.SetColorizeImages(true);
			double num = this.mStreamerMag * GlobalMembers.M(1.0);
			double num2 = this.mStreamerMag * GlobalMembers.M(1.0);
			double num3 = 6.2831854820251465 * ((double)(this.mUpdateCnt % GlobalMembers.M(501)) / GlobalMembers.M(500.0));
			double num4 = 6.2831854820251465 / GlobalMembers.M(20.0);
			for (int i = 0; i <= GlobalMembers.M(1); i++)
			{
				double num5 = 0.0;
				Image image_QUEST_DIG_STREAK = GlobalMembersResourcesWP.IMAGE_QUEST_DIG_STREAK;
				if (i == 0)
				{
					g.SetColor(new Color(GlobalMembers.M(16763904), GlobalMembers.M(255)));
					num5 = GlobalMembers.M(0.215);
				}
				else
				{
					g.SetColor(new Color(GlobalMembers.M(16776960), GlobalMembers.M(255)));
				}
				int num6 = 0;
				for (double num7 = num3; num7 < num3 + 6.2831854820251465; num7 += num4)
				{
					num6++;
					if (num6 % 2 == 0)
					{
						double num8 = ((i == 0) ? num : num2);
						float theNum = (float)(num8 * Math.Cos(num7 - num5));
						float theNum2 = (float)(num8 * Math.Sin(num7 - num5));
						float theNum3 = (float)(num8 * Math.Cos(num7 + num4 / 2.0));
						float theNum4 = (float)(num8 * Math.Sin(num7 + num4 / 2.0));
						float theNum5 = (float)(num8 * Math.Cos(num7 + num4 + num5));
						float theNum6 = (float)(num8 * Math.Sin(num7 + num4 + num5));
						float theNum7 = 0f;
						float theNum8 = 0f;
						GoldCollectEffect.DrawStreamer_tris[0, 0].x = GlobalMembers.S(theNum7);
						GoldCollectEffect.DrawStreamer_tris[0, 0].y = GlobalMembers.S(theNum8);
						GoldCollectEffect.DrawStreamer_tris[0, 0].u = GlobalMembers.M(0.5f);
						GoldCollectEffect.DrawStreamer_tris[0, 0].v = GlobalMembers.M(0f);
						GoldCollectEffect.DrawStreamer_tris[0, 1].x = GlobalMembers.S(theNum3);
						GoldCollectEffect.DrawStreamer_tris[0, 1].y = GlobalMembers.S(theNum4);
						GoldCollectEffect.DrawStreamer_tris[0, 1].u = GlobalMembers.M(0.5f);
						GoldCollectEffect.DrawStreamer_tris[0, 1].v = GlobalMembers.M(1f);
						GoldCollectEffect.DrawStreamer_tris[0, 2].x = GlobalMembers.S(theNum);
						GoldCollectEffect.DrawStreamer_tris[0, 2].y = GlobalMembers.S(theNum2);
						GoldCollectEffect.DrawStreamer_tris[0, 2].u = GlobalMembers.M(0f);
						GoldCollectEffect.DrawStreamer_tris[0, 2].v = GlobalMembers.M(1f);
						GoldCollectEffect.DrawStreamer_tris[1, 0].x = GlobalMembers.S(theNum7);
						GoldCollectEffect.DrawStreamer_tris[1, 0].y = GlobalMembers.S(theNum8);
						GoldCollectEffect.DrawStreamer_tris[1, 0].u = GlobalMembers.M(0.5f);
						GoldCollectEffect.DrawStreamer_tris[1, 0].v = GlobalMembers.M(0f);
						GoldCollectEffect.DrawStreamer_tris[1, 1].x = GlobalMembers.S(theNum3);
						GoldCollectEffect.DrawStreamer_tris[1, 1].y = GlobalMembers.S(theNum4);
						GoldCollectEffect.DrawStreamer_tris[1, 1].u = GlobalMembers.M(0.5f);
						GoldCollectEffect.DrawStreamer_tris[1, 1].v = GlobalMembers.M(1f);
						GoldCollectEffect.DrawStreamer_tris[1, 2].x = GlobalMembers.S(theNum5);
						GoldCollectEffect.DrawStreamer_tris[1, 2].y = GlobalMembers.S(theNum6);
						GoldCollectEffect.DrawStreamer_tris[1, 2].u = GlobalMembers.M(1f);
						GoldCollectEffect.DrawStreamer_tris[1, 2].v = GlobalMembers.M(1f);
						if (GlobalMembers.M(0) != 0)
						{
							g.PushState();
							g.SetColor(new Color(-1));
							for (int j = 0; j < 2; j++)
							{
								for (int k = 0; k <= 3; k++)
								{
									g.DrawLine((int)GoldCollectEffect.DrawStreamer_tris[j, k].x, (int)GoldCollectEffect.DrawStreamer_tris[j, k].y, (int)GoldCollectEffect.DrawStreamer_tris[j, (k + 1) % 3].x, (int)GoldCollectEffect.DrawStreamer_tris[j, (k + 1) % 3].y);
								}
							}
							g.PopState();
						}
						g.DrawTrianglesTex(image_QUEST_DIG_STREAK, GoldCollectEffect.DrawStreamer_tris, 2);
					}
				}
			}
			g.PopState();
		}

		public bool mLayerOnTop;

		public float mLayerOnTopSwitchPct;

		public BSpline mSpline = new BSpline();

		public CurvedVal mSplineInterp = new CurvedVal();

		public CurvedVal mAlphaOut = new CurvedVal();

		public CurvedVal mScaleCv = new CurvedVal();

		public CurvedVal mPointBlinkCv = new CurvedVal();

		public CurvedVal mStreamerMag = new CurvedVal();

		public CurvedVal mParticleEmitOverTime = new CurvedVal();

		public PIEffect mSparkles;

		public int mGlowRGB;

		public int mGlowRGB2;

		public int mUpdateCnt;

		public DigGoal mGoal;

		public bool mDeleteMe;

		public double mX;

		public double mY;

		public int mImageId;

		public int mSrcImageId;

		public int mGlowImageId;

		public DigGoal.TileData mTileData = new DigGoal.TileData();

		public bool mCentering;

		public Point mStartPoint = default(Point);

		public Point mTargetPoint = default(Point);

		public Point mLastPoint = default(Point);

		public double mLastRotation;

		public double mStopGlowAtPct;

		public float mTimeMod;

		public bool mAddedPoints;

		public bool mIsNugget;

		public int mExtraSplineTime;

		public int mStartedAtTick;

		public int mVal;

		public ETreasureType mTreasureType;

		public int mDisplayVal;

		public string mDisplayName = string.Empty;

		public double mExtraScaling;

		public bool mUseBaseSparkles;

		private static SexyVertex2D[,] DrawStreamer_tris = new SexyVertex2D[2, 3];
	}
}
