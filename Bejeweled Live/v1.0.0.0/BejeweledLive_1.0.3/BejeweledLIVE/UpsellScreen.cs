using System;
using Sexy;

namespace BejeweledLIVE
{
	public class UpsellScreen : InterfaceWidget, ButtonListener
	{
		private Font BigTextFont
		{
			get
			{
				return Resources.FONT_BUTTON;
			}
		}

		private Font NormalTextFont
		{
			get
			{
				return Resources.FONT_ACHIEVEMENT_NAME;
			}
		}

		public UpsellScreen(GameApp aApp)
			: base(aApp)
		{
			for (int i = 0; i < this.mImageFrame.Length; i++)
			{
				this.mImageFrame[i] = new FrameWidget();
				Image imageInAtlasById = AtlasResources.GetImageInAtlasById(10178 + i);
				this.mImageFrame[i].SetImage(imageInAtlasById);
				this.mImageFrame[i].SetColor(new SexyColor(0, 0, 0, 255));
				this.mImageFrame[i].SetVisible(false);
				this.mImageFrame[i].mWidth = imageInAtlasById.mWidth;
				this.mImageFrame[i].mHeight = imageInAtlasById.mHeight;
				this.AddWidget(this.mImageFrame[i]);
			}
			for (int j = 0; j < this.mHeadingText.Length; j++)
			{
				this.mHeadingText[j] = new FancyTextWidget();
				this.AddWidget(this.mHeadingText[j]);
				this.mHeadingText[j].SetVisible(false);
			}
			for (int k = 0; k < this.mBodyText.Length; k++)
			{
				this.mBodyText[k] = new FancyTextWidget();
				this.AddWidget(this.mBodyText[k]);
				this.mBodyText[k].SetVisible(false);
			}
			this.mHeadingText[0].SetVisible(true);
			this.mBodyText[0].SetVisible(true);
			this.mImageFrame[0].SetVisible(true);
			this.mLogo = new SparklyLogo();
			this.AddWidget(this.mLogo);
			this.mUnlockButton = FancySmallButton.GetNewFancySmallButton(0, 1, 0, Strings.Unlock, this);
			this.AddWidget(this.mUnlockButton);
			this.mQuitButton = FancySmallButton.GetNewFancySmallButton(1, 1, 1, Strings.OptionsDialog_Quit, this);
			this.AddWidget(this.mQuitButton);
			this.mFadeTick = SexyAppFrameworkConstants.ticksForSeconds(5f);
			this.mCurrentTick = 0;
			this.mCurrentIndex = 0;
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			if (1 == uiStateLayout)
			{
				this.mLogo.Scale(0.8f);
				this.mLogo.Move((int)Constants.mConstants.S(30f), (int)Constants.mConstants.S(10f));
				int num = 0;
				for (int i = 0; i < this.mImageFrame.Length; i++)
				{
					this.mImageFrame[i].Move(this.mLogo.mX + this.mLogo.mWidth / 2 - this.mImageFrame[i].mWidth / 2, (int)Constants.mConstants.S(10f) + this.mLogo.mY + this.mLogo.mHeight + (this.mHeight - (this.mLogo.mY + this.mLogo.mHeight)) / 2 - this.mImageFrame[i].mHeight / 2);
					if (num < this.mImageFrame[i].mY + this.mImageFrame[i].mHeight)
					{
						num = this.mImageFrame[i].mY + this.mImageFrame[i].mHeight;
					}
				}
				int num2 = this.mLogo.mX + this.mLogo.mWidth + (this.mWidth - (this.mLogo.mX + this.mLogo.mWidth)) / 2;
				TRect trect = new TRect(this.mWidth - (this.mLogo.mX + this.mLogo.mWidth), (int)Constants.mConstants.S(50f), this.mWidth - (this.mWidth - num2) * 2, this.mHeight - (int)Constants.mConstants.S(20f));
				for (int j = 0; j < this.mHeadingText.Length; j++)
				{
					this.mHeadingText[j].Resize(trect.mX, trect.mY + (int)Constants.mConstants.S(10f), trect.mWidth, 0);
					this.mHeadingText[j].Clear();
					this.mHeadingText[j].SetComposeColor(SexyColor.White);
					this.mHeadingText[j].SetComposeFont(this.BigTextFont);
					this.mHeadingText[j].SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
					this.mHeadingText[j].AddWrappedText(UpsellScreen.mHeading[j]);
					this.mHeadingText[j].NewLine(4);
					this.mHeadingText[j].ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
					this.headingBottomPositions[j] = this.mHeadingText[j].mY + this.mHeadingText[j].mHeight;
				}
				trect.mY = num - (int)Constants.mConstants.S(15f);
				trect.mWidth -= (int)Constants.mConstants.S(20f);
				InterfaceWidget.LayoutWidgetAbove(this.mQuitButton, ref trect);
				InterfaceWidget.LayoutWidgetAbove(this.mUnlockButton, ref trect);
				trect.mWidth += (int)Constants.mConstants.S(20f);
				for (int k = 0; k < this.mBodyText.Length; k++)
				{
					this.mBodyText[k].Resize(trect.mX, this.mHeight / 2, trect.mWidth, trect.mY - (int)Constants.mConstants.S(20f));
					this.mBodyText[k].Clear();
					this.mBodyText[k].SetComposeColor(SexyColor.White);
					this.mBodyText[k].SetComposeFont(this.NormalTextFont);
					this.mBodyText[k].SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
					this.mBodyText[k].AddWrappedText(UpsellScreen.mBody[k]);
					this.mBodyText[k].NewLine(4);
					this.mBodyText[k].ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
					this.mBodyText[k].mY = this.headingBottomPositions[k] + (trect.mY - this.headingBottomPositions[k]) / 2 - this.mBodyText[k].mHeight / 2;
				}
				return;
			}
			TRect trect2 = new TRect((int)Constants.mConstants.S(10f), (int)Constants.mConstants.S(10f), this.mWidth - (int)Constants.mConstants.S(20f), this.mHeight - (int)Constants.mConstants.S(20f));
			this.mLogo.Scale(1f);
			this.mLogo.Move(this.mWidth / 2 - this.mLogo.mWidth / 2, (int)Constants.mConstants.S(10f));
			trect2.mY += this.mLogo.mHeight + this.mLogo.mY;
			trect2.mY += (int)Constants.mConstants.S(70f);
			int num3 = 0;
			int mY = trect2.mY;
			for (int l = 0; l < this.mHeadingText.Length; l++)
			{
				this.mHeadingText[l].Resize(trect2.mX + (int)Constants.mConstants.S(20f), trect2.mY + (int)Constants.mConstants.S(10f), trect2.mWidth - (int)Constants.mConstants.S(40f), 0);
				this.mHeadingText[l].Clear();
				this.mHeadingText[l].SetComposeColor(SexyColor.White);
				this.mHeadingText[l].SetComposeFont(this.BigTextFont);
				this.mHeadingText[l].SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
				this.mHeadingText[l].AddWrappedText(UpsellScreen.mHeading[l]);
				this.mHeadingText[l].NewLine(2);
				this.mHeadingText[l].ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
				if (num3 < this.mHeadingText[l].mHeight)
				{
					num3 = this.mHeadingText[l].mY + this.mHeadingText[l].mHeight;
				}
			}
			for (int m = 0; m < this.mHeadingText.Length; m++)
			{
				this.mHeadingText[m].mY = mY + (num3 - mY) / 2 - this.mHeadingText[m].mHeight / 2;
			}
			trect2.mY = num3 - (int)Constants.mConstants.S(20f);
			trect2.mX = this.mWidth / 2 - trect2.mWidth / 2;
			for (int n = 0; n < this.mImageFrame.Length; n++)
			{
				this.mImageFrame[n].Move(this.mWidth / 2 - this.mImageFrame[n].mWidth / 2, trect2.mY);
			}
			trect2.mY += this.mImageFrame[0].mHeight;
			int num4 = (int)Constants.mConstants.S(10f);
			TRect trect3 = new TRect(num4, this.mHeight, this.mWidth - num4 * 2, 0);
			InterfaceWidget.LayoutWidgetsAbove(this.mUnlockButton, this.mQuitButton, ref trect3);
			trect3.mHeight = this.mHeight - trect3.mY;
			trect3.mY = trect2.mY + (trect3.mY - trect2.mY) / 2;
			num4 = (int)Constants.mConstants.S(40f);
			trect3.mX = num4;
			trect3.mWidth = this.mWidth - num4 * 2;
			for (int num5 = 0; num5 < this.mBodyText.Length; num5++)
			{
				this.mBodyText[num5].Clear();
				this.mBodyText[num5].Resize(trect3.mX, trect3.mY - this.mBodyText[num5].mHeight / 2, trect3.mWidth, 0);
				this.mBodyText[num5].SetComposeColor(SexyColor.White);
				this.mBodyText[num5].SetComposeFont(this.NormalTextFont);
				this.mBodyText[num5].SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
				this.mBodyText[num5].AddWrappedText(UpsellScreen.mBody[num5]);
				this.mBodyText[num5].NewLine(4);
				this.mBodyText[num5].ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
				this.mBodyText[num5].Resize(trect3.mX, trect3.mY - this.mBodyText[num5].mHeight / 2, trect3.mWidth, 0);
			}
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			this.mCurrentIndex = 0;
			if (1 == uiStateLayout)
			{
				this.mLogo.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mHeadingText[0].SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mImageFrame[0].SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mBodyText[0].SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				this.mQuitButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				this.mUnlockButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				base.TransactionTimeSeconds(0.5f);
			}
			else
			{
				this.mLogo.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mHeadingText[0].SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mImageFrame[0].SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mBodyText[0].SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				this.mQuitButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				this.mUnlockButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				base.TransactionTimeSeconds(0.5f);
			}
			this.FadeIn(0f, 0.5f);
			this.mApp.PlaySong(SongType.MainMenu, true);
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.mLogo.FadeOut(0f, 0.5f);
			this.mHeadingText[this.mCurrentIndex].FadeOut(0f, 0.5f);
			this.mImageFrame[this.mCurrentIndex].FadeOut(0f, 0.5f);
			this.mBodyText[this.mCurrentIndex].FadeOut(0f, 0.5f);
			this.mQuitButton.FadeOut(0f, 0.5f);
			this.mUnlockButton.FadeOut(0f, 0.5f);
			this.FadeOut(0f, 0.5f);
			this.mCurrentIndex = 0;
		}

		public override void Update()
		{
			base.Update();
			if (this.mVisible)
			{
				if (this.mCurrentTick <= this.mFadeTick)
				{
					this.mCurrentTick++;
				}
				else
				{
					this.mHeadingText[this.mCurrentIndex].FadeOut(0f, 1f);
					this.mBodyText[this.mCurrentIndex].FadeOut(0f, 1f);
					this.mImageFrame[this.mCurrentIndex].FadeOut(0f, 1f);
					this.mCurrentIndex++;
					this.mCurrentIndex %= 5;
					this.mHeadingText[this.mCurrentIndex].SetVisible(true);
					this.mHeadingText[this.mCurrentIndex].FadeIn(0f, 1f);
					this.mBodyText[this.mCurrentIndex].SetVisible(true);
					this.mBodyText[this.mCurrentIndex].FadeIn(0f, 1f);
					this.mImageFrame[this.mCurrentIndex].SetVisible(true);
					this.mImageFrame[this.mCurrentIndex].FadeIn(0f, 1f);
					this.mCurrentTick = 0;
				}
			}
			if (!SexyAppBase.IsInTrialMode)
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_MODE_SELECT);
			}
		}

		public override void Draw(Graphics g)
		{
			g.SetColor(new SexyColor(0, 0, 0, (int)(127f * this.mOpacity)));
			g.SetColorizeImages(true);
			g.FillRect(this.GetRect());
			base.Draw(g);
		}

		public void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 0:
				this.mApp.BuyGame();
				return;
			case 1:
				this.mApp.AppExit();
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
			this.ButtonDepress(1);
			return true;
		}

		private const float SCREEN_FADE_TIME = 1f;

		private const int DelayBetweenScreenSwitches = 5;

		private const int NUMBER_OF_SCREENS = 5;

		private FrameWidget[] mImageFrame = new FrameWidget[5];

		private FancyTextWidget[] mHeadingText = new FancyTextWidget[5];

		private FancyTextWidget[] mBodyText = new FancyTextWidget[5];

		private SparklyLogo mLogo;

		private PodButton mUnlockButton;

		private PodButton mQuitButton;

		private int[] headingBottomPositions = new int[5];

		private readonly int mFadeTick;

		private int mCurrentTick;

		private int mCurrentIndex;

		private static readonly string[] mHeading = new string[]
		{
			Strings.UPSELL_HEADING1,
			Strings.UPSELL_HEADING2,
			Strings.UPSELL_HEADING3,
			Strings.UPSELL_HEADING4,
			Strings.UPSELL_HEADING5
		};

		private static readonly string[] mBody = new string[]
		{
			Strings.UPSELL_DESC1,
			Strings.UPSELL_DESC2,
			Strings.UPSELL_DESC3,
			Strings.UPSELL_DESC4,
			Strings.UPSELL_DESC5
		};

		public enum ButtonID
		{
			UNLOCK_ID,
			QUIT_ID
		}
	}
}
