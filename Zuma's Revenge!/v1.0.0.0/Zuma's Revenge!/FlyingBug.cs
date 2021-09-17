using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class FlyingBug : Effect
	{
		protected override void Init()
		{
			this.mBoard = GameApp.gApp.GetBoard();
			if (this.mBoard == null)
			{
				return;
			}
			this.mBugs.Clear();
			int num = this.mMaxBugs - this.mMinBugs;
			if (num <= 0)
			{
				num = 1;
			}
			int num2 = this.mMinBugs + Common.Rand() % num;
			for (int i = 0; i < num2; i++)
			{
				Critter critter = new Critter();
				this.mBugs.Add(critter);
				this.SetupBug(critter);
			}
		}

		protected virtual void SetupBug(Critter d)
		{
			d.mCel = 0;
			int num = Common.Rand() % 4;
			if (num == 0)
			{
				d.mX = (float)(-(float)d.mImage.GetCelWidth());
				d.mY = (float)(Common.Rand() % Common._SS(GameApp.gApp.mHeight));
				d.mAngle = Common.GetCanonicalAngleRad(Common.DegreesToRadians(-45f) + Common.DegreesToRadians((float)(Common.Rand() % 90)));
			}
			else if (num == 1)
			{
				d.mX = (float)Common._SS(GameApp.gApp.mWidth);
				d.mY = (float)(Common.Rand() % Common._SS(GameApp.gApp.mHeight));
				d.mAngle = Common.DegreesToRadians(135f) + Common.DegreesToRadians((float)(Common.Rand() % 90));
			}
			else if (num == 2)
			{
				d.mX = (float)(Common.Rand() % Common._SS(GameApp.gApp.mWidth));
				d.mY = (float)(-(float)d.mImage.GetCelHeight());
				d.mAngle = Common.DegreesToRadians(225f) + Common.DegreesToRadians((float)(Common.Rand() % 90));
			}
			else
			{
				d.mX = (float)(Common.Rand() % Common._SS(GameApp.gApp.mWidth));
				d.mY = (float)Common._SS(GameApp.gApp.mHeight);
				d.mAngle = Common.DegreesToRadians(45f) + Common.DegreesToRadians((float)(Common.Rand() % 90));
			}
			d.mUpdateCount = 0;
			d.mAX = (d.mAY = 0f);
			d.mAngleInc = 0f;
			d.mTargetAngle = d.mAngle;
			d.mState = 0;
			d.mAlpha = 255f;
			d.mFadeOut = false;
			d.mSize = 1f;
			d.mRotateDelay = 0;
			d.mAnimRate = this.mDefaultAnimRate;
			int num2 = this.mFlyingMaxTimer - this.mFlyingMinTimer;
			if (num2 <= 0)
			{
				num2 = 1;
			}
			d.mTimer = this.mFlyingMinTimer + Common.Rand() % num2;
		}

		protected virtual void UpdateStateFlying(Critter d)
		{
			d.mX += d.mVX;
			d.mY += d.mVY;
			d.mTimer--;
			Rect theTRect = new Rect((int)d.mX, (int)d.mY, d.mImage.GetCelWidth(), d.mImage.GetCelHeight());
			Rect rect = new Rect(theTRect.mWidth, theTRect.mHeight, GameApp.gApp.mWidth - theTRect.mWidth * 2, GameApp.gApp.mHeight - theTRect.mHeight * 2);
			if ((d.mTimer <= 0 && (!this.mNoRotateUntilOnScreen || rect.Intersects(theTRect))) || this.mNoEnterRect.Intersects(theTRect))
			{
				if (d.mTimer > 0)
				{
					d.mTimer = this.mReverseTimer;
					d.mTargetAngle = d.mAngle - 3.14159f;
					d.mRotateDelay = this.mReverseRotateDelay;
				}
				else
				{
					int num = this.mRotateMaxTimer - this.mRotateMinTimer;
					if (num <= 0)
					{
						num = 1;
					}
					d.mTimer = this.mRotateMinTimer + Common.Rand() % num;
					d.mTargetAngle = Common.DegreesToRadians((float)(Common.Rand() % this.mMaxRotateDegrees)) * (float)((Common.Rand() % 2 != 0) ? (-1) : 1);
				}
				d.mAngleInc = (d.mTargetAngle - d.mAngle) / (float)d.mTimer;
				d.mState = 1;
			}
		}

		protected virtual void UpdateStateRotating(Critter d)
		{
			d.mTimer--;
			if (d.mRotateDelay == 0 || d.mTimer > 0)
			{
				d.mAngle += d.mAngleInc;
				d.mVX = (float)Math.Cos((double)d.mAngle) * d.mInitVel;
				d.mVY = -(float)Math.Sin((double)d.mAngle) * d.mInitVel;
			}
			d.mX += d.mVX;
			d.mY += d.mVY;
			if (d.mTimer == 0)
			{
				if (d.mRotateDelay > 0)
				{
					d.mRotateDelay--;
					return;
				}
				if (this.mAllowRest && Common.Rand() % 2 != 0)
				{
					d.mState = 2;
					d.mTimer = this.mTimeToSlowDown;
					d.mAX = -d.mVX / (float)d.mTimer;
					d.mAY = -d.mVY / (float)d.mTimer;
					return;
				}
				int num = this.mFlyingMaxTimer - this.mFlyingMinTimer;
				if (num <= 0)
				{
					num = 1;
				}
				d.mTimer = this.mFlyingMinTimer + Common.Rand() % num;
				d.mState = 0;
			}
		}

		protected virtual void UpdateStateSlowingDown(Critter d)
		{
			d.mVX += d.mAX;
			d.mVY += d.mAY;
			d.mX += d.mVX;
			d.mY += d.mVY;
			float num = (float)d.mTimer / (float)this.mTimeToSlowDown;
			float num2 = 1f - num;
			d.mAnimRate = (int)((float)this.mDefaultAnimRate + (float)(this.mDefaultAnimRate * 2) * num2);
			d.mSize = 0.75f + 0.25f * num;
			if (--d.mTimer == 0)
			{
				d.mState = 3;
				int num3 = this.mRestingMaxTimer - this.mRestingMinTimer;
				if (num3 <= 0)
				{
					num3 = 1;
				}
				d.mTimer = this.mRestingMinTimer + Common.Rand() % num3;
				d.mVX = (d.mVY = 0f);
				d.mCel = (d.mCel + 1) % d.mImage.mNumCols;
			}
		}

		protected virtual void UpdateStateResting(Critter d)
		{
			if (--d.mTimer == 0)
			{
				float num = (float)this.mAccelerationTime;
				d.mState = 4;
				d.mAX = (float)Math.Cos((double)d.mAngle) * d.mInitVel / num;
				d.mAY = -(float)Math.Sin((double)d.mAngle) * d.mInitVel / num;
				d.mTimer = (int)num;
			}
		}

		protected virtual void UpdateStateAccelerating(Critter d)
		{
			d.mVX += d.mAX;
			d.mVY += d.mAY;
			d.mX += d.mVX;
			d.mY += d.mVY;
			float num = (float)d.mTimer / Common._M(100f);
			float num2 = 1f - num;
			d.mSize = 1f - 0.25f * num;
			if (d.mSize > 1f)
			{
				d.mSize = 1f;
			}
			d.mAnimRate = (int)((float)(this.mDefaultAnimRate * 3) - (float)(this.mDefaultAnimRate * 2) * num2);
			if (d.mAnimRate < this.mDefaultAnimRate)
			{
				d.mAnimRate = this.mDefaultAnimRate;
			}
			if (--d.mTimer == 0)
			{
				d.mState = 0;
				int num3 = this.mFlyingMaxTimer - this.mFlyingMinTimer;
				if (num3 <= 0)
				{
					num3 = 1;
				}
				d.mTimer = this.mFlyingMinTimer + Common.Rand() % num3;
			}
		}

		public virtual void DrawBug(Graphics g, Critter d, Transform t)
		{
			g.SetColorizeImages(true);
			this.mDrawColor.SetColor(0, 0, 0, (int)d.mAlpha);
			g.SetColor(this.mDrawColor);
			if (g.Is3D())
			{
				g.DrawImageTransformF(d.mImage, t, d.mImage.GetCelRect(d.mCel), Common._S(d.mX + 3f), Common._S(d.mY + 3f));
			}
			else
			{
				g.DrawImageTransform(d.mImage, t, d.mImage.GetCelRect(d.mCel), Common._S(d.mX + 3f), Common._S(d.mY + 3f));
			}
			g.SetColorizeImages(false);
			if (d.mAlpha != 255f)
			{
				g.SetColorizeImages(true);
				this.mDrawColor.SetColor(255, 255, 255, (int)d.mAlpha);
				g.SetColor(this.mDrawColor);
			}
			if (g.Is3D())
			{
				g.DrawImageTransformF(d.mImage, t, d.mImage.GetCelRect(d.mCel), Common._S(d.mX), Common._S(d.mY));
			}
			else
			{
				g.DrawImageTransform(d.mImage, t, d.mImage.GetCelRect(d.mCel), Common._S(d.mX), Common._S(d.mY));
			}
			g.SetColorizeImages(false);
		}

		public FlyingBug()
		{
			this.mResGroup = "LandCritters";
			this.mMinBugs = (this.mMaxBugs = 0);
			this.mNoEnterRect = new Rect(0, 0, 0, 0);
			this.mReverseTimer = (this.mReverseRotateDelay = 0);
			this.mMaxRotateDegrees = 360;
			this.mAllowRest = true;
			this.mNoRotateUntilOnScreen = false;
		}

		public override void Update()
		{
			if (!GameApp.gApp.mGraphicsDriver.Is3D())
			{
				return;
			}
			this.mUpdateCount++;
			Rect rect = this.mBoard.GetGun().GetRect();
			for (int i = 0; i < this.mBugs.Count; i++)
			{
				Critter critter = this.mBugs[i];
				critter.mUpdateCount++;
				if (this.mUpdateCount % critter.mAnimRate == 0 && critter.mState != 3)
				{
					critter.mCel = (critter.mCel + 1) % critter.mImage.mNumCols;
				}
				critter.mFadeOut = false;
				bool flag = false;
				for (int j = 0; j < this.mBoard.mLevel.mNumCurves; j++)
				{
					int waypointFromXY = this.mBoard.mLevel.mCurveMgr[j].GetWaypointFromXY(critter.mX, critter.mY, ref flag, (uint)Common._M(0), (uint)Common._M1(1200));
					if (waypointFromXY != -1 && this.mBoard.mLevel.mCurveMgr[j].GetBallFromWaypoint(waypointFromXY) != null)
					{
						critter.mFadeOut = true;
						break;
					}
				}
				if (rect.Intersects((int)critter.mX, (int)critter.mY, critter.mImage.GetCelWidth(), critter.mImage.GetCelHeight()))
				{
					critter.mFadeOut = true;
				}
				int num = Common._M(4);
				if (critter.mFadeOut && critter.mAlpha > (float)Common._M(64))
				{
					critter.mAlpha = Math.Max(0f, critter.mAlpha - (float)num);
				}
				else if (!critter.mFadeOut && critter.mAlpha < 255f)
				{
					critter.mAlpha = Math.Min(255f, critter.mAlpha + (float)num);
				}
				switch (critter.mState)
				{
				case 0:
					this.UpdateStateFlying(critter);
					break;
				case 1:
					this.UpdateStateRotating(critter);
					break;
				case 2:
					this.UpdateStateSlowingDown(critter);
					break;
				case 3:
					this.UpdateStateResting(critter);
					break;
				case 4:
					this.UpdateStateAccelerating(critter);
					break;
				}
				this.mTestRect.SetValue(-50, -50, GameApp.gApp.mWidth + 100, GameApp.gApp.mHeight + 100);
				if (this.mTestRect.Intersects((int)critter.mX, (int)critter.mY, critter.mImage.GetCelWidth(), critter.mImage.GetCelHeight()))
				{
					this.SetupBug(critter);
				}
			}
		}

		public override void DrawAboveBalls(Graphics g)
		{
			if (!g.Is3D())
			{
				return;
			}
			for (int i = 0; i < this.mBugs.Count; i++)
			{
				Critter critter = this.mBugs[i];
				this.mDrawTransform.Reset();
				this.mDrawTransform.RotateRad(critter.mAngle - 1.570795f);
				if (!Common._eq(critter.mSize, 1f, 0.0001f))
				{
					this.mDrawTransform.Scale(critter.mSize, critter.mSize);
				}
				this.DrawBug(g, critter, this.mDrawTransform);
			}
		}

		public override string GetName()
		{
			return "FlyingBug";
		}

		protected List<Critter> mBugs = new List<Critter>();

		protected Board mBoard;

		protected Rect mNoEnterRect;

		protected int mMinBugs;

		protected int mMaxBugs;

		protected int mReverseTimer;

		protected int mReverseRotateDelay;

		protected int mRotateMinTimer;

		protected int mRotateMaxTimer;

		protected int mTimeToSlowDown;

		protected int mFlyingMinTimer;

		protected int mFlyingMaxTimer;

		protected int mDefaultAnimRate;

		protected int mRestingMinTimer;

		protected int mRestingMaxTimer;

		protected int mAccelerationTime;

		protected int mMaxRotateDegrees;

		protected bool mAllowRest;

		protected bool mNoRotateUntilOnScreen;

		protected Color mDrawColor = default(Color);

		protected Rect mTestRect = default(Rect);

		protected Transform mDrawTransform = new Transform();

		protected enum State
		{
			State_Flying,
			State_Rotating,
			State_SlowingDown,
			State_Resting,
			State_Accelerating
		}
	}
}
