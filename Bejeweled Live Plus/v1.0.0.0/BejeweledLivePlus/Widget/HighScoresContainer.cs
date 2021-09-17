using System;
using System.Collections.Generic;
using BejeweledLivePlus.UI;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class HighScoresContainer : Widget, Bej3ScrollWidgetListener, ScrollWidgetListener
	{
		public HighScoresContainer(int width)
		{
			this.mScoreTable = null;
			this.mHighlightedRow = -1;
			this.mMaxNameWidth = ConstantsWP.HIGHSCORESWIDGET_NAME_WIDTH;
		}

		private void ClearList()
		{
			if (this.mEntryNumberLabels.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this.mEntryNumberLabels.Count; i++)
			{
				this.RemoveWidget(this.mEntryNumberLabels[i]);
				this.RemoveWidget(this.mPointLabels[i]);
				this.RemoveWidget(this.mNameLabels[i]);
			}
			this.mEntryNumberLabels.Clear();
			this.mPointLabels.Clear();
			this.mNameLabels.Clear();
		}

		private void CreateList()
		{
			int num = 0;
			foreach (HighScoreEntryLive highScoreEntryLive in this.mScoreTable.mHighScoresLive)
			{
				num++;
				Label label = new Label(GlobalMembersResources.FONT_DIALOG, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT);
				label.SetLayerColor(0, Bej3Widget.COLOR_DIALOG_WHITE);
				label.SetText(num.ToString() + ".");
				this.mEntryNumberLabels.Add(label);
				this.AddWidget(label);
				HighScoreLabel highScoreLabel = new HighScoreLabel(GlobalMembersResources.FONT_DIALOG, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_LEFT);
				highScoreLabel.SetLayerColor(0, Bej3Widget.COLOR_DIALOG_WHITE);
				highScoreLabel.SetText(highScoreEntryLive.mName);
				this.mNameLabels.Add(highScoreLabel);
				highScoreLabel.mMaxScollWidth = 340;
				this.AddWidget(highScoreLabel);
				Label label2 = new Label(GlobalMembersResources.FONT_DIALOG, Label_Alignment_Horizontal.LABEL_ALIGNMENT_HORIZONTAL_RIGHT);
				label2.SetLayerColor(0, Bej3Widget.COLOR_DIALOG_2_FILL);
				label2.SetText(highScoreEntryLive.mScore.ToString());
				this.mPointLabels.Add(label2);
				this.AddWidget(label2);
			}
			this.ChangeWidth(this.mNewWidth);
		}

		public void LinkUpAssets()
		{
		}

		public void ChangeWidth(int theNewWidth)
		{
			if (this.mScoreTable == null || this.mScoreTable.mHighScoresLive == null)
			{
				return;
			}
			theNewWidth = 610;
			int count = this.mScoreTable.mHighScoresLive.Count;
			this.Resize(0, 0, theNewWidth, count * ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT - ConstantsWP.HIGHSCORESWIDGET_CONTAINER_OFFSET_Y * 2);
			if (this.mParent != null)
			{
				ScrollWidget scrollWidget = this.mParent as ScrollWidget;
				scrollWidget.ClientSizeChanged();
			}
			int highscoreswidget_ITEM_TEXT_OFFSET = ConstantsWP.HIGHSCORESWIDGET_ITEM_TEXT_OFFSET;
			for (int i = 0; i < count; i++)
			{
				this.mEntryNumberLabels[i].Resize(ConstantsWP.HIGHSCORESWIDGET_ENTRYNUMBER_X, ConstantsWP.HIGHSCORESWIDGET_ENTRYNUMBER_Y + ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT * i + highscoreswidget_ITEM_TEXT_OFFSET, 0, 0);
				this.mNameLabels[i].Resize(ConstantsWP.HIGHSCORESWIDGET_NAME_X - 70, ConstantsWP.HIGHSCORESWIDGET_NAME_Y + ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT * i + highscoreswidget_ITEM_TEXT_OFFSET, 0, 0);
				this.mPointLabels[i].Resize(this.mWidth - ConstantsWP.HIGHSCORESWIDGET_POINTS_X, ConstantsWP.HIGHSCORESWIDGET_POINTS_Y + ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT * i + highscoreswidget_ITEM_TEXT_OFFSET, 0, 0);
			}
		}

		public void SelectModeView(HighScoresMenuContainer.HSMODE m)
		{
			HighScoreTable.HighScoreTableTime t = ((this.mMenu != null) ? this.mMenu.mCurrentDisplayView : HighScoreTable.HighScoreTableTime.TIME_RECENT);
			this.mScoreTable.ReadLeaderboard(t);
		}

		public void SetMode(string modeName)
		{
			this.mScoreTable = GlobalMembers.gApp.mHighScoreMgr.GetOrCreateTable(modeName);
			if (this.mSkipFirstReadLR)
			{
				this.mSkipFirstReadLR = false;
			}
			HighScoreTable.HighScoreTableTime t = ((this.mMenu != null) ? this.mMenu.mCurrentDisplayView : HighScoreTable.HighScoreTableTime.TIME_RECENT);
			this.mScoreTable.ReadLeaderboard(t);
		}

		public void ReadLeaderBoard(HighScoreTable.HighScoreTableTime t)
		{
			if (this.mScoreTable != null)
			{
				this.mScoreTable.ReadLeaderboard(t);
			}
		}

		public virtual void ScrollTargetReached(ScrollWidget scrollWidget)
		{
		}

		public virtual void ScrollTargetInterrupted(ScrollWidget scrollWidget)
		{
		}

		public virtual void PageChanged(Bej3ScrollWidget scrollWidget, int pageH, int pageV)
		{
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			base.Resize(theX, theY, theWidth, theHeight);
			if (theY > 0)
			{
				base.Resize(theX, theY, theWidth, theHeight);
			}
		}

		public override void Update()
		{
			base.Update();
			if (this.mScoreTable == null)
			{
				return;
			}
			this.UpdateLoadingWheel();
			HighScoreTable.LRState mLRState = this.mScoreTable.mLRState;
			if (GlobalMembers.mByAllTimeButton != null && GlobalMembers.mByTodayButton != null)
			{
				bool isLeaderboardLoading = GlobalMembers.isLeaderboardLoading;
				GlobalMembers.mByAllTimeButton.SetDisabled(isLeaderboardLoading);
				GlobalMembers.mByTodayButton.SetDisabled(isLeaderboardLoading);
			}
			if (mLRState == HighScoreTable.LRState.LR_Idle)
			{
				this.mNeedDrawLoadingWheel = false;
				return;
			}
			if (mLRState == HighScoreTable.LRState.LR_Loading)
			{
				this.mNeedDrawLoadingWheel = true;
				this.ClearList();
				return;
			}
			if (mLRState == HighScoreTable.LRState.LR_Ready)
			{
				this.mNeedDrawLoadingWheel = false;
				this.ClearList();
				this.CreateList();
				this.mScoreTable.mLRState = HighScoreTable.LRState.LR_Idle;
				return;
			}
			if (mLRState == HighScoreTable.LRState.LR_Error)
			{
				this.mNeedDrawLoadingWheel = false;
				Dialog dialog = GlobalMembers.gApp.DoXBLErrorDialog();
				this.mScoreTable.mLRState = HighScoreTable.LRState.LR_Idle;
				dialog.mDialogListener = GlobalMembers.gApp;
				if (GlobalMembers.gApp.mBoard != null && GlobalMembers.gApp.mInterfaceState == InterfaceState.INTERFACE_STATE_GAMEDETAILMENU)
				{
					GlobalMembers.gApp.mBoard.mVisPausePct = 0f;
				}
			}
		}

		public override void Draw(Graphics g)
		{
			HighScoreTable.LRState mLRState = this.mScoreTable.mLRState;
			if (mLRState != HighScoreTable.LRState.LR_Loading)
			{
				if (mLRState == HighScoreTable.LRState.LR_Ready || mLRState == HighScoreTable.LRState.LR_Idle)
				{
					g.mTransY -= (float)ConstantsWP.HIGHSCORESWIDGET_CONTAINER_OFFSET_Y;
					g.mClipRect.mY = this.mParent.GetAbsPos().mY;
					g.mClipRect.mHeight = this.mParent.mHeight;
					int count = this.mScoreTable.mHighScoresLive.Count;
					Rect rect = new Rect(ConstantsWP.LISTBOX_DIVIDER_OFFSET_1, 0, this.mWidth - ConstantsWP.LISTBOX_DIVIDER_OFFSET_1 * 2, ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT);
					for (int i = 0; i < count; i++)
					{
						if (i == this.mHighlightedRow)
						{
							g.SetColor(HighScoresContainer.hiLColour);
						}
						else if (i % 2 == 0)
						{
							g.SetColor(HighScoresContainer.rowColour1);
						}
						else
						{
							g.SetColor(HighScoresContainer.rowColour2);
						}
						g.FillRect(rect.mX, rect.mY, rect.mWidth, rect.mHeight);
						rect.mY += ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT;
					}
					rect = new Rect(ConstantsWP.LISTBOX_LINE_OFFSET_1, -ConstantsWP.LISTBOX_DIVIDER_OFFSET_2 / 2, this.mWidth - ConstantsWP.LISTBOX_LINE_OFFSET_1 * 2, ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT);
					g.SetColor(HighScoresContainer.lineColour);
					for (int j = 0; j < count; j++)
					{
						g.FillRect(rect.mX, rect.mY, rect.mWidth, ConstantsWP.LISTBOX_LINE_HEIGHT);
						rect.mY += ConstantsWP.HIGHSCORESWIDGET_ITEM_HEIGHT;
						if (j % 2 == 0)
						{
							rect.mY += ConstantsWP.HIGHSCORESWIDGET_ITEM_LINE_OFFSET_ODD;
						}
						else
						{
							rect.mY -= ConstantsWP.HIGHSCORESWIDGET_ITEM_LINE_OFFSET_ODD;
						}
					}
					g.SetColor(HighScoresContainer.lineColour);
					g.FillRect(rect.mX, rect.mY, rect.mWidth, ConstantsWP.LISTBOX_LINE_HEIGHT_2);
					g.SetColor(Color.White);
					Bej3Widget.DrawImageBoxTileCenter(g, new Rect(ConstantsWP.LISTBOX_SHADOW_X, ConstantsWP.LISTBOX_SHADOW_Y - this.mY, this.mWidth - ConstantsWP.LISTBOX_SHADOW_X * 2, this.mParent.mHeight - ConstantsWP.LISTBOX_SHADOW_Y + ConstantsWP.LISTBOX_SHADOW_Y_BOTTOM), GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_SHADOW);
				}
			}
			this.DrawLoadingWheel(g);
		}

		private void UpdateLoadingWheel()
		{
			if (!this.mNeedDrawLoadingWheel)
			{
				return;
			}
			if (this.mUpdateCnt % 15 == 0)
			{
				this.mLoadingWheelDrawIndex++;
				this.mLoadingWheelDrawIndex %= 12;
			}
		}

		private void DrawLoadingWheel(Graphics g)
		{
			if (!this.mNeedDrawLoadingWheel)
			{
				return;
			}
			float mTransY = g.mTransY;
			Rect mClipRect = g.mClipRect;
			g.mTransY = (float)this.mParent.GetAbsPos().mY;
			int theX = (this.mParent.mWidth - 64) / 2;
			int theY = (this.mParent.mHeight - 64) / 2;
			g.mClipRect = new Rect(0, 0, GlobalMembers.gApp.mWidth, GlobalMembers.gApp.mHeight);
			g.DrawImage(HighScoresContainer.mLoadingImages[this.mLoadingWheelDrawIndex], theX, theY);
			g.mTransY = mTransY;
			g.mClipRect = mClipRect;
		}

		private List<Label> mEntryNumberLabels = new List<Label>();

		private List<Label> mPointLabels = new List<Label>();

		private List<HighScoreLabel> mNameLabels = new List<HighScoreLabel>();

		public HighScoreTable mScoreTable;

		private int mHighlightedRow;

		public int mMaxNameWidth;

		public int mUserPosition;

		public HighScoresMenuContainer mMenu;

		private int mNewWidth;

		private bool mSkipFirstReadLR = true;

		private static Color lineColour = new Color(252, 203, 153);

		private static Color rowColour1 = new Color(173, 120, 75);

		private static Color rowColour2 = new Color(204, 137, 80);

		private static Color hiLColour = new Color(247, 175, 115);

		private bool mNeedDrawLoadingWheel;

		private int mLoadingWheelDrawIndex;

		private static Image[] mLoadingImages = new Image[]
		{
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_01,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_02,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_03,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_04,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_05,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_06,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_07,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_08,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_09,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_10,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_11,
			GlobalMembersResourcesWP.IMAGE_LR_LOADING_12
		};
	}
}
