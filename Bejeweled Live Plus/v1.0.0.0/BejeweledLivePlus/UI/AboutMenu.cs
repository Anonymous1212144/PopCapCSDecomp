using System;
using System.Collections.Generic;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	internal class AboutMenu : Bej3Widget, CheckboxListener
	{
		public AboutMenu()
			: base(Menu_Type.MENU_ABOUTMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight - ConstantsWP.ABOUTMENU_OFFSET_Y);
			this.mFinalY = ConstantsWP.ABOUTMENU_OFFSET_Y;
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.Resize(ConstantsWP.ABOUTMENU_HEADING_LABEL_X, ConstantsWP.ABOUTMENU_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetText(GlobalMembers._ID("ABOUT", 3521));
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.AddWidget(this.mHeadingLabel);
			Font font_DIALOG = GlobalMembersResources.FONT_DIALOG;
			int num = ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_Y;
			Label label = new Label(font_DIALOG);
			label.SetText(GlobalMembers._ID("© 2012 Electronic Arts Inc. Bejeweled and PopCap are trademarks of Electronic Arts Inc.", 3523));
			label.SetTextBlockEnabled(true);
			int visibleHeight = label.GetVisibleHeight(ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_WIDTH);
			label.SetTextBlock(new Rect(ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_X, num, ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_WIDTH, visibleHeight), false);
			this.AddWidget(label);
			this.mMessageLabels.Add(label);
			num += visibleHeight + ConstantsWP.ABOUTMENU_MESSAGE_2_TEXT_OFFSET;
			label = new Label(font_DIALOG);
			label.SetText(GlobalMembers._ID("For Support visit us at:", 3075));
			label.SetTextBlockEnabled(true);
			visibleHeight = label.GetVisibleHeight(ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_WIDTH);
			label.SetTextBlock(new Rect(ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_X, num, ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_WIDTH, visibleHeight), false);
			this.AddWidget(label);
			this.mMessageLabels.Add(label);
			num += visibleHeight + ConstantsWP.ABOUTMENU_MESSAGE_2_LINK_HEIGHT;
			Bej3HyperlinkWidget bej3HyperlinkWidget = new Bej3HyperlinkWidget(4, this);
			bej3HyperlinkWidget.Resize(ConstantsWP.ABOUTMENU_LINK_X, num, ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_WIDTH, ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_HEIGHT);
			bej3HyperlinkWidget.SetFont(GlobalMembersResources.FONT_DIALOG);
			bej3HyperlinkWidget.SetLayerColor(0, Bej3Widget.COLOR_HYPERLINK_FILL);
			bej3HyperlinkWidget.mUnderlineSize = 0;
			bej3HyperlinkWidget.mLabel = GlobalMembers._ID("help@eamobile.com", 3076);
			Bej3Widget.CenterWidgetAt(ConstantsWP.ABOUTMENU_LINK_X, num, bej3HyperlinkWidget);
			this.AddWidget(bej3HyperlinkWidget);
			this.mMessageLabels.Add(bej3HyperlinkWidget);
			num += ConstantsWP.ABOUTMENU_VERSION_Y_OFFSET + ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_HEIGHT - ConstantsWP.ABOUTMENU_POST_LINK_OFFSET_Y;
			label = new Label(font_DIALOG);
			label.Resize(ConstantsWP.ABOUTMENU_LINK_X, num, ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_WIDTH, 0);
			label.SetText(GlobalMembers._ID("Version:", 3077));
			this.AddWidget(label);
			this.mMessageLabels.Add(label);
			num += ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_HEIGHT_2;
			label = new Label(font_DIALOG);
			label.Resize(ConstantsWP.ABOUTMENU_LINK_X, num, ConstantsWP.ABOUTMENU_MESSAGE_2_LABEL_WIDTH, 0);
			label.SetText(GlobalMembers.gApp.mVersion);
			this.AddWidget(label);
			this.mMessageLabels.Add(label);
			this.mBackButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE, true);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3524));
			Bej3Widget.CenterWidgetAt(ConstantsWP.ABOUTMENU_CLOSE_BUTTON_X, ConstantsWP.ABOUTMENU_CLOSE_BUTTON_Y, this.mBackButton, true, false);
			this.AddWidget(this.mBackButton);
			this.LinkUpAssets();
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.ButtonDepress(1);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawLightBox(g, new Rect(ConstantsWP.ABOUTMENU_BOX_1_X, ConstantsWP.ABOUTMENU_BOX_1_Y, ConstantsWP.ABOUTMENU_BOX_1_W, ConstantsWP.ABOUTMENU_BOX_1_H));
		}

		public override void Update()
		{
			base.Update();
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
		}

		public override void Hide()
		{
			base.Hide();
		}

		public override void Show()
		{
			base.Show();
			this.mTargetPos = ConstantsWP.ABOUTMENU_OFFSET_Y;
			this.SetVisible(false);
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 1:
				GlobalMembers.gApp.DoOptionsMenu();
				base.Transition_SlideOut();
				break;
			case 2:
			case 3:
			case 4:
				break;
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

		public virtual void CheckboxChecked(int theId, bool check)
		{
			if (theId != 2)
			{
				return;
			}
			if (check != GlobalMembers.gApp.mProfile.mAllowAnalytics)
			{
				GlobalMembers.gApp.mProfile.mAllowAnalytics = check;
				GlobalMembers.gApp.mProfile.WriteProfile();
				this.LinkUpAssets();
			}
		}

		private Label mHeadingLabel;

		private List<Widget> mMessageLabels = new List<Widget>();

		private Bej3Button mBackButton;

		private enum AboutMenu_IDS
		{
			BTN_TERMS_ID,
			BTN_BACK_ID,
			CHK_ANALYTICS_ID,
			BTN_POPCAP_COM_ID,
			BTN_SUPPORT_ID
		}
	}
}
