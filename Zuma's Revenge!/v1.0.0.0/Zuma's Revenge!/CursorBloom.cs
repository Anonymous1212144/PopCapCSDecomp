using System;

namespace ZumasRevenge
{
	public class CursorBloom
	{
		public void Reset()
		{
			this.mScale = 0f;
			this.mX = 0;
			this.mY = 0;
			this.mAlpha = 255;
		}

		public float mScale;

		public int mX;

		public int mY;

		public int mAlpha = 255;
	}
}
