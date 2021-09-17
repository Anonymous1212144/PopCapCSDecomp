using System;
using SexyFramework.Misc;

namespace SexyFramework.Graphics
{
	public class CharData
	{
		public CharData()
		{
			this.mKerningFirst = 0;
			this.mKerningCount = 0;
			this.mWidth = 0;
			this.mOrder = 0;
		}

		public Rect mImageRect = default(Rect);

		public Point mOffset = default(Point);

		public ushort mKerningFirst;

		public ushort mKerningCount;

		public int mWidth;

		public int mOrder;

		public int mHashEntryIndex;
	}
}
