using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class Critter
	{
		public float mInitVel;

		public float mVX;

		public float mVY;

		public float mAX;

		public float mAY;

		public int mTimer;

		public float mAngle;

		public float mTargetAngle;

		public float mAngleInc;

		public float mX;

		public float mY;

		public Image mImage;

		public int mCel;

		public int mState;

		public int mAnimRate;

		public float mAlpha;

		public bool mFadeOut;

		public float mSize;

		public int mRotateDelay;

		public int mUpdateCount;

		public CommonColorFader mFader = new CommonColorFader();
	}
}
