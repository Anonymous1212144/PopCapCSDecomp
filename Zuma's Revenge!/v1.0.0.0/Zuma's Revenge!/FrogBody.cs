using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class FrogBody
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mAlpha);
			sync.SyncLong(ref this.mCel);
		}

		public Image mShadow;

		public Image mLegs;

		public Image mMouth;

		public Image mBody;

		public Image mEyes;

		public Image mTongue;

		public Image mLazerEyeLoop;

		public Point mLegsOffset = new Point();

		public Point mMouthOffset = new Point();

		public Point mBodyOffset = new Point();

		public Point mEyesOffset = new Point();

		public FrogType mType;

		public int mTongueX;

		public int mCX;

		public int mCY;

		public int mNextBallX;

		public int mNextBallY;

		public int mAlpha;

		public int mCel;
	}
}
