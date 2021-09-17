using System;
using Sexy;

namespace BejeweledLIVE
{
	public class MoreGamesDialog : InterfaceWidget, ButtonListener, DialogListener
	{
		public MoreGamesDialog(GameApp app)
			: base(app)
		{
			this.mFancyText = new FancyTextWidget();
			this.AddWidget(this.mFancyText);
			this.mList = new MoreGamesListWidget(this.mApp, 0, this);
			this.mList.Resize(0, 0, 320, 0);
			this.LoadEmUp();
			this.mScroller = new ScrollWidget();
			this.mScroller.EnableIndicators(AtlasResources.IMAGE_SCROLL_INDICATOR);
			this.mScroller.AddWidget(this.mList);
			this.mScroller.mClip = true;
			this.AddWidget(this.mScroller);
			this.mBackButton = new PodSmallButton(1, 1, Strings.Back, this);
			this.AddWidget(this.mBackButton);
			this.mScrollTransition = new WidgetTransition();
			this.mScrollTransition.SetVisible(false);
			this.AddWidget(this.mScrollTransition);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			Font font_TEXT = Resources.FONT_TEXT;
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			if (1 == uiStateLayout)
			{
				this.mFancyText.Resize(0, 0, 160, 200);
				this.mBackButton.Resize(0, 0, Constants.mConstants.BackButton_Width, this.mBackButton.Height());
				this.mBackButton.MoveCenterTo(Constants.mConstants.MoreGamesDialog_BackButton_X_Landscape, Constants.mConstants.MoreGamesDialog_BackButton_Y_Landscape);
				this.mScroller.Resize(Constants.mConstants.MoreGamesDialog_Scroller_Size_LandScape / 2, 0, Constants.mConstants.MoreGamesDialog_Scroller_Size_LandScape, Constants.mConstants.MoreGamesDialog_Scroller_Size_LandScape);
				this.mScroller.SetScrollInsets(new Insets(0, 0, 0, 0));
				this.mScroller.EnableOverlays(false);
			}
			else
			{
				this.mFancyText.Resize(0, 0, this.mWidth, 3 * font_TEXT.GetHeight());
				this.mBackButton.Resize(0, this.mApp.mHeight - this.mBackButton.Height(), this.mApp.mWidth, this.mBackButton.Height());
				Image image_MORE_GAMES_LIST_TOP_OV = AtlasResources.IMAGE_MORE_GAMES_LIST_TOP_OV;
				Image image_MORE_GAMES_LIST_BOTTOM_OV = AtlasResources.IMAGE_MORE_GAMES_LIST_BOTTOM_OV;
				this.mScroller.Resize(0, this.mFancyText.Bottom(), Constants.mConstants.MoreGamesDialog_Scroller_Width_Portrait, Constants.mConstants.MoreGamesDialog_Scroller_Height_Portrait);
				this.mScroller.SetScrollInsets(new Insets(0, image_MORE_GAMES_LIST_TOP_OV.GetHeight(), 0, image_MORE_GAMES_LIST_BOTTOM_OV.GetHeight()));
				this.mScroller.ScrollToMin(false);
				this.mScroller.AddOverlayImage(image_MORE_GAMES_LIST_TOP_OV, new CGPoint(0f, 0f));
				this.mScroller.AddOverlayImage(image_MORE_GAMES_LIST_BOTTOM_OV, new CGPoint(0f, (float)(this.mScroller.Height() - image_MORE_GAMES_LIST_BOTTOM_OV.GetHeight())));
				this.mScroller.SetColor(0, Constants.SCROLLER_BACK_COLOR);
				this.mScroller.EnableBackgroundFill(true);
				this.mScroller.EnableOverlays(true);
			}
			this.mFancyText.Clear();
			this.mFancyText.SetComposeFont(font_TEXT);
			this.mFancyText.SetComposeColor(SexyColor.White);
			this.mFancyText.SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
			this.mFancyText.AddWrappedText(this.mLexText);
			this.mFancyText.NewLine(0);
			this.mFancyText.ComposeFinish(FancyTextWidget.FinishOptions.VERTICAL_CENTER);
			this.mScroller.SetIndicatorsInsets(new Insets(2, 2, 4, 2));
			this.mScroller.FlashIndicators();
			this.mList.Resize(this.mList.mX, this.mList.mY, this.mScroller.mWidth, this.mList.mHeight);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			if (1 == uiStateLayout)
			{
				this.mFancyText.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f, 0.5f);
				this.mScrollTransition.SlideIn(this.mScroller, WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0f, 0.5f);
				this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f, 0.5f);
			}
			else
			{
				this.mFancyText.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mScrollTransition.SlideIn(this.mScroller, WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0f, 0.5f);
				this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
			}
			base.TransactionTimeSeconds(0.5f);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			float startSeconds = 0f;
			float num = 0.5f;
			this.mFancyText.FadeOut(startSeconds, num);
			this.mScrollTransition.FadeOut(this.mScroller, startSeconds, num);
			this.mBackButton.FadeOut(startSeconds, num);
			base.TransactionTimeSeconds(num);
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
		}

		public void ButtonDepress(int buttonId)
		{
			if (1 == buttonId)
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
			}
		}

		public void DialogButtonDepress(int dialogId, int buttonId)
		{
			if (dialogId == 0)
			{
				this.mList.GetLinkForRow(buttonId);
			}
		}

		public void DialogButtonPress(int theInt, int theInt2)
		{
		}

		protected void LoadEmUp()
		{
		}

		protected void AddRow(string imageName, string link)
		{
		}

		void ButtonListener.ButtonPress(int theId)
		{
		}

		void ButtonListener.ButtonPress(int theId, int theClickCount)
		{
		}

		void ButtonListener.ButtonDownTick(int theId)
		{
		}

		void ButtonListener.ButtonMouseEnter(int theId)
		{
		}

		void ButtonListener.ButtonMouseLeave(int theId)
		{
		}

		void ButtonListener.ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		protected string mLexText = string.Empty;

		protected FancyTextWidget mFancyText;

		protected ScrollWidget mScroller;

		protected MoreGamesListWidget mList;

		protected WidgetTransition mScrollTransition;

		protected PodButton mBackButton;

		protected enum ButtonID
		{
			MORE_GAMES_LIST_DIALOG_ID,
			BACK_BUTTON_ID
		}
	}
}
