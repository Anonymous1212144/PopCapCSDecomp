using System;

namespace BejeweledLivePlus
{
	public class BadgeFinalfrenzyBronze : Badge
	{
		public BadgeFinalfrenzyBronze()
		{
			this.mIdx = 6;
			this.mGPoints = 5;
			this.mUnlockRequirement = 20000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_ELECTRIFIER;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_ELECTRIFIER;
			this.mLevel = BadgeLevel.BADGELEVEL_BRONZE;
			this.mName = "Final_frenzy_Bronze";
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
			return GlobalMembers._ID("Final frenzy Bronze", 4018);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score over 20 000 points during a Last Hurrah in Lightning mode", 4068);
		}
	}
}
