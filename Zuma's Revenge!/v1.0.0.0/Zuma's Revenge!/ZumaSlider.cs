using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class ZumaSlider : Slider
	{
		public string Label
		{
			get
			{
				return this.mLabel;
			}
			set
			{
				this.mLabel = value;
				Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_BASE);
				this.mLabelWidth = fontByID.StringWidth(this.mLabel);
			}
		}

		public ZumaSlider(int id, SliderListener listener, string label)
			: base(Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_THUMB), Res.GetImageByID(ResID.IMAGE_GUI_DIALOG_BOX_MAINMENU_SLIDER), id, listener)
		{
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_BASE);
			this.mFeedbackSoundID = -1;
			this.mLabel = label;
			this.mLabelWidth = fontByID.StringWidth(this.mLabel);
			this.mHasAlpha = (this.mHasTransparencies = true);
		}

		public override void Draw(Graphics g)
		{
			Font fontByID = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_BASE);
			g.PushState();
			g.ClearClipRect();
			g.SetFont(fontByID);
			g.SetColor(255, 255, 64, 255);
			int num = Common._S(Common._M(20));
			int num2 = Common._S(Common._M(-35));
			g.DrawString(this.mLabel, (this.mWidth + num - this.mLabelWidth) / 2, g.mFont.mAscent + this.mHeight + num2 - Common._S(Common._M(12)) - g.mFont.mHeight);
			g.PopState();
			base.Draw(g);
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			this.MarkDirty();
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			this.MarkDirty();
		}

		public override void MouseUp(int x, int y)
		{
			base.MouseUp(x, y);
			if (this.mFeedbackSoundID >= 0)
			{
				GameApp.gApp.PlaySample(this.mFeedbackSoundID);
			}
		}

		public string mLabel;

		public int mLabelWidth;

		public int mFeedbackSoundID;
	}
}
