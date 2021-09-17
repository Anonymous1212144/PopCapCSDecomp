using System;
using SexyFramework.Sound;

namespace ZumasRevenge.Sound
{
	internal class BurstSound : BasicSound
	{
		public BurstSound(int inSoundID, SoundManager inSoundManager, bool inAutoRelease)
		{
			this.m_SoundID = inSoundID;
			this.m_SoundManager = inSoundManager;
			this.mAutoRelease = inAutoRelease;
		}

		public override void Dispose()
		{
		}

		public override void Play()
		{
			if (this.mPaused || !this.ReleaseInstance() || !this.FindFreeSoundInstance(ref this.mSoundInstance))
			{
				return;
			}
			this.SetAttributes(this.mSoundInstance);
			this.mSoundInstance.Play(false, this.mAutoRelease);
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

		private void SetAttributes(SoundInstance inInstance)
		{
			if (this.mPan != 0)
			{
				inInstance.SetPan(this.mPan);
			}
			inInstance.AdjustPitch((double)this.mPitch);
			inInstance.SetVolume(this.m_SoundManager.GetMasterVolume());
		}

		private bool ReleaseInstance()
		{
			if (this.mSoundInstance != null && !this.mAutoRelease)
			{
				if (this.mSoundInstance.IsPlaying())
				{
					return false;
				}
				this.mSoundInstance.Release();
				this.mSoundInstance = null;
			}
			return true;
		}

		public override void Fade()
		{
		}

		public override void Update()
		{
		}

		public override float GetOptionVolume()
		{
			return 0f;
		}

		public override void Pause(bool inPauseOn)
		{
			this.mPaused = inPauseOn;
		}

		public override bool IsFree()
		{
			return this.mAutoRelease || this.mSoundInstance == null || !this.mSoundInstance.IsPlaying();
		}

		public override bool IsFading()
		{
			return false;
		}

		public override bool IsLooping()
		{
			return false;
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
			this.mPan = inPan;
		}

		public override void SetPitch(float inPitch)
		{
			this.mPitch = inPitch;
		}

		public override void SetVolume(float inVolume)
		{
			this.mVolume = inVolume;
			this.m_SoundManager.SetVolume((double)this.mVolume);
		}

		public override void EnableAutoUnload()
		{
			this.mUnloadSource = true;
		}

		private SoundInstance mSoundInstance;

		private bool mAutoRelease;

		private bool mPaused;

		private bool mUnloadSource;

		private int mPan;

		private float mPitch;

		private float mVolume = 1f;
	}
}
