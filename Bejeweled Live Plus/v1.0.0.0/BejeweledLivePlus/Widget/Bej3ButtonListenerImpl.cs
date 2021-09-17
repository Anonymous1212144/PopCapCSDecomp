using System;

namespace BejeweledLivePlus.Widget
{
	public class Bej3ButtonListenerImpl
	{
		public void ButtonDepress(int theId)
		{
			GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BUTTON_RELEASE);
		}

		public bool ButtonsEnabled()
		{
			return true;
		}
	}
}
