using System;

namespace BejeweledLivePlus
{
	public class BadgeRelicSilver : Badge
	{
		public BadgeRelicSilver()
		{
			this.mIdx = 4;
			this.mGPoints = 5;
			this.mUnlockRequirement = 8;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RELIC_HUNTER;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_RELICHUNTER;
			this.mLevel = BadgeLevel.BADGELEVEL_BRONZE;
			this.mName = "Relic_Silver";
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
			return GlobalMembers._ID("Relic Hunter Bronze", 4016);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Collect 8 artifacts in Diamond Mine mode", 4056);
		}
	}
}
