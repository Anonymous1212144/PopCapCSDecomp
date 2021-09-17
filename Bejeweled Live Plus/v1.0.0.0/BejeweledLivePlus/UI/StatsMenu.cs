using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	internal class StatsMenu : Bej3Widget, Bej3ButtonListener, ButtonListener
	{
		public StatsMenu()
			: base(Menu_Type.MENU_STATSMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mFinalY = 0;
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.Resize(ConstantsWP.STATS_MENU_HEADING_X, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetText(GlobalMembers._ID("STATS", 3439));
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.AddWidget(this.mHeadingLabel);
			this.mProfileButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE, true);
			this.mProfileButton.SetLabel(GlobalMembers._ID("BACK", 3440));
			Bej3Widget.CenterWidgetAt(320, 966, this.mProfileButton, true, false);
			this.AddWidget(this.mProfileButton);
			this.mContainer = new StatsMenuContainer();
			this.mScrollWidget = new ScrollWidget(this.mContainer);
			this.mScrollWidget.Resize(ConstantsWP.STATS_MENU_CONTAINER_PADDING_X, ConstantsWP.STATS_MENU_CONTAINER_PADDING_TOP, this.mWidth - ConstantsWP.STATS_MENU_CONTAINER_PADDING_X * 2, this.mHeight - ConstantsWP.STATS_MENU_CONTAINER_PADDING_TOP - ConstantsWP.STATS_MENU_CONTAINER_PADDING_BOTTOM);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_VERTICAL);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.AddWidget(this.mContainer);
			this.AddWidget(this.mScrollWidget);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.ButtonDepress(10002);
			}
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.MENU_DIVIDER_Y);
			base.DeferOverlay(0);
		}

		public override void DrawOverlay(Graphics g)
		{
		}

		public override void ButtonMouseEnter(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			if (theId == 10001)
			{
				GlobalMembers.gApp.DoMainMenu();
				((MainMenuOptions)GlobalMembers.gApp.mMenus[5]).Expand();
				base.Transition_SlideOut();
				return;
			}
			GlobalMembers.gApp.DoMainMenu();
			((MainMenuOptions)GlobalMembers.gApp.mMenus[5]).Expand();
			base.Transition_SlideOut();
		}

		public override void LinkUpAssets()
		{
			this.mContainer.LinkUpAssets();
			base.LinkUpAssets();
		}

		public override void Show()
		{
			this.mContainer.Show();
			base.Show();
			this.SetVisible(false);
		}

		public override void ShowCompleted()
		{
			this.mTopButton.SetType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
			this.mTopButton.SetDisabled(false);
		}

		public override void Hide()
		{
			base.Hide();
			this.mContainer.Hide();
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
			if (this.mInterfaceState != InterfaceState.INTERFACE_STATE_PROFILEMENU)
			{
				((ProfileMenuBase)GlobalMembers.gApp.mMenus[12]).UnloadPlayerImages();
			}
		}

		private Label mHeadingLabel;

		private StatsMenuContainer mContainer;

		private ScrollWidget mScrollWidget;

		private Bej3Button mProfileButton;

		private enum STATSMENU_BUTTON_IDS
		{
			BTN_PROFILE_ID
		}
	}
}
