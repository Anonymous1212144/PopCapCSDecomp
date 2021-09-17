using System;

namespace BejeweledLivePlus.Audio
{
	internal class DelayedSound : UpdatedSound
	{
		public DelayedSound(Sound inSound, int inDelay)
		{
			this.mSound = inSound;
			this.mDelay = inDelay;
		}

		public override void Dispose()
		{
			this.mSound.Dispose();
		}

		public override void Play()
		{
			if (this.mUpdateCount > 0)
			{
				return;
			}
			this.mIsFree = false;
			this.mDoCountdown = true;
		}

		public override void Fade()
		{
			this.mSound.Fade();
		}

		public override void Update()
		{
			if (this.mDoCountdown)
			{
				this.mUpdateCount++;
			}
			if (this.mUpdateCount == this.mDelay)
			{
				this.mSound.Play();
				return;
			}
			if (this.mUpdateCount > this.mDelay)
			{
				this.mIsFree = true;
				this.mDoCountdown = false;
				this.mSound.Update();
			}
		}

		public override void Pause(bool inPauseOn)
		{
			if (this.mUpdateCount <= this.mDelay)
			{
				this.mDoCountdown = !inPauseOn;
			}
			this.mSound.Pause(inPauseOn);
		}

		public override bool IsFree()
		{
			return this.mIsFree && this.mSound.IsFree();
		}

		public override bool IsFading()
		{
			return this.mSound.IsFading();
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

		private bool mIsFree = true;

		private bool mDoCountdown;

		private int mDelay;

		private int mUpdateCount;
	}
}
