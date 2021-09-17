using System;
using System.Collections.Generic;
using Sexy;

namespace BejeweledLIVE
{
	public class Points : Widget
	{
		public static Points GetNewPoints(GameApp theApp, Font theFont, string theString, int theX, int theY, int theLife, int theJustification, SexyColor theColor)
		{
			if (Points.unusedPoints.Count == 0)
			{
				return new Points(theApp, theFont, theString, theX, theY, theLife, theJustification, theColor);
			}
			Points points = Points.unusedPoints.Pop();
			points.Reset(theApp, theFont, theString, theX, theY, theLife, theJustification, theColor);
			return points;
		}

		public void Reset(GameApp theApp, Font theFont, string theString, int theX, int theY, int theLife, int theJustification, SexyColor theColor)
		{
			this.mMouseVisible = false;
			this.mApp = theApp;
			this.mFont = theFont;
			this.mString = theString;
			this.mLife = theLife;
			this.mStartLife = this.mLife;
			int num = this.mFont.StringWidth(theString);
			int num2 = theX;
			if (theJustification == 0)
			{
				num2 -= num / 2;
			}
			else if (theJustification == 1)
			{
				num2 -= num;
			}
			if (num2 < 0)
			{
				num2 = 2;
			}
			else if (num2 + num >= theApp.mWidth)
			{
				num2 = theApp.mWidth - num - 2;
			}
			int theY2 = theY - this.mFont.GetAscent();
			this.Resize(num2, theY2, num, this.mFont.GetHeight());
			this.mColor.CopyFrom(theColor);
			this.mFont = theFont;
			this.mScale = 0f;
			this.mScaleAdd = 0f;
		}

		private Points(GameApp theApp, Font theFont, string theString, int theX, int theY, int theLife, int theJustification, SexyColor theColor)
		{
			this.Reset(theApp, theFont, theString, theX, theY, theLife, theJustification, theColor);
		}

		public void PrepareForReuse()
		{
			Points.unusedPoints.Push(this);
		}

		public override void Dispose()
		{
			this.PrepareForReuse();
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			double num = (double)(this.mStartLife - this.mLife) / 8.0;
			double num2 = Math.Min(num, (double)this.mLife / 18.0);
			if (num2 > 1.0)
			{
				num2 = 1.0;
			}
			g.SetFont(this.mFont);
			g.SetColor(new SexyColor(this.mColor.mRed, this.mColor.mGreen, this.mColor.mBlue, (int)(num2 * 255.0)));
			g.DrawString(this.mString, 0, this.mFont.GetAscent() + 1);
		}

		public GameApp mApp;

		public int mLife;

		private int mStartLife;

		public string mString = string.Empty;

		public Font mFont;

		public SexyColor mColor = default(SexyColor);

		public float mScale;

		public float mDestScale;

		public float mScaleAdd;

		public float mScaleDampening;

		public float mScaleDifMult;

		private static Stack<Points> unusedPoints = new Stack<Points>();
	}
}
