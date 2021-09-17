using System;

namespace ZumasRevenge
{
	public class EffectParams
	{
		public EffectParams()
		{
			this.mEffectIndex = -1;
		}

		public EffectParams(string k, string v, int i)
		{
			this.mKey = k;
			this.mValue = v;
			this.mEffectIndex = i;
		}

		public string mKey;

		public string mValue;

		public int mEffectIndex;
	}
}
