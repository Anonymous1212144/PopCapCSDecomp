using System;
using Sexy;

namespace BejeweledLIVE
{
	public class HighScoreNameDialog : InterfaceWidget, EditListener
	{
		public HighScoreNameDialog(GameApp app)
			: base(app)
		{
			this.mHeadingText = new FancyTextWidget();
			this.AddWidget(this.mHeadingText);
			this.mNameRing = new ImageWidget(AtlasResources.IMAGE_PILL_RING, ImageWidget.Geometry.DRAW_BOX);
			this.AddWidget(this.mNameRing);
			this.mNameEdit = new EditWidget(1, this);
			this.mNameEdit.SetFont(Resources.FONT_TEXT);
			this.mNameEdit.SetVisible(false);
			this.mNameEdit.mMaxChars = 8;
			this.mNameEdit.mColors[0] = SexyColor.White;
			this.mNameEdit.mColors[2] = SexyColor.Black;
			this.mName = this.mApp.mProfile.GetMostRecentHighScoreName();
			this.mNameEdit.SetText(this.mName);
			this.mNameRing.AddWidget(this.mNameEdit);
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
			Insets insets = new Insets(10, 10, 10, 10);
			this.mHeadingText.Resize(insets.mLeft, insets.mTop, this.mWidth - insets.mRight - insets.mTop, 0);
			this.mHeadingText.Clear();
			this.mHeadingText.SetComposeFont(Resources.FONT_TEXT);
			this.mHeadingText.SetComposeColor(SexyColor.White);
			this.mHeadingText.SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
			this.mHeadingText.AddText("Congratulations!");
			this.mHeadingText.NewLine(0);
			this.mHeadingText.AddText("You earned a new high score!");
			this.mHeadingText.NewLine(0);
			this.mHeadingText.AddText("ENTER YOUR NAME");
			this.mHeadingText.NewLine(0);
			this.mHeadingText.ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
			this.mNameRing.Resize(this.mHeadingText.mX, this.mHeadingText.Bottom(), this.mHeadingText.mWidth, this.mNameRing.mHeight);
			TRect theRect = default(TRect);
			theRect.mX = this.mNameRing.Height();
			theRect.mWidth = this.mNameRing.Width() - 2 * theRect.mX;
			theRect.mHeight = 30;
			theRect.mY = (this.mNameRing.Height() - theRect.mHeight) / 2;
			this.mNameEdit.Resize(theRect);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			this.mHeadingText.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
			this.mNameRing.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
			this.mNameEdit.SetVisible(true);
			this.mFader.SetKey(SexyAppFrameworkConstants.ticksForSeconds(0f), 0f, false, true);
			this.mFader.SetKey(SexyAppFrameworkConstants.ticksForSeconds(0.6f), 1f, false, true);
			base.TransactionTimeSeconds(0.5f);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.mHeadingText.FadeOut(0f, 0.5f);
			this.mNameRing.FadeOut(0f, 0.5f);
			this.mNameEdit.SetVisible(false);
			this.mFader.SetKey(SexyAppFrameworkConstants.ticksForSeconds(0f), 1f, false, true);
			this.mFader.SetKey(SexyAppFrameworkConstants.ticksForSeconds(0.5f), 0f, false, true);
			base.TransactionTimeSeconds(0.5f);
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
			if (1 == this.mInterfaceStateParam)
			{
				this.mWidgetManager.SetFocus(this.mNameEdit);
			}
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			int num = base.TransactionTick();
			float num2 = (this.mFader.Empty() ? 1f : this.mFader.Tick((float)num));
			g.SetColor(new SexyColor(0, 0, 0, (int)(167f * num2)));
			g.FillRect(0, 0, this.mWidth, this.mHeight);
		}

		public void EditWidgetText(int theId, ref string theText)
		{
			this.mName = theText;
			if (this.mName.length() > 0)
			{
				this.mApp.mProfile.UpdateScoreName((int)this.mApp.mGameMode, this.mApp.mHighScorePos, ref this.mName);
			}
			this.mApp.GotoInterfaceState(15);
		}

		public bool AllowText(int theId, ref string theText)
		{
			return true;
		}

		public bool AllowChar(int theId, char theChar)
		{
			return true;
		}

		public bool ShouldClear()
		{
			return true;
		}

		protected KeyInterpolatorFloat mFader = KeyInterpolatorFloat.GetNewKeyInterpolatorFloat();

		protected FancyTextWidget mHeadingText;

		protected ImageWidget mNameRing;

		protected EditWidget mNameEdit;

		protected string mName = string.Empty;
	}
}
