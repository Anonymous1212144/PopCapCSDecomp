using System;
using System.Collections.Generic;
using SexyFramework.Sound;

namespace BejeweledLivePlus.Audio
{
	public class SoundEffects : IDisposable
	{
		public SoundEffects(SoundManager soundManager)
		{
			this.m_SoundManager = soundManager;
			SoundFactory.SetSoundManager(soundManager);
		}

		public void Dispose()
		{
			this.StopAll();
		}

		public void Play(int inSoundID)
		{
			this.Play(inSoundID, new SoundAttribs());
		}

		public void Play(int inSoundID, SoundAttribs inAttribs)
		{
			Sound sound = null;
			if (this.mSounds.ContainsKey(inSoundID))
			{
				sound = this.mSounds[inSoundID];
			}
			else
			{
				this.mSounds.Add(inSoundID, null);
			}
			if (sound == null)
			{
				if (inAttribs.stagger > 0)
				{
					sound = SoundFactory.GetStaggeredSound(inSoundID, inAttribs.stagger);
				}
				else
				{
					sound = SoundFactory.GetSound(inSoundID, inAttribs.delay);
				}
				this.mSounds[inSoundID] = sound;
			}
			sound.Play();
		}

		public void Loop(int inSoundID)
		{
			this.Loop(inSoundID, new SoundAttribs());
		}

		public void Loop(int inSoundID, SoundAttribs inAttribs)
		{
			Sound sound = null;
			if (this.mSounds.ContainsKey(inSoundID))
			{
				sound = this.mSounds[inSoundID];
			}
			else
			{
				this.mSounds.Add(inSoundID, null);
			}
			if (sound == null)
			{
				sound = SoundFactory.GetLoopingSound(inSoundID, inAttribs.delay, inAttribs.fadein, inAttribs.fadeout);
				this.mSounds[inSoundID] = sound;
			}
			sound.Play();
		}

		public void Update()
		{
			bool flag = false;
			int[] array = new int[this.mSounds.Keys.Count];
			int num = 0;
			foreach (KeyValuePair<int, Sound> keyValuePair in this.mSounds)
			{
				Sound value = keyValuePair.Value;
				if (value.IsFree())
				{
					if (keyValuePair.Key == this.mChainedSound1)
					{
						flag = true;
					}
					array[num++] = keyValuePair.Key;
				}
				else
				{
					value.Update();
				}
			}
			for (int i = 0; i < num; i++)
			{
				this.mSounds.Remove(array[i]);
			}
			if (flag)
			{
				this.PlayNextInChain();
			}
		}

		internal bool IsLooping(int p)
		{
			return true;
		}

		internal void Stop(int inSoundID)
		{
			SoundInstance existSoundInstance = this.m_SoundManager.GetExistSoundInstance(inSoundID);
			if (existSoundInstance != null)
			{
				existSoundInstance.Release();
			}
			this.Stop(inSoundID, false);
		}

		internal void Stop(int inSoundID, bool inUnload)
		{
			Sound sound = null;
			if (!this.FindSound(inSoundID, ref sound))
			{
				return;
			}
			if (inUnload)
			{
				sound.EnableAutoUnload();
			}
			this.mSounds.Remove(inSoundID);
		}

		internal void StopAll()
		{
			if (this.m_SoundManager != null)
			{
				this.m_SoundManager.StopAllSounds();
			}
		}

		internal void Fade(int inSoundID, bool inUnload)
		{
			Sound sound = null;
			if (!this.FindSound(inSoundID, ref sound))
			{
				return;
			}
			if (inUnload)
			{
				sound.EnableAutoUnload();
			}
			sound.Fade();
		}

		internal void Fade(int inSoundID)
		{
			this.Fade(inSoundID, false);
		}

		internal void PauseLoopingSounds(bool p)
		{
		}

		internal void PlayChained(int p, int p_2, int aDelay)
		{
		}

		private bool FindSound(int inSoundID, ref Sound outSound)
		{
			if (this.mSounds.ContainsKey(inSoundID))
			{
				outSound = this.mSounds[inSoundID];
				return true;
			}
			return false;
		}

		private void PlayNextInChain()
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.delay = this.mChainedWait;
			this.Play(this.mChainedSound2, soundAttribs);
			this.mChainedSound1 = -1;
			this.mChainedSound2 = -1;
			this.mChainedWait = 0;
		}

		private SoundManager m_SoundManager;

		private Dictionary<int, Sound> mSounds = new Dictionary<int, Sound>();

		private int mChainedSound1 = -1;

		private int mChainedSound2 = -1;

		private int mChainedWait;
	}
}
