using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	internal class TextContainer : Widget, ScrollWidgetListener
	{
		public TextContainer(string theText, int textWidth)
		{
			this.mText = new Label(GlobalMembersResources.FONT_DIALOG);
			this.mText.SetText(theText);
			this.mText.mLineSpacingOffset = ConstantsWP.HINTDIALOG_TEXT_LINE_SPACING_ADJUST;
			Graphics graphics = new Graphics();
			graphics.SetFont(this.mText.GetFont());
			int num = 0;
			int num2 = 0;
			int wordWrappedHeight = graphics.GetWordWrappedHeight(textWidth, this.mText.GetText(), this.mText.GetFont().GetLineSpacing() - ConstantsWP.HINTDIALOG_TEXT_LINE_SPACING_ADJUST, ref num, ref num2);
			this.mText.SetTextBlock(new Rect(0, 0, textWidth, wordWrappedHeight), true);
			this.mText.SetTextBlockAlignment(0);
			this.mText.SetTextBlockEnabled(true);
			this.AddWidget(this.mText);
			this.Resize(this.mText.GetRect());
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public Label mText;
	}
}
