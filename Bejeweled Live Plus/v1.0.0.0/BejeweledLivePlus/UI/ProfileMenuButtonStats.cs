using System;
using System.Collections.Generic;
using BejeweledLivePlus.Widget;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	internal class ProfileMenuButtonStats : ProfileMenuButton
	{
		public ProfileMenuButtonStats(Bej3ButtonListener theListener)
			: base(6, theListener, GlobalMembers._ID("STATS", 3432))
		{
			this.mCurrentStatIndex = -1;
			base.MakeChildrenTouchInvisible();
			this.SetNextStat();
		}

		public override void Update()
		{
			base.Update();
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			this.mNoStatsMessageLabel.SetTextBlock(new Rect(ConstantsWP.PROFILEMENU_BUTTON_EXTRA_MESSAGE_X, ConstantsWP.PROFILEMENU_BUTTON_EXTRA_MESSAGE_Y, ConstantsWP.PROFILEMENU_BUTTON_EXTRA_MESSAGE_WIDTH, 0), true);
		}

		public void SetNextStat()
		{
		}

		private Label mNoStatsMessageLabel;

		private List<int> mRelevantStats = new List<int>();

		private int mCurrentStatIndex;

		private enum PROFILEMENU_STATS
		{
			PROFILEMENU_STATS_GEMS_MATCHED,
			PROFILEMENU_STATS_FLAME_GEMS,
			PROFILEMENU_STATS_STAR_GEMS,
			PROFILEMENU_STATS_HYPERCUBES,
			PROFILEMENU_STATS_BEST_MOVE,
			PROFILEMENU_STATS_TIME_PLAYED,
			PROFILEMENU_STATS_MAX
		}
	}
}
