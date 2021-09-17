using System;
using SexyFramework.Sound;

namespace SexyFramework
{
	public abstract class IAudioDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract bool InitAudioDriver();

		public abstract SoundManager CreateSoundManager();

		public abstract MusicInterface CreateMusicInterface();
	}
}
