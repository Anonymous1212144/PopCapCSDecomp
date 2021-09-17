using System;
using SexyFramework.Sound;

namespace BejeweledLivePlus.Audio
{
	public class SoundFactory
	{
		public static void SetSoundManager(SoundManager inSoundManager)
		{
			SoundFactory.m_SoundManager = inSoundManager;
		}

		public static Sound GetSound(int inSoundID, int inDelay)
		{
			return SoundFactory.GetSound(inSoundID, inDelay, true);
		}

		public static Sound GetSound(int inSoundID, int inDelay, bool inAutoRelease)
		{
			BurstSound burstSound = new BurstSound(inSoundID, SoundFactory.m_SoundManager, inAutoRelease);
			if (inDelay > 0)
			{
				return new DelayedSound(burstSound, inDelay);
			}
			return burstSound;
		}

		public static Sound GetStaggeredSound(int inSoundID, int inStaggerTime)
		{
			return new StaggeredSound(new BurstSound(inSoundID, SoundFactory.m_SoundManager, true), inStaggerTime);
		}

		public static Sound GetLoopingSound(int inSoundID, int inDelay, float inFadeInSpeed, float inFadeOutSpeed)
		{
			LoopingSound loopingSound = new LoopingSound(inSoundID, SoundFactory.m_SoundManager);
			if (inDelay > 0)
			{
				if (inFadeInSpeed < 1f || inFadeOutSpeed < 1f)
				{
					return new DelayedSound(new FadedSound(loopingSound, inFadeInSpeed, inFadeOutSpeed), inDelay);
				}
				return new DelayedSound(loopingSound, inDelay);
			}
			else
			{
				if (inFadeInSpeed < 1f || inFadeOutSpeed < 1f)
				{
					return new FadedSound(loopingSound, inFadeInSpeed, inFadeOutSpeed);
				}
				return loopingSound;
			}
		}

		private static SoundManager m_SoundManager;
	}
}
