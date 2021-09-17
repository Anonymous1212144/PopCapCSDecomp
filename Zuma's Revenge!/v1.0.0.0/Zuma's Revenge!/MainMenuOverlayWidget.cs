using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class MainMenuOverlayWidget : Widget, ButtonListener
	{
		public void ButtonPress(int theId)
		{
			if (theId == 11 || theId == 6 || theId == 14)
			{
				GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_BUTTON1));
			}
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public void ButtonDepress(int theId)
		{
			GameApp mApp = this.mMenu.mApp;
			if (this.mMenu.mFirstTimeAlpha > 0 || this.mMenu.mIFUnlockAnim != null || mApp.mGenericHelp != null || this.mMenu.mDelayedIFStartState > 0 || this.mMenu.ShowingTikiTemple() || mApp.mMapScreen != null)
			{
				return;
			}
			if (mApp.mBambooTransition != null && mApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			if (mApp.GetDialog(2) != null)
			{
				return;
			}
			if (theId == 11)
			{
				GameApp.gApp.ShowLegal();
				return;
			}
			if (theId == 6)
			{
				mApp.DoOptionsDialog(false);
				return;
			}
			if (theId == 14 && GameApp.USE_TRIAL_VERSION)
			{
				this.ProcessLocked();
			}
		}

		public void ProcessLocked()
		{
			this.mMenu.mState = MainMenu_State.State_QuitPrompt;
			string @string = TextManager.getInstance().getString(834);
			int width_pad = Common._DS(Common._M(20));
			GameApp.gApp.DoYesNoDialog(TextManager.getInstance().getString(835), @string, true, TextManager.getInstance().getString(446), TextManager.getInstance().getString(447), false, Common._S(Common._M(50)), 1, width_pad);
			GameApp.gApp.mYesNoDialogDelegate = new GameApp.YesNoDialogDelegate(this.ProcessUnlock);
		}

		public void ProcessUnlock(int theId)
		{
			if (theId == 1000 && GameApp.USE_TRIAL_VERSION)
			{
				GameApp.gApp.ToMarketPlace();
			}
			this.mMenu.mState = MainMenu_State.State_MainMenu;
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public MainMenuOverlayWidget(MainMenu theMainMenu)
		{
			this.mMenu = theMainMenu;
			this.mMenuMoreGamesStartY = -1;
			this.mMenuMoreGamesOriginY = -1;
			this.mMenuMoreGamesDestY = -1;
			this.mMenuMoreGamesSignStartY = -1;
			this.mMenuMoreGamesSignOriginY = -1;
			this.mMenuMoreGamesSignDestY = -1;
			this.mMenuMoreGamesSignY = -1;
			this.mMenuOptionsStartX = -1;
			this.mMenuOptionsDestX = -1;
			this.mMenuOptionsOriginX = -1;
			this.mHasTransparencies = true;
			this.mWidgetFlagsMod.mRemoveFlags |= 49;
			GameApp mApp = this.mMenu.mApp;
			this.Resize(mApp.GetScreenRect().mX, mApp.GetScreenRect().mY, mApp.GetScreenRect().mWidth - mApp.GetScreenRect().mX, mApp.GetScreenRect().mHeight - mApp.GetScreenRect().mY);
		}

		public void Init()
		{
			GameApp mApp = this.mMenu.mApp;
			this.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN);
			this.IMAGE_UI_MAINMENU_OPTIONS_DOWN = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_OPTIONS_DOWN);
			this.IMAGE_UI_MAINMENU_MORE_GAMES = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES);
			this.IMAGE_UI_MAINMENU_MORE_GAMES_DOWN = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_DOWN);
			this.IMAGE_UI_MAINMENU_OPTIONS = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_OPTIONS);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW);
			this.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER);
			this.IMAGE_UI_MAINMENU_RIBBIT = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_RIBBIT);
			this.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE);
			this.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE);
			this.IMAGE_UI_MAINMENU_TIKIHEAD = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_TIKIHEAD);
			this.IMAGE_UI_MAINMENU_UNLOCK = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_UNLOCK);
			this.IMAGE_UI_MAINMENU_UNLOCK_ON = Res.GetImageByID(ResID.IMAGE_UI_MAINMENU_UNLOCK_ON);
			this.AddOptionsButton();
			this.AddMoreGamesButton();
			this.mMenuMoreGamesSignY = Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN));
			this.mMenuMoreGamesSignStartY = this.mMenuMoreGamesSignY;
			this.mMenuMoreGamesSignOriginY = this.mMenuMoreGamesSignY;
		}

		public void DoMoreGamesSlide(bool isSlidingIn)
		{
			this.mMenuMoreGamesSignStartY = this.mMenuMoreGamesSignY;
			this.mMenuMoreGamesStartY = this.mMenu.mMoreGamesButton.mY;
			this.mMenuOptionsStartX = this.mMenu.mOptionsButton.mX;
			if (isSlidingIn)
			{
				this.mMenuMoreGamesDestY = this.mMenuMoreGamesOriginY;
				this.mMenuOptionsDestX = this.mMenuOptionsOriginX;
				this.mMenuMoreGamesSignDestY = this.mMenuMoreGamesSignOriginY;
				return;
			}
			this.mMenuMoreGamesDestY = (this.mMenuMoreGamesSignDestY = this.mMenu.mApp.mScreenBounds.mHeight + Common._S(150));
			this.mMenuOptionsDestX = -Common._S(300);
		}

		public void AddMoreGamesButton()
		{
			GameApp mApp = this.mMenu.mApp;
			int width = this.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN.GetWidth();
			int height = this.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN.GetHeight();
			float num = (float)width * 0.64f;
			float num2 = (float)height * 0.68f;
			float num3 = (float)Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN)) + (float)width * 0.18f;
			float num4 = (float)Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN)) + (float)height * 0.16f;
			this.mMenuMoreGamesStartY = (int)num4;
			this.mMenuMoreGamesOriginY = (int)num4;
			this.mMenu.mMoreGamesButton = new ButtonWidget(11, this);
			this.mMenu.mMoreGamesButton.mButtonImage = this.IMAGE_UI_MAINMENU_MORE_GAMES;
			this.mMenu.mMoreGamesButton.mOverImage = this.IMAGE_UI_MAINMENU_MORE_GAMES;
			this.mMenu.mMoreGamesButton.mDownImage = this.IMAGE_UI_MAINMENU_MORE_GAMES_DOWN;
			this.mMenu.mMoreGamesButton.mBtnNoDraw = true;
			this.mMenu.mMoreGamesButton.mDoFinger = true;
			this.mMenu.mMoreGamesButton.Resize(mApp.GetWideScreenAdjusted((int)num3), (int)num4, (int)num, (int)num2);
			this.mMenu.AddWidget(this.mMenu.mMoreGamesButton);
		}

		public void AddOptionsButton()
		{
			GameApp mApp = this.mMenu.mApp;
			this.mMenu.mOptionsButton = new ButtonWidget(6, this);
			this.mMenu.mOptionsButton.mButtonImage = this.IMAGE_UI_MAINMENU_OPTIONS;
			this.mMenu.mOptionsButton.mOverImage = this.IMAGE_UI_MAINMENU_OPTIONS;
			this.mMenu.mOptionsButton.mDownImage = this.IMAGE_UI_MAINMENU_OPTIONS_DOWN;
			this.mMenu.AddWidget(this.mMenu.mOptionsButton);
			this.mMenu.mOptionsButton.mBtnNoDraw = true;
			this.mMenu.mOptionsButton.mDoFinger = true;
			this.mMenuOptionsOriginX = Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_OPTIONS_DOWN));
			this.mMenu.mOptionsButton.Resize(this.mMenuOptionsOriginX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_OPTIONS_DOWN)), this.IMAGE_UI_MAINMENU_OPTIONS_DOWN.GetWidth(), this.IMAGE_UI_MAINMENU_OPTIONS_DOWN.GetHeight());
			this.mMenu.mUnlockButton = new ButtonWidget(14, this);
			this.mMenu.mUnlockButton.mButtonImage = this.IMAGE_UI_MAINMENU_OPTIONS;
			this.mMenu.mUnlockButton.mOverImage = this.IMAGE_UI_MAINMENU_OPTIONS;
			this.mMenu.mUnlockButton.mDownImage = this.IMAGE_UI_MAINMENU_OPTIONS_DOWN;
			this.mMenu.AddWidget(this.mMenu.mUnlockButton);
			this.mMenu.mUnlockButton.mBtnNoDraw = true;
			this.mMenu.mUnlockButton.mDoFinger = true;
			this.mMenu.mUnlockButton.Resize(325, 230, this.IMAGE_UI_MAINMENU_UNLOCK.GetWidth(), this.IMAGE_UI_MAINMENU_UNLOCK.GetHeight());
			if (GameApp.USE_TRIAL_VERSION)
			{
				this.mMenu.mUnlockButton.SetVisible(true);
				return;
			}
			this.mMenu.mUnlockButton.SetVisible(false);
			this.mMenu.mUnlockButton.SetDisabled(true);
		}

		public void UpdateOverlaySlide(float theSlidePct)
		{
			float num = theSlidePct * (float)(this.mMenuMoreGamesDestY - this.mMenuMoreGamesStartY);
			this.mMenu.mMoreGamesButton.Move(this.mMenu.mMoreGamesButton.mX, (int)((float)this.mMenuMoreGamesStartY + num));
			float num2 = theSlidePct * (float)(this.mMenuMoreGamesSignDestY - this.mMenuMoreGamesSignStartY);
			this.mMenuMoreGamesSignY = (int)((float)this.mMenuMoreGamesSignStartY + num2);
			float num3 = theSlidePct * (float)(this.mMenuOptionsDestX - this.mMenuOptionsStartX);
			this.mMenu.mOptionsButton.Move((int)((float)this.mMenuOptionsStartX + num3), this.mMenu.mOptionsButton.mY);
		}

		public void DrawOptionsButton(Graphics g)
		{
			Image image = this.IMAGE_UI_MAINMENU_OPTIONS;
			int width = image.GetWidth();
			int height = image.GetHeight();
			if (this.mMenu.mOptionsButton.mIsDown)
			{
				image = this.IMAGE_UI_MAINMENU_OPTIONS_DOWN;
				width = image.GetWidth();
				height = image.GetHeight();
			}
			int theX = (int)((float)this.mMenu.mOptionsButton.mX + (float)(this.mMenu.mOptionsButton.mWidth - width) / 2f);
			int theY = (int)((float)this.mMenu.mOptionsButton.mY + (float)(this.mMenu.mOptionsButton.mHeight - height) / 2f);
			if (Localization.GetCurrentLanguage() == Localization.LanguageType.Language_GR || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_RU || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_SP || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_SPC || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_IT)
			{
				g.DrawImage(image, theX, theY, (int)((float)image.mWidth * 0.9f), (int)((float)image.mHeight * 0.9f));
			}
			else
			{
				g.DrawImage(image, theX, theY);
			}
			if (GameApp.USE_TRIAL_VERSION)
			{
				image = this.IMAGE_UI_MAINMENU_UNLOCK;
				width = image.GetWidth();
				height = image.GetHeight();
				if (this.mMenu.mUnlockButton.mIsDown)
				{
					image = this.IMAGE_UI_MAINMENU_UNLOCK_ON;
					width = image.GetWidth();
					height = image.GetHeight();
				}
				theX = this.mMenu.mUnlockButton.mX + 35;
				theY = this.mMenu.mUnlockButton.mY;
				g.DrawImage(image, theX, theY);
			}
		}

		public void DrawMoreGamesButton(Graphics g)
		{
			Image image = this.IMAGE_UI_MAINMENU_MORE_GAMES;
			int num = image.GetWidth();
			int height = image.GetHeight();
			if (this.mMenu.mMoreGamesButton.mIsDown)
			{
				image = this.IMAGE_UI_MAINMENU_MORE_GAMES_DOWN;
				num = image.GetWidth() + Common._DS(20);
				height = image.GetHeight();
			}
			else
			{
				image = this.IMAGE_UI_MAINMENU_MORE_GAMES;
				num = image.GetWidth();
				height = image.GetHeight();
			}
			float num2 = (float)this.mMenu.mMoreGamesButton.mX + (float)(this.mMenu.mMoreGamesButton.mWidth - num) * 0.5f;
			float num3 = (float)this.mMenu.mMoreGamesButton.mY + (float)(this.mMenu.mMoreGamesButton.mHeight - height) * 0.5f;
			g.DrawImage(image, (int)num2, (int)num3);
		}

		public override void Draw(Graphics g)
		{
			GameApp mApp = this.mMenu.mApp;
			if (mApp.mCredits != null && MathUtils._geq(mApp.mCredits.mAlpha, 255f))
			{
				return;
			}
			if (this.mMenu.mChallengeMenu != null)
			{
				return;
			}
			if (mApp.mMapScreen != null)
			{
				return;
			}
			if (this.mMenu.mTikiTemple != null)
			{
				return;
			}
			g.DrawImage(this.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW, 0, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER)) + (this.mMenu.mMainMenuButtonsScrollWidget.mY - this.mMenu.mMenuScrollOriginY));
			g.DrawImageMirror(this.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW, mApp.GetScreenRect().mWidth - mApp.GetScreenRect().mX - this.IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW.GetWidth(), Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_SCROLLMENU_BORDER)) + (this.mMenu.mMainMenuButtonsScrollWidget.mY - this.mMenu.mMenuScrollOriginY));
			g.DrawImage(this.IMAGE_UI_MAINMENU_RIBBIT, this.mMenu.mMenuFrogX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_RIBBIT)));
			g.DrawImage(this.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE)) - mApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE)));
			g.DrawImage(this.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE)) - mApp.mWideScreenXOffset, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE)));
			g.DrawImage(this.IMAGE_UI_MAINMENU_TIKIHEAD, this.mMenu.mMenuTikiX, Common._DS(Res.GetOffsetYByID(ResID.IMAGE_UI_MAINMENU_TIKIHEAD)));
			g.DrawImage(this.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN, Common._DS(Res.GetOffsetXByID(ResID.IMAGE_UI_MAINMENU_MORE_GAMES_SIGN)) - mApp.mWideScreenXOffset + mApp.GetScreenRect().mX, this.mMenuMoreGamesSignY);
			this.DrawOptionsButton(g);
			this.DrawMoreGamesButton(g);
			if (this.mMenu.mFirstTimeAlpha > 0)
			{
				g.SetColor(new Color(0, 0, 0, this.mMenu.mFirstTimeAlpha));
				g.FillRect(Common._S(-80), 0, this.mWidth + Common._S(160), this.mHeight);
			}
		}

		private MainMenu mMenu;

		private int mMenuMoreGamesStartY;

		private int mMenuMoreGamesOriginY;

		private int mMenuMoreGamesDestY;

		private int mMenuMoreGamesSignStartY;

		private int mMenuMoreGamesSignOriginY;

		private int mMenuMoreGamesSignDestY;

		private int mMenuMoreGamesSignY;

		private int mMenuOptionsStartX;

		private int mMenuOptionsDestX;

		private int mMenuOptionsOriginX;

		private Image IMAGE_UI_MAINMENU_MORE_GAMES_SIGN;

		private Image IMAGE_UI_MAINMENU_OPTIONS_DOWN;

		private Image IMAGE_UI_MAINMENU_MORE_GAMES;

		private Image IMAGE_UI_MAINMENU_MORE_GAMES_DOWN;

		private Image IMAGE_UI_MAINMENU_OPTIONS;

		private Image IMAGE_UI_MAINMENU_SCROLLMENU_SHADOW;

		private Image IMAGE_UI_MAINMENU_SCROLLMENU_BORDER;

		private Image IMAGE_UI_MAINMENU_RIBBIT;

		private Image IMAGE_UI_MAINMENU_BOTRIGHT_FOLIAGE;

		private Image IMAGE_UI_MAINMENU_BOTLEFT_FOLIAGE;

		private Image IMAGE_UI_MAINMENU_TIKIHEAD;

		private Image IMAGE_UI_MAINMENU_UNLOCK;

		private Image IMAGE_UI_MAINMENU_UNLOCK_ON;
	}
}
