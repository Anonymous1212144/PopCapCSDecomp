using System;
using BejeweledLivePlus.Widget;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class HighScoresMenuContainer : Bej3Widget, Bej3ButtonListener, ButtonListener
	{
		public HighScoresMenuContainer()
			: base(Menu_Type.MENU_HIGHSCORESMENU, false, Bej3ButtonType.TOP_BUTTON_TYPE_NONE)
		{
			this.mLockedScrollWidget = null;
			this.mDoesSlideInFromBottom = (this.mCanAllowSlide = false);
			int highscores_MENU_MODEWIDTH = ConstantsWP.HIGHSCORES_MENU_MODEWIDTH;
			int num = 4;
			int highscores_MENU_CONTAINER_HEIGHT = ConstantsWP.HIGHSCORES_MENU_CONTAINER_HEIGHT;
			Rect size = new Rect((ConstantsWP.HIGHSCORES_MENU_WIDTH - ConstantsWP.HIGHSCORES_MENU_CONTAINER_PADDING_X * 2) / 2 - ConstantsWP.HIGHSCORES_MENU_MODEWIDTH / 2, ConstantsWP.HIGHSCORES_MENU_MODE_PADDING_TOP, highscores_MENU_MODEWIDTH, highscores_MENU_CONTAINER_HEIGHT - ConstantsWP.HIGHSCORES_MENU_MODE_PADDING_TOP - ConstantsWP.HIGHSCORES_MENU_MODE_PADDING_TOP);
			this.Resize(0, 0, ConstantsWP.HIGHSCORES_MENU_CONTAINER_WIDTH * num, highscores_MENU_CONTAINER_HEIGHT);
			this.mHighscoreWidgets[0] = new HighScoresWidget(size, true, ConstantsWP.HIGHSCORES_MENU_SCROLLWIDGET_CORRECTION);
			this.mHighscoreWidgets[0].SetHeading(GlobalMembers.gApp.GetModeHeading(GameMode.MODE_CLASSIC));
			this.mHighscoreWidgets[0].SetMode(GameMode.MODE_CLASSIC);
			size.mX += highscores_MENU_MODEWIDTH + ConstantsWP.HIGHSCORES_MENU_MODE_PADDING_X;
			this.mHighscoreWidgets[1] = new HighScoresWidget(size, true, ConstantsWP.HIGHSCORES_MENU_SCROLLWIDGET_CORRECTION);
			this.mHighscoreWidgets[1].SetHeading(GlobalMembers.gApp.GetModeHeading(GameMode.MODE_LIGHTNING));
			this.mHighscoreWidgets[1].SetMode(GameMode.MODE_LIGHTNING);
			size.mX += highscores_MENU_MODEWIDTH + ConstantsWP.HIGHSCORES_MENU_MODE_PADDING_X;
			this.mHighscoreWidgets[2] = new HighScoresWidget(size, true, ConstantsWP.HIGHSCORES_MENU_SCROLLWIDGET_CORRECTION);
			this.mHighscoreWidgets[2].SetHeading(GlobalMembers.gApp.GetModeHeading(GameMode.MODE_DIAMOND_MINE));
			this.mHighscoreWidgets[2].SetMode(GameMode.MODE_DIAMOND_MINE);
			size.mX += highscores_MENU_MODEWIDTH + ConstantsWP.HIGHSCORES_MENU_MODE_PADDING_X;
			this.mHighscoreWidgets[3] = new HighScoresWidget(size, true, ConstantsWP.HIGHSCORES_MENU_SCROLLWIDGET_CORRECTION);
			this.mHighscoreWidgets[3].SetHeading(GlobalMembers.gApp.GetModeHeading(GameMode.MODE_BUTTERFLY));
			this.mHighscoreWidgets[3].SetMode(GameMode.MODE_BUTTERFLY);
			for (int i = 0; i < 4; i++)
			{
				this.AddWidget(this.mHighscoreWidgets[i]);
				this.mHighscoreWidgets[i].mContainer.mMenu = this;
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
			foreach (HighScoresWidget highScoresWidget in this.mHighscoreWidgets)
			{
				highScoresWidget.mContainer.mScoreTable.mLRState = HighScoreTable.LRState.LR_Idle;
			}
			this.mY = 0;
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
		}

		public void AllowScrolling(bool allow)
		{
			for (int i = 0; i < 4; i++)
			{
				this.mHighscoreWidgets[i].AllowScrolling(allow);
			}
		}

		public override void LinkUpAssets()
		{
			base.LinkUpAssets();
			for (int i = 0; i < 4; i++)
			{
				this.mHighscoreWidgets[i].LinkUpAssets();
			}
		}

		public void SelectTimeView(HighScoreTable.HighScoreTableTime t)
		{
			this.mCurrentDisplayView = t;
			this.mHighscoreWidgets[(int)this.mCurrentDisplayMode].ReadLeaderBoard(t);
		}

		public void SelectModeView(HighScoresMenuContainer.HSMODE m)
		{
			if (m < HighScoresMenuContainer.HSMODE.HIGHSCORES_CLASSIC || m >= HighScoresMenuContainer.HSMODE.HIGHSCORES_MAX_MODES)
			{
				return;
			}
			this.mCurrentDisplayMode = m;
			this.mHighscoreWidgets[(int)this.mCurrentDisplayMode].SelectModeView(m);
		}

		public HighScoresMenuContainer.HSMODE mCurrentDisplayMode;

		public HighScoreTable.HighScoreTableTime mCurrentDisplayView;

		private HighScoresWidget[] mHighscoreWidgets = new HighScoresWidget[4];

		public bool mScrollLocked;

		public Bej3ScrollWidget mLockedScrollWidget;

		public enum HSMODE
		{
			HIGHSCORES_CLASSIC,
			HIGHSCORES_LIGHTNING,
			HIGHSCORES_DIAMOND_MINE,
			HIGHSCORES_BUTTERFLIES,
			HIGHSCORES_MAX_MODES
		}
	}
}
