using System;
using System.Collections.Generic;
using SexyFramework.Graphics;

namespace SexyFramework.Widget
{
	public class PAImage
	{
		public List<SharedImageRef> mImages = new List<SharedImageRef>();

		public int mOrigWidth;

		public int mOrigHeight;

		public int mCols;

		public int mRows;

		public string mImageName;

		public int mDrawMode;

		public PATransform mTransform = new PATransform();
	}
}
