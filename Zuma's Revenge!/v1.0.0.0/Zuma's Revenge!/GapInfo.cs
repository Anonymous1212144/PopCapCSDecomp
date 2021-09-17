using System;

namespace ZumasRevenge
{
	public class GapInfo
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mCurve);
			sync.SyncLong(ref this.mDist);
			sync.SyncLong(ref this.mBallId);
		}

		public int mCurve;

		public int mDist;

		public int mBallId;
	}
}
