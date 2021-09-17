using System;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	internal class StatsMenuContainer : Bej3Widget, ScrollWidgetListener
	{
		private string GetFavouriteGemColour(ref Color theColour, ref bool wasSet)
		{
			string result = GlobalMembers._ID("N/A", 3503);
			string[] array = new string[]
			{
				GlobalMembers._ID("Red", 3504),
				GlobalMembers._ID("White", 3505),
				GlobalMembers._ID("Green", 3506),
				GlobalMembers._ID("Yellow", 3507),
				GlobalMembers._ID("Purple", 3508),
				GlobalMembers._ID("Orange", 3509),
				GlobalMembers._ID("Blue", 3510)
			};
			Color[] array2 = new Color[]
			{
				new Color(254, 11, 11),
				new Color(255, 255, 255),
				new Color(25, 162, 7),
				new Color(249, 255, 48),
				new Color(118, 20, 187),
				new Color(255, 120, 0),
				new Color(11, 23, 182)
			};
			wasSet = false;
			int num = -1;
			int num2 = 0;
			for (int i = 0; i < 7; i++)
			{
				if (GlobalMembers.gApp.mProfile.mStats[5 + i] > num2)
				{
					num2 = GlobalMembers.gApp.mProfile.mStats[5 + i];
					num = i;
				}
			}
			if (num > -1)
			{
				result = array[num];
				theColour = array2[num];
				wasSet = true;
			}
			return result;
		}

		public StatsMenuContainer()
			: base(Menu_Type.MENU_STATSMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mDoesSlideInFromBottom = (this.mCanAllowSlide = false);
			this.Resize(0, 0, GlobalMembers.gApp.mWidth - ConstantsWP.STATS_MENU_CONTAINER_PADDING_X * 2, ConstantsWP.STATS_MENU_CONTAINER_HEIGHT);
			int stats_MENU_HEADINGS_STEP_Y = ConstantsWP.STATS_MENU_HEADINGS_STEP_Y;
			int num = ConstantsWP.STATS_MENU_HEADINGS_START_Y_1;
			int theX = ConstantsWP.STATS_MENU_HEADINGS_X;
			int num2 = 0;
			this.mHeadingLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Gems Matched:", 3496), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mHeadingLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 1;
			this.mHeadingLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Flame Gems:", 3497), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mHeadingLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 2;
			this.mHeadingLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Star Gems:", 3498), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mHeadingLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 3;
			this.mHeadingLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Hypercubes:", 3499), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mHeadingLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 4;
			this.mHeadingLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Favorite Gem Color:", 3500), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mHeadingLabels[num2].Resize(theX, num, 0, 0);
			num = ConstantsWP.STATS_MENU_HEADINGS_START_Y_2;
			num2 = 5;
			this.mHeadingLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Current Zen Score:", 3441), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mHeadingLabels[num2].Resize(theX, num, 0, 0);
			num = ConstantsWP.STATS_MENU_HEADINGS_START_Y_3;
			num2 = 6;
			this.mHeadingLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("All-Time Best Move:\n", 3501), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mHeadingLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 7;
			this.mHeadingLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, GlobalMembers._ID("Total Time Played:", 3502), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			this.mHeadingLabels[num2].Resize(theX, num, 0, 0);
			for (int i = 0; i < 8; i++)
			{
				this.AddWidget(this.mHeadingLabels[i]);
			}
			num = ConstantsWP.STATS_MENU_HEADINGS_START_Y_1;
			Label_Alignment_Horizontal horizontalAlignment = Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT;
			num += ConstantsWP.STATS_MENU_DATA_Y_OFFSET;
			stats_MENU_HEADINGS_STEP_Y = ConstantsWP.STATS_MENU_HEADINGS_STEP_Y;
			theX = ConstantsWP.STATS_MENU_STATS_X;
			num2 = 0;
			this.mStatsLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, horizontalAlignment);
			this.mStatsLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 1;
			this.mStatsLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, horizontalAlignment);
			this.mStatsLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 2;
			this.mStatsLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, horizontalAlignment);
			this.mStatsLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 3;
			this.mStatsLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, horizontalAlignment);
			this.mStatsLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num += ConstantsWP.STATS_MENU_DATA_Y_OFFSET;
			num2 = 4;
			this.mStatsLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, horizontalAlignment);
			this.mStatsLabels[num2].Resize(theX, num, 0, 0);
			num = ConstantsWP.STATS_MENU_HEADINGS_START_Y_2;
			num2 = 5;
			this.mStatsLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, horizontalAlignment);
			this.mStatsLabels[num2].Resize(theX, num, 0, 0);
			num = ConstantsWP.STATS_MENU_HEADINGS_START_Y_3;
			num2 = 6;
			this.mStatsLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, horizontalAlignment);
			this.mStatsLabels[num2].Resize(theX, num, 0, 0);
			num += stats_MENU_HEADINGS_STEP_Y;
			num2 = 7;
			this.mStatsLabels[num2] = new Label(GlobalMembersResources.FONT_DIALOG, horizontalAlignment);
			this.mStatsLabels[num2].Resize(theX, num, 0, 0);
			for (int j = 0; j < 8; j++)
			{
				this.mStatsLabels[j].SetLayerColor(0, Bej3Widget.COLOR_DIALOG_4_FILL);
				this.AddWidget(this.mStatsLabels[j]);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public override void Show()
		{
			base.Show();
			this.mY = 0;
			this.mAlphaCurve.SetConstant(1.0);
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.STATS_MENU_DIVIDER_2_Y);
			Bej3Widget.DrawDividerCentered(g, this.mWidth / 2, ConstantsWP.STATS_MENU_DIVIDER_3_Y);
		}

		public override void LinkUpAssets()
		{
			this.mStatsLabels[0].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.mStats.MaxStat(4)));
			this.mStatsLabels[1].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.mStats.MaxStat(12)));
			this.mStatsLabels[2].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.mStats.MaxStat(13)));
			this.mStatsLabels[3].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.mStats.MaxStat(14)));
			Color color_DIALOG_1_FILL = Bej3Widget.COLOR_DIALOG_1_FILL;
			bool flag = false;
			this.mStatsLabels[4].SetText(this.GetFavouriteGemColour(ref color_DIALOG_1_FILL, ref flag));
			this.mStatsLabels[4].SetColor(color_DIALOG_1_FILL);
			if (flag)
			{
				this.mStatsLabels[4].SetLayerColor(0, Color.White);
			}
			else
			{
				this.mStatsLabels[4].SetLayerColor(0, Bej3Widget.COLOR_DIALOG_4_FILL);
			}
			this.mStatsLabels[5].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.mStats.MaxStat(2)));
			this.mStatsLabels[6].SetText(Common.CommaSeperate(GlobalMembers.gApp.mProfile.mStats.MaxStat(25)));
			this.mStatsLabels[7].SetText(Profile.GetTotalTimePlayed());
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		private Label[] mHeadingLabels = new Label[8];

		private Label[] mStatsLabels = new Label[8];

		private enum STATSMENU_STATS
		{
			STATSMENU_STATS_GEMS_MATCHED,
			STATSMENU_STATS_FLAME_GEMS,
			STATSMENU_STATS_STAR_GEMS,
			STATSMENU_STATS_HYPERCUBES,
			STATSMENU_STATS_FAV_GEM_COL,
			STATSMENU_STATS_HIGHSCORE_ZEN,
			STATSMENU_STATS_ALL_TIME_BEST_MOVE,
			STATSMENU_STATS_TOTAL_TIME_PLAYED,
			STATSMENU_STATS_MAX
		}
	}
}
