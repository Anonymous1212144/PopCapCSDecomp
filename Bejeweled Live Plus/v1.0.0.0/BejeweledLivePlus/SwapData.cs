using System;
using SexyFramework;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class SwapData
	{
		public Piece mPiece1;

		public Piece mPiece2;

		public Point mSwapDir = default(Point);

		public CurvedVal mSwapPct = new CurvedVal();

		public CurvedVal mGemScale = new CurvedVal();

		public bool mForwardSwap;

		public int mHoldingSwap;

		public bool mIgnore;

		public bool mForceSwap;

		public bool mDestroyTarget;
	}
}
