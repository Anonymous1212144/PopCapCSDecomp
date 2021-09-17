using System;
using SexyFramework;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class BallExplosion : IDisposable
	{
		public BallExplosion()
		{
			this.mPIEffect = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_BALL_EXPLODE).Duplicate();
			Common.SetFXNumScale(this.mPIEffect, GlobalMembers.gSexyAppBase.Is3DAccelerated() ? 1f : Common._M(0.3f));
		}

		public virtual void Dispose()
		{
			if (this.mPIEffect != null)
			{
				this.mPIEffect.Dispose();
			}
			this.mPIEffect = null;
		}

		public void Init()
		{
		}

		public void Release()
		{
		}

		public bool Update()
		{
			if (this.mPIEffect == null)
			{
				return true;
			}
			this.mPIEffect.Update();
			return !this.mPIEffect.IsActive();
		}

		public void Draw(Graphics g)
		{
			this.mPIEffect.Draw(g);
		}

		public void SetPos(int x, int y)
		{
			this.mPIEffect.mDrawTransform.LoadIdentity();
			float num = GameApp.DownScaleNum(1f);
			this.mPIEffect.mDrawTransform.Scale(num, num);
			this.mPIEffect.mDrawTransform.Translate((float)Common._S(x), (float)Common._S(y));
		}

		public PIEffect mPIEffect;
	}
}
