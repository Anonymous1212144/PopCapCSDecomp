using System;
using System.Collections.Generic;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class ZenOptionsContainer : Bej3Widget, SliderListener, CheckboxListener, Bej3SlideSelectorListener
	{
		public ZenOptionsContainer()
			: base(Menu_Type.MENU_ZENOPTIONSMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mDoesSlideInFromBottom = false;
			this.mEnabled = true;
			this.mAmbientSoundWidgets.Clear();
			this.mMantrasWidgets.Clear();
			this.mBreathModWidgets.Clear();
			this.mAmbientSoundStartDelay = 0;
			this.mAmbientSoundId = -1;
			int num = ConstantsWP.ZENOPTIONS_AMBIENT_SOUND_DESC_Y;
			this.mAmbientSoundDescLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Replace the game music with environmental audio tracks for greater relaxation and focus.", 3622), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE);
			this.mAmbientSoundDescLabel.SetTextBlock(new Rect((GlobalMembers.gApp.mWidth - ConstantsWP.ZENOPTIONS_DESC_WIDTH) / 2, ConstantsWP.ZENOPTIONS_DESC_Y, ConstantsWP.ZENOPTIONS_DESC_WIDTH, ConstantsWP.ZENOPTIONS_AMBIENT_SOUND_DESC_HEIGHT), true);
			this.mAmbientSoundDescLabel.SetTextBlockEnabled(true);
			this.mAmbientSoundDescLabel.SetTextBlockAlignment(0);
			this.AddWidget(this.mAmbientSoundDescLabel);
			this.mAmbientSoundWidgets.Add(this.mAmbientSoundDescLabel);
			num += ConstantsWP.ZENOPTIONS_AMBIENT_SOUND_SLIDE_OFFSET_Y;
			this.mAmbientSoundSelector = new Bej3SlideSelector(9, this, this, ConstantsWP.ZENOPTIONS_AMBIENCE_ITEM_SIZE, ConstantsWP.ZENOPTIONS_AMBIENCE_WIDTH);
			this.mAmbientSoundSelector.Resize(0, num, ConstantsWP.ZENOPTIONS_AMBIENT_SOUND_SELECTOR_WIDTH, ConstantsWP.ZENOPTIONS_AMBIENT_SOUND_SELECTOR_HEIGHT + 75);
			this.mAmbientSoundSelector.SetThreshold(ConstantsWP.ZENOPTIONS_SLIDER_HOR_THRESHOLD, 0);
			this.mAmbientSoundSelector.AddItem(1, 779);
			this.mAmbientSoundSelector.AddItem(2, 790);
			this.mAmbientSoundSelector.AddItem(3, 785);
			this.mAmbientSoundSelector.AddItem(4, 781);
			this.mAmbientSoundSelector.AddItem(5, 789);
			this.mAmbientSoundSelector.AddItem(6, 792);
			this.mAmbientSoundSelector.AddItem(7, 780);
			this.mAmbientSoundSelector.AddItem(8, 782);
			this.AddWidget(this.mAmbientSoundSelector);
			this.mAmbientSoundWidgets.Add(this.mAmbientSoundSelector);
			num += ConstantsWP.ZENOPTIONS_AMBIENT_SOUND_SELECTOR_HEIGHT + ConstantsWP.ZENOPTIONS_SLIDE_ITEM_LABEL_OFFSET_Y;
			this.mAmbientSoundItemNameLabel = new Label(GlobalMembersResources.FONT_DIALOG, "");
			this.mAmbientSoundItemNameLabel.Resize(ConstantsWP.ZENOPTIONS_CENTER_X, num, 0, 0);
			this.mAmbientSoundItemNameLabel.SetLayerColor(0, Bej3Widget.COLOR_DIALOG_WHITE);
			this.AddWidget(this.mAmbientSoundItemNameLabel);
			this.mAmbientSoundWidgets.Add(this.mAmbientSoundItemNameLabel);
			this.mAmbientSwipeLeftButton = new Bej3Button(14, this, Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
			this.mAmbientSwipeLeftButton.Resize(ConstantsWP.ZENOPTIONS_BTN_LEFT_OFFSET, num - GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPE.mHeight / 2, 0, 0);
			this.AddWidget(this.mAmbientSwipeLeftButton);
			this.mAmbientSoundWidgets.Add(this.mAmbientSwipeLeftButton);
			this.mAmbientSwipeRightButton = new Bej3Button(15, this, Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE);
			this.mAmbientSwipeRightButton.Resize(ConstantsWP.ZENOPTIONS_BTN_RIGHT_OFFSET, num - GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPE.mHeight / 2, 0, 0);
			this.AddWidget(this.mAmbientSwipeRightButton);
			this.mAmbientSoundWidgets.Add(this.mAmbientSwipeRightButton);
			num += ConstantsWP.AMBIENT_VOL_OFFSET_Y;
			this.mAmbientVolumeSlider = new Bej3Slider(10, this);
			this.mAmbientVolumeSlider.Resize(ConstantsWP.ZENOPTIONS_ITEM_RIGHT_OFFSET - ConstantsWP.ZENOPTIONS_SLIDER_WIDTH, num, ConstantsWP.ZENOPTIONS_SLIDER_WIDTH, 0);
			this.mAmbientVolumeSlider.SetThreshold(ConstantsWP.ZENOPTIONS_SLIDER_HOR_THRESHOLD, 0);
			this.AddWidget(this.mAmbientVolumeSlider);
			this.mAmbientSoundWidgets.Add(this.mAmbientVolumeSlider);
			this.mAmbientVolumeLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Volume", 520), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mAmbientVolumeLabel.Resize(ConstantsWP.ZENOPTIONS_ITEM_LEFT_OFFSET, this.mAmbientVolumeSlider.mY + GlobalMembersResources.FONT_DIALOG.GetAscent(), 0, 0);
			this.AddWidget(this.mAmbientVolumeLabel);
			this.mAmbientSoundWidgets.Add(this.mAmbientVolumeLabel);
			num = ConstantsWP.ZENOPTIONS_MANTRAS_DESC_Y;
			this.mMantra_DescLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Positive textual affirmations are displayed on screen, helping to focus on beneficial areas for meditation.", 3623), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE);
			this.mMantra_DescLabel.SetTextBlock(new Rect((GlobalMembers.gApp.mWidth - ConstantsWP.ZENOPTIONS_DESC_WIDTH) / 2, ConstantsWP.ZENOPTIONS_DESC_Y, ConstantsWP.ZENOPTIONS_DESC_WIDTH, ConstantsWP.ZENOPTIONS_MANTRAS_DESC_HEIGHT), true);
			this.mMantra_DescLabel.SetTextBlockEnabled(true);
			this.mMantra_DescLabel.SetTextBlockAlignment(0);
			this.AddWidget(this.mMantra_DescLabel);
			this.mMantrasWidgets.Add(this.mMantra_DescLabel);
			num += ConstantsWP.ZENOPTIONS_MANTRAS_SLIDE_OFFSET_Y;
			this.mMantraSelector = new Bej3SlideSelector(13, this, this, ConstantsWP.ZENOPTIONS_MANTRA_ITEM_SIZE, ConstantsWP.ZENOPTIONS_MANTRA_WIDTH);
			this.mMantraSelector.Resize(0, num, ConstantsWP.ZENOPTIONS_MANTRA_SELECTOR_WIDTH, ConstantsWP.ZENOPTIONS_MANTRA_SELECTOR_HEIGHT + 75);
			this.mMantraSelector.SetThreshold(ConstantsWP.ZENOPTIONS_SLIDER_HOR_THRESHOLD, 0);
			this.mMantraSelector.AddItem(11, 784);
			this.mMantraSelector.AddItem(12, 783);
			this.mMantraSelector.AddItem(13, 786);
			this.mMantraSelector.AddItem(14, 788);
			this.mMantraSelector.AddItem(15, 787);
			this.mMantraSelector.AddItem(16, 791);
			this.mMantraSelector.AddItem(17, 793);
			this.AddWidget(this.mMantraSelector);
			this.mMantrasWidgets.Add(this.mMantraSelector);
			num += ConstantsWP.ZENOPTIONS_MANTRA_SELECTOR_HEIGHT + ConstantsWP.ZENOPTIONS_SLIDE_ITEM_LABEL_OFFSET_Y;
			this.mMantraItemNameLabel = new Label(GlobalMembersResources.FONT_DIALOG, "");
			this.mMantraItemNameLabel.Resize(ConstantsWP.ZENOPTIONS_CENTER_X, num, 0, 0);
			this.mMantraItemNameLabel.SetLayerColor(0, Bej3Widget.COLOR_DIALOG_WHITE);
			this.AddWidget(this.mMantraItemNameLabel);
			this.mMantrasWidgets.Add(this.mMantraItemNameLabel);
			this.mMantraSwipeLeftButton = new Bej3Button(16, this, Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
			this.mMantraSwipeLeftButton.Resize(ConstantsWP.ZENOPTIONS_BTN_LEFT_OFFSET, num - GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPE.mHeight / 2, 0, 0);
			this.AddWidget(this.mMantraSwipeLeftButton);
			this.mMantrasWidgets.Add(this.mMantraSwipeLeftButton);
			this.mMantraSwipeRightButton = new Bej3Button(17, this, Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE);
			this.mMantraSwipeRightButton.Resize(ConstantsWP.ZENOPTIONS_BTN_RIGHT_OFFSET, num - GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPE.mHeight / 2, 0, 0);
			this.AddWidget(this.mMantraSwipeRightButton);
			this.mMantrasWidgets.Add(this.mMantraSwipeRightButton);
			num += ConstantsWP.MANTRA_SUBLIMINAL_CHECK_OFFSET_Y;
			this.mMantra_SubliminalLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Subliminal", 521), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mMantra_SubliminalCheckbox = new Bej3Checkbox(11, this);
			this.mMantra_SubliminalCheckbox.mClippingEnabled = true;
			int num2 = ConstantsWP.ZENOPTIONS_ITEM_LEFT_OFFSET + GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX.mWidth / 2;
			this.mMantra_SubliminalCheckbox.Resize(num2, num, 0, 0);
			this.mMantra_SubliminalLabel.Resize(num2 + ConstantsWP.ZENOPTIONS_ENABLE_CHECK_OFFSET_X, this.mMantra_SubliminalCheckbox.mY + (GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX.mHeight + GlobalMembersResources.FONT_DIALOG.GetAscent()) / 2, 0, 0);
			this.AddWidget(this.mMantra_SubliminalCheckbox);
			this.AddWidget(this.mMantra_SubliminalLabel);
			this.mMantrasWidgets.Add(this.mMantra_SubliminalCheckbox);
			this.mMantrasWidgets.Add(this.mMantra_SubliminalLabel);
			num += ConstantsWP.MANTRA_SPEED_VIS_SLIDER_OFFSET_Y;
			this.mMantra_SpeedVisibilitySlider = new Bej3Slider(12, this);
			this.mMantra_SpeedVisibilitySlider.Resize(ConstantsWP.ZENOPTIONS_ITEM_RIGHT_OFFSET - ConstantsWP.ZENOPTIONS_SLIDER_WIDTH, num, ConstantsWP.ZENOPTIONS_SLIDER_WIDTH, 0);
			this.mMantra_SpeedVisibilitySlider.SetThreshold(ConstantsWP.ZENOPTIONS_SLIDER_HOR_THRESHOLD, 0);
			this.mMantra_SpeedVisibilitySlider.mGrayOutWhenZero = false;
			this.AddWidget(this.mMantra_SpeedVisibilitySlider);
			this.mMantrasWidgets.Add(this.mMantra_SpeedVisibilitySlider);
			this.mMantra_SpeedVisibilityLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Visibility", 523), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mMantra_SpeedVisibilityLabel.Resize(ConstantsWP.ZENOPTIONS_ITEM_LEFT_OFFSET, this.mMantra_SpeedVisibilitySlider.mY + GlobalMembersResources.FONT_DIALOG.GetAscent(), 0, 0);
			this.AddWidget(this.mMantra_SpeedVisibilityLabel);
			this.mMantrasWidgets.Add(this.mMantra_SpeedVisibilityLabel);
			num = ConstantsWP.ZENOPTIONS_BREATH_MOD_DESC_Y;
			this.mBreathMod_DescLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Audio and visual feedback help modulate your breathing and create a sense of relaxation.", 3624), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE);
			this.mBreathMod_DescLabel.SetTextBlock(new Rect((GlobalMembers.gApp.mWidth - ConstantsWP.ZENOPTIONS_DESC_WIDTH) / 2, ConstantsWP.ZENOPTIONS_DESC_Y, ConstantsWP.ZENOPTIONS_DESC_WIDTH, ConstantsWP.ZENOPTIONS_BREATH_MOD_DESC_HEIGHT), true);
			this.mBreathMod_DescLabel.SetTextBlockEnabled(true);
			this.mBreathMod_DescLabel.SetTextBlockAlignment(0);
			this.AddWidget(this.mBreathMod_DescLabel);
			this.mBreathModWidgets.Add(this.mBreathMod_DescLabel);
			num += ConstantsWP.ZENOPTIONS_BREATH_MOD_ENABLE_OFFSET_Y;
			int num3 = ConstantsWP.ZENOPTIONS_ITEM_LEFT_OFFSET + GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX.mWidth / 2;
			this.mBreathModCheckbox = new Bej3Checkbox(0, this);
			this.mBreathModCheckbox.mClippingEnabled = true;
			this.mBreathModCheckbox.Resize(num3, num, 0, 0);
			this.AddWidget(this.mBreathModCheckbox);
			this.mBreathModWidgets.Add(this.mBreathModCheckbox);
			this.mBreathModEnable = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Enable", 3625), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mBreathModEnable.Resize(num3 + ConstantsWP.ZENOPTIONS_ENABLE_CHECK_OFFSET_X, this.mBreathModCheckbox.mY + (GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX.mHeight + GlobalMembersResources.FONT_DIALOG.GetAscent()) / 2, 0, 0);
			this.AddWidget(this.mBreathModEnable);
			this.mBreathModWidgets.Add(this.mBreathModEnable);
			num += ConstantsWP.ZENOPTIONS_BREATHMOD_VIS_CHECK_OFFSET_Y;
			this.mBreathMod_VisualIndicatorLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Visual Indicator", 516), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mBreathMod_VisualIndicatorCheckbox = new Bej3Checkbox(6, this);
			this.mBreathMod_VisualIndicatorCheckbox.mClippingEnabled = true;
			this.mBreathMod_VisualIndicatorCheckbox.Resize(num3, num, 0, 0);
			this.mBreathMod_VisualIndicatorLabel.Resize(num3 + ConstantsWP.ZENOPTIONS_ENABLE_CHECK_OFFSET_X, this.mBreathMod_VisualIndicatorCheckbox.mY + (GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX.mHeight + GlobalMembersResources.FONT_DIALOG.GetAscent()) / 2, 0, 0);
			this.AddWidget(this.mBreathMod_VisualIndicatorCheckbox);
			this.AddWidget(this.mBreathMod_VisualIndicatorLabel);
			this.mBreathModWidgets.Add(this.mBreathMod_VisualIndicatorCheckbox);
			this.mBreathModWidgets.Add(this.mBreathMod_VisualIndicatorLabel);
			num += ConstantsWP.BREATHMOD_SPEED_SLIDER_OFFSET_Y;
			this.mBreathMod_SpeedSlider = new Bej3Slider(7, this);
			this.mBreathMod_SpeedSlider.Resize(ConstantsWP.ZENOPTIONS_ITEM_RIGHT_OFFSET - ConstantsWP.ZENOPTIONS_SLIDER_WIDTH, num, ConstantsWP.ZENOPTIONS_SLIDER_WIDTH, 0);
			this.mBreathMod_SpeedSlider.SetThreshold(ConstantsWP.ZENOPTIONS_SLIDER_HOR_THRESHOLD, 0);
			this.mBreathMod_SpeedSlider.mGrayOutWhenZero = false;
			this.AddWidget(this.mBreathMod_SpeedSlider);
			this.mBreathModWidgets.Add(this.mBreathMod_SpeedSlider);
			this.mBreathMod_SpeedLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Speed", 517), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mBreathMod_SpeedLabel.Resize(ConstantsWP.ZENOPTIONS_ITEM_LEFT_OFFSET, this.mBreathMod_SpeedSlider.mY + GlobalMembersResources.FONT_DIALOG.GetAscent(), 0, 0);
			this.AddWidget(this.mBreathMod_SpeedLabel);
			this.mBreathModWidgets.Add(this.mBreathMod_SpeedLabel);
			num += ConstantsWP.BREATHMOD_VOL_SLIDER_OFFSET_Y;
			this.mBreathMod_VolumeSlider = new Bej3Slider(8, this);
			this.mBreathMod_VolumeSlider.Resize(ConstantsWP.ZENOPTIONS_ITEM_RIGHT_OFFSET - ConstantsWP.ZENOPTIONS_SLIDER_WIDTH, num, ConstantsWP.ZENOPTIONS_SLIDER_WIDTH, 0);
			this.mBreathMod_VolumeSlider.SetThreshold(ConstantsWP.ZENOPTIONS_SLIDER_HOR_THRESHOLD, 0);
			this.AddWidget(this.mBreathMod_VolumeSlider);
			this.mBreathModWidgets.Add(this.mBreathMod_VolumeSlider);
			this.mBreathMod_VolumeLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Volume", 518), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mBreathMod_VolumeLabel.Resize(ConstantsWP.ZENOPTIONS_ITEM_LEFT_OFFSET, this.mBreathMod_VolumeSlider.mY + GlobalMembersResources.FONT_DIALOG.GetAscent(), 0, 0);
			this.AddWidget(this.mBreathMod_VolumeLabel);
			this.mBreathModWidgets.Add(this.mBreathMod_VolumeLabel);
			this.mTotalHeight = num + ConstantsWP.BREATHMOD_VOL_SLIDER_OFFSET_Y;
			this.Resize(0, 0, ConstantsWP.ZENOPTIONS_CENTER_X * 2, GlobalMembers.gApp.mHeight);
		}

		public void SliderReleased(int theId, double theVal)
		{
		}

		public override void LinkUpAssets()
		{
			if (GlobalMembers.gApp.mProfile.mAmbientSelection <= 0 || GlobalMembers.gApp.mProfile.mAmbientSelection >= 9)
			{
				GlobalMembers.gApp.mProfile.mAmbientSelection = 1;
			}
			if (GlobalMembers.gApp.mProfile.mMantraSelection <= 10 || GlobalMembers.gApp.mProfile.mMantraSelection >= 18)
			{
				GlobalMembers.gApp.mProfile.mMantraSelection = 11;
			}
			this.mBreathModCheckbox.SetChecked(GlobalMembers.gApp.mProfile.mBreathOn, false);
			this.mBreathMod_VisualIndicatorCheckbox.SetChecked(GlobalMembers.gApp.mProfile.mBreathVisual, false);
			this.mBreathMod_SpeedSlider.SetValue((double)GlobalMembers.gApp.mProfile.mBreathSpeed);
			this.mBreathMod_VolumeSlider.SetValue(GlobalMembers.gApp.mZenBreathVolume);
			this.mAmbientVolumeSlider.SetValue(GlobalMembers.gApp.mZenAmbientVolume);
			this.mMantra_SubliminalCheckbox.SetChecked(GlobalMembers.gApp.mProfile.mAffirmationSubliminal, false);
			if (GlobalMembers.gApp.mProfile.mAffirmationSubliminal)
			{
				this.mMantra_SpeedVisibilityLabel.SetText(GlobalMembers._ID("Visibility", 3517));
				this.mMantra_SpeedVisibilitySlider.SetValue((double)GlobalMembers.gApp.mProfile.mAffirmationSubliminality);
			}
			else
			{
				this.mMantra_SpeedVisibilityLabel.SetText(GlobalMembers._ID("Speed", 3518));
				this.mMantra_SpeedVisibilitySlider.SetValue((double)GlobalMembers.gApp.mProfile.mAffirmationSpeed);
			}
			this.mBreathModCheckbox.LinkUpAssets();
			this.mMantraSelector.SetDisabled(false);
			this.mMantraSelector.LinkUpAssets();
			this.mAmbientSoundSelector.SetDisabled(false);
			this.mAmbientSoundSelector.LinkUpAssets();
			this.mAmbientSoundSelector.CenterOnItem(GlobalMembers.gApp.mProfile.mAmbientSelection, true);
			this.mAmbientSoundItemNameLabel.SetText(this.GetAmbienceName(GlobalMembers.gApp.mProfile.mAmbientSelection));
			this.mMantraSelector.CenterOnItem(GlobalMembers.gApp.mProfile.mMantraSelection, true);
			this.mMantraItemNameLabel.SetText(this.GetMantraName(GlobalMembers.gApp.mProfile.mMantraSelection));
			base.LinkUpAssets();
			this.GrayOutOptions();
		}

		public void GrayOutOptions()
		{
			switch (this.mMode)
			{
			case ZenOptionsMode.MODE_AMBIENT_SOUNDS:
			{
				bool flag = !GlobalMembers.gApp.mProfile.mNoiseOn;
				this.mAmbientVolumeSlider.SetDisabled(flag);
				this.mAmbientVolumeLabel.mGrayedOut = (this.mAmbientVolumeSlider.mGrayedOut = flag || this.mAmbientVolumeSlider.mVal == 0.0);
				this.mAmbientVolumeSlider.LinkUpAssets();
				return;
			}
			case ZenOptionsMode.MODE_MANTRAS:
			{
				bool flag2 = !GlobalMembers.gApp.mProfile.mAffirmationOn;
				this.mMantra_SpeedVisibilitySlider.SetDisabled(flag2);
				this.mMantra_SubliminalCheckbox.SetDisabled(flag2);
				this.mMantra_SpeedVisibilityLabel.mGrayedOut = (this.mMantra_SpeedVisibilitySlider.mGrayedOut = flag2);
				this.mMantra_SubliminalLabel.mGrayedOut = flag2;
				this.mMantra_SpeedVisibilitySlider.LinkUpAssets();
				return;
			}
			case ZenOptionsMode.MODE_BREATH_MOD:
			{
				bool flag3 = !GlobalMembers.gApp.mProfile.mBreathOn;
				this.mBreathMod_VolumeSlider.SetDisabled(flag3);
				this.mBreathMod_SpeedSlider.SetDisabled(flag3);
				this.mBreathMod_VisualIndicatorCheckbox.SetDisabled(flag3);
				this.mBreathMod_VolumeLabel.mGrayedOut = (this.mBreathMod_VolumeSlider.mGrayedOut = flag3 || this.mBreathMod_VolumeSlider.mVal == 0.0);
				this.mBreathMod_VisualIndicatorLabel.mGrayedOut = flag3;
				this.mBreathMod_SpeedLabel.mGrayedOut = (this.mBreathMod_SpeedSlider.mGrayedOut = flag3);
				this.mBreathMod_VolumeSlider.LinkUpAssets();
				this.mBreathMod_SpeedSlider.LinkUpAssets();
				return;
			}
			default:
				return;
			}
		}

		public override void Draw(Graphics g)
		{
			if (this.mMode == ZenOptionsMode.MODE_AMBIENT_SOUNDS)
			{
				Bej3Widget.DrawSwipeInlay(g, this.mAmbientSoundSelector.mY - ConstantsWP.ZENOPTIONS_INLAY_PAD_Y, this.mAmbientSoundSelector.mHeight + ConstantsWP.ZENOPTIONS_INLAY_PAD_Y * 3 - 75, this.mWidth, true);
				return;
			}
			if (this.mMode == ZenOptionsMode.MODE_MANTRAS)
			{
				Bej3Widget.DrawSwipeInlay(g, this.mMantraSelector.mY - ConstantsWP.ZENOPTIONS_INLAY_PAD_Y, this.mAmbientSoundSelector.mHeight + ConstantsWP.ZENOPTIONS_INLAY_PAD_Y * 3 - 75, this.mWidth, true);
			}
		}

		public override void Update()
		{
			base.Update();
			bool flag = false;
			if (this.mAmbientSoundId >= 0 && --this.mAmbientSoundStartDelay <= 0)
			{
				flag = this.AmbientLoadSound(this.mAmbientSoundId);
				this.mAmbientSoundId = -1;
			}
			if (flag && GlobalMembers.gApp.mProfile.mNoiseOn && !GlobalMembers.gApp.IsMuted() && GlobalMembers.gApp.mBoard != null)
			{
				ZenBoard zenBoard = (ZenBoard)GlobalMembers.gApp.mBoard;
				zenBoard.PlayZenNoise();
			}
		}

		public override void ButtonMouseEnter(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 14:
			{
				int itemId = GlobalMembers.MAX(1, this.mAmbientSoundSelector.GetSelectedId() - 1);
				this.mAmbientSoundSelector.CenterOnItem(itemId, false);
				return;
			}
			case 15:
			{
				int itemId2 = GlobalMembers.MIN(8, this.mAmbientSoundSelector.GetSelectedId() + 1);
				this.mAmbientSoundSelector.CenterOnItem(itemId2, false);
				return;
			}
			case 16:
			{
				int itemId3 = GlobalMembers.MAX(11, this.mMantraSelector.GetSelectedId() - 1);
				this.mMantraSelector.CenterOnItem(itemId3, false);
				return;
			}
			case 17:
			{
				int itemId4 = GlobalMembers.MIN(17, this.mMantraSelector.GetSelectedId() + 1);
				this.mMantraSelector.CenterOnItem(itemId4, false);
				return;
			}
			default:
				return;
			}
		}

		public virtual void SliderVal(int theId, double theVal)
		{
			switch (theId)
			{
			case 7:
				GlobalMembers.gApp.mProfile.mBreathSpeed = (float)theVal;
				break;
			case 8:
				GlobalMembers.gApp.mZenBreathVolume = (double)((float)theVal);
				if (!GlobalMembers.gApp.IsMuted())
				{
					ZenBoard zenBoard = GlobalMembers.gApp.mBoard as ZenBoard;
					if (zenBoard != null)
					{
						zenBoard.mBreathSoundInstance.SetVolume(GlobalMembers.gApp.mZenBreathVolume);
					}
					GlobalMembers.gApp.mSoundManager.SetVolume(4, GlobalMembers.gApp.mZenBreathVolume);
				}
				break;
			case 10:
				GlobalMembers.gApp.mZenAmbientVolume = (double)((float)theVal);
				if (!GlobalMembers.gApp.IsMuted())
				{
					ZenBoard zenBoard2 = GlobalMembers.gApp.mBoard as ZenBoard;
					if (zenBoard2 != null && zenBoard2.mNoiseSoundInstance != null)
					{
						zenBoard2.mNoiseSoundInstance.SetVolume(GlobalMembers.gApp.mZenAmbientVolume);
					}
					GlobalMembers.gApp.mSoundManager.SetVolume(2, GlobalMembers.gApp.mZenAmbientVolume);
				}
				break;
			case 12:
				if (this.mMantra_SubliminalCheckbox.IsChecked())
				{
					GlobalMembers.gApp.mProfile.mAffirmationSubliminality = (float)theVal;
				}
				else
				{
					GlobalMembers.gApp.mProfile.mAffirmationSpeed = (float)theVal;
				}
				break;
			}
			this.GrayOutOptions();
		}

		public void CheckboxChecked(int theId, bool checked1)
		{
			if (theId != 0)
			{
				if (theId != 6)
				{
					if (theId == 11)
					{
						GlobalMembers.gApp.mProfile.mAffirmationSubliminal = checked1;
						if (checked1 && GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN && GlobalMembers.gApp.mBoard != null)
						{
							((ZenBoard)GlobalMembers.gApp.mBoard).mAffirmationPct = 0.751f;
						}
						if (checked1)
						{
							this.mMantra_SpeedVisibilityLabel.SetText(GlobalMembers._ID("Visibility", 3519));
							this.mMantra_SpeedVisibilitySlider.SetValue((double)GlobalMembers.gApp.mProfile.mAffirmationSubliminality);
						}
						else
						{
							this.mMantra_SpeedVisibilityLabel.SetText(GlobalMembers._ID("Speed", 3520));
							this.mMantra_SpeedVisibilitySlider.SetValue((double)GlobalMembers.gApp.mProfile.mAffirmationSpeed);
						}
					}
				}
				else
				{
					GlobalMembers.gApp.mProfile.mBreathVisual = checked1;
				}
			}
			else
			{
				GlobalMembers.gApp.mProfile.mBreathOn = checked1;
			}
			this.GrayOutOptions();
		}

		public virtual void SlideSelectorChanged(int theSlideSelectorId, int theItemId)
		{
			if (theSlideSelectorId == 9)
			{
				this.AmbientSoundChanged(theItemId);
				return;
			}
			if (theSlideSelectorId != 13)
			{
				return;
			}
			this.MantraSelected(theItemId);
		}

		public void AmbientSoundChanged(int theId)
		{
			bool flag = GlobalMembers.gApp.mProfile.mNoiseOn;
			GlobalMembers.gApp.mProfile.mNoiseOn = theId != 1;
			flag = flag != GlobalMembers.gApp.mProfile.mNoiseOn;
			if (flag)
			{
				this.GrayOutOptions();
			}
			bool flag2 = true;
			if (GlobalMembers.gApp.mProfile.mAmbientSelection == theId)
			{
				flag2 = false;
			}
			ZenBoard zenBoard = null;
			if (GlobalMembers.gApp.mBoard != null)
			{
				zenBoard = GlobalMembers.gApp.mBoard as ZenBoard;
			}
			if (flag2 && zenBoard != null)
			{
				zenBoard.StopZenNoise();
				this.mAmbientSoundStartDelay = ConstantsWP.ZENOPTIONS_AMBIENT_SOUND_START_DELAY_FRAMES;
				this.mAmbientSoundId = theId;
			}
			bool flag3 = false;
			if (flag2)
			{
				if (this.mAmbientSoundStartDelay > 0)
				{
					this.mAmbientSoundItemNameLabel.SetText(this.GetAmbienceName(theId));
				}
				else
				{
					flag3 = this.AmbientLoadSound(theId);
				}
			}
			if (flag3 && GlobalMembers.gApp.mProfile.mNoiseOn && !GlobalMembers.gApp.IsMuted())
			{
				zenBoard.PlayZenNoise();
			}
		}

		public bool AmbientLoadSound(int theId)
		{
			GlobalMembers.gApp.mProfile.mAmbientSelection = theId;
			this.mAmbientSoundItemNameLabel.SetText(this.GetAmbienceName(theId));
			switch (theId)
			{
			case 1:
				GlobalMembers.gApp.mProfile.mNoiseFileName = string.Empty;
				break;
			case 2:
				GlobalMembers.gApp.mProfile.mNoiseFileName = "*";
				break;
			case 3:
				GlobalMembers.gApp.mProfile.mNoiseFileName = "Ocean Surf.caf";
				break;
			case 4:
				GlobalMembers.gApp.mProfile.mNoiseFileName = "Crickets.caf";
				break;
			case 5:
				GlobalMembers.gApp.mProfile.mNoiseFileName = "Rain Leaves.caf";
				break;
			case 6:
				GlobalMembers.gApp.mProfile.mNoiseFileName = "Waterfall.caf";
				break;
			case 7:
				GlobalMembers.gApp.mProfile.mNoiseFileName = "Coastal.caf";
				break;
			case 8:
				GlobalMembers.gApp.mProfile.mNoiseFileName = "Forest.caf";
				break;
			}
			ZenBoard zenBoard = null;
			if (GlobalMembers.gApp.mBoard != null)
			{
				zenBoard = (ZenBoard)GlobalMembers.gApp.mBoard;
			}
			if (zenBoard != null)
			{
				zenBoard.LoadAmbientSound();
			}
			return zenBoard != null && zenBoard.mNoiseSoundInstance != null;
		}

		public void MantraSelected(int theId)
		{
			bool flag = GlobalMembers.gApp.mProfile.mAffirmationOn;
			GlobalMembers.gApp.mProfile.mAffirmationOn = theId != 11;
			flag = flag != GlobalMembers.gApp.mProfile.mAffirmationOn;
			if (flag)
			{
				this.GrayOutOptions();
			}
			bool flag2 = GlobalMembers.gApp.mProfile.mMantraSelection != theId;
			GlobalMembers.gApp.mProfile.mMantraSelection = theId;
			this.mMantraItemNameLabel.SetText(this.GetMantraName(theId));
			switch (theId)
			{
			case 12:
				GlobalMembers.gApp.mProfile.mAffirmationFileName = "General.txt";
				break;
			case 13:
				GlobalMembers.gApp.mProfile.mAffirmationFileName = "Positive Thinking.txt";
				break;
			case 14:
				GlobalMembers.gApp.mProfile.mAffirmationFileName = "Quit Bad Habits.txt";
				break;
			case 15:
				GlobalMembers.gApp.mProfile.mAffirmationFileName = "Prosperity.txt";
				break;
			case 16:
				GlobalMembers.gApp.mProfile.mAffirmationFileName = "Self Confidence.txt";
				break;
			case 17:
				GlobalMembers.gApp.mProfile.mAffirmationFileName = "Weight Loss.txt";
				break;
			}
			ZenBoard zenBoard = GlobalMembers.gApp.mBoard as ZenBoard;
			if (zenBoard != null && flag2)
			{
				zenBoard.LoadAffirmations();
			}
		}

		public string GetAmbienceName(int theId)
		{
			return ZenOptionsContainer.ambientSounds[theId - 1];
		}

		public string GetMantraName(int theId)
		{
			return ZenOptionsContainer.mantras[theId - 10 - 1];
		}

		public override void Show()
		{
			this.mAlphaCurve.SetConstant(1.0);
			base.Show();
			this.mY = this.mTargetPos;
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
		}

		public override void Hide()
		{
			this.mAlphaCurve.SetConstant(1.0);
			base.Hide();
		}

		public void SetMode(ZenOptionsMode mode)
		{
			this.mMode = mode;
			foreach (Widget w in this.mAmbientSoundWidgets)
			{
				Bej3Widget.DisableWidget(w, mode != ZenOptionsMode.MODE_AMBIENT_SOUNDS);
			}
			foreach (Widget w2 in this.mMantrasWidgets)
			{
				Bej3Widget.DisableWidget(w2, mode != ZenOptionsMode.MODE_MANTRAS);
			}
			foreach (Widget w3 in this.mBreathModWidgets)
			{
				Bej3Widget.DisableWidget(w3, mode != ZenOptionsMode.MODE_BREATH_MOD);
			}
			this.GrayOutOptions();
		}

		public override void PlayMenuMusic()
		{
		}

		public ZenOptionsMode mMode;

		public List<Widget> mAmbientSoundWidgets = new List<Widget>();

		public List<Widget> mMantrasWidgets = new List<Widget>();

		public List<Widget> mBreathModWidgets = new List<Widget>();

		public Label mDescLabel;

		public bool mEnabled;

		public int mTotalHeight;

		public Label mBreathMod_DescLabel;

		public Bej3Checkbox mBreathModCheckbox;

		public Label mBreathModEnable;

		public Label mBreathMod_VisualIndicatorLabel;

		public Bej3Checkbox mBreathMod_VisualIndicatorCheckbox;

		public Label mBreathMod_SpeedLabel;

		public Bej3Slider mBreathMod_SpeedSlider;

		public Label mBreathMod_VolumeLabel;

		public Bej3Slider mBreathMod_VolumeSlider;

		public int mAmbientSoundStartDelay;

		public int mAmbientSoundId;

		public Label mAmbientSoundDescLabel;

		public Label mAmbientVolumeLabel;

		public Bej3Slider mAmbientVolumeSlider;

		public Bej3SlideSelector mAmbientSoundSelector;

		public Label mAmbientSoundItemNameLabel;

		public Bej3Button mAmbientSwipeLeftButton;

		public Bej3Button mAmbientSwipeRightButton;

		public Label mMantra_DescLabel;

		public Label mMantra_SubliminalLabel;

		public Bej3Checkbox mMantra_SubliminalCheckbox;

		public Label mMantra_SpeedVisibilityLabel;

		public Bej3Slider mMantra_SpeedVisibilitySlider;

		public Bej3SlideSelector mMantraSelector;

		public Label mMantraItemNameLabel;

		public Bej3Button mMantraSwipeLeftButton;

		public Bej3Button mMantraSwipeRightButton;

		private static string[] ambientSounds = new string[]
		{
			GlobalMembers._ID("None", 3461),
			GlobalMembers._ID("Random", 3462),
			GlobalMembers._ID("Ocean Surf", 3463),
			GlobalMembers._ID("Crickets", 3464),
			GlobalMembers._ID("Rain Leaves", 3465),
			GlobalMembers._ID("Waterfall", 3466),
			GlobalMembers._ID("Coastal", 3467),
			GlobalMembers._ID("Forest", 3468)
		};

		private static string[] mantras = new string[]
		{
			GlobalMembers._ID("None", 3469),
			GlobalMembers._ID("General", 3470),
			GlobalMembers._ID("Positive Thinking", 3471),
			GlobalMembers._ID("Quit Bad Habits", 3472),
			GlobalMembers._ID("Prosperity", 3473),
			GlobalMembers._ID("Self-Confidence", 3474),
			GlobalMembers._ID("Weight Loss", 3475)
		};
	}
}
