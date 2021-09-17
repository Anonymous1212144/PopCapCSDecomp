using System;
using Sexy;

namespace BejeweledLIVE
{
	public class Profile
	{
		public Profile()
		{
			this.mHintDisableFlags = 0;
			this.mRunWhenLocked = false;
			this.DisableHint(10);
			this.mSFXVolume = 1.0;
			this.mMusicVolume = 0.45;
			this.mPlayUserMusic = false;
			this.mNumGamesPlayed = 0;
			this.mBestScore = 0;
			this.mSecondsPlayed = 0;
			this.mPuzzlesSolved = 0;
			this.mGemsCleared = 0;
			this.mBiggestCascade = 0;
			this.mBiggestCombo = 0;
			this.mNumPowerGemsCreated = 0;
			this.mNumHyperGemsCreated = 0;
			this.mNumDefaultPuzzlesSolved = 0;
			this.mLastScoreHighGameMode = -1;
			for (int i = 0; i < 10; i++)
			{
				this.mHighestLevel[i] = 0;
			}
			this.ResetHighScores();
			string text = GlobalStaticVars.GetDocumentsDir() + "/profile.dat";
			if (!GlobalStaticVars.gSexyAppBase.FileExists(text))
			{
				SexyAppBase.FirstRun = true;
			}
			Buffer buffer = new Buffer();
			if (GlobalStaticVars.gSexyAppBase.ReadBufferFromFile(text, ref buffer, false))
			{
				int num = buffer.ReadLong();
				int num2 = buffer.ReadLong();
				if (num == 1763438324 && num2 >= 3)
				{
					this.mHintDisableFlags = buffer.ReadLong();
					if (num2 < 6)
					{
						this.DisableHint(10);
					}
					this.mNumGamesPlayed = buffer.ReadLong();
					this.mSFXVolume = buffer.ReadDouble();
					this.mMusicVolume = buffer.ReadDouble();
					this.mBestScore = buffer.ReadLong();
					this.mSecondsPlayed = buffer.ReadLong();
					this.mPuzzlesSolved = buffer.ReadLong();
					this.mGemsCleared = buffer.ReadLong();
					this.mBiggestCascade = buffer.ReadLong();
					this.mBiggestCombo = buffer.ReadLong();
					this.mNumPowerGemsCreated = buffer.ReadLong();
					this.mNumHyperGemsCreated = buffer.ReadLong();
					this.mNumLaserGemsCreated = buffer.ReadLong();
					this.mNumDefaultPuzzlesSolved = buffer.ReadLong();
					int num3 = ((num2 < 5) ? 9 : 10);
					for (int j = 0; j < num3; j++)
					{
						this.mHighestLevel[j] = buffer.ReadLong();
					}
					num3 = ((num2 < 5) ? 2 : 10);
					for (int k = 0; k < num3; k++)
					{
						for (int l = 0; l < 5; l++)
						{
							this.mHighScores[k, l].mInitials = buffer.ReadString();
							this.mHighScores[k, l].mScore = buffer.ReadLong();
							this.mHighScores[k, l].mLevel = buffer.ReadLong();
						}
					}
					this.mRecentlyAdded[0] = buffer.ReadLong();
					this.mRecentlyAdded[1] = buffer.ReadLong();
					this.mPlayUserMusic = buffer.ReadBoolean();
					this.mLastScoreHighGameMode = buffer.ReadLong();
					this.mMostRecentHighScoreName = buffer.ReadString();
					this.mRunWhenLocked = buffer.ReadBoolean();
				}
			}
		}

		public virtual void Dispose()
		{
			this.SaveProfile();
		}

		public void SaveProfile()
		{
			Buffer buffer = new Buffer();
			buffer.WriteLong(1763438324);
			buffer.WriteLong(6);
			buffer.WriteLong(this.mHintDisableFlags);
			buffer.WriteLong(this.mNumGamesPlayed);
			buffer.WriteDouble(this.mSFXVolume);
			buffer.WriteDouble(this.mMusicVolume);
			buffer.WriteLong(this.mBestScore);
			buffer.WriteLong(this.mSecondsPlayed);
			buffer.WriteLong(this.mPuzzlesSolved);
			buffer.WriteLong(this.mGemsCleared);
			buffer.WriteLong(this.mBiggestCascade);
			buffer.WriteLong(this.mBiggestCombo);
			buffer.WriteLong(this.mNumPowerGemsCreated);
			buffer.WriteLong(this.mNumHyperGemsCreated);
			buffer.WriteLong(this.mNumLaserGemsCreated);
			buffer.WriteLong(this.mNumDefaultPuzzlesSolved);
			for (int i = 0; i < 10; i++)
			{
				buffer.WriteLong(this.mHighestLevel[i]);
			}
			for (int j = 0; j < 10; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					buffer.WriteString(this.mHighScores[j, k].mInitials);
					buffer.WriteLong(this.mHighScores[j, k].mScore);
					buffer.WriteLong(this.mHighScores[j, k].mLevel);
				}
			}
			buffer.WriteLong(this.mRecentlyAdded[0]);
			buffer.WriteLong(this.mRecentlyAdded[1]);
			buffer.WriteBoolean(this.mPlayUserMusic);
			buffer.WriteLong(this.mLastScoreHighGameMode);
			buffer.WriteString(this.mMostRecentHighScoreName);
			buffer.WriteBoolean(this.mRunWhenLocked);
			GlobalStaticVars.gSexyAppBase.WriteBufferToFile(Profile.GetFilename(), buffer);
		}

		private static string GetFilename()
		{
			return GlobalStaticVars.GetDocumentsDir() + "/profile.dat";
		}

		public void DeleteSavedProfile()
		{
			Common.DeleteFile(Profile.GetFilename());
		}

		public bool WantsHint(Profile.Hint theHintType)
		{
			return this.WantsHint((int)theHintType);
		}

		public bool WantsHint(int theHintType)
		{
			return (this.mHintDisableFlags & (1 << theHintType)) == 0;
		}

		public void DisableHint(Profile.Hint theHintType)
		{
			this.DisableHint((int)theHintType);
		}

		public void DisableHint(int theHintType)
		{
			this.mHintDisableFlags |= 1 << theHintType;
		}

		public void EnableHint(Profile.Hint theHintType)
		{
			this.EnableHint((int)theHintType);
		}

		public void EnableHint(int theHintType)
		{
			this.mHintDisableFlags &= ~(1 << theHintType);
		}

		public int GetHighScore(int theGameMode)
		{
			return this.mHighScores[theGameMode, 0].mScore;
		}

		public int GetScoreCount(int theGameMode)
		{
			int num = 0;
			while (num < 5 && this.mHighScores[theGameMode, num].mScore > 0)
			{
				num++;
			}
			return num;
		}

		public void GetScoreInfo(int theGameMode, int theIndex, ref string theName, ref int theScore, ref int theLevel)
		{
			theName = this.mHighScores[theGameMode, theIndex].mInitials;
			theScore = this.mHighScores[theGameMode, theIndex].mScore;
			theLevel = this.mHighScores[theGameMode, theIndex].mLevel;
		}

		public int GetScorePos(int theGameMode, int theScore)
		{
			int result = -1;
			if (theScore > 0)
			{
				for (int i = 4; i >= 0; i--)
				{
					if (theScore > this.mHighScores[theGameMode, i].mScore)
					{
						result = i;
					}
				}
			}
			return result;
		}

		public int EnterScore(string theName, int theGameMode, int theScore, int theLevel)
		{
			int num = -1;
			if (theScore > 0)
			{
				for (int i = 4; i >= 0; i--)
				{
					if (theScore > this.mHighScores[theGameMode, i].mScore)
					{
						num = i;
					}
				}
				if (num >= 0)
				{
					for (int j = 4; j > num; j--)
					{
						this.mHighScores[theGameMode, j] = this.mHighScores[theGameMode, j - 1];
					}
					this.mHighScores[theGameMode, num] = new Profile.CurrentProfile();
					this.mHighScores[theGameMode, num].mScore = theScore;
					this.mHighScores[theGameMode, num].mLevel = theLevel;
					this.mHighScores[theGameMode, num].mInitials = theName;
					this.mRecentlyAdded[theGameMode] = num;
					this.mLastScoreHighGameMode = theGameMode;
				}
			}
			return num;
		}

		public void UpdateScoreName(int theGameMode, int thePosition, ref string theNewName)
		{
			this.mHighScores[theGameMode, thePosition].mInitials = theNewName;
			this.mMostRecentHighScoreName = theNewName;
		}

		public void ResetHighScores()
		{
			for (int i = 0; i < 10; i++)
			{
				this.mRecentlyAdded[i] = -1;
				for (int j = 0; j < 5; j++)
				{
					this.mHighScores[i, j] = new Profile.CurrentProfile();
					this.mHighScores[i, j].mScore = -1;
					this.mHighScores[i, j].mLevel = -1;
					this.mHighScores[i, j].mInitials = string.Empty;
				}
			}
		}

		public string GetMostRecentHighScoreName()
		{
			return this.mMostRecentHighScoreName;
		}

		public void SetTutorialCleared(int theTutorial)
		{
			this.SetTutorialCleared(theTutorial, true);
		}

		public void SetTutorialCleared(int theTutorial, bool isCleared)
		{
			if (isCleared)
			{
				this.mTutorialFlags |= 1 << theTutorial;
				return;
			}
			this.mTutorialFlags &= ~(1 << theTutorial);
		}

		public const int PROFILE_VERSION = 6;

		public const int MIN_PROFILE_VERSION = 3;

		public const int PROFILE_MAGIC = 1763438324;

		public int mHintDisableFlags;

		public int mNumGamesPlayed;

		public double mSFXVolume;

		public double mMusicVolume;

		public bool mPlayUserMusic;

		public int mBestScore;

		public int mSecondsPlayed;

		public int mPuzzlesSolved;

		public int mGemsCleared;

		public bool mRunWhenLocked;

		public int mBiggestCascade;

		public int mBiggestCombo;

		public int mNumPowerGemsCreated;

		public int mNumHyperGemsCreated;

		public int mNumLaserGemsCreated;

		public int mNumDefaultPuzzlesSolved;

		public int[] mHighestLevel = new int[10];

		public int[] mRecentlyAdded = new int[10];

		public int mLastScoreHighGameMode;

		public int mTutorialFlags;

		protected Profile.CurrentProfile[,] mHighScores = new Profile.CurrentProfile[10, 5];

		protected string mMostRecentHighScoreName = string.Empty;

		public enum Hint
		{
			HINT_BAD_MOVE,
			HINT_PIECE_POWER_GEM,
			HINT_PIECE_HYPER_CUBE,
			HINT_PIECE_LASER_GEM,
			HINT_HELP_SCREEN,
			HINT_PUZZLE_END_HINT,
			HINT_TIMER,
			HINT_MOVE,
			HINT_MUSIC,
			HINT_BLITZ_INTRO,
			HINT_FRESH_NET_CONTENT
		}

		protected class CurrentProfile
		{
			public int mScore;

			public int mLevel;

			public string mInitials = string.Empty;
		}
	}
}
