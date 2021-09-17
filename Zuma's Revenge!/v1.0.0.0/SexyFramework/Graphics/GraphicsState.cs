using System;
using System.Collections.Generic;
using SexyFramework.Misc;

namespace SexyFramework.Graphics
{
	public class GraphicsState
	{
		public void CopyStateFrom(GraphicsState theState)
		{
			this.mDestImage = theState.mDestImage;
			this.mTransX = theState.mTransX;
			this.mTransY = theState.mTransY;
			this.mClipRect = theState.mClipRect;
			this.mFont = theState.mFont;
			this.mPushedColorVector = theState.mPushedColorVector;
			this.mColor = theState.mColor;
			this.mFinalColor = theState.mFinalColor;
			this.mDrawMode = theState.mDrawMode;
			this.mColorizeImages = theState.mColorizeImages;
			this.mFastStretch = theState.mFastStretch;
			this.mWriteColoredString = theState.mWriteColoredString;
			this.mLinearBlend = theState.mLinearBlend;
			this.mScaleX = theState.mScaleX;
			this.mScaleY = theState.mScaleY;
			this.mScaleOrigX = theState.mScaleOrigX;
			this.mScaleOrigY = theState.mScaleOrigY;
			this.mIs3D = theState.mIs3D;
		}

		public static Image mStaticImage = new Image();

		protected static Point[] mPFPoints = null;

		public Image mDestImage;

		public float mTransX;

		public float mTransY;

		public float mScaleX;

		public float mScaleY;

		public float mScaleOrigX;

		public float mScaleOrigY;

		public Rect mClipRect = default(Rect);

		public Rect mDestRect = default(Rect);

		public Rect mSrcRect = default(Rect);

		public List<Color> mPushedColorVector = new List<Color>();

		public Color mFinalColor = default(Color);

		public Color mColor = default(Color);

		public Font mFont;

		public int mDrawMode;

		public bool mColorizeImages;

		public bool mFastStretch;

		public bool mWriteColoredString;

		public bool mLinearBlend;

		public bool mIs3D;
	}
}
