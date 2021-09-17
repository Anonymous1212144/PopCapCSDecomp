using System;
using System.Collections.Generic;
using System.Linq;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class NewUserDialog : Bej3Dialog, Bej3EditListener, EditListener
	{
		public virtual void EditWidgetGotFocus(int id)
		{
		}

		public virtual void EditWidgetLostFocus(int id)
		{
		}

		public virtual bool AllowKey(int id, KeyCode key)
		{
			return true;
		}

		public virtual void EditWidgetText(int theId, string theString)
		{
			this.ButtonDepress(1000);
		}

		protected override void SlideInFinished()
		{
			this.mWidgetManager.SetFocus(this.mNameWidget);
			base.SlideInFinished();
		}

		public NewUserDialog(bool isRename)
			: base(isRename ? 2 : 1, true, isRename ? GlobalMembers._ID("EDIT NAME", 3414) : GlobalMembers._ID("WELCOME!", 303), "", "", 2, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED)
		{
			bool flag = true;
			this.mTextBoxOffset = ConstantsWP.WELCOME_DIALOG_TEXTBOX_Y_OFFSET;
			this.mNameWidget = new Bej3EditWidget(1, this);
			this.mNameWidget.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mNameWidget.SetColor(0, new Color(0, 0, 0, 0));
			this.mNameWidget.SetColor(1, new Color(0, 0, 0, 0));
			this.mNameWidget.SetColor(2, Color.White);
			this.mNameWidget.SetColor(3, Color.White);
			this.mNameWidget.SetColor(4, Color.Black);
			this.mNameWidget.mMaxChars = 16;
			this.mNameWidget.mCursorOffset = GlobalMembers.M(-5);
			this.mNameWidget.SetText("");
			this.AddWidget(this.mNameWidget);
			this.mHasForcedUppercase = false;
			this.mNameWidget.mVisible = true;
			this.mMessageLabel.SetText(GlobalMembers._ID("Enter your name:", 305));
			this.mMessageLabel.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			if (flag)
			{
				this.mButtons.Clear();
				Bej3Button bej3Button = new Bej3Button(this.mYesButton.mId, this, Bej3ButtonType.BUTTON_TYPE_LONG);
				bej3Button.SetLabel(this.mYesButton.mLabel);
				GlobalMembers.KILL_WIDGET(this.mYesButton);
				this.mYesButton = bej3Button;
				this.mButtons.Add(this.mYesButton);
				this.AddWidget(this.mYesButton);
				bej3Button = new Bej3Button(this.mNoButton.mId, this, Bej3ButtonType.BUTTON_TYPE_LONG);
				bej3Button.SetLabel(this.mNoButton.mLabel);
				GlobalMembers.KILL_WIDGET(this.mNoButton);
				this.mNoButton = bej3Button;
				this.mButtons.Add(this.mNoButton);
				this.AddWidget(this.mNoButton);
			}
			this.LinkUpAssets();
		}

		public NewUserDialog(bool isRename, bool doButtons)
			: base(isRename ? 2 : 1, true, isRename ? GlobalMembers._ID("EDIT NAME", 3414) : GlobalMembers._ID("WELCOME!", 303), "", "", doButtons ? 2 : 0, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED)
		{
			this.mTextBoxOffset = ConstantsWP.WELCOME_DIALOG_TEXTBOX_Y_OFFSET;
			this.mNameWidget = new Bej3EditWidget(1, this);
			this.mNameWidget.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.mNameWidget.SetColor(0, new Color(0, 0, 0, 0));
			this.mNameWidget.SetColor(1, new Color(0, 0, 0, 0));
			this.mNameWidget.SetColor(2, Color.White);
			this.mNameWidget.SetColor(3, Color.White);
			this.mNameWidget.SetColor(4, Color.Black);
			this.mNameWidget.mMaxChars = 16;
			this.mNameWidget.mCursorOffset = GlobalMembers.M(-5);
			this.mNameWidget.SetText("");
			this.AddWidget(this.mNameWidget);
			this.mHasForcedUppercase = false;
			this.mNameWidget.mVisible = true;
			this.mMessageLabel.SetText(GlobalMembers._ID("Enter your name:", 305));
			this.mMessageLabel.SetFont(GlobalMembersResources.FONT_SUBHEADER);
			if (doButtons)
			{
				this.mButtons.Clear();
				Bej3Button bej3Button = new Bej3Button(this.mYesButton.mId, this, Bej3ButtonType.BUTTON_TYPE_LONG);
				bej3Button.SetLabel(this.mYesButton.mLabel);
				GlobalMembers.KILL_WIDGET(this.mYesButton);
				this.mYesButton = bej3Button;
				this.mButtons.Add(this.mYesButton);
				this.AddWidget(this.mYesButton);
				bej3Button = new Bej3Button(this.mNoButton.mId, this, Bej3ButtonType.BUTTON_TYPE_LONG);
				bej3Button.SetLabel(this.mNoButton.mLabel);
				GlobalMembers.KILL_WIDGET(this.mNoButton);
				this.mNoButton = bej3Button;
				this.mButtons.Add(this.mNoButton);
				this.AddWidget(this.mNoButton);
			}
			this.LinkUpAssets();
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			theWidth = ConstantsWP.NEWUSERDIALOG_WIDTH;
			base.Resize(theX, theY, theWidth, theHeight);
			this.mY = ((this.mId == 2) ? ConstantsWP.MENU_Y_POS_HIDDEN : GlobalMembers.gApp.mHeight);
			this.mTargetPos = ConstantsWP.NEWUSERDIALOG_Y;
			this.mNameWidget.Resize((this.mWidth - ConstantsWP.NEWUSERDIALOG_TEXTBOX_WIDTH) / 2, this.mHeight - this.mTextBoxOffset, ConstantsWP.NEWUSERDIALOG_TEXTBOX_WIDTH, ConstantsWP.EDITWIDGET_HEIGHT);
			this.mHeadingLabel.mY = ConstantsWP.NEWUSERDIALOG_HEADING_Y;
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDialogBox(g, this.mWidth);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.NEWUSERDIALOG_DIVIDER_Y, false);
		}

		public virtual bool AllowChar(int theId, char theChar)
		{
			if (this.mNameWidget.mFont.CharWidth(theChar) == 0)
			{
				return false;
			}
			List<char> list = new List<char>();
			for (int i = 97; i <= 122; i++)
			{
				list.Add((char)i);
				list.Add((char)(i - 32));
			}
			for (int j = 48; j <= 57; j++)
			{
				list.Add((char)j);
			}
			list.Add(' ');
			for (int k = 0; k < list.Count; k++)
			{
				if (list[k] == theChar)
				{
					return true;
				}
			}
			return false;
		}

		public virtual bool AllowText(int theId, string theText)
		{
			if (theText.Length == 1 && !this.mHasForcedUppercase)
			{
				this.mNameWidget.mString = this.mNameWidget.mString;
				Enumerable.ToArray<char>(this.mNameWidget.mString)[0].ToString().ToUpper();
				this.mHasForcedUppercase = true;
			}
			return true;
		}

		public override int GetPreferredHeight(int theWidth)
		{
			return ConstantsWP.NEWUSERDIALOG_HEIGHT;
		}

		public string GetName()
		{
			return this.mNameWidget.mString;
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mMessageLabel.SetTextBlock(new Rect(ConstantsWP.DIALOGBOX_MESSAGE_PADDING_X, ConstantsWP.WELCOME_DIALOG_MESSAGE_Y, this.mWidth - ConstantsWP.DIALOGBOX_MESSAGE_PADDING_X * 2, this.mHeight - ConstantsWP.WELCOME_DIALOG_MESSAGE_Y), false);
		}

		public override void Kill()
		{
			base.Kill();
			this.mNameWidget.LostFocus();
		}

		protected int mTextBoxOffset;

		public Bej3EditWidget mNameWidget;

		public string mOrigString;

		public bool mHasForcedUppercase;
	}
}
