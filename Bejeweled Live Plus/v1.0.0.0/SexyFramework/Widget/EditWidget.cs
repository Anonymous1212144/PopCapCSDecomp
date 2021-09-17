using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace SexyFramework.Widget
{
	public class EditWidget : Widget
	{
		public EditWidget(int theId, EditListener theEditListener)
		{
			this.mId = theId;
			this.mEditListener = theEditListener;
			this.mFont = null;
			this.mHadDoubleClick = false;
			this.mHilitePos = -1;
			this.mLastModifyIdx = -1;
			this.mLeftPos = 0;
			this.mUndoCursor = 0;
			this.mUndoHilitePos = 0;
			this.mLastModifyIdx = 0;
			this.mBlinkAcc = 0;
			this.mCursorPos = 0;
			this.mShowingCursor = false;
			this.mDrawSelOverride = false;
			this.mMaxChars = -1;
			this.mMaxPixels = -1;
			this.mPasswordChar = null;
			this.mBlinkDelay = 40;
			this.mClipInset = 4;
			this.mTextInset = 4;
			this.mCursorOffset = 0;
			this.mHiliteWidthAdd = 0;
			this.SetColors3(EditWidget.gEditWidgetColors, 5);
		}

		public void ClearWidthCheckFonts()
		{
			this.mWidthCheckList.Clear();
		}

		public void AddWidthCheckFont(Font theFont, int theMaxPixels)
		{
			EditWidget.WidthCheck widthCheck;
			widthCheck.mWidth = theMaxPixels;
			widthCheck.mFont = theFont.Duplicate();
			this.mWidthCheckList.Add(widthCheck);
		}

		public void AddWidthCheckFont(Font theFont)
		{
			int mWidth = -1;
			EditWidget.WidthCheck widthCheck;
			widthCheck.mWidth = mWidth;
			widthCheck.mFont = theFont.Duplicate();
			this.mWidthCheckList.Add(widthCheck);
		}

		public virtual void SetText(string theText, bool leftPosToZero)
		{
			this.mString = theText;
			this.mCursorPos = this.mString.Length;
			this.mHilitePos = 0;
			if (leftPosToZero)
			{
				this.mLeftPos = 0;
			}
			else
			{
				this.FocusCursor(true);
			}
			this.MarkDirty();
		}

		public virtual void SetText(string theText)
		{
			this.SetText(theText, true);
		}

		protected string GetDisplayString()
		{
			if (this.mPasswordChar == " ")
			{
				return this.mString;
			}
			if (this.mPasswordDisplayString.Length != this.mString.Length)
			{
				this.mPasswordDisplayString = this.mString;
			}
			return this.mPasswordDisplayString;
		}

		public override bool WantsFocus()
		{
			return true;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.FocusCursor(false);
		}

		public virtual void SetFont(Font theFont, Font theWidthCheckFont)
		{
			this.mFont = theFont.Duplicate();
			this.ClearWidthCheckFonts();
			if (theWidthCheckFont != null)
			{
				this.AddWidthCheckFont(theWidthCheckFont, -1);
			}
		}

		public virtual void SetFont(Font theFont)
		{
			this.mFont = theFont.Duplicate();
			Font font = null;
			this.ClearWidthCheckFonts();
			if (font != null)
			{
				this.AddWidthCheckFont(font, -1);
			}
		}

		public int MAX(int x, int y)
		{
			return (x > y) ? x : y;
		}

		public int MIN(int x, int y)
		{
			return (x < y) ? x : y;
		}

		public override void Draw(Graphics g)
		{
			string displayString = this.GetDisplayString();
			g.SetColor(this.mColors[0]);
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			for (int i = 0; i < 2; i++)
			{
				g.PushState();
				g.SetFont(this.mFont);
				if (i == 1)
				{
					int num = this.mFont.StringWidth(displayString.Substring(0, this.mCursorPos)) - this.mFont.StringWidth(displayString.Substring(0, this.mLeftPos)) + this.mTextInset;
					int num2 = num + 2;
					if (this.mHilitePos != -1 && this.mCursorPos != this.mHilitePos)
					{
						num2 = this.mFont.StringWidth(displayString.Substring(0, this.mHilitePos)) - this.mFont.StringWidth(displayString.Substring(0, this.mLeftPos)) + this.mTextInset;
					}
					if (!this.mShowingCursor)
					{
						num += 2;
					}
					num = this.MIN(this.MAX(0, num), this.mWidth - 8);
					num2 = this.MIN(this.MAX(0, num2), this.mWidth - 8);
					int num3 = 0;
					if (this.mHilitePos != -1 && this.mHilitePos != this.mCursorPos)
					{
						num3 = this.mHiliteWidthAdd;
					}
					g.ClipRect(this.mClipInset + this.MIN(num, num2) + this.mCursorOffset, (this.mHeight - this.mFont.GetHeight()) / 2, Math.Abs(num2 - num) + num3, this.mFont.GetHeight());
				}
				else
				{
					g.ClipRect(this.mClipInset, 0, this.mWidth - this.mClipInset * 2, this.mHeight);
				}
				bool flag = this.mHasFocus || this.mDrawSelOverride;
				if (i == 1 && flag)
				{
					g.SetColor(this.mColors[3]);
					g.FillRect(0, 0, this.mWidth, this.mHeight);
				}
				if (i == 0 || !flag)
				{
					g.SetColor(this.mColors[2]);
				}
				else
				{
					g.SetColor(this.mColors[4]);
				}
				g.DrawString(displayString.Substring(this.mLeftPos), this.mTextInset, (this.mHeight - this.mFont.GetHeight()) / 2 + this.mFont.GetAscent());
				g.PopState();
			}
			g.SetColor(this.mColors[1]);
			g.DrawRect(0, 0, this.mWidth - 1, this.mHeight - 1);
		}

		protected virtual void UpdateCaretPos()
		{
		}

		public override void GotFocus()
		{
			base.GotFocus();
			bool mTabletPC = this.mWidgetManager.mApp.mTabletPC;
			this.mWidgetManager.mApp.ShowKeyboard();
			this.mShowingCursor = true;
			this.mBlinkAcc = 0;
			this.MarkDirty();
		}

		public override void LostFocus()
		{
			base.LostFocus();
			bool mTabletPC = this.mWidgetManager.mApp.mTabletPC;
			this.mWidgetManager.mApp.HideKeyboard();
			this.mShowingCursor = false;
			this.MarkDirty();
		}

		public override void Update()
		{
			base.Update();
			if (this.mHasFocus)
			{
				if (this.mWidgetManager.mApp.mTabletPC)
				{
					this.UpdateCaretPos();
				}
				if (++this.mBlinkAcc > this.mBlinkDelay)
				{
					this.MarkDirty();
					this.mBlinkAcc = 0;
					this.mShowingCursor = !this.mShowingCursor;
				}
			}
		}

		public void EnforceMaxPixels()
		{
			if (this.mMaxPixels <= 0 && this.mWidthCheckList.size<EditWidget.WidthCheck>() == 0)
			{
				return;
			}
			if (this.mWidthCheckList.size<EditWidget.WidthCheck>() == 0)
			{
				while (this.mFont.StringWidth(this.mString) > this.mMaxPixels)
				{
					this.mString = this.mString.Substring(0, this.mString.Length - 1);
				}
				return;
			}
			for (int i = 0; i < this.mWidthCheckList.Count; i++)
			{
				int mWidth = this.mWidthCheckList[i].mWidth;
				if (mWidth <= 0)
				{
					mWidth = this.mMaxPixels;
					if (mWidth <= 0)
					{
						goto IL_C4;
					}
				}
				while (this.mWidthCheckList[i].mFont.StringWidth(this.mString) > mWidth)
				{
					this.mString = this.mString.Substring(0, this.mString.Length - 1);
				}
				IL_C4:;
			}
		}

		public virtual bool IsPartOfWord(char theChar)
		{
			return (theChar >= 'A' && theChar <= 'Z') || (theChar >= 'a' && theChar <= 'z') || (theChar >= '0' && theChar <= '9') || (theChar >= '¿' && theChar <= 'ˇ') || theChar == '_';
		}

		protected virtual void ProcessKey(KeyCode theKey, char theChar)
		{
			bool flag = this.mWidgetManager.mKeyDown[16];
			bool flag2 = this.mWidgetManager.mKeyDown[17];
			if (theKey == KeyCode.KEYCODE_SHIFT || theKey == KeyCode.KEYCODE_CONTROL || theKey == KeyCode.KEYCODE_COMMAND)
			{
				return;
			}
			bool flag3 = false;
			bool flag4 = !flag;
			if (flag && this.mHilitePos == -1)
			{
				this.mHilitePos = this.mCursorPos;
			}
			string text = this.mString;
			int num = this.mCursorPos;
			int num2 = this.mHilitePos;
			int num3 = this.mLeftPos;
			if (theChar == '\u0003' || theChar == '\u0018')
			{
				if (this.mHilitePos != -1 && this.mHilitePos != this.mCursorPos)
				{
					if (this.mCursorPos < this.mHilitePos)
					{
						this.mWidgetManager.mApp.CopyToClipboard(this.GetDisplayString().Substring(this.mCursorPos, this.mHilitePos));
					}
					else
					{
						this.mWidgetManager.mApp.CopyToClipboard(this.GetDisplayString().Substring(this.mHilitePos, this.mCursorPos));
					}
					if (theChar == '\u0003')
					{
						flag4 = false;
					}
					else
					{
						this.mString = this.mString.Substring(0, this.MIN(this.mCursorPos, this.mHilitePos)) + this.mString.Substring(this.MAX(this.mCursorPos, this.mHilitePos));
						this.mCursorPos = this.MIN(this.mCursorPos, this.mHilitePos);
						this.mHilitePos = -1;
						flag3 = true;
					}
				}
			}
			else if (theChar == '\u0016')
			{
				string clipboard = this.mWidgetManager.mApp.GetClipboard();
				if (clipboard.Length > 0)
				{
					string text2 = "";
					int num4 = 0;
					while (num4 < clipboard.Length && clipboard.ToString().get_Chars(num4) != '\r' && clipboard.ToString().get_Chars(num4) != '\n')
					{
						if (this.mFont.CharWidth(clipboard.ToString().get_Chars(num4)) != 0 && this.mEditListener.AllowChar(this.mId, clipboard.ToString().get_Chars(num4)))
						{
							text2 += char.ToString(clipboard.ToString().get_Chars(num4));
						}
						num4++;
					}
					if (this.mHilitePos == -1)
					{
						this.mString = this.mString.Substring(0, this.mCursorPos) + text2 + this.mString.Substring(this.mCursorPos);
					}
					else
					{
						this.mString = this.mString.Substring(0, this.MIN(this.mCursorPos, this.mHilitePos)) + text2 + this.mString.Substring(this.MAX(this.mCursorPos, this.mHilitePos));
						this.mCursorPos = this.MIN(this.mCursorPos, this.mHilitePos);
						this.mHilitePos = -1;
					}
					this.mCursorPos += text2.Length;
					flag3 = true;
				}
			}
			else if (theChar == '\u001a')
			{
				this.mLastModifyIdx = -1;
				string text3 = this.mString;
				int num5 = this.mCursorPos;
				int num6 = this.mHilitePos;
				this.mString = this.mUndoString;
				this.mCursorPos = this.mUndoCursor;
				this.mHilitePos = this.mUndoHilitePos;
				this.mUndoString = text3;
				this.mUndoCursor = num5;
				this.mUndoHilitePos = num6;
				flag4 = false;
			}
			else if (theKey == KeyCode.KEYCODE_LEFT)
			{
				if (flag2)
				{
					while (this.mCursorPos > 0)
					{
						if (this.IsPartOfWord(this.mString.get_Chars(this.mCursorPos - 1)))
						{
							break;
						}
						this.mCursorPos--;
					}
					while (this.mCursorPos > 0)
					{
						if (!this.IsPartOfWord(this.mString.get_Chars(this.mCursorPos - 1)))
						{
							break;
						}
						this.mCursorPos--;
					}
				}
				else if (flag || this.mHilitePos == -1)
				{
					this.mCursorPos--;
				}
				else
				{
					this.mCursorPos = this.MIN(this.mCursorPos, this.mHilitePos);
				}
			}
			else if (theKey == KeyCode.KEYCODE_RIGHT)
			{
				if (flag2)
				{
					while (this.mCursorPos < this.mString.Length - 1)
					{
						if (!this.IsPartOfWord(this.mString.get_Chars(this.mCursorPos + 1)))
						{
							break;
						}
						this.mCursorPos++;
					}
					while (this.mCursorPos < this.mString.Length - 1 && !this.IsPartOfWord(this.mString.get_Chars(this.mCursorPos + 1)))
					{
						this.mCursorPos++;
					}
				}
				if (flag || this.mHilitePos == -1)
				{
					this.mCursorPos++;
				}
				else
				{
					this.mCursorPos = this.MAX(this.mCursorPos, this.mHilitePos);
				}
			}
			else if (theKey == KeyCode.KEYCODE_BACK)
			{
				if (this.mString.Length > 0)
				{
					if (this.mHilitePos != -1 && this.mHilitePos != this.mCursorPos)
					{
						this.mString = this.mString.Substring(0, this.MIN(this.mCursorPos, this.mHilitePos)) + this.mString.Substring(this.MAX(this.mCursorPos, this.mHilitePos));
						this.mCursorPos = this.MIN(this.mCursorPos, this.mHilitePos);
						this.mHilitePos = -1;
						flag3 = true;
					}
					else
					{
						if (this.mCursorPos > 0)
						{
							this.mString = this.mString.Substring(0, this.mCursorPos - 1) + this.mString.Substring(this.mCursorPos);
						}
						else
						{
							this.mString = this.mString.Substring(this.mCursorPos);
						}
						this.mCursorPos--;
						this.mHilitePos = -1;
						if (this.mCursorPos != this.mLastModifyIdx)
						{
							flag3 = true;
						}
						this.mLastModifyIdx = this.mCursorPos - 1;
					}
				}
			}
			else if (theKey == KeyCode.KEYCODE_DELETE)
			{
				if (this.mString.Length > 0)
				{
					if (this.mHilitePos != -1 && this.mHilitePos != this.mCursorPos)
					{
						this.mString = this.mString.Substring(0, this.MIN(this.mCursorPos, this.mHilitePos)) + this.mString.Substring(this.MAX(this.mCursorPos, this.mHilitePos));
						this.mCursorPos = this.MIN(this.mCursorPos, this.mHilitePos);
						this.mHilitePos = -1;
						flag3 = true;
					}
					else
					{
						if (this.mCursorPos < this.mString.Length)
						{
							this.mString = this.mString.Substring(0, this.mCursorPos) + this.mString.Substring(this.mCursorPos + 1);
						}
						if (this.mCursorPos != this.mLastModifyIdx)
						{
							flag3 = true;
						}
						this.mLastModifyIdx = this.mCursorPos;
					}
				}
			}
			else if (theKey == KeyCode.KEYCODE_HOME)
			{
				this.mCursorPos = 0;
			}
			else if (theKey == KeyCode.KEYCODE_END)
			{
				this.mCursorPos = this.mString.Length;
			}
			else if (theKey == KeyCode.KEYCODE_RETURN)
			{
				this.mEditListener.EditWidgetText(this.mId, this.mString);
			}
			else
			{
				string theString = theChar.ToString();
				uint num7 = (uint)theChar;
				uint num8 = 127U;
				if (GlobalMembers.gSexyAppBase.mbAllowExtendedChars)
				{
					num8 = 255U;
				}
				if (num7 >= 32U && num7 <= num8 && this.mFont.StringWidth(theString) > 0 && this.mEditListener.AllowChar(this.mId, theChar))
				{
					if (this.mHilitePos != -1 && this.mHilitePos != this.mCursorPos)
					{
						this.mString = this.mString.Substring(0, this.MIN(this.mCursorPos, this.mHilitePos)) + theChar.ToString() + this.mString.Substring(this.MAX(this.mCursorPos, this.mHilitePos));
						this.mCursorPos = this.MIN(this.mCursorPos, this.mHilitePos);
						this.mHilitePos = -1;
						flag3 = true;
					}
					else
					{
						this.mString = this.mString.Substring(0, this.mCursorPos) + theChar.ToString() + this.mString.Substring(this.mCursorPos);
						if (this.mCursorPos != this.mLastModifyIdx + 1)
						{
							flag3 = true;
						}
						this.mLastModifyIdx = this.mCursorPos;
						this.mHilitePos = -1;
					}
					this.mCursorPos++;
					this.FocusCursor(false);
				}
				else
				{
					flag4 = false;
				}
			}
			if (this.mMaxChars != -1 && this.mString.Length > this.mMaxChars)
			{
				this.mString = this.mString.Substring(0, this.mMaxChars);
			}
			this.EnforceMaxPixels();
			if (this.mCursorPos < 0)
			{
				this.mCursorPos = 0;
			}
			else if (this.mCursorPos > this.mString.Length)
			{
				this.mCursorPos = this.mString.Length;
			}
			if (num != this.mCursorPos)
			{
				this.mBlinkAcc = 0;
				this.mShowingCursor = true;
			}
			this.FocusCursor(true);
			if (flag4 || this.mHilitePos == this.mCursorPos)
			{
				this.mHilitePos = -1;
			}
			if (!this.mEditListener.AllowText(this.mId, this.mString))
			{
				this.mString = text;
				this.mCursorPos = num;
				this.mHilitePos = num2;
				this.mLeftPos = num3;
			}
			else if (flag3)
			{
				this.mUndoString = text;
				this.mUndoCursor = num;
				this.mUndoHilitePos = num2;
			}
			this.MarkDirty();
		}

		public override void KeyDown(KeyCode theKey)
		{
			if ((theKey < (KeyCode)65 || theKey >= KeyCode.KEYCODE_ASCIIEND) && this.mEditListener.AllowKey(this.mId, theKey))
			{
				this.ProcessKey(theKey, '\0');
			}
			base.KeyDown(theKey);
		}

		public override void KeyChar(char theChar)
		{
			this.ProcessKey(KeyCode.KEYCODE_UNKNOWN, theChar);
			base.KeyChar(theChar);
		}

		public virtual int GetCharAt(int x, int y)
		{
			int result = 0;
			string displayString = this.GetDisplayString();
			for (int i = this.mLeftPos; i < displayString.Length; i++)
			{
				string theString = displayString.Substring(this.mLeftPos, i - this.mLeftPos);
				string theString2 = displayString.Substring(this.mLeftPos, i - this.mLeftPos + 1);
				int num = this.mFont.StringWidth(theString);
				int num2 = this.mFont.StringWidth(theString2);
				if (x >= (num + num2) / 2 + 5)
				{
					result = i + 1;
				}
			}
			return result;
		}

		public virtual void FocusCursor(bool bigJump)
		{
			while (this.mCursorPos < this.mLeftPos)
			{
				if (bigJump)
				{
					this.mLeftPos = this.MAX(0, this.mLeftPos - 10);
				}
				else
				{
					this.mLeftPos = this.MAX(0, this.mLeftPos - 1);
				}
				this.MarkDirty();
			}
			if (this.mFont != null)
			{
				string displayString = this.GetDisplayString();
				while (this.mWidth - 8 > 0 && this.mFont.StringWidth(displayString.Substring(0, this.mCursorPos)) - this.mFont.StringWidth(displayString.Substring(0, this.mLeftPos)) >= this.mWidth - 8)
				{
					if (bigJump)
					{
						this.mLeftPos = this.MIN(this.mLeftPos + 10, this.mString.Length - 1);
					}
					else
					{
						this.mLeftPos = this.MIN(this.mLeftPos + 1, this.mString.Length - 1);
					}
					this.MarkDirty();
				}
			}
		}

		public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
			base.MouseDown(x, y, theBtnNum, theClickCount);
			this.mWidgetManager.mApp.ShowKeyboard();
			this.mHilitePos = -1;
			this.mCursorPos = this.GetCharAt(x, y);
			if (theClickCount > 1)
			{
				this.mHadDoubleClick = true;
				this.HiliteWord();
			}
			this.MarkDirty();
			this.FocusCursor(false);
		}

		public override void MouseUp(int x, int y, int theBtnNum, int theClickCount)
		{
			base.MouseUp(x, y, theBtnNum, theClickCount);
			if (this.mHilitePos == this.mCursorPos)
			{
				this.mHilitePos = -1;
			}
			if (this.mHadDoubleClick)
			{
				this.mHilitePos = -1;
				this.mCursorPos = this.GetCharAt(x, y);
				this.mHadDoubleClick = false;
				this.HiliteWord();
			}
			this.MarkDirty();
		}

		protected virtual void HiliteWord()
		{
			string displayString = this.GetDisplayString();
			if (this.mCursorPos < displayString.Length)
			{
				this.mHilitePos = this.mCursorPos;
				while (this.mHilitePos > 0)
				{
					if (!this.IsPartOfWord(displayString.get_Chars(this.mHilitePos - 1)))
					{
						break;
					}
					this.mHilitePos--;
				}
				while (this.mCursorPos < displayString.Length - 1 && this.IsPartOfWord(displayString.get_Chars(this.mCursorPos + 1)))
				{
					this.mCursorPos++;
				}
				if (this.mCursorPos < displayString.Length)
				{
					this.mCursorPos++;
				}
			}
		}

		public override void MouseDrag(int x, int y)
		{
			base.MouseDrag(x, y);
			if (this.mHilitePos == -1)
			{
				this.mHilitePos = this.mCursorPos;
			}
			this.mCursorPos = this.GetCharAt(x, y);
			this.MarkDirty();
			this.FocusCursor(false);
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_TEXT);
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.mWidgetManager.mApp.SetCursor(ECURSOR.CURSOR_POINTER);
		}

		public override void MarkDirty()
		{
			if (this.mColors[0].mAlpha != 255)
			{
				base.MarkDirtyFull();
				return;
			}
			base.MarkDirty();
		}

		public int mId;

		public string mString;

		public string mPasswordDisplayString = "";

		public Font mFont;

		public int mClipInset;

		public int mTextInset;

		public int mCursorOffset;

		public int mHiliteWidthAdd;

		public List<EditWidget.WidthCheck> mWidthCheckList = new List<EditWidget.WidthCheck>();

		internal static int[,] gEditWidgetColors = new int[,]
		{
			{ 255, 255, 255 },
			{ 0, 0, 0 },
			{ 0, 0, 0 },
			{ 0, 0, 0 },
			{ 255, 255, 255 }
		};

		public EditListener mEditListener;

		public bool mShowingCursor;

		public bool mDrawSelOverride;

		public bool mHadDoubleClick;

		public int mCursorPos;

		public int mHilitePos;

		public int mBlinkAcc;

		public int mBlinkDelay;

		public int mLeftPos;

		public int mMaxChars;

		public int mMaxPixels;

		public string mPasswordChar;

		public string mUndoString;

		public int mUndoCursor;

		public int mUndoHilitePos;

		public int mLastModifyIdx;

		public enum COLOR
		{
			COLOR_BKG,
			COLOR_OUTLINE,
			COLOR_TEXT,
			COLOR_HILITE,
			COLOR_HILITE_TEXT,
			NUM_COLORS
		}

		public struct WidthCheck
		{
			public Font mFont;

			public int mWidth;
		}
	}
}
