using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3Widget : Bej3WidgetBase, Bej3ButtonListener, ButtonListener
	{
		public void ClearSlidingForDialog(Bej3Dialog dialog)
		{
			if (dialog == this.mSlidingForDialog)
			{
				this.mSlidingForDialog = null;
				this.mTargetPos = this.mFinalY;
			}
		}

		protected virtual void DialogFinished(Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
		}

		protected virtual void DialogFinishedSlidingIn(Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
		}

		protected void ResetFadedBack(bool showing)
		{
			if (this.mState == Bej3WidgetState.STATE_OUT)
			{
				return;
			}
			this.mShouldFadeBehind = showing;
			GlobalMembers.gApp.mDoFadeBackForDialogs = showing;
		}

		protected void DrawFadedBack(Graphics g)
		{
		}

		public Bej3Widget(Menu_Type type, bool hasCloseButton, Bej3ButtonType topButtonType)
		{
			this.mInterfaceState = InterfaceState.INTERFACE_STATE_LOADING;
			this.mState = Bej3WidgetState.STATE_OUT;
			this.mIgnoreButtons = false;
			this.mDoesSlideInFromBottom = true;
			this.mCanAllowSlide = true;
			this.mSlidingForDialog = null;
			this.mAnimationFraction = 0f;
			this.mAllowFade = true;
			this.mAllowSlide = false;
			this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_IDLE;
			this.mFadingBackground = false;
			this.mShouldFadeBehind = false;
			this.mTopButton = null;
			this.mType = type;
			this.mY = (this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN);
			this.Resize(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			this.mAlphaCurve.SetConstant(0.0);
			this.SetVisible(false);
			if (topButtonType == Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
			{
				this.mTopButton = null;
				return;
			}
			this.mTopButton = new Bej3Button(10001, this, Bej3ButtonType.BUTTON_TYPE_CUSTOM);
			int theX = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(711));
			int theY = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(711));
			int celWidth = GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP.GetCelWidth();
			int celHeight = GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP.GetCelHeight();
			this.mTopButton.Resize(theX, theY, celWidth, celHeight);
			this.AddWidget(this.mTopButton);
			this.SetTopButtonType(topButtonType);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public virtual bool SlideForDialog(bool slideOut, Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
			if (slideOut)
			{
				this.mSlidingForDialog = dialog;
				this.Transition_SlideOut();
			}
			else
			{
				this.mY = dialog.mY;
				this.mTargetPos = this.mFinalY;
				this.Transition_SlideThenFadeIn();
				this.mSlidingForDialog = null;
				this.SetVisible(true);
				if (this.mTopButton != null)
				{
					Bej3ButtonType type = this.mTopButton.GetType();
					this.SetTopButtonType(type);
					if (this.mState == Bej3WidgetState.STATE_IN || this.mState == Bej3WidgetState.STATE_FADING_IN)
					{
						this.mTopButton.SetDisabled(false);
					}
				}
			}
			return this.mAlphaCurve != 0.0;
		}

		public static void SetOverlayType(Bej3Widget.OVERLAY_TYPE type)
		{
			Bej3Widget.mOverlayType = type;
		}

		public static void CenterWidgetAt(int x, int y, Widget widget)
		{
			Bej3Widget.CenterWidgetAt(x, y, widget, true, true);
		}

		public static void CenterWidgetAt(int x, int y, Widget widget, bool centerX)
		{
			Bej3Widget.CenterWidgetAt(x, y, widget, centerX, true);
		}

		public static void CenterWidgetAt(int x, int y, Widget widget, bool centerX, bool centerY)
		{
			int num = x;
			if (centerX)
			{
				num -= widget.mWidth / 2;
			}
			int num2 = y;
			if (centerY)
			{
				num2 -= widget.mHeight / 2;
			}
			widget.Resize(num, num2, widget.mWidth, widget.mHeight);
		}

		public static void DisableWidget(Widget w, bool disabled)
		{
			w.SetDisabled(disabled);
			w.SetVisible(!disabled);
			w.mMouseVisible = !disabled;
		}

		public static bool FloatEquals(float a, float b, float epsilon)
		{
			if (epsilon < 0.8f)
			{
				epsilon = 0.8f;
			}
			return Math.Abs(a - b) <= epsilon;
		}

		public static bool FloatEquals(float a, float b)
		{
			return Bej3Widget.FloatEquals(a, b, 0.8f);
		}

		public static void DrawDialogBox(Graphics g, int width)
		{
			Bej3Widget.DrawDialogBox(g, width, 0f, true, false);
		}

		public static void DrawDialogBox(Graphics g, int width, float rustyFade)
		{
			Bej3Widget.DrawDialogBox(g, width, rustyFade, true, false);
		}

		public static void DrawDialogBox(Graphics g, int width, float rustyFade, bool drawFrame)
		{
			Bej3Widget.DrawDialogBox(g, width, rustyFade, drawFrame, false);
		}

		public static void DrawDialogBox(Graphics g, int width, float rustyFade, bool drawFrame, bool forceDraw)
		{
			if (!forceDraw && Bej3Widget.mDialogBoxDrawnThisFrame && Bej3Widget.mCurrentSlidingMenu != null && Bej3Widget.mCurrentSlidingMenu.mFadingBackground)
			{
				return;
			}
			g.ClearClipRect();
			Bej3Widget.mDialogBoxDrawnThisFrame = true;
			Color mColor = g.mColor;
			g.SetColor(Color.White);
			g.mFinalColor = Color.White;
			g.PushColorMult();
			int theX = 0;
			int num = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(706));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DASHBOARD_DASH_MAIN, theX, num);
			switch (Bej3Widget.mOverlayType)
			{
			case Bej3Widget.OVERLAY_TYPE.OVERLAY_RUSTY:
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DASHBOARD_DM_OVERLAY, theX, num);
				break;
			case Bej3Widget.OVERLAY_TYPE.OVERLAY_ICE:
			{
				int num2 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(709) - GlobalMembersResourcesWP.ImgYOfs(706));
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DASHBOARD_ICE_OVERLAY, theX, num + num2);
				break;
			}
			}
			num += GlobalMembersResourcesWP.IMAGE_DASHBOARD_DASH_MAIN.mHeight;
			for (int i = num; i < GlobalMembers.gApp.mHeight; i += GlobalMembersResourcesWP.IMAGE_DASHBOARD_DASH_TILE.mHeight)
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DASHBOARD_DASH_TILE, 0, i);
			}
			g.PopColorMult();
			g.SetColor(mColor);
		}

		public static void DrawSwipeInlay(Graphics g, int y, int height, int width)
		{
			Bej3Widget.DrawSwipeInlay(g, y, height, width, false);
		}

		public static void DrawSwipeInlay(Graphics g, int y, int height, int width, bool useCutAway)
		{
			int num = (useCutAway ? ConstantsWP.MENU_INLAY_OFFSET_CUTAWAY : ConstantsWP.MENU_INLAY_OFFSET_BOTTOM);
			int menu_INSIDE_CLIPPING_OFFSET_LEFT = ConstantsWP.MENU_INSIDE_CLIPPING_OFFSET_LEFT;
			int menu_INSIDE_CLIPPING_OFFSET_RIGHT = ConstantsWP.MENU_INSIDE_CLIPPING_OFFSET_RIGHT;
			width = width - menu_INSIDE_CLIPPING_OFFSET_LEFT - menu_INSIDE_CLIPPING_OFFSET_RIGHT;
			g.DrawImageBox(new Rect(menu_INSIDE_CLIPPING_OFFSET_LEFT, y + ConstantsWP.MENU_INLAY_OFFSET_TOP - GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE.mHeight, width, GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE);
			g.GetFinalColor();
			Color mColor = g.mColor;
			g.SetColor(new Color(176, 90, 56, 63));
			g.FillRect(menu_INSIDE_CLIPPING_OFFSET_LEFT, y + ConstantsWP.MENU_INLAY_OFFSET_TOP, width, height - ConstantsWP.MENU_INLAY_OFFSET_TOP - num);
			g.mColor = mColor;
			if (!useCutAway)
			{
				g.DrawImageBox(new Rect(menu_INSIDE_CLIPPING_OFFSET_LEFT, y - num + height, width, GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE);
				return;
			}
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_CORNER, menu_INSIDE_CLIPPING_OFFSET_LEFT, y - num + height);
			g.DrawImageMirror(GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_CORNER, menu_INSIDE_CLIPPING_OFFSET_LEFT + width - GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_CORNER.mWidth, y - num + height, true);
			g.DrawImageBox(new Rect(menu_INSIDE_CLIPPING_OFFSET_LEFT + GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_CORNER.mWidth, y - num + height, width - GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_CORNER.mWidth * 2, GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE);
		}

		public static void DrawLightBox(Graphics g, Rect theDest)
		{
			Bej3Widget.DrawImageBoxTileCenter(g, theDest, GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_BOX_INTERIOR_BG);
		}

		public static void DrawImageCentered(Graphics g, Image image, int imageCel, int x, int y)
		{
			int celWidth = image.GetCelWidth();
			int celHeight = image.GetCelHeight();
			g.DrawImage(image, new Rect(x - celWidth / 2, y - celHeight / 2, celWidth, celHeight), image.GetCelRect(imageCel));
		}

		public static void DrawImageBoxTileCenter(Graphics g, Rect theDest, Image theComponentImage, bool useSmallTiles, int centerOffsetX)
		{
			Bej3Widget.DrawImageBoxTileCenter(g, theDest, theComponentImage, useSmallTiles, centerOffsetX, 0);
		}

		public static void DrawImageBoxTileCenter(Graphics g, Rect theDest, Image theComponentImage, bool useSmallTiles)
		{
			Bej3Widget.DrawImageBoxTileCenter(g, theDest, theComponentImage, useSmallTiles, 0, 0);
		}

		public static void DrawImageBoxTileCenter(Graphics g, Rect theDest, Image theComponentImage)
		{
			Bej3Widget.DrawImageBoxTileCenter(g, theDest, theComponentImage, false, 0, 0);
		}

		public static void DrawImageBoxTileCenter(Graphics g, Rect theDest, Image theComponentImage, bool useSmallTiles, int centerOffsetX, int centerOffsetY)
		{
			Rect rect = new Rect(0, 0, theComponentImage.mWidth, theComponentImage.mHeight);
			if (rect.mWidth <= 0 || rect.mHeight <= 0)
			{
				return;
			}
			int num = (useSmallTiles ? ConstantsWP.BEJ3WIDGET_IMAGEBOX_OFFSET_SMALL : ConstantsWP.BEJ3WIDGET_IMAGEBOX_OFFSET);
			int num2 = rect.mWidth / 2 + centerOffsetX - num;
			int num3 = rect.mHeight / 2 + centerOffsetY - num;
			int mX = rect.mX;
			int mY = rect.mY;
			int num4 = rect.mWidth - num2 * 2;
			int num5 = rect.mHeight - num3 * 2;
			if (theDest.mWidth < num2 * 2)
			{
				int num6 = theDest.mWidth / 2;
				if ((theDest.mWidth & 1) == 1)
				{
					num6++;
				}
			}
			if (theDest.mHeight < num3 * 2)
			{
				int num7 = theDest.mHeight / 2;
				if ((theDest.mHeight & 1) == 1)
				{
					num7++;
				}
			}
			Rect mClipRect = g.mClipRect;
			g.DrawImage(theComponentImage, theDest.mX, theDest.mY, new Rect(mX, mY, num2, num3));
			g.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num2, theDest.mY, new Rect(mX + num2 + num4, mY, num2, num3));
			g.DrawImage(theComponentImage, theDest.mX, theDest.mY + theDest.mHeight - num3, new Rect(mX, mY + num3 + num5, num2, num3));
			g.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num2, theDest.mY + theDest.mHeight - num3, new Rect(mX + num2 + num4, mY + num3 + num5, num2, num3));
			g.ClipRect(theDest.mX + num2, theDest.mY, theDest.mWidth - num2 * 2, theDest.mHeight);
			for (int i = 0; i < (theDest.mWidth - num2 * 2 + num4 - 1) / num4; i++)
			{
				g.DrawImage(theComponentImage, theDest.mX + num2 + i * num4, theDest.mY, new Rect(mX + num2, mY, num4, num3));
				g.DrawImage(theComponentImage, theDest.mX + num2 + i * num4, theDest.mY + theDest.mHeight - num3, new Rect(mX + num2, mY + num3 + num5, num4, num3));
			}
			g.mClipRect = mClipRect;
			g.ClipRect(theDest.mX, theDest.mY + num3, theDest.mWidth, theDest.mHeight - num3 * 2);
			for (int j = 0; j < (theDest.mHeight - num3 * 2 + num5 - 1) / num5; j++)
			{
				g.DrawImage(theComponentImage, theDest.mX, theDest.mY + num3 + j * num5, new Rect(mX, mY + num3, num2, num5));
				g.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num2, theDest.mY + num3 + j * num5, new Rect(mX + num2 + num4, mY + num3, num2, num5));
			}
			g.mClipRect = mClipRect;
			g.ClipRect(theDest.mX + num2, theDest.mY + num3, theDest.mWidth - num2 * 2, theDest.mHeight - num3 * 2);
			for (int i = 0; i < (theDest.mWidth - num2 * 2 + num4 - 1) / num4; i++)
			{
				for (int j = 0; j < (theDest.mHeight - num3 * 2 + num5 - 1) / num5; j++)
				{
					g.DrawImage(theComponentImage, theDest.mX + num2 + i * num4, theDest.mY + num3 + j * num5, new Rect(mX + num2, mY + num3, num4, num5));
				}
			}
			g.mClipRect = mClipRect;
		}

		public static void DrawDividerCentered(Graphics g, int x, int y)
		{
			Bej3Widget.DrawDividerCentered(g, x, y, true);
		}

		public static void DrawDividerCentered(Graphics g, int x, int y, bool isLong)
		{
			if (Bej3Widget.Draw_diamondXOffs == null)
			{
				Bej3Widget.Draw_diamondXOffs = new int?(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM.GetCelWidth() / 2);
			}
			if (Bej3Widget.Draw_diamondYOffs == null)
			{
				Bej3Widget.Draw_diamondYOffs = new int?(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM.GetCelHeight() / 2);
			}
			if (Bej3Widget.Draw_leftCurlXOffs == null)
			{
				Bej3Widget.Draw_leftCurlXOffs = new int?((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1341) - GlobalMembersResourcesWP.ImgXOfs(1340)));
			}
			if (Bej3Widget.Draw_leftCurlYOffs == null)
			{
				Bej3Widget.Draw_leftCurlYOffs = new int?((int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1341) - GlobalMembersResourcesWP.ImgYOfs(1340)));
			}
			if (Bej3Widget.Draw_rightCurlXOffs == null)
			{
				Bej3Widget.Draw_rightCurlXOffs = Bej3Widget.Draw_leftCurlXOffs - GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER.GetCelWidth();
			}
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM, x - Bej3Widget.Draw_diamondXOffs.Value, y - Bej3Widget.Draw_diamondYOffs.Value);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER, x - Bej3Widget.Draw_leftCurlXOffs.Value - Bej3Widget.Draw_diamondXOffs.Value - ConstantsWP.MENU_DIVIDER_DIAMOND_OFFSET_LEFT, y - Bej3Widget.Draw_leftCurlYOffs.Value - Bej3Widget.Draw_diamondYOffs.Value);
			g.DrawImageMirror(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER, x + Bej3Widget.Draw_rightCurlXOffs.Value + Bej3Widget.Draw_diamondXOffs.Value + ConstantsWP.MENU_DIVIDER_DIAMOND_OFFSET_RIGHT, y - Bej3Widget.Draw_leftCurlYOffs.Value - Bej3Widget.Draw_diamondYOffs.Value, true);
		}

		public static void DrawInlayBox(Graphics g, Rect dest, int textWidth)
		{
			Bej3Widget.DrawInlayBox(g, dest, textWidth, false);
		}

		public static void DrawInlayBox(Graphics g, Rect dest, int textWidth, bool darkBackground)
		{
			if (Bej3Widget.curlHeight == null)
			{
				Bej3Widget.curlHeight = new int?(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER.mHeight);
			}
			Bej3Widget.DrawImageBoxTileCenter(g, new Rect(dest.mX, dest.mY + ConstantsWP.LISTBOX_BG_OFFSET_TOP, dest.mWidth, dest.mHeight - ConstantsWP.LISTBOX_BG_OFFSET_TOP - ConstantsWP.LISTBOX_BG_OFFSET_BOTTOM), darkBackground ? GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_BG_DARK : GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_BG);
			g.DrawImageBox(new Rect(dest.mX, dest.mY + ConstantsWP.LISTBOX_HEADING_OFFSET, dest.mWidth, GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_HEADER.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_HEADER);
			g.DrawImageBox(new Rect(dest.mX, dest.mY + dest.mHeight - GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_FOOTER.mHeight - ConstantsWP.LISTBOX_FOOTER_OFFSET, dest.mWidth, GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_FOOTER.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_FOOTER);
			Point point = new Point(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM.mWidth, GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM.mHeight);
			int num = dest.mWidth / 2;
			int num2 = dest.mHeight - ConstantsWP.HIGHSCORESWIDGET_BOTTOM_CURL_Y;
			point = new Point((int)((float)GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM.mWidth * ConstantsWP.HIGHSCORESWIDGET_BOTTOM_CURL_SCALE), (int)((float)GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM.mHeight * ConstantsWP.HIGHSCORESWIDGET_BOTTOM_CURL_SCALE));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM, dest.mX + num - point.mX / 2, dest.mY + num2 - point.mY, point.mX, point.mY);
			int highscoreswidget_BOTTOM_CURL_OFFSET = ConstantsWP.HIGHSCORESWIDGET_BOTTOM_CURL_OFFSET;
			int num3 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(1341) - GlobalMembersResourcesWP.ImgYOfs(1340));
			Point point2 = new Point((int)((float)GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER.mWidth * ConstantsWP.HIGHSCORESWIDGET_BOTTOM_CURL_SCALE), (int)((float)GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER.mHeight * ConstantsWP.HIGHSCORESWIDGET_BOTTOM_CURL_SCALE));
			g.DrawImageMirror(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER, new Rect(dest.mX + dest.mWidth / 2 - highscoreswidget_BOTTOM_CURL_OFFSET + point.mX / 2, dest.mY + num2 - point.mY - num3, point2.mX, Bej3Widget.curlHeight.Value * (int)ConstantsWP.HIGHSCORESWIDGET_BOTTOM_CURL_SCALE), GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER.GetCelRect(0), true);
			int num4 = highscoreswidget_BOTTOM_CURL_OFFSET - point2.mX - point.mX / 2;
			g.DrawImageMirror(GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER, new Rect(dest.mX + dest.mWidth / 2 + num4, dest.mY + num2 - point.mY - num3, point2.mX, Bej3Widget.curlHeight.Value * (int)ConstantsWP.HIGHSCORESWIDGET_BOTTOM_CURL_SCALE), GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER.GetCelRect(0), false);
		}

		public static void DrawInlayBoxShadow(Graphics g, Rect dest)
		{
			g.SetColor(Color.White);
			g.DrawImageBox(new Rect(dest.mX + ConstantsWP.LISTBOX_SHADOW_X, dest.mY + ConstantsWP.LISTBOX_SHADOW_Y + ConstantsWP.LISTBOX_BG_OFFSET_TOP, dest.mWidth - ConstantsWP.LISTBOX_SHADOW_X * 2, dest.mHeight - ConstantsWP.LISTBOX_SHADOW_Y * 2 - ConstantsWP.LISTBOX_BG_OFFSET_TOP - ConstantsWP.LISTBOX_BG_OFFSET_BOTTOM), GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_SHADOW);
		}

		public static void DrawInlay(Graphics g, Rect theRect, bool drawHeader)
		{
			g.DrawImageBox(theRect, GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_BG);
			if (drawHeader)
			{
				g.DrawImageBox(new Rect(theRect.mX + ConstantsWP.DIALOG_INLAY_HEADING_OFFSET_LEFT, theRect.mY + ConstantsWP.DIALOG_INLAY_HEADING_OFFSET_Y, theRect.mWidth - ConstantsWP.DIALOG_INLAY_HEADING_OFFSET_LEFT - ConstantsWP.DIALOG_INLAY_HEADING_OFFSET_RIGHT, GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_HEADER.GetCelHeight()), GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_HEADER);
			}
		}

		public override void SetDisabled(bool isDisabled)
		{
			base.SetDisabled(isDisabled);
			foreach (Widget widget in this.mWidgets)
			{
				widget.SetDisabled(isDisabled);
			}
		}

		public override void DrawAll(ModalFlags theFlags, Graphics g)
		{
			float num = (float)this.mAlphaCurve;
			g.SetColor(new Color(255, 255, 255, (int)(255f * num)));
			if (num != 1f)
			{
				g.PushColorMult();
			}
			base.DrawAll(theFlags, g);
			if (num != 1f)
			{
				g.PopColorMult();
			}
		}

		public override void UpdateAll(ModalFlags theFlags)
		{
			if (!this.mVisible && this.mState != Bej3WidgetState.STATE_FADING_OUT && !this.mAlphaCurve.IsDoingCurve())
			{
				return;
			}
			base.UpdateAll(theFlags);
		}

		public override void Update()
		{
			this.mAlphaCurve;
			bool flag = Bej3Widget.FloatEquals((float)this.mY + this.mAnimationFraction, (float)this.mTargetPos, ConstantsWP.DASHBOARD_SLIDER_SPEED);
			if (!flag && this.mAllowSlide)
			{
				int num = ((this.mY < this.mTargetPos) ? 1 : (-1));
				this.mAnimationFraction += (float)this.mY + ((float)(this.mTargetPos - this.mY) - this.mAnimationFraction) * ConstantsWP.DASHBOARD_SLIDER_SPEED_SCALAR + ConstantsWP.DASHBOARD_SLIDER_SPEED * (float)num;
				this.mY = (int)this.mAnimationFraction;
				this.mAnimationFraction -= (float)this.mY;
			}
			if (this.mAllowSlide && Bej3Widget.FloatEquals((float)this.mY + this.mAnimationFraction, (float)this.mTargetPos, ConstantsWP.DASHBOARD_SLIDER_SPEED))
			{
				this.mY = this.mTargetPos;
				this.mAnimationFraction = 0f;
			}
			switch (this.mCurrentTansitionState)
			{
			case TRANSITION_STATE.TRANSITION_STATE_SLIDE_TO_BOTTOM:
				if (flag)
				{
					this.mAlphaCurve.SetConstant(0.0);
					this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_IDLE;
				}
				break;
			case TRANSITION_STATE.TRANSITION_STATE_SLIDE_THEN_FADE_IN:
				if (flag)
				{
					this.mAllowFade = true;
					flag = this.mAlphaCurve == 1.0;
				}
				break;
			}
			if (this.mAllowFade)
			{
				this.mAlphaCurve.IncInVal();
			}
			if (this.mSlidingForDialog != null && flag)
			{
				this.mSlidingForDialog.AllowSlideIn(true, this.mTopButton);
				this.mSlidingForDialog = null;
				this.SetVisible(false);
			}
			switch (this.mState)
			{
			case Bej3WidgetState.STATE_FADING_IN:
				if (flag && !this.mAlphaCurve.IsDoingCurve())
				{
					this.mY = this.mTargetPos;
					this.SetDisabled(false);
					this.mState = Bej3WidgetState.STATE_IN;
					this.ShowCompleted();
					this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_IDLE;
				}
				break;
			case Bej3WidgetState.STATE_FADING_OUT:
				if (flag && !this.mAlphaCurve.IsDoingCurve())
				{
					this.mY = this.mTargetPos;
					this.SetVisible(false);
					this.mState = Bej3WidgetState.STATE_OUT;
					if (this.mCanAllowSlide && GlobalMembers.gApp != null && GlobalMembers.gApp.GetDialogCount() == 0)
					{
						foreach (Bej3Widget bej3Widget in GlobalMembers.gApp.mMenus)
						{
							if (bej3Widget != null && this != bej3Widget)
							{
								bej3Widget.AllowSlideIn(true, this.mTopButton);
							}
						}
					}
					if (Bej3Widget.mCurrentSlidingMenu != null && Bej3Widget.mCurrentSlidingMenu == this)
					{
						Bej3Widget.mCurrentSlidingMenu = null;
					}
					this.HideCompleted();
					if (Bej3Widget.mCurrentSlidingMenu != null && this.mCanAllowSlide && this.mCurrentTansitionState == TRANSITION_STATE.TRANSITION_STATE_SLIDE_TO_BOTTOM)
					{
						Bej3Widget.mCurrentSlidingMenu.mY = this.mY;
					}
					this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_IDLE;
				}
				break;
			}
			base.Update();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			this.mFinalY = theY;
			this.mTargetPos = theY;
			if (this.mDoesSlideInFromBottom)
			{
				this.mY = ConstantsWP.MENU_Y_POS_HIDDEN;
			}
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public virtual void Show()
		{
			this.mIgnoreButtons = false;
			if ((this.mState == Bej3WidgetState.STATE_IN || this.mState == Bej3WidgetState.STATE_FADING_IN) && !this.NeedsShowTransition())
			{
				return;
			}
			this.SetDisabled(true);
			this.SetVisible(true);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal((PreCalculatedCurvedValManager.CURVED_VAL_ID)this.GetShowCurve(), this.mAlphaCurve);
			this.mState = Bej3WidgetState.STATE_FADING_IN;
			this.mY = ConstantsWP.MENU_Y_POS_HIDDEN;
			this.PlayMenuMusic();
			if (GlobalMembers.gApp.mTooltipManager != null)
			{
				GlobalMembers.gApp.mTooltipManager.ClearTooltipsWithAnimation();
			}
			this.mTargetPos = this.mFinalY;
			this.mAllowSlide = false;
			this.mAllowFade = !this.mDoesSlideInFromBottom;
			if (this.mDoesSlideInFromBottom)
			{
				if (Bej3Widget.mCurrentSlidingMenu != null)
				{
					Bej3Widget.mCurrentSlidingMenu.mSlidingForDialog = null;
				}
				Bej3Widget.mCurrentSlidingMenu = this;
			}
			BejeweledLivePlusApp.mIdleTicksForButton = 0;
		}

		public virtual void Hide()
		{
			this.mIgnoreButtons = true;
			if (this.mState == Bej3WidgetState.STATE_OUT || this.mState == Bej3WidgetState.STATE_FADING_OUT)
			{
				return;
			}
			this.SetDisabled(true);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_HIDE_CURVE, this.mAlphaCurve);
			this.mState = Bej3WidgetState.STATE_FADING_OUT;
			this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
			this.mAllowFade = true;
		}

		public virtual bool NeedsShowTransition()
		{
			return false;
		}

		public virtual void HideCompleted()
		{
		}

		public virtual void ShowCompleted()
		{
		}

		public virtual void InterfaceStateChanged(InterfaceState newState)
		{
			this.mInterfaceState = newState;
			this.LinkUpAssets();
		}

		public override void LinkUpAssets()
		{
		}

		public virtual void PlayMenuMusic()
		{
			GlobalMembers.gApp.mMusic.PlaySongNoDelay(1, true);
		}

		public virtual int GetShowCurve()
		{
			return 23;
		}

		public virtual void ShowBackButton(bool doShow)
		{
		}

		public virtual void ButtonMouseEnter(int theId)
		{
		}

		public virtual void ButtonDepress(int theId)
		{
		}

		public virtual bool ButtonsEnabled()
		{
			return !this.mIgnoreButtons && this.mState == Bej3WidgetState.STATE_IN;
		}

		public virtual void AllowSlideIn(bool allow, Bej3Button previousTopButton)
		{
			if (allow)
			{
				this.mFadingBackground = false;
			}
			this.mAllowFade = allow;
			if (this.mAllowSlide == allow)
			{
				return;
			}
			this.mAllowSlide = allow;
			bool visible = (allow && this.mState != Bej3WidgetState.STATE_OUT) || this.mState == Bej3WidgetState.STATE_IN;
			if (this.mState != Bej3WidgetState.STATE_FADING_OUT)
			{
				this.SetVisible(visible);
			}
			if (previousTopButton != null && this.mTopButton != null && allow && this.mState != Bej3WidgetState.STATE_OUT)
			{
				this.mTopButton.SetType(previousTopButton.GetType());
			}
		}

		public Bej3WidgetState GetWidgetState()
		{
			return this.mState;
		}

		public void SetTopButtonType(Bej3ButtonType topButtonType)
		{
			if (this.mTopButton != null)
			{
				this.mTopButton.SetType(topButtonType);
			}
		}

		public static bool SlideCurrent(bool slideOut, Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
			return Bej3Widget.mCurrentSlidingMenu != null && Bej3Widget.mCurrentSlidingMenu.SlideForDialog(slideOut, dialog, previousButtonType);
		}

		public static void NotifyCurrentDialogFinished(Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
			if (Bej3Widget.mCurrentSlidingMenu != null)
			{
				Bej3Widget.mCurrentSlidingMenu.DialogFinished(dialog, previousButtonType);
			}
		}

		public static void NotifyCurrentDialogFinishedSlidingIn(Bej3Dialog dialog, Bej3ButtonType previousButtonType)
		{
			if (Bej3Widget.mCurrentSlidingMenu != null)
			{
				Bej3Widget.mCurrentSlidingMenu.DialogFinishedSlidingIn(dialog, previousButtonType);
			}
		}

		public static void ClearSlide(Bej3Dialog dialog)
		{
			if (Bej3Widget.mCurrentSlidingMenu != null)
			{
				Bej3Widget.mCurrentSlidingMenu.ClearSlidingForDialog(dialog);
			}
		}

		public void SetDisabledTopButton(bool disabled)
		{
			if (this.mTopButton != null)
			{
				this.mTopButton.mMouseVisible = !disabled;
				this.mTopButton.SetDisabled(disabled);
			}
		}

		public int GetTargetPosition()
		{
			return this.mTargetPos;
		}

		public void SetTargetPosition(int targetPos)
		{
			this.mTargetPos = targetPos;
		}

		public void Transition_SlideOut()
		{
			this.mTargetPos = ConstantsWP.MENU_Y_POS_HIDDEN;
			this.mAllowSlide = true;
			this.mAllowFade = false;
			this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_SLIDE_TO_BOTTOM;
		}

		public void Transition_SlideThenFadeIn()
		{
			this.mAllowSlide = true;
			this.mAllowFade = true;
			this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_SLIDE_THEN_FADE_IN;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_SHOW_CURVE, this.mAlphaCurve);
			this.SetVisible(true);
		}

		public void Transition_FadeIn()
		{
			this.mY = this.mTargetPos;
			this.mAllowSlide = false;
			this.mAllowFade = true;
			this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_FADE_IN;
			this.SetVisible(true);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_TRANSITION_FADE_IN, this.mAlphaCurve);
		}

		public void Transition_FadeOut()
		{
			this.mAllowSlide = false;
			this.mAllowFade = true;
			this.mCurrentTansitionState = TRANSITION_STATE.TRANSITION_STATE_FADE_OUT;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBEJ3_WIDGET_TRANSITION_FADE_OUT, this.mAlphaCurve);
		}

		public TRANSITION_STATE GetTransitionState()
		{
			return this.mCurrentTansitionState;
		}

		public Bej3WidgetState GetState()
		{
			return this.mState;
		}

		public bool IsInOutPosition()
		{
			return this.mY != 106 && this.mY != this.mFinalY && this.mY != ConstantsWP.MENU_Y_POS_HIDDEN;
		}

		public bool IsTransitioning()
		{
			return this.mY != this.mTargetPos;
		}

		public virtual void ButtonPress(int theId)
		{
		}

		public virtual void ButtonPress(int theId, int theClickCount)
		{
		}

		public virtual void ButtonDownTick(int theId)
		{
		}

		public virtual void ButtonMouseLeave(int theId)
		{
		}

		public virtual void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		protected TRANSITION_STATE mCurrentTansitionState;

		public static readonly Color COLOR_HYPERLINK = new Color(16777215);

		public static readonly Color COLOR_HYPERLINK_HOVER = new Color(14540287);

		public static readonly Color COLOR_INGAME_ANNOUNCEMENT = new Color(0, 185, 244);

		public static readonly Color COLOR_SCORE = new Color(24, 32, 99);

		public static readonly Color COLOR_DIGBOARD_SCORE_GLOW = new Color(182, 0, 209);

		public static readonly Color COLOR_DIGBOARD_SCORE_STROKE = new Color(255, 255, 255);

		public static readonly Color COLOR_TRANSPARENT = new Color(0, 0, 0, 0);

		public static readonly Color COLOR_FORCE_WHITE = new Color(50000, 50000, 50000, 255);

		public static readonly Color COLOR_HYPERLINK_STROKE = new Color(81, 239, 245);

		public static readonly Color COLOR_HYPERLINK_FILL = new Color(81, 239, 245);

		public static readonly Color COLOR_TOOLTIP_FILL = new Color(228, 125, 55);

		public static readonly Color COLOR_HEADING_GLOW_1 = new Color(165, 59, 219);

		public static readonly Color COLOR_CRYSTALBALL_FONT = new Color(165, 59, 219);

		public static readonly Color COLOR_CRYSTALBALL_FONT_CLASSIC = new Color(0, 185, 244);

		public static readonly Color COLOR_SUBHEADING_1_FILL = new Color(155, 16, 98);

		public static readonly Color COLOR_SUBHEADING_1_STROKE = new Color(255, 255, 255);

		public static readonly Color COLOR_SUBHEADING_2_FILL = new Color(255, 226, 197);

		public static readonly Color COLOR_SUBHEADING_2_STROKE = new Color(135, 50, 39);

		public static readonly Color COLOR_SUBHEADING_3_FILL = new Color(165, 59, 219);

		public static readonly Color COLOR_SUBHEADING_3_STROKE = new Color(255, 255, 255);

		public static readonly Color COLOR_SUBHEADING_4_FILL = new Color(255, 255, 255);

		public static readonly Color COLOR_SUBHEADING_4_STROKE = new Color(18, 40, 120);

		public static readonly Color COLOR_SUBHEADING_5_FILL = new Color(255, 255, 255);

		public static readonly Color COLOR_SUBHEADING_5_STROKE = new Color(59, 9, 82);

		public static readonly Color COLOR_SUBHEADING_6_FILL = new Color(255, 255, 255);

		public static readonly Color COLOR_SUBHEADING_6_STROKE = new Color(4, 113, 14);

		public static readonly Color COLOR_SUBHEADING_HYPERLINK_FILL = new Color(0, 0, 255);

		public static readonly Color COLOR_SUBHEADING_HYPERLINK_STROKE = new Color(255, 255, 255);

		public static readonly Color COLOR_DIALOG_WHITE = new Color(255, 255, 255);

		public static readonly Color COLOR_DIALOG_1_FILL = new Color(112, 4, 67);

		public static readonly Color COLOR_DIALOG_2_FILL = new Color(249, 220, 106);

		public static readonly Color COLOR_DIALOG_3_FILL = new Color(40, 143, 155);

		public static readonly Color COLOR_DIALOG_4_FILL = new Color(118, 20, 187);

		public static readonly Color COLOR_DIALOG_HYPERLINK = new Color(255, 255, 255);

		private static Bej3Widget.OVERLAY_TYPE mOverlayType = Bej3Widget.OVERLAY_TYPE.OVERLAY_NONE;

		public Bej3WidgetState mState;

		protected InterfaceState mInterfaceState;

		protected bool mIgnoreButtons;

		public int mTargetPos;

		protected float mAnimationFraction;

		public int mFinalY;

		protected bool mAllowFade;

		protected bool mAllowSlide;

		protected bool mCanAllowSlide;

		protected bool mDoesSlideInFromBottom;

		public bool mUpdateWhenNotVisible;

		public bool mShouldFadeBehind;

		public CurvedVal mAlphaCurve = new CurvedVal();

		public Menu_Type mType;

		public static bool mDialogBoxDrawnThisFrame = false;

		public static Bej3Widget mCurrentSlidingMenu = null;

		public Bej3Button mTopButton;

		public Bej3Dialog mSlidingForDialog;

		public bool mFadingBackground;

		private static int? Draw_diamondXOffs;

		private static int? Draw_diamondYOffs;

		private static int? Draw_leftCurlXOffs;

		private static int? Draw_leftCurlYOffs;

		private static int? Draw_rightCurlXOffs;

		private static int? curlHeight;

		public enum OVERLAY_TYPE
		{
			OVERLAY_NONE,
			OVERLAY_RUSTY,
			OVERLAY_ICE
		}

		public enum BEJ3WIDGETBUTTON_IDS
		{
			BEJ3BUTTON_TOP_ID = 10001
		}
	}
}
