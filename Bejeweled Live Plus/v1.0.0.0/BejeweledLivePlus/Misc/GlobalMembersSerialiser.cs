using System;

namespace BejeweledLivePlus.Misc
{
	public static class GlobalMembersSerialiser
	{
		public static int SaveFileHeader_GetOffsetToFirstChunk(SaveFileHeader header)
		{
			return header.sizeofOldHeader() + header.mSize;
		}
	}
}
