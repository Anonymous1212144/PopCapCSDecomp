using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Widget
{
	public class Label : Bej3WidgetBase
	{
		public void Init(Font font, string theText, Label_Alignment_Horizontal horizontalAlignment, Label_Alignment_Vertical verticalAlignment)
		{
			this.mLineSpacingOffset = 0;
			this.mScaleOverriden = false;
			this.mTextBlockAlignment = 0;
			this.mWidgetFlagsMod.mRemoveFlags |= 8;
			this.mWidgetFlagsMod.mRemoveFlags |= 16;
			this.mUsesCustomClipRect = false;
			this.mTextWidth = 0;
			this.mForceSplitHeading = false;
			this.mCenterTextBlockInY = false;
			this.mClippingEnabled = true;
			this.mMaxWidth = 0;
			this.mScale = 1f;
			this.mIsTextBlock = false;
			this.mTextBlockOffsetY = 0;
			this.SetFont(font);
			this.SetText(theText);
			this.SetAlignment(horizontalAlignment, verticalAlignment);
		}

		public Label(Font font, string theText, Label_Alignment_Horizontal horizontalAlignment, Label_Alignment_Vertical verticalAlignment)
		{
			this.Init(font, theText, horizontalAlignment, verticalAlignment);
		}

		public Label(Font font, string theText, Label_Alignment_Horizontal horizontalAlignment)
		{
			Label_Alignment_Vertical verticalAlignment = Label_Alignment_Vertical.LABEL_ALIGNMENT_VERTICAL_CENTRE;
			this.Init(font, theText, horizontalAlignment, verticalAlignment);
		}

		public Label(Font font, string theText)
		{
			Label_Alignment_Horizontal horizontalAlignment = Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE;
			Label_Alignment_Vertical verticalAlignment = Label_Alignment_Vertical.LABEL_ALIGNMENT_VERTICAL_CENTRE;
			this.Init(font, theText, horizontalAlignment, verticalAlignment);
		}

		public Label(Font font, Label_Alignment_Horizontal horizontalAlignment, Label_Alignment_Vertical verticalAlignment)
		{
			this.Init(font, "", horizontalAlignment, verticalAlignment);
		}

		public Label(Font font, Label_Alignment_Horizontal horizontalAlignment)
		{
			Label_Alignment_Vertical verticalAlignment = Label_Alignment_Vertical.LABEL_ALIGNMENT_VERTICAL_CENTRE;
			this.Init(font, "", horizontalAlignment, verticalAlignment);
		}

		public Label(Font font)
		{
			Label_Alignment_Horizontal horizontalAlignment = Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE;
			Label_Alignment_Vertical verticalAlignment = Label_Alignment_Vertical.LABEL_ALIGNMENT_VERTICAL_CENTRE;
			this.Init(font, "", horizontalAlignment, verticalAlignment);
		}

		public override void Update()
		{
			base.WidgetUpdate();
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
				g.ClearClipRect();
				g.mClipRect.mX = g.mClipRect.mX - 5000;
				g.mClipRect.mWidth = g.mClipRect.mWidth + 10000;
				g.mClipRect.mY = g.mClipRect.mY - 5000;
				g.mClipRect.mHeight = g.mClipRect.mHeight + 10000;
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

		public void SetText(string theText)
		{
			if (this.mForceSplitHeading)
			{
				this.mForceSplitHeading = false;
				this.mIsTextBlock = false;
			}
			this.mText = theText;
			this.CalcOffset();
		}

		public void SetAlignment(Label_Alignment_Horizontal horizontalAlignment, Label_Alignment_Vertical verticalAlignment)
		{
			this.mHorizontalAlignment = horizontalAlignment;
			this.mVerticalAlignment = verticalAlignment;
			this.CalcOffset();
		}

		public void SetClippingEnabled(bool clippingEnabled)
		{
			this.mClippingEnabled = clippingEnabled;
		}

		public void SetFont(Font theFont)
		{
			this.mFont = (ImageFont)theFont;
			this.mDescentOffset = this.mFont.GetHeight() - this.mFont.GetAscent();
			this.CalcOffset();
			this.mLayerColours.Clear();
			for (int i = 0; i < this.mFont.GetLayerCount(); i++)
			{
				this.mLayerColours.Add(Color.White);
			}
			if (theFont == GlobalMembersResources.FONT_HUGE)
			{
				this.mLayerColours[0] = Bej3Widget.COLOR_HEADING_GLOW_1;
				return;
			}
			if (theFont == GlobalMembersResources.FONT_SUBHEADER)
			{
				this.mLayerColours[1] = Bej3Widget.COLOR_SUBHEADING_1_FILL;
				this.mLayerColours[0] = Bej3Widget.COLOR_SUBHEADING_1_STROKE;
				return;
			}
			if (theFont == GlobalMembersResources.FONT_DIALOG)
			{
				this.mLayerColours[0] = Bej3Widget.COLOR_DIALOG_1_FILL;
			}
		}

		public Font GetFont()
		{
			return this.mFont;
		}

		public void SetColor(Color theColor)
		{
			this.mColor = new Color(theColor);
		}

		public void CalcOffset()
		{
			if (this.mIsTextBlock)
			{
				base.Resize(this.mTextBlock.mX, this.mTextBlock.mY, this.mTextBlock.mWidth, this.mTextBlock.mHeight);
				int visibleHeight = this.GetVisibleHeight(this.mTextBlock.mWidth);
				this.mTextBlockOffsetY = this.mHeight / 2 - visibleHeight / 2;
				return;
			}
			int num = this.mFont.StringWidth(this.mText);
			int num2 = this.mFont.GetHeight();
			if (!this.mScaleOverriden)
			{
				this.mScale = 1f;
				if (this.mMaxWidth > 0 && num > this.mMaxWidth)
				{
					this.mScale = (float)this.mMaxWidth / (float)num;
					if (this.mScale < 0.5f)
					{
						this.mForceSplitHeading = true;
						this.mScale = 0.5f;
						this.mIsTextBlock = true;
						this.SetTextBlock(new Rect(this.mX, this.mY, this.mMaxWidth, 0), true);
					}
					num = this.mMaxWidth;
					num2 *= (int)this.mScale;
				}
			}
			this.mTextWidth = num;
			Point point = default(Point);
			switch (this.mHorizontalAlignment)
			{
			case Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE:
				point.mX = -num / 2;
				this.mDrawOffset.mX = 0;
				break;
			case Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT:
				point.mX = 0;
				this.mDrawOffset.mX = 0;
				break;
			case Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT:
				point.mX = -num;
				this.mDrawOffset.mX = 0;
				break;
			default:
				point.mX = 0;
				this.mDrawOffset.mX = 0;
				break;
			}
			switch (this.mVerticalAlignment)
			{
			case Label_Alignment_Vertical.LABEL_ALIGNMENT_VERTICAL_CENTRE:
				point.mY = -num2 / 2;
				this.mDrawOffset.mY = num2;
				break;
			case Label_Alignment_Vertical.LABEL_ALIGNMENT_VERTICAL_TOP:
				point.mY = 0;
				this.mDrawOffset.mY = num2;
				break;
			case Label_Alignment_Vertical.LABEL_ALIGNMENT_VERTICAL_BOTTOM:
				point.mY = 0;
				this.mDrawOffset.mY = num2;
				break;
			default:
				point.mY = 0;
				this.mDrawOffset.mY = 0;
				break;
			}
			int theX = this.mBasePosition.mX + point.mX;
			int theY = this.mBasePosition.mY + point.mY + this.mDescentOffset;
			base.Resize(theX, theY, num, num2);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			this.mBasePosition.mX = theX;
			this.mBasePosition.mY = theY;
			if (this.mForceSplitHeading)
			{
				this.mTextBlock.mX = (int)((float)theX - (float)this.mTextBlock.mWidth * this.mScale / 2f);
				this.mTextBlock.mY = (int)((float)theY - (float)this.mTextBlock.mHeight * this.mScale / 2f);
			}
			this.CalcOffset();
		}

		public void SetTextBlock(Rect theBlock, bool centerInY)
		{
			this.mTextBlock = theBlock;
			this.mCenterTextBlockInY = centerInY;
			this.CalcOffset();
		}

		public void SetTextBlockEnabled(bool enabled)
		{
			this.mIsTextBlock = enabled;
			this.CalcOffset();
		}

		public void SetTextBlockAlignment(int theAlignment)
		{
			this.mTextBlockAlignment = theAlignment;
		}

		public int GetVisibleHeight(int theWidth)
		{
			if (this.mIsTextBlock)
			{
				Graphics graphics = new Graphics();
				graphics.SetFont(this.mFont);
				graphics.SetScale(this.mScale, this.mScale, 0f, 0f);
				return base.GetWordWrappedHeight(graphics, theWidth, this.mText, this.mFont.GetLineSpacing() - this.mLineSpacingOffset);
			}
			return this.mHeight;
		}

		public int GetVisibleWidth(int theHeight)
		{
			if (this.mIsTextBlock)
			{
				Graphics graphics = new Graphics();
				graphics.SetFont(this.mFont);
				graphics.SetScale(this.mScale, this.mScale, 0f, 0f);
				int theY = 0;
				if (this.mCenterTextBlockInY)
				{
					theY = this.mTextBlockOffsetY;
				}
				int theX = 0;
				int num = this.mTextBlock.mWidth;
				int num2 = this.mTextBlock.mHeight;
				if (this.mForceSplitHeading)
				{
					theY = -this.mSplitYOffset;
					num /= (int)this.mScale;
					num2 /= (int)this.mScale;
				}
				int result = 0;
				graphics.WriteWordWrapped(new Rect(theX, theY, num, num2), this.mText, this.mFont.GetLineSpacing() - this.mLineSpacingOffset, this.mTextBlockAlignment, ref result);
				return result;
			}
			return this.mWidth;
		}

		public void SetMaximumWidth(int maxWidth, int splitYOffset)
		{
			this.mMaxWidth = maxWidth;
			this.mSplitYOffset = splitYOffset;
			if (this.mMaxWidth <= 0 && this.mForceSplitHeading)
			{
				this.mIsTextBlock = false;
				this.mForceSplitHeading = false;
			}
			this.CalcOffset();
		}

		public void SetMaximumWidth(int maxWidth)
		{
			this.SetMaximumWidth(maxWidth, ConstantsWP.DIALOG_HEADING_LABEL_SPLIT_Y);
		}

		public void SetCustomClipRect(Rect theClipRect)
		{
			this.mUsesCustomClipRect = true;
			this.mCustomClipRect = theClipRect;
		}

		public int GetTextWidth()
		{
			return this.mTextWidth;
		}

		public Rect GetTextBlock()
		{
			return this.mTextBlock;
		}

		public void SetScale(float scale)
		{
			this.mScale = scale;
			this.mScaleOverriden = this.mScale != 1f;
		}

		public string GetText()
		{
			return this.mText;
		}

		public void SetLayerColor(int layer, Color colour)
		{
			GlobalMembers.DBG_ASSERT(layer < this.mLayerColours.Count && layer >= 0);
			this.mLayerColours[layer] = new Color(colour);
		}

		protected string mText = string.Empty;

		protected ImageFont mFont;

		protected Label_Alignment_Horizontal mHorizontalAlignment;

		protected Label_Alignment_Vertical mVerticalAlignment;

		protected new bool mClippingEnabled;

		protected bool mIsTextBlock;

		protected Rect mTextBlock;

		protected int mTextBlockOffsetY;

		protected int mTextBlockAlignment;

		protected bool mCenterTextBlockInY;

		protected Point mBasePosition = default(Point);

		protected Point mDrawOffset = default(Point);

		protected int mDescentOffset;

		protected int mMaxWidth;

		protected bool mScaleOverriden;

		protected float mScale;

		protected int mSplitYOffset;

		protected bool mForceSplitHeading;

		protected int mTextWidth;

		protected Rect mCustomClipRect;

		protected bool mUsesCustomClipRect;

		protected List<Color> mLayerColours = new List<Color>();

		public int mLineSpacingOffset;
	}
}
