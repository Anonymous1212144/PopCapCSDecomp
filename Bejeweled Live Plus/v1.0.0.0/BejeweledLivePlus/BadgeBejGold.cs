using System;

namespace BejeweledLivePlus
{
	public class BadgeBejGold : Badge
	{
		public BadgeBejGold()
		{
			this.mIdx = 1;
			this.mGPoints = 10;
			this.mUnlockRequirement = 300000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BEJEWELER;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BEJEWELER;
			this.mLevel = BadgeLevel.BADGELEVEL_GOLD;
			this.mName = "Bejeweler_Gold";
		}

		public override int GetStat()
		{
			int result = 0;
			if (this.mBoard is ClassicBoard)
			{
				result = ((ClassicBoard)this.mBoard).mPoints;
			}
			return result;
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Bejeweler Gold", 4013);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Score 300 000 points in classic mode", 4053);
		}
	}
}
