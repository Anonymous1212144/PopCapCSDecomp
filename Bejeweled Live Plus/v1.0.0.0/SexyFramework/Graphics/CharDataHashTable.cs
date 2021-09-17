using System;
using System.Collections.Generic;

namespace SexyFramework.Graphics
{
	public class CharDataHashTable
	{
		protected int GetBucketIndex(char inChar)
		{
			if (this.mOrderedHash)
			{
				return (int)(inChar & 'Ͽ');
			}
			uint num = 3203386110U;
			num ^= (uint)inChar;
			num *= 1540483477U;
			num ^= num >> 13;
			num *= 1540483477U;
			num ^= num >> 15;
			return (int)(num & 1023U);
		}

		public CharDataHashTable()
		{
			this.mOrderedHash = false;
			for (int i = 0; i < 1024; i++)
			{
				this.mHashEntries.Add(new CharDataHashEntry());
			}
		}

		public CharData GetCharData(char inChar, bool inAllowAdd)
		{
			int num = this.GetBucketIndex(inChar);
			CharDataHashEntry charDataHashEntry = this.mHashEntries[num];
			if (charDataHashEntry.mChar == (ushort)inChar && charDataHashEntry.mDataIndex != 65535)
			{
				return this.mCharData[(int)charDataHashEntry.mDataIndex];
			}
			if (charDataHashEntry.mChar != 0)
			{
				while (charDataHashEntry.mChar != (ushort)inChar)
				{
					if (charDataHashEntry.mNext == 4294967295U)
					{
						if (!inAllowAdd)
						{
							return null;
						}
						charDataHashEntry.mNext = (uint)this.mHashEntries.Count;
						this.mHashEntries.Add(new CharDataHashEntry());
						charDataHashEntry = this.mHashEntries[num];
						CharDataHashEntry charDataHashEntry2 = this.mHashEntries[(int)charDataHashEntry.mNext];
						charDataHashEntry2.mChar = (ushort)inChar;
						charDataHashEntry2.mDataIndex = (ushort)this.mCharData.Count;
						this.mCharData.Add(new CharData());
						CharData charData = this.mCharData[(int)charDataHashEntry2.mDataIndex];
						charData.mHashEntryIndex = (int)charDataHashEntry.mNext;
						return charData;
					}
					else
					{
						num = (int)charDataHashEntry.mNext;
						charDataHashEntry = this.mHashEntries[num];
					}
				}
				return this.mCharData[(int)charDataHashEntry.mDataIndex];
			}
			if (!inAllowAdd)
			{
				return null;
			}
			charDataHashEntry.mChar = (ushort)inChar;
			charDataHashEntry.mDataIndex = (ushort)this.mCharData.Count;
			this.mCharData.Add(new CharData());
			CharData charData2 = this.mCharData[(int)charDataHashEntry.mDataIndex];
			charData2.mHashEntryIndex = num;
			return charData2;
		}

		public bool mOrderedHash;

		public List<CharData> mCharData = new List<CharData>();

		public List<CharDataHashEntry> mHashEntries = new List<CharDataHashEntry>(1024);

		public enum ECharDataHash
		{
			HASH_BITS = 10,
			HASH_BUCKET_COUNT = 1024,
			HASH_BUCKET_MASK = 1023
		}
	}
}
