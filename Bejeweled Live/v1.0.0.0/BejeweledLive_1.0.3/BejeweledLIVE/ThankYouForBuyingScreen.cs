using System;
using Sexy;

namespace BejeweledLIVE
{
	public class ThankYouForBuyingScreen : InterfaceWidget, ButtonListener
	{
		public ThankYouForBuyingScreen(GameApp app)
			: base(app)
		{
			this.mContinueButton = new PodSmallButton(0, 1, "CONTINUE", this);
			this.AddWidget(this.mContinueButton);
			Font font_TEXT = Resources.FONT_TEXT;
			this.mLabelsWidth = 0;
			for (int i = 0; i < 1; i++)
			{
				this.mLabels[i] = new LabelWidget(ThankYouForBuyingScreen.LABEL_CONTENTS[i], font_TEXT);
				this.mLabels[i].SetJustification(0);
				this.mLabelsWidth = Math.Max(this.mLabelsWidth, font_TEXT.StringWidth(ThankYouForBuyingScreen.LABEL_CONTENTS[i]));
			}
			for (int i = 0; i < this.mLabels.Length; i++)
			{
				this.mLabels[i].SetColor(0, SexyColor.White);
				this.mLabels[i].SizeToFit();
				this.AddWidget(this.mLabels[i]);
			}
			this.mTitle = new ImageWidget(AtlasResources.IMAGE_MAINMENU_LIVE_LOGO, ImageWidget.Geometry.DRAW_CENTERED);
			this.AddWidget(this.mTitle);
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			this.mTitle.Move((this.mWidth - this.mTitle.mWidth) / 2, 0);
			TRect trect = default(TRect);
			trect.mX = 0;
			trect.mWidth = this.mWidth;
			trect.mY = (this.mTitle.Bottom() + this.mTitle.Top()) / 2 - GameApp.DIALOGBOX_INTERIOR_INSETS.mTop + (GameApp.DIALOGBOX_INTERIOR_INSETS.mTop - GameApp.DIALOGBOX_EXTERIOR_INSETS.mTop) / 2;
			TRect trect2 = default(TRect);
			trect2.mX = trect.mX + GameApp.DIALOGBOX_CONTENT_INSETS.mLeft;
			trect2.mWidth = trect.mWidth - GameApp.DIALOGBOX_CONTENT_INSETS.mLeft - GameApp.DIALOGBOX_CONTENT_INSETS.mRight;
			trect2.mY = this.mTitle.Bottom() + 50;
			trect2.mHeight = 0;
			for (int i = 0; i < this.mLabels.Length; i++)
			{
				InterfaceWidget.LayoutWidgetBelow(this.mLabels[i], ref trect2);
			}
			trect.mHeight = trect2.mY + GameApp.DIALOGBOX_CONTENT_INSETS.mBottom - trect.mY;
			trect2.mX = 0;
			trect2.mWidth = this.mWidth;
			trect2.mY = this.mHeight;
			InterfaceWidget.LayoutWidgetAbove(this.mContinueButton, ref trect2);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			for (int i = 0; i < this.mLabels.Length; i++)
			{
				float num = (float)i * 0.1f;
				float endSeconds = num + 0.3f;
				this.mLabels[i].SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, num, endSeconds);
			}
			this.mTitle.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.3f);
			this.mContinueButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.4f);
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
			this.mTitle.FadeOut(startSeconds, num);
			this.mContinueButton.FadeOut(startSeconds, num);
			base.TransactionTimeSeconds(num);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public override bool BackButtonPress()
		{
			this.ButtonDepress(0);
			return true;
		}

		public void ButtonDepress(int buttonId)
		{
			if (buttonId != 0)
			{
				return;
			}
			this.mApp.GotoInterfaceState(2);
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonPress(int theId, int theClickCount)
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

		protected LabelWidget[] mLabels = new LabelWidget[1];

		protected int mLabelsWidth;

		protected ImageWidget mTitle;

		protected PodButton mContinueButton;

		private static readonly string[] LABEL_CONTENTS = new string[] { "Thank you for buying the game" };

		public enum ButtonID
		{
			CONTINUE_BUTTON_ID
		}

		private enum Labels
		{
			Description,
			MAX
		}
	}
}
