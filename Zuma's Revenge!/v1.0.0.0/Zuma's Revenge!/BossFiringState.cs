using System;

namespace ZumasRevenge
{
	public class BossFiringState
	{
		public BossFiringState()
		{
		}

		public BossFiringState(BossFiringState rhs)
		{
			this.mState = rhs.mState;
			this.mPawYOffset = rhs.mPawYOffset;
			this.mSkullXOffset = rhs.mSkullXOffset;
			this.mSkullYOffset = rhs.mSkullYOffset;
			this.mSkullAngle = rhs.mSkullAngle;
			this.mHeadAngle = rhs.mHeadAngle;
			this.mSkullGrowPct = rhs.mSkullGrowPct;
			this.mTargetSkullAngle = rhs.mTargetSkullAngle;
			this.mSkullAngleInc = rhs.mSkullAngleInc;
			this.mSwipeFrame = rhs.mSwipeFrame;
			this.mTimer = rhs.mTimer;
			this.mStreaksAlpha = rhs.mStreaksAlpha;
			this.mBulletId = rhs.mBulletId;
		}

		public int mState;

		public float mPawYOffset;

		public float mSkullXOffset;

		public float mSkullYOffset;

		public float mSkullAngle;

		public float mHeadAngle;

		public float mSkullGrowPct = 1f;

		public float mTargetSkullAngle;

		public float mSkullAngleInc;

		public int mSwipeFrame;

		public int mTimer;

		public float mStreaksAlpha;

		public int mBulletId;
	}
}
