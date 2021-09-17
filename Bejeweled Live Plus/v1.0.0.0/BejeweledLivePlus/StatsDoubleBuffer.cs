using System;

namespace BejeweledLivePlus
{
	public class StatsDoubleBuffer
	{
		public StatsDoubleBuffer()
		{
			this.mBufferStats[0] = new StatsArray();
			this.mBufferStats[1] = new StatsArray();
			this.m_index = 0U;
		}

		public void Swap()
		{
			this.Swap(false);
		}

		public void Swap(bool swapContents)
		{
			if (swapContents)
			{
				uint num = ((this.m_index == 0U) ? 1U : 0U);
				this.mBufferStats[(int)((UIntPtr)num)].CopyFrom(this.mBufferStats[(int)((UIntPtr)this.m_index)]);
			}
			this.m_index = ((this.m_index == 0U) ? 1U : 0U);
		}

		public void Clear()
		{
			this.mBufferStats[(int)((UIntPtr)this.m_index)].Clear();
		}

		public bool Clear(uint i)
		{
			if (i < 2U)
			{
				this.mBufferStats[(int)((UIntPtr)i)].Clear();
				return true;
			}
			return false;
		}

		public void ClearAll()
		{
			this.mBufferStats[0].Clear();
			this.mBufferStats[1].Clear();
		}

		public int this[int index]
		{
			get
			{
				return this.mBufferStats[(int)((UIntPtr)this.m_index)].mStats[index];
			}
			set
			{
				this.mBufferStats[(int)((UIntPtr)this.m_index)].mStats[index] = value;
			}
		}

		public void CopyTo(int[] dstArr)
		{
			this.mBufferStats[(int)((UIntPtr)this.m_index)].CopyToArray(dstArr);
		}

		public void CopyFrom(int[] srcArr)
		{
			this.mBufferStats[(int)((UIntPtr)this.m_index)].CopyFromArray(srcArr);
		}

		public int MaxStat(int i)
		{
			return Math.Max(this.mBufferStats[0].mStats[i], this.mBufferStats[1].mStats[i]);
		}

		private StatsArray[] mBufferStats = new StatsArray[2];

		private uint m_index;
	}
}
