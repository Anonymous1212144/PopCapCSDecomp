using System;

namespace ZumasRevenge
{
	public class SkeletonPowerOrb
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mSize);
			sync.SyncFloat(ref this.mAlpha);
		}

		public float mSize;

		public float mAlpha = 255f;
	}
}
