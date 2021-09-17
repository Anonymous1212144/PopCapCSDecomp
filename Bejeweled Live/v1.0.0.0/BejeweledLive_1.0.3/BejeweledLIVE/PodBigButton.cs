using System;
using Sexy;

namespace BejeweledLIVE
{
	public class PodBigButton : PodButton
	{
		public PodBigButton(int id, BigButtonColors cel, string label, ButtonListener listener)
			: this(id, (int)cel, label, listener)
		{
		}

		public PodBigButton(int id, int cel, string label, ButtonListener listener)
			: base(id, listener)
		{
			this.SetFont(Resources.FONT_BUTTON);
			this.mLabel = label;
			this.mNormalRect = this.mButtonImage.GetCelRect(cel);
			this.mWidth = base.DefaultWidth();
			this.mHeight = base.DefaultHeight();
			this.ComputeDrawFrames();
		}
	}
}
