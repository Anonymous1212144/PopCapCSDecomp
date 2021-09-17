using System;

namespace ZumasRevenge
{
	public class RockChunk
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mCol);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mAlpha);
		}

		public int mCol;

		public float mX;

		public float mY;

		public float mVX;

		public float mVY;

		public float mAlpha;
	}
}
