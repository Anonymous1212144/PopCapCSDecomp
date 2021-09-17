using System;

namespace BejeweledLivePlus
{
	public class SoundPlayConditionWaitUpdates : SoundPlayCondition
	{
		public int NumUpdates { get; set; }

		public SoundPlayConditionWaitUpdates()
		{
			this.NumUpdates = 0;
			this.updateElapsed = 0;
		}

		public SoundPlayConditionWaitUpdates(int numUpdates)
		{
			this.NumUpdates = numUpdates;
			this.updateElapsed = 0;
		}

		public override void update()
		{
			this.updateElapsed++;
		}

		public override bool shouldActivate()
		{
			return this.updateElapsed >= this.NumUpdates;
		}

		private int updateElapsed;
	}
}
