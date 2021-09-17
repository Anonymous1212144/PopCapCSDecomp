using System;
using System.Collections.Generic;
using BejeweledLivePlus.Bej3Graphics;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	internal class DigGoalEndLevelDialog : EndLevelDialog
	{
		public DigGoalEndLevelDialog(QuestBoard theBoard)
			: base(theBoard)
		{
			base.NudgeButtons(GlobalMembers.MS(-40));
			this.DoTreasureAnim();
			for (int i = 0; i < 3; i++)
			{
				this.mTreasurePct[i] = new CurvedVal();
			}
			foreach (KeyValuePair<int, DialogButton> keyValuePair in this.mBtns)
			{
				keyValuePair.Value.mY += GlobalMembers.MS(40);
			}
			this.mFXManager = new EffectsManager(null);
			this.AddWidget(this.mFXManager);
			this.mFXManager.Resize(0, 0, GlobalMembers.MS(1600), GlobalMembers.MS(1200));
			this.mFXManager.mMouseVisible = false;
			this.mMouseInvisibleChildren.AddLast(this.mFXManager);
		}

		public override void KeyChar(char theChar)
		{
			base.KeyChar(theChar);
		}

		public override void Update()
		{
			base.Update();
			DigGoal digGoal = (DigGoal)((QuestBoard)this.mBoard).mQuestGoal;
			int i = 0;
			while (i < 3)
			{
				if (this.mTreasureDist[i] > 0.0 && !this.mTreasurePct[i].HasBeenTriggered())
				{
					if (this.mTreasurePauseTicks[i] < GlobalMembers.M(50))
					{
						this.mTreasurePauseTicks[i]++;
						break;
					}
					this.mTreasurePct[i].IncInVal();
					break;
				}
				else
				{
					i++;
				}
			}
			if (this.mWidgetManager != null)
			{
				for (int j = 0; j < this.mBarRectVector.size<Rect>(); j++)
				{
					string[] array = new string[]
					{
						GlobalMembers._ID("Gold Collected", 215),
						GlobalMembers._ID("Diamonds Collected", 216),
						GlobalMembers._ID("Artifacts Collected", 217)
					};
					if (this.mBarRectVector[j].Contains(this.mWidgetManager.mLastMouseX, this.mWidgetManager.mLastMouseY))
					{
						GlobalMembers.gApp.mTooltipManager.RequestTooltip(this, array[j], string.Format(GlobalMembers._ID("${0}", 218), SexyFramework.Common.CommaSeperate(digGoal.mTreasureEarnings[j])), new Point(this.mBarRectVector[j].mX + this.mBarRectVector[j].mWidth / 2, this.mBarRectVector[j].mY + GlobalMembers.MS(20)), GlobalMembers.MS(400), 1, GlobalMembers.M(25), null, null, 0, -1);
						break;
					}
				}
				for (int k = 0; k < GlobalMembers.MIN(this.mTreasureRectVector.size<Rect>(), digGoal.mCollectedArtifacts.size<int>()); k++)
				{
					if (this.mTreasureRectVector[k].Contains(this.mWidgetManager.mLastMouseX, this.mWidgetManager.mLastMouseY))
					{
						DigGoal.ArtifactData artifactData = digGoal.mArtifacts[digGoal.mCollectedArtifacts[k]];
						GlobalMembers.gApp.mTooltipManager.RequestTooltip(this, artifactData.mName, string.Format(GlobalMembers._ID("${0}", 219), SexyFramework.Common.CommaSeperate(digGoal.mArtifactBaseValue * artifactData.mValue)), new Point(this.mTreasureRectVector[k].mX + this.mTreasureRectVector[k].mWidth / 2, this.mTreasureRectVector[k].mY + GlobalMembers.MS(20)), GlobalMembers.MS(400), 1, GlobalMembers.M(25), null, null, 0, -1);
						break;
					}
				}
			}
			if (BejeweledLivePlus.Misc.Common.Rand() % GlobalMembers.M(20000) < this.mDiamondRect.mWidth)
			{
				Effect effect = this.mFXManager.AllocEffect(Effect.Type.TYPE_SPARKLE_SHARD);
				effect.mDY *= GlobalMembers.M(0.25f);
				float num = Math.Abs(GlobalMembersUtils.GetRandFloat());
				num *= num;
				effect.mX = (float)(this.mDiamondRect.mX + BejeweledLivePlus.Misc.Common.Rand() % this.mDiamondRect.mWidth);
				effect.mY = (float)(this.mDiamondRect.mY + BejeweledLivePlus.Misc.Common.Rand() % this.mDiamondRect.mHeight);
				effect.mColor = new Color(GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255), GlobalMembers.M(255));
				effect.mIsAdditive = true;
				effect.mDScale = GlobalMembers.M(0.015f);
				effect.mScale = GlobalMembers.M(0.3f) + Math.Abs(GlobalMembersUtils.GetRandFloat()) * GlobalMembers.M(0.2f);
				this.mFXManager.AddEffect(effect);
			}
		}

		public override void DrawStatsLabels(Graphics g)
		{
			g.PushState();
			g.SetColor(new Color(-1));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			((ImageFont)g.GetFont()).PushLayerColor("Outline", new Color(GlobalMembers.M(4210688)));
			((ImageFont)g.GetFont()).PushLayerColor("Glow", new Color(0, 0, 0, 0));
			g.SetColor(new Color(GlobalMembers.M(16763736)));
			g.Translate(GlobalMembers.MS(230), GlobalMembers.MS(450));
			string theString = GlobalMembers._ID("Max Depth", 204);
			int theX = 0;
			int theY = 0;
			GlobalMembers.MS(48);
			g.WriteString(theString, theX, theY, -1, -1);
			g.WriteString(GlobalMembers._ID("Total Time", 205), 0, GlobalMembers.MS(48), -1, -1);
			g.WriteString(GlobalMembers._ID("Best Move", 206), 0, GlobalMembers.MS(48) * 2, -1, -1);
			g.WriteString(GlobalMembers._ID("Best Treasure", 207), 0, GlobalMembers.MS(48) * 3, -1, -1);
			((ImageFont)g.GetFont()).PopLayerColor("Outline");
			((ImageFont)g.GetFont()).PopLayerColor("Glow");
			g.PopState();
		}

		public override void DrawStatsText(Graphics g)
		{
			DigGoal digGoal = (DigGoal)((QuestBoard)this.mBoard).mQuestGoal;
			g.PushState();
			g.SetColor(new Color(-1));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			((ImageFont)g.GetFont()).PushLayerColor("Outline", new Color(GlobalMembers.M(4210688)));
			((ImageFont)g.GetFont()).PushLayerColor("Glow", new Color(0, 0, 0, 0));
			g.SetColor(new Color(GlobalMembers.M(16763736)));
			g.Translate(GlobalMembers.MS(545), GlobalMembers.MS(450));
			string theString = string.Format(GlobalMembers._ID("{0} m", 208), SexyFramework.Common.CommaSeperate(digGoal.GetGridDepth() * 10));
			int theX = GlobalMembers.MS(220);
			int theY = GlobalMembers.MS(0);
			GlobalMembers.MS(48);
			g.WriteString(theString, theX, theY, -1, 1);
			int num = this.mGameStats[0];
			g.WriteString(string.Format(GlobalMembers._ID("{0}:{1:d2}", 209), num / 60, num % 60), GlobalMembers.MS(220), GlobalMembers.MS(0) + GlobalMembers.MS(48), -1, 1);
			g.WriteString(string.Format(GlobalMembers._ID("${0}", 210), SexyFramework.Common.CommaSeperate(this.mGameStats[25])), GlobalMembers.MS(220), GlobalMembers.MS(0) + GlobalMembers.MS(48) * 2, -1, 1);
			int num2 = 0;
			string text = GlobalMembers._ID("No Treasures", 211);
			for (int i = 0; i < digGoal.mCollectedArtifacts.size<int>(); i++)
			{
				DigGoal.ArtifactData artifactData = digGoal.mArtifacts[digGoal.mCollectedArtifacts[i]];
				if (digGoal.mArtifactBaseValue * artifactData.mValue >= num2)
				{
					text = artifactData.mName;
					num2 = digGoal.mArtifactBaseValue * artifactData.mValue;
				}
			}
			g.WriteString(string.Format(GlobalMembers._ID("${0}", 212), SexyFramework.Common.CommaSeperate(num2)), GlobalMembers.MS(220), GlobalMembers.MS(0) + GlobalMembers.MS(48) * 3, -1, 1);
			g.WriteString(string.Format(GlobalMembers._ID("({0})", 213), text), GlobalMembers.MS(-40), GlobalMembers.MS(0) + GlobalMembers.MS(48) * 4, -1, 0);
			((ImageFont)g.GetFont()).PopLayerColor("Outline");
			((ImageFont)g.GetFont()).PopLayerColor("Glow");
			g.PopState();
		}

		public override void DrawFrames(Graphics g)
		{
			g.PushState();
			g.DrawImageBox(new Rect(GlobalMembers.MS(195), GlobalMembers.MS(385) - GlobalMembers.MS(0), GlobalMembers.MS(600), GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_LABEL.GetHeight()), GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_LABEL);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BOX, GlobalMembers.MS(195), GlobalMembers.MS(720));
			g.PopState();
			g.SetColor(new Color(-1));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			((ImageFont)g.GetFont()).PushLayerColor("Main", new Color(GlobalMembers.M(8931352)));
			((ImageFont)g.GetFont()).PushLayerColor("OUTLINE", new Color(GlobalMembers.M(16777215)));
			((ImageFont)g.GetFont()).PushLayerColor("GLOW", new Color(0, 0, 0, 0));
			g.WriteString(GlobalMembers._ID("Treasure Found:", 214), GlobalMembers.MS(800), GlobalMembers.MS(766));
			((ImageFont)g.GetFont()).PopLayerColor("Main");
			((ImageFont)g.GetFont()).PopLayerColor("OUTLINE");
			((ImageFont)g.GetFont()).PopLayerColor("GLOW");
			g.PushState();
			g.Translate(GlobalMembers.MS(0), GlobalMembers.MS(-8));
			this.DrawLabeledStatsFrame(g);
			g.PopState();
			g.PushState();
			g.Translate(GlobalMembers.MS(0), GlobalMembers.MS(-8));
			this.DrawLabeledHighScores(g);
			g.PopState();
		}

		public override void Draw(Graphics g)
		{
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), "Main", new Color(255, 255, 255, 255));
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), "OUTLINE", new Color(255, 255, 255, 255));
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), "GLOW", new Color(0, 0, 0, 0));
			g.DrawImageBox(new Rect(GlobalMembers.MS(85), GlobalMembers.MS(0), GlobalMembers.MS(1430), GlobalMembers.MS(1200)), GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIALOG);
			g.DrawImage(GlobalMembersResourcesWP.IMAGE_GAMEOVER_STAMP, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_GAMEOVER_STAMP_ID) - 160f), (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_GAMEOVER_STAMP_ID) + 0f));
			g.SetColor(new Color(-1));
			g.SetFont(GlobalMembersResources.FONT_HEADER);
			((ImageFont)g.GetFont()).PushLayerColor("Main", new Color(GlobalMembers.M(8931352)));
			((ImageFont)g.GetFont()).PushLayerColor("LAYER_2", new Color(GlobalMembers.M(15253648)));
			((ImageFont)g.GetFont()).PushLayerColor("LAYER_3", new Color(0, 0, 0, 0));
			g.WriteString(string.Format(GlobalMembers._ID("Treasure Excavated:", 220), new object[0]), GlobalMembers.MS(800), GlobalMembers.MS(140));
			((ImageFont)g.GetFont()).PopLayerColor("Main");
			((ImageFont)g.GetFont()).PopLayerColor("LAYER_2");
			((ImageFont)g.GetFont()).PopLayerColor("LAYER_3");
			((ImageFont)g.GetFont()).PushLayerColor("Main", new Color(GlobalMembers.M(16777215)));
			((ImageFont)g.GetFont()).PushLayerColor("LAYER_2", new Color(GlobalMembers.M(11558960)));
			((ImageFont)g.GetFont()).PushLayerColor("LAYER_3", new Color(0, 0, 0, 0));
			g.WriteString(string.Format(GlobalMembers._ID("${0}", 221), SexyFramework.Common.CommaSeperate((int)((double)this.mBoard.mPoints * this.mCountupPct))), GlobalMembers.MS(800), GlobalMembers.MS(220));
			((ImageFont)g.GetFont()).PopLayerColor("Main");
			((ImageFont)g.GetFont()).PopLayerColor("LAYER_2");
			((ImageFont)g.GetFont()).PopLayerColor("LAYER_3");
			g.SetColor(new Color(-1));
			this.DrawFrames(g);
			int num = 0;
			int num2 = 0;
			DigGoal digGoal = (DigGoal)((QuestBoard)this.mBoard).mQuestGoal;
			for (int i = 0; i < 3; i++)
			{
				num = GlobalMembers.MAX(digGoal.mTreasureEarnings[i], num);
				num2 += digGoal.mTreasureEarnings[i];
			}
			((ImageFont)g.GetFont()).PushLayerColor("Outline", new Color(GlobalMembers.M(4210688)));
			((ImageFont)g.GetFont()).PushLayerColor("Glow", new Color(0, 0, 0, 0));
			g.SetColor(new Color(GlobalMembers.M(16763736)));
			Color[] array = new Color[]
			{
				new Color(GlobalMembers.M(16776960)),
				new Color(GlobalMembers.M(11599871)),
				new Color(GlobalMembers.M(16777215))
			};
			Image[] array2 = new Image[]
			{
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_GOLD,
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_GEMS,
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_TREASURE
			};
			this.mTreasureRectVector.Clear();
			this.mBarRectVector.Clear();
			if (num > 0)
			{
				for (int j = 0; j < 3; j++)
				{
					int num3 = GlobalMembers.MS(190) + GlobalMembers.MS(760) * digGoal.mTreasureEarnings[j] / num;
					int num4 = (int)((double)num3 * this.mCountupPct);
					if (j == 1)
					{
						this.mDiamondRect = new Rect(GlobalMembers.M(198), GlobalMembers.M(790) + j * GlobalMembers.M(92), (int)((float)num4 / GlobalMembers.MS(1f) - (float)GlobalMembers.M(105)), GlobalMembers.M(68));
					}
					g.DrawImage(array2[j], GlobalMembers.MS(198), GlobalMembers.MS(780) + j * GlobalMembers.MS(92), new Rect(array2[j].mWidth - num4, 0, num4, array2[j].mHeight));
					g.SetColor(array[j]);
					g.WriteString(string.Format(GlobalMembers._ID("${0}", 223), SexyFramework.Common.CommaSeperate(digGoal.mTreasureEarnings[j])), GlobalMembers.MS(1380), GlobalMembers.MS(840) + j * GlobalMembers.MS(92), -1, 1);
					Color color = array[j];
					color.mAlpha = (int)((double)color.mAlpha * GlobalMembers.MAX(0.0, GlobalMembers.MIN(1.0, this.mCountupPct * GlobalMembers.M(2.0) - GlobalMembers.M(0.9))));
					g.SetColor(color);
					g.WriteString(string.Format(GlobalMembers._ID("{0}%", 222), (int)((double)digGoal.mTreasureEarnings[j] * 100.0 / (double)num2 + 0.5)), GlobalMembers.MS(125) + num4, GlobalMembers.MS(840) + j * GlobalMembers.MS(92), -1, 0);
					if (j == 2 && digGoal.mCollectedArtifacts.size<int>() != 0)
					{
						int num5 = digGoal.mCollectedArtifacts.size<int>();
						for (int k = 0; k < num5; k++)
						{
							DigGoal.ArtifactData artifactData = digGoal.mArtifacts[digGoal.mCollectedArtifacts[k]];
							float num6 = GlobalMembers.MIN(GlobalMembers.M(0.5f), GlobalMembers.MAX(GlobalMembers.M(0.25f), GlobalMembers.M(0.75f) - (float)num5 / (float)(num3 - GlobalMembers.MS(135)) * GlobalMembers.MS(22f)));
							int num7 = GlobalMembers.MS(210) + (int)((double)(num4 - GlobalMembers.MS(135)) * ((double)k + 0.5) / (double)num5);
							int num8 = GlobalMembers.MS(1010);
							if ((double)num6 < GlobalMembers.M(0.26))
							{
								num7 = GlobalMembers.MS(210) + (int)((double)(num4 - GlobalMembers.MS(135)) * ((double)(k / 2) + 0.5) / (double)((num5 + 1) / 2));
								if (k % 2 == 0)
								{
									num8 -= GlobalMembers.MS(20);
								}
								else
								{
									num8 += GlobalMembers.MS(20);
								}
							}
							g.SetColorizeImages(true);
							g.SetColor(this.mCountupPct);
							Transform transform = new Transform();
							transform.Scale(num6, num6);
							g.DrawImageTransform(GlobalMembersResourcesWP.GetImageById(artifactData.mImageId), transform, (float)num7, (float)num8);
							g.SetColorizeImages(false);
							this.mTreasureRectVector.Add(new Rect((int)((float)num7 - (float)GlobalMembers.MS(80) * num6), (int)((float)num8 - (float)GlobalMembers.MS(80) * num6), (int)((float)GlobalMembers.MS(160) * num6), (int)((float)GlobalMembers.MS(160) * num6)));
						}
					}
					else
					{
						this.mBarRectVector.Add(new Rect(GlobalMembers.MS(198), GlobalMembers.MS(790) + j * GlobalMembers.MS(92), num4 - GlobalMembers.MS(10), GlobalMembers.MS(90)));
					}
				}
			}
			((ImageFont)g.GetFont()).PopLayerColor("Outline");
			((ImageFont)g.GetFont()).PopLayerColor("Glow");
		}

		public void DoTreasureAnim()
		{
			DigGoal digGoal = (DigGoal)((QuestBoard)this.mBoard).mQuestGoal;
			double num = 0.0;
			for (int i = 0; i < 3; i++)
			{
				this.mTreasureDist[i] = 0.0;
				this.mTreasurePauseTicks[i] = 0;
				num += (double)digGoal.mTreasureEarnings[i];
			}
			if (num > 0.0)
			{
				double num2 = GlobalMembers.M(0.1);
				double num3 = GlobalMembers.MIN(1.0, GlobalMembers.M(0.6) + GlobalMembers.M(0.4) * num / (double)GlobalMembers.M(1000000));
				num3 *= GlobalMembers.M(1.2);
				for (int j = 0; j < 3; j++)
				{
					if ((double)digGoal.mTreasureEarnings[j] > 0.0 && (double)digGoal.mTreasureEarnings[j] / num < num2)
					{
						this.mTreasureDist[j] = num2;
						num3 -= num2;
					}
				}
				for (int k = 0; k < 3; k++)
				{
					if (this.mTreasureDist[k] == 0.0)
					{
						this.mTreasureDist[k] = (double)digGoal.mTreasureEarnings[k] / num * num3;
					}
					if (this.mTreasureDist[k] > 0.0)
					{
						this.mTreasurePct[k].SetCurve(GlobalMembers.MP("b-0,1,0.01,1,####        J~### V~###"));
						if (GlobalMembers.M(1) == 1)
						{
							this.mTreasurePct[k].mIncRate *= 1.0 / (this.mTreasureDist[k] / num3);
						}
					}
				}
			}
		}

		public CurvedVal[] mTreasurePct = new CurvedVal[3];

		public double[] mTreasureDist = new double[3];

		public int[] mTreasurePauseTicks = new int[3];

		public List<Rect> mTreasureRectVector = new List<Rect>();

		public List<Rect> mBarRectVector = new List<Rect>();

		public Rect mDiamondRect = default(Rect);

		public EffectsManager mFXManager;
	}
}
