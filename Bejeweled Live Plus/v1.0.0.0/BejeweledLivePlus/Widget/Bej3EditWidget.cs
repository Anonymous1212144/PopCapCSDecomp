using System;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3EditWidget : EditWidget
	{
		public Bej3EditWidget(int theId, Bej3EditListener theEditListener)
			: base(theId, theEditListener)
		{
			this.SetFont(GlobalMembersResources.FONT_DIALOG);
			this.SetColor(0, new Color(0, 0, 0, 0));
			this.SetColor(1, new Color(0, 0, 0, 0));
			this.SetColor(2, Color.White);
			this.SetColor(3, Color.White);
			this.SetColor(4, new Color(100, 100, 100));
			this.mCursorOffset = (int)ModVal.M(-5f);
			this.SetText("");
			this.mTextInset = ConstantsWP.EDITWIDGET_BOX_PADDING_X;
			this.mClipInset = ConstantsWP.EDITWIDGET_CURSOR_OFFSET;
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, ConstantsWP.EDITWIDGET_HEIGHT);
		}

		public override void Draw(Graphics g)
		{
			g.mClipRect.mX = g.mClipRect.mX - ConstantsWP.EDITWIDGET_BACKGROUND_OFFSET;
			g.mClipRect.mWidth = g.mClipRect.mWidth + ConstantsWP.EDITWIDGET_BACKGROUND_OFFSET * 2;
			g.DrawImageBox(new Rect(-ConstantsWP.EDITWIDGET_BACKGROUND_OFFSET, 0, this.mWidth + ConstantsWP.EDITWIDGET_BACKGROUND_OFFSET * 2, this.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_TEXTBOX);
			Utils.SetFontLayerColor((ImageFont)this.mFont, 0, Bej3Widget.COLOR_DIALOG_WHITE);
			base.Draw(g);
		}

		public override void MouseDown(int x, int y, int theBtnNum, int theClickCount)
		{
			this.SetFocus(this);
			this.GotFocus();
			base.MouseDown(x, y, theBtnNum, theClickCount);
		}

		public override void KeyDown(KeyCode theKey)
		{
			base.KeyDown(theKey);
		}

		public override void GotFocus()
		{
			base.GotFocus();
			Bej3EditListener bej3EditListener = (Bej3EditListener)this.mEditListener;
			bej3EditListener.EditWidgetGotFocus(this.mId);
		}

		public override void LostFocus()
		{
			base.LostFocus();
			Bej3EditListener bej3EditListener = (Bej3EditListener)this.mEditListener;
			bej3EditListener.EditWidgetLostFocus(this.mId);
		}
	}
}
