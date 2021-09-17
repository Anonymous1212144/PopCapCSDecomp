using System;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3Button : DialogButton
	{
		private bool CalcButtonWidth(string text, int newWidth)
		{
			if (this.mFont == null || string.IsNullOrEmpty(text))
			{
				return false;
			}
			newWidth = Math.Max(this.mFont.StringWidth(text) + ConstantsWP.BEJ3BUTTON_AUTOSCALE_TEXT_WIDTH_OFFSET, ConstantsWP.BEJ3BUTTON_AUTOSCALE_MIN_WIDTH);
			return true;
		}

		protected bool IsTopButton()
		{
			return this.mType >= Bej3ButtonType.TOP_BUTTON_TYPE_NONE && this.mType <= Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS;
		}

		protected void DrawTopButton(Graphics g)
		{
			Color color = g.mPushedColorVector[g.mPushedColorVector.Count - 1];
			g.PopColorMult();
			g.ClearClipRect();
			g.SetColor(Color.White);
			if (this.mDisabled && this.mDisabledRect.mWidth > 0 && this.mDisabledRect.mHeight > 0)
			{
				g.DrawImage(this.mComponentImage, this.mInsideImageRect, this.mDisabledRect);
			}
			else if (this.IsButtonDown())
			{
				g.DrawImage(this.mComponentImage, this.mInsideImageRect, this.mDownRect);
			}
			else if (this.mOverAlpha > 0.0)
			{
				if (this.mOverAlpha < 1.0)
				{
					g.DrawImage(this.mComponentImage, this.mInsideImageRect, this.mNormalRect);
				}
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(this.mOverAlpha * 255.0)));
				g.DrawImage(this.mComponentImage, this.mInsideImageRect, this.mOverRect);
				g.SetColorizeImages(false);
			}
			else if (this.mIsOver)
			{
				g.DrawImage(this.mComponentImage, this.mInsideImageRect, this.mOverRect);
			}
			else
			{
				g.DrawImage(this.mComponentImage, this.mInsideImageRect, this.mNormalRect);
			}
			if (this.mType != Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED)
			{
				g.SetColorizeImages(true);
				int num = 255;
				if (this.mType == Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
				{
					num = 255;
				}
				else if (this.mType == Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS)
				{
					num = 255;
				}
				g.SetColor(new Color(255, 255, 255, (int)((float)num * Bej3Button.mTopButtonGlow)));
				g.DrawImage(this.mComponentImage, this.mInsideImageRect, this.mDownRect);
			}
			g.SetColor(color);
			g.PushColorMult();
		}

		protected void DrawZenSlideButton(Graphics g)
		{
			if (this.mBtnNoDraw)
			{
				return;
			}
			g.SetColorizeImages(true);
			g.SetColor(new Color((int)(255f * this.mAlpha), (int)(255f * this.mAlpha), (int)(255f * this.mAlpha), (int)(255f * this.mAlpha)));
			g.SetScale(this.mZenSize, this.mZenSize, (float)(this.mWidth / 2), (float)(this.mHeight / 2));
			g.DrawImage(this.mButtonImage, this.mWidth / 2 - this.mButtonImage.mWidth / 2, this.mHeight / 2 - this.mButtonImage.mHeight / 2);
		}

		protected void DrawBackButton(Graphics g)
		{
			Image image_DASHBOARD_MENU_UP = GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP;
			int theY = this.mHeight / 2 - image_DASHBOARD_MENU_UP.GetCelHeight() / 2;
			g.DrawImage(image_DASHBOARD_MENU_UP, this.mWidth / 2 - image_DASHBOARD_MENU_UP.GetCelWidth() / 2, theY);
		}

		protected void DrawHintOkButton(Graphics g)
		{
			Image image_DIALOG_BUTTON_SMALL_BLUE = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE;
			if (this.mIsDown && this.mIsOver)
			{
				int theX = this.mWidth / 2 - this.mDownRect.mWidth / 2;
				int theY = this.mHeight / 2 - this.mDownRect.mHeight / 2;
				g.DrawImage(image_DIALOG_BUTTON_SMALL_BLUE, theX, theY, this.mDownRect);
			}
			else
			{
				int theX = this.mWidth / 2 - this.mNormalRect.mWidth / 2;
				int theY = this.mHeight / 2 - this.mNormalRect.mHeight / 2;
				g.DrawImage(image_DIALOG_BUTTON_SMALL_BLUE, theX, theY, this.mNormalRect);
			}
			g.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			g.SetColorizeImages(true);
			g.SetColor(Color.White);
			this.s = GlobalMembers._ID("OK", 3218);
			this.width = g.StringWidth(this.s);
			g.DrawString(this.s, this.mWidth / 2 - this.width / 2 + ConstantsWP.BEJ3BUTTON_HINT_OK_OFFSET_X, this.mHeight / 2 + ConstantsWP.BEJ3BUTTON_HINT_OK_OFFSET_Y);
		}

		protected void DrawCameraButton(Graphics g)
		{
			Image image_DIALOG_BUTTON_SMALL_BLUE = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE;
			int num;
			int num2;
			if (this.mIsDown && this.mIsOver)
			{
				num = this.mWidth / 2 - this.mDownRect.mWidth / 2;
				num2 = this.mHeight / 2 - this.mDownRect.mHeight / 2;
				g.DrawImage(image_DIALOG_BUTTON_SMALL_BLUE, num, num2, this.mDownRect);
				num += (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_REPLAY_ID) - GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_BUTTON_SMALL_BLUE_ID));
				num2 += (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_REPLAY_ID) - GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_BUTTON_SMALL_BLUE_ID));
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY, num, num2, GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY.GetCelRect(0));
				return;
			}
			num = this.mWidth / 2 - this.mNormalRect.mWidth / 2;
			num2 = this.mHeight / 2 - this.mNormalRect.mHeight / 2;
			g.DrawImage(image_DIALOG_BUTTON_SMALL_BLUE, num, num2, this.mNormalRect);
			num += (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_REPLAY_ID) - GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_BUTTON_SMALL_BLUE_ID));
			num2 += (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_REPLAY_ID) - GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_BUTTON_SMALL_BLUE_ID));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY, num, num2, GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY.GetCelRect(1));
		}

		protected void DrawProfilePictureButton(Graphics g)
		{
			g.DrawImage(this.mButtonImage, this.mWidth / 2 - this.mButtonImage.GetCelWidth() / 2, this.mHeight / 2 - this.mButtonImage.GetCelHeight() / 2);
		}

		protected void DrawDropDownButton(Graphics g)
		{
			int num;
			int num2;
			if (this.mIsDown && this.mIsOver)
			{
				num = this.mWidth / 2 - this.mDownRect.mWidth / 2;
				num2 = this.mHeight / 2 - this.mDownRect.mHeight / 2;
				g.DrawImage(this.mComponentImage, num, num2, this.mDownRect);
			}
			else
			{
				num = this.mWidth / 2 - this.mNormalRect.mWidth / 2;
				num2 = this.mHeight / 2 - this.mNormalRect.mHeight / 2;
				g.DrawImage(this.mComponentImage, num, num2, this.mNormalRect);
			}
			num += ConstantsWP.BEJ3BUTTON_DROPDOWN_OFFSET_X;
			num2 += ConstantsWP.BEJ3BUTTON_DROPDOWN_OFFSET_Y;
			if (this.mTypeImageRotation == 0f)
			{
				g.DrawImage(this.mTypeImage, num, num2, this.mIconSrcRect);
				return;
			}
			g.DrawImageRotatedF(this.mTypeImage, (float)num, (float)num2, (double)this.mTypeImageRotation, ConstantsWP.BEJ3BUTTON_DROPDOWN_ROT_CENTER_X, ConstantsWP.BEJ3BUTTON_DROPDOWN_ROT_CENTER_Y, this.mIconSrcRect);
		}

		protected void DrawInfoButton(Graphics g)
		{
			int num;
			int num2;
			if (this.mIsDown && this.mIsOver)
			{
				num = this.mWidth / 2 - this.mDownRect.mWidth / 2;
				num2 = this.mHeight / 2 - this.mDownRect.mHeight / 2;
				g.DrawImage(this.mComponentImage, num, num2, this.mDownRect);
			}
			else
			{
				num = this.mWidth / 2 - this.mNormalRect.mWidth / 2;
				num2 = this.mHeight / 2 - this.mNormalRect.mHeight / 2;
				g.DrawImage(this.mComponentImage, num, num2, this.mNormalRect);
			}
			g.DrawImage(this.mTypeImage, num + this.mIconOffset.mX, num2 + this.mIconOffset.mY, this.mIconSrcRect);
		}

		protected void DrawSwipeButton(Graphics g)
		{
			if (this.mBtnNoDraw)
			{
				return;
			}
			int num = this.mWidth / 2 - this.mNormalRect.mWidth / 2;
			int num2 = this.mHeight / 2 - this.mNormalRect.mHeight / 2;
			int num3 = 0;
			if (this.mSlideGlowEnabled)
			{
				this.xg = num + (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1322) - GlobalMembersResourcesWP.ImgXOfs(1321));
				this.yg = num2 + (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1322) - GlobalMembersResourcesWP.ImgYOfs(1321));
				g.SetColorizeImages(true);
				g.SetColor(new Color(255, 255, 255, (int)(255f * Bej3Button.mSlideGlow)));
				int num4 = ((this.mType == Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE) ? (-1) : 1);
				num3 = (int)Bej3Button.mSlideGlow * num4 * ConstantsWP.BEJ3BUTTON_SLIDE_ARROW_DIST;
				g.mClipRect.mX = g.mClipRect.mX + num3;
				g.Translate(num3, 0);
				g.DrawImageMirror(GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPEGLOW, this.xg, this.yg, this.mType == Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
				g.SetColor(new Color(255, 255, 255, 255));
			}
			if (this.mIsDown)
			{
				g.DrawImageMirror(this.mComponentImage, num, num2, this.mDownRect, this.mType == Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
			}
			else
			{
				g.DrawImageMirror(this.mComponentImage, num, num2, this.mNormalRect, this.mType == Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE);
			}
			g.Translate(-num3, 0);
		}

		protected void DrawGameCenterButton(Graphics g)
		{
			Image image_DIALOG_BUTTON_GAMECENTER = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_GAMECENTER;
			int theId = 1328;
			int theId2 = 1329;
			this.iconX = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId) - GlobalMembersResourcesWP.ImgXOfs(theId2));
			this.iconY = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId) - GlobalMembersResourcesWP.ImgYOfs(theId2));
			if (this.mDisabled && this.mDisabledRect.mWidth > 0 && this.mDisabledRect.mHeight > 0)
			{
				g.DrawImage(this.mComponentImage, 0, 0, this.mDisabledRect);
				g.DrawImageCel(image_DIALOG_BUTTON_GAMECENTER, this.iconX, this.iconY, 0);
				return;
			}
			if (this.IsButtonDown())
			{
				g.DrawImage(this.mComponentImage, 0, 0, this.mDownRect);
				g.DrawImageCel(image_DIALOG_BUTTON_GAMECENTER, this.iconX, this.iconY, 0);
				return;
			}
			if (this.mOverAlpha > 0.0)
			{
				if (this.mOverAlpha < 1.0)
				{
					g.DrawImage(this.mComponentImage, 0, 0, this.mNormalRect);
					g.DrawImageCel(image_DIALOG_BUTTON_GAMECENTER, this.iconX, this.iconY, 0);
				}
				g.SetColor(new Color(255, 255, 255, (int)((double)(this.mAlpha / 255f) * this.mOverAlpha * 255.0)));
				g.DrawImage(this.mComponentImage, 0, 0, this.mOverRect);
				g.DrawImageCel(image_DIALOG_BUTTON_GAMECENTER, this.iconX, this.iconY, 0);
				return;
			}
			if (this.mIsOver)
			{
				g.DrawImage(this.mComponentImage, 0, 0, this.mOverRect);
				g.DrawImageCel(image_DIALOG_BUTTON_GAMECENTER, this.iconX, this.iconY, 0);
				return;
			}
			g.DrawImage(this.mComponentImage, 0, 0, this.mNormalRect);
			g.DrawImageCel(image_DIALOG_BUTTON_GAMECENTER, this.iconX, this.iconY, 1);
		}

		protected void DrawOtherButton(Graphics g)
		{
			if (this.mComponentImage == null)
			{
				base.Draw(g);
				return;
			}
			if (this.mBtnNoDraw)
			{
				return;
			}
			if (this.mType == Bej3ButtonType.BUTTON_TYPE_LONG || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_GREEN)
			{
				switch (this.mOverlayType)
				{
				case Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_DIAMOND_MINE:
					g.DrawImageBox(new Rect(0, 0, this.mWidth, this.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE);
					if (GlobalMembers.gApp.mBoard.WantWarningGlow())
					{
						g.PushState();
						g.SetColor(GlobalMembers.gApp.mBoard.GetWarningGlowColor());
						g.SetDrawMode(1);
						g.SetColorizeImages(true);
						g.DrawImageBox(new Rect(0, 0, this.mWidth, this.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE);
						g.PopState();
					}
					break;
				case Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_ICE_STORM:
					g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM, 0, 0);
					if (GlobalMembers.gApp.mBoard.WantWarningGlow())
					{
						g.PushState();
						g.SetColor(GlobalMembers.gApp.mBoard.GetWarningGlowColor());
						g.SetDrawMode(1);
						g.SetColorizeImages(true);
						g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM, 0, 0);
						g.PopState();
					}
					break;
				default:
					g.DrawImageBox(new Rect(0, 0, this.mWidth, this.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME);
					if (this.mBorderGlowEnabled && GlobalMembers.gApp.mBoard != null && GlobalMembers.gApp.mBoard.WantWarningGlow())
					{
						g.PushState();
						g.SetColor(GlobalMembers.gApp.mBoard.GetWarningGlowColor());
						g.SetDrawMode(Graphics.DrawMode.Additive);
						g.SetColorizeImages(true);
						g.DrawImageBox(new Rect(0, 0, this.mWidth, this.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME);
						g.PopState();
					}
					break;
				}
			}
			bool flag = this.IsButtonDown();
			if (this.mNormalRect.mWidth == 0)
			{
				if (flag)
				{
					g.Translate(this.mTranslateX, this.mTranslateY);
				}
				g.DrawImageBox(this.mInsideImageRect, this.mComponentImage);
			}
			else
			{
				if (this.mDisabled && this.mDisabledRect.mWidth > 0 && this.mDisabledRect.mHeight > 0)
				{
					g.DrawImageBox(this.mDisabledRect, this.mInsideImageRect, this.mComponentImage);
				}
				else if (this.IsButtonDown())
				{
					g.DrawImageBox(this.mDownRect, this.mInsideImageRect, this.mComponentImage);
				}
				else if (this.mOverAlpha > 0.0)
				{
					if (this.mOverAlpha < 1.0)
					{
						g.DrawImageBox(this.mNormalRect, this.mInsideImageRect, this.mComponentImage);
					}
					g.SetColorizeImages(true);
					Color mColor = g.mColor;
					g.SetColor(new Color(255, 255, 255, (int)(this.mOverAlpha * 255.0)));
					g.DrawImageBox(this.mOverRect, this.mInsideImageRect, this.mComponentImage);
					g.SetColorizeImages(false);
					g.mColor = mColor;
				}
				else if (this.mIsOver)
				{
					g.DrawImageBox(this.mOverRect, this.mInsideImageRect, this.mComponentImage);
				}
				else
				{
					g.DrawImageBox(this.mNormalRect, this.mInsideImageRect, this.mComponentImage);
				}
				if (flag)
				{
					g.Translate(this.mTranslateX, this.mTranslateY);
				}
			}
			if (this.mIsHighLighted)
			{
				g.DrawImageBox(this.mDownRect, this.mInsideImageRect, this.mComponentImage);
			}
			if (this.mFont != null)
			{
				g.SetFont(this.mFont);
				int num = g.mColor.mAlpha;
				if (this.mIsOver)
				{
					g.SetColor(this.mColors[1]);
				}
				else
				{
					g.SetColor(this.mColors[0]);
				}
				g.mColor.mAlpha = num;
				int num2 = (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
				int num3 = (this.mHeight - this.mFont.GetHeight()) / 2 + this.mFont.GetAscent();
				Utils.SetFontLayerColor((ImageFont)this.mFont, 1, Color.White);
				if (this.mType == Bej3ButtonType.BUTTON_TYPE_LONG)
				{
					Utils.SetFontLayerColor((ImageFont)this.mFont, 1, Bej3Widget.COLOR_SUBHEADING_4_FILL);
					Utils.SetFontLayerColor((ImageFont)this.mFont, 0, Bej3Widget.COLOR_SUBHEADING_4_STROKE);
				}
				else if (this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE)
				{
					Utils.SetFontLayerColor((ImageFont)this.mFont, 1, Bej3Widget.COLOR_SUBHEADING_5_FILL);
					Utils.SetFontLayerColor((ImageFont)this.mFont, 0, Bej3Widget.COLOR_SUBHEADING_5_STROKE);
				}
				else if (this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_GREEN)
				{
					Utils.SetFontLayerColor((ImageFont)this.mFont, 1, Bej3Widget.COLOR_SUBHEADING_6_FILL);
					Utils.SetFontLayerColor((ImageFont)this.mFont, 0, Bej3Widget.COLOR_SUBHEADING_6_STROKE);
				}
				else if (this.mType == Bej3ButtonType.BUTTON_TYPE_CUSTOM)
				{
					Utils.SetFontLayerColor((ImageFont)this.mFont, 1, Bej3Widget.COLOR_SUBHEADING_4_FILL);
					Utils.SetFontLayerColor((ImageFont)this.mFont, 0, Bej3Widget.COLOR_SUBHEADING_4_STROKE);
				}
				else if (this.mType == Bej3ButtonType.BUTTON_TYPE_CUSTOM_INV)
				{
					Utils.SetFontLayerColor((ImageFont)this.mFont, 0, Bej3Widget.COLOR_SUBHEADING_4_FILL);
					Utils.SetFontLayerColor((ImageFont)this.mFont, 1, Bej3Widget.COLOR_SUBHEADING_4_STROKE);
				}
				if (this.mType == Bej3ButtonType.BUTTON_TYPE_LONG || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_GREEN)
				{
					num3 += ConstantsWP.BEJ3BUTTON_TEXT_OFFSET_Y;
				}
				g.DrawString(this.mLabel, num2 + this.mTextOffsetX, num3 + this.mTextOffsetY);
			}
			if (this.mIconImage != null)
			{
				if (this.mIsOver)
				{
					g.SetColor(this.mColors[1]);
				}
				else
				{
					g.SetColor(this.mColors[0]);
				}
				int num4 = (this.mWidth - this.mIconImage.GetWidth()) / 2;
				int num5 = (this.mHeight - this.mIconImage.GetHeight()) / 2;
				g.DrawImage(this.mIconImage, num4 + this.mTextOffsetX, num5 + this.mTextOffsetY);
			}
			if (flag)
			{
				g.Translate(-this.mTranslateX, -this.mTranslateY);
			}
		}

		public void HighLighted(bool enable)
		{
			this.mIsHighLighted = enable;
		}

		public Bej3Button(int theId, Bej3ButtonListener theListener)
			: base(null, theId, theListener)
		{
			this.initButton(theId, theListener, Bej3ButtonType.BUTTON_TYPE_CUSTOM, false);
		}

		public Bej3Button(int theId, Bej3ButtonListener theListener, Bej3ButtonType theType)
			: base(null, theId, theListener)
		{
			this.initButton(theId, theListener, theType, false);
		}

		public Bej3Button(int theId, Bej3ButtonListener theListener, Bej3ButtonType theType, bool sizeToContent)
			: base(null, theId, theListener)
		{
			this.initButton(theId, theListener, theType, sizeToContent);
		}

		public void initButton(int theId, Bej3ButtonListener theListener, Bej3ButtonType theType, bool sizeToContent)
		{
			this.mTargetTypeImageRotation = 0f;
			this.mTypeImageRotation = 0f;
			this.mClippingEnabled = true;
			this.mImageId = -1;
			this.mZenSize = 1f;
			this.mSizeToContent = sizeToContent;
			this.mSlideGlowEnabled = true;
			this.mOverlayType = Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_NONE;
			this.mBorderGlowEnabled = false;
			this.mType = theType;
			this.mPlayPressSound = true;
			this.SetType(theType);
			int theWidth = 0;
			if (this.mType == Bej3ButtonType.BUTTON_TYPE_LONG || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_GREEN)
			{
				theWidth = ConstantsWP.BEJ3BUTTON_AUTOSCALE_DEFAULT_WIDTH;
			}
			this.Resize(0, 0, theWidth, 0);
			this.mTypeImageRotation = this.mTargetTypeImageRotation;
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.LinkUpAssets();
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.LinkUpAssets();
		}

		public override void MouseMove(int theX, int theY)
		{
			base.MouseMove(theX, theY);
			this.LinkUpAssets();
		}

		public override void MouseDown(int theX, int theY, int theBtnNum, int theClickCount)
		{
			base.MouseDown(theX, theY, theBtnNum, theClickCount);
			this.mIsDown = true;
			this.mIsOver = true;
			if (this.mPlayPressSound)
			{
				if (this.IsTopButton() && GlobalMembers.gApp.GetDialog(18) == null)
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTON_PRESS);
				}
				else
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTON_PRESS);
				}
			}
			this.LinkUpAssets();
		}

		public override void MouseUp(int theX, int theY, int theBtnNum, int theClickCount)
		{
			Bej3ButtonListener bej3ButtonListener = this.mButtonListener as Bej3ButtonListener;
			if (bej3ButtonListener != null && bej3ButtonListener.ButtonsEnabled())
			{
				if (this.mPlayPressSound)
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTON_MOUSEOVER);
				}
				base.MouseUp(theX, theY, theBtnNum, theClickCount);
			}
			this.mIsDown = false;
			this.mIsOver = false;
			this.LinkUpAssets();
		}

		public override void TouchesCanceled()
		{
			this.mIsDown = false;
			this.mIsOver = false;
			this.LinkUpAssets();
		}

		public override void Update()
		{
			if (this.mTargetTypeImageRotation != this.mTypeImageRotation)
			{
				float num = this.mTargetTypeImageRotation - this.mTypeImageRotation;
				this.mTypeImageRotation += num * ConstantsWP.BEJ3BUTTON_ROTATION_SPEED;
				if (Math.Abs(this.mTypeImageRotation - this.mTargetTypeImageRotation) < 0.01f)
				{
					this.mTypeImageRotation = this.mTargetTypeImageRotation;
				}
			}
			base.Update();
		}

		public override void DrawOverlay(Graphics g, int thePriority)
		{
		}

		public override void Draw(Graphics g)
		{
			if (!this.mClippingEnabled)
			{
				g.ClearClipRect();
			}
			g.SetColorizeImages(true);
			switch (this.mType)
			{
			case Bej3ButtonType.BUTTON_TYPE_BACK:
				this.DrawBackButton(g);
				return;
			case Bej3ButtonType.BUTTON_TYPE_DROPDOWN_UP:
			case Bej3ButtonType.BUTTON_TYPE_DROPDOWN_DOWN:
			case Bej3ButtonType.BUTTON_TYPE_DROPDOWN_RIGHT:
				this.DrawDropDownButton(g);
				return;
			case Bej3ButtonType.BUTTON_TYPE_INFO:
				this.DrawInfoButton(g);
				return;
			case Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE:
			case Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE:
				this.DrawSwipeButton(g);
				return;
			case Bej3ButtonType.BUTTON_TYPE_PROFILE_PICTURE:
				this.DrawProfilePictureButton(g);
				return;
			case Bej3ButtonType.BUTTON_TYPE_ZEN_SLIDE:
				this.DrawZenSlideButton(g);
				return;
			case Bej3ButtonType.BUTTON_TYPE_HINT_CAMERA:
				this.DrawCameraButton(g);
				return;
			case Bej3ButtonType.BUTTON_TYPE_HINT_OK:
				this.DrawHintOkButton(g);
				return;
			case Bej3ButtonType.TOP_BUTTON_TYPE_MENU:
			case Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED:
			case Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS:
				this.DrawTopButton(g);
				return;
			case Bej3ButtonType.BUTTON_TYPE_GAMECENTER:
				this.DrawGameCenterButton(g);
				return;
			}
			this.DrawOtherButton(g);
		}

		public void SetBorderGlow(bool Value)
		{
			this.mBorderGlowEnabled = Value;
		}

		public override void SetDisabled(bool isDisabled)
		{
			if (isDisabled == this.mDisabled)
			{
				return;
			}
			bool mDisabled = this.mDisabled;
			if (this.mType == Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED)
			{
				isDisabled = true;
			}
			base.SetDisabled(isDisabled);
			if (mDisabled != isDisabled)
			{
				this.LinkUpAssets();
			}
		}

		public Bej3ButtonType GetType()
		{
			return this.mType;
		}

		public void SetType(Bej3ButtonType theType)
		{
			this.mType = theType;
			if (this.mFont == null)
			{
				this.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			}
			this.LinkUpAssets();
		}

		public void SetupCustomButton(Image image, int x, int y)
		{
			this.SetupCustomButton(image, x, y, -1, -1);
		}

		public void SetupCustomButton(Image image, int x, int y, int width)
		{
			this.SetupCustomButton(image, x, y, width, -1);
		}

		public void SetupCustomButton(Image image, int x, int y, int width, int height)
		{
			this.mComponentImage = image;
			if (this.mComponentImage != null)
			{
				int theCel = 1;
				if (this.mComponentImage.GetCelCount() == 0)
				{
					theCel = 0;
				}
				this.mNormalRect = this.mComponentImage.GetCelRect(0);
				this.mOverRect = this.mComponentImage.GetCelRect(theCel);
				this.mDownRect = this.mComponentImage.GetCelRect(theCel);
				int theWidth = ((width >= 0) ? width : this.mComponentImage.GetCelWidth());
				int theHeight = ((height >= 0) ? height : this.mComponentImage.GetCelHeight());
				this.Resize(x, y, theWidth, theHeight);
			}
		}

		public virtual void LinkUpAssets()
		{
			this.mTargetTypeImageRotation = 0f;
			this.mTypeImage = null;
			if (this.IsTopButton())
			{
				this.mInsideImageRect = new Rect(0, 0, this.mWidth, this.mHeight);
			}
			switch (this.mType)
			{
			case Bej3ButtonType.BUTTON_TYPE_LONG:
				this.mTypeImage = null;
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE;
				if (this.mComponentImage != null)
				{
					this.mNormalRect = this.mComponentImage.GetCelRect(5);
					this.mOverRect = this.mComponentImage.GetCelRect(4);
					this.mDownRect = this.mComponentImage.GetCelRect(4);
				}
				if (this.mSizeToContent)
				{
					this.CalcButtonWidth(this.mLabel, this.mWidth);
					goto IL_412;
				}
				goto IL_412;
			case Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE:
				this.mTypeImage = null;
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE;
				if (this.mComponentImage != null)
				{
					this.mNormalRect = this.mComponentImage.GetCelRect(1);
					this.mOverRect = this.mComponentImage.GetCelRect(0);
					this.mDownRect = this.mComponentImage.GetCelRect(0);
				}
				if (this.mSizeToContent)
				{
					this.CalcButtonWidth(this.mLabel, this.mWidth);
					goto IL_412;
				}
				goto IL_412;
			case Bej3ButtonType.BUTTON_TYPE_LONG_GREEN:
				this.mTypeImage = null;
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE;
				if (this.mComponentImage != null)
				{
					this.mNormalRect = this.mComponentImage.GetCelRect(3);
					this.mOverRect = this.mComponentImage.GetCelRect(2);
					this.mDownRect = this.mComponentImage.GetCelRect(2);
				}
				if (this.mSizeToContent)
				{
					this.CalcButtonWidth(this.mLabel, this.mWidth);
					goto IL_412;
				}
				goto IL_412;
			case Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE:
			case Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE:
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPE;
				if (this.mComponentImage != null)
				{
					this.mNormalRect = this.mComponentImage.GetCelRect(1);
					this.mOverRect = this.mComponentImage.GetCelRect(0);
					this.mDownRect = this.mComponentImage.GetCelRect(0);
					goto IL_412;
				}
				goto IL_412;
			case Bej3ButtonType.BUTTON_TYPE_ZEN_SLIDE:
				this.mButtonImage = GlobalMembersResourcesWP.GetImageById(this.mImageId);
				if (this.mButtonImage != null)
				{
					this.Resize(this.mX, this.mY, this.mButtonImage.mWidth, this.mButtonImage.mHeight);
					goto IL_412;
				}
				goto IL_412;
			case Bej3ButtonType.BUTTON_TYPE_HINT_CAMERA:
			{
				Image image_DIALOG_BUTTON_SMALL_BLUE = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE;
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE;
				if (image_DIALOG_BUTTON_SMALL_BLUE != null)
				{
					this.mNormalRect = image_DIALOG_BUTTON_SMALL_BLUE.GetCelRect(1);
					this.mDownRect = image_DIALOG_BUTTON_SMALL_BLUE.GetCelRect(0);
					this.mOverRect = image_DIALOG_BUTTON_SMALL_BLUE.GetCelRect(0);
					goto IL_412;
				}
				goto IL_412;
			}
			case Bej3ButtonType.BUTTON_TYPE_HINT_OK:
			{
				Image image_DIALOG_BUTTON_SMALL_BLUE2 = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE;
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE;
				if (image_DIALOG_BUTTON_SMALL_BLUE2 != null)
				{
					this.mDownRect = image_DIALOG_BUTTON_SMALL_BLUE2.GetCelRect(0);
					this.mOverRect = image_DIALOG_BUTTON_SMALL_BLUE2.GetCelRect(0);
					this.mNormalRect = image_DIALOG_BUTTON_SMALL_BLUE2.GetCelRect(1);
					goto IL_412;
				}
				goto IL_412;
			}
			case Bej3ButtonType.TOP_BUTTON_TYPE_MENU:
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP;
				this.mNormalRect = this.mComponentImage.GetCelRect(0);
				this.mOverRect = (this.mDownRect = this.mComponentImage.GetCelRect(1));
				goto IL_412;
			case Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED:
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DASHBOARD_CLOSED_BUTTON;
				this.mNormalRect = (this.mOverRect = (this.mDownRect = (this.mInsideImageRect = this.mComponentImage.GetCelRect(0))));
				this.mDisabled = true;
				goto IL_412;
			case Bej3ButtonType.TOP_BUTTON_TYPE_DISMISS:
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_DOWN;
				this.mNormalRect = this.mComponentImage.GetCelRect(0);
				this.mOverRect = (this.mDownRect = this.mComponentImage.GetCelRect(1));
				goto IL_412;
			case Bej3ButtonType.BUTTON_TYPE_GAMECENTER:
				this.mComponentImage = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_GAMECENTER_BG;
				if (this.mComponentImage != null)
				{
					this.mDownRect = (this.mOverRect = (this.mNormalRect = this.mComponentImage.GetCelRect(0)));
					goto IL_412;
				}
				goto IL_412;
			}
			this.mTypeImage = null;
			IL_412:
			if (this.mTypeImage != null)
			{
				this.mIconSrcRect = (this.mDisabled ? this.mTypeImage.GetCelRect(1) : this.mTypeImage.GetCelRect(0));
			}
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			if (this.mType == Bej3ButtonType.BUTTON_TYPE_BACK)
			{
				Image image_DASHBOARD_MENU_UP = GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP;
				if (image_DASHBOARD_MENU_UP == null)
				{
					return;
				}
				theWidth = image_DASHBOARD_MENU_UP.GetCelWidth();
				theHeight = image_DASHBOARD_MENU_UP.GetCelHeight();
			}
			else if (this.mType == Bej3ButtonType.BUTTON_TYPE_LEFT_SWIPE || this.mType == Bej3ButtonType.BUTTON_TYPE_RIGHT_SWIPE)
			{
				Image image_DIALOG_ARROW_SWIPEGLOW = GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPEGLOW;
				theWidth = image_DIALOG_ARROW_SWIPEGLOW.GetCelWidth() * 2;
				theHeight = image_DIALOG_ARROW_SWIPEGLOW.GetCelHeight();
			}
			else if (this.mType == Bej3ButtonType.BUTTON_TYPE_DROPDOWN_DOWN || this.mType == Bej3ButtonType.BUTTON_TYPE_DROPDOWN_RIGHT || this.mType == Bej3ButtonType.BUTTON_TYPE_DROPDOWN_UP || this.mType == Bej3ButtonType.BUTTON_TYPE_INFO)
			{
				theHeight = (theWidth = (int)ConstantsWP.BEJ3BUTTON_DROPDOWN_SIZE);
			}
			else if (this.mType == Bej3ButtonType.BUTTON_TYPE_HINT_OK || this.mType == Bej3ButtonType.BUTTON_TYPE_HINT_CAMERA)
			{
				Image image_DIALOG_BUTTON_SMALL_BLUE = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE;
				theWidth = image_DIALOG_BUTTON_SMALL_BLUE.GetCelWidth();
				theHeight = image_DIALOG_BUTTON_SMALL_BLUE.GetCelHeight();
			}
			else if (this.mType == Bej3ButtonType.BUTTON_TYPE_ZEN_SLIDE)
			{
				if (this.mButtonImage != null)
				{
					theWidth = this.mButtonImage.mWidth;
					theHeight = this.mButtonImage.mHeight;
				}
			}
			else if (this.mType == Bej3ButtonType.BUTTON_TYPE_GAMECENTER)
			{
				Image image_DIALOG_BUTTON_GAMECENTER_BG = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_GAMECENTER_BG;
				theWidth = image_DIALOG_BUTTON_GAMECENTER_BG.GetCelWidth();
				theHeight = image_DIALOG_BUTTON_GAMECENTER_BG.GetCelHeight();
			}
			else if (this.mComponentImage != null)
			{
				if (this.mType == Bej3ButtonType.BUTTON_TYPE_LONG || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_GREEN)
				{
					switch (this.mOverlayType)
					{
					case Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_DIAMOND_MINE:
						theHeight = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE.GetCelHeight();
						break;
					case Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_ICE_STORM:
						theHeight = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM.GetCelHeight();
						break;
					default:
						theHeight = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME.GetCelHeight();
						break;
					}
				}
				else
				{
					theHeight = Math.Max(theHeight, this.mComponentImage.GetCelHeight());
				}
			}
			base.Resize(theX, theY, theWidth, theHeight);
			this.SetOverlayType(this.mOverlayType);
		}

		public void SetOverlayType(Bej3Button.BUTTON_OVERLAY_TYPE type)
		{
			this.mOverlayType = type;
			this.mInsideImageRect = new Rect(0, 0, this.mWidth, this.mHeight);
			if (this.mType == Bej3ButtonType.BUTTON_TYPE_LONG || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_PURPLE || this.mType == Bej3ButtonType.BUTTON_TYPE_LONG_GREEN)
			{
				switch (this.mOverlayType)
				{
				case Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_DIAMOND_MINE:
				{
					this.mHeight = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE.GetCelHeight();
					int num = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1330) - GlobalMembersResourcesWP.ImgXOfs(1326));
					int num2 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1330) - GlobalMembersResourcesWP.ImgYOfs(1326));
					this.mInsideImageRect.mX = this.mInsideImageRect.mX + num;
					this.mInsideImageRect.mY = this.mInsideImageRect.mY + num2;
					this.mInsideImageRect.mWidth = this.mInsideImageRect.mWidth - (GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE.GetCelWidth() - GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE.GetCelWidth());
					this.mInsideImageRect.mHeight = this.mInsideImageRect.mHeight - (GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE.GetCelHeight() - GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE.GetCelHeight());
					return;
				}
				case Bej3Button.BUTTON_OVERLAY_TYPE.BUTTON_OVERLAY_ICE_STORM:
				{
					this.mHeight = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM.GetCelHeight();
					int num3 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1330) - GlobalMembersResourcesWP.ImgXOfs(1327));
					int num4 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1330) - GlobalMembersResourcesWP.ImgYOfs(1327));
					this.mInsideImageRect.mX = this.mInsideImageRect.mX + num3;
					this.mInsideImageRect.mY = this.mInsideImageRect.mY + num4;
					this.mInsideImageRect.mWidth = this.mInsideImageRect.mWidth - (GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM.GetCelWidth() - GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE.GetCelWidth());
					this.mInsideImageRect.mHeight = this.mInsideImageRect.mHeight - (GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM.GetCelHeight() - GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE.GetCelHeight());
					this.mTextOffsetY = ConstantsWP.BEJ3BUTTON_ICESTORM_TEXT_OFFSET_Y;
					return;
				}
				default:
				{
					this.mHeight = GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME.GetCelHeight();
					int num5 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1330) - GlobalMembersResourcesWP.ImgXOfs(1325)) + ConstantsWP.BEJ3BUTTON_INSIDE_RECT_OFFSET;
					int num6 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1330) - GlobalMembersResourcesWP.ImgYOfs(1325)) + ConstantsWP.BEJ3BUTTON_INSIDE_RECT_OFFSET;
					this.mInsideImageRect.mX = this.mInsideImageRect.mX + num5;
					this.mInsideImageRect.mY = this.mInsideImageRect.mY + num6;
					this.mInsideImageRect.mWidth = this.mInsideImageRect.mWidth - (GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME.GetCelWidth() - GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE.GetCelWidth());
					this.mInsideImageRect.mHeight = this.mInsideImageRect.mHeight - (GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME.GetCelHeight() - GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE.GetCelHeight());
					break;
				}
				}
			}
		}

		public void SetLabel(string theLabel)
		{
			this.mLabel = theLabel;
			this.LinkUpAssets();
		}

		public void SetImageId(int theId)
		{
			this.mImageId = theId;
			this.LinkUpAssets();
		}

		public void EnableSlideGlow(bool enabled)
		{
			this.mSlideGlowEnabled = enabled;
		}

		public int GetButtonWidth()
		{
			if (this.mFont != null && !string.IsNullOrEmpty(this.mLabel))
			{
				return Math.Max(this.mFont.StringWidth(this.mLabel) + ConstantsWP.BEJ3BUTTON_AUTOSCALE_TEXT_WIDTH_OFFSET, ConstantsWP.BEJ3BUTTON_AUTOSCALE_MIN_WIDTH);
			}
			return 0;
		}

		public static void UpdateStatics()
		{
			InterfaceState mInterfaceState = GlobalMembers.gApp.mInterfaceState;
			if ((BejeweledLivePlusApp.mIdleTicksForButton > 500 && mInterfaceState != InterfaceState.INTERFACE_STATE_INGAME) || Bej3Button.topButtonAnimating)
			{
				Bej3Button.topButtonAnimating = true;
				Bej3Button.mTopButtonGlow += 0.01f * (float)Bej3Button.mTopButtonGlowGoingUp;
				if (Bej3Button.mTopButtonGlow >= 0.99f || Bej3Button.mTopButtonGlow < 0.01f)
				{
					Bej3Button.mTopButtonGlowGoingUp = -Bej3Button.mTopButtonGlowGoingUp;
				}
				if (Bej3Button.mTopButtonGlowGoingUp != 0 && Bej3Button.mTopButtonGlow <= 0.01f)
				{
					Bej3Button.numberOfFlashes++;
					if (Bej3Button.numberOfFlashes >= 2)
					{
						Bej3Button.numberOfFlashes = 0;
						Bej3Button.topButtonAnimating = false;
						BejeweledLivePlusApp.mIdleTicksForButton = 0;
					}
				}
			}
			if (Bej3Button.mSlideGlowBrightening)
			{
				Bej3Button.mSlideGlow += 0.02f;
				if (Bej3Button.mSlideGlow >= 1f)
				{
					Bej3Button.mSlideGlow = 1f;
					Bej3Button.mSlideGlowBrightening = false;
					return;
				}
			}
			else
			{
				Bej3Button.mSlideGlow -= 0.02f;
				if (Bej3Button.mSlideGlow <= 0f)
				{
					Bej3Button.mSlideGlow = 0f;
					Bej3Button.mSlideGlowBrightening = true;
				}
			}
		}

		protected Bej3Button.BUTTON_OVERLAY_TYPE mOverlayType;

		public Bej3ButtonType mType;

		private Image mTypeImage;

		private Rect mIconSrcRect = default(Rect);

		private Point mIconOffset = default(Point);

		private Rect mButtonSrcRect = default(Rect);

		private float mTypeImageRotation;

		private float mTargetTypeImageRotation;

		private int mImageId;

		private bool mSlideGlowEnabled;

		private bool mBorderGlowEnabled;

		private static bool mSlideGlowBrightening = true;

		private static float mSlideGlow = 0f;

		private static int mSlideGlowTimer = 0;

		private static float mTopButtonGlow = 0f;

		private static int mTopButtonGlowGoingUp = 1;

		public Rect mInsideImageRect = default(Rect);

		public bool mSizeToContent;

		public float mAlpha;

		public bool mClippingEnabled;

		public float mZenSize;

		public bool mPlayPressSound;

		private int width;

		private string s = GlobalMembers._ID("OK", 3218);

		private int xg;

		private int yg;

		public int iconX;

		public int iconY;

		protected bool mIsHighLighted;

		private static bool topButtonAnimating = false;

		private static int numberOfFlashes = -1;

		public enum BUTTON_OVERLAY_TYPE
		{
			BUTTON_OVERLAY_NONE,
			BUTTON_OVERLAY_DIAMOND_MINE,
			BUTTON_OVERLAY_ICE_STORM
		}
	}
}
