using System;

namespace BejeweledLivePlus
{
	public class Resources
	{
		public static void LinkUpFonts(int artRes)
		{
			if (artRes == 480)
			{
				GlobalMembersResources.FONT_DIALOG = GlobalMembersResourcesWP.FONT_LOWRES_DIALOG;
				GlobalMembersResources.FONT_HEADER = GlobalMembersResourcesWP.FONT_LOWRES_HEADER;
				GlobalMembersResources.FONT_SUBHEADER = GlobalMembersResourcesWP.FONT_LOWRES_SUBHEADER;
				GlobalMembersResources.FONT_INGAME = GlobalMembersResourcesWP.FONT_LOWRES_INGAME;
				GlobalMembersResources.FONT_FLOATERS = GlobalMembersResourcesWP.FONT_LOWRES_HEADER;
				GlobalMembersResources.FONT_CRYSTALBALL = GlobalMembersResourcesWP.FONT_LOWRES_HEADER;
				GlobalMembersResources.FONT_SCORE = GlobalMembersResourcesWP.FONT_LOWRES_INGAME;
				GlobalMembersResources.FONT_HUGE = GlobalMembersResourcesWP.FONT_LOWRES_HEADER;
				return;
			}
			if (artRes != 960)
			{
				return;
			}
			GlobalMembersResources.FONT_DIALOG = GlobalMembersResourcesWP.FONT_HIRES_DIALOG;
			GlobalMembersResources.FONT_HEADER = GlobalMembersResourcesWP.FONT_HIRES_HEADER;
			GlobalMembersResources.FONT_SUBHEADER = GlobalMembersResourcesWP.FONT_HIRES_SUBHEADER;
			GlobalMembersResources.FONT_INGAME = GlobalMembersResourcesWP.FONT_HIRES_INGAME;
			GlobalMembersResources.FONT_FLOATERS = GlobalMembersResourcesWP.FONT_HIRES_HEADER;
			GlobalMembersResources.FONT_CRYSTALBALL = GlobalMembersResourcesWP.FONT_HIRES_HEADER;
			GlobalMembersResources.FONT_SCORE = GlobalMembersResourcesWP.FONT_HIRES_INGAME;
			GlobalMembersResources.FONT_HUGE = GlobalMembersResourcesWP.FONT_HIRES_HEADER;
		}
	}
}
