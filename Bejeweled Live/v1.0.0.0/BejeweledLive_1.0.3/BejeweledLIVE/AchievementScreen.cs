using System;
using Sexy;

namespace BejeweledLIVE
{
	public class AchievementScreen : InterfaceWidget, ButtonListener
	{
		public AchievementScreen(GameApp aApp)
			: base(aApp)
		{
			this.mAchievementFrame = new AchievementFrame(aApp);
			this.AddWidget(this.mAchievementFrame);
			this.mFrame = new FrameWidget(Strings.Menu_Achievements);
			this.AddWidget(this.mFrame);
			this.mBackButton = FancySmallButton.GetNewFancySmallButton(0, 0, Strings.Back, this);
			this.AddWidget(this.mBackButton);
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			TRect theRect = new TRect((int)Constants.mConstants.S(10f), this.mHeight, this.mWidth - (int)Constants.mConstants.S(10f) * 2, 0);
			if (this.mApp.IsLandscape())
			{
				theRect.mWidth = (int)((float)theRect.mWidth * Constants.BUTTON_WIDTH_FACTOR_LANDSCAPE);
			}
			theRect.mX = this.mWidth / 2 - theRect.mWidth / 2;
			InterfaceWidget.LayoutWidgetAbove(this.mBackButton, ref theRect);
			theRect.mHeight = theRect.mY;
			theRect.mY = (int)Constants.mConstants.S(10f);
			theRect.mX = 0;
			theRect.mWidth = this.mWidth;
			this.mFrame.Resize(theRect);
			this.BringToFront(this.mFrame);
			theRect.mY = (int)Constants.mConstants.S(65f);
			theRect.mHeight -= (int)Constants.mConstants.S(55f);
			this.mAchievementFrame.Resize(theRect);
			this.mAchievementFrame.Layout();
			this.BringToBack(this.mAchievementFrame);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			this.mFrame.SetOpacity(1f);
			this.mAchievementFrame.SetOpacity(1f);
			this.mBackButton.SetOpacity(1f);
			this.mFrame.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
			this.mAchievementFrame.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
			this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
			base.TransactionTimeSeconds(0.5f);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
			if (uiState == 22 || uiState == 23)
			{
				this.mApp.PlaySong(SongType.MainMenu, true);
			}
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.mFrame.FadeOut(0f, 0.5f);
			this.mAchievementFrame.FadeOut(0f, 0.45f);
			this.mBackButton.FadeOut(0f, 0.5f);
			base.TransactionTimeSeconds(0.5f);
		}

		public void ButtonMouseMove(int id, int x, int y)
		{
		}

		public void ButtonMouseLeave(int id)
		{
		}

		public void ButtonMouseEnter(int id)
		{
		}

		public void ButtonDownTick(int id)
		{
		}

		public override bool BackButtonPress()
		{
			if (this.mInterfaceState == 23)
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_GAME);
			}
			else
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
			}
			return true;
		}

		public void ButtonDepress(int id)
		{
			if (id != 0)
			{
				return;
			}
			this.BackButtonPress();
		}

		public void ButtonPress(int id)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
		}

		public void ButtonPress(int id, int theClickCount)
		{
			this.ButtonPress(id);
		}

		public AchievementFrame mAchievementFrame;

		public PodButton mBackButton;

		public FrameWidget mFrame;

		public enum ButtonID
		{
			BACK_BUTTON_ID
		}
	}
}
