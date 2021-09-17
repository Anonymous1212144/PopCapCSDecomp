using System;

namespace BejeweledLivePlus.Misc
{
	public class BitFlags
	{
		public BitFlags()
		{
			this.Clear();
		}

		public void Clear()
		{
			this.mFlags = 0U;
		}

		public void EnableAll()
		{
			this.mFlags = uint.MaxValue;
		}

		public bool IsBitSet(int theBit)
		{
			return (this.mFlags & (1U << theBit)) != 0U;
		}

		public void SetBit(int theBit)
		{
			this.mFlags |= 1U << theBit;
		}

		public void ClearBit(int theBit)
		{
			this.mFlags &= ~(1U << theBit);
		}

		public uint mFlags;
	}
}
