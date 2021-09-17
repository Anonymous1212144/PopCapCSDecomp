using System;
using System.Collections.Generic;
using System.Text;
using BejeweledLIVE;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Sexy;

namespace Bejeweled3
{
	public class BoardBejLive : Board
	{
		private int MAX_MULTIPLIER
		{
			get
			{
				if (this.mBlitzMode)
				{
					return 8;
				}
				return 1;
			}
		}

		public BoardBejLive(GameApp theApp)
			: base(theApp)
		{
			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 16; j++)
				{
					BoardBejLive.mWarpPoints[i, j] = new WarpPoint();
				}
			}
			BoardBejLive.mPieceMap.Clear();
			if (BoardBejLive.mHyperspace == null)
			{
				BoardBejLive.mHyperspace = new Hyperspace(theApp);
			}
			else
			{
				BoardBejLive.mHyperspace.Reset();
			}
			BoardBejLive.gGemColors = new SexyColor[BoardBejLive.gAllGemColors.Length - 1];
			for (int k = 0; k < BoardBejLive.gGemColors.Length; k++)
			{
				BoardBejLive.gGemColors[k] = BoardBejLive.gAllGemColors[k + 1];
			}
			for (int l = 0; l < 8; l++)
			{
				for (int m = 0; m < 8; m++)
				{
					this.mBoard[l, m] = null;
				}
			}
			this.mPostFXManager = new EffectsManager(this);
			this.RemoveAllWidgets(true);
			if (this.mMenuHintButton != null)
			{
				this.RemoveWidget(this.mMenuHintButton);
			}
			this.mMenuHintButton = new MenuHintButton(0, 1, 3, false, this);
			this.AddWidget(this.mMenuHintButton);
			this.ClearBoard();
			this.mTimedBonus = (double)this.mApp.mTimedBonus;
			this.mTimedBonusMult = this.mApp.mTimedBonusMult;
			this.mClassicMode = this.mApp.mGameMode == GameMode.MODE_CLASSIC || this.mApp.mGameMode == GameMode.MODE_CLASSIC_SECRET;
			this.mTimedMode = this.mApp.mGameMode == GameMode.MODE_TIMED || this.mApp.mGameMode == GameMode.MODE_TIMED_SECRET || this.mApp.mGameMode == GameMode.MODE_ENDLESS_SECRET;
			this.mBlitzMode = this.mApp.mGameMode == GameMode.MODE_BLITZ;
			this.mEndlessMode = this.mApp.mGameMode == GameMode.MODE_ENDLESS || this.mApp.mGameMode == GameMode.MODE_ENDLESS_SECRET;
			this.SetMenuHintButtonUsage();
			this.mNextPieceId = 1;
			this.mRand.SRand(GlobalStaticVars.timeGetTime());
			for (int n = 0; n < 24; n++)
			{
				this.mGameStats[n] = 0;
			}
			this.mFullLaser = true;
			this.mHasAlpha = true;
			this.mUpdateAcc = 0f;
			this.mGameOverCount = 0;
			this.mLevelCompleteCount = 0;
			this.mLastHitSoundTick = 0;
			this.mClip = false;
			this.Init();
			this.mMouseDown = false;
			this.mMouseDownX = 0;
			this.mMouseDownY = 0;
			this.mShowLevelPoints = false;
			this.mFavorComboGems = false;
			this.mUserPaused = false;
			this.mBoardHidePct = 0f;
			this.mVisPausePct = 0f;
			this.mTimeExpired = false;
			this.mShowMoveCredit = false;
			this.mDoThirtySecondVoice = true;
			this.mShowLevelDelay = (int)Constants.mConstants.M(80f);
			this.mPointsManager = new PointsManager();
			this.mPointsManager.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			this.AddWidget(this.mPointsManager);
			this.mBackgroundIdx = 0;
			this.mPostFXManager.Reset();
			this.mPostFXManager.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			this.AddWidget(this.mPostFXManager);
			this.mWidgetFlagsMod.mAddFlags = this.mWidgetFlagsMod.mAddFlags | 2;
			this.mWantLevelup = false;
			this.ResizeButtons();
			this.FillInBlanks(false);
			if (this.mApp.IsLandscape())
			{
				this.miPrevOrientation = (this.miCurrOrientation = UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_LEFT);
			}
			else
			{
				this.miPrevOrientation = (this.miCurrOrientation = UI_ORIENTATION.UI_ORIENTATION_PORTRAIT);
			}
			List<StaticParticleEmitter> list = StaticParticleEmitter.CreateProgressBarEmitters(0, 0, new Vector2((float)this.mWidth, Constants.mConstants.S(150f)), this.mApp.mGameMode);
			foreach (StaticParticleEmitter staticParticleEmitter in list)
			{
				this.progressBarEmitters.Add(staticParticleEmitter);
			}
		}

		private void SetMenuHintButtonUsage()
		{
			if (this.mTimedMode && !SexyAppBase.IsInTrialMode)
			{
				this.mMenuHintButton.DoubleButtonVersion = true;
				this.mMenuHintButton.LeftButtonUsage = MenuHintButton.LeftButtonType.PlayPause;
				return;
			}
			if (this.mEndlessMode || SexyAppBase.IsInTrialMode)
			{
				this.mMenuHintButton.DoubleButtonVersion = true;
				this.mMenuHintButton.LeftButtonUsage = MenuHintButton.LeftButtonType.Empty;
				return;
			}
			this.mMenuHintButton.DoubleButtonVersion = false;
		}

		public static void PreAllocateMemory()
		{
			for (int i = 1; i < 20; i++)
			{
				string text = string.Format(Strings.Level, i);
				BoardBejLive.numberStringCache.Add(i, text);
			}
		}

		public override void Dispose()
		{
			this.ClearBoard();
			this.RemoveAllWidgets(true);
			this.mUIWarpPercentAdd.PrepareForReuse();
			for (int i = 0; i < this.progressBarEmitters.Count; i++)
			{
				this.progressBarEmitters[i].PrepareForReuse();
			}
		}

		public override bool IsInProgress()
		{
			return (!this.mFirstMove || SexyAppBase.IsInTrialMode) && this.mState != Board.BoardState.STATE_GAME_OVER_ANIM && this.mState != Board.BoardState.STATE_GAME_OVER_FALL && this.mState != Board.BoardState.STATE_WHIRLPOOL && this.mState != Board.BoardState.STATE_GAME_OVER_DISPLAY && this.mState != Board.BoardState.STATE_PUZZLE_MODE_DONE && !this.mBlitzMode;
		}

		protected void ProcessEndGameInfo()
		{
			if (!this.mProcessedEndGameInfo)
			{
				this.ResetChainCount();
				this.UpdateProfileGemInfo();
				int num = this.mTicksPlayed / 100;
				if (this.mTimedMode)
				{
					this.mApp.mModeSeconds[1] += num;
				}
				else if (this.mEndlessMode)
				{
					this.mApp.mModeSeconds[3] += num;
				}
				else
				{
					this.mApp.mModeSeconds[0] += num;
				}
				this.mApp.mHighScorePos = -1;
				this.mApp.mHighScorePos = this.mApp.mProfile.GetScorePos((int)this.mApp.mGameMode, this.mPoints);
				if (this.mApp.mHighScorePos != -1)
				{
					Gamer gamer = Main.GetGamer();
					if (gamer != null)
					{
						string text = this.mApp.mProfile.GetMostRecentHighScoreName();
						if (text.length() == 0)
						{
							text = gamer.Gamertag;
						}
						this.mApp.mProfile.EnterScore(text, (int)this.mApp.mGameMode, this.mPoints, this.mLevel + 1);
					}
				}
				this.mApp.mProfile.SaveProfile();
				LeaderBoardComm.RecordResult(this.mApp.mGameMode, this.mPoints);
				this.mProcessedEndGameInfo = true;
			}
		}

		private void UpdateProfileGemInfo()
		{
			this.mTotalGameTime = this.GetTotalGameTime();
			this.mApp.mProfile.mSecondsPlayed += this.mTotalGameTime - this.mTotalGameTimeCommitted;
			this.mTotalGameTimeCommitted = this.mTotalGameTime;
			if (!this.mPuzzleMode && !this.mGameInvalid)
			{
				this.mApp.mProfile.mNumPowerGemsCreated += this.mNumPowerGemsCreated - this.mNumPowerGemsCreatedCommitted;
				if (this.mApp.mProfile.mNumPowerGemsCreated >= 10)
				{
					this.mApp.mProfile.DisableHint(Profile.Hint.HINT_PIECE_POWER_GEM);
				}
				this.mNumPowerGemsCreatedCommitted = this.mNumPowerGemsCreated;
				this.mApp.mProfile.mNumHyperGemsCreated += this.mNumHyperGemsCreated - this.mNumHyperGemsCreatedCommitted;
				if (this.mApp.mProfile.mNumHyperGemsCreated >= 5)
				{
					this.mApp.mProfile.DisableHint(Profile.Hint.HINT_PIECE_HYPER_CUBE);
				}
				this.mNumHyperGemsCreatedCommitted = this.mNumHyperGemsCreated;
				this.mApp.mProfile.mNumLaserGemsCreated += this.mNumLaserGemsCreated - this.mNumLaserGemsCreatedCommitted;
				if (this.mApp.mProfile.mNumLaserGemsCreated >= 5)
				{
					this.mApp.mProfile.DisableHint(Profile.Hint.HINT_PIECE_LASER_GEM);
				}
				this.mNumLaserGemsCreatedCommitted = this.mNumLaserGemsCreated;
				this.mApp.mProfile.mGemsCleared += this.mGemsCleared - this.mGemsClearedCommitted;
				this.mGemsClearedCommitted = this.mGemsCleared;
			}
			if (!this.mGameInvalid)
			{
				if (!this.mEndlessMode)
				{
					this.mApp.mProfile.mBestScore = Math.Max(this.mApp.mProfile.mBestScore, this.mPoints);
				}
				this.mApp.mProfile.mBiggestCascade = Math.Max(this.mApp.mProfile.mBiggestCascade, this.mLongestChainReaction);
				this.mApp.mProfile.mBiggestCombo = Math.Max(this.mApp.mProfile.mBiggestCombo, this.mHighestScoringMove);
			}
			ReportAchievement.ReportInferno(this.mApp.mProfile.mNumPowerGemsCreated);
			ReportAchievement.ReportMillionaire(this.mApp.mProfile.mGemsCleared);
		}

		public override void Pause(bool visible)
		{
			this.Pause(visible, false);
		}

		public override void Pause(bool visible, bool immediate)
		{
			if (this.mPauseCount == 0)
			{
				this.mTimePlayedAdd += (int)(((long)this.mUpdateCnt - (long)((ulong)this.mTimePeriodStart)) / 100L);
				this.MarkDirty();
			}
			if (visible)
			{
				if (this.mVisPauseCount == 0)
				{
					this.MarkDirty();
				}
				this.mVisPauseCount++;
			}
			this.mPauseCount++;
			if (this.mTimedMode && this.mMenuHintButton != null)
			{
				this.mMenuHintButton.Pause(true);
			}
			if (immediate)
			{
				this.mPauseFade = 0f;
			}
			this.mFrozen = true;
		}

		public override void Unpause(bool visible)
		{
			if (visible && this.mVisPauseCount > 0)
			{
				this.mVisPauseCount--;
				if (this.mVisPauseCount <= 0)
				{
					this.MarkDirty();
				}
			}
			if (this.mPauseCount > 0)
			{
				this.mPauseCount--;
				if (this.mPauseCount == 0)
				{
					this.mTimePeriodStart = (uint)this.mUpdateCnt;
					if (this.mTimedMode && this.mMenuHintButton != null)
					{
						this.mMenuHintButton.Pause(false);
					}
				}
			}
			if (this.mPauseCount == 0)
			{
				this.mPauseFade = 1f;
			}
			this.mFrozen = false;
		}

		public override void DeleteAllSavedGames()
		{
			for (int i = 0; i < Board.aFileName.Length; i++)
			{
				Common.DeleteFile(GlobalStaticVars.GetDocumentsDir() + "/" + Board.aFileName[i]);
			}
		}

		public override void DeleteSavedGame()
		{
			string savedGameFileName = Board.GetSavedGameFileName();
			Common.DeleteFile(savedGameFileName);
		}

		public override bool LoadGame()
		{
			if (this.mPuzzleMode && !this.mSecretMode)
			{
				return false;
			}
			string savedGameFileName = Board.GetSavedGameFileName();
			Buffer buffer = new Buffer();
			if (!this.mApp.ReadBufferFromFile(savedGameFileName, ref buffer, true))
			{
				return false;
			}
			int num = buffer.ReadLong();
			if (num != 7)
			{
				return false;
			}
			this.mRand.SRand(buffer.ReadLong());
			this.mBackdropNum = buffer.ReadLong();
			this.mLevel = buffer.ReadLong();
			this.SetDisplayLevel();
			this.mGoDelay = 0;
			this.mShowLevelDelay = 0;
			this.mSpeedBonusPoints = buffer.ReadLong();
			this.mSpeedBonusNum = buffer.ReadDouble();
			this.mSpeedBonusCount = buffer.ReadLong();
			if (this.mSpeedBonusCount > 0)
			{
				this.mSpeedBonusDisp.SetCurve(CurvedValDefinition.mSpeedBonusDispCurve);
			}
			this.CreateFontGlowImages();
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					this.mBoard[i, j].PrepareForReuse();
					bool flag = buffer.ReadBoolean();
					if (flag)
					{
						PieceBejLive newCPieceBejLive = PieceBejLive.GetNewCPieceBejLive(this);
						newCPieceBejLive.mX = buffer.ReadFloat();
						newCPieceBejLive.mY = buffer.ReadFloat();
						newCPieceBejLive.mZ = buffer.ReadFloat();
						newCPieceBejLive.mCol = buffer.ReadLong();
						newCPieceBejLive.mRow = buffer.ReadLong();
						newCPieceBejLive.mOfsX = buffer.ReadLong();
						newCPieceBejLive.mOfsY = buffer.ReadLong();
						newCPieceBejLive.mShrinking = buffer.ReadBoolean();
						newCPieceBejLive.mShrinkSize = buffer.ReadLong();
						for (int k = 0; k < newCPieceBejLive.mLighting.Length; k++)
						{
							newCPieceBejLive.mLighting[k] = buffer.ReadFloat();
						}
						newCPieceBejLive.mShineAnim = buffer.ReadBoolean();
						newCPieceBejLive.mShineFactor = buffer.ReadDouble();
						newCPieceBejLive.mShineAnimFrame = buffer.ReadFloat();
						newCPieceBejLive.mSparkleLife = buffer.ReadLong();
						newCPieceBejLive.mSparkleFrame = buffer.ReadLong();
						newCPieceBejLive.mSparklePairOfsCol = buffer.ReadLong();
						newCPieceBejLive.mSparklePairOfsRow = buffer.ReadLong();
						newCPieceBejLive.mFallVelocity = buffer.ReadFloat();
						newCPieceBejLive.mXVelocity = buffer.ReadFloat();
						newCPieceBejLive.mColor = buffer.ReadLong();
						newCPieceBejLive.mIsElectrocuting = buffer.ReadBoolean();
						newCPieceBejLive.mElectrocutePercent = buffer.ReadFloat();
						newCPieceBejLive.mLastActiveTick = buffer.ReadLong();
						newCPieceBejLive.mChangedTick = buffer.ReadLong();
						newCPieceBejLive.mExplodeDelay = buffer.ReadLong();
						newCPieceBejLive.mSwapTick = buffer.ReadLong();
						newCPieceBejLive.mSpinFrame = buffer.ReadLong();
						newCPieceBejLive.mRotPct = buffer.ReadFloat();
						newCPieceBejLive.mFlags = buffer.ReadLong();
						if (newCPieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME) || newCPieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM) || newCPieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
						{
							this.mBlitzGemsRenderer.Add(newCPieceBejLive);
							this.CreateEmittersForSpecialPiece(newCPieceBejLive);
						}
						newCPieceBejLive.mLastActiveTick = 0;
						newCPieceBejLive.mChangedTick = 0;
						this.mBoard[i, j] = newCPieceBejLive;
					}
					else
					{
						this.mBoard[i, j] = null;
					}
				}
			}
			this.mState = (Board.BoardState)buffer.ReadLong();
			if (this.mState == Board.BoardState.STATE_SWAPPING || this.mState == Board.BoardState.STATE_SWAPPING_BACK || this.mState == Board.BoardState.STATE_SWAP_INVALID)
			{
				this.mState = Board.BoardState.STATE_MAKE_MOVE;
			}
			this.mTimedMode = buffer.ReadBoolean();
			this.mPuzzleMode = buffer.ReadBoolean();
			this.mCreateMode = buffer.ReadBoolean();
			this.mFreeMove = buffer.ReadBoolean();
			this.mFirstMove = buffer.ReadBoolean();
			this.mLevelBarPct = buffer.ReadFloat();
			this.mTimePlayedAdd = buffer.ReadLong();
			this.mTotalGameTimeCommitted = buffer.ReadLong();
			this.mNoMoveCount = 0;
			this.mPoints = buffer.ReadLong();
			this.mPointsWithoutSpeedBonus = buffer.ReadLong();
			this.mLevelPointsTotal = buffer.ReadLong();
			this.mPointMultiplier = buffer.ReadLong();
			this.mDispPoints = buffer.ReadLong();
			this.mGemsCleared = buffer.ReadLong();
			this.mGemsClearedCommitted = buffer.ReadLong();
			this.mMoveClearCount = buffer.ReadLong();
			this.mChainCount = buffer.ReadLong();
			this.mLongestChainReaction = buffer.ReadLong();
			this.mHighestScoringMove = buffer.ReadLong();
			this.mInBonus = buffer.ReadBoolean();
			this.mBonusCount = buffer.ReadLong();
			this.mLastQuakePoints = buffer.ReadLong();
			this.mNextQuakePoints = buffer.ReadLong();
			this.mPointsPerQuake = buffer.ReadLong();
			this.mBonusPenalty = buffer.ReadDouble();
			this.mTimedPenaltyVel = buffer.ReadDouble();
			this.mTimedPenaltyAccel = buffer.ReadDouble();
			this.mTimedPenaltyJerk = buffer.ReadDouble();
			this.mTimerBarNum = buffer.ReadLong();
			this.mTransitionPos = buffer.ReadLong();
			this.mWhirlpoolFrame = buffer.ReadFloat();
			this.mWhirlpoolFade = buffer.ReadDouble();
			this.mShowedLevelThing = buffer.ReadBoolean();
			this.mSeenPowergemHint = buffer.ReadBoolean();
			this.mSeenHypercubeHint = buffer.ReadBoolean();
			this.mSeenLasergemHint = buffer.ReadBoolean();
			this.mTimedBonus = buffer.ReadDouble();
			this.mTimedBonusMult = buffer.ReadDouble();
			this.mEndlessNumOuterGems = buffer.ReadLong();
			this.mEndlessNumInnerGems = buffer.ReadLong();
			this.mEndlessNumRainbowGems = buffer.ReadLong();
			this.mHandleBombs = buffer.ReadBoolean();
			this.mInsaneMode = buffer.ReadBoolean();
			this.mSecretMode = buffer.ReadBoolean();
			this.mTwilightMode = buffer.ReadBoolean();
			this.mDoubleSpeed = buffer.ReadBoolean();
			this.mGravityReversed = buffer.ReadBoolean();
			this.mWasModeUnlocked = buffer.ReadBoolean();
			this.mPuzzleHintCount = buffer.ReadLong();
			this.mNumHyperGemsCreated = buffer.ReadLong();
			this.mNumPowerGemsCreated = buffer.ReadLong();
			this.mNumLaserGemsCreated = buffer.ReadLong();
			this.mNumHyperGemsCreatedCommitted = buffer.ReadLong();
			this.mNumPowerGemsCreatedCommitted = buffer.ReadLong();
			this.mNumLaserGemsCreatedCommitted = buffer.ReadLong();
			BoardBejLive.perfectGamesCompleted = buffer.ReadLong();
			this.noIllegalMovesAchievementFlag = buffer.ReadBoolean();
			this.noHintsAchievementFlag = buffer.ReadBoolean();
			this.lastColourMatch = buffer.ReadLong();
			this.monocolouristCount = buffer.ReadLong();
			this.starGemsActivated = buffer.ReadLong();
			this.trialGameTime = buffer.ReadDouble();
			for (int l = 0; l < this.mGameStats.Length; l++)
			{
				this.mGameStats[l] = buffer.ReadLong();
			}
			for (int m = 0; m < 8; m++)
			{
				for (int n = 0; n < 8; n++)
				{
					PieceBejLive pieceBejLive = this.mBoard[m, n];
					if (pieceBejLive != null)
					{
						pieceBejLive.mX = (float)base.GetColX(pieceBejLive.mCol);
						pieceBejLive.mY = (float)base.GetRowY(pieceBejLive.mRow);
					}
				}
			}
			if (this.mState == Board.BoardState.STATE_WHIRLPOOL)
			{
				this.EndLevelUpEffect();
				this.mFallDelay = 200;
				this.mState = Board.BoardState.STATE_FALLING;
			}
			else
			{
				this.mFallDelay = 0;
			}
			this.mNoMoveCount = 0;
			if (!this.mTimedMode)
			{
				this.mBonusPenalty = 0.0;
				this.mTimedPenaltyVel = 0.0;
				this.mTimedPenaltyAccel = 0.0;
				this.mTimedPenaltyJerk = 0.0;
			}
			return true;
		}

		public override void Show()
		{
			if (this.actionSheet != null)
			{
				this.ShowMenu();
			}
			this.mInterfaceOffset.SetOutRange((double)Constants.mConstants.OutValueX, 0.0);
			this.mInterfaceOffset.SetInVal(0.0);
			this.mInterfaceOffset.SetMode(CurvedVal.CurveMode.MODE_CLAMP);
			this.mInterfaceOffset.SetRamp(CurvedVal.Ramp.RAMP_FAST_TO_SLOW);
			if (this.mPuzzleStateOverlay != null)
			{
				this.mPuzzleStateOverlay.Resize(0, 0, 0, 0);
			}
			this.ResizeButtons();
			this.SetVisible(true);
			this.mSHOWSTATE = Board.SHOWSTATE.SHOWSTATE_SHOWING;
			this.PlayMusic();
		}

		public override void HideNow()
		{
			this.mSHOWSTATE = Board.SHOWSTATE.SHOWSTATE_HIDDEN;
			this.mInterfaceOffset.SetOutRange(2000.0, 2000.0);
			this.ResizeButtons();
		}

		protected override void Hide()
		{
			this.mInterfaceOffset.SetOutRange(0.0, (double)Constants.mConstants.OutValueX);
			this.mInterfaceOffset.SetInVal(0.0);
			this.mInterfaceOffset.SetMode(0);
			this.mInterfaceOffset.SetRamp(2);
			this.mSHOWSTATE = Board.SHOWSTATE.SHOWSTATE_HIDING;
		}

		public override void ShowNow()
		{
			this.mSHOWSTATE = Board.SHOWSTATE.SHOWSTATE_SHOWN;
			this.mInterfaceOffset.SetOutRange(0.0, 0.0);
			this.mInterfaceOffset.SetInVal(0.0);
			if (this.mPuzzleStateOverlay != null)
			{
				this.mPuzzleStateOverlay.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			}
			this.ResizeButtons();
			this.SetVisible(true);
		}

		public override void DrawBoard(Graphics g, bool noBack)
		{
			this.DrawBoard(g, noBack, false);
		}

		public override void DrawBoard(Graphics g)
		{
			this.DrawBoard(g, false, false);
		}

		public override void DrawBoard(Graphics g, bool noBack, bool noFront)
		{
			if (this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_HIDDEN)
			{
				return;
			}
			float num = this.mGemShowPct * this.mPauseFade;
			BoardBejLive.gFrameLightCount = 0;
			g.SetColorizeImages(true);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetColor(SexyColor.White);
			g.DrawImage(AtlasResources.IMAGE_GRID, Constants.mConstants.Board_Board_X, Constants.mConstants.Board_Board_Y);
			if (this.mSpeedBonusFlameModePct > 0f && this.mPauseCount == 0)
			{
				g.SetColorizeImages(true);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				float num2 = (float)this.mUpdateCnt * Constants.mConstants.MS(0.15f);
				int num3 = Math.Abs((int)((double)(num2 * 4096f) / 6.28318) % 4096);
				g.SetColor(new SexyColor((int)Constants.mConstants.M(255f), (int)(Constants.mConstants.M(128f) + Board.SIN_TAB[num3] * Constants.mConstants.M(32f)), (int)(Constants.mConstants.M(64f) + Board.SIN_TAB[num3] * Constants.mConstants.M(16f)), (int)(Constants.mConstants.M(150f) * Math.Min(1f, this.mSpeedBonusFlameModePct * 4f))));
				g.FillRect(this.BoardRect);
				g.SetColor(SexyColor.White);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			}
			this.DrawFrame(g);
			this.DrawBar(g);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			this.DrawPieces(g);
			if (this.mpHintPiece != null && !this.mDidTimeUp && !this.TrialExpired())
			{
				int num4 = 0;
				while (num4 < 2 && (num4 != 1 || this.mpHintPiece.mSparklePairOfsCol != 0 || this.mpHintPiece.mSparklePairOfsRow != 0))
				{
					int num5 = (int)(this.mpHintPiece.mX + (float)((num4 == 0) ? 0 : (this.mpHintPiece.mSparklePairOfsCol * GameConstants.GEM_WIDTH)));
					int num6 = (int)(this.mpHintPiece.mY + (float)((num4 == 0) ? 0 : (this.mpHintPiece.mSparklePairOfsRow * GameConstants.GEM_HEIGHT)));
					g.SetColorizeImages(true);
					if (num4 == 0)
					{
						g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
						float num7 = Math.Min(1f, (float)this.mpHintPiece.mSparkleLife * 0.1f) * num;
						g.SetColor(new SexyColor(255, 255, 255, (int)(255f * num7)));
						if (this.mpHintPiece.mSparkleLife >= 80)
						{
							int frame = this.mpHintPiece.mSparkleFrame % 14;
							g.DrawImage(AtlasResources.IMAGE_SPARKLE, num5 + Constants.mConstants.BoardBej2_Sparkle_X_LongLife, num6 - Constants.mConstants.BoardBej2_Sparkle_Y_LongLife, new TRect(Constants.mConstants.BoardBej2_Sparkle_Frame(frame), 0, Constants.mConstants.BoardBej2_Sparkle_Dimension, Constants.mConstants.BoardBej2_Sparkle_Dimension));
						}
						if (this.mpHintPiece.mSparkleFrame >= 8 && this.mpHintPiece.mSparkleLife >= 40)
						{
							int frame2 = (this.mpHintPiece.mSparkleFrame - 8) % 14;
							g.DrawImage(AtlasResources.IMAGE_SPARKLE, num5 - Constants.mConstants.BoardBej2_Sparkle_X_MediumLife, num6 + Constants.mConstants.BoardBej2_Sparkle_Y_MediumLife, new TRect(Constants.mConstants.BoardBej2_Sparkle_Frame(frame2), 0, Constants.mConstants.BoardBej2_Sparkle_Dimension, Constants.mConstants.BoardBej2_Sparkle_Dimension));
						}
						if (this.mpHintPiece.mSparkleFrame >= 16 && this.mpHintPiece.mSparkleLife >= 0)
						{
							int frame3 = (this.mpHintPiece.mSparkleFrame - 16) % 14;
							g.DrawImage(AtlasResources.IMAGE_SPARKLE, num5 + Constants.mConstants.BoardBej2_Sparkle_X_ShortLife, num6 + Constants.mConstants.BoardBej2_Sparkle_Y_ShortLife, new TRect(Constants.mConstants.BoardBej2_Sparkle_Frame(frame3), 0, Constants.mConstants.BoardBej2_Sparkle_Dimension, Constants.mConstants.BoardBej2_Sparkle_Dimension));
						}
					}
					int num8 = 200;
					int num9 = 290 - num8;
					if (this.mpHintPiece.mSparkleLife >= num9)
					{
						g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
						float num10 = (float)(num8 - (this.mpHintPiece.mSparkleLife - num9)) * 0.125f + 0f;
						float num11 = 1f;
						int num12 = Math.Abs((int)((double)(num10 * 4096f) / 6.28318) % 4096);
						float num13 = (float)(1.0 - (double)Board.COS_TAB[num12]) * 0.5f;
						int num14 = num6 - 23 + (int)(num13 * 5f);
						g.SetColor(new SexyColor(255, 255, 255, (int)(255f * num11 * num)));
						g.DrawImage(AtlasResources.IMAGE_HELP_ARROW, num5 + Constants.mConstants.BoardBej2_HintArrowOffset, num14);
						g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
						g.SetColor(new SexyColor(255, 255, 255, (int)(255f * num11 * num13 * num)));
						g.DrawImage(AtlasResources.IMAGE_HELP_INDICATOR_ARROW, num5 + Constants.mConstants.BoardBej2_HintArrowOverlayOffset_X, num14 + Constants.mConstants.BoardBej2_HintArrowOverlayOffset_Y);
					}
					num4++;
				}
			}
			this.DrawAdditiveOverlayEffect(g);
			int count = this.mLightningStorms.Count;
			this.DrawLightning(g);
		}

		public void DrawUI(Graphics g, bool noBack)
		{
			this.DrawUI(g, noBack, false);
		}

		public void DrawUI(Graphics g)
		{
			this.DrawUI(g, false, false);
		}

		public void DrawUI(Graphics g, bool noBack, bool noFront)
		{
			if (this.mTimedMode)
			{
				if (!noFront)
				{
					this.DrawWidgetTo(g, this.mMenuHintButton);
					return;
				}
			}
			else if (!noFront)
			{
				this.DrawWidgetTo(g, this.mMenuHintButton);
			}
		}

		public GameApp GetApp()
		{
			return this.mApp;
		}

		public PieceBejLive GetPieceById(int ID)
		{
			PieceBejLive result;
			BoardBejLive.mPieceMap.TryGetValue(ID, ref result);
			return result;
		}

		public void ClearBoard()
		{
			this.ClearBoard(false);
		}

		public void ClearBoard(bool bFromRestart)
		{
			if (!bFromRestart)
			{
				this.mFirstUpdate = true;
				this.mPreparingLevelImages = false;
			}
			this.mHintPiece = null;
			this.mExternalDraw = false;
			this.mPauseCount = 0;
			this.mVisPauseCount = 0;
			this.mDoubleSpeed = this.mApp.mGameMode == GameMode.MODE_TIMED_SECRET;
			this.mPuzzleStateOverlay = null;
			this.mNoMoreMoves = false;
			this.mInTutorialMode = false;
			this.mTotalGameTime = 0;
			this.mGemsCleared = 0;
			this.mLongestChainReaction = 0;
			this.mHighestScoringMove = 0;
			this.mPoints = 0;
			this.mPointsWithoutSpeedBonus = 0;
			this.mFallDelay = 180;
			this.mTimedMode = false;
			this.mFlashBarRed = false;
			this.mGemShowPct = 1f;
			this.mPauseFade = 1f;
			this.mGoDelay = 300;
			this.mSelectAnimCnt = 0;
			this.mShowingSun = false;
			this.mSunPosition = 0f;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (this.mBoard[i, j] != null)
					{
						this.mBoard[i, j].PrepareForReuse();
						this.mBoard[i, j] = null;
					}
				}
			}
			this.mNextPieceId = 1;
			for (int k = 0; k < 24; k++)
			{
				this.mGameStats[k] = 0;
			}
			this.mFullLaser = true;
			this.mHasAlpha = true;
			this.mUpdateAcc = 0f;
			this.mGameOverCount = 0;
			this.mLevelCompleteCount = 0;
			this.mLastHitSoundTick = 0;
			this.mClip = false;
			this.Init();
			this.mMouseDown = false;
			this.mMouseDownX = 0;
			this.mMouseDownY = 0;
			this.mShowLevelPoints = false;
			this.mFavorComboGems = false;
			this.mUserPaused = false;
			this.mBoardHidePct = 0f;
			this.mVisPausePct = 0f;
			this.mTimeExpired = false;
			this.mShowMoveCredit = false;
			this.mDoThirtySecondVoice = true;
			this.mShowLevelDelay = (int)Constants.mConstants.M(80f);
			this.mWidgetFlagsMod.mAddFlags = this.mWidgetFlagsMod.mAddFlags | 2;
			this.mScale.SetConstant(1.0);
			this.ResizeButtons();
			this.miPrevOrientation = (this.miCurrOrientation = this.mApp.mInterfaceOrientation);
			for (int l = 0; l < this.mLightningStorms.Count; l++)
			{
				this.mLightningStorms[l].PrepareForReuse();
			}
			this.mLightningStorms.Clear();
			this.mApp.mEffectOverlay.StopStrechyText();
			this.SetStat(Stats.STAT_WAS_GAME_COMPLETED, 0);
		}

		public void Scramble()
		{
			this.mUserPaused = true;
			this.mPauseFade = 0f;
			ActionSheet newActionSheet = ActionSheet.GetNewActionSheet(true, 37, this, this.mApp);
			newActionSheet.AddButton(4, "RESTART");
			newActionSheet.SetCancelButton(6, "CANCEL");
			newActionSheet.Present();
		}

		public void Restart()
		{
			this.mApp.mFacebook.recordBlitzGame(this.mPoints, false, this.GetGameStatString());
			this.ClearBoard(true);
			this.FillInBlanks(false);
		}

		public string GetGameStatString()
		{
			this.mGameStatString = string.Format("{0:D},{1:D},{2:D},{3:D},{4:D},{5:D},{6:D},{7:D},{8:D},{9:D},{10:D},{11:D},{12:D},{13:D},{14:D},{15:D}", new object[]
			{
				this.mGameStats[2],
				this.mGameStats[3],
				this.mPoints,
				this.mGameStats[5],
				this.mGameStats[9],
				this.mGameStats[10],
				this.mGameStats[11],
				this.mGameStats[12],
				this.mGameStats[14],
				this.mGameStats[15],
				this.mGameStats[16],
				this.mGameStats[17],
				this.mGameStats[18],
				this.mGameStats[20],
				this.mGameStats[21],
				this.mGameStats[22]
			});
			return this.mGameStatString;
		}

		protected void Init()
		{
			this.didReachLevelEnd = false;
			this.mFirstMove = true;
			this.mFirstWhirlDraw = false;
			this.mState = Board.BoardState.STATE_FALLING;
			this.mComplementNum = -1;
			this.mLastComplement = -1;
			this.mSHOWSTATE = Board.SHOWSTATE.SHOWSTATE_SHOWING;
			this.mGameTicks = 0;
			this.mLapseGameTicks = 0;
			this.mIdleTicks = 0;
			this.mFindSetCounter = 0;
			this.mLastMatchTick = 0;
			this.mLastMatchTime = -1;
			this.mMatchTallyCount = 0;
			this.mLastMatchTally = 0;
			this.mSpeedNeedle = 50f;
			this.mSpeedBonusAlpha = 0f;
			this.mSpeedBonusPoints = 0;
			this.mSpeedModeFactor.SetConstant(1.0);
			this.mSpeedBonusNum = 0.0;
			this.mSpeedBonusCount = 0;
			this.mSpeedBonusLastCount = 0;
			this.mSpeedBonusFlameModePct = 0f;
			this.mSpeedMedCount = 0;
			this.mSpeedHighCount = 0;
			this.mSpeedBonusPointsScale.SetConstant(1.0);
			this.mCurMoveCreditId = 0;
			this.mCurMatchId = 0;
			this.mGemFallDelay = 0;
			this.mPointMultiplier = 0;
			this.mMultiplierCount = 0;
			this.mPoints = 0;
			this.mPointsWithoutSpeedBonus = 0;
			this.mDispPoints = 0;
			this.mLevelBarPct = 1f;
			this.mLevelPointsTotal = 0;
			this.mLevel = 0;
			this.SetDisplayLevel();
			this.mPrevMultiGemColor = -1;
			this.miEndGamePause = 0;
			this.miGameOverTicks = 90;
			this.mNoMoveCount = 0;
			this.miBufferCheckCount = 0;
			this.mbPieceDeleted = false;
			this.miWarningSoundPlaying = 100;
			this.mBoardDarken = 0f;
			this.mComboCount = 0;
			this.mLastComboCount = 0;
			this.mComboCountDisp = 0f;
			this.mComboLen = 3;
			for (int i = 0; i < this.mComboLen; i++)
			{
				this.mComboColors[i] = 0;
			}
			this.mComboSelectorAngle = 22f;
			this.mLastPlayerSwapColor = -1;
			this.mMoneyDispGoal = 0;
			this.mComboBonusSlowdownPct = 0f;
			this.mWantHintTicks = 0;
			this.mHyperGemColor.mRed = 0f;
			this.mHyperGemColor.mGreen = 0f;
			this.mHyperGemColor.mBlue = 0f;
			this.mHyperGemColorFactor = 0f;
			this.mBoardDarken = 0f;
			for (int j = 0; j < 8; j++)
			{
				this.mBumpVelocities[j] = 0f;
				this.mNextColumnCredit[j] = -1;
			}
			this.mWantCounter = 0;
			this.mDropGameTick = 0;
			this.mReadyForDrop = true;
			this.mDroppingMultiplier = false;
			this.mDidTimeUp = false;
			this.mTimeUpCount = 0;
			this.mPosted = false;
			this.mGotBestScore = false;
			this.mComboLen = 0;
			this.mGameSpeed.SetConstant(1.0);
			this.mWantGemsCleared = 0;
			this.mPreHurrahPoints = 0;
			if (this.mBlitzMode)
			{
				this.mMinutes = 1;
			}
			this.mBoostsEnabled = 0;
			this.SetupLevelData();
			this.mNextQuakePoints = this.mPointsPerQuake;
			this.mBoostsUsed = 0;
			this.mBestScore = 0;
			this.mFrozen = false;
			this.mEditing = false;
			this.mCountUnder = 0;
			this.mNumLastMoves = 0;
			this.mPrevSpeedBonusPoints = 0;
			for (int k = 0; k < 13; k++)
			{
				this.mPointsBreakdown[k] = 0;
				this.mSpeedBonusBreakdown[k] = 0;
			}
			this.mbFirstDraw = true;
			this.mpHintPiece = null;
			this.mState = Board.BoardState.STATE_FALLING;
			this.mBlitzGemsRenderer.Clear();
			this.mMultFontRenderer.Clear();
			for (int l = 0; l < this.mMoveDataVector.Count; l++)
			{
				this.mMoveDataVector[l].PrepareForReuse();
			}
			this.mMoveDataVector.Clear();
			for (int m = 0; m < this.mSwapDataVector.Count; m++)
			{
				this.mSwapDataVector[m].PrepareForReuse();
			}
			this.mSwapDataVector.Clear();
			for (int n = 0; n < this.mQueuedMoveVector.Count; n++)
			{
				this.mQueuedMoveVector[n].PrepareForResue();
			}
			this.mQueuedMoveVector.Clear();
			this.miLightningGemCount = 0;
			this.miHintCoolDownCount = 0;
			this.mSelectedPiece = null;
			this.miMultiPlayTick = 0;
		}

		private void SetDisplayLevel()
		{
			this.mDispLevel = (this.mLevel + 1).ToString();
			if (!BoardBejLive.numberStringCache.TryGetValue(this.mLevel + 1, ref this.mDispLevelStretchy))
			{
				this.mDispLevelStretchy = string.Format(Strings.Level, this.mLevel + 1);
				BoardBejLive.numberStringCache.Add(this.mLevel + 1, this.mDispLevelStretchy);
			}
		}

		protected void SetupLevelData()
		{
			double num = (double)this.mPointMultiplier;
			this.mPointMultiplier = this.mLevel + 1;
			this.didReachLevelEnd = false;
			if (this.mTimedMode)
			{
				this.mPointMultiplier = 4 + this.mLevel;
				this.mPointsPerQuake = (int)(this.mTimedBonus * (double)this.mPointMultiplier);
				this.mTimedBonus += this.mTimedBonusMult;
				this.mTimedBonusMult += this.mApp.mTimedBonusAccel;
				if (num == 0.0)
				{
					this.mTimedPenaltyVel = 100.0;
					this.mTimedPenaltyAccel = 0.5;
					this.mTimedPenaltyJerk = 0.006;
				}
				else
				{
					double num2 = (double)this.mPointMultiplier / num;
					this.mTimedPenaltyVel *= num2;
					this.mTimedPenaltyAccel *= num2;
					this.mTimedPenaltyJerk *= num2;
					double num3 = 135.0;
					this.mTimedPenaltyVel += (double)((int)(this.mTimedPenaltyAccel * num3));
					this.mTimedPenaltyAccel += (double)((int)(this.mTimedPenaltyJerk * num3));
				}
			}
			else if (this.mEndlessMode)
			{
				this.mPointMultiplier = this.mLevel + 1;
				this.mPointsPerQuake = (int)((double)(this.mApp.mEndlessBonus * this.mPointMultiplier) + (double)this.mLevel * this.mApp.mEndlessBonusMult * (double)this.mPointMultiplier);
			}
			else if (this.mBlitzMode)
			{
				this.mPointMultiplier = 1;
				this.mPointsPerQuake = (int)((double)this.mApp.mNormBonus + (double)this.mLevel * this.mApp.mNormBonusMult);
			}
			else
			{
				this.mPointMultiplier = this.mLevel + 1;
				this.mPointsPerQuake = (int)((double)(this.mApp.mNormBonus * this.mPointMultiplier) + (double)this.mLevel * this.mApp.mNormBonusMult * (double)this.mPointMultiplier);
			}
			this.mBonusPenalty = 0.0;
		}

		protected void Draw3DWhirlpoolState(Graphics g)
		{
			if (!BoardBejLive.mHyperspace.IsRunning || !BoardBejLive.mHyperspace.mShowBkg)
			{
				int num = 0;
				for (int i = 0; i < 15; i++)
				{
					for (int j = 0; j < 15; j++)
					{
						WarpPoint warpPoint = BoardBejLive.mWarpPoints[i, j];
						WarpPoint warpPoint2 = BoardBejLive.mWarpPoints[i, j + 1];
						WarpPoint warpPoint3 = BoardBejLive.mWarpPoints[i + 1, j];
						WarpPoint warpPoint4 = BoardBejLive.mWarpPoints[i + 1, j + 1];
						int num2 = 0;
						BoardBejLive.aTriVertices[num, num2].x = warpPoint.mX;
						BoardBejLive.aTriVertices[num, num2].y = warpPoint.mY;
						BoardBejLive.aTriVertices[num, num2].u = warpPoint.mU;
						BoardBejLive.aTriVertices[num, num2].v = warpPoint.mV;
						num2++;
						BoardBejLive.aTriVertices[num, num2].x = warpPoint2.mX;
						BoardBejLive.aTriVertices[num, num2].y = warpPoint2.mY;
						BoardBejLive.aTriVertices[num, num2].u = warpPoint2.mU;
						BoardBejLive.aTriVertices[num, num2].v = warpPoint2.mV;
						num2++;
						BoardBejLive.aTriVertices[num, num2].x = warpPoint4.mX;
						BoardBejLive.aTriVertices[num, num2].y = warpPoint4.mY;
						BoardBejLive.aTriVertices[num, num2].u = warpPoint4.mU;
						BoardBejLive.aTriVertices[num, num2].v = warpPoint4.mV;
						num++;
						num2 = 0;
						BoardBejLive.aTriVertices[num, num2].x = warpPoint.mX;
						BoardBejLive.aTriVertices[num, num2].y = warpPoint.mY;
						BoardBejLive.aTriVertices[num, num2].u = warpPoint.mU;
						BoardBejLive.aTriVertices[num, num2].v = warpPoint.mV;
						num2++;
						BoardBejLive.aTriVertices[num, num2].x = warpPoint4.mX;
						BoardBejLive.aTriVertices[num, num2].y = warpPoint4.mY;
						BoardBejLive.aTriVertices[num, num2].u = warpPoint4.mU;
						BoardBejLive.aTriVertices[num, num2].v = warpPoint4.mV;
						num2++;
						BoardBejLive.aTriVertices[num, num2].x = warpPoint3.mX;
						BoardBejLive.aTriVertices[num, num2].y = warpPoint3.mY;
						BoardBejLive.aTriVertices[num, num2].u = warpPoint3.mU;
						BoardBejLive.aTriVertices[num, num2].v = warpPoint3.mV;
						num++;
					}
				}
				g.DrawTrianglesTex(BoardBejLive.aTriVertices, num, SexyColor.White, 0, this.mApp.IsLandscape() ? GameConstants.BGH_TEXTURE : GameConstants.BGV_TEXTURE);
			}
			if (this.mWhirlpoolFade > 0.0)
			{
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, (int)(255.0 * this.mWhirlpoolFade)));
				Image image_BLACK_HOLE_COVER = AtlasResources.IMAGE_BLACK_HOLE_COVER;
				g.DrawImage(image_BLACK_HOLE_COVER, this.mApp.mWidth / 2 - image_BLACK_HOLE_COVER.GetWidth() / 2, this.mApp.mHeight / 2 - image_BLACK_HOLE_COVER.GetHeight() / 2);
				g.SetColorizeImages(false);
				g.SetColorizeImages(true);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				int num3 = (int)this.mWhirlpoolFrame;
				int num4 = num3 + 1;
				if (num4 == 5)
				{
					num4 = 0;
				}
				float num5 = this.mWhirlpoolFrame - (float)num3;
				int theX = this.mApp.mWidth / 2;
				int theY = this.mApp.mHeight / 2;
				TRect theSrcRect = new TRect(num3 * Constants.mConstants.BoardBej2_BlackHoleDimension, 0, Constants.mConstants.BoardBej2_BlackHoleDimension, Constants.mConstants.BoardBej2_BlackHoleDimension);
				TRect theSrcRect2 = new TRect(num4 * Constants.mConstants.BoardBej2_BlackHoleDimension, 0, Constants.mConstants.BoardBej2_BlackHoleDimension, Constants.mConstants.BoardBej2_BlackHoleDimension);
				g.SetColor(new SexyColor(255, 255, 255, (int)(255.0 * this.mWhirlpoolFade * (1.0 - (double)num5))));
				if (((SexyColor)g.mColor).mAlpha > 0 && ((SexyColor)g.mColor).mAlpha <= 255)
				{
					g.DrawImageRotated(AtlasResources.IMAGE_BLACK_HOLE, theX, theY, this.mWhirlpoolRot, theSrcRect);
				}
				g.SetColor(new SexyColor(255, 255, 255, (int)(255.0 * this.mWhirlpoolFade * (double)num5)));
				if (((SexyColor)g.mColor).mAlpha > 0 && ((SexyColor)g.mColor).mAlpha <= 255)
				{
					g.DrawImageRotated(AtlasResources.IMAGE_BLACK_HOLE, theX, theY, this.mWhirlpoolRot, theSrcRect2);
				}
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				g.SetColorizeImages(false);
			}
			float num6 = (float)(1.0 - this.mUIWarpPercent);
			float num7 = (float)Math.Sin(this.mUIWarpPercent * 3.14159 / 2.0);
			int num8 = this.mApp.mWidth / 2;
			int num9 = this.mApp.mHeight / 2;
			if (this.mUIWarpPercent != 0.0)
			{
				int num10 = Constants.mConstants.Board_Board_X + (int)(num7 * 1000f);
				int board_Board_Y = Constants.mConstants.Board_Board_Y;
				int width = AtlasResources.IMAGE_GRID.GetWidth();
				float num11 = (float)(this.mUIWarpPercent * 5.0);
				SexyTransform2D sexyTransform2D = default(SexyTransform2D);
				sexyTransform2D.Translate((float)(-(float)width / 2), (float)(-(float)width / 2));
				sexyTransform2D.Scale(num6, num6);
				sexyTransform2D.RotateRad(-num11);
				sexyTransform2D.Translate((float)num8 + (float)(((double)num10 * Constants.mConstants.Scale_Long + (double)(width / 2) - (double)num8) * (double)num6), (float)num9 + (float)(((double)board_Board_Y * Constants.mConstants.Scale_Long + (double)(width / 2) - (double)num9) * (double)num6));
				sexyTransform2D.Translate((float)((int)((double)(-(double)Constants.mConstants.Board_Board_X) * Constants.mConstants.Scale_Long * (double)num6)), (float)((int)((double)(-(double)Constants.mConstants.Board_Board_Y) * Constants.mConstants.Scale_Long * (double)num6)));
				Graphics.PushTransform(ref sexyTransform2D);
				this.DrawBoard(g);
				Graphics.PopTransform();
			}
			else
			{
				this.DrawBoard(g);
			}
			int num12 = (int)(0f - num7 * (float)Constants.mConstants.BoardBej3_Warp_Text_Target_X);
			int num13 = 24 - Constants.mConstants.BoardBej2_ScoreText_WarpOffset_Y - (int)(num7 * (float)Constants.mConstants.BoardBej3_Warp_Text_Target_Y);
			int num14 = 200;
			float num15 = (float)(this.mUIWarpPercent * -6.0);
			SexyTransform2D sexyTransform2D2 = default(SexyTransform2D);
			sexyTransform2D2.Translate((float)(-(float)num14 / 2), (float)(-(float)num14 / 2));
			sexyTransform2D2.Scale(num6, num6);
			sexyTransform2D2.RotateRad(-num15);
			sexyTransform2D2.Translate((float)num8 + (float)(((double)num12 * Constants.mConstants.Scale_Long + (double)(num14 / 2) - (double)num8) * (double)num6), (float)num9 + (float)(((double)num13 * Constants.mConstants.Scale_Long + (double)(num14 / 2) - (double)num9) * (double)num6));
			sexyTransform2D2.Translate((float)((int)(0.0 * Constants.mConstants.Scale_Long * (double)num6)), (float)((int)(-24.0 * Constants.mConstants.Scale_Long * (double)num6)));
			Graphics.PushTransform(ref sexyTransform2D2);
			this.DrawScore(g);
			Graphics.PopTransform();
			num12 = (int)(this.mPuzzleMode ? 16f : (12f - num7 * (float)Constants.mConstants.BoardBej3_Warp_Text_Target_X));
			num13 = (int)(125f + num7 * (float)Constants.mConstants.BoardBej3_Warp_Text_Target_Y);
			num14 = 100;
			num15 = (float)(this.mUIWarpPercent * -4.0);
			SexyTransform2D sexyTransform2D3 = default(SexyTransform2D);
			sexyTransform2D3.Translate((float)(-(float)num14 / 2), (float)(-(float)num14 / 2));
			sexyTransform2D3.Scale(num6, num6);
			sexyTransform2D3.RotateRad(num15);
			sexyTransform2D3.Translate((float)num8 + (float)(((double)num12 * Constants.mConstants.Scale_Long + (double)(num14 / 2) - (double)num8) * (double)num6), (float)num9 + (float)(((double)num13 * Constants.mConstants.Scale_Long + (double)(num14 / 2) - (double)num9) * (double)num6));
			sexyTransform2D3.Translate((float)((int)((double)(-(double)(this.mPuzzleMode ? 16 : 12)) * Constants.mConstants.Scale_Long * (double)num6)), (float)((int)(-125.0 * Constants.mConstants.Scale_Long * (double)num6)));
			Graphics.PushTransform(ref sexyTransform2D3);
			this.DrawUI(g);
			Graphics.PopTransform();
		}

		public override void Draw(Graphics g)
		{
			if (this.mbFirstDraw)
			{
				g.DrawImage(AtlasResources.GetImageInAtlasById(10054), 0, 0, new TRect(0, 0, Constants.mConstants.LASER_GLOW_LENGTH, Constants.mConstants.LASER_GLOW_LENGTH));
				this.mbFirstDraw = false;
			}
			int num = (int)this.mInterfaceOffset.GetOutVal();
			if (this.mFirstWhirlDraw)
			{
				this.mApp.PlaySample(Resources.SOUND_WHIRLPOOL_START);
				this.mFirstWhirlDraw = false;
			}
			base.DeferOverlay(1);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(255, 255, 255, 255));
			if (this.mState == Board.BoardState.STATE_WHIRLPOOL)
			{
				this.Draw3DWhirlpoolState(g);
			}
			else
			{
				this.DrawBackdrop(g);
				g.Translate(-num * 2, 0);
				this.DrawScore(g, false, false);
				g.Translate(num, 0);
				g.Translate(num * 2, 0);
				this.DrawBoard(g);
				g.Translate(-num, 0);
				if (this.mPauseFade < 1f && this.mApp.mDialogMap.Count == 0 && this.actionSheet == null)
				{
					this.DrawPauseMessage(g);
				}
			}
			g.EndFrame();
			g.BeginFrame();
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
		}

		private void DrawPauseMessage(Graphics g)
		{
			g.SetFont(Resources.FONT_BUTTON);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255.0 * (1.0 - (double)this.mPauseFade))));
			int num;
			int num2;
			base.GetBoardCenter(out num, out num2);
			num += (int)this.mInterfaceOffset.GetOutVal();
			g.DrawString(BoardBejLive.GamePausedString, num - g.GetFont().StringWidth(BoardBejLive.GamePausedString) / 2, num2 - g.GetFont().StringHeight(BoardBejLive.GamePausedString));
			g.DrawString(BoardBejLive.TapToContinueString, num - g.GetFont().StringWidth(BoardBejLive.TapToContinueString) / 2, num2);
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			this.SwitchOrientation();
			Vector2 position = (this.mApp.IsLandscape() ? new Vector2((float)Constants.mConstants.Board_Board_X - (float)AtlasResources.IMAGE_FRAME_TOP_BACK.mWidth * 0.4f, (float)(this.mHeight / 2)) : new Vector2((float)(this.mWidth / 2), (float)(Constants.mConstants.Board_Board_Y + AtlasResources.IMAGE_GRID.mHeight) + (float)AtlasResources.IMAGE_FRAME_TOP_BACK.mWidth * 0.4f));
			Vector2 size = (this.mApp.IsLandscape() ? new Vector2((float)AtlasResources.IMAGE_FRAME_TOP_BACK.mWidth, (float)this.mHeight) : new Vector2((float)this.mWidth, (float)AtlasResources.IMAGE_FRAME_TOP_BACK.mWidth));
			float spreadMax = (this.mApp.IsLandscape() ? (-1.57079637f) : 0f);
			foreach (ParticleEmitter particleEmitter in this.progressBarEmitters)
			{
				particleEmitter.Position = position;
				particleEmitter.Size = size;
				particleEmitter.SpreadMin = (particleEmitter.SpreadMax = spreadMax);
				particleEmitter.ClearParticles();
			}
			foreach (ParticleEmitter particleEmitter2 in this.lowerLayerEmitters)
			{
				particleEmitter2.ClearParticles();
			}
			foreach (ParticleEmitter particleEmitter3 in this.upperLayerEmitters)
			{
				particleEmitter3.ClearParticles();
			}
		}

		public override void ResizeButtons()
		{
			int num = (int)this.mInterfaceOffset.GetOutVal();
			if (this.mPuzzleMode || this.mTimedMode)
			{
				if (this.mApp.IsLandscape())
				{
					this.mMenuHintButton.Move(Constants.mConstants.BoardBej3_MenuHintButton_X_Landscape - this.mMenuHintButton.mWidth / 2 - num, this.mHeight - this.mMenuHintButton.mHeight - Constants.mConstants.BoardBej3_MenuHintButton_Offset_Y);
					return;
				}
				this.mMenuHintButton.Move(Constants.mConstants.BoardBej3_MenuHintButton_X_Portrait - this.mMenuHintButton.mWidth / 2 - num, this.mHeight - this.mMenuHintButton.mHeight - Constants.mConstants.BoardBej3_MenuHintButton_Offset_Y);
				return;
			}
			else
			{
				if (this.mApp.IsLandscape())
				{
					this.mMenuHintButton.Move(Constants.mConstants.BoardBej3_MenuHintButton_X_Landscape - this.mMenuHintButton.mWidth / 2 - num, this.mHeight - this.mMenuHintButton.mHeight - Constants.mConstants.BoardBej3_MenuHintButton_Offset_Y);
					return;
				}
				this.mMenuHintButton.Move(Constants.mConstants.BoardBej3_MenuHintButton_X_Portrait - this.mMenuHintButton.mWidth / 2 - num, this.mHeight - this.mMenuHintButton.mHeight - Constants.mConstants.BoardBej3_MenuHintButton_Offset_Y);
				return;
			}
		}

		public override int GetColAt(int theX)
		{
			for (int i = 0; i < 8; i++)
			{
				int colX = base.GetColX(i);
				if (theX >= colX && theX < colX + GameConstants.GEM_WIDTH)
				{
					return i;
				}
			}
			return -1;
		}

		public override int GetRowAt(int theY)
		{
			for (int i = 0; i < 8; i++)
			{
				int rowY = base.GetRowY(i);
				if (theY >= rowY && theY < rowY + GameConstants.GEM_HEIGHT)
				{
					return i;
				}
			}
			return -1;
		}

		protected void CheckForHints()
		{
			if (this.mPuzzleMode)
			{
				return;
			}
			PieceBejLive pieceBejLive = null;
			PieceBejLive pieceBejLive2 = null;
			PieceBejLive pieceBejLive3 = null;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive4 = this.mBoard[i, j];
					if (pieceBejLive4 != null)
					{
						if (pieceBejLive4.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
						{
							pieceBejLive = pieceBejLive4;
						}
						else if (pieceBejLive4.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
						{
							pieceBejLive2 = pieceBejLive4;
						}
						else if (pieceBejLive4.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
						{
							pieceBejLive3 = pieceBejLive4;
						}
					}
				}
			}
			if (pieceBejLive2 != null && !this.mSeenPowergemHint)
			{
				if (this.mApp.mProfile.WantsHint(Profile.Hint.HINT_PIECE_POWER_GEM))
				{
					this.mPointsManager.KillPoints();
					this.mHintPiece = pieceBejLive2;
					this.mHintAngle = 0f;
					this.alert = new Alert(Dialogs.DIALOG_PIECE_HINT_POWER_GEM, this, this.mApp);
					this.alert.SetHeadingText(Strings.Power_Gem);
					this.alert.SetBodyText(Strings.Board_Help_Message_Power_Gem);
					this.alert.AddButton(Dialogoid.ButtonID.YES_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_GREEN, Strings.Continue);
					this.alert.Present();
				}
				this.mSeenPowergemHint = true;
				return;
			}
			if (pieceBejLive != null && !this.mSeenHypercubeHint)
			{
				if (this.mApp.mProfile.WantsHint(Profile.Hint.HINT_PIECE_HYPER_CUBE))
				{
					this.mPointsManager.KillPoints();
					this.mHintPiece = pieceBejLive;
					this.mHintAngle = 0f;
					this.alert = new Alert(Dialogs.DIALOG_PIECE_HINT_HYPER_CUBE, this, this.mApp);
					this.alert.SetHeadingText(Strings.HYPER_CUBE);
					this.alert.SetBodyText(Strings.Board_Help_Message_Hyper_Gem);
					this.alert.AddButton(Dialogoid.ButtonID.YES_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_GREEN, Strings.Continue);
					this.alert.Present();
				}
				this.mSeenHypercubeHint = true;
				return;
			}
			if (pieceBejLive3 != null && !this.mSeenLasergemHint)
			{
				if (this.mApp.mProfile.WantsHint(Profile.Hint.HINT_PIECE_LASER_GEM))
				{
					this.mPointsManager.KillPoints();
					this.mHintPiece = pieceBejLive3;
					this.mHintAngle = 0f;
					this.alert = new Alert(Dialogs.DIALOG_PIECE_HINT_LASER_GEM, this, this.mApp);
					this.alert.SetHeadingText(Strings.LASER_GEM);
					this.alert.SetBodyText(Strings.Board_Help_Message_Laser_Gem);
					this.alert.AddButton(Dialogoid.ButtonID.YES_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_GREEN, Strings.Continue);
					this.alert.Present();
				}
				this.mSeenLasergemHint = true;
			}
		}

		private void UpdateTrialMode()
		{
			if (SexyAppBase.IsInTrialMode)
			{
				if (this.mState != Board.BoardState.STATE_WHIRLPOOL && this.mPauseCount == 0 && this.alert == null)
				{
					this.trialGameTime -= 0.01;
				}
				if (this.TrialExpired())
				{
					if (this.mState == Board.BoardState.STATE_MAKE_MOVE || this.mState == Board.BoardState.STATE_BOARD_STILL)
					{
						this.TrialLimitReached();
					}
					this.trialGameTime = 0.0;
				}
			}
		}

		private bool TrialExpired()
		{
			return SexyAppBase.IsInTrialMode && this.trialGameTime <= 0.0;
		}

		private void TrialLimitReached()
		{
			if (this.trialExpired)
			{
				return;
			}
			this.mGoDelay = 0;
			ReportAchievement.GivePendingAchievements();
			this.mApp.mEffectOverlay.DoStrechyText(Strings.Board_Trial_Up_Message_Center_Of_Screen, 80);
			this.mApp.PlaySample(Resources.SOUND_TIME_UP);
			this.GameOver(300);
			this.trialExpired = true;
		}

		private void DoComplement(int theComplementNum)
		{
			this.mApp.PlayVoice(BoardBejLive.gComplements[theComplementNum]);
			this.mComplementNum = theComplementNum;
			this.mComplementAlpha.SetCurve(CurvedValDefinition.complementAlphaCurve);
			this.mComplementScale.SetCurve(CurvedValDefinition.complementScaleCurve);
			this.mLastComplement = theComplementNum;
			this.mApp.mEffectOverlay.DoStrechyText(BoardBejLive.gComplementStrings[theComplementNum], 80);
		}

		public override void Update()
		{
			int num = this.mGameTicks;
			this.miCurrOrientation = this.mApp.mInterfaceOrientation;
			if (this.miCurrOrientation != this.miPrevOrientation)
			{
				this.SwitchOrientation();
				this.miPrevOrientation = this.miCurrOrientation;
			}
			float num2 = 1f;
			float num3 = Constants.mConstants.M(0.95f);
			this.aSpeed = num3 * this.aSpeed + (1f - num3) * num2;
			if ((double)Math.Abs(this.aSpeed - num2) < 0.01)
			{
				this.aSpeed = num2;
			}
			this.aSpeed *= this.GetGameSpeed();
			this.mUpdateAcc += this.aSpeed;
			int num4 = (int)this.mUpdateAcc;
			this.mUpdateAcc -= (float)num4;
			for (int i = 0; i < num4; i++)
			{
				this.DoUpdate();
			}
			base.Update();
			this.UpdateTrialMode();
			if (this.IsGameSuspended())
			{
				return;
			}
			if (this.mWantGemsCleared == 0)
			{
				this.mWantGemsCleared = (int)Constants.mConstants.M(20f);
			}
			if (this.mLapseGameTicks >= 400 && num != this.mGameTicks)
			{
				if (!this.mApp.mProfile.WantsHint(Profile.Hint.HINT_MOVE))
				{
					this.mWantGemsCleared = (int)Math.Max(5L, (long)(this.mWantGemsCleared - this.mRand.Next() % 5));
				}
				else
				{
					this.mWantGemsCleared = (int)Math.Max(5L, (long)(this.mWantGemsCleared - (this.mRand.Next() % 5 + 1)));
				}
				this.mLapseGameTicks = 0;
			}
			if (this.mDidTimeUp)
			{
				this.mTimeUpCount++;
			}
			if ((BoardBejLive.mBlitzFlags & 8) == 0)
			{
				this.mSpeedModeFactor.SetConstant(1.0);
			}
			if ((BoardBejLive.mBlitzFlags & 4) == 0)
			{
				this.mSpeedBonusNum = 0.0;
			}
			if (this.HasBoost(Boost.BOOST_FREEZE_BUTTON) && this.mSpeedBonusCount >= 10)
			{
				float num5 = Constants.mConstants.M(100f) + (float)Math.Min(10, this.mSpeedBonusCount + 1) * Constants.mConstants.M(13.75f);
				uint num6 = (uint)(this.mIdleTicks - this.mLastMatchTick);
				if (num6 >= num5 - Constants.mConstants.M(35f) && this.mSwapDataVector.Count == 0)
				{
					this.mFrozen = true;
					this.ClearBoost(Boost.BOOST_FREEZE_BUTTON);
				}
			}
		}

		protected void UpdateBonusPenalty()
		{
			float num = Math.Min(this.mLevelBarPct + 0.65f, 1f);
			this.mBonusPenalty += this.mTimedPenaltyVel * (double)num / 100.0;
			this.mTimedPenaltyVel += this.mTimedPenaltyAccel * (double)num / 100.0;
			this.mTimedPenaltyAccel += this.mTimedPenaltyJerk * (double)num / 100.0;
		}

		public void LoadLevelBackdrops()
		{
			this.mBackdropNum = this.mLevel % GameApp.NumberOfBackdrops;
			this.mNextBackdropNum = (this.mBackdropNum + 1) % GameApp.NumberOfBackdrops;
			base.StartPrepareLevelImages();
		}

		public override void LeftTrialmode()
		{
			this.SetMenuHintButtonUsage();
			this.ResizeButtons();
		}

		protected override void DoUpdate()
		{
			if (this.alert != null)
			{
				return;
			}
			if (this.newLevelStarted && this.mState == Board.BoardState.STATE_BOARD_STILL)
			{
				this.SaveGame();
				this.newLevelStarted = false;
			}
			if ((this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_SHOWING || this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_HIDING) && !this.mApp.FocusPaused())
			{
				this.mFrozen = true;
				bool flag = !this.mInterfaceOffset.IncInVal(0.02);
				int num = (int)this.mInterfaceOffset.GetOutVal();
				if (this.mPuzzleStateOverlay != null)
				{
					this.mPuzzleStateOverlay.Resize(-num, 0, this.mApp.mWidth, this.mApp.mHeight);
				}
				this.ResizeButtons();
				if (this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_HIDING)
				{
					this.mUserPaused = true;
					if (this.mPauseFade != 0f)
					{
						this.mPauseFade = Math.Max(this.mPauseFade - 0.05f, 0f);
						this.MarkDirty();
					}
					this.mPointsManager.Pause(true, this.mPauseFade);
				}
				else
				{
					this.mUserPaused = false;
					if (this.mPauseFade < 1f)
					{
						this.mPauseFade = Math.Min(this.mPauseFade + 0.05f, 1f);
						this.MarkDirty();
					}
					this.mPointsManager.Pause(false, this.mPauseFade);
				}
				if (flag)
				{
					if (this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_SHOWING)
					{
						this.mSHOWSTATE = Board.SHOWSTATE.SHOWSTATE_SHOWN;
						this.mFrozen = false;
					}
					else
					{
						this.mSHOWSTATE = Board.SHOWSTATE.SHOWSTATE_HIDDEN;
						this.mFrozen = true;
					}
				}
			}
			this.mPointsManager.Pause(this.mPauseCount > 0, this.mPauseFade);
			if (this.mPauseCount == this.mVisPauseCount && this.mVisPauseCount > 0)
			{
				if (this.mPauseFade != 0f)
				{
					this.UpdateBonusPenalty();
					this.mPauseFade = Math.Max(this.mPauseFade - 0.05f, 0f);
					this.MarkDirty();
				}
				return;
			}
			if (this.mPauseCount > 0)
			{
				return;
			}
			if (this.mBlitzMode)
			{
				if ((double)this.mLevelBarPct <= 0.25)
				{
					this.mFlashBarRed = this.mUpdateCnt / 8 % 2 == 0;
					if (this.miWarningSoundPlaying == 0 && this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_SHOWN)
					{
						this.mApp.PlaySample(Resources.SOUND_WARNING);
						this.miWarningSoundPlaying = Math.Max(50, (int)(100f * (1f - (0.25f - this.mLevelBarPct) / 0.25f)));
					}
					if (this.miWarningSoundPlaying != 0 && !this.mDidTimeUp)
					{
						this.miWarningSoundPlaying--;
					}
				}
				else
				{
					this.mFlashBarRed = false;
				}
			}
			if (this.mTimedMode && (this.mState == Board.BoardState.STATE_MAKE_MOVE || this.mState == Board.BoardState.STATE_BOARD_STILL) && this.mLevelBarPct <= 0f)
			{
				this.mFlashBarRed = true;
				this.mApp.mEffectOverlay.DoStrechyText(Strings.Board_Time_Up, 80);
				this.mApp.PlaySample(Resources.SOUND_TIME_UP);
				this.GameOver(200);
			}
			if (--this.mShowLevelDelay == 0)
			{
				if (this.mBlitzMode)
				{
					this.mApp.mEffectOverlay.DoStrechyText("1:00", 80);
				}
				else
				{
					this.mApp.mEffectOverlay.DoStrechyText(this.mDispLevelStretchy, 80);
				}
			}
			if (--this.mGoDelay == 0)
			{
				this.mApp.PlaySample(Resources.SOUND_GO);
				this.mApp.mEffectOverlay.DoStrechyText(BoardBejLive.GoString, 0);
			}
			if (this.mShowingSun)
			{
				this.mSunPosition += 0.005f;
				if (this.mSunPosition > 1f)
				{
					this.mShowingSun = false;
				}
				this.MarkDirty();
			}
			else if (Board.Rand() % 6000 == 0 && !this.mDidTimeUp)
			{
				this.mShowingSun = true;
				this.mSunPosition = 0f;
			}
			if (this.mUserPaused || this.mApp.GetDialog(Dialogs.DIALOG_OPTIONS) != null || this.mApp.GetDialog(Dialogs.DIALOG_RESET_HIGH_SCORES) != null)
			{
				this.mBoardHidePct = Math.Min(1f, this.mBoardHidePct + this.mBoardHidePct + Constants.mConstants.M(0.035f));
				if (this.mUserPaused)
				{
					this.mVisPausePct = Math.Min(1f, this.mVisPausePct + Constants.mConstants.M(0.035f));
				}
				return;
			}
			this.mBoardHidePct = Math.Max(0f, this.mBoardHidePct - Constants.mConstants.M(0.075f));
			this.mVisPausePct = Math.Max(0f, this.mVisPausePct - Constants.mConstants.M(0.075f));
			if (this.mSelectedPiece != null && this.mUpdateCnt % 8 == 0)
			{
				this.MarkDirty();
				this.mSelectAnimCnt++;
			}
			if (this.mMouseDown && this.mSelectedPiece != null)
			{
				this.MouseDrag(this.mWidgetManager.mLastMouseX, this.mWidgetManager.mLastMouseY);
			}
			PieceBejLive pieceBejLive = null;
			if (this.mWidgetManager != null)
			{
				pieceBejLive = this.GetPieceAt(this.mWidgetManager.mLastMouseX, this.mWidgetManager.mLastMouseY);
			}
			PieceBejLive pieceBejLive2 = this.GetSelectedPiece();
			if (pieceBejLive != null && pieceBejLive2 != null && pieceBejLive.mColor > -1 && pieceBejLive.mColor < 7 && pieceBejLive2.IsFlagSet(2) && Math.Abs(pieceBejLive.mRow - pieceBejLive2.mRow) + Math.Abs(pieceBejLive.mCol - pieceBejLive2.mCol) == 1)
			{
				this.mHyperGemColorFactor = Math.Min(this.mHyperGemColorFactor + Constants.mConstants.M(0.01f), 1f);
				this.mHyperGemColor.Lerp(BoardBejLive.gGemColors[pieceBejLive.mColor]);
			}
			else
			{
				this.mHyperGemColorFactor = Math.Max(this.mHyperGemColorFactor - Constants.mConstants.M(0.01f), 0f);
			}
			for (int i = 0; i < this.mQueuedMoveVector.Count; i++)
			{
				QueuedMove queuedMove = this.mQueuedMoveVector[i];
				if (this.mUpdateCnt >= queuedMove.mUpdateCnt)
				{
					if (queuedMove.mUpdateCnt == this.mUpdateCnt)
					{
						pieceBejLive2 = this.GetPieceById(queuedMove.mSelectedId);
						if (pieceBejLive2 != null)
						{
							this.BlitzTrySwap(pieceBejLive2, queuedMove.mSwappedRow, queuedMove.mSwappedCol, queuedMove.mForceSwap, queuedMove.mPlayerSwapped);
						}
					}
					int num2 = this.mUpdateCnt - 1;
					if (this.mMoveDataVector.Count > 0)
					{
						num2 = this.mMoveDataVector[0].mUpdateCnt;
					}
					if (queuedMove.mUpdateCnt < num2)
					{
						this.mQueuedMoveVector[i].PrepareForResue();
						this.mQueuedMoveVector.RemoveAt(i);
						i--;
					}
				}
			}
			if (this.UpdateBulging())
			{
				for (int j = 0; j < 8; j++)
				{
					for (int k = 0; k < 8; k++)
					{
						PieceBejLive pieceBejLive3 = this.mBoard[j, k];
						if (pieceBejLive3 != null)
						{
							pieceBejLive3.mSelectorAlpha.IncInVal();
							if (pieceBejLive3.mRotPct != 0f || pieceBejLive3.mSelected)
							{
								pieceBejLive3.mRotPct += Constants.mConstants.M(0.02f);
								if (pieceBejLive3.mRotPct >= 1f)
								{
									pieceBejLive3.mRotPct = 0f;
								}
							}
						}
					}
				}
				return;
			}
			if (this.mFirstUpdate)
			{
				this.mBackdropNum = this.mLevel % GameApp.NumberOfBackdrops;
				this.mNextBackdropNum = (this.mBackdropNum + 1) % GameApp.NumberOfBackdrops;
				base.StartPrepareLevelImages();
				base.ProcessLevelImages();
				this.mFirstUpdate = false;
			}
			if (this.mState == Board.BoardState.STATE_GAME_OVER_ANIM || this.mState == Board.BoardState.STATE_GAME_OVER_FALL || this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY)
			{
				this.UpdateGameOverAnim();
			}
			this.UpdateGame();
			if ((this.mState == Board.BoardState.STATE_BOARD_STILL || this.mState == Board.BoardState.STATE_MAKE_MOVE) && this.mTimerDelay > 0)
			{
				this.mTimerDelay--;
			}
			if (this.mState == Board.BoardState.STATE_BOARD_STILL || this.mState == Board.BoardState.STATE_MAKE_MOVE || this.mState == Board.BoardState.STATE_GAME_OVER_ANIM || this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY || this.mState == Board.BoardState.STATE_GAME_OVER_FALL)
			{
				ReportAchievement.GivePendingAchievements();
			}
			if (this.mTimedMode && this.mTimerDelay <= 0 && (this.mState == Board.BoardState.STATE_MAKE_MOVE || this.mState == Board.BoardState.STATE_BOARD_STILL || this.mState == Board.BoardState.STATE_SWAP_INVALID || this.mState == Board.BoardState.STATE_SWAPPING_BACK))
			{
				this.UpdateBonusPenalty();
			}
			this.UpdateparticleEmitters();
		}

		private void UpdateparticleEmitters()
		{
			foreach (ParticleEmitter particleEmitter in this.lowerLayerEmitters)
			{
				particleEmitter.Update();
			}
			foreach (ParticleEmitter particleEmitter2 in this.upperLayerEmitters)
			{
				particleEmitter2.Update();
			}
			foreach (ParticleEmitter particleEmitter3 in this.progressBarEmitters)
			{
				particleEmitter3.Update();
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.TrialExpired())
			{
				return;
			}
			if (this.mPauseCount != 0)
			{
				this.Unpause(true);
				return;
			}
			if (this.mUserPaused || this.mVisPausePct > 0.5f)
			{
				this.mUserPaused = false;
				return;
			}
			if (!this.CanPlay())
			{
				return;
			}
			this.mMouseDown = true;
			this.mMouseDownX = x;
			this.mMouseDownY = y;
			if (this.mState == Board.BoardState.STATE_GAME_OVER_ANIM || this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY || this.mState == Board.BoardState.STATE_WHIRLPOOL || this.mState == Board.BoardState.STATE_LEVEL_TRANSITION)
			{
				return;
			}
			int num = this.GetColAt(x);
			int num2 = this.GetRowAt(y);
			if (num == -1 || num2 == -1)
			{
				this.mSelectedPiece = null;
				return;
			}
			PieceBejLive selectedPiece = this.GetSelectedPiece();
			PieceBejLive pieceBejLive = this.GetPieceAt(x, y);
			bool flag = false;
			if (pieceBejLive == null)
			{
				pieceBejLive = this.GetPieceAtRowCol(num2, num);
				if (pieceBejLive != null)
				{
					flag = true;
				}
			}
			else
			{
				num = pieceBejLive.mCol;
				num2 = pieceBejLive.mRow;
				if (this.mSelectedPiece == pieceBejLive && !this.mEditing)
				{
					this.mouseWentDownOnSelected = true;
				}
				else
				{
					this.mouseWentDownOnSelected = false;
					if (!this.mEditing)
					{
						PieceBejLive pieceBejLive2 = this.MoveAssistedPiece(pieceBejLive, x, y, selectedPiece);
						if (pieceBejLive2 != null)
						{
							pieceBejLive = pieceBejLive2;
							num = pieceBejLive.mCol;
							num2 = pieceBejLive.mRow;
						}
					}
					this.mSelectAnimCnt = 0;
					this.mSelectedPiece = pieceBejLive;
					this.mSelectedPiece.mbDrawSelector = true;
					this.mApp.PlaySample(Resources.SOUND_SELECT);
				}
			}
			if (!flag && pieceBejLive != selectedPiece)
			{
				if (selectedPiece != null)
				{
					bool flag2 = this.QueueSwap(selectedPiece, num2, num);
					if (flag2)
					{
						this.ResetChainCount();
					}
					if (this.mLightningStorms.Count == 0 && !flag2)
					{
						selectedPiece.mSelected = false;
						selectedPiece.mSelectorAlpha.SetConstant(0.0);
						if (pieceBejLive != null)
						{
							pieceBejLive.mSelected = true;
							pieceBejLive.mSelectorAlpha.SetConstant(1.0);
							return;
						}
					}
				}
				else if (pieceBejLive != null)
				{
					if (pieceBejLive.IsButton())
					{
						this.QueueSwap(pieceBejLive, pieceBejLive.mRow, pieceBejLive.mCol, false, true);
						this.ResetChainCount();
						return;
					}
					pieceBejLive.mSelected = true;
					pieceBejLive.mSelectorAlpha.SetConstant(1.0);
					this.mApp.PlaySample(Resources.SOUND_SELECT);
					return;
				}
			}
			else if (selectedPiece != null)
			{
				selectedPiece.mSelected = false;
				selectedPiece.mSelectorAlpha.SetConstant(0.0);
			}
		}

		private void ResetChainCount()
		{
			this.mMovePoints = 0;
			if (!this.mInBonus && this.mChainCount > this.mLongestChainReaction)
			{
				this.mLongestChainReaction = this.mChainCount;
			}
			this.mChainCount = 0;
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			this.mMouseDown = false;
			if (this.mEditing)
			{
				PieceBejLive pieceAt = this.GetPieceAt(x, y);
				if (pieceAt == this.mSelectedPiece && this.mSelectedPiece != null)
				{
					if (this.mSelectedPiece.mFlags != 0)
					{
						if (this.mSelectedPiece.IsFlagSet(2))
						{
							this.mSelectedPiece.mColor = this.mSelectedPiece.mLastColor;
						}
						this.RemoveGemsFromBlitzBatch(this.mSelectedPiece);
						this.RemoveGemsFromMultiFontBatch(this.mSelectedPiece);
						this.mSelectedPiece.ClearFlags();
						this.mSelectedPiece.mShrinking = false;
						this.mSelectedPiece.mImmunityCount = (int)Constants.mConstants.M(25f);
						return;
					}
					if (this.mSelectedPiece.mColor >= 0 && this.mSelectedPiece.mColor < 7 && this.mSelectedPiece.mFlags == 0)
					{
						this.mSelectedPiece.mColor = (this.mSelectedPiece.mColor + 1) % 7;
						return;
					}
				}
			}
			else if (this.mouseWentDownOnSelected && this.mSelectedPiece != null)
			{
				this.mSelectedPiece.mbDrawSelector = false;
				this.mSelectedPiece = null;
			}
		}

		public override void MouseDrag(int x, int y)
		{
			if (!this.mMouseDown || this.mSelectedPiece == null)
			{
				return;
			}
			base.MouseDrag(x, y);
			PieceBejLive selectedPiece = this.GetSelectedPiece();
			int num = x - this.mMouseDownX;
			int num2 = y - this.mMouseDownY;
			if ((float)Math.Abs(num) >= Constants.mConstants.MS(40f) || (float)Math.Abs(num2) >= Constants.mConstants.MS(40f))
			{
				if (this.mEditing && this.mSelectedPiece.mFlags == 0)
				{
					if (Math.Abs(num) > Math.Abs(num2))
					{
						if (num > 0)
						{
							this.Flamify(selectedPiece);
							return;
						}
						if (num < 0)
						{
							this.Laserify(selectedPiece);
							return;
						}
					}
					else
					{
						if (num2 > 0 && this.mMultiplierCount < this.MAX_MULTIPLIER - 1)
						{
							this.RemoveGemsFromBlitzBatch(this.mSelectedPiece);
							this.RemoveGemsFromMultiFontBatch(this.mSelectedPiece);
							this.mSelectedPiece.ClearFlags();
							this.mSelectedPiece.mShrinking = false;
							this.mSelectedPiece.mImmunityCount = (int)Constants.mConstants.M(25f);
							this.mSelectedPiece.SetFlag(6);
							this.mMultiplierCount++;
							return;
						}
						if (num2 < 0)
						{
							this.Hypergemify(selectedPiece);
							return;
						}
					}
				}
				else
				{
					TPoint a = new TPoint(-1, -1);
					if (Math.Abs(num) > Math.Abs(num2))
					{
						if (num > 0 && selectedPiece.mCol < 7)
						{
							a = new TPoint(selectedPiece.mCol + 1, selectedPiece.mRow);
						}
						else if (num < 0 && selectedPiece.mCol > 0)
						{
							a = new TPoint(selectedPiece.mCol - 1, selectedPiece.mRow);
						}
					}
					else if (num2 > 0 && selectedPiece.mRow < 7)
					{
						a = new TPoint(selectedPiece.mCol, selectedPiece.mRow + 1);
					}
					else if (num2 < 0 && selectedPiece.mRow > 0)
					{
						a = new TPoint(selectedPiece.mCol, selectedPiece.mRow - 1);
					}
					if (a != new TPoint(-1, -1))
					{
						this.QueueSwap(selectedPiece, a.mY, a.mX);
					}
				}
			}
		}

		public override void MouseMove(int x, int y)
		{
		}

		public override void ButtonPress(int theId)
		{
			if (this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_FADING_OUT)
			{
				return;
			}
			this.mApp.PlaySample(Resources.SOUND_CLICK);
		}

		public override void ButtonDepress(int buttonId)
		{
			if (this.mMenuHintButton.mDisabled || !this.mMenuHintButton.mVisible)
			{
				return;
			}
			if (this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_FADING_OUT)
			{
				return;
			}
			switch (buttonId)
			{
			case 0:
				this.Pause(true);
				if (this.IsInProgress())
				{
					this.SaveGame();
				}
				this.ShowMenu();
				return;
			case 1:
				this.ShowHint();
				return;
			case 2:
				break;
			case 3:
				if (this.mApp.CheatsEnabled() && this.mTimedMode)
				{
					this.mBonusPenalty = (double)(this.mNextQuakePoints - this.mLastQuakePoints);
					return;
				}
				if (this.mPauseCount > 0)
				{
					this.Unpause(true);
					return;
				}
				this.Pause(true);
				return;
			default:
				switch (buttonId)
				{
				case 400:
					break;
				case 401:
					if (this.mState == Board.BoardState.STATE_PUZZLE_MODE_DONE)
					{
						this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
						this.KillTutorialButtons();
						return;
					}
					break;
				default:
					if (buttonId != 500)
					{
						return;
					}
					this.mState = Board.BoardState.STATE_PUZZLE_AT_END_CLOSE;
					this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
					this.KillTutorialButtons();
					this.HideNow();
					break;
				}
				break;
			}
		}

		private void ShowMenu()
		{
			this.actionSheet = ActionSheet.GetNewActionSheet(true, 41, this, this.mApp, Strings.Menu_Pause);
			this.actionSheet.AddButton(2000, Strings.Menu_Resume, SmallButtonColors.SMALL_BUTTON_GREEN);
			this.actionSheet.AddButton(2001, Strings.Menu_Achievements, SmallButtonColors.SMALL_BUTTON_BLUE);
			this.actionSheet.AddButton(2002, Strings.Menu_Leaderboards, SmallButtonColors.SMALL_BUTTON_BLUE);
			this.actionSheet.AddButton(2003, Strings.Menu_Help_Options, SmallButtonColors.SMALL_BUTTON_PURPLE);
			if (SexyAppBase.IsInTrialMode)
			{
				this.actionSheet.AddButton(2004, Strings.Unlock_Full_Game, SmallButtonColors.SMALL_BUTTON_BLUE);
			}
			this.actionSheet.AddButton(2005, Strings.Menu_Main_Menu, SmallButtonColors.SMALL_BUTTON_RED);
			this.actionSheet.Present();
		}

		public override bool BackButtonPress()
		{
			if (this.mApp.mInterfaceState == InterfaceStates.UI_STATE_GAME)
			{
				if (this.actionSheet != null)
				{
					this.actionSheet.BackButtonPress();
				}
				else if (this.alert != null)
				{
					this.alert.BackButtonPress();
				}
				else
				{
					this.ButtonDepress(0);
				}
				return true;
			}
			return false;
		}

		public override void DialogButtonDepress(int dialogId, int buttonId)
		{
			this.alert = null;
			switch (dialogId)
			{
			case 12:
			case 14:
				break;
			case 13:
				if (1000 == buttonId)
				{
					this.ShowHint();
					return;
				}
				break;
			case 15:
				this.mHintPiece = null;
				if (1000 == buttonId)
				{
					this.mApp.mProfile.DisableHint(Profile.Hint.HINT_PUZZLE_END_HINT);
					return;
				}
				break;
			case 16:
				this.mHintPiece = null;
				if (1000 == buttonId)
				{
					this.mApp.mProfile.DisableHint(Profile.Hint.HINT_TIMER);
					return;
				}
				break;
			default:
				switch (dialogId)
				{
				case 28:
					this.mHintPiece = null;
					if (1000 == buttonId)
					{
						this.mApp.mProfile.DisableHint(Profile.Hint.HINT_PIECE_POWER_GEM);
						return;
					}
					break;
				case 29:
					this.mHintPiece = null;
					if (1000 == buttonId)
					{
						this.mApp.mProfile.DisableHint(Profile.Hint.HINT_PIECE_HYPER_CUBE);
						return;
					}
					break;
				case 30:
					this.mHintPiece = null;
					if (1000 == buttonId)
					{
						this.mApp.mProfile.DisableHint(Profile.Hint.HINT_PIECE_LASER_GEM);
						return;
					}
					break;
				default:
					switch (dialogId)
					{
					case 41:
						switch (buttonId)
						{
						case 2001:
							this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_ACHIEVEMENTS_OVER_GAME);
							return;
						case 2002:
							LiveLeaderboardDialog.PrepareState((this.mApp.mGameMode == GameMode.MODE_TIMED) ? LeaderboardState.Action : LeaderboardState.Classic, LeaderboardViewState.Friends);
							this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_SCORES_OVER_GAME);
							return;
						case 2003:
							this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_INFO_OVER_GAME);
							return;
						case 2004:
							this.mApp.BuyGame();
							this.actionSheet = null;
							return;
						case 2005:
							this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
							return;
						default:
							this.Unpause(true);
							this.actionSheet.Dismiss();
							this.actionSheet = null;
							return;
						}
						break;
					case 42:
						if (buttonId != 3000)
						{
							return;
						}
						this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
						break;
					default:
						return;
					}
					break;
				}
				break;
			}
		}

		public override void DialogButtonPress(int theDialogId, int theButtonId)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
		}

		private int GetTotalGameTime()
		{
			return (int)(((long)this.mUpdateCnt - (long)((ulong)this.mTimePeriodStart)) / 100L) + this.mTimePlayedAdd;
		}

		private void GameOver(int aShakeLife)
		{
			if (this.mWantLevelup)
			{
				return;
			}
			this.DeleteSavedGame();
			this.mpHintPiece = (this.mHintPiece = null);
			int num = 0;
			this.mTotalGameTime = this.GetTotalGameTime();
			this.mShakeLife = aShakeLife;
			this.ProcessEndGameInfo();
			this.mMenuHintButton.FadeOut(0f, 1f);
			if (this.mBlitzMode)
			{
				if (!this.mDidTimeUp)
				{
					this.mPreHurrahPoints = this.mPoints;
					this.mApp.mEffectOverlay.DoStrechyText(Strings.Board_Time_Up, 80);
					this.mApp.PlaySample(Resources.SOUND_TIME_UP);
					this.mDidTimeUp = true;
					this.mGameSpeed.SetCurve(CurvedValDefinition.mGameSpeedCurve);
					this.mTimeUpAlpha.SetCurve(CurvedValDefinition.mTimeUpAlphaCurve);
					this.mTimeUpScale.SetCurve(CurvedValDefinition.mTimeUpScaleCurve);
					this.SetStat(Stats.STAT_TIME_UP_MULTIPLIER, this.mPointMultiplier);
				}
				if ((BoardBejLive.mBlitzFlags & 2) != 0)
				{
					for (int i = 0; i < 8; i++)
					{
						for (int j = 0; j < 8; j++)
						{
							PieceBejLive pieceBejLive = this.mBoard[i, j];
							if (pieceBejLive != null && (pieceBejLive.IsFlagSet(1) || pieceBejLive.IsFlagSet(2) || pieceBejLive.IsFlagSet(3) || pieceBejLive.IsFlagSet(6)))
							{
								if (this.mTimeUpCount == 0)
								{
									pieceBejLive.mExplodeDelay = (int)(Constants.mConstants.M(175f) + (float)num * Constants.mConstants.M(25f));
								}
								else
								{
									pieceBejLive.mExplodeDelay = (int)(Constants.mConstants.M(25f) + (float)num * Constants.mConstants.M(25f));
								}
								num++;
							}
						}
					}
				}
				if (num == 0)
				{
					this.mState = ((this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY) ? this.mState : Board.BoardState.STATE_GAME_OVER_ANIM);
					if (this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY)
					{
						this.mSlotsAlpha.SetCurve(CurvedValDefinition.mSlotsAlphaCurve);
						this.mSlotsSpinPct.SetCurve(CurvedValDefinition.mSlotsSpinPctCurve);
						if (this.mGameOverCount > 0 || this.mLevelCompleteCount > 0 || this.mWantLevelup)
						{
							return;
						}
						this.DeleteSavedGame();
						this.mGameOverCount = 1;
						this.SetStat(Stats.STAT_FINAL_MULTIPLIER, this.mPointMultiplier);
						this.SetStat(Stats.STAT_HURRAH_POINTS, this.mPoints - this.mPreHurrahPoints);
						this.SetStat(Stats.STAT_WAS_GAME_COMPLETED, 1);
						this.mApp.mFacebook.recordBlitzGame(this.mPoints, true, this.GetGameStatString());
						this.mApp.DoGameOver(true);
						return;
					}
				}
			}
			else
			{
				this.mState = ((this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY) ? this.mState : Board.BoardState.STATE_GAME_OVER_ANIM);
				this.miGameOverTicks = aShakeLife;
				this.mDidTimeUp = true;
			}
		}

		protected void UpdateGameOverAnim()
		{
			if (this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY)
			{
				this.DeleteSavedGame();
				this.mGameOverCount = 1;
				this.mApp.DoGameOver(true);
			}
		}

		private void UpdateGame()
		{
			this.mSpeedBonusDisp.IncInVal();
			if (!this.IsGameSuspended())
			{
				this.mGameTicks++;
				this.mFindSetCounter++;
				if (this.mGameTicks >= 250)
				{
					this.mLapseGameTicks++;
				}
				this.mNoMoveCount++;
				this.AddToStat(Stats.STAT_SECONDS_PLAYED);
				float num = (float)(400 - this.miHintCoolDownCount) / 400f * 0.8f + 0.2f;
				this.mMenuHintButton.SetHintOpacity(num);
				this.mMenuHintButton.SetHintEnabled(num >= 1f);
				if (this.miHintCoolDownCount != 0)
				{
					this.miHintCoolDownCount--;
				}
			}
			if (this.miMultiPlayTick != 0)
			{
				this.miMultiPlayTick--;
			}
			if (this.mState != Board.BoardState.STATE_WHIRLPOOL && this.mState != Board.BoardState.STATE_GAME_OVER_ANIM && this.mState != Board.BoardState.STATE_GAME_OVER_DISPLAY && this.mState != Board.BoardState.STATE_GAME_OVER_FALL)
			{
				this.mState = ((this.IsBoardStill() && !this.mDidTimeUp) ? Board.BoardState.STATE_BOARD_STILL : this.mState);
			}
			if (this.mSHOWSTATE == Board.SHOWSTATE.SHOWSTATE_SHOWN)
			{
				if (this.mLightningStorms.Count == 0 && this.mSpeedBonusFlameModePct > 0f)
				{
					if (this.mState != Board.BoardState.STATE_LEVEL_COMPLETE && this.mState != Board.BoardState.STATE_LEVEL_TRANSITION && this.mState != Board.BoardState.STATE_WHIRLPOOL)
					{
						this.mSpeedBonusFlameModePct = Math.Max(0f, this.mSpeedBonusFlameModePct - Constants.mConstants.M(0.00125f));
					}
					if ((double)this.mSpeedBonusFlameModePct == 0.0)
					{
						this.mSpeedBonusNum = 0.0;
					}
				}
				if (this.mState != Board.BoardState.STATE_GAME_OVER_ANIM && this.mState != Board.BoardState.STATE_GAME_OVER_DISPLAY && this.mState != Board.BoardState.STATE_GAME_OVER_FALL)
				{
					if (this.miBufferCheckCount > 0)
					{
						this.FindSets(false);
						this.miBufferCheckCount = ((this.miBufferCheckCount - 1 <= 0) ? 0 : (this.miBufferCheckCount - 1));
					}
					if (this.mLightningStorms.Count == 0)
					{
						this.FillInBlanks();
					}
					this.UpdateMoveData();
					this.UpdateSwapping();
					if (!this.mEditing)
					{
						this.UpdateFalling();
					}
				}
				if (this.mState == Board.BoardState.STATE_WHIRLPOOL)
				{
					this.UpdateLevelUpEffect();
				}
			}
			if (!this.CanPlay())
			{
				PieceBejLive selectedPiece = this.GetSelectedPiece();
				if (selectedPiece != null)
				{
					selectedPiece.mSelected = false;
					selectedPiece.mSelectorAlpha.SetConstant(0.0);
				}
			}
			for (int i = 0; i < 8; i++)
			{
				this.mBumpVelocities[i] = Math.Min(0f, this.mBumpVelocities[i] + Constants.mConstants.Gravity);
			}
			for (int j = 0; j < 8; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					PieceBejLive pieceBejLive = this.mBoard[j, k];
					if (pieceBejLive != null)
					{
						if (pieceBejLive.mImmunityCount != 0)
						{
							pieceBejLive.mImmunityCount--;
						}
						if (pieceBejLive.mSpinSpeed != 0f)
						{
							int num2 = 20;
							pieceBejLive.mSpinFrame += (int)pieceBejLive.mSpinSpeed;
							if (pieceBejLive.mSpinFrame < 0)
							{
								pieceBejLive.mSpinFrame += num2;
							}
						}
					}
				}
			}
			bool flag = false;
			int num3 = 0;
			int num4 = 0;
			for (int l = 0; l < 8; l++)
			{
				for (int m = 0; m < 8; m++)
				{
					PieceBejLive pieceBejLive2 = this.mBoard[l, m];
					if (pieceBejLive2 != null && pieceBejLive2 != null)
					{
						if (this.mState == Board.BoardState.STATE_GAME_OVER_ANIM)
						{
							pieceBejLive2.Shake();
						}
						else if (this.mState == Board.BoardState.STATE_GAME_OVER_FALL)
						{
							pieceBejLive2.GameOverFall();
						}
						else
						{
							if (pieceBejLive2.mImmunityCount != 0)
							{
								pieceBejLive2.mImmunityCount--;
							}
							if (pieceBejLive2.mSpinSpeed != 0f)
							{
								int num5 = 20;
								pieceBejLive2.mSpinFrame += (int)pieceBejLive2.mSpinSpeed;
								if (pieceBejLive2.mSpinFrame < 0)
								{
									pieceBejLive2.mSpinFrame += num5;
								}
								if (pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_COUNTER))
								{
									if (pieceBejLive2.mSpinFrame >= num5)
									{
										pieceBejLive2.mSpinFrame = 0;
										pieceBejLive2.mSpinSpeed = 0f;
									}
									if (pieceBejLive2.mSpinSpeed != 0f && pieceBejLive2.mSpinFrame >= 5 && pieceBejLive2.mSpinFrame <= 10)
									{
										pieceBejLive2.mCounter--;
										pieceBejLive2.StampOverlay();
										pieceBejLive2.mSpinFrame = 16;
									}
								}
							}
							else
							{
								if (pieceBejLive2.mShineAnim)
								{
									pieceBejLive2.mShineFactor -= 0.012;
									if (pieceBejLive2.mShineFactor < 0.0)
									{
										pieceBejLive2.mShineAnim = false;
										this.MarkDirty();
									}
								}
								if (pieceBejLive2 == this.mSelectedPiece)
								{
									pieceBejLive2.mShineFactor += 0.045;
									if (pieceBejLive2.mShineFactor > 1.0)
									{
										pieceBejLive2.mShineFactor = 1.0;
									}
									if (!pieceBejLive2.mShineAnim)
									{
										pieceBejLive2.mShineAnim = true;
										pieceBejLive2.mShineAnimFrame = 0f;
									}
								}
								if (pieceBejLive2.mShineAnim)
								{
									pieceBejLive2.mShineAnimFrame += 0.0625f;
									if (pieceBejLive2.mShineAnimFrame >= 10f)
									{
										pieceBejLive2.mShineAnimFrame -= 10f;
									}
									this.MarkDirty();
								}
							}
							if (pieceBejLive2 != this.mSelectedPiece)
							{
								pieceBejLive2.mbDrawSelector = false;
							}
							if (pieceBejLive2.mSparkleLife > 0)
							{
								pieceBejLive2.mSparkleLife--;
								if (pieceBejLive2.mSparkleLife % 5 == 0)
								{
									pieceBejLive2.mSparkleFrame++;
									this.MarkDirty();
								}
							}
							if (pieceBejLive2.mExplodeDelay > 0 && --pieceBejLive2.mExplodeDelay == 0)
							{
								if (pieceBejLive2.IsFlagSet(19))
								{
									int mImmunityCount = pieceBejLive2.mImmunityCount;
									pieceBejLive2.mImmunityCount = 1;
									num4 += (int)pieceBejLive2.CX();
									num3++;
									this.ExplodeAt((int)pieceBejLive2.CX(), (int)pieceBejLive2.CY());
									flag = true;
									pieceBejLive2.mImmunityCount = mImmunityCount;
								}
								if (pieceBejLive2.IsFlagSet(1) && pieceBejLive2.IsFlagSet(3))
								{
									this.chainReactorSpecialsTriggered++;
									pieceBejLive2.mDestructing = true;
									this.DoStarGem(pieceBejLive2);
								}
								else if (pieceBejLive2.IsFlagSet(1) || (pieceBejLive2.IsFlagSet(3) && pieceBejLive2.mImmunityCount > 0))
								{
									num4 += (int)pieceBejLive2.CX();
									num3++;
									this.chainReactorSpecialsTriggered++;
									this.AddToStat(Stats.STAT_FLAMEGEMS_USED, 1, pieceBejLive2.mMoveCreditId);
									this.AddPoints((int)pieceBejLive2.CX(), (int)pieceBejLive2.CY(), (int)Constants.mConstants.M(20f), SexyColor.White, (uint)pieceBejLive2.mMatchId, true, true, pieceBejLive2.mMoveCreditId);
									this.ExplodeAt((int)pieceBejLive2.CX(), (int)pieceBejLive2.CY());
									flag = true;
								}
								else if ((!pieceBejLive2.IsFlagSet(2) && !pieceBejLive2.IsFlagSet(3) && !pieceBejLive2.IsFlagSet(16) && !pieceBejLive2.IsFlagSet(6)) || !this.TriggerSpecial(pieceBejLive2, null))
								{
									if (pieceBejLive2.IsFlagSet(11))
									{
										this.TallyPiece(pieceBejLive2);
										this.DeletePiece(pieceBejLive2);
									}
									else
									{
										this.SmallExplodeAt(pieceBejLive2, pieceBejLive2.CX(), pieceBejLive2.CY(), true, false);
										flag = true;
									}
								}
							}
							if (pieceBejLive2.mShrinking && this.mUpdateCnt % 3 == 0)
							{
								pieceBejLive2.mShrinkSize++;
							}
							if (pieceBejLive2.mSpinFrame != 0 || pieceBejLive2 == this.mSelectedPiece)
							{
								if (pieceBejLive2.mColor == 1)
								{
									if (this.mUpdateCnt % 3 == 0)
									{
										pieceBejLive2.mSpinFrame = (pieceBejLive2.mSpinFrame + 1) % 20;
									}
								}
								else if (pieceBejLive2.mColor == 2 || pieceBejLive2.mColor == 6)
								{
									if (this.mUpdateCnt % 4 == 0)
									{
										pieceBejLive2.mSpinFrame = (pieceBejLive2.mSpinFrame + 1) % 20;
									}
								}
								else if (this.mUpdateCnt % 4 == 0)
								{
									pieceBejLive2.mSpinFrame = (pieceBejLive2.mSpinFrame + 1) % 20;
								}
								this.MarkDirty();
							}
							pieceBejLive2.mSelectorAlpha.IncInVal();
							if (pieceBejLive2.mRotPct != 0f || pieceBejLive2.mSelected)
							{
								pieceBejLive2.mRotPct += Constants.mConstants.M(0.02f);
								if (pieceBejLive2.mRotPct >= 1f)
								{
									pieceBejLive2.mRotPct = 0f;
								}
							}
							if (pieceBejLive2.mDestPct.GetDouble != 0.0)
							{
								pieceBejLive2.mX = (float)((double)base.GetColX(pieceBejLive2.mCol) * (1.0 - pieceBejLive2.mDestPct.GetDouble) + (double)base.GetColX(pieceBejLive2.mDestCol) * pieceBejLive2.mDestPct.GetDouble);
								pieceBejLive2.mY = (float)((double)base.GetRowY(pieceBejLive2.mRow) * (1.0 - pieceBejLive2.mDestPct.GetDouble) + (double)base.GetRowY(pieceBejLive2.mDestRow) * pieceBejLive2.mDestPct.GetDouble);
							}
						}
					}
				}
			}
			ReportAchievement.ReportChainReaction(this.chainReactorSpecialsTriggered);
			if (flag)
			{
				this.mApp.PlaySample(Resources.SOUND_BOMB_EXPLODE);
			}
			this.UpdateLightning();
			if (this.mUpdateCnt % 4 == 0)
			{
				if (this.mDispPoints < this.mPoints)
				{
					this.mDispPoints += (int)((float)(this.mPoints - this.mDispPoints) * Constants.mConstants.M(0.2f) + 1f);
				}
				else if (this.mDispPoints > this.mPoints)
				{
					this.mDispPoints += (int)((float)(this.mPoints - this.mDispPoints) * Constants.mConstants.M(0.2f) - 1f);
				}
				if (this.mMoneyDisp < this.mMoneyDispGoal)
				{
					this.mMoneyDisp += (int)((float)(this.mMoneyDispGoal - this.mMoneyDisp) * Constants.mConstants.M(0.2f) + 1f);
					int num6 = this.mMoneyDisp;
					int num7 = this.mMoneyDispGoal;
				}
			}
			if (this.IsGameIdle())
			{
				this.mIdleTicks++;
			}
			if (this.mComboFlashPct.IsInitialized() && !this.mComboFlashPct.HasBeenTriggered())
			{
				this.mComboCountDisp = Math.Min((float)this.mComboLen, this.mComboCountDisp + Constants.mConstants.M(0.04f));
			}
			else if (this.mComboCountDisp < (float)this.mComboCount)
			{
				this.mComboCountDisp = Math.Min((float)this.mComboCount, this.mComboCountDisp + Constants.mConstants.M(0.04f));
			}
			else
			{
				this.mComboCountDisp = Math.Max((float)this.mComboCount, this.mComboCountDisp - Constants.mConstants.M(0.04f));
			}
			if (this.mComboFlashPct.IsInitialized() && !this.mComboFlashPct.IncInVal())
			{
				this.NewCombo();
			}
			this.UpdateSpeedBonus();
			this.UpdateBar();
			this.UpdateComplements();
			this.mPostFXManager.mAlpha = this.GetPieceAlpha();
			if (this.mComboFlashPct.GetDouble == 0.0)
			{
				int num8 = (int)(44.0 - (double)this.mComboLen * 5.5);
				float num9 = (float)(-(float)this.mComboCount * num8 + (this.mComboLen - 1) * num8 / 2);
				this.mComboSelectorAngle += (num9 - this.mComboSelectorAngle) / 20f;
			}
			if (this.mGameTicks == 200)
			{
				this.mGo.SetCurve(CurvedValDefinition.mGoCurve);
			}
			if (this.mNoMoveCount > 1000 && !this.mApp.CheatsEnabled() && this.mApp.mProfile.WantsHint(Profile.Hint.HINT_MOVE))
			{
				this.mDoHintPenalty = false;
				this.mbAutoHint = true;
				this.ShowHint();
			}
			else
			{
				this.mbAutoHint = false;
			}
			if ((float)this.GetMaxMovesStat(5) < Constants.mConstants.M(8f))
			{
				this.mReadyForDrop = true;
			}
			if (this.mState == Board.BoardState.STATE_GAME_OVER_ANIM || this.mState == Board.BoardState.STATE_GAME_OVER_FALL)
			{
				this.miGameOverTicks--;
				if (this.miGameOverTicks <= 0)
				{
					if (this.mState == Board.BoardState.STATE_GAME_OVER_ANIM)
					{
						for (int n = 0; n < 8; n++)
						{
							for (int num10 = 0; num10 < 8; num10++)
							{
								PieceBejLive pieceBejLive3 = this.mBoard[n, num10];
								if (pieceBejLive3 != null)
								{
									pieceBejLive3.mFallVelocity = (float)(Board.Rand() % 8 - 6) * 0.75f;
									pieceBejLive3.mXVelocity = (float)(Board.Rand() % 7 - 3) * 0.75f;
								}
							}
						}
						this.mState = Board.BoardState.STATE_GAME_OVER_FALL;
						this.miGameOverTicks = 280;
						this.mApp.PlaySample(Resources.SOUND_EXPLODE);
						this.mSelectedPiece = null;
						return;
					}
					this.mState = Board.BoardState.STATE_GAME_OVER_DISPLAY;
				}
			}
		}

		private void UpdateComplements()
		{
			int num = 0;
			for (int i = 0; i < this.mMoveDataVector.size<MoveData>(); i++)
			{
				num += this.CalcAwesomeness(this.mMoveDataVector[i].mMoveCreditId);
			}
			int num2 = -1;
			int j = BoardBejLive.gComplements.Length - 1;
			while (j >= 0)
			{
				if (num >= BoardBejLive.gComplementPoints[j])
				{
					num2 = j;
					if (j > this.mLastComplement)
					{
						this.DoComplement(j);
						break;
					}
					break;
				}
				else
				{
					j--;
				}
			}
			if (num == 0 || num2 > this.mLastComplement)
			{
				this.mLastComplement = num2;
			}
			else if (num2 < this.mLastComplement - 1)
			{
				this.mLastComplement = num2 + 1;
			}
			if (this.mComplementNum != -1 && (this.mApp.mCurVoiceId == BoardBejLive.gComplements[this.mComplementNum] || this.mComplementAlpha.mInVal != 0.0))
			{
				this.mComplementAlpha.IncInVal();
			}
		}

		private int CalcAwesomeness(int theMoveCreditId)
		{
			int num = (int)Math.Max(0.0, Math.Pow((double)Math.Max(0, this.GetMoveStat(theMoveCreditId, Stats.STAT_CASCADES) - 1), 1.5));
			int moveStat = this.GetMoveStat(theMoveCreditId, Stats.STAT_FLAMEGEMS_USED);
			num += Math.Max(0, moveStat * 2 - 1);
			moveStat = this.GetMoveStat(theMoveCreditId, Stats.STAT_LASERGEMS_USED);
			num += Math.Max(0, (int)((double)moveStat * 2.5) - 1);
			moveStat = this.GetMoveStat(theMoveCreditId, Stats.STAT_HYPERGEMS_USED);
			num += Math.Max(0, moveStat * 3 - 1);
			moveStat = this.GetMoveStat(theMoveCreditId, Stats.STAT_FLAMEGEMS_MADE);
			num += moveStat;
			moveStat = this.GetMoveStat(theMoveCreditId, Stats.STAT_LASERGEMS_MADE);
			num += moveStat;
			moveStat = this.GetMoveStat(theMoveCreditId, Stats.STAT_HYPERGEMS_MADE);
			num += moveStat * 2;
			moveStat = this.GetMoveStat(theMoveCreditId, Stats.STAT_BIGGESTMATCH);
			num += Math.Max(0, (moveStat - 5) * 8);
			return num + (int)Math.Pow((double)this.GetMoveStat(theMoveCreditId, Stats.STAT_GEMS_CLEARED) / 15.0, 1.5);
		}

		private void UpdateMoveData()
		{
			int num = 0;
			int num2 = 0;
			int i = 0;
			while (i < this.mMoveDataVector.Count)
			{
				MoveData moveData = this.mMoveDataVector[i];
				if (moveData.mPartOfReplay)
				{
					num2++;
				}
				if (this.mLightningStorms.Count != 0)
				{
					goto IL_F3;
				}
				bool flag = false;
				for (int j = 0; j < 8; j++)
				{
					for (int k = 0; k < 8; k++)
					{
						PieceBejLive pieceBejLive = this.mBoard[j, k];
						if (pieceBejLive != null && pieceBejLive.mMoveCreditId == moveData.mMoveCreditId)
						{
							if (this.IsPieceStill(pieceBejLive))
							{
								pieceBejLive.mLastMoveCreditId = pieceBejLive.mMoveCreditId;
								pieceBejLive.mMoveCreditId = -1;
								pieceBejLive.mLastActiveTick = 0;
							}
							else
							{
								flag = true;
							}
						}
					}
				}
				for (int l = 0; l < 8; l++)
				{
					if (this.mNextColumnCredit[l] == moveData.mMoveCreditId)
					{
						flag = true;
					}
				}
				if (flag)
				{
					goto IL_F3;
				}
				this.mMoveDataVector[i].PrepareForReuse();
				this.mMoveDataVector.RemoveAt(i);
				i--;
				IL_FF:
				i++;
				continue;
				IL_F3:
				if (moveData.mPartOfReplay)
				{
					num++;
					goto IL_FF;
				}
				goto IL_FF;
			}
		}

		private void ResetPerMoveAchievementCounters()
		{
			this.chainReactorSpecialsTriggered = 0;
			this.starGemsActivated = 0;
			this.powerBrokerAchievementCount = 0;
			for (int i = 0; i < BoardBejLive.oneMoveGameColourCounter.Length; i++)
			{
				BoardBejLive.oneMoveGameColourCounter[i] = 0;
			}
			for (int j = 0; j < BoardBejLive.oneMoveGameTypeCounter.Length; j++)
			{
				BoardBejLive.oneMoveGameTypeCounter[j] = 0;
			}
		}

		protected override void UpdateFalling()
		{
			if (this.IsGameSuspended() && this.mFrozen)
			{
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						PieceBejLive pieceBejLive = this.mBoard[i, j];
						if (pieceBejLive != null)
						{
							pieceBejLive.mFallVelocity = 0f;
						}
						for (int k = 0; k < 8; k++)
						{
							this.mBumpVelocities[k] = 0f;
						}
					}
				}
				return;
			}
			bool flag = false;
			int num = 0;
			int num2 = 0;
			if (this.mGemFallDelay > 0)
			{
				if (--this.mGemFallDelay == 0)
				{
					for (int l = 0; l < 8; l++)
					{
						for (int m = 0; m < 8; m++)
						{
							PieceBejLive pieceBejLive2 = this.mBoard[l, m];
							if (pieceBejLive2 != null && pieceBejLive2.mFallVelocity != 0f)
							{
								pieceBejLive2.mFallVelocity = 0.01f;
							}
						}
					}
				}
				return;
			}
			if (this.mLightningStorms.Count == 0)
			{
				for (int n = 0; n < 8; n++)
				{
					float num3 = 1200f;
					float mFallVelocity = 0f;
					for (int num4 = 7; num4 >= 0; num4--)
					{
						PieceBejLive pieceBejLive3 = this.mBoard[num4, n];
						if (pieceBejLive3 != null && ((!this.IsPieceSwapping(pieceBejLive3) && !this.IsPieceMatching(pieceBejLive3)) || pieceBejLive3.mFallVelocity < 0f))
						{
							pieceBejLive3.mY += pieceBejLive3.mFallVelocity;
							bool flag2 = pieceBejLive3.mY >= (float)base.GetRowY(num4);
							if (flag2)
							{
								pieceBejLive3.mY = (float)base.GetRowY(num4);
								if (pieceBejLive3.mFallVelocity >= Constants.mConstants.M(0.625f))
								{
									num++;
									num2 += (int)pieceBejLive3.mX + GameConstants.GEM_WIDTH / 2;
								}
								if (pieceBejLive3.mFallVelocity > 0f)
								{
									pieceBejLive3.mFallVelocity = 0f;
									this.miBufferCheckCount = 1;
								}
							}
							else if (pieceBejLive3.mY >= num3 - (float)GameConstants.GEM_HEIGHT)
							{
								pieceBejLive3.mY = num3 - (float)GameConstants.GEM_HEIGHT;
								pieceBejLive3.mFallVelocity = mFallVelocity;
							}
							else
							{
								flag = true;
								pieceBejLive3.mFallVelocity += this.GetGravityFactor() * Constants.mConstants.Gravity;
								this.mState = Board.BoardState.STATE_FALLING;
							}
							if (pieceBejLive3.mFallVelocity != 0f)
							{
								pieceBejLive3.mLastActiveTick = this.mUpdateCnt;
							}
							num3 = pieceBejLive3.mY;
							mFallVelocity = pieceBejLive3.mFallVelocity;
						}
					}
				}
			}
			if (this.mState == Board.BoardState.STATE_BOARD_STILL && !flag)
			{
				this.ResetPerMoveAchievementCounters();
				ReportAchievement.ReportDynamo(this.dynamicBoardCounter);
				this.dynamicBoardCounter = 0;
			}
			if (this.mState == Board.BoardState.STATE_BOARD_STILL && !this.mBlitzMode && !this.mNoMoreMoves && !flag && !this.FindMove(null, 0, true, true, true) && !this.FindSets() && this.mSwapDataVector.Count == 0)
			{
				this.mNoMoreMoves = true;
				this.mApp.mEffectOverlay.DoStrechyText(Strings.Board_No_Moves, 80);
				this.mApp.PlaySample(Resources.SOUND_NO_MORE_MOVES);
				this.GameOver(200);
			}
			if (num > 0 && (float)Math.Abs(this.mLastHitSoundTick - this.mUpdateCnt) >= Constants.mConstants.M(8f))
			{
				this.mLastHitSoundTick = this.mUpdateCnt;
				this.mApp.PlaySample(Resources.SOUND_GEM_HIT, base.GetPanPosition(num2 / num));
			}
			this.CheckForHints();
		}

		public void StabilizeState()
		{
		}

		protected override void UpdateSwapping()
		{
			if (this.mDidTimeUp)
			{
				for (int i = 0; i < this.mSwapDataVector.Count; i++)
				{
					this.mSwapDataVector[i].PrepareForReuse();
				}
				this.mSwapDataVector.Clear();
			}
			if (this.mSwapDataVector.Count == 0)
			{
				return;
			}
			this.mFirstMove = false;
			this.mState = Board.BoardState.STATE_SWAPPING;
			this.mNoMoveCount = 0;
			this.mpHintPiece = null;
			for (int j = 0; j < this.mSwapDataVector.Count; j++)
			{
				SwapData swapData = this.mSwapDataVector[j];
				bool flag = false;
				swapData.mGemScale.IncInVal();
				if (!swapData.mSwapPct.IncInVal())
				{
					flag = true;
				}
				int num = base.GetColX(swapData.mPiece1.mCol) + swapData.mSwapDir.mX * GameConstants.GEM_WIDTH / 2;
				int num2 = base.GetRowY(swapData.mPiece1.mRow) + swapData.mSwapDir.mY * GameConstants.GEM_WIDTH / 2;
				swapData.mPiece1.mX = (float)((double)num - swapData.mSwapPct.GetDouble * (double)swapData.mSwapDir.mX * (double)GameConstants.GEM_WIDTH / 2.0);
				swapData.mPiece1.mY = (float)((double)num2 - swapData.mSwapPct.GetDouble * (double)swapData.mSwapDir.mY * (double)GameConstants.GEM_WIDTH / 2.0);
				swapData.mPiece1.mbDrawSelector = true;
				swapData.mPiece1.mSparkleLife = 0;
				if (swapData.mPiece2 != null)
				{
					swapData.mPiece2.mX = (float)((double)num + swapData.mSwapPct.GetDouble * (double)swapData.mSwapDir.mX * (double)GameConstants.GEM_WIDTH / 2.0);
					swapData.mPiece2.mY = (float)((double)num2 + swapData.mSwapPct.GetDouble * (double)swapData.mSwapDir.mY * (double)GameConstants.GEM_WIDTH / 2.0);
					swapData.mPiece2.mbDrawSelector = true;
					swapData.mPiece2.mSparkleLife = 0;
				}
				if (flag)
				{
					if (swapData.mPiece1.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
					{
						if (swapData.mPiece2.mCanDestroy)
						{
							this.DoHypergem(swapData.mPiece1, swapData.mPiece2.mColor);
							this.MatchMade(swapData);
							this.SwapSucceeded(swapData);
						}
					}
					else if (swapData.mPiece2 != null && swapData.mPiece2.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
					{
						if (swapData.mPiece1.mCanDestroy)
						{
							this.chainReactorSpecialsTriggered++;
							this.MatchMade(swapData);
							this.DoHypergem(swapData.mPiece2, swapData.mPiece1.mColor);
							this.SwapSucceeded(swapData);
						}
					}
					else if (swapData.mForwardSwap)
					{
						bool flag2 = swapData.mForceSwap || this.ForceSwaps();
						int num3 = swapData.mPiece1.mRow + swapData.mSwapDir.mY;
						int num4 = swapData.mPiece1.mCol + swapData.mSwapDir.mX;
						int k = 0;
						while (k < 2)
						{
							if (swapData.mPiece2 != null)
							{
								stdC.swap<int>(ref swapData.mPiece1.mCol, ref swapData.mPiece2.mCol);
								stdC.swap<int>(ref swapData.mPiece1.mRow, ref swapData.mPiece2.mRow);
								this.mBoard[swapData.mPiece1.mRow, swapData.mPiece1.mCol] = swapData.mPiece1;
								this.mBoard[swapData.mPiece2.mRow, swapData.mPiece2.mCol] = swapData.mPiece2;
							}
							else
							{
								stdC.swap<int>(ref swapData.mPiece1.mCol, ref num4);
								stdC.swap<int>(ref swapData.mPiece1.mRow, ref num3);
								this.mBoard[swapData.mPiece1.mRow, swapData.mPiece1.mCol] = swapData.mPiece1;
								this.mBoard[num3, num4] = swapData.mPiece2;
							}
							swapData.mIgnore = k == 0;
							swapData.mPiece1.mSwapTick = this.mUpdateCnt;
							if (swapData.mPiece2 != null)
							{
								swapData.mPiece2.mSwapTick = this.mUpdateCnt;
							}
							this.mLastComboCount = this.mComboCount;
							bool flag3 = false;
							if (k == 1 || (flag3 = this.FindSets(true, swapData.mPiece1, swapData.mPiece2)) || flag2)
							{
								if (flag3)
								{
									this.MatchMade(swapData);
								}
								if (k == 0 && this.mLastComboCount == this.mComboCount && swapData.mPiece1.mColor == this.mLastPlayerSwapColor)
								{
									this.ComboFailed();
								}
								if (k != 0)
								{
									break;
								}
								this.SwapSucceeded(swapData);
								this.AddToStat(Stats.STAT_NUM_GOOD_MOVES, 1);
								this.AddToStat(Stats.STAT_NUM_MOVES, 1);
								if (this.mSpeedBonusCount > 0)
								{
									int speedPoints = this.GetSpeedPoints();
									this.AddToStat(Stats.STAT_SPEED_POINTS, speedPoints);
									break;
								}
								break;
							}
							else
							{
								this.SwapFailed(swapData);
								this.mApp.PlaySample(Resources.SOUND_BAD);
								this.AddToStat(Stats.STAT_NUM_MOVES, 1);
								swapData.mForwardSwap = false;
								swapData.mSwapPct.SetCurve(CurvedValDefinition.mSwapPctCurve);
								swapData.mGemScale.SetCurve(CurvedValDefinition.mGemScaleCurve);
								swapData.mIgnore = false;
								flag = false;
								k++;
							}
						}
					}
					else if (!swapData.mForwardSwap)
					{
						swapData.mIgnore = true;
						if (this.FindSets(true, swapData.mPiece1, swapData.mPiece2))
						{
							this.SwapSucceeded(swapData);
							this.AddToStat(Stats.STAT_NUM_GOOD_MOVES, 1);
							if (this.mSpeedBonusCount > 0)
							{
								int speedPoints2 = this.GetSpeedPoints();
								this.AddToStat(Stats.STAT_SPEED_POINTS, speedPoints2);
							}
						}
					}
					if (flag)
					{
						for (j = 0; j < this.mSwapDataVector.Count; j++)
						{
							SwapData swapData2 = this.mSwapDataVector[j];
							if (swapData2 == swapData)
							{
								swapData.mPiece1.mbDrawSelector = false;
								if (swapData.mPiece2 != null)
								{
									swapData.mPiece2.mbDrawSelector = false;
								}
								this.mSelectedPiece = null;
								this.mSwapDataVector[j].PrepareForReuse();
								this.mSwapDataVector.RemoveAt(j);
								j--;
								break;
							}
						}
					}
				}
			}
		}

		protected override void UpdateLightning()
		{
			this.aCandidatePieces.Clear();
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null && pieceBejLive != null && pieceBejLive.mIsElectrocuting)
					{
						num++;
					}
				}
			}
			int num2 = 0;
			this.mBoardDarken = Math.Max(this.mBoardDarken - 0.02f, 0f);
			if (this.mLightningStorms.Count > 0)
			{
				this.mBoardDarken = Math.Min(this.mBoardDarken + 0.05f, 1f);
			}
			for (int k = 0; k < this.mLightningStorms.Count; k++)
			{
				LightningStorm lightningStorm = this.mLightningStorms[k];
				lightningStorm.Update();
				bool flag = false;
				switch (lightningStorm.mStormType)
				{
				case LightningStorm.StormType.STORM_HORZ:
				case LightningStorm.StormType.STORM_VERT:
				case LightningStorm.StormType.STORM_BOTH:
				case LightningStorm.StormType.STORM_SHORT:
				case LightningStorm.StormType.STORM_STAR:
				case LightningStorm.StormType.STORM_SCREEN:
				case LightningStorm.StormType.STORM_FLAMING:
				{
					int num3 = ((lightningStorm.mStormType == LightningStorm.StormType.STORM_FLAMING) ? 1 : 0);
					bool flag2 = true;
					for (int l = 1; l < lightningStorm.mPieceIds.Count; l++)
					{
						PieceBejLive pieceById = this.GetPieceById(lightningStorm.mPieceIds[l]);
						if (pieceById != null && pieceById.mCanDestroy)
						{
							pieceById.mElectrocutePercent += Constants.mConstants.M(0.015f);
							if (pieceById.mElectrocutePercent >= 1f)
							{
								PieceBejLive pieceById2 = this.GetPieceById(lightningStorm.mElectrocuterId);
								if (!pieceById.mDestructing)
								{
									this.SetMoveCredit(pieceById, lightningStorm.mMoveCreditId);
									pieceById.mMatchId = lightningStorm.mMatchId;
									if (pieceById.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
									{
										pieceById.mExplodeDelay = 5;
									}
									else if (!this.TriggerSpecial(pieceById, pieceById2, lightningStorm.mColor) && !pieceById.mDestructing)
									{
										pieceById.mExplodeDelay = 1;
									}
								}
								lightningStorm.mPieceIds.RemoveAt(l);
								l--;
							}
							else
							{
								flag2 = false;
							}
						}
					}
					lightningStorm.mTimer -= 0.01f;
					if (lightningStorm.mTimer <= 0f)
					{
						lightningStorm.mTimer = Constants.mConstants.M(0.1f);
						int num4 = 0;
						int num5 = 4;
						if (lightningStorm.mStormType == LightningStorm.StormType.STORM_HORZ)
						{
							num5 = 2;
						}
						if (lightningStorm.mStormType == LightningStorm.StormType.STORM_VERT)
						{
							num4 = 2;
						}
						if (lightningStorm.mStormType == LightningStorm.StormType.STORM_STAR)
						{
							num5 = 8;
						}
						if (lightningStorm.mStormType == LightningStorm.StormType.STORM_SCREEN)
						{
							int num6 = Math.Min(lightningStorm.mDist, 7);
							int num7 = 0;
							for (int m = -num6; m <= num6; m++)
							{
								this.offsets[num7, 0] = m;
								this.offsets[num7++, 1] = -num6;
								this.offsets[num7, 0] = m;
								this.offsets[num7++, 1] = num6;
							}
							for (int n = -num6 + 1; n <= num6 - 1; n++)
							{
								this.offsets[num7, 0] = -num6;
								this.offsets[num7++, 1] = n;
								this.offsets[num7, 0] = num6;
								this.offsets[num7++, 1] = n;
							}
							num5 = num7;
						}
						for (int num8 = num4; num8 < num5; num8++)
						{
							for (int num9 = -num3; num9 <= num3; num9++)
							{
								int num10 = ((lightningStorm.mStormType == LightningStorm.StormType.STORM_SCREEN) ? 1 : lightningStorm.mDist);
								int num11 = lightningStorm.mCX + (num10 * this.offsets[num8, 0] + this.offsets[num8, 1] * num9) * GameConstants.GEM_WIDTH;
								int num12 = lightningStorm.mCY + (num10 * this.offsets[num8, 1] + this.offsets[num8, 0] * num9) * GameConstants.GEM_HEIGHT;
								PieceBejLive pieceAt = this.GetPieceAt(num11, num12);
								if (num10 <= lightningStorm.mStormLength && num11 >= base.GetColX(0) && num11 < base.GetColX(8) && num12 >= base.GetRowY(0) && num12 < base.GetRowY(8) && pieceAt != null && !pieceAt.mDestructing)
								{
									bool flag3 = false;
									for (int num13 = 0; num13 < this.mLightningStorms.Count; num13++)
									{
										for (int num14 = 0; num14 < this.mLightningStorms[num13].mPieceIds.Count; num14++)
										{
											if (pieceAt.mId == this.mLightningStorms[num13].mPieceIds[num14])
											{
												flag3 = true;
											}
										}
									}
									if (!flag3)
									{
										flag2 = false;
										if (pieceAt.mElectrocutePercent == 0f)
										{
											lightningStorm.mPieceIds.Add(pieceAt.mId);
											pieceAt.mElectrocutePercent = 0.01f;
											for (int num15 = 0; num15 < 20; num15++)
											{
												EffectBej3 effectBej = this.mPostFXManager.AllocEffect(EffectBej3.EffectType.TYPE_SPARKLE_SHARD);
												effectBej.mX = (float)num11 + Board.GetRandFloat() * (float)Math.Abs(this.offsets[num8, 0]) * (float)GameConstants.GEM_WIDTH / 3f;
												effectBej.mY = (float)num12 + Board.GetRandFloat() * (float)Math.Abs(this.offsets[num8, 1]) * (float)GameConstants.GEM_HEIGHT / 3f;
												effectBej.mDX = (float)((int)((double)Board.GetRandFloat() * ((double)Math.Abs(this.offsets[num8, 1]) + 0.5) * (double)Constants.mConstants.M(10f)));
												effectBej.mDY = (float)((int)((double)Board.GetRandFloat() * ((double)Math.Abs(this.offsets[num8, 0]) + 0.5) * (double)Constants.mConstants.M(10f)));
												this.mPostFXManager.AddEffect(effectBej);
											}
										}
										pieceAt.mShakeScale = Math.Min(1f, Math.Max(pieceAt.mShakeScale, pieceAt.mElectrocutePercent));
									}
								}
							}
						}
						if (lightningStorm.mDist == 0)
						{
							this.mApp.PlaySample(Resources.SOUND_ELECTRO_EXPLODE);
						}
						lightningStorm.mDist++;
						int mStormLength = lightningStorm.mStormLength;
						if (lightningStorm.mDist < mStormLength)
						{
							flag2 = false;
						}
						if (flag2)
						{
							this.mComboBonusSlowdownPct = 1f;
							PieceBejLive pieceById3 = this.GetPieceById(lightningStorm.mPieceIds[0]);
							if (pieceById3 != null)
							{
								pieceById3.ClearFlag(PIECEFLAG.PIECEFLAG_LASER);
								if (pieceById3.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
								{
									this.TriggerSpecial(pieceById3, pieceById3, lightningStorm.mColor);
								}
								else
								{
									pieceById3.mExplodeDelay = 1;
								}
							}
							lightningStorm.mHoldDelay -= Constants.mConstants.M(0.25f);
							for (int num16 = 0; num16 < 8; num16++)
							{
								for (int num17 = 0; num17 < 8; num17++)
								{
									PieceBejLive pieceBejLive2 = this.mBoard[num16, num17];
									if (pieceBejLive2 != null)
									{
										pieceBejLive2.mFallVelocity = 0f;
									}
								}
							}
							if (lightningStorm.mHoldDelay <= 0f)
							{
								flag = true;
							}
						}
					}
					break;
				}
				}
				if (lightningStorm.mDoneDelay > 0 && --lightningStorm.mDoneDelay == 0)
				{
					flag = true;
				}
				if (flag)
				{
					this.mLightningStorms[k].PrepareForReuse();
					this.mLightningStorms.RemoveAt(k);
					if (this.mLightningStorms.Count == 0)
					{
						this.mApp.LockOrientation(false);
					}
					if (this.mLightningStorms.Count == 0)
					{
						this.FillInBlanks();
					}
					k--;
				}
				else
				{
					if (lightningStorm.mUpdateCnt < num2)
					{
						return;
					}
					num2 += (int)Constants.mConstants.M(25f);
				}
			}
		}

		private bool UpdateBulging()
		{
			bool result = false;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null && !pieceBejLive.mScale.IncInVal())
					{
						if (pieceBejLive.mScale.GetFloat > 1f || (pieceBejLive.IsShrinking() && pieceBejLive.mScale.GetFloat == 1f))
						{
							pieceBejLive.mScale.SetCurve(CurvedValDefinition.pieceShrinkScaleCurve);
						}
						else if (pieceBejLive.mScale.GetFloat == 1f)
						{
							pieceBejLive.mScale.SetConstant(1.0);
						}
						else if (pieceBejLive.mScale.GetFloat == 0f)
						{
							this.DeletePiece(pieceBejLive);
						}
					}
				}
			}
			return result;
		}

		protected void KillTutorialButtons()
		{
		}

		public void CreateFontGlowImages()
		{
		}

		private void CreateEmittersForSpecialPiece(PieceBejLive piece)
		{
			if (piece.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
			{
				this.lowerLayerEmitters.Add(PieceParticleEmitter.CreateHyperCubeEmitter(piece));
				return;
			}
			if (piece.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
			{
				this.upperLayerEmitters.Add(PieceParticleEmitter.CreateFlameGemEmitter(piece));
			}
		}

		private void EndLevelUpEffect()
		{
			BoardBejLive.mHyperspace.Stop();
			this.mWidgetManager.RemoveWidget(BoardBejLive.mHyperspace);
			this.mLevel++;
			this.SetDisplayLevel();
			this.CreateFontGlowImages();
			this.SetupLevelData();
			this.mLastQuakePoints = this.mPointsWithoutSpeedBonus;
			this.mNextQuakePoints = this.mPointsWithoutSpeedBonus + this.mPointsPerQuake;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.aPointVector.Clear();
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null)
					{
						if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
						{
							num++;
						}
						else if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
						{
							num2++;
						}
						else if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
						{
							num3++;
						}
					}
					this.aPointVector.Add(new TPoint(j, i));
				}
			}
			this.NewBoard();
			for (int k = 0; k < num; k++)
			{
				int num4 = Board.Rand() % this.aPointVector.Count;
				PieceBejLive pieceBejLive2 = this.mBoard[this.aPointVector[num4].mY, this.aPointVector[num4].mX];
				this.aPointVector.RemoveAt(num4);
				this.RemoveGemsFromBlitzBatch(pieceBejLive2);
				this.RemoveGemsFromMultiFontBatch(pieceBejLive2);
				pieceBejLive2.ClearFlags();
				pieceBejLive2.SetFlag(PIECEFLAG.PIECEFLAG_FLAME);
				pieceBejLive2.mShrinking = false;
				pieceBejLive2.mShrinkSize = 0;
				this.mBlitzGemsRenderer.Add(pieceBejLive2);
				this.CreateEmittersForSpecialPiece(pieceBejLive2);
			}
			for (int l = 0; l < num2; l++)
			{
				int num5 = Board.Rand() % this.aPointVector.Count;
				PieceBejLive pieceBejLive3 = this.mBoard[this.aPointVector[num5].mY, this.aPointVector[num5].mX];
				this.aPointVector.RemoveAt(num5);
				this.RemoveGemsFromBlitzBatch(pieceBejLive3);
				this.RemoveGemsFromMultiFontBatch(pieceBejLive3);
				pieceBejLive3.ClearFlags();
				pieceBejLive3.SetFlag(PIECEFLAG.PIECEFLAG_HYPERGEM);
				pieceBejLive3.mChangedTick = this.mUpdateCnt;
				pieceBejLive3.mLastColor = pieceBejLive3.mColor;
				pieceBejLive3.mColor = -1;
				pieceBejLive3.mShrinking = false;
				pieceBejLive3.ClearFlag(PIECEFLAG.PIECEFLAG_FLAME);
				this.mBlitzGemsRenderer.Add(pieceBejLive3);
				this.CreateEmittersForSpecialPiece(pieceBejLive3);
			}
			for (int m = 0; m < num3; m++)
			{
				int num6 = Board.Rand() % this.aPointVector.Count;
				PieceBejLive pieceBejLive4 = this.mBoard[this.aPointVector[num6].mY, this.aPointVector[num6].mX];
				this.aPointVector.RemoveAt(num6);
				this.RemoveGemsFromBlitzBatch(pieceBejLive4);
				this.RemoveGemsFromMultiFontBatch(pieceBejLive4);
				pieceBejLive4.ClearFlags();
				pieceBejLive4.SetFlag(PIECEFLAG.PIECEFLAG_LASER);
				pieceBejLive4.mShrinking = false;
				this.mBlitzGemsRenderer.Add(pieceBejLive4);
				this.CreateEmittersForSpecialPiece(pieceBejLive4);
				this.miLightningGemCount++;
				if (this.miLightningGemCount >= 4)
				{
					this.miBufferCheckCount = 10;
				}
			}
			if (this.mTimedMode)
			{
				this.mTimerDelay = 200;
			}
			this.mWhirlpoolFade = 0.0;
			this.mWhirlpoolRot = 0.0;
			this.mWhirlpoolRotAcc = 0.0;
			this.mInterfaceRestorePercent = 0f;
			this.mState = Board.BoardState.STATE_INTERFACE_RESTORE;
			this.EndInterfaceRestore();
			this.mFallDelay = 150;
			this.mShowLevelDelay = 80;
			this.Show();
			this.newLevelStarted = true;
			this.PlayMusic();
		}

		private void PlayMusic()
		{
			switch (this.mApp.mGameMode)
			{
			case GameMode.MODE_CLASSIC:
				this.mApp.PlaySong(SongType.Classic, true);
				return;
			case GameMode.MODE_TIMED:
				this.mApp.PlaySong(SongType.Action, true);
				return;
			case GameMode.MODE_ENDLESS:
				this.mApp.PlaySong(SongType.Endless, true);
				return;
			}
			this.mApp.PlaySong(SongType.Classic, true);
		}

		protected void EndInterfaceRestore()
		{
			this.mFallDelay = 25;
			this.mState = Board.BoardState.STATE_FALLING;
			if (this.mPuzzleStateOverlay != null)
			{
				this.mPuzzleStateOverlay.SetVisible(true);
			}
			this.mMenuHintButton.SetVisible(true);
		}

		protected void NewBoard()
		{
			this.mGoDelay = 300;
			this.blazingSpeedCountThisLevel = 0;
			this.mState = Board.BoardState.STATE_FALLING;
			this.mNoMoveCount = 0;
			this.mBlitzGemsRenderer.Clear();
			for (int i = 7; i >= 0; i--)
			{
				for (int j = 0; j < 8; j++)
				{
					if (this.mBoard[i, j] != null)
					{
						this.mBoard[i, j].PrepareForReuse();
						this.mBoard[i, j] = null;
					}
				}
			}
			for (int i = 7; i >= 0; i--)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive newCPieceBejLive = PieceBejLive.GetNewCPieceBejLive(this);
					newCPieceBejLive.mCol = j;
					newCPieceBejLive.mRow = i;
					newCPieceBejLive.mX = (float)base.GetColX(j);
					int num = (this.mGravityReversed ? 800 : 0);
					if (i < 7)
					{
						num = (int)this.mBoard[i + 1, j].mY;
					}
					if (this.mGravityReversed)
					{
						newCPieceBejLive.mY = (float)(num + GameConstants.GEM_HEIGHT + 16 + Board.Rand() % 240);
					}
					else
					{
						newCPieceBejLive.mY = (float)(num - GameConstants.GEM_HEIGHT - 16 - Board.Rand() % 240);
					}
					if (this.mBoard[i, j] != null)
					{
						this.mBoard[i, j].PrepareForReuse();
					}
					this.mBoard[i, j] = newCPieceBejLive;
					do
					{
						newCPieceBejLive.mColor = this.mRand.Next() % 7;
					}
					while (this.HasSet());
				}
			}
		}

		protected void DoWhirlpool()
		{
			BoardBejLive.mHyperspace.Stop();
			this.KillTutorialButtons();
			bool flag = this.mApp.Is3DAccelerated();
			this.mApp.mProfile.DisableHint(Profile.Hint.HINT_BAD_MOVE);
			this.mMenuHintButton.SetVisible(false);
			this.mSelectedPiece = null;
			if (this.mWhirlpoolSound != null)
			{
				this.mWhirlpoolSound.Release();
			}
			if (flag)
			{
				Image image = (this.mApp.IsLandscape() ? GameConstants.BGH_TEXTURE : GameConstants.BGV_TEXTURE);
				int width = image.GetWidth();
				int height = image.GetHeight();
				int mWidth = this.mApp.mWidth;
				int mHeight = this.mApp.mHeight;
				for (int i = 0; i < 16; i++)
				{
					for (int j = 0; j < 16; j++)
					{
						WarpPoint warpPoint = BoardBejLive.mWarpPoints[i, j];
						warpPoint.mX = (float)(j * mWidth / 15);
						warpPoint.mY = (float)(i * mHeight / 15);
						warpPoint.mZ = 0f;
						warpPoint.mVelX = 0f;
						warpPoint.mVelY = 0f;
						warpPoint.mU = warpPoint.mX / (float)width;
						warpPoint.mV = warpPoint.mY / (float)height;
						float num = warpPoint.mX - (float)(mWidth / 2);
						float num2 = warpPoint.mY - (float)(mHeight / 2);
						warpPoint.mRot = (float)(Math.Atan2((double)num2, (double)num) + 6.28318);
						warpPoint.mDist = (float)Math.Sqrt((double)(num * num + num2 * num2));
					}
				}
				this.mWarpSpeed = 0f;
				this.mWarpDelay = 90;
				this.mUISuckDelay = 80;
				this.mHyperspaceDelay = 100;
			}
			else
			{
				this.DoHyperspace();
			}
			this.mUIWarpPercent = 0.0;
			this.mUIWarpPercentAdd.SetInVal(0.0);
			this.mWarpSizeMult = 1f;
			this.mWhirlpoolFade = 0.0;
			this.mWhirlpoolFrame = 0f;
			this.mWhirlpoolRot = 0.0;
			this.mWhirlpoolRotAcc = 0.0;
			this.mSelectedPiece = null;
			int num3;
			int num4;
			base.GetBoardCenter(out num3, out num4);
			this.mState = Board.BoardState.STATE_WHIRLPOOL;
			this.mFirstWhirlDraw = true;
		}

		protected void UpdateLevelTransition()
		{
			this.mWhirlpoolFade -= 0.015;
			if (this.mWhirlpoolFade < 0.0)
			{
				this.mWhirlpoolFade = 0.0;
				if (this.mWhirlpoolSound != null)
				{
					this.mWhirlpoolSound.Release();
					this.mWhirlpoolSound = null;
				}
			}
			else if (this.mWhirlpoolSound != null)
			{
				this.mWhirlpoolSound.SetVolume(this.mWhirlpoolFade);
			}
			this.MarkDirty();
			this.mTransitionPos += 3;
			if (this.mTransitionPos > this.mHeight)
			{
				this.SetDisplayLevel();
				this.CreateFontGlowImages();
				this.mTimerDelay = 100;
				this.NewBoard();
			}
		}

		protected void UpdateLevelUpEffect()
		{
			bool flag = false;
			if (this.mWhirlpoolFade > 0.0)
			{
				this.mWhirlpoolFrame -= 0.1f;
				if (this.mWhirlpoolFrame < 0f)
				{
					this.mWhirlpoolFrame += 5f;
				}
				this.mWhirlpoolRotAcc += 0.00050000002374872565;
				this.mWhirlpoolRot -= this.mWhirlpoolRotAcc;
				this.MarkDirty();
			}
			if (BoardBejLive.mHyperspace.IsRunning)
			{
				if (BoardBejLive.mHyperspace.mIsDone)
				{
					flag = true;
				}
				else if (BoardBejLive.mHyperspace.mShowBkg)
				{
					if (BoardBejLive.mHyperspace.mFlashState == 2)
					{
						BoardBejLive.mHyperspace.mFlashState = 3;
						base.ProcessLevelImages();
						this.mBackdropNum = (this.mBackdropNum + 1) % GameApp.NumberOfBackdrops;
						this.mNextBackdropNum = (this.mBackdropNum + 1) % GameApp.NumberOfBackdrops;
						base.StartPrepareLevelImages();
					}
					return;
				}
			}
			if (this.mHyperspaceDelay > 0 && --this.mHyperspaceDelay == 0)
			{
				this.DoHyperspace();
			}
			this.mWhirlpoolFade += 0.02;
			if (this.mWhirlpoolFade > 1.0)
			{
				this.mWhirlpoolFade = 1.0;
			}
			this.mWarpSizeMult += 0.001f;
			if (this.mWarpDelay > 0)
			{
				this.mWarpDelay--;
			}
			else
			{
				this.mWarpSpeed += 0.01f;
			}
			if (this.mUISuckDelay > 0)
			{
				this.mUISuckDelay--;
			}
			else
			{
				this.mUIWarpPercentAdd.IncInVal(0.05);
				this.mUIWarpPercent += this.mUIWarpPercentAdd.GetOutVal() * 0.01;
				if (this.mUIWarpPercent > 1.0)
				{
					this.mUIWarpPercent = 1.0;
				}
			}
			int mWidth = this.mApp.mWidth;
			int mHeight = this.mApp.mHeight;
			for (int i = 1; i < 15; i++)
			{
				for (int j = 1; j < 15; j++)
				{
					WarpPoint warpPoint = BoardBejLive.mWarpPoints[i, j];
					int num = j;
					int num2 = 15 - j;
					int num3 = i;
					int num4 = 15 - i;
					int num5 = Math.Min(Math.Min(num, num2), Math.Min(num3, num4));
					if (this.mWarpDelay == 0)
					{
						warpPoint.mDist -= (float)(0.35 * (double)num5 * (double)this.mWarpSpeed);
						if (warpPoint.mDist < 0f)
						{
							warpPoint.mDist = 0f;
						}
						warpPoint.mRot += (float)(0.001 * (double)num5 * (double)this.mWarpSpeed);
					}
					if (warpPoint.mRot < 0f)
					{
						warpPoint.mRot += 6.28318f;
					}
					else if (warpPoint.mRot > 6.28318f)
					{
						warpPoint.mRot -= 6.28318f;
					}
					int num6 = (int)((double)(warpPoint.mRot * 4096f) / 6.28318) % 4096;
					warpPoint.mX = Board.COS_TAB[num6] * warpPoint.mDist * this.mWarpSizeMult + (float)(mWidth / 2);
					warpPoint.mY = Board.SIN_TAB[num6] * warpPoint.mDist * this.mWarpSizeMult + (float)(mHeight / 2);
				}
			}
			if (flag)
			{
				this.EndLevelUpEffect();
			}
			this.MarkDirty();
		}

		protected void DoHyperspace()
		{
			BoardBejLive.mHyperspace.Start();
			BoardBejLive.mHyperspace.Resize(0, 0, this.mWidth, this.mHeight);
			BoardBejLive.mHyperspace.mZOrder = this.mZOrder;
			this.mWidgetManager.AddWidget(BoardBejLive.mHyperspace);
		}

		private void UpdateBar()
		{
			if (this.mPauseCount > 0)
			{
				return;
			}
			if (this.mTimedMode)
			{
				float num = this.mLevelBarPct;
				float num2 = ((this.mPoints == 0) ? 1f : ((float)this.mDispPoints / (float)this.mPoints));
				this.mLevelBarPct = (num2 * (float)this.mPointsWithoutSpeedBonus - (float)this.mLastQuakePoints - (float)((int)this.mBonusPenalty)) * 0.5f / (float)(this.mNextQuakePoints - this.mLastQuakePoints) + 0.5f;
				if (num < this.mLevelBarPct)
				{
					int num3 = (int)(this.mLevelBarPct * 1600f);
					if (num3 < 80)
					{
						num3 = 80;
					}
					if (num3 <= 240 && (int)((long)this.mUpdateCnt - (long)((ulong)this.mLastWarningSound)) >= num3)
					{
						this.mApp.PlaySample(Resources.SOUND_WARNING);
						this.mLastWarningSound = (uint)this.mUpdateCnt;
					}
				}
				if ((double)this.mLevelBarPct < 0.15)
				{
					this.mFlashBarRed = this.mUpdateCnt / 8 % 2 == 0;
				}
				else
				{
					this.mFlashBarRed = false;
				}
			}
			else if (this.mBlitzMode)
			{
				float levelPct = this.GetLevelPct();
				if (this.mLevelBarPct < levelPct)
				{
					this.mLevelBarPct = Math.Min(levelPct, this.mLevelBarPct + (levelPct - this.mLevelBarPct) * Constants.mConstants.M(0.025f) + Constants.mConstants.M(0.0005f));
				}
				else
				{
					this.mLevelBarPct = Math.Max(levelPct, this.mLevelBarPct + (levelPct - this.mLevelBarPct) * Constants.mConstants.M(0.05f) - Constants.mConstants.M(0.0001f));
				}
			}
			else
			{
				this.mLevelBarPct = ((float)(this.mDispPoints - this.mLastQuakePoints) - (float)this.mBonusPenalty) / (float)(this.mNextQuakePoints - this.mLastQuakePoints);
			}
			if (this.mLevelBarPct < 0f)
			{
				this.mLevelBarPct = 0f;
			}
			if ((this.mLevelBarPct >= 1f || this.didReachLevelEnd) && !this.mBlitzMode)
			{
				this.mLevelBarPct = 1f;
				this.didReachLevelEnd = true;
			}
			this.CheckLevelBar();
		}

		private void CheckLevelBar()
		{
			int levelPoints = this.GetLevelPoints();
			if (this.GetTimeLimit() == 0)
			{
				if (this.mState != Board.BoardState.STATE_WHIRLPOOL && this.mState == Board.BoardState.STATE_BOARD_STILL && this.mLevelBarPct >= 1f && this.mLevelPointsTotal >= levelPoints && this.mSpeedBonusFlameModePct == 0f)
				{
					this.LevelUp();
					return;
				}
			}
			else if (levelPoints > 0 && this.mLevelPointsTotal >= levelPoints)
			{
				this.LevelUp();
			}
		}

		private float GetLevelPct()
		{
			int levelPoints = this.GetLevelPoints();
			int timeLimit = this.GetTimeLimit();
			float num = 0f;
			if (timeLimit != 0)
			{
				if (this.mBlitzMode)
				{
					num = Math.Max(0f, (float)this.GetTicksLeft() / ((float)timeLimit * 100f));
					if ((num <= 0f && this.IsBoardStill() && this.mGameOverCount == 0) || this.mState == Board.BoardState.STATE_GAME_OVER_DISPLAY)
					{
						this.mTimeExpired = true;
						this.GameOver(200);
					}
				}
			}
			else
			{
				num = Math.Min(1f, (float)this.mLevelPointsTotal / (float)levelPoints);
			}
			return num;
		}

		private void LevelUp()
		{
			if (this.mWantLevelup || this.mGameOverCount > 0)
			{
				return;
			}
			ReportAchievement.GivePendingAchievements();
			if (this.mApp.mCurVoiceId != -1 || this.mApp.mNextVoiceId != -1)
			{
				return;
			}
			this.mWantLevelup = false;
			this.didReachLevelEnd = false;
			this.blazingSpeedCountThisLevel = 0;
			this.delayBeforeSpeedBonusDecrease = 1000;
			if (this.mTimedMode)
			{
				ReportAchievement.ReportActionHero(this.highFlyerScore);
				ReportAchievement.Report7thHeaven(this.mLevel + 1);
			}
			if (this.mClassicMode)
			{
				ReportAchievement.ReportClassicRock(this.mLevel + 1);
			}
			this.highFlyerScore = 0;
			this.DoWhirlpool();
			this.mApp.PlaySample(Resources.SOUND_LEVEL_COMPLETE);
		}

		private void BackToMenu()
		{
		}

		private void UpdateSpeedBonus()
		{
			uint num = (uint)(this.mIdleTicks - this.mLastMatchTick);
			float num2 = num / Constants.mConstants.SPEED_TIME1;
			float num3 = (num + Math.Min((float)this.mLastMatchTime, Constants.mConstants.SPEED_TIME1)) / Constants.mConstants.SPEED_TIME2;
			Math.Max(num2, num3);
			if (this.delayBeforeSpeedBonusDecrease > 0)
			{
				this.delayBeforeSpeedBonusDecrease--;
				num = 0U;
			}
			if (this.mMatchTallyCount == Constants.mConstants.SPEED_START_THRESHOLD)
			{
				Constants.mConstants.M(0.75f);
			}
			float num4 = (float)((double)Constants.mConstants.SPEED_TIME_LEFT + this.mSpeedBonusNum * (double)(Constants.mConstants.SPEED_TIME_RIGHT - Constants.mConstants.SPEED_TIME_LEFT));
			if (num >= num4 && this.mSpeedBonusNum > 0.0 && this.mSpeedBonusFlameModePct == 0f && this.mState != Board.BoardState.STATE_WHIRLPOOL)
			{
				this.mSpeedBonusNum *= (double)Constants.mConstants.M(0.993f);
			}
			this.mSpeedModeFactor.SetConstant(1.0 + this.mSpeedBonusNum * (double)Constants.mConstants.M(0.65f));
			this.mSpeedNeedle += (float)(((0.5 - this.mSpeedBonusNum) * (double)Constants.mConstants.MS(132f) - (double)this.mSpeedNeedle) * (double)Constants.mConstants.M(0.1f));
			float num5 = Constants.mConstants.M(125f) + (float)Math.Min(10, this.mSpeedBonusCount + 1) * Constants.mConstants.M(17.1875f);
			if (num >= num5 && this.mSpeedBonusCount > 0 && this.mState != Board.BoardState.STATE_WHIRLPOOL)
			{
				this.mSpeedBonusLastCount = this.mSpeedBonusCount;
				this.mSpeedBonusCount = 0;
				this.mSpeedBonusNum = 0.0;
				this.mLastMatchTick = -1000;
				this.mLastMatchTime = 1000;
				this.mSpeedBonusDisp.SetCurve(CurvedValDefinition.mSpeedBonusDispCurve2);
				this.mSpeedBonusPointsScale.SetCurve(CurvedValDefinition.mSpeedBonusPointsScaleCurve);
			}
		}

		private int CalcSpeedBonus(int aCount)
		{
			return (int)(Math.Pow((double)(aCount - Constants.mConstants.SPEED_START_THRESHOLD + 1), 1.15) * (double)this.SPEED_SCORE_MULT);
		}

		private bool AllowSpeedBonus()
		{
			return true;
		}

		private bool AllowLaserGems()
		{
			return (BoardBejLive.mBlitzFlags & 16) != 0;
		}

		private bool AllowPowerups()
		{
			return true;
		}

		private float GetModePointMultiplier()
		{
			if (this.mBlitzMode)
			{
				return 5f;
			}
			if (this.mClassicMode)
			{
				return 1f;
			}
			return 1f;
		}

		private void DrawBar(Graphics g)
		{
			g.SetFastStretch(true);
			if (this.mPauseCount != 0)
			{
				return;
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetColorizeImages(true);
			if (this.mApp.IsLandscape())
			{
				g.DrawImage(AtlasResources.IMAGE_FRAME_BOTTOM, Constants.mConstants.Board_Board_X + AtlasResources.IMAGE_GRID.mHeight - Constants.mConstants.BoardBej3_Frame_Bottom_Offset_Landscape, Constants.mConstants.Board_Board_Y);
				g.SetColor(StaticParticleEmitter.ProgressBarBackColour);
				g.DrawImage(AtlasResources.IMAGE_FRAME_TOP_BACK, Constants.mConstants.Board_Board_X + Constants.mConstants.BoardBej3_Frame_Top_Offset_Landscape, Constants.mConstants.Board_Board_Y + this.mHeight / 2 - AtlasResources.IMAGE_FRAME_TOP.mHeight / 2);
				TRect clipRect = new TRect(Constants.mConstants.Board_Board_X + Constants.mConstants.BoardBej3_Frame_Top_Offset_Landscape + Constants.mConstants.BoardBej3_ProgressBar_Clip_X_Landscape, Constants.mConstants.Board_Board_Y + Constants.mConstants.BoardBej3_ProgressBar_Clip_Y_Landscape + (int)((1f - this.mLevelBarPct) * (float)(AtlasResources.IMAGE_FRAME_TOP_BACK.mHeight - Constants.mConstants.BoardBej3_ProgressBar_Clip_Y_Landscape - Constants.mConstants.BoardBej3_ProgressBar_Clip_Y2_Landscape)), AtlasResources.IMAGE_FRAME_TOP_BACK.mWidth - Constants.mConstants.BoardBej3_ProgressBar_Clip_X2_Landscape, (int)(this.mLevelBarPct * (float)(AtlasResources.IMAGE_FRAME_TOP_BACK.mHeight - Constants.mConstants.BoardBej3_ProgressBar_Clip_Y_Landscape - Constants.mConstants.BoardBej3_ProgressBar_Clip_Y2_Landscape)));
				g.SetClipRect(clipRect);
				g.DrawImage(AtlasResources.IMAGE_PROGRESSBAR_PARTICLE_BACK, 0, 0, this.mWidth, this.mHeight);
				foreach (ParticleEmitter particleEmitter in this.progressBarEmitters)
				{
					particleEmitter.Draw(g, this.mPauseFade);
				}
				g.ClearClipRect();
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				g.SetColor(SexyColor.White);
				g.DrawImage(AtlasResources.IMAGE_FRAME_TOP, Constants.mConstants.Board_Board_X + Constants.mConstants.BoardBej3_Frame_Top_Offset_Landscape, Constants.mConstants.Board_Board_Y + this.mHeight / 2 - AtlasResources.IMAGE_FRAME_TOP.mHeight / 2);
			}
			else
			{
				g.DrawImageRotated(AtlasResources.IMAGE_FRAME_BOTTOM, Constants.mConstants.Board_Board_X + this.mWidth / 2, Constants.mConstants.Board_Board_Y - Constants.mConstants.BoardBej3_Frame_Bottom_Offset_Portrait, -1.5707963705062866);
				g.SetColor(StaticParticleEmitter.ProgressBarBackColour);
				g.DrawImageRotated(AtlasResources.IMAGE_FRAME_TOP_BACK, Constants.mConstants.Board_Board_X + this.mWidth / 2, Constants.mConstants.Board_Board_Y + AtlasResources.IMAGE_GRID.mHeight + Constants.mConstants.BoardBej3_Frame_Top_Offset_Portrait, -1.5707963705062866);
				TRect clipRect2 = new TRect(Constants.mConstants.Board_Board_X + Constants.mConstants.BoardBej3_ProgressBar_Clip_X_Portrait, Constants.mConstants.Board_Board_Y + AtlasResources.IMAGE_GRID.mHeight + Constants.mConstants.BoardBej3_ProgressBar_Clip_Y_Portrait, (int)(this.mLevelBarPct * (float)(AtlasResources.IMAGE_FRAME_TOP_BACK.mHeight - Constants.mConstants.BoardBej3_ProgressBar_Clip_X_Portrait - Constants.mConstants.BoardBej3_ProgressBar_Clip_X2_Portrait)), AtlasResources.IMAGE_FRAME_TOP_BACK.mWidth - Constants.mConstants.BoardBej3_ProgressBar_Clip_Y2_Portrait);
				g.SetClipRect(clipRect2);
				g.DrawImage(AtlasResources.IMAGE_PROGRESSBAR_PARTICLE_BACK, 0, 0, this.mWidth, this.mHeight);
				foreach (ParticleEmitter particleEmitter2 in this.progressBarEmitters)
				{
					particleEmitter2.Draw(g, this.mPauseFade);
				}
				g.ClearClipRect();
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				g.SetColor(SexyColor.White);
				g.DrawImageRotated(AtlasResources.IMAGE_FRAME_TOP, Constants.mConstants.Board_Board_X + this.mWidth / 2, Constants.mConstants.Board_Board_Y + AtlasResources.IMAGE_GRID.mHeight + Constants.mConstants.BoardBej3_Frame_Top_Offset_Portrait, -1.5707963705062866);
			}
			g.SetFont(Resources.FONT_TIMER);
			g.SetColorizeImages(true);
			int theX = this.mWidth / 2;
			int boardBej3_Time_Position_Y = Constants.mConstants.BoardBej3_Time_Position_Y;
			if (this.mDidTimeUp)
			{
				if (this.mBlitzMode)
				{
					theX = Constants.mConstants.BoardBej3_LastHurrah_Position_X - g.GetFont().StringWidth(Strings.Board_Last_Hurrah) / 2;
					g.SetColor(new SexyColor((int)(255f * (float)(this.mUpdateCnt % 90) / 90f), (int)(255f * (float)(this.mUpdateCnt % 120) / 120f), (int)(255f * (float)(this.mUpdateCnt % 180) / 180f), 255));
					g.DrawString(Strings.Board_Last_Hurrah, theX, boardBej3_Time_Position_Y + Constants.mConstants.BoardBej3_LastHurrah_Position_Offset_X);
				}
			}
			else if (this.mSpeedBonusFlameModePct > 0f)
			{
				g.SetColor(new SexyColor(255, 0, 0, (int)(255f * (float)(this.mGameTicks % 60) / 60f)));
				if (this.mApp.IsLandscape())
				{
					g.DrawString(Strings.Board_Blazing, Constants.mConstants.BoardBej3_MenuHintButton_X_Landscape - g.GetFont().StringWidth(Strings.Board_Blazing) / 2, Constants.mConstants.BoardBej3_Blazing_Position_Y_Landscape);
					g.DrawString(Strings.Board_speed, Constants.mConstants.BoardBej3_MenuHintButton_X_Landscape - g.GetFont().StringWidth(Strings.Board_speed) / 2, Constants.mConstants.BoardBej3_Blazing2_Position_Y_Landscape);
				}
				else
				{
					g.DrawString(Strings.Board_Blazing_Speed, this.mWidth / 2 - g.GetFont().StringWidth(Strings.Board_Blazing_Speed) / 2, boardBej3_Time_Position_Y);
				}
			}
			else if (this.mBlitzMode)
			{
				int num = this.GetTicksLeft() / 100;
				if (num > 60)
				{
					num = 60;
				}
				string timeString = Board.GetTimeString(num);
				theX = Constants.mConstants.BoardBej3_Time_Position_X - g.GetFont().StringWidth(timeString) / 2;
				g.DrawString(timeString, theX, boardBej3_Time_Position_Y);
			}
			g.SetColorizeImages(false);
		}

		private void DrawFrame(Graphics g)
		{
			if (this.mApp.IsLandscape())
			{
				g.DrawImage(AtlasResources.IMAGE_FRAME_CHIP_VERT, Constants.mConstants.Board_Board_X - AtlasResources.IMAGE_FRAME_CHIP_VERT.mWidth + Constants.mConstants.BoardBej2_FrameChipOffset, Constants.mConstants.Board_Board_Y, AtlasResources.IMAGE_FRAME_CHIP_VERT.mWidth, AtlasResources.IMAGE_GRID.mHeight);
				g.DrawImage(AtlasResources.IMAGE_FRAME_CHIP_VERT, Constants.mConstants.Board_Board_X + AtlasResources.IMAGE_GRID.mWidth - Constants.mConstants.BoardBej2_FrameChipOffset, Constants.mConstants.Board_Board_Y, AtlasResources.IMAGE_FRAME_CHIP_VERT.mWidth, AtlasResources.IMAGE_GRID.mHeight);
				return;
			}
			g.DrawImage(AtlasResources.IMAGE_FRAME_CHIP_HORIZ, Constants.mConstants.Board_Board_X, Constants.mConstants.Board_Board_Y - AtlasResources.IMAGE_FRAME_CHIP_HORIZ.mHeight + Constants.mConstants.BoardBej2_FrameChipOffset, AtlasResources.IMAGE_GRID.mWidth, AtlasResources.IMAGE_FRAME_CHIP_HORIZ.mHeight);
			g.DrawImage(AtlasResources.IMAGE_FRAME_CHIP_HORIZ, Constants.mConstants.Board_Board_X, Constants.mConstants.Board_Board_Y + AtlasResources.IMAGE_GRID.mHeight - Constants.mConstants.BoardBej2_FrameChipOffset, AtlasResources.IMAGE_GRID.mWidth, AtlasResources.IMAGE_FRAME_CHIP_HORIZ.mHeight);
		}

		private void DrawPieces(Graphics g)
		{
			this.DrawPieces(g, false);
		}

		private void DrawPieces(Graphics g, bool thePostFX)
		{
			float num = this.mGemShowPct * this.mPauseFade;
			for (int i = 0; i < BoardBejLive.gemMask.Length; i++)
			{
				BoardBejLive.gemMask[i] = true;
			}
			this.skippedGems.Clear();
			this.mMultFontRenderer.Clear();
			for (int j = 0; j < this.mLightningStorms.Count; j++)
			{
				if (this.mLightningStorms[j].mStormType == LightningStorm.StormType.STORM_HYPERGEM && this.mLightningStorms[j].mColor >= 0)
				{
					BoardBejLive.gemMask[this.mLightningStorms[j].mColor] = false;
				}
			}
			foreach (ParticleEmitter particleEmitter in this.lowerLayerEmitters)
			{
				particleEmitter.Draw(g, num);
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			for (int k = 0; k < 8; k++)
			{
				for (int l = 0; l < 8; l++)
				{
					PieceBejLive pieceBejLive = this.mBoard[k, l];
					if (pieceBejLive != null && !this.IsPieceSwapping(pieceBejLive))
					{
						if (pieceBejLive.mColor != -1 && BoardBejLive.gemMask[pieceBejLive.mColor])
						{
							this.DrawPiece(g, pieceBejLive);
						}
						else
						{
							this.skippedGems.Add(pieceBejLive);
						}
					}
				}
			}
			foreach (ParticleEmitter particleEmitter2 in this.upperLayerEmitters)
			{
				particleEmitter2.Draw(g, num);
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			if (this.mBoardDarken > 0f)
			{
				this.mShowingSun = false;
				g.SetColorizeImages(true);
				g.SetColor(new Color(0, 0, 0, (int)(this.mPauseFade * this.mBoardDarken * 128f)));
				g.FillRect(this.BoardRect);
				g.SetColor(Color.White);
			}
			foreach (PieceBejLive thePiece in this.skippedGems)
			{
				this.DrawPiece(g, thePiece);
			}
			for (int m = 0; m < this.mSwapDataVector.Count; m++)
			{
				SwapData swapData = this.mSwapDataVector[m];
				float num2 = (float)swapData.mGemScale.GetDouble;
				if (swapData.mPiece2 != null && swapData.mSwapPct.GetDouble <= 3.1415927410125732)
				{
					this.DrawPiece(g, swapData.mPiece2, 1f - num2);
				}
				this.DrawPiece(g, swapData.mPiece1, 1f + num2);
				if (swapData.mPiece2 != null && swapData.mSwapPct.GetDouble > 3.1415927410125732)
				{
					this.DrawPiece(g, swapData.mPiece2, 1f - num2);
				}
			}
			foreach (PieceBejLive pieceBejLive2 in this.mBlitzGemsRenderer)
			{
				if (pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
				{
					pieceBejLive2.DrawHyperGemOverlay(g, this.mUpdateCnt, (double)num);
				}
				else if (pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
				{
					pieceBejLive2.DrawLaserGemOverlay(g, this.mUpdateCnt, (double)num);
				}
			}
			foreach (PieceBejLive pieceBejLive3 in this.mMultFontRenderer)
			{
				pieceBejLive3.DrawMultiplierGem(g, this.mUpdateCnt, this.mPointMultiplier, (double)num);
			}
			if (this.mPointMultiplier != 1 && this.mPrevMultiGemColor != -1)
			{
				int board_Multiplier_X = Constants.mConstants.Board_Multiplier_X;
				int num3 = (this.mApp.IsLandscape() ? ((int)Constants.mConstants.MS(155f)) : ((int)Constants.mConstants.MS(0f)));
				int theId = ((this.mPointMultiplier == 1) ? 10064 : (10064 + this.mPointMultiplier - 2));
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 255, 255, (int)(255f * num)));
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				g.DrawImage(AtlasResources.GetImageInAtlasById(10056 + this.mPrevMultiGemColor), board_Multiplier_X, num3, new TRect(0, 0, Constants.mConstants.BoardBej3_MultiplierGem_Size, Constants.mConstants.BoardBej3_MultiplierGem_Size));
				g.DrawImage(AtlasResources.GetImageInAtlasById(theId), board_Multiplier_X + Constants.mConstants.BoardBej3_MultiplierGem_Text_Offset_UI, num3 + Constants.mConstants.BoardBej3_MultiplierGem_Text_Offset_UI, new TRect(0, 0, Constants.mConstants.BoardBej3_MultiplierGem_Text_Size, Constants.mConstants.BoardBej3_MultiplierGem_Text_Size));
				g.SetColorizeImages(false);
			}
		}

		private void DrawPiece(Graphics g, PieceBejLive thePiece)
		{
			this.DrawPiece(g, thePiece, 1f);
		}

		private void DrawPiece(Graphics g, PieceBejLive thePiece, float theScale)
		{
			float num = this.mGemShowPct * this.mPauseFade;
			g.SetScale(theScale * thePiece.mScale.GetFloat);
			if (this.mShowMoveCredit && thePiece.mMoveCreditId != -1)
			{
				g.SetColor(SexyColor.White);
				g.SetFont(Resources.FONT_TEXT);
			}
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255.0 * thePiece.mAlpha.GetDouble * (double)this.GetPieceAlpha())));
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
			{
				thePiece.DrawHyperGem(g, this.mUpdateCnt, (double)num);
			}
			else if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
			{
				thePiece.DrawFlameGem(g, this.mUpdateCnt, (double)num);
			}
			else if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
			{
				thePiece.DrawLaserGemLowerLayer(g, this.mUpdateCnt, (double)num);
			}
			else if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER) && !thePiece.IsShrinking())
			{
				this.mMultFontRenderer.Add(thePiece);
			}
			else if ((!thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER) && !thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER) && !thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM)) || (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER) && thePiece.IsShrinking()))
			{
				Image imageInAtlasById = AtlasResources.GetImageInAtlasById(10031 + thePiece.mColor);
				float num2 = thePiece.mRotPct * (float)imageInAtlasById.GetCelCount();
				imageInAtlasById.GetCelRect((int)num2);
				imageInAtlasById.GetCelRect(((int)num2 + 1) % imageInAtlasById.GetCelCount());
				if (thePiece == this.mSelectedPiece)
				{
					int theX = this.mSelectedPiece.mSpinFrame * GameConstants.GEM_WIDTH;
					g.SetColor(new SexyColor(255, 255, 255, (int)(255f * num)));
					g.DrawImageF(AtlasResources.GetImageInAtlasById(10031 + this.mSelectedPiece.mColor), (float)((int)thePiece.mX + thePiece.mOfsX), (float)((int)thePiece.mY + thePiece.mOfsY), new TRect(theX, 0, GameConstants.GEM_WIDTH, GameConstants.GEM_HEIGHT));
					g.SetColorizeImages(false);
				}
				else
				{
					g.SetColorizeImages(true);
					g.SetColor(new SexyColor(255, 255, 255, (int)(255f * num)));
					g.DrawImageF(imageInAtlasById, (float)((int)thePiece.mX + thePiece.mOfsX) + thePiece.mFlyVX, (float)((int)thePiece.mY + thePiece.mOfsY) + thePiece.mFlyVY, new TRect(0, 0, GameConstants.GEM_WIDTH, GameConstants.GEM_HEIGHT));
					g.SetColorizeImages(false);
					if (thePiece.mSpinFrame == 0)
					{
						thePiece.mLighting[0] = 0f;
						thePiece.mLighting[1] = 0f;
						thePiece.mLighting[2] = 0f;
						thePiece.mLighting[3] = 0f;
						thePiece.mLighting[4] = 0f;
						thePiece.mLighting[5] = 0f;
						thePiece.mLighting[6] = 0f;
						thePiece.mLighting[7] = 0f;
						thePiece.mLighting[8] = 0f;
						if (this.mShowingSun)
						{
							float mSunPosition = this.mSunPosition;
							float num3 = (float)(thePiece.mCol + thePiece.mRow) / 16f;
							float num4 = Math.Abs(mSunPosition - num3);
							float num5 = 1f - num4 * 9f;
							if (num5 > 0f)
							{
								thePiece.mLighting[3] += num5 * 0.8f;
								thePiece.mLighting[7] += num5 * 0.6f;
								thePiece.mLighting[8] += num5 * 0.6f;
							}
						}
						int num6 = (int)thePiece.mShineAnimFrame;
						int num7 = (num6 + 1) % 10;
						float num8 = thePiece.mShineAnimFrame - (float)num6;
						thePiece.mLighting[this.aFacet[num6]] += (1f - num8) * (float)thePiece.mShineFactor;
						thePiece.mLighting[this.aFacet[num7]] += num8 * (float)thePiece.mShineFactor;
						for (int i = 0; i < 2; i++)
						{
							int num9 = this.aFacet[(i == 0) ? num6 : num7];
							float num10 = ((i == 0) ? (1f - num8) : num8) * (float)thePiece.mShineFactor;
							if (num9 != 9)
							{
								int num11 = thePiece.mCol + this.aLightOffset[num9, 0];
								int num12 = thePiece.mRow + this.aLightOffset[num9, 1];
								if (num11 >= 0 && num11 < 8 && num12 >= 0 && num12 < 8)
								{
									float num13 = 0.3f;
									if (this.aLightOffset[num9, 0] != 0 && thePiece.mRow + this.aLightOffset[num9, 1] != 0)
									{
										num13 *= 0.707f;
									}
									PieceBejLive pieceBejLive = this.mBoard[num12, num11];
									if (pieceBejLive != null)
									{
										int num14 = (num9 + 4) % 8;
										pieceBejLive.mLighting[num14] += num10 * 0.3f;
									}
								}
							}
						}
						this.mGlintingPieces.Add(thePiece);
					}
				}
				g.SetColorizeImages(false);
			}
			if (!this.TrialExpired() && this.mState != Board.BoardState.STATE_GAME_OVER_FALL && this.mState != Board.BoardState.STATE_GAME_OVER_ANIM)
			{
				if (thePiece.mSparkleLife > 0)
				{
					this.mpHintPiece = thePiece;
				}
				else if (this.mpHintPiece == thePiece)
				{
					this.mpHintPiece = null;
				}
				if (thePiece.mHintAlpha.GetDouble != 0.0)
				{
					g.SetColor(new SexyColor(255, 255, 255, (int)(thePiece.mHintAlpha.GetDouble * 255.0)));
					g.DrawImageF(AtlasResources.IMAGE_HELP_INDICATOR_ARROW, thePiece.CX(), (float)((double)thePiece.CY() + thePiece.mHintArrowPos.GetDouble));
				}
				if (thePiece.mbDrawSelector)
				{
					g.SetColor(new SexyColor(SexyColor.White));
					g.DrawImageF(AtlasResources.IMAGE_SELECT, (float)((int)thePiece.mX), (float)((int)thePiece.mY), new TRect((int)(Constants.mConstants.S((float)(this.mSelectAnimCnt % 1)) * 84f), 0, Constants.mConstants.BoardBej2_selectPiece_dimensions, Constants.mConstants.BoardBej2_selectPiece_dimensions));
				}
			}
			g.SetScale(1f);
		}

		public static void DrawShrinkingPiece(Graphics g, PieceBejLive thePiece, float aShowPct, Image anImage)
		{
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255f * aShowPct)));
			float num = 1f - (float)thePiece.mShrinkSize / 6f;
			g.DrawImage(anImage, new TRect((int)(thePiece.mX + (float)thePiece.mOfsX + thePiece.mFlyVX + (float)(GameConstants.GEM_WIDTH / 2) - num * (float)GameConstants.GEM_WIDTH / 2f), (int)(thePiece.mY + (float)thePiece.mOfsY + thePiece.mFlyVY + (float)(GameConstants.GEM_HEIGHT / 2) - num * (float)GameConstants.GEM_HEIGHT / 2f), (int)((float)GameConstants.GEM_WIDTH * num), (int)((float)GameConstants.GEM_HEIGHT * num)), new TRect(0, 0, GameConstants.GEM_WIDTH, GameConstants.GEM_HEIGHT));
			g.SetColorizeImages(false);
		}

		private void DrawScore(Graphics g)
		{
			this.DrawScore(g, false, false);
		}

		private StringBuilder scoreString
		{
			get
			{
				if (BoardBejLive.scoreStringCachedScore != this.mDispPoints)
				{
					GlobalStaticVars.CommaSeperate_(this.mDispPoints, BoardBejLive.scoreStringCache);
					BoardBejLive.scoreStringCachedScore = this.mDispPoints;
				}
				return BoardBejLive.scoreStringCache;
			}
		}

		public static void DrawScoreHelper(Graphics g, Font font, string score, int x, int y, float opacity)
		{
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255f * opacity)));
			g.SetFont(font);
			g.DrawImage(AtlasResources.IMAGE_WIDGET_TOP, x, y);
			g.DrawString(score, x + AtlasResources.IMAGE_WIDGET_TOP.mWidth / 2 - font.StringWidth(score) / 2, y + Constants.mConstants.BoardBej3_ScoreText_Offset_Y);
		}

		public static void DrawScoreHelper(Graphics g, Font font, StringBuilder score, int x, int y, float opacity)
		{
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255f * opacity)));
			g.SetFont(font);
			g.DrawImage(AtlasResources.IMAGE_WIDGET_TOP, x, y);
			g.DrawString(score, x + AtlasResources.IMAGE_WIDGET_TOP.mWidth / 2 - font.StringWidth(score) / 2, y + Constants.mConstants.BoardBej3_ScoreText_Offset_Y);
		}

		public static void DrawSpeedBonusHelper(Graphics g, int x, int y, int speedPoints, double burningSpeedBuildUp, float opacity, bool drawBurningSpeedClip)
		{
			g.SetColorizeImages(true);
			g.SetColor(new SexyColor(255, 255, 255, (int)(255f * opacity)));
			g.SetScale(Constants.SpeedBonusTextScale);
			if (burningSpeedBuildUp < BoardBejLive.speedBonusDisplay)
			{
				BoardBejLive.speedBonusDisplay = burningSpeedBuildUp;
			}
			else
			{
				BoardBejLive.speedBonusDisplay += (burningSpeedBuildUp - BoardBejLive.speedBonusDisplay) / 10.0;
			}
			Font font_SPEED_BONUS = Resources.FONT_SPEED_BONUS;
			g.SetFont(font_SPEED_BONUS);
			string text;
			if (!BoardBejLive.speedpointStrings.TryGetValue(speedPoints, ref text))
			{
				text = string.Format(Strings.Speed_Bonus, speedPoints);
				BoardBejLive.speedpointStrings.Add(speedPoints, text);
			}
			int theX = x - font_SPEED_BONUS.StringWidth(text) / 2;
			g.DrawString(text, theX, y);
			if (drawBurningSpeedClip && BoardBejLive.speedBonusDisplay > 0.0 && burningSpeedBuildUp >= 0.5)
			{
				TRect theClip = new TRect(theX, 0, (int)((BoardBejLive.speedBonusDisplay - 0.5) * 2.0 * (double)font_SPEED_BONUS.StringWidth(text)), y + 800);
				g.HardwareClipRect(theClip);
				g.SetColor(new Color(240, 140, 50, (int)(255f * opacity)));
				g.DrawString(text, theX, y);
				g.ClearHardwareClipRect();
			}
			g.SetScale(1f);
		}

		private void DrawScore(Graphics g, bool noBack, bool noFront)
		{
			if (!noFront)
			{
				Font font_BIG_TEXT = Resources.FONT_BIG_TEXT;
				g.SetFont(font_BIG_TEXT);
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
				g.SetColor(SexyColor.White);
				if (this.mApp.IsLandscape())
				{
					BoardBejLive.DrawScoreHelper(g, font_BIG_TEXT, this.scoreString, Constants.mConstants.BoardBej3_MenuHintButton_X_Landscape - AtlasResources.IMAGE_WIDGET_TOP.mWidth / 2, 24 + Constants.mConstants.BoardBej2_ScoreText_Y_Landscape, 1f);
				}
				else
				{
					BoardBejLive.DrawScoreHelper(g, font_BIG_TEXT, this.scoreString, Constants.mConstants.BoardBej2_ScoreText_X_Portrait, 24 + Constants.mConstants.BoardBej2_ScoreText_Y_Portrait, 1f);
				}
				if (!this.mBlitzMode)
				{
					g.SetFont(Resources.FONT_TEXT);
					if (this.mApp.IsLandscape())
					{
						g.DrawString(this.mDispLevel, Constants.mConstants.BoardBej3_MenuHintButton_X_Landscape - g.GetFont().StringWidth(this.mDispLevel) / 2, 24 + Constants.mConstants.BoardBej2_ScoreText_Y_Landscape + Constants.mConstants.BoardBej2_LevelText_Y_Landscape);
					}
					else
					{
						g.DrawString(this.mDispLevel, Constants.mConstants.BoardBej2_ScoreText_X_Portrait + AtlasResources.IMAGE_WIDGET_TOP.mWidth / 2 - g.GetFont().StringWidth(this.mDispLevel) / 2, 24 + Constants.mConstants.BoardBej2_ScoreText_Y_Portrait + Constants.mConstants.BoardBej2_LevelText_Y_Portrait);
					}
				}
			}
			if (this.AllowSpeedBonus() && this.mSpeedBonusCount > 0)
			{
				int num = (this.mApp.IsLandscape() ? Constants.mConstants.BoardBej2_ScoreText_X_Landscape : Constants.mConstants.BoardBej2_ScoreText_X_Portrait);
				num += AtlasResources.IMAGE_WIDGET_TOP.mWidth / 2;
				int y;
				if (this.mApp.IsLandscape())
				{
					y = 24 + Constants.mConstants.BoardBej2_ScoreText_Y_Landscape - Constants.mConstants.BoardBej3_SpeedBonus_Y;
				}
				else
				{
					y = 24 + Constants.mConstants.BoardBej2_ScoreText_Y_Portrait - Constants.mConstants.BoardBej3_SpeedBonus_Y;
				}
				int speedPoints = this.GetSpeedPoints();
				BoardBejLive.DrawSpeedBonusHelper(g, num, y, speedPoints, this.mSpeedBonusNum, 1f, this.mState != Board.BoardState.STATE_WHIRLPOOL);
				this.SetStat(Stats.STAT_HIGHEST_SPEED, Math.Max(this.mSpeedBonusCount, this.mGameStats[21]));
			}
			if ((this.mEndlessMode || SexyAppBase.IsInTrialMode) && this.mState != Board.BoardState.STATE_WHIRLPOOL)
			{
				if (SexyAppBase.IsInTrialMode)
				{
					this.mMenuHintButton.TrialTimeLeft = this.trialGameTime;
				}
				g.SetColorizeImages(false);
			}
		}

		private int GetSpeedPoints()
		{
			return Math.Min(200, this.mSpeedBonusCount * 20) * this.mPointMultiplier;
		}

		private void DrawLightning(Graphics g)
		{
			if (this.mUserPaused)
			{
				return;
			}
			if (this.mPauseCount > 0)
			{
				return;
			}
			for (int i = 0; i < this.mLightningStorms.Count; i++)
			{
				LightningStorm lightningStorm = this.mLightningStorms[i];
				lightningStorm.Draw(g);
			}
		}

		private bool DecrementCounterGem(PieceBejLive thePiece, bool immediate)
		{
			if (thePiece.mCounter > 0)
			{
				if (immediate)
				{
					thePiece.mCounter--;
					if (thePiece.mCounter == 0 && thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_COUNTER) && this.mGameOverCount == 0)
					{
						this.GameOver(200);
					}
				}
				else if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_COUNTER))
				{
					thePiece.mSpinFrame = 1;
					thePiece.mSpinSpeed = Constants.mConstants.M(0.33f);
				}
				else if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_SKULL))
				{
					thePiece.mCounter--;
				}
			}
			return false;
		}

		private int GetLevelPoints()
		{
			return 0;
		}

		private int GetTimeLimit()
		{
			if (!this.HasBoost(Boost.BOOST_5_SECONDS))
			{
				return this.mMinutes * 60;
			}
			return this.mMinutes * 60 + 5;
		}

		private bool HasBoost(Boost theBoostNum)
		{
			return (this.mBoostsUsed & (1 << (int)theBoostNum)) == 0 && (this.mBoostsEnabled & (1 << (int)theBoostNum)) != 0;
		}

		private int GetTicksLeft()
		{
			int timeLimit = this.GetTimeLimit();
			if (timeLimit == 0)
			{
				return -1;
			}
			int num = (int)Constants.mConstants.M(250f);
			return Math.Max(0, timeLimit * 100 - Math.Max(0, this.mGameTicks - num));
		}

		private void SetTicksLeft(int ticksLeft)
		{
			int timeLimit = this.GetTimeLimit();
			int num = (int)Constants.mConstants.M(250f);
			this.mGameTicks = Math.Max(0, timeLimit * 100 - ticksLeft + num);
		}

		private float GetPieceAlpha()
		{
			return 1f - this.mBoardHidePct;
		}

		private PieceBejLive GetPieceAtRowCol(int theRow, int theCol)
		{
			if (theRow < 0 || theCol < 0 || theRow >= 8 || theCol >= 8)
			{
				return null;
			}
			return this.mBoard[theRow, theCol];
		}

		private float GetMatchSpeed()
		{
			return (float)this.mSpeedModeFactor.GetDouble;
		}

		private float GetGameSpeed()
		{
			return (float)this.mGameSpeed.GetDouble;
		}

		private bool WantsWholeGameReplay()
		{
			return true;
		}

		private bool IsGameSuspended()
		{
			return this.mLevelCompleteCount != 0 || this.mLightningStorms.Count != 0 || this.mFrozen || this.mEditing;
		}

		public void RemoveFromPieceMap(int ID)
		{
			if (BoardBejLive.mPieceMap.ContainsKey(ID))
			{
				BoardBejLive.mPieceMap.Remove(ID);
			}
		}

		public void AddToPieceMap(int ID, PieceBejLive pPiece)
		{
			BoardBejLive.mPieceMap.Add(ID, pPiece);
		}

		private void FillInBlanks()
		{
			this.FillInBlanks(true);
		}

		private void FillInBlanks(bool allowCascades)
		{
			if (this.mGameOverCount != 0)
			{
				return;
			}
			for (int i = 7; i >= 0; i--)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null && pieceBejLive.mExplodeDelay > 0)
					{
						return;
					}
				}
			}
			this.aNewPieceVector.Clear();
			bool flag;
			do
			{
				flag = false;
				for (int k = 0; k < 8; k++)
				{
					bool flag2 = false;
					int num = base.GetRowY(0) - 24;
					int mMoveCreditId = this.mNextColumnCredit[k];
					double num2 = (double)this.mBumpVelocities[k];
					for (int l = 7; l >= 0; l--)
					{
						PieceBejLive pieceBejLive2 = this.mBoard[l, k];
						if (pieceBejLive2 != null)
						{
							if (!pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_DRAGONSTONE))
							{
								pieceBejLive2.mCanMatch = true;
							}
							if (pieceBejLive2.mY < (float)num)
							{
								num = (int)pieceBejLive2.mY;
							}
							if (flag2 && (this.IsPieceSwapping(pieceBejLive2) || this.IsPieceMatching(pieceBejLive2)))
							{
								this.mBoard[l + 1, k] = null;
								flag2 = false;
							}
							if (flag2)
							{
								flag = true;
								if ((double)pieceBejLive2.mFallVelocity == 0.0)
								{
									pieceBejLive2.mFallVelocity += this.mBumpVelocities[k] + 1f;
									pieceBejLive2.mLastActiveTick = this.mUpdateCnt;
								}
								num2 = (double)pieceBejLive2.mFallVelocity;
								pieceBejLive2.mRow++;
								this.mBoard[l, k] = null;
								this.mBoard[l + 1, k] = pieceBejLive2;
								mMoveCreditId = pieceBejLive2.mMoveCreditId;
							}
						}
						else
						{
							if (flag2)
							{
								this.mBoard[l + 1, k] = null;
							}
							flag2 = true;
						}
					}
					if (flag2)
					{
						flag = true;
						PieceBejLive newCPieceBejLive = PieceBejLive.GetNewCPieceBejLive(this);
						newCPieceBejLive.mFallVelocity = (float)num2 - 0.171875f;
						newCPieceBejLive.mCreatedTick = this.mUpdateCnt;
						newCPieceBejLive.mLastActiveTick = this.mUpdateCnt;
						newCPieceBejLive.mCol = k;
						newCPieceBejLive.mRow = 0;
						newCPieceBejLive.mMoveCreditId = mMoveCreditId;
						newCPieceBejLive.mX = (float)base.GetColX(k);
						newCPieceBejLive.mY = (float)(num - GameConstants.GEM_HEIGHT) - ((float)this.mRand.Next() % Constants.mConstants.M(5f) - Constants.mConstants.M(3.125f));
						if (newCPieceBejLive.mY > (float)(-(float)GameConstants.GEM_HEIGHT))
						{
							newCPieceBejLive.mY = (float)(-(float)GameConstants.GEM_HEIGHT);
						}
						this.aNewPieceVector.Add(newCPieceBejLive);
						this.mBoard[0, k] = newCPieceBejLive;
					}
					this.mNextColumnCredit[k] = -1;
				}
			}
			while (flag);
			if (this.aNewPieceVector.Count > 0)
			{
				int num3 = 0;
				bool flag3 = this.AllowNoMoreMoves();
				bool flag4 = this.WantSpecialPiece(ref this.aNewPieceVector);
				this.aGemList.Clear();
				for (int m = 0; m < 7; m++)
				{
					this.aGemList.Add(m);
					this.aGemList.Add(m);
				}
				if (this.mFavorComboGems)
				{
					for (int n = 0; n < this.mComboLen; n++)
					{
						this.aGemList.Add(this.mComboColors[n]);
					}
				}
				for (int num4 = 0; num4 < this.mFavorGemColors.Count; num4++)
				{
					this.aGemList.Add(this.mFavorGemColors[num4]);
				}
				for (;;)
				{
					for (int num5 = 0; num5 < this.aNewPieceVector.Count; num5++)
					{
						this.aNewPieceVector[num5].ClearFlags();
						this.aNewPieceVector[num5].mColor = -1;
						this.aNewPieceVector[num5].mCanDestroy = true;
						this.aNewPieceVector[num5].mCanMatch = true;
						this.aNewPieceVector[num5].mCounter = 0;
					}
					if (flag4)
					{
						this.DropSpecialPiece(ref this.aNewPieceVector);
					}
					if (this.WantHyperMixers() && this.NumPiecesWithFlag(PIECEFLAG.PIECEFLAG_HYPERGEM) == 0 && this.FindMove(this.findMoveCoords, 0, true, true, true) && this.findMoveCoords[1] < 3 && this.findMoveCoords[3] < 3)
					{
						PieceBejLive pieceBejLive3;
						do
						{
							pieceBejLive3 = this.aNewPieceVector[Board.Rand(this.aNewPieceVector.Count)];
						}
						while (pieceBejLive3.mFlags != 0);
						pieceBejLive3.mColor = -1;
						pieceBejLive3.SetFlag(PIECEFLAG.PIECEFLAG_HYPERGEM);
						this.StartHypergemEffect(pieceBejLive3);
					}
					for (int num6 = 0; num6 < this.aNewPieceVector.Count; num6++)
					{
						PieceBejLive pieceBejLive4 = this.aNewPieceVector[num6];
						if (pieceBejLive4.mFlags == 0)
						{
							pieceBejLive4.mColor = this.aGemList[this.mRand.Next(this.aGemList.Count)];
						}
						if (num3 >= 150 && num6 == 0)
						{
							this.Hypergemify(pieceBejLive4);
							flag3 = true;
						}
						if (num6 == 0)
						{
							int mColor = pieceBejLive4.mColor;
						}
						else
						{
							int mColor2 = pieceBejLive4.mColor;
						}
					}
					if (this.mBlitzMode || this.mEndlessMode || this.mTimedMode || this.mLevel == 0 || SexyAppBase.IsInTrialMode)
					{
						flag3 |= this.FindMove(null, 0, true, true);
					}
					if (!allowCascades)
					{
						flag3 &= !this.HasSet();
					}
					flag3 &= !this.HasIllegalSet();
					if (flag3)
					{
						flag3 &= this.PiecesDropped(ref this.aNewPieceVector);
					}
					if (flag3)
					{
						break;
					}
					num3++;
				}
				return;
			}
		}

		private bool BlanksFilled()
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (this.mBoard[i, j] == null)
					{
						return false;
					}
				}
			}
			return true;
		}

		private bool IsPieceSwapping(PieceBejLive thePiece)
		{
			return this.IsPieceSwapping(thePiece, false);
		}

		private bool IsPieceSwapping(PieceBejLive thePiece, bool includeIgnored)
		{
			for (int i = 0; i < this.mSwapDataVector.Count; i++)
			{
				if ((!this.mSwapDataVector[i].mIgnore || includeIgnored) && (this.mSwapDataVector[i].mPiece1 == thePiece || this.mSwapDataVector[i].mPiece2 == thePiece))
				{
					return true;
				}
			}
			return false;
		}

		private bool IsPieceMatching(PieceBejLive pPiece)
		{
			return pPiece.IsShrinking();
		}

		private bool AllowNoMoreMoves()
		{
			return !this.mBlitzMode && !this.mEndlessMode && !this.mTimedMode && this.mLevel != 0;
		}

		private bool WantSpecialPiece(ref List<PieceBejLive> thePieceVector)
		{
			this.mDroppingMultiplier = false;
			bool flag = false;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null)
					{
						flag |= pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER);
					}
				}
			}
			if (this.mReadyForDrop && this.mWantGemsCleared != 0 && !this.mDidTimeUp)
			{
				int maxMovesStat = this.GetMaxMovesStat(Stats.STAT_GEMS_CLEARED);
				int num = this.mPointMultiplier;
				for (int k = 0; k < 8; k++)
				{
					for (int l = 0; l < 8; l++)
					{
						PieceBejLive pieceBejLive2 = this.mBoard[k, l];
						if (pieceBejLive2 != null && pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
						{
							num++;
						}
					}
				}
				if ((BoardBejLive.mBlitzFlags & 1) != 0 || num == 1)
				{
					if (this.mDropGameTick == 0 && maxMovesStat >= 12)
					{
						this.mDroppingMultiplier = true;
					}
				}
				else if (maxMovesStat >= this.mWantGemsCleared)
				{
					this.mDroppingMultiplier = true;
				}
				if (this.mDroppingMultiplier && num >= 8)
				{
					this.mDroppingMultiplier = false;
				}
				if (this.mDroppingMultiplier)
				{
					return true;
				}
			}
			return false;
		}

		private void DropSpecialPiece(ref List<PieceBejLive> thePieceVector)
		{
			if (this.mMultiplierCount >= this.MAX_MULTIPLIER - 1)
			{
				return;
			}
			this.mApp.PlaySample(Resources.SOUND_MULTIPLIER_MAKE);
			int num = this.mRand.Next() % thePieceVector.Count;
			for (int i = 0; i < 7; i++)
			{
				thePieceVector[num].mColor = this.mRand.Next() % 7;
				int num2 = 0;
				for (int j = 0; j < 8; j++)
				{
					for (int k = 0; k < 8; k++)
					{
						PieceBejLive pieceBejLive = this.mBoard[j, k];
						if (pieceBejLive != null && pieceBejLive.mY > 0f && pieceBejLive.mColor == thePieceVector[num].mColor)
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
			this.RemoveGemsFromBlitzBatch(thePieceVector[num]);
			this.RemoveGemsFromMultiFontBatch(thePieceVector[num]);
			thePieceVector[num].SetFlag(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER);
			this.mMultiplierCount++;
			this.mDropGameTick = this.mGameTicks;
			this.mReadyForDrop = false;
			this.mWantGemsCleared = 0;
		}

		private int NumPiecesWithFlag(PIECEFLAG theFlag)
		{
			return this.NumPiecesWithFlag((int)theFlag);
		}

		private int NumPiecesWithFlag(int theFlag)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null && pieceBejLive.IsFlagSet(theFlag))
					{
						num++;
					}
				}
			}
			return num;
		}

		private bool PiecesDropped(ref List<PieceBejLive> thePieceVector)
		{
			return true;
		}

		private bool WantHyperMixers()
		{
			return false;
		}

		private void DeletePiece(PieceBejLive thePiece)
		{
			this.mGemsCleared++;
			if (thePiece.mColor >= 0)
			{
				BoardBejLive.oneMoveGameColourCounter[thePiece.mColor]++;
			}
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
			{
				BoardBejLive.oneMoveGameTypeCounter[0]++;
			}
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
			{
				BoardBejLive.oneMoveGameTypeCounter[1]++;
			}
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
			{
				BoardBejLive.oneMoveGameTypeCounter[2]++;
			}
			ReportAchievement.ReportSuperconductor(BoardBejLive.oneMoveGameColourCounter, BoardBejLive.oneMoveGameTypeCounter);
			this.RemoveGemsFromBlitzBatch(thePiece);
			this.TallyPiece(thePiece);
			for (int i = 0; i < this.mSwapDataVector.Count; i++)
			{
				SwapData swapData = this.mSwapDataVector[i];
				if (swapData.mPiece1 == thePiece || swapData.mPiece2 == thePiece)
				{
					if (swapData.mSwapPct.GetDouble < 0.0)
					{
						stdC.swap<int>(ref swapData.mPiece1.mCol, ref swapData.mPiece2.mCol);
						stdC.swap<int>(ref swapData.mPiece1.mRow, ref swapData.mPiece2.mRow);
						stdC.swap<PieceBejLive>(ref this.mBoard[swapData.mPiece1.mRow, swapData.mPiece1.mCol], ref this.mBoard[swapData.mPiece2.mRow, swapData.mPiece2.mCol]);
					}
					if (swapData.mPiece1 != null)
					{
						swapData.mPiece1.mX = (float)base.GetColX(swapData.mPiece1.mCol);
						swapData.mPiece1.mY = (float)base.GetRowY(swapData.mPiece1.mRow);
						swapData.mPiece1 = null;
					}
					if (swapData.mPiece2 != null)
					{
						swapData.mPiece2.mX = (float)base.GetColX(swapData.mPiece2.mCol);
						swapData.mPiece2.mY = (float)base.GetRowY(swapData.mPiece2.mRow);
						swapData.mPiece2 = null;
					}
					this.mSwapDataVector[i].PrepareForReuse();
					this.mSwapDataVector.RemoveAt(i);
					i--;
				}
			}
			for (int j = 0; j < thePiece.mRow; j++)
			{
				PieceBejLive pieceBejLive = this.mBoard[j, thePiece.mCol];
				if (pieceBejLive != null)
				{
					this.SetMoveCredit(pieceBejLive, thePiece.mMoveCreditId);
				}
			}
			this.mNextColumnCredit[thePiece.mCol] = Math.Max(this.mNextColumnCredit[thePiece.mCol], thePiece.mMoveCreditId);
			this.mBoard[thePiece.mRow, thePiece.mCol] = null;
			if (thePiece == this.mSelectedPiece)
			{
				this.mSelectedPiece = null;
			}
			thePiece.PrepareForReuse();
		}

		private bool FindMove(int[] theCoords, int theMoveNum, bool horz, bool vert, bool reverse)
		{
			return this.FindMove(theCoords, theMoveNum, horz, vert, reverse, null);
		}

		private bool FindMove(int[] theCoords, int theMoveNum, bool horz, bool vert)
		{
			return this.FindMove(theCoords, theMoveNum, horz, vert, false, null);
		}

		private bool FindMove(int[] theCoords, int theMoveNum, bool horz, bool vert, bool reverse, PieceBejLive theIncludePiece)
		{
			int num = 0;
			int num2 = (reverse ? 7 : 0);
			int num3 = (reverse ? (-1) : 8);
			int num4 = (reverse ? (-1) : 1);
			for (int num5 = num2; num5 != num3; num5 += num4)
			{
				for (int i = 0; i < 8; i++)
				{
					PieceBejLive pieceBejLive = this.mBoard[num5, i];
					if (pieceBejLive != null && this.WillPieceBeStill(pieceBejLive))
					{
						for (int j = 0; j < 4; j++)
						{
							int num6 = i + this.tempSwapArray[j, 0];
							int num7 = num5 + this.tempSwapArray[j, 1];
							if (num6 >= 0 && num6 < 8 && num7 >= 0 && num7 < 8)
							{
								PieceBejLive pieceBejLive2 = pieceBejLive;
								bool flag = false;
								bool flag2 = theIncludePiece == null;
								if (pieceBejLive != null && pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM) && this.mBoard[num7, num6] != null)
								{
									if (theIncludePiece != null && pieceBejLive.mColor == theIncludePiece.mColor)
									{
										flag2 = true;
									}
									flag = true;
								}
								if (this.mBoard[num7, num6] != null && this.mBoard[num7, num6].mColor != -1 && this.WillPieceBeStill(this.mBoard[num7, num6]))
								{
									this.mBoard[num5, i] = this.mBoard[num7, num6];
									this.mBoard[num7, num6] = pieceBejLive2;
									flag2 |= theIncludePiece == this.mBoard[num5, i];
									int num8 = i;
									int num9 = i;
									while (num8 > 0 && this.mBoard[num5, num8 - 1] != null && this.mBoard[num5, i].mColor == this.mBoard[num5, num8 - 1].mColor)
									{
										if (!this.WillPieceBeStill(this.mBoard[num5, num8 - 1]))
										{
											break;
										}
										flag2 |= theIncludePiece == this.mBoard[num5, num8 - 1];
										num8--;
									}
									while (num9 < 7 && this.mBoard[num5, num9 + 1] != null && this.mBoard[num5, i].mColor == this.mBoard[num5, num9 + 1].mColor && this.WillPieceBeStill(this.mBoard[num5, num9 + 1]))
									{
										flag2 |= theIncludePiece == this.mBoard[num5, num9 + 1];
										num9++;
									}
									int num10 = num5;
									int num11 = num5;
									while (num10 > 0 && this.mBoard[num10 - 1, i] != null && this.mBoard[num5, i].mColor == this.mBoard[num10 - 1, i].mColor)
									{
										if (!this.WillPieceBeStill(this.mBoard[num10 - 1, i]))
										{
											break;
										}
										flag2 |= theIncludePiece == this.mBoard[num10 - 1, i];
										num10--;
									}
									while (num11 < 7 && this.mBoard[num11 + 1, i] != null && this.mBoard[num5, i].mColor == this.mBoard[num11 + 1, i].mColor && !this.IsPieceMatching(this.mBoard[num11 + 1, i]))
									{
										flag2 |= theIncludePiece == this.mBoard[num11 + 1, i];
										num11++;
									}
									pieceBejLive2 = this.mBoard[num5, i];
									this.mBoard[num5, i] = this.mBoard[num7, num6];
									this.mBoard[num7, num6] = pieceBejLive2;
									if ((num9 - num8 >= 2 && horz) || (num11 - num10 >= 2 && vert))
									{
										flag = true;
									}
								}
								if (flag)
								{
									if (num == theMoveNum)
									{
										if (theCoords != null)
										{
											theCoords[0] = i;
											theCoords[1] = num5;
											theCoords[2] = num6;
											theCoords[3] = num7;
										}
										return true;
									}
									num++;
								}
							}
						}
					}
				}
			}
			return false;
		}

		private bool HasIllegalSet()
		{
			for (int i = 0; i < 8; i++)
			{
				int num = 0;
				int num2 = -1;
				bool flag = false;
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null)
					{
						bool flag2 = pieceBejLive.mCreatedTick == this.mUpdateCnt && pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER);
						if (pieceBejLive.mColor != -1 && pieceBejLive.mColor == num2)
						{
							flag = flag || flag2;
							if (++num >= 3 && flag)
							{
								return true;
							}
						}
						else
						{
							num2 = pieceBejLive.mColor;
							num = 1;
							flag = flag2;
						}
					}
					else
					{
						num2 = -1;
					}
				}
			}
			for (int j = 0; j < 8; j++)
			{
				int num3 = 0;
				int num4 = -1;
				bool flag3 = false;
				for (int i = 0; i < 8; i++)
				{
					PieceBejLive pieceBejLive2 = this.mBoard[i, j];
					if (pieceBejLive2 != null)
					{
						bool flag4 = pieceBejLive2.mCreatedTick == this.mUpdateCnt && pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER);
						if (pieceBejLive2.mColor != -1 && pieceBejLive2.mColor == num4)
						{
							flag3 = flag3 || flag4;
							if (++num3 >= 3 && flag3)
							{
								return true;
							}
						}
						else
						{
							num4 = pieceBejLive2.mColor;
							num3 = 1;
							flag3 = flag4;
						}
					}
					else
					{
						num4 = -1;
					}
				}
			}
			return false;
		}

		private bool HasSet()
		{
			return this.HasSet(null);
		}

		private bool HasSet(PieceBejLive theCheckPiece)
		{
			for (int i = 0; i < 8; i++)
			{
				int num = 0;
				int num2 = -1;
				bool flag = false;
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null)
					{
						if (pieceBejLive.mColor != -1 && pieceBejLive.mColor == num2)
						{
							flag |= pieceBejLive == theCheckPiece;
							if (++num >= 3 && flag)
							{
								return true;
							}
						}
						else
						{
							num2 = pieceBejLive.mColor;
							num = 1;
							flag = pieceBejLive == theCheckPiece || theCheckPiece == null;
						}
					}
					else
					{
						num2 = -1;
					}
				}
			}
			for (int j = 0; j < 8; j++)
			{
				int num3 = 0;
				int num4 = -1;
				bool flag2 = false;
				for (int i = 0; i < 8; i++)
				{
					PieceBejLive pieceBejLive2 = this.mBoard[i, j];
					if (pieceBejLive2 != null)
					{
						if (pieceBejLive2.mColor != -1 && pieceBejLive2.mColor == num4)
						{
							flag2 |= pieceBejLive2 == theCheckPiece;
							if (++num3 >= 3 && flag2)
							{
								return true;
							}
						}
						else
						{
							num4 = pieceBejLive2.mColor;
							num3 = 1;
							flag2 = pieceBejLive2 == theCheckPiece || theCheckPiece == null;
						}
					}
					else
					{
						num4 = -1;
					}
				}
			}
			return false;
		}

		private bool FindSets(bool fromUpdateSwapping, PieceBejLive thePiece1)
		{
			return this.FindSets(fromUpdateSwapping, thePiece1, null);
		}

		private bool FindSets(bool fromUpdateSwapping)
		{
			return this.FindSets(fromUpdateSwapping, null, null);
		}

		private bool FindSets()
		{
			return this.FindSets(false, null, null);
		}

		private bool FindSets(bool fromUpdateSwapping, PieceBejLive thePiece1, PieceBejLive thePiece2)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			BoardBejLive.aDelayingPieceSet.Clear();
			BoardBejLive.aTallyPieceSet.Clear();
			for (int i = 0; i < BoardBejLive.aMatchedSets.Count; i++)
			{
				BoardBejLive.aMatchedSets[i].PrepareForReuse();
			}
			BoardBejLive.aMatchedSets.Clear();
			BoardBejLive.aMoveCreditSet.Clear();
			foreach (PowerUpPiecePair powerUpPiecePair in BoardBejLive.aDeferPowerupMap.Values)
			{
				powerUpPiecePair.PrepareForReuse();
			}
			BoardBejLive.aDeferPowerupMap.Clear();
			BoardBejLive.aDeferLaserVector.Clear();
			BoardBejLive.aDeferExplodeVector.Clear();
			BoardBejLive.matchedColours.Clear();
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					bool flag4 = false;
					int num5 = 0;
					int num6 = -1;
					int num7 = 0;
					int num8 = 0;
					int num9 = 0;
					int num10 = 0;
					int num11 = -1;
					for (int l = 0; l < 8; l++)
					{
						int num12;
						int num13;
						if (j == 0)
						{
							num12 = k;
							num13 = l;
						}
						else
						{
							num12 = l;
							num13 = k;
						}
						int num14 = -1;
						bool flag5 = false;
						PieceBejLive pieceBejLive = this.mBoard[num13, num12];
						bool flag6 = (pieceBejLive != null && this.IsPieceStill(pieceBejLive)) || BoardBejLive.aTallyPieceSet.Contains(pieceBejLive);
						if (pieceBejLive != null && !BoardBejLive.aDelayingPieceSet.Contains(pieceBejLive) && (this.WillPieceBeStill(pieceBejLive) || BoardBejLive.aTallyPieceSet.Contains(pieceBejLive)))
						{
							flag4 |= !flag6;
							if (pieceBejLive.mChangedTick == this.mUpdateCnt)
							{
								num14 = pieceBejLive.mLastColor;
							}
							else
							{
								num14 = pieceBejLive.mColor;
							}
							if (num14 == num6 && num14 != -1)
							{
								num10 = num12;
								num9 = num13;
								flag5 = true;
								num5++;
								num11 = num14;
							}
						}
						if (!flag5 || l == 7)
						{
							if (num5 >= 3)
							{
								this.mNoMoveCount = 0;
								flag2 = true;
								if (!BoardBejLive.matchedColours.Contains(num11))
								{
									BoardBejLive.matchedColours.Add(num11);
								}
								int num15 = 0;
								bool flag7 = false;
								MatchSet newMatchSet = MatchSet.GetNewMatchSet();
								bool flag8 = false;
								bool flag9 = false;
								bool flag10 = false;
								int num16 = -1;
								int num17 = -1;
								newMatchSet.mMatchId = this.mCurMoveCreditId++;
								newMatchSet.mMoveCreditId = -1;
								newMatchSet.mExplosionCount = 0;
								for (int m = num7; m <= num9; m++)
								{
									for (int n = num8; n <= num10; n++)
									{
										PieceBejLive pieceBejLive2 = this.mBoard[m, n];
										if (pieceBejLive2 != null)
										{
											for (int num18 = 0; num18 < 4; num18++)
											{
												PieceBejLive pieceAtRowCol = this.GetPieceAtRowCol(pieceBejLive2.mRow + BoardBejLive.anOffsets[num18, 0], pieceBejLive2.mCol + BoardBejLive.anOffsets[num18, 1]);
												if (pieceAtRowCol != null && pieceAtRowCol.mColor == pieceBejLive2.mColor)
												{
													if (pieceAtRowCol.mY != (float)base.GetRowY(pieceAtRowCol.mRow))
													{
														flag9 = true;
													}
													PieceBejLive pieceAtRowCol2 = this.GetPieceAtRowCol(pieceBejLive2.mRow + BoardBejLive.anOffsets[num18, 0] * 2, pieceBejLive2.mCol + BoardBejLive.anOffsets[num18, 1] * 2);
													if (pieceAtRowCol2 != null && pieceAtRowCol2.mColor == pieceBejLive2.mColor && pieceAtRowCol2.mY != (float)base.GetRowY(pieceAtRowCol2.mRow))
													{
														flag9 = true;
													}
												}
											}
											if (pieceBejLive2.mSwapTick == this.mUpdateCnt)
											{
												flag10 = true;
											}
											if (pieceBejLive2.mColor == num6)
											{
												flag7 |= pieceBejLive2.mSwapTick == this.mUpdateCnt;
												newMatchSet.mPieces.Add(pieceBejLive2);
											}
											num16 = Math.Max(num16, pieceBejLive2.mMoveCreditId);
											num17 = Math.Max(num17, pieceBejLive2.mLastMoveCreditId);
										}
									}
								}
								if (num16 == -1)
								{
									num16 = num17;
								}
								newMatchSet.mMoveCreditId = num16;
								if (flag4 && fromUpdateSwapping)
								{
									flag3 = true;
									this.miBufferCheckCount = 1;
									for (int num19 = num7; num19 <= num9; num19++)
									{
										for (int num20 = num8; num20 <= num10; num20++)
										{
											PieceBejLive pieceBejLive3 = this.mBoard[num19, num20];
											if (pieceBejLive3 != null)
											{
												BoardBejLive.aDelayingPieceSet.Add(pieceBejLive3);
											}
										}
									}
									newMatchSet.PrepareForReuse();
									goto IL_A4F;
								}
								if (!flag10 && flag9)
								{
									for (int num21 = num7; num21 <= num9; num21++)
									{
										for (int num22 = num8; num22 <= num10; num22++)
										{
											PieceBejLive pieceBejLive4 = this.mBoard[num21, num22];
											if (pieceBejLive4 != null)
											{
												BoardBejLive.aDelayingPieceSet.Add(pieceBejLive4);
											}
										}
									}
									newMatchSet.PrepareForReuse();
									goto IL_A4F;
								}
								num++;
								this.mChainCount++;
								ReportAchievement.ReportUltimateCascade(this.mChainCount);
								if (!flag7)
								{
									num2++;
								}
								BoardBejLive.aPowerupCandidates.Clear();
								BoardBejLive.aNewestPowerupCandidates.Clear();
								for (int num23 = num7; num23 <= num9; num23++)
								{
									for (int num24 = num8; num24 <= num10; num24++)
									{
										PieceBejLive pieceBejLive5 = this.mBoard[num23, num24];
										if (pieceBejLive5 != null)
										{
											pieceBejLive5.mMatchId = newMatchSet.mMatchId;
											pieceBejLive5.mMoveCreditId = num16;
											if (pieceBejLive5.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
											{
												this.IncPointMult(pieceBejLive5);
												pieceBejLive5.ClearFlag(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER);
											}
											if (!flag8)
											{
												flag8 = this.ComboProcess(pieceBejLive5.mColor);
											}
											bool flag11 = false;
											if (pieceBejLive5.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
											{
												BoardBejLive.aDeferExplodeVector.Add(pieceBejLive5);
												newMatchSet.mExplosionCount++;
											}
											if (pieceBejLive5.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER) && pieceBejLive5.mChangedTick != this.mUpdateCnt)
											{
												int num25 = this.FindStormIdxFor(pieceBejLive5);
												if (num25 == -1)
												{
													this.AddToStat(Stats.STAT_LASERGEMS_USED, 1, num16);
													if (this.mFullLaser)
													{
														this.DoStarGem(pieceBejLive5);
													}
													else
													{
														LightningStorm newLightningStorm = LightningStorm.GetNewLightningStorm(this, pieceBejLive5, this.mFullLaser ? LightningStorm.StormType.STORM_BOTH : LightningStorm.StormType.STORM_SHORT);
														this.mLightningStorms.Add(newLightningStorm);
														this.mApp.LockOrientation(true);
													}
													this.miLightningGemCount--;
												}
											}
											else if (pieceBejLive5.mChangedTick == this.mUpdateCnt && pieceBejLive5.mColor > -1 && pieceBejLive5.mColor < 7)
											{
												if (pieceBejLive5.mFlags == 0 || pieceBejLive5.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
												{
													if (this.AllowPowerups())
													{
														flag = true;
														pieceBejLive5.mChangedTick = this.mUpdateCnt;
														BoardBejLive.aTallyPieceSet.Remove(pieceBejLive5);
														pieceBejLive5.mMoveCreditId = num16;
														BoardBejLive.aDeferLaserVector.Add(pieceBejLive5);
													}
													flag11 = false;
												}
											}
											else if (pieceBejLive5.mChangedTick != this.mUpdateCnt)
											{
												flag11 = pieceBejLive5.mFlags == 0 || pieceBejLive5.IsFlagSet(PIECEFLAG.PIECEFLAG_FRAGMENT);
												int num26 = 0;
												for (int num27 = num7; num27 <= num9; num27++)
												{
													for (int num28 = num8; num28 <= num10; num28++)
													{
														PieceBejLive pieceBejLive6 = this.mBoard[num27, num28];
														if (pieceBejLive6 != pieceBejLive5 && pieceBejLive6.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
														{
															num26++;
														}
													}
												}
												if (num26 == 0)
												{
													pieceBejLive5.mShrinking = true;
													if (flag7)
													{
														pieceBejLive5.mScale.SetCurve(CurvedValDefinition.pieceMatchGrowScaleCurve);
													}
													else
													{
														pieceBejLive5.mScale.SetCurve(CurvedValDefinition.pieceCascadeGrowScaleCurve);
													}
													pieceBejLive5.mScale.mIncRate *= (double)this.GetMatchSpeed();
												}
												pieceBejLive5.mChangedTick = this.mUpdateCnt;
												pieceBejLive5.mLastColor = pieceBejLive5.mColor;
												BoardBejLive.aTallyPieceSet.Add(pieceBejLive5);
											}
											if (flag11)
											{
												if (pieceBejLive5.mLastActiveTick > num15)
												{
													num15 = pieceBejLive5.mLastActiveTick;
													BoardBejLive.aNewestPowerupCandidates.Clear();
												}
												if (pieceBejLive5.mLastActiveTick == num15)
												{
													BoardBejLive.aNewestPowerupCandidates.Add(pieceBejLive5);
												}
												BoardBejLive.aPowerupCandidates.Add(pieceBejLive5);
											}
										}
									}
								}
								if (BoardBejLive.aPowerupCandidates.Count > 0)
								{
									PieceBejLive pieceBejLive7 = BoardBejLive.aNewestPowerupCandidates[BoardBejLive.aNewestPowerupCandidates.Count / 2];
									for (int num29 = 0; num29 < BoardBejLive.aPowerupCandidates.Count; num29++)
									{
										PieceBejLive pieceBejLive8 = BoardBejLive.aPowerupCandidates[num29];
										PieceBejLive pieceAtRowCol3;
										PieceBejLive pieceAtRowCol4;
										if (j == 0)
										{
											pieceAtRowCol3 = this.GetPieceAtRowCol(pieceBejLive8.mRow, pieceBejLive8.mCol + 1);
											pieceAtRowCol4 = this.GetPieceAtRowCol(pieceBejLive8.mRow, pieceBejLive8.mCol - 1);
										}
										else
										{
											pieceAtRowCol3 = this.GetPieceAtRowCol(pieceBejLive8.mRow + 1, pieceBejLive8.mCol);
											pieceAtRowCol4 = this.GetPieceAtRowCol(pieceBejLive8.mRow - 1, pieceBejLive8.mCol);
										}
										if (pieceAtRowCol3 != null && pieceAtRowCol3.mColor == pieceBejLive8.mColor && pieceAtRowCol4 != null && pieceAtRowCol4.mColor == pieceBejLive8.mColor)
										{
											pieceBejLive7 = pieceBejLive8;
										}
									}
									int num30 = Math.Max(Math.Abs(num8 - pieceBejLive7.mCol), Math.Abs(num7 - pieceBejLive7.mRow));
									int num31 = Math.Max(Math.Abs(num10 - pieceBejLive7.mCol), Math.Abs(num9 - pieceBejLive7.mRow));
									PieceBejLive pieceBejLive9 = null;
									for (int num32 = 1; num32 <= 2; num32++)
									{
										if (num30 > num31)
										{
											if (j == 0)
											{
												pieceBejLive9 = this.GetPieceAtRowCol(Math.Max(num7, pieceBejLive7.mRow - num32), pieceBejLive7.mCol);
											}
											else
											{
												pieceBejLive9 = this.GetPieceAtRowCol(pieceBejLive7.mRow, Math.Max(num8, pieceBejLive7.mCol - num32));
											}
										}
										else if (j == 0)
										{
											pieceBejLive9 = this.GetPieceAtRowCol(Math.Min(num9, pieceBejLive7.mRow + num32), pieceBejLive7.mCol);
										}
										else
										{
											pieceBejLive9 = this.GetPieceAtRowCol(pieceBejLive7.mRow, Math.Min(num10, pieceBejLive7.mCol + num32));
										}
										if (pieceBejLive9 != null && pieceBejLive9.mFlags == 0)
										{
											break;
										}
									}
									if (this.AllowPowerups())
									{
										if (num5 >= 5 && !pieceBejLive7.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
										{
											pieceBejLive7.mMoveCreditId = num16;
											if (pieceBejLive9 != null)
											{
												pieceBejLive9.mMoveCreditId = num16;
											}
											BoardBejLive.aDeferPowerupMap.Add(pieceBejLive7, PowerUpPiecePair.GetNewPowerUpPiecePair(num5, pieceBejLive9));
										}
										else if (num5 >= 4)
										{
											pieceBejLive7.mMoveCreditId = num16;
											if (pieceBejLive9 != null)
											{
												pieceBejLive9.mMoveCreditId = num16;
											}
											BoardBejLive.aDeferPowerupMap.Add(pieceBejLive7, PowerUpPiecePair.GetNewPowerUpPiecePair(num5, pieceBejLive9));
										}
									}
									if (num5 == 3 && this.MatchThree(pieceBejLive7))
									{
										BoardBejLive.aTallyPieceSet.Remove(pieceBejLive7);
									}
								}
								BoardBejLive.aMatchedSets.Add(newMatchSet);
							}
							num6 = num14;
							num5 = 1;
							flag4 = !flag6;
							num8 = num12;
							num7 = num13;
							num10 = num12;
							num9 = num13;
						}
						IL_A4F:;
					}
				}
			}
			if (fromUpdateSwapping)
			{
				if (BoardBejLive.matchedColours.Count > 0)
				{
					if (BoardBejLive.matchedColours.Count > 1)
					{
						if (!BoardBejLive.matchedColours.Contains(thePiece1.mColor) || thePiece1.mColor != this.lastColourMatch)
						{
							this.monocolouristCount = 0;
							this.lastColourMatch = thePiece1.mColor;
						}
					}
					else if (this.lastColourMatch != BoardBejLive.matchedColours[0])
					{
						this.lastColourMatch = BoardBejLive.matchedColours[0];
						this.monocolouristCount = 0;
					}
				}
				if (!flag2)
				{
					this.noIllegalMovesAchievementFlag = false;
				}
			}
			for (int num33 = 0; num33 < BoardBejLive.aDeferExplodeVector.Count; num33++)
			{
				PieceBejLive pieceBejLive10 = BoardBejLive.aDeferExplodeVector[num33];
				pieceBejLive10.mExplodeDelay = 1;
			}
			for (int num34 = 0; num34 < BoardBejLive.aDeferLaserVector.Count; num34++)
			{
				PieceBejLive pieceBejLive11 = BoardBejLive.aDeferLaserVector[num34];
				if (pieceBejLive11.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
				{
					pieceBejLive11.mExplodeDelay = 1;
					pieceBejLive11.mFlags = 0;
				}
				if (this.AllowLaserGems())
				{
					this.powerBrokerAchievementCount++;
					this.AddToStat(Stats.STAT_LASERGEMS_MADE, 1, pieceBejLive11.mMoveCreditId);
					this.Laserify(pieceBejLive11);
				}
				else
				{
					this.powerBrokerAchievementCount++;
					this.AddToStat(Stats.STAT_FLAMEGEMS_MADE, 1, pieceBejLive11.mMoveCreditId);
					this.Flamify(pieceBejLive11);
				}
			}
			foreach (KeyValuePair<PieceBejLive, PowerUpPiecePair> keyValuePair in BoardBejLive.aDeferPowerupMap)
			{
				PieceBejLive pieceBejLive12 = keyValuePair.Key;
				if (pieceBejLive12.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
				{
					pieceBejLive12 = keyValuePair.Value.Piece;
				}
				if (pieceBejLive12.mFlags == 0)
				{
					BoardBejLive.aTallyPieceSet.Remove(pieceBejLive12);
					if (keyValuePair.Value.MatchCount > 4)
					{
						this.powerBrokerAchievementCount++;
						this.AddToStat(Stats.STAT_HYPERGEMS_MADE, 1, pieceBejLive12.mMoveCreditId);
						this.Hypergemify(pieceBejLive12);
					}
					else
					{
						this.powerBrokerAchievementCount++;
						this.Flamify(pieceBejLive12);
						flag = true;
						this.AddToStat(Stats.STAT_FLAMEGEMS_MADE, 1, pieceBejLive12.mMoveCreditId);
					}
					pieceBejLive12.mChangedTick = this.mUpdateCnt;
					pieceBejLive12.mLastColor = pieceBejLive12.mColor;
				}
			}
			ReportAchievement.ReportPowerBroker(this.powerBrokerAchievementCount);
			bool flag12 = false;
			for (int num35 = 0; num35 < BoardBejLive.aMatchedSets.Count; num35++)
			{
				MatchSet matchSet = BoardBejLive.aMatchedSets[num35];
				PieceBejLive pieceBejLive13 = null;
				bool flag13 = false;
				bool flag14 = false;
				int num36 = 0;
				int num37 = 0;
				for (int num38 = 0; num38 < matchSet.mPieces.Count; num38++)
				{
					PieceBejLive pieceBejLive14 = matchSet.mPieces[num38];
					num36 += (int)pieceBejLive14.mX;
					num37 += (int)pieceBejLive14.mY;
					if ((pieceBejLive14.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME) || pieceBejLive14.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM) || pieceBejLive14.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER)) && !pieceBejLive14.mDestructing && pieceBejLive14.mChangedTick == this.mUpdateCnt)
					{
						pieceBejLive13 = pieceBejLive14;
					}
					if (pieceBejLive14.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER) && pieceBejLive14.mChangedTick != this.mUpdateCnt)
					{
						flag13 = true;
					}
				}
				if (fromUpdateSwapping && this.mSpeedBonusFlameModePct > 0f)
				{
					int num39 = 0;
					PieceBejLive pieceBejLive15 = null;
					for (int num40 = 0; num40 < matchSet.mPieces.Count; num40++)
					{
						PieceBejLive pieceBejLive16 = matchSet.mPieces[num40];
						if (pieceBejLive16.mSwapTick > num39)
						{
							pieceBejLive15 = pieceBejLive16;
							num39 = pieceBejLive16.mSwapTick;
						}
					}
					if (pieceBejLive15 != null)
					{
						pieceBejLive15.SetFlag(PIECEFLAG.PIECEFLAG_INFERNO_SWAP);
						pieceBejLive15.mExplodeDelay = 1;
						this.AddToStat(Stats.STAT_BLAZING_SPEED_EXPLOSION, 1);
					}
				}
				if (flag14)
				{
					for (int num41 = 0; num41 < matchSet.mPieces.Count; num41++)
					{
						PieceBejLive pieceBejLive17 = matchSet.mPieces[num41];
						pieceBejLive17.mIsBulging = true;
						pieceBejLive17.mExplodeDelay = 0;
						int num42 = this.FindStormIdxFor(pieceBejLive17);
						if (num42 != -1)
						{
							this.mLightningStorms[num42].PrepareForReuse();
							this.mLightningStorms.RemoveAt(num42);
							if (this.mLightningStorms.Count == 0)
							{
								this.mApp.LockOrientation(false);
							}
						}
						BoardBejLive.aTallyPieceSet.Remove(pieceBejLive17);
					}
				}
				else
				{
					flag12 = true;
					if (flag13)
					{
						for (int num43 = 0; num43 < matchSet.mPieces.Count; num43++)
						{
							PieceBejLive pieceBejLive18 = matchSet.mPieces[num43];
							pieceBejLive18.mCanMatch = false;
						}
					}
					else if (pieceBejLive13 != null)
					{
						for (int num44 = 0; num44 < matchSet.mPieces.Count; num44++)
						{
							PieceBejLive pieceBejLive19 = matchSet.mPieces[num44];
							if (pieceBejLive19 != pieceBejLive13 && (pieceBejLive19.mFlags == 0 || pieceBejLive19.IsFlagSet(PIECEFLAG.PIECEFLAG_SKULL)))
							{
								pieceBejLive19.mDestPct.SetCurve(CurvedValDefinition.aPiecemDestPctCurve);
								pieceBejLive19.mAlpha.SetCurve(CurvedValDefinition.aPiecemAlphaCurve);
								pieceBejLive19.mDestCol = pieceBejLive13.mCol;
								pieceBejLive19.mDestRow = pieceBejLive13.mRow;
							}
						}
					}
					int count = matchSet.mPieces.Count;
					int num45 = num36 / count;
					int num46 = num37 / count;
					num3 += num45;
					num4 += num46;
					this.AddToStat(Stats.STAT_NUM_MATCHES, 1, matchSet.mMoveCreditId);
					int num47 = Math.Max(1, this.GetMoveStat(matchSet.mMoveCreditId, Stats.STAT_NUM_MATCHES));
					int num48 = 50 * num47 + (count - 3) * 50;
					if (count >= 5)
					{
						num48 += (count - 4) * 350;
					}
					if (matchSet.mPieces[0].mColor < 0)
					{
						this.AddPoints(num45 + GameConstants.GEM_WIDTH / 2, num46 + GameConstants.GEM_HEIGHT / 2 - 8, num48, new SexyColor(BoardBejLive.gPointColors[0]), (uint)matchSet.mMatchId, true, true, matchSet.mMoveCreditId);
					}
					else
					{
						this.AddPoints(num45 + GameConstants.GEM_WIDTH / 2, num46 + GameConstants.GEM_HEIGHT / 2 - 8, num48, new SexyColor(BoardBejLive.gPointColors[matchSet.mPieces[0].mColor]), (uint)matchSet.mMatchId, true, true, matchSet.mMoveCreditId);
					}
					this.MaxStat(Stats.STAT_BIGGESTMATCH, count, matchSet.mMoveCreditId);
					BoardBejLive.aMoveCreditSet.Add(matchSet.mMoveCreditId);
				}
			}
			if (BoardBejLive.aMatchedSets.Count > 0)
			{
				this.ProcessMatches(BoardBejLive.aMatchedSets, fromUpdateSwapping);
			}
			if (flag12)
			{
				double num49 = (double)base.GetPanPosition(num3 / num + GameConstants.GEM_WIDTH / 2);
				if (fromUpdateSwapping && this.mSpeedBonusCount <= 0)
				{
					if (flag)
					{
						this.mApp.PlaySample(Resources.SOUND_MULTISHOT, (int)num49);
					}
					else if (num > 1)
					{
						this.mApp.PlaySample(Resources.SOUND_TRANSFER_BIG, (int)num49);
					}
					else
					{
						this.mApp.PlaySample(Resources.SOUND_TRANSFER, (int)num49);
					}
				}
				int num50 = this.GetMaxMovesStat(Stats.STAT_CASCADES) + 1;
				if (num2 == 0)
				{
					num50 = 1;
				}
				if (num50 > 6)
				{
					num50 = 6;
				}
				if (fromUpdateSwapping && this.mSpeedBonusCount > 0)
				{
					this.mApp.PlaySample(Resources.SOUND_SPEED_1 + Math.Min(8, this.mSpeedBonusCount), (int)num49);
				}
				else
				{
					this.mApp.PlaySample(Resources.SOUND_COMBO_1 + num50, (int)num49);
				}
				if (!fromUpdateSwapping)
				{
					for (int num51 = 0; num51 < BoardBejLive.aMoveCreditSet.Count; num51++)
					{
						this.AddToStat(Stats.STAT_CASCADES, 1, BoardBejLive.aMoveCreditSet[num51]);
					}
				}
			}
			for (int num52 = 0; num52 < BoardBejLive.aTallyPieceSet.Count; num52++)
			{
				this.TallyPiece(BoardBejLive.aTallyPieceSet[num52]);
			}
			return (flag3 && fromUpdateSwapping && (BoardBejLive.aDelayingPieceSet.Contains(thePiece1) || BoardBejLive.aDelayingPieceSet.Contains(thePiece2))) || num > 0;
		}

		private bool CanPlay()
		{
			if (this.mDidTimeUp)
			{
				return false;
			}
			if (this.mLevelCompleteCount != 0)
			{
				return false;
			}
			if (this.mGameOverCount != 0)
			{
				return false;
			}
			if (this.mShowLevelDelay > 0)
			{
				return false;
			}
			if (this.mBlitzMode && this.GetTicksLeft() == 0)
			{
				return false;
			}
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null && (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_BOMB) || pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_REALTIME_BOMB)) && pieceBejLive.mCounter == 0)
					{
						return false;
					}
				}
			}
			return !this.mWantLevelup && (this.mLevelBarPct < 1f || this.mSpeedBonusFlameModePct != 0f);
		}

		private bool ForceSwaps()
		{
			return false;
		}

		private void StartHypergemEffect(PieceBejLive thePiece)
		{
		}

		private void StartPieceEffect(PieceBejLive thePiece)
		{
		}

		private bool IsPieceStill(PieceBejLive thePiece)
		{
			return thePiece.mFallVelocity == 0f && (float)base.GetRowY(thePiece.mRow) == thePiece.mY && !this.IsPieceMatching(thePiece) && thePiece.mCanMatch && thePiece.mExplodeDelay == 0 && thePiece.mDestPct.GetDouble == 0.0 && !this.IsPieceSwapping(thePiece);
		}

		private bool WillPieceBeStill(PieceBejLive thePiece)
		{
			return !this.IsPieceMatching(thePiece) && !this.IsPieceSwapping(thePiece) && thePiece.mCanMatch && thePiece.mExplodeDelay == 0 && thePiece.mDestPct.GetDouble == 0.0;
		}

		private int GetMaxMovesStat(Stats theStatNum)
		{
			return this.GetMaxMovesStat((int)theStatNum);
		}

		private int GetMaxMovesStat(int theStatNum)
		{
			ulong num = 0UL;
			for (int i = 0; i < this.mMoveDataVector.Count; i++)
			{
				if (i == 0 || this.mMoveDataVector[i].mStats[theStatNum] > num)
				{
					num = this.mMoveDataVector[i].mStats[theStatNum];
				}
			}
			return (int)num;
		}

		private PieceBejLive GetSelectedPiece()
		{
			return this.mSelectedPiece;
		}

		private bool IsBoardStill()
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive == null || (pieceBejLive != null && !this.IsPieceStill(pieceBejLive)))
					{
						return false;
					}
				}
			}
			return !this.HasSet() && this.mLightningStorms.Count == 0;
		}

		private bool IsGameIdle()
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive == null || (!this.IsPieceStill(pieceBejLive) && !this.IsPieceSwapping(pieceBejLive)))
					{
						return false;
					}
				}
			}
			return !this.HasSet() && this.mLightningStorms.Count == 0 && !this.mFrozen && !this.mEditing;
		}

		private void NewCombo()
		{
			if (!this.WantColorCombos())
			{
				return;
			}
			this.mComboCountDisp = 0f;
			this.mComboCount = 0;
			this.mLastComboCount = 0;
			bool flag;
			do
			{
				flag = true;
				int[] array = new int[1];
				int[] array2 = array;
				for (int i = 0; i < this.mComboLen; i++)
				{
					this.mComboColors[i] = this.mRand.Next() % 7;
					if (++array2[this.mComboColors[i]] >= 3)
					{
						flag = false;
					}
				}
			}
			while (!flag);
		}

		private bool WantColorCombos()
		{
			return false;
		}

		private bool WantsHideOnPause()
		{
			return !this.mFrozen;
		}

		private PieceBejLive GetPieceAt(int theX, int theY)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null && (float)theX >= pieceBejLive.mX && (float)theY >= pieceBejLive.mY && (float)theX < pieceBejLive.mX + (float)GameConstants.GEM_WIDTH && (float)theY < pieceBejLive.mY + (float)GameConstants.GEM_WIDTH)
					{
						return pieceBejLive;
					}
				}
			}
			return null;
		}

		private float GetSwapSpeed()
		{
			return (float)this.mSpeedModeFactor.GetDouble;
		}

		private float GetGravityFactor()
		{
			return 1f + ((float)this.mSpeedModeFactor.GetDouble - 1f) * Constants.mConstants.M(0.203125f);
		}

		private void SwapSucceeded(SwapData theSwapData)
		{
		}

		private void SwapFailed(SwapData theSwapData)
		{
			if (this.mNumLastMoves > 0)
			{
				this.mNumLastMoves--;
			}
			if (this.mApp.mProfile.WantsHint(Profile.Hint.HINT_BAD_MOVE))
			{
				this.alert = new Alert(Dialogs.DIALOG_INSTRUCTION, this, this.mApp);
				this.alert.SetBodyText(Strings.Board_Help_Message_Swap);
				this.alert.AddButton(Dialogoid.ButtonID.OK_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_GREEN, Strings.OK);
				this.alert.Present();
				if (this.mLevel > 0)
				{
					this.mApp.mProfile.DisableHint(Profile.Hint.HINT_BAD_MOVE);
				}
				this.mApp.PlaySample(Resources.SOUND_BAD);
			}
		}

		private void ExamineBoard()
		{
		}

		private void ExplodeAt(int theX, int theY)
		{
			BoardBejLive.gExplodeCount = 0;
			BoardBejLive.gShardCount = 0;
			this.BumpColumns(theX, theY, Constants.mConstants.M(1f));
			this.ExplodeAtHelper(theX, theY);
			if (Constants.mConstants.M(1f) != 0f)
			{
				float[] array = new float[]
				{
					1f, 0.9f, 0.85f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.3f, 0.28f,
					0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f
				};
				float num = array[Math.Min(BoardBejLive.gExplodeCount, 15)];
				float num2 = 0f;
				for (int i = 0; i < 80; i++)
				{
					num2 += num;
					if (num2 >= 1f)
					{
						num2 -= 1f;
						EffectBej3 effectBej = this.mPostFXManager.AllocEffect(EffectBej3.EffectType.TYPE_EXPLOSION);
						int num3 = (int)((double)i * 0.5);
						double num4 = (double)i * 0.503 + (double)(Board.Rand() % 100) / 800.0;
						int num5 = Math.Abs((int)(num4 * 4096.0 / 6.28318) % 4096);
						effectBej.mX = (float)(BoardBejLive.gExplodePoints[BoardBejLive.gExplodeCount - 1, 0] + (int)((float)num3 * Board.COS_TAB[num5]));
						effectBej.mY = (float)(BoardBejLive.gExplodePoints[BoardBejLive.gExplodeCount - 1, 1] + (int)((float)num3 * Board.SIN_TAB[num5]));
						effectBej.mDelay = (float)(num3 / 10);
						this.mPostFXManager.AddEffect(effectBej);
					}
				}
			}
		}

		private void SmallExplodeAt(PieceBejLive thePiece, float theCenterX, float theCenterY, bool process, bool fromFlame)
		{
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_COIN))
			{
				this.DeletePiece(thePiece);
				return;
			}
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
			{
				this.IncPointMult(thePiece);
			}
			if (thePiece.mColor < 0)
			{
				this.AddPoints((int)thePiece.mX, (int)thePiece.mY, (int)Constants.mConstants.M(50f), new SexyColor(BoardBejLive.gAllGemColors[0]), (uint)thePiece.mMatchId, true, true, thePiece.mMoveCreditId);
			}
			else
			{
				this.AddPoints((int)thePiece.mX, (int)thePiece.mY, (int)Constants.mConstants.M(50f), new SexyColor(BoardBejLive.gGemColors[thePiece.mColor]), (uint)thePiece.mMatchId, true, true, thePiece.mMoveCreditId);
			}
			int num = (int)(thePiece.mX + (float)(GameConstants.GEM_WIDTH / 2));
			int num2 = (int)(thePiece.mY + (float)(GameConstants.GEM_HEIGHT / 2));
			float num3 = Constants.mConstants.M(0.01f) * ((float)num - theCenterX);
			float num4 = Constants.mConstants.M(0.01f) * ((float)num2 - theCenterY);
			if (num3 == 0f)
			{
				num4 *= Constants.mConstants.M(2f);
			}
			if (num4 == 0f)
			{
				num3 *= Constants.mConstants.M(2f);
			}
			int num5 = 0;
			while ((float)num5 < Constants.mConstants.M(5f))
			{
				EffectBej3 effectBej = this.mPostFXManager.AllocEffect(EffectBej3.EffectType.TYPE_GEM_SHARD);
				if (thePiece.mColor < 0)
				{
					effectBej.mColor = BoardBejLive.gAllGemColors[0];
				}
				else
				{
					effectBej.mColor = BoardBejLive.gGemColors[thePiece.mColor];
				}
				int num6 = (int)((double)(num5 * (int)((double)(num5 + 120) / 120.0)) * 480.0 / 1024.0);
				double num7 = (double)num5 * 0.503 + (double)(Board.Rand() % 100) / 800.0;
				float num8 = Constants.mConstants.M(1.2f) + Math.Abs(Board.GetRandFloat()) * Constants.mConstants.M(1.2f);
				int num9 = Math.Abs((int)(num7 * 4096.0 / 6.28318) % 4096);
				effectBej.mDX = Board.COS_TAB[num9] * num8 + num3;
				effectBej.mDY = Board.SIN_TAB[num9] * num8 + Constants.mConstants.M(-2f) + num4;
				if (num3 != 0f || num4 != 0f)
				{
					num7 = (double)Board.GetRandFloat() * 3.14159;
					num6 = (int)(Board.GetRandFloat() * Constants.mConstants.S(Constants.mConstants.M(48f)));
					num9 = Math.Abs((int)(num7 * 4096.0 / 6.28318) % 4096);
					effectBej.mX = (float)(num + (int)(Constants.mConstants.M(1f) * (float)num6 * Board.COS_TAB[num9]));
					effectBej.mY = (float)(num2 + (int)(Constants.mConstants.M(1f) * (float)num6 * Board.SIN_TAB[num9]));
					num7 = Math.Atan2((double)(effectBej.mY - theCenterY), (double)(effectBej.mX - theCenterX)) + (double)(Board.GetRandFloat() * Constants.mConstants.M(0.3f));
					num8 = Constants.mConstants.M(3.5f) + Math.Abs(Board.GetRandFloat()) * Constants.mConstants.M(1f);
					num9 = Math.Abs((int)(num7 * 4096.0 / 6.28318) % 4096);
					effectBej.mDX = Board.COS_TAB[num9] * num8;
					effectBej.mDY = Board.SIN_TAB[num9] * num8 + Constants.mConstants.M(-2f);
					effectBej.mDecel = Constants.mConstants.M(0.98f) + Board.GetRandFloat() * Constants.mConstants.M(0.005f);
				}
				else
				{
					effectBej.mX = (float)(num + (int)(Constants.mConstants.M(1.2f) * (float)num6 * effectBej.mDX + Constants.mConstants.M(14f)));
					effectBej.mY = (float)(num2 + (int)(Constants.mConstants.M(1.2f) * (float)num6 * effectBej.mDY + Constants.mConstants.M(10f)));
				}
				effectBej.mAngle = (float)num7;
				effectBej.mDAngle = Constants.mConstants.M(0.05f) * Board.GetRandFloat();
				effectBej.mGravity = Constants.mConstants.M(0.06f);
				effectBej.mValue[0] = Board.GetRandFloat() * 6.28318548f;
				effectBej.mValue[1] = Board.GetRandFloat() * 6.28318548f;
				effectBej.mValue[2] = Constants.mConstants.M(0.045f) * (Constants.mConstants.M(3f) * stdC.fabsf(Board.GetRandFloat()) + Constants.mConstants.M(1f));
				effectBej.mValue[3] = Constants.mConstants.M(0.045f) * (Constants.mConstants.M(3f) * stdC.fabsf(Board.GetRandFloat()) + Constants.mConstants.M(1f));
				effectBej.mDAlpha = Constants.mConstants.M(-0.0025f) * (Constants.mConstants.M(2f) * stdC.fabsf(Board.GetRandFloat()) + Constants.mConstants.M(4f));
				this.mPostFXManager.AddEffect(effectBej);
				num5++;
			}
			float mElectrocutePercent = thePiece.mElectrocutePercent;
			thePiece.mIsExploding = true;
			this.DeletePiece(thePiece);
		}

		private bool HasLargeExplosions()
		{
			return this.HasBoost(Boost.BOOST_INFERNO_GEMS);
		}

		private void AddToStat(Stats theStatNum)
		{
			this.AddToStat((int)theStatNum);
		}

		private void AddToStat(Stats theStatNum, int theNumber)
		{
			this.AddToStat((int)theStatNum, theNumber);
		}

		private void AddToStat(int theStatNum, int theNumber)
		{
			this.AddToStat(theStatNum, theNumber, -1);
		}

		private void AddToStat(int theStatNum)
		{
			this.AddToStat(theStatNum, 1, -1);
		}

		private void AddToStat(Stats theStatNum, int theNumber, int theMoveCreditId)
		{
			this.AddToStat((int)theStatNum, theNumber, theMoveCreditId);
		}

		private void AddToStat(int theStatNum, int theNumber, int theMoveCreditId)
		{
			this.mGameStats[theStatNum] += theNumber;
			if (theMoveCreditId != -1)
			{
				for (int i = 0; i < this.mMoveDataVector.Count; i++)
				{
					if (this.mMoveDataVector[i].mMoveCreditId == theMoveCreditId)
					{
						this.mMoveDataVector[i].mStats[theStatNum] += (ulong)((long)theNumber);
					}
				}
			}
		}

		private void SetStat(Stats theStatNum, int theNumber)
		{
			this.mGameStats[(int)theStatNum] = theNumber;
		}

		private int GetStat(Stats theStatNum)
		{
			return this.mGameStats[(int)theStatNum];
		}

		public bool TriggerSpecial(PieceBejLive thePiece, PieceBejLive theSrc)
		{
			return this.TriggerSpecial(thePiece, theSrc, -2);
		}

		public bool TriggerSpecial(PieceBejLive thePiece, PieceBejLive theSrc, int aColor)
		{
			if (thePiece.mDestructing)
			{
				return false;
			}
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
			{
				this.chainReactorSpecialsTriggered++;
				thePiece.mExplodeDelay = 1;
				return true;
			}
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM) && this.FindStormIdxFor(thePiece) == -1)
			{
				this.chainReactorSpecialsTriggered++;
				if (aColor == -2)
				{
					if (theSrc != null)
					{
						if (theSrc.mColor == -1)
						{
							aColor = theSrc.mLastColor;
						}
						else
						{
							aColor = theSrc.mColor;
						}
					}
					else if (thePiece.mLastColor != -1)
					{
						aColor = thePiece.mLastColor;
					}
					else
					{
						aColor = this.mRand.Next() % 7;
					}
				}
				this.DoHypergem(thePiece, aColor);
				return true;
			}
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER) && this.FindStormIdxFor(thePiece) == -1)
			{
				this.chainReactorSpecialsTriggered++;
				thePiece.mDestructing = true;
				this.AddToStat(Stats.STAT_LASERGEMS_USED, 1, thePiece.mMoveCreditId);
				this.DoStarGem(thePiece);
				return true;
			}
			if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
			{
				thePiece.mDestructing = true;
				thePiece.mExplodeDelay = 1;
				return true;
			}
			return false;
		}

		private void DoStarGem(PieceBejLive thePiece)
		{
			this.starGemsActivated++;
			LightningStorm newLightningStorm = LightningStorm.GetNewLightningStorm(this, thePiece, LightningStorm.StormType.STORM_BOTH);
			this.mLightningStorms.Add(newLightningStorm);
			this.mApp.LockOrientation(true);
			thePiece.mShrinking = true;
			ReportAchievement.ReportSuperNova(this.starGemsActivated);
		}

		private void TallyPiece(PieceBejLive thePiece)
		{
			if (!thePiece.mTallied)
			{
				this.AddToStat(Stats.STAT_GEMS_CLEARED, 1, thePiece.mMoveCreditId);
				thePiece.mTallied = true;
				if (thePiece.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
				{
					this.IncPointMult(thePiece);
				}
			}
		}

		private void Flamify(PieceBejLive thePiece)
		{
			if (thePiece.mColor == -1)
			{
				return;
			}
			this.RemoveGemsFromBlitzBatch(thePiece);
			this.RemoveGemsFromMultiFontBatch(thePiece);
			this.mApp.PlaySample(Resources.SOUND_MULTISHOT);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_COUNTER, false);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_BOMB, false);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_REALTIME_BOMB, false);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_SKULL, false);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_FLAME);
			thePiece.mImmunityCount = (int)Constants.mConstants.M(25f);
			thePiece.mShrinking = false;
			thePiece.mShrinkSize = 0;
			thePiece.mScale.SetConstant(1.0);
			this.mBlitzGemsRenderer.Add(thePiece);
			this.mNumPowerGemsCreated++;
			this.CreateEmittersForSpecialPiece(thePiece);
		}

		private void Laserify(PieceBejLive thePiece)
		{
			if (thePiece.mColor == -1)
			{
				return;
			}
			this.RemoveGemsFromBlitzBatch(thePiece);
			this.RemoveGemsFromMultiFontBatch(thePiece);
			this.mApp.PlaySample(Resources.SOUND_LIGHTNING_MAKE);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_COUNTER, false);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_BOMB, false);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_REALTIME_BOMB, false);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_SKULL, false);
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_LASER);
			thePiece.mImmunityCount = (int)Constants.mConstants.M(25f);
			thePiece.mShrinking = false;
			thePiece.mScale.SetConstant(1.0);
			this.mBlitzGemsRenderer.Add(thePiece);
			this.mNumLaserGemsCreated++;
			this.CreateEmittersForSpecialPiece(thePiece);
			this.miLightningGemCount++;
			if (this.miLightningGemCount >= 4)
			{
				this.miBufferCheckCount = 10;
			}
		}

		private void Hypergemify(PieceBejLive thePiece)
		{
			this.RemoveGemsFromBlitzBatch(thePiece);
			this.RemoveGemsFromMultiFontBatch(thePiece);
			this.mApp.PlaySample(Resources.SOUND_HYPERGEM_CREATE);
			thePiece.ClearFlags();
			thePiece.SetFlag(PIECEFLAG.PIECEFLAG_HYPERGEM);
			thePiece.mChangedTick = this.mUpdateCnt;
			thePiece.mLastColor = thePiece.mColor;
			thePiece.mColor = -1;
			thePiece.mImmunityCount = (int)Constants.mConstants.M(25f);
			thePiece.mShrinking = false;
			thePiece.ClearFlag(PIECEFLAG.PIECEFLAG_FLAME);
			thePiece.mScale.SetConstant(1.0);
			this.AddToStat(Stats.STAT_HYPERGEMS_MADE, 1, thePiece.mMoveCreditId);
			this.mBlitzGemsRenderer.Add(thePiece);
			this.mNumHyperGemsCreated++;
			this.CreateEmittersForSpecialPiece(thePiece);
		}

		private bool ComboProcess(int theColor)
		{
			if (!this.WantColorCombos())
			{
				return false;
			}
			if (this.mComboColors[this.mComboCount] == theColor)
			{
				this.mComboCount++;
				if (this.mComboCount == this.mComboLen)
				{
					this.ComboCompleted();
				}
				return true;
			}
			if (this.mComboCount > 0 && this.mComboColors[this.mComboCount - 1] == theColor && this.mComboCount == this.mLastComboCount)
			{
				this.mLastComboCount = this.mComboCount - 1;
			}
			return false;
		}

		private void ComboCompleted()
		{
			if (!this.WantColorCombos())
			{
				return;
			}
			this.mComboCount = 0;
			this.mComboFlashPct.SetCurve(CurvedValDefinition.mComboFlashPctCurve);
		}

		private void IncPointMult(PieceBejLive thePieceFrom)
		{
			int num = this.mPointMultiplier + 1;
			if (!this.mTimeExpired)
			{
				this.mPointMultiplier = ((this.mPointMultiplier + 1 >= this.MAX_MULTIPLIER) ? this.MAX_MULTIPLIER : (this.mPointMultiplier + 1));
				this.mPrevMultiGemColor = thePieceFrom.mColor;
			}
			thePieceFrom.ClearFlag(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER);
			this.AddPoints((int)thePieceFrom.CX(), (int)thePieceFrom.CY(), 1000 * num, new SexyColor(BoardBejLive.gGemColors[thePieceFrom.mColor]), (uint)thePieceFrom.mMatchId, false, false, -1);
			if (this.miMultiPlayTick == 0)
			{
				if (!this.mDidTimeUp)
				{
					this.mApp.PlaySample(Resources.SOUND_MULTIPLIER_TRIGGER);
				}
				else
				{
					this.mApp.PlaySample(Resources.SOUND_MULTISHOT);
				}
				this.miMultiPlayTick = 30;
			}
		}

		private PointsBej3 AddPoints(int theX, int theY, int thePoints, SexyColor theColor, uint theId, bool addtotube, bool usePointMultiplier)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, -1);
		}

		private PointsBej3 AddPoints(int theX, int theY, int thePoints, SexyColor theColor, uint theId, bool addtotube)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, true, -1);
		}

		private PointsBej3 AddPoints(int theX, int theY, int thePoints, SexyColor theColor, uint theId)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, true, true, -1);
		}

		private PointsBej3 AddPoints(int theX, int theY, int thePoints, SexyColor theColor)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, uint.MaxValue, true, true, -1);
		}

		private PointsBej3 AddPoints(int theX, int theY, int thePoints, SexyColor theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId)
		{
			if (thePoints <= 0)
			{
				return null;
			}
			float num = (float)this.mPointMultiplier * this.GetModePointMultiplier();
			if (!usePointMultiplier)
			{
				num = 1f;
			}
			PointsBej3 pointsBej = null;
			int num2 = thePoints;
			thePoints *= (int)num;
			if (theX >= 0 && theY >= 0)
			{
				float num3 = Constants.mConstants.M(50f);
				float num4 = Constants.mConstants.M(1000f);
				float num5 = Constants.mConstants.M(0.6f);
				float num6 = Constants.mConstants.M(1f);
				float num7 = Constants.mConstants.M(1f);
				int num8 = thePoints;
				int num9 = num2;
				int num10 = (int)((double)num9 * Math.Pow((double)this.mPointMultiplier, 0.44999998807907104));
				if (theId != 4294967295U)
				{
					for (int i = 0; i < this.mPointsManager.mPointsList.Count; i++)
					{
						PointsBej3 pointsBej2 = this.mPointsManager.mPointsList[i];
						if (pointsBej2.mId == theId)
						{
							pointsBej2.mState = 0;
							pointsBej2.mAlpha = 1f;
							pointsBej2.mValue += (uint)thePoints;
							pointsBej2.mDestScale = Math.Min(num6, pointsBej2.mDestScale + 0.05f);
							pointsBej2.mTimer = num7;
							pointsBej = pointsBej2;
							num8 = (int)pointsBej2.mValue;
							num10 += pointsBej2.mCorrectedPoints;
							num9 += pointsBej2.mScalePoints;
							break;
						}
					}
				}
				float num11 = num6 - num5;
				float num12 = Math.Max(0f, Math.Min(1f, ((float)num9 - num3) / num4));
				float num13 = Math.Max(0f, Math.Min(1f, ((float)num10 - num3) / num4));
				float mDestScale = num5 + Math.Min(1f, num12 * 2f) * num11;
				theY = Math.Max(theY, 120);
				string text;
				if (!BoardBejLive.numberStringCache.TryGetValue(num8, ref text))
				{
					text = num8.ToString();
					BoardBejLive.numberStringCache.Add(num8, text);
				}
				if (pointsBej == null && Resources.FONT_TEXT != null)
				{
					int theAnim = -1;
					pointsBej = PointsBej3.GetNewPointsBej3(this.mApp, Resources.FONT_FLOAT_POINTS, text, theX, theY, num7, 0, theColor, theAnim);
					pointsBej.mMoveCreditId = theMoveCreditId;
					pointsBej.mId = theId;
					pointsBej.mDestScale = mDestScale;
					pointsBej.mScaleDifMult = Constants.mConstants.M(0.15f);
					pointsBej.mScaleDampening = Constants.mConstants.M(0.46f) + (float)num9 * Constants.mConstants.M(0.0015f);
					if (pointsBej.mScaleDampening > Constants.mConstants.M(0.962f))
					{
						pointsBej.mScaleDampening = Constants.mConstants.M(0.962f);
					}
					pointsBej.mValue = (uint)thePoints;
					this.mPointsManager.Add(pointsBej);
				}
				else if (pointsBej != null)
				{
					pointsBej.mString = text;
					pointsBej.RestartWobble();
					if (!pointsBej.mDrawn)
					{
						pointsBej.mX = ((float)theX + pointsBej.mX) / 2f;
						pointsBej.mY = ((float)theY + pointsBej.mY) / 2f;
					}
				}
				pointsBej.mColor.CopyFrom(theColor);
				pointsBej.mColorCycle[0].mCycleColors.Clear();
				pointsBej.mColorCycle[1].mCycleColors.Clear();
				for (int j = 0; j < 6; j++)
				{
					for (int k = 0; k < 3; k++)
					{
						float num14 = 0f;
						float num15 = 0f;
						float num16 = 0f;
						if (k == 0)
						{
							num16 = Math.Min(1f, Math.Max(0f, (num13 - 0.3f) * 2f));
							float num17 = Math.Max(0f, num13 - 0.5f);
							num14 = ((j % 2 == 0) ? 0.5f : (0.5f + num17 * Constants.mConstants.M(1f)));
							num15 = 1f;
						}
						else if (k == 1)
						{
							num16 = Math.Min(1f, Math.Max(0f, (num13 - 0.3f) * 2f));
							float num18 = Math.Max(0f, num13 - 0.1f);
							num14 = ((j % 2 == 0) ? 0.5f : (0.5f + num18 * Constants.mConstants.M(1f)));
							num15 = Math.Max(0f, (num13 - 0.5f) * 3f);
						}
						else if (k == 2)
						{
							num16 = Math.Min(1f, Math.Max(0f, (num13 - 1f) * 1f));
							num14 = 1f;
							num15 = 0.7f;
						}
						SexyColor sexyColor = default(SexyColor);
						sexyColor.mRed = (int)((float)GlobalMembersColorCycle.gCycleColors[j].mRed * num16 + Math.Min(255f, (float)theColor.mRed * num14 * (1f - num16)));
						sexyColor.mGreen = (int)((float)GlobalMembersColorCycle.gCycleColors[j].mGreen * num16 + Math.Min(255f, (float)theColor.mGreen * num14 * (1f - num16)));
						sexyColor.mBlue = (int)((float)GlobalMembersColorCycle.gCycleColors[j].mBlue * num16 + Math.Min(255f, (float)theColor.mBlue * num14 * (1f - num16)));
						sexyColor.mAlpha = (int)(num15 * 255f);
						pointsBej.mColorCycle[k].mCycleColors.Add(sexyColor);
					}
				}
				pointsBej.mColorCycle[0].SetPosition(0.25f);
				pointsBej.mColorCycle[1].SetPosition(0.75f);
				pointsBej.mColorCycling = true;
				pointsBej.mCorrectedPoints = num10;
				pointsBej.mScalePoints = num9;
				pointsBej.mWobbleScale = num12 * Constants.mConstants.M(0.7f);
				pointsBej.mTimer = 0.6f + num12 * 1.6f;
				if (pointsBej != null)
				{
					pointsBej.mScale = pointsBej.mDestScale * Constants.mConstants.M(0.1f);
				}
			}
			int l = (int)((float)thePoints / this.GetModePointMultiplier());
			while (l > 0)
			{
				int num19 = Math.Min(l, 10);
				double num20 = (double)Constants.mConstants.M(0.8f);
				int num21 = (int)((float)this.GetMoveStat(theMoveCreditId, Stats.STAT_POINTS) / this.GetModePointMultiplier());
				double num22 = Math.Pow((double)(num21 + num19), num20) - Math.Pow((double)num21, num20);
				num22 *= (double)Constants.mConstants.M(3f);
				if (addtotube)
				{
					this.mLevelPointsTotal += (int)num22;
				}
				l -= num19;
				this.AddToStat(Stats.STAT_POINTS, (int)((float)num19 * this.GetModePointMultiplier()), theMoveCreditId);
			}
			this.mMovePoints += thePoints;
			if (!this.mInBonus && this.mHighestScoringMove < this.mMovePoints)
			{
				this.mHighestScoringMove = this.mMovePoints;
			}
			this.mPoints += thePoints + this.GetSpeedPoints();
			this.mPointsWithoutSpeedBonus += thePoints;
			if (this.mTimedMode)
			{
				this.highFlyerScore += thePoints + this.GetSpeedPoints();
			}
			return pointsBej;
		}

		private int FindStormIdxFor(PieceBejLive thePiece)
		{
			for (int i = 0; i < this.mLightningStorms.Count; i++)
			{
				LightningStorm lightningStorm = this.mLightningStorms[i];
				if (lightningStorm.mElectrocuterId == thePiece.mId)
				{
					return i;
				}
			}
			return -1;
		}

		private bool MatchThree(PieceBejLive thePiece)
		{
			return false;
		}

		private int GetMoveStat(int theMoveCreditId, Stats theStatNum)
		{
			return this.GetMoveStat(theMoveCreditId, (int)theStatNum);
		}

		private int GetMoveStat(int theMoveCreditId, int theStatNum)
		{
			return this.GetMoveStat(theMoveCreditId, theStatNum, 0);
		}

		private int GetMoveStat(int theMoveCreditId, int theStatNum, int theDefault)
		{
			for (int i = 0; i < this.mMoveDataVector.Count; i++)
			{
				if (this.mMoveDataVector[i].mMoveCreditId == theMoveCreditId)
				{
					return (int)this.mMoveDataVector[i].mStats[theStatNum];
				}
			}
			return theDefault;
		}

		private void MaxStat(int theStatNum, int theNumber)
		{
			this.MaxStat(theStatNum, theNumber, -1);
		}

		private void MaxStat(Stats theStatNum, int theNumber, int theMoveCreditId)
		{
			this.MaxStat((int)theStatNum, theNumber, theMoveCreditId);
		}

		private void MaxStat(int theStatNum, int theNumber, int theMoveCreditId)
		{
			this.mGameStats[theStatNum] = Math.Max(this.mGameStats[theStatNum], theNumber);
			if (theMoveCreditId != -1)
			{
				for (int i = 0; i < this.mMoveDataVector.Count; i++)
				{
					if (this.mMoveDataVector[i].mMoveCreditId == theMoveCreditId)
					{
						this.mMoveDataVector[i].mStats[theStatNum] = Math.Max(this.mMoveDataVector[i].mStats[theStatNum], (ulong)((long)theNumber));
					}
				}
			}
		}

		private void ProcessMatches(List<MatchSet> theMatches)
		{
			this.ProcessMatches(theMatches, false);
		}

		private void ProcessMatches(List<MatchSet> theMatches, bool fromUpdateSwapping)
		{
		}

		public void SetMoveCredit(PieceBejLive thePiece, int theMoveCreditId)
		{
			thePiece.mMoveCreditId = Math.Max(thePiece.mMoveCreditId, theMoveCreditId);
		}

		private void DoHypergem(PieceBejLive thePiece, int theColor)
		{
			this.AddToStat(Stats.STAT_HYPERGEMS_USED, 1, thePiece.mMoveCreditId);
			this.mChainCount++;
			ReportAchievement.ReportUltimateCascade(this.mChainCount);
			if (this.lastColourMatch == theColor)
			{
				ReportAchievement.ReportSpectrum(this.monocolouristCount);
			}
			else
			{
				this.monocolouristCount = 0;
				this.lastColourMatch = theColor;
			}
			this.ComboProcess(theColor);
			thePiece.mDestructing = true;
			LightningStorm newLightningStorm = LightningStorm.GetNewLightningStorm(this, thePiece, LightningStorm.StormType.STORM_HYPERGEM);
			newLightningStorm.mColor = theColor;
			this.mLightningStorms.Add(newLightningStorm);
			this.mApp.LockOrientation(true);
			if (theColor == -1)
			{
				ReportAchievement.ReportSingularity();
			}
		}

		private bool NormalTrySwap(PieceBejLive theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			if (theSelected == null)
			{
				return false;
			}
			if (theSwappedRow < 0 || theSwappedRow >= 8 || theSwappedCol < 0 || theSwappedCol >= 8)
			{
				return false;
			}
			if (!this.IsSwapLegal(theSelected, theSwappedRow, theSwappedCol))
			{
				return false;
			}
			PieceBejLive pieceAtRowCol = this.GetPieceAtRowCol(theSwappedRow, theSwappedCol);
			if (pieceAtRowCol == null)
			{
				return false;
			}
			if (playerSwapped)
			{
				this.mLastPlayerSwapColor = theSelected.mColor;
			}
			this.ResetPerMoveAchievementCounters();
			if (playerSwapped)
			{
				theSelected.mSelected = false;
				theSelected.mSelectorAlpha.SetCurve(CurvedValDefinition.theSelectedmSelectorAlphaCurve);
			}
			MoveData newMoveData = MoveData.GetNewMoveData();
			newMoveData.mUpdateCnt = this.mUpdateCnt;
			newMoveData.mSelectedId = theSelected.mId;
			newMoveData.mSwappedRow = theSwappedRow;
			newMoveData.mSwappedCol = theSwappedCol;
			newMoveData.mMoveCreditId = this.mCurMoveCreditId++;
			for (int i = 0; i < 24; i++)
			{
				newMoveData.mStats[i] = 0UL;
			}
			this.mMoveDataVector.Add(newMoveData);
			theSelected.mMoveCreditId = newMoveData.mMoveCreditId;
			if (pieceAtRowCol != null)
			{
				pieceAtRowCol.mMoveCreditId = newMoveData.mMoveCreditId;
			}
			theSelected.mLastActiveTick = this.mUpdateCnt;
			if (pieceAtRowCol != null)
			{
				pieceAtRowCol.mLastActiveTick = this.mUpdateCnt - 1;
			}
			SwapData newSwapData = SwapData.GetNewSwapData();
			newSwapData.mPiece1 = theSelected;
			newSwapData.mPiece2 = pieceAtRowCol;
			newSwapData.mSwapDir.x = theSwappedCol - theSelected.mCol;
			newSwapData.mSwapDir.y = theSwappedRow - theSelected.mRow;
			newSwapData.mSwapPct.SetCurve(CurvedValDefinition.aSwapDatamSwapPctCurve);
			newSwapData.mGemScale.SetCurve(CurvedValDefinition.aSwapDatamScaleCurve);
			newSwapData.mSwapPct.mIncRate *= (double)this.GetSwapSpeed();
			newSwapData.mGemScale.mIncRate *= (double)this.GetSwapSpeed();
			newSwapData.mForwardSwap = true;
			newSwapData.mIgnore = false;
			newSwapData.mForceSwap = forceSwap;
			this.mSwapDataVector.Add(newSwapData);
			this.mDoHintPenalty = true;
			this.ResetChainCount();
			return true;
		}

		private bool BlitzTrySwap(PieceBejLive theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			if (theSelected.IsFlagSet(PIECEFLAG.PIECEFLAG_DETONATOR))
			{
				int num = 0;
				for (int i = 0; i < 8; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						PieceBejLive pieceBejLive = this.mBoard[i, j];
						if (pieceBejLive != null && (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME) || pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM) || pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER) || pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER)))
						{
							pieceBejLive.mExplodeDelay = (int)(Constants.mConstants.M(1f) + (float)num * Constants.mConstants.M(25f));
							num++;
						}
					}
				}
				if (num > 0)
				{
					this.DeletePiece(theSelected);
				}
				else
				{
					this.mApp.PlaySample(Resources.SOUND_BAD);
					this.AddToStat(Stats.STAT_NUM_MOVES, 1);
				}
				return true;
			}
			bool flag = this.NormalTrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped);
			if (flag)
			{
				this.mFrozen = false;
				if (this.mCountUnder > 0)
				{
					this.mNumLastMoves++;
				}
			}
			return flag;
		}

		private bool QueueSwap(PieceBejLive theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap)
		{
			return this.QueueSwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, true);
		}

		private bool QueueSwap(PieceBejLive theSelected, int theSwappedRow, int theSwappedCol)
		{
			return this.QueueSwap(theSelected, theSwappedRow, theSwappedCol, false, true);
		}

		private bool QueueSwap(PieceBejLive theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			if (!this.IsSwapLegal(theSelected, theSwappedRow, theSwappedCol))
			{
				if (!this.mInTutorialMode && this.mApp.mProfile.WantsHint(Profile.Hint.HINT_BAD_MOVE))
				{
					int num = this.mSelectedPiece.mRow - theSelected.mRow;
					int num2 = this.mSelectedPiece.mCol - theSelected.mCol;
					int num3 = Math.Max(Math.Abs(num), Math.Abs(num2));
					if (num3 > 1)
					{
						this.alert = new Alert(Dialogs.DIALOG_INSTRUCTION, this, this.mApp);
						this.alert.SetBodyText(Strings.Board_Help_Message_Far_Away_Swap);
						this.alert.AddButton(Dialogoid.ButtonID.OK_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_GREEN, Strings.OK);
						this.alert.Present();
						if (this.mLevel > 0)
						{
							this.mApp.mProfile.DisableHint(Profile.Hint.HINT_BAD_MOVE);
						}
					}
					this.mSelectedPiece = null;
				}
				return false;
			}
			QueuedMove newQueuedMove = QueuedMove.GetNewQueuedMove();
			newQueuedMove.mUpdateCnt = this.mUpdateCnt;
			newQueuedMove.mSelectedId = theSelected.mId;
			newQueuedMove.mSwappedRow = theSwappedRow;
			newQueuedMove.mSwappedCol = theSwappedCol;
			newQueuedMove.mForceSwap = forceSwap;
			newQueuedMove.mPlayerSwapped = playerSwapped;
			this.mQueuedMoveVector.Add(newQueuedMove);
			return true;
		}

		private bool IsSwapLegal(PieceBejLive theSelected, int theSwappedRow, int theSwappedCol)
		{
			if (this.mLightningStorms.Count != 0 || this.mEditing)
			{
				return false;
			}
			PieceBejLive pieceAtRowCol = this.GetPieceAtRowCol(theSwappedRow, theSwappedCol);
			if (!this.IsPieceStill(theSelected) || theSelected.IsFlagSet(PIECEFLAG.PIECEFLAG_DOOM))
			{
				return false;
			}
			if (pieceAtRowCol != null && (!this.IsPieceStill(pieceAtRowCol) || pieceAtRowCol.IsFlagSet(PIECEFLAG.PIECEFLAG_DOOM)))
			{
				return false;
			}
			int num = theSwappedCol - theSelected.mCol;
			int num2 = theSwappedRow - theSelected.mRow;
			return (!theSelected.IsButton() || Math.Abs(num) + Math.Abs(num2) == 0) && (theSelected.IsButton() || Math.Abs(num) + Math.Abs(num2) == 1);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.SetVisible(true);
			this.FadeOut(0f, 0.5f);
			base.TransactionTimeSeconds(0.5f);
		}

		private void MatchMade(SwapData theSwapData)
		{
			if (this.mTimedMode)
			{
				double num = Math.Min(this.mApp.mGraceSeconds + (double)(this.mMovePoints / this.mPointMultiplier) * this.mApp.mGraceSecondsMult, this.mApp.mGraceSecondsMax);
				this.mTimerDelay += (int)(100.0 * num);
			}
			uint num2 = (uint)(this.mIdleTicks - this.mLastMatchTick);
			this.monocolouristCount++;
			ReportAchievement.ReportSpectrum(this.monocolouristCount);
			this.dynamicBoardCounter++;
			if (!this.AllowSpeedBonus())
			{
				return;
			}
			this.mWantHintTicks = 0;
			this.mMatchTallyCount++;
			if (this.mSpeedBonusCount >= 9 && this.mSpeedBonusFlameModePct == 0f)
			{
				float num3 = Math.Max(0f, Math.Min(1.5f, 1f - (num2 - Constants.mConstants.SPEED_TIME_RIGHT) / (Constants.mConstants.SPEED_TIME_RIGHT - Constants.mConstants.SPEED_TIME_LEFT)));
				float num4 = Math.Abs((float)((double)num3 - this.mSpeedBonusNum));
				if (num4 > 0f && this.mSpeedBonusFlameModePct <= 0f)
				{
					this.mSpeedBonusNum = Math.Min(1.0, this.mSpeedBonusNum + (double)Math.Min(0.1f, num4 * Constants.mConstants.M(0.075f)));
					if (this.mSpeedBonusNum >= 1.0)
					{
						this.DoSpeedText(0);
					}
				}
			}
			if ((this.mTimedMode || this.mBlitzMode) && (this.mSpeedBonusCount > 0 || (this.mLastMatchTime >= 0 && (ulong)num2 + (ulong)((long)this.mLastMatchTime) <= 300UL)))
			{
				if (this.mSpeedBonusCount == 0)
				{
					this.mSpeedBonusDisp.SetCurve(CurvedValDefinition.mSpeedBonusDispCurve3);
				}
				this.mSpeedBonusCount++;
				this.mSpeedBonusPointsGlow.SetCurve(CurvedValDefinition.mSpeedBonusPointsGlowCurve);
				this.mSpeedBonusPointsScale.SetCurve(CurvedValDefinition.mSpeedBonusPointsScaleCurve2);
				for (int i = 0; i < this.mPointsManager.mPointsList.Count; i++)
				{
					PointsBej3 pointsBej = this.mPointsManager.mPointsList[i];
					if (pointsBej.mUpdateCnt == 0)
					{
						int num5 = Math.Min(200, (this.mSpeedBonusCount + 1) * 20);
						this.mSpeedBonusPoints += (int)((float)(num5 * this.mPointMultiplier) * this.GetModePointMultiplier());
						this.AddPoints((int)pointsBej.mX, (int)pointsBej.mY, num5, new SexyColor(pointsBej.mColor), pointsBej.mId, false, true, pointsBej.mMoveCreditId);
						pointsBej.mUpdateCnt++;
						break;
					}
				}
			}
			this.mLastMatchTime = (int)num2;
			this.mLastMatchTick = this.mIdleTicks;
		}

		private void DecrementAllDoomGems(bool immediate)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null && pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_DOOM))
					{
						this.DecrementCounterGem(pieceBejLive, immediate);
					}
				}
			}
		}

		private bool DecrementAllCounterGems(bool immediate)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null && !pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_REALTIME_BOMB) && !pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_DOOM))
					{
						this.DecrementCounterGem(pieceBejLive, immediate);
					}
				}
			}
			return false;
		}

		private void ComboFailed()
		{
			if (!this.WantColorCombos())
			{
				return;
			}
			this.mComboCount = Math.Max(0, this.mComboCount - 1);
		}

		private void DoSpeedText(int anIndex)
		{
			this.blazingSpeedCountThisLevel++;
			if (this.blazingSpeedCountThisLevel >= 2)
			{
				ReportAchievement.ReportFasterThanLight();
			}
			ReportAchievement.ReportBurnNotice();
			this.mSpeedBonusFlameModePct = 1f;
			this.mApp.PlaySample(Resources.SOUND_BURNING_SPEED);
			this.mApp.PlaySample(Resources.SOUND_BLAZING_SPEED);
		}

		private void ClearBoost(Boost theBoostNum)
		{
			if (!this.HasBoost(theBoostNum))
			{
				return;
			}
			this.mBoostsUsed |= 1 << (int)theBoostNum;
		}

		private void ShowHint()
		{
			if (this.mDidTimeUp)
			{
				return;
			}
			this.noHintsAchievementFlag = false;
			if (this.mDoHintPenalty)
			{
				this.mPoints -= 50 * this.mPointMultiplier;
				if (this.mPoints < 0)
				{
					this.mPoints = 0;
				}
				this.mPointsWithoutSpeedBonus -= 50 * this.mPointMultiplier;
				if (this.mPointsWithoutSpeedBonus < 0)
				{
					this.mPointsWithoutSpeedBonus = 0;
				}
				this.mDoHintPenalty = false;
			}
			this.mWantHintTicks = 0;
			if (this.FindMove(this.findMoveCoords, 0, true, true))
			{
				PieceBejLive pieceBejLive = this.mBoard[this.findMoveCoords[3], this.findMoveCoords[2]];
				PieceBejLive pieceBejLive2 = this.mBoard[this.findMoveCoords[1], this.findMoveCoords[0]];
				if (pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
				{
					pieceBejLive = pieceBejLive2;
				}
				pieceBejLive.mSparkleLife = 290;
				pieceBejLive.mSparkleFrame = 0;
				pieceBejLive.mSparklePairOfsCol = 0;
				pieceBejLive.mSparklePairOfsRow = 0;
				pieceBejLive.mSpinFrame = 1;
				if (this.mApp.CheatsEnabled())
				{
					PieceBejLive pieceAtRowCol = this.GetPieceAtRowCol(this.findMoveCoords[3], this.findMoveCoords[2]);
					this.QueueSwap(pieceAtRowCol, this.findMoveCoords[1], this.findMoveCoords[0], true, true);
				}
			}
			this.mNoMoveCount = 0;
			if (!this.mbAutoHint)
			{
				this.miHintCoolDownCount = 400;
			}
		}

		private void ExplodeAtHelper(int theX, int theY)
		{
			int num = theX;
			int num2 = theY;
			PieceBejLive pieceAt = this.GetPieceAt(theX, theY);
			if (pieceAt != null)
			{
				num = (int)(pieceAt.mX + (float)(GameConstants.GEM_WIDTH / 2));
				num2 = (int)(pieceAt.mY + (float)(GameConstants.GEM_HEIGHT / 2));
			}
			BoardBejLive.gExplodePoints[BoardBejLive.gExplodeCount, 0] = num;
			BoardBejLive.gExplodePoints[BoardBejLive.gExplodeCount, 1] = num2;
			BoardBejLive.gExplodeCount++;
			int num3 = (this.HasLargeExplosions() ? 13 : 9);
			int[,] array = ((num3 == 9) ? this.aSmCheckPositions : this.aLgCheckPositions);
			for (int i = 0; i < num3; i++)
			{
				int num4 = theX + array[i, 0] * GameConstants.GEM_WIDTH;
				int num5 = theY + array[i, 1] * GameConstants.GEM_HEIGHT;
				PieceBejLive pieceAt2 = this.GetPieceAt(num4, num5);
				if (pieceAt2 != null && (pieceAt2.mExplodeDelay == 0 || i == 8))
				{
					int num6 = (int)(pieceAt2.mX + (float)(GameConstants.GEM_WIDTH / 2));
					int num7 = (int)(pieceAt2.mY + (float)(GameConstants.GEM_HEIGHT / 2));
					bool flag = false;
					int num8 = (int)(Constants.mConstants.M(0.013f) * (float)(num4 - theX));
					int num9 = (int)(Constants.mConstants.M(0.013f) * (float)(num5 - theY));
					if (array[i, 0] != 0 || array[i, 1] != 0)
					{
						int num10 = 0;
						while ((float)num10 < Constants.mConstants.M(5f))
						{
							EffectBej3 effectBej = this.mPostFXManager.AllocEffect(EffectBej3.EffectType.TYPE_GEM_SHARD);
							if (pieceAt2.mColor < 0)
							{
								effectBej.mColor = BoardBejLive.gAllGemColors[0];
							}
							else
							{
								effectBej.mColor = BoardBejLive.gGemColors[pieceAt2.mColor];
							}
							int num11 = num10 * (int)((double)(num10 + 60) / 60.0);
							double num12 = (double)num10 * 0.503 + (double)(Board.Rand() % 100) / 800.0;
							float num13 = Constants.mConstants.M(0.6f) + Math.Abs(Board.GetRandFloat()) * Constants.mConstants.M(0.6f);
							int num14 = Math.Abs((int)(num12 * 4096.0 / 6.28318) % 4096);
							effectBej.mDX = Board.COS_TAB[num14] * num13 + (float)num8;
							effectBej.mDY = Board.SIN_TAB[num14] * num13 + Constants.mConstants.MS(-2f) + (float)num9;
							if (num8 != 0 || num9 != 0)
							{
								num12 = (double)Board.GetRandFloat() * 3.14159;
								num11 = (int)(Board.GetRandFloat() * Constants.mConstants.M(24f));
								num14 = Math.Abs((int)(num12 * 4096.0 / 6.28318) % 4096);
								effectBej.mX = (float)(num6 + (int)(Constants.mConstants.M(1f) * (float)num11 * Board.COS_TAB[num14]));
								effectBej.mY = (float)(num7 + (int)(Constants.mConstants.M(1f) * (float)num11 * Board.SIN_TAB[num14]));
								num12 = Math.Atan2((double)(effectBej.mY - (float)num2), (double)(effectBej.mX - (float)num)) + (double)(Board.GetRandFloat() * Constants.mConstants.M(0.3f));
								num13 = Constants.mConstants.M(3.5f) + Math.Abs(Board.GetRandFloat()) * Constants.mConstants.M(1f);
								num14 = Math.Abs((int)(num12 * 4096.0 / 6.28318) % 4096);
								effectBej.mDX = Board.COS_TAB[num14] * num13;
								effectBej.mDY = Board.SIN_TAB[num14] * num13 + Constants.mConstants.M(-2f);
								effectBej.mDecel = Constants.mConstants.M(0.98f) + Board.GetRandFloat() * Constants.mConstants.M(0.005f);
							}
							else
							{
								effectBej.mX = (float)(num6 + (int)(Constants.mConstants.M(1.2f) * (float)num11 * effectBej.mDX)) + Constants.mConstants.M(14f);
								effectBej.mY = (float)(num7 + (int)(Constants.mConstants.M(1.2f) * (float)num11 * effectBej.mDY)) + Constants.mConstants.M(10f);
							}
							effectBej.mAngle = (float)num12;
							effectBej.mDAngle = Constants.mConstants.M(0.05f) * Board.GetRandFloat();
							effectBej.mGravity = Constants.mConstants.M(0.06f);
							effectBej.mValue[0] = Board.GetRandFloat() * 6.28318548f;
							effectBej.mValue[1] = Board.GetRandFloat() * 6.28318548f;
							effectBej.mValue[2] = Constants.mConstants.M(0.045f) * (Constants.mConstants.M(3f) * Math.Abs(Board.GetRandFloat()) + Constants.mConstants.M(1f));
							effectBej.mValue[3] = Constants.mConstants.M(0.045f) * (Constants.mConstants.M(3f) * Math.Abs(Board.GetRandFloat()) + Constants.mConstants.M(1f));
							effectBej.mDAlpha = Constants.mConstants.M(-0.0025f) * (Constants.mConstants.M(2f) * Math.Abs(Board.GetRandFloat()) + Constants.mConstants.M(4f));
							this.mPostFXManager.AddEffect(effectBej);
							num10++;
						}
						if (pieceAt2.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
						{
							flag = true;
						}
					}
					if (pieceAt2.mImmunityCount == 0)
					{
						if (pieceAt != null)
						{
							this.SetMoveCredit(pieceAt2, pieceAt.mMoveCreditId);
							if (pieceAt.mMatchId == -1)
							{
								pieceAt.mMatchId = this.mCurMoveCreditId++;
							}
							pieceAt2.mMatchId = pieceAt.mMatchId;
						}
						if (flag)
						{
							pieceAt2.mExplodeDelay = (int)Constants.mConstants.M(15f);
						}
						else if (pieceAt2.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
						{
							pieceAt2.mExplodeDelay = (int)Constants.mConstants.M(5f);
						}
						else
						{
							if (pieceAt2.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
							{
								int num15 = this.FindStormIdxFor(pieceAt2);
								if (num15 != -1)
								{
									LightningStorm lightningStorm = this.mLightningStorms[num15];
									if (lightningStorm.mUpdateCnt == 0 && (lightningStorm.mStormType == LightningStorm.StormType.STORM_HORZ || lightningStorm.mStormType == LightningStorm.StormType.STORM_VERT))
									{
										lightningStorm.PrepareForReuse();
										this.mLightningStorms.RemoveAt(num15);
										pieceAt2.mDestructing = false;
										if (this.mLightningStorms.Count == 0)
										{
											this.mApp.LockOrientation(false);
										}
									}
								}
								this.AddPoints((int)pieceAt2.mX, (int)pieceAt2.mY, (int)Constants.mConstants.M(30f), BoardBejLive.gGemColors[pieceAt2.mColor], (uint)pieceAt2.mMatchId, true, true, pieceAt2.mMoveCreditId);
							}
							if ((pieceAt2.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME) || !this.TriggerSpecial(pieceAt2, pieceAt)) && pieceAt2.mCanDestroy)
							{
								pieceAt2.mIsExploding = true;
								if (pieceAt2.mColor < 0)
								{
									this.AddPoints((int)pieceAt2.mX, (int)pieceAt2.mY, (int)Constants.mConstants.M(20f), BoardBejLive.gAllGemColors[0], (uint)pieceAt2.mMatchId, true, true, pieceAt2.mMoveCreditId);
								}
								else
								{
									this.AddPoints((int)pieceAt2.mX, (int)pieceAt2.mY, (int)Constants.mConstants.M(20f), BoardBejLive.gGemColors[pieceAt2.mColor], (uint)pieceAt2.mMatchId, true, true, pieceAt2.mMoveCreditId);
								}
								this.DeletePiece(pieceAt2);
							}
						}
					}
				}
			}
		}

		private void BumpColumns(int theX, int theY, float thePower)
		{
			thePower *= 0.7f;
			for (int i = 0; i < 8; i++)
			{
				float num = 0f;
				float num2 = 0f;
				int j = 7;
				while (j >= -1)
				{
					PieceBejLive pieceAtRowCol = this.GetPieceAtRowCol(j, i);
					bool flag = false;
					float num3;
					float num4;
					if (pieceAtRowCol != null && pieceAtRowCol.mY + (float)(GameConstants.GEM_HEIGHT / 2) < (float)theY)
					{
						num3 = pieceAtRowCol.mX + (float)(GameConstants.GEM_WIDTH / 2) - (float)theX;
						num4 = pieceAtRowCol.mY + (float)(GameConstants.GEM_HEIGHT / 2) - (float)theY;
						flag = true;
						goto IL_A6;
					}
					if (j == -1)
					{
						num3 = (float)(base.GetColX(i) + GameConstants.GEM_WIDTH / 2 - theX);
						num4 = (float)(base.GetRowY(j) + GameConstants.GEM_HEIGHT / 2 - theY);
						goto IL_A6;
					}
					IL_1BD:
					j--;
					continue;
					IL_A6:
					float num5 = 0f;
					if (num4 != 0f || num3 != 0f)
					{
						num5 = (float)Math.Atan2((double)num4, (double)num3);
					}
					float num6 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4)) / (float)GameConstants.GEM_WIDTH;
					int num7 = Math.Abs((int)((double)(num5 * 4096f) / 6.28318) % 4096);
					float num8 = thePower / (Math.Max(0f, num6 - Constants.mConstants.M(1f)) + Constants.mConstants.M(1f)) * Board.SIN_TAB[num7];
					num2 = num8 * Constants.mConstants.MS(-7f);
					if (!flag)
					{
						goto IL_1BD;
					}
					if (num == 0f)
					{
						num = num2;
					}
					pieceAtRowCol.mFallVelocity = Math.Min(pieceAtRowCol.mFallVelocity, num);
					if (this.IsPieceSwapping(pieceAtRowCol))
					{
						for (int k = j; k < 8; k++)
						{
							PieceBejLive pieceBejLive = this.mBoard[k, i];
							if (pieceBejLive != null)
							{
								pieceBejLive.mFallVelocity = Math.Max(0f, pieceBejLive.mFallVelocity);
							}
						}
						goto IL_1BD;
					}
					goto IL_1BD;
				}
				this.mBumpVelocities[i] = Math.Min(num, num2) * 0.3125f;
			}
		}

		private void SwitchOrientation()
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					if (pieceBejLive != null)
					{
						pieceBejLive.mX = (float)base.GetColX(j);
						pieceBejLive.mY = (float)base.GetRowY(i);
					}
				}
			}
			this.ResizeButtons();
		}

		public void RemoveGemsFromBlitzBatch(PieceBejLive pPiece)
		{
			if (this.mBlitzGemsRenderer.Contains(pPiece))
			{
				this.mBlitzGemsRenderer.Remove(pPiece);
			}
			if (pPiece.AssociatedParticleEmitter != null)
			{
				if (this.lowerLayerEmitters.Contains(pPiece.AssociatedParticleEmitter))
				{
					this.lowerLayerEmitters.Remove(pPiece.AssociatedParticleEmitter);
				}
				if (this.upperLayerEmitters.Contains(pPiece.AssociatedParticleEmitter))
				{
					this.upperLayerEmitters.Remove(pPiece.AssociatedParticleEmitter);
				}
				pPiece.AssociatedParticleEmitter.PrepareForReuse();
				pPiece.AssociatedParticleEmitter = null;
			}
		}

		public void RemoveGemsFromMultiFontBatch(PieceBejLive pPiece)
		{
			for (int i = 0; i < this.mMultFontRenderer.Count; i++)
			{
				if (pPiece == this.mMultFontRenderer[i])
				{
					this.mMultFontRenderer.RemoveAt(i);
					return;
				}
			}
		}

		private static int GetGemLightingIndexForColour(int colour)
		{
			switch (colour)
			{
			case 0:
				return 3;
			case 1:
				return 1;
			case 2:
				return 6;
			case 3:
				return 0;
			case 4:
				return 4;
			case 5:
				return 5;
			case 6:
				return 2;
			default:
				return 0;
			}
		}

		private void DrawAdditiveOverlayEffect(Graphics g)
		{
			if (this.mPauseCount > 0)
			{
				this.mGlintingPieces.Clear();
				return;
			}
			g.SetColorizeImages(true);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
			if (this.mSpeedBonusFlameModePct > 0f)
			{
				float num = (float)this.mUpdateCnt * Constants.mConstants.MS(0.15f);
				int num2 = Math.Abs((int)((double)(num * 4096f) / 6.28318) % 4096);
				g.SetColor(new SexyColor((int)Constants.mConstants.M(255f), (int)(Constants.mConstants.M(128f) + Board.SIN_TAB[num2] * Constants.mConstants.M(32f)), (int)(Constants.mConstants.M(64f) + Board.SIN_TAB[num2] * Constants.mConstants.M(16f)), (int)(Constants.mConstants.M(100f) * Math.Min(1f, this.mSpeedBonusFlameModePct * 4f))));
				g.FillRect(this.BoardRect);
				g.SetColor(SexyColor.White);
			}
			if (this.mFlashBarRed)
			{
				g.SetColorizeImages(true);
				g.SetColor(new SexyColor(255, 128, 128));
			}
			if (this.mFlashBarRed)
			{
				if (this.mApp.IsLandscape())
				{
					g.DrawImage(AtlasResources.IMAGE_FRAME_CHIP_WARN_VERT, Constants.mConstants.Board_Board_X - AtlasResources.IMAGE_FRAME_CHIP_WARN_VERT.mWidth + Constants.mConstants.BoardBej2_FrameChipOffset, Constants.mConstants.Board_Board_Y, AtlasResources.IMAGE_FRAME_CHIP_WARN_VERT.mWidth, AtlasResources.IMAGE_GRID.mHeight);
					g.DrawImage(AtlasResources.IMAGE_FRAME_CHIP_WARN_VERT, Constants.mConstants.Board_Board_X + AtlasResources.IMAGE_GRID.mWidth - AtlasResources.IMAGE_FRAME_CHIP_WARN_VERT.mWidth + Constants.mConstants.BoardBej2_FrameChip_Warn_Offset, Constants.mConstants.Board_Board_Y, AtlasResources.IMAGE_FRAME_CHIP_WARN_VERT.mWidth, AtlasResources.IMAGE_GRID.mHeight);
				}
				else
				{
					g.DrawImage(AtlasResources.IMAGE_FRAME_CHIP_WARN_HORIZ, Constants.mConstants.Board_Board_X, Constants.mConstants.Board_Board_Y - Constants.mConstants.BoardBej2_FrameChip_Warn_Offset, AtlasResources.IMAGE_GRID.mWidth, AtlasResources.IMAGE_FRAME_CHIP_WARN_HORIZ.mHeight);
					g.DrawImage(AtlasResources.IMAGE_FRAME_CHIP_WARN_HORIZ, Constants.mConstants.Board_Board_X, Constants.mConstants.Board_Board_Y + AtlasResources.IMAGE_GRID.mHeight - Constants.mConstants.BoardBej2_FrameChip_Warn_Offset2, AtlasResources.IMAGE_GRID.mWidth, AtlasResources.IMAGE_FRAME_CHIP_WARN_HORIZ.mHeight);
				}
			}
			g.SetColorizeImages(true);
			float num3 = this.mGemShowPct * this.mPauseFade;
			foreach (PieceBejLive pieceBejLive in this.mGlintingPieces)
			{
				for (int i = 0; i < 9; i++)
				{
					if ((double)pieceBejLive.mLighting[i] > 0.01)
					{
						int num4 = (int)Math.Min(num3 * pieceBejLive.mLighting[i] * 255f, 255f);
						g.SetColor(new SexyColor(num4, num4, num4));
						g.SetScale(pieceBejLive.mScale.GetFloat);
						g.DrawImage(AtlasResources.IMAGE_GEM_LIGHTING, (int)(pieceBejLive.mX + (float)pieceBejLive.mOfsX + pieceBejLive.mFlyVX), (int)(pieceBejLive.mY + (float)pieceBejLive.mOfsY + pieceBejLive.mFlyVY), new TRect(i * GameConstants.GEM_WIDTH, BoardBejLive.GetGemLightingIndexForColour(pieceBejLive.mColor) * GameConstants.GEM_HEIGHT, GameConstants.GEM_WIDTH, GameConstants.GEM_HEIGHT));
					}
				}
			}
			this.mGlintingPieces.Clear();
			foreach (PieceBejLive pieceBejLive2 in this.mBlitzGemsRenderer)
			{
				if (pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
				{
					pieceBejLive2.DrawFlameGemOverlay(g, this.mUpdateCnt, (double)num3);
				}
			}
		}

		private TRect BoardRect
		{
			get
			{
				return new TRect(Constants.mConstants.Board_Board_X, Constants.mConstants.Board_Board_Y, AtlasResources.IMAGE_GRID.GetWidth(), AtlasResources.IMAGE_GRID.GetHeight());
			}
		}

		private PieceBejLive MoveAssistedPiece(PieceBejLive pSelectedPiece, int x, int y, PieceBejLive pPrevSelectedPiece)
		{
			if (pSelectedPiece == null)
			{
				return null;
			}
			PieceBejLive result = null;
			int num = 0;
			int num4;
			int num3;
			int num2 = (num3 = (num4 = 0));
			int num5 = 0;
			int num6 = this.FindBestGemMove(pSelectedPiece, ref num5, ref num5, ref num3, ref num4, ref num2);
			if (num6 >= 3)
			{
				return null;
			}
			if (pSelectedPiece.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
			{
				return null;
			}
			Constants.mConstants.S(150f);
			for (int i = pSelectedPiece.mRow - 1; i <= pSelectedPiece.mRow + 1; i++)
			{
				if (i >= 0 && i < 8)
				{
					for (int j = pSelectedPiece.mCol - 1; j <= pSelectedPiece.mCol + 1; j++)
					{
						if (j >= 0 && j < 8 && (j != pSelectedPiece.mCol || i != pSelectedPiece.mRow))
						{
							PieceBejLive pieceAtRowCol = this.GetPieceAtRowCol(i, j);
							if (pieceAtRowCol != pPrevSelectedPiece && pieceAtRowCol != null && base.PointInPiece(pieceAtRowCol, this.mMouseDownX, this.mMouseDownY, this.FUDGE))
							{
								num2 = (num3 = (num4 = 0));
								if (pieceAtRowCol.IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
								{
									return pieceAtRowCol;
								}
								int num7 = 0;
								num6 = this.FindBestGemMove(pieceAtRowCol, ref num7, ref num7, ref num3, ref num4, ref num2);
								if (num6 >= 3)
								{
									num6 += num3 * 6;
									num6 += num4 * 20;
									num6 += num2 * 13;
									if (num6 > num)
									{
										result = pieceAtRowCol;
										num = num6;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		private int FindBestGemMove(PieceBejLive aPiece, ref int theDirXResult, ref int theDirYResult, ref int iFlameCount, ref int iMultiCount, ref int iLaserCount)
		{
			int num = -1;
			for (int i = 0; i < 4; i++)
			{
				int num2 = BoardBejLive.aSwapArray[i, 0];
				int num3 = BoardBejLive.aSwapArray[i, 1];
				int num4 = this.EvalGemSwap(aPiece, num2, num3, ref iFlameCount, ref iMultiCount, ref iLaserCount);
				if (num4 > num)
				{
					num = num4;
					if (theDirXResult != 0)
					{
						theDirXResult = num2;
					}
					if (theDirYResult != 0)
					{
						theDirYResult = num3;
					}
				}
			}
			return num;
		}

		private int EvalGemSwap(PieceBejLive aPiece, int theDirX, int theDirY, ref int iFlameCount, ref int iMultiCount, ref int iLaserCount)
		{
			int mCol = aPiece.mCol;
			int mRow = aPiece.mRow;
			int num = 0;
			int num2 = mCol + theDirX;
			int num3 = mRow + theDirY;
			if (num2 >= 0 && num2 < 8 && num3 >= 0 && num3 < 8)
			{
				PieceBejLive pieceBejLive = this.mBoard[mRow, mCol];
				if (this.mBoard[num3, num2] != null)
				{
					if (this.mBoard[num3, num2].IsFlagSet(PIECEFLAG.PIECEFLAG_HYPERGEM))
					{
						PieceBejLive pieceBejLive2 = aPiece;
						for (int i = 0; i < 8; i++)
						{
							for (int j = 0; j < 8; j++)
							{
								aPiece = this.mBoard[i, j];
								if (aPiece != null && aPiece.mColor == pieceBejLive2.mColor)
								{
									num++;
									if (pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
									{
										iMultiCount++;
									}
									if (pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
									{
										iLaserCount++;
									}
									if (pieceBejLive2.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
									{
										iFlameCount++;
									}
								}
							}
						}
					}
					else if (this.mBoard[num3, num2].mColor >= 0 && this.mBoard[num3, num2].mColor <= 7)
					{
						this.mBoard[mRow, mCol] = this.mBoard[num3, num2];
						this.mBoard[num3, num2] = pieceBejLive;
						MoveAssistEval moveAssistEval = default(MoveAssistEval);
						MoveAssistEval moveAssistEval2 = default(MoveAssistEval);
						MoveAssistEval moveAssistEval3 = default(MoveAssistEval);
						MoveAssistEval moveAssistEval4 = default(MoveAssistEval);
						int largestSetAtRow = this.GetLargestSetAtRow(mRow, ref moveAssistEval);
						int largestSetAtRow2 = this.GetLargestSetAtRow(num3, ref moveAssistEval2);
						int largestSetAtCol = this.GetLargestSetAtCol(mCol, ref moveAssistEval3);
						int largestSetAtCol2 = this.GetLargestSetAtCol(num2, ref moveAssistEval4);
						int num4 = Math.Max(largestSetAtRow, largestSetAtRow2);
						int num5 = Math.Max(largestSetAtCol, largestSetAtCol2);
						if (num4 >= 3)
						{
							num += num4;
							iFlameCount = ((largestSetAtRow > largestSetAtRow2) ? moveAssistEval.Flame : moveAssistEval2.Flame);
							iLaserCount = ((largestSetAtRow > largestSetAtRow2) ? moveAssistEval.Laser : moveAssistEval2.Laser);
							iMultiCount = ((largestSetAtRow > largestSetAtRow2) ? moveAssistEval.Multiplier : moveAssistEval2.Multiplier);
						}
						if (num5 >= 3)
						{
							num += num5;
							iFlameCount = ((largestSetAtCol > largestSetAtCol2) ? moveAssistEval3.Flame : moveAssistEval4.Flame);
							iLaserCount = ((largestSetAtCol > largestSetAtCol2) ? moveAssistEval3.Laser : moveAssistEval4.Laser);
							iMultiCount = ((largestSetAtCol > largestSetAtCol2) ? moveAssistEval3.Multiplier : moveAssistEval4.Multiplier);
						}
						pieceBejLive = this.mBoard[mRow, mCol];
						this.mBoard[mRow, mCol] = this.mBoard[num3, num2];
						this.mBoard[num3, num2] = pieceBejLive;
					}
				}
			}
			return num;
		}

		private int GetLargestSetAtCol(int theCol, ref MoveAssistEval pEval)
		{
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < 8; i++)
			{
				PieceBejLive pieceBejLive = this.mBoard[i, theCol];
				if (pieceBejLive != null)
				{
					if (pieceBejLive.mColor <= 7 && pieceBejLive.mColor == num2)
					{
						num++;
						if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
						{
							num4++;
						}
						else if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
						{
							num5++;
						}
						else if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
						{
							num6++;
						}
						if (num > num3)
						{
							num3 = num;
							pEval.Flame = num4;
							pEval.Laser = num5;
							pEval.Multiplier = num6;
						}
					}
					else
					{
						num2 = pieceBejLive.mColor;
						num = 1;
						num5 = (num4 = (num6 = 0));
					}
				}
				else
				{
					num5 = (num4 = (num6 = 0));
					num2 = -1;
				}
			}
			return num3;
		}

		private int GetLargestSetAtRow(int theRow, ref MoveAssistEval pEval)
		{
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < 8; i++)
			{
				PieceBejLive pieceBejLive = this.mBoard[theRow, i];
				if (pieceBejLive != null)
				{
					if (pieceBejLive.mColor <= 7 && pieceBejLive.mColor == num2)
					{
						num++;
						if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_FLAME))
						{
							num4++;
						}
						else if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_LASER))
						{
							num5++;
						}
						else if (pieceBejLive.IsFlagSet(PIECEFLAG.PIECEFLAG_POINT_MULTIPLIER))
						{
							num6++;
						}
						if (num > num3)
						{
							num3 = num;
							pEval.Flame = num4;
							pEval.Laser = num5;
							pEval.Multiplier = num6;
						}
					}
					else
					{
						num2 = pieceBejLive.mColor;
						num = 1;
						num5 = (num4 = (num6 = 0));
					}
				}
				else
				{
					num2 = -1;
					num5 = (num4 = (num6 = 0));
				}
			}
			return num3;
		}

		public override bool SaveGame()
		{
			this.UpdateProfileGemInfo();
			string savedGameFileName = Board.GetSavedGameFileName();
			this.Pause(false);
			this.Unpause(false);
			Buffer buffer = new Buffer();
			int theLong = 7;
			buffer.WriteLong(theLong);
			buffer.WriteLong(7);
			buffer.WriteLong(this.mBackdropNum);
			buffer.WriteLong(this.mLevel);
			buffer.WriteLong(this.mSpeedBonusPoints);
			buffer.WriteDouble(this.mSpeedBonusNum);
			buffer.WriteLong(this.mSpeedBonusCount);
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					PieceBejLive pieceBejLive = this.mBoard[i, j];
					bool flag = pieceBejLive != null;
					buffer.WriteBoolean(flag);
					if (flag)
					{
						buffer.WriteFloat(pieceBejLive.mX);
						buffer.WriteFloat(pieceBejLive.mY);
						buffer.WriteFloat(pieceBejLive.mZ);
						buffer.WriteLong(pieceBejLive.mCol);
						buffer.WriteLong(pieceBejLive.mRow);
						buffer.WriteLong(pieceBejLive.mOfsX);
						buffer.WriteLong(pieceBejLive.mOfsY);
						buffer.WriteBoolean(pieceBejLive.mShrinking);
						buffer.WriteLong(pieceBejLive.mShrinkSize);
						for (int k = 0; k < pieceBejLive.mLighting.Length; k++)
						{
							buffer.WriteFloat(pieceBejLive.mLighting[k]);
						}
						buffer.WriteBoolean(pieceBejLive.mShineAnim);
						buffer.WriteDouble(pieceBejLive.mShineFactor);
						buffer.WriteFloat(pieceBejLive.mShineAnimFrame);
						buffer.WriteLong(pieceBejLive.mSparkleLife);
						buffer.WriteLong(pieceBejLive.mSparkleFrame);
						buffer.WriteLong(pieceBejLive.mSparklePairOfsCol);
						buffer.WriteLong(pieceBejLive.mSparklePairOfsRow);
						buffer.WriteFloat(pieceBejLive.mFallVelocity);
						buffer.WriteFloat(pieceBejLive.mXVelocity);
						buffer.WriteLong(pieceBejLive.mColor);
						buffer.WriteBoolean(pieceBejLive.mIsElectrocuting);
						buffer.WriteFloat(pieceBejLive.mElectrocutePercent);
						buffer.WriteLong(pieceBejLive.mLastActiveTick);
						buffer.WriteLong(pieceBejLive.mChangedTick);
						buffer.WriteLong(pieceBejLive.mExplodeDelay);
						buffer.WriteLong(pieceBejLive.mSwapTick);
						buffer.WriteLong(pieceBejLive.mSpinFrame);
						buffer.WriteFloat(pieceBejLive.mRotPct);
						buffer.WriteLong(pieceBejLive.mFlags);
					}
				}
			}
			this.StabilizeState();
			buffer.WriteLong((int)this.mState);
			buffer.WriteBoolean(this.mTimedMode);
			buffer.WriteBoolean(this.mPuzzleMode);
			buffer.WriteBoolean(this.mCreateMode);
			buffer.WriteBoolean(this.mFreeMove);
			buffer.WriteBoolean(this.mFirstMove);
			buffer.WriteFloat(this.mLevelBarPct);
			buffer.WriteLong(this.mTimePlayedAdd);
			buffer.WriteLong(this.mTotalGameTimeCommitted);
			buffer.WriteLong(this.mPoints);
			buffer.WriteLong(this.mPointsWithoutSpeedBonus);
			buffer.WriteLong(this.mLevelPointsTotal);
			buffer.WriteLong(this.mPointMultiplier);
			buffer.WriteLong(this.mDispPoints);
			buffer.WriteLong(this.mGemsCleared);
			buffer.WriteLong(this.mGemsClearedCommitted);
			buffer.WriteLong(this.mMoveClearCount);
			buffer.WriteLong(this.mChainCount);
			buffer.WriteLong(this.mLongestChainReaction);
			buffer.WriteLong(this.mHighestScoringMove);
			buffer.WriteBoolean(this.mInBonus);
			buffer.WriteLong(this.mBonusCount);
			buffer.WriteLong(this.mLastQuakePoints);
			buffer.WriteLong(this.mNextQuakePoints);
			buffer.WriteLong(this.mPointsPerQuake);
			buffer.WriteDouble(this.mBonusPenalty);
			buffer.WriteDouble(this.mTimedPenaltyVel);
			buffer.WriteDouble(this.mTimedPenaltyAccel);
			buffer.WriteDouble(this.mTimedPenaltyJerk);
			buffer.WriteLong(this.mTimerBarNum);
			buffer.WriteLong(this.mTransitionPos);
			buffer.WriteFloat(this.mWhirlpoolFrame);
			buffer.WriteDouble(this.mWhirlpoolFade);
			buffer.WriteBoolean(this.mShowedLevelThing);
			buffer.WriteBoolean(this.mSeenPowergemHint);
			buffer.WriteBoolean(this.mSeenHypercubeHint);
			buffer.WriteBoolean(this.mSeenLasergemHint);
			buffer.WriteDouble(this.mTimedBonus);
			buffer.WriteDouble(this.mTimedBonusMult);
			buffer.WriteLong(this.mEndlessNumOuterGems);
			buffer.WriteLong(this.mEndlessNumInnerGems);
			buffer.WriteLong(this.mEndlessNumRainbowGems);
			buffer.WriteBoolean(this.mHandleBombs);
			buffer.WriteBoolean(this.mInsaneMode);
			buffer.WriteBoolean(this.mSecretMode);
			buffer.WriteBoolean(this.mTwilightMode);
			buffer.WriteBoolean(this.mDoubleSpeed);
			buffer.WriteBoolean(this.mGravityReversed);
			buffer.WriteBoolean(this.mWasModeUnlocked);
			buffer.WriteLong(this.mPuzzleHintCount);
			buffer.WriteLong(this.mNumHyperGemsCreated);
			buffer.WriteLong(this.mNumPowerGemsCreated);
			buffer.WriteLong(this.mNumLaserGemsCreated);
			buffer.WriteLong(this.mNumHyperGemsCreatedCommitted);
			buffer.WriteLong(this.mNumPowerGemsCreatedCommitted);
			buffer.WriteLong(this.mNumLaserGemsCreatedCommitted);
			buffer.WriteLong(BoardBejLive.perfectGamesCompleted);
			buffer.WriteBoolean(this.noIllegalMovesAchievementFlag);
			buffer.WriteBoolean(this.noHintsAchievementFlag);
			buffer.WriteLong(this.lastColourMatch);
			buffer.WriteLong(this.monocolouristCount);
			buffer.WriteLong(this.starGemsActivated);
			buffer.WriteDouble(this.trialGameTime);
			for (int l = 0; l < this.mGameStats.Length; l++)
			{
				buffer.WriteLong(this.mGameStats[l]);
			}
			return this.mApp.WriteBufferToFile(savedGameFileName, buffer);
		}

		public const int TRIAL_MODE_GAME_DURATION = 90;

		private const int trialTimeUpMessageCenterDuration = 80;

		private const int trialTimeUpGameOverShakeDuration = 300;

		private const int DEFAULT_COMBO_LEN = 3;

		private const int MAX_COMBO_LEN = 5;

		private const int MAX_COMBO_POWERUP_LEVEL = 3;

		private const int NUM_GEMCOLORS = 7;

		private const int NUM_TIME_SEGMENTS = 13;

		private const int MAX_SHARD = 5;

		private const int HINT_COOLDOWN = 400;

		private const int AUTOHINT_DELAY = 1000;

		private static readonly string GoString = Strings.Go;

		private static readonly string GamePausedString = Strings.Game_Paused;

		private static readonly string TapToContinueString = Strings.Tap_To_Continue;

		private static readonly string trialUpMessageTop = Strings.Board_Trial_Up_Message_Top_Of_Screen;

		private List<ParticleEmitter> lowerLayerEmitters = new List<ParticleEmitter>(10);

		private List<ParticleEmitter> upperLayerEmitters = new List<ParticleEmitter>(10);

		private List<ParticleEmitter> progressBarEmitters = new List<ParticleEmitter>(5);

		private int chainReactionAchievementCounter;

		private bool noIllegalMovesAchievementFlag = true;

		private bool noHintsAchievementFlag = true;

		private static int perfectGamesCompleted;

		private int powerBrokerAchievementCount;

		private int chainReactorSpecialsTriggered;

		private int lastColourMatch = -1;

		private int monocolouristCount;

		private int starGemsActivated;

		private int blazingSpeedCountThisLevel;

		private int dynamicBoardCounter;

		private int highFlyerScore;

		private static int[] oneMoveGameColourCounter = new int[7];

		private static int[] oneMoveGameTypeCounter = new int[3];

		private bool didReachLevelEnd;

		private static Dictionary<int, string> numberStringCache = new Dictionary<int, string>();

		private bool newLevelStarted;

		private double trialGameTime = 90.0;

		private bool trialExpired;

		private int? trialTimeUpMessageOffset = default(int?);

		private ActionSheet actionSheet;

		private CurvedVal mComplementAlpha = CurvedVal.GetNewCurvedVal();

		private CurvedVal mComplementScale = CurvedVal.GetNewCurvedVal();

		private MenuHintButton mMenuHintButton;

		private int mComplementNum;

		private int mLastComplement;

		private int mPointsWithoutSpeedBonus;

		private int mChainCount;

		private int mMovePoints;

		private Alert alert;

		public new PieceBejLive mHintPiece;

		private bool mDoHintPenalty;

		private uint mLastWarningSound;

		private bool mNewGameClicked;

		private int mBonusCount;

		private bool mCreateMode;

		private int mEndlessNumOuterGems;

		private int mEndlessNumInnerGems;

		private int mEndlessSparkleOuter;

		private int mEndlessSparkleInner;

		private int mEndlessSparkleLife;

		private int mEndlessNumRainbowGems;

		private bool mFirstMove;

		private bool mFreeMove;

		private bool mInBonus;

		private int mMoveClearCount;

		private int mNumPowerGemsCreated;

		private int mNumHyperGemsCreated;

		private int mNumLaserGemsCreated;

		private int mNumPowerGemsCreatedCommitted;

		private int mNumHyperGemsCreatedCommitted;

		private int mNumLaserGemsCreatedCommitted;

		private int mGemsClearedCommitted;

		private bool mGameInvalid;

		private bool mPuzzleMode;

		private int mPuzzleHintCount;

		private bool mHandleBombs;

		private int mSecretTotalPuzzles;

		private int mSecretPuzzlesSolved;

		private int mPlanetPuzzlesSolved;

		private bool mSeenPowergemHint;

		private bool mSeenHypercubeHint;

		private bool mSeenLasergemHint;

		private bool mShowedLevelThing;

		private int mTicksPlayed;

		private int mTimerBarNum;

		private int mShakeLife;

		private bool mProcessedEndGameInfo;

		private int mTotalGameTimeCommitted;

		private int mTransitionPos;

		private bool mSecretMode;

		private bool mTwilightMode;

		private bool mInsaneMode;

		private bool mWasModeUnlocked;

		private bool mClassicMode;

		private bool mEndlessMode;

		private bool mBlitzMode;

		private int mLastQuakePoints;

		private int mNextQuakePoints;

		private int mPointsPerQuake;

		private double mBonusPenalty;

		private string mDispLevel;

		private string mDispLevelStretchy;

		private double mTimedBonus;

		private double mTimedBonusMult;

		private double mTimedPenaltyVel;

		private double mTimedPenaltyAccel;

		private double mTimedPenaltyJerk;

		private int mTimerDelay;

		private float mInterfaceRestorePercent;

		private bool mGravityReversed;

		private int delayBeforeSpeedBonusDecrease;

		private int mTimePlayedAdd;

		private uint mTimePeriodStart;

		private static TriVertex[,] aTriVertices = new TriVertex[512, 3];

		private float mWhirlpoolFrame;

		private double mWhirlpoolFade;

		private double mWhirlpoolRot;

		private double mWhirlpoolRotAcc;

		private double mUIWarpPercent;

		private bool mFirstWhirlDraw;

		private float mWarpSpeed;

		private int mWarpDelay;

		private int mUISuckDelay;

		private float mWarpSizeMult;

		private int mHyperspaceDelay;

		private static WarpPoint[,] mWarpPoints = new WarpPoint[16, 16];

		private CurvedVal mUIWarpPercentAdd = CurvedVal.GetNewCurvedVal();

		private static Hyperspace mHyperspace;

		private SoundInstance mWhirlpoolSound;

		private float aSpeed = 1f;

		private readonly float SPEED_SCORE_MULT = Constants.mConstants.M(50f);

		public static readonly SexyColor[] gAllGemColors = new SexyColor[]
		{
			new SexyColor(255, 255, 255),
			new SexyColor(255, 255, 0),
			new SexyColor(255, 255, 255),
			new SexyColor(0, 128, 255),
			new SexyColor(255, 0, 0),
			new SexyColor(255, 0, 255),
			new SexyColor(255, 128, 0),
			new SexyColor(0, 255, 0)
		};

		public static readonly SexyColor[] gPointColors = new SexyColor[]
		{
			new SexyColor(255, 255, 64),
			new SexyColor(255, 255, 255),
			new SexyColor(64, 128, 255),
			new SexyColor(255, 100, 100),
			new SexyColor(255, 64, 255),
			new SexyColor(255, 181, 145),
			new SexyColor(64, 255, 64),
			new SexyColor(64, 64, 64),
			new SexyColor(64, 64, 64),
			new SexyColor(255, 255, 255)
		};

		public readonly int FUDGE = GameConstants.GEM_WIDTH / 2;

		public static readonly SexyColor[] gComboColors = new SexyColor[]
		{
			new SexyColor(255, 0, 0),
			new SexyColor(192, 192, 192),
			new SexyColor(0, 224, 0),
			new SexyColor(224, 224, 0),
			new SexyColor(224, 0, 224),
			new SexyColor(255, 128, 0),
			new SexyColor(0, 0, 255),
			new SexyColor(0, 0, 0)
		};

		internal static SexyColor[] gGemColors;

		private static int[] gComplementPoints = new int[] { 3, 6, 12, 20, 30, 45 };

		internal static readonly int[] gComplements = new int[]
		{
			Resources.SOUND_GOOD,
			Resources.SOUND_EXCELLENT,
			Resources.SOUND_AWESOME,
			Resources.SOUND_SPECTACULAR,
			Resources.SOUND_EXTRAORDINARY,
			Resources.SOUND_UNBELIEVABLE
		};

		internal static readonly string[] gComplementStrings = new string[]
		{
			Strings.Board_Good,
			Strings.Board_Excellent,
			Strings.Board_Awesome,
			Strings.Board_Spectacular,
			Strings.Board_Extraordinary,
			Strings.Board_Unbelievable
		};

		internal static readonly AtlasResources.AtlasImageId[] gWordId = new AtlasResources.AtlasImageId[]
		{
			AtlasResources.AtlasImageId.IMAGE_POPCAP_BUTTON_ID,
			AtlasResources.AtlasImageId.IMAGE_POPCAP_BUTTON_ID,
			AtlasResources.AtlasImageId.IMAGE_POPCAP_BUTTON_ID,
			AtlasResources.AtlasImageId.IMAGE_POPCAP_BUTTON_ID,
			AtlasResources.AtlasImageId.IMAGE_POPCAP_BUTTON_ID,
			AtlasResources.AtlasImageId.IMAGE_POPCAP_BUTTON_ID,
			AtlasResources.AtlasImageId.IMAGE_POPCAP_BUTTON_ID
		};

		public static int gFrameLightCount = 0;

		internal static int gExplodeCount = 0;

		internal static int gShardCount = 0;

		internal static int[,] gExplodePoints = new int[64, 2];

		public bool mFullLaser;

		public float mUpdateAcc;

		public int mNextPieceId;

		public PieceBejLive[,] mBoard = new PieceBejLive[8, 8];

		public float[] mBumpVelocities = new float[8];

		public int[] mNextColumnCredit = new int[8];

		public static Dictionary<int, PieceBejLive> mPieceMap = new Dictionary<int, PieceBejLive>(200);

		public MTRand mRand = new MTRand();

		public int mLastHitSoundTick;

		public List<SwapData> mSwapDataVector = new List<SwapData>();

		public List<MoveData> mMoveDataVector = new List<MoveData>();

		public List<QueuedMove> mQueuedMoveVector = new List<QueuedMove>();

		public List<StateInfo> mStateInfoVector = new List<StateInfo>();

		public int[] mGameStats = new int[24];

		public int mGameOverCount;

		public int mLevelCompleteCount;

		private int mDispPointsStore;

		public int mDispPoints;

		public float mLevelBarPct;

		public int mLevelPointsTotal;

		public int mLevel;

		public int mPointMultiplier;

		public int mMultiplierCount;

		public int mCurMoveCreditId;

		public int mCurMatchId;

		public int mGemFallDelay;

		public bool mTimeExpired;

		public int mGameTicks;

		public int mIdleTicks;

		public int mLapseGameTicks;

		public int mFindSetCounter;

		public int mLastMatchTick;

		public int mLastMatchTime;

		public int mMatchTallyCount;

		public int mLastMatchTally;

		public CurvedVal mSpeedModeFactor = CurvedVal.GetNewCurvedVal();

		public float mSpeedBonusAlpha;

		public CurvedVal mSpeedBonusDisp = CurvedVal.GetNewCurvedVal();

		public float mSpeedNeedle;

		public int mSpeedBonusPoints;

		public bool mFavorComboGems;

		public List<int> mFavorGemColors = new List<int>();

		public double mSpeedBonusNum;

		public int mSpeedBonusCount;

		public int mSpeedBonusLastCount;

		public int mSpeedMedCount;

		public int mSpeedHighCount;

		public CurvedVal mSpeedBonusPointsGlow = CurvedVal.GetNewCurvedVal();

		public CurvedVal mSpeedBonusPointsScale = CurvedVal.GetNewCurvedVal();

		public float mSpeedBonusFlameModePct;

		public int[] mComboColors = new int[5];

		public int mComboCount;

		public int mLastComboCount;

		public int mComboLen;

		public float mComboCountDisp;

		public CurvedVal mComboFlashPct = CurvedVal.GetNewCurvedVal();

		public float mComboSelectorAngle;

		public int mLastPlayerSwapColor;

		public int mMoneyDisp;

		public int mMoneyDispGoal;

		public float mComboBonusSlowdownPct;

		public List<LightningStorm> mLightningStorms = new List<LightningStorm>();

		public PointsManager mPointsManager;

		public EffectsManager mPostFXManager;

		public bool mUserPaused;

		public float mBoardHidePct;

		public float mVisPausePct;

		public bool mShowLevelPoints;

		public int mWantHintTicks;

		public bool mShowMoveCredit;

		public bool mDoThirtySecondVoice;

		public float mHyperGemColorFactor;

		public FColor mHyperGemColor = new FColor();

		public float mBoardDarken;

		private bool mouseWentDownOnSelected;

		public bool mMouseDown;

		public int mMouseDownX;

		public int mMouseDownY;

		public int mBackgroundIdx;

		public bool mWantLevelup;

		public CurvedVal mScale = CurvedVal.GetNewCurvedVal();

		protected int mMinutes;

		protected int mDropCountdown;

		protected int mWantCounter;

		protected int mWantGemsCleared;

		protected int mDropGameTick;

		protected int mCountUnder;

		protected int mNumLastMoves;

		protected int mPrevMultiGemColor;

		protected bool mReadyForDrop;

		protected bool mDroppingMultiplier;

		protected static int mBlitzFlags = 30;

		protected int mBoostsEnabled;

		protected int mBoostsUsed;

		protected bool mFrozen;

		protected bool mEditing;

		protected int[] mPointsBreakdown = new int[13];

		protected int[] mSpeedBonusBreakdown = new int[13];

		protected List<int> mMultiplierTimes = new List<int>();

		protected int mPreHurrahPoints;

		protected bool mDidTimeUp;

		protected bool mPosted;

		protected bool mGotBestScore;

		protected int mTimeUpCount;

		protected int mBestScore;

		protected CurvedVal mGameSpeed = CurvedVal.GetNewCurvedVal();

		protected CurvedVal mTimeUpAlpha = CurvedVal.GetNewCurvedVal();

		protected CurvedVal mTimeUpScale = CurvedVal.GetNewCurvedVal();

		protected CurvedVal mGo = CurvedVal.GetNewCurvedVal();

		protected CurvedVal mSlotsSpinPct = CurvedVal.GetNewCurvedVal();

		protected CurvedVal mSlotsAlpha = CurvedVal.GetNewCurvedVal();

		protected int mPrevSpeedBonusPoints;

		protected PieceBejLive mSelectedPiece;

		private List<PieceBejLive> aCandidatePieces = new List<PieceBejLive>();

		private int[,] offsets = new int[,]
		{
			{ 1, 0 },
			{ -1, 0 },
			{ 0, 1 },
			{ 0, -1 },
			{ -1, -1 },
			{ -1, 1 },
			{ 1, -1 },
			{ 1, 1 }
		};

		private List<TPoint> aPointVector = new List<TPoint>(100);

		private static bool[] gemMask = new bool[8];

		private List<PieceBejLive> skippedGems = new List<PieceBejLive>();

		private int[] aFacet = new int[] { 0, 4, 8, 2, 6, 3, 7, 8, 1, 5 };

		private int[,] aLightOffset = new int[,]
		{
			{ 0, -1 },
			{ -1, -1 },
			{ -1, 0 },
			{ -1, 1 },
			{ 0, 1 },
			{ 1, 1 },
			{ 1, 0 },
			{ 1, -1 },
			{ 0, 0 }
		};

		private static StringBuilder scoreStringCache = new StringBuilder();

		private static int scoreStringCachedScore = int.MinValue;

		private static Dictionary<int, string> speedpointStrings = new Dictionary<int, string>();

		private static double speedBonusDisplay = 0.0;

		private List<PieceBejLive> aNewPieceVector = new List<PieceBejLive>();

		private List<int> aGemList = new List<int>();

		private int[] findMoveCoords = new int[4];

		private readonly int[,] tempSwapArray = new int[,]
		{
			{ 1, 0 },
			{ -1, 0 },
			{ 0, 1 },
			{ 0, -1 }
		};

		private static List<MatchSet> aMatchedSets = new List<MatchSet>(20);

		private static List<PieceBejLive> aDelayingPieceSet = new List<PieceBejLive>(20);

		private static List<PieceBejLive> aTallyPieceSet = new List<PieceBejLive>(20);

		private static List<PieceBejLive> aDeferLaserVector = new List<PieceBejLive>(20);

		private static List<PieceBejLive> aDeferExplodeVector = new List<PieceBejLive>(20);

		private static List<int> aMoveCreditSet = new List<int>(20);

		private static Dictionary<PieceBejLive, PowerUpPiecePair> aDeferPowerupMap = new Dictionary<PieceBejLive, PowerUpPiecePair>(20);

		private static List<PieceBejLive> aPowerupCandidates = new List<PieceBejLive>(20);

		private static List<PieceBejLive> aNewestPowerupCandidates = new List<PieceBejLive>(20);

		private static readonly int[,] anOffsets = new int[,]
		{
			{ -1, 0 },
			{ 1, 0 },
			{ 0, -1 },
			{ 0, 1 }
		};

		private static List<int> matchedColours = new List<int>(10);

		private readonly int[,] aSmCheckPositions = new int[,]
		{
			{ -1, -1 },
			{ 0, -1 },
			{ 1, -1 },
			{ -1, 0 },
			{ 1, 0 },
			{ -1, 1 },
			{ 0, 1 },
			{ 1, 1 },
			{ 0, 0 }
		};

		private readonly int[,] aLgCheckPositions = new int[,]
		{
			{ -1, -1 },
			{ 0, -1 },
			{ 1, -1 },
			{ -1, 0 },
			{ 1, 0 },
			{ -1, 1 },
			{ 0, 1 },
			{ 1, 1 },
			{ 0, -2 },
			{ -2, 0 },
			{ 2, 0 },
			{ 0, 2 },
			{ 0, 0 }
		};

		private UI_ORIENTATION miPrevOrientation;

		private UI_ORIENTATION miCurrOrientation;

		private List<PieceBejLive> mBlitzGemsRenderer = new List<PieceBejLive>(100);

		private List<PieceBejLive> mMultFontRenderer = new List<PieceBejLive>();

		private bool mbFirstDraw;

		private static readonly int[,] aSwapArray = new int[,]
		{
			{ 1, 0 },
			{ -1, 0 },
			{ 0, 1 },
			{ 0, -1 }
		};

		private PieceBejLive mpHintPiece;

		private List<PieceBejLive> mGlintingPieces = new List<PieceBejLive>(100);

		private CProfiler mProfiler = new CProfiler();

		private string mGameStatString = new string(new char[128]);

		private int miEndGamePause;

		private int miGameOverTicks;

		private int miBufferCheckCount;

		private bool mbPieceDeleted;

		private int miWarningSoundPlaying;

		private int miLightningGemCount;

		private int miHintCoolDownCount;

		private bool mbAutoHint;

		private int miMultiPlayTick;

		private enum PauseDialogButtonIds
		{
			Resume = 2000,
			Achievements,
			Leaderboards,
			HelpOptions,
			Unlock,
			MainMenu
		}

		private enum QuitConfirmButtonIds
		{
			Save = 3000,
			Back
		}

		private enum OneMoveAchievementTypes
		{
			HyperCube,
			StarGem,
			FlameGem,
			MAX
		}

		public enum ButtonId
		{
			BUTTON_HINT,
			BUTTON_MENU,
			BUTTON_RESET,
			BUTTON_REPLAY,
			BUTTON_ZEN_OPTIONS
		}

		public enum BLITZFLAG
		{
			BLITZFLAG_FB_MULTIPLIERS = 1,
			BLITZFLAG_LAST_HURRAH,
			BLITZFLAG_FLAME_BONUS = 4,
			BLITZFLAG_GEM_SPEEDUP = 8,
			BLITZFLAG_LASER_GEMS = 16
		}

		private enum PIECEFLAG5
		{
			ORIENTATION_LANDSCAPE,
			ORIENTATION_PORTRAIT
		}
	}
}
