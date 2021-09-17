using System;
using System.Collections.Generic;
using System.Linq;
using JeffLib;
using SexyFramework;
using SexyFramework.AELib;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;

namespace ZumasRevenge
{
	public class Level : IDisposable
	{
		protected void InitFinalBossLevel()
		{
			if (!this.mApp.mResourceManager.IsGroupLoaded("CloakedBoss") && !this.mApp.mResourceManager.LoadResources("CloakedBoss"))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
			}
			this.mDoTorchCrap = true;
			this.mBoard.mPreventBallAdvancement = true;
			this.mTorchTextAlpha = 700f;
			this.mTorchStageState = 0;
			this.mTorchStageTimer = ZumasRevenge.Common._M(150);
			this.mTorchDaisScale = 1f;
			this.mTorchCompMgr = this.mApp.LoadComposition("pax\\cloakedboss", "_BOSSES");
			Composition composition = this.mTorchCompMgr.GetComposition("squish");
			this.mTorchBossX = (float)(-(float)ZumasRevenge.Common._DS(composition.mWidth) - ZumasRevenge.Common._DS(ZumasRevenge.Common._M(500)));
			this.mTorchBossY = (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-920));
			this.mTorchBossDestX = (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-520));
			this.mTorchBossDestY = (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-462));
			int num = ZumasRevenge.Common._M(50);
			this.mTorchBossVX = (this.mTorchBossDestX - this.mTorchBossX) / (float)num;
			this.mTorchBossVY = (this.mTorchBossDestY - this.mTorchBossY) / (float)num;
			for (int i = 0; i < this.mTorches.Count; i++)
			{
				this.mTorches[i].mActive = (this.mTorches[i].mDraw = true);
			}
			for (int j = 0; j < 3; j++)
			{
				this.mCloakedBossTextAlpha[j] = 0f;
			}
		}

		public Level()
		{
			this.mCurMultiplierTimeLeft = (this.mMaxMultiplierTime = 0);
			this.mTorchStageState = -1;
			this.mTorchBossX = (this.mTorchBossY = -1f);
			this.mTorchDaisScale = 1f;
			this.mTorchCompMgr = null;
			this.mTorchStageTimer = 0;
			this.mTorchBossVX = (this.mTorchBossVY = (this.mTorchBossDestX = (this.mTorchBossDestY = 0f)));
			this.mFrogFlyOff = null;
			this.mTorchStageShakeAmt = 0;
			this.mNumGauntletBallsBroke = 0;
			this.mBossBGID = "";
			this.mZumaPulseUCStart = 0;
			this.mCurGauntletMultPct = 0f;
			this.mChallengePoints = 100;
			this.mChallengeAcePoints = 1000;
			this.mTorchStageAlpha = 0f;
			this.mGauntletCurTime = 0;
			this.mCloakPoof = null;
			this.mCloakClapFrame = -1;
			this.mCanDrawBoss = true;
			this.mIndex = -1;
			this.mIronFrog = false;
			this.mStartingGauntletLevel = 1;
			this.mAllCurvesAtRolloutPoint = false;
			this.mHasReachedCruisingSpeed = false;
			this.mPotPct = 1f;
			this.mFrog = null;
			this.mUpdateCount = 0;
			this.mFireSpeed = 8f;
			this.mBGFromPSD = false;
			this.mCurBarSizeInc = 1;
			this.mEndSequence = -1;
			this.mDoTorchCrap = false;
			this.mHasDoneTorchCrap = false;
			this.mTorchTextAlpha = 0f;
			this.mReloadDelay = 0;
			this.mTreasureFreq = 300;
			this.mParTime = 0;
			this.mBoss = (this.mOrgBoss = null);
			this.mSecondaryBoss = null;
			for (int i = 0; i < 4; i++)
			{
				this.mCurveSkullAngleOverrides[i] = float.MaxValue;
			}
			this.mLoopAtEnd = false;
			this.mIsEndless = false;
			this.mInvertMouseTimer = (this.mMaxInvertMouseTimer = 0);
			this.mTimer = (this.mTimeToComplete = -1);
			this.mBossFreezePowerupTime = (this.mFrogShieldPowerupCount = 300);
			this.mSliderEdgeRotate = false;
			this.mTorchTimer = 0;
			this.mFinalLevel = false;
			this.mNoBackground = false;
			this.mFurthestBallDistance = 0;
			this.mOffscreenClearBonus = false;
			this.mIntroTorchDelay = 0;
			this.mIntroTorchIndex = -1;
			for (int j = 0; j < 5; j++)
			{
				this.mFrogImages[j] = new LillyPadImageInfo();
				this.mFrogImages[j].mImage = null;
			}
			this.mPostZumaTimeCounter = 0;
			this.mPostZumaTimeSlowInc = (this.mPostZumaTimeSpeedInc = 0f);
			this.mZone = (this.mNum = -1);
			this.mApp = GameApp.gApp;
			this.mBoard = ((this.mApp != null) ? this.mApp.GetBoard() : null);
			this.mHurryToRolloutAmt = 0f;
			this.mTempSpeedupTimer = 0;
			this.mSuckMode = false;
			this.mMoveType = 0;
			this.mMoveSpeed = 25;
			this.mNumFrogPoints = 0;
			this.mCurFrogPoint = 0;
			this.mFrogX[0] = 320;
			this.mFrogY[0] = 240;
			this.mDoingPadHints = false;
			this.mBarWidth = (this.mBarHeight = 0);
			this.mNoFlip = false;
			this.mHoleMgr = new HoleMgr();
			this.mDrawCurves = false;
			for (int k = 0; k < 4; k++)
			{
				this.mCurveMgr[k] = null;
			}
			this.mNumCurves = 0;
			this.mMirrorType = MirrorType.MirrorType_None;
			if (this.mApp != null)
			{
				this.mGingerMouthXStart = (float)this.mApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_JAW)));
				this.mFredMouthXStart = (float)this.mApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_JAW)));
			}
			this.mZumaBarX = 344;
			this.mZumaBarWidth = int.MaxValue;
			this.Reset();
		}

		public virtual Level Clone()
		{
			Level level = (Level)base.MemberwiseClone();
			level.mCurveMgr = new CurveMgr[4];
			level.mCloakedBossTextAlpha = (float[])this.mCloakedBossTextAlpha.Clone();
			level.mDaisRocks = new List<DaisRock>();
			if (this.mDaisRocks != null)
			{
				level.mDaisRocks.AddRange(this.mDaisRocks.ToArray());
			}
			level.mEggs = new List<TorchLevelEgg>();
			if (this.mEggs != null)
			{
				level.mEggs.AddRange(this.mEggs.ToArray());
			}
			level.mMovingWallDefaults = new List<Wall>();
			if (this.mMovingWallDefaults != null)
			{
				level.mMovingWallDefaults.AddRange(this.mMovingWallDefaults.ToArray());
			}
			level.mEffects = new List<Effect>();
			if (this.mEffects != null)
			{
				level.mEffects.AddRange(this.mEffects.ToArray());
			}
			level.mCloakPoof = this.mCloakPoof;
			level.mFrogFlyOff = this.mFrogFlyOff;
			level.mPowerupRegions = new List<PowerupRegion>();
			if (this.mPowerupRegions != null)
			{
				level.mPowerupRegions.AddRange(this.mPowerupRegions.ToArray());
			}
			level.mTorches = new List<Torch>();
			if (this.mTorches != null)
			{
				level.mTorches.AddRange(this.mTorches.ToArray());
			}
			level.mEffectNames = new List<string>();
			if (this.mEffectNames != null)
			{
				level.mEffectNames.AddRange(this.mEffectNames.ToArray());
			}
			level.mEffectParams = new List<EffectParams>();
			if (this.mEffectParams != null)
			{
				level.mEffectParams.AddRange(this.mEffectParams.ToArray());
			}
			level.mTreasurePoints = new List<TreasurePoint>();
			if (this.mTreasurePoints != null)
			{
				level.mTreasurePoints.AddRange(this.mTreasurePoints.ToArray());
			}
			level.mCurveMgr = (CurveMgr[])this.mCurveMgr.Clone();
			level.mCurveSkullAngleOverrides = (float[])this.mCurveSkullAngleOverrides.Clone();
			level.mHoleMgr = this.mHoleMgr;
			level.mTunnelData = new List<TunnelData>();
			if (this.mTunnelData != null)
			{
				level.mTunnelData.AddRange(this.mTunnelData.ToArray());
			}
			level.mWalls = new List<Wall>();
			if (this.mWalls != null)
			{
				level.mWalls.AddRange(this.mWalls.ToArray());
			}
			level.mFrogImages = (LillyPadImageInfo[])this.mFrogImages.Clone();
			level.mFrogX = (int[])this.mFrogX.Clone();
			level.mFrogY = (int[])this.mFrogY.Clone();
			return level;
		}

		public virtual void Dispose()
		{
			if (this.mCloakPoof != null)
			{
				this.mCloakPoof.Dispose();
				this.mCloakPoof = null;
			}
			if (this.mFrogFlyOff != null)
			{
				this.mFrogFlyOff.Dispose();
				this.mFrogFlyOff = null;
			}
			if (this.mTorchCompMgr != null)
			{
				this.mTorchCompMgr = null;
			}
			for (int i = 0; i < 4; i++)
			{
				if (this.mCurveMgr[i] != null)
				{
					this.mCurveMgr[i].Dispose();
					this.mCurveMgr[i] = null;
				}
			}
			for (int j = 0; j < 5; j++)
			{
				if (this.mFrogImages[j].mFilename.Length > 0)
				{
					if (this.mFrogImages[j].mImage != null)
					{
						this.mFrogImages[j].mImage.Dispose();
					}
				}
				else
				{
					this.mApp.mResourceManager.DeleteImage(this.mFrogImages[j].mResId);
				}
			}
			if (this.mHoleMgr != null)
			{
				this.mHoleMgr = null;
			}
			if (this.mOrgBoss != null && this.mOrgBoss.mResGroup.Length > 0 && !SexyFramework.Common.StrEquals(this.mOrgBoss.mResGroup, "Boss6Common") && GameApp.gApp.mResourceManager.IsGroupLoaded(this.mOrgBoss.mResGroup))
			{
				GameApp.gApp.mResourceManager.DeleteResources(this.mOrgBoss.mResGroup);
			}
			if (this.mSecondaryBoss != null && this.mSecondaryBoss.mResGroup.Length > 0 && !SexyFramework.Common.StrEquals(this.mOrgBoss.mResGroup, "Boss6Common") && GameApp.gApp.mResourceManager.IsGroupLoaded(this.mSecondaryBoss.mResGroup))
			{
				GameApp.gApp.mResourceManager.DeleteResources(this.mSecondaryBoss.mResGroup);
			}
			if (this.mBossBGID != "")
			{
				BaseRes baseRes = GameApp.gApp.mResourceManager.GetBaseRes(0, this.mBossBGID);
				string text = baseRes.mCompositeResGroup;
				if (text.Length == 0)
				{
					text = baseRes.mResGroup;
				}
				if (text.Length > 0 && GameApp.gApp.mResourceManager.IsGroupLoaded(text))
				{
					GameApp.gApp.mResourceManager.DeleteResources(text);
				}
			}
			if (this.mOrgBoss != null)
			{
				this.mOrgBoss.Dispose();
				this.mOrgBoss = null;
			}
			if (this.mSecondaryBoss != null)
			{
				this.mSecondaryBoss.Dispose();
				this.mSecondaryBoss = null;
			}
		}

		public virtual int GetNumCurves()
		{
			return this.mNumCurves;
		}

		public virtual int GetGunPointFromPos(int x, int y)
		{
			for (int i = 0; i < this.mNumFrogPoints; i++)
			{
				int num = x - this.mFrogX[i];
				int num2 = y - this.mFrogY[i];
				if (num * num + num2 * num2 < 3136)
				{
					return i;
				}
			}
			return -1;
		}

		public virtual void Preload()
		{
			if (this.mZone == 6 && (this.IsFinalBossLevel() || this.mEndSequence != -1) && this.IsFinalBossLevel())
			{
				this.mBossIntroBG = this.mApp.mResourceManager.GetResourceRef(0, "IMAGE_BOSS6_INTRO_BG").GetSharedImageRef();
				this.mBossBGID = "IMAGE_BOSS6_INTRO_BG";
			}
			if (this.mBoss != null && this.mZone != 6)
			{
				this.mBossIntroBG = this.mApp.mResourceManager.GetResourceRef(0, this.mBoss.mResPrefix + "INTRO_BG").GetSharedImageRef();
				this.mBossBGID = this.mBoss.mResPrefix + "INTRO_BG";
			}
			if (this.mBossBGID != "")
			{
				BaseRes baseRes = this.mApp.mResourceManager.GetBaseRes(0, this.mBossBGID);
				string text = baseRes.mCompositeResGroup;
				if (text != "")
				{
					text = baseRes.mResGroup;
				}
				if (text != "" && !this.mApp.mResourceManager.IsGroupLoaded(text))
				{
					this.mApp.mResourceManager.LoadResources(text);
					if (!this.mApp.mResourceManager.LoadResources(text))
					{
						this.mApp.ShowResourceError(true);
					}
				}
			}
		}

		public virtual void StartLevel(bool from_load, bool needs_reinit)
		{
			new Stopwatch("Level::StartLevel");
			this.Preload();
			if (this.mZone == 5 && !this.mApp.mResourceManager.IsGroupLoaded("GrottoSounds") && !this.mApp.mResourceManager.LoadResources("GrottoSounds"))
			{
				this.mApp.ShowResourceError(true);
				this.mApp.Shutdown();
				return;
			}
			if (this.mZone != 5 && this.mApp.mResourceManager.IsGroupLoaded("GrottoSounds"))
			{
				this.mApp.mResourceManager.DeleteResources("GrottoSounds");
			}
			else if (this.mZone != 6 && this.mApp.mResourceManager.IsGroupLoaded("Boss6Common"))
			{
				this.mApp.mResourceManager.DeleteResources("Boss6Common");
			}
			if (!needs_reinit)
			{
				new Stopwatch("Level::StartLevel::GetImage - FrogImages");
				for (int i = 0; i < 5; i++)
				{
					if (this.mFrogImages[i].mFilename.Length != 0)
					{
						string pathFrom = SexyFramework.Common.GetPathFrom(this.mFrogImages[i].mFilename, "");
						string idByPath = GameApp.gApp.mResourceManager.GetIdByPath(pathFrom);
						this.mFrogImages[i].mImage = (DeviceImage)this.mApp.mResourceManager.LoadImage(idByPath).GetImage();
						this.mFrogImages[i].mImage.mNumCols = 2;
					}
					else if (this.mFrogImages[i].mResId.Length != 0)
					{
						this.mFrogImages[i].mImage = (DeviceImage)this.mApp.mResourceManager.LoadImage(this.mFrogImages[i].mResId).GetImage();
						if (this.mFrogImages[i].mImage != null)
						{
							this.mFrogImages[i].mImage.mNumCols = 2;
						}
					}
				}
			}
			new Stopwatch("Level::StartLevel::LoadCurve");
			for (int j = 0; j < this.mNumCurves; j++)
			{
				if (!this.mCurveMgr[j].mIsLoaded && !this.mCurveMgr[j].LoadCurve())
				{
					this.mApp.Popup("Unable to load curve for " + this.mCurveMgr[j].GetPath());
				}
				if (this.mBoard.GauntletMode())
				{
					this.mCurveMgr[j].mCurveDesc.mVals.mNumColors = GameApp.gDDS.GetNumGauntletBalls(this.mNumCurves);
				}
				this.mCurveMgr[j].StartLevel(from_load);
				if (j == 0)
				{
					this.mCurveMgr[j].mInitialPathHilite = true;
				}
			}
			for (int k = 0; k < this.mHoleMgr.GetNumHoles(); k++)
			{
				int l = 0;
				while (l < 4)
				{
					if (this.mHoleMgr.GetHole(k).mCurveNum == l)
					{
						if (this.mCurveSkullAngleOverrides[l] < 3.40282347E+38f)
						{
							this.mHoleMgr.GetHole(k).mRotation = this.mCurveSkullAngleOverrides[l];
							break;
						}
						break;
					}
					else
					{
						l++;
					}
				}
			}
			ZumasRevenge.Common.gAddBalls = false;
			if (!needs_reinit)
			{
				this.mEffects.Clear();
				this.InitEffects();
				if (this.IsFinalBossLevel() && !this.mHasDoneTorchCrap && !this.mDoTorchCrap)
				{
					this.InitFinalBossLevel();
				}
				else if (this.mEndSequence == 3)
				{
					this.mBoard.mPreventBallAdvancement = false;
				}
				this.ResetEffects();
			}
			this.mPostZumaTimeCounter = this.mApp.GetLevelMgr().mPostZumaTime;
			this.mPostZumaTimeSlowInc = 0f;
			this.mPostZumaTimeSpeedInc = 0f;
			this.mTimer = this.mTimeToComplete;
			if (this.mBoss != null)
			{
				this.mBoss.Init(this);
				if (this.mSecondaryBoss != null)
				{
					this.mSecondaryBoss.Init(this);
				}
				if (!needs_reinit && this.mEndSequence == 2 && this.mBoard.GetGameState() == GameState.GameState_Playing)
				{
					this.mCloakClapFrame = -1;
					this.mCanDrawBoss = false;
					Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_ARMDOWN_REST);
					this.mTorchBossY = (float)(-(float)imageByID.mHeight - ZumasRevenge.Common._DS(ZumasRevenge.Common._M(100)));
					this.mCloakPoof = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_CLOAKTOLAMEEXPLOSION01").Duplicate();
					ZumasRevenge.Common.SetFXNumScale(this.mCloakPoof, GameApp.gApp.Is3DAccelerated() ? 1f : ZumasRevenge.Common._M(0.15f));
					for (int m = 0; m < 3; m++)
					{
						this.mCloakedBossTextAlpha[m] = 0f;
					}
				}
				if (GameApp.gDDS.HasBossParam("HurryAmt"))
				{
					this.mHurryToRolloutAmt = GameApp.gDDS.GetBossParam("HurryAmt");
				}
				this.mGingerMouthX = this.mGingerMouthXStart + 20f;
				this.mFredMouthX = this.mFredMouthXStart - 20f;
				this.mFredTongueX = 505f;
				this.mTargetBarSize = 330;
				this.mCurBarSize = 0;
			}
			else if (this.mTimeToComplete > 0)
			{
				this.mGingerMouthX = this.mGingerMouthXStart + 20f;
				this.mFredMouthX = this.mFredMouthXStart - 20f;
				this.mFredTongueX = 505f;
				this.mTargetBarSize = 330;
				this.mCurBarSize = 0;
			}
			if (this.IsFinalBossLevel())
			{
				if (!this.mApp.mResourceManager.IsGroupLoaded("Bosses") && !this.mApp.mResourceManager.LoadResources("Bosses"))
				{
					this.mApp.ShowResourceError(true);
					this.mApp.Shutdown();
					return;
				}
				this.mIntroTorchDelay = 0;
				this.mIntroTorchIndex = -1;
			}
			if (!needs_reinit)
			{
				for (int n = 0; n < this.mEffects.size<Effect>(); n++)
				{
					this.mEffects[n].LevelStarted(from_load);
				}
			}
			if (this.mBoard.GauntletMode())
			{
				this.mGauntletCurNumForMult = this.mApp.GetLevelMgr().mGauntletNumForMultBase;
			}
		}

		public virtual void StartLevel()
		{
			this.StartLevel(false, false);
		}

		public string GetCurvePath(int curve_num)
		{
			string text = "levels/";
			if (this.mCurveMgr[curve_num].mCurveDesc.mPath.IndexOf('/') != -1 || this.mCurveMgr[curve_num].mCurveDesc.mPath.IndexOf('\\') != -1)
			{
				text += this.mCurveMgr[curve_num].mCurveDesc.mPath;
			}
			else
			{
				text = text + this.mId + "/" + this.mCurveMgr[curve_num].mCurveDesc.mPath;
			}
			return text;
		}

		public bool CanDrawFrog()
		{
			return !this.IsFinalBossLevel() || this.mTorchStageState == 6 || this.mTorchStageState > 10;
		}

		public virtual void Reset(bool reset_effects)
		{
			this.mGauntletTimeRedAmt = 0f;
			this.mCurMultiplierTimeLeft = (this.mMaxMultiplierTime = 0);
			this.mGauntletCurNumForMult = 0;
			this.mGauntletCurTime = 0;
			this.mAllCurvesAtRolloutPoint = false;
			this.mHasReachedCruisingSpeed = false;
			this.mZumaBallPct = 0f;
			this.mZumaBallFrame = 0;
			this.mTargetBarSize = 0;
			this.mCurBarSize = 0;
			this.mBarLightness = 0f;
			this.mHaveReachedTarget = false;
			this.mNumGauntletBallsBroke = 0;
			this.mCurGauntletMultPct = 0f;
			this.mGauntletMultipliersEarned = 0;
			this.mGingerMouthX = this.mGingerMouthXStart;
			this.mFredMouthX = this.mFredMouthXStart;
			this.mGingerMouthVX = 0.5f;
			this.mFredMouthVX = 0f;
			this.mFredTongueX = 541f;
			this.mFredTongueVX = 0f;
			this.mZumaBallPct = 0f;
			this.mZumaBarState = -1;
			this.mFurthestBallDistance = 0;
			this.mGoldBallXOff = 0f;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].Reset();
			}
			if (this.mApp != null && reset_effects && ((this.mBoard != null && !this.mBoard.GauntletMode()) || (this.mBoard == null && this.mApp.mLoadingThreadStarted && !this.mApp.mLoadingThreadCompleted)))
			{
				for (int j = 0; j < Enumerable.Count<Effect>(this.mEffects); j++)
				{
					this.mEffects[j].NukeParams();
				}
				for (int k = 0; k < Enumerable.Count<EffectParams>(this.mEffectParams); k++)
				{
					this.mEffects[this.mEffectParams[k].mEffectIndex].SetParams(this.mEffectParams[k].mKey, this.mEffectParams[k].mValue);
				}
				for (int l = 0; l < Enumerable.Count<Effect>(this.mEffects); l++)
				{
					this.mEffects[l].Reset(this.mId);
				}
			}
		}

		public virtual void Reset()
		{
			this.Reset(true);
		}

		public virtual void ReInit()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].SetFarthestBall(0);
			}
			this.mPotPct = 1f;
			this.mCurBarSizeInc = 1;
			this.mInvertMouseTimer = (this.mMaxInvertMouseTimer = 0);
			this.mTimer = (this.mTimeToComplete = -1);
			this.mFurthestBallDistance = 0;
			this.mOffscreenClearBonus = false;
			this.mPostZumaTimeCounter = 0;
			this.mPostZumaTimeSlowInc = (this.mPostZumaTimeSpeedInc = 0f);
			this.mTempSpeedupTimer = 0;
			if (this.mOrgBoss != null)
			{
				int x = this.mBoss.GetX();
				int y = this.mBoss.GetY();
				Boss boss = this.mApp.GetLevelMgr().GetLevelById(this.mId).mBoss;
				Boss boss2 = boss.Instantiate();
				boss2.mName = this.mDisplayName;
				boss2.PostInstantiationHook(boss);
				boss2.mLevel = this;
				this.mOrgBoss = null;
				this.mBoss = boss2;
				this.mBoss.SetXY((float)x, (float)y);
				this.mOrgBoss = this.mBoss;
			}
			if (this.mSecondaryBoss != null)
			{
				Boss boss3 = this.mApp.GetLevelMgr().GetLevelById(this.mId).mSecondaryBoss;
				Boss boss2 = boss3.Instantiate();
				boss2.mName = this.mDisplayName;
				boss2.PostInstantiationHook(boss3);
				boss2.mLevel = this;
				this.mSecondaryBoss = null;
				this.mSecondaryBoss = boss2;
			}
			for (int j = 0; j < this.mTorches.size<Torch>(); j++)
			{
				if (this.mTorches[j].mFlame != null)
				{
					this.mTorches[j].mFlame.ResetAnim();
					this.mTorches[j].mFlame.mEmitAfterTimeline = true;
				}
				this.mTorches[j].mActive = true;
				this.mTorches[j].mDraw = true;
				this.mTorches[j].mWasHit = false;
			}
			this.Reset(false);
			for (int k = 0; k < this.mNumCurves; k++)
			{
				this.mCurveMgr[k].Reset();
			}
		}

		public virtual void AfterBoardAdded()
		{
		}

		public virtual bool CollidedWithWall(Bullet b)
		{
			float num = (float)b.GetRadius() * ZumasRevenge.Common._M(0.75f);
			FRect theTRect = new FRect(b.GetX() - num, b.GetY() - num, num * 2f, num * 2f);
			for (int i = 0; i < this.mWalls.size<Wall>(); i++)
			{
				Wall wall = this.mWalls[i];
				if (wall.mStrength != 0 && wall.mType != 0)
				{
					int num2 = ((wall.mImage == null) ? ((int)wall.mWidth) : wall.mImage.GetCelWidth());
					int num3 = ((wall.mImage == null) ? ((int)wall.mHeight) : wall.mImage.GetCelHeight());
					int num4 = ((wall.mImage == null) ? 0 : (num2 / 2));
					int num5 = ((wall.mImage == null) ? 0 : (num3 / 2));
					FRect frect = new FRect(wall.mX - (float)num4, wall.mY - (float)num5, (float)num2, (float)num3);
					if (frect.Intersects(theTRect))
					{
						if (wall.mStrength > 0)
						{
							wall.mStrength--;
						}
						if (wall.mStrength == 0)
						{
							wall.mCurRespawnTimer = 0;
						}
						frect.Inflate(theTRect.mWidth / 2f, theTRect.mHeight / 2f);
						FPoint a = new FPoint(b.GetX() + b.mVelX, b.GetY() + b.mVelY);
						FPoint a2 = new FPoint(b.GetX() - b.mVelX, b.GetY() - b.mVelY);
						float angle;
						if (ZumasRevenge.Common.LinesIntersect(a, a2, new FPoint(frect.mX, frect.mY), new FPoint(frect.mX + frect.mWidth, frect.mY)))
						{
							angle = 90f;
						}
						else if (ZumasRevenge.Common.LinesIntersect(a, a2, new FPoint(frect.mX, frect.mY + frect.mHeight), new FPoint(frect.mX + frect.mWidth, frect.mY + frect.mHeight)))
						{
							angle = 270f;
						}
						else if (ZumasRevenge.Common.LinesIntersect(a, a2, new FPoint(frect.mX, frect.mY), new FPoint(frect.mX, frect.mY + frect.mHeight)))
						{
							angle = 180f;
						}
						else
						{
							if (!ZumasRevenge.Common.LinesIntersect(a, a2, new FPoint(frect.mX + frect.mWidth, frect.mY), new FPoint(frect.mX + frect.mWidth, frect.mY + frect.mHeight)))
							{
								return true;
							}
							angle = 0f;
						}
						this.mBoard.AddBallExplosionParticleEffect(b, angle, 180f);
						GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_WALLBALL));
						return true;
					}
				}
			}
			Rect r = new Rect((int)theTRect.mX, (int)theTRect.mY, (int)theTRect.mWidth, (int)theTRect.mHeight);
			for (int j = 0; j < this.mTorches.size<Torch>(); j++)
			{
				Torch torch = this.mTorches[j];
				torch.CheckCollision(r);
			}
			return false;
		}

		public virtual void CopyEffectsFrom(Level l)
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				for (int j = 0; j < l.mEffects.size<Effect>(); j++)
				{
					if (ZumasRevenge.Common.StrEquals(l.mEffects[j].GetName(), this.mEffects[i].GetName()))
					{
						this.mEffects[i].CopyFrom(l.mEffects[j]);
						break;
					}
				}
			}
		}

		public virtual string GetStatsScreenText(GameStats stats, int score)
		{
			return "";
		}

		public void AddTorch(int x, int y, int w, int h)
		{
			Torch torch = new Torch();
			torch.mX = x;
			torch.mY = y;
			torch.mWidth = w;
			torch.mHeight = h;
			this.mTorches.Add(torch);
		}

		public bool PointIntersectsWall(float x, float y)
		{
			if (Enumerable.Count<Wall>(this.mWalls) == 0)
			{
				return false;
			}
			for (int i = 0; i < Enumerable.Count<Wall>(this.mWalls); i++)
			{
				Wall wall = this.mWalls[i];
				Rect rect = new Rect((int)wall.mX, (int)wall.mY, (int)wall.mWidth, (int)wall.mHeight);
				if (wall.mStrength != 0 && rect.Contains((int)x, (int)y))
				{
					return true;
				}
			}
			return false;
		}

		public void DrawDaisRocks(Graphics g)
		{
			for (int i = 0; i < this.mDaisRocks.size<DaisRock>(); i++)
			{
				DaisRock daisRock = this.mDaisRocks[i];
				g.SetColorizeImages(true);
				g.SetColor(255, 255, 255, (int)daisRock.mAlpha);
				this.mGlobalTranform.Reset();
				this.mGlobalTranform.Scale(daisRock.mSize, daisRock.mSize);
				float rot = (255f - daisRock.mAlpha) / 255f * ZumasRevenge.Common._M(2.5f) * 3.14159274f;
				this.mGlobalTranform.RotateRad(rot);
				g.DrawImageTransform(daisRock.mImg, this.mGlobalTranform, daisRock.mX, daisRock.mY);
			}
		}

		public void FadeInkSpots()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].QuicklyFadeInkSpots();
			}
		}

		public void MultiplierActivated()
		{
			this.mGauntletCurNumForMult += this.mApp.GetLevelMgr().mGauntletNumForMultInc;
			if (this.mGauntletCurNumForMult > this.mApp.GetLevelMgr().mMaxGauntletNumForMult)
			{
				this.mGauntletCurNumForMult = this.mApp.GetLevelMgr().mMaxGauntletNumForMult;
			}
			int mMultiplierDuration = this.mApp.GetLevelMgr().mMultiplierDuration;
			if (this.mCurMultiplierTimeLeft == 0)
			{
				this.mCurMultiplierTimeLeft = (this.mMaxMultiplierTime = mMultiplierDuration);
			}
			else
			{
				this.mCurMultiplierTimeLeft += mMultiplierDuration;
				this.mMaxMultiplierTime = this.mCurMultiplierTimeLeft;
			}
			if (GameApp.gDDS.AddMultiplierTime(this.mApp.GetLevelMgr().mMultiplierTimeAdd))
			{
				this.UpdateChallengeModeDifficulty();
			}
		}

		public void UpdateChallengeModeDifficulty()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].mCurveDesc.mVals.mNumColors = GameApp.gDDS.GetNumGauntletBalls(this.mNumCurves);
			}
		}

		public virtual void SkipInitialPathHilite()
		{
			bool flag = false;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (Enumerable.Count<PathSparkle>(this.mCurveMgr[i].mSparkles) > 0 || this.mCurveMgr[i].mInitialPathHilite)
				{
					flag = true;
					this.mCurveMgr[i].mSparkles.Clear();
					this.mCurveMgr[i].mInitialPathHilite = false;
				}
			}
			if (flag)
			{
				ZumasRevenge.Common.gAddBalls = true;
			}
		}

		public virtual bool DoingInitialPathHilite()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (this.mCurveMgr[i].mInitialPathHilite)
				{
					return true;
				}
			}
			return false;
		}

		public virtual void SwitchToSecondaryBoss()
		{
			int x = this.mBoss.GetX();
			int y = this.mBoss.GetY();
			float hp = this.mBoss.GetHP();
			this.mBoss = this.mSecondaryBoss;
			this.mBoss.SetHP(hp);
			this.mBoss.SetXY((float)x, (float)y);
		}

		public virtual void Update(float f)
		{
			this.mUpdateCount++;
			if (this.mTimer > 0)
			{
				this.mTimer--;
			}
			if (this.mInvertMouseTimer > 0 && --this.mInvertMouseTimer == 0)
			{
				GameApp.gApp.GetBoard().UpdateGunPos();
			}
			if (this.mTorchStageState == 10 || this.mTorchStageState == 11 || (this.mTorchStageState == 9 && this.mBoard.mFullScreenAlphaRate < 0))
			{
				if (this.mTorchStageState != 11 && this.mUpdateCount % ZumasRevenge.Common._M(2) == 0)
				{
					List<Image> list = new List<Image>();
					for (int i = 0; i < 3; i++)
					{
						list.Add(Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_ROCKFALL_PEBBLE1 + i));
						list.Add(Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_ROCKFALL_SPECK1 + i));
					}
					this.mDaisRocks.Add(new DaisRock());
					DaisRock daisRock = this.mDaisRocks.back<DaisRock>();
					daisRock.mImg = list[SexyFramework.Common.Rand(list.size<Image>())];
					daisRock.mX = (float)ZumasRevenge.Common._DS(MathUtils.IntRange(ZumasRevenge.Common._M(400), ZumasRevenge.Common._M1(1200)));
					daisRock.mY = (float)(-(float)daisRock.mImg.mHeight / 2);
				}
				for (int j = 0; j < this.mDaisRocks.size<DaisRock>(); j++)
				{
					DaisRock daisRock2 = this.mDaisRocks[j];
					daisRock2.mY += ZumasRevenge.Common._M(15f);
					daisRock2.mSize -= ZumasRevenge.Common._M(0.002f);
					daisRock2.mAlpha -= ZumasRevenge.Common._M(0.1f);
					if (daisRock2.mSize <= 0f || daisRock2.mAlpha <= 0f)
					{
						this.mDaisRocks.RemoveAt(j);
						j--;
					}
				}
			}
			if ((this.IsFinalBossLevel() && this.mTorchStageState != 6) || this.mTorchStageState >= 10)
			{
				string[] array = new string[] { "start", "squish", "rattle" };
				Composition composition = null;
				int num;
				switch (this.mTorchStageState)
				{
				case 0:
					num = 0;
					break;
				case 1:
					num = 1;
					break;
				default:
					num = 2;
					break;
				}
				if (this.mTorchStageState < 6)
				{
					composition = this.mTorchCompMgr.GetComposition(array[num]);
				}
				float num2 = ZumasRevenge.Common._M(0.97f);
				int num3 = ZumasRevenge.Common._M(15);
				if (this.mTorchStageState == 0)
				{
					if (--this.mTorchStageTimer <= 0)
					{
						composition.Update();
						this.mTorchBossX += this.mTorchBossVX;
						this.mTorchBossY += this.mTorchBossVY;
						if (this.mTorchBossX >= this.mTorchBossDestX)
						{
							this.mTorchBossX = this.mTorchBossDestX;
							this.mTorchBossVX = 0f;
						}
						if (this.mTorchBossY >= this.mTorchBossDestY)
						{
							this.mTorchBossY = this.mTorchBossDestY;
							this.mTorchBossVY = 0f;
						}
						if (this.mTorchBossVX == 0f && this.mTorchBossVY == 0f)
						{
							this.mTorchStageState = 1;
							this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_CLOAKED_DAIS_LANDING));
						}
					}
				}
				else if (this.mTorchStageState == 1)
				{
					composition.Update();
					if (composition.mUpdateCount == num3)
					{
						this.mTorchDaisScale = num2;
					}
					float num4 = (1f - num2) / (float)(composition.GetMaxDuration() - num3);
					this.mTorchDaisScale += num4;
					if (this.mTorchDaisScale > 1f)
					{
						this.mTorchDaisScale = 1f;
					}
					if (composition.mUpdateCount >= composition.GetMaxDuration() && SexyFramework.Common._eq(this.mTorchDaisScale, 1f))
					{
						this.mTorchStageState = 2;
						this.mTorchStageTimer = ZumasRevenge.Common._M(100);
					}
				}
				else if (this.mTorchStageState == 2)
				{
					int num5 = ZumasRevenge.Common._M(100);
					if (this.mTorchStageTimer > 0)
					{
						this.mTorchStageTimer--;
					}
					if (this.mTorchStageTimer == 0 && this.mEggs.size<TorchLevelEgg>() < 4)
					{
						composition.Update();
					}
					float[] array2 = new float[] { 38f, 38f, 1421f, 1423f };
					float[] array3 = new float[] { 82f, 952f, 85f, 949f };
					if (composition.mUpdateCount >= composition.GetMaxDuration() && this.mTorchStageTimer <= 0 && this.mEggs.size<TorchLevelEgg>() < 4)
					{
						this.mTorchStageTimer = num5;
						composition.Reset();
						this.mEggs.Add(new TorchLevelEgg());
						TorchLevelEgg torchLevelEgg = this.mEggs.back<TorchLevelEgg>();
						torchLevelEgg.mX = (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(545));
						torchLevelEgg.mY = (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(208));
						torchLevelEgg.mAlpha = 0f;
						torchLevelEgg.mDestX = ZumasRevenge.Common._DS(array2[this.mEggs.Count - 1]);
						torchLevelEgg.mDestY = ZumasRevenge.Common._DS(array3[this.mEggs.Count - 1] + (float)ZumasRevenge.Common._M(60));
						float num6 = ZumasRevenge.Common._M(60f);
						torchLevelEgg.mVX = (torchLevelEgg.mDestX - torchLevelEgg.mX) / num6;
						torchLevelEgg.mVY = (torchLevelEgg.mDestY - torchLevelEgg.mY) / num6;
						torchLevelEgg.mDestAngle = 3.14159274f * ZumasRevenge.Common._M(3f);
						if (torchLevelEgg.mDestX > torchLevelEgg.mX)
						{
							torchLevelEgg.mDestAngle *= -1f;
						}
						torchLevelEgg.mAngleInc = torchLevelEgg.mDestAngle / num6;
					}
					for (int k = 0; k < this.mEggs.size<TorchLevelEgg>(); k++)
					{
						TorchLevelEgg torchLevelEgg2 = this.mEggs[k];
						if (torchLevelEgg2.mAlpha < 255f && (torchLevelEgg2.mVX != 0f || torchLevelEgg2.mVY != 0f))
						{
							torchLevelEgg2.mAlpha += (float)ZumasRevenge.Common._M(8);
							if (torchLevelEgg2.mAlpha > 255f)
							{
								torchLevelEgg2.mAlpha = 255f;
							}
						}
						torchLevelEgg2.mX += torchLevelEgg2.mVX;
						torchLevelEgg2.mY += torchLevelEgg2.mVY;
						int num7 = 0;
						if ((torchLevelEgg2.mVX < 0f && torchLevelEgg2.mX <= torchLevelEgg2.mDestX) || (torchLevelEgg2.mVX > 0f && torchLevelEgg2.mX >= torchLevelEgg2.mDestX))
						{
							num7++;
						}
						if ((torchLevelEgg2.mVY < 0f && torchLevelEgg2.mY <= torchLevelEgg2.mDestY) || (torchLevelEgg2.mVY > 0f && torchLevelEgg2.mY >= torchLevelEgg2.mDestY))
						{
							num7++;
						}
						if (num7 == 2)
						{
							if (this.mTorches[k].mActive)
							{
								this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_TORCH_EXTINGUISHED));
							}
							this.mTorches[k].mActive = false;
						}
						torchLevelEgg2.mAngle += torchLevelEgg2.mAngleInc;
					}
					Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSSES_EGG);
					if (this.mEggs.size<TorchLevelEgg>() == 4 && !new Rect(-80, 0, this.mApp.mWidth + ZumasRevenge.Common._S(160), this.mApp.mHeight).Intersects(new Rect((int)Enumerable.Last<TorchLevelEgg>(this.mEggs).mX, (int)Enumerable.Last<TorchLevelEgg>(this.mEggs).mY, imageByID.mWidth, imageByID.mHeight)))
					{
						this.mTorchStageState = 3;
						this.mTorchStageTimer = ZumasRevenge.Common._M(50);
					}
				}
				else if (this.mTorchStageState == 7 || (this.mTorchStageState == 9 && this.mBoard.mFullScreenAlphaRate < 0))
				{
					this.mBoard.UpdatePlayingFX();
					List<Image> list2 = new List<Image>();
					for (int l = 0; l < 3; l++)
					{
						list2.Add(Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_ROCKFALL_PEBBLE1 + l));
						list2.Add(Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_ROCKFALL_SPECK1 + l));
					}
					this.mTorchStageAlpha += ZumasRevenge.Common._M(1.5f);
					this.mTorchStageShakeAmt = SexyFramework.Common.Rand(ZumasRevenge.Common._M(5));
					if (this.mUpdateCount % ZumasRevenge.Common._M(10) == 0)
					{
						Image imageByID2 = Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_DIAS);
						this.mDaisRocks.Add(new DaisRock());
						DaisRock daisRock3 = this.mDaisRocks.back<DaisRock>();
						float num8 = (float)(ZumasRevenge.Common._DS(660) + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(30)));
						float num9 = num8 + (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(40));
						float num10 = num8 + (float)imageByID2.mWidth - (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(100));
						float num11 = num10 + (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(35));
						float mY = (float)(ZumasRevenge.Common._DS(417) + imageByID2.mHeight - ZumasRevenge.Common._DS(ZumasRevenge.Common._M(100)));
						daisRock3.mImg = list2[SexyFramework.Common.Rand(list2.size<Image>())];
						float num12 = (float)(SexyFramework.Common.IntRange((int)num8, (int)num9) - daisRock3.mImg.mWidth / 2);
						float num13 = (float)(SexyFramework.Common.IntRange((int)num10, (int)num11) + daisRock3.mImg.mWidth / 2);
						daisRock3.mX = ((SexyFramework.Common.Rand(2) == 0) ? num12 : num13);
						daisRock3.mY = mY;
					}
					if (this.mUpdateCount % ZumasRevenge.Common._M(50) == 0)
					{
						this.mApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_RUMBLE));
						if (++Level.last_sound_idx >= 2)
						{
							Level.last_sound_idx = 0;
						}
					}
					for (int m = 0; m < this.mDaisRocks.size<DaisRock>(); m++)
					{
						DaisRock daisRock4 = this.mDaisRocks[m];
						daisRock4.mY += ZumasRevenge.Common._M(1f);
						daisRock4.mSize -= ZumasRevenge.Common._M(0.02f);
						daisRock4.mAlpha -= ZumasRevenge.Common._M(1f);
						if (daisRock4.mSize <= 0f || daisRock4.mAlpha <= 0f)
						{
							this.mDaisRocks.RemoveAt(m);
							m--;
						}
					}
					if (this.mTorchStageAlpha >= 255f && --this.mTorchStageTimer <= 0)
					{
						this.mTorchStageAlpha = 255f;
						this.mTorchStageState = 8;
						this.mTorchStageShakeAmt = 0;
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_LOWERING));
						this.mApp.SetCursor(ECURSOR.CURSOR_POINTER);
					}
				}
				else if (this.mTorchStageState == 3 || this.mTorchStageState == 8)
				{
					for (int n = 0; n < this.mDaisRocks.size<DaisRock>(); n++)
					{
						DaisRock daisRock5 = this.mDaisRocks[n];
						daisRock5.mY += ZumasRevenge.Common._M(1f);
						daisRock5.mSize -= ZumasRevenge.Common._M(0.02f);
						daisRock5.mAlpha -= ZumasRevenge.Common._M(1f);
						if (daisRock5.mSize <= 0f || daisRock5.mAlpha <= 0f)
						{
							this.mDaisRocks.RemoveAt(n);
							n--;
						}
					}
					if (this.mTorchStageState == 8 && this.mUpdateCount % ZumasRevenge.Common._M(250) == 0)
					{
						this.mApp.mSoundPlayer.Loop(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_RUMBLE));
					}
					if (this.mTorchStageTimer == 1 && this.mTorchStageState == 3)
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_LOWERING));
					}
					if (--this.mTorchStageTimer <= 0)
					{
						this.mTorchDaisScale -= ZumasRevenge.Common._M(0.01f);
						if (this.mTorchDaisScale <= 0f)
						{
							this.mTorchDaisScale = 0f;
							if (this.mTorchStageState == 3)
							{
								this.mApp.mSoundPlayer.Stop(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_RUMBLE));
								this.mTorchStageState = 4;
								this.mTorchStageTimer = ZumasRevenge.Common._M(150);
							}
							else
							{
								this.mTorchStageState = 9;
							}
						}
					}
				}
				else if (this.mTorchStageState == 4)
				{
					if (--this.mTorchStageTimer <= 0)
					{
						if (this.mTorchStageTimer == 0)
						{
							for (int num14 = 0; num14 < this.mTorches.size<Torch>(); num14++)
							{
								this.mTorches[num14].mFlame.ResetAnim();
								this.mTorches[num14].mActive = true;
								this.mTorches[num14].mDraw = true;
							}
						}
						this.mTorchDaisScale += ZumasRevenge.Common._M(0.02f);
						if (this.mTorchDaisScale >= 1f)
						{
							this.mTorchDaisScale = 1f;
							if (this.mFrogFlyOff != null)
							{
								this.mFrogFlyOff.Dispose();
								this.mFrogFlyOff = null;
							}
							this.mFrogFlyOff = new FrogFlyOff();
							this.mFrogFlyOff.JumpIn(this.mFrog, this.mFrog.GetCenterX(), this.mFrog.GetCenterY(), false);
							this.mTorchStageState = 5;
						}
					}
				}
				else if (this.mTorchStageState == 10)
				{
					this.mFrogFlyOff.Update();
					if (this.mFrogFlyOff.mTimer >= this.mFrogFlyOff.mFrogJumpTime)
					{
						this.mApp.mSoundPlayer.Stop(Res.GetSoundByID(ResID.SOUND_NEW_DAIS_RUMBLE));
						this.mTorchStageState = 11;
						this.mTorchStageTimer = ZumasRevenge.Common._M(100);
						this.mFrog.SetPos((int)this.mFrogFlyOff.mFrogX, this.mFrog.GetCurY());
						this.mFrogFlyOff.Dispose();
						this.mFrogFlyOff = null;
					}
				}
				else if (this.mTorchStageState == 5)
				{
					this.mFrogFlyOff.Update();
					if (this.mFrogFlyOff.mTimer > this.mFrogFlyOff.mFrogJumpTime)
					{
						this.mFrog.SetAngle((float)((int)this.mFrogFlyOff.mFrogAngle));
						this.mFrogFlyOff.Dispose();
						this.mFrogFlyOff = null;
						this.mTorchStageState = 6;
						this.mBoard.mPreventBallAdvancement = false;
						this.mDoTorchCrap = false;
						this.mHasDoneTorchCrap = true;
					}
				}
				else if (this.mTorchStageState == 11)
				{
					if (--this.mTorchStageTimer <= 0 && (this.mTorchBossY += (float)ZumasRevenge.Common._M(10)) >= (float)ZumasRevenge.Common._M1(0))
					{
						this.mTorchStageState = 12;
						this.mTorchStageTimer = 0;
					}
				}
				else if (this.mTorchStageState == 12)
				{
					int num15 = ZumasRevenge.Common._M(500);
					this.mTorchStageTimer++;
					for (int num16 = 0; num16 < 3; num16++)
					{
						if (this.mTorchStageTimer >= num15)
						{
							this.mCloakedBossTextAlpha[num16] -= ZumasRevenge.Common._M(2f);
							if (this.mCloakedBossTextAlpha[num16] < 0f)
							{
								this.mCloakedBossTextAlpha[num16] = 0f;
							}
						}
						else
						{
							this.mCloakedBossTextAlpha[num16] += ZumasRevenge.Common._M(2f);
							if (this.mCloakedBossTextAlpha[num16] > 255f)
							{
								this.mCloakedBossTextAlpha[num16] = 255f;
							}
							else if (this.mCloakedBossTextAlpha[num16] < (float)ZumasRevenge.Common._M(128))
							{
								break;
							}
						}
					}
					Image imageByID3 = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_CLAP);
					int num17 = ZumasRevenge.Common._M(6);
					int num18 = imageByID3.mNumRows * imageByID3.mNumCols;
					if (this.mTorchStageTimer >= num15 && this.mTorchStageTimer % num17 == 0)
					{
						this.mCloakClapFrame++;
						if (this.mCloakClapFrame == ZumasRevenge.Common._M(5))
						{
							this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_CLOAKED_CLAP));
						}
					}
					if (this.mTorchStageTimer >= num15 + ZumasRevenge.Common._M(15))
					{
						this.mCloakPoof.mDrawTransform.LoadIdentity();
						float num19 = GameApp.DownScaleNum(1f);
						this.mCloakPoof.mDrawTransform.Scale(num19, num19);
						this.mCloakPoof.mDrawTransform.Translate((float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(812)), (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(220)));
						this.mCloakPoof.Update();
						if (SexyFramework.Common._eq(this.mCloakPoof.mFrameNum, (float)ZumasRevenge.Common._M(135), 0.5f))
						{
							this.mCanDrawBoss = true;
						}
						else if (this.mCloakPoof.mFrameNum >= (float)this.mCloakPoof.mLastFrameNum)
						{
							this.mBoard.mContinueNextLevelOnLoadProfile = false;
							this.mTorchStageState = 13;
							this.mBoard.mHasDoneIntroSounds = false;
							if (this.mApp.mResourceManager.IsGroupLoaded("CloakedBoss"))
							{
								this.mApp.mResourceManager.DeleteResources("CloakedBoss");
							}
						}
					}
				}
			}
			if (this.mTorchStageState > 4 && this.mBoard.GetGameState() != GameState.GameState_BossIntro && this.mTorchStageState != 12 && this.mTorchTextAlpha > 0f)
			{
				this.mTorchTextAlpha -= ZumasRevenge.Common._M(1.3f);
				if (this.mTorchTextAlpha < 0f)
				{
					this.mTorchTextAlpha = 0f;
				}
			}
			this.UpdateEffects();
			for (int num20 = 0; num20 < this.mWalls.size<Wall>(); num20++)
			{
				Wall wall = this.mWalls[num20];
				wall.Update();
				if ((wall.mVX > 0f && wall.mX > (float)ZumasRevenge.Common._SS(this.mApp.mWidth)) || (wall.mVX < 0f && wall.mX + wall.mWidth < 0f) || (wall.mVY > 0f && wall.mY > (float)ZumasRevenge.Common._SS(this.mApp.mHeight)) || (wall.mVY < 0f && wall.mY + wall.mHeight < 0f))
				{
					this.mWalls.RemoveAt(num20);
					num20--;
				}
			}
			for (int num21 = 0; num21 < this.mMovingWallDefaults.size<Wall>(); num21++)
			{
				Wall wall2 = this.mMovingWallDefaults[num21];
				int num22 = int.MaxValue;
				bool flag = false;
				for (int num23 = 0; num23 < this.mWalls.size<Wall>(); num23++)
				{
					Wall wall3 = this.mWalls[num23];
					if (wall3.mId == wall2.mId)
					{
						flag = true;
						int num24;
						if (wall3.mVX > 0f)
						{
							num24 = (int)((wall3.mX < 0f) ? 0f : (wall3.mX - wall2.mX));
						}
						else
						{
							num24 = (int)((wall3.mX + wall3.mWidth > wall2.mX) ? 0f : (wall2.mX - (wall3.mX + wall3.mWidth)));
						}
						int num25;
						if (wall3.mVY > 0f)
						{
							num25 = (int)((wall3.mY < 0f) ? 0f : (wall3.mY - wall2.mY));
						}
						else
						{
							num25 = (int)((wall3.mY + wall3.mHeight > wall2.mY) ? 0f : (wall2.mY - (wall3.mY + wall3.mHeight)));
						}
						int num26 = num24 * num24 + num25 * num25;
						if (num26 < num22)
						{
							num22 = num26;
						}
					}
				}
				if (num22 > wall2.mSpacing || !flag)
				{
					this.mWalls.Add(wall2);
					this.mWalls.back<Wall>().mCurLifeTimer = MathUtils.IntRange(wall2.mMinLifeTimer, wall2.mMaxLifeTimer);
				}
			}
			this.mHoleMgr.Update();
			if (this.mBoss != null)
			{
				this.mBoss.Update(f);
			}
		}

		public virtual void Draw(Graphics g)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].DrawUnderBalls(g);
			}
			for (int j = 0; j < this.mTorches.size<Torch>(); j++)
			{
				this.mTorches[j].Draw(g);
			}
			for (int k = 0; k < this.mEffects.size<Effect>(); k++)
			{
				this.mEffects[k].DrawUnderBalls(g);
			}
			if (this.mBoss != null && this.mCanDrawBoss)
			{
				this.mBoss.DrawBelowBalls(g);
			}
		}

		public virtual void DrawBottomLevel(Graphics g)
		{
			if (this.mBoss != null && this.mCanDrawBoss)
			{
				this.mBoss.DrawBottomLevel(g);
			}
		}

		public virtual void DrawToplevel(Graphics g)
		{
			if (this.mBoss != null && this.mCanDrawBoss)
			{
				this.mBoss.DrawTopLevel(g);
			}
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].DrawTopLevel(g);
			}
			if (this.mTorchTextAlpha > 0f && this.mBoard.GetGameState() != GameState.GameState_BossIntro && this.mTorchStageState > 5)
			{
				int centerX = this.mBoard.GetGun().GetCenterX();
				int centerY = this.mBoard.GetGun().GetCenterY();
				string theString = (this.mBoard.IsHardAdventureMode() ? TextManager.getInstance().getString(495) : TextManager.getInstance().getString(496));
				string theString2 = (this.mBoard.IsHardAdventureMode() ? TextManager.getInstance().getString(497) : TextManager.getInstance().getString(498));
				g.SetFont(Res.GetFontByID(ResID.FONT_BOSS_TAUNT));
				int num = (int)this.mTorchTextAlpha - 350;
				if (num > 0)
				{
					g.SetColor(0, 0, 0, (num > 255) ? 255 : num);
					g.DrawString(theString, ZumasRevenge.Common._S(centerX) - g.GetFont().StringWidth(theString) / 2, ZumasRevenge.Common._S(centerY - ZumasRevenge.Common._M(90)));
				}
				g.SetColor(0, 0, 0, ((int)this.mTorchTextAlpha > 255) ? 255 : ((int)this.mTorchTextAlpha));
				g.DrawString(theString2, ZumasRevenge.Common._S(centerX) - g.GetFont().StringWidth(theString2) / 2, ZumasRevenge.Common._S(centerY + ZumasRevenge.Common._M(120)));
			}
			for (int j = 0; j < this.mPowerupRegions.size<PowerupRegion>(); j++)
			{
				PowerupRegion powerupRegion = this.mPowerupRegions[j];
				if (powerupRegion.mDebugDraw)
				{
					g.SetColor(255, 0, 0);
					int numPoints = this.mCurveMgr[powerupRegion.mCurveNum].mWayPointMgr.GetNumPoints();
					float num2;
					float num3;
					this.mCurveMgr[powerupRegion.mCurveNum].GetXYFromWaypoint((int)(powerupRegion.mCurvePctStart * (float)numPoints), out num2, out num3);
					float num4;
					float num5;
					this.mCurveMgr[powerupRegion.mCurveNum].GetXYFromWaypoint((int)(powerupRegion.mCurvePctEnd * (float)numPoints), out num4, out num5);
					g.FillRect(ZumasRevenge.Common._S((int)num2) - 2, ZumasRevenge.Common._S((int)num3) - 2, 4, 4);
					g.SetColor(0, 255, 0);
					g.FillRect(ZumasRevenge.Common._S((int)num4) - 2, ZumasRevenge.Common._S((int)num5) - 2, 4, 4);
				}
			}
			if (this.mTorchStageState >= 11)
			{
				if (this.mTorchStageTimer > 0 || (this.mCloakPoof != null && this.mCloakPoof.mFrameNum < (float)ZumasRevenge.Common._M(135)))
				{
					int num6 = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(32));
					if (this.mTorchStageTimer < ZumasRevenge.Common._M(570))
					{
						g.SetColorizeImages(true);
						g.SetColor(255, 255, 255, 128);
						g.SetColorizeImages(false);
					}
					Image imageByID = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_ARMDOWN_REST);
					Image imageByID2 = Res.GetImageByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_CLAP);
					if (this.mCloakClapFrame < 0)
					{
						g.DrawImage(imageByID, ZumasRevenge.Common._S(this.mBoss.GetX()) - imageByID.mWidth / 2 + ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_ARMDOWN_REST)) - num6, ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_BOSS_LAME_CLOAKEDBOSS_ARMDOWN_REST)) + ZumasRevenge.Common._S(this.mBoss.GetY()) + (int)this.mTorchBossY - imageByID.mHeight / 2);
					}
					else if (this.mTorchStageTimer < ZumasRevenge.Common._M(570))
					{
						int theCel = Math.Min(this.mCloakClapFrame, imageByID2.mNumRows * imageByID2.mNumCols - 1);
						g.DrawImageCel(imageByID2, ZumasRevenge.Common._S(this.mBoss.GetX()) - imageByID2.GetCelWidth() / 2 - num6, ZumasRevenge.Common._S(this.mBoss.GetY()) - imageByID2.GetCelHeight() / 2 + (int)this.mTorchBossY, theCel);
					}
					g.SetFont(Res.GetFontByID(ResID.FONT_BOSS_TAUNT));
					bool flag = this.mBoard.IsHardAdventureMode();
					if (this.mTorchStageState == 12)
					{
						string[] array = new string[]
						{
							TextManager.getInstance().getString(490),
							TextManager.getInstance().getString(491),
							TextManager.getInstance().getString(492)
						};
						if (flag)
						{
							array[0] = TextManager.getInstance().getString(493);
							array[1] = TextManager.getInstance().getString(494);
							array[2] = "";
						}
						for (int k = 0; k < array.Length; k++)
						{
							if (this.mCloakedBossTextAlpha[k] > 0f)
							{
								g.SetColor(0, 0, 0, (int)this.mCloakedBossTextAlpha[k]);
								g.WriteString(array[k].ToString(), -GameApp.gApp.mBoardOffsetX, ZumasRevenge.Common._DS(ZumasRevenge.Common._M(550)) + k * g.GetFont().GetHeight(), 1024);
							}
						}
					}
				}
				if (this.mTorchStageState == 12 && this.mCloakPoof.mFrameNum < (float)this.mCloakPoof.mLastFrameNum && this.mCloakPoof.mFrameNum > 0f)
				{
					this.mCloakPoof.Draw(g);
				}
			}
		}

		public virtual void DrawAboveBalls(Graphics g)
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].DrawAboveBalls(g);
			}
		}

		public virtual void DrawUnderBackground(Graphics g)
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].DrawUnderBackground(g);
			}
		}

		public virtual void DrawFullScene(Graphics g)
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].DrawFullScene(g);
			}
		}

		public virtual void DrawFullSceneNoFrog(Graphics g)
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].DrawFullSceneNoFrog(g);
			}
		}

		public virtual void DrawPriority(Graphics g, int priority)
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].DrawPriority(g, priority);
			}
		}

		public virtual void DrawTorchLighting(Graphics g)
		{
			if (this.mTorches.size<Torch>() == 0)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_QUADRANT);
			float[] array = new float[] { 1f, 1f, -1f, -1f };
			float[] array2 = new float[] { 1f, -1f, 1f, -1f };
			int[] array3 = new int[]
			{
				ZumasRevenge.Common._DS(-160),
				ZumasRevenge.Common._DS(-160),
				this.mApp.mWidth + ZumasRevenge.Common._DS(320) - imageByID.mWidth,
				this.mApp.mWidth + ZumasRevenge.Common._DS(320) - imageByID.mWidth
			};
			int[] array4 = new int[]
			{
				default(int),
				this.mApp.mHeight - imageByID.mHeight,
				default(int),
				this.mApp.mHeight - imageByID.mHeight
			};
			for (int i = 0; i < this.mTorches.size<Torch>(); i++)
			{
				int mOverlayAlpha = this.mTorches[i].mOverlayAlpha;
				if (mOverlayAlpha != 0)
				{
					if (mOverlayAlpha != 255)
					{
						g.SetColorizeImages(true);
					}
					g.SetColor(255, 255, 255, mOverlayAlpha);
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.Scale(array[i], array2[i]);
					g.DrawImageTransform(imageByID, this.mGlobalTranform, (float)(array3[i] + imageByID.mWidth / 2), (float)(array4[i] + imageByID.mHeight / 2));
					g.SetColorizeImages(false);
				}
			}
		}

		public virtual void DrawSkullPit(Graphics g)
		{
			bool flag = false;
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				if (this.mEffects[i].DrawSkullPit(g, this.mHoleMgr))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.mHoleMgr.DrawRings(g);
				this.mHoleMgr.Draw(g);
			}
		}

		public virtual void DrawTunnel(Graphics g, Image img, int x, int y, int w, int h)
		{
			if (this.mNum != 2147483647 || this.mZone != 4 || this.mBoss == null)
			{
				for (int i = 0; i < this.mEffects.size<Effect>(); i++)
				{
					if (!this.mEffects[i].DrawTunnel(g, img, x, y))
					{
						return;
					}
				}
			}
			g.DrawImage(img, x, y, w, h);
		}

		public void DrawGauntletUI(Graphics g)
		{
			ZumasRevenge.Common._S(ZumasRevenge.Common._M(465));
			ZumasRevenge.Common._S(ZumasRevenge.Common._M(22));
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_OFF);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_ON);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_TIMER_FRAME);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMECENTER);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMELEFT);
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_OFF))) - ZumasRevenge.Common._S(27), ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_OFF)) + ZumasRevenge.Common._S(7));
			g.DrawImage(imageByID2, GameApp.gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_ON))) - ZumasRevenge.Common._S(27), ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GAUNTLET_MAIN_BAR_BONUS_ON)) + ZumasRevenge.Common._S(7), new Rect(0, 0, (int)((float)imageByID2.mWidth * this.mCurGauntletMultPct), imageByID2.mHeight));
			g.DrawImage(imageByID3, GameApp.gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_TIMER_FRAME))), ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_TIMER_FRAME)));
			int num = (int)this.mGauntletTimeRedAmt;
			if (this.mGauntletTimeRedAmt > 0f)
			{
				g.SetColorizeImages(true);
				if (num > 128)
				{
					num = (255 - num) * 2;
				}
				else
				{
					num *= 2;
				}
				if (num > 255)
				{
					num = 255;
				}
				else if (num < 0)
				{
					num = 0;
				}
			}
			int num2 = this.mApp.GetLevelMgr().mGauntletSessionLength - this.mGauntletCurTime;
			if (num2 < 0)
			{
				num2 = 0;
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_STROKE);
			g.SetFont(fontByID);
			g.SetColor(192, 230, 99);
			int theY = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(93)) + (ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(35)) - g.GetFont().mHeight) / 2;
			g.WriteString(JeffLib.Common.UpdateToTimeStr(num2), GameApp.gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(ZumasRevenge.Common._M(225))), theY, ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(141)), 0);
			if (num > 0)
			{
				g.SetColor(255, 0, 0, num);
				g.WriteString(JeffLib.Common.UpdateToTimeStr(num2), GameApp.gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(ZumasRevenge.Common._M(225))), theY, ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(141)), 0);
			}
			g.SetColorizeImages(false);
			int wideScreenAdjusted = GameApp.gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMECENTER)));
			int theY2 = ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMECENTER));
			int wideScreenAdjusted2 = GameApp.gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_CHALLENGE_UI_GAUNTLETFRAMELEFT)));
			g.DrawImage(imageByID4, wideScreenAdjusted, theY2);
			g.DrawImage(imageByID5, wideScreenAdjusted2, ZumasRevenge.Common._S(0));
			g.DrawImageMirror(imageByID4, wideScreenAdjusted + imageByID4.GetWidth() + ZumasRevenge.Common._S(60), theY2);
		}

		public void InitEffects(Level copy_effects_from)
		{
			for (int i = 0; i < this.mEffectNames.size<string>(); i++)
			{
				Effect effect = this.mApp.GetLevelMgr().mEffectManager.GetEffect(this.mEffectNames[i], this.mId, copy_effects_from);
				if (effect != null)
				{
					this.mEffects.Add(effect);
				}
				this.mEffects[i].NukeParams();
			}
		}

		public void InitEffects()
		{
			this.InitEffects(null);
		}

		public void ResetEffects()
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].LoadResources();
				this.mEffects[i].Reset(this.mId);
			}
		}

		public void ForceTreasure(int tnum)
		{
			this.mBoard.mCurTreasureNum = tnum;
			this.mBoard.mCurTreasure = this.mTreasurePoints[tnum];
			this.mBoard.mMinTreasureY = (this.mBoard.mMaxTreasureY = float.MaxValue);
		}

		public Ball GetBallById(int id)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				foreach (Ball ball in this.mCurveMgr[i].mBallList)
				{
					if (ball.GetId() == id)
					{
						return ball;
					}
				}
				foreach (Ball ball2 in this.mCurveMgr[i].mPendingBalls)
				{
					if (ball2.GetId() == id)
					{
						return ball2;
					}
				}
			}
			return null;
		}

		public bool AllTorchesOut()
		{
			if (this.mTorchStageState != 6)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < Enumerable.Count<Torch>(this.mTorches); i++)
			{
				if (!this.mTorches[i].mActive)
				{
					num++;
				}
			}
			return num == Enumerable.Count<Torch>(this.mTorches) && Enumerable.Count<Torch>(this.mTorches) > 0;
		}

		public bool IsFinalBossLevel()
		{
			return Enumerable.Count<Torch>(this.mTorches) > 0;
		}

		public virtual void UpdateUI()
		{
			if (this.mZumaBarState >= ZumasRevenge.Common._M(2) && !this.mBoard.GauntletMode() && this.mCurBarSize < this.mTargetBarSize)
			{
				this.mCurBarSize++;
			}
			if (this.mZumaBarState == 0)
			{
				this.mGingerMouthX += this.mGingerMouthVX;
				int num = (int)this.mGingerMouthXStart + ZumasRevenge.Common._S(15);
				if (this.mGingerMouthX >= (float)num)
				{
					this.mGingerMouthX = (float)num;
					this.mZumaBarState++;
					this.mGingerMouthVX = 0f;
				}
			}
			else if (this.mZumaBarState == 1)
			{
				this.mGoldBallXOff += ZumasRevenge.Common._S(0.75f);
				if ((this.mZumaBallPct += 0.05f) >= 1.2f)
				{
					this.mZumaBallPct = 1.2f;
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 2)
			{
				if ((this.mZumaBallPct -= 0.05f) <= 1f)
				{
					this.mZumaBallPct = 1f;
				}
				if (this.mGingerMouthVX == 0f && this.mZumaBallPct <= 1f)
				{
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 4)
			{
				this.mFredMouthX += this.mFredMouthVX;
				int num2 = ZumasRevenge.Common._S(15);
				if (this.mFredMouthX <= this.mFredMouthXStart - (float)num2)
				{
					this.mFredMouthX = this.mFredMouthXStart - (float)num2;
					this.mFredMouthVX *= -1f;
					this.mZumaBarState++;
					this.mFredTongueVX = ZumasRevenge.Common._S(-2.5f);
				}
			}
			else if (this.mZumaBarState == 5)
			{
				this.mFredTongueX += this.mFredTongueVX;
				int num3 = ZumasRevenge.Common._S(36);
				if (this.mFredTongueX <= (float)(541 - num3))
				{
					this.mFredTongueX = (float)(541 - num3);
					this.mFredTongueVX *= -1f;
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 6)
			{
				this.mFredTongueX += this.mFredTongueVX;
				this.mGoldBallXOff += ZumasRevenge.Common._S(2.5f);
				if ((this.mZumaBallPct += 0.05f) >= 1.2f)
				{
					this.mZumaBallPct = 1.2f;
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 7)
			{
				this.mFredTongueX += this.mFredTongueVX;
				if ((this.mZumaBallPct -= 0.05f) <= 1f)
				{
					this.mZumaBallPct = 1f;
				}
				this.mGoldBallXOff += ZumasRevenge.Common._S(0.75f);
				if (this.mFredTongueX >= 541f)
				{
					this.mFredTongueX = 541f;
					this.mFredTongueVX = 0f;
				}
				if (this.mFredTongueX >= 541f)
				{
					this.mZumaBarState++;
					int num4 = (int)ZumasRevenge.Common._S(2.5f);
					this.mFredMouthVX = (float)num4;
					this.mGingerMouthVX = (float)(-(float)num4);
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BAR_FULL));
				}
			}
			else if (this.mZumaBarState >= 8 && this.mZumaBarState < 12)
			{
				this.mFredMouthX += this.mFredMouthVX;
				this.mGingerMouthX += this.mGingerMouthVX;
				int num5 = 0;
				if (this.mFredMouthX >= this.mFredMouthXStart && this.mFredMouthVX > 0f)
				{
					num5++;
					this.mFredMouthX = this.mFredMouthXStart;
				}
				else if (this.mFredMouthX <= this.mFredMouthXStart - (float)ZumasRevenge.Common._S(15) && this.mFredMouthVX < 0f)
				{
					num5++;
					this.mFredMouthX = this.mFredMouthXStart - (float)ZumasRevenge.Common._S(15);
				}
				if (this.mGingerMouthX <= this.mGingerMouthXStart && this.mGingerMouthVX < 0f)
				{
					this.mGingerMouthX = this.mGingerMouthXStart;
					num5++;
				}
				else if (this.mGingerMouthX >= this.mGingerMouthXStart + (float)ZumasRevenge.Common._S(15) && this.mGingerMouthVX > 0f)
				{
					this.mGingerMouthX = this.mGingerMouthXStart + (float)ZumasRevenge.Common._S(15);
					num5++;
				}
				if (num5 == 2)
				{
					this.mZumaBarState++;
					this.mFredMouthVX *= -1f;
					this.mGingerMouthVX *= -1f;
				}
			}
			else if (this.mZumaBarState == 12)
			{
				if ((this.mBarLightness += 18f) >= 255f)
				{
					this.mBarLightness = 255f;
					this.mZumaBarState++;
				}
			}
			else if (this.mZumaBarState == 13)
			{
				if ((this.mBarLightness -= 18f) <= 0f)
				{
					this.mBarLightness = 0f;
					this.mZumaBarState++;
					this.mZumaPulseUCStart = this.mUpdateCount;
				}
			}
			else if (this.mZumaBarState == 14 && this.mBoard.GauntletMode())
			{
				this.mCurBarSize -= 2;
				if (this.mCurBarSize <= 0)
				{
					this.Reset();
					this.mCurBarSize = 0;
				}
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_GOLD_BALL);
			if (this.mCurBarSize != this.mTargetBarSize)
			{
				this.mZumaBallFrame = (this.mZumaBallFrame + 1) % imageByID.mNumCols;
			}
			if (!this.mHaveReachedTarget && !this.mBoard.GauntletMode() && this.ShouldUpdateZumaBar() && this.mNumCurves > 0 && this.mCurBarSize == 330 && this.mBoard.mScore >= this.mBoard.mScoreTarget && this.mBoss == null)
			{
				this.mZumaBarState = 4;
				this.mFredMouthVX = ZumasRevenge.Common._S(-2.5f);
				if (!this.mBoard.IsEndless())
				{
					this.mHaveReachedTarget = true;
					for (int i = 0; i < this.mNumCurves; i++)
					{
						this.mCurveMgr[i].ZumaAchieved(true);
						if (!this.mBoard.DestroyAll())
						{
							this.mCurveMgr[i].DetonateBalls();
						}
					}
					this.mApp.mUserProfile.GetAdvModeVars().mNumZumasCurLevel++;
					this.mBoard.mNumZumaBalls = this.GetTotalBallsOnLevel();
				}
				if (!this.mBoard.DestroyAll())
				{
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_EXPLODE));
				}
			}
			int num6 = this.mBoard.mScoreTarget - this.mBoard.GetLevelBeginScore();
			if (num6 > 0)
			{
				int num7 = this.mBoard.mScoreTarget - this.mBoard.mScore;
				if (num7 < 0)
				{
					num7 = 0;
					if (this.mBoard.mLevelEndFrame == 0)
					{
						if (this.mBoard.GetNumBallColors() <= 2)
						{
							this.mBoard.mLevelEndFrame = this.mBoard.GetStateCount();
						}
					}
					else if (this.mBoard.GetStateCount() - this.mBoard.mLevelEndFrame == 3000)
					{
						for (int j = 0; j < this.mNumCurves; j++)
						{
							this.mCurveMgr[j].mCurveDesc.mVals.mPowerUpFreq[0] = 500;
							this.mCurveMgr[j].mCurveDesc.mVals.mPowerUpFreq[1] = 0;
							this.mCurveMgr[j].mCurveDesc.mVals.mPowerUpFreq[2] = 0;
							this.mCurveMgr[j].mCurveDesc.mVals.mPowerUpFreq[3] = 0;
							this.mCurveMgr[j].mCurveDesc.mVals.mAccelerationRate = 0.0003f;
						}
					}
				}
				if (this.mBoss == null && !this.mBoard.GauntletMode())
				{
					this.mTargetBarSize = 330 - 330 * num7 / num6;
				}
			}
			if (this.mBoss != null)
			{
				this.mTargetBarSize = (int)(330f - (1f - this.mBoss.GetHP() / 100f) * 330f);
			}
			if (this.mBoard.GauntletMode() && !this.DoingInitialPathHilite())
			{
				if (this.mGauntletCurTime < this.mApp.GetLevelMgr().mGauntletSessionLength)
				{
					this.mGauntletCurTime++;
					if (this.mGauntletCurTime % 100 == 0 && this.mApp.GetLevelMgr().mGauntletSessionLength - this.mGauntletCurTime <= 1100)
					{
						this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_CHALLENGE_COUNTDOWN));
					}
					if (GameApp.gDDS.SetGauntletTime(this.mGauntletCurTime))
					{
						this.UpdateChallengeModeDifficulty();
					}
					int num8 = this.mApp.GetLevelMgr().mGauntletSessionLength - this.mGauntletCurTime;
					if (num8 <= 1100 && num8 % 100 == 0)
					{
						this.mGauntletTimeRedAmt = 255f;
					}
				}
				if (this.mGauntletCurTime >= this.mApp.GetLevelMgr().mGauntletSessionLength && this.CurvesAtRest())
				{
					this.mBoard.EndGauntletMode(true);
					bool theAcedLevel = false;
					if (this.mBoard.mScore > this.mChallengeAcePoints)
					{
						theAcedLevel = true;
					}
					GameApp.gApp.ReportEndOfLevelMetrics(this.mBoard, true, theAcedLevel);
				}
			}
			if (this.mGauntletTimeRedAmt > 0f)
			{
				this.mGauntletTimeRedAmt -= ZumasRevenge.Common._M(2.6f);
			}
		}

		public virtual void UpdatePlaying()
		{
			if (this.mCurMultiplierTimeLeft > 0 && --this.mCurMultiplierTimeLeft == 0)
			{
				this.mBoard.GauntletMultiplierEnded();
			}
			if (this.mDoingPadHints)
			{
				if (this.mBoard.mZumaTips.size<ZumaTip>() != 0)
				{
					return;
				}
				this.mDoingPadHints = false;
			}
			for (int i = 0; i < this.mTorches.size<Torch>(); i++)
			{
				this.mTorches[i].Update();
				if (!this.mTorches[i].mActive)
				{
					this.mTorches[i].mOverlayAlpha += ZumasRevenge.Common._M(2);
					if (this.mTorches[i].mOverlayAlpha > 255)
					{
						this.mTorches[i].mOverlayAlpha = 255;
					}
				}
				else
				{
					this.mTorches[i].mOverlayAlpha -= ZumasRevenge.Common._M(2);
					if (this.mTorches[i].mOverlayAlpha < 0)
					{
						this.mTorches[i].mOverlayAlpha = 0;
					}
				}
			}
			if (this.mBoard.GauntletMode())
			{
				float num = (float)this.mNumGauntletBallsBroke / (float)this.mGauntletCurNumForMult;
				float num2 = num - this.mCurGauntletMultPct;
				float num3 = this.mCurGauntletMultPct;
				if (this.mCurGauntletMultPct < num || num2 < -0.001f)
				{
					this.mCurGauntletMultPct += ZumasRevenge.Common._M(0.01f);
					if (num3 < num && this.mCurGauntletMultPct > num)
					{
						this.mCurGauntletMultPct = num;
					}
					else if (this.mCurGauntletMultPct > 1f)
					{
						this.mCurGauntletMultPct = 0f;
					}
				}
			}
			bool flag = this.mHasReachedCruisingSpeed;
			this.mHasReachedCruisingSpeed = true;
			this.mAllCurvesAtRolloutPoint = true;
			ZumasRevenge.Common._M(20f);
			bool flag2 = false;
			if (!this.IsFinalBossLevel() || this.mTorchStageState == 6)
			{
				for (int j = 0; j < this.mNumCurves; j++)
				{
					if (this.mCurveMgr[j].UpdatePlaying() && j + 1 < this.mNumCurves)
					{
						this.mCurveMgr[j + 1].mInitialPathHilite = true;
					}
					if (this.mCurveMgr[j].mSparkles.size<PathSparkle>() > 0)
					{
						flag2 = true;
					}
					if (!this.mCurveMgr[j].HasReachedCruisingSpeed())
					{
						this.mHasReachedCruisingSpeed = false;
					}
					if (!this.mCurveMgr[j].HasReachedRolloutPoint())
					{
						this.mAllCurvesAtRolloutPoint = false;
					}
					if (this.mTempSpeedupTimer == 1)
					{
						this.mCurveMgr[j].mOverrideSpeed = -1f;
					}
					int farthestBallPercent = this.mCurveMgr[j].GetFarthestBallPercent();
					if (farthestBallPercent > this.mFurthestBallDistance)
					{
						this.mFurthestBallDistance = farthestBallPercent;
					}
				}
			}
			if (!flag && this.mHasReachedCruisingSpeed)
			{
				this.mApp.mSoundPlayer.Fade((this.mZone == 5) ? Res.GetSoundByID(ResID.SOUND_UNDERWATER_ROLLOUT) : Res.GetSoundByID(ResID.SOUND_ROLLING));
			}
			if (!ZumasRevenge.Common.gAddBalls && !flag2 && !this.mBoard.mPreventBallAdvancement)
			{
				ZumasRevenge.Common.gAddBalls = true;
				for (int k = 0; k < this.mNumCurves; k++)
				{
					this.mCurveMgr[k].mInitialPathHilite = false;
				}
			}
			if (this.mTempSpeedupTimer > 0)
			{
				this.mTempSpeedupTimer--;
			}
			if (this.mBoard.HasAchievedZuma() && this.mPostZumaTimeCounter > 0)
			{
				this.mPostZumaTimeCounter--;
				float num4 = (float)(this.mApp.GetLevelMgr().mPostZumaTime - this.mPostZumaTimeCounter) / (float)this.mApp.GetLevelMgr().mPostZumaTime;
				this.mPostZumaTimeSlowInc = num4 * this.mApp.GetLevelMgr().mPostZumaTimeSlowInc;
				this.mPostZumaTimeSpeedInc = num4 * this.mApp.GetLevelMgr().mPostZumaTimeSlowInc;
			}
		}

		public virtual void UpdateBossIntro()
		{
			if (this.mBoss != null)
			{
				this.mBoss.Update();
			}
		}

		public virtual void DrawUI(Graphics g)
		{
			if (this.mBoss != null || this.IsFinalBossLevel())
			{
				g.mTransX = 0f;
				this.DrawBossUI(g);
				g.mTransX = (float)this.mApp.mBoardOffsetX;
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_WOOD);
			this.mBarXOffset = (int)((float)imageByID.mWidth * 0.05f);
			this.DrawWoodPanel(g);
			this.mBoard.DrawRollerScore(g);
			if (this.mBoard.GauntletMode())
			{
				this.DrawGauntletUI(g);
			}
			else
			{
				this.DrawScoreFrame(g);
				this.DrawZumaBar(g);
				this.DrawFredAndGinger(g);
			}
			this.DrawTikiEnds(g);
		}

		public virtual void DrawGunPoints(Graphics g)
		{
			if (this.mNumFrogPoints > 1)
			{
				for (int i = 0; i < this.mNumFrogPoints; i++)
				{
					if (this.mFrogImages[i].mImage != null)
					{
						int theCel = ((this.mBoard.mMouseOverGunPos == i) ? 1 : 0);
						g.DrawImageCel(this.mFrogImages[i].mImage, ZumasRevenge.Common._S(this.mFrogX[i]) - this.mFrogImages[i].mImage.GetCelWidth() / 2 + GameApp.gScreenShakeX, ZumasRevenge.Common._S(this.mFrogY[i]) - this.mFrogImages[i].mImage.GetCelHeight() / 2 + GameApp.gScreenShakeY, theCel);
					}
				}
			}
			float num = (1f - this.mTorchDaisScale) * (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(189));
			if (this.IsFinalBossLevel() || this.mTorchStageState >= 10)
			{
				for (int j = 0; j < this.mTorches.size<Torch>(); j++)
				{
					this.mTorches[j].DrawAbove(g);
				}
				if (this.mTorchStageAlpha > 0f)
				{
					g.SetColor(0, 0, 0, (int)Math.Min(255f, this.mTorchStageAlpha));
					g.FillRect(ZumasRevenge.Common._S(-80), 0, GameApp.gApp.mWidth + ZumasRevenge.Common._S(160), GameApp.gApp.mHeight);
				}
				Image imageByID = Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_BASE);
				Image imageByID2 = Res.GetImageByID(ResID.IMAGE_LEVELS_BOSS6PART1_DIAS);
				Image imageByID3 = Res.GetImageByID(ResID.IMAGE_LARGE_FROG);
				Image imageByID4 = Res.GetImageByID(ResID.IMAGE_FROG_SHADOW);
				if (this.IsFinalBossLevel())
				{
					g.DrawImage(imageByID, ZumasRevenge.Common._DS(690 - this.mApp.mOffset160X), ZumasRevenge.Common._DS(330));
				}
				string[] array = new string[] { "start", "squish", "rattle" };
				int num2 = imageByID2.mWidth * (int)this.mTorchDaisScale;
				int num3 = imageByID2.mHeight * (int)this.mTorchDaisScale;
				int num4 = ZumasRevenge.Common._DS(793 - this.mApp.mOffset160X);
				int num5 = ZumasRevenge.Common._DS(395);
				num4 += (imageByID2.mWidth - num2) / 2;
				num5 += (imageByID2.mHeight - num3) / 2;
				if (this.mTorchStageState < 9)
				{
					g.DrawImage(imageByID2, num4 + ZumasRevenge.Common._DS(this.mTorchStageShakeAmt), num5 - ZumasRevenge.Common._DS(this.mTorchStageShakeAmt) + (int)num, num2, num3);
				}
				for (int k = 0; k < this.mDaisRocks.size<DaisRock>(); k++)
				{
					DaisRock daisRock = this.mDaisRocks[k];
					g.SetColorizeImages(true);
					g.SetColor(255, 255, 255, (int)daisRock.mAlpha);
					this.mGlobalTranform.Reset();
					this.mGlobalTranform.Scale(daisRock.mSize, daisRock.mSize);
					float rot = (255f - daisRock.mAlpha) / 255f * ZumasRevenge.Common._M(2.5f) * 3.14159274f;
					this.mGlobalTranform.RotateRad(rot);
					g.DrawImageTransform(daisRock.mImg, this.mGlobalTranform, daisRock.mX, daisRock.mY);
					g.SetColorizeImages(false);
				}
				if (this.mTorchStageState < 4)
				{
					int num6;
					switch (this.mTorchStageState)
					{
					case 0:
						num6 = 2;
						break;
					case 1:
						num6 = 2;
						break;
					default:
						num6 = 2;
						break;
					}
					Composition composition = this.mTorchCompMgr.GetComposition(array[num6]);
					int num7 = composition.mUpdateCount;
					if (num7 >= composition.GetMaxDuration())
					{
						num7 = composition.GetMaxDuration() - 1;
					}
					if (this.mTorchStageState == 0)
					{
						num7 = 1;
					}
					if (num7 == 0)
					{
						num7 = 1;
					}
					CumulativeTransform cumulativeTransform = new CumulativeTransform();
					cumulativeTransform.mTrans.Translate(this.mTorchBossX, this.mTorchBossY);
					if (this.mTorchStageState == 3)
					{
						cumulativeTransform.mTrans.Scale(this.mTorchDaisScale, this.mTorchDaisScale);
						cumulativeTransform.mTrans.Translate(((float)ZumasRevenge.Common._DS(composition.mWidth) - (float)ZumasRevenge.Common._DS(composition.mWidth) * this.mTorchDaisScale) / 1.5f + (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(80)) * (1f - this.mTorchDaisScale), ((float)ZumasRevenge.Common._DS(composition.mHeight) - (float)ZumasRevenge.Common._DS(composition.mHeight) * this.mTorchDaisScale) / ZumasRevenge.Common._M1(1.5f) + num);
					}
					composition.Draw(g, cumulativeTransform, num7, ZumasRevenge.Common._DS(1f));
					Image imageByID5 = Res.GetImageByID(ResID.IMAGE_BOSSES_EGG_ADD);
					Image imageByID6 = Res.GetImageByID(ResID.IMAGE_BOSSES_EGG);
					for (int l = 0; l < this.mEggs.size<TorchLevelEgg>(); l++)
					{
						TorchLevelEgg torchLevelEgg = this.mEggs[l];
						int num8 = (int)(torchLevelEgg.mAlpha * this.mTorchDaisScale);
						if (num8 != 255)
						{
							g.SetColorizeImages(true);
						}
						g.SetColor(255, 255, 255, num8);
						g.SetDrawMode(1);
						g.DrawImageRotated(imageByID5, (int)(torchLevelEgg.mX + (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-30))), (int)(torchLevelEgg.mY + (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(-30))), (double)torchLevelEgg.mAngle);
						g.SetDrawMode(0);
						g.DrawImageRotated(imageByID6, (int)torchLevelEgg.mX, (int)torchLevelEgg.mY, (double)torchLevelEgg.mAngle);
						g.SetColorizeImages(false);
					}
					return;
				}
				if (this.mTorchStageState == 8 || this.mTorchStageState == 7)
				{
					float num9 = this.mTorchDaisScale * ZumasRevenge.Common._M(0.5f);
					SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
					sexyTransform2D.Scale(this.mTorchDaisScale, this.mTorchDaisScale);
					sexyTransform2D.Translate((float)ZumasRevenge.Common._S(ZumasRevenge.Common._M(-2)), (float)ZumasRevenge.Common._S(ZumasRevenge.Common._M1(3)));
					sexyTransform2D.RotateRad(this.mFrog.GetAngle());
					sexyTransform2D.Translate((float)ZumasRevenge.Common._S(ZumasRevenge.Common._M(2)), (float)ZumasRevenge.Common._S(ZumasRevenge.Common._M1(-3)));
					float num10 = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(20f)) * (1f - this.mTorchDaisScale);
					g.DrawImageMatrix(imageByID4, sexyTransform2D, imageByID4.GetCelRect(1), (float)ZumasRevenge.Common._S(this.mFrog.GetCurX()) + (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-2)) * (1f - this.mTorchDaisScale) + (float)ZumasRevenge.Common._DS(this.mTorchStageShakeAmt), (float)ZumasRevenge.Common._S(this.mFrog.GetCurY()) + num10 + num - (float)ZumasRevenge.Common._DS(this.mTorchStageShakeAmt));
					num10 = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(30f)) * (1f - this.mTorchDaisScale);
					sexyTransform2D.LoadIdentity();
					sexyTransform2D.Scale(num9, num9);
					sexyTransform2D.RotateRad(this.mFrog.GetAngle());
					g.DrawImageMatrix(imageByID3, sexyTransform2D, (float)(ZumasRevenge.Common._S(this.mFrog.GetCenterX()) - ZumasRevenge.Common._DS(ZumasRevenge.Common._M(2)) + ZumasRevenge.Common._DS(this.mTorchStageShakeAmt)), (float)(ZumasRevenge.Common._S(this.mFrog.GetCenterY()) - ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(10))) + num10 + num - (float)ZumasRevenge.Common._DS(this.mTorchStageShakeAmt));
					return;
				}
				if (this.mFrogFlyOff != null)
				{
					this.mFrogFlyOff.Draw(g);
				}
			}
		}

		public void DrawBossUI(Graphics g)
		{
			GameApp gApp = GameApp.gApp;
			if (gApp.mBoard != null && !gApp.mBoard.mDrawBossUI)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_BOSSUI);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE);
			if (gApp.IsWideScreen())
			{
				g.DrawImage(imageByID, (int)((double)gApp.GetScreenRect().mWidth - (double)imageByID.GetWidth() * 1.5 - (double)imageByID2.GetWidth()), 0, (int)((float)imageByID.GetWidth() * 1.5f), imageByID.GetHeight());
				g.DrawImage(imageByID2, gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE))) - (gApp.GetScreenWidth() - gApp.mScreenBounds.mWidth), ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE)));
				return;
			}
			g.DrawImage(imageByID, (int)((double)gApp.GetScreenRect().mWidth - (double)imageByID.GetWidth() * 1.5), 0, (int)((double)imageByID.GetWidth() * 1.5), imageByID.GetHeight());
		}

		public void DrawWoodPanel(Graphics g)
		{
			int num = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_WOOD));
			int theY = ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_WOOD));
			int x = num - this.mBarXOffset;
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_WOOD);
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(x), theY);
			g.DrawImageMirror(imageByID, GameApp.gApp.GetWideScreenAdjusted(this.mBarXOffset), theY);
		}

		public void DrawScoreFrame(Graphics g)
		{
			int num = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_SCORE_FRAME)) + this.mBarXOffset;
			int theY = ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_SCORE_FRAME));
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_SCORE_FRAME);
			if (!GameApp.gApp.IsWideScreen())
			{
				num -= ZumasRevenge.Common._S(10);
			}
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(num), theY);
		}

		public void DrawTikiEnds(Graphics g)
		{
			if (!GameApp.gApp.IsWideScreen())
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LEFTFRAMESIDE);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE);
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(0), 0);
			g.DrawImage(imageByID2, GameApp.gApp.GetWidthAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHTFRAMESIDE))) - (GameApp.gApp.GetScreenWidth() - GameApp.gApp.mScreenBounds.mWidth), 0);
		}

		public void DrawZumaBar(Graphics g)
		{
			if (this.mTimer >= 0)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESSLITEWOOD);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_LIGHT);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_TOP);
			g.DrawImage(imageByID, this.mZumaBarX, ZumasRevenge.Common._S(9));
			this.SetZumaBarProgress();
			if (this.mZumaBarState < 2)
			{
				return;
			}
			this.DrawZumaBarProgress(g, imageByID2);
			this.DrawZumaBarProgressPulse(g);
			this.DrawZumaBarProgress(g, imageByID3);
		}

		public void DrawFredAndGinger(Graphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_CONNECT_BAR);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_JAW);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_JAW);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_TOP);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER);
			Image imageByID6 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER);
			g.DrawImage(imageByID, GameApp.gApp.GetWideScreenAdjusted(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_CONNECT_BAR)) - this.mBarXOffset), ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_CONNECT_BAR)), imageByID4.GetWidth(), imageByID.GetHeight());
			g.DrawImage(imageByID2, (int)this.mGingerMouthX + this.mBarXOffset, ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_JAW)) - ZumasRevenge.Common._S(3));
			g.DrawImage(imageByID3, (int)this.mFredMouthX - this.mBarXOffset, ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_JAW)) - ZumasRevenge.Common._S(2));
			this.DrawGoldBall(g);
			int x = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER)) + this.mBarXOffset;
			int x2 = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER)) - this.mBarXOffset;
			g.DrawImage(imageByID5, GameApp.gApp.GetWideScreenAdjusted(x), ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_LEFT_MOUTH_LOWER)));
			g.DrawImage(imageByID6, GameApp.gApp.GetWideScreenAdjusted(x2), ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_GUI_INGAME_UI_RIGHT_MOUTH_UPPER)));
		}

		public void SetZumaBarProgress()
		{
			int num = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_TOP).mWidth - this.mBarXOffset * 2;
			if (this.mZumaBarState >= 7 && this.mZumaBarState < 14)
			{
				this.mZumaBarWidth = num;
			}
			else
			{
				this.mZumaBarWidth = (int)((float)num * (float)this.mCurBarSize / 330f + (float)ZumasRevenge.Common._S(8));
			}
			this.mZumaBarWidth = Math.Min(this.mZumaBarWidth, num);
		}

		public void DrawZumaBarProgress(Graphics g, Image inImage)
		{
			g.DrawImage(inImage, this.mZumaBarX, ZumasRevenge.Common._S(9), new Rect(0, 0, this.mZumaBarWidth, inImage.mHeight));
			if (this.mZumaBarState < 12 || this.mZumaBarState > 13 || this.mBarLightness <= 0f)
			{
				return;
			}
			g.SetColorizeImages(true);
			g.SetDrawMode(1);
			g.SetColor(255, 255, 255, (int)this.mBarLightness);
			g.DrawImage(inImage, this.mZumaBarX, ZumasRevenge.Common._S(9), new Rect(0, 0, this.mZumaBarWidth, inImage.mHeight));
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		public void DrawZumaBarProgressPulse(Graphics g)
		{
			if (this.mZumaBarState < 14)
			{
				return;
			}
			g.PushState();
			g.SetDrawMode(1);
			int num = ZumasRevenge.Common._M(0) + JeffLib.Common.GetAlphaFromUpdateCount(this.mUpdateCount - this.mZumaPulseUCStart, ZumasRevenge.Common._M1(255));
			if (num > 255)
			{
				num = 255;
			}
			g.SetColorizeImages(true);
			g.SetColor(255, 255, 255, num);
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_LIGHT);
			g.DrawImage(imageByID, this.mZumaBarX, ZumasRevenge.Common._S(9), new Rect(0, 0, this.mZumaBarWidth, imageByID.mHeight));
			g.PopState();
		}

		public void DrawGoldBall(Graphics g)
		{
			if (this.mTimer >= 0 || this.mZumaBarState >= 8 || this.mZumaBallPct <= 0f)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_GOLD_BALL);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_PROGRESS_TOP);
			int theWidth = (int)((float)imageByID.GetCelHeight() * this.mZumaBallPct);
			int num = (int)((float)imageByID.GetCelWidth() * this.mZumaBallPct);
			int num2 = this.mZumaBarX - ZumasRevenge.Common._S(20) + this.mZumaBarWidth - imageByID.mHeight / 2 + ZumasRevenge.Common._S((int)this.mGoldBallXOff);
			if (num2 < this.mZumaBarX)
			{
				num2 = this.mZumaBarX;
			}
			Rect theDestRect = new Rect(num2, ZumasRevenge.Common._S(9) + (imageByID2.mHeight - num) / 2, theWidth, num);
			g.DrawImageCel(imageByID, theDestRect, this.mZumaBallFrame);
		}

		public virtual int GetFarthestBallPercent(ref int farthest_curve, bool ignore_gaps)
		{
			int num = 0;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				int farthestBallPercent = this.mCurveMgr[i].GetFarthestBallPercent(ignore_gaps);
				if (farthestBallPercent > num)
				{
					farthest_curve = i;
					num = farthestBallPercent;
				}
			}
			return num;
		}

		public virtual int GetFarthestBallPercent()
		{
			int num = 0;
			return this.GetFarthestBallPercent(ref num, true);
		}

		public virtual void NukeEffects()
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].DeleteResources();
			}
			this.mEffects.Clear();
		}

		public virtual void BulletFired(Bullet b)
		{
			for (int i = 0; i < Enumerable.Count<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].BulletFired(b);
			}
		}

		public virtual void BulletHit(Bullet b)
		{
			for (int i = 0; i < Enumerable.Count<Effect>(this.mEffects); i++)
			{
				this.mEffects[i].BulletHit(b);
			}
		}

		public virtual void ReactivateWalls(int wall_id)
		{
			for (int i = 0; i < Enumerable.Count<Wall>(this.mWalls); i++)
			{
				if (this.mWalls[i].mId == wall_id || wall_id == -1)
				{
					this.mWalls[i].mStrength = this.mWalls[i].mOrgStrength;
				}
			}
		}

		public virtual void ReactivateWalls()
		{
			this.ReactivateWalls(-1);
		}

		public virtual bool CompactCurves()
		{
			if (!this.CanCompactCurves())
			{
				return false;
			}
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].CompactCurve();
			}
			return true;
		}

		public virtual bool CanCompactCurves()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (!this.mCurveMgr[i].CanCompact())
				{
					return false;
				}
			}
			return true;
		}

		public virtual void SetupHiddenHoles()
		{
			if (this.mHoleMgr.GetNumHoles() < 2)
			{
				return;
			}
			int num = 0;
			HoleInfo hole;
			while ((hole = this.mHoleMgr.GetHole(num)) != null)
			{
				if (!hole.mVisible)
				{
					int num2 = 0;
					Rect theTRect = new Rect(hole.mX, hole.mY, 96, 96);
					theTRect.Inflate(-4, -4);
					HoleInfo hole2;
					while ((hole2 = this.mHoleMgr.GetHole(num2)) != null)
					{
						if (!hole2.mVisible)
						{
							num2++;
						}
						else
						{
							Rect rect = new Rect(hole2.mX, hole2.mY, 96, 96);
							rect.Inflate(-4, -4);
							if (rect.Intersects(theTRect))
							{
								hole.mShared.Add(num2);
							}
							num2++;
						}
					}
				}
				num++;
			}
		}

		public virtual void PlayerLostLevel()
		{
		}

		public void DeactivateLightningEffects()
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].ElectrifyBalls(-1, false);
			}
		}

		public bool HasPowerup(PowerType p)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (this.mCurveMgr[i].HasPowerup(p))
				{
					return true;
				}
			}
			return false;
		}

		public virtual int GetRandomPendingBallColor(int max_curve_colors)
		{
			return MathUtils.SafeRand() % max_curve_colors;
		}

		public virtual float GetRandomFrogBulletColor(int max_curve_colors, int color_num)
		{
			return 1f / (float)max_curve_colors;
		}

		public virtual Ball GetBallAtXY(int x, int y)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				foreach (Ball ball in this.mCurveMgr[i].mBallList)
				{
					if (ball.Contains(x, y))
					{
						return ball;
					}
				}
			}
			return null;
		}

		public Ball GetRandomBall()
		{
			int num = MathUtils.SafeRand() % this.mNumCurves;
			if (this.mCurveMgr[num].mBallList.Count > 3)
			{
				int num2 = MathUtils.SafeRand() % (this.mCurveMgr[num].mBallList.Count - 2);
				Ball ball = this.mCurveMgr[num].mBallList[num2];
				if (ball.GetPowerType() == PowerType.PowerType_Max)
				{
					return ball;
				}
			}
			return null;
		}

		public virtual void ParseUnknownAttribute(string key, string val)
		{
		}

		public virtual void CopyFrom(Level src)
		{
			for (int i = 0; i < this.mHoleMgr.GetNumHoles(); i++)
			{
				HoleInfo hole = this.mHoleMgr.GetHole(i);
				hole.mCurve = this.mCurveMgr[hole.mCurveNum];
			}
			this.mApp = GameApp.gApp;
			this.mBoard = this.mApp.GetBoard();
		}

		public int GetTotalBallsOnLevel()
		{
			int num = 0;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				num += Enumerable.Count<Ball>(this.mCurveMgr[i].mBallList);
				num += Enumerable.Count<Ball>(this.mCurveMgr[i].mPendingBalls);
			}
			return num;
		}

		public int GetMaxBallsForLevel()
		{
			int num = 0;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				num += this.mCurveMgr[i].mCurveDesc.mVals.mNumBalls;
			}
			return num;
		}

		public bool CheckFruitActivation(int curve_num)
		{
			if (this.mBoard.mPreventBallAdvancement)
			{
				return false;
			}
			int num = (this.mBoard.GauntletMode() ? this.mApp.GetLevelMgr().mGauntletTFreq : this.mTreasureFreq);
			if (!Board.gForceTreasure && (this.mBoard.mCurTreasure != null || MathUtils.SafeRand() % num != 0))
			{
				return false;
			}
			List<int> list = new List<int>();
			int num2;
			int num3;
			if (curve_num == -1)
			{
				num2 = 0;
				num3 = this.mNumCurves;
			}
			else
			{
				num3 = curve_num;
				num2 = curve_num;
			}
			for (int i = num2; i < num3; i++)
			{
				int farthestBallPercent = this.mCurveMgr[i].GetFarthestBallPercent();
				for (int j = 0; j < Enumerable.Count<TreasurePoint>(this.mTreasurePoints); j++)
				{
					TreasurePoint treasurePoint = this.mTreasurePoints[j];
					if (treasurePoint.mCurveDist[i] > 0 && farthestBallPercent >= treasurePoint.mCurveDist[i])
					{
						list.Add(j);
					}
				}
			}
			if (Enumerable.Count<int>(list) == 0)
			{
				return false;
			}
			int num4 = MathUtils.SafeRand() % Enumerable.Count<int>(list);
			this.mBoard.mCurTreasureNum = list[num4];
			this.mBoard.mCurTreasure = this.mTreasurePoints[list[num4]];
			this.mBoard.mMinTreasureY = (this.mBoard.mMaxTreasureY = float.MaxValue);
			return true;
		}

		public bool CurvesAtRest()
		{
			if (this.mBoard.HasFiredBullets() || this.mBoard.GetGun().IsFiring())
			{
				return false;
			}
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (!this.mCurveMgr[i].AtRest())
				{
					return false;
				}
			}
			return true;
		}

		public virtual void MadeCombo(int combo_size)
		{
		}

		public virtual void MadeGapShot(int gap_size)
		{
		}

		public virtual void MadeConsecutiveClear(int clear_size)
		{
		}

		public virtual void ClearedInARowBonus()
		{
		}

		public virtual void AllBallsDestroyed()
		{
		}

		public virtual void BallExploded(int ball_type)
		{
		}

		public virtual bool ShouldUpdateZumaBar()
		{
			return true;
		}

		public virtual bool AllowPointsFromBalls()
		{
			return true;
		}

		public virtual bool CanAdvanceBalls()
		{
			return this.mBoss == null || this.mBoss.CanAdvanceBalls();
		}

		public virtual bool BeatLevelOverride()
		{
			return false;
		}

		public virtual void TemporarilySpeedUpCurves(float max_speed, int time_count)
		{
			this.mTempSpeedupTimer = time_count;
			for (int i = 0; i < this.mNumCurves; i++)
			{
				this.mCurveMgr[i].mOverrideSpeed = max_speed;
			}
		}

		public virtual void BallCreatedCallback(Ball b, int num_created)
		{
		}

		public virtual void MouseDown(int x, int y, int cc)
		{
		}

		public virtual void ChangedPad(int new_pad)
		{
			if (!this.mDoingPadHints)
			{
				return;
			}
			this.mBoard.mZumaTips[0] = null;
			this.mBoard.mZumaTips.RemoveAt(0);
			this.mBoard.MarkDirty();
		}

		public virtual int GetFrogReloadType()
		{
			if (!this.mApp.mUserProfile.HasSeenHint(ZumaProfile.FIRST_SHOT_HINT) && !this.mBoard.GauntletMode() && this.mNum == 1 && this.mZone == 1)
			{
				return 2;
			}
			if (this.mBoss != null)
			{
				return this.mBoss.GetFrogReloadType();
			}
			return -1;
		}

		public virtual void PlayerStartedFiring()
		{
			if (this.mBoss != null)
			{
				this.mBoss.PlayerStartedFiring();
			}
		}

		public float GetPowerIncPct()
		{
			if (this.mBoard.IronFrogMode() || this.mBoard.GauntletMode())
			{
				return 0f;
			}
			float num = (float)this.mCurBarSize / 330f;
			if (num >= this.mApp.GetLevelMgr().mPowerupIncAtZumaPct)
			{
				return this.mApp.GetLevelMgr().mPowerIncPct;
			}
			return 0f;
		}

		public void IncNumBallsExploded(int val)
		{
			this.mApp.mUserProfile.mBallsBroken++;
			if (!this.mBoard.GauntletMode())
			{
				return;
			}
			this.mNumGauntletBallsBroke += val;
			if (this.mNumGauntletBallsBroke >= this.mGauntletCurNumForMult)
			{
				this.mNumGauntletBallsBroke %= this.mGauntletCurNumForMult;
				int num = SexyFramework.Common.Rand() % this.mNumCurves;
				this.mCurveMgr[num].mNumMultBallsToSpawn++;
				this.mGauntletMultipliersEarned++;
			}
		}

		public virtual bool CanUpdate()
		{
			return true;
		}

		public int GetOwningCurve(Ball b)
		{
			for (int i = 0; i < this.mNumCurves; i++)
			{
				if (this.mCurveMgr[i].HasBall(b))
				{
					return i;
				}
			}
			return -1;
		}

		public void UpdateEffects()
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].Update();
			}
		}

		public virtual bool CanRotateFrog()
		{
			return this.mEndSequence != 2 || ((this.mTorchStageState == 13 || this.mTorchStageState == -1) && this.mBoss.GetHP() > 0f);
		}

		public virtual bool CanFireBall()
		{
			int num = 0;
			int num2 = 0;
			while (num2 < this.mNumCurves && this.mCurveMgr[num2].IsWinning())
			{
				num2++;
				num++;
			}
			return (!this.mBoard.GauntletMode() || this.mGauntletCurTime < this.mApp.GetLevelMgr().mGauntletSessionLength) && num != this.mNumCurves && (!this.mDoTorchCrap || this.mHasDoneTorchCrap) && (this.mApp.mUserProfile.HasSeenHint(ZumaProfile.FIRST_SHOT_HINT) || ((this.HasReachedCruisingSpeed() || this.mBoard.GauntletMode() || (this.mBoss != null && this.mBoss.AllowFrogToFire())) && (Enumerable.Count<ZumaTip>(this.mBoard.mZumaTips) == 0 || (this.mBoard.mZumaTips[0].mId == ZumaProfile.FIRST_SHOT_HINT && this.mFrog.GetAngle() >= ZumasRevenge.Common._M(4.504f) && this.mFrog.GetAngle() <= ZumasRevenge.Common._M1(4.9049f)))));
		}

		public virtual bool CanUseKeyboard()
		{
			return true;
		}

		public virtual bool CanSwapBalls()
		{
			return true;
		}

		public virtual Level Instantiate()
		{
			Level level = this.Clone();
			level.mHoleMgr = null;
			return level;
		}

		public virtual void SetFrog(Gun g)
		{
			this.mFrog = g;
			if (this.mBoss != null)
			{
				this.mBoss.FrogInitialized(g);
			}
			if (this.mSecondaryBoss != null)
			{
				this.mSecondaryBoss.FrogInitialized(g);
			}
			if (this.mMoveType != 0 && this.mCurveMgr[0] != null)
			{
				int endPoint = this.mCurveMgr[0].mWayPointMgr.GetEndPoint();
				float num;
				float num2;
				this.mCurveMgr[0].GetXYFromWaypoint(endPoint, out num, out num2);
				if (this.mMoveType == 1)
				{
					if (num2 < (float)g.GetCenterY())
					{
						g.SetDestAngle(-3.14159f);
						return;
					}
					g.SetDestAngle(0f);
					return;
				}
				else
				{
					if (num < (float)g.GetCenterX())
					{
						g.SetDestAngle(-1.570795f);
						return;
					}
					g.SetDestAngle(1.570795f);
				}
			}
		}

		public virtual void SyncState(DataSync sync)
		{
			this.SyncWalls(sync, true);
			bool flag = this.mBoss != null;
			bool flag2 = this.mBoss == this.mSecondaryBoss;
			sync.SyncBoolean(ref flag);
			sync.SyncBoolean(ref flag2);
			sync.SyncLong(ref this.mInvertMouseTimer);
			sync.SyncBoolean(ref this.mCanDrawBoss);
			if (flag)
			{
				bool flag3 = false;
				if (this.mBoard.ShouldBypassFinalSequenceOnLoad())
				{
					flag3 = true;
				}
				sync.SyncBoolean(ref flag3);
				if (!flag3)
				{
					if (sync.isWrite())
					{
						this.mBoss.SyncState(sync);
					}
					else
					{
						if (flag2)
						{
							this.mBoss = this.mSecondaryBoss;
						}
						this.mBoss.SyncState(sync);
					}
				}
			}
			sync.SyncFloat(ref this.mTorchDaisScale);
			sync.SyncLong(ref this.mTorchStageState);
			sync.SyncLong(ref this.mTorchStageTimer);
			sync.SyncFloat(ref this.mTorchStageAlpha);
			if (sync.isRead())
			{
				if (this.mTorchStageState >= 9 && this.mTorchStageState < 13)
				{
					this.mTorchStageState = 13;
					this.mBoard.mPreventBallAdvancement = false;
					this.mTorchStageTimer = 0;
					this.mTorchDaisScale = 0f;
					this.mCanDrawBoss = true;
				}
				this.mTorches.Clear();
				Buffer buffer = sync.GetBuffer();
				int num = (int)buffer.ReadLong();
				for (int i = 0; i < num; i++)
				{
					Torch torch = new Torch();
					torch.SyncState(sync);
					this.mTorches.Add(torch);
				}
				if (this.mTorchStageState != -1 && this.mTorchStageState < 6)
				{
					this.InitFinalBossLevel();
				}
				else if (this.mTorchStageState == 6)
				{
					this.mBoard.mPreventBallAdvancement = false;
					this.mDoTorchCrap = false;
					this.mHasDoneTorchCrap = true;
					this.mTorchDaisScale = 1f;
					this.mTorchTextAlpha = 0f;
				}
			}
			else
			{
				Buffer buffer2 = sync.GetBuffer();
				buffer2.WriteLong((long)this.mTorches.Count);
				for (int j = 0; j < this.mTorches.Count; j++)
				{
					this.mTorches[j].SyncState(sync);
				}
			}
			sync.SyncBoolean(ref this.mDoTorchCrap);
			sync.SyncBoolean(ref this.mHasDoneTorchCrap);
			sync.SyncFloat(ref this.mTorchTextAlpha);
			sync.SyncLong(ref this.mFurthestBallDistance);
			sync.SyncLong(ref this.mCurFrogPoint);
			sync.SyncLong(ref this.mTempSpeedupTimer);
			sync.SyncBoolean(ref this.mHaveReachedTarget);
			sync.SyncFloat(ref this.mBarLightness);
			sync.SyncFloat(ref this.mZumaBallPct);
			sync.SyncLong(ref this.mZumaBarState);
			sync.SyncFloat(ref this.mGoldBallXOff);
			sync.SyncFloat(ref this.mGingerMouthX);
			sync.SyncFloat(ref this.mGingerMouthVX);
			sync.SyncFloat(ref this.mFredMouthX);
			sync.SyncFloat(ref this.mFredMouthVX);
			sync.SyncFloat(ref this.mFredTongueX);
			sync.SyncFloat(ref this.mFredTongueVX);
			sync.SyncLong(ref this.mCurBarSize);
			sync.SyncLong(ref this.mTargetBarSize);
			this.SyncWalls(sync, true);
			for (int k = 0; k < this.mNumCurves; k++)
			{
				this.mCurveMgr[k].SyncState(sync);
			}
			sync.SyncBoolean(ref this.m_canGetAchievementNoMove);
			sync.SyncBoolean(ref this.m_canGetAchievementNoJump);
			sync.SyncLong(ref this.m_OriginX);
			sync.SyncLong(ref this.m_OriginY);
		}

		private void SyncWalls(DataSync sync, bool clear)
		{
			if (sync.isRead())
			{
				if (clear)
				{
					this.mWalls.Clear();
				}
				long num = sync.GetBuffer().ReadLong();
				int num2 = 0;
				while ((long)num2 < num)
				{
					Wall wall = new Wall();
					wall.SyncState(sync);
					this.mWalls.Add(wall);
					num2++;
				}
				return;
			}
			sync.GetBuffer().WriteLong((long)this.mWalls.Count);
			foreach (Wall wall2 in this.mWalls)
			{
				wall2.SyncState(sync);
			}
		}

		public bool AllCurvesAtRolloutPoint()
		{
			return this.mAllCurvesAtRolloutPoint;
		}

		public bool HasReachedCruisingSpeed()
		{
			return this.mHasReachedCruisingSpeed;
		}

		public float GetBarPercent()
		{
			return (float)this.mCurBarSize / (float)this.mTargetBarSize;
		}

		public int GetBossBombDelay()
		{
			if (this.mBoss == null)
			{
				return 0;
			}
			BossShoot bossShoot = this.mBoss as BossShoot;
			if (bossShoot == null)
			{
				return 0;
			}
			return bossShoot.mBombAppearDelay;
		}

		public void ProximityBombActivated(float x, float y, int radius)
		{
			if (this.mBoss != null && this.mBoss.IsHitByExplosion(x, y, radius))
			{
				this.mBoss.ProximityBombActivated(x, y, radius);
			}
		}

		public void UserDied()
		{
			for (int i = 0; i < this.mEffects.size<Effect>(); i++)
			{
				this.mEffects[i].UserDied();
			}
		}

		public const int TARGET_BAR_SIZE = 330;

		public const int FRED_TONGUE_X = 541;

		public const int STARTING_TORCH_TEXT_ALPHA = 700;

		protected float[] mCloakedBossTextAlpha = new float[3];

		public List<DaisRock> mDaisRocks = new List<DaisRock>();

		public List<TorchLevelEgg> mEggs = new List<TorchLevelEgg>();

		public List<Wall> mMovingWallDefaults = new List<Wall>();

		public List<Effect> mEffects = new List<Effect>();

		protected bool mAllCurvesAtRolloutPoint;

		protected bool mHasReachedCruisingSpeed;

		protected float mCurGauntletMultPct;

		protected Transform mGlobalTranform = new Transform();

		private CompositionMgr mTorchCompMgr;

		public float mTorchBossX;

		public float mTorchBossY;

		public float mTorchBossDestX;

		public float mTorchBossDestY;

		public float mTorchBossVX;

		public float mTorchBossVY;

		public float mTorchDaisScale;

		public int mChallengePoints;

		public int mChallengeAcePoints;

		public int mCloakClapFrame;

		public PIEffect mCloakPoof;

		public FrogFlyOff mFrogFlyOff;

		public List<PowerupRegion> mPowerupRegions = new List<PowerupRegion>();

		public List<Torch> mTorches = new List<Torch>();

		public List<string> mEffectNames = new List<string>();

		public List<EffectParams> mEffectParams = new List<EffectParams>();

		public List<TreasurePoint> mTreasurePoints = new List<TreasurePoint>();

		public string mId = "";

		public string mDisplayName = "";

		public int mDisplayNameId = -1;

		public string mPopupText = "";

		public string mImagePath = "";

		public string mSoundscapeId = "";

		public MirrorType mMirrorType;

		public CurveMgr[] mCurveMgr = new CurveMgr[4];

		public float[] mCurveSkullAngleOverrides = new float[4];

		public HoleMgr mHoleMgr;

		public List<TunnelData> mTunnelData = new List<TunnelData>();

		public List<Wall> mWalls = new List<Wall>();

		public Boss mBoss;

		public Boss mSecondaryBoss;

		public Boss mOrgBoss;

		public Gun mFrog;

		public Board mBoard;

		public GameApp mApp;

		public SharedImageRef mBossIntroBG;

		public string mPreviewText = "";

		public int mPreviewTextId = -1;

		public LillyPadImageInfo[] mFrogImages = new LillyPadImageInfo[5];

		public bool mCanDrawBoss;

		public int mTorchStageState;

		public int mTorchStageTimer;

		public float mTorchStageAlpha;

		public int mTorchStageShakeAmt;

		public int mEndSequence;

		public int mIndex;

		public bool mOffscreenClearBonus;

		public bool mNoBackground;

		public bool mFinalLevel;

		public bool mBGFromPSD;

		public float mPotPct;

		public float mFireSpeed;

		public float mHurryToRolloutAmt;

		public bool mDoTorchCrap;

		public bool mHasDoneTorchCrap;

		public float mTorchTextAlpha;

		public bool mDrawCurves;

		public bool mSuckMode;

		public bool mIsEndless;

		public bool mLoopAtEnd;

		public bool mDoingPadHints;

		public bool mNoFlip;

		public bool mSliderEdgeRotate;

		public bool mIronFrog;

		public int mReloadDelay;

		public int mNumCurves;

		public int mNumFrogPoints;

		public int mCurFrogPoint;

		public int[] mFrogX = new int[5];

		public int[] mFrogY = new int[5];

		public int mBarWidth;

		public int mBarHeight;

		public int mTreasureFreq;

		public int mParTime;

		public int mMoveType;

		public int mMoveSpeed;

		public int mUpdateCount;

		public int mTimer;

		public int mTimeToComplete;

		public int mInvertMouseTimer;

		public int mMaxInvertMouseTimer;

		public int mTempSpeedupTimer;

		public int mBossFreezePowerupTime;

		public int mFrogShieldPowerupCount;

		public int mStartingGauntletLevel;

		public int mTorchTimer;

		public int mFurthestBallDistance;

		public int mIntroTorchDelay;

		public int mIntroTorchIndex;

		public int mGauntletCurTime;

		public int mGauntletMultipliersEarned;

		public int mNumGauntletBallsBroke;

		public int mGauntletCurNumForMult;

		public int mCurMultiplierTimeLeft;

		public int mMaxMultiplierTime;

		public float mGauntletTimeRedAmt;

		public int mZone;

		public int mNum;

		public int mPostZumaTimeCounter;

		public float mPostZumaTimeSpeedInc;

		public float mPostZumaTimeSlowInc;

		public string mBossBGID = "";

		public int m_OriginX = -1;

		public int m_OriginY = -1;

		public bool m_canGetAchievementNoMove;

		public bool m_canGetAchievementNoJump;

		public bool mHaveReachedTarget;

		public int mCurBarSize;

		public int mCurBarSizeInc;

		public int mTargetBarSize;

		public int mZumaBallFrame;

		public float mBarLightness;

		public int mZumaPulseUCStart;

		public float mGingerMouthX;

		public float mGingerMouthVX;

		public float mGingerMouthXStart;

		public float mFredMouthX;

		public float mFredMouthVX;

		public float mFredMouthXStart;

		public float mFredTongueX;

		public float mFredTongueVX;

		public float mZumaBallPct;

		public int mZumaBarState;

		public float mGoldBallXOff;

		public int mBarXOffset;

		public int mZumaBarX;

		public int mZumaBarWidth;

		private static int last_sound_idx;

		private static bool torchChangeState;

		public enum TorchState
		{
			TorchState_FlyIn,
			TorchState_Bounce,
			TorchState_TossEgg,
			TorchState_Disappear,
			TorchState_RaiseDais,
			TorchState_FrogFlyIn,
			TorchState_IntroDone,
			TorchState_ShakeDais,
			TorchState_FrogDisappear,
			TorchState_DoFade,
			TorchState_DropInToNextLevel,
			TorchState_CloakedBossAppear,
			TorchState_CloakedBossTransform,
			TorchState_Complete
		}
	}
}
