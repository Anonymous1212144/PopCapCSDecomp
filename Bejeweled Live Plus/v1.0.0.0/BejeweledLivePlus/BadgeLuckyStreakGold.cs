using System;

namespace BejeweledLivePlus
{
	public class BadgeLuckyStreakGold : Badge
	{
		public BadgeLuckyStreakGold()
		{
			this.mIdx = 19;
			this.mGPoints = 10;
			this.mUnlockRequirement = 250000;
			this.mUnlocked = GlobalMembers.gApp.mProfile.mBadgeStatus[this.mIdx];
			this.mSmallIcon = GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_LUCKY_STREAK;
			this.mSmallIconGray = GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_LUCKYSTREAK;
			this.mLevel = BadgeLevel.BADGELEVEL_GOLD;
			this.mName = "Lucky_Streak_Gold";
		}

		public override int GetStat()
		{
			return GlobalMembers.gApp.mProfile.mLast3MatchScoreManager.GetLowerScore();
		}

		public override bool WantsMidGameCalc()
		{
			return true;
		}

		public override string GetTooltipHeader()
		{
			return GlobalMembers._ID("Lucky Streak", 4031);
		}

		public override string GetTooltipBody()
		{
			return GlobalMembers._ID("Finish 3 games in a row over 250 000 points", 4067);
		}
	}
}
