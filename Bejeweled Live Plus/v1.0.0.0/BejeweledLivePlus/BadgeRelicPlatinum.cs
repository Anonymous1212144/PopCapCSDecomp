using System;

namespace BejeweledLivePlus
{
	public class BadgeRelicPlatinum : Badge
	{
		public BadgeRelicPlatinum()
		{
			this.mIdx = 5;
			this.mGPoints = 10;
			this.mUnlockRequirement = 15;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RELIC_HUNTER;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_RELICHUNTER;
			this.mLevel = BadgeLevel.BADGELEVEL_PLATINUM;
			this.mName = "Relic_Platinum";
		}

		public override int GetStat()
		{
			return this.mBoard.mGameStats[36];
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Relic Hunter Platinum", 4017);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Collect 15 artifacts in Diamond Mine mode", 4057);
		}
	}
}
