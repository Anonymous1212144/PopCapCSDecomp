using System;
using Sexy;

namespace BejeweledLIVE
{
	public class LiveLeaderboardDialog : InterfaceWidget, ButtonListener
	{
		public static LeaderboardState CurrentLeaderboardState { get; set; }

		public static LeaderboardViewState CurrentViewState { get; set; }

		public LiveLeaderboardDialog(GameApp app)
			: base(app)
		{
			LiveLeaderboardDialog.CurrentLeaderboardState = LeaderboardState.NOT_SET;
			LiveLeaderboardDialog.CurrentViewState = LeaderboardViewState.NOT_SET;
			this.mScoresWidget = new LiveLeaderboard(this.mApp);
			this.AddWidget(this.mScoresWidget);
			this.mRank = new LiveRank(this.mApp);
			this.AddWidget(this.mRank);
			this.mLeaderboardTypeButton = new FancySmallDoubleButton(2, 1, 3, 0, Strings.Menu_Play_Classic, Strings.Menu_Play_Action, this);
			this.AddWidget(this.mLeaderboardTypeButton);
			this.mViewStateButton = new FancySmallDoubleButton(5, 4, 3, 1, Strings.Leaderboard_Friends, Strings.Leaderboard_MyScore, this);
			this.mViewStateButton.TopRightImage = AtlasResources.IMAGE_PLAYERS_ICON;
			this.mViewStateButton.BottomRightImage = AtlasResources.IMAGE_PLAYER_ICON;
			this.AddWidget(this.mViewStateButton);
			this.mRankButton = FancySmallButton.GetNewFancySmallButton(3, 3, 2, Strings.Rank, this);
			this.mRankButton.FadeWhenDisabled = true;
			this.AddWidget(this.mRankButton);
			this.mBackButton = FancySmallButton.GetNewFancySmallButton(0, 3, 3, Strings.Back, this);
			this.AddWidget(this.mBackButton);
			this.GoToState(LeaderboardState.Classic, LeaderboardViewState.Friends);
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			if (1 == uiStateLayout)
			{
				this.mBackButton.mWidth = (this.mRankButton.mWidth = (this.mLeaderboardTypeButton.mWidth = (this.mViewStateButton.mWidth = (int)Constants.mConstants.S(300f))));
				int num = this.mWidth - (int)Constants.mConstants.S(10f) - this.mBackButton.mWidth;
				int theY = this.mHeight - (int)Constants.mConstants.S(10f);
				TRect theRect = new TRect(num, theY, this.mWidth - num, 0);
				InterfaceWidget.LayoutWidgetAbove(this.mBackButton, ref theRect);
				InterfaceWidget.LayoutWidgetAbove(this.mRankButton, ref theRect);
				InterfaceWidget.LayoutWidgetAbove(this.mViewStateButton, ref theRect);
				InterfaceWidget.LayoutWidgetAbove(this.mLeaderboardTypeButton, ref theRect);
				theRect.mWidth = this.mWidth - theRect.mWidth - (int)Constants.mConstants.S(20f);
				theRect.mHeight = this.mHeight - (int)Constants.mConstants.S(10f);
				theRect.mX = (int)Constants.mConstants.S(10f);
				theRect.mY = (int)Constants.mConstants.S(10f);
				this.mScoresWidget.Resize(theRect);
				this.mScoresWidget.Layout(uiStateLayout);
				this.mRank.Resize(theRect);
			}
			else
			{
				TRect theRect2 = new TRect((int)Constants.mConstants.S(10f), this.mHeight - (int)Constants.mConstants.S(10f), this.mWidth - (int)Constants.mConstants.S(20f), 0);
				InterfaceWidget.LayoutWidgetsAbove(this.mBackButton, this.mRankButton, ref theRect2);
				InterfaceWidget.LayoutWidgetsAbove(this.mLeaderboardTypeButton, this.mViewStateButton, ref theRect2);
				theRect2.mHeight = theRect2.mY;
				theRect2.mY = (int)Constants.mConstants.S(10f);
				theRect2.mX = 0;
				theRect2.mWidth = this.mWidth;
				this.mScoresWidget.Resize(theRect2);
				this.mScoresWidget.Layout(uiStateLayout);
				this.mRank.Resize(theRect2);
			}
			this.mRank.Layout();
			this.mRank.SetVisible(LiveLeaderboardDialog.CurrentLeaderboardState == LeaderboardState.Rank);
			this.mScoresWidget.SetVisible(LiveLeaderboardDialog.CurrentLeaderboardState == LeaderboardState.Action || LiveLeaderboardDialog.CurrentLeaderboardState == LeaderboardState.Classic);
			this.GoToState(LiveLeaderboardDialog.nextLeaderboardState, LiveLeaderboardDialog.nextViewState);
		}

		public static void PrepareState(LeaderboardState goToState, LeaderboardViewState viewState)
		{
			LiveLeaderboardDialog.nextLeaderboardState = goToState;
			LiveLeaderboardDialog.nextViewState = viewState;
		}

		private void GoToState(LeaderboardState goToState)
		{
			this.GoToState(goToState, LiveLeaderboardDialog.CurrentViewState, false);
		}

		private void GoToState(LeaderboardState goToState, LeaderboardViewState viewState)
		{
			this.GoToState(goToState, viewState, false);
		}

		private void GoToState(LeaderboardState goToState, LeaderboardViewState viewState, bool immediate)
		{
			LiveLeaderboardDialog.nextLeaderboardState = goToState;
			LiveLeaderboardDialog.nextViewState = viewState;
			LeaderboardState currentLeaderboardState = LiveLeaderboardDialog.CurrentLeaderboardState;
			LeaderboardViewState currentViewState = LiveLeaderboardDialog.CurrentViewState;
			switch (goToState)
			{
			case LeaderboardState.Action:
				if ((immediate || LiveLeaderboardDialog.CurrentLeaderboardState != LeaderboardState.Action) && (immediate || LiveLeaderboardDialog.CurrentLeaderboardState == LeaderboardState.Rank))
				{
					LiveLeaderboardDialog.CurrentLeaderboardState = goToState;
					LiveLeaderboardDialog.CurrentViewState = viewState;
					this.mRank.SetVisible(false);
					this.mScoresWidget.SetupForCategory(LiveLeaderboardDialog.CurrentLeaderboardState, LiveLeaderboardDialog.CurrentViewState);
					this.mScoresWidget.SetVisible(true);
				}
				this.mScoresWidget.SetVisible(true);
				this.mRankButton.SetDisabled(false);
				this.mRank.SetDisabled(true);
				this.mScoresWidget.SetDisabled(false);
				this.mViewStateButton.SetDisabled(false);
				break;
			case LeaderboardState.Classic:
				if ((immediate || LiveLeaderboardDialog.CurrentLeaderboardState != LeaderboardState.Classic) && (immediate || LiveLeaderboardDialog.CurrentLeaderboardState == LeaderboardState.Rank))
				{
					LiveLeaderboardDialog.CurrentLeaderboardState = goToState;
					LiveLeaderboardDialog.CurrentViewState = viewState;
					this.mRank.SetVisible(false);
					this.mScoresWidget.SetupForCategory(LiveLeaderboardDialog.CurrentLeaderboardState, LiveLeaderboardDialog.CurrentViewState);
					this.mScoresWidget.SetVisible(true);
				}
				this.mScoresWidget.SetVisible(true);
				this.mRankButton.SetDisabled(false);
				this.mRank.SetDisabled(true);
				this.mScoresWidget.SetDisabled(false);
				this.mViewStateButton.SetDisabled(false);
				break;
			case LeaderboardState.Rank:
				if (immediate || LiveLeaderboardDialog.CurrentLeaderboardState != LeaderboardState.Rank)
				{
					this.mScoresWidget.SetVisible(false);
					this.mRank.SetVisible(true);
					this.mRank.SetOpacity(1f);
				}
				this.mLeaderboardTypeButton.SetDisabled(false);
				this.mLeaderboardTypeButton.mSelected = FancySmallDoubleButton.Selection.None;
				this.mViewStateButton.SetDisabled(true);
				this.mRankButton.SetDisabled(true);
				this.mRank.SetVisible(true);
				this.mRank.SetDisabled(false);
				this.mScoresWidget.SetDisabled(true);
				break;
			}
			if (goToState == LeaderboardState.Rank)
			{
				this.mViewStateButton.SetDisabled(true);
			}
			else
			{
				switch (viewState)
				{
				default:
					LiveLeaderboardDialog.CurrentViewState = viewState;
					break;
				}
			}
			LiveLeaderboardDialog.CurrentLeaderboardState = goToState;
			this.mScoresWidget.SetupForCategory(goToState, viewState);
			this.SetButtonTransparencies();
		}

		public void SetButtonTransparencies()
		{
			switch (LiveLeaderboardDialog.CurrentLeaderboardState)
			{
			case LeaderboardState.Action:
				this.mScoresWidget.SetVisible(true);
				this.mRankButton.SetDisabled(false);
				this.mRank.SetDisabled(true);
				this.mScoresWidget.SetDisabled(false);
				this.mViewStateButton.SetDisabled(false);
				this.mLeaderboardTypeButton.SetDisabled(false);
				this.mLeaderboardTypeButton.mSelected = FancySmallDoubleButton.Selection.Bottom;
				break;
			case LeaderboardState.Classic:
				this.mScoresWidget.SetVisible(true);
				this.mRankButton.SetDisabled(false);
				this.mRank.SetDisabled(true);
				this.mScoresWidget.SetDisabled(false);
				this.mViewStateButton.SetDisabled(false);
				this.mLeaderboardTypeButton.SetDisabled(false);
				this.mLeaderboardTypeButton.mSelected = FancySmallDoubleButton.Selection.Top;
				break;
			case LeaderboardState.Rank:
				this.mViewStateButton.SetDisabled(true);
				this.mRankButton.SetDisabled(true);
				this.mRank.SetVisible(true);
				this.mRank.SetDisabled(false);
				this.mScoresWidget.SetDisabled(true);
				this.mLeaderboardTypeButton.mSelected = FancySmallDoubleButton.Selection.None;
				this.mLeaderboardTypeButton.SetDisabled(false);
				break;
			}
			if (LiveLeaderboardDialog.CurrentLeaderboardState == LeaderboardState.Rank)
			{
				return;
			}
			switch (LiveLeaderboardDialog.CurrentViewState)
			{
			case LeaderboardViewState.Friends:
				this.mViewStateButton.mSelected = FancySmallDoubleButton.Selection.Top;
				return;
			case LeaderboardViewState.Personal:
				this.mViewStateButton.mSelected = FancySmallDoubleButton.Selection.Bottom;
				return;
			default:
				this.mViewStateButton.mSelected = FancySmallDoubleButton.Selection.None;
				return;
			}
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
			this.SetButtonTransparencies();
			if (uiState == 9 || uiState == 10 || uiState == 26)
			{
				this.mApp.PlaySong(SongType.MainMenu, true);
				this.mScoresWidget.RefreshList();
			}
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			if (1 == uiStateLayout)
			{
				this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.1f, 0.85f);
				this.mRankButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.1f, 0.75f);
				this.mLeaderboardTypeButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.1f, 0.35f);
				this.mViewStateButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.1f, 0.55f);
				this.mScoresWidget.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.1f, 0.95f);
			}
			else
			{
				this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.35f, 0.85f);
				this.mRankButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.35f, 0.85f);
				this.mLeaderboardTypeButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.1f, 0.75f);
				this.mViewStateButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.1f, 0.75f);
				this.mScoresWidget.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0.2f, 0.95f);
			}
			this.mScoresWidget.ClearList();
			this.mRank.Setup();
			this.mRank.SetOpacity(1f);
			this.mScoresWidget.SetOpacity(1f);
			base.TransactionTimeSeconds(1f);
			this.GoToState(LiveLeaderboardDialog.nextLeaderboardState, LiveLeaderboardDialog.nextViewState, true);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.mBackButton.FadeOut(0f, 0.5f);
			switch (LiveLeaderboardDialog.CurrentLeaderboardState)
			{
			case LeaderboardState.Action:
				this.mScoresWidget.FadeOut(0f, 0.5f);
				break;
			case LeaderboardState.Classic:
				this.mScoresWidget.FadeOut(0f, 0.5f);
				break;
			case LeaderboardState.Rank:
				this.mRank.FadeOut(0f, 0.5f);
				break;
			}
			this.mLeaderboardTypeButton.FadeOut(0f, 0.5f);
			this.mViewStateButton.FadeOut(0f, 0.5f);
			this.mRankButton.FadeOut(0f, 0.5f);
			base.TransactionTimeSeconds(0.5f);
			LiveLeaderboardDialog.PrepareState(LeaderboardState.Classic, LeaderboardViewState.Friends);
		}

		public void ButtonDepress(int buttonId)
		{
			bool flag = true;
			switch (buttonId)
			{
			case 0:
				this.BackButtonPress();
				flag = false;
				break;
			case 1:
				this.GoToState(LeaderboardState.Action);
				break;
			case 2:
				this.GoToState(LeaderboardState.Classic);
				break;
			case 3:
				this.GoToState(LeaderboardState.Rank);
				break;
			case 4:
				this.GoToState(LiveLeaderboardDialog.CurrentLeaderboardState, LeaderboardViewState.Personal);
				break;
			case 5:
				this.GoToState(LiveLeaderboardDialog.CurrentLeaderboardState, LeaderboardViewState.Friends);
				break;
			}
			if (flag)
			{
				this.mScoresWidget.RefreshList();
			}
		}

		public void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		public override bool BackButtonPress()
		{
			if (this.mInterfaceState == 10)
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_GAME);
			}
			else if (this.mInterfaceState == 26)
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_GAME_OVER);
			}
			else
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
			}
			return true;
		}

		private FancyPodButton mBackButton;

		private FancySmallDoubleButton mLeaderboardTypeButton;

		private FancyPodButton mRankButton;

		private FancySmallDoubleButton mViewStateButton;

		private LiveLeaderboard mScoresWidget;

		private LiveRank mRank;

		private static LeaderboardState nextLeaderboardState;

		private static LeaderboardViewState nextViewState;

		private enum ButtonID
		{
			BACK_BUTTON_ID,
			ACTION_BUTTON_ID,
			CLASSIC_BUTTON_ID,
			RANK_BUTTON_ID,
			VIEWSTATE_PERSONAL_BUTTON_ID,
			VIEWSTATE_FRIENDS_BUTTON_ID
		}
	}
}
