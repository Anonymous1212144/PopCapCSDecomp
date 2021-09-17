using System;
using Sexy;

namespace BejeweledLIVE
{
	public class Effect : IReusable
	{
		public static Effect GetNewEffect()
		{
			return Effect.reuseHelper.GetNew();
		}

		public void PrepareForReuse()
		{
			Effect.reuseHelper.PushOnToReuseStack(this);
		}

		public void Reset()
		{
			this.mType = Effect.EffectType.TYPE_EXPLOSION;
			this.mX = 0;
			this.mY = 0;
			this.mFrame = 0;
		}

		public Effect()
		{
			this.Reset();
		}

		public Effect.EffectType mType;

		public int mX;

		public int mY;

		public int mFrame;

		public int mDelay;

		private static ReusableObjectHelper<Effect> reuseHelper = new ReusableObjectHelper<Effect>();

		public enum EffectType
		{
			TYPE_EXPLOSION
		}
	}
}
