using System;
using Sexy;

namespace BejeweledLIVE
{
	public class PodPauseButton : PodRoundButton
	{
		public PodPauseButton(int id, ButtonListener listener)
			: base(id, AtlasResources.IMAGE_PAUSE_BUTTON_PAUSE, listener)
		{
			this.mPaused = false;
		}

		public override void Draw(Graphics g)
		{
			this.mButtonImage = (this.mPaused ? AtlasResources.IMAGE_PAUSE_BUTTON_PLAY : AtlasResources.IMAGE_PAUSE_BUTTON_PAUSE);
			base.Draw(g);
		}

		public bool mPaused;
	}
}
