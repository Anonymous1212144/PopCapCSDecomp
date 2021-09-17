using System;

namespace BejeweledLIVE
{
	public class MTRand
	{
		internal void SRand(uint aSeed)
		{
			this.rand = new Random(DateTime.Now.Millisecond);
		}

		internal void SRand(int aSeed)
		{
			this.rand = new Random(DateTime.Now.Millisecond);
		}

		internal int Next()
		{
			return this.rand.Next();
		}

		internal int Next(int max)
		{
			return this.rand.Next(max);
		}

		private Random rand = new Random(DateTime.Now.Millisecond);
	}
}
