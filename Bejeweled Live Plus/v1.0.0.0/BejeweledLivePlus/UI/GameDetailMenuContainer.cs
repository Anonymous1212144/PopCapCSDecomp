using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class GameDetailMenuContainer : Bej3Widget
	{
		public GameDetailMenuContainer()
			: base(Menu_Type.MENU_GAMEDETAILMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mScrollLocked = false;
			this.mDoesSlideInFromBottom = false;
			int gamedetailmenu_POST_GAME_TAB_WIDTH = ConstantsWP.GAMEDETAILMENU_POST_GAME_TAB_WIDTH;
			this.Resize(ConstantsWP.GAMEDETAILMENU_POST_GAME_CONTAINER_X, ConstantsWP.GAMEDETAILMENU_POST_GAME_CONTAINER_Y, (ConstantsWP.GAMEDETAILMENU_POST_GAME_TAB_WIDTH + ConstantsWP.GAMEDETAILMENU_POST_GAME_TAB_PADDING) * 2, ConstantsWP.GAMEDETAILMENU_POST_GAME_CONTAINER_HEIGHT);
			this.mHighScoresWidgetPostGame = new HighScoresWidget(new Rect(ConstantsWP.GAMEDETAILMENU_HIGHSCORES_POST_GAME_X, ConstantsWP.GAMEDETAILMENU_HIGHSCORES_POST_GAME_Y, ConstantsWP.GAMEDETAILMENU_HIGHSCORES_POST_GAME_WIDTH, ConstantsWP.GAMEDETAILMENU_HIGHSCORES_POST_GAME_HEIGHT), false, ConstantsWP.GAMEDETAILMENU_POST_GAME_SCROLLWIDGET_CORRECTION);
			this.AddWidget(this.mHighScoresWidgetPostGame);
			int theWidth;
			int theX;
			if (GlobalMembers.gApp.mGameCenterIsAvailable)
			{
				theWidth = ConstantsWP.GAMEDETAILMENU_POST_GAME_RANKBAR_1_WIDTH_INCL_GC;
				theX = ConstantsWP.GAMEDETAILMENU_POST_GAME_RANKBAR_1_X_INCL_GC;
			}
			else
			{
				theWidth = ConstantsWP.GAMEDETAILMENU_POST_GAME_RANKBAR_1_WIDTH;
				theX = ConstantsWP.GAMEDETAILMENU_POST_GAME_RANKBAR_1_X;
			}
			this.mRankBarWidget1 = new RankBarWidget(theWidth);
			this.mRankBarWidget1.Resize(theX, ConstantsWP.GAMEDETAILMENU_POST_GAME_RANKBAR_1_Y, theWidth, 100);
			this.mRankBarWidget1.mDrawCrown = true;
			this.mRankBarWidget1.mTobleroning = true;
			this.mRankBarWidget1.mDrawText = true;
			this.AddWidget(this.mRankBarWidget1);
			if (GlobalMembers.gApp.mGameCenterIsAvailable)
			{
				this.mGameCenterButton = new Bej3Button(0, this, Bej3ButtonType.BUTTON_TYPE_GAMECENTER);
				Bej3Widget.CenterWidgetAt(ConstantsWP.GAMEDETAILMENU_GAMECENTER_X, ConstantsWP.GAMEDETAILMENU_GAMECENTER_Y, this.mGameCenterButton);
				this.AddWidget(this.mGameCenterButton);
			}
			else
			{
				this.mGameCenterButton = null;
			}
			this.mSpecialGemsHeadingLabel = new Label(GlobalMembersResources.FONT_SUBHEADER, string.Empty);
			this.mSpecialGemsHeadingLabel.SetLayerColor(0, Bej3Widget.COLOR_SUBHEADING_2_STROKE);
			this.mSpecialGemsHeadingLabel.SetLayerColor(1, Bej3Widget.COLOR_SUBHEADING_2_FILL);
			this.mSpecialGemsHeadingLabel.Resize(ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_HEADING_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_HEADING_Y, 0, 0);
			this.AddWidget(this.mSpecialGemsHeadingLabel);
			int num = ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_START_Y;
			this.mStatsHeaderLabel = new Label(GlobalMembersResources.FONT_SUBHEADER, GlobalMembers._ID("STATS", 3296), Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_CENTRE);
			this.mStatsHeaderLabel.SetLayerColor(0, Bej3Widget.COLOR_SUBHEADING_1_STROKE);
			this.mStatsHeaderLabel.SetLayerColor(1, Bej3Widget.COLOR_SUBHEADING_1_FILL);
			this.mStatsHeaderLabel.Resize(gamedetailmenu_POST_GAME_TAB_WIDTH * 3 / 2, num, 0, 0);
			this.AddWidget(this.mStatsHeaderLabel);
			int theX2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_X + gamedetailmenu_POST_GAME_TAB_WIDTH;
			num += ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_STEP_Y_BIG;
			Label label = new Label(GlobalMembersResources.FONT_DIALOG, string.Empty, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			label.Resize(theX2, num, 0, 0);
			this.AddWidget(label);
			this.mStatsHeadingLabels[0] = label;
			num += ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_STEP_Y;
			label = new Label(GlobalMembersResources.FONT_DIALOG, string.Empty, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			label.Resize(theX2, num, 0, 0);
			this.AddWidget(label);
			this.mStatsHeadingLabels[1] = label;
			num += ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_STEP_Y;
			label = new Label(GlobalMembersResources.FONT_DIALOG, string.Empty, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			label.Resize(theX2, num, 0, 0);
			this.AddWidget(label);
			this.mStatsHeadingLabels[2] = label;
			num += ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_STEP_Y;
			label = new Label(GlobalMembersResources.FONT_DIALOG, string.Empty, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
			label.Resize(theX2, num, 0, 0);
			this.AddWidget(label);
			this.mStatsHeadingLabels[3] = label;
			theX2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_DATA_X + gamedetailmenu_POST_GAME_TAB_WIDTH;
			num = ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_START_Y + ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_STEP_Y_BIG;
			label = new Label(GlobalMembersResources.FONT_DIALOG, string.Empty, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT);
			label.Resize(theX2, num, 0, 0);
			this.AddWidget(label);
			this.mStatsLabels[0] = label;
			num += ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_STEP_Y;
			label = new Label(GlobalMembersResources.FONT_DIALOG, string.Empty, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT);
			label.Resize(theX2, num, 0, 0);
			this.AddWidget(label);
			this.mStatsLabels[1] = label;
			num += ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_STEP_Y;
			label = new Label(GlobalMembersResources.FONT_DIALOG, string.Empty, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT);
			label.Resize(theX2, num, 0, 0);
			this.AddWidget(label);
			this.mStatsLabels[2] = label;
			num += ConstantsWP.GAMEDETAILMENU_POST_GAME_STATS_STEP_Y;
			label = new Label(GlobalMembersResources.FONT_DIALOG, string.Empty, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT);
			label.Resize(theX2, num, 0, 0);
			this.AddWidget(label);
			this.mStatsLabels[3] = label;
			for (int i = 0; i < 4; i++)
			{
				this.mStatsHeadingLabels[i].SetLayerColor(0, Bej3Widget.COLOR_DIALOG_WHITE);
				this.mStatsLabels[i].SetLayerColor(0, Bej3Widget.COLOR_DIALOG_4_FILL);
			}
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
			base.Dispose();
		}

		public void SetMode(GameMode mode, GameDetailMenu.GAMEDETAILMENU_STATE state)
		{
			this.mMode = mode;
			this.mHighScoresWidgetPostGame.SetMode(mode);
			this.mHighScoresWidgetPostGame.SetHeading(GlobalMembers.gApp.GetModeHeading(mode));
			switch (mode)
			{
			case GameMode.MODE_CLASSIC:
				this.mStatsHeadingLabels[0].SetText(GlobalMembers._ID("Level Achieved:", 3297));
				this.mStatsHeadingLabels[1].SetText(GlobalMembers._ID("Best Move:", 3298));
				this.mStatsHeadingLabels[2].SetText(GlobalMembers._ID("Longest Cascade:", 3299));
				this.mStatsHeadingLabels[3].SetText(GlobalMembers._ID("Total Time:", 3300));
				this.mSpecialGemsHeadingLabel.SetText(GlobalMembers._ID("SPECIAL GEMS", 3301));
				return;
			case GameMode.MODE_LIGHTNING:
				this.mStatsHeadingLabels[0].SetText(GlobalMembers._ID("Highest Multiplier:", 3317));
				this.mStatsHeadingLabels[1].SetText(GlobalMembers._ID("Best move:", 3318));
				this.mStatsHeadingLabels[2].SetText(GlobalMembers._ID("Longest Cascade:", 3319));
				this.mStatsHeadingLabels[3].SetText(GlobalMembers._ID("Total time:", 3320));
				this.mSpecialGemsHeadingLabel.SetText(GlobalMembers._ID("SPECIAL GEMS", 3301));
				break;
			case GameMode.MODE_DIAMOND_MINE:
				this.mStatsHeadingLabels[0].SetText(GlobalMembers._ID("Max Depth:", 3302));
				this.mStatsHeadingLabels[1].SetText(GlobalMembers._ID("Total Time:", 3303));
				this.mStatsHeadingLabels[2].SetText(GlobalMembers._ID("Best Move:", 3304));
				this.mStatsHeadingLabels[3].SetText(GlobalMembers._ID("Best Treasure:", 3305));
				this.mSpecialGemsHeadingLabel.SetText(GlobalMembers._ID("TREASURE FOUND", 3306));
				return;
			case GameMode.MODE_ZEN:
			case GameMode.MODE_ICESTORM:
				break;
			case GameMode.MODE_BUTTERFLY:
				this.mStatsHeadingLabels[0].SetText(GlobalMembers._ID("Butterflies Freed:", 3307));
				this.mStatsHeadingLabels[1].SetText(GlobalMembers._ID("Best Move:", 3308));
				this.mStatsHeadingLabels[2].SetText(GlobalMembers._ID("Best Butterfly Combo:", 3309));
				this.mStatsHeadingLabels[3].SetText(GlobalMembers._ID("Total Time:", 3310));
				this.mSpecialGemsHeadingLabel.SetText(GlobalMembers._ID("SPECIAL GEMS", 3311));
				return;
			case GameMode.MODE_POKER:
				this.mStatsHeadingLabels[0].SetText(GlobalMembers._ID("Best Hand:", 3312));
				this.mStatsHeadingLabels[1].SetText(GlobalMembers._ID("Number of Hands:", 3313));
				this.mStatsHeadingLabels[2].SetText(GlobalMembers._ID("Skulls Busted:", 3314));
				this.mStatsHeadingLabels[3].SetText(GlobalMembers._ID("Skull Coin Flips:", 3315));
				this.mSpecialGemsHeadingLabel.SetText(GlobalMembers._ID("SPECIAL GEMS", 3316));
				return;
			default:
				return;
			}
		}

		public override void Update()
		{
			base.Update();
			this.mScrollLocked = this.mHighScoresWidgetPostGame.mScrollLocked;
		}

		public override void Draw(Graphics g)
		{
			g.PushState();
			int gamedetailmenu_POST_GAME_TAB_WIDTH = ConstantsWP.GAMEDETAILMENU_POST_GAME_TAB_WIDTH;
			Bej3Widget.DrawInlayBox(g, new Rect(ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_Y, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_HEIGHT), this.mSpecialGemsHeadingLabel.GetTextWidth());
			Bej3Widget.DrawInlayBoxShadow(g, new Rect(ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_Y, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_HEIGHT));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_DIALOG, 0, Color.White);
			int gamedetailmenu_POST_GAME_TAB_WIDTH2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_TAB_WIDTH;
			Bej3Widget.DrawDividerCentered(g, ConstantsWP.GAMEDETAILMENU_POST_GAME_TAB_WIDTH / 2 + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_DIVIDER_Y);
			switch (this.mMode)
			{
			case GameMode.MODE_CLASSIC:
			case GameMode.MODE_LIGHTNING:
			case GameMode.MODE_BUTTERFLY:
			case GameMode.MODE_POKER:
			{
				Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_FLAME_LRG, 0, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_1_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_FLAME_GEM_POS_Y);
				Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_STAR_LRG, 0, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_2_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_STAR_GEM_POS_Y);
				Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_HYPERCUBE_LRG, 0, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_3_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_HYPERCUBE_POS_Y);
				int theX = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_1_X + gamedetailmenu_POST_GAME_TAB_WIDTH2 + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_X_1;
				int theY = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_Y;
				g.DrawString(this.mSpecialStatsStrings[0], theX, theY);
				theX = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_2_X + gamedetailmenu_POST_GAME_TAB_WIDTH2 + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_X_2;
				theY = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_Y;
				g.DrawString(this.mSpecialStatsStrings[1], theX, theY);
				theX = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_3_X + gamedetailmenu_POST_GAME_TAB_WIDTH2 + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_X_3;
				theY = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_Y;
				g.DrawString(this.mSpecialStatsStrings[2], theX, theY);
				break;
			}
			case GameMode.MODE_DIAMOND_MINE:
			{
				Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_GOLD, 0, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_1_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y);
				Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_GEM, 0, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_2_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y);
				Bej3Widget.DrawImageCentered(g, GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_TREASURE, 0, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_3_X + gamedetailmenu_POST_GAME_TAB_WIDTH, ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y);
				int theX2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_1_X + gamedetailmenu_POST_GAME_TAB_WIDTH2 + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_X_1;
				int theY2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_Y;
				g.DrawString(this.mSpecialStatsStrings[0], theX2, theY2);
				theX2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_2_X + gamedetailmenu_POST_GAME_TAB_WIDTH2 + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_X_1;
				theY2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_Y;
				g.DrawString(this.mSpecialStatsStrings[1], theX2, theY2);
				theX2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_POS_3_X + gamedetailmenu_POST_GAME_TAB_WIDTH2 + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_X_1;
				theY2 = ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_DIAMOND_MINE_POS_Y + ConstantsWP.GAMEDETAILMENU_POST_GAME_INLAY_1_GEM_TEXT_OFFSET_Y;
				g.DrawString(this.mSpecialStatsStrings[2], theX2, theY2);
				break;
			}
			}
			g.PopState();
		}

		public override void Show()
		{
			base.Show();
			this.LinkUpAssets();
		}

		public override void ShowCompleted()
		{
			base.ShowCompleted();
		}

		public override void Hide()
		{
			base.Hide();
		}

		public override void HideCompleted()
		{
			this.mRankBarWidget1.Shown(null);
			base.HideCompleted();
			this.mHighScoresWidgetPostGame.UnNewScore();
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
		}

		public void GetStatsFromBoard(Board theBoard)
		{
			if (theBoard != null)
			{
				switch (this.mMode)
				{
				case GameMode.MODE_CLASSIC:
					this.mStatsLabels[0].SetText(SexyFramework.Common.CommaSeperate(theBoard.mLevel + 1));
					this.mStatsLabels[1].SetText(SexyFramework.Common.CommaSeperate(theBoard.mGameStats[25]));
					this.mStatsLabels[2].SetText(SexyFramework.Common.CommaSeperate(theBoard.mGameStats[24]));
					this.mStatsLabels[3].SetText(Utils.GetTimeString(theBoard.mGameStats[0]));
					this.mSpecialStatsStrings[0] = string.Format(GlobalMembers._ID("x {0}", 3321), theBoard.mGameStats[17]);
					this.mSpecialStatsStrings[1] = string.Format(GlobalMembers._ID("x {0}", 3322), theBoard.mGameStats[18]);
					this.mSpecialStatsStrings[2] = string.Format(GlobalMembers._ID("x {0}", 3323), theBoard.mGameStats[19]);
					break;
				case GameMode.MODE_LIGHTNING:
					this.mStatsLabels[0].SetText(SexyFramework.Common.CommaSeperate(theBoard.mPointMultiplier));
					this.mStatsLabels[1].SetText(SexyFramework.Common.CommaSeperate(theBoard.mGameStats[25]));
					this.mStatsLabels[2].SetText(SexyFramework.Common.CommaSeperate(theBoard.mGameStats[24]));
					this.mStatsLabels[3].SetText(Utils.GetTimeString(theBoard.mGameStats[0] - 3));
					this.mSpecialStatsStrings[0] = string.Format(GlobalMembers._ID("x {0}", 3331), theBoard.mGameStats[17]);
					this.mSpecialStatsStrings[1] = string.Format(GlobalMembers._ID("x {0}", 3332), theBoard.mGameStats[18]);
					this.mSpecialStatsStrings[2] = string.Format(GlobalMembers._ID("x {0}", 3333), theBoard.mGameStats[19]);
					break;
				case GameMode.MODE_DIAMOND_MINE:
				{
					DigBoard digBoard = (DigBoard)theBoard;
					DigGoal digGoal = (DigGoal)digBoard.mQuestGoal;
					int aTime = digBoard.mGameStats[0];
					this.mStatsLabels[0].SetText(string.Format(GlobalMembers._ID("{0} m", 3324), SexyFramework.Common.CommaSeperate(digGoal.GetGridDepth() * 10)));
					this.mStatsLabels[1].SetText(Utils.GetTimeString(aTime));
					this.mStatsLabels[2].SetText(SexyFramework.Common.CommaSeperate(theBoard.mGameStats[25]));
					int num = 0;
					for (int i = 0; i < digGoal.mCollectedArtifacts.Count; i++)
					{
						DigGoal.ArtifactData artifactData = digGoal.mArtifacts[digGoal.mCollectedArtifacts[i]];
						if (digGoal.mArtifactBaseValue * artifactData.mValue >= num)
						{
							num = digGoal.mArtifactBaseValue * artifactData.mValue;
						}
					}
					this.mStatsLabels[3].SetText(string.Format("{0}", SexyFramework.Common.CommaSeperate(num)));
					int num2 = 0;
					int num3 = 0;
					for (int j = 0; j < 3; j++)
					{
						num2 = GlobalMembers.MAX(digGoal.mTreasureEarnings[j], num2);
						num3 += digGoal.mTreasureEarnings[j];
					}
					if (num2 > 0)
					{
						int num4 = (int)((double)digGoal.mTreasureEarnings[0] * 100.0 / (double)num3 + 0.5);
						int num5 = (int)((double)digGoal.mTreasureEarnings[1] * 100.0 / (double)num3 + 0.5);
						int num6 = (int)((double)digGoal.mTreasureEarnings[2] * 100.0 / (double)num3 + 0.5);
						int num7 = num4 + num5 + num6;
						if (num7 != 100)
						{
							num4 -= num7 - 100;
						}
						this.mSpecialStatsStrings[0] = string.Format(GlobalMembers._ID("{0}%", 3490), num4);
						this.mSpecialStatsStrings[1] = string.Format(GlobalMembers._ID("{0}%", 3491), num5);
						this.mSpecialStatsStrings[2] = string.Format(GlobalMembers._ID("{0}%", 3492), num6);
					}
					else
					{
						this.mSpecialStatsStrings[0] = string.Format(GlobalMembers._ID("{0}%", 3493), 0);
						this.mSpecialStatsStrings[1] = string.Format(GlobalMembers._ID("{0}%", 3494), 0);
						this.mSpecialStatsStrings[2] = string.Format(GlobalMembers._ID("{0}%", 3495), 0);
					}
					break;
				}
				case GameMode.MODE_BUTTERFLY:
					this.mStatsLabels[0].SetText(SexyFramework.Common.CommaSeperate(theBoard.mGameStats[28]));
					this.mStatsLabels[1].SetText(SexyFramework.Common.CommaSeperate(theBoard.mGameStats[25]));
					this.mStatsLabels[2].SetText(SexyFramework.Common.CommaSeperate(theBoard.mGameStats[29]));
					this.mStatsLabels[3].SetText(Utils.GetTimeString(theBoard.mGameStats[0]));
					this.mSpecialStatsStrings[0] = string.Format(GlobalMembers._ID("x {0}", 3325), theBoard.mGameStats[17]);
					this.mSpecialStatsStrings[1] = string.Format(GlobalMembers._ID("x {0}", 3326), theBoard.mGameStats[18]);
					this.mSpecialStatsStrings[2] = string.Format(GlobalMembers._ID("x {0}", 3327), theBoard.mGameStats[19]);
					break;
				case GameMode.MODE_POKER:
					throw new NotImplementedException();
				}
				this.mRankBarWidget1.Shown(theBoard);
			}
		}

		public override void ButtonDepress(int theId)
		{
			if (theId != 0)
			{
				return;
			}
			switch (this.mMode)
			{
			case GameMode.MODE_CLASSIC:
				throw new NotImplementedException();
			case GameMode.MODE_LIGHTNING:
			case GameMode.MODE_ZEN:
				return;
			case GameMode.MODE_DIAMOND_MINE:
				throw new NotImplementedException();
			case GameMode.MODE_BUTTERFLY:
				throw new NotImplementedException();
			default:
				return;
			}
		}

		public const int STAT_TYPE_MAX = 4;

		private GameMode mMode;

		private Label[] mStatsHeadingLabels = new Label[4];

		private Label[] mStatsLabels = new Label[4];

		private Label mSpecialGemsHeadingLabel;

		private RankBarWidget mRankBarWidget1;

		public HighScoresWidget mHighScoresWidgetPostGame;

		private string[] mSpecialStatsStrings = new string[3];

		private Label mStatsHeaderLabel;

		private Bej3Button mGameCenterButton;

		public bool mScrollLocked;

		private enum BUTTON_IDS
		{
			BTN_GAMECENTER_ID
		}
	}
}
