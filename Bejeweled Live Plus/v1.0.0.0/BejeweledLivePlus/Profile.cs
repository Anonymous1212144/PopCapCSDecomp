using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using Microsoft.Xna.Framework.GamerServices;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class Profile : IDisposable
	{
		public static bool IsEliteBadge(int badgeId)
		{
			return false;
		}

		protected void ClearProfile()
		{
			this.mProfileId = this.GetNewProfileId();
			this.mProfileName = "";
			this.mFacebookName = "";
			this.mFacebookPassword = "";
			this.mFacebookAutoLogin = false;
			this.mIsNew = true;
			this.mOfflineRank = 0;
			this.mOfflineRankPoints = 0L;
			this.mOnlineRank = 0;
			this.mOnlineRankPoints = 0L;
			this.mOfflineGames = 0;
			this.mOnlineGames = 0;
			this.mFlags = 0;
			this.mLastQuestPage = 0;
			this.mLastQuestBlink = false;
			this.ClearQuestHelpShown();
			this.mStatsTodayDay = (this.mStatsTodayYear = 0);
			this.mStats.ClearAll();
			for (int i = 0; i < 40; i++)
			{
				this.mStatsToday[i] = 0;
			}
			for (int j = 0; j < 5; j++)
			{
				this.mGameStats[j] = 0;
			}
			for (int k = 0; k < 20; k++)
			{
				this.mBadgeStatus[k] = false;
			}
			for (int l = 0; l < 4; l++)
			{
				this.mEndlessModeUnlocked[l] = false;
			}
			for (int m = 0; m < 5; m++)
			{
				this.mOfflineBoostCounts[m] = 0;
			}
			this.mCustomCursors = GlobalMembers.gApp.mCustomCursorsEnabled;
			this.mTutorialFlags = 0UL;
			this.mTipIdx = 0;
			this.mNeedMoveProfileFiles = false;
			for (int n = 0; n < BejeweledLivePlusAppConstants.NUM_QUEST_SETS; n++)
			{
				for (int num = 0; num < BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET + BejeweledLivePlusAppConstants.QUESTS_OPTIONAL_PER_SET; num++)
				{
					this.mQuestsCompleted[n, num] = false;
				}
			}
			this.mLocalDataMap.Clear();
			this.mBreathOn = false;
			this.mBreathVisual = true;
			this.mBreathSpeed = 0.42f;
			this.mNoiseOn = false;
			this.mNoiseFileName = string.Empty;
			this.mBeatOn = false;
			this.mSBAFileName = string.Empty;
			this.mAffirmationOn = false;
			this.mAffirmationFileName = string.Empty;
			this.mAffirmationSubliminal = false;
			this.mAffirmationSpeed = 0.5f;
			this.mAffirmationSubliminality = 0.5f;
			for (int num2 = 0; num2 < 7; num2++)
			{
				this.mHighScores[num2] = 0;
				this.mHighScoresToday[num2] = 0;
			}
			for (int num3 = 0; num3 < 3; num3++)
			{
				this.mRecentBadges[num3] = -1;
			}
			this.mImageNumber = 0;
			this.mProfilePicture = null;
			this.mAutoHint = true;
			this.mAmbientSelection = (this.mMantraSelection = 0);
			this.mAllowAnalytics = true;
			this.mRateGameChoice = RATE_GAME_CHOICE.RATE_GAME_CHOICE_NOT_SEEN;
			this.mRateGameSeenAt = 0;
			this.mHasSeenIntro = false;
			this.mTotalGamesPlayed = 0UL;
			this.mDeferredBadgeVector.Clear();
			for (int num4 = 0; num4 < 20; num4++)
			{
				this.mPreAwardedBadgeLevels[num4] = 0;
			}
			this.mHasSeenGetBlitzDialog = false;
			this.mHasSeenOpenBlitzDialog = false;
			this.mMetricsPostAllAwardedBadges = true;
			this.mLast3MatchScoreManager.Clear();
		}

		protected bool LoadProfileHelper(string theProfileName)
		{
			string theFileName = this.GetProfileDir(theProfileName) + "\\profile.dat";
			Buffer buffer = new Buffer();
			if (!GlobalMembers.gSexyApp.ReadBufferFromStorage(theFileName, buffer))
			{
				return false;
			}
			if (buffer.ReadInt32() != 958131957)
			{
				return false;
			}
			int num = buffer.ReadInt32();
			if (num < 71)
			{
				return false;
			}
			this.mProfileName = theProfileName;
			this.mIsNew = false;
			this.mOfflineRank = buffer.ReadInt32();
			this.mStatsTodayDay = buffer.ReadInt32();
			this.mStatsTodayYear = buffer.ReadInt32();
			if (num > 71)
			{
				int num2 = buffer.ReadInt32();
				for (int i = 0; i < num2; i++)
				{
					this.mStats[i] = buffer.ReadInt32();
				}
				int num3 = buffer.ReadInt32();
				for (int j = 0; j < num3; j++)
				{
					this.mStatsToday[j] = buffer.ReadInt32();
				}
				int num4 = buffer.ReadInt32();
				for (int k = 0; k < num4; k++)
				{
					this.mGameStats[k] = buffer.ReadInt32();
				}
				int num5 = buffer.ReadInt32();
				for (int l = 0; l < num5; l++)
				{
					this.mBadgeStatus[l] = buffer.ReadBoolean();
				}
				int num6 = buffer.ReadInt32();
				for (int m = 0; m < num6; m++)
				{
					this.mEndlessModeUnlocked[m] = buffer.ReadBoolean();
				}
			}
			else
			{
				for (int n = 0; n < 38; n++)
				{
					this.mStats[n] = buffer.ReadInt32();
				}
				for (int num7 = 38; num7 < 40; num7++)
				{
					this.mStats[num7] = 0;
				}
				for (int num8 = 0; num8 < 38; num8++)
				{
					this.mStatsToday[num8] = buffer.ReadInt32();
				}
				for (int num9 = 38; num9 < 40; num9++)
				{
					this.mStatsToday[num9] = 0;
				}
				for (int num10 = 0; num10 < 5; num10++)
				{
					this.mGameStats[num10] = buffer.ReadInt32();
				}
				for (int num11 = 5; num11 < 5; num11++)
				{
					this.mGameStats[num11] = 0;
				}
				for (int num12 = 0; num12 < 20; num12++)
				{
					this.mBadgeStatus[num12] = buffer.ReadBoolean();
				}
				for (int num13 = 0; num13 < 4; num13++)
				{
					this.mEndlessModeUnlocked[num13] = buffer.ReadBoolean();
				}
				for (int num14 = 4; num14 < 4; num14++)
				{
					this.mEndlessModeUnlocked[num14] = false;
				}
			}
			this.mCustomCursors = buffer.ReadBoolean();
			this.mTutorialFlags = (ulong)buffer.ReadInt64();
			this.mTipIdx = buffer.ReadInt32();
			if (num >= 67)
			{
				this.mLastQuestPage = buffer.ReadInt32();
				this.mLastQuestBlink = buffer.ReadBoolean();
			}
			for (int num15 = 0; num15 < BejeweledLivePlusAppConstants.NUM_QUEST_SETS; num15++)
			{
				for (int num16 = 0; num16 < BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET + BejeweledLivePlusAppConstants.QUESTS_OPTIONAL_PER_SET; num16++)
				{
					this.mQuestsCompleted[num15, num16] = buffer.ReadBoolean();
				}
			}
			this.mFacebookName = buffer.ReadString();
			this.mFacebookPassword = buffer.ReadString();
			this.mFacebookAutoLogin = buffer.ReadBoolean();
			this.mLocalDataMap.Clear();
			int num17 = buffer.ReadInt32();
			for (int num18 = 0; num18 < num17; num18++)
			{
				string text = buffer.ReadString();
				string text2 = buffer.ReadString();
				this.mLocalDataMap.Add(text, text2);
			}
			for (int num19 = 0; num19 < 5; num19++)
			{
				this.mOfflineBoostCounts[num19] = buffer.ReadInt32();
			}
			this.mOnlineRank = buffer.ReadInt32();
			this.mOnlineRankPoints = (long)buffer.ReadInt32();
			if (num <= 60)
			{
				this.mOnlineRankPoints = (long)this.mStats[1];
			}
			else
			{
				this.mOfflineRankPoints = (long)buffer.ReadInt32();
			}
			this.mOfflineGames = buffer.ReadInt32();
			this.mOnlineGames = buffer.ReadInt32();
			this.mFlags = buffer.ReadInt32();
			if (num >= 61)
			{
				this.mOfflineRankPoints |= (long)buffer.ReadInt32();
				this.mOnlineRankPoints |= (long)buffer.ReadInt32();
			}
			this.mOnlineRank = (int)this.GetRankAtPoints(this.mOnlineRankPoints);
			this.mOfflineRank = (int)this.GetRankAtPoints(this.mOfflineRankPoints);
			this.mBreathOn = buffer.ReadBoolean();
			this.mBreathVisual = buffer.ReadBoolean();
			this.mBreathSpeed = buffer.ReadFloat();
			this.mNoiseOn = buffer.ReadBoolean();
			this.mNoiseFileName = buffer.ReadString();
			this.mBeatOn = buffer.ReadBoolean();
			this.mSBAFileName = buffer.ReadString();
			this.mAffirmationOn = buffer.ReadBoolean();
			this.mAffirmationFileName = buffer.ReadString();
			this.mAffirmationSubliminal = buffer.ReadBoolean();
			this.mAffirmationSpeed = buffer.ReadFloat();
			this.mAffirmationSubliminality = buffer.ReadFloat();
			if (num >= 69)
			{
				int num20 = buffer.ReadInt32();
				bool flag = num20 == BejeweledLivePlusAppConstants.NUM_QUEST_SETS * BejeweledLivePlusAppConstants.QUESTS_PER_SET;
				for (int num21 = 0; num21 < num20; num21++)
				{
					if (flag)
					{
						this.mQuestHelpShown[num21] = buffer.ReadBoolean();
					}
					else
					{
						buffer.ReadBoolean();
					}
				}
			}
			this.mAllowAnalytics = buffer.ReadBoolean();
			this.mAutoHint = buffer.ReadBoolean();
			this.mImageNumber = buffer.ReadInt32();
			if (num > 71)
			{
				int num22 = buffer.ReadInt32();
				for (int num23 = 0; num23 < num22; num23++)
				{
					this.mRecentBadges[num23] = buffer.ReadInt32();
				}
			}
			else
			{
				for (int num24 = 0; num24 < 3; num24++)
				{
					this.mRecentBadges[num24] = buffer.ReadInt32();
				}
				for (int num25 = 3; num25 < 3; num25++)
				{
					this.mRecentBadges[num25] = 0;
				}
			}
			this.mAmbientSelection = buffer.ReadInt32();
			this.mMantraSelection = buffer.ReadInt32();
			this.mRateGameChoice = (RATE_GAME_CHOICE)buffer.ReadInt32();
			this.mRateGameSeenAt = buffer.ReadInt32();
			if (num != 73)
			{
				this.mRateGameChoice = RATE_GAME_CHOICE.RATE_GAME_CHOICE_NOT_SEEN;
				this.mRateGameSeenAt = this.mStats[0];
			}
			this.mHasSeenIntro = buffer.ReadBoolean();
			this.mTotalGamesPlayed = (ulong)buffer.ReadInt64();
			this.mProfileId = 1U;
			if (num > 71)
			{
				this.mDeferredBadgeVector.Clear();
				int num26 = buffer.ReadInt32();
				for (int num27 = 0; num27 < num26; num27++)
				{
					this.mDeferredBadgeVector.Add(buffer.ReadInt32());
				}
				this.mProfileId = (uint)buffer.ReadInt32();
			}
			if (num > 72)
			{
				this.mHasSeenGetBlitzDialog = buffer.ReadBoolean();
				this.mHasSeenOpenBlitzDialog = buffer.ReadBoolean();
				this.mMetricsPostAllAwardedBadges = buffer.ReadBoolean();
				this.mLast3MatchScoreManager.Load(buffer);
			}
			else
			{
				this.mHasSeenGetBlitzDialog = false;
				this.mHasSeenOpenBlitzDialog = false;
				this.mMetricsPostAllAwardedBadges = true;
				this.mLast3MatchScoreManager.Clear();
			}
			this.ListAddName(theProfileName);
			return true;
		}

		protected uint GetNewProfileId()
		{
			return this.mLastProfileId++;
		}

		public bool ListAddName(string theName)
		{
			for (int i = 0; i < this.mProfileList.Count; i++)
			{
				if (string.Compare(this.mProfileList[i], theName, 5) == 0)
				{
					return false;
				}
			}
			this.mProfileList.Add(theName);
			return true;
		}

		public bool ListRemoveName(string theName)
		{
			bool result = false;
			for (int i = 0; i < this.mProfileList.Count; i++)
			{
				if (string.Compare(this.mProfileList[i], theName, 5) == 0)
				{
					this.mProfileList.RemoveAt(i);
					result = true;
					break;
				}
			}
			return result;
		}

		public void MoveProfileFilesHelper(string theFrom, string theTo)
		{
		}

		public void ClearQuestHelpShown()
		{
			for (int i = 0; i < this.mQuestHelpShown.Length; i++)
			{
				this.mQuestHelpShown[i] = false;
			}
		}

		public bool WantQuestHelp(int theQuestId)
		{
			return theQuestId >= BejeweledLivePlusAppConstants.NUM_QUEST_SETS * BejeweledLivePlusAppConstants.QUESTS_PER_SET || theQuestId < 0 || !this.mQuestHelpShown[theQuestId];
		}

		public void SetQuestHelpShown(int theQuestId)
		{
			this.SetQuestHelpShown(theQuestId, true);
		}

		public void SetQuestHelpShown(int theQuestId, bool theVal)
		{
			if (theQuestId >= BejeweledLivePlusAppConstants.NUM_QUEST_SETS * BejeweledLivePlusAppConstants.QUESTS_PER_SET || theQuestId < 0)
			{
				return;
			}
			this.mQuestHelpShown[theQuestId] = theVal;
		}

		public Profile()
		{
			this.mLastProfileId = 0U;
			this.pointRanges.Clear();
			int num = 0;
			for (int i = 0; i <= GlobalMembers.gApp.mRankNames.size<string>(); i++)
			{
				this.pointRanges.Add(num + 250 * i);
				num += 250 * i;
			}
			this.ClearProfile();
			this.mLastServerGMTTime = DateTime.Now;
			this.mLastServerTimeDelta = DateTime.Now;
			this.mGamesPlayedToday = 0UL;
			this.mLastTickOriginTime = DateTime.Now;
			this.mLastTickOriginCalcTime = DateTime.Now;
		}

		public void Dispose()
		{
			if (this.mNeedMoveProfileFiles)
			{
				this.MoveProfileFiles();
			}
			this.WriteProfile();
			this.WriteProfileList();
		}

		public static string GetTotalTimePlayed()
		{
			int aTime = GlobalMembers.gApp.mProfile.mStats[0];
			return Utils.GetTimeString(aTime);
		}

		public static string GetTotalTimePlayedToday()
		{
			int num = GlobalMembers.gApp.mProfile.mStats[0] - GlobalMembers.gApp.mProfile.mStatsToday[0];
			return Utils.GetTimeString(Math.Max(0, num));
		}

		public void ReadProfileList(ref string theLastPlayer)
		{
			bool flag = false;
			this.mProfileList.Clear();
			string text = string.Empty;
			Buffer buffer = new Buffer();
			string theFileName = BejeweledLivePlus.Misc.Common.GetAppDataFolder() + "users\\users.dat";
			if (GlobalMembers.gSexyApp.ReadBufferFromStorage(theFileName, buffer))
			{
				int num = buffer.ReadInt32();
				int num2 = buffer.ReadInt32();
				if (num2 < 8 || num2 > 8 || num != 958131957)
				{
					this.WriteProfileList();
					return;
				}
				theLastPlayer = buffer.ReadString();
				int num3 = buffer.ReadInt32();
				this.mLastProfileId = Math.Max(this.mLastProfileId, (uint)num3);
				if (num2 >= 6)
				{
					this.mLastFacebookId = buffer.ReadString();
					this.mLastServerGMTTime = DateTime.FromFileTime(buffer.ReadInt64());
					this.mLastServerTimeDelta = DateTime.FromFileTime(buffer.ReadInt64());
					this.mGamesPlayedToday = (ulong)buffer.ReadInt64();
				}
				if (num2 >= 7)
				{
					this.mLastTickOriginTime = DateTime.FromFileTime(buffer.ReadInt64());
					this.mLastTickOriginCalcTime = DateTime.FromFileTime(buffer.ReadInt64());
				}
				for (;;)
				{
					text = buffer.ReadString();
					if (!(text != string.Empty))
					{
						break;
					}
					if (!this.LoadProfileHelper(text))
					{
						flag = true;
						this.DeleteProfile(text);
					}
					else
					{
						this.ListAddName(text);
					}
				}
			}
			if (flag)
			{
				this.WriteProfileList();
			}
		}

		public void WriteProfileList()
		{
			Buffer buffer = new Buffer();
			buffer.WriteInt32(958131957);
			buffer.WriteInt32(8);
			buffer.WriteString(this.mProfileName);
			GlobalMembers.gApp.RegistryWriteString("LastUser", this.mProfileName);
			buffer.WriteInt32((int)this.mLastProfileId);
			buffer.WriteString(this.mLastFacebookId);
			buffer.WriteInt64(this.mLastServerGMTTime.ToFileTime());
			buffer.WriteInt64(this.mLastServerTimeDelta.ToFileTime());
			buffer.WriteInt64((long)this.mGamesPlayedToday);
			buffer.WriteInt64(this.mLastTickOriginTime.ToFileTime());
			buffer.WriteInt64(this.mLastTickOriginCalcTime.ToFileTime());
			for (int i = 0; i < this.mProfileList.Count; i++)
			{
				buffer.WriteString(this.mProfileList[i]);
			}
			string theFileName = BejeweledLivePlus.Misc.Common.GetAppDataFolder() + "users\\users.dat";
			GlobalMembers.gApp.WriteBufferToFile(theFileName, buffer);
		}

		public List<string> GetProfileList()
		{
			return this.mProfileList;
		}

		public bool LoadProfile(string theProfileName)
		{
			return this.LoadProfile(theProfileName, true);
		}

		public bool LoadProfile(string theProfileName, bool saveCurrent)
		{
			if (this.mNeedMoveProfileFiles)
			{
				this.MoveProfileFiles();
			}
			if (saveCurrent)
			{
				this.WriteProfile();
			}
			if (!this.LoadProfileHelper(theProfileName))
			{
				this.ListRemoveName(theProfileName);
				return false;
			}
			return true;
		}

		public bool CreateProfile(string theProfileName)
		{
			return this.CreateProfile(theProfileName, true);
		}

		public bool CreateProfile(string theProfileName, bool clearProfile)
		{
			if (theProfileName != string.Empty && !this.ListAddName(theProfileName))
			{
				string text = theProfileName;
				for (int i = 0; i < this.mProfileList.Count; i++)
				{
					if (string.Compare(this.mProfileList[i], text, 5) == 0)
					{
						text = this.mProfileList[i];
						break;
					}
				}
				string theDialogLines = string.Format(GlobalMembers._ID("There is already a user named {0}", 412), text);
				GlobalMembers.gApp.DoDialog(0, true, GlobalMembers._ID("SAME NAME", 413), theDialogLines, GlobalMembers._ID("OK", 414), 3);
				return false;
			}
			BejeweledLivePlus.Misc.Common.Deltree(this.GetProfileDir(theProfileName));
			if (this.mNeedMoveProfileFiles)
			{
				this.MoveProfileFiles();
			}
			this.WriteProfile();
			if (clearProfile)
			{
				this.ClearProfile();
			}
			this.mProfileName = theProfileName;
			BejeweledLivePlus.Misc.Common.MkDir(this.GetProfileDir(theProfileName));
			this.WriteProfileList();
			return true;
		}

		public bool RenameProfile(string theOldProfileName, string theNewProfileName)
		{
			if (string.Compare(theOldProfileName, theNewProfileName, 5) == 0)
			{
				this.ListRemoveName(theOldProfileName);
				this.ListAddName(theNewProfileName);
				this.mProfileName = theNewProfileName;
				return true;
			}
			this.WriteProfile();
			string profileDir = this.GetProfileDir(theOldProfileName);
			string profileDir2 = this.GetProfileDir(theNewProfileName);
			BejeweledLivePlus.Misc.Common.Deltree(profileDir2);
			BejeweledLivePlus.Misc.Common.MkDir(profileDir2);
			GlobalMembers.gSexyApp.mFileDriver.MoveFile(profileDir, profileDir2);
			this.ListRemoveName(theOldProfileName);
			this.ListAddName(theNewProfileName);
			if (string.Compare(theOldProfileName, this.mProfileName, 5) == 0)
			{
				this.mProfileName = theNewProfileName;
			}
			BejeweledLivePlus.Misc.Common.Deltree(profileDir);
			return true;
		}

		public bool DeleteProfile(string theProfileName)
		{
			if (string.Compare(theProfileName, this.mProfileName, 5) == 0)
			{
				this.mProfileName = string.Empty;
			}
			BejeweledLivePlus.Misc.Common.Deltree(this.GetProfileDir(theProfileName));
			return !this.ListRemoveName(theProfileName) && false;
		}

		public bool WriteProfile()
		{
			if (this.mProfileName == string.Empty)
			{
				return false;
			}
			string text = this.GetProfileDir(this.mProfileName) + "\\profile.dat";
			BejeweledLivePlus.Misc.Common.MkDir(SexyFramework.Common.GetFileDir(text, false));
			Buffer buffer = new Buffer();
			buffer.WriteInt32(958131957);
			buffer.WriteInt32(73);
			buffer.WriteInt32(this.mOfflineRank);
			buffer.WriteInt32(this.mStatsTodayDay);
			buffer.WriteInt32(this.mStatsTodayYear);
			buffer.WriteInt32(40);
			for (int i = 0; i < 40; i++)
			{
				buffer.WriteInt32(this.mStats[i]);
			}
			buffer.WriteInt32(40);
			for (int j = 0; j < 40; j++)
			{
				buffer.WriteInt32(this.mStatsToday[j]);
			}
			buffer.WriteInt32(5);
			for (int k = 0; k < 5; k++)
			{
				buffer.WriteInt32(this.mGameStats[k]);
			}
			buffer.WriteInt32(20);
			for (int l = 0; l < 20; l++)
			{
				buffer.WriteBoolean(this.mBadgeStatus[l]);
			}
			buffer.WriteInt32(4);
			for (int m = 0; m < 4; m++)
			{
				buffer.WriteBoolean(this.mEndlessModeUnlocked[m]);
			}
			buffer.WriteBoolean(this.mCustomCursors);
			buffer.WriteInt64((long)this.mTutorialFlags);
			buffer.WriteInt32(this.mTipIdx);
			buffer.WriteInt32(this.mLastQuestPage);
			buffer.WriteBoolean(this.mLastQuestBlink);
			for (int n = 0; n < BejeweledLivePlusAppConstants.NUM_QUEST_SETS; n++)
			{
				for (int num = 0; num < BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET + BejeweledLivePlusAppConstants.QUESTS_OPTIONAL_PER_SET; num++)
				{
					buffer.WriteBoolean(this.mQuestsCompleted[n, num]);
				}
			}
			buffer.WriteString(this.mFacebookName);
			buffer.WriteString(this.mFacebookPassword);
			buffer.WriteBoolean(this.mFacebookAutoLogin);
			buffer.WriteInt32(this.mLocalDataMap.Count);
			Dictionary<string, string>.Enumerator enumerator = this.mLocalDataMap.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Buffer buffer2 = buffer;
				KeyValuePair<string, string> keyValuePair = enumerator.Current;
				buffer2.WriteString(keyValuePair.Key);
				Buffer buffer3 = buffer;
				KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
				buffer3.WriteString(keyValuePair2.Value);
			}
			for (int num2 = 0; num2 < 5; num2++)
			{
				buffer.WriteInt32(this.mOfflineBoostCounts[num2]);
			}
			buffer.WriteInt32(this.mOnlineRank);
			buffer.WriteInt32((int)(this.mOnlineRankPoints & (long)((ulong)(-1))));
			buffer.WriteInt32((int)(this.mOfflineRankPoints & (long)((ulong)(-1))));
			buffer.WriteInt32(this.mOfflineGames);
			buffer.WriteInt32(this.mOnlineGames);
			buffer.WriteInt32(this.mFlags);
			buffer.WriteInt32((int)(this.mOfflineRankPoints >> 32));
			buffer.WriteInt32((int)(this.mOnlineRankPoints >> 32));
			buffer.WriteBoolean(this.mBreathOn);
			buffer.WriteBoolean(this.mBreathVisual);
			buffer.WriteFloat(this.mBreathSpeed);
			buffer.WriteBoolean(this.mNoiseOn);
			buffer.WriteString(this.mNoiseFileName);
			buffer.WriteBoolean(this.mBeatOn);
			buffer.WriteString(this.mSBAFileName);
			buffer.WriteBoolean(this.mAffirmationOn);
			buffer.WriteString(this.mAffirmationFileName);
			buffer.WriteBoolean(this.mAffirmationSubliminal);
			buffer.WriteFloat(this.mAffirmationSpeed);
			buffer.WriteFloat(this.mAffirmationSubliminality);
			buffer.WriteInt32(BejeweledLivePlusAppConstants.NUM_QUEST_SETS * BejeweledLivePlusAppConstants.QUESTS_PER_SET);
			for (int num3 = 0; num3 < BejeweledLivePlusAppConstants.NUM_QUEST_SETS * BejeweledLivePlusAppConstants.QUESTS_PER_SET; num3++)
			{
				buffer.WriteBoolean(this.mQuestHelpShown[num3]);
			}
			buffer.WriteBoolean(this.mAllowAnalytics);
			buffer.WriteBoolean(this.mAutoHint);
			buffer.WriteInt32(this.mImageNumber);
			buffer.WriteInt32(3);
			for (int num4 = 0; num4 < 3; num4++)
			{
				buffer.WriteInt32(this.mRecentBadges[num4]);
			}
			buffer.WriteInt32(this.mAmbientSelection);
			buffer.WriteInt32(this.mMantraSelection);
			buffer.WriteInt32((int)this.mRateGameChoice);
			buffer.WriteInt32(this.mRateGameSeenAt);
			buffer.WriteBoolean(this.mHasSeenIntro);
			buffer.WriteInt64((long)this.mTotalGamesPlayed);
			int count = this.mDeferredBadgeVector.Count;
			buffer.WriteInt32(count);
			for (int num5 = 0; num5 < count; num5++)
			{
				buffer.WriteInt32(this.mDeferredBadgeVector[num5]);
			}
			buffer.WriteInt32((int)this.mProfileId);
			buffer.WriteBoolean(this.mHasSeenGetBlitzDialog);
			buffer.WriteBoolean(this.mHasSeenOpenBlitzDialog);
			buffer.WriteBoolean(this.mMetricsPostAllAwardedBadges);
			this.mLast3MatchScoreManager.Save(buffer);
			GlobalMembers.gSexyApp.WriteBufferToFile(text, buffer);
			return true;
		}

		public bool GetAnyProfile()
		{
			for (int i = 0; i < this.mProfileList.Count; i++)
			{
				if (this.LoadProfile(this.mProfileList[i]))
				{
					return true;
				}
			}
			return false;
		}

		public int GetProfileCount()
		{
			return this.mProfileList.Count;
		}

		public string GetProfile(int theProfile)
		{
			if (theProfile < 0 || theProfile >= this.GetProfileCount())
			{
				return string.Empty;
			}
			return this.mProfileList[theProfile];
		}

		public void MoveProfileFiles()
		{
			this.MoveProfileFilesHelper(this.GetProfileDir(string.Empty), this.GetProfileDir(this.mProfileName));
			this.mNeedMoveProfileFiles = false;
		}

		public string GetProfileDir(string theProfileName)
		{
			string saveDataPath = GlobalMembers.gFileDriver.GetSaveDataPath();
			string result = saveDataPath + "users\\" + GlobalMembersProfile.ToUserDirectoryName(theProfileName);
			if (theProfileName == string.Empty)
			{
				result = saveDataPath + "users\\_temp";
			}
			return result;
		}

		public bool HasClearedTutorial(int theTutorial)
		{
			return (this.mTutorialFlags & (1UL << theTutorial)) != 0UL;
		}

		public void SetTutorialCleared(int theTutorial)
		{
			this.SetTutorialCleared(theTutorial, true);
		}

		public void SetTutorialCleared(int theTutorial, bool isCleared)
		{
			if (isCleared)
			{
				this.mTutorialFlags |= 1UL << theTutorial;
				return;
			}
			this.mTutorialFlags &= ~(1UL << theTutorial);
		}

		public long GetRankPoints(uint theRank)
		{
			if (theRank == 0U)
			{
				return 0L;
			}
			if ((ulong)theRank >= (ulong)((long)this.pointRanges.Count))
			{
				return (long)this.pointRanges[this.pointRanges.Count - 1];
			}
			return (long)this.pointRanges[(int)theRank];
		}

		public float GetRankAtPoints(long thePoints)
		{
			for (int i = 0; i < this.pointRanges.Count - 1; i++)
			{
				if (thePoints < (long)this.pointRanges[i])
				{
					return (float)Math.Max(0, i - 1);
				}
			}
			if (thePoints >= (long)this.pointRanges[this.pointRanges.Count - 2])
			{
				return (float)(this.pointRanges.Count - 2);
			}
			return 0f;
		}

		public List<int> GetPointRanges()
		{
			return this.pointRanges;
		}

		public string GetRankName()
		{
			return GlobalMembers.gApp.mRankNames[Math.Min(this.mOfflineRank, GlobalMembers.gApp.mRankNames.size<string>() - 1)];
		}

		public string GetRankName(GameMode mode)
		{
			return "Jeweler";
		}

		public int GetModeHighScore(GameMode mode)
		{
			return this.mHighScores[(int)mode];
		}

		public int GetModeHighScoreToday(GameMode mode)
		{
			return this.mHighScoresToday[(int)mode];
		}

		public bool UsesPresetProfilePicture()
		{
			return true;
		}

		public int GetProfilePictureId()
		{
			return this.mImageNumber;
		}

		public void SetProfilePictureId(int id)
		{
			this.mImageNumber = id;
		}

		public void AddRecentBadge(int badgeId)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.mRecentBadges[i] == badgeId)
				{
					return;
				}
			}
			for (int j = 1; j >= 0; j--)
			{
				this.mRecentBadges[j + 1] = this.mRecentBadges[j];
			}
			this.mRecentBadges[0] = badgeId;
		}

		public void UpdateRank(Board theBoard)
		{
			if (theBoard == null)
			{
				return;
			}
			GlobalMembers.gApp.mProfile.mOfflineRankPoints += (long)this.GetRankPointsBracket((int)((float)theBoard.mGameStats[1] / theBoard.GetRankPointMultiplier()));
			int num = GlobalMembers.gApp.mProfile.mOfflineRank;
			GlobalMembers.gApp.mProfile.mOfflineRank = (int)GlobalMembers.gApp.mProfile.GetRankAtPoints(GlobalMembers.gApp.mProfile.mOfflineRankPoints);
			if (GlobalMembers.gApp.mProfile.mOfflineRank > num)
			{
				string theEventString = string.Format("RankUp Rank={0} RankPoints={1} Seconds={2}", GlobalMembers.gApp.mProfile.mOfflineRank, GlobalMembers.gApp.mProfile.mOfflineRankPoints, GlobalMembers.gApp.mProfile.mStats[0]);
				GlobalMembers.gApp.LogStatString(theEventString);
			}
		}

		public int GetRankPointsBracket(int score)
		{
			int[] array = new int[]
			{
				0, 25000, 50000, 75000, 100000, 125000, 150000, 175000, 200000, 225000,
				250000, 275000, 300000, 350000, 400000, 450000, 500000, 600000, 700000, 800000,
				900000, 1000000, 1200000, 1400000, 1600000, 1800000, 2000000
			};
			int num = 50;
			if (score <= 0)
			{
				return 0;
			}
			if (score > array[array.Length - 1])
			{
				return 1350;
			}
			for (int i = 1; i < array.Length; i++)
			{
				if (score > array[i - 1] && score <= array[i])
				{
					num += i * 50;
					break;
				}
			}
			return num;
		}

		public uint GetProfileId()
		{
			return this.mProfileId;
		}

		public void TryToGetLiveProfile()
		{
			SignedInGamer signedInGamer = Gamer.SignedInGamers[0];
			try
			{
				signedInGamer.BeginGetProfile(new AsyncCallback(this.GetProfileCallback), signedInGamer);
			}
			catch (Exception)
			{
			}
		}

		private void GetProfileCallback(IAsyncResult result)
		{
			lock (this.mGetProfileLock)
			{
				Gamer gamer = result.AsyncState as Gamer;
				if (gamer != null)
				{
					try
					{
						GamerProfile gamerProfile = gamer.EndGetProfile(result);
						gamerProfile.GetGamerPicture();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		public const int MAX_CHARS = 16;

		private List<int> pointRanges = new List<int>();

		private Image mProfilePicture;

		private int mImageNumber;

		private uint mProfileId;

		private uint mLastProfileId;

		public List<string> mProfileList = new List<string>();

		public DateTime mLastServerGMTTime = DateTime.Now;

		public DateTime mLastServerTimeDelta = DateTime.Now;

		public DateTime mLastTickOriginTime = DateTime.Now;

		public DateTime mLastTickOriginCalcTime = DateTime.Now;

		public ulong mGamesPlayedToday;

		public string mLastFacebookId = string.Empty;

		public string mProfileName = string.Empty;

		public int mFlags;

		public bool mNeedMoveProfileFiles;

		public string mFacebookName = string.Empty;

		public string mFacebookPassword = string.Empty;

		public bool mFacebookAutoLogin;

		public bool mIsNew;

		public StatsDoubleBuffer mStats = new StatsDoubleBuffer();

		public int[] mStatsToday = new int[40];

		public int mStatsTodayDay;

		public int mStatsTodayYear;

		public bool[,] mQuestsCompleted = new bool[BejeweledLivePlusAppConstants.NUM_QUEST_SETS, BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET + BejeweledLivePlusAppConstants.QUESTS_OPTIONAL_PER_SET];

		public bool[] mEndlessModeUnlocked = new bool[4];

		public int mLastQuestPage;

		public bool mLastQuestBlink;

		public int[] mGameStats = new int[5];

		public bool[] mBadgeStatus = new bool[20];

		public int[] mHighScores = new int[7];

		public int[] mHighScoresToday = new int[7];

		public int[] mRecentBadges = new int[3];

		public int mOfflineRank;

		public long mOfflineRankPoints;

		public int mOnlineRank;

		public long mOnlineRankPoints;

		public int mOfflineGames;

		public int mOnlineGames;

		public bool mCustomCursors;

		public ulong mTutorialFlags;

		public int mTipIdx;

		public int[] mOfflineBoostCounts = new int[5];

		public bool mBreathOn;

		public bool mBreathVisual;

		public float mBreathSpeed;

		public bool mNoiseOn;

		public string mNoiseFileName;

		public bool mBeatOn;

		public string mSBAFileName;

		public bool mAffirmationOn;

		public string mAffirmationFileName;

		public bool mAffirmationSubliminal;

		public float mAffirmationSpeed;

		public float mAffirmationSubliminality;

		public int mAmbientSelection;

		public int mMantraSelection;

		public bool[] mQuestHelpShown = new bool[BejeweledLivePlusAppConstants.NUM_QUEST_SETS * BejeweledLivePlusAppConstants.QUESTS_PER_SET];

		public Dictionary<string, string> mLocalDataMap = new Dictionary<string, string>();

		public bool mAllowAnalytics;

		public bool mAutoHint;

		public RATE_GAME_CHOICE mRateGameChoice;

		public int mRateGameSeenAt;

		public ulong mTotalGamesPlayed;

		public List<int> mDeferredBadgeVector = new List<int>();

		public int[] mPreAwardedBadgeLevels = new int[20];

		public bool mHasSeenIntro;

		public bool mHasSeenGetBlitzDialog;

		public bool mHasSeenOpenBlitzDialog;

		public bool mMetricsPostAllAwardedBadges;

		public Last3MatchScoreManager mLast3MatchScoreManager = new Last3MatchScoreManager();

		private object mGetProfileLock = new object();

		public enum RecentBadgeNum
		{
			MAX_NUMBER_OF_RECENT_BADGES = 3
		}
	}
}
