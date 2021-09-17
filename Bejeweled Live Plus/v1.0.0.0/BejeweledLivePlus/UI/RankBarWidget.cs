using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace BejeweledLivePlus.UI
{
	public class RankBarWidget : Widget
	{
		public RankBarWidget(int theWidth, Board theBoard, RankUpDialog theRankUpDialog, bool drawText)
			: this(theWidth, theBoard, theRankUpDialog, drawText, true)
		{
		}

		public RankBarWidget(int theWidth, Board theBoard, RankUpDialog theRankUpDialog)
			: this(theWidth, theBoard, theRankUpDialog, true, true)
		{
		}

		public RankBarWidget(int theWidth, Board theBoard)
			: this(theWidth, theBoard, null, true, true)
		{
		}

		public RankBarWidget(int theWidth)
			: this(theWidth, null, null, true, true)
		{
		}

		public RankBarWidget(int theWidth, Board theBoard, RankUpDialog theRankUpDialog, bool drawText, bool drawRankName)
		{
			this.mTimeShown = 0;
			this.mShown = false;
			this.mDrawCrown = false;
			this.mTobleroning = false;
			this.mBoard = theBoard;
			this.mRankUpDialog = theRankUpDialog;
			this.mDrawText = drawText;
			this.mDrawRankName = drawRankName;
			this.mPrevFocus = null;
			this.mDispRank = GlobalMembers.gApp.mProfile.mOfflineRank;
			this.mDispRankPoints = GlobalMembers.gApp.mProfile.mOfflineRankPoints;
			this.mRankDelay = 0;
			this.mTobleroningOffset = 0f;
			this.mTobleroneWaiting = true;
			this.mTobleroningDirection = 1;
			this.Resize(0, 0, theWidth, 0);
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public override void Resize(int theX, int theY, int theWidth, int theHeight)
		{
			theHeight = GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_BG.mHeight;
			base.Resize(theX, theY, theWidth, theHeight);
		}

		public override void Update()
		{
			base.Update();
			if (!this.mShown)
			{
				return;
			}
			this.mTimeShown++;
			if (GlobalMembers.gApp.GetDialog(39) != null)
			{
				return;
			}
			if (this.mBoard == null)
			{
				return;
			}
			this.mRankupGlow.IncInVal();
			if (GlobalMembers.M(0) != 0 && this.mWidgetManager != null && this.mWidgetManager.mKeyDown[17])
			{
				this.mDispRankPoints = GlobalMembers.gApp.mProfile.mOfflineRankPoints - (long)GlobalMembers.M(25000);
				this.mDispRank = (int)GlobalMembers.gApp.mProfile.GetRankAtPoints(this.mDispRankPoints);
			}
			if (this.mRankDelay > 0)
			{
				this.mRankDelay--;
			}
			else if (this.mDispRankPoints < GlobalMembers.gApp.mProfile.mOfflineRankPoints && this.mTimeShown >= GlobalMembers.M(150))
			{
				if (this.mTimeShown % GlobalMembers.M(20) == 0)
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_RANK_COUNTUP);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eRANK_BAR_WIDGET_RANKUP_GLOW_ADD, this.mRankupGlow);
				}
				this.mDispRankPoints = (long)GlobalMembers.MIN((float)GlobalMembers.gApp.mProfile.mOfflineRankPoints, (float)(this.mDispRankPoints + (GlobalMembers.gApp.mProfile.mOfflineRankPoints - this.mDispRankPoints) / (long)GlobalMembers.M(100) + (long)ConstantsWP.RANKBARWIDGET_UPDATE_SPEED));
				int num = (int)GlobalMembers.gApp.mProfile.GetRankAtPoints(this.mDispRankPoints);
				if (num > this.mDispRank)
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_RANKUP);
					this.mDispRank = num;
					this.mDispRankPoints = GlobalMembers.gApp.mProfile.GetRankPoints((uint)this.mDispRank);
					this.mRankDelay = GlobalMembers.M(140);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eRANK_BAR_WIDGET_RANKUP_GLOW, this.mRankupGlow);
					if (this.mRankUpDialog != null)
					{
						this.mRankUpDialog.DoRankUp();
					}
				}
			}
			if (this.mTobleroning && this.mRankDelay <= 0)
			{
				if (this.mTobleroneWaiting)
				{
					this.mTobleroningTimer--;
					if (this.mTobleroningTimer <= 0)
					{
						this.mTobleroningTimer = 150;
						this.mTobleroneWaiting = !this.mTobleroneWaiting;
						this.mTobleroningDirection = ((this.mTobleroningOffset == 0f) ? (-1) : 1);
						this.mTobleroningTarget = (int)this.mTobleroningOffset + this.mTobleroningDirection;
					}
				}
				else
				{
					float num2 = 0.01f;
					if (Math.Abs(this.mTobleroningOffset - (float)this.mTobleroningTarget) > num2)
					{
						this.mTobleroningOffset += num2 * (float)this.mTobleroningDirection;
					}
					else
					{
						this.mTobleroningTimer--;
						this.mTobleroningOffset = (float)((int)(this.mTobleroningOffset + num2 * (float)this.mTobleroningDirection));
						if (this.mTobleroningTimer <= 0)
						{
							this.mTobleroningTimer = 150;
							this.mTobleroneWaiting = !this.mTobleroneWaiting;
						}
					}
				}
			}
			this.MarkDirty();
		}

		public override void Draw(Graphics g)
		{
			g.SetFont(GlobalMembersResources.FONT_DIALOG);
			g.SetColor(new Color(-1));
			int rank = this.GetRank();
			long rankPoints = this.GetRankPoints();
			long nextRankPoints = this.GetNextRankPoints();
			long rankPoints2 = GlobalMembers.gApp.mProfile.GetRankPoints((uint)rank);
			if (this.mRankDelay > 0)
			{
				rankPoints2 = GlobalMembers.gApp.mProfile.GetRankPoints((uint)(rank - 1));
			}
			float num = 0f;
			if (nextRankPoints != rankPoints2)
			{
				num = (float)GlobalMembers.MIN((double)(rankPoints - rankPoints2) / (double)(nextRankPoints - rankPoints2), 1.0);
			}
			g.DrawImageBox(new Rect(0, 0, this.mWidth, this.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_BG);
			if (this.mDrawCrown)
			{
				g.DrawImage(GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_CROWN, 0, 0);
			}
			int num2 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(1354) - GlobalMembersResourcesWP.ImgXOfs(1355));
			if (!this.mDrawCrown)
			{
				num2 -= ConstantsWP.RANKBARWIDGET_BG_OFFSET_NO_CROWN;
			}
			int num3 = this.mWidth - (GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_BG.mWidth - GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR.mWidth);
			if (!this.mDrawCrown)
			{
				num3 += ConstantsWP.RANKBARWIDGET_BG_OFFSET_NO_CROWN;
			}
			g.DrawImageBox(new Rect(num2, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_ID) - GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_BG_ID)), num3, GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR);
			num2 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_FILL_ID) - GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_BG_ID));
			num3 = 0;
			if (!this.mDrawCrown)
			{
				num2 -= ConstantsWP.RANKBARWIDGET_BG_OFFSET_NO_CROWN;
				num3 += ConstantsWP.RANKBARWIDGET_BG_OFFSET_NO_CROWN;
			}
			Rect theDest = new Rect(num2, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_FILL_ID) - GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_BG_ID)), num3 + this.mWidth - (GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_BG.mWidth - GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_FILL.mWidth), GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_FILL.mHeight);
			Rect mClipRect = g.mClipRect;
			int num4 = num3 + this.mWidth - (GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_BG.mWidth - GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_GLOW.mWidth);
			num4 -= ConstantsWP.RANKBARWIDGET_FILL_OFFSET * 2;
			g.ClipRect(num2, 0, (int)((float)num4 * num), this.mHeight);
			g.DrawImageBox(theDest, GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_FILL);
			g.mClipRect = mClipRect;
			if (this.mRankupGlow != null)
			{
				g.SetColorizeImages(true);
				g.SetColor(this.mRankupGlow);
				num2 = (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_GLOW_ID) - GlobalMembersResourcesWP.ImgXOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_BG_ID));
				if (!this.mDrawCrown)
				{
					num2 -= ConstantsWP.RANKBARWIDGET_BG_OFFSET_NO_CROWN;
				}
				g.DrawImageBox(new Rect(num2, (int)GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_GLOW_ID) - GlobalMembersResourcesWP.ImgYOfs(ResourceId.IMAGE_DIALOG_PROGRESS_BAR_BG_ID)), num4 + ConstantsWP.RANKBARWIDGET_FILL_OFFSET * 2, GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_GLOW.mHeight), GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_GLOW);
			}
			if (this.mDrawText)
			{
				string rankName = this.GetRankName(rank, false);
				g.SetColor(new Color(-1));
				string theString = string.Format(GlobalMembers._ID("Rank: {0}", 427), rank + 1);
				float num5 = 1f;
				float num6 = (float)g.GetFont().StringWidth(theString);
				g.SetScale(num5, num5, (float)ConstantsWP.RANKBARWIDGET_TEXT_POS_1_X, (float)ConstantsWP.RANKBARWIDGET_TEXT_SCALING_POS_Y);
				int num7 = this.mWidth / 2;
				if (this.mDrawCrown)
				{
					num7 += ConstantsWP.RANKBARWIDGET_TEXT_POS_6_X;
				}
				num2 = (int)((this.mDrawRankName && !this.mTobleroning) ? ((float)ConstantsWP.RANKBARWIDGET_TEXT_POS_1_X) : ((float)num7 - num6 / 2f));
				int num8 = (int)(this.mTobleroningOffset * (float)ConstantsWP.RANKBARWIDGET_TOBLERONE);
				if (this.mTobleroning)
				{
					g.mClipRect.mY = g.mClipRect.mY + ConstantsWP.RANKBARWIDGET_TOBLERONE_CLIP_TOP;
					g.mClipRect.mHeight = g.mClipRect.mHeight - (ConstantsWP.RANKBARWIDGET_TOBLERONE_CLIP_TOP + ConstantsWP.RANKBARWIDGET_TOBLERONE_CLIP_BOTTOM);
				}
				g.WriteString(theString, num2, ConstantsWP.RANKBARWIDGET_TEXT_POS_Y + num8, -1, -1);
				if (this.mTobleroning)
				{
					num8 += ConstantsWP.RANKBARWIDGET_TOBLERONE;
				}
				if (this.mDrawRankName)
				{
					int theJustification = 1;
					int num9;
					if (this.mTobleroning)
					{
						num9 = this.mWidth / 2 - g.StringWidth(rankName) / 2;
						if (this.mDrawCrown)
						{
							num9 += ConstantsWP.RANKBARWIDGET_TEXT_POS_6_X;
						}
						theJustification = -1;
					}
					else
					{
						num9 = this.mWidth - ConstantsWP.RANKBARWIDGET_TEXT_POS_4_X;
					}
					g.SetScale(num5, num5, (float)(this.mWidth - ConstantsWP.RANKBARWIDGET_TEXT_POS_4_X), (float)ConstantsWP.RANKBARWIDGET_TEXT_SCALING_POS_Y);
					g.WriteString(rankName, num9, ConstantsWP.RANKBARWIDGET_TEXT_POS_Y + num8, -1, theJustification);
				}
				g.SetScale(1f, 1f, 0f, 0f);
			}
		}

		public int GetRank()
		{
			if (this.mBoard != null)
			{
				return this.mDispRank;
			}
			return GlobalMembers.gApp.mProfile.mOfflineRank;
		}

		public long GetRankPoints()
		{
			if (this.mBoard != null)
			{
				return this.mDispRankPoints;
			}
			return GlobalMembers.gApp.mProfile.mOfflineRankPoints;
		}

		public long GetNextRankPoints()
		{
			if (this.mRankDelay > 0)
			{
				return GlobalMembers.gApp.mProfile.GetRankPoints((uint)this.GetRank());
			}
			return GlobalMembers.gApp.mProfile.GetRankPoints((uint)(this.GetRank() + 1));
		}

		public string GetRankName(int aRank, bool includeRankNumber)
		{
			return GlobalMembers.gApp.mRankNames[GlobalMembers.MIN(aRank, GlobalMembers.gApp.mRankNames.size<string>() - 1)];
		}

		public long GetRankPointsRemaining()
		{
			return (long)((float)(this.GetNextRankPoints() - this.GetRankPoints()) * this.mRankPointMultiplier + 999f) / 1000L;
		}

		public void Shown(Board theBoard)
		{
			this.mBoard = theBoard;
			if (theBoard != null)
			{
				this.mRankPointMultiplier = theBoard.GetRankPointMultiplier();
			}
			this.mTimeShown = 0;
			this.mShown = true;
			this.mDispRank = GlobalMembers.gApp.mProfile.mOfflineRank;
			this.mDispRankPoints = GlobalMembers.gApp.mProfile.mOfflineRankPoints;
			this.mRankDelay = 0;
			this.mTobleroningOffset = 0f;
			this.mTobleroneWaiting = true;
			this.mTobleroningDirection = 1;
			this.mTobleroningTimer = 300;
		}

		public void Hidden()
		{
			this.mShown = false;
		}

		public bool FinishedRankUp()
		{
			return this.mDispRankPoints >= GlobalMembers.gApp.mProfile.mOfflineRankPoints;
		}

		public override void MouseEnter()
		{
			base.MouseEnter();
			if (GlobalMembers.gApp.mDebugKeysEnabled)
			{
				this.mPrevFocus = GlobalMembers.gApp.mWidgetManager.mFocusWidget;
				GlobalMembers.gApp.mWidgetManager.SetFocus(this);
			}
		}

		public override void MouseLeave()
		{
			base.MouseLeave();
			if (GlobalMembers.gApp.mDebugKeysEnabled && this.mPrevFocus != null)
			{
				GlobalMembers.gApp.mWidgetManager.SetFocus(this.mPrevFocus);
			}
		}

		public override void KeyChar(char theChar)
		{
		}

		private float mRankPointMultiplier;

		public bool mDrawCrown;

		public CurvedVal mRankupGlow = new CurvedVal();

		public long mDispRankPoints;

		public int mDispRank;

		public int mRankDelay;

		public bool mDrawText;

		public bool mDrawRankName;

		public Board mBoard;

		public RankUpDialog mRankUpDialog;

		public int mTimeShown;

		public bool mShown;

		public Widget mPrevFocus;

		public bool mTobleroning;

		public float mTobleroningOffset;

		public int mTobleroningDirection;

		public int mTobleroningTimer;

		public bool mTobleroneWaiting;

		public int mTobleroningTarget;
	}
}
