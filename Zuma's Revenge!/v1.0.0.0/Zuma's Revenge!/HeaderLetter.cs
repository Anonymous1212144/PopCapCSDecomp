using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class HeaderLetter
	{
		public HeaderLetter(Image img)
		{
			this.mImage = img;
		}

		public HeaderLetter()
		{
			this.mImage = null;
		}

		public Image mImage;

		public float mAngle;

		public float mAngleInc;

		public float mVX;

		public float mVY;

		public float mX;

		public float mY;

		public float mAngleAccel;

		public bool mHinge;

		public int mSwingCount;

		public int mUpdateCount;
	}
}
