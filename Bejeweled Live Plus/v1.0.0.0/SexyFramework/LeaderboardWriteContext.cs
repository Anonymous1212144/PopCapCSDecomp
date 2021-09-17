using System;

namespace SexyFramework
{
	public abstract class LeaderboardWriteContext : IAsyncTask
	{
		public override void Dispose()
		{
			base.Dispose();
		}

		public abstract uint GetEstimatedRank();
	}
}
