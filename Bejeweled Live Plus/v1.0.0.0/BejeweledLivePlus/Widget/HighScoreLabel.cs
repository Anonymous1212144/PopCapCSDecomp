using System;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Widget
{
	public class HighScoreLabel : Label
	{
		public HighScoreLabel(Font font, Label_Alignment_Horizontal horizontalAlignment)
			: base(font, horizontalAlignment)
		{
		}

		public override void Update()
		{
			base.Update();
			if (this.mMaxScollWidth > 0 && this.mMaxScollWidth < this.mTextWidth && this.mUpdateCnt % 15 == 0)
			{
				this.mDrawOffset.mX = this.mDrawOffset.mX - 1;
				if (this.mDrawOffset.mX + this.mTextWidth + 10 < 0)
				{
					this.mDrawOffset.mX = this.mMaxScollWidth + 10;
				}
			}
		}

		public override void Draw(Graphics g)
		{
			g.PushState();
			g.SetColorizeImages(true);
			for (int i = 0; i < this.mLayerColours.Count; i++)
			{
				if (this.mFont == GlobalMembersResources.FONT_DIALOG && this.mGrayedOut && i == 0)
				{
					Utils.SetFontLayerColor(this.mFont, i, Bej3WidgetBase.GreyedOutColor);
				}
				else
				{
					Utils.SetFontLayerColor(this.mFont, i, this.mLayerColours[i]);
				}
			}
			Color color = default(Color);
			if (this.mGrayedOut)
			{
				color = new Color(Bej3WidgetBase.GreyedOutColor);
			}
			else
			{
				color = new Color(this.mColor);
			}
			if (color.mAlpha == 0)
			{
				return;
			}
			g.SetColor(color);
			g.SetFont(this.mFont);
			if (!this.mClippingEnabled)
			{
				g.ClearClipRect();
			}
			if (this.mUsesCustomClipRect)
			{
				g.SetClipRect((int)((float)this.mCustomClipRect.mX - g.mTransX), (int)((float)this.mCustomClipRect.mY - g.mTransY), this.mCustomClipRect.mWidth, this.mCustomClipRect.mHeight);
			}
			if (this.mScale != 1f)
			{
				int num;
				int num2;
				if (!this.mScaleOverriden)
				{
					num = this.mDrawOffset.mX;
					num2 = this.mDrawOffset.mY;
				}
				else
				{
					num = this.mBasePosition.mX - this.mX;
					num2 = this.mBasePosition.mY - this.mY;
				}
				Utils.PushScale(g, this.mScale, this.mScale, (float)num, (float)num2);
			}
			if (this.mMaxScollWidth > 0)
			{
				g.mClipRect.mWidth = this.mMaxScollWidth;
			}
			if (this.mIsTextBlock)
			{
				int theY = 0;
				if (this.mCenterTextBlockInY)
				{
					theY = this.mTextBlockOffsetY;
				}
				int theX = 0;
				int num3 = this.mTextBlock.mWidth;
				int num4 = this.mTextBlock.mHeight;
				if (this.mForceSplitHeading)
				{
					theY = -this.mSplitYOffset;
					num3 /= (int)this.mScale;
					num4 /= (int)this.mScale;
				}
				g.WriteWordWrapped(new Rect(theX, theY, num3, num4), this.mText, this.mFont.GetLineSpacing() - this.mLineSpacingOffset, this.mTextBlockAlignment);
			}
			else
			{
				g.DrawString(this.mText, this.mDrawOffset.mX, this.mDrawOffset.mY - this.mDescentOffset);
			}
			if (this.mScale != 1f)
			{
				Utils.PopScale(g);
			}
			g.PopState();
		}

		public int mMaxScollWidth;
	}
}
