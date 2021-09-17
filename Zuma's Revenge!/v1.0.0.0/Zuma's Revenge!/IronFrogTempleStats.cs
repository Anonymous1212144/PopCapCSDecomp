using System;

namespace ZumasRevenge
{
	public class IronFrogTempleStats
	{
		public void CopyFrom(IronFrogTempleStats rhs)
		{
			this.mNumAttempts = rhs.mNumAttempts;
			this.mNumVictories = rhs.mNumVictories;
			this.mBestTime = rhs.mBestTime;
			this.mCurTime = rhs.mCurTime;
			this.mBestScore = rhs.mBestScore;
			this.mHighestLevel = rhs.mHighestLevel;
			for (int i = 0; i < 10; i++)
			{
				this.mLevelDeaths[i] = rhs.mLevelDeaths[i];
			}
			this.mTotalTimePlayed = rhs.mTotalTimePlayed;
		}

		public void Sync(DataSync theSync)
		{
			theSync.SyncLong(ref this.mNumAttempts);
			theSync.SyncLong(ref this.mNumVictories);
			theSync.SyncLong(ref this.mBestTime);
			theSync.SyncLong(ref this.mCurTime);
			theSync.SyncLong(ref this.mBestScore);
			theSync.SyncLong(ref this.mHighestLevel);
			for (int i = 0; i < 10; i++)
			{
				theSync.SyncLong(ref this.mLevelDeaths[i]);
			}
			theSync.SyncLong(ref this.mTotalTimePlayed);
		}

		public int mNumAttempts;

		public int mNumVictories;

		public int mBestTime;

		public int mCurTime;

		public int mBestScore;

		public int mHighestLevel;

		public int[] mLevelDeaths = new int[10];

		public int mTotalTimePlayed;
	}
}
