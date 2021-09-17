using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using JeffLib;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using SexyFramework;
using SexyFramework.AELib;
using SexyFramework.Drivers.App;
using SexyFramework.Drivers.File;
using SexyFramework.Drivers.Profile;
using SexyFramework.File;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;
using ZumasRevenge.Profile;
using ZumasRevenge.Sound;

namespace ZumasRevenge
{
	public class GameApp : SexyApp, NewUserDialogListener, ProfileEventListener
	{
		protected void PreShowLoadingScreen()
		{
			if (!this.mResourceManager.IsGroupLoaded("LoadScreen"))
			{
				this.mResourceManager.LoadResources("LoadScreen");
			}
			this.mResourceManager.LoadImage("ATLASIMAGE_ATLAS_GAMEPLAY_640_00");
			this.mResourceManager.LoadImage("ATLASIMAGE_ATLAS_MENURELATED_640_00");
			this.mMusic.LoadMusic(1, "music/MUSIC_LOADING");
			this.mMusic.LoadMusic(0, "music/MUSIC_HAWAIIAN");
			this.mMusic.Enable(true);
			this.StartLoading();
		}

		public void ShowLoadingScreen()
		{
			this.mLoadingScreen = new LoadingScreen();
			this.mLoadingScreen.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mLoadingScreen);
			if (this.mMusic.IsUserMusicPlaying())
			{
				this.mLoadingScreen.ProcessBGM();
			}
			this.mUnderDialogWidget.CreateImages();
			this.mUnderDialogWidget.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mUnderDialogWidget);
			this.mUnderDialogWidget.SetVisible(false);
		}

		protected void LoadingScreenCallback()
		{
			this.mWidgetManager.BringToFront(this.mLoadingScreen);
		}

		protected void SetupMainMenuDefaults(bool do_load_thread)
		{
			if (do_load_thread)
			{
				this.mLoadType = 4;
				this.mStartInGameModeThreadProcRunning = true;
				int num = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(700));
				int num2 = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(650));
				Ratio aspectRatio = this.mGraphicsDriver.GetAspectRatio();
				if (aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3)
				{
					ZumasRevenge.Common._DS(ZumasRevenge.Common._M(160));
				}
				this.StartMMThreadProc();
				this.DoCommonInGameLoadThread(new Rect((this.mWidth - num) / 2, (this.mHeight - num2) / 2, num, num2));
				this.mReturnToMMDlg = null;
			}
			else
			{
				this.StartMMThreadProc();
			}
			this.mWidgetManager.AddWidget(this.mMainMenu);
			this.ClearUpdateBacklog(true);
		}

		protected void SetupMainMenuDefaults()
		{
			this.SetupMainMenuDefaults(true);
		}

		protected void DoCommonInGameLoadThread(Rect aRect)
		{
			this.mLoadRect = aRect;
			bool flag = false;
			while (this.mStartInGameModeThreadProcRunning)
			{
				this.UpdateAppStep(ref flag);
				SexyFramework.Common.SexySleep(0);
			}
			this.mWidgetManager.MarkAllDirty();
			if (this.mInGameLoadThreadProcFailed)
			{
				this.Popup("There was an error initializing the game.");
				this.mBoard.Dispose();
				this.mBoard = null;
				for (int i = 0; i < this.mNormalLevelMgr.mLevels.size<Level>(); i++)
				{
					this.mNormalLevelMgr.mLevels[i].mBoard = null;
				}
				if (this.mWidescreenBoardWidget != null)
				{
					this.mWidgetManager.RemoveWidget(this.mWidescreenBoardWidget);
					base.SafeDeleteWidget(this.mWidescreenBoardWidget);
					this.mWidescreenBoardWidget = null;
				}
				this.Shutdown();
				return;
			}
			if (this.mLoadType != 4)
			{
				this.mWidgetManager.AddWidget(this.mBoard);
				this.mWidgetManager.SetFocus(this.mBoard);
				if (this.mWidescreenBoardWidget == null)
				{
					this.mWidescreenBoardWidget = new WidescreenBoardWidget();
					this.mWidescreenBoardWidget.Resize(ZumasRevenge.Common._S(-80), 0, this.mWidth + ZumasRevenge.Common._S(160), this.mHeight);
					this.mWidgetManager.AddWidget(this.mWidescreenBoardWidget);
				}
			}
		}

		public GameApp(Game xnaGame, bool from_reinit)
		{
			GameApp.gApp = this;
			this.mGameMain = xnaGame;
			((WP7AppDriver)this.mAppDriver).InitXNADriver(xnaGame);
			this.SetBoolean("drivers.ios.use_gles20", true);
			this.SetBoolean("drivers.ios.use_multitouch", false);
			this.SetInteger("compat_AppOrigScreenWidth", this.mOrigScreenWidth);
			this.SetInteger("compat_AppOrigScreenHeight", this.mOrigScreenHeight);
			this.mSavingOrLoadingProfile = false;
			this.mWideScreenXOffset = 0;
			this.mUpsell = null;
			this.mDoingDRM = false;
			this.mTrialType = 0;
			this.mShotCorrectionAngleToWidthDist = 1500f;
			this.mShotCorrectionAngleMax = 13f;
			this.mShotCorrectionWidthMax = 65f;
			this.mGuideStyle = 1;
			this.mShotCorrectionDebugStyle = 3;
			this.mIronFrogModeIncluded = false;
			this.mGenericHelp = null;
			this.mLegalInfo = null;
			this.mAboutInfo = null;
			this.mProdName = "ZumasRevenge";
			this.mRegKey = "PopCap\\ZumasRevenge";
			this.mLevelXML = "levels/levels";
			this.mHardLevelXML = "levels/levels_hard";
			this.mBoard = null;
			this.mDebugKeysEnabled = false;
			this.mAllowSwapScreenImage = false;
			this.mLoadType = -1;
			this.mCredits = null;
			this.mIFLoadingAnimStartCel = 0;
			this.mDelayIntro = false;
			this.mReturnToMMDlg = null;
			this.mDoingAdvModeLoad = false;
			this.mConfTime = 1500;
			GameApp.mGameRes = 640;
			this.mHiRes = false;
			this.mWidescreenAware = true;
			this.mWidescreenTranslate = true;
			this.mAllowWindowResize = true;
			this.mReInit = false;
			this.mFromReInit = from_reinit;
			this.mMapScreen = null;
			this.mMapScreenHackWidget = null;
			this.mInGameLoadThreadProcFailed = false;
			this.mForceZoneRestart = -1;
			this.mStartInGameModeThreadProcRunning = false;
			this.mClickedHardMode = false;
			this.mContinuedGame = false;
			GameApp.gNeedsPreCache = true;
			this.mAutoMonkey = null;
			GameApp.initResolution(640);
			this.mAutoStartLoadingThread = false;
			this.mLoadingScreen = null;
			this.mFramesPlayed = 0;
			this.mAutoEnable3D = true;
			this.mNoVSync = true;
			this.mCachedLoadState = 0;
			this.mCachedLoad = false;
			this.mNormalLevelMgr = null;
			this.mCustomCursorsEnabled = true;
			this.mCursorTarget = true;
			this.mColorblind = false;
			this.mUserProfile = null;
			this.mProfileMgr = null;
			this.mMainMenu = null;
			this.mMoreGames = null;
			this.mNewUserDlg = null;
			this.mUnderDialogWidget = new UnderDialogWidget();
			this.mDialogObscurePct = 0f;
			this.mFullscreenBits = 32;
			this.mInitialLoad = true;
			GameApp.gDDS = new DDS();
			GameApp.gDDS.mMinLevel = int.MaxValue;
			this.mBambooTransition = null;
			this.mProductVersion = this.GetProductVersion("");
			if (!this.mFileDriver.InitFileDriver(this))
			{
				this.Shutdown();
			}
		}

		public string GetProductVersion(string thePath)
		{
			string fullName = Assembly.GetCallingAssembly().FullName;
			string text = "v" + fullName.Split(new char[] { '=' })[1].Split(new char[] { ',' })[0];
			return text.Substring(0, text.Length - 2);
		}

		public override void Dispose()
		{
			if (this.mSoundManager != null)
			{
				this.mSoundManager.ReleaseChannels();
				this.mSoundManager.ReleaseSounds();
			}
			if (this.mGenericHelp != null)
			{
				this.KillDialog(this.mGenericHelp);
				this.mGenericHelp = null;
			}
			if (this.mLegalInfo != null)
			{
				this.KillDialog(this.mLegalInfo);
				this.mLegalInfo = null;
			}
			if (this.mAboutInfo != null)
			{
				this.KillDialog(this.mAboutInfo);
				this.mAboutInfo = null;
			}
			if (this.mBambooTransition != null)
			{
				this.mWidgetManager.RemoveWidget(this.mBambooTransition);
				this.mBambooTransition = null;
			}
			if (this.mUpsell != null)
			{
				this.mWidgetManager.RemoveWidget(this.mUpsell);
				this.mUpsell = null;
			}
			if (this.gCreditsHackWidget != null)
			{
				this.mWidgetManager.RemoveWidget(this.gCreditsHackWidget);
			}
			this.gCreditsHackWidget = null;
			this.mWidgetManager.RemoveWidget(this.mUnderDialogWidget);
			this.mUnderDialogWidget = null;
			this.mCredits = null;
			Ball.DeleteBallGlobals();
			if (this.mBoard != null)
			{
				if (this.mBoard.NeedSaveGame() && this.mUserProfile != null)
				{
					this.mBoard.SaveGame(this.mUserProfile.GetSaveGameName(this.IsHardMode()), null);
				}
				this.mWidgetManager.RemoveWidget(this.mBoard);
			}
			this.mReturnToMMDlg = null;
			this.mProxBombManager = null;
			this.mLevelThumbnails.Clear();
			this.mMusic = null;
			this.mSoundPlayer = null;
			this.mBoard = null;
			if (this.mNormalLevelMgr != null)
			{
				for (int i = 0; i < this.mNormalLevelMgr.mLevels.size<Level>(); i++)
				{
					this.mNormalLevelMgr.mLevels[i].mBoard = null;
				}
			}
			if (this.mMapScreen != null)
			{
				this.mMapScreen.CleanButtons();
			}
			if (this.mMapScreenHackWidget != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMapScreenHackWidget);
			}
			this.mMapScreenHackWidget = null;
			this.mMapScreen = null;
			if (this.mMainMenu != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMainMenu);
			}
			this.mMainMenu = null;
			if (this.mMoreGames != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMoreGames);
			}
			this.mMoreGames = null;
			if (this.mLoadingScreen != null)
			{
				this.mWidgetManager.RemoveWidget(this.mLoadingScreen);
			}
			this.mLoadingScreen = null;
			this.mNormalLevelMgr = null;
			if (this.mNewUserDlg != null)
			{
				this.KillDialog(this.mNewUserDlg.mId, true, false);
			}
			this.mNewUserDlg = null;
			GameApp.gDDS = null;
			for (int j = 0; j < this.mCachedTorchEffects.size<CachedTorchEffect>(); j++)
			{
				this.mCachedTorchEffects[j].mTorchFlame = null;
				this.mCachedTorchEffects[j].mTorchFlameOut = null;
			}
			for (int k = 0; k < this.mCachedVolcanoEffects.size<CachedVolcanoEffect>(); k++)
			{
				this.mCachedVolcanoEffects[k].mExplosion = null;
				this.mCachedVolcanoEffects[k].mProjectile = null;
			}
			this.mResourceManager.DeleteResources("");
			this.mProfileMgr = null;
			this.RegistryWriteBoolean("LastShutdownOK", true);
		}

		public bool IsWideScreen()
		{
			return true;
		}

		public int GetWideScreenAdjusted(int x)
		{
			return x;
		}

		public int GetWidthAdjusted(int x)
		{
			return x - ZumasRevenge.Common._DS(125);
		}

		public bool LoadMoreGamesInfo()
		{
			Buffer buffer = new Buffer();
			if (base.ReadBufferFromFile(SexyFramework.Common.GetAppDataFolder() + "users/mg.dat", ref buffer))
			{
				this.mLastMoreGamesUpdate = buffer.ReadLong();
				return true;
			}
			return false;
		}

		public void SaveMoreGamesInfo()
		{
		}

		public void ConsoleCallback(string cmd, List<string> _params)
		{
		}

		public void SaveProfile()
		{
			if (!this.mSavingOrLoadingProfile && this.mUserProfile != null)
			{
				this.mSavingOrLoadingProfile = true;
				this.mUserProfile.SaveDetails();
				this.mSavingOrLoadingProfile = false;
			}
		}

		public bool HasSaveGame()
		{
			if (this.mUserProfile == null)
			{
				return false;
			}
			string saveGameName = this.mUserProfile.GetSaveGameName(this.IsHardMode());
			return StorageFile.FileExists(saveGameName);
		}

		public void HandleCrash(bool from_assert)
		{
		}

		public void SaveGlobalConfig()
		{
			Buffer buffer = new Buffer();
			buffer.WriteDouble(this.mMusicVolume);
			buffer.WriteDouble(this.mSfxVolume);
			buffer.WriteBoolean(this.mColorblind);
			StorageFile.WriteBufferToFile("users/OptionConfig.sav", buffer);
		}

		public void LoadGlobalConfig()
		{
			Buffer buffer = new Buffer();
			if (StorageFile.ReadBufferFromFile("users/OptionConfig.sav", buffer))
			{
				this.mMusicVolume = buffer.ReadDouble();
				this.mSfxVolume = buffer.ReadDouble();
				this.mColorblind = buffer.ReadBoolean();
				this.SetMusicVolume(this.mMusicVolume);
				this.SetSfxVolume(this.mSfxVolume);
			}
		}

		public void RevertOptionsChanges()
		{
			bool theValue = false;
			bool is3d = false;
			bool wantWindowed = false;
			this.RegistryReadBoolean("PreHiRes", ref theValue);
			this.RegistryReadBoolean("Pre3D", ref is3d);
			this.RegistryReadBoolean("PreWindowed", ref wantWindowed);
			this.RegistryWriteBoolean("NeedsConfirmation", false);
			this.SwitchScreenMode(wantWindowed, is3d, true);
			this.mPreferredWidth = (this.mPreferredHeight = -1);
			this.RegistryWriteBoolean("HiRes", theValue);
			this.mReInit = true;
			this.Shutdown();
		}

		public void InGameLoadThread_DrawFunc()
		{
			GameApp.InGameLoadThread_DrawFunc_CallCounter++;
			Font fontByName = this.GetFontByName("FONT_SHAGLOUNGE38_STROKE");
			Image imageByName = this.GetImageByName("IMAGE_BLUE_BALL");
			Graphics graphics = new Graphics(this.mWidgetManager.mImage);
			string text = ((this.mLoadType == 4) ? TextManager.getInstance().getString(726) : TextManager.getInstance().getString(581));
			string text2 = "";
			int num = GameApp.InGameLoadThread_DrawFunc_CallCounter % 40;
			if (num >= 30)
			{
				text2 = "...";
			}
			else if (num >= 20)
			{
				text2 = "..";
			}
			else if (num >= 10)
			{
				text2 = ".";
			}
			text += text2;
			if (this.mLoadType == 1 || this.mLoadType == 0)
			{
				Ratio aspectRatio = this.mGraphicsDriver.GetAspectRatio();
				int num2 = ((aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3) ? ZumasRevenge.Common._DS(ZumasRevenge.Common._M(160)) : 0);
				if (this.mLoadType == 1)
				{
					int num3 = ((aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3) ? ZumasRevenge.Common._DS(ZumasRevenge.Common._M(80)) : 0);
					int num4 = this.mLoadRect.mX + (this.mLoadRect.mWidth - fontByName.StringWidth("Loading...")) / 2;
					num4 += num3;
					int num5 = this.mLoadRect.mY + (this.mLoadRect.mHeight - fontByName.mHeight) / 2 + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(50));
					graphics.SetColor(250, 124, 0);
					graphics.SetFont(fontByName);
					graphics.DrawString(text, num4, num5);
					return;
				}
				if (this.mLoadType == 0)
				{
					int num4 = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(656)) + (ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(330)) - fontByName.StringWidth("Loading...")) / 2 - 2;
					num4 += num2;
					int num5 = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(697)) + (ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(500)) - fontByName.mHeight) / 2 - 2;
					graphics.SetColor(Color.White);
					graphics.SetFont(fontByName);
					graphics.DrawString(text, num4 + 2, num5 + graphics.GetFont().GetAscent() + 2);
					return;
				}
			}
			else
			{
				if (this.mLoadType == 2)
				{
					graphics.SetFont(fontByName);
					graphics.SetColor(250, 124, 0);
					graphics.DrawString(text, this.mLoadRect.mX + (this.mLoadRect.mWidth - graphics.GetFont().StringWidth("Loading...")) / 2, this.mLoadRect.mY + (this.mLoadRect.mHeight - graphics.GetFont().mHeight) / 2 + graphics.GetFont().GetAscent());
					return;
				}
				if (this.mLoadType == 3)
				{
					graphics.SetFont(fontByName);
					graphics.SetColor(Color.White);
					return;
				}
				if (this.mLoadType == 4)
				{
					graphics.Translate(this.mReturnToMMDlg.mX, this.mReturnToMMDlg.mY);
					this.mReturnToMMDlg.Draw(graphics);
					graphics.SetFont(fontByName);
					graphics.SetColor(250, 124, 0);
					graphics.DrawString(text, (this.mLoadRect.mWidth - graphics.GetFont().StringWidth("Returning to Menu...")) / 2 + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(20)), (this.mLoadRect.mHeight - graphics.GetFont().mHeight) / 2 - ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(30)) + graphics.GetFont().GetAscent());
					int theY = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(400));
					int num6 = this.mLoadRect.mWidth - imageByName.GetCelWidth() * 4 - ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-100));
					int num7 = num6 / 4;
					Image[] array = new Image[]
					{
						this.GetImageByName("IMAGE_BLUE_BALL"),
						this.GetImageByName("IMAGE_RED_BALL"),
						this.GetImageByName("IMAGE_YELLOW_BALL"),
						this.GetImageByName("IMAGE_GREEN_BALL")
					};
					int[] array2 = new int[]
					{
						SexyFramework.Common.Rand(50),
						SexyFramework.Common.Rand(50),
						SexyFramework.Common.Rand(50),
						SexyFramework.Common.Rand(50)
					};
					for (int i = 0; i < 4; i++)
					{
						int num8 = array[i].mNumCols * array[i].mNumRows;
						int num9 = (array2[i] + GameApp.InGameLoadThread_DrawFunc_CallCounter) % num8;
						if (num9 < 0)
						{
							num9 = -num9;
						}
						else if (num9 >= num8)
						{
							num9 = num8 - 1;
						}
						Rect theSrcRect = new Rect(array[i].GetCelRect(num9));
						graphics.DrawImageRotated(array[i], num7 + num6 / 4 * i, theY, -1.5707963705062866, theSrcRect);
					}
				}
			}
		}

		public void StartAdvModeThreadProc()
		{
			this.mBoard = new Board(this, -1);
			this.mBoard.mAdventureMode = true;
			this.mBoard.mIsHardMode = this.mClickedHardMode;
			if (!this.mBoard.Init())
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			this.mContinuedGame = false;
			if (this.HasSaveGame() && this.mForceZoneRestart == -1)
			{
				if (!this.mBoard.LoadGame(this.mUserProfile.GetSaveGameName(this.IsHardMode())))
				{
					StorageFile.DeleteFile(this.mUserProfile.GetSaveGameName(this.IsHardMode()));
					this.mUserProfile.ClearAdventureModeDetails();
				}
				else
				{
					this.mContinuedGame = true;
				}
			}
			else
			{
				this.PlaySong(12);
				if (this.mForceZoneRestart != -1)
				{
					this.mBoard.RestartFromZone(this.mForceZoneRestart);
				}
				else if (!this.mBoard.StartLevel(1))
				{
					this.mInGameLoadThreadProcFailed = true;
					this.mStartInGameModeThreadProcRunning = false;
					return;
				}
			}
			this.mBoard.MakeCachedBackground();
			this.mInGameLoadThreadProcFailed = false;
			this.mForceZoneRestart = -1;
			this.mStartInGameModeThreadProcRunning = false;
			this.mAutoMonkey.SetState(MonkeyState.Playing);
		}

		public void StartChallengeModeThreadProc()
		{
			this.mBoard = new Board(this, this.mNormalLevelMgr.GetStartingGauntletLevel(this.mChallengeLevelId));
			if (!this.mBoard.Init())
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			if (!this.mBoard.StartLevel(this.mChallengeLevelId))
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mInGameLoadThreadProcFailed = false;
			this.mStartInGameModeThreadProcRunning = false;
			this.mAutoMonkey.SetState(MonkeyState.Playing);
		}

		public void StartIronFrogModeThreadProc()
		{
			this.mBoard = new Board(this, -1);
			if (!this.mBoard.Init())
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			if (!this.mBoard.StartLevel(this.mNormalLevelMgr.GetFirstIronFrogLevel() + 1))
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mInGameLoadThreadProcFailed = false;
			this.mStartInGameModeThreadProcRunning = false;
		}

		public void StartMMThreadProc()
		{
			if (!this.mResourceManager.IsGroupLoaded("MenuRelated") && !this.mResourceManager.LoadResources("MenuRelated"))
			{
				this.mStartInGameModeThreadProcRunning = false;
				this.mInGameLoadThreadProcFailed = true;
				return;
			}
			if (this.mResourceManager.IsGroupLoaded("GrottoSounds"))
			{
				this.mResourceManager.DeleteResources("GrottoSounds");
			}
			if (this.mResourceManager.IsGroupLoaded("Boss6Common"))
			{
				this.mResourceManager.DeleteResources("Boss6Common");
			}
			this.mMainMenu = new MainMenu(this);
			this.mMainMenu.Init();
			this.mMainMenu.Resize(this.GetScreenRect());
			this.mInGameLoadThreadProcFailed = false;
			this.mStartInGameModeThreadProcRunning = false;
		}

		public void DoUpsell(bool from_exit)
		{
		}

		public bool IsRegistered()
		{
			return true;
		}

		public bool IsSafeForLockout()
		{
			return !this.mLoadingThreadStarted || this.mLoadingThreadCompleted;
		}

		public void DoLockout()
		{
			this.mDoingDRM = true;
			if (this.mBoard != null)
			{
				this.mBoard.DoShutdownSaveGame();
			}
		}

		public void DoCredits(bool isFromMainMenu)
		{
			if (!this.mResourceManager.IsGroupLoaded("Credits") && !this.mResourceManager.LoadResources("Credits"))
			{
				this.ShowResourceError(true);
				this.Shutdown();
				return;
			}
			this.mCredits = new Credits(isFromMainMenu);
			this.mCredits.Init(this.mBoard != null && !this.mBoard.IsHardAdventureMode());
			this.gCreditsHackWidget = new CreditsHackWidget();
			this.gCreditsHackWidget.Resize(0, 0, this.mWidth, this.mHeight);
			this.gCreditsHackWidget.mClip = false;
			this.mWidgetManager.AddWidget(this.gCreditsHackWidget);
			if (!isFromMainMenu)
			{
				this.EndCurrentGame();
			}
		}

		public void ReturnFromCredits()
		{
			if (!this.mCredits.mFromMainMenu)
			{
				this.ShowMainMenu();
			}
			this.mWidgetManager.RemoveWidget(this.gCreditsHackWidget);
			base.SafeDeleteWidget(this.gCreditsHackWidget);
			this.mCredits.Dispose();
			this.mCredits = null;
			this.gCreditsHackWidget = null;
		}

		public void GenericHelpClosed()
		{
			this.mGenericHelp = null;
		}

		public void SetStat(string stat_name, int val)
		{
		}

		public int GetStat(string stat_name)
		{
			return 0;
		}

		public void SetAchievement(string achievement_name)
		{
		}

		public void ResetAchievements()
		{
		}

		public void RehupAchievements()
		{
		}

		public virtual void ConvertResources()
		{
		}

		public virtual void ConvertLevels()
		{
		}

		public void OnHardwareBackButtonPressed()
		{
			GlobalMembers.IsBackButtonPressed = true;
		}

		public void OnHardwareBackButtonPressProcessed()
		{
			GlobalMembers.IsBackButtonPressed = false;
		}

		public void OnExiting()
		{
			if (this.mBoard != null)
			{
				this.mBoard.ProcessExitingEvent();
			}
		}

		public void OnDeactivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.PauseAllMusic();
				this.mMusicInterface.OnDeactived();
			}
			if (this.mBoard != null)
			{
				this.mBoard.ProcessOnDeactiveEvent();
			}
		}

		public void OnActivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.OnActived();
				this.mMusicInterface.ResumeAllMusic();
			}
			GameApp.USE_TRIAL_VERSION = Guide.IsTrialMode;
		}

		public void OnServiceActivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.OnServiceActived();
			}
		}

		public void OnServiceDeactivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.OnServiceDeactived();
			}
		}

		public bool IsHardwareBackButtonPressed()
		{
			return GlobalMembers.IsBackButtonPressed;
		}

		public void InitText()
		{
			TextManager.getInstance().init();
		}

		public override void Init()
		{
			int num = GameApp.mGameRes;
			if (num != 320 && num != 640)
			{
				if (num == 768)
				{
					this.mWideScreenXOffset = ZumasRevenge.Common._DS(160);
				}
			}
			else
			{
				this.mWideScreenXOffset = 0;
			}
			this.mProfileMgr = new ZumaProfileMgr();
			this.mProfileManager = this.mProfileMgr;
			this.mAutoMonkey = new AutoMonkey(this);
			base.Init();
			Res.InitResources(this);
			this.mResourceManager.mBaseArtRes = GameApp.mGameRes;
			this.mResourceManager.mLeadArtRes = 1200;
			this.mResourceManager.mCurArtRes = GameApp.mGameRes;
			this.SetString("DIALOG_BUTTON_YES", TextManager.getInstance().getString(446));
			this.SetString("DIALOG_BUTTON_NO", TextManager.getInstance().getString(447));
			this.SetString("DIALOG_BUTTON_OK", TextManager.getInstance().getString(675));
			this.SetString("DIALOG_BUTTON_CANCEL", TextManager.getInstance().getString(454));
			this.mCachedLoad = false;
			this.InitAudio();
			this.PreShowLoadingScreen();
			this.LoadGlobalConfig();
			this.mInitFinished = true;
		}

		public void StartThreadInit()
		{
			ThreadStart threadStart = new ThreadStart(this.Init);
			this.mInitThread = new Thread(threadStart);
			this.mInitThread.Start();
		}

		public override void InitHook()
		{
		}

		public override string NotifyCrashHook()
		{
			return base.NotifyCrashHook();
		}

		public string GetCrashZipName(int num_override)
		{
			return "";
		}

		public string GetCrashZipName()
		{
			return this.GetCrashZipName(-1);
		}

		protected void GamerSignedInCallback(object sender, SignedInEventArgs args)
		{
			SignedInGamer gamer = args.Gamer;
			if (gamer != null)
			{
				this.m_DefaultProfileName = gamer.Gamertag;
			}
			if (gamer.IsSignedInToLive)
			{
				if (this.m_XLiveState == GameApp.EXLiveWaiting.E_WaitingForSignIn)
				{
					gamer.BeginGetAchievements(new AsyncCallback(this.GetAchievementsCallback), gamer);
					this.m_XLiveState = GameApp.EXLiveWaiting.E_WaitingForAchivements;
				}
			}
			else
			{
				this.m_XLiveState = GameApp.EXLiveWaiting.E_NONE;
				if (this.IsFirstGameLoad(this.m_DefaultProfileName))
				{
					GameApp.gInitialProfLoadSuccessful = true;
					this.mUserProfile = (ZumaProfile)this.mProfileMgr.AddProfile(this.m_DefaultProfileName);
					GameApp.gDDS.ChangeProfile(this.mUserProfile);
				}
				else
				{
					this.mUserProfile = (ZumaProfile)GameApp.gApp.mProfileMgr.GetProfile(GameApp.gApp.m_DefaultProfileName);
				}
			}
			GameApp.USE_TRIAL_VERSION = Guide.IsTrialMode;
		}

		protected void GetAchievementsCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer == null)
			{
				return;
			}
			if (this.mUserProfile == null)
			{
				this.mUserProfile = (ZumaProfile)GameApp.gApp.mProfileMgr.GetProfile(0);
			}
			try
			{
				this.mUserProfile.m_AchievementMgr.m_AchievementsXLive = signedInGamer.EndGetAchievements(result);
			}
			catch (Exception)
			{
			}
			this.m_XLiveState = GameApp.EXLiveWaiting.E_Ready;
		}

		public override void LoadingThreadProc()
		{
			if (this.mCachedLoadState > 1)
			{
				return;
			}
			GameApp.gInitialProfLoadSuccessful = this.mProfileMgr.Init();
			SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.GamerSignedInCallback);
			int num = 70;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				Level levelByIndex;
				do
				{
					levelByIndex = this.mNormalLevelMgr.GetLevelByIndex(num2++);
				}
				while (levelByIndex != null && (levelByIndex.mBoss != null || JeffLib.Common.StrFindNoCase(levelByIndex.mId, "boss") != -1));
				if (levelByIndex == null)
				{
					break;
				}
				"levelthumbs\\" + levelByIndex.mId.ToLower() + "_thumb";
				IdxThumbPair idxThumbPair = new IdxThumbPair();
				idxThumbPair.first = num2 - 1;
				idxThumbPair.second = null;
				this.mLevelThumbnails.Add(idxThumbPair);
			}
			this.mResourceManager.PrepareLoadResourcesList(GameApp.gInitialLoadGroups);
			this.mMusic.LoadMusic(12, "music/MUSIC_TUNE1");
			this.mMusic.LoadMusic(24, "music/MUSIC_TUNE2");
			this.mMusic.LoadMusic(35, "music/MUSIC_TUNE3");
			this.mMusic.LoadMusic(45, "music/MUSIC_TUNE4");
			this.mMusic.LoadMusic(58, "music/MUSIC_TUNE5");
			this.mMusic.LoadMusic(71, "music/MUSIC_TUNE6");
			this.mMusic.LoadMusic(120, "music/MUSIC_WON1");
			this.mMusic.LoadMusic(121, "music/MUSIC_WON2");
			this.mMusic.LoadMusic(122, "music/MUSIC_WON3");
			this.mMusic.LoadMusic(123, "music/MUSIC_WON4");
			this.mMusic.LoadMusic(124, "music/MUSIC_WON5");
			this.mMusic.LoadMusic(125, "music/MUSIC_WON6");
			this.mMusic.LoadMusic(127, "music/MUSIC_BOSS");
			this.mMusic.LoadMusic(144, "music/MUSIC_WON_GAME");
			this.mMusic.LoadMusic(126, "music/MUSIC_GAME_OVER");
			GameApp.gInitialProfLoadSuccessful = true;
			this.mUserProfile = (ZumaProfile)this.mProfileMgr.AddProfile(this.m_DefaultProfileName);
			GameApp.gDDS.ChangeProfile(this.mUserProfile);
		}

		public override void LoadingThreadCompleted()
		{
			base.LoadingThreadCompleted();
			Enumerable.Count<string>(GameApp.gInitialLoadGroups);
			this.mBambooTransition = new BambooTransition();
			this.mProxBombManager = new ProxBombManager();
			if (this.mCachedLoad)
			{
				this.mLoadingThreadCompleted = true;
				this.mLoaded = true;
				this.ShowMainMenu();
				return;
			}
			if (this.mLoadingFailed || this.mCachedLoadState > 1)
			{
				return;
			}
			this.mLoadingScreen.LoadingComplete();
		}

		public bool IsFinishedLoading()
		{
			return true;
		}

		public void GameFinishedLoading()
		{
		}

		public void StartLoading()
		{
			if (!this.mResourceManager.IsGroupLoaded("MainSounds"))
			{
				this.mResourceManager.LoadResources("MainSounds");
			}
			if (!this.mResourceManager.IsGroupLoaded("Text"))
			{
				this.mResourceManager.LoadResources("Text");
			}
			Font fontByName = this.GetFontByName("FONT_SHAGEXOTICA68_BASE");
			((ImageFont)fontByName).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName2 = this.GetFontByName("FONT_SHAGEXOTICA68_BLACK");
			((ImageFont)fontByName2).PushLayerColor("Main", new Color(0, 0, 0, 255));
			Font fontByName3 = this.GetFontByName("FONT_SHAGEXOTICA68_STROKE");
			((ImageFont)fontByName3).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			Font fontByName4 = this.GetFontByName("FONT_SHAGLOUNGE28_STROKE");
			((ImageFont)fontByName4).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			Font fontByName5 = this.GetFontByName("FONT_SHAGEXOTICA38_BASE");
			((ImageFont)fontByName5).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName5).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName6 = this.GetFontByName("FONT_SHAGEXOTICA38_BLACK");
			((ImageFont)fontByName6).PushLayerColor("Main", new Color(0, 0, 0, 255));
			Font fontByName7 = this.GetFontByName("FONT_SHAGEXOTICA38_BLACK_GLOW");
			((ImageFont)fontByName7).PushLayerColor("Glow", new Color(0, 0, 0, 255));
			Font fontByName8 = this.GetFontByName("FONT_SHAGEXOTICA38_GREEN_STROKE");
			((ImageFont)fontByName8).PushLayerColor("Stroke", new Color(79, 91, 66, 255));
			Font fontByName9 = this.GetFontByName("FONT_SHAGEXOTICA100_BASE");
			((ImageFont)fontByName9).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName9).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName10 = this.GetFontByName("FONT_SHAGEXOTICA100_STROKE");
			((ImageFont)fontByName10).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName10).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName11 = this.GetFontByName("FONT_SHAGEXOTICA100_GAUNTLET");
			((ImageFont)fontByName11).PushLayerColor("Main", new Color(85, 50, 160, 255));
			((ImageFont)fontByName11).PushLayerColor("Stroke", new Color(248, 238, 195, 255));
			((ImageFont)fontByName11).PushLayerColor("Shadow", new Color(235, 131, 130, 255));
			Font fontByName12 = this.GetFontByName("FONT_SHAGLOUNGE28_BASE");
			((ImageFont)fontByName12).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			if (Localization.GetCurrentLanguage() != Localization.LanguageType.Language_RU && Localization.GetCurrentLanguage() != Localization.LanguageType.Language_PL)
			{
				((ImageFont)fontByName12).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			}
			Font fontByName13 = this.GetFontByName("FONT_SHAGLOUNGE28_SHADOW");
			((ImageFont)fontByName13).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName14 = this.GetFontByName("FONT_SHAGLOUNGE28_STROKE_GREEN");
			((ImageFont)fontByName14).PushLayerColor("Stroke", new Color(80, 92, 67, 255));
			Font fontByName15 = this.GetFontByName("FONT_SHAGLOUNGE28_BROWN");
			((ImageFont)fontByName15).PushLayerColor("Main", new Color(193, 145, 54, 255));
			((ImageFont)fontByName15).PushLayerColor("Stroke", new Color(66, 45, 14, 255));
			((ImageFont)fontByName15).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName16 = this.GetFontByName("FONT_SHAGLOUNGE28_GREEN");
			((ImageFont)fontByName16).PushLayerColor("Main", new Color(165, 232, 25, 255));
			((ImageFont)fontByName16).PushLayerColor("Glow", new Color(0, 0, 0, 255));
			Font fontByName17 = this.GetFontByName("FONT_SHAGLOUNGE38_BASE");
			((ImageFont)fontByName17).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName17).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName18 = this.GetFontByName("FONT_SHAGLOUNGE38_STROKE");
			((ImageFont)fontByName18).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			Font fontByName19 = this.GetFontByName("FONT_SHAGLOUNGE38_RED_STROKE_YELLOW");
			((ImageFont)fontByName19).PushLayerColor("Main", new Color(218, 10, 9, 255));
			((ImageFont)fontByName19).PushLayerColor("Stroke", new Color(248, 241, 135, 255));
			Font fontByName20 = this.GetFontByName("FONT_SHAGLOUNGE38_YELLOW");
			((ImageFont)fontByName20).PushLayerColor("Stroke", new Color(247, 207, 0, 255));
			Font fontByName21 = this.GetFontByName("FONT_SHAGLOUNGE38_GAUNTLET");
			((ImageFont)fontByName21).PushLayerColor("Main", new Color(249, 245, 188, 255));
			((ImageFont)fontByName21).PushLayerColor("Stroke", new Color(88, 51, 159, 255));
			Font fontByName22 = this.GetFontByName("FONT_SHAGLOUNGE38_GAUNTLET2");
			((ImageFont)fontByName22).PushLayerColor("Main", new Color(251, 245, 189, 255));
			((ImageFont)fontByName22).PushLayerColor("Stroke", new Color(228, 39, 226, 255));
			Font fontByName23 = this.GetFontByName("FONT_SHAGLOUNGE45_BASE");
			((ImageFont)fontByName23).PushLayerColor("Stroke", new Color(0, 0, 0, 255));
			((ImageFont)fontByName23).PushLayerColor("Shadow", new Color(0, 0, 0, 255));
			Font fontByName24 = this.GetFontByName("FONT_SHAGLOUNGE45_GAUNTLET");
			((ImageFont)fontByName24).PushLayerColor("Main", new Color(249, 245, 188, 255));
			((ImageFont)fontByName24).PushLayerColor("Stroke", new Color(88, 51, 159, 255));
			Font fontByName25 = this.GetFontByName("FONT_SHAGLOUNGE45_RED");
			((ImageFont)fontByName25).PushLayerColor("Main", new Color(183, 61, 47, 255));
			Font fontByName26 = this.GetFontByName("FONT_SHAGLOUNGE45_YELLOW");
			((ImageFont)fontByName26).PushLayerColor("Main", new Color(222, 180, 8, 255));
			this.StartLoadingComplete = true;
		}

		public override void LostFocus()
		{
			if (this.mBoard != null && Board.gPauseOnLostFocus)
			{
				this.mBoard.Pause(true);
			}
			this.mMusic.Enable(false);
			this.SaveProfile();
		}

		public override void GotFocus()
		{
			this.DetectMusicSettings();
			if (this.mBoard != null && Board.gPauseOnLostFocus)
			{
				this.mBoard.Pause(false);
				this.mBoard.mNumPauseUpdatesToDo = ZumasRevenge.Common._M(50);
				this.mBoard.MarkDirty();
			}
			this.ReportAppLaunchInfo(4);
		}

		public override bool DebugKeyDown(int key)
		{
			return false;
		}

		public override void UpdateFrames()
		{
			this.mMusic.Update();
			this.mSoundPlayer.Update();
			base.UpdateFrames();
			this.TransitionFromLoadingScreen();
			if (this.mDialogMap.Count > 0)
			{
				if (this.mMainMenu != null && this.mMainMenu.mUserSelDlg != null)
				{
					this.mWidgetManager.PutBehind(this.mUnderDialogWidget, this.mMainMenu.mUserSelDlg);
				}
				else
				{
					this.mWidgetManager.PutBehind(this.mUnderDialogWidget, this.mDialogList.Last.Value);
				}
				if (this.mDialogObscurePct < 1f)
				{
					if (this.mBoard != null && this.mBoard.mDoingFirstTimeIntro)
					{
						this.mDialogObscurePct = Math.Min(ZumasRevenge.Common._M(0.9f), this.mDialogObscurePct + ZumasRevenge.Common._M1(0.06f));
					}
					else
					{
						this.mDialogObscurePct = Math.Min(1f, this.mDialogObscurePct + ZumasRevenge.Common._M(0.06f));
					}
				}
			}
			else
			{
				if (this.mBoard != null && this.mBoard.mDoingFirstTimeIntro)
				{
					this.mDialogObscurePct = Math.Max(0f, this.mDialogObscurePct - ZumasRevenge.Common._M(0.015f));
				}
				else
				{
					this.mDialogObscurePct = Math.Max(0f, this.mDialogObscurePct - ZumasRevenge.Common._M(0.06f));
				}
				if (this.mDialogObscurePct == 0f && this.mUnderDialogWidget.mVisible)
				{
					this.mUnderDialogWidget.SetVisible(false);
				}
			}
			if (this.m_XLiveState == GameApp.EXLiveWaiting.E_Ready)
			{
				this.m_XLiveState = GameApp.EXLiveWaiting.E_NONE;
				SignedInGamer signedInGamer = Gamer.SignedInGamers[0];
				if (signedInGamer != null)
				{
					this.m_DefaultProfileName = signedInGamer.Gamertag;
				}
				if (!this.IsFirstGameLoad(this.m_DefaultProfileName) || !this.IsFirstGameLoad(this.m_DefaultName))
				{
					if (!this.IsFirstGameLoad(this.m_DefaultName))
					{
						GameApp.gApp.mProfileMgr.RenameProfile(this.m_DefaultName, GameApp.gApp.m_DefaultProfileName);
					}
					this.mUserProfile = (ZumaProfile)GameApp.gApp.mProfileMgr.GetProfile(GameApp.gApp.m_DefaultProfileName);
					return;
				}
				GameApp.gInitialProfLoadSuccessful = true;
				this.mUserProfile = (ZumaProfile)this.mProfileMgr.AddProfile(this.m_DefaultProfileName);
				GameApp.gDDS.ChangeProfile(this.mUserProfile);
			}
		}

		public virtual void PlaySamplePan(int theSoundNum, int thePan, int min_time)
		{
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.pan = thePan;
			this.mSoundPlayer.Play(theSoundNum, soundAttribs);
		}

		public virtual void PlaySamplePan(int theSoundNum, int thePan)
		{
			this.PlaySamplePan(theSoundNum, thePan, 5);
		}

		public override void PlaySample(int theSoundNum, int min_time)
		{
			this.mSoundPlayer.Play(theSoundNum);
		}

		public override void PlaySample(int theSoundNum)
		{
			this.PlaySample(theSoundNum, 5);
		}

		public override void DialogButtonDepress(int dialog_id, int button_id)
		{
			if (dialog_id == 1)
			{
				if (this.mYesNoDialogDelegate != null)
				{
					this.mYesNoDialogDelegate(button_id);
					this.mDialog.Kill();
					if (this.mBoard != null)
					{
						this.mBoard.Pause(false, true);
					}
					if (Enumerable.Count<KeyValuePair<int, Dialog>>(this.mDialogMap) == 1)
					{
						this.mDialog.SetFocusWidgetToBoard();
					}
					this.mDialog.Kill();
					return;
				}
			}
			else if (dialog_id == 0)
			{
				((ZumaDialog)base.GetDialog(dialog_id)).Kill();
				if (this.mBoard != null)
				{
					this.mBoard.Pause(false, true);
				}
				if (this.mDialogCallBack != null)
				{
					this.mDialogCallBack();
					this.mDialogCallBack = null;
				}
			}
		}

		public override void SwitchScreenMode(bool wantWindowed, bool is3d, bool force)
		{
			base.SwitchScreenMode(wantWindowed, is3d, force);
			this.RegistryWriteBoolean("Is3D", is3d);
			if (this.mBoard != null)
			{
				this.mBoard.mNumPauseUpdatesToDo = ZumasRevenge.Common._M(10);
				this.mBoard.MarkDirty();
			}
		}

		public override MusicInterface CreateMusicInterface()
		{
			if (this.mNoSoundNeeded)
			{
				return new MusicInterface();
			}
			return base.CreateMusicInterface();
		}

		public override void HandleCmdLineParam(string theParamName, string theParamValue)
		{
			base.HandleCmdLineParam(theParamName, theParamValue);
		}

		public override void AddDialog(int id, Dialog d)
		{
			GameApp.gAddingDlgID = id;
			base.AddDialog(id, d);
			GameApp.gAddingDlgID = -12345;
			if (id != 6)
			{
				foreach (Dialog dialog in this.mDialogList)
				{
					if (dialog != d)
					{
						DialogHideInfo dialogHideInfo = new DialogHideInfo();
						dialogHideInfo.mDialog = dialog;
						dialogHideInfo.mHideCount = 1;
						new KeyValuePair<int, DialogHideInfo>(dialog.mId, dialogHideInfo);
						DialogHideInfo dialogHideInfo2 = null;
						if (this.mDialogHideInfoMap != null)
						{
							if (this.mDialogHideInfoMap.TryGetValue(dialog.mId, ref dialogHideInfo2))
							{
								dialogHideInfo2.mHideCount++;
							}
							else
							{
								this.mDialogHideInfoMap.Add(dialog.mId, dialogHideInfo);
							}
						}
					}
				}
			}
		}

		public override void AddDialog(Dialog theDialog)
		{
			base.AddDialog(theDialog);
		}

		public override bool KillDialog(int id, bool removeWidget, bool deleteWidget)
		{
			if (id != GameApp.gAddingDlgID)
			{
				List<int> list = new List<int>();
				if (this.mDialogHideInfoMap != null)
				{
					foreach (KeyValuePair<int, DialogHideInfo> keyValuePair in this.mDialogHideInfoMap)
					{
						if (--keyValuePair.Value.mHideCount == 0)
						{
							list.Add(keyValuePair.Key);
						}
					}
					for (int i = 0; i < Enumerable.Count<int>(list); i++)
					{
						this.mDialogHideInfoMap.Remove(list[i]);
					}
				}
			}
			return base.KillDialog(id, removeWidget, deleteWidget);
		}

		public override bool KillDialog(int theDialogId)
		{
			return base.KillDialog(theDialogId);
		}

		public override bool KillDialog(Dialog theDialog)
		{
			return base.KillDialog(theDialog);
		}

		public void InitAudio()
		{
			this.mMusic = new Music(this.mMusicInterface);
			this.mMusic.RegisterCallBack();
			this.mSoundPlayer = new SoundEffects(this.mSoundManager);
		}

		public bool MusicEnabled()
		{
			return !this.mMusicInterface.isPlayingUserMusic();
		}

		public void DetectMusicSettings()
		{
			Dialog dialog = base.GetDialog(2);
			if (dialog != null)
			{
				((OptionsDialog)dialog).DetectMusicSettings();
				return;
			}
			this.mMusic.Enable(this.MusicEnabled() && this.GetMusicVolume() > 0.0);
		}

		public void TransitionFromLoadingScreen()
		{
			if (this.mLoadingScreen == null)
			{
				return;
			}
			if (this.mDelayIntro)
			{
				this.LoadBoard();
				return;
			}
			if (this.mLoadingScreen.CanShowMenu() && !this.TriggerFirstProfileDialog())
			{
				this.ShowMainMenu();
				this.mSoundPlayer.Stop(Res.GetSoundByID(ResID.SOUND_SEAGULLS));
				this.mWidgetManager.BringToFront(this.mLoadingScreen);
				return;
			}
			if (this.mLoadingScreen.Done() && this.mNewUserDlg == null)
			{
				this.KillLoadingScreen();
				this.mSoundPlayer.Stop(this.GetSoundIDByName("SOUND_SEAGULLS"), true);
			}
		}

		public void LoadBoard()
		{
			this.mDelayIntro = false;
			if (this.mBoard != null)
			{
				this.mWidgetManager.AddWidget(this.mBoard);
			}
			this.KillLoadingScreen();
		}

		public void KillLoadingScreen()
		{
			if (this.mLoadingScreen == null)
			{
				return;
			}
			this.mWidgetManager.RemoveWidget(this.mLoadingScreen);
			this.mLoadingScreen.Dispose();
			this.mLoadingScreen = null;
			if (this.mResourceManager.IsGroupLoaded("LoadScreen"))
			{
				this.mResourceManager.DeleteResources("LoadScreen");
			}
		}

		public bool TriggerFirstProfileDialog()
		{
			return false;
		}

		public bool IsFirstGameLoad()
		{
			return this.mProfileMgr.GetNumProfiles() == 0U;
		}

		public bool IsFirstGameLoad(string name)
		{
			return !this.mProfileMgr.HasProfile(name);
		}

		public LevelMgr GetLevelMgr()
		{
			if (this.mUserProfile == null || this.mBoard == null)
			{
				return this.mNormalLevelMgr;
			}
			return this.mNormalLevelMgr;
		}

		public void ResetAllLevelMgrs()
		{
			this.mNormalLevelMgr.Reset();
		}

		public bool ReloadAllLevelMgrs()
		{
			LevelMgr[] array = new LevelMgr[] { this.mNormalLevelMgr };
			for (int i = 0; i < 1; i++)
			{
				if (!array[i].LoadLevels(array[i].mLevelXML))
				{
					this.Popup(array[i].GetErrorText());
					this.Popup("Your boss DDS parameters were all reset. You should quit and restart.");
					return false;
				}
			}
			return true;
		}

		public void ShowMainMenu(bool do_load_thread)
		{
			this.mClickedHardMode = false;
			this.PlaySong(1);
			if (this.mInitialLoad)
			{
				if (!GameApp.gApp.mResourceManager.IsGroupLoaded("MenuRelated") && !this.mResourceManager.LoadResources("MenuRelated"))
				{
					this.mStartInGameModeThreadProcRunning = false;
					this.mInGameLoadThreadProcFailed = true;
					return;
				}
				this.mMainMenu = new MainMenu(this);
				this.mMainMenu.Init();
				this.mMainMenu.Resize(this.GetScreenRect());
				this.mWidgetManager.AddWidget(this.mMainMenu);
				this.mWidgetManager.SetFocus(this.mMainMenu);
				this.mLoadingScreen.mMouseVisible = false;
				this.mInitialLoad = false;
				this.CheckForAppUpdate();
			}
			else
			{
				if (this.mUserProfile != null)
				{
					this.mUserProfile.mDoChallengeTrophyZoom = (this.mUserProfile.mDoChallengeAceTrophyZoom = false);
					this.mUserProfile.mDoChallengeAceCupComplete = (this.mUserProfile.mDoChallengeCupComplete = false);
					this.mUserProfile.mUnlockSparklesIdx1 = (this.mUserProfile.mUnlockSparklesIdx2 = -1);
				}
				this.SetupMainMenuDefaults(do_load_thread);
			}
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("Map"))
			{
				this.mResourceManager.PrepareLoadResources("Map");
			}
			if (!GameApp.gApp.mResourceManager.IsGroupLoaded("CommonGame"))
			{
				this.mResourceManager.PrepareLoadResources("CommonGame");
			}
			if (this.mUserProfile == null)
			{
				ZumaProfile zumaProfile = (ZumaProfile)this.mProfileMgr.GetAnyProfile();
				string text = "";
				if (zumaProfile != null)
				{
					text = zumaProfile.GetName();
				}
				this.RegistryReadString("LastUser", ref text);
				if (text.Length > 0)
				{
					if (!GameApp.gInitialProfLoadSuccessful || !this.ChangeUser(text))
					{
						if (this.mProfileMgr.GetNumProfiles() > 0U)
						{
							zumaProfile = (ZumaProfile)this.mProfileMgr.GetAnyProfile();
							if (zumaProfile != null)
							{
								this.mUserProfile = zumaProfile;
								this.ChangeUser(zumaProfile.GetName());
							}
							this.mMainMenu.DoChangeUserDialog();
							this.ClearUpdateBacklog(false);
						}
						else
						{
							if (!GameApp.gInitialProfLoadSuccessful && !this.mCachedLoad)
							{
								this.DoGenericDialog("ERROR", "One or more of your saved game files is\nincompatible with this version of the game.\nThey have been deleted.", true, null, 0);
							}
							this.DoNewUserDialog();
						}
					}
					this.mMainMenu.MarkDirty();
				}
				else
				{
					this.DoNewUserDialog();
					this.mMainMenu.MarkDirty();
				}
				this.mMainMenu.RehupButtons();
			}
			if (this.mUserProfile != null && this.mMainMenu.mChallengeMenu != null)
			{
				this.mMainMenu.mChallengeMenu.InitCS();
			}
			this.mMainMenu.RehupButtons();
		}

		public void ShowMainMenu()
		{
			this.ShowMainMenu(true);
		}

		public void HideChallengeMenu()
		{
			if (this.mMainMenu != null && this.mMainMenu.mChallengeMenu != null)
			{
				this.mMainMenu.HideChallengeMenu();
			}
		}

		public void HideMainMenu(bool delete_resources)
		{
			if (this.mMainMenu != null)
			{
				if (this.mMainMenu.mChallengeMenu != null)
				{
					this.mMainMenu.HideChallengeMenu();
				}
				this.mWidgetManager.RemoveWidget(this.mMainMenu);
				base.SafeDeleteWidget(this.mMainMenu);
				this.mMainMenu = null;
			}
			this.HideAdventureModeMapScreen();
			if (this.mResourceManager.IsGroupLoaded("MenuRelated"))
			{
				this.mResourceManager.DeleteResources("MenuRelated");
			}
		}

		public void ShowMoreGames()
		{
			this.mMoreGames = new MoreGames(this);
			this.mMoreGames.Init();
			this.mMoreGames.Resize(GameApp.gApp.GetScreenRect());
			this.mWidgetManager.AddWidget(this.mMoreGames);
			this.mMainMenu.DoMoreGamesSlide(false);
			this.mMoreGames.DoSlide(true);
		}

		public void HideMoreGames()
		{
			if (this.mMoreGames != null)
			{
				this.mMoreGames.DoSlide(false);
			}
			this.mMainMenu.DoMoreGamesSlide(true);
		}

		public void DeleteMoreGames(bool delete_resources)
		{
			if (this.mMoreGames != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMoreGames);
				base.SafeDeleteWidget(this.mMoreGames);
				this.mMoreGames = null;
			}
			if (delete_resources)
			{
				this.mResourceManager.DeleteResources("MoreGames");
			}
		}

		public void ShowIronFrog()
		{
			this.SetupMainMenuDefaults();
			this.mMainMenu.mChallengeMenu.InitCS();
			this.mMainMenu.RehupButtons();
			this.mMainMenu.DoIronFrog(false);
			this.PlaySong(1);
		}

		public void ShowChallengeSelector()
		{
			this.SetupMainMenuDefaults();
			this.mMainMenu.ShowChallengeMenu();
			this.mMainMenu.mChallengeMenu.mCueMainSong = true;
		}

		public void ShowAdventureModeMapScreen()
		{
			if (!this.mResourceManager.IsGroupLoaded("Map") && !this.mResourceManager.LoadResources("Map"))
			{
				this.ShowResourceError(true);
				this.Shutdown();
				return;
			}
			if (!this.mResourceManager.IsGroupLoaded("GamePlay"))
			{
				this.mResourceManager.PrepareLoadResources("GamePlay");
			}
			this.mMapScreen = new MapScreen();
			this.mMapScreenHackWidget = new MapScreenHackWidget();
			this.mMapScreen.mParent = this.mMapScreenHackWidget;
			this.mWidgetManager.AddWidget(this.mMapScreenHackWidget);
			this.mMapScreenHackWidget.Resize(0, 0, this.mWidth, this.mHeight);
			this.mMapScreen.Init(false, this.mUserProfile.GetAdvModeVars().mCurrentAdvZone, this.mUserProfile.GetAdvModeVars().mCurrentAdvLevel, false, true);
			this.mWidgetManager.SetFocus(this.mMapScreenHackWidget);
			this.mMapScreen.DoSlide(true);
			if (this.mMainMenu != null)
			{
				this.mMainMenu.HideScrollButtons();
			}
		}

		public void HideAdventureModeMapScreen()
		{
			if (this.mMapScreenHackWidget != null)
			{
				this.mWidgetManager.RemoveWidget(this.mMapScreenHackWidget);
				base.SafeDeleteWidget(this.mMapScreenHackWidget);
				this.mMapScreenHackWidget = null;
			}
			if (this.mMapScreen != null)
			{
				this.mMapScreen.Dispose();
				this.mMapScreen = null;
			}
			if ((this.mUserProfile == null || !this.mUserProfile.mNeedsFirstTimeIntro) && this.mResourceManager.IsGroupLoaded("Map"))
			{
				this.mResourceManager.DeleteResources("Map");
			}
			if (this.mMainMenu != null)
			{
				this.mMainMenu.ShowScrollButtons();
				this.PlaySong(1);
			}
		}

		public void StartAdventureMode()
		{
			if (this.mStartInGameModeThreadProcRunning)
			{
				return;
			}
			this.PlaySong(12);
			this.mLoadType = ((this.mForceZoneRestart == -1) ? 0 : 1);
			if (this.IsHardMode())
			{
				this.mUserProfile.mFirstTimeReplayingHardMode = false;
			}
			else
			{
				this.mUserProfile.mFirstTimeReplayingNormalMode = false;
			}
			this.mStartInGameModeThreadProcRunning = true;
			this.StartAdvModeThreadProc();
			Rect aRect;
			if (this.mLoadType == 1)
			{
				int mX = this.mMapScreen.mCards[this.mMapScreen.mSelectedZone - 1].mX;
				int mY = this.mMapScreen.mCards[this.mMapScreen.mSelectedZone - 1].mY;
				Image imageByName = this.GetImageByName("IMAGE_UI_CHALLENGESCREEN_HOME_SELECT");
				aRect = new Rect(mX, mY, (int)(0.4f * (float)imageByName.mWidth), (int)(0.4f * (float)imageByName.mHeight));
			}
			else
			{
				aRect = new Rect(ZumasRevenge.Common._DS(ZumasRevenge.Common._M(624)), ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(697)), ZumasRevenge.Common._DS(ZumasRevenge.Common._M2(700)), ZumasRevenge.Common._DS(ZumasRevenge.Common._M3(500)));
			}
			Ratio aspectRatio = this.mGraphicsDriver.GetAspectRatio();
			int num = ((aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3) ? ZumasRevenge.Common._DS(ZumasRevenge.Common._M(160)) : 0);
			aRect.mWidth += num;
			this.DoCommonInGameLoadThread(aRect);
			this.mBoard.AdventureModeSetupComplete(this.mContinuedGame);
			this.HideMainMenu(true);
		}

		public void StartAdvModeFirstTime()
		{
			if (!this.mResourceManager.IsGroupLoaded("MapZoom") && !this.mResourceManager.LoadResources("MapZoom"))
			{
				this.ShowResourceError(true);
				this.Shutdown();
				return;
			}
			if (!this.mResourceManager.IsGroupLoaded("Text") && !this.mResourceManager.LoadResources("Text"))
			{
				this.ShowResourceError(true);
				this.Shutdown();
				return;
			}
			this.mMusic.FadeOut();
			this.HideMainMenu(true);
			this.mBoard = new Board(this, -1);
			if (this.mLoadingScreen == null)
			{
				this.mWidgetManager.AddWidget(this.mBoard);
			}
			this.mBoard.mAdventureMode = true;
			if (!this.mBoard.Init(true))
			{
				this.mInGameLoadThreadProcFailed = true;
				this.mStartInGameModeThreadProcRunning = false;
				return;
			}
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			this.mContinuedGame = false;
			this.mBoard.StartLevel(1);
			this.mBoard.MakeCachedBackground();
			this.mWidgetManager.SetFocus(this.mBoard);
			if (this.mWidescreenBoardWidget == null)
			{
				this.mWidescreenBoardWidget = new WidescreenBoardWidget();
				this.mWidescreenBoardWidget.Resize(ZumasRevenge.Common._S(-80), 0, this.mWidth + ZumasRevenge.Common._S(160), this.mHeight);
				this.mWidgetManager.AddWidget(this.mWidescreenBoardWidget);
			}
			this.mBoard.SetMenuBtnEnabled(false);
			this.mAutoMonkey.SetState(MonkeyState.Playing);
		}

		public void DoDeferredEndGame()
		{
			if (this.mBoard != null)
			{
				this.mBoard.mNumDrawFramesLeft = ZumasRevenge.Common._M(2);
				this.mBoard.mReturnToMainMenu = true;
			}
		}

		public void EndCurrentGame()
		{
			this.mBoard.DoShutdownSaveGame();
			this.mBoard.mSkipShutdownSave = true;
			this.mWidgetManager.RemoveWidget(this.mBoard);
			base.SafeDeleteWidget(this.mBoard);
			this.mBoard = null;
		}

		public void StartGauntletMode(string normal_level_id, Rect thumb_rect)
		{
			if (this.mStartInGameModeThreadProcRunning)
			{
				return;
			}
			this.mLoadType = 2;
			this.mChallengeLevelId = normal_level_id;
			this.mStartInGameModeThreadProcRunning = true;
			this.StartChallengeModeThreadProc();
			Rect aRect = new Rect(thumb_rect);
			Ratio aspectRatio = this.mGraphicsDriver.GetAspectRatio();
			int num = ((aspectRatio.mNumerator != 4 && aspectRatio.mDenominator != 3) ? ZumasRevenge.Common._DS(ZumasRevenge.Common._M(320)) : 0);
			aRect.mWidth += num;
			this.DoCommonInGameLoadThread(aRect);
			this.HideMainMenu(true);
			this.PlaySong(12);
			this.mBoard.GauntletModeSetupComplete();
			this.mAutoMonkey.SetState(MonkeyState.Playing);
		}

		public void StartIronFrogMode()
		{
			if (this.mStartInGameModeThreadProcRunning)
			{
				return;
			}
			this.mUserProfile.mIronFrogStats.mCurTime = 0;
			this.mLoadType = 3;
			this.mStartInGameModeThreadProcRunning = true;
			this.StartIronFrogModeThreadProc();
			int num = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(700));
			int num2 = ZumasRevenge.Common._DS(ZumasRevenge.Common._M(650));
			this.DoCommonInGameLoadThread(new Rect((this.mWidth - num) / 2, (this.mHeight - num2) / 2, num, num2));
			this.HideMainMenu(true);
			this.PlaySong(12);
		}

		public void PlaySong(int song, float fade_speed)
		{
			bool inLoop = true;
			if (song != 0)
			{
				switch (song)
				{
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
					goto IL_37;
				case 126:
					break;
				default:
					if (song != 137)
					{
						goto IL_3D;
					}
					break;
				}
				inLoop = false;
				goto IL_3D;
			}
			IL_37:
			inLoop = false;
			IL_3D:
			this.mMusic.PlaySongNoDelay(song, inLoop);
		}

		public void PlaySong(int song)
		{
			this.PlaySong(song, 0.005f);
		}

		public void DoOptionsDialog(bool ingame)
		{
			if (this.mBoard != null)
			{
				this.mBoard.Pause(true, true);
			}
			OptionsDialog optionsDialog = new OptionsDialog(ingame);
			ZumasRevenge.Common.SetupDialog(optionsDialog);
			this.AddDialog(optionsDialog);
			if (ingame)
			{
				optionsDialog.Move(optionsDialog.mX, optionsDialog.mY + ZumasRevenge.Common._S(30));
			}
		}

		public void FinishOptionsDialog(bool doSave)
		{
			OptionsDialog optionsDialog = base.GetDialog(2) as OptionsDialog;
			bool wantWindowed = false;
			bool flag = true;
			bool flag2 = true;
			bool mIsWindowed = this.mIsWindowed;
			this.Is3DAccelerated();
			if (flag2)
			{
				flag = true;
			}
			bool flag3 = false;
			this.EnableCustomCursors(false);
			this.mCursorTarget = false;
			this.RegistryWriteBoolean("Z2Cursor", this.mCursorTarget);
			if (doSave)
			{
				this.mColorblind = optionsDialog.mColorBlindSlider.IsOn();
				this.SaveGlobalConfig();
			}
			if (flag3)
			{
				this.RegistryWriteBoolean("PreHiRes", this.mHiRes);
				this.RegistryWriteBoolean("Pre3D", this.Is3DAccelerated());
				this.RegistryWriteBoolean("PreWindowed", this.mIsWindowed);
				this.RegistryWriteBoolean("NeedsConfirmation", true);
				this.mPreferredWidth = (this.mPreferredHeight = -1);
				this.RegistryWriteBoolean("HiRes", true);
				this.mReInit = true;
				this.Shutdown();
				if (!flag)
				{
					this.RegistryWriteBoolean("Is3D", false);
				}
				else
				{
					this.RegistryEraseValue("Is3D");
				}
			}
			else
			{
				this.SwitchScreenMode(wantWindowed, flag, true);
				this.ClearUpdateBacklog(false);
			}
			optionsDialog.mDrawScale.SetCurve(ZumasRevenge.Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			optionsDialog.mWidgetFlagsMod.mRemoveFlags |= 16;
			optionsDialog.Kill();
			if (this.mBoard != null)
			{
				this.mBoard.Pause(false, true);
				if (this.mBoard.mMenuButton != null)
				{
					this.mBoard.mMenuButton.mDisabled = false;
				}
			}
		}

		public int DoQuitPromptDialog()
		{
			return this.DoYesNoDialog(TextManager.getInstance().getString(448), TextManager.getInstance().getString(453), true);
		}

		public void TakeScreenshot(string prefix)
		{
		}

		public static SharedImageRef CompositionLoadFunc(string file_dir, string file_name)
		{
			int num = file_name.IndexOf('\\');
			string text = "";
			string text2 = "";
			if (num != -1)
			{
				text = file_name.Substring(0, num);
				text2 = file_name.Substring(num + 1);
			}
			string text3;
			string text4;
			if (text.Length == 0)
			{
				text3 = JeffLib.Common.PathToResName(file_dir, "images", "IMAGE") + GameApp.mCompositionResPrefix + "_" + JeffLib.Common.StripFileExtension(file_name).ToUpper();
				text4 = JeffLib.Common.PathToResName(file_dir, "images", "IMAGE") + "_" + JeffLib.Common.StripFileExtension(file_name).ToUpper();
			}
			else
			{
				text3 = JeffLib.Common.PathToResName(file_dir, "images", "IMAGE") + GameApp.mCompositionResPrefix + "_" + (text + "_" + text2).ToUpper();
				text4 = JeffLib.Common.PathToResName(file_dir, "images", "IMAGE") + "_" + (text + "_" + text2).ToUpper();
			}
			text3 = text3.Replace(' ', '_');
			text3 = text3.Replace('-', '_');
			text4 = text4.Replace(' ', '_');
			text4 = text4.Replace('-', '_');
			SharedImageRef sharedImageRef = GameApp.gApp.mResourceManager.LoadImage(text3);
			if (sharedImageRef == null || (sharedImageRef != null && sharedImageRef.GetImage() == null))
			{
				sharedImageRef = GameApp.gApp.mResourceManager.LoadImage(text4);
				sharedImageRef.mSharedImage.mImage.mFilePath = text4;
			}
			else
			{
				sharedImageRef.mSharedImage.mImage.mFilePath = text3;
			}
			return sharedImageRef;
		}

		public static void CompositionPostLoadFunc(SharedImageRef img, Layer l)
		{
			l.mXOff = ZumasRevenge.Common._DS(GameApp.gApp.mResourceManager.GetImageOffset(l.GetImage().mFilePath).mX);
			l.mYOff = ZumasRevenge.Common._DS(GameApp.gApp.mResourceManager.GetImageOffset(l.GetImage().mFilePath).mY);
		}

		public bool ChangeUser(string user_name)
		{
			return true;
		}

		public bool DeleteUser(string user_name)
		{
			return true;
		}

		public bool ShadersSupported()
		{
			return true;
		}

		public void DoNewUserDialog(int button_mode, bool isIntro)
		{
		}

		public void DoNewUserDialog()
		{
			this.DoNewUserDialog(3, false);
		}

		public void DoNewUserDialog(int button_mode)
		{
			this.DoNewUserDialog(button_mode, false);
		}

		public Rect GetNewUserDialogFrame()
		{
			return Rect.ZERO_RECT;
		}

		public void BlankNameEntered()
		{
		}

		public void NameIsAllSpaces()
		{
		}

		public void FinishedNewUser(bool canceled)
		{
		}

		public void DoGenericDialog(string header, string message, bool block, GameApp.PreBlockCallback pre_block_callback, int width_pad)
		{
			Font fontByName = this.GetFontByName("FONT_SHAGLOUNGE38_YELLOW");
			ZumaDialog zumaDialog = new ZumaDialog(0, true, "", message, TextManager.getInstance().getString(483), 3);
			zumaDialog.mSpaceAfterHeader = 0;
			if (this.mBoard != null)
			{
				this.mBoard.Pause(true, true);
			}
			zumaDialog.mContentInsets.mTop += ZumasRevenge.Common._S(ZumasRevenge.Common._M(30));
			int num = 0;
			int num2 = 0;
			JeffLib.Common.StringDimensions(message, fontByName, out num, out num2);
			zumaDialog.mAllowDrag = false;
			zumaDialog.GetSize(ref num, ref num2);
			num += width_pad;
			zumaDialog.Resize((this.mWidth - num) / 2, (this.mHeight - num2) / 2, num, num2);
			ZumasRevenge.Common.SetupDialog(zumaDialog);
			this.AddDialog(zumaDialog);
			this.mDialogCallBack = pre_block_callback;
		}

		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no, bool drag, int header_space, int id)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, btn_no, drag, header_space, id, 0);
		}

		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no, bool drag, int header_space)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, btn_no, drag, header_space, 1, 0);
		}

		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no, bool drag)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, btn_no, drag, -1, 1, 0);
		}

		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, btn_no, true, -1, 1, 0);
		}

		public int DoYesNoDialog(string header, string message, bool block, string btn_yes)
		{
			return this.DoYesNoDialog(header, message, block, btn_yes, TextManager.getInstance().getString(447), true, -1, 1, 0);
		}

		public int DoYesNoDialog(string header, string message, bool block)
		{
			return this.DoYesNoDialog(header, message, block, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), true, -1, 1, 0);
		}

		public int DoYesNoDialog(string header, string message)
		{
			return this.DoYesNoDialog(header, message, false, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), true, -1, 1, 0);
		}

		public int DoYesNoDialog(string header, string message, bool block, string btn_yes, string btn_no, bool drag, int header_space, int id, int width_pad)
		{
			Font fontByName = this.GetFontByName("FONT_SHAGLOUNGE38_YELLOW");
			this.mDialog = new ZumaDialog(id, true, "", message, "", 1);
			this.mDialog.mSpaceAfterHeader = 0;
			if (this.mBoard != null)
			{
				this.mBoard.Pause(true, true);
			}
			this.mDialog.mContentInsets.mTop += ZumasRevenge.Common._S(ZumasRevenge.Common._M(30));
			int num;
			int num2;
			JeffLib.Common.StringDimensions(message, fontByName, out num, out num2);
			this.mDialog.mAllowDrag = false;
			this.mDialog.GetSize(ref num, ref num2);
			num += width_pad;
			this.mDialog.Resize((this.mWidth - num) / 2, (this.mHeight - num2) / 2, num, num2);
			this.mDialog.mYesButton.mLabel = btn_yes;
			this.mDialog.mNoButton.mLabel = btn_no;
			this.mDialog.mAllowDrag = false;
			ZumasRevenge.Common.SetupDialog(this.mDialog);
			this.AddDialog(this.mDialog);
			this.mWidgetManager.SetFocus(this.mDialog);
			if (block)
			{
				return this.mDialog.WaitForResult(false);
			}
			return -1;
		}

		public void EndYesNoDialog(int ButtonId)
		{
			if (this.mYesNoDialogDelegate != null)
			{
				this.mYesNoDialogDelegate(ButtonId);
			}
		}

		public int GetPan(int thePos)
		{
			return 3000 * (thePos - 400) / 400;
		}

		public CompositionMgr LoadComposition(string file_name, string res_prefix)
		{
			string text = SexyLocale.StringToUpper(file_name);
			if (this.mPreloadedComps.ContainsKey(text) && this.mPreloadedComps[text].isValid())
			{
				return new CompositionMgr(this.mPreloadedComps[text]);
			}
			CompositionMgr compositionMgr = new CompositionMgr();
			compositionMgr.mLoadImageFunc = new AECommon.LoadCompImageFunc(GameApp.CompositionLoadFunc);
			compositionMgr.mPostLoadImageFunc = new AECommon.PostLoadCompImageFunc(GameApp.CompositionPostLoadFunc);
			GameApp.mCompositionResPrefix = res_prefix;
			bool flag = compositionMgr.LoadFromFile(file_name);
			GameApp.mCompositionResPrefix = "";
			if (!flag)
			{
				compositionMgr = null;
			}
			this.mPreloadedComps[text] = compositionMgr;
			return new CompositionMgr(compositionMgr);
		}

		public PIEffect GetPIEffect(string file_name, bool create_copy)
		{
			if (this.mLoadingThreadCompleted)
			{
				if (file_name == "TorchFlame")
				{
					for (int i = 0; i < this.mCachedTorchEffects.size<CachedTorchEffect>(); i++)
					{
						CachedTorchEffect cachedTorchEffect = this.mCachedTorchEffects[i];
						if (!cachedTorchEffect.mFlameInUse)
						{
							cachedTorchEffect.mFlameInUse = true;
							cachedTorchEffect.mTorchFlame.ResetAnim();
							return cachedTorchEffect.mTorchFlame;
						}
					}
				}
				else if (file_name == "TorchFlameOut")
				{
					for (int j = 0; j < this.mCachedTorchEffects.size<CachedTorchEffect>(); j++)
					{
						CachedTorchEffect cachedTorchEffect2 = this.mCachedTorchEffects[j];
						if (!cachedTorchEffect2.mFlameOutInUse)
						{
							cachedTorchEffect2.mFlameOutInUse = true;
							cachedTorchEffect2.mTorchFlameOut.ResetAnim();
							return cachedTorchEffect2.mTorchFlameOut;
						}
					}
				}
				else if (file_name == "Devil Projectile")
				{
					for (int k = 0; k < this.mCachedVolcanoEffects.size<CachedVolcanoEffect>(); k++)
					{
						CachedVolcanoEffect cachedVolcanoEffect = this.mCachedVolcanoEffects[k];
						if (!cachedVolcanoEffect.mProjectileInUse)
						{
							cachedVolcanoEffect.mProjectileInUse = true;
							cachedVolcanoEffect.mProjectile.ResetAnim();
							cachedVolcanoEffect.mProjectile.mEmitAfterTimeline = true;
							return cachedVolcanoEffect.mProjectile;
						}
					}
				}
				else if (file_name == "Devil Explosion")
				{
					for (int l = 0; l < this.mCachedVolcanoEffects.size<CachedVolcanoEffect>(); l++)
					{
						CachedVolcanoEffect cachedVolcanoEffect2 = this.mCachedVolcanoEffects[l];
						if (!cachedVolcanoEffect2.mExplosionInUse)
						{
							cachedVolcanoEffect2.mExplosionInUse = true;
							cachedVolcanoEffect2.mExplosion.ResetAnim();
							return cachedVolcanoEffect2.mExplosion;
						}
					}
				}
			}
			if (this.mCachedPIEffects.ContainsKey(file_name))
			{
				for (int m = 0; m < this.mCachedPIEffects[file_name].Count; m++)
				{
					GenericCachedEffect genericCachedEffect = this.mCachedPIEffects[file_name][m];
					if (!genericCachedEffect.mInUse)
					{
						genericCachedEffect.mInUse = true;
						genericCachedEffect.mEffect.ResetAnim();
						return genericCachedEffect.mEffect;
					}
				}
			}
			string theFileName = string.Concat(new string[]
			{
				this.GetBaseResImagesDir(),
				"particles\\",
				file_name,
				"\\",
				file_name,
				".ppf"
			});
			PIEffect pieffect = new PIEffect();
			PIEffect pieffect2 = new PIEffect();
			if (!pieffect2.LoadEffect(theFileName))
			{
				return null;
			}
			pieffect = pieffect2;
			if (!create_copy)
			{
				return pieffect;
			}
			return pieffect.Duplicate();
		}

		public PIEffect GetPIEffect(string file_name)
		{
			return this.GetPIEffect(file_name, true);
		}

		public bool IsHardMode()
		{
			return false;
		}

		public MemoryImage GenerateLevelThumbnail(string thumb_path, Level l)
		{
			return null;
		}

		public bool IronFrogUnlocked()
		{
			return this.mUserProfile != null && this.mUserProfile.mAdvModeVars.mNumTimesZoneBeat[5] > 0;
		}

		public bool ChallengeModeUnlocked()
		{
			return this.mUserProfile != null && this.mUserProfile.mChallengeUnlockState[0, 0] > 0;
		}

		public bool HSScreenUnlocked()
		{
			return this.mUserProfile != null && this.mUserProfile.mAdvModeVars.mNumTimesZoneBeat[0] >= 1;
		}

		public void ReleaseTorchEffect(PIEffect fx)
		{
			if (fx == null)
			{
				return;
			}
			for (int i = 0; i < this.mCachedTorchEffects.size<CachedTorchEffect>(); i++)
			{
				CachedTorchEffect cachedTorchEffect = this.mCachedTorchEffects[i];
				if (cachedTorchEffect.mTorchFlame == fx)
				{
					cachedTorchEffect.mFlameInUse = false;
					return;
				}
				if (cachedTorchEffect.mTorchFlameOut == fx)
				{
					cachedTorchEffect.mFlameOutInUse = false;
					return;
				}
			}
		}

		public void ReleaseVolcanoEffect(PIEffect fx)
		{
			if (fx == null)
			{
				return;
			}
			for (int i = 0; i < this.mCachedVolcanoEffects.size<CachedVolcanoEffect>(); i++)
			{
				CachedVolcanoEffect cachedVolcanoEffect = this.mCachedVolcanoEffects[i];
				if (cachedVolcanoEffect.mProjectile == fx)
				{
					cachedVolcanoEffect.mProjectileInUse = false;
					return;
				}
				if (cachedVolcanoEffect.mExplosion == fx)
				{
					cachedVolcanoEffect.mExplosionInUse = false;
					return;
				}
			}
		}

		public void ReleaseGenericCachedEffect(PIEffect fx)
		{
			if (fx == null)
			{
				return;
			}
			foreach (KeyValuePair<string, List<GenericCachedEffect>> keyValuePair in this.mCachedPIEffects)
			{
				foreach (GenericCachedEffect genericCachedEffect in keyValuePair.Value)
				{
					if (genericCachedEffect.mEffect == fx)
					{
						genericCachedEffect.mInUse = false;
						break;
					}
				}
			}
		}

		public Board GetBoard()
		{
			return this.mBoard;
		}

		public bool ShowingLoadingScreen()
		{
			return this.mLoadingScreen != null;
		}

		public void IncFramesPlayed()
		{
			this.mFramesPlayed++;
		}

		public string GetResImagesDir()
		{
			return string.Format("images\\{0}\\", GameApp.mGameRes);
		}

		public string GetBaseResImagesDir()
		{
			return string.Format("images\\{0}\\", this.mResourceManager.mBaseArtRes);
		}

		public static int ScaleNum(int theNum, int theAdd)
		{
			return (int)((float)theNum * GameApp.mGameUpScale) + theAdd;
		}

		public static int ScaleNum(int theNum)
		{
			return GameApp.ScaleNum(theNum, 0);
		}

		public static float ScaleNum(float theNum, float theAdd)
		{
			return theNum * GameApp.mGameUpScale + theAdd;
		}

		public static float ScaleNum(float theNum)
		{
			return GameApp.ScaleNum(theNum, 0f);
		}

		public static double ScaleNum(double theNum, double theAdd)
		{
			return theNum * (double)GameApp.mGameUpScale + theAdd;
		}

		public static double ScaleNum(double theNum)
		{
			return GameApp.ScaleNum(theNum, 0.0);
		}

		public static int DownScaleNum(int theNum, int theAdd)
		{
			return (int)((float)theNum * GameApp.mGameDownScale) + theAdd;
		}

		public static int DownScaleNum(int theNum)
		{
			return GameApp.DownScaleNum(theNum, 0);
		}

		public static float DownScaleNum(float theNum, float theAdd)
		{
			return theNum * GameApp.mGameDownScale + theAdd;
		}

		public static float DownScaleNum(float theNum)
		{
			return GameApp.DownScaleNum(theNum, 0f);
		}

		public static double DownScaleNum(double theNum, double theAdd)
		{
			return theNum * (double)GameApp.mGameDownScale + theAdd;
		}

		public static double DownScaleNum(double theNum)
		{
			return GameApp.DownScaleNum(theNum, 0.0);
		}

		public static int ScreenScaleNum(int theNum, int theAdd)
		{
			return (int)((float)theNum * GameApp.mGameScreenScale) + theAdd;
		}

		public static int ScreenScaleNum(int theNum)
		{
			return GameApp.ScreenScaleNum(theNum, 0);
		}

		public static float ScreenScaleNum(float theNum, float theAdd)
		{
			return theNum * GameApp.mGameScreenScale + theAdd;
		}

		public static float ScreenScaleNum(float theNum)
		{
			return GameApp.ScreenScaleNum(theNum, 0f);
		}

		public static double ScreenScaleNum(double theNum, double theAdd)
		{
			return theNum * (double)GameApp.mGameScreenScale + theAdd;
		}

		public static double ScreenScaleNum(double theNum)
		{
			return GameApp.ScreenScaleNum(theNum, 0.0);
		}

		public virtual uint GetProfileVersion()
		{
			return 0U;
		}

		public virtual void NotifyProfileChanged(UserProfile player)
		{
		}

		public virtual UserProfile CreateUserProfile()
		{
			return new ZumaProfile();
		}

		public virtual void OnProfileLoad(UserProfile player, Buffer buffer)
		{
		}

		public virtual void OnProfileSave(UserProfile player, Buffer buffer)
		{
		}

		public Rect GetScreenRect()
		{
			return this.mWidgetManager.mMouseDestRect;
		}

		public int GetScreenWidth()
		{
			return this.mWidgetManager.mMouseDestRect.mWidth - this.mWidgetManager.mMouseDestRect.mX;
		}

		public static bool IsTablet()
		{
			return true;
		}

		public Image GetLevelThumbnail(int theLevelNum)
		{
			Image second = this.mLevelThumbnails[theLevelNum].second;
			string[] array = new string[] { "jungle", "village", "city", "coast", "grotto", "volcano" };
			if (second == null)
			{
				int num = theLevelNum / 10;
				int num2 = theLevelNum % 10 + 1;
				string text = array[num] + string.Format("{0}", num2);
				string theFileName = "levelthumbs\\" + text + "_thumb";
				IdxThumbPair idxThumbPair = this.mLevelThumbnails[theLevelNum];
				idxThumbPair.second = this.GetImage(theFileName, true, true, false);
				if (idxThumbPair.second != null)
				{
					second = idxThumbPair.second;
				}
			}
			return second;
		}

		public void DeleteLevelThumbnail(int theLevel)
		{
			if (theLevel >= 0 && theLevel <= Enumerable.Count<IdxThumbPair>(this.mLevelThumbnails))
			{
				IdxThumbPair idxThumbPair = this.mLevelThumbnails[theLevel];
				if (idxThumbPair.second != null)
				{
					idxThumbPair.second.Dispose();
					idxThumbPair.second = null;
				}
			}
		}

		public void DeleteZoneThumbnails(int theZone)
		{
			if (theZone >= 0 && theZone <= 6)
			{
				int num = theZone * 10;
				for (int i = 0; i < 10; i++)
				{
					IdxThumbPair idxThumbPair = this.mLevelThumbnails[num + i];
					if (idxThumbPair.second != null)
					{
						idxThumbPair.second.Dispose();
						idxThumbPair.second = null;
					}
				}
			}
		}

		public void LoadAllThumbnails()
		{
			for (int i = 0; i < 6; i++)
			{
				int num = i * 10;
				for (int j = 0; j < 10; j++)
				{
					this.GetLevelThumbnail(num + j);
				}
			}
		}

		public void AppEnteredBackground()
		{
			if (this.mBoard != null && this.mBoard.NeedSaveGame() && this.mUserProfile != null)
			{
				this.mBoard.SaveGame(this.mUserProfile.GetSaveGameName(this.IsHardMode()), null);
			}
		}

		public override double GetLoadingThreadProgress()
		{
			return (double)this.mResourceManager.GetLoadResourcesListProgress(GameApp.gInitialLoadGroups);
		}

		public void ToggleBambooTransition()
		{
			if (this.mBambooTransition == null)
			{
				return;
			}
			if (!this.mBambooTransition.IsInProgress())
			{
				this.mBambooTransition.Reset();
				this.mBambooTransition.SetVisible(true);
				this.mBambooTransition.SetDisabled(false);
				this.mWidgetManager.AddWidget(this.mBambooTransition);
				this.mWidgetManager.BringToFront(this.mBambooTransition);
				this.mBambooTransition.StartTransition();
			}
		}

		public void BambooTransitionOpened()
		{
			this.mBambooTransition.Reset();
			this.mBambooTransition.SetVisible(false);
			this.mBambooTransition.SetDisabled(true);
			this.mWidgetManager.RemoveWidget(this.mBambooTransition);
		}

		public void EndChallengeModeGame()
		{
			this.EndCurrentGame();
			this.ShowChallengeSelector();
		}

		public void InitMetricsManager()
		{
		}

		public void HideHelp()
		{
			OptionsDialog optionsDialog = base.GetDialog(2) as OptionsDialog;
			if (optionsDialog != null)
			{
				optionsDialog.OnHelpHided();
			}
		}

		public void ShowAbout()
		{
			this.mAboutInfo = new AboutInfo();
			this.AddDialog(this.mAboutInfo);
		}

		public void HideAbout()
		{
			this.mAboutInfo.mDrawScale.SetCurve(ZumasRevenge.Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mAboutInfo.mWidgetFlagsMod.mRemoveFlags |= 16;
			this.mAboutInfo = null;
		}

		public void ShowLegal()
		{
			this.mLegalInfo = new LegalInfo();
			this.AddDialog(this.mLegalInfo);
		}

		public void HideLegal()
		{
			this.mLegalInfo.mDrawScale.SetCurve(ZumasRevenge.Common._MP("b+0,1,0.05,1,~###         ~#A5t"));
			this.mLegalInfo.mWidgetFlagsMod.mRemoveFlags |= 16;
			this.mLegalInfo = null;
		}

		public void ShowMetricsDebug()
		{
		}

		public void HideMetricsDebug()
		{
		}

		public void ReportAppLaunchInfo(int theAppEvent)
		{
		}

		public void ReportEndOfLevelMetrics(Board theBoard, bool theLevelSuccess, bool theAcedLevel)
		{
		}

		public void ReportEndOfLevelMetrics(Board theBoard, bool theLevelSuccess)
		{
			this.ReportEndOfLevelMetrics(theBoard, theLevelSuccess, false);
		}

		public void ReportEndOfLevelMetrics(Board theBoard)
		{
			this.ReportEndOfLevelMetrics(theBoard, false, false);
		}

		public void CheckForAppUpdate()
		{
		}

		public void GetTouchInputOffset(ref int x, ref int y)
		{
			x = this.mTouchOffsetX;
			y = this.mTouchOffsetY;
		}

		public void SetTouchInputOffset(int x, int y)
		{
			this.mTouchOffsetX = x;
			this.mTouchOffsetY = y;
		}

		public void LoadLevelXML()
		{
			this.mLoadingProc = new ThreadStart(this.LoadingLevel);
			this.mLoadingThread = new Thread(this.mLoadingProc);
			this.mLoadLevelSuccess = false;
			this.mLoadingThread.Start();
		}

		private void LoadingLevel()
		{
			this.mNormalLevelMgr = ((XNAFileDriver)this.mFileDriver).GetContentManager().Load<LevelMgr>(this.mLevelXML);
			this.mNormalLevelMgr.Init();
			this.mNormalLevelMgr.mLevelXML = this.mLevelXML;
			this.mLoadLevelSuccess = true;
		}

		public void OpenURL(string url)
		{
			try
			{
				new WebBrowserTask
				{
					Uri = new Uri(url)
				}.Show();
			}
			catch (Exception)
			{
			}
		}

		public void HandleGameUpdateRequired(GameUpdateRequiredException ex)
		{
			GameApp.UN_UPDATE_VERSION = true;
			GameApp.USE_XBOX_SERVICE = false;
			GameApp.mDisplayTitleUpdateMessage = true;
			GameApp.DisplayTitleUpdateMessage();
		}

		public static void DisplayTitleUpdateMessage()
		{
			List<string> list = new List<string>();
			string @string = TextManager.getInstance().getString(446);
			string string2 = TextManager.getInstance().getString(447);
			list.Add(string2);
			list.Add(@string);
			if (GameApp.mDisplayTitleUpdateMessage && !Guide.IsVisible)
			{
				GameApp.mDisplayTitleUpdateMessage = false;
				string string3 = TextManager.getInstance().getString(62);
				Guide.BeginShowMessageBox("   ", string3, list, 1, 3, new AsyncCallback(GameApp.UpdateDialogGetMBResult), null);
			}
		}

		public static void UpdateDialogGetMBResult(IAsyncResult userResult)
		{
			int? num = Guide.EndShowMessageBox(userResult);
			if (num != null && num.Value > 0)
			{
				if (Guide.IsTrialMode)
				{
					Guide.ShowMarketplace(0);
					return;
				}
				new MarketplaceDetailTask
				{
					ContentType = 1,
					ContentIdentifier = "43f34364-9df4-4d95-b9cf-e48b3c85cda9"
				}.Show();
			}
		}

		public void ToMarketPlace()
		{
			if (Guide.IsTrialMode)
			{
				Guide.ShowMarketplace(0);
				return;
			}
			new MarketplaceDetailTask
			{
				ContentType = 1
			}.Show();
		}

		public static void initResolution(int param1)
		{
			GameApp.mGameRes = param1;
			int num = GameApp.mGameRes;
			if (num <= 640)
			{
				if (num == 320)
				{
					GameApp.mGameUpScale = 0.5333334f;
					GameApp.mGameDownScale = 0.2666667f;
					GameApp.mGameScreenScale = 1f / GameApp.mGameUpScale;
					return;
				}
				if (num == 600)
				{
					GameApp.mGameUpScale = 1f;
					GameApp.mGameDownScale = 0.5f;
					GameApp.mGameScreenScale = 1f;
					return;
				}
				if (num == 640)
				{
					GameApp.mGameUpScale = 1.06666684f;
					GameApp.mGameDownScale = 0.5333334f;
					GameApp.mGameScreenScale = 1f / GameApp.mGameUpScale;
					return;
				}
			}
			else
			{
				if (num == 720)
				{
					GameApp.mGameUpScale = 1.2f;
					GameApp.mGameDownScale = 0.6f;
					GameApp.mGameScreenScale = 1f / GameApp.mGameUpScale;
					return;
				}
				if (num == 768)
				{
					GameApp.mGameUpScale = 1.28f;
					GameApp.mGameDownScale = 0.64f;
					GameApp.mGameScreenScale = 1f / GameApp.mGameUpScale;
					return;
				}
			}
			GameApp.mGameUpScale = 2f;
			GameApp.mGameDownScale = 1f;
			GameApp.mGameScreenScale = 0.5f;
		}

		public void SetOrientation(int Orientation)
		{
			((WP7AppDriver)this.mAppDriver).SetOrientation(Orientation);
		}

		// Note: this type is marked as 'beforefieldinit'.
		static GameApp()
		{
			string[] array = new string[5];
			array[0] = "Init";
			array[1] = "CommonGame";
			array[2] = "GamePlay";
			array[3] = "MenuRelated";
			GameApp.gInitialLoadGroups = array;
		}

		public static bool USE_TRIAL_VERSION = false;

		public static bool NONE_XBOX_LIVE = false;

		public static bool UN_UPDATE_VERSION = false;

		public static bool USE_XBOX_SERVICE = false;

		public WebBrowserTask mWbt;

		public bool mWaitForActive;

		public bool mInitFinished;

		public static bool mDisplayTitleUpdateMessage = false;

		public static bool mExit = false;

		public Image mBackgroundLayer;

		public Thread mInitThread;

		public static GameApp gApp = null;

		public static DDS gDDS = null;

		public static int gSaveGameVersion = 197;

		public static int gNumOptionalGroups = 8;

		public static string[] gOptionalGroups = new string[] { "CommonBoss", "Boss1", "Boss2", "Boss3", "Boss4", "Boss5", "Boss6Common", "GrottoSounds" };

		public static string gOrgTitle = "";

		public static bool gDidCrashHandler = false;

		public static string gMetricsVersion = "1.0";

		public static int gScreenShakeX = 0;

		public static int gScreenShakeY = 0;

		public static int gLastLevel = 0;

		public static int gLastZone = -1;

		public static bool gNeedsPreCache = true;

		private static int InGameLoadThread_DrawFunc_CallCounter = 0;

		private static int gAddingDlgID = -12345;

		public static bool gInitialProfLoadSuccessful;

		private static string[] gInitialLoadGroups;

		private GameApp.PreBlockCallback mDialogCallBack;

		public Game mGameMain;

		public AutoMonkey mAutoMonkey;

		public bool mSavingOrLoadingProfile;

		public float mShotCorrectionAngleToWidthDist;

		public float mShotCorrectionAngleMax;

		public float mShotCorrectionWidthMax;

		public int mGuideStyle;

		public int mShotCorrectionDebugStyle;

		public bool mIronFrogModeIncluded;

		public Board mBoard;

		public LoadingScreen mLoadingScreen;

		public LevelMgr mNormalLevelMgr;

		public Dictionary<string, List<GenericCachedEffect>> mCachedPIEffects = new Dictionary<string, List<GenericCachedEffect>>();

		public MapScreenHackWidget mMapScreenHackWidget;

		public Rect mLoadRect = default(Rect);

		public ZumaDialog mReturnToMMDlg;

		public Dictionary<int, DialogHideInfo> mDialogHideInfoMap;

		public bool mDoingDRM;

		public int mTrialType;

		public int mFramesPlayed;

		public int mCachedLoadState;

		public bool mCachedLoad;

		public bool mInitialLoad;

		public bool mDelayIntro;

		public int mWideScreenXOffset;

		public long mLastMoreGamesUpdate;

		public int mIFLoadingAnimStartCel;

		public Upsell mUpsell;

		public List<CachedTorchEffect> mCachedTorchEffects = new List<CachedTorchEffect>();

		public List<CachedVolcanoEffect> mCachedVolcanoEffects = new List<CachedVolcanoEffect>();

		public Dictionary<string, PIEffect> mPIEffectMap = new Dictionary<string, PIEffect>();

		public bool mClickedHardMode;

		public bool mInGameLoadThreadProcFailed;

		public bool mStartInGameModeThreadProcRunning;

		public bool mContinuedGame;

		public int mForceZoneRestart;

		public string mChallengeLevelId = "";

		public UnderDialogWidget mUnderDialogWidget;

		public float mDialogObscurePct;

		public Dictionary<string, CompositionMgr> mPreloadedComps = new Dictionary<string, CompositionMgr>();

		public Credits mCredits;

		public CreditsHackWidget gCreditsHackWidget;

		public GenericHelp mGenericHelp;

		public MapScreen mMapScreen;

		public List<IdxThumbPair> mLevelThumbnails = new List<IdxThumbPair>();

		public ProxBombManager mProxBombManager;

		public Music mMusic;

		public SoundEffects mSoundPlayer;

		public ZumaProfile mUserProfile;

		public ZumaProfileMgr mProfileMgr;

		public MainMenu mMainMenu;

		public MoreGames mMoreGames;

		public NewUserDialog mNewUserDlg;

		public string mLevelXML;

		public string mHardLevelXML;

		public static string mCompositionResPrefix;

		public bool mHiRes;

		public static int mGameRes;

		public static float mGameDownScale;

		public static float mGameUpScale;

		public static float mGameScreenScale;

		public bool mReInit;

		public bool mFromReInit;

		public bool mDoingAdvModeLoad;

		public int mConfTime;

		public int mLoadType;

		public bool mColorblind;

		public bool mCursorTarget;

		public string mTimeStamp;

		public BambooTransition mBambooTransition;

		public string m_DefaultProfileName = "DewinterWang";

		public string m_DefaultName = "DewinterWang";

		public LegalInfo mLegalInfo;

		public AboutInfo mAboutInfo;

		public WidescreenBoardWidget mWidescreenBoardWidget;

		public Profile m_Profile = new Profile();

		public GameApp.YesNoDialogDelegate mYesNoDialogDelegate;

		public ZumaDialog mDialog;

		public int mTouchOffsetX;

		public int mTouchOffsetY;

		private Thread mLoadingThread;

		private ThreadStart mLoadingProc;

		public bool mLoadLevelSuccess;

		public bool StartLoadingComplete;

		public int mBoardOffsetX = 85;

		public int mBoardUIOffsetX = 53;

		public int mOffset160X = 160;

		protected GameApp.EXLiveWaiting m_XLiveState = GameApp.EXLiveWaiting.E_NONE;

		public delegate void YesNoDialogDelegate(int buttonId);

		public delegate void PreBlockCallback();

		public enum Metrics_AppEventType
		{
			Metrics_AppEvent_StartNormal = 1,
			Metrics_AppEvent_StartUpgrade,
			Metrics_AppEvent_StartInstall,
			Metrics_AppEvent_MovedToForeground,
			Metrics_AppEvent_StartFromPushNotification
		}

		public enum EXLiveWaiting
		{
			E_NONE,
			E_WaitingForSignIn,
			E_WaitingForAchivements,
			E_Ready
		}
	}
}
