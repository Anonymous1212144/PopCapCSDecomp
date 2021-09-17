using System;

namespace Bejeweled3
{
	public static class GlobalMembersPieceBej3
	{
		public static uint GetFlagBit(int theFlag)
		{
			return (uint)((uint)1L << theFlag);
		}
	}
}
