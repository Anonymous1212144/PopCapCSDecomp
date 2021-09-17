using System;
using System.Collections.Generic;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.UI;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Sound;

namespace BejeweledLivePlus
{
	public class SpeedBoard : Board
	{
		public SpeedBoard()
		{
			this.mShowPointMultiplier = true;
			this.mTimeFXManager = new EffectsManager(this);
			this.mLevelBarSizeBias = ConstantsWP.SPEEDBOARD_LEVELBAR_SIZE_BIAS;
			this.mHumSoundEffect = GlobalMembers.gApp.mSoundManager.GetSoundInstance(GlobalMembersResourcesWP.SOUND_LIGHTNING_HUMLOOP);
			if (this.mHumSoundEffect != null && GlobalMembers.gApp.mSfxVolume > 0.0)
			{
				this.mHumSoundEffect.SetVolume(0.0);
			}
			this.mUiConfig = Board.EUIConfig.eUIConfig_StandardNoReplay;
			this.mDrawingOverlay = false;
			this.mCurTempo = 0f;
			this.mParams["Title"] = "Lightning";
		}

		public override void Dispose()
		{
			this.mTimeFXManager = null;
			if (this.mHumSoundEffect != null)
			{
				this.mHumSoundEffect.Release();
			}
			base.Dispose();
		}

		public override string GetSavedGameName()
		{
			return "speed.sav";
		}

		public override string GetMusicName()
		{
			return "Speed";
		}

		public override bool SaveGameExtra(Serialiser theBuffer)
		{
			int chunkBeginLoc = theBuffer.WriteGameChunkHeader(GameChunkId.eChunkSpeedBoard);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoard5SecChance, this.m5SecChance.mChance);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoard5SecChanceStep, this.m5SecChance.mSteps);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoard5SecChanceLastRoll, this.m5SecChance.mLastRoll);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoard10SecChance, this.m10SecChance.mChance);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoard10SecChanceStep, this.m10SecChance.mSteps);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoard10SecChanceLastRoll, this.m10SecChance.mLastRoll);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoardBonusTime, this.mBonusTime);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoardBonusTimeDisp, this.mBonusTimeDisp);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoardGameTicks, this.mGameTicksF);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoardCollectorExtendPct, this.mCollectorExtendPct);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoardPanicScalePct, this.mPanicScalePct);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoardTimeScaleOverride, this.mTimeScaleOverride);
			theBuffer.WriteValuePair(Serialiser.PairID.SpeedBoardTotalBonusTime, this.mTotalBonusTime);
			theBuffer.FinalizeGameChunkHeader(chunkBeginLoc);
			return !this.mDidTimeUp;
		}

		public override void LoadGameExtra(Serialiser theBuffer)
		{
			int num = 0;
			GameChunkHeader header = new GameChunkHeader();
			if (!theBuffer.CheckReadGameChunkHeader(GameChunkId.eChunkSpeedBoard, header, out num))
			{
				return;
			}
			theBuffer.ReadValuePair(out this.m5SecChance.mChance);
			theBuffer.ReadValuePair(out this.m5SecChance.mSteps);
			theBuffer.ReadValuePair(out this.m5SecChance.mLastRoll);
			theBuffer.ReadValuePair(out this.m10SecChance.mChance);
			theBuffer.ReadValuePair(out this.m10SecChance.mSteps);
			theBuffer.ReadValuePair(out this.m10SecChance.mLastRoll);
			theBuffer.ReadValuePair(out this.mBonusTime);
			theBuffer.ReadValuePair(out this.mBonusTimeDisp);
			theBuffer.ReadValuePair(out this.mGameTicksF);
			theBuffer.ReadValuePair(out this.mCollectorExtendPct);
			theBuffer.ReadValuePair(out this.mPanicScalePct);
			theBuffer.ReadValuePair(out this.mTimeScaleOverride);
			theBuffer.ReadValuePair(out this.mTotalBonusTime);
		}

		public override int GetHintTime()
		{
			return 5;
		}

		public override void Init()
		{
			base.Init();
			this.mPreHurrahPoints = 0;
			this.mSpeedTier = 0;
			this.mPrevPointsGoal = 0;
			this.mPMDropLevel = 0;
			this.mPointsGoal = 2500;
			this.mDoThirtySecondVoice = false;
			this.mUsePM = false;
			this.mDidTimeUp = false;
			this.mTimeUpCount = 0;
			this.mBonusTime = 0;
			this.mTotalBonusTime = 0;
			this.mBonusTimeDisp = 0f;
			this.mTotalGameTicks = 0;
			this.mReadyForDrop = true;
			this.mWantGemsCleared = 0;
			this.mDropGameTick = 0;
			this.mBonusPenalty = 0.0;
			this.mTimedPenaltyAmnesty = GlobalMembers.M(250);
			this.mUsePM = GlobalMembers.sexyatoi(this.mParams, "UsePM") != 0;
			this.mPointsGoalStart = (this.mPointsGoal = GlobalMembers.sexyatoi(this.mParams, "PointsGoalStart"));
			this.mAddPointsGoalPerLevel = GlobalMembers.sexyatoi(this.mParams, "AddPointsGoalPerLevel");
			this.mPointsGoalAddPower = (double)GlobalMembers.sexyatof(this.mParams, "PointsGoalAddPower");
			this.mTimeStart = GlobalMembers.sexyatoi(this.mParams, "TimeStart");
			this.mTimeChange = GlobalMembers.sexyatoi(this.mParams, "TimeChange");
			this.m5SecChance.Init(this.mParams["5SecChanceCurve"]);
			this.m10SecChance.Init(this.mParams["10SecChanceCurve"]);
			this.m5SecChanceDec = GlobalMembers.sexyatof(this.mParams, "5SecChanceDec");
			this.m10SecChanceDec = GlobalMembers.sexyatof(this.mParams, "10SecChanceDec");
			this.mTimedPenaltyVel = GlobalMembers.sexyatof(this.mParams, "TimedPenaltyVelocity");
			this.mTimedPenaltyAccel = GlobalMembers.sexyatof(this.mParams, "TimedPenaltyAccel");
			this.mTimedPenaltyJerk = GlobalMembers.sexyatof(this.mParams, "TimedPenaltyJerk");
			this.mTimedLevelBonus = GlobalMembers.sexyatof(this.mParams, "TimedLevelBonus");
			this.mTimeStep = GlobalMembers.sexyatof(this.mParams, "TimeStep");
			this.mLevelTimeStep = GlobalMembers.sexyatof(this.mParams, "LevelTimeStep");
			this.mPointMultiplierStart = GlobalMembers.sexyatof(this.mParams, "PointMultiplierStart");
			this.mAddPointMultiplierPerLevel = GlobalMembers.sexyatof(this.mParams, "AddPointMultiplierPerLevel");
			this.mUseCheckpoints = this.mPointsGoalStart > 0 && this.mTimeStart > 0;
			this.mMaxTicksLeft = 6000;
			this.mPanicScalePct = 0f;
			this.mGameTicksF = 0f;
			this.mTimeScaleOverride = 0f;
			this.mCurTempo = 0f;
			this.mCollectedTimeAlpha.SetConstant(1.0);
			this.mCollectorExtendPct.SetConstant(0.0);
			this.mLastHurrahAlpha.SetConstant(0.0);
		}

		public override int GetBoardY()
		{
			return GlobalMembers.RS(ConstantsWP.SPEEDBOARD_BOARD_Y);
		}

		public override void GameOverExit()
		{
			this.SubmitHighscore();
			GlobalMembers.gApp.DoGameDetailMenu(GameMode.MODE_LIGHTNING, GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME);
		}

		public override void GameOverAnnounce()
		{
			new Announcement(this, GlobalMembers._ID("TIME UP", 478));
			GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_TIMEUP);
		}

		public override void PieceTallied(Piece thePiece)
		{
			if (thePiece.IsFlagSet(131072U))
			{
				base.AddToStat(34, 1, thePiece.mMoveCreditId);
				int num = this.mBonusTime;
				this.mBonusTime += thePiece.mCounter;
				this.mTotalBonusTime += thePiece.mCounter;
				SpeedCollectEffect speedCollectEffect = SpeedCollectEffect.alloc(this, new Point((int)thePiece.CX(), (int)thePiece.CY()), new Point(ConstantsWP.SPEEDBOARD_COLLECT_EFFECT_DEST_X, ConstantsWP.SPEEDBOARD_COLLECT_EFFECT_DEST_Y), Res.GetImageByID(ResourceId.IMAGE_QUEST_DIG_BOARD_NUGGETPART1_ID + SexyFramework.Common.Rand() % GlobalMembers.M(9)), thePiece.mCounter, (float)GlobalMembers.MS(1.0));
				this.mTimeFXManager.AddEffect(speedCollectEffect);
				speedCollectEffect.Init(thePiece);
				if (!thePiece.IsFlagSet(4U))
				{
					thePiece.mAlpha.SetConstant(0.0);
				}
				else
				{
					thePiece.ClearFlag(131072U);
				}
				if (thePiece.mCounter == 5)
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_TIMEBONUS_5, (int)((double)base.GetPanPosition(thePiece) * GlobalMembers.M(0.5)), 1.0, GlobalMembers.M(0.1) * (double)this.mBonusTime);
				}
				else
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_TIMEBONUS_10, (int)((double)base.GetPanPosition(thePiece) * GlobalMembers.M(0.5)), 1.0, GlobalMembers.M(0.1) * (double)this.mBonusTime);
				}
				int num2 = Math.Max(0, this.mBonusTime - 60);
				if (num2 > 0)
				{
					this.AddPoints((int)thePiece.CX(), (int)thePiece.CY(), thePiece.mCounter * GlobalMembers.M(50), GlobalMembers.gGemColors[thePiece.mColor], (uint)thePiece.mMatchId, true, true, thePiece.mMoveCreditId);
				}
				string text = string.Format("+{0:d} sec", thePiece.mCounter);
				text = GlobalMembers._ID(text, 479);
				new Points(GlobalMembers.gApp, GlobalMembersResources.FONT_HEADER, text, (int)thePiece.CX(), (int)thePiece.CY(), GlobalMembers.M(1f), 0, GlobalMembers.gGemColors[thePiece.mColor], GlobalMembers.M(-1))
				{
					mDestScale = GlobalMembers.M(1.5f),
					mScaleDifMult = GlobalMembers.M(0.2f),
					mScaleDampening = GlobalMembers.M(0.8f)
				}.mDY *= GlobalMembers.M(0.2f);
				if (thePiece.IsFlagSet(4U))
				{
					thePiece.mCounter = 0;
				}
			}
			base.PieceTallied(thePiece);
		}

		public override Rect GetLevelBarRect()
		{
			return new Rect((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME_ID)), GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME.mWidth, GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME.mHeight);
		}

		public override Rect GetCountdownBarRect()
		{
			return new Rect((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK_ID)), GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK.mWidth, GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK.mHeight);
		}

		public override int GetTimeDrawX()
		{
			Rect levelBarRect = this.GetLevelBarRect();
			levelBarRect.mX += GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_TIMER.mWidth / 2;
			levelBarRect.mWidth -= GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_TIMER.mWidth + ConstantsWP.SPEEDBOARD_TIMEDRAW_X_OFFSET;
			return levelBarRect.mX + (int)((float)levelBarRect.mWidth * this.mCountdownBarPct);
		}

		public override bool CanTimeUp()
		{
			return this.mBonusTime != 0 || base.CanTimeUp();
		}

		public override int GetTicksLeft()
		{
			if (this.mInUReplay)
			{
				return this.mUReplayTicksLeft;
			}
			int timeLimit = this.GetTimeLimit();
			if (timeLimit == 0)
			{
				return -1;
			}
			int num = GlobalMembers.M(250);
			int num2 = (int)Math.Min((float)timeLimit * 100f, Math.Max(0f, (float)timeLimit * 100f - Math.Max(0f, this.mGameTicksF - (float)num)));
			return Math.Min(this.mMaxTicksLeft, num2);
		}

		public override bool WantsTutorial(int theTutorialFlag)
		{
			return base.WantsTutorial(theTutorialFlag);
		}

		public override int GetTimeLimit()
		{
			return 60;
		}

		public override int GetLevelPoints()
		{
			return this.mPointsGoalStart + this.mAddPointsGoalPerLevel * this.mSpeedTier;
		}

		public override int GetLevelPointsTotal()
		{
			return this.mLevelPointsTotal - (int)this.mBonusPenalty;
		}

		public override void LevelUp()
		{
			this.mSpeedTier++;
			this.mBonusPenalty = 0.0;
			this.mLevelPointsTotal = 0;
			this.mTimedPenaltyAmnesty = GlobalMembers.M(500);
			double num = (double)this.mTimedLevelBonus;
			this.mTimedPenaltyVel = (float)Math.Max(0.0, (double)this.mTimedPenaltyVel - (double)this.mTimedPenaltyAccel * num);
			this.mTimedPenaltyAccel = (float)Math.Max(0.0, (double)this.mTimedPenaltyAccel - (double)this.mTimedPenaltyJerk * num);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BACKGROUND_CHANGE);
		}

		public override int WantExpandedTopWidget()
		{
			return 1;
		}

		public override string GetTopWidgetButtonText()
		{
			return base.GetTopWidgetButtonText();
		}

		public override float GetModePointMultiplier()
		{
			return 5f;
		}

		public override float GetRankPointMultiplier()
		{
			return 5.66666651f;
		}

		public override void GameOver()
		{
			this.GameOver(false);
		}

		public override void GameOver(bool visible)
		{
			if (this.mWantLevelup || this.mHyperspace != null)
			{
				return;
			}
			if (!this.mTimeFXManager.IsEmpty())
			{
				return;
			}
			if (this.mBonusTime == 0 && this.mPointsBreakdown.size<List<int>>() <= this.mPointMultiplier)
			{
				this.AddPointBreakdownSection();
			}
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					if (piece != null && piece.IsFlagSet(131072U))
					{
						if (this.mBonusTime == 0)
						{
							Points points = this.AddPoints((int)piece.CX(), (int)piece.CY(), piece.mCounter * GlobalMembers.M(50), GlobalMembers.gGemColors[piece.mColor], (uint)piece.mMatchId, true, true, piece.mMoveCreditId);
							points.mTimer *= GlobalMembers.M(1.5f);
						}
						else if (piece.mCounter >= 10)
						{
							this.Laserify(piece);
						}
						else
						{
							this.Flamify(piece);
						}
						piece.ClearFlag(131072U);
						piece.mCounter = 0;
					}
				}
			}
			if (this.mBonusTime > 0)
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_LIGHTNING_ENERGIZE);
				GlobalMembers.gApp.mCurveValCache.GetCurvedValMult(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_COLLECTOR_EXTEND_PCT_A, this.mCollectorExtendPct);
				this.m5SecChance.Step(this.mLevelTimeStep);
				this.m10SecChance.Step(this.mLevelTimeStep);
				this.m5SecChance.mChance.IncInVal();
				this.m10SecChance.mChance.IncInVal();
				this.mTimeExpired = false;
				GlobalMembers.gApp.PlaySample(Res.GetSoundByID(ResourceId.SOUND_MULTIPLIER_UP2_1_ID + Math.Min(3, this.mPointMultiplier - 1)));
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_PREV_POINT_MULT_ALPHA, this.mPrevPointMultAlpha);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_POS_PCT_2, this.mPointMultPosPct);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_SCALE_2, this.mPointMultScale, this.mPointMultPosPct);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_ALPHA_2, this.mPointMultAlpha, this.mPointMultPosPct);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_Y_ADD_2, this.mPointMultYAdd, this.mPointMultPosPct);
				this.mPointMultTextMorph.SetConstant(0.0);
				this.mPointMultiplier++;
				this.AddPointBreakdownSection();
				this.mMaxTicksLeft = this.mBonusTime * 100;
				this.mGameTicks = Math.Max(0, (60 - this.mBonusTime) * 100 + GlobalMembers.M(0));
				this.mGameTicksF = (float)this.mGameTicks;
				this.mBonusTime = 0;
				this.mTimeScaleOverride = 0f;
				LightningBarFillEffect lightningBarFillEffect = LightningBarFillEffect.alloc();
				lightningBarFillEffect.mOverlay = true;
				this.mPostFXManager.AddEffect(lightningBarFillEffect);
				return;
			}
			if (this.mSpeedBonusFlameModePct > 0f)
			{
				return;
			}
			int num = 0;
			this.mCursorSelectPos = new Point(-1, -1);
			int num2 = 0;
			if (!this.mDidTimeUp)
			{
				this.mPreHurrahPoints = this.mPoints;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_COLLECTED_TIME_ALPHA, this.mCollectedTimeAlpha);
				GlobalMembers.gApp.mMusic.PlaySongNoDelay(12, false);
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BOMB_EXPLODE, 0, (double)GlobalMembers.M(1), GlobalMembers.M(-2.0));
				GlobalMembers.gApp.PlayVoice(new VoicePlayArgs(GlobalMembersResourcesWP.SOUND_VOICE_TIMEUP, 0, 1.0, -2, new SoundPlayConditionWaitUpdates(GlobalMembersResourcesWP.SOUND_BOMB_EXPLODE)));
				this.mDidTimeUp = true;
				new Announcement(this, GlobalMembers._ID("TIME UP", 480));
			}
			if (this.mSpeedBonusCount > 0)
			{
				this.EndSpeedBonus();
			}
			bool flag = false;
			for (int k = 0; k < 8; k++)
			{
				for (int l = 0; l < 8; l++)
				{
					Piece piece2 = this.mBoard[k, l];
					if (piece2 != null)
					{
						flag |= piece2.IsFlagSet(1024U);
						if (piece2.IsFlagSet(1U) || piece2.IsFlagSet(2U) || piece2.IsFlagSet(4U) || piece2.IsFlagSet(16U) || piece2.IsFlagSet(2048U) || piece2.IsFlagSet(4096U) || piece2.IsFlagSet(524288U) || piece2.IsFlagSet(131072U))
						{
							if (piece2.IsFlagSet(1024U))
							{
								piece2.mDestructing = true;
							}
							if (this.mTimeUpCount == 0)
							{
								piece2.mExplodeDelay = GlobalMembers.M(175) + num2 * GlobalMembers.M(25);
							}
							else
							{
								piece2.mExplodeDelay = GlobalMembers.M(25) + num2 * GlobalMembers.M(25);
							}
							num2++;
							num++;
						}
					}
				}
			}
			if (num2 == 0)
			{
				for (int m = 0; m < 8; m++)
				{
					for (int n = 0; n < 8; n++)
					{
						Piece piece3 = this.mBoard[m, n];
						if (piece3 != null && piece3.IsFlagSet(1024U) && !piece3.mTallied)
						{
							if (piece3.IsFlagSet(1024U))
							{
								piece3.mDestructing = true;
							}
							this.TallyPiece(piece3, true);
							piece3.mAlpha.SetConstant(1.0);
							piece3.ClearFlag(1024U);
							num2++;
						}
					}
				}
			}
			if (num2 > 0 && this.mLastHurrahAlpha == 0.0)
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_LAST_HURRAH_ALPHA_A, this.mLastHurrahAlpha);
				this.mLastHurrahUpdates = 0;
			}
			if (num2 == 0)
			{
				base.GameOver(false);
				if (this.mLastHurrahAlpha > 0.0)
				{
					this.mGameOverCount = GlobalMembers.M(200);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_LAST_HURRAH_ALPHA_B, this.mLastHurrahAlpha);
				}
			}
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, theForceAdd, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, -1, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, true, -1, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, true, true, -1, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, uint.MaxValue, true, true, -1, false, 1);
		}

		public override Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd, int thePointType)
		{
			return base.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, theForceAdd, thePointType);
		}

		public override bool WantSpecialPiece(List<Piece> thePieceVector)
		{
			if (this.mUsePM)
			{
				return this.mPMDropLevel < this.mSpeedTier && this.mSpeedTier < 8;
			}
			if (this.mTimeStart == 0)
			{
				return this.mPMDropLevel < this.mSpeedTier && this.mSpeedTier < 8;
			}
			bool flag = false;
			return this.mReadyForDrop && this.mWantGemsCleared != 0 && !this.mDidTimeUp && flag;
		}

		public override bool WantWarningGlow()
		{
			return this.WantWarningGlow(false);
		}

		public new bool WantWarningGlow(bool forSound)
		{
			if (forSound)
			{
				return this.mBonusTime <= 0 && base.WantWarningGlow();
			}
			return base.WantWarningGlow();
		}

		public override float GetLevelPct()
		{
			int levelPoints = this.GetLevelPoints();
			if (levelPoints > 0)
			{
				int levelPointsTotal = this.GetLevelPointsTotal();
				float num = Math.Min(1f, Math.Max(0f, 0.5f + (float)levelPointsTotal / (float)levelPoints * 0.5f));
				if (this.mDidTimeUp)
				{
					num = 0f;
				}
				if (num <= 0f && base.IsBoardStill() && this.mDeferredTutorialVector.size<DeferredTutorial>() == 0 && this.mGameOverCount == 0)
				{
					this.mTimeExpired = true;
					this.GameOver();
				}
				int num2 = (int)(num * (float)GlobalMembers.M(4000));
				int num3 = GlobalMembers.M(35) + (int)((float)num2 * GlobalMembers.M(0.1f));
				if (this.mUpdateCnt - this.mLastWarningTick >= num3 && num2 > 0 && num2 <= GlobalMembers.M(1000))
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COUNTDOWN_WARNING, 0, Math.Min(1.0, GlobalMembers.M(0.5) - (double)((float)num2 * GlobalMembers.M(0.0005f))));
					this.mLastWarningTick = this.mUpdateCnt;
				}
				return num;
			}
			int timeLimit = this.GetTimeLimit();
			float num4 = 0f;
			bool flag = this.mUpdateCnt % 20 == 0;
			if (timeLimit != 0)
			{
				num4 = Math.Max(0f, (float)this.GetTicksLeft() / ((float)timeLimit * 100f));
				if (num4 <= 0f && base.IsBoardStill() && this.mDeferredTutorialVector.size<DeferredTutorial>() == 0 && this.mGameOverCount == 0)
				{
					this.mTimeExpired = true;
					this.GameOver();
				}
				int ticksLeft = this.GetTicksLeft();
				int num5 = GlobalMembers.M(35) + (int)((float)ticksLeft * GlobalMembers.M(0.1f));
				if (this.mUseCheckpoints)
				{
					if (this.mUpdateCnt - this.mLastWarningTick >= num5 && ticksLeft > 0 && ticksLeft <= GlobalMembers.M(1000))
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COUNTDOWN_WARNING, 0, Math.Min(1.0, GlobalMembers.M(0.5) - (double)((float)ticksLeft * GlobalMembers.M(0.0005f))));
						this.mLastWarningTick = this.mUpdateCnt;
					}
				}
				else if (!this.mUserPaused && this.mUpdateCnt - this.mLastWarningTick >= num5 && ticksLeft > 0 && this.WantWarningGlow(true))
				{
					int num6 = ((this.GetTimeLimit() > 60) ? 1500 : 1000);
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COUNTDOWN_WARNING, 0, Math.Min(1.0, GlobalMembers.M(0.5) - (double)((float)ticksLeft / (float)num6 / 2f)));
					this.mLastWarningTick = this.mUpdateCnt;
				}
				if (ticksLeft == 3000 && this.mDoThirtySecondVoice)
				{
					flag = true;
					GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_THIRTYSECONDS);
					if (this.mInUReplay)
					{
						this.mUReplayTicksLeft--;
					}
				}
			}
			if (this.mUseCheckpoints)
			{
				num4 = (float)(this.mPoints - this.mPrevPointsGoal) / (float)(this.mPointsGoal - this.mPrevPointsGoal);
			}
			if (flag && this.WriteUReplayCmd(7))
			{
				this.mUReplayBuffer.WriteShort((short)this.GetTicksLeft());
			}
			return num4;
		}

		public override bool DropSpecialPiece(List<Piece> thePieceVector)
		{
			if (this.mUsePM)
			{
				int num = (int)(this.mRand.Next() % (uint)thePieceVector.size<Piece>());
				for (int i = 0; i < 7; i++)
				{
					thePieceVector[num].mColor = (int)(this.mRand.Next() % 7U);
					int num2 = 0;
					for (int j = 0; j < 8; j++)
					{
						for (int k = 0; k < 8; k++)
						{
							Piece piece = this.mBoard[j, k];
							if (piece != null && piece.GetScreenY() > 0f && piece.mColor == thePieceVector[num].mColor)
							{
								num2++;
							}
						}
					}
					if (num2 > 3)
					{
						break;
					}
				}
				thePieceVector[num].SetFlag(16U);
				if (this.WantsTutorial(4))
				{
					base.DeferTutorialDialog(4, thePieceVector[num]);
				}
				this.mPMDropLevel++;
			}
			else
			{
				int num3 = (int)(this.mRand.Next() % (uint)thePieceVector.size<Piece>());
				for (int l = 0; l < 7; l++)
				{
					thePieceVector[num3].mColor = (int)(this.mRand.Next() % 7U);
					int num4 = 0;
					for (int m = 0; m < 8; m++)
					{
						for (int n = 0; n < 8; n++)
						{
							Piece piece2 = this.mBoard[m, n];
							if (piece2 != null && piece2.mY > 0f && piece2.mColor == thePieceVector[num3].mColor)
							{
								num4++;
							}
						}
					}
					if (num4 > 3)
					{
						break;
					}
				}
				base.Blastify(thePieceVector[num3]);
				this.mDropGameTick = this.mGameTicks;
				this.mReadyForDrop = false;
				this.mWantGemsCleared = 0;
				this.mPMDropLevel++;
			}
			return true;
		}

		public override bool PiecesDropped(List<Piece> thePieceVector)
		{
			int num = 0;
			if (this.mGameTicks > 100 && !this.mTimeExpired && this.GetTicksLeft() > 0)
			{
				for (int i = 0; i < thePieceVector.size<Piece>(); i++)
				{
					this.m5SecChance.Step();
					this.m10SecChance.Step();
					if (this.m10SecChance.Check(this.mRand.Next() % 100000U / 100000f))
					{
						num = 10;
					}
					else if (this.m5SecChance.Check(this.mRand.Next() % 100000U / 100000f))
					{
						num = 5;
					}
				}
			}
			if (num > 0)
			{
				int num2 = (int)(this.mRand.Next() % (uint)thePieceVector.size<Piece>());
				Piece piece = thePieceVector[num2];
				if (piece.mFlags == 0)
				{
					piece.SetFlag(131072U);
					piece.mCounter = num;
					base.StartTimeBonusEffect(piece);
					if (this.WantsTutorial(9))
					{
						base.DeferTutorialDialog(9, piece);
					}
					if (piece.mCounter == 5)
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_TIMEBONUS_APPEARS_5, base.GetPanPosition(piece));
					}
					else
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_TIMEBONUS_APPEARS_10, base.GetPanPosition(piece));
					}
				}
			}
			return true;
		}

		public void TimeCollected(int theTimeCollected)
		{
			if (theTimeCollected <= 5)
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_LIGHTNING_TUBE_FILL_5);
			}
			else
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_LIGHTNING_TUBE_FILL_10);
			}
			double mOutMin = this.mCollectorExtendPct;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eSPEED_BOARD_COLLECTOR_EXTEND_PCT_B, this.mCollectorExtendPct);
			this.mCollectorExtendPct.mOutMin = mOutMin;
			this.mCollectorExtendPct.mOutMax = Math.Min(1.0, (double)this.mBonusTime / 60.0);
		}

		public override void Update()
		{
			int mGameTicks = this.mGameTicks;
			this.mLastHurrahAlpha.IncInVal();
			this.mLastHurrahUpdates++;
			base.Update();
			if (this.mUpdateCnt % 10 == 0 && this.mHumSoundEffect != null)
			{
				this.mHumSoundEffect.SetVolume(this.mCollectorExtendPct * GlobalMembers.M(0.1));
			}
			if (this.mGameTicks != mGameTicks)
			{
				if (this.mTimeScaleOverride == 0f)
				{
					float num = Math.Min(1f, GlobalMembers.M(0.7f) + (float)this.GetTicksLeft() / GlobalMembers.M(600f) * GlobalMembers.M(0.3f));
					this.mGameTicksF += num;
				}
				else
				{
					this.mGameTicksF += this.mTimeScaleOverride;
				}
				this.m5SecChance.Step(this.mTimeStep / 100f);
				this.m10SecChance.Step(this.mTimeStep / 100f);
				this.mTotalGameTicks++;
				if (!this.WantWarningGlow(true))
				{
					int ticksLeft = this.GetTicksLeft();
					if (ticksLeft % 100 == 0 && ticksLeft > 0 && ticksLeft <= GlobalMembers.M(800) && ticksLeft != this.mMaxTicksLeft)
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_TICK, 0, GlobalMembers.M(0.3), (double)((ticksLeft / 100 % 2 == 0) ? 0f : GlobalMembers.M(-5f)));
					}
				}
				if (this.mGameTicks % GlobalMembers.M(150) == 0)
				{
					for (int i = 0; i < 8; i++)
					{
						for (int j = 0; j < 8; j++)
						{
							Piece piece = this.mBoard[i, j];
							if (piece != null && piece.IsFlagSet(262144U))
							{
								piece.mCounter--;
								if (piece.mCounter <= 0)
								{
									piece.ClearFlag(131072U);
									piece.ClearFlag(262144U);
								}
							}
						}
					}
				}
			}
			if (!this.mDidTimeUp)
			{
				this.mPreHurrahPoints = this.mPoints;
			}
			if (this.mDidTimeUp)
			{
				this.mTimeUpCount++;
			}
			float num2 = Math.Min(this.GetLevelPct() + 0.65f, 1f);
			this.mBonusTimeDisp += ((float)this.mBonusTime - this.mBonusTimeDisp) / 50f;
			if (this.mTimedPenaltyAmnesty > 0)
			{
				this.mTimedPenaltyAmnesty--;
			}
			else
			{
				this.mBonusPenalty += (double)(this.mTimedPenaltyVel * num2) / 100.0;
				this.mTimedPenaltyVel += this.mTimedPenaltyAccel * num2 / 100f;
				this.mTimedPenaltyAccel += this.mTimedPenaltyJerk * num2 / 100f;
			}
			if (this.mWantGemsCleared == 0)
			{
				this.mWantGemsCleared = GlobalMembers.M(20);
			}
			if (this.mGameTicks % GlobalMembers.M(400) == 0 && mGameTicks != this.mGameTicks)
			{
				this.mWantGemsCleared = Math.Max(5, this.mWantGemsCleared - (int)(this.mRand.Next() % 5U));
			}
			if (this.mUseCheckpoints && this.mPoints > this.mPointsGoal)
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BACKGROUND_CHANGE);
				this.mSpeedTier++;
				this.mPrevPointsGoal = this.mPointsGoal;
				if (this.mUsePM)
				{
					this.mPointsGoal += (int)((double)this.mPointsGoalStart + (double)this.mAddPointsGoalPerLevel * Math.Pow((double)this.mSpeedTier, this.mPointsGoalAddPower)) * (this.mSpeedTier + 1);
				}
				else
				{
					this.mPointsGoal += (int)((double)this.mPointsGoalStart + (double)this.mAddPointsGoalPerLevel * Math.Pow((double)this.mSpeedTier, this.mPointsGoalAddPower));
				}
				this.mGameTicks = 0;
			}
			Math.Pow(this.mCollectorExtendPct, GlobalMembers.M(0.7));
			this.mTimeFXManager.Update();
			if (this.mBonusTime > 0)
			{
				this.mPanicScalePct = Math.Max(0f, this.mPanicScalePct - GlobalMembers.M(0.01f));
			}
			else
			{
				this.mPanicScalePct = Math.Min(1f, this.mPanicScalePct + GlobalMembers.M(0.01f));
			}
			if (this.mTimeUpCount > 0 && this.mTimeUpCount < GlobalMembers.M(100) && this.mUpdateCnt % GlobalMembers.M(3) == 0)
			{
				this.mX = (int)((double)(GlobalMembersUtils.GetRandFloat() * (float)(GlobalMembers.M(100) - this.mTimeUpCount) / (float)GlobalMembers.M(100)) * GlobalMembers.MS(12.0));
				this.mY = (int)((double)(GlobalMembersUtils.GetRandFloat() * (float)(GlobalMembers.M(100) - this.mTimeUpCount) / (float)GlobalMembers.M(100)) * GlobalMembers.MS(12.0));
			}
			if (this.mSpeedBonusFlameModePct > 0f)
			{
				if (this.mTimeScaleOverride == 0f)
				{
					int ticksLeft2 = this.GetTicksLeft();
					int num3 = (int)(800f * this.mSpeedBonusFlameModePct);
					if ((double)ticksLeft2 * GlobalMembers.M(1.3) < (double)num3)
					{
						this.mTimeScaleOverride = (float)ticksLeft2 / (float)num3;
						return;
					}
				}
			}
			else
			{
				this.mTimeScaleOverride = 0f;
			}
		}

		public override void RefreshUI()
		{
			base.RefreshUI();
			this.mHintButton.SetBorderGlow(true);
			this.mHintButton.Resize(ConstantsWP.BOARD_UI_HINT_BTN_X, ConstantsWP.SPEEDBOARD_HINT_BTN_Y, ConstantsWP.BOARD_UI_HINT_BTN_WIDTH, 0);
		}

		public override void DrawLevelBar(Graphics g)
		{
		}

		public override void KeyChar(char theChar)
		{
			base.KeyChar(theChar);
		}

		public override void DrawScore(Graphics g)
		{
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			string theString = SexyFramework.Common.CommaSeperate(this.mDispPoints);
			int num = this.mWidth / 2;
			int num2 = (int)((GlobalMembers.IMG_SYOFS(897) + (float)GlobalMembersResources.FONT_DIALOG.mAscent) / 2f - (float)this.mTransScoreOffsetY);
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), 0, Color.White);
			float mScaleX = g.mScaleX;
			float mScaleY = g.mScaleY;
			g.SetScale(ConstantsWP.BOARD_LEVEL_SCORE_SCALE, ConstantsWP.BOARD_LEVEL_SCORE_SCALE, (float)num, (float)(num2 - g.GetFont().GetAscent() / 2));
			g.WriteString(theString, num, num2);
			g.mScaleX = mScaleX;
			g.mScaleY = mScaleY;
		}

		public override void DrawFrame(Graphics g)
		{
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME_ID) + (float)this.mTransBoardOffsetX), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME_ID) - (float)this.mTransBoardOffsetY));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME_ID) + (float)this.mTransBoardOffsetX), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME_ID) - (float)this.mTransBoardOffsetY));
			Rect countdownBarRect = this.GetCountdownBarRect();
			int num = countdownBarRect.mY + countdownBarRect.mHeight / 2;
			if (this.mDrawingOverlay)
			{
				Color color = g.mPushedColorVector[g.mPushedColorVector.Count - 1];
				g.PopColorMult();
				g.SetColorizeImages(false);
				g.SetDrawMode(Graphics.DrawMode.Normal);
				Utils.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_TIMER, (float)this.GetTimeDrawX(), (float)num);
				g.SetColorizeImages(true);
				g.SetColor(color);
				g.PushColorMult();
				g.SetDrawMode(Graphics.DrawMode.Additive);
			}
			Utils.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_TIMER, (float)this.GetTimeDrawX(), (float)num);
			this.DrawFrame2(g);
		}

		public void DrawFrame2(Graphics g)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255f * this.GetBoardAlpha())));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER_ID)));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			g.SetColor(new Color(255, 255, 255, (int)((double)(255f * this.GetAlpha()) * this.mCollectedTimeAlpha)));
			int num = (int)(this.mCollectorExtendPct * 60.0 + 0.5);
			string theString = string.Format(GlobalMembers._ID("+{0:d}:{1:d2}", 481), num / 60, num % 60);
			g.WriteString(theString, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER_ID)) + GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER.mWidth / 2 + ConstantsWP.SPEEDBOARD_EXTRATIME_X_OFFSET, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER_ID)) + GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER.mHeight / 2 + ConstantsWP.SPEEDBOARD_EXTRATIME_Y_OFFSET, 0, 0);
			this.DrawLevelBar(g);
			if (this.mLastHurrahAlpha != 0.0)
			{
				g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
				g.SetColor(Color.FAlpha((float)(this.mLastHurrahAlpha * (double)this.GetPieceAlpha())));
				GlobalMembers.M(1.25f);
				Math.Sin((double)((float)this.mLastHurrahUpdates * GlobalMembers.M(0.06f)));
				GlobalMembers.M(0.15f);
				int num2 = 5;
				g.WriteString(GlobalMembers._ID("Last Hurrah", 482), GlobalMembers.S(base.GetBoardCenterX()), ConstantsWP.SPEEDBOARD_LAST_HURRAH_Y - num2);
			}
			g.SetColor(Color.FAlpha(this.GetAlpha()));
			this.DrawSpeedBonus(g);
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			this.mTimeFXManager.Draw(g);
			base.DrawOverlay(g, thePriority);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			base.DrawGameElements(g);
		}

		public override void DrawUI(Graphics g)
		{
			this.DrawMenuWidget(g);
			base.DrawUI(g);
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
			BejeweledLivePlusApp.UnloadContent("GamePlayQuest_Lightning");
		}

		public override void LoadContent(bool threaded)
		{
			base.LoadContent(threaded);
			BejeweledLivePlusApp.LoadContent("GamePlayQuest_Lightning");
			base.ConfigureBarEmitters();
		}

		public override void SubmitHighscore()
		{
			HighScoreTable orCreateTable = GlobalMembers.gApp.mHighScoreMgr.GetOrCreateTable(GlobalMembers.gApp.GetModeHeading(GameMode.MODE_LIGHTNING));
			if (orCreateTable.Submit(GlobalMembers.gApp.mProfile.mProfileName, this.mPoints, GlobalMembers.gApp.mProfile.GetProfilePictureId()))
			{
				GlobalMembers.gApp.SaveHighscores();
			}
		}

		public override void DrawCountdownBar(Graphics g)
		{
			if (this.mOffsetY != 0)
			{
				g.Translate(0, this.mOffsetY);
			}
			g.SetColorizeImages(true);
			float num = (float)Math.Pow((double)this.GetBoardAlpha(), 4.0);
			g.SetColor(new Color(255, 255, 255, (int)(this.GetBoardAlpha() * (float)GlobalMembers.M(255))));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK_ID)));
			g.SetColor(new Color(GlobalMembers.M(64), GlobalMembers.M(32), GlobalMembers.M(8), (int)(num * (float)GlobalMembers.M(255))));
			if (this.WantWarningGlow())
			{
				Color warningGlowColor = this.GetWarningGlowColor();
				if (warningGlowColor.mAlpha > 0)
				{
					g.PushState();
					g.SetDrawMode(Graphics.DrawMode.Additive);
					g.SetColor(warningGlowColor);
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME_ID)), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME_ID)));
					g.PopState();
				}
			}
			Rect countdownBarRect = this.GetCountdownBarRect();
			countdownBarRect.mWidth = (int)(this.mCountdownBarPct * (float)countdownBarRect.mWidth + (float)this.mLevelBarSizeBias);
			g.FillRect(countdownBarRect);
			if (this.mLevelBarBonusAlpha > 0.0)
			{
				Rect countdownBarRect2 = this.GetCountdownBarRect();
				countdownBarRect2.mX -= GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_TIMER.mWidth / 2;
				countdownBarRect2.mWidth = (int)((float)countdownBarRect2.mWidth * this.GetLevelPct());
				g.SetColor(new Color(GlobalMembers.M(240), GlobalMembers.M(255), 200, (int)(this.mLevelBarBonusAlpha * (double)GlobalMembers.M(255))));
				g.FillRect(countdownBarRect2);
			}
			Graphics3D graphics3D = g.Get3D();
			SexyTransform2D mDrawTransform = this.mCountdownBarPIEffect.mDrawTransform;
			Rect mClipRect = g.mClipRect;
			if (graphics3D != null)
			{
				countdownBarRect.Scale(this.mScale, this.mScale, (int)ConstantsWP.DEVICE_HEIGHT_F, (int)ConstantsWP.DEVICE_WIDTH_F);
				this.mCountdownBarPIEffect.mDrawTransform.Translate(-ConstantsWP.DEVICE_HEIGHT_F, -ConstantsWP.DEVICE_WIDTH_F);
				this.mCountdownBarPIEffect.mDrawTransform.Scale((float)this.mScale, (float)this.mScale);
				this.mCountdownBarPIEffect.mDrawTransform.Translate(ConstantsWP.DEVICE_HEIGHT_F, ConstantsWP.DEVICE_WIDTH_F);
			}
			g.SetClipRect(countdownBarRect);
			this.mCountdownBarPIEffect.mColor = new Color(255, 255, 255, (int)(num * (float)GlobalMembers.M(255)));
			this.mCountdownBarPIEffect.Draw(g);
			this.mCountdownBarPIEffect.mDrawTransform = mDrawTransform;
			g.SetColor(Color.White);
			g.SetClipRect(mClipRect);
			if (this.mOffsetY != 0)
			{
				g.Translate(0, -this.mOffsetY);
			}
		}

		public override void DrawWarningHUD(Graphics g)
		{
			g.PushState();
			Color color = g.GetColor();
			g.SetDrawMode(Graphics.DrawMode.Additive);
			g.SetColorizeImages(true);
			Color warningGlowColor = this.GetWarningGlowColor();
			g.SetColor(warningGlowColor);
			g.PushColorMult();
			this.mDrawingOverlay = true;
			this.DrawFrame(g);
			this.mDrawingOverlay = false;
			g.PopColorMult();
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.SetColor(color);
			g.PopState();
		}

		public override bool WantsTutorialReplays()
		{
			return false;
		}

		public override int GetTimerYOffset()
		{
			return ConstantsWP.SPEEDBOARD_TIMEDRAW_Y_OFFSET;
		}

		public override void PlayMenuMusic()
		{
			GlobalMembers.gApp.mMusic.PlaySongNoDelay(11, true);
		}

		public override Color GetWarningGlowColor()
		{
			int ticksLeft = this.GetTicksLeft();
			int theAlpha;
			if (ticksLeft == 0)
			{
				theAlpha = 127;
			}
			else
			{
				int num = 1000;
				float num2 = (float)(num - ticksLeft) / (float)num;
				theAlpha = (int)((float)((int)(Math.Sin((double)((float)this.mUpdateCnt * GlobalMembers.M(0.15f))) * 127.0) + 127) * num2 * this.GetPieceAlpha());
			}
			if (this.mBonusTime > 0)
			{
				return new Color(255, 255, 0, theAlpha);
			}
			return new Color(255, 0, 0, theAlpha);
		}

		public override Image GetMultiplierImage()
		{
			return GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_MULTIPLIER;
		}

		public override int GetMultiplierImageX()
		{
			return (int)GlobalMembers.IMG_SXOFS(899);
		}

		public override int GetMultiplierImageY()
		{
			return (int)GlobalMembers.IMG_SYOFS(899);
		}

		private bool mDrawingOverlay;

		public int mPreHurrahPoints;

		public int mSpeedTier;

		public int mPointsGoal;

		public int mPrevPointsGoal;

		public int mPMDropLevel;

		public bool mUsePM;

		public bool mUseCheckpoints;

		public bool mDidTimeUp;

		public EffectsManager mTimeFXManager;

		public SoundInstance mHumSoundEffect;

		public int mTimeUpCount;

		public int mTotalGameTicks;

		public int mBonusTime;

		public int mTotalBonusTime;

		public float mBonusTimeDisp;

		public int mTimedPenaltyAmnesty;

		public float mTimedPenaltyVel;

		public float mTimedPenaltyAccel;

		public float mTimedPenaltyJerk;

		public float mTimedLevelBonus;

		public double mBonusPenalty;

		public float mPointMultiplierStart;

		public float mAddPointMultiplierPerLevel;

		public bool mReadyForDrop;

		public int mWantGemsCleared;

		public int mDropGameTick;

		public int mTimeStart;

		public int mTimeChange;

		public int mMaxTicksLeft;

		public int mPointsGoalStart;

		public int mAddPointsGoalPerLevel;

		public double mPointsGoalAddPower;

		public QuasiRandom m5SecChance = new QuasiRandom();

		public QuasiRandom m10SecChance = new QuasiRandom();

		public float m5SecChanceDec;

		public float m10SecChanceDec;

		public float mTimeStep;

		public float mLevelTimeStep;

		public float mGameTicksF;

		public float mTimeScaleOverride;

		public CurvedVal mCollectorExtendPct = new CurvedVal();

		public CurvedVal mCollectedTimeAlpha = new CurvedVal();

		public CurvedVal mLastHurrahAlpha = new CurvedVal();

		public int mLastHurrahUpdates;

		public float mPanicScalePct;

		public float mCurTempo;
	}
}
