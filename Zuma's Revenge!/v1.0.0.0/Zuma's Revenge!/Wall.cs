using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class Wall
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mWidth);
			sync.SyncFloat(ref this.mHeight);
			sync.SyncLong(ref this.mStrength);
			sync.SyncLong(ref this.mOrgStrength);
			sync.SyncLong(ref this.mMinRespawnTimer);
			sync.SyncLong(ref this.mMaxRespawnTimer);
			sync.SyncLong(ref this.mCurRespawnTimer);
			sync.SyncLong(ref this.mMinLifeTimer);
			sync.SyncLong(ref this.mMaxLifeTimer);
			sync.SyncLong(ref this.mCurLifeTimer);
			sync.SyncLong(ref this.mId);
			sync.SyncLong(ref this.mColor.mRed);
			sync.SyncLong(ref this.mColor.mGreen);
			sync.SyncLong(ref this.mColor.mBlue);
			sync.SyncLong(ref this.mColor.mAlpha);
			sync.SyncLong(ref this.mUpdateCount);
			sync.SyncLong(ref this.mState);
			sync.SyncLong(ref this.mSize);
			sync.SyncLong(ref this.mMaxSize);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mExpCel);
			sync.SyncLong(ref this.mType);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncLong(ref this.mSpacing);
		}

		public void Update()
		{
			if (this.mCurLifeTimer > 0 && --this.mCurLifeTimer == 0)
			{
				this.mType = 0;
				this.mCurRespawnTimer = MathUtils.IntRange(this.mMinRespawnTimer, this.mMaxRespawnTimer);
				return;
			}
			if (this.mCurRespawnTimer > 0 && --this.mCurRespawnTimer == 0)
			{
				this.mType = 1;
				this.mCurLifeTimer = MathUtils.IntRange(this.mMinLifeTimer, this.mMaxLifeTimer);
			}
			if (this.mType == 0 && this.mCurRespawnTimer <= 0)
			{
				return;
			}
			this.mUpdateCount++;
			if (this.mVX != 0f)
			{
				this.mX += this.mVX;
			}
			if (this.mVY != 0f)
			{
				this.mY += this.mVY;
			}
		}

		public void Draw(Graphics g)
		{
			if (this.mStrength != 0)
			{
				int num = this.mType;
			}
		}

		public bool Hit()
		{
			return false;
		}

		public float mX;

		public float mY;

		public float mWidth;

		public float mHeight;

		public float mVX;

		public float mVY;

		public int mSpacing;

		public int mStrength;

		public int mOrgStrength;

		public int mMinRespawnTimer;

		public int mMaxRespawnTimer;

		public int mCurRespawnTimer;

		public int mMinLifeTimer;

		public int mMaxLifeTimer;

		public int mCurLifeTimer;

		public int mId;

		public int mUpdateCount;

		public int mState;

		public int mSize = 1;

		public int mMaxSize = 1;

		public Color mColor = default(Color);

		public Image mImage;

		public int mCel;

		public int mExpCel;

		public int mType = 1;
	}
}
