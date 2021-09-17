using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class Steam
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncFloat(ref this.mAlpha);
			sync.SyncFloat(ref this.mAlphaDec);
			sync.SyncFloat(ref this.mAngle);
			sync.SyncFloat(ref this.mAngleInc);
			sync.SyncFloat(ref this.mXOff);
			sync.SyncFloat(ref this.mYOff);
			sync.SyncFloat(ref this.mSize);
			sync.SyncFloat(ref this.mVX);
			sync.SyncFloat(ref this.mVY);
			sync.SyncLong(ref this.mImgNum);
			if (sync.isRead())
			{
				this.mImage = ((this.mImage == null) ? Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG1) : Res.GetImageByID(ResID.IMAGE_BOSS_STONEHEAD_FOG2));
			}
		}

		public float mAlpha = 255f;

		public float mAlphaDec;

		public float mAngle;

		public float mAngleInc;

		public float mXOff;

		public float mYOff;

		public float mSize = 0.1f;

		public float mVX;

		public float mVY;

		public int mImgNum;

		public Image mImage;
	}
}
