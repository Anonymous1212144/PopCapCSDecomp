using System;
using System.Collections.Generic;
using BejeweledLIVE;
using Sexy;

namespace Bejeweled3
{
	public class SwapData
	{
		public static void PreAllocateMemory()
		{
			for (int i = 0; i < 10; i++)
			{
				new SwapData().PrepareForReuse();
			}
		}

		public static SwapData GetNewSwapData()
		{
			if (SwapData.unusedObjects.Count > 0)
			{
				return SwapData.unusedObjects.Pop();
			}
			return new SwapData();
		}

		private SwapData()
		{
		}

		private void Reset()
		{
			this.mPiece1 = null;
			this.mPiece2 = null;
			this.mSwapDir = default(TPoint);
			this.mSwapPct.Reset();
			this.mGemScale.Reset();
			this.mForwardSwap = false;
			this.mIgnore = false;
			this.mForceSwap = false;
		}

		public void PrepareForReuse()
		{
			this.Reset();
			SwapData.unusedObjects.Push(this);
		}

		public PieceBejLive mPiece1;

		public PieceBejLive mPiece2;

		public TPoint mSwapDir = default(TPoint);

		public CurvedVal mSwapPct = CurvedVal.GetNewCurvedVal();

		public CurvedVal mGemScale = CurvedVal.GetNewCurvedVal();

		public bool mForwardSwap;

		public bool mIgnore;

		public bool mForceSwap;

		private static Stack<SwapData> unusedObjects = new Stack<SwapData>();
	}
}
