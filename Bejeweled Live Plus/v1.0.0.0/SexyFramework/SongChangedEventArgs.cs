using System;

namespace SexyFramework
{
	public class SongChangedEventArgs : EventArgs
	{
		public int songID;

		public bool loop;
	}
}
