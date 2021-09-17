using System;
using System.Collections.Generic;

namespace Bejeweled3
{
	public class EffectTypePair
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 1000; i++)
			{
				new EffectTypePair().PrepareForReuse();
			}
		}

		private EffectTypePair()
		{
		}

		public static EffectTypePair GetNewEffectTypePair(EffectBej3.EffectType effectType, EffectBej3 effect)
		{
			if (EffectTypePair.unusedObjects.Count > 0)
			{
				EffectTypePair effectTypePair = EffectTypePair.unusedObjects.Pop();
				effectTypePair.Reset(effectType, effect);
				return effectTypePair;
			}
			return new EffectTypePair(effectType, effect);
		}

		public void PrepareForReuse()
		{
			EffectTypePair.unusedObjects.Push(this);
		}

		public void Reset(EffectBej3.EffectType effectType, EffectBej3 effect)
		{
			this.Key = effectType;
			this.Value = effect;
		}

		private EffectTypePair(EffectBej3.EffectType effectType, EffectBej3 effect)
		{
			this.Reset(effectType, effect);
		}

		public EffectBej3.EffectType Key;

		public EffectBej3 Value;

		private static Stack<EffectTypePair> unusedObjects = new Stack<EffectTypePair>();
	}
}
