using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class AchievementText
	{
		public AchievementText()
		{
			this.mAlpha = 0f;
			this.mFadeIn = true;
			this.mX = 0;
			this.mY = 0;
			this.mUnlocked = false;
		}

		public string mHeaderStr = "";

		public string mValueStr = "";

		public string mDescStr = "";

		public string mPointStr = "";

		public float mAlpha;

		public bool mFadeIn;

		public int mX;

		public int mY;

		public Image mIcon;

		public bool mUnlocked;
	}
}
