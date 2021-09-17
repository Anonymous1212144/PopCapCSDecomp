using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class SpeedEndLevelDialog : EndLevelDialog
	{
		public SpeedEndLevelDialog(SpeedBoard theBoard)
			: base(theBoard)
		{
			this.mSpeedBoard = theBoard;
			this.mTotalBonusTime = this.mSpeedBoard.mTotalBonusTime;
		}

		public override void Update()
		{
			base.Update();
		}

		public override void DrawStatsLabels(Graphics g)
		{
			string theString = GlobalMembers._ID("Highest Multiplier", 483);
			int theX = GlobalMembers.MS(230);
			int theY = GlobalMembers.MS(475);
			GlobalMembers.MS(48);
			g.WriteString(theString, theX, theY, -1, -1);
			g.WriteString(GlobalMembers._ID("Best Move", 484), GlobalMembers.MS(230), GlobalMembers.MS(475) + GlobalMembers.MS(48), -1, -1);
			g.WriteString(GlobalMembers._ID("Longest Cascade", 485), GlobalMembers.MS(230), GlobalMembers.MS(475) + GlobalMembers.MS(48) * 2, -1, -1);
			g.WriteString(GlobalMembers._ID("Total Time", 486), GlobalMembers.MS(230), GlobalMembers.MS(475) + GlobalMembers.MS(48) * 3, -1, -1);
		}

		public override void DrawStatsText(Graphics g)
		{
			string theString = string.Format(GlobalMembers._ID("x{0}", 487), this.mPointMultiplier);
			int theX = GlobalMembers.MS(750);
			int theY = GlobalMembers.MS(475);
			GlobalMembers.MS(48);
			g.WriteString(theString, theX, theY, -1, 1);
			g.WriteString(SexyFramework.Common.CommaSeperate(this.mGameStats[25]), GlobalMembers.MS(750), GlobalMembers.MS(475) + GlobalMembers.MS(48), -1, 1);
			g.WriteString(SexyFramework.Common.CommaSeperate(this.mGameStats[24]), GlobalMembers.MS(750), GlobalMembers.MS(475) + GlobalMembers.MS(48) * 2, -1, 1);
			int num = 60 + this.mTotalBonusTime;
			g.WriteString(string.Format(GlobalMembers._ID("{0}:{1:d2}", 488), num / 60, num % 60), GlobalMembers.MS(750), GlobalMembers.MS(475) + GlobalMembers.MS(48) * 3, -1, 1);
		}

		public override void DrawFrames(Graphics g)
		{
			base.DrawFrames(g);
		}

		public virtual void DrawBar(Graphics g, Image theImage, Rect theRect)
		{
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_GRAPH, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_SECTION_GRAPH_ID) + -160f), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_SECTION_GRAPH_ID) + 0f));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_LINES, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_LINES_ID) + -160f), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_LINES_ID) + 0f));
			int integer = GlobalMembers.gSexyApp.GetInteger("LIGHTNING_GAMEOVER_DLG_ICON_FLAME_OFFSET", GlobalMembers.M(-160));
			int integer2 = GlobalMembers.gSexyApp.GetInteger("LIGHTNING_GAMEOVER_DLG_ICON_STAR_OFFSET", GlobalMembers.M(-160));
			int integer3 = GlobalMembers.gSexyApp.GetInteger("LIGHTNING_GAMEOVER_DLG_ICON_HYPERCUBE_OFFSET", GlobalMembers.M(-160));
			int integer4 = GlobalMembers.gSexyApp.GetInteger("LIGHTNING_GAMEOVER_DLG_ICON_LIGHTNING_OFFSET", GlobalMembers.M(-160));
			int integer5 = GlobalMembers.gSexyApp.GetInteger("LIGHTNING_GAMEOVER_DLG_BOX_YELLOW_OFFSET", GlobalMembers.M(-160));
			int integer6 = GlobalMembers.gSexyApp.GetInteger("LIGHTNING_GAMEOVER_DLG_BOX_PINK_OFFSET", GlobalMembers.M(-160));
			int integer7 = GlobalMembers.gSexyApp.GetInteger("LIGHTNING_GAMEOVER_DLG_BOX_ORANGE_OFFSET", GlobalMembers.M(-160));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_FLAME, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_ICON_FLAME_ID) + (float)integer2), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_ICON_FLAME_ID) + 0f));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_STAR, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_ICON_STAR_ID) + (float)integer), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_ICON_STAR_ID) + 0f));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_HYPERCUBE, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_ICON_HYPERCUBE_ID) + (float)integer3), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_ICON_HYPERCUBE_ID) + 0f));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_LIGHTNING, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_ICON_LIGHTNING_ID) + (float)integer4), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_ICON_LIGHTNING_ID) + 0f));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_YELLOW, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_BOX_YELLOW_ID) + (float)integer5), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_BOX_YELLOW_ID) + 0f));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_PINK, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_BOX_PINK_ID) + (float)integer6), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_BOX_PINK_ID) + 0f));
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_ORANGE, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_BOX_ORANGE_ID) + (float)integer7), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_BOX_ORANGE_ID) + 0f));
			int num = GlobalMembers.S(GlobalMembers.gSexyApp.GetInteger("LIGHTNING_GAMEOVER_DLG_ICON_LABEL_SPACING", GlobalMembers.M(5)));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("OUTLINE", new Color(GlobalMembers.M(255868960)));
			g.SetColor(new Color(GlobalMembers.M(16777215)));
			g.DrawString(string.Format(GlobalMembers._ID("FLAME x{0}", 489), this.mGameStats[17]), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_ICON_FLAME_ID) + (float)integer) + GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_FLAME.GetCelWidth() + num, GlobalMembers.MS(723));
			g.DrawString(string.Format(GlobalMembers._ID("STAR x{0}", 490), this.mGameStats[18]), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_ICON_STAR_ID) + (float)integer2) + GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_STAR.GetCelWidth() + num, GlobalMembers.MS(723));
			g.DrawString(string.Format(GlobalMembers._ID("HYPER x{0}", 491), this.mGameStats[19]), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_ICON_HYPERCUBE_ID) + (float)integer3) + GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_HYPERCUBE.GetCelWidth() + num, GlobalMembers.MS(723));
			g.DrawString(string.Format(GlobalMembers._ID("TIME +{0}:{1:d2}", 492), this.mTotalBonusTime / 60, this.mTotalBonusTime % 60), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_ICON_LIGHTNING_ID) + (float)integer4) + GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_LIGHTNING.GetCelWidth() + num, GlobalMembers.MS(723));
			g.DrawString(GlobalMembers._ID("SPEED", 493), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_BOX_YELLOW_ID) + (float)integer5) + GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_YELLOW.GetCelWidth() + num, GlobalMembers.MS(723));
			g.DrawString(GlobalMembers._ID("SPECIAL", 494), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_BOX_ORANGE_ID) + (float)integer7) + GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_ORANGE.GetCelWidth() + num, GlobalMembers.MS(723));
			g.DrawString(GlobalMembers._ID("MATCHES", 495), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_BOX_PINK_ID) + (float)integer6) + GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_PINK.GetCelWidth() + num, GlobalMembers.MS(723));
			((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("OUTLINE");
			int num2 = 0;
			int[] array = new int[]
			{
				default(int),
				1,
				2
			};
			int num3 = 3;
			int num4 = 0;
			for (int i = 0; i < this.mPointsBreakdown.Count; i++)
			{
				int num5 = 0;
				for (int j = 0; j < num3; j++)
				{
					num5 += this.mPointsBreakdown[i][array[j]];
				}
				num2 = GlobalMembers.MAX(num2, num5);
				num4 += num5;
			}
			int num6;
			if (num2 <= 5000)
			{
				num6 = 1000;
			}
			else if (num2 <= 10000)
			{
				num6 = 2000;
			}
			else
			{
				num6 = (num2 + 24999) / 25000 * 5000;
			}
			int num7 = GlobalMembers.M(360);
			int num8 = GlobalMembers.M(880) / this.mPointsBreakdown.Count;
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			Utils.SetFontLayerColor((ImageFont)GlobalMembersResources.FONT_DIALOG, "OUTLINE", new Color(0, 0, 0, 0));
			g.SetColor(new Color(GlobalMembers.M(13676688)));
			for (int k = 0; k < 5; k++)
			{
				g.WriteString(string.Format(GlobalMembers._ID("{0}k", 496), (k + 1) * num6 / 1000), GlobalMembers.MS(330), GlobalMembers.MS(975) + k * GlobalMembers.MS(-46), -1, 1);
			}
			for (int l = 0; l < this.mPointsBreakdown.Count; l++)
			{
				for (int m = num3 - 1; m >= 0; m--)
				{
					int num9 = 0;
					for (int n = 0; n <= m; n++)
					{
						num9 += this.mPointsBreakdown[l][array[n]];
					}
					Image[] array2 = new Image[]
					{
						GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR__PINK,
						GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR_ORANGE,
						GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR_YELLOW
					};
					int num10 = (int)((double)(GlobalMembers.M(225) * num9 / (num6 * 5)) * this.mCountupPct);
					if (num10 > 0)
					{
						num10 = GlobalMembers.MAX(num10, GlobalMembers.M(10));
						g.DrawImageBox(new Rect(GlobalMembers.S(num7 + GlobalMembers.M(10)), GlobalMembers.MS(1005) - GlobalMembers.S(num10), GlobalMembers.S(num8 - GlobalMembers.M(20)), GlobalMembers.S(num10)), array2[m]);
					}
					g.WriteString((l == this.mPointsBreakdown.Count - 1) ? GlobalMembers._ID("Last", 497) : string.Format(GlobalMembers._ID("x{0}", 498), l + 1), GlobalMembers.S(num7 + num8 / 2), GlobalMembers.MS(1043));
				}
				num7 += num8;
			}
		}

		public SpeedBoard mSpeedBoard;

		public int mTotalBonusTime;
	}
}
