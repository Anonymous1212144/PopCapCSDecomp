using System;

namespace BejeweledLivePlus
{
	public class BadgeBonanzaBronze : Badge
	{
		public BadgeBonanzaBronze()
		{
			this.mIdx = 12;
			this.mGPoints = 5;
			this.mUnlockRequirement = 4;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BUTTERFLY_BONANZA;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BTF_BONANZA;
			this.mLevel = BadgeLevel.BADGELEVEL_BRONZE;
			this.mName = "Bonanza_bronze";
		}

		public override int GetStat()
		{
			return this.mBoard.mGameStats[29];
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Bonanza Bronze", 4024);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Collect 4 butterflies in one move in Butterfly", 4060);
		}
	}
}
