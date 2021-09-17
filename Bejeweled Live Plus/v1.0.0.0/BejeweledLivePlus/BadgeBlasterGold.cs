using System;

namespace BejeweledLivePlus
{
	public class BadgeBlasterGold : Badge
	{
		public BadgeBlasterGold()
		{
			this.mIdx = 16;
			this.mGPoints = 15;
			this.mUnlockRequirement = 60;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BLASTER;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BLASTER;
			this.mLevel = BadgeLevel.BADGELEVEL_GOLD;
			this.mName = "Blaster_Gold";
		}

		public override int GetStat()
		{
			return GlobalMembers.gApp.mProfile.mStats.MaxStat(33);
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Blaster", 4028);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Destroy 60 gems in 1 move", 4064);
		}
	}
}
