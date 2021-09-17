using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class EyeBullet
	{
		public bool Update(int x, int y, bool do_explosion)
		{
			if (this.mInitialAlpha < 255f)
			{
				this.mInitialAlpha += Common._M(15f);
			}
			this.mProjectile.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1f);
			this.mProjectile.mDrawTransform.Scale(num, num);
			this.mProjectile.mDrawTransform.Translate((float)Common._S(x + this.mXOff), (float)Common._S(y + this.mYOff));
			this.mProjectile.mColor.mAlpha = (int)this.mInitialAlpha;
			this.mProjectile.Update();
			if ((this.mSparks.mFrameNum < (float)this.mSparks.mLastFrameNum || this.mSparks.mCurNumParticles > 0) && (this.mSparks.mFrameNum > 0f || this.mSparkFirstFrame))
			{
				this.mSparkFirstFrame = false;
				this.mSparks.mDrawTransform.LoadIdentity();
				float num2 = GameApp.DownScaleNum(1f);
				this.mSparks.mDrawTransform.Scale(num2, num2);
				this.mSparks.mDrawTransform.Translate((float)Common._S(x + this.mXOff), (float)Common._S(y + this.mYOff));
				this.mSparks.Update();
			}
			if (do_explosion)
			{
				this.mExplosion.mDrawTransform.LoadIdentity();
				float num3 = GameApp.DownScaleNum(1f);
				this.mExplosion.mDrawTransform.Scale(num3, num3);
				this.mExplosion.mDrawTransform.Translate((float)Common._S(x + this.mXOff), (float)Common._S(y + this.mYOff));
				this.mExplosion.Update();
				if (this.mExplosion.mFrameNum > (float)this.mExplosion.mLastFrameNum)
				{
					return true;
				}
			}
			return false;
		}

		public void Draw(Graphics g, int alpha)
		{
			g.PushState();
			this.mProjectile.mColor.mAlpha = alpha;
			this.mProjectile.Draw(g);
			g.PopState();
			if (this.mSparks.mCurNumParticles > 0)
			{
				g.PushState();
				this.mSparks.mColor.mAlpha = alpha;
				this.mSparks.Draw(g);
				g.PopState();
			}
			if (this.mExplosion.mFrameNum > 0f)
			{
				g.PushState();
				this.mExplosion.mColor.mAlpha = alpha;
				this.mExplosion.Draw(g);
				g.PopState();
			}
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mXOff);
			sync.SyncLong(ref this.mYOff);
			sync.SyncFloat(ref this.mInitialAlpha);
			sync.SyncBoolean(ref this.mSparkFirstFrame);
			if (sync.isWrite())
			{
				Common.SerializePIEffect(this.mExplosion, sync);
				Common.SerializePIEffect(this.mProjectile, sync);
				Common.SerializePIEffect(this.mSparks, sync);
				return;
			}
			this.mExplosion = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJEXPLOSION").Duplicate();
			this.mProjectile = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJ").Duplicate();
			this.mSparks = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_STONEBOSSPROJSPARKS").Duplicate();
			Common.DeserializePIEffect(this.mExplosion, sync);
			Common.DeserializePIEffect(this.mProjectile, sync);
			Common.DeserializePIEffect(this.mSparks, sync);
		}

		public PIEffect mProjectile;

		public PIEffect mSparks;

		public PIEffect mExplosion;

		public bool mSparkFirstFrame;

		public float mInitialAlpha;

		public int mXOff;

		public int mYOff;
	}
}
