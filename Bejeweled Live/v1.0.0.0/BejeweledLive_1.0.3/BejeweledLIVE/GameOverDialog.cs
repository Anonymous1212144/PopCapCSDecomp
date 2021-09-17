using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class GameOverDialog : InterfaceWidget, ButtonListener
	{
		public GameOverDialog(GameApp aApp)
			: base(aApp)
		{
			this.mHeading = new LabelWidget(Strings.Final_Score, Resources.FONT_HUGE);
			this.mHeading.SetJustification(0);
			this.mHeading.SizeToFit();
			this.AddWidget(this.mHeading);
			this.mScore = new LabelWidget("", Resources.FONT_HUGE);
			this.mScore.SetJustification(0);
			this.mScore.SizeToFit();
			this.AddWidget(this.mScore);
			this.mLabelsWidth = 0;
			Font font_ACHIEVEMENT_NAME = Resources.FONT_ACHIEVEMENT_NAME;
			int i;
			for (i = 0; i < GameOverDialog.STAT_NAMES.Length; i++)
			{
				this.mLabels[i] = new LabelWidget(GameOverDialog.STAT_NAMES[i], font_ACHIEVEMENT_NAME);
				this.mLabels[i].SetJustification(-1);
				this.mLabelsWidth = Math.Max(this.mLabelsWidth, font_ACHIEVEMENT_NAME.StringWidth(GameOverDialog.STAT_NAMES[i]));
			}
			while (i < this.mLabels.Length)
			{
				this.mLabels[i] = new LabelWidget("", font_ACHIEVEMENT_NAME);
				this.mLabels[i].SetJustification(1);
				i++;
			}
			for (i = 0; i < this.mLabels.Length; i++)
			{
				this.mLabels[i].SetColor(0, SexyColor.White);
				this.mLabels[i].SizeToFit();
				this.AddWidget(this.mLabels[i]);
			}
			this.mScoresWidget = new LiveLeaderboard(this.mApp);
			this.mScoresWidget.UseHeading = false;
			this.mScoresWidget.SetupForCategory(LeaderboardState.Action, LeaderboardViewState.Friends);
			this.AddWidget(this.mScoresWidget);
			this.mPlayButton = FancySmallButton.GetNewFancySmallButton(0, 2, 0, Strings.Menu_Play_Again, this);
			this.AddWidget(this.mPlayButton);
			this.mLeaderboardButton = FancySmallButton.GetNewFancySmallButton(2, 2, 1, Strings.Menu_Leaderboards, this);
			this.AddWidget(this.mLeaderboardButton);
			this.mMenuButton = FancySmallButton.GetNewFancySmallButton(1, 2, 2, Strings.Menu_Main_Menu, this);
			this.AddWidget(this.mMenuButton);
			this.mContinueButton = FancySmallButton.GetNewFancySmallButton(3, 0, 0, Strings.Continue, this);
			this.AddWidget(this.mContinueButton);
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			g.SetColor(new Color(0, 0, 0, (int)(100f * this.mOpacity)));
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			base.Draw(g);
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			if (SexyAppBase.IsInTrialMode)
			{
				this.mLabels[4].mText = Common.StrFormat_("{0:00}:{1:00}", new object[] { 1, 30 });
			}
			else
			{
				this.mLabels[4].mText = Common.StrFormat_("{0:00}:{1:00}", new object[]
				{
					this.mApp.mBoard.mTotalGameTime / 60,
					this.mApp.mBoard.mTotalGameTime % 60
				});
			}
			this.mLabels[5].mText = GlobalStaticVars.CommaSeperate_(this.mApp.mBoard.mGemsCleared);
			this.mLabels[6].mText = GlobalStaticVars.CommaSeperate_(this.mApp.mBoard.mLongestChainReaction);
			this.mLabels[7].mText = GlobalStaticVars.CommaSeperate_(this.mApp.mBoard.mHighestScoringMove);
			int gameOverDialog_Heading_Y = Constants.mConstants.GameOverDialog_Heading_Y;
			TRect trect = new TRect((int)Constants.mConstants.S(20f), gameOverDialog_Heading_Y, this.mWidth - (int)Constants.mConstants.S(20f) * 2, 0);
			TRect trect2 = default(TRect);
			TRect trect3 = new TRect((int)Constants.mConstants.S(10f), this.mHeight - (int)Constants.mConstants.S(10f), this.mWidth - (int)Constants.mConstants.S(20f), 0);
			LiveLeaderboardDialog.CurrentLeaderboardState = ((this.mApp.mGameMode == GameMode.MODE_TIMED) ? LeaderboardState.Action : LeaderboardState.Classic);
			LiveLeaderboardDialog.CurrentViewState = LeaderboardViewState.Friends;
			LiveLeaderboardDialog.PrepareState(LiveLeaderboardDialog.CurrentLeaderboardState, LiveLeaderboardDialog.CurrentViewState);
			this.mScoresWidget.SetupForCategory(LiveLeaderboardDialog.CurrentLeaderboardState, LiveLeaderboardDialog.CurrentViewState);
			if (this.mApp.IsLandscape())
			{
				trect.mWidth = this.mWidth / 2 - (int)Constants.mConstants.S(60f);
				this.mHeading.SetJustification(0);
				InterfaceWidget.LayoutWidgetBelow(this.mHeading, ref trect);
				this.mHeading.SetScale(1f);
				float num = (float)this.mHeading.mFont.StringWidth(this.mHeading.mText);
				if (num > (float)trect.mWidth)
				{
					this.mHeading.SetScale((float)trect.mWidth / num);
				}
				this.mScore.mText = GlobalStaticVars.CommaSeperate_(this.mApp.mBoard.mPoints);
				InterfaceWidget.LayoutWidgetBelow(this.mScore, ref trect);
				this.mScore.SetScale(1f);
				num = (float)this.mScore.mFont.StringWidth(this.mScore.mText);
				if (num > (float)trect.mWidth)
				{
					this.mScore.SetScale((float)trect.mWidth / num);
				}
				trect.mY = Constants.mConstants.GameOverDialog_Text_Y;
				int i = 0;
				int num2 = this.mLabels.Length / 2;
				while (i < num2)
				{
					InterfaceWidget.LayoutWidgetsBelow(this.mLabels[i], this.mLabelsWidth, this.mLabels[num2 + i], ref trect);
					i++;
				}
				if (SexyAppBase.IsInTrialMode)
				{
					this.mMenuButton.SetVisible(false);
					this.mPlayButton.SetVisible(false);
					this.mLeaderboardButton.SetVisible(false);
					this.mContinueButton.SetVisible(true);
					InterfaceWidget.LayoutWidgetAbove(this.mContinueButton, ref trect3);
				}
				else
				{
					this.mContinueButton.SetVisible(false);
					this.mMenuButton.SetVisible(true);
					this.mPlayButton.SetVisible(true);
					this.mLeaderboardButton.SetVisible(true);
					InterfaceWidget.LayoutWidgetsAbove(this.mMenuButton, this.mLeaderboardButton, ref trect3);
					trect3.mWidth = this.mMenuButton.mWidth;
					trect3.mX = this.mWidth / 2 - trect3.mWidth / 2;
					InterfaceWidget.LayoutWidgetAbove(this.mPlayButton, ref trect3);
				}
				trect.mX = this.mWidth / 2 - (int)Constants.mConstants.S(40f);
				trect.mY = 0;
				trect.mHeight = trect3.mY;
				trect.mWidth = this.mWidth - trect.mX - (int)Constants.mConstants.S(10f);
				TRect theRect = new TRect(trect.mX, trect.mY, trect.mWidth, trect3.mY - trect.mY);
				this.mScoresWidget.Resize(theRect);
				this.mScoresWidget.Layout(uiStateLayout);
				return;
			}
			this.mHeading.SetJustification(0);
			InterfaceWidget.LayoutWidgetBelow(this.mHeading, ref trect);
			this.mHeading.SetScale(1f);
			float num3 = (float)this.mHeading.mFont.StringWidth(this.mHeading.mText);
			if (num3 > (float)trect.mWidth)
			{
				this.mHeading.SetScale((float)trect.mWidth / num3);
			}
			this.mScore.mText = GlobalStaticVars.CommaSeperate_(this.mApp.mBoard.mPoints);
			InterfaceWidget.LayoutWidgetBelow(this.mScore, ref trect);
			this.mScore.SetScale(1f);
			num3 = (float)this.mScore.mFont.StringWidth(this.mScore.mText);
			if (num3 > (float)trect.mWidth)
			{
				this.mScore.SetScale((float)trect.mWidth / num3);
			}
			trect2.mX = trect.mX + GameApp.DIALOGBOX_CONTENT_INSETS.mLeft;
			trect2.mWidth = trect.mWidth - trect2.mX;
			trect2.mY = Constants.mConstants.GameOverDialog_Text_Y;
			trect2.mHeight = 0;
			int j = 0;
			int num4 = this.mLabels.Length / 2;
			while (j < num4)
			{
				InterfaceWidget.LayoutWidgetsBelow(this.mLabels[j], this.mLabelsWidth, this.mLabels[num4 + j], ref trect2);
				j++;
			}
			if (SexyAppBase.IsInTrialMode)
			{
				this.mMenuButton.SetVisible(false);
				this.mPlayButton.SetVisible(false);
				this.mLeaderboardButton.SetVisible(false);
				this.mContinueButton.SetVisible(true);
				InterfaceWidget.LayoutWidgetAbove(this.mContinueButton, ref trect3);
			}
			else
			{
				this.mContinueButton.SetVisible(false);
				this.mMenuButton.SetVisible(true);
				this.mPlayButton.SetVisible(true);
				this.mLeaderboardButton.SetVisible(true);
				InterfaceWidget.LayoutWidgetAbove(this.mMenuButton, ref trect3);
				InterfaceWidget.LayoutWidgetAbove(this.mLeaderboardButton, ref trect3);
				InterfaceWidget.LayoutWidgetAbove(this.mPlayButton, ref trect3);
			}
			int num5 = trect.mY + (int)Constants.mConstants.S(70f);
			TRect theRect2 = new TRect(trect.mX, num5, trect.mWidth, trect3.mY - num5);
			this.mScoresWidget.Resize(theRect2);
			this.mScoresWidget.Layout(uiStateLayout);
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
			if (uiState == 15)
			{
				this.mApp.PlaySong(SongType.GameOver, true);
				this.mScoresWidget.RefreshList();
			}
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			int i = 0;
			int num = this.mLabels.Length / 2;
			while (i < num)
			{
				float num2 = (float)i * 0.1f;
				float endSeconds = num2 + 0.3f;
				this.mLabels[i].SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, num2, endSeconds);
				this.mLabels[num + i].SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, num2, endSeconds);
				i++;
			}
			this.mScoresWidget.FadeIn(0f, 0.5f);
			this.mHeading.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.3f);
			this.mScore.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.3f);
			if (SexyAppBase.IsInTrialMode)
			{
				this.mContinueButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
			}
			else
			{
				this.mPlayButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.4f);
				this.mMenuButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.1f, 0.5f);
				this.mLeaderboardButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.1f, 0.5f);
			}
			this.mScoresWidget.ClearList();
			this.FadeIn(0f, 0.5f);
			base.TransactionTimeSeconds(0.8f);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			float startSeconds = 0f;
			float num = 0.5f;
			for (int i = 0; i < this.mLabels.Length; i++)
			{
				this.mLabels[i].FadeOut(startSeconds, num);
			}
			this.mScoresWidget.FadeOut(startSeconds, num);
			this.mHeading.FadeOut(startSeconds, num);
			this.mScore.FadeOut(startSeconds, num);
			this.mPlayButton.FadeOut(startSeconds, num);
			this.mMenuButton.FadeOut(startSeconds, num);
			this.mContinueButton.FadeOut(startSeconds, num);
			this.mLeaderboardButton.FadeOut(startSeconds, num);
			this.FadeOut(startSeconds, num);
			base.TransactionTimeSeconds(num);
			GameApp.LoadInitialLevelBackdrops();
		}

		public void ButtonDepress(int buttonId)
		{
			switch (buttonId)
			{
			case 0:
				this.mApp.DoNewGame();
				return;
			case 1:
				this.mApp.GotoInterfaceState(2);
				return;
			case 2:
				LiveLeaderboardDialog.PrepareState((this.mApp.mGameMode == GameMode.MODE_CLASSIC) ? LeaderboardState.Classic : LeaderboardState.Action, LeaderboardViewState.Friends);
				this.mApp.GotoInterfaceState(26);
				return;
			case 3:
				this.mApp.GotoInterfaceState(19);
				return;
			default:
				return;
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
			if (SexyAppBase.IsInTrialMode)
			{
				this.ButtonDepress(3);
			}
			else
			{
				this.ButtonDepress(1);
			}
			return true;
		}

		private static readonly string[] STAT_NAMES = new string[]
		{
			Strings.GameOverDialog_STAT_NAMES_Game_Duration,
			Strings.GameOverDialog_STAT_NAMES_Gems_Collected,
			Strings.GameOverDialog_STAT_NAMES_Biggest_Cascade,
			Strings.GameOverDialog_STAT_NAMES_Biggest_Combo
		};

		private LabelWidget mHeading;

		private LabelWidget mScore;

		protected LabelWidget[] mLabels = new LabelWidget[GameOverDialog.STAT_NAMES.Length * 2];

		private LiveLeaderboard mScoresWidget;

		private PodButton mPlayButton;

		private PodButton mLeaderboardButton;

		private PodButton mMenuButton;

		private PodButton mContinueButton;

		private int mLabelsWidth;

		public enum ButtonID
		{
			PLAY_BUTTON_ID,
			MENU_BUTTON_ID,
			SCORES_BUTTON_ID,
			TRIAL_CONTINUE_BUTTON_ID
		}
	}
}
