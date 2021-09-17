using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class Ball
	{
		public void DrawStandardPower(Graphics g, int img_id, int cel, int thePowerType)
		{
			GameApp gApp = GameApp.gApp;
			ResID id = (ResID)(img_id + this.mColorType);
			if (gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_POWERUPS_GREEN_CBM;
			}
			else if (gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_POWERUPS_PURPLE_CBM;
			}
			else if (gApp.mColorblind && this.mColorType == 5)
			{
				id = ResID.IMAGE_POWERUPS_WHITE_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			Image imageByID2;
			if (this.mPowerType == PowerType.PowerType_MoveBackwards)
			{
				imageByID2 = Res.GetImageByID(ResID.IMAGE_POWERUP_REVERSE_ANYCOLOR);
			}
			else if (this.mPowerType == PowerType.PowerType_Laser)
			{
				imageByID2 = Res.GetImageByID(ResID.IMAGE_POWERUP_LAZER_ANYCOLOR);
			}
			else
			{
				imageByID2 = Res.GetImageByID(ResID.IMAGE_POWERUPS_PULSES);
			}
			float num = ZumasRevenge.Common._S(this.mX) - (float)(imageByID.GetCelWidth() / 2);
			float num2 = ZumasRevenge.Common._S(this.mY) - (float)(imageByID.GetCelHeight() / 2);
			float num3 = ((this.mPowerType == PowerType.PowerType_MoveBackwards) ? 1.570795f : (-1.570795f));
			bool flag = gApp.Is3DAccelerated();
			if (flag)
			{
				Rect celRect = imageByID.GetCelRect(cel);
				g.DrawImageRotatedF(imageByID, (float)((int)num), (float)((int)num2), (double)(this.mRotation + num3), celRect);
			}
			else
			{
				BlendedImage blendedImage = Ball.CreateBlendedPowerup(thePowerType, this.mColorType, imageByID, cel);
				blendedImage.Draw(g, num, num2);
			}
			if (this.mPowerType == PowerType.PowerType_MoveBackwards || this.mPowerType == PowerType.PowerType_Laser)
			{
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				g.SetColor(new Color(ZumasRevenge.Common.gBrightBallColors[this.mColorType]));
				float num4 = (float)imageByID2.GetCelWidth() / 2f;
				float num5 = (float)imageByID2.GetCelHeight() / 2f;
				num = ZumasRevenge.Common._S(this.mX) - num4;
				num2 = ZumasRevenge.Common._S(this.mY) - num5;
				Rect celRect2 = imageByID2.GetCelRect(this.mCel);
				if (flag)
				{
					g.DrawImageRotatedF(imageByID2, num, num2 - (float)ZumasRevenge.Common._M(0), (double)(this.mRotation + num3), num4, num5 + (float)ZumasRevenge.Common._M1(0), celRect2);
				}
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
				return;
			}
			if (this.mPulseState < 2)
			{
				g.SetColorizeImages(true);
				int mAlpha = 255 - this.mPulseTimer * ((this.mPulseState == 0) ? ZumasRevenge.Common._M(4) : ZumasRevenge.Common._M1(2));
				Color color = new Color(ZumasRevenge.Common.gBrightBallColors[this.mColorType]);
				if (gApp.mColorblind)
				{
					color = new Color(Color.White);
				}
				color.mAlpha = mAlpha;
				g.SetColor(color);
				g.SetDrawMode(1);
				float num6 = (float)imageByID2.GetCelWidth() / 2f;
				float num7 = (float)imageByID2.GetCelHeight() / 2f;
				num = ZumasRevenge.Common._S(this.mX) - num6;
				num2 = ZumasRevenge.Common._S(this.mY) - num7;
				Rect celRect3 = imageByID2.GetCelRect(cel);
				if (flag)
				{
					g.DrawImageRotatedF(imageByID2, num, num2 - (float)ZumasRevenge.Common._M(0), (double)(this.mRotation + num3), num6, num7 + (float)ZumasRevenge.Common._M1(0), celRect3);
				}
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
		}

		public void DrawNewPower(Graphics g, char theLetter, int xoff, int yoff)
		{
			GameApp gApp = GameApp.gApp;
			bool flag = gApp.Is3DAccelerated();
			Image imageByID = Res.GetImageByID(ResID.IMAGE_BALL);
			float num = ZumasRevenge.Common._S(this.mX + (float)xoff) - (float)(imageByID.mWidth / 2);
			float num2 = ZumasRevenge.Common._S(this.mY + (float)yoff) - (float)(imageByID.mHeight / 2);
			g.SetColorizeImages(true);
			g.SetColor(new Color(ZumasRevenge.Common.gBallColors[this.mColorType]));
			if (MathUtils._eq(this.mRadius, (float)ZumasRevenge.Common.GetDefaultBallRadius()))
			{
				if (flag)
				{
					g.DrawImageF(imageByID, num, num2);
				}
				else
				{
					g.DrawImage(imageByID, (int)num, (int)num2);
				}
			}
			else
			{
				this.mGlobalTransform.Reset();
				float num3 = this.mRadius / (float)ZumasRevenge.Common.GetDefaultBallRadius();
				this.mGlobalTransform.Scale(num3, num3);
				num = this.mX + (float)xoff;
				num2 = this.mY + (float)yoff;
				if (flag)
				{
					g.DrawImageTransformF(imageByID, this.mGlobalTransform, num, num2);
				}
				else
				{
					g.DrawImageTransform(imageByID, this.mGlobalTransform, num, num2);
				}
			}
			g.SetColorizeImages(false);
			g.SetColor(new Color(ZumasRevenge.Common._M(16777215)));
			g.SetFont(Res.GetFontByID(ResID.FONT_MAIN22));
			string theString = theLetter.ToString();
			g.DrawString(theString, (int)(ZumasRevenge.Common._S(this.mX + (float)xoff) - (float)(g.GetFont().CharWidth(theLetter) / 2)), (int)(ZumasRevenge.Common._S(this.mY + (float)yoff) - (float)(g.GetFont().GetHeight() / 2) + (float)g.GetFont().GetAscent()));
		}

		public void DrawNewPower(Graphics g, char theLetter)
		{
			this.DrawNewPower(g, theLetter, 0, 0);
		}

		public void DrawPower(Graphics g)
		{
			PowerType thePowerType = this.mPowerType;
			switch (thePowerType)
			{
			case PowerType.PowerType_ProximityBomb:
				this.DrawStandardPower(g, 870, 3, (int)thePowerType);
				return;
			case PowerType.PowerType_SlowDown:
				this.DrawStandardPower(g, 870, 2, (int)thePowerType);
				return;
			case PowerType.PowerType_Accuracy:
				this.DrawStandardPower(g, 870, 0, (int)thePowerType);
				return;
			case PowerType.PowerType_MoveBackwards:
				this.DrawStandardPower(g, 870, 4, (int)thePowerType);
				return;
			case PowerType.PowerType_Lob:
			case PowerType.PowerType_BombBullet:
			case PowerType.PowerType_BallEater:
			case PowerType.PowerType_Fireball:
			case PowerType.PowerType_ShieldFrog:
			case PowerType.PowerType_FreezeBoss:
				break;
			case PowerType.PowerType_Cannon:
				this.DrawStandardPower(g, 870, 5, (int)thePowerType);
				return;
			case PowerType.PowerType_ColorNuke:
				this.DrawStandardPower(g, 870, 1, (int)thePowerType);
				return;
			case PowerType.PowerType_Laser:
				this.DrawStandardPower(g, 870, 6, (int)thePowerType);
				return;
			case PowerType.PowerType_GauntletMultBall:
				this.DrawMultPowerup(g);
				break;
			default:
				return;
			}
		}

		public void DrawExplosion(Graphics g)
		{
		}

		protected void DoDrawBase(Graphics g, int xoff, int yoff)
		{
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				this.DrawPower(g);
				return;
			}
			int num = ((GameApp.gApp.GetBoard().GetHallucinateTimer() > 0) ? this.mDisplayType : this.mColorType);
			ResID id = ResID.IMAGE_BLUE_BALL + num;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			float x = ZumasRevenge.Common._S(this.mX + (float)xoff - this.mRadius);
			float y = ZumasRevenge.Common._S(this.mY + (float)yoff - this.mRadius);
			int frame = this.GetFrame(imageByID);
			this.mLastFrame = frame;
			if (GameApp.gApp.Is3DAccelerated())
			{
				Rect celRect = imageByID.GetCelRect(frame);
				this.mGlobalTransform.Reset();
				this.mGlobalTransform.RotateRad(this.mRotation);
				if (this.mDrawScale != 1f)
				{
					this.mGlobalTransform.Scale(this.mDrawScale, this.mDrawScale);
				}
				g.DrawImageTransformF(imageByID, this.mGlobalTransform, celRect, ZumasRevenge.Common._S(this.mX + (float)xoff), ZumasRevenge.Common._S(this.mY + (float)yoff));
				return;
			}
			BlendedImage blendedImage = Ball.CreateBlendedBall(num);
			blendedImage.Draw(g, x, y);
		}

		protected void DoDrawAdditive(Graphics g, int xoff, int yoff)
		{
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				return;
			}
			int num = ((GameApp.gApp.GetBoard().GetHallucinateTimer() > 0) ? this.mDisplayType : this.mColorType);
			ResID id = ResID.IMAGE_BLUE_BALL + num;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			ZumasRevenge.Common._S(this.mX + (float)xoff - this.mRadius);
			ZumasRevenge.Common._S(this.mY + (float)yoff - this.mRadius);
			int frame = this.GetFrame(imageByID);
			this.mLastFrame = frame;
			if (GameApp.gApp.Is3DAccelerated())
			{
				Rect celRect = imageByID.GetCelRect(frame);
				this.mGlobalTransform.Reset();
				this.mGlobalTransform.RotateRad(this.mRotation);
				if (this.mDrawScale != 1f)
				{
					this.mGlobalTransform.Scale(this.mDrawScale, this.mDrawScale);
				}
				if (this.mHilightPulse)
				{
					g.SetColorizeImages(true);
					g.SetDrawMode(1);
					g.SetColor(255, 255, 255);
					g.DrawImageTransformF(imageByID, this.mGlobalTransform, celRect, ZumasRevenge.Common._S(this.mX), ZumasRevenge.Common._S(this.mY));
					g.SetDrawMode(0);
					g.SetColorizeImages(false);
				}
			}
		}

		public void DoDraw(Graphics g, int xoff, int yoff)
		{
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				this.DrawPower(g);
				return;
			}
			int num = ((GameApp.gApp.GetBoard().GetHallucinateTimer() > 0) ? this.mDisplayType : this.mColorType);
			ResID id = ResID.IMAGE_BLUE_BALL + num;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			float x = ZumasRevenge.Common._S(this.mX + (float)xoff - this.mRadius);
			float y = ZumasRevenge.Common._S(this.mY + (float)yoff - this.mRadius);
			int frame = this.GetFrame(imageByID);
			this.mLastFrame = frame;
			if (GameApp.gApp.Is3DAccelerated())
			{
				Rect celRect = imageByID.GetCelRect(frame);
				this.mGlobalTransform.Reset();
				this.mGlobalTransform.RotateRad(this.mRotation);
				if (this.mDrawScale != 1f)
				{
					this.mGlobalTransform.Scale(this.mDrawScale, this.mDrawScale);
				}
				g.DrawImageTransformF(imageByID, this.mGlobalTransform, celRect, ZumasRevenge.Common._S(this.mX + (float)xoff), ZumasRevenge.Common._S(this.mY + (float)yoff));
				if (this.mHilightPulse)
				{
					g.SetColorizeImages(true);
					g.SetDrawMode(1);
					g.SetColor(255, 255, 255);
					g.DrawImageTransformF(imageByID, this.mGlobalTransform, celRect, ZumasRevenge.Common._S(this.mX), ZumasRevenge.Common._S(this.mY));
					g.SetDrawMode(0);
					g.SetColorizeImages(false);
					return;
				}
			}
			else
			{
				BlendedImage blendedImage = Ball.CreateBlendedBall(num);
				blendedImage.Draw(g, x, y);
			}
		}

		public void DoDraw(Graphics g)
		{
			this.DoDraw(g, 0, 0);
		}

		public void DrawMultPowerup(Graphics g)
		{
			GameApp gApp = GameApp.gApp;
			ResID id = ResID.IMAGE_MULTIPLIER_BALL_BLUE + this.GetColorType();
			bool flag = true;
			if (gApp.mColorblind && this.mColorType == 3)
			{
				flag = false;
				id = (g.Is3D() ? ResID.IMAGE_GREEN_BALL_CBM : ResID.IMAGE_MULTIPLIER_BALL_GREEN_CBM);
			}
			else if (gApp.mColorblind && this.mColorType == 4)
			{
				flag = false;
				id = (g.Is3D() ? ResID.IMAGE_PURPLE_BALL_CBM : ResID.IMAGE_MULTIPLIER_BALL_PURPLE_CBM);
			}
			Image imageByID = Res.GetImageByID(id);
			float num = ZumasRevenge.Common._S(this.mX) - (float)(imageByID.GetCelWidth() / 2);
			float num2 = ZumasRevenge.Common._S(this.mY) - (float)(imageByID.GetCelHeight() / 2);
			if (flag)
			{
				int multAlpha = Ball.GetMultAlpha(this.mMultBallCel);
				int num3 = ZumasRevenge.Common._M(255);
				if (multAlpha != num3)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, multAlpha);
				}
				BlendedImage blendedImage = null;
				BlendedImage blendedImage2 = null;
				if (!g.Is3D())
				{
					blendedImage = Ball.CreateBlendedPowerup(13, this.mColorType, imageByID, this.mMultBallCel);
					blendedImage2 = Ball.CreateBlendedPowerup(14, this.mColorType, imageByID, this.mMultBallCel2);
				}
				Rect celRect = imageByID.GetCelRect(this.mMultBallCel);
				if (g.Is3D())
				{
					g.DrawImageRotatedF(imageByID, num, num2, (double)this.mRotation, celRect);
				}
				else
				{
					blendedImage.Draw(g, num, num2);
				}
				g.SetColorizeImages(false);
				multAlpha = Ball.GetMultAlpha(this.mMultBallCel2);
				if (multAlpha != num3)
				{
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, multAlpha);
				}
				celRect = imageByID.GetCelRect(this.mMultBallCel2);
				if (g.Is3D())
				{
					g.DrawImageRotatedF(imageByID, num, num2, (double)this.mRotation, celRect);
				}
				else
				{
					blendedImage2.Draw(g, num, num2);
				}
				g.SetColorizeImages(false);
				g.SetDrawMode(1);
				g.SetColor(255, 255, 255, ZumasRevenge.Common._M(204));
				g.SetColorizeImages(true);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_MULTIPLIER_BALL_OUTER);
				celRect = imageByID2.GetCelRect(this.GetFrame(imageByID2, ZumasRevenge.Common._M(2)));
				if (g.Is3D())
				{
					g.DrawImageRotatedF(imageByID2, num, num2, (double)this.mRotation, celRect);
				}
				else
				{
					g.DrawImageRotated(imageByID2, (int)num, (int)num2, (double)this.mRotation, celRect);
				}
				g.SetColorizeImages(false);
				g.SetDrawMode(0);
				return;
			}
			if (g.Is3D())
			{
				Rect celRect2 = imageByID.GetCelRect(0);
				g.DrawImageRotatedF(imageByID, num, num2, (double)this.mRotation, celRect2);
				return;
			}
			BlendedImage blendedImage3 = Ball.CreateBlendedPowerup(13, this.mColorType, imageByID, 0);
			blendedImage3.Draw(g, num, num2);
		}

		public void UpdateProxmityBombExplosion()
		{
		}

		public void UpdateRotation()
		{
			if (this.mRotationInc != 0f)
			{
				this.mRotation += this.mRotationInc;
				if ((this.mRotationInc > 0f && this.mRotation > this.mDestRotation) || (this.mRotationInc < 0f && this.mRotation < this.mDestRotation))
				{
					this.mRotation = this.mDestRotation;
					this.mRotationInc = 0f;
				}
			}
		}

		public void SetupDefaultOverlayPulse()
		{
			int num = (int)Component.GetComponentValue(this.mOverlayPulse, 0f, this.mUpdateCount);
			this.mOverlayPulse.Clear();
			if (num == 128)
			{
				this.mOverlayPulse.Add(new Component(128f, 178f, this.mUpdateCount, this.mUpdateCount + 50));
				this.mOverlayPulse.Add(new Component(178f, 255f, this.mUpdateCount + 51, this.mUpdateCount + 60));
				this.mOverlayPulse.Add(new Component(255f, 128f, this.mUpdateCount + 61, this.mUpdateCount + 80));
				return;
			}
			this.mOverlayPulse.Add(new Component((float)num, 128f, this.mUpdateCount, this.mUpdateCount + 10));
		}

		public void SetupElectricOverlayPulse(bool force_fade_out)
		{
			int num = (int)Component.GetComponentValue(this.mOverlayPulse, 0f, this.mUpdateCount);
			this.mOverlayPulse.Clear();
			if (force_fade_out)
			{
				if (num == 0)
				{
					return;
				}
				this.mOverlayPulse.Add(new Component((float)num, 0f, this.mUpdateCount, this.mUpdateCount + 20));
				return;
			}
			else
			{
				if (num == 128)
				{
					this.mOverlayPulse.Add(new Component(128f, 255f, this.mUpdateCount, this.mUpdateCount + 20));
					this.mOverlayPulse.Add(new Component(255f, 128f, this.mUpdateCount + 21, this.mUpdateCount + 41));
					return;
				}
				this.mOverlayPulse.Add(new Component((float)num, 128f, this.mUpdateCount, this.mUpdateCount + 20));
				return;
			}
		}

		public void SetupElectricOverlayPulse()
		{
			this.SetupElectricOverlayPulse(false);
		}

		public static void ResetIdGen()
		{
			Ball.mIdGen = 0;
		}

		public Ball()
		{
			this.mFrog = null;
			this.mMultOverlayAlpha = 0;
			this.mMultFX = null;
			this.mInTunnel = false;
			this.mCannonFrame = -1;
			this.mId = ++Ball.mIdGen;
			this.mDoBossPulse = false;
			this.mBossBlinkTimer = 0;
			this.mDebugDrawID = false;
			this.mCurve = null;
			this.mUpdateCount = 0;
			this.mHilightPulse = false;
			this.mSuckFromCompacting = false;
			this.mX = 0f;
			this.mY = 0f;
			this.mColorType = 0;
			this.mDisplayType = 0;
			this.mRadius = (float)ZumasRevenge.Common.GetDefaultBallRadius();
			this.mSuckBack = true;
			this.mBullet = null;
			this.mCel = 0;
			this.mShouldRemove = false;
			this.mLastFrame = 0;
			this.mMultBallCel = 0;
			this.mMultBallCel2 = ZumasRevenge.Common._M(7);
			this.mIsCannon = false;
			this.mSpeedy = false;
			this.mElectricOverlayCel = 0;
			this.mList = null;
			this.mCollidesWithNext = false;
			this.mSuckCount = 0;
			this.mBackwardsCount = 0;
			this.mBackwardsSpeed = 0f;
			this.mComboCount = 0;
			this.mComboScore = 0;
			this.mRotation = 0f;
			this.mRotationInc = 0f;
			this.mNeedCheckCollision = false;
			this.mSuckPending = false;
			this.mShrinkClear = false;
			this.mIconCel = -1;
			this.mIconAppearScale = 1f;
			this.mIconScaleRate = 0f;
			this.mStartFrame = 0;
			this.mWayPoint = 0f;
			this.mPowerType = PowerType.PowerType_Max;
			this.mDestPowerType = PowerType.PowerType_Max;
			this.mPowerCount = 0;
			this.mPowerFade = 0;
			this.mGapBonus = 0;
			this.mNumGaps = 0;
			this.mParticles = null;
			this.mDrawScale = 1f;
			this.mExplodeFrame = 0;
			this.mPowerGracePeriod = 0;
			this.mLastPowerType = PowerType.PowerType_Max;
			this.mDoLaserAnim = false;
			this.mElectricExplodeOverlay.mLoopCount = (this.mElectricExplodeOverlay.mLayer1Cel = (this.mElectricExplodeOverlay.mLayer2Cel = 0));
			this.mExplodingFromLightning = false;
			this.mExploding = (this.mExplodingInTunnel = false);
		}

		public virtual void CopyFrom(Ball other)
		{
			this.mInTunnel = other.mInTunnel;
			this.mMultOverlayAlpha = other.mMultOverlayAlpha;
			this.mMultFX = other.mMultFX;
			this.mColorType = other.mColorType;
			this.mDisplayType = other.mDisplayType;
			this.mWayPoint = other.mWayPoint;
			this.mLastWayPoint = other.mLastWayPoint;
			this.mRotation = other.mRotation;
			this.mDestRotation = other.mDestRotation;
			this.mRotationInc = other.mRotationInc;
			this.mX = other.mX;
			this.mY = other.mY;
			this.mLastX = other.mLastX;
			this.mLastY = other.mLastY;
			this.mDrawScale = other.mDrawScale;
			this.mRadius = other.mRadius;
			this.mPulseState = other.mPulseState;
			this.mPulseTimer = other.mPulseTimer;
			this.mOverlayPulse.Clear();
			this.mOverlayPulse.AddRange(other.mOverlayPulse.ToArray());
			this.mElectricOverlay.Clear();
			this.mElectricOverlay.AddRange(other.mElectricOverlay.ToArray());
			this.mElectricExplodeOverlay = other.mElectricExplodeOverlay;
			this.mElectricOverlayCel = other.mElectricOverlayCel;
			this.mList = other.mList;
			this.mCurve = other.mCurve;
			this.mCollidesWithNext = other.mCollidesWithNext;
			this.mSuckPending = other.mSuckPending;
			this.mShrinkClear = other.mShrinkClear;
			this.mSuckFromCompacting = other.mSuckFromCompacting;
			this.mExplodingInTunnel = other.mExplodingInTunnel;
			this.mExploding = other.mExploding;
			this.mExplodingFromLightning = other.mExplodingFromLightning;
			this.mExplodeFrame = other.mExplodeFrame;
			this.mShouldRemove = other.mShouldRemove;
			this.mSpeedy = other.mSpeedy;
			this.mSuckBack = other.mSuckBack;
			this.mPowerGracePeriod = other.mPowerGracePeriod;
			this.mLastPowerType = other.mLastPowerType;
			this.mCannonFrame = other.mCannonFrame;
			this.mIsCannon = other.mIsCannon;
			this.mDoLaserAnim = other.mDoLaserAnim;
			this.mUpdateCount = other.mUpdateCount;
			this.mCel = other.mCel;
			this.mBullet = other.mBullet;
			this.mSuckCount = other.mSuckCount;
			this.mBackwardsCount = other.mBackwardsCount;
			this.mComboCount = other.mComboCount;
			this.mBackwardsSpeed = other.mBackwardsSpeed;
			this.mPowerCount = other.mPowerCount;
			this.mComboScore = other.mComboScore;
			this.mStartFrame = other.mStartFrame;
			this.mPowerFade = other.mPowerFade;
			this.mGapBonus = other.mGapBonus;
			this.mNumGaps = other.mNumGaps;
			this.mIconAppearScale = other.mIconAppearScale;
			this.mIconScaleRate = other.mIconScaleRate;
			this.mIconCel = other.mIconCel;
			this.mMultBallCel = other.mMultBallCel;
			this.mMultBallCel2 = other.mMultBallCel2;
			this.mParticles = other.mParticles;
			this.mPowerType = other.mPowerType;
			this.mDestPowerType = other.mDestPowerType;
			this.mHilightPulse = other.mHilightPulse;
			this.mDebugDrawID = other.mDebugDrawID;
			this.mDoBossPulse = other.mDoBossPulse;
			this.mBossBlinkTimer = other.mBossBlinkTimer;
			this.mLastFrame = other.mLastFrame;
			this.mFrog = other.mFrog;
		}

		public virtual void Dispose()
		{
			if (this.mCurve != null && this.mCurve.mBoard != null)
			{
				this.mCurve.mBoard.BallDeleted(this);
			}
			Board board = GameApp.gApp.GetBoard();
			if (board != null && this == board.GetGuideBall())
			{
				board.GuideBallInvalidated();
			}
			this.mParticles = null;
			this.CleanUpMultiplierOverlays();
		}

		public void SetPos(float x, float y)
		{
			this.mX = x;
			this.mY = y;
		}

		public void SetWayPoint(float thePoint, bool in_tunnel)
		{
			this.mWayPoint = thePoint;
			this.mInTunnel = in_tunnel;
		}

		public int GetFrame(Image img, int div)
		{
			int num = ((img.mNumCols == 1) ? img.mNumRows : (img.mNumRows * img.mNumCols));
			int num2 = (int)this.mWayPoint;
			int num3 = (num2 / div + this.mStartFrame) % num;
			if (num3 < 0)
			{
				num3 = -num3;
			}
			else if (num3 >= num)
			{
				num3 = num - 1;
			}
			return num3;
		}

		public int GetFrame(Image img)
		{
			return this.GetFrame(img, 1);
		}

		public void CleanUpMultiplierOverlays()
		{
			GameApp.gApp.ReleaseGenericCachedEffect(this.mMultFX);
			this.mMultFX = null;
		}

		public void SetRotation(float theRot, bool immediate)
		{
			if (immediate)
			{
				this.mRotation = theRot;
				return;
			}
			if (MathUtils._eq(theRot, this.mRotation, 0.001f))
			{
				return;
			}
			while (Math.Abs(theRot - this.mRotation) > 3.14159f)
			{
				if (theRot > this.mRotation)
				{
					theRot -= 6.28318f;
				}
				else
				{
					theRot += 6.28318f;
				}
			}
			this.mDestRotation = theRot;
			this.mRotationInc = 0.104719669f;
			if (theRot < this.mRotation)
			{
				this.mRotationInc = -this.mRotationInc;
			}
		}

		public void SetRotation(float theRot)
		{
			this.SetRotation(theRot, false);
		}

		public virtual void DrawBase(Graphics g, int xoff, int yoff)
		{
			if (this.mDrawScale <= 0f || this.mColorType == -1)
			{
				return;
			}
			if (this.mExploding && !this.mShrinkClear && this.mExplodingInTunnel)
			{
				if (g.Is3D())
				{
					this.DrawExplosion(g);
				}
			}
			else if (!this.mExploding || this.mShrinkClear)
			{
				this.DoDrawBase(g, xoff, yoff);
			}
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		public virtual void DrawAdditive(Graphics g, int xoff, int yoff)
		{
			if (this.mDrawScale <= 0f || this.mColorType == -1)
			{
				return;
			}
			if (this.mExploding && !this.mShrinkClear && this.mExplodingInTunnel)
			{
				if (g.Is3D())
				{
					this.DrawExplosion(g);
				}
			}
			else if (!this.mExploding || this.mShrinkClear)
			{
				this.DoDrawAdditive(g, xoff, yoff);
				if (this.mPowerFade != 0 && (this.mCurve == null || this.mCurve.mPostZumaFlashTimer <= 0))
				{
					int num = ((this.mPowerType == PowerType.PowerType_GauntletMultBall) ? ((int)ZumasRevenge.Common._M(2f)) : 4);
					if (((this.mPowerFade >> num) & 1) != 0)
					{
						g.SetDrawMode(1);
						this.DoDrawBase(g, xoff, yoff);
						this.DoDrawAdditive(g, xoff, yoff);
					}
				}
				else if ((this.mDoBossPulse && (float)this.mBossBlinkTimer < ZumasRevenge.Common._M(10f)) || (this.mCurve != null && this.mCurve.mPostZumaFlashTimer > 0))
				{
					g.SetDrawMode(1);
					this.DoDrawBase(g, xoff, yoff);
					this.DoDrawAdditive(g, xoff, yoff);
				}
			}
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		public virtual void Draw(Graphics g, int xoff, int yoff)
		{
			if (this.mDrawScale <= 0f || this.mColorType == -1)
			{
				return;
			}
			if (this.mExploding && !this.mShrinkClear && this.mExplodingInTunnel)
			{
				if (g.Is3D())
				{
					this.DrawExplosion(g);
				}
			}
			else if (!this.mExploding || this.mShrinkClear)
			{
				this.DoDraw(g, xoff, yoff);
				if (this.mPowerFade != 0 && (this.mCurve == null || this.mCurve.mPostZumaFlashTimer <= 0))
				{
					int num = ((this.mPowerType == PowerType.PowerType_GauntletMultBall) ? ((int)ZumasRevenge.Common._M(2f)) : 4);
					if (((this.mPowerFade >> num) & 1) != 0)
					{
						g.SetDrawMode(1);
						this.DoDraw(g, xoff, yoff);
					}
				}
				else if ((this.mDoBossPulse && (float)this.mBossBlinkTimer < ZumasRevenge.Common._M(10f)) || (this.mCurve != null && this.mCurve.mPostZumaFlashTimer > 0))
				{
					g.SetDrawMode(1);
					this.DoDraw(g, xoff, yoff);
					g.SetDrawMode(0);
				}
			}
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
			if (this.mDebugDrawID)
			{
				Font fontByID = Res.GetFontByID(ResID.FONT_MAIN22);
				g.SetFont(fontByID);
				g.SetColor(Color.Black);
				g.FillRect((int)ZumasRevenge.Common._S(this.mX - 12f), (int)ZumasRevenge.Common._S(this.mY - 8f), ZumasRevenge.Common._S(24), ZumasRevenge.Common._S(16));
				g.SetColor(Color.White);
				g.DrawString(string.Format("{0}", this.mId), (int)ZumasRevenge.Common._S(this.mX - 10f), (int)ZumasRevenge.Common._S(this.mY - 12f) + fontByID.GetAscent());
			}
		}

		public void Draw(Graphics g)
		{
			this.Draw(g, 0, 0);
		}

		public void DrawProximityBombExplosion(Graphics g)
		{
		}

		public void DrawShadow(Graphics g)
		{
			if (!GlobalMembers.gSexyApp.Is3DAccelerated())
			{
				return;
			}
			if (this.mExploding)
			{
				return;
			}
			Transform transform = new Transform();
			float num = ZumasRevenge.Common._S(this.mX - 3f);
			float num2 = ZumasRevenge.Common._S(this.mY + 5f);
			if (this.mDrawScale > 1f)
			{
				num -= ZumasRevenge.Common._S(ZumasRevenge.Common._M(9f)) * (this.mDrawScale - 1f);
				num2 += ZumasRevenge.Common._S(ZumasRevenge.Common._M(15f)) * (this.mDrawScale - 1f);
				transform.Scale(this.mDrawScale, this.mDrawScale);
			}
			g.DrawImageTransformF(Res.GetImageByID(ResID.IMAGE_BALL_SHADOW), transform, num, num2);
		}

		public void DrawTopLayer(Graphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			if ((this.mPowerType != PowerType.PowerType_Max || this.mElectricOverlay.size<Component>() > 0 || this.mOverlayPulse.size<Component>() > 0) && !this.GetIsExploding())
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_BALL_GLOW);
				Color color = Ball.gOverlayColors[this.mColorType];
				color.mAlpha = (int)Component.GetComponentValue(this.mOverlayPulse, 0f, this.mUpdateCount);
				g.SetColor(color);
				g.SetColorizeImages(true);
				g.SetDrawMode(1);
				int num = (int)ZumasRevenge.Common._S(this.mX - this.mRadius);
				int num2 = (int)ZumasRevenge.Common._S(this.mY - this.mRadius);
				num -= (imageByID.mWidth - ZumasRevenge.Common._S(ZumasRevenge.Common.GetDefaultBallSize())) / 2 - 1;
				num2 -= (imageByID.mHeight - ZumasRevenge.Common._S(ZumasRevenge.Common.GetDefaultBallSize())) / 2 - 1;
				if (!GameApp.gApp.mColorblind)
				{
					if (graphics3D != null)
					{
						g.DrawImageF(imageByID, (float)num, (float)num2);
					}
					else
					{
						g.DrawImage(imageByID, num, num2);
					}
				}
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
			if (this.mMultFX != null && (!GameApp.gApp.mColorblind || (this.mColorType != 3 && this.mColorType != 4)))
			{
				this.mMultFX.DrawLayer(g, this.mMultFX.GetLayer("Top"));
				this.mMultFX.DrawLayerNormal(g, this.mMultFX.GetLayer("Top"));
				this.mMultFX.DrawLayerAdditive(g, this.mMultFX.GetLayer("Top"));
				this.mMultFX.DrawPhisycalLayer(g, this.mMultFX.GetLayer("Top"));
			}
			if (this.mExplodingInTunnel)
			{
				this.DrawLightningExplosion(g);
			}
			if (this.mElectricOverlay.size<Component>() > 0)
			{
				int num3 = (int)Component.GetComponentValue(this.mElectricOverlay, 0f, this.mUpdateCount);
				g.SetDrawMode(1);
				if (num3 != 255)
				{
					g.SetColor(255, 255, 255, num3);
					g.SetColorizeImages(true);
				}
				g.SetDrawMode(0);
				if (num3 != 255)
				{
					g.SetColorizeImages(false);
				}
			}
		}

		public void DrawBottomLayer(Graphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			if (!this.mCurve.mWayPointMgr.InTunnel(this, true))
			{
				this.mCurve.mWayPointMgr.InTunnel(this, false);
			}
			if (this.mMultFX != null && graphics3D != null)
			{
				this.mMultFX.DrawLayer(g, this.mMultFX.GetLayer("Bottom"));
				this.mMultFX.DrawLayerNormal(g, this.mMultFX.GetLayer("Bottom"));
				this.mMultFX.DrawLayerAdditive(g, this.mMultFX.GetLayer("Bottom"));
				this.mMultFX.DrawPhisycalLayer(g, this.mMultFX.GetLayer("Bottom"));
			}
			if (this.mDoLaserAnim)
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_LAZER_BURN);
				Rect celRect = imageByID.GetCelRect(Ball.mLaserAnimCel);
				float num = CommonMath.AngleBetweenPoints((float)this.mFrog.GetCenterX(), (float)this.mFrog.GetCenterY(), this.mX, this.mY) + 1.570795f;
				g.DrawImageRotated(imageByID, (int)ZumasRevenge.Common._S(this.mX + (float)ZumasRevenge.Common._M(-38)), (int)ZumasRevenge.Common._S(this.mY + (float)ZumasRevenge.Common._M1(-52)), (double)num, ZumasRevenge.Common._S(ZumasRevenge.Common._M2(38)), ZumasRevenge.Common._S(ZumasRevenge.Common._M3(52)), celRect);
			}
		}

		public void DrawAboveBalls(Graphics g)
		{
			if (this.mIconCel != -1 && MathUtils._geq(this.mIconAppearScale, 1f) && g.Is3D())
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_POWERUPS_PULSES);
				float num = ((this.GetPowerOrDestType() == PowerType.PowerType_MoveBackwards) ? 0f : (-1.570795f));
				int num2 = (int)ZumasRevenge.Common._S(this.mX);
				int num3 = (int)ZumasRevenge.Common._S(this.mY);
				g.SetDrawMode(1);
				g.SetColorizeImages(true);
				g.SetColor(new Color(ZumasRevenge.Common.gBallColors[this.mColorType]));
				this.mGlobalTransform.Reset();
				this.mGlobalTransform.Scale(this.mIconAppearScale, this.mIconAppearScale);
				this.mGlobalTransform.RotateRad(this.mRotation + num);
				g.DrawImageTransform(imageByID, this.mGlobalTransform, imageByID.GetCelRect(this.mIconCel), (float)num2, (float)num3);
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
			if (this.mExploding && !this.mShrinkClear && !this.mExplodingInTunnel)
			{
				this.DrawExplosion(g);
			}
			if (!this.mExplodingInTunnel)
			{
				this.DrawLightningExplosion(g);
			}
		}

		public void DrawLightningExplosion(Graphics g)
		{
			if (this.mElectricExplodeOverlay.mLayer1Alpha.size<Component>() > 0)
			{
				g.SetDrawMode(1);
				int num = (int)Component.GetComponentValue(this.mElectricExplodeOverlay.mLayer2Alpha, 255f, this.mUpdateCount);
				if (num != 255)
				{
					g.SetColor(255, 255, 255, num);
					g.SetColorizeImages(true);
				}
				g.SetColorizeImages(false);
				num = (int)Component.GetComponentValue(this.mElectricExplodeOverlay.mLayer1Alpha, 255f, this.mUpdateCount);
				Component.GetComponentValue(this.mElectricExplodeOverlay.mLayer1Scale, 1f, this.mUpdateCount);
				if (num != 255)
				{
					g.SetColor(255, 255, 255, num);
					g.SetColorizeImages(true);
				}
				g.SetDrawMode(0);
				if (num != 255)
				{
					g.SetColorizeImages(false);
				}
			}
		}

		public void DoElectricOverlay(bool val)
		{
			if (!val && Enumerable.Count<Component>(this.mElectricOverlay) > 0)
			{
				int num = (int)Component.GetComponentValue(this.mElectricOverlay, 0f, this.mUpdateCount);
				this.mElectricOverlay.Clear();
				int num2 = (int)(0.0392156877f * (float)num);
				this.mElectricOverlay.Add(new Component((float)num, 0f, this.mUpdateCount, this.mUpdateCount + ((num2 < 1) ? 1 : num2)));
				this.SetupDefaultOverlayPulse();
				return;
			}
			if (val && Enumerable.Count<Component>(this.mElectricOverlay) == 0)
			{
				this.mElectricOverlay.Add(new Component(0f, 255f, this.mUpdateCount, this.mUpdateCount + 10));
				return;
			}
			if (!val)
			{
				if (!val)
				{
					this.SetupDefaultOverlayPulse();
				}
				return;
			}
			int num3 = (int)Component.GetComponentValue(this.mElectricOverlay, 0f, this.mUpdateCount);
			if (num3 == 255)
			{
				return;
			}
			this.mElectricOverlay.Clear();
			int num4 = (int)(0.0392156877f * (float)num3);
			this.mElectricOverlay.Add(new Component((float)num3, 255f, this.mUpdateCount, this.mUpdateCount + ((num4 < 1) ? 1 : num4)));
			this.SetupElectricOverlayPulse();
		}

		public bool CollidesWithPhysically(Ball theBall, int thePad)
		{
			float num = theBall.GetX() - this.GetX();
			float num2 = theBall.GetY() - this.GetY();
			float num3 = (float)theBall.GetRadius() + (float)(thePad * 2) + (float)this.GetRadius();
			return num * num + num2 * num2 < num3 * num3;
		}

		public bool CollidesWithPhysically(Ball theBall)
		{
			return this.CollidesWithPhysically(theBall, 0);
		}

		public bool CollidesWith(Ball theBall, int thePad)
		{
			return Math.Abs((float)((int)this.mWayPoint) - (float)((int)theBall.mWayPoint)) < (float)((ZumasRevenge.Common.GetDefaultBallRadius() + thePad) * 2);
		}

		public bool CollidesWith(Ball theBall)
		{
			return this.CollidesWith(theBall, 0);
		}

		public bool CollidesWithPhysically(int pointx, int pointy, int radius)
		{
			float num = (float)pointx - this.GetX();
			float num2 = (float)pointy - this.GetY();
			float num3 = (float)radius + (float)this.GetRadius();
			return num * num + num2 * num2 < num3 * num3;
		}

		public bool Intersects(SexyVector3 p1, SexyVector3 v1, ref float t)
		{
			SexyVector3 v2 = new SexyVector3(p1.x - this.mX, p1.y - this.mY, 0f);
			float num = this.mRadius - (float)ZumasRevenge.Common._M(1);
			float num2 = v1.Dot(v1);
			float num3 = 2f * v2.Dot(v1);
			float num4 = v2.Dot(v2) - num * 2f * (num * 2f);
			float num5 = num3 * num3 - 4f * num2 * num4;
			if (num5 < 0f)
			{
				return false;
			}
			num5 = (float)Math.Sqrt((double)num5);
			t = (-num3 - num5) / (2f * num2);
			return true;
		}

		public void SetBullet(Bullet theBullet)
		{
			this.mBullet = theBullet;
		}

		public void SetCollidesWithPrev(bool collidesWithPrev)
		{
			Ball prevBall = this.GetPrevBall();
			if (prevBall != null)
			{
				prevBall.SetCollidesWithNext(collidesWithPrev);
			}
		}

		public bool GetCollidesWithPrev()
		{
			Ball prevBall = this.GetPrevBall();
			return prevBall != null && prevBall.GetCollidesWithNext();
		}

		public void UpdateCollisionInfo(int thePad)
		{
			Ball prevBall = this.GetPrevBall();
			Ball nextBall = this.GetNextBall();
			if (prevBall != null)
			{
				prevBall.SetCollidesWithNext(prevBall.CollidesWith(this, thePad));
			}
			if (nextBall != null)
			{
				this.SetCollidesWithNext(nextBall.CollidesWith(this, thePad));
				return;
			}
			this.SetCollidesWithNext(false);
		}

		public void UpdateCollisionInfo()
		{
			this.UpdateCollisionInfo(0);
		}

		public void SetPowerType(PowerType theType, bool delay)
		{
			this.mDoBossPulse = false;
			if (theType == this.mPowerType)
			{
				return;
			}
			this.mPulseState = 0;
			this.mPulseTimer = 0;
			this.mIconCel = -1;
			if (theType != PowerType.PowerType_Max)
			{
				this.mPowerGracePeriod = 0;
				this.mLastPowerType = PowerType.PowerType_Max;
			}
			if (delay)
			{
				this.mDestPowerType = theType;
				if (theType == PowerType.PowerType_Max && this.mPowerType == PowerType.PowerType_GauntletMultBall)
				{
					this.mPowerFade = 300;
				}
				else
				{
					this.mPowerFade = 100;
				}
				switch (theType)
				{
				case PowerType.PowerType_ProximityBomb:
					this.mIconCel = 3;
					break;
				case PowerType.PowerType_SlowDown:
					this.mIconCel = 2;
					break;
				case PowerType.PowerType_Accuracy:
					this.mIconCel = 0;
					break;
				case PowerType.PowerType_MoveBackwards:
					this.mIconCel = 4;
					break;
				case PowerType.PowerType_Cannon:
					this.mIconCel = 5;
					break;
				case PowerType.PowerType_ColorNuke:
					this.mIconCel = 1;
					break;
				case PowerType.PowerType_Laser:
					this.mIconCel = 6;
					break;
				}
				int soundByID = Res.GetSoundByID(ResID.SOUND_MULT_APPEAR);
				int soundByID2 = Res.GetSoundByID(ResID.SOUND_POWERUP_APPEARS);
				int soundByID3 = Res.GetSoundByID(ResID.SOUND_MULT_DISAPPEAR);
				int soundByID4 = Res.GetSoundByID(ResID.SOUND_POWERUP_DISAPPEARS);
				if (theType != PowerType.PowerType_Max)
				{
					if (theType == PowerType.PowerType_GauntletMultBall)
					{
						((GameApp)GlobalMembers.gSexyApp).PlaySample(soundByID);
					}
					else
					{
						((GameApp)GlobalMembers.gSexyApp).PlaySample(soundByID2);
					}
				}
				else if (this.GetPowerOrDestType() != PowerType.PowerType_Max)
				{
					if (this.GetPowerOrDestType() == PowerType.PowerType_GauntletMultBall)
					{
						((GameApp)GlobalMembers.gSexyApp).PlaySample(soundByID3);
					}
					else
					{
						((GameApp)GlobalMembers.gSexyApp).PlaySample(soundByID4);
					}
				}
				this.mIconAppearScale = 5f;
				this.mIconScaleRate = (this.mIconAppearScale - 1f) / (float)this.mPowerFade;
			}
			else
			{
				this.mDestPowerType = PowerType.PowerType_Max;
				this.mPowerType = theType;
			}
			if (theType != PowerType.PowerType_Max && this.mCurve != null)
			{
				this.mCurve.SetColorHasPowerup(this.mColorType, true);
			}
			this.SetupDefaultOverlayPulse();
		}

		public void SetPowerType(PowerType theType)
		{
			this.SetPowerType(theType, true);
		}

		public PowerType GetPowerOrDestType(bool include_grace_period)
		{
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				return this.mPowerType;
			}
			if (this.mPowerGracePeriod > 0 && this.mLastPowerType != PowerType.PowerType_Max)
			{
				return this.mLastPowerType;
			}
			return this.mDestPowerType;
		}

		public PowerType GetPowerOrDestType()
		{
			return this.GetPowerOrDestType(true);
		}

		public void RemoveFromList()
		{
			if (this.mList != null)
			{
				this.mList.Remove(this);
				this.mList = null;
			}
		}

		public int InsertInList(List<Ball> theList, int theInsertItr, CurveMgr cm)
		{
			this.mList = theList;
			theList.Insert(theInsertItr, this);
			this.mCurve = cm;
			return theInsertItr;
		}

		public SexyVector3 GetSpeed()
		{
			return new SexyVector3(this.mX - this.mLastX, this.mY - this.mLastY, 0f);
		}

		public float GetWayPointProgress()
		{
			return this.mWayPoint - this.mLastWayPoint;
		}

		public Ball GetPrevBall(bool mustCollide)
		{
			if (this.mList == null)
			{
				return null;
			}
			int listItr = this.GetListItr();
			if (listItr == 0)
			{
				return null;
			}
			if (!mustCollide)
			{
				return this.mList[listItr - 1];
			}
			Ball ball = this.mList[listItr - 1];
			if (ball.GetCollidesWithNext())
			{
				return ball;
			}
			return null;
		}

		public Ball GetPrevBall()
		{
			return this.GetPrevBall(false);
		}

		public Ball GetNextBall(bool mustCollide)
		{
			if (this.mList == null)
			{
				return null;
			}
			int num = this.GetListItr();
			num++;
			if (num >= Enumerable.Count<Ball>(this.mList))
			{
				return null;
			}
			if (!mustCollide || this.GetCollidesWithNext())
			{
				return this.mList[num];
			}
			return null;
		}

		public Ball GetNextBall()
		{
			return this.GetNextBall(false);
		}

		public CurveMgr GetCurve()
		{
			return this.mCurve;
		}

		public void Explode(bool in_tunnel, bool from_lightning_frog)
		{
			if (this.mExploding)
			{
				return;
			}
			this.mExploding = true;
			this.mExplodingInTunnel = in_tunnel;
			Board board = GameApp.gApp.GetBoard();
			if (!this.mExplodingInTunnel)
			{
				board.AddBallExplosionParticleEffect(this);
			}
			if (this.GetPowerOrDestType() == PowerType.PowerType_ProximityBomb)
			{
				PowerEffect powerEffect = new PowerEffect(this.mX, this.mY);
				powerEffect.AddDefaultEffectType(0, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect);
				board.AddProxBombExplosion(this.GetX(), this.GetY());
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_Accuracy)
			{
				PowerEffect powerEffect2 = new PowerEffect(this.mX, this.mY);
				powerEffect2.AddDefaultEffectType(1, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect2);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_MoveBackwards)
			{
				PowerEffect powerEffect3 = new ReversePowerEffect(this.mX, this.mY, this);
				powerEffect3.AddDefaultEffectType(2, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect3);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_SlowDown)
			{
				PowerEffect powerEffect4 = new PowerEffect(this.mX, this.mY);
				powerEffect4.AddDefaultEffectType(3, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect4);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_Cannon)
			{
				PowerEffect powerEffect5 = new CannonPowerEffect(this);
				powerEffect5.AddDefaultEffectType(4, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect5);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_Laser)
			{
				PowerEffect powerEffect6 = new PowerEffect(this.mX, this.mY);
				powerEffect6.AddDefaultEffectType(5, this.mColorType, this.mRotation);
				board.AddPowerEffect(powerEffect6);
			}
			else if (this.GetPowerOrDestType() == PowerType.PowerType_GauntletMultBall)
			{
				this.CleanUpMultiplierOverlays();
			}
			if (this.GetPowerOrDestType() != PowerType.PowerType_Max)
			{
				this.mCurve.SetColorHasPowerup(this.mColorType, false);
			}
			if (from_lightning_frog)
			{
				this.mExplodingFromLightning = true;
				this.mElectricOverlay.Clear();
				this.mElectricOverlay.Add(new Component(255f, 0f, this.mUpdateCount, this.mUpdateCount + 10));
				this.mElectricExplodeOverlay.mLayer1Alpha.Add(new Component(0f, 0f, this.mUpdateCount, this.mUpdateCount + 20));
				this.mElectricExplodeOverlay.mLayer1Alpha.Add(new Component(25f, 255f, this.mUpdateCount + 21, this.mUpdateCount + 41));
				this.mElectricExplodeOverlay.mLayer1Scale.Add(new Component(0.5f, 1f, this.mUpdateCount + 21, this.mUpdateCount + 41));
				this.mElectricExplodeOverlay.mLayer2Alpha.Add(new Component(25f, 255f, this.mUpdateCount, this.mUpdateCount + 20));
				this.mElectricExplodeOverlay.mLoopCount = 0;
				return;
			}
			if (this.mElectricOverlay.size<Component>() > 0)
			{
				this.mElectricOverlay.Clear();
				this.mElectricOverlay.Add(new Component(255f, 0f, this.mUpdateCount, this.mUpdateCount + 5));
			}
		}

		public void Explode(bool in_tunnel)
		{
			this.Explode(in_tunnel, false);
		}

		public void Explode()
		{
			this.Explode(false, false);
		}

		public void Update()
		{
			this.mUpdateCount++;
			this.mLastWayPoint = this.mWayPoint;
			this.mLastX = this.mX;
			this.mLastY = this.mY;
			GameApp gApp = GameApp.gApp;
			if (gApp.GetBoard().GetHallucinateTimer() > 0 && this.mUpdateCount % ZumasRevenge.Common._M(25) == 0)
			{
				this.mDisplayType = MathUtils.SafeRand() % 6;
			}
			if (this.mDoBossPulse && this.mBossBlinkTimer == 0)
			{
				this.mBossBlinkTimer = ZumasRevenge.Common._M(20);
			}
			else if (this.mBossBlinkTimer > 0)
			{
				this.mBossBlinkTimer--;
			}
			if (this.mUpdateCount % ZumasRevenge.Common._M(6) == 0 && (!gApp.mColorblind || (this.mColorType != 3 && this.mColorType != 4)))
			{
				Image imageByID = Res.GetImageByID(ResID.IMAGE_MULTIPLIER_BALL_BLUE);
				this.mMultBallCel = (this.mMultBallCel + 1) % (imageByID.mNumRows * imageByID.mNumCols);
				this.mMultBallCel2 = (this.mMultBallCel2 + 1) % (imageByID.mNumRows * imageByID.mNumCols);
			}
			if (this.mPowerFade > 0)
			{
				this.mIconAppearScale -= this.mIconScaleRate;
				if (this.mIconAppearScale < 1f)
				{
					this.mIconAppearScale = 1f;
				}
				if (this.mPowerType == PowerType.PowerType_GauntletMultBall && this.mDestPowerType == PowerType.PowerType_Max && this.mPowerFade < 51)
				{
					this.mMultOverlayAlpha -= 5;
					if (this.mMultOverlayAlpha < 0)
					{
						this.mMultOverlayAlpha = 0;
					}
				}
				this.mPowerFade--;
				if (this.mPowerFade == 0)
				{
					this.mPowerType = this.mDestPowerType;
					if (this.mPowerType == PowerType.PowerType_GauntletMultBall)
					{
						int num = this.mColorType;
						if (gApp.mColorblind && (this.mColorType == 3 || this.mColorType == 4))
						{
							num = 5;
						}
						this.mMultFX = gApp.mResourceManager.GetPIEffect(Ball.fx_files[num]).Duplicate();
						this.mMultFX.mEmitAfterTimeline = true;
						this.mMultOverlayAlpha = 0;
					}
					else if (this.mPowerType == PowerType.PowerType_Max)
					{
						this.CleanUpMultiplierOverlays();
					}
					this.mIconCel = -1;
					this.mDestPowerType = PowerType.PowerType_Max;
					if (this.mPowerType != PowerType.PowerType_Max && this.mPowerCount <= 0)
					{
						this.mPowerCount = (int)((float)ZumasRevenge.Common._M(2000) * GameApp.gDDS.mHandheldBalance.mFruitPowerupAdditionalDuration);
					}
				}
			}
			if (this.mMultFX != null)
			{
				this.mMultFX.mDrawTransform.LoadIdentity();
				float num2 = GameApp.DownScaleNum(1f);
				this.mMultFX.mDrawTransform.Scale(num2, num2);
				this.mMultFX.mDrawTransform.RotateRad(this.mRotation);
				this.mMultFX.mDrawTransform.Translate(ZumasRevenge.Common._S(this.mX), ZumasRevenge.Common._S(this.mY));
				this.mMultFX.mColor.mAlpha = this.mMultOverlayAlpha;
				this.mMultFX.Update();
			}
			if (this.mMultFX != null && (this.mDestPowerType != PowerType.PowerType_Max || this.mPowerFade >= 51 || this.mPowerFade == 0))
			{
				int num3 = ZumasRevenge.Common._M(3);
				if (this.mInTunnel && this.mMultOverlayAlpha > 0)
				{
					this.mMultOverlayAlpha -= num3;
				}
				else if (!this.mInTunnel && this.mMultOverlayAlpha < 255)
				{
					this.mMultOverlayAlpha += num3;
				}
			}
			this.mMultOverlayAlpha = Math.Min(Math.Max(this.mMultOverlayAlpha, 0), 255);
			if (this.mDoLaserAnim && this.mUpdateCount % ZumasRevenge.Common._M(4) == 0)
			{
				Ball.mLaserAnimCel = (Ball.mLaserAnimCel + 1) % Res.GetImageByID(ResID.IMAGE_LAZER_BURN).mNumCols;
			}
			if (this.mPowerCount > 0 && !this.mExploding && --this.mPowerCount <= 0)
			{
				this.mPowerGracePeriod = ZumasRevenge.Common._M(150);
				this.mLastPowerType = this.GetPowerOrDestType();
				this.mCurve.PowerupExpired(this.GetPowerOrDestType());
				this.mCurve.SetColorHasPowerup(this.mColorType, false);
				this.SetPowerType(PowerType.PowerType_Max);
			}
			if (this.mPowerGracePeriod > 0 && --this.mPowerGracePeriod == 0)
			{
				this.mLastPowerType = PowerType.PowerType_Max;
			}
			if (this.mElectricOverlay.size<Component>() > 0 && Component.UpdateComponentVec(this.mElectricOverlay, this.mUpdateCount) && MathUtils._eq(Component.GetComponentValue(this.mElectricOverlay, 0f, this.mUpdateCount), 0f, 0.0001f))
			{
				this.mElectricOverlay.Clear();
			}
			if (this.mElectricExplodeOverlay.mLayer1Alpha.size<Component>() > 0)
			{
				int num4 = this.mUpdateCount % ZumasRevenge.Common._M(7);
			}
			if (this.mExploding && this.mElectricExplodeOverlay.mLayer1Alpha.size<Component>() > 0)
			{
				Component.UpdateComponentVec(this.mElectricExplodeOverlay.mLayer2Alpha, this.mUpdateCount);
				Component.UpdateComponentVec(this.mElectricExplodeOverlay.mLayer1Scale, this.mUpdateCount);
				if (Component.UpdateComponentVec(this.mElectricExplodeOverlay.mLayer1Alpha, this.mUpdateCount))
				{
					if (++this.mElectricExplodeOverlay.mLoopCount == 1)
					{
						this.mElectricExplodeOverlay.mLayer1Alpha.Clear();
						this.mElectricExplodeOverlay.mLayer1Alpha.Add(new Component(255f, 255f, this.mUpdateCount, this.mUpdateCount + 30));
					}
					else if (this.mElectricExplodeOverlay.mLoopCount == 2)
					{
						this.mElectricExplodeOverlay.mLayer1Alpha.Clear();
						this.mElectricExplodeOverlay.mLayer1Alpha.Add(new Component(255f, 0f, this.mUpdateCount, this.mUpdateCount + 20));
						this.mElectricExplodeOverlay.mLayer2Alpha.Clear();
						this.mElectricExplodeOverlay.mLayer2Alpha.Add(new Component(255f, 0f, this.mUpdateCount, this.mUpdateCount + 20));
						this.mElectricExplodeOverlay.mLayer1Scale.Clear();
						this.mElectricExplodeOverlay.mLayer1Scale.Add(new Component(1f, 1f, this.mUpdateCount, this.mUpdateCount + 4));
						this.mElectricExplodeOverlay.mLayer1Scale.Add(new Component(1f, 0.2f, this.mUpdateCount + 5, this.mUpdateCount + 20));
					}
					else if (this.mElectricExplodeOverlay.mLoopCount == 3)
					{
						this.mElectricExplodeOverlay.mLayer1Scale.Clear();
						this.mElectricExplodeOverlay.mLayer1Alpha.Clear();
						this.mElectricExplodeOverlay.mLayer2Alpha.Clear();
					}
				}
			}
			if (this.mPowerType != PowerType.PowerType_Max)
			{
				if (Component.UpdateComponentVec(this.mOverlayPulse, this.mUpdateCount))
				{
					if (this.mElectricOverlay.size<Component>() == 0)
					{
						this.SetupDefaultOverlayPulse();
					}
					else
					{
						this.SetupElectricOverlayPulse();
					}
				}
				if (!this.mExploding)
				{
					this.mPulseTimer++;
					if (this.mPulseState == 0 && this.mPulseTimer >= ZumasRevenge.Common._M(30))
					{
						this.mPulseState++;
						this.mPulseTimer = 0;
					}
					else if (this.mPulseState == 1 && this.mPulseTimer >= 128)
					{
						this.mPulseTimer = 0;
						this.mPulseState++;
					}
					else if (this.mPulseState == 2 && this.mPulseTimer >= ZumasRevenge.Common._M(25))
					{
						this.mPulseState = 0;
						this.mPulseTimer = 0;
					}
				}
			}
			else if (this.mElectricOverlay.size<Component>() > 0 && Component.UpdateComponentVec(this.mOverlayPulse, this.mUpdateCount))
			{
				this.SetupElectricOverlayPulse();
			}
			else if (this.mElectricOverlay.size<Component>() == 0 && this.mOverlayPulse.size<Component>() > 0 && Component.UpdateComponentVec(this.mOverlayPulse, this.mUpdateCount))
			{
				this.mOverlayPulse.Clear();
			}
			this.UpdateRotation();
			if (this.mPowerType == PowerType.PowerType_MoveBackwards && this.mUpdateCount % ZumasRevenge.Common._M(4) == 0)
			{
				this.mCel = ((this.mCel == 0) ? (Res.GetImageByID(ResID.IMAGE_POWERUP_REVERSE_ANYCOLOR).mNumCols - 1) : (this.mCel - 1));
				return;
			}
			if (this.mPowerType == PowerType.PowerType_Laser && this.mUpdateCount % ZumasRevenge.Common._M(4) == 0)
			{
				this.mCel = (this.mCel + 1) % Res.GetImageByID(ResID.IMAGE_POWERUP_LAZER_ANYCOLOR).mNumRows;
			}
		}

		public void UpdateExplosion()
		{
			if (!this.mExploding)
			{
				return;
			}
			if (!this.mExplodingFromLightning && this.mUpdateCount % ZumasRevenge.Common._M(2) == 0)
			{
				this.mExplodeFrame++;
			}
			if (this.mExplodeFrame >= 20 || this.mElectricExplodeOverlay.mLoopCount >= 3)
			{
				this.mShouldRemove = true;
			}
		}

		public void SetFrame(int theFrame)
		{
			ResID id = ResID.IMAGE_BLUE_BALL + this.mColorType;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			int mNumRows = imageByID.mNumRows;
			int num = (int)this.mWayPoint + theFrame;
			num %= mNumRows;
			this.mStartFrame = mNumRows - num;
		}

		public void ForceFrame(int theFrame)
		{
			this.mStartFrame = theFrame;
		}

		public void IncFrame(int theInc)
		{
			ResID id = ResID.IMAGE_BLUE_BALL + this.mColorType;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			int mNumRows = imageByID.mNumRows;
			this.mStartFrame += theInc;
			this.mStartFrame %= mNumRows;
			if (this.mStartFrame < 0)
			{
				this.mStartFrame = mNumRows + this.mStartFrame;
			}
		}

		public void RandomizeFrame()
		{
			ResID id = ResID.IMAGE_BLUE_BALL + this.mColorType;
			if (GameApp.gApp.mColorblind && this.mColorType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
			}
			else if (GameApp.gApp.mColorblind && this.mColorType == 4)
			{
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			Image imageByID = Res.GetImageByID(id);
			this.mStartFrame = MathUtils.SafeRand() % imageByID.mNumRows;
		}

		public static void DeleteBallGlobals()
		{
			for (int i = 0; i < 8; i++)
			{
				Ball.gBlendedBalls[i] = null;
				if (i < 6)
				{
					Ball.gBlendedBombLights[i] = null;
				}
				for (int j = 0; j <= 14; j++)
				{
					Ball.gBlendedPowerups[j, i] = null;
				}
			}
			for (int j = 0; j < 14; j++)
			{
				Ball.gBlendedPowerupLights[j] = null;
			}
		}

		public virtual void SyncState(DataSync sync)
		{
			sync.RegisterPointer(this);
			sync.SyncLong(ref this.mId);
			sync.SyncLong(ref this.mPowerGracePeriod);
			int num = (int)this.mLastPowerType;
			sync.SyncLong(ref num);
			this.mLastPowerType = (PowerType)num;
			sync.SyncLong(ref this.mColorType);
			sync.SyncFloat(ref this.mWayPoint);
			sync.SyncFloat(ref this.mRotation);
			sync.SyncFloat(ref this.mDestRotation);
			sync.SyncFloat(ref this.mRotationInc);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncBoolean(ref this.mInTunnel);
			sync.SyncLong(ref this.mMultOverlayAlpha);
			Buffer buffer = sync.GetBuffer();
			if (sync.isWrite())
			{
				buffer.WriteBoolean(this.mMultFX != null);
				if (this.mMultFX != null)
				{
					ZumasRevenge.Common.SerializePIEffect(this.mMultFX, sync);
				}
			}
			else
			{
				this.mMultFX = null;
				if (buffer.ReadBoolean())
				{
					this.mMultFX = new PIEffect();
					ZumasRevenge.Common.DeserializePIEffect(this.mMultFX, sync);
				}
			}
			sync.SyncBoolean(ref this.mDoBossPulse);
			sync.SyncFloat(ref this.mRadius);
			sync.SyncLong(ref this.mPulseState);
			sync.SyncLong(ref this.mPulseTimer);
			sync.SyncLong(ref this.mCannonFrame);
			sync.SyncBoolean(ref this.mCollidesWithNext);
			sync.SyncBoolean(ref this.mNeedCheckCollision);
			sync.SyncBoolean(ref this.mSuckPending);
			sync.SyncBoolean(ref this.mShrinkClear);
			sync.SyncBoolean(ref this.mSuckFromCompacting);
			sync.SyncBoolean(ref this.mExplodingInTunnel);
			sync.SyncBoolean(ref this.mExploding);
			sync.SyncLong(ref this.mExplodeFrame);
			sync.SyncBoolean(ref this.mShouldRemove);
			sync.SyncBoolean(ref this.mIsCannon);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mSuckCount);
			sync.SyncBoolean(ref this.mSuckBack);
			sync.SyncLong(ref this.mBackwardsCount);
			sync.SyncFloat(ref this.mBackwardsSpeed);
			sync.SyncLong(ref this.mComboCount);
			sync.SyncLong(ref this.mComboScore);
			sync.SyncLong(ref this.mStartFrame);
			sync.SyncLong(ref this.mPowerCount);
			sync.SyncLong(ref this.mPowerFade);
			sync.SyncBoolean(ref this.mSpeedy);
			sync.SyncLong(ref this.mGapBonus);
			sync.SyncLong(ref this.mNumGaps);
			sync.SyncLong(ref this.mElectricOverlayCel);
			sync.SyncBoolean(ref this.mExplodingFromLightning);
			sync.SyncLong(ref this.mElectricExplodeOverlay.mLoopCount);
			sync.SyncLong(ref this.mElectricExplodeOverlay.mLayer2Cel);
			sync.SyncLong(ref this.mElectricExplodeOverlay.mLayer1Cel);
			this.SyncListComponents(sync, this.mOverlayPulse, true);
			this.SyncListComponents(sync, this.mElectricOverlay, true);
			this.SyncListComponents(sync, this.mElectricExplodeOverlay.mLayer1Alpha, true);
			this.SyncListComponents(sync, this.mElectricExplodeOverlay.mLayer2Alpha, true);
			this.SyncListComponents(sync, this.mElectricExplodeOverlay.mLayer1Scale, true);
			num = (int)this.mPowerType;
			sync.SyncLong(ref num);
			this.mPowerType = (PowerType)num;
			num = (int)this.mDestPowerType;
			sync.SyncLong(ref num);
			this.mDestPowerType = (PowerType)num;
			sync.SyncPointer(this);
		}

		private void SyncListComponents(DataSync sync, List<Component> theList, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					theList.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					Component component = new Component();
					component.SyncState(sync);
					theList.Add(component);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (Component component2 in theList)
			{
				component2.SyncState(sync);
			}
		}

		public void SetColorType(int theType)
		{
			this.mColorType = theType;
		}

		public void SetCollidesWithNext(bool collidesWithNext)
		{
			this.mCollidesWithNext = collidesWithNext;
		}

		public void DoLaserAnim(bool d, Gun g)
		{
			this.mDoLaserAnim = d;
			this.mFrog = g;
		}

		public void DoLaserAnim(bool d)
		{
			this.DoLaserAnim(d, null);
		}

		public void SetShrinkClear(bool shrink)
		{
			this.mShrinkClear = shrink;
		}

		public void SetSuckCount(int theCount, bool suck_back)
		{
			this.mSuckCount = theCount;
			this.mSuckBack = suck_back;
		}

		public void SetSuckCount(int theCount)
		{
			this.SetSuckCount(theCount, true);
		}

		public void SetComboCount(int theCount, int theScore)
		{
			this.mComboCount = theCount;
			this.mComboScore = theScore;
		}

		public void SetBackwardsCount(int theCount)
		{
			this.mBackwardsCount = theCount;
		}

		public void SetBackwardsSpeed(float theSpeed)
		{
			this.mBackwardsSpeed = theSpeed;
		}

		public void SetNeedCheckCollision(bool needCheck)
		{
			this.mNeedCheckCollision = needCheck;
		}

		public void SetSuckPending(bool pending, bool compact)
		{
			this.mSuckPending = pending;
			this.mSuckFromCompacting = compact;
		}

		public void SetSuckPending(bool pending)
		{
			this.SetSuckPending(pending, false);
		}

		public void SetGapBonus(int theBonus, int theNumGaps)
		{
			this.mGapBonus = (ushort)theBonus;
			this.mNumGaps = (ushort)theNumGaps;
		}

		public void SetRadius(float r)
		{
			this.mRadius = r;
		}

		public void SetIsCannon(bool isCannon)
		{
			this.mIsCannon = isCannon;
			this.mCannonFrame = 0;
		}

		public void SetSpeedy(bool speedy)
		{
			this.mSpeedy = speedy;
		}

		public void SetPowerCount(int c)
		{
			this.mPowerCount = c;
		}

		public bool GetSuckBack()
		{
			return this.mSuckBack;
		}

		public bool GetSpeedy()
		{
			return this.mSpeedy;
		}

		public bool Contains(int x, int y)
		{
			x -= (int)this.mX;
			y -= (int)this.mY;
			int num = this.GetRadius() - 3;
			return x * x + y * y < num * num;
		}

		public bool GetShouldRemove()
		{
			return this.mShouldRemove;
		}

		public bool GetIsExploding()
		{
			return this.mExploding;
		}

		public bool GetIsCannon()
		{
			return this.mIsCannon;
		}

		public static int GetIdGen()
		{
			return Ball.mIdGen;
		}

		public float GetX()
		{
			return this.mX;
		}

		public float GetY()
		{
			return this.mY;
		}

		public float GetWayPoint()
		{
			return this.mWayPoint;
		}

		public int GetColorType()
		{
			return this.mColorType;
		}

		public float GetRotation()
		{
			return this.mRotation;
		}

		public float GetDestRotation()
		{
			return this.mDestRotation;
		}

		public Bullet GetBullet()
		{
			return this.mBullet;
		}

		public bool GetCollidesWithNext()
		{
			return this.mCollidesWithNext;
		}

		public bool GetShrinkClear()
		{
			return this.mShrinkClear;
		}

		public bool HasOverlays()
		{
			return this.mPowerType != PowerType.PowerType_Max || Enumerable.Count<Component>(this.mElectricOverlay) > 0 || Enumerable.Count<Component>(this.mElectricExplodeOverlay.mLayer1Alpha) > 0 || this.mMultFX != null;
		}

		public bool HasUnderlays()
		{
			return (this.mDoLaserAnim && !this.mExploding) || this.mMultFX != null;
		}

		public int GetSuckCount()
		{
			return this.mSuckCount;
		}

		public int GetComboCount()
		{
			return this.mComboCount;
		}

		public int GetComboScore()
		{
			return this.mComboScore;
		}

		public int GetBackwardsCount()
		{
			return this.mBackwardsCount;
		}

		public float GetBackwardsSpeed()
		{
			return this.mBackwardsSpeed;
		}

		public bool GetNeedCheckCollision()
		{
			return this.mNeedCheckCollision;
		}

		public bool GetSuckPending()
		{
			return this.mSuckPending;
		}

		public bool GetSuckFromCompacting()
		{
			return this.mSuckFromCompacting;
		}

		public PowerType GetPowerType()
		{
			return this.mPowerType;
		}

		public PowerType GetDestPowerType()
		{
			return this.mDestPowerType;
		}

		public int GetListItr()
		{
			if (this.mList == null)
			{
				return -1;
			}
			return this.mList.IndexOf(this);
		}

		public int GetPowerCount()
		{
			return this.mPowerCount;
		}

		public int GetGapBonus()
		{
			return (int)this.mGapBonus;
		}

		public int GetNumGaps()
		{
			return (int)this.mNumGaps;
		}

		public int GetStartFrame()
		{
			return this.mStartFrame;
		}

		public int GetId()
		{
			return this.mId;
		}

		public SexyVector2 GetPos()
		{
			return new SexyVector2(this.mX, this.mY);
		}

		public int GetRadius()
		{
			return (int)this.mRadius;
		}

		public bool GetInTunnel()
		{
			return this.mInTunnel;
		}

		private static BlendedImage CreateBlendedPowerup(int thePowerupType, int theType, Image theImage, int cel)
		{
			int num = theType;
			if (GameApp.gApp.mColorblind && theType == 3)
			{
				num = 6;
			}
			else if (GameApp.gApp.mColorblind && theType == 4)
			{
				num = 7;
			}
			if (Ball.gBlendedPowerups[thePowerupType, num] == null)
			{
				Rect celRect = theImage.GetCelRect(cel);
				Ball.gBlendedPowerups[thePowerupType, num] = new BlendedImage((MemoryImage)theImage, celRect, false);
			}
			return Ball.gBlendedPowerups[thePowerupType, num];
		}

		private static BlendedImage CreateBlendedBall(int theType)
		{
			ResID id = ResID.IMAGE_BLUE_BALL + theType;
			int num = theType;
			if (GameApp.gApp.mColorblind && theType == 3)
			{
				id = ResID.IMAGE_GREEN_BALL_CBM;
				num = 6;
			}
			else if (GameApp.gApp.mColorblind && theType == 4)
			{
				num = 7;
				id = ResID.IMAGE_PURPLE_BALL_CBM;
			}
			if (Ball.gBlendedBalls[num] == null)
			{
				MemoryImage memoryImage = (MemoryImage)Res.GetImageByID(id);
				int num2 = memoryImage.mWidth / memoryImage.mNumCols;
				int num3 = memoryImage.mHeight / memoryImage.mNumRows;
				int theCel = memoryImage.mNumRows / 2;
				Rect celRect = memoryImage.GetCelRect(theCel);
				Ball.gBlendedBalls[num] = new BlendedImage(memoryImage, celRect, false);
			}
			return Ball.gBlendedBalls[num];
		}

		private static int GetMultAlpha(int cel)
		{
			int num = ZumasRevenge.Common._M(255);
			int num2 = ZumasRevenge.Common._M(5);
			int num3 = num;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_MULTIPLIER_BALL_BLUE);
			int num4 = imageByID.mNumRows * imageByID.mNumCols - num2;
			if (cel < num2)
			{
				num3 = num / num2 * cel;
			}
			else if (cel > num4)
			{
				num3 = num - num / num2 * (cel - num4);
			}
			if (num3 > num)
			{
				num3 = num;
			}
			else if (num3 < 0)
			{
				num3 = 0;
			}
			return num3;
		}

		protected static int mIdGen = 0;

		protected static int mLaserAnimCel;

		protected Transform mGlobalTransform = new Transform();

		public static Color[] gOverlayColors = new Color[]
		{
			new Color(0, 0, 255),
			new Color(255, 255, 0),
			new Color(255, 0, 0),
			new Color(0, 255, 0),
			new Color(255, 0, 255),
			new Color(255, 255, 255)
		};

		public static string[] fx_files = new string[] { "PIEFFECT_NONRESIZE_BPI", "PIEFFECT_NONRESIZE_YPI", "PIEFFECT_NONRESIZE_RPI", "PIEFFECT_NONRESIZE_GPI", "PIEFFECT_NONRESIZE_PPI", "PIEFFECT_NONRESIZE_WPI" };

		private static BlendedImage[] gBlendedBalls = new BlendedImage[8];

		private static BlendedImage[,] gBlendedPowerups = new BlendedImage[15, 8];

		private static BlendedImage[] gBlendedPowerupLights = new BlendedImage[14];

		private static BlendedImage[] gBlendedBombLights = new BlendedImage[6];

		protected bool mInTunnel;

		protected int mMultOverlayAlpha;

		protected PIEffect mMultFX;

		protected int mId;

		protected int mColorType;

		protected int mDisplayType;

		protected float mWayPoint;

		protected float mLastWayPoint;

		protected float mRotation;

		protected float mDestRotation;

		protected float mRotationInc;

		protected float mX;

		protected float mY;

		protected float mLastX;

		protected float mLastY;

		protected float mDrawScale;

		protected float mRadius;

		protected int mPulseState;

		protected int mPulseTimer;

		private List<Component> mOverlayPulse = new List<Component>();

		private List<Component> mElectricOverlay = new List<Component>();

		private ElectricExplodeOverlay mElectricExplodeOverlay = new ElectricExplodeOverlay();

		protected int mElectricOverlayCel;

		protected List<Ball> mList;

		protected CurveMgr mCurve;

		protected bool mCollidesWithNext;

		protected bool mNeedCheckCollision;

		protected bool mSuckPending;

		protected bool mShrinkClear;

		protected bool mSuckFromCompacting;

		protected bool mExplodingInTunnel;

		protected bool mExploding;

		protected bool mExplodingFromLightning;

		protected int mExplodeFrame;

		protected bool mShouldRemove;

		protected bool mSpeedy;

		protected bool mSuckBack;

		protected int mPowerGracePeriod;

		protected PowerType mLastPowerType;

		protected int mCannonFrame;

		protected bool mIsCannon;

		protected bool mDoLaserAnim;

		protected int mUpdateCount;

		protected int mCel;

		public Bullet mBullet;

		protected int mSuckCount;

		protected int mBackwardsCount;

		protected float mBackwardsSpeed;

		protected int mComboCount;

		protected int mComboScore;

		protected int mStartFrame;

		protected int mPowerCount;

		protected int mPowerFade;

		protected ushort mGapBonus;

		protected ushort mNumGaps;

		protected float mIconAppearScale;

		protected float mIconScaleRate;

		protected int mIconCel;

		protected int mMultBallCel;

		protected int mMultBallCel2;

		protected List<Ball.Particle> mParticles;

		protected PowerType mPowerType;

		protected PowerType mDestPowerType;

		public bool mHilightPulse;

		public bool mDebugDrawID;

		public bool mDoBossPulse;

		public int mBossBlinkTimer;

		public int mLastFrame;

		public Gun mFrog;

		protected struct Particle
		{
			public float x;

			public float y;

			public float vx;

			public float vy;

			public int mSize;
		}
	}
}
