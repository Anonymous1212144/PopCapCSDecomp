using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class RotatingSeg : Widget
	{
		public RotatingSeg()
		{
			this.mChar = '0';
			this.mNumRevs = 0;
			this.mClip = default(Rect);
			this.mFont = null;
			this.mDoMax = false;
			this.mIndex = 0;
		}

		public RotatingSeg(char theChar, int numRevs, int index, bool doMax, Rect clip, Font font)
		{
			this.mChar = theChar;
			this.mNumRevs = numRevs;
			this.mClip = clip;
			this.mFont = font;
			this.mDoMax = doMax;
			this.mIndex = index;
			this.mY = this.mClip.mY + (this.mClip.mHeight + this.mFont.GetAscent()) / 2;
		}

		public virtual void Draw(Graphics g, float yOffs)
		{
			Rect clipRect = this.mClip;
			clipRect.Offset(0, (int)yOffs);
			g.SetClipRect(clipRect);
			g.DrawString(this.mChar.ToString(), this.mClip.mX + (this.mClip.mWidth - this.mFont.CharWidth(this.mChar)) / 2, (int)((float)(this.mY - ConstantsWP.ROTATING_COUNTER_OFFSET_Y) + yOffs));
			if ((float)(this.mY - ConstantsWP.ROTATING_COUNTER_OFFSET_Y) + yOffs + (float)this.mClip.mHeight - (float)this.mFont.GetAscent() < (float)(this.mClip.mY + this.mClip.mHeight))
			{
				char nextChar = this.GetNextChar();
				g.DrawString(nextChar.ToString(), this.mClip.mX + (this.mClip.mWidth - this.mFont.CharWidth(nextChar)) / 2, this.mY - (int)((float)ConstantsWP.ROTATING_COUNTER_OFFSET_Y + yOffs + (float)this.mClip.mHeight));
			}
		}

		public char GetNextChar()
		{
			char result = '\0';
			if (this.mDoMax && this.mNumRevs == 1)
			{
				switch (this.mIndex)
				{
				case 0:
					result = 'M';
					break;
				case 1:
					result = 'A';
					break;
				case 2:
					result = 'X';
					break;
				}
			}
			else if (this.mChar == '9')
			{
				result = '0';
			}
			else
			{
				switch (this.mChar)
				{
				case '0':
					result = '1';
					break;
				case '1':
					result = '2';
					break;
				case '2':
					result = '3';
					break;
				case '3':
					result = '4';
					break;
				case '4':
					result = '5';
					break;
				case '5':
					result = '6';
					break;
				case '6':
					result = '7';
					break;
				case '7':
					result = '8';
					break;
				case '8':
					result = '9';
					break;
				default:
					result = '0';
					break;
				}
			}
			return result;
		}

		public char mChar;

		public int mNumRevs;

		public new Rect mClip = default(Rect);

		public Font mFont;

		public bool mDoMax;

		public int mIndex;
	}
}
