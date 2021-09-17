using System;

namespace ZumasRevenge
{
	public struct ScoreTip
	{
		public ScoreTip(string t, int l)
		{
			this.mTip = t;
			this.mMinLevel = l;
			this.mTipId = -1;
		}

		public ScoreTip(string t)
		{
			this.mTip = t;
			this.mMinLevel = -1;
			this.mTipId = -1;
		}

		public string mTip;

		public int mTipId;

		public int mMinLevel;
	}
}
