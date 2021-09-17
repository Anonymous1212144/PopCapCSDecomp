using System;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public class SaveFileHeader
	{
		public SaveFileHeader()
		{
			this.mSize = SaveFileHeader.size() - SaveFileHeaderV101.size();
			this.mGameType = 0;
			this.mGameChunkCount = 0;
		}

		public void Copyfrom(SaveFileHeader data)
		{
			this.mOldHeader.Copyfrom(data.mOldHeader);
			this.mSize = data.mSize;
			this.mGameType = data.mGameType;
			this.mGameChunkCount = data.mGameChunkCount;
		}

		public static int size()
		{
			return SaveFileHeaderV101.size() + 12;
		}

		public int sizeofOldHeader()
		{
			return SaveFileHeaderV101.size();
		}

		public void write(Buffer buffer)
		{
			this.mOldHeader.write(buffer);
			buffer.WriteInt32(this.mSize);
			buffer.WriteInt32(this.mGameType);
			buffer.WriteInt32(this.mGameChunkCount);
		}

		public void read(Buffer buffer, SaveFileHeader.ReadContent content)
		{
			if (content == SaveFileHeader.ReadContent.SelfAndOldHeader)
			{
				this.mOldHeader.read(buffer);
			}
			this.mSize = buffer.ReadInt32();
			this.mGameType = buffer.ReadInt32();
			this.mGameChunkCount = buffer.ReadInt32();
		}

		public SaveFileHeaderV101 mOldHeader = new SaveFileHeaderV101();

		public int mSize;

		public int mGameType;

		public int mGameChunkCount;

		public enum ReadContent
		{
			Self,
			SelfAndOldHeader
		}
	}
}
