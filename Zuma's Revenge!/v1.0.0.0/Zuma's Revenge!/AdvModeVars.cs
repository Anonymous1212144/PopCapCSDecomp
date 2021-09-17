using System;

namespace ZumasRevenge
{
	public class AdvModeVars
	{
		public AdvModeVars()
		{
			for (int i = 0; i < 6; i++)
			{
				this.mCheckpointScores[i] = new CheckpointScores();
			}
		}

		public void CopyFrom(AdvModeVars rhs)
		{
			this.mHighestZoneBeat = rhs.mHighestZoneBeat;
			this.mHighestLevelBeat = rhs.mHighestLevelBeat;
			for (int i = 0; i < 6; i++)
			{
				this.mFirstTimeInZone[i] = rhs.mFirstTimeInZone[i];
			}
			this.mNumDeathsCurLevel = rhs.mNumDeathsCurLevel;
			this.mNumZumasCurLevel = rhs.mNumZumasCurLevel;
			this.mPerfectZone = rhs.mPerfectZone;
			for (int j = 0; j < 6; j++)
			{
				this.mNumTimesZoneBeat[j] = rhs.mNumTimesZoneBeat[j];
			}
			this.mDDSTier = rhs.mDDSTier;
			this.mRestartDDSTier = rhs.mRestartDDSTier;
			this.mCurrentAdvScore = rhs.mCurrentAdvScore;
			this.mCurrentAdvLevel = rhs.mCurrentAdvLevel;
			this.mCurrentAdvZone = rhs.mCurrentAdvZone;
			this.mCurrentAdvLives = rhs.mCurrentAdvLives;
			for (int k = 0; k < 60; k++)
			{
				this.mBestLevelTime[k] = rhs.mBestLevelTime[k];
			}
			for (int l = 0; l < 6; l++)
			{
				this.mCheckpointScores[l].CopyFrom(rhs.mCheckpointScores[l]);
			}
		}

		public int mHighestZoneBeat;

		public int mHighestLevelBeat;

		public bool[] mFirstTimeInZone = new bool[6];

		public int mNumDeathsCurLevel;

		public int mNumZumasCurLevel;

		public bool mPerfectZone;

		public int[] mNumTimesZoneBeat = new int[6];

		public int mDDSTier;

		public int mRestartDDSTier;

		public int mCurrentAdvScore;

		public int mCurrentAdvLevel;

		public int mCurrentAdvZone;

		public int mCurrentAdvLives;

		public int[] mBestLevelTime = new int[60];

		public CheckpointScores[] mCheckpointScores = new CheckpointScores[6];
	}
}
