using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Widget
{
	public class Tooltip
	{
		public Point mRequestedPos = default(Point);

		public Point mOffsetPos = default(Point);

		public int mWidth;

		public int mHeight;

		public int mArrowDir;

		public string mHeaderText;

		public string mBodyText;

		public int mTimer;

		public float mAlphaPct;

		public bool mAppearing;

		public Font mFontTitle;

		public Font mFont;

		public Color mColor;
	}
}
