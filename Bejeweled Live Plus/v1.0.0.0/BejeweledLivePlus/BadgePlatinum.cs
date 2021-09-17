using System;

namespace BejeweledLivePlus
{
	public class BadgePlatinum : Badge
	{
		public BadgePlatinum()
		{
			this.mIdx = 3;
			this.mGPoints = 10;
			this.mUnlockRequirement = 750000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_DIAMOND_MINE;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_DIAMONDMINE;
			this.mLevel = BadgeLevel.BADGELEVEL_PLATINUM;
			this.mName = "Platinum";
		}

		public override int GetStat()
		{
			int result = 0;
			if (this.mBoard is DigBoard)
			{
				result = ((DigBoard)this.mBoard).mPoints;
			}
			return result;
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Mine Platinum", 4015);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score 750 000 points in Diamond Mine mode", 4055);
		}
	}
}
