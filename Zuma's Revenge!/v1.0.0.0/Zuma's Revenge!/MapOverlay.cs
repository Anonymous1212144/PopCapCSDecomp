using System;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class MapOverlay
	{
		public float mAlpha;

		public bool mUnlocked;

		public FPoint[] mCloudPoints = new FPoint[]
		{
			new FPoint(),
			new FPoint(),
			new FPoint()
		};

		public FPoint[] mCloudSizes = new FPoint[]
		{
			new FPoint(),
			new FPoint(),
			new FPoint()
		};
	}
}
