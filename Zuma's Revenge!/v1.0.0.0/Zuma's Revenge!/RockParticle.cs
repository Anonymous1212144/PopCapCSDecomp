using System;

namespace ZumasRevenge
{
	public class RockParticle
	{
		public RockParticle()
		{
		}

		public RockParticle(RockParticle rhs)
		{
			this.mAlpha = rhs.mAlpha;
			this.mCel = rhs.mCel;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mVX = rhs.mVX;
			this.mVY = rhs.mVY;
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mCel);
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
		}

		public float mAlpha;

		public int mCel;

		public float mX;

		public float mY;

		public float mVX;

		public float mVY;
	}
}
