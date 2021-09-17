using System;

namespace ZumasRevenge
{
	public class Egg
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAngle);
			sync.SyncFloat(ref this.mSize);
		}

		public float mAngle = 1.570795f;

		public float mSize;
	}
}
