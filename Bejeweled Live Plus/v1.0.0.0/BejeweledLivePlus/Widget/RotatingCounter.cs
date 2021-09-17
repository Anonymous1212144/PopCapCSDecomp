using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class RotatingCounter : Widget
	{
		public RotatingCounter(int displayNum, Rect clip, Font font)
		{
			this.mClip = clip;
			this.mFont = font;
			this.mMaxNum = (int)(Math.Pow(10.0, 3.0) - 1.0);
			this.mCurNumber = -1;
			this.mSegs.Capacity = 3;
			for (int i = 0; i < 3; i++)
			{
				this.mSegs.Add(new RotatingSeg());
				this.mSegs[i].mChar = '0';
				this.mSegs[i].mIndex = i;
				this.mSegs[i].mFont = font;
				this.mSegs[i].mNumRevs = 0;
			}
			this.ResetCounter(displayNum);
		}

		public override void Dispose()
		{
			foreach (RotatingSeg rotatingSeg in this.mSegs)
			{
				if (rotatingSeg != null)
				{
					rotatingSeg.Dispose();
				}
			}
			this.mSegs.Clear();
			base.Dispose();
		}

		public void IncByNum(int theNum)
		{
			if (this.mCurNumber <= this.mMaxNum && theNum != 0)
			{
				bool flag = this.mCurNumber + theNum > this.mMaxNum;
				int num = Math.Min(this.mCurNumber + theNum, this.mMaxNum);
				int num2 = this.mCurNumber;
				for (int i = 2; i >= 0; i--)
				{
					this.mSegs[i].mNumRevs = num - num2;
					num /= 10;
					num2 /= 10;
				}
				if (flag)
				{
					for (int j = 0; j < 3; j++)
					{
						this.mSegs[j].mNumRevs++;
						this.mSegs[j].mDoMax = true;
					}
				}
				this.mCurNumber = Math.Min(this.mCurNumber + theNum, this.mMaxNum);
			}
		}

		public static char CharFromNum(int num)
		{
			char[] array = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			char result = array[0];
			if (num >= 0 && num <= 9)
			{
				result = array[num];
			}
			return result;
		}

		public void ResetCounter(int theNum)
		{
			if (this.mCurNumber == theNum)
			{
				return;
			}
			this.mCurNumber = theNum;
			bool flag = theNum > this.mMaxNum;
			int num = this.mClip.mWidth / 3;
			int num2 = (int)Math.Pow(10.0, 2.0);
			string text = "MAX";
			for (int i = 0; i < 3; i++)
			{
				Rect rect = new Rect(this.mClip.mX + num * i, this.mClip.mY, num, this.mClip.mHeight);
				char mChar;
				if (theNum >= this.mMaxNum)
				{
					mChar = text.get_Chars(i);
				}
				else
				{
					mChar = RotatingCounter.CharFromNum(this.mCurNumber / num2 % 10);
				}
				this.mSegs[i].mChar = mChar;
				this.mSegs[i].mNumRevs = (flag ? 1 : 0);
				this.mSegs[i].mIndex = i;
				this.mSegs[i].mDoMax = flag;
				this.mSegs[i].mClip = rect;
				this.mSegs[i].mFont = this.mFont;
				this.mSegs[i].mY = rect.mY + (rect.mHeight + this.mFont.GetAscent()) / 2;
				num2 /= 10;
			}
		}

		public new virtual void Update()
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.mSegs[i].mNumRevs > 0)
				{
					bool flag = true;
					for (int j = i + 1; j < 3; j++)
					{
						if (this.mSegs[j].mChar != '9')
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						if (i == 0)
						{
							int num = 0;
							num++;
						}
						this.mSegs[i].mY -= 15;
						if (this.mSegs[i].mY <= this.mClip.mY)
						{
							this.mSegs[i].mChar = this.mSegs[i].GetNextChar();
							this.mSegs[i].mNumRevs--;
							this.mSegs[i].mY = this.mClip.mY + (this.mClip.mHeight + this.mFont.GetAscent()) / 2;
						}
					}
				}
			}
		}

		public virtual void Draw(Graphics g, float yOffs)
		{
			g.PushState();
			g.SetFont(this.mFont);
			g.SetColor(new Color(255, 0, 255));
			Utils.SetFontLayerColor((ImageFont)this.mFont, 0, Color.Black);
			for (int i = 0; i < 3; i++)
			{
				this.mSegs[i].Draw(g, yOffs);
			}
			g.PopState();
		}

		public List<RotatingSeg> mSegs = new List<RotatingSeg>();

		public new Rect mClip = default(Rect);

		public Font mFont;

		public int mMaxNum;

		public int mCurNumber;
	}
}
