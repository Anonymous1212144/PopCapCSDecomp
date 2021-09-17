using System;
using SexyFramework.Sound;

namespace BejeweledLivePlus.Audio
{
	internal class LoopingSound : BasicSound
	{
		public LoopingSound(int inSoundID, SoundManager inSoundManager)
		{
			this.m_SoundID = inSoundID;
			this.m_SoundManager = inSoundManager;
		}

		public override void Dispose()
		{
		}

		public override void Play()
		{
			if (this.mSoundInstance != null || !this.FindFreeSoundInstance(ref this.mSoundInstance))
			{
				return;
			}
			this.mSoundInstance.SetVolume((double)this.GetVolume());
			this.mSoundInstance.Play(true, false);
		}

		protected override bool FindFreeSoundInstance(ref SoundInstance outInstance)
		{
			SoundInstance soundInstance = this.m_SoundManager.GetSoundInstance(this.m_SoundID);
			if (soundInstance != null)
			{
				outInstance = soundInstance;
			}
			return soundInstance != null;
		}

		public override void Fade()
		{
		}

		public override void Update()
		{
		}

		public override void Pause(bool inPauseOn)
		{
			this.mPaused = inPauseOn;
			if (this.mSoundInstance == null)
			{
				return;
			}
			if (this.mPaused)
			{
				this.mSoundInstance.Release();
				return;
			}
			if (this.FindFreeSoundInstance(ref this.mSoundInstance))
			{
				this.mSoundInstance.SetVolume((double)this.GetVolume());
				this.mSoundInstance.Play(true, false);
			}
		}

		public override bool IsFree()
		{
			return this.mSoundInstance == null;
		}

		public override bool IsFading()
		{
			return false;
		}

		public override bool IsLooping()
		{
			return this.mSoundInstance != null;
		}

		public override float GetVolume()
		{
			if (!this.mPaused)
			{
				return this.mVolume;
			}
			return 0f;
		}

		public override void SetPan(int inPan)
		{
		}

		public override void SetPitch(float inPitch)
		{
		}

		public override void SetVolume(float inVolume)
		{
			this.mVolume = inVolume;
			if (this.mSoundInstance != null)
			{
				this.mSoundInstance.SetVolume((double)inVolume);
			}
		}

		public override void EnableAutoUnload()
		{
			this.mUnloadSource = true;
		}

		private SoundInstance mSoundInstance;

		private bool mPaused;

		private bool mUnloadSource;

		private float mVolume = 1f;
	}
}
