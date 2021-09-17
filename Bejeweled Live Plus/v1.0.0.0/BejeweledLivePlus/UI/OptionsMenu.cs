using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class OptionsMenu : Bej3Widget, CheckboxListener, SliderListener
	{
		public OptionsMenu()
			: base(Menu_Type.MENU_OPTIONSMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.Resize(0, GlobalMembers.gApp.mHeight, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mFinalY = 255;
			this.mBackButton = new Bej3Button(6, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3606));
			this.mBackButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_BACK_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y, this.mBackButton, true, false);
			this.AddWidget(this.mBackButton);
			this.mCreditsButton = new Bej3Button(5, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mCreditsButton.SetLabel(GlobalMembers._ID("CREDITS", 3605));
			this.mCreditsButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_BACK_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y - 50, this.mCreditsButton);
			this.AddWidget(this.mCreditsButton);
			this.mHelpButton = new Bej3Button(4, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mHelpButton.SetLabel(GlobalMembers._ID("HELP", 3337));
			this.mHelpButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_BACK_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y - 50 - 93, this.mHelpButton);
			this.AddWidget(this.mHelpButton);
			this.mTermsButton = new Bej3Button(3, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mTermsButton.SetLabel(GlobalMembers._ID("TERMS OF SERVICE", 3359));
			this.mTermsButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_TERMS_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y - 50 - 186, this.mTermsButton);
			this.AddWidget(this.mTermsButton);
			this.mPrivacyButton = new Bej3Button(2, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mPrivacyButton.SetLabel(GlobalMembers._ID("PRIVACY POLICY", 3358));
			this.mPrivacyButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_PRIVACY_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y - 50 - 279, this.mPrivacyButton);
			this.AddWidget(this.mPrivacyButton);
			this.mEULAButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mEULAButton.SetLabel(GlobalMembers._ID("END USER LICENSE AGREEMENT", 3357));
			this.mEULAButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_EULA_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y - 50 - 372, this.mEULAButton);
			this.AddWidget(this.mEULAButton);
			this.mAboutButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mAboutButton.SetLabel(GlobalMembers._ID("ABOUT", 3419));
			this.mAboutButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_EULA_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y - 50 - 465, this.mAboutButton);
			this.AddWidget(this.mAboutButton);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.ButtonDepress(10001);
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
				GlobalMembers.gApp.DoAboutMenu();
				base.Transition_SlideOut();
				return;
			case 1:
				GlobalMembers.gApp.OpenURLWithWarning(GlobalMembers._ID("http://tos.ea.com/legalapp/mobileeula/US/en/OTHER", 3581));
				return;
			case 2:
				GlobalMembers.gApp.OpenURLWithWarning(GlobalMembers._ID("http://tos.ea.com/legalapp/WEBPRIVACY/US/en/PC/", 3582));
				return;
			case 3:
				GlobalMembers.gApp.OpenURLWithWarning(GlobalMembers._ID("http://tos.ea.com/legalapp/WEBTERMS/US/en/PC/", 3583));
				return;
			case 4:
				GlobalMembers.gApp.DoHelpDialog(0, 3);
				base.Transition_SlideOut();
				return;
			case 5:
				GlobalMembers.gApp.DoCreditsMenu();
				base.Transition_SlideOut();
				return;
			case 6:
				GlobalMembers.gApp.DoMainMenu();
				base.Transition_SlideOut();
				return;
			default:
				if (theId != 10001)
				{
					return;
				}
				GlobalMembers.gApp.DoMainMenu();
				base.Transition_SlideOut();
				return;
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
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
			base.Show();
			base.ResetFadedBack(true);
			this.SetVisible(false);
		}

		public override void Hide()
		{
			base.Hide();
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
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

		private Bej3Button mEULAButton;

		private Bej3Button mPrivacyButton;

		private Bej3Button mTermsButton;

		private Bej3Button mCreditsButton;

		private Bej3Button mAboutButton;

		private Bej3Button mBackButton;

		private Bej3Button mHelpButton;

		private enum OptionsMenu_BUTTON_IDS
		{
			BTN_ABOUT_ID,
			BTN_EULA_ID,
			BTN_PP_ID,
			BTN_TOS_ID,
			BTN_HELP_ID,
			BTN_CREDITS_ID,
			BTN_BACK_ID
		}
	}
}
