using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class BossText
	{
		public BossText()
		{
		}

		public BossText(string t)
		{
			this.mAlpha = 0f;
			this.mText = t;
		}

		public string mText = "";

		public int mTextId = -1;

		public float mAlpha;

		public Color mColor = default(Color);
	}
}
