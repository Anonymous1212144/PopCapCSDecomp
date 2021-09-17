using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class ProfileMenuContainer : Bej3Widget, Bej3ScrollWidgetListener, ScrollWidgetListener
	{
		public ProfileMenuContainer(ProfileMenu parent)
			: base(Menu_Type.MENU_PROFILEMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mDoesSlideInFromBottom = (this.mCanAllowSlide = false);
			this.Resize(0, 0, this.mWidth - ConstantsWP.PROFILEMENU_PADDING_X * 2, ConstantsWP.PROFILEMENU_CONTAINER_HEIGHT);
			this.mGoToStatsButton = new Bej3Button(2, parent, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mGoToHighscoresButton = new Bej3Button(3, parent, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mGoToBadgesButton = new Bej3Button(4, parent, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mGoToStatsButton.SetLabel(GlobalMembers._ID("STATS", 3429));
			this.mGoToHighscoresButton.SetLabel(GlobalMembers._ID("TOP SCORES", 3430));
			this.mGoToBadgesButton.SetLabel(GlobalMembers._ID("BADGES", 3431));
			int mHeight = this.mGoToStatsButton.mComponentImage.mHeight;
			int num = this.mGoToStatsButton.GetButtonWidth();
			num = ConstantsWP.PROFILEMENU_GOTO_BUTTON_WIDTH;
			this.mGoToStatsButton.Resize((this.mWidth - num) / 2, ConstantsWP.PROFILEMENU_STATS_Y + ConstantsWP.PROFILEMENU_STATS_BTN_OFFSET_Y, num, mHeight);
			int num2 = this.mGoToHighscoresButton.GetButtonWidth();
			num2 = ConstantsWP.PROFILEMENU_GOTO_BUTTON_WIDTH;
			this.mGoToHighscoresButton.Resize((this.mWidth - num2) / 2, ConstantsWP.PROFILEMENU_HIGHSCORES_Y + ConstantsWP.PROFILEMENU_SCORES_BTN_OFFSET_Y, num2, mHeight);
			int num3 = this.mGoToBadgesButton.GetButtonWidth();
			num3 = ConstantsWP.PROFILEMENU_GOTO_BUTTON_WIDTH;
			this.mGoToBadgesButton.Resize((this.mWidth - num3) / 2, ConstantsWP.PROFILEMENU_BADGES_Y + ConstantsWP.PROFILEMENU_BADGES_BTN_OFFSET_Y, num3, mHeight);
			this.AddWidget(this.mGoToStatsButton);
			this.AddWidget(this.mGoToHighscoresButton);
			this.AddWidget(this.mGoToBadgesButton);
		}

		public override void Draw(Graphics g)
		{
		}

		public override void Update()
		{
			base.Update();
		}

		public override void ButtonDepress(int theId)
		{
		}

		public override void LinkUpAssets()
		{
		}

		public override void Hide()
		{
			base.Hide();
			this.mAlphaCurve.SetConstant(1.0);
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
		}

		public override void Show()
		{
			base.Show();
			this.mY = this.mTargetPos;
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public virtual void PageChanged(Bej3ScrollWidget scrollWidget, int pageH, int pageV)
		{
		}

		private Bej3Button mGoToStatsButton;

		private Bej3Button mGoToHighscoresButton;

		private Bej3Button mGoToBadgesButton;
	}
}
