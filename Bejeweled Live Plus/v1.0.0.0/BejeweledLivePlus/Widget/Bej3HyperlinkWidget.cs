using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Bej3HyperlinkWidget : HyperlinkWidget
	{
		public Bej3HyperlinkWidget(int theId, ButtonListener theButtonListener)
			: base(theId, theButtonListener)
		{
			this.mUnderlineHeight = 1;
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			for (int i = 0; i < this.mLayerColours.Count; i++)
			{
				Utils.SetFontLayerColor((ImageFont)this.mFont, i, this.mLayerColours[i]);
			}
			int theX = (this.mWidth - this.mFont.StringWidth(this.mLabel)) / 2;
			int num = (this.mHeight + this.mFont.GetAscent()) / 2 - 1;
			g.SetColor(this.mColor);
			g.SetFont(this.mFont);
			g.DrawString(this.mLabel, theX, num);
			for (int j = 0; j < this.mUnderlineSize; j++)
			{
				g.FillRect(theX, num + this.mUnderlineOffset + j, this.mFont.StringWidth(this.mLabel), this.mUnderlineHeight);
			}
		}

		public override void SetFont(Font theFont)
		{
			base.SetFont(theFont);
			this.mLayerColours.Clear();
			int layerCount = ((ImageFont)this.mFont).GetLayerCount();
			for (int i = 0; i < layerCount; i++)
			{
				this.mLayerColours.Add(Color.White);
			}
			this.mUnderlineHeight = 1;
			if (theFont == GlobalMembersResources.FONT_HUGE)
			{
				this.mLayerColours[0] = Bej3Widget.COLOR_HEADING_GLOW_1;
				return;
			}
			if (theFont == GlobalMembersResources.FONT_SUBHEADER)
			{
				this.mLayerColours[1] = Bej3Widget.COLOR_SUBHEADING_1_FILL;
				this.mLayerColours[0] = Bej3Widget.COLOR_SUBHEADING_1_STROKE;
				return;
			}
			if (theFont == GlobalMembersResources.FONT_DIALOG)
			{
				this.mLayerColours[0] = Bej3Widget.COLOR_DIALOG_1_FILL;
			}
		}

		public void SetLayerColor(int layer, Color colour)
		{
			this.mLayerColours[layer] = colour;
		}

		private List<Color> mLayerColours = new List<Color>();

		private int mUnderlineHeight;
	}
}
