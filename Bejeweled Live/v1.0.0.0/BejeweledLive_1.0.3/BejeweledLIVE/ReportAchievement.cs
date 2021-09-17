using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.GamerServices;
using Sexy;

namespace BejeweledLIVE
{
	internal static class ReportAchievement
	{
		public static int EarnedGamerScore { get; private set; }

		public static int MaxGamerScore { get; private set; }

		public static void Initialise()
		{
			SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(ReportAchievement.GamerSignedInCallback);
		}

		private static void GamerSignedInCallback(object sender, SignedInEventArgs args)
		{
			SignedInGamer gamer = args.Gamer;
			if (gamer != null && ReportAchievement.gamestate == ReportAchievement.GameState.WaitingForSignIn)
			{
				ReportAchievement.gamestate = ReportAchievement.GameState.WaitingForAchivements;
				ReportAchievement.StartGetAchievements();
			}
		}

		private static void GetAchievementsCallback(IAsyncResult result)
		{
			SignedInGamer gamer = Main.GetGamer();
			if (gamer == null)
			{
				return;
			}
			lock (ReportAchievement.achievementLock)
			{
				Achievements.ClearAchievements();
				ReportAchievement.MaxGamerScore = 0;
				ReportAchievement.EarnedGamerScore = 0;
				try
				{
					ReportAchievement.achievements = gamer.EndGetAchievements(result);
					for (int i = 0; i < ReportAchievement.achievements.Count; i++)
					{
						Achievement achievement = ReportAchievement.achievements[i];
						ReportAchievement.MaxGamerScore += achievement.GamerScore;
						if (achievement.IsEarned)
						{
							ReportAchievement.EarnedGamerScore += achievement.GamerScore;
						}
						AchievementItem item = new AchievementItem(achievement);
						Achievements.AddAchievement(item);
					}
				}
				catch (GameUpdateRequiredException)
				{
					GlobalStaticVars.gSexyAppBase.ShowUpdateRequiredMessage();
				}
				catch (Exception ex)
				{
					ReportAchievement.e = ex.Message;
				}
				ReportAchievement.gamestate = ReportAchievement.GameState.Ready;
			}
		}

		private static void GiveAchievement(Achievements.AchievementId achievement)
		{
			ReportAchievement.GiveAchievement(achievement, false);
		}

		private static void GiveAchievement(Achievements.AchievementId achievement, bool forceGive)
		{
			if (!forceGive && ReportAchievement.pendingAchievements.Contains(achievement))
			{
				return;
			}
			if (Gamer.SignedInGamers.Count == 0)
			{
				return;
			}
			SignedInGamer gamer = Main.GetGamer();
			string achievementKey = Achievements.GetAchievementKey(achievement);
			if (achievementKey == null)
			{
				return;
			}
			lock (ReportAchievement.achievementLock)
			{
				if (ReportAchievement.achievements != null)
				{
					foreach (Achievement achievement2 in ReportAchievement.achievements)
					{
						if (achievement2.Key == achievementKey && achievement2.IsEarned)
						{
							return;
						}
					}
					if (!SexyAppBase.IsInTrialMode)
					{
						gamer.BeginAwardAchievement(achievementKey, new AsyncCallback(ReportAchievement.AwardingAchievementCallback), null);
					}
					if (!ReportAchievement.pendingAchievements.Contains(achievement))
					{
						ReportAchievement.pendingAchievements.Add(achievement);
					}
					if (SexyAppBase.IsInTrialMode)
					{
						ReportAchievement.achievementAlertsToShow.Enqueue(ReportAchievement.CreateAchievementAlert(achievement));
					}
				}
			}
		}

		public static void GivePendingAchievements()
		{
			if (ReportAchievement.achievementAlertsToShow.Count > 0)
			{
				GlobalStaticVars.gSexyAppBase.ShowAchievementMessage(ReportAchievement.achievementAlertsToShow.Dequeue());
			}
		}

		private static TrialAchievementAlert CreateAchievementAlert(Achievements.AchievementId achievement)
		{
			TrialAchievementAlert trialAchievementAlert = new TrialAchievementAlert(Dialogs.DIALOG_ACHIEVEMENT_MESSAGE, GlobalStaticVars.gSexyAppBase, GlobalStaticVars.gSexyAppBase, achievement);
			trialAchievementAlert.AddButton(Dialogoid.ButtonID.YES_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_GREEN, Strings.Unlock_Full_Game);
			trialAchievementAlert.AddButton(Dialogoid.ButtonID.NO_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_RED, Strings.BUY_LATER);
			trialAchievementAlert.DefaultButton = 1001;
			return trialAchievementAlert;
		}

		private static void AwardingAchievementCallback(IAsyncResult result)
		{
			SignedInGamer gamer = Main.GetGamer();
			if (gamer != null)
			{
				gamer.EndAwardAchievement(result);
				ReportAchievement.StartGetAchievements();
			}
		}

		public static void StartGetAchievements()
		{
			if (Main.GetGamer() == null)
			{
				return;
			}
			SignedInGamer gamer = Main.GetGamer();
			gamer.BeginGetAchievements(new AsyncCallback(ReportAchievement.GetAchievementsCallback), gamer);
		}

		internal static void LeftTrialMode()
		{
			foreach (Achievements.AchievementId achievement in ReportAchievement.pendingAchievements)
			{
				ReportAchievement.GiveAchievement(achievement, true);
			}
		}

		internal static void ReportUltimateCascade(int cascadeCount)
		{
			if (cascadeCount >= 4 && cascadeCount >= 8)
			{
				ReportAchievement.GiveAchievement(Achievements.AchievementId.Ultimate_Cascade);
			}
		}

		internal static void ReportHyperBlast(int gemsCleared)
		{
			if (gemsCleared >= 12)
			{
				ReportAchievement.GiveAchievement(Achievements.AchievementId.Hyperblast);
				if (gemsCleared >= 16)
				{
					ReportAchievement.GiveAchievement(Achievements.AchievementId.Hypercube_Supremo);
				}
			}
		}

		internal static void ReportSingularity()
		{
			ReportAchievement.GiveAchievement(Achievements.AchievementId.Singularity);
		}

		internal static void ReportChainReaction(int specialGemsDetonated)
		{
			if (specialGemsDetonated >= 3)
			{
				ReportAchievement.GiveAchievement(Achievements.AchievementId.Chain_Reaction);
				if (specialGemsDetonated >= 5)
				{
					ReportAchievement.GiveAchievement(Achievements.AchievementId.Chain_Detonation);
				}
			}
		}

		internal static void ReportPowerBroker(int specialGemsCreated)
		{
			if (specialGemsCreated >= 2)
			{
				if (specialGemsCreated >= 4)
				{
					ReportAchievement.GiveAchievement(Achievements.AchievementId.Power_Monger);
				}
				ReportAchievement.GiveAchievement(Achievements.AchievementId.Power_Broker);
			}
		}

		internal static void ReportSpectrum(int consecutiveColourMatches)
		{
			if (consecutiveColourMatches >= 3)
			{
				ReportAchievement.GiveAchievement(Achievements.AchievementId.Spectrum);
				if (consecutiveColourMatches >= 5)
				{
					ReportAchievement.GiveAchievement(Achievements.AchievementId.Spectrum_Mastery);
				}
			}
		}

		internal static void ReportSuperNova(int starGemsActivated)
		{
			if (starGemsActivated < 4)
			{
				return;
			}
			ReportAchievement.GiveAchievement(Achievements.AchievementId.Supernova);
		}

		internal static void ReportSuperconductor(int[] oneMoveGameColourCounter, int[] oneMoveGameTypeCounter)
		{
			for (int i = 0; i < oneMoveGameColourCounter.Length; i++)
			{
				if (oneMoveGameColourCounter[i] == 0)
				{
					return;
				}
			}
			for (int j = 0; j < oneMoveGameTypeCounter.Length; j++)
			{
				if (oneMoveGameTypeCounter[j] == 0)
				{
					return;
				}
			}
			ReportAchievement.GiveAchievement(Achievements.AchievementId.Superconductor);
		}

		internal static void ReportFasterThanLight()
		{
			ReportAchievement.GiveAchievement(Achievements.AchievementId.FasterThanLight);
		}

		internal static void ReportBurnNotice()
		{
			ReportAchievement.GiveAchievement(Achievements.AchievementId.Burn_Notice);
		}

		internal static void ReportDynamo(int movesMadeBeforeSettling)
		{
			if (movesMadeBeforeSettling < 5)
			{
				return;
			}
			ReportAchievement.GiveAchievement(Achievements.AchievementId.Dynamo);
		}

		internal static void ReportActionHero(int pointsScored)
		{
			if (pointsScored < 500000)
			{
				return;
			}
			ReportAchievement.GiveAchievement(Achievements.AchievementId.ActionHero);
		}

		internal static void Report7thHeaven(int levelReached)
		{
			if (levelReached < 7)
			{
				return;
			}
			ReportAchievement.GiveAchievement(Achievements.AchievementId.SeventhHeaven);
		}

		internal static void ReportClassicRock(int levelReached)
		{
			if (levelReached < 10)
			{
				return;
			}
			ReportAchievement.GiveAchievement(Achievements.AchievementId.Classic_Rock);
		}

		internal static void ReportInferno(int flameGemsCreated)
		{
			if (flameGemsCreated < 100)
			{
				return;
			}
			ReportAchievement.GiveAchievement(Achievements.AchievementId.Inferno);
		}

		internal static void ReportMillionaire(int gemsCollected)
		{
			if (gemsCollected < 1000000)
			{
				return;
			}
			ReportAchievement.GiveAchievement(Achievements.AchievementId.Millionaire);
		}

		private const int SUPERCONDUCTOR_LIMIT = 4;

		private const int DYNAMIC_BOARD_LIMIT = 5;

		private const int HIGH_FLYER_LIMIT = 500000;

		private const int SEVENTH_HEAVEN_LIMIT = 7;

		private const int FIRST_CLASS_LIMIT = 10;

		private const int PERFECTION_LEVEL_LIMIT = 0;

		private const int PERFECTION_GAME_COUNT = 3;

		private const int MONOCOLOURIST_APPRENTICE_LIMIT = 3;

		private const int MONOCOLOURIST_EXPERT_LIMIT = 5;

		private const int CASCADE_APPRENTICE_LIMIT = 4;

		private const int CASCADE_EXPERT_LIMIT = 8;

		private const int HYPERACTIVE_APPRENTICE_LIMIT = 12;

		private const int HYPERACTIVE_EXPERT_LIMIT = 16;

		private const int CHAINREACTOR_APPRENTICE_LIMIT = 3;

		private const int CHAINREACTOR_EXPERT_LIMIT = 5;

		private const int POWERBROKER_APPRENTICE_LIMIT = 2;

		private const int POWERBROKER_EXPERT_LIMIT = 4;

		private const int INFERNO_LIMIT = 100;

		private const int MILLIONAIRE_LIMIT = 1000000;

		private static Queue<TrialAchievementAlert> achievementAlertsToShow = new Queue<TrialAchievementAlert>(10);

		private static object achievementLock = new object();

		private static List<Achievements.AchievementId> pendingAchievements = new List<Achievements.AchievementId>();

		private static AchievementCollection achievements;

		private static ReportAchievement.GameState gamestate = ReportAchievement.GameState.WaitingForSignIn;

		private static string e;

		private enum GameState
		{
			Error,
			WaitingForSignIn,
			WaitingForAchivements,
			Ready
		}
	}
}
