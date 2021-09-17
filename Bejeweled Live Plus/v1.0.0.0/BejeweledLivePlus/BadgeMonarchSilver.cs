using System;

namespace BejeweledLivePlus
{
	public class BadgeMonarchSilver : Badge
	{
		public BadgeMonarchSilver()
		{
			this.mIdx = 10;
			this.mGPoints = 5;
			this.mUnlockRequirement = 300000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BUTTERFLY_MONARCH;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BTF_MONARCH;
			this.mLevel = BadgeLevel.BADGELEVEL_SILVER;
			this.mName = "Monarch_Silver";
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
			return GlobalMembers._ID("Monarch Bronze", 4022);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score 300 000 points in Butterflies", 4058);
		}
	}
}
