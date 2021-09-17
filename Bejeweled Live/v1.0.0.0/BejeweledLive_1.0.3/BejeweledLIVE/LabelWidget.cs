using System;
using Sexy;

namespace BejeweledLIVE
{
	public class LabelWidget : InterfaceControl
	{
		public void SetInsets(Insets insets)
		{
			this.mContentInsets = insets;
		}

		public void SizeToFit()
		{
			int theWidth = (int)(this.mScale * (float)this.mFont.StringWidth(this.mText) + (float)this.mContentInsets.mLeft + (float)this.mContentInsets.mRight);
			int theHeight = (int)(this.mScale * (float)this.mFont.GetHeight() + (float)this.mContentInsets.mTop + (float)this.mContentInsets.mBottom);
			this.Resize(0, 0, theWidth, theHeight);
		}

		public int GetJustification()
		{
			return this.mJustification;
		}

		public void SetJustification(int theJustification)
		{
			this.mJustification = Math.Min(1, Math.Max(theJustification, -1));
		}

		public int GetLineJustification()
		{
			return this.mLineJustification;
		}

		public void SetScale(float scale)
		{
			this.mScale = scale;
			this.mFont.mScaleX = (this.mFont.mScaleY = this.mScale);
		}

		public void SetLineJustification(int theJustification)
		{
			this.mLineJustification = Math.Min(1, Math.Max(theJustification, -1));
		}

		public override void Draw(Graphics g)
		{
			int theX = 0;
			int theY = 0;
			g.SetFont(this.mFont);
			g.SetScale(this.mScale);
			switch (this.mJustification)
			{
			case -1:
				theX = this.mContentInsets.mLeft;
				break;
			case 0:
				theX = (this.mWidth + this.mContentInsets.mLeft - this.mContentInsets.mRight - this.mFont.StringWidth(this.mText)) / 2;
				break;
			case 1:
				theX = this.mWidth - this.mContentInsets.mRight - this.mFont.StringWidth(this.mText);
				break;
			}
			switch (this.mLineJustification)
			{
			case -1:
				theY = this.mContentInsets.mTop + this.mFont.GetAscent();
				break;
			case 0:
				theY = this.mContentInsets.mTop + (this.mHeight - this.mContentInsets.mTop - this.mContentInsets.mBottom - this.mFont.GetHeight()) / 2 + this.mFont.GetAscent();
				break;
			case 1:
				theY = this.mHeight - this.mContentInsets.mBottom - this.mFont.GetHeight() + this.mFont.GetAscent();
				break;
			}
			SexyColor aColor = (SexyColor)this.GetColor(0);
			if (this.mOpacity < 1f)
			{
				aColor.mAlpha = (int)(this.mOpacity * (float)aColor.mAlpha);
				g.SetColorizeImages(true);
			}
			g.SetColor(new SexyColor(aColor));
			g.DrawString(this.mText, theX, theY);
			g.SetScale(1f);
		}

		public override void MarkDirty()
		{
			if (((SexyColor)this.mColors[0]).mAlpha != 255)
			{
				base.MarkDirtyFull();
				return;
			}
			base.MarkDirty();
		}

		public LabelWidget(string theText, Font theFont)
		{
			this.mText = theText;
			this.mFont = theFont;
			this.mClip = false;
			this.SetColors(LabelWidget.gWidgetColors, 1);
			this.mContentInsets = new Insets(0, 0, 0, 0);
			this.mJustification = -1;
			this.mLineJustification = -1;
			this.SizeToFit();
		}

		public override void Dispose()
		{
		}

		public override string ToString()
		{
			return "Label: " + this.mText;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static LabelWidget()
		{
			int[,] array = new int[1, 4];
			array[0, 3] = 255;
			LabelWidget.gWidgetColors = array;
		}

		private static int[,] gWidgetColors;

		public int mId;

		public string mText = string.Empty;

		public Font mFont;

		public int mJustification;

		public int mLineJustification;

		public Insets mContentInsets = default(Insets);

		private float mScale = 1f;

		public enum Color
		{
			COLOR_TEXT,
			NUM_COLORS
		}

		public enum Justify
		{
			JUSTIFY_TOP = -1,
			JUSTIFY_LEFT = -1,
			JUSTIFY_CENTER,
			JUSTIFY_RIGHT,
			JUSTIFY_BOTTOM = 1
		}
	}
}
