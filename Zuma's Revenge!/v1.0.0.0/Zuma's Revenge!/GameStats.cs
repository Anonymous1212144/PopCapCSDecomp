using System;

namespace ZumasRevenge
{
	public class GameStats
	{
		public GameStats()
		{
			this.mTimePlayed = 0;
			this.mNumBallsCleared = 0;
			this.mNumGemsCleared = 0;
			this.mNumGaps = 0;
			this.mNumCombos = 0;
			this.mMaxCombo = -1;
			this.mMaxComboScore = 0;
			this.mMaxInARow = 0;
			this.mMaxInARowScore = 0;
			this.mDangerTimePlayed = 0;
			this.mTotalShots = (this.mNumMisses = 0);
		}

		public void Reset()
		{
			this.mTimePlayed = 0;
			this.mNumBallsCleared = 0;
			this.mNumGemsCleared = 0;
			this.mNumGaps = 0;
			this.mNumCombos = 0;
			this.mMaxCombo = -1;
			this.mMaxComboScore = 0;
			this.mMaxInARow = 0;
			this.mMaxInARowScore = 0;
			this.mDangerTimePlayed = 0;
			this.mTotalShots = (this.mNumMisses = 0);
		}

		public void Add(GameStats theStats)
		{
			this.mTimePlayed += theStats.mTimePlayed;
			this.mNumBallsCleared += theStats.mNumBallsCleared;
			this.mNumGemsCleared += theStats.mNumGemsCleared;
			this.mNumCombos += theStats.mNumCombos;
			this.mNumGaps += theStats.mNumGaps;
			if (theStats.mMaxCombo > this.mMaxCombo || (theStats.mMaxCombo == this.mMaxCombo && theStats.mMaxComboScore > this.mMaxComboScore))
			{
				this.mMaxCombo = theStats.mMaxCombo;
				this.mMaxComboScore = theStats.mMaxComboScore;
			}
			if (theStats.mMaxInARow > this.mMaxInARow)
			{
				this.mMaxInARow = theStats.mMaxInARow;
				this.mMaxInARowScore = theStats.mMaxInARowScore;
			}
		}

		public void SyncState(DataSync theSync)
		{
			theSync.SyncLong(ref this.mTimePlayed);
			theSync.SyncLong(ref this.mNumBallsCleared);
			theSync.SyncLong(ref this.mNumGemsCleared);
			theSync.SyncLong(ref this.mNumGaps);
			theSync.SyncLong(ref this.mNumCombos);
			theSync.SyncLong(ref this.mMaxCombo);
			theSync.SyncLong(ref this.mMaxComboScore);
			theSync.SyncLong(ref this.mMaxInARow);
			theSync.SyncLong(ref this.mMaxInARowScore);
			theSync.SyncLong(ref this.mDangerTimePlayed);
			theSync.SyncLong(ref this.mTotalShots);
			theSync.SyncLong(ref this.mNumMisses);
		}

		public int mTimePlayed;

		public int mDangerTimePlayed;

		public int mNumBallsCleared;

		public int mNumGemsCleared;

		public int mNumGaps;

		public int mNumCombos;

		public int mMaxCombo;

		public int mMaxComboScore;

		public int mMaxInARow;

		public int mMaxInARowScore;

		public int mTotalShots;

		public int mNumMisses;
	}
}
