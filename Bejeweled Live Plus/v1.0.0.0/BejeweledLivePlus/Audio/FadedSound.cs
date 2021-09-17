using System;

namespace BejeweledLivePlus.Audio
{
	internal class FadedSound : UpdatedSound
	{
		public FadedSound(Sound inSound, float inFadeInSpeed, float inFadeOutSpeed)
		{
			this.mSound = inSound;
			this.mFadeInSpeed = inFadeInSpeed;
			this.mFadeOutSpeed = inFadeOutSpeed;
		}

		public override void Dispose()
		{
			this.mSound.Dispose();
		}

		public override void Play()
		{
			if (!this.mIsFree)
			{
				return;
			}
			this.mIsFree = false;
			this.mTargetVolume = this.mSound.GetVolume();
			this.mSound.Play();
		}

		public override void Fade()
		{
			this.mTargetVolume = 0f;
		}

		public override void Update()
		{
			float num = this.mSound.GetVolume();
			if (num == this.mTargetVolume)
			{
				return;
			}
			if (this.mTargetVolume == 0f)
			{
				num -= this.mFadeOutSpeed;
				if (num < 0f)
				{
					num = 0f;
				}
			}
			else
			{
				num += this.mFadeInSpeed;
				if (num > this.mTargetVolume)
				{
					num = this.mTargetVolume;
				}
			}
			if (num == 0f)
			{
				this.mIsFree = true;
				return;
			}
			this.mSound.SetVolume(num);
		}

		public override void Pause(bool inPauseOn)
		{
			if (inPauseOn)
			{
				this.CacheTargetVolume();
			}
			else
			{
				this.RestoreTargetVolume();
			}
			this.mSound.Pause(inPauseOn);
		}

		public override bool IsFree()
		{
			return this.mIsFree;
		}

		public override bool IsFading()
		{
			return this.mTargetVolume == 0f && this.mSound.GetVolume() > 0f;
		}

		public override bool IsLooping()
		{
			return this.mSound.IsLooping();
		}

		public override float GetVolume()
		{
			return this.mSound.GetVolume();
		}

		public override void SetPan(int inPan)
		{
			this.mSound.SetPan(inPan);
		}

		public override void SetPitch(float inPitch)
		{
			this.mSound.SetPitch(inPitch);
		}

		public override void SetVolume(float inVolume)
		{
			this.mSound.SetVolume(inVolume);
		}

		public override void EnableAutoUnload()
		{
			this.mSound.EnableAutoUnload();
		}

		protected void CacheTargetVolume()
		{
			if (this.mTargetVolume == 0f)
			{
				return;
			}
			this.mLastTarget = this.mTargetVolume;
			this.mTargetVolume = 0f;
		}

		protected void RestoreTargetVolume()
		{
			if (this.mLastTarget == -1f)
			{
				return;
			}
			this.mTargetVolume = this.mLastTarget;
			this.mLastTarget = -1f;
		}

		private bool mIsFree = true;

		private float mFadeInSpeed;

		private float mFadeOutSpeed;

		private float mTargetVolume;

		private float mLastTarget = -1f;
	}
}
