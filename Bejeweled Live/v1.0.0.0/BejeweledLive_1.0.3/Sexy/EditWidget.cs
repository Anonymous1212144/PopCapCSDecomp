using System;
using Microsoft.Xna.Framework.GamerServices;

namespace Sexy
{
	public class EditWidget : Widget
	{
		protected string GetDisplayString()
		{
			if (this.mPasswordChar == " ")
			{
				return this.mString;
			}
			if (this.mPasswordDisplayString.Length != this.mString.Length)
			{
				this.mPasswordDisplayString = this.mPasswordDisplayString + this.mString.Length.ToString() + this.mPasswordChar;
			}
			return this.mPasswordDisplayString;
		}

		public virtual void SetFont(Font theFont)
		{
			this.mFont.Dispose();
			this.mFont = theFont.Duplicate();
		}

		public virtual void SetText(string theText)
		{
			this.mString = theText;
			this.MarkDirty();
		}

		public override void Resize(TRect frame)
		{
			base.Resize(frame);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.RehupBounds();
		}

		public override void Draw(Graphics g)
		{
			if (this.mEditing)
			{
				return;
			}
			string displayString = this.GetDisplayString();
			g.SetColor(this.mColors[0]);
			g.FillRect(0, 0, this.mWidth, this.mHeight);
			Graphics g2 = GlobalStaticVars.g;
			g.Reset();
			g2.SetFont(this.mFont);
			g2.SetColor(this.mColors[2]);
			g2.DrawString(displayString, 4, (this.mHeight - this.mFont.GetHeight()) / 2 + this.mFont.GetAscent());
			g2.Dispose();
			g.SetColor(this.mColors[1]);
			g.DrawRect(0, 0, this.mWidth - 1, this.mHeight - 1);
		}

		public override void Update()
		{
			base.Update();
		}

		public override bool WantsFocus()
		{
			return true;
		}

		public override void GotFocus()
		{
			base.GotFocus();
			Guide.BeginShowKeyboardInput(0, "NEW HIGH SCORE", "Congratulations!\nYou earned a new high score!\nENTER YOUR NAME", this.mString, new AsyncCallback(this.KeyboardCallback), this);
		}

		public override void LostFocus()
		{
			base.LostFocus();
		}

		public virtual void RehupBounds()
		{
		}

		public virtual void EditingEnded(string theString)
		{
			this.mEditing = false;
			this.mString = theString;
			this.mEditListener.EditWidgetText(this.mId, ref this.mString);
		}

		public virtual bool ShouldChangeCharacters(int theRangeStart, int theRangeLength, string theReplacementChars)
		{
			return true;
		}

		public virtual bool ShouldClear()
		{
			bool flag = this.mEditListener.ShouldClear();
			if (flag)
			{
				this.mString = "";
			}
			return flag;
		}

		public EditWidget(int theId, EditListener theEditListener)
		{
			this.mId = theId;
			this.mEditListener = theEditListener;
			this.mFont = null;
			this.mMaxChars = -1;
			this.mPasswordChar = " ";
			this.mEditing = false;
			this.mAcceptsEmptyText = false;
			this.mFont = new Font();
			this.SetColors(EditWidget.gEditWidgetColors, 5);
		}

		public override void Dispose()
		{
			base.Dispose();
			this.mFont = null;
		}

		private void KeyboardCallback(IAsyncResult result)
		{
			string text = Guide.EndShowKeyboardInput(result);
			if (text == null)
			{
				this.LostFocus();
				return;
			}
			this.EditingEnded(text);
		}

		internal static int[,] gEditWidgetColors = new int[,]
		{
			{ 255, 255, 255 },
			{ 0, 0, 0 },
			{ 0, 0, 0 },
			{ 0, 0, 0 },
			{ 255, 255, 255 }
		};

		public int mId;

		public string mString;

		public string mPasswordDisplayString;

		public Font mFont;

		public EditListener mEditListener;

		public int mMaxChars;

		public string mPasswordChar;

		public bool mEditing;

		public bool mAcceptsEmptyText;

		public EditWidget.KEYBOARD_TYPE mKeyBoardType;

		public enum KEYBOARD_TYPE
		{
			KEYBOARD_TYPE_NUMBERS_AND_PUNCTUATION
		}

		public enum Colors
		{
			COLOR_BKG,
			COLOR_OUTLINE,
			COLOR_TEXT,
			COLOR_HILITE,
			COLOR_HILITE_TEXT,
			NUM_COLORS
		}
	}
}
