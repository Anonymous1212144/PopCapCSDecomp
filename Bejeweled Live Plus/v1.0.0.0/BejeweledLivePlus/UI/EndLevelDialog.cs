using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class EndLevelDialog : Bej3Dialog
	{
		public EndLevelDialog(Board theBoard)
			: base(38, true, "", "", "", 0, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.BUTTON_TYPE_LONG, Bej3ButtonType.TOP_BUTTON_TYPE_CLOSED)
		{
			this.mBoard = theBoard;
			this.mPointsBreakdown = this.mBoard.mPointsBreakdown;
			for (int i = 0; i < 40; i++)
			{
				this.mGameStats[i] = this.mBoard.mGameStats[i];
			}
			this.mPoints = this.mBoard.mPoints;
			this.mLevel = this.mBoard.mLevel;
			this.mPointMultiplier = this.mBoard.mPointMultiplier;
			this.mCountupPct.SetCurve(GlobalMembers.MP("b+0,1,0.016667,1,####  M#1^;       S~TEC"));
			this.Resize(GlobalMembers.MS(0), GlobalMembers.MS(0), GlobalMembers.MS(1600), GlobalMembers.MS(1200));
			this.mContentInsets.mBottom = GlobalMembers.MS(60);
			this.mFlushPriority = 100;
			this.mAllowDrag = false;
			this.mRankBar = new RankBarWidget(1195, this.mBoard);
			this.mRankBar.Move(GlobalMembers.MS(200), GlobalMembers.MS(240));
			this.AddWidget(this.mRankBar);
		}

		public override void Dispose()
		{
			this.RemoveAllWidgets(true, false);
			base.Dispose();
		}

		public override void ButtonDepress(int theId)
		{
			base.ButtonDepress(theId);
			if (theId == 0)
			{
				Board board = GlobalMembers.gApp.mBoard;
				if (board != null)
				{
					this.Kill();
					board.BackToMenu();
					return;
				}
			}
			else if (theId == 1)
			{
				Board board2 = GlobalMembers.gApp.mBoard;
				if (board2 != null && board2.mBoardType == EBoardType.eBoardType_Quest)
				{
					this.Kill();
					return;
				}
			}
			else if (theId == 2)
			{
				Board board3 = GlobalMembers.gApp.mBoard;
				if (board3 != null)
				{
					this.Kill();
					board3.Init();
					board3.NewGame();
					this.mWidgetManager.SetFocus(board3);
				}
			}
		}

		public override void Update()
		{
			if (GlobalMembers.gApp.GetDialog(39) != null)
			{
				return;
			}
			base.Update();
			this.mCountupPct.IncInVal();
		}

		public override void Draw(Graphics g)
		{
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), "Main", new Color(255, 255, 255, 255));
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), "OUTLINE", new Color(255, 255, 255, 255));
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), "GLOW", new Color(255, 255, 255, 255));
			g.DrawImageBox(new Rect(GlobalMembers.MS(110), GlobalMembers.MS(0), GlobalMembers.MS(1380), GlobalMembers.MS(1200)), GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIALOG);
			g.SetColor(new Color(-1));
			g.SetFont(GlobalMembersResources.FONT_HEADER);
			((ImageFont)g.GetFont()).PushLayerColor("Main", new Color(GlobalMembers.M(8931352)));
			((ImageFont)g.GetFont()).PushLayerColor("LAYER_2", new Color(GlobalMembers.M(15253648)));
			((ImageFont)g.GetFont()).PushLayerColor("LAYER_3", new Color(0, 0, 0, 0));
			g.WriteString(GlobalMembers._ID("Final Score:", 236), GlobalMembers.MS(800), GlobalMembers.MS(140));
			((ImageFont)g.GetFont()).PopLayerColor("Main");
			((ImageFont)g.GetFont()).PopLayerColor("LAYER_2");
			((ImageFont)g.GetFont()).PopLayerColor("LAYER_3");
			((ImageFont)g.GetFont()).PushLayerColor("Main", new Color(GlobalMembers.M(16777215)));
			((ImageFont)g.GetFont()).PushLayerColor("LAYER_2", new Color(GlobalMembers.M(11558960)));
			((ImageFont)g.GetFont()).PushLayerColor("LAYER_3", new Color(0, 0, 0, 0));
			g.WriteString(SexyFramework.Common.CommaSeperate((int)((double)this.mPoints * this.mCountupPct)), GlobalMembers.MS(800), GlobalMembers.MS(220));
			((ImageFont)g.GetFont()).PopLayerColor("Main");
			((ImageFont)g.GetFont()).PopLayerColor("LAYER_2");
			((ImageFont)g.GetFont()).PopLayerColor("LAYER_3");
			g.SetColor(new Color(-1));
			this.DrawFrames(g);
		}

		public new void KeyDown(KeyCode theKey)
		{
			if (theKey == KeyCode.KEYCODE_ESCAPE)
			{
				this.ButtonDepress(0);
				return;
			}
			if (theKey == KeyCode.KEYCODE_SPACE)
			{
				this.ButtonDepress(0);
			}
		}

		public void SetQuestName(string theQuest)
		{
		}

		public void NudgeButtons(int theOffset)
		{
			foreach (KeyValuePair<int, DialogButton> keyValuePair in this.mBtns)
			{
				keyValuePair.Value.mY += theOffset;
			}
		}

		public virtual void DrawSpecialGemDisplay(Graphics g)
		{
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("GLOW", new Color(0, 0, 0, 0));
			((ImageFont)GlobalMembersResources.FONT_DIALOG).PushLayerColor("OUTLINE", new Color(0, 0, 0, 0));
			g.SetColor(new Color(GlobalMembers.M(16777215)));
			g.WriteString(string.Format(GlobalMembers._ID("x {0}", 230), this.mGameStats[17]), GlobalMembers.MS(400), GlobalMembers.MS(900), -1, -1);
			g.WriteString(string.Format(GlobalMembers._ID("x {0}", 231), this.mGameStats[18]), GlobalMembers.MS(780), GlobalMembers.MS(900), -1, -1);
			g.WriteString(string.Format(GlobalMembers._ID("x {0}", 232), this.mGameStats[19]), GlobalMembers.MS(1150), GlobalMembers.MS(900), -1, -1);
			((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("OUTLINE");
			((ImageFont)GlobalMembersResources.FONT_DIALOG).PopLayerColor("GLOW");
		}

		private string Unkern(string theString)
		{
			char[] array = new char[theString.Length * 2];
			string text = new string(array);
			for (int i = 0; i < theString.Length; i++)
			{
				text = text + theString.get_Chars(i) + "~";
			}
			return text;
		}

		public virtual void DrawHighScores(Graphics g)
		{
		}

		public virtual void DrawStatsLabels(Graphics g)
		{
		}

		public virtual void DrawStatsText(Graphics g)
		{
		}

		public virtual void DrawLabeledHighScores(Graphics g)
		{
			g.DrawImageBox(new Rect(GlobalMembers.MS(800), GlobalMembers.MS(385) - GlobalMembers.MS(0), GlobalMembers.MS(480), GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_LABEL.GetHeight()), GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_LABEL);
			g.SetColor(new Color(-1));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			((ImageFont)g.GetFont()).PushLayerColor("Main", new Color(GlobalMembers.M(8931352)));
			((ImageFont)g.GetFont()).PushLayerColor("Outline", new Color(GlobalMembers.M(16777215)));
			((ImageFont)g.GetFont()).PushLayerColor("Glow", new Color(0, 0, 0, 0));
			g.WriteString(GlobalMembers._ID("Top Scores:", 234), GlobalMembers.MS(1085), GlobalMembers.MS(435));
			((ImageFont)g.GetFont()).PopLayerColor("Main");
			((ImageFont)g.GetFont()).PopLayerColor("Outline");
			((ImageFont)g.GetFont()).PopLayerColor("Glow");
			g.PushState();
			g.Translate(GlobalMembers.MS(0), GlobalMembers.MS(60));
			this.DrawHighScores(g);
			g.PopState();
		}

		public virtual void DrawStatsFrame(Graphics g)
		{
			g.DrawImageBox(new Rect(GlobalMembers.MS(195), GlobalMembers.MS(385), GlobalMembers.MS(602), GlobalMembers.MS(282)), GlobalMembersResourcesWP.IMAGE_GAMEOVER_LIGHT_BOX);
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			((ImageFont)g.GetFont()).PushLayerColor("Main", new Color(255, 255, 255, 255));
			((ImageFont)g.GetFont()).PushLayerColor("OUTLINE", new Color(GlobalMembers.M(4210688)));
			((ImageFont)g.GetFont()).PushLayerColor("GLOW", new Color(0, 0, 0, 0));
			g.SetColor(new Color(GlobalMembers.M(16053456)));
			this.DrawStatsLabels(g);
			g.SetColor(new Color(GlobalMembers.M(16777056)));
			this.DrawStatsText(g);
			((ImageFont)g.GetFont()).PopLayerColor("Main");
			((ImageFont)g.GetFont()).PopLayerColor("OUTLINE");
			((ImageFont)g.GetFont()).PopLayerColor("GLOW");
		}

		public virtual void DrawLabeledStatsFrame(Graphics g)
		{
			g.DrawImageBox(new Rect(GlobalMembers.MS(195), GlobalMembers.MS(385) - GlobalMembers.MS(0), GlobalMembers.MS(600), GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_LABEL.GetHeight()), GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_LABEL);
			g.SetColor(new Color(-1));
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			((ImageFont)g.GetFont()).PushLayerColor("Main", new Color(GlobalMembers.M(8931352)));
			((ImageFont)g.GetFont()).PushLayerColor("OUTLINE", new Color(GlobalMembers.M(16777215)));
			((ImageFont)g.GetFont()).PushLayerColor("GLOW", new Color(0, 0, 0, 0));
			g.WriteString(GlobalMembers._ID("Statistics", 235), GlobalMembers.MS(485), GlobalMembers.MS(435));
			((ImageFont)g.GetFont()).PopLayerColor("Main");
			((ImageFont)g.GetFont()).PopLayerColor("OUTLINE");
			((ImageFont)g.GetFont()).PopLayerColor("GLOW");
			g.PushState();
			g.Translate(GlobalMembers.MS(0), GlobalMembers.MS(60));
			this.DrawStatsFrame(g);
			g.PopState();
		}

		public virtual void DrawFrames(Graphics g)
		{
			this.DrawStatsFrame(g);
			this.DrawHighScores(g);
		}

		public void OptionConfirmed()
		{
		}

		public Board mBoard;

		public Dictionary<int, DialogButton> mBtns = new Dictionary<int, DialogButton>();

		public List<HighScoreEntryLive> mHighScores = new List<HighScoreEntryLive>();

		public CurvedVal mCountupPct = new CurvedVal();

		public RankBarWidget mRankBar;

		public int mPoints;

		public int[] mGameStats = new int[40];

		public List<List<int>> mPointsBreakdown;

		public int mLevel;

		public int mPointMultiplier;

		public enum EWidgetId
		{
			eId_MainMenu,
			eId_ChooseQuest,
			eId_PlayAgain,
			eId_Badges
		}
	}
}
