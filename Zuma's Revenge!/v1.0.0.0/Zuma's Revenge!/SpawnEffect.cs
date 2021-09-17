using System;
using SexyFramework.PIL;

namespace ZumasRevenge
{
	public class SpawnEffect
	{
		public SpawnEffect(bool create)
		{
			if (create)
			{
				this.mRings = new System(100, 50);
				this.mRings.mParticleScale2D = 0.3f;
				this.mRings.mScale = Common._S(1f);
				this.mRings.mHighWatermark = Common._M(80);
				this.mRings.mLowWatermark = Common._M(60);
				this.mRings.mFPSCallback = new System.FPSCallback(System.FadeParticlesFPSCallback);
				this.mRings.WaitForEmitters(true);
				this.mSwirl = new System(100, 50);
				this.mSwirl.mHighWatermark = Common._M(80);
				this.mSwirl.mLowWatermark = Common._M(60);
				this.mSwirl.mFPSCallback = new System.FPSCallback(System.FadeParticlesFPSCallback);
				this.mSwirl.mParticleScale2D = 0.3f;
				this.mSwirl.mScale = Common._S(1f);
				this.mSwirl.WaitForEmitters(true);
			}
		}

		public SpawnEffect()
			: this(true)
		{
		}

		public virtual void Dispose()
		{
			if (this.mRings != null)
			{
				this.mRings.Dispose();
				this.mRings = null;
			}
			if (this.mSwirl != null)
			{
				this.mSwirl.Dispose();
				this.mSwirl = null;
			}
		}

		public System mRings;

		public System mSwirl;
	}
}
