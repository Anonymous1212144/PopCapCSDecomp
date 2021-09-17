using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class FancySmallDoubleButton : FancyPodButton
	{
		public FancySmallDoubleButton(int idTop, int idBottom, int colour, string topLabel, string bottomLabel, ButtonListener listener)
			: this(idTop, idBottom, 0, 0, topLabel, bottomLabel, listener)
		{
		}

		public FancySmallDoubleButton(int idTop, int idBottom, int numButtons, int buttonNum, string topLabel, string bottomLabel, ButtonListener listener)
			: base(idTop, numButtons, buttonNum, string.Empty, listener)
		{
			this.Reset(idTop, idBottom, numButtons, buttonNum, topLabel, bottomLabel, listener);
		}

		protected override void Reset(int id, int numButtons, int buttonNum, string label, ButtonListener listener)
		{
			base.Reset(id, numButtons, buttonNum, string.Empty, listener);
			this.SetFont(Resources.FONT_BUTTON);
			this.mButtonImage = AtlasResources.IMAGE_PILL_SMALL_CENTER1;
			this.mRingImage = AtlasResources.IMAGE_DOUBLE_BUTTON;
			this.mOverlayImage = AtlasResources.IMAGE_PILL_SMALL_CENTER2;
			this.mAdditiveImage = AtlasResources.IMAGE_PILL_SMALL_ADDITIVE;
			this.mNormalRect = this.mButtonImage.GetCelRect(0);
			this.mWidth = base.DefaultWidth();
			this.mHeight = base.DefaultHeight();
			this.ComputeDrawFrames();
			this.SetFont(Resources.FONT_BUTTON);
			this.mIsFancy = false;
		}

		protected void Reset(int idTop, int idBottom, int numButtons, int buttonNum, string topLabel, string bottomLabel, ButtonListener listener)
		{
			this.Reset(idTop, numButtons, buttonNum, string.Empty, listener);
			this.mIdTop = idTop;
			this.mIdBottom = idBottom;
			this.mTopLabel = topLabel;
			this.mBottomLabel = bottomLabel;
			this.mSelected = FancySmallDoubleButton.Selection.Top;
		}

		protected void Reset(int id, int colour, string label, ButtonListener listener)
		{
			this.Reset(id, 0, 0, label, listener);
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		protected override void ComputeDrawFrames()
		{
			base.ComputeDrawFrames();
			this.topFrame = new TRect(this.mPillFrame.mX, this.mHeight / 4 - this.mPillFrame.mHeight / 2, this.mPillFrame.mWidth, this.mPillFrame.mHeight);
			this.bottomframe = new TRect(this.mPillFrame.mX, this.mHeight / 3 * 2 - this.mPillFrame.mHeight / 2, this.mPillFrame.mWidth, this.mPillFrame.mHeight);
			if (this.mFont != null)
			{
				this.mFont.mScaleX = (this.mFont.mScaleY = (this.mTextScale = 1f));
				float num = 0f;
				if (!string.IsNullOrEmpty(this.mTopLabel))
				{
					int num2 = 0;
					if (this.TopRightImage != null)
					{
						num2 = this.TopRightImage.GetWidth() + (int)PodButton.mTextOffsetSide;
					}
					num = Math.Max(num, (float)(this.mFont.StringWidth(this.mTopLabel) + num2) + PodButton.mTextOffsetSide);
				}
				if (!string.IsNullOrEmpty(this.mBottomLabel))
				{
					int num3 = 0;
					if (this.BottomRightImage != null)
					{
						num3 = this.BottomRightImage.GetWidth() + (int)PodButton.mTextOffsetSide;
					}
					num = Math.Max(num, (float)(this.mFont.StringWidth(this.mBottomLabel) + num3) + PodButton.mTextOffsetSide);
				}
				if (num > (float)this.mWidth)
				{
					this.mTextScale = (float)this.mWidth / num;
				}
			}
			if (!string.IsNullOrEmpty(this.mTopLabel) && this.mFont != null)
			{
				if (this.TopRightImage != null)
				{
					GlobalStaticVars.g.SetFont(this.mFont);
					GlobalStaticVars.g.SetScale(this.mTextScale);
				}
				this.mTopLabelOffset.mX = this.mWidth / 2 - this.mFont.StringWidth(this.mTopLabel) / 2;
				this.mTopLabelOffset.mY = this.mHeight / 4 - this.mFont.StringHeight(this.mTopLabel) / 2;
				GlobalStaticVars.g.SetScale(1f);
			}
			if (!string.IsNullOrEmpty(this.mBottomLabel) && this.mFont != null)
			{
				if (this.BottomRightImage != null)
				{
					GlobalStaticVars.g.SetFont(this.mFont);
					GlobalStaticVars.g.SetScale(this.mTextScale);
				}
				this.mBottomLabelOffset.mX = this.mWidth / 2 - this.mFont.StringWidth(this.mBottomLabel) / 2;
				this.mBottomLabelOffset.mY = this.mHeight / 3 * 2 - this.mFont.StringHeight(this.mBottomLabel) / 2;
				GlobalStaticVars.g.SetScale(1f);
			}
			if (this.TopRightImage != null)
			{
				GlobalStaticVars.g.SetFont(this.mFont);
				GlobalStaticVars.g.SetScale(this.mTextScale);
				this.mTopLabelOffset.mX = this.mTopLabelOffset.mX - this.TopRightImage.mWidth / 2;
				this.mTopRightImageOffset.mX = this.mTopLabelOffset.mX + this.mFont.StringWidth(this.mTopLabel) + Constants.mConstants.FrameWidget_Image_Offset_X;
				this.mTopRightImageOffset.mY = this.mHeight / 4 - this.TopRightImage.mHeight / 2;
				GlobalStaticVars.g.SetScale(1f);
			}
			if (this.BottomRightImage != null)
			{
				GlobalStaticVars.g.SetFont(this.mFont);
				GlobalStaticVars.g.SetScale(this.mTextScale);
				this.mBottomLabelOffset.mX = this.mBottomLabelOffset.mX - this.BottomRightImage.mWidth / 2;
				this.mBottomRightImageOffset.mX = this.mBottomLabelOffset.mX + this.mFont.StringWidth(this.mBottomLabel) + Constants.mConstants.FrameWidget_Image_Offset_X;
				this.mBottomRightImageOffset.mY = this.mHeight / 3 * 2 - this.TopRightImage.mHeight / 2;
				GlobalStaticVars.g.SetScale(1f);
			}
		}

		protected void DrawPill(Graphics g, FancySmallDoubleButton.Selection button)
		{
			if (button == FancySmallDoubleButton.Selection.Top)
			{
				g.DrawImageBox(this.mNormalRect, this.topFrame, this.mButtonImage);
			}
			if (button == FancySmallDoubleButton.Selection.Bottom)
			{
				g.DrawImageBox(this.mNormalRect, this.bottomframe, this.mButtonImage);
			}
		}

		protected void DrawTheOverlay(Graphics g, FancySmallDoubleButton.Selection button)
		{
			if (button == FancySmallDoubleButton.Selection.Top)
			{
				g.DrawImageBox(this.mNormalRect, this.topFrame, this.mOverlayImage);
			}
			if (button == FancySmallDoubleButton.Selection.Bottom)
			{
				g.DrawImageBox(this.mNormalRect, this.bottomframe, this.mOverlayImage);
			}
		}

		protected override void DrawTheAdditive(Graphics g)
		{
			if (this.IsTopButtonDown() && this.mSelected != FancySmallDoubleButton.Selection.Top)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.DrawImageBox(this.mNormalRect, this.topFrame, this.mAdditiveImage);
			}
			if (this.IsBottomButtonDown() && this.mSelected != FancySmallDoubleButton.Selection.Bottom)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.DrawImageBox(this.mNormalRect, this.bottomframe, this.mAdditiveImage);
			}
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
		}

		public override void Draw(Graphics g)
		{
			int num = (int)(this.mOpacity * 225f);
			int num2 = (int)(this.mOpacity * 255f);
			int num3 = (int)(this.mOpacity * 255f);
			int num4 = 255;
			int num5 = 255;
			int num6 = 255;
			int num7 = 255;
			int num8 = 255;
			int num9 = (int)(this.mOpacity * ((this.buttonCount != 0) ? ((float)this.buttonNumber / (float)this.buttonCount * 255f) : 0f));
			g.SetColorizeImages(true);
			if (this.mSelected == FancySmallDoubleButton.Selection.Top || this.mDisabled)
			{
				num4 = (int)((float)num4 * 0.4f);
				num5 = (int)((float)num5 * 0.4f);
			}
			if (this.mSelected == FancySmallDoubleButton.Selection.Bottom || this.mDisabled)
			{
				num6 = (int)((float)num6 * 0.4f);
				num7 = (int)((float)num7 * 0.4f);
			}
			if (num3 != 0)
			{
				g.SetColor(new Color(num8, num8, num8, num3));
				this.DrawRing(g);
			}
			if (this.buttonCount != 0 && this.buttonNumber == 0 && this.mIsFancy)
			{
				g.SetColor(new Color(num8, num8, num8, num));
				g.ClearClipRect();
				g.DrawImage(AtlasResources.IMAGE_PILL_TOP, this.mWidth / 2 - AtlasResources.IMAGE_PILL_TOP.GetWidth() / 2, -AtlasResources.IMAGE_PILL_TOP.GetHeight() + Constants.mConstants.PodButtonFancy_Top_Offset);
			}
			if (this.buttonCount != 0 && this.buttonNumber == this.buttonCount && this.mIsFancy)
			{
				g.SetColor(new Color(num8, num8, num8, num));
				g.ClearClipRect();
				g.DrawImage(AtlasResources.IMAGE_PILL_BOT, this.mWidth / 2 - AtlasResources.IMAGE_PILL_BOT.GetWidth() / 2, this.mHeight - Constants.mConstants.PodButtonFancy_Bottom_Offset);
			}
			if (num != 0)
			{
				g.SetColor(new Color(num5, num5, num5, num));
				this.DrawPill(g, FancySmallDoubleButton.Selection.Top);
				g.SetColor(new Color(num7, num7, num7, num));
				this.DrawPill(g, FancySmallDoubleButton.Selection.Bottom);
			}
			if (num9 != 0)
			{
				g.SetColor(new Color(num5, num5, num5, num));
				this.DrawTheOverlay(g, FancySmallDoubleButton.Selection.Top);
				g.SetColor(new Color(num7, num7, num7, num));
				this.DrawTheOverlay(g, FancySmallDoubleButton.Selection.Bottom);
			}
			g.SetColor(new Color(num8, num8, num8, 127));
			this.DrawTheAdditive(g);
			if (this.TopRightImage != null)
			{
				g.SetColor(new Color(num5, num5, num5, num));
				g.DrawImage(this.TopRightImage, this.mTopRightImageOffset.mX, this.mTopRightImageOffset.mY);
			}
			if (this.BottomRightImage != null)
			{
				g.SetColor(new Color(num7, num7, num7, num));
				g.DrawImage(this.BottomRightImage, this.mBottomRightImageOffset.mX, this.mBottomRightImageOffset.mY);
			}
			if (!string.IsNullOrEmpty(this.mTopLabel) && this.mFont != null && num != 0)
			{
				g.SetFont(this.mFont);
				g.SetScale(this.mTextScale);
				g.SetColor(new Color(num4, num4, num4, num2));
				g.DrawString(this.mTopLabel, this.mTopLabelOffset.mX, this.mTopLabelOffset.mY);
				g.SetScale(1f);
			}
			if (!string.IsNullOrEmpty(this.mBottomLabel) && this.mFont != null && num != 0)
			{
				g.SetFont(this.mFont);
				g.SetScale(this.mTextScale);
				g.SetColor(new Color(num6, num6, num6, num2));
				g.DrawString(this.mBottomLabel, this.mBottomLabelOffset.mX, this.mBottomLabelOffset.mY);
				g.SetScale(1f);
			}
		}

		public override void MouseDown(int theX, int theY, int theClickCount)
		{
			this.mouseDownCoords = new TPoint(theX, theY);
			if ((this.IsInTopButton(theX, theY) && this.mSelected == FancySmallDoubleButton.Selection.Top) || (this.IsInBottomButton(theX, theY) && this.mSelected == FancySmallDoubleButton.Selection.Bottom))
			{
				return;
			}
			base.MouseDown(theX, theY, theClickCount);
		}

		public bool IsTopButtonDown()
		{
			return this.mIsDown && this.mIsOver && !this.mDisabled && this.IsInTopButton(this.mouseDownCoords.x, this.mouseDownCoords.y);
		}

		public bool IsBottomButtonDown()
		{
			return this.mIsDown && this.mIsOver && !this.mDisabled && this.IsInBottomButton(this.mouseDownCoords.x, this.mouseDownCoords.y);
		}

		private bool IsInTopButton(int x, int y)
		{
			return y <= this.mHeight / 2;
		}

		private bool IsInBottomButton(int x, int y)
		{
			return y > this.mHeight / 2;
		}

		public override void MouseDown(int theX, int theY, int theBtnNum, int theClickCount)
		{
			if ((this.IsInTopButton(theX, theY) && this.mSelected == FancySmallDoubleButton.Selection.Top) || (this.IsInBottomButton(theX, theY) && this.mSelected == FancySmallDoubleButton.Selection.Bottom))
			{
				return;
			}
			base.MouseDown(theX, theY, theBtnNum, theClickCount);
			this.mButtonListener.ButtonPress(this.mId, theClickCount);
			this.MarkDirty();
		}

		public override void MouseUp(int theX, int theY, int theBtnNum, int theClickCount)
		{
			if (this.mIsOver && this.mWidgetManager.mHasFocus)
			{
				if (this.IsInTopButton(theX, theY) && this.mSelected != FancySmallDoubleButton.Selection.Top)
				{
					this.mButtonListener.ButtonDepress(this.mIdTop);
					this.mSelected = FancySmallDoubleButton.Selection.Top;
				}
				else if (this.IsInBottomButton(theX, theY) && this.mSelected != FancySmallDoubleButton.Selection.Bottom)
				{
					this.mButtonListener.ButtonDepress(this.mIdBottom);
					this.mSelected = FancySmallDoubleButton.Selection.Bottom;
				}
			}
			this.MarkDirty();
		}

		private TRect topFrame;

		private TRect bottomframe;

		public Image TopRightImage;

		public Image BottomRightImage;

		private TPoint mTopRightImageOffset;

		private TPoint mBottomRightImageOffset;

		private TPoint mTopLabelOffset;

		private TPoint mBottomLabelOffset;

		public string mTopLabel;

		public string mBottomLabel;

		public FancySmallDoubleButton.Selection mSelected;

		private TPoint mouseDownCoords;

		private int mIdTop;

		private int mIdBottom;

		public enum Selection
		{
			Top,
			Bottom,
			None
		}
	}
}
