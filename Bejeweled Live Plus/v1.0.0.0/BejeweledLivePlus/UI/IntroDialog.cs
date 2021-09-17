using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class IntroDialog : Bej3Dialog
	{
		public IntroDialog()
			: base(58, true, GlobalMembers._ID("WELCOME TO BEJEWELED", 3347), "", GlobalMembers._ID("CONTINUE", 3348), 3, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.mExtraMessageLabel1 = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("New to Bejeweled?", 3349));
			this.mExtraMessageLabel1.SetTextBlock(new Rect(ConstantsWP.INTRO_DIALOG_TEXT_X, ConstantsWP.INTRO_DIALOG_TEXT_1_Y, ConstantsWP.INTRO_DIALOG_TEXT_WIDTH, 0), false);
			this.mExtraMessageLabel1.SetTextBlockEnabled(true);
			this.AddWidget(this.mExtraMessageLabel1);
			this.mExtraMessageLabel2 = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("^700443^Start with ^7514BB^Classic^700443^.", 3350));
			this.mExtraMessageLabel2.SetTextBlock(new Rect(ConstantsWP.INTRO_DIALOG_TEXT_X, ConstantsWP.INTRO_DIALOG_TEXT_2_Y, ConstantsWP.INTRO_DIALOG_TEXT_WIDTH, 0), false);
			this.mExtraMessageLabel2.SetTextBlockEnabled(true);
			this.mExtraMessageLabel2.SetLayerColor(0, Color.White);
			this.AddWidget(this.mExtraMessageLabel2);
			this.mExtraMessageLabel3 = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("Already a Bejeweled Pro?", 3351));
			this.mExtraMessageLabel3.SetTextBlock(new Rect(ConstantsWP.INTRO_DIALOG_TEXT_X, ConstantsWP.INTRO_DIALOG_TEXT_3_Y, ConstantsWP.INTRO_DIALOG_TEXT_WIDTH, 0), false);
			this.mExtraMessageLabel3.SetTextBlockEnabled(true);
			this.AddWidget(this.mExtraMessageLabel3);
			this.mExtraMessageLabel4 = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("^700443^Try all-new ^7514BB^Butterflies^700443^,\ndig for treasure in ^7514BB^Diamond Mine^700443^\nor relax body and mind in ^7514BB^Zen^700443^ mode.", 3352));
			this.mExtraMessageLabel4.SetTextBlock(new Rect(ConstantsWP.INTRO_DIALOG_TEXT_X, ConstantsWP.INTRO_DIALOG_TEXT_4_Y, ConstantsWP.INTRO_DIALOG_TEXT_WIDTH, 0), false);
			this.mExtraMessageLabel4.SetTextBlockEnabled(true);
			this.mExtraMessageLabel4.SetLayerColor(0, Color.White);
			this.AddWidget(this.mExtraMessageLabel4);
			((Bej3Button)this.mYesButton).SetType(Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public new void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public new int GetPreferredHeight(int theWidth)
		{
			return ConstantsWP.INTRO_DIALOG_HEIGHT;
		}

		public new void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public new void Kill()
		{
			GlobalMembers.gApp.mProfile.mHasSeenIntro = true;
			base.Kill();
		}

		private Label mExtraMessageLabel1;

		private Label mExtraMessageLabel2;

		private Label mExtraMessageLabel3;

		private Label mExtraMessageLabel4;

		private Label mExtraMessageLabel5;
	}
}
