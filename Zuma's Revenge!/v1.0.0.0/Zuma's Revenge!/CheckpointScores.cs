using System;

namespace ZumasRevenge
{
	public class CheckpointScores
	{
		public void CopyFrom(CheckpointScores rhs)
		{
			this.mZoneStart = rhs.mZoneStart;
			this.mMidpoint = rhs.mMidpoint;
			this.mBoss = rhs.mBoss;
		}

		public int mZoneStart;

		public int mMidpoint;

		public int mBoss;
	}
}
