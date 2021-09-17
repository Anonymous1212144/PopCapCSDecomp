using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Sexy
{
	public class XNAMusicInterface : MusicInterface
	{
		public XNAMusicInterface(SexyAppBase theApp)
		{
			this.mEnabled = false;
			this.mContent = theApp.mContentManager;
			this.mCurrentSong = -1;
			this.mFadeOut = false;
			for (int i = 0; i < XNAMusicInterfaceConstants.MAX_SONGS; i++)
			{
				this.mSongs[i] = null;
			}
		}

		public override void Dispose()
		{
			this.UnloadAllMusic();
			base.Dispose();
		}

		public override void Enable(bool enable)
		{
			this.mEnabled = enable;
		}

		public override bool LoadMusic(int theSongId, string theFileName)
		{
			this.mSongs[theSongId] = this.mContent.Load<Song>(theFileName);
			return true;
		}

		public override void PlayMusic(int theSongid, int offset, float fadeOutSeconds, float fadeinSeconds, bool loop)
		{
			if (XNAMusicInterface.PlayingUserMusic)
			{
				return;
			}
			if (theSongid < 0)
			{
				return;
			}
			this.mCurrentSong = theSongid;
			Song song = this.mSongs[theSongid];
			if (song == null)
			{
				return;
			}
			try
			{
				MediaPlayer.IsRepeating = loop;
				MediaPlayer.Play(song);
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
		}

		public override void StopMusic(float fadeOutSeconds)
		{
			if (XNAMusicInterface.PlayingUserMusic)
			{
				return;
			}
			try
			{
				MediaPlayer.Stop();
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
		}

		public override void PauseMusic()
		{
			if (XNAMusicInterface.PlayingUserMusic)
			{
				return;
			}
			try
			{
				MediaPlayer.Pause();
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
		}

		public override void ResumeMusic()
		{
			if (XNAMusicInterface.PlayingUserMusic)
			{
				return;
			}
			if (this.mCurrentSong < 0)
			{
				return;
			}
			try
			{
				MediaPlayer.Resume();
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
		}

		public override void UnloadMusic(int theSongId)
		{
			if (theSongId == this.mCurrentSong)
			{
				try
				{
					if (!XNAMusicInterface.PlayingUserMusic)
					{
						MediaPlayer.Stop();
					}
				}
				catch (Exception)
				{
				}
			}
			if (this.mSongs[theSongId] != null)
			{
				this.mSongs[theSongId].Dispose();
				this.mSongs[theSongId] = null;
			}
		}

		public override void UnloadAllMusic()
		{
			try
			{
				if (!XNAMusicInterface.PlayingUserMusic)
				{
					MediaPlayer.Stop();
				}
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
			for (int i = 0; i < XNAMusicInterfaceConstants.MAX_SONGS; i++)
			{
				this.UnloadMusic(i);
			}
		}

		public override bool IsPlaying(int theSongId)
		{
			bool flag = false;
			try
			{
				flag = MediaPlayer.State == 1;
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
			return this.mCurrentSong == theSongId && flag;
		}

		public override void SetVolume(float theVolume)
		{
			try
			{
				MediaPlayer.Volume = theVolume;
				this.maxVolume = theVolume;
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
		}

		public override float GetVolume()
		{
			return this.maxVolume;
		}

		public override void Update()
		{
			try
			{
				if (this.mFadeOut && !XNAMusicInterface.PlayingUserMusic)
				{
					MediaPlayer.Stop();
				}
			}
			catch (Exception)
			{
				XNAMusicInterface.PlayingUserMusic = true;
			}
		}

		public override int GetFreeMusicId()
		{
			for (int i = 0; i < XNAMusicInterfaceConstants.MAX_SONGS; i++)
			{
				if (this.mSongs[i] == null)
				{
					return i;
				}
			}
			return -1;
		}

		private Song[] mSongs = new Song[5];

		private bool mEnabled;

		private ContentManager mContent;

		private int mCurrentSong;

		private bool mFadeOut;

		private float maxVolume;

		public static bool PlayingUserMusic;

		public static float UserMusicVolume = 1f;
	}
}
