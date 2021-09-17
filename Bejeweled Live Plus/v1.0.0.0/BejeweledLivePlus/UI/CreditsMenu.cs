using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class CreditsMenu : Bej3Widget, Bej3ButtonListener, ButtonListener
	{
		public CreditsMenu()
			: base(Menu_Type.MENU_CREDITSMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.Resize(ConstantsWP.CREDITSMENU_HEADING_LABEL_X, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetText(GlobalMembers._ID("CREDITS", 3243));
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.AddWidget(this.mHeadingLabel);
			this.mContainer = new CreditsMenuContainer();
			this.mScrollWidget = new CreditsMenuScrollWidget(this.mContainer);
			this.mScrollWidget.Resize(ConstantsWP.CREDITSMENU_PADDING_X, ConstantsWP.CREDITSMENU_PADDING_TOP, this.mWidth - ConstantsWP.CREDITSMENU_PADDING_X * 2, this.mHeight - ConstantsWP.CREDITSMENU_PADDING_TOP - ConstantsWP.CREDITSMENU_PADDING_BOTTOM);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_VERTICAL);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.AddWidget(this.mContainer);
			this.AddWidget(this.mScrollWidget);
			this.mBackButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE, true);
			this.mBackButton.Resize(0, 0, ConstantsWP.ABOUTMENU_BACK_BUTTON_WIDTH, 0);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3244));
			Bej3Widget.CenterWidgetAt(this.mWidth / 2, ConstantsWP.CREDITSMENU_BUTTON_Y, this.mBackButton, true, false);
			this.AddWidget(this.mBackButton);
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

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawSwipeInlay(g, this.mScrollWidget.mY, this.mScrollWidget.mHeight, this.mWidth);
		}

		public override void SetVisible(bool isVisible)
		{
			base.SetVisible(isVisible);
			if (this.mContainer != null)
			{
				this.mContainer.SetVisible(isVisible);
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mContainer.LinkUpAssets();
		}

		public override void Show()
		{
			this.mContainer.Show();
			base.Show();
			this.mScrollWidget.Restart();
			this.mScrollWidget.mAnimate = true;
			this.mTargetPos = 106;
			this.SetVisible(false);
		}

		public override void Hide()
		{
			base.Hide();
			this.mContainer.Hide();
		}

		public override void ButtonDepress(int theId)
		{
			if (theId == 0)
			{
				GlobalMembers.gApp.DoOptionsMenu();
				base.Transition_SlideOut();
				return;
			}
			if (theId != 10001)
			{
				return;
			}
			GlobalMembers.gApp.DoMainMenu();
			base.Transition_SlideOut();
		}

		private Label mHeadingLabel;

		private CreditsMenuScrollWidget mScrollWidget;

		private CreditsMenuContainer mContainer;

		private Bej3Button mBackButton;

		private enum CreditsMenu_IDS
		{
			BTN_BACK_ID
		}
	}
}
