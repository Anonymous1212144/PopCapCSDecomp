using System;
using Sexy;

namespace BejeweledLIVE
{
	public class PlayMenu : InterfaceWidget, ButtonListener
	{
		public PlayMenu(GameApp theApp)
			: base(theApp)
		{
			this.mLogo = new SparklyLogo();
			this.AddWidget(this.mLogo);
			this.mClassicButton = new PodBigButton(0, BigButtonColors.BIG_BUTTON_GREEN, Constants.mConstants.getString(0), this);
			this.AddWidget(this.mClassicButton);
			this.mActionButton = new PodBigButton(1, BigButtonColors.BIG_BUTTON_PURPLE, "ACTION", this);
			this.AddWidget(this.mActionButton);
			this.mEndlessButton = new PodBigButton(2, BigButtonColors.BIG_BUTTON_ORANGE, "ENDLESS", this);
			this.AddWidget(this.mEndlessButton);
			this.mBackButton = new PodBigButton(3, BigButtonColors.BIG_BUTTON_GREEN, "BACK", this);
			this.AddWidget(this.mBackButton);
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
			this.mLogo.SetupForLayout(uiStateLayout);
			if (1 == uiStateLayout)
			{
				this.mLogo.Move(90, 13);
				this.mClassicButton.MoveCenterTo(122, 128);
				this.mActionButton.MoveCenterTo(122, 210);
				this.mEndlessButton.MoveCenterTo(358, 128);
			}
			else
			{
				this.mLogo.Move(10, 13);
				this.mClassicButton.MoveCenterTo(160, 133);
				this.mActionButton.MoveCenterTo(160, 217);
				this.mEndlessButton.MoveCenterTo(160, 301);
				TRect trect = new TRect(0, 0, this.mWidth, this.mHeight);
				InterfaceWidget.LayoutWidgetAbove(this.mBackButton, ref trect);
			}
			if (SexyAppBase.IsInTrialMode)
			{
				this.mClassicButton.SetDisabled(true);
				this.mActionButton.SetDisabled(true);
				this.mEndlessButton.SetDisabled(false);
				this.mClassicButton.FadeWhenDisabled = (this.mActionButton.FadeWhenDisabled = true);
				return;
			}
			this.mClassicButton.SetDisabled(false);
			this.mActionButton.SetDisabled(false);
			this.mEndlessButton.SetDisabled(false);
			this.mClassicButton.FadeWhenDisabled = (this.mActionButton.FadeWhenDisabled = false);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			if (1 == uiStateLayout)
			{
				this.mLogo.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mClassicButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.075f, 0.575f);
				this.mActionButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.125f, 0.625f);
				this.mEndlessButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.075f, 0.575f);
				this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.075f, 0.575f);
			}
			else
			{
				this.mLogo.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mClassicButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				this.mActionButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.075f, 0.575f);
				this.mEndlessButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.125f, 0.625f);
				this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.075f, 0.575f);
			}
			base.TransactionTimeSeconds(0.9f);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void SetDisabled(bool disabled)
		{
			base.SetDisabled(disabled);
			if (SexyAppBase.IsInTrialMode)
			{
				this.mClassicButton.SetDisabled(true);
				this.mActionButton.SetDisabled(true);
			}
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			float startSeconds = 0f;
			float num = 0.25f;
			this.mLogo.FadeOut(startSeconds, num);
			this.mClassicButton.FadeOut(startSeconds, num);
			this.mActionButton.FadeOut(startSeconds, num);
			this.mEndlessButton.FadeOut(startSeconds, num);
			this.mBackButton.FadeOut(startSeconds, num);
			base.TransactionTimeSeconds(num);
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonDepress(int buttonId)
		{
			switch (buttonId)
			{
			case 0:
				this.mApp.mGameMode = GameMode.MODE_CLASSIC;
				this.mApp.NewGame();
				return;
			case 1:
				this.mApp.mGameMode = GameMode.MODE_TIMED;
				this.mApp.NewGame();
				return;
			case 2:
				this.mApp.mGameMode = GameMode.MODE_ENDLESS;
				this.mApp.NewGame();
				return;
			case 3:
				this.mApp.GotoInterfaceState(2);
				return;
			default:
				return;
			}
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

		protected SparklyLogo mLogo;

		protected PodButton mClassicButton;

		protected PodButton mActionButton;

		protected PodButton mEndlessButton;

		protected PodButton mBackButton;

		protected enum ButtonIds
		{
			CLASSIC_BUTTON_ID,
			ACTION_BUTTON_ID,
			ENDLESS_BUTTON_ID,
			BACK_BUTTON_ID,
			BUTTON_COUNT
		}

		protected enum LayoutIndex
		{
			LOGO_POS,
			STARSHINE_POS
		}
	}
}
