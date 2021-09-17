using System;

namespace ZumasRevenge
{
	public class GauntletHSInfo
	{
		public GauntletHSInfo()
		{
		}

		public GauntletHSInfo(int score, string n)
		{
			this.mScore = score;
			this.mProfileName = n;
		}

		public int mScore;

		public string mProfileName = "";
	}
}
