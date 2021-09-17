using System;
using BejeweledLivePlus.UI;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class HighScoresWidget : Bej3WidgetBase
	{
		public HighScoresWidget(Rect size, bool drawHeading, int scrollwidgetCorrectionOfffset)
		{
			this.mScrollLocked = false;
			this.mDrawHeading = drawHeading;
			this.mTopScoresHeadingLabel = new Label(GlobalMembersResources.FONT_SUBHEADER);
			this.mTopScoresHeadingLabel.SetText(GlobalMembers._ID("TOP SCORES", 3346));
			this.mTopScoresHeadingLabel.SetLayerColor(0, Bej3Widget.COLOR_SUBHEADING_2_STROKE);
			this.mTopScoresHeadingLabel.SetLayerColor(1, Bej3Widget.COLOR_SUBHEADING_2_FILL);
			this.AddWidget(this.mTopScoresHeadingLabel);
			this.mContainer = new HighScoresContainer(this.mWidth);
			this.mScrollWidget = new Bej3ScrollWidget(this.mContainer);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.mScrollDownOffset = scrollwidgetCorrectionOfffset;
			this.AllowScrolling(true);
			this.mScrollWidget.AddWidget(this.mContainer);
			this.AddWidget(this.mScrollWidget);
			base.Resize(size);
		}

		public HighScoresWidget(Rect size, bool drawHeading)
		{
			int mScrollDownOffset = 0;
			this.mScrollLocked = false;
			this.mDrawHeading = drawHeading;
			this.mTopScoresHeadingLabel = new Label(GlobalMembersResources.FONT_SUBHEADER);
			this.mTopScoresHeadingLabel.SetText(GlobalMembers._ID("TOP SCORES", 3346));
			this.mTopScoresHeadingLabel.SetLayerColor(0, Bej3Widget.COLOR_SUBHEADING_2_STROKE);
			this.mTopScoresHeadingLabel.SetLayerColor(1, Bej3Widget.COLOR_SUBHEADING_2_FILL);
			this.AddWidget(this.mTopScoresHeadingLabel);
			this.mContainer = new HighScoresContainer(this.mWidth);
			this.mScrollWidget = new Bej3ScrollWidget(this.mContainer);
			this.mScrollWidget.EnableBounce(true);
			this.mScrollWidget.mScrollDownOffset = mScrollDownOffset;
			this.AllowScrolling(true);
			this.mScrollWidget.AddWidget(this.mContainer);
			this.AddWidget(this.mScrollWidget);
			base.Resize(size);
		}

		public override void Update()
		{
			this.mScrollLocked = this.mScrollWidget.mLocked;
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			Bej3Widget.DrawInlayBox(g, new Rect(0, 0, this.mWidth, this.mHeight), this.mTopScoresHeadingLabel.GetTextWidth(), true);
			base.DeferOverlay(0);
		}

		public override void DrawOverlay(Graphics g)
		{
		}

		public void SetHeading(string theHeading)
		{
			this.mTopScoresHeadingLabel.SetText(theHeading);
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			this.mTopScoresHeadingLabel.Resize(this.mWidth / 2, ConstantsWP.HIGHSCORESWIDGET_TOPSCORES_HEADING_Y, 0, 0);
			this.mContainer.ChangeWidth(this.mWidth);
			this.mScrollWidget.Resize(0, ConstantsWP.HIGHSCORESWIDGET_BOX_Y, this.mWidth, this.mHeight - GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_FOOTER.mHeight - ConstantsWP.LISTBOX_FOOTER_OFFSET - ConstantsWP.HIGHSCORESWIDGET_BOX_Y);
		}

		public override void LinkUpAssets()
		{
			if (this.mContainer != null)
			{
				this.mContainer.LinkUpAssets();
			}
		}

		public void SetMode(GameMode gameMode)
		{
			this.mGameMode = (int)gameMode;
			string modeHeading = GlobalMembers.gApp.GetModeHeading((GameMode)this.mGameMode);
			this.mContainer.SetMode(modeHeading);
			this.mScrollWidget.ScrollToMin(false);
		}

		public int GetMode()
		{
			return this.mGameMode;
		}

		public void AllowScrolling(bool allow)
		{
			if (allow)
			{
				this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_VERTICAL);
				return;
			}
			this.mScrollWidget.SetScrollMode(ScrollWidget.ScrollMode.SCROLL_DISABLED);
		}

		public void SetMaxNameWidth(int maxWidth)
		{
			this.mContainer.mMaxNameWidth = maxWidth;
		}

		public override void TouchEnded(SexyAppBase.Touch touch)
		{
			base.TouchEnded(touch);
			Point absPos = this.mScrollWidget.GetAbsPos();
			touch.location.mX = touch.location.mX - absPos.mX;
			touch.location.mY = touch.location.mY - absPos.mY;
			touch.previousLocation.mX = touch.previousLocation.mX - absPos.mX;
			touch.previousLocation.mY = touch.previousLocation.mY - absPos.mY;
			this.mScrollWidget.TouchEnded(touch);
		}

		public void CenterOnUser()
		{
			int num = (this.mContainer.mUserPosition + 1) * ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT - this.mHeight / 2 + ConstantsWP.HIGHSCORESWIDGET_SCROLL_TO_OFFSET;
			num = GlobalMembers.MAX(0, GlobalMembers.MIN(num, this.mContainer.mHeight - this.mScrollWidget.mHeight));
			this.mScrollWidget.ScrollToPoint(new Point(0, num), false);
		}

		public void UnNewScore()
		{
		}

		public void SelectModeView(HighScoresMenuContainer.HSMODE m)
		{
			this.mContainer.SelectModeView(m);
		}

		public void ReadLeaderBoard(HighScoreTable.HighScoreTableTime t)
		{
			this.mContainer.ReadLeaderBoard(t);
		}

		private bool mDrawHeading;

		private int mGameMode;

		private Label mTopScoresHeadingLabel;

		public HighScoresContainer mContainer;

		public bool mScrollLocked;

		public Bej3ScrollWidget mScrollWidget;
	}
}
