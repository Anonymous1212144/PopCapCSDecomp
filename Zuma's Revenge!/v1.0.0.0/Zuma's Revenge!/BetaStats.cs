using System;
using SexyFramework;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class BetaStats
	{
		protected void Reset()
		{
			this.mBossHP = 0;
			this.mLives = 0;
			this.mNumDeathsThisLevel = 0;
			this.mLevelTime = (this.mAceTime = 0);
			this.mHighestGapShotScore = 0;
			this.mHighestChainShotPoints = 0;
			this.mHighestComboPoints = 0;
			this.mFurthestRolloutPct = 0f;
			this.mMaxFruitMultiplier = 0;
			this.mPerfectLevelBonus = 0;
			this.mAceBonus = 0;
			this.mLevelScore = (this.mTotalScore = 0);
			this.mLargestGapShot = 0;
			this.mNumGapShots = 0;
			this.mPointsFromGapShots = 0;
			this.mLargestChainShot = 0;
			this.mPointsFromChainShots = 0;
			this.mLargestCombo = 0;
			this.mPointsFromCombos = 0;
			this.mNumClearCurveBonuses = 0;
			this.mPointsFromClearCurve = 0;
			this.mNumFruits = 0;
			this.mPointsFromFruit = 0;
			this.mNumTimesLaserCanceled = 0;
			this.mPointsFromLaser = 0;
			this.mPointsFromCannon = 0;
			this.mPointsFromColorNuke = 0;
			this.mPointsFromProxBomb = 0;
			this.mWasFromCheckpoint = (this.mWasFromZoneRestart = false);
			this.mNumTimesActivatedPowerup = new int[14];
			this.mNumTimesSpawnedPowerup = new int[14];
		}

		protected void SaveCSVFile(int challenge_level, int challenge_mult)
		{
		}

		protected void SaveCSVFile(int challenge_level)
		{
			this.SaveCSVFile(challenge_level, -1);
		}

		protected void SaveCSVFile()
		{
			this.SaveCSVFile(-1, -1);
		}

		protected void Serialize(Buffer b)
		{
		}

		protected bool Deserialize(Buffer b)
		{
			return true;
		}

		public BetaStats()
		{
			this.mSessionID = 0;
			this.mMode = BetaStats.Mode.Mode_None;
			this.mLevelZone = -1;
			this.mLevelNum = -1;
			this.mProfile = null;
			this.Reset();
		}

		public void CopyFrom(BetaStats rhs)
		{
			this.mProfile = rhs.mProfile;
			this.mSessionID = rhs.mSessionID;
			this.mMode = rhs.mMode;
			this.mNumDeathsThisLevel = rhs.mNumDeathsThisLevel;
			this.mLevelName = rhs.mLevelName;
			this.mLevelZone = rhs.mLevelZone;
			this.mLevelNum = rhs.mLevelNum;
			this.mLevelTime = rhs.mLevelTime;
			this.mAceTime = rhs.mAceTime;
			this.mNumGapShots = rhs.mNumGapShots;
			this.mLargestGapShot = rhs.mLargestGapShot;
			this.mHighestGapShotScore = rhs.mHighestGapShotScore;
			this.mLargestChainShot = rhs.mLargestChainShot;
			this.mHighestChainShotPoints = rhs.mHighestChainShotPoints;
			this.mLargestCombo = rhs.mLargestCombo;
			this.mHighestComboPoints = rhs.mHighestComboPoints;
			this.mFurthestRolloutPct = rhs.mFurthestRolloutPct;
			this.mNumClearCurveBonuses = rhs.mNumClearCurveBonuses;
			this.mPerfectLevelBonus = rhs.mPerfectLevelBonus;
			this.mAceBonus = rhs.mAceBonus;
			this.mPointsFromClearCurve = rhs.mPointsFromClearCurve;
			this.mPointsFromGapShots = rhs.mPointsFromGapShots;
			this.mPointsFromCombos = rhs.mPointsFromCombos;
			this.mPointsFromChainShots = rhs.mPointsFromChainShots;
			this.mNumFruits = rhs.mNumFruits;
			this.mLives = rhs.mLives;
			this.mBossHP = rhs.mBossHP;
			this.mPointsFromFruit = rhs.mPointsFromFruit;
			this.mMaxFruitMultiplier = rhs.mMaxFruitMultiplier;
			this.mNumTimesLaserCanceled = rhs.mNumTimesLaserCanceled;
			this.mWasFromCheckpoint = rhs.mWasFromCheckpoint;
			this.mWasFromZoneRestart = rhs.mWasFromZoneRestart;
			this.mLevelScore = rhs.mLevelScore;
			this.mTotalScore = rhs.mTotalScore;
			this.mPointsFromLaser = rhs.mPointsFromLaser;
			this.mPointsFromCannon = rhs.mPointsFromCannon;
			this.mPointsFromColorNuke = rhs.mPointsFromColorNuke;
			this.mPointsFromProxBomb = rhs.mPointsFromProxBomb;
			for (int i = 0; i < 14; i++)
			{
				this.mNumTimesActivatedPowerup[i] = rhs.mNumTimesActivatedPowerup[i];
				this.mNumTimesSpawnedPowerup[i] = rhs.mNumTimesSpawnedPowerup[i];
			}
		}

		public string GetCSVFileName()
		{
			switch (this.mMode)
			{
			case BetaStats.Mode.Mode_Challenge:
				return Common.GetAppDataFolder() + "CHALLENGE STATS DO NOT DELETE.csv";
			case BetaStats.Mode.Mode_IronFrog:
				return Common.GetAppDataFolder() + "IRON FROG STATS DO NOT DELETE.csv";
			case BetaStats.Mode.Mode_Adventure:
				return Common.GetAppDataFolder() + "ADVENTURE STATS DO NOT DELETE.csv";
			case BetaStats.Mode.Mode_HardAdventure:
				return Common.GetAppDataFolder() + "HEROIC STATS DO NOT DELETE.csv";
			default:
				return "ERROR.csv";
			}
		}

		public string GetDATFileName()
		{
			string text = "";
			switch (this.mMode)
			{
			case BetaStats.Mode.Mode_Challenge:
				text = "challenge";
				break;
			case BetaStats.Mode.Mode_IronFrog:
				text = "if";
				break;
			case BetaStats.Mode.Mode_Adventure:
				text = "adv";
				break;
			case BetaStats.Mode.Mode_HardAdventure:
				text = "hard_adv";
				break;
			}
			return Common.GetAppDataFolder() + string.Format("users/user{0}_{1}_stats.dat", this.mProfile.GetId(), text);
		}

		public void Init(ZumaProfile p, int session_id, int mode)
		{
			this.mProfile = p;
			this.mSessionID = session_id;
			this.Reset();
			this.mMode = (BetaStats.Mode)mode;
			this.mLevelZone = -1;
			this.mLevelNum = -1;
		}

		public void LevelStarted(string level_name, int zone, int num, bool from_checkpoint, bool zone_restart)
		{
			this.Reset();
			this.mLevelName = level_name;
			this.mLevelZone = zone;
			this.mLevelNum = num;
			this.mWasFromCheckpoint = from_checkpoint;
			this.mWasFromZoneRestart = zone_restart;
		}

		public void BeatLevel(int level_time, int ace_time, int ace_bonus, int perfect_bonus, float rollout_pct, int level_score, int total_score, int lives)
		{
			this.mLives = lives;
			this.mLevelTime = level_time;
			this.mAceTime = ace_time;
			this.mAceBonus = ace_bonus;
			this.mPerfectLevelBonus = perfect_bonus;
			this.mFurthestRolloutPct = rollout_pct;
			this.mLevelScore = level_score;
			this.mTotalScore = total_score;
			this.SaveCSVFile();
		}

		public void DiedOnLevel(int level_time, int level_score, int total_score, int lives_left, int challenge_level, int challenge_multiplier, int boss_hp)
		{
			this.mBossHP = boss_hp;
			this.mLevelScore = level_score;
			this.mTotalScore = total_score;
			this.mLives = lives_left;
			this.mFurthestRolloutPct = 1f;
			this.mNumDeathsThisLevel++;
			this.mLevelTime = level_time;
			this.SaveCSVFile(challenge_level, challenge_multiplier);
		}

		public void DiedOnLevel(int level_time, int level_score, int total_score, int lives_left, int challenge_level, int challenge_multiplier)
		{
			this.DiedOnLevel(level_time, level_score, total_score, lives_left, challenge_level, challenge_multiplier, 0);
		}

		public void DiedOnLevel(int level_time, int level_score, int total_score, int lives_left, int challenge_level)
		{
			this.DiedOnLevel(level_time, level_score, total_score, lives_left, challenge_level, -1);
		}

		public void DiedOnLevel(int level_time, int level_score, int total_score, int lives_left)
		{
			this.DiedOnLevel(level_time, level_score, total_score, lives_left, -1, -1);
		}

		public void LoadData()
		{
			string datfileName = this.GetDATFileName();
			Buffer b = new Buffer();
			if (GameApp.gApp.ReadBufferFromFile(datfileName, ref b) && !this.Deserialize(b))
			{
				GameApp.gApp.EraseFile(datfileName);
				GameApp.gApp.EraseFile(this.GetCSVFileName());
			}
		}

		public void SaveData()
		{
			Buffer buffer = new Buffer();
			this.Serialize(buffer);
			GameApp.gApp.WriteBufferToFile(this.GetDATFileName(), buffer);
		}

		public void SetFruitMultiplier(int m)
		{
			if (m > this.mMaxFruitMultiplier)
			{
				this.mMaxFruitMultiplier = m;
			}
		}

		public void GapShot(int points, int size)
		{
			this.mNumGapShots++;
			this.mProfile.mNumGapShots++;
			if (size > this.mLargestGapShot)
			{
				this.mLargestGapShot = size;
			}
			if (points > this.mHighestGapShotScore)
			{
				this.mHighestGapShotScore = points;
			}
			if (points > this.mProfile.mHighestGapShotScore)
			{
				this.mProfile.mHighestGapShotScore = points;
			}
			if (this.mLargestGapShot > this.mProfile.mLargestGapShot)
			{
				this.mProfile.mLargestGapShot = this.mLargestGapShot;
			}
			this.mPointsFromGapShots += points;
			this.mProfile.mPointsFromGapShots += points;
		}

		public void ChainShot(int points, int size)
		{
			if (size > this.mLargestChainShot)
			{
				this.mLargestChainShot = size;
			}
			if (points > this.mHighestChainShotPoints)
			{
				this.mHighestChainShotPoints = points;
			}
			this.mPointsFromChainShots += points;
			this.mProfile.mPointsFromChainShots += points;
			if (this.mLargestChainShot > this.mProfile.mLargestChainShot)
			{
				this.mProfile.mLargestChainShot = this.mLargestChainShot;
			}
		}

		public void Combo(int points, int size)
		{
			if (points > this.mHighestComboPoints)
			{
				this.mHighestComboPoints = points;
			}
			if (size > this.mLargestCombo)
			{
				this.mLargestCombo = size;
			}
			this.mPointsFromCombos += points;
			this.mProfile.mPointsFromCombos += points;
			if (this.mLargestCombo > this.mProfile.mLargestCombo)
			{
				this.mProfile.mLargestCombo = this.mLargestCombo;
			}
		}

		public void ClearedCurve(int points)
		{
			this.mNumClearCurveBonuses++;
			this.mPointsFromClearCurve += points;
			this.mProfile.mNumClearCurveBonuses++;
			this.mProfile.mPointsFromClearCurve += points;
			if (this.mNumClearCurveBonuses >= 2)
			{
				GameApp.gApp.SetAchievement("clear_2x");
			}
		}

		public void HitFruit(int points)
		{
			this.mNumFruits++;
			this.mPointsFromFruit += points;
			this.mProfile.mNumFruits++;
			this.mProfile.mPointsFromFruit += points;
		}

		public void CanceledLaser()
		{
			this.mNumTimesLaserCanceled++;
			this.mProfile.mNumTimesLaserCanceled++;
		}

		public void BallExplodedFromPowerup(int power_type)
		{
			if (power_type == 0)
			{
				this.mPointsFromProxBomb += 10;
				this.mProfile.mPointsFromProxBomb += 10;
				return;
			}
			switch (power_type)
			{
			case 7:
				this.mPointsFromCannon += 10;
				this.mProfile.mPointsFromCannon += 10;
				return;
			case 8:
				this.mPointsFromColorNuke += 10;
				this.mProfile.mPointsFromColorNuke += 10;
				return;
			case 9:
				this.mPointsFromLaser += 10;
				this.mProfile.mPointsFromLaser += 10;
				return;
			default:
				return;
			}
		}

		public void ActivatedPowerup(int power_type)
		{
			this.mNumTimesActivatedPowerup[power_type]++;
			this.mProfile.mNumTimesActivatedPowerup[power_type]++;
		}

		public void SpawnedPowerup(int power_type)
		{
			this.mNumTimesSpawnedPowerup[power_type]++;
		}

		protected int mSessionID;

		protected BetaStats.Mode mMode;

		protected int mNumDeathsThisLevel;

		protected string mLevelName;

		protected int mLevelZone;

		protected int mLevelNum;

		protected int mLevelTime;

		protected int mAceTime;

		protected int mNumGapShots;

		protected int mLargestGapShot;

		protected int mHighestGapShotScore;

		protected int mLargestChainShot;

		protected int mHighestChainShotPoints;

		protected int mLargestCombo;

		protected int mHighestComboPoints;

		protected float mFurthestRolloutPct;

		protected int mNumClearCurveBonuses;

		protected int mPerfectLevelBonus;

		protected int mAceBonus;

		protected int mPointsFromClearCurve;

		protected int mPointsFromGapShots;

		protected int mPointsFromCombos;

		protected int mPointsFromChainShots;

		protected int mNumFruits;

		protected int mLives;

		protected int mBossHP;

		protected int mPointsFromFruit;

		protected int mMaxFruitMultiplier;

		protected int mNumTimesLaserCanceled;

		protected bool mWasFromCheckpoint;

		protected bool mWasFromZoneRestart;

		protected int mLevelScore;

		protected int mTotalScore;

		protected int mPointsFromLaser;

		protected int mPointsFromCannon;

		protected int mPointsFromColorNuke;

		protected int mPointsFromProxBomb;

		protected int[] mNumTimesActivatedPowerup = new int[14];

		protected int[] mNumTimesSpawnedPowerup = new int[14];

		public ZumaProfile mProfile;

		public enum Mode
		{
			Mode_Challenge,
			Mode_IronFrog,
			Mode_Adventure,
			Mode_HardAdventure,
			Mode_None
		}
	}
}
