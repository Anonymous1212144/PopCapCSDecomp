using System;
using System.Collections.Generic;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.UI;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Sound;
using SexyFramework.Widget;

namespace BejeweledLivePlus
{
	public class Board : Bej3Widget, Bej3ButtonListener, ButtonListener, DialogListener, SliderListener
	{
		private void ClipCollapsingBoard(Graphics g)
		{
			g.SetClipRect(GlobalMembers.S(this.GetBoardX()), GlobalMembers.S(this.GetBoardY()) + this.mTransBoardOffsetY + ConstantsWP.BOARD_FRAME_OVERLAP_Y, this.mWidth, GlobalMembers.S(100) * 8 - ConstantsWP.BOARD_FRAME_OVERLAP_Y - this.mTransBoardOffsetY * 2);
		}

		public Board()
			: base(Menu_Type.MENU_GAMEBOARD, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mShouldUnloadContentWhenDone = true;
			this.mBackgroundLoadedThreaded = false;
			this.FUDGE = 75;
			this.mCheatPiece = null;
			this.mCheatInputingScore = false;
			this.mCheatScoreStr = "";
			this.mSlowedDown = false;
			this.mSlowDownCounter = 0;
			this.mDoesSlideInFromBottom = (this.mCanAllowSlide = false);
			this.mContentLoaded = false;
			this.mFirstDraw = true;
			this.mReplayButton = null;
			this.mReplayPulsePct.SetConstant(-1.0);
			this.mReplayWasTutorial = false;
			this.mHintButton = null;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					this.mBoard[i, j] = null;
				}
			}
			this.mBoardType = EBoardType.eBoardType_Normal;
			this.mGameFinished = false;
			this.mNextPieceId = 1;
			this.mBottomFillRow = 7;
			int seed = BejeweledLivePlus.Misc.Common.Rand();
			this.mRand.SRand((uint)seed);
			for (int k = 0; k < 40; k++)
			{
				this.mGameStats[k] = 0;
			}
			this.mFullLaser = 1;
			this.mHasAlpha = true;
			this.mUpdateAcc = 0f;
			this.mGameOverCount = 0;
			this.mLevelCompleteCount = 0;
			this.mScrambleDelayTicks = 0;
			this.mOffsetX = 0;
			this.mOffsetY = 0;
			this.mLevelBarSizeBias = 0;
			this.mInLoadSave = false;
			this.mComplementNum = -1;
			this.mLastComplement = -1;
			this.mLastHitSoundTick = 0;
			this.mClip = false;
			this.mShowAutohints = true;
			this.mMouseDown = false;
			this.mMouseDownX = 0;
			this.mMouseDownY = 0;
			this.mMouseUpPiece = null;
			this.mContinuedFromLoad = false;
			this.mUiConfig = Board.EUIConfig.eUIConfig_Standard;
			this.mSidebarText = "";
			this.mShowLevelPoints = false;
			this.mFavorComboGems = false;
			this.mWantNewCoin = false;
			this.mCoinsEarned = 0;
			this.mUserPaused = false;
			this.mBoardHidePct = 0f;
			this.mVisPausePct = 0f;
			this.mInUReplay = false;
			this.mWasLevelUpReplay = false;
			this.mUReplayLastTick = 0;
			this.mUReplayGameFlags = 3;
			this.mRewindSound = null;
			this.mRecordTimestamp = 0;
			this.mPlaybackTimestamp = 0;
			this.mHasReplayData = false;
			this.mWatchedCurReplay = false;
			this.mReplayIgnoredMoves = 0;
			this.mReplayHadIgnoredMoves = false;
			this.mInReplay = false;
			this.mIsOneMoveReplay = false;
			this.mIsWholeGameReplay = false;
			this.mRewinding = false;
			this.mTimeExpired = false;
			this.mHadReplayError = false;
			this.mLastWarningTick = 0;
			this.mShowMoveCredit = false;
			this.mDoThirtySecondVoice = true;
			this.mSunFired = false;
			this.mLastSunTick = 0;
			this.mCoinCatcherAppearing = false;
			this.mCoinCatcherPctPct = 0.0;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_COIN_CATCHER_PCT, this.mCoinCatcherPct);
			this.mPendingCoinAnimations = 0;
			this.mPointsManager = new PointsManager();
			this.mPointsManager.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.AddWidget(this.mPointsManager);
			this.mBackground = null;
			this.mBackgroundIdx = -1;
			this.mTimeSlider = null;
			this.mPreFXManager = new EffectsManager(this);
			this.mPreFXManager.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mPostFXManager = new EffectsManager(this, true);
			this.mPostFXManager.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mTutorialFlags = (int)GlobalMembers.gApp.mProfile.mTutorialFlags;
			this.mWholeGameReplay.mTutorialFlags = GlobalMembers.gApp.mProfile.mTutorialFlags;
			this.mWholeGameReplay.mReplayTicks = 0;
			this.mWidgetFlagsMod.mAddFlags |= 2;
			this.mSideAlpha.SetConstant(1.0);
			this.mSideXOff.SetConstant(0.0);
			this.mAlpha.SetConstant(1.0);
			this.mScale.SetConstant(1.0);
			this.mKilling = false;
			this.mHardwareSpeedBonusDraw = GlobalMembers.gApp.mGraphicsDriver.GetRenderDevice3D() != null && GlobalMembers.gApp.mGraphicsDriver.GetRenderDevice3D().SupportsImageRenderTargets() && GlobalMembers.gApp.mGraphicsDriver.GetRenderDevice3D().SupportsPixelShaders();
			if (GlobalMembers.M(1) != 0)
			{
				this.mHardwareSpeedBonusDraw = false;
			}
			this.mSpeedFirePIEffect = null;
			this.mSpeedFireBarPIEffect[0] = null;
			this.mSpeedFireBarPIEffect[1] = null;
			this.mLevelBarPIEffect = null;
			this.mCountdownBarPIEffect = null;
			this.mHyperspace = null;
			this.mWantLevelup = false;
			this.mFlameSound = null;
			this.mSliderSetTicks = -1;
			this.mRestartPrevImage = null;
			this.mStartDelay = 0;
			this.mWantHelpDialog = -1;
			this.mFlattenedImage = null;
			this.mCursorSelectPos = new Point(-1, -1);
			this.mUReplayVersion = 0;
			this.mMoveCounter = 0;
			this.mShowPointMultiplier = false;
			this.mShowBoard = true;
			this.mGameOverPiece = null;
			this.mResetButton = null;
			this.mNeedsMaskCleared = false;
			this.mWantsReddishFlamegems = false;
			for (int l = 0; l < 7; l++)
			{
				this.mNewGemColors.Add(l);
				this.mNewGemColors.Add(l);
			}
			this.mBadgeManager = BadgeManager.GetBadgeManagerInstance();
			this.mMessager = null;
			this.mMessager = new Messager();
			this.mMessager.Init(GlobalMembersResources.FONT_DIALOG);
			this.mFlattening = false;
			this.mSlideXScale = 0f;
			this.mGameClosing = false;
			this.mGoAnnouncementDone = false;
			this.mTimeAnnouncementDone = false;
			this.mSuspendingGame = false;
			this.mForceReleaseButterfly = false;
			this.mForcedReleasedBflyPiece = null;
			GlobalMembers.gComplementStr[0] = GlobalMembers._ID("Good", 3561);
			GlobalMembers.gComplementStr[1] = GlobalMembers._ID("Excellent", 3562);
			GlobalMembers.gComplementStr[2] = GlobalMembers._ID("Awesome", 3563);
			GlobalMembers.gComplementStr[3] = GlobalMembers._ID("Spectacular", 3564);
			GlobalMembers.gComplementStr[4] = GlobalMembers._ID("Extraordinary", 3565);
			GlobalMembers.gComplementStr[5] = GlobalMembers._ID("Unbelievable", 3566);
			GlobalMembers.gComplementStr[6] = GlobalMembers._ID("Blazing Speed", 3567);
			this.mNOfIntentionalMatchesDuringCascade = 0;
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				if (GlobalMembers.gApp.GetDialog(18) != null)
				{
					return;
				}
				if ((this.mWantLevelup || this.mHyperspace != null) && !this.mHyperspacePassed)
				{
					if (this.mHyperspace != null)
					{
						HyperspaceWhirlpool hyperspaceWhirlpool = this.mHyperspace as HyperspaceWhirlpool;
						hyperspaceWhirlpool.mShowBkg = true;
						hyperspaceWhirlpool.mIsDone = true;
						this.mShowBoard = false;
						this.mHyperspacePassed = true;
						hyperspaceWhirlpool.SetState(HyperspaceWhirlpool.HyperSpaceState.HyperSpaceState_PortalRide);
						foreach (Announcement announcement in this.mAnnouncements)
						{
							if (announcement.mText == GlobalMembers._ID("LEVEL\nCOMPLETE", 139))
							{
								this.mAnnouncements.Remove(announcement);
								break;
							}
						}
					}
					return;
				}
				if (GlobalMembers.gApp.mDialogMap.Count != 0)
				{
					return;
				}
				if (this.mInReplay)
				{
					this.BackToGame();
					return;
				}
				if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN && GlobalMembers.gApp.mMenus[19].mY != ConstantsWP.MENU_Y_POS_HIDDEN && GlobalMembers.gApp.mMenus[19].mY != 0)
				{
					return;
				}
				args.processed = true;
				GlobalMembers.gApp.mLosfocus = true;
				PauseMenu pauseMenu = GlobalMembers.gApp.mMenus[7] as PauseMenu;
				if (pauseMenu != null)
				{
					pauseMenu.ButtonDepress(10001);
				}
			}
		}

		public override void Dispose()
		{
			while (this.mLightningStorms.Count > 0)
			{
				if (this.mLightningStorms[this.mLightningStorms.Count - 1] != null)
				{
					this.mLightningStorms[this.mLightningStorms.Count - 1].Dispose();
				}
				this.mLightningStorms.RemoveAt(this.mLightningStorms.Count - 1);
			}
			this.ClearAllPieces();
			this.RemoveAllWidgets(true, false);
			GlobalMembers.KILL_WIDGET_NOW(this.mPreFXManager);
			if (GlobalMembers.gApp.mBlitzBackground != this.mBackground && GlobalMembers.gApp.mBlitzBackgroundLo != this.mBackground)
			{
				GlobalMembers.KILL_WIDGET_NOW(this.mBackground);
			}
			GlobalMembers.KILL_WIDGET_NOW(this.mHyperspace);
			if (this.mRewindSound != null)
			{
				this.mRewindSound.Release();
				this.mRewindSound = null;
			}
			if (this.mSpeedFirePIEffect != null)
			{
				this.mSpeedFirePIEffect.Dispose();
			}
			if (this.mSpeedFireBarPIEffect[0] != null)
			{
				this.mSpeedFireBarPIEffect[0].Dispose();
			}
			if (this.mSpeedFireBarPIEffect[1] != null)
			{
				this.mSpeedFireBarPIEffect[1].Dispose();
			}
			if (this.mLevelBarPIEffect != null)
			{
				this.mLevelBarPIEffect.Dispose();
			}
			if (this.mCountdownBarPIEffect != null)
			{
				this.mCountdownBarPIEffect.Dispose();
			}
			while (this.mAnnouncements.Count > 0)
			{
				if (this.mAnnouncements[this.mAnnouncements.Count - 1] != null)
				{
					this.mAnnouncements[this.mAnnouncements.Count - 1].Dispose();
				}
				this.mAnnouncements.RemoveAt(this.mAnnouncements.Count - 1);
			}
			if (this.mFlameSound != null)
			{
				this.mFlameSound.Release();
			}
			if (this.mShouldUnloadContentWhenDone)
			{
				this.UnloadContent();
			}
			base.Dispose();
		}

		public virtual int GetGameOverCountTreshold()
		{
			return GlobalMembers.M(400);
		}

		public virtual void Pause()
		{
			this.mUserPaused = true;
		}

		public virtual void Unpause()
		{
			this.mUserPaused = false;
			this.PlayMenuMusic();
			this.mSuspendingGame = false;
		}

		public virtual void UnloadContent()
		{
			this.mContentLoaded = false;
		}

		public virtual void LoadContent(bool threaded)
		{
			if (threaded)
			{
				BejeweledLivePlusApp.LoadContentInBackground("GamePlay");
				this.mBackgroundIdx = -1;
				this.SetupBackground(1);
				this.mBackgroundLoadedThreaded = true;
				return;
			}
			BejeweledLivePlusApp.LoadContent("GamePlay");
			this.LinkUpAssets();
			this.RefreshUI();
			this.mContentLoaded = true;
		}

		public override void Show()
		{
			if (this.mState != Bej3WidgetState.STATE_IN)
			{
				this.LoadContent(false);
			}
			base.Show();
			this.mY = 0;
		}

		public override void Hide()
		{
			base.Hide();
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_HIDE_CURVE, this.mAlphaCurve);
			this.mTargetPos = 0;
		}

		public virtual void RestartGameRequest()
		{
			Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.DoDialog(22, true, GlobalMembers._ID("RESTART GAME?", 3236), GlobalMembers._ID("Abandon the current game and start again? Your current game will be lost.", 3237), "", 1);
			int dialog_RESTART_GAME_WIDTH = ConstantsWP.DIALOG_RESTART_GAME_WIDTH;
			bej3Dialog.Resize(GlobalMembers.S(this.GetBoardCenterX()) - dialog_RESTART_GAME_WIDTH / 2, this.mHeight / 2, dialog_RESTART_GAME_WIDTH, bej3Dialog.GetPreferredHeight(dialog_RESTART_GAME_WIDTH));
			Bej3Button bej3Button = (Bej3Button)bej3Dialog.mYesButton;
			bej3Button.SetLabel(GlobalMembers._ID("RESTART GAME", 3238));
			bej3Dialog.SetButtonPosition(bej3Button, 0);
			bej3Button = (Bej3Button)bej3Dialog.mNoButton;
			bej3Button.SetLabel(GlobalMembers._ID("CANCEL", 3239));
			bej3Button.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			bej3Dialog.mDialogListener = this;
			bej3Dialog.mFlushPriority = 1;
			bej3Dialog.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
		}

		public void DoPrompt()
		{
			this.mTrialPromptShown = true;
			if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_BUTTERFLY)
			{
				Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.DoDialog(51, true, GlobalMembers._ID("PROMPT", 3789), string.Format(GlobalMembers._ID("In the trial version you can only reach a maximum of {0} points. Unlock the full version to enjoy the full experience.", 3797), SexyFramework.Common.CommaSeperate(95000)), "", 3);
				bej3Dialog.mDialogListener = this;
				((Bej3Button)bej3Dialog.mYesButton).SetLabel(GlobalMembers._ID("OK", 116));
				return;
			}
			if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_DIAMOND_MINE)
			{
				Bej3Dialog bej3Dialog2 = (Bej3Dialog)GlobalMembers.gApp.DoDialog(51, true, GlobalMembers._ID("PROMPT", 3789), string.Format(GlobalMembers._ID("In the trial version you can only reach a maximum of {0} points. Unlock the full version to enjoy the full experience.", 3797), SexyFramework.Common.CommaSeperate(50000)), "", 3);
				bej3Dialog2.mDialogListener = this;
				((Bej3Button)bej3Dialog2.mYesButton).SetLabel(GlobalMembers._ID("OK", 116));
				return;
			}
			if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_CLASSIC)
			{
				Bej3Dialog bej3Dialog3 = (Bej3Dialog)GlobalMembers.gApp.DoDialog(51, true, GlobalMembers._ID("PROMPT", 3789), GlobalMembers._ID("In the trial version you can only reach maximum Level of the 4th. Unlock the full version to enjoy the full experience.", 3800), "", 3);
				bej3Dialog3.mDialogListener = this;
				((Bej3Button)bej3Dialog3.mYesButton).SetLabel(GlobalMembers._ID("OK", 116));
			}
		}

		public virtual void MainMenuRequest()
		{
			Bej3Dialog bej3Dialog;
			if (!GlobalMembers.gApp.mMainMenu.mIsFullGame())
			{
				bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.DoDialog(50, true, GlobalMembers._ID("PROMPT", 3789), GlobalMembers._ID("Do you wish to go to main menu? Your game is not saved in the trial game.", 3799), "", 1);
			}
			else
			{
				bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.DoDialog(50, true, GlobalMembers._ID("PROMPT", 3789), GlobalMembers._ID("Do you wish to go to main menu? Your game will be saved.", 3790), "", 1);
			}
			int dialog_RESTART_GAME_WIDTH = ConstantsWP.DIALOG_RESTART_GAME_WIDTH;
			bej3Dialog.Resize(GlobalMembers.S(GlobalMembers.gApp.mBoard.GetBoardCenterX()) - dialog_RESTART_GAME_WIDTH / 2, this.mHeight / 2, dialog_RESTART_GAME_WIDTH, bej3Dialog.GetPreferredHeight(dialog_RESTART_GAME_WIDTH));
			Bej3Button bej3Button = (Bej3Button)bej3Dialog.mYesButton;
			bej3Button.SetLabel(GlobalMembers._ID("MAIN MENU", 3293));
			bej3Dialog.SetButtonPosition(bej3Button, 0);
			bej3Button = (Bej3Button)bej3Dialog.mNoButton;
			bej3Button.SetLabel(GlobalMembers._ID("CANCEL", 3239));
			bej3Button.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			bej3Dialog.mDialogListener = this;
			bej3Dialog.mFlushPriority = 1;
			bej3Dialog.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
		}

		public virtual bool WantFreezeAutoplay()
		{
			if (GlobalMembers.gApp.mAutoPlay == BejeweledLivePlusApp.EAutoPlayState.AUTOPLAY_TEST_HYPER)
			{
				return GlobalMembers.gApp.mAutoLevelUpCount >= GlobalMembers.M(300);
			}
			return this.mWantLevelup;
		}

		public virtual bool WriteUReplayCmd(int theCmd)
		{
			if (this.mInUReplay || !this.WantsWholeGameReplay())
			{
				return false;
			}
			byte b = (byte)theCmd;
			int num = this.mUpdateCnt - this.mUReplayLastTick;
			if (num > 255)
			{
				b |= 64;
			}
			else if (num > 0)
			{
				b |= 128;
			}
			this.mUReplayBuffer.WriteByte(b);
			if (num > 255)
			{
				this.mUReplayBuffer.WriteShort((short)num);
			}
			else if (num > 0)
			{
				this.mUReplayBuffer.WriteByte((byte)num);
			}
			this.mUReplayLastTick = this.mUpdateCnt;
			return true;
		}

		public virtual void InitUI()
		{
			if ((this.mUiConfig == Board.EUIConfig.eUIConfig_Standard || this.mUiConfig == Board.EUIConfig.eUIConfig_WithResetAndReplay || this.mUiConfig == Board.EUIConfig.eUIConfig_WithReset || this.mUiConfig == Board.EUIConfig.eUIConfig_StandardNoReplay) && this.mHintButton == null)
			{
				this.mHintButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG);
				this.mHintButton.SetLabel(GlobalMembers._ID("HINT", 3220));
				this.mHintButton.mPlayPressSound = false;
				this.AddWidget(this.mHintButton);
			}
			switch (this.mUiConfig)
			{
			case Board.EUIConfig.eUIConfig_WithReset:
			case Board.EUIConfig.eUIConfig_WithResetAndReplay:
			case Board.EUIConfig.eUIConfig_Quest:
				if (this.mResetButton == null)
				{
					this.mResetButton = new Bej3Button(2, this);
					this.AddWidget(this.mResetButton);
				}
				break;
			}
			if ((this.mUiConfig == Board.EUIConfig.eUIConfig_Standard || this.mUiConfig == Board.EUIConfig.eUIConfig_WithResetAndReplay) && this.mReplayButton == null)
			{
				this.mReplayButton = new Bej3Button(3, this);
				this.mReplayButton.mMouseVisible = false;
				this.mReplayButton.mBtnNoDraw = true;
				this.AddWidget(this.mReplayButton);
			}
		}

		public virtual void RefreshUI()
		{
			if (this.mUiConfig == Board.EUIConfig.eUIConfig_Standard || this.mUiConfig == Board.EUIConfig.eUIConfig_StandardNoReplay)
			{
				this.mHintButton.Resize(ConstantsWP.BOARD_UI_HINT_BTN_X, ConstantsWP.BOARD_UI_HINT_BTN_Y, ConstantsWP.BOARD_UI_HINT_BTN_WIDTH, 0);
				this.mHintButton.mHasAlpha = true;
				this.mHintButton.mDoFinger = true;
				this.mHintButton.mOverAlphaSpeed = 0.1;
				this.mHintButton.mOverAlphaFadeInSpeed = 0.2;
				this.mHintButton.mWidgetFlagsMod.mRemoveFlags |= 4;
				this.mHintButton.mDisabled = false;
				this.mHintButton.SetOverlayType(Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_NONE);
			}
			if (this.mReplayButton != null)
			{
				this.mReplayButton.Resize(GlobalMembers.IMGRECT_S(GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON, 0f, (float)this.GetBottomWidgetOffset()));
				this.mReplayButton.mButtonImage = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON;
				this.mReplayButton.mNormalRect = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON.GetCelRect(0);
				this.mReplayButton.mOverImage = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON;
				this.mReplayButton.mOverRect = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON.GetCelRect(1);
				this.mReplayButton.mDownImage = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON;
				this.mReplayButton.mDownRect = GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON.GetCelRect(1);
				this.mReplayButton.mHasAlpha = true;
				this.mReplayButton.mDoFinger = true;
				this.mReplayButton.mOverAlphaSpeed = 0.1;
				this.mReplayButton.mOverAlphaFadeInSpeed = 0.2;
				this.mReplayButton.mLabel = "";
			}
		}

		public void RemoveAllButtons()
		{
			this.RemoveWidget(this.mHintButton);
			if (this.mHintButton != null)
			{
				this.mHintButton.Dispose();
			}
			this.RemoveWidget(this.mReplayButton);
			if (this.mReplayButton != null)
			{
				this.mReplayButton.Dispose();
			}
			this.RemoveWidget(this.mResetButton);
			if (this.mResetButton != null)
			{
				this.mResetButton.Dispose();
			}
		}

		public virtual void Init()
		{
			this.mShouldUnloadContentWhenDone = true;
			while (this.mLightningStorms.Count > 0)
			{
				if (this.mLightningStorms[this.mLightningStorms.Count - 1] != null)
				{
					this.mLightningStorms[this.mLightningStorms.Count - 1].Dispose();
				}
				this.mLightningStorms.RemoveAt(this.mLightningStorms.Count - 1);
			}
			this.mMoveDataVector.Clear();
			this.mQueuedMoveVector.Clear();
			this.ClearAllPieces();
			this.mBoardColors[0] = new Color(6, 6, 6, GlobalMembers.M(160));
			this.mBoardColors[1] = new Color(24, 24, 24, GlobalMembers.M(160));
			this.mBoardUIOffsetY = 0;
			this.mGameFinished = false;
			this.mGameTicks = 0;
			this.mIdleTicks = 0;
			this.mGameOverCount = 0;
			this.mSettlingDelay = 0;
			this.mLastMatchTick = -1000;
			this.mLastMatchTime = 1000;
			this.mMatchTallyCount = 0;
			this.mLastMatchTally = 0;
			this.mUpdateCnt = 0;
			this.mSpeedNeedle = 50f;
			this.mSpeedBonusAlpha = 0f;
			this.mSpeedBonusPoints = 0;
			this.mSpeedModeFactor.SetConstant(3.0);
			this.mSpeedBonusNum = 0.0;
			this.mSpeedBonusCount = 0;
			this.mSpeedBonusCountHighest = 0;
			this.mSpeedBonusLastCount = 0;
			this.mSpeedBonusFlameModePct = 0f;
			this.mSpeedMedCount = 0;
			this.mSpeedHighCount = 0;
			this.mSpeedBonusPointsScale.SetConstant(0.0);
			this.mHypermixerCheckRow = 3;
			this.mLastWarningTick = 0;
			this.mCurMoveCreditId = 0;
			this.mCurMatchId = 0;
			this.mGemFallDelay = 0;
			this.mPointMultiplier = 1;
			this.mPoints = 0;
			this.mPointsBreakdown.Clear();
			this.AddPointBreakdownSection();
			this.mDispPoints = 0;
			this.mLevelBarPct = 0f;
			this.mCountdownBarPct = 0f;
			this.mLevelPointsTotal = 0;
			this.mLevel = 0;
			this.mScrambleUsesLeft = 2;
			this.mDeferredSounds.Clear();
			this.mComboCount = 0;
			this.mLastComboCount = 0;
			this.mComboCountDisp = 0f;
			this.mComboSelectorAngle = 22f;
			this.mLastPlayerSwapColor = -1;
			this.mMoneyDisp = GlobalMembers.gApp.GetCoinCount();
			this.mMoneyDispGoal = 0;
			this.mComboBonusSlowdownPct = 0f;
			this.mGemCountValueDisp = 0;
			this.mCascadeCountValueDisp = 0;
			this.mGemCountValueCheck = 0;
			this.mCascadeCountValueCheck = 0;
			this.mHintCooldownTicks = 0;
			this.mWantHintTicks = 0;
			this.mBoardDarken = 0f;
			this.mBoardDarkenAnnounce = 0f;
			this.mWarningGlowAlpha = 0f;
			this.mTimeExpired = false;
			this.mPointMultPosPct.SetConstant(1.0);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_TIMER_INFLATE, this.mTimerInflate);
			this.mTimerAlpha.SetConstant(1.0);
			for (int i = 0; i < 8; i++)
			{
				this.mBumpVelocities[i] = 0f;
				this.mNextColumnCredit[i] = -1;
			}
			this.InitUI();
			this.mZenDoBadgeAward = false;
			this.mDoRankUp = false;
			this.mBadgeManager.LinkBoard(this);
			this.mWantTimeAnnouncement = false;
			this.mTimeDelayCount = 0;
			this.mReadyDelayCount = 0;
			this.mGoDelayCount = 0;
			this.mLastMoveSave.Clear();
			this.mPreFXManager.Clear();
			this.mPostFXManager.Clear();
			while (this.mAnnouncements.Count > 0)
			{
				if (this.mAnnouncements[this.mAnnouncements.Count - 1] != null)
				{
					this.mAnnouncements[this.mAnnouncements.Count - 1].Dispose();
				}
				this.mAnnouncements.RemoveAt(this.mAnnouncements.Count - 1);
			}
			GlobalMembers.gGR.ClearOperationsFrom(0);
			this.mGoAnnouncementDone = false;
			this.mTimeAnnouncementDone = false;
			this.mTransitionBoardCurve.SetConstant(0.0);
			this.mSlidingHUDCurve.SetConstant(0.0);
			this.mTransBoardOffsetX = 0;
			this.mTransBoardOffsetY = 0;
			this.mTransLevelOffsetY = 0;
			this.mTransScoreOffsetY = 0;
			this.mTransDashboardOffsetY = 0;
			this.mTransReplayOffsetY = 0;
			this.mTransOptionsBtnOffsetX = 0;
			this.mTransHintBtnOffsetX = 0;
			this.mSuspendingGame = false;
			this.mIllegalMoveTutorial = false;
			this.mWantReplaySave = false;
			this.mForceReleaseButterfly = false;
			this.mForcedReleasedBflyPiece = null;
			this.mNOfIntentionalMatchesDuringCascade = 0;
		}

		public void ConfigureBarEmitters()
		{
			Rect rect;
			PILayer layer;
			PIDeflector pideflector;
			if (this.WantTopLevelBar())
			{
				rect = this.GetLevelBarRect();
				this.mLevelBarPIEffect.mEmitAfterTimeline = true;
				this.mLevelBarPIEffect.mDrawTransform.LoadIdentity();
				this.mLevelBarPIEffect.mDrawTransform.Translate((float)rect.mX, (float)rect.mY);
				layer = this.mLevelBarPIEffect.GetLayer(0);
				pideflector = layer.mLayerDef.mDeflectorVector[0];
				pideflector.mPoints[0].mValuePoint2DVector[0].mValue = new FPoint((float)rect.mWidth, 0f).ToXnaVector2();
				pideflector.mPoints[2].mValuePoint2DVector[0].mValue = new FPoint(0f, (float)rect.mHeight).ToXnaVector2();
				pideflector.mPoints[3].mValuePoint2DVector[0].mValue = new FPoint((float)rect.mWidth, (float)rect.mHeight).ToXnaVector2();
				layer = this.mLevelBarPIEffect.GetLayer(1);
				pideflector = layer.mLayerDef.mDeflectorVector[0];
				pideflector.mPoints[0].mValuePoint2DVector[0].mValue = new FPoint((float)rect.mWidth, 0f).ToXnaVector2();
				pideflector.mPoints[2].mValuePoint2DVector[0].mValue = new FPoint(0f, (float)rect.mHeight).ToXnaVector2();
				pideflector.mPoints[3].mValuePoint2DVector[0].mValue = new FPoint((float)rect.mWidth, (float)rect.mHeight).ToXnaVector2();
				return;
			}
			rect = this.GetCountdownBarRect();
			this.mCountdownBarPIEffect.mEmitAfterTimeline = true;
			this.mCountdownBarPIEffect.mDrawTransform.LoadIdentity();
			this.mCountdownBarPIEffect.mDrawTransform.Translate((float)rect.mX, (float)rect.mY);
			layer = this.mCountdownBarPIEffect.GetLayer(0);
			pideflector = layer.mLayerDef.mDeflectorVector[0];
			pideflector.mPoints[0].mValuePoint2DVector[0].mValue = new FPoint((float)rect.mWidth, 0f).ToXnaVector2();
			pideflector.mPoints[2].mValuePoint2DVector[0].mValue = new FPoint(0f, (float)rect.mHeight).ToXnaVector2();
			pideflector.mPoints[3].mValuePoint2DVector[0].mValue = new FPoint((float)rect.mWidth, (float)rect.mHeight).ToXnaVector2();
			layer = this.mCountdownBarPIEffect.GetLayer(1);
			pideflector = layer.mLayerDef.mDeflectorVector[0];
			pideflector.mPoints[0].mValuePoint2DVector[0].mValue = new FPoint((float)rect.mWidth, 0f).ToXnaVector2();
			pideflector.mPoints[2].mValuePoint2DVector[0].mValue = new FPoint(0f, (float)rect.mHeight).ToXnaVector2();
			pideflector.mPoints[3].mValuePoint2DVector[0].mValue = new FPoint((float)rect.mWidth, (float)rect.mHeight).ToXnaVector2();
		}

		public virtual Rect GetLevelBarRect()
		{
			if (this.WantTopLevelBar())
			{
				int boardCenterX = this.GetBoardCenterX();
				int theNum = (int)GlobalMembersResourcesWP.ImgYOfs(1092);
				Rect celRect = GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_BACK.GetCelRect(0);
				celRect.Offset(GlobalMembers.S(boardCenterX) - celRect.mWidth / 2, GlobalMembers.S(theNum));
				return celRect;
			}
			Rect result = new Rect(0, 0, 0, 0);
			return result;
		}

		public virtual Rect GetCountdownBarRect()
		{
			int boardCenterX = this.GetBoardCenterX();
			int theNum = this.GetBoardY() + 800 + GlobalMembers.M(30);
			Rect celRect = GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_BACK.GetCelRect(0);
			celRect.Offset(GlobalMembers.S(boardCenterX) - celRect.mWidth / 2, GlobalMembers.S(theNum) - celRect.mHeight / 2);
			return celRect;
		}

		public Piece GetTutorialIrisPiece()
		{
			if (this.mDeferredTutorialVector.Count > 0)
			{
				return this.GetPieceById(this.mDeferredTutorialVector[0].mPieceId);
			}
			return null;
		}

		public virtual string GetSavedGameName()
		{
			return string.Empty;
		}

		public virtual string GetMusicName()
		{
			return "Classic";
		}

		public virtual bool AllowNoMoreMoves()
		{
			return this.mLevel != 0 && this.mSpeedBonusFlameModePct == 0f;
		}

		public virtual bool AllowSpeedBonus()
		{
			return true;
		}

		public virtual bool AllowPowerups()
		{
			return true;
		}

		public virtual bool AllowLaserGems()
		{
			return true;
		}

		public virtual bool AllowHints()
		{
			return true;
		}

		public virtual bool AllowTooltips()
		{
			return false;
		}

		public virtual bool HasLargeExplosions()
		{
			return false;
		}

		public virtual bool ForceSwaps()
		{
			return false;
		}

		public virtual bool CanPlay()
		{
			if (this.mAnnouncements.Count > 0)
			{
				foreach (Announcement announcement in this.mAnnouncements)
				{
					if (announcement.mBlocksPlay)
					{
						return false;
					}
				}
			}
			if (this.mDeferredTutorialVector.Count > 0)
			{
				return false;
			}
			if (!this.mHasBoardSettled)
			{
				return false;
			}
			if (this.mReadyDelayCount != 0)
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
			if (this.GetTicksLeft() == 0)
			{
				return false;
			}
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && piece.IsFlagSet(96U) && piece.mCounter == 0)
					{
						return false;
					}
				}
			}
			return this.mHyperspace == null;
		}

		public virtual bool WantsBackground()
		{
			return true;
		}

		public virtual bool WantsLevelBasedBackground()
		{
			return false;
		}

		public virtual bool IsGameSuspended()
		{
			if (this.mInUReplay)
			{
				return (this.mUReplayGameFlags & 2) != 0;
			}
			return this.mReadyDelayCount > 0 || this.mScrambleDelayTicks > 0 || this.mTimeExpired || this.mLevelCompleteCount != 0 || GlobalMembers.gApp.GetDialog(18) != null || GlobalMembers.gApp.GetDialog(39) != null || this.mLightningStorms.Count != 0;
		}

		public virtual bool CanPiecesFall()
		{
			if (this.mInUReplay)
			{
				return (this.mUReplayGameFlags & 1) != 0;
			}
			return GlobalMembers.gApp.GetDialog(18) == null && this.mGemFallDelay == 0 && this.mLightningStorms.Count == 0 && this.mHyperspace == null;
		}

		public virtual bool IsGamePaused()
		{
			return (this.mUserPaused || !GlobalMembers.gApp.mHasFocus || GlobalMembers.gApp.GetDialog(21) != null || GlobalMembers.gApp.GetDialog(22) != null) && this.WantsHideOnPause() && !this.mInReplay;
		}

		public virtual int GetTimeLimit()
		{
			return 0;
		}

		public virtual int GetTimeDrawX()
		{
			return GlobalMembers.S(this.GetBoardCenterX());
		}

		public virtual int GetHintTime()
		{
			return 15;
		}

		public virtual bool WantsHideOnPause()
		{
			return this.GetTimeLimit() != 0;
		}

		public virtual bool WantHypermixerEdgeCheck()
		{
			return false;
		}

		public virtual bool WantHypermixerBottomCheck()
		{
			return true;
		}

		public virtual bool WantAnnihilatorReplacement()
		{
			return false;
		}

		public virtual bool SupportsReplays()
		{
			return false;
		}

		public virtual bool WantsWholeGameReplay()
		{
			return false;
		}

		public virtual bool WantsCalmEffects()
		{
			return false;
		}

		public virtual bool WantsTutorialReplays()
		{
			return true;
		}

		public virtual int GetGemCountPopupThreshold()
		{
			return 10;
		}

		public virtual int GetMinComplementLevel()
		{
			return 0;
		}

		public virtual float GetGravityFactor()
		{
			return (float)(1.0 + (this.mSpeedModeFactor - 1.0) * (double)GlobalMembers.M(0.65f));
		}

		public virtual float GetSwapSpeed()
		{
			return (float)this.mSpeedModeFactor;
		}

		public virtual float GetMatchSpeed()
		{
			return (float)this.mSpeedModeFactor;
		}

		public virtual float GetGameSpeed()
		{
			return 1f;
		}

		public virtual float GetSpeedModeFactorScale()
		{
			return 1f;
		}

		public virtual float GetModePointMultiplier()
		{
			return 1f;
		}

		public virtual float GetRankPointMultiplier()
		{
			return this.GetModePointMultiplier();
		}

		public virtual string GetTopWidgetButtonText()
		{
			return string.Empty;
		}

		public virtual int GetBottomWidgetOffset()
		{
			return 0;
		}

		public virtual bool WantColorCombos()
		{
			return false;
		}

		public virtual int WantExpandedTopWidget()
		{
			return 0;
		}

		public virtual bool WantHyperMixers()
		{
			return false;
		}

		public virtual bool WantBulgeCascades()
		{
			return true;
		}

		public virtual bool WantDrawTimer()
		{
			return true;
		}

		public virtual bool WantsTutorial(int theTutorialFlag)
		{
			for (int i = 0; i < this.mDeferredTutorialVector.Count; i++)
			{
				if (this.mDeferredTutorialVector[i].mTutorialFlag == theTutorialFlag)
				{
					return false;
				}
			}
			return !GlobalMembers.gApp.HasClearedTutorial(theTutorialFlag) && this.mSpeedBonusFlameModePct == 0f && !this.mTimeExpired;
		}

		public virtual bool WantAutoload()
		{
			return true;
		}

		public virtual void HypermixerDropped()
		{
		}

		public virtual HYPERSPACETRANS GetHyperspaceTransType()
		{
			return HYPERSPACETRANS.HYPERSPACETRANS_Classic;
		}

		public bool CheckLoad()
		{
			bool flag = true;
			return flag && this.LoadGame();
		}

		public static void ParseGridLayout(string theStr, List<GridData> outGrid, bool theEnforceStdGridSize)
		{
			int num = 0;
			for (int i = 0; i < theStr.Length; i++)
			{
				if (char.IsLetter(theStr.get_Chars(i)))
				{
					GridData gridData = outGrid[outGrid.Count - 1];
					if (num == 0)
					{
						gridData = outGrid[outGrid.Count - 1];
						num = gridData.mTiles.Count - 1;
					}
					gridData.At((num - 1) / 8, (num - 1) % 8).mAttr = theStr.get_Chars(i);
				}
				else if (char.IsDigit(theStr.get_Chars(i)))
				{
					if (theEnforceStdGridSize && (outGrid.Count == 0 || num >= 64))
					{
						num = 0;
						outGrid.Add(new GridData());
						for (int j = 0; j < 64; j++)
						{
							outGrid[outGrid.Count - 1].mTiles.Add(new GridData.TileData());
						}
					}
					else if (!theEnforceStdGridSize && (outGrid.Count == 0 || num / 8 >= outGrid[outGrid.Count - 1].GetRowCount()))
					{
						if (outGrid.Count == 0)
						{
							outGrid.Add(new GridData());
						}
						outGrid[outGrid.Count - 1].AddRow();
					}
					string text = theStr.get_Chars(i).ToString();
					int mBack = 0;
					int.TryParse(text, ref mBack);
					outGrid[outGrid.Count - 1].At(num / 8, num % 8).mBack = (uint)mBack;
					num++;
				}
			}
		}

		public void ReloadConfig()
		{
			GlobalMembers.gApp.LoadConfigs();
			if (this.mMessager != null)
			{
				this.mMessager.AddMessage("Reloaded configs");
			}
		}

		public void ReplayGame()
		{
			string savedGameName = this.GetSavedGameName();
			if (string.IsNullOrEmpty(savedGameName))
			{
				return;
			}
			Serialiser theBuffer = new Serialiser();
			if (!GlobalMembers.gApp.ReadBufferFromStorage(GlobalMembers.gApp.mProfile.GetProfileDir(GlobalMembers.gApp.mProfile.mProfileName) + "\\" + savedGameName, theBuffer))
			{
				return;
			}
			this.LoadGame(theBuffer, false);
		}

		public virtual void NewGame()
		{
			this.NewGame(false);
		}

		public virtual void NewGame(bool restartingGame)
		{
			BejeweledLivePlusApp.mAllowRating = true;
			this.mUserPaused = false;
			this.mVisPausePct = 0f;
			uint randSeedOverride = this.GetRandSeedOverride();
			if (randSeedOverride != 0U)
			{
				this.mRand.SRand(randSeedOverride);
			}
			for (int i = 0; i < 40; i++)
			{
				this.mGameStats[i] = 0;
			}
			if (this.mParams.ContainsKey("BackgroundIdx"))
			{
				int num = 0;
				int.TryParse(this.mParams["BackgroundIdx"], ref num);
				this.mBackgroundIdx = num;
			}
			if (this.mParams.ContainsKey("BackgroundIdxSet"))
			{
				Utils.SplitAndConvertStr(this.mParams["BackgroundIdxSet"], this.mBackgroundIdxSet);
			}
			this.mBackgroundIdx %= GlobalMembers.gBackgroundNames.Length;
			if (this.WantsLevelBasedBackground())
			{
				if (!restartingGame || this.mBackgroundIdx != 0)
				{
					this.mBackgroundIdx = -1;
					this.SetupBackground(1);
				}
			}
			else
			{
				this.SetupBackground();
			}
			this.ConfigureBarEmitters();
			this.mHasBoardSettled = false;
			this.mContinuedFromLoad = false;
			if (this.WantAutoload())
			{
				if (this.CheckLoad())
				{
					this.mContinuedFromLoad = true;
				}
				else
				{
					this.NewCombo();
					this.FillInBlanks(false, true);
				}
			}
			else
			{
				this.NewCombo();
				this.FillInBlanks(false);
			}
			if (!this.mContinuedFromLoad)
			{
				GlobalMembers.gApp.LogStatString(string.Format("GameStart Title=\"{0}\"", this.GetLoggingGameName()));
				if (this.GetTimeLimit() >= 0)
				{
					this.mTimeDelayCount = 1;
				}
				else
				{
					this.mTimeDelayCount = 0;
				}
				this.mReadyDelayCount = GlobalMembers.M(0);
				this.mGoDelayCount = GlobalMembers.M(25);
			}
			else
			{
				this.mTimeDelayCount = 0;
				if (this.WantsHideOnPause())
				{
					this.mReadyDelayCount = GlobalMembers.M(120);
					this.mGoDelayCount = GlobalMembers.M(120);
				}
				else
				{
					this.mGoDelayCount = GlobalMembers.M(25);
				}
			}
			this.mSettlingDelay = 0;
			this.mSuspendingGame = false;
		}

		public virtual bool SaveGame(Serialiser theBuffer)
		{
			if (GlobalMembers.gApp != null && GlobalMembers.gApp.mProfile != null)
			{
				if (this.mGameClosing)
				{
					this.SyncUnAwardedBadges(GlobalMembers.gApp.mProfile.mDeferredBadgeVector);
				}
				if (this.mGameClosing && !this.IsGameIdle())
				{
					GlobalMembers.gApp.mProfile.mStats.Swap(false);
					GlobalMembers.gApp.mProfile.WriteProfile();
				}
				else
				{
					GlobalMembers.gApp.mProfile.mStats.Swap(true);
				}
			}
			if (this.mLightningStorms.Count != 0 || this.mHyperspace != null)
			{
				return false;
			}
			theBuffer.Clear();
			theBuffer.WriteFileHeader(101, (int)GlobalMembers.gApp.mCurrentGameMode);
			int chunkBeginLoc = theBuffer.WriteGameChunkHeader(GameChunkId.eChunkBoard);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardUpdateCnt, this.mUpdateCnt);
			theBuffer.WriteSpecialBlock(Serialiser.PairID.BoardPieces, 64);
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					theBuffer.WriteBoolean(piece != null);
					if (piece != null)
					{
						if (piece.IsFlagSet(2U))
						{
							int mMatchId = piece.mMatchId;
						}
						theBuffer.WriteInt32(i);
						theBuffer.WriteInt32(j);
						theBuffer.WriteInt32(piece.mId);
						piece.Save(theBuffer);
					}
				}
			}
			theBuffer.WriteArrayPair(Serialiser.PairID.BoardBumpVelocities, 8, this.mBumpVelocities);
			theBuffer.WriteArrayPair(Serialiser.PairID.BoardNextColumnCredit, 8, this.mNextColumnCredit);
			string str = this.mRand.Serialize();
			theBuffer.WriteStringPair(Serialiser.PairID.BoardRand, str);
			theBuffer.WriteSpecialBlock(Serialiser.PairID.BoardSwapData, this.mSwapDataVector.Count);
			this.SaveSwapData(theBuffer, this.mSwapDataVector);
			theBuffer.WriteSpecialBlock(Serialiser.PairID.BoardMoveData, this.mMoveDataVector.Count);
			this.SaveMoveData(theBuffer, this.mMoveDataVector, 103);
			theBuffer.WriteSpecialBlock(Serialiser.PairID.BoardQueuedMoves, this.mQueuedMoveVector.Count);
			this.SaveQueuedMoves(theBuffer, this.mQueuedMoveVector);
			theBuffer.WriteArrayPair(Serialiser.PairID.BoardGameStats, 40, this.mGameStats);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardPoints, this.mPoints);
			theBuffer.WriteSpecialBlock(Serialiser.PairID.BoardPointsBreakdown, this.mPointsBreakdown.Count, 5);
			for (int k = 0; k < this.mPointsBreakdown.Count; k++)
			{
				for (int l = 0; l < 5; l++)
				{
					theBuffer.WriteInt32(this.mPointsBreakdown[k][l]);
				}
			}
			theBuffer.WriteValuePair(Serialiser.PairID.BoardMoneyDisp, this.mMoneyDisp);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardLevelBarPct, this.mLevelBarPct);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardCountdownBarPct, this.mCountdownBarPct);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardLevelPointsTotal, this.mLevelPointsTotal);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardLevel, this.mLevel);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardPointMultiplier, this.mPointMultiplier);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardCurMoveCreditId, this.mCurMoveCreditId);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardCurMatchId, this.mCurMatchId);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardGemFallDelay, this.mGemFallDelay);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardMoveCounter, this.mMoveCounter);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardGameTicks, this.mGameTicks);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardIdleTicks, this.mIdleTicks);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardLastMatchTick, this.mLastMatchTick);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardLastMatchTime, this.mLastMatchTime);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardMatchTallyCount, this.mMatchTallyCount);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedModeFactor, this.mSpeedModeFactor);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedBonusAlpha, this.mSpeedBonusAlpha);
			theBuffer.WriteStringPair(Serialiser.PairID.BoardSpeedBonusText, this.mSpeedBonusText);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedometerPopup, this.mSpeedometerPopup);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedometerGlow, this.mSpeedometerGlow);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedBonusDisp, this.mSpeedBonusDisp);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedNeedle, this.mSpeedNeedle);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedBonusPoints, this.mSpeedBonusPoints);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedBonusNum, this.mSpeedBonusNum);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedBonusCount, this.mSpeedBonusCount);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedBonusCountHighest, this.mSpeedBonusCountHighest);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardSpeedBonusLastCount, this.mSpeedBonusLastCount);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardHasBoardSettled, this.mHasBoardSettled);
			theBuffer.WriteArrayPair(Serialiser.PairID.BoardComboColors, 5, this.mComboColors);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardComboCount, this.mComboCount);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardComboLen, this.mComboLen);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardComboCountDisp, this.mComboCountDisp);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardComboFlashPct, this.mComboFlashPct);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardLastPlayerSwapColor, this.mLastPlayerSwapColor);
			if (this.mUpdateCnt > 0)
			{
				this.SaveReplayData(theBuffer, this.mWholeGameReplay);
			}
			theBuffer.WriteValuePair(Serialiser.PairID.BoardNextPieceId, this.mNextPieceId);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardScrambleDelayTicks, this.mScrambleDelayTicks);
			theBuffer.WriteValuePair(Serialiser.PairID.BoardGameFinished, this.mGameFinished);
			theBuffer.FinalizeGameChunkHeader(chunkBeginLoc);
			bool result = this.SaveGameExtra(theBuffer);
			theBuffer.FinalizeFileHeader();
			return result;
		}

		public virtual bool LoadGame(Serialiser theBuffer)
		{
			return this.LoadGame(theBuffer, true);
		}

		public virtual bool LoadGame(Serialiser theBuffer, bool resetReplay)
		{
			this.mInLoadSave = true;
			theBuffer.SeekFront();
			int num;
			int num2;
			int num3;
			if (!theBuffer.ReadFileHeader(out num, out num2, out num3))
			{
				this.mInLoadSave = false;
				return false;
			}
			int num4 = 0;
			GameChunkHeader header = new GameChunkHeader();
			if (!theBuffer.CheckReadGameChunkHeader(GameChunkId.eChunkBoard, header, out num4))
			{
				this.mInLoadSave = false;
				return false;
			}
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					if (piece != null)
					{
						piece.mTallied = true;
						this.DeletePiece(piece);
					}
				}
			}
			theBuffer.ReadValuePair(out this.mUpdateCnt);
			int num5 = 0;
			this.mPreFXManager.Clear();
			this.mPostFXManager.Clear();
			for (int k = 0; k < 8; k++)
			{
				for (int l = 0; l < 8; l++)
				{
					Piece piece2;
					if ((piece2 = this.mBoard[k, l]) != null)
					{
						piece2.release();
						this.mBoard[k, l] = null;
					}
				}
			}
			int num6;
			theBuffer.ReadSpecialBlock(out num6);
			for (int m = 0; m < num6; m++)
			{
				bool flag = theBuffer.ReadBoolean();
				if (flag)
				{
					int num7 = theBuffer.ReadInt32();
					int num8 = theBuffer.ReadInt32();
					int id = theBuffer.ReadInt32();
					Piece piece2 = Piece.alloc(this, id);
					num5 = Math.Max(num5, piece2.mId);
					piece2.mCol = num8;
					piece2.mRow = num7;
					piece2.Load(theBuffer, num);
					this.mBoard[num7, num8] = piece2;
					this.StartPieceEffect(piece2);
				}
			}
			this.mBumpVelocities = new float[8];
			theBuffer.ReadArrayPair(8, this.mBumpVelocities);
			this.mNextColumnCredit = new int[8];
			theBuffer.ReadArrayPair(8, this.mNextColumnCredit);
			string theSerialData;
			theBuffer.ReadStringPair(out theSerialData);
			this.mRand.SRand(theSerialData);
			int num9;
			theBuffer.ReadSpecialBlock(out num9);
			this.LoadSwapData(theBuffer, this.mSwapDataVector, num9);
			theBuffer.ReadSpecialBlock(out num9);
			this.LoadMoveData(theBuffer, this.mMoveDataVector, num9, num);
			theBuffer.ReadSpecialBlock(out num9);
			this.LoadQueuedMoves(theBuffer, this.mQueuedMoveVector, num9);
			this.mNextPieceId = num5 + 1;
			this.mGameStats = new int[40];
			theBuffer.ReadArrayPair(40, this.mGameStats);
			theBuffer.ReadValuePair(out this.mPoints);
			this.mDispPoints = this.mPoints;
			int num10;
			theBuffer.ReadSpecialBlock(out num9, out num10);
			this.mPointsBreakdown = new List<List<int>>();
			for (int n = 0; n < num9; n++)
			{
				this.mPointsBreakdown.Add(new List<int>());
				for (int num11 = 0; num11 < 5; num11++)
				{
					this.mPointsBreakdown[n].Add(0);
				}
			}
			for (int num12 = 0; num12 < num9; num12++)
			{
				for (int num13 = 0; num13 < num10; num13++)
				{
					this.mPointsBreakdown[num12][num13] = (int)theBuffer.ReadLong();
				}
			}
			theBuffer.ReadValuePair(out this.mMoneyDisp);
			this.mMoneyDispGoal = this.mMoneyDisp;
			theBuffer.ReadValuePair(out this.mLevelBarPct);
			theBuffer.ReadValuePair(out this.mCountdownBarPct);
			theBuffer.ReadValuePair(out this.mLevelPointsTotal);
			theBuffer.ReadValuePair(out this.mLevel);
			theBuffer.ReadValuePair(out this.mPointMultiplier);
			theBuffer.ReadValuePair(out this.mCurMoveCreditId);
			theBuffer.ReadValuePair(out this.mCurMatchId);
			theBuffer.ReadValuePair(out this.mGemFallDelay);
			theBuffer.ReadValuePair(out this.mMoveCounter);
			theBuffer.ReadValuePair(out this.mGameTicks);
			theBuffer.ReadValuePair(out this.mIdleTicks);
			theBuffer.ReadValuePair(out this.mLastMatchTick);
			theBuffer.ReadValuePair(out this.mLastMatchTime);
			theBuffer.ReadValuePair(out this.mMatchTallyCount);
			theBuffer.ReadValuePair(out this.mSpeedModeFactor);
			theBuffer.ReadValuePair(out this.mSpeedBonusAlpha);
			theBuffer.ReadStringPair(out this.mSpeedBonusText);
			theBuffer.ReadValuePair(out this.mSpeedometerPopup);
			theBuffer.ReadValuePair(out this.mSpeedometerGlow);
			theBuffer.ReadValuePair(out this.mSpeedBonusDisp);
			theBuffer.ReadValuePair(out this.mSpeedNeedle);
			theBuffer.ReadValuePair(out this.mSpeedBonusPoints);
			theBuffer.ReadValuePair(out this.mSpeedBonusNum);
			theBuffer.ReadValuePair(out this.mSpeedBonusCount);
			theBuffer.ReadValuePair(out this.mSpeedBonusCountHighest);
			theBuffer.ReadValuePair(out this.mSpeedBonusLastCount);
			theBuffer.ReadValuePair(out this.mHasBoardSettled);
			this.mComboColors = new int[5];
			theBuffer.ReadArrayPair(5, this.mComboColors);
			theBuffer.ReadValuePair(out this.mComboCount);
			theBuffer.ReadValuePair(out this.mComboLen);
			theBuffer.ReadValuePair(out this.mComboCountDisp);
			theBuffer.ReadValuePair(out this.mComboFlashPct);
			theBuffer.ReadValuePair(out this.mLastPlayerSwapColor);
			if (this.mUpdateCnt > 0)
			{
				this.LoadReplayData(theBuffer, this.mWholeGameReplay);
			}
			else
			{
				this.mWholeGameReplay.mReplayMoves.Clear();
			}
			while (this.mLightningStorms.Count > 0)
			{
				if (this.mLightningStorms[this.mLightningStorms.Count - 1] != null)
				{
					this.mLightningStorms[this.mLightningStorms.Count - 1].Dispose();
				}
				this.mLightningStorms.RemoveAt(this.mLightningStorms.Count - 1);
			}
			while (this.mAnnouncements.Count > 0)
			{
				if (this.mAnnouncements[this.mAnnouncements.Count - 1] != null)
				{
					this.mAnnouncements[this.mAnnouncements.Count - 1].Dispose();
				}
				this.mAnnouncements.RemoveAt(this.mAnnouncements.Count - 1);
			}
			this.mPrevPointMultAlpha.SetConstant(0.0);
			this.mPointMultPosPct.SetConstant(1.0);
			this.mPointMultTextMorph.SetConstant(0.0);
			this.mPointMultScale.SetConstant(1.0);
			this.mPointMultAlpha.SetConstant(0.0);
			this.mPointMultYAdd.SetConstant(0.0);
			this.mTimerInflate.SetConstant(0.0);
			this.mTimerAlpha.SetConstant(1.0);
			this.mGemCountValueDisp = 0;
			this.mGemCountValueCheck = 0;
			this.mGemCountAlpha.SetConstant(0.0);
			this.mGemScalarAlpha.SetConstant(0.0);
			this.mGemCountCurve.SetConstant(0.0);
			this.mCascadeCountValueDisp = 0;
			this.mCascadeCountValueCheck = 0;
			this.mCascadeCountAlpha.SetConstant(0.0);
			this.mCascadeScalarAlpha.SetConstant(0.0);
			this.mCascadeCountCurve.SetConstant(0.0);
			this.mComplementAlpha.SetConstant(0.0);
			this.mComplementScale.SetConstant(0.0);
			this.mComplementNum = -1;
			this.mLastComplement = -1;
			this.mSideXOff.SetConstant(0.0);
			this.mSideAlpha.SetConstant(1.0);
			this.mScale.SetConstant(1.0);
			this.mAlpha.SetConstant(1.0);
			int num14;
			theBuffer.ReadValuePair(out num14);
			this.mNextPieceId = Math.Max(num14, num5 + 1);
			theBuffer.ReadValuePair(out this.mScrambleDelayTicks);
			theBuffer.ReadValuePair(out this.mGameFinished);
			this.mWantHintTicks = 0;
			this.mDeferredTutorialVector.Clear();
			GlobalMembers.KILL_WIDGET(this.mHyperspace);
			this.mHyperspace = null;
			this.mWantLevelup = false;
			this.LoadGameExtra(theBuffer);
			if (resetReplay)
			{
				this.HideReplayWidget();
				this.mReplayWidgetShowPct.SetConstant(0.0);
				this.mHasReplayData = false;
			}
			GlobalMembers.gApp.LogStatString(string.Format("GameLoaded Title=\"{0}\" Level={1} Misc.Points={2}", this.GetLoggingGameName(), this.mLevel, this.mPoints));
			this.mLevelBarPIEffect.Update();
			if (this.WantsLevelBasedBackground() && this.mBackgroundIdx != this.mLevel)
			{
				this.mBackgroundIdx = this.mLevel - 1;
				this.SetupBackground(1);
			}
			this.mInLoadSave = false;
			return true;
		}

		public bool LoadGame()
		{
			string savedGameName = this.GetSavedGameName();
			if (string.IsNullOrEmpty(savedGameName))
			{
				return false;
			}
			Serialiser theBuffer = new Serialiser();
			return GlobalMembers.gApp.ReadBufferFromStorage(GlobalMembers.gApp.mProfile.GetProfileDir(GlobalMembers.gApp.mProfile.mProfileName) + "\\" + savedGameName, theBuffer) && this.LoadGame(theBuffer);
		}

		public bool HasSavedGame()
		{
			string savedGameName = this.GetSavedGameName();
			return !string.IsNullOrEmpty(savedGameName) && BejeweledLivePlus.Misc.Common.FileExists(GlobalMembers.gApp.mProfile.GetProfileDir(GlobalMembers.gApp.mProfile.mProfileName) + "\\" + savedGameName);
		}

		public bool DeleteSavedGame()
		{
			string savedGameName = this.GetSavedGameName();
			return !string.IsNullOrEmpty(savedGameName) && GlobalMembers.gApp.mFileDriver.DeleteFile(GlobalMembers.gApp.mProfile.GetProfileDir(GlobalMembers.gApp.mProfile.mProfileName) + "\\" + savedGameName);
		}

		public bool SaveGame()
		{
			this.mInLoadSave = true;
			string savedGameName = this.GetSavedGameName();
			if (string.IsNullOrEmpty(savedGameName))
			{
				this.mInLoadSave = false;
				return false;
			}
			Serialiser theBuffer = new Serialiser();
			if (!this.SafeSaveGame(theBuffer))
			{
				this.mInLoadSave = false;
				return false;
			}
			bool result = GlobalMembers.gApp.WriteBufferToFile(GlobalMembers.gApp.mProfile.GetProfileDir(GlobalMembers.gApp.mProfile.mProfileName) + "\\" + savedGameName, theBuffer);
			this.mInLoadSave = false;
			return result;
		}

		public virtual bool SaveGameExtra(Serialiser theBuffer)
		{
			return true;
		}

		public virtual void LoadGameExtra(Serialiser theBuffer)
		{
		}

		public bool SaveSwapData(Serialiser theBuffer, List<SwapData> theSwapDataVector)
		{
			for (int i = 0; i < theSwapDataVector.Count; i++)
			{
				SwapData swapData = theSwapDataVector[i];
				theBuffer.WriteInt32((swapData.mPiece1 != null) ? swapData.mPiece1.mId : 0);
				theBuffer.WriteInt32((swapData.mPiece2 != null) ? swapData.mPiece2.mId : 0);
				theBuffer.WriteInt32(swapData.mSwapDir.mX);
				theBuffer.WriteInt32(swapData.mSwapDir.mY);
				theBuffer.WriteCurvedVal(swapData.mSwapPct);
				theBuffer.WriteCurvedVal(swapData.mGemScale);
				theBuffer.WriteBoolean(swapData.mForwardSwap);
				theBuffer.WriteInt32(swapData.mHoldingSwap);
				theBuffer.WriteBoolean(swapData.mIgnore);
				theBuffer.WriteBoolean(swapData.mForceSwap);
				theBuffer.WriteBoolean(swapData.mDestroyTarget);
			}
			return true;
		}

		public bool LoadSwapData(Serialiser theBuffer, List<SwapData> theSwapDataVector, int size)
		{
			theSwapDataVector.Clear();
			for (int i = 0; i < size; i++)
			{
				SwapData swapData = new SwapData();
				swapData.mPiece1 = this.GetPieceById(theBuffer.ReadInt32());
				swapData.mPiece2 = this.GetPieceById(theBuffer.ReadInt32());
				swapData.mSwapDir.mX = theBuffer.ReadInt32();
				swapData.mSwapDir.mY = theBuffer.ReadInt32();
				swapData.mSwapPct = theBuffer.ReadCurvedVal();
				swapData.mGemScale = theBuffer.ReadCurvedVal();
				swapData.mForwardSwap = theBuffer.ReadBoolean();
				swapData.mHoldingSwap = theBuffer.ReadInt32();
				swapData.mIgnore = theBuffer.ReadBoolean();
				swapData.mForceSwap = theBuffer.ReadBoolean();
				swapData.mDestroyTarget = theBuffer.ReadBoolean();
				theSwapDataVector.Add(swapData);
			}
			return true;
		}

		public bool SaveMoveData(Serialiser theBuffer, List<MoveData> theMoveDataVector, int saveGameVersion)
		{
			for (int i = 0; i < (int)((short)theMoveDataVector.Count); i++)
			{
				MoveData moveData = theMoveDataVector[i];
				theBuffer.WriteInt32(moveData.mUpdateCnt);
				theBuffer.WriteInt32(moveData.mSelectedId);
				theBuffer.WriteInt32(moveData.mSwappedRow);
				theBuffer.WriteInt32(moveData.mSwappedCol);
				theBuffer.WriteInt32(moveData.mMoveCreditId);
				if (saveGameVersion > 102)
				{
					theBuffer.WriteInt32(40);
				}
				for (int j = 0; j < 40; j++)
				{
					theBuffer.WriteInt32(moveData.mStats[j]);
				}
			}
			return true;
		}

		public bool LoadMoveData(Serialiser theBuffer, List<MoveData> theMoveDataVector, int size, int saveGameVersion)
		{
			theMoveDataVector.Clear();
			for (int i = 0; i < size; i++)
			{
				MoveData moveData = new MoveData();
				moveData.mUpdateCnt = theBuffer.ReadInt32();
				moveData.mSelectedId = theBuffer.ReadInt32();
				moveData.mSwappedRow = theBuffer.ReadInt32();
				moveData.mSwappedCol = theBuffer.ReadInt32();
				moveData.mMoveCreditId = theBuffer.ReadInt32();
				if (saveGameVersion > 102)
				{
					int num = theBuffer.ReadInt32();
					for (int j = 0; j < num; j++)
					{
						moveData.mStats[j] = theBuffer.ReadInt32();
					}
					for (int k = num; k < 40; k++)
					{
						moveData.mStats[k] = 0;
					}
				}
				else
				{
					for (int l = 0; l < 38; l++)
					{
						moveData.mStats[l] = theBuffer.ReadInt32();
					}
					for (int m = 38; m < 40; m++)
					{
						moveData.mStats[m] = 0;
					}
				}
				theMoveDataVector.Add(moveData);
			}
			return true;
		}

		public bool SaveQueuedMoves(Buffer theBuffer, List<QueuedMove> theQueuedMoves)
		{
			for (int i = 0; i < theQueuedMoves.Count; i++)
			{
				QueuedMove queuedMove = theQueuedMoves[i];
				theBuffer.WriteInt32(queuedMove.mUpdateCnt);
				theBuffer.WriteInt32(queuedMove.mSelectedId);
				theBuffer.WriteInt32(queuedMove.mSwappedCol);
				theBuffer.WriteInt32(queuedMove.mSwappedRow);
				theBuffer.WriteBoolean(queuedMove.mForceSwap);
				theBuffer.WriteBoolean(queuedMove.mPlayerSwapped);
				theBuffer.WriteBoolean(queuedMove.mDestroyTarget);
			}
			return true;
		}

		public bool LoadQueuedMoves(Buffer theBuffer, List<QueuedMove> theQueuedMoves, int size)
		{
			theQueuedMoves.Clear();
			for (int i = 0; i < size; i++)
			{
				theQueuedMoves.Add(new QueuedMove
				{
					mUpdateCnt = theBuffer.ReadInt32(),
					mSelectedId = theBuffer.ReadInt32(),
					mSwappedCol = theBuffer.ReadInt32(),
					mSwappedRow = theBuffer.ReadInt32(),
					mForceSwap = theBuffer.ReadBoolean(),
					mPlayerSwapped = theBuffer.ReadBoolean(),
					mDestroyTarget = theBuffer.ReadBoolean()
				});
			}
			return true;
		}

		public bool SaveReplayData(Serialiser theBuffer, ReplayData theReplayData)
		{
			theBuffer.WriteValuePair(Serialiser.PairID.ReplayVersion, 1);
			string str = "BlitzDeluxe";
			theBuffer.WriteStringPair(Serialiser.PairID.ReplayID, str);
			theBuffer.WriteBufferPair(Serialiser.PairID.ReplaySaveBuffer, theReplayData.mSaveBuffer);
			if (theReplayData.mSaveBuffer.GetDataLen() == 0)
			{
				return false;
			}
			theBuffer.WriteSpecialBlock(Serialiser.PairID.ReplayQueuedMoves, theReplayData.mReplayMoves.Count);
			this.SaveQueuedMoves(theBuffer, theReplayData.mReplayMoves);
			theBuffer.WriteValuePair(Serialiser.PairID.ReplayTutorialFlags, theReplayData.mTutorialFlags);
			theBuffer.WriteSpecialBlock(Serialiser.PairID.ReplayStateInfo, theReplayData.mStateInfoVector.Count);
			for (int i = 0; i < theReplayData.mStateInfoVector.Count; i++)
			{
				StateInfo stateInfo = theReplayData.mStateInfoVector[i];
				theBuffer.WriteInt32(stateInfo.mUpdateCnt);
				theBuffer.WriteInt32(stateInfo.mPoints);
				theBuffer.WriteInt32(stateInfo.mMoneyDisp);
				theBuffer.WriteInt32(stateInfo.mNextPieceId);
				theBuffer.WriteInt32(stateInfo.mIdleTicks);
			}
			theBuffer.WriteValuePair(Serialiser.PairID.ReplayTicks, theReplayData.mReplayTicks);
			return true;
		}

		public bool LoadReplayData(Serialiser theBuffer, ReplayData theReplayData)
		{
			int num;
			theBuffer.ReadValuePair(out num);
			if (num < 1 || num > 1)
			{
				return false;
			}
			string text;
			theBuffer.ReadStringPair(out text);
			Buffer buffer;
			theBuffer.ReadBufferPair(out buffer);
			theReplayData.mSaveBuffer.SetData(buffer.mData);
			if (theReplayData.mSaveBuffer.GetDataLen() == 0)
			{
				return false;
			}
			int num2;
			int num3;
			int num4;
			bool flag = theReplayData.mSaveBuffer.ReadFileHeader(out num2, out num3, out num4);
			theReplayData.mSaveBuffer.SeekFront();
			if (!flag || num2 < 101 || num2 > 103)
			{
				return false;
			}
			int num5;
			theBuffer.ReadValuePair(out num5);
			this.LoadQueuedMoves(theBuffer, theReplayData.mReplayMoves, num5);
			theBuffer.ReadValuePair(out theReplayData.mTutorialFlags);
			theReplayData.mStateInfoVector.Clear();
			theBuffer.ReadSpecialBlock(out num5);
			for (int i = 0; i < num5; i++)
			{
				StateInfo stateInfo = new StateInfo();
				stateInfo.mUpdateCnt = theBuffer.ReadInt32();
				stateInfo.mPoints = theBuffer.ReadInt32();
				stateInfo.mMoneyDisp = theBuffer.ReadInt32();
				stateInfo.mNextPieceId = theBuffer.ReadInt32();
				stateInfo.mIdleTicks = theBuffer.ReadInt32();
				theReplayData.mStateInfoVector.Add(stateInfo);
			}
			theBuffer.ReadValuePair(out theReplayData.mReplayTicks);
			return true;
		}

		public bool SaveReplay(ReplayData theReplayData)
		{
			if (this.mReplayStartMove.mPreSaveBuffer.GetDataLen() == 0)
			{
				return false;
			}
			theReplayData.mSaveBuffer = this.mReplayStartMove.mPreSaveBuffer;
			theReplayData.mReplayMoves = this.mQueuedMoveVector;
			theReplayData.mTutorialFlags = (ulong)((long)this.mTutorialFlags);
			theReplayData.mReplayTicks = 0;
			return true;
		}

		public void LoadReplay(ReplayData theReplayData)
		{
			this.mHadReplayError = false;
			theReplayData.mSaveBuffer.SeekFront();
			if (!this.LoadGame(theReplayData.mSaveBuffer))
			{
				this.mHadReplayError = true;
			}
			this.mQueuedMoveVector = theReplayData.mReplayMoves;
			this.mTutorialFlags = (int)theReplayData.mTutorialFlags;
			this.mStateInfoVector = theReplayData.mStateInfoVector;
		}

		public bool SafeSaveGame(Serialiser theBuffer)
		{
			if (!this.SaveGame(theBuffer))
			{
				if (this.mMoveDataVector.Count > 0)
				{
					theBuffer.Copyfrom(this.mMoveDataVector[this.mMoveDataVector.Count - 1].mPreSaveBuffer);
				}
				else
				{
					if (this.mLastMoveSave.GetDataLen() <= 0)
					{
						return false;
					}
					theBuffer.Copyfrom(this.mLastMoveSave);
				}
			}
			return true;
		}

		public void RewindToReplay(ReplayData theReplayData)
		{
			Serialiser serialiser = new Serialiser();
			this.SafeSaveGame(serialiser);
			this.mPreReplaySave.Copyfrom(serialiser);
			if (this.mRewindSound == null)
			{
				this.mRewindSound = GlobalMembers.gSexyApp.mSoundManager.GetSoundInstance(GlobalMembersResourcesWP.SOUND_REWIND);
			}
			if (this.mRewindSound != null && GlobalMembers.gApp.mMuteCount <= 0)
			{
				this.mRewindSound.SetVolume((GlobalMembers.gApp.mMuteCount > 0) ? 0.0 : GlobalMembers.gApp.mSfxVolume);
				this.mRewindSound.Play(true, false);
			}
			this.mInReplay = true;
			this.mRewinding = true;
			this.mPlaybackTimestamp = GlobalMembers.gGR.GetLastOperationTimestamp();
			((PauseMenu)GlobalMembers.gApp.mMenus[7]).SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
			this.DisableUI(true);
			this.LoadReplay(theReplayData);
			if (this.mHadReplayError)
			{
				this.mHadReplayError = false;
			}
		}

		public void DoReplaySetup()
		{
			this.mWantReplaySave = false;
			if (!this.SupportsReplays())
			{
				return;
			}
			if (this.mReplayStartMove.mPartOfReplay)
			{
				return;
			}
			this.HideReplayWidget();
			if (!this.SaveReplay(this.mCurReplayData))
			{
				return;
			}
			this.mReplayStartMove.mPartOfReplay = true;
			GlobalMembers.gGR.ClearOperationsTo(this.mReplayStartMove.mUpdateCnt - 1);
			this.mHasReplayData = true;
			if (!this.mInReplay)
			{
				this.mWatchedCurReplay = false;
				this.mReplayIgnoredMoves = 0;
				this.mReplayHadIgnoredMoves = false;
			}
			if (!this.mWantLevelup && this.mHyperspace == null)
			{
				this.ShowReplayWidget();
			}
		}

		public void HideReplayWidget()
		{
			if (this.mReplayWidgetShowPct > 0.0)
			{
				this.mReplayButton.mMouseVisible = false;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_REPLAY_WIDGET_HIDE_PCT, this.mReplayWidgetShowPct);
			}
		}

		public void ShowReplayWidget()
		{
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_REPLAY_WIDGET_SHOW_PCT, this.mReplayWidgetShowPct);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_REPLAY_POPUP);
			this.mReplayButton.mMouseVisible = true;
		}

		public bool LoadUReplay(Buffer theBuffer)
		{
			int num = (int)theBuffer.ReadLong();
			if (num != 1354408503)
			{
				return false;
			}
			int num2 = (int)theBuffer.ReadLong();
			if (num2 > 3 || num2 < 1)
			{
				return false;
			}
			this.mUReplayVersion = num2;
			theBuffer.ReadLong();
			this.mBoostsEnabled = (int)theBuffer.ReadLong();
			if (num2 >= 2)
			{
				this.mUReplayTotalTicks = (int)theBuffer.ReadLong();
			}
			else
			{
				this.mUReplayTotalTicks = 0;
			}
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					piece.release();
					this.mBoard[i, j] = null;
				}
			}
			int num3 = (int)theBuffer.ReadLong();
			theBuffer.ReadLong();
			byte[] array = new byte[num3];
			byte[] dataPtr = theBuffer.GetDataPtr();
			for (int k = 0; k < num3; k++)
			{
				array[k] = dataPtr[k + theBuffer.mReadBitPos / 8];
			}
			this.mUReplayBuffer.Clear();
			this.mUReplayBuffer.WriteBytes(array, num3);
			this.mUReplayTicksLeft = 6000;
			this.mInUReplay = true;
			return true;
		}

		public bool SaveUReplay(ref Buffer theBuffer)
		{
			theBuffer.WriteLong(1354408503L);
			theBuffer.WriteLong(3L);
			theBuffer.WriteLong(1L);
			theBuffer.WriteLong((long)this.mBoostsEnabled);
			theBuffer.WriteLong((long)this.mUpdateCnt);
			theBuffer.WriteLong((long)this.mUReplayBuffer.GetDataLen());
			theBuffer.WriteLong((long)this.mUReplayBuffer.GetDataLen());
			theBuffer.WriteBytes(this.mUReplayBuffer.GetDataPtr(), this.mUReplayBuffer.GetDataLen());
			return true;
		}

		public virtual void BoardSettled()
		{
		}

		public virtual void DialogClosed(int theId)
		{
		}

		public void TallyCoin(Piece thePiece)
		{
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_TREASUREFIND);
			int[] array = new int[]
			{
				GlobalMembers.M(250),
				GlobalMembers.M(500),
				GlobalMembers.M(1000),
				GlobalMembers.M(2500),
				GlobalMembers.M(5000)
			};
			if (!thePiece.mDestructing)
			{
				this.AddPoints((int)thePiece.CX(), (int)thePiece.CY(), array[0], GlobalMembers.gGemColors[thePiece.mColor], (uint)thePiece.mMatchId);
			}
			this.mPendingCoinAnimations++;
			this.mCoinCatcherAppearing = true;
			int[] array2 = new int[] { 10, 25, 50, 100 };
			if (!this.mInReplay)
			{
				this.mCoinsEarned += array2[thePiece.mCounter];
			}
		}

		public virtual void TallyPiece(Piece thePiece, bool thePieceDestroyed)
		{
			if (!thePiece.mTallied)
			{
				thePiece.mTallied = true;
				this.PieceTallied(thePiece);
				if (thePieceDestroyed)
				{
					if (!thePiece.IsFlagSet(65536U))
					{
						this.AddToStat(4, 1, thePiece.mMoveCreditId);
						if (thePiece.mColor > -1)
						{
							this.AddToStat(5 + thePiece.mColor, 1, thePiece.mMoveCreditId);
						}
						if (thePiece.mMoveCreditId != -1)
						{
							this.MaxStat(25, this.GetMoveStat(thePiece.mMoveCreditId, 1));
							this.MaxStat(33, this.GetMoveStat(thePiece.mMoveCreditId, 4));
							this.UpdateSpecialGemsStats(thePiece.mMoveCreditId);
						}
					}
					if (thePiece.IsFlagSet(16U))
					{
						this.IncPointMult(thePiece);
					}
				}
			}
		}

		public virtual void PieceTallied(Piece thePiece)
		{
			if (thePiece.IsFlagSet(1024U))
			{
				thePiece.mAlpha.SetConstant(0.0);
				this.TallyCoin(thePiece);
			}
			if (thePiece.IsFlagSet(512U) && this.mGameOverPiece == null)
			{
				int num = (int)thePiece.CX();
				int num2 = (int)thePiece.CY();
				Effect effect = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_LIGHT);
				effect.mFlags = 2;
				effect.mX = (float)num;
				effect.mY = (float)num2;
				effect.mZ = GlobalMembers.M(0.08f);
				effect.mValue[0] = GlobalMembers.M(45.1f);
				effect.mValue[1] = GlobalMembers.M(-0.5f);
				effect.mAlpha = GlobalMembers.M(0.3f);
				effect.mDAlpha = GlobalMembers.M(0.06f);
				effect.mScale = GlobalMembers.M(300f);
				this.mPostFXManager.AddEffect(effect);
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_GEM_COUNTDOWN_DESTROYED);
				if (!this.mIsOneMoveReplay)
				{
					this.mPostFXManager.AddSteamExplosion((float)num, (float)num2, GlobalMembers.gGemColors[thePiece.mColor]);
				}
				this.BumpColumn(thePiece, GlobalMembers.M(2f));
				for (int i = 0; i < GlobalMembers.M(12); i++)
				{
					Effect effect2 = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_COUNTDOWN_SHARD);
					effect2.mColor = GlobalMembers.gGemColors[thePiece.mColor];
					effect2.mX = (float)num;
					effect2.mY = (float)num2;
					this.mPostFXManager.AddEffect(effect2);
				}
			}
		}

		public void AddToStat(int theStatNum, int theNumber, int theMoveCreditId)
		{
			this.AddToStat(theStatNum, theNumber, theMoveCreditId, true);
		}

		public void AddToStat(int theStatNum, int theNumber)
		{
			this.AddToStat(theStatNum, theNumber, -1, true);
		}

		public void AddToStat(int theStatNum)
		{
			this.AddToStat(theStatNum, 1, -1, true);
		}

		public void AddToStat(int theStatNum, int theNumber, int theMoveCreditId, bool addToProfile)
		{
			this.mGameStats[theStatNum] += theNumber;
			if (this.mGameStats[theStatNum] < 0)
			{
				this.mGameStats[theStatNum] = int.MaxValue;
			}
			if (!this.mIsWholeGameReplay && !this.mInReplay && addToProfile)
			{
				StatsDoubleBuffer mStats;
				(mStats = GlobalMembers.gApp.mProfile.mStats)[theStatNum] = mStats[theStatNum] + theNumber;
				if (GlobalMembers.gApp.mProfile.mStats[theStatNum] < 0)
				{
					GlobalMembers.gApp.mProfile.mStats[theStatNum] = int.MaxValue;
				}
			}
			if (theMoveCreditId != -1)
			{
				for (int i = 0; i < this.mMoveDataVector.Count; i++)
				{
					if (this.mMoveDataVector[i].mMoveCreditId == theMoveCreditId)
					{
						this.mMoveDataVector[i].mStats[theStatNum] += theNumber;
					}
				}
			}
		}

		public void MaxStat(int theStatNum, int theNumber)
		{
			this.MaxStat(theStatNum, theNumber, -1);
		}

		public void MaxStat(int theStatNum, int theNumber, int theMoveCreditId)
		{
			this.mGameStats[theStatNum] = Math.Max(this.mGameStats[theStatNum], theNumber);
			if (!this.mIsWholeGameReplay)
			{
				GlobalMembers.gApp.mProfile.mStats[theStatNum] = Math.Max(GlobalMembers.gApp.mProfile.mStats[theStatNum], theNumber);
			}
			if (theMoveCreditId != -1)
			{
				for (int i = 0; i < this.mMoveDataVector.Count; i++)
				{
					if (this.mMoveDataVector[i].mMoveCreditId == theMoveCreditId)
					{
						this.mMoveDataVector[i].mStats[theStatNum] = Math.Max(this.mMoveDataVector[i].mStats[theStatNum], theNumber);
					}
				}
			}
		}

		public int GetMoveStat(int theMoveCreditId, int theStatNum)
		{
			return this.GetMoveStat(theMoveCreditId, theStatNum, 0);
		}

		public int GetMoveStat(int theMoveCreditId, int theStatNum, int theDefault)
		{
			for (int i = 0; i < this.mMoveDataVector.Count; i++)
			{
				if (this.mMoveDataVector[i].mMoveCreditId == theMoveCreditId)
				{
					return this.mMoveDataVector[i].mStats[theStatNum];
				}
			}
			return theDefault;
		}

		public int GetTotalMovesStat(int theStatNum)
		{
			int num = 0;
			for (int i = 0; i < this.mMoveDataVector.Count; i++)
			{
				num += this.mMoveDataVector[i].mStats[theStatNum];
			}
			return num;
		}

		public int GetMaxMovesStat(int theStatNum)
		{
			int num = 0;
			for (int i = 0; i < this.mMoveDataVector.Count; i++)
			{
				if (i == 0 || this.mMoveDataVector[i].mStats[theStatNum] > num)
				{
					num = this.mMoveDataVector[i].mStats[theStatNum];
				}
			}
			return num;
		}

		public void UpdateDeferredSounds()
		{
			if (this.mDeferredSounds.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this.mDeferredSounds.Count; i++)
			{
				if (this.mGameTicks >= this.mDeferredSounds[i].mOnGameTick)
				{
					GlobalMembers.gApp.PlaySample(this.mDeferredSounds[i].mId, 0, this.mDeferredSounds[i].mVolume);
					this.mDeferredSounds.RemoveAt(i);
					i--;
				}
			}
		}

		public void AddDeferredSound(int theSoundId, int theDelayGameTicks)
		{
			this.AddDeferredSound(theSoundId, theDelayGameTicks, 1.0);
		}

		public void AddDeferredSound(int theSoundId, int theDelayGameTicks, double theVol)
		{
			Board.DeferredSound deferredSound = new Board.DeferredSound();
			deferredSound.mId = theSoundId;
			deferredSound.mOnGameTick = this.mGameTicks + theDelayGameTicks;
			deferredSound.mVolume = theVol;
			this.mDeferredSounds.Add(deferredSound);
		}

		public void DoSpeedText(int anIndex)
		{
			this.WriteUReplayCmd(9);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_FLAMEBONUS);
			GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_BLAZINGSPEED, 0, 1.0, -2);
			this.DoComplement(GlobalMembers.gComplementCount);
			PIEffect pieffect = GlobalMembersResourcesWP.PIEFFECT_SPEEDBOARD_FLAME.Duplicate();
			pieffect.mDrawTransform.Translate((float)ConstantsWP.SPEEDBOARD_BLAZING_SPEED_EFFECT_1_X, (float)ConstantsWP.SPEEDBOARD_BLAZING_SPEED_EFFECT_1_Y);
			pieffect.mDrawTransform.Scale(GlobalMembers.S(1f), GlobalMembers.S(1f));
			this.mSpeedFireBarPIEffect[0] = pieffect;
			pieffect = GlobalMembersResourcesWP.PIEFFECT_SPEEDBOARD_FLAME.Duplicate();
			pieffect.mDrawTransform.Translate((float)ConstantsWP.SPEEDBOARD_BLAZING_SPEED_EFFECT_2_X, (float)ConstantsWP.SPEEDBOARD_BLAZING_SPEED_EFFECT_2_Y);
			pieffect.mDrawTransform.Scale(GlobalMembers.S(1f), GlobalMembers.S(1f));
			this.mSpeedFireBarPIEffect[1] = pieffect;
			this.mSpeedBonusFlameModePct = 1f;
			this.AddToStat(23);
		}

		public virtual void DoComplement(int theComplementNum)
		{
			Announcement announcement = null;
			int num = GlobalMembers.gComplements[theComplementNum];
			switch (num)
			{
			case 1738:
				announcement = new Announcement(this, GlobalMembers._ID("AWESOME", 3563));
				break;
			case 1739:
				announcement = new Announcement(this, GlobalMembers._ID("BLAZINGSPEED", 3567));
				break;
			case 1740:
			case 1743:
			case 1744:
			case 1745:
				break;
			case 1741:
				announcement = new Announcement(this, GlobalMembers._ID("EXCELLENT", 3562));
				break;
			case 1742:
				announcement = new Announcement(this, GlobalMembers._ID("EXTRAORDINARY", 3565));
				break;
			case 1746:
				announcement = new Announcement(this, GlobalMembers._ID("GOOD", 3561));
				break;
			default:
				if (num != 1750)
				{
					if (num == 1753)
					{
						announcement = new Announcement(this, GlobalMembers._ID("UNBELIEVABLE", 3566));
					}
				}
				else
				{
					announcement = new Announcement(this, GlobalMembers._ID("SPECTACULAR", 3564));
				}
				break;
			}
			announcement.mBlocksPlay = false;
			announcement.mAlpha.mIncRate *= GlobalMembers.M(3.0);
			announcement.mScale.mIncRate *= GlobalMembers.M(3.0);
			announcement.mDarkenBoard = false;
			announcement.mGoAnnouncement = true;
			GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.GetSoundById(GlobalMembers.gComplements[theComplementNum]));
			this.mComplementNum = theComplementNum;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_COMPLEMENT_ALPHA, this.mComplementAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_COMPLEMENT_SCALE, this.mComplementScale, this.mComplementAlpha);
			this.mLastComplement = theComplementNum;
		}

		public virtual void NewHyperMixer()
		{
		}

		public virtual void NewCombo()
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
					this.mComboColors[i] = (int)(this.mRand.Next() % 7U);
					if (++array2[this.mComboColors[i]] >= 3)
					{
						flag = false;
					}
				}
			}
			while (!flag);
		}

		public virtual bool ComboProcess(int theColor)
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

		public virtual void ComboFailed()
		{
			if (!this.WantColorCombos())
			{
				return;
			}
			this.mComboCount = Math.Max(0, this.mComboCount - 1);
		}

		public virtual void ComboCompleted()
		{
			if (!this.WantColorCombos())
			{
				return;
			}
			this.mComboCount = 0;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_COMBO_FLASH_PCT, this.mComboFlashPct);
		}

		public virtual void DepositCoin()
		{
			if (!this.mIsWholeGameReplay && !this.mInUReplay)
			{
				this.mMoneyDispGoal += 100;
			}
			if (this.mCoinCatcherPctPct >= 1.0)
			{
				this.mCoinCatcherPctPct = 4.0;
			}
			this.mPendingCoinAnimations--;
		}

		public virtual void CoinBalanceChanged()
		{
			this.mMoneyDisp = (this.mMoneyDispGoal = GlobalMembers.gApp.GetCoinCount());
		}

		public int GetPanPosition(int theX)
		{
			int num = this.GetColX(7) + 100;
			int num2 = theX - (this.GetBoardX() + num / 2);
			double num3 = (double)num2 / (double)num * 2.0;
			return (int)(num3 * (double)GlobalMembers.M(800));
		}

		public int GetPanPosition(Piece thePiece)
		{
			if (thePiece == null)
			{
				return 0;
			}
			return this.GetPanPosition((int)(thePiece.GetScreenX() + 50f));
		}

		public Points DoAddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd)
		{
			return this.DoAddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, theForceAdd, 1);
		}

		public Points DoAddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId)
		{
			return this.DoAddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, false, 1);
		}

		public Points DoAddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier)
		{
			return this.DoAddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, -1, false, 1);
		}

		public Points DoAddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube)
		{
			return this.DoAddPoints(theX, theY, thePoints, theColor, theId, addtotube, true, -1, false, 1);
		}

		public Points DoAddPoints(int theX, int theY, int thePoints, Color theColor, uint theId)
		{
			return this.DoAddPoints(theX, theY, thePoints, theColor, theId, true, true, -1, false, 1);
		}

		public Points DoAddPoints(int theX, int theY, int thePoints, Color theColor)
		{
			return this.DoAddPoints(theX, theY, thePoints, theColor, uint.MaxValue, true, true, -1, false, 1);
		}

		public Points DoAddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd, int thePointType)
		{
			if (thePoints <= 0 && !theForceAdd)
			{
				return null;
			}
			float num = (float)this.mPointMultiplier;
			if (!usePointMultiplier)
			{
				num = 1f;
			}
			int i = (int)((float)thePoints * num);
			while (i > 0)
			{
				int num2 = Math.Min(i, 10);
				double num3 = GlobalMembers.M(0.8);
				int num4 = (int)((float)this.GetMoveStat(theMoveCreditId, 1) / this.GetModePointMultiplier());
				double num5 = Math.Pow((double)(num4 + num2), num3) - Math.Pow((double)num4, num3);
				num5 *= GlobalMembers.M(3.0);
				if (addtotube)
				{
					this.mLevelPointsTotal += (int)num5;
				}
				i -= num2;
				int num6 = (int)((float)num2 * this.GetModePointMultiplier());
				this.AddToStat(1, num6, theMoveCreditId, true);
				this.mPoints += num6;
				if (this.mPoints < 0)
				{
					this.mPoints = int.MaxValue;
				}
			}
			return this.mPointsManager.Add(theX, theY, thePoints, theColor, theId, usePointMultiplier, theMoveCreditId, theForceAdd);
		}

		public virtual Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, theForceAdd, 1);
		}

		public virtual Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, false, 1);
		}

		public virtual Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, -1, false, 1);
		}

		public virtual Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, addtotube, true, -1, false, 1);
		}

		public virtual Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, theId, true, true, -1, false, 1);
		}

		public virtual Points AddPoints(int theX, int theY, int thePoints, Color theColor)
		{
			return this.AddPoints(theX, theY, thePoints, theColor, uint.MaxValue, true, true, -1, false, 1);
		}

		public virtual Points AddPoints(int theX, int theY, int thePoints, Color theColor, uint theId, bool addtotube, bool usePointMultiplier, int theMoveCreditId, bool theForceAdd, int thePointType)
		{
			if (this.mInUReplay)
			{
				return null;
			}
			if (this.WriteUReplayCmd(3))
			{
				this.mUReplayBuffer.WriteShort(this.EncodeX((float)theX));
				this.mUReplayBuffer.WriteShort(this.EncodeY((float)theY));
				this.mUReplayBuffer.WriteLong((long)thePoints);
				this.mUReplayBuffer.WriteLong((long)theColor.ToInt());
				this.mUReplayBuffer.WriteShort((short)theId);
				this.mUReplayBuffer.WriteBoolean(usePointMultiplier);
			}
			int num = this.mPoints;
			Points result = this.DoAddPoints(theX, theY, thePoints, theColor, theId, addtotube, usePointMultiplier, theMoveCreditId, theForceAdd, thePointType);
			int num2 = this.mPoints - num;
			List<int> list;
			(list = this.mPointsBreakdown[this.mPointsBreakdown.Count - 1])[thePointType] = list[thePointType] + num2;
			return result;
		}

		public virtual void AddPointBreakdownSection()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < 5; i++)
			{
				list.Add(0);
			}
			this.mPointsBreakdown.Add(list);
		}

		public virtual int GetLevelPoints()
		{
			return GlobalMembers.M(2500) + this.mLevel * GlobalMembers.M(750);
		}

		public virtual int GetLevelPointsTotal()
		{
			return this.mLevelPointsTotal;
		}

		public virtual void LevelUp()
		{
			if (this.mGameFinished || this.mWantLevelup || this.mHyperspace != null || this.mGameOverCount > 0)
			{
				return;
			}
			GlobalMembers.gApp.LogStatString(string.Format("LevelUp Level={0} Misc.Points={1} Seconds={2}\n", this.mLevel + 2, this.mPoints, this.mGameStats[0]));
			this.mWantLevelup = true;
			this.mLevelup = true;
			this.mHyperspacePassed = false;
		}

		public virtual void HyperspaceEvent(HYPERSPACEEVENT inEvent)
		{
			switch (inEvent)
			{
			case HYPERSPACEEVENT.HYPERSPACEEVENT_Start:
				this.mHyperspace.SetBGImage(this.mBackground.GetBackgroundImage());
				this.SetupBackground(1);
				this.mBackground.mVisible = false;
				return;
			case HYPERSPACEEVENT.HYPERSPACEEVENT_HideAll:
				this.mSideAlpha.SetConstant(1.0);
				this.mShowBoard = false;
				this.RandomizeBoard();
				return;
			case HYPERSPACEEVENT.HYPERSPACEEVENT_OldLevelClear:
				if (this.mLevelup)
				{
					this.mLevel++;
					this.mLevelup = false;
				}
				this.mLevelPointsTotal = 0;
				this.mMoveCounter = 0;
				return;
			case HYPERSPACEEVENT.HYPERSPACEEVENT_ZoomIn:
				this.mBackground.mVisible = true;
				this.mShowBoard = true;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SCALE_HYPERSPACE_ZOOM, this.mScale);
				return;
			case HYPERSPACEEVENT.HYPERSPACEEVENT_SlideOver:
			{
				this.mScale.SetConstant(1.0);
				Piece[,] array = this.mBoard;
				int upperBound = array.GetUpperBound(0);
				int upperBound2 = array.GetUpperBound(1);
				for (int i = array.GetLowerBound(0); i <= upperBound; i++)
				{
					for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
					{
						Piece piece = array[i, j];
						if (piece != null && piece.IsFlagSet(1U))
						{
							piece.ClearHyperspaceEffects();
						}
					}
				}
				return;
			}
			case HYPERSPACEEVENT.HYPERSPACEEVENT_Finish:
			{
				if (this.mHasReplayData && GlobalMembers.gApp.mCurrentGameMode != GameMode.MODE_ZEN)
				{
					this.ShowReplayWidget();
				}
				this.mBackground.SetVisible(true);
				GlobalMembers.KILL_WIDGET(this.mHyperspace);
				this.mHyperspace = null;
				GlobalMembers.gApp.mProfile.WriteProfile();
				this.SaveGame();
				Announcement announcement = new Announcement(this, string.Format(GlobalMembers._ID("LEVEL {0}", 541), this.mLevel + 1));
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eANNOUNCEMENT_ALPHA_BOARD, announcement.mAlpha);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eANNOUNCEMENT_SCALE_BOARD, announcement.mScale);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eANNOUNCEMENT_HORZ_SCALE_MULT_BOARD, announcement.mHorzScaleMult);
				announcement.mBlocksPlay = false;
				announcement.mDarkenBoard = false;
				if (!this.mInReplay)
				{
					GlobalMembers.gApp.DisableOptionsButtons(false);
				}
				return;
			}
			default:
				return;
			}
		}

		public void RandomizeBoard()
		{
			this.RandomizeBoard(false);
		}

		public void RandomizeBoard(bool clearFlags)
		{
			bool flag = false;
			List<Piece> list = new List<Piece>();
			do
			{
				list.Clear();
				list.Capacity = 64;
				Piece[,] array = this.mBoard;
				int upperBound = array.GetUpperBound(0);
				int upperBound2 = array.GetUpperBound(1);
				for (int i = array.GetLowerBound(0); i <= upperBound; i++)
				{
					for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
					{
						Piece piece = array[i, j];
						if (piece != null)
						{
							list.Add(piece);
							piece.mDestCol = (piece.mCol = 7 - piece.mCol);
							piece.mDestRow = (piece.mRow = 7 - piece.mRow);
						}
					}
				}
				foreach (Piece piece2 in list)
				{
					if (piece2 != null)
					{
						this.mBoard[piece2.mDestRow, piece2.mDestCol] = piece2;
					}
				}
				int[,] array2 = new int[,]
				{
					{ 1, 0 },
					{ 1, 1 },
					{ 0, 1 }
				};
				int num = 0;
				while ((num < GlobalMembers.M(16) || !this.FindMove(null, 0, true, true)) && num < GlobalMembers.M(20))
				{
					int num2 = (int)((ulong)(this.mRand.Next() % 8U) & 18446744073709551614UL);
					int num3 = (int)((ulong)(this.mRand.Next() % 8U) & 18446744073709551614UL);
					Piece piece3 = this.mBoard[num3, num2];
					if (piece3 != null && piece3.mDestCol == piece3.mCol && piece3.mDestRow == piece3.mRow)
					{
						for (int k = 0; k < 4; k++)
						{
							for (int l = 0; l < 3; l++)
							{
								int num4 = num2 + array2[l, 0];
								int num5 = num3 + array2[l, 1];
								Piece piece4 = this.mBoard[num3, num2];
								Piece piece5 = this.mBoard[num5, num4];
								int num6 = piece4.mDestCol;
								piece4.mDestCol = piece5.mDestCol;
								piece5.mDestCol = num6;
								num6 = piece4.mDestRow;
								piece4.mDestRow = piece5.mDestRow;
								piece5.mDestRow = num6;
								this.mBoard[piece4.mDestRow, piece4.mDestCol] = piece4;
								this.mBoard[piece5.mDestRow, piece5.mDestCol] = piece5;
							}
							if ((k & 1) == 0 && !this.HasSet())
							{
								break;
							}
						}
					}
					num++;
				}
				flag = this.FindMove(null, 0, true, true);
				foreach (Piece piece6 in list)
				{
					if (piece6 != null)
					{
						this.mBoard[piece6.mRow, piece6.mCol] = piece6;
					}
				}
			}
			while (!flag);
			foreach (Piece piece7 in list)
			{
				if (piece7 != null)
				{
					piece7.mCol = piece7.mDestCol;
					piece7.mRow = piece7.mDestRow;
					piece7.mX = (float)this.GetColX(piece7.mCol);
					piece7.mY = (float)this.GetRowY(piece7.mRow);
					this.mBoard[piece7.mRow, piece7.mCol] = piece7;
				}
			}
			int[] array3;
			do
			{
				for (int m = 0; m < GlobalMembers.M(3); m++)
				{
					Piece piece8 = this.mBoard[(int)((UIntPtr)(this.mRand.Next() % 8U)), (int)((UIntPtr)(this.mRand.Next() % 8U))];
					Piece piece9 = this.mBoard[(int)((UIntPtr)(this.mRand.Next() % 8U)), (int)((UIntPtr)(this.mRand.Next() % 8U))];
					if (piece8 != null && piece9 != null)
					{
						for (int n = 0; n < 2; n++)
						{
							Piece piece10 = this.mBoard[piece8.mRow, piece8.mCol];
							this.mBoard[piece8.mRow, piece8.mCol] = this.mBoard[piece9.mRow, piece9.mCol];
							this.mBoard[piece9.mRow, piece9.mCol] = piece10;
							int num7 = piece8.mCol;
							piece8.mCol = piece9.mCol;
							piece9.mCol = num7;
							num7 = piece8.mRow;
							piece8.mRow = piece9.mRow;
							piece9.mRow = num7;
							if (!this.HasSet())
							{
								break;
							}
						}
					}
				}
				array3 = new int[4];
			}
			while (!this.FindMove(array3, 3, true, true, true) || array3[1] < 4);
			Piece[,] array4 = this.mBoard;
			int upperBound3 = array4.GetUpperBound(0);
			int upperBound4 = array4.GetUpperBound(1);
			for (int num8 = array4.GetLowerBound(0); num8 <= upperBound3; num8++)
			{
				for (int num9 = array4.GetLowerBound(1); num9 <= upperBound4; num9++)
				{
					Piece piece11 = array4[num8, num9];
					if (piece11 != null)
					{
						piece11.mX = (float)this.GetColX(piece11.mCol);
						piece11.mY = (float)this.GetRowY(piece11.mRow);
					}
				}
			}
		}

		public virtual void GameOverAnnounce()
		{
			if (this.GetTicksLeft() == 0 && this.GetTimeLimit() > 0)
			{
				new Announcement(this, GlobalMembers._ID("TIME UP", 94));
				GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_TIMEUP);
				return;
			}
			new Announcement(this, GlobalMembers._ID("GAME OVER", 95));
			GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_GAMEOVER);
		}

		public virtual string GetLoggingGameName()
		{
			return this.mParams["Title"];
		}

		public virtual void LogGameOver(string theExtraParams)
		{
			GlobalMembers.gApp.LogStatString(string.Format("GameOver Title=\"{0}\" Seconds={1} Misc.Points={2} Level={3} PointMult={4}{5}{6}", new object[]
			{
				this.GetLoggingGameName(),
				this.mGameStats[0],
				this.mPoints,
				this.mLevel + 1,
				this.mPointMultiplier,
				(theExtraParams.Length > 0) ? " " : string.Empty,
				theExtraParams
			}));
		}

		public virtual string GetExtraGameOverLogParams()
		{
			return string.Empty;
		}

		public virtual void GameOver()
		{
			this.GameOver(true);
		}

		public virtual void GameOver(bool visible)
		{
			this.mCursorSelectPos = new Point(-1, -1);
			if (this.mGameFinished || this.mGameOverCount > 0 || this.mLevelCompleteCount > 0 || this.mWantLevelup || this.mHyperspace != null)
			{
				return;
			}
			GlobalMembers.gApp.mProfile.mLast3MatchScoreManager.Update(this.mGameStats[1]);
			this.LogGameOver(this.GetExtraGameOverLogParams());
			this.mGameFinished = true;
			this.DeleteSavedGame();
			if (visible)
			{
				this.GameOverAnnounce();
			}
			this.HideReplayWidget();
			this.mHasReplayData = false;
			this.mGameOverCount = 1;
			this.CalcBadges();
			GlobalMembers.gApp.mProfile.mTotalGamesPlayed += 1UL;
			GlobalMembers.gApp.mProfile.WriteProfile();
		}

		public virtual void CalcBadges()
		{
			this.SyncUnAwardedBadges(GlobalMembers.gApp.mProfile.mDeferredBadgeVector);
		}

		public virtual void BombExploded(Piece thePiece)
		{
			if (this.mLevelCompleteCount != 0)
			{
				return;
			}
			if (this.mGameOverPiece != null)
			{
				return;
			}
			this.GameOver();
			this.mGameOverPiece = thePiece;
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_TIMEBOMBEXPLODE);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_NUKE_RADIUS, this.mNukeRadius);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_NUKE_ALPHA, this.mNukeAlpha, this.mNukeRadius);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_NOVA_RADIUS, this.mNovaRadius, this.mNukeRadius);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_NOVA_ALPHA, this.mNovaAlpha, this.mNukeRadius);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GAME_OVER_PIECE_SCALE, this.mGameOverPieceScale, this.mNukeRadius);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GAME_OVER_PIECE_GLOW, this.mGameOverPieceGlow, this.mNukeRadius);
			this.mNukeRadius.IncInVal((double)GlobalMembers.M(0.1f));
		}

		public void UpdateBombExplode()
		{
			if (!this.mNukeRadius.IsDoingCurve())
			{
				return;
			}
			bool flag = !this.mNukeRadius.IncInVal();
			this.mGameOverPiece.Update();
			float num = (float)(this.mNovaRadius * (double)GlobalMembers.MS(280f));
			float num2 = this.mGameOverPiece.mX + 50f;
			float num3 = this.mGameOverPiece.mY + 50f;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Piece pieceAtRowCol = this.GetPieceAtRowCol(i, j);
					if (pieceAtRowCol != null && pieceAtRowCol != this.mGameOverPiece)
					{
						float num4 = pieceAtRowCol.mX + 50f - num2;
						float num5 = pieceAtRowCol.mY + 50f - num3;
						float num6 = (float)Math.Sqrt((double)(num4 * num4 + num5 * num5));
						if (num6 < num)
						{
							for (int k = 0; k < GlobalMembers.M(3); k++)
							{
								Effect effect = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_GEM_SHARD);
								effect.mColor = GlobalMembers.gGemColors[pieceAtRowCol.mColor];
								float mAngle = (float)k * 0.503f + (float)(BejeweledLivePlus.Misc.Common.Rand() % 100) / 800f;
								effect.mX = pieceAtRowCol.CX() + GlobalMembersUtils.GetRandFloat() * 100f / 2f;
								effect.mY = pieceAtRowCol.CY() + GlobalMembersUtils.GetRandFloat() * 100f / 2f;
								effect.mAngle = mAngle;
								effect.mDAngle = GlobalMembers.M(0.05f) * GlobalMembersUtils.GetRandFloat();
								effect.mScale = GlobalMembers.M(1f);
								effect.mAlpha = GlobalMembers.M(1f);
								effect.mDecel = GlobalMembers.M(0.8f) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.15f);
								float num7 = (float)Math.Atan2((double)num5, (double)num4);
								float num8 = GlobalMembers.M(1f);
								effect.mDX = num8 * GlobalMembers.M(4f) * GlobalMembersUtils.GetRandFloat() + (float)Math.Cos((double)num7) * GlobalMembers.M(16f);
								effect.mDY = num8 * GlobalMembers.M(4f) * GlobalMembersUtils.GetRandFloat() + (float)Math.Sin((double)num7) * GlobalMembers.M(16f);
								effect.mGravity = num8 * GlobalMembers.M(0.05f);
								effect.mValue[0] = GlobalMembersUtils.GetRandFloat() * 3.14159274f * 2f;
								effect.mValue[1] = effect.mValue[0] + GlobalMembers.M(0.25f) * 3.14159274f * 2f;
								effect.mValue[2] = (float)((GlobalMembersUtils.GetRandFloat() > 0f) ? 0 : 1);
								effect.mValue[3] = GlobalMembers.M(0.045f) * (GlobalMembers.M(3f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(1f));
								effect.mDAlpha = (float)(GlobalMembers.M(-0.005) * (double)(GlobalMembers.M(4f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(2f)));
								this.mPostFXManager.AddEffect(effect);
							}
							this.DeletePiece(pieceAtRowCol);
						}
					}
				}
			}
			if (this.mNukeRadius.GetInVal() >= (double)GlobalMembers.M(0.58f))
			{
				this.mNukeRadius.GetInVal();
				double num9 = (double)GlobalMembers.M(1.65f);
			}
			if (this.mNukeRadius.CheckInThreshold((double)GlobalMembers.M(1.65f)))
			{
				this.mGameOverPiece.mFlags = 0;
				this.mPostFXManager.Clear();
				this.mShowBoard = false;
			}
			if (flag)
			{
				this.GameOver();
			}
		}

		public virtual void SetupBackground()
		{
			this.SetupBackground(0);
		}

		public virtual void SetupBackground(int theDeltaIdx)
		{
			if (theDeltaIdx == 0 && this.mBackgroundIdx >= 0 && this.mBackground != null)
			{
				return;
			}
			int num = GlobalMembers.gBackgroundNames.Length;
			if (!this.WantsBackground())
			{
				return;
			}
			string text = string.Empty;
			int num2 = this.mBackgroundIdx;
			if (theDeltaIdx == 0)
			{
				if (this.mBackgroundIdx < 0)
				{
					if (this.mBackgroundIdxSet.Count == 0)
					{
						this.mBackgroundIdx = (int)((ulong)this.mRand.Next() % (ulong)((long)num));
					}
					else
					{
						this.mBackgroundIdx = this.mBackgroundIdxSet[BejeweledLivePlus.Misc.Common.Rand() % this.mBackgroundIdxSet.Count];
					}
					num2 = this.mBackgroundIdx % GlobalMembers.gBackgroundNames.Length;
				}
			}
			else
			{
				this.mBackgroundIdx = (this.mBackgroundIdx + theDeltaIdx + GlobalMembers.aDesiredOrderList.Length) % GlobalMembers.aDesiredOrderList.Length;
				num2 = this.GetLoadIndexFromBackgroundIndex();
			}
			text = GlobalMembers.gBackgroundNames[num2];
			if (text.IndexOf(".pam") != -1)
			{
				text = string.Format("images\\{0}\\backgrounds\\", GlobalMembers.gApp.mArtRes) + BejeweledLivePlus.Misc.Common.GetFileName(text, true) + "\\" + text;
			}
			else
			{
				text = string.Format("images\\{0}\\backgrounds\\", GlobalMembers.gApp.mArtRes) + text;
			}
			if (GlobalMembers.gApp.mForceBkg.Length != 0)
			{
				text = GlobalMembers.gApp.mForceBkg;
			}
			if (GlobalMembers.gApp.mTestBkg.Length != 0)
			{
				text = GlobalMembers.gApp.mTestBkg;
			}
			this.SetBackground(text);
		}

		public void SetBackground(string Path)
		{
			if (this.mBackgroundLoadedThreaded)
			{
				this.mBackgroundLoadedThreaded = false;
				return;
			}
			GlobalMembers.KILL_WIDGET_NOW(this.mBackground);
			this.mBackground = new Background(Path, true, false);
			this.mBackground.mZOrder = -1;
			this.mBackground.mAllowRescale = false;
			this.mBackground.Resize(0, 0, this.mWidth, this.mHeight);
			GlobalMembers.gApp.mWidgetManager.AddWidget(this.mBackground);
			GlobalMembers.gApp.ClearUpdateBacklog(false);
		}

		public int GetLoadIndexFromBackgroundIndex()
		{
			return GlobalMembers.aDesiredOrderList[this.mBackgroundIdx];
		}

		public virtual void IncPointMult(Piece thePieceFrom)
		{
			int num = this.mPointMultiplier + 1;
			if (!this.mTimeExpired)
			{
				if (this.mBackground != null)
				{
					this.mBackground.mScoreWaitLevel = this.mPointMultiplier;
				}
				if (this.mPointMultSoundQueue.Count == 0)
				{
					this.mPointMultSoundDelay = 0;
				}
				this.mPointMultSoundQueue.Add(GlobalMembersResourcesWP.GetSoundById(1577 + Math.Min(3, this.mPointMultiplier - 1)));
				this.mPointMultiplier++;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_PREV_POINT_MULT_ALPHA, this.mPrevPointMultAlpha);
				if (thePieceFrom == null)
				{
					this.mSrcPointMultPos = new Point(this.GetBoardCenterX(), this.GetBoardCenterY());
				}
				else
				{
					this.mSrcPointMultPos = new Point((int)thePieceFrom.CX(), (int)thePieceFrom.CY());
					if (thePieceFrom.mColor == 6)
					{
						this.mSrcPointMultPos.mY = this.mSrcPointMultPos.mY + GlobalMembers.M(-10);
					}
					if (thePieceFrom.mColor == 4)
					{
						this.mSrcPointMultPos.mY = this.mSrcPointMultPos.mY + GlobalMembers.M(12);
					}
				}
				this.mPointMultTextMorph.SetConstant(0.0);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_POS_PCT_1, this.mPointMultPosPct);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_SCALE_1, this.mPointMultScale, this.mPointMultPosPct);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_ALPHA_1, this.mPointMultAlpha, this.mPointMultPosPct);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_Y_ADD_1, this.mPointMultYAdd, this.mPointMultPosPct);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_DARKEN_PCT, this.mPointMultDarkenPct, this.mPointMultPosPct);
				Color[] array = new Color[]
				{
					new Color(255, 128, 128),
					new Color(255, 255, 255),
					new Color(128, 255, 128),
					new Color(255, 255, 128),
					new Color(255, 128, 255),
					new Color(255, 192, 128),
					new Color(128, 255, 255)
				};
				if (thePieceFrom == null)
				{
					this.mPointMultColor = new Color(255, 255, 255);
				}
				else
				{
					this.mPointMultColor = array[thePieceFrom.mColor];
				}
			}
			if (thePieceFrom != null)
			{
				this.AddPoints((int)thePieceFrom.CX(), (int)thePieceFrom.CY(), 1000 * num, GlobalMembers.gGemColors[thePieceFrom.mColor], (uint)thePieceFrom.mMatchId, false, false, -1);
			}
		}

		public virtual void Flamify(Piece thePiece)
		{
			if (thePiece.mColor == -1)
			{
				return;
			}
			if (!thePiece.SetFlag(1U))
			{
				return;
			}
			if (this.WantsCalmEffects())
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_POWERGEM_CREATED, GlobalMembers.M(0), GlobalMembers.M(0.5), GlobalMembers.M(-2.0));
			}
			else
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_POWERGEM_CREATED);
			}
			thePiece.ClearDisallowFlags();
			thePiece.ClearFlag(736U);
			thePiece.mImmunityCount = GlobalMembers.M(25);
			PopAnimEffect popAnimEffect = PopAnimEffect.fromPopAnim(GlobalMembersResourcesWP.POPANIM_FLAMEGEMCREATION);
			popAnimEffect.mX = thePiece.CX();
			popAnimEffect.mY = thePiece.CY();
			popAnimEffect.mScale = 2f;
			popAnimEffect.mDoubleSpeed = true;
			popAnimEffect.Play("Creation_Below Gem_Horizontal");
			popAnimEffect.Update();
			this.mPreFXManager.AddEffect(popAnimEffect);
			popAnimEffect = PopAnimEffect.fromPopAnim(GlobalMembersResourcesWP.POPANIM_FLAMEGEMCREATION);
			popAnimEffect.mX = thePiece.CX();
			popAnimEffect.mY = thePiece.CY();
			popAnimEffect.mScale = 2f;
			popAnimEffect.mOverlay = true;
			popAnimEffect.mDoubleSpeed = true;
			popAnimEffect.Play("Creation_Above Gem");
			popAnimEffect.Update();
			this.mPostFXManager.AddEffect(popAnimEffect);
		}

		public virtual void Hypercubeify(Piece thePiece)
		{
			this.Hypercubeify(thePiece, true);
		}

		public virtual void Hypercubeify(Piece thePiece, bool theStartEffect)
		{
			if (!thePiece.CanSetFlag(2U))
			{
				return;
			}
			thePiece.ClearFlags();
			thePiece.SetFlag(2U);
			thePiece.mChangedTick = this.mUpdateCnt;
			thePiece.mLastColor = thePiece.mColor;
			thePiece.mColor = -1;
			thePiece.mImmunityCount = GlobalMembers.M(25);
			if (theStartEffect)
			{
				this.StartHypercubeEffect(thePiece);
				if (this.WantsCalmEffects())
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_HYPERCUBE_CREATE, GlobalMembers.M(0), GlobalMembers.M(0.4), GlobalMembers.M(-3.0));
					return;
				}
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_HYPERCUBE_CREATE);
			}
		}

		public virtual void Laserify(Piece thePiece)
		{
			if (thePiece.mColor == -1)
			{
				return;
			}
			if (!thePiece.SetFlag(4U))
			{
				return;
			}
			thePiece.mShakeScale = 0f;
			if (this.WantsCalmEffects())
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_LASERGEM_CREATED, GlobalMembers.M(0), GlobalMembers.M(0.5), GlobalMembers.M(-2.0));
			}
			else
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_LASERGEM_CREATED);
			}
			thePiece.ClearDisallowFlags();
			thePiece.ClearFlag(736U);
			thePiece.mImmunityCount = GlobalMembers.M(25);
			this.StartLaserGemEffect(thePiece);
		}

		public void Bombify(Piece thePiece, int theCounter)
		{
			this.Bombify(thePiece, theCounter, false);
		}

		public void Bombify(Piece thePiece)
		{
			this.Bombify(thePiece, 20, false);
		}

		public void Bombify(Piece thePiece, int theCounter, bool realTime)
		{
			if (!thePiece.CanSetFlag(512U) && (realTime ? thePiece.CanSetFlag(64U) : thePiece.CanSetFlag(32U)))
			{
				return;
			}
			thePiece.ClearFlags();
			thePiece.ClearDisallowFlags();
			if (realTime)
			{
				thePiece.SetFlag(576U);
			}
			else
			{
				thePiece.SetFlag(544U);
			}
			thePiece.mCounter = theCounter;
			thePiece.mImmunityCount = GlobalMembers.M(25);
		}

		public void Doomify(Piece thePiece, int theCounter)
		{
			if (!thePiece.CanSetFlag(256U) || !thePiece.CanSetFlag(512U))
			{
				return;
			}
			thePiece.ClearFlags();
			thePiece.ClearDisallowFlags();
			thePiece.SetFlag(768U);
			thePiece.mColor = -1;
			thePiece.mCounter = theCounter;
			thePiece.mImmunityCount = GlobalMembers.M(25);
		}

		public void Coinify(Piece thePiece)
		{
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COIN_CREATED);
			if (thePiece.IsFlagSet(1024U))
			{
				thePiece.mCounter = Math.Min(thePiece.mCounter + 1, 3);
			}
			else
			{
				if (!thePiece.CanSetFlag(1024U))
				{
					return;
				}
				thePiece.ClearFlags();
				thePiece.SetFlag(1024U);
				thePiece.mColor = 1;
				thePiece.mImmunityCount = GlobalMembers.M(25);
			}
			thePiece.mCounter = 3;
		}

		public void Butterflyify(Piece thePiece)
		{
			if (!thePiece.CanSetFlag(128U))
			{
				return;
			}
			thePiece.ClearFlags();
			thePiece.ClearBoundEffects();
			thePiece.SetFlag(128U);
			this.StartPieceEffect(thePiece);
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_BUTTERFLY_CREATE);
			particleEffect.mPieceRel = thePiece;
			particleEffect.SetEmitterTint(0, 0, GlobalMembers.gGemColors[thePiece.mColor]);
			particleEffect.SetEmitterTint(0, 1, GlobalMembers.gGemColors[thePiece.mColor]);
			particleEffect.SetEmitterTint(0, 2, GlobalMembers.gGemColors[thePiece.mColor]);
			particleEffect.mDoubleSpeed = false;
			this.mPostFXManager.AddEffect(particleEffect);
		}

		public void Blastify(Piece thePiece)
		{
			if (!thePiece.CanSetFlag(524288U))
			{
				return;
			}
			thePiece.SetFlag(524288U);
			this.StartPieceEffect(thePiece);
		}

		public void StartPieceEffect(Piece thePiece)
		{
			if (thePiece.IsFlagSet(16U))
			{
				this.StartMultiplierGemEffect(thePiece);
				return;
			}
			if (thePiece.IsFlagSet(4U))
			{
				this.StartLaserGemEffect(thePiece);
				return;
			}
			if (thePiece.IsFlagSet(128U))
			{
				this.StartButterflyEffect(thePiece);
				return;
			}
			if (thePiece.IsFlagSet(2U))
			{
				this.StartHypercubeEffect(thePiece);
				return;
			}
			if (thePiece.IsFlagSet(6144U))
			{
				this.StartBoostGemEffect(thePiece);
				return;
			}
			if (thePiece.IsFlagSet(524288U))
			{
				this.StartBlastgemEffect(thePiece);
				return;
			}
			if (thePiece.IsFlagSet(131072U))
			{
				this.StartTimeBonusEffect(thePiece);
			}
		}

		public void Start3DFireGemEffect(Piece thePiece)
		{
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_FIREGEM_HYPERSPACE);
			particleEffect.SetEmitAfterTimeline(true);
			particleEffect.mDoDrawTransform = true;
			particleEffect.mFlags |= 8;
			thePiece.BindEffect(particleEffect);
			this.mPreFXManager.AddEffect(particleEffect);
		}

		public void StartLaserGemEffect(Piece thePiece)
		{
			ParticleEffect theEffect = this.NewBottomLaserEffect(thePiece.mColor);
			thePiece.BindEffect(theEffect);
			this.mPreFXManager.AddEffect(theEffect);
			theEffect = this.NewTopLaserEffect(thePiece.mColor);
			thePiece.BindEffect(theEffect);
			this.mPostFXManager.AddEffect(theEffect);
		}

		public ParticleEffect NewTopLaserEffect(int theGemColor)
		{
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_STARGEM);
			particleEffect.SetEmitAfterTimeline(true);
			particleEffect.mDoDrawTransform = true;
			for (int i = 0; i < 7; i++)
			{
				PILayer layer = particleEffect.GetLayer(i + 1);
				if (i == theGemColor)
				{
					layer.SetVisible(true);
				}
				else
				{
					layer.SetVisible(false);
				}
			}
			PILayer layer2 = particleEffect.GetLayer(theGemColor + 1);
			PIEmitterInstance emitter = layer2.GetEmitter("Glow");
			if (emitter != null)
			{
				emitter.SetVisible(false);
			}
			return particleEffect;
		}

		public ParticleEffect NewBottomLaserEffect(int theGemColor)
		{
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_STARGEM);
			particleEffect.SetEmitAfterTimeline(true);
			particleEffect.mDoDrawTransform = true;
			particleEffect.mDoubleSpeed = true;
			for (int i = 0; i < 7; i++)
			{
				PILayer layer = particleEffect.GetLayer(i + 1);
				if (i == theGemColor)
				{
					layer.SetVisible(true);
				}
				else
				{
					layer.SetVisible(false);
				}
			}
			particleEffect.GetLayer("Top").SetVisible(false);
			PILayer layer2 = particleEffect.GetLayer(theGemColor + 1);
			PIEmitterInstance emitter = layer2.GetEmitter("Stars");
			if (emitter != null)
			{
				emitter.SetVisible(false);
			}
			return particleEffect;
		}

		public void StartMultiplierGemEffect(Piece thePiece)
		{
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_ANIM_CURVE_A, thePiece.mAnimCurve);
			thePiece.mAnimCurve.SetMode(1);
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_MULTIPLIER);
			particleEffect.mPieceRel = thePiece;
			particleEffect.mDoDrawTransform = true;
			particleEffect.mDoubleSpeed = true;
			particleEffect.SetEmitAfterTimeline(true);
			for (int i = 0; i < 7; i++)
			{
				PILayer layer = particleEffect.GetLayer(i + 1);
				if (i == thePiece.mColor)
				{
					layer.SetVisible(true);
				}
				else
				{
					layer.SetVisible(false);
				}
			}
			this.mPostFXManager.AddEffect(particleEffect);
		}

		public void StartButterflyEffect(Piece thePiece)
		{
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_BUTTERFLY);
			particleEffect.mPieceRel = thePiece;
			particleEffect.SetEmitAfterTimeline(true);
			particleEffect.mDoDrawTransform = false;
			particleEffect.SetEmitterTint(0, 0, GlobalMembers.gGemColors[thePiece.mColor]);
			particleEffect.SetEmitterTint(0, 1, GlobalMembers.gGemColors[thePiece.mColor]);
			this.mPostFXManager.AddEffect(particleEffect);
		}

		public void StartHypercubeEffect(Piece thePiece)
		{
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_HYPERCUBE);
			particleEffect.mPieceRel = thePiece;
			particleEffect.SetEmitAfterTimeline(true);
			particleEffect.mDoDrawTransform = true;
			this.mPreFXManager.AddEffect(particleEffect);
		}

		public void StartBlastgemEffect(Piece thePiece)
		{
			ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_BLASTGEM);
			particleEffect.mPieceRel = thePiece;
			particleEffect.SetEmitAfterTimeline(true);
			particleEffect.mDoDrawTransform = true;
			this.mPreFXManager.AddEffect(particleEffect);
		}

		public void StartTimeBonusEffect(Piece thePiece)
		{
			TimeBonusEffect timeBonusEffect = TimeBonusEffect.alloc(thePiece);
			timeBonusEffect.mX = 50f;
			timeBonusEffect.mY = 50f;
			timeBonusEffect.mZ = GlobalMembers.M(0.08f);
			this.mPostFXManager.AddEffect(timeBonusEffect);
		}

		public void StartBoostGemEffect(Piece thePiece)
		{
		}

		public Piece GetSelectedPiece()
		{
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && piece.mSelected)
					{
						return piece;
					}
				}
			}
			return null;
		}

		public List<int> GetNewGemColors()
		{
			List<int> list = new List<int>();
			list.AddRange(this.mNewGemColors);
			if (this.mFavorComboGems)
			{
				for (int i = 0; i < this.mComboLen; i++)
				{
					list.Add(this.mComboColors[i]);
				}
			}
			for (int j = 0; j < this.mFavorGemColors.Count; j++)
			{
				list.Add(this.mFavorGemColors[j]);
			}
			return list;
		}

		public Piece CreateNewPiece(int theRow, int theCol)
		{
			if (theRow == -1 && theCol == -1)
			{
				return null;
			}
			Piece piece = Piece.alloc(this);
			piece.mCreatedTick = this.mUpdateCnt;
			piece.mLastActiveTick = this.mUpdateCnt;
			piece.mCol = theCol;
			piece.mRow = theRow;
			piece.mX = (float)this.GetColX(theCol);
			piece.mY = (float)this.GetRowY(theRow);
			this.mBoard[theRow, theCol] = piece;
			return piece;
		}

		public void DrawButterfly(Graphics g, int anOfsX, int anOfsY, Piece pPiece, float aScale)
		{
			g.SetScale(aScale, aScale, GlobalMembers.S(pPiece.GetScreenX() + 50f), GlobalMembers.S(pPiece.GetScreenY() + 50f));
			GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_BUTTERFLY_SHADOW, GlobalMembers.S(anOfsX), GlobalMembers.S(anOfsY), pPiece.mColor);
			g.SetScale(1f, 1f, GlobalMembers.S(pPiece.GetScreenX() + 50f), GlobalMembers.S(pPiece.GetScreenY() + 50f));
			Transform transform = new Transform();
			transform.Translate((float)GlobalMembers.S(ConstantsWP.BUTTERFLY_DRAW_OFFSET_1), 0f);
			transform.Scale((float)((1.0 - pPiece.mAnimCurve) * (double)aScale), aScale);
			GlobalMembers.gGR.DrawImageTransform(g, GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS, transform, GlobalMembers.IMGSRCRECT(GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS, pPiece.mColor), (float)GlobalMembers.S(anOfsX + ConstantsWP.BUTTERFLY_DRAW_OFFSET_2), (float)GlobalMembers.S(anOfsY + ConstantsWP.BUTTERFLY_DRAW_OFFSET_3));
			transform.Scale(-1f, 1f);
			GlobalMembers.gGR.DrawImageTransform(g, GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS, transform, GlobalMembers.IMGSRCRECT(GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS, pPiece.mColor), (float)GlobalMembers.S(anOfsX + ConstantsWP.BUTTERFLY_DRAW_OFFSET_4), (float)GlobalMembers.S(anOfsY + ConstantsWP.BUTTERFLY_DRAW_OFFSET_3));
			g.SetScale(aScale, aScale, GlobalMembers.S(pPiece.GetScreenX() + 50f), GlobalMembers.S(pPiece.GetScreenY() + 50f));
			GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_BUTTERFLY_BODY, GlobalMembers.S(anOfsX), GlobalMembers.S(anOfsY), pPiece.mColor);
			g.SetScale(1f, 1f, GlobalMembers.S(pPiece.GetScreenX() + 50f), GlobalMembers.S(pPiece.GetScreenY() + 50f));
		}

		public short EncodeX(float theX)
		{
			return (short)((theX - (float)this.GetBoardX()) / 100f * 256f);
		}

		public short EncodeY(float theY)
		{
			return (short)((theY - (float)this.GetRowY(0)) / 100f * 256f);
		}

		public float DecodeX(short theX)
		{
			return (float)(theX * 100) / 256f + (float)this.GetBoardX();
		}

		public float DecodeY(short theY)
		{
			return (float)(theY * 100) / 256f + (float)this.GetRowY(0);
		}

		public void EncodeSpeedBonus()
		{
			if (this.WriteUReplayCmd(8))
			{
				this.mUReplayBuffer.WriteByte((byte)this.mSpeedBonusCount);
				this.mUReplayBuffer.WriteByte((byte)(Math.Max(0.0, Math.Min(1.0, this.mSpeedBonusNum)) * 255.0));
			}
		}

		public byte EncodePieceFlags(int thePieceFlags)
		{
			if ((thePieceFlags & 1) != 0)
			{
				return 1;
			}
			if ((thePieceFlags & 4) != 0)
			{
				return 2;
			}
			if ((thePieceFlags & 2) != 0)
			{
				return 3;
			}
			if ((thePieceFlags & 16) != 0)
			{
				return 4;
			}
			if ((thePieceFlags & 1024) != 0)
			{
				return 5;
			}
			if ((thePieceFlags & 2048) != 0)
			{
				return 8;
			}
			if ((thePieceFlags & 4096) != 0)
			{
				return 7;
			}
			if ((thePieceFlags & 8192) != 0)
			{
				return 6;
			}
			return 0;
		}

		public int DecodePieceFlags(byte theType)
		{
			if (theType == 1)
			{
				return 1;
			}
			if (theType == 2)
			{
				return 4;
			}
			if (theType == 3)
			{
				return 2;
			}
			if (theType == 4)
			{
				return 16;
			}
			if (theType == 5)
			{
				return 1024;
			}
			if (theType == 8)
			{
				return 2048;
			}
			if (theType == 7)
			{
				return 4096;
			}
			if (theType == 6)
			{
				return 8192;
			}
			return 0;
		}

		public void EncodeLightningStorm(LightningStorm theLightningStorm)
		{
			if (theLightningStorm.mStormType == 2)
			{
				if (this.WriteUReplayCmd(14))
				{
					this.EncodePieceRef(this.GetPieceById(theLightningStorm.mElectrocuterId));
					return;
				}
			}
			else if (theLightningStorm.mStormType == 7 && this.WriteUReplayCmd(15))
			{
				this.EncodePieceRef(this.GetPieceById(theLightningStorm.mElectrocuterId));
				this.mUReplayBuffer.WriteByte((byte)theLightningStorm.mColor);
			}
		}

		public void EncodePieceRef(Piece thePiece)
		{
			if (thePiece == null)
			{
				this.mUReplayBuffer.WriteByte(byte.MaxValue);
				return;
			}
			this.mUReplayBuffer.WriteByte((byte)(thePiece.mRow * 8 + thePiece.mCol));
		}

		public Piece DecodePieceRef()
		{
			byte b = this.mUReplayBuffer.ReadByte();
			if (b == 255)
			{
				return null;
			}
			return this.mBoard[(int)(b / 8), (int)(b % 8)];
		}

		public virtual bool WantPointComplements()
		{
			return true;
		}

		public virtual bool IsHypermixerCancelledBy(Piece thePiece)
		{
			return thePiece.IsFlagSet(2U);
		}

		public void DrawCheckboard(Graphics g)
		{
			float mTransX = g.mTransX;
			float mTransY = g.mTransY;
			if (this.mSideXOff != 0.0)
			{
				g.Translate((int)(this.mSideXOff * (double)this.mSlideXScale), 0);
			}
			else
			{
				g.Translate((int)((double)GlobalMembers.S(1260) * this.mSlideUIPct), 0);
			}
			float num = this.mSpeedBonusFlameModePct * (float)GlobalMembers.M(60);
			Color theColor = this.mBoardColors[0];
			theColor.mAlpha = (int)((float)theColor.mAlpha * this.GetBoardAlpha());
			Color theColor2 = new Color((int)GlobalMembers.M(180f), (int)(GlobalMembers.M(100f) + (float)Math.Sin((double)num) * GlobalMembers.M(14f)), (int)(GlobalMembers.M(48f) + (float)Math.Sin((double)num) * GlobalMembers.M(8f)), (int)(GlobalMembers.M(200f) * this.GetBoardAlpha()));
			Color color = Utils.ColorLerp(theColor, theColor2, Math.Min(1f, this.mSpeedBonusFlameModePct * 5f));
			Color theColor3 = this.mBoardColors[1];
			theColor3.mAlpha = (int)((float)theColor3.mAlpha * this.GetBoardAlpha());
			Color theColor4 = new Color((int)GlobalMembers.M(160f), (int)(GlobalMembers.M(90f) + (float)Math.Sin((double)num) * GlobalMembers.M(12f)), (int)(GlobalMembers.M(40f) + (float)Math.Sin((double)num) * GlobalMembers.M(7f)), (int)(GlobalMembers.M(200f) * this.GetBoardAlpha()));
			Color color2 = Utils.ColorLerp(theColor3, theColor4, Math.Min(1f, this.mSpeedBonusFlameModePct * 5f));
			int[] array = new int[9];
			for (int i = 0; i < 9; i++)
			{
				array[i] = GlobalMembers.S(this.GetColScreenX(i));
			}
			if (this.mBoardUIOffsetY != 0)
			{
				g.Translate(0, GlobalMembers.S(this.mBoardUIOffsetY));
			}
			int num2 = 8;
			for (int j = 0; j < num2; j++)
			{
				int num3 = GlobalMembers.S(this.GetRowScreenY(j));
				int num4 = GlobalMembers.S(this.GetRowScreenY(j + 1));
				for (int k = 0; k < 8; k++)
				{
					int num5 = array[k];
					int num6 = array[k + 1];
					if ((j + k) % 2 == 0)
					{
						g.SetColor(color);
					}
					else
					{
						g.SetColor(color2);
					}
					g.FillRect(num5, num3, num6 - num5, num4 - num3);
				}
			}
			if (g.mTransX != mTransX || g.mTransY != mTransY)
			{
				g.mTransX = mTransX;
				g.mTransY = mTransY;
			}
			g.SetColor(Color.White);
		}

		public int GetColX(int theCol)
		{
			return theCol * 100;
		}

		public int GetRowY(int theRow)
		{
			return theRow * 100;
		}

		public int GetColScreenX(int theCol)
		{
			return this.GetColX(theCol) + this.GetBoardX();
		}

		public int GetRowScreenY(int theRow)
		{
			return this.GetRowY(theRow) + this.GetBoardY();
		}

		public int GetColAt(int theX)
		{
			for (int i = 0; i < 8; i++)
			{
				int colX = this.GetColX(i);
				if (theX >= colX && theX < colX + 100)
				{
					return i;
				}
			}
			return -1;
		}

		public int GetRowAt(int theY)
		{
			for (int i = 0; i < 8; i++)
			{
				int rowY = this.GetRowY(i);
				if (theY >= rowY && theY < rowY + 100)
				{
					return i;
				}
			}
			return -1;
		}

		public virtual int GetBoardX()
		{
			return GlobalMembers.RS(ConstantsWP.BOARD_X);
		}

		public virtual int GetBoardY()
		{
			return GlobalMembers.RS(ConstantsWP.BOARD_Y);
		}

		public int GetBoardCenterX()
		{
			return this.GetBoardX() + 400;
		}

		public int GetBoardCenterY()
		{
			return this.GetBoardY() + 400;
		}

		public virtual float GetAlpha()
		{
			return (float)this.mAlpha;
		}

		public virtual float GetBoardAlpha()
		{
			return this.GetAlpha();
		}

		public virtual float GetPieceAlpha()
		{
			return (1f - this.mBoardHidePct) * this.GetBoardAlpha();
		}

		public Piece GetPieceAtRowCol(int theRow, int theCol)
		{
			if (theRow < 0 || theRow >= 8 || theCol < 0 || theCol >= 8)
			{
				return null;
			}
			return this.mBoard[theRow, theCol];
		}

		public Piece GetPieceAtXY(int theX, int theY)
		{
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && (float)theX >= piece.mX && (float)theY >= piece.mY && (float)theX < piece.mX + 100f && (float)theY < piece.mY + 100f)
					{
						return piece;
					}
				}
			}
			return null;
		}

		public Piece GetPieceAtScreenXY(int theX, int theY)
		{
			return this.GetPieceAtXY(theX - this.GetBoardX(), theY - this.GetBoardY());
		}

		public Piece GetPieceById(int theId)
		{
			if (theId == -1)
			{
				return null;
			}
			if (this.mPieceMap.ContainsKey(theId))
			{
				return this.mPieceMap[theId];
			}
			return null;
		}

		public Piece GetRandomPieceOnGrid()
		{
			List<Piece> list = new List<Piece>();
			list.Capacity = 64;
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					if (piece != null)
					{
						list.Add(piece);
					}
				}
			}
			if (list.Count > 0)
			{
				return list[(int)((ulong)this.mRand.Next() % (ulong)((long)list.Count))];
			}
			return null;
		}

		public virtual void DeletePiece(Piece thePiece)
		{
			this.TallyPiece(thePiece, true);
			if (this.WriteUReplayCmd(4))
			{
				this.EncodePieceRef(thePiece);
			}
			for (int i = 0; i < this.mSwapDataVector.Count; i++)
			{
				SwapData swapData = this.mSwapDataVector[i];
				if (swapData.mPiece1 == thePiece || swapData.mPiece2 == thePiece)
				{
					if (swapData.mSwapPct < 0.0 && swapData.mPiece1 != null && swapData.mPiece2 != null)
					{
						int num = swapData.mPiece1.mCol;
						swapData.mPiece1.mCol = swapData.mPiece2.mCol;
						swapData.mPiece2.mCol = num;
						num = swapData.mPiece1.mRow;
						swapData.mPiece1.mRow = swapData.mPiece2.mRow;
						swapData.mPiece2.mRow = num;
						Piece piece = this.mBoard[swapData.mPiece1.mRow, swapData.mPiece1.mCol];
						this.mBoard[swapData.mPiece1.mRow, swapData.mPiece1.mCol] = this.mBoard[swapData.mPiece2.mRow, swapData.mPiece2.mCol];
						this.mBoard[swapData.mPiece2.mRow, swapData.mPiece2.mCol] = piece;
					}
					if (swapData.mPiece1 != null)
					{
						swapData.mPiece1.mX = (float)this.GetColX(swapData.mPiece1.mCol);
						swapData.mPiece1.mY = (float)this.GetRowY(swapData.mPiece1.mRow);
						swapData.mPiece1 = null;
					}
					if (swapData.mPiece2 != null)
					{
						swapData.mPiece2.mX = (float)this.GetColX(swapData.mPiece2.mCol);
						swapData.mPiece2.mY = (float)this.GetRowY(swapData.mPiece2.mRow);
						swapData.mPiece2 = null;
					}
					this.mSwapDataVector.RemoveAt(i);
					i--;
				}
			}
			for (int j = 0; j < thePiece.mRow; j++)
			{
				Piece piece2 = this.mBoard[j, thePiece.mCol];
				if (piece2 != null)
				{
					this.SetMoveCredit(piece2, thePiece.mMoveCreditId);
				}
			}
			this.mPreFXManager.RemovePieceFromEffects(thePiece);
			this.mPostFXManager.RemovePieceFromEffects(thePiece);
			this.mNextColumnCredit[thePiece.mCol] = Math.Max(this.mNextColumnCredit[thePiece.mCol], thePiece.mMoveCreditId);
			this.mBoard[thePiece.mRow, thePiece.mCol] = null;
			thePiece.release();
			thePiece = null;
		}

		public void ClearAllPieces()
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (this.mBoard[i, j] != null)
					{
						this.mBoard[i, j].release();
						this.mBoard[i, j] = null;
					}
				}
			}
			this.mSwapDataVector.Clear();
		}

		public void AddToPieceMap(int theId, Piece thePiece)
		{
			this.mPieceMap.Add(theId, thePiece);
		}

		public virtual void RemoveFromPieceMap(int theId)
		{
			if (this.mPieceMap.ContainsKey(theId))
			{
				this.mPieceMap.Remove(theId);
			}
		}

		public bool IsPieceSwapping(Piece thePiece, bool includeIgnored)
		{
			return this.IsPieceSwapping(thePiece, includeIgnored, false);
		}

		public bool IsPieceSwapping(Piece thePiece)
		{
			return this.IsPieceSwapping(thePiece, false, false);
		}

		public bool IsPieceSwapping(Piece thePiece, bool includeIgnored, bool onlyCheckForwardSwaps)
		{
			foreach (SwapData swapData in this.mSwapDataVector)
			{
				if ((!swapData.mIgnore || includeIgnored) && ((swapData.mForwardSwap && swapData.mHoldingSwap == 0) || !onlyCheckForwardSwaps) && (swapData.mPiece1 == thePiece || swapData.mPiece2 == thePiece))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsPieceMatching(Piece thePiece)
		{
			return thePiece.IsShrinking();
		}

		public bool IsPieceStill(Piece thePiece)
		{
			return thePiece.mFallVelocity == 0f && thePiece.mDestPct == 0.0 && thePiece.mExplodeDelay == 0 && !thePiece.mDestPct.IsDoingCurve() && (float)this.GetRowY(thePiece.mRow) == thePiece.mY && (thePiece.mCanMatch || thePiece.IsFlagSet(65536U)) && (!thePiece.IsFlagSet(8192U) && !this.IsPieceMatching(thePiece)) && !this.IsPieceSwapping(thePiece, false, false);
		}

		public bool WillPieceBeStill(Piece thePiece)
		{
			return !this.IsPieceMatching(thePiece) && !this.IsPieceSwapping(thePiece, false, true) && thePiece.mCanMatch && thePiece.mExplodeDelay == 0 && thePiece.mDestPct == 0.0 && !thePiece.IsFlagSet(8192U);
		}

		public bool CanBakeShadow(Piece thePiece)
		{
			return thePiece.mRotPct == 0f && !thePiece.IsFlagSet(6165U);
		}

		public bool IsBoardStill()
		{
			if (this.mSettlingDelay != 0)
			{
				return false;
			}
			if (this.mLightningStorms.Count != 0)
			{
				return false;
			}
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && !this.IsPieceStill(piece))
					{
						return false;
					}
				}
			}
			if (this.HasSet())
			{
				return false;
			}
			if (!this.mHasBoardSettled)
			{
				this.mHasBoardSettled = true;
				this.BoardSettled();
			}
			return true;
		}

		public virtual bool IsGameIdle()
		{
			if (this.mSettlingDelay != 0)
			{
				return false;
			}
			if (this.mLightningStorms.Count != 0)
			{
				return false;
			}
			if (this.mScrambleDelayTicks != 0)
			{
				return false;
			}
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && !this.IsPieceStill(piece) && !this.IsPieceSwapping(piece, false, false))
					{
						return false;
					}
				}
			}
			return !this.HasSet(null);
		}

		public virtual void DoHypercube(Piece thePiece, int theColor)
		{
			if (theColor == -1)
			{
				this.AddToStat(37, 1, thePiece.mMoveCreditId);
			}
			this.AddToStat(14, 1, thePiece.mMoveCreditId);
			this.ComboProcess(theColor);
			thePiece.mDestructing = true;
			LightningStorm lightningStorm = new LightningStorm(this, thePiece, 7);
			lightningStorm.mColor = theColor;
			this.mLightningStorms.Add(lightningStorm);
			this.EncodeLightningStorm(lightningStorm);
			Effect effect = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_HYPERCUBE_ENERGIZE);
			effect.mX = thePiece.CX();
			effect.mY = thePiece.CY();
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_ALPHA_HYPERCUBE, effect.mCurvedAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_CURVED_SCALE_HYPERCUBE, effect.mCurvedScale);
			effect.mDScale = 0f;
			effect.mDAlpha = 0f;
			effect.mAngle = 0f;
			effect.mDAngle = 1f;
			effect.mIsAdditive = true;
			effect.mGravity = 0f;
			effect.mOverlay = true;
			this.mPostFXManager.AddEffect(effect);
		}

		public virtual void DoHypercube(Piece thePiece, Piece theSwappedPiece)
		{
			this.DoHypercube(thePiece, theSwappedPiece.mColor);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ELECTRO_PATH);
			LightningStorm lightningStorm = this.mLightningStorms[this.mLightningStorms.Count - 1];
			lightningStorm.AddLightning((int)thePiece.mX + 50, (int)thePiece.mY + 50, (int)theSwappedPiece.mX + 50, (int)theSwappedPiece.mY + 50);
			Effect effect = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_LIGHT);
			effect.mFlags = 2;
			effect.mX = theSwappedPiece.CX();
			effect.mY = theSwappedPiece.CY();
			effect.mZ = GlobalMembers.M(0.04f);
			effect.mValue[0] = GlobalMembers.M(16.1f);
			effect.mValue[1] = GlobalMembers.M(-0.8f);
			effect.mAlpha = GlobalMembers.M(0f);
			effect.mDAlpha = GlobalMembers.M(0.1f);
			effect.mScale = GlobalMembers.M(140f);
			this.mPostFXManager.AddEffect(effect);
		}

		public virtual void ExamineBoard()
		{
		}

		public virtual bool WantSpecialPiece(List<Piece> thePieceVector)
		{
			return false;
		}

		public virtual bool DropSpecialPiece(List<Piece> thePieceVector)
		{
			return false;
		}

		public virtual bool TryingDroppedPieces(List<Piece> thePieceVector, int theTryCount)
		{
			return true;
		}

		public virtual bool PiecesDropped(List<Piece> thePieceVector)
		{
			return true;
		}

		public int NumPiecesWithFlag(int theFlag)
		{
			int num = 0;
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece.IsFlagSet((uint)theFlag))
					{
						num++;
					}
				}
			}
			return num;
		}

		public virtual bool CanTimeUp()
		{
			return this.IsBoardStill();
		}

		public virtual int GetTicksLeft()
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
			int num = GlobalMembers.M(50);
			return Math.Min(timeLimit * 100, Math.Max(0, timeLimit * 100 - Math.Max(0, this.mGameTicks - num)));
		}

		public virtual float GetLevelPct()
		{
			int levelPoints = this.GetLevelPoints();
			bool flag = this.mUpdateCnt % 20 == 0;
			float result;
			if (levelPoints != 0)
			{
				result = Math.Min(1f, (float)this.GetLevelPointsTotal() / (float)levelPoints);
			}
			else
			{
				result = 0f;
			}
			if (flag && this.WriteUReplayCmd(7))
			{
				this.mUReplayBuffer.WriteShort((short)this.GetTicksLeft());
			}
			return result;
		}

		public virtual float GetCountdownPct()
		{
			int timeLimit = this.GetTimeLimit();
			bool flag = this.mUpdateCnt % 20 == 0;
			float result = Math.Max(0f, (float)this.GetTicksLeft() / ((float)timeLimit * 100f));
			this.CheckCountdownBar();
			int ticksLeft = this.GetTicksLeft();
			int num = GlobalMembers.M(35) + (int)((float)ticksLeft * GlobalMembers.M(0.1f));
			if (this.mUpdateCnt - this.mLastWarningTick >= num && ticksLeft > 0 && this.WantWarningGlow(true))
			{
				int num2 = ((this.GetTimeLimit() > 60) ? 1500 : 1000);
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COUNTDOWN_WARNING, 0, Math.Min(1.0, GlobalMembers.M(1.0) - (double)((float)ticksLeft / (float)num2 / 3f)));
				this.mLastWarningTick = this.mUpdateCnt;
			}
			if (ticksLeft == 3000 && this.mDoThirtySecondVoice && this.mLevelCompleteCount == 0)
			{
				flag = true;
				GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_THIRTYSECONDS);
				if (this.mInUReplay)
				{
					this.mUReplayTicksLeft--;
				}
			}
			if (flag && this.WriteUReplayCmd(7))
			{
				this.mUReplayBuffer.WriteShort((short)this.GetTicksLeft());
			}
			return result;
		}

		public void ResolveMysteryGem()
		{
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				int j = array.GetLowerBound(1);
				while (j <= upperBound2)
				{
					Piece piece = array[i, j];
					if (piece.IsFlagSet(8192U))
					{
						piece.ClearFlag(8192U);
						int num = 0;
						switch (this.mRand.Next() % 3U)
						{
						case 0U:
							num = 1;
							this.Flamify(piece);
							break;
						case 1U:
							num = 3;
							this.Hypercubeify(piece);
							break;
						case 2U:
							num = 2;
							this.Laserify(piece);
							break;
						}
						if (this.WriteUReplayCmd(19))
						{
							this.EncodePieceRef(piece);
							this.mUReplayBuffer.WriteByte((byte)num);
							return;
						}
						return;
					}
					else
					{
						j++;
					}
				}
			}
		}

		public virtual int GetPowerGemThreshold()
		{
			return 5;
		}

		public virtual void SwapSucceeded(SwapData theSwapData)
		{
		}

		public virtual void SwapFailed(SwapData theSwapData)
		{
			if (this.mWantReplaySave)
			{
				this.mWantReplaySave = false;
			}
		}

		public virtual bool IsSwapLegal(Piece theSelected, int theSwappedRow, int theSwappedCol)
		{
			if (this.mLightningStorms.Count != 0)
			{
				return false;
			}
			if (!theSelected.mCanSwap)
			{
				return false;
			}
			Piece pieceAtRowCol = this.GetPieceAtRowCol(theSwappedRow, theSwappedCol);
			if (!this.IsPieceStill(theSelected) || theSelected.IsFlagSet(256U))
			{
				return false;
			}
			if (pieceAtRowCol != null && (!pieceAtRowCol.mCanSwap || !this.IsPieceStill(pieceAtRowCol) || pieceAtRowCol.IsFlagSet(256U)))
			{
				return false;
			}
			if (this.mDeferredTutorialVector.Count > 0)
			{
				return false;
			}
			int num = theSwappedCol - theSelected.mCol;
			int num2 = theSwappedRow - theSelected.mRow;
			return (!theSelected.IsButton() || Math.Abs(num) + Math.Abs(num2) == 0) && (theSelected.IsButton() || Math.Abs(num) + Math.Abs(num2) == 1);
		}

		public virtual bool QueueSwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			return this.QueueSwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, false);
		}

		public virtual bool QueueSwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap)
		{
			return this.QueueSwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, true, false);
		}

		public virtual bool QueueSwap(Piece theSelected, int theSwappedRow, int theSwappedCol)
		{
			return this.QueueSwap(theSelected, theSwappedRow, theSwappedCol, false, true, false);
		}

		public virtual bool QueueSwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped, bool destroyTarget)
		{
			if (!this.IsSwapLegal(theSelected, theSwappedRow, theSwappedCol))
			{
				return false;
			}
			QueuedMove queuedMove = new QueuedMove();
			queuedMove.mUpdateCnt = this.mUpdateCnt;
			queuedMove.mSelectedId = theSelected.mId;
			queuedMove.mSwappedRow = theSwappedRow;
			queuedMove.mSwappedCol = theSwappedCol;
			queuedMove.mForceSwap = forceSwap;
			queuedMove.mPlayerSwapped = playerSwapped;
			queuedMove.mDestroyTarget = destroyTarget;
			this.mQueuedMoveVector.Add(queuedMove);
			if (this.WantsWholeGameReplay())
			{
				this.mWholeGameReplay.mReplayMoves.Add(queuedMove);
			}
			return true;
		}

		public void PushMoveData(Piece theSelected, int theSwappedRow, int theSwappedCol)
		{
			MoveData moveData = new MoveData();
			moveData.mUpdateCnt = this.mUpdateCnt;
			moveData.mSelectedId = theSelected.mId;
			moveData.mSwappedRow = theSwappedRow;
			moveData.mSwappedCol = theSwappedCol;
			moveData.mPreSaveBuffer.WriteBytes(this.mLastMoveSave.mData, this.mLastMoveSave.GetDataLen());
			moveData.mMoveCreditId = this.mCurMoveCreditId++;
			for (int i = 0; i < 40; i++)
			{
				moveData.mStats[i] = 0;
			}
			this.mMoveDataVector.Add(moveData);
		}

		private bool CheckTrialGameFinished()
		{
			bool result = false;
			if (!GlobalMembers.gApp.mMainMenu.mIsFullGame())
			{
				switch (GlobalMembers.gApp.mCurrentGameMode)
				{
				case GameMode.MODE_CLASSIC:
					if (this.mLevel >= 4)
					{
						if (!this.mBuyFullGameShown)
						{
							GlobalMembers.gApp.DoTrialDialog(0);
							this.mBuyFullGameShown = true;
						}
						result = true;
					}
					break;
				case GameMode.MODE_DIAMOND_MINE:
					if (this.mLevelPointsTotal >= 50000)
					{
						if (!this.mBuyFullGameShown)
						{
							GlobalMembers.gApp.DoTrialDialog(2);
							this.mBuyFullGameShown = true;
						}
						result = true;
					}
					break;
				case GameMode.MODE_BUTTERFLY:
					if (this.mDispPoints >= 95000)
					{
						if (!this.mBuyFullGameShown)
						{
							GlobalMembers.gApp.DoTrialDialog(5);
							this.mBuyFullGameShown = true;
						}
						result = true;
					}
					break;
				}
			}
			return result;
		}

		public virtual bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped, false);
		}

		public virtual bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, true, false);
		}

		public virtual bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, false, true, false);
		}

		public virtual bool TrySwap(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped, bool destroyTarget)
		{
			if (this.CheckTrialGameFinished())
			{
				return false;
			}
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
			if (playerSwapped)
			{
				this.mLastPlayerSwapColor = theSelected.mColor;
			}
			if (this.mHasReplayData && !this.mInReplay)
			{
				if (this.mWatchedCurReplay)
				{
					this.mReplayIgnoredMoves = 3;
				}
				else
				{
					this.mReplayIgnoredMoves++;
				}
			}
			Serialiser theBuffer = new Serialiser();
			if (this.SaveGame(theBuffer))
			{
				this.mLastMoveSave = theBuffer;
			}
			if (playerSwapped)
			{
				theSelected.mSelected = false;
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SELECTOR_ALPHA, theSelected.mSelectorAlpha);
			}
			int mMoveCreditId = this.mCurMoveCreditId;
			this.PushMoveData(theSelected, theSwappedRow, theSwappedCol);
			Piece pieceAtRowCol = this.GetPieceAtRowCol(theSwappedRow, theSwappedCol);
			theSelected.mMoveCreditId = mMoveCreditId;
			if (pieceAtRowCol != null)
			{
				pieceAtRowCol.mMoveCreditId = mMoveCreditId;
			}
			if (theSelected.IsFlagSet(4096U))
			{
				Dictionary<Piece, bool> dictionary = new Dictionary<Piece, bool>();
				List<Piece> list = new List<Piece>();
				Piece[,] array = this.mBoard;
				int upperBound = array.GetUpperBound(0);
				int upperBound2 = array.GetUpperBound(1);
				for (int i = array.GetLowerBound(0); i <= upperBound; i++)
				{
					for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
					{
						Piece piece = array[i, j];
						if (piece != null && this.IsPieceStill(piece) && piece.mCanScramble && !piece.IsFlagSet(6144U))
						{
							list.Add(piece);
							dictionary[piece] = true;
						}
					}
				}
				if (this.mIdleTicks == 0 || list.Count < 10 || this.mScrambleDelayTicks >= GlobalMembers.M(150))
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_START_ROTATE);
					return true;
				}
				int mMoveCreditId2 = this.mCurMoveCreditId;
				this.mLastMoveSave.Clear();
				this.SaveGame(this.mLastMoveSave);
				this.PushMoveData(theSelected, theSwappedRow, theSwappedCol);
				for (int k = 0; k < list.Count; k++)
				{
					Piece piece2 = list[k];
					piece2.mDestCol = piece2.mCol;
					piece2.mDestRow = piece2.mRow;
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_DEST_PCT_A, piece2.mDestPct);
				}
				this.mLastMatchTick = this.mIdleTicks;
				this.mScrambleDelayTicks = 200;
				bool flag = false;
				if (this.WriteUReplayCmd(21))
				{
					this.EncodePieceRef(theSelected);
					flag = true;
				}
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SCRAMBLE);
				int num = 0;
				List<Piece> list2;
				List<Piece> list3;
				for (;;)
				{
					list2 = new List<Piece>();
					list3 = new List<Piece>();
					List<Piece> list4 = list;
					Dictionary<Piece, bool> dictionary2 = dictionary;
					for (int l = 0; l < list4.Count; l++)
					{
						Piece piece3 = list4[l];
						for (int m = 0; m < 8; m++)
						{
							Piece piece4 = this.mBoard[piece3.mRow, m];
							if (piece4 != null && piece4 != piece3 && dictionary2.ContainsKey(piece4))
							{
								list2.Add(piece3);
								list3.Add(piece4);
								break;
							}
						}
					}
					bool flag2 = false;
					for (int n = 0; n < 2; n++)
					{
						for (int num2 = 0; num2 < list2.Count; num2++)
						{
							int num3 = ((n == 0) ? num2 : (list2.Count - 1 - num2));
							Piece piece5 = list2[num3];
							Piece piece6 = list3[num3];
							Piece piece7 = this.mBoard[piece5.mRow, piece5.mCol];
							this.mBoard[piece5.mRow, piece5.mCol] = this.mBoard[piece6.mRow, piece6.mCol];
							this.mBoard[piece6.mRow, piece6.mCol] = piece7;
							int num4 = piece5.mCol;
							piece5.mCol = piece6.mCol;
							piece6.mCol = num4;
							num4 = piece5.mRow;
							piece5.mRow = piece6.mRow;
							piece6.mRow = num4;
							float num5 = piece5.mX;
							piece5.mX = piece6.mX;
							piece6.mX = num5;
							num5 = piece5.mY;
							piece5.mY = piece6.mY;
							piece6.mY = num5;
							piece5.mMoveCreditId = mMoveCreditId2;
						}
						if (n == 0 && (this.HasSet() || num == 250))
						{
							flag2 = true;
							break;
						}
					}
					if (flag2)
					{
						break;
					}
					num++;
				}
				if (flag)
				{
					this.mUReplayBuffer.WriteByte((byte)list2.Count);
					for (int num6 = 0; num6 < list2.Count; num6++)
					{
						this.EncodePieceRef(list2[num6]);
						this.EncodePieceRef(list3[num6]);
					}
				}
				if (--this.mScrambleUsesLeft == 0)
				{
					this.DeletePiece(theSelected);
				}
				return true;
			}
			else if (theSelected.IsFlagSet(2U) && pieceAtRowCol != null)
			{
				if (!pieceAtRowCol.mCanDestroy)
				{
					return false;
				}
				this.mWantHintTicks = 0;
				this.DecrementAllCounterGems(false);
				this.DoHypercube(theSelected, pieceAtRowCol);
				return true;
			}
			else
			{
				if (pieceAtRowCol == null || !pieceAtRowCol.IsFlagSet(2U) || pieceAtRowCol == null)
				{
					if (playerSwapped)
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_START_ROTATE);
					}
					if (this.WriteUReplayCmd(1))
					{
						Piece pieceAtRowCol2 = this.GetPieceAtRowCol(theSwappedRow, theSwappedCol);
						this.mUReplayBuffer.WriteByte(0);
						this.EncodePieceRef(theSelected);
						this.EncodePieceRef(pieceAtRowCol2);
					}
					theSelected.mLastActiveTick = this.mUpdateCnt;
					if (pieceAtRowCol != null)
					{
						pieceAtRowCol.mLastActiveTick = this.mUpdateCnt - 1;
					}
					SwapData swapData = new SwapData();
					swapData.mPiece1 = theSelected;
					swapData.mPiece2 = pieceAtRowCol;
					swapData.mSwapDir = new Point(theSwappedCol - theSelected.mCol, theSwappedRow - theSelected.mRow);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SWAP_PCT_1, swapData.mSwapPct);
					if (GlobalMembers.gIs3D)
					{
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GEM_SCALE_1, swapData.mGemScale);
					}
					swapData.mSwapPct.mIncRate *= (double)this.GetSwapSpeed();
					swapData.mGemScale.mIncRate *= (double)this.GetSwapSpeed();
					swapData.mForwardSwap = true;
					swapData.mHoldingSwap = 0;
					swapData.mIgnore = false;
					swapData.mForceSwap = forceSwap;
					swapData.mDestroyTarget = destroyTarget;
					this.mSwapDataVector.Add(swapData);
					return true;
				}
				if (!theSelected.mCanDestroy)
				{
					return false;
				}
				this.mWantHintTicks = 0;
				this.DecrementAllCounterGems(false);
				this.DoHypercube(pieceAtRowCol, theSelected);
				return true;
			}
		}

		public bool TrySwapAndRecord(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap)
		{
			return this.TrySwapAndRecord(theSelected, theSwappedRow, theSwappedCol, forceSwap, true);
		}

		public bool TrySwapAndRecord(Piece theSelected, int theSwappedRow, int theSwappedCol)
		{
			return this.TrySwapAndRecord(theSelected, theSwappedRow, theSwappedCol, false, true);
		}

		public bool TrySwapAndRecord(Piece theSelected, int theSwappedRow, int theSwappedCol, bool forceSwap, bool playerSwapped)
		{
			return this.TrySwap(theSelected, theSwappedRow, theSwappedCol, forceSwap, playerSwapped);
		}

		public virtual void PieceDestroyedInSwap(Piece thePiece)
		{
		}

		public void BumpColumn(Piece thePiece, float thePower)
		{
			float num = 0f;
			for (int i = 7; i >= 0; i--)
			{
				float num2 = 0f;
				for (int j = thePiece.mCol; j <= thePiece.mCol; j++)
				{
					Piece pieceAtRowCol = this.GetPieceAtRowCol(i, j);
					if (pieceAtRowCol != null && pieceAtRowCol.mY < thePiece.mY)
					{
						float num3 = Math.Abs(pieceAtRowCol.mY - thePiece.mY);
						float num4 = Math.Min(1f, num3 / this.BumpColumn_MAX_DIST);
						num4 = 1f - num4;
						num2 = thePower * GlobalMembers.M(-3.75f) * num4;
						if (num2 > -0.9f && num2 < 0f)
						{
							num2 = 0f;
						}
						if (num == 0f)
						{
							num = num2;
						}
						pieceAtRowCol.mFallVelocity = Math.Min(pieceAtRowCol.mFallVelocity, num);
					}
					this.mBumpVelocities[j] = Math.Max(num, num2);
				}
			}
		}

		public void BumpColumns(int theX, int theY, float thePower)
		{
			if (this.mInUReplay)
			{
				return;
			}
			for (int i = 0; i < 8; i++)
			{
				float num = 0f;
				float num2 = 0f;
				Piece piece = null;
				int j = 7;
				while (j >= -1)
				{
					Piece pieceAtRowCol = this.GetPieceAtRowCol(j, i);
					bool flag = false;
					float num3;
					float num4;
					if (pieceAtRowCol != null && pieceAtRowCol.GetScreenY() + 50f < (float)theY)
					{
						num3 = pieceAtRowCol.GetScreenX() + 50f - (float)theX;
						num4 = pieceAtRowCol.GetScreenY() + 50f - (float)theY;
						flag = true;
						goto IL_99;
					}
					if (j == -1)
					{
						num3 = (float)(this.GetColScreenX(i) + 50 - theX);
						num4 = (float)(this.GetRowScreenY(j) + 50 - theY);
						goto IL_99;
					}
					IL_18D:
					j--;
					continue;
					IL_99:
					float num5 = (float)Math.Atan2((double)num4, (double)num3);
					float num6 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4)) / 100f;
					float num7 = thePower / (Math.Max(0f, num6 - GlobalMembers.M(1f)) + GlobalMembers.M(1f)) * Math.Abs((float)Math.Sin((double)num5));
					num2 = num7 * GlobalMembers.M(-5.25f);
					if (num2 > -0.9f && num2 < 0f)
					{
						num2 = 0f;
					}
					if (!flag)
					{
						goto IL_18D;
					}
					if (num == 0f)
					{
						num = num2;
					}
					pieceAtRowCol.mFallVelocity = Math.Min(pieceAtRowCol.mFallVelocity, num);
					if (this.IsPieceSwapping(pieceAtRowCol))
					{
						piece = null;
						for (int k = j; k < 8; k++)
						{
							Piece piece2 = this.mBoard[k, i];
							if (piece2 != null)
							{
								piece2.mFallVelocity = Math.Max(0f, piece2.mFallVelocity);
							}
						}
						goto IL_18D;
					}
					if (piece == null)
					{
						piece = pieceAtRowCol;
						goto IL_18D;
					}
					goto IL_18D;
				}
				if (piece != null && this.WriteUReplayCmd(12))
				{
					this.EncodePieceRef(piece);
					this.mUReplayBuffer.WriteShort((short)(piece.mFallVelocity / 100f * 256f * 100f));
				}
				this.mBumpVelocities[i] = Math.Min(num, num2);
			}
		}

		public virtual void CelDestroyedBySpecial(int theCol, int theRow)
		{
		}

		public void UpdateLightning()
		{
			new List<Piece>();
			bool flag = this.WantsCalmEffects();
			GlobalMembers.M(0.05f);
			int num = 0;
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && piece.mIsElectrocuting)
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
				bool flag2 = false;
				switch (lightningStorm.mStormType)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				{
					int num3 = ((lightningStorm.mStormType == 6) ? 1 : 0);
					bool flag3 = true;
					for (int l = 1; l < lightningStorm.mPieceIds.Count; l++)
					{
						Piece pieceById = this.GetPieceById(lightningStorm.mPieceIds[l]);
						if (pieceById != null && pieceById.mCanDestroy)
						{
							if (flag)
							{
								pieceById.mElectrocutePercent += GlobalMembers.M(0.01f);
							}
							else
							{
								pieceById.mElectrocutePercent += GlobalMembers.M(0.015f);
							}
							if (pieceById.mElectrocutePercent >= 1f)
							{
								Piece pieceById2 = this.GetPieceById(lightningStorm.mElectrocuterId);
								if (!pieceById.mDestructing)
								{
									this.SetMoveCredit(pieceById, lightningStorm.mMoveCreditId);
									pieceById.mMatchId = lightningStorm.mMatchId;
									pieceById.mExplodeSourceId = lightningStorm.mElectrocuterId;
									pieceById.mExplodeSourceFlags |= lightningStorm.mStartPieceFlags;
									if (pieceById.IsFlagSet(16U))
									{
										pieceById.mExplodeDelay = 5;
									}
									else if (!this.TriggerSpecial(pieceById, pieceById2) && !pieceById.mDestructing)
									{
										pieceById.mExplodeDelay = 1;
									}
								}
								lightningStorm.mPieceIds.RemoveAt(l);
								l--;
							}
							else
							{
								flag3 = false;
							}
						}
					}
					for (int m = 0; m < lightningStorm.mElectrocutedCelVector.Count; m++)
					{
						ElectrocutedCel electrocutedCel = lightningStorm.mElectrocutedCelVector[m];
						if (flag)
						{
							electrocutedCel.mElectrocutePercent += GlobalMembers.M(0.01f);
						}
						else
						{
							electrocutedCel.mElectrocutePercent += GlobalMembers.M(0.015f);
						}
						if (electrocutedCel.mElectrocutePercent >= 1f)
						{
							this.CelDestroyedBySpecial(electrocutedCel.mCol, electrocutedCel.mRow);
							lightningStorm.mElectrocutedCelVector.RemoveAt(m);
							m--;
						}
					}
					lightningStorm.mTimer -= 0.01f;
					if (lightningStorm.mTimer <= 0f)
					{
						if (flag)
						{
							lightningStorm.mTimer = GlobalMembers.M(0.15f);
						}
						else
						{
							lightningStorm.mTimer = GlobalMembers.M(0.1f);
						}
						int[,] array2 = new int[,]
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
						int num4 = 0;
						int num5 = 4;
						if (lightningStorm.mStormType == 0)
						{
							num5 = 2;
						}
						if (lightningStorm.mStormType == 1)
						{
							num4 = 2;
						}
						if (lightningStorm.mStormType == 4)
						{
							num5 = 8;
						}
						if (lightningStorm.mStormType == 5)
						{
							int num6 = Math.Min(lightningStorm.mDist, 7);
							int num7 = 0;
							for (int n = -num6; n <= num6; n++)
							{
								array2[num7, 0] = n;
								array2[num7++, 1] = -num6;
								array2[num7, 0] = n;
								array2[num7++, 1] = num6;
							}
							for (int num8 = -num6 + 1; num8 <= num6 - 1; num8++)
							{
								array2[num7, 0] = -num6;
								array2[num7++, 1] = num8;
								array2[num7, 0] = num6;
								array2[num7++, 1] = num8;
							}
							num5 = num7;
						}
						for (int num9 = num4; num9 < num5; num9++)
						{
							for (int num10 = -num3; num10 <= num3; num10++)
							{
								int num11 = ((lightningStorm.mStormType == 5) ? 1 : lightningStorm.mDist);
								int num12 = lightningStorm.mCX + (num11 * array2[num9, 0] + array2[num9, 1] * num10) * 100;
								int num13 = lightningStorm.mCY + (num11 * array2[num9, 1] + array2[num9, 0] * num10) * 100;
								Piece pieceAtXY = this.GetPieceAtXY(num12, num13);
								if (num11 <= lightningStorm.mStormLength && num12 >= 0 && num12 < this.GetColX(8) && num13 >= 0 && num13 < this.GetRowY(8))
								{
									if (num12 != lightningStorm.mCX || num13 != lightningStorm.mCY)
									{
										ElectrocutedCel electrocutedCel2 = new ElectrocutedCel();
										electrocutedCel2.mCol = this.GetColAt(num12);
										electrocutedCel2.mRow = this.GetRowAt(num13);
										electrocutedCel2.mElectrocutePercent = 0.01f;
										lightningStorm.mElectrocutedCelVector.Add(electrocutedCel2);
									}
									if (pieceAtXY != null && !pieceAtXY.mDestructing)
									{
										bool flag4 = false;
										for (int num14 = 0; num14 < this.mLightningStorms.Count; num14++)
										{
											for (int num15 = 0; num15 < this.mLightningStorms[num14].mPieceIds.Count; num15++)
											{
												if (pieceAtXY.mId == this.mLightningStorms[num14].mPieceIds[num15])
												{
													flag4 = true;
												}
											}
										}
										if (!flag4)
										{
											flag3 = false;
											if (pieceAtXY.mElectrocutePercent == 0f)
											{
												lightningStorm.mPieceIds.Add(pieceAtXY.mId);
												pieceAtXY.mElectrocutePercent = 0.01f;
												for (int num16 = 0; num16 < 8; num16++)
												{
													Effect effect = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_SPARKLE_SHARD);
													effect.mX = (float)(num12 + this.GetBoardX()) + GlobalMembersUtils.GetRandFloat() * (float)Math.Abs(array2[num9, 0]) * 100f / 3f;
													effect.mY = (float)(num13 + this.GetBoardY()) + GlobalMembersUtils.GetRandFloat() * (float)Math.Abs(array2[num9, 1]) * 100f / 3f;
													effect.mDX = (float)((int)((double)GlobalMembersUtils.GetRandFloat() * ((double)Math.Abs(array2[num9, 1]) + 0.5) * (double)GlobalMembers.M(10)));
													effect.mDY = (float)((int)((double)GlobalMembersUtils.GetRandFloat() * ((double)Math.Abs(array2[num9, 0]) + 0.5) * (double)GlobalMembers.M(10)));
													this.mPostFXManager.AddEffect(effect);
												}
											}
											pieceAtXY.mShakeScale = Math.Min(1f, Math.Max(pieceAtXY.mShakeScale, pieceAtXY.mElectrocutePercent));
										}
									}
								}
							}
						}
						if (lightningStorm.mDist == 0)
						{
							if (flag)
							{
								GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ELECTRO_EXPLODE, 0, GlobalMembers.M(0.6), GlobalMembers.M(-1.0));
							}
							else
							{
								GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ELECTRO_EXPLODE);
							}
						}
						lightningStorm.mDist++;
						int mStormLength = lightningStorm.mStormLength;
						if (lightningStorm.mDist < mStormLength)
						{
							flag3 = false;
						}
						if (flag3)
						{
							this.mComboBonusSlowdownPct = 1f;
							Piece pieceById3 = this.GetPieceById(lightningStorm.mPieceIds[0]);
							if (pieceById3 != null)
							{
								pieceById3.ClearFlag(4U);
								if (pieceById3.IsFlagSet(2U))
								{
									this.TriggerSpecial(pieceById3, pieceById3);
								}
								else
								{
									this.CelDestroyedBySpecial(pieceById3.mCol, pieceById3.mRow);
									pieceById3.mExplodeDelay = 1;
									pieceById3.mExplodeSourceId = pieceById3.mId;
									pieceById3.mExplodeSourceFlags |= pieceById3.mFlags;
								}
							}
							lightningStorm.mHoldDelay -= GlobalMembers.M(0.25f);
							for (int num17 = 0; num17 < 8; num17++)
							{
								for (int num18 = 0; num18 < 8; num18++)
								{
									Piece piece2 = this.mBoard[num17, num18];
									if (piece2 != null)
									{
										piece2.mFallVelocity = 0f;
									}
								}
							}
							if (lightningStorm.mHoldDelay <= 0f)
							{
								flag2 = true;
							}
						}
					}
					break;
				}
				}
				if (lightningStorm.mDoneDelay > 0 && --lightningStorm.mDoneDelay == 0)
				{
					flag2 = true;
				}
				if (flag2)
				{
					lightningStorm.Dispose();
					this.mLightningStorms.RemoveAt(k);
					if (this.mLightningStorms.Count == 0 && !this.mInUReplay)
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
					num2 += GlobalMembers.M(25);
				}
			}
		}

		public int FindStormIdxFor(Piece thePiece)
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

		public void DrawLightning(Graphics g)
		{
			for (int i = 0; i < this.mLightningStorms.Count; i++)
			{
				LightningStorm lightningStorm = this.mLightningStorms[i];
				lightningStorm.Draw(g);
			}
		}

		public void ExplodeAtHelper(int theX, int theY)
		{
			int num = theX;
			int num2 = theY;
			Piece pieceAtScreenXY = this.GetPieceAtScreenXY(theX, theY);
			if (pieceAtScreenXY != null)
			{
				num = (int)(pieceAtScreenXY.GetScreenX() + 50f);
				num2 = (int)(pieceAtScreenXY.GetScreenY() + 50f);
			}
			GlobalMembers.gExplodePoints[GlobalMembers.gExplodeCount, 0] = num;
			GlobalMembers.gExplodePoints[GlobalMembers.gExplodeCount, 1] = num2;
			GlobalMembers.gExplodeCount++;
			Effect effect = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_LIGHT);
			effect.mFlags = 2;
			effect.mX = (float)theX;
			effect.mY = (float)theY;
			effect.mZ = GlobalMembers.M(0.08f);
			effect.mValue[0] = GlobalMembers.M(45.1f);
			effect.mValue[1] = GlobalMembers.M(-0.5f);
			effect.mAlpha = GlobalMembers.M(0.3f);
			effect.mDAlpha = GlobalMembers.M(0.06f);
			effect.mScale = GlobalMembers.M(300f);
			this.mPostFXManager.AddEffect(effect);
			int[,] array = new int[,]
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
			int[,] array2 = new int[,]
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
			int num3 = (this.HasLargeExplosions() ? 13 : 9);
			int[,] array3 = ((num3 == 9) ? array : array2);
			for (int i = 0; i < num3; i++)
			{
				int num4 = theX + array3[i, 0] * 100;
				int num5 = theY + array3[i, 1] * 100;
				int num6;
				int num7;
				if (pieceAtScreenXY != null)
				{
					num6 = pieceAtScreenXY.mCol + array3[i, 0];
					num7 = pieceAtScreenXY.mRow + array3[i, 1];
				}
				else
				{
					num6 = this.GetColAt(theX - this.GetBoardX());
					num7 = this.GetRowAt(theY - this.GetBoardY());
				}
				if (num6 >= 0 && num6 < 8 && num7 >= 0 && num7 < 8)
				{
					this.CelDestroyedBySpecial(num6, num7);
				}
				Piece pieceAtScreenXY2 = this.GetPieceAtScreenXY(num4, num5);
				if (pieceAtScreenXY2 != null && (pieceAtScreenXY2.mExplodeDelay == 0 || i == 8))
				{
					int num8 = (int)(pieceAtScreenXY2.GetScreenX() + 50f);
					int num9 = (int)(pieceAtScreenXY2.GetScreenY() + 50f);
					bool flag = false;
					int num10 = (int)(GlobalMembers.M(0.013f) * (float)(num4 - theX));
					int num11 = (int)(GlobalMembers.M(0.013f) * (float)(num5 - theY));
					if (array3[i, 0] != 0 || array3[i, 1] != 0)
					{
						if (this.WantsCalmEffects())
						{
							for (int j = 0; j < GlobalMembers.M(8); j++)
							{
								Effect effect2 = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_SPARKLE_SHARD);
								float num12 = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
								float num13 = GlobalMembers.M(0f) + GlobalMembers.M(2f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								effect2.mDX = num13 * (float)Math.Cos((double)num12) + (float)num10;
								effect2.mDY = num13 * (float)Math.Sin((double)num12) + (float)num11;
								effect2.mDX *= GlobalMembers.M(2.5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								effect2.mDY *= GlobalMembers.M(2.5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								float num14 = Math.Abs(GlobalMembersUtils.GetRandFloat());
								num14 *= num14;
								effect2.mX = num14 * (float)num + (1f - num14) * (float)num8 + (float)Math.Cos((double)num12) * GlobalMembers.M(64f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								effect2.mY = num14 * (float)num2 + (1f - num14) * (float)num9 + (float)Math.Sin((double)num12) * GlobalMembers.M(64f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								effect2.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255));
								effect2.mIsAdditive = true;
								effect2.mDScale = GlobalMembers.M(0.015f);
								effect2.mScale = GlobalMembers.M(0.1f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1f);
								this.mPostFXManager.AddEffect(effect2);
							}
						}
						else
						{
							int num15 = GlobalMembers.M(3);
							for (int k = 0; k < num15; k++)
							{
								Effect effect3 = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_STEAM);
								float num16 = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
								float num17 = GlobalMembers.M(0f) + GlobalMembers.M(2f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								effect3.mDX = num17 * (float)Math.Cos((double)num16) + (float)num10;
								effect3.mDY = num17 * (float)Math.Sin((double)num16) + (float)num11;
								effect3.mDX *= GlobalMembers.M(2.5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								effect3.mDY *= GlobalMembers.M(2.5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								float num18 = Math.Abs(GlobalMembersUtils.GetRandFloat());
								num18 *= num18;
								effect3.mX = num18 * (float)num + (1f - num18) * (float)num8 + (float)Math.Cos((double)num16) * GlobalMembers.M(64f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								effect3.mY = num18 * (float)num2 + (1f - num18) * (float)num9 + (float)Math.Sin((double)num16) * GlobalMembers.M(64f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
								effect3.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(96), GlobalMembers.M(32), GlobalMembers.M(64));
								effect3.mIsAdditive = true;
								effect3.mDScale = GlobalMembers.M(0.015f);
								effect3.mScale = GlobalMembers.M(0.1f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1f);
								this.mPostFXManager.AddEffect(effect3);
							}
						}
						int num19 = GlobalMembers.M(5);
						for (int l = 0; l < num19; l++)
						{
							Effect effect4 = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_GEM_SHARD);
							effect4.mColor = GlobalMembers.gGemColors[pieceAtScreenXY2.mColor];
							float num20 = GlobalMembers.M(1.2f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1.2f);
							float num21;
							if (num10 != 0 || num11 != 0)
							{
								num21 = GlobalMembersUtils.GetRandFloat() * 3.14159f;
								int num22 = (int)(GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(48f));
								effect4.mX = (float)(num8 + (int)(GlobalMembers.M(1f) * (float)num22 * (float)Math.Cos((double)num21)));
								effect4.mY = (float)(num9 + (int)(GlobalMembers.M(1f) * (float)num22 * (float)Math.Sin((double)num21)));
								num21 = (float)Math.Atan2((double)(effect4.mY - (float)num2), (double)(effect4.mX - (float)num)) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.3f);
								num20 = GlobalMembers.M(3.5f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1f);
								effect4.mDX = (float)Math.Cos((double)num21) * num20;
								effect4.mDY = (float)Math.Sin((double)num21) * num20 + GlobalMembers.M(-2f);
								effect4.mDecel = (float)(GlobalMembers.M(0.98) + (double)(GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.005f)));
							}
							else
							{
								int num23 = l * (int)((float)(l + 120) / 120f);
								num21 = (float)((double)l * 0.503 + (double)((float)(BejeweledLivePlus.Misc.Common.Rand() % 100) / 800f));
								effect4.mDX = (float)Math.Cos((double)num21) * num20 + (float)num10;
								effect4.mDY = (float)Math.Sin((double)num21) * num20 + GlobalMembers.MS(-2f) + (float)num11;
								effect4.mX = (float)(num8 + (int)(GlobalMembers.M(1.2f) * (float)num23 * effect4.mDX)) + GlobalMembers.M(14f);
								effect4.mY = (float)(num9 + (int)(GlobalMembers.M(1.2f) * (float)num23 * effect4.mDY)) + GlobalMembers.M(10f);
							}
							effect4.mAngle = num21;
							effect4.mDAngle = GlobalMembers.M(0.05f) * GlobalMembersUtils.GetRandFloat();
							effect4.mGravity = GlobalMembers.M(0.06f);
							effect4.mValue[0] = GlobalMembersUtils.GetRandFloat() * 3.14159274f * 2f;
							effect4.mValue[1] = GlobalMembersUtils.GetRandFloat() * 3.14159274f * 2f;
							effect4.mValue[2] = GlobalMembers.M(0.045f) * (GlobalMembers.M(3f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(1f));
							effect4.mValue[3] = GlobalMembers.M(0.045f) * (GlobalMembers.M(3f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(1f));
							effect4.mDAlpha = (float)(GlobalMembers.M(-0.0025) * (double)(GlobalMembers.M(2f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(4f)));
							this.mPostFXManager.AddEffect(effect4);
						}
						int num24 = GlobalMembers.M(3);
						for (int m = 0; m < num24; m++)
						{
							Effect effect5 = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_STEAM);
							float num25 = (float)m * 3.14159274f * 2f / 20f;
							float num26 = GlobalMembers.M(0.5f) + GlobalMembers.M(5.75f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
							effect5.mDX = num26 * (float)Math.Cos((double)num25) + (float)num10;
							effect5.mDY = num26 * (float)Math.Sin((double)num25) + (float)num11;
							effect5.mX = (float)num8 + (float)Math.Cos((double)num25) * GlobalMembers.M(25f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
							effect5.mY = (float)num9 + (float)Math.Sin((double)num25) * GlobalMembers.M(25f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
							effect5.mIsAdditive = false;
							effect5.mScale = GlobalMembers.M(0.5f);
							effect5.mDScale = GlobalMembers.M(0.005f);
							effect5.mValue[1] *= 1f - Math.Abs(GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.5f));
							effect5.mColor = new Color(128, 128, 128);
							this.mPostFXManager.AddEffect(effect5);
						}
						if (pieceAtScreenXY2.IsFlagSet(1U))
						{
							flag = true;
						}
					}
					if (pieceAtScreenXY2.mImmunityCount == 0)
					{
						if (pieceAtScreenXY != null)
						{
							this.SetMoveCredit(pieceAtScreenXY2, pieceAtScreenXY.mMoveCreditId);
							if (pieceAtScreenXY.mMatchId == -1)
							{
								pieceAtScreenXY.mMatchId = this.mCurMoveCreditId++;
							}
							pieceAtScreenXY2.mMatchId = pieceAtScreenXY.mMatchId;
						}
						if (flag)
						{
							if (this.WantsCalmEffects())
							{
								pieceAtScreenXY2.mExplodeDelay = GlobalMembers.M(25);
							}
							else
							{
								pieceAtScreenXY2.mExplodeDelay = GlobalMembers.M(15);
							}
							pieceAtScreenXY2.mExplodeSourceId = pieceAtScreenXY.mId;
							pieceAtScreenXY2.mExplodeSourceFlags |= pieceAtScreenXY.mFlags;
						}
						else if (pieceAtScreenXY2.IsFlagSet(16U))
						{
							pieceAtScreenXY2.mExplodeDelay = GlobalMembers.M(5);
							pieceAtScreenXY2.mExplodeSourceId = pieceAtScreenXY.mId;
							pieceAtScreenXY2.mExplodeSourceFlags |= pieceAtScreenXY.mFlags;
						}
						else if (!this.mInUReplay)
						{
							if (pieceAtScreenXY2.IsFlagSet(4U))
							{
								int num27 = this.FindStormIdxFor(pieceAtScreenXY2);
								if (num27 != -1)
								{
									LightningStorm lightningStorm = this.mLightningStorms[num27];
									if (lightningStorm.mUpdateCnt == 0 && (lightningStorm.mStormType == 0 || lightningStorm.mStormType == 1))
									{
										lightningStorm.Dispose();
										this.mLightningStorms.RemoveAt(num27);
										pieceAtScreenXY2.mDestructing = false;
									}
								}
							}
							if (((pieceAtScreenXY2.IsFlagSet(524289U) && !pieceAtScreenXY2.IsFlagSet(4U)) || !this.TriggerSpecial(pieceAtScreenXY2, pieceAtScreenXY)) && pieceAtScreenXY2.mCanDestroy)
							{
								pieceAtScreenXY2.mIsExploding = true;
								if (pieceAtScreenXY != null)
								{
									pieceAtScreenXY2.mExplodeSourceId = pieceAtScreenXY.mId;
									pieceAtScreenXY2.mExplodeSourceFlags |= pieceAtScreenXY.mFlags;
								}
								this.AddPoints((int)pieceAtScreenXY2.CX(), (int)pieceAtScreenXY2.GetScreenY(), GlobalMembers.M(20), GlobalMembers.gGemColors[pieceAtScreenXY2.mColor], (uint)pieceAtScreenXY2.mMatchId, true, true, pieceAtScreenXY2.mMoveCreditId);
								this.DeletePiece(pieceAtScreenXY2);
							}
						}
					}
				}
			}
		}

		public virtual void ExplodeAt(int theX, int theY)
		{
			GlobalMembers.gExplodeCount = 0;
			GlobalMembers.gShardCount = 0;
			if (this.WriteUReplayCmd(10))
			{
				this.mUReplayBuffer.WriteShort(this.EncodeX((float)theX));
				this.mUReplayBuffer.WriteShort(this.EncodeY((float)theY));
			}
			if (this.WantsCalmEffects())
			{
				this.BumpColumns(theX, theY, GlobalMembers.M(0.6f));
			}
			else
			{
				this.BumpColumns(theX, theY, GlobalMembers.M(1f));
			}
			this.ExplodeAtHelper(theX, theY);
			if (GlobalMembers.M(1) != 0)
			{
				PopAnimEffect popAnimEffect = PopAnimEffect.fromPopAnim(GlobalMembersResourcesWP.POPANIM_FLAMEGEMEXPLODE);
				popAnimEffect.mX = (float)theX;
				popAnimEffect.mY = (float)theY;
				popAnimEffect.mOverlay = true;
				popAnimEffect.mScale = 2f;
				if (!this.WantsCalmEffects())
				{
					popAnimEffect.mDoubleSpeed = true;
				}
				popAnimEffect.Play();
				this.mPostFXManager.AddEffect(popAnimEffect);
			}
		}

		public void SmallExplodeAt(Piece thePiece, float theCenterX, float theCenterY, bool process, bool fromFlame)
		{
			if (this.WriteUReplayCmd(11))
			{
				this.EncodePieceRef(thePiece);
			}
			int num = (int)thePiece.GetScreenX();
			int num2 = (int)thePiece.GetScreenY();
			int num3 = num + 50;
			int num4 = num2 + 50;
			if (!thePiece.IsFlagSet(6144U))
			{
				this.AddPoints(num3, num2, GlobalMembers.M(50), GlobalMembers.gGemColors[thePiece.mColor], (uint)thePiece.mMatchId, true, true, thePiece.mMoveCreditId);
			}
			if (thePiece.IsFlagSet(1024U) && !this.mInUReplay)
			{
				this.DeletePiece(thePiece);
				return;
			}
			float num5 = GlobalMembers.M(0.01f) * ((float)num3 - theCenterX);
			float num6 = GlobalMembers.M(0.01f) * ((float)num4 - theCenterY);
			if (num5 == 0f)
			{
				num6 *= GlobalMembers.M(2f);
			}
			if (num6 == 0f)
			{
				num5 *= GlobalMembers.M(2f);
			}
			if (this.WantsCalmEffects())
			{
				int num7 = GlobalMembers.M(3);
				for (int i = 0; i < num7; i++)
				{
					Effect effect = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_SPARKLE_SHARD);
					float num8 = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
					float num9 = GlobalMembers.M(0f) + GlobalMembers.M(2f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
					effect.mDX = num9 * (float)Math.Cos((double)num8) + num5;
					effect.mDY = num9 * (float)Math.Sin((double)num8) + num6;
					if (fromFlame)
					{
						effect.mDX *= GlobalMembers.M(2.5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect.mDY *= GlobalMembers.M(2.5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						float num10 = Math.Abs(GlobalMembersUtils.GetRandFloat());
						num10 *= num10;
						effect.mX = num10 * theCenterX + (1f - num10) * (float)num3 + (float)Math.Cos((double)num8) * GlobalMembers.M(64f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect.mY = num10 * theCenterY + (1f - num10) * (float)num4 + (float)Math.Sin((double)num8) * GlobalMembers.M(64f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(96), GlobalMembers.M(32), GlobalMembers.M(64));
						effect.mIsAdditive = true;
						effect.mDScale = GlobalMembers.M(0.015f);
					}
					else
					{
						effect.mX = (float)num3 + (float)Math.Cos((double)num8) * GlobalMembers.M(24f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect.mY = (float)num4 + (float)Math.Sin((double)num8) * GlobalMembers.M(24f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect.mColor = GlobalMembers.gGemColors[thePiece.mColor];
						effect.mIsAdditive = false;
						effect.mDScale = GlobalMembers.M(0.02f);
					}
					effect.mScale = GlobalMembers.M(0.1f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1f);
					this.mPostFXManager.AddEffect(effect);
				}
			}
			else
			{
				int num11 = GlobalMembers.M(3);
				for (int j = 0; j < num11; j++)
				{
					Effect effect2 = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_STEAM);
					float num12 = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
					float num13 = GlobalMembers.M(0f) + GlobalMembers.M(2f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
					effect2.mDX = num13 * (float)Math.Cos((double)num12) + num5;
					effect2.mDY = num13 * (float)Math.Sin((double)num12) + num6;
					if (fromFlame)
					{
						effect2.mDX *= GlobalMembers.M(2.5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect2.mDY *= GlobalMembers.M(2.5f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						float num14 = Math.Abs(GlobalMembersUtils.GetRandFloat());
						num14 *= num14;
						effect2.mX = num14 * theCenterX + (1f - num14) * (float)num3 + (float)Math.Cos((double)num12) * GlobalMembers.M(64f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect2.mY = num14 * theCenterY + (1f - num14) * (float)num4 + (float)Math.Sin((double)num12) * GlobalMembers.M(64f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect2.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(96), GlobalMembers.M(32), GlobalMembers.M(64));
						effect2.mIsAdditive = true;
						effect2.mDScale = GlobalMembers.M(0.015f);
					}
					else
					{
						effect2.mX = (float)num3 + (float)Math.Cos((double)num12) * GlobalMembers.M(24f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect2.mY = (float)num4 + (float)Math.Sin((double)num12) * GlobalMembers.M(24f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
						effect2.mColor = GlobalMembers.gGemColors[thePiece.mColor];
						effect2.mIsAdditive = false;
						effect2.mDScale = GlobalMembers.M(0.02f);
					}
					effect2.mScale = GlobalMembers.M(0.1f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1f);
					this.mPostFXManager.AddEffect(effect2);
				}
			}
			int num15 = GlobalMembers.M(3);
			for (int k = 0; k < num15; k++)
			{
				Effect effect3 = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_GEM_SHARD);
				effect3.mColor = GlobalMembers.gGemColors[thePiece.mColor];
				float num16 = GlobalMembers.M(1.2f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1.2f);
				float num17;
				if (num5 != 0f || num6 != 0f)
				{
					num17 = GlobalMembersUtils.GetRandFloat() * 3.14159f;
					int num18 = (int)(GlobalMembersUtils.GetRandFloat() * GlobalMembers.S(GlobalMembers.M(48f)));
					effect3.mX = (float)(num3 + (int)(GlobalMembers.M(1f) * (float)num18 * (float)Math.Cos((double)num17)));
					effect3.mY = (float)(num4 + (int)(GlobalMembers.M(1f) * (float)num18 * (float)Math.Sin((double)num17)));
					num17 = (float)Math.Atan2((double)(effect3.mY - theCenterY), (double)(effect3.mX - theCenterX)) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.3f);
					num16 = GlobalMembers.M(3.5f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(1f);
					effect3.mDX = (float)Math.Cos((double)num17) * num16;
					effect3.mDY = (float)Math.Sin((double)num17) * num16 + GlobalMembers.M(-2f);
					effect3.mDecel = (float)(GlobalMembers.M(0.98) + (double)(GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.005f)));
				}
				else
				{
					int num19 = GlobalMembers.S(k * (int)((float)(k + 120) / 120f));
					num17 = (float)k * 0.503f + (float)(BejeweledLivePlus.Misc.Common.Rand() % 100) / 800f;
					effect3.mDX = (float)Math.Cos((double)num17) * num16 + num5;
					effect3.mDY = (float)Math.Sin((double)num17) * num16 + GlobalMembers.M(-2f) + num6;
					effect3.mX = (float)(num3 + (int)(GlobalMembers.M(1.2f) * (float)num19 * effect3.mDX)) + GlobalMembers.M(14f);
					effect3.mY = (float)(num4 + (int)(GlobalMembers.M(1.2f) * (float)num19 * effect3.mDY)) + GlobalMembers.M(10f);
				}
				effect3.mAngle = num17;
				effect3.mDAngle = GlobalMembers.M(0.05f) * GlobalMembersUtils.GetRandFloat();
				effect3.mGravity = GlobalMembers.M(0.06f);
				effect3.mValue[0] = GlobalMembersUtils.GetRandFloat() * 3.14159274f * 2f;
				effect3.mValue[1] = GlobalMembersUtils.GetRandFloat() * 3.14159274f * 2f;
				effect3.mValue[2] = GlobalMembers.M(0.045f) * (GlobalMembers.M(3f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(1f));
				effect3.mValue[3] = GlobalMembers.M(0.045f) * (GlobalMembers.M(3f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(1f));
				effect3.mDAlpha = GlobalMembers.M(-0.0025f) * (GlobalMembers.M(2f) * Math.Abs(GlobalMembersUtils.GetRandFloat()) + GlobalMembers.M(4f));
				this.mPostFXManager.AddEffect(effect3);
			}
			int num20 = GlobalMembers.M(3);
			for (int l = 0; l < num20; l++)
			{
				Effect effect4 = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_STEAM);
				float num21 = (float)l * 3.14159274f * 2f / 20f;
				float num22 = GlobalMembers.M(0.5f) + GlobalMembers.M(5.75f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
				effect4.mDX = num22 * (float)Math.Cos((double)num21) + num5;
				effect4.mDY = num22 * (float)Math.Sin((double)num21) + num6;
				effect4.mX = (float)num3 + (float)Math.Cos((double)num21) * GlobalMembers.M(25f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
				effect4.mY = (float)num4 + (float)Math.Sin((double)num21) * GlobalMembers.M(25f) * Math.Abs(GlobalMembersUtils.GetRandFloat());
				effect4.mIsAdditive = false;
				effect4.mScale = GlobalMembers.M(0.5f);
				effect4.mDScale = GlobalMembers.M(0.005f);
				effect4.mValue[1] *= 1f - Math.Abs(GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.5f));
				effect4.mColor = new Color(128, 128, 128);
				this.mPostFXManager.AddEffect(effect4);
			}
			if (thePiece.mElectrocutePercent > 0f)
			{
				EffectsManager effectsManager = this.mPostFXManager;
				int num23 = GlobalMembers.M(3);
				for (int m = 0; m < num23; m++)
				{
					Effect effect5 = effectsManager.AllocEffect(Effect.Type.TYPE_FRUIT_SPARK);
					float num24 = GlobalMembers.M(1f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(2f);
					float num25 = GlobalMembersUtils.GetRandFloat() * 3.14159274f;
					effect5.mScale = GlobalMembers.M(1f) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.5f);
					effect5.mDX = num24 * (float)Math.Cos((double)num25);
					effect5.mDY = num24 * (float)Math.Sin((double)num25);
					effect5.mX = (float)(num + 50) + (float)Math.Cos((double)num25) * 100f / 2f;
					effect5.mY = (float)(num2 + 50) + (float)Math.Sin((double)num25) * 100f / 2f;
					effect5.mIsAdditive = true;
					effect5.mAlpha = 1f;
					effect5.mDAlpha = GlobalMembers.M(-0.005f);
					effect5.mGravity = 0f;
					effectsManager.AddEffect(effect5);
				}
			}
			if (!this.mInUReplay)
			{
				thePiece.mIsExploding = true;
				this.DeletePiece(thePiece);
			}
		}

		public bool FindRandomMove(int[] theCoords)
		{
			return this.FindRandomMove(theCoords, false);
		}

		public bool FindRandomMove(int[] theCoords, bool thePowerGemMove)
		{
			bool flag = BejeweledLivePlus.Misc.Common.Rand(2) == 0;
			int num = BejeweledLivePlus.Misc.Common.Rand(10);
			for (int i = 0; i < 2; i++)
			{
				for (int j = num; j >= 0; j--)
				{
					if (this.FindMove(theCoords, j, true, true, flag, null, thePowerGemMove))
					{
						return true;
					}
				}
				for (int k = num; k >= 0; k--)
				{
					if (this.FindMove(theCoords, k, true, true, !flag, null, thePowerGemMove))
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool FindMove(int[] theCoords, int theMoveNum, bool horz, bool vert, bool reverse, Piece theIncludePiece)
		{
			return this.FindMove(theCoords, theMoveNum, horz, vert, reverse, theIncludePiece, false);
		}

		public bool FindMove(int[] theCoords, int theMoveNum, bool horz, bool vert, bool reverse)
		{
			return this.FindMove(theCoords, theMoveNum, horz, vert, reverse, null, false);
		}

		public bool FindMove(int[] theCoords, int theMoveNum, bool horz, bool vert)
		{
			return this.FindMove(theCoords, theMoveNum, horz, vert, false, null, false);
		}

		public bool FindMove(int[] theCoords, int theMoveNum, bool horz, bool vert, bool reverse, Piece theIncludePiece, bool powerGemMove)
		{
			int num = 0;
			int num2 = (reverse ? 7 : 0);
			int num3 = (reverse ? (-1) : 8);
			int num4 = num2;
			while (num4 != num3)
			{
				for (int i = 0; i < 8; i++)
				{
					Piece piece = this.mBoard[num4, i];
					if (piece != null && (piece == null || (this.WillPieceBeStill(piece) && !piece.IsFlagSet(256U) && piece.mCanSwap)))
					{
						for (int j = 0; j < 4; j++)
						{
							int num5 = i + Board.FM_aSwapArray[j, 0];
							int num6 = num4 + Board.FM_aSwapArray[j, 1];
							if (num5 >= 0 && num5 < 8 && num6 >= 0 && num6 < 8)
							{
								Piece piece2 = piece;
								bool flag = false;
								bool flag2 = theIncludePiece == null;
								if (piece != null && piece.IsFlagSet(2U) && this.mBoard[num6, num5] != null)
								{
									if (theIncludePiece != null && piece.mColor == theIncludePiece.mColor)
									{
										flag2 = true;
									}
									flag = true;
								}
								if (this.mBoard[num6, num5] != null && this.mBoard[num6, num5].mColor != -1 && this.WillPieceBeStill(this.mBoard[num6, num5]))
								{
									this.mBoard[num4, i] = this.mBoard[num6, num5];
									this.mBoard[num6, num5] = piece2;
									flag2 |= theIncludePiece == this.mBoard[num4, i];
									int num7 = i;
									int num8 = i;
									while (num7 > 0 && this.mBoard[num4, num7 - 1] != null && this.mBoard[num4, i].mColor == this.mBoard[num4, num7 - 1].mColor)
									{
										if (!this.WillPieceBeStill(this.mBoard[num4, num7 - 1]))
										{
											break;
										}
										flag2 |= theIncludePiece == this.mBoard[num4, num7 - 1];
										num7--;
									}
									while (num8 < 7 && this.mBoard[num4, num8 + 1] != null && this.mBoard[num4, i].mColor == this.mBoard[num4, num8 + 1].mColor && this.WillPieceBeStill(this.mBoard[num4, num8 + 1]))
									{
										flag2 |= theIncludePiece == this.mBoard[num4, num8 + 1];
										num8++;
									}
									int num9 = num4;
									int num10 = num4;
									while (num9 > 0 && this.mBoard[num9 - 1, i] != null && this.mBoard[num4, i].mColor == this.mBoard[num9 - 1, i].mColor)
									{
										if (!this.WillPieceBeStill(this.mBoard[num9 - 1, i]))
										{
											break;
										}
										flag2 |= theIncludePiece == this.mBoard[num9 - 1, i];
										num9--;
									}
									while (num10 < 7 && this.mBoard[num10 + 1, i] != null && this.mBoard[num4, i].mColor == this.mBoard[num10 + 1, i].mColor && this.WillPieceBeStill(this.mBoard[num10 + 1, i]))
									{
										flag2 |= theIncludePiece == this.mBoard[num10 + 1, i];
										num10++;
									}
									piece2 = this.mBoard[num4, i];
									this.mBoard[num4, i] = this.mBoard[num6, num5];
									this.mBoard[num6, num5] = piece2;
									if (powerGemMove)
									{
										if ((num8 - num7 >= 3 && horz) || (num10 - num9 >= 3 && vert))
										{
											flag = true;
										}
										if (num8 - num7 >= 2 && horz && num10 - num9 >= 2 && vert)
										{
											flag = true;
										}
									}
									else if ((num8 - num7 >= 2 && horz) || (num10 - num9 >= 2 && vert))
									{
										flag = true;
									}
								}
								if (flag && flag2)
								{
									if (num == theMoveNum)
									{
										if (theCoords != null)
										{
											theCoords[0] = i;
											theCoords[1] = num4;
											theCoords[2] = num5;
											theCoords[3] = num6;
										}
										return true;
									}
									num++;
								}
							}
						}
					}
				}
				if (reverse)
				{
					num4--;
				}
				else
				{
					num4++;
				}
			}
			return false;
		}

		public bool HasSet()
		{
			return this.HasSet(null);
		}

		public bool HasSet(Piece theCheckPiece)
		{
			for (int i = 0; i < 8; i++)
			{
				int num = 0;
				int num2 = -1;
				bool flag = false;
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					if (piece != null)
					{
						if (piece.mColor != -1 && piece.mColor == num2)
						{
							flag |= piece == theCheckPiece;
							if (++num >= 3 && flag)
							{
								return true;
							}
						}
						else
						{
							num2 = piece.mColor;
							num = 1;
							flag = piece == theCheckPiece || theCheckPiece == null;
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
					Piece piece2 = this.mBoard[i, j];
					if (piece2 != null)
					{
						if (piece2.mColor != -1 && piece2.mColor == num4)
						{
							flag2 |= piece2 == theCheckPiece;
							if (++num3 >= 3 && flag2)
							{
								return true;
							}
						}
						else
						{
							num4 = piece2.mColor;
							num3 = 1;
							flag2 = piece2 == theCheckPiece || theCheckPiece == null;
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

		public virtual bool HasIllegalSet()
		{
			for (int i = 0; i < 8; i++)
			{
				int num = 0;
				int num2 = -1;
				bool flag = false;
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					if (piece != null)
					{
						bool flag2 = piece.mCreatedTick == this.mUpdateCnt && piece.IsFlagSet(16U);
						if (piece.mColor != -1 && piece.mColor == num2)
						{
							flag = flag || flag2;
							if (++num >= 3 && flag)
							{
								return true;
							}
						}
						else
						{
							num2 = piece.mColor;
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
					Piece piece2 = this.mBoard[i, j];
					if (piece2 != null)
					{
						bool flag4 = piece2.mCreatedTick == this.mUpdateCnt && piece2.IsFlagSet(16U);
						if (piece2.mColor != -1 && piece2.mColor == num4)
						{
							flag3 = flag3 || flag4;
							if (++num3 >= 3 && flag3)
							{
								return true;
							}
						}
						else
						{
							num4 = piece2.mColor;
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

		public virtual bool TriggerSpecial(Piece thePiece)
		{
			return this.TriggerSpecial(thePiece, null);
		}

		public virtual bool TriggerSpecial(Piece thePiece, Piece theSrc)
		{
			if (thePiece.mDestructing)
			{
				return false;
			}
			if (thePiece.IsFlagSet(1U) && !thePiece.IsFlagSet(4U))
			{
				thePiece.mExplodeDelay = 1;
				thePiece.mExplodeSourceId = ((theSrc != null) ? theSrc.mId : (-1));
				thePiece.mExplodeSourceFlags |= ((theSrc != null) ? theSrc.mFlags : (-1));
				return true;
			}
			if (thePiece.IsFlagSet(524288U))
			{
				thePiece.mExplodeDelay = 1;
				thePiece.mExplodeSourceId = ((theSrc != null) ? theSrc.mId : (-1));
				thePiece.mExplodeSourceFlags |= ((theSrc != null) ? theSrc.mFlags : (-1));
				return true;
			}
			if (thePiece.IsFlagSet(2U) && this.FindStormIdxFor(thePiece) == -1)
			{
				int theColor;
				if (theSrc != null)
				{
					if (theSrc.mColor == -1)
					{
						theColor = theSrc.mLastColor;
					}
					else
					{
						theColor = theSrc.mColor;
					}
				}
				else if (thePiece.mLastColor != -1)
				{
					theColor = thePiece.mLastColor;
				}
				else
				{
					theColor = (int)(this.mRand.Next() % 7U);
				}
				this.DoHypercube(thePiece, theColor);
				return true;
			}
			if (thePiece.IsFlagSet(4U) && this.FindStormIdxFor(thePiece) == -1)
			{
				thePiece.mDestructing = true;
				if (thePiece.IsFlagSet(1U))
				{
					this.AddToStat(31, 1, thePiece.mMoveCreditId);
				}
				this.AddToStat(13, 1, thePiece.mMoveCreditId);
				LightningStorm lightningStorm = new LightningStorm(this, thePiece, 2);
				this.mLightningStorms.Add(lightningStorm);
				this.EncodeLightningStorm(lightningStorm);
				return true;
			}
			if (thePiece.IsFlagSet(16U))
			{
				thePiece.mDestructing = true;
				thePiece.mExplodeDelay = 1;
				thePiece.mExplodeSourceId = ((theSrc != null) ? theSrc.mId : (-1));
				thePiece.mExplodeSourceFlags |= ((theSrc != null) ? theSrc.mFlags : (-1));
				return true;
			}
			return false;
		}

		public int FindSets(bool fromUpdateSwapping, Piece thePiece1)
		{
			return this.FindSets(fromUpdateSwapping, thePiece1, null);
		}

		public int FindSets(bool fromUpdateSwapping)
		{
			return this.FindSets(fromUpdateSwapping, null, null);
		}

		public int FindSets()
		{
			return this.FindSets(false, null, null);
		}

		public virtual int FindSets(bool fromUpdateSwapping, Piece thePiece1, Piece thePiece2)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			this.FS_aBulgeTriggerPieceSet.Clear();
			this.FS_aDelayingPieceSet.Clear();
			this.FS_aTallyPieceSet.Clear();
			this.FS_aPowerupPieceSet.Clear();
			this.FS_aMatchedSets.Clear();
			bool flag2 = false;
			foreach (SwapData swapData in this.mSwapDataVector)
			{
				if (!swapData.mSwapPct.HasBeenTriggered() && swapData.mForceSwap)
				{
					flag2 = true;
				}
			}
			this.FS_aMoveCreditSet.Clear();
			this.FS_aDeferPowerupMap.Clear();
			this.FS_aDeferLaserSet.Clear();
			this.FS_aDeferExplodeVector.Clear();
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					bool flag3 = false;
					int num5 = 0;
					int num6 = -1;
					int num7 = 0;
					int num8 = 0;
					int num9 = 0;
					int num10 = 0;
					for (int k = 0; k < 8; k++)
					{
						int num11;
						int num12;
						if (i == 0)
						{
							num11 = j;
							num12 = k;
						}
						else
						{
							num11 = k;
							num12 = j;
						}
						int num13 = -1;
						bool flag4 = false;
						Piece piece = this.mBoard[num12, num11];
						bool flag5 = piece != null && (this.IsPieceStill(piece) | this.FS_aTallyPieceSet.ContainsKey(piece)) && !this.FS_aDelayingPieceSet.ContainsKey(piece);
						bool flag6 = piece != null && (this.WillPieceBeStill(piece) || this.FS_aTallyPieceSet.ContainsKey(piece));
						if (flag6)
						{
							if (piece.mChangedTick == this.mUpdateCnt)
							{
								num13 = piece.mLastColor;
							}
							else
							{
								num13 = piece.mColor;
							}
							if (num13 == num6 && num13 != -1)
							{
								flag3 |= !flag5 || flag2;
								num10 = num11;
								num9 = num12;
								flag4 = true;
								num5++;
							}
						}
						if (!flag4 || k == 7)
						{
							if (num5 >= 3)
							{
								int num14 = 0;
								bool flag7 = false;
								MatchSet matchSet = new MatchSet();
								bool flag8 = false;
								bool flag9 = false;
								int num15 = -1;
								int num16 = -1;
								matchSet.mMatchId = this.mCurMoveCreditId++;
								matchSet.mMoveCreditId = -1;
								matchSet.mExplosionCount = 0;
								bool flag10 = false;
								for (int l = num7; l <= num9; l++)
								{
									for (int m = num8; m <= num10; m++)
									{
										Piece piece2 = this.mBoard[l, m];
										if (piece2 != null)
										{
											flag10 |= this.FS_aBulgeTriggerPieceSet.ContainsKey(piece2);
											int num17 = 0;
											bool flag11 = false;
											int[,] array = new int[,]
											{
												{ -1, 0 },
												{ 1, 0 },
												{ 0, -1 },
												{ 0, 1 }
											};
											for (int n = 0; n < 4; n++)
											{
												for (int num18 = 1; num18 < 8; num18++)
												{
													Piece pieceAtRowCol = this.GetPieceAtRowCol(piece2.mRow + array[n, 0] * num18, piece2.mCol + array[n, 1] * num18);
													if (pieceAtRowCol == null || pieceAtRowCol.mColor != piece2.mColor)
													{
														break;
													}
													bool flag12 = !this.IsPieceStill(pieceAtRowCol) && this.WillPieceBeStill(pieceAtRowCol);
													if (n / 2 != i)
													{
														num17++;
														flag11 = flag11 || flag12;
													}
													else
													{
														flag9 = flag9 || flag12;
													}
												}
											}
											flag9 |= num17 >= 2 && flag11;
											if (piece2.mColor == num6)
											{
												flag7 |= piece2.mSwapTick == this.mUpdateCnt;
												matchSet.mPieces.Add(piece2);
											}
											num15 = Math.Max(num15, piece2.mMoveCreditId);
											num16 = Math.Max(num16, piece2.mLastMoveCreditId);
										}
									}
								}
								if (num15 == -1)
								{
									num15 = num16;
								}
								matchSet.mMoveCreditId = num15;
								if (flag3 || flag9)
								{
									flag = true;
									for (int num19 = num7; num19 <= num9; num19++)
									{
										for (int num20 = num8; num20 <= num10; num20++)
										{
											Piece piece3 = this.mBoard[num19, num20];
											if (piece3 != null)
											{
												piece3.mMoveCreditId = num15;
												this.FS_aDelayingPieceSet[piece3] = 0;
											}
										}
									}
								}
								else
								{
									num++;
									if (!flag7)
									{
										num2++;
									}
									List<Piece> list = new List<Piece>();
									List<Piece> list2 = new List<Piece>();
									for (int num21 = num7; num21 <= num9; num21++)
									{
										for (int num22 = num8; num22 <= num10; num22++)
										{
											Piece piece4 = this.mBoard[num21, num22];
											if (piece4 != null && piece4.IsFlagSet(16U))
											{
												if (this.WriteUReplayCmd(13))
												{
													this.EncodePieceRef(piece4);
												}
												this.IncPointMult(piece4);
												piece4.ClearFlag(16U);
												this.mPostFXManager.FreePieceEffect(piece4.mId);
											}
										}
									}
									for (int num23 = num7; num23 <= num9; num23++)
									{
										for (int num24 = num8; num24 <= num10; num24++)
										{
											Piece piece5 = this.mBoard[num23, num24];
											if (piece5 != null)
											{
												piece5.mMatchId = matchSet.mMatchId;
												piece5.mMoveCreditId = num15;
												if (piece5.IsFlagSet(1024U))
												{
													this.TallyCoin(piece5);
													piece5.ClearFlag(1024U);
												}
												if (!flag8)
												{
													flag8 = this.ComboProcess(piece5.mColor);
												}
												bool flag13 = false;
												if (piece5.IsFlagSet(524289U))
												{
													this.FS_aDeferExplodeVector.Add(piece5);
													matchSet.mExplosionCount++;
												}
												if (piece5.IsFlagSet(4U) && piece5.mChangedTick != this.mUpdateCnt)
												{
													int num25 = this.FindStormIdxFor(piece5);
													if (num25 == -1)
													{
														this.AddToStat(13, 1, num15);
														LightningStorm lightningStorm = new LightningStorm(this, piece5, (this.mFullLaser != 0) ? 2 : 3);
														this.mLightningStorms.Add(lightningStorm);
														this.EncodeLightningStorm(lightningStorm);
													}
												}
												else if (piece5.mChangedTick == this.mUpdateCnt && piece5.mColor > -1 && piece5.mColor < 7)
												{
													if (piece5.mFlags == 0 || piece5.IsFlagSet(1U) || piece5.IsFlagSet(128U) || piece5.IsFlagSet(131072U))
													{
														if (piece5.IsFlagSet(128U))
														{
															this.mForceReleaseButterfly = true;
															this.mForcedReleasedBflyPiece = piece5;
														}
														if (this.AllowPowerups())
														{
															if (this.WantsTutorial(2))
															{
																this.DeferTutorialDialog(2, piece5);
																flag10 = true;
															}
															else if (!flag10)
															{
																piece5.mScale.SetConstant(1.0);
																piece5.mChangedTick = this.mUpdateCnt;
																this.FS_aPowerupPieceSet[piece5] = 0;
																piece5.mMoveCreditId = num15;
																this.FS_aDeferLaserSet[piece5] = 0;
															}
														}
														flag13 = false;
													}
												}
												else if (piece5.mChangedTick != this.mUpdateCnt)
												{
													flag13 = piece5.mFlags == 0;
													if (flag7)
													{
														GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_A, piece5.mScale);
													}
													else if (this.WantBulgeCascades())
													{
														GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_B, piece5.mScale);
													}
													else
													{
														GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_C, piece5.mScale);
													}
													if (!GlobalMembers.gIs3D)
													{
														piece5.mRotPct = 0f;
													}
													piece5.mScale.mIncRate *= (double)this.GetMatchSpeed();
													piece5.mChangedTick = this.mUpdateCnt;
													piece5.mLastColor = piece5.mColor;
													this.FS_aTallyPieceSet[piece5] = 0;
												}
												if (flag13)
												{
													if (piece5.mLastActiveTick > num14)
													{
														num14 = piece5.mLastActiveTick;
														list2.Clear();
													}
													if (piece5.mLastActiveTick == num14)
													{
														list2.Add(piece5);
													}
													list.Add(piece5);
												}
											}
										}
									}
									if (list.Count > 0)
									{
										Piece piece6 = list2[list2.Count / 2];
										for (int num26 = 0; num26 < list.Count; num26++)
										{
											Piece piece7 = list[num26];
											Piece pieceAtRowCol2;
											Piece pieceAtRowCol3;
											if (i == 0)
											{
												pieceAtRowCol2 = this.GetPieceAtRowCol(piece7.mRow, piece7.mCol + 1);
												pieceAtRowCol3 = this.GetPieceAtRowCol(piece7.mRow, piece7.mCol - 1);
											}
											else
											{
												pieceAtRowCol2 = this.GetPieceAtRowCol(piece7.mRow + 1, piece7.mCol);
												pieceAtRowCol3 = this.GetPieceAtRowCol(piece7.mRow - 1, piece7.mCol);
											}
											if (pieceAtRowCol2 != null && pieceAtRowCol2.mColor == piece7.mColor && pieceAtRowCol3 != null && pieceAtRowCol3.mColor == piece7.mColor)
											{
												piece6 = piece7;
											}
										}
										int num27 = Math.Max(Math.Abs(num8 - piece6.mCol), Math.Abs(num7 - piece6.mRow));
										int num28 = Math.Max(Math.Abs(num10 - piece6.mCol), Math.Abs(num9 - piece6.mRow));
										Piece piece8 = null;
										for (int num29 = 1; num29 <= 2; num29++)
										{
											if (num27 > num28)
											{
												if (i == 0)
												{
													piece8 = this.GetPieceAtRowCol(Math.Max(num7, piece6.mRow - num29), piece6.mCol);
												}
												else
												{
													piece8 = this.GetPieceAtRowCol(piece6.mRow, Math.Max(num8, piece6.mCol - num29));
												}
											}
											else if (i == 0)
											{
												piece8 = this.GetPieceAtRowCol(Math.Min(num9, piece6.mRow + num29), piece6.mCol);
											}
											else
											{
												piece8 = this.GetPieceAtRowCol(piece6.mRow, Math.Min(num10, piece6.mCol + num29));
											}
											if (piece8 != null && piece8.mFlags == 0)
											{
												break;
											}
										}
										if (this.AllowPowerups())
										{
											if (num5 >= 6 && piece6.CanSetFlag(4U) && piece6.CanSetFlag(1U))
											{
												if (this.WantsTutorial(6))
												{
													this.DeferTutorialDialog(6, piece6);
													flag10 = true;
												}
												else if (!flag10)
												{
													piece6.mMoveCreditId = num15;
													if (piece8 != null)
													{
														piece8.mMoveCreditId = num15;
													}
													this.FS_aDeferPowerupMap[piece6] = new Pair<int, Piece>(num5, piece8);
												}
											}
											else if (num5 >= 5 && !piece6.IsFlagSet(4U) && !this.FS_aBulgeTriggerPieceSet.ContainsKey(piece6))
											{
												if (this.WantsTutorial(3))
												{
													this.DeferTutorialDialog(3, piece6);
													flag10 = true;
												}
												else if (!flag10)
												{
													piece6.mMoveCreditId = num15;
													if (piece8 != null)
													{
														piece8.mMoveCreditId = num15;
													}
													this.FS_aDeferPowerupMap[piece6] = new Pair<int, Piece>(num5, piece8);
												}
											}
											else if (num5 >= 4)
											{
												if (this.WantsTutorial(1))
												{
													this.DeferTutorialDialog(1, piece6);
													flag10 = true;
												}
												else if (!flag10)
												{
													piece6.mMoveCreditId = num15;
													if (piece8 != null)
													{
														piece8.mMoveCreditId = num15;
													}
													this.FS_aDeferPowerupMap[piece6] = new Pair<int, Piece>(num5, piece8);
												}
											}
										}
										if (this.CreateMatchPowerup(num5, piece6, this.FS_aTallyPieceSet))
										{
											piece6.mScale.SetConstant(1.0);
										}
									}
									if (flag10)
									{
										for (int num30 = num7; num30 <= num9; num30++)
										{
											for (int num31 = num8; num31 <= num10; num31++)
											{
												Piece piece9 = this.mBoard[num30, num31];
												if (piece9 != null)
												{
													this.FS_aBulgeTriggerPieceSet[piece9] = 0;
													if (this.FS_aDeferLaserSet.ContainsKey(piece9))
													{
														this.FS_aDeferLaserSet.Remove(piece9);
													}
												}
											}
										}
									}
									this.FS_aMatchedSets.Add(matchSet);
								}
							}
							num6 = num13;
							num5 = 1;
							flag3 = !flag5;
							num8 = num11;
							num7 = num12;
							num10 = num11;
							num9 = num12;
						}
					}
				}
			}
			for (int num32 = 0; num32 < this.FS_aDeferExplodeVector.Count; num32++)
			{
				Piece piece10 = this.FS_aDeferExplodeVector[num32];
				piece10.mExplodeDelay = 1;
				piece10.mExplodeSourceId = piece10.mId;
				piece10.mExplodeSourceFlags |= piece10.mFlags;
				piece10.mScale.SetConstant(1.0);
			}
			foreach (Piece piece11 in this.FS_aDeferLaserSet.Keys)
			{
				if (piece11.IsFlagSet(524289U))
				{
					piece11.mExplodeDelay = 1;
					piece11.mExplodeSourceId = piece11.mId;
					piece11.mExplodeSourceFlags = piece11.mFlags;
					piece11.mFlags = 0;
				}
				if (this.AllowLaserGems())
				{
					this.AddToStat(4, 1, piece11.mMoveCreditId);
					this.AddToStat(18, 1, piece11.mMoveCreditId);
					this.Laserify(piece11);
				}
				else
				{
					this.AddToStat(4, 1, piece11.mMoveCreditId);
					this.AddToStat(17, 1, piece11.mMoveCreditId);
					this.Flamify(piece11);
				}
			}
			foreach (KeyValuePair<Piece, Pair<int, Piece>> keyValuePair in this.FS_aDeferPowerupMap)
			{
				Piece piece12 = keyValuePair.Key;
				Dictionary<Piece, Pair<int, Piece>>.Enumerator enumerator3;
				if (piece12.IsFlagSet(4U))
				{
					keyValuePair = enumerator3.Current;
					piece12 = keyValuePair.Value.second;
				}
				if (piece12.mFlags == 0 && !this.FS_aBulgeTriggerPieceSet.ContainsKey(piece12))
				{
					this.FS_aPowerupPieceSet[piece12] = 0;
					keyValuePair = enumerator3.Current;
					if (keyValuePair.Value.first > 5)
					{
						this.AddToStat(4, 1, piece12.mMoveCreditId);
						this.AddToStat(17, 1, piece12.mMoveCreditId);
						this.AddToStat(18, 1, piece12.mMoveCreditId);
						this.AddToStat(30, 1, piece12.mMoveCreditId);
						this.Laserify(piece12);
						this.Flamify(piece12);
						piece12.mScale.SetConstant(1.0);
					}
					else
					{
						keyValuePair = enumerator3.Current;
						if (keyValuePair.Value.first > 4)
						{
							this.AddToStat(4, 1, piece12.mMoveCreditId);
							this.AddToStat(19, 1, piece12.mMoveCreditId);
							this.Hypercubeify(piece12);
							piece12.mScale.SetConstant(1.0);
						}
						else
						{
							this.Flamify(piece12);
							this.AddToStat(4, 1, piece12.mMoveCreditId);
							this.AddToStat(17, 1, piece12.mMoveCreditId);
						}
					}
					piece12.mChangedTick = this.mUpdateCnt;
					if (piece12.mColor != -1)
					{
						piece12.mLastColor = piece12.mColor;
					}
					piece12.mScale.SetConstant(1.0);
				}
			}
			if (this.FS_aMatchedSets.Count > 0)
			{
				this.ProcessMatches(this.FS_aMatchedSets, this.FS_aTallyPieceSet, fromUpdateSwapping);
			}
			bool flag14 = false;
			for (int num33 = 0; num33 < this.FS_aMatchedSets.Count; num33++)
			{
				MatchSet matchSet2 = this.FS_aMatchedSets[num33];
				Piece piece13 = null;
				bool flag15 = false;
				bool flag16 = false;
				int num34 = 0;
				int num35 = 0;
				bool flag17 = false;
				for (int num36 = 0; num36 < matchSet2.mPieces.Count; num36++)
				{
					Piece piece14 = matchSet2.mPieces[num36];
					flag17 |= piece14.mSwapTick == this.mUpdateCnt;
					num34 += (int)piece14.GetScreenX();
					num35 += (int)piece14.GetScreenY();
					if (this.FS_aPowerupPieceSet.ContainsKey(piece14))
					{
						piece13 = piece14;
					}
					if (piece14.IsFlagSet(4U) && piece14.mChangedTick != this.mUpdateCnt)
					{
						flag15 = true;
					}
					if (this.FS_aBulgeTriggerPieceSet.ContainsKey(piece14))
					{
						flag16 = true;
					}
				}
				if (this.WriteUReplayCmd(2))
				{
					int num37 = 0;
					if (fromUpdateSwapping)
					{
						num37 |= 1;
					}
					if (flag17)
					{
						num37 |= 2;
					}
					if (flag16)
					{
						num37 |= 4;
					}
					if (flag15)
					{
						num37 |= 8;
					}
					this.mUReplayBuffer.WriteByte((byte)num37);
					if (!flag17)
					{
						this.mUReplayBuffer.WriteByte((byte)this.GetMaxMovesStat(27));
					}
					this.mUReplayBuffer.WriteByte((byte)matchSet2.mPieces.Count);
					for (int num38 = 0; num38 < matchSet2.mPieces.Count; num38++)
					{
						Piece piece15 = matchSet2.mPieces[num38];
						this.EncodePieceRef(piece15);
						if (piece15.IsFlagSet(7U) && !piece15.mDestructing && !this.FS_aTallyPieceSet.ContainsKey(piece15))
						{
							this.mUReplayBuffer.WriteByte(this.EncodePieceFlags(piece15.mFlags));
						}
						else
						{
							this.mUReplayBuffer.WriteByte(0);
						}
					}
				}
				if (fromUpdateSwapping && this.mSpeedBonusFlameModePct > 0f)
				{
					int num39 = 0;
					Piece piece16 = null;
					for (int num40 = 0; num40 < matchSet2.mPieces.Count; num40++)
					{
						Piece piece17 = matchSet2.mPieces[num40];
						if (piece17.mSwapTick > num39)
						{
							piece16 = piece17;
							num39 = piece17.mSwapTick;
						}
					}
					if (piece16 != null)
					{
						piece16.SetFlag(32768U);
						this.AddToStat(16, 1);
						piece16.mExplodeDelay = 1;
						piece16.mExplodeSourceId = piece16.mId;
						piece16.mExplodeSourceFlags |= piece16.mFlags;
					}
				}
				if (flag16)
				{
					for (int num41 = 0; num41 < matchSet2.mPieces.Count; num41++)
					{
						Piece piece18 = matchSet2.mPieces[num41];
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_D, piece18.mScale);
						piece18.mIsBulging = true;
						piece18.mExplodeDelay = 0;
						piece18.mExplodeSourceId = piece18.mId;
						piece18.mExplodeSourceFlags |= piece18.mFlags;
						int num42 = this.FindStormIdxFor(piece18);
						if (num42 != -1)
						{
							if (this.mLightningStorms[num42] != null)
							{
								this.mLightningStorms[num42].Dispose();
							}
							this.mLightningStorms.RemoveAt(num42);
						}
						this.FS_aPowerupPieceSet[piece18] = 0;
					}
				}
				else
				{
					flag14 = true;
					if (flag15)
					{
						for (int num43 = 0; num43 < matchSet2.mPieces.Count; num43++)
						{
							Piece piece19 = matchSet2.mPieces[num43];
							piece19.mScale.SetConstant(1.0);
							piece19.mCanMatch = false;
						}
					}
					else if (piece13 != null)
					{
						for (int num44 = 0; num44 < matchSet2.mPieces.Count; num44++)
						{
							Piece piece20 = matchSet2.mPieces[num44];
							if (piece20 != piece13 && (piece20.mFlags == 0 || piece20.IsFlagSet(128U) || piece20.IsFlagSet(131072U)))
							{
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_E, piece20.mScale);
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_DEST_PCT_B, piece20.mDestPct);
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_ALPHA, piece20.mAlpha);
								piece20.mDestPct.mIncRate *= (double)this.GetMatchSpeed();
								piece20.mScale.mIncRate *= (double)this.GetMatchSpeed();
								piece20.mAlpha.mIncRate *= (double)this.GetMatchSpeed();
								piece20.mDestCol = piece13.mCol;
								piece20.mDestRow = piece13.mRow;
								int num45 = piece13.mCol - piece20.mCol;
								int num46 = piece13.mRow - piece20.mRow;
								if (piece13.IsFlagSet(1U))
								{
									PopAnimEffect popAnimEffect = PopAnimEffect.fromPopAnim(GlobalMembersResourcesWP.POPANIM_FLAMEGEMCREATION);
									popAnimEffect.mPieceRel = piece20;
									popAnimEffect.mX = piece20.CX();
									popAnimEffect.mY = piece20.CY();
									popAnimEffect.mOverlay = true;
									popAnimEffect.mDoubleSpeed = true;
									if (num45 != 0)
									{
										popAnimEffect.Play("smear horizontal");
										if (num45 < 0)
										{
											popAnimEffect.mAngle = 3.14159274f;
										}
									}
									else
									{
										popAnimEffect.Play("smear vertical");
										if (num46 < 0)
										{
											popAnimEffect.mAngle = 3.14159274f;
										}
									}
									this.mPostFXManager.AddEffect(popAnimEffect);
								}
							}
						}
					}
					int count = matchSet2.mPieces.Count;
					int num47 = num34 / count;
					int num48 = num35 / count;
					num3 += num47;
					num4 += num48;
					if (flag17)
					{
						this.mNOfIntentionalMatchesDuringCascade++;
					}
					this.MaxStat(39, this.mNOfIntentionalMatchesDuringCascade);
					this.AddToStat(26, 1, matchSet2.mMoveCreditId);
					int num49 = Math.Max(1, this.GetMoveStat(matchSet2.mMoveCreditId, 26));
					int num50 = 50 * num49 + (count - 3) * 50;
					if (count >= 5)
					{
						num50 += (count - 4) * 350;
					}
					this.AddPoints(num47 + 50, num48 + 50 - 8, num50, GlobalMembers.gPointColors[matchSet2.mPieces[0].mColor], (uint)matchSet2.mMatchId, true, true, matchSet2.mMoveCreditId, false, 0);
					this.MaxStat(24, count, matchSet2.mMoveCreditId);
					this.FS_aMoveCreditSet[matchSet2.mMoveCreditId] = 0;
				}
			}
			if (flag14)
			{
				int panPosition = this.GetPanPosition(num3 / num + 50);
				if (num > 1)
				{
					if (this.WantsCalmEffects())
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DOUBLESET, panPosition, GlobalMembers.M(0.6), GlobalMembers.M(0.0));
					}
					else
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DOUBLESET, panPosition);
					}
				}
				int num51 = this.GetMaxMovesStat(27) + 1;
				if (num2 == 0)
				{
					num51 = 1;
				}
				if (num51 > 6)
				{
					num51 = 6;
				}
				if (fromUpdateSwapping && this.mSpeedBonusCount > 0)
				{
					if (this.mSpeedBonusNum > 0.01)
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_FLAMESPEED1, panPosition, 1.0, this.mSpeedBonusNum * (double)GlobalMembers.M(1f));
					}
					else
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.GetSoundById(1721 + Math.Min(8, this.mSpeedBonusCount)), panPosition, 1.0, this.mSpeedBonusNum * (double)GlobalMembers.M(1f));
					}
				}
				else if (num51 == 1 && this.WantsCalmEffects())
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_ZEN_COMBO_2, panPosition);
				}
				else
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COMBO_1 + num51, panPosition);
				}
				foreach (int theMoveCreditId in this.FS_aMoveCreditSet.Keys)
				{
					this.AddToStat(27, 1, theMoveCreditId);
				}
			}
			foreach (Piece piece21 in this.FS_aTallyPieceSet.Keys)
			{
				if (!piece21.mIsBulging)
				{
					this.TallyPiece(piece21, !this.FS_aPowerupPieceSet.ContainsKey(piece21));
				}
			}
			if (flag && fromUpdateSwapping && (this.FS_aDelayingPieceSet.ContainsKey(thePiece1) || this.FS_aDelayingPieceSet.ContainsKey(thePiece2)))
			{
				return 2;
			}
			if (num <= 0)
			{
				return 0;
			}
			return 1;
		}

		public virtual void ShowHint(bool fromButton)
		{
			if (this.mInReplay)
			{
				return;
			}
			this.mHintCooldownTicks = GlobalMembers.M(300);
			this.mWantHintTicks = 0;
			if (!fromButton && !this.mShowAutohints)
			{
				return;
			}
			int[] array = new int[4];
			if (this.FindRandomMove(array))
			{
				Piece thePiece = this.mBoard[array[3], array[2]];
				Piece piece = this.mBoard[array[1], array[0]];
				if (piece != null && piece.IsFlagSet(2U))
				{
					thePiece = piece;
				}
				if (this.WriteUReplayCmd(16))
				{
					this.EncodePieceRef(thePiece);
				}
				this.ShowHint(thePiece, fromButton);
			}
		}

		public void ShowHint(Piece thePiece, bool theShowArrow)
		{
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_HINT_ALPHA, thePiece.mHintAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_HINT_ARROW_POS, thePiece.mHintArrowPos);
			if (this.WantsCalmEffects())
			{
				thePiece.mHintAlpha.mIncRate *= (double)GlobalMembers.M(0.5f);
				thePiece.mHintArrowPos.mIncRate *= (double)GlobalMembers.M(0.5f);
			}
			if (theShowArrow)
			{
				ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_HINTFLASH);
				particleEffect.mPieceRel = thePiece;
				particleEffect.mDoubleSpeed = true;
				this.mPostFXManager.AddEffect(particleEffect);
			}
		}

		public virtual void FillInBlanks(bool allowCascades)
		{
			this.FillInBlanks(allowCascades, false);
		}

		public virtual void FillInBlanks()
		{
			this.FillInBlanks(true, false);
		}

		public virtual void FillInBlanks(bool allowCascades, bool creatingNewBoard)
		{
			if (this.mGameOverPiece != null)
			{
				return;
			}
			for (int i = this.mBottomFillRow; i >= 0; i--)
			{
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					if (piece != null && piece.mExplodeDelay > 0)
					{
						return;
					}
				}
			}
			List<Piece> list = new List<Piece>();
			bool flag = false;
			bool flag2;
			do
			{
				flag2 = false;
				for (int k = 0; k < 8; k++)
				{
					bool flag3 = false;
					int num = -this.GetBoardY();
					int mMoveCreditId = this.mNextColumnCredit[k];
					double num2 = (double)this.mBumpVelocities[k];
					int num3 = 0;
					int num4 = 0;
					for (int l = this.mBottomFillRow; l >= 0; l--)
					{
						Piece piece2 = this.mBoard[l, k];
						if (piece2 != null)
						{
							piece2.mCanMatch = true;
							if (piece2.mY < (float)num)
							{
								num = (int)piece2.mY;
							}
							if (flag3 && (piece2.mDestRow == -1 || piece2.mDestPct.IsDoingCurve() || this.IsPieceSwapping(piece2) || this.IsPieceMatching(piece2)))
							{
								if (num4 > 0 && this.WriteUReplayCmd(5))
								{
									this.mUReplayBuffer.WriteByte((byte)k);
									this.mUReplayBuffer.WriteByte((byte)num3);
									this.mUReplayBuffer.WriteByte((byte)num4);
								}
								num4 = 0;
								this.mBoard[l + 1, k] = null;
								flag3 = false;
							}
							if (flag3)
							{
								flag2 = true;
								if ((double)piece2.mFallVelocity == 0.0)
								{
									piece2.mFallVelocity += this.mBumpVelocities[k] + 1f;
									piece2.mLastActiveTick = this.mUpdateCnt;
								}
								num4++;
								num2 = (double)piece2.mFallVelocity;
								piece2.mRow++;
								this.mBoard[l, k] = null;
								this.mBoard[l + 1, k] = piece2;
								mMoveCreditId = piece2.mMoveCreditId;
							}
						}
						else if (flag3)
						{
							this.mBoard[l + 1, k] = null;
							num4++;
						}
						else
						{
							num3 = l;
							num4 = 0;
							flag3 = true;
						}
					}
					if (num4 > 0 && this.WriteUReplayCmd(5))
					{
						this.mUReplayBuffer.WriteByte((byte)k);
						this.mUReplayBuffer.WriteByte((byte)num3);
						this.mUReplayBuffer.WriteByte((byte)num4);
					}
					if (flag3)
					{
						flag2 = true;
						Piece piece3 = this.CreateNewPiece(0, k);
						if (creatingNewBoard)
						{
							piece3.mY = (float)(num - 100);
						}
						else
						{
							piece3.mFallVelocity = (float)(num2 - 0.550000011920929);
							piece3.mMoveCreditId = mMoveCreditId;
							piece3.mY = (float)(num - 100) - (float)((ulong)this.mRand.Next() % (ulong)((long)GlobalMembers.M(15))) - GlobalMembers.M(10f);
							if (piece3.GetScreenY() > -100f)
							{
								piece3.mY = (float)(-100 - this.GetBoardY());
							}
						}
						list.Add(piece3);
						int num5 = 0;
						for (int m = this.mBottomFillRow; m >= 0; m--)
						{
							Piece piece4 = this.mBoard[m, k];
							if (piece4 != null)
							{
								float num6 = piece4.GetScreenY();
								if (piece4.GetScreenY() < (float)(-(float)this.GetBoardY()))
								{
									num5++;
									num6 = (float)(-(float)num5 * 100 * 2 - this.GetBoardY());
								}
								double num7 = (double)((float)this.GetRowY(piece4.mRow) - num6);
								double num8 = Math.Sqrt(2.0 * num7 / (GlobalMembers.M(0.265) * (double)this.GetGravityFactor()));
								int num9 = (int)num8;
								this.mSettlingDelay = Math.Max(this.mSettlingDelay, num9);
							}
						}
					}
					this.mNextColumnCredit[k] = -1;
				}
				flag = flag || flag2;
			}
			while (flag2);
			if (list.Count > 0)
			{
				int num10 = 0;
				bool flag4 = this.AllowNoMoreMoves();
				bool flag5 = this.WantSpecialPiece(list);
				bool flag6 = false;
				List<int> newGemColors = this.GetNewGemColors();
				bool specialDropped;
				for (;;)
				{
					specialDropped = false;
					for (int n = 0; n < list.Count; n++)
					{
						list[n].ClearFlags();
						list[n].mColor = -1;
						list[n].mCanDestroy = true;
						list[n].mCanMatch = true;
						list[n].mCounter = 0;
					}
					if (flag5)
					{
						specialDropped = this.DropSpecialPiece(list);
					}
					if (this.WantHyperMixers())
					{
						bool flag7 = true;
						Piece[,] array = this.mBoard;
						int upperBound = array.GetUpperBound(0);
						int upperBound2 = array.GetUpperBound(1);
						for (int num11 = array.GetLowerBound(0); num11 <= upperBound; num11++)
						{
							for (int num12 = array.GetLowerBound(1); num12 <= upperBound2; num12++)
							{
								Piece thePiece = array[num11, num12];
								if (this.IsHypermixerCancelledBy(thePiece))
								{
									flag7 = false;
									goto IL_4CF;
								}
							}
						}
						IL_4CF:
						if (flag7)
						{
							int[] array2 = new int[4];
							bool flag8 = false;
							if (this.WantHypermixerBottomCheck() && this.FindMove(array2, 0, true, true, true) && array2[1] < this.mHypermixerCheckRow && array2[3] < this.mHypermixerCheckRow)
							{
								flag8 = true;
							}
							if (!flag8 && this.WantHypermixerEdgeCheck())
							{
								int num13 = 0;
								bool flag9 = false;
								bool flag10 = false;
								while ((!flag9 || !flag10) && this.FindMove(array2, num13++, true, true, false))
								{
									if (array2[0] <= GlobalMembers.M(3) || array2[2] <= GlobalMembers.M(3))
									{
										flag9 = true;
									}
									if (array2[0] >= 8 - GlobalMembers.M(4) || array2[2] >= 8 - GlobalMembers.M(4))
									{
										flag10 = true;
									}
								}
								if (!flag9 || !flag10)
								{
									flag8 = true;
								}
							}
							if (flag8)
							{
								Piece piece5;
								do
								{
									piece5 = list[BejeweledLivePlus.Misc.Common.Rand(list.Count)];
								}
								while (piece5.mFlags != 0);
								piece5.mColor = -1;
								piece5.SetFlag(2U);
								flag6 = true;
								this.HypermixerDropped();
							}
						}
					}
					for (int num14 = 0; num14 < list.Count; num14++)
					{
						Piece piece6 = list[num14];
						if (piece6.mFlags == 0 || piece6.IsFlagSet(96U))
						{
							piece6.mColor = newGemColors[(int)this.mRand.Next((long)newGemColors.Count)];
						}
						int num15 = (this.mHasBoardSettled ? GlobalMembers.M(200) : GlobalMembers.M(200000));
						if (num10 >= num15 && num14 == 0)
						{
							allowCascades = true;
							flag4 = true;
						}
					}
					if (this.TryingDroppedPieces(list, num10))
					{
						flag4 |= this.FindMove(null, 0, true, true, false, null, num10 < this.GetPowerGemThreshold());
					}
					if (!allowCascades)
					{
						flag4 &= !this.HasSet();
					}
					flag4 &= !this.HasIllegalSet();
					if (flag4)
					{
						flag4 &= this.PiecesDropped(list);
					}
					if (flag4)
					{
						break;
					}
					num10++;
				}
				if (list.Count == 64 && this.mGameTicks > 0 && this.WantAnnihilatorReplacement())
				{
					for (int num16 = 0; num16 < 2; num16++)
					{
						for (int num17 = 0; num17 < 64; num17++)
						{
							Piece piece7 = list[(int)((ulong)this.mRand.Next() % (ulong)((long)list.Count))];
							if (piece7.mFlags == 0)
							{
								piece7.mColor = -1;
								piece7.SetFlag(2U);
								break;
							}
						}
					}
				}
				if (flag6)
				{
					this.NewHyperMixer();
				}
				this.BlanksFilled(specialDropped);
				for (int num18 = 0; num18 < list.Count; num18++)
				{
					Piece piece8 = list[num18];
					if (this.WriteUReplayCmd(0))
					{
						this.EncodePieceRef(piece8);
						this.mUReplayBuffer.WriteByte(this.EncodePieceFlags(piece8.mFlags));
						this.mUReplayBuffer.WriteByte((byte)piece8.mColor);
						this.mUReplayBuffer.WriteShort((short)((piece8.GetScreenY() - (float)this.GetRowY(0)) / 100f * 256f));
						if (piece8.mFallVelocity < 0f && this.WriteUReplayCmd(12))
						{
							this.EncodePieceRef(piece8);
							this.mUReplayBuffer.WriteShort((short)(piece8.mFallVelocity / 100f * 256f * 100f));
						}
					}
					this.StartPieceEffect(piece8);
				}
			}
		}

		public virtual void BlanksFilled(bool specialDropped)
		{
			if (specialDropped)
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_MULTIPLIER_APPEARS);
			}
		}

		public void MatchMade(SwapData theSwapData)
		{
			int num = this.mIdleTicks - this.mLastMatchTick;
			this.mMoveCounter++;
			if (theSwapData != null && !theSwapData.mForceSwap)
			{
				this.DecrementAllCounterGems(false);
			}
			this.mWantHintTicks = 0;
			if (!this.AllowSpeedBonus())
			{
				return;
			}
			this.mMatchTallyCount++;
			if (this.mSpeedBonusCount >= 9 && this.mSpeedBonusFlameModePct == 0f && (this.GetTimeLimit() == 0 || this.GetTicksLeft() >= 5))
			{
				float num2 = (float)num - GlobalMembers.M(100f);
				float num4;
				if (num2 >= 0f)
				{
					float num3 = GlobalMembers.M(100f) - GlobalMembers.M(180f);
					num4 = Math.Max(0f, Math.Min(1.5f, 1f - num2 / num3));
				}
				else
				{
					num4 = 1.5f;
				}
				float num5 = (float)((double)num4 - this.mSpeedBonusNum);
				if (num5 > 0f)
				{
					this.mSpeedBonusNum = Math.Min(1.0, this.mSpeedBonusNum + (double)Math.Min(0.1f, num5 * this.GetSpeedBonusRamp()));
					if (this.mSpeedBonusNum >= 1.0 && this.mSpeedBonusFlameModePct == 0f)
					{
						this.mSpeedBonusNum = 1.0;
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_DISP_ON, this.mSpeedBonusDisp);
						this.DoSpeedText(0);
					}
				}
			}
			if (this.mSpeedBonusCount > 0 || (this.mLastMatchTime >= 0 && num + this.mLastMatchTime <= 300))
			{
				if (this.mSpeedBonusCount == 0)
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_DISP_ON, this.mSpeedBonusDisp);
				}
				this.mSpeedBonusCount++;
				this.mSpeedBonusCountHighest = Math.Max(this.mSpeedBonusCountHighest, this.mSpeedBonusCount);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_POINTS_GLOW, this.mSpeedBonusPointsGlow);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_POINTS_SCALE_ON, this.mSpeedBonusPointsScale);
				foreach (Points points in this.mPointsManager.mPointsList)
				{
					if (points.mUpdateCnt == 0)
					{
						int num6 = Math.Min(200, (this.mSpeedBonusCount + 1) * 20);
						this.mSpeedBonusPoints += (int)((float)(num6 * this.mPointMultiplier) * this.GetModePointMultiplier());
						this.AddPoints((int)points.mX, (int)points.mY, num6, points.mColor, points.mId, false, true, points.mMoveCreditId, false, 2);
						points.mUpdateCnt++;
						break;
					}
				}
			}
			this.mLastMatchTime = num;
			this.mLastMatchTick = this.mIdleTicks;
			this.EncodeSpeedBonus();
		}

		public virtual bool CreateMatchPowerup(int theMatchCount, Piece thePiece, Dictionary<Piece, int> thePieceSet)
		{
			return false;
		}

		public void UpdateSpeedBonus()
		{
			if (GlobalMembers.gApp.GetDialog(18) != null || this.mHyperspace != null || this.mLightningStorms.Count != 0)
			{
				this.mLastMatchTick = this.mIdleTicks;
			}
			int num = this.mIdleTicks - this.mLastMatchTick;
			float num2 = 1f;
			if (this.mMoveCounter == 0)
			{
				num2 = 2f;
			}
			else if (this.mMoveCounter == 1)
			{
				num2 = 1.5f;
			}
			float num3 = (float)((double)GlobalMembers.M(180f) + this.mSpeedBonusNum * (double)(GlobalMembers.M(100f) - GlobalMembers.M(180f)));
			num3 *= num2;
			if ((float)num >= num3 && this.mSpeedBonusNum > 0.0 && this.mSpeedBonusFlameModePct == 0f)
			{
				this.mSpeedBonusNum *= (double)GlobalMembers.M(0.993f);
				if (this.mUpdateCnt % 10 == 0)
				{
					this.EncodeSpeedBonus();
				}
				if (this.mSpeedBonusNum <= 0.005)
				{
					this.mSpeedBonusNum = 0.0;
					this.EncodeSpeedBonus();
				}
			}
			this.mSpeedModeFactor.SetConstant((1.0 + this.mSpeedBonusNum * (double)GlobalMembers.M(0.65f)) * (double)this.GetSpeedModeFactorScale());
			this.mSpeedNeedle += (float)(((0.5 - this.mSpeedBonusNum) * GlobalMembers.MS(132.0) - (double)this.mSpeedNeedle) * (double)GlobalMembers.M(0.1f));
			float num4 = (GlobalMembers.M(100f) + (float)Math.Min(10, this.mSpeedBonusCount + 1) * GlobalMembers.M(13.75f)) * num2;
			if ((float)num >= num4 && this.mSpeedBonusCount > 0)
			{
				this.EndSpeedBonus();
			}
		}

		public virtual void EndSpeedBonus()
		{
			this.mLastMatchTick = -1000;
			this.mLastMatchTime = 1000;
			this.mSpeedBonusLastCount = this.mSpeedBonusCount;
			this.mSpeedBonusCount = 0;
			this.mSpeedBonusNum = 0.0;
			this.EncodeSpeedBonus();
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_DISP_OFF, this.mSpeedBonusDisp);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_POINTS_SCALE_OFF_NORMAL, this.mSpeedBonusPointsScale);
		}

		public virtual bool AllowUI()
		{
			return this.mLevelCompleteCount <= GlobalMembers.M(0) && this.mGameOverCount <= GlobalMembers.M(0);
		}

		public void DoGemCountPopup(int theCount)
		{
			this.mGemCountValueDisp = theCount;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GEM_COUNT_CURVE, this.mGemCountCurve);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GEM_COUNT_ALPHA, this.mGemCountAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GEM_SCALAR_ALPHA, this.mGemScalarAlpha);
		}

		public void DoCascadePopup(int theCount)
		{
			this.mCascadeCountValueDisp = theCount;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_CASCADE_COUNT_CURVE, this.mCascadeCountCurve);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_CASCADE_COUNT_ALPHA, this.mCascadeCountAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GEM_SCALAR_ALPHA, this.mGemScalarAlpha);
		}

		public virtual void UpdateCountPopups()
		{
			int totalMovesStat = this.GetTotalMovesStat(4);
			if (totalMovesStat >= this.GetGemCountPopupThreshold() && totalMovesStat > this.mGemCountValueCheck)
			{
				this.DoGemCountPopup(totalMovesStat);
				if (this.WriteUReplayCmd(17))
				{
					this.mUReplayBuffer.WriteByte((byte)Math.Min(255, totalMovesStat));
				}
			}
			if (totalMovesStat == 0 || totalMovesStat > this.mGemCountValueCheck)
			{
				this.mGemCountValueCheck = totalMovesStat;
			}
			else if (totalMovesStat < this.mGemCountValueCheck - 4)
			{
				this.mGemCountValueCheck = totalMovesStat + 4;
			}
			int maxMovesStat = this.GetMaxMovesStat(27);
			if (maxMovesStat >= GlobalMembers.M(3) && maxMovesStat > this.mCascadeCountValueCheck)
			{
				this.DoCascadePopup(maxMovesStat);
				if (this.WriteUReplayCmd(18))
				{
					this.mUReplayBuffer.WriteByte((byte)maxMovesStat);
				}
			}
			this.mCascadeCountValueCheck = maxMovesStat;
		}

		public virtual int CalcAwesomeness(int theMoveCreditId)
		{
			int num = (int)Math.Max(0.0, Math.Pow((double)Math.Max(0, this.GetMoveStat(theMoveCreditId, 27) - 1), (double)GlobalMembers.M(1.5f)));
			int moveStat = this.GetMoveStat(theMoveCreditId, 12);
			num += Math.Max(0, moveStat * 2 - 1);
			moveStat = this.GetMoveStat(theMoveCreditId, 13);
			num += Math.Max(0, (int)((double)moveStat * 2.5) - 1);
			moveStat = this.GetMoveStat(theMoveCreditId, 14);
			num += Math.Max(0, moveStat * 3 - 1);
			moveStat = this.GetMoveStat(theMoveCreditId, 17);
			num += moveStat;
			moveStat = this.GetMoveStat(theMoveCreditId, 18);
			num += moveStat;
			moveStat = this.GetMoveStat(theMoveCreditId, 19);
			num += moveStat * 2;
			moveStat = this.GetMoveStat(theMoveCreditId, 24);
			num += Math.Max(0, (moveStat - 5) * 8);
			return num + (int)Math.Pow((double)this.GetMoveStat(theMoveCreditId, 4) / GlobalMembers.M(15.0), GlobalMembers.M(1.5));
		}

		public void UpdateComplements()
		{
			if (!this.WantPointComplements())
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this.mMoveDataVector.Count; i++)
			{
				num += this.CalcAwesomeness(this.mMoveDataVector[i].mMoveCreditId);
			}
			int num2 = -1;
			int j = GlobalMembers.gComplementCount - 1;
			while (j >= 0)
			{
				if (num >= this.UpdateComplements_gComplementPoints[j])
				{
					num2 = j;
					if (j <= this.mLastComplement)
					{
						break;
					}
					if (this.SupportsReplays() && j >= 1 && !this.mIsOneMoveReplay && !this.mIsWholeGameReplay && !this.mWantReplaySave)
					{
						this.mWantReplaySave = true;
						if (this.mReplayStartMove != null)
						{
							this.mReplayStartMove.CopyFrom(this.mMoveDataVector[0]);
						}
						else
						{
							this.mReplayStartMove = new MoveData();
							this.mReplayStartMove.CopyFrom(this.mMoveDataVector[0]);
						}
					}
					if (j >= this.GetMinComplementLevel())
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
				return;
			}
			if (num2 < this.mLastComplement - 1)
			{
				this.mLastComplement = num2 + 1;
			}
		}

		public virtual void DoCombineAnim(Piece i_fromPiece, Piece i_tgtPiece)
		{
			if (i_fromPiece != i_tgtPiece && (i_fromPiece.mFlags == 0 || i_fromPiece.IsFlagSet(128U)))
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_E, i_fromPiece.mScale);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_DEST_PCT_B, i_fromPiece.mDestPct);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_ALPHA, i_fromPiece.mAlpha);
				i_fromPiece.mDestPct.mIncRate *= (double)this.GetMatchSpeed();
				i_fromPiece.mDestCol = i_tgtPiece.mCol;
				i_fromPiece.mDestRow = i_tgtPiece.mRow;
				int num = i_tgtPiece.mCol - i_fromPiece.mCol;
				int num2 = i_tgtPiece.mRow - i_fromPiece.mRow;
				if (i_tgtPiece.IsFlagSet(1U))
				{
					PopAnimEffect popAnimEffect = PopAnimEffect.fromPopAnim(GlobalMembersResourcesWP.POPANIM_FLAMEGEMCREATION);
					popAnimEffect.mPieceRel = i_fromPiece;
					popAnimEffect.mX = i_fromPiece.CX();
					popAnimEffect.mY = i_fromPiece.CY();
					popAnimEffect.mOverlay = true;
					if (num != 0)
					{
						popAnimEffect.Play("smear horizontal");
						if (num < 0)
						{
							popAnimEffect.mAngle = 3.14159274f;
						}
					}
					else
					{
						popAnimEffect.Play("smear vertical");
						if (num2 < 0)
						{
							popAnimEffect.mAngle = 3.14159274f;
						}
					}
					this.mPostFXManager.AddEffect(popAnimEffect);
				}
			}
		}

		public virtual void ProcessMatches(List<MatchSet> theMatches, Dictionary<Piece, int> theTallySet)
		{
			this.ProcessMatches(theMatches, theTallySet, false);
		}

		public virtual void ProcessMatches(List<MatchSet> theMatches, Dictionary<Piece, int> theTallySet, bool fromUpdateSwapping)
		{
		}

		public bool DecrementAllCounterGems(bool immediate)
		{
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && !piece.IsFlagSet(320U))
					{
						this.DecrementCounterGem(piece, immediate);
					}
				}
			}
			return false;
		}

		public void DecrementAllDoomGems(bool immediate)
		{
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && piece.IsFlagSet(256U))
					{
						this.DecrementCounterGem(piece, immediate);
					}
				}
			}
		}

		public virtual bool DecrementCounterGem(Piece thePiece, bool immediate)
		{
			if (thePiece.mCounter > 0)
			{
				if (immediate)
				{
					thePiece.mCounter--;
					if (thePiece.mCounter == 0 && thePiece.IsFlagSet(512U) && this.mGameOverCount == 0)
					{
						this.GameOver();
					}
				}
				else if (thePiece.IsFlagSet(512U))
				{
					thePiece.mSpinFrame = 1f;
					thePiece.mSpinSpeed = GlobalMembers.M(0.33f);
				}
				else if (thePiece.IsFlagSet(128U))
				{
					thePiece.mCounter--;
				}
			}
			return false;
		}

		public void SetMoveCredit(Piece thePiece, int theMoveCreditId)
		{
			thePiece.mMoveCreditId = Math.Max(thePiece.mMoveCreditId, theMoveCreditId);
		}

		public void SetTutorialCleared(int theTutorial)
		{
			this.SetTutorialCleared(theTutorial, true);
		}

		public void SetTutorialCleared(int theTutorial, bool isCleared)
		{
			if (isCleared)
			{
				this.mTutorialFlags |= 1 << theTutorial;
			}
			else
			{
				this.mTutorialFlags &= ~(1 << theTutorial);
			}
			if (!this.mIsWholeGameReplay)
			{
				GlobalMembers.gApp.mProfile.SetTutorialCleared(theTutorial, isCleared);
			}
		}

		public void DeferTutorialDialog(int theTutorialFlag, Piece thePiece)
		{
			if (this.WantsTutorialReplays())
			{
				this.mReplayStartMove.CopyFrom(this.mMoveDataVector[0]);
				this.mReplayStartMove.mPartOfReplay = true;
				GlobalMembers.gGR.ClearOperationsTo(this.mReplayStartMove.mUpdateCnt - 1);
				this.mReplayIgnoredMoves = 0;
				this.mReplayHadIgnoredMoves = false;
			}
			DeferredTutorial deferredTutorial = new DeferredTutorial();
			deferredTutorial.mTutorialFlag = theTutorialFlag;
			deferredTutorial.mPieceId = thePiece.mId;
			this.SaveReplay(deferredTutorial.mReplayData);
			this.mDeferredTutorialVector.Add(deferredTutorial);
			this.HideReplayWidget();
		}

		public void CheckForTutorialDialogs()
		{
			if (GlobalMembers.gApp.GetDialog(18) != null || GlobalMembers.gApp.GetDialog(19) != null)
			{
				return;
			}
			if (this.mLevelCompleteCount != 0 || this.mGameOverCount != 0 || GlobalMembers.gApp.HasClearedTutorial(19))
			{
				this.mDeferredTutorialVector.Clear();
				return;
			}
			while (this.mDeferredTutorialVector.Count > 0)
			{
				if (this.mTimeExpired)
				{
					this.mDeferredTutorialVector.Clear();
					return;
				}
				DeferredTutorial deferredTutorial = this.mDeferredTutorialVector[0];
				if (deferredTutorial.mPieceId == -2)
				{
					this.mDeferredTutorialVector.RemoveAt(0);
					return;
				}
				if (this.GetPieceById(deferredTutorial.mPieceId) == null)
				{
					this.SetTutorialCleared(deferredTutorial.mTutorialFlag, false);
					this.mDeferredTutorialVector.RemoveAt(0);
					if (((PauseMenu)GlobalMembers.gApp.mMenus[7]).mTopButton != null)
					{
						((PauseMenu)GlobalMembers.gApp.mMenus[7]).mTopButton.SetDisabled(false);
					}
				}
				else
				{
					if (this.mHasReplayData)
					{
						this.HideReplayWidget();
						this.mHasReplayData = false;
					}
					string theHeader = string.Empty;
					string theText = string.Empty;
					switch (deferredTutorial.mTutorialFlag)
					{
					case 1:
						theHeader = GlobalMembers._ID("FLAME GEM", 96);
						theText = GlobalMembers._ID("You made a FLAME GEM by matching 4 Gems in a row. Match it for an explosion!", 3221);
						break;
					case 2:
						theHeader = GlobalMembers._ID("STAR GEM", 98);
						theText = GlobalMembers._ID("You made a STAR GEM by creating two intersecting matches!", 3222);
						break;
					case 3:
						theHeader = GlobalMembers._ID("HYPERCUBE", 100);
						theText = GlobalMembers._ID("You made a HYPERCUBE by matching 5 Gems in a row. Swap it to trigger!", 3223);
						break;
					case 4:
						theHeader = GlobalMembers._ID("MULTIPLIER GEM", 104);
						theText = GlobalMembers._ID("You have received a MULTIPLIER GEM! Match it to multiply your score for the rest of the game.", 3225);
						break;
					case 6:
						theHeader = GlobalMembers._ID("SUPERNOVA GEM", 102);
						theText = GlobalMembers._ID("You made a SUPERNOVA GEM by matching 6+ Gems in a row. Match it to release the force of a million suns. ", 3224);
						break;
					case 8:
						theHeader = GlobalMembers._ID("COIN", 106);
						theText = GlobalMembers._ID("You have received a COIN! Collect them to buy Boosts to power you up!", 3226);
						break;
					case 9:
						theHeader = GlobalMembers._ID("TIME BONUS", 108);
						theText = GlobalMembers._ID("You have received a TIME GEM! Collect them to extend your game after the timer bar empties!", 3227);
						break;
					case 13:
						theHeader = GlobalMembers._ID("VERTICAL MATCH", 110);
						theText = GlobalMembers._ID("Making a VERTICAL MATCH will destroy an ice column. Destroy more ice columns to increase your multiplier bonus!", 3228);
						break;
					case 14:
						theHeader = GlobalMembers._ID("Poker skull", 3230);
						theText = GlobalMembers._ID("Destroy the skull by filling the skull bar!", 3231);
						break;
					case 16:
						theHeader = GlobalMembers._ID("DARK ROCK", 112);
						theText = GlobalMembers._ID("Dark rocks can't be destroyed by normal matching. Use special Gems to destroy them!", 3229);
						break;
					}
					bool allowReplay = deferredTutorial.mReplayData.mSaveBuffer.GetDataLen() > 0 && this.WantsTutorialReplays();
					if (!GlobalMembers.gApp.mProfile.HasClearedTutorial(deferredTutorial.mTutorialFlag))
					{
						Bej3Widget.mCurrentSlidingMenu = GlobalMembers.gApp.mMenus[7];
						Piece pieceById = this.GetPieceById(deferredTutorial.mPieceId);
						HintDialog hintDialog = new HintDialog(theHeader, theText, allowReplay, true, pieceById, this);
						GlobalMembers.gApp.AddDialog(18, hintDialog);
						hintDialog.mFlushPriority = 1;
						GlobalMembers.gApp.mMenus[7].Transition_SlideOut();
						this.SetTutorialCleared(deferredTutorial.mTutorialFlag);
						this.mTutorialPieceIrisPct.SetCurve("b;0,1,0.028571,1,####         ~~###");
						return;
					}
					this.mDeferredTutorialVector.RemoveAt(0);
					if (((PauseMenu)GlobalMembers.gApp.mMenus[7]).mTopButton != null)
					{
						((PauseMenu)GlobalMembers.gApp.mMenus[7]).mTopButton.SetDisabled(false);
					}
				}
			}
		}

		public bool UpdateBulging()
		{
			bool result = false;
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && piece.mIsBulging)
					{
						result = true;
						if (!piece.mScale.IncInVal())
						{
							piece.mScale.SetConstant(1.0);
							piece.mIsBulging = false;
						}
					}
				}
			}
			return result;
		}

		public void FlipHeldSwaps()
		{
			for (int i = 0; i < this.mSwapDataVector.Count; i++)
			{
				SwapData swapData = this.mSwapDataVector[i];
				if (swapData.mHoldingSwap > 0 && swapData.mPiece1 != null && swapData.mPiece2 != null)
				{
					int num = swapData.mPiece1.mCol;
					swapData.mPiece1.mCol = swapData.mPiece2.mCol;
					swapData.mPiece2.mCol = num;
					num = swapData.mPiece1.mRow;
					swapData.mPiece1.mRow = swapData.mPiece2.mRow;
					swapData.mPiece2.mRow = num;
					this.mBoard[swapData.mPiece1.mRow, swapData.mPiece1.mCol] = swapData.mPiece1;
					this.mBoard[swapData.mPiece2.mRow, swapData.mPiece2.mCol] = swapData.mPiece2;
					swapData.mSwapDir.mX = -swapData.mSwapDir.mX;
					swapData.mSwapDir.mY = -swapData.mSwapDir.mY;
				}
			}
		}

		public void UpdateSwapping()
		{
			for (int i = 0; i < this.mSwapDataVector.Count; i++)
			{
				SwapData swapData = this.mSwapDataVector[i];
				bool flag = false;
				swapData.mGemScale.IncInVal();
				if (!swapData.mSwapPct.IncInValScalar(1f))
				{
					flag = true;
				}
				int num = this.GetColX(swapData.mPiece1.mCol) + swapData.mSwapDir.mX * 100 / 2;
				int num2 = this.GetRowY(swapData.mPiece1.mRow) + swapData.mSwapDir.mY * 100 / 2;
				swapData.mPiece1.mX = (float)((double)num - swapData.mSwapPct * (double)swapData.mSwapDir.mX * 100.0 / 2.0);
				swapData.mPiece1.mY = (float)((double)num2 - swapData.mSwapPct * (double)swapData.mSwapDir.mY * 100.0 / 2.0);
				if (!swapData.mDestroyTarget && swapData.mPiece2 != null)
				{
					swapData.mPiece2.mX = (float)((double)num + swapData.mSwapPct * (double)swapData.mSwapDir.mX * 100.0 / 2.0);
					swapData.mPiece2.mY = (float)((double)num2 + swapData.mSwapPct * (double)swapData.mSwapDir.mY * 100.0 / 2.0);
				}
				if (flag)
				{
					if (this.WriteUReplayCmd(1))
					{
						if (swapData.mForwardSwap)
						{
							this.mUReplayBuffer.WriteByte(2);
						}
						else
						{
							this.mUReplayBuffer.WriteByte(3);
						}
						this.EncodePieceRef(swapData.mPiece1);
						this.EncodePieceRef(swapData.mPiece2);
					}
					if (swapData.mForwardSwap && !this.mInUReplay)
					{
						bool flag2 = swapData.mForceSwap || this.ForceSwaps();
						int num3 = swapData.mPiece1.mRow + swapData.mSwapDir.mY;
						int num4 = swapData.mPiece1.mCol + swapData.mSwapDir.mX;
						for (int j = 0; j < 2; j++)
						{
							if (swapData.mDestroyTarget)
							{
								this.PieceDestroyedInSwap(swapData.mPiece2);
								swapData.mPiece2.Dispose();
								swapData.mPiece2 = null;
							}
							if (swapData.mPiece2 != null)
							{
								int num5 = swapData.mPiece1.mCol;
								swapData.mPiece1.mCol = swapData.mPiece2.mCol;
								swapData.mPiece2.mCol = num5;
								num5 = swapData.mPiece1.mRow;
								swapData.mPiece1.mRow = swapData.mPiece2.mRow;
								swapData.mPiece2.mRow = num5;
								this.mBoard[swapData.mPiece1.mRow, swapData.mPiece1.mCol] = swapData.mPiece1;
								this.mBoard[swapData.mPiece2.mRow, swapData.mPiece2.mCol] = swapData.mPiece2;
							}
							else
							{
								int num6 = swapData.mPiece1.mCol;
								swapData.mPiece1.mCol = num4;
								num4 = num6;
								num6 = swapData.mPiece1.mRow;
								swapData.mPiece1.mRow = num3;
								num3 = num6;
								this.mBoard[swapData.mPiece1.mRow, swapData.mPiece1.mCol] = swapData.mPiece1;
								this.mBoard[num3, num4] = swapData.mPiece2;
							}
							swapData.mIgnore = j == 0;
							swapData.mPiece1.mSwapTick = this.mUpdateCnt;
							if (swapData.mPiece2 != null)
							{
								swapData.mPiece2.mSwapTick = this.mUpdateCnt;
							}
							this.mLastComboCount = this.mComboCount;
							int num7 = 0;
							if (j == 1 || (num7 = this.FindSets(true, swapData.mPiece1, swapData.mPiece2)) != 0 || flag2)
							{
								if ((num7 == 2 && !flag2) || (swapData.mPiece1 != null && swapData.mPiece1.mIsBulging) || (swapData.mPiece2 != null && swapData.mPiece2.mIsBulging))
								{
									swapData.mHoldingSwap++;
									if (swapData.mHoldingSwap <= GlobalMembers.M(400))
									{
										flag = false;
										goto IL_6D7;
									}
									num7 = 0;
									swapData.mHoldingSwap = 0;
								}
								if (num7 != 0)
								{
									this.MatchMade(swapData);
								}
								else if (flag2)
								{
									this.DecrementAllDoomGems(false);
								}
								if (j == 0 && this.mLastComboCount == this.mComboCount && swapData.mPiece1.mColor == this.mLastPlayerSwapColor)
								{
									this.ComboFailed();
								}
								if (j == 0)
								{
									this.AddToStat(15);
									this.AddToStat(20);
									this.SwapSucceeded(swapData);
									break;
								}
								break;
							}
							else
							{
								if (this.WriteUReplayCmd(1))
								{
									this.mUReplayBuffer.WriteByte(1);
									this.EncodePieceRef(swapData.mPiece1);
									this.EncodePieceRef(swapData.mPiece2);
								}
								this.AddToStat(15);
								this.SwapFailed(swapData);
								if (GlobalMembers.gApp.mProfile.mStats[20] < 3 && !this.mInReplay && !GlobalMembers.gApp.mProfile.HasClearedTutorial(19))
								{
									GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
									this.mIllegalMoveTutorial = true;
									Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.DoDialog(18, true, GlobalMembers._ID("Illegal Move", 114), GlobalMembers._ID("Each swap must create a row of three or more identical gems.", 115), GlobalMembers._ID("OK", 116), 3);
									GlobalMembers.gApp.mMenus[7].Transition_SlideOut();
									bej3Dialog.mFlushPriority = 1;
									if (swapData.mPiece1 != null)
									{
										GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SELECTOR_ALPHA, swapData.mPiece1.mSelectorAlpha);
										if (swapData.mPiece2 != null)
										{
											GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SELECTOR_ALPHA, swapData.mPiece2.mSelectorAlpha);
										}
										if (swapData.mPiece1.mRow >= 4)
										{
											bej3Dialog.Move(GlobalMembers.S(this.GetBoardCenterX()) - bej3Dialog.mWidth / 2, (int)(GlobalMembers.S(swapData.mPiece1.CY()) - (float)bej3Dialog.mHeight + (float)GlobalMembers.MS(-220)));
										}
										else
										{
											bej3Dialog.Move(GlobalMembers.S(this.GetBoardCenterX()) - bej3Dialog.mWidth / 2, (int)(GlobalMembers.S(swapData.mPiece1.CY()) + (float)GlobalMembers.MS(220)));
										}
									}
								}
								GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_START_ROTATE);
								swapData.mHoldingSwap = 0;
								swapData.mForwardSwap = false;
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SWAP_PCT_2, swapData.mSwapPct);
								if (GlobalMembers.gIs3D)
								{
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GEM_SCALE_2, swapData.mGemScale);
								}
								swapData.mIgnore = false;
								flag = false;
							}
							IL_6D7:;
						}
					}
					if (flag && !this.mInUReplay)
					{
						for (i = 0; i < this.mSwapDataVector.Count; i++)
						{
							SwapData swapData2 = this.mSwapDataVector[i];
							if (swapData2 == swapData)
							{
								this.mSwapDataVector.RemoveAt(i);
								i--;
								break;
							}
						}
					}
				}
			}
		}

		public virtual void UpdateFalling()
		{
			if (!this.CanPiecesFall())
			{
				if (this.mGemFallDelay > 0)
				{
					this.mGemFallDelay--;
				}
				Piece[,] array = this.mBoard;
				int upperBound = array.GetUpperBound(0);
				int upperBound2 = array.GetUpperBound(1);
				for (int i = array.GetLowerBound(0); i <= upperBound; i++)
				{
					for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
					{
						Piece piece = array[i, j];
						if (piece != null && piece.mFallVelocity != 0f)
						{
							piece.mFallVelocity = 0.01f;
						}
					}
				}
				for (int k = 0; k < 8; k++)
				{
					this.mBumpVelocities[k] = 0f;
				}
				return;
			}
			int num = 0;
			int num2 = 0;
			for (int l = 0; l < 8; l++)
			{
				float num3 = 1200f;
				float mFallVelocity = 0f;
				for (int m = 7; m >= 0; m--)
				{
					Piece piece2 = this.mBoard[m, l];
					if (piece2 != null && (piece2.mFallVelocity < 0f || (!this.IsPieceMatching(piece2) && !this.IsPieceSwapping(piece2, false, false))))
					{
						piece2.mY += piece2.mFallVelocity;
						bool flag = piece2.mY >= (float)this.GetRowY(m);
						if (flag)
						{
							piece2.mY = (float)this.GetRowY(m);
							if (piece2.mFallVelocity >= GlobalMembers.M(2f))
							{
								num++;
								num2 += (int)piece2.GetScreenX() + 50;
							}
							if (piece2.mFallVelocity > 0f)
							{
								piece2.mFallVelocity = 0f;
							}
						}
						else if (piece2.mY >= num3 - 100f)
						{
							piece2.mY = num3 - 100f;
							piece2.mFallVelocity = mFallVelocity;
						}
						else
						{
							piece2.mFallVelocity += this.GetGravityFactor() * GlobalMembers.M(0.21995f);
						}
						if (piece2.mFallVelocity != 0f)
						{
							piece2.mLastActiveTick = this.mUpdateCnt;
						}
						num3 = piece2.mY;
						mFallVelocity = piece2.mFallVelocity;
					}
				}
			}
			if (num > 0 && Math.Abs(this.mLastHitSoundTick - this.mUpdateCnt) >= GlobalMembers.M(8))
			{
				this.mLastHitSoundTick = this.mUpdateCnt;
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_GEM_HIT, this.GetPanPosition(num2 / num));
			}
		}

		public void UpdateHint()
		{
			if (GlobalMembers.gApp.mProfile.mAutoHint && this.mBoardHidePct == 0f && this.IsBoardStill() && this.CanPlay())
			{
				this.mWantHintTicks++;
				if (this.mWantHintTicks == this.GetHintTime() * 100)
				{
					this.ShowHint(false);
				}
			}
		}

		public void UpdateLevelBar()
		{
			if (this.mLevelBarPIEffect != null)
			{
				float levelPct = this.GetLevelPct();
				if (this.mLevelBarPct < levelPct)
				{
					if (this.mTimerAlpha == 0.0)
					{
						this.mLevelBarPct = Math.Min(levelPct, this.mLevelBarPct + (levelPct - this.mLevelBarPct) * GlobalMembers.M(0.0255f) + GlobalMembers.M(0.0012f));
					}
					else
					{
						this.mLevelBarPct = Math.Min(levelPct, this.mLevelBarPct + (levelPct - this.mLevelBarPct) * GlobalMembers.M(0.025f) + GlobalMembers.M(0.0005f));
					}
				}
				else
				{
					this.mLevelBarPct = Math.Max(levelPct, this.mLevelBarPct + (levelPct - this.mLevelBarPct) * GlobalMembers.M(0.05f) - GlobalMembers.M(0.0001f));
				}
				this.UpdateLevelBarEffect();
				this.CheckWin();
			}
		}

		public virtual void UpdateLevelBarEffect()
		{
			Rect levelBarRect = this.GetLevelBarRect();
			PILayer layer = this.mLevelBarPIEffect.GetLayer(0);
			PIEmitterInstance emitter = layer.GetEmitter(0);
			emitter.mEmitterInstanceDef.mPoints[0].mValuePoint2DVector[0].mValue = new FPoint(0f, (float)(levelBarRect.mHeight / 2)).ToXnaVector2();
			emitter.mEmitterInstanceDef.mPoints[1].mValuePoint2DVector[0].mValue = new FPoint(this.mLevelBarPct * (float)levelBarRect.mWidth + (float)this.mLevelBarSizeBias, (float)(levelBarRect.mHeight / 2)).ToXnaVector2();
			layer = this.mLevelBarPIEffect.GetLayer(1);
			emitter = layer.GetEmitter(0);
			emitter.mEmitterInstanceDef.mPoints[0].mValuePoint2DVector[0].mValue = new FPoint(0f, (float)(levelBarRect.mHeight / 2)).ToXnaVector2();
			emitter.mEmitterInstanceDef.mPoints[1].mValuePoint2DVector[0].mValue = new FPoint(this.mLevelBarPct * (float)levelBarRect.mWidth + (float)this.mLevelBarSizeBias, (float)(levelBarRect.mHeight / 2)).ToXnaVector2();
		}

		public void UpdateCountdownBar()
		{
			if (this.mCountdownBarPIEffect != null)
			{
				float countdownPct = this.GetCountdownPct();
				if (this.mCountdownBarPct < countdownPct)
				{
					if (this.mTimerAlpha == 0.0)
					{
						this.mCountdownBarPct = Math.Min(countdownPct, this.mCountdownBarPct + (countdownPct - this.mCountdownBarPct) * GlobalMembers.M(0.0275f) + GlobalMembers.M(0.00125f));
					}
					else
					{
						this.mCountdownBarPct = Math.Min(countdownPct, this.mCountdownBarPct + (countdownPct - this.mCountdownBarPct) * GlobalMembers.M(0.025f) + GlobalMembers.M(0.0005f));
					}
				}
				else
				{
					this.mCountdownBarPct = Math.Max(countdownPct, this.mCountdownBarPct + (countdownPct - this.mCountdownBarPct) * GlobalMembers.M(0.05f) - GlobalMembers.M(0.0001f));
				}
				Rect countdownBarRect = this.GetCountdownBarRect();
				PILayer layer = this.mCountdownBarPIEffect.GetLayer(0);
				PIEmitterInstance emitter = layer.GetEmitter(0);
				emitter.mEmitterInstanceDef.mPoints[0].mValuePoint2DVector[0].mValue = new FPoint(0f, (float)(countdownBarRect.mHeight / 2)).ToXnaVector2();
				emitter.mEmitterInstanceDef.mPoints[1].mValuePoint2DVector[0].mValue = new FPoint(this.mCountdownBarPct * (float)countdownBarRect.mWidth, (float)(countdownBarRect.mHeight / 2)).ToXnaVector2();
				layer = this.mCountdownBarPIEffect.GetLayer(1);
				emitter = layer.GetEmitter(0);
				emitter.mEmitterInstanceDef.mPoints[0].mValuePoint2DVector[0].mValue = new FPoint(0f, (float)(countdownBarRect.mHeight / 2)).ToXnaVector2();
				emitter.mEmitterInstanceDef.mPoints[1].mValuePoint2DVector[0].mValue = new FPoint(this.mCountdownBarPct * (float)countdownBarRect.mWidth, (float)(countdownBarRect.mHeight / 2)).ToXnaVector2();
				this.CheckCountdownBar();
			}
		}

		public virtual void CheckCountdownBar()
		{
			float num = Math.Max(0f, (float)this.GetTicksLeft() / ((float)this.GetTimeLimit() * 100f));
			if (this.GetTimeLimit() > 0 && num <= 0f && this.CanTimeUp() && this.mDeferredTutorialVector.Count == 0 && this.mGameOverCount == 0)
			{
				this.GameOver();
				if (this.mGameFinished)
				{
					this.mTimeExpired = true;
				}
			}
		}

		public virtual bool CheckWin()
		{
			int levelPoints = this.GetLevelPoints();
			if (this.GetTimeLimit() == 0)
			{
				int levelPointsTotal = this.GetLevelPointsTotal();
				if (this.mLevelBarPct >= 1f && levelPointsTotal >= levelPoints && this.mSpeedBonusFlameModePct == 0f)
				{
					this.LevelUp();
					return true;
				}
			}
			else if (levelPoints > 0 && this.GetLevelPointsTotal() >= levelPoints)
			{
				this.LevelUp();
				return true;
			}
			return false;
		}

		public virtual bool WantWarningGlow()
		{
			return this.WantWarningGlow(false);
		}

		public virtual bool WantWarningGlow(bool forSound)
		{
			int timeLimit = this.GetTimeLimit();
			int ticksLeft = this.GetTicksLeft();
			int num = ((timeLimit > 60) ? 1500 : 1000);
			return timeLimit > 0 && ticksLeft < num;
		}

		public virtual float GetSpeedBonusRamp()
		{
			return GlobalMembers.M(0.075f);
		}

		public virtual Color GetWarningGlowColor()
		{
			int ticksLeft = this.GetTicksLeft();
			if (ticksLeft == 0)
			{
				return new Color(255, 0, 0, 127);
			}
			int num = ((this.GetTimeLimit() > 60) ? 1500 : 1000);
			float num2 = (float)(num - ticksLeft) / (float)num;
			int theAlpha = (int)(((float)Math.Sin((double)((float)this.mUpdateCnt * GlobalMembers.M(0.15f))) * 127f + 127f) * num2 * this.GetPieceAlpha());
			return new Color(255, 0, 0, theAlpha);
		}

		public virtual bool WantTopLevelBar()
		{
			return this.GetTimeLimit() == 0;
		}

		public virtual bool WantTopFrame()
		{
			return true;
		}

		public virtual bool WantBottomFrame()
		{
			return true;
		}

		public virtual bool WantDrawButtons()
		{
			return true;
		}

		public virtual bool WantDrawScore()
		{
			return true;
		}

		public virtual bool WantDrawBackground()
		{
			return true;
		}

		public virtual bool WantCountdownBar()
		{
			return this.GetTimeLimit() > 0;
		}

		public void UpdateMoveData()
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
					goto IL_181;
				}
				bool flag = false;
				Piece[,] array = this.mBoard;
				int upperBound = array.GetUpperBound(0);
				int upperBound2 = array.GetUpperBound(1);
				for (int j = array.GetLowerBound(0); j <= upperBound; j++)
				{
					for (int k = array.GetLowerBound(1); k <= upperBound2; k++)
					{
						Piece piece = array[j, k];
						if (piece != null && piece.mMoveCreditId == moveData.mMoveCreditId)
						{
							if (this.IsPieceStill(piece))
							{
								piece.mLastMoveCreditId = piece.mMoveCreditId;
								piece.mMoveCreditId = -1;
								piece.mLastActiveTick = 0;
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
					goto IL_181;
				}
				Piece[,] array2 = this.mBoard;
				int upperBound3 = array2.GetUpperBound(0);
				int upperBound4 = array2.GetUpperBound(1);
				for (int m = array2.GetLowerBound(0); m <= upperBound3; m++)
				{
					for (int n = array2.GetLowerBound(1); n <= upperBound4; n++)
					{
						Piece piece2 = array2[m, n];
						if (piece2 != null && piece2.mLastMoveCreditId == moveData.mMoveCreditId)
						{
							piece2.mLastMoveCreditId = -1;
						}
					}
				}
				this.mMoveDataVector.RemoveAt(i);
				i--;
				IL_18D:
				i++;
				continue;
				IL_181:
				if (moveData.mPartOfReplay)
				{
					num++;
					goto IL_18D;
				}
				goto IL_18D;
			}
			if (num2 == 1 && num == 0 && this.mHasReplayData)
			{
				if (!this.mWantLevelup && this.mHyperspace == null && this.mDeferredTutorialVector.Count == 0)
				{
					this.ShowReplayWidget();
				}
				if (!this.mInReplay)
				{
					this.mReplayHadIgnoredMoves = this.mReplayIgnoredMoves > 0;
					this.mReplayIgnoredMoves = 0;
				}
			}
		}

		public virtual uint GetRandSeedOverride()
		{
			return 0U;
		}

		public void UpdateReplayPopup()
		{
			if (!this.SupportsReplays())
			{
				return;
			}
			if (this.mHasReplayData && this.mReplayIgnoredMoves >= 3)
			{
				this.mHasReplayData = false;
				this.HideReplayWidget();
			}
			if (GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON == null)
			{
				return;
			}
			int num = GlobalMembers.S(GlobalMembers.gApp.mWidth / 2);
			int num2 = (int)(-(int)(GlobalMembers.IMG_SYOFS(1094) + (float)GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON.GetCelHeight()));
			this.mReplayButton.Resize(GlobalMembers.IMGRECT_NS(GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON, (float)num, (float)((double)ConstantsWP.REPLAY_OFFSET_Y + (double)num2 * (1.0 - this.mReplayWidgetShowPct))));
		}

		public void UpdateReplay()
		{
			GlobalMembers.gGR.mIgnoreDraws = false;
			GlobalMembers.gGR.mRecordDraws = false;
			if (!this.mRewinding)
			{
				if (this.mInReplay)
				{
					this.mRewindRand.SRand((uint)(GlobalMembers.gApp.mUpdateCount / GlobalMembers.M(7)));
				}
				int num = this.mUpdateCnt - GlobalMembers.gGR.GetFirstOperationTimestamp();
				if (GlobalMembers.gGR.GetFirstOperationTimestamp() == -1)
				{
					num = 0;
				}
				bool flag = this.mAlpha == 1.0 && !this.IsBoardStill() && (this.mReplayIgnoredMoves == 0 || !this.mHasReplayData) && (!this.mHasReplayData || num < 500);
				if (flag)
				{
					int mUpdateCnt = this.mUpdateCnt;
					GlobalMembers.gGR.GetLastOperationTimestamp();
					GlobalMembers.gGR.mRecordDraws = true;
					GlobalMembers.gGR.SetTimestamp(this.mUpdateCnt);
					GlobalMembers.gGR.ClearOperationsTo(this.mUpdateCnt - ConstantsWP.BOARD_MAX_REWIND_TIME);
				}
				if (this.mReplayStartMove.mPartOfReplay && !this.mHasReplayData)
				{
					GlobalMembers.gGR.ClearOperationsTo(this.mReplayStartMove.mUpdateCnt - 1);
				}
				GlobalMembers.gGR.mIgnoreDraws = false;
				if (this.mInReplay && this.mQueuedMoveVector.Count == 0 && this.IsBoardStill())
				{
					if (this.mWantLevelup && !this.mReplayWasTutorial)
					{
						this.mWasLevelUpReplay = true;
					}
					if (((this.mReplayIgnoredMoves > 0 || this.mReplayHadIgnoredMoves) && !this.mIsOneMoveReplay && !this.mIsWholeGameReplay) || this.mWasLevelUpReplay)
					{
						if (this.mReplayFadeout == 1.0)
						{
							this.LoadGame(this.mPreReplaySave, false);
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_REPLAY_FADEOUT_TO_CLEAR, this.mReplayFadeout);
							this.mInReplay = false;
							if (this.mCurrentHint != null)
							{
								this.OnAchievementHintFinished(this.mCurrentHint);
								this.mCurrentHint = null;
							}
							((PauseMenu)GlobalMembers.gApp.mMenus[7]).SetTopButtonType(this.mReplayWasTutorial ? Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS : Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
							this.DisableUI(false);
							GlobalMembers.gApp.DisableOptionsButtons(false);
							this.ToggleReplayPulse(false);
							if (this.mWasLevelUpReplay)
							{
								this.mWasLevelUpReplay = false;
								if (this.mHasReplayData && GlobalMembers.gApp.mCurrentGameMode != GameMode.MODE_ZEN)
								{
									this.ShowReplayWidget();
									return;
								}
							}
						}
						else if (this.mReplayFadeout == 0.0 && (this.mReplayFadeout.GetInVal() == 0.0 || this.mReplayFadeout.GetInVal() == 1.0))
						{
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_REPLAY_FADEOUT_TO_WHITE, this.mReplayFadeout);
							this.ToggleReplayPulse(false);
							return;
						}
					}
					else if (!this.mIsWholeGameReplay || this.mGameOverCount >= 350)
					{
						this.mInReplay = false;
						if (this.mCurrentHint != null)
						{
							this.OnAchievementHintFinished(this.mCurrentHint);
							this.mCurrentHint = null;
						}
						this.DisableUI(false);
						if (!this.mReplayWasTutorial)
						{
							((PauseMenu)GlobalMembers.gApp.mMenus[7]).SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
						}
						GlobalMembers.gApp.DisableOptionsButtons(false);
						this.ToggleReplayPulse(false);
					}
				}
				return;
			}
			this.mRewindRand.SRand((uint)(GlobalMembers.gApp.mUpdateCount / GlobalMembers.M(3)));
			int num2 = this.mPlaybackTimestamp - GlobalMembers.gGR.GetFirstOperationTimestamp();
			int num3 = Math.Min(GlobalMembers.M(2) + num2 / GlobalMembers.M(100), num2 / GlobalMembers.M(20));
			this.mPlaybackTimestamp -= num3;
			if (num3 <= 0)
			{
				this.mPlaybackTimestamp--;
			}
			if (GlobalMembers.gGR.GetLastOperationTimestamp() < 0 || num2 <= 0)
			{
				if (this.mRewindSound != null)
				{
					this.mRewindSound.Release();
					this.mRewindSound = null;
				}
				this.mRewinding = false;
				this.mPlaybackTimestamp = this.mUpdateCnt;
				GlobalMembers.gGR.ClearOperationsFrom(0);
				this.ToggleReplayPulse(true);
				return;
			}
			GlobalMembers.gGR.mIgnoreDraws = true;
		}

		public void BackToGame()
		{
			int num = this.mLevel;
			this.LoadGame(this.mPreReplaySave, false);
			int num2 = this.mLevel;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_REPLAY_FADEOUT_TO_CLEAR, this.mReplayFadeout);
			this.mReplayFadeout;
			this.mInReplay = false;
			if (this.mCurrentHint != null)
			{
				this.OnAchievementHintFinished(this.mCurrentHint);
				this.mCurrentHint = null;
			}
			((PauseMenu)GlobalMembers.gApp.mMenus[7]).SetTopButtonType(this.mReplayWasTutorial ? Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS : Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
			this.DisableUI(false);
			GlobalMembers.gApp.DisableOptionsButtons(false);
			this.ToggleReplayPulse(false);
			this.mWasLevelUpReplay = num != num2;
			this.mRewinding = false;
			this.mHasReplayData = true;
			if (this.mRewindSound != null)
			{
				this.mRewindSound.Stop();
				this.mRewindSound = null;
			}
			if (!this.mReplayWasTutorial)
			{
				this.ShowReplayWidget();
				return;
			}
			this.HideReplayWidget();
		}

		public virtual void UpdateTooltip()
		{
			if (!this.AllowTooltips())
			{
				return;
			}
			string empty = string.Empty;
			string empty2 = string.Empty;
			GlobalMembers.S(GlobalMembers.M(500));
			Point point = default(Point);
			Point point2 = new Point((int)((float)this.mWidgetManager.mLastMouseX / GlobalMembers.S(1f)), (int)((float)this.mWidgetManager.mLastMouseY / GlobalMembers.S(1f)));
			Rect rect = new Rect(0, 0, 0, 0);
			if (!rect.Contains(point2.mX, point2.mY))
			{
				Piece pieceAtScreenXY = this.GetPieceAtScreenXY(point2.mX, point2.mY);
				if (pieceAtScreenXY != null)
				{
					int num = (int)((float)point2.mX - (pieceAtScreenXY.GetScreenX() + 50f));
					int num2 = (int)((float)point2.mY - (pieceAtScreenXY.GetScreenY() + 50f));
					float num3 = (float)Math.Sqrt((double)((float)num * (float)num + (float)(num2 * num2)));
					if ((double)num3 <= 100.0 * GlobalMembers.M(0.35) && this.GetTooltipText(pieceAtScreenXY, ref empty2, ref empty))
					{
						point = new Point((int)GlobalMembers.S(pieceAtScreenXY.GetScreenX() + 50f), (int)GlobalMembers.S(pieceAtScreenXY.GetScreenY() + 50f));
						if ((float)point.mX > (float)this.mWidth * GlobalMembers.M(0.7f))
						{
							point.mX -= GlobalMembers.MS(48);
							return;
						}
						point.mX += GlobalMembers.MS(48);
					}
				}
			}
		}

		public virtual bool GetTooltipText(Piece thePiece, ref string theHeader, ref string theBody)
		{
			bool result = false;
			if (thePiece.IsFlagSet(1U))
			{
				if (thePiece.IsFlagSet(4U))
				{
					theHeader = GlobalMembers._ID("SUPERNOVA GEM", 117);
					theBody = GlobalMembers._ID("Created by matching 6+ Gems in a row, this powerful Gem explodes with the force of a million suns when matched.", 118);
				}
				else
				{
					theHeader = GlobalMembers._ID("FLAME GEM", 119);
					theBody = GlobalMembers._ID("Created by forming 4 Gems of the same color in a line. Explodes when matched!", 120);
				}
				result = true;
			}
			else if (thePiece.IsFlagSet(4U))
			{
				theHeader = GlobalMembers._ID("STAR GEM", 121);
				theBody = GlobalMembers._ID("Created by making two intersecting matches. Match it to fire lightning 4 ways!", 122);
				result = true;
			}
			else if (thePiece.IsFlagSet(2U))
			{
				theHeader = GlobalMembers._ID("HYPERCUBE", 123);
				theBody = GlobalMembers._ID("Created by matching 5 Gems in a line. Swap it with a Gem to zap all Gems of the same color onscreen.", 124);
				result = true;
			}
			else if (thePiece.IsFlagSet(16U))
			{
				theHeader = GlobalMembers._ID("MULTIPLIER GEM", 125);
				theBody = GlobalMembers._ID("Randomly drops onto your board. Match it to increase your score multiplier by 1!", 126);
				result = true;
			}
			else if (thePiece.IsFlagSet(2048U))
			{
				theHeader = GlobalMembers._ID("DETONATOR", 127);
				theBody = GlobalMembers._ID("Click to detonate all Special Gems on the board.", 128);
				result = true;
			}
			else if (thePiece.IsFlagSet(4096U))
			{
				theHeader = GlobalMembers._ID("SCRAMBLER", 129);
				theBody = GlobalMembers._ID("Click to scramble all Gems on the board.", 130);
				result = true;
			}
			else if (thePiece.IsFlagSet(1024U))
			{
				theHeader = GlobalMembers._ID("COIN", 131);
				theBody = GlobalMembers._ID("Clear this Gem to collect the coin inside! Save money to buy Boosts!", 132);
				result = true;
			}
			else if (thePiece.IsFlagSet(128U))
			{
				theHeader = GlobalMembers._ID("BUTTERFLIES", 133);
				theBody = GlobalMembers._ID("Match butterflies with like-colored Gems to free them.", 134);
				result = true;
			}
			else if (thePiece.IsFlagSet(131072U))
			{
				theHeader = GlobalMembers._ID("TIME GEM", 135);
				theBody = string.Format(GlobalMembers._ID("Match this Gem to add {0} seconds to the clock!", 136), SexyFramework.Common.CommaSeperate(thePiece.mCounter));
				result = true;
			}
			else if (thePiece.IsFlagSet(96U))
			{
				theHeader = GlobalMembers._ID("TIME BOMB", 137);
				theBody = GlobalMembers._ID("Match this Gem before the counter reaches zero!", 138);
				result = true;
			}
			return result;
		}

		public virtual void UpdatePoints()
		{
			if (this.mUpdateCnt % GlobalMembers.M(4) == 0)
			{
				if (this.mDispPoints < this.mPoints)
				{
					this.mDispPoints += (int)((float)(this.mPoints - this.mDispPoints) * GlobalMembers.M(0.2f)) + 1;
				}
				else if (this.mDispPoints > this.mPoints)
				{
					this.mDispPoints += (int)((float)(this.mPoints - this.mDispPoints) * GlobalMembers.M(0.2f)) - 1;
				}
				if (this.mMoneyDisp < this.mMoneyDispGoal)
				{
					this.mMoneyDisp += (int)((float)(this.mMoneyDispGoal - this.mMoneyDisp) * GlobalMembers.M(0.5f) + 1f);
					if (this.mMoneyDisp == this.mMoneyDispGoal)
					{
						this.mCoinCatcherAppearing = false;
					}
				}
			}
		}

		public virtual void UpdateGame()
		{
			if (this.mAnnouncements.Count > 0)
			{
				this.mAnnouncements[0].Update();
			}
			this.mSunPosition.IncInVal();
			this.mAlpha.IncInVal();
			this.mSideAlpha.IncInVal();
			this.mSideXOff.IncInVal();
			this.mScale.IncInVal();
			this.mPrevPointMultAlpha.IncInVal();
			this.mPointMultPosPct.IncInVal();
			this.mPointMultTextMorph.IncInVal();
			this.mSpeedBonusDisp.IncInVal();
			this.mSpeedBonusPointsGlow.IncInVal();
			this.mSpeedBonusPointsScale.IncInVal();
			this.mTutorialPieceIrisPct.IncInVal();
			this.mGemCountCurve.IncInVal();
			this.mGemCountAlpha.IncInVal();
			this.mGemScalarAlpha.IncInVal();
			this.mCascadeCountCurve.IncInVal();
			this.mCascadeCountAlpha.IncInVal();
			this.mGemScalarAlpha.IncInVal();
			this.mBoostShowPct.IncInVal();
			this.mTimerInflate.IncInVal();
			this.mTimerAlpha.IncInVal();
			if (this.mKilling)
			{
				return;
			}
			if (this.mPointMultPosPct.CheckUpdatesFromEndThreshold(1))
			{
				this.mPointMultTextMorph.SetCurve("b+0,1,0.02,1,####         ~~###");
			}
			if (this.mBoostShowPct.CheckInThreshold((double)GlobalMembers.M(0.5f)))
			{
				this.ResolveMysteryGem();
			}
			if (!this.IsGameSuspended())
			{
				this.mGameTicks++;
				if (this.mGameTicks % 100 == 0)
				{
					this.AddToStat(0);
				}
				this.UpdateDeferredSounds();
			}
			this.FlipHeldSwaps();
			this.FindSets();
			this.FlipHeldSwaps();
			if (this.mGameOverPiece != null)
			{
				this.UpdateBombExplode();
			}
			else
			{
				if (this.mLightningStorms.Count == 0)
				{
					this.FillInBlanks();
				}
				this.UpdateMoveData();
				this.UpdateSwapping();
				this.UpdateFalling();
			}
			this.mPointMultSoundDelay = Math.Max(0, this.mPointMultSoundDelay - 1);
			if (this.mPointMultSoundDelay == 0 && this.mPointMultSoundQueue.Count > 0)
			{
				GlobalMembers.gApp.PlaySample(this.mPointMultSoundQueue[0]);
				this.mPointMultSoundQueue.RemoveAt(0);
				this.mPointMultSoundDelay = 40;
			}
			if (!this.CanPlay())
			{
				Piece selectedPiece = this.GetSelectedPiece();
				if (selectedPiece != null)
				{
					selectedPiece.mSelected = false;
					selectedPiece.mSelectorAlpha.SetConstant(0.0);
				}
			}
			for (int i = 0; i < 8; i++)
			{
				this.mBumpVelocities[i] = Math.Min(0f, this.mBumpVelocities[i] + GlobalMembers.M(0.21995f));
			}
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int j = array.GetLowerBound(0); j <= upperBound; j++)
			{
				for (int k = array.GetLowerBound(1); k <= upperBound2; k++)
				{
					Piece piece = array[j, k];
					if (piece != null)
					{
						if (piece.mImmunityCount != 0)
						{
							piece.mImmunityCount--;
						}
						if (piece.IsFlagSet(352U) && piece.mCounter == 0 && this.IsBoardStill())
						{
							this.BombExploded(piece);
						}
						if (piece.mSpinSpeed != 0f)
						{
							int num = 20;
							piece.mSpinFrame += piece.mSpinSpeed;
							if (piece.mSpinFrame < 0f)
							{
								piece.mSpinFrame += (float)num;
							}
							if (piece.IsFlagSet(512U))
							{
								if (piece.mSpinFrame >= (float)num)
								{
									piece.mSpinFrame = 0f;
									piece.mSpinSpeed = 0f;
								}
								if (piece.mSpinSpeed != 0f && piece.mSpinFrame >= 5f && piece.mSpinFrame <= 10f)
								{
									piece.mCounter--;
									piece.StampOverlay();
									piece.mSpinFrame = 16f;
								}
							}
						}
						if (piece.IsFlagSet(64U) && this.mGameOverCount == 0 && this.mLevelCompleteCount == 0 && !this.IsGameSuspended())
						{
							piece.mTimer = (piece.mTimer + 1) % piece.mTimerThreshold;
							if (piece.mTimer == 0)
							{
								this.DecrementCounterGem(piece, false);
							}
						}
					}
				}
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num2 = 0;
			int num3 = 0;
			Piece[,] array2 = this.mBoard;
			int upperBound3 = array2.GetUpperBound(0);
			int upperBound4 = array2.GetUpperBound(1);
			for (int l = array2.GetLowerBound(0); l <= upperBound3; l++)
			{
				for (int m = array2.GetLowerBound(1); m <= upperBound4; m++)
				{
					Piece piece2 = array2[l, m];
					if (piece2 != null && piece2.mExplodeDelay > 0 && --piece2.mExplodeDelay == 0)
					{
						if (piece2.IsFlagSet(32768U))
						{
							int mImmunityCount = piece2.mImmunityCount;
							piece2.mImmunityCount = 1;
							num3 += (int)piece2.CX();
							num2++;
							this.ExplodeAt((int)piece2.CX(), (int)piece2.CY());
							flag = true;
							piece2.mImmunityCount = mImmunityCount;
							piece2.ClearFlag(32768U);
						}
						else if (piece2.IsFlagSet(524289U) || (piece2.IsFlagSet(4U) && piece2.mImmunityCount > 0))
						{
							if (piece2.IsFlagSet(524288U))
							{
								int num4 = this.mGameStats[21];
								this.AddPoints((int)piece2.CX(), (int)piece2.CY(), 1000 * num4, GlobalMembers.gGemColors[piece2.mColor], (uint)piece2.mMatchId, false, false, -1);
								this.AddToStat(21, 1, piece2.mMoveCreditId);
								int mMoveCreditId = piece2.mMoveCreditId;
								int num5 = 0;
								Piece[,] array3 = this.mBoard;
								int upperBound5 = array3.GetUpperBound(0);
								int upperBound6 = array3.GetUpperBound(1);
								for (int n = array3.GetLowerBound(0); n <= upperBound5; n++)
								{
									for (int num6 = array3.GetLowerBound(1); num6 <= upperBound6; num6++)
									{
										Piece piece3 = array3[n, num6];
										if (piece3 != null && piece3.IsFlagSet(525335U))
										{
											if (piece3.IsFlagSet(1024U))
											{
												if (num5 == 0)
												{
													num5++;
												}
												this.TallyCoin(piece3);
												piece3.ClearFlag(1024U);
											}
											else
											{
												piece3.mMoveCreditId = mMoveCreditId;
												piece3.mExplodeDelay = GlobalMembers.M(1) + num5 * GlobalMembers.M(25);
												piece3.mImmunityCount = 0;
												num5++;
											}
										}
									}
								}
							}
							else
							{
								this.AddToStat(12, 1, piece2.mMoveCreditId);
							}
							num3 += (int)piece2.CX();
							num2++;
							this.AddPoints((int)piece2.CX(), (int)piece2.CY(), GlobalMembers.M(20), Color.White, (uint)piece2.mMatchId, true, true, piece2.mMoveCreditId);
							this.ExplodeAt((int)piece2.CX(), (int)piece2.CY());
							flag = true;
						}
						else if (!piece2.IsFlagSet(1046U) || !this.TriggerSpecial(piece2, null))
						{
							if (piece2.IsFlagSet(128U))
							{
								this.TallyPiece(piece2, true);
								this.DeletePiece(piece2);
							}
							else
							{
								if (piece2.IsFlagSet(16U))
								{
									if (!flag3)
									{
										GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_MULTIPLIER_HURRAHED);
										flag3 = true;
									}
								}
								else
								{
									flag2 = true;
								}
								if (piece2.IsFlagSet(6144U))
								{
									this.AddPoints((int)piece2.CX(), (int)piece2.CY(), GlobalMembers.M(300), Color.White, (uint)piece2.mMatchId, true, true, piece2.mMoveCreditId);
								}
								this.SmallExplodeAt(piece2, piece2.CX(), piece2.CY(), true, false);
							}
						}
					}
				}
			}
			if (flag)
			{
				if (this.WantsCalmEffects())
				{
					if (num2 > 0)
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_PREBLAST, 0, GlobalMembers.M(0.5), GlobalMembers.M(-1.0));
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_GEM_SHATTERS, this.GetPanPosition(num3 / num2), GlobalMembers.M(0.5), GlobalMembers.M(-1.0));
					}
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BOMB_EXPLODE, 0, GlobalMembers.M(0.5), GlobalMembers.M(-1.0));
				}
				else
				{
					if (num2 > 0)
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_PREBLAST);
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_GEM_SHATTERS, this.GetPanPosition(num3 / num2));
					}
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BOMB_EXPLODE);
				}
			}
			else if (flag2)
			{
				if (this.WantsCalmEffects())
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SMALL_EXPLODE, 0, GlobalMembers.M(0.5), GlobalMembers.M(-1.0));
				}
				else
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SMALL_EXPLODE);
				}
			}
			Piece[,] array4 = this.mBoard;
			int upperBound7 = array4.GetUpperBound(0);
			int upperBound8 = array4.GetUpperBound(1);
			for (int num7 = array4.GetLowerBound(0); num7 <= upperBound7; num7++)
			{
				for (int num8 = array4.GetLowerBound(1); num8 <= upperBound8; num8++)
				{
					Piece piece4 = array4[num7, num8];
					if (piece4 != null)
					{
						if (!piece4.mScale.IncInVal())
						{
							if (piece4.mScale > 1.0)
							{
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_F, piece4.mScale);
							}
							else if (piece4.mScale == 1.0)
							{
								piece4.mScale.SetConstant(1.0);
							}
							else if (piece4.mScale == 0.0 && !piece4.mDestPct.IsDoingCurve())
							{
								this.DeletePiece(piece4);
								goto IL_B46;
							}
						}
						piece4.mSelectorAlpha.IncInVal();
						if (piece4.mRotPct != 0f || piece4.mSelected)
						{
							piece4.mRotPct += GlobalMembers.M(0.02f);
							if (piece4.mRotPct >= 1f)
							{
								piece4.mRotPct = 0f;
							}
						}
						if (piece4.mDestPct != 0.0)
						{
							piece4.mX = (float)((double)this.GetColX(piece4.mCol) * (1.0 - piece4.mDestPct) + (double)this.GetColX(piece4.mDestCol) * piece4.mDestPct);
							piece4.mY = (float)((double)this.GetRowY(piece4.mRow) * (1.0 - piece4.mDestPct) + (double)this.GetRowY(piece4.mDestRow) * piece4.mDestPct);
						}
						else
						{
							piece4.mFlyVY += piece4.mFlyAY;
							piece4.mX += piece4.mFlyVX;
							piece4.mY += piece4.mFlyVY;
						}
					}
					IL_B46:;
				}
			}
			if (this.mLightningStorms.Count == 0)
			{
				this.FillInBlanks();
			}
			if (this.IsBoardStill())
			{
				this.mNOfIntentionalMatchesDuringCascade = 0;
				this.CheckForTutorialDialogs();
				if (this.mGameOverCount == 0 && !this.ForceSwaps() && !this.FindMove(null, 0, true, true) && !this.mWantLevelup)
				{
					this.GameOver();
				}
			}
			this.UpdateLightning();
			if (this.IsGameIdle())
			{
				this.mIdleTicks++;
			}
			if (this.mComboFlashPct.IsInitialized() && !this.mComboFlashPct.HasBeenTriggered())
			{
				this.mComboCountDisp = Math.Min((float)this.mComboLen, this.mComboCountDisp + GlobalMembers.M(0.04f));
			}
			else if (this.mComboCountDisp < (float)this.mComboCount)
			{
				this.mComboCountDisp = Math.Min((float)this.mComboCount, this.mComboCountDisp + GlobalMembers.M(0.04f));
			}
			else
			{
				this.mComboCountDisp = Math.Max((float)this.mComboCount, this.mComboCountDisp - GlobalMembers.M(0.04f));
			}
			if (this.mComboFlashPct.IsInitialized() && !this.mComboFlashPct.IncInVal())
			{
				this.NewCombo();
			}
			this.UpdateSpeedBonus();
			this.UpdateCountPopups();
			this.UpdateComplements();
			if (this.WantTopLevelBar())
			{
				this.UpdateLevelBar();
			}
			else
			{
				this.UpdateCountdownBar();
			}
			this.UpdateHint();
			if (GlobalMembers.gApp.mMenus[8].GetState() == Bej3WidgetState.STATE_OUT && GlobalMembers.gApp.mMenus[11].GetState() == Bej3WidgetState.STATE_OUT && this.mGameOverCount > 0)
			{
				this.mDeferredTutorialVector.Clear();
				if (++this.mGameOverCount >= this.GetGameOverCountTreshold() && GlobalMembers.gApp.mInterfaceState != InterfaceState.INTERFACE_STATE_GAMEDETAILMENU && GlobalMembers.gApp.mDialogList.Count == 0 && !this.mQuestPortalPct.IsDoingCurve() && GlobalMembers.gApp.mProfile.mDeferredBadgeVector.Count == 0)
				{
					this.GameOverExit();
				}
			}
			float num9 = (float)this.mAlphaCurve;
			this.mPostFXManager.mAlpha = this.GetPieceAlpha() * num9;
			this.mPreFXManager.mAlpha = this.GetPieceAlpha() * num9;
			if (this.IsBoardStill() && GlobalMembers.gApp.mCurrentGameMode != GameMode.MODE_DIAMOND_MINE)
			{
				if (!this.mSunPosition.IsDoingCurve())
				{
					float num10 = Math.Max(0.1f, (float)this.mGameStats[15] / ((float)this.mGameTicks / 100f));
					if ((GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.01f) && this.mLastSunTick == 0 && this.mUpdateCnt >= GlobalMembers.M(50)) || (GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.003f) * num10 && !this.mSunFired) || GlobalMembersUtils.GetRandFloatU() < GlobalMembers.M(0.0006f) * num10)
					{
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SUN_POSITION, this.mSunPosition);
						this.mSunFired = true;
						this.mLastSunTick = this.mUpdateCnt;
					}
				}
				else
				{
					this.mSunFired = false;
				}
			}
			if (this.mComboFlashPct == 0.0)
			{
				int num11 = (int)(44.0 - (double)this.mComboLen * 5.5);
				float num12 = (float)(-(float)this.mComboCount * num11 + (this.mComboLen - 1) * num11 / 2);
				this.mComboSelectorAngle += (num12 - this.mComboSelectorAngle) / 20f;
			}
			if (this.mWantLevelup && this.mDeferredTutorialVector.Count == 0 && this.mQueuedMoveVector.Count == 0 && this.IsBoardStill() && !this.mInReplay && !this.mWasLevelUpReplay)
			{
				if (!this.mInReplay)
				{
					GlobalMembers.gApp.DisableOptionsButtons(true);
				}
				GlobalMembers.KILL_WIDGET(this.mHyperspace);
				this.mHyperspace = null;
				Announcement announcement = new Announcement(this, GlobalMembers._ID("LEVEL\nCOMPLETE", 139));
				announcement.mBlocksPlay = false;
				announcement.mDarkenBoard = false;
				GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_LEVELCOMPLETE);
				GlobalMembers.gApp.mForceBkg = string.Empty;
				GlobalMembers.gSexyApp.mGraphicsDriver.GetRenderDevice3D();
				this.mHyperspace = new HyperspaceWhirlpool(this);
				if (this.mHyperspace != null)
				{
					if (this.mHyperspace.IsUsing3DTransition())
					{
						this.HideReplayWidget();
						this.mHyperspace.SetBGImage(this.mBackground.GetBackgroundImage());
						this.mHyperspace.Resize(0, 0, this.mWidth, this.mHeight);
						this.mWidgetManager.AddWidget(this.mHyperspace);
						this.mWidgetManager.PutInfront(this.mHyperspace, this);
					}
					else
					{
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_ALPHA_LEVEL_UP, this.mAlpha);
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SCALE_LEVEL_UP, this.mScale);
						this.mHyperspace.SetBGImage(this.mBackground.GetBackgroundImage());
						this.SetupBackground(1);
						this.mHyperspace.Resize(0, 0, this.mWidth, this.mHeight);
						this.mWidgetManager.AddWidget(this.mHyperspace);
						this.mWidgetManager.PutBehind(this.mHyperspace, this);
						this.mBackground.mVisible = false;
					}
				}
				this.mWantLevelup = false;
			}
			if (this.mPointMultDarkenPct > 0.0 && !this.mTimeExpired)
			{
				if (this.mLightningStorms.Count > 0)
				{
					this.mBoardDarken = (float)Math.Max((double)this.mBoardDarken, this.mPointMultDarkenPct);
				}
				else
				{
					this.mBoardDarken = (float)this.mPointMultDarkenPct;
				}
			}
			if (this.mBoardDarkenAnnounce > 0f)
			{
				this.mBoardDarken = Math.Max(this.mBoardDarken, this.mBoardDarkenAnnounce);
			}
			if (this.mHintCooldownTicks > 0)
			{
				this.mHintCooldownTicks--;
				this.mHintButton.mMouseVisible = false;
			}
			else
			{
				this.mHintButton.mMouseVisible = true;
			}
			if (this.mLightningStorms.Count == 0 && this.mSpeedBonusFlameModePct > 0f)
			{
				this.mSpeedBonusFlameModePct = Math.Max(0f, this.mSpeedBonusFlameModePct - GlobalMembers.M(0.00125f));
				if (this.mSpeedBonusFlameModePct == 0f)
				{
					this.mSpeedBonusNum = 0.0;
					this.EncodeSpeedBonus();
				}
			}
			if (this.mPendingCoinAnimations == 0 && this.mGameOverCount == 0 && !this.mInReplay)
			{
				this.mMoneyDispGoal = GlobalMembers.gApp.GetCoinCount();
			}
			int num13 = 0;
			if (this.CanPiecesFall())
			{
				num13 |= 1;
			}
			if (!this.IsGameSuspended())
			{
				num13 |= 2;
			}
			if (num13 != this.mUReplayGameFlags)
			{
				if (this.WriteUReplayCmd(6))
				{
					this.mUReplayBuffer.WriteByte((byte)num13);
				}
				this.mUReplayGameFlags = num13;
			}
			if (this.mSettlingDelay > 0)
			{
				this.mSettlingDelay--;
			}
			if (this.mScrambleDelayTicks > 0)
			{
				this.mScrambleDelayTicks--;
			}
			this.CheckTrialGameFinished();
		}

		public void DoUReplayUpdate()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num5 = 0;
			int num6 = 0;
			Piece[,] array;
			int upperBound;
			int upperBound2;
			while (!this.mUReplayBuffer.AtEnd())
			{
				int mReadBitPos = this.mUReplayBuffer.mReadBitPos;
				byte b = this.mUReplayBuffer.ReadByte();
				int num7 = this.mUReplayLastTick;
				if ((b & 64) != 0)
				{
					num7 += (int)this.mUReplayBuffer.ReadShort();
					b &= 63;
				}
				else if ((b & 128) != 0)
				{
					num7 += (int)this.mUReplayBuffer.ReadByte();
					b &= 63;
				}
				if (this.mUpdateCnt < num7)
				{
					this.mUReplayBuffer.mReadBitPos = mReadBitPos;
					break;
				}
				switch (b)
				{
				case 0:
				{
					int num8 = (int)this.mUReplayBuffer.ReadByte();
					Piece piece = Piece.alloc(this);
					piece.mFlags = this.DecodePieceFlags(this.mUReplayBuffer.ReadByte());
					piece.mCol = num8 % 8;
					piece.mRow = num8 / 8;
					piece.mColor = (int)((sbyte)this.mUReplayBuffer.ReadByte());
					piece.mX = (float)this.GetColX(piece.mCol);
					piece.mY = (float)(this.mUReplayBuffer.ReadShort() * 100) / 256f + (float)this.GetRowY(0);
					if (piece.mRow >= 0 && piece.mRow < 8 && piece.mCol >= 0 && piece.mCol < 8)
					{
						if (piece.IsFlagSet(1024U))
						{
							GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COIN_CREATED, GlobalMembers.M(0), (double)GlobalMembers.M(0.1f));
							piece.mCounter = 3;
							ParticleEffect particleEffect = ParticleEffect.fromPIEffect(GlobalMembersResourcesWP.PIEFFECT_COINSPARKLE);
							particleEffect.mPieceRel = piece;
							particleEffect.mDoubleSpeed = true;
							this.mPostFXManager.AddEffect(particleEffect);
						}
						this.StartPieceEffect(piece);
						this.mBoard[piece.mRow, piece.mCol] = piece;
					}
					else
					{
						piece.release();
					}
					break;
				}
				case 1:
				{
					int num9 = (int)this.mUReplayBuffer.ReadByte();
					Piece piece2 = this.DecodePieceRef();
					Piece piece3 = this.DecodePieceRef();
					if ((num9 == 2 || num9 == 1) && piece2 != null && piece3 != null)
					{
						Piece piece4 = this.mBoard[piece2.mRow, piece2.mCol];
						this.mBoard[piece2.mRow, piece2.mCol] = this.mBoard[piece3.mRow, piece3.mCol];
						this.mBoard[piece3.mRow, piece3.mCol] = piece4;
						int num10 = piece2.mCol;
						piece2.mCol = piece3.mCol;
						piece3.mCol = num10;
						num10 = piece2.mRow;
						piece2.mRow = piece3.mRow;
						piece3.mRow = num10;
					}
					for (int i = 0; i < this.mSwapDataVector.Count; i++)
					{
						SwapData swapData = this.mSwapDataVector[i];
						if (swapData.mPiece1 == piece2 || swapData.mPiece2 == piece3)
						{
							if (swapData.mPiece1 != null)
							{
								swapData.mPiece1.mX = (float)this.GetColX(swapData.mPiece1.mCol);
								swapData.mPiece1.mY = (float)this.GetRowY(swapData.mPiece1.mRow);
								swapData.mPiece1 = null;
							}
							if (swapData.mPiece2 != null)
							{
								swapData.mPiece2.mX = (float)this.GetColX(swapData.mPiece2.mCol);
								swapData.mPiece2.mY = (float)this.GetRowY(swapData.mPiece2.mRow);
								swapData.mPiece2 = null;
							}
							this.mSwapDataVector.RemoveAt(i);
							i--;
						}
					}
					if (num9 != 2 && num9 != 3 && piece2 != null && piece3 != null)
					{
						if (num9 == 0)
						{
							GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_START_ROTATE);
						}
						else
						{
							GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_START_ROTATE);
						}
						SwapData swapData2 = new SwapData();
						swapData2.mPiece1 = piece2;
						swapData2.mPiece2 = piece3;
						swapData2.mSwapDir = new Point(piece3.mCol - piece2.mCol, piece3.mRow - piece2.mRow);
						if (num9 == 0)
						{
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SELECTOR_ALPHA, swapData2.mPiece1.mSelectorAlpha);
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SWAP_PCT_3, swapData2.mSwapPct);
							if (GlobalMembers.gIs3D)
							{
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GEM_SCALE_1, swapData2.mGemScale);
							}
							swapData2.mSwapPct.mIncRate *= (double)this.GetSwapSpeed();
							swapData2.mGemScale.mIncRate *= (double)this.GetSwapSpeed();
							swapData2.mForwardSwap = true;
						}
						else
						{
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SWAP_PCT_4, swapData2.mSwapPct);
							if (GlobalMembers.gIs3D)
							{
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_GEM_SCALE_2, swapData2.mGemScale);
							}
							swapData2.mForwardSwap = false;
						}
						swapData2.mIgnore = false;
						swapData2.mForceSwap = false;
						this.mSwapDataVector.Add(swapData2);
					}
					break;
				}
				case 2:
				{
					List<Piece> list = new List<Piece>();
					Piece piece5 = null;
					int num11 = 0;
					int num12 = (int)this.mUReplayBuffer.ReadByte();
					flag = (num12 & 1) != 0;
					bool flag4 = (num12 & 2) != 0;
					if (!flag4)
					{
						if (this.mUReplayVersion >= 3)
						{
							num4 = (int)this.mUReplayBuffer.ReadByte();
						}
						num3++;
					}
					int num13 = (int)this.mUReplayBuffer.ReadByte();
					for (int j = 0; j < num13; j++)
					{
						Piece piece6 = this.DecodePieceRef();
						piece6.mX = (float)this.GetColX(piece6.mCol);
						piece6.mY = (float)this.GetRowY(piece6.mRow);
						num11 += (int)piece6.GetScreenX();
						int num14 = (int)this.mUReplayBuffer.ReadByte();
						if (piece6 != null)
						{
							if (piece6.IsFlagSet(1024U))
							{
								this.TallyCoin(piece6);
								piece6.ClearFlag(1024U);
							}
							list.Add(piece6);
							if (num14 != 0)
							{
								if (num14 == 1)
								{
									this.Flamify(piece6);
								}
								else if (num14 == 2 && !piece6.IsFlagSet(4U))
								{
									this.Laserify(piece6);
								}
								else if (num14 == 3)
								{
									this.Hypercubeify(piece6);
								}
								piece5 = piece6;
							}
						}
					}
					num += num11 / num13;
					num2++;
					if ((num12 & 4) == 0)
					{
						if (piece5 != null)
						{
							for (int k = 0; k < list.Count; k++)
							{
								Piece piece7 = list[k];
								if (piece7 != piece5 && (piece7.mFlags == 0 || piece7.IsFlagSet(16384U)))
								{
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_E, piece7.mScale);
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_DEST_PCT_B, piece7.mDestPct);
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_ALPHA, piece7.mAlpha);
									piece7.mScale.mIncRate *= (double)this.GetMatchSpeed();
									piece7.mDestPct.mIncRate *= (double)this.GetMatchSpeed();
									piece7.mAlpha.mIncRate *= (double)this.GetMatchSpeed();
									piece7.mDestCol = piece5.mCol;
									piece7.mDestRow = piece5.mRow;
									int num15 = piece5.mCol - piece7.mCol;
									int num16 = piece5.mRow - piece7.mRow;
									if (piece5.IsFlagSet(1U))
									{
										PopAnimEffect popAnimEffect = PopAnimEffect.fromPopAnim(GlobalMembersResourcesWP.POPANIM_FLAMEGEMCREATION);
										popAnimEffect.mPieceRel = piece7;
										popAnimEffect.mX = piece7.CX();
										popAnimEffect.mY = piece7.CY();
										popAnimEffect.mOverlay = true;
										popAnimEffect.mDoubleSpeed = true;
										if (num15 != 0)
										{
											popAnimEffect.Play("smear horizontal");
											if (num15 < 0)
											{
												popAnimEffect.mAngle = 3.14159274f;
											}
										}
										else
										{
											popAnimEffect.Play("smear vertical");
											if (num16 < 0)
											{
												popAnimEffect.mAngle = 3.14159274f;
											}
										}
										this.mPostFXManager.AddEffect(popAnimEffect);
									}
								}
							}
						}
						else if ((num12 & 8) == 0)
						{
							for (int l = 0; l < list.Count; l++)
							{
								Piece piece8 = list[l];
								if (flag4)
								{
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_A, piece8.mScale);
								}
								else if (this.WantBulgeCascades())
								{
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_B, piece8.mScale);
								}
								else
								{
									GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_B, piece8.mScale);
								}
								if (!GlobalMembers.gIs3D)
								{
									piece8.mRotPct = 0f;
								}
								piece8.mScale.mIncRate *= (double)this.GetMatchSpeed();
							}
						}
					}
					break;
				}
				case 3:
				{
					int theX = (int)this.DecodeX(this.mUReplayBuffer.ReadShort());
					int theY = (int)this.DecodeY(this.mUReplayBuffer.ReadShort());
					int thePoints = (int)this.mUReplayBuffer.ReadLong();
					Color theColor = new Color((int)this.mUReplayBuffer.ReadLong());
					int theId = (int)this.mUReplayBuffer.ReadShort();
					bool usePointMultiplier = this.mUReplayBuffer.ReadBoolean();
					this.DoAddPoints(theX, theY, thePoints, theColor, (uint)theId, true, usePointMultiplier, -1);
					break;
				}
				case 4:
				{
					Piece piece9 = this.DecodePieceRef();
					if (piece9 != null)
					{
						this.DeletePiece(piece9);
					}
					break;
				}
				case 5:
				{
					byte b2 = this.mUReplayBuffer.ReadByte();
					byte b3 = this.mUReplayBuffer.ReadByte();
					byte b4 = this.mUReplayBuffer.ReadByte();
					int num17 = (int)b3;
					for (int m = 0; m < (int)b4; m++)
					{
						this.mBoard[num17, (int)b2] = this.mBoard[num17 - 1, (int)b2];
						if (this.mBoard[num17, (int)b2] != null)
						{
							this.mBoard[num17, (int)b2].mRow++;
						}
						num17--;
					}
					this.mBoard[num17, (int)b2] = null;
					break;
				}
				case 6:
					this.mUReplayGameFlags = (int)this.mUReplayBuffer.ReadByte();
					break;
				case 7:
					this.mUReplayTicksLeft = (int)this.mUReplayBuffer.ReadShort();
					break;
				case 8:
				{
					int num18 = (int)this.mUReplayBuffer.ReadByte();
					this.mSpeedBonusNum = (double)((float)this.mUReplayBuffer.ReadByte() / 255f);
					if (num18 != this.mSpeedBonusCount)
					{
						if (num18 > 0)
						{
							if (this.mSpeedBonusCount == 0)
							{
								GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_DISP_ON, this.mSpeedBonusDisp);
							}
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_POINTS_GLOW, this.mSpeedBonusPointsGlow);
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_POINTS_SCALE_ON, this.mSpeedBonusPointsScale);
							this.mSpeedBonusCountHighest = Math.Max(this.mSpeedBonusCountHighest, this.mSpeedBonusCount);
						}
						else
						{
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_DISP_OFF, this.mSpeedBonusDisp);
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_POINTS_SCALE_OFF_UREPLAY, this.mSpeedBonusPointsScale);
						}
						this.mSpeedBonusCount = num18;
					}
					break;
				}
				case 9:
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SPEED_BONUS_DISP_ON, this.mSpeedBonusDisp);
					this.DoSpeedText(0);
					break;
				case 10:
				{
					int num19 = (int)this.DecodeX(this.mUReplayBuffer.ReadShort());
					int theY2 = (int)this.DecodeY(this.mUReplayBuffer.ReadShort());
					num6 += num19;
					num5++;
					flag2 = true;
					this.ExplodeAt(num19, theY2);
					break;
				}
				case 11:
				{
					flag3 = true;
					Piece piece10 = this.DecodePieceRef();
					if (piece10 != null)
					{
						this.SmallExplodeAt(piece10, piece10.CX(), piece10.CY(), true, false);
					}
					break;
				}
				case 12:
				{
					Piece piece11 = this.DecodePieceRef();
					float num20 = (float)this.mUReplayBuffer.ReadShort() / 25600f * 100f;
					if (piece11 != null)
					{
						for (int n = 0; n <= piece11.mRow; n++)
						{
							Piece pieceAtRowCol = this.GetPieceAtRowCol(n, piece11.mCol);
							if (pieceAtRowCol != null)
							{
								pieceAtRowCol.mFallVelocity = Math.Min(pieceAtRowCol.mFallVelocity, num20);
							}
						}
					}
					break;
				}
				case 13:
				{
					Piece piece12 = this.DecodePieceRef();
					if (piece12 != null)
					{
						this.IncPointMult(piece12);
						piece12.ClearFlag(16U);
						this.mPostFXManager.FreePieceEffect(piece12.mId);
					}
					break;
				}
				case 14:
				{
					Piece piece13 = this.DecodePieceRef();
					if (piece13 != null)
					{
						LightningStorm lightningStorm = new LightningStorm(this, piece13, 2);
						this.mLightningStorms.Add(lightningStorm);
					}
					break;
				}
				case 15:
				{
					Piece piece14 = this.DecodePieceRef();
					int mColor = (int)this.mUReplayBuffer.ReadByte();
					if (piece14 != null)
					{
						LightningStorm lightningStorm2 = new LightningStorm(this, piece14, 7);
						lightningStorm2.mColor = mColor;
						this.mLightningStorms.Add(lightningStorm2);
					}
					break;
				}
				case 16:
				{
					Piece piece15 = this.DecodePieceRef();
					if (piece15 != null)
					{
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_HINT_ALPHA, piece15.mHintAlpha);
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_HINT_ARROW_POS, piece15.mHintArrowPos);
					}
					break;
				}
				case 17:
					this.DoGemCountPopup((int)this.mUReplayBuffer.ReadByte());
					break;
				case 18:
					this.DoCascadePopup((int)this.mUReplayBuffer.ReadByte());
					break;
				case 19:
				{
					Piece piece16 = this.DecodePieceRef();
					int num21 = (int)this.mUReplayBuffer.ReadByte();
					if (piece16 != null)
					{
						piece16.ClearFlag(8192U);
						if (num21 == 1)
						{
							this.Flamify(piece16);
						}
						else if (num21 == 2)
						{
							this.Laserify(piece16);
						}
						else if (num21 == 3)
						{
							this.Hypercubeify(piece16);
						}
					}
					break;
				}
				case 20:
				{
					Piece piece17 = this.DecodePieceRef();
					if (piece17 != null)
					{
						array = this.mBoard;
						upperBound = array.GetUpperBound(0);
						upperBound2 = array.GetUpperBound(1);
						for (int num22 = array.GetLowerBound(0); num22 <= upperBound; num22++)
						{
							for (int num23 = array.GetLowerBound(1); num23 <= upperBound2; num23++)
							{
								Piece piece18 = array[num22, num23];
								if (piece18 != null && piece18.IsFlagSet(1024U))
								{
									this.TallyCoin(piece18);
									piece18.ClearFlag(1024U);
								}
							}
						}
					}
					break;
				}
				case 21:
				{
					int num24 = (int)this.mUReplayBuffer.ReadByte();
					List<Piece> list2 = new List<Piece>();
					List<Piece> list3 = new List<Piece>();
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SCRAMBLE);
					for (int num25 = 0; num25 < num24; num25++)
					{
						Piece piece19 = this.DecodePieceRef();
						Piece piece20 = this.DecodePieceRef();
						if (piece19 != null && piece20 != null)
						{
							list2.Add(piece19);
							list3.Add(piece20);
							piece19.mDestCol = piece19.mCol;
							piece19.mDestRow = piece19.mRow;
							GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_DEST_PCT_C, piece19.mDestPct);
						}
					}
					for (int num26 = 0; num26 < list2.Count; num26++)
					{
						Piece piece21 = list2[num26];
						Piece piece22 = list3[num26];
						Piece piece23 = this.mBoard[piece21.mRow, piece21.mCol];
						this.mBoard[piece21.mRow, piece21.mCol] = this.mBoard[piece22.mRow, piece22.mCol];
						this.mBoard[piece22.mRow, piece22.mCol] = piece23;
						int num27 = piece21.mCol;
						piece21.mCol = piece22.mCol;
						piece22.mCol = num27;
						num27 = piece21.mRow;
						piece21.mRow = piece22.mRow;
						piece22.mRow = num27;
						float num28 = piece21.mX;
						piece21.mX = piece22.mX;
						piece22.mX = num28;
						num28 = piece21.mY;
						piece21.mY = piece22.mY;
						piece22.mY = num28;
					}
					if (this.mScrambleUsesLeft > 1)
					{
						this.mScrambleUsesLeft--;
					}
					break;
				}
				}
				this.mUReplayLastTick = this.mUpdateCnt;
			}
			if ((this.mUReplayLastTick == 0 && this.mUReplayBuffer.AtEnd()) || this.mUpdateCnt == this.mUReplayTotalTicks)
			{
				this.mInReplay = false;
				if (this.mCurrentHint != null)
				{
					this.OnAchievementHintFinished(this.mCurrentHint);
					this.mCurrentHint = null;
				}
				((PauseMenu)GlobalMembers.gApp.mMenus[7]).SetTopButtonType(this.mReplayWasTutorial ? Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS : Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
				GlobalMembers.gApp.DisableOptionsButtons(false);
				this.ToggleReplayPulse(false);
			}
			if (flag2)
			{
				if (num5 > 0)
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_PREBLAST);
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_GEM_SHATTERS, this.GetPanPosition(num6 / num5));
				}
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BOMB_EXPLODE);
			}
			else if (flag3)
			{
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_SMALL_EXPLODE);
			}
			base.Update();
			if (num2 > 0)
			{
				int panPosition = this.GetPanPosition(num / num2 + 50);
				if (num2 > 1)
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_DOUBLESET, panPosition);
				}
				int num29 = num4 + 1;
				if (num29 > 6)
				{
					num29 = 6;
				}
				if (flag && this.mSpeedBonusCount > 0)
				{
					if (this.mSpeedBonusNum > 0.01)
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_FLAMESPEED1, panPosition, 1.0, this.mSpeedBonusNum * (double)GlobalMembers.M(1f));
					}
					else
					{
						GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.GetSoundById(1721 + Math.Min(8, this.mSpeedBonusCount)), panPosition, 1.0, this.mSpeedBonusNum * (double)GlobalMembers.M(1f));
					}
				}
				else
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COMBO_1 + num29, panPosition);
				}
			}
			if (this.mAnnouncements.Count > 0)
			{
				this.mAnnouncements[0].Update();
			}
			this.mSunPosition.IncInVal();
			this.mAlpha.IncInVal();
			this.mScale.IncInVal();
			this.mPrevPointMultAlpha.IncInVal();
			this.mPointMultPosPct.IncInVal();
			this.mPointMultTextMorph.IncInVal();
			this.mSpeedBonusDisp.IncInVal();
			this.mSpeedBonusPointsGlow.IncInVal();
			this.mSpeedBonusPointsScale.IncInVal();
			this.mTutorialPieceIrisPct.IncInVal();
			this.mGemCountCurve.IncInVal();
			this.mGemCountAlpha.IncInVal();
			this.mGemScalarAlpha.IncInVal();
			this.mCascadeCountCurve.IncInVal();
			this.mCascadeCountAlpha.IncInVal();
			this.mGemScalarAlpha.IncInVal();
			this.mBoostShowPct.IncInVal();
			this.mTimerInflate.IncInVal();
			this.mTimerAlpha.IncInVal();
			if (this.mPointMultPosPct.CheckUpdatesFromEndThreshold(1))
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_POINT_MULT_TEXT_MORPH, this.mPointMultTextMorph);
			}
			array = this.mBoard;
			upperBound = array.GetUpperBound(0);
			upperBound2 = array.GetUpperBound(1);
			for (int num22 = array.GetLowerBound(0); num22 <= upperBound; num22++)
			{
				for (int num23 = array.GetLowerBound(1); num23 <= upperBound2; num23++)
				{
					Piece piece24 = array[num22, num23];
					if (piece24 != null && !piece24.mScale.IncInVal() && piece24.mScale > 1.0)
					{
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SCALE_F, piece24.mScale);
					}
				}
			}
			array = this.mBoard;
			upperBound = array.GetUpperBound(0);
			upperBound2 = array.GetUpperBound(1);
			for (int num22 = array.GetLowerBound(0); num22 <= upperBound; num22++)
			{
				for (int num23 = array.GetLowerBound(1); num23 <= upperBound2; num23++)
				{
					Piece piece25 = array[num22, num23];
					if (piece25 != null)
					{
						piece25.mSelectorAlpha.IncInVal();
					}
				}
			}
			array = this.mBoard;
			upperBound = array.GetUpperBound(0);
			upperBound2 = array.GetUpperBound(1);
			for (int num22 = array.GetLowerBound(0); num22 <= upperBound; num22++)
			{
				for (int num23 = array.GetLowerBound(1); num23 <= upperBound2; num23++)
				{
					Piece piece26 = array[num22, num23];
					if (piece26 != null && piece26.mDestPct != 0.0)
					{
						piece26.mX = (float)((double)this.GetColX(piece26.mCol) * (1.0 - piece26.mDestPct) + (double)this.GetColX(piece26.mDestCol) * piece26.mDestPct);
						piece26.mY = (float)((double)this.GetRowY(piece26.mRow) * (1.0 - piece26.mDestPct) + (double)this.GetRowY(piece26.mDestRow) * piece26.mDestPct);
					}
				}
			}
			array = this.mBoard;
			upperBound = array.GetUpperBound(0);
			upperBound2 = array.GetUpperBound(1);
			for (int num22 = array.GetLowerBound(0); num22 <= upperBound; num22++)
			{
				for (int num23 = array.GetLowerBound(1); num23 <= upperBound2; num23++)
				{
					Piece piece27 = array[num22, num23];
					if (piece27 != null)
					{
						piece27.mDestPct.IncInVal();
						piece27.mAlpha.IncInVal();
						piece27.mHintAlpha.IncInVal();
						piece27.mHintArrowPos.IncInVal();
					}
				}
			}
			if (this.mFlameSound == null)
			{
				this.mFlameSound = GlobalMembers.gSexyApp.mSoundManager.GetSoundInstance(GlobalMembersResourcesWP.SOUND_FLAMELOOP);
				if (this.mFlameSound != null)
				{
					this.mFlameSound.SetVolume((GlobalMembers.gApp.mMuteCount > 0) ? 0.0 : GlobalMembers.gApp.mSfxVolume);
					this.mFlameSound.Play(true, false);
				}
			}
			if (this.mFlameSound != null)
			{
				this.mFlameSound.SetVolume(Math.Max(0.0, 1.0 - (1.0 - this.mSpeedBonusNum) * GlobalMembers.M(2.5)) * this.mAlpha * (double)this.GetPieceAlpha());
			}
			this.mPreFXManager.Update();
			this.mPostFXManager.Update();
			if (!this.IsGameSuspended() || this.mLevelCompleteCount != 0)
			{
				this.mLevelBarPIEffect.Update();
				this.mCountdownBarPIEffect.Update();
				if (this.mSpeedBonusNum > 0.0)
				{
					this.mSpeedFirePIEffect.Update();
				}
				for (int num30 = 0; num30 < 2; num30++)
				{
					if (this.mSpeedFireBarPIEffect[num30] != null)
					{
						this.mSpeedFireBarPIEffect[num30].Update();
						if (!this.mSpeedFireBarPIEffect[num30].IsActive())
						{
							if (this.mSpeedFireBarPIEffect[num30] != null)
							{
								this.mSpeedFireBarPIEffect[num30].Dispose();
							}
							this.mSpeedFireBarPIEffect[num30] = null;
						}
					}
				}
			}
			array = this.mBoard;
			upperBound = array.GetUpperBound(0);
			upperBound2 = array.GetUpperBound(1);
			for (int num22 = array.GetLowerBound(0); num22 <= upperBound; num22++)
			{
				for (int num23 = array.GetLowerBound(1); num23 <= upperBound2; num23++)
				{
					Piece piece28 = array[num22, num23];
					if (piece28 != null)
					{
						piece28.Update();
					}
				}
			}
			this.UpdateLevelBar();
			this.UpdateCountdownBar();
			this.UpdateSwapping();
			this.UpdateFalling();
			this.UpdateLightning();
			this.UpdatePoints();
			if (this.mLightningStorms.Count == 0 && this.mSpeedBonusFlameModePct > 0f)
			{
				this.mSpeedBonusFlameModePct = (float)Math.Max(0.0, (double)this.mSpeedBonusFlameModePct - GlobalMembers.M(0.00125));
			}
		}

		public virtual void BackToMenu()
		{
			if (!this.mFromDebugMenu)
			{
				if (!this.mKilling)
				{
					GlobalMembers.gApp.DoPlayMenu();
					this.mBackground.SetVisible(false);
					if (this.mAlpha > 0.0)
					{
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_ALPHA_BACK_TO_MENU, this.mAlpha);
					}
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_SCALE_BACK_TO_MENU, this.mScale);
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BACKTOMAIN);
					this.mKilling = true;
					this.DisableUI(true);
					return;
				}
			}
			else
			{
				GlobalMembers.gApp.DoMenu();
			}
		}

		public virtual void GameOverExit()
		{
			this.BackToMenu();
		}

		public virtual void DoUpdate()
		{
			if (GlobalMembers.gApp.GetDialog(41) != null || GlobalMembers.gApp.GetDialog(43) != null || GlobalMembers.gApp.GetDialog(39) != null)
			{
				GlobalMembers.gGR.mRecordDraws = false;
				base.Update();
				return;
			}
			if (GlobalMembers.gApp.GetDialog(18) != null)
			{
				GlobalMembers.gGR.mRecordDraws = false;
			}
			if (((GlobalMembers.gApp.mProfile.mDeferredBadgeVector.Count > 0 && (this.mGameFinished || (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN && this.mZenDoBadgeAward))) || this.mDoRankUp) && (this.mGameOverCount == 0 || this.mGameOverCount >= GlobalMembers.M(300)))
			{
				GlobalMembers.gGR.mRecordDraws = false;
				RankBarWidget rankBarWidget = (RankBarWidget)GlobalMembers.gApp.GetDialog(34);
				if (this.mDoRankUp && GlobalMembers.gApp.GetDialog(23) == null)
				{
					RankUpDialog theDialog = new RankUpDialog(this);
					GlobalMembers.gApp.AddDialog(theDialog);
					this.mGameStats[1] = 0;
					GlobalMembers.gApp.mMenus[7].Transition_SlideOut();
					this.mDoRankUp = false;
				}
				else if (GlobalMembers.gApp.mProfile.mDeferredBadgeVector.Count > 0 && (this.mGameFinished || (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN && this.mZenDoBadgeAward)) && rankBarWidget == null && this.mHyperspace == null)
				{
					if (GlobalMembers.gApp.mMenus[8].GetState() != Bej3WidgetState.STATE_OUT)
					{
						return;
					}
					GlobalMembers.gApp.DoBadgeMenu(1, GlobalMembers.gApp.mProfile.mDeferredBadgeVector);
					GlobalMembers.gApp.mMenus[7].Transition_SlideOut();
					GlobalMembers.gApp.mProfile.mDeferredBadgeVector.Clear();
					this.mZenDoBadgeAward = false;
					return;
				}
				if (GlobalMembers.gApp.GetDialog(23) != null)
				{
					this.mSlideUIPct.IncInVal();
					return;
				}
			}
			bool flag = this.mHadReplayError;
			if (this.mInUReplay)
			{
				this.DoUReplayUpdate();
				return;
			}
			if (this.mAlpha == 1.0 && this.mScale == 1.0 && this.mUpdateCnt >= 200)
			{
				this.UpdateTooltip();
			}
			if (!this.mUserPaused)
			{
				this.mPreFXManager.Update();
				this.mPostFXManager.Update();
			}
			if (this.mUserPaused || !GlobalMembers.gApp.mHasFocus || GlobalMembers.gApp.mInterfaceState == InterfaceState.INTERFACE_STATE_GAMEDETAILMENU || (GlobalMembers.gApp.GetDialog(40) != null && this.mTimerAlpha == 1.0) || GlobalMembers.gApp.GetDialog(21) != null || GlobalMembers.gApp.GetDialog(22) != null)
			{
				this.mBoardHidePct = Math.Min(1f, this.mBoardHidePct + GlobalMembers.M(0.035f));
				if (GlobalMembers.gApp.mDialogList.Count == 0 && this.mStartDelay == 0)
				{
					this.mVisPausePct = Math.Min(1f, this.mVisPausePct + GlobalMembers.M(0.035f));
				}
				base.Update();
				if (this.mVisPausePct >= 1f)
				{
					bool flag2 = this.mSuspendingGame;
				}
				return;
			}
			if (this.mScale == 1.0)
			{
				this.mBoardHidePct = Math.Max(0f, this.mBoardHidePct - GlobalMembers.M(0.075f));
				this.mVisPausePct = Math.Max(0f, this.mVisPausePct - GlobalMembers.M(0.075f));
			}
			if (GlobalMembers.gApp.GetDialog(19) != null)
			{
				return;
			}
			if (this.mStartDelay > 0)
			{
				if (this.mStartDelay == GlobalMembers.M(10))
				{
					this.CheckForTutorialDialogs();
					if (GlobalMembers.gApp.GetDialog(19) != null)
					{
						return;
					}
				}
				if (--this.mStartDelay == 0)
				{
					this.DisableUI(false);
				}
				return;
			}
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null)
					{
						piece.Update();
					}
				}
			}
			if (GlobalMembers.gApp.mDialogMap.Count != 0 && !this.mInReplay)
			{
				if (GlobalMembers.gApp.GetDialog(18) != null && this.mAnnouncements.Count > 0)
				{
					this.mAnnouncements[0].Update();
				}
				if (GlobalMembers.gApp.GetDialog(44) == null)
				{
					return;
				}
			}
			if (this.mAlpha == 1.0 && this.mScale == 1.0 && GlobalMembers.gApp.mDialogMap.Count == 0 && this.mGameOverCount == 0 && this.mHasBoardSettled)
			{
				int timeLimit = this.GetTimeLimit();
				if (timeLimit > 0 && this.mTimeDelayCount > 0)
				{
					this.mWantTimeAnnouncement = true;
					if (--this.mTimeDelayCount == 0)
					{
						Announcement announcement = new Announcement(this, string.Format("{0}:{1:d2}", timeLimit / 60, timeLimit % 60));
						announcement.mBlocksPlay = false;
						announcement.mAlpha.mIncRate *= GlobalMembers.M(2.0);
						announcement.mScale.mIncRate *= GlobalMembers.M(2.0);
						announcement.mDarkenBoard = false;
						announcement.mTimeAnnouncement = true;
						if (!GlobalMembers.gApp.mMainMenu.mIsFullGame() && !this.mTrialPromptShown)
						{
							this.DoPrompt();
						}
					}
				}
				if (this.mReadyDelayCount > 0 && --this.mReadyDelayCount == GlobalMembers.M(110))
				{
					if (GlobalMembers.M(1) != 0)
					{
						Announcement announcement2 = new Announcement(this, GlobalMembers._ID("GET READY", 140));
						announcement2.mBlocksPlay = false;
						announcement2.mAlpha.mIncRate *= GlobalMembers.M(3.0);
						announcement2.mScale.mIncRate *= GlobalMembers.M(3.0);
						announcement2.mDarkenBoard = false;
					}
					GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_GETREADY);
					if (!GlobalMembers.gApp.mMainMenu.mIsFullGame() && !this.mTrialPromptShown)
					{
						this.DoPrompt();
					}
				}
				if (((this.mTimerInflate == 0.0 && (!this.mWantTimeAnnouncement || this.mTimeAnnouncementDone)) || this.GetTimeLimit() == 0 || this.mGoDelayCount > 1) && this.mGoDelayCount >= 0 && --this.mGoDelayCount == 0)
				{
					Announcement announcement3 = new Announcement(this, GlobalMembers._ID("GO!", 141));
					announcement3.mBlocksPlay = false;
					announcement3.mAlpha.mIncRate *= GlobalMembers.M(3.0);
					announcement3.mScale.mIncRate *= GlobalMembers.M(3.0);
					announcement3.mDarkenBoard = false;
					announcement3.mGoAnnouncement = true;
					GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_GO);
					if (!GlobalMembers.gApp.mMainMenu.mIsFullGame() && !this.mTrialPromptShown)
					{
						this.DoPrompt();
					}
				}
			}
			if (this.mCountdownBarPIEffect != null || this.mLevelCompleteCount != 0)
			{
				this.mCountdownBarPIEffect.Update();
			}
			if (!this.IsGameSuspended() || this.mLevelCompleteCount != 0)
			{
				if (this.mLevelBarPIEffect != null)
				{
					this.mLevelBarPIEffect.Update();
				}
				if (this.mSpeedBonusNum > 0.0 && this.mSpeedFirePIEffect != null)
				{
					this.mSpeedFirePIEffect.Update();
				}
				if (!this.mRewinding)
				{
					for (int k = 0; k < 2; k++)
					{
						if (this.mSpeedFireBarPIEffect[k] != null)
						{
							this.mSpeedFireBarPIEffect[k].Update();
							if (!this.mSpeedFireBarPIEffect[k].IsActive())
							{
								if (this.mSpeedFireBarPIEffect[k] != null)
								{
									this.mSpeedFireBarPIEffect[k].Dispose();
								}
								this.mSpeedFireBarPIEffect[k] = null;
							}
						}
					}
				}
			}
			if (this.SupportsReplays())
			{
				this.UpdateReplayPopup();
				this.UpdateReplay();
			}
			else if (this.mInReplay || (this.WantsTutorialReplays() && (this.mTutorialFlags & 524288) == 0))
			{
				this.UpdateReplay();
			}
			if (this.mRewinding)
			{
				return;
			}
			if (this.mMouseDown)
			{
				this.MouseDrag(this.mWidgetManager.mLastMouseX, this.mWidgetManager.mLastMouseY);
			}
			if (this.mWantReplaySave && this.IsBoardStill())
			{
				this.DoReplaySetup();
			}
			for (int l = 0; l < this.mQueuedMoveVector.Count; l++)
			{
				QueuedMove queuedMove = this.mQueuedMoveVector[l];
				if (this.mUpdateCnt >= queuedMove.mUpdateCnt)
				{
					if (queuedMove.mUpdateCnt == this.mUpdateCnt)
					{
						Piece pieceById = this.GetPieceById(queuedMove.mSelectedId);
						if (pieceById != null)
						{
							this.TrySwap(pieceById, queuedMove.mSwappedRow, queuedMove.mSwappedCol, queuedMove.mForceSwap, queuedMove.mPlayerSwapped);
						}
						else
						{
							this.mHadReplayError = true;
						}
					}
					int num = this.mUpdateCnt - 1;
					if (this.mReplayStartMove.mPartOfReplay && !this.mWantLevelup && !this.mInReplay)
					{
						num = this.mReplayStartMove.mUpdateCnt;
					}
					if (queuedMove.mUpdateCnt <= num && this.IsBoardStill())
					{
						this.mQueuedMoveVector.RemoveAt(l);
						l--;
					}
				}
			}
			Piece[,] array2 = this.mBoard;
			int upperBound3 = array2.GetUpperBound(0);
			int upperBound4 = array2.GetUpperBound(1);
			for (int m = array2.GetLowerBound(0); m <= upperBound3; m++)
			{
				for (int n = array2.GetLowerBound(1); n <= upperBound4; n++)
				{
					Piece piece2 = array2[m, n];
					if (piece2 != null)
					{
						piece2.mDestPct.IncInVal();
						piece2.mAlpha.IncInVal();
						piece2.mHintAlpha.IncInVal();
						piece2.mHintArrowPos.IncInVal();
					}
				}
			}
			if (this.mCoinCatcherAppearing)
			{
				this.mCoinCatcherPctPct = Math.Min(4.0, this.mCoinCatcherPctPct + 0.028);
			}
			else if (!this.mTimeExpired || this.mPointMultiplier == 0)
			{
				this.mCoinCatcherPctPct = Math.Max(0.0, this.mCoinCatcherPctPct - 0.028);
			}
			this.mCoinCatcherPct.SetInVal(Math.Min(1.0, this.mCoinCatcherPctPct));
			if (this.UpdateBulging())
			{
				Piece[,] array3 = this.mBoard;
				int upperBound5 = array3.GetUpperBound(0);
				int upperBound6 = array3.GetUpperBound(1);
				for (int num2 = array3.GetLowerBound(0); num2 <= upperBound5; num2++)
				{
					for (int num3 = array3.GetLowerBound(1); num3 <= upperBound6; num3++)
					{
						Piece piece3 = array3[num2, num3];
						if (piece3 != null)
						{
							piece3.mSelectorAlpha.IncInVal();
							if (piece3.mRotPct != 0f || piece3.mSelected)
							{
								piece3.mRotPct += GlobalMembers.M(0.02f);
								if (piece3.mRotPct >= 1f)
								{
									piece3.mRotPct = 0f;
								}
							}
						}
					}
				}
				return;
			}
			this.UpdatePoints();
			base.Update();
			this.UpdateGame();
			if (this.SupportsReplays())
			{
				this.UpdateReplayPopup();
			}
			if (this.mInReplay)
			{
				if (this.mStateInfoVector.Count > 0)
				{
					StateInfo stateInfo = this.mStateInfoVector[0];
					if (stateInfo.mUpdateCnt == this.mUpdateCnt)
					{
						if (stateInfo.mPoints != this.mPoints || stateInfo.mMoneyDisp != GlobalMembers.gApp.GetCoinCount() || stateInfo.mNextPieceId != this.mNextPieceId || stateInfo.mIdleTicks != this.mIdleTicks)
						{
							this.mHadReplayError = true;
						}
						this.mStateInfoVector.RemoveAt(0);
					}
				}
			}
			else if (this.mUpdateCnt % 100 == 0 && this.WantsWholeGameReplay())
			{
				StateInfo stateInfo2 = new StateInfo();
				stateInfo2.mUpdateCnt = this.mUpdateCnt;
				stateInfo2.mPoints = this.mPoints;
				stateInfo2.mMoneyDisp = GlobalMembers.gApp.GetCoinCount();
				stateInfo2.mNextPieceId = this.mNextPieceId;
				stateInfo2.mIdleTicks = this.mIdleTicks;
				this.mWholeGameReplay.mStateInfoVector.Add(stateInfo2);
			}
			if (this.mIsWholeGameReplay && GlobalMembers.gApp.GetDialog(18) != null)
			{
				GlobalMembers.gApp.KillDialog(18);
				this.mDeferredTutorialVector.RemoveAt(0);
				this.mTutorialPieceIrisPct.SetConstant(0.0);
			}
			this.mBadgeManager.Update();
		}

		public override void Update()
		{
			if (!this.mContentLoaded || this.mSuspendingGame)
			{
				return;
			}
			if (this.mCurrentHint == null)
			{
				if (this.mAchievementHints.Count > 0)
				{
					this.mCurrentHint = this.mAchievementHints[0];
				}
			}
			else if (!this.mInReplay)
			{
				this.mCurrentHint.Update();
			}
			if (!this.AllowUI())
			{
				this.DisableUI(true);
			}
			if (this.mMessager != null)
			{
				this.mMessager.Update();
			}
			if (this.mRestartPrevImage != null)
			{
				if (!this.mRestartPct.IncInVal())
				{
					this.mRestartPrevImage = null;
				}
				for (int i = 0; i < GlobalMembers.M(3); i++)
				{
					Effect effect = this.mPostFXManager.AllocEffect(Effect.Type.TYPE_NONE);
					effect.mImage = GlobalMembersResourcesWP.IMAGE_VERTICAL_STREAK;
					effect.mScale = GlobalMembers.M(1f) + GlobalMembersUtils.GetRandFloat() * GlobalMembers.M(0.2f);
					effect.mDX = (float)GlobalMembers.M(0);
					effect.mDY = (float)GlobalMembers.M(6);
					effect.mX = -160f + GlobalMembersUtils.GetRandFloat() * 1920f;
					effect.mY = (float)(this.mRestartPct * 1200.0);
					effect.mIsAdditive = true;
					effect.mAlpha = 1f;
					effect.mDAlpha = GlobalMembers.M(-0.03f);
					effect.mGravity = 0f;
					this.mPostFXManager.AddEffect(effect);
				}
				return;
			}
			if (this.mUpdateCnt == 0 && !this.mInReplay && this.WantsWholeGameReplay())
			{
				this.SaveGame(this.mWholeGameReplay.mSaveBuffer);
			}
			float num = 1f;
			float num2 = GlobalMembers.M(0.95f);
			if (this.mRewinding)
			{
				Board.Update_aSpeed = 1f;
			}
			else if (this.mInReplay && !this.mIsWholeGameReplay)
			{
				int num3 = this.mUpdateCnt - this.mPlaybackTimestamp;
				if (num3 < 10)
				{
					num = GlobalMembers.M(0.3f);
					Board.Update_aSpeed = num;
				}
				else
				{
					num = GlobalMembers.M(0.85f);
				}
				if (this.mIsOneMoveReplay && num3 < GlobalMembers.M(3))
				{
					num = GlobalMembers.M(0.03f);
					Board.Update_aSpeed = num;
				}
				num2 = GlobalMembers.M(0.98f);
				if (this.mSwapDataVector.Count > 0)
				{
					num = GlobalMembers.M(0.4f);
				}
			}
			Board.Update_aSpeed = (float)((double)(num2 * Board.Update_aSpeed) + (1.0 - (double)num2) * (double)num);
			if (Math.Abs(Board.Update_aSpeed - num) < 0.01f)
			{
				Board.Update_aSpeed = num;
			}
			Board.Update_aSpeed *= this.GetGameSpeed();
			this.mUpdateAcc += Board.Update_aSpeed;
			int num4 = (int)this.mUpdateAcc;
			this.mUpdateAcc -= (float)num4;
			for (int j = 0; j < num4; j++)
			{
				this.DoUpdate();
			}
			float num5 = (float)this.mAlphaCurve;
			this.mPreFXManager.mAlpha = this.GetPieceAlpha() * num5;
			this.mPostFXManager.mAlpha = this.GetPieceAlpha() * num5;
			if (GlobalMembers.gApp.GetDialog(23) == null && this.mQuestPortalPct.IsDoingCurve() && !this.mQuestPortalPct.IncInVal())
			{
				this.mQuestPortalPct.SetConstant(0.0);
				this.mQuestPortalCenterPct.SetConstant(0.0);
				this.mNeedsMaskCleared = true;
				if (this.mGameOverCount > 0)
				{
					this.mGameOverCount = GlobalMembers.M(400);
				}
				this.DoUpdate();
			}
			if (this.mHintButton != null && this.mHintCooldownTicks == 0)
			{
				this.mHintButton.mMouseVisible = (double)this.GetAlpha() * this.mSideAlpha == 1.0;
			}
			if (this.mResetButton != null)
			{
				this.mResetButton.mMouseVisible = (double)this.GetAlpha() * this.mSideAlpha == 1.0;
			}
			if (this.mBackground != null)
			{
				this.mBackground.mWantAnim = this.mHyperspace == null && (double)this.GetAlpha() == 1.0 && this.mScale == 1.0 && this.mHasBoardSettled && !this.mInReplay && !this.mSideXOff.IsDoingCurve() && GlobalMembers.gApp.mDialogList.Count == 0;
			}
			if (this.mAlpha == 0.0 && this.mKilling && GlobalMembers.gApp.mBoard == this)
			{
				GlobalMembers.KILL_WIDGET(GlobalMembers.gApp.mBoard);
				GlobalMembers.gApp.mBoard = null;
			}
		}

		public bool PieceNeedsEffect(Piece thePiece)
		{
			return thePiece.mColor >= 0 && ((thePiece.IsFlagSet(65536U) && thePiece.mColor >= 0) || !thePiece.IsFlagSet(482U));
		}

		public void DrawGemLighting(Graphics g, Piece thePiece, float theX, float theY, float theZ, float theBrightness, float theFalloffFactor, float theFalloffBias)
		{
			GlobalMembers.gFrameLightCount++;
			int theCel = (int)Math.Min(thePiece.mRotPct * (float)GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount(), (float)(GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount() - 1));
			DeviceImage deviceImage = (DeviceImage)GlobalMembersResourcesWP.GetImageById(862 + thePiece.mColor);
			deviceImage.GetCelRect(theCel);
			int num = (int)GlobalMembers.S(thePiece.GetScreenX());
			int num2 = (int)GlobalMembers.S(thePiece.GetScreenY());
			float theX2 = GlobalMembers.S(theX) - (float)num;
			float theY2 = GlobalMembers.S(theY) - (float)num2;
			SexyVector3 sexyVector = new SexyVector3(theX2, theY2, theZ);
			sexyVector = sexyVector.Normalize();
			float[] array = new float[] { sexyVector.x, sexyVector.y, sexyVector.z, theBrightness };
			Color color = new Color((int)((array[0] + 1f) * 0.5f * 255f), (int)((array[1] + 1f) * 0.5f * 255f), (int)((array[2] + 1f) * 0.5f * 255f), (int)(array[3] * 255f));
			g.SetColor(color);
			g.DrawImageCel(deviceImage, num, num2, theCel);
		}

		public void DrawGemEffectsLighting(Graphics g, bool thePostFX, uint gemMask)
		{
			int[] array = new int[] { 17, 21 };
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (this.mPostFXManager.mEffectList[array[i]].Count > 0)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			Graphics3D graphics3D = g.Get3D();
			if (this.GetPieceAlpha() < 1f || this.mInReplay || graphics3D == null || !graphics3D.SupportsPixelShaders())
			{
				return;
			}
			g.SetColorizeImages(true);
			g.SetDrawMode(Graphics.DrawMode.Additive);
			RenderEffect effect = graphics3D.GetEffect(GlobalMembersResourcesWP.EFFECT_GEM_LIGHT);
			using (RenderEffectAutoState renderEffectAutoState = new RenderEffectAutoState(g, effect))
			{
				while (!renderEffectAutoState.IsDone())
				{
					for (int j = 0; j < array.Length; j++)
					{
						foreach (Effect effect2 in this.mPostFXManager.mEffectList[array[j]])
						{
							if (effect2.mLightSize > 0f)
							{
								Piece[,] array2 = this.mBoard;
								int upperBound = array2.GetUpperBound(0);
								int upperBound2 = array2.GetUpperBound(1);
								for (int k = array2.GetLowerBound(0); k <= upperBound; k++)
								{
									for (int l = array2.GetLowerBound(1); l <= upperBound2; l++)
									{
										Piece piece = array2[k, l];
										if (piece != null && !this.IsPieceSwapping(piece, false, false) && piece != this.mGameOverPiece && (!thePostFX || piece.mElectrocutePercent > 0f || ((ulong)gemMask & (ulong)(1L << ((piece.mColor + 1) & 31))) != 0UL) && (!thePostFX || !piece.IsFlagSet(6144U)))
										{
											float num = piece.CX() - effect2.mX;
											float num2 = piece.CY() - effect2.mY;
											if ((ulong)effect2.mPieceId != (ulong)((long)piece.mId) && num * num + num2 * num2 <= effect2.mLightSize * effect2.mLightSize)
											{
												this.DrawGemLighting(g, piece, effect2.mX, effect2.mY, effect2.mZ, effect2.mLightIntensity, effect2.mValue[0], effect2.mValue[1]);
											}
										}
									}
								}
							}
						}
					}
					renderEffectAutoState.NextPass();
				}
			}
			g.SetColorizeImages(false);
			g.SetDrawMode(Graphics.DrawMode.Normal);
		}

		public void DrawGemLightningLighting(Graphics g, Piece thePiece, LightningStorm anInfo)
		{
			if (!this.PieceNeedsEffect(thePiece))
			{
				return;
			}
			int theX = (int)GlobalMembers.S(thePiece.GetScreenX());
			int theY = (int)GlobalMembers.S(thePiece.GetScreenY());
			for (int i = 0; i < 2; i++)
			{
				int num = this.GetRowAt(anInfo.mCY) - thePiece.mRow;
				int num2 = this.GetColAt(anInfo.mCX) - thePiece.mCol;
				if ((i == 0 && Math.Abs(num) == 1 && (anInfo.mStormType == 0 || anInfo.mStormType == 2)) || (i == 1 && Math.Abs(num2) == 1 && (anInfo.mStormType == 1 || anInfo.mStormType == 2)))
				{
					GlobalMembers.gFrameLightCount++;
					int num3 = GlobalMembers.M(-50);
					if (i == 0)
					{
						num3 *= num;
					}
					else
					{
						num3 *= num2;
					}
					int theCel = (int)Math.Min(thePiece.mRotPct * (float)GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount(), (float)(GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount() - 1));
					DeviceImage deviceImage = (DeviceImage)GlobalMembersResourcesWP.GetImageById(862 + thePiece.mColor);
					Rect celRect = deviceImage.GetCelRect(theCel);
					float num4 = (float)(celRect.mX - num3 + 50) / (float)deviceImage.mWidth;
					float num5 = (float)(celRect.mY - num3 + 50) / (float)deviceImage.mHeight;
					float[] array = new float[4];
					array[0] = num4;
					array[1] = num5;
					float[] array2 = array;
					float[] array3 = new float[4];
					array3[2] = (float)(GlobalMembers.M(0.3) * anInfo.mLightingAlpha);
					float[] array4 = array3;
					if (i == 0)
					{
						array2[3] = 0.4f * GlobalMembers.M(0.3f);
						array4[1] = (float)((double)num * GlobalMembers.M(0.8) * anInfo.mLightingAlpha);
					}
					else
					{
						array2[2] = 0.5f * GlobalMembers.M(0.3f);
						array4[0] = (float)((double)num2 * GlobalMembers.M(0.8) * anInfo.mLightingAlpha);
					}
					Color color = new Color((int)((array4[0] + 1f) * 0.5f * 255f), (int)((array4[1] + 1f) * 0.5f * 255f), (int)((array4[2] + 1f) * 0.5f * 255f), (int)((array4[3] + 1f) * 0.5f * 255f));
					g.SetColor(color);
					g.DrawImageCel(deviceImage, theX, theY, theCel);
				}
			}
		}

		public void DrawGemSun(Graphics g, Piece thePiece, RenderEffect aEffect)
		{
			if (!this.PieceNeedsEffect(thePiece))
			{
				return;
			}
			Graphics3D graphics3D = g.Get3D();
			if (this.GetPieceAlpha() < 1f || graphics3D == null || !graphics3D.SupportsPixelShaders())
			{
				return;
			}
			int num = (int)((double)(thePiece.CX() - (float)this.GetBoardX() + (thePiece.CY() - (float)this.GetRowY(0))) * 0.707);
			int num2 = (int)((double)num - this.mSunPosition);
			if (Math.Abs(num2) > GlobalMembers.M(160))
			{
				return;
			}
			GlobalMembers.gFrameLightCount++;
			int theCel = (int)Math.Min(thePiece.mRotPct * (float)GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount(), (float)(GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount() - 1));
			DeviceImage deviceImage = (DeviceImage)GlobalMembersResourcesWP.GetImageById(862 + thePiece.mColor);
			deviceImage.GetCelRect(theCel);
			float num3 = (float)Math.Atan2((double)GlobalMembers.M(20f), (double)num2);
			float[] array = new float[4];
			array[0] = -(float)Math.Cos((double)num3) * 0.707f;
			array[1] = -(float)Math.Cos((double)num3) * 0.707f;
			array[2] = (float)Math.Sin((double)num3);
			float[] array2 = array;
			Color color = new Color((int)((array2[0] + 1f) * 0.5f * 255f), (int)((array2[1] + 1f) * 0.5f * 255f), (int)((array2[2] + 1f) * 0.5f * 255f), (int)((array2[3] + 1f) * 0.5f * 255f));
			g.SetColorizeImages(true);
			g.SetColor(color);
			g.DrawImageCel(deviceImage, (int)GlobalMembers.S(thePiece.GetScreenX()), (int)GlobalMembers.S(thePiece.GetScreenY()), theCel);
		}

		public void DrawLaser(Graphics g, Piece thePiece, bool theFront)
		{
			g.SetColor(Color.White);
			for (int i = 0; i < 4; i++)
			{
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = (float)this.mUpdateCnt * GlobalMembers.M(0.06f);
				if (i == 0)
				{
					num = (float)Math.Sin((double)num4);
					num2 = (float)Math.Sin((double)num4);
					num3 = (float)Math.Cos((double)num4);
				}
				else if (i == 1)
				{
					num = -(float)Math.Sin((double)(num4 + 0.7853982f));
					num2 = (float)Math.Sin((double)(num4 + 0.7853982f));
					num3 = (float)Math.Cos((double)(num4 + 0.7853982f));
				}
				else if (i == 2)
				{
					num = (float)Math.Sin((double)(num4 + 1.57079637f));
					num2 = -(float)Math.Sin((double)(num4 + 1.57079637f));
					num3 = (float)Math.Cos((double)(num4 + 1.57079637f));
				}
				else if (i == 3)
				{
					num = (float)Math.Sin((double)(num4 + 2.3561945f));
					num2 = (float)Math.Sin((double)(num4 + 2.3561945f));
					num3 = -(float)Math.Cos((double)(num4 + 2.3561945f));
				}
				if ((num3 < 0f) ^ theFront)
				{
					Utils.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_SPARKLET_BIG, GlobalMembers.S(thePiece.CX() + num * 100f * GlobalMembers.M(0.41f)), GlobalMembers.S(thePiece.CY() + num2 * 100f * GlobalMembers.M(0.41f)), 1f + num3 * GlobalMembers.M(0.3f), 1f + num3 * GlobalMembers.M(0.3f));
				}
				if (theFront && num3 > GlobalMembers.M(-0.5f))
				{
					this.DrawGemLighting(g, thePiece, thePiece.CX() + num * 100f * GlobalMembers.M(0.41f), thePiece.CY() + num2 * 100f * GlobalMembers.M(0.41f), num3 * GlobalMembers.M(0.07f), 1f, 20.1f, 0f);
				}
			}
		}

		public virtual void DrawHypercube(Graphics g, Piece thePiece)
		{
			int theCel = GlobalMembers.gApp.mUpdateCount / GlobalMembers.M(10) % GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME.GetCelCount();
			int num = (int)GlobalMembers.S(thePiece.GetScreenX() - 16f);
			int num2 = (int)GlobalMembers.S(thePiece.GetScreenY() - 16f);
			g.SetColor(Color.White);
			g.mColor.mAlpha = (int)(this.GetPieceAlpha() * 255f);
			GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME))) + num, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME))) + num2, theCel);
			g.SetDrawMode(Graphics.DrawMode.Additive);
			GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_HYPERCUBE_COLORGLOW, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_HYPERCUBE_COLORGLOW))) + num, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_HYPERCUBE_COLORGLOW))) + num2, theCel);
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.SetColor(Color.White);
		}

		public void DrawBombGem(Graphics g, Piece thePiece)
		{
		}

		public void DrawDoomGem(Graphics g, Piece thePiece)
		{
		}

		public virtual void DrawPieceShadow(Graphics g, Piece thePiece)
		{
			if (thePiece.IsFlagSet(128U))
			{
				return;
			}
			float num = (float)thePiece.mScale;
			int num2 = (int)thePiece.GetScreenX();
			int num3 = (int)thePiece.GetScreenY();
			int theNum = (int)((float)num2 + thePiece.mShakeOfsX);
			int theNum2 = (int)((float)num3 + thePiece.mShakeOfsY);
			if (num != 1f)
			{
				g.SetScale(num, num, (float)GlobalMembers.S(num2 + 50), (float)GlobalMembers.S(num3 + 50));
			}
			bool colorizeImages = g.GetColorizeImages();
			g.SetColorizeImages(true);
			float num4;
			if (this.mHyperspace != null && thePiece.mColor >= 0)
			{
				num4 = (float)(thePiece.mAlpha * (double)this.mHyperspace.GetPieceAlpha());
			}
			else
			{
				num4 = (float)(thePiece.mAlpha * (double)this.GetPieceAlpha());
			}
			g.SetColor(new Color(255, 255, 255, (int)(255f * num4)));
			if (thePiece.IsFlagSet(16U))
			{
				int theCel = (int)Math.Min(thePiece.mRotPct * (float)GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount(), (float)(GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount() - 1));
				GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.GetImageById(809 + thePiece.mColor), GlobalMembers.S(theNum), GlobalMembers.S(theNum2), theCel);
			}
			else if (thePiece.mColor <= -1 || thePiece.mColor >= 7)
			{
				thePiece.IsFlagSet(4U);
			}
			g.SetColorizeImages(colorizeImages);
			if (num != 1f)
			{
				g.SetScale(1f, 1f, (float)GlobalMembers.S(num2 + 50), (float)GlobalMembers.S(num3 + 50));
			}
		}

		public virtual void DrawPiece(Graphics g, Piece thePiece)
		{
			this.DrawPiece(g, thePiece, 1f);
		}

		public virtual void DrawPiece(Graphics g, Piece thePiece, float theScale)
		{
			Graphics3D graphics3D = g.Get3D();
			float num = (float)thePiece.mScale;
			float num2;
			if (this.mHyperspace != null && thePiece.mColor >= 0)
			{
				num2 = (float)(thePiece.mAlpha * (double)this.mHyperspace.GetPieceAlpha());
			}
			else
			{
				num2 = (float)(thePiece.mAlpha * (double)this.GetPieceAlpha());
			}
			if (num2 == 0f)
			{
				return;
			}
			num *= theScale;
			int num3 = (int)thePiece.GetScreenX();
			int num4 = (int)thePiece.GetScreenY();
			int num5 = (int)((float)num3 + thePiece.mShakeOfsX);
			int num6 = (int)((float)num4 + thePiece.mShakeOfsY);
			bool flag = false;
			if (num != 1f)
			{
				g.SetScale(num, num, (float)GlobalMembers.S(num3 + 50), (float)GlobalMembers.S(num4 + 50));
			}
			if (this.mShowMoveCredit)
			{
				g.SetColor(Color.White);
				g.SetFont(GlobalMembersResources.FONT_DIALOG);
				if (thePiece.mMoveCreditId != -1)
				{
					g.DrawString(string.Format("{0}", thePiece.mMoveCreditId), GlobalMembers.S(num3 + GlobalMembers.M(10)), GlobalMembers.S(num4 + GlobalMembers.M(20)));
				}
				if (thePiece.mCounter != 0)
				{
					g.DrawString(string.Format("{0}", thePiece.mCounter), GlobalMembers.S(num3 + GlobalMembers.M(80)), GlobalMembers.S(num4 + GlobalMembers.M(20)));
				}
			}
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255f * num2)));
			if (thePiece.IsFlagSet(2U))
			{
				this.DrawHypercube(g, thePiece);
			}
			else if (thePiece.IsFlagSet(96U))
			{
				this.DrawBombGem(g, thePiece);
			}
			else if (thePiece.IsFlagSet(256U))
			{
				this.DrawDoomGem(g, thePiece);
			}
			else if (!thePiece.IsFlagSet(128U))
			{
				flag = true;
			}
			if (flag && thePiece.mColor >= 0)
			{
				if (this.CanBakeShadow(thePiece))
				{
					if (graphics3D == null)
					{
						float num7 = num;
						if (num7 > 1f)
						{
							num7 = (num7 - 1f) * 2f + 1f;
						}
						int num8 = (int)((-num7 + 2f) * 16f / 2f - 1f);
						num8 = Math.Max(0, Math.Min(num8, 14));
						Image image = GlobalMembers.gApp.mShrunkenGems[thePiece.mColor, num8];
						g.SetScale(1f, 1f, 0f, 0f);
						GlobalMembers.gGR.DrawImage(g, image, GlobalMembers.S(num5) - (image.mWidth - GlobalMembers.S(100)) / 2, GlobalMembers.S(num6) - (image.mHeight - GlobalMembers.S(100)) / 2);
					}
					else
					{
						GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_GEMS_SHADOWED, GlobalMembers.S(num5), GlobalMembers.S(num6), thePiece.mColor);
					}
				}
				else
				{
					new Color(255, 255, 255);
					new Color(192, 192, 192);
					new Color(32, 192, 32);
					new Color(224, 192, 32);
					new Color(255, 255, 255);
					new Color(255, 160, 32);
					new Color(255, 255, 255);
					Image imageById = GlobalMembersResourcesWP.GetImageById(802 + thePiece.mColor);
					float num9 = thePiece.mRotPct * (float)imageById.GetCelCount();
					Rect celRect = imageById.GetCelRect((int)num9);
					Rect celRect2 = imageById.GetCelRect(((int)num9 + 1) % imageById.GetCelCount());
					float[] array = new float[4];
					array[0] = (float)(celRect2.mX - celRect.mX) / (float)imageById.mWidth;
					array[1] = (float)(celRect2.mY - celRect.mY) / (float)imageById.mHeight;
					array[2] = num9 - (float)((int)num9);
					float[] array2 = array;
					if (imageById.mAtlasImage != null)
					{
						array2[0] = array2[0] / (float)imageById.mAtlasImage.mWidth;
						array2[1] = array2[1] / (float)imageById.mAtlasImage.mHeight;
					}
					GlobalMembers.gGR.DrawImageCel(g, imageById, GlobalMembers.S(num5), GlobalMembers.S(num6), (int)num9);
				}
				g.SetColorizeImages(false);
			}
			if (thePiece.IsFlagSet(8192U))
			{
				g.PushState();
				g.SetColorizeImages(true);
				g.SetColor(Color.FAlpha((float)((GlobalMembers.M(0.25) * this.mBoostShowPct + (double)GlobalMembers.M(0.75f)) * (double)this.GetPieceAlpha())));
				float num10 = (float)(GlobalMembers.M(1.0) + this.mBoostShowPct * (double)GlobalMembers.MS(0.25f));
				Utils.PushScale(g, num10, num10, GlobalMembers.S(thePiece.CX()), GlobalMembers.S(thePiece.CY()));
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_GREENQUESTION, GlobalMembers.S(num5) + GlobalMembers.MS(34), GlobalMembers.S(num6) + GlobalMembers.MS(17));
				Utils.PopScale(g);
				g.PopState();
			}
			if (thePiece.IsFlagSet(512U) && !thePiece.IsShrinking() && thePiece.mOverlay != null && (thePiece.mSpinFrame < 5f || thePiece.mSpinFrame > 15f))
			{
				float num11 = thePiece.mSpinFrame * 3.14159274f * 2f / 20f;
				float num12 = (float)(thePiece.mScale * (double)GlobalMembers.M(0.8f) * (1.0 + thePiece.mOverlayBulge));
				int num13 = (int)((float)thePiece.mOverlay.mWidth * num12);
				num13 = (int)((float)num13 * (float)Math.Cos((double)num11));
				int num14 = (int)((float)thePiece.mOverlay.mHeight * num12);
				g.SetDrawMode(Graphics.DrawMode.Normal);
				g.SetColorizeImages(true);
				int num15 = (int)((double)(GlobalMembers.S(num5 + 50) - num13 / 2) + thePiece.mScale * (double)GlobalMembers.MS(0));
				int theY = (int)((float)GlobalMembers.S(num6 + 50) - (float)num14 * GlobalMembers.M(0.65f));
				int num16 = (int)((double)GlobalMembers.M(255) * thePiece.mAlpha);
				g.SetColor(Color.White);
				num15 += (int)((float)Math.Sin((double)num11) * 100f / 2f);
				int num17 = (int)((double)((float)num16) * thePiece.mOverlayCurve);
				Image image_BOMBGLOWS_DANGERGLOW = GlobalMembersResourcesWP.IMAGE_BOMBGLOWS_DANGERGLOW;
				int num18 = (int)((float)image_BOMBGLOWS_DANGERGLOW.GetCelWidth() * num12 * 2f);
				int num19 = (int)((float)image_BOMBGLOWS_DANGERGLOW.GetCelHeight() * num12 * 2f);
				int theX = GlobalMembers.S(num3 + 50) - num18 / 2;
				int theY2 = GlobalMembers.S(num4 + 50) - num19 / 2;
				g.SetColor(new Color(255, 255, 255, (int)((double)((float)num17 * GlobalMembers.M(1f)) * thePiece.mAlpha)));
				g.DrawImageCel(image_BOMBGLOWS_DANGERGLOW, new Rect(theX, theY2, num18, num19), thePiece.mColor);
				g.SetColor(new Color(255, 255, 255, (int)((double)num17 * thePiece.mAlpha)));
				g.DrawImage(thePiece.mOverlayGlow, num15, theY, num13, num14);
				num16 = 255 - num17;
				g.SetColor(new Color(255, 255, 255, (int)((double)num16 * thePiece.mAlpha)));
				g.DrawImage(thePiece.mOverlay, num15, theY, num13, num14);
			}
			if (thePiece.mHidePct > 0f)
			{
				float num20 = 0.15f + thePiece.mHidePct * 0.85f;
				g.SetColor(new Color(128, 128, 128, (int)(num20 * 255f)));
				g.FillRect(GlobalMembers.S(num3) + 1, GlobalMembers.S(num4) + 1, GlobalMembers.S(100) - 2, GlobalMembers.S(100) - 2);
			}
			if (thePiece.mSelectorAlpha != 0.0)
			{
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255.0 * thePiece.mSelectorAlpha * (double)this.GetPieceAlpha())));
				GlobalMembers.gGR.DrawImage(g, GlobalMembersResourcesWP.IMAGE_SELECTOR, GlobalMembers.S(num3), GlobalMembers.S(num4));
			}
			if (num != 1f)
			{
				g.SetScale(1f, 1f, (float)GlobalMembers.S(num3 + 50), (float)GlobalMembers.S(num4 + 50));
			}
		}

		public void DrawStandardPieces(Graphics g, Piece[] pPieces, int numPieces)
		{
			this.DrawStandardPieces(g, pPieces, numPieces, 1f);
		}

		public void DrawStandardPieces(Graphics g, Piece[] pPieces, int numPieces, float theScale)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			Piece piece = null;
			Graphics3D graphics3D = g.Get3D();
			for (int i = 0; i < numPieces; i++)
			{
				Piece piece2;
				if ((piece2 = pPieces[i]) != null)
				{
					float num4;
					if (this.mHyperspace != null && piece2.mColor >= 0)
					{
						num4 = (float)(piece2.mAlpha * (double)this.mHyperspace.GetPieceAlpha());
					}
					else
					{
						num4 = (float)(piece2.mAlpha * (double)this.GetPieceAlpha());
					}
					if (num4 >= 0.01f)
					{
						float num5 = (float)(piece2.mScale * (double)theScale);
						int num6 = (int)(piece2.GetScreenX() + piece2.mShakeOfsX);
						int num7 = (int)(piece2.GetScreenY() + piece2.mShakeOfsY);
						if (piece2.IsFlagSet(2U))
						{
							this.DSP_pHyperCubes[num2].piece = piece2;
							this.DSP_pHyperCubes[num2].alpha = num4;
							this.DSP_pHyperCubes[num2].scale = num5;
							this.DSP_pHyperCubes[num2].offsX = num6;
							this.DSP_pHyperCubes[num2].offsY = num7;
							num2++;
						}
						else if (piece2.IsFlagSet(128U))
						{
							this.DSP_pButterflies[num3].piece = piece2;
							this.DSP_pButterflies[num3].alpha = num4;
							this.DSP_pButterflies[num3].scale = num5;
							this.DSP_pButterflies[num3].offsX = num6;
							this.DSP_pButterflies[num3].offsY = num7;
							num3++;
						}
						else if (piece2.mColor >= 0)
						{
							this.DSP_pNormalPieces[num].piece = piece2;
							this.DSP_pNormalPieces[num].alpha = num4;
							this.DSP_pNormalPieces[num].scale = num5;
							this.DSP_pNormalPieces[num].offsX = num6;
							this.DSP_pNormalPieces[num].offsY = num7;
							num++;
						}
						if (piece2.mSelectorAlpha != 0.0)
						{
							piece = piece2;
						}
					}
				}
			}
			g.SetColorizeImages(true);
			int num8 = GlobalMembers.gApp.mUpdateCount / GlobalMembers.M(10);
			for (int i = 0; i < num; i++)
			{
				Piece piece2 = this.DSP_pNormalPieces[i].piece;
				float num5 = this.DSP_pNormalPieces[i].scale;
				float num4 = this.DSP_pNormalPieces[i].alpha;
				int num6 = this.DSP_pNormalPieces[i].offsX;
				int num7 = this.DSP_pNormalPieces[i].offsY;
				if (num5 != 1f)
				{
					g.SetScale(num5, num5, GlobalMembers.S(piece2.GetScreenX() + 50f), GlobalMembers.S(piece2.GetScreenY() + 50f));
				}
				g.SetColor(new Color(255, 255, 255, (int)(255f * num4)));
				if (this.CanBakeShadow(piece2))
				{
					if (graphics3D == null)
					{
						float num9 = num5;
						if (num9 > 1f)
						{
							num9 = (num9 - 1f) * 2f + 1f;
						}
						int num10 = (int)((-num9 + 2f) * 16f / 2f - 1f);
						num10 = Math.Max(0, Math.Min(num10, 14));
						Image image = GlobalMembers.gApp.mShrunkenGems[piece2.mColor, num10];
						g.SetScale(1f, 1f, 0f, 0f);
						GlobalMembers.gGR.DrawImage(g, image, GlobalMembers.S(num6) - (image.mWidth - GlobalMembers.S(100)) / 2, GlobalMembers.S(num7) - (image.mHeight - GlobalMembers.S(100)) / 2);
					}
					else
					{
						GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_GEMS_SHADOWED, GlobalMembers.S(num6), GlobalMembers.S(num7), piece2.mColor);
					}
				}
				else
				{
					Image imageById = GlobalMembersResourcesWP.GetImageById(802 + piece2.mColor);
					float num11 = piece2.mRotPct * (float)imageById.GetCelCount();
					GlobalMembers.gGR.DrawImageCel(g, imageById, GlobalMembers.S(num6), GlobalMembers.S(num7), (int)num11);
				}
				if (num5 != 1f)
				{
					g.SetScale(1f, 1f, GlobalMembers.S(piece2.GetScreenX() + 50f), GlobalMembers.S(piece2.GetScreenY() + 50f));
				}
			}
			if (num2 > 0)
			{
				g.SetColor(Color.White);
				g.mColor.mAlpha = (int)(this.GetPieceAlpha() * 255f);
				for (int i = 0; i < num2; i++)
				{
					Piece piece2 = this.DSP_pHyperCubes[i].piece;
					int theCel = (piece2.mId + num8) % GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME.GetCelCount();
					GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_HYPERCUBE_FRAME_ID)) + GlobalMembers.S(piece2.GetScreenX() - 16f)), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_HYPERCUBE_FRAME_ID)) + GlobalMembers.S(piece2.GetScreenY() - 16f)), theCel);
				}
				g.SetColor(Color.White);
			}
			if (num2 > 0)
			{
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.SetColor(Color.White);
				g.mColor.mAlpha = (int)(this.GetPieceAlpha() * 255f);
				for (int i = 0; i < num2; i++)
				{
					Piece piece2 = this.DSP_pHyperCubes[i].piece;
					int theCel2 = (piece2.mId + num8) % GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME.GetCelCount();
					GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.IMAGE_HYPERCUBE_COLORGLOW, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_HYPERCUBE_COLORGLOW_ID)) + GlobalMembers.S(piece2.GetScreenX() - 16f)), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_HYPERCUBE_COLORGLOW_ID)) + GlobalMembers.S(piece2.GetScreenY() - 16f)), theCel2);
				}
				g.SetDrawMode(Graphics.DrawMode.Normal);
				g.SetColor(Color.White);
			}
			if (num3 > 0)
			{
				g.SetColor(Color.White);
				for (int j = 0; j < num3; j++)
				{
					this.DrawButterfly(g, this.DSP_pButterflies[j].offsX, this.DSP_pButterflies[j].offsY, this.DSP_pButterflies[j].piece, this.DSP_pButterflies[j].scale);
				}
			}
			if (piece != null)
			{
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255.0 * piece.mSelectorAlpha * (double)this.GetPieceAlpha())));
				GlobalMembers.gGR.DrawImage(g, GlobalMembersResourcesWP.IMAGE_SELECTOR, (int)GlobalMembers.S(piece.GetScreenX()), (int)GlobalMembers.S(piece.GetScreenY()));
			}
			g.SetColorizeImages(false);
		}

		public void DrawShadowPieces(Graphics g, Piece[] pPieces, int numPieces)
		{
			this.DrawShadowPieces(g, pPieces, numPieces, 1f);
		}

		public void DrawShadowPieces(Graphics g, Piece[] pPieces, int numPieces, float theScale)
		{
			if (pPieces == null || numPieces == 0)
			{
				return;
			}
			bool colorizeImages = g.GetColorizeImages();
			g.SetColorizeImages(true);
			for (int i = 0; i < numPieces; i++)
			{
				Piece piece;
				if ((piece = pPieces[i]) != null)
				{
					int theNum = (int)(piece.GetScreenX() + piece.mShakeOfsX);
					int theNum2 = (int)(piece.GetScreenY() + piece.mShakeOfsY);
					float num = (float)piece.mScale;
					if (num != 1f)
					{
						g.SetScale(num, num, GlobalMembers.S(piece.GetScreenX() + 50f), GlobalMembers.S(piece.GetScreenY() + 50f));
					}
					float num2;
					if (this.mHyperspace != null && piece.mColor >= 0)
					{
						num2 = (float)(piece.mAlpha * (double)this.mHyperspace.GetPieceAlpha());
					}
					else
					{
						num2 = (float)(piece.mAlpha * (double)this.GetPieceAlpha());
					}
					g.SetColor(new Color(255, 255, 255, (int)(255f * num2)));
					if (!piece.IsFlagSet(128U) && ((piece.mColor > -1 && piece.mColor < 7) || piece.IsFlagSet(4U)))
					{
						int theCel = (int)Math.Min(piece.mRotPct * (float)GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount(), (float)(GlobalMembersResourcesWP.IMAGE_GEMS_RED.GetCelCount() - 1));
						GlobalMembers.gGR.DrawImageCel(g, GlobalMembersResourcesWP.GetImageById(809 + piece.mColor), GlobalMembers.S(theNum), GlobalMembers.S(theNum2), theCel);
					}
					if (num != 1f)
					{
						g.SetScale(1f, 1f, GlobalMembers.S(piece.GetScreenX() + 50f), GlobalMembers.S(piece.GetScreenY() + 50f));
					}
				}
			}
			g.SetColorizeImages(colorizeImages);
		}

		public virtual void DrawPieceText(Graphics g, Piece thePiece)
		{
			this.DrawPieceText(g, thePiece, 1f);
		}

		public virtual void DrawPieceText(Graphics g, Piece thePiece, float theScale)
		{
		}

		public virtual void DrawFrame(Graphics g)
		{
			this.DrawTopFrame(g);
			this.DrawBottomFrame(g);
		}

		public virtual void DrawTopFrame(Graphics g)
		{
			if (this.WantTopFrame())
			{
				if (this.WantCountdownBar() || this.WantTopLevelBar())
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_FRAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_PROGRESS_BAR_FRAME_ID) + (float)this.mTransBoardOffsetX), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_PROGRESS_BAR_FRAME_ID) - (float)this.mTransBoardOffsetY));
					return;
				}
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME))) + (float)this.mTransBoardOffsetX), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME))) - (float)this.mTransBoardOffsetY));
			}
		}

		public virtual void DrawBottomFrame(Graphics g)
		{
			if (this.WantBottomFrame())
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME_ID)) + (float)this.mTransBoardOffsetX), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME_ID)) - (float)this.mTransBoardOffsetY));
			}
		}

		public virtual void DrawLevelBar(Graphics g)
		{
			g.PushState();
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.SetColorizeImages(true);
			float num = (float)Math.Pow((double)this.GetBoardAlpha(), 4.0);
			g.SetColor(new Color(255, 255, 255, (int)(this.GetBoardAlpha() * (float)GlobalMembers.M(255))));
			Image theImage = null;
			int num2 = 0;
			int num3 = 0;
			Rect levelBarRect = this.GetLevelBarRect();
			levelBarRect.mX += GlobalMembers.S(this.mTransBoardOffsetX);
			levelBarRect.mY -= GlobalMembers.S(this.mTransBoardOffsetY);
			if (this.WantTopLevelBar())
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_BACK, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_INGAMEUI_PROGRESS_BAR_BACK_ID) + (float)this.mTransBoardOffsetX), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_INGAMEUI_PROGRESS_BAR_BACK_ID) - (float)this.mTransBoardOffsetY));
			}
			g.SetColor(new Color(GlobalMembers.M(53), GlobalMembers.M(104), GlobalMembers.M(238), (int)(num * (float)GlobalMembers.M(255))));
			if (this.WantWarningGlow())
			{
				Color warningGlowColor = this.GetWarningGlowColor();
				if (warningGlowColor.mAlpha > 0)
				{
					Color color = g.GetColor();
					g.SetDrawMode(Graphics.DrawMode.Additive);
					g.SetColor(warningGlowColor);
					Utils.DrawImageCentered(g, theImage, (float)num2, (float)num3);
					g.SetDrawMode(Graphics.DrawMode.Normal);
					g.SetColor(color);
				}
			}
			levelBarRect.mWidth = (int)(this.mLevelBarPct * (float)levelBarRect.mWidth + (float)this.mLevelBarSizeBias);
			g.FillRect(levelBarRect);
			if (this.mLevelBarBonusAlpha > 0.0)
			{
				Rect levelBarRect2 = this.GetLevelBarRect();
				levelBarRect2.mWidth = (int)((float)levelBarRect2.mWidth * this.GetLevelPct());
				levelBarRect2.mX += this.mTransBoardOffsetX;
				levelBarRect2.mY -= this.mTransBoardOffsetY;
				g.SetColor(new Color(GlobalMembers.M(240), GlobalMembers.M(255), 200, (int)(this.mLevelBarBonusAlpha * (double)GlobalMembers.M(255))));
				g.FillRect(levelBarRect2);
			}
			Graphics3D graphics3D = g.Get3D();
			SexyTransform2D mDrawTransform = this.mLevelBarPIEffect.mDrawTransform;
			Rect mClipRect = g.mClipRect;
			if (graphics3D != null)
			{
				levelBarRect.Scale(this.mScale, this.mScale, GlobalMembers.S(960), GlobalMembers.S(600));
				this.mLevelBarPIEffect.mDrawTransform.Translate((float)GlobalMembers.S(-960), (float)GlobalMembers.S(-600));
				this.mLevelBarPIEffect.mDrawTransform.Scale((float)this.mScale, (float)this.mScale);
				this.mLevelBarPIEffect.mDrawTransform.Translate((float)GlobalMembers.S(960), (float)GlobalMembers.S(600));
			}
			int num4 = (int)(this.mAlphaCurve * (double)this.GetAlpha() * 255.0);
			if (num4 == 255)
			{
				g.SetClipRect(levelBarRect);
				this.mLevelBarPIEffect.mColor = new Color(255, 255, 255, (int)(num * (float)GlobalMembers.M(255)));
				this.mLevelBarPIEffect.Draw(g);
				this.mLevelBarPIEffect.mDrawTransform = mDrawTransform;
				g.SetColor(new Color(255, 255, 255, (int)(num * (float)GlobalMembers.M(150))));
				if (!this.mWantLevelup && this.mHyperspace == null)
				{
					int num5 = GlobalMembersResourcesWP.IMAGE_LEVELBAR_ENDPIECE.mWidth / 2;
					g.SetClipRect(levelBarRect.mX, levelBarRect.mY, levelBarRect.mWidth + num5, levelBarRect.mHeight);
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_LEVELBAR_ENDPIECE, levelBarRect.mX + levelBarRect.mWidth - num5, levelBarRect.mY);
				}
			}
			g.SetClipRect(mClipRect);
			g.PopState();
		}

		public virtual void DrawCountdownBar(Graphics g)
		{
			g.SetColorizeImages(true);
			float num = (float)Math.Pow((double)this.GetBoardAlpha(), 4.0);
			g.SetColor(new Color(255, 255, 255, (int)(this.GetBoardAlpha() * (float)GlobalMembers.M(255))));
			Rect countdownBarRect = this.GetCountdownBarRect();
			int boardCenterX = this.GetBoardCenterX();
			int theNum = this.GetBoardY() + 800 + GlobalMembers.M(30);
			Image image_INGAMEUI_PROGRESS_BAR_BACK = GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_BACK;
			Utils.DrawImageCentered(g, image_INGAMEUI_PROGRESS_BAR_BACK, (float)GlobalMembers.S(boardCenterX), (float)GlobalMembers.S(theNum));
			g.SetColor(new Color(GlobalMembers.M(64), GlobalMembers.M(32), GlobalMembers.M(8), (int)(num * (float)GlobalMembers.M(255))));
			if (this.WantWarningGlow())
			{
				Color warningGlowColor = this.GetWarningGlowColor();
				if (warningGlowColor.mAlpha > 0)
				{
					Color color = g.GetColor();
					g.SetDrawMode(Graphics.DrawMode.Additive);
					g.SetColor(warningGlowColor);
					Utils.DrawImageCentered(g, image_INGAMEUI_PROGRESS_BAR_BACK, (float)GlobalMembers.S(boardCenterX), (float)GlobalMembers.S(theNum));
					g.SetDrawMode(Graphics.DrawMode.Normal);
					g.SetColor(color);
				}
			}
			countdownBarRect.mWidth = (int)(this.mCountdownBarPct * (float)countdownBarRect.mWidth + (float)this.mLevelBarSizeBias);
			g.FillRect(countdownBarRect);
			if (this.mLevelBarBonusAlpha > 0.0)
			{
				Rect countdownBarRect2 = this.GetCountdownBarRect();
				countdownBarRect2.mWidth = (int)((float)countdownBarRect2.mWidth * this.GetLevelPct());
				g.SetColor(new Color(GlobalMembers.M(240), GlobalMembers.M(255), 200, (int)(this.mLevelBarBonusAlpha * (double)GlobalMembers.M(255))));
				g.FillRect(countdownBarRect2);
			}
			Graphics3D graphics3D = g.Get3D();
			SexyTransform2D mDrawTransform = this.mCountdownBarPIEffect.mDrawTransform;
			Rect mClipRect = g.mClipRect;
			if (graphics3D != null)
			{
				countdownBarRect.Scale(this.mScale, this.mScale, GlobalMembers.S(960), GlobalMembers.S(600));
				this.mCountdownBarPIEffect.mDrawTransform.Translate((float)GlobalMembers.S(-960), (float)GlobalMembers.S(-600));
				this.mCountdownBarPIEffect.mDrawTransform.Scale((float)this.mScale, (float)this.mScale);
				this.mCountdownBarPIEffect.mDrawTransform.Translate((float)GlobalMembers.S(960), (float)GlobalMembers.S(600));
			}
			g.SetClipRect(countdownBarRect);
			this.mCountdownBarPIEffect.mColor = new Color(255, 255, 255, (int)(num * (float)GlobalMembers.M(255)));
			this.mCountdownBarPIEffect.Draw(g);
			this.mCountdownBarPIEffect.mDrawTransform = mDrawTransform;
			g.SetColor(Color.White);
			g.mClipRect = mClipRect;
		}

		public virtual void DrawCountPopups(Graphics g)
		{
		}

		public void DrawComplements(Graphics g)
		{
			if (this.mSuspendingGame)
			{
				return;
			}
			g.SetDrawMode(Graphics.DrawMode.Normal);
			if (this.mComplementAlpha != 0.0)
			{
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255.0 * this.mComplementAlpha * (double)this.GetPieceAlpha())));
				g.SetFont(GlobalMembersResources.FONT_HUGE);
				int num = g.StringWidth(GlobalMembers.gComplementStr[this.mComplementNum]);
				Utils.SetFontLayerColor((ImageFont)g.GetFont(), 0, Bej3Widget.COLOR_CRYSTALBALL_FONT);
				g.DrawString(GlobalMembers.gComplementStr[this.mComplementNum], (int)(GlobalMembers.S((float)this.GetBoardCenterX()) - (float)(num / 2)), GlobalMembers.S(this.GetBoardCenterY()) + g.GetFont().GetAscent() / 2);
			}
		}

		public void DrawPointMultiplier(Graphics g, bool front)
		{
			if (!this.mShowPointMultiplier)
			{
				return;
			}
			g.SetDrawMode(Graphics.DrawMode.Normal);
			Rect mClipRect = g.mClipRect;
			Graphics3D graphics3D = g.Get3D();
			SexyTransform2D theTransform = default(SexyTransform2D);
			if (graphics3D != null && this.mIsWholeGameReplay)
			{
				SexyTransform2D theMatrix = new SexyTransform2D(true);
				graphics3D.PopTransform(ref theMatrix);
				theTransform.CopyFrom(theMatrix);
			}
			Image multiplierImage = this.GetMultiplierImage();
			int multiplierImageX = this.GetMultiplierImageX();
			int multiplierImageY = this.GetMultiplierImageY();
			int num = multiplierImageX + multiplierImage.GetCelWidth() / 2;
			int num2 = multiplierImageY + ConstantsWP.BOARD_MULTIPLIER_Y;
			new Point(num + GlobalMembers.MS(-3), num2 + GlobalMembers.MS(-2));
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 0, Bej3Widget.COLOR_SUBHEADING_1_STROKE);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 1, Bej3Widget.COLOR_SUBHEADING_1_FILL);
			if (!front)
			{
				g.DrawImage(multiplierImage, multiplierImageX, multiplierImageY);
				int num3 = 0;
				if (this.mPrevPointMultAlpha != 0.0)
				{
					Color color = this.mPrevPointMultAlpha;
					num3 = color.mAlpha;
					g.SetColor(color);
					g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
					string theString = string.Format(GlobalMembers._ID("x{0}", 144), this.mPointMultiplier - 1);
					g.DrawString(theString, (int)((float)num - (float)g.StringWidth(theString) * 0.5f), num2);
				}
				if (this.mPointMultPosPct == 1.0 && (this.mPointMultiplier >= 1 || (this.mTimeExpired && this.mPointMultiplier > 0)))
				{
					g.SetColor(new Color(255, 255, 255, 255 - num3));
					g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
					string theString2 = string.Format(GlobalMembers._ID("x{0}", 145), this.mPointMultiplier);
					g.DrawString(theString2, (int)((double)((float)num - (float)g.StringWidth(theString2) * 0.5f) + (double)GlobalMembers.S(-700) * this.mSlideUIPct), num2);
				}
				return;
			}
			if (this.mPointMultiplier > 1 && (this.mPointMultTextMorph.IsDoingCurve() || this.mPointMultAlpha.IsDoingCurve()))
			{
				string theString3 = string.Format(GlobalMembers._ID("x{0}", 146), this.mPointMultiplier);
				Color color2 = this.mPointMultColor;
				color2.mRed = (int)SexyMath.Lerp((double)this.mPointMultColor.mRed, 255.0, this.mPointMultAlpha);
				color2.mGreen = (int)SexyMath.Lerp((double)this.mPointMultColor.mGreen, 255.0, this.mPointMultAlpha);
				color2.mBlue = (int)SexyMath.Lerp((double)this.mPointMultColor.mBlue, 255.0, this.mPointMultAlpha);
				color2.mAlpha = (int)((double)(255f * this.GetPieceAlpha()) * (1.0 - this.mSlideUIPct) * this.mPointMultAlpha * (1.0 - this.mPointMultTextMorph) * 0.5);
				g.SetColor(color2);
				float num4 = (float)this.mPointMultScale;
				g.SetScale(num4, num4, (float)num, (float)(num2 + ConstantsWP.BOARD_MULTIPLIER_LARGE_Y));
				g.SetFont(GlobalMembersResources.FONT_HEADER);
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_HEADER, 0, Color.White);
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_HEADER, 1, Color.White);
				g.DrawString(theString3, (int)((float)num - (float)g.StringWidth(theString3) * 0.5f), (int)((float)num2 + (float)ConstantsWP.BOARD_MULTIPLIER_LARGE_Y_OFFSET * num4 * ConstantsWP.BOARD_MULTIPLIER_LARGE_Y_SCALE_OFFSET));
				g.SetScale(1f, 1f, 0f, 0f);
			}
			if (graphics3D != null && this.mIsWholeGameReplay)
			{
				graphics3D.PushTransform(theTransform);
			}
			g.mClipRect = mClipRect;
		}

		public void DrawReplayOverlay(Graphics g)
		{
			bool flag = false;
			g.SetDrawMode(Graphics.DrawMode.Normal);
			if (this.mRewinding)
			{
				g.PushState();
				g.Translate((int)(-(int)g.mTransX), (int)(-(int)g.mTransY));
				GlobalMembers.gGR.ExecuteOperations(g, GlobalMembers.gGR.GetLastOperationTimestamp());
				g.PopState();
				GlobalMembers.gGR.ClearOperationsFrom(this.mPlaybackTimestamp);
				flag = true;
			}
			if (this.mInReplay)
			{
				flag = true;
			}
			if (flag && !this.mIsOneMoveReplay && !this.mIsWholeGameReplay)
			{
				MTRand.SetRandAllowed(true);
				if (this.mRewinding)
				{
					g.SetColor(new Color(GlobalMembers.M(64), GlobalMembers.M(64), GlobalMembers.M(64), GlobalMembers.M(128)));
					g.FillRect(0, 0, this.mWidth, this.mHeight);
					g.SetDrawMode(Graphics.DrawMode.Normal);
					for (int i = 0; i < (int)(this.mRewindRand.Next() % (uint)GlobalMembers.M(10) + (uint)GlobalMembers.M(6)); i++)
					{
						g.SetColor(new Color(255, 255, 255, GlobalMembers.M(64)));
						g.FillRect(0, (int)((ulong)this.mRewindRand.Next() % (ulong)((long)this.mHeight)), this.mWidth, (int)((ulong)this.mRewindRand.Next() % (ulong)((long)GlobalMembers.M(2)) + (ulong)((long)GlobalMembers.M(1))));
					}
				}
				else if (this.mInReplay)
				{
					g.SetColor(new Color(GlobalMembers.M(64), GlobalMembers.M(64), GlobalMembers.M(64), GlobalMembers.M(70)));
					g.FillRect(0, 0, this.mWidth, this.mHeight);
					for (int j = 0; j < (int)(this.mRewindRand.Next() % (uint)GlobalMembers.M(2) + (uint)GlobalMembers.M(3)); j++)
					{
						g.SetColor(new Color(150, 150, 150, GlobalMembers.M(50)));
						g.FillRect(0, (int)((ulong)this.mRewindRand.Next() % (ulong)((long)this.mHeight)), this.mWidth, (int)((ulong)this.mRewindRand.Next() % (ulong)((long)GlobalMembers.M(2)) + (ulong)((long)GlobalMembers.M(1))));
					}
				}
				MTRand.SetRandAllowed(false);
			}
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			this.DrawOverlayPreAnnounce(g, thePriority);
			this.DrawOverlayPostAnnounce(g, thePriority);
		}

		public virtual void DrawOverlayPreAnnounce(Graphics g, int thePriority)
		{
			g.SetDrawMode(Graphics.DrawMode.Normal);
			float num = (float)this.mAlphaCurve;
			g.SetColor(new Color(255, 255, 255, (int)(255f * num)));
			if (num != 1f)
			{
				g.PushColorMult();
			}
			this.DrawPointMultiplier(g, false);
			if (this.mSlideUIPct >= 1.0 && this.mGameOverCount > 0)
			{
				if (this.mTutorialPieceIrisPct != 0.0 && this.mTimeExpired)
				{
					this.DrawIris(g, GlobalMembers.MS(800), GlobalMembers.MS(100));
				}
				if (GlobalMembers.gApp.mInterfaceState != InterfaceState.INTERFACE_STATE_BADGEMENU && GlobalMembers.gApp.mInterfaceState != InterfaceState.INTERFACE_STATE_GAMEDETAILMENU && this.mHyperspace == null)
				{
					g.PushState();
					this.mPostFXManager.Draw(g);
					g.PopState();
				}
				return;
			}
			if (this.mSpeedFireBarPIEffect[0] != null)
			{
				this.mSpeedFireBarPIEffect[0].Draw(g);
			}
			if (this.mSpeedFireBarPIEffect[1] != null)
			{
				this.mSpeedFireBarPIEffect[1].Draw(g);
			}
			if (this.mBoardDarken > 0f)
			{
				Rect mScreenBounds = GlobalMembers.gApp.mScreenBounds;
				mScreenBounds.Offset(-this.mX, -this.mY);
				g.SetClipRect(mScreenBounds);
				g.SetColor(new Color(0, 0, 0, (int)(this.GetBoardAlpha() * this.mBoardDarken * (float)GlobalMembers.M(128))));
				g.FillRect(-this.mX, -this.mY, this.mWidth, this.mHeight);
				g.SetColor(Color.White);
				this.DrawPieces(g, true);
			}
			if (GlobalMembers.gApp.mInterfaceState != InterfaceState.INTERFACE_STATE_BADGEMENU && GlobalMembers.gApp.mInterfaceState != InterfaceState.INTERFACE_STATE_GAMEDETAILMENU && this.mHyperspace == null && this.mUpdateCnt > 130)
			{
				g.PushState();
				this.mPostFXManager.Draw(g);
				g.PopState();
			}
			Graphics3D graphics3D = g.Get3D();
			this.DrawLightning(g);
			if (this.WantsHideOnPause())
			{
				if (this.mVisPausePct > 0f)
				{
					this.DrawPauseText(g, this.mVisPausePct);
				}
				else if (this.mSuspendingGame)
				{
					this.DrawPauseText(g, 1f);
				}
			}
			this.mWidgetManager.FlushDeferredOverlayWidgets(1);
			this.DrawCountPopups(g);
			this.DrawComplements(g);
			this.DrawPointMultiplier(g, true);
			this.DrawReplayOverlay(g);
			Piece tutorialIrisPiece = this.GetTutorialIrisPiece();
			if (tutorialIrisPiece != null)
			{
				this.DrawIris(g, (int)GlobalMembers.S(tutorialIrisPiece.CX()), (int)GlobalMembers.S(tutorialIrisPiece.CY()));
			}
			if (this.mGameOverPiece != null)
			{
				if (graphics3D != null)
				{
					graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_TEST_INSIDE);
					g.SetColor(Color.White);
					if (this.mShowBoard)
					{
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_BOOM_FTOP_WIDGET, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_BOOM_FTOP_WIDGET))), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_BOOM_FTOP_WIDGET))));
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_BOOM_FBOTTOM_WIDGET, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_BOOM_FBOTTOM_WIDGET))), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_BOOM_FBOTTOM_WIDGET))));
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_BOOM_BOARD, GlobalMembers.S(this.GetColScreenX(0) + GlobalMembers.M(0)), GlobalMembers.S(this.GetRowScreenY(0) + GlobalMembers.M(-20)));
						g.PushState();
						g.ClipRect(GlobalMembers.S(this.GetColScreenX(0) + GlobalMembers.M(0)), GlobalMembers.S(this.GetRowScreenY(0) + GlobalMembers.M(-20)), GlobalMembers.S(800), GlobalMembers.S(800));
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_BOOM_CRATER, (int)(GlobalMembers.S(this.mGameOverPiece.CX()) - (float)(GlobalMembersResourcesWP.IMAGE_BOOM_CRATER.mWidth / 2)), (int)(GlobalMembers.S(this.mGameOverPiece.CY()) - (float)(GlobalMembersResourcesWP.IMAGE_BOOM_CRATER.mHeight / 2)));
						g.PopState();
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_BOOM_FGRIDBAR_TOP, GlobalMembers.S(this.GetColScreenX(0)) + GlobalMembers.MS(-16), GlobalMembers.S(this.GetRowScreenY(0)) + GlobalMembers.MS(-33));
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_BOOM_FGRIDBAR_BOT, GlobalMembers.S(this.GetColScreenX(0)) + GlobalMembers.MS(-7), GlobalMembers.S(this.GetRowScreenY(8)) + GlobalMembers.MS(-7));
					}
				}
				if (graphics3D != null)
				{
					graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_NONE);
				}
				if (this.mShowBoard)
				{
					this.DrawPiece(g, this.mGameOverPiece, (float)this.mGameOverPieceScale);
					this.DrawPieceText(g, this.mGameOverPiece, (float)this.mGameOverPieceScale);
				}
				if (this.mGameOverPieceGlow > 0.0 && this.mShowBoard)
				{
					float num2 = (float)(this.mGameOverPieceScale * GlobalMembers.M(0.5));
					g.SetColorizeImages(true);
					g.SetColor(this.mGameOverPieceGlow);
					Transform transform = new Transform();
					transform.Scale(num2, num2);
					g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_ANGRYBOMB, transform, GlobalMembers.S(this.mGameOverPiece.GetScreenX() + 50f), GlobalMembers.S(this.mGameOverPiece.GetScreenY() + 50f));
					g.SetColorizeImages(false);
				}
				g.SetColorizeImages(true);
				g.SetColor(this.mNovaAlpha);
				Transform transform2 = new Transform();
				transform2.Scale((float)this.mNovaRadius, (float)this.mNovaRadius);
				g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_BOOM_NOVA, transform2, GlobalMembers.S(this.mGameOverPiece.CX()), GlobalMembers.S(this.mGameOverPiece.CY()));
			}
			g.SetDrawMode(Graphics.DrawMode.Normal);
		}

		public virtual void DrawOverlayPostAnnounce(Graphics g, int thePriority)
		{
			g.SetDrawMode(Graphics.DrawMode.Normal);
			if (this.mAnnouncements.Count > 0)
			{
				this.mAnnouncements[0].Draw(g);
			}
			if (this.mGameOverPiece != null && this.mNukeRadius > 0.0)
			{
				Transform transform = new Transform();
				g.SetColor(this.mNukeAlpha);
				transform.Reset();
				transform.Scale((float)this.mNukeRadius, (float)this.mNukeRadius);
				g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_BOOM_NUKE, transform, GlobalMembers.S(this.mGameOverPiece.CX()), GlobalMembers.S(this.mGameOverPiece.CY()));
			}
			if (this.mReplayFadeout != 0.0)
			{
				int num = 80;
				g.SetColor(this.mReplayFadeout);
				g.FillRect(GlobalMembers.S(-160), 0, GlobalMembers.S(1920), GlobalMembers.S(1200) + num);
			}
			if (this.mRestartPrevImage != null)
			{
				DeviceImage deviceImage = GlobalMembers.gApp.mRestartRT.Lock(GlobalMembers.gApp.mScreenBounds.mWidth, GlobalMembers.gApp.mScreenBounds.mHeight);
				g.SetColor(Color.White);
				int num2 = (int)((double)GlobalMembers.gApp.mScreenBounds.mHeight * this.mRestartPct);
				int num3 = Math.Max(0, Math.Min(GlobalMembers.gApp.mScreenBounds.mHeight, num2));
				Rect theSrcRect = new Rect(0, num3, GlobalMembers.gApp.mScreenBounds.mWidth, GlobalMembers.gApp.mScreenBounds.mHeight - num3);
				Rect theDestRect = new Rect(GlobalMembers.gApp.mScreenBounds.mX, num3, GlobalMembers.gApp.mScreenBounds.mWidth, GlobalMembers.gApp.mScreenBounds.mHeight - num3);
				if (this.mRestartPrevImage == deviceImage)
				{
					g.DrawImage(this.mRestartPrevImage, theDestRect, theSrcRect);
				}
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.SetDrawMode(Graphics.DrawMode.Normal);
				GlobalMembers.gApp.mRestartRT.Unlock();
			}
			if (this.mMessager != null)
			{
				this.mMessager.Draw(g, GlobalMembers.MS(10), this.mMessager.mFont.mHeight * (this.mMessager.mMessages.Count + 1) + GlobalMembers.MS(10));
			}
			if (this.mAlphaCurve != 1.0)
			{
				g.PopColorMult();
			}
		}

		public virtual void DrawGameElements(Graphics g)
		{
			if (!this.mContentLoaded)
			{
				return;
			}
			Rect mClipRect = g.mClipRect;
			if (this.mHyperspace != null)
			{
				this.ClipCollapsingBoard(g);
			}
			g.SetDrawMode(Graphics.DrawMode.Normal);
			GlobalMembers.gFrameLightCount = 0;
			g.SetColorizeImages(true);
			Color color = new Color(255, 255, 255, (int)((double)(255f * this.GetAlpha()) * this.mSideAlpha));
			g.SetColor(color);
			if (this.AllowSpeedBonus() && this.mSpeedBonusDisp != 0.0)
			{
				g.SetColor(this.mSpeedBonusDisp);
				ImageFont imageFont = (ImageFont)GlobalMembersResources.FONT_SUBHEADER;
				g.SetFont(imageFont);
				string theString = GlobalMembers._ID("SPEED", 3234);
				int num = g.StringWidth(theString);
				float num2 = (float)this.mSpeedBonusPointsScale;
				g.SetScale(num2, num2, (float)(ConstantsWP.SPEEDBOARD_SPEED_BONUS_SCALE_X + num / 2), (float)ConstantsWP.SPEEDBOARD_SPEED_BONUS_SCALE_Y);
				Utils.SetFontLayerColor(imageFont, 0, new Color(0, 0, 0, 0));
				Utils.SetFontLayerColor(imageFont, 1, Color.White);
				string theString2;
				if (this.mSpeedBonusCount == 0)
				{
					theString2 = string.Format(GlobalMembers._ID("+{0}", 3235), (int)((float)Math.Min(200, (this.mSpeedBonusLastCount + 1) * 20) * this.GetModePointMultiplier()));
				}
				else
				{
					theString2 = string.Format(GlobalMembers._ID("+{0}", 3235), (int)((float)Math.Min(200, (this.mSpeedBonusCount + 1) * 20) * this.GetModePointMultiplier()));
				}
				g.DrawString(theString, ConstantsWP.SPEEDBOARD_SPEED_BONUS_X, ConstantsWP.SPEEDBOARD_SPEED_BONUS_Y);
				g.DrawString(theString2, ConstantsWP.SPEEDBOARD_SPEED_BONUS_X_2, ConstantsWP.SPEEDBOARD_SPEED_BONUS_Y_2);
				if (this.mSpeedBonusNum > 0.0)
				{
					Utils.SetFontLayerColor(imageFont, 1, new Color(14716992));
					int num3 = Math.Max(g.StringWidth(theString), g.StringWidth(theString2));
					float num4 = (float)(this.mSpeedBonusNum * GlobalMembers.M(1.8) + GlobalMembers.M(-0.95));
					int num5 = (int)((float)num3 * num4);
					if (num5 > 0)
					{
						if (this.mSpeedBonusNum < 1.0)
						{
							g.SetClipRect(0, 0, num5 + ConstantsWP.SPEEDBOARD_SPEED_BONUS_X, this.mHeight);
						}
						g.DrawString(theString, ConstantsWP.SPEEDBOARD_SPEED_BONUS_X, ConstantsWP.SPEEDBOARD_SPEED_BONUS_Y);
						g.DrawString(theString2, ConstantsWP.SPEEDBOARD_SPEED_BONUS_X_2, ConstantsWP.SPEEDBOARD_SPEED_BONUS_Y_2);
						g.ClearClipRect();
					}
				}
				g.SetScale(1f, 1f, 0f, 0f);
			}
			g.SetColor(color);
			float mTransX = g.mTransX;
			float mTransY = g.mTransY;
			if (this.mSideXOff != 0.0)
			{
				g.Translate((int)(this.mSideXOff * (double)this.mSlideXScale), 0);
			}
			else
			{
				g.Translate((int)((double)GlobalMembers.S(1260) * this.mSlideUIPct), 0);
			}
			this.DrawPieces(g, false);
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && !this.mSuspendingGame && piece.mHintAlpha != 0.0)
					{
						g.SetColor(new Color(255, 255, 255, (int)((double)this.GetPieceAlpha() * piece.mHintAlpha * 255.0)));
						g.SetColorizeImages(true);
						Transform transform = new Transform();
						transform.Translate(0f, (float)((int)GlobalMembers.S(piece.mHintArrowPos)));
						Point[] array2 = new Point[]
						{
							new Point(1, 0),
							new Point(0, 0),
							new Point(0, 0),
							new Point(0, 1)
						};
						for (int k = 0; k < 4; k++)
						{
							g.DrawImageTransformF(GlobalMembersResourcesWP.IMAGE_HINTARROW, transform, GlobalMembers.S(piece.CX() + (float)array2[k].mX), GlobalMembers.S(piece.CY() + (float)array2[k].mY));
							transform.RotateDeg(90f);
						}
					}
				}
			}
			g.SetColor(color);
			if (this.mIsWholeGameReplay && !GlobalMembers.gApp.Is3DAccelerated())
			{
				g.SetColor(Color.White);
				g.PushState();
				g.Translate(this.mTimeSlider.mX, this.mTimeSlider.mY);
				this.mTimeSlider.Draw(g);
				g.SetFont(GlobalMembersResources.FONT_HEADER);
				((ImageFont)g.mFont).PushLayerColor("OUTLINE", new Color(64, 0, 32));
				((ImageFont)g.mFont).PushLayerColor("GLOW", new Color(224, 64, 160));
				int theX = (int)(this.mTimeSlider.mVal * (double)(this.mTimeSlider.mWidth - this.mTimeSlider.mThumbImage.GetCelWidth()) + (double)(this.mTimeSlider.mThumbImage.GetCelWidth() / 2));
				int ticksLeft = this.GetTicksLeft();
				string theString3 = string.Format(GlobalMembers._ID("{0}:{1:d2}", 151), (ticksLeft + 99) / 100 / 60, (ticksLeft + 99) / 100 % 60);
				g.WriteString(theString3, theX, this.mTimeSlider.mHeight / 2 + GlobalMembers.MS(17));
				((ImageFont)g.mFont).PopLayerColor("OUTLINE");
				((ImageFont)g.mFont).PopLayerColor("GLOW");
				g.PopState();
			}
			g.mTransX = mTransX;
			g.mTransY = mTransY;
			if (this.mHyperspace != null)
			{
				g.SetClipRect(mClipRect);
			}
		}

		public virtual void DrawSpeedBonus(Graphics g)
		{
		}

		public void DrawDistortion(Graphics g)
		{
			this.mDistortionPieces.Clear();
		}

		public virtual void DrawPieces(Graphics g)
		{
			this.DrawPieces(g, false);
		}

		public virtual void DrawPieces(Graphics g, bool thePostFX)
		{
			int numPieces = 0;
			int numPieces2 = 0;
			int numPieces3 = 0;
			uint num = 0U;
			Graphics3D graphics3D = g.Get3D();
			if (graphics3D != null)
			{
				graphics3D.SetTexture(0, GlobalMembersResourcesWP.IMAGE_GEMS_RED);
			}
			Piece[,] array = this.mBoard;
			int upperBound = array.GetUpperBound(0);
			int upperBound2 = array.GetUpperBound(1);
			for (int i = array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
				{
					Piece piece = array[i, j];
					if (piece != null && piece != this.mGameOverPiece && (!thePostFX || piece.mElectrocutePercent > 0f || ((ulong)num & (ulong)(1L << ((piece.mColor + 1) & 31))) != 0UL) && (!thePostFX || (!piece.IsFlagSet(4096U) && !piece.IsFlagSet(2048U))))
					{
						if (piece.IsFlagSet(65536U))
						{
							this.DP_pQuestPieces[numPieces3++] = piece;
						}
						else
						{
							this.DP_pStdPieces[numPieces++] = piece;
							if (!piece.IsFlagSet(1U) && !this.CanBakeShadow(piece))
							{
								this.DP_pShadowPieces[numPieces2++] = piece;
							}
						}
					}
				}
			}
			if (!thePostFX)
			{
				if (!this.mSuspendingGame)
				{
					this.DrawShadowPieces(g, this.DP_pShadowPieces, numPieces2);
				}
				if (GlobalMembers.gApp.mInterfaceState != InterfaceState.INTERFACE_STATE_BADGEMENU && GlobalMembers.gApp.mInterfaceState != InterfaceState.INTERFACE_STATE_GAMEDETAILMENU && this.mHyperspace == null)
				{
					this.mPreFXManager.Draw(g);
				}
				g.SetDrawMode(Graphics.DrawMode.Normal);
				g.SetColorizeImages(false);
				num = uint.MaxValue;
			}
			else
			{
				for (int k = 0; k < this.mLightningStorms.Count; k++)
				{
					if (this.mLightningStorms[k].mStormType == 7)
					{
						num |= 1U << this.mLightningStorms[k].mColor + 1;
					}
				}
			}
			if (!this.mSuspendingGame)
			{
				this.DrawStandardPieces(g, this.DP_pStdPieces, numPieces);
			}
			this.DrawQuestPieces(g, this.DP_pQuestPieces, numPieces3, thePostFX);
			if (this.mSuspendingGame)
			{
				return;
			}
			if (this.mLightningStorms.Count > 0 && graphics3D != null && graphics3D.SupportsPixelShaders() && this.GetPieceAlpha() >= 0.99f)
			{
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.SetColorizeImages(true);
				RenderEffect effect = graphics3D.GetEffect(GlobalMembersResourcesWP.EFFECT_GEM_SUN);
				using (RenderEffectAutoState renderEffectAutoState = new RenderEffectAutoState(g, effect))
				{
					while (!renderEffectAutoState.IsDone())
					{
						for (int l = 0; l < this.mLightningStorms.Count; l++)
						{
							LightningStorm lightningStorm = this.mLightningStorms[l];
							if (lightningStorm.mLightingAlpha == 0.0)
							{
								Piece[,] array2 = this.mBoard;
								int upperBound3 = array2.GetUpperBound(0);
								int upperBound4 = array2.GetUpperBound(1);
								for (int m = array2.GetLowerBound(0); m <= upperBound3; m++)
								{
									for (int n = array2.GetLowerBound(1); n <= upperBound4; n++)
									{
										Piece piece2 = array2[m, n];
										if (piece2 != null && piece2 != this.mGameOverPiece && !this.IsPieceSwapping(piece2) && (!thePostFX || piece2.mElectrocutePercent > 0f || ((ulong)num & (ulong)(1L << ((piece2.mColor + 1) & 31))) != 0UL) && (!thePostFX || (!piece2.IsFlagSet(4096U) && !piece2.IsFlagSet(2048U))))
										{
											this.DrawGemLightningLighting(g, piece2, lightningStorm);
										}
									}
								}
							}
						}
						renderEffectAutoState.NextPass();
					}
				}
				g.SetDrawMode(Graphics.DrawMode.Normal);
			}
			if (this.mSunPosition.IsInitialized() && !this.mSunPosition.HasBeenTriggered())
			{
				g.SetDrawMode(Graphics.DrawMode.Additive);
				RenderEffect effect2 = graphics3D.GetEffect(GlobalMembersResourcesWP.EFFECT_GEM_SUN);
				using (RenderEffectAutoState renderEffectAutoState2 = new RenderEffectAutoState(g, effect2))
				{
					while (!renderEffectAutoState2.IsDone())
					{
						Piece[,] array3 = this.mBoard;
						int upperBound5 = array3.GetUpperBound(0);
						int upperBound6 = array3.GetUpperBound(1);
						for (int num2 = array3.GetLowerBound(0); num2 <= upperBound5; num2++)
						{
							for (int num3 = array3.GetLowerBound(1); num3 <= upperBound6; num3++)
							{
								Piece piece3 = array3[num2, num3];
								if (piece3 != null && piece3 != this.mGameOverPiece && !this.IsPieceSwapping(piece3) && (!thePostFX || piece3.mElectrocutePercent > 0f || ((ulong)num & (ulong)(1L << ((piece3.mColor + 1) & 31))) != 0UL) && (!thePostFX || (!piece3.IsFlagSet(4096U) && !piece3.IsFlagSet(2048U))))
								{
									this.DrawGemSun(g, piece3, effect2);
								}
							}
						}
						renderEffectAutoState2.NextPass();
					}
				}
				g.SetDrawMode(Graphics.DrawMode.Normal);
			}
			for (int num4 = 0; num4 < this.mSwapDataVector.Count; num4++)
			{
				SwapData swapData = this.mSwapDataVector[num4];
				float num5 = (float)swapData.mGemScale;
				if (!swapData.mDestroyTarget && swapData.mPiece2 != null && swapData.mSwapPct <= 3.1415927410125732)
				{
					this.DrawPiece(g, swapData.mPiece2, 1f - num5);
				}
				this.DrawPiece(g, swapData.mPiece1, 1f + num5);
				if (!swapData.mDestroyTarget && swapData.mPiece2 != null && swapData.mSwapPct > 3.1415927410125732)
				{
					this.DrawPiece(g, swapData.mPiece2, 1f - num5);
				}
			}
			if (this.mCursorSelectPos.mX != -1 && this.GetSelectedPiece() == null)
			{
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255f * this.GetPieceAlpha())));
				GlobalMembers.gGR.DrawImage(g, GlobalMembersResourcesWP.IMAGE_SELECTOR, GlobalMembers.S(this.GetBoardX() + this.GetColX(this.mCursorSelectPos.mX)), GlobalMembers.S(this.GetBoardY() + this.GetRowY(this.mCursorSelectPos.mY)));
			}
		}

		public virtual void DrawQuestPieces(Graphics g, Piece[] pPieces, int numPieces)
		{
			this.DrawQuestPieces(g, pPieces, numPieces, false);
		}

		public virtual void DrawQuestPieces(Graphics g, Piece[] pPieces, int numPieces, bool thePostFX)
		{
		}

		public virtual void DrawButtons(Graphics g)
		{
			if (!this.mIsWholeGameReplay)
			{
				g.SetDrawMode(Graphics.DrawMode.Normal);
				float mTransX = g.mTransX;
				float mTransY = g.mTransY;
				g.Translate(this.mHintButton.mX + (int)GlobalMembers.S(this.mSideXOff) + this.mOffsetX, this.mHintButton.mY + this.mOffsetY);
				this.mHintButton.Draw(g);
				g.SetColor(Color.White);
				g.mTransX = mTransX;
				g.mTransY = mTransY;
				if (this.mResetButton != null)
				{
					g.Translate(this.mResetButton.mX + (int)GlobalMembers.S(this.mSideXOff), this.mResetButton.mY);
					this.mResetButton.Draw(g);
					g.SetColorizeImages(false);
					g.mTransX = mTransX;
					g.mTransY = mTransY;
				}
			}
		}

		public void DrawComboUIGem(Graphics g, int aCombo)
		{
		}

		public void DrawIris(Graphics g, int theCX, int theCY)
		{
			Graphics3D graphics3D = g.Get3D();
			if (graphics3D != null)
			{
				g.PushState();
				g.SetColorizeImages(true);
				g.SetColor(new Color(GlobalMembers.M(0), GlobalMembers.M(0), GlobalMembers.M(0), (int)(255f * ConstantsWP.BOARD_IRIS_ALPHA)));
				int num = (int)((float)GlobalMembersResourcesWP.IMAGE_BOARD_IRIS.mWidth * ConstantsWP.BOARD_IRIS_SCALE);
				int num2 = (int)((float)GlobalMembersResourcesWP.IMAGE_BOARD_IRIS.mHeight * ConstantsWP.BOARD_IRIS_SCALE);
				Rect rect = new Rect(theCX - num / 2, theCY - num2 / 2, num, num2);
				g.FillRect(0, 0, rect.mX, this.mHeight);
				g.FillRect(rect.mX + rect.mWidth, 0, this.mWidth - rect.mX + rect.mWidth, this.mHeight);
				g.FillRect(rect.mX, 0, rect.mWidth, rect.mY);
				g.FillRect(rect.mX, rect.mY + rect.mHeight, rect.mWidth, this.mHeight - rect.mY + rect.mHeight);
				g.SetColor(new Color(255, 255, 255, 255));
				g.SetScale(ConstantsWP.BOARD_IRIS_SCALE, ConstantsWP.BOARD_IRIS_SCALE, (float)theCX, (float)theCY);
				Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_BOARD_IRIS, 0, theCX, theCY);
				g.PopState();
			}
		}

		public virtual int GetUICenterX()
		{
			return GlobalMembers.RS(ConstantsWP.BOARD_UI_SCORE_CENTER_X);
		}

		public virtual void DrawScoreWidget(Graphics g)
		{
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			if (!this.mContentLoaded)
			{
				return;
			}
			if (this.mFlattening)
			{
				base.DrawAll(theFlags, g);
				return;
			}
			if (this.mFlattenedImage != null && GlobalMembers.gIs3D && this.mFlattenedImage.GetRenderData() != null)
			{
				this.Flatten();
			}
			if (this.mAlpha == 0.0)
			{
				return;
			}
			Graphics3D graphics3D = null;
			if (this.mNeedsMaskCleared && graphics3D != null)
			{
				graphics3D.ClearMask();
			}
			if (this.mQuestPortalPct != 0.0 && graphics3D != null)
			{
				graphics3D.ClearMask();
				graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_WRITE_MASKONLY);
				Transform transform = new Transform();
				transform.Scale((float)(this.mQuestPortalPct * (double)GlobalMembers.M(0.93f)), (float)(this.mQuestPortalPct * (double)GlobalMembers.M(0.93f)));
				g.SetColor(Color.White);
				g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_BOOM_NUKE, transform, (float)((int)((double)GlobalMembers.S(800) + (double)GlobalMembers.S(this.mQuestPortalOrigin.mX - 800) * (1.0 - this.mQuestPortalCenterPct))), (float)((int)((double)GlobalMembers.S(600) + (double)GlobalMembers.S(this.mQuestPortalOrigin.mY - 600) * (1.0 - this.mQuestPortalCenterPct))));
				graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_TEST_INSIDE);
				float num = (float)Math.Min(1.0, GlobalMembers.M(0.5) + this.mQuestPortalPct * (double)GlobalMembers.M(0.75f));
				SexyTransform2D theTransform = default(SexyTransform2D);
				theTransform.Translate((float)(-(float)this.mWidth / 2), (float)(-(float)this.mHeight / 2));
				theTransform.Scale(num, num);
				theTransform.Translate((float)(this.mWidth / 2), (float)(this.mHeight / 2));
				theTransform.Translate((float)((int)((double)GlobalMembers.S(this.mQuestPortalOrigin.mX - 800) * (1.0 - this.mQuestPortalCenterPct) * (double)GlobalMembers.M(1f))), (float)((int)((double)GlobalMembers.S(this.mQuestPortalOrigin.mY - 600) * (1.0 - this.mQuestPortalCenterPct) * (double)GlobalMembers.M(1f))));
				graphics3D.PushTransform(theTransform);
			}
			bool flag = this.mScale != 1.0;
			if (graphics3D != null && flag)
			{
				float num2 = (float)GlobalMembers.gSexyApp.mScreenBounds.mWidth / 2f;
				float num3 = (float)GlobalMembers.gSexyApp.mScreenBounds.mHeight / 2f;
				SexyTransform2D theTransform2 = default(SexyTransform2D);
				theTransform2.Translate(-num2, -num3);
				theTransform2.Scale((float)this.mScale, (float)this.mScale);
				theTransform2.Translate(num2, num3);
				graphics3D.PushTransform(theTransform2);
			}
			if (this.mGameOverPiece != null && graphics3D != null)
			{
				graphics3D.ClearMask();
			}
			if (this.mNovaRadius != 0.0 && this.mShowBoard && graphics3D != null)
			{
				graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_WRITE_MASKONLY);
				Transform transform2 = new Transform();
				transform2.Scale((float)(this.mNovaRadius * (double)GlobalMembers.M(0.93f)), (float)(this.mNovaRadius * (double)GlobalMembers.M(0.93f)));
				g.SetColor(Color.White);
				g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_BOOM_NUKE, transform2, (float)((int)GlobalMembers.S(this.mGameOverPiece.CX())), (float)((int)GlobalMembers.S(this.mGameOverPiece.CY())));
				graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_TEST_OUTSIDE);
			}
			if (this.mFlattenedImage != null)
			{
				g.DrawImage(this.mFlattenedImage, GlobalMembers.gApp.mScreenBounds.mX, 0);
			}
			else if (this.mShowBoard)
			{
				base.DrawAll(theFlags, g);
			}
			else
			{
				if (this.mHyperspace != null)
				{
					g.PushState();
					g.Translate(this.mHyperspace.mX, this.mHyperspace.mY);
					this.mHyperspace.DrawBackground(g);
					g.PopState();
				}
				base.DeferOverlay(1);
			}
			if (flag || this.mQuestPortalPct != 0.0)
			{
				Graphics mCurG = this.mWidgetManager.mCurG;
				g.PushState();
				g.Translate((int)(-(int)g.mTransX), (int)(-(int)g.mTransY));
				this.mWidgetManager.mCurG = g;
				this.mWidgetManager.FlushDeferredOverlayWidgets(10);
				g.PopState();
				this.mWidgetManager.mCurG = mCurG;
			}
			if (graphics3D != null && flag)
			{
				graphics3D.PopTransform();
			}
			if (graphics3D != null && this.mQuestPortalPct != 0.0)
			{
				graphics3D.PopTransform();
			}
		}

		public override void Draw(Graphics g)
		{
			if (!this.mContentLoaded)
			{
				return;
			}
			Rect mClipRect = g.mClipRect;
			if (this.mHyperspace != null)
			{
				this.ClipCollapsingBoard(g);
			}
			if (this.mFirstDraw)
			{
				this.FirstDraw();
				this.mFirstDraw = false;
			}
			base.DeferOverlay(1);
			if (this.mSlideUIPct >= 1.0 && this.mGameOverCount > 0)
			{
				return;
			}
			g.SetColorizeImages(true);
			this.DrawCheckboard(g);
			if (this.mHyperspace != null)
			{
				g.SetClipRect(mClipRect);
			}
			if (this.mWarningGlowAlpha > 0f)
			{
				g.SetColor(this.mWarningGlowColor);
				g.mColor.mAlpha = (int)(this.mWarningGlowAlpha * 255f);
				g.SetColorizeImages(true);
				int num = 800;
				int theNum = 800;
				int boardX = this.GetBoardX();
				int boardY = this.GetBoardY();
				float num2 = GlobalMembers.M(0.5f) + GlobalMembers.M(2.5f) * (float)Math.Pow((double)this.mWarningGlowAlpha, (double)GlobalMembers.M(0.5f));
				int theStretchedHeight = (int)((float)GlobalMembersResourcesWP.IMAGE_DANGERBORDERUP.GetHeight() * num2);
				int num3 = (int)((float)GlobalMembersResourcesWP.IMAGE_DANGERBORDERLEFT.GetWidth() * num2);
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DANGERBORDERUP, GlobalMembers.S(boardX), GlobalMembers.S(boardY), GlobalMembers.S(num), theStretchedHeight);
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DANGERBORDERLEFT, GlobalMembers.S(boardX), GlobalMembers.S(boardY), num3, GlobalMembers.S(theNum));
				g.DrawImageMirror(GlobalMembersResourcesWP.IMAGE_DANGERBORDERLEFT, new Rect(GlobalMembers.S(boardX + num) - num3, GlobalMembers.S(boardY), num3, GlobalMembers.S(theNum)), new Rect(0, 0, GlobalMembersResourcesWP.IMAGE_DANGERBORDERLEFT.GetWidth(), GlobalMembersResourcesWP.IMAGE_DANGERBORDERLEFT.GetHeight()));
				Transform transform = new Transform();
				transform.Scale((float)num, 0f - num2);
				g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_DANGERBORDERUP, transform, (float)GlobalMembers.S(boardX + num / 2), (float)(GlobalMembers.S(theNum) + GlobalMembersResourcesWP.IMAGE_DANGERBORDERUP.GetHeight() / 2));
				g.SetColor(new Color(-1));
				g.SetColorizeImages(false);
			}
			if (!this.mIsWholeGameReplay)
			{
				this.DrawUI(g);
			}
			if (this.mCurrentHint != null && !this.mInReplay)
			{
				this.mCurrentHint.Draw(g);
			}
			g.SetColor(Color.White);
		}

		public virtual void FirstDraw()
		{
		}

		public virtual void DrawUI(Graphics g)
		{
			if (this.mSideAlpha != 1.0)
			{
				g.SetColor(this.mSideAlpha);
				g.PushColorMult();
			}
			float mTransX = g.mTransX;
			float mTransY = g.mTransY;
			if (this.mSideXOff != 0.0)
			{
				g.Translate((int)(this.mSideXOff * (double)this.mSlideXScale), 0);
			}
			this.DrawBars(g);
			if (this.mSideXOff != 0.0)
			{
				g.mTransX = mTransX;
				g.mTransY = mTransY;
			}
			this.DrawHUD(g);
			if (this.mSideXOff != 0.0)
			{
				g.mTransX = mTransX;
				g.mTransY = mTransY;
				g.Translate((int)GlobalMembers.S(this.mSideXOff), 0);
			}
			if (this.WantWarningGlow())
			{
				this.DrawWarningHUD(g);
			}
			if (this.WantDrawButtons())
			{
				this.DrawButtons(g);
			}
			this.DrawHUDText(g);
			if (this.mSideXOff != 0.0)
			{
				g.mTransX = mTransX;
				g.mTransY = mTransY;
			}
			if (this.mSideAlpha != 1.0)
			{
				g.PopColorMult();
			}
		}

		public virtual void DrawHUD(Graphics g)
		{
			g.SetDrawMode(Graphics.DrawMode.Normal);
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255f * this.GetAlpha())));
			float mTransX = g.mTransX;
			float mTransY = g.mTransY;
			if (this.mSideXOff != 0.0)
			{
				g.Translate((int)this.mSideXOff, 0);
			}
			this.DrawReplayWidget(g);
			this.DrawScoreWidget(g);
			this.DrawMenuWidget(g);
			if (this.mSideXOff != 0.0)
			{
				g.mTransX = mTransX;
				g.mTransY = mTransY;
				g.Translate((int)(this.mSideXOff * (double)this.mSlideXScale), 0);
			}
			this.DrawFrame(g);
			if (this.mSideXOff != 0.0)
			{
				g.mTransX = mTransX;
				g.mTransY = mTransY;
			}
		}

		public virtual void DrawWarningHUD(Graphics g)
		{
			Color color = g.GetColor().Clone();
			bool flag = this.WantTopFrame() || this.WantBottomFrame();
			if (flag)
			{
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.SetColor(this.GetWarningGlowColor());
			}
			if (this.WantTopFrame())
			{
				if (this.WantTopLevelBar() || this.GetTimeLimit() > 0)
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_FRAME, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_FRAME))) + (float)this.mTransBoardOffsetX), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_FRAME))) - (float)this.mTransBoardOffsetY));
				}
				else
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME))) + (float)this.mTransBoardOffsetX), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME))) - (float)this.mTransBoardOffsetY));
				}
			}
			if (this.WantBottomFrame())
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME, (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME))) + (float)this.mTransBoardOffsetX), (int)(GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(GlobalMembersResourcesWP.GetIdByImage(GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME))) + (float)this.mTransBoardOffsetY));
			}
			if (flag)
			{
				g.SetColor(color);
				g.SetDrawMode(Graphics.DrawMode.Normal);
			}
		}

		public virtual void DrawHUDText(Graphics g)
		{
			if (this.WantExpandedTopWidget() == 0)
			{
				int num = (int)(GlobalMembers.IMG_SXOFS(1094) + (float)GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON.GetCelWidth());
				int num2 = num + (this.mWidth - num) / 2;
				int num3 = (int)((GlobalMembers.IMG_SYOFS(1091) + (float)GlobalMembersResources.FONT_DIALOG.mAscent) / 2f - (float)this.mTransLevelOffsetY);
				g.SetFont(GlobalMembersResources.FONT_DIALOG);
				float mScaleX = g.mScaleX;
				float mScaleY = g.mScaleY;
				g.SetScale(ConstantsWP.BOARD_LEVEL_SCORE_SCALE, ConstantsWP.BOARD_LEVEL_SCORE_SCALE, (float)num2, (float)(num3 - g.GetFont().GetAscent() / 2));
				Utils.SetFontLayerColor((ImageFont)g.GetFont(), 0, Color.White);
				g.WriteString(string.Format(GlobalMembers._ID("Level {0}", 3232), SexyFramework.Common.CommaSeperate(this.mLevel + 1)), num2, num3);
				g.mScaleX = mScaleX;
				g.mScaleY = mScaleY;
			}
			else
			{
				string topWidgetButtonText = this.GetTopWidgetButtonText();
				if (topWidgetButtonText != string.Empty)
				{
					g.SetFont(GlobalMembersResources.FONT_DIALOG);
					g.WriteString(topWidgetButtonText, GlobalMembers.S(this.GetUICenterX()), GlobalMembers.MS(262));
				}
			}
			if (this.WantDrawScore())
			{
				this.DrawScore(g);
			}
			if (this.WantDrawTimer())
			{
				this.DrawTimer(g);
			}
		}

		public virtual void DrawMenuWidget(Graphics g)
		{
		}

		public virtual void DrawBars(Graphics g)
		{
			if (this.WantCountdownBar())
			{
				this.DrawCountdownBar(g);
				return;
			}
			if (this.WantTopLevelBar())
			{
				this.DrawLevelBar(g);
			}
		}

		public virtual void DrawTimer(Graphics g)
		{
			if (this.GetTimeLimit() == 0)
			{
				return;
			}
			if (!this.mIsWholeGameReplay)
			{
				int ticksLeft = this.GetTicksLeft();
				Rect countdownBarRect = this.GetCountdownBarRect();
				Point impliedObject = new Point(GlobalMembers.S(this.GetBoardCenterX()), GlobalMembers.MS(500));
				Point p = new Point(this.GetTimeDrawX(), countdownBarRect.mY + countdownBarRect.mHeight / 2);
				impliedObject + p;
				string theString = string.Format(GlobalMembers._ID("{0}:{1:d2}", 148), (ticksLeft + 99) / 100 / 60, (ticksLeft + 99) / 100 % 60);
				g.SetFont(GlobalMembersResources.FONT_SCORE);
				g.SetColor(new Color(255, 255, 255, (int)((double)(255f * this.GetAlpha()) * this.mTimerAlpha)));
				g.WriteString(theString, p.mX, p.mY + this.GetTimerYOffset());
			}
		}

		public void Flatten()
		{
			this.mFlattenedImage = null;
			this.mFlattening = true;
			DeviceImage theDestImage = GlobalMembers.gApp.mRestartRT.Lock(GlobalMembers.gApp.mScreenBounds.mWidth, GlobalMembers.gApp.mScreenBounds.mHeight);
			Graphics graphics = new Graphics(theDestImage);
			graphics.Translate(-GlobalMembers.gApp.mScreenBounds.mX, 0);
			bool mVisible = GlobalMembers.gApp.mUnderDialogWidget.mVisible;
			bool mVisible2 = GlobalMembers.gApp.mTooltipManager.mVisible;
			bool flag = GlobalMembers.gApp.mQuestMenu != null && GlobalMembers.gApp.mQuestMenu.mVisible;
			bool flag2 = GlobalMembers.gApp.mQuestMenu != null && GlobalMembers.gApp.mQuestMenu.mBackground.mVisible;
			GlobalMembers.gApp.mUnderDialogWidget.mVisible = false;
			GlobalMembers.gApp.mTooltipManager.mVisible = false;
			if (flag)
			{
				GlobalMembers.gApp.mQuestMenu.mVisible = false;
			}
			if (flag2)
			{
				GlobalMembers.gApp.mQuestMenu.mBackground.mVisible = false;
			}
			this.mWidgetManager.DrawWidgetsTo(graphics);
			GlobalMembers.gApp.mUnderDialogWidget.mVisible = mVisible;
			GlobalMembers.gApp.mTooltipManager.mVisible = mVisible2;
			if (flag)
			{
				GlobalMembers.gApp.mQuestMenu.mVisible = true;
			}
			if (flag2)
			{
				GlobalMembers.gApp.mQuestMenu.mBackground.mVisible = true;
			}
			GlobalMembers.gApp.mRestartRT.Unlock();
			this.mFlattening = false;
			this.mFlattenedImage = theDestImage;
			this.MarkAllDirty();
		}

		public override void RemovedFromManager(WidgetManager theWidgetManager)
		{
			base.RemovedFromManager(theWidgetManager);
			if (this.mBackground != null && this.mBackground.mParent != null && (GlobalMembers.gApp.mBoard == null || GlobalMembers.gApp.mBoard == this))
			{
				this.mBackground.mParent.RemoveWidget(this.mBackground);
			}
			if (this.mGameOverCount == 0 && GlobalMembers.gApp.mMainMenu.mIsFullGame())
			{
				this.SaveGame();
			}
			GlobalMembers.gGR.mIgnoreDraws = false;
			GlobalMembers.gGR.mRecordDraws = false;
		}

		public void DrawPauseText(Graphics g, float alpha)
		{
			if (alpha == 0f)
			{
				return;
			}
			if (alpha > 1f)
			{
				alpha = 1f;
			}
			if (g.mPushedColorVector.Count > 0)
			{
				g.PopColorMult();
			}
			g.SetFont(GlobalMembersResources.FONT_HUGE);
			g.SetColor(new Color(255, 255, 255, (int)(255f * alpha)));
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), 0, Bej3Widget.COLOR_INGAME_ANNOUNCEMENT);
			string theString = GlobalMembers._ID("PAUSED", 147);
			g.DrawString(theString, GlobalMembers.S(this.GetBoardCenterX()) - g.GetFont().StringWidth(theString) / 2, GlobalMembers.MS(540));
		}

		public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
			if (this.mInReplay)
			{
				return;
			}
			if (!this.CanPlay())
			{
				return;
			}
			if (GlobalMembers.gApp.TouchedToolTip(x, y))
			{
				return;
			}
			this.mCursorSelectPos = new Point(-1, -1);
			this.mMouseDown = true;
			this.mMouseDownX = x;
			this.mMouseDownY = y;
			int num = this.GetColAt((int)((float)x / GlobalMembers.S(1f)));
			int num2 = this.GetRowAt((int)((float)y / GlobalMembers.S(1f)));
			Piece selectedPiece = this.GetSelectedPiece();
			if (theBtnNum != 0)
			{
				if (selectedPiece != null)
				{
					selectedPiece.mSelected = false;
					selectedPiece.mSelectorAlpha.SetConstant(0.0);
				}
				return;
			}
			Piece piece = this.GetPieceAtScreenXY((int)((float)x / GlobalMembers.S(1f)), (int)((float)y / GlobalMembers.S(1f)));
			if (selectedPiece == piece)
			{
				return;
			}
			bool flag = false;
			if (piece == null)
			{
				piece = this.GetPieceAtRowCol(num2, num);
				if (piece != null)
				{
					flag = true;
				}
			}
			else
			{
				num = piece.mCol;
				num2 = piece.mRow;
				Piece piece2 = this.MoveAssistedPiece(piece, x, y, selectedPiece);
				if (piece2 != null)
				{
					piece = piece2;
					num = piece.mCol;
					num2 = piece.mRow;
				}
			}
			if (piece != null && !piece.mCanSwap)
			{
				flag = true;
			}
			if (!flag && piece != selectedPiece)
			{
				if (selectedPiece != null)
				{
					if (this.mLightningStorms.Count == 0 && !this.QueueSwap(selectedPiece, num2, num))
					{
						selectedPiece.mSelected = false;
						selectedPiece.mSelectorAlpha.SetConstant(0.0);
						if (piece != null)
						{
							piece.mSelected = true;
							piece.mSelectorAlpha.SetConstant(1.0);
							return;
						}
					}
				}
				else if (piece != null)
				{
					if (piece.IsButton())
					{
						this.QueueSwap(piece, piece.mRow, piece.mCol, false, true);
						return;
					}
					piece.mSelected = true;
					piece.mSelectorAlpha.SetConstant(1.0);
					GlobalMembers.gSexyApp.PlaySample(GlobalMembersResourcesWP.SOUND_SELECT);
					return;
				}
			}
			else if (selectedPiece != null)
			{
				selectedPiece.mSelected = false;
				selectedPiece.mSelectorAlpha.SetConstant(0.0);
			}
		}

		public override void MouseUp(int x, int y)
		{
			this.mMouseDown = false;
			Piece selectedPiece = this.GetSelectedPiece();
			if (selectedPiece != null && selectedPiece == this.mMouseUpPiece && !this.IsPieceSwapping(selectedPiece))
			{
				selectedPiece.mSelected = false;
				selectedPiece.mSelectorAlpha.SetConstant(0.0);
				this.mMouseUpPiece = null;
				return;
			}
			this.mMouseUpPiece = selectedPiece;
		}

		public override void MouseDrag(int x, int y)
		{
			base.MouseDrag(x, y);
			if (!this.CanPlay())
			{
				return;
			}
			Piece selectedPiece = this.GetSelectedPiece();
			if (!this.mMouseDown || selectedPiece == null)
			{
				return;
			}
			int num = x - this.mMouseDownX;
			int num2 = y - this.mMouseDownY;
			if (Math.Abs(num) >= GlobalMembers.MS(40) || Math.Abs(num2) >= GlobalMembers.MS(40))
			{
				Point impliedObject = new Point(-1, -1);
				if (Math.Abs(num) > Math.Abs(num2))
				{
					if (num > 0 && selectedPiece.mCol < 7)
					{
						impliedObject = new Point(selectedPiece.mCol + 1, selectedPiece.mRow);
					}
					else if (num < 0 && selectedPiece.mCol > 0)
					{
						impliedObject = new Point(selectedPiece.mCol - 1, selectedPiece.mRow);
					}
				}
				else if (num2 > 0 && selectedPiece.mRow < 7)
				{
					impliedObject = new Point(selectedPiece.mCol, selectedPiece.mRow + 1);
				}
				else if (num2 < 0 && selectedPiece.mRow > 0)
				{
					impliedObject = new Point(selectedPiece.mCol, selectedPiece.mRow - 1);
				}
				if (impliedObject != new Point(-1, -1))
				{
					this.QueueSwap(selectedPiece, impliedObject.mY, impliedObject.mX);
				}
			}
		}

		public override void KeyDown(KeyCode theKeyCode)
		{
			Point point = default(Point);
			bool flag = false;
			bool flag2 = false;
			if (this.mInReplay)
			{
				return;
			}
			if (theKeyCode <= (KeyCode)65)
			{
				if (theKeyCode != KeyCode.KEYCODE_ESCAPE)
				{
					switch (theKeyCode)
					{
					case KeyCode.KEYCODE_SPACE:
					{
						Piece selectedPiece = this.GetSelectedPiece();
						if (selectedPiece != null)
						{
							selectedPiece.mSelected = false;
							selectedPiece.mSelectorAlpha.SetConstant(0.0);
						}
						else
						{
							flag = true;
						}
						break;
					}
					case KeyCode.KEYCODE_PRIOR:
					case KeyCode.KEYCODE_NEXT:
					case KeyCode.KEYCODE_END:
					case KeyCode.KEYCODE_HOME:
						break;
					case KeyCode.KEYCODE_LEFT:
						flag2 = true;
						point = new Point(-1, 0);
						break;
					case KeyCode.KEYCODE_UP:
						flag2 = true;
						point = new Point(0, -1);
						break;
					case KeyCode.KEYCODE_RIGHT:
						flag2 = true;
						point = new Point(1, 0);
						break;
					case KeyCode.KEYCODE_DOWN:
						flag2 = true;
						point = new Point(0, 1);
						break;
					default:
						if (theKeyCode == (KeyCode)65)
						{
							flag = true;
							point = new Point(-1, 0);
						}
						break;
					}
				}
				else if (!this.mKilling)
				{
					this.ButtonDepress(1);
				}
			}
			else if (theKeyCode != (KeyCode)68)
			{
				if (theKeyCode != (KeyCode)83)
				{
					if (theKeyCode == (KeyCode)87)
					{
						flag = true;
						point = new Point(0, -1);
					}
				}
				else
				{
					flag = true;
					point = new Point(0, 1);
				}
			}
			else
			{
				flag = true;
				point = new Point(1, 0);
			}
			Piece piece = null;
			if (!this.mTimeExpired && this.CanPlay())
			{
				if (flag && this.GetSelectedPiece() == null)
				{
					Piece pieceAtScreenXY;
					if (this.mCursorSelectPos.mX == -1)
					{
						pieceAtScreenXY = this.GetPieceAtScreenXY((int)((float)this.mWidgetManager.mLastMouseX / GlobalMembers.S(1f)), (int)((float)this.mWidgetManager.mLastMouseY / GlobalMembers.S(1f)));
					}
					else
					{
						pieceAtScreenXY = this.GetPieceAtScreenXY(this.GetBoardX() + this.GetColX(this.mCursorSelectPos.mX) + 50, this.GetBoardY() + this.GetRowY(this.mCursorSelectPos.mY) + 50);
					}
					if (pieceAtScreenXY != null)
					{
						if (pieceAtScreenXY.IsFlagSet(6144U))
						{
							if (this.mCursorSelectPos.mX != -1)
							{
								piece = pieceAtScreenXY;
							}
						}
						else if ((point.mX != 0 || point.mY != 0 || this.mCursorSelectPos.mX != -1) && !this.IsPieceSwapping(pieceAtScreenXY))
						{
							pieceAtScreenXY.mSelected = true;
							pieceAtScreenXY.mSelectorAlpha.SetConstant(1.0);
						}
					}
				}
				if (point.mX != 0 || point.mY != 0 || piece != null)
				{
					Piece piece2 = piece;
					if (piece2 == null)
					{
						piece2 = this.GetSelectedPiece();
					}
					if (piece2 != null && (this.IsGameSuspended() || !this.QueueSwap(piece2, piece2.mRow + point.mY, piece2.mCol + point.mX, false, true)))
					{
						piece2.mSelected = false;
						GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.ePIECE_SELECTOR_ALPHA, piece2.mSelectorAlpha);
						return;
					}
					if (this.mCursorSelectPos.mX == -1)
					{
						if (!flag)
						{
							this.mCursorSelectPos = new Point(3 + Math.Max(0, point.mX), 3 + Math.Max(0, point.mY));
							return;
						}
					}
					else if (flag2)
					{
						this.mCursorSelectPos.mX = Math.Max(0, Math.Min(7, this.mCursorSelectPos.mX + point.mX));
						this.mCursorSelectPos.mY = Math.Max(0, Math.Min(7, this.mCursorSelectPos.mY + point.mY));
					}
				}
			}
		}

		public override void KeyChar(char theChar)
		{
			if (this.mInReplay)
			{
				return;
			}
			if (theChar == ' ' && this.mCursorSelectPos.mX == -1 && (this.WantsHideOnPause() || this.mUserPaused) && !this.mInReplay)
			{
				this.mUserPaused = !this.mUserPaused;
			}
		}

		public override void ButtonPress(int theId)
		{
			if (theId != 0)
			{
				base.ButtonPress(theId);
			}
		}

		public override void ButtonMouseEnter(int theId)
		{
			if (theId != 0)
			{
				base.ButtonMouseEnter(theId);
				return;
			}
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_HINT);
		}

		public override void ButtonMouseLeave(int theId)
		{
			if (theId != 0)
			{
				base.ButtonMouseLeave(theId);
			}
		}

		public override void ButtonDepress(int theId)
		{
			if (this.mInReplay)
			{
				return;
			}
			if (!this.AllowUI())
			{
				return;
			}
			if (theId != 0)
			{
				base.ButtonDepress(theId);
			}
			switch (theId)
			{
			case 0:
				if (this.mBoardHidePct == 0f)
				{
					this.ShowHint(true);
					return;
				}
				break;
			case 1:
				GlobalMembers.gApp.DoPauseMenu();
				return;
			case 2:
				break;
			case 3:
				this.mWatchedCurReplay = true;
				this.mReplayWasTutorial = false;
				this.RewindToReplay(this.mCurReplayData);
				this.mReplayWidgetShowPct.SetConstant(1.0);
				this.HideReplayWidget();
				GlobalMembers.gApp.DisableOptionsButtons(true);
				break;
			default:
				return;
			}
		}

		public virtual void SliderVal(int theId, double theVal)
		{
			this.mSliderSetTicks = (int)((1.0 - theVal) * (double)this.mUReplayTotalTicks + 0.5);
		}

		public virtual void DialogButtonDepress(int theDialogId, int theButtonId)
		{
			Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.GetDialog(theDialogId);
			if (theDialogId == 18)
			{
				if (this.mDeferredTutorialVector.Count > 0)
				{
					DeferredTutorial deferredTutorial = this.mDeferredTutorialVector[0];
					if (theButtonId == 1000)
					{
						this.mDeferredTutorialVector.RemoveAt(0);
						if (((HintDialog)bej3Dialog).mNoHintsCheckbox.IsChecked())
						{
							this.SetTutorialCleared(19);
							this.mTutorialFlags = (int)GlobalMembers.gApp.mProfile.mTutorialFlags;
							this.mDeferredTutorialVector.Clear();
						}
					}
					else if (theButtonId == 1001)
					{
						this.mReplayWasTutorial = true;
						ReplayData mReplayData = deferredTutorial.mReplayData;
						this.RewindToReplay(mReplayData);
						this.mReplayIgnoredMoves = 0;
						this.SetTutorialCleared(deferredTutorial.mTutorialFlag, false);
						GlobalMembers.gApp.DisableOptionsButtons(true);
					}
					else
					{
						this.mDeferredTutorialVector.RemoveAt(0);
						this.SetTutorialCleared(19);
						this.mTutorialFlags = (int)GlobalMembers.gApp.mProfile.mTutorialFlags;
						this.mDeferredTutorialVector.Clear();
					}
				}
				if (this.mIllegalMoveTutorial)
				{
					this.mIllegalMoveTutorial = false;
				}
				bej3Dialog.mVisible = false;
				this.mTutorialPieceIrisPct.SetConstant(0.0);
				bej3Dialog.Kill();
			}
			if (theDialogId == 22)
			{
				if (theButtonId == 1000)
				{
					bej3Dialog.Kill();
					bej3Dialog.mCanSlideInMenus = false;
					((PauseMenu)GlobalMembers.gApp.mMenus[7]).Collapse(false, true);
					this.DeleteSavedGame();
					this.mWantLevelup = false;
					this.mHyperspace = null;
					this.Init();
					this.mHasReplayData = false;
					this.NewGame(true);
					for (int i = 0; i < 2; i++)
					{
						if (this.mSpeedFireBarPIEffect[i] != null)
						{
							this.mSpeedFireBarPIEffect[i].Dispose();
							this.mSpeedFireBarPIEffect[i] = null;
						}
					}
					this.HideReplayWidget();
					GlobalMembers.gApp.GoBackToGame();
					return;
				}
				bej3Dialog.Kill();
			}
			if (theDialogId == 50)
			{
				if (theButtonId == 1000)
				{
					bej3Dialog.Kill();
					if (!GlobalMembers.gApp.mMainMenu.mIsFullGame())
					{
						this.DeleteSavedGame();
					}
					bej3Dialog.mCanSlideInMenus = true;
					((PauseMenu)GlobalMembers.gApp.mMenus[7]).Collapse(false, true);
					GlobalMembers.gApp.DoMainMenu();
					return;
				}
				bej3Dialog.Kill();
			}
			if (theDialogId == 51)
			{
				bej3Dialog.Kill();
			}
		}

		public virtual void DisableUI(bool disabled)
		{
			this.mHintButton.SetDisabled(disabled);
			if (this.mResetButton != null)
			{
				this.mResetButton.SetDisabled(disabled);
			}
		}

		public virtual int GetSidebarTextY()
		{
			return GlobalMembers.M(320);
		}

		public virtual void DrawScore(Graphics g)
		{
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			string text = SexyFramework.Common.CommaSeperate(this.mDispPoints);
			if (this.mShowLevelPoints)
			{
				text = string.Format(GlobalMembers._ID("{0} of {1}", 157), text, SexyFramework.Common.CommaSeperate(this.GetLevelPoints()));
			}
			int num = (int)GlobalMembers.IMG_SXOFS(1094) / 2;
			int num2 = (int)(GlobalMembers.IMG_SYOFS(1091) + (float)GlobalMembersResources.FONT_DIALOG.mAscent) / 2 - this.mTransScoreOffsetY;
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), 0, Color.White);
			float mScaleX = g.mScaleX;
			float mScaleY = g.mScaleY;
			g.SetScale(ConstantsWP.BOARD_LEVEL_SCORE_SCALE, ConstantsWP.BOARD_LEVEL_SCORE_SCALE, (float)num, (float)(num2 - g.GetFont().GetAscent() / 2));
			g.WriteString(text, num, num2);
			g.mScaleX = mScaleX;
			g.mScaleY = mScaleY;
		}

		public void DrawReplayWidget(Graphics g)
		{
			if (this.mReplayButton != null)
			{
				if (this.mReplayWidgetShowPct > 0.0)
				{
					bool flag = this.mReplayButton.mIsDown && this.mReplayButton.mIsOver && !this.mReplayButton.mDisabled;
					Rect theRect = ((flag ^ this.mReplayButton.mInverted) ? this.mReplayButton.mButtonImage.GetCelRect(1) : this.mReplayButton.mButtonImage.GetCelRect(0));
					this.mReplayButton.DrawButtonImage(g, this.mReplayButton.mButtonImage, theRect, this.mReplayButton.mX, this.mReplayButton.mY);
				}
				if (this.mReplayPulsePct >= 0.0)
				{
					int celWidth = GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY.GetCelWidth();
					int celHeight = GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY.GetCelHeight();
					int theX = (this.mWidth - celWidth) / 2;
					int theY = (int)((GlobalMembers.IMG_SYOFS(1091) - (float)celHeight) / 2f);
					g.DrawImageCel(GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY, theX, theY, 1);
					if (this.mReplayPulsePct > 0.0)
					{
						g.PushState();
						g.SetDrawMode(Graphics.DrawMode.Additive);
						g.SetColor(new Color(255, 255, 255, (int)(255.0 * this.mReplayPulsePct)));
						g.DrawImageCel(GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY, theX, theY, 0);
						g.PopState();
					}
				}
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			if (this.mLevelBarPIEffect == null && GlobalMembersResourcesWP.PIEFFECT_LEVELBAR != null)
			{
				this.mLevelBarPIEffect = GlobalMembersResourcesWP.PIEFFECT_LEVELBAR.Duplicate();
			}
			if (this.mCountdownBarPIEffect == null && GlobalMembersResourcesWP.PIEFFECT_COUNTDOWNBAR != null)
			{
				this.mCountdownBarPIEffect = GlobalMembersResourcesWP.PIEFFECT_COUNTDOWNBAR.Duplicate();
			}
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			if (Bej3Widget.mCurrentSlidingMenu == this)
			{
				if (GlobalMembers.gApp.mMenus[7] != null)
				{
					GlobalMembers.gApp.mMenus[7].AllowSlideIn(allow, previousTopButton);
				}
				Bej3Widget.mCurrentSlidingMenu = GlobalMembers.gApp.mMenus[7];
			}
			base.AllowSlideIn(allow, previousTopButton);
		}

		public override bool SlideForDialog(bool slideOut, Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
			this.mY = 0;
			return GlobalMembers.gApp.mMenus[7].SlideForDialog(slideOut, dialog, previousButtonType);
		}

		public override int GetShowCurve()
		{
			return 24;
		}

		public void ToggleReplayPulse(bool on)
		{
			if (on)
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBOARD_REPLAY_PULSE_PCT, this.mReplayPulsePct);
				this.mReplayPulsePct.SetMode(1);
				return;
			}
			this.mReplayPulsePct.SetConstant(-1.0);
		}

		public void SyncUnAwardedBadges(List<int> deferredBadgeVector)
		{
			bool flag = this.mGameOverCount == 0;
			for (int i = 0; i < 20; i++)
			{
				Badge badgeByIndex = this.mBadgeManager.GetBadgeByIndex(i);
				if (badgeByIndex != null && (!flag || badgeByIndex.WantsMidGameCalc()) && badgeByIndex.CanUnlock())
				{
					for (int j = 0; j < deferredBadgeVector.Count; j++)
					{
						if (deferredBadgeVector[j] == badgeByIndex.mIdx)
						{
							deferredBadgeVector.RemoveAt(j);
						}
					}
					deferredBadgeVector.Add(badgeByIndex.mIdx);
					GlobalMembers.gApp.mProfile.mBadgeStatus[badgeByIndex.mIdx] = true;
					badgeByIndex.mUnlocked = true;
					GlobalMembers.gApp.mProfile.AddRecentBadge(badgeByIndex.mIdx);
				}
			}
		}

		public virtual void SubmitHighscore()
		{
		}

		public virtual int GetTimerYOffset()
		{
			return 0;
		}

		public virtual Image GetMultiplierImage()
		{
			return null;
		}

		public virtual int GetMultiplierImageX()
		{
			return 0;
		}

		public virtual int GetMultiplierImageY()
		{
			return 0;
		}

		public void MoveGemsOffscreen()
		{
			for (int i = 0; i < 8; i++)
			{
				int num = this.GetBoardY() - (11 - i) * 100;
				for (int j = 0; j < 8; j++)
				{
					Piece piece = this.mBoard[i, j];
					piece.mY = (float)num;
				}
			}
		}

		public void UpdateSlidingHUD(bool slidingOff)
		{
			this.mSlidingHUDCurve.IncInVal((double)ConstantsWP.BOARD_SLIDING_HUD_SPEED);
			if (!this.mSlidingHUDCurve.IsDoingCurve())
			{
				((HyperspaceWhirlpool)this.mHyperspace).mSlidingHUD = false;
				if (slidingOff)
				{
					this.mSlidingHUDCurve.SetConstant(1.0);
				}
				else
				{
					this.mSlidingHUDCurve.SetConstant(0.0);
				}
			}
			int num = (int)((GlobalMembers.IMG_SYOFS(1091) + (float)GlobalMembersResources.FONT_DIALOG.mAscent) / 2f);
			this.mTransBoardOffsetX = (int)((double)this.mWidth * this.mSlidingHUDCurve);
			this.mTransLevelOffsetY = (this.mTransScoreOffsetY = (int)((double)(num + GlobalMembersResources.FONT_DIALOG.GetDescent()) * this.mSlidingHUDCurve));
			this.mTransDashboardOffsetY = (int)((double)(GlobalMembers.gApp.mHeight - ConstantsWP.MENU_Y_POS_HIDDEN) * this.mSlidingHUDCurve);
			GlobalMembers.gApp.mMenus[7].SetTargetPosition(ConstantsWP.MENU_Y_POS_HIDDEN + this.mTransDashboardOffsetY);
			this.mTransHintBtnOffsetX = (int)((double)(GlobalMembers.gApp.mWidth - ConstantsWP.BOARD_UI_HINT_BTN_X) * this.mSlidingHUDCurve);
			this.mHintButton.mX = ConstantsWP.BOARD_UI_HINT_BTN_X + this.mTransHintBtnOffsetX;
			if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN)
			{
				ZenBoard zenBoard = this as ZenBoard;
				if (zenBoard != null)
				{
					this.mTransOptionsBtnOffsetX = (int)((double)(ConstantsWP.ZENBOARD_UI_ZEN_BTN_X + ConstantsWP.ZENBOARD_UI_ZEN_BTN_WIDTH) * this.mSlidingHUDCurve);
					zenBoard.mZenOptionsButton.mX = ConstantsWP.ZENBOARD_UI_ZEN_BTN_X - this.mTransOptionsBtnOffsetX;
				}
			}
		}

		public void UpdateBoardTransition(bool closingBoard)
		{
			this.mTransitionBoardCurve.IncInVal();
			if (!this.mTransitionBoardCurve.IsDoingCurve())
			{
				((HyperspaceWhirlpool)this.mHyperspace).mTransitionBoard = false;
				if (closingBoard)
				{
					this.mTransitionBoardCurve.SetConstant(1.0);
				}
				else
				{
					this.mTransitionBoardCurve.SetConstant(0.0);
				}
			}
			this.mTransBoardOffsetY = (int)((double)((float)(GlobalMembers.S(100) * 8) / 2f) * this.mTransitionBoardCurve);
		}

		public void UpdateSpecialGemsStats(int MoveCreditId)
		{
			int moveStat = this.GetMoveStat(MoveCreditId, 12);
			int moveStat2 = this.GetMoveStat(MoveCreditId, 13);
			int moveStat3 = this.GetMoveStat(MoveCreditId, 14);
			int moveStat4 = this.GetMoveStat(MoveCreditId, 31);
			int theNumber = moveStat + moveStat2 + moveStat3 + moveStat4;
			this.MaxStat(38, theNumber);
		}

		public bool PointInPiece(Piece thePiece, int x, int y)
		{
			int num = (int)thePiece.GetScreenX();
			int num2 = (int)thePiece.GetScreenY();
			return x >= GlobalMembers.S(num) && y >= GlobalMembers.S(num2) && x < GlobalMembers.S(num + 100) && y < GlobalMembers.S(num2 + 100);
		}

		public bool PointInPiece(Piece thePiece, int x, int y, int theFuzzFactor)
		{
			return this.PointInPiece(thePiece, x, y) || this.PointInPiece(thePiece, x - theFuzzFactor, y - theFuzzFactor) || this.PointInPiece(thePiece, x, y - theFuzzFactor) || this.PointInPiece(thePiece, x + theFuzzFactor, y - theFuzzFactor) || this.PointInPiece(thePiece, x + theFuzzFactor, y) || this.PointInPiece(thePiece, x + theFuzzFactor, y + theFuzzFactor) || this.PointInPiece(thePiece, x, y + theFuzzFactor) || this.PointInPiece(thePiece, x - theFuzzFactor, y + theFuzzFactor) || this.PointInPiece(thePiece, x - theFuzzFactor, y);
		}

		public Piece MoveAssistedPiece(Piece pSelectedPiece, int x, int y, Piece pPrevSelectedPiece)
		{
			if (pSelectedPiece == null)
			{
				return null;
			}
			Piece result = null;
			int num = 0;
			int num4;
			int num3;
			int num2 = (num3 = (num4 = 0));
			int num6;
			int num7;
			int num5 = this.FindBestGemMove(pSelectedPiece, out num6, out num7, out num3, out num4, out num2);
			if (num5 >= 3)
			{
				return null;
			}
			if (pSelectedPiece.IsFlagSet(2U))
			{
				return null;
			}
			for (int i = pSelectedPiece.mRow - 1; i <= pSelectedPiece.mRow + 1; i++)
			{
				if (i >= 0 && i < 8)
				{
					for (int j = pSelectedPiece.mCol - 1; j <= pSelectedPiece.mCol + 1; j++)
					{
						if (j >= 0 && j < 8 && (j != pSelectedPiece.mCol || i != pSelectedPiece.mRow))
						{
							Piece pieceAtRowCol = this.GetPieceAtRowCol(i, j);
							if (pieceAtRowCol != pPrevSelectedPiece && pieceAtRowCol != null && this.PointInPiece(pieceAtRowCol, this.mMouseDownX, this.mMouseDownY, GlobalMembers.S(this.FUDGE)))
							{
								num2 = (num3 = (num4 = 0));
								if (pieceAtRowCol.IsFlagSet(2U))
								{
									return pieceAtRowCol;
								}
								num5 = this.FindBestGemMove(pieceAtRowCol, out num6, out num7, out num3, out num4, out num2);
								if (num5 >= 3)
								{
									num5 += num3 * 6;
									num5 += num4 * 20;
									num5 += num2 * 13;
									if (num5 > num)
									{
										result = pieceAtRowCol;
										num = num5;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		public int FindBestGemMove(Piece aPiece, out int theDirXResult, out int theDirYResult, out int iFlameCount, out int iMultiCount, out int iLaserCount)
		{
			int[,] array = new int[,]
			{
				{ 1, 0 },
				{ -1, 0 },
				{ 0, 1 },
				{ 0, -1 }
			};
			int num = -1;
			theDirXResult = 0;
			theDirYResult = 0;
			iFlameCount = 0;
			iMultiCount = 0;
			iLaserCount = 0;
			for (int i = 0; i < 4; i++)
			{
				int num2 = array[i, 0];
				int num3 = array[i, 1];
				int num4 = this.EvalGemSwap(aPiece, num2, num3, out iFlameCount, out iMultiCount, out iLaserCount);
				if (num4 > num)
				{
					num = num4;
					theDirXResult = num2;
					theDirYResult = num3;
				}
			}
			return num;
		}

		public int EvalGemSwap(Piece aPiece, int theDirX, int theDirY, out int iFlameCount, out int iMultiCount, out int iLaserCount)
		{
			int mCol = aPiece.mCol;
			int mRow = aPiece.mRow;
			int num = 0;
			iFlameCount = 0;
			iMultiCount = 0;
			iLaserCount = 0;
			int num2 = mCol + theDirX;
			int num3 = mRow + theDirY;
			if (num2 >= 0 && num2 < 8 && num3 >= 0 && num3 < 8)
			{
				Piece piece = this.mBoard[mRow, mCol];
				if (this.mBoard[num3, num2] != null)
				{
					if (this.mBoard[num3, num2].IsFlagSet(2U))
					{
						Piece[,] array = this.mBoard;
						int upperBound = array.GetUpperBound(0);
						int upperBound2 = array.GetUpperBound(1);
						for (int i = array.GetLowerBound(0); i <= upperBound; i++)
						{
							for (int j = array.GetLowerBound(1); j <= upperBound2; j++)
							{
								Piece piece2 = array[i, j];
								if (piece2 != null && aPiece.mColor == piece2.mColor)
								{
									num++;
									if (piece2.IsFlagSet(16U))
									{
										iMultiCount++;
									}
									if (piece2.IsFlagSet(4U))
									{
										iLaserCount++;
									}
									if (piece2.IsFlagSet(1U))
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
						this.mBoard[num3, num2] = piece;
						MoveAssistEval moveAssistEval = new MoveAssistEval();
						MoveAssistEval moveAssistEval2 = new MoveAssistEval();
						MoveAssistEval moveAssistEval3 = new MoveAssistEval();
						MoveAssistEval moveAssistEval4 = new MoveAssistEval();
						int largestSetAtRow = this.GetLargestSetAtRow(mRow, moveAssistEval);
						int largestSetAtRow2 = this.GetLargestSetAtRow(num3, moveAssistEval2);
						int largestSetAtCol = this.GetLargestSetAtCol(mCol, moveAssistEval3);
						int largestSetAtCol2 = this.GetLargestSetAtCol(num2, moveAssistEval4);
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
						piece = this.mBoard[mRow, mCol];
						this.mBoard[mRow, mCol] = this.mBoard[num3, num2];
						this.mBoard[num3, num2] = piece;
					}
				}
			}
			return num;
		}

		public int GetLargestSetAtCol(int theCol, MoveAssistEval pEval)
		{
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < 8; i++)
			{
				Piece piece = this.mBoard[i, theCol];
				if (piece != null)
				{
					if (piece.mColor <= 7 && piece.mColor == num2)
					{
						num++;
						if (piece.IsFlagSet(1U))
						{
							num4++;
						}
						else if (piece.IsFlagSet(4U))
						{
							num5++;
						}
						else if (piece.IsFlagSet(16U))
						{
							num6++;
						}
						if (num > num3)
						{
							num3 = num;
							if (pEval != null)
							{
								pEval.Flame = num4;
								pEval.Laser = num5;
								pEval.Multiplier = num6;
							}
						}
					}
					else
					{
						num2 = piece.mColor;
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

		public int GetLargestSetAtRow(int theRow, MoveAssistEval pEval)
		{
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < 8; i++)
			{
				Piece piece = this.mBoard[theRow, i];
				if (piece != null)
				{
					if (piece.mColor <= 7 && piece.mColor == num2)
					{
						num++;
						if (piece.IsFlagSet(1U))
						{
							num4++;
						}
						else if (piece.IsFlagSet(4U))
						{
							num5++;
						}
						else if (piece.IsFlagSet(16U))
						{
							num6++;
						}
						if (num > num3)
						{
							num3 = num;
							if (pEval != null)
							{
								pEval.Flame = num4;
								pEval.Laser = num5;
								pEval.Multiplier = num6;
							}
						}
					}
					else
					{
						num2 = piece.mColor;
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

		public virtual void SliderReleased(int theId, double theVal)
		{
		}

		public virtual void DialogButtonPress(int theDialogId, int theButtonId)
		{
		}

		public void BOARD_MSG(string theMsg)
		{
			if (this.mMessager != null)
			{
				this.mMessager.AddMessage(theMsg);
			}
		}

		public void ShowAchievementHint(string achName)
		{
			this.mAchievementHints.Add(new AchievementHint(achName, new AchievementHintFinishedHandler(this.OnAchievementHintFinished)));
		}

		public void OnAchievementHintFinished(AchievementHint sender)
		{
			this.mAchievementHints.Remove(sender);
			this.mCurrentHint = null;
		}

		private int FUDGE;

		public bool mContentLoaded;

		public bool mShouldUnloadContentWhenDone;

		public CurvedVal mSlidingHUDCurve = new CurvedVal();

		public CurvedVal mTransitionBoardCurve = new CurvedVal();

		public int mTransBoardOffsetX;

		public int mTransBoardOffsetY;

		public int mTransLevelOffsetY;

		public int mTransScoreOffsetY;

		public int mTransDashboardOffsetY;

		public int mTransReplayOffsetY;

		public int mTransOptionsBtnOffsetX;

		public int mTransHintBtnOffsetX;

		public Dictionary<string, string> mParams = new Dictionary<string, string>();

		public int mFullLaser;

		public bool mFromDebugMenu;

		public float mUpdateAcc;

		public int mNextPieceId;

		public Piece[,] mBoard = new Piece[8, 8];

		public float[] mBumpVelocities = new float[8];

		public int[] mNextColumnCredit = new int[8];

		public Dictionary<int, Piece> mPieceMap = new Dictionary<int, Piece>();

		public MTRand mRand = new MTRand();

		public Board.EUIConfig mUiConfig;

		public int mLastHitSoundTick;

		public List<SwapData> mSwapDataVector = new List<SwapData>();

		public List<MoveData> mMoveDataVector = new List<MoveData>();

		public List<QueuedMove> mQueuedMoveVector = new List<QueuedMove>();

		public List<StateInfo> mStateInfoVector = new List<StateInfo>();

		public int[] mGameStats = new int[40];

		public int mGameOverCount;

		public int mLevelCompleteCount;

		public int mPoints;

		public bool mInLoadSave;

		public Color[] mBoardColors = new Color[2];

		public List<List<int>> mPointsBreakdown = new List<List<int>>();

		public int mDispPoints;

		public float mLevelBarPct;

		public float mCountdownBarPct;

		public int mLevelBarSizeBias;

		public List<int> mBackgroundIdxSet = new List<int>();

		public bool mGameFinished;

		public CurvedVal mLevelBarBonusAlpha = new CurvedVal();

		public int mLevelPointsTotal;

		public int mLevel;

		public int mHypermixerCheckRow;

		public int mPointMultiplier;

		public bool mShowPointMultiplier;

		public int mCurMoveCreditId;

		public int mCurMatchId;

		public int mGemFallDelay;

		public bool mTimeExpired;

		public int mLastWarningTick;

		public int mScrambleUsesLeft;

		public int mMoveCounter;

		public int mGameTicks;

		public int mIdleTicks;

		public int mSettlingDelay;

		public int mLastMatchTick;

		public int mLastMatchTime;

		public int mMatchTallyCount;

		public int mLastMatchTally;

		public CurvedVal mSpeedModeFactor = new CurvedVal();

		public float mSpeedBonusAlpha;

		public string mSpeedBonusText = string.Empty;

		public CurvedVal mSpeedometerPopup = new CurvedVal();

		public CurvedVal mSpeedometerGlow = new CurvedVal();

		public CurvedVal mSpeedBonusDisp = new CurvedVal();

		public float mSpeedNeedle;

		public int mSpeedBonusPoints;

		public bool mFavorComboGems;

		public List<int> mFavorGemColors = new List<int>();

		public List<int> mNewGemColors = new List<int>();

		public double mSpeedBonusNum;

		public int mSpeedBonusCount;

		public int mSpeedBonusCountHighest;

		public int mSpeedBonusLastCount;

		public int mSpeedMedCount;

		public int mSpeedHighCount;

		public bool mHardwareSpeedBonusDraw;

		public PIEffect mSpeedFirePIEffect = new PIEffect();

		public PIEffect[] mSpeedFireBarPIEffect = new PIEffect[2];

		public PIEffect mLevelBarPIEffect = new PIEffect();

		public PIEffect mCountdownBarPIEffect = new PIEffect();

		public CurvedVal mSpeedBonusPointsGlow = new CurvedVal();

		public CurvedVal mSpeedBonusPointsScale = new CurvedVal();

		public float mSpeedBonusFlameModePct;

		public EBoardType mBoardType;

		public bool mHasBoardSettled;

		public bool mContinuedFromLoad;

		public int mBoardUIOffsetY;

		public int[] mComboColors = new int[5];

		public int mComboCount;

		public int mLastComboCount;

		public int mComboLen;

		public float mComboCountDisp;

		public CurvedVal mComboFlashPct = new CurvedVal();

		public float mComboSelectorAngle;

		public int mLastPlayerSwapColor;

		public int mMoneyDisp;

		public int mMoneyDispGoal;

		public float mComboBonusSlowdownPct;

		public bool mWantNewCoin;

		public int mCoinsEarned;

		public int mPendingCoinAnimations;

		public List<LightningStorm> mLightningStorms = new List<LightningStorm>();

		public PointsManager mPointsManager;

		public Serialiser mLastMoveSave = new Serialiser();

		public Buffer mTestSave = new Buffer();

		public EffectsManager mPreFXManager;

		public EffectsManager mPostFXManager;

		public bool mUserPaused;

		public float mBoardHidePct;

		public float mVisPausePct;

		public Buffer mUReplayBuffer = new Buffer();

		public bool mInUReplay;

		public bool mWasLevelUpReplay;

		public int mUReplayVersion;

		public int mUReplayTotalTicks;

		public int mUReplayLastTick;

		public int mUReplayTicksLeft;

		public int mUReplayGameFlags;

		public bool mIllegalMoveTutorial;

		public bool mReplayWasTutorial;

		public CurvedVal mReplayPulsePct = new CurvedVal();

		public bool mWantReplaySave;

		public MoveData mReplayStartMove = new MoveData();

		public ReplayData mWholeGameReplay = new ReplayData();

		public bool mShowAutohints;

		public bool mHasReplayData;

		public bool mWatchedCurReplay;

		public int mReplayIgnoredMoves;

		public bool mReplayHadIgnoredMoves;

		public ReplayData mCurReplayData = new ReplayData();

		public Serialiser mPreReplaySave = new Serialiser();

		public SoundInstance mRewindSound;

		public SoundInstance mFlameSound;

		public int mRecordTimestamp;

		public int mPlaybackTimestamp;

		public bool mInReplay;

		public bool mIsOneMoveReplay;

		public bool mIsWholeGameReplay;

		public bool mHadReplayError;

		public bool mRewinding;

		public MTRand mRewindRand = new MTRand();

		public CurvedVal mReplayWidgetShowPct = new CurvedVal();

		public CurvedVal mReplayFadeout = new CurvedVal();

		public CurvedVal mPrevPointMultAlpha = new CurvedVal();

		public Point mSrcPointMultPos = default(Point);

		public CurvedVal mPointMultPosPct = new CurvedVal();

		public CurvedVal mPointMultTextMorph = new CurvedVal();

		public CurvedVal mPointMultScale = new CurvedVal();

		public CurvedVal mPointMultAlpha = new CurvedVal();

		public CurvedVal mPointMultYAdd = new CurvedVal();

		public CurvedVal mPointMultDarkenPct = new CurvedVal();

		public Color mPointMultColor = default(Color);

		public CurvedVal mTimerInflate = new CurvedVal();

		public CurvedVal mTimerAlpha = new CurvedVal();

		public List<int> mPointMultSoundQueue = new List<int>();

		public int mPointMultSoundDelay;

		public int mBottomFillRow;

		public int mGemCountValueDisp;

		public int mGemCountValueCheck;

		public CurvedVal mGemCountAlpha = new CurvedVal();

		public CurvedVal mGemScalarAlpha = new CurvedVal();

		public CurvedVal mGemCountCurve = new CurvedVal();

		public int mCascadeCountValueDisp;

		public int mCascadeCountValueCheck;

		public CurvedVal mCascadeCountAlpha = new CurvedVal();

		public CurvedVal mCascadeScalarAlpha = new CurvedVal();

		public CurvedVal mCascadeCountCurve = new CurvedVal();

		public CurvedVal mComplementAlpha = new CurvedVal();

		public CurvedVal mComplementScale = new CurvedVal();

		public int mComplementNum;

		public int mLastComplement;

		public string mSidebarText = string.Empty;

		public bool mShowLevelPoints;

		public int mHintCooldownTicks;

		public int mWantHintTicks;

		public int mTutorialFlags;

		public List<DeferredTutorial> mDeferredTutorialVector = new List<DeferredTutorial>();

		public CurvedVal mTutorialPieceIrisPct = new CurvedVal();

		public bool mShowMoveCredit;

		public bool mDoThirtySecondVoice;

		public CurvedVal mSunPosition = new CurvedVal();

		public bool mSunFired;

		public int mLastSunTick;

		public float mBoardDarken;

		public float mBoardDarkenAnnounce;

		public Color mWarningGlowColor = default(Color);

		public float mWarningGlowAlpha;

		public bool mMouseDown;

		public int mMouseDownX;

		public int mMouseDownY;

		public Piece mMouseUpPiece;

		public Piece mCheatPiece;

		public bool mCheatInputingScore;

		public string mCheatScoreStr;

		public bool mSlowedDown;

		public int mSlowDownCounter;

		public CurvedVal mCoinCatcherPct = new CurvedVal();

		public double mCoinCatcherPctPct;

		public bool mCoinCatcherAppearing;

		public int mBackgroundIdx;

		public Background mBackground;

		public Bej3Button mHintButton;

		public ButtonWidget mReplayButton;

		public ButtonWidget mResetButton;

		public bool mFirstDraw;

		public int mScrambleDelayTicks;

		public Slider mTimeSlider;

		public int mSliderSetTicks;

		public bool mWantLevelup;

		public Hyperspace mHyperspace;

		public bool mDoAutoload;

		public new CurvedVal mAlpha = new CurvedVal();

		public CurvedVal mScale = new CurvedVal();

		public bool mKilling;

		public CurvedVal mSlideUIPct = new CurvedVal();

		public CurvedVal mSideAlpha = new CurvedVal();

		public CurvedVal mSideXOff = new CurvedVal();

		public CurvedVal mBoostShowPct = new CurvedVal();

		public int mStartDelay;

		public int mWantHelpDialog;

		public CurvedVal mRestartPct = new CurvedVal();

		public DeviceImage mRestartPrevImage;

		public DeviceImage mFlattenedImage;

		public bool mFlattening;

		public bool mIsFacebookGame;

		public int mBoostsEnabled;

		public Point mCursorSelectPos = default(Point);

		public int mOffsetX;

		public int mOffsetY;

		public CurvedVal mNukeRadius = new CurvedVal();

		public CurvedVal mNukeAlpha = new CurvedVal();

		public CurvedVal mNovaRadius = new CurvedVal();

		public CurvedVal mNovaAlpha = new CurvedVal();

		public CurvedVal mGameOverPieceScale = new CurvedVal();

		public CurvedVal mGameOverPieceGlow = new CurvedVal();

		public Piece mGameOverPiece;

		public bool mShowBoard;

		public bool mWantsReddishFlamegems;

		private bool mTrialPromptShown;

		private bool mBuyFullGameShown;

		public bool mHyperspacePassed;

		public List<Board.DeferredSound> mDeferredSounds = new List<Board.DeferredSound>();

		public CurvedVal mQuestPortalPct = new CurvedVal();

		public CurvedVal mQuestPortalCenterPct = new CurvedVal();

		public Point mQuestPortalOrigin = default(Point);

		public bool mNeedsMaskCleared;

		public List<Piece> mDistortionPieces = new List<Piece>();

		public List<Board.DistortionQuad> mDistortionQuads = new List<Board.DistortionQuad>();

		public List<Announcement> mAnnouncements = new List<Announcement>();

		public Messager mMessager;

		public BadgeManager mBadgeManager;

		public bool mDoRankUp;

		public bool mZenDoBadgeAward;

		public bool mWantTimeAnnouncement;

		public int mTimeDelayCount;

		public int mReadyDelayCount;

		public int mGoDelayCount;

		public float mSlideXScale;

		public bool mGameClosing;

		public bool mGoAnnouncementDone;

		public bool mTimeAnnouncementDone;

		public bool mBackgroundLoadedThreaded;

		public bool mSuspendingGame;

		public bool mForceReleaseButterfly;

		public Piece mForcedReleasedBflyPiece;

		public int mNOfIntentionalMatchesDuringCascade;

		private List<AchievementHint> mAchievementHints = new List<AchievementHint>();

		protected AchievementHint mCurrentHint;

		public readonly int aMenuYPosHidden = ConstantsWP.MENU_Y_POS_HIDDEN;

		private bool mLevelup;

		private float BumpColumn_MAX_DIST = 100f * GlobalMembers.M(2f);

		private static int[,] FM_aSwapArray = new int[,]
		{
			{ 1, 0 },
			{ -1, 0 },
			{ 0, 1 },
			{ 0, -1 }
		};

		private Dictionary<Piece, int> FS_aBulgeTriggerPieceSet = new Dictionary<Piece, int>();

		private Dictionary<Piece, int> FS_aDelayingPieceSet = new Dictionary<Piece, int>();

		private Dictionary<Piece, int> FS_aTallyPieceSet = new Dictionary<Piece, int>();

		private Dictionary<Piece, int> FS_aPowerupPieceSet = new Dictionary<Piece, int>();

		private List<MatchSet> FS_aMatchedSets = new List<MatchSet>();

		private Dictionary<int, int> FS_aMoveCreditSet = new Dictionary<int, int>();

		private Dictionary<Piece, Pair<int, Piece>> FS_aDeferPowerupMap = new Dictionary<Piece, Pair<int, Piece>>();

		private Dictionary<Piece, int> FS_aDeferLaserSet = new Dictionary<Piece, int>();

		private List<Piece> FS_aDeferExplodeVector = new List<Piece>();

		private int[] UpdateComplements_gComplementPoints = new int[]
		{
			GlobalMembers.M(3),
			GlobalMembers.M(6),
			GlobalMembers.M(12),
			GlobalMembers.M(20),
			GlobalMembers.M(30),
			GlobalMembers.M(45)
		};

		private static float Update_aSpeed = 1f;

		private Board.MyPiece[] DSP_pNormalPieces = new Board.MyPiece[128];

		private Board.MyPiece[] DSP_pHyperCubes = new Board.MyPiece[64];

		private Board.MyPiece[] DSP_pButterflies = new Board.MyPiece[64];

		private Color[] DSP_gemColors = new Color[]
		{
			new Color(255, 255, 255),
			new Color(192, 192, 192),
			new Color(32, 192, 32),
			new Color(224, 192, 32),
			new Color(255, 255, 255),
			new Color(255, 160, 32),
			new Color(255, 255, 255)
		};

		private Piece[] DP_pStdPieces = new Piece[128];

		private Piece[] DP_pShadowPieces = new Piece[128];

		private Piece[] DP_pQuestPieces = new Piece[128];

		public class DistortionQuad
		{
			public DistortionQuad()
			{
			}

			public DistortionQuad(float inX1, float inY1, float inX2, float inY2)
			{
				this.x1 = inX1;
				this.y1 = inY1;
				this.x2 = inX2;
				this.y2 = inY2;
			}

			public float x1;

			public float y1;

			public float x2;

			public float y2;
		}

		public enum BUTTON
		{
			BUTTON_HINT,
			BUTTON_MENU,
			BUTTON_RESET,
			BUTTON_REPLAY,
			BUTTON_QUEST_HELP,
			BUTTON_ZEN_OPTIONS
		}

		public enum EUIConfig
		{
			eUIConfig_Standard,
			eUIConfig_WithReset,
			eUIConfig_WithResetAndReplay,
			eUIConfig_Quest,
			eUIConfig_StandardNoReplay
		}

		private struct MyPiece
		{
			public Piece piece;

			public float alpha;

			public float scale;

			public int offsX;

			public int offsY;
		}

		public class DeferredSound
		{
			public int mId;

			public int mOnGameTick;

			public double mVolume;
		}
	}
}
