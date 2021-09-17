using System;
using SexyFramework.Sound;

namespace BejeweledLivePlus
{
	public class SoundPlayConditionWait : SoundPlayCondition
	{
		public SoundInstance WaitInstance { get; set; }

		public SoundPlayConditionWait()
		{
			this.WaitInstance = null;
		}

		public SoundPlayConditionWait(int waitId)
		{
			this.WaitInstance = GlobalMembers.gApp.mSoundManager.GetExistSoundInstance(waitId);
		}

		public override void update()
		{
		}

		public override bool shouldActivate()
		{
			bool result = true;
			SoundInstance waitInstance = this.WaitInstance;
			if (waitInstance != null && waitInstance.IsPlaying())
			{
				result = false;
			}
			return result;
		}
	}
}
