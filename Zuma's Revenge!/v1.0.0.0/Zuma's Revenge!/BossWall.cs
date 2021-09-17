using System;

namespace ZumasRevenge
{
	public class BossWall
	{
		public BossWall()
		{
		}

		public BossWall(BossWall rhs)
		{
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mId = rhs.mId;
			this.mAlphaFadeDir = rhs.mAlphaFadeDir;
			this.mAlpha = rhs.mAlpha;
		}

		public int mX;

		public int mY;

		public int mWidth;

		public int mHeight;

		public int mId;

		public int mAlphaFadeDir;

		public int mAlpha;
	}
}
