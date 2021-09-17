using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	internal class MainMenuOptions : ProfileMenuBase
	{
		public MainMenuOptions()
			: base(Menu_Type.MENU_MAINMENUOPTIONSMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_MENU)
		{
			this.mExpandOnShow = false;
			this.mCanSlideIn = false;
			this.mFirstShow = false;
			this.didFadeIn = false;
			int num = 15;
			this.mPlayerHeight = 460;
			int num2 = 450;
			int num3 = 220;
			int num4 = -130;
			float num5 = 1.3f;
			int num6 = 84;
			this.mPlayerNameLabel = new Label(GlobalMembersResources.FONT_DIALOG);
			this.mPlayerNameLabel.Resize(ConstantsWP.PROFILEMENU_NAME_LABEL_X, ConstantsWP.PROFILEMENU_NAME_LABEL_Y + num2 - num6 - num, 0, 0);
			this.AddWidget(this.mPlayerNameLabel);
			this.mRankBarWidget = new RankBarWidget(ConstantsWP.PROFILEMENU_RANKBAR_WIDTH);
			this.mRankBarWidget.mDrawRankName = false;
			this.mRankBarWidget.mDrawCrown = false;
			this.mRankBarWidget.Resize(ConstantsWP.PROFILEMENU_RANKBAR_X + num4, ConstantsWP.PROFILEMENU_RANKBAR_Y + num3, (int)((float)ConstantsWP.PROFILEMENU_RANKBAR_WIDTH * num5), 0);
			this.AddWidget(this.mRankBarWidget);
			this.Resize(0, GlobalMembers.gApp.mHeight, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mFinalY = ConstantsWP.MENU_Y_POS_HIDDEN;
			this.mPlayerImage = new ImageWidget(712, true);
			this.mPlayerImage.Resize(ConstantsWP.MAINMENU_OPTIONSMENU_PROFILE_X, ConstantsWP.MAINMENU_OPTIONSMENU_PROFILE_Y - num + 10, (int)((float)ConstantsWP.LARGE_PROFILE_PICTURE_SIZE * 2.3f), (int)((double)ConstantsWP.LARGE_PROFILE_PICTURE_SIZE * 2.3));
			this.mPlayerImage.mScale = 2.3f;
			this.AddWidget(this.mPlayerImage);
			this.mProfileButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mProfileButton.SetLabel(GlobalMembers._ID("STATS", 3296));
			this.mProfileButton.Resize(0, 0, ConstantsWP.MAINMENU_OPTIONSMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAINMENU_OPTIONSMENU_PROFILE_X, ConstantsWP.MAINMENU_OPTIONSMENU_PROFILE_Y + this.mPlayerHeight + num6, this.mProfileButton);
			this.AddWidget(this.mProfileButton);
			this.mOptionsButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mOptionsButton.mFont = GlobalMembersResources.FONT_SUBHEADER;
			this.mOptionsButton.SetLabel(GlobalMembers._ID("OPTIONS", 3403));
			this.mOptionsButton.Resize(0, 0, ConstantsWP.MAINMENU_OPTIONSMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.MAINMENU_OPTIONSMENU_OPTIONS_X, ConstantsWP.MAINMENU_OPTIONSMENU_OPTIONS_Y + this.mPlayerHeight + num6, this.mOptionsButton);
			this.AddWidget(this.mOptionsButton);
			this.mBackButton = new Bej3Button(4, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3405));
			this.mBackButton.Resize(0, 0, ConstantsWP.MAINMENU_OPTIONSMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_BACK_X, ConstantsWP.MAINMENU_OPTIONSMENU_HELP_Y + this.mPlayerHeight + num6, this.mBackButton);
			this.AddWidget(this.mBackButton);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		public virtual void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back)
			{
				if (this.mY == ConstantsWP.MENU_Y_POS_HIDDEN)
				{
					if (GlobalMembers.gApp.mInterfaceState == InterfaceState.INTERFACE_STATE_MAINMENU && GlobalMembers.gApp.mMenus[6].mVisible)
					{
						return;
					}
					if (GlobalMembers.gApp.mMainMenu.mContainer.CurrentPage != 0)
					{
						return;
					}
					GlobalMembers.gApp.mMainMenu.QuitGameRequest();
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
					Bej3Dialog bej3Dialog = (Bej3Dialog)GlobalMembers.gApp.GetDialog(33);
					bej3Dialog.mDialogListener = GlobalMembers.gApp;
					if (bej3Dialog != null)
					{
						base.Transition_SlideOut();
						return;
					}
				}
				else
				{
					args.processed = true;
					this.ButtonDepress(10001);
				}
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			base.DrawFadedBack(g);
			g.SetColorizeImages(false);
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.PROFILEMENU_DIVIDER_NAME - 80);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.PROFILEMENU_DIVIDER_RANK + 200);
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			base.DrawAll(theFlags, g);
		}

		public override void UpdateAll(ModalFlags theFlags)
		{
			base.UpdateAll(theFlags);
		}

		public override void Update()
		{
			base.SetTopButtonType((this.mTargetPos < 800) ? Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS : Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
			base.Update();
			if (!this.didFadeIn && this.mState == Bej3WidgetState.STATE_FADING_IN && this.mCanSlideIn)
			{
				this.didFadeIn = true;
				Bej3Widget.mCurrentSlidingMenu = this;
				this.AllowSlideIn(true, null);
				this.mCanSlideIn = false;
				if (!this.mFirstShow)
				{
					this.mY = GlobalMembers.gApp.mHeight;
				}
			}
			if (this.mFirstShow && this.mState == Bej3WidgetState.STATE_IN && this.mY == this.mTargetPos)
			{
				IntroDialog theDialog = new IntroDialog();
				GlobalMembers.gApp.AddDialog(theDialog);
				base.Transition_SlideOut();
				this.mFirstShow = false;
			}
			if (this.mState == Bej3WidgetState.STATE_FADING_IN)
			{
				GlobalMembers.gApp.DisableAllExceptThis(this, true);
			}
		}

		public override void ButtonDepress(int theId)
		{
			GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
			switch (theId)
			{
			case 0:
				GlobalMembers.gApp.DoStatsMenu();
				base.Transition_SlideOut();
				return;
			case 1:
				GlobalMembers.gApp.DoHelpAndOptionsMenu();
				base.Transition_SlideOut();
				return;
			case 2:
				GlobalMembers.gApp.DoEditProfileMenu();
				base.Transition_SlideOut();
				return;
			case 3:
				GlobalMembers.gApp.DoHelpDialog(0, 2);
				base.Transition_SlideOut();
				return;
			case 4:
				this.Collapse();
				return;
			default:
				if (theId != 10001)
				{
					return;
				}
				if (this.mTargetPos == ConstantsWP.MENU_Y_POS_HIDDEN)
				{
					this.Expand();
					return;
				}
				this.Collapse();
				return;
			}
		}

		public override void Show()
		{
			if (GlobalMembers.gApp.mProfile.UsesPresetProfilePicture())
			{
				this.mPlayerImage.SetImage(712 + GlobalMembers.gApp.mProfile.GetProfilePictureId());
			}
			Graphics graphics = new Graphics();
			graphics.SetFont(this.mPlayerNameLabel.GetFont());
			string theString = GlobalMembers._ID("Player", 446);
			this.mPlayerNameLabel.SetText(Utils.GetEllipsisString(graphics, theString, ConstantsWP.PROFILEMENU_NAME_LABEL_WIDTH));
			int mY = this.mY;
			base.Show();
			this.mY = mY;
			base.ResetFadedBack(false);
			if (this.mExpandOnShow)
			{
				this.Expand();
			}
			else
			{
				this.Collapse(true);
				if (this.mY != GlobalMembers.gApp.mHeight)
				{
					this.mY = this.mTargetPos;
				}
			}
			this.SetVisible(false);
			if (this.didFadeIn)
			{
				base.Transition_FadeIn();
			}
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
			bool disableOthers = this.mTargetPos == ConstantsWP.MAINMENU_OPTIONSMENU_EXPANDED_POS;
			GlobalMembers.gApp.DisableAllExceptThis(this, disableOthers);
		}

		public override void Hide()
		{
			base.Hide();
			this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_IDLE;
			if (this.mAlphaCurve != 0.0)
			{
				GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_HIDE_CURVE, this.mAlphaCurve);
			}
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
		}

		public void Expand()
		{
			this.mTargetPos = ConstantsWP.MAINMENU_OPTIONSMENU_EXPANDED_POS - this.mPlayerHeight;
			base.ResetFadedBack(true);
			GlobalMembers.gApp.DisableAllExceptThis(this, true);
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
		}

		public void Collapse()
		{
			this.Collapse(false);
		}

		public void Collapse(bool fadeInstantly)
		{
			this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
			base.ResetFadedBack(false);
			GlobalMembers.gApp.DisableAllExceptThis(this, false);
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			bool mAllowSlide = this.mAllowSlide;
			base.AllowSlideIn(allow, previousTopButton);
			if (allow && this.mY == this.mTargetPos && (this.mState == Bej3WidgetState.STATE_FADING_IN || this.mState == Bej3WidgetState.STATE_IN))
			{
				GlobalMembers.gApp.DoRateGameDialog();
			}
			if (!mAllowSlide && previousTopButton != null)
			{
				if (this.mExpandOnShow)
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
				}
				else
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
				}
			}
			if (allow && (this.mState == Bej3WidgetState.STATE_IN || this.mState == Bej3WidgetState.STATE_FADING_IN) && this.mTopButton != null)
			{
				this.mTopButton.SetDisabled(false);
			}
		}

		public override void SetVisible(bool isVisible)
		{
			base.SetVisible(isVisible);
		}

		public override bool SlideForDialog(bool slideOut, Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
			GlobalMembers.gApp.mIgnoreSound = true;
			bool result = base.SlideForDialog(slideOut, dialog, previousButtonType);
			GlobalMembers.gApp.mIgnoreSound = false;
			if (dialog.mId == 56)
			{
				this.Expand();
			}
			return result;
		}

		public override void KeyChar(char theChar)
		{
			switch (theChar)
			{
			case '(':
				ConstantsWP.TOP_BUTTON_ANIMATION_SPEED -= 0.1f;
				return;
			case ')':
				ConstantsWP.TOP_BUTTON_ANIMATION_SPEED += 0.1f;
				break;
			case '*':
			case ',':
				break;
			case '+':
				ConstantsWP.DASHBOARD_SLIDER_SPEED += 0.1f;
				return;
			case '-':
				ConstantsWP.DASHBOARD_SLIDER_SPEED -= 0.1f;
				return;
			default:
				switch (theChar)
				{
				case '[':
					ConstantsWP.DASHBOARD_SLIDER_SPEED_SCALAR -= 0.001f;
					return;
				case '\\':
					break;
				case ']':
					ConstantsWP.DASHBOARD_SLIDER_SPEED_SCALAR += 0.001f;
					return;
				default:
					return;
				}
				break;
			}
		}

		private int mPlayerHeight;

		private Bej3Button mProfileButton;

		private Bej3Button mOptionsButton;

		private Bej3Button mHelpButton;

		private Bej3Button mBackButton;

		private Label mPlayerNameLabel;

		private RankBarWidget mRankBarWidget;

		public bool mExpandOnShow;

		public bool mCanSlideIn;

		public bool mFirstShow;

		public bool didFadeIn;

		private enum MainMenuOptions_BUTTON_IDS
		{
			BTN_PROFILE_ID,
			BTN_OPTIONS_ID,
			BTN_MOREGAMES_ID,
			BTN_HELP_ID,
			BTN_BACK_ID
		}
	}
}
