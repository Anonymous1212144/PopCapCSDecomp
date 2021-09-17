using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class ZenInfoDialog : Bej3Dialog
	{
		public ZenInfoDialog()
			: base(44, true, GlobalMembers._ID("ZEN", 3443), "", GlobalMembers._ID("CONTINUE", 3444), 3, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED)
		{
			this.LinkUpAssets();
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, ConstantsWP.ZENINFODIALOG_WIDTH, GlobalMembers.gApp.mHeight);
			this.mTargetPos = 0;
			if (this.mTopButton != null)
			{
				this.mTopButton.mId = 1001;
			}
			int num = ConstantsWP.ZENINFODIALOG_MSG_START_Y;
			int num2 = ConstantsWP.ZENINFODIALOG_WIDTH - ConstantsWP.ZENINFODIALOG_MSG_LEFT_X * 2;
			this.mExtraMessageLabel1 = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("Relax as you play this endless mode", 3445));
			this.mExtraMessageLabel1.SetClippingEnabled(false);
			this.mExtraMessageLabel1.SetTextBlock(new Rect(ConstantsWP.ZENINFODIALOG_MSG_LEFT_X, num, num2, ConstantsWP.ZENINFODIALOG_MSG_HEIGHT), false);
			this.mExtraMessageLabel1.SetTextBlockEnabled(true);
			this.AddWidget(this.mExtraMessageLabel1);
			num += ConstantsWP.ZENINFODIALOG_MSG_HEIGHT;
			this.mExtraMessageLabel2 = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Chill out with soothing ambient sounds", 3446));
			this.mExtraMessageLabel2.SetClippingEnabled(false);
			this.mExtraMessageLabel2.SetTextBlock(new Rect(ConstantsWP.ZENINFODIALOG_MSG_LEFT_X + ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, num, num2 - ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, ConstantsWP.ZENINFODIALOG_MSG_HEIGHT), false);
			this.mExtraMessageLabel2.SetTextBlockEnabled(true);
			this.mExtraMessageLabel2.SetTextBlockAlignment(-1);
			this.AddWidget(this.mExtraMessageLabel2);
			num += ConstantsWP.ZENINFODIALOG_MSG_HEIGHT;
			this.mExtraMessageLabel3 = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Boost your brain with positive mantras", 3447));
			this.mExtraMessageLabel3.SetClippingEnabled(false);
			this.mExtraMessageLabel3.SetTextBlock(new Rect(ConstantsWP.ZENINFODIALOG_MSG_LEFT_X + ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, num, num2 - ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, ConstantsWP.ZENINFODIALOG_MSG_HEIGHT), false);
			this.mExtraMessageLabel3.SetTextBlockEnabled(true);
			this.mExtraMessageLabel3.SetTextBlockAlignment(-1);
			this.AddWidget(this.mExtraMessageLabel3);
			num += ConstantsWP.ZENINFODIALOG_MSG_HEIGHT;
			this.mExtraMessageLabel4 = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Release stress with breathing modulation", 3448));
			this.mExtraMessageLabel4.SetClippingEnabled(false);
			this.mExtraMessageLabel4.SetTextBlock(new Rect(ConstantsWP.ZENINFODIALOG_MSG_LEFT_X + ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, num, num2 - ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, ConstantsWP.ZENINFODIALOG_MSG_HEIGHT), false);
			this.mExtraMessageLabel4.SetTextBlockEnabled(true);
			this.mExtraMessageLabel4.SetTextBlockAlignment(-1);
			this.AddWidget(this.mExtraMessageLabel4);
			num += ConstantsWP.ZENINFODIALOG_MSG_HEIGHT;
			this.mExtraMessageLabel5 = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("Headphones are recommended", 3449));
			this.mExtraMessageLabel5.SetClippingEnabled(false);
			this.mExtraMessageLabel5.SetTextBlock(new Rect(ConstantsWP.ZENINFODIALOG_MSG_LEFT_X, num, num2, ConstantsWP.ZENINFODIALOG_MSG_HEIGHT), false);
			this.mExtraMessageLabel5.SetTextBlockEnabled(true);
			this.mExtraMessageLabel5.SetTextBlockAlignment(0);
			this.AddWidget(this.mExtraMessageLabel5);
			Bej3Widget.CenterWidgetAt(ConstantsWP.ZENINFODIALOG_WIDTH / 2, ConstantsWP.MENU_BOTTOM_BUTTON_Y, this.mYesButton, true, false);
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
			((Bej3Button)this.mYesButton).SetType(Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM, this.mExtraMessageLabel2.mX - ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, this.mExtraMessageLabel2.mY + ConstantsWP.ZENINFODIALOG_GEM_BULLET_Y_OFFSET);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM, this.mExtraMessageLabel3.mX - ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, this.mExtraMessageLabel3.mY + ConstantsWP.ZENINFODIALOG_GEM_BULLET_Y_OFFSET);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM, this.mExtraMessageLabel4.mX - ConstantsWP.ZENINFODIALOG_GEM_BULLET_WIDTH, this.mExtraMessageLabel4.mY + ConstantsWP.ZENINFODIALOG_GEM_BULLET_Y_OFFSET);
		}

		private Label mExtraMessageLabel1;

		private Label mExtraMessageLabel2;

		private Label mExtraMessageLabel3;

		private Label mExtraMessageLabel4;

		private Label mExtraMessageLabel5;
	}
}
