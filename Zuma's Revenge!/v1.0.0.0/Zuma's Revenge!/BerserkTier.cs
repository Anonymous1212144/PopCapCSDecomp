using System;
using System.Collections.Generic;

namespace ZumasRevenge
{
	public class BerserkTier
	{
		public BerserkTier()
		{
			this.mHealthLimit = 0;
			this.mParams = new List<BerserkModifier>();
		}

		public BerserkTier(int hl)
		{
			this.mHealthLimit = hl;
			this.mParams = new List<BerserkModifier>();
		}

		public BerserkTier(BerserkTier rhs)
		{
			this.mHealthLimit = rhs.mHealthLimit;
			this.mParams = new List<BerserkModifier>();
			for (int i = 0; i < rhs.mParams.Count; i++)
			{
				this.mParams.Add(new BerserkModifier(rhs.mParams[i]));
			}
		}

		public int mHealthLimit;

		public List<BerserkModifier> mParams;
	}
}
