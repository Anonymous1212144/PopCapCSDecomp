using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy;

namespace BejeweledLIVE
{
	public class AchievementListWidget : TableWidget
	{
		public AchievementListWidget(GameApp aApp, int dialogId, DialogListener listener)
			: base(dialogId, listener)
		{
			this.mApp = aApp;
			this.mBg1 = AtlasResources.IMAGE_LEADERBOARD_BG1;
			this.mBg2 = AtlasResources.IMAGE_LEADERBOARD_BG2;
		}

		public void RefreshList()
		{
			this.mRows.Clear();
			int numberOfAchievements = Achievements.GetNumberOfAchievements();
			for (int i = 0; i < numberOfAchievements; i++)
			{
				base.AddRow(this.mWidth, Constants.mConstants.AchievementScreen_Row_Height);
			}
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			if (this.mRows.Count > 0)
			{
				TRect trect = default(TRect);
				g.HardwareClip();
				int num = Math.Max(0, -this.mY / this.rowHeight);
				int num2 = Math.Min(this.mRows.Count, num + 2 + g.GetClipRect().mHeight / this.rowHeight);
				for (int i = num; i < num2; i++)
				{
					trect = this.mRows[i];
					g.Translate(trect.mX, trect.mY);
					this.DrawRowBackground(i, g, i == this.mTouchHiliteIndex);
					g.Translate(-trect.mX, -trect.mY);
				}
				int num3 = 0;
				bool flag = true;
				while (flag)
				{
					flag = false;
					for (int j = num; j < num2; j++)
					{
						trect = this.mRows[j];
						g.Translate(trect.mX, trect.mY);
						if (this.DrawRowText(j, g, j == this.mTouchHiliteIndex, num3))
						{
							flag = true;
						}
						g.Translate(-trect.mX, -trect.mY);
					}
					num3++;
				}
				for (int k = num; k < num2; k++)
				{
					trect = this.mRows[k];
					g.Translate(trect.mX, trect.mY);
					this.DrawRowImage(k, g, k == this.mTouchHiliteIndex);
					g.Translate(-trect.mX, -trect.mY);
				}
				for (int l = num; l < num2; l++)
				{
					trect = this.mRows[l];
					g.Translate(trect.mX, trect.mY);
					this.DrawRowLocks(l, g, l == this.mTouchHiliteIndex);
					g.Translate(-trect.mX, -trect.mY);
				}
				g.EndHardwareClip();
			}
		}

		private void DrawRowBackground(int rowIndex, Graphics g, bool hilite)
		{
			Color color;
			color..ctor(255, 255, 255, (int)(255f * this.mOpacity));
			g.SetColorizeImages(true);
			g.SetColor(color);
			Image theImage;
			if (rowIndex % 2 == 0)
			{
				theImage = this.mBg1;
			}
			else
			{
				theImage = this.mBg2;
			}
			g.DrawImage(theImage, 0, 0, this.mWidth, this.mRows[rowIndex].mHeight);
		}

		private bool DrawRowText(int rowIndex, Graphics g, bool hilite, int fontLayer)
		{
			Color color;
			color..ctor(255, 255, 255, (int)(255f * this.mOpacity));
			AchievementItem achievementItem = Achievements.GetAchievementItem((Achievements.AchievementId)rowIndex);
			g.SetColorizeImages(true);
			if (achievementItem.IsEarned)
			{
				g.SetColor(color);
			}
			else
			{
				g.SetColor(new Color(255, 255, 255, (int)(255f * this.mOpacity * 0.5f)));
			}
			g.SetScale(1f);
			g.SetFont(Resources.FONT_ACHIEVEMENT_NAME);
			g.DrawStringLayer(achievementItem.Name, Constants.mConstants.AchievementScreen_Description_Box.mX, Constants.mConstants.AchievementScreen_Name_Y, fontLayer);
			g.SetScale(0.8f);
			g.WriteWordWrappedLayer(Constants.mConstants.AchievementScreen_Description_Box, achievementItem.Description, -1, -1, fontLayer, true);
			g.SetScale(1f);
			if (!AchievementListWidget.gamerScoreStrings.ContainsKey(achievementItem.GamerScore))
			{
				AchievementListWidget.gamerScoreStrings.Add(achievementItem.GamerScore, achievementItem.GamerScore.ToString());
			}
			g.DrawStringLayer(AchievementListWidget.gamerScoreStrings[achievementItem.GamerScore], Constants.mConstants.AchievementScreen_Score_X, Constants.mConstants.AchievementScreen_Name_Y, fontLayer);
			return g.GetFont().LayerCount - 1 > fontLayer;
		}

		private void DrawRowImage(int rowIndex, Graphics g, bool hilite)
		{
			Color color;
			color..ctor(255, 255, 255, (int)(255f * this.mOpacity));
			AchievementItem achievementItem = Achievements.GetAchievementItem((Achievements.AchievementId)rowIndex);
			g.SetColorizeImages(true);
			if (achievementItem.IsEarned)
			{
				g.SetColor(color);
			}
			else
			{
				g.SetColor(new Color(255, 255, 255, (int)(255f * this.mOpacity * 0.5f)));
			}
			g.DrawImage(achievementItem.AchievementImage, (int)((float)Constants.mConstants.AchievementScreen_Image_X * Constants.mConstants.AchievementScreen_Picture_Scale), (int)((float)(this.mRows[rowIndex].mHeight / 2) - (float)achievementItem.AchievementImage.mHeight * Constants.mConstants.AchievementScreen_Picture_Scale / 2f), (int)((float)achievementItem.AchievementImage.mWidth * Constants.mConstants.AchievementScreen_Picture_Scale), (int)((float)achievementItem.AchievementImage.mHeight * Constants.mConstants.AchievementScreen_Picture_Scale));
		}

		private void DrawRowLocks(int rowIndex, Graphics g, bool hilite)
		{
			Color color;
			color..ctor(255, 255, 255, (int)(255f * this.mOpacity));
			AchievementItem achievementItem = Achievements.GetAchievementItem((Achievements.AchievementId)rowIndex);
			if (!achievementItem.IsEarned)
			{
				g.SetColor(color);
				Image image_LOCK = AtlasResources.IMAGE_LOCK;
				g.DrawImage(image_LOCK, (int)((float)Constants.mConstants.AchievementScreen_Image_X + (float)achievementItem.AchievementImage.mWidth * Constants.mConstants.AchievementScreen_Picture_Scale / 2f - (float)(image_LOCK.mWidth / 2)), this.mRows[rowIndex].mHeight / 2 - image_LOCK.mHeight / 2);
			}
		}

		protected override void DrawRow(int rowIndex, Graphics g, bool hilite)
		{
		}

		private const float LOCKED_ACHIEVEMENT_ALPHA = 0.5f;

		private const float DESCRIPTION_SCALE = 0.8f;

		private GameApp mApp;

		private Image mBg1;

		private Image mBg2;

		private static Dictionary<int, string> gamerScoreStrings = new Dictionary<int, string>();
	}
}
