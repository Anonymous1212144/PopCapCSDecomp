using System;
using Sexy;

namespace BejeweledLIVE
{
	public class LiveLeaderboard : InterfaceControl, DialogListener
	{
		public LiveLeaderboard(GameApp app)
		{
			this.mApp = app;
			this.mScoresFrame = new FrameWidget();
			this.AddWidget(this.mScoresFrame);
			this.mCurrentActiveWidget = null;
			this.mSubframeTransition = new WidgetTransition();
			this.mSubframeTransition.SetVisible(false);
			this.AddWidget(this.mSubframeTransition);
			this.mLeaderboard = new Widget();
			this.AddWidget(this.mLeaderboard);
			this.mScroller = new ScrollWidget();
			this.leaderboardList = new LeaderboardListWidget(this.mApp, 0, this, this.mScroller);
			LeaderBoardComm.LoadingCompleted += this.leaderboardList.OnLoadingCompleted;
			this.mScroller.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mScroller.AddWidget(this.leaderboardList);
			this.mLeaderboard.AddWidget(this.mScroller);
		}

		public override void FadeOut(float startSeconds, float endSeconds)
		{
			base.FadeOut(startSeconds, endSeconds);
			this.mScroller.FadeOut(startSeconds, endSeconds);
			this.leaderboardList.FadeOut(startSeconds, endSeconds);
			this.mScoresFrame.FadeOut(startSeconds, endSeconds);
		}

		public override void FadeIn(float startSeconds, float endSeconds)
		{
			this.SetupForCategory(LiveLeaderboardDialog.CurrentLeaderboardState, LiveLeaderboardDialog.CurrentViewState);
			base.FadeIn(startSeconds, endSeconds);
			this.leaderboardList.FadeIn(startSeconds, endSeconds);
			this.mScoresFrame.FadeIn(startSeconds, endSeconds);
			this.mScroller.FadeIn(startSeconds, endSeconds);
		}

		public override void SetOpacity(float opacity)
		{
			base.SetOpacity(opacity);
			this.leaderboardList.SetOpacity(opacity);
			this.mScoresFrame.SetOpacity(opacity);
			this.mScroller.SetOpacity(opacity);
		}

		public override void SetVisible(bool isVisible)
		{
			base.SetVisible(isVisible);
			this.leaderboardList.SetVisible(isVisible);
			this.mScoresFrame.SetVisible(isVisible);
			this.mScroller.SetVisible(isVisible);
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			this.mScroller.SetDisabled(isDisabled);
		}

		public void Layout(int uiStateLayout)
		{
			this.mTabsFrame = new TRect(Constants.mConstants.BUTTON_BAR_OFFSET_X, 0, this.mWidth - Constants.mConstants.BUTTON_BAR_OFFSET_X * 2, Constants.mConstants.BUTTON_BAR_HEIGHT);
			this.mTextFrame.mX = GameApp.DIALOGBOX_CONTENT_INSETS.mLeft;
			this.mTextFrame.mWidth = this.mWidth - GameApp.DIALOGBOX_CONTENT_INSETS.mLeft - GameApp.DIALOGBOX_CONTENT_INSETS.mRight;
			this.mTextFrame.mY = this.mTabsFrame.mY + this.mTabsFrame.mHeight + 8;
			this.mTextFrame.mHeight = this.mHeight - GameApp.DIALOGBOX_CONTENT_INSETS.mBottom - this.mTextFrame.mY;
			TRect leaderboard_Bounds = Constants.mConstants.Leaderboard_Bounds;
			leaderboard_Bounds.mWidth += this.mWidth;
			leaderboard_Bounds.mHeight += this.mHeight;
			if (!this.UseHeading)
			{
				int num = (int)Constants.mConstants.S(70f);
				leaderboard_Bounds.mY -= num;
				leaderboard_Bounds.mHeight += num + (int)Constants.mConstants.S(10f);
			}
			this.mLeaderboard.Resize(leaderboard_Bounds);
			this.leaderboardList.mWidth = this.mLeaderboard.mWidth;
			this.mScroller.Resize(new TRect(0, 0, this.mLeaderboard.mWidth, this.mLeaderboard.mHeight));
			this.mScroller.SetIndicatorsInsets(new Insets(2, 2, 4, 2));
			this.mScroller.FlashIndicators();
			this.mScroller.SetVisible(true);
			this.mScroller.SetColor(0, Constants.SCROLLER_BACK_COLOR);
			this.mScroller.EnableBackgroundFill(true);
			this.mScroller.AddOverlayImage(AtlasResources.IMAGE_LEADERBOARD_LIST_TOP_OV, new CGPoint(0f, 0f));
			this.mScroller.AddOverlayImage(AtlasResources.IMAGE_LEADERBOARD_LIST_BOTTOM_OV, new CGPoint(0f, (float)(this.mScroller.mHeight - AtlasResources.IMAGE_LEADERBOARD_LIST_BOTTOM_OV.GetHeight())));
			this.mCurrentActiveWidget = this.mLeaderboard;
			TRect theRect = new TRect(-Constants.mConstants.Leaderboard_Frame_Bounds.mX, -Constants.mConstants.Leaderboard_Frame_Bounds.mY, this.mWidth + Constants.mConstants.Leaderboard_Frame_Bounds.mWidth, this.mHeight + Constants.mConstants.Leaderboard_Frame_Bounds.mHeight);
			this.mScoresFrame.Resize(theRect);
			this.leaderboardList.CenterOnPlayer();
		}

		public void RefreshList()
		{
			this.leaderboardList.RefreshList();
		}

		public void ClearList()
		{
			this.leaderboardList.Clear();
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			base.DeferOverlay(1);
			if (this.mOpacity == 0f)
			{
				return;
			}
			int theAlpha = (int)(255f * this.mOpacity);
			new SexyColor(255, 255, 255, theAlpha);
			g.SetColorizeImages(true);
			new TRect(0, 0, this.mWidth, this.mHeight);
		}

		public override void DrawOverlay(Graphics g)
		{
			base.DrawOverlay(g);
			if (this.mApp.IsLandscape())
			{
				this.DrawLandscape(g);
				return;
			}
			this.DrawPortrait(g);
		}

		public void RemoveLabel()
		{
			this.mScoresFrame.RemoveLabel();
		}

		public void SetupForCategory(LeaderboardState leaderboardState, LeaderboardViewState viewState)
		{
			switch (leaderboardState)
			{
			case LeaderboardState.Action:
				if (this.UseHeading)
				{
					this.mScoresFrame.SetLabel(Strings.Menu_Play_Action);
				}
				else
				{
					this.mScoresFrame.RemoveLabel();
				}
				this.SetupForViewState(viewState);
				this.leaderboardList.CurrentLeaderboardState = LeaderboardState.Action;
				this.leaderboardList.ViewStateDisplayed = viewState;
				break;
			case LeaderboardState.Classic:
				if (this.UseHeading)
				{
					this.mScoresFrame.SetLabel(Strings.Menu_Play_Classic);
				}
				else
				{
					this.mScoresFrame.RemoveLabel();
				}
				this.SetupForViewState(viewState);
				this.leaderboardList.CurrentLeaderboardState = LeaderboardState.Classic;
				this.leaderboardList.ViewStateDisplayed = viewState;
				break;
			}
			if (viewState == LeaderboardViewState.Personal)
			{
				this.mScoresFrame.RemoveColor();
				this.mScoresFrame.RightLabelImage = AtlasResources.IMAGE_PLAYER_ICON;
			}
			else
			{
				this.mScoresFrame.RemoveColor();
				this.mScoresFrame.RightLabelImage = AtlasResources.IMAGE_PLAYERS_ICON;
			}
			this.leaderboardList.CenterOnPlayer();
		}

		private void SetupForViewState(LeaderboardViewState viewState)
		{
			this.currentViewState = viewState;
			switch (viewState)
			{
			case LeaderboardViewState.Friends:
			case LeaderboardViewState.Personal:
				if (!this.mScroller.mVisible)
				{
					this.mScroller.SetVisible(true);
				}
				return;
			default:
				return;
			}
		}

		protected void DrawLandscape(Graphics g)
		{
			base.Draw(g);
			SexyColor aColor = new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			new SexyColor(0, 255, 0, (int)(255f * this.mOpacity));
			g.SetColorizeImages(true);
			g.SetFont(Resources.FONT_ACHIEVEMENT_NAME);
			int scoresWidget_Y_Scores_Landscape = Constants.mConstants.ScoresWidget_Y_Scores_Landscape;
			g.SetColor(aColor);
			int mHeight = g.GetFont().mHeight;
			g.SetFont(Resources.FONT_ACHIEVEMENT_NAME);
			int num = this.mHeight / 2 + mHeight;
			if (!this.leaderboardList.Connected)
			{
				if (this.leaderboardList.ConnectionPossible)
				{
					this.mAnim++;
					this.mAnim %= AtlasResources.IMAGE_GEM2.GetCelCount();
					g.DrawImageCel(AtlasResources.IMAGE_GEM2, this.mWidth / 2 - AtlasResources.IMAGE_GEM2.GetCelWidth() / 2, num + g.GetFont().StringHeight(this.Connecting), this.mAnim);
					this.WriteCenteredLine(g, num + mHeight, this.Connecting);
				}
				else
				{
					g.WriteWordWrapped(new TRect((int)Constants.mConstants.S(100f), 0, this.mWidth - (int)Constants.mConstants.S(100f) * 2, this.mHeight), this.Cannot_Connect, 0, 0, true);
				}
			}
			if (this.currentViewState == LeaderboardViewState.Personal)
			{
				GameMode selectedGameMode = this.GetSelectedGameMode();
				Profile mProfile = this.mApp.mProfile;
				if (mProfile.GetScoreCount((int)selectedGameMode) == 0)
				{
					this.WriteCenteredLine(g, num, this.NO_HIGH_SCORES);
					return;
				}
			}
			else
			{
				GameMode selectedGameMode2 = this.GetSelectedGameMode();
				if (LeaderBoardComm.LoadResults(selectedGameMode2) == 0)
				{
					this.WriteCenteredLine(g, num, this.NO_HIGH_SCORES);
				}
			}
		}

		private GameMode GetSelectedGameMode()
		{
			switch (LiveLeaderboardDialog.CurrentLeaderboardState)
			{
			case LeaderboardState.Action:
				this.gameMode = GameMode.MODE_TIMED;
				break;
			case LeaderboardState.Classic:
				this.gameMode = GameMode.MODE_CLASSIC;
				break;
			}
			return this.gameMode;
		}

		protected void DrawPortrait(Graphics g)
		{
			base.Draw(g);
			SexyColor aColor = new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			new SexyColor(0, 255, 0, (int)(255f * this.mOpacity));
			g.SetColorizeImages(true);
			g.SetFont(Resources.FONT_ACHIEVEMENT_NAME);
			int scoresWidget_Y_Scores_Portrait = Constants.mConstants.ScoresWidget_Y_Scores_Portrait;
			g.SetColor(aColor);
			int mHeight = g.GetFont().mHeight;
			g.SetFont(Resources.FONT_ACHIEVEMENT_NAME);
			int num = this.mHeight / 2 + mHeight;
			if (!this.leaderboardList.Connected)
			{
				if (this.leaderboardList.ConnectionPossible)
				{
					this.mAnim++;
					this.mAnim %= AtlasResources.IMAGE_GEM2.GetCelCount();
					g.DrawImageCel(AtlasResources.IMAGE_GEM2, this.mWidth / 2 - AtlasResources.IMAGE_GEM2.GetCelWidth() / 2, num + g.GetFont().StringHeight(this.Connecting), this.mAnim);
					this.WriteCenteredLine(g, num + mHeight, this.Connecting);
				}
				else
				{
					g.WriteWordWrapped(new TRect((int)Constants.mConstants.S(100f), 0, this.mWidth - (int)Constants.mConstants.S(100f) * 2, this.mHeight), this.Cannot_Connect, 0, 0, true);
				}
			}
			if (this.currentViewState == LeaderboardViewState.Personal)
			{
				GameMode selectedGameMode = this.GetSelectedGameMode();
				Profile mProfile = this.mApp.mProfile;
				if (mProfile.GetScoreCount((int)selectedGameMode) == 0)
				{
					this.WriteCenteredLine(g, num, this.NO_HIGH_SCORES);
					return;
				}
			}
			else
			{
				GameMode selectedGameMode2 = this.GetSelectedGameMode();
				if (LeaderBoardComm.LoadResults(selectedGameMode2) == 0)
				{
					this.WriteCenteredLine(g, num, this.NO_HIGH_SCORES);
				}
			}
		}

		public void Expand()
		{
			this.mOpacity = 0.5f;
			this.SetDisabled(true);
			base.ExpandDown(0f, 0.5f, 325f);
		}

		public void Collapse()
		{
			this.mOpacity = 1f;
			this.SetDisabled(false);
			base.CollapseUp(0f, 0.5f);
		}

		public void DialogButtonDepress(int dialogId, int buttonId)
		{
		}

		public void DialogButtonPress(int dialogId, int buttonId)
		{
		}

		private const int cannotConnectStringOffset = 100;

		protected GameApp mApp;

		protected string mRank = string.Empty;

		protected TRect mTabsFrame = default(TRect);

		protected TRect mTextFrame = default(TRect);

		private bool mHasFinishedTransition;

		private int mAnim;

		protected ScrollWidget mScroller;

		protected LeaderboardListWidget leaderboardList;

		protected Widget mLeaderboard;

		protected Widget mCurrentActiveWidget;

		protected WidgetTransition mSubframeTransition;

		private FrameWidget mScoresFrame;

		public bool UseHeading = true;

		private LeaderboardViewState currentViewState;

		private string Connecting = Strings.Connecting;

		private string Cannot_Connect = Strings.Cannot_Connect;

		private string NO_HIGH_SCORES = Strings.NO_HIGH_SCORES;

		private GameMode gameMode;

		public enum ButtonID
		{
			LEADERBOARD_LIST_DIALOG_ID
		}
	}
}
