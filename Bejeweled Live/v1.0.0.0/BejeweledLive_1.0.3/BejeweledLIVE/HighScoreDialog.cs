using System;
using Sexy;

namespace BejeweledLIVE
{
	public class HighScoreDialog : InterfaceWidget, ButtonListener
	{
		public HighScoreDialog(GameApp app)
			: base(app)
		{
			this.mScoresWidget = new ScoresWidget(this.mApp);
			this.AddWidget(this.mScoresWidget);
			this.mBackButton = new PodSmallButton(0, 1, "BACK", this);
			this.AddWidget(this.mBackButton);
			this.mResetButton = new PodSmallButton(1, 3, "RESET", this);
			this.AddWidget(this.mResetButton);
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			TRect theRect = new TRect(0, this.mHeight, this.mWidth, 0);
			InterfaceWidget.LayoutWidgetsAbove(this.mBackButton, this.mResetButton, ref theRect);
			this.mScoresWidget.Setup();
			theRect.mHeight = theRect.mY;
			theRect.mY = 0;
			this.mScoresWidget.Resize(theRect);
			this.mScoresWidget.Layout(uiStateLayout);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f, 0.5f);
			this.mResetButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0f, 0.5f);
			this.mScoresWidget.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0.05f, 0.5f);
			base.TransactionTimeSeconds(0.5f);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.mBackButton.FadeOut(0f, 0.5f);
			this.mResetButton.FadeOut(0f, 0.5f);
			this.mScoresWidget.FadeOut(0f, 0.5f);
			base.TransactionTimeSeconds(0.5f);
		}

		public override bool BackButtonPress()
		{
			this.ButtonDepress(0);
			return true;
		}

		public void ButtonDepress(int buttonId)
		{
			switch (buttonId)
			{
			case 0:
				this.mApp.GotoInterfaceState((10 == this.mInterfaceState) ? 4 : 3);
				return;
			case 1:
				this.mApp.ConfirmResetHighScores();
				return;
			default:
				return;
			}
		}

		public void ButtonPress(int theId)
		{
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

		private PodButton mBackButton;

		private PodButton mResetButton;

		private ScoresWidget mScoresWidget;

		private enum ButtonID
		{
			BACK_BUTTON_ID,
			RESET_BUTTON_ID
		}
	}
}
