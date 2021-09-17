using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace SexyFramework.Widget
{
	public class PAObjectInst
	{
		public PAObjectInst()
		{
			this.mName = null;
			this.mSpriteInst = null;
			this.mPredrawCallback = true;
			this.mPostdrawCallback = true;
			this.mImagePredrawCallback = true;
			this.mIsBlending = false;
		}

		public string mName;

		public PASpriteInst mSpriteInst;

		public PATransform mBlendSrcTransform = new PATransform();

		public Color mBlendSrcColor = default(Color);

		public bool mIsBlending;

		public SexyTransform2D mTransform = new SexyTransform2D(false);

		public Color mColorMult = default(Color);

		public bool mPredrawCallback;

		public bool mImagePredrawCallback;

		public bool mPostdrawCallback;
	}
}
