using System;
using SexyFramework.Misc;

namespace SexyFramework.PIL
{
	public class PointKeyFrame
	{
		public PointKeyFrame(int f, Point s)
		{
			this.first = f;
			this.second = s;
		}

		public int first;

		public Point second;
	}
}
