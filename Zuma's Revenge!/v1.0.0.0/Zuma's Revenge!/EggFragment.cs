using System;

namespace ZumasRevenge
{
	public class EggFragment
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mDecVX);
			sync.SyncFloat(ref this.mDecVY);
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncLong(ref this.mCol);
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
		}

		public float mVX;

		public float mVY;

		public float mDecVX;

		public float mDecVY;

		public float mAlpha;

		public int mCol;

		public float mX;

		public float mY;
	}
}
