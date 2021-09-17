using System;
using System.Collections.Generic;

namespace SexyFramework.Graphics
{
	public class PITexture
	{
		public string mName;

		public string mFileName;

		public List<SharedImageRef> mImageVector = new List<SharedImageRef>();

		public SharedImageRef mImageStrip = new SharedImageRef();

		public int mNumCels;

		public bool mPadded;
	}
}
