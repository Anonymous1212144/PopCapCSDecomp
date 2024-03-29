﻿using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;
using ZumasRevenge.Achievement;

namespace ZumasRevenge
{
	public class AchievementsPages : Widget
	{
		public AchievementsPages(Achievements theAchievements)
		{
			this.mAchievements = theAchievements;
			this.mNumPages = 0;
			this.mHeaderFont = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_YELLOW);
			this.mStatsFont = Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BLACK_GLOW);
			this.mDescFont = Res.GetFontByID(ResID.FONT_SHAGEXOTICA38_BLACK);
			this.mPointFont = Res.GetFontByID(ResID.FONT_SHAGLOUNGE45_GAUNTLET);
			this.mText = new List<AchievementText>();
			for (int i = 0; i < this.mText.Count; i++)
			{
				this.mText[i].mAlpha = 255f;
			}
			this.Resize(0, 0, this.IMAGE_UI_LEADERBOARDS_SHADOW.GetWidth(), (this.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() + 30) * this.mNumPages);
		}

		public int NumPages()
		{
			return this.mNumPages;
		}

		public void AddPage()
		{
			int num = (this.IMAGE_UI_LEADERBOARDS_SHADOW.GetHeight() - 40) * this.mNumPages;
			this.SetupAchievementsText(ref num);
			this.mNumPages++;
		}

		public override void Draw(Graphics g)
		{
			if (GameApp.gApp.mBambooTransition != null && GameApp.gApp.mBambooTransition.IsInProgress())
			{
				return;
			}
			int num = 8;
			int num2 = 0;
			int num3 = 0;
			Graphics3D graphics3D = g.Get3D();
			graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_TEST_OUTSIDE);
			for (int i = 0; i < this.mText.Count; i++)
			{
				if (Localization.GetCurrentLanguage() == Localization.LanguageType.Language_RU || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_PL)
				{
					num = -2;
					num2 = -4;
				}
				if (Localization.GetCurrentLanguage() == Localization.LanguageType.Language_RU && num3++ != 10)
				{
					num = 4;
					num2 = 0;
				}
				AchievementText achievementText = this.mText[i];
				int num4 = 0;
				int num5 = 0;
				int num6 = 96;
				if (achievementText.mIcon != null)
				{
					num4 = num6 + 5;
					if (!achievementText.mUnlocked)
					{
						g.SetColor(120, 120, 120, 140);
						g.SetColorizeImages(true);
					}
					g.DrawImage(achievementText.mIcon, achievementText.mX, achievementText.mY, num6, num6);
					g.SetColorizeImages(false);
				}
				if (achievementText.mValueStr.Length == 0)
				{
					g.SetFont(this.mHeaderFont);
					if (!achievementText.mUnlocked)
					{
						g.SetColor(120, 120, 120, 140);
					}
					else
					{
						g.SetColor(0, 0, 0);
					}
					g.DrawString(achievementText.mHeaderStr, num4 + achievementText.mX + (int)this.mAchievements.mXOff, num5 + achievementText.mY + g.GetFont().GetAscent());
					g.SetFont(this.mDescFont);
					if (!achievementText.mUnlocked)
					{
						g.SetColor(120, 120, 120, 140);
					}
					else
					{
						g.SetColor(0, 0, 0);
					}
					Rect theRect = new Rect(num4 + achievementText.mX + (int)this.mAchievements.mXOff, num + achievementText.mY + g.GetFont().GetAscent(), 360, this.mDescFont.GetHeight() * 2);
					g.WriteWordWrapped(theRect, achievementText.mDescStr, 25 + num2, -1);
					g.SetFont(this.mPointFont);
					if (!achievementText.mUnlocked)
					{
						g.SetColor(120, 120, 120, 140);
					}
					else
					{
						g.SetColor(255, 249, 255, 255);
					}
					g.DrawString(achievementText.mPointStr, num4 + achievementText.mX + (int)this.mAchievements.mXOff, 15 + achievementText.mY + g.GetFont().GetAscent());
					g.SetColorizeImages(false);
				}
				else
				{
					g.SetFont(this.mStatsFont);
					g.SetColor(0, 0, 0);
					g.WriteString(achievementText.mValueStr, num4 + achievementText.mX + Common._DS(Common._M(20)) + (int)this.mAchievements.mXOff, num5 + achievementText.mY + this.mStatsFont.GetAscent(), this.mWidth, -1);
				}
			}
			int num7 = this.NumPages() - 1;
			int num8 = this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30;
			int theY = (this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetHeight() - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetHeight()) / 2;
			for (int j = 0; j < num7; j++)
			{
				g.DrawImage(this.IMAGE_UI_CHALLENGESCREEN_DIVIDER, num8 - this.IMAGE_UI_CHALLENGESCREEN_DIVIDER.GetWidth(), theY);
				num8 += this.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES.GetWidth() + 30;
			}
			graphics3D.SetMasking(Graphics3D.EMaskMode.MASKMODE_NONE);
		}

		public override void Update()
		{
			float num = Common._M(10f);
			for (int i = 0; i < this.mText.size<AchievementText>(); i++)
			{
				AchievementText achievementText = this.mText[i];
				if (achievementText.mFadeIn && achievementText.mAlpha < 255f)
				{
					this.MarkDirty();
					achievementText.mAlpha += num;
					if (achievementText.mAlpha > 255f)
					{
						achievementText.mAlpha = 255f;
					}
				}
				else if (!achievementText.mFadeIn && achievementText.mAlpha > 0f)
				{
					this.MarkDirty();
					achievementText.mAlpha -= num;
					if (achievementText.mAlpha <= 0f)
					{
						this.mText.RemoveAt(i);
						i--;
					}
				}
			}
		}

		private void SetupAchievementsText(ref int theStartY)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			GameApp.gApp.mUserProfile.m_AchievementMgr.getAchievementsInfo(ref num2, ref num, ref num4, ref num3);
			int num5 = Common._DS(Common._M(80));
			int num6 = Common._DS(Common._M(200));
			int num7 = Common._DS(Common._M(0));
			num7 += 32;
			for (int i = 0; i < 3; i++)
			{
				int num8 = i + this.mNumPages * 3;
				if (num8 >= num)
				{
					return;
				}
				this.mText.Add(new AchievementText());
				AchievementText achievementText = this.mText.back<AchievementText>();
				AchievementEntry achievementEntry = GameApp.gApp.mUserProfile.m_AchievementMgr.GetAchievementEntry((EAchievementType)num8);
				achievementText.mIcon = Res.GetImageByID((ResID)achievementEntry.m_IconResID);
				achievementText.mUnlocked = achievementEntry.m_Unlocked;
				achievementText.mHeaderStr = TextManager.getInstance().getString(achievementEntry.m_NameResID).ToUpper();
				achievementText.mValueStr = "";
				achievementText.mDescStr = TextManager.getInstance().getString(achievementEntry.m_DescriptionResID);
				achievementText.mX = num5 + 10;
				achievementText.mY = num7 + theStartY;
				this.mText.Add(new AchievementText());
				achievementText = this.mText.back<AchievementText>();
				achievementText.mUnlocked = achievementEntry.m_Unlocked;
				achievementText.mHeaderStr = "";
				achievementText.mValueStr = "";
				achievementText.mPointStr = string.Format("{0}G", achievementEntry.m_GPoints);
				achievementText.mX = num5 + 495;
				achievementText.mY = num7 + theStartY + 10;
				num7 += num6;
			}
		}

		private List<AchievementText> mText;

		private Font mHeaderFont;

		private Font mStatsFont;

		private Font mDescFont;

		private Font mPointFont;

		private Achievements mAchievements;

		public int mNumPages;

		private Image IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_TAPESTRY_LEAVES);

		private Image IMAGE_UI_CHALLENGESCREEN_DIVIDER = Res.GetImageByID(ResID.IMAGE_UI_CHALLENGESCREEN_DIVIDER);

		public Image IMAGE_UI_LEADERBOARDS_SHADOW = Res.GetImageByID(ResID.IMAGE_UI_LEADERBOARDS_SHADOW2);
	}
}
