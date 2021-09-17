using System;

namespace BejeweledLivePlus
{
	public class BadgeHighvolatageBronze : Badge
	{
		public BadgeHighvolatageBronze()
		{
			this.mIdx = 8;
			this.mGPoints = 5;
			this.mUnlockRequirement = 100000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_HIGH_VOLTAGE;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_HIGHVOLTAGE;
			this.mLevel = BadgeLevel.BADGELEVEL_BRONZE;
			this.mName = "High_volatage_Bronze";
		}

		public override int GetStat()
		{
			int result = 0;
			if (this.mBoard is SpeedBoard)
			{
				result = ((SpeedBoard)this.mBoard).mPoints;
			}
			return result;
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("High voltage Bronze", 4020);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score 100 000 points in Lightning mode", 4070);
		}
	}
}
