using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.GamerServices;

namespace BejeweledLivePlus
{
	public class BadgeManager
	{
		public static BadgeManager GetBadgeManagerInstance()
		{
			if (BadgeManager.instance == null)
			{
				BadgeManager.instance = new BadgeManager();
			}
			return BadgeManager.instance;
		}

		public void LinkBoard(Board board)
		{
			this.mBoard = board;
			for (int i = 0; i < 20; i++)
			{
				if (this.mBadgeClass[i] != null)
				{
					this.mBadgeClass[i].mBoard = this.mBoard;
				}
			}
		}

		public BadgeManager()
		{
			this.ConfigBadges();
			this.SyncBadges();
			this.GetAchievementsFromXBLA();
		}

		public void Dispose()
		{
			for (int i = 0; i < 20; i++)
			{
				if (this.mBadgeClass[i] != null)
				{
					this.mBadgeClass[i].Dispose();
					this.mBadgeClass[i] = null;
				}
			}
		}

		public void SetBadge(int theIdx)
		{
			this.mBadge = this.mBadgeClass[theIdx];
		}

		public Badge GetBadge()
		{
			return this.mBadge;
		}

		public Badge GetBadgeByIndex(int theIdx)
		{
			return this.mBadgeClass[theIdx];
		}

		public Badge GetBadgeByType(Badges theType)
		{
			return this.GetBadgeByIndex((int)theType);
		}

		public void SyncBadges()
		{
			for (int i = 0; i < 20; i++)
			{
				if (this.mBadgeClass[i] != null)
				{
					this.mBadgeClass[i].mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[i];
				}
			}
			this.SyncAchievementsWithXBLA();
		}

		public void Update()
		{
			foreach (Badge badge in this.mBadgeClass)
			{
				if (badge.CanUnlock() && !badge.mPending)
				{
					this.AwardAchievement(badge.mName);
					badge.mPending = true;
				}
				if (badge.mUnlocked)
				{
					badge.mPending = false;
				}
			}
		}

		public void AwardAchievement(string aName)
		{
			SignedInGamer signedInGamer = Gamer.SignedInGamers[0];
			if (signedInGamer == null)
			{
				return;
			}
			try
			{
				signedInGamer.BeginAwardAchievement(aName, new AsyncCallback(this.AwardAchievementCallback), signedInGamer);
			}
			catch (GameUpdateRequiredException ex)
			{
				throw ex;
			}
			catch (Exception)
			{
			}
		}

		protected void AwardAchievementCallback(IAsyncResult result)
		{
			lock (BadgeManager.mAchievementsLockObject)
			{
				try
				{
					SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
					if (signedInGamer != null)
					{
						signedInGamer.EndAwardAchievement(result);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		public void SyncAchievementsWithXBLA()
		{
			if (GlobalMembers.g_AchievementsXLive != null && GlobalMembers.g_AchievementsXLive.Count > 0)
			{
				List<string> list = new List<string>();
				for (int i = 0; i < GlobalMembers.g_AchievementsXLive.Count; i++)
				{
					Achievement achievement = GlobalMembers.g_AchievementsXLive[i];
					Badge badge = null;
					foreach (Badge badge2 in this.mBadgeClass)
					{
						if (badge2.mName == achievement.Key)
						{
							badge = badge2;
							break;
						}
					}
					if (badge != null)
					{
						if (achievement.IsEarned)
						{
							badge.mUnlocked = true;
						}
						else if (badge.mUnlocked)
						{
							list.Add(badge.mName);
						}
					}
				}
				foreach (string aName in list)
				{
					this.AwardAchievement(aName);
				}
				for (int k = 0; k < 20; k++)
				{
					if (this.mBadgeClass[k] != null)
					{
						GlobalMembers.gApp.mProfile.mBadgeStatus[k] = this.mBadgeClass[k].mUnlocked;
					}
				}
			}
		}

		public void GetAchievementsFromXBLA()
		{
			SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.GamerSignedInCallback);
		}

		protected void GamerSignedInCallback(object sender, SignedInEventArgs args)
		{
			try
			{
				SignedInGamer gamer = args.Gamer;
				if (gamer != null)
				{
					gamer.BeginGetAchievements(new AsyncCallback(this.GetAchievementsCallback), gamer);
				}
			}
			catch (Exception)
			{
			}
		}

		protected void GetAchievementsCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer == null)
			{
				return;
			}
			lock (BadgeManager.mAchievementsLockObject)
			{
				try
				{
					GlobalMembers.g_AchievementsXLive = signedInGamer.EndGetAchievements(result);
					this.SyncAchievementsWithXBLA();
				}
				catch (Exception)
				{
				}
			}
		}

		private void ConfigBadges()
		{
			this.mBadgeClass[0] = new BadgeLevelord();
			this.mBadgeClass[1] = new BadgeBejGold();
			this.mBadgeClass[2] = new BadgeBronze();
			this.mBadgeClass[3] = new BadgePlatinum();
			this.mBadgeClass[4] = new BadgeRelicSilver();
			this.mBadgeClass[5] = new BadgeRelicPlatinum();
			this.mBadgeClass[6] = new BadgeFinalfrenzyBronze();
			this.mBadgeClass[7] = new BadgeFinalfrenzyPlatinum();
			this.mBadgeClass[8] = new BadgeHighvolatageBronze();
			this.mBadgeClass[9] = new BadgeHighvolatagePlatinum();
			this.mBadgeClass[10] = new BadgeMonarchSilver();
			this.mBadgeClass[11] = new BadgeMonarchPlatinum();
			this.mBadgeClass[12] = new BadgeBonanzaBronze();
			this.mBadgeClass[13] = new BadgeBonanzaPlatinum();
			this.mBadgeClass[14] = new BadgeChromaticGold();
			this.mBadgeClass[15] = new BadgeStellarGold();
			this.mBadgeClass[16] = new BadgeBlasterGold();
			this.mBadgeClass[17] = new BadgeSuperStar();
			this.mBadgeClass[18] = new BadgeChainreactionGlod();
			this.mBadgeClass[19] = new BadgeLuckyStreakGold();
		}

		private static BadgeManager instance = null;

		private static object mAchievementsLockObject = new object();

		public Badge mBadge;

		public Badge[] mBadgeClass = new Badge[20];

		public Board mBoard;
	}
}
