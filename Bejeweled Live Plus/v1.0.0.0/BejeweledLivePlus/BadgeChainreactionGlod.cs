using System;

namespace BejeweledLivePlus
{
	public class BadgeChainreactionGlod : Badge
	{
		public BadgeChainreactionGlod()
		{
			this.mIdx = 18;
			this.mGPoints = 15;
			this.mUnlockRequirement = 7;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_CHAIN_REACTION;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_CHAINREACTION;
			this.mLevel = BadgeLevel.BADGELEVEL_GOLD;
			this.mName = "Chain_reaction_Gold";
		}

		public override int GetStat()
		{
			return GlobalMembers.gApp.mProfile.mStats.MaxStat(38);
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Chain reaction", 4030);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Detonate 7 special gems in a single move", 4066);
		}
	}
}
