using System;
using System.Collections.Generic;

namespace SexyFramework.PIL
{
	public class SortPointKeyFrames : Comparer<PointKeyFrame>
	{
		public override int Compare(PointKeyFrame x, PointKeyFrame y)
		{
			if (x.first < y.first)
			{
				return -1;
			}
			if (x.first > y.first)
			{
				return 1;
			}
			return 0;
		}
	}
}
