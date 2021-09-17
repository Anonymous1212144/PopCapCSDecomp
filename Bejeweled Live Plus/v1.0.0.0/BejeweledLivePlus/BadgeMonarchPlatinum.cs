using System;

namespace BejeweledLivePlus
{
	public class BadgeMonarchPlatinum : Badge
	{
		public BadgeMonarchPlatinum()
		{
			this.mIdx = 11;
			this.mGPoints = 10;
			this.mUnlockRequirement = 750000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BUTTERFLY_MONARCH;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BTF_MONARCH;
			this.mLevel = BadgeLevel.BADGELEVEL_PLATINUM;
			this.mName = "Monarch_Platinum";
		}

		public override int GetStat()
		{
			int result = 0;
			if (this.mBoard is ButterflyBoard)
			{
				result = ((ButterflyBoard)this.mBoard).mPoints;
			}
			return result;
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Monarch Platinum", 4023);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score 750 000 points in Butterflies", 4059);
		}
	}
}
