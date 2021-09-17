using System;
using System.IO;
using Microsoft.Xna.Framework.GamerServices;

namespace BejeweledLivePlus
{
	public class HighScoreEntryLive
	{
		public HighScoreEntryLive()
		{
			this.mRank = -1;
			this.mScore = -1;
			this.mName = string.Empty;
			this.mPicStream = null;
		}

		public void Init(LeaderboardEntry liveEntry)
		{
			try
			{
				this.mName = liveEntry.Gamer.Gamertag;
				this.mScore = liveEntry.Columns.GetValueInt32("BestScore");
			}
			catch (Exception)
			{
			}
		}

		private void GetProfileCallback(IAsyncResult result)
		{
		}

		public GamerProfile mProfile;

		public int mRank;

		public int mScore;

		public string mName;

		public Stream mPicStream;

		private object mGetProfileLock = new object();
	}
}
