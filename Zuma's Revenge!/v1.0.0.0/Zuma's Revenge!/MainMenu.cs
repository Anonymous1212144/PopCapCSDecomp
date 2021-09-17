using System;
using System.Collections.Generic;
using System.Text;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class MainMenu : Widget, ButtonListener, DialogListener, PopAnimListener
	{
		public void ButtonPress(int theId)
		{
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			if (this.ShowingTikiTemple() || this.mApp.mGenericHelp != null || this.mApp.mMapScreen != null || this.mApp.mCredits != null)
			{
				return;
			}
			if (theId != 8 && this.mMainMenuOverlayWidget != null)
			{
				this.mMainMenuOverlayWidget.ButtonPress(theId);
			}
			GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
		}

		public void ButtonDepress(int theId)
		{
			if (this.mFirstTimeAlpha > 0 || this.mIFUnlockAnim != null || this.mApp.mGenericHelp != null || this.mDelayedIFStartState > 0 || this.ShowingTikiTemple() || this.mApp.mMapScreen != null)
			{
				return;
			}
			if (this.mState == MainMenu_State.State_Scroll)
			{
				return;
			}
			if (this.mApp.mBambooTransition != null && this.mApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			ChallengeMenu challengeMenu = this.mChallengeMenu;
			if (this.mApp.GetDialog(2) != null)
			{
				return;
			}
			this.mTip = null;
			this.mApp.mClickedHardMode = false;
			if (theId == 17)
			{
				return;
			}
			if (theId == 11)
			{
				return;
			}
			if (theId == 6)
			{
				return;
			}
			if (theId == 7)
			{
				this.mSkipEnterSound = true;
				if (this.mApp.DoYesNoDialog(TextManager.getInstance().getString(448), TextManager.getInstance().getString(453), true) == 1000)
				{
					if (!this.mApp.IsRegistered() && this.mApp.mTrialType == 1 && this.mApp.GetBoolean("UpsellExit", false))
					{
						this.mApp.DoUpsell(true);
						return;
					}
					this.mApp.Shutdown();
					return;
				}
			}
			else
			{
				if (theId == 8)
				{
					return;
				}
				if (theId == 1)
				{
					if (!this.mApp.ChallengeModeUnlocked())
					{
						this.mState = MainMenu_State.State_UnlockPrompt;
						this.mApp.DoGenericDialog(TextManager.getInstance().getString(837), TextManager.getInstance().getString(838), true, new GameApp.PreBlockCallback(this.ChangeMainMenuState), ZumasRevenge.Common._DS(100));
						this.mSkipEnterSound = true;
						return;
					}
					this.mApp.mUserProfile.mDoChallengeAceCupComplete = (this.mApp.mUserProfile.mDoChallengeCupComplete = false);
					this.mApp.mUserProfile.mDoChallengeAceTrophyZoom = (this.mApp.mUserProfile.mDoChallengeTrophyZoom = false);
					this.mApp.mUserProfile.mNewChallengeCupUnlocked = false;
					this.ShowChallengeMenu();
					return;
				}
				else
				{
					if (theId == 16)
					{
						this.DoMainMenu(true);
						return;
					}
					if (theId == 15)
					{
						ButtonWidget buttonWidget = null;
						for (int i = 0; i < this.mButtons.Count; i++)
						{
							if (this.mButtons[i].mId == theId)
							{
								buttonWidget = this.mButtons[i];
								break;
							}
						}
						buttonWidget.SetVisible(false);
						this.MarkDirty();
						this.mDelayedIFStartState = 1;
						return;
					}
					if (theId == 12)
					{
						if (this.mApp.mAutoMonkey != null)
						{
							this.mApp.mAutoMonkey.mEnableAutoMonkey = !this.mApp.mAutoMonkey.mEnableAutoMonkey;
							return;
						}
					}
					else
					{
						if (theId == 13)
						{
							return;
						}
						if (this.mMainMenuButtonsWidget != null)
						{
							this.mMainMenuButtonsWidget.ButtonDepress(theId);
						}
						if (this.mMainMenuOverlayWidget != null)
						{
							this.mMainMenuOverlayWidget.ButtonDepress(theId);
						}
					}
				}
			}
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
			if (this.mApp.mCredits != null || this.mApp.mGenericHelp != null)
			{
				return;
			}
			this.mSkipEnterSound = false;
		}

		public void ButtonMouseLeave(int theId)
		{
			if (this.mApp.mCredits != null)
			{
				return;
			}
			this.MarkDirty();
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public void ChangeMainMenuState()
		{
			this.mState = MainMenu_State.State_MainMenu;
		}

		public void DialogButtonPress(int theDialogId, int theButtonId)
		{
		}

		public void DialogButtonDepress(int theDialogId, int theButtonId)
		{
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
			return true;
		}

		public bool PopAnimObjectPostdraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			return true;
		}

		public ImagePredrawResult PopAnimImagePredraw(int theId, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Image theImage, Graphics g, int theDrawCount)
		{
			return ImagePredrawResult.ImagePredraw_Normal;
		}

		public void PopAnimStopped(int theId)
		{
		}

		public void PopAnimCommand(int theId, string theCommand, string theParam)
		{
		}

		public bool PopAnimCommand(int theId, PASpriteInst theSpriteInst, string theCommand, string theParam)
		{
			this.PopAnimCommand(theId, theCommand, theParam);
			return true;
		}

		public MainMenu(GameApp app)
		{
			this.mState = MainMenu_State.State_MainMenu;
			this.mUserSelDlg = null;
			this.mApp = app;
			this.mDistance = 0f;
			this.mAddAcc = 0f;
			this.mIFSparkle = null;
			this.mIFUnlockAnim = null;
			this.mHeroicSparkle = null;
			this.mTip = null;
			this.mFirstTimeAlpha = 0;
			this.mDelayedIFStartState = 0;
			this.mIncLavaAlpha = true;
			this.mUpsellBtn = null;
			this.mMainMenuButtonsWidget = null;
			this.mMenuScrollOriginY = -1;
			this.mMenuScrollDestY = -1;
			this.mMenuScrollStartY = -1;
			this.mMenuTikiStartX = -1;
			this.mMenuTikiDestX = -1;
			this.mMenuTikiOriginX = -1;
			this.mMenuTikiX = -1;
			this.mMenuFrogX = -1;
			this.mMenuFrogOriginX = -1;
			this.mMenuFrogStartX = -1;
			this.mMenuFrogDestX = -1;
			this.mMenuTikiDudeX = -1;
			this.mMenuTikiDudeDestX = -1;
			this.mMenuTikiDudeStartX = -1;
			this.mMenuTikiDudeOriginX = -1;
			this.mChallengeSparkle = null;
			this.mChallengeMenu = null;
			this.mTikiTemple = null;
			this.mMonkeyButton = null;
			this.mLogButton = null;
			this.mChangeProfileBtn = null;
			this.mClip = false;
			this.mMainMenuOverlayWidget = null;
			this.mTikiTeethSparkle = null;
			this.mVolcanoSmoke = null;
			this.mVolcanoProjectiles = null;
			this.mEffectBatch = new PIEffectBatch();
			this.mMoreGamesButton = null;
			this.mOptionsButton = null;
			this.mUnlockButton = null;
			this.mMenuScrollPct.SetConstant(0.0);
			this.mMenuScrollPct.mAppUpdateCountSrc = this.mUpdateCnt;
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			if (this.mTikiTemple != null)
			{
				this.mWidgetManager.RemoveWidget(this.mTikiTemple);
			}
			if (this.mChallengeMenu != null)
			{
				this.mWidgetManager.RemoveWidget(this.mChallengeMenu);
			}
			this.mVolcanoSmoke.Dispose();
			this.mTikiTeethSparkle.Dispose();
		}

		public void DoIronFrog(bool scroll)
		{
			if (!scroll)
			{
				this.mState = MainMenu_State.State_IF;
				List<ButtonWidget>.Enumerator enumerator = this.mButtons.GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator.Current.Move(enumerator.Current.mX + ZumasRevenge.Common._S(960), enumerator.Current.mY);
				}
			}
			else
			{
				this.mState = MainMenu_State.State_Scroll;
				this.mDistance = 960f;
			}
			this.mAddAcc = 0f;
		}

		public void DoMainMenu(bool scroll)
		{
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(true);
			}
			if (!scroll)
			{
				this.mState = MainMenu_State.State_MainMenu;
			}
			else
			{
				this.mDistance = 960f;
				this.mState = MainMenu_State.State_Scroll;
			}
			this.mAddAcc = 0f;
		}

		public void InitSparkles()
		{
			if (this.mApp.mUserProfile != null && this.mHeroicSparkle == null && this.mApp.mUserProfile.mAdvModeVars.mNumTimesZoneBeat[5] > 0 && !this.mApp.mUserProfile.mHasDoneHeroicUnlockEffect)
			{
				MainMenu.gNeedsOtherModeUnlockSound = true;
				this.mApp.mUserProfile.mHasDoneHeroicUnlockEffect = true;
				this.mHeroicSparkle = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_GOLDSPARKLE_AREA).Duplicate();
				float num = GameApp.DownScaleNum(1f);
				this.mHeroicSparkle.mDrawTransform.Scale(num, num);
				this.mHeroicSparkle.mDrawTransform.Translate((float)(this.mPts[5].mX + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(180))), (float)(this.mPts[5].mY - ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(60))));
			}
			if (this.mApp.mUserProfile != null && this.mChallengeSparkle == null && this.mApp.mUserProfile.mAdvModeVars.mHighestLevelBeat >= 10 && !this.mApp.mUserProfile.mHasDoneChallengeUnlockEffect)
			{
				MainMenu.gNeedsOtherModeUnlockSound = true;
				this.mApp.mUserProfile.mHasDoneChallengeUnlockEffect = true;
				this.mChallengeSparkle = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_GOLDSPARKLE_AREA).Duplicate();
				float num2 = GameApp.DownScaleNum(1f);
				this.mChallengeSparkle.mDrawTransform.Scale(num2, num2);
				this.mChallengeSparkle.mDrawTransform.Translate((float)(this.mPts[1].mX + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(180))), (float)(this.mPts[1].mY - ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(-60))));
			}
			if (this.mApp.mUserProfile != null && this.mIFSparkle == null && this.mApp.IronFrogUnlocked() && !this.mApp.mUserProfile.mHasDoneIFUnlockEffect)
			{
				MainMenu.gNeedsIFUnlockSound = true;
				this.mApp.mUserProfile.mHasDoneIFUnlockEffect = true;
				this.mIFSparkle = Res.GetPIEffectByID(ResID.PIEFFECT_NONRESIZE_GOLDSPARKLE_AREA).Duplicate();
				float num3 = GameApp.DownScaleNum(1f);
				this.mIFSparkle.mDrawTransform.Scale(num3, num3);
				this.mIFSparkle.mDrawTransform.Scale(ZumasRevenge.Common._M(3f), ZumasRevenge.Common._M1(2f));
				this.mIFSparkle.mDrawTransform.Translate((float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-30)), (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(900)));
			}
		}

		public void DoMoreGamesSlide(bool isSlidingIn)
		{
			this.mMenuScrollStartY = this.mMainMenuButtonsScrollWidget.mY;
			this.mMenuScrollPct.SetCurve(ZumasRevenge.Common._MP("b30,1,0.02,1,#  ,#  tO  o~  3~"));
			this.mMenuTikiStartX = this.mMenuTikiX;
			this.mMenuFrogStartX = this.mMenuFrogX;
			this.mMenuTikiDudeStartX = this.mMenuTikiDudeX;
			if (isSlidingIn)
			{
				this.mMenuScrollDestY = this.mMenuScrollOriginY;
				this.mMenuTikiDestX = this.mMenuTikiOriginX;
				this.mMenuFrogDestX = this.mMenuFrogOriginX;
				this.mMenuTikiDudeDestX = this.mMenuTikiDudeOriginX;
			}
			else
			{
				this.mMenuScrollDestY = this.mApp.mScreenBounds.mHeight + ZumasRevenge.Common._S(150);
				this.mMenuTikiDudeDestX = (this.mMenuTikiDestX = -ZumasRevenge.Common._S(300));
				this.mMenuFrogDestX = this.mApp.GetScreenWidth() + ZumasRevenge.Common._S(300);
			}
			this.mMainMenuOverlayWidget.DoMoreGamesSlide(isSlidingIn);
		}

		public void Init()
		{
			this.IMAGE_UI_MAINMENU_TIKI = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TIKI);
			this.mLavaXOff = 0f;
			this.mLavaXScale = 1.09f;
			this.mLavaProjectileXOff = ((GameApp.mGameRes == 768) ? 0f : ((float)((GameApp.mGameRes == 640) ? 150 : 60)));
			this.mLavaSmokeXOff = ((GameApp.mGameRes == 768) ? 0f : ((float)((GameApp.mGameRes == 640) ? 150 : 60)));
			this.mTeethSparkleXOff = ((GameApp.mGameRes == 768) ? 0f : ((float)((GameApp.mGameRes == 640) ? 144 : 57)));
			this.mUpdateCnt = 0;
			this.LoadTalkingBubbleText();
			if (this.mTalkingBubbleTextOptions.Count > 0)
			{
				Random random = new Random();
				int num = random.Next(0, this.mTalkingBubbleTextOptions.Count - 1);
				this.AddText(this.mTalkingBubbleTextOptions[num]);
			}
			this.InitSparkles();
			this.mMainMenuButtonsWidget = new MainMenuButtonsWidget(this, this.mApp);
			this.mMainMenuButtonsWidget.Resize(0, 0, this.mMainMenuButtonsWidget.mWidth, this.mMainMenuButtonsWidget.mHeight);
			this.mMainMenuButtonsScrollWidget = new ScrollWidget();
			this.mMainMenuButtonsScrollWidget.EnableBounce(false);
			this.mMenuScrollOriginY = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKI));
			this.mMainMenuButtonsScrollWidget.Resize(0, this.mMenuScrollOriginY, this.mApp.GetScreenWidth(), this.mApp.GetScreenRect().mHeight);
			this.mMainMenuButtonsScrollWidget.AddWidget(this.mMainMenuButtonsWidget);
			this.mMainMenuButtonsScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mMainMenuButtonsScrollWidget.EnablePaging(true);
			this.AddWidget(this.mMainMenuButtonsScrollWidget);
			this.mMenuTikiOriginX = (this.mMenuTikiX = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKIHEAD)) - this.mApp.mWideScreenXOffset);
			this.mMenuFrogOriginX = (this.mMenuFrogX = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_RIBBIT)) - this.mApp.mWideScreenXOffset + 42);
			this.mMenuTikiDudeOriginX = (this.mMenuTikiDudeX = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_GUY)) - this.mApp.mWideScreenXOffset);
			Insets insets = new Insets();
			insets.mLeft = ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_TIKI)) - this.IMAGE_UI_MAINMENU_TIKI.GetWidth() - this.mApp.GetScreenRect().mX + 67;
			insets.mRight = this.mApp.GetScreenWidth() - (insets.mLeft + this.mMainMenuButtonsWidget.mWidth / this.mMainMenuButtonsWidget.GetNumButtons());
			insets.mTop = 0;
			insets.mBottom = 0;
			this.mMainMenuOverlayWidget = new MainMenuOverlayWidget(this);
			this.mMainMenuOverlayWidget.Init();
			this.AddWidget(this.mMainMenuOverlayWidget);
			this.mMainMenuButtonsScrollWidget.SetScrollInsets(insets);
			this.mMainMenuButtonsScrollWidget.SetPageHorizontal(2, false);
			this.mMainMenuButtonsScrollWidget.SetPageHorizontal(0, true);
			if (this.mVolcanoSmoke == null)
			{
				this.mVolcanoSmoke = this.mApp.GetPIEffect("ls_volcano_smoke");
				this.mVolcanoSmoke.mEmitAfterTimeline = true;
				ZumasRevenge.Common.SetFXNumScale(this.mVolcanoSmoke, 4f);
				this.mEffectBatch.AddEffect(this.mVolcanoSmoke);
			}
			if (this.mTikiTeethSparkle == null)
			{
				this.mTikiTeethSparkle = this.mApp.mResourceManager.GetPIEffect("PIEFFECT_MM_SPARKLE").Duplicate();
				this.mTikiTeethSparkle.mEmitAfterTimeline = true;
				ZumasRevenge.Common.SetFXNumScale(this.mTikiTeethSparkle, 3f);
				this.mEffectBatch.AddEffect(this.mTikiTeethSparkle);
			}
			this.CreateChangeProfileButton();
			this.RehupButtons();
		}

		public void CreateChangeProfileButton()
		{
		}

		public override void AddedToManager(WidgetManager mgr)
		{
			base.AddedToManager(mgr);
		}

		public bool ShouldShowUpsellBtn()
		{
			return this.mApp.mUserProfile != null && this.mApp.mTrialType != 0 && this.mApp.mUserProfile.mAdvModeVars.mCurrentAdvZone > 2;
		}

		public void CloseUserSelDialog()
		{
		}

		public void RehupButtons()
		{
			this.GetButton(10);
			this.mDrawHat = (this.mDrawFro = (this.mDrawTuxedo = (this.mDrawMoustache = false)));
			if (this.mApp.mUserProfile != null)
			{
				this.mDrawHat = this.mApp.mUserProfile.mHeroicModeVars.mHighestZoneBeat >= 6;
				this.mDrawMoustache = this.mApp.mUserProfile.mIronFrogStats.mBestTime > 0;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < 7; i++)
				{
					for (int j = 0; j < 10; j++)
					{
						if (this.mApp.mUserProfile.mChallengeUnlockState[i, j] == 4)
						{
							num++;
						}
						else if (this.mApp.mUserProfile.mChallengeUnlockState[i, j] == 5)
						{
							num++;
							num2++;
						}
					}
				}
				this.mDrawTuxedo = num == 70;
				this.mDrawFro = num2 == 70;
			}
			ButtonWidget button = this.GetButton(17);
			if (button != null)
			{
				if (this.ShouldShowUpsellBtn())
				{
					button.mVisible = true;
					button.mDisabled = false;
				}
				else
				{
					button.mVisible = false;
					button.mDisabled = true;
				}
			}
			if (this.mChallengeMenu != null)
			{
				this.mChallengeMenu.RehupChallengeButtons();
			}
		}

		public ButtonWidget GetButton(int id)
		{
			for (int i = 0; i < this.mButtons.Count; i++)
			{
				if (this.mButtons[i].mId == id)
				{
					return this.mButtons[i];
				}
			}
			return null;
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mApp.mBambooTransition != null && this.mApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			this.mTip = null;
		}

		public void RemoveUpsellButton()
		{
			if (this.mUpsellBtn == null)
			{
				return;
			}
			for (int i = 0; i < this.mButtons.Count; i++)
			{
				if (this.mButtons[i].mId == 17)
				{
					this.mButtons.Remove(this.mButtons[i]);
					i--;
				}
			}
			this.RemoveWidget(this.mUpsellBtn);
			this.mApp.SafeDeleteWidget(this.mUpsellBtn);
			this.mUpsellBtn = null;
		}

		public void DoChangeUserDialog()
		{
			ZumaUserSelDlg zumaUserSelDlg = this.mUserSelDlg;
		}

		public void RehupUserList()
		{
			this.RehupButtons();
			if (this.mUserSelDlg != null)
			{
				ZumaProfileMgr mProfileMgr = this.mApp.mProfileMgr;
			}
		}

		public override void Update()
		{
			if (this.mApp.mMapScreen != null && !this.mApp.mMapScreen.mDirty)
			{
				return;
			}
			if (this.mApp.IsHardwareBackButtonPressed())
			{
				this.ProcessHardwareBackButton();
			}
			if (this.mApp.mCredits != null && MathUtils._geq(this.mApp.mCredits.mAlpha, 255f))
			{
				return;
			}
			if (this.mDelayedIFStartState > 0)
			{
				if (this.mDelayedIFStartState == 2)
				{
					ButtonWidget buttonWidget = null;
					for (int i = 0; i < this.mButtons.Count; i++)
					{
						if (this.mButtons[i].mId == 15)
						{
							buttonWidget = this.mButtons[i];
							break;
						}
					}
					this.mApp.mIFLoadingAnimStartCel = ((ExtraSexyButton)buttonWidget).mDownAnimation.GetFrame();
					this.mApp.StartIronFrogMode();
				}
				return;
			}
			this.mUpdateCnt++;
			float num = ZumasRevenge.Common._M(10f);
			if (this.mApp.ShowingLoadingScreen() || (this.mApp.mUserProfile != null && this.mApp.mUserProfile.mNewChallengeCupUnlocked) || this.mFirstTimeAlpha > 0)
			{
				this.MarkDirty();
			}
			if (this.mFirstTimeAlpha > 0 && this.mFirstTimeAlpha < 255)
			{
				this.mFirstTimeAlpha += ZumasRevenge.Common._M(3);
				if (this.mFirstTimeAlpha >= 255)
				{
					this.mFirstTimeAlpha = 255;
					this.mApp.StartAdvModeFirstTime();
				}
			}
			if (this.mHeroicSparkle != null)
			{
				this.mHeroicSparkle.mDrawTransform.LoadIdentity();
				float num2 = GameApp.DownScaleNum(1f);
				this.mHeroicSparkle.mDrawTransform.Scale(num2, num2);
				this.mHeroicSparkle.mDrawTransform.Translate((float)(this.mPts[5].mX + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(180))), (float)(this.mPts[5].mY - ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(-60))));
				this.mHeroicSparkle.Update();
				if (this.mHeroicSparkle.mCurNumParticles > 0)
				{
					this.MarkDirty();
				}
				else if (this.mHeroicSparkle.mFrameNum > 2f)
				{
					this.mHeroicSparkle = null;
				}
			}
			if (this.mIFSparkle != null)
			{
				this.mIFSparkle.mDrawTransform.LoadIdentity();
				float num3 = GameApp.DownScaleNum(1f);
				this.mIFSparkle.mDrawTransform.Scale(num3, num3);
				this.mIFSparkle.mDrawTransform.Scale(ZumasRevenge.Common._M(3f), ZumasRevenge.Common._M1(2f));
				this.mIFSparkle.mDrawTransform.Translate((float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M(-30)), (float)ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(900)));
				this.mIFSparkle.Update();
				if (this.mIFSparkle.mCurNumParticles > 0)
				{
					this.MarkDirty();
				}
				else if (this.mIFSparkle.mFrameNum > 2f)
				{
					this.mIFSparkle = null;
				}
			}
			if (this.mChallengeSparkle != null)
			{
				this.mChallengeSparkle.mDrawTransform.LoadIdentity();
				float num4 = GameApp.DownScaleNum(1f);
				this.mChallengeSparkle.mDrawTransform.Scale(num4, num4);
				this.mChallengeSparkle.mDrawTransform.Translate((float)(this.mPts[1].mX + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(180)) + ZumasRevenge.Common._S(960)), (float)(this.mPts[1].mY - ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(-60))));
				this.mChallengeSparkle.Update();
				if (this.mChallengeSparkle.mCurNumParticles > 0)
				{
					this.MarkDirty();
				}
				else if (this.mChallengeSparkle.mFrameNum > 2f)
				{
					this.mChallengeSparkle = null;
				}
			}
			if (this.mIFUnlockAnim != null)
			{
				this.mIFUnlockAnim.Update();
				if (this.mIFUnlockAnim.mMainSpriteInst.mFrameNum >= (float)(this.mIFUnlockAnim.mMainSpriteInst.mDef.mFrames.Count - 1))
				{
					this.mIFUnlockAnim = null;
					this.RehupButtons();
				}
			}
			if (this.mUpdateCnt >= ZumasRevenge.Common._M(50))
			{
				if (MainMenu.gNeedsIFUnlockSound)
				{
					MainMenu.gNeedsIFUnlockSound = false;
					this.mApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_IRON_FROG_UNLOCKED));
				}
				if (MainMenu.gNeedsOtherModeUnlockSound)
				{
					MainMenu.gNeedsOtherModeUnlockSound = false;
				}
			}
			if (this.mApp.Is3DAccelerated())
			{
				if (this.mApp.mHasFocus)
				{
					this.MarkDirty();
				}
				if (this.mIncLavaAlpha)
				{
					this.mLavaAlpha += ZumasRevenge.Common._M(1.4f);
					if (this.mLavaAlpha >= 255f)
					{
						this.mLavaAlpha = 255f;
						this.mIncLavaAlpha = false;
					}
				}
				else
				{
					this.mLavaAlpha -= ZumasRevenge.Common._M(1.4f);
					if (this.mLavaAlpha <= 0f)
					{
						this.mLavaAlpha = 0f;
						this.mIncLavaAlpha = true;
					}
				}
			}
			if (this.mMenuScrollPct.IsDoingCurve())
			{
				float num5 = (float)this.mMenuScrollPct.GetOutVal();
				float num6 = num5 * (float)(this.mMenuScrollDestY - this.mMenuScrollStartY);
				this.mMainMenuButtonsScrollWidget.Resize(this.mMainMenuButtonsScrollWidget.mX, (int)((float)this.mMenuScrollStartY + num6), this.mMainMenuButtonsScrollWidget.mWidth, this.mMainMenuButtonsScrollWidget.mHeight);
				float num7 = num5 * (float)(this.mMenuTikiDestX - this.mMenuTikiStartX);
				this.mMenuTikiX = (int)((float)this.mMenuTikiStartX + num7);
				float num8 = num5 * (float)(this.mMenuFrogDestX - this.mMenuFrogStartX);
				this.mMenuFrogX = (int)((float)this.mMenuFrogStartX + num8);
				float num9 = num5 * (float)(this.mMenuTikiDudeDestX - this.mMenuTikiDudeStartX);
				this.mMenuTikiDudeX = (int)((float)this.mMenuTikiDudeStartX + num9);
				if (this.mMainMenuOverlayWidget != null)
				{
					this.mMainMenuOverlayWidget.UpdateOverlaySlide(num5);
				}
			}
			if (this.mApp.mMoreGames != null && this.mApp.mMoreGames.IsReadyForDelete())
			{
				this.mApp.DeleteMoreGames(false);
			}
			if (this.mState == MainMenu_State.State_MainMenu && this.mApp.Is3DAccelerated() && this.mApp.mHasFocus)
			{
				this.MarkDirty();
			}
			for (int j = 0; j < this.mText.Count; j++)
			{
				MainMenu.MMText mmtext = this.mText[j];
				if (mmtext.mFadingIn && mmtext.mAlpha < 255f)
				{
					this.MarkDirty();
					mmtext.mAlpha += num;
					if (mmtext.mAlpha >= 255f)
					{
						mmtext.mAlpha = 255f;
					}
				}
				else if (!mmtext.mFadingIn)
				{
					this.MarkDirty();
					mmtext.mAlpha -= num;
					if (mmtext.mAlpha <= 0f)
					{
						this.mText.Remove(mmtext);
						j--;
					}
				}
			}
			if (this.mVolcanoSmoke != null)
			{
				this.mVolcanoSmoke.mDrawTransform.LoadIdentity();
				this.mVolcanoSmoke.mDrawTransform.Scale(ZumasRevenge.Common._DS(1.4f), ZumasRevenge.Common._DS(1.4f));
				this.mVolcanoSmoke.mDrawTransform.Translate((float)(ZumasRevenge.Common._S(this.mX) + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(1440))) + this.mLavaSmokeXOff, (float)(ZumasRevenge.Common._S(this.mY) + ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(115))));
				this.mVolcanoSmoke.Update();
			}
			if (this.mTikiTeethSparkle != null)
			{
				this.mTikiTeethSparkle.mDrawTransform.LoadIdentity();
				this.mTikiTeethSparkle.mDrawTransform.Scale(ZumasRevenge.Common._DS(1.4f), ZumasRevenge.Common._DS(1.4f));
				this.mTikiTeethSparkle.mDrawTransform.Translate((float)(ZumasRevenge.Common._S(this.mX) + ZumasRevenge.Common._DS(ZumasRevenge.Common._M(165))) + this.mTeethSparkleXOff, (float)(ZumasRevenge.Common._S(this.mY) + ZumasRevenge.Common._DS(ZumasRevenge.Common._M1(269))));
				this.mTikiTeethSparkle.Update();
				if (SexyFramework.Common.Rand(2000) == 0 && this.mTikiTeethSparkle.mCurNumParticles == 0 && this.mTikiTeethSparkle.mFrameNum >= (float)this.mTikiTeethSparkle.mLastFrameNum)
				{
					this.mTikiTeethSparkle.ResetAnim();
					this.mTikiTeethSparkle.mRandSeeds.Clear();
					this.mTikiTeethSparkle.mRandSeeds.Add(SexyFramework.Common.Rand(1000));
				}
			}
		}

		public MainMenu.MMText AddText(string txt)
		{
			if (txt.Length <= 0)
			{
				return null;
			}
			MainMenu.MMText mmtext = new MainMenu.MMText();
			mmtext.mAlpha = 0f;
			mmtext.mFadingIn = true;
			mmtext.mText = txt;
			this.mText.Insert(0, mmtext);
			this.FadeOutText(1);
			return this.mText[0];
		}

		public void FadeOutText(int start)
		{
			for (int i = start; i < this.mText.Count; i++)
			{
				this.mText[i].mFadingIn = false;
			}
		}

		public void DrawTalkingBubble(Graphics g, int x, int y, int width, int height)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, 179));
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_TL);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_TOP);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_TAIL);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_BOT);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_BL);
			Image imageByID6 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TALK_BUBBLE_SIDE);
			g.DrawImage(imageByID, x, y);
			int i = x + imageByID.GetWidth();
			int num = x + width - imageByID.GetWidth();
			g.ClearClipRect();
			g.SetClipRect(i, y, num - i, imageByID2.GetHeight());
			while (i < num)
			{
				g.DrawImage(imageByID2, i, y);
				i += imageByID2.GetWidth();
			}
			g.ClearClipRect();
			g.DrawImageMirror(imageByID, num, y);
			g.DrawImage(imageByID3, x - imageByID3.GetWidth() + ZumasRevenge.Common._DS(MainMenu.tailXOff), y + imageByID.GetHeight());
			int j = y + imageByID.GetHeight() + imageByID3.GetHeight();
			int num2 = y + height - imageByID4.GetHeight();
			g.ClearClipRect();
			g.SetClipRect(x, y + imageByID.GetHeight(), width, y + height - imageByID5.GetHeight() - (y + imageByID.GetHeight()));
			while (j < num2)
			{
				g.DrawImage(imageByID6, x, j);
				j += imageByID6.GetHeight();
			}
			for (j = y + imageByID.GetHeight(); j < num2; j += imageByID6.GetHeight())
			{
				g.DrawImageMirror(imageByID6, x + width - imageByID6.GetWidth(), j);
			}
			g.ClearClipRect();
			g.DrawImage(imageByID5, x, num2);
			i = x + imageByID5.GetWidth();
			num = x + width - imageByID5.GetWidth();
			g.ClearClipRect();
			g.SetClipRect(i, num2, num - i, imageByID4.GetHeight());
			while (i < num)
			{
				g.DrawImage(imageByID4, i, num2);
				i += imageByID4.GetWidth();
			}
			g.ClearClipRect();
			g.DrawImageMirror(imageByID5, num, num2);
			g.SetColorizeImages(false);
			g.SetColor(new Color(255, 255, 255, 179));
			int num3 = x + imageByID6.GetWidth();
			int num4 = y + imageByID.GetHeight();
			int theWidth = x + width - imageByID6.GetWidth() - num3;
			int theHeight = y + height - imageByID5.GetHeight() - num4;
			g.FillRect(num3, num4, theWidth, theHeight);
			g.FillRect(x + ZumasRevenge.Common._DS(MainMenu.tailXOff), num4, imageByID6.GetWidth() - ZumasRevenge.Common._DS(MainMenu.tailXOff), imageByID3.GetHeight());
		}

		public void LoadTalkingBubbleText()
		{
			this.mTalkingBubbleTextOptions.Capacity = 45;
			for (int i = 614; i <= 658; i++)
			{
				this.mTalkingBubbleTextOptions.Add(TextManager.getInstance().getString(i));
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mApp.mCredits != null && MathUtils._geq(this.mApp.mCredits.mAlpha, 255f))
			{
				return;
			}
			if (this.mChallengeMenu != null)
			{
				BambooTransition mBambooTransition = this.mApp.mBambooTransition;
				return;
			}
			if (this.mTikiTemple != null)
			{
				return;
			}
			if (this.mApp != null && this.mApp.mMapScreen != null)
			{
				return;
			}
			Image imageByID = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_BG);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_LAVA);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_GUY);
			Image imageByID4 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_RIGHTENDPIECE);
			Image imageByID5 = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_ENDCAP);
			if (this.mDelayedIFStartState == 1)
			{
				this.mDelayedIFStartState = 2;
			}
			float num = (float)imageByID.GetWidth() / (float)this.mApp.GetScreenRect().mWidth;
			float num2 = (float)imageByID.GetHeight() / (float)this.mApp.GetScreenRect().mHeight;
			g.DrawImage(imageByID, 0, 0, this.mApp.GetScreenRect().mWidth, this.mApp.GetScreenRect().mHeight);
			g.SetDrawMode(1);
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)this.mLavaAlpha));
			g.DrawImage(imageByID2, (int)((float)(ZumasRevenge.Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_LAVA)) - this.mApp.mWideScreenXOffset + this.mApp.GetScreenRect().mX) + this.mLavaXOff), ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_LAVA)), (int)((float)imageByID2.GetWidth() * this.mLavaXScale), imageByID2.GetHeight());
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
			g.DrawImage(imageByID3, this.mMenuTikiDudeX, ZumasRevenge.Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_GUY)));
			if (this.mMainMenuButtonsWidget != null && this.mMainMenuButtonsWidget.mVisible)
			{
				if (this.mMainMenuButtonsWidget.mX >= 0)
				{
					g.DrawImageMirror(imageByID4, this.mMainMenuButtonsWidget.mX - imageByID4.GetWidth(), this.mMainMenuButtonsScrollWidget.mY);
					g.DrawImageMirror(imageByID5, this.mMainMenuButtonsWidget.mX - imageByID4.GetWidth(), this.mMainMenuButtonsScrollWidget.mY - ZumasRevenge.Common._S(42));
					g.DrawImage(this.IMAGE_UI_MAINMENU_TIKI, this.mMainMenuButtonsWidget.mX - this.IMAGE_UI_MAINMENU_TIKI.GetWidth(), this.mMainMenuButtonsScrollWidget.mY);
				}
				else if (this.mMainMenuButtonsWidget.mX + this.mMainMenuButtonsWidget.mWidth <= this.mApp.GetScreenWidth())
				{
					g.DrawImage(imageByID4, this.mMainMenuButtonsWidget.mX + this.mMainMenuButtonsWidget.mWidth, this.mMainMenuButtonsScrollWidget.mY);
					g.DrawImage(imageByID5, this.mMainMenuButtonsWidget.mX + this.mMainMenuButtonsWidget.mWidth + imageByID4.GetWidth() + this.mApp.GetScreenRect().mX - imageByID5.GetWidth(), this.mMainMenuButtonsScrollWidget.mY - ZumasRevenge.Common._S(42));
					g.DrawImageMirror(this.IMAGE_UI_MAINMENU_TIKI, this.mMainMenuButtonsWidget.mX + this.mMainMenuButtonsWidget.mWidth, this.mMainMenuButtonsScrollWidget.mY);
				}
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET);
			Font fontByID2 = Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BLACK);
			g.SetFont(fontByID);
			g.SetColor(Color.White);
			if (GameApp.USE_XBOX_SERVICE)
			{
				string theString;
				if (this.mApp.mUserProfile != null)
				{
					StringBuilder stringBuilder = new StringBuilder(TextManager.getInstance().getString(660));
					stringBuilder.Replace("$1", " " + this.mApp.mUserProfile.GetName());
					theString = stringBuilder.ToString();
				}
				else
				{
					theString = TextManager.getInstance().getString(659);
				}
				g.WriteString(theString, 0, ZumasRevenge.Common._S(ZumasRevenge.Common._M(30)), this.mWidth);
				g.SetFont(fontByID2);
				this.DrawChangeProfileString(g);
			}
			if (this.mState == MainMenu_State.State_Scroll || this.mState == MainMenu_State.State_MainMenu)
			{
				ZumasRevenge.Common._S(ZumasRevenge.Common._M(375));
				ZumasRevenge.Common._S(ZumasRevenge.Common._M(75));
				ZumasRevenge.Common._S(ZumasRevenge.Common._M(1));
				this.DrawTikiTalk(g);
				this.mEffectBatch.DrawBatch(g);
			}
			if (this.mTip != null)
			{
				this.mTip.Draw(g);
			}
		}

		public void DrawTikiTalk(Graphics g)
		{
			if (this.mApp.mMoreGames != null || this.mText.Count == 0)
			{
				return;
			}
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_BASE);
			string text = this.mText[0].mText;
			int num = ZumasRevenge.Common._DS(800);
			int num2 = ZumasRevenge.Common._DS(100);
			int num3 = (int)((float)(this.mApp.GetScreenRect().mWidth - num) * 0.5f);
			int num4 = ZumasRevenge.Common._DS(75);
			int num5 = ZumasRevenge.Common._DS(15);
			int num6 = num - num5 * 2;
			int num7 = num2 - num5 * 2;
			int num8 = ZumasRevenge.Common._GetWordWrappedHeight(text, fontByID, num6);
			if (num8 > num7)
			{
				int num9 = num8 - num7;
				num7 += num9;
				num2 += num9;
			}
			Rect theRect = new Rect(num3 + num5, num4 + num5, num6, num7);
			theRect.mY += (int)((float)(num7 - num8) * 0.5f);
			this.DrawTalkingBubble(g, num3, num4, num, num2 + 10);
			g.SetFont(fontByID);
			g.SetColor(Color.Black);
			g.WriteWordWrapped(theRect, text, -1, 0);
		}

		public void DrawChangeProfileString(Graphics g)
		{
		}

		public void SelectUser(string user_name)
		{
		}

		public void HideChallengeMenu()
		{
			this.RemoveWidget(this.mChallengeMenu);
			this.mApp.SafeDeleteWidget(this.mChallengeMenu);
			this.mChallengeMenu = null;
			this.mState = MainMenu_State.State_MainMenu;
			this.ShowScrollButtons();
		}

		public void ShowTikiTemple()
		{
			this.mTikiTemple = new TikiTemple();
			this.mTikiTemple.Resize(this.mApp.GetScreenRect());
			this.mTikiTemple.Init();
			this.mWidgetManager.AddWidget(this.mTikiTemple);
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(false);
			}
			this.mState = MainMenu_State.State_TikiTemple;
		}

		public void ShowAchievements()
		{
			this.mAchievements = new Achievements();
			this.mAchievements.Resize(this.mApp.GetScreenRect());
			this.mAchievements.Init();
			this.mWidgetManager.AddWidget(this.mAchievements);
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(false);
			}
			this.mState = MainMenu_State.State_Achievement;
		}

		public void ShowLeaderBoards()
		{
			this.mLeaderBoards = new LeaderBoards();
			this.mLeaderBoards.Resize(this.mApp.GetScreenRect());
			this.mLeaderBoards.Init();
			this.mWidgetManager.AddWidget(this.mLeaderBoards);
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(false);
			}
			this.mState = MainMenu_State.State_LeaderBoards;
		}

		public void HideTikiTemple()
		{
			this.mWidgetManager.RemoveWidget(this.mTikiTemple);
			this.mApp.SafeDeleteWidget(this.mTikiTemple);
			this.mTikiTemple = null;
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(true);
			}
			this.mState = MainMenu_State.State_MainMenu;
		}

		public void HideLeaderBoards()
		{
			this.mWidgetManager.RemoveWidget(this.mLeaderBoards);
			this.mApp.SafeDeleteWidget(this.mLeaderBoards);
			this.mLeaderBoards = null;
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(true);
			}
			this.mState = MainMenu_State.State_MainMenu;
		}

		public void HideAchievements()
		{
			this.mWidgetManager.RemoveWidget(this.mAchievements);
			this.RemoveWidget(this.mAchievements);
			this.mApp.SafeDeleteWidget(this.mAchievements);
			this.mAchievements = null;
			if (this.mMainMenuButtonsWidget != null)
			{
				this.mMainMenuButtonsWidget.SetVisible(true);
			}
			this.mState = MainMenu_State.State_MainMenu;
		}

		public void ShowChallengeMenuFromMainMenu()
		{
			this.mApp.LoadAllThumbnails();
			this.mChallengeMenu = new ChallengeMenu(this.mApp, this, true);
			this.mChallengeMenu.Resize(this.mApp.GetScreenRect().mX, this.mApp.GetScreenRect().mY, this.mApp.GetScreenRect().mWidth - this.mApp.GetScreenRect().mX, this.mApp.GetScreenRect().mHeight - this.mApp.GetScreenRect().mY);
			this.mChallengeMenu.Init();
			this.AddWidget(this.mChallengeMenu);
			this.mChallengeMenu.InitCS();
			this.RehupButtons();
			this.mChallengeMenu.mCSVisFrame = this.mUpdateCnt;
			this.mState = MainMenu_State.State_CS;
			this.HideScrollButtons();
		}

		public void ShowChallengeMenu()
		{
			this.mChallengeMenu = new ChallengeMenu(this.mApp, this, false);
			this.mChallengeMenu.Resize(this.mApp.GetScreenRect().mX, this.mApp.GetScreenRect().mY, this.mApp.GetScreenRect().mWidth - this.mApp.GetScreenRect().mX, this.mApp.GetScreenRect().mHeight - this.mApp.GetScreenRect().mY);
			this.mChallengeMenu.Init();
			this.AddWidget(this.mChallengeMenu);
			this.mChallengeMenu.InitCS();
			this.RehupButtons();
			this.mChallengeMenu.mCSVisFrame = this.mUpdateCnt;
			this.mState = MainMenu_State.State_CS;
		}

		public void HideScrollButtons()
		{
			this.mMainMenuButtonsWidget.HideScrollButtons();
		}

		public void ShowScrollButtons()
		{
			this.mMainMenuButtonsWidget.ShowScrollButtons();
		}

		public bool ShowingTikiTemple()
		{
			return this.mTikiTemple != null;
		}

		public void ProcessHardwareBackButton()
		{
			if (this.mApp.mMapScreen != null)
			{
				return;
			}
			Dialog dialog = this.mApp.GetDialog(2);
			if (dialog != null)
			{
				(dialog as OptionsDialog).ProcessHardwareBackButton();
				return;
			}
			if (GameApp.gApp.mAboutInfo != null)
			{
				GameApp.gApp.mAboutInfo.ProcessHardwareBackButton();
				return;
			}
			if (GameApp.gApp.mLegalInfo != null)
			{
				GameApp.gApp.mLegalInfo.ProcessHardwareBackButton();
				return;
			}
			if (GameApp.gApp.mLegalInfo != null)
			{
				GameApp.gApp.mLegalInfo.ProcessHardwareBackButton();
				return;
			}
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			switch (this.mState)
			{
			case MainMenu_State.State_CS:
				if (this.mChallengeMenu.ProcessHardwareBackButton())
				{
					this.mState = MainMenu_State.State_MainMenu;
					return;
				}
				return;
			case MainMenu_State.State_TikiTemple:
				this.mState = MainMenu_State.State_MainMenu;
				this.mTikiTemple.ProcessHardwareBackButton();
				return;
			case MainMenu_State.State_LeaderBoards:
				if (this.mLeaderBoards.ProcessHardwareBackButton())
				{
					this.mState = MainMenu_State.State_MainMenu;
					return;
				}
				return;
			case MainMenu_State.State_Achievement:
				if (this.mAchievements.ProcessHardwareBackButton())
				{
					this.mState = MainMenu_State.State_MainMenu;
					return;
				}
				return;
			case MainMenu_State.State_MapScreen:
				this.mState = MainMenu_State.State_MainMenu;
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			case MainMenu_State.State_UnlockPrompt:
				this.mState = MainMenu_State.State_MainMenu;
				this.mApp.GetDialog(0).ButtonDepress(1000);
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			case MainMenu_State.State_QuitPrompt:
				this.mState = MainMenu_State.State_MainMenu;
				this.mApp.GetDialog(1).ButtonDepress(1001);
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			if (GameApp.gApp.mGenericHelp != null)
			{
				this.mState = MainMenu_State.State_MainMenu;
				GameApp.gApp.mGenericHelp.ForceCloseDialog();
				this.mApp.OnHardwareBackButtonPressProcessed();
				return;
			}
			this.mState = MainMenu_State.State_QuitPrompt;
			this.mApp.DoQuitPromptDialog();
			this.mApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessYesNo);
			this.mApp.OnHardwareBackButtonPressProcessed();
		}

		public void ProcessYesNo(int theId)
		{
			this.mState = MainMenu_State.State_MainMenu;
			if (theId == 1000)
			{
				if (!this.mApp.IsRegistered() && this.mApp.mTrialType == 1 && this.mApp.GetBoolean("UpsellExit", false))
				{
					this.mApp.DoUpsell(true);
				}
				else
				{
					this.mApp.SaveProfile();
				}
				this.mApp.Shutdown();
			}
		}

		public void StartChallengeGame()
		{
			this.mCSOverRect = default(Rect);
			this.mApp.StartGauntletMode(this.mGauntletModLevel_id, this.mCSOverRect);
		}

		private static int MAX_VOLCANO_PROJECTILES = 6;

		private static bool gNeedsIFUnlockSound = false;

		private static bool gNeedsOtherModeUnlockSound = false;

		public static int gScreenShake = 0;

		public static int gScreenShakeTimer = 0;

		public float mAdjust;

		private Point[] mPts = new Point[]
		{
			new Point((int)ZumasRevenge.Common._DSA(1100f, 0f), ZumasRevenge.Common._S(ZumasRevenge.Common._M(253))),
			new Point((int)ZumasRevenge.Common._DSA(1102f, 0f), ZumasRevenge.Common._S(ZumasRevenge.Common._M1(315))),
			new Point(ZumasRevenge.Common._DS(ZumasRevenge.Common._M2(0)), ZumasRevenge.Common._S(ZumasRevenge.Common._M3(376))),
			new Point((int)ZumasRevenge.Common._DSA(1100f, 0f), ZumasRevenge.Common._S(ZumasRevenge.Common._M4(490))),
			new Point((int)ZumasRevenge.Common._DSA(1288f, 0f), ZumasRevenge.Common._S(ZumasRevenge.Common._M5(490))),
			new Point((int)ZumasRevenge.Common._DSA(1100f, 0f), ZumasRevenge.Common._S(ZumasRevenge.Common._M6(430)))
		};

		public GameApp mApp;

		public ZumaTip mTip;

		public List<MainMenu.MMText> mText = new List<MainMenu.MMText>();

		public PIEffect mHeroicSparkle;

		public PIEffect mIFSparkle;

		public PIEffect mChallengeSparkle;

		public PIEffectBatch mEffectBatch;

		public PopAnim mIFUnlockAnim;

		public float mDistance;

		public float mAddAcc;

		public float mLavaAlpha;

		public MainMenu_State mState;

		public int mDelayedIFStartState;

		public int mFirstTimeAlpha;

		public bool mIncLavaAlpha;

		public bool mDrawHat;

		public bool mDrawMoustache;

		public bool mDrawTuxedo;

		public bool mDrawFro;

		public bool mSkipEnterSound;

		public Achievements mAchievements;

		public ChallengeMenu mChallengeMenu;

		public TikiTemple mTikiTemple;

		public LeaderBoards mLeaderBoards;

		public ZumaUserSelDlg mUserSelDlg;

		private List<ButtonWidget> mButtons = new List<ButtonWidget>();

		private ButtonWidget mUpsellBtn;

		private ButtonWidget mChangeProfileBtn;

		public ScrollWidget mMainMenuButtonsScrollWidget;

		public MainMenuButtonsWidget mMainMenuButtonsWidget;

		public ButtonWidget mMoreGamesButton;

		public ButtonWidget mOptionsButton;

		public ButtonWidget mUnlockButton;

		public int mMenuScrollOriginY;

		public int mMenuScrollDestY;

		public int mMenuScrollStartY;

		public int mMenuTikiStartX;

		public int mMenuTikiOriginX;

		public int mMenuTikiDestX;

		public int mMenuTikiX;

		public int mMenuFrogStartX;

		public int mMenuFrogOriginX;

		public int mMenuFrogDestX;

		public int mMenuFrogX;

		public int mMenuTikiDudeStartX;

		public int mMenuTikiDudeOriginX;

		public int mMenuTikiDudeDestX;

		public int mMenuTikiDudeX;

		public ButtonWidget mMonkeyButton;

		public ButtonWidget mLogButton;

		private MainMenuOverlayWidget mMainMenuOverlayWidget;

		private CurvedVal mMenuScrollPct = new CurvedVal();

		private List<string> mTalkingBubbleTextOptions = new List<string>();

		private MainMenu.VolcanoProjectile[] mVolcanoProjectiles;

		private PIEffect mVolcanoSmoke;

		private PIEffect mTikiTeethSparkle;

		private float mLavaXOff;

		private float mLavaXScale;

		private float mLavaProjectileXOff;

		private float mLavaSmokeXOff;

		private float mTeethSparkleXOff;

		private Image IMAGE_UI_MAINMENU_TIKI;

		public Rect mCSOverRect;

		public string mGauntletModLevel_id;

		private static int tailXOff = 5;

		public class VolcanoProjectile
		{
			public PIEffect mProjectile;

			public bool mInUse;
		}

		public class MMText
		{
			public float mAlpha;

			public string mText;

			public string mExtraText;

			public bool mFadingIn;

			public bool mShowChallengeCrowns;

			public int mYOff;
		}
	}
}
