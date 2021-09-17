using System;

namespace BejeweledLivePlus
{
	public class BadgeHighvolatagePlatinum : Badge
	{
		public BadgeHighvolatagePlatinum()
		{
			this.mIdx = 9;
			this.mGPoints = 10;
			this.mUnlockRequirement = 750000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_HIGH_VOLTAGE;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_HIGHVOLTAGE;
			this.mLevel = BadgeLevel.BADGELEVEL_PLATINUM;
			this.mName = "High_voltage_Platinum";
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
			return GlobalMembers._ID("High voltage Platinum", 4021);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score 750 000 points in Lightning mode", 4071);
		}
	}
}
