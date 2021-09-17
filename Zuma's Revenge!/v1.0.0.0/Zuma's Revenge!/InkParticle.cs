using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class InkParticle
	{
		public void SyncState(DataSync s)
		{
			Buffer buffer = s.GetBuffer();
			if (s.isRead())
			{
				buffer.WriteBoolean(this.mImage == Res.GetImageByID(ResID.IMAGE_BOSS_SQUID_GLOBULE1));
			}
			else if (buffer.ReadBoolean())
			{
				this.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_SQUID_GLOBULE1);
			}
			else
			{
				this.mImage = Res.GetImageByID(ResID.IMAGE_BOSS_SQUID_GLOBULE2);
			}
			s.SyncFloat(ref this.mWidthPct);
			s.SyncFloat(ref this.mHeightPct);
			s.SyncFloat(ref this.mX);
			s.SyncFloat(ref this.mY);
			s.SyncFloat(ref this.mAngle);
			s.SyncFloat(ref this.mInitSpeed);
			s.SyncFloat(ref this.mVX);
			s.SyncFloat(ref this.mVY);
			s.SyncFloat(ref this.mGravity);
			s.SyncFloat(ref this.mAlpha);
			s.SyncFloat(ref this.mAlphaRate);
			s.SyncFloat(ref this.mJiggleRate);
			s.SyncLong(ref this.mJiggleDir);
			s.SyncLong(ref this.mPostHitCount);
		}

		public float mWidthPct;

		public float mHeightPct;

		public float mAngle;

		public float mX;

		public float mY;

		public Image mImage;

		public float mInitSpeed;

		public float mVX;

		public float mVY;

		public float mGravity;

		public float mAlpha;

		public float mAlphaRate;

		public float mJiggleRate;

		public int mJiggleDir;

		public int mPostHitCount;
	}
}
