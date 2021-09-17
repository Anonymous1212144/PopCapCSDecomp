using System;
using Sexy;

namespace BejeweledLIVE
{
	public class LiveRank : InterfaceControl
	{
		public LiveRank(GameApp app)
		{
			this.mApp = app;
			this.mLabel = Strings.Rank;
			this.mFrame = new FrameWidget();
			this.AddWidget(this.mFrame);
		}

		public void Setup()
		{
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
			this.mRank += GameConstants.SUFFIX_NAMES[Math.Min(GameConstants.SUFFIX_NAMES.Length - 1, num2)];
		}

		public override void Draw(Graphics g)
		{
			if (this.mOpacity == 0f)
			{
				return;
			}
			base.DeferOverlay();
		}

		public override void DrawOverlay(Graphics g)
		{
			if (this.mApp.IsLandscape())
			{
				this.DrawLandscape(g);
				return;
			}
			this.DrawPortrait(g);
		}

		public override void SetOpacity(float opacity)
		{
			base.SetOpacity(opacity);
			this.mFrame.SetOpacity(opacity);
		}

		public override void FadeIn(float startSeconds, float endSeconds)
		{
			base.FadeIn(startSeconds, endSeconds);
			this.mFrame.FadeIn(startSeconds, endSeconds);
		}

		public override void FadeOut(float startSeconds, float endSeconds)
		{
			base.FadeOut(startSeconds, endSeconds);
			this.mFrame.FadeOut(startSeconds, endSeconds);
		}

		public override void SetVisible(bool isVisible)
		{
			base.SetVisible(isVisible);
			this.mFrame.SetVisible(isVisible);
		}

		public void Layout()
		{
			this.Setup();
			this.mFrame.SetLabel(this.mLabel);
			this.bounds = new TRect(0, AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() - Constants.mConstants.LiveRank_Frame_Offset_Y, this.mWidth, this.mHeight - AtlasResources.IMAGE_SUB_HEADER_MID.GetHeight() + Constants.mConstants.LiveRank_Frame_Offset_Y);
			this.bounds = new TRect(0, 0, this.mWidth, this.mHeight);
			this.mFrame.SetColor(Constants.FRAME_BACK_COLOUR(1f));
			this.mFrame.Resize(this.bounds);
			this.biggestCascade = Common.StrFormat_("x{0}", new object[] { GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mBiggestCascade) });
			this.gemsCleared = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mGemsCleared);
			this.bestScore = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mBestScore);
			this.biggestCombo = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mBiggestCombo);
			this.powerGemsCreated = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mNumPowerGemsCreated);
			this.hypercubesCreated = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mNumHyperGemsCreated);
			this.starGemsCreated = GlobalStaticVars.CommaSeperate_(this.mApp.mProfile.mNumLaserGemsCreated);
			this.timePlayed = Common.StrFormat_(this.Played_Time_h_m, new object[]
			{
				this.mApp.mProfile.mSecondsPlayed / 3600,
				this.mApp.mProfile.mSecondsPlayed / 60 % 60
			});
		}

		public override void TouchEnded(_Touch touch)
		{
		}

		protected void DrawButtonBar(Graphics g)
		{
			int theAlpha = (int)(255f * this.mOpacity);
			SexyColor aColor = new SexyColor(255, 255, 255, theAlpha);
			g.SetColorizeImages(true);
			g.SetColor(aColor);
		}

		protected void DrawLandscape(Graphics g)
		{
			base.Draw(g);
			SexyColor aColor = new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			new SexyColor(0, 255, 0, (int)(255f * this.mOpacity));
			g.SetFont(Resources.FONT_HEADING);
			this.DrawButtonBar(g);
			g.SetFont(Resources.FONT_TEXT);
			int num = Constants.mConstants.LiveRank_Text_Y_Landscape;
			g.SetColor(aColor);
			int num2 = -g.GetFont().mHeight + Constants.mConstants.LiveRank_Row_Distance_Landscape;
			int scoresWidget_aCol1_Rank_Landscape = Constants.mConstants.ScoresWidget_aCol1_Rank_Landscape;
			int num3 = this.mWidth - Constants.mConstants.ScoresWidget_aCol2_Rank_Landscape;
			g.DrawString(this.RANK_Rank, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.mRank, num3 - g.GetFont().StringWidth(this.mRank), num);
			num += num2;
			g.DrawString(this.RANK_BestScore, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.bestScore, num3 - g.GetFont().StringWidth(this.bestScore), num);
			num += num2;
			g.DrawString(this.RANK_GemsCollected, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.gemsCleared, num3 - g.GetFont().StringWidth(this.gemsCleared), num);
			num += num2;
			g.DrawString(this.RANK_BiggestCascade, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.biggestCascade, num3 - g.GetFont().StringWidth(this.biggestCascade), num);
			num += num2;
			g.DrawString(this.RANK_BiggestCombo, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.biggestCombo, num3 - g.GetFont().StringWidth(this.biggestCombo), num);
			num += num2;
			g.DrawString(this.RANK_PowerGemsCreated, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.powerGemsCreated, num3 - g.GetFont().StringWidth(this.powerGemsCreated), num);
			num += num2;
			g.DrawString(this.RANK_HyperCubesCreated, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.hypercubesCreated, num3 - g.GetFont().StringWidth(this.hypercubesCreated), num);
			num += num2;
			g.DrawString(this.RANK_LaserGemsCreated, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.starGemsCreated, num3 - g.GetFont().StringWidth(this.starGemsCreated), num);
			num += num2;
			g.DrawString(this.RANK_TimePlayed, scoresWidget_aCol1_Rank_Landscape, num);
			g.DrawString(this.timePlayed, num3 - g.GetFont().StringWidth(this.timePlayed), num);
			num += num2;
		}

		protected void DrawPortrait(Graphics g)
		{
			base.Draw(g);
			SexyColor aColor = new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			new SexyColor(0, 255, 0, (int)(255f * this.mOpacity));
			g.SetFont(Resources.FONT_HEADING);
			this.DrawButtonBar(g);
			g.SetColor(aColor);
			g.SetFont(Resources.FONT_TEXT);
			int num = Constants.mConstants.LiveRank_Text_Y_Portrait;
			g.SetColor(aColor);
			int num2 = -g.GetFont().mHeight + Constants.mConstants.LiveRank_Row_Distance_Portrait;
			int scoresWidget_aCol1_Rank_Portrait = Constants.mConstants.ScoresWidget_aCol1_Rank_Portrait;
			int num3 = this.mWidth - Constants.mConstants.ScoresWidget_aCol2_Rank_Portrait;
			g.DrawString(this.RANK_Rank, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.mRank, num3 - g.GetFont().StringWidth(this.mRank), num);
			num += num2;
			g.DrawString(this.RANK_BestScore, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.bestScore, num3 - g.GetFont().StringWidth(this.bestScore), num);
			num += num2;
			g.DrawString(this.RANK_GemsCollected, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.gemsCleared, num3 - g.GetFont().StringWidth(this.gemsCleared), num);
			num += num2;
			g.DrawString(this.RANK_BiggestCascade, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.biggestCascade, num3 - g.GetFont().StringWidth(this.biggestCascade), num);
			num += num2;
			g.DrawString(this.RANK_BiggestCombo, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.biggestCombo, num3 - g.GetFont().StringWidth(this.biggestCombo), num);
			num += num2;
			g.DrawString(this.RANK_PowerGemsCreated, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.powerGemsCreated, num3 - g.GetFont().StringWidth(this.powerGemsCreated), num);
			num += num2;
			g.DrawString(this.RANK_HyperCubesCreated, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.hypercubesCreated, num3 - g.GetFont().StringWidth(this.hypercubesCreated), num);
			num += num2;
			g.DrawString(this.RANK_LaserGemsCreated, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.starGemsCreated, num3 - g.GetFont().StringWidth(this.starGemsCreated), num);
			num += num2;
			g.DrawString(this.RANK_TimePlayed, scoresWidget_aCol1_Rank_Portrait, num);
			g.DrawString(this.timePlayed, num3 - g.GetFont().StringWidth(this.timePlayed), num);
			num += num2;
		}

		public string GetLabel()
		{
			return this.mLabel;
		}

		protected string mLabel = string.Empty;

		protected ButtonListener mListener;

		protected GameApp mApp;

		protected string mRank = string.Empty;

		private FrameWidget mFrame;

		private TRect bounds;

		private string RANK_Rank = Strings.RANK_Rank;

		private string RANK_BestScore = Strings.RANK_BestScore;

		private string RANK_BiggestCascade = Strings.RANK_BiggestCascade;

		private string RANK_BiggestCombo = Strings.RANK_BiggestCombo;

		private string RANK_GemsCollected = Strings.RANK_GemsCollected;

		private string RANK_HyperCubesCreated = Strings.RANK_HyperCubesCreated;

		private string RANK_LaserGemsCreated = Strings.RANK_LaserGemsCreated;

		private string RANK_PowerGemsCreated = Strings.RANK_PowerGemsCreated;

		private string RANK_TimePlayed = Strings.RANK_TimePlayed;

		private string Played_Time_h_m = Strings.Played_Time_h_m;

		private string biggestCascade;

		private string gemsCleared;

		private string bestScore;

		private string biggestCombo;

		private string powerGemsCreated;

		private string hypercubesCreated;

		private string starGemsCreated;

		private string timePlayed;

		protected enum Tabs
		{
			FRIENDS,
			MY_SCORE,
			OVERALL,
			CAT_MAX
		}
	}
}
