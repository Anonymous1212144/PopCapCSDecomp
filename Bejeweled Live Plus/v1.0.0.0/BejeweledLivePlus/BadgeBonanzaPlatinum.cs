using System;

namespace BejeweledLivePlus
{
	public class BadgeBonanzaPlatinum : Badge
	{
		public BadgeBonanzaPlatinum()
		{
			this.mIdx = 13;
			this.mGPoints = 10;
			this.mUnlockRequirement = 10;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BUTTERFLY_BONANZA;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BTF_BONANZA;
			this.mLevel = BadgeLevel.BADGELEVEL_PLATINUM;
			this.mName = "Bonanza_Platinum";
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
			return GlobalMembers._ID("Bonanza Platinum", 4025);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Collect 10 butterflies in one move in Butterfly", 4061);
		}
	}
}
