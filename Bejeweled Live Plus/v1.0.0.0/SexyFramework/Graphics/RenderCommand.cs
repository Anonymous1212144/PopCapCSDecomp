using System;

namespace SexyFramework.Graphics
{
	public class RenderCommand
	{
		public ActiveFontLayer mFontLayer;

		public int[] mDest = new int[2];

		public int[] mSrc = new int[4];

		public int mMode;

		public Color mColor = default(Color);

		public RenderCommand mNext;
	}
}
