using System;

namespace SexyFramework.Graphics
{
	public class RenderCommand : IDisposable
	{
		public RenderCommand()
		{
			this.mFontLayer = null;
		}

		public virtual void Dispose()
		{
			this.mFontLayer = null;
			this.mDest = null;
			this.mSrc = null;
		}

		public Color mColor = default(Color);

		public ActiveFontLayer mFontLayer;

		public int[] mDest = new int[2];

		public int[] mSrc = new int[4];

		public int mMode;
	}
}
