using System;
using System.Collections.Generic;

namespace Bejeweled3
{
	public class MoveData
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 20; i++)
			{
				new MoveData().PrepareForReuse();
			}
		}

		public static MoveData GetNewMoveData()
		{
			if (MoveData.unusedObjects.Count > 0)
			{
				return MoveData.unusedObjects.Pop();
			}
			return new MoveData();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			MoveData.unusedObjects.Push(this);
		}

		private void Reset()
		{
			this.mUpdateCnt = 0;
			this.mSelectedId = -1;
			this.mSwappedRow = 0;
			this.mSwappedCol = 0;
			this.mPartOfReplay = false;
		}

		private MoveData()
		{
			this.Reset();
		}

		public int mUpdateCnt;

		public int mSelectedId;

		public int mSwappedRow;

		public int mSwappedCol;

		public bool mPartOfReplay;

		public int mMoveCreditId;

		public ulong[] mStats = new ulong[24];

		private static Stack<MoveData> unusedObjects = new Stack<MoveData>(20);
	}
}
