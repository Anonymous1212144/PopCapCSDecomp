using System;

namespace BejeweledLivePlus
{
	public class BadgeFinalfrenzyPlatinum : Badge
	{
		public BadgeFinalfrenzyPlatinum()
		{
			this.mIdx = 7;
			this.mGPoints = 10;
			this.mUnlockRequirement = 60000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_ELECTRIFIER;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_ELECTRIFIER;
			this.mLevel = BadgeLevel.BADGELEVEL_PLATINUM;
			this.mName = "Final_frenzy_Platinum";
		}

		public override int GetStat()
		{
			int result = 0;
			if (this.mBoard is SpeedBoard)
			{
				result = GlobalMembers.gApp.mBoard.mPoints - ((SpeedBoard)this.mBoard).mPreHurrahPoints;
			}
			return result;
		}

		public override bool WantsMidGameCalc()
		{
			return false;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Final frenzy Platinum", 4019);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score over 60 000 points during a Last Hurrah in Lightning mode", 4069);
		}
	}
}
