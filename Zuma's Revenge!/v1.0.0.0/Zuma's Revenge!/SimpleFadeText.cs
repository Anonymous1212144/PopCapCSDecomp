using System;

namespace ZumasRevenge
{
	public class SimpleFadeText
	{
		public SimpleFadeText()
		{
			this.mAlpha = 0f;
			this.mFadeIn = true;
		}

		public SimpleFadeText(string str)
			: this()
		{
			this.mString = str;
		}

		public string mString;

		public float mAlpha;

		public bool mFadeIn;
	}
}
