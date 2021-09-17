using System;
using System.Collections.Generic;

namespace BejeweledLivePlus
{
	public class GridData
	{
		public int GetRowCount()
		{
			return this.mTiles.Count / 8;
		}

		public void AddRow()
		{
			for (int i = 0; i < 8; i++)
			{
				this.mTiles.Add(new GridData.TileData());
			}
		}

		public GridData.TileData At(int theRow, int theCol)
		{
			int num = 8 * theRow + theCol;
			if (this.mTiles.Count <= num)
			{
				int num2 = num + 1 - this.mTiles.Count;
				for (int i = 0; i < num2; i++)
				{
					this.mTiles.Add(new GridData.TileData());
				}
			}
			return this.mTiles[num];
		}

		public List<GridData.TileData> mTiles = new List<GridData.TileData>();

		public class TileData
		{
			public TileData()
			{
				this.mBack = 0U;
				this.mAttr = '\0';
			}

			public uint mBack;

			public char mAttr;
		}
	}
}
