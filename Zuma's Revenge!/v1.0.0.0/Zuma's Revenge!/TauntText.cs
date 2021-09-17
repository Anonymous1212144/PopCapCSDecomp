using System;

namespace ZumasRevenge
{
	public class TauntText
	{
		public void SyncState(DataSync sync)
		{
			sync.SyncLong(ref this.mTextId);
			sync.SyncLong(ref this.mMinDeaths);
			sync.SyncLong(ref this.mDelay);
			sync.SyncLong(ref this.mCondition);
			sync.SyncLong(ref this.mMinTime);
			sync.SyncLong(ref this.mUpdateCount);
			if (sync.isRead())
			{
				this.mText = TextManager.getInstance().getString(this.mTextId);
			}
		}

		public TauntText()
		{
		}

		public TauntText(TauntText rhs)
		{
			this.mText = rhs.mText;
			this.mMinDeaths = rhs.mMinDeaths;
			this.mDelay = rhs.mDelay;
			this.mCondition = rhs.mCondition;
			this.mMinTime = rhs.mMinTime;
			this.mUpdateCount = rhs.mUpdateCount;
		}

		public string mText;

		public int mTextId = -1;

		public int mMinDeaths = -1;

		public int mDelay = 100;

		public int mCondition = -1;

		public int mMinTime;

		public int mUpdateCount;
	}
}
