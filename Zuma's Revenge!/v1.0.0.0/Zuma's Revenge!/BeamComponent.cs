using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class BeamComponent
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mV0);
			sync.SyncFloat(ref this.mDistTraveled);
			sync.SyncBoolean(ref this.mAdditive);
			sync.SyncLong(ref this.mAlphaDelta);
			sync.SyncLong(ref this.mMinAlpha);
			sync.SyncLong(ref this.mCel);
			sync.SyncLong(ref this.mColor.mAlpha);
			sync.SyncLong(ref this.mColor.mRed);
			sync.SyncLong(ref this.mColor.mGreen);
			sync.SyncLong(ref this.mColor.mBlue);
		}

		public MemoryImage mImage;

		public float mX;

		public float mY;

		public float mVX;

		public float mVY;

		public float mV0;

		public float mDistTraveled;

		public bool mAdditive;

		public int mAlphaDelta;

		public int mMinAlpha;

		public int mCel;

		public Color mColor = default(Color);
	}
}
