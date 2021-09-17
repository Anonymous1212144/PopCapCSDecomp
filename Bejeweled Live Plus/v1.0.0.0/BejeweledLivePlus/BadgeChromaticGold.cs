using System;

namespace BejeweledLivePlus
{
	public class BadgeChromaticGold : Badge
	{
		public BadgeChromaticGold()
		{
			this.mIdx = 14;
			this.mGPoints = 20;
			this.mUnlockRequirement = 400;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_CHROMATIC;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_CHROMATIC;
			this.mLevel = BadgeLevel.BADGELEVEL_GOLD;
			this.mName = "Chromatic_Gold";
		}

		public override int GetStat()
		{
			return GlobalMembers.gApp.mProfile.mStats.MaxStat(14);
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Chromatic", 4026);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Clear 400 hypercubes", 4062);
		}
	}
}
