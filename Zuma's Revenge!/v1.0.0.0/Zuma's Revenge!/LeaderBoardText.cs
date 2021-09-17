using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class LeaderBoardText
	{
		public LeaderBoardText()
		{
			this.mAlpha = 0f;
			this.mFadeIn = true;
			this.mX = 0;
			this.mY = 0;
		}

		public string mHeaderStr = "";

		public string mValueStr = "";

		public float mAlpha;

		public bool mFadeIn;

		public int mX;

		public int mY;

		public Image mIcon;

		public bool mShowIcon;
	}
}
