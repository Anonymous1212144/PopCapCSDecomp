using System;

namespace Sexy
{
	public class CProfiler
	{
		public void Dispose()
		{
		}

		public void AddProfile(int iID)
		{
		}

		public void RemoveProfile(int iID)
		{
		}

		public void StartProfile(int iID)
		{
		}

		public void StopProfile(int iID)
		{
		}

		public void DumpToFile()
		{
		}

		public enum ProfileState
		{
			PROFILE_DRAW_PIECE,
			PROFILE_DRAW_PIECES,
			PROFILE_DRAW_UI,
			PROFILE_DRAW_BOARD,
			PROFILE_DRAW_LIGHTNING,
			PROFILE_UPDATE_FINDSET,
			PROFILE_UPDATE_FALLING,
			PROFILE_UPDATE_SWAPPING,
			PROFILE_UPDATE_MOVEDATA,
			PROFILE_UPDATE_FLLBLANKS
		}
	}
}
