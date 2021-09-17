using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class HelpDialog : Bej3Widget, CheckboxListener, Bej3ScrollWidgetListener, ScrollWidgetListener
	{
		private void HandleCloseButton()
		{
			switch (this.mHelpDialogState)
			{
			case HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_PREGAME:
				((PauseMenu)GlobalMembers.gApp.mMenus[7]).mComingFromHelp = true;
				GlobalMembers.gApp.mProfile.SetTutorialCleared(HelpDialog.mTutorialFlag);
				GlobalMembers.gApp.StartSetupGame(true);
				base.Transition_SlideOut();
				((PauseMenu)GlobalMembers.gApp.mMenus[7]).mComingFromHelp = false;
				return;
			case HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_INGAME:
				((PauseMenu)GlobalMembers.gApp.mMenus[7]).mComingFromHelp = true;
				GlobalMembers.gApp.DoPauseMenu();
				((PauseMenu)GlobalMembers.gApp.mMenus[7]).Expand();
				this.mVisible = true;
				base.Transition_SlideOut();
				((PauseMenu)GlobalMembers.gApp.mMenus[7]).mComingFromHelp = false;
				return;
			case HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_MAINMENU:
				GlobalMembers.gApp.DoMainMenu(true);
				base.Transition_SlideOut();
				return;
			case HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_OPTIONS:
				GlobalMembers.gApp.DoOptionsMenu();
				base.Transition_SlideOut();
				return;
			case HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_FIRSTGAME:
				((PauseMenu)GlobalMembers.gApp.mMenus[7]).mComingFromHelp = true;
				GlobalMembers.gApp.mProfile.SetTutorialCleared(HelpDialog.mTutorialFlag);
				GlobalMembers.gApp.StartSetupGame(true);
				if (this.mIsBack)
				{
					((PauseMenu)GlobalMembers.gApp.mMenus[7]).ButtonDepress(10001);
				}
				base.Transition_SlideOut();
				((PauseMenu)GlobalMembers.gApp.mMenus[7]).mComingFromHelp = false;
				return;
			default:
				return;
			}
		}

		private void SetUpSlideButtons()
		{
			int pageHorizontal = this.mScrollWidget.GetPageHorizontal();
			bool flag = pageHorizontal > 0;
			this.mSlideLeftButton.SetVisible(flag);
			this.mSlideLeftButton.SetDisabled(!flag);
			flag = pageHorizontal < this.mNumWindows - 1;
			this.mSlideRightButton.SetVisible(flag);
			this.mSlideRightButton.SetDisabled(!flag);
		}

		public HelpDialog()
			: base(Menu_Type.MENU_HELPMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.mCurrentPage = 0;
			this.mScrollingToPage = 0;
			this.mNumWindows = 0;
			this.mIsSetUp = false;
			this.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mFinalY = 0;
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.Resize(ConstantsWP.HELPDIALOG_HEADING_X, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetText(GlobalMembers._ID("HELP", 3337));
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.AddWidget(this.mHeadingLabel);
			int num = 30;
			int theHeight = 50;
			this.mSwipeMsgLabel = new Label(GlobalMembersResources.FONT_SUBHEADER);
			this.mSwipeMsgLabel.SetTextBlock(new Rect(ConstantsWP.HELPDIALOG_SWIPE_MSG_LABEL_X, ConstantsWP.HELPDIALOG_SWIPE_MSG_LABEL_Y - num - 10, ConstantsWP.SLIDE_BUTTON_MESSAGE_WIDTH, theHeight), true);
			this.mSwipeMsgLabel.SetTextBlockEnabled(true);
			this.mSwipeMsgLabel.SetClippingEnabled(false);
			this.mSwipeMsgLabel.SetText(GlobalMembers._ID("Swipe for more help", 3338));
			this.mSwipeMsgLabel.SetLayerColor(1, Bej3Widget.COLOR_SUBHEADING_3_FILL);
			this.mSwipeMsgLabel.SetLayerColor(0, Bej3Widget.COLOR_SUBHEADING_3_STROKE);
			this.AddWidget(this.mSwipeMsgLabel);
			this.mDisableHintLabel = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Help", 3339), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mDisableHintLabel.Resize(ConstantsWP.HELPDIALOG_DISABLE_HINT_LABEL_X, ConstantsWP.HELPDIALOG_DISABLE_HINT_LABEL_Y, 0, 0);
			this.mCheckbox = new Bej3Checkbox(0, this);
			this.mCheckbox.Resize(ConstantsWP.HELPDIALOG_DISABLE_HINT_X, ConstantsWP.HELPDIALOG_DISABLE_HINT_Y, 0, 0);
			this.mCheckbox.mGrayOutWhenDisabled = false;
			ConstantsWP.HELPDIALOG_DIVIDER_BOX_1_W += this.mDisableHintLabel.GetTextWidth();
			int num2 = this.mWidth / 2 - ConstantsWP.HELPDIALOG_DIVIDER_BOX_1_W / 2;
			num2 = ConstantsWP.HELPDIALOG_DIVIDER_BOX_1_X - num2;
			ConstantsWP.HELPDIALOG_DIVIDER_BOX_1_X -= num2;
			this.mCheckbox.mX -= num2;
			this.mDisableHintLabel.mX -= num2;
			this.mContainer = new HelpDialogContainer();
			this.mScrollWidget = new Bej3ScrollWidget(this, false);
			this.mScrollWidget.Resize(ConstantsWP.HELPDIALOG_CONTAINER_X, ConstantsWP.HELPDIALOG_CONTAINER_Y, ConstantsWP.HELPDIALOG_CONTAINER_WIDTH, ConstantsWP.HELPDIALOG_CONTAINER_HEIGHT + 75);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.AddWidget(this.mContainer);
			this.mScrollWidget.EnablePaging(true);
			this.mScrollWidget.SetScrollInsets(new Insets(0, 0, ConstantsWP.HELPDIALOG_CONTAINER_TAB_PADDING, 0));
			this.AddWidget(this.mScrollWidget);
			this.mCloseButton = new Bej3Button(3, this, Bej3ButtonType.BUTTON_TYPE_LONG, true);
			Bej3Widget.CenterWidgetAt(this.mWidth / 2, ConstantsWP.HELPDIALOG_CLOSE_Y, this.mCloseButton, true, false);
			this.AddWidget(this.mCloseButton);
			this.mSlideLeftButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
			this.mSlideLeftButton.Resize(ConstantsWP.HELPDIALOG_SLIDE_BUTTON_OFFSET_X - 18, ConstantsWP.HELPDIALOG_SLIDE_BUTTON_Y - 15, 0, 0);
			this.AddWidget(this.mSlideLeftButton);
			this.mSlideRightButton = new Bej3Button(2, this, Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE);
			this.mSlideRightButton.Resize(0, 0, 0, 0);
			this.AddWidget(this.mSlideRightButton);
			for (int i = 0; i < 4; i++)
			{
				this.mHelpWindow[i] = null;
			}
			this.mIsBack = false;
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.mIsBack = true;
				this.ButtonDepress(3);
				this.mIsBack = false;
			}
		}

		public static Rect GetWindowRect(int window)
		{
			return HelpDialog.GetWindowRect(window, false);
		}

		public static Rect GetWindowRect(int window, bool useLargePadding)
		{
			Rect result = new Rect(ConstantsWP.HELPDIALOG_CONTAINER_TAB_PADDING + window * (ConstantsWP.HELPDIALOG_CONTAINER_TAB_PADDING + ConstantsWP.HELPDIALOG_CONTAINER_TAB_WIDTH), ConstantsWP.HELPDIALOG_WINDOW_Y, ConstantsWP.HELPDIALOG_CONTAINER_TAB_WIDTH, ConstantsWP.HELPDIALOG_WINDOW_HEIGHT);
			return result;
		}

		public static string GetContentName(int tutorialFlag)
		{
			if (tutorialFlag != 0)
			{
				switch (tutorialFlag)
				{
				case 21:
					break;
				case 22:
					return "Help_DiamondMine";
				default:
					return "";
				}
			}
			return "Help_Basic";
		}

		public static int GetWindowCountForMode(int tutorialFlags)
		{
			if (tutorialFlags != 0 && tutorialFlags != 10)
			{
				switch (tutorialFlags)
				{
				case 17:
					return 2;
				case 18:
				case 20:
				case 21:
				case 22:
					return 3;
				}
				return 1;
			}
			return 3;
		}

		public void SetupHelp()
		{
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eHELP_DIALOG_ALPHA, HelpWindow.mHelpAlpha);
			this.mNumWindows = HelpDialog.GetWindowCountForMode(HelpDialog.mTutorialFlag);
			for (int i = 0; i < this.mNumWindows; i++)
			{
				this.mHelpWindow[i] = new HelpWindow();
				this.mHelpWindow[i].SetVisible(true);
				this.mHelpWindow[i].Resize(HelpDialog.GetWindowRect(i));
				this.mContainer.AddWidget(this.mHelpWindow[i]);
			}
			this.mContainer.SetNumberOfWindows(this.mNumWindows, false);
			this.mScrollWidget.ClientSizeChanged();
			this.mCurrentPage = (this.mScrollingToPage = 0);
			this.mScrollWidget.SetPageHorizontal(this.mCurrentPage, false);
			if (HelpDialog.mTutorialFlag == 0 || HelpDialog.mTutorialFlag == 21)
			{
				if (HelpDialog.mTutorialFlag == 21)
				{
					this.mHelpWindow[0].mHeaderText = GlobalMembers._ID("Score as many points as possible until there are no more moves.", 243);
				}
				this.mHelpWindow[0].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_SWAP3);
				this.mHelpWindow[0].mXOfs.Add(ConstantsWP.HELPDIALOG_OFFSET_BASICS_1);
				this.mHelpWindow[0].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[0].mCaptions.Add(GlobalMembers._ID("Swap adjacent gems to make rows of three.", 244));
				this.mHelpWindow[1].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_MATCH4);
				this.mHelpWindow[1].mXOfs.Add(ConstantsWP.HELPDIALOG_OFFSET_BASICS_2);
				this.mHelpWindow[1].mTextWidthScale.Add(GlobalMembers.M(0f));
				this.mHelpWindow[1].mCaptions.Add(GlobalMembers._ID("Match 4 or more gems to create Special Gems.", 245));
				this.mHelpWindow[2].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_STARGEM);
				this.mHelpWindow[2].mXOfs.Add(ConstantsWP.HELPDIALOG_OFFSET_BASICS_3);
				this.mHelpWindow[2].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[2].mCaptions.Add(GlobalMembers._ID("Make an L or T match to create a Star Gem!", 246));
			}
			else if (HelpDialog.mTutorialFlag == 10)
			{
				this.mHelpWindow[0].SetVisible(true);
				this.mHelpWindow[0].mHeaderText = "";
				this.mHelpWindow[0].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_LIGHTNING_MATCH);
				this.mHelpWindow[0].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[0].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[0].mCaptions.Add(GlobalMembers._ID("Match Time Gems to earn extra time.", 247));
				FrameAnimation frameAnimation = new FrameAnimation(GlobalMembersResourcesWP.ATLASIMAGE_EX_HELP_LIGHTNING_01, "atlases\\help_lightning_01.plist");
				this.mHelpWindow[0].AddWidget(frameAnimation);
				frameAnimation.Resize(94, 30, 190, 248);
				this.mHelpWindow[1].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_LIGHTNING_TIME);
				this.mHelpWindow[1].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[1].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[1].mCaptions.Add(GlobalMembers._ID("Extra time is added to your next round, where point values increase!", 248));
				FrameAnimation frameAnimation2 = new FrameAnimation(GlobalMembersResourcesWP.ATLASIMAGE_EX_HELP_LIGHTNING_02, "atlases\\help_lightning_02.plist");
				this.mHelpWindow[1].AddWidget(frameAnimation2);
				frameAnimation2.Resize(77, 30, 256, 248);
				this.mHelpWindow[2].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_LIGHTNING_SPEED);
				this.mHelpWindow[2].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[2].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[2].mCaptions.Add(GlobalMembers._ID("Make matches quickly for a Speed Bonus. Max it out for Blazing Speed!", 249));
				FrameAnimation frameAnimation3 = new FrameAnimation(GlobalMembersResourcesWP.ATLASIMAGE_EX_HELP_LIGHTNING_03, "atlases\\help_lightning_03.plist");
				this.mHelpWindow[2].AddWidget(frameAnimation3);
				frameAnimation3.Resize(94, 30, 190, 248);
			}
			else if (HelpDialog.mTutorialFlag == 17)
			{
				this.mHelpWindow[0].SetVisible(true);
				this.mHelpWindow[0].mHeaderText = "";
				this.mHelpWindow[0].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_BFLY_MATCH);
				this.mHelpWindow[0].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[0].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[0].mCaptions.Add(GlobalMembers._ID("Match butterfly gems with like colored gems to release them.", 250));
				this.mHelpWindow[1].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_BFLY_SPIDER);
				this.mHelpWindow[1].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[1].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[1].mCaptions.Add(GlobalMembers._ID("Don't let any of the butterflies reach the spider!", 251));
			}
			else if (HelpDialog.mTutorialFlag == 18)
			{
				this.mHelpWindow[0].SetVisible(true);
				this.mHelpWindow[0].mHeaderText = "";
				this.mHelpWindow[0].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_POKER_MATCH);
				this.mHelpWindow[0].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[0].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[0].mCaptions.Add(GlobalMembers._ID("Make poker hands with gem matches.", 252));
				this.mHelpWindow[1].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_POKER_SKULLHAND);
				this.mHelpWindow[1].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[1].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[1].mCaptions.Add(GlobalMembers._ID("When Skulls appear, try to avoid the hands that they occupy.", 253));
				this.mHelpWindow[2].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_POKER_SKULL_CLEAR);
				this.mHelpWindow[2].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[2].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[2].mCaptions.Add(GlobalMembers._ID("Remove Skulls by filling the Eliminator bar.  Better hands fill it faster.", 254));
			}
			else if (HelpDialog.mTutorialFlag == 20)
			{
				this.mHelpWindow[0].SetVisible(true);
				this.mHelpWindow[0].mHeaderText = GlobalMembers._ID("Score as many points as you can before the ice reaches the top!", 255);
				this.mHelpWindow[0].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_ICESTORM_HORIZ);
				this.mHelpWindow[0].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[0].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[0].mCaptions.Add(GlobalMembers._ID("Make matches to push down the rising ice columns.", 256));
				this.mHelpWindow[1].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_ICESTORM_VERT);
				this.mHelpWindow[1].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[1].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[1].mCaptions.Add(GlobalMembers._ID("Make vertical matches to smash ice columns and earn mega bonus points.", 257));
				this.mHelpWindow[2].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_ICESTORM_METER);
				this.mHelpWindow[2].mXOfs.Add(GlobalMembers.M(0));
				this.mHelpWindow[2].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[2].mCaptions.Add(GlobalMembers._ID("Clearing ice fills the blue meter and increases your score multiplier.", 258));
			}
			else if (HelpDialog.mTutorialFlag == 22)
			{
				this.mHelpWindow[0].mHeaderText = "";
				this.mHelpWindow[0].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_DIAMOND_MATCH);
				this.mHelpWindow[0].mXOfs.Add(ConstantsWP.HELPDIALOG_OFFSET_DIAMOND_1);
				this.mHelpWindow[0].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[0].mCaptions.Add(GlobalMembers._ID("Make matches directly next to ground to dig down.", 259));
				this.mHelpWindow[1].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_DIAMOND_ADVANCE);
				this.mHelpWindow[1].mXOfs.Add(ConstantsWP.HELPDIALOG_OFFSET_DIAMOND_2);
				this.mHelpWindow[1].mTextWidthScale.Add(GlobalMembers.M(0f));
				this.mHelpWindow[1].mCaptions.Add(GlobalMembers._ID("Clear all ground tiles down to the white line to advance.", 260));
				this.mHelpWindow[2].mPopAnims.Add(GlobalMembersResourcesWP.POPANIM_HELP_DIAMOND_GOLD);
				this.mHelpWindow[2].mXOfs.Add(ConstantsWP.HELPDIALOG_OFFSET_DIAMOND_3);
				this.mHelpWindow[2].mTextWidthScale.Add(GlobalMembers.M(1f));
				this.mHelpWindow[2].mCaptions.Add(GlobalMembers._ID("Make matches next to gold, gems and artifacts to earn points.", 261));
			}
			for (int j = 0; j < this.mNumWindows; j++)
			{
				for (int k = 0; k < this.mHelpWindow[j].mPopAnims.size<PopAnim>(); k++)
				{
					PopAnim popAnim = this.mHelpWindow[j].mPopAnims[k];
					popAnim.mClip = true;
					for (int l = 0; l < popAnim.mMainSpriteInst.mParticleEffectVector.size<PAParticleEffect>(); l++)
					{
						PAParticleEffect paparticleEffect = popAnim.mMainSpriteInst.mParticleEffectVector[l];
					}
					for (int m = 0; m < popAnim.mMainSpriteInst.mChildren.size<PAObjectInst>(); m++)
					{
						PASpriteInst mSpriteInst = popAnim.mMainSpriteInst.mChildren[m].mSpriteInst;
						if (mSpriteInst != null)
						{
							for (int n = 0; n < mSpriteInst.mParticleEffectVector.size<PAParticleEffect>(); n++)
							{
								PAParticleEffect paparticleEffect2 = mSpriteInst.mParticleEffectVector[n];
							}
						}
					}
					this.mHelpWindow[j].mPopAnims[k].Play();
				}
			}
			this.LinkUpAssets();
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public override void Update()
		{
			base.Update();
			if ((this.mState == Bej3WidgetState.STATE_FADING_IN || this.mState == Bej3WidgetState.STATE_IN) && this.mAllowSlide && !this.mIsSetUp)
			{
				BejeweledLivePlusApp.LoadContent(HelpDialog.GetContentName(HelpDialog.mTutorialFlag));
				this.SetupHelp();
				this.mIsSetUp = true;
				this.mContainer.SetVisible(this.mIsSetUp);
				GlobalMembers.gApp.ClearUpdateBacklog(false);
			}
			this.SetUpSlideButtons();
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawSwipeInlay(g, this.mScrollWidget.mY, this.mScrollWidget.mHeight - 75, this.mWidth, true);
			this.mHasDrawn = true;
		}

		public override void DrawOverlay(Graphics g)
		{
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mContainer.LinkUpAssets();
			this.mCheckbox.SetChecked((GlobalMembers.gApp.mProfile.mTutorialFlags & 524288UL) != 0UL, false);
			this.mSlideLeftButton.LinkUpAssets();
			this.mSlideRightButton.LinkUpAssets();
			this.mSlideRightButton.Resize(this.mWidth - this.mSlideRightButton.mWidth - ConstantsWP.HELPDIALOG_SLIDE_BUTTON_OFFSET_X + 18, ConstantsWP.HELPDIALOG_SLIDE_BUTTON_Y - 15, 0, 0);
			this.mScrollWidget.SetPageHorizontal(this.mCurrentPage, true);
			this.SetUpSlideButtons();
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			base.AllowSlideIn(allow, previousTopButton);
			if (allow && this.mHelpDialogState == HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_PREGAME)
			{
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
			}
		}

		public override void Show()
		{
			this.mIsSetUp = false;
			BejeweledLivePlusApp.LoadContent(HelpDialog.GetContentName(HelpDialog.mTutorialFlag));
			this.mContainer.Show();
			base.Show();
			this.mTargetPos = 106;
			this.mY = ConstantsWP.MENU_Y_POS_HIDDEN;
			this.mContainer.SetVisible(true);
			switch (this.mHelpDialogState)
			{
			case HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_PREGAME:
				if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN)
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
				}
				else
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
				}
				break;
			case HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_INGAME:
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
				break;
			}
			this.SetVisible(false);
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
			this.mSlideLeftButton.EnableSlideGlow(true);
			this.mSlideRightButton.EnableSlideGlow(true);
		}

		public override void Hide()
		{
			base.Hide();
			this.mContainer.Hide();
		}

		public override void HideCompleted()
		{
			base.HideCompleted();
			for (int i = 0; i < 4; i++)
			{
				GlobalMembers.KILL_WIDGET(this.mHelpWindow[i]);
			}
			BejeweledLivePlusApp.UnloadContent(HelpDialog.GetContentName(HelpDialog.mTutorialFlag));
			GlobalMembers.gApp.mProfile.SetTutorialCleared(HelpDialog.mTutorialFlag);
			if (HelpDialog.mTutorialFlag == 21)
			{
				GlobalMembers.gApp.mProfile.SetTutorialCleared(0);
			}
			if (HelpDialog.mTutorialFlag == 0)
			{
				GlobalMembers.gApp.mProfile.SetTutorialCleared(21);
			}
			GlobalMembers.gApp.mProfile.WriteProfile();
			this.mContainer.mWindowCount = 0;
		}

		public override void PlayMenuMusic()
		{
		}

		public override void SetVisible(bool isVisible)
		{
			base.SetVisible(isVisible);
			if (this.mContainer != null)
			{
				this.mContainer.SetVisible(isVisible);
			}
		}

		public void SetMode(int theTutorialFlag)
		{
			this.SetMode(theTutorialFlag, true);
		}

		public void SetMode(int theTutorialFlag, bool showCheckbox)
		{
			HelpDialog.mTutorialFlag = theTutorialFlag;
			for (int i = 0; i < this.mAnimRefVector.size<ResourceRef>(); i++)
			{
				this.mAnimRefVector[i].GetPopAnim().Play();
			}
			this.mFirstDraw = true;
			this.mHasDrawn = false;
		}

		public void SetHelpDialogState(HelpDialog.HELPDIALOG_STATE state)
		{
			this.mHelpDialogState = state;
			if (this.mHelpDialogState == HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_PREGAME || this.mHelpDialogState == HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_FIRSTGAME)
			{
				this.mCloseButton.SetLabel(GlobalMembers._ID("PLAY", 3341));
				this.mCloseButton.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
			}
			else
			{
				this.mCloseButton.SetLabel(GlobalMembers._ID("BACK", 3342));
				this.mCloseButton.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
			}
			Bej3Widget.CenterWidgetAt(this.mWidth / 2, ConstantsWP.HELPDIALOG_CLOSE_Y, this.mCloseButton, true, false);
		}

		public override void ButtonDepress(int theId)
		{
			switch (theId)
			{
			case 1:
				this.mScrollingToPage = this.mScrollWidget.GetPageHorizontal() - 1;
				this.mScrollWidget.SetPageHorizontal(this.mScrollingToPage, true);
				return;
			case 2:
				this.mScrollingToPage = this.mScrollWidget.GetPageHorizontal() + 1;
				this.mScrollWidget.SetPageHorizontal(this.mScrollingToPage, true);
				return;
			case 3:
				this.HandleCloseButton();
				return;
			default:
				if (theId != 10001)
				{
					return;
				}
				if (this.mHelpDialogState == HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_INGAME)
				{
					GlobalMembers.gApp.GoBackToGame();
					base.Transition_SlideOut();
					return;
				}
				if (this.mHelpDialogState == HelpDialog.HELPDIALOG_STATE.HELPDIALOG_STATE_PREGAME)
				{
					GlobalMembers.gApp.mProfile.SetTutorialCleared(HelpDialog.mTutorialFlag);
					GlobalMembers.gApp.DoMainMenu();
					base.Transition_SlideOut();
					return;
				}
				GlobalMembers.gApp.DoMainMenu();
				base.Transition_SlideOut();
				return;
			}
		}

		public void CheckboxChecked(int theId, bool Checked)
		{
			if (theId == 0)
			{
				GlobalMembers.gApp.mProfile.SetTutorialCleared(19, Checked);
			}
			this.LinkUpAssets();
		}

		public void PageChanged(Bej3ScrollWidget scrollWidget, int pageH, int pageV)
		{
			int num = 0;
			while (num < 4 && this.mHelpWindow[num] != null)
			{
				if (num == pageH)
				{
					this.mHelpWindow[num].mSeenByUser = true;
					this.mHelpWindow[num].SetVisible(true);
				}
				else
				{
					this.mHelpWindow[num].mSeenByUser = false;
				}
				num++;
			}
			this.mCurrentPage = pageH;
			this.SetUpSlideButtons();
		}

		public void ScrollTargetReached(ScrollWidget scrollWidget)
		{
			int pageHorizontal = scrollWidget.GetPageHorizontal();
			if (this.mCurrentPage != pageHorizontal)
			{
				this.mCurrentPage = pageHorizontal;
				this.LinkUpAssets();
			}
			int pageHorizontal2 = scrollWidget.GetPageHorizontal();
			int num = 0;
			while (num < 4 && this.mHelpWindow[num] != null)
			{
				if (num == pageHorizontal2)
				{
					this.mHelpWindow[num].mSeenByUser = true;
				}
				else
				{
					this.mHelpWindow[num].mSeenByUser = false;
					this.mHelpWindow[num].ResetAnimation();
				}
				num++;
			}
		}

		public void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		private bool mIsSetUp;

		private int mCurrentPage;

		private int mScrollingToPage;

		private int mNumWindows;

		private Label mHeadingLabel;

		private Bej3Button mCloseButton;

		private bool mIsBack;

		private bool mShowCheckbox;

		private bool mHasDrawn;

		private bool mFirstDraw;

		private List<ResourceRef> mAnimRefVector = new List<ResourceRef>();

		private Bej3Checkbox mCheckbox;

		private Label mDisableHintLabel;

		private HelpDialogContainer mContainer;

		private Bej3ScrollWidget mScrollWidget;

		private HelpWindow[] mHelpWindow = new HelpWindow[4];

		private Bej3Button mSlideLeftButton;

		private Bej3Button mSlideRightButton;

		private Label mSwipeMsgLabel;

		public static int mTutorialFlag;

		public HelpDialog.HELPDIALOG_STATE mHelpDialogState;

		public enum HELPDIALOG_STATE
		{
			HELPDIALOG_STATE_PREGAME,
			HELPDIALOG_STATE_INGAME,
			HELPDIALOG_STATE_MAINMENU,
			HELPDIALOG_STATE_OPTIONS,
			HELPDIALOG_STATE_FIRSTGAME
		}

		public enum HELP_WINDOW
		{
			HELP_WINDOW_COUNT = 4
		}

		private enum HELPDIALOG_IDS
		{
			CHK_DISABLE_HINTS,
			BTN_LEFT_ID,
			BTN_RIGHT_ID,
			BTN_CLOSE_HELP_ID
		}
	}
}
