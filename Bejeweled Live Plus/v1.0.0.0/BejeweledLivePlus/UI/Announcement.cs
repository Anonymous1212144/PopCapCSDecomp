using System;
using BejeweledLivePlus.Misc;
using BejeweledLivePlus.Widget;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.UI
{
	public class Announcement : IDisposable
	{
		public Announcement(Board theBoard)
			: this(theBoard, string.Empty)
		{
		}

		public Announcement(Board theBoard, string theText)
		{
			this.mBoard = theBoard;
			if (this.mBoard != null)
			{
				this.mPos = new Point(this.mBoard.GetBoardCenterX(), this.mBoard.GetBoardCenterY());
				if (!this.mBoard.mShowBoard)
				{
					this.mPos.mX = GlobalMembers.S(GlobalMembers.M(800));
				}
			}
			this.mText = theText;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eANNOUNCEMENT_ALPHA, this.mAlpha);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eANNOUNCEMENT_SCALE, this.mScale);
			this.mHorzScaleMult.SetConstant(1.0);
			this.mDarkenBoard = true;
			this.mBlocksPlay = true;
			this.mFont = GlobalMembersResources.FONT_HUGE;
			this.mGoAnnouncement = false;
			this.mTimeAnnouncement = false;
			if (this.mBoard != null)
			{
				this.mBoard.mAnnouncements.Add(this);
			}
		}

		public void Dispose()
		{
		}

		public void End()
		{
			if (this.mAlpha.GetInVal() < 0.85)
			{
				this.mAlpha.SetInVal(0.85);
				this.mScale.SetInVal(0.85);
				this.mHorzScaleMult.SetInVal(0.85);
			}
		}

		public virtual void Update()
		{
			this.mAlpha.IncInVal();
			this.mScale.IncInVal();
			this.mHorzScaleMult.IncInVal();
			if (!this.mAlpha.IsDoingCurve() && !this.mScale.IsDoingCurve())
			{
				if (this.mBoard != null)
				{
					if (this.mGoAnnouncement)
					{
						this.mBoard.mGoAnnouncementDone = true;
					}
					else if (this.mTimeAnnouncement)
					{
						this.mBoard.mTimeAnnouncementDone = true;
					}
					this.mBoard.mAnnouncements.RemoveAt(0);
					this.Dispose();
				}
				return;
			}
			if (this.mDarkenBoard && this.mBoard != null)
			{
				this.mBoard.mBoardDarkenAnnounce = (float)this.mAlpha;
			}
		}

		public virtual void Draw(Graphics g)
		{
			if (this.mScale == 0.0 || this.mBoard.mSuspendingGame)
			{
				return;
			}
			g.PushState();
			g.SetFont(this.mFont);
			g.SetColor(Color.White);
			if (this.mBoard != null)
			{
				g.mColor.mAlpha = (int)(this.mAlpha * (double)this.mBoard.GetPieceAlpha() * 255.0);
			}
			else
			{
				g.mColor.mAlpha = (int)(this.mAlpha * 255.0);
			}
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), 0, Bej3Widget.COLOR_INGAME_ANNOUNCEMENT);
			Utils.SetFontLayerColor((ImageFont)g.GetFont(), 1, Color.White);
			int num = GlobalMembers.S(this.mPos.mX) + ((this.mBoard != null) ? ((int)GlobalMembers.S(this.mBoard.mSideXOff * (double)this.mBoard.mSlideXScale)) : 0);
			int num2 = GlobalMembers.S(this.mPos.mY);
			float num3 = (float)this.mScale;
			Utils.PushScale(g, (float)((double)num3 * this.mHorzScaleMult), num3, (float)num, (float)num2);
			int num4 = 1;
			string text = this.mText;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				if (c == '\n')
				{
					num4++;
				}
			}
			int num5 = 0;
			int num6 = 0;
			for (int j = 0; j < this.mText.Length; j++)
			{
				if (this.mText.get_Chars(j) == '\n')
				{
					g.WriteString(this.mText.Substring(num6, j - num6), num, num2 - (num4 - num5) * GlobalMembers.MS(140) + GlobalMembers.MS(175));
					num6 = j + 1;
					num5++;
				}
			}
			g.WriteString(this.mText.Substring(num6), num, num2 - (num4 - num5) * GlobalMembers.MS(140) + GlobalMembers.MS(175));
			Utils.PopScale(g);
			g.PopState();
		}

		public Point mPos = default(Point);

		public string mText = string.Empty;

		public CurvedVal mAlpha = new CurvedVal();

		public CurvedVal mScale = new CurvedVal();

		public CurvedVal mHorzScaleMult = new CurvedVal();

		public bool mDarkenBoard;

		public bool mBlocksPlay;

		public Board mBoard;

		public Font mFont;

		public bool mGoAnnouncement;

		public bool mTimeAnnouncement;
	}
}
