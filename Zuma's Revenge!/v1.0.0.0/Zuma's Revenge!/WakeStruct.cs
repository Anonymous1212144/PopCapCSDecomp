using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class WakeStruct
	{
		public uint mBallId;

		public SexyVector2 mVel = default(SexyVector2);

		public float mX;

		public float mY;

		public float mAngle;

		public float mSize = 1f;

		public float mAlpha = 255f;

		public float mAlphaInc;

		public bool mAdditive;

		public bool mExpanding;

		public bool mIsHead;

		public int mUpdateCount;

		public Image mImage;
	}
}
