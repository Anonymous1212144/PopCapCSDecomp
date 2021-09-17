using System;

namespace ZumasRevenge
{
	public class ChallengeTempleStats
	{
		public void CopyFrom(ChallengeTempleStats rhs)
		{
			this.mHighestScore = rhs.mHighestScore;
			this.mNumTimesHitScoreTarget = rhs.mNumTimesHitScoreTarget;
			this.mHighestMult = rhs.mHighestMult;
			for (int i = 0; i < 70; i++)
			{
				this.mNumTimesPlayedCurve[i] = rhs.mNumTimesPlayedCurve[i];
			}
			this.mTotalTime = rhs.mTotalTime;
		}

		public void Sync(DataSync theSync)
		{
			theSync.SyncLong(ref this.mHighestScore);
			theSync.SyncLong(ref this.mNumTimesHitScoreTarget);
			theSync.SyncLong(ref this.mHighestMult);
			for (int i = 0; i < 70; i++)
			{
				theSync.SyncLong(ref this.mNumTimesPlayedCurve[i]);
			}
			theSync.SyncLong(ref this.mTotalTime);
		}

		public int mHighestScore;

		public int mNumTimesHitScoreTarget;

		public int mHighestMult;

		public int[] mNumTimesPlayedCurve = new int[70];

		public int mTotalTime;
	}
}
