using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;

namespace BejeweledLivePlus
{
	public class ReplayData
	{
		public Serialiser mSaveBuffer = new Serialiser();

		public List<QueuedMove> mReplayMoves = new List<QueuedMove>();

		public ulong mTutorialFlags;

		public int mReplayTicks;

		public List<StateInfo> mStateInfoVector = new List<StateInfo>();
	}
}
