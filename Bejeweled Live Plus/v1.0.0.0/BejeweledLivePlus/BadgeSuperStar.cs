using System;

namespace BejeweledLivePlus
{
	public class BadgeSuperStar : Badge
	{
		public BadgeSuperStar()
		{
			this.mIdx = 17;
			this.mGPoints = 10;
			this.mUnlockRequirement = 1;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_SUPERSTAR;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_SUPERSTAR;
			this.mLevel = BadgeLevel.BADGELEVEL_GOLD;
			this.mName = "Superstar";
		}

		public override int GetStat()
		{
			return GlobalMembers.gApp.mProfile.mStats.MaxStat(30);
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Superstar", 4029);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Line up 6 gems in a row", 4065);
		}
	}
}
