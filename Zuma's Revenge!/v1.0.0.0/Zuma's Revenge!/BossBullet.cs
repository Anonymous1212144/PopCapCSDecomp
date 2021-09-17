using System;

namespace ZumasRevenge
{
	public class BossBullet : IDisposable
	{
		public BossBullet()
		{
			this.mDelay = (this.mBouncesLeft = (this.mUpdateCount = (this.mOffscreenPause = 0)));
			this.mGravity = (this.mTargetVX = (this.mTargetVY = 0f));
			this.mDeleteInstantly = false;
			this.mSize = 1f;
			this.mShotType = 0;
			this.mId = -1;
			this.mInitialSpeed = 0f;
			this.mVolcanoShot = (this.mHoming = false);
			this.mAmp = (this.mFreq = 0f);
			this.mSineMotion = false;
			this.mCanHitPlayer = true;
			this.mState = 0;
			this.mImageNum = 0;
			this.mAngle = 0f;
			this.mAlpha = 255f;
			this.mCel = 0;
			this.mData = null;
			this.mBossShoot = null;
		}

		public BossBullet(BossBullet rhs)
			: this()
		{
			if (rhs == null)
			{
				return;
			}
			this.mDelay = rhs.mDelay;
			this.mBouncesLeft = rhs.mBouncesLeft;
			this.mUpdateCount = rhs.mUpdateCount;
			this.mOffscreenPause = rhs.mOffscreenPause;
			this.mGravity = rhs.mGravity;
			this.mTargetVX = rhs.mTargetVX;
			this.mTargetVY = rhs.mTargetVY;
			this.mDeleteInstantly = rhs.mDeleteInstantly;
			this.mSize = rhs.mSize;
			this.mShotType = rhs.mShotType;
			this.mId = rhs.mId;
			this.mInitialSpeed = rhs.mInitialSpeed;
			this.mVolcanoShot = rhs.mVolcanoShot;
			this.mHoming = rhs.mHoming;
			this.mAmp = rhs.mAmp;
			this.mFreq = rhs.mFreq;
			this.mSineMotion = rhs.mSineMotion;
			this.mCanHitPlayer = rhs.mCanHitPlayer;
			this.mState = rhs.mState;
			this.mImageNum = rhs.mImageNum;
			this.mAngle = rhs.mAngle;
			this.mAlpha = rhs.mAlpha;
			this.mCel = rhs.mCel;
			this.mData = rhs.mData;
			this.mBossShoot = rhs.mBossShoot;
		}

		public virtual void Dispose()
		{
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mAmp);
			sync.SyncFloat(ref this.mFreq);
			sync.SyncBoolean(ref this.mSineMotion);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mDelay);
			sync.SyncLong(ref this.mState);
			sync.SyncLong(ref this.mImageNum);
			sync.SyncFloat(ref this.mAngle);
			sync.SyncBoolean(ref this.mHoming);
			sync.SyncFloat(ref this.mTargetVX);
			sync.SyncBoolean(ref this.mCanHitPlayer);
			sync.SyncFloat(ref this.mTargetVY);
			sync.SyncFloat(ref this.mInitialSpeed);
			sync.SyncLong(ref this.mOffscreenPause);
			sync.SyncBoolean(ref this.mVolcanoShot);
			sync.SyncFloat(ref this.mSize);
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncLong(ref this.mShotType);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mBouncesLeft);
			sync.SyncLong(ref this.mId);
		}

		public float mVX;

		public float mVY;

		public float mInitialSpeed;

		public float mTargetVX;

		public float mTargetVY;

		public float mX;

		public float mY;

		public float mAmp;

		public float mFreq;

		public float mGravity;

		public float mAngle;

		public float mSize;

		public float mAlpha;

		public bool mSineMotion;

		public bool mHoming;

		public bool mCanHitPlayer;

		public bool mDeleteInstantly;

		public int mBouncesLeft;

		public int mId;

		public int mUpdateCount;

		public int mDelay;

		public int mState;

		public int mImageNum;

		public int mOffscreenPause;

		public int mShotType;

		public int mCel;

		public bool mVolcanoShot;

		public object mData;

		public BossShoot mBossShoot;
	}
}
