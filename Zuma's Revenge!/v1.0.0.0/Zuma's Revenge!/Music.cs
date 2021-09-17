using System;
using SexyFramework;
using SexyFramework.Drivers.App;

namespace ZumasRevenge
{
	public class Music : IDisposable
	{
		public Music(MusicInterface inMusicInterface)
		{
			this.mMusicInterface = inMusicInterface;
			this.mEnabled = false;
			this.mCurrentSong = Song.DefaultSong;
			this.mNextSong = Song.DefaultSong;
		}

		public void RegisterCallBack()
		{
			this.mMusicInterface.RegisterCallback(new SongChangedEventHandle(this.OnSongChanged));
		}

		public void OnSongChanged(object sender, SongChangedEventArgs args)
		{
			this.mCurrentSong = new Song(args.songID, args.loop, 1f);
		}

		public void Dispose()
		{
			this.mMusicInterface.UnloadAllMusic();
		}

		public void Enable(bool inEnable)
		{
			if (this.mEnabled && !inEnable)
			{
				this.mNextSong = this.mCurrentSong;
				this.mCurrentSong = Song.DefaultSong;
				this.mMusicInterface.StopAllMusic();
			}
			this.mEnabled = inEnable;
		}

		public void LoadMusic(int inSongID, string inFileName)
		{
			this.mMusicInterface.LoadMusic(inSongID, inFileName, WP7AppDriver.sWP7AppDriverInstance.mContentManager);
		}

		public void PlaySong(int inSongID, float inFadeSpeed, bool inLoop)
		{
			this.PlaySong(inSongID, inFadeSpeed, inLoop, false);
		}

		public void PlaySongNoDelay(int inSongID, bool inLoop)
		{
			if (this.IsPlaying(inSongID, false))
			{
				return;
			}
			this.mCurrentSong = new Song(inSongID, inLoop, 1f);
			this.mMusicInterface.PlayMusic(this.mCurrentSong.mID, 0, !this.mCurrentSong.mLoop, 0L);
		}

		public void PlaySong(int inSongID, float inFadeSpeed, bool inLoop, bool inForce)
		{
			if (this.IsPlaying(inSongID, inForce))
			{
				return;
			}
			if (this.DelaySong(inSongID, inFadeSpeed, inLoop))
			{
				return;
			}
			this.mCurrentSong = new Song(inSongID, inLoop, 1f);
			this.mMusicInterface.PlayMusic(this.mCurrentSong.mID, 0, !this.mCurrentSong.mLoop, 0L);
		}

		public void FadeOut()
		{
			this.mCurrentSong = Song.DefaultSong;
			this.mNextSong = this.mCurrentSong;
			this.mMusicInterface.FadeOutAll();
		}

		public void StopAll()
		{
			this.mMusicInterface.StopAllMusic();
		}

		public void Update()
		{
			if (!this.mEnabled || this.mMusicInterface.IsPlaying(this.mCurrentSong.mID))
			{
				return;
			}
			if (this.mNextSong.mID != -1)
			{
				this.mMusicInterface.FadeIn(this.mNextSong.mID, 0, (double)this.mNextSong.mFadeSpeed, !this.mNextSong.mLoop);
			}
			this.mCurrentSong = this.mNextSong;
			this.mNextSong = Song.DefaultSong;
		}

		private bool IsPlaying(int inSongID, bool inForceStop)
		{
			return this.mMusicInterface.IsPlaying(inSongID) && !inForceStop;
		}

		private bool DelaySong(int inSongID, float inFadeSpeed, bool inLoop)
		{
			if (this.mEnabled && !this.mMusicInterface.IsPlaying(this.mCurrentSong.mID))
			{
				return false;
			}
			this.mNextSong = new Song(inSongID, inLoop, inFadeSpeed);
			if (this.mEnabled)
			{
				this.mMusicInterface.FadeOut(this.mCurrentSong.mID, true, (double)inFadeSpeed);
			}
			return true;
		}

		public bool IsUserMusicPlaying()
		{
			return this.mMusicInterface.isPlayingUserMusic();
		}

		private MusicInterface mMusicInterface;

		private bool mEnabled;

		private Song mCurrentSong = Song.DefaultSong;

		private Song mNextSong = Song.DefaultSong;
	}
}
