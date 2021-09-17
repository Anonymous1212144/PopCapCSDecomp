using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class MainMenu : Bej3Widget
	{
		private void GoToPage(int page)
		{
			MainMenu.mScrollwidgetPage = page;
			this.mScrollWidget.ScrollToPoint(new Point(ConstantsWP.MAIN_MENU_TAB_WIDTH * MainMenu.mScrollwidgetPage, 0), true);
			this.mContainer.MakeButtonsFullyVisible();
		}

		public MainMenu()
			: base(Menu_Type.MENU_MAINMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mBJ3LogoShowing = false;
			this.mIntroFinished = false;
			this.mScrollWidget = null;
			this.mCanAllowSlide = false;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_SHOW_CURVE, this.mShowCurve);
			this.mCanAllowSlide = false;
			this.mDoesSlideInFromBottom = false;
			this.mLoadingThreadCompleted = false;
			this.mPartnerBlackAlpha = 0f;
			this.mBJ3LogoAlpha.SetConstant(0.0);
			int integer = GlobalMembers.gSexyApp.GetInteger("NumLogos", 0);
			for (int i = 0; i < integer; i++)
			{
				this.mPartnerBlackAlpha = 1f;
				string @string = GlobalMembers.gSexyApp.GetString(string.Format("Logo{0}File", i + 1));
				PartnerLogo partnerLogo = new PartnerLogo();
				partnerLogo.mImage = GlobalMembers.gSexyApp.GetImage(@string, true, true, false);
				partnerLogo.mTime = (partnerLogo.mOrgTime = GlobalMembers.gSexyApp.GetInteger(string.Format("Logo{0}HoldTime", i + 1), 300));
				if (partnerLogo.mImage != null)
				{
					partnerLogo.mImage.AddImageFlags(8U);
					this.mPartnerLogos.Add(partnerLogo);
				}
			}
			this.mHasLoaderResources = true;
			this.mClip = false;
			this.mHighestVirtFPS = 0;
			this.mHasAlpha = true;
			this.mRotation.SetConstant(0.0);
			this.mContainer = null;
			this.mLoaded = false;
			this.mSwitchedMusic = false;
			this.mFinishedLoadSequence = false;
			this.mBkgBlackAlpha.SetConstant(0.0);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_FORE_BLACK_ALPHA, this.mForeBlackAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_LOGO_ALPHA_FADE_IN, this.mLogoAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_LOADER_ALPHA_FADE_IN, this.mLoaderAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_TIP_TEXT_ALPHA_FADE_IN, this.mTipTextAlpha);
			this.mContainer = null;
			this.mDispLoadPct = 0f;
			this.mDrawDeviceId = false;
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			try
			{
				if (args.button == SystemButtons.Back)
				{
					args.processed = true;
					if (this.mContainer.CurrentPage == 0)
					{
						MainMenuOptions mainMenuOptions = GlobalMembers.gApp.mMenus[5] as MainMenuOptions;
						if (mainMenuOptions != null)
						{
							mainMenuOptions.OnSystemButtonPressed(args);
						}
					}
					else
					{
						this.mContainer.ButtonDepress(11);
					}
				}
			}
			catch (Exception)
			{
				GlobalMembers.gApp.WantExit = true;
			}
		}

		public override void Dispose()
		{
			GlobalMembers.KILL_WIDGET(this.mContainer);
			GlobalMembers.KILL_WIDGET(this.mScrollWidget);
			this.RemoveAllWidgets(true, false);
			base.Dispose();
		}

		public virtual int GetBoardX()
		{
			return GlobalMembers.RS(ConstantsWP.BOARD_X);
		}

		public int GetBoardCenterX()
		{
			return this.GetBoardX() + 400;
		}

		public virtual void QuitGameRequest()
		{
			Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.DoDialog(33, true, GlobalMembers._ID("QUIT GAME?", 5000), GlobalMembers._ID("Are you sure you want to quit the game?", 5001), "", 1);
			int dialog_RESTART_GAME_WIDTH = ConstantsWP.DIALOG_RESTART_GAME_WIDTH;
			bej3Dialog.Resize(GlobalMembers.S(this.GetBoardCenterX()) - dialog_RESTART_GAME_WIDTH / 2, this.mHeight / 2, dialog_RESTART_GAME_WIDTH, bej3Dialog.GetPreferredHeight(dialog_RESTART_GAME_WIDTH));
			Bej3Button bej3Button = (Bej3Button)bej3Dialog.mYesButton;
			bej3Button.SetLabel(GlobalMembers._ID("QUIT GAME", 5002));
			bej3Dialog.SetButtonPosition(bej3Button, 0);
			bej3Dialog.mResult = int.MaxValue;
			bej3Button = (Bej3Button)bej3Dialog.mNoButton;
			bej3Button.SetLabel(GlobalMembers._ID("CANCEL", 3239));
			bej3Button.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			bej3Dialog.mFlushPriority = 1;
			bej3Dialog.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
		}

		public override void TouchBegan(SexyAppBase.Touch touch)
		{
		}

		public override void GotFocus()
		{
			if (this.mIntroFinished)
			{
				this.ShowLogo();
			}
		}

		public void SetupBackground()
		{
		}

		public override void Resize(Rect theRect)
		{
			base.Resize(theRect);
		}

		public override void UpdateAll(ModalFlags theFlags)
		{
			if (!this.mVisible)
			{
				return;
			}
			base.UpdateAll(theFlags);
		}

		public override void Update()
		{
			if (this.mLoaded)
			{
				this.mBJ3LogoAlpha.IncInVal();
			}
			base.Update();
			if (this.mPartnerLogos.size<PartnerLogo>() > 0)
			{
				this.mPartnerBlackAlpha = 1f;
				PartnerLogo partnerLogo = this.mPartnerLogos[0];
				if (partnerLogo.mAlpha < 255 && partnerLogo.mTime == partnerLogo.mOrgTime)
				{
					partnerLogo.mAlpha += GlobalMembers.M(5);
					if (partnerLogo.mAlpha >= 255)
					{
						partnerLogo.mAlpha = 255;
					}
				}
				else if (--partnerLogo.mTime <= 0)
				{
					partnerLogo.mAlpha -= GlobalMembers.M(5);
					if (partnerLogo.mAlpha <= 0)
					{
						partnerLogo.mImage.Dispose();
						this.mPartnerLogos.RemoveAt(0);
					}
				}
				this.MarkDirty();
				return;
			}
			this.mPartnerBlackAlpha = Math.Max(0f, this.mPartnerBlackAlpha - GlobalMembers.M(0.05f));
			if (this.mLoaded && this.mRotation == 0.0 && !this.mFinishedLoadSequence)
			{
				this.mFinishedLoadSequence = true;
				if (GlobalMembers.gApp.mHasFocus)
				{
					if (GlobalMembers.gApp.mProfile.mProfileName.Length == 0)
					{
						GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_WELCOMETOBEJEWELED);
					}
					else
					{
						GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_WELCOMEBACK);
					}
				}
			}
			if (this.mLoaded && this.mHasLoaderResources && this.mLogoAlpha == 0.0)
			{
				BejeweledLivePlusApp.UnloadContent("Loader");
				this.mHasLoaderResources = false;
			}
			if (this.mLoaded && this.mRotation == 0.0 && GlobalMembers.gApp.mProfile.mProfileName.Length == 0 && GlobalMembers.gApp.GetDialog(1) == null)
			{
				if (GlobalMembers.gApp.mLastUser.Length != 0)
				{
					GlobalMembers.gApp.mProfile.LoadProfile(GlobalMembers.gApp.mLastUser);
					GlobalMembers.gApp.mLastUser = "";
				}
				else if (GlobalMembers.gApp.mDialogMap.Count == 0)
				{
					GlobalMembers.gApp.DoWelcomeDialog();
				}
			}
			float num = GlobalMembers.gApp.mResourceManager.GetLoadResourcesListProgress(GlobalMembers.gApp.initialLoadGroups);
			this.mDispLoadPct += (num - this.mDispLoadPct) * ConstantsWP.LOADING_SMOOTH_STEP;
			if (!this.mLoaded && (float)this.mLoaderAlpha >= 1f)
			{
				GlobalMembers.gApp.DoInitWhileLoading();
			}
			if (num >= 1f && !this.mLoaded && this.mDispLoadPct >= 0.995f)
			{
				this.OnLoaded();
				GlobalMembers.gApp.mCurveValCache.GetCurvedValMult(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_BUTTON_ROTATION_ADD, this.mButtonRotationAdd);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_LOGO_ALPHA_FADE_OUT, this.mLogoAlpha);
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_LOADER_ALPHA_FADE_OUT, this.mLoaderAlpha);
				GlobalMembers.gApp.mCurveValCache.GetCurvedValMult(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_TIP_TEXT_ALPHA_FADE_OUT, this.mTipTextAlpha);
				if (!this.mSwitchedMusic)
				{
					this.mSwitchedMusic = true;
				}
				this.mLoaded = true;
				this.LoadingComplete();
				this.mContainer.Show();
				GlobalMembers.gApp.GoToInterfaceState(InterfaceState.INTERFACE_STATE_MAINMENU);
				if (GlobalMembers.gApp.mMusicInterface.isPlayingUserMusic())
				{
					GlobalMembers.gApp.ConfirmUserMusic();
				}
			}
			bool flag = this.mLoaded;
			float num2 = (float)GlobalMembers.gSexyApp.mScreenBounds.mWidth / (float)GlobalMembers.gSexyApp.mScreenBounds.mHeight;
			float inFovDegrees = GlobalMembers.M(38.5f) * num2;
			this.mCamera.Init(inFovDegrees, num2, 0.1f, 1000f);
			this.mButtonCamera.Init(inFovDegrees, num2, 0.1f, 1000f);
			SexyCoords3 sexyCoords = new SexyCoords3();
			sexyCoords.Translate(0f, GlobalMembers.M(-0.4f), 0f);
			this.mCamera.SetCoords(this.mCamera.GetCoords().Leave(sexyCoords));
			this.mButtonCamera.SetCoords(this.mButtonCamera.GetCoords().Leave(sexyCoords));
			SexyCoords3 sexyCoords2 = new SexyCoords3();
			sexyCoords2.RotateRadZ((float)(this.mRotation * (double)GlobalMembers.M(-0.78f)));
			this.mCamera.SetCoords(this.mCamera.GetCoords().Leave(sexyCoords2));
			sexyCoords2.RotateRadZ((float)this.mButtonRotationAdd);
			this.mButtonCamera.SetCoords(this.mButtonCamera.GetCoords().Leave(sexyCoords2));
			SexyVector3 sexyVector = default(SexyVector3);
			new FPoint((float)(GlobalMembers.gApp.mScreenBounds.mX + GlobalMembers.MS(160)) + sexyVector.x * (float)GlobalMembers.gApp.mScreenBounds.mWidth, sexyVector.y * (float)GlobalMembers.gApp.mScreenBounds.mHeight);
			new FPoint((float)(this.mWidth / 2), (float)(this.mHeight / 2));
			SexyVector3 inEyePos = new SexyVector3(GlobalMembers.M(-0.802f), GlobalMembers.M(1.93f), GlobalMembers.M(0.64f));
			this.mButtonCamera.EyeToScreen(inEyePos);
			new FPoint((float)(GlobalMembers.gApp.mScreenBounds.mX + GlobalMembers.MS(160)) + sexyVector.x * (float)GlobalMembers.gApp.mScreenBounds.mWidth, sexyVector.y * (float)GlobalMembers.gApp.mScreenBounds.mHeight);
			new FPoint((float)(GlobalMembers.gApp.mScreenBounds.mX + GlobalMembers.MS(160)) + sexyVector.x * (float)GlobalMembers.gApp.mScreenBounds.mWidth, sexyVector.y * (float)GlobalMembers.gApp.mScreenBounds.mHeight);
			if (this.mScrollWidget != null)
			{
				int num3 = ConstantsWP.MAIN_MENU_TAB_WIDTH * MainMenu.mScrollwidgetPage;
				if (this.mScrollWidget.GetScrollOffset().mX != (float)(-(float)num3))
				{
					this.mScrollWidget.mIsDown = false;
					this.mScrollWidget.ScrollToPoint(new Point(num3, 0), true);
				}
			}
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			if (this.mUpdateCnt == 0)
			{
				this.Update();
			}
			if (this.mLoaded && !MainMenu.preFlight)
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_MAIN_MENU_LOGO, GlobalMembers.gApp.mWidth - 1, GlobalMembers.gApp.mHeight - 1);
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE, GlobalMembers.gApp.mWidth - 1, GlobalMembers.gApp.mHeight - 1);
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_MAIN_MENU_BACKGROUND, GlobalMembers.gApp.mWidth - 1, GlobalMembers.gApp.mHeight - 1);
				MainMenu.preFlight = true;
			}
			float num = (float)Math.Min(1.0, (double)GlobalMembers.M(2.3f) - (double)this.mDispLoadPct * this.mBkgBlackAlpha * (double)GlobalMembers.M(2f));
			float num2 = (float)Math.Max(0.0, Math.Min(1.0, (double)this.mDispLoadPct * this.mBkgBlackAlpha * (double)GlobalMembers.M(3f)));
			num -= num2 * GlobalMembers.M(0.4f);
			Math.Min(1.0, GlobalMembers.M(1.9) - (double)(this.mDispLoadPct * GlobalMembers.M(2.1f)));
			if (this.mLoaded)
			{
				base.DeferOverlay(1);
			}
			bool flag = this.mLoaded;
			if (this.mLogoAlpha != null)
			{
				g.SetColorizeImages(true);
				g.SetColor(this.mLogoAlpha);
				int num3 = this.mWidth / 2 - GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_LOADER_POPCAP.mWidth / 2;
				int num4 = this.mHeight / 2 - GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_LOADER_POPCAP.mHeight / 2;
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_LOADER_POPCAP, num3, num4);
				if (GlobalMembers.gApp.mResourceManager.mCurLocSet == 1145390149U)
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED, num3 + (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED_ID) - GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_LOADER_POPCAP_LOADER_POPCAP_ID)), num4 + (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED_ID) - GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_LOADER_POPCAP_LOADER_POPCAP_ID)));
				}
				else
				{
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_BLACK_TM, num3 + (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_LOADER_POPCAP_BLACK_TM_ID) - GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_LOADER_POPCAP_LOADER_POPCAP_ID)), num4 + (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_LOADER_POPCAP_BLACK_TM_ID) - GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_LOADER_POPCAP_LOADER_POPCAP_ID)));
				}
			}
			if (this.mLoaderAlpha > 0.0)
			{
				this.DrawLoadingBar(g);
			}
			if (GlobalMembers.gApp.mTips.size<string>() != 0 && GlobalMembers.gApp.mTipIdx > 0)
			{
				g.mTransX = 0f;
				g.mTransY = 0f;
				g.SetColor(new Color(255, 255, 255, (int)(255.0 * this.mTipTextAlpha * Math.Max(0.0, Math.Min(1.0, (double)this.mDispLoadPct * GlobalMembers.M(2.0) - GlobalMembers.M(0.15))))));
				g.SetFont(GlobalMembersResources.FONT_DIALOG);
				((ImageFont)g.mFont).PushLayerColor("GLOW", new Color(64, 0, 32, 128));
				((ImageFont)g.mFont).PushLayerColor("OUTLINE", new Color(0, 0, 0, 0));
				g.WriteString(GlobalMembers.gApp.mTips[(GlobalMembers.gApp.mTipIdx - 1) % GlobalMembers.gApp.mTips.size<string>()], this.mWidth / 2, GlobalMembers.MS(1165));
				((ImageFont)g.mFont).PopLayerColor("OUTLINE");
				((ImageFont)g.mFont).PopLayerColor("GLOW");
			}
			if (this.mLoaded)
			{
				this.mHighestVirtFPS = (int)Math.Max((float)this.mHighestVirtFPS, GlobalMembers.gApp.mCurVFPS);
			}
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
			if (this.mLoaded)
			{
				g.SetColorizeImages(true);
				g.SetColor(this.mBJ3LogoAlpha);
				g.SetColor(new Color(255, 255, 255, 255));
				Transform transform = new Transform();
				transform.Scale(1.35f, 1.1f);
				g.DrawImageTransform(GlobalMembersResourcesWP.IMAGE_MAIN_MENU_LOGO, transform, (float)((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_MAIN_MENU_LOGO_ID)) + 290), (float)((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_MAIN_MENU_LOGO_ID)) + 90));
			}
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			base.DrawAll(theFlags, g);
			this.mWidgetManager.FlushDeferredOverlayWidgets(1);
		}

		public override void ButtonMouseEnter(int theId)
		{
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTON_MOUSEOVER);
		}

		public override void ButtonDepress(int theId)
		{
			if (GlobalMembers.gApp.mTooltipManager.mTooltips.size<Tooltip>() > 0)
			{
				GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
			}
			switch (theId)
			{
			case 7:
				this.GoToPage(1);
				return;
			case 8:
				this.GoToPage(2);
				return;
			case 9:
				this.GoToPage(0);
				return;
			default:
				return;
			}
		}

		public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
		}

		public override void SetVisible(bool isVisible)
		{
			base.SetVisible(isVisible);
		}

		public void OnLoaded()
		{
			GlobalMembers.gApp.ExtractResources();
			GlobalMembers.gApp.DoLoadingThreadCompleted();
			this.mLoadingThreadCompleted = true;
			GlobalMembers.gApp.LoadTempleMeshes();
			this.SetupBackground();
		}

		public void LoadingComplete()
		{
			this.mContainer = new MainMenuScrollContainer(this);
			this.mContainer.Resize(0, 0, ConstantsWP.MAIN_MENU_WIDTH, this.mHeight);
			this.mScrollWidget = new ScrollWidget(this.mContainer);
			this.mScrollWidget.Resize(0, 0, this.mWidth, this.mHeight);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_DISABLED);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.AddWidget(this.mContainer);
			this.AddWidget(this.mScrollWidget);
			this.mContainer.mScrollWidget = this.mScrollWidget;
		}

		public bool mIsFullGame()
		{
			return this.mContainer != null && this.mContainer.mIsFullGame;
		}

		public void HideLogo()
		{
			if (!this.mBJ3LogoShowing)
			{
				return;
			}
			this.mBJ3LogoShowing = false;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eMAIN_MENU_BEJ3_LOGO_ALPHA, this.mBJ3LogoAlpha);
		}

		public void ShowLogo()
		{
			if (!this.mLoaded)
			{
				return;
			}
			this.mIntroFinished = true;
			if (this.mBJ3LogoShowing)
			{
				return;
			}
			this.mBJ3LogoShowing = true;
			this.mBJ3LogoAlpha = this.mShowCurve;
		}

		public void DrawLoadingBar(Graphics g)
		{
			Graphics3D graphics3D = g.Get3D();
			SexyVertex2D[] array = new SexyVertex2D[MainMenu.NUM_LOADERBAR_POINTS * 2];
			float num = 0f;
			for (int i = 0; i < MainMenu.NUM_LOADERBAR_POINTS; i++)
			{
				float num2 = 1f / (float)(MainMenu.NUM_LOADERBAR_POINTS - 1) * this.mDispLoadPct;
				if (i == 0 || i == MainMenu.NUM_LOADERBAR_POINTS - 2)
				{
					num2 = 1f / (float)(MainMenu.NUM_LOADERBAR_POINTS - 1);
				}
				num2 *= GlobalMembers.M(1.022f);
				float num3 = -GlobalMembers.M_PI / 2f + num * GlobalMembers.M_PI * 2f;
				num += num2;
				float theU;
				if (i == 0)
				{
					theU = 0f;
				}
				else if (i == MainMenu.NUM_LOADERBAR_POINTS - 1)
				{
					theU = 1f;
				}
				else
				{
					theU = 0.5f;
				}
				for (int j = 0; j < 2; j++)
				{
					float num4 = (float)(GlobalMembers.MS(180) + j * GlobalMembers.MS(60));
					array[i * 2 + j] = new SexyVertex2D(g.mTransX + (float)(this.mWidth / 2) + (float)Math.Cos((double)num3) * num4, (float)(this.mHeight / 2) + (float)Math.Sin((double)num3) * num4, theU, (float)j);
				}
			}
			graphics3D.SetTexture(0, GlobalMembersResourcesWP.IMAGE_LOADER_WHITEDOT);
			float num5 = (float)((0.5 * Math.Sin((double)((float)this.mUpdateCnt * GlobalMembers.M(0.03f))) * 0.5 * (double)GlobalMembers.M(0.5f) + (double)GlobalMembers.M(0.75f)) * this.mLoaderAlpha);
			graphics3D.DrawPrimitive(708U, Graphics3D.EPrimitiveType.PT_TriangleStrip, array, MainMenu.NUM_LOADERBAR_POINTS * 2 - 2, new Color(70, 136, 247, (int)(255f * num5)), 1, 0f, 0f, true, 0U);
		}

		public override void Show()
		{
			if (this.mInterfaceState != InterfaceState.INTERFACE_STATE_LOADING)
			{
				BejeweledLivePlusApp.LoadContent("MainMenu");
			}
			if (this.mContainer != null && this.mInterfaceState != InterfaceState.INTERFACE_STATE_LOADING)
			{
				this.mContainer.Show();
			}
			base.Show();
			this.mBJ3LogoShowing = false;
			this.mAllowFade = true;
			this.mY = 0;
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
		}

		public override void Hide()
		{
			this.HideLogo();
			base.Hide();
			this.SetVisible(false);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_HIDE_CURVE, this.mAlphaCurve);
			this.mTargetPos = this.mFinalY;
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME)
			{
				BejeweledLivePlusApp.UnloadContent("MainMenu");
			}
		}

		public override void InterfaceStateChanged(InterfaceState newState)
		{
			if (this.mContainer != null)
			{
				this.mContainer.InterfaceStateChanged(newState);
			}
			base.InterfaceStateChanged(newState);
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			if (this.mContainer != null)
			{
				this.mContainer.LinkUpAssets();
			}
		}

		public override void PlayMenuMusic()
		{
			if (!GlobalMembers.gApp.mMusicInterface.isPlayingUserMusic())
			{
				if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_LOADING)
				{
					GlobalMembers.gApp.mMusic.PlaySongNoDelay(0, true);
					return;
				}
				GlobalMembers.gApp.mMusic.PlaySongNoDelay(1, true);
			}
		}

		public void WantBuyGame()
		{
			this.mContainer.ButtonDepress(9);
		}

		public MainMenuScrollContainer mContainer;

		private ScrollWidget mScrollWidget;

		private CurvedVal mBJ3LogoAlpha = new CurvedVal();

		private bool mBJ3LogoShowing;

		private bool mIntroFinished;

		private CurvedVal mShowCurve = new CurvedVal();

		public static int mScrollwidgetPage;

		public List<PartnerLogo> mPartnerLogos = new List<PartnerLogo>();

		public float mPartnerBlackAlpha;

		public Graphics3D.PerspectiveCamera mCamera = new Graphics3D.PerspectiveCamera();

		public Graphics3D.PerspectiveCamera mButtonCamera = new Graphics3D.PerspectiveCamera();

		public CurvedVal mRotation = new CurvedVal();

		public CurvedVal mButtonRotationAdd = new CurvedVal();

		public CurvedVal mForeBlackAlpha = new CurvedVal();

		public CurvedVal mBkgBlackAlpha = new CurvedVal();

		public CurvedVal mLogoAlpha = new CurvedVal();

		public CurvedVal mLoaderAlpha = new CurvedVal();

		public CurvedVal mTipTextAlpha = new CurvedVal();

		public int mHighestVirtFPS;

		public Font mUserNameFont;

		public float mDispLoadPct;

		public bool mLoaded;

		public bool mFinishedLoadSequence;

		public bool mSwitchedMusic;

		public bool mHasLoaderResources;

		public bool mLoadingThreadCompleted;

		public bool mDrawDeviceId;

		private static bool preFlight = false;

		private static CurvedVal txtAlpha = new CurvedVal(GlobalMembers.MP("b;0,1,0.01,5,####  ,####K~###      ^~###m####"));

		private static readonly int NUM_LOADERBAR_POINTS = 40;

		private enum MAINMENU_IDS
		{
			BTN_MYSTERY_ID
		}
	}
}
