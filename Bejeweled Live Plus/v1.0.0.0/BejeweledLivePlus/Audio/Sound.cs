using System;

namespace BejeweledLivePlus.Audio
{
	public abstract class Sound : IDisposable
	{
		public abstract void Dispose();

		public abstract void Play();

		public abstract void Fade();

		public abstract void Update();

		public abstract void Pause(bool inPauseOn);

		public abstract bool IsFree();

		public abstract bool IsFading();

		public abstract bool IsLooping();

		public abstract float GetVolume();

		public abstract void SetPan(int inPan);

		public abstract void SetPitch(float inPitch);

		public abstract void SetVolume(float inVolume);

		public abstract void EnableAutoUnload();
	}
}
