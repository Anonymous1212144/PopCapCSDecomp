using System;
using SexyFramework.Misc;

namespace SexyFramework.Widget
{
	public class PAObjectPos
	{
		public PAObjectPos()
		{
		}

		public PAObjectPos(PAObjectPos inObj)
		{
			this.mName = inObj.mName;
			this.mObjectNum = inObj.mObjectNum;
			this.mIsSprite = inObj.mIsSprite;
			this.mIsAdditive = inObj.mIsAdditive;
			this.mHasSrcRect = inObj.mHasSrcRect;
			this.mResNum = inObj.mResNum;
			this.mPreloadFrames = inObj.mPreloadFrames;
			this.mAnimFrameNum = inObj.mAnimFrameNum;
			this.mTimeScale = inObj.mTimeScale;
			this.mTransform.CopyFrom(inObj.mTransform);
			this.mSrcRect = inObj.mSrcRect;
			this.mColorInt = inObj.mColorInt;
		}

		public string mName;

		public int mObjectNum;

		public bool mIsSprite;

		public bool mIsAdditive;

		public bool mHasSrcRect;

		public byte mResNum;

		public int mPreloadFrames;

		public int mAnimFrameNum;

		public float mTimeScale;

		public PATransform mTransform = new PATransform(true);

		public Rect mSrcRect = default(Rect);

		public int mColorInt;
	}
}
