using System;

namespace ZumasRevenge
{
	public class CurveDesc
	{
		public CurveDesc()
		{
			this.mVals = new BasicCurveVals();
			this.mVals.mSpeed = 0.5f;
			this.mStartDistance = 40;
			this.mVals.mNumColors = 4;
			this.mVals.mNumBalls = 0;
			this.mVals.mBallRepeat = 40;
			this.mVals.mMaxSingle = 10;
			this.mMergeSpeed = Common._M(0.025f);
			this.mDangerDistance = 600;
			this.mVals.mAccelerationRate = 0f;
			this.mVals.mMaxSpeed = 100f;
			this.mVals.mScoreTarget = 1000;
			this.mVals.mSkullRotation = -1;
			this.mCurAcceleration = 0f;
			this.mCutoffPoint = Common._M(17);
			for (int i = 0; i < 14; i++)
			{
				this.mVals.mPowerUpFreq[i] = 5250;
				this.mVals.mMaxNumPowerUps[i] = int.MaxValue;
			}
			this.mVals.mPowerUpChance = 100;
			this.mVals.mSlowFactor = 4f;
			this.mVals.mSlowDistance = 500;
			this.mVals.mZumaBack = 300;
			this.mVals.mZumaSlow = 1100;
			this.mVals.mDrawPit = true;
			this.mVals.mDrawTunnels = true;
			this.mVals.mDestroyAll = true;
			this.mVals.mDieAtEnd = true;
		}

		public CurveDesc(CurveDesc rhs)
		{
			if (rhs == null)
			{
				return;
			}
			this.mCurAcceleration = rhs.mCurAcceleration;
			this.mCutoffPoint = rhs.mCutoffPoint;
			this.mPath = rhs.mPath;
			this.mMergeSpeed = rhs.mMergeSpeed;
			this.mDangerDistance = rhs.mDangerDistance;
			this.mStartDistance = rhs.mStartDistance;
			this.mVals = new BasicCurveVals();
			this.mVals.mSpeed = rhs.mVals.mSpeed;
			this.mVals.mNumColors = rhs.mVals.mNumColors;
			this.mVals.mNumBalls = rhs.mVals.mNumBalls;
			this.mVals.mBallRepeat = rhs.mVals.mBallRepeat;
			this.mVals.mMaxSingle = rhs.mVals.mMaxSingle;
			this.mVals.mAccelerationRate = rhs.mVals.mAccelerationRate;
			this.mVals.mMaxSpeed = rhs.mVals.mMaxSpeed;
			this.mVals.mScoreTarget = rhs.mVals.mScoreTarget;
			this.mVals.mSkullRotation = rhs.mVals.mSkullRotation;
			this.mVals.mStartDistance = rhs.mVals.mStartDistance;
			for (int i = 0; i < 14; i++)
			{
				this.mVals.mPowerUpFreq[i] = rhs.mVals.mPowerUpFreq[i];
				this.mVals.mMaxNumPowerUps[i] = rhs.mVals.mMaxNumPowerUps[i];
			}
			this.mVals.mPowerUpChance = rhs.mVals.mPowerUpChance;
			this.mVals.mSlowFactor = rhs.mVals.mSlowFactor;
			this.mVals.mSlowDistance = rhs.mVals.mSlowDistance;
			this.mVals.mZumaBack = rhs.mVals.mZumaBack;
			this.mVals.mZumaSlow = rhs.mVals.mZumaSlow;
			this.mVals.mDrawPit = rhs.mVals.mDrawPit;
			this.mVals.mDrawTunnels = rhs.mVals.mDrawTunnels;
			this.mVals.mDestroyAll = rhs.mVals.mDestroyAll;
			this.mVals.mDieAtEnd = rhs.mVals.mDieAtEnd;
			this.mVals.mMaxClumpSize = rhs.mVals.mMaxClumpSize;
			this.mVals.mOrgAccelerationRate = rhs.mVals.mOrgAccelerationRate;
			this.mVals.mOrgMaxSpeed = rhs.mVals.mOrgMaxSpeed;
		}

		public void GetValuesFrom(CurveData data)
		{
			this.mVals = new BasicCurveVals();
			this.mVals.mSpeed = data.mVals.mSpeed;
			this.mVals.mNumColors = data.mVals.mNumColors;
			this.mVals.mNumBalls = data.mVals.mNumBalls;
			this.mVals.mBallRepeat = data.mVals.mBallRepeat;
			this.mVals.mMaxSingle = data.mVals.mMaxSingle;
			this.mVals.mAccelerationRate = data.mVals.mAccelerationRate;
			this.mVals.mMaxSpeed = data.mVals.mMaxSpeed;
			this.mVals.mScoreTarget = data.mVals.mScoreTarget;
			this.mVals.mSkullRotation = data.mVals.mSkullRotation;
			this.mVals.mStartDistance = data.mVals.mStartDistance;
			for (int i = 0; i < 14; i++)
			{
				this.mVals.mPowerUpFreq[i] = data.mVals.mPowerUpFreq[i];
				this.mVals.mMaxNumPowerUps[i] = data.mVals.mMaxNumPowerUps[i];
			}
			this.mVals.mPowerUpChance = data.mVals.mPowerUpChance;
			this.mVals.mSlowFactor = data.mVals.mSlowFactor;
			this.mVals.mSlowDistance = data.mVals.mSlowDistance;
			this.mVals.mZumaBack = data.mVals.mZumaBack;
			this.mVals.mZumaSlow = data.mVals.mZumaSlow;
			this.mVals.mDrawPit = data.mVals.mDrawPit;
			this.mVals.mDrawTunnels = data.mVals.mDrawTunnels;
			this.mVals.mDestroyAll = data.mVals.mDestroyAll;
			this.mVals.mDieAtEnd = data.mVals.mDieAtEnd;
			this.mVals.mMaxClumpSize = data.mVals.mMaxClumpSize;
			this.mVals.mOrgAccelerationRate = data.mVals.mOrgAccelerationRate;
			this.mVals.mOrgMaxSpeed = data.mVals.mOrgMaxSpeed;
		}

		public string mPath;

		public BasicCurveVals mVals;

		public int mDangerDistance;

		public float mMergeSpeed;

		public float mCurAcceleration;

		public int mCutoffPoint;

		public int mStartDistance;
	}
}
