using System;

namespace BejeweledLivePlus
{
	public class BadgeLevelord : Badge
	{
		public BadgeLevelord()
		{
			this.mIdx = 0;
			this.mGPoints = 10;
			this.mUnlockRequirement = 10;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_LEVELORD;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_LEVELORD;
			this.mLevel = BadgeLevel.BADGELEVEL_BRONZE;
			this.mName = "Levelord";
		}

		public override int GetStat()
		{
			int result = 0;
			if (this.mBoard is ClassicBoard)
			{
				result = ((ClassicBoard)this.mBoard).mLevel + 1;
			}
			return result;
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Levelord", 4012);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Reach level 10 in classic mode", 4052);
		}
	}
}
