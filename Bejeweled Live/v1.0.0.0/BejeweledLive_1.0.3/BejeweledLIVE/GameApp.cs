using System;
using System.Collections.Generic;
using System.Globalization;
using Bejeweled3;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Sexy;

namespace BejeweledLIVE
{
	public class GameApp : SexyAppBase
	{
		public Image mCurBackdrop
		{
			get
			{
				if (!base.IsLandscape())
				{
					return GameConstants.BGV_TEXTURE;
				}
				return GameConstants.BGH_TEXTURE;
			}
		}

		public GameApp(Main m)
		{
			InterfaceParams[,] array = new InterfaceParams[27, 20];
			array[0, 0] = InterfaceParams.IN;
			array[1, 1] = InterfaceParams.IN;
			array[2, 2] = InterfaceParams.IN;
			array[2, 3] = InterfaceParams.IN;
			array[3, 2] = InterfaceParams.IN;
			array[3, 6] = InterfaceParams.IN;
			array[4, 4] = InterfaceParams.OFF;
			array[4, 6] = InterfaceParams.IN;
			array[5, 2] = InterfaceParams.IN;
			array[5, 12] = InterfaceParams.IN;
			array[6, 4] = InterfaceParams.OFF;
			array[6, 12] = InterfaceParams.IN;
			array[7, 2] = InterfaceParams.IN;
			array[7, 12] = InterfaceParams.IN;
			array[8, 4] = InterfaceParams.OFF;
			array[8, 12] = InterfaceParams.IN;
			array[9, 2] = InterfaceParams.IN;
			array[9, 7] = InterfaceParams.IN;
			array[10, 4] = InterfaceParams.OFF;
			array[10, 7] = InterfaceParams.IN;
			array[11, 2] = InterfaceParams.IN;
			array[11, 8] = InterfaceParams.IN;
			array[12, 4] = InterfaceParams.OFF;
			array[12, 8] = InterfaceParams.IN;
			array[13, 4] = InterfaceParams.IN;
			array[13, 5] = InterfaceParams.IN;
			array[14, 4] = InterfaceParams.OFF;
			array[14, 9] = InterfaceParams.IN;
			array[15, 2] = InterfaceParams.IN;
			array[15, 4] = InterfaceParams.OFF;
			array[15, 10] = InterfaceParams.IN;
			array[16, 4] = InterfaceParams.OFF;
			array[16, 11] = InterfaceParams.IN;
			array[17, 2] = InterfaceParams.IN;
			array[17, 13] = InterfaceParams.IN;
			array[19, 2] = InterfaceParams.IN;
			array[19, 15] = InterfaceParams.IN;
			array[20, 16] = InterfaceParams.IN;
			array[21, 17] = InterfaceParams.IN;
			array[22, 2] = InterfaceParams.IN;
			array[22, 18] = InterfaceParams.IN;
			array[23, 4] = InterfaceParams.OFF;
			array[23, 18] = InterfaceParams.IN;
			array[24, 2] = InterfaceParams.IN;
			array[24, 19] = InterfaceParams.IN;
			array[25, 4] = InterfaceParams.OFF;
			array[25, 19] = InterfaceParams.IN;
			array[26, 2] = InterfaceParams.IN;
			array[26, 4] = InterfaceParams.OFF;
			array[26, 7] = InterfaceParams.IN;
			this.gInterfaceStateParams = array;
			this.gWidgetZOrder = new InterfaceLayers[]
			{
				InterfaceLayers.UI_LAYER_LOADING,
				InterfaceLayers.UI_LAYER_LOADING,
				InterfaceLayers.UI_LAYER_BACKGROUND,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_BOARD,
				InterfaceLayers.UI_LAYER_BOARD_EFFECTS,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU,
				InterfaceLayers.UI_LAYER_MENU
			};
			this.mInterfaceWidgets = new InterfaceWidget[20];
			this.mFacebook = new FacebookInterface();
			this.mModeSeconds = new int[4];
			this.achievementAlerts = new Stack<TrialAchievementAlert>();
			base..ctor(m);
			SexyAppBase.UseLiveServers = true;
			SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.SignedInGamer_SignedIn);
			this.mNextVoiceId = (this.mCurVoiceId = -1);
			this.mBackgroundCacheImage = new MemoryImage();
			this.mBackgroundCacheImage.Create(Math.Max(this.mWidth, this.mHeight), Math.Max(this.mWidth, this.mHeight), PixelFormat.kPixelFormat_RGB565);
			Constants.mConstants = new Constants_480x800();
			AtlasResources.mAtlasResources = new AtlasResources_480x800();
			Constants.mLanguage = (Constants.LanguageIndex)0;
			Strings.Culture = CultureInfo.CurrentCulture;
			Debug.OutputDebug<string, string>("Phone Location:", Strings.Culture.TwoLetterISOLanguageName);
			if (Strings.Culture.TwoLetterISOLanguageName == "fr")
			{
				Constants.mLanguage = Constants.LanguageIndex.fr;
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "de")
			{
				Constants.mLanguage = Constants.LanguageIndex.de;
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "es")
			{
				Constants.mLanguage = Constants.LanguageIndex.es;
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "it")
			{
				Constants.mLanguage = Constants.LanguageIndex.it;
			}
			else
			{
				Constants.mLanguage = Constants.LanguageIndex.en;
			}
			Debug.OutputDebug<string, int>("Language Setting:", (int)Constants.mLanguage);
			GameApp.DIALOGBOX_EXTERIOR_INSETS = Constants.mConstants.GameApp_DIALOGBOX_EXTERIOR_INSETS;
			GameApp.DIALOGBOX_INTERIOR_INSETS = Constants.mConstants.GameApp_DIALOGBOX_INTERIOR_INSETS;
			GameApp.DIALOGBOX_CONTENT_INSETS = Constants.mConstants.GameApp_DIALOGBOX_CONTENT_INSETS;
			Dialog.YES_STRING = Strings.DIALOG_YES_STRING;
			Dialog.NO_STRING = Strings.DIALOG_NO_STRING;
			Dialog.OK_STRING = Strings.OK;
			Dialog.CANCEL_STRING = Strings.Cancel;
		}

		private void SignedInGamer_SignedIn(object sender, SignedInEventArgs e)
		{
			LeaderBoardComm.LoadInitialLeaderboard();
		}

		public override void InitHook()
		{
			this.mProfile = new Profile();
			Debug.OutputDebug<string, int>("total level count ", GameApp.NumberOfBackdrops);
			if ((this.mWidth == 480 && this.mHeight == 800) || (this.mWidth == 800 && this.mHeight == 480))
			{
				this.mResourceManager.ParseResourcesFile("resources_480x800.xml");
			}
			else if ((this.mWidth == 320 && this.mHeight == 480) || (this.mWidth == 320 && this.mHeight == 480))
			{
				this.mResourceManager.ParseResourcesFile("resources_320x480.xml");
			}
			switch (Constants.mLanguage)
			{
			case Constants.LanguageIndex.en:
				this.mResourceManager.ParseResourcesFile("sound_en.xml");
				break;
			case Constants.LanguageIndex.fr:
				this.mResourceManager.ParseResourcesFile("sound_fr.xml");
				break;
			case Constants.LanguageIndex.de:
				this.mResourceManager.ParseResourcesFile("sound_de.xml");
				break;
			case Constants.LanguageIndex.es:
				this.mResourceManager.ParseResourcesFile("sound_es.xml");
				break;
			case Constants.LanguageIndex.it:
				this.mResourceManager.ParseResourcesFile("sound_it.xml");
				break;
			}
			this.mNumExtrasLoaded = 0;
			try
			{
				if (!MediaPlayer.GameHasControl)
				{
					this.mProfile.mMusicVolume = (double)MediaPlayer.Volume;
				}
			}
			catch (Exception)
			{
			}
			this.SetSfxVolume(this.mProfile.mSFXVolume);
			base.SetMusicVolume(this.mProfile.mMusicVolume);
			if (!SexyAppBase.FirstRun)
			{
				Main.RunWhenLocked = this.mProfile.mRunWhenLocked;
			}
			this.mNormBonus = 2500;
			this.mNormBonusMult = 1000.0;
			this.mNormPMBase = 1.0;
			this.mNormPMMult = 0.5;
			this.mTimedBonus = 2000;
			this.mTimedBonusMult = 500.0;
			this.mTimedBonusAccel = 150.0;
			this.mTimedPMBase = 20.0;
			this.mTimedPMMult = 10.0;
			this.mEndlessBonus = 2500;
			this.mEndlessBonusMult = 1000.0;
			this.mEndlessPMBase = 1.0;
			this.mGraceSeconds = 0.1;
			this.mGraceSecondsMult = 0.002;
			this.mGraceSecondsMax = 0.5;
		}

		public override void SaveGame()
		{
			if (this.mBoard != null && ((this.mBoard.mState != Board.BoardState.STATE_GAME_OVER_DISPLAY) & (this.mBoard.mState != Board.BoardState.STATE_GAME_OVER_ANIM)) && this.mBoard.IsInProgress())
			{
				this.mBoard.SaveGame();
			}
			base.SaveGame();
		}

		public override bool BackButtonPress()
		{
			if (this.actionSheet != null)
			{
				return this.actionSheet.BackButtonPress();
			}
			if (this.achievementAlerts.Count > 0)
			{
				return this.achievementAlerts.Peek().BackButtonPress();
			}
			return base.BackButtonPress();
		}

		public override void LeftTrialMode()
		{
			base.LeftTrialMode();
			ReportAchievement.LeftTrialMode();
			if (this.mBoard != null)
			{
				if (this.mInterfaceState == InterfaceStates.UI_STATE_GAME)
				{
					this.mBoard.Unpause(true);
				}
				this.mBoard.LeftTrialmode();
			}
		}

		public void TryConfirmLoad()
		{
			this.actionSheet = ActionSheet.GetNewActionSheet(false, 9, this, this);
			this.actionSheet.SetText(Strings.Menu_Saved_Game_Prompt);
			this.actionSheet.AddButton(1000, Strings.Continue);
			this.actionSheet.SetDangerButton(1001, Strings.Menu_New_Game);
			this.actionSheet.SetCancelButton(1003, Strings.Cancel);
			this.actionSheet.Present();
		}

		private void ConfirmOverwriteSaveGame()
		{
			this.actionSheet = ActionSheet.GetNewActionSheet(false, 44, this, this);
			this.actionSheet.SetText(Strings.Confirm_New_Game);
			this.actionSheet.AddButton(1000, Strings.DIALOG_YES_STRING);
			this.actionSheet.SetDangerButton(1001, Strings.DIALOG_NO_STRING);
			this.actionSheet.Present();
		}

		public override void LoadingThreadProc()
		{
			Debug.OutputDebug<string>("start load thread...");
			GlobalStaticVars.mGlobalContent.LoadGameContent();
			this.mResourceManager.LoadAllResources();
			if (this.mResourceManager.HadError())
			{
				this.mLoadingFailed = true;
				return;
			}
			Resources.ExtractResources(this.mResourceManager, AtlasResources.mAtlasResources);
			if (this.mResourceManager.HadError())
			{
				this.mLoadingFailed = true;
				return;
			}
			this.PreAllocateMemory();
			GC.Collect();
		}

		private void PreAllocateMemory()
		{
			CurvedValDefinition.PreAllocateMemory();
			EffectBej3.PreAllocateMemory();
			PieceParticleEmitter.PreAllocateMemory();
			StaticParticleEmitter.PreAllocateMemory();
			SwapData.PreAllocateMemory();
			CurvedVal.PreAllocateMemory();
			MoveData.PreAllocateMemory();
			PieceBejLive.PreAllocateMemory();
			ColorCycle.PreAllocateMemory();
			LightningStorm.PreAllocateMemory();
			LightningZap.PreAllocateMemory();
			PointsBej3.PreAllocateMemory();
			MatchSet.PreAllocateMemory();
			EffectTypePair.PreAllocateMemory();
			ActionSheet.PreAllocateMemory();
			FancySmallButton.PreAllocateMemory();
			BoardBejLive.PreAllocateMemory();
			GameApp.LoadInitialLevelBackdrops();
			GC.Collect();
		}

		public static void LoadInitialLevelBackdrops()
		{
			GameConstants.LoadLevelBackdrops(1, 2);
		}

		public override void LoadingThreadCompleted()
		{
			base.LoadingThreadCompleted();
			Resources.LinkUpResArray();
			ReportAchievement.Initialise();
			this.mMainMenu = new MainMenu(this);
			this.mOptionsScreen = new OptionsDialog(this);
			this.mEffectOverlay = new EffectOverlay(this);
			this.mBoard = new BoardBejLive(this);
			this.mInterfaceWidgets[2] = new BackgroundWidget(this);
			this.mInterfaceWidgets[3] = this.mMainMenu;
			this.mInterfaceWidgets[4] = this.mBoard;
			this.mInterfaceWidgets[5] = this.mEffectOverlay;
			this.mInterfaceWidgets[6] = this.mOptionsScreen;
			this.mInterfaceWidgets[7] = new LiveLeaderboardDialog(this);
			this.mInterfaceWidgets[8] = null;
			this.mInterfaceWidgets[9] = new HighScoreNameDialog(this);
			this.mInterfaceWidgets[10] = new GameOverDialog(this);
			this.mInterfaceWidgets[11] = null;
			this.mInterfaceWidgets[12] = new HelpScreen(this);
			this.mInterfaceWidgets[13] = new MoreGamesDialog(this);
			this.mInterfaceWidgets[14] = null;
			this.mInterfaceWidgets[15] = new UpsellScreen(this);
			this.mInterfaceWidgets[16] = new ThankYouForBuyingScreen(this);
			this.mInterfaceWidgets[17] = null;
			this.mInterfaceWidgets[18] = new AchievementScreen(this);
			this.mInterfaceWidgets[19] = new Credits(this);
			for (int i = 0; i < 20; i++)
			{
				if (this.mInterfaceWidgets[i] != null)
				{
					this.mInterfaceWidgets[i].mZOrder = (int)this.gWidgetZOrder[i];
				}
			}
			if (!false)
			{
				this.GotoInterfaceState(2);
			}
			this.PrepareImageCache();
		}

		private void PrepareImageCache()
		{
			GlobalStaticVars.g.BeginFrame();
			GlobalStaticVars.g.SetColorizeImages(true);
			GlobalStaticVars.g.SetColor(new Color(0, 0, 0, 0));
			int num = 10183;
			for (int i = 10001; i < num; i++)
			{
				GlobalStaticVars.g.DrawImage(AtlasResources.GetImageInAtlasById(i), 0, 0);
			}
			GlobalStaticVars.g.EndFrame();
		}

		public void GotoInterfaceState(InterfaceStates state)
		{
			this.GotoInterfaceState((int)state);
		}

		public override void GotoInterfaceState(int state)
		{
			int num = (int)this.mInterfaceState;
			if (num != state)
			{
				this.mInterfaceState = (InterfaceStates)state;
				for (int i = 0; i < 20; i++)
				{
					InterfaceWidget interfaceWidget = this.mInterfaceWidgets[i];
					if (interfaceWidget != null)
					{
						int uiStateParam = (int)this.gInterfaceStateParams[state, i];
						interfaceWidget.InterfaceTransactionBegin(state, uiStateParam, (int)this.mInterfaceLayout);
					}
				}
				if (this.mInterfaceTransaction == 0)
				{
					for (int j = 0; j < 20; j++)
					{
						InterfaceWidget interfaceWidget2 = this.mInterfaceWidgets[j];
						if (interfaceWidget2 != null)
						{
							interfaceWidget2.InterfaceTransactionEnd(this.mInterfaceState);
						}
					}
				}
				bool flag = GameApp.gInterfaceStateStatusBar[state];
				bool flag2 = GameApp.gInterfaceStateStatusBar[num];
				if (flag != flag2)
				{
				}
				bool flag3 = GameApp.gInterfaceStateIdleTimer[state];
				bool flag4 = GameApp.gInterfaceStateIdleTimer[num];
			}
		}

		public bool WidgetIsInForState(int interfaceWidget, int interfaceState)
		{
			return 0 <= interfaceWidget && interfaceWidget < 20 && 0 <= interfaceState && interfaceState < 18 && InterfaceParams.OUT != this.gInterfaceStateParams[interfaceState, interfaceWidget];
		}

		internal override void NewGame()
		{
			if (!Board.HasSavedGame())
			{
				this.DoNewGame();
				return;
			}
			if (SexyAppBase.IsInTrialMode)
			{
				this.DialogButtonDepress(9, 1000);
				return;
			}
			this.TryConfirmLoad();
		}

		public override void BuyGame()
		{
			base.BuyGame();
		}

		public void DoNewGame()
		{
			if (this.mBoard != null)
			{
				this.mWidgetManager.RemoveWidget(this.mBoard);
				base.SafeDeleteWidget(this.mBoard);
			}
			this.mBoard = new BoardBejLive(this);
			this.mBoard.mZOrder = (int)this.gWidgetZOrder[4];
			this.mBoard.InitInterfaceState(this.mInterfaceState, 0, (int)this.mInterfaceLayout);
			this.mInterfaceWidgets[4] = this.mBoard;
			this.GotoInterfaceState(13);
			if (this.mGameMode == GameMode.MODE_PUZZLE)
			{
				this.mFirstPuzzleGame = false;
			}
		}

		public override void InterfaceOrientationChanged(UI_ORIENTATION toOrientation)
		{
			if (this.mIsOrientationLocked)
			{
				this.nextOrientation = new UI_ORIENTATION?(toOrientation);
				return;
			}
			if (base.OrientationIsLandscape(toOrientation))
			{
				this.mInterfaceLayout = InterfaceLayouts.UI_LAYOUT_LANDSCAPE;
			}
			else
			{
				this.mInterfaceLayout = InterfaceLayouts.UI_LAYOUT_PORTRAIT;
			}
			base.InterfaceOrientationChanged(toOrientation);
		}

		internal void InterfaceTransactionDown(InterfaceWidget iw)
		{
			if (--this.mInterfaceTransaction == 0)
			{
				for (int i = 0; i < 20; i++)
				{
					InterfaceWidget interfaceWidget = this.mInterfaceWidgets[i];
					if (interfaceWidget != null)
					{
						interfaceWidget.InterfaceTransactionEnd(this.mInterfaceState);
					}
				}
				base.LockOrientation(false);
			}
		}

		internal void InterfaceTransactionUp(InterfaceWidget iw)
		{
			if (this.mInterfaceTransaction++ == 0)
			{
				base.LockOrientation(true);
			}
		}

		public override void LostFocus()
		{
			base.LostFocus();
			if (this.mInterfaceState == InterfaceStates.UI_STATE_ACHIEVEMENTS_OVER_GAME || this.mInterfaceState == InterfaceStates.UI_STATE_CREDITS_OVER_GAME || this.mInterfaceState == InterfaceStates.UI_STATE_GAME || this.mInterfaceState == InterfaceStates.UI_STATE_HELP_OVER_GAME || this.mInterfaceState == InterfaceStates.UI_STATE_INFO_OVER_GAME || this.mInterfaceState == InterfaceStates.UI_STATE_SCORES_OVER_GAME)
			{
				this.SaveGame();
			}
			this.mProfile.SaveProfile();
			if (this.mBoard != null)
			{
				this.mBoard.Pause(true);
			}
		}

		public override void Tombstoned()
		{
			base.Tombstoned();
		}

		public void ConfirmResetHighScores()
		{
			this.actionSheet = ActionSheet.GetNewActionSheet(true, 23, this, this);
			this.actionSheet.SetDangerButton(1002, Strings.Reset_Scores);
			this.actionSheet.SetCancelButton(1003, Strings.Cancel);
			this.actionSheet.Present();
		}

		public bool FocusPaused()
		{
			return false;
		}

		public override void ModalOpen()
		{
		}

		public override void ModalClose()
		{
		}

		public bool CheatsEnabled()
		{
			return false;
		}

		public void DoGameOver(bool doHighScoreCheck)
		{
			this.mBoard.DeleteSavedGame();
			this.PlaySample(Resources.SOUND_GAME_OVER);
			this.GotoInterfaceState(InterfaceStates.UI_STATE_GAME_OVER);
		}

		public void PlayVoice(int theSoundId)
		{
			this.PlayVoice(theSoundId, 0, 1.0, -1);
		}

		public void PlayVoice(int theSoundId, int thePan, double theVolume, int theInterruptId)
		{
			if (this.mNextVoice != null)
			{
				this.mNextVoice.Release();
				this.mNextVoiceId = -1;
			}
			this.mNextVoice = this.mSoundManager.GetSoundInstance((uint)theSoundId);
			if (this.mNextVoice != null)
			{
				this.mNextVoice.SetVolume(theVolume);
				this.mNextVoice.SetPan(thePan);
				this.mInterruptCurVoice = this.mCurVoiceId == theInterruptId || (this.mNextVoiceId == theInterruptId && this.mInterruptCurVoice) || theInterruptId == -2;
				this.mNextVoiceId = theSoundId;
			}
		}

		public void UpdateVoices()
		{
			if (this.mCurVoice != null && this.mCurVoice.IsReleased())
			{
				this.mCurVoiceId = -1;
				this.mCurVoice = null;
			}
			if (this.mCurVoice != null)
			{
				double num = this.mCurVoice.GetVolume();
				if (this.mNextVoice != null && this.mInterruptCurVoice)
				{
					num = Math.Max(0.0, num - 0.05000000074505806);
					this.mCurVoice.SetVolume(num);
				}
				if (!this.mCurVoice.IsPlaying() || num == 0.0)
				{
					this.mCurVoice = null;
					this.mCurVoiceId = -1;
				}
			}
			if (this.mCurVoice == null && this.mNextVoice != null)
			{
				this.mCurVoice = this.mNextVoice;
				this.mCurVoiceId = this.mNextVoiceId;
				this.mNextVoice = null;
				this.mNextVoiceId = -1;
				this.mInterruptCurVoice = false;
				this.mCurVoice.Play(false);
			}
		}

		public override void DrawGame(GameTime gameTime)
		{
			if (!this.mLoadingThreadCompleted)
			{
				GlobalStaticVars.mGlobalContent.DrawSplashScreen();
				return;
			}
			base.DrawGame(gameTime);
		}

		protected override bool ShowRunWhenLockedMessage()
		{
			if (Guide.IsVisible)
			{
				return false;
			}
			Guide.BeginShowMessageBox(Strings.Allow_Run_When_Locked_Heading, Strings.Allow_Run_When_Locked, new string[]
			{
				Strings.DIALOG_YES_STRING,
				Strings.DIALOG_NO_STRING
			}, 0, 0, new AsyncCallback(this.RunWhenLockedMessageClosed), null);
			return true;
		}

		private void RunWhenLockedMessageClosed(IAsyncResult result)
		{
			int? num = Guide.EndShowMessageBox(result);
			Main.RunWhenLocked = (this.mProfile.mRunWhenLocked = num == 0);
		}

		public override void UpdateFrames()
		{
			if (this.wantToShowUpdateMessage)
			{
				this.ShowUpdateMessage();
			}
			if (!this.mLoadingThreadCompleted)
			{
				return;
			}
			this.UpdateVoices();
			base.UpdateFrames();
		}

		public override void SetSfxVolume(double theVolume)
		{
			base.SetSfxVolume(theVolume);
		}

		public override void PlaySample(int theSoundNum)
		{
			this.PlaySample(theSoundNum, 0);
		}

		public override void PlaySample(int theSoundNum, int thePan)
		{
			if (this.mSoundManager == null)
			{
				return;
			}
			SoundInstance soundInstance = this.mSoundManager.GetSoundInstance((uint)theSoundNum);
			if (soundInstance != null)
			{
				if (thePan != 0)
				{
					soundInstance.SetPan(thePan);
				}
				soundInstance.Play(false);
			}
		}

		public override void DialogButtonPress(int dialogId, int buttonId)
		{
			this.PlaySample(Resources.SOUND_CLICK);
		}

		public void ShowAchievementMessage(TrialAchievementAlert achievementAlert)
		{
			this.mBoard.Pause(true);
			achievementAlert.Present();
			this.achievementAlerts.Push(achievementAlert);
		}

		public override void ShowUpdateRequiredMessage()
		{
			this.wantToShowUpdateMessage = true;
			Main.GamerServicesComp.Enabled = false;
		}

		private void ShowUpdateMessage()
		{
			SexyAppBase.UseLiveServers = false;
			if (Guide.IsVisible)
			{
				return;
			}
			Guide.BeginShowMessageBox(Strings.Update, Strings.Update_Required, new string[]
			{
				Strings.DIALOG_YES_STRING,
				Strings.DIALOG_NO_STRING
			}, 0, 0, new AsyncCallback(this.GameUpdateMessageClosed), null);
			this.wantToShowUpdateMessage = false;
		}

		private void GameUpdateMessageClosed(IAsyncResult result)
		{
			bool flag = Guide.EndShowMessageBox(result) == 0;
			if (!flag)
			{
				SexyAppBase.UseLiveServers = false;
				return;
			}
			if (Main.IsInTrialMode)
			{
				Guide.ShowMarketplace(0);
				return;
			}
			new MarketplaceDetailTask
			{
				ContentType = 1
			}.Show();
		}

		public override void DialogButtonDepress(int dialogId, int buttonId)
		{
			this.actionSheet = null;
			if (this.achievementAlerts.Count > 0)
			{
				this.achievementAlerts.Pop();
			}
			if (dialogId <= 23)
			{
				if (dialogId == 9)
				{
					if (1003 != buttonId)
					{
						if (1001 == buttonId)
						{
							this.ConfirmOverwriteSaveGame();
						}
						else
						{
							this.DoNewGame();
							this.mBoard.LoadGame();
						}
					}
					base.PlaySong(SongType.MainMenu, true);
					return;
				}
				if (dialogId != 23)
				{
					return;
				}
				if (1002 == buttonId)
				{
					this.mProfile.ResetHighScores();
					return;
				}
			}
			else if (dialogId != 36)
			{
				switch (dialogId)
				{
				case 43:
					this.mBoard.Unpause(true);
					switch (buttonId)
					{
					case 1000:
						this.BuyGame();
						return;
					case 1001:
						break;
					default:
						return;
					}
					break;
				case 44:
					if (1000 == buttonId)
					{
						this.DoNewGame();
						return;
					}
					this.TryConfirmLoad();
					return;
				case 45:
					switch (buttonId)
					{
					case 1000:
						Guide.ShowMarketplace(0);
						return;
					case 1001:
						SexyAppBase.UseLiveServers = false;
						break;
					default:
						return;
					}
					break;
				default:
					return;
				}
			}
			else
			{
				if (1002 == buttonId)
				{
					this.GotoInterfaceState(InterfaceStates.UI_STATE_LEADERBOARD);
					return;
				}
				if (InterfaceStates.UI_STATE_BLITZ_INTRO == this.mInterfaceState)
				{
					this.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
					return;
				}
			}
		}

		public override void AppExit()
		{
			base.AppExit();
			if (SexyAppBase.IsInTrialMode && this.mBoard != null)
			{
				this.mBoard.DeleteAllSavedGames();
			}
			this.mProfile.Dispose();
		}

		// Note: this type is marked as 'beforefieldinit'.
		static GameApp()
		{
			bool[] array = new bool[27];
			array[3] = true;
			array[4] = true;
			GameApp.gInterfaceStateStatusBar = array;
			GameApp.gInterfaceStateIdleTimer = new bool[]
			{
				true, true, true, true, true, true, true, true, true, true,
				true, true, true, false, true, true, true, true, true, true,
				true, true, true, true, true, true, true
			};
		}

		public static string AppVersionNumber = "1.0.3";

		public static int NumberOfBackdrops;

		public static Insets DIALOGBOX_EXTERIOR_INSETS;

		public static Insets DIALOGBOX_INTERIOR_INSETS;

		public static Insets DIALOGBOX_CONTENT_INSETS;

		internal readonly InterfaceParams[,] gInterfaceStateParams;

		internal readonly InterfaceLayers[] gWidgetZOrder;

		internal static bool[] gInterfaceStateStatusBar;

		internal static bool[] gInterfaceStateIdleTimer;

		private SoundInstance mCurVoice;

		private SoundInstance mNextVoice;

		private bool mInterruptCurVoice;

		public int mCurVoiceId;

		public int mNextVoiceId;

		public MemoryImage mTextEffectsImage;

		public InterfaceStates mInterfaceState;

		public InterfaceLayouts mInterfaceLayout;

		public InterfaceWidget[] mInterfaceWidgets;

		public int mInterfaceTransaction;

		public int mEndlessBonus;

		public MainMenu mMainMenu;

		public OptionsDialog mOptionsScreen;

		public Board mBoard;

		public GameMode mGameMode;

		public bool mFirstPuzzleGame;

		public Profile mProfile;

		public int mHighScorePos;

		public MemoryImage mBackgroundCacheImage;

		public FacebookInterface mFacebook;

		public EffectOverlay mEffectOverlay;

		public bool mShowUpsell;

		public int mNormBonus;

		public double mNormBonusMult;

		public double mNormPMBase;

		public double mNormPMMult;

		public int mGamesPlayed;

		public int mFramesPlayed;

		public bool mDoFocusPause;

		public bool mWaitingForPuzzles;

		public int mLoadFailCount;

		public bool mFocusPaused;

		public double mEndlessBonusMult;

		public double mEndlessPMBase;

		public int mTimedBonus;

		public double mTimedBonusMult;

		public double mTimedBonusAccel;

		public double mTimedPMBase;

		public double mTimedPMMult;

		public double mTimedPVBase;

		public double mTimedPVMult;

		public double mTimedPABase;

		public double mTimedPAMult;

		public double mGraceSeconds;

		public double mGraceSecondsMult;

		public double mGraceSecondsMax;

		public int[] mModeSeconds;

		public int mNumExtrasLoaded;

		private ActionSheet actionSheet;

		private Stack<TrialAchievementAlert> achievementAlerts;

		private bool wantToShowUpdateMessage;
	}
}
