using System;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class ElectroBolt
	{
		public float mAngStart;

		public float mAngStartD;

		public float mAngEnd;

		public float mAngEndD;

		public bool mCrossover;

		public bool mHitOtherGem;

		public int mHitOtherGemId;

		public float[] mMidPointsPos = new float[8];

		public float[] mMidPointsPosD = new float[8];

		public int mNumMidPoints;
	}
}
