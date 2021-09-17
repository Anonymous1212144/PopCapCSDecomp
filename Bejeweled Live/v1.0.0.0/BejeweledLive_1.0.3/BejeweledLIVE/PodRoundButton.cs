using System;
using Sexy;

namespace BejeweledLIVE
{
	public class PodRoundButton : PodButton
	{
		private void init(Image buttonImage, Image ringImage)
		{
			this.mButtonImage = buttonImage;
			this.mRingImage = ringImage;
			this.mNormalRect = this.mButtonImage.GetCelRect(0);
			this.mWidth = base.DefaultWidth();
			this.mHeight = base.DefaultHeight();
			this.ComputeDrawFrames();
		}

		public PodRoundButton(int id, Image buttonImage, ButtonListener listener)
			: base(id, listener)
		{
			this.init(buttonImage, AtlasResources.IMAGE_PAUSE_BUTTON_RING);
		}

		public PodRoundButton(int id, Image buttonImage, Image ringImage, ButtonListener listener)
			: base(id, listener)
		{
			this.init(buttonImage, ringImage);
		}

		public new void DrawPill(Graphics g)
		{
			g.DrawImage(this.mButtonImage, this.mPillFrame.mX, this.mPillFrame.mY);
		}

		public new void DrawRing(Graphics g)
		{
			g.DrawImage(this.mRingImage, this.mRingFrame.mX, this.mRingFrame.mY);
		}
	}
}
