using System;
using Sexy;

namespace BejeweledLIVE
{
	public class Credits : InterfaceWidget, ButtonListener
	{
		public Credits(GameApp aApp)
			: base(aApp)
		{
			this.mBackButton = FancySmallButton.GetNewFancySmallButton(6, 0, 0, Strings.Back, this);
			this.AddWidget(this.mBackButton);
			this.mFrame = new FrameWidget(Strings.CREDITS, Constants.FRAME_BACK_COLOUR(1f));
			this.AddWidget(this.mFrame);
			this.BringToBack(this.mFrame);
			this.mCreditText = new FancyTextWidget();
			this.AddWidget(this.mCreditText);
			int num = (int)Constants.mConstants.S(10f);
			Insets insets = new Insets(num, num, num, num);
			int mWidth = this.mApp.mWidth;
			int num2 = mWidth - insets.mRight;
			int contentWidth = num2 - insets.mLeft;
			int mTop = insets.mTop;
			this.SetupTextWidget(insets, contentWidth, mTop);
			this.BringToBack(this.mFrame);
		}

		private void SetupTextWidget(Insets insets, int contentWidth, int theY)
		{
			this.mCreditText.Resize(insets.mLeft, theY, contentWidth, 0);
			this.mCreditText.Clear();
			this.mCreditText.SetComposeAlignment(FancyTextWidget.Alignment.CENTER);
			this.mCreditText.SetComposeFont(Resources.FONT_ACHIEVEMENT_NAME);
			Font font_BUTTON = Resources.FONT_BUTTON;
			Font font_TEXT = Resources.FONT_TEXT;
			Font font_ACHIEVEMENT_NAME = Resources.FONT_ACHIEVEMENT_NAME;
			SexyColor aColor = new SexyColor(255, 255, 255, 255);
			SexyColor aColor2 = new SexyColor(200, 200, 200, 255);
			this.mCreditText.SetComposeColor(SexyColor.White);
			this.mCreditText.NewLine(15);
			this.mCreditText.AddImage(AtlasResources.IMAGE_MAINMENU_LIVE_LOGO, 0.5f);
			this.mCreditText.NewLine();
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_WindowsPhone7Team);
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_Producers);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text in this.producers)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_GameDesign);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text2 in this.gameDesign)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text2);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_EngineeringManager);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text3 in this.engineeringManager)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text3);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_Engineering);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text4 in this.engineering)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text4);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_ArtManager);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text5 in this.artManager)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text5);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_Art);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text6 in this.art)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text6);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_OriginalMusic);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text7 in this.music)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text7);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_QAManager);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text8 in this.qaManager)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text8);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_QA_Lead);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text9 in this.qaLead)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text9);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_SeniorQA);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text10 in this.seniorQa)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text10);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_Additional_QA);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text11 in this.additionalQa)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text11);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_LocalizationDirector);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text12 in this.localisationDirector)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text12);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_LocalizationPM);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text13 in this.localisationPm)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text13);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_LocalizationLead);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text14 in this.localisationLead)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text14);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_LocalizedAudio);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text15 in this.localizedAudio)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text15);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_LocalizationQA);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text16 in this.localisationQa)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text16);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_Marketing);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text17 in this.marketing)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text17);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_BusinessDevelopment);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text18 in this.businessDevelopment)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text18);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_ExecutiveProducer);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text19 in this.executiveProducer)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text19);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_GeneralManager);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text20 in this.generalManager)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text20);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_HeadofStudio);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text21 in this.headOfStudio)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text21);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_SpecialThanks);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text22 in this.specialThanks)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text22);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_OriginalTeam);
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_OriginalProducers);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text23 in this.originalProducers)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text23);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_OriginalConcept);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text24 in this.originalGameDesign)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text24);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_OriginalEngineering);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text25 in this.originalEngineering)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text25);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_OriginalArt);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text26 in this.originalArt)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text26);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_Backgrounds);
			this.mCreditText.SetComposeFont(font_ACHIEVEMENT_NAME);
			this.mCreditText.SetComposeColor(aColor2);
			foreach (string text27 in this.backgrounds)
			{
				this.mCreditText.NewLine(15);
				this.mCreditText.AddText(text27);
			}
			this.mCreditText.NewLine(15);
			this.mCreditText.NewLine(15);
			this.mCreditText.SetComposeFont(font_TEXT);
			this.mCreditText.SetComposeColor(aColor);
			this.mCreditText.AddText(Strings.CREDITS_ThankYouForPlaying);
			this.mCreditText.NewLine(4);
			this.mCreditText.ComposeFinish(FancyTextWidget.FinishOptions.AUTO_HEIGHT);
			this.totalTextHeight = (float)this.mCreditText.mHeight;
		}

		public override void Update()
		{
			base.Update();
			this.textPos -= 0.5f;
			if (this.textPos < -this.totalTextHeight || this.resetTextPos)
			{
				this.textPos = (float)this.textStartPos;
				this.resetTextPos = false;
			}
			this.mCreditText.mY = (int)this.textPos;
		}

		public override void SetupForState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.SetupForState(uiState, uiStateParam, uiStateLayout);
			this.Resize(0, 0, this.mApp.mWidth, this.mApp.mHeight);
			this.SetTextStartPos();
			TRect theRect = new TRect((int)Constants.mConstants.S(10f), this.mHeight - (int)Constants.mConstants.S(10f), this.mWidth - (int)Constants.mConstants.S(10f) * 2, 0);
			if (this.mApp.IsLandscape())
			{
				theRect.mWidth = (int)((float)theRect.mWidth * Constants.BUTTON_WIDTH_FACTOR_LANDSCAPE);
			}
			theRect.mX = this.mWidth / 2 - theRect.mWidth / 2;
			InterfaceWidget.LayoutWidgetAbove(this.mBackButton, ref theRect);
			theRect.mWidth = this.mWidth - (int)Constants.mConstants.S(10f) * 2;
			theRect.mX = this.mWidth / 2 - theRect.mWidth / 2;
			theRect.mHeight = theRect.mY;
			theRect.mY = (int)Constants.mConstants.S(10f);
			this.mFrame.Resize(theRect);
			this.mCreditText.mX = this.mWidth / 2 - this.mCreditText.mWidth / 2;
			this.mCreditText.ClippingRegion = new TRect?(new TRect(this.mFrame.mX + (int)Constants.mConstants.S(10f), this.mFrame.mY + (int)Constants.mConstants.S(115f), -this.mFrame.mX + this.mFrame.mWidth - (int)Constants.mConstants.S(10f), -this.mFrame.mY + this.mFrame.mHeight - (int)Constants.mConstants.S(160f)));
		}

		private void SetTextStartPos()
		{
			this.textStartPos = this.mHeight - (int)Constants.mConstants.S(170f) + (int)Constants.mConstants.S(20f);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
			this.mFrame.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
			this.mCreditText.FadeIn(0f, 0.5f);
			this.resetTextPos = true;
			this.SetTextStartPos();
			base.TransactionTimeSeconds(0.5f);
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
			if (uiState == 24 || uiState == 25)
			{
				this.mApp.PlaySong(SongType.MainMenu, true);
			}
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			this.mBackButton.FadeOut(0f, 0.5f);
			this.mFrame.FadeOut(0f, 0.5f);
			this.mCreditText.FadeOut(0f, 0.5f);
		}

		public override bool BackButtonPress()
		{
			if (this.mInterfaceState == 25)
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_INFO_OVER_GAME);
			}
			else
			{
				this.mApp.GotoInterfaceState(InterfaceStates.UI_STATE_INFO);
			}
			return true;
		}

		public void ButtonDepress(int theId)
		{
			if (theId != 6)
			{
				return;
			}
			this.BackButtonPress();
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

		private const float textSpeed = 0.5f;

		private readonly string[] producers = new string[] { "Sameer Baroova", "Eric Thommerot" };

		private readonly string[] gameDesign = new string[] { "David Bishop", "Antubel Moreda" };

		private readonly string[] engineeringManager = new string[] { "Sameer Baroova" };

		private readonly string[] engineering = new string[] { "Robert Lester", "Kevin Lockard", "Christian 'Schinki' Schinkoethe", "Yufeng Shen" };

		private readonly string[] artManager = new string[] { "Lee Davies" };

		private readonly string[] art = new string[] { "Riana McKeith", "Philip Plunkett" };

		private readonly string[] music = new string[] { "Peter Hajba" };

		private readonly string[] qaManager = new string[] { "René Laurent" };

		private readonly string[] qaLead = new string[] { "Borja Guillán" };

		private readonly string[] seniorQa = new string[] { "Kieran Gleeson", "JP Vaughan" };

		private readonly string[] additionalQa = new string[] { "Stephen Geddes" };

		private readonly string[] localisationDirector = new string[] { "Jonathon Young" };

		private readonly string[] localisationPm = new string[] { "Anthony Mackey" };

		private readonly string[] localisationLead = new string[] { "Jean 'Swissy' de Mérey" };

		private readonly string[] localisationQa = new string[] { "Lorenzo Penati", "Antonio Asensio Pérez", "Jessica Schuster" };

		private readonly string[] marketing = new string[] { "Rex Sikora" };

		private readonly string[] businessDevelopment = new string[] { "Eddie Dowse", "Andrew Stein" };

		private readonly string[] executiveProducer = new string[] { "Viktorya Hollings" };

		private readonly string[] generalManager = new string[] { "Paul Breslin" };

		private readonly string[] headOfStudio = new string[] { "Ed Allard" };

		private readonly string[] specialThanks = new string[] { "Bob Chamberlain", "Yang Han", "Ed Miller", "Cathy Orr", "David Roberts", "John Vechey" };

		private readonly string[] localizedAudio = new string[] { "John E. Kelleher", "Jean 'Swissy' de Mérey", "Anthony Mackey" };

		private readonly string[] originalProducers = new string[] { "Brian Fiete", "Matthew Lee Johnston", "Jason Kapalka" };

		private readonly string[] originalGameDesign = new string[] { "Brian Fiete", "Jason Kapalka" };

		private readonly string[] originalEngineering = new string[] { "Dan Banay", "Joe Mobley" };

		private readonly string[] originalArt = new string[] { "Josh Langley", "Walter Wilson" };

		private readonly string[] backgrounds = new string[] { "Jim Abraham", "Misael Armendariz", "Marcia Broderick", "Matt Holmberg", "Jordan Kotzebue", "Bill Olmstead", "Rich Werner" };

		private PodButton mBackButton;

		private FrameWidget mFrame;

		private FancyTextWidget mCreditText;

		private float textPos;

		private int textStartPos;

		private float totalTextHeight;

		private bool resetTextPos;
	}
}
