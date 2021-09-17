using System;
using Sexy;

namespace BejeweledLIVE
{
	public class OptionsDialog : InterfaceWidget, ButtonListener, DialogListener, SliderListener, CheckboxListener
	{
		public OptionsDialog(GameApp app)
			: base(app)
		{
			this.mBackButton = FancySmallButton.GetNewFancySmallButton(0, 3, 3, Strings.Back, this);
			this.AddWidget(this.mBackButton);
			this.mHelpButton = FancySmallButton.GetNewFancySmallButton(2, 3, 2, Strings.OptionsDialog_Help, this);
			this.AddWidget(this.mHelpButton);
			this.mRestartButton = new PodSmallButton(4, 0, Strings.OptionsDialog_Restart, this);
			this.AddWidget(this.mRestartButton);
			this.mCreditsButton = FancySmallButton.GetNewFancySmallButton(8, 3, 1, Strings.CREDITS, this);
			this.AddWidget(this.mCreditsButton);
			this.mAboutButton = FancySmallButton.GetNewFancySmallButton(9, 3, 0, Strings.ABOUT, this);
			this.AddWidget(this.mAboutButton);
			this.mSoundVolumeLabel = new LabelWidget(Strings.OptionsDialog_Sound_Label, Resources.FONT_TEXT);
			this.mSoundVolumeLabel.SetColor(0, SexyColor.White);
			this.mSoundVolumeLabel.SizeToFit();
			this.AddWidget(this.mSoundVolumeLabel);
			this.mSoundVolumeSlider = new OptionsSlider(5, this);
			this.AddWidget(this.mSoundVolumeSlider);
			this.mMusicVolumeLabel = new LabelWidget(Strings.OptionsDialog_MUSIC_ON_LABEL, Resources.FONT_TEXT);
			this.mMusicVolumeLabel.SetColor(0, SexyColor.White);
			this.mMusicVolumeLabel.SizeToFit();
			this.AddWidget(this.mMusicVolumeLabel);
			this.mMusicVolumeSlider = new OptionsSlider(6, this);
			this.AddWidget(this.mMusicVolumeSlider);
			Constants.mConstants.S(300f);
			this.mAutoHintLabel[0] = new LabelWidget(Strings.OptionsDialog_Auto_Hint1, Resources.FONT_TEXT);
			this.mAutoHintLabel[0].SetColor(0, SexyColor.White);
			this.mAutoHintLabel[0].SizeToFit();
			this.AddWidget(this.mAutoHintLabel[0]);
			this.mAutoHintLabel[1] = new LabelWidget(Strings.OptionsDialog_Auto_Hint2, Resources.FONT_TEXT);
			this.mAutoHintLabel[1].SetColor(0, SexyColor.White);
			this.mAutoHintLabel[1].SizeToFit();
			this.AddWidget(this.mAutoHintLabel[1]);
			this.mAutoHintSwitch = new OptionsCheckbox(7, this);
			this.AddWidget(this.mAutoHintSwitch);
			this.mRunWhenLockedLabel[0] = new LabelWidget(Strings.OptionsDialog_Run_When_Screen_Locked1, Resources.FONT_TEXT);
			this.mRunWhenLockedLabel[0].SetColor(0, SexyColor.White);
			this.mRunWhenLockedLabel[0].SizeToFit();
			this.AddWidget(this.mRunWhenLockedLabel[0]);
			this.mRunWhenLockedLabel[1] = new LabelWidget(Strings.OptionsDialog_Run_When_Screen_Locked2, Resources.FONT_TEXT);
			this.mRunWhenLockedLabel[1].SetColor(0, SexyColor.White);
			this.mRunWhenLockedLabel[1].SizeToFit();
			this.AddWidget(this.mRunWhenLockedLabel[1]);
			this.mRunWhenLockedSwitch = new OptionsCheckbox(10, this);
			this.AddWidget(this.mRunWhenLockedSwitch);
			this.mFrame = new FrameWidget(Strings.Menu_Options, Constants.FRAME_BACK_COLOUR(1f));
			this.AddWidget(this.mFrame);
			this.PutBehind(this.mFrame, this.mSoundVolumeLabel);
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
			TRect trect = new TRect(10, this.mHeight - 10, this.mWidth - 20, 0);
			double value = (this.mApp.mMusicEnabled ? this.mApp.GetMusicVolume() : 0.0);
			this.mMusicVolumeSlider.SetValue(value);
			this.SetMusicVolumeLabel();
			this.mSoundVolumeSlider.SetValue(this.mApp.GetSfxVolume());
			this.mAutoHintSwitch.SetChecked(this.mApp.mProfile.WantsHint(Profile.Hint.HINT_MOVE), false);
			bool flag = 4 == uiState && GameMode.MODE_BLITZ == this.mApp.mGameMode;
			this.SetRunWhenLocked(this.mApp.mProfile.mRunWhenLocked);
			this.mRunWhenLockedSwitch.SetChecked(this.mApp.mProfile.mRunWhenLocked, false);
			if (1 == uiStateLayout)
			{
				this.mBackButton.mWidth = (int)Constants.mConstants.S(300f);
				this.mHelpButton.mWidth = (int)Constants.mConstants.S(300f);
				this.mCreditsButton.mWidth = (int)Constants.mConstants.S(300f);
				this.mAboutButton.mWidth = (int)Constants.mConstants.S(300f);
				int num = this.mWidth - (int)Constants.mConstants.S(10f) - this.mBackButton.mWidth;
				int theY = this.mHeight / 2 - 4 * this.mBackButton.mHeight / 2 + (int)Constants.mConstants.S(30f);
				TRect theRect = new TRect(num, theY, this.mWidth - num, 0);
				theRect.mWidth -= (int)Constants.mConstants.S(10f);
				this.mHelpButton.ButtonNumber = 0;
				this.mAboutButton.ButtonNumber = 1;
				this.mCreditsButton.ButtonNumber = 2;
				this.mBackButton.ButtonNumber = 3;
				this.mHelpButton.ButtonCount = (this.mAboutButton.ButtonCount = (this.mCreditsButton.ButtonCount = (this.mBackButton.ButtonCount = 3)));
				InterfaceWidget.LayoutWidgetBelow(this.mHelpButton, ref theRect);
				InterfaceWidget.LayoutWidgetBelow(this.mAboutButton, ref theRect);
				InterfaceWidget.LayoutWidgetBelow(this.mCreditsButton, ref theRect);
				InterfaceWidget.LayoutWidgetBelow(this.mBackButton, ref theRect);
				theRect.mWidth = this.mWidth - theRect.mWidth - (int)Constants.mConstants.S(20f);
				theRect.mHeight = this.mHeight - (int)Constants.mConstants.S(20f);
				theRect.mX = (int)Constants.mConstants.S(10f);
				theRect.mY = (int)Constants.mConstants.S(10f);
				this.mFrame.Resize(theRect);
				theRect.mY += AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() + (int)Constants.mConstants.S(48f);
				theRect.mX = this.mFrame.mX + (int)Constants.mConstants.S(75f);
				theRect.mWidth = this.mFrame.mWidth - (int)Constants.mConstants.S(150f);
				this.mSoundVolumeLabel.MoveCenterTo(this.mFrame.mWidth / 2, theRect.mY);
				theRect.mY += this.mSoundVolumeLabel.mHeight - (int)Constants.mConstants.S(20f);
				InterfaceWidget.LayoutWidgetBelow(this.mSoundVolumeSlider, ref theRect);
				theRect.mY += (int)Constants.mConstants.S(25f);
				this.mMusicVolumeLabel.MoveCenterTo(this.mFrame.mWidth / 2, theRect.mY);
				theRect.mY += this.mSoundVolumeLabel.mHeight - (int)Constants.mConstants.S(20f);
				InterfaceWidget.LayoutWidgetBelow(this.mMusicVolumeSlider, ref theRect);
				theRect.mY += (int)Constants.mConstants.S(10f);
				int mY = theRect.mY;
				theRect.mX = this.mFrame.mWidth / 2 - this.mFrame.mWidth / 4;
				for (int i = 0; i < this.mAutoHintLabel.Length; i++)
				{
					this.mAutoHintLabel[i].MoveCenterTo(theRect.mX, theRect.mY);
					theRect.mY += this.mAutoHintLabel[1].mHeight;
				}
				theRect.mY += this.mAutoHintLabel[1].mHeight;
				this.mAutoHintSwitch.MoveCenterTo(theRect.mX, theRect.mY);
				theRect.mY = mY;
				theRect.mX = this.mFrame.mWidth / 2 + this.mFrame.mWidth / 4;
				for (int j = 0; j < this.mRunWhenLockedLabel.Length; j++)
				{
					this.mRunWhenLockedLabel[j].MoveCenterTo(theRect.mX, theRect.mY);
					theRect.mY += this.mAutoHintLabel[1].mHeight;
				}
				theRect.mY += this.mAutoHintLabel[1].mHeight;
				this.mRunWhenLockedSwitch.MoveCenterTo(theRect.mX, theRect.mY);
			}
			else
			{
				this.mHelpButton.ButtonNumber = 0;
				this.mAboutButton.ButtonNumber = 0;
				this.mCreditsButton.ButtonNumber = 1;
				this.mBackButton.ButtonNumber = 2;
				this.mHelpButton.ButtonCount = (this.mAboutButton.ButtonCount = (this.mCreditsButton.ButtonCount = (this.mBackButton.ButtonCount = 2)));
				InterfaceWidget.LayoutWidgetAbove(this.mBackButton, ref trect);
				InterfaceWidget.LayoutWidgetAbove(this.mCreditsButton, ref trect);
				InterfaceWidget.LayoutWidgetsAbove(this.mHelpButton, this.mAboutButton, ref trect);
				TRect theRect2 = new TRect(0, (int)Constants.mConstants.S(10f), this.mWidth, trect.mY);
				this.mFrame.Resize(theRect2);
				theRect2.mY += AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() + (int)Constants.mConstants.S(50f);
				theRect2.mX = (int)Constants.mConstants.S(75f);
				theRect2.mWidth = this.mWidth - (int)Constants.mConstants.S(75f) - theRect2.mX;
				this.mSoundVolumeLabel.MoveCenterTo(this.mWidth / 2, theRect2.mY);
				theRect2.mY += this.mSoundVolumeLabel.mHeight - (int)Constants.mConstants.S(20f);
				InterfaceWidget.LayoutWidgetBelow(this.mSoundVolumeSlider, ref theRect2);
				theRect2.mY += (int)Constants.mConstants.S(50f);
				this.mMusicVolumeLabel.MoveCenterTo(this.mWidth / 2, theRect2.mY);
				theRect2.mY += this.mSoundVolumeLabel.mHeight - (int)Constants.mConstants.S(20f);
				InterfaceWidget.LayoutWidgetBelow(this.mMusicVolumeSlider, ref theRect2);
				theRect2.mY += (int)Constants.mConstants.S(70f);
				int mY2 = theRect2.mY;
				theRect2.mX = this.mFrame.mWidth / 2 - this.mFrame.mWidth / 4 + (int)Constants.mConstants.S(40f);
				for (int k = 0; k < this.mAutoHintLabel.Length; k++)
				{
					this.mAutoHintLabel[k].MoveCenterTo(theRect2.mX, theRect2.mY);
					theRect2.mY += this.mAutoHintLabel[1].mHeight;
				}
				theRect2.mY += this.mAutoHintLabel[1].mHeight;
				this.mAutoHintSwitch.MoveCenterTo(theRect2.mX, theRect2.mY);
				theRect2.mY = mY2;
				theRect2.mX = this.mFrame.mWidth / 2 + this.mFrame.mWidth / 4 - (int)Constants.mConstants.S(40f);
				for (int l = 0; l < this.mRunWhenLockedLabel.Length; l++)
				{
					this.mRunWhenLockedLabel[l].MoveCenterTo(theRect2.mX, theRect2.mY);
					theRect2.mY += this.mAutoHintLabel[1].mHeight;
				}
				theRect2.mY += this.mAutoHintLabel[1].mHeight;
				this.mRunWhenLockedSwitch.MoveCenterTo(theRect2.mX, theRect2.mY);
			}
			this.mRestartButton.SetVisible(flag);
			this.mMusicVolumeLabel.SetVisible(!flag);
			this.mMusicVolumeSlider.SetVisible(!flag);
		}

		public override void TransitionInToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionInToState(uiState, uiStateParam, uiStateLayout);
			if (1 == uiStateLayout)
			{
				this.mFrame.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f, 0.4f);
				this.mSoundVolumeLabel.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.125f, 0.625f);
				this.mSoundVolumeSlider.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.125f, 0.625f);
				this.mMusicVolumeLabel.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.075f, 0.575f);
				this.mMusicVolumeSlider.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0.075f, 0.575f);
				foreach (LabelWidget labelWidget in this.mAutoHintLabel)
				{
					labelWidget.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f, 0.5f);
				}
				this.mAutoHintSwitch.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f, 0.5f);
				this.mHelpButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0f, 0.4f);
				this.mCreditsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0f, 0.6f);
				this.mAboutButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0f, 0.5f);
				this.mRunWhenLockedSwitch.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f, 0.6f);
				foreach (LabelWidget labelWidget2 in this.mRunWhenLockedLabel)
				{
					labelWidget2.SlideIn(WidgetTransitionSubType.WIDGET_FROM_LEFT, 0f, 0.7f);
				}
				if (this.mRestartButton.mVisible)
				{
					this.mRestartButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.075f, 0.575f);
				}
				this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_RIGHT, 0.125f, 0.625f);
			}
			else
			{
				this.mFrame.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.4f);
				this.mSoundVolumeLabel.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0.125f, 0.625f);
				this.mSoundVolumeSlider.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0.125f, 0.625f);
				this.mMusicVolumeLabel.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0.075f, 0.575f);
				this.mMusicVolumeSlider.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0.075f, 0.575f);
				foreach (LabelWidget labelWidget3 in this.mAutoHintLabel)
				{
					labelWidget3.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				}
				this.mAutoHintSwitch.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.5f);
				this.mHelpButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.4f);
				this.mCreditsButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.5f);
				this.mAboutButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0f, 0.4f);
				this.mRunWhenLockedSwitch.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.6f);
				foreach (LabelWidget labelWidget4 in this.mRunWhenLockedLabel)
				{
					labelWidget4.SlideIn(WidgetTransitionSubType.WIDGET_FROM_TOP, 0f, 0.7f);
				}
				if (this.mRestartButton.mVisible)
				{
					this.mRestartButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.075f, 0.575f);
				}
				this.mBackButton.SlideIn(WidgetTransitionSubType.WIDGET_FROM_BOTTOM, 0.125f, 0.625f);
			}
			base.TransactionTimeSeconds(0.7f);
		}

		public override void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
			if (uiState == 3 || uiState == 4)
			{
				this.mApp.PlaySong(SongType.MainMenu, true);
			}
		}

		public override void TransitionOutToState(int uiState, int uiStateParam, int uiStateLayout)
		{
			base.TransitionOutToState(uiState, uiStateParam, uiStateLayout);
			float startSeconds = 0f;
			float num = 0.25f;
			this.mFrame.FadeOut(startSeconds, num);
			this.mBackButton.FadeOut(startSeconds, num);
			this.mHelpButton.FadeOut(startSeconds, num);
			this.mRestartButton.FadeOut(startSeconds, num);
			this.mSoundVolumeLabel.FadeOut(startSeconds, num);
			this.mSoundVolumeSlider.FadeOut(startSeconds, num);
			this.mMusicVolumeLabel.FadeOut(startSeconds, num);
			this.mMusicVolumeSlider.FadeOut(startSeconds, num);
			foreach (LabelWidget labelWidget in this.mAutoHintLabel)
			{
				labelWidget.FadeOut(startSeconds, num);
			}
			this.mAutoHintSwitch.FadeOut(startSeconds, num);
			this.mCreditsButton.FadeOut(startSeconds, num);
			this.mAboutButton.FadeOut(startSeconds, num);
			foreach (LabelWidget labelWidget2 in this.mRunWhenLockedLabel)
			{
				labelWidget2.FadeOut(startSeconds, num);
			}
			this.mRunWhenLockedSwitch.FadeOut(startSeconds, num);
			base.TransactionTimeSeconds(num);
		}

		public override bool BackButtonPress()
		{
			if (this.actionSheet != null)
			{
				return this.actionSheet.BackButtonPress();
			}
			if (this.alert != null)
			{
				return this.alert.BackButtonPress();
			}
			this.mApp.GotoInterfaceState((4 == this.mInterfaceState) ? 13 : 2);
			return true;
		}

		public override void Update()
		{
			base.Update();
			if (this.mParent != null && this.mVisible && !this.mDisabled && !this.mApp.mProfile.mPlayUserMusic && !this.mApp.mMusicEnabled)
			{
				this.mApp.mProfile.mPlayUserMusic = true;
				this.mMusicVolumeSlider.mLabel = Strings.OptionsDialog_MUSIC_OFF_LABEL;
				this.mMusicVolumeSlider.SetValue(0.0);
			}
		}

		public void CheckboxChecked(int checkboxId, bool isChecked)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
			if (checkboxId != 7)
			{
				if (checkboxId != 10)
				{
					return;
				}
				if (isChecked)
				{
					this.SetRunWhenLocked(true);
				}
				else
				{
					this.SetRunWhenLocked(false);
				}
				this.ShowRestartBox();
				return;
			}
			else
			{
				if (isChecked)
				{
					this.mApp.mProfile.EnableHint(Profile.Hint.HINT_MOVE);
					return;
				}
				this.mApp.mProfile.DisableHint(Profile.Hint.HINT_MOVE);
				return;
			}
		}

		private void SetRunWhenLocked(bool value)
		{
			Main.RunWhenLocked = value;
			this.mApp.mProfile.mRunWhenLocked = value;
		}

		public void SliderVal(int sliderId, double value)
		{
			if (5 == sliderId)
			{
				this.mApp.SetSfxVolume(value);
				this.mApp.mProfile.mSFXVolume = value;
				if (!this.mSoundVolumeSlider.mDragging)
				{
					this.mApp.PlaySample(Resources.SOUND_CLICK);
					return;
				}
			}
			else if (6 == sliderId)
			{
				if (value > 0.01 && !this.mApp.mMusicEnabled)
				{
					this.mApp.EnableMusic(true);
				}
				else if (value < 0.01 && this.mApp.mMusicEnabled)
				{
					this.mApp.EnableMusic(false);
				}
				this.mApp.mProfile.mPlayUserMusic = !this.mApp.mMusicEnabled;
				this.SetMusicVolumeLabel();
				this.mApp.SetMusicVolume(value);
				this.mApp.mProfile.mMusicVolume = value;
			}
		}

		private void SetMusicVolumeLabel()
		{
			this.mMusicVolumeLabel.mText = ((this.mApp.mProfile.mMusicVolume == 0.0) ? Strings.OptionsDialog_MUSIC_OFF_LABEL : Strings.OptionsDialog_MUSIC_ON_LABEL);
		}

		public void ButtonPress(int buttonId)
		{
			this.mApp.PlaySample(Resources.SOUND_CLICK);
		}

		public void ButtonDepress(int buttonId)
		{
			switch (buttonId)
			{
			case 0:
				this.BackButtonPress();
				return;
			case 1:
				if (this.mApp.mBoard.IsInProgress() && !SexyAppBase.IsInTrialMode)
				{
					this.actionSheet = ActionSheet.GetNewActionSheet(true, 0, this, this.mApp);
					this.actionSheet.AddButton(1002, Strings.OptionsDialog_Save_And_Quit);
					this.actionSheet.SetCancelButton(1003, Strings.Cancel);
					this.actionSheet.Present();
					return;
				}
				this.mApp.GotoInterfaceState(2);
				return;
			case 2:
				if (4 != this.mInterfaceState)
				{
					this.mApp.GotoInterfaceState(5);
					return;
				}
				if (GameMode.MODE_BLITZ == this.mApp.mGameMode)
				{
					this.mApp.GotoInterfaceState(8);
					return;
				}
				this.mApp.GotoInterfaceState(6);
				return;
			case 3:
				LiveLeaderboardDialog.PrepareState((this.mApp.mGameMode == GameMode.MODE_TIMED) ? LeaderboardState.Action : LeaderboardState.Classic, LeaderboardViewState.Friends);
				this.mApp.GotoInterfaceState((4 == this.mInterfaceState) ? 10 : 9);
				return;
			case 4:
				this.mApp.GotoInterfaceState(13);
				return;
			case 5:
			case 6:
			case 7:
				break;
			case 8:
				if (this.mInterfaceState == 3)
				{
					this.mApp.GotoInterfaceState(24);
					return;
				}
				this.mApp.GotoInterfaceState(25);
				return;
			case 9:
				this.ShowAboutBox();
				break;
			default:
				return;
			}
		}

		private void ShowAboutBox()
		{
			this.alert = new Alert(Dialogs.DIALOG_INSTRUCTION, this, this.mApp);
			this.alert.SetHeadingText(Strings.About_Heading);
			this.alert.ClearBodyText();
			this.alert.AddBodyText(string.Empty);
			this.alert.AddBodyText(Strings.About_Text_1);
			this.alert.AddBodyText(Strings.About_Text_2);
			this.alert.AddBodyText(string.Empty);
			this.alert.AddBodyText(Strings.About_Text_3);
			this.alert.AddBodyText(Strings.About_Text_4);
			this.alert.AddBodyText(string.Empty);
			this.alert.AddBodyText(string.Empty);
			this.alert.AddBodyText(Strings.About_Text_7 + GameApp.AppVersionNumber);
			this.alert.AddButton(Dialogoid.ButtonID.OK_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_GREEN, Strings.OK);
			this.alert.Present();
		}

		private void ShowRestartBox()
		{
			this.alert = new Alert(Dialogs.DIALOG_INSTRUCTION, this, this.mApp);
			this.alert.SetHeadingText(string.Empty);
			this.alert.ClearBodyText();
			this.alert.AddBodyText(Strings.Optionsdialog_Run_When_Locked_Restart_Message);
			this.alert.AddButton(Dialogoid.ButtonID.OK_BUTTON_ID, SmallButtonColors.SMALL_BUTTON_GREEN, Strings.OK);
			this.alert.Present();
		}

		public void DialogButtonDepress(int dialogId, int buttonId)
		{
			this.alert = null;
			if (dialogId != 0)
			{
				return;
			}
			if (1002 == buttonId)
			{
				this.mApp.GotoInterfaceState(2);
			}
		}

		public override void TouchCheatGesture()
		{
			int mInterfaceStateParam = this.mInterfaceStateParam;
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

		public void DialogButtonPress(int theId, int theClickCount)
		{
		}

		private const double MUSIC_SLIDER_THRESHOLD = 0.01;

		protected LabelWidget[] mRunWhenLockedLabel = new LabelWidget[2];

		protected OptionsCheckbox mRunWhenLockedSwitch;

		protected TRect bounds;

		protected FancyPodButton mBackButton;

		protected FancyPodButton mHelpButton;

		protected PodButton mRestartButton;

		protected FancyPodButton mCreditsButton;

		protected FancyPodButton mAboutButton;

		protected LabelWidget mSoundVolumeLabel;

		protected OptionsSlider mSoundVolumeSlider;

		protected LabelWidget mMusicVolumeLabel;

		protected OptionsSlider mMusicVolumeSlider;

		protected LabelWidget[] mAutoHintLabel = new LabelWidget[2];

		protected OptionsCheckbox mAutoHintSwitch;

		private FrameWidget mFrame;

		private Alert alert;

		private ActionSheet actionSheet;

		public enum ButtonID
		{
			BACK_BUTTON_ID,
			QUIT_BUTTON_ID,
			HELP_BUTTON_ID,
			SCORES_BUTTON_ID,
			RESTART_BUTTON_ID,
			SOUND_SLIDER_ID,
			MUSIC_SLIDER_ID,
			HINT_SWITCH_ID,
			CREDITS_BUTTON_ID,
			ABOUT_BUTTON_ID,
			RUN_WHEN_LOCKED_SWITCH_ID
		}
	}
}
