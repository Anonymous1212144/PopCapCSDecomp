using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using BejeweledLivePlus.Audio;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Localization;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.UI;
using BejeweledLivePlus.Widget;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using SexyFramework;
using SexyFramework.Drivers.App;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Sound;
using SexyFramework.Widget;

namespace BejeweledLivePlus
{
	public class BejeweledLivePlusApp : SexyApp, PopAnimListener, IDisposable
	{
		public int ElapsedTime { get; set; }

		public BejeweledLivePlusApp(Game xnaGame)
		{
			string[] array = new string[44];
			array[0] = "Common";
			array[1] = "Fonts";
			array[2] = "MainMenu";
			array[3] = "GamePlay";
			array[4] = "HyperspaceWhirlpool_Common";
			array[5] = "HyperspaceWhirlpool_Normal";
			array[6] = "AwardGlow";
			array[7] = "GamePlay_UI_Normal";
			array[8] = "GamePlay_UI_Dig_Common";
			array[9] = "GamePlay_UI_Dig";
			array[10] = "GamePlayQuest_Lightning";
			array[11] = "GamePlayQuest_Dig";
			array[12] = "GamePlayQuest_Butterfly_Common";
			array[13] = "GamePlayQuest_Butterfly";
			array[14] = "Badges";
			array[15] = "BADGES_BIG_ELITE";
			array[16] = "BADGES_BIG_BRONZE";
			array[17] = "BADGES_BIG_SILVER";
			array[18] = "BADGES_BIG_GOLD";
			array[19] = "BADGES_BIG_PLATINUM";
			array[20] = "BADGES_BIG_LEVELORD";
			array[21] = "BADGES_BIG_BEJEWELER";
			array[22] = "BADGES_BIG_DIAMOND_MINE";
			array[23] = "BADGES_BIG_RELIC_HUNTER";
			array[24] = "BADGES_BIG_ELECTRIFIER";
			array[25] = "BADGES_BIG_HIGH_VOLTAGE";
			array[26] = "BADGES_BIG_BUTTERFLY_MONARCH";
			array[27] = "BADGES_BIG_BUTTERFLY_BONANZA";
			array[28] = "BADGES_BIG_CHROMATIC";
			array[29] = "BADGES_BIG_STELLAR";
			array[30] = "BADGES_BIG_BLASTER";
			array[31] = "BADGES_BIG_SUPERSTAR";
			array[32] = "BADGES_BIG_CHAIN_REACTION";
			array[33] = "BADGES_BIG_LUCKY_STREAK";
			array[34] = "Help_Basic";
			array[35] = "Help_Bfly";
			array[36] = "Help_DiamondMine";
			array[37] = "Help_Lightning";
			array[38] = "Help_Bfly";
			array[39] = "ProfilePic_0";
			array[40] = "ZenOptions";
			array[41] = "BadgesGrayIcon";
			array[42] = "AtlasEx";
			this.initialLoadGroups = array;
			this.mVersion = GlobalMembers.Version;
			this.mMenus = new Bej3Widget[20];
			this.mVoiceVolume = 0.85;
			this.mZenAmbientVolume = 0.85;
			this.mZenAmbientMusicVolume = 0.5;
			this.mZenBinauralVolume = 0.5;
			this.mZenBreathVolume = 0.5;
			this.mWebRoot = string.Empty;
			this.mResForced = true;
			this.mTestBkg = "";
			this.mForceBkg = "";
			this.mStatsDumpBuffer = new Buffer();
			this.mBoostCosts = new int[5];
			this.mLinkWarningLocation = string.Empty;
			this.mBuyCoinsURL = string.Empty;
			this.mClientId = string.Empty;
			this.mQuestObjPIEffects = new Dictionary<int, PIEffect>[BejeweledLivePlusAppConstants.NUM_QUEST_SETS];
			this.mShrunkenGems = new Image[7, 15];
			this.mAlphaImages = new Dictionary<MemoryImage, MemoryImage>();
			this.mSwitchProfileName = string.Empty;
			this.mHighScoreMgr = new HighScoreMgr();
			this.mRestartRT = new SharedRenderTarget();
			this.mLastUser = "";
			this.mGems3DListener = new Bej3P3DListener[7];
			this.mGems3D = new Mesh[7];
			this.mAffirmationFiles = new List<string>();
			this.mSBAFiles = new List<string>();
			this.mAmbientFiles = new List<string>();
			this.mTips = new List<string>();
			this.mRankNames = new List<string>();
			this.mBadgeCutoffs = new List<List<int>>();
			this.mBadgeSecondaryCutoffs = new List<List<int>>();
			this.mQueuedStatsCalls = string.Empty;
			this.gInterfaceDefinitions = new List<Menu_Type>[17];
			this.LastSfxId = -1;
			base..ctor();
			GlobalMembers.gApp = this;
			GlobalMembers.gGameMain = xnaGame;
			((WP7AppDriver)this.mAppDriver).InitXNADriver(xnaGame);
			this.mFileDriver.InitFileDriver(this);
			GlobalMembers.gGR = new GraphicsRecorder();
			this.mArtRes = 960;
			this.mShowBackground = true;
			this.mDialogObscurePct = 0f;
			this.mDoFadeBackForDialogs = false;
		}

		public void HandleGameUpdateRequired(GameUpdateRequiredException ex)
		{
			GameMain gameMain = (GameMain)GlobalMembers.gGameMain;
			gameMain.GamerService.Enabled = false;
			this.mDisplayTitleUpdate = true;
		}

		private void DisplayTitleUpdate()
		{
			if (!Guide.IsVisible)
			{
				this.mDisplayTitleUpdate = false;
				List<string> list = new List<string>();
				list.Add(GlobalMembers._ID("No", 7702));
				list.Add(GlobalMembers._ID("Yes", 7701));
				Guide.BeginShowMessageBox(GlobalMembers._ID("Title Update Available", 7703), GlobalMembers._ID("An update is available! This update is required to connect to Xbox LIVE. Update now?", 7704), list, 1, 3, new AsyncCallback(this.UpdateDialogGetResult), null);
			}
		}

		protected void UpdateDialogGetResult(IAsyncResult result)
		{
			int? num = Guide.EndShowMessageBox(result);
			if (num != null && num.Value > 0)
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
		}

		private void dumpStringResource()
		{
			string name = CultureInfo.CurrentCulture.Name;
			if (name == "de-DE" || name == "es-ES" || name == "fr-FR" || name == "it-IT" || name == "en-US")
			{
				string fileName = string.Format("Strings.{0}.resx", name);
				if (name == "en-US")
				{
					fileName = "Strings.resx";
				}
				this.mPopLoc.dumpLocalizedTextResource(fileName);
			}
		}

		public override void Init()
		{
			base.Init();
			this.InitStepLocalization();
			this.InitStepLoadResources();
			this.InitStepPrepareCurvedVal();
			this.SetUpInterfaceStateDefinitions();
			while (!this.mResourceManager.IsResourceLoaded("IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED"))
			{
				Thread.Sleep(200);
			}
			if (!GlobalMembersResourcesWP.ExtractInitResources(this.mResourceManager))
			{
				this.ShowResourceError(true);
			}
			if (!GlobalMembersResourcesWP.ExtractLoader_960Resources(this.mResourceManager))
			{
				this.ShowResourceError(true);
			}
			this.mMusic = new Music(this.mMusicInterface);
			this.mMusic.RegisterCallBack();
			this.mSoundPlayer = new SoundEffects(this.mSoundManager);
			this.mMusic.LoadMusic(0, "music\\LoadingScreen");
			this.mMusic.LoadMusic(1, "music\\MainMenu");
			this.mUnderDialogWidget = new UnderDialogWidget();
			this.mUnderDialogWidget.Resize(0, 0, this.mWidth, this.mHeight);
			this.mWidgetManager.AddWidget(this.mUnderDialogWidget);
			this.mUnderDialogWidget.SetVisible(false);
			this.mUnderDialogWidget.CreateImages();
			this.mMainMenu = new MainMenu();
			this.mMainMenu.Resize(new Rect(0, 0, this.mWidth, this.mHeight));
			this.mWidgetManager.AddWidget(this.mMainMenu);
			this.mWidgetManager.SetFocus(this.mMainMenu);
			this.mMenus[0] = new MenuBackground();
			this.mWidgetManager.AddWidget(this.mMenus[0]);
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_LOADING);
		}

		private void InitStepLocalization()
		{
			string text;
			if (Strings.Culture.TwoLetterISOLanguageName == "fr")
			{
				text = "FR-FR";
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "de")
			{
				text = "DE-DE";
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "es")
			{
				text = "ES-ES";
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "it")
			{
				text = "IT-IT";
			}
			else
			{
				text = "EN-US";
			}
			this.mResourceManager.mCurLocSet = (uint)(((uint)text.get_Chars(0) << 24) | ((uint)text.get_Chars(1) << 16) | ((uint)text.get_Chars(3) << 8) | text.get_Chars(4));
			new LocalizedString();
		}

		private void InitStepLoadResources()
		{
			Res.InitResources(this);
			this.mResourceManager.mBaseArtRes = 1200;
			this.mResourceManager.mCurArtRes = 960;
			this.mResourceManager.PrepareLoadResourcesList(this.initialLoadGroups);
		}

		private void InitStepLoadProperties()
		{
			if (!this.LoadProperties(this.mResourceManager.GetLocaleFolder(true) + "properties\\defaultFramework.xml", true, false, false))
			{
				this.LoadProperties("properties\\defaultFramework.xml", true, false, false);
			}
			if (!this.LoadProperties(this.mResourceManager.GetLocaleFolder(true) + "properties\\defaultUIConstants.xml", true, false, false))
			{
				this.LoadProperties("properties\\defaultUIConstants.xml", true, false, false);
			}
			if (!this.LoadProperties(this.mResourceManager.GetLocaleFolder(true) + "properties\\defaultFilenames.xml", true, false, false))
			{
				this.LoadProperties("properties\\defaultFilenames.xml", true, false, false);
			}
		}

		private void InitStepLoadConfigs()
		{
			BejeweledLivePlus.Misc.Common.SRand((uint)DateTime.Now.ToFileTime());
			this.LoadConfigs();
		}

		private void InitStepPrepareCurvedVal()
		{
			this.mCurveValCache = new PreCalculatedCurvedValManager();
		}

		private void InitStepLoadExtraConfigs()
		{
			EncodingParser encodingParser = new EncodingParser();
			StringBuilder stringBuilder = new StringBuilder();
			char c = '\0';
			if (!encodingParser.OpenFile(this.mResourceManager.GetLocaleFolder(true) + "properties\\tips.txt") && !encodingParser.OpenFile("properties\\tips.txt"))
			{
				this.Popup("Failed to open properties\\tips.txt");
			}
			while (encodingParser.GetChar(ref c) == EncodingParser.GetCharReturnType.SUCCESSFUL || stringBuilder.Length > 0)
			{
				if (c == '\n' || c == '\r' || encodingParser.EndOfFile())
				{
					string text = stringBuilder.ToString().Trim();
					if (text.Length > 0)
					{
						this.mTips.Add(text);
					}
					stringBuilder.Clear();
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			EncodingParser encodingParser2 = new EncodingParser();
			if (!encodingParser2.OpenFile(this.mResourceManager.GetLocaleFolder(true) + "properties\\ranks.txt") && !encodingParser2.OpenFile("properties\\ranks.txt"))
			{
				this.Popup("Failed to open properties\\ranks.txt");
			}
			stringBuilder.Clear();
			while (encodingParser2.GetChar(ref c) == EncodingParser.GetCharReturnType.SUCCESSFUL || stringBuilder.Length > 0)
			{
				if (c == '\n' || c == '\r' || encodingParser2.EndOfFile())
				{
					string text2 = stringBuilder.ToString().Trim();
					if (text2.Length > 0)
					{
						this.mRankNames.Add(text2);
					}
					stringBuilder.Clear();
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
		}

		private void InitStepLoadProfile()
		{
			this.mProfile = new Profile();
			string theProfileName = "Test";
			this.mProfile.ReadProfileList(ref theProfileName);
			this.RegistryReadString("LastUser", ref theProfileName);
			if (!this.mProfile.LoadProfile(theProfileName, false) && !this.mProfile.GetAnyProfile())
			{
				this.mProfile.CreateProfile("Player", true);
				this.mProfile.LoadProfile("Player", true);
			}
		}

		private void InitStepLoadHighScores()
		{
			this.LoadHighscores();
		}

		private void InitStepSetupMusics()
		{
			this.mAffirmationFiles.Add("General.txt");
			this.mAffirmationFiles.Add("Positive Thinking.txt");
			this.mAffirmationFiles.Add("Prosperity.txt");
			this.mAffirmationFiles.Add("Quit Bad Habits.txt");
			this.mAffirmationFiles.Add("Self Confidence.txt");
			this.mAffirmationFiles.Add("Weight Loss.txt");
			this.mAmbientFiles.Add("Coastal");
			this.mAmbientFiles.Add("Crickets");
			this.mAmbientFiles.Add("Forest");
			this.mAmbientFiles.Add("Ocean Surf");
			this.mAmbientFiles.Add("Rain Leaves");
			this.mAmbientFiles.Add("Waterfall");
			this.mMusic.LoadMusic(2, "music\\Classic");
			this.mMusic.LoadMusic(3, "music\\Classic_lose");
			this.mMusic.LoadMusic(4, "music\\Zen");
			this.mMusic.LoadMusic(13, "music\\Diamond_mine");
			this.mMusic.LoadMusic(14, "music\\Diamond_mine_lose");
			this.mMusic.LoadMusic(5, "music\\Butterfly");
			this.mMusic.LoadMusic(6, "music\\Butterfly_lose");
			this.mMusic.LoadMusic(11, "music\\Lightning");
			this.mMusic.LoadMusic(12, "music\\Lightning_end");
		}

		private void InitStepSetupToolTipMgr()
		{
			this.mTooltipManager = new TooltipManager();
			this.mTooltipManager.Resize(0, 0, GlobalMembers.S(1600), GlobalMembers.S(1200));
			this.mTooltipManager.mZOrder = 10;
		}

		private void InitStepPreInitEffects()
		{
			this.PreInitEffects();
		}

		private void PrepareInitSteps()
		{
			if (this.initSteps_ == null)
			{
				this.initSteps_ = new Dictionary<string, BejeweledLivePlusApp.InitStepFunc>();
				this.initSteps_["LOAD_PROPERTIES"] = new BejeweledLivePlusApp.InitStepFunc(this.InitStepLoadProperties);
				this.initSteps_["LOAD_CONFIGS"] = new BejeweledLivePlusApp.InitStepFunc(this.InitStepLoadConfigs);
				this.initSteps_["LOAD_EXTRACONFIGS"] = new BejeweledLivePlusApp.InitStepFunc(this.InitStepLoadExtraConfigs);
				this.initSteps_["LOAD_PROFILE"] = new BejeweledLivePlusApp.InitStepFunc(this.InitStepLoadProfile);
				this.initSteps_["LOAD_HIGHSCORES"] = new BejeweledLivePlusApp.InitStepFunc(this.InitStepLoadHighScores);
				this.initSteps_["SETUP_TOOLTIPMGR"] = new BejeweledLivePlusApp.InitStepFunc(this.InitStepSetupToolTipMgr);
				this.initSteps_["PREINIT_EFFECT"] = new BejeweledLivePlusApp.InitStepFunc(this.InitStepPreInitEffects);
				this.initSteps_["SETUP_MUSICS"] = new BejeweledLivePlusApp.InitStepFunc(this.InitStepSetupMusics);
			}
		}

		public void DoInitWhileLoading()
		{
			this.PrepareInitSteps();
			foreach (string text in this.initSteps_.Keys)
			{
				if (this.initSteps_[text] != null)
				{
					this.initSteps_[text]();
					this.initSteps_.Remove(text);
					break;
				}
			}
		}

		private void PrepareResExtractor()
		{
			if (this.resExtract_ == null)
			{
				this.resExtract_ = new Dictionary<string, BejeweledLivePlusApp.ExtractResourceFunc>();
				this.resExtract_["IMAGE_ALPHA_ALPHA_UP"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractCommon_960Resources);
				this.resExtract_["IMAGE_LEVELBAR_ENDPIECE"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlay_UI_Normal_960Resources);
				this.resExtract_["IMAGE_GEMSNORMAL_BLUE"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlay_960Resources);
				this.resExtract_["IMAGE_ARROW_GLOW"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractMainMenu_960Resources);
				this.resExtract_["IMAGE_INGAMEUI_LIGHTNING_TIMER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlayQuest_Lightning_960Resources);
				this.resExtract_["IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Common_960Resources);
				this.resExtract_["IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Normal_960Resources);
				this.resExtract_["IMAGE_AWARD_GLOW"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractAwardGlow_960Resources);
				this.resExtract_["SOUND_ZEN_NECKLACE_4"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractCommon_CommonResources);
				this.resExtract_["POPANIM_FLAMEGEMEXPLODE"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlay_CommonResources);
				this.resExtract_["POPANIM_QUEST_DIG_COGS"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlay_UI_Dig_CommonResources);
				this.resExtract_["IMAGE_QUEST_DIG_COGS_COGS_96X96_2"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlay_UI_Dig_960Resources);
				this.resExtract_["IMAGE_WALLROCKS_SMALL_BROWN"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlayQuest_Dig_960Resources);
				this.resExtract_["POPANIM_ANIMS_LARGE_SPIDER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlayQuest_Butterfly_CommonResources);
				this.resExtract_["IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_71X36"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractGamePlayQuest_Butterfly_960Resources);
				this.resExtract_["SOUND_VOICE_WELCOMETOBEJEWELED"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractCommon_ENUSResources);
				this.resExtract_["IMAGE_BADGES_SMALL_UNKNOWN"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBadges_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_ELITE"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_ELITE_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_BRONZE"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_BRONZE_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_SILVER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_SILVER_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_GOLD"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_GOLD_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_PLATINUM"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_PLATINUM_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_LEVELORD"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_LEVELORD_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_BEJEWELER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_BEJEWELER_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_DIAMOND_MINE"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_DIAMOND_MINE_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_RELIC_HUNTER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_RELIC_HUNTER_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_ELECTRIFIER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_ELECTRIFIER_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_HIGH_VOLTAGE"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_HIGH_VOLTAGE_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_BUTTERFLY_MONARCH"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_MONARCH_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_BUTTERFLY_BONANZA"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_BONANZA_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_CHROMATIC"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_CHROMATIC_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_STELLAR"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_STELLAR_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_BLASTER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_BLASTER_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_SUPERSTAR"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_SUPERSTAR_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_CHAIN_REACTION"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_CHAIN_REACTION_960Resources);
				this.resExtract_["IMAGE_BADGES_BIG_LUCKY_STREAK"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBADGES_BIG_LUCKY_STREAK_960Resources);
				this.resExtract_["POPANIM_HELP_STARGEM"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractHelp_Basic_CommonResources);
				this.resExtract_["POPANIM_HELP_BFLY_SPIDER"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractHelp_Bfly_CommonResources);
				this.resExtract_["POPANIM_HELP_DIAMOND_GOLD"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractHelp_DiamondMine_CommonResources);
				this.resExtract_["POPANIM_HELP_LIGHTNING_SPEED"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractHelp_Lightning_CommonResources);
				this.resExtract_["IMAGE_PP29"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractProfilePic_0Resources);
				this.resExtract_["IMAGE_ZEN_OPTIONS_WEIGHT_LOSS"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractZenOptions_960Resources);
				this.resExtract_["BADGES_GRAY_ICON"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractBadgesGrayIconResources);
				this.resExtract_["LR_LOADING"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractLRLoadingResources);
				this.resExtract_["ATLAS_EX"] = new BejeweledLivePlusApp.ExtractResourceFunc(GlobalMembersResourcesWP.ExtractAtlasExResources);
			}
		}

		public void ExtractResources()
		{
			this.PrepareResExtractor();
			List<string> list = new List<string>();
			foreach (string text in this.resExtract_.Keys)
			{
				if (this.mResourceManager.IsResourceLoaded(text) && this.resExtract_[text] != null)
				{
					this.resExtract_[text](this.mResourceManager);
					list.Add(text);
				}
			}
			foreach (string text2 in list)
			{
				this.resExtract_.Remove(text2);
			}
		}

		public void PreInitEffects()
		{
			Effect.initPool();
			PopAnimEffect.initPool();
			ParticleEffect.initPool();
			BlingParticleEffect.initPool();
			GemCollectEffect.initPool();
			LightningBarFillEffect.initPool();
			TimeBonusEffect.initPool();
			ButterflyEffect.initPool();
			CoinFlyEffect.initPool();
			PointsEffect.initPool();
			TextNotifyEffect.initPool();
			SpeedCollectEffect.initPool();
			TimeBonusEffect.batchInit();
		}

		public override bool Update(int gameTime)
		{
			this.ElapsedTime = gameTime;
			for (int i = 0; i < this.mPendingVoice.Count; i++)
			{
				bool flag = true;
				VoicePlayArgs voicePlayArgs = this.mPendingVoice[i];
				if (voicePlayArgs.Condition != null)
				{
					voicePlayArgs.Condition.update();
				}
				if (voicePlayArgs.Condition != null && !voicePlayArgs.Condition.shouldActivate())
				{
					flag = false;
				}
				if (flag)
				{
					this.PlayVoice(voicePlayArgs.SoundID, voicePlayArgs.Pan, voicePlayArgs.Volume, voicePlayArgs.InterruptID);
					this.mPendingVoice.RemoveAt(i);
					i--;
				}
			}
			return base.Update(gameTime);
		}

		public override void Draw(int gameTime)
		{
			base.Draw(gameTime);
			if (this.mDisplayTitleUpdate)
			{
				this.DisplayTitleUpdate();
			}
		}

		public bool WantExit
		{
			get
			{
				return this.mWantExit;
			}
			set
			{
				this.mWantExit = value;
			}
		}

		public void OnActivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.OnActived();
				this.mMusicInterface.ResumeAllMusic();
			}
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_PAUSEMENU && this.mMenus[7].mTargetPos == ConstantsWP.MENU_Y_POS_HIDDEN && this.mMenus[7].mTopButton.mType != Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED)
			{
				this.mMenus[7].ButtonDepress(10001);
			}
		}

		public void OnDeactivated()
		{
			if (this.mMusicInterface != null)
			{
				this.mMusicInterface.PauseAllMusic();
				this.mMusicInterface.OnDeactived();
			}
			if (this.mBoard != null && GlobalMembers.gApp.mMainMenu.mIsFullGame() && !this.mBoard.mGameFinished)
			{
				this.mBoard.SaveGame();
			}
			if (this.mProfile != null)
			{
				this.mProfile.WriteProfile();
				this.mProfile.WriteProfileList();
			}
			this.WriteToRegistry();
			if (GlobalMembers.gApp.GetDialog(18) != null)
			{
				return;
			}
			if (this.mBoard != null && this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME)
			{
				if (this.mBoard.mHyperspace != null || this.mBoard.mWantLevelup)
				{
					if (this.mBoard != null && GlobalMembers.gApp.mMainMenu.mIsFullGame() && !this.mBoard.mGameFinished)
					{
						this.mBoard.SaveGame();
					}
					return;
				}
				if (GlobalMembers.gApp.mDialogMap.Count != 0)
				{
					return;
				}
				if (this.mBoard.mInReplay)
				{
					this.mBoard.BackToGame();
					this.mMenus[7].ButtonDepress(10001);
					return;
				}
				if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN && GlobalMembers.gApp.mMenus[19].mY != ConstantsWP.MENU_Y_POS_HIDDEN && GlobalMembers.gApp.mMenus[19].mY != 0)
				{
					return;
				}
				Bej3Widget bej3Widget = GlobalMembers.gApp.mMenus[7];
				this.mLosfocus = true;
				Bej3Widget bej3Widget2 = this.mMenus[8];
				if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME && this.mInterfaceState != InterfaceState.INTERFACE_STATE_PAUSEMENU && this.mDialogMap.Count == 0 && (!this.mMenus[8].mVisible || !this.mBoard.mWantLevelup))
				{
					this.mMenus[7].ButtonDepress(10001);
				}
			}
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

		public void OnExiting()
		{
			if (GlobalMembers.gApp.mMainMenu.mIsFullGame())
			{
				if (this.mBoard != null && !this.mBoard.mGameFinished)
				{
					this.mBoard.SaveGame();
				}
			}
			else if (this.mBoard != null)
			{
				this.mBoard.DeleteSavedGame();
			}
			if (this.mProfile != null)
			{
				this.mProfile.WriteProfile();
				this.mProfile.WriteProfileList();
			}
			this.WriteToRegistry();
		}

		public void OnHardwardBackButtonPressed()
		{
			GlobalMembers.IsBackButtonPressed = true;
		}

		public bool IsHardwareBackButtonPressed()
		{
			return GlobalMembers.IsBackButtonPressed;
		}

		public void OnHardwareBackButtonPressProcessed()
		{
			GlobalMembers.IsBackButtonPressed = false;
		}

		protected override void ReorderSystemButtonHandler(SystemButtons button, List<Widget> handlers)
		{
			Widget[] array = handlers.ToArray();
			Widget widget = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (widget == null)
				{
					if (array[i] is MainMenu)
					{
						widget = array[i];
					}
				}
				else
				{
					Widget widget2 = array[i];
					array[i] = widget;
					array[i - 1] = widget2;
				}
			}
			handlers.Clear();
			handlers.AddRange(array);
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

		public void PopAnimPlaySample(string theSampleName, int thePan, double theVolume, double theNumSteps)
		{
		}

		public PIEffect PopAnimLoadParticleEffect(string theEffectName)
		{
			return null;
		}

		public bool PopAnimObjectPredraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return false;
		}

		public bool PopAnimObjectPostdraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return false;
		}

		public ImagePredrawResult PopAnimImagePredraw(int theId, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Image theImage, Graphics g, int theDrawCount)
		{
			return ImagePredrawResult.ImagePredraw_DontAsk;
		}

		public void PopAnimStopped(int theId)
		{
		}

		public void PopAnimCommand(int theId, string theCommand, string theParam)
		{
		}

		public bool PopAnimCommand(int theId, PASpriteInst theSpriteInst, string theCommand, string theParam)
		{
			return false;
		}

		public override void TouchBegan(SexyAppBase.Touch theTouch)
		{
			base.TouchBegan(theTouch);
			BejeweledLivePlusApp.mIdleTicksForButton = 0;
		}

		public override void TouchEnded(SexyAppBase.Touch theTouch)
		{
			base.TouchEnded(theTouch);
			BejeweledLivePlusApp.mIdleTicksForButton = 0;
		}

		public override void TouchMoved(SexyAppBase.Touch theTouch)
		{
			base.TouchMoved(theTouch);
			BejeweledLivePlusApp.mIdleTicksForButton = 0;
		}

		public override void TouchesCanceled()
		{
			base.TouchesCanceled();
			BejeweledLivePlusApp.mIdleTicksForButton = 0;
		}

		private void SetUpInterfaceStateDefinitions()
		{
			for (int i = 0; i < 20; i++)
			{
				this.mMenus[i] = null;
			}
			for (int j = 0; j < 17; j++)
			{
				this.gInterfaceDefinitions[j] = new List<Menu_Type>();
			}
			this.gInterfaceDefinitions[0].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[0].Add(Menu_Type.MENU_MAINMENU);
			this.gInterfaceDefinitions[1].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[1].Add(Menu_Type.MENU_MAINMENU);
			this.gInterfaceDefinitions[1].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[1].Add(Menu_Type.MENU_MAINMENUOPTIONSMENU);
			this.gInterfaceDefinitions[3].Add(Menu_Type.MENU_GAMEBOARD);
			this.gInterfaceDefinitions[3].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[3].Add(Menu_Type.MENU_GAMEDETAILMENU);
			this.gInterfaceDefinitions[4].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[4].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[4].Add(Menu_Type.MENU_BADGEMENU);
			this.gInterfaceDefinitions[6].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[6].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[6].Add(Menu_Type.MENU_STATSMENU);
			this.gInterfaceDefinitions[7].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[7].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[7].Add(Menu_Type.MENU_HIGHSCORESMENU);
			this.gInterfaceDefinitions[8].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[8].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[8].Add(Menu_Type.MENU_EDITPROFILEMENU);
			this.gInterfaceDefinitions[9].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[9].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[9].Add(Menu_Type.MENU_CREDITSMENU);
			this.gInterfaceDefinitions[10].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[10].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[10].Add(Menu_Type.MENU_ABOUTMENU);
			this.gInterfaceDefinitions[11].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[11].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[11].Add(Menu_Type.MENU_LEGALMENU);
			this.gInterfaceDefinitions[14].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[14].Add(Menu_Type.MENU_GAMEBOARD);
			this.gInterfaceDefinitions[14].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[14].Add(Menu_Type.MENU_HELPMENU);
			this.gInterfaceDefinitions[2].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[2].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[2].Add(Menu_Type.MENU_OPTIONSMENU);
			this.gInterfaceDefinitions[15].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[15].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[15].Add(Menu_Type.MENU_HELPANDOPTIONSMENU);
			this.gInterfaceDefinitions[13].Add(Menu_Type.MENU_GAMEBOARD);
			this.gInterfaceDefinitions[13].Add(Menu_Type.MENU_ZENOPTIONSMENU);
			this.gInterfaceDefinitions[5].Add(Menu_Type.MENU_BACKGROUND);
			this.gInterfaceDefinitions[5].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[5].Add(Menu_Type.MENU_PROFILEMENU);
			this.gInterfaceDefinitions[12].Add(Menu_Type.MENU_GAMEBOARD);
			this.gInterfaceDefinitions[12].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[12].Add(Menu_Type.MENU_PAUSEMENU);
			this.gInterfaceDefinitions[16].Add(Menu_Type.MENU_GAMEBOARD);
			this.gInterfaceDefinitions[16].Add(Menu_Type.MENU_FADE_UNDERLAY);
			this.gInterfaceDefinitions[16].Add(Menu_Type.MENU_PAUSEMENU);
		}

		private void SetContentSpecificConstants()
		{
			ConstantsWP.EDITWIDGET_HEIGHT = GlobalMembersResourcesWP.IMAGE_DIALOG_TEXTBOX.GetCelHeight();
		}

		private void DoNewBlitzGame(int theMinutes)
		{
		}

		private void DoNewClassicGame()
		{
			GlobalMembers.KILL_WIDGET_NOW(this.mBoard);
			this.mBoard = new ClassicBoard();
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			this.mBoard.Init();
			if (!this.DoSavedGameCheck())
			{
				this.DoGameDetailMenu(GameMode.MODE_CLASSIC, GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME);
				return;
			}
			this.mNeedMusicStart = true;
		}

		private void DoNewSpeedGame(int theId)
		{
			this.mLastDataParserId = theId;
			this.mLastDataParser = this.mSpeedModeDataParser;
			GlobalMembers.KILL_WIDGET_NOW(this.mBoard);
			this.mBoard = new SpeedBoard();
			this.mBoard.Resize(new Rect(0, 0, this.mWidth, this.mHeight));
			this.mBoard.mParams = this.mSpeedModeDataParser.mQuestDataVector[theId].mParams;
			this.mBoard.Init();
			if (!this.DoSavedGameCheck())
			{
				this.DoGameDetailMenu(GameMode.MODE_LIGHTNING, GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME);
				return;
			}
			this.mNeedMusicStart = true;
		}

		private void DoNewZenGame()
		{
			this.mLastDataParserId = -1;
			this.mLastDataParser = null;
			GlobalMembers.KILL_WIDGET_NOW(this.mBoard);
			if (this.mBinauralManager == null)
			{
				this.mBinauralManager = new BinauralManager();
			}
			this.mBoard = new ZenBoard();
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			this.mBoard.Init();
			this.StartSetupGame(true);
		}

		private void DoNewEndlessGame(EEndlessMode theId)
		{
			this.DoNewConfigGame((int)theId, this.mSecretModeDataParser, true);
		}

		private void DoNewConfigGame(int theId, QuestDataParser theParams, bool isPerprtual)
		{
			this.mLastDataParserId = theId;
			this.mLastDataParser = theParams;
			GlobalMembers.KILL_WIDGET(this.mSecretMenu);
			GlobalMembers.KILL_WIDGET_NOW(this.mBoard);
			string text = theParams.mQuestDataVector[theId].mParams["Challenge"].ToUpper();
			if (text == "BUTTERFLIES" || text == "BUTTERFLY")
			{
				this.mBoard = new ButterflyBoard();
			}
			else if (text == "DIG")
			{
				this.mBoard = new DigBoard();
			}
			this.mBoard.mParams = theParams.mQuestDataVector[theId].mParams;
			this.mBoard.Resize(0, 0, this.mWidth, this.mHeight);
			QuestBoard questBoard = (QuestBoard)this.mBoard;
			questBoard.mQuestId = (isPerprtual ? 1000 : 0) + theId;
			questBoard.mIsPerpetual = isPerprtual;
			questBoard.mShowLevelPoints = !isPerprtual;
			this.mBoard.Init();
			if (!this.DoSavedGameCheck())
			{
				this.DoGameDetailMenu(this.mCurrentGameMode, GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME);
				return;
			}
			this.mNeedMusicStart = true;
		}

		private void DoNewQuest(int theId)
		{
		}

		public bool HasClearedTutorial(int theTutorialFlag)
		{
			return (this.mProfile.mTutorialFlags & (1UL << theTutorialFlag)) != 0UL || (this.mProfile.mTutorialFlags & 524288UL) != 0UL;
		}

		public void StartSetupGame(bool deleteSavedGame)
		{
			if (this.mCurrentGameMode == GameMode.MODE_ZEN)
			{
				deleteSavedGame = false;
			}
			if (deleteSavedGame)
			{
				this.mBoard.DeleteSavedGame();
			}
			int tutorialFlagsForMode = BejeweledLivePlusApp.GetTutorialFlagsForMode(this.mCurrentGameMode);
			if (!this.HasClearedTutorial(tutorialFlagsForMode))
			{
				if (tutorialFlagsForMode == 22)
				{
					this.mDiamondMineFirstLaunch = true;
				}
				this.DoHelpDialog(tutorialFlagsForMode, 0);
			}
			else
			{
				this.mShowBackground = false;
				this.mWidgetManager.AddWidget(this.mBoard);
				this.mWidgetManager.SetFocus(this.mBoard);
				this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_INGAME);
				this.mBoard.NewGame();
				if (this.mCurrentGameMode == GameMode.MODE_ZEN)
				{
					ZenBoard zenBoard = (ZenBoard)this.mBoard;
					zenBoard.LoadAffirmations();
					zenBoard.LoadAmbientSound();
					zenBoard.PlayZenNoise();
				}
				if (Bej3Widget.mCurrentSlidingMenu != null)
				{
					Bej3Widget.mCurrentSlidingMenu.mSlidingForDialog = null;
				}
				Bej3Widget.mCurrentSlidingMenu = this.mBoard;
				this.mGameInProgress = true;
			}
			if (this.mCurrentGameMode == GameMode.MODE_ZEN && !this.mProfile.HasClearedTutorial(23))
			{
				if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_HELPMENU)
				{
					Bej3Widget.mCurrentSlidingMenu = this.mMenus[8];
				}
				else
				{
					Bej3Widget.mCurrentSlidingMenu = this.mMenus[7];
				}
				ZenInfoDialog theDialog = new ZenInfoDialog();
				this.AddDialog(44, theDialog);
				GlobalMembers.gApp.mProfile.mTutorialFlags ^= 8388608UL;
				if (Bej3Widget.mCurrentSlidingMenu != null)
				{
					Bej3Widget.mCurrentSlidingMenu.Transition_SlideOut();
				}
			}
			this.ClearUpdateBacklog(false);
		}

		public void DoGameDetailMenu(GameMode mode, GameDetailMenu.GAMEDETAILMENU_STATE state)
		{
			if (state == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME)
			{
				this.StartSetupGame(true);
				return;
			}
			GameDetailMenu gameDetailMenu = (GameDetailMenu)this.mMenus[6];
			gameDetailMenu.SetMode(mode, state);
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_GAMEDETAILMENU);
			if (state == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME && this.mBoard != null)
			{
				gameDetailMenu.GetStatsFromBoard(this.mBoard);
			}
			if (state == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME && this.GetDialogCount() == 0)
			{
				this.mMenus[6].Transition_SlideThenFadeIn();
			}
		}

		public void DoHelpDialog(int tutorialFlags, int state)
		{
			HelpDialog helpDialog = (HelpDialog)this.mMenus[8];
			helpDialog.SetMode(tutorialFlags);
			helpDialog.SetHelpDialogState((HelpDialog.HELPDIALOG_STATE)state);
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_HELPMENU);
			if (state == 0)
			{
				this.mMenus[8].mY = ConstantsWP.MENU_Y_POS_HIDDEN;
				this.mMenus[8].Transition_SlideThenFadeIn();
				this.mMenus[8].AllowSlideIn(true, null);
			}
		}

		public void FindFiles(string theSearch, List<string> theFileVector)
		{
		}

		public void UpdateVoices()
		{
			if (this.mCurVoice != null)
			{
				double num = this.mCurVoice.GetVolume();
				if (this.mNextVoice != null && this.mInterruptCurVoice)
				{
					num = GlobalMembers.MAX(0.0, num - (double)GlobalMembers.M(0.05f));
					this.mCurVoice.SetVolume(num);
				}
				if (!this.mCurVoice.IsPlaying() || num == 0.0)
				{
					this.mCurVoice.Release();
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
				this.mCurVoice.Play(false, false);
			}
		}

		public void UpdateStatsCalls()
		{
		}

		public void KillStatsCall()
		{
		}

		public static void LoadContent(string theGroup, bool notifyMenus)
		{
		}

		public static void LoadContent(string theGroup)
		{
			BejeweledLivePlusApp.LoadContent(theGroup, true);
		}

		public static void UnloadContent(string theGroup, bool force)
		{
		}

		public static void UnloadContent(string theGroup)
		{
			BejeweledLivePlusApp.UnloadContent(theGroup, false);
		}

		public static void LoadContentInBackground(string theGroup)
		{
		}

		public static void WaitUntilGroupLoaded(string theGroup)
		{
		}

		public void ContentLoaded()
		{
		}

		public static void LoadContentQuestMenu(int category)
		{
		}

		public override void Dispose()
		{
			if (!this.mIsDisposed)
			{
				try
				{
					this.mMusic.Dispose();
					this.mMusic = null;
					this.mSoundPlayer.Dispose();
					this.mSoundPlayer = null;
					this.mProfile.Dispose();
					this.mProfile = null;
					if (this.mCurveValCache != null)
					{
						this.mCurveValCache = null;
					}
				}
				finally
				{
					this.mIsDisposed = true;
					GC.SuppressFinalize(this);
				}
			}
		}

		public void DisableOptionsButtons(bool disable)
		{
			this.mMenus[7].SetDisabledTopButton(disable);
			if (this.mCurrentGameMode == GameMode.MODE_ZEN && this.mBoard != null)
			{
				((ZenBoard)this.mBoard).mZenOptionsButton.mMouseVisible = !disable;
				((ZenBoard)this.mBoard).mZenOptionsButton.SetDisabled(disable);
			}
		}

		public void DoBadgeMenu(int state, List<int> deferredBadgeVector)
		{
			BadgeMenu badgeMenu = (BadgeMenu)this.mMenus[11];
			badgeMenu.SetMode((BadgeMenu.BADGEMENU_STATE)state, deferredBadgeVector);
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_BADGEMENU);
			if (state == 1)
			{
				badgeMenu.ShowBackButton(false);
			}
		}

		public Dialog DoXBLErrorDialog()
		{
			return this.DoDialog(57, true, GlobalMembers._ID("ERROR", 4900), GlobalMembers._ID("Unable to connect to Xbox LIVE at this time. Please check your connection.", 4901), GlobalMembers._ID("OK", 414), 3);
		}

		public void DoProfileMenu()
		{
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_PROFILEMENU);
		}

		public void DoStatsMenu()
		{
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_STATSMENU);
		}

		public void DoHighScoresMenu()
		{
			this.mMenus[3].mVisible = true;
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_HIGHSCORESMENU);
		}

		public void DoEditProfileMenu(bool isFirstTime)
		{
			((EditProfileDialog)this.mMenus[15]).SetupForFirstShow(isFirstTime);
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_EDITPROFILEMENU);
		}

		public void DoEditProfileMenu()
		{
			this.DoEditProfileMenu(false);
		}

		public void DoOptionsMenu()
		{
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_OPTIONSMENU);
		}

		public void DoHelpAndOptionsMenu()
		{
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_HELPANDOPTIONSMENU);
		}

		public void DoMainMenu(bool openMainMenuOptions)
		{
			if (this.mBoard != null)
			{
				this.mBoard.mGameClosing = true;
				this.mBoard.UnloadContent();
				GlobalMembers.KILL_WIDGET_NOW(this.mBoard);
				this.mBoard = null;
			}
			if (this.mQuestMenu != null)
			{
				this.mQuestMenu.SetVisible(true);
			}
			this.mGameInProgress = false;
			Bej3Widget.SetOverlayType(Bej3Widget.OVERLAY_TYPE.OVERLAY_NONE);
			this.mShowBackground = true;
			((MainMenuOptions)this.mMenus[5]).mExpandOnShow = openMainMenuOptions;
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_MAINMENU);
		}

		public void DoMainMenu()
		{
			this.DoMainMenu(false);
		}

		public void DoCreditsMenu()
		{
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_CREDITSMENU);
		}

		public void DoAboutMenu()
		{
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_ABOUTMENU);
		}

		public void DoZenOptionsMenu()
		{
			this.mBoard.Pause();
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_ZENOPTIONSMENU);
		}

		public void DoPauseMenu()
		{
			PauseMenu pauseMenu = (PauseMenu)this.mMenus[7];
			pauseMenu.SetMode(this.mCurrentGameMode);
			this.mBoard.Pause();
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_PAUSEMENU);
		}

		public void DoLegalMenu()
		{
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_LEGALMENU);
		}

		public void GoBackToGame()
		{
			this.mBoard.Unpause();
			this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_INGAME);
		}

		public string GetModeHeading(GameMode mode)
		{
			switch (mode)
			{
			case GameMode.MODE_CLASSIC:
				return GlobalMembers._ID("CLASSIC", 3208);
			case GameMode.MODE_LIGHTNING:
				return GlobalMembers._ID("LIGHTNING", 3211);
			case GameMode.MODE_DIAMOND_MINE:
				return GlobalMembers._ID("DIAMOND MINE", 3210);
			case GameMode.MODE_ZEN:
				return GlobalMembers._ID("ZEN", 3209);
			case GameMode.MODE_BUTTERFLY:
				return GlobalMembers._ID("BUTTERFLIES", 3212);
			case GameMode.MODE_ICESTORM:
				return GlobalMembers._ID("ICESTORM", 3214);
			case GameMode.MODE_POKER:
				return GlobalMembers._ID("POKER", 3213);
			default:
				return "";
			}
		}

		public string GetModeHint(GameMode mode)
		{
			switch (mode)
			{
			case GameMode.MODE_CLASSIC:
				return GlobalMembers._ID("Score as many points as possible until there are no more moves.", 3554);
			case GameMode.MODE_LIGHTNING:
				return GlobalMembers._ID("Score as many points as possible until there are no more moves.", 3557);
			case GameMode.MODE_DIAMOND_MINE:
				return GlobalMembers._ID("Score as many points as you can before the time runs out.", 3555);
			case GameMode.MODE_ZEN:
				return GlobalMembers._ID("Relax body and mind in this endless mode.", 3556);
			case GameMode.MODE_BUTTERFLY:
				return GlobalMembers._ID("Score as many points as you can before a Butterfly Gem reaches the top.", 3558);
			default:
				return string.Empty;
			}
		}

		public static int GetTutorialFlagsForMode(GameMode mode)
		{
			int result = 0;
			switch (mode)
			{
			case GameMode.MODE_CLASSIC:
				result = 21;
				break;
			case GameMode.MODE_LIGHTNING:
				result = 10;
				break;
			case GameMode.MODE_DIAMOND_MINE:
				result = 22;
				break;
			case GameMode.MODE_ZEN:
				result = 0;
				break;
			case GameMode.MODE_BUTTERFLY:
				result = 17;
				break;
			}
			return result;
		}

		public int ModeHeadingToGameMode(string theHeading)
		{
			for (int i = 0; i < 7; i++)
			{
				string modeHeading = this.GetModeHeading((GameMode)i);
				string highScoreTableId = this.GetHighScoreTableId(modeHeading);
				if (modeHeading == theHeading || highScoreTableId == theHeading)
				{
					return i;
				}
			}
			return 7;
		}

		public string GetHighScoreTableId(string theLocalisedTableName)
		{
			if (theLocalisedTableName == GlobalMembers._ID("CLASSIC", 3208))
			{
				return "CLASSIC";
			}
			if (theLocalisedTableName == GlobalMembers._ID("ZEN", 3209))
			{
				return "ZEN";
			}
			if (theLocalisedTableName == GlobalMembers._ID("DIAMOND MINE", 3210))
			{
				return "DIAMOND MINE";
			}
			if (theLocalisedTableName == GlobalMembers._ID("LIGHTNING", 3211))
			{
				return "LIGHTNING";
			}
			if (theLocalisedTableName == GlobalMembers._ID("BUTTERFLIES", 3212))
			{
				return "BUTTERFLIES";
			}
			return theLocalisedTableName;
		}

		public bool ChangeProfileName(string newName)
		{
			return false;
		}

		public void DisableAllExceptThis(Widget allExceptThis, bool disableOthers)
		{
			for (int i = 0; i < 20; i++)
			{
				if (this.mMenus[i] != null && this.mMenus[i] != allExceptThis && this.mMenus[i].GetWidgetState() == Bej3WidgetState.STATE_IN)
				{
					this.mMenus[i].SetDisabled(disableOthers);
				}
			}
		}

		public override void LoadingThreadCompleted()
		{
		}

		public void LoadConfigs()
		{
			if (this.mQuestDataParser != null)
			{
				this.mQuestDataParser.Dispose();
			}
			if (this.mDefaultQuestDataParser != null)
			{
				this.mDefaultQuestDataParser.Dispose();
			}
			if (this.mSecretModeDataParser != null)
			{
				this.mSecretModeDataParser.Dispose();
			}
			if (this.mSpeedModeDataParser != null)
			{
				this.mSpeedModeDataParser.Dispose();
			}
			this.mQuestDataParser = new QuestDataParser();
			if (!this.mQuestDataParser.LoadDescriptor(this.mResourceManager.GetLocaleFolder(true) + "properties\\quest.cfg") && !this.mQuestDataParser.LoadDescriptor("properties\\quest.cfg"))
			{
				this.Popup(this.mQuestDataParser.mError);
			}
			this.mDefaultQuestDataParser = new QuestDataParser();
			if (!this.mDefaultQuestDataParser.LoadDescriptor(this.mResourceManager.GetLocaleFolder(true) + "properties\\defaultquest.cfg") && !this.mDefaultQuestDataParser.LoadDescriptor("properties\\defaultquest.cfg"))
			{
				this.Popup(this.mDefaultQuestDataParser.mError);
			}
			this.mSecretModeDataParser = new QuestDataParser();
			if (!this.mSecretModeDataParser.LoadDescriptor(this.mResourceManager.GetLocaleFolder(true) + "properties\\secret.cfg") && !this.mSecretModeDataParser.LoadDescriptor("properties\\secret.cfg"))
			{
				this.Popup(this.mSecretModeDataParser.mError);
			}
			this.mSpeedModeDataParser = new QuestDataParser();
			if (!this.mSpeedModeDataParser.LoadDescriptor(this.mResourceManager.GetLocaleFolder(true) + "properties\\speed.cfg") && !this.mSpeedModeDataParser.LoadDescriptor("properties\\speed.cfg"))
			{
				this.Popup(this.mSpeedModeDataParser.mError);
			}
		}

		public void LoadHighscores()
		{
		}

		public bool LoadTempleMeshes()
		{
			return false;
		}

		public void SaveHighscores(bool theForceSave)
		{
		}

		public void SaveHighscores()
		{
			this.SaveHighscores(false);
		}

		public MemoryImage GetOrCreateAlphaImage(MemoryImage theSrcImage)
		{
			return new MemoryImage();
		}

		public void LogStatString(string theEventString)
		{
		}

		public void QueueStatsCall(string theStatsCall)
		{
		}

		public int GetBoostIdx(string theName)
		{
			return -1;
		}

		public override void ShutdownHook()
		{
		}

		public override void WriteToRegistry()
		{
			base.WriteToRegistry();
			this.RegistryWriteInteger("MusicVolume", (int)(this.mMusicVolume * 100.0));
			this.RegistryWriteInteger("SfxVolume", (int)(this.mSfxVolume * 100.0));
			this.RegistryWriteInteger("Muted", (this.mMuteCount - this.mAutoMuteCount > 0) ? 1 : 0);
			this.RegistryWriteInteger("ScreenMode", this.mIsWindowed ? 0 : 1);
			this.RegistryWriteInteger("PreferredX", this.mPreferredX);
			this.RegistryWriteInteger("PreferredY", this.mPreferredY);
			this.RegistryWriteInteger("PreferredWidth", this.mPreferredWidth);
			this.RegistryWriteInteger("PreferredHeight", this.mPreferredHeight);
			this.RegistryWriteInteger("CustomCursors", this.mCustomCursorsEnabled ? 1 : 0);
			this.RegistryWriteInteger("InProgress", 0);
			this.RegistryWriteBoolean("WaitForVSync", this.mWaitForVSync);
			Buffer buffer = new Buffer();
			if (GlobalMembers.gApp.mProfile != null)
			{
				ulong num = ((GlobalMembers.gApp.mProfile.mLastFacebookId.Length > 0) ? ulong.Parse(GlobalMembers.gApp.mProfile.mLastFacebookId) : 0UL);
				buffer.WriteLong((long)num);
				buffer.WriteLong((long)(num >> 32));
				buffer.WriteByte((byte)GlobalMembers.gApp.mProfile.mProfileList.Count);
				buffer.WriteShort((short)Math.Min(65535, this.mProfile.mStats[0] / 60));
				buffer.WriteShort((short)Math.Min(65535, GlobalMembers.gApp.mProfile.mOnlineGames));
				buffer.WriteShort((short)Math.Min(65535, GlobalMembers.gApp.mProfile.mOfflineGames));
				buffer.WriteLong(GlobalMembers.gApp.mProfile.mOfflineRankPoints / 1000L);
				this.RegistryWriteData("GameData", buffer.GetDataPtr(), (ulong)((long)buffer.GetDataLen()));
			}
			if (this.mProfile != null)
			{
				this.RegistryWriteString("LastUser", this.mProfile.mProfileName);
			}
			if (this.mResChanged)
			{
				this.RegistryWriteInteger("ArtRes", this.mArtRes);
			}
			this.RegistryWriteInteger("VoiceVolume", (int)(this.mVoiceVolume * 100.0));
			this.RegistryWriteInteger("ZenAmbientVolume", (int)(this.mZenAmbientVolume * 100.0));
			this.RegistryWriteInteger("ZenAmbientMusicVolume", (int)(this.mZenAmbientMusicVolume * 100.0));
			this.RegistryWriteInteger("ZenBinauralVolume", (int)(this.mZenBinauralVolume * 100.0));
			this.RegistryWriteInteger("ZenBreathVolume", (int)(this.mZenBreathVolume * 100.0));
			this.RegistryWriteBoolean("RegCodeNotNeeded", this.mRegCodeNotNeeded);
			this.RegistryWriteBoolean("AnimateBackground", this.mAnimateBackground);
			this.RegistryWriteString("ClientId", this.mClientId);
			this.RegistryWriteInteger("TipIdx", this.mTipIdx);
			base.RegistrySave();
		}

		public override void ReadFromRegistry()
		{
			base.ReadFromRegistry();
			int num = 0;
			if (this.RegistryReadInteger("VoiceVolume", ref num))
			{
				this.mVoiceVolume = (double)num / 100.0;
			}
			if (this.RegistryReadInteger("ZenAmbientVolume", ref num))
			{
				this.mZenAmbientVolume = (double)num / 100.0;
			}
			if (this.RegistryReadInteger("ZenAmbientMusicVolume", ref num))
			{
				this.mZenAmbientMusicVolume = (double)num / 100.0;
			}
			if (this.RegistryReadInteger("ZenBinauralVolume", ref num))
			{
				this.mZenBinauralVolume = (double)num / 100.0;
			}
			if (this.RegistryReadInteger("ZenBreathVolume", ref num))
			{
				this.mZenBreathVolume = (double)num / 100.0;
			}
			this.RegistryReadBoolean("RegCodeNotNeeded", ref this.mRegCodeNotNeeded);
			this.RegistryReadBoolean("AnimateBackground", ref this.mAnimateBackground);
			this.RegistryReadString("ClientId", ref this.mClientId);
			this.RegistryReadInteger("TipIdx", ref this.mTipIdx);
		}

		public override Dialog NewDialog(int theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			Bej3Dialog bej3Dialog = new Bej3Dialog(theDialogId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
			bej3Dialog.SetHeaderFont(GlobalMembersResources.FONT_HUGE);
			bej3Dialog.SetLinesFont(GlobalMembersResources.FONT_DIALOG);
			bej3Dialog.SetButtonFont(GlobalMembersResources.FONT_DIALOG);
			return bej3Dialog;
		}

		public override void ModalOpen()
		{
		}

		public void AskQuit()
		{
		}

		public void UnlockEndlessMode(EEndlessMode theMode)
		{
		}

		public void QueueQuit()
		{
		}

		public bool DoSavedGameCheck()
		{
			if (!GlobalMembers.gApp.mMainMenu.mIsFullGame())
			{
				this.mBoard.DeleteSavedGame();
			}
			if (!this.mBoard.HasSavedGame())
			{
				return false;
			}
			Bej3Dialog bej3Dialog = (Bej3Dialog)this.DoDialog(20, true, GlobalMembers._ID("Resume?", 75), GlobalMembers._ID("Resume a saved game or start a new one ? Your saved game will be lost if you start a new one.", 3184), "", 2, 3, 3);
			this.mIgnoreSound = true;
			bej3Dialog.mYesButton.mLabel = GlobalMembers._ID("RESUME GAME", 3185);
			((Bej3Button)bej3Dialog.mYesButton).SetType(Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
			bej3Dialog.mNoButton.mLabel = GlobalMembers._ID("NEW GAME", 3186);
			Bej3Button bej3Button = new Bej3Button(1002, bej3Dialog, Bej3ButtonType.BUTTON_TYPE_LONG);
			bej3Button.SetLabel(GlobalMembers._ID("CANCEL", 3187));
			bej3Button.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			bej3Dialog.AddButton(bej3Button);
			bej3Dialog.SetButtonPosition(bej3Dialog.mYesButton, 0);
			bej3Dialog.mWidth = ConstantsWP.RESUME_DIALOG_WIDTH;
			bej3Dialog.SizeToContent();
			if (bej3Dialog.mTopButton != null)
			{
				bej3Dialog.mTopButton.mId = 1002;
			}
			this.mWidgetManager.SetFocus(bej3Dialog);
			this.mIgnoreSound = false;
			return true;
		}

		public void DoPlayMenu()
		{
			this.DoMainMenu();
		}

		public void UserChanged()
		{
		}

		public void DoNewGame(GameMode mode)
		{
			this.mCurrentGameMode = mode;
			switch (mode)
			{
			case GameMode.MODE_CLASSIC:
				this.DoNewClassicGame();
				return;
			case GameMode.MODE_LIGHTNING:
				this.DoNewSpeedGame(0);
				return;
			case GameMode.MODE_DIAMOND_MINE:
				this.DoNewEndlessGame(EEndlessMode.ENDLESS_GOLDRUSH);
				return;
			case GameMode.MODE_ZEN:
				this.DoNewZenGame();
				return;
			case GameMode.MODE_BUTTERFLY:
				this.DoNewEndlessGame(EEndlessMode.ENDLESS_BUTTERFLY);
				return;
			default:
				return;
			}
		}

		public void ConfirmUserMusic()
		{
			string theDialogHeader = GlobalMembers._ID("MUSIC", 3601);
			string theDialogLines = GlobalMembers._ID("Do you want to interrupt your music and use game background music?", 7710);
			Bej3Dialog bej3Dialog = (Bej3Dialog)this.DoDialog(52, true, theDialogHeader, theDialogLines, string.Empty, 1);
			bej3Dialog.SetButtonPosition(bej3Dialog.mYesButton, 0);
			bej3Dialog.mWidth = ConstantsWP.RESUME_DIALOG_WIDTH;
			bej3Dialog.SizeToContent();
			this.mWidgetManager.SetFocus(bej3Dialog);
		}

		public void DoTrialDialog(int theId)
		{
			string theDialogLines = string.Empty;
			string theDialogHeader = GlobalMembers._ID("PROMPT", 3789);
			string mLabel = GlobalMembers._ID("BUY FULL GAME", 3795);
			switch (theId)
			{
			case 0:
			case 2:
			case 5:
				theDialogLines = GlobalMembers._ID("You can't proceed in with trial game ,would you like to buy the full game?", 3792);
				theDialogHeader = GlobalMembers._ID("End Of The Trial Version", 3796);
				break;
			case 1:
			case 3:
				theDialogLines = GlobalMembers._ID("You can't play this mode with trial game ,would you like to buy the full game?", 3793);
				break;
			case 7:
				theDialogLines = GlobalMembers._ID("The Leaderboards are only available in the full game.", 3801);
				mLabel = GlobalMembers._ID("UPDATE", 3802);
				break;
			case 8:
				theDialogLines = GlobalMembers._ID("You can't browse the achievements with trial game ,would you like to buy the full game?", 3794);
				break;
			}
			Bej3Dialog bej3Dialog = (Bej3Dialog)this.DoDialog(51, true, theDialogHeader, theDialogLines, "", 1);
			this.mIgnoreSound = true;
			bej3Dialog.mYesButton.mLabel = mLabel;
			bej3Dialog.mNoButton.mLabel = GlobalMembers._ID("CANCEL", 3239);
			((Bej3Button)bej3Dialog.mNoButton).SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			bej3Dialog.SetButtonPosition(bej3Dialog.mYesButton, 0);
			bej3Dialog.mWidth = ConstantsWP.RESUME_DIALOG_WIDTH;
			bej3Dialog.SizeToContent();
			Rect theRect = new Rect(bej3Dialog.mYesButton.mRect);
			theRect.mWidth += 37;
			bej3Dialog.mYesButton.Resize(theRect);
			theRect.mY = bej3Dialog.mNoButton.mY;
			bej3Dialog.mNoButton.Resize(theRect);
			this.mWidgetManager.SetFocus(bej3Dialog);
			this.mIgnoreSound = false;
		}

		public void GoToBlitz()
		{
		}

		public void DoSecretMenu()
		{
		}

		public void DoQuestMenu(bool killBoard)
		{
		}

		public void DoQuestMenu()
		{
			this.DoQuestMenu(true);
		}

		public void DoMenu(bool doMusic)
		{
		}

		public void DoMenu()
		{
			this.DoMenu(true);
		}

		public void DoWelcomeDialog()
		{
		}

		public void DoNewUserDialog()
		{
		}

		public void DoChangePictureDialog(bool allowCancel)
		{
		}

		public void DoUserSelectionDialog()
		{
		}

		public void DoRegisteredDialog()
		{
		}

		public void GoToInterfaceState(InterfaceState newState, bool onlyOrder)
		{
			this.mPreviousInterfaceState = this.mInterfaceState;
			this.mMenus[2] = this.mMainMenu;
			this.mMenus[1] = this.mBoard;
			this.mMenus[3] = this.mUnderDialogWidget;
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_MAINMENU)
			{
				this.mGameInProgress = false;
			}
			this.mInterfaceState = newState;
			int num = -1;
			bool flag = (this.mInterfaceState == InterfaceState.INTERFACE_STATE_GAMEDETAILMENU && ((GameDetailMenu)this.mMenus[6]).GetGameMenuState() == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME) || (this.mInterfaceState == InterfaceState.INTERFACE_STATE_BADGEMENU && ((BadgeMenu)this.mMenus[11]).GetState() == 0);
			for (int i = 0; i < 20; i++)
			{
				if (this.mMenus[i] != null)
				{
					this.mWidgetManager.BringToFront(this.mMenus[i]);
					if (!onlyOrder)
					{
						bool flag2 = false;
						if ((i != 0 || this.mShowBackground) && (i != 1 || !flag))
						{
							for (int j = 0; j < this.gInterfaceDefinitions[(int)newState].size<Menu_Type>(); j++)
							{
								if (this.gInterfaceDefinitions[(int)newState][j] == this.mMenus[i].mType)
								{
									num = i;
									flag2 = true;
									break;
								}
							}
						}
						this.mMenus[i].InterfaceStateChanged(newState);
						if (flag2)
						{
							this.mWidgetManager.SetFocus(this.mMenus[i]);
							this.mMenus[i].Show();
						}
						else
						{
							this.mMenus[i].AllowSlideIn(false, null);
							this.mMenus[i].LostFocus();
							this.mMenus[i].Hide();
						}
					}
				}
			}
			if (!onlyOrder && num >= 0 && this.mMenus[num] != null)
			{
				this.mWidgetManager.SetFocus(this.mMenus[num]);
			}
			this.ClearUpdateBacklog(false);
		}

		public void GoToInterfaceState(InterfaceState newState)
		{
			this.GoToInterfaceState(newState, false);
		}

		public bool TouchedToolTip(int x, int y)
		{
			return false;
		}

		public override void HandleCmdLineParam(string theParamName, string theParamValue)
		{
		}

		public override void PreDDInterfaceInitHook()
		{
		}

		public void PrepareForSoftwareRendering()
		{
		}

		public override void Set3DAcclerated(bool is3D, bool reinit)
		{
		}

		public override void Done3dTesting()
		{
		}

		public bool LoadingThreadLoadGroup(string group)
		{
			return false;
		}

		public bool IsLoadingCompleted()
		{
			return BejeweledLivePlusApp.mIsLoadingCompleted;
		}

		public void DoLoadingThreadCompleted()
		{
			this.mNumLoadingThreadTasks = this.mResourceManager.GetNumResources("Common", true, true) + this.mResourceManager.GetNumResources("Fonts", true, true) + this.mResourceManager.GetNumResources("MainMenu", true, true) + this.mResourceManager.GetNumResources("GamePlay", true, true);
			this.LoadingThreadLoadGroup("Common");
			this.LoadingThreadLoadGroup("MainMenu");
			this.LoadingThreadLoadGroup("GamePlay");
			foreach (BejeweledLivePlusApp.ExtractResourceFunc extractResourceFunc in this.resExtract_.Values)
			{
				if (extractResourceFunc != null)
				{
					extractResourceFunc(this.mResourceManager);
				}
			}
			this.resExtract_.Clear();
			Resources.LinkUpFonts(this.mArtRes);
			this.SetContentSpecificConstants();
			this.mMenus[13] = new StatsMenu();
			this.mMenus[2] = this.mMainMenu;
			this.mMenus[11] = new BadgeMenu(true);
			this.mMenus[5] = new MainMenuOptions();
			this.mMenus[7] = new PauseMenu();
			this.mMenus[14] = new HighScoresMenu();
			this.mMenus[12] = new ProfileMenu();
			this.mMenus[9] = new OptionsMenu();
			this.mMenus[18] = new LegalMenu();
			this.mMenus[17] = new AboutMenu();
			this.mMenus[8] = new HelpDialog();
			this.mMenus[6] = new GameDetailMenu();
			this.mMenus[16] = new CreditsMenu();
			this.mMenus[15] = new EditProfileDialog();
			this.mMenus[19] = new ZenOptionsMenu();
			this.mMenus[10] = new HelpAndOptionsMenu();
			for (int i = 0; i < 20; i++)
			{
				if (this.mMenus[i] != null)
				{
					this.mWidgetManager.AddWidget(this.mMenus[i]);
				}
			}
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_DIALOG, 0, new Color(0, 0, 0, 0));
			BejeweledLivePlusApp.mIsLoadingCompleted = true;
		}

		public Bej3Dialog DoRenameUserDialog()
		{
			NewUserDialog newUserDialog = new NewUserDialog(true, true);
			newUserDialog.mOrigString = this.mProfile.mProfileName;
			newUserDialog.mNameWidget.SetText(((EditProfileDialog)GlobalMembers.gApp.mMenus[15]).mDisplayName);
			this.AddDialog(newUserDialog);
			return newUserDialog;
		}

		public override void SwitchScreenMode(bool wantWindowed, bool is3d, bool force)
		{
		}

		public void SwitchScreenMode(bool wantWindowed, bool is3d)
		{
			this.SwitchScreenMode(wantWindowed, is3d, false);
		}

		public void ResizeWindowedButton()
		{
		}

		public Bej3Dialog OpenURLWithWarning(string theURL)
		{
			this.mLinkWarningLocation = theURL;
			Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.DoDialog(53, true, GlobalMembers._ID("VISIT WEBSITE?", 3196), GlobalMembers._ID("Minimize Bejeweled Live + and go to external website?", 3197), "", 1, 3, 4);
			((Bej3Button)bej3Dialog.mYesButton).SetLabel(GlobalMembers._ID("VISIT WEBSITE", 3198));
			((Bej3Button)bej3Dialog.mNoButton).SetLabel(GlobalMembers._ID("CANCEL", 3199));
			bej3Dialog.SetButtonPosition(bej3Dialog.mYesButton, 0);
			return bej3Dialog;
		}

		public new bool OpenURL(string theURL, bool shutdownOnOpen)
		{
			this.mWantDataUpdateOnFocus = true;
			return base.OpenURL(theURL, shutdownOnOpen);
		}

		public virtual bool OpenURL(string theURL)
		{
			return this.OpenURL(theURL, false);
		}

		public void OpenLastConfirmedWebsite()
		{
			this.OpenURL(this.mLinkWarningLocation);
		}

		public override void GotFocus()
		{
		}

		public override void LostFocus()
		{
			bool flag = this.mBoard != null && this.mBoard.mGameOverCount == 0 && this.mGameInProgress;
			if (flag)
			{
				this.mBoard.SyncUnAwardedBadges(this.mProfile.mDeferredBadgeVector);
			}
			this.mProfile.WriteProfile();
			this.WriteToRegistry();
			if (flag)
			{
				this.mBoard.SaveGame();
			}
			base.LostFocus();
			if (this.mSoundManager != null)
			{
				this.mSoundManager.SetVolume(2, 0.0);
				this.mSoundManager.SetVolume(3, 0.0);
				this.mSoundManager.SetVolume(4, 0.0);
			}
			WP7AppDriver wp7AppDriver = this.mAppDriver as WP7AppDriver;
			if (wp7AppDriver != null && this.mGameInProgress && this.mBoard.WantsHideOnPause())
			{
				this.mBoard.mSuspendingGame = true;
			}
			if (this.mGameInProgress && this.mBoard.mSuspendingGame && this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME)
			{
				this.mBoard.Pause();
			}
			this.mMusicInterface.PauseAllMusic();
		}

		public virtual void AppEnteredBackground()
		{
		}

		public int GetCoinCount()
		{
			return -1;
		}

		public void AddToCoinCount(int theDelta)
		{
		}

		public int GetBoostCost(int theBoostId)
		{
			return -1;
		}

		public int GetRank()
		{
			return -1;
		}

		public long GetRankPoints()
		{
			return -1L;
		}

		public bool IsVoicePending(int soundId)
		{
			bool result = false;
			foreach (VoicePlayArgs voicePlayArgs in this.mPendingVoice)
			{
				if (voicePlayArgs.SoundID == soundId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public void PlayVoice(VoicePlayArgs args)
		{
			this.mPendingVoice.Add(args);
		}

		public void PlayVoice(int theSoundId, int thePan, double theVolume, int theInterruptId)
		{
			if (this.mIgnoreSound)
			{
				return;
			}
			if (this.mNextVoice != null)
			{
				this.mNextVoice.Release();
			}
			if (this.mMuteCount > 0)
			{
				return;
			}
			if (this.mVoiceVolume <= 0.0)
			{
				return;
			}
			this.mNextVoice = this.mSoundManager.GetSoundInstance(theSoundId);
			if (this.mNextVoice != null)
			{
				this.mNextVoice.SetMasterVolumeIdx(1);
				this.mNextVoice.SetVolume(theVolume * this.mVoiceVolume);
				this.mNextVoice.SetPan(thePan);
				this.mInterruptCurVoice = this.mCurVoiceId == theInterruptId || (this.mNextVoiceId == theInterruptId && this.mInterruptCurVoice) || theInterruptId == -2;
				this.mNextVoiceId = theSoundId;
			}
		}

		public void PlayVoice(int theSoundId, int thePan, double theVolume)
		{
			this.PlayVoice(theSoundId, thePan, theVolume, -1);
		}

		public void PlayVoice(int theSoundId, int thePan)
		{
			this.PlayVoice(theSoundId, thePan, 1.0, -1);
		}

		public void PlayVoice(int theSoundId)
		{
			this.PlayVoice(theSoundId, 0, 1.0, -1);
		}

		public void BuyGame()
		{
			Guide.ShowMarketplace(0);
		}

		public void PlaySample(int theSoundId, int thePan, double theVolume, double theNumSteps)
		{
			if (this.mIgnoreSound)
			{
				return;
			}
			if (this.mSoundManager == null)
			{
				return;
			}
			if (theVolume <= 0.0)
			{
				return;
			}
			SoundInstance soundInstance = this.mSoundManager.GetSoundInstance(theSoundId);
			if (soundInstance != null && !soundInstance.IsPlaying())
			{
				soundInstance.SetVolume((this.mMuteCount > 0) ? 0.0 : (theVolume * this.mSfxVolume));
				soundInstance.SetPan(thePan);
				soundInstance.AdjustPitch(theNumSteps);
				soundInstance.Play(false, true);
			}
		}

		public void PlaySample(int theSoundId, int thePan, double theVolume)
		{
			this.PlaySample(theSoundId, thePan, theVolume, 0.0);
		}

		public override void PlaySample(int theSoundId, int thePan)
		{
			this.PlaySample(theSoundId, thePan, 1.0, 0.0);
		}

		public override void PlaySample(int theSoundId)
		{
			this.PlaySample(theSoundId, 0, 1.0, 0.0);
		}

		public override bool DebugKeyDown(int theKey)
		{
			return false;
		}

		public override bool IsUIOrientationAllowed(UI_ORIENTATION theOrientation)
		{
			return false;
		}

		public override void UpdateFrames()
		{
			this.mMusic.Update();
			this.mSoundPlayer.Update();
			this.UpdateVoices();
			base.UpdateFrames();
			if (this.mDoFadeBackForDialogs || (this.mDialogMap.Count > 0 && base.GetDialog(40) == null && base.GetDialog(18) == null && (base.GetDialog(1) == null || base.GetDialog(1).mButtonMode != 0)))
			{
				if (this.mDialogObscurePct < 1f && this.mDoFadeBackForDialogs)
				{
					this.mDialogObscurePct = Math.Min(1f, this.mDialogObscurePct + GlobalMembers.M(0.05f));
					if (!this.mUnderDialogWidget.mVisible)
					{
						this.mUnderDialogWidget.SetVisible(true);
						this.mUnderDialogWidget.mUpdateCnt = GlobalMembers.M(100);
					}
					else
					{
						this.mUnderDialogWidget.MarkDirty();
					}
				}
				else if (!this.mDoFadeBackForDialogs)
				{
					this.mDialogObscurePct = Math.Max(0f, this.mDialogObscurePct - GlobalMembers.M(0.04f));
				}
			}
			else
			{
				this.mDialogObscurePct = Math.Max(0f, this.mDialogObscurePct - GlobalMembers.M(0.04f));
				if (this.mDialogObscurePct == 0f && this.mUnderDialogWidget.mVisible)
				{
					this.mUnderDialogWidget.SetVisible(false);
				}
			}
			if ((this.mInterfaceState == InterfaceState.INTERFACE_STATE_PAUSEMENU || this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME) && this.mMenus[7].mTargetPos == ConstantsWP.MENU_Y_POS_HIDDEN && this.mMenus[7].mTopButton.mType == Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS && !this.mBoard.mReplayWasTutorial)
			{
				this.mMenus[7].ButtonDepress(10001);
			}
			Bej3Button.UpdateStatics();
		}

		public override void DialogButtonDepress(int theDialogId, int theButtonId)
		{
			Bej3Dialog bej3Dialog = (Bej3Dialog)base.GetDialog(theDialogId);
			if (theDialogId == 33)
			{
				switch (theButtonId)
				{
				case 1000:
					GlobalMembers.gApp.WantExit = true;
					bej3Dialog.Kill();
					return;
				case 1001:
					bej3Dialog.Kill();
					return;
				default:
					bej3Dialog.mResult = int.MaxValue;
					break;
				}
			}
			if (theDialogId == 57)
			{
				InterfaceState interfaceState = this.mInterfaceState;
				if (interfaceState != InterfaceState.INTERFACE_STATE_GAMEDETAILMENU)
				{
					if (interfaceState == InterfaceState.INTERFACE_STATE_HIGHSCORESMENU)
					{
						this.mMenus[14].ButtonDepress(10001);
						this.DoMainMenu();
					}
				}
				else
				{
					GameDetailMenu gameDetailMenu = this.mMenus[6] as GameDetailMenu;
					gameDetailMenu.mFinalY = 50;
					this.mMenus[6].ButtonDepress(2);
				}
			}
			if (theDialogId == 51)
			{
				if (theButtonId == 1000)
				{
					this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_MAINMENU);
					this.DoMainMenu();
					((MainMenu)this.mMenus[2]).WantBuyGame();
				}
				if (this.mBoard != null)
				{
					this.mBoard.DeleteSavedGame();
					this.GoToInterfaceState(InterfaceState.INTERFACE_STATE_MAINMENU);
					this.DoMainMenu();
				}
			}
			if (theDialogId == 33 && theButtonId == 1000)
			{
				GlobalMembers.gApp.QueueQuit();
			}
			if (theDialogId == 20)
			{
				if (theButtonId == 1002)
				{
					GlobalMembers.KILL_WIDGET_NOW(this.mBoard);
				}
				else if (theButtonId == 1001)
				{
					this.DoGameDetailMenu(this.mCurrentGameMode, GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME);
				}
				else
				{
					if (this.mLatestClickedBall != null)
					{
						this.mLatestClickedBall.StartInGameTransition(false);
					}
					this.mBoard.LoadContent(false);
					this.StartSetupGame(false);
				}
			}
			else if (theDialogId == 53)
			{
				if (theButtonId == 1000)
				{
					new WebBrowserTask
					{
						Uri = new Uri(this.mLinkWarningLocation)
					}.Show();
				}
			}
			else if (theDialogId == 52)
			{
				if (theButtonId == 1000)
				{
					this.mMusicInterface.stopUserMusic();
				}
				GlobalMembers.gApp.mMusic.PlaySongNoDelay(1, true);
			}
			bej3Dialog.Kill();
		}

		public override void ButtonPress(int theId)
		{
			base.ButtonPress(theId);
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			if (theId == 1001)
			{
				GlobalMembers.gApp.SwitchScreenMode(true, this.Is3DAccelerated());
			}
		}

		public void DrawWaiter(Graphics g, int theX, int theY, int theUpdateCnt, int theAlpha)
		{
		}

		public virtual bool ShouldReInit()
		{
			return this.mReInit;
		}

		public virtual bool FrameNeedsSwapScreenImage()
		{
			return false;
		}

		public virtual Dialog DoDialog(int theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode, int buttonType1, int buttonType2)
		{
			Bej3Dialog bej3Dialog = (Bej3Dialog)base.DoDialog(theDialogId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode);
			Bej3Button bej3Button = (Bej3Button)bej3Dialog.mYesButton;
			if (bej3Button != null)
			{
				bej3Button.SetType((Bej3ButtonType)buttonType1);
			}
			bej3Button = (Bej3Button)bej3Dialog.mNoButton;
			if (bej3Button != null)
			{
				bej3Button.SetType((Bej3ButtonType)buttonType2);
			}
			bej3Dialog.Resize(0, bej3Dialog.mY, this.mWidth, bej3Dialog.mHeight);
			return bej3Dialog;
		}

		public override Dialog DoDialog(int theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, int theButtonMode)
		{
			return this.DoDialog(theDialogId, isModal, theDialogHeader, theDialogLines, theDialogFooter, theButtonMode, 3, 3);
		}

		public void EnableMusic(bool enable)
		{
		}

		public bool IsMusicEnabled()
		{
			return this.mMuteCount > 0;
		}

		public override void SetMusicVolume(double theVolume)
		{
			base.SetMusicVolume(theVolume);
		}

		public int GetSysFps()
		{
			return -1;
		}

		public int GetFrameStep()
		{
			return 1;
		}

		public long GetFrameTimer()
		{
			return -1L;
		}

		public override void Mute(bool autoMute)
		{
			base.Mute(autoMute);
			if (this.mBoard != null)
			{
				ZenBoard zenBoard = null;
				if (this.mBoard is ZenBoard)
				{
					zenBoard = this.mBoard as ZenBoard;
				}
				if (zenBoard != null)
				{
					zenBoard.MuteZenSounds();
				}
			}
		}

		public void Mute()
		{
			this.Mute(false);
		}

		public override void Unmute(bool autoMute)
		{
			base.Unmute(autoMute);
			if (this.mBoard != null)
			{
				ZenBoard zenBoard = null;
				if (this.mBoard is ZenBoard)
				{
					zenBoard = this.mBoard as ZenBoard;
				}
				if (zenBoard != null)
				{
					zenBoard.UnmuteZenSounds();
				}
			}
		}

		public void Unmute()
		{
			this.Unmute(false);
		}

		public bool IsKeyboardShowing()
		{
			return false;
		}

		public void DoRateGameDialog()
		{
		}

		public void DoGiftGameDialog()
		{
		}

		public void RateGame()
		{
		}

		public virtual void DrawSpecial(Graphics g)
		{
		}

		public void DoMoreGamesDialog()
		{
		}

		internal static List<string> mLoadedResourceGroups = new List<string>();

		public bool mGameCenterIsAvailable;

		private bool mWantExit;

		private int mTouchOffsetX;

		private int mTouchOffsetY;

		public PreCalculatedCurvedValManager mCurveValCache;

		private List<VoicePlayArgs> mPendingVoice = new List<VoicePlayArgs>();

		private bool mDisplayTitleUpdate;

		public string[] initialLoadGroups;

		private Dictionary<string, BejeweledLivePlusApp.InitStepFunc> initSteps_;

		private Dictionary<string, BejeweledLivePlusApp.ExtractResourceFunc> resExtract_;

		public bool mLosfocus;

		public BejeweledLivePlusApp.EAutoPlayState mAutoPlay;

		public string mVersion;

		public bool mGameInProgress;

		public InterfaceState mInterfaceState;

		public InterfaceState mPreviousInterfaceState;

		public GameMode mCurrentGameMode;

		public bool mShowBackground;

		public Bej3Widget[] mMenus;

		public MainMenu mMainMenu;

		public int mAutoSerializeInterval;

		public int mAutoSerializeIntervalAmnesty;

		public int mAutoSerializeMode;

		public int mAutoLevelUpCount;

		public bool mAutoSerializeModeRandomized;

		public bool mDiamondMineFirstLaunch;

		public double mVoiceVolume;

		public double mZenAmbientVolume;

		public double mZenAmbientMusicVolume;

		public double mZenBinauralVolume;

		public double mZenBreathVolume;

		public bool mIgnoreSound;

		public string mWebRoot;

		public bool mForcedWebRoot;

		public bool mReInit;

		public bool mWas3D;

		public bool mResChanged;

		public bool mResForced;

		public bool mJumpToZen;

		public bool mJumpToClassic;

		public bool mJumpToSpeed;

		public bool mAnimateBackground;

		public int mJumpToQuest;

		public int mArtRes;

		public int mPreInitArtRes;

		public int mPauseFrames;

		public bool mFocusedAfterLoad;

		public string mTestBkg;

		public string mForceBkg;

		public Music mMusic;

		public SoundEffects mSoundPlayer;

		public Buffer mStatsDumpBuffer;

		public int[] mBoostCosts;

		public string mLinkWarningLocation;

		public bool mDoFadeBackForDialogs;

		public bool mRegCodeNotNeeded;

		public bool mWantDataUpdateOnFocus;

		public int mWaitingForRegUpdateTicks;

		public string mBuyCoinsURL;

		public string mClientId;

		public BinauralManager mBinauralManager;

		public Dictionary<int, PIEffect>[] mQuestObjPIEffects;

		public Image[,] mShrunkenGems;

		public Dictionary<MemoryImage, MemoryImage> mAlphaImages;

		public Profile mProfile;

		public string mSwitchProfileName;

		public int mQuitCountdown;

		public HighScoreMgr mHighScoreMgr;

		public int mMaxGamesPerDay;

		public QuestDataParser mLastDataParser;

		public int mLastDataParserId;

		public QuestDataParser mDefaultQuestDataParser;

		public QuestDataParser mQuestDataParser;

		public QuestDataParser mSecretModeDataParser;

		public QuestDataParser mSpeedModeDataParser;

		public QuestMenu mQuestMenu;

		public SecretMenu mSecretMenu;

		public Background mBlitzBackground;

		public Background mBlitzBackgroundLo;

		public Board mBoard;

		public TooltipManager mTooltipManager;

		public SharedRenderTarget mRestartRT;

		public SoundInstance mCurVoice;

		public int mCurVoiceId;

		public SoundInstance mNextVoice;

		public int mNextVoiceId;

		public bool mInterruptCurVoice;

		public string mLastUser;

		public bool mCreatingFBUser;

		public bool mNeedMusicStart;

		public Bej3P3DListener[] mGems3DListener;

		public Mesh[] mGems3D;

		public Mesh mWarpTube3D;

		public Mesh mWarpTubeCap3D;

		public List<string> mAffirmationFiles;

		public List<string> mSBAFiles;

		public List<string> mAmbientFiles;

		public List<string> mTips;

		public List<string> mRankNames;

		public List<List<int>> mBadgeCutoffs;

		public List<List<int>> mBadgeSecondaryCutoffs;

		public UnderDialogWidget mUnderDialogWidget;

		public float mDialogObscurePct;

		public int mTipIdx;

		public string mQueuedStatsCalls;

		public bool mStatsStalled;

		public static bool mAllowRating;

		public static int mIdleTicksForButton;

		public CrystalBall mLatestClickedBall;

		private bool mIsDisposed;

		private List<Menu_Type>[] gInterfaceDefinitions;

		private static bool mIsLoadingCompleted = false;

		private int SfxUpdateCount;

		private int LastSfxId;

		public enum EAutoPlayState
		{
			AUTOPLAY_OFF,
			AUTOPLAY_RANDOM,
			AUTOPLAY_NO_DELAY,
			AUTOPLAY_NO_DELAY_WITH_INV,
			AUTOPLAY_TEST_HYPER,
			AUTOPLAY__COUNT
		}

		public delegate void InitStepFunc();

		public delegate bool ExtractResourceFunc(ResourceManager manager);
	}
}
