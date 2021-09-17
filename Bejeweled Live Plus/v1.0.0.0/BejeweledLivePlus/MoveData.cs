using System;
using BejeweledLivePlus.Misc;

namespace BejeweledLivePlus
{
	public class MoveData
	{
		public MoveData()
		{
			this.mUpdateCnt = 0;
			this.mSelectedId = -1;
			this.mSwappedRow = 0;
			this.mSwappedCol = 0;
			this.mPartOfReplay = false;
		}

		public void CopyFrom(MoveData data)
		{
			this.mUpdateCnt = data.mUpdateCnt;
			this.mSelectedId = data.mSelectedId;
			this.mSwappedRow = data.mSwappedRow;
			this.mSwappedCol = data.mSwappedCol;
			this.mPartOfReplay = data.mPartOfReplay;
			this.mMoveCreditId = data.mMoveCreditId;
			this.mPreSaveBuffer.Copyfrom(data.mPreSaveBuffer);
			for (int i = 0; i < 40; i++)
			{
				this.mStats[i] = data.mStats[i];
			}
		}

		public int mUpdateCnt;

		public int mSelectedId;

		public int mSwappedRow;

		public int mSwappedCol;

		public Serialiser mPreSaveBuffer = new Serialiser();

		public bool mPartOfReplay;

		public int mMoveCreditId;

		public int[] mStats = new int[40];
	}
}
