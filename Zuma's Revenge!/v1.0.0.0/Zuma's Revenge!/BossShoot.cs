using System;
using System.Collections.Generic;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class BossShoot : Boss, IDisposable
	{
		public int mColorVampChanceToMatch2ndBall
		{
			get
			{
				return this.mDColorVampChanceToMatch2ndBall.value;
			}
			set
			{
				this.mDColorVampChanceToMatch2ndBall.value = value;
			}
		}

		public int mFrogStunTime
		{
			get
			{
				return this.mDFrogStunTime.value;
			}
			set
			{
				this.mDFrogStunTime.value = value;
			}
		}

		public int mFrogPoisonTime
		{
			get
			{
				return this.mDFrogPoisonTime.value;
			}
			set
			{
				this.mDFrogPoisonTime.value = value;
			}
		}

		public int mFrogHallucinateTime
		{
			get
			{
				return this.mDFrogHallucinateTime.value;
			}
			set
			{
				this.mDFrogHallucinateTime.value = value;
			}
		}

		public int mFrogSlowTimer
		{
			get
			{
				return this.mDFrogSlowTimer.value;
			}
			set
			{
				this.mDFrogSlowTimer.value = value;
			}
		}

		public int mShotDelay
		{
			get
			{
				return this.mDShotDelay.value;
			}
			set
			{
				this.mDShotDelay.value = value;
			}
		}

		public float mFlightSpeed
		{
			get
			{
				return this.mDFlightSpeed.value;
			}
			set
			{
				this.mDFlightSpeed.value = value;
			}
		}

		public int mFlightMinDist
		{
			get
			{
				return this.mDFlightMinDist.value;
			}
			set
			{
				this.mDFlightMinDist.value = value;
			}
		}

		public int mColorVampHealthInc
		{
			get
			{
				return this.mDColorVampHealthInc.value;
			}
			set
			{
				this.mDColorVampHealthInc.value = value;
			}
		}

		public int mMinColorChangeTime
		{
			get
			{
				return this.mDMinColorChangeTime.value;
			}
			set
			{
				this.mDMinColorChangeTime.value = value;
			}
		}

		public int mMaxColorChangeTime
		{
			get
			{
				return this.mDMaxColorChangeTime.value;
			}
			set
			{
				this.mDMaxColorChangeTime.value = value;
			}
		}

		public float mHomingCorrectionAmt
		{
			get
			{
				return this.mDHomingCorrectionAmt.value;
			}
			set
			{
				this.mDHomingCorrectionAmt.value = value;
			}
		}

		public int mMinHoverTime
		{
			get
			{
				return this.mDMinHoverTime.value;
			}
			set
			{
				this.mDMinHoverTime.value = value;
			}
		}

		public int mMaxHoverTime
		{
			get
			{
				return this.mDMaxHoverTime.value;
			}
			set
			{
				this.mDMaxHoverTime.value = value;
			}
		}

		public int mMinFireDelay
		{
			get
			{
				return this.mDMinFireDelay.value;
			}
			set
			{
				this.mDMinFireDelay.value = value;
			}
		}

		public int mMaxFireDelay
		{
			get
			{
				return this.mDMaxFireDelay.value;
			}
			set
			{
				this.mDMaxFireDelay.value = value;
			}
		}

		public float mMinBulletSpeed
		{
			get
			{
				return this.mDMinBulletSpeed.value;
			}
			set
			{
				this.mDMinBulletSpeed.value = value;
			}
		}

		public float mMaxBulletSpeed
		{
			get
			{
				return this.mDMaxBulletSpeed.value;
			}
			set
			{
				this.mDMaxBulletSpeed.value = value;
			}
		}

		public int mMaxBulletsToFire
		{
			get
			{
				return this.mDMaxBulletsToFire.value;
			}
			set
			{
				this.mDMaxBulletsToFire.value = value;
			}
		}

		public int mMaxRetaliationBullets
		{
			get
			{
				return this.mDMaxRetaliationBullets.value;
			}
			set
			{
				this.mDMaxRetaliationBullets.value = value;
			}
		}

		public int mMinSineShotTime
		{
			get
			{
				return this.mDMinSineShotTime.value;
			}
			set
			{
				this.mDMinSineShotTime.value = value;
			}
		}

		public int mMaxSineShotTime
		{
			get
			{
				return this.mDMaxSineShotTime.value;
			}
			set
			{
				this.mDMaxSineShotTime.value = value;
			}
		}

		public float mMinAmp
		{
			get
			{
				return this.mDMinAmp.value;
			}
			set
			{
				this.mDMinAmp.value = value;
			}
		}

		public float mMaxAmp
		{
			get
			{
				return this.mDMaxAmp.value;
			}
			set
			{
				this.mDMaxAmp.value = value;
			}
		}

		public float mMinFreq
		{
			get
			{
				return this.mDMinFreq.value;
			}
			set
			{
				this.mDMinFreq.value = value;
			}
		}

		public float mMaxFreq
		{
			get
			{
				return this.mDMaxFreq.value;
			}
			set
			{
				this.mDMaxFreq.value = value;
			}
		}

		public float mMaxYInc
		{
			get
			{
				return this.mDMaxYInc.value;
			}
			set
			{
				this.mDMaxYInc.value = value;
			}
		}

		public float mMinYInc
		{
			get
			{
				return this.mDMinYInc.value;
			}
			set
			{
				this.mDMinYInc.value = value;
			}
		}

		public float mMaxXInc
		{
			get
			{
				return this.mDMaxXInc.value;
			}
			set
			{
				this.mDMaxXInc.value = value;
			}
		}

		public float mMinXInc
		{
			get
			{
				return this.mDMinXInc.value;
			}
			set
			{
				this.mDMinXInc.value = value;
			}
		}

		public float mDefaultSpeed
		{
			get
			{
				return this.mDDefaultSpeed.value;
			}
			set
			{
				this.mDDefaultSpeed.value = value;
			}
		}

		public bool mStrafe
		{
			get
			{
				return this.mDStrafe.value;
			}
			set
			{
				this.mDStrafe.value = value;
			}
		}

		public bool mEndHoverOnHit
		{
			get
			{
				return this.mDEndHoverOnHit.value;
			}
			set
			{
				this.mDEndHoverOnHit.value = value;
			}
		}

		public float mMinRetalSpeed
		{
			get
			{
				return this.mDMinRetalSpeed.value;
			}
			set
			{
				this.mDMinRetalSpeed.value = value;
			}
		}

		public float mMaxRetalSpeed
		{
			get
			{
				return this.mDMaxRetalSpeed.value;
			}
			set
			{
				this.mDMaxRetalSpeed.value = value;
			}
		}

		public int mShotType
		{
			get
			{
				return this.mDShotType.value;
			}
			set
			{
				this.mDShotType.value = value;
			}
		}

		public int mTeleportMinTime
		{
			get
			{
				return this.mDTeleportMinTime.value;
			}
			set
			{
				this.mDTeleportMinTime.value = value;
			}
		}

		public int mTeleportMaxTime
		{
			get
			{
				return this.mDTeleportMaxTime.value;
			}
			set
			{
				this.mDTeleportMaxTime.value = value;
			}
		}

		public float mMovementAccel
		{
			get
			{
				return this.mDMovementAccel.value;
			}
			set
			{
				this.mDMovementAccel.value = value;
			}
		}

		public int mDefaultMovementUpdateDelay
		{
			get
			{
				return this.mDDefaultMovementUpdateDelay.value;
			}
			set
			{
				this.mDDefaultMovementUpdateDelay.value = value;
			}
		}

		public int mMovementMode
		{
			get
			{
				return this.mDMovementMode.value;
			}
			set
			{
				this.mDMovementMode.value = value;
			}
		}

		public bool mUseShield
		{
			get
			{
				return this.mDUseShield.value;
			}
			set
			{
				this.mDUseShield.value = value;
			}
		}

		public float mShieldRotateSpeed
		{
			get
			{
				return this.mDShieldRotateSpeed.value;
			}
			set
			{
				this.mDShieldRotateSpeed.value = value;
			}
		}

		public int mShieldQuadRespawnTime
		{
			get
			{
				return this.mDShieldQuadRespawnTime.value;
			}
			set
			{
				this.mDShieldQuadRespawnTime.value = value;
			}
		}

		public int mShieldPauseTime
		{
			get
			{
				return this.mDShieldPauseTime.value;
			}
			set
			{
				this.mDShieldPauseTime.value = value;
			}
		}

		public int mShieldHP
		{
			get
			{
				return this.mDShieldHP.value;
			}
			set
			{
				this.mDShieldHP.value = value;
			}
		}

		public int mBallShieldDamage
		{
			get
			{
				return this.mDBallShieldDamage.value;
			}
			set
			{
				this.mDBallShieldDamage.value = value;
			}
		}

		protected void CalcDestX(int min_dist)
		{
			if (this.mStrafe)
			{
				if (this.mX < (float)this.mEndX)
				{
					this.mDestX = (float)this.mEndX;
				}
				else
				{
					this.mDestX = (float)this.mStartX;
				}
			}
			else
			{
				this.mDestX = (float)this.GetMinXDist(min_dist);
			}
			if (this.mDestX > this.mX)
			{
				this.mSpeed = this.mDefaultSpeed;
				return;
			}
			this.mSpeed = -this.mDefaultSpeed;
		}

		protected void CalcDestX()
		{
			this.CalcDestX(100);
		}

		protected void CalcDestY(int min_dist)
		{
			if (this.mStrafe)
			{
				if (this.mY < (float)this.mEndY)
				{
					this.mDestY = (float)this.mEndY;
				}
				else
				{
					this.mDestY = (float)this.mStartY;
				}
			}
			else
			{
				this.mDestY = (float)this.GetMinYDist(min_dist);
			}
			if (this.mDestY > this.mY)
			{
				this.mSpeed = this.mDefaultSpeed;
				return;
			}
			this.mSpeed = -this.mDefaultSpeed;
		}

		protected void CalcDestY()
		{
			this.CalcDestY(100);
		}

		protected int GetMinXDist(int min_dist)
		{
			int num;
			int num2;
			if (this.mX - (float)min_dist - (float)(this.mWidth / 2) <= (float)this.mStartX)
			{
				num = (int)(this.mX + (float)(this.mWidth / 2) + (float)min_dist);
				num2 = this.mEndX;
			}
			else if (this.mX + (float)min_dist + (float)(this.mWidth / 2) >= (float)this.mEndX)
			{
				num2 = (int)(this.mX - (float)min_dist - (float)(this.mWidth / 2));
				num = this.mStartX;
			}
			else if (SexyFramework.Common.Rand() % 100 < 50)
			{
				num = this.mStartX;
				num2 = (int)(this.mX - (float)min_dist - (float)(this.mWidth / 2));
			}
			else
			{
				num = (int)(this.mX + (float)min_dist + (float)(this.mWidth / 2));
				num2 = this.mEndX;
			}
			if (num + this.mWidth / 2 > this.mEndX)
			{
				num = this.mEndX - 10;
			}
			else if (num - this.mWidth / 2 < this.mStartX)
			{
				num = this.mStartX + 10;
			}
			if (num > num2)
			{
				num = num2;
				num2 = num;
			}
			return SexyFramework.Common.IntRange(num, num2);
		}

		protected int GetMinXDist()
		{
			return this.GetMinXDist(100);
		}

		protected int GetMinYDist(int min_dist)
		{
			int num;
			int num2;
			if (this.mY - (float)min_dist - (float)(this.mHeight / 2) <= (float)this.mStartY)
			{
				num = (int)(this.mY + (float)(this.mHeight / 2) + (float)min_dist);
				num2 = this.mEndY;
			}
			else if (this.mY + (float)min_dist + (float)(this.mHeight / 2) >= (float)this.mEndY)
			{
				num2 = (int)(this.mY - (float)min_dist - (float)(this.mHeight / 2));
				num = this.mStartY;
			}
			else if (SexyFramework.Common.Rand() % 100 < 50)
			{
				num = this.mStartY;
				num2 = (int)(this.mY - (float)min_dist - (float)(this.mHeight / 2));
			}
			else
			{
				num = (int)(this.mY + (float)min_dist + (float)(this.mHeight / 2));
				num2 = this.mEndX;
			}
			if (num > num2)
			{
				num = num2;
				num2 = num;
			}
			return SexyFramework.Common.IntRange(num, num2);
		}

		protected int GetMinYDist()
		{
			return this.GetMinYDist(100);
		}

		protected bool AtDest()
		{
			if (this.mSpeed > 0f)
			{
				return (this.mStartX > 0 && this.mX >= this.mDestX) || (this.mStartY > 0 && this.mY >= this.mDestY);
			}
			return (this.mStartX > 0 && this.mX <= this.mDestX) || (this.mStartY > 0 && this.mY <= this.mDestY);
		}

		protected override bool DoHit(Bullet b, bool from_prox_bomb)
		{
			int num = (int)this.mHP;
			base.DoHit(b, from_prox_bomb);
			num = (int)((float)num - this.mHP);
			if (num <= 0)
			{
				return false;
			}
			this.mMaxShotIncCounter += num;
			if (this.CanRetaliate())
			{
				this.mRetalShotIncCounter += num;
			}
			if (this.mMaxShotIncCounter >= this.mIncMaxShotHealthAmt && this.mIncMaxShotHealthAmt > 0)
			{
				this.mMaxShotIncCounter = 0;
				this.mMaxBulletsToFire++;
			}
			if (this.mRetalShotIncCounter >= this.mIncRetalMaxShotHealthAmt && this.mIncRetalMaxShotHealthAmt > 0)
			{
				this.mRetalShotIncCounter = 0;
				this.mMaxRetaliationBullets++;
			}
			if (this.mColorVampire)
			{
				int num2 = this.mColorVampShotType;
				if (!this.mAvoidColor && this.mColorVampChanceToMatch2ndBall > 0 && SexyFramework.Common.Rand() % 100 <= this.mColorVampChanceToMatch2ndBall)
				{
					if (this.mLevel.mFrog.GetNextBullet() != null)
					{
						num2 = this.mLevel.mFrog.GetNextBullet().GetColorType();
						goto IL_10E;
					}
				}
				while (num2 == this.mColorVampShotType)
				{
					num2 = SexyFramework.Common.Rand() % 4;
				}
				IL_10E:
				this.mColorVampShotType = num2;
				this.mColorChangeTimer = SexyFramework.Common.IntRange(this.mMinColorChangeTime, this.mMaxColorChangeTime);
			}
			if (this.mEndHoverOnHit)
			{
				this.EndHoverOnHit();
			}
			this.mMinHoverTime -= this.mDecMinHover;
			this.mMaxHoverTime -= this.mDecMaxHover;
			this.mMinFireDelay -= this.mDecMinFire;
			this.mMaxFireDelay -= this.mDecMaxFire;
			if (this.mMaxRetaliationBullets > 0 && this.CanRetaliate() && !base.IsStunned())
			{
				int num3 = 0;
				for (int i = 0; i < this.mMaxRetaliationBullets; i++)
				{
					BossBullet bossBullet = new BossBullet();
					this.mBullets.Add(bossBullet);
					bossBullet.mX = this.mX;
					bossBullet.mY = this.mY;
					bossBullet.mId = ++BossShoot.gLastBulletId;
					bossBullet.mDelay = i * this.mRetalShotDelay;
					if (this.mSinusoidalRetaliation)
					{
						if (!this.FireSinusoidalBullet(bossBullet, (this.mMaxRetaliationBullets == 1) ? (SexyFramework.Common.Rand() % 100 < 50) : ((i + 1) % 2 == 0)))
						{
							this.mBullets.RemoveAt(this.mBullets.Count - 1);
						}
						else
						{
							bossBullet.mTargetVX = bossBullet.mVX;
							bossBullet.mTargetVY = bossBullet.mVY;
							num3++;
							this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS1_FIRE));
						}
					}
					else
					{
						num3++;
						this.FireBulletAtPlayer(bossBullet, SexyFramework.Common.FloatRange(this.mMinRetalSpeed, this.mMaxRetalSpeed));
						bossBullet.mTargetVX = bossBullet.mVX;
						bossBullet.mTargetVY = bossBullet.mVY;
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BOSS1_FIRE));
					}
				}
				base.PlaySound(2);
				this.DidRetaliate(num3);
			}
			return true;
		}

		protected void GetTargetedVelocity(float speed, float x, float y, ref float vx, ref float vy)
		{
			float num = SexyFramework.Common.AngleBetweenPoints(x, y, (float)this.mLevel.mFrog.GetCenterX(), (float)this.mLevel.mFrog.GetCenterY());
			vx = (float)Math.Cos((double)num) * speed;
			vy = -(float)Math.Sin((double)num) * speed;
		}

		protected float FireBulletAtPlayer(BossBullet b, float speed, float x, float y)
		{
			float num = SexyFramework.Common.AngleBetweenPoints(x, y, (float)this.mLevel.mFrog.GetCenterX(), (float)this.mLevel.mFrog.GetCenterY());
			b.mVX = (float)Math.Cos((double)num) * speed;
			b.mVY = -(float)Math.Sin((double)num) * speed;
			b.mSineMotion = false;
			b.mGravity = 0f;
			b.mInitialSpeed = speed;
			return num;
		}

		protected float FireBulletAtPlayer(BossBullet b, float speed)
		{
			return this.FireBulletAtPlayer(b, speed, this.mX, this.mY);
		}

		protected bool FireSinusoidalBullet(BossBullet b, bool negative)
		{
			b.mVX = (b.mVY = 0f);
			if (!this.mSineShotsTargetPlayer)
			{
				b.mSineMotion = true;
				b.mAmp = SexyFramework.Common.FloatRange(this.mMinAmp, this.mMaxAmp);
				b.mFreq = SexyFramework.Common.FloatRange(this.mMinFreq, this.mMaxFreq);
				if (negative)
				{
					b.mAmp *= -1f;
				}
				if (this.mStartX > 0)
				{
					b.mVY = SexyFramework.Common.FloatRange(this.mMinYInc, this.mMaxYInc);
				}
				else
				{
					b.mVX = SexyFramework.Common.FloatRange(this.mMinXInc, this.mMaxXInc);
				}
				b.mGravity = 0f;
				return true;
			}
			if (this.mStartY > 0)
			{
				b.mSineMotion = false;
				b.mGravity = ZumasRevenge.Common._M(0.04f);
				if (negative)
				{
					b.mGravity *= -1f;
				}
				int num = SexyFramework.Common.IntRange(this.mMinSineShotTime, this.mMaxSineShotTime);
				b.mVX = ((float)this.mLevel.mFrog.GetCenterX() - this.mX) / (float)num;
				b.mVY = ((float)this.mLevel.mFrog.GetCenterY() - this.mY - 0.5f * b.mGravity * (float)num * (float)num) / (float)num;
				int num2 = (int)Math.Abs(b.mVY / b.mGravity);
				bool flag = true;
				float num3 = b.mVY * (float)num2 + 0.5f * b.mGravity * (float)num2 * (float)num2 + this.mY;
				while (((num3 < (float)(BossShoot.DEFAULT_BULLET_SIZE * 4) && b.mGravity > 0f) || (num3 > (float)(ZumasRevenge.Common._SS(GameApp.gApp.mHeight) - BossShoot.DEFAULT_BULLET_SIZE * 4) && b.mGravity < 0f)) && num > this.mMinSineShotTime)
				{
					if (flag)
					{
						flag = false;
						num = this.mMaxSineShotTime;
					}
					num -= ZumasRevenge.Common._M(5);
					b.mVX = ((float)this.mLevel.mFrog.GetCenterX() - this.mX) / (float)num;
					b.mVY = ((float)this.mLevel.mFrog.GetCenterY() - this.mY - 0.5f * b.mGravity * (float)num * (float)num) / (float)num;
					num2 = (int)Math.Abs(b.mVY / b.mGravity);
					num3 = b.mVY * (float)num2 + 0.5f * b.mGravity * (float)num2 * (float)num2 + this.mY;
				}
				if (num < this.mMinSineShotTime)
				{
					return false;
				}
			}
			else
			{
				b.mSineMotion = false;
				b.mGravity = ZumasRevenge.Common._M(-0.08f);
				if (negative)
				{
					b.mGravity *= -1f;
				}
				int num4 = SexyFramework.Common.IntRange(this.mMinSineShotTime, this.mMaxSineShotTime);
				b.mVX = -((float)this.mLevel.mFrog.GetCenterX() - this.mX + 0.5f * b.mGravity * (float)num4 * (float)num4) / (float)num4;
				b.mVY = ((float)this.mLevel.mFrog.GetCenterY() - this.mY) / (float)num4;
				int num5 = (int)Math.Abs(b.mVX / b.mGravity);
				bool flag2 = true;
				float num6 = b.mVX * (float)num5 + 0.5f * b.mGravity * (float)num5 * (float)num5 + this.mX;
				while (((num6 < (float)(BossShoot.DEFAULT_BULLET_SIZE * 4) && b.mVX < 0f) || (num6 > (float)(ZumasRevenge.Common._SS(GameApp.gApp.mWidth) - BossShoot.DEFAULT_BULLET_SIZE * 4) && b.mVX > 0f)) && num4 > this.mMinSineShotTime)
				{
					if (flag2)
					{
						flag2 = false;
						num4 = this.mMaxSineShotTime;
					}
					num4 -= ZumasRevenge.Common._M(5);
					b.mVX = -((float)this.mLevel.mFrog.GetCenterX() - this.mX + 0.5f * b.mGravity * (float)num4 * (float)num4) / (float)num4;
					b.mVY = ((float)this.mLevel.mFrog.GetCenterY() - this.mY) / (float)num4;
					num5 = (int)Math.Abs(b.mVX / b.mGravity);
					num6 = b.mVX * (float)num5 + 0.5f * b.mGravity * (float)num5 * (float)num5 + this.mX;
				}
				if (num4 <= this.mMinSineShotTime)
				{
					return false;
				}
			}
			return true;
		}

		protected override void ReInit()
		{
			base.ReInit();
			if (this.mColorVampire)
			{
				this.mColorVampShotType = SexyFramework.Common.Rand() % 4;
				this.mColorChangeTimer = SexyFramework.Common.IntRange(this.mMinColorChangeTime, this.mMaxColorChangeTime);
			}
			if (this.mColorVampHealthInc > 0)
			{
				this.mColorVampHealthIncPerHit = (int)((float)(Boss.NUM_HEARTS * 4) / (this.mMaxHP / (float)this.mColorVampHealthInc));
			}
			if (this.mStrafe)
			{
				this.mFireDelay = SexyFramework.Common.IntRange(this.mMinFireDelay, this.mMaxFireDelay);
			}
			this.mSpeed = (float)Math.Sign(this.mSpeed) * this.mDefaultSpeed;
			if (this.mMinRetalSpeed == 0f)
			{
				this.mMinRetalSpeed = this.mMinBulletSpeed;
			}
			if (this.mMaxRetalSpeed == 0f)
			{
				this.mMaxRetalSpeed = this.mMaxBulletSpeed;
			}
			this.mCurShieldPauseTime = this.mShieldPauseTime;
			if (this.mTeleportMinTime != 0 && this.mTeleportMaxTime != 0)
			{
				this.mTeleportTime = SexyFramework.Common.IntRange(this.mTeleportMinTime, this.mTeleportMaxTime);
			}
		}

		protected virtual void DrawBossSpecificArt(Graphics g)
		{
			int default_BULLET_SIZE = BossShoot.DEFAULT_BULLET_SIZE;
			int default_BULLET_SIZE2 = BossShoot.DEFAULT_BULLET_SIZE;
			if (this.mHP > 0f && !this.mDoDeathExplosions)
			{
				for (int i = 0; i < this.mBullets.Count; i++)
				{
					if (this.mBullets[i].mDelay <= 0)
					{
						g.SetColor(Color.White);
						CommonGraphics.DrawCircle(g, ZumasRevenge.Common._S(this.mBullets[i].mX), ZumasRevenge.Common._S(this.mBullets[i].mY), (float)ZumasRevenge.Common._S(ZumasRevenge.Common._M(24)), 30);
					}
				}
			}
		}

		protected override void DrawMisc(Graphics g)
		{
			if (this.mIsBerserk)
			{
				this.DrawBerserk(g);
			}
			if (this.mTeleportDir != 0)
			{
				g.PushState();
				int theX = (int)ZumasRevenge.Common._S(this.mX) - this.mWidth / 2;
				int theY = (int)ZumasRevenge.Common._S(this.mY) - this.mHeight / 2;
				int theWidth = ZumasRevenge.Common._S(this.mWidth);
				int theHeight;
				if (this.mTeleportDir == -1)
				{
					theHeight = (int)((float)ZumasRevenge.Common._S(this.mHeight) - (float)ZumasRevenge.Common._S(this.mHeight) * this.mTeleportPct);
				}
				else
				{
					theHeight = (int)((float)ZumasRevenge.Common._S(this.mHeight) * this.mTeleportPct);
				}
				g.ClipRect(theX, theY, theWidth, theHeight);
			}
			if (this.mUseShield)
			{
				this.DrawShield(g);
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			base.DrawMisc(g);
		}

		protected virtual bool PreBulletUpdate(BossBullet b, int index)
		{
			if (b.mDelay > 0)
			{
				if (--b.mDelay == 0)
				{
					b.mX = this.mX;
					b.mY = this.mY;
					base.PlaySound(2);
				}
				return true;
			}
			if (b.mOffscreenPause > 0 && b.mY < (float)ZumasRevenge.Common._M(-305))
			{
				if (--b.mOffscreenPause == 0)
				{
					b.mVY *= -1f;
					int centerX = this.mLevel.mFrog.GetCenterX();
					b.mX = (float)centerX;
				}
				return true;
			}
			return false;
		}

		protected virtual void BulletErased(int index)
		{
		}

		protected virtual void DidFire()
		{
		}

		protected virtual void DidRetaliate(int num_shot)
		{
		}

		protected virtual Rect GetBulletRect(BossBullet b)
		{
			int num = (int)((float)BossShoot.DEFAULT_BULLET_SIZE * 0.75f);
			int num2 = (int)((float)BossShoot.DEFAULT_BULLET_SIZE * 0.75f);
			return new Rect((int)b.mX - num / 2, (int)b.mY - num2 / 2, num, num2);
		}

		protected virtual Rect GetFrogRect()
		{
			int num = ZumasRevenge.Common._M(1);
			return new Rect(this.mLevel.mFrog.GetCenterX() - num, this.mLevel.mFrog.GetCenterY() - num, num * 2, num * 2);
		}

		protected virtual bool CanFire()
		{
			return true;
		}

		protected virtual void BulletHitPlayer(BossBullet b)
		{
			base.PlaySound(4);
		}

		protected void WarpToPoint(bool play_sound)
		{
			if (this.mPoints.Count == 0)
			{
				return;
			}
			int num;
			do
			{
				num = SexyFramework.Common.Rand() % this.mPoints.Count;
			}
			while (num == this.mCurrentLocPoint && this.mPoints.Count > 1);
			this.mX = (float)this.mPoints[num].mX;
			this.mY = (float)this.mPoints[num].mY;
			this.mCurrentLocPoint = num;
			if (play_sound)
			{
				base.PlaySound(8);
			}
		}

		protected void WarpToPoint()
		{
			this.WarpToPoint(true);
		}

		protected void EndHoverOnHit()
		{
			this.mHoverTime = 0;
			if (this.mTeleportTime != -1)
			{
				this.mTeleportDir = -1;
				this.mTeleportPct = 0f;
				this.mTeleportTime = 0;
				return;
			}
			if (this.mPoints.Count == 0)
			{
				this.mHoverTime = 0;
				this.mXOff = (this.mYOff = 0);
				if (this.mStartX > 0)
				{
					this.CalcDestX(this.mFlightMinDist);
				}
				else
				{
					this.CalcDestY(this.mFlightMinDist);
				}
				if (this.mFlightSpeed > 0f)
				{
					this.mSpeed = this.mFlightSpeed * (float)Math.Sign(this.mSpeed);
					return;
				}
			}
			else if (this.mEndHoverCountdown == 0)
			{
				this.mEndHoverCountdown = ZumasRevenge.Common._M(300);
			}
		}

		protected override void BerserkActivated(int health_limit)
		{
			base.BerserkActivated(health_limit);
			if (this.mEnrageShieldRestore)
			{
				for (int i = 0; i < 4; i++)
				{
					this.mShieldQuadrant[i].mTimer = 0;
					this.mShieldQuadrant[i].mHP = this.mShieldHP;
				}
			}
			this.mEnrageDelayTimer = this.mEnrageDelay;
			int j = 0;
			while (j < this.mBerserkMovementVec.Count)
			{
				BossBerserkMovement bossBerserkMovement = this.mBerserkMovementVec[j];
				if (bossBerserkMovement.mHealthLimit == health_limit)
				{
					this.mStartX = bossBerserkMovement.mStartX;
					this.mStartY = bossBerserkMovement.mStartY;
					this.mEndX = bossBerserkMovement.mEndX;
					this.mEndY = bossBerserkMovement.mEndY;
					bool flag = this.mPoints.Count > 0;
					this.mPoints.Clear();
					if (bossBerserkMovement.mPoints.Count > 0)
					{
						this.mPoints.AddRange(bossBerserkMovement.mPoints.ToArray());
					}
					if (bossBerserkMovement.mX != 2147483647)
					{
						this.SetX((float)bossBerserkMovement.mX);
					}
					if (bossBerserkMovement.mY != 2147483647)
					{
						this.SetY((float)bossBerserkMovement.mY);
					}
					if (this.mPoints.Count != 0)
					{
						if (!flag)
						{
							this.mHoverTime = SexyFramework.Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
							this.WarpToPoint();
						}
						return;
					}
					this.mCurrentLocPoint = -1;
					this.mHoverTime = 0;
					if (this.mStartY <= 0)
					{
						this.CalcDestX();
						return;
					}
					this.CalcDestY();
					return;
				}
				else
				{
					j++;
				}
			}
		}

		protected virtual void DrawBerserk(Graphics g)
		{
			if (this.mHP > 0f && !this.mDoDeathExplosions)
			{
				this.mLevel.mBoard.DoingBossIntro();
			}
		}

		protected virtual void DrawShield(Graphics g)
		{
			if (this.mHP > 0f && !this.mDoDeathExplosions)
			{
				this.mLevel.mBoard.DoingBossIntro();
			}
		}

		protected virtual BossBullet CreateBossBullet()
		{
			return new BossBullet();
		}

		protected virtual void BossBulletDestroyed(BossBullet b, bool outofscreen)
		{
		}

		protected virtual bool CheckBulletHitPlayer(BossBullet b)
		{
			if (this.mMaxShotBounces > 0 && b.mBouncesLeft <= 0)
			{
				return false;
			}
			Rect bulletRect = this.GetBulletRect(b);
			this.mLevel.mFrog.GetWidth();
			this.mLevel.mFrog.GetHeight();
			float y = (float)(this.mLevel.mFrog.GetCenterY() - 5);
			float x = (float)(this.mLevel.mFrog.GetCenterX() + 2);
			return b.mCanHitPlayer && ((this.mBulletsUseSphereColl && MathUtils.CirclesIntersect(x, y, (float)(bulletRect.mX + bulletRect.mWidth / 2), (float)(bulletRect.mY + bulletRect.mHeight / 2), (float)(40 + this.mBulletRadius))) || (!this.mBulletsUseSphereColl && bulletRect.Intersects(this.GetFrogRect())));
		}

		protected virtual void ShieldQuadrantHit(int quad)
		{
		}

		protected virtual bool CanRetaliate()
		{
			return true;
		}

		protected virtual void GetShotBounceOffs(BossBullet b, ref int x, ref int y)
		{
			x = 0;
			y = 0;
		}

		protected virtual void QuadHitByProxBomb(int quad)
		{
		}

		protected virtual void ShotBounced(BossBullet b)
		{
		}

		protected virtual void AppliedSlowTimer()
		{
		}

		protected void CopyFrom(BossShoot rhs)
		{
			base.CopyFrom(rhs);
			this.mHitEffectYOff = rhs.mHitEffectYOff;
			this.mPauseMovement = rhs.mPauseMovement;
			this.mPauseShieldRegen = rhs.mPauseShieldRegen;
			this.mDrawHeartsBelowMisc = rhs.mDrawHeartsBelowMisc;
			this.mDrawHeartsBelowBoss = rhs.mDrawHeartsBelowBoss;
			this.mDestX = rhs.mDestX;
			this.mDestY = rhs.mDestY;
			this.mTargetDestX = rhs.mTargetDestX;
			this.mShieldAngle = rhs.mShieldAngle;
			this.mShieldTargetAngle = rhs.mShieldTargetAngle;
			this.mShieldRadius = rhs.mShieldRadius;
			this.mHoverTime = rhs.mHoverTime;
			this.mFireDelay = rhs.mFireDelay;
			this.mXOff = rhs.mXOff;
			this.mYOff = rhs.mYOff;
			this.mCurShieldPauseTime = rhs.mCurShieldPauseTime;
			this.mEndHoverCountdown = rhs.mEndHoverCountdown;
			this.mEnrageDelayTimer = rhs.mEnrageDelayTimer;
			this.mMovementUpdateDelay = rhs.mMovementUpdateDelay;
			this.mAttackDelayAfterHitFrog = rhs.mAttackDelayAfterHitFrog;
			this.mCurrentLocPoint = rhs.mCurrentLocPoint;
			this.mMaxShotBounces = rhs.mMaxShotBounces;
			this.mShieldPauseTime = rhs.mShieldPauseTime;
			this.mShieldRotateSpeed = rhs.mShieldRotateSpeed;
			this.mUseShield = rhs.mUseShield;
			this.mShieldQuadRespawnTime = rhs.mShieldQuadRespawnTime;
			this.mShieldHP = rhs.mShieldHP;
			this.mBallShieldDamage = rhs.mBallShieldDamage;
			this.mTeleportDir = rhs.mTeleportDir;
			this.mTeleportPct = rhs.mTeleportPct;
			this.mTeleportMinTime = rhs.mTeleportMinTime;
			this.mTeleportMaxTime = rhs.mTeleportMaxTime;
			this.mTeleportTime = rhs.mTeleportTime;
			this.mEnrageDelay = rhs.mEnrageDelay;
			this.mBombAppearDelay = rhs.mBombAppearDelay;
			this.mShotDelay = rhs.mShotDelay;
			this.mBombAppearDelay = rhs.mBombAppearDelay;
			this.mShotType = rhs.mShotType;
			this.mStartX = rhs.mStartX;
			this.mStartY = rhs.mStartY;
			this.mEndX = rhs.mEndX;
			this.mEndY = rhs.mEndY;
			this.mMinHoverTime = rhs.mMinHoverTime;
			this.mMaxHoverTime = rhs.mMaxHoverTime;
			this.mMinFireDelay = rhs.mMinFireDelay;
			this.mMaxFireDelay = rhs.mMaxFireDelay;
			this.mFrogStunTime = rhs.mFrogStunTime;
			this.mFrogPoisonTime = rhs.mFrogPoisonTime;
			this.mFrogHallucinateTime = rhs.mFrogHallucinateTime;
			this.mDecMinHover = rhs.mDecMinHover;
			this.mDecMaxHover = rhs.mDecMaxHover;
			this.mDecMinFire = rhs.mDecMinFire;
			this.mDecMaxFire = rhs.mDecMaxFire;
			this.mSubType = rhs.mSubType;
			this.mMaxBulletsToFire = rhs.mMaxBulletsToFire;
			this.mMaxRetaliationBullets = rhs.mMaxRetaliationBullets;
			this.mMinSineShotTime = rhs.mMinSineShotTime;
			this.mColorVampShotType = rhs.mColorVampShotType;
			this.mColorChangeTimer = rhs.mColorChangeTimer;
			this.mMinColorChangeTime = rhs.mMinColorChangeTime;
			this.mMaxColorChangeTime = rhs.mMaxColorChangeTime;
			this.mColorVampHealthInc = rhs.mColorVampHealthInc;
			this.mColorVampHealthIncPerHit = rhs.mColorVampHealthIncPerHit;
			this.mIncMaxShotHealthAmt = rhs.mIncMaxShotHealthAmt;
			this.mIncRetalMaxShotHealthAmt = rhs.mIncRetalMaxShotHealthAmt;
			this.mMaxShotIncCounter = rhs.mMaxShotIncCounter;
			this.mRetalShotIncCounter = rhs.mRetalShotIncCounter;
			this.mDefaultMovementUpdateDelay = rhs.mDefaultMovementUpdateDelay;
			this.mMovementMode = rhs.mMovementMode;
			this.mMovementAccel = rhs.mMovementAccel;
			this.mMinSpots = rhs.mMinSpots;
			this.mMaxSpots = rhs.mMaxSpots;
			this.mMinSpotRad = rhs.mMinSpotRad;
			this.mMaxSpotRad = rhs.mMaxSpotRad;
			this.mMinSpotFade = rhs.mMinSpotFade;
			this.mMaxSpotFade = rhs.mMaxSpotFade;
			this.mInkTargetMode = rhs.mInkTargetMode;
			this.mSpotFadeDelay = rhs.mSpotFadeDelay;
			this.mFlightSpeed = rhs.mFlightSpeed;
			this.mHomingCorrectionAmt = rhs.mHomingCorrectionAmt;
			this.mFlightMinDist = rhs.mFlightMinDist;
			this.mColorVampChanceToMatch2ndBall = rhs.mColorVampChanceToMatch2ndBall;
			this.mBulletRadius = rhs.mBulletRadius;
			this.mSinusoidalRetaliation = rhs.mSinusoidalRetaliation;
			this.mCanShootBullets = rhs.mCanShootBullets;
			this.mSineShotsTargetPlayer = rhs.mSineShotsTargetPlayer;
			this.mEndHoverOnHit = rhs.mEndHoverOnHit;
			this.mColorVampire = rhs.mColorVampire;
			this.mAvoidColor = rhs.mAvoidColor;
			this.mStrafe = rhs.mStrafe;
			this.mEnrageShieldRestore = rhs.mEnrageShieldRestore;
			this.mBulletsUseSphereColl = rhs.mBulletsUseSphereColl;
			this.mMinBulletSpeed = rhs.mMinBulletSpeed;
			this.mMaxBulletSpeed = rhs.mMaxBulletSpeed;
			this.mMinRetalSpeed = rhs.mMinRetalSpeed;
			this.mMaxRetalSpeed = rhs.mMaxRetalSpeed;
			this.mSpeed = rhs.mSpeed;
			this.mDefaultSpeed = rhs.mDefaultSpeed;
			this.mVolcanoOffscreenDelay = rhs.mVolcanoOffscreenDelay;
			this.mMinAmp = rhs.mMinAmp;
			this.mMaxAmp = rhs.mMaxAmp;
			this.mMinYInc = rhs.mMinYInc;
			this.mMaxYInc = rhs.mMaxYInc;
			this.mMinXInc = rhs.mMinXInc;
			this.mMaxXInc = rhs.mMaxXInc;
			this.mMinFreq = rhs.mMinFreq;
			this.mMaxFreq = rhs.mMaxFreq;
			this.mFrogSlowTimer = rhs.mFrogSlowTimer;
			for (int i = 0; i < 4; i++)
			{
				this.mShieldQuadrant[i] = rhs.mShieldQuadrant[i];
			}
			this.mPoints.Clear();
			for (int j = 0; j < rhs.mPoints.Count; j++)
			{
				this.mPoints.Add(new Point(rhs.mPoints[j]));
			}
			this.mBerserkMovementVec.Clear();
			for (int k = 0; k < rhs.mBerserkMovementVec.Count; k++)
			{
				this.mBerserkMovementVec.Add(new BossBerserkMovement(rhs.mBerserkMovementVec[k]));
			}
			this.mBullets.Clear();
		}

		public BossShoot(Level l)
			: base(l)
		{
			this.mDestX = 0f;
			this.mDestY = 0f;
			this.mSpeed = 0f;
			this.mHoverTime = 0;
			this.mFireDelay = 0;
			this.mStartX = 0;
			this.mEndX = 0;
			this.mStartY = 0;
			this.mEndY = 0;
			this.mMinHoverTime = 0;
			this.mMaxHoverTime = 0;
			this.mPauseShieldRegen = false;
			this.mMinFireDelay = 0;
			this.mMaxFireDelay = 0;
			this.mMinBulletSpeed = 0f;
			this.mMaxBulletSpeed = 0f;
			this.mFrogStunTime = 0;
			this.mDecMinHover = 0;
			this.mDecMaxHover = 0;
			this.mDecMinFire = 0;
			this.mDecMaxFire = 0;
			this.mXOff = 0;
			this.mYOff = 0;
			this.mSubType = 0;
			this.mMaxBulletsToFire = 1;
			this.mMaxRetaliationBullets = 0;
			this.mSinusoidalRetaliation = false;
			this.mMinAmp = 2f;
			this.mMaxAmp = 4f;
			this.mMinYInc = 2f;
			this.mMaxYInc = 4f;
			this.mMinXInc = 2f;
			this.mMaxXInc = 4f;
			this.mMinFreq = 0.04f;
			this.mMaxFreq = 0.04f;
			this.mPauseMovement = false;
			this.mCanShootBullets = false;
			this.mSineShotsTargetPlayer = false;
			this.mMinSineShotTime = 200;
			this.mMaxSineShotTime = 400;
			this.mShotType = 5;
			this.mEndHoverOnHit = false;
			this.mColorVampShotType = -1;
			this.mColorVampire = false;
			this.mAvoidColor = false;
			this.mMinColorChangeTime = 0;
			this.mMaxColorChangeTime = 0;
			this.mColorChangeTimer = 0;
			this.mColorVampHealthInc = 0;
			this.mColorVampHealthIncPerHit = 0;
			this.mStrafe = false;
			this.mMaxShotIncCounter = 0;
			this.mRetalShotIncCounter = 0;
			this.mIncMaxShotHealthAmt = 0;
			this.mIncRetalMaxShotHealthAmt = 0;
			this.mRetalShotDelay = 0;
			this.mMinRetalSpeed = 0f;
			this.mMaxRetalSpeed = 0f;
			this.mColorVampChanceToMatch2ndBall = 0;
			this.mFrogPoisonTime = 0;
			this.mFlightSpeed = 0f;
			this.mFlightMinDist = 100;
			this.mFrogHallucinateTime = 0;
			this.mHomingCorrectionAmt = 0.05f;
			this.mFrogSlowTimer = 0;
			this.mBombAppearDelay = 0;
			this.mCurrentLocPoint = -1;
			this.mUseShield = false;
			this.mShieldPauseTime = 0;
			this.mShieldRotateSpeed = 0f;
			this.mCurShieldPauseTime = 0;
			this.mShieldAngle = 0f;
			this.mShieldTargetAngle = 0f;
			this.mShieldQuadRespawnTime = 0;
			this.mBallShieldDamage = 0;
			this.mShieldHP = 1;
			this.mEnrageShieldRestore = false;
			this.mEndHoverCountdown = 0;
			this.mMinSpotRad = 0;
			this.mMaxSpotRad = 0;
			this.mMinSpots = 0;
			this.mMaxSpots = 0;
			this.mMinSpotFade = 0f;
			this.mMaxSpotFade = 0f;
			this.mSpotFadeDelay = 0;
			this.mInkTargetMode = 0;
			this.mEnrageDelay = 0;
			this.mEnrageDelayTimer = 0;
			this.mMovementMode = 0;
			this.mMovementAccel = 999999f;
			this.mDefaultMovementUpdateDelay = 0;
			this.mMovementUpdateDelay = 0;
			this.mTargetDestX = 0f;
			this.mVolcanoOffscreenDelay = 0;
			this.mBulletsUseSphereColl = true;
			this.mBulletRadius = 0;
			this.mTeleportPct = 0f;
			this.mTeleportTime = -1;
			this.mTeleportMinTime = 0;
			this.mTeleportMaxTime = 0;
			this.mTeleportDir = 0;
			this.mShieldRadius = 120;
			this.mDrawHeartsBelowMisc = true;
			this.mAttackDelayAfterHitFrog = 0;
			this.mMaxShotBounces = 0;
			this.mHitEffectYOff = 40;
			this.mDrawHeartsBelowBoss = false;
			this.mBossRadius = 24;
			this.mWidth = 146;
			this.mHeight = 124;
			this.mBullets.Reserve(100);
		}

		public BossShoot()
			: this(null)
		{
		}

		public override void Dispose()
		{
		}

		public virtual void DeleteAllBullets()
		{
			for (int i = 0; i < this.mBullets.Count; i++)
			{
				this.BossBulletDestroyed(this.mBullets[i], true);
				this.BulletErased(i);
			}
			this.mBullets.Clear();
		}

		public virtual void SetDestX(float dx)
		{
			this.mTargetDestX = dx;
			if (this.mMovementUpdateDelay <= 0)
			{
				this.mMovementUpdateDelay = this.mDefaultMovementUpdateDelay;
			}
		}

		public virtual void AddBerserkMovement(BossBerserkMovement bbm)
		{
			this.mBerserkMovementVec.Add(bbm);
		}

		public List<BossBerserkMovement> getBerserkMovementList()
		{
			return this.mBerserkMovementVec;
		}

		public override void PostInstantiationHook(Boss source_boss)
		{
			base.PostInstantiationHook(source_boss);
			base.AddParamPointer("ColorHelp", this.mDColorVampChanceToMatch2ndBall);
			base.AddParamPointer("FrogStun", this.mDFrogStunTime);
			base.AddParamPointer("stun", this.mDFrogStunTime);
			base.AddParamPointer("FrogPoison", this.mDFrogPoisonTime);
			base.AddParamPointer("FrogHallucinate", this.mDFrogHallucinateTime);
			base.AddParamPointer("hallucinate", this.mDFrogHallucinateTime);
			base.AddParamPointer("poison", this.mDFrogPoisonTime);
			base.AddParamPointer("SlowShot", this.mDFrogSlowTimer);
			base.AddParamPointer("ShotDelay", this.mDShotDelay);
			base.AddParamPointer("flightspeed", this.mDFlightSpeed);
			base.AddParamPointer("minflightdist", this.mDFlightMinDist);
			base.AddParamPointer("VampHealthInc", this.mDColorVampHealthInc);
			base.AddParamPointer("VampColorChangeMin", this.mDMinColorChangeTime);
			base.AddParamPointer("VampColorChangeMax", this.mDMaxColorChangeTime);
			base.AddParamPointer("HomingSpeed", this.mDHomingCorrectionAmt);
			base.AddParamPointer("MinHover", this.mDMinHoverTime);
			base.AddParamPointer("MaxHover", this.mDMaxHoverTime);
			base.AddParamPointer("MinFire", this.mDMinFireDelay);
			base.AddParamPointer("MaxFire", this.mDMaxFireDelay);
			base.AddParamPointer("MinBullet", this.mDMinBulletSpeed);
			base.AddParamPointer("MaxBullet", this.mDMaxBulletSpeed);
			base.AddParamPointer("MaxBullets", this.mDMaxBulletsToFire);
			base.AddParamPointer("Retaliation", this.mDMaxRetaliationBullets);
			base.AddParamPointer("MinSineShotTime", this.mDMinSineShotTime);
			base.AddParamPointer("MaxSineShotTime", this.mDMaxSineShotTime);
			base.AddParamPointer("MinAmp", this.mDMinAmp);
			base.AddParamPointer("MaxAmp", this.mDMaxAmp);
			base.AddParamPointer("MinFreq", this.mDMinFreq);
			base.AddParamPointer("MaxFreq", this.mDMaxFreq);
			base.AddParamPointer("MaxSineYInc", this.mDMaxYInc);
			base.AddParamPointer("MinSineYInc", this.mDMinYInc);
			base.AddParamPointer("MaxSineXInc", this.mDMaxXInc);
			base.AddParamPointer("MinSineXInc", this.mDMinXInc);
			base.AddParamPointer("MoveSpeed", this.mDDefaultSpeed);
			base.AddParamPointer("Strafe", this.mDStrafe);
			base.AddParamPointer("EndHoverOnHit", this.mDEndHoverOnHit);
			base.AddParamPointer("RetalSpeedMin", this.mDMinRetalSpeed);
			base.AddParamPointer("RetalSpeedMax", this.mDMaxRetalSpeed);
			base.AddParamPointer("ShotType", this.mDShotType);
			base.AddParamPointer("TeleportMinTime", this.mDTeleportMinTime);
			base.AddParamPointer("TeleportMaxTime", this.mDTeleportMaxTime);
			base.AddParamPointer("Accel", this.mDMovementAccel);
			base.AddParamPointer("MoveDelay", this.mDDefaultMovementUpdateDelay);
			base.AddParamPointer("MoveMode", this.mDMovementMode);
			base.AddParamPointer("UseShield", this.mDUseShield);
			base.AddParamPointer("ShieldRotSpeed", this.mDShieldRotateSpeed);
			base.AddParamPointer("ShieldRespawnTime", this.mDShieldQuadRespawnTime);
			base.AddParamPointer("ShieldPauseTime", this.mDShieldPauseTime);
			base.AddParamPointer("ShieldHP", this.mDShieldHP);
			base.AddParamPointer("BallShieldDamage", this.mDBallShieldDamage);
		}

		public override void Init(Level l)
		{
			base.Init(l);
			if (this.mMovementMode == 0)
			{
				if (this.mPoints.Count == 0)
				{
					if (this.mStartY <= 0)
					{
						this.mX = (float)(this.mEndX - this.mStartX) / 2f + (float)this.mStartX;
						this.CalcDestX();
					}
					else
					{
						this.mY = (float)(this.mEndY - this.mStartY) / 2f + (float)this.mStartY;
						this.CalcDestY();
					}
				}
				else
				{
					this.WarpToPoint(false);
					this.mHoverTime = SexyFramework.Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
				}
			}
			else
			{
				this.mTargetDestX = (this.mDestX = (this.mX = (float)ZumasRevenge.Common._SS(GameApp.gApp.mWidth) / 2f - (float)GameApp.gApp.mBoardOffsetX));
			}
			for (int i = 0; i < 4; i++)
			{
				this.mShieldQuadrant[i].mHP = this.mShieldHP;
			}
			this.ReInit();
		}

		public override void Update(float f)
		{
			BossShoot.gVolcanoBulletCounter = 0;
			base.Update(f);
			bool flag = this.AtDest();
			if (this.mDoDeathExplosions || this.mHP <= 0f)
			{
				return;
			}
			if (this.mTeleportTime > 0)
			{
				this.mTeleportTime--;
			}
			bool flag2 = this.mLevel.AllCurvesAtRolloutPoint();
			if (this.mTeleportTime == 0 && SexyFramework.Common._geq(this.mAlphaOverride, 255f) && this.mFireDelay > 0 && !base.IsStunned() && flag2 && this.mTeleportDir == 0)
			{
				this.mTeleportDir = -1;
				this.mTeleportPct = 0f;
			}
			if (this.mTeleportDir != 0)
			{
				this.mTeleportPct += ZumasRevenge.Common._M(0.05f);
				if (this.mTeleportPct >= 1f)
				{
					this.mTeleportPct = 0f;
					if (this.mTeleportDir == -1)
					{
						this.mTeleportDir = 1;
						this.mX = (float)this.GetMinXDist();
						this.CalcDestX();
					}
					else
					{
						this.mTeleportTime = SexyFramework.Common.IntRange(this.mTeleportMinTime, this.mTeleportMaxTime);
						this.mTeleportDir = 0;
					}
				}
			}
			if (!base.IsStunned() && flag2 && SexyFramework.Common._geq(this.mAlphaOverride, 255f))
			{
				if (base.IsImpatient())
				{
					float num = ZumasRevenge.Common._M(0.0003f);
					this.mMinBulletSpeed += num;
					this.mMaxBulletSpeed += num;
					this.mMinRetalSpeed += num;
					this.mMaxRetalSpeed += num;
					if (this.mFireDelay > 100)
					{
						this.mFireDelay--;
					}
				}
				if (this.mUseShield)
				{
					if (this.mShieldPauseTime > 0 && this.mCurShieldPauseTime > 0 && --this.mCurShieldPauseTime == 0)
					{
						this.mShieldTargetAngle = this.mShieldAngle + 1.570795f;
					}
					if (this.mShieldPauseTime > 0 && this.mCurShieldPauseTime == 0 && this.mShieldAngle < this.mShieldTargetAngle)
					{
						this.mShieldAngle += 1.570795f / (float)ZumasRevenge.Common._M(25);
						if (this.mShieldAngle > this.mShieldTargetAngle)
						{
							this.mCurShieldPauseTime = this.mShieldPauseTime;
							this.mShieldAngle = ZumasRevenge.Common.GetCanonicalAngleRad(this.mShieldTargetAngle);
						}
					}
					if (this.mShieldPauseTime == 0 && (this.mShieldAngle += this.mShieldRotateSpeed) > 6.28318f)
					{
						this.mShieldAngle = ZumasRevenge.Common.GetCanonicalAngleRad(this.mShieldAngle);
					}
					if (!this.mPauseShieldRegen)
					{
						for (int i = 0; i < 4; i++)
						{
							if (this.mShieldQuadrant[i].mTimer > 0)
							{
								this.mShieldQuadrant[i].mTimer--;
							}
						}
					}
				}
				if (this.mColorChangeTimer > 0 && this.mColorVampire && --this.mColorChangeTimer == 0)
				{
					int num2;
					for (num2 = this.mColorVampShotType; num2 == this.mColorVampShotType; num2 = SexyFramework.Common.Rand() % 4)
					{
					}
					this.mColorVampShotType = num2;
					this.mColorChangeTimer = SexyFramework.Common.IntRange(this.mMinColorChangeTime, this.mMaxColorChangeTime);
				}
				if (this.mTeleportDir == 0 && !this.mPauseMovement)
				{
					if (this.mMovementMode != 0)
					{
						if (this.mMovementUpdateDelay >= 0 && --this.mMovementUpdateDelay <= 0)
						{
							this.mDestX = this.mTargetDestX;
						}
						if (!SexyFramework.Common._eq(this.mX, this.mDestX))
						{
							float mX = this.mX;
							if (this.mX < this.mDestX)
							{
								this.mX += this.mMovementAccel;
							}
							else
							{
								this.mX -= this.mMovementAccel;
							}
							if ((mX < this.mDestX && this.mX >= this.mDestX) || (mX > this.mDestX && this.mX <= this.mDestX))
							{
								this.mX = this.mDestX;
							}
						}
					}
					if (this.mHoverTime == 0 && this.mEndHoverCountdown == 0 && this.mEnrageDelayTimer == 0 && !flag && !this.mStrafe && this.mMovementMode == 0)
					{
						if (this.mStartX > 0)
						{
							this.mX += this.mSpeed;
						}
						else
						{
							this.mY += this.mSpeed;
						}
						if (this.AtDest())
						{
							if (this.mStartX > 0)
							{
								this.mX = this.mDestX;
							}
							else
							{
								this.mY = this.mDestY;
							}
							this.mHoverTime = SexyFramework.Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
							this.mFireDelay = SexyFramework.Common.IntRange(this.mMinFireDelay, this.mMaxFireDelay);
							if (this.mFireDelay > this.mHoverTime)
							{
								this.mFireDelay = this.mHoverTime / 2;
							}
						}
					}
					else if ((this.mHoverTime > 0 || this.mStrafe || this.mMovementMode != 0) && this.mEndHoverCountdown == 0 && this.mEnrageDelayTimer == 0)
					{
						if (!this.mStrafe)
						{
							this.mXOff = (int)((double)ZumasRevenge.Common._M(-9) * Math.Cos((double)((float)(ZumasRevenge.Common._M1(1) * this.mUpdateCount) * 3.14159f / 180f)) + (double)ZumasRevenge.Common._M2(2));
							this.mYOff = (int)((double)ZumasRevenge.Common._M(6) * Math.Sin((double)((float)(ZumasRevenge.Common._M1(2) * this.mUpdateCount) * 3.14159f / 180f)) + (double)ZumasRevenge.Common._M2(3));
						}
						else if (this.mPoints.Count == 0)
						{
							if (this.mStartX > 0)
							{
								this.mX += this.mSpeed;
							}
							else
							{
								this.mY += this.mSpeed;
							}
							if (this.AtDest())
							{
								if (this.mStartX > 0)
								{
									this.mX = this.mDestX;
									this.CalcDestX();
								}
								else
								{
									this.mY = this.mDestY;
									this.CalcDestY();
								}
							}
						}
						if (!this.mStrafe && this.mMovementMode == 0 && --this.mHoverTime == 0)
						{
							if (this.mPoints.Count == 0)
							{
								this.mXOff = (this.mYOff = 0);
								if (this.mStartX > 0)
								{
									this.CalcDestX();
								}
								else
								{
									this.CalcDestY();
								}
							}
							else
							{
								this.WarpToPoint();
								this.mHoverTime = SexyFramework.Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
							}
						}
						if (this.mFireDelay > 0)
						{
							this.mFireDelay--;
						}
						if (this.mAttackDelayAfterHitFrog > 0)
						{
							this.mAttackDelayAfterHitFrog--;
						}
						bool flag3 = (GameApp.gApp.GetLevelMgr().mBossesCanAttackFuckedFrog || !this.mLevel.mFrog.IsFuckedUp()) && this.mAttackDelayAfterHitFrog == 0;
						if (this.mFireDelay == 0 && flag3 && this.CanFire())
						{
							if (this.mStrafe || this.mMovementMode != 0)
							{
								this.mFireDelay = SexyFramework.Common.IntRange(this.mMinFireDelay, this.mMaxFireDelay);
							}
							BossBullet bossBullet = null;
							BossBullet bossBullet2 = null;
							if (this.mSubType == 0)
							{
								bossBullet = this.CreateBossBullet();
								this.mBullets.Add(bossBullet);
								bossBullet.mBossShoot = this;
								bossBullet.mX = this.mX;
								bossBullet.mY = this.mY;
								this.FireBulletAtPlayer(bossBullet, SexyFramework.Common.FloatRange(this.mMinBulletSpeed, this.mMaxBulletSpeed));
								bossBullet.mId = ++BossShoot.gLastBulletId;
							}
							else
							{
								List<int> list = new List<int>();
								if (this.mShotType == 5)
								{
									for (int j = 0; j < 5; j++)
									{
										list.Add(j);
									}
								}
								else
								{
									for (int k = 0; k < this.mMaxBulletsToFire; k++)
									{
										list.Add(this.mShotType);
									}
								}
								int l = 0;
								while (l < this.mMaxBulletsToFire)
								{
									int num3 = SexyFramework.Common.Rand() % list.Count;
									int num4 = list[num3];
									list.RemoveAt(num3);
									l++;
									bossBullet = this.CreateBossBullet();
									this.mBullets.Add(bossBullet);
									if (bossBullet2 == null)
									{
										bossBullet2 = bossBullet;
									}
									bossBullet.mBossShoot = this;
									bossBullet.mX = this.mX;
									bossBullet.mY = this.mY;
									bossBullet.mId = ++BossShoot.gLastBulletId;
									bossBullet.mDelay = (l - 1) * this.mShotDelay;
									bossBullet.mShotType = num4;
									bossBullet.mBouncesLeft = this.mMaxShotBounces;
									if (num4 == 1 || num4 == 3)
									{
										this.FireBulletAtPlayer(bossBullet, SexyFramework.Common.FloatRange(this.mMinBulletSpeed, this.mMaxBulletSpeed));
										bossBullet.mHoming = num4 == 3;
										bossBullet.mTargetVX = bossBullet.mVX;
										bossBullet.mTargetVY = bossBullet.mVY;
									}
									else if (num4 == 2 && !this.FireSinusoidalBullet(bossBullet, num4 == 1))
									{
										if (bossBullet2 == bossBullet)
										{
											bossBullet2 = null;
										}
										bossBullet = null;
										this.mBullets.RemoveAt(this.mBullets.Count - 1);
									}
									else if (num4 == 0)
									{
										bossBullet.mSineMotion = false;
										if (this.mStartY > 0)
										{
											bossBullet.mVX = SexyFramework.Common.FloatRange(this.mMinBulletSpeed, this.mMaxBulletSpeed);
											bossBullet.mVY = 0f;
											if (this.mX > (float)this.mLevel.mFrog.GetCenterX())
											{
												bossBullet.mVX *= -1f;
											}
										}
										else
										{
											bossBullet.mVX = 0f;
											bossBullet.mVY = SexyFramework.Common.FloatRange(this.mMinBulletSpeed, this.mMaxBulletSpeed);
											if (this.mY > (float)this.mLevel.mFrog.GetCenterY())
											{
												bossBullet.mVY *= -1f;
											}
										}
									}
									else if (num4 == 4)
									{
										bossBullet.mVX = 0f;
										bossBullet.mVY = -SexyFramework.Common.FloatRange(this.mMinBulletSpeed, this.mMinBulletSpeed);
										bossBullet.mOffscreenPause = this.mVolcanoOffscreenDelay;
										bossBullet.mVolcanoShot = true;
									}
								}
							}
							if (bossBullet != null)
							{
								bossBullet.mUpdateCount = 0;
								this.mFireDelay = SexyFramework.Common.IntRange(this.mMinFireDelay, this.mMaxFireDelay);
								if (bossBullet2.mDelay == 0)
								{
									base.PlaySound(2);
								}
								this.DidFire();
							}
						}
					}
				}
			}
			if (SexyFramework.Common._geq(this.mAlphaOverride, 255f))
			{
				for (int m = 0; m < this.mBullets.Count; m++)
				{
					BossBullet bossBullet3 = this.mBullets[m];
					if (bossBullet3.mDeleteInstantly)
					{
						this.BossBulletDestroyed(bossBullet3, false);
						this.mBullets.RemoveAt(m);
						this.BulletErased(m);
						m--;
					}
					else if (!this.PreBulletUpdate(bossBullet3, m))
					{
						bossBullet3.mUpdateCount++;
						if (this.mStartY > 0)
						{
							bossBullet3.mVY += bossBullet3.mGravity;
						}
						else
						{
							bossBullet3.mVX += bossBullet3.mGravity;
						}
						if (!bossBullet3.mSineMotion)
						{
							float mX2 = bossBullet3.mX;
							float mY = bossBullet3.mY;
							bossBullet3.mX += bossBullet3.mVX;
							bossBullet3.mY += bossBullet3.mVY;
							if (this.mMaxShotBounces > 0)
							{
								bool flag4 = false;
								int num5 = 0;
								int num6 = 0;
								this.GetShotBounceOffs(bossBullet3, ref num5, ref num6);
								if (mY + (float)num6 < (float)ZumasRevenge.Common._SS(GameApp.gApp.mHeight) && bossBullet3.mY + (float)num6 >= (float)ZumasRevenge.Common._SS(GameApp.gApp.mHeight) && bossBullet3.mVY > 0f)
								{
									bossBullet3.mVY = -Math.Abs(bossBullet3.mVY);
									bossBullet3.mY = mY;
									flag4 = true;
								}
								else if (mY > (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-50)) && bossBullet3.mY <= (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(-50)) && bossBullet3.mVY < 0f)
								{
									bossBullet3.mVY = Math.Abs(bossBullet3.mVY);
									bossBullet3.mY = mY;
									flag4 = true;
								}
								if (mX2 + (float)num5 < (float)ZumasRevenge.Common._SS(GameApp.gApp.mWidth) && bossBullet3.mX + (float)num5 >= (float)ZumasRevenge.Common._SS(GameApp.gApp.mWidth) && bossBullet3.mVX > 0f)
								{
									bossBullet3.mVX = -Math.Abs(bossBullet3.mVX);
									bossBullet3.mX = mX2;
									flag4 = true;
								}
								else if (mX2 > (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-40)) && bossBullet3.mX <= (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(-40)) && bossBullet3.mVX < 0f)
								{
									bossBullet3.mVX = Math.Abs(bossBullet3.mVX);
									bossBullet3.mX = mX2;
									flag4 = true;
								}
								if (this.mMaxShotBounces > 0 && bossBullet3.mBouncesLeft <= 0)
								{
									goto IL_10C6;
								}
								if (flag4)
								{
									bossBullet3.mTargetVX = bossBullet3.mVX;
									bossBullet3.mTargetVY = bossBullet3.mVY;
									bossBullet3.mBouncesLeft--;
									this.ShotBounced(bossBullet3);
								}
								if (bossBullet3.mBouncesLeft <= 0)
								{
									goto IL_10C6;
								}
							}
						}
						else if (this.mStartX > 0)
						{
							bossBullet3.mX += bossBullet3.mAmp * (float)Math.Cos((double)((float)bossBullet3.mUpdateCount * bossBullet3.mFreq));
							bossBullet3.mY += bossBullet3.mVY;
						}
						else
						{
							bossBullet3.mY += bossBullet3.mAmp * (float)Math.Cos((double)((float)bossBullet3.mUpdateCount * bossBullet3.mFreq));
							bossBullet3.mX += bossBullet3.mVX;
						}
						if (bossBullet3.mHoming && bossBullet3.mY < (float)this.mLevel.mFrogY[0])
						{
							float num7 = 0f;
							this.GetTargetedVelocity(bossBullet3.mInitialSpeed, bossBullet3.mX, bossBullet3.mY, ref bossBullet3.mTargetVX, ref num7);
						}
						if (!SexyFramework.Common._eq(bossBullet3.mVX, bossBullet3.mTargetVX))
						{
							bool flag5 = bossBullet3.mVX < bossBullet3.mTargetVX;
							bossBullet3.mVX += this.mHomingCorrectionAmt * (float)((bossBullet3.mVX < bossBullet3.mTargetVX) ? 1 : (-1));
							if ((flag5 && bossBullet3.mVX > bossBullet3.mTargetVX) || (!flag5 && bossBullet3.mVX < bossBullet3.mTargetVX))
							{
								bossBullet3.mVX = bossBullet3.mTargetVX;
							}
						}
						Rect bulletRect = this.GetBulletRect(bossBullet3);
						if (this.CheckBulletHitPlayer(bossBullet3))
						{
							this.mAttackDelayAfterHitFrog = GameApp.gApp.GetLevelMgr().mAttackDelayAfterHittingFrog;
							this.mHulaAmnesty = this.mAttackDelayAfterHitFrog;
							if (this.mFrogStunTime > 0)
							{
								this.mLevel.mFrog.Stun(this.mFrogStunTime);
							}
							else if (this.mFrogPoisonTime > 0)
							{
								this.mLevel.mFrog.Poison(this.mFrogPoisonTime);
							}
							else if (this.mFrogHallucinateTime > 0)
							{
								this.mLevel.mBoard.SetHallucinateTimer(this.mFrogHallucinateTime);
							}
							else if (this.mFrogSlowTimer > 0)
							{
								this.mLevel.mFrog.SetSlowTimer(this.mFrogSlowTimer);
								this.AppliedSlowTimer();
							}
							else if (this.mMinSpots > 0 && this.mMaxSpots > 0 && this.mMinSpotRad > 0 && this.mMaxSpotRad > 0)
							{
								for (int n = 0; n < this.mLevel.mNumCurves; n++)
								{
									this.mLevel.mCurveMgr[n].AddInkSpots(SexyFramework.Common.IntRange(this.mMinSpots, this.mMaxSpots), (float)this.mMinSpotRad, (float)this.mMaxSpotRad, this.mMinSpotFade, this.mMaxSpotFade, this.mSpotFadeDelay, this.mInkTargetMode);
								}
							}
							this.BulletHitPlayer(bossBullet3);
							this.BossBulletDestroyed(bossBullet3, false);
							this.mBullets.RemoveAt(m);
							this.BulletErased(m);
							m--;
						}
						else if ((!bulletRect.Intersects(new Rect(0, 0, ZumasRevenge.Common._SS(GameApp.gApp.mWidth), ZumasRevenge.Common._SS(GameApp.gApp.mHeight + 200))) && !bossBullet3.mVolcanoShot && this.mMaxShotBounces == 0) || (bossBullet3.mVolcanoShot && bossBullet3.mY > (float)(ZumasRevenge.Common._SS(GameApp.gApp.mHeight) + 300)))
						{
							this.BossBulletDestroyed(bossBullet3, true);
							this.mBullets.RemoveAt(m);
							this.BulletErased(m);
							m--;
						}
					}
					IL_10C6:;
				}
				if (this.mEndHoverCountdown > 0 && --this.mEndHoverCountdown == 0)
				{
					this.WarpToPoint();
					this.mHoverTime = SexyFramework.Common.IntRange(this.mMinHoverTime, this.mMaxHoverTime);
				}
				if (this.mEnrageDelayTimer > 0)
				{
					this.mEnrageDelayTimer--;
				}
			}
		}

		public override void Update()
		{
			this.Update(1f);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mTeleportDir != 0)
			{
				int num = ZumasRevenge.Common._S(this.mWidth + ZumasRevenge.Common._M(200));
				g.PushState();
				int theX = (int)(ZumasRevenge.Common._S(this.mX) - (float)(num / 2));
				int theY = (int)(ZumasRevenge.Common._S(this.mY) - (float)(ZumasRevenge.Common._S(this.mHeight + ZumasRevenge.Common._M(10)) / 2));
				int theHeight;
				if (this.mTeleportDir == -1)
				{
					theHeight = (int)((float)ZumasRevenge.Common._S(this.mHeight) - (float)ZumasRevenge.Common._S(this.mHeight) * this.mTeleportPct);
				}
				else
				{
					theHeight = (int)((float)ZumasRevenge.Common._S(this.mHeight) * this.mTeleportPct);
				}
				g.ClipRect(theX, theY, num, theHeight);
			}
			if (this.mDrawHeartsBelowBoss)
			{
				this.DrawHearts(g);
			}
			this.DrawBossSpecificArt(g);
			if (!this.mStrafe && this.mHoverTime <= ZumasRevenge.Common._M(100) && this.mHoverTime > 0 && this.mPoints.Count > 1 && this.mHoverTime / ZumasRevenge.Common._M1(10) % 2 == 0)
			{
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, ZumasRevenge.Common._M(128));
				g.SetDrawMode(1);
				this.DrawBossSpecificArt(g);
				g.SetDrawMode(0);
				g.SetColorizeImages(false);
			}
			if (this.mDoExplosion && this.mShouldDoDeathExplosions)
			{
				this.mHitEffect.mDrawTransform.LoadIdentity();
				float num2 = GameApp.DownScaleNum(1f);
				this.mHitEffect.mDrawTransform.Scale(num2, num2);
				this.mHitEffect.mDrawTransform.Translate(ZumasRevenge.Common._S(this.mX) + (float)ZumasRevenge.Common._S(ZumasRevenge.Common._M(9)), ZumasRevenge.Common._S(this.mY) + (float)ZumasRevenge.Common._S(this.mHitEffectYOff));
				this.mHitEffect.Draw(g);
			}
			if (this.mTeleportDir != 0)
			{
				g.PopState();
			}
			if (this.mDrawHeartsBelowMisc && !this.mDrawHeartsBelowBoss)
			{
				this.DrawHearts(g);
			}
			this.DrawMisc(g);
			if (!this.mDrawHeartsBelowMisc && !this.mDrawHeartsBelowBoss)
			{
				this.DrawHearts(g);
			}
		}

		public override void SyncState(DataSync sync)
		{
			base.SyncState(sync);
			sync.SyncBoolean(ref this.mPauseShieldRegen);
			sync.SyncLong(ref this.mCurrentLocPoint);
			sync.SyncFloat(ref this.mSpeed);
			sync.SyncFloat(ref this.mDDefaultSpeed.value);
			sync.SyncFloat(ref this.mDMaxXInc.value);
			sync.SyncFloat(ref this.mDMinXInc.value);
			sync.SyncFloat(ref this.mDMaxYInc.value);
			sync.SyncFloat(ref this.mDMinYInc.value);
			sync.SyncFloat(ref this.mDMinAmp.value);
			sync.SyncFloat(ref this.mDMaxAmp.value);
			sync.SyncFloat(ref this.mDMinFreq.value);
			sync.SyncFloat(ref this.mDMaxFreq.value);
			sync.SyncFloat(ref this.mDHomingCorrectionAmt.value);
			sync.SyncLong(ref this.mDFrogSlowTimer.value);
			sync.SyncLong(ref this.mBombAppearDelay);
			sync.SyncLong(ref this.mEndHoverCountdown);
			sync.SyncLong(ref this.mEnrageDelayTimer);
			sync.SyncLong(ref BossShoot.gLastBulletId);
			sync.SyncBoolean(ref this.mPauseMovement);
			sync.SyncLong(ref this.mAttackDelayAfterHitFrog);
			sync.SyncLong(ref this.mTeleportDir);
			sync.SyncFloat(ref this.mTeleportPct);
			sync.SyncLong(ref this.mTeleportTime);
			sync.SyncLong(ref this.mVolcanoOffscreenDelay);
			sync.SyncLong(ref this.mDMovementMode.value);
			sync.SyncFloat(ref this.mDMovementAccel.value);
			sync.SyncLong(ref this.mDDefaultMovementUpdateDelay.value);
			sync.SyncLong(ref this.mMovementUpdateDelay);
			sync.SyncFloat(ref this.mTargetDestX);
			sync.SyncLong(ref this.mDBallShieldDamage.value);
			sync.SyncLong(ref this.mDShieldHP.value);
			sync.SyncBoolean(ref this.mDUseShield.value);
			sync.SyncFloat(ref this.mDShieldRotateSpeed.value);
			sync.SyncLong(ref this.mDShieldPauseTime.value);
			sync.SyncFloat(ref this.mShieldAngle);
			for (int i = 0; i < 4; i++)
			{
				sync.SyncLong(ref this.mShieldQuadrant[i].mTimer);
				sync.SyncLong(ref this.mShieldQuadrant[i].mHP);
			}
			sync.SyncLong(ref this.mCurShieldPauseTime);
			sync.SyncFloat(ref this.mShieldTargetAngle);
			sync.SyncLong(ref this.mDShieldQuadRespawnTime.value);
			Buffer buffer = sync.GetBuffer();
			if (sync.isRead())
			{
				this.mPoints.Clear();
				int num = (int)buffer.ReadLong();
				for (int j = 0; j < num; j++)
				{
					int theX = (int)buffer.ReadLong();
					int theY = (int)buffer.ReadLong();
					this.mPoints.Add(new Point(theX, theY));
				}
			}
			else
			{
				buffer.WriteLong((long)this.mPoints.Count);
				for (int k = 0; k < this.mPoints.Count; k++)
				{
					buffer.WriteLong((long)this.mPoints[k].mX);
					buffer.WriteLong((long)this.mPoints[k].mY);
				}
			}
			sync.SyncLong(ref this.mStartX);
			sync.SyncLong(ref this.mEndX);
			sync.SyncLong(ref this.mStartY);
			sync.SyncLong(ref this.mEndY);
			sync.SyncLong(ref this.mDColorVampChanceToMatch2ndBall.value);
			sync.SyncLong(ref this.mDShotType.value);
			sync.SyncLong(ref this.mDMinSineShotTime.value);
			sync.SyncLong(ref this.mDMaxSineShotTime.value);
			sync.SyncLong(ref this.mDMaxBulletsToFire.value);
			sync.SyncLong(ref this.mDMaxRetaliationBullets.value);
			sync.SyncFloat(ref this.mDMinBulletSpeed.value);
			sync.SyncFloat(ref this.mDMaxBulletSpeed.value);
			sync.SyncFloat(ref this.mDMinRetalSpeed.value);
			sync.SyncFloat(ref this.mDMaxRetalSpeed.value);
			sync.SyncLong(ref this.mDMinColorChangeTime.value);
			sync.SyncLong(ref this.mDMaxColorChangeTime.value);
			sync.SyncLong(ref this.mDFrogStunTime.value);
			sync.SyncLong(ref this.mDShotDelay.value);
			sync.SyncLong(ref this.mDColorVampHealthInc.value);
			sync.SyncFloat(ref this.mDestY);
			sync.SyncFloat(ref this.mDestX);
			sync.SyncLong(ref this.mHoverTime);
			sync.SyncLong(ref this.mFireDelay);
			sync.SyncLong(ref this.mXOff);
			sync.SyncLong(ref this.mYOff);
			sync.SyncLong(ref this.mDMinHoverTime.value);
			sync.SyncLong(ref this.mDMaxHoverTime.value);
			sync.SyncLong(ref this.mDMinFireDelay.value);
			sync.SyncLong(ref this.mDMaxFireDelay.value);
			sync.SyncLong(ref this.mColorChangeTimer);
			sync.SyncLong(ref this.mColorVampShotType);
			sync.SyncLong(ref this.mMaxShotIncCounter);
			sync.SyncLong(ref this.mRetalShotIncCounter);
			sync.SyncLong(ref this.mDMaxBulletsToFire.value);
			sync.SyncLong(ref this.mDMaxRetaliationBullets.value);
			this.SyncListBossBullets(sync, this.mBullets, true);
		}

		private void SyncListBossBullets(DataSync sync, List<BossBullet> theList, bool clear)
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
					BossBullet bossBullet = new BossBullet();
					bossBullet.SyncState(sync);
					theList.Add(bossBullet);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)theList.Count);
			foreach (BossBullet bossBullet2 in theList)
			{
				bossBullet2.SyncState(sync);
			}
		}

		public override Boss Instantiate()
		{
			BossShoot bossShoot = new BossShoot(this.mLevel);
			bossShoot.CopyFrom(this);
			bossShoot.mBullets.Clear();
			return bossShoot;
		}

		public override bool Collides(Bullet b)
		{
			float p1x = 0f;
			float p1y = 0f;
			float p1x2 = 0f;
			float p1y2 = 0f;
			if (this.mUseShield && CommonMath.CircleCircleIntersection(this.mX, this.mY, (float)this.mShieldRadius, b.GetX(), b.GetY(), (float)b.GetRadius(), ref p1x, ref p1y, ref p1x2, ref p1y2))
			{
				float num = SexyFramework.Common.AngleBetweenPoints(p1x, p1y, this.mX, this.mY) + 3.14159f;
				float num2 = SexyFramework.Common.AngleBetweenPoints(p1x2, p1y2, this.mX, this.mY) + 3.14159f;
				for (int i = 0; i < 4; i++)
				{
					if (this.mShieldQuadrant[i].mTimer <= 0)
					{
						float num3 = this.mShieldAngle + (float)i * 3.14159f / 2f;
						float num4 = this.mShieldAngle + (float)(i + 1) * 3.14159f / 2f;
						if (num3 > 6.28318f && num4 > 6.28318f)
						{
							num3 = ZumasRevenge.Common.GetCanonicalAngleRad(num3);
							num4 = ZumasRevenge.Common.GetCanonicalAngleRad(num4);
						}
						if (num3 > num4)
						{
							float num5 = num3;
							num3 = num4;
							num4 = num5;
						}
						if ((num >= num3 && num <= num4) || (num2 >= num3 && num2 <= num4))
						{
							this.ShieldQuadrantHit(i);
							if (this.mBallShieldDamage > 0 && --this.mShieldQuadrant[i].mHP == 0)
							{
								this.mShieldQuadrant[i].mTimer = this.mShieldQuadRespawnTime;
								this.mShieldQuadrant[i].mHP = this.mShieldHP;
								base.PlaySound(9);
							}
							return true;
						}
					}
				}
			}
			float num6 = (float)b.GetRadius() * ZumasRevenge.Common._M(0.75f);
			new Rect((int)(b.GetX() - num6), (int)(b.GetY() - num6), (int)(num6 * 2f), (int)(num6 * 2f));
			bool flag = this.AllowFrogToFire() && this.BulletIntersectsBoss(b);
			if (this.mColorVampire && flag && this.mAvoidColor && b.GetColorType() == this.mColorVampShotType)
			{
				if (this.mColorVampHealthInc > 0)
				{
					this.mHP += (float)this.mColorVampHealthInc;
					if (this.mHP > this.mMaxHP)
					{
						this.mHP = this.mMaxHP;
					}
					int num7 = this.mColorVampHealthIncPerHit;
					for (int j = Boss.NUM_HEARTS - 1; j >= 0; j--)
					{
						if (this.mHeartCels[j] > 0)
						{
							int num8 = this.mHeartCels[j];
							if ((this.mHeartCels[j] -= num7) >= 0)
							{
								break;
							}
							this.mHeartCels[j] = 0;
							num7 -= num8;
						}
					}
				}
				return true;
			}
			return (this.mColorVampire && flag && !this.mAvoidColor && b.GetColorType() != this.mColorVampShotType) || base.Collides(b);
		}

		public override void ProximityBombActivated(float x, float y, int radius)
		{
			if (this.mUseShield)
			{
				float p1x = 0f;
				float p1y = 0f;
				float p1x2 = 0f;
				float p1y2 = 0f;
				bool flag = CommonMath.CircleCircleIntersection(this.mX, this.mY, (float)this.mProxBombRadius, x, y, (float)(radius + ZumasRevenge.Common.GetDefaultBallRadius()), ref p1x, ref p1y, ref p1x2, ref p1y2);
				if (flag)
				{
					float num = SexyFramework.Common.AngleBetweenPoints(p1x, p1y, this.mX, this.mY) + 3.14159f;
					float num2 = SexyFramework.Common.AngleBetweenPoints(p1x2, p1y2, this.mX, this.mY) + 3.14159f;
					for (int i = 0; i < 4; i++)
					{
						if (this.mShieldQuadrant[i].mTimer <= 50)
						{
							float num3 = this.mShieldAngle + (float)i * 3.14159f / 2f;
							float num4 = this.mShieldAngle + (float)(i + 1) * 3.14159f / 2f;
							if (num3 > 6.28318f && num4 > 6.28318f)
							{
								num3 = ZumasRevenge.Common.GetCanonicalAngleRad(num3);
								num4 = ZumasRevenge.Common.GetCanonicalAngleRad(num4);
							}
							if (num3 > num4)
							{
								float num5 = num3;
								num3 = num4;
								num4 = num5;
							}
							if ((num >= num3 && num <= num4) || (num2 >= num3 && num2 <= num4))
							{
								this.mShieldQuadrant[i].mTimer = this.mShieldQuadRespawnTime;
								this.mShieldQuadrant[i].mHP = this.mShieldHP;
								this.QuadHitByProxBomb(i);
							}
						}
					}
				}
			}
			base.ProximityBombActivated(x, y, radius);
		}

		public override void FrogInitialized(Gun g)
		{
			base.FrogInitialized(g);
			if (this.mMovementMode != 0)
			{
				this.mTargetDestX = (this.mDestX = (this.mX = (float)(ZumasRevenge.Common._SS(GameApp.gApp.mWidth) / 2 - GameApp.gApp.mBoardOffsetX)));
			}
		}

		public virtual void DisableShields()
		{
			for (int i = 0; i < 4; i++)
			{
				this.mShieldQuadrant[i].mTimer = this.mShieldQuadRespawnTime;
				this.mShieldQuadrant[i].mHP = this.mShieldHP;
			}
		}

		public Transform mGlobalTranform = new Transform();

		protected static int gLastBulletId = 0;

		protected static int PATROL_FLASH_TIMER = 10;

		protected static int PATROL_FLASH_COUNT = 20;

		protected static int DEFAULT_BULLET_SIZE = 40;

		protected static int gVolcanoBulletCounter = 0;

		protected ParamData<int> mDColorVampChanceToMatch2ndBall = new ParamData<int>();

		protected ParamData<int> mDFrogStunTime = new ParamData<int>();

		protected ParamData<int> mDFrogPoisonTime = new ParamData<int>();

		protected ParamData<int> mDFrogHallucinateTime = new ParamData<int>();

		protected ParamData<int> mDFrogSlowTimer = new ParamData<int>();

		protected ParamData<int> mDShotDelay = new ParamData<int>();

		protected ParamData<float> mDFlightSpeed = new ParamData<float>();

		protected ParamData<int> mDFlightMinDist = new ParamData<int>();

		protected ParamData<int> mDColorVampHealthInc = new ParamData<int>();

		protected ParamData<int> mDMinColorChangeTime = new ParamData<int>();

		protected ParamData<int> mDMaxColorChangeTime = new ParamData<int>();

		protected ParamData<float> mDHomingCorrectionAmt = new ParamData<float>();

		protected ParamData<int> mDMinHoverTime = new ParamData<int>();

		protected ParamData<int> mDMaxHoverTime = new ParamData<int>();

		protected ParamData<int> mDMinFireDelay = new ParamData<int>();

		protected ParamData<int> mDMaxFireDelay = new ParamData<int>();

		protected ParamData<float> mDMinBulletSpeed = new ParamData<float>();

		protected ParamData<float> mDMaxBulletSpeed = new ParamData<float>();

		protected ParamData<int> mDMaxBulletsToFire = new ParamData<int>();

		protected ParamData<int> mDMaxRetaliationBullets = new ParamData<int>();

		protected ParamData<int> mDMinSineShotTime = new ParamData<int>();

		protected ParamData<int> mDMaxSineShotTime = new ParamData<int>();

		protected ParamData<float> mDMinAmp = new ParamData<float>();

		protected ParamData<float> mDMaxAmp = new ParamData<float>();

		protected ParamData<float> mDMinFreq = new ParamData<float>();

		protected ParamData<float> mDMaxFreq = new ParamData<float>();

		protected ParamData<float> mDMaxYInc = new ParamData<float>();

		protected ParamData<float> mDMinYInc = new ParamData<float>();

		protected ParamData<float> mDMaxXInc = new ParamData<float>();

		protected ParamData<float> mDMinXInc = new ParamData<float>();

		protected ParamData<float> mDDefaultSpeed = new ParamData<float>();

		protected ParamData<bool> mDStrafe = new ParamData<bool>();

		protected ParamData<bool> mDEndHoverOnHit = new ParamData<bool>();

		protected ParamData<float> mDMinRetalSpeed = new ParamData<float>();

		protected ParamData<float> mDMaxRetalSpeed = new ParamData<float>();

		protected ParamData<int> mDShotType = new ParamData<int>();

		protected ParamData<int> mDTeleportMinTime = new ParamData<int>();

		protected ParamData<int> mDTeleportMaxTime = new ParamData<int>();

		protected ParamData<float> mDMovementAccel = new ParamData<float>();

		protected ParamData<int> mDDefaultMovementUpdateDelay = new ParamData<int>();

		protected ParamData<int> mDMovementMode = new ParamData<int>();

		protected ParamData<bool> mDUseShield = new ParamData<bool>();

		protected ParamData<float> mDShieldRotateSpeed = new ParamData<float>();

		protected ParamData<int> mDShieldQuadRespawnTime = new ParamData<int>();

		protected ParamData<int> mDShieldPauseTime = new ParamData<int>();

		protected ParamData<int> mDShieldHP = new ParamData<int>();

		protected ParamData<int> mDBallShieldDamage = new ParamData<int>();

		protected int mHitEffectYOff;

		protected bool mPauseMovement;

		protected bool mPauseShieldRegen;

		protected bool mDrawHeartsBelowMisc;

		protected bool mDrawHeartsBelowBoss;

		protected List<BossBerserkMovement> mBerserkMovementVec = new List<BossBerserkMovement>();

		protected List<BossBullet> mBullets = new List<BossBullet>();

		protected float mDestX;

		protected float mDestY;

		protected float mTargetDestX;

		protected float mShieldAngle;

		protected float mShieldTargetAngle;

		protected ShieldQuadrant[] mShieldQuadrant = new ShieldQuadrant[]
		{
			new ShieldQuadrant(),
			new ShieldQuadrant(),
			new ShieldQuadrant(),
			new ShieldQuadrant()
		};

		protected int mShieldRadius;

		protected int mHoverTime;

		protected int mFireDelay;

		protected int mXOff;

		protected int mYOff;

		protected int mCurShieldPauseTime;

		protected int mEndHoverCountdown;

		protected int mEnrageDelayTimer;

		protected int mMovementUpdateDelay;

		protected int mAttackDelayAfterHitFrog;

		public List<Point> mPoints = new List<Point>();

		public int mCurrentLocPoint;

		public int mMaxShotBounces;

		public int mTeleportDir;

		public float mTeleportPct;

		public int mTeleportTime;

		public int mEnrageDelay;

		public int mBombAppearDelay;

		public int mRetalShotDelay;

		public int mStartX;

		public int mStartY;

		public int mEndX;

		public int mEndY;

		public int mDecMinHover;

		public int mDecMaxHover;

		public int mDecMinFire;

		public int mDecMaxFire;

		public int mSubType;

		public int mColorVampShotType;

		public int mColorChangeTimer;

		public int mColorVampHealthIncPerHit;

		public int mIncMaxShotHealthAmt;

		public int mIncRetalMaxShotHealthAmt;

		public int mMaxShotIncCounter;

		public int mRetalShotIncCounter;

		public int mMinSpots;

		public int mMaxSpots;

		public int mMinSpotRad;

		public int mMaxSpotRad;

		public float mMinSpotFade;

		public float mMaxSpotFade;

		public int mInkTargetMode;

		public int mSpotFadeDelay;

		public int mBulletRadius;

		public bool mSinusoidalRetaliation;

		public bool mCanShootBullets;

		public bool mSineShotsTargetPlayer;

		public bool mColorVampire;

		public bool mAvoidColor;

		public bool mEnrageShieldRestore;

		public bool mBulletsUseSphereColl;

		public float mSpeed;

		public enum Move
		{
			Move_Default,
			Move_MirrorPlayer,
			Move_OppositePlayer
		}

		public enum ShotType
		{
			ShotType_Straight,
			ShotType_TargetedLinear,
			ShotType_Sine,
			ShotType_Homing,
			ShotType_Volcano,
			ShotType_Any
		}

		public enum SubType
		{
			SubType_SingleShot,
			SubType_MultiShot
		}
	}
}
