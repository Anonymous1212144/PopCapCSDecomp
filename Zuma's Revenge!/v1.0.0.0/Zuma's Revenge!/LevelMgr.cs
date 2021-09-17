using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class LevelMgr
	{
		protected bool SetupCurveInfoFromXML(string curve_str, string value, Level l)
		{
			string text = curve_str.Substring(5);
			if (text.Length == 0)
			{
				return this.Fail("Expected \"curve\" followed by a number, like \"curve1\" instead of just \"curve\".");
			}
			int num = Convert.ToInt32(text, 10);
			if (num - 1 >= 4 || num - 1 < 0)
			{
				return this.Fail(string.Format("Curve number must be in the range from 1-{0}", 4));
			}
			if (l.mCurveMgr[num - 1] != null)
			{
				return this.Fail(string.Format("Curve number {0} is already defined", num));
			}
			if (num > l.mNumCurves)
			{
				l.mNumCurves = num;
			}
			l.mCurveMgr[num - 1] = new CurveMgr(null, num - 1);
			l.mCurveMgr[num - 1].mLevel = l;
			string text2 = value;
			if (this.mIsHardConfig)
			{
				text2 += "_hard";
			}
			l.mCurveMgr[num - 1].SetPath(text2);
			return true;
		}

		protected bool Fail(string theErrorText)
		{
			if (!this.mHasFailed)
			{
				Console.WriteLine("LevelMrg::Failed parsing binary .xml\n");
			}
			return false;
		}

		protected bool DoParseLevels()
		{
			int num = -1;
			int num2 = 0;
			LevelMgr.gBossNum = 0;
			bool flag = false;
			int num3 = -1;
			if (!this.mXMLParser.HasFailed())
			{
				BXMLElement bxmlelement;
				for (;;)
				{
					bxmlelement = new BXMLElement();
					if (!this.mXMLParser.NextElement(ref bxmlelement))
					{
						goto IL_12B7;
					}
					if (bxmlelement.mType == 1)
					{
						if (!flag && bxmlelement.mValue == "Dot")
						{
							int num4 = int.Parse(bxmlelement.mAttributes["num"]);
							int theX = int.Parse(bxmlelement.mAttributes["x"]);
							int theY = int.Parse(bxmlelement.mAttributes["y"]);
							if (num4 > this.mMapPoints.size<Point>())
							{
								this.mMapPoints.Resize(num4);
							}
							this.mMapPoints[num4 - 1] = new Point(theX, theY);
						}
						else if (!flag && bxmlelement.mValue == "DDS")
						{
							if (!this.DoParseDDS(bxmlelement))
							{
								break;
							}
						}
						else if (!flag && bxmlelement.mValue == "HandheldBalance")
						{
							while (this.mXMLParser.NextElement(ref bxmlelement))
							{
								string mValue = bxmlelement.mValue;
								if (bxmlelement.mType == 1)
								{
									if (bxmlelement.mValue == "Tablet" && GameApp.IsTablet())
									{
										if (!this.DoParseHandheldBalance(bxmlelement))
										{
											return false;
										}
									}
									else if (bxmlelement.mValue == "Phone" && !GameApp.IsTablet() && !this.DoParseHandheldBalance(bxmlelement))
									{
										return false;
									}
								}
								else if (bxmlelement.mType == 2 && bxmlelement.mValue == "HandheldBalance")
								{
									break;
								}
							}
						}
						else if (!flag && bxmlelement.mValue == "Level")
						{
							flag = true;
							Level level = null;
							if (bxmlelement.mValue == "Level")
							{
								level = new Level();
								this.mLevels.Add(level);
								level.mIndex = this.mLevels.Count - 1;
							}
							level.mChallengePoints = -1;
							level.mChallengeAcePoints = -1;
							foreach (KeyValuePair<string, string> keyValuePair in bxmlelement.mAttributes)
							{
								if (level == null)
								{
									break;
								}
								if (keyValuePair.Key == "id")
								{
									level.mId = keyValuePair.Value;
								}
								else if (keyValuePair.Key == "challengepoints")
								{
									level.mChallengePoints = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "acechallenge")
								{
									level.mChallengeAcePoints = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "OffscreenClearBonus")
								{
									level.mOffscreenClearBonus = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "gauntlet" || keyValuePair.Key == "gauntletlevel")
								{
									level.mStartingGauntletLevel = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "dispname")
								{
									level.mDisplayName = keyValuePair.Value;
								}
								else if (keyValuePair.Key == "endsequence")
								{
									level.mEndSequence = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "hurryamt")
								{
									level.mHurryToRolloutAmt = float.Parse(keyValuePair.Value, 167, CultureInfo.InvariantCulture);
								}
								else if (keyValuePair.Key == "ironfrog")
								{
									level.mIronFrog = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "torchtime")
								{
									level.mTorchTimer = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "bossfreeze")
								{
									level.mBossFreezePowerupTime = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "frogshield")
								{
									level.mFrogShieldPowerupCount = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "drawcurve")
								{
									level.mDrawCurves = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "background")
								{
									level.mImagePath = keyValuePair.Value;
								}
								else if (keyValuePair.Key == "psd")
								{
									level.mBGFromPSD = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "nobackground")
								{
									level.mNoBackground = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "noflip")
								{
									level.mNoFlip = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "edgerotate")
								{
									level.mSliderEdgeRotate = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "suckmode")
								{
									level.mSuckMode = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "curvedata")
								{
									num3 = int.Parse(keyValuePair.Value) - 1;
								}
								else if (keyValuePair.Key == "tfreq")
								{
									level.mTreasureFreq = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "time")
								{
									level.mTimeToComplete = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "partime" || keyValuePair.Key == "par")
								{
									level.mParTime = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "endless")
								{
									level.mIsEndless = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "loopatend" || keyValuePair.Key == "loop")
								{
									level.mLoopAtEnd = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "inverttime")
								{
									level.mMaxInvertMouseTimer = int.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "potpct")
								{
									level.mPotPct = float.Parse(keyValuePair.Value, 167, CultureInfo.InvariantCulture) / 100f;
								}
								else if (keyValuePair.Key == "NextLevelText")
								{
									level.mPreviewText = keyValuePair.Value;
								}
								else if (keyValuePair.Key == "finallevel")
								{
									level.mFinalLevel = bool.Parse(keyValuePair.Value);
								}
								else if (keyValuePair.Key == "popup")
								{
									level.mPopupText = keyValuePair.Value;
								}
								else if (JeffLib.Common.StrFindNoCase(keyValuePair.Key, "effect") != -1)
								{
									string value = keyValuePair.Value;
									if (value != "ShadowCanopy1" && value != "ShadowCanopy2" && value != "ShadowCanopy3")
									{
										level.mEffectNames.Add(value);
									}
								}
								else if (keyValuePair.Key.StartsWith("curve"))
								{
									if (keyValuePair.Key.IndexOf("skullangle") != -1)
									{
										int num5 = (int)(keyValuePair.Key.get_Chars(5) - '0' - '\u0001');
										level.mCurveSkullAngleOverrides[num5] = MathUtils.DegreesToRadians(float.Parse(keyValuePair.Value, 167, CultureInfo.InvariantCulture));
									}
									else if (!this.SetupCurveInfoFromXML(keyValuePair.Key, keyValuePair.Value, level))
									{
										return false;
									}
								}
								else
								{
									level.ParseUnknownAttribute(keyValuePair.Key, keyValuePair.Value);
								}
							}
							if (level.mIronFrog && level.mChallengePoints == -1)
							{
								level.mChallengePoints = this.mZones[6].mChallengePoints;
							}
							if (level.mIronFrog && level.mChallengeAcePoints == -1)
							{
								level.mChallengeAcePoints = this.mZones[6].mChallengeAcePoints;
							}
							if (!level.mIronFrog && JeffLib.Common.StrFindNoCase(level.mId, "debug") == -1)
							{
								bool flag2 = false;
								for (int i = 0; i < 6; i++)
								{
									if (this.mZones[i].mStartLevel == level.mId)
									{
										num = i;
										num2 = 0;
										break;
									}
									if (JeffLib.Common.StrFindNoCase(level.mId, this.mZones[i].mBossPrefix) != -1)
									{
										num = i;
										flag2 = true;
										break;
									}
								}
								level.mZone = num + 1;
								if (!flag2)
								{
									num2 = (level.mNum = num2 + 1);
								}
								else
								{
									level.mNum = int.MaxValue;
									num2 = 0;
									num = -1;
								}
								if (level.mChallengePoints == -1)
								{
									level.mChallengePoints = this.mZones[level.mZone - 1].mChallengePoints;
								}
								if (level.mChallengeAcePoints == -1)
								{
									level.mChallengeAcePoints = this.mZones[level.mZone - 1].mChallengeAcePoints;
								}
							}
							if (level.mFinalLevel || JeffLib.Common.StrFindNoCase(level.mId, "boss6") != -1)
							{
								level.mNum = int.MaxValue;
								level.mZone = 6;
							}
						}
						else if (flag && bxmlelement.mValue == "TreasurePoint")
						{
							Level level2 = this.mLevels.back<Level>();
							level2.mTreasurePoints.Add(new TreasurePoint());
							if (!this.DoParseTreasure(bxmlelement, level2.mTreasurePoints.back<TreasurePoint>()))
							{
								return false;
							}
						}
						else if (flag && bxmlelement.mValue == "SetEffectParams")
						{
							Level l = this.mLevels.back<Level>();
							if (!this.DoParseSetEffectParams(bxmlelement, l))
							{
								return false;
							}
						}
						else if (!flag && bxmlelement.mValue == "Gauntlet")
						{
							if (!this.DoParseGauntletMode(bxmlelement))
							{
								return false;
							}
						}
						else if (!flag && bxmlelement.mValue == "Tip")
						{
							string text = "";
							if (!this.GetAttribute(bxmlelement, "text", ref text))
							{
								goto Block_47;
							}
							this.mLevelTips.Add(text);
						}
						else if (!flag && bxmlelement.mValue == "ScoreTip")
						{
							string t = "";
							if (!this.GetAttribute(bxmlelement, "text", ref t))
							{
								goto Block_50;
							}
							int l2 = -1;
							string text2 = "";
							if (this.GetAttribute(bxmlelement, "minlevel", ref text2))
							{
								l2 = int.Parse(text2);
							}
							this.mScoreTips.Add(new ScoreTip(t, l2));
						}
						else if (!flag && bxmlelement.mValue == "Zone")
						{
							string text3 = "";
							if (!this.GetAttribute(bxmlelement, "num", ref text3))
							{
								goto Block_54;
							}
							int num6 = int.Parse(text3);
							string mStartLevel = "";
							if (!this.GetAttribute(bxmlelement, "start", ref mStartLevel))
							{
								goto Block_55;
							}
							int mChallengePoints = 100;
							if (this.GetAttribute(bxmlelement, "challengepoints", ref text3))
							{
								mChallengePoints = int.Parse(text3);
							}
							int mChallengeAcePoints = 1000;
							if (this.GetAttribute(bxmlelement, "acechallenge", ref text3))
							{
								mChallengeAcePoints = int.Parse(text3);
							}
							string mBossPrefix = "";
							if (!this.GetAttribute(bxmlelement, "boss", ref mBossPrefix))
							{
								goto Block_58;
							}
							string mDifficulty = "";
							string mCupName = "";
							this.GetAttribute(bxmlelement, "rating", ref mDifficulty);
							this.GetAttribute(bxmlelement, "cup", ref mCupName);
							if (this.mZones[num6 - 1] == null)
							{
								this.mZones[num6 - 1] = new ZoneInfo();
							}
							this.mZones[num6 - 1].mBossPrefix = mBossPrefix;
							this.mZones[num6 - 1].mStartLevel = mStartLevel;
							this.mZones[num6 - 1].mNum = num6;
							this.mZones[num6 - 1].mChallengePoints = mChallengePoints;
							this.mZones[num6 - 1].mChallengeAcePoints = mChallengeAcePoints;
							this.mZones[num6 - 1].mDifficulty = mDifficulty;
							this.mZones[num6 - 1].mCupName = mCupName;
							string text4 = "";
							for (int j = 1; j <= 10; j++)
							{
								if (this.GetAttribute(bxmlelement, "BossTaunt" + j, ref text4))
								{
									this.mZones[num6 - 1].mBossTaunts[j - 1] = text4;
								}
								else
								{
									this.mZones[num6 - 1].mBossTaunts[j - 1] = "";
								}
							}
							string mFruitId = "";
							if (!this.GetAttribute(bxmlelement, "fruit", ref mFruitId))
							{
								goto Block_62;
							}
							this.mZones[num6 - 1].mFruitId = mFruitId;
						}
						else if (!flag && bxmlelement.mValue == "Defaults")
						{
							if (!this.DoParseDefaults(bxmlelement))
							{
								return false;
							}
						}
						else if (flag && bxmlelement.mValue == "Tunnel")
						{
							if (!this.DoParseTunnel(bxmlelement, this.mLevels.back<Level>()))
							{
								return false;
							}
						}
						else if (flag && bxmlelement.mValue == "Gun")
						{
							if (!this.DoParseGun(bxmlelement, this.mLevels.back<Level>()))
							{
								return false;
							}
						}
						else if (flag && bxmlelement.mValue == "PowerupRegion")
						{
							if (!this.DoParsePowerupRegion(bxmlelement, this.mLevels.back<Level>()))
							{
								return false;
							}
						}
						else if (flag && bxmlelement.mValue == "Torch")
						{
							if (!this.DoParseTorch(bxmlelement, this.mLevels.back<Level>()))
							{
								return false;
							}
						}
						else if (flag && bxmlelement.mValue == "Wall")
						{
							if (!this.DoParseWall(bxmlelement, this.mLevels.back<Level>()))
							{
								return false;
							}
						}
						else if (flag && bxmlelement.mValue == "MovingWall")
						{
							if (!this.DoParseMovingWall(bxmlelement, this.mLevels.back<Level>()))
							{
								return false;
							}
						}
						else
						{
							if (!flag || !(bxmlelement.mValue == "Boss"))
							{
								goto IL_F37;
							}
							if (!this.DoParseBoss(bxmlelement, this.mLevels.back<Level>()))
							{
								return false;
							}
						}
					}
					else if (bxmlelement.mType == 2)
					{
						if (flag && (bxmlelement.mValue == "Level" || bxmlelement.mValue == "Challenge"))
						{
							Level level3 = this.mLevels.back<Level>();
							flag = false;
							if (GameApp.gApp != null)
							{
								if (level3.mImagePath == "")
								{
									level3.mImagePath = string.Concat(new string[]
									{
										GameApp.gApp.GetResImagesDir(),
										"levels/",
										level3.mId,
										"/",
										level3.mId
									});
								}
								else if (!level3.mImagePath.StartsWith("images/levels"))
								{
									level3.mImagePath = string.Concat(new string[]
									{
										GameApp.gApp.GetResImagesDir(),
										"levels/",
										level3.mId,
										"/",
										level3.mImagePath
									});
								}
								for (int k = 0; k < 5; k++)
								{
									if (level3.mFrogImages[k].mFilename != "")
									{
										level3.mFrogImages[k].mFilename = string.Concat(new string[]
										{
											GameApp.gApp.GetResImagesDir(),
											"levels/",
											level3.mId,
											"/",
											level3.mFrogImages[k].mFilename
										});
									}
								}
							}
							if (num3 != -1)
							{
								if (num3 < 0 || num3 >= level3.mNumCurves)
								{
									goto IL_110A;
								}
								for (int m = 0; m < level3.mNumCurves; m++)
								{
									if (m != num3)
									{
										level3.mCurveMgr[m].CopyCurveDataFrom(level3.mCurveMgr[num3]);
									}
								}
								num3 = -1;
							}
							if (!this.mHasFailed)
							{
								level3.SetupHiddenHoles();
							}
							bool flag3 = false;
							for (int n = 0; n < level3.mEffectNames.size<string>(); n++)
							{
								if (SexyFramework.Common.StrEquals(level3.mEffectNames[n], "Lavashader", true))
								{
									flag3 = true;
									break;
								}
							}
							if (!flag3)
							{
								level3.mEffectNames.Add("LavaShader");
								level3.mEffectParams.Add(new EffectParams("fullscene", "true", level3.mEffectNames.size<string>() - 1));
								level3.mEffectParams.Add(new EffectParams("distamt", "0.001", level3.mEffectNames.size<string>() - 1));
								level3.mEffectParams.Add(new EffectParams("scale", "0.5", level3.mEffectNames.size<string>() - 1));
								level3.mEffectParams.Add(new EffectParams("scroll", "0.05", level3.mEffectNames.size<string>() - 1));
								level3.mEffectParams.Add(new EffectParams("mumu", "true", level3.mEffectNames.size<string>() - 1));
							}
						}
					}
					else if (bxmlelement.mType == 3)
					{
						goto Block_101;
					}
				}
				return false;
				Block_47:
				return this.Fail("<Tip> section must contain /text/ parameter only");
				Block_50:
				return this.Fail("<ScoreTip> section must contain /text/ parameter only");
				Block_54:
				return this.Fail("<Zone> section must contain /num/ parameter");
				Block_55:
				return this.Fail("<Zone> section must contain /start/ parameter");
				Block_58:
				return this.Fail("<Zone> section must contain /boss/ parameter");
				Block_62:
				return this.Fail("<Zone> section must contain /fruit/ parameter");
				IL_F37:
				this.Fail("Invalid Section '" + bxmlelement.mValue + "'");
				goto IL_12B7;
				IL_110A:
				return this.Fail("Invalid number set for parameter \"curvedata\":" + num3);
				Block_101:
				this.Fail("Element Not Expected '" + bxmlelement.mValue + "'");
			}
			IL_12B7:
			this.mXMLParser = null;
			return !this.mHasFailed;
		}

		protected bool DoParseDDS(BXMLElement elem)
		{
			string str = "";
			if (!this.GetAttribute(elem, "tier", ref str))
			{
				return this.Fail("Expected /tier/ tag in <DDS> section");
			}
			int num = this.StrToInt(str);
			if (!this.GetAttribute(elem, this._S("powerpct"), ref str))
			{
				return this.Fail("Expected /powerpct/ tag in <DDS> section");
			}
			float num2 = this.StrToFloat(str);
			if (!this.GetAttribute(elem, this._S("slowadd"), ref str))
			{
				return this.Fail("Expected /slowadd/ tag in <DDS> section");
			}
			int num3 = this.StrToInt(str);
			if (num != this.mNumDDSTiers + 1)
			{
				return this.Fail("You must add DDS tiers in order, starting with tier 1");
			}
			float num4 = 100f;
			if (this.GetAttribute(elem, this._S("speedpct"), ref str))
			{
				num4 = this.StrToFloat(str);
			}
			float num5 = 0f;
			if (this.GetAttribute(elem, this._S("zumapct"), ref str))
			{
				num5 = this.StrToFloat(str) / 100f;
			}
			this.mNumDDSTiers = num;
			this.mDDSPowerupPctInc.Add(num2 / 100f);
			this.mDDSSlowAdd.Add(num3);
			this.mDDSSpeedPct.Add(num4 / 100f);
			this.mDDSZumaPointDecPct.Add(num5);
			return true;
		}

		protected bool DoParseGauntletMode(BXMLElement elem)
		{
			string str = "";
			if (!this.GetAttribute(elem, this._S("NumCurves"), ref str))
			{
				return this.Fail("Unable to find \"NumCurves\" in \"Gauntlet\" tag");
			}
			int num_curves = this.StrToInt(str);
			int mHurryDist = 25;
			float mHurryMaxSpeed = 0f;
			if (this.GetAttribute(elem, this._S("HurryDist"), ref str))
			{
				mHurryDist = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("HurrySpeed"), ref str))
			{
				mHurryMaxSpeed = this.StrToFloat(str);
			}
			while (this.mXMLParser.NextElement(ref elem))
			{
				if (elem.mType == 1)
				{
					if (this.StrEquals(elem.mValue, this._S("Difficulty")))
					{
						Gauntlet_Vals gauntlet_Vals = new Gauntlet_Vals();
						if (this.GetAttribute(elem, this._S("time"), ref str))
						{
							gauntlet_Vals.mDifficultyLevel = this.StrToInt(str);
							gauntlet_Vals.mTimeBaseDifficulty = true;
						}
						else
						{
							if (!this.GetAttribute(elem, this._S("points"), ref str))
							{
								return this.Fail("Unable to find \"time\" or \"points\" tag in \"Difficulty\" section for gauntlet mode.");
							}
							gauntlet_Vals.mDifficultyLevel = this.StrToInt(str);
							gauntlet_Vals.mTimeBaseDifficulty = false;
						}
						this.GET_GAUNTLET_ELEM(elem, this._S("speed"), ref str);
						gauntlet_Vals.mSpeed = this.StrToFloat(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("startdist"), ref str);
						gauntlet_Vals.mStartDistance = this.StrToInt(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("zumascore"), ref str);
						gauntlet_Vals.mZumaScore = this.StrToInt(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("ballrepeat"), ref str);
						gauntlet_Vals.mBallRepeat = this.StrToInt(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("powerup"), ref str);
						gauntlet_Vals.mPowerupChance = this.StrToInt(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("colors"), ref str);
						gauntlet_Vals.mNumColors = this.StrToInt(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("rollback"), ref str);
						gauntlet_Vals.mRollbackPct = this.StrToInt(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("dangerratio"), ref str);
						gauntlet_Vals.mSlowFactor = this.StrToFloat(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("maxclumps"), ref str);
						gauntlet_Vals.mMaxClumpSize = this.StrToInt(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("maxsingles"), ref str);
						gauntlet_Vals.mMaxSingle = this.StrToInt(str);
						this.GET_GAUNTLET_ELEM(elem, this._S("rollbackduration"), ref str);
						gauntlet_Vals.mRollbackTime = this.StrToInt(str);
						gauntlet_Vals.mHurryDist = mHurryDist;
						gauntlet_Vals.mHurryMaxSpeed = mHurryMaxSpeed;
						GameApp.gDDS.AddGauntletVals(gauntlet_Vals, num_curves);
					}
				}
				else if (elem.mType == 2 && this.StrEquals(elem.mValue, this._S("Gauntlet")))
				{
					break;
				}
			}
			return true;
		}

		protected bool DoParseBossDDS(BXMLElement elem, string boss_name, Level l)
		{
			string param_name = "";
			if (!this.GetAttribute(elem, this._S("value"), ref param_name))
			{
				return this.Fail("Expected /value/ tag in <DDS> section");
			}
			string str = "";
			if (!this.GetAttribute(elem, this._S("min"), ref str))
			{
				return this.Fail("Expected /min/ tag in <DDS> section");
			}
			float min = this.StrToFloat(str);
			if (!this.GetAttribute(elem, this._S("max"), ref str))
			{
				return this.Fail("Expected /max/ tag in <DDS> section");
			}
			float max = this.StrToFloat(str);
			float range_min = -1f;
			float range_max = -1f;
			if (this.GetAttribute(elem, this._S("ddsmin"), ref str))
			{
				range_min = this.StrToFloat(str) / 100f;
			}
			if (this.GetAttribute(elem, this._S("ddsmax"), ref str))
			{
				range_max = this.StrToFloat(str) / 100f;
			}
			GameApp.gDDS.AddBossParam(this.ToString(boss_name), param_name, min, max, range_min, range_max);
			return true;
		}

		protected bool DoParseTunnel(BXMLElement elem, Level l)
		{
			string str = "";
			string mImageName = "";
			string text = "";
			int mX = 0;
			int mY = 0;
			if (!this.GetAttribute(elem, this._S("pri"), ref str))
			{
				return this.Fail("Unable to find \"pri\" (priority) parameter in \"Tunnel\" tag");
			}
			int num = this.sexyatoi(str);
			bool mAboveShadows = false;
			if (!l.mBGFromPSD)
			{
				if (!this.GetAttribute(elem, this._S("image"), ref str))
				{
					return this.Fail("Unable to find \"image\" parameter in \"Tunnel\" tag");
				}
				mImageName = this.ToString(str);
				if (!this.GetAttribute(elem, this._S("x"), ref str))
				{
					return this.Fail("Unable to find \"x\" parameter in \"Tunnel\" tag");
				}
				mX = this.sexyatoi(str);
				if (!this.GetAttribute(elem, this._S("y"), ref str))
				{
					return this.Fail("Unable to find \"y\" parameter in \"Tunnel\" tag");
				}
				mY = this.sexyatoi(str);
			}
			else
			{
				if (!this.GetAttribute(elem, this._S("layer"), ref str))
				{
					return this.Fail("Unable to find \"layer\" parameter in \"Tunnel\" tag");
				}
				text = this.StringToUpper(str);
				if (this.GetAttribute(elem, this._S("aboveshadows"), ref str))
				{
					mAboveShadows = this.StrToBool(str);
				}
			}
			if (num < 0 || num >= 5)
			{
				return this.Fail("Priority must be in the range of 0 to " + 4);
			}
			l.mTunnelData.Add(new TunnelData());
			TunnelData tunnelData = l.mTunnelData.back<TunnelData>();
			tunnelData.mImageName = mImageName;
			if (text.Length > 0)
			{
				tunnelData.mLayerId = "IMAGE_LEVELS_" + l.mId.ToUpper() + "_CHUTE" + this.ToString(text);
			}
			tunnelData.mAboveShadows = mAboveShadows;
			tunnelData.mX = mX;
			tunnelData.mY = mY;
			tunnelData.mPriority = num;
			if (this.GetAttribute(elem, this._S("nothumb"), ref str))
			{
				tunnelData.mNoThumb = this.StrToBool(str);
			}
			return true;
		}

		protected bool DoParseTorch(BXMLElement elem, Level l)
		{
			string str = "";
			if (!this.GetAttribute(elem, this._S("x"), ref str))
			{
				return false;
			}
			int x = this.StrToInt(str);
			if (!this.GetAttribute(elem, this._S("y"), ref str))
			{
				return false;
			}
			int y = this.StrToInt(str);
			if (!this.GetAttribute(elem, this._S("w"), ref str))
			{
				return false;
			}
			int w = this.StrToInt(str);
			if (!this.GetAttribute(elem, this._S("h"), ref str))
			{
				return false;
			}
			int h = this.StrToInt(str);
			l.AddTorch(x, y, w, h);
			return true;
		}

		protected bool DoParseDefaults(BXMLElement elem)
		{
			string str = "";
			if (!this.GetAttribute(elem, this._S("cannon"), ref str))
			{
				this.mCannonShots = 3;
			}
			else
			{
				this.mCannonShots = this.sexyatoi(str);
			}
			if (!this.GetAttribute(elem, this._S("cannonangle"), ref str))
			{
				this.mCannonAngle = 30f;
			}
			else
			{
				this.mCannonAngle = this.sexyatof(str) * 3.14159f / 180f;
			}
			if (this.GetAttribute(elem, this._S("MaxZumaPctForColorNuke"), ref str))
			{
				this.mMaxZumaPctForColorNuke = this.StrToFloat(str) / 100f;
			}
			if (this.GetAttribute(elem, this._S("BeatGamePointsForLife"), ref str))
			{
				this.mBeatGamePointsForLife = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("PointsForExtraLife"), ref str))
			{
				this.mPointsForLife = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("BossesCanAttackAffectedFrog"), ref str))
			{
				this.mBossesCanAttackFuckedFrog = this.StrToBool(str);
			}
			if (this.GetAttribute(elem, this._S("BossAttackDelayAfterHitFrog"), ref str))
			{
				this.mAttackDelayAfterHittingFrog = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("PointsForBronze"), ref str))
			{
				this.mPointsForBronze = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("PointsForSilver"), ref str))
			{
				this.mPointsForSilver = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("PointsForGold"), ref str))
			{
				this.mPointsForGold = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("PowerIncAtZumaPct"), ref str))
			{
				this.mPowerupIncAtZumaPct = this.StrToFloat(str) / 100f;
			}
			if (this.GetAttribute(elem, this._S("PowerInc"), ref str))
			{
				this.mPowerIncPct = this.StrToFloat(str) / 100f;
			}
			if (this.GetAttribute(elem, this._S("ClearCurvePoints"), ref str))
			{
				this.mClearCurvePoints = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("ClearCurveRolloutPct"), ref str))
			{
				this.mClearCurveRolloutPct = this.StrToFloat(str) / 100f;
			}
			if (this.GetAttribute(elem, this._S("ClearCurveSpeedMult"), ref str))
			{
				this.mClearCurveSpeedMult = this.StrToFloat(str);
			}
			if (this.GetAttribute(elem, this._S("gauntletsessionlength"), ref str))
			{
				this.mGauntletSessionLength = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("NumForMultBase"), ref str))
			{
				this.mGauntletNumForMultBase = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("NumForMultInc"), ref str))
			{
				this.mGauntletNumForMultInc = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("MultiplierDuration"), ref str))
			{
				this.mMultiplierDuration = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("MultTimeAdd"), ref str))
			{
				this.mMultiplierTimeAdd = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("MaxNumForMult"), ref str))
			{
				this.mMaxGauntletNumForMult = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("PointTimeAdd"), ref str))
			{
				this.mPointTimeAdd = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("NumPointsForTimeAdd"), ref str))
			{
				this.mNumPointsForTimeAdd = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("ChallengeTFreq"), ref str))
			{
				this.mGauntletTFreq = this.StrToInt(str);
			}
			if (!this.GetAttribute(elem, this._S("cannonstack"), ref str))
			{
				this.mCannonStacks = true;
			}
			else
			{
				this.mCannonStacks = this.StrToBool(str);
			}
			if (this.GetAttribute(elem, this._S("MinMultSpawnDist"), ref str))
			{
				this.mMinMultBallDistance = this.StrToFloat(str) / 100f;
			}
			if (this.GetAttribute(elem, this._S("MultBallPoints"), ref str))
			{
				this.mMultBallPoints = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("MultBallLife"), ref str))
			{
				this.mMultBallLife = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("BossTauntChance"), ref str))
			{
				this.mBossTauntChance = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("powerspawndelay"), ref str))
			{
				this.mPowerupSpawnDelay = this.StrToInt(str);
			}
			else
			{
				this.mPowerupSpawnDelay = 0;
			}
			if (!this.GetAttribute(elem, this._S("lazer"), ref str))
			{
				this.mLazerShots = 3;
			}
			else
			{
				this.mLazerShots = this.sexyatoi(str);
			}
			if (!this.GetAttribute(elem, this._S("lazerstack"), ref str))
			{
				this.mLazerStacks = false;
			}
			else
			{
				this.mLazerStacks = this.StrToBool(str);
			}
			if (this.GetAttribute(elem, this._S("powerdelay"), ref str))
			{
				this.mPowerDelay = this.sexyatoi(str);
			}
			if (this.GetAttribute(elem, this._S("powercooldown"), ref str))
			{
				this.mPowerCooldown = this.sexyatoi(str);
			}
			if (this.GetAttribute(elem, this._S("colornukeafterzuma"), ref str))
			{
				this.mAllowColorNukeAfterZuma = this.StrToBool(str);
			}
			if (this.GetAttribute(elem, this._S("colornuketimeafterzuma"), ref str))
			{
				this.mColorNukeTimeAfterZuma = this.sexyatoi(str);
			}
			if (this.GetAttribute(elem, this._S("UniquePowerupColor"), ref str))
			{
				this.mUniquePowerupColor = this.StrToBool(str);
			}
			if (this.GetAttribute(elem, this._S("PowerupCapAffectsTriggered"), ref str))
			{
				this.mCapAffectsPowerupsSpawned = this.StrToBool(str);
			}
			if (this.GetAttribute(elem, this._S("PZT"), ref str))
			{
				this.mPostZumaTime = this.StrToInt(str);
			}
			if (this.GetAttribute(elem, this._S("PZTSpeedInc"), ref str))
			{
				this.mPostZumaTimeSpeedInc = this.StrToFloat(str) / 100f;
			}
			if (this.GetAttribute(elem, this._S("PZTSlowInc"), ref str))
			{
				this.mPostZumaTimeSlowInc = this.StrToFloat(str) / 100f;
			}
			return true;
		}

		protected bool ParseCommonWallShit(BXMLElement elem, Wall w)
		{
			string text = "";
			if (!this.GetAttribute(elem, this._S("x"), ref text))
			{
				this.COMMON_WALL_FAIL("x");
			}
			int num = this.sexyatoi(text);
			if (!this.GetAttribute(elem, this._S("y"), ref text))
			{
				this.COMMON_WALL_FAIL("y");
			}
			int num2 = this.sexyatoi(text);
			if (!this.GetAttribute(elem, this._S("width"), ref text) && !this.GetAttribute(elem, this._S("w"), ref text))
			{
				this.COMMON_WALL_FAIL("width");
			}
			int num3 = this.sexyatoi(text);
			if (!this.GetAttribute(elem, this._S("height"), ref text) && !this.GetAttribute(elem, this._S("h"), ref text))
			{
				this.COMMON_WALL_FAIL("height");
			}
			int num4 = this.sexyatoi(text);
			w.mX = (float)num;
			w.mY = (float)num2;
			w.mWidth = (float)num3;
			w.mHeight = (float)num4;
			if (this.GetAttribute(elem, this._S("color"), ref text))
			{
				w.mColor = new Color((int)JeffLib.Common.StrToHex(this.ToString(text)));
				if (text.get_Chars(0) == '0' && text.get_Chars(1) == '0')
				{
					w.mColor.mAlpha = 0;
				}
			}
			else
			{
				w.mColor = new Color(255, 0, 0);
			}
			w.mCurRespawnTimer = (w.mCurLifeTimer = 0);
			w.mMinRespawnTimer = (w.mMaxRespawnTimer = (w.mMinLifeTimer = (w.mMaxLifeTimer = 0)));
			if (this.GetAttribute(elem, this._S("minrespawntime"), ref text))
			{
				w.mMinRespawnTimer = this.sexyatoi(text);
			}
			if (this.GetAttribute(elem, this._S("maxrespawntime"), ref text))
			{
				w.mMaxRespawnTimer = this.sexyatoi(text);
			}
			if (this.GetAttribute(elem, this._S("minlifetime"), ref text))
			{
				w.mMinLifeTimer = this.sexyatoi(text);
			}
			if (this.GetAttribute(elem, this._S("maxlifetime"), ref text))
			{
				w.mMaxLifeTimer = this.sexyatoi(text);
			}
			w.mCurLifeTimer = MathUtils.IntRange(w.mMinLifeTimer, w.mMaxLifeTimer);
			return true;
		}

		protected bool DoParseWall(BXMLElement elem, Level l)
		{
			string str = "";
			int mOrgStrength = (this.GetAttribute(elem, this._S("strength"), ref str) ? this.sexyatoi(str) : (-1));
			l.mWalls.Add(new Wall());
			this.ParseCommonWallShit(elem, l.mWalls[l.mWalls.size<Wall>() - 1]);
			Wall wall = l.mWalls.back<Wall>();
			wall.mStrength = (wall.mOrgStrength = mOrgStrength);
			if (this.GetAttribute(elem, this._S("id"), ref str))
			{
				wall.mId = this.sexyatoi(str);
			}
			else
			{
				wall.mId = -1;
			}
			wall.mVX = (wall.mVY = 0f);
			return true;
		}

		protected bool DoParseMovingWall(BXMLElement elem, Level l)
		{
			string str = "";
			float mVX = 0f;
			float mVY = 0f;
			if (this.GetAttribute(elem, this._S("vx"), ref str))
			{
				mVX = this.sexyatof(str);
			}
			if (this.GetAttribute(elem, this._S("vy"), ref str))
			{
				mVY = this.sexyatof(str);
			}
			int num = 0;
			if (this.GetAttribute(elem, this._S("spacing"), ref str))
			{
				num = this.sexyatoi(str);
			}
			l.mMovingWallDefaults.Add(new Wall());
			this.ParseCommonWallShit(elem, l.mMovingWallDefaults[l.mMovingWallDefaults.size<Wall>() - 1]);
			Wall wall = l.mMovingWallDefaults.back<Wall>();
			wall.mVX = mVX;
			wall.mVY = mVY;
			wall.mStrength = -1;
			wall.mSpacing = num * num;
			wall.mId = l.mMovingWallDefaults.size<Wall>();
			return true;
		}

		protected bool DoParseTreasure(BXMLElement theElem, TreasurePoint thePoint)
		{
			string str = "";
			if (this.GetAttribute(theElem, this._S("x"), ref str))
			{
				thePoint.x = this.StrToInt(str);
			}
			if (this.GetAttribute(theElem, this._S("y"), ref str))
			{
				thePoint.y = this.StrToInt(str);
			}
			for (int i = 0; i < 4; i++)
			{
				if (this.GetAttribute(theElem, "dist" + (i + 1), ref str))
				{
					thePoint.mCurveDist[i] = this.StrToInt(str);
				}
				else
				{
					thePoint.mCurveDist[i] = 0;
				}
			}
			return true;
		}

		protected bool DoParseSetEffectParams(BXMLElement elem, Level l)
		{
			string str = "";
			if (!this.GetAttribute(elem, this._S("num"), ref str))
			{
				return false;
			}
			int num = this.StrToInt(str);
			foreach (KeyValuePair<string, string> keyValuePair in elem.mAttributes)
			{
				l.mEffectParams.Add(new EffectParams());
				EffectParams effectParams = l.mEffectParams.back<EffectParams>();
				effectParams.mKey = this.ToString(keyValuePair.Key);
				effectParams.mValue = this.ToString(keyValuePair.Value);
				effectParams.mEffectIndex = num - 1;
			}
			return true;
		}

		protected bool DoParseGun(BXMLElement elem, Level l)
		{
			string str = "";
			if (this.GetAttribute(elem, this._S("type"), ref str))
			{
				if (this.StrEquals(str, this._S("normal")))
				{
					l.mMoveType = 0;
				}
				else if (this.StrEquals(str, this._S("horiz")) || this.StrEquals(str, this._S("horizontal")))
				{
					l.mMoveType = 1;
				}
				else
				{
					if (!this.StrEquals(str, this._S("vert")) && !this.StrEquals(str, this._S("vertical")))
					{
						return this.Fail("Invalid gun type");
					}
					l.mMoveType = 2;
				}
			}
			if (l.mMoveType == 0)
			{
				int i = 0;
				while (i < 5)
				{
					string str2 = "";
					bool flag = false;
					bool flag2 = false;
					if (this.GetAttribute(elem, "gx" + (i + 1), ref str2))
					{
						flag = true;
						l.mFrogX[i] = this.sexyatoi(str2);
					}
					if (this.GetAttribute(elem, "gy" + (i + 1), ref str2))
					{
						flag2 = true;
						l.mFrogY[i] = this.sexyatoi(str2);
					}
					if (this.GetAttribute(elem, "image" + (i + 1), ref str2))
					{
						l.mFrogImages[i].mFilename = this.ToString(str2);
					}
					if (this.GetAttribute(elem, "resid" + (i + 1), ref str2))
					{
						l.mFrogImages[i].mResId = "IMAGE_LEVELS_" + this.StringToUpper(l.mId) + "_" + this.ToString(str2);
					}
					if (flag && flag2)
					{
						l.mNumFrogPoints = i + 1;
						i++;
					}
					else
					{
						if (flag || flag2)
						{
							return this.Fail("For every gx, there must also be a gy: A mistmatch was detected for gun point " + (i + 1));
						}
						break;
					}
				}
				string str3 = "";
				if (this.GetAttribute(elem, this._S("jumpspeed"), ref str3))
				{
					l.mMoveSpeed = this.sexyatoi(str3);
				}
				if (this.GetAttribute(elem, this._S("image"), ref str3))
				{
					for (int j = 0; j < 5; j++)
					{
						l.mFrogImages[j].mFilename = this.ToString(str3);
					}
				}
				else if (this.GetAttribute(elem, this._S("resid"), ref str3))
				{
					for (int k = 0; k < 5; k++)
					{
						l.mFrogImages[k].mResId = "IMAGE_LEVELS_" + this.StringToUpper(l.mId) + "_" + this.ToString(this.StringToUpper(str3));
					}
				}
			}
			else
			{
				int num = -1;
				int num2 = -1;
				string str4 = "";
				if (this.GetAttribute(elem, this._S("startx"), ref str4))
				{
					num = this.sexyatoi(str4);
				}
				if (this.GetAttribute(elem, this._S("starty"), ref str4))
				{
					num2 = this.sexyatoi(str4);
				}
				if (num == -1 || num2 == -1)
				{
					return this.Fail("You must specify a startx and starty for this gun type");
				}
				l.mNumFrogPoints = 1;
				l.mFrogX[0] = num;
				l.mFrogY[0] = num2;
				if (l.mMoveType == 1)
				{
					if (!this.GetAttribute(elem, this._S("width"), ref str4))
					{
						return this.Fail("\"width\" expected for horizontal gun type");
					}
					l.mBarWidth = this.sexyatoi(str4);
				}
				else if (l.mMoveType == 2)
				{
					if (!this.GetAttribute(elem, this._S("height"), ref str4))
					{
						return this.Fail("\"height\" expected for vertical gun type");
					}
					l.mBarHeight = this.sexyatoi(str4);
				}
			}
			return true;
		}

		protected bool DoParsePowerupRegion(BXMLElement elem, Level l)
		{
			string str = "";
			if (!this.GetAttribute(elem, this._S("start"), ref str))
			{
				return this.Fail("Need /start/ tag in PowerupRegion");
			}
			float num = this.StrToFloat(str);
			if (!this.GetAttribute(elem, this._S("end"), ref str))
			{
				return this.Fail("Need /end/ tag in PowerupRegion");
			}
			float num2 = this.StrToFloat(str);
			bool mDebugDraw = false;
			if (this.GetAttribute(elem, this._S("debugdraw"), ref str))
			{
				mDebugDraw = this.StrToBool(str);
			}
			if (!this.GetAttribute(elem, this._S("chance"), ref str))
			{
				return this.Fail("need /chance/ tag in PowerupRegion");
			}
			int mChance = this.StrToInt(str);
			int mCurveNum = 0;
			if (!this.GetAttribute(elem, this._S("curve"), ref str))
			{
				mCurveNum = this.StrToInt(str) - 1;
			}
			l.mPowerupRegions.Add(new PowerupRegion());
			PowerupRegion powerupRegion = l.mPowerupRegions.back<PowerupRegion>();
			powerupRegion.mCurvePctStart = num / 100f;
			powerupRegion.mCurvePctEnd = num2 / 100f;
			powerupRegion.mChance = mChance;
			powerupRegion.mCurveNum = mCurveNum;
			powerupRegion.mDebugDraw = mDebugDraw;
			return true;
		}

		protected bool DoParseBoss(BXMLElement elem, Level l)
		{
			string text = "";
			this.GET_BOSS_ELEM(elem, this._S("type"), ref text);
			Boss boss = null;
			int num = this.sexyatoi(text);
			bool flag = false;
			if (this.GetAttribute(elem, this._S("art"), ref text))
			{
				if (this.StrEquals(text, this._S("tiger")))
				{
					boss = new BossTiger();
				}
				else if (this.StrEquals(text, this._S("skeleton")))
				{
					flag = true;
					boss = new BossSkeleton();
				}
				else if (this.StrEquals(text, this._S("doctor")))
				{
					boss = new BossDoctor();
				}
				else if (this.StrEquals(text, this._S("squid")))
				{
					boss = new BossSquid();
				}
				else if (this.StrEquals(text, this._S("mosquito")))
				{
					boss = new BossMosquito();
				}
				else if (this.StrEquals(text, this._S("stonehead")))
				{
					boss = new BossStoneHead();
				}
				else if (this.StrEquals(text, this._S("lame")))
				{
					boss = new BossLame();
				}
				else if (this.StrEquals(text, this._S("volcano")))
				{
					boss = new BossVolcano();
				}
				else if (this.StrEquals(text, this._S("darkfrog")))
				{
					boss = new BossDarkFrog();
				}
			}
			if (this.GetAttribute(elem, this._S("sepia"), ref text))
			{
				boss.mSepiaImagePath = this.ToString(text);
			}
			if (boss == null)
			{
				GameApp.gApp.Popup("Invalid boss type: " + num + ". Only type 3 is valid");
				return false;
			}
			if (l.mBoss == null)
			{
				l.mBoss = (l.mOrgBoss = boss);
			}
			else
			{
				l.mSecondaryBoss = boss;
			}
			if (JeffLib.Common.StrFindNoCase(l.mId, "debug") == -1)
			{
				boss.mNum = ++LevelMgr.gBossNum;
			}
			else
			{
				boss.mNum = LevelMgr.gBossNum;
			}
			if (this.GetAttribute(elem, this._S("wordbubble"), ref text))
			{
				boss.mWordBubbleText = this.ToString(text);
			}
			this.GET_BOSS_ELEM(elem, this._S("hpdec"), ref text);
			float num2 = this.StrToFloat(text);
			boss.SetHPDecPerHit(num2);
			if (this.GetAttribute(elem, this._S("HPDecProxBomb"), ref text))
			{
				boss.SetHPDecPerHitProxBomb(this.StrToFloat(text));
			}
			else
			{
				boss.SetHPDecPerHitProxBomb(num2);
			}
			if (this.GetAttribute(elem, this._S("cancompact"), ref text))
			{
				boss.mAllowCompacting = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("WpnHPDec"), ref text) && flag)
			{
				(boss as BossSkeleton).mSpecialWpnHPDec = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("SpawnPowerupWhilePoweredUp"), ref text) && flag)
			{
				(boss as BossSkeleton).mSpawnPowerupWhilePoweredUp = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("ChanceToSpawnPowerup"), ref text) && flag)
			{
				(boss as BossSkeleton).mChanceToSpawnPowerup = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("drawradius"), ref text))
			{
				boss.mDrawRadius = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("RadiusColorMode"), ref text))
			{
				boss.mRadiusColorChangeMode = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("AllowLevelDDS"), ref text))
			{
				boss.mAllowLevelDDS = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("xshake"), ref text))
			{
				boss.mShakeXAmt = this.sexyatoi(text);
			}
			if (this.GetAttribute(elem, this._S("yshake"), ref text))
			{
				boss.mShakeYAmt = this.sexyatoi(text);
			}
			if (this.GetAttribute(elem, this._S("heartxoff"), ref text))
			{
				boss.mHeartXOff = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("heartyoff"), ref text))
			{
				boss.mHeartYOff = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("ImpatientTimer"), ref text))
			{
				boss.mImpatientTimer = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("ResetWallsOnHit"), ref text))
			{
				boss.mResetWallsOnBossHit = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("ResetTimerOnTikiHit"), ref text))
			{
				boss.mResetWallTimerOnTikiHit = this.StrToBool(text);
			}
			bool flag2 = false;
			if (this.GetAttribute(elem, this._S("WallDownTime"), ref text))
			{
				flag2 = true;
				boss.mWallDownTime = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("TikiHealthRespawn"), ref text))
			{
				if (flag2)
				{
					return this.Fail("You can't use both /WallDownTime/ and /TikiHealthRespawn/");
				}
				boss.mResetWallsOnBossHit = false;
				boss.mTikiHealthRespawnAmt = this.StrToInt(text);
			}
			int num3 = 1;
			while (this.GetAttribute(elem, this._S("wall" + num3 + "x"), ref text))
			{
				int x = this.StrToInt(text);
				if (!this.GetAttribute(elem, this._S("wall" + num3 + "y"), ref text))
				{
					return false;
				}
				int y = this.StrToInt(text);
				if (!this.GetAttribute(elem, this._S("wall" + num3 + "w"), ref text))
				{
					return false;
				}
				int w = this.StrToInt(text);
				if (!this.GetAttribute(elem, this._S("wall" + num3 + "h"), ref text))
				{
					return false;
				}
				int h = this.StrToInt(text);
				boss.AddWall(x, y, w, h, num3);
				num3++;
			}
			num3 = 1;
			while (this.GetAttribute(elem, this._S("tiki" + num3 + "x"), ref text))
			{
				int x2 = this.StrToInt(text);
				if (!this.GetAttribute(elem, this._S("tiki" + num3 + "y"), ref text))
				{
					return false;
				}
				int y2 = this.StrToInt(text);
				int rail_w = 0;
				int rail_h = 0;
				int travel_time = 0;
				if (this.GetAttribute(elem, this._S("tiki" + num3 + "w"), ref text))
				{
					rail_w = this.StrToInt(text);
				}
				if (this.GetAttribute(elem, this._S("tiki" + num3 + "h"), ref text))
				{
					rail_h = this.StrToInt(text);
				}
				if (this.GetAttribute(elem, this._S("tiki" + num3 + "time"), ref text))
				{
					travel_time = this.StrToInt(text);
				}
				boss.AddTiki(x2, y2, num3, rail_w, rail_h, travel_time);
				num3++;
			}
			BossShoot bossShoot = boss as BossShoot;
			if (this.GetAttribute(elem, this._S("bossradius"), ref text))
			{
				bossShoot.mBossRadius = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("maxbounces"), ref text))
			{
				bossShoot.mMaxShotBounces = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("BallShieldDamage"), ref text))
			{
				bossShoot.mBallShieldDamage = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("ShieldHP"), ref text))
			{
				bossShoot.mShieldHP = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("TeleportMinTime"), ref text))
			{
				bossShoot.mTeleportMinTime = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("TeleportMaxTime"), ref text))
			{
				bossShoot.mTeleportMaxTime = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("BombFreqMin"), ref text))
			{
				bossShoot.mBombFreqMin = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("BombFreqMax"), ref text))
			{
				bossShoot.mBombFreqMax = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("BombDuration"), ref text))
			{
				bossShoot.mBombDuration = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("MinInkNum"), ref text))
			{
				bossShoot.mMinSpots = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("MaxInkNum"), ref text))
			{
				bossShoot.mMaxSpots = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("MinInkRad"), ref text))
			{
				bossShoot.mMinSpotRad = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("MaxInkRad"), ref text))
			{
				bossShoot.mMaxSpotRad = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("MinInkFade"), ref text))
			{
				bossShoot.mMinSpotFade = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("MaxInkFade"), ref text))
			{
				bossShoot.mMaxSpotFade = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("InkFadeDelay"), ref text))
			{
				bossShoot.mSpotFadeDelay = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("InkTargetsBalls"), ref text))
			{
				bossShoot.mInkTargetMode = (this.StrToBool(text) ? 1 : 0);
			}
			if (this.GetAttribute(elem, this._S("InkTargetsScreen"), ref text) && this.StrToBool(text))
			{
				bossShoot.mInkTargetMode = 2;
			}
			if (this.GetAttribute(elem, this._S("EnrageDelay"), ref text))
			{
				bossShoot.mEnrageDelay = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("BombDelay"), ref text))
			{
				bossShoot.mBombAppearDelay = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("startx"), ref text))
			{
				bossShoot.mStartX = this.sexyatoi(text);
			}
			if (this.GetAttribute(elem, this._S("endx"), ref text))
			{
				bossShoot.mEndX = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("starty"), ref text))
			{
				bossShoot.mStartY = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("endy"), ref text))
			{
				bossShoot.mEndY = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("x"), ref text))
			{
				bossShoot.SetX((float)this.StrToInt(text));
			}
			if (this.GetAttribute(elem, this._S("y"), ref text))
			{
				bossShoot.SetY((float)this.StrToInt(text));
			}
			if (this.GetAttribute(elem, this._S("UseShield"), ref text))
			{
				bossShoot.mUseShield = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("ShieldRotSpeed"), ref text))
			{
				bossShoot.mShieldRotateSpeed = MathUtils.DegreesToRadians(this.StrToFloat(text));
			}
			if (this.GetAttribute(elem, this._S("ShieldRespawnTime"), ref text))
			{
				bossShoot.mShieldQuadRespawnTime = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("ShieldPauseTime"), ref text))
			{
				bossShoot.mShieldPauseTime = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("EnrageShieldRestore"), ref text))
			{
				bossShoot.mEnrageShieldRestore = this.StrToBool(text);
			}
			num3 = 1;
			while (this.GetAttribute(elem, "x" + num3, ref text))
			{
				Point point = new Point();
				point.mX = this.StrToInt(text);
				if (!this.GetAttribute(elem, "y" + num3, ref text))
				{
					return this.Fail("You must have an x<num> for every y<num> and v.v");
				}
				point.mY = this.StrToInt(text);
				bossShoot.mPoints.Add(point);
				num3++;
			}
			if (this.GetAttribute(elem, this._S("CanShootBullets"), ref text))
			{
				bossShoot.mCanShootBullets = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("ShotType"), ref text))
			{
				if (this.StrEquals(text, this._S("straight")))
				{
					bossShoot.mShotType = 0;
				}
				else if (this.StrEquals(text, this._S("sine")))
				{
					bossShoot.mShotType = 2;
				}
				else if (this.StrEquals(text, this._S("target")) || this.StrEquals(text, this._S("targeted")))
				{
					bossShoot.mShotType = 1;
				}
				else if (this.StrEquals(text, this._S("homing")))
				{
					bossShoot.mShotType = 3;
				}
				else if (this.StrEquals(text, this._S("volcano")))
				{
					bossShoot.mShotType = 4;
				}
				else
				{
					if (!this.StrEquals(text, this._S("any")) && !this.StrEquals(text, this._S("all")))
					{
						return this.Fail("Invalid /shottype/ entered for boss: " + this.ToString(text));
					}
					bossShoot.mShotType = 5;
				}
			}
			if (this.GetAttribute(elem, this._S("VolcanoOffscreenDelay"), ref text))
			{
				bossShoot.mVolcanoOffscreenDelay = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("art"), ref text) && bossShoot.mShotType == 3 && this.GetAttribute(elem, this._S("homingspeed"), ref text))
			{
				bossShoot.mHomingCorrectionAmt = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("retalspeedmin"), ref text))
			{
				bossShoot.mMinRetalSpeed = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("retalspeedmax"), ref text))
			{
				bossShoot.mMaxRetalSpeed = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("shotdelay"), ref text))
			{
				bossShoot.mShotDelay = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("retalshotdelay"), ref text))
			{
				bossShoot.mRetalShotDelay = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("EndHoverOnHit"), ref text))
			{
				bossShoot.mEndHoverOnHit = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("FlightSpeed"), ref text))
			{
				bossShoot.mFlightSpeed = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("MinFlightDist"), ref text))
			{
				bossShoot.mFlightMinDist = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("colorvampire"), ref text))
			{
				bossShoot.mColorVampire = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("strafe"), ref text))
			{
				bossShoot.mStrafe = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("enrageamt"), ref text))
			{
				bossShoot.mIncMaxShotHealthAmt = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("retalenrageamt"), ref text))
			{
				bossShoot.mIncRetalMaxShotHealthAmt = this.StrToInt(text);
			}
			if (bossShoot.mColorVampire)
			{
				this.GET_BOSS_ELEM(elem, this._S("avoidcolor"), ref text);
				bossShoot.mAvoidColor = this.StrToBool(text);
				this.GET_BOSS_ELEM(elem, this._S("vamphealthinc"), ref text);
				bossShoot.mColorVampHealthInc = this.StrToInt(text);
				this.GET_BOSS_ELEM(elem, this._S("vampcolorchangemin"), ref text);
				bossShoot.mMinColorChangeTime = this.StrToInt(text);
				this.GET_BOSS_ELEM(elem, this._S("vampcolorchangemax"), ref text);
				bossShoot.mMaxColorChangeTime = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("ColorHelp"), ref text))
			{
				bossShoot.mColorVampChanceToMatch2ndBall = this.StrToInt(text);
				if (bossShoot.mColorVampire && bossShoot.mAvoidColor)
				{
					GameApp.gApp.MsgBox("You have a color vamp boss with /avoidcolor/ set to true,\nbut you also defined /ColorHelp/: these aren't used together, fyi.", "Yo", 0);
				}
			}
			bool flag3 = true;
			if (this.GetAttribute(elem, this._S("MoveMode"), ref text))
			{
				bossShoot.mMovementMode = this.StrToInt(text);
				if (bossShoot.mMovementMode < 0 || bossShoot.mMovementMode > 2)
				{
					return this.Fail("/MoveMode/ must be between 0 and 2");
				}
				if (bossShoot.mMovementMode != 0)
				{
					flag3 = false;
				}
			}
			if (this.GetAttribute(elem, this._S("Accel"), ref text))
			{
				bossShoot.mMovementAccel = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("MoveDelay"), ref text))
			{
				bossShoot.mDefaultMovementUpdateDelay = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("minhover"), ref text))
			{
				bossShoot.mMinHoverTime = this.sexyatoi(text);
			}
			else if (flag3)
			{
				return this.Fail("you must specify /minhover/ and /maxhover/ if you aren't doing MirrorPlayer or OppositePlayer");
			}
			if (this.GetAttribute(elem, this._S("maxhover"), ref text))
			{
				bossShoot.mMaxHoverTime = this.StrToInt(text);
			}
			else if (flag3)
			{
				return this.Fail("you must specify /minhover/ and /maxhover/ if you aren't doing MirrorPlayer or OppositePlayer");
			}
			this.GET_BOSS_ELEM(elem, this._S("minfire"), ref text);
			bossShoot.mMinFireDelay = this.StrToInt(text);
			this.GET_BOSS_ELEM(elem, this._S("maxfire"), ref text);
			bossShoot.mMaxFireDelay = this.StrToInt(text);
			if (this.GetAttribute(elem, this._S("NormalPassUnder"), ref text))
			{
				bossShoot.mEatsBalls = this.StrToBool(text);
			}
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			this.GetAttribute(elem, this._S("stun"), ref text2);
			this.GetAttribute(elem, this._S("poison"), ref text3);
			this.GetAttribute(elem, this._S("hallucinate"), ref text4);
			this.GetAttribute(elem, this._S("SlowShot"), ref text5);
			if (text2.Length > 0)
			{
				bossShoot.mFrogStunTime = this.StrToInt(text2);
			}
			else if (text3.Length > 0)
			{
				bossShoot.mFrogPoisonTime = this.StrToInt(text3);
			}
			else if (text4.Length > 0)
			{
				bossShoot.mFrogHallucinateTime = this.StrToInt(text4);
			}
			else if (text5.Length > 0)
			{
				bossShoot.mFrogSlowTimer = this.StrToInt(text5);
			}
			this.GET_BOSS_ELEM(elem, this._S("minbullet"), ref text);
			bossShoot.mMinBulletSpeed = this.StrToFloat(text);
			this.GET_BOSS_ELEM(elem, this._S("maxbullet"), ref text);
			bossShoot.mMaxBulletSpeed = this.StrToFloat(text);
			if (this.GetAttribute(elem, this._S("subtype"), ref text))
			{
				bossShoot.mSubType = this.StrToInt(text) - 1;
			}
			if (this.GetAttribute(elem, this._S("maxbullets"), ref text))
			{
				bossShoot.mMaxBulletsToFire = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("retaliation"), ref text))
			{
				bossShoot.mMaxRetaliationBullets = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("SineShotsTargetPlayer"), ref text))
			{
				bossShoot.mSineShotsTargetPlayer = this.StrToBool(text);
			}
			if (this.GetAttribute(elem, this._S("MinSineShotTime"), ref text))
			{
				bossShoot.mMinSineShotTime = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("MaxSineShotTime"), ref text))
			{
				bossShoot.mMaxSineShotTime = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("retaltype"), ref text))
			{
				bossShoot.mSinusoidalRetaliation = this.StrEquals(text, this._S("sine")) || this.StrEquals(text, this._S("sinusoidal"));
			}
			if (this.GetAttribute(elem, this._S("minamp"), ref text))
			{
				bossShoot.mMinAmp = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("maxamp"), ref text))
			{
				bossShoot.mMaxAmp = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("minfreq"), ref text))
			{
				bossShoot.mMinFreq = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("maxfreq"), ref text))
			{
				bossShoot.mMaxFreq = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("MinSineYInc"), ref text))
			{
				bossShoot.mMinYInc = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("MaxSineYInc"), ref text))
			{
				bossShoot.mMaxYInc = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("MinSineXInc"), ref text))
			{
				bossShoot.mMinXInc = this.StrToFloat(text);
			}
			if (this.GetAttribute(elem, this._S("MaxSineXInc"), ref text))
			{
				bossShoot.mMaxXInc = this.StrToFloat(text);
			}
			this.GET_BOSS_ELEM(elem, this._S("movespeed"), ref text);
			bossShoot.mSpeed = this.StrToFloat(text);
			bossShoot.mDefaultSpeed = bossShoot.mSpeed;
			if (this.GetAttribute(elem, this._S("decminhover"), ref text))
			{
				bossShoot.mDecMinHover = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("decmaxhover"), ref text))
			{
				bossShoot.mDecMaxHover = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("decminfire"), ref text))
			{
				bossShoot.mDecMinFire = this.StrToInt(text);
			}
			if (this.GetAttribute(elem, this._S("decmaxfire"), ref text))
			{
				bossShoot.mDecMaxFire = this.StrToInt(text);
			}
			while (this.mXMLParser.NextElement(ref elem))
			{
				if (elem.mType == 1)
				{
					if (this.StrEquals(elem.mValue, this._S("DDS")) && !this.DoParseBossDDS(elem, l.mId, l))
					{
						return false;
					}
					if (this.StrEquals(elem.mValue, this._S("Berserk")) && !this.DoParseBossBerserk(elem, boss))
					{
						return false;
					}
					if (this.StrEquals(elem.mValue, this._S("Skeleton")) && !this.DoParseBossSkeletonEmitter(elem, boss as BossSkeleton))
					{
						return false;
					}
					if (this.StrEquals(elem.mValue, this._S("Hint")) && !this.DoParseBossHintText(elem, boss))
					{
						return false;
					}
					if (this.StrEquals(elem.mValue, this._S("Hula")) && !this.DoParseHula(elem, boss))
					{
						return false;
					}
					if (this.StrEquals(elem.mValue, this._S("DeathText")))
					{
						if (!this.GetAttribute(elem, this._S("line"), ref text) && !this.GetAttribute(elem, this._S("value"), ref text))
						{
							return this.Fail("<DeathText> section needs a /line/ or /value/ tag");
						}
						boss.mDeathText.Add(new BossText(text));
					}
				}
				else if (elem.mType == 2 && this.StrEquals(elem.mValue, this._S("Boss")))
				{
					break;
				}
			}
			return true;
		}

		protected bool DoParseHandheldBalance(BXMLElement elem)
		{
			string str = "";
			if (!this.GetAttribute(elem, this._S("FruitPowerupDuration"), ref str))
			{
				return this.Fail("Expected /FruitPowerupDuration/ tag in <HandheldBalance> section");
			}
			float num = this.StrToFloat(str);
			if (!this.GetAttribute(elem, this._S("SameColorChance"), ref str))
			{
				return this.Fail("Expected /SameColorChance/ tag in <HandheldBalance> section");
			}
			float num2 = this.StrToFloat(str);
			float[] array = new float[DDS.NUM_ADVENTURE_ZONES];
			for (int i = 1; i < DDS.NUM_ADVENTURE_ZONES + 1; i++)
			{
				string theName = "Adventure" + i;
				if (!this.GetAttribute(elem, theName, ref str))
				{
					return this.Fail("Expected /AdventureX/ tag in <HandheldBalance> section");
				}
				array[i - 1] = this.StrToFloat(str);
			}
			float[] array2 = new float[DDS.NUM_CHALLENGE_LEVELS];
			for (int j = 1; j < DDS.NUM_CHALLENGE_LEVELS + 1; j++)
			{
				string theName2 = "Challenge" + j;
				if (!this.GetAttribute(elem, theName2, ref str))
				{
					return this.Fail("Expected /ChallengeX/ tag in <HandheldBalance> section");
				}
				array2[j - 1] = this.StrToFloat(str);
			}
			GameApp.gDDS.mHandheldBalance.mFruitPowerupAdditionalDuration = 1f + num / 100f;
			GameApp.gDDS.mHandheldBalance.mChanceOfSameColorBallIncrease = 1f + num2 / 100f;
			uint num3 = 0U;
			while ((ulong)num3 < (ulong)((long)DDS.NUM_ADVENTURE_ZONES))
			{
				GameApp.gDDS.mHandheldBalance.mAdventureModeSpeedDelta[(int)((UIntPtr)num3)] = array[(int)((UIntPtr)num3)] / 100f;
				num3 += 1U;
			}
			uint num4 = 0U;
			while ((ulong)num4 < (ulong)((long)DDS.NUM_CHALLENGE_LEVELS))
			{
				GameApp.gDDS.mHandheldBalance.mChallengeModeSpeedDelta[(int)((UIntPtr)num4)] = array2[(int)((UIntPtr)num4)] / 100f;
				num4 += 1U;
			}
			return true;
		}

		protected bool DoParseBossSkeletonEmitter(BXMLElement elem, BossSkeleton bs)
		{
			if (bs == null)
			{
				return this.Fail("Found <Skeleton> section but boss isn't a skeleton boss");
			}
			string str = "";
			if (!this.GetAttribute(elem, this._S("NumPerGroup"), ref str))
			{
				return this.Fail("Expected /NumPerGroup/ tag in <Skeleton> section");
			}
			bs.mNumSkeleToEmit = this.StrToInt(str);
			if (!this.GetAttribute(elem, this._S("GroupDelay"), ref str))
			{
				return this.Fail("Expected /GroupDelay/ tag in <Skeleton> section");
			}
			bs.mSkeleDelay = this.StrToInt(str);
			if (!this.GetAttribute(elem, this._S("DelayAfterEmit"), ref str))
			{
				return this.Fail("Expected /DelayAfterEmit/ tag in <Skeleton> section");
			}
			bs.mDelayAfterSkeleEmit = this.StrToInt(str);
			string text = "";
			string text2 = "";
			this.GetAttribute(elem, this._S("vx"), ref text);
			this.GetAttribute(elem, this._S("vy"), ref text2);
			if (text.Length == 0 && text2.Length == 0)
			{
				return this.Fail("Expected /vx/ or /vy/ tag in <Skeleton> section");
			}
			bs.mSkeletonVX = this.StrToFloat(text);
			bs.mSkeletonVY = this.StrToFloat(text2);
			if (!this.GetAttribute(elem, this._S("x"), ref str))
			{
				return this.Fail("Expected /x/ tag in <Skeleton> section");
			}
			bs.mSkeletonEmitX = this.StrToFloat(str);
			if (!this.GetAttribute(elem, this._S("y"), ref str))
			{
				return this.Fail("Expected /y/ tag in <Skeleton> section");
			}
			bs.mSkeletonEmitY = this.StrToFloat(str);
			return true;
		}

		protected bool DoParseBossBerserk(BXMLElement elem, Boss b)
		{
			string text = "";
			if (!this.GetAttribute(elem, this._S("value"), ref text))
			{
				return this.Fail("Expected /value/ tag in <Berserk> section");
			}
			string str = "";
			bool flag = this.StrEquals(text, this._S("movement"));
			if (!flag && !this.GetAttribute(elem, this._S("amount"), ref str))
			{
				return this.Fail("Expected /amount/ tag in <Berserk> section");
			}
			string str2 = "";
			if (!this.GetAttribute(elem, this._S("HealthLimit"), ref str2))
			{
				return this.Fail("Expected /HealthLimit/ tag in <Berserk> section");
			}
			int num = this.StrToInt(str2);
			string str3 = "";
			string str4 = "";
			string str5 = "";
			this.GetAttribute(elem, this._S("min"), ref str3);
			this.GetAttribute(elem, this._S("max"), ref str4);
			bool @override = false;
			if (this.GetAttribute(elem, this._S("override"), ref str5))
			{
				@override = this.StrToBool(str5);
			}
			if (this.StrEquals(text, this._S("ShotType")))
			{
				if (this.StrEquals(str, this._S("straight")))
				{
					str = this._S(string.Concat(0));
				}
				else if (this.StrEquals(str, this._S("sine")))
				{
					str = this._S(string.Concat(2));
				}
				else if (this.StrEquals(str, this._S("target")) || this.StrEquals(str2, this._S("targeted")))
				{
					str = this._S(string.Concat(1));
				}
				else if (this.StrEquals(str, this._S("homing")))
				{
					str = this._S(string.Concat(3));
				}
				else if (this.StrEquals(str, this._S("volcano")))
				{
					str = this._S(string.Concat(4));
				}
				else if (this.StrEquals(str, this._S("any")) || this.StrEquals(str2, this._S("all")))
				{
					str = this._S(string.Concat(5));
				}
			}
			if (!flag)
			{
				string str6 = this.ToString(str3);
				string str7 = this.ToString(str4);
				b.AddBerserkValue(num, text, this.ToString(str), ref str6, ref str7, @override);
				str3 = this.ToString(str6);
				str4 = this.ToString(str7);
			}
			else
			{
				BossBerserkMovement bossBerserkMovement = new BossBerserkMovement();
				if (this.GetAttribute(elem, this._S("startx"), ref str2))
				{
					bossBerserkMovement.mStartX = this.StrToInt(str2);
				}
				if (this.GetAttribute(elem, this._S("endx"), ref str2))
				{
					bossBerserkMovement.mEndX = this.StrToInt(str2);
				}
				if (this.GetAttribute(elem, this._S("starty"), ref str2))
				{
					bossBerserkMovement.mStartY = this.StrToInt(str2);
				}
				if (this.GetAttribute(elem, this._S("endy"), ref str2))
				{
					bossBerserkMovement.mEndY = this.StrToInt(str2);
				}
				if (this.GetAttribute(elem, this._S("x"), ref str2))
				{
					bossBerserkMovement.mX = this.StrToInt(str2);
				}
				if (this.GetAttribute(elem, this._S("y"), ref str2))
				{
					bossBerserkMovement.mY = this.StrToInt(str2);
				}
				int num2 = 1;
				while (this.GetAttribute(elem, this._S("x" + num2), ref str2))
				{
					Point point = new Point();
					point.mX = this.StrToInt(str2);
					if (!this.GetAttribute(elem, this._S("y" + num2), ref str2))
					{
						return this.Fail("You must have an x<num> for every y<num> and v.v (error in <Berserk value=\"movement\"...> tag)");
					}
					point.mY = this.StrToInt(str2);
					bossBerserkMovement.mPoints.Add(point);
					num2++;
				}
				BossShoot bossShoot = b as BossShoot;
				if (bossShoot == null)
				{
					return this.Fail("Unable to cast from Boss* to BossShoot* while parsing <Berserk value=\"movement\"...> tag.");
				}
				bossBerserkMovement.mHealthLimit = num;
				bossShoot.AddBerserkMovement(bossBerserkMovement);
				b.AddBerserkValue(num, this._S(""), "");
			}
			return true;
		}

		protected bool DoParseBossHintText(BXMLElement elem, Boss b)
		{
			string text = "";
			if (!this.GetAttribute(elem, this._S("text"), ref text))
			{
				return this.Fail("Expected /text/ tag in <Hint> section");
			}
			TauntText tauntText = new TauntText();
			tauntText.mText = text;
			if (!this.GetAttribute(elem, this._S("condition"), ref text))
			{
				return this.Fail("Expected /condition/ tag in <Hint> section");
			}
			tauntText.mCondition = this.StrToInt(text);
			if (!this.GetAttribute(elem, this._S("deaths"), ref text))
			{
				return this.Fail("Expected /deaths/ tag in <Hint> section");
			}
			tauntText.mMinDeaths = this.StrToInt(text);
			if (!this.GetAttribute(elem, this._S("delay"), ref text))
			{
				return this.Fail("Expected /delay/ tag in <Hint> section");
			}
			tauntText.mDelay = this.StrToInt(text);
			text = this._S("0");
			if (tauntText.mCondition > 0 && !this.GetAttribute(elem, this._S("mintime"), ref text))
			{
				return this.Fail("Expected /mintime/ tag for <Hint> sections using a /condition/ value greater than 0");
			}
			tauntText.mMinTime = this.StrToInt(text);
			b.mTauntText.Add(tauntText);
			return true;
		}

		protected bool DoParseHula(BXMLElement elem, Boss b)
		{
			string str = "";
			if (!this.GetAttribute(elem, this._S("vx"), ref str))
			{
				return false;
			}
			float vx = this.StrToFloat(str);
			if (!this.GetAttribute(elem, this._S("projvy"), ref str))
			{
				return false;
			}
			float projvy = this.StrToFloat(str);
			if (!this.GetAttribute(elem, this._S("spawn"), ref str))
			{
				return false;
			}
			int spawn = this.StrToInt(str);
			if (!this.GetAttribute(elem, this._S("spawny"), ref str))
			{
				return false;
			}
			int spawny = this.StrToInt(str);
			if (!this.GetAttribute(elem, this._S("projchance"), ref str))
			{
				return false;
			}
			int proj_chance = this.StrToInt(str);
			int proj_range;
			if (this.GetAttribute(elem, this._S("projrange"), ref str))
			{
				proj_range = this.StrToInt(str);
			}
			else
			{
				proj_range = 0;
			}
			if (!this.GetAttribute(elem, this._S("berserk"), ref str))
			{
				return false;
			}
			int berserk_amt = this.StrToInt(str);
			int atime = 0;
			int atype = 0;
			if (this.GetAttribute(elem, this._S("hallucinate"), ref str))
			{
				atime = this.StrToInt(str);
				atype = 3;
			}
			else if (this.GetAttribute(elem, this._S("stun"), ref str))
			{
				atime = this.StrToInt(str);
				atype = 1;
			}
			else if (this.GetAttribute(elem, this._S("poison"), ref str))
			{
				atime = this.StrToInt(str);
				atype = 2;
			}
			else if (this.GetAttribute(elem, this._S("slow"), ref str))
			{
				atime = this.StrToInt(str);
				atype = 4;
			}
			int amnesty = 0;
			if (this.GetAttribute(elem, this._S("amnesty"), ref str))
			{
				amnesty = this.StrToInt(str);
			}
			b.AddHulaEntry(vx, projvy, spawn, spawny, proj_chance, berserk_amt, proj_range, atype, atime, amnesty);
			return true;
		}

		protected void CopyLevel(Level src, ref Level dst, Board b)
		{
			dst = src.Instantiate();
			for (int i = 0; i < dst.mNumCurves; i++)
			{
				dst.mCurveMgr[i] = new CurveMgr(b, src.mCurveMgr[i].mCurveNum);
				dst.mCurveMgr[i].Copy(src.mCurveMgr[i], dst, b);
			}
			if (src.mBoss != null)
			{
				dst.mBoss = src.mBoss.Instantiate();
				dst.mBoss.mName = src.mDisplayName;
				dst.mBoss.PostInstantiationHook(src.mBoss);
				dst.mBoss.mLevel = dst;
				dst.mOrgBoss = dst.mBoss;
			}
			if (src.mSecondaryBoss != null)
			{
				dst.mSecondaryBoss = src.mSecondaryBoss.Instantiate();
				dst.mSecondaryBoss.mName = src.mDisplayName;
				dst.mSecondaryBoss.PostInstantiationHook(src.mSecondaryBoss);
				dst.mSecondaryBoss.mLevel = dst;
			}
			dst.mHoleMgr = new HoleMgr(src.mHoleMgr);
			dst.CopyFrom(src);
			for (int j = 0; j < dst.mNumCurves; j++)
			{
				if (!dst.mCurveMgr[j].mIsLoaded)
				{
					dst.mCurveMgr[j].LoadCurve();
				}
			}
		}

		public LevelMgr()
		{
			this.mBeatGamePointsForLife = 1000;
			this.mGauntletNumForMultBase = 2;
			this.mGauntletNumForMultInc = 0;
			this.mMultiplierDuration = 100;
			this.mHasFailed = false;
			this.mNumDDSTiers = 0;
			this.mXMLParser = null;
			this.mCannonStacks = true;
			this.mLazerStacks = false;
			this.mBossTauntChance = 0;
			this.mMultBallLife = 1000;
			this.mMultBallPoints = 100;
			this.mGauntletTFreq = 1000;
			this.mCannonShots = (this.mLazerShots = 3);
			this.mPowerDelay = 1500;
			this.mPowerCooldown = 1000;
			this.mPowerupIncAtZumaPct = 1f;
			this.mPowerIncPct = 0f;
			this.mMaxGauntletNumForMult = 0;
			this.mMultiplierTimeAdd = 0;
			this.mPointTimeAdd = (this.mNumPointsForTimeAdd = 0);
			this.mAllowColorNukeAfterZuma = true;
			this.mColorNukeTimeAfterZuma = -1;
			this.mBossesCanAttackFuckedFrog = true;
			this.mAttackDelayAfterHittingFrog = 0;
			this.mPointsForBronze = 5000;
			this.mPointsForSilver = 10000;
			this.mPointsForGold = 15000;
			this.mUniquePowerupColor = false;
			this.mCapAffectsPowerupsSpawned = true;
			this.mFirstIronFrogLevel = (this.mLastIronFrogLevel = -1);
			this.mEffectManager = null;
			this.mPowerupSpawnDelay = 0;
			this.mMaxZumaPctForColorNuke = -1f;
			this.mClearCurveRolloutPct = 0.1f;
			this.mGauntletSessionLength = 0;
			this.mIsHardConfig = false;
			this.mClearCurvePoints = 1000;
			this.mMinMultBallDistance = 0f;
			this.mPointsForLife = 10000;
			this.mPostZumaTime = 0;
			this.mPostZumaTimeSlowInc = (this.mPostZumaTimeSpeedInc = 0f);
			MapScreen.gZoneNames[0] = TextManager.getInstance().getString(839);
			MapScreen.gZoneNames[1] = TextManager.getInstance().getString(840);
			MapScreen.gZoneNames[2] = TextManager.getInstance().getString(841);
			MapScreen.gZoneNames[3] = TextManager.getInstance().getString(842);
			MapScreen.gZoneNames[4] = TextManager.getInstance().getString(843);
			MapScreen.gZoneNames[5] = TextManager.getInstance().getString(844);
		}

		public void Init()
		{
			this.mEffectManager = new EffectManager();
		}

		public virtual void Dispose()
		{
			this.mXMLParser = null;
			this.mEffectManager = null;
			this.Reset();
		}

		public void Reset()
		{
			LevelMgr.gBossNum = 0;
			this.mError = "";
			this.mNumDDSTiers = 0;
			this.mDDSSlowAdd.Clear();
			this.mDDSPowerupPctInc.Clear();
			this.mDDSZumaPointDecPct.Clear();
			this.mDDSSpeedPct.Clear();
			this.mHasFailed = false;
			this.mLevels.Clear();
			this.mCannonStacks = true;
			this.mLazerStacks = false;
			this.mCannonShots = (this.mLazerShots = 3);
			this.mEffectManager.Reset();
		}

		public bool doLoadLevels()
		{
			BXMLElement bxmlelement = new BXMLElement();
			bool flag;
			while (!this.mXMLParser.HasFailed())
			{
				if (!this.mXMLParser.NextElement(ref bxmlelement))
				{
					this.Fail("Failed loading levels");
					this.mXMLParser = null;
					return false;
				}
				if (bxmlelement.mType == 1)
				{
					if (!(bxmlelement.mValue != "Config"))
					{
						flag = this.DoParseLevels();
						this.mXMLParser = null;
						this.mFirstIronFrogLevel = (this.mLastIronFrogLevel = -1);
						if (flag)
						{
							for (int i = 0; i < this.mLevels.size<Level>(); i++)
							{
								if (this.mFirstIronFrogLevel == -1 && this.mLevels[i].mIronFrog)
								{
									this.mFirstIronFrogLevel = i;
								}
								else if (this.mLevels[i].mIronFrog && i > this.mLastIronFrogLevel)
								{
									this.mLastIronFrogLevel = i;
								}
								if (this.mFirstIronFrogLevel != -1 && this.mLevels[i].mIronFrog)
								{
									this.mLevels[i].mZone = 7;
									this.mLevels[i].mNum = i - this.mFirstIronFrogLevel + 1;
								}
							}
						}
						return flag;
					}
					break;
				}
			}
			this.Fail("Expecting Config tag");
			flag = this.DoParseLevels();
			this.mXMLParser = null;
			return flag;
		}

		public bool LoadLevels(string theFilename)
		{
			this.mLevels.Clear();
			this.mHasFailed = false;
			this.mError = "";
			this.mXMLParser = new BXMLParser();
			string filename = theFilename + ".dat";
			this.mXMLParser.OpenStream(filename);
			return this.doLoadLevels();
		}

		public bool LoadLevels(byte[] data)
		{
			this.mLevels.Clear();
			this.mHasFailed = false;
			this.mError = "";
			this.mXMLParser = new BXMLParser();
			Buffer buffer = new Buffer();
			buffer.SetData(data, data.Length);
			this.mXMLParser.OpenBuffer(buffer);
			return this.doLoadLevels();
		}

		public string GetErrorText()
		{
			return this.mError;
		}

		public bool HadError()
		{
			return this.mError.Length != 0;
		}

		public static string GetZoneName(int zone_num)
		{
			return MapScreen.gZoneNames[zone_num];
		}

		public static string GetTerseZoneName(int zone_num)
		{
			switch (zone_num)
			{
			case 1:
				return "Jungle";
			case 2:
				return "Village";
			case 3:
				return "City";
			case 4:
				return "Coast";
			case 5:
				return "Grotto";
			case 6:
				return "Volcano";
			case 7:
				return "Iron Frog";
			default:
				return "";
			}
		}

		public int GetScoreTipIdx(int level_num)
		{
			List<int> list = new List<int>();
			for (int i = 7; i < Enumerable.Count<ScoreTip>(this.mScoreTips); i++)
			{
				if (this.mScoreTips[i].mMinLevel <= level_num)
				{
					list.Add(i);
				}
			}
			if (Enumerable.Count<int>(list) == 0)
			{
				return 0;
			}
			return list[MathUtils.SafeRand() % Enumerable.Count<int>(list)];
		}

		public bool GetLevelById(string id, ref Level desc, Board b)
		{
			for (int i = 0; i < Enumerable.Count<Level>(this.mLevels); i++)
			{
				if (ZumasRevenge.Common.StrICaseEquals(id, this.mLevels[i].mId))
				{
					this.CopyLevel(this.mLevels[i], ref desc, b);
					return true;
				}
			}
			return false;
		}

		public bool GetLevelByIndex(int index, ref Level desc, Board b)
		{
			if (index < 0 || index >= Enumerable.Count<Level>(this.mLevels))
			{
				return false;
			}
			this.CopyLevel(this.mLevels[index], ref desc, b);
			return true;
		}

		public Level GetLevelByIndex(int index)
		{
			if (index < 0 || index >= Enumerable.Count<Level>(this.mLevels))
			{
				return null;
			}
			return this.mLevels[index];
		}

		public Level GetLevelById(string id)
		{
			for (int i = 0; i < Enumerable.Count<Level>(this.mLevels); i++)
			{
				if (ZumasRevenge.Common.StrICaseEquals(id, this.mLevels[i].mId))
				{
					return this.mLevels[i];
				}
			}
			return null;
		}

		public string GetLevelId(int index)
		{
			if (index < 0 || index >= Enumerable.Count<Level>(this.mLevels))
			{
				return "";
			}
			return this.mLevels[index].mId;
		}

		public Level GetLevelByZone(int zone, int num)
		{
			for (int i = 0; i < Enumerable.Count<Level>(this.mLevels); i++)
			{
				if (this.mLevels[i].mZone == zone && this.mLevels[i].mNum == num)
				{
					return this.mLevels[i];
				}
			}
			return null;
		}

		public int GetLevelIndex(string id)
		{
			for (int i = 0; i < Enumerable.Count<Level>(this.mLevels); i++)
			{
				if (ZumasRevenge.Common.StrEquals(id, this.mLevels[i].mId))
				{
					return i;
				}
			}
			return -1;
		}

		public int GetStartingGauntletLevel(string id)
		{
			for (int i = 0; i < Enumerable.Count<Level>(this.mLevels); i++)
			{
				if (ZumasRevenge.Common.StrEquals(id, this.mLevels[i].mId))
				{
					return this.mLevels[i].mStartingGauntletLevel;
				}
			}
			return -1;
		}

		public void GetZoneInfo(string id, out int zone, out int levelnum)
		{
			for (int i = 0; i < Enumerable.Count<Level>(this.mLevels); i++)
			{
				if (ZumasRevenge.Common.StrEquals(id, this.mLevels[i].mId))
				{
					zone = this.mLevels[i].mZone;
					levelnum = this.mLevels[i].mNum;
					return;
				}
			}
			zone = -1;
			levelnum = -1;
		}

		public string GetZoneFruitId(int zone)
		{
			if (zone > 6)
			{
				zone = 6;
			}
			return this.mZones[zone - 1].mFruitId;
		}

		public string GetZoneStartId(int zone)
		{
			return LevelMgr.zones[zone - 1];
		}

		public int GetFirstIronFrogLevel()
		{
			return this.mFirstIronFrogLevel;
		}

		public int GetLastIronFrogLevel()
		{
			return this.mLastIronFrogLevel;
		}

		public bool GetLevelStrData(int index, ref string level_id, ref string level_disp_name)
		{
			if (index < 0 || index >= Enumerable.Count<Level>(this.mLevels))
			{
				return false;
			}
			if (level_id != null)
			{
				level_id = this.mLevels[index].mId;
			}
			if (level_disp_name != null)
			{
				level_disp_name = this.mLevels[index].mDisplayName;
			}
			return true;
		}

		private bool GetAttribute(BXMLElement elem, string theName, ref string theValue)
		{
			string text = theName.ToLower();
			if (elem.mAttributes.ContainsKey(text))
			{
				theValue = elem.mAttributes[text];
				return true;
			}
			return false;
		}

		private float StrToFloat(string str)
		{
			if (str == "")
			{
				return 0f;
			}
			return float.Parse(str, 167, CultureInfo.InvariantCulture);
		}

		private int StrToInt(string str)
		{
			if (str == "")
			{
				return 0;
			}
			return int.Parse(str);
		}

		private bool StrToBool(string str)
		{
			return !(str == "") && bool.Parse(str);
		}

		private string ToString(string str)
		{
			return str;
		}

		private string _S(string str)
		{
			return str;
		}

		private int sexyatoi(string str)
		{
			return this.StrToInt(str);
		}

		private float sexyatof(string str)
		{
			return this.StrToFloat(str);
		}

		private bool StrEquals(string str, string cmp)
		{
			return str == cmp;
		}

		private string StringToUpper(string str)
		{
			return str.ToUpper();
		}

		private string StringToLower(string str)
		{
			return str.ToLower();
		}

		private bool COMMON_WALL_FAIL(string str)
		{
			return this.Fail("Unabled to find \"" + str + "\" parameter in \"Wall\" or \"MovingWall\" tag");
		}

		private bool GET_BOSS_ELEM(BXMLElement elem, string ename, ref string str)
		{
			return this.GetAttribute(elem, ename, ref str) || this.Fail("Unable to find \"" + this.ToString(str) + "\" in \"Boss\" tag");
		}

		private bool GET_DESTPT_ELEM(BXMLElement elem, string ename, ref string str)
		{
			return this.GetAttribute(elem, ename, ref str) || this.Fail("Unable to find \"" + this.ToString(str) + "\" in \"Boss\".\"Waypoint\" tag");
		}

		private bool GET_ATTACK_ELEM(BXMLElement elem, string ename, ref string str)
		{
			return this.GetAttribute(elem, ename, ref str) || this.Fail("Unable to find \"" + this.ToString(str) + "\" in \"Boss\".\"Atttack\" tag");
		}

		private bool GET_GAUNTLET_ELEM(BXMLElement elem, string ename, ref string str)
		{
			return this.GetAttribute(elem, ename, ref str) || this.Fail("Unable to find \"" + ename + "\" in \"Difficulty\" tag for Gauntlet mode setting");
		}

		public const int TARGET_BAR_SIZE = 330;

		public const int FRED_TONGUE_X = 541;

		public const int STARTING_TORCH_TEXT_ALPHA = 700;

		public static bool gForceTreasure;

		public static int gBossNum = 0;

		private static string[] zones = new string[] { "jungle1", "village1", "city1", "coast1", "grotto1", "volcano1" };

		public List<Point> mMapPoints = new List<Point>();

		public bool mHasFailed;

		public bool mIsHardConfig;

		public string mCurDir;

		public EffectManager mEffectManager;

		public List<string> mLevelTips = new List<string>();

		public List<int> mLevelTipIds = new List<int>();

		public List<ScoreTip> mScoreTips = new List<ScoreTip>();

		public string mLevelXML = "";

		public int mGauntletSessionLength;

		public int mGauntletNumForMultBase;

		public int mGauntletNumForMultInc;

		public int mGauntletTFreq;

		public int mMaxGauntletNumForMult;

		public int mMultiplierTimeAdd;

		public int mPointTimeAdd;

		public int mNumPointsForTimeAdd;

		public int mMultiplierDuration;

		public int mCannonShots;

		public int mBossTauntChance;

		public int mMultBallLife;

		public int mMultBallPoints;

		public int mPointsForBronze;

		public int mPointsForSilver;

		public int mPointsForGold;

		public float mPowerupIncAtZumaPct;

		public float mPowerIncPct;

		public int mClearCurvePoints;

		public float mClearCurveSpeedMult;

		public float mClearCurveRolloutPct;

		public float mCannonAngle;

		public float mMaxZumaPctForColorNuke;

		public int mLazerShots;

		public bool mCannonStacks;

		public bool mLazerStacks;

		public int mPowerDelay;

		public int mPowerCooldown;

		public int mPowerupSpawnDelay;

		public bool mAllowColorNukeAfterZuma;

		public int mColorNukeTimeAfterZuma;

		public bool mUniquePowerupColor;

		public bool mCapAffectsPowerupsSpawned;

		public int mPostZumaTime;

		public float mPostZumaTimeSpeedInc;

		public float mPostZumaTimeSlowInc;

		public float mMinMultBallDistance;

		public int mPointsForLife;

		public bool mBossesCanAttackFuckedFrog;

		public int mAttackDelayAfterHittingFrog;

		public int mBeatGamePointsForLife;

		public int mNumDDSTiers;

		public List<float> mDDSPowerupPctInc = new List<float>();

		public List<float> mDDSSpeedPct = new List<float>();

		public List<int> mDDSSlowAdd = new List<int>();

		public List<float> mDDSZumaPointDecPct = new List<float>();

		public string mError = "";

		public List<Level> mLevels = new List<Level>();

		public BXMLParser mXMLParser = new BXMLParser();

		public ZoneInfo[] mZones = new ZoneInfo[7];

		public int mFirstIronFrogLevel;

		public int mLastIronFrogLevel;
	}
}
