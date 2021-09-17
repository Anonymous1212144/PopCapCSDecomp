using System;
using Sexy;

namespace BejeweledLIVE
{
	public class ScoresWidget : InterfaceControl
	{
		public ScoresWidget(GameApp app)
		{
			this.mApp = app;
		}

		public void Setup()
		{
			this.mCategory = ((this.mApp.mProfile.mLastScoreHighGameMode >= 0 && this.mApp.mProfile.mLastScoreHighGameMode < 3) ? this.mApp.mProfile.mLastScoreHighGameMode : 2);
			int num = 0;
			for (int i = 0; i < 10; i++)
			{
				if (this.mApp.mProfile.mGemsCleared >= GameConstants.PREFIX_CUTOFFS[i])
				{
					num = i;
				}
			}
			int num2 = 0;
			if (this.mApp.mProfile.mNumDefaultPuzzlesSolved >= 80)
			{
				num2 += 5;
			}
			else if (this.mApp.mProfile.mNumDefaultPuzzlesSolved >= 70)
			{
				num2 += 4;
			}
			else if (this.mApp.mProfile.mNumDefaultPuzzlesSolved >= 40)
			{
				num2 += 3;
			}
			else if (this.mApp.mProfile.mNumDefaultPuzzlesSolved >= 20)
			{
				num2 += 2;
			}
			else if (this.mApp.mProfile.mNumDefaultPuzzlesSolved >= 1)
			{
				num2++;
			}
			if (this.mApp.mProfile.mHighestLevel[3] >= 100)
			{
				num2 += 3;
			}
			else if (this.mApp.mProfile.mHighestLevel[3] >= 50)
			{
				num2 += 2;
			}
			else if (this.mApp.mProfile.mHighestLevel[3] >= 10)
			{
				num2++;
			}
			if (this.mApp.mProfile.mHighestLevel[0] >= 20)
			{
				num2 += 3;
			}
			else if (this.mApp.mProfile.mHighestLevel[0] >= 10)
			{
				num2 += 2;
			}
			else if (this.mApp.mProfile.mHighestLevel[0] >= 5)
			{
				num2++;
			}
			if (this.mApp.mProfile.mHighestLevel[1] >= 12)
			{
				num2 += 4;
			}
			else if (this.mApp.mProfile.mHighestLevel[1] >= 9)
			{
				num2 += 3;
			}
			else if (this.mApp.mProfile.mHighestLevel[1] >= 6)
			{
				num2 += 2;
			}
			else if (this.mApp.mProfile.mHighestLevel[1] >= 3)
			{
				num2++;
			}
			this.mRank = GameConstants.PREFIX_NAMES[num];
			this.mRank += " ";
			this.mRank += GameConstants.SUFFIX_NAMES[num2];
		}

		public void Layout(int uiStateLayout)
		{
			this.mTabsFrame.mX = GameApp.DIALOGBOX_INTERIOR_INSETS.mLeft + 1;
			this.mTabsFrame.mWidth = this.mWidth - GameApp.DIALOGBOX_INTERIOR_INSETS.mLeft - GameApp.DIALOGBOX_INTERIOR_INSETS.mRight - 2;
			this.mTabsFrame.mY = GameApp.DIALOGBOX_INTERIOR_INSETS.mTop + 1;
			this.mTabsFrame.mHeight = Constants.mConstants.BUTTON_BAR_HEIGHT;
			this.mTextFrame.mX = GameApp.DIALOGBOX_CONTENT_INSETS.mLeft;
			this.mTextFrame.mWidth = this.mWidth - GameApp.DIALOGBOX_CONTENT_INSETS.mLeft - GameApp.DIALOGBOX_CONTENT_INSETS.mRight;
			this.mTextFrame.mY = this.mTabsFrame.mY + this.mTabsFrame.mHeight + 8;
			this.mTextFrame.mHeight = this.mHeight - GameApp.DIALOGBOX_CONTENT_INSETS.mBottom - this.mTextFrame.mY;
		}

		public override void Draw(Graphics g)
		{
			if (this.mApp.IsLandscape())
			{
				this.DrawLandscape(g);
				return;
			}
			this.DrawPortrait(g);
		}

		public override void TouchEnded(_Touch touch)
		{
			int num = (int)touch.location.x;
			int num2 = (int)touch.location.y;
			if (num2 < Constants.mConstants.BUTTON_BAR_OFFSET_Y + Constants.mConstants.BUTTON_BAR_HEIGHT)
			{
				this.mApp.PlaySample(Resources.SOUND_CLICK);
				int num3 = num / (this.mWidth / 3);
				if (num3 == 0)
				{
					this.mCategory = 2;
					return;
				}
				if (num3 == 1)
				{
					this.mCategory = 0;
					return;
				}
				this.mCategory = 1;
			}
		}

		protected void DrawButtonBar(Graphics g)
		{
			int theAlpha = (int)(255f * this.mOpacity);
			SexyColor sexyColor = new SexyColor(255, 255, 255, theAlpha);
			g.SetColorizeImages(true);
			TRect theDest = new TRect(0, 0, this.mWidth, this.mHeight);
			g.SetColor(sexyColor);
			g.DrawImageBox(theDest, AtlasResources.IMAGE_ALERT_BG);
			int mY = this.mTabsFrame.mY;
			int mWidth = this.mTabsFrame.mWidth;
			int num = (mWidth + 2) / 3;
			g.SetFont(Resources.FONT_TEXT);
			int button_BAR_HEIGHT = Constants.mConstants.BUTTON_BAR_HEIGHT;
			int theY = mY + button_BAR_HEIGHT / 2 + g.GetFont().GetAscent() / 2 - 2;
			SexyColor sexyColor2 = new SexyColor(127, 127, 127, theAlpha);
			g.SetColor(new SexyColor(0, 255, 0, (int)(50f * this.mOpacity)));
			g.FillRect(this.mTabsFrame);
			g.SetColor(new SexyColor(0, 0, 0, (int)(60f * this.mOpacity)));
			g.FillRect(this.mTabsFrame.mX + num, mY, 1, button_BAR_HEIGHT);
			g.FillRect(this.mTabsFrame.mX + 2 * num, mY, 1, button_BAR_HEIGHT);
			g.SetColor((this.mCategory == 2) ? sexyColor : sexyColor2);
			g.DrawString(this.Rank, this.mTabsFrame.mX + num / 2 - g.GetFont().StringWidth(this.Rank) / 2, theY);
			g.SetColor((this.mCategory == 0) ? sexyColor : sexyColor2);
			g.DrawString(this.Menu_Play_Classic, this.mTabsFrame.mX + mWidth / 2 - g.GetFont().StringWidth(this.Menu_Play_Classic) / 2, theY);
			g.SetColor((this.mCategory == 1) ? sexyColor : sexyColor2);
			g.DrawString(this.Menu_Play_Action, this.mTabsFrame.mX + 2 * num + num / 2 - g.GetFont().StringWidth(this.Menu_Play_Action) / 2, theY);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
			g.SetColor(new SexyColor(255, 255, 0, (int)(80f * this.mOpacity)));
			int num2 = num - 1;
			int theX;
			switch (this.mCategory)
			{
			case 0:
				theX = this.mTabsFrame.mX + num + 1;
				break;
			case 1:
				theX = this.mTabsFrame.mX + 2 * num + 1;
				num2 -= (this.mApp.IsPortrait() ? 2 : 1);
				break;
			case 2:
				theX = this.mTabsFrame.mX;
				num2++;
				break;
			default:
				theX = 0;
				break;
			}
			g.FillRect(theX, mY, num2, button_BAR_HEIGHT);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.SetColor(new SexyColor(255, 255, 255, (int)(40f * this.mOpacity)));
			g.FillRect(this.mTabsFrame.mX, mY, mWidth, button_BAR_HEIGHT / 2);
		}

		protected void DrawLandscape(Graphics g)
		{
			base.Draw(g);
			this.DrawButtonBar(g);
			new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			SexyColor aColor = new SexyColor(0, 255, 0, (int)(255f * this.mOpacity));
			g.SetFont(Resources.FONT_TEXT);
			int num = Constants.mConstants.BUTTON_BAR_HEIGHT + 50;
			g.SetColor(SexyColor.White);
			int mHeight = g.GetFont().mHeight;
			if (this.mCategory == 2)
			{
				int scoresWidget_aCol1_Rank_Landscape = Constants.mConstants.ScoresWidget_aCol1_Rank_Landscape;
				int scoresWidget_aCol2_Rank_Landscape = Constants.mConstants.ScoresWidget_aCol2_Rank_Landscape;
				g.DrawString(this.Rank + " " + this.mRank, scoresWidget_aCol1_Rank_Landscape, num);
				num += mHeight;
				int num2 = num;
				g.DrawString(Common.StrFormat_("{0}: {1}", new object[]
				{
					this.Best_Score,
					GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mBestScore)
				}), scoresWidget_aCol1_Rank_Landscape, num);
				num += mHeight;
				g.DrawString(Common.StrFormat_("{0}: {1}", new object[]
				{
					this.Gems_Collected,
					GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mGemsCleared)
				}), scoresWidget_aCol1_Rank_Landscape, num);
				num += mHeight;
				g.DrawString(Common.StrFormat_("{0}: x{1}", new object[]
				{
					this.Biggest_Cascade,
					this.mApp.mProfile.mBiggestCascade
				}), scoresWidget_aCol1_Rank_Landscape, num);
				num += mHeight;
				g.DrawString(Common.StrFormat_("{0}: {1}h {2}m", new object[]
				{
					this.Time_Played,
					this.mApp.mProfile.mSecondsPlayed / 3600,
					this.mApp.mProfile.mSecondsPlayed / 60 % 60
				}), scoresWidget_aCol1_Rank_Landscape, num);
				num = num2;
				g.DrawString(Common.StrFormat_("{0}: {1}", new object[]
				{
					this.Biggest_Combo,
					GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mBiggestCombo)
				}), scoresWidget_aCol2_Rank_Landscape, num);
				num += mHeight;
				g.DrawString(Common.StrFormat_("{0}: {1}", new object[]
				{
					this.Power_Gems_Created,
					GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mNumPowerGemsCreated)
				}), scoresWidget_aCol2_Rank_Landscape, num);
				num += mHeight;
				g.DrawString(Common.StrFormat_("{0}: {1}", new object[]
				{
					this.Hyper_Cubes_Created,
					GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mNumHyperGemsCreated)
				}), scoresWidget_aCol2_Rank_Landscape, num);
				num += mHeight;
				return;
			}
			int num3 = ((this.mCategory == 0) ? 0 : 1);
			Profile mProfile = this.mApp.mProfile;
			int scoreCount = mProfile.GetScoreCount(num3);
			if (scoreCount > 0)
			{
				int scoresWidget_aCol1_Scores_Landscape = Constants.mConstants.ScoresWidget_aCol1_Scores_Landscape;
				int scoresWidget_aCol2_Scores_Landscape = Constants.mConstants.ScoresWidget_aCol2_Scores_Landscape;
				int scoresWidget_aCol3_Scores_Landscape = Constants.mConstants.ScoresWidget_aCol3_Scores_Landscape;
				int scoresWidget_aCol4_Scores_Landscape = Constants.mConstants.ScoresWidget_aCol4_Scores_Landscape;
				string theString = this.Name;
				g.DrawString(theString, scoresWidget_aCol2_Scores_Landscape, num);
				theString = this.Score;
				g.DrawString(theString, scoresWidget_aCol3_Scores_Landscape - g.GetFont().StringWidth(theString), num);
				theString = this.Level_Lower_Case;
				g.DrawString(theString, scoresWidget_aCol4_Scores_Landscape - g.GetFont().StringWidth(theString), num);
				num += g.GetFont().mHeight;
				for (int i = 0; i < scoreCount; i++)
				{
					if (mProfile.mRecentlyAdded[num3] == i)
					{
						g.SetColor(aColor);
					}
					int mDispPoints = 0;
					int mDispPoints2 = 0;
					string theString2 = "";
					mProfile.GetScoreInfo(num3, i, ref theString2, ref mDispPoints2, ref mDispPoints);
					theString = Common.StrFormat_("{0}.", new object[] { i + 1 });
					g.DrawString(theString, scoresWidget_aCol1_Scores_Landscape - g.GetFont().StringWidth(theString), num);
					g.DrawString(theString2, scoresWidget_aCol2_Scores_Landscape, num);
					theString = GlobalStaticVars.CommaSeperate_(mDispPoints2);
					g.DrawString(theString, scoresWidget_aCol3_Scores_Landscape - g.GetFont().StringWidth(theString), num);
					theString = GlobalStaticVars.CommaSeperate_(mDispPoints);
					g.DrawString(theString, scoresWidget_aCol4_Scores_Landscape - g.GetFont().StringWidth(theString), num);
					num += mHeight;
					g.SetColor(SexyColor.White);
				}
				return;
			}
			g.SetFont(Resources.FONT_BUTTON);
			this.WriteCenteredLine(g, this.mHeight / 2 - mHeight / 2, this.NO_HIGH_SCORES);
		}

		protected void DrawPortrait(Graphics g)
		{
			base.Draw(g);
			this.DrawButtonBar(g);
			SexyColor aColor = new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			SexyColor aColor2 = new SexyColor(0, 255, 0, (int)(255f * this.mOpacity));
			g.SetFont(Resources.FONT_TEXT);
			int num = Constants.mConstants.BUTTON_BAR_HEIGHT + 50;
			g.SetColor(aColor);
			int mHeight = g.GetFont().mHeight;
			if (this.mCategory == 2)
			{
				int scoresWidget_aCol1_Rank_Portrait = Constants.mConstants.ScoresWidget_aCol1_Rank_Portrait;
				int num2 = this.mWidth - Constants.mConstants.ScoresWidget_aCol2_Rank_Portrait;
				g.DrawString(string.Format("{0}: {1}", this.Rank, this.mRank), scoresWidget_aCol1_Rank_Portrait, num);
				num += mHeight + 4;
				g.DrawString(string.Format("{0}:", this.Best_Score), scoresWidget_aCol1_Rank_Portrait, num);
				string theString = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mBestScore);
				g.DrawString(theString, num2 - g.GetFont().StringWidth(theString), num);
				num += mHeight;
				g.DrawString(string.Format("{0}:", this.Gems_Collected), scoresWidget_aCol1_Rank_Portrait, num);
				theString = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mGemsCleared);
				g.DrawString(theString, num2 - g.GetFont().StringWidth(theString), num);
				num += mHeight;
				g.DrawString(string.Format("{0}:", this.Biggest_Cascade), scoresWidget_aCol1_Rank_Portrait, num);
				theString = Common.StrFormat_("x{0}", new object[] { GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mBiggestCascade) });
				g.DrawString(theString, num2 - g.GetFont().StringWidth(theString), num);
				num += mHeight;
				g.DrawString(string.Format("{0}:", this.Biggest_Combo), scoresWidget_aCol1_Rank_Portrait, num);
				theString = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mBiggestCombo);
				g.DrawString(theString, num2 - g.GetFont().StringWidth(theString), num);
				num += mHeight;
				g.DrawString(string.Format("{0}:", this.Power_Gems_Created), scoresWidget_aCol1_Rank_Portrait, num);
				theString = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mNumPowerGemsCreated);
				g.DrawString(theString, num2 - g.GetFont().StringWidth(theString), num);
				num += mHeight;
				g.DrawString(string.Format("{0}:", this.Hyper_Cubes_Created), scoresWidget_aCol1_Rank_Portrait, num);
				theString = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mNumHyperGemsCreated);
				g.DrawString(theString, num2 - g.GetFont().StringWidth(theString), num);
				num += mHeight;
				g.DrawString(string.Format("{0}:", this.Time_Played), scoresWidget_aCol1_Rank_Portrait, num);
				theString = Common.StrFormat_("{0}h {1}m", new object[]
				{
					this.mApp.mProfile.mSecondsPlayed / 3600,
					this.mApp.mProfile.mSecondsPlayed / 60 % 60
				});
				g.DrawString(theString, num2 - g.GetFont().StringWidth(theString), num);
				num += mHeight;
				return;
			}
			int num3 = ((this.mCategory == 0) ? 0 : 1);
			Profile mProfile = this.mApp.mProfile;
			int scoreCount = mProfile.GetScoreCount(num3);
			if (scoreCount > 0)
			{
				int scoresWidget_aCol1_Scores_Portrait = Constants.mConstants.ScoresWidget_aCol1_Scores_Portrait;
				int scoresWidget_aCol2_Scores_Portrait = Constants.mConstants.ScoresWidget_aCol2_Scores_Portrait;
				int scoresWidget_aCol3_Scores_Portrait = Constants.mConstants.ScoresWidget_aCol3_Scores_Portrait;
				int scoresWidget_aCol4_Scores_Portrait = Constants.mConstants.ScoresWidget_aCol4_Scores_Portrait;
				num += 20;
				string theString = this.Name;
				g.DrawString(theString, scoresWidget_aCol2_Scores_Portrait, num);
				theString = this.Score;
				g.DrawString(theString, scoresWidget_aCol3_Scores_Portrait - g.GetFont().StringWidth(theString), num);
				theString = this.Level_Lower_Case;
				g.DrawString(theString, scoresWidget_aCol4_Scores_Portrait - g.GetFont().StringWidth(theString), num);
				num += g.GetFont().mHeight;
				for (int i = 0; i < scoreCount; i++)
				{
					if (mProfile.mRecentlyAdded[num3] == i)
					{
						g.SetColor(aColor2);
					}
					int mDispPoints = 0;
					int mDispPoints2 = 0;
					string theString2 = "";
					mProfile.GetScoreInfo(num3, i, ref theString2, ref mDispPoints2, ref mDispPoints);
					theString = Common.StrFormat_("{0}.", new object[] { i + 1 });
					g.DrawString(theString, scoresWidget_aCol1_Scores_Portrait - g.GetFont().StringWidth(theString), num);
					Graphics g2 = GlobalStaticVars.g;
					g.Reset();
					g2.SetClipRect(scoresWidget_aCol2_Scores_Portrait, 0, 120, this.mHeight);
					g2.DrawString(theString2, scoresWidget_aCol2_Scores_Portrait, num);
					theString = GlobalStaticVars.CommaSeperate_(mDispPoints2);
					g.DrawString(theString, scoresWidget_aCol3_Scores_Portrait - g.GetFont().StringWidth(theString), num);
					theString = GlobalStaticVars.CommaSeperate_(mDispPoints);
					g.DrawString(theString, scoresWidget_aCol4_Scores_Portrait - g.GetFont().StringWidth(theString), num);
					num += mHeight;
					g.SetColor(SexyColor.White);
				}
				return;
			}
			g.SetFont(Resources.FONT_BUTTON);
			this.WriteCenteredLine(g, this.mHeight / 2 - mHeight / 2, this.NO_HIGH_SCORES);
		}

		protected GameApp mApp;

		protected int mCategory;

		protected string mRank = string.Empty;

		protected TRect mTabsFrame = default(TRect);

		protected TRect mTextFrame = default(TRect);

		private string Best_Score = Strings.Best_Score;

		private string Gems_Collected = Strings.Gems_Collected;

		private string Biggest_Cascade = Strings.Biggest_Cascade;

		private string Time_Played = Strings.Time_Played;

		private string Biggest_Combo = Strings.Biggest_Combo;

		private string Power_Gems_Created = Strings.Power_Gems_Created;

		private string Hyper_Cubes_Created = Strings.Hyper_Cubes_Created;

		private string Rank = Strings.Rank;

		private string Menu_Play_Classic = Strings.Menu_Play_Classic;

		private string Menu_Play_Action = Strings.Menu_Play_Action;

		private string Level_Lower_Case = Strings.Level_Lower_Case;

		private string Name = Strings.Name;

		private string Score = Strings.Score;

		private string NO_HIGH_SCORES = Strings.NO_HIGH_SCORES;

		protected enum Tabs
		{
			CLASSIC,
			ACTION,
			RANK,
			CAT_MAX
		}
	}
}
