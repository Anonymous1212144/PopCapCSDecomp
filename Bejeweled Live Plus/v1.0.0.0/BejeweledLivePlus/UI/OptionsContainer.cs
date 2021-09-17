using System;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class OptionsContainer : Bej3Widget, CheckboxListener, SliderListener
	{
		public OptionsContainer()
			: base(Menu_Type.MENU_OPTIONSMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mDoesSlideInFromBottom = false;
			this.Resize(0, ConstantsWP.OPTIONSMENU_CONTAINER_Y, ConstantsWP.OPTIONSMENU_CONTAINER_W, ConstantsWP.OPTIONSMENU_CONTAINER_H);
			this.mAutoHintLabel = new Label(GlobalMembersResources.FONT_SUBHEADER, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mAutoHintLabel.Resize(ConstantsWP.OPTIONSMENU_AUTOHINT_LABEL_X, ConstantsWP.OPTIONSMENU_AUTOHINT_LABEL_Y, 0, 0);
			this.mAutoHintLabel.SetText(GlobalMembers._ID("Auto-Hint", 3415));
			this.AddWidget(this.mAutoHintLabel);
			this.mMuteLabel = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("Mute All Sounds", 3416), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mMuteLabel.Resize(ConstantsWP.OPTIONSMENU_MUTE_LABEL_X, ConstantsWP.OPTIONSMENU_MUTE_LABEL_Y, 0, 0);
			this.AddWidget(this.mMuteLabel);
			this.mTutorialLabel = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("Help", 3417), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mTutorialLabel.Resize(ConstantsWP.OPTIONSMENU_TUTORIAL_LABEL_X, ConstantsWP.OPTIONSMENU_TUTORIAL_LABEL_Y, 0, 0);
			this.AddWidget(this.mTutorialLabel);
			this.mVoiceImage = new ImageWidget(1365);
			this.mVoiceImage.Resize((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_SFX_ICONS_VOICES_ID)) + ConstantsWP.OPTIONSMENU_VOICE_LABEL_X, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_SFX_ICONS_VOICES_ID)) + ConstantsWP.OPTIONSMENU_VOICE_LABEL_Y, 0, 0);
			this.AddWidget(this.mVoiceImage);
			this.mFXImage = new ImageWidget(1363);
			this.mFXImage.Resize((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_SFX_ICONS_SOUND_ID)) + ConstantsWP.OPTIONSMENU_FX_LABEL_X, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_SFX_ICONS_SOUND_ID)) + ConstantsWP.OPTIONSMENU_FX_LABEL_Y, 0, 0);
			this.AddWidget(this.mFXImage);
			this.mMusicImage = new ImageWidget(1361);
			this.mMusicImage.Resize((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_SFX_ICONS_MUSIC_ID)) + ConstantsWP.OPTIONSMENU_MUSIC_LABEL_X, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_SFX_ICONS_MUSIC_ID)) + ConstantsWP.OPTIONSMENU_MUSIC_LABEL_Y, 0, 0);
			this.AddWidget(this.mMusicImage);
			this.mAutoHintCheckbox = new Bej3Checkbox(0, this);
			this.mAutoHintCheckbox.Resize(ConstantsWP.OPTIONSMENU_AUTOHINT_CHECKBOX_X, ConstantsWP.OPTIONSMENU_AUTOHINT_CHECKBOX_Y, 0, 0);
			this.mAutoHintCheckbox.mGrayOutWhenDisabled = false;
			this.AddWidget(this.mAutoHintCheckbox);
			this.mMuteCheckbox = new Bej3Checkbox(1, this);
			this.mMuteCheckbox.Resize(ConstantsWP.OPTIONSMENU_MUTE_CHECKBOX_X, ConstantsWP.OPTIONSMENU_MUTE_CHECKBOX_Y, 0, 0);
			this.mMuteCheckbox.mGrayOutWhenDisabled = false;
			this.AddWidget(this.mMuteCheckbox);
			this.mTutorialCheckbox = new Bej3Checkbox(2, this);
			this.mTutorialCheckbox.Resize(ConstantsWP.OPTIONSMENU_TUTORIAL_CHECKBOX_X, ConstantsWP.OPTIONSMENU_TUTORIAL_CHECKBOX_Y, 0, 0);
			this.mTutorialCheckbox.mGrayOutWhenDisabled = false;
			this.AddWidget(this.mTutorialCheckbox);
			this.mMusicSlider = new Bej3Slider(3, this);
			this.mMusicSlider.Resize(ConstantsWP.OPTIONSMENU_MUSIC_SLIDER_X, ConstantsWP.OPTIONSMENU_MUSIC_SLIDER_Y, ConstantsWP.OPTIONSMENU_SLIDER_WIDTH, 0);
			this.AddWidget(this.mMusicSlider);
			this.mFXSlider = new Bej3Slider(4, this);
			this.mFXSlider.Resize(ConstantsWP.OPTIONSMENU_MUSIC_SLIDER_X, ConstantsWP.OPTIONSMENU_FX_SLIDER_Y, ConstantsWP.OPTIONSMENU_SLIDER_WIDTH, 0);
			this.AddWidget(this.mFXSlider);
			this.mVoiceSlider = new Bej3Slider(5, this);
			this.mVoiceSlider.Resize(ConstantsWP.OPTIONSMENU_MUSIC_SLIDER_X, ConstantsWP.OPTIONSMENU_VOICE_SLIDER_Y, ConstantsWP.OPTIONSMENU_SLIDER_WIDTH, 0);
			this.AddWidget(this.mVoiceSlider);
			this.Show();
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void Show()
		{
			int mY = this.mY;
			base.Show();
			this.mY = mY;
		}

		public override void PlayMenuMusic()
		{
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			if (g.mTransY >= (float)GlobalMembers.gSexyAppBase.mHeight)
			{
				return;
			}
			base.DrawAll(theFlags, g);
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawLightBox(g, new Rect(ConstantsWP.OPTIONSMENU_BOX_1_X, ConstantsWP.OPTIONSMENU_BOX_1_Y, ConstantsWP.OPTIONSMENU_BOX_1_W, ConstantsWP.OPTIONSMENU_BOX_1_H));
			Bej3Widget.DrawLightBox(g, new Rect(ConstantsWP.OPTIONSMENU_BOX_2_X, ConstantsWP.OPTIONSMENU_BOX_2_Y, ConstantsWP.OPTIONSMENU_BOX_2_W, ConstantsWP.OPTIONSMENU_BOX_2_H));
		}

		public void CheckboxChecked(int theId, bool isChecked)
		{
			switch (theId)
			{
			case 0:
				GlobalMembers.gApp.mProfile.mAutoHint = isChecked;
				return;
			case 1:
				if (isChecked)
				{
					if (GlobalMembers.gApp.mMuteCount <= 0)
					{
						GlobalMembers.gApp.Mute();
					}
				}
				else if (GlobalMembers.gApp.mMuteCount > 0)
				{
					GlobalMembers.gApp.Unmute();
				}
				this.UpdateControls();
				return;
			case 2:
				GlobalMembers.gApp.mProfile.SetTutorialCleared(19, !isChecked);
				return;
			default:
				return;
			}
		}

		public void SliderVal(int theId, double theVal)
		{
			switch (theId)
			{
			case 3:
				GlobalMembers.gApp.SetMusicVolume(theVal);
				if (GlobalMembers.gApp.mGameInProgress && GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN && GlobalMembers.gApp.mBoard != null)
				{
					ZenBoard zenBoard = GlobalMembers.gApp.mBoard as ZenBoard;
					if (zenBoard != null)
					{
						zenBoard.MusicVolumeChanged();
					}
				}
				this.UpdateControls();
				return;
			case 4:
				GlobalMembers.gApp.SetSfxVolume(theVal);
				return;
			case 5:
				GlobalMembers.gApp.mVoiceVolume = theVal;
				if (!GlobalMembers.gApp.IsMuted())
				{
					GlobalMembers.gApp.mSoundManager.SetVolume(1, GlobalMembers.gApp.mVoiceVolume);
				}
				return;
			default:
				return;
			}
		}

		public void SliderReleased(int theId, double theVal)
		{
			switch (theId)
			{
			case 4:
				if (!GlobalMembers.gApp.IsMuted())
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_COMBO_2);
				}
				break;
			case 5:
				if (!GlobalMembers.gApp.IsMuted())
				{
					GlobalMembers.gApp.PlayVoice(GlobalMembersResourcesWP.SOUND_VOICE_GO);
				}
				break;
			}
			this.UpdateControls();
		}

		public override void LinkUpAssets()
		{
			this.mAutoHintCheckbox.LinkUpAssets();
			this.mMuteCheckbox.LinkUpAssets();
			this.mMusicSlider.LinkUpAssets();
			this.mFXSlider.LinkUpAssets();
			this.mVoiceSlider.LinkUpAssets();
			this.mMuteCheckbox.SetChecked(GlobalMembers.gApp.IsMuted(), false);
			this.mTutorialCheckbox.SetChecked(!GlobalMembers.gApp.mProfile.HasClearedTutorial(19), false);
			this.UpdateControls();
			base.LinkUpAssets();
			this.UpdateValues();
		}

		public void UpdateValues()
		{
			this.mMusicSlider.SetValue(GlobalMembers.gApp.mMusicVolume);
			this.mFXSlider.SetValue(GlobalMembers.gApp.mSfxVolume);
			this.mVoiceSlider.SetValue(GlobalMembers.gApp.mVoiceVolume);
			this.mAutoHintCheckbox.SetChecked(GlobalMembers.gApp.mProfile.mAutoHint, false);
		}

		public void UpdateControls()
		{
			bool flag = GlobalMembers.gApp.mMuteCount > 0;
			GlobalMembers.gApp.IsMusicEnabled();
			this.mMusicSlider.SetDisabled(flag);
			this.mVoiceSlider.SetDisabled(flag);
			this.mFXSlider.SetDisabled(flag);
			if (flag || GlobalMembers.gApp.GetMusicVolume() == 0.0)
			{
				this.mMusicImage.SetImage(1362);
				this.mMusicImage.mGrayedOut = true;
				this.mMusicSlider.mGrayedOut = true;
			}
			else
			{
				this.mMusicImage.SetImage(1361);
				this.mMusicImage.mGrayedOut = false;
				this.mMusicSlider.mGrayedOut = false;
			}
			if (flag || GlobalMembers.gApp.mVoiceVolume == 0.0)
			{
				this.mVoiceImage.SetImage(1366);
				this.mVoiceImage.mGrayedOut = true;
				this.mVoiceSlider.mGrayedOut = true;
			}
			else
			{
				this.mVoiceImage.SetImage(1365);
				this.mVoiceImage.mGrayedOut = false;
				this.mVoiceSlider.mGrayedOut = false;
			}
			if (flag || GlobalMembers.gApp.GetSfxVolume() == 0.0)
			{
				this.mFXImage.SetImage(1364);
				this.mFXImage.mGrayedOut = true;
				this.mFXSlider.mGrayedOut = true;
			}
			else
			{
				this.mFXImage.SetImage(1363);
				this.mFXImage.mGrayedOut = false;
				this.mFXSlider.mGrayedOut = false;
			}
			this.mMusicSlider.LinkUpAssets();
			this.mVoiceSlider.LinkUpAssets();
			this.mFXSlider.LinkUpAssets();
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			this.UpdateControls();
		}

		private Label mMuteLabel;

		public Bej3Checkbox mMuteCheckbox;

		private Label mTutorialLabel;

		private Bej3Checkbox mTutorialCheckbox;

		private Label mAutoHintLabel;

		private Bej3Checkbox mAutoHintCheckbox;

		private Bej3Slider mMusicSlider;

		private Bej3Slider mFXSlider;

		private Bej3Slider mVoiceSlider;

		private ImageWidget mVoiceImage;

		private ImageWidget mFXImage;

		private ImageWidget mMusicImage;

		private enum OptionsMenuContainer_BUTTON_IDS
		{
			CHK_AUTOHINT_ID,
			CHK_MUTE_ID,
			CHK_TUTORIAL_ID,
			SLD_MUSIC_ID,
			SLD_FX_ID,
			SLD_VOICE_ID
		}
	}
}
