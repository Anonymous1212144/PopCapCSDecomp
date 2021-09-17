using System;
using System.Collections.Generic;
using Sexy;

namespace Bejeweled3
{
	public class ReplayData
	{
		public Buffer mSaveBuffer = new Buffer();

		public List<QueuedMove> mReplayMoves = new List<QueuedMove>();

		public List<StateInfo> mStateInfoVector = new List<StateInfo>();
	}
}
