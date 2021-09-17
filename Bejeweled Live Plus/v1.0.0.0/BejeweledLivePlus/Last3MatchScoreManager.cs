using System;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class Last3MatchScoreManager
	{
		public Last3MatchScoreManager()
		{
			this.mMatchCount = 0;
			for (int i = 0; i < 3; i++)
			{
				this.mScoreHistory[i] = 0;
			}
		}

		public void Update(int iLatestScore)
		{
			this.mScoreHistory[this.mMatchCount] = iLatestScore;
			if (this.mMatchCount < 2)
			{
				this.mMatchCount++;
				return;
			}
			this.mMatchCount = 0;
		}

		public int GetLowerScore()
		{
			int num = this.mScoreHistory[0];
			for (int i = 1; i < 3; i++)
			{
				if (this.mScoreHistory[i] < num)
				{
					num = this.mScoreHistory[i];
				}
			}
			return num;
		}

		public void Clear()
		{
			this.mMatchCount = 0;
			for (int i = 0; i < 3; i++)
			{
				this.mScoreHistory[i] = 0;
			}
		}

		public bool Save(Buffer theBuffer)
		{
			int num = this.mScoreHistory.Length;
			theBuffer.WriteInt32(this.mMatchCount);
			theBuffer.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				theBuffer.WriteInt32(this.mScoreHistory[i]);
			}
			return true;
		}

		public bool Load(Buffer theBuffer)
		{
			this.mMatchCount = theBuffer.ReadInt32();
			int num = theBuffer.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				this.mScoreHistory[i] = theBuffer.ReadInt32();
			}
			return true;
		}

		private int[] mScoreHistory = new int[3];

		private int mMatchCount;
	}
}
