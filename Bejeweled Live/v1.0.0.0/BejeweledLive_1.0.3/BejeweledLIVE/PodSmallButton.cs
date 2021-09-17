using System;
using Sexy;

namespace BejeweledLIVE
{
	public class PodSmallButton : PodButton
	{
		public PodSmallButton(int id, int cel, string label, ButtonListener listener)
			: base(id, listener)
		{
			this.SetFont(Resources.FONT_BUTTON);
			this.mLabel = label;
			this.mButtonImage = AtlasResources.IMAGE_PILL_SMALL_CENTER1;
			this.mNormalRect = this.mButtonImage.GetCelRect(cel);
			this.mWidth = base.DefaultWidth();
			this.mHeight = base.DefaultHeight();
			this.ComputeDrawFrames();
		}
	}
}
