using System;
using System.Collections.Generic;

namespace Bejeweled3
{
	public class QueuedMove
	{
		public static QueuedMove GetNewQueuedMove()
		{
			if (QueuedMove.unusedObjects.Count > 0)
			{
				return QueuedMove.unusedObjects.Pop();
			}
			return new QueuedMove();
		}

		public void PrepareForResue()
		{
			this.Reset();
			QueuedMove.unusedObjects.Push(this);
		}

		private QueuedMove()
		{
		}

		private void Reset()
		{
			this.mUpdateCnt = 0;
			this.mSelectedId = 0;
			this.mSwappedRow = 0;
			this.mSwappedCol = 0;
			this.mForceSwap = false;
			this.mPlayerSwapped = false;
		}

		public int mUpdateCnt;

		public int mSelectedId;

		public int mSwappedRow;

		public int mSwappedCol;

		public bool mForceSwap;

		public bool mPlayerSwapped;

		private static Stack<QueuedMove> unusedObjects = new Stack<QueuedMove>();
	}
}
