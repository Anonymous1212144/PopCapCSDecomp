using System;
using JeffLib;

namespace ZumasRevenge
{
	public class LTSmokeParticle
	{
		public AlphaFader mAlpha = new AlphaFader();

		public CommonColorFader mColorFader = new CommonColorFader();

		public float mSize;

		public float mVX;

		public float mVY;

		public float mX;

		public float mY;

		public bool mFadingIn;

		public int mAlphaFadeOutTime;
	}
}
