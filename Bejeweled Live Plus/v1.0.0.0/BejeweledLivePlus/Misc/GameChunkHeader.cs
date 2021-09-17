using System;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public class GameChunkHeader
	{
		public GameChunkHeader()
		{
			this.mMagic = 0;
			this.mHeaderSize = GameChunkHeader.size();
			this.mId = 0;
			this.mSize = 0;
			this.mOffset = 0;
		}

		public void CopyFrom(GameChunkHeader data)
		{
			this.mMagic = data.mMagic;
			this.mHeaderSize = data.mHeaderSize;
			this.mId = data.mId;
			this.mSize = data.mSize;
			this.mOffset = data.mOffset;
		}

		public static int size()
		{
			return 20;
		}

		public void write(Buffer buffer)
		{
			buffer.WriteInt32(this.mMagic);
			buffer.WriteInt32(this.mHeaderSize);
			buffer.WriteInt32(this.mId);
			buffer.WriteInt32(this.mSize);
			buffer.WriteInt32(this.mOffset);
		}

		public void read(Buffer buffer)
		{
			this.mMagic = buffer.ReadInt32();
			this.mHeaderSize = buffer.ReadInt32();
			this.mId = buffer.ReadInt32();
			this.mSize = buffer.ReadInt32();
			this.mOffset = buffer.ReadInt32();
		}

		public void zero()
		{
			this.mMagic = 0;
			this.mHeaderSize = 0;
			this.mId = 0;
			this.mSize = 0;
			this.mOffset = 0;
		}

		public int mMagic;

		public int mHeaderSize;

		public int mId;

		public int mSize;

		public int mOffset;
	}
}
