using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class HelpAndOptionsMenu : Bej3Widget, CheckboxListener, SliderListener
	{
		public HelpAndOptionsMenu()
			: base(Menu_Type.MENU_HELPANDOPTIONSMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.Resize(0, GlobalMembers.gApp.mHeight, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mFinalY = 256;
			this.mContainer = new OptionsContainer();
			this.AddWidget(this.mContainer);
			this.mAboutButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mAboutButton.SetLabel(GlobalMembers._ID("ABOUT", 3419));
			this.mAboutButton.Resize(0, 0, ConstantsWP.OPTIONSMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.OPTIONSMENU_ABOUT_X, ConstantsWP.OPTIONSMENU_ABOUT_Y + 120, this.mAboutButton);
			this.mCreditsButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mCreditsButton.SetLabel(GlobalMembers._ID("CREDITS", 3605));
			this.mCreditsButton.Resize(0, 0, ConstantsWP.OPTIONSMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.OPTIONSMENU_CREDITS_X, ConstantsWP.OPTIONSMENU_CREDITS_Y + 120, this.mCreditsButton);
			this.mLegalButton = new Bej3Button(3, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mLegalButton.SetLabel(GlobalMembers._ID("LEGAL", 3421));
			this.mLegalButton.Resize(0, 0, ConstantsWP.OPTIONSMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.OPTIONSMENU_LEGAL_X, ConstantsWP.OPTIONSMENU_LEGAL_Y + 120, this.mLegalButton);
			this.mBackButton = new Bej3Button(2, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3606));
			this.mBackButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_BACK_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y + 35, this.mBackButton);
			this.AddWidget(this.mBackButton);
			this.mHelpButton = new Bej3Button(4, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mHelpButton.SetLabel(GlobalMembers._ID("HELP", 3337));
			this.mHelpButton.Resize(0, 0, ConstantsWP.OPTIONSMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.OPTIONSMENU_ABOUT_X - (ConstantsWP.OPTIONSMENU_ABOUT_X - ConstantsWP.OPTIONSMENU_LEGAL_X) / 2, ConstantsWP.OPTIONSMENU_ABOUT_Y, this.mHelpButton);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.ButtonDepress(2);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void Draw(Graphics g)
		{
			base.DrawFadedBack(g);
			Bej3Widget.DrawDialogBox(g, this.mWidth);
		}

		public override void Update()
		{
			base.Update();
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
		}

		public override void ButtonDepress(int theId)
		{
			GlobalMembers.gApp.mProfile.WriteProfile();
			GlobalMembers.gApp.WriteToRegistry();
			switch (theId)
			{
			case 0:
				GlobalMembers.gApp.DoCreditsMenu();
				base.Transition_SlideOut();
				return;
			case 1:
				GlobalMembers.gApp.DoAboutMenu();
				base.Transition_SlideOut();
				return;
			case 2:
				GlobalMembers.gApp.DoMainMenu();
				((MainMenuOptions)GlobalMembers.gApp.mMenus[5]).Expand();
				base.Transition_SlideOut();
				return;
			case 3:
				GlobalMembers.gApp.DoLegalMenu();
				base.Transition_SlideOut();
				return;
			case 4:
				GlobalMembers.gApp.DoHelpDialog(0, 3);
				base.Transition_SlideOut();
				return;
			default:
				if (theId != 10001)
				{
					return;
				}
				GlobalMembers.gApp.DoMainMenu();
				((MainMenuOptions)GlobalMembers.gApp.mMenus[5]).Expand();
				base.Transition_SlideOut();
				return;
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mContainer.LinkUpAssets();
		}

		public override void Show()
		{
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_OPTIONSMENU)
			{
				this.mTargetPos = 0;
			}
			else
			{
				this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
			}
			this.mContainer.Show();
			base.Show();
			base.ResetFadedBack(true);
			this.SetVisible(false);
		}

		public override void Hide()
		{
			this.mContainer.Hide();
			base.Hide();
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			this.mContainer.SetDisabled(isDisabled);
		}

		public void CheckboxChecked(int theId, bool isChecked)
		{
			throw new NotImplementedException();
		}

		public void SliderVal(int theId, double theVal)
		{
			throw new NotImplementedException();
		}

		public void SliderReleased(int theId, double theVal)
		{
			throw new NotImplementedException();
		}

		private OptionsContainer mContainer;

		private Bej3Button mCreditsButton;

		private Bej3Button mAboutButton;

		private Bej3Button mLegalButton;

		private Bej3Button mBackButton;

		private Bej3Button mHelpButton;

		private enum OptionsMenu_BUTTON_IDS
		{
			BTN_CREDITS_ID,
			BTN_ABOUT_ID,
			BTN_BACK_ID,
			BTN_LEGAL_ID,
			BTN_HELP_ID
		}
	}
}
