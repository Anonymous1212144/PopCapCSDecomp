using System;

namespace ZumasRevenge
{
	public class AdvModeTempleStats
	{
		public void CopyFrom(AdvModeTempleStats rhs)
		{
			this.mHighestLevel = rhs.mHighestLevel;
			this.mBestTime = rhs.mBestTime;
			this.mBestScore = rhs.mBestScore;
			this.mNumLevelsAced = rhs.mNumLevelsAced;
			this.mNumPerfectLevels = rhs.mNumPerfectLevels;
			this.mNumClearCurves = rhs.mNumClearCurves;
			for (int i = 0; i < 6; i++)
			{
				this.mBossDeaths[i] = rhs.mBossDeaths[i];
			}
			for (int j = 0; j < 60; j++)
			{
				this.mLevelDeaths[j] = rhs.mLevelDeaths[j];
			}
			this.mTotalTimePlayed = rhs.mTotalTimePlayed;
			this.mCurrentTime = rhs.mCurrentTime;
		}

		public void Sync(DataSync theSync)
		{
			theSync.SyncLong(ref this.mHighestLevel);
			theSync.SyncLong(ref this.mBestTime);
			theSync.SyncLong(ref this.mBestScore);
			theSync.SyncLong(ref this.mNumLevelsAced);
			theSync.SyncLong(ref this.mNumPerfectLevels);
			theSync.SyncLong(ref this.mNumClearCurves);
			for (int i = 0; i < 6; i++)
			{
				theSync.SyncLong(ref this.mBossDeaths[i]);
			}
			for (int j = 0; j < 60; j++)
			{
				theSync.SyncLong(ref this.mLevelDeaths[j]);
			}
			theSync.SyncLong(ref this.mTotalTimePlayed);
			theSync.SyncLong(ref this.mCurrentTime);
		}

		public int mHighestLevel;

		public int mBestTime = int.MaxValue;

		public int mBestScore;

		public int mNumLevelsAced;

		public int mNumPerfectLevels;

		public int mNumClearCurves;

		public int[] mBossDeaths = new int[6];

		public int[] mLevelDeaths = new int[60];

		public int mTotalTimePlayed;

		public int mCurrentTime;
	}
}
