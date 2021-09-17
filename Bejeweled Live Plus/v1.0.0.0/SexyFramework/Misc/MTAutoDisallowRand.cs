using System;

namespace SexyFramework.Misc
{
	public class MTAutoDisallowRand
	{
		public MTAutoDisallowRand()
		{
			MTRand.SetRandAllowed(false);
		}

		public void Dispose()
		{
			MTRand.SetRandAllowed(true);
		}
	}
}
