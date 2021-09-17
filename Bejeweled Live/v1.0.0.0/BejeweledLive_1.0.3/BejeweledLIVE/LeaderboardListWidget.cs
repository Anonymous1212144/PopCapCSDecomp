using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Sexy;

namespace BejeweledLIVE
{
	public class LeaderboardListWidget : TableWidget
	{
		public bool Connected { get; protected set; }

		public bool ConnectionPossible { get; protected set; }

		public LeaderboardListWidget(GameApp theApp, int dialogId, DialogListener listener, ScrollWidget scroller)
			: base(dialogId, listener)
		{
			this.mApp = theApp;
			this.mFriendBg = AtlasResources.IMAGE_LEADERBOARD_BG1;
			this.mPlayerBg = AtlasResources.IMAGE_LEADERBOARD_BG3;
			this.mScroller = scroller;
			this.ConnectionPossible = true;
			this.Connected = false;
		}

		public void OnLoadingCompleted()
		{
			this.RefreshList();
		}

		private GameMode GetSelectedGameMode()
		{
			GameMode result = GameMode.MODE_CLASSIC;
			switch (this.CurrentLeaderboardState)
			{
			case LeaderboardState.Action:
				result = GameMode.MODE_TIMED;
				break;
			case LeaderboardState.Classic:
				result = GameMode.MODE_CLASSIC;
				break;
			}
			return result;
		}

		public void Clear()
		{
			this.ConnectionPossible = true;
			this.Connected = false;
			this.mRows.Clear();
			this.mHeight = 0;
			if (this.mScroller != null)
			{
				this.mScroller.CacheDerivedValues();
			}
		}

		public void RefreshList()
		{
			this.mRows.Clear();
			this.mHeight = 0;
			int num = 0;
			if (this.ViewStateDisplayed == LeaderboardViewState.Personal)
			{
				GameMode selectedGameMode = this.GetSelectedGameMode();
				Profile mProfile = this.mApp.mProfile;
				num = mProfile.GetScoreCount((int)selectedGameMode);
			}
			else
			{
				switch (this.CurrentLeaderboardState)
				{
				case LeaderboardState.Action:
					num = LeaderBoardComm.LoadResults(GameMode.MODE_TIMED);
					break;
				case LeaderboardState.Classic:
					num = LeaderBoardComm.LoadResults(GameMode.MODE_CLASSIC);
					break;
				}
			}
			for (int i = 0; i < num; i++)
			{
				base.AddRow(this.mWidth, Constants.mConstants.LeaderboardListWidget_Row_Height);
			}
			if (this.mScroller != null)
			{
				this.mScroller.CacheDerivedValues();
			}
			this.Connected = num > -1;
			this.ConnectionPossible = num != -2;
		}

		public void CenterOnPlayer()
		{
			int num = 0;
			if (this.ViewStateDisplayed == LeaderboardViewState.Friends)
			{
				num = LeaderBoardComm.GetSignedInGamerIndex(this.CurrentLeaderboardState) - 1;
				if (num < 0)
				{
					num = 0;
				}
				if (num >= LeaderBoardComm.GetMaxEntries(this.CurrentLeaderboardState))
				{
					num = LeaderBoardComm.GetMaxEntries(this.CurrentLeaderboardState) - 1;
				}
			}
			this.mScroller.ScrollToPoint(new CGPoint(0f, (float)(num * Constants.mConstants.LeaderboardListWidget_Row_Height)), this.ViewStateDisplayed == LeaderboardViewState.Friends);
		}

		public override void Update()
		{
			base.Update();
			LeaderboardListWidget.loadingRotation = (LeaderboardListWidget.loadingRotation + 0.1f) % 6.28318548f;
		}

		protected override void RowClicked(int rowIndex)
		{
			if (rowIndex < 0)
			{
				return;
			}
			try
			{
				Gamer gamer;
				if (this.ViewStateDisplayed == LeaderboardViewState.Friends)
				{
					gamer = LeaderBoardComm.GetLeaderboardGamer(rowIndex, this.CurrentLeaderboardState);
				}
				else
				{
					gamer = Main.GetGamer();
				}
				if (gamer != null)
				{
					Guide.ShowGamerCard(0, gamer);
				}
			}
			catch (Exception)
			{
			}
		}

		public override void Draw(Graphics g)
		{
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
						if (this.DrawRowRank(j, g, j == this.mTouchHiliteIndex, num3))
						{
							flag = true;
						}
						g.Translate(-trect.mX, -trect.mY);
					}
					num3++;
				}
				num3 = 0;
				flag = true;
				while (flag)
				{
					flag = false;
					for (int k = num; k < num2; k++)
					{
						trect = this.mRows[k];
						g.Translate(trect.mX, trect.mY);
						if (this.DrawRowName(k, g, k == this.mTouchHiliteIndex, num3))
						{
							flag = true;
						}
						g.Translate(-trect.mX, -trect.mY);
					}
					num3++;
				}
				num3 = 0;
				flag = true;
				while (flag)
				{
					flag = false;
					for (int l = num; l < num2; l++)
					{
						trect = this.mRows[l];
						g.Translate(trect.mX, trect.mY);
						if (this.DrawRowScore(l, g, l == this.mTouchHiliteIndex, num3))
						{
							flag = true;
						}
						g.Translate(-trect.mX, -trect.mY);
					}
					num3++;
				}
				for (int m = num; m < num2; m++)
				{
					trect = this.mRows[m];
					g.Translate(trect.mX, trect.mY);
					this.DrawRowGamerPicture(m, g, m == this.mTouchHiliteIndex);
					g.Translate(-trect.mX, -trect.mY);
				}
				g.EndHardwareClip();
			}
		}

		private void DrawRowBackground(int rowIndex, Graphics g, bool hilite)
		{
			Color color = new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			Gamer gamer = Main.GetGamer();
			bool flag = this.ViewStateDisplayed != LeaderboardViewState.Personal && LeaderBoardComm.IsPlayer(gamer, rowIndex, this.CurrentLeaderboardState);
			Image image = (flag ? this.mPlayerBg : this.mFriendBg);
			Image theImage;
			if (flag)
			{
				theImage = AtlasResources.IMAGE_LEADERBOARD_BG3;
			}
			else if (rowIndex % 2 == 0)
			{
				theImage = AtlasResources.IMAGE_LEADERBOARD_BG1;
			}
			else
			{
				theImage = AtlasResources.IMAGE_LEADERBOARD_BG2;
			}
			g.SetColorizeImages(true);
			g.SetColor(color);
			g.DrawImage(theImage, 0, 0, this.mWidth, this.mRows[rowIndex].mHeight);
		}

		private bool DrawRowRank(int rowIndex, Graphics g, bool hilite, int fontLayer)
		{
			new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			Gamer gamer = Main.GetGamer();
			SexyColor aColor = ((this.ViewStateDisplayed != LeaderboardViewState.Personal && LeaderBoardComm.IsPlayer(gamer, rowIndex, this.CurrentLeaderboardState)) ? new SexyColor(255, 255, 255, (int)(255f * this.mOpacity)) : new SexyColor(150, 150, 150, (int)(255f * this.mOpacity)));
			string text;
			if (!this.rankStrings.TryGetValue(rowIndex + 1, ref text))
			{
				text = (rowIndex + 1).ToString();
				this.rankStrings.Add(rowIndex + 1, text);
			}
			g.SetFont(Resources.FONT_BUTTON);
			int theX = Constants.mConstants.LeaderboardListWidget_Rank_X - g.GetFont().StringWidth(text) / 2;
			int theY = this.mRows[rowIndex].mHeight / 2 - g.GetFont().GetHeight() / 2;
			g.SetColor(aColor);
			g.DrawStringLayer(text, theX, theY, fontLayer);
			return g.GetFont().LayerCount - 1 > fontLayer;
		}

		private bool DrawRowName(int rowIndex, Graphics g, bool hilite, int fontLayer)
		{
			new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			Gamer gamer = Main.GetGamer();
			SexyColor aColor = ((this.ViewStateDisplayed != LeaderboardViewState.Personal && LeaderBoardComm.IsPlayer(gamer, rowIndex, this.CurrentLeaderboardState)) ? new SexyColor(255, 255, 255, (int)(255f * this.mOpacity)) : new SexyColor(150, 150, 150, (int)(255f * this.mOpacity)));
			string theString = string.Empty;
			int num = 0;
			int num2 = 0;
			GameMode selectedGameMode = this.GetSelectedGameMode();
			Profile mProfile = this.mApp.mProfile;
			if (this.ViewStateDisplayed == LeaderboardViewState.Personal)
			{
				mProfile.GetScoreInfo((int)selectedGameMode, rowIndex, ref theString, ref num2, ref num);
			}
			else
			{
				theString = LeaderBoardComm.GetLeaderboardName(rowIndex, this.CurrentLeaderboardState);
			}
			g.SetColor(aColor);
			g.SetFont(Resources.FONT_ACHIEVEMENT_NAME);
			g.DrawStringLayer(theString, Constants.mConstants.LeaderboardListWidget_Name_X, Constants.mConstants.LeaderboardListWidget_Name_Y, fontLayer);
			return g.GetFont().LayerCount - 1 > fontLayer;
		}

		private bool DrawRowScore(int rowIndex, Graphics g, bool hilite, int fontLayer)
		{
			new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			Gamer gamer = Main.GetGamer();
			SexyColor aColor = ((this.ViewStateDisplayed != LeaderboardViewState.Personal && LeaderBoardComm.IsPlayer(gamer, rowIndex, this.CurrentLeaderboardState)) ? new SexyColor(255, 255, 255, (int)(255f * this.mOpacity)) : new SexyColor(150, 150, 150, (int)(255f * this.mOpacity)));
			string empty = string.Empty;
			int num = 0;
			int num2 = 0;
			GameMode selectedGameMode = this.GetSelectedGameMode();
			Profile mProfile = this.mApp.mProfile;
			if (this.ViewStateDisplayed == LeaderboardViewState.Personal)
			{
				mProfile.GetScoreInfo((int)selectedGameMode, rowIndex, ref empty, ref num2, ref num);
			}
			else
			{
				num2 = (int)LeaderBoardComm.GetLeaderboardScore(rowIndex, this.CurrentLeaderboardState);
			}
			if (this.ViewStateDisplayed == LeaderboardViewState.Personal)
			{
				string theString = ((num2 > 0) ? GlobalStaticVars.CommaSeperate_(num2) : Strings.NO_HIGH_SCORES);
				g.SetColor(aColor);
				g.SetFont(Resources.FONT_BUTTON);
				float num3 = (float)g.GetFont().StringWidth(theString);
				float num4 = (float)(this.mWidth - Constants.mConstants.LeaderboardListWidget_Score_X);
				if (num3 > num4)
				{
					g.SetScale(num4 / num3);
				}
				g.DrawStringLayer(theString, Constants.mConstants.LeaderboardListWidget_Score_X, Constants.mConstants.LeaderboardListWidget_Score_Y, fontLayer);
				g.SetScale(1f);
			}
			else if (num2 > 0)
			{
				string theString2 = GlobalStaticVars.CommaSeperate_(num2);
				g.SetColor(aColor);
				float num5 = (float)g.GetFont().StringWidth(theString2);
				float num6 = (float)(this.mWidth - Constants.mConstants.LeaderboardListWidget_Score_X) - Constants.mConstants.S(20f);
				if (num5 > num6)
				{
					g.SetScale(num6 / num5);
				}
				g.SetFont(Resources.FONT_BUTTON);
				g.DrawStringLayer(theString2, Constants.mConstants.LeaderboardListWidget_Score_X, Constants.mConstants.LeaderboardListWidget_Score_Y, fontLayer);
				g.SetScale(1f);
			}
			return g.GetFont().LayerCount - 1 > fontLayer;
		}

		private void DrawRowGamerPicture(int rowIndex, Graphics g, bool hilite)
		{
			Color color = new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			SignedInGamer gamer = Main.GetGamer();
			if (this.ViewStateDisplayed == LeaderboardViewState.Personal || !LeaderBoardComm.IsPlayer(gamer, rowIndex, this.CurrentLeaderboardState))
			{
				new SexyColor(150, 150, 150, (int)(255f * this.mOpacity));
			}
			else
			{
				new SexyColor(255, 255, 255, (int)(255f * this.mOpacity));
			}
			this.GetSelectedGameMode();
			Profile mProfile = this.mApp.mProfile;
			Image image;
			if (this.ViewStateDisplayed == LeaderboardViewState.Personal)
			{
				image = LeaderBoardComm.GetGamerImage(gamer);
			}
			else
			{
				image = LeaderBoardComm.GetLeaderboardGamerImage(rowIndex, this.CurrentLeaderboardState);
				LeaderBoardComm.GetLeaderboardScore(rowIndex, this.CurrentLeaderboardState);
			}
			g.SetColor(color);
			int leaderboardListWidget_Picture_X = Constants.mConstants.LeaderboardListWidget_Picture_X;
			g.DrawImage(image, leaderboardListWidget_Picture_X, this.mRows[rowIndex].mHeight / 2 - image.GetHeight() / 2);
			if (image == LeaderBoardComm.UnknownPlayerImage && gamer != null && gamer.IsSignedInToLive)
			{
				Image image_LOADING_RING = AtlasResources.IMAGE_LOADING_RING;
				int num = (int)((float)image.mWidth * 0.5f);
				g.SetColor(color);
				g.DrawImageRotatedScaled(image_LOADING_RING, (float)(leaderboardListWidget_Picture_X + image.mWidth / 2), (float)(this.mRows[rowIndex].mHeight / 2), (double)LeaderboardListWidget.loadingRotation, (float)(image_LOADING_RING.GetWidth() / 2), (float)(image_LOADING_RING.GetHeight() / 2), new TRect?(new TRect(image_LOADING_RING.mS + 1, image_LOADING_RING.mT + 1, image_LOADING_RING.mWidth - 2, image_LOADING_RING.mHeight - 2)), num, num);
			}
		}

		protected override void DrawRow(int rowIndex, Graphics g, bool hilite)
		{
		}

		private const int FRIENDS_GREY_TONE = 150;

		public ScrollWidget mScroller;

		private static float loadingRotation;

		private Dictionary<int, string> rankStrings = new Dictionary<int, string>(100);

		protected GameApp mApp;

		protected Image mFriendBg;

		protected Image mPlayerBg;

		public LeaderboardViewState ViewStateDisplayed;

		public LeaderboardState CurrentLeaderboardState;
	}
}
