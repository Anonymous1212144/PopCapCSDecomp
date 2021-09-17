using System;

namespace BejeweledLivePlus.Audio
{
	internal class StaggeredSound : UpdatedSound
	{
		public StaggeredSound(Sound inSound, int inStaggerTime)
		{
			this.mSound = inSound;
			this.mStaggerTime = inStaggerTime;
			this.mStaggerCount = inStaggerTime;
		}

		public override void Dispose()
		{
			this.mSound.Dispose();
		}

		public override void Play()
		{
			if (this.mStaggerCount < this.mStaggerTime)
			{
				return;
			}
			this.mStaggerCount = 0;
			this.mSound.Play();
		}

		public override void Fade()
		{
			this.mSound.Fade();
		}

		public override void Update()
		{
			this.mStaggerCount++;
		}

		public override void Pause(bool inPauseOn)
		{
			this.mSound.Pause(inPauseOn);
		}

		public override bool IsFree()
		{
			return this.mStaggerCount > this.mStaggerTime * 1000 && this.mSound.IsFree();
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

		private int mStaggerTime;

		private int mStaggerCount;
	}
}
