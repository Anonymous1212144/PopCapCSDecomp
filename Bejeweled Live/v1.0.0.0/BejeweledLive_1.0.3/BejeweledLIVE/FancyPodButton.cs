using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public abstract class FancyPodButton : PodButton
	{
		public FancyPodButton(int id, int aNumButtons, int aButtonNum, string aLabel, ButtonListener listener)
			: base(id, listener)
		{
			this.Reset(id, aNumButtons, aButtonNum, aLabel, listener);
		}

		protected virtual void Reset(int id, int aNumButtons, int aButtonNum, string aLabel, ButtonListener listener)
		{
			base.Reset(id, listener);
			this.buttonCount = aNumButtons;
			this.buttonNumber = aButtonNum;
			this.mIsFancy = false;
			this.mLabel = aLabel;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override void Draw(Graphics g)
		{
			int num = (int)(this.mOpacity * 225f);
			int num2 = (int)(this.mOpacity * 255f);
			int num3 = (int)(this.mOpacity * 255f);
			int num4 = 255;
			int num5 = 255;
			int num6 = 255;
			int num7 = (int)(this.mOpacity * (float)((this.buttonCount != 0) ? (255 / this.buttonCount * this.buttonNumber) : 0));
			g.SetColorizeImages(true);
			if (this.mDisabled && this.FadeWhenDisabled)
			{
				num4 = (int)((float)num6 * 0.4f);
				num5 = (int)((float)num5 * 0.4f);
			}
			if (num3 != 0)
			{
				g.SetColor(new Color(num6, num6, num6, num3));
				this.DrawRing(g);
			}
			if (this.buttonCount != 0 && this.buttonNumber == 0 && this.mIsFancy)
			{
				g.SetColor(new Color(num5, num5, num5, num));
				g.ClearClipRect();
				g.DrawImage(AtlasResources.IMAGE_PILL_TOP, this.mWidth / 2 - AtlasResources.IMAGE_PILL_TOP.GetWidth() / 2, -AtlasResources.IMAGE_PILL_TOP.GetHeight() + Constants.mConstants.PodButtonFancy_Top_Offset);
			}
			if (this.buttonCount != 0 && this.buttonNumber == this.buttonCount && this.mIsFancy)
			{
				g.SetColor(new Color(num5, num5, num5, num));
				g.ClearClipRect();
				g.DrawImage(AtlasResources.IMAGE_PILL_BOT, this.mWidth / 2 - AtlasResources.IMAGE_PILL_BOT.GetWidth() / 2, this.mHeight - Constants.mConstants.PodButtonFancy_Bottom_Offset);
			}
			if (num != 0)
			{
				g.SetColor(new Color(num5, num5, num5, num));
				this.DrawPill(g);
			}
			if (num7 != 0)
			{
				g.SetColor(new Color(num5, num5, num5, num7));
				this.DrawTheOverlay(g);
			}
			if (this.IsButtonDown())
			{
				g.SetColor(new Color(num5, num5, num5, 127));
				this.DrawTheAdditive(g);
			}
			if (this.RightImage != null)
			{
				g.SetColor(new Color(num5, num5, num5, num));
				g.DrawImage(this.RightImage, this.mRightImageOffset, this.mHeight / 2 - this.RightImage.mHeight / 2);
			}
			if (!string.IsNullOrEmpty(this.mLabel) && this.mFont != null && num != 0)
			{
				g.SetFont(this.mFont);
				g.SetColor(new Color(num4, num4, num4, num2));
				g.SetScale(this.mTextScale);
				g.DrawString(this.mLabel, this.mLabelOffset.mX, this.mLabelOffset.mY);
				g.SetScale(1f);
			}
		}

		protected override void DrawPill(Graphics g)
		{
			g.DrawImageBox(this.mNormalRect, this.mPillFrame, this.mButtonImage);
		}

		protected override void DrawRing(Graphics g)
		{
			g.DrawImageBox(this.mRingFrame, this.mRingImage);
		}

		protected virtual void DrawTheOverlay(Graphics g)
		{
			g.DrawImageBox(this.mNormalRect, this.mPillFrame, this.mOverlayImage);
		}

		protected virtual void DrawTheAdditive(Graphics g)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
			g.DrawImageBox(this.mNormalRect, this.mPillFrame, this.mAdditiveImage);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
		}

		protected override void ComputeDrawFrames()
		{
			base.ComputeDrawFrames();
			if (this.RightImage != null)
			{
				this.mLabelOffset.mX = this.mLabelOffset.mX - this.RightImage.mWidth / 2;
				this.mRightImageOffset = this.mLabelOffset.mX + this.mFont.StringWidth(this.mLabel) + Constants.mConstants.FrameWidget_Image_Offset_X;
			}
		}

		public int ButtonNumber
		{
			get
			{
				return this.buttonNumber;
			}
			set
			{
				this.buttonNumber = value;
			}
		}

		public int ButtonCount
		{
			get
			{
				return this.buttonCount;
			}
			set
			{
				this.buttonCount = value;
			}
		}

		public Image RightImage
		{
			get
			{
				return this.rightImage;
			}
			set
			{
				this.rightImage = value;
				this.ComputeDrawFrames();
			}
		}

		protected const float DISABLED_BUTTON_OPACITY = 0.4f;

		private Image rightImage;

		private int mRightImageOffset;

		protected Image mOverlayImage;

		protected Image mAdditiveImage;

		public bool mIsFancy;

		protected int buttonCount;

		protected int buttonNumber;
	}
}
