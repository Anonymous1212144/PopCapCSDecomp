using System;

namespace SexyFramework.Graphics
{
	public class CharDataHashEntry
	{
		public CharDataHashEntry()
		{
			this.mChar = 0;
			this.mDataIndex = ushort.MaxValue;
			this.mNext = uint.MaxValue;
		}

		public ushort mChar;

		public ushort mDataIndex;

		public uint mNext;
	}
}
