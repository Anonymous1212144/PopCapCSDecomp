using System;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public class SaveFileHeaderV101
	{
		public SaveFileHeaderV101()
		{
			this.mMagic = 0;
			this.mGameVersion = 0;
			this.mBoardVersion = 0;
			this.mPlatform = 0;
		}

		public void Copyfrom(SaveFileHeaderV101 data)
		{
			this.mMagic = data.mMagic;
			this.mGameVersion = data.mGameVersion;
			this.mBoardVersion = data.mBoardVersion;
			this.mPlatform = data.mPlatform;
		}

		public static int size()
		{
			return 16;
		}

		public void write(Buffer buffer)
		{
			buffer.WriteInt32(this.mMagic);
			buffer.WriteInt32(this.mGameVersion);
			buffer.WriteInt32(this.mBoardVersion);
			buffer.WriteInt32(this.mPlatform);
		}

		public void read(Buffer buffer)
		{
			this.mMagic = buffer.ReadInt32();
			this.mGameVersion = buffer.ReadInt32();
			this.mBoardVersion = buffer.ReadInt32();
			this.mPlatform = buffer.ReadInt32();
		}

		public int mMagic;

		public int mGameVersion;

		public int mBoardVersion;

		public int mPlatform;
	}
}
