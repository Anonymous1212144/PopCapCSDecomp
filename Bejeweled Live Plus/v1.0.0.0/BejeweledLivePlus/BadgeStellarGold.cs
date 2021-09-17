using System;

namespace BejeweledLivePlus
{
	public class BadgeStellarGold : Badge
	{
		public BadgeStellarGold()
		{
			this.mIdx = 15;
			this.mGPoints = 20;
			this.mUnlockRequirement = 400;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_STELLAR;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_STELLAR;
			this.mLevel = BadgeLevel.BADGELEVEL_GOLD;
			this.mName = "Stellar_Gold";
		}

		public override int GetStat()
		{
			return GlobalMembers.gApp.mProfile.mStats.MaxStat(13);
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Chromatic", 4027);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Clear 400 star gems", 4063);
		}
	}
}
