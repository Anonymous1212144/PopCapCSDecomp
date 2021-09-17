using System;
using Sexy;

namespace BejeweledLIVE
{
	public class PauseMenu : InterfaceWidget, ButtonListener
	{
		public PauseMenu(GameApp theApp)
			: base(theApp)
		{
			this.mContinueButton = new PodBigButton(0, BigButtonColors.BIG_BUTTON_GREEN, "RESUME", this);
			this.AddWidget(this.mContinueButton);
			this.mAchievementsButton = new PodBigButton(1, BigButtonColors.BIG_BUTTON_PURPLE, "ACHIEVEMENTS", this);
			this.AddWidget(this.mAchievementsButton);
			this.mLeaderboardsButton = new PodBigButton(2, BigButtonColors.BIG_BUTTON_ORANGE, "LEADERBOARDS", this);
			this.AddWidget(this.mLeaderboardsButton);
			this.mHelpOptionsButton = new PodBigButton(3, BigButtonColors.BIG_BUTTON_BLUE, "HELP & OPTIONS", this);
			this.AddWidget(this.mHelpOptionsButton);
			this.mUnlockFullGameButton = new PodBigButton(4, BigButtonColors.BIG_BUTTON_YELLOW, "UNLOCK FULL GAME", this);
			this.AddWidget(this.mUnlockFullGameButton);
			this.mMainMenuButton = new PodBigButton(5, BigButtonColors.BIG_BUTTON_RED, "MAIN MENU", this);
			this.AddWidget(this.mMainMenuButton);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			if (1 == uiStateLayout)
			{
				this.mContinueButton.MoveCenterTo(122, 128);
				this.mAchievementsButton.MoveCenterTo(122, 210);
				this.mLeaderboardsButton.MoveCenterTo(358, 128);
			}
			else
			{
				int mainMenu_ButtonDistance_Y_Portrait = Constants.mConstants.MainMenu_ButtonDistance_Y_Portrait;
				int num = Constants.mConstants.MainMenu_ButtonStartPos_Y_TrialMode_Portrait;
				int cx = this.mWidth / 2;
				this.mContinueButton.MoveCenterTo(cx, num);
				num += mainMenu_ButtonDistance_Y_Portrait;
				this.mAchievementsButton.MoveCenterTo(cx, num);
				num += mainMenu_ButtonDistance_Y_Portrait;
				this.mLeaderboardsButton.MoveCenterTo(cx, num);
				num += mainMenu_ButtonDistance_Y_Portrait;
				this.mHelpOptionsButton.MoveCenterTo(cx, num);
				num += mainMenu_ButtonDistance_Y_Portrait;
				this.mUnlockFullGameButton.MoveCenterTo(cx, num);
				num += mainMenu_ButtonDistance_Y_Portrait;
				this.mMainMenuButton.MoveCenterTo(cx, num);
			}
			if (SexyAppBase.IsInTrialMode)
			{
				this.mUnlockFullGameButton.SetVisible(true);
				return;
			}
			this.mUnlockFullGameButton.SetVisible(false);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			if (1 == uiStateLayout)
			{
				this.mContinueButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.075f, 0.575f);
				this.mAchievementsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.125f, 0.625f);
				this.mLeaderboardsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.075f, 0.575f);
			}
			else
			{
				this.mContinueButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				this.mAchievementsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.075f, 0.575f);
				this.mLeaderboardsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.125f, 0.625f);
				this.mHelpOptionsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.125f, 0.625f);
				this.mUnlockFullGameButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.125f, 0.625f);
				this.mMainMenuButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.125f, 0.625f);
			}
			base.TransactionTimeSeconds(0.9f);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			float startSeconds = 0f;
			float num = 0.25f;
			this.mContinueButton.FadeOut(startSeconds, num);
			this.mAchievementsButton.FadeOut(startSeconds, num);
			this.mLeaderboardsButton.FadeOut(startSeconds, num);
			this.mHelpOptionsButton.FadeOut(startSeconds, num);
			this.mUnlockFullGameButton.FadeOut(startSeconds, num);
			this.mMainMenuButton.FadeOut(startSeconds, num);
			base.TransactionTimeSeconds(num);
		}

		public void ButtonDepress(int buttonId)
		{
			switch (buttonId)
			{
			case 0:
			case 2:
				break;
			case 1:
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_ACHIEVEMENTS);
				return;
			case 3:
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_INFO);
				return;
			case 4:
				this.mApp.BuyGame();
				return;
			case 5:
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MORE_GAMES);
				break;
			default:
				return;
			}
		}

		public override void Update()
		{
			base.Update();
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
		}

		public void ButtonPress(int theId)
		{
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

		protected PodButton mContinueButton;

		protected PodButton mAchievementsButton;

		protected PodButton mLeaderboardsButton;

		protected PodButton mHelpOptionsButton;

		protected PodButton mUnlockFullGameButton;

		protected PodButton mMainMenuButton;

		protected enum ButtonIds
		{
			CONTINUE_BUTTON_ID,
			ACHIEVEMENTS_BUTTON_ID,
			LEADERBOARDS_BUTTON_ID,
			HELP_OPTIONS_BUTTON_ID,
			UNLOCK_FULL_GAME_BUTTON_ID,
			MAIN_MENU_BUTTON_ID
		}
	}
}
