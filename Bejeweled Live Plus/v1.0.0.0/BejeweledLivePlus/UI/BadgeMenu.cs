using System;
using System.Collections.Generic;
using BejeweledLivePlus.Localization;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	internal class BadgeMenu : Bej3Widget, Bej3ButtonListener, ButtonListener, Bej3ScrollWidgetListener, ScrollWidgetListener
	{
		private void SetUpSlideButtons()
		{
			if (this.IsInProfileMenu())
			{
				return;
			}
			int pageHorizontal = this.mScrollWidget.GetPageHorizontal();
			bool flag = pageHorizontal > 0;
			this.mSlideLeftButton.SetVisible(flag);
			this.mSlideLeftButton.SetDisabled(!flag);
			flag = pageHorizontal < 2;
			this.mSlideRightButton.SetVisible(flag);
			this.mSlideRightButton.SetDisabled(!flag);
		}

		public BadgeMenu(bool isInProfileMenu)
			: base(Menu_Type.MENU_BADGEMENU, true, Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
		{
			this.mCurrentPage = 0;
			this.mScrollingToPage = 0;
			this.Resize(0, ConstantsWP.MENU_Y_POS_HIDDEN, ConstantsWP.BADGE_MENU_WIDTH, GlobalMembers.gApp.mHeight);
			this.mFinalY = 106;
			this.mBadgeManager = BadgeManager.GetBadgeManagerInstance();
			int num = 80;
			this.mHeadingLabel = new Label(GlobalMembersResources.FONT_HUGE);
			this.mHeadingLabel.Resize(ConstantsWP.BADGE_MENU_HEADING_X, ConstantsWP.DIALOG_HEADING_LABEL_Y, 0, 0);
			this.mHeadingLabel.SetText(GlobalMembers._ID("ACHIEVEMENTS", 4011));
			this.mHeadingLabel.SetMaximumWidth(ConstantsWP.DIALOG_HEADING_LABEL_MAX_WIDTH);
			this.mCumuBadgeScoreLabel = new Label(GlobalMembersResources.FONT_DIALOG);
			this.AddWidget(this.mCumuBadgeScoreLabel);
			this.mCumuBadgeScoreLabel.Resize(ConstantsWP.BADGE_MENU_HEADING_X, ConstantsWP.DIALOG_HEADING_LABEL_Y + 100, 0, 0);
			this.mCumuBadgeScoreLabel.SetText("20/200");
			this.mContainer = new BadgeMenuContainer(this);
			this.mScrollWidget = new Bej3ScrollWidget(this, false);
			this.mScrollWidget.Resize(ConstantsWP.BADGEMENU_CONTAINER_PADDING_X, ConstantsWP.BADGEMENU_CONTAINER_TOP, this.mWidth - ConstantsWP.BADGEMENU_CONTAINER_PADDING_X * 2, ConstantsWP.BADGEMENU_CONTAINER_HEIGHT + num);
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.EnablePaging(true);
			this.mScrollWidget.SetScrollInsets(new Insets(0, 0, 0, 0));
			this.mScrollWidget.AddWidget(this.mContainer);
			this.mScrollWidget.SetPageHorizontal(0, false);
			this.AddWidget(this.mScrollWidget);
			this.mCloseButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_LONG, true);
			this.mCloseButton.SetLabel(GlobalMembers._ID("CLOSE", 3487));
			Bej3Widget.CenterWidgetAt(this.mWidth / 2, ConstantsWP.MENU_BOTTOM_BUTTON_Y + num, this.mCloseButton, true, false);
			this.mCloseButton.mBtnNoDraw = true;
			this.AddWidget(this.mCloseButton);
			if (!this.IsInProfileMenu())
			{
				this.mSlideLeftButton = new Bej3Button(1, this, Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
				this.mSlideLeftButton.Resize(ConstantsWP.BADGEMENU_SLIDE_BUTTON_OFFSET_X, ConstantsWP.BADGEMENU_SLIDE_BUTTON_Y + this.BeHigher, 0, 0);
				this.AddWidget(this.mSlideLeftButton);
				this.mSlideRightButton = new Bej3Button(2, this, Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE);
				this.mSlideRightButton.Resize(0, this.BeHigher, 0, 0);
				this.AddWidget(this.mSlideRightButton);
				this.mBottomMessageLabel = null;
			}
			else
			{
				this.mSlideLeftButton = null;
				this.mSlideRightButton = null;
				this.mBottomMessageLabel = null;
			}
			this.mBottomMessageLabel = new Label(GlobalMembersResources.FONT_SUBHEADER);
			this.mBottomMessageLabel.SetTextBlock(new Rect(ConstantsWP.HIGHSCORES_MENU_BOTTOM_MESSAGE_X, ConstantsWP.BADGEMENU_SLIDE_BUTTON_Y + 35 + this.BeHigher, ConstantsWP.SLIDE_BUTTON_MESSAGE_WIDTH, 0), true);
			this.mBottomMessageLabel.SetTextBlockEnabled(true);
			this.mBottomMessageLabel.SetText(GlobalMembers._ID("Swipe for more achievments", 3552));
			this.mBottomMessageLabel.SetClippingEnabled(false);
			this.mBottomMessageLabel.SetLayerColor(1, Bej3Widget.COLOR_SUBHEADING_3_FILL);
			this.mBottomMessageLabel.SetLayerColor(0, Bej3Widget.COLOR_SUBHEADING_3_STROKE);
			this.AddWidget(this.mBottomMessageLabel);
			this.SetMode(BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_MENU, null);
			base.SystemButtonPressed += this.OnSystemButtonPressed;
		}

		private void OnSystemButtonPressed(SystemButtonPressedArgs args)
		{
			if (args.button == SystemButtons.Back && !base.IsInOutPosition())
			{
				args.processed = true;
				this.ButtonDepress(10001);
			}
		}

		public override void PlayMenuMusic()
		{
		}

		public override void Draw(Graphics g)
		{
			g.SetColor(Color.White);
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			g.SetColorizeImages(true);
			if (this.mHeadingLabel != null && this.mHeadingLabel.mVisible)
			{
				g.Translate(this.mHeadingLabel.mX, this.mHeadingLabel.mY);
				this.mHeadingLabel.Draw(g);
				g.Translate(-this.mHeadingLabel.mX, -this.mHeadingLabel.mY);
			}
			if (this.mCloseButton != null && this.mCloseButton.mVisible)
			{
				this.mCloseButton.mBtnNoDraw = false;
				g.Translate(this.mCloseButton.mX, this.mCloseButton.mY);
				this.mCloseButton.Draw(g);
				g.Translate(-this.mCloseButton.mX, -this.mCloseButton.mY);
				this.mCloseButton.mBtnNoDraw = true;
			}
			Bej3Widget.DrawSwipeInlay(g, this.mScrollWidget.mY + GlobalMembers.S(10), this.mScrollWidget.mHeight - GlobalMembers.S(150), this.mWidth, true);
			base.DeferOverlay(0);
		}

		public override void DrawOverlay(Graphics g)
		{
			if ((this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING || this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_PROFILE_AWARDING) && this.mBadgeAlpha > 0.0 && this.mCurrentBadge < this.mDeferredBadgeVector.size<int>())
			{
				g.Translate(-this.mX, -this.mY);
				Graphics3D graphics3D = g.Get3D();
				graphics3D = null;
				g.SetColor(Color.White);
				this.mBadgeManager.SetBadge(this.mDeferredBadgeVector[this.mCurrentBadge]);
				Badge mBadge = this.mBadgeManager.mBadge;
				SexyTransform2D theTransform = new SexyTransform2D(true);
				if (graphics3D != null)
				{
					graphics3D.PopTransform(ref theTransform);
				}
				Color color = g.GetColor();
				bool colorizeImages = g.GetColorizeImages();
				g.SetColorizeImages(true);
				g.SetColor(new Color(0, 0, 0, (int)(this.mBadgeAlpha * this.mAwardShadowAlpha * (double)ConstantsWP.BADGEMENU_AWARDED_ALPHA)));
				g.FillRect(-(int)g.mTransX, -(int)g.mTransY, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
				int num = (this.mWidth - (int)((float)GlobalMembersResourcesWP.IMAGE_AWARD_GLOW.GetWidth() * ConstantsWP.BOARD_BADGE_AWARD_SCALE)) / 2;
				int num2 = (this.mHeight - (int)((float)GlobalMembersResourcesWP.IMAGE_AWARD_GLOW.GetHeight() * ConstantsWP.BOARD_BADGE_AWARD_SCALE)) / 2;
				g.SetScale(ConstantsWP.BOARD_BADGE_AWARD_SCALE, ConstantsWP.BOARD_BADGE_AWARD_SCALE, (float)num, (float)num2);
				int num3 = (int)(this.mBadgeAlpha * this.mAwardShadowAlpha * (double)GlobalMembers.M(255));
				g.SetColor(new Color(0, 0, 0, (int)((float)num3 * GlobalMembers.M(1f))));
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_AWARD_GLOW, num, num2);
				g.SetColor(new Color(num3 * (int)GlobalMembers.M(0.7f), num3 * (int)GlobalMembers.M(1f), num3 * (int)GlobalMembers.M(0.9f)));
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_AWARD_GLOW, num, num2);
				g.mScaleX = 1f;
				g.mScaleY = 1f;
				g.SetDrawMode(Graphics.DrawMode.Normal);
				string theString = string.Format(GlobalMembers._ID("{0}^FFFFFF^ \"{1}\" Badge", 71), mBadge.GetBadgeLevelName(), mBadge.GetTooltipHeader());
				string awardString = mBadge.GetAwardString();
				g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
				g.SetColor(new Color(255, 224, 0, (int)(this.mBadgeAlpha * this.mAwardShadowAlpha * 255.0)));
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 1, Bej3Widget.COLOR_SUBHEADING_6_FILL);
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 0, Bej3Widget.COLOR_SUBHEADING_6_STROKE);
				g.WriteString(GlobalMembers._ID("You have earned the", 3488), this.mWidth / 2, ConstantsWP.BADGEMENU_AWARDED_TOP_TEXT_Y);
				g.SetFont(GlobalMembersResources.FONT_DIALOG);
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_DIALOG, 0, Bej3Widget.COLOR_DIALOG_WHITE);
				int num4 = 0;
				g.SetColor(new Color(255, 255, 255, (int)(this.mBadgeAlpha * this.mAwardShadowAlpha * 255.0)));
				g.WriteString(theString, this.mWidth / 2, num4 + ConstantsWP.BADGEMENU_AWARDED_TOP_TEXT_Y + GlobalMembersResources.FONT_SUBHEADER.GetHeight());
				if (Strings.LANG == "DE-DE")
				{
					g.WriteString("-Abzeichen", this.mWidth / 2, num4 + ConstantsWP.BADGEMENU_AWARDED_TOP_TEXT_Y + 2 * GlobalMembersResources.FONT_SUBHEADER.GetHeight() + 3);
				}
				g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
				g.SetColor(new Color(255, 224, 0, (int)(this.mBadgeAlpha * this.mAwardShadowAlpha * 255.0)));
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 1, Bej3Widget.COLOR_SUBHEADING_6_FILL);
				Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_SUBHEADER, 0, Bej3Widget.COLOR_SUBHEADING_6_STROKE);
				g.WriteWordWrapped(new Rect(ConstantsWP.BADGEMENU_AWARDED_DESC_PAD_X, ConstantsWP.BADGEMENU_AWARDED_BTM_TEXT_Y, this.mWidth - ConstantsWP.BADGEMENU_AWARDED_DESC_PAD_X * 2, this.mHeight - ConstantsWP.BADGEMENU_AWARDED_BTM_TEXT_Y), awardString, -1, 0);
				g.SetColor(Color.White);
				float num5 = (float)((double)(1f - GlobalMembers.M(0.36f)) * this.mBadgeScale + (double)GlobalMembers.M(0.36f));
				Transform transform = new Transform();
				transform.Scale(num5, num5);
				Point absoluteBadgePosition = this.mContainer.GetAbsoluteBadgePosition(mBadge.mIdx, true);
				int num6 = (int)((double)(this.mWidth / 2) * this.mBadgeScale + (double)absoluteBadgePosition.mX * (1.0 - this.mBadgeScale));
				int num7 = (int)((double)(this.mHeight / 2) * this.mBadgeScale + (double)absoluteBadgePosition.mY * (1.0 - this.mBadgeScale));
				int num8 = this.mScaledBadgeLevel;
				this.mBadgeIcon = BadgeMenu.aBadgeIds[mBadge.mIdx];
				this.mBadgeRing = BadgeMenu.aRingIds[num8];
				g.DrawImageTransform(this.mBadgeIcon, transform, (float)num6, (float)num7);
				g.DrawImageTransform(this.mBadgeRing, transform, (float)num6, (float)num7);
				g.SetColorizeImages(colorizeImages);
				g.SetColor(color);
				if (graphics3D != null)
				{
					graphics3D.PushTransform(theTransform);
				}
			}
			if (this.mBadgeEffect != null)
			{
				this.mBadgeEffect.Draw(g);
			}
		}

		public override void Update()
		{
			base.Update();
			base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
			if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING || this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_PROFILE_AWARDING)
			{
				if (this.mBadgeScale < (double)GlobalMembers.M(0.1f))
				{
					if (this.mBadgeEffect == null)
					{
						if (this.mCurrentBadge < this.mDeferredBadgeVector.size<int>())
						{
							this.InitBadgeEffect();
							this.mBadgeStatus[this.mDeferredBadgeVector[this.mCurrentBadge]] = this.mBadgeManager.mBadgeClass[this.mDeferredBadgeVector[this.mCurrentBadge]].mUnlocked;
						}
					}
					else if (this.mBadgeEffect.HasTimelineExpired())
					{
						this.mBadgeEffect = null;
						if (this.mCurrentBadge < this.mDeferredBadgeVector.size<int>())
						{
							this.DoNextBadge();
						}
					}
				}
				if (this.mBadgeEffect != null)
				{
					this.mBadgeEffect.Update();
				}
				if (this.mWidgetScale == 0.0 && this.mBadgeAnimPct.IsDoingCurve() && this.mBadgeScale < 1.0)
				{
					GlobalMembers.OutputDebugStrF("BadgeMenu: Begin dialog scale\n");
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_WIDGET_SCALE, this.mWidgetScale);
					if (this.mHeadingLabel != null)
					{
						this.mHeadingLabel.SetVisible(true);
					}
				}
				bool flag = this.mCurrentBadge < this.mDeferredBadgeVector.size<int>();
				if (this.mCloseButton != null)
				{
					this.mCloseButton.SetDisabled(flag);
				}
				if (!flag && this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_PROFILE_AWARDING)
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
					if (this.mTopButton != null)
					{
						this.mTopButton.SetDisabled(false);
					}
					this.mBadgemenuState = BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_MENU;
				}
				if (this.mAwardShadowAlpha < 0.40000000596046448)
				{
					this.mAllowSlide = true;
				}
				this.mWidgetScale.IncInVal();
			}
		}

		public override void ButtonMouseEnter(int theId)
		{
		}

		public override void ButtonDepress(int theId)
		{
			GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
			switch (theId)
			{
			case 0:
				if (this.mBadgemenuState != BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING)
				{
					GlobalMembers.gApp.DoMainMenu();
					base.Transition_SlideOut();
					return;
				}
				if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN)
				{
					GlobalMembers.gApp.GoBackToGame();
					base.Transition_SlideOut();
					return;
				}
				if (GlobalMembers.gApp.mBoard != null)
				{
					GlobalMembers.gApp.mBoard.SubmitHighscore();
				}
				GlobalMembers.gApp.DoGameDetailMenu(GlobalMembers.gApp.mCurrentGameMode, GameDetailMenu.GAMEDETAILMENU_STATE.STATE_POST_GAME);
				base.Transition_SlideOut();
				GlobalMembers.gApp.mMenus[6].AllowSlideIn(false, this.mTopButton);
				base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
				return;
			case 1:
				if (this.mScrollWidget.GetScrollMode() != 0)
				{
					this.mScrollingToPage = this.mScrollWidget.GetPageHorizontal() - 1;
					this.mScrollWidget.SetPageHorizontal(this.mScrollingToPage, true);
				}
				break;
			case 2:
				if (this.mScrollWidget.GetScrollMode() != 0)
				{
					this.mScrollingToPage = this.mScrollWidget.GetPageHorizontal() + 1;
					this.mScrollWidget.SetPageHorizontal(this.mScrollingToPage, true);
					return;
				}
				break;
			default:
				if (theId != 10001)
				{
					return;
				}
				if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING)
				{
					GlobalMembers.gApp.GoBackToGame();
				}
				else
				{
					GlobalMembers.gApp.DoMainMenu();
				}
				base.Transition_SlideOut();
				return;
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			if (!this.IsInProfileMenu())
			{
				this.mSlideRightButton.LinkUpAssets();
				this.mSlideRightButton.Resize(this.mWidth - this.mSlideRightButton.mWidth - ConstantsWP.BADGEMENU_SLIDE_BUTTON_OFFSET_X, ConstantsWP.BADGEMENU_SLIDE_BUTTON_Y + this.BeHigher, 0, 0);
			}
			this.SetUpSlideButtons();
		}

		public override void Show()
		{
			if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING || this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_PROFILE_AWARDING)
			{
				BejeweledLivePlusApp.LoadContent("AwardGlow");
			}
			BejeweledLivePlusApp.LoadContent("Badges");
			this.LinkUpAssets();
			this.mContainer.Show();
			base.Show();
			base.ResetFadedBack(true);
			this.SetVisible(false);
			this.mY = ConstantsWP.MENU_Y_POS_HIDDEN;
			this.AddWidget(GlobalMembers.gApp.mTooltipManager);
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
			if (!this.IsInProfileMenu())
			{
				this.mSlideLeftButton.EnableSlideGlow(true);
				this.mSlideRightButton.EnableSlideGlow(true);
			}
		}

		public override void Hide()
		{
			this.mContainer.Hide();
			base.Hide();
			this.RemoveWidget(GlobalMembers.gApp.mTooltipManager);
		}

		public override void HideCompleted()
		{
			if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING)
			{
				BejeweledLivePlusApp.UnloadContent("AwardGlow");
				this.UnloadBigBadge();
				this.SetMode(BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_MENU, null);
			}
			BejeweledLivePlusApp.UnloadContent("Badges");
			base.HideCompleted();
		}

		public void DoNextBadge()
		{
			this.UnloadBigBadge();
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_ANIM_PCT_2, this.mBadgeAnimPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_SCALE_2, this.mBadgeScale, this.mBadgeAnimPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_ALPHA_2, this.mBadgeAlpha, this.mBadgeAnimPct);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_AWARD_SHADOW_ALPHA_2, this.mAwardShadowAlpha, this.mBadgeAnimPct);
			this.mCurrentBadge++;
			if (this.mCurrentBadge < this.mDeferredBadgeVector.size<int>())
			{
				Badge badge = this.mBadgeManager.mBadgeClass[this.mDeferredBadgeVector[this.mCurrentBadge]];
				if (badge != null)
				{
					int badgePage = BadgeMenuContainer.GetBadgePage(this.mBadgeManager.mBadgeClass[this.mDeferredBadgeVector[this.mCurrentBadge]].mIdx);
					if (this.mCurrentPage != badgePage)
					{
						this.mScrollingToPage = badgePage;
						this.mScrollWidget.SetPageHorizontal(this.mScrollingToPage, true);
					}
				}
				GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
				GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BADGEAWARDED);
				this.mScaledBadgeLevel = this.mBadgeManager.mBadgeClass[this.mDeferredBadgeVector[this.mCurrentBadge]].mLevel - BadgeLevel.BADGELEVEL_BRONZE;
				return;
			}
			GlobalMembers.gApp.mProfile.mDeferredBadgeVector.Clear();
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_HORIZONTAL);
		}

		public void UnloadBigBadge()
		{
			if (this.mBadgeIconRef.HasResource())
			{
				this.mBadgeIconRef.Release();
			}
			if (this.mBadgeRingRef.HasResource())
			{
				this.mBadgeRingRef.Release();
			}
		}

		public void InitBadgeEffect()
		{
			if (this.mBadgeEffect != null)
			{
				return;
			}
			this.mBadgeEffect = GlobalMembersResourcesWP.PIEFFECT_BADGE_UPGRADE.Duplicate();
			Point absoluteBadgePosition = this.mContainer.GetAbsoluteBadgePosition(this.mDeferredBadgeVector[this.mCurrentBadge], true);
			this.mBadgeEffect.mDrawTransform.Scale(GlobalMembers.S(1f), GlobalMembers.S(1f));
			this.mBadgeEffect.mDrawTransform.Translate((float)absoluteBadgePosition.mX, (float)absoluteBadgePosition.mY);
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BADGEFALL);
		}

		public void SetMode(BadgeMenu.BADGEMENU_STATE state, List<int> deferredBadgeVector)
		{
			this.mBadgemenuState = state;
			if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING || this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_PROFILE_AWARDING)
			{
				this.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
				this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_DISABLED);
				if (GlobalMembers.gApp.mCurrentGameMode == GameMode.MODE_ZEN)
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS);
				}
				else
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
				}
				this.mDeferredBadgeVector.Resize(deferredBadgeVector.Count);
				for (int i = 0; i < deferredBadgeVector.Count; i++)
				{
					this.mDeferredBadgeVector[i] = deferredBadgeVector[i];
				}
				this.mWidgetScale.SetConstant(0.0);
				this.mCurrentBadge = -1;
				this.mBadgeEffect = null;
				this.mScaledBadgeLevel = 0;
				for (int j = 0; j < 20; j++)
				{
					this.mBadgeStatus[j] = this.mBadgeManager.mBadgeClass[j].mUnlocked;
				}
				this.mBadgeManager.SyncBadges();
				this.DoNextBadge();
				this.mCloseButton.SetDisabled(true);
				if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING)
				{
					this.mCloseButton.SetLabel(GlobalMembers._ID("CONTINUE", 3088));
				}
				else
				{
					this.mCloseButton.SetLabel(GlobalMembers._ID("BACK", 3089));
				}
				if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING)
				{
					this.mCloseButton.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_GREEN);
				}
				else
				{
					this.mCloseButton.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
				}
				this.mAllowSlide = false;
			}
			else if (state == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_MENU)
			{
				this.mDeferredBadgeVector.Clear();
				this.mBadgeManager.SyncBadges();
				for (int k = 0; k < 20; k++)
				{
					this.mBadgeStatus[k] = GlobalMembers.gApp.mProfile.mBadgeStatus[k];
				}
				this.mBadgeAnimPct = new CurvedVal();
				this.mBadgeScale = new CurvedVal();
				this.mBadgeAlpha = new CurvedVal();
				this.mAwardShadowAlpha = new CurvedVal();
				this.mWidgetScale = new CurvedVal();
				this.mWidgetScale.SetConstant(1.0);
				this.mCurrentBadge = -1;
				this.mBadgeEffect = null;
				this.mScaledBadgeLevel = 0;
				this.mCloseButton.SetDisabled(false);
				this.mCloseButton.SetVisible(true);
				this.mCloseButton.SetLabel(GlobalMembers._ID("BACK", 3090));
				this.mCloseButton.SetType(Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE);
				if (this.mTopButton != null)
				{
					this.mTopButton.mBtnNoDraw = false;
				}
			}
			int num = 0;
			foreach (Badge badge in this.mBadgeManager.mBadgeClass)
			{
				if (badge.mUnlocked)
				{
					num += badge.mGPoints;
				}
			}
			this.mCumuBadgeScoreLabel.SetText(string.Format("G : {0}/200", num));
		}

		public new int GetState()
		{
			return (int)this.mBadgemenuState;
		}

		public static Image GetSmallBadgeImage(int badgeId)
		{
			Image[] array = new Image[0];
			return array[badgeId];
		}

		public override void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			bool mAllowSlide = false;
			if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING || this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_PROFILE_AWARDING)
			{
				mAllowSlide = this.mAllowSlide;
			}
			base.AllowSlideIn(allow, previousTopButton);
			if (this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING || this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_PROFILE_AWARDING)
			{
				this.mAllowSlide = mAllowSlide;
				if (this.mInterfaceState == InterfaceState.INTERFACE_STATE_BADGEMENU)
				{
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_MENU);
					base.SetTopButtonType(Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED);
				}
			}
			this.mContainer.AllowSlideIn(allow, previousTopButton);
		}

		public bool ContainerTouchEnded(SexyAppBase.Touch touch)
		{
			if ((this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_AWARDING || this.mBadgemenuState == BadgeMenu.BADGEMENU_STATE.BADGEMENU_STATE_PROFILE_AWARDING) && this.mCurrentBadge < this.mDeferredBadgeVector.size<int>())
			{
				if (this.mBadgeAnimPct.GetInVal() < 0.75 && this.mBadgeScale == 1.0)
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_ANIM_PCT_1, this.mBadgeAnimPct);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_SCALE_1, this.mBadgeScale, this.mBadgeAnimPct);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_ALPHA_1, this.mBadgeAlpha, this.mBadgeAnimPct);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBADGE_MENU_AWARD_SHADOW_ALPHA_1, this.mAwardShadowAlpha, this.mBadgeAnimPct);
					GlobalMembers.OutputDebugStrF("BadgeMenu: Mouse click - Spawn next badge\n");
				}
				else
				{
					if (this.mBadgeEffect != null)
					{
						this.mBadgeEffect.Clear();
						this.mBadgeEffect = null;
					}
					int num = this.mDeferredBadgeVector[this.mCurrentBadge];
					this.mBadgeStatus[num] = this.mBadgeManager.mBadgeClass[num].mUnlocked;
					this.DoNextBadge();
					GlobalMembers.OutputDebugStrF("BadgeMenu: Mouse click - Dispatch of current badge\n");
				}
				return true;
			}
			return false;
		}

		public virtual void PageChanged(Bej3ScrollWidget scrollWidget, int pageH, int pageV)
		{
			if (this.mCurrentPage != pageH)
			{
				GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
			}
			this.mCurrentPage = pageH;
			this.SetUpSlideButtons();
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
			int pageHorizontal = scrollWidget.GetPageHorizontal();
			if (this.mCurrentPage != pageHorizontal)
			{
				this.mCurrentPage = pageHorizontal;
				this.LinkUpAssets();
			}
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public bool IsInProfileMenu()
		{
			return false;
		}

		private PIEffect mBadgeEffect;

		private int mScaledBadgeLevel;

		private ResourceRef mBadgeIconRef = new ResourceRef();

		private ResourceRef mBadgeRingRef = new ResourceRef();

		private Image mBadgeIcon;

		private Image mBadgeRing;

		private CurvedVal mWidgetScale = new CurvedVal();

		private BadgeMenuContainer mContainer;

		private int mScrollingToPage;

		private CurvedVal mBadgeAnimPct = new CurvedVal();

		private CurvedVal mBadgeScale = new CurvedVal();

		private CurvedVal mBadgeAlpha = new CurvedVal();

		private CurvedVal mAwardShadowAlpha = new CurvedVal();

		private Label mHeadingLabel;

		private Bej3Button mCloseButton;

		private Bej3Button mSlideLeftButton;

		private Bej3Button mSlideRightButton;

		private Label mBottomMessageLabel;

		private Label mCumuBadgeScoreLabel;

		public Bej3ScrollWidget mScrollWidget;

		public int mCurrentPage;

		public BadgeMenu.BADGEMENU_STATE mBadgemenuState;

		public int mCurrentBadge;

		public List<int> mDeferredBadgeVector = new List<int>();

		public BadgeManager mBadgeManager;

		public int BeHigher = 60;

		public int[] mBadgeLevels = new int[20];

		public bool[] mBadgeStatus = new bool[20];

		private static Image[] aBadgeIds = new Image[]
		{
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_LEVELORD,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BEJEWELER,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_DIAMOND_MINE,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_DIAMOND_MINE,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_RELIC_HUNTER,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_RELIC_HUNTER,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ELECTRIFIER,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ELECTRIFIER,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_HIGH_VOLTAGE,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_HIGH_VOLTAGE,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BUTTERFLY_MONARCH,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BUTTERFLY_MONARCH,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BUTTERFLY_BONANZA,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BUTTERFLY_BONANZA,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_CHROMATIC,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_STELLAR,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BLASTER,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_SUPERSTAR,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_CHAIN_REACTION,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_LUCKY_STREAK
		};

		private static Image[] aRingIds = new Image[]
		{
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BRONZE,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_SILVER,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_GOLD,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_PLATINUM,
			GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ELITE
		};

		public enum BADGEMENU_STATE
		{
			BADGEMENU_STATE_MENU,
			BADGEMENU_STATE_AWARDING,
			BADGEMENU_STATE_PROFILE_AWARDING
		}

		public enum BTN_BADGEMENU_ID
		{
			BTN_CLOSE_BADGES_ID,
			BTN_LEFT_ID,
			BTN_RIGHT_ID
		}

		public enum BADGE_MENU_PAGE
		{
			BADGE_MENU_PAGE_COUNT = 3
		}
	}
}
