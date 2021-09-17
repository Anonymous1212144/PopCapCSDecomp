using System;

namespace ZumasRevenge
{
	public class RollerDigit
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mNum);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVY);
			sync.SyncLong(ref this.mDelay);
			sync.SyncLong(ref this.mBounceState);
			sync.SyncLong(ref this.mRestingY);
		}

		public int mNum = -1;

		public float mX;

		public float mY;

		public float mVY;

		public int mDelay;

		public int mBounceState;

		public int mRestingY;
	}
}
