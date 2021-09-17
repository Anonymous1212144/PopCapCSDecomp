using System;
using Sexy;

namespace BejeweledLIVE
{
	public class Light : IReusable
	{
		public static Light GetNewLight()
		{
			return Light.reuseHelper.GetNew();
		}

		public void PrepareForReuse()
		{
			Light.reuseHelper.PushOnToReuseStack(this);
		}

		public void Reset()
		{
		}

		public Light()
		{
			this.Reset();
		}

		public float mX;

		public float mY;

		public float mDiffDiv;

		public float mDistSub;

		public float mCurIntensity;

		public bool mCharging;

		public float mChargeRate;

		public float mDecayRate;

		private static ReusableObjectHelper<Light> reuseHelper = new ReusableObjectHelper<Light>();
	}
}
