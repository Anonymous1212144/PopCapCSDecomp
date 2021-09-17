using System;
using Sexy;

namespace BejeweledLIVE
{
	public class FancyBigButton : FancyPodButton
	{
		public FancyBigButton(int id, int numButtons, int buttonNum, string label, ButtonListener listener)
			: base(id, numButtons, buttonNum, label, listener)
		{
			this.mRingImage = AtlasResources.IMAGE_PILL_RING;
			this.mButtonImage = AtlasResources.IMAGE_PILL_CENTRE1;
			this.mOverlayImage = AtlasResources.IMAGE_PILL_CENTRE2;
			this.mAdditiveImage = AtlasResources.IMAGE_PILL_ADDITIVE;
			this.SetFont(Resources.FONT_BUTTON);
			this.mNormalRect = this.mButtonImage.GetCelRect(0);
			this.mWidth = base.DefaultWidth();
			this.mHeight = base.DefaultHeight();
			this.ComputeDrawFrames();
		}
	}
}
