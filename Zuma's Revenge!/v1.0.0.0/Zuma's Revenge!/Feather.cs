using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class Feather
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mX);
			sync.SyncFloat(ref this.mY);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncFloat(ref this.mDecVX);
			sync.SyncFloat(ref this.mDecVY);
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncLong(ref this.mImgNum);
			if (sync.isRead())
			{
				this.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_FEATHER1 + (this.mImgNum - 1));
			}
			sync.SyncFloat(ref this.mAngleOsc.mVal);
			sync.SyncFloat(ref this.mAngleOsc.mMinVal);
			sync.SyncFloat(ref this.mAngleOsc.mMaxVal);
			sync.SyncFloat(ref this.mAngleOsc.mInc);
			sync.SyncFloat(ref this.mAngleOsc.mAccel);
			sync.SyncBoolean(ref this.mAngleOsc.mForward);
		}

		public Image mImage;

		public float mX;

		public float mY;

		public float mVX;

		public float mVY;

		public float mDecVX;

		public float mDecVY;

		public float mAlpha;

		public int mImgNum;

		public Oscillator mAngleOsc = new Oscillator();
	}
}
