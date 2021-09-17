using System;
using System.Collections.Generic;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class GameDetailMenu : Bej3Widget, Bej3ScrollWidgetListener, ScrollWidgetListener
	{
		private void Init()
		{
			this.mIgnoreSetMode = false;
			this.mMode = GameMode.MODE_CLASSIC;
			this.mGameMenuState = GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME;
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.AddWidget(this.mHeadingLabel);
			this.mPlayButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
			this.AddWidget(this.mPlayButton);
			this.mBackButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3289));
			this.AddWidget(this.mBackButton);
			this.mScrollWidget = new Bej3ScrollWidget(this, false);
			this.mEndGameContainer = new GameDetailMenuContainer();
			this.mScrollWidget.Resize(ConstantsWP.GAMEDETAILMENU_POST_GAME_CONTAINER_X, ConstantsWP.GAMEDETAILMENU_POST_GAME_CONTAINER_Y, ConstantsWP.GAMEDETAILMENU_POST_GAME_CONTAINER_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_CONTAINER_HEIGHT + 75);
			this.mScrollWidget.AddWidget(this.mEndGameContainer);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.EnablePaging(true);
			this.mScrollWidget.SetScrollInsets(new Insets(0, 0, 0, 0));
			this.mScrollWidget.SetPageHorizontal(0, false);
			this.AddWidget(this.mScrollWidget);
			this.mDefaultWidgetsPostGame.Add(this.mScrollWidget);
			this.mDefaultWidgetsPostGame.Add(this.mEndGameContainer);
			this.mModeDescriptionLabel = new Label(GlobalMembersResources.FONT_SUBHEADER);
			this.mModeDescriptionLabel.SetTextBlockEnabled(true);
			this.mModeDescriptionLabel.SetTextBlock(new Rect(ConstantsWP.GAMEDETAILMENU_MODE_DESCRIPTION_X, ConstantsWP.GAMEDETAILMENU_MODE_DESCRIPTION_Y, ConstantsWP.GAMEDETAILMENU_MODE_DESCRIPTION_WIDTH, ConstantsWP.GAMEDETAILMENU_MODE_DESCRIPTION_HEIGHT), true);
			this.mModeDescriptionLabel.SetClippingEnabled(false);
			this.mDefaultWidgetsPreGame.Add(this.mModeDescriptionLabel);
			this.AddWidget(this.mModeDescriptionLabel);
			this.mSlideLeftButton = new Bej3Button(3, this, Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
			this.mSlideLeftButton.Resize(ConstantsWP.GAMEDETAILMENU_POST_GAME_SLIDE_BUTTON_OFFSET_X, ConstantsWP.GAMEDETAILMENU_POST_GAME_SLIDE_BUTTON_Y, 0, 0);
			this.AddWidget(this.mSlideLeftButton);
			this.mDefaultWidgetsPostGame.Add(this.mSlideLeftButton);
			this.mSlideRightButton = new Bej3Button(2, this, Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE);
			this.mSlideRightButton.Resize(0, 0, 0, 0);
			this.AddWidget(this.mSlideRightButton);
			this.mDefaultWidgetsPostGame.Add(this.mSlideRightButton);
			this.mHighScoresWidgetPreGame = new HighScoresWidget(new Rect(ConstantsWP.GAMEDETAILMENU_HIGHSCORES_X, ConstantsWP.GAMEDETAILMENU_HIGHSCORES_Y, ConstantsWP.GAMEDETAILMENU_HIGHSCORES_WIDTH, ConstantsWP.GAMEDETAILMENU_HIGHSCORES_HEIGHT), false);
			this.mDefaultWidgetsPreGame.Add(this.mHighScoresWidgetPreGame);
			this.AddWidget(this.mHighScoresWidgetPreGame);
			this.mSwipeMsgLabel = new Label(GlobalMembersResources.FONT_SUBHEADER, string.Empty);
			this.mSwipeMsgLabel.SetTextBlock(new Rect(ConstantsWP.GAMEDETAILMENU_POST_GAME_SWIPE_MSG_X, ConstantsWP.GAMEDETAILMENU_POST_GAME_SWIPE_MSG_Y_1 - 50, ConstantsWP.SLIDE_BUTTON_MESSAGE_WIDTH, 100), true);
			this.mSwipeMsgLabel.SetTextBlockEnabled(true);
			this.mSwipeMsgLabel.SetLayerColor(1, Bej3Widget.COLOR_SUBHEADING_3_FILL);
			this.mSwipeMsgLabel.SetLayerColor(0, Bej3Widget.COLOR_SUBHEADING_3_STROKE);
			this.AddWidget(this.mSwipeMsgLabel);
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
		}

		private void SetUpSlideButtons()
		{
			if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME)
			{
				this.mHeadingLabel.SetVisible(true);
				this.mSlideRightButton.Resize(this.mWidth - this.mSlideRightButton.mWidth - ConstantsWP.GAMEDETAILMENU_POST_GAME_SLIDE_BUTTON_OFFSET_X, ConstantsWP.GAMEDETAILMENU_POST_GAME_SLIDE_BUTTON_Y, 0, 0);
				int pageHorizontal = this.mScrollWidget.GetPageHorizontal();
				bool flag = pageHorizontal > 0;
				this.mSlideLeftButton.SetVisible(flag);
				this.mSlideLeftButton.SetDisabled(!flag);
				flag = pageHorizontal < 1;
				this.mSlideRightButton.SetVisible(flag);
				this.mSlideRightButton.SetDisabled(!flag);
				this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
				this.mHeadingLabel.Resize(ConstantsWP.GAMEDETAILMENU_HEADING_X, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
				switch (pageHorizontal)
				{
				case 0:
					this.mSwipeMsgLabel.SetText(GlobalMembers._ID("Swipe for stats", 3294));
					return;
				case 1:
					this.mSwipeMsgLabel.SetText(GlobalMembers._ID("Swipe for top scores", 3295));
					break;
				default:
					return;
				}
			}
		}

		protected GameDetailMenu(Menu_Type type, bool hasBackButton, Bej3ButtonType topButtonType)
			: base(type, hasBackButton, topButtonType)
		{
			this.mAllowScrolling = true;
			this.wasDown = false;
			this.Init();
			this.LinkUpAssets();
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		public GameDetailMenu()
			: base(Menu_Type.MENU_GAMEDETAILMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.Init();
			this.LinkUpAssets();
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back)
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

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			if (this.mY < ConstantsWP.MENU_Y_POS_HIDDEN)
			{
				base.DrawAll(theFlags, g);
				return;
			}
			this.Draw(g);
			if (this.mTopButton != null)
			{
				g.Translate(this.mTopButton.mX, this.mTopButton.mY);
				this.mTopButton.Draw(g);
				g.Translate(-this.mTopButton.mX, -this.mTopButton.mY);
			}
		}

		public override void Draw(Graphics g)
		{
			g.PushState();
			this.mAlphaCurve;
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME)
			{
				Bej3Widget.DrawSwipeInlay(g, this.mScrollWidget.mY, this.mScrollWidget.mHeight - 75, this.mWidth, true);
			}
			g.PopState();
		}

		public override void UpdateAll(ModalFlags theFlags)
		{
			if (!this.mVisible)
			{
				return;
			}
			if (this.mY < ConstantsWP.MENU_Y_POS_HIDDEN || this.mState == Bej3WidgetState.STATE_FADING_IN)
			{
				base.UpdateAll(theFlags);
				return;
			}
			this.Update();
			if (this.mTopButton != null)
			{
				this.mTopButton.Update();
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME)
			{
				this.SetUpSlideButtons();
			}
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			bool mAllowSlide = this.mAllowSlide;
			base.AllowSlideIn(allow, previousTopButton);
			if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME)
			{
				if (!this.mAllowSlide && allow && this.mState == Bej3WidgetState.STATE_FADING_IN)
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
					return;
				}
			}
			else
			{
				if (previousTopButton != null)
				{
					base.SetTopButtonType(previousTopButton.GetType());
				}
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
			}
		}

		public override void ButtonMouseEnter(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 0:
				if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME)
				{
					GlobalMembers.gApp.StartSetupGame(true);
				}
				else if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME)
				{
					if (GlobalMembers.gApp.mBoard != null)
					{
						GlobalMembers.gApp.mBoard.mShouldUnloadContentWhenDone = false;
					}
					this.mIgnoreSetMode = true;
					GlobalMembers.gApp.DoNewGame(this.mMode);
					GlobalMembers.gApp.mBoard.mAlphaCurve.SetConstant(1.0);
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
					this.mIgnoreSetMode = false;
					base.Transition_SlideOut();
				}
				if (this.mInterfaceState != InterfaceState.INTERFACE_STATE_HELPMENU)
				{
					base.Transition_SlideOut();
					return;
				}
				break;
			case 1:
				GlobalMembers.gApp.DoMainMenu();
				base.Transition_SlideOut();
				return;
			case 2:
				this.mScrollWidget.SetPageHorizontal(this.mScrollWidget.GetPageHorizontal() + 1, true);
				return;
			case 3:
				this.mScrollWidget.SetPageHorizontal(this.mScrollWidget.GetPageHorizontal() - 1, true);
				this.mEndGameContainer.mHighScoresWidgetPostGame.ReadLeaderBoard(HighScoreTable.HighScoreTableTime.TIME_RECENT);
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

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			if (this != null)
			{
				this.LinkUpAssets();
			}
		}

		public void SetMode(GameMode mode, GameDetailMenu.GAMEDETAILMENU_STATE state)
		{
			if (mode == GameMode.MODE_ZEN)
			{
			}
			if (this.mIgnoreSetMode)
			{
				return;
			}
			this.mMode = mode;
			this.mGameMenuState = state;
			this.mHeadingLabel.SetMaximumWidth(0);
			this.mHeadingLabel.Resize(ConstantsWP.GAMEDETAILMENU_HEADING_X, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.mSwipeMsgLabel.SetVisible(state == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME);
			if (this.mMode != GameMode.MODE_ZEN)
			{
				switch (this.mGameMenuState)
				{
				case GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME:
				{
					for (int i = 0; i < this.mDefaultWidgetsPostGame.Count; i++)
					{
						Bej3Widget.DisableWidget(this.mDefaultWidgetsPostGame[i], true);
					}
					this.mEndGameContainer.Hide();
					for (int j = 0; j < this.mDefaultWidgetsPreGame.Count; j++)
					{
						Bej3Widget.DisableWidget(this.mDefaultWidgetsPreGame[j], false);
					}
					this.mHighScoresWidgetPreGame.SetMode(mode);
					this.mHeadingLabel.SetText(GlobalMembers.gApp.GetModeHeading(this.mMode));
					this.mPlayButton.SetLabel(GlobalMembers._ID("PLAY", 3290));
					this.mBackButton.SetLabel(GlobalMembers._ID("BACK", 3291));
					this.mModeDescriptionLabel.SetText(GlobalMembers.gApp.GetModeHint(this.mMode));
					break;
				}
				case GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME:
				{
					for (int k = 0; k < this.mDefaultWidgetsPreGame.Count; k++)
					{
						Bej3Widget.DisableWidget(this.mDefaultWidgetsPreGame[k], true);
					}
					for (int l = 0; l < this.mDefaultWidgetsPostGame.Count; l++)
					{
						Bej3Widget.DisableWidget(this.mDefaultWidgetsPostGame[l], false);
					}
					this.mEndGameContainer.Show();
					this.mPlayButton.SetLabel(GlobalMembers._ID("PLAY AGAIN", 3292));
					this.mBackButton.SetLabel(GlobalMembers._ID("MAIN MENU", 3293));
					this.mEndGameContainer.SetMode(this.mMode, this.mGameMenuState);
					break;
				}
				}
			}
			Bej3Widget.CenterWidgetAt(ConstantsWP.GAMEDETAILMENU_PLAYBUTTON_X, ConstantsWP.GAMEDETAILMENU_PLAYBUTTON_Y + 50, this.mPlayButton, true, false);
			Bej3Widget.CenterWidgetAt(ConstantsWP.GAMEDETAILMENU_BACKBUTTON_X, ConstantsWP.GAMEDETAILMENU_PLAYBUTTON_Y + 50, this.mBackButton, true, false);
		}

		public override void HideCompleted()
		{
			this.mEndGameContainer.HideCompleted();
			base.HideCompleted();
			if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_INGAME)
			{
				BejeweledLivePlusApp.UnloadContent("MainMenu");
			}
		}

		public override void LinkUpAssets()
		{
			if (this.mPlayButton != null && this.mBackButton != null && this.mHighScoresWidgetPreGame != null)
			{
				this.mPlayButton.LinkUpAssets();
				this.mBackButton.LinkUpAssets();
				this.mHighScoresWidgetPreGame.LinkUpAssets();
				this.SetUpSlideButtons();
				base.LinkUpAssets();
			}
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
			int pageHorizontal = this.mScrollWidget.GetPageHorizontal();
			if (this.ScrollTargetReached_lastPage != pageHorizontal)
			{
				this.ScrollTargetReached_lastPage = pageHorizontal;
				this.LinkUpAssets();
			}
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public virtual void PageChanged(Bej3ScrollWidget scrollWidget, int pageH, int pageV)
		{
			if (pageH == 0)
			{
				this.mEndGameContainer.mHighScoresWidgetPostGame.ReadLeaderBoard(HighScoreTable.HighScoreTableTime.TIME_RECENT);
			}
			this.SetUpSlideButtons();
		}

		public override void Show()
		{
			if (!this.mVisible)
			{
				this.mY = ConstantsWP.MENU_Y_POS_HIDDEN;
				if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_PRE_GAME)
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
				}
				else
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
				}
			}
			this.mScrollWidget.SetPageHorizontal(0, false);
			this.LinkUpAssets();
			base.Show();
			this.mAlphaCurve.SetConstant(1.0);
			if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME)
			{
				base.SetTargetPosition(50);
				base.Transition_SlideThenFadeIn();
				GlobalMembers.gApp.mMenus[7].SetVisible(false);
				GlobalMembers.gApp.mMenus[7].Hide();
				this.mEndGameContainer.mHighScoresWidgetPostGame.CenterOnUser();
			}
			else
			{
				this.SetVisible(false);
				this.mHighScoresWidgetPreGame.CenterOnUser();
			}
			base.ResetFadedBack(true);
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
			if (this.mGameMenuState == GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME)
			{
				this.mSlideLeftButton.EnableSlideGlow(true);
				this.mSlideRightButton.EnableSlideGlow(true);
			}
		}

		public override void Hide()
		{
			Bej3WidgetState mState = this.mState;
			base.Hide();
			this.mEndGameContainer.Hide();
		}

		public void GetStatsFromBoard(Board theBoard)
		{
			this.mEndGameContainer.GetStatsFromBoard(theBoard);
			switch (this.mMode)
			{
			case GameMode.MODE_CLASSIC:
			{
				int mPoints = theBoard.mPoints;
				this.mHeadingLabel.SetText(Common.CommaSeperate(mPoints));
				break;
			}
			case GameMode.MODE_LIGHTNING:
			{
				int mPoints2 = theBoard.mPoints;
				this.mHeadingLabel.SetText(Common.CommaSeperate(mPoints2));
				break;
			}
			case GameMode.MODE_DIAMOND_MINE:
			{
				int mLevelPointsTotal = theBoard.mLevelPointsTotal;
				this.mHeadingLabel.SetText(string.Format("{0}", Common.CommaSeperate(mLevelPointsTotal)));
				break;
			}
			case GameMode.MODE_BUTTERFLY:
			{
				int mPoints3 = theBoard.mPoints;
				this.mHeadingLabel.SetText(Common.CommaSeperate(mPoints3));
				break;
			}
			case GameMode.MODE_POKER:
			{
				int mPoints4 = theBoard.mPoints;
				this.mHeadingLabel.SetText(Common.CommaSeperate(mPoints4));
				break;
			}
			}
			GlobalMembers.gApp.mProfile.UpdateRank(theBoard);
		}

		public GameDetailMenu.GAMEDETAILMENU_STATE GetGameMenuState()
		{
			return this.mGameMenuState;
		}

		public const int MAX_TABS = 2;

		protected GameMode mMode;

		protected GameDetailMenu.GAMEDETAILMENU_STATE mGameMenuState;

		protected Bej3Button mPlayButton;

		protected Bej3Button mBackButton;

		protected Label mHeadingLabel;

		protected HighScoresWidget mHighScoresWidgetPreGame;

		protected List<Widget> mDefaultWidgetsPreGame = new List<Widget>();

		protected List<Widget> mDefaultWidgetsPostGame = new List<Widget>();

		protected Bej3Button mSlideLeftButton;

		protected Bej3Button mSlideRightButton;

		protected Label mSwipeMsgLabel;

		protected Label mModeDescriptionLabel;

		protected Bej3ScrollWidget mScrollWidget;

		protected GameDetailMenuContainer mEndGameContainer;

		protected bool mAllowScrolling;

		protected bool wasDown;

		private bool mIgnoreSetMode;

		private int ScrollTargetReached_lastPage = -1;

		public enum GAMEDETAILMENU_STATE
		{
			STATE_PRE_GAME,
			STATE_POST_GAME
		}

		public enum GAMEDETAILMENU_IDS
		{
			BTN_PLAY_ID,
			BTN_BACK_ID,
			BTN_RIGHT_ID,
			BTN_LEFT_ID
		}
	}
}
