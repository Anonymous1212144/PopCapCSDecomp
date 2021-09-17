using System;
using Sexy;

namespace BejeweledLIVE
{
	public class MainMenu : InterfaceWidget, ButtonListener, DialogListener
	{
		private int buttonCount
		{
			get
			{
				if (!SexyAppBase.IsInTrialMode)
				{
					return 3;
				}
				return 4;
			}
		}

		public MainMenu(GameApp theApp)
			: base(theApp)
		{
			this.mLogo = new SparklyLogo();
			this.AddWidget(this.mLogo);
			this.mPlayButton = new FancyBigButton(0, this.buttonCount, 0, Strings.Menu_Play, this);
			this.AddWidget(this.mPlayButton);
			this.mUnlockFullGameButton = new FancyBigButton(4, this.buttonCount, 1, Strings.Unlock_Full_Game, this);
			this.AddWidget(this.mUnlockFullGameButton);
			this.mAchievementsButton = new FancyBigButton(1, this.buttonCount, 2, Strings.Menu_Achievements, this);
			this.AddWidget(this.mAchievementsButton);
			this.mLeaderboardsButton = new FancyBigButton(2, this.buttonCount, 3, Strings.Menu_Leaderboards, this);
			this.AddWidget(this.mLeaderboardsButton);
			this.mHelpOptionsButton = new FancyBigButton(3, this.buttonCount, 4, Strings.Menu_Help_Options, this);
			this.AddWidget(this.mHelpOptionsButton);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override bool BackButtonPress()
		{
			if (this.actionSheet == null)
			{
				this.mApp.AppExit();
			}
			else
			{
				this.actionSheet.BackButtonPress();
			}
			return true;
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			this.mLogo.SetupForLayout(uiStateLayout);
			int num = (SexyAppBase.IsInTrialMode ? Constants.mConstants.MainMenu_ButtonDistance_TrialMode : Constants.mConstants.MainMenu_ButtonDistance_NotTrialMode);
			int mWidth = (int)Constants.mConstants.S(500f);
			this.mPlayButton.mWidth = mWidth;
			this.mAchievementsButton.mWidth = mWidth;
			this.mLeaderboardsButton.mWidth = mWidth;
			this.mHelpOptionsButton.mWidth = mWidth;
			this.mUnlockFullGameButton.mWidth = mWidth;
			if (1 == uiStateLayout)
			{
				int buttonCount = (SexyAppBase.IsInTrialMode ? 2 : 1);
				this.mPlayButton.ButtonCount = buttonCount;
				this.mAchievementsButton.ButtonCount = buttonCount;
				this.mLeaderboardsButton.ButtonCount = buttonCount;
				this.mHelpOptionsButton.ButtonCount = buttonCount;
				this.mUnlockFullGameButton.ButtonCount = buttonCount;
				int buttonNumber = 0;
				this.mPlayButton.ButtonNumber = buttonNumber;
				this.mLeaderboardsButton.ButtonNumber = buttonNumber++;
				this.mAchievementsButton.ButtonNumber = buttonNumber;
				this.mHelpOptionsButton.ButtonNumber = buttonNumber++;
				if (SexyAppBase.IsInTrialMode)
				{
					this.mUnlockFullGameButton.ButtonNumber = buttonNumber++;
				}
				this.mPlayButton.mIsFancy = (this.mLeaderboardsButton.mIsFancy = true);
				this.mLogo.Move(this.mWidth / 2 - this.mLogo.mWidth / 2, Constants.mConstants.MainMenu_SparklyLogo_Y_Landscape);
				int num2 = (int)Constants.mConstants.S(10f);
				int theNewX = num2;
				int num3 = (SexyAppBase.IsInTrialMode ? Constants.mConstants.MainMenu_ButtonStartPos_Y_TrialMode_Landscape : Constants.mConstants.MainMenu_ButtonStartPos_Y_NotTrialMode_Landscape);
				this.mPlayButton.Move(theNewX, num3);
				num3 += this.mPlayButton.mHeight;
				num3 += num;
				this.mAchievementsButton.Move(theNewX, num3);
				theNewX = this.mWidth - num2 - this.mPlayButton.mWidth;
				num3 = (SexyAppBase.IsInTrialMode ? Constants.mConstants.MainMenu_ButtonStartPos_Y_TrialMode_Landscape : Constants.mConstants.MainMenu_ButtonStartPos_Y_NotTrialMode_Landscape);
				this.mLeaderboardsButton.Move(theNewX, num3);
				num3 += this.mPlayButton.mHeight;
				num3 += num;
				this.mHelpOptionsButton.Move(theNewX, num3);
				num3 += this.mPlayButton.mHeight;
				num3 += num;
				this.mUnlockFullGameButton.MoveCenterTo(this.mWidth / 2, num3 + this.mUnlockFullGameButton.mHeight / 2);
				if (SexyAppBase.IsInTrialMode)
				{
					this.mUnlockFullGameButton.SetVisible(true);
					this.mUnlockFullGameButton.SetDisabled(false);
					num3 += this.mPlayButton.mHeight;
					num3 += num;
				}
				else
				{
					this.mUnlockFullGameButton.SetVisible(false);
					this.mUnlockFullGameButton.SetDisabled(true);
				}
				this.mAchievementsButton.mIsFancy = (this.mHelpOptionsButton.mIsFancy = (this.mUnlockFullGameButton.mIsFancy = true));
				return;
			}
			this.mPlayButton.ButtonCount = this.buttonCount;
			this.mAchievementsButton.ButtonCount = this.buttonCount;
			this.mLeaderboardsButton.ButtonCount = this.buttonCount;
			this.mHelpOptionsButton.ButtonCount = this.buttonCount;
			this.mUnlockFullGameButton.ButtonCount = this.buttonCount;
			int num4 = 0;
			this.mPlayButton.ButtonNumber = num4++;
			if (SexyAppBase.IsInTrialMode)
			{
				this.mUnlockFullGameButton.ButtonNumber = num4++;
			}
			this.mAchievementsButton.ButtonNumber = num4++;
			this.mLeaderboardsButton.ButtonNumber = num4++;
			this.mHelpOptionsButton.ButtonNumber = num4++;
			this.mLogo.Move(this.mWidth / 2 - this.mLogo.mWidth / 2, Constants.mConstants.MainMenu_SparklyLogo_Y_Portrait);
			int num5 = (SexyAppBase.IsInTrialMode ? Constants.mConstants.MainMenu_ButtonStartPos_Y_TrialMode_Portrait : Constants.mConstants.MainMenu_ButtonStartPos_Y_NotTrialMode_Portrait);
			int cx = this.mWidth / 2;
			this.mPlayButton.MoveCenterTo(cx, num5);
			num5 += this.mPlayButton.mHeight;
			num5 += num;
			this.mUnlockFullGameButton.MoveCenterTo(cx, num5);
			if (SexyAppBase.IsInTrialMode)
			{
				this.mUnlockFullGameButton.SetVisible(true);
				this.mUnlockFullGameButton.SetDisabled(false);
				num5 += this.mUnlockFullGameButton.mHeight;
				num5 += num;
			}
			else
			{
				this.mUnlockFullGameButton.SetVisible(false);
				this.mUnlockFullGameButton.SetDisabled(true);
			}
			this.mAchievementsButton.MoveCenterTo(cx, num5);
			num5 += this.mAchievementsButton.mHeight;
			num5 += num;
			this.mLeaderboardsButton.MoveCenterTo(cx, num5);
			num5 += this.mLeaderboardsButton.mHeight;
			num5 += num;
			this.mHelpOptionsButton.MoveCenterTo(cx, num5);
			num5 += this.mHelpOptionsButton.mHeight;
			num5 += num;
			this.mPlayButton.mIsFancy = true;
			this.mHelpOptionsButton.mIsFancy = true;
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			this.fadedButtons = false;
			float num = 0f;
			if (this.isFirstTransition)
			{
				this.isFirstTransition = false;
				num = 0.5f;
			}
			if (1 == uiStateLayout)
			{
				this.mLogo.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f + num, 0.5f + num);
				this.mPlayButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f + num, 0.5f + num);
				if (SexyAppBase.IsInTrialMode)
				{
					this.mUnlockFullGameButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.5f + num, 0.9f + num);
				}
				this.mAchievementsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.3f + num, 0.7f + num);
				this.mLeaderboardsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0f + num, 0.5f + num);
				this.mHelpOptionsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.3f + num, 0.7f + num);
			}
			else
			{
				this.mLogo.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f + num, 0.5f + num);
				this.mPlayButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f + num, 0.5f + num);
				if (SexyAppBase.IsInTrialMode)
				{
					this.mUnlockFullGameButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.05f + num, 0.55f + num);
				}
				this.mAchievementsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.075f + num, 0.575f + num);
				this.mLeaderboardsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.125f + num, 0.625f + num);
				this.mHelpOptionsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.125f + num, 0.625f + num);
			}
			base.TransactionTimeSeconds(0.9f + num);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			float startSeconds = 0f;
			float num = 0.25f;
			this.mLogo.FadeOut(startSeconds, num);
			if (!this.fadedButtons)
			{
				this.mPlayButton.FadeOut(startSeconds, num);
				this.mUnlockFullGameButton.FadeOut(startSeconds, num);
				this.mAchievementsButton.FadeOut(startSeconds, num);
				this.mLeaderboardsButton.FadeOut(startSeconds, num);
				this.mHelpOptionsButton.FadeOut(startSeconds, num);
			}
			base.TransactionTimeSeconds(num);
		}

		public void DialogButtonDepress(int dialogId, int buttonId)
		{
			this.actionSheet = null;
			if (dialogId != 40)
			{
				this.mApp.PlaySong(SongType.MainMenu, true);
				return;
			}
			switch (buttonId)
			{
			case 6:
				this.mApp.mGameMode = GameMode.MODE_CLASSIC;
				this.mApp.NewGame();
				return;
			case 7:
				this.mApp.mGameMode = GameMode.MODE_TIMED;
				this.mApp.NewGame();
				return;
			case 8:
				this.mApp.mGameMode = GameMode.MODE_ENDLESS;
				this.mApp.NewGame();
				return;
			default:
				if (buttonId != 1003)
				{
					this.mApp.PlaySong(SongType.MainMenu, true);
					return;
				}
				this.mApp.PlaySong(SongType.MainMenu, true);
				return;
			}
		}

		public void ButtonDepress(int buttonId)
		{
			switch (buttonId)
			{
			case 0:
				this.ShowPlayDialog();
				return;
			case 1:
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_ACHIEVEMENTS);
				return;
			case 2:
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_SCORES);
				return;
			case 3:
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_INFO);
				return;
			case 4:
				this.mApp.BuyGame();
				return;
			case 5:
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MORE_GAMES);
				return;
			default:
				return;
			}
		}

		private void ShowPlayDialog()
		{
			this.actionSheet = ActionSheet.GetNewActionSheet(true, 40, this, this.mApp, Strings.Menu_Play);
			this.actionSheet.AddButton(6, Strings.Menu_Play_Classic, SmallButtonColors.SMALL_BUTTON_GREEN);
			this.actionSheet.AddButton(7, Strings.Menu_Play_Action, SmallButtonColors.SMALL_BUTTON_PURPLE, !SexyAppBase.IsInTrialMode, SexyAppBase.IsInTrialMode);
			this.actionSheet.AddButton(8, Strings.Menu_Play_Endless, SmallButtonColors.SMALL_BUTTON_RED, !SexyAppBase.IsInTrialMode, SexyAppBase.IsInTrialMode);
			this.actionSheet.SetCancelButton(1003, Strings.Back);
			this.actionSheet.Present();
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
			if (uiState == 2)
			{
				this.mApp.PlaySong(SongType.MainMenu, true);
			}
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public void ButtonPress(int theId, int theClickCount)
		{
			this.ButtonPress(theId);
		}

		public void DialogButtonPress(int theDialogId, int theButtonId)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
		}

		public void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
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

		protected SparklyLogo mLogo;

		protected FancyPodButton mPlayButton;

		protected FancyPodButton mAchievementsButton;

		protected FancyPodButton mLeaderboardsButton;

		protected FancyPodButton mHelpOptionsButton;

		protected FancyPodButton mUnlockFullGameButton;

		private ActionSheet actionSheet;

		private bool isFirstTransition = true;

		private bool fadedButtons;

		protected enum ButtonIds
		{
			PLAY_BUTTON_ID,
			ACHIEVEMENTS_BUTTON_ID,
			LEADERBOARDS_BUTTON_ID,
			HELP_OPTIONS_BUTTON_ID,
			UNLOCK_FULL_GAME_BUTTON_ID,
			MORE_GAMES_BUTTON_ID,
			CLASSIC_BUTTON_ID,
			ACTION_BUTTON_ID,
			ENDLESS_BUTTON_ID
		}

		protected enum LayoutIndex
		{
			LOGO_POS,
			STARSHINE_POS
		}
	}
}
