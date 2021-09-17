using System;

namespace BejeweledLivePlus.Audio
{
	internal struct Song
	{
		public Song(int inID, bool inLoop, float inFadeSpeed)
		{
			this.mID = inID;
			this.mLoop = inLoop;
			this.mFadeSpeed = inFadeSpeed;
		}

		public int mID;

		public bool mLoop;

		public float mFadeSpeed;

		public static Song DefaultSong = new Song(-1, false, 1f);
	}
}
