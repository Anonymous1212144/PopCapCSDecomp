using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class CheatWidget : Widget
	{
		public CheatWidget(Widget theTarget, string theCheats, Font theFont)
		{
			float num = ((GameApp.mGameRes == 768) ? 1f : ((GameApp.mGameRes == 640) ? 2f : 1.5f));
			this.mButtonSize = (int)((float)CheatWidget.BUTTON_SIZE * num);
			this.mClient = theTarget;
			this.mCheatChars = theCheats;
			int length = theCheats.Length;
			this.mButtonsPerRow = (GameApp.gApp.GetScreenRect().mWidth + GameApp.gApp.GetScreenRect().mX - GameApp.gApp.mBoardOffsetX * 2) / this.mButtonSize;
			this.mCols = this.mButtonsPerRow;
			this.mRows = (length + this.mButtonsPerRow - 1) / this.mButtonsPerRow;
			this.mWidth = this.mCols * this.mButtonSize + 1;
			this.mHeight = this.mRows * this.mButtonSize + 1;
			this.mAlignment = true;
			this.mEnable = true;
		}

		public override void Draw(Graphics g)
		{
			if (!this.mEnable)
			{
				return;
			}
			g.SetColor(new Color(255, 200));
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			int num = 0;
			for (int i = 0; i < this.mRows; i++)
			{
				for (int j = 0; j < this.mCols; j++)
				{
					Rect theRect = new Rect(j * this.mButtonSize + 1, i * this.mButtonSize + 1, this.mButtonSize - 2, this.mButtonSize - 2);
					g.SetColor(20, 20, 20);
					g.FillRect(theRect);
				}
				int num2 = 0;
				while (num < this.mCheatChars.Length && num2 < this.mCols)
				{
					((GameMain)GameApp.gApp.mGameMain).DrawSysString(string.Concat(this.mCheatChars.get_Chars(num)), (float)(num2 * this.mButtonSize + this.mButtonSize / 2 - 10) * 800f / 1066f, (float)(i * this.mButtonSize + this.mY + this.mButtonSize / 2 - 10) * 800f / 1066f);
					num2++;
					num++;
				}
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (!this.mEnable)
			{
				return;
			}
			int num = y / this.mButtonSize;
			int num2 = x / this.mButtonSize;
			int num3 = num * this.mButtonsPerRow + num2;
			if (num3 < this.mCheatChars.Length)
			{
				if (this.mCheatChars.get_Chars(num3) == 'X')
				{
					GameApp.gApp.mStepMode = 0;
					GameApp.gApp.ClearUpdateBacklog(false);
					this.mEnable = false;
					this.SetVisible(false);
					return;
				}
				if (this.mCheatChars.get_Chars(num3) == 'j')
				{
					this.SwapAlignment();
					return;
				}
				this.mClient.KeyChar(this.mCheatChars.get_Chars(num3));
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
		}

		public void SwapAlignment()
		{
			if (this.mAlignment)
			{
				this.Move(this.mX, GameApp.gApp.GetScreenRect().mHeight - this.mHeight);
				this.mAlignment = false;
				return;
			}
			this.Move(this.mX, 0);
			this.mAlignment = true;
		}

		public string mCheatChars;

		public Widget mClient;

		public int mRows;

		public int mCols;

		public int mButtonsPerRow;

		public int mButtonSize;

		public bool mAlignment;

		public bool mEnable;

		private static int BUTTON_SIZE = Common._DS(80);
	}
}
