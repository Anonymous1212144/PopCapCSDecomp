using System;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class EndLevelExplosion : IDisposable
	{
		public EndLevelExplosion()
		{
			this.mPIEffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_END_LEVEL_EXPLOSION).Duplicate();
			Common.SetFXNumScale(this.mPIEffect, GlobalMembers.gSexyAppBase.Is3DAccelerated() ? 1f : Common._M(0.5f));
		}

		public virtual void Dispose()
		{
			if (this.mPIEffect != null)
			{
				this.mPIEffect.Dispose();
			}
			this.mPIEffect = null;
		}

		public void SetPos(int x, int y)
		{
			this.mPIEffect.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1f);
			this.mPIEffect.mDrawTransform.Scale(num, num);
			this.mPIEffect.mDrawTransform.Translate((float)Common._S(x), (float)Common._S(y));
		}

		public int mDelay;

		public int mX;

		public int mY;

		public PIEffect mPIEffect;
	}
}
