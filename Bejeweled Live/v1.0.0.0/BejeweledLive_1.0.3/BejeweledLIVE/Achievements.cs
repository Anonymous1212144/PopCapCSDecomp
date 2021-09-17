using System;
using System.Collections.Generic;

namespace BejeweledLIVE
{
	public static class Achievements
	{
		static Achievements()
		{
			Achievements.ACHIEVEMENT_KEYS = new string[20];
			Achievements.ACHIEVEMENT_KEYS[0] = "UltimateCascade";
			Achievements.ACHIEVEMENT_KEYS[1] = "Hyperblast";
			Achievements.ACHIEVEMENT_KEYS[2] = "HypercubeSupremo";
			Achievements.ACHIEVEMENT_KEYS[3] = "Singularity";
			Achievements.ACHIEVEMENT_KEYS[4] = "ChainReaction";
			Achievements.ACHIEVEMENT_KEYS[5] = "ChainDetonation";
			Achievements.ACHIEVEMENT_KEYS[6] = "PowerBroker";
			Achievements.ACHIEVEMENT_KEYS[7] = "PowerMonger";
			Achievements.ACHIEVEMENT_KEYS[8] = "Spectrum";
			Achievements.ACHIEVEMENT_KEYS[9] = "SpectrumMastery";
			Achievements.ACHIEVEMENT_KEYS[10] = "Supernova";
			Achievements.ACHIEVEMENT_KEYS[11] = "Superconductor";
			Achievements.ACHIEVEMENT_KEYS[12] = "BurnNotice";
			Achievements.ACHIEVEMENT_KEYS[13] = "FasterThanLight";
			Achievements.ACHIEVEMENT_KEYS[14] = "Dynamo";
			Achievements.ACHIEVEMENT_KEYS[15] = "ActionHero";
			Achievements.ACHIEVEMENT_KEYS[16] = "SeventhHeaven";
			Achievements.ACHIEVEMENT_KEYS[17] = "ClassicRock";
			Achievements.ACHIEVEMENT_KEYS[18] = "Inferno";
			Achievements.ACHIEVEMENT_KEYS[19] = "Millionaire";
		}

		public static AchievementItem GetAchievementItem(Achievements.AchievementId index)
		{
			string achievementKey = Achievements.GetAchievementKey(index);
			for (int i = 0; i < Achievements.gAchievementList.Count; i++)
			{
				if (Achievements.gAchievementList[i].Key == achievementKey)
				{
					return Achievements.gAchievementList[i];
				}
			}
			return null;
		}

		public static string GetAchievementKey(Achievements.AchievementId index)
		{
			return Achievements.ACHIEVEMENT_KEYS[(int)index];
		}

		public static void ClearAchievements()
		{
			foreach (AchievementItem achievementItem in Achievements.gAchievementList)
			{
				achievementItem.Dispose();
			}
			Achievements.gAchievementList.Clear();
		}

		public static int GetNumberOfAchievements()
		{
			return Achievements.gAchievementList.Count;
		}

		public static void AddAchievement(AchievementItem item)
		{
			Achievements.gAchievementList.Add(item);
		}

		public static readonly string[] ACHIEVEMENT_KEYS;

		private static List<AchievementItem> gAchievementList = new List<AchievementItem>();

		public enum AchievementId
		{
			Ultimate_Cascade,
			Hyperblast,
			Hypercube_Supremo,
			Singularity,
			Chain_Reaction,
			Chain_Detonation,
			Power_Broker,
			Power_Monger,
			Spectrum,
			Spectrum_Mastery,
			Supernova,
			Superconductor,
			Burn_Notice,
			FasterThanLight,
			Dynamo,
			ActionHero,
			SeventhHeaven,
			Classic_Rock,
			Inferno,
			Millionaire,
			MAX_NOT_DEFINED
		}
	}
}
