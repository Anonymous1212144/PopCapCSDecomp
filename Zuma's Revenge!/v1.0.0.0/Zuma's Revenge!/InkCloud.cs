using System;

namespace ZumasRevenge
{
	public class InkCloud
	{
		public void SyncState(DataSync s)
		{
			s.SyncBoolean(ref this.mFadeIn);
			s.SyncFloat(ref this.mAlpha);
			s.SyncFloat(ref this.mSize);
			s.SyncFloat(ref this.mX);
			s.SyncFloat(ref this.mY);
		}

		public bool mFadeIn;

		public float mAlpha;

		public float mSize;

		public float mX;

		public float mY;
	}
}
