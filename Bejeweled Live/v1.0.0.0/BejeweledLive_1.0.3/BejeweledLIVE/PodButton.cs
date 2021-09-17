using System;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class PodButton : InterfaceButton
	{
		public PodButton(int id, ButtonListener listener)
			: base(id, listener)
		{
			this.mRingImage = AtlasResources.IMAGE_PILL_RING;
			this.mHeight = this.DefaultHeight();
		}

		protected override void Reset(int theId, ButtonListener theButtonListener)
		{
			base.Reset(theId, theButtonListener);
			this.FadeWhenDisabled = false;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public int DefaultWidth()
		{
			return Math.Max(this.mNormalRect.mWidth, this.mRingImage.mWidth);
		}

		public int DefaultHeight()
		{
			int num = 0;
			if (this.mRingImage != null)
			{
				num = this.mRingImage.mHeight;
			}
			return Math.Max(this.mNormalRect.mHeight, num);
		}

		public override void Resize(int x, int y, int width, int height)
		{
			base.Resize(x, y, width, height);
			this.ComputeDrawFrames();
		}

		public override void Resize(TRect frame)
		{
			base.Resize(frame);
		}

		public override void Draw(Graphics g)
		{
			int num = (int)(this.mOpacity * 255f);
			int num2 = (int)(this.mOpacity * (float)(this.IsButtonDown() ? 255 : 127));
			g.SetColorizeImages(true);
			if (this.mDisabled && this.FadeWhenDisabled)
			{
				num = (int)((float)num * 0.4f);
				num2 = (int)((float)num2 * 0.4f);
			}
			if (num != 0)
			{
				g.SetColor(new Color(255, 255, 255, num));
				this.DrawPill(g);
			}
			if (num2 != 0)
			{
				g.SetColor(new Color(255, 255, 255, num2));
				this.DrawRing(g);
			}
			if (!string.IsNullOrEmpty(this.mLabel) && this.mFont != null && num != 0)
			{
				g.SetFont(this.mFont);
				g.SetColor(new Color(255, 255, 255, num));
				g.DrawString(this.mLabel, this.mLabelOffset.mX, this.mLabelOffset.mY);
			}
		}

		protected virtual void ComputeDrawFrames()
		{
			if (this.mNormalRect.mWidth < this.mRingImage.mWidth)
			{
				this.mRingFrame.mX = 0;
				this.mRingFrame.mWidth = this.mWidth;
				this.mPillFrame.mX = (this.mRingImage.mWidth - this.mNormalRect.mWidth) / 2;
				this.mPillFrame.mWidth = this.mWidth - (this.mRingImage.mWidth - this.mNormalRect.mWidth);
			}
			else
			{
				this.mRingFrame.mX = (this.mNormalRect.mWidth - this.mRingImage.mWidth) / 2;
				this.mRingFrame.mWidth = this.mWidth - (this.mNormalRect.mWidth - this.mRingImage.mWidth);
				this.mPillFrame.mX = 0;
				this.mPillFrame.mWidth = this.mWidth;
			}
			this.mRingFrame.mY = (this.mHeight - this.mRingImage.mHeight) / 2;
			this.mRingFrame.mHeight = this.mRingImage.mHeight;
			this.mPillFrame.mY = (this.mHeight - this.mNormalRect.mHeight) / 2;
			this.mPillFrame.mHeight = this.mNormalRect.mHeight;
			if (!string.IsNullOrEmpty(this.mLabel) && this.mFont != null)
			{
				if (this.mFont != null)
				{
					this.mFont.mScaleX = (this.mFont.mScaleY = (this.mTextScale = 1f));
					float num = (float)this.mFont.StringWidth(this.mLabel) + PodButton.mTextOffsetSide;
					if (num > (float)this.mWidth)
					{
						this.mTextScale = (float)this.mWidth / num;
					}
				}
				this.mFont.mScaleX = (this.mFont.mScaleY = this.mTextScale);
				this.mLabelOffset.mX = this.mPillFrame.mX + (this.mPillFrame.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
				this.mLabelOffset.mY = this.mPillFrame.mY + (this.mPillFrame.mHeight / 2 - this.mFont.StringHeight(this.mLabel) / 2);
			}
		}

		protected virtual void DrawPill(Graphics g)
		{
			g.DrawImageBox(this.mNormalRect, this.mPillFrame, this.mButtonImage);
		}

		protected virtual void DrawRing(Graphics g)
		{
			g.DrawImageBox(this.mRingFrame, this.mRingImage);
		}

		public override string ToString()
		{
			return "PodButton: " + this.mLabel;
		}

		public override string mLabel
		{
			get
			{
				return base.mLabel;
			}
			set
			{
				base.mLabel = value;
				this.ComputeDrawFrames();
			}
		}

		private const float DISABLED_BUTTON_OPACITY = 0.4f;

		protected static readonly float mTextOffsetSide = Constants.mConstants.S(90f);

		public bool FadeWhenDisabled;

		protected Image mRingImage;

		protected TRect mRingFrame = default(TRect);

		protected TRect mPillFrame = default(TRect);

		protected TPoint mLabelOffset = default(TPoint);

		protected float mTextScale = 1f;
	}
}
