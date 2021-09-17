using System;
using System.Collections.Generic;

namespace ZumasRevenge
{
	public class ImageSort : Comparer<FogElement>
	{
		public override int Compare(FogElement x, FogElement y)
		{
			if (x.mImage == y.mImage)
			{
				return 0;
			}
			return -1;
		}
	}
}
