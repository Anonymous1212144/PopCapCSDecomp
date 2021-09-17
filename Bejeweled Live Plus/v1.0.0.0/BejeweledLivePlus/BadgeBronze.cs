using System;

namespace BejeweledLivePlus
{
	public class BadgeBronze : Badge
	{
		public BadgeBronze()
		{
			this.mIdx = 2;
			this.mGPoints = 5;
			this.mUnlockRequirement = 100000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_DIAMOND_MINE;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_DIAMONDMINE;
			this.mLevel = BadgeLevel.BADGELEVEL_BRONZE;
			this.mName = "Bronze";
		}

		public override int GetStat()
		{
			int result = 0;
			if (this.mBoard is DigBoard)
			{
				result = ((DigBoard)this.mBoard).mLevelPointsTotal;
			}
			return result;
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Mine Bronze", 4014);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score 100 000 points in Diamond Mine mode", 4054);
		}
	}
}
