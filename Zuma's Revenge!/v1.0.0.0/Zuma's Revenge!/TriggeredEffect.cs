using System;
using SexyFramework.PIL;

namespace ZumasRevenge
{
	public class TriggeredEffect
	{
		public TriggeredEffect(bool create)
		{
			if (create)
			{
				this.mRings = new System(50, 50);
				this.mRings.mHighWatermark = Common._M(80);
				this.mRings.mLowWatermark = Common._M(60);
				this.mRings.mFPSCallback = new System.FPSCallback(System.FadeParticlesFPSCallback);
				this.mRings.mScale = Common._S(1f);
				this.mRings.WaitForEmitters(true);
				this.mRainbow = new System(50, 50);
				this.mRainbow.mHighWatermark = Common._M(80);
				this.mRainbow.mLowWatermark = Common._M(60);
				this.mRainbow.mFPSCallback = new System.FPSCallback(System.FadeParticlesFPSCallback);
				this.mRainbow.mScale = Common._S(1f);
				this.mRainbow.WaitForEmitters(true);
				this.mGas = new System(50, 50);
				this.mGas.mHighWatermark = Common._M(80);
				this.mGas.mLowWatermark = Common._M(60);
				this.mGas.mFPSCallback = new System.FPSCallback(System.FadeParticlesFPSCallback);
				this.mGas.mScale = Common._S(1f);
				this.mGas.WaitForEmitters(true);
				this.mFlare = new System(50, 50);
				this.mFlare.mHighWatermark = Common._M(80);
				this.mFlare.mLowWatermark = Common._M(60);
				this.mFlare.mFPSCallback = new System.FPSCallback(System.FadeParticlesFPSCallback);
				this.mFlare.mScale = Common._S(1f);
				this.mFlare.WaitForEmitters(true);
				this.mTrail = new System(150, 50);
				this.mTrail.mHighWatermark = Common._M(80);
				this.mTrail.mLowWatermark = Common._M(60);
				this.mTrail.mFPSCallback = new System.FPSCallback(System.FadeParticlesFPSCallback);
				this.mTrail.mScale = Common._S(1f);
				this.mTrail.WaitForEmitters(true);
			}
		}

		public TriggeredEffect()
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
			if (this.mRainbow != null)
			{
				this.mRainbow.Dispose();
				this.mRainbow = null;
			}
			if (this.mGas != null)
			{
				this.mGas.Dispose();
				this.mGas = null;
			}
			if (this.mFlare != null)
			{
				this.mFlare.Dispose();
				this.mFlare = null;
			}
			if (this.mTrail != null)
			{
				this.mTrail.Dispose();
				this.mTrail = null;
			}
		}

		public System mRings;

		public System mRainbow;

		public System mGas;

		public System mFlare;

		public System mTrail;
	}
}
