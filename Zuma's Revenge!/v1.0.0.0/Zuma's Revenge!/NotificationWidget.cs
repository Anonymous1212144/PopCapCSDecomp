using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class NotificationWidget : Widget
	{
		public NotificationWidget(Board theBoard, string theStringInfo)
		{
			this.mBoard = theBoard;
			this.mDisplayTime = 2000UL;
			this.mDisplayStart = ulong.MaxValue;
			this.mSoundID = -1;
			this.mSlideState = NotificationWidget.SLIDE_STATE.SLIDE_ON;
			this.mIsFinished = false;
			this.mSlideVal.mAppUpdateCountSrc = this.mBoard.mUpdateCnt;
			this.mSlideVal.SetCurve(Common._MP("b70,1,0.02,1,#     $P    }~"));
			this.mFont = Res.GetFontByID(ResID.FONT_SHAGLOUNGE38_YELLOW);
			this.mNotification = theStringInfo;
			this.mNotificationStringWidth = this.mFont.StringWidth(theStringInfo);
			int num = Common._DS(600);
			int num2 = Common._DS(100);
			int num3 = ((this.mNotificationStringWidth + num2 > num) ? (this.mNotificationStringWidth + num2) : num);
			int num4 = Common._DS(150);
			this.mYStart = (float)(GameApp.gApp.GetScreenRect().mHeight + num4);
			this.mYEnd = (float)(GameApp.gApp.GetScreenRect().mHeight - num4 / 2);
			this.Resize(GameApp.gApp.GetScreenRect().mX + (GameApp.gApp.GetScreenRect().mWidth - num3) / 2, (int)this.mYStart, num3, num4);
		}

		public override void Draw(Graphics g)
		{
			Common.DrawCommonDialogBacking(g, 0, 0, this.mWidth, this.mHeight * 2);
			g.SetFont(this.mFont);
			g.SetColor(Color.White);
			if (Localization.GetCurrentLanguage() == Localization.LanguageType.Language_CH || Localization.GetCurrentLanguage() == Localization.LanguageType.Language_CHT)
			{
				g.DrawString(this.mNotification, (this.mWidth - this.mNotificationStringWidth) / 2, Common._DS(60) + 3);
				return;
			}
			g.DrawString(this.mNotification, (this.mWidth - this.mNotificationStringWidth) / 2, Common._DS(60));
		}

		public override void Update()
		{
			switch (this.mSlideState)
			{
			case NotificationWidget.SLIDE_STATE.SLIDE_ON:
				if (this.mSlideVal.IsDoingCurve())
				{
					float num = (float)this.mSlideVal.GetOutVal() * (this.mYEnd - this.mYStart);
					this.Move(this.mX, (int)(this.mYStart + num));
					return;
				}
				this.PlaySound();
				this.mSlideState = NotificationWidget.SLIDE_STATE.SLIDE_ONSCREEN;
				this.mDisplayStart = (ulong)Common.SexyTime();
				return;
			case NotificationWidget.SLIDE_STATE.SLIDE_ONSCREEN:
			{
				ulong num2 = (ulong)Common.SexyTime();
				if (num2 - this.mDisplayStart >= this.mDisplayTime)
				{
					this.mSlideVal.SetCurve(Common._MP("b70,1,0.02,1,#     $P    }~"));
					this.mSlideState = NotificationWidget.SLIDE_STATE.SLIDE_OFF;
					this.mYStart = (float)this.mY;
					this.mYEnd = (float)(GameApp.gApp.GetScreenRect().mHeight + this.mHeight);
					return;
				}
				break;
			}
			case NotificationWidget.SLIDE_STATE.SLIDE_OFF:
				if (this.mSlideVal.IsDoingCurve())
				{
					float num3 = (float)this.mSlideVal.GetOutVal() * (this.mYEnd - this.mYStart);
					this.Move(this.mX, (int)(this.mYStart + num3));
					return;
				}
				this.mSlideState = NotificationWidget.SLIDE_STATE.SLIDE_OFFSCREEN;
				return;
			case NotificationWidget.SLIDE_STATE.SLIDE_OFFSCREEN:
				this.mIsFinished = true;
				break;
			default:
				return;
			}
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mBoard == null)
			{
				return;
			}
			this.mBoard.MouseDown(x, y, theClickCount);
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (this.mBoard == null)
			{
				return;
			}
			this.mBoard.MouseDown(x, y, theClickCount);
		}

		public override void MouseMove(int x, int y)
		{
			if (this.mBoard == null)
			{
				return;
			}
			this.mBoard.MouseMove(x, y);
		}

		public override void MouseDrag(int x, int y)
		{
			if (this.mBoard == null)
			{
				return;
			}
			this.mBoard.MouseMove(x, y);
		}

		public bool IsFinished()
		{
			return this.mIsFinished;
		}

		private void PlaySound()
		{
			if (this.mSoundID == -1)
			{
				return;
			}
			this.mBoard.mApp.mSoundPlayer.Play(this.mSoundID);
			this.mSoundID = -1;
		}

		private Board mBoard;

		private float mYStart;

		private float mYEnd;

		private ulong mDisplayTime;

		private ulong mDisplayStart;

		private NotificationWidget.SLIDE_STATE mSlideState;

		private bool mIsFinished;

		private CurvedVal mSlideVal = new CurvedVal();

		private string mNotification;

		private int mNotificationStringWidth;

		private Font mFont;

		public int mSoundID;

		private enum SLIDE_STATE
		{
			SLIDE_ON,
			SLIDE_ONSCREEN,
			SLIDE_OFF,
			SLIDE_OFFSCREEN,
			NUM_SLIDE_STATES
		}
	}
}
