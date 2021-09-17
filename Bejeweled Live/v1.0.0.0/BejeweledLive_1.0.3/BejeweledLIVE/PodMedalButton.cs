using System;
using Sexy;

namespace BejeweledLIVE
{
	public class PodMedalButton : PodRoundButton
	{
		public PodMedalButton(int id, Image medalImage, ButtonListener listener)
			: base(id, medalImage, AtlasResources.IMAGE_PAUSE_BUTTON_RING, listener)
		{
		}
	}
}
