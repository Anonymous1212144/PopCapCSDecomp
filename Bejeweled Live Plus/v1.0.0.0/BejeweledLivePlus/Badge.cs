using System;
using SexyFramework;
using SexyFramework.Graphics;

namespace BejeweledLivePlus
{
	public class Badge : IDisposable
	{
		public virtual void Dispose()
		{
		}

		public virtual bool CanUnlock()
		{
			return this.GetStat() >= this.mUnlockRequirement && !this.mUnlocked;
		}

		public virtual int GetStat()
		{
			return 0;
		}

		public virtual bool WantsMidGameCalc()
		{
			return true;
		}

		public virtual bool DoesBadgeApply()
		{
			return true;
		}

		public virtual int GetMaxLevel()
		{
			return GlobalMembers.gApp.mBadgeCutoffs[this.mIdx].size<int>();
		}

		public virtual string GetTooltipHeader()
		{
			return "Base Achievement";
		}

		public virtual string GetTooltipBody()
		{
			return "Base Achievement How To";
		}

		public virtual bool IsProgressive()
		{
			return true;
		}

		public virtual string GetProgressString()
		{
			return "TODO:Badge progress";
		}

		public virtual string GetAwardString()
		{
			return string.Empty;
		}

		public string GetBadgeLevelName()
		{
			return this.GetBadgeLevelName(false);
		}

		public string GetBadgeLevelName(bool nextLevel)
		{
			if (Profile.IsEliteBadge(this.mIdx))
			{
				return string.Empty;
			}
			string[] array = new string[]
			{
				GlobalMembers._ID("^FF6600^Bronze^FFFFFF^", 3079),
				GlobalMembers._ID("^EEEEEE^Silver^FFFFFF^", 3080),
				GlobalMembers._ID("^FFEE00^Gold^FFFFFF^", 3081),
				GlobalMembers._ID("^66BBFF^Platinum^FFFFFF^", 3082),
				""
			};
			if (nextLevel)
			{
				return array[(int)this.mLevel];
			}
			return array[Math.Max(0, this.mLevel - BadgeLevel.BADGELEVEL_BRONZE)];
		}

		protected virtual string GetNextLevelString()
		{
			return string.Empty;
		}

		protected virtual string GetMaxLevelString()
		{
			return string.Empty;
		}

		protected virtual int GetPrevLevelReq()
		{
			return 0;
		}

		protected virtual int GetNextLevelReq()
		{
			return 0;
		}

		protected virtual int GetLevelProgress()
		{
			return this.GetStat();
		}

		public virtual string GetGameCenterId(int level)
		{
			return string.Empty;
		}

		public int mIdx = -1;

		public int mGPoints;

		public int mUnlockRequirement;

		public bool mUnlocked;

		public Board mBoard;

		public Image mSmallIcon;

		public Image mSmallIconGray;

		public BadgeLevel mLevel;

		public bool mPending;

		public string mName = "";
	}
}
