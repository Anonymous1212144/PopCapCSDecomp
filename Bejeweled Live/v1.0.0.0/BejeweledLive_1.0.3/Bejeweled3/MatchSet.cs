using System;
using System.Collections.Generic;

namespace Bejeweled3
{
	public class MatchSet
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 5; i++)
			{
				new MatchSet().PrepareForReuse();
			}
		}

		public static MatchSet GetNewMatchSet()
		{
			if (MatchSet.unusedObjects.Count > 0)
			{
				return MatchSet.unusedObjects.Pop();
			}
			return new MatchSet();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			MatchSet.unusedObjects.Push(this);
		}

		private void Reset()
		{
			this.mPieces.Clear();
		}

		private MatchSet()
		{
			this.Reset();
		}

		public List<PieceBejLive> mPieces = new List<PieceBejLive>(8);

		public int mMatchId;

		public int mMoveCreditId;

		public int mExplosionCount;

		private static Stack<MatchSet> unusedObjects = new Stack<MatchSet>();
	}
}
