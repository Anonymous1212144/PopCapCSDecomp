using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class HighScoresMenu : Bej3Widget, Bej3ScrollWidgetListener, ScrollWidgetListener
	{
		private void SetUpSlideButtons()
		{
			int pageHorizontal = this.mScrollWidget.GetPageHorizontal();
			bool flag = pageHorizontal > 0;
			this.mSlideLeftButton.SetVisible(flag);
			this.mSlideLeftButton.SetDisabled(!flag);
			flag = pageHorizontal < 3;
			this.mSlideRightButton.SetVisible(flag);
			this.mSlideRightButton.SetDisabled(!flag);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void Update()
		{
			base.Update();
			this.SetUpSlideButtons();
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawSwipeInlay(g, this.mScrollWidget.mY, this.mScrollWidget.mHeight - 75, this.mWidth, true);
		}

		public override void DrawOverlay(Graphics g)
		{
			float num = (float)this.mAlphaCurve;
			g.SetColorizeImages(true);
			g.SetColor(new Color(255, 255, 255, (int)(255f * num)));
		}

		public override void ButtonMouseEnter(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 0:
				this.mScrollingToPage = this.mScrollWidget.GetPageHorizontal() - 1;
				this.mScrollWidget.SetPageHorizontal(this.mScrollingToPage, true);
				this.mContainer.SelectModeView(this.mContainer.mCurrentDisplayMode - 1);
				return;
			case 1:
				this.mScrollingToPage = this.mScrollWidget.GetPageHorizontal() + 1;
				this.mScrollWidget.SetPageHorizontal(this.mScrollingToPage, true);
				this.mContainer.SelectModeView(this.mContainer.mCurrentDisplayMode + 1);
				return;
			case 2:
			case 6:
				break;
			case 3:
				GlobalMembers.gApp.DoMainMenu();
				base.Transition_SlideOut();
				return;
			case 4:
			{
				GameMode gameMode = (GameMode)this.mScrollWidget.GetPageHorizontal();
				if (gameMode == GameMode.MODE_ZEN)
				{
					gameMode = GameMode.MODE_BUTTERFLY;
				}
				GlobalMembers.gApp.DoNewGame(gameMode);
				base.Transition_SlideOut();
				return;
			}
			case 5:
				if (!GlobalMembers.isLeaderboardLoading)
				{
					this.mContainer.SelectTimeView(HighScoreTable.HighScoreTableTime.TIME_RECENT);
					this.mByTodayButton.HighLighted(true);
					this.mByAllTimeButton.HighLighted(false);
					return;
				}
				break;
			case 7:
				if (!GlobalMembers.isLeaderboardLoading)
				{
					this.mContainer.SelectTimeView(HighScoreTable.HighScoreTableTime.TIME_ALLTIME);
					this.mByTodayButton.HighLighted(false);
					this.mByAllTimeButton.HighLighted(true);
				}
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

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mMainMenuButton.LinkUpAssets();
			this.mPlayAgainButton.LinkUpAssets();
			this.mContainer.LinkUpAssets();
			this.mSlideLeftButton.LinkUpAssets();
			this.mSlideRightButton.LinkUpAssets();
			this.mSlideRightButton.Resize(this.mWidth - this.mSlideRightButton.mWidth - ConstantsWP.HIGHSCORES_MENU_SLIDE_BUTTON_OFFSET_X + 10, ConstantsWP.HIGHSCORES_MENU_SLIDE_BUTTON_Y, 0, 0);
			this.mScrollWidget.SetPageHorizontal(this.mCurrentPage, true);
			this.SetUpSlideButtons();
		}

		public override void Show()
		{
			this.mContainer.Show();
			base.Show();
			this.SetVisible(false);
			base.ResetFadedBack(true);
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
			this.mSlideLeftButton.EnableSlideGlow(true);
			this.mSlideRightButton.EnableSlideGlow(true);
			this.mContainer.SelectModeView((HighScoresMenuContainer.HSMODE)this.mCurrentPage);
		}

		public override void Hide()
		{
			base.Hide();
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
			int pageHorizontal = scrollWidget.GetPageHorizontal();
			if (this.mCurrentPage != pageHorizontal)
			{
				this.mCurrentPage = pageHorizontal;
				this.LinkUpAssets();
			}
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public virtual void PageChanged(Bej3ScrollWidget scrollWidget, int pageH, int pageV)
		{
			if (pageH < 0 || pageH >= 4)
			{
				return;
			}
			if (pageH == this.mCurrentPage)
			{
				return;
			}
			this.mCurrentPage = pageH;
			this.mContainer.SelectModeView((HighScoresMenuContainer.HSMODE)this.mCurrentPage);
			this.SetUpSlideButtons();
		}

		public HighScoresMenu()
			: base(Menu_Type.MENU_HIGHSCORESMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.mAllowScrolling = true;
			this.mCurrentPage = 0;
			this.mScrollingToPage = 0;
			this.wasDown = false;
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, ConstantsWP.HIGHSCORES_MENU_WIDTH, GlobalMembers.gApp.mHeight);
			this.mFinalY = 0;
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.Resize(ConstantsWP.HIGHSCORES_MENU_HEADING_X, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetText(GlobalMembers._ID("LeaderBoards", 3381));
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.AddWidget(this.mHeadingLabel);
			this.mBottomMessageLabel = new Label(GlobalMembersResources.FONT_SUBHEADER);
			this.mBottomMessageLabel.SetTextBlock(new Rect(ConstantsWP.HIGHSCORES_MENU_BOTTOM_MESSAGE_X, ConstantsWP.HIGHSCORES_MENU_BOTTOM_MESSAGE_Y, ConstantsWP.SLIDE_BUTTON_MESSAGE_WIDTH, 0), true);
			this.mBottomMessageLabel.SetTextBlockEnabled(true);
			this.mBottomMessageLabel.SetText(GlobalMembers._ID("Swipe for more leaderboards", 3344));
			this.mBottomMessageLabel.SetClippingEnabled(false);
			this.mBottomMessageLabel.SetLayerColor(1, Bej3Widget.COLOR_SUBHEADING_3_FILL);
			this.mBottomMessageLabel.SetLayerColor(0, Bej3Widget.COLOR_SUBHEADING_3_STROKE);
			this.AddWidget(this.mBottomMessageLabel);
			this.mContainer = new HighScoresMenuContainer();
			this.mScrollWidget = new Bej3ScrollWidget(this, false);
			this.mScrollWidget.Resize(ConstantsWP.HIGHSCORES_MENU_CONTAINER_PADDING_X, ConstantsWP.HIGHSCORES_MENU_CONTAINER_TOP, this.mWidth - ConstantsWP.HIGHSCORES_MENU_CONTAINER_PADDING_X * 2, GlobalMembers.gApp.mHeight - ConstantsWP.HIGHSCORES_MENU_CONTAINER_BOTTOM - ConstantsWP.HIGHSCORES_MENU_CONTAINER_TOP + 75);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.EnablePaging(true);
			this.mScrollWidget.SetScrollInsets(new Insets(0, 0, 0, 0));
			this.mScrollWidget.AddWidget(this.mContainer);
			this.mScrollWidget.SetPageHorizontal(0, false);
			this.AddWidget(this.mScrollWidget);
			this.mMainMenuButton = new Bej3Button(3, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mMainMenuButton.SetLabel(GlobalMembers._ID("MAIN MENU", 3293));
			Bej3Widget.CenterWidgetAt(ConstantsWP.HIGHSCORES_MENU_MODE_BTN_BACK_X - 155, ConstantsWP.HIGHSCORES_MENU_MODE_BTN_BACK_Y + 110, this.mMainMenuButton, true, false);
			this.AddWidget(this.mMainMenuButton);
			this.mPlayAgainButton = new Bej3Button(4, this, Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
			this.mPlayAgainButton.SetLabel(GlobalMembers._ID("PLAY", 3290));
			Bej3Widget.CenterWidgetAt(ConstantsWP.HIGHSCORES_MENU_MODE_BTN_BACK_X + 155, ConstantsWP.HIGHSCORES_MENU_MODE_BTN_BACK_Y + 110, this.mPlayAgainButton, true, false);
			this.AddWidget(this.mPlayAgainButton);
			this.mSlideLeftButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
			this.mSlideLeftButton.Resize(ConstantsWP.HIGHSCORES_MENU_SLIDE_BUTTON_OFFSET_X - 10, ConstantsWP.HIGHSCORES_MENU_SLIDE_BUTTON_Y, 0, 0);
			this.AddWidget(this.mSlideLeftButton);
			this.mSlideRightButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE);
			this.mSlideRightButton.Resize(0, 0, 0, 0);
			this.AddWidget(this.mSlideRightButton);
			this.mByTodayButton = new Bej3Button(5, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mByTodayButton.SetLabel(GlobalMembers._ID("Recent", 7789));
			this.mByTodayButton.Resize(0, 0, 290, 18);
			Bej3Widget.CenterWidgetAt(170, 210, this.mByTodayButton, true, false);
			this.AddWidget(this.mByTodayButton);
			GlobalMembers.mByTodayButton = this.mByTodayButton;
			this.mByTodayButton.HighLighted(true);
			this.mByAllTimeButton = new Bej3Button(7, this, Bej3ButtonType.BUTTON_TYPE_LONG);
			this.mByAllTimeButton.SetLabel(GlobalMembers._ID("AllTime", 7791));
			this.mByAllTimeButton.Resize(0, 0, 290, 18);
			Bej3Widget.CenterWidgetAt(GlobalMembers.gApp.mWidth - 170, 210, this.mByAllTimeButton, true, false);
			this.AddWidget(this.mByAllTimeButton);
			GlobalMembers.mByAllTimeButton = this.mByAllTimeButton;
			if (GlobalMembers.gApp.mGameCenterIsAvailable)
			{
				this.mXBLButton = new Bej3Button(2, this, Bej3ButtonType.BUTTON_TYPE_GAMECENTER);
				Bej3Widget.CenterWidgetAt(GlobalMembers.gApp.mWidth - 80, 170, this.mXBLButton);
				this.AddWidget(this.mXBLButton);
			}
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

		private Bej3ScrollWidget mScrollWidget;

		private HighScoresMenuContainer mContainer;

		private bool mAllowScrolling;

		private int mCurrentPage;

		private int mScrollingToPage;

		private bool wasDown;

		private Label mHeadingLabel;

		private Label mBottomMessageLabel;

		private Bej3Button mMainMenuButton;

		private Bej3Button mPlayAgainButton;

		private Bej3Button mSlideLeftButton;

		private Bej3Button mSlideRightButton;

		private Bej3Button mByTodayButton;

		private Bej3Button mByAllTimeButton;

		private Bej3Button mXBLButton;

		private enum HIGHSCORESMENU_BUTTON_IDS
		{
			BTN_LEFT_ID,
			BTN_RIGHT_ID,
			BTN_XBL_ID,
			BTN_MAINMENU_ID,
			BTN_PLAYAGAIN_ID,
			BTN_TODAY_ID,
			BTN_WEEK_ID,
			BTN_ALLTIME_ID
		}
	}
}
