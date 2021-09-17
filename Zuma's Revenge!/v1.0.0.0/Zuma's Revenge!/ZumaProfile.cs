using System;
using System.Collections.Generic;
using SexyFramework.Drivers.Profile;
using SexyFramework.File;
using SexyFramework.Misc;
using ZumasRevenge.Achievement;

namespace ZumasRevenge
{
	public class ZumaProfile : UserProfile
	{
		protected void InitBossLevel()
		{
		}

		public ZumaProfile(ZumaProfile rhs)
		{
			this.CopyFrom(rhs);
		}

		public ZumaProfile()
		{
			this.mLastSeenMoreGames = 0L;
			this.mWantsChallengeHelp = true;
			this.mNeedsFirstTimeIntro = true;
			this.mUnlockTrial = true;
			this.mHasDoneHeroicUnlockEffect = (this.mHasDoneChallengeUnlockEffect = (this.mHasDoneIFUnlockEffect = false));
			this.mLargestGapShot = 0;
			this.mHighestGapShotScore = 0;
			this.mNumGapShots = 0;
			this.mLargestChainShot = 0;
			this.mLargestCombo = 0;
			this.mNumClearCurveBonuses = 0;
			this.mNeedsChallengeUnlockHint = true;
			this.mPointsFromClearCurve = 0;
			this.mPointsFromGapShots = 0;
			this.mPointsFromCombos = 0;
			this.mDoChallengeAceTrophyZoom = (this.mDoChallengeTrophyZoom = false);
			this.mDoChallengeCupComplete = (this.mDoChallengeAceCupComplete = false);
			this.mUnlockSparklesIdx1 = (this.mUnlockSparklesIdx2 = -1);
			this.mPointsFromChainShots = 0;
			this.mNumFruits = 0;
			this.mPointsFromFruit = 0;
			this.mNumTimesLaserCanceled = 0;
			this.mPointsFromLaser = 0;
			this.mPointsFromCannon = 0;
			this.mPointsFromColorNuke = 0;
			this.mPointsFromProxBomb = 0;
			this.mDoAceCupXFade = false;
			this.mNewChallengeCupUnlocked = false;
			for (int i = 0; i < 14; i++)
			{
				this.mNumTimesActivatedPowerup[i] = 0;
				this.mNumTimesSpawnedPowerup[i] = 0;
			}
			this.mAdvModeVars.mDDSTier = (this.mAdvModeVars.mRestartDDSTier = -1);
			this.mHeroicModeVars.mDDSTier = (this.mHeroicModeVars.mRestartDDSTier = -1);
			this.mFirstTimeReplayingNormalMode = false;
			this.mFirstTimeReplayingHardMode = false;
			this.mBoss6Part2DialogSeen = 0;
			this.mSessionID = 0;
			this.mAdvModeVars.mHighestZoneBeat = 0;
			this.mAdvModeVars.mHighestLevelBeat = 0;
			this.mAdvModeVars.mNumDeathsCurLevel = 0;
			this.mAdvModeVars.mPerfectZone = true;
			this.mAdvModeVars.mNumZumasCurLevel = 0;
			this.mHeroicModeVars.mHighestZoneBeat = 0;
			this.mHeroicModeVars.mHighestLevelBeat = 0;
			this.mHeroicModeVars.mNumDeathsCurLevel = 0;
			this.mHeroicModeVars.mPerfectZone = true;
			this.mHeroicModeVars.mNumZumasCurLevel = 0;
			this.mHighestIronFrogLevel = (this.mHighestIronFrogScore = 0);
			this.mHighestAdvModeScore = (this.mAdvModeHSLevel = (this.mAdvModeHSZone = 0));
			this.mHasBeatIronFrogMode = false;
			for (int j = 0; j < 6; j++)
			{
				this.mHeroicModeVars.mNumTimesZoneBeat[j] = 0;
				this.mAdvModeVars.mNumTimesZoneBeat[j] = 0;
			}
			for (int k = 0; k < 60; k++)
			{
				this.mHeroicModeVars.mBestLevelTime[k] = (this.mAdvModeVars.mBestLevelTime[k] = int.MaxValue);
			}
			this.ClearAdventureModeDetails();
			this.mSessionID++;
			this.mAdvBetaStats.Init(this, this.mSessionID, 2);
			this.mHardAdvBetaStats.Init(this, this.mSessionID, 3);
			this.mChallengeBetaStats.Init(this, this.mSessionID, 0);
			this.mIronFrogBetaStats.Init(this, this.mSessionID, 1);
			for (int l = 0; l < 7; l++)
			{
				for (int m = 0; m < 10; m++)
				{
					this.mChallengeUnlockState[l, m] = 0;
				}
			}
			this.mNumDoubleGapShots = (this.mNumTripleGapShots = (this.mMatchesMade = (this.mBallsBroken = (this.mBallsSwapped = (this.mBallsFired = (this.mFruitBombed = (this.mBallsTossed = (this.mDeathsAfterZuma = 0))))))));
			Buffer buffer = new Buffer();
			this.mGauntletHSMap.Clear();
			if (StorageFile.ReadBufferFromFile("users/hs.dat", buffer))
			{
				long num = buffer.ReadLong();
				if (num != (long)GameApp.gSaveGameVersion)
				{
					StorageFile.DeleteFile("users/hs.dat");
				}
				else
				{
					long num2 = buffer.ReadLong();
					int num3 = 0;
					while ((long)num3 < num2)
					{
						long num4 = buffer.ReadLong();
						long num5 = buffer.ReadLong();
						List<GauntletHSInfo> list = new List<GauntletHSInfo>();
						int num6 = 0;
						while ((long)num6 < num5)
						{
							long num7 = buffer.ReadLong();
							string n = buffer.ReadString();
							list.Add(new GauntletHSInfo((int)num7, n));
							num6++;
						}
						this.mGauntletHSMap.Add((int)num4, list);
						num3++;
					}
				}
			}
			this.mUserSharingEnabled = true;
		}

		public virtual void CopyFrom(ZumaProfile rhs)
		{
			this.mNeedsFirstTimeIntro = rhs.mNeedsFirstTimeIntro;
			this.mUnlockTrial = rhs.mUnlockTrial;
			this.mAdvModeVars.CopyFrom(rhs.mAdvModeVars);
			this.mHeroicModeVars.CopyFrom(rhs.mHeroicModeVars);
			this.mUnlockSparklesIdx1 = rhs.mUnlockSparklesIdx1;
			this.mUnlockSparklesIdx2 = rhs.mUnlockSparklesIdx2;
			this.mDoChallengeAceTrophyZoom = rhs.mDoChallengeAceTrophyZoom;
			this.mDoChallengeTrophyZoom = rhs.mDoChallengeTrophyZoom;
			this.mDoChallengeCupComplete = rhs.mDoChallengeCupComplete;
			this.mDoChallengeAceCupComplete = rhs.mDoChallengeAceCupComplete;
			this.mDoAceCupXFade = rhs.mDoAceCupXFade;
			this.mNeedsChallengeUnlockHint = rhs.mNeedsChallengeUnlockHint;
			this.mHasDoneHeroicUnlockEffect = rhs.mHasDoneHeroicUnlockEffect;
			this.mHasDoneIFUnlockEffect = rhs.mHasDoneIFUnlockEffect;
			this.mHasDoneChallengeUnlockEffect = rhs.mHasDoneChallengeUnlockEffect;
			this.mNewChallengeCupUnlocked = rhs.mNewChallengeCupUnlocked;
			this.mWantsChallengeHelp = rhs.mWantsChallengeHelp;
			this.mAdvBetaStats.CopyFrom(rhs.mAdvBetaStats);
			this.mHardAdvBetaStats.CopyFrom(rhs.mHardAdvBetaStats);
			this.mChallengeBetaStats.CopyFrom(rhs.mChallengeBetaStats);
			this.mIronFrogBetaStats.CopyFrom(rhs.mIronFrogBetaStats);
			this.mSessionID = rhs.mSessionID;
			this.mSessionID++;
			this.mAdvBetaStats.Init(this, this.mSessionID, 2);
			this.mHardAdvBetaStats.Init(this, this.mSessionID, 3);
			this.mChallengeBetaStats.Init(this, this.mSessionID, 0);
			this.mIronFrogBetaStats.Init(this, this.mSessionID, 1);
			this.mHasBeatIronFrogMode = rhs.mHasBeatIronFrogMode;
			this.mHighestIronFrogLevel = rhs.mHighestIronFrogLevel;
			this.mHighestIronFrogScore = rhs.mHighestIronFrogScore;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					this.mChallengeUnlockState[i, j] = rhs.mChallengeUnlockState[i, j];
				}
			}
			this.mHighestAdvModeScore = rhs.mHighestAdvModeScore;
			this.mAdvModeHSLevel = rhs.mAdvModeHSLevel;
			this.mAdvModeHSZone = rhs.mAdvModeHSZone;
			this.mBoss6Part2DialogSeen = rhs.mBoss6Part2DialogSeen;
			this.mFirstTimeAtBoss = rhs.mFirstTimeAtBoss;
			this.mHintsSeen = rhs.mHintsSeen;
			this.mFirstTimeReplayingNormalMode = rhs.mFirstTimeReplayingNormalMode;
			this.mFirstTimeReplayingHardMode = rhs.mFirstTimeReplayingHardMode;
			this.mAdventureStats.CopyFrom(rhs.mAdventureStats);
			this.mHeroicStats.CopyFrom(rhs.mHeroicStats);
			this.mIronFrogStats.CopyFrom(rhs.mIronFrogStats);
			this.mChallengeStats.CopyFrom(rhs.mChallengeStats);
			this.mNumDoubleGapShots = rhs.mNumDoubleGapShots;
			this.mNumTripleGapShots = rhs.mNumTripleGapShots;
			this.mMatchesMade = rhs.mMatchesMade;
			this.mBallsBroken = rhs.mBallsBroken;
			this.mBallsSwapped = rhs.mBallsSwapped;
			this.mBallsFired = rhs.mBallsFired;
			this.mFruitBombed = rhs.mFruitBombed;
			this.mBallsTossed = rhs.mBallsTossed;
			this.mDeathsAfterZuma = rhs.mDeathsAfterZuma;
			this.mLargestGapShot = rhs.mLargestGapShot;
			this.mHighestGapShotScore = rhs.mHighestGapShotScore;
			this.mNumGapShots = rhs.mNumGapShots;
			this.mLargestChainShot = rhs.mLargestChainShot;
			this.mLargestCombo = rhs.mLargestCombo;
			this.mNumClearCurveBonuses = rhs.mNumClearCurveBonuses;
			this.mPointsFromClearCurve = rhs.mPointsFromClearCurve;
			this.mPointsFromGapShots = rhs.mPointsFromGapShots;
			this.mPointsFromCombos = rhs.mPointsFromCombos;
			this.mPointsFromChainShots = rhs.mPointsFromChainShots;
			this.mNumFruits = rhs.mNumFruits;
			this.mPointsFromFruit = rhs.mPointsFromFruit;
			this.mNumTimesLaserCanceled = rhs.mNumTimesLaserCanceled;
			this.mPointsFromLaser = rhs.mPointsFromLaser;
			this.mPointsFromCannon = rhs.mPointsFromCannon;
			this.mPointsFromColorNuke = rhs.mPointsFromColorNuke;
			this.mPointsFromProxBomb = rhs.mPointsFromProxBomb;
			for (int k = 0; k < 14; k++)
			{
				this.mNumTimesActivatedPowerup[k] = rhs.mNumTimesActivatedPowerup[k];
				this.mNumTimesSpawnedPowerup[k] = rhs.mNumTimesSpawnedPowerup[k];
			}
			this.mLastSeenMoreGames = rhs.mLastSeenMoreGames;
			this.mUserSharingEnabled = rhs.mUserSharingEnabled;
		}

		public void ClearAdventureModeDetails()
		{
			this.mAdvModeVars.mCurrentAdvLevel = (this.mAdvModeVars.mCurrentAdvZone = 1);
			this.mAdvModeVars.mCurrentAdvScore = 0;
			this.mAdvModeVars.mCurrentAdvLives = 3;
			this.mHeroicModeVars.mCurrentAdvLevel = (this.mHeroicModeVars.mCurrentAdvZone = 1);
			this.mHeroicModeVars.mCurrentAdvScore = 0;
			this.mHeroicModeVars.mCurrentAdvLives = 3;
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					this.mChallengeUnlockState[i, j] = 0;
				}
			}
			this.mDoChallengeAceTrophyZoom = (this.mDoChallengeTrophyZoom = false);
			this.mDoAceCupXFade = (this.mDoChallengeCupComplete = (this.mDoChallengeAceCupComplete = false));
			this.mHintsSeen = 0;
			this.mAdvModeVars.mPerfectZone = true;
			this.mHeroicModeVars.mPerfectZone = true;
			this.mFirstTimeAtBoss = true;
			this.mAdvModeVars.mNumDeathsCurLevel = 0;
			this.mAdvModeVars.mNumZumasCurLevel = 0;
			this.mHeroicModeVars.mNumDeathsCurLevel = 0;
			this.mHeroicModeVars.mNumZumasCurLevel = 0;
			this.mBoss6Part2DialogSeen = 0;
			this.mAdvModeVars.mDDSTier = (this.mAdvModeVars.mRestartDDSTier = -1);
			this.mHeroicModeVars.mDDSTier = (this.mHeroicModeVars.mRestartDDSTier = -1);
			for (int k = 0; k < 6; k++)
			{
				this.mHeroicModeVars.mFirstTimeInZone[k] = true;
				this.mAdvModeVars.mFirstTimeInZone[k] = true;
			}
		}

		public int ChallengeCupComplete(int zone)
		{
			zone--;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 10; i++)
			{
				if (this.mChallengeUnlockState[zone, i] == 4)
				{
					num++;
				}
				else if (this.mChallengeUnlockState[zone, i] == 5)
				{
					num++;
					num2++;
				}
			}
			if (num2 == 10)
			{
				return 2;
			}
			if (num == 10)
			{
				return 1;
			}
			return 0;
		}

		public string GetSaveGameNameFolder()
		{
			return "users";
		}

		public string GetSaveGameName(bool hard_mode)
		{
			return this.GetSaveGameNameFolder() + string.Format("/{0}_in_game{1}.sav", hard_mode ? "heroic" : "adv", this.GetId());
		}

		public AdvModeVars GetAdvModeVars()
		{
			if (GameApp.gApp.IsHardMode())
			{
				return this.mHeroicModeVars;
			}
			return this.mAdvModeVars;
		}

		public void BossLevelStarted()
		{
			if (!this.mFirstTimeAtBoss)
			{
				return;
			}
			this.mFirstTimeAtBoss = false;
			this.InitBossLevel();
		}

		public bool SyncDetails(DataSync theSync)
		{
			int gSaveGameVersion = GameApp.gSaveGameVersion;
			theSync.SyncLong(ref gSaveGameVersion);
			if (gSaveGameVersion != GameApp.gSaveGameVersion)
			{
				return false;
			}
			theSync.SyncLong(ref this.mAdvModeVars.mCurrentAdvLives);
			theSync.SyncLong(ref this.mAdvModeVars.mCurrentAdvScore);
			theSync.SyncLong(ref this.mAdvModeVars.mCurrentAdvLevel);
			theSync.SyncLong(ref this.mAdvModeVars.mCurrentAdvZone);
			theSync.SyncLong(ref this.mHeroicModeVars.mCurrentAdvLives);
			theSync.SyncLong(ref this.mHeroicModeVars.mCurrentAdvScore);
			theSync.SyncLong(ref this.mHeroicModeVars.mCurrentAdvLevel);
			theSync.SyncLong(ref this.mHeroicModeVars.mCurrentAdvZone);
			theSync.SyncBoolean(ref this.mWantsChallengeHelp);
			theSync.SyncBoolean(ref this.mNeedsFirstTimeIntro);
			theSync.SyncBoolean(ref this.mUnlockTrial);
			theSync.SyncLong(ref this.mSessionID);
			theSync.SyncLong(ref this.mAdvModeVars.mDDSTier);
			theSync.SyncLong(ref this.mAdvModeVars.mRestartDDSTier);
			theSync.SyncLong(ref this.mHeroicModeVars.mDDSTier);
			theSync.SyncLong(ref this.mHeroicModeVars.mRestartDDSTier);
			theSync.SyncBoolean(ref this.mHasDoneChallengeUnlockEffect);
			theSync.SyncBoolean(ref this.mHasDoneIFUnlockEffect);
			theSync.SyncBoolean(ref this.mHasDoneHeroicUnlockEffect);
			theSync.SyncBoolean(ref this.mFirstTimeAtBoss);
			for (int i = 0; i < 6; i++)
			{
				theSync.SyncBoolean(ref this.mAdvModeVars.mFirstTimeInZone[i]);
				theSync.SyncBoolean(ref this.mHeroicModeVars.mFirstTimeInZone[i]);
			}
			for (int j = 0; j < 6; j++)
			{
				theSync.SyncLong(ref this.mAdvModeVars.mCheckpointScores[j].mBoss);
				theSync.SyncLong(ref this.mAdvModeVars.mCheckpointScores[j].mMidpoint);
				theSync.SyncLong(ref this.mAdvModeVars.mCheckpointScores[j].mZoneStart);
				theSync.SyncLong(ref this.mHeroicModeVars.mCheckpointScores[j].mBoss);
				theSync.SyncLong(ref this.mHeroicModeVars.mCheckpointScores[j].mMidpoint);
				theSync.SyncLong(ref this.mHeroicModeVars.mCheckpointScores[j].mZoneStart);
			}
			theSync.SyncBoolean(ref this.mNeedsChallengeUnlockHint);
			theSync.SyncLong(ref this.mBoss6Part2DialogSeen);
			theSync.SyncLong(ref this.mHighestAdvModeScore);
			theSync.SyncLong(ref this.mAdvModeHSZone);
			theSync.SyncLong(ref this.mAdvModeHSLevel);
			theSync.SyncLong(ref this.mHintsSeen);
			theSync.SyncLong(ref this.mLargestGapShot);
			theSync.SyncLong(ref this.mHighestGapShotScore);
			theSync.SyncLong(ref this.mNumGapShots);
			theSync.SyncLong(ref this.mLargestChainShot);
			theSync.SyncLong(ref this.mLargestCombo);
			theSync.SyncLong(ref this.mNumClearCurveBonuses);
			theSync.SyncLong(ref this.mPointsFromClearCurve);
			theSync.SyncLong(ref this.mPointsFromGapShots);
			theSync.SyncLong(ref this.mPointsFromCombos);
			theSync.SyncLong(ref this.mPointsFromChainShots);
			theSync.SyncLong(ref this.mNumFruits);
			theSync.SyncLong(ref this.mPointsFromFruit);
			theSync.SyncLong(ref this.mNumTimesLaserCanceled);
			theSync.SyncLong(ref this.mPointsFromLaser);
			theSync.SyncLong(ref this.mPointsFromCannon);
			theSync.SyncLong(ref this.mPointsFromColorNuke);
			theSync.SyncLong(ref this.mPointsFromProxBomb);
			theSync.SyncLong(ref this.mNumDoubleGapShots);
			theSync.SyncLong(ref this.mNumTripleGapShots);
			theSync.SyncLong(ref this.mMatchesMade);
			theSync.SyncLong(ref this.mBallsBroken);
			theSync.SyncLong(ref this.mBallsSwapped);
			theSync.SyncLong(ref this.mBallsFired);
			theSync.SyncLong(ref this.mFruitBombed);
			theSync.SyncLong(ref this.mBallsTossed);
			theSync.SyncLong(ref this.mDeathsAfterZuma);
			for (int k = 0; k < 14; k++)
			{
				theSync.SyncLong(ref this.mNumTimesActivatedPowerup[k]);
				theSync.SyncLong(ref this.mNumTimesSpawnedPowerup[k]);
			}
			theSync.SyncLong(ref this.mAdvModeVars.mNumDeathsCurLevel);
			theSync.SyncLong(ref this.mAdvModeVars.mNumZumasCurLevel);
			theSync.SyncBoolean(ref this.mAdvModeVars.mPerfectZone);
			theSync.SyncLong(ref this.mHeroicModeVars.mNumDeathsCurLevel);
			theSync.SyncLong(ref this.mHeroicModeVars.mNumZumasCurLevel);
			theSync.SyncBoolean(ref this.mHeroicModeVars.mPerfectZone);
			theSync.SyncBoolean(ref this.mFirstTimeReplayingNormalMode);
			theSync.SyncBoolean(ref this.mFirstTimeReplayingHardMode);
			theSync.SyncLong(ref this.mAdvModeVars.mHighestZoneBeat);
			theSync.SyncLong(ref this.mAdvModeVars.mHighestLevelBeat);
			theSync.SyncLong(ref this.mHeroicModeVars.mHighestZoneBeat);
			theSync.SyncLong(ref this.mHeroicModeVars.mHighestLevelBeat);
			for (int l = 0; l < 6; l++)
			{
				theSync.SyncLong(ref this.mHeroicModeVars.mNumTimesZoneBeat[l]);
				theSync.SyncLong(ref this.mAdvModeVars.mNumTimesZoneBeat[l]);
			}
			for (int m = 0; m < 60; m++)
			{
				theSync.SyncLong(ref this.mHeroicModeVars.mBestLevelTime[m]);
				theSync.SyncLong(ref this.mAdvModeVars.mBestLevelTime[m]);
			}
			theSync.SyncBoolean(ref this.mHasBeatIronFrogMode);
			this.mAdventureStats.Sync(theSync);
			this.mHeroicStats.Sync(theSync);
			this.mIronFrogStats.Sync(theSync);
			this.mChallengeStats.Sync(theSync);
			for (int n = 0; n < 7; n++)
			{
				for (int num = 0; num < 10; num++)
				{
					theSync.SyncLong(ref this.mChallengeUnlockState[n, num]);
				}
			}
			theSync.SyncBoolean(ref this.mNewChallengeCupUnlocked);
			theSync.SyncLong(ref this.mLastSeenMoreGames);
			theSync.SyncBoolean(ref this.mUserSharingEnabled);
			this.m_AchievementMgr.Sync(theSync);
			return true;
		}

		public override bool ReadProfileSettings(Buffer theData)
		{
			try
			{
				DataSync theSync = new DataSync(theData, true);
				this.SyncDetails(theSync);
				this.mSessionID++;
				this.mAdvBetaStats.Init(this, this.mSessionID, 2);
				this.mHardAdvBetaStats.Init(this, this.mSessionID, 3);
				this.mChallengeBetaStats.Init(this, this.mSessionID, 0);
				this.mIronFrogBetaStats.Init(this, this.mSessionID, 1);
				this.mAdvBetaStats.LoadData();
				this.mHardAdvBetaStats.LoadData();
				this.mChallengeBetaStats.LoadData();
				this.mIronFrogBetaStats.LoadData();
			}
			catch (Exception)
			{
				this.ClearAdventureModeDetails();
				return false;
			}
			return true;
		}

		public override bool WriteProfileSettings(Buffer theData)
		{
			DataSync theSync = new DataSync(theData, false);
			this.SyncDetails(theSync);
			return true;
		}

		public void BossLevelComplete()
		{
			this.mFirstTimeAtBoss = true;
		}

		public bool HasSeenHint(int hint_num)
		{
			return (this.mHintsSeen & hint_num) == hint_num;
		}

		public void MarkHintAsSeen(int hint_num)
		{
			this.mHintsSeen |= hint_num;
		}

		public int AddGauntletHighScore(int level, int score, string profile_name)
		{
			if (!this.mGauntletHSMap.ContainsKey(level))
			{
				this.mGauntletHSMap.Add(level, new List<GauntletHSInfo>());
			}
			List<GauntletHSInfo> list = this.mGauntletHSMap[level];
			string[] array = new string[]
			{
				TextManager.getInstance().getString(712),
				TextManager.getInstance().getString(713),
				TextManager.getInstance().getString(714),
				TextManager.getInstance().getString(715),
				TextManager.getInstance().getString(716),
				TextManager.getInstance().getString(717),
				TextManager.getInstance().getString(718)
			};
			int num = 0;
			while (list.Count < 5)
			{
				list.Insert(list.Count, new GauntletHSInfo(15000 - num * 1000, array[num++]));
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (score >= list[i].mScore)
				{
					list.Insert(i, new GauntletHSInfo(score, profile_name));
					Buffer buffer = new Buffer();
					buffer.WriteLong((long)GameApp.gSaveGameVersion);
					buffer.WriteLong((long)this.mGauntletHSMap.Count);
					foreach (KeyValuePair<int, List<GauntletHSInfo>> keyValuePair in this.mGauntletHSMap)
					{
						buffer.WriteLong((long)keyValuePair.Key);
						buffer.WriteLong((long)keyValuePair.Value.Count);
						for (int j = 0; j < keyValuePair.Value.Count; j++)
						{
							buffer.WriteLong((long)keyValuePair.Value[j].mScore);
							buffer.WriteString(keyValuePair.Value[j].mProfileName);
						}
					}
					StorageFile.WriteBufferToFile("users/hs.dat", buffer);
					return i;
				}
			}
			list.Insert(list.Count, new GauntletHSInfo(score, profile_name));
			Buffer buffer2 = new Buffer();
			buffer2.WriteLong((long)GameApp.gSaveGameVersion);
			buffer2.WriteLong((long)this.mGauntletHSMap.Count);
			foreach (KeyValuePair<int, List<GauntletHSInfo>> keyValuePair2 in this.mGauntletHSMap)
			{
				buffer2.WriteLong((long)keyValuePair2.Key);
				buffer2.WriteLong((long)keyValuePair2.Value.Count);
				for (int k = 0; k < keyValuePair2.Value.Count; k++)
				{
					buffer2.WriteLong((long)keyValuePair2.Value[k].mScore);
					buffer2.WriteString(keyValuePair2.Value[k].mProfileName);
				}
			}
			StorageFile.WriteBufferToFile("users/hs.dat", buffer2);
			return list.Count - 1;
		}

		private static int HSSort(GauntletHSInfo hs1, GauntletHSInfo hs2)
		{
			if (hs1.mScore > hs2.mScore)
			{
				return -1;
			}
			if (hs1.mScore < hs2.mScore)
			{
				return 1;
			}
			return 0;
		}

		public void GetGauntletHighScores(int level, ref List<GauntletHSInfo> scores)
		{
			if (this.mGauntletHSMap.ContainsKey(level))
			{
				scores = this.mGauntletHSMap[level];
			}
			string[] array = new string[]
			{
				TextManager.getInstance().getString(719),
				TextManager.getInstance().getString(720),
				TextManager.getInstance().getString(721),
				TextManager.getInstance().getString(722),
				TextManager.getInstance().getString(723),
				TextManager.getInstance().getString(724),
				TextManager.getInstance().getString(725)
			};
			int num = 0;
			while (scores.Count < 5)
			{
				scores.Insert(scores.Count, new GauntletHSInfo(15000 - num * 1000, array[num++]));
			}
			scores.Sort(new Comparison<GauntletHSInfo>(ZumaProfile.HSSort));
		}

		public void DisableUserSharing()
		{
			this.mUserSharingEnabled = false;
		}

		public void EnableUserSharing()
		{
			this.mUserSharingEnabled = true;
		}

		public bool IsUserSharingEnabled()
		{
			return this.mUserSharingEnabled;
		}

		internal new int GetGamepadIndex()
		{
			return 0;
		}

		public static int MAX_LEVEL_SAMPLES = 4;

		public static int MAX_BOSS_SAMPLES = 4;

		public static int FIRST_SHOT_HINT = 1;

		public static int ZUMA_BAR_HINT = 2;

		public static int SKULL_PIT_HINT = 4;

		public static int LILLY_PAD_HINT = 8;

		public static int FRUIT_HINT = 16;

		public static int CHALLENGE_HINT = 32;

		public static int SWAP_BALL_HINT = 64;

		public long mLastSeenMoreGames;

		public AdvModeTempleStats mAdventureStats = new AdvModeTempleStats();

		public AdvModeTempleStats mHeroicStats = new AdvModeTempleStats();

		public IronFrogTempleStats mIronFrogStats = new IronFrogTempleStats();

		public ChallengeTempleStats mChallengeStats = new ChallengeTempleStats();

		public BetaStats mAdvBetaStats = new BetaStats();

		public BetaStats mHardAdvBetaStats = new BetaStats();

		public BetaStats mChallengeBetaStats = new BetaStats();

		public BetaStats mIronFrogBetaStats = new BetaStats();

		public bool mNewChallengeCupUnlocked;

		public int[,] mChallengeUnlockState = new int[7, 10];

		public bool mFirstTimeReplayingNormalMode;

		public bool mFirstTimeReplayingHardMode;

		public bool mUnlockTrial;

		public bool mNeedsFirstTimeIntro = true;

		public bool mWantsChallengeHelp;

		public bool mDoChallengeTrophyZoom;

		public bool mDoChallengeAceTrophyZoom;

		public bool mDoChallengeCupComplete;

		public bool mDoChallengeAceCupComplete;

		public bool mDoAceCupXFade;

		public bool mNeedsChallengeUnlockHint;

		public int mUnlockSparklesIdx1;

		public int mUnlockSparklesIdx2;

		public bool mHasDoneIFUnlockEffect;

		public bool mHasDoneChallengeUnlockEffect;

		public bool mHasDoneHeroicUnlockEffect;

		public bool mHasBeatIronFrogMode;

		public int mHighestIronFrogLevel;

		public int mHighestIronFrogScore;

		public int mNumDoubleGapShots;

		public int mNumTripleGapShots;

		public int mMatchesMade;

		public int mBallsBroken;

		public int mBallsSwapped;

		public int mBallsFired;

		public int mFruitBombed;

		public int mBallsTossed;

		public int mDeathsAfterZuma;

		public AdvModeVars mAdvModeVars = new AdvModeVars();

		public AdvModeVars mHeroicModeVars = new AdvModeVars();

		public int mHighestAdvModeScore;

		public int mAdvModeHSLevel;

		public int mAdvModeHSZone;

		public int mBoss6Part2DialogSeen;

		public int mLargestGapShot;

		public int mHighestGapShotScore;

		public int mNumGapShots;

		public int mLargestChainShot;

		public int mLargestCombo;

		public int mNumClearCurveBonuses;

		public int mPointsFromClearCurve;

		public int mPointsFromGapShots;

		public int mPointsFromCombos;

		public int mPointsFromChainShots;

		public int mNumFruits;

		public int mPointsFromFruit;

		public int mNumTimesLaserCanceled;

		public int mPointsFromLaser;

		public int mPointsFromCannon;

		public int mPointsFromColorNuke;

		public int mPointsFromProxBomb;

		public int[] mNumTimesActivatedPowerup = new int[14];

		public int[] mNumTimesSpawnedPowerup = new int[14];

		public static int MAX_GAUNTLET_HIGH_SCORES = 5;

		private bool mUserSharingEnabled;

		protected Dictionary<int, List<GauntletHSInfo>> mGauntletHSMap = new Dictionary<int, List<GauntletHSInfo>>();

		protected int mSessionID;

		protected bool mFirstTimeAtBoss;

		protected int mHintsSeen;

		public AchievementManager m_AchievementMgr = new AchievementManager();

		public enum ChallengeState
		{
			ChallengeState_Locked,
			ChallengeState_ZoneUnlocked,
			ChallengeState_CanPlay,
			ChallengeState_LevelComplete,
			ChallengeState_GoalComplete,
			ChallengeState_AceComplete
		}
	}
}
