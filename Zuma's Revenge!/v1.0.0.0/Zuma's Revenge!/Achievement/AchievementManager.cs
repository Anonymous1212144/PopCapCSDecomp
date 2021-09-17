using System;
using Microsoft.Xna.Framework.GamerServices;

namespace ZumasRevenge.Achievement
{
	public class AchievementManager
	{
		public AchievementManager()
		{
			this.Init();
		}

		private void Init()
		{
			AchievementEntry achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.FE_RROG.ToString();
			achievementEntry.m_NameResID = 78;
			achievementEntry.m_DescriptionResID = 64;
			achievementEntry.m_GPoints = 30;
			achievementEntry.m_IconResID = 1824;
			this.m_AchievementList[0] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.CHALLENGE_ACCEPTED.ToString();
			achievementEntry.m_NameResID = 79;
			achievementEntry.m_DescriptionResID = 65;
			achievementEntry.m_GPoints = 5;
			achievementEntry.m_IconResID = 1825;
			this.m_AchievementList[1] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.FROZEN_FROG.ToString();
			achievementEntry.m_NameResID = 80;
			achievementEntry.m_DescriptionResID = 66;
			achievementEntry.m_GPoints = 10;
			achievementEntry.m_IconResID = 1826;
			this.m_AchievementList[2] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.POWER_PLAYER.ToString();
			achievementEntry.m_NameResID = 81;
			achievementEntry.m_DescriptionResID = 67;
			achievementEntry.m_GPoints = 10;
			achievementEntry.m_IconResID = 1827;
			this.m_AchievementList[3] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.C_C_C_C_CHAIN_BONUS.ToString();
			achievementEntry.m_NameResID = 82;
			achievementEntry.m_DescriptionResID = 68;
			achievementEntry.m_GPoints = 10;
			achievementEntry.m_IconResID = 1828;
			this.m_AchievementList[4] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.THREAD_THE_NEEDLE.ToString();
			achievementEntry.m_NameResID = 83;
			achievementEntry.m_DescriptionResID = 69;
			achievementEntry.m_GPoints = 5;
			achievementEntry.m_IconResID = 1829;
			this.m_AchievementList[5] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.TIME_FOR_TADPOLES.ToString();
			achievementEntry.m_NameResID = 84;
			achievementEntry.m_DescriptionResID = 70;
			achievementEntry.m_GPoints = 25;
			achievementEntry.m_IconResID = 1830;
			this.m_AchievementList[6] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.YOU_WIN.ToString();
			achievementEntry.m_NameResID = 85;
			achievementEntry.m_DescriptionResID = 71;
			achievementEntry.m_GPoints = 5;
			achievementEntry.m_IconResID = 1831;
			this.m_AchievementList[7] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.CEPHALOPOD_SMASHE.ToString();
			achievementEntry.m_NameResID = 86;
			achievementEntry.m_DescriptionResID = 72;
			achievementEntry.m_GPoints = 20;
			achievementEntry.m_IconResID = 1832;
			this.m_AchievementList[8] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.PESTILENCE_PACIFIER.ToString();
			achievementEntry.m_NameResID = 87;
			achievementEntry.m_DescriptionResID = 73;
			achievementEntry.m_GPoints = 20;
			achievementEntry.m_IconResID = 1833;
			this.m_AchievementList[9] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.BONE_PICKER.ToString();
			achievementEntry.m_NameResID = 88;
			achievementEntry.m_DescriptionResID = 74;
			achievementEntry.m_GPoints = 20;
			achievementEntry.m_IconResID = 1834;
			this.m_AchievementList[10] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.TIKI_TRAMPLER.ToString();
			achievementEntry.m_NameResID = 89;
			achievementEntry.m_DescriptionResID = 75;
			achievementEntry.m_GPoints = 15;
			achievementEntry.m_IconResID = 1835;
			this.m_AchievementList[11] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.JAW_BREAKER.ToString();
			achievementEntry.m_NameResID = 90;
			achievementEntry.m_DescriptionResID = 76;
			achievementEntry.m_GPoints = 15;
			achievementEntry.m_IconResID = 1836;
			this.m_AchievementList[12] = achievementEntry;
			achievementEntry = new AchievementEntry();
			achievementEntry.m_Key = EAchievementType.FROG_STATUE.ToString();
			achievementEntry.m_NameResID = 91;
			achievementEntry.m_DescriptionResID = 77;
			achievementEntry.m_GPoints = 10;
			achievementEntry.m_IconResID = 1823;
			this.m_AchievementList[13] = achievementEntry;
		}

		public void getAchievementsInfo(ref int unlockedNum, ref int totalNum, ref int unlockedGPoint, ref int totalGPoint)
		{
			totalNum = this.m_AchievementList.Length;
			unlockedNum = 0;
			unlockedGPoint = 0;
			totalGPoint = 0;
			foreach (AchievementEntry achievementEntry in this.m_AchievementList)
			{
				if (achievementEntry.m_Unlocked)
				{
					unlockedNum++;
					unlockedGPoint += achievementEntry.m_GPoints;
				}
				totalGPoint += achievementEntry.m_GPoints;
			}
		}

		public AchievementEntry GetAchievementEntry(EAchievementType type)
		{
			return this.m_AchievementList[(int)type];
		}

		public bool isAchievementUnlocked(EAchievementType type)
		{
			return this.m_AchievementList[(int)type].m_Unlocked;
		}

		public void UnlockAchievement(EAchievementType type)
		{
			this.m_AchievementList[(int)type].m_Unlocked = true;
			if ((GameApp.USE_XBOX_SERVICE || GameApp.UN_UPDATE_VERSION) && !GameApp.USE_TRIAL_VERSION && !this.UnlockAchievementXLive(type.ToString(), type))
			{
				this.ToggleAchievement(type);
			}
		}

		public void ToggleAchievement(EAchievementType type)
		{
			if (GameApp.gApp.mBoard != null)
			{
				AchievementEntry achievementEntry = GameApp.gApp.mUserProfile.m_AchievementMgr.GetAchievementEntry(type);
				GameApp.gApp.mBoard.ToggleNotification(TextManager.getInstance().getString(92) + TextManager.getInstance().getString(achievementEntry.m_NameResID), Res.GetSoundByID(ResID.SOUND_MIDZONE_NOTIFY));
			}
		}

		public void Sync(DataSync sync)
		{
			for (int i = 0; i < 14; i++)
			{
				sync.SyncBoolean(ref this.m_AchievementList[i].m_Unlocked);
			}
		}

		public bool UnlockAchievementXLive(string achievementKey, EAchievementType type)
		{
			SignedInGamer signedInGamer = Gamer.SignedInGamers[0];
			if (signedInGamer == null)
			{
				return false;
			}
			try
			{
				AchievementUpdateEntry achievementUpdateEntry = new AchievementUpdateEntry();
				achievementUpdateEntry.mGamer = signedInGamer;
				achievementUpdateEntry.mType = type;
				signedInGamer.BeginAwardAchievement(achievementKey, new AsyncCallback(this.AwardAchievementCallback), achievementUpdateEntry);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		protected void AwardAchievementCallback(IAsyncResult result)
		{
			AchievementUpdateEntry achievementUpdateEntry = result.AsyncState as AchievementUpdateEntry;
			SignedInGamer mGamer = achievementUpdateEntry.mGamer;
			if (mGamer != null)
			{
				mGamer.EndAwardAchievement(result);
				return;
			}
			this.ToggleAchievement(achievementUpdateEntry.mType);
		}

		public void SyncAchievementsXLive()
		{
			if (this.m_AchievementsXLive == null)
			{
				return;
			}
			foreach (Achievement achievement in this.m_AchievementsXLive)
			{
				EAchievementType eachievementType = (EAchievementType)Enum.Parse(typeof(EAchievementType), achievement.Key, false);
				if (achievement.IsEarned)
				{
					this.m_AchievementList[(int)eachievementType].m_Unlocked = true;
				}
				else if (this.m_AchievementList[(int)eachievementType].m_Unlocked)
				{
					this.UnlockAchievementXLive(achievement.Key, eachievementType);
				}
			}
		}

		private AchievementEntry[] m_AchievementList = new AchievementEntry[14];

		public AchievementCollection m_AchievementsXLive;
	}
}
