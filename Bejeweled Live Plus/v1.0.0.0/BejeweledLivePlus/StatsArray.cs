using System;

namespace BejeweledLivePlus
{
	public class StatsArray
	{
		public StatsArray()
		{
			this.Clear();
		}

		public StatsArray(StatsArray obj)
		{
			for (int i = 0; i < this.mStats.Length; i++)
			{
				this.mStats[i] = obj.mStats[i];
			}
		}

		public void Clear()
		{
			for (int i = 0; i < this.mStats.Length; i++)
			{
				this.mStats[i] = 0;
			}
		}

		public void CopyFrom(StatsArray rhs)
		{
			for (int i = 0; i < this.mStats.Length; i++)
			{
				this.mStats[i] = rhs.mStats[i];
			}
		}

		public void CopyToArray(int[] dstArr)
		{
			if (dstArr != null)
			{
				for (int i = 0; i < this.mStats.Length; i++)
				{
					dstArr[i] = this.mStats[i];
				}
			}
		}

		public void CopyFromArray(int[] srcArr)
		{
			if (srcArr != null)
			{
				for (int i = 0; i < this.mStats.Length; i++)
				{
					this.mStats[i] = srcArr[i];
				}
			}
		}

		public int[] mStats = new int[40];
	}
}
