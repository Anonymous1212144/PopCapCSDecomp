using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class LegalMenu : Bej3Widget, CheckboxListener
	{
		public LegalMenu()
			: base(Menu_Type.MENU_LEGALMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.Resize(ConstantsWP.LEGALMENU_HEADING_LABEL_X, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetText(GlobalMembers._ID("LEGAL", 3353));
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.AddWidget(this.mHeadingLabel);
			this.mAnalyticsLabelHeading = new Label(GlobalMembersResources.FONT_SUBHEADER, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mAnalyticsLabelHeading.Resize(ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_HEADING_X, ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_HEADING_Y, 0, 0);
			this.mAnalyticsLabelHeading.SetText(GlobalMembers._ID("USER SHARING", 3354));
			this.AddWidget(this.mAnalyticsLabelHeading);
			this.mAnalyticsLabel = new Label(GlobalMembersResources.FONT_DIALOG);
			this.mAnalyticsLabel.Resize(ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_X, ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_Y, 0, 0);
			this.mAnalyticsLabel.SetTextBlock(new Rect(ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_X, ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_Y, ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_WIDTH, ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_HEIGHT), true);
			this.mAnalyticsLabel.SetTextBlockEnabled(true);
			this.mAnalyticsLabel.SetTextBlockAlignment(-1);
			this.mAnalyticsLabel.mLineSpacingOffset = ConstantsWP.LEGALMENU_ANONYMOUS_STATS_LABEL_LINESPACING_OFFSET;
			this.mAnalyticsLabel.SetClippingEnabled(false);
			this.mAnalyticsLabel.SetText(GlobalMembers._ID("If you no longer wish to send anonymous game play information to PopCap, uncheck the box and we will disable the transmission of such information.", 3355));
			this.AddWidget(this.mAnalyticsLabel);
			this.mAnalyticsCheckbox = new Bej3Checkbox(4, this);
			this.mAnalyticsCheckbox.Resize(ConstantsWP.LEGALMENU_ANONYMOUS_STATS_CHECKBOX_X, ConstantsWP.LEGALMENU_ANONYMOUS_STATS_CHECKBOX_Y, 0, 0);
			this.mAnalyticsCheckbox.mGrayOutWhenDisabled = false;
			this.AddWidget(this.mAnalyticsCheckbox);
			this.mBackButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3356));
			this.mBackButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_BACK_X, ConstantsWP.LEGALMENU_BUTTON_BACK_Y, this.mBackButton, true, false);
			this.AddWidget(this.mBackButton);
			this.mEULAButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mEULAButton.SetLabel(GlobalMembers._ID("END USER LICENSE AGREEMENT", 3357));
			this.mEULAButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_EULA_X, ConstantsWP.LEGALMENU_BUTTON_EULA_Y, this.mEULAButton);
			this.AddWidget(this.mEULAButton);
			this.mPrivacyButton = new Bej3Button(2, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mPrivacyButton.SetLabel(GlobalMembers._ID("PRIVACY POLICY", 3358));
			this.mPrivacyButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_PRIVACY_X, ConstantsWP.LEGALMENU_BUTTON_PRIVACY_Y, this.mPrivacyButton);
			this.AddWidget(this.mPrivacyButton);
			this.mTermsButton = new Bej3Button(3, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mTermsButton.SetLabel(GlobalMembers._ID("TERMS OF SERVICE", 3359));
			this.mTermsButton.Resize(0, 0, ConstantsWP.LEGALMENU_BUTTON_WIDTH, 0);
			Bej3Widget.CenterWidgetAt(ConstantsWP.LEGALMENU_BUTTON_TERMS_X, ConstantsWP.LEGALMENU_BUTTON_TERMS_Y, this.mTermsButton);
			this.AddWidget(this.mTermsButton);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.ButtonDepress(0);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.MENU_DIVIDER_Y);
			Bej3Widget.DrawLightBox(g, new Rect(ConstantsWP.LEGALMENU_BOX_1_X, ConstantsWP.LEGALMENU_BOX_1_Y, ConstantsWP.LEGALMENU_BOX_1_W, ConstantsWP.LEGALMENU_BOX_1_H));
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mAnalyticsCheckbox.SetChecked(GlobalMembers.gApp.mProfile.mAllowAnalytics, false);
			this.mAnalyticsLabelHeading.SetText(GlobalMembers._ID("USER SHARING", 3360));
			if (GlobalMembers.gApp.mProfile.mAllowAnalytics)
			{
				this.mAnalyticsLabel.SetText(GlobalMembers._ID("If you no longer wish to send anonymous game play information to PopCap, uncheck the box and we will disable the transmission of such information.", 3361));
				return;
			}
			this.mAnalyticsLabel.SetText(GlobalMembers._ID("If you wish to send anonymous game play information to PopCap, check the box and we will enable the transmission of such information.", 3362));
		}

		public override void Show()
		{
			base.Show();
			this.mTargetPos = 106;
			this.SetVisible(false);
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 0:
				GlobalMembers.gApp.DoOptionsMenu();
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

		public virtual void CheckboxChecked(int theId, bool isChecked)
		{
			if (theId != 4)
			{
				return;
			}
			if (isChecked != GlobalMembers.gApp.mProfile.mAllowAnalytics)
			{
				GlobalMembers.gApp.mProfile.mAllowAnalytics = isChecked;
				GlobalMembers.gApp.mProfile.WriteProfile();
				this.LinkUpAssets();
			}
		}

		private Label mHeadingLabel;

		private Bej3Button mBackButton;

		private Label mAnalyticsLabelHeading;

		private Label mAnalyticsLabel;

		private Bej3Checkbox mAnalyticsCheckbox;

		private Bej3Button mEULAButton;

		private Bej3Button mPrivacyButton;

		private Bej3Button mTermsButton;

		private enum LegalMenu_IDS
		{
			BTN_BACK_ID,
			BTN_EULA_ID,
			BTN_PRIVACY_ID,
			BTN_TERMS_ID,
			CHK_ANALYTICS_ID
		}
	}
}
