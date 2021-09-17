using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class PauseMenu : Bej3Widget
	{
		protected override void DialogFinished(Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
			if (dialog != null)
			{
				this.mY = dialog.mY;
				this.SetVisible(true);
			}
		}

		public PauseMenu()
			: base(Menu_Type.MENU_PAUSEMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_MENU)
		{
			this.mComingFromHelp = false;
			this.mOptionsContainer = new OptionsContainer();
			this.AddWidget(this.mOptionsContainer);
			this.mRestartButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mRestartButton.SetLabel(GlobalMembers._ID("RESTART", 3422));
			this.AddWidget(this.mRestartButton);
			this.mMainMenuButton = new Bej3Button(2, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.AddWidget(this.mMainMenuButton);
			this.mPlayButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
			this.mPlayButton.SetLabel(GlobalMembers._ID("RESUME", 3609));
			this.AddWidget(this.mPlayButton);
			this.mHelpButton = new Bej3Button(3, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mHelpButton.SetLabel(GlobalMembers._ID("HELP", 3423));
			this.AddWidget(this.mHelpButton);
			this.mFinalY = 106;
			this.SetMode(GameMode.MODE_CLASSIC);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		public void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN && GlobalMembers.gApp.mMenus[19].mState != Bej3WidgetState.STATE_OUT)
				{
					return;
				}
				if (GlobalMembers.gApp.mBoard.mHyperspace != null || GlobalMembers.gApp.mBoard.mWantLevelup || GlobalMembers.gApp.mDialogMap.Count != 0)
				{
					return;
				}
				if (this.mY != 106)
				{
					return;
				}
				args.processed = true;
				this.ButtonDepress(10001);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public void SetMode(GameMode mode)
		{
			this.mMode = mode;
			this.mMainMenuButton.SetLabel(GlobalMembers._ID("MAIN MENU", 3424));
			this.mMainMenuButton.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			Bej3Widget.DisableWidget(this.mMainMenuButton, false);
			Bej3Widget.DisableWidget(this.mPlayButton, false);
			Bej3Widget.DisableWidget(this.mRestartButton, mode == GameMode.MODE_ZEN);
			Bej3Widget.DisableWidget(this.mHelpButton, false);
			this.mMainMenuButton.Resize(0, 0, ConstantsWP.PAUSEMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.PAUSEMENU_BUTTON_MAINMENU_X, ConstantsWP.PAUSEMENU_BUTTON_MAINMENU_Y, this.mMainMenuButton);
			if (mode == GameMode.MODE_ZEN)
			{
				this.mHelpButton.Resize(0, 0, ConstantsWP.PAUSEMENU_BUTTON_WIDTH, 0);
				Bej3Widget.CenterWidgetAt(ConstantsWP.PAUSEMENU_BUTTON_HELP_ZEN_X, ConstantsWP.PAUSEMENU_BUTTON_HELP_ZEN_Y, this.mHelpButton);
			}
			else
			{
				this.mHelpButton.Resize(0, 0, ConstantsWP.PAUSEMENU_BUTTON_WIDTH, 0);
				Bej3Widget.CenterWidgetAt(ConstantsWP.PAUSEMENU_BUTTON_HELP_X, ConstantsWP.PAUSEMENU_BUTTON_HELP_Y, this.mHelpButton);
			}
			this.mPlayButton.Resize(0, 0, ConstantsWP.PAUSEMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.PAUSEMENU_BUTTON_RESUME_X, ConstantsWP.PAUSEMENU_BUTTON_RESUME_Y, this.mPlayButton);
			this.mRestartButton.Resize(0, 0, ConstantsWP.PAUSEMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.PAUSEMENU_BUTTON_RESTART_X, ConstantsWP.PAUSEMENU_BUTTON_RESTART_Y, this.mRestartButton);
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			bool mAllowSlide = this.mAllowSlide;
			if (this.mTargetPos == this.mY && base.IsInOutPosition())
			{
				GlobalMembers.gApp.mIgnoreSound = true;
			}
			base.AllowSlideIn(allow, previousTopButton);
			GlobalMembers.gApp.mIgnoreSound = false;
			if (!mAllowSlide)
			{
				if (allow && this.mTargetPos != 0 && this.mState != Bej3WidgetState.STATE_OUT)
				{
					if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_PAUSEMENU)
					{
						base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
					}
					else
					{
						base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
					}
					if (this.mTopButton != null)
					{
						this.mTopButton.SetDisabled(false);
					}
					if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME)
					{
						this.SetVisible(true);
						return;
					}
				}
			}
			else if (allow && GlobalMembers.gApp.mCurrentGameMode != GameMode.MODE_ZEN)
			{
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
			}
		}

		public override bool SlideForDialog(bool slideOut, Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
			int mId = dialog.mId;
			bool mIgnoreSound = mId != 22;
			GlobalMembers.gApp.mIgnoreSound = mIgnoreSound;
			bool result = base.SlideForDialog(slideOut, dialog, previousButtonType);
			GlobalMembers.gApp.mIgnoreSound = false;
			bool showing = !slideOut;
			if (!slideOut)
			{
				if (mId != 22 && mId != 50)
				{
					this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
					showing = false;
					bool flag = false;
					if (GlobalMembers.gApp.mBoard != null)
					{
						flag = GlobalMembers.gApp.mBoard.mInReplay;
						if (GlobalMembers.gApp.mBoard.mIllegalMoveTutorial && GlobalMembers.gApp.mBoard.mDeferredTutorialVector.Count > 0)
						{
							flag = true;
						}
					}
					if (GlobalMembers.gApp.mBoard == null || !flag)
					{
						base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
					}
					else if (flag)
					{
						base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
					}
				}
				else
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
				}
				if (this.mTopButton != null)
				{
					this.mTopButton.SetDisabled(false);
				}
			}
			base.ResetFadedBack(showing);
			return result;
		}

		public override void Draw(Graphics g)
		{
			this.mAlphaCurve.SetConstant(1.0);
			GlobalMembers.gApp.mWidgetManager.FlushDeferredOverlayWidgets(1);
			base.DrawFadedBack(g);
			g.SetColor(Color.White);
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			base.DeferOverlay(1);
		}

		public override void DrawOverlay(Graphics g)
		{
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME && GlobalMembers.gApp.mBoard != null && GlobalMembers.gApp.mBoard.WantWarningGlow())
			{
				g.PushState();
				g.SetColor(GlobalMembers.gApp.mBoard.GetWarningGlowColor());
				if (GlobalMembers.gApp.mCurrentGameMode != GameMode.MODE_BUTTERFLY)
				{
					g.SetDrawMode(Graphics.DrawMode.Additive);
				}
				g.SetColorizeImages(true);
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DASHBOARD_DM_OVERLAY, 0, (int)GlobalMembers.IMG_SYOFS(706));
				g.PopState();
			}
		}

		public override void Show()
		{
			if (this.mState == Bej3WidgetState.STATE_IN)
			{
				return;
			}
			base.ResetFadedBack(true);
			this.mOptionsContainer.Show();
			base.Show();
			if (this.mInterfaceState != InterfaceState.INTERFACE_STATE_PAUSEMENU)
			{
				this.Collapse(true);
			}
			this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_PAUSEMENU || GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN)
			{
				this.SetVisible(true);
			}
			else
			{
				this.SetVisible(false);
			}
			if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN && !this.mComingFromHelp)
			{
				this.AllowSlideIn(true, null);
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
			}
			this.mAlphaCurve.SetCurve("b+1,0,0.03,1,~###,####         u####");
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public override void Hide()
		{
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME)
			{
				base.ResetFadedBack(false);
			}
			this.mOptionsContainer.Hide();
			base.Hide();
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_GAMEDETAILMENU && this.mTopButton != null)
			{
				this.mTopButton.SetDisabled(true);
			}
		}

		public override void ButtonMouseEnter(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
			GlobalMembers.gApp.mProfile.WriteProfile();
			GlobalMembers.gApp.WriteToRegistry();
			switch (theId)
			{
			case 0:
				this.Collapse();
				return;
			case 1:
			{
				GlobalMembers.gApp.mBoard.RestartGameRequest();
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
				Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.GetDialog(22);
				if (bej3Dialog != null)
				{
					base.Transition_SlideOut();
				}
				this.mAlphaCurve.SetConstant(1.0);
				return;
			}
			case 2:
			{
				GlobalMembers.gApp.mBoard.MainMenuRequest();
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
				Bej3Dialog bej3Dialog2 = (Bej3Dialog)GlobalMembers.gApp.GetDialog(22);
				if (bej3Dialog2 != null)
				{
					base.Transition_SlideOut();
				}
				this.mAlphaCurve.SetConstant(1.0);
				return;
			}
			case 3:
				switch (this.mMode)
				{
				case GameMode.MODE_LIGHTNING:
					GlobalMembers.gApp.DoHelpDialog(10, 1);
					goto IL_1BA;
				case GameMode.MODE_DIAMOND_MINE:
					GlobalMembers.gApp.DoHelpDialog(22, 1);
					goto IL_1BA;
				case GameMode.MODE_BUTTERFLY:
					GlobalMembers.gApp.DoHelpDialog(17, 1);
					goto IL_1BA;
				case GameMode.MODE_ICESTORM:
					GlobalMembers.gApp.DoHelpDialog(20, 1);
					goto IL_1BA;
				}
				GlobalMembers.gApp.DoHelpDialog(0, 1);
				IL_1BA:
				base.Transition_SlideOut();
				break;
			default:
				if (theId != 10001)
				{
					return;
				}
				if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME)
				{
					bool flag = true;
					if (GlobalMembers.gApp.mBoard != null && GlobalMembers.gApp.mBoard.mGameFinished)
					{
						flag = false;
					}
					if (flag)
					{
						this.Expand();
						return;
					}
				}
				else
				{
					if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_PAUSEMENU)
					{
						this.Collapse();
						return;
					}
					if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_GAMEDETAILMENU)
					{
						GlobalMembers.gApp.mMenus[6].ButtonDepress(10001);
						return;
					}
				}
				break;
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mOptionsContainer.LinkUpAssets();
		}

		public override void PlayMenuMusic()
		{
		}

		public void Collapse(bool fadeInstantly)
		{
			this.Collapse(fadeInstantly, false);
		}

		public void Collapse()
		{
			this.Collapse(false, false);
		}

		public void Collapse(bool fadeInstantly, bool fromRestart)
		{
			if (this.mInterfaceState != InterfaceState.INTERFACE_STATE_INGAME)
			{
				GlobalMembers.gApp.GoBackToGame();
			}
			this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
			base.ResetFadedBack(false);
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
			GlobalMembers.gApp.DisableAllExceptThis(this, false);
			if (fromRestart && this.mTopButton != null)
			{
				this.mTopButton.SetDisabled(false);
			}
		}

		public void Expand()
		{
			Bej3Widget.DisableWidget(this.mOptionsContainer, false);
			this.mAlphaCurve.SetConstant(1.0);
			GlobalMembers.gApp.DoPauseMenu();
			this.mTargetPos = this.mFinalY;
			base.ResetFadedBack(true);
			GlobalMembers.gApp.DisableAllExceptThis(this, true);
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
			Bej3Widget.mCurrentSlidingMenu = this;
		}

		private GameMode mMode;

		private Bej3Button mRestartButton;

		private Bej3Button mMainMenuButton;

		private Bej3Button mPlayButton;

		private Bej3Button mHelpButton;

		public OptionsContainer mOptionsContainer;

		public bool mComingFromHelp;

		private enum PAUSEMENU_BUTTON_IDS
		{
			BTN_PLAY_ID,
			BTN_RESTART_ID,
			BTN_MAINMENU_ID,
			BTN_HELP_ID
		}
	}
}
