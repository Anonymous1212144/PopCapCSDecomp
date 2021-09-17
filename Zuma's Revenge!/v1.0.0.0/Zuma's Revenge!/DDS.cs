using System;
using System.Collections.Generic;
using System.Linq;

namespace ZumasRevenge
{
	public class DDS
	{
		private static int LERPint(int vmin, int vmax, int level, int range)
		{
			return (int)((float)(vmax - vmin) / (float)range) * level + vmin;
		}

		private static float LERPfloat(float vmin, float vmax, int level, int range)
		{
			return (vmax - vmin) / (float)range * (float)level + vmin;
		}

		protected void GetLERPedValues()
		{
			if (this.mGauntletTime == -1 || this.mBoard.mLevel.mNumCurves == 0)
			{
				return;
			}
			List<Gauntlet_Vals> list = this.mGauntletVals[this.mBoard.mLevel.mNumCurves - 1];
			Gauntlet_Vals gauntlet_Vals = null;
			Gauntlet_Vals gauntlet_Vals2 = null;
			for (int i = 0; i < Enumerable.Count<Gauntlet_Vals>(list); i++)
			{
				if (gauntlet_Vals == null && this.mGauntletTime == list[i].mDifficultyLevel)
				{
					this.mCurrentGauntletVals = list[i];
					return;
				}
				if (gauntlet_Vals == null && this.mGauntletTime < list[i].mDifficultyLevel)
				{
					gauntlet_Vals = list[i - 1];
					gauntlet_Vals2 = list[i];
					break;
				}
				if (gauntlet_Vals != null && this.mGauntletTime < list[i].mDifficultyLevel)
				{
					gauntlet_Vals2 = list[i];
					break;
				}
			}
			if (gauntlet_Vals == null && gauntlet_Vals2 == null)
			{
				int mDifficultyLevel = list[Enumerable.Count<Gauntlet_Vals>(list) - 1].mDifficultyLevel;
				gauntlet_Vals = list[Enumerable.Count<Gauntlet_Vals>(list) - 2];
				gauntlet_Vals2 = list[Enumerable.Count<Gauntlet_Vals>(list) - 1];
			}
			int level = this.mGauntletTime - gauntlet_Vals.mDifficultyLevel;
			int range = Math.Abs(gauntlet_Vals2.mDifficultyLevel - gauntlet_Vals.mDifficultyLevel);
			this.mCurrentGauntletVals.mSpeed = DDS.LERPfloat(gauntlet_Vals.mSpeed, gauntlet_Vals2.mSpeed, level, range);
			this.mCurrentGauntletVals.mStartDistance = DDS.LERPint(gauntlet_Vals.mStartDistance, gauntlet_Vals2.mStartDistance, level, range);
			this.mCurrentGauntletVals.mZumaScore = DDS.LERPint(gauntlet_Vals.mZumaScore, gauntlet_Vals2.mZumaScore, level, range);
			this.mCurrentGauntletVals.mBallRepeat = DDS.LERPint(gauntlet_Vals.mBallRepeat, gauntlet_Vals2.mBallRepeat, level, range);
			this.mCurrentGauntletVals.mPowerupChance = DDS.LERPint(gauntlet_Vals.mPowerupChance, gauntlet_Vals2.mPowerupChance, level, range);
			this.mCurrentGauntletVals.mRollbackPct = DDS.LERPint(gauntlet_Vals.mRollbackPct, gauntlet_Vals2.mRollbackPct, level, range);
			this.mCurrentGauntletVals.mSlowFactor = DDS.LERPfloat(gauntlet_Vals.mSlowFactor, gauntlet_Vals2.mSlowFactor, level, range);
			this.mCurrentGauntletVals.mMaxClumpSize = DDS.LERPint(gauntlet_Vals.mMaxClumpSize, gauntlet_Vals2.mMaxClumpSize, level, range);
			this.mCurrentGauntletVals.mMaxSingle = DDS.LERPint(gauntlet_Vals.mMaxSingle, gauntlet_Vals2.mMaxSingle, level, range);
			this.mCurrentGauntletVals.mRollbackTime = DDS.LERPint(gauntlet_Vals.mRollbackTime, gauntlet_Vals2.mRollbackTime, level, range);
			this.mCurrentGauntletVals.mDifficultyLevel = this.mGauntletTime;
			this.mCurrentGauntletVals.mNumColors = gauntlet_Vals.mNumColors;
			if (this.mCurrentGauntletVals.mPowerupChance < 1)
			{
				this.mCurrentGauntletVals.mPowerupChance = 1;
			}
			if (this.mCurrentGauntletVals.mBallRepeat < 1)
			{
				this.mCurrentGauntletVals.mBallRepeat = 1;
			}
			if (this.mCurrentGauntletVals.mStartDistance > 95)
			{
				this.mCurrentGauntletVals.mStartDistance = 95;
			}
		}

		public DDS()
		{
			this.mApp = GameApp.gApp;
			this.mBoard = null;
			this.mProfile = null;
			this.mMaxPowerupPct = 0.25f;
			this.mMaxSlowPct = 2f;
			this.mMaxSpeedPct = 0.1f;
			this.mMaxSameColorPct = 0.1f;
			this.mMaxStartDistPct = 0.02f;
			this.mMaxZumaBackAdd = 100;
			this.mMaxZumaSlowAdd = 100;
			this.mMinLevel = 3;
			this.mGauntletTime = -1;
			this.mCurGauntletDiffIdx = -1;
			this.mGauntletTimeAdd = 0;
			this.mHandheldBalance.mFruitPowerupAdditionalDuration = 1f;
			this.mHandheldBalance.mChanceOfSameColorBallIncrease = 1f;
			for (int i = 0; i < DDS.NUM_ADVENTURE_ZONES; i++)
			{
				this.mHandheldBalance.mAdventureModeSpeedDelta[i] = 1f;
			}
			for (int j = 0; j < DDS.NUM_CHALLENGE_LEVELS; j++)
			{
				this.mHandheldBalance.mChallengeModeSpeedDelta[j] = 1f;
			}
			for (int k = 0; k < 4; k++)
			{
				this.mGauntletVals[k] = new List<Gauntlet_Vals>();
			}
			for (int l = 0; l < 4; l++)
			{
				this.mVals[l] = new DDS_Vals();
			}
		}

		public void StartLevel(Level l)
		{
			if (l.mBoss != null)
			{
				this.mProfile.BossLevelStarted();
			}
			LevelMgr levelMgr = this.mApp.GetLevelMgr();
			int num = this.mApp.mUserProfile.GetAdvModeVars().mDDSTier;
			if (num >= levelMgr.mNumDDSTiers)
			{
				num = levelMgr.mNumDDSTiers - 1;
			}
			int num2 = 0;
			if (num >= 0)
			{
				num2 = levelMgr.mDDSSlowAdd[num];
			}
			float num3 = 1f;
			if (num >= 0)
			{
				num3 = levelMgr.mDDSSpeedPct[num];
			}
			for (int i = 0; i < l.mNumCurves; i++)
			{
				DDS_Vals dds_Vals = this.mVals[i];
				CurveDesc mCurveDesc = l.mCurveMgr[i].mCurveDesc;
				dds_Vals.mSlowDistance = mCurveDesc.mVals.mSlowDistance + num2;
				dds_Vals.mSpeed = mCurveDesc.mVals.mSpeed * num3;
				dds_Vals.mBallRepeat = mCurveDesc.mVals.mBallRepeat;
				dds_Vals.mZumaBack = mCurveDesc.mVals.mZumaBack;
				dds_Vals.mZumaSlow = mCurveDesc.mVals.mZumaSlow;
				dds_Vals.mStartDistance = mCurveDesc.mVals.mStartDistance;
				for (int j = 0; j < 14; j++)
				{
					dds_Vals.mPowerUpFreq[j] = mCurveDesc.mVals.mPowerUpFreq[j];
				}
			}
			if (this.mBoard.GauntletMode())
			{
				this.GetLERPedValues();
			}
		}

		public void ChangeProfile(ZumaProfile p)
		{
			this.mProfile = p;
		}

		public void AddBossParam(string boss_name, string param_name, float _min, float _max, float range_min, float range_max)
		{
			Boss_DDS_Vals boss_DDS_Vals;
			if (this.mBossVals.ContainsKey(boss_name))
			{
				boss_DDS_Vals = this.mBossVals[boss_name];
			}
			else
			{
				Boss_DDS_Vals boss_DDS_Vals2 = new Boss_DDS_Vals();
				boss_DDS_Vals2.mBossName = boss_name;
				this.mBossVals[boss_name] = boss_DDS_Vals2;
				boss_DDS_Vals = boss_DDS_Vals2;
			}
			Boss_Param_Range boss_Param_Range = new Boss_Param_Range();
			boss_Param_Range.mMin = _min;
			boss_Param_Range.mMax = _max;
			boss_Param_Range.mRatingMin = range_min;
			boss_Param_Range.mRatingMax = range_max;
			boss_DDS_Vals.mParams[param_name].Add(boss_Param_Range);
		}

		public Dictionary<string, Boss_DDS_Vals> getBossParams()
		{
			return this.mBossVals;
		}

		public void AddBossParam(string boss_name, string param_name, float _min, float _max)
		{
			this.AddBossParam(boss_name, param_name, -1f, -1f);
		}

		public float GetBossParam(string param_name)
		{
			string mName = this.mBoard.mLevel.mBoss.mName;
			Boss_DDS_Vals boss_DDS_Vals = null;
			this.mBossVals.TryGetValue(mName, ref boss_DDS_Vals);
			List<Boss_Param_Range> list = null;
			boss_DDS_Vals.mParams.TryGetValue(param_name, ref list);
			List<Boss_Param_Range> list2 = list;
			float num = 0.5f;
			for (int i = 0; i < Enumerable.Count<Boss_Param_Range>(list2); i++)
			{
				Boss_Param_Range boss_Param_Range = list2[i];
				if (boss_Param_Range.InRange(num))
				{
					float num2 = (MathUtils._geq(boss_Param_Range.mRatingMax, 1f) ? 1f : boss_Param_Range.mRatingMax) - boss_Param_Range.mRatingMin;
					float num3 = ((num2 == 0f || boss_Param_Range.mRatingMin < 0f || boss_Param_Range.mRatingMax < 0f) ? num : ((num - boss_Param_Range.mRatingMin) / num2));
					float num4 = boss_Param_Range.mMin * (1f - num3) + num3 * boss_Param_Range.mMax;
					if (boss_Param_Range.mMin < boss_Param_Range.mMax)
					{
						if (num4 < boss_Param_Range.mMin)
						{
							num4 = boss_Param_Range.mMin;
						}
						else if (num4 > boss_Param_Range.mMax)
						{
							num4 = boss_Param_Range.mMax;
						}
					}
					else if (num4 < boss_Param_Range.mMax)
					{
						num4 = boss_Param_Range.mMax;
					}
					else if (num4 > boss_Param_Range.mMin)
					{
						num4 = boss_Param_Range.mMin;
					}
					return num4;
				}
			}
			Boss_Param_Range boss_Param_Range2 = list2[Enumerable.Count<Boss_Param_Range>(list2) - 1];
			return boss_Param_Range2.mMin * (1f - num) + boss_Param_Range2.mMax * num;
		}

		public bool HasBossParam(string param_name)
		{
			string mName = this.mBoard.mLevel.mBoss.mName;
			if (!this.mBossVals.ContainsKey(mName))
			{
				return false;
			}
			Boss_DDS_Vals boss_DDS_Vals = this.mBossVals[mName];
			return boss_DDS_Vals.mParams.ContainsKey("param_name");
		}

		public void UserLostBossLevel(float boss_health)
		{
		}

		public void BossLevelComplete()
		{
			this.mProfile.BossLevelComplete();
		}

		public void GetBossDebugString(ref string str, bool colorize)
		{
			if (!this.mBossVals.ContainsKey(this.mBoard.mLevel.mId))
			{
				str = "No Boss DDS defined";
				return;
			}
			Boss_DDS_Vals boss_DDS_Vals = this.mBossVals[this.mBoard.mLevel.mId];
			Dictionary<string, List<Boss_Param_Range>>.Enumerator enumerator = boss_DDS_Vals.mParams.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!colorize)
				{
					object obj = str;
					object[] array = new object[4];
					array[0] = obj;
					object[] array2 = array;
					int num = 1;
					KeyValuePair<string, List<Boss_Param_Range>> keyValuePair = enumerator.Current;
					array2[num] = keyValuePair.Key;
					array[2] = ": ";
					object[] array3 = array;
					int num2 = 3;
					KeyValuePair<string, List<Boss_Param_Range>> keyValuePair2 = enumerator.Current;
					array3[num2] = this.GetBossParam(keyValuePair2.Key);
					str = string.Concat(array);
				}
				else
				{
					object obj2 = str;
					object[] array4 = new object[4];
					array4[0] = obj2;
					object[] array5 = array4;
					int num3 = 1;
					KeyValuePair<string, List<Boss_Param_Range>> keyValuePair3 = enumerator.Current;
					array5[num3] = keyValuePair3.Key;
					array4[2] = ": ";
					object[] array6 = array4;
					int num4 = 3;
					KeyValuePair<string, List<Boss_Param_Range>> keyValuePair4 = enumerator.Current;
					array6[num4] = this.GetBossParam(keyValuePair4.Key);
					str = string.Concat(array4);
				}
			}
		}

		public void AddGauntletVals(Gauntlet_Vals vals, int num_curves)
		{
			num_curves--;
			int count = this.mGauntletVals[num_curves].Count;
			this.mGauntletVals[num_curves].Add(vals);
		}

		public List<Gauntlet_Vals>[] getGauntletVals()
		{
			return this.mGauntletVals;
		}

		public bool SetGauntletTime(int l)
		{
			List<Gauntlet_Vals> list = this.mGauntletVals[this.mBoard.mLevel.mNumCurves - 1];
			if (!list[0].mTimeBaseDifficulty)
			{
				return false;
			}
			if (l == 0)
			{
				this.mCurGauntletDiffIdx = 0;
				this.mCurrentGauntletVals = list[0];
				this.mGauntletTime = (this.mGauntletTimeAdd = 0);
				return false;
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (l + this.mGauntletTimeAdd >= list[i].mDifficultyLevel)
				{
					this.mCurrentGauntletVals = list[i];
					this.mGauntletTime = l;
					int num = this.mCurGauntletDiffIdx;
					this.mCurGauntletDiffIdx = i;
					return i != num;
				}
			}
			return false;
		}

		public bool SetGauntletPoints(int p)
		{
			List<Gauntlet_Vals> list = this.mGauntletVals[this.mBoard.mLevel.mNumCurves - 1];
			if (list[0].mTimeBaseDifficulty)
			{
				return false;
			}
			if (p == 0)
			{
				this.mCurGauntletDiffIdx = 0;
				this.mCurrentGauntletVals = list[0];
				return false;
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (p >= list[i].mDifficultyLevel)
				{
					this.mCurrentGauntletVals = list[i];
					int num = this.mCurGauntletDiffIdx;
					this.mCurGauntletDiffIdx = i;
					return i != num;
				}
			}
			return false;
		}

		public bool AddMultiplierTime(int t)
		{
			this.mGauntletTimeAdd += t;
			return this.SetGauntletTime(this.mGauntletTime);
		}

		public int GetGauntletLevel()
		{
			return this.mGauntletTime;
		}

		public void Reset()
		{
			this.mBossVals.Clear();
			for (int i = 0; i < 4; i++)
			{
				this.mGauntletVals[i].Clear();
			}
		}

		public int GetOverallPowerupChance(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				LevelMgr levelMgr = this.mApp.GetLevelMgr();
				int num = this.mApp.mUserProfile.GetAdvModeVars().mDDSTier;
				if (num >= levelMgr.mNumDDSTiers)
				{
					num = levelMgr.mNumDDSTiers - 1;
				}
				float num2 = 0f;
				if (num >= 0)
				{
					num2 = levelMgr.mDDSPowerupPctInc[num];
				}
				float num3 = (float)this.mBoard.mLevel.mCurveMgr[curve_num].mCurveDesc.mVals.mPowerUpChance;
				return (int)(num3 + num3 * num2);
			}
			return this.mCurrentGauntletVals.mPowerupChance;
		}

		public int GetPowerFreq(int powertype, int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mVals[curve_num].mPowerUpFreq[powertype];
			}
			if (!Common.IsDeprecatedPowerUp((PowerType)powertype) && powertype != 13)
			{
				return 100;
			}
			return 0;
		}

		public int GetSlowDistance(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mVals[curve_num].mSlowDistance;
			}
			return this.mBoard.mLevel.mCurveMgr[curve_num].mCurveDesc.mVals.mSlowDistance;
		}

		public int GetBallRepeat(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mVals[curve_num].mBallRepeat;
			}
			return this.mCurrentGauntletVals.mBallRepeat;
		}

		public int GetStartDistance(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mVals[curve_num].mStartDistance;
			}
			return this.mCurrentGauntletVals.mStartDistance;
		}

		public int GetZumaBack(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mVals[curve_num].mZumaBack;
			}
			return this.mCurrentGauntletVals.mRollbackTime;
		}

		public int GetZumaSlow(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mVals[curve_num].mZumaSlow;
			}
			return this.mBoard.mLevel.mCurveMgr[curve_num].mCurveDesc.mVals.mZumaSlow;
		}

		public int GetZumaScore(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				float num = 0f;
				LevelMgr levelMgr = this.mApp.GetLevelMgr();
				int num2 = this.mApp.mUserProfile.GetAdvModeVars().mDDSTier;
				if (num2 >= levelMgr.mNumDDSTiers)
				{
					num2 = levelMgr.mNumDDSTiers - 1;
				}
				if (num2 >= 0)
				{
					num = levelMgr.mDDSZumaPointDecPct[num2];
				}
				int mScoreTarget = this.mBoard.mLevel.mCurveMgr[curve_num].mCurveDesc.mVals.mScoreTarget;
				return mScoreTarget - (int)(num * (float)mScoreTarget);
			}
			return this.mCurrentGauntletVals.mZumaScore;
		}

		public int GetNumGauntletBalls(int curve_num)
		{
			return this.mCurrentGauntletVals.mNumColors;
		}

		public int GetGauntletDiffLevel()
		{
			return this.mCurrentGauntletVals.mDifficultyLevel;
		}

		public float GetSpeed(int curve_num, bool ignore_gauntlet_dds_level)
		{
			if (!this.mBoard.GauntletMode() || ignore_gauntlet_dds_level)
			{
				return this.mVals[curve_num].mSpeed;
			}
			return this.mCurrentGauntletVals.mSpeed;
		}

		public float GetSpeed(int curve_num)
		{
			return this.GetSpeed(curve_num, false);
		}

		public int GetGauntletHurryDist(int curve_num)
		{
			return this.mGauntletVals[curve_num - 1][0].mHurryDist;
		}

		public float GetGauntletHurryMaxSpeed(int curve_num)
		{
			return this.mGauntletVals[curve_num - 1][0].mHurryMaxSpeed;
		}

		public float GetSlowFactor(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mBoard.mLevel.mCurveMgr[curve_num].mCurveDesc.mVals.mSlowFactor;
			}
			return this.mCurrentGauntletVals.mSlowFactor;
		}

		public int GetRollbackPct(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return -9999;
			}
			return this.mCurrentGauntletVals.mRollbackPct;
		}

		public int GetMaxClumps(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mBoard.mLevel.mCurveMgr[curve_num].mCurveDesc.mVals.mMaxClumpSize;
			}
			return this.mCurrentGauntletVals.mMaxClumpSize;
		}

		public int GetMaxSingles(int curve_num)
		{
			if (!this.mBoard.GauntletMode())
			{
				return this.mBoard.mLevel.mCurveMgr[curve_num].mCurveDesc.mVals.mMaxSingle;
			}
			return this.mCurrentGauntletVals.mMaxSingle;
		}

		public string GetStatsString(bool colorize)
		{
			if (this.mBoard.mLevel.mCurveMgr[0] == null)
			{
				return "";
			}
			LevelMgr levelMgr = this.mApp.GetLevelMgr();
			int num = this.mApp.mUserProfile.GetAdvModeVars().mDDSTier;
			if (num >= levelMgr.mNumDDSTiers)
			{
				num = levelMgr.mNumDDSTiers - 1;
			}
			int num2 = 0;
			if (num >= 0)
			{
				num2 = levelMgr.mDDSSlowAdd[num];
			}
			float num3 = 1f;
			if (num >= 0)
			{
				num3 = levelMgr.mDDSSpeedPct[num];
			}
			float num4 = 0f;
			if (num >= 0)
			{
				num4 = levelMgr.mDDSPowerupPctInc[num];
			}
			float num5 = 0f;
			if (num >= 0)
			{
				num5 = levelMgr.mDDSZumaPointDecPct[num];
			}
			CurveDesc mCurveDesc = this.mBoard.mLevel.mCurveMgr[0].mCurveDesc;
			float num6 = (float)mCurveDesc.mVals.mPowerUpChance;
			num6 += num6 * num4;
			int num7 = mCurveDesc.mVals.mScoreTarget;
			num7 -= (int)(num5 * (float)num7);
			return string.Format("DDS Tier: {0}, Slow dist: {0}, Speed: {0}\nPowerup chance: {0}, Zuma score:{0}", new object[]
			{
				this.mApp.mUserProfile.GetAdvModeVars().mDDSTier,
				mCurveDesc.mVals.mSlowDistance + num2,
				mCurveDesc.mVals.mSpeed * num3,
				num6,
				num7
			});
		}

		public string GetStatsString()
		{
			return this.GetStatsString(true);
		}

		public static int NUM_ADVENTURE_ZONES = 6;

		public static int NUM_CHALLENGE_LEVELS = 16;

		public GameApp mApp;

		public Board mBoard;

		public ZumaProfile mProfile;

		public int mCurGauntletDiffIdx;

		public int mGauntletTime;

		public int mGauntletTimeAdd;

		public float mMaxPowerupPct;

		public float mMaxSlowPct;

		public float mMaxSpeedPct;

		public float mMaxSameColorPct;

		public float mMaxStartDistPct;

		public int mMaxZumaBackAdd;

		public int mMaxZumaSlowAdd;

		public int mMinLevel;

		public HandheldBalance mHandheldBalance = new HandheldBalance();

		protected Gauntlet_Vals mCurrentGauntletVals;

		protected DDS_Vals[] mVals = new DDS_Vals[4];

		protected List<Gauntlet_Vals>[] mGauntletVals = new List<Gauntlet_Vals>[4];

		protected Dictionary<string, Boss_DDS_Vals> mBossVals = new Dictionary<string, Boss_DDS_Vals>();
	}
}
