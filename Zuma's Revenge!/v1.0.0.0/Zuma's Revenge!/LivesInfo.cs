using System;
using JeffLib;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class LivesInfo : IDisposable
	{
		public LivesInfo(Board board, int theLivesDelta)
		{
			this.mFont = Res.GetFontByID(ResID.FONT_SHAGEXOTICA68_BASE);
			this.mLivesDelta = theLivesDelta;
			this.mDisplayTime = 1200UL;
			this.mDisplayStart = ulong.MaxValue;
			this.mWaitTime = 150UL;
			this.mLivesCount = board.GetNumLives() - 1;
			this.mSlideVal.SetConstant(0.0);
			this.mSlideVal.mAppUpdateCountSrc = board.mUpdateCnt;
			this.InitLayout();
			this.StartSliding(LivesInfo.SLIDE_STATE.SLIDE_ON, 0);
			SoundAttribs soundAttribs = new SoundAttribs();
			soundAttribs.delay = 50;
			GameApp.gApp.mSoundPlayer.Play(Res.GetSoundByID(ResID.SOUND_NEW_EXTRA_LIFE), soundAttribs);
		}

		public virtual void Dispose()
		{
			if (this.mLivesText.mImage != null)
			{
				this.mLivesText.mImage.Dispose();
			}
		}

		public void Draw(Graphics g)
		{
			this.mFrame.mX = (this.mInset.mX = (int)((float)this.mFrame.mX - g.mTransX));
			this.DrawPlank(g);
			this.DrawLivesCount(g);
		}

		public void Update()
		{
			switch (this.mSlideState)
			{
			case LivesInfo.SLIDE_STATE.SLIDE_ON:
				this.DisplayOldCount();
				break;
			case LivesInfo.SLIDE_STATE.SLIDE_ONSCREEN:
				this.SlideOff();
				break;
			case LivesInfo.SLIDE_STATE.SLIDE_OFF:
				if (!this.IsSliding())
				{
					this.mSlideState = LivesInfo.SLIDE_STATE.SLIDE_OFFSCREEN;
				}
				break;
			case LivesInfo.SLIDE_STATE.SLIDE_WAIT:
				this.DisplayCount();
				break;
			}
			if (this.mLivesText.mImage != null)
			{
				this.mLivesText.Update();
			}
		}

		public bool IsDone()
		{
			return this.mSlideState == LivesInfo.SLIDE_STATE.SLIDE_OFFSCREEN;
		}

		private void InitLayout()
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LIVESFRAME);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_FROG_LIVES);
			Image imageByID3 = Res.GetImageByID(ResID.IMAGE_UI_POLE);
			this.mXOffset = imageByID3.GetWidth();
			int height = imageByID.GetHeight();
			int theX = GameApp.gApp.GetScreenRect().mX - GameApp.gApp.mWideScreenXOffset;
			int theY = GameApp.gApp.GetScreenRect().mHeight - height;
			int num = (int)((float)height * 0.13f);
			this.mTextXOffset = imageByID2.GetWidth() + ZumasRevenge.Common._S(50);
			this.mFrame = new Rect(theX, theY, 0, height);
			this.mFrame.mWidth = this.mTextXOffset + this.mFont.StringWidth("x 00");
			this.mFrame.mX = this.mFrame.mX - this.mFrame.mWidth;
			this.mInset = this.mFrame;
			this.mInset.mWidth = this.mInset.mWidth - num;
			this.mInset.mHeight = this.mInset.mHeight - num;
			this.mInset.mY = this.mInset.mY + num;
		}

		private void StartSliding(LivesInfo.SLIDE_STATE inSlideState, int inXPos)
		{
			this.mSlideState = inSlideState;
			this.mXStart = (float)(this.mFrame.mX + this.mXOffset);
			this.mXEnd = (float)(GameApp.gApp.GetScreenRect().mX + inXPos + this.mXOffset);
			this.mSlideVal.SetCurve(ZumasRevenge.Common._MP("b70,1,0.04,1,#     $P    }~"));
		}

		private void DrawPlank(Graphics g)
		{
			Image imageByID = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_FROG_LIVES);
			Image imageByID2 = Res.GetImageByID(ResID.IMAGE_GUI_INGAME_UI_LIVESFRAME);
			int mX = this.mInset.mX;
			int theY = (int)((float)this.mInset.mY + (float)(this.mInset.mHeight - imageByID.GetHeight()) * 0.5f);
			g.DrawImageBox(this.mFrame, imageByID2);
			g.DrawImage(imageByID, mX, theY);
		}

		private void DrawLivesCount(Graphics g)
		{
			if (this.mSlideState != LivesInfo.SLIDE_STATE.SLIDE_ONSCREEN)
			{
				int num = this.CapAt99((this.mSlideState == LivesInfo.SLIDE_STATE.SLIDE_ON || this.mSlideState == LivesInfo.SLIDE_STATE.SLIDE_WAIT) ? (this.mLivesCount - this.mLivesDelta) : this.mLivesCount);
				string theString = "x  " + num;
				g.SetFont(this.mFont);
				g.SetColor(Color.White);
				g.WriteString(theString, this.mInset.mX + this.mTextXOffset, this.mInset.mY + this.mFont.GetHeight());
				return;
			}
			if (this.mLivesText.mImage != null)
			{
				this.mLivesText.Draw(g);
			}
		}

		private void DisplayOldCount()
		{
			if (this.IsSliding())
			{
				return;
			}
			this.mSlideState = LivesInfo.SLIDE_STATE.SLIDE_WAIT;
			this.mDisplayStart = (ulong)SexyFramework.Common.SexyTime();
		}

		private void DisplayCount()
		{
			if ((ulong)SexyFramework.Common.SexyTime() - this.mDisplayStart < this.mWaitTime)
			{
				return;
			}
			this.mSlideState = LivesInfo.SLIDE_STATE.SLIDE_ONSCREEN;
			this.mDisplayStart = (ulong)SexyFramework.Common.SexyTime();
			this.InitLivesText();
			this.PreDrawLivesText(this.CapAt99(this.mLivesCount));
		}

		private void SlideOff()
		{
			if ((ulong)SexyFramework.Common.SexyTime() - this.mDisplayStart < this.mDisplayTime)
			{
				return;
			}
			this.mLivesText.mImage = null;
			this.StartSliding(LivesInfo.SLIDE_STATE.SLIDE_OFF, -this.mFrame.mWidth);
		}

		private bool IsSliding()
		{
			if (this.mSlideVal.IsDoingCurve())
			{
				float num = (float)this.mSlideVal.GetOutVal() * (this.mXEnd - this.mXStart);
				this.mFrame.mX = (this.mInset.mX = (int)(this.mXStart + num));
				return true;
			}
			return false;
		}

		private int CapAt99(int inLivesCount)
		{
			if (inLivesCount < 0)
			{
				return 0;
			}
			if (inLivesCount > 99)
			{
				return 99;
			}
			return inLivesCount;
		}

		private void InitLivesText()
		{
			int theWidth = this.mFont.StringWidth("x 00") + ZumasRevenge.Common._S(20);
			int theHeight = this.mFont.GetHeight() + ZumasRevenge.Common._S(10);
			this.mLivesText = new FwooshImage();
			this.mLivesText.mAlphaDec = 0f;
			this.mLivesText.mImage = new DeviceImage();
			this.mLivesText.mImage.mApp = GameApp.gApp;
			this.mLivesText.mImage.SetImageMode(true, true);
			this.mLivesText.mImage.AddImageFlags(16U);
			this.mLivesText.mImage.Create(theWidth, theHeight);
			this.mLivesText.mX = this.mInset.mX + this.mTextXOffset;
			this.mLivesText.mY = this.mInset.mY + (int)((float)this.mInset.mHeight * 0.5f) + ZumasRevenge.Common._S(5);
		}

		private void PreDrawLivesText(int inLivesCount)
		{
			Graphics graphics = new Graphics(this.mLivesText.mImage);
			graphics.Get3D().ClearColorBuffer(new Color(0, 0, 0, 0));
			graphics.SetFont(this.mFont);
			graphics.SetColor(Color.White);
			graphics.WriteString("x " + inLivesCount + " ", 0, this.mFont.GetAscent(), this.mLivesText.mImage.GetWidth());
			graphics.ClearRenderContext();
		}

		private Font mFont;

		private Rect mFrame = default(Rect);

		private Rect mInset = default(Rect);

		private int mTextXOffset;

		private int mXOffset;

		private FwooshImage mLivesText = new FwooshImage();

		private int mLivesCount;

		private int mLivesDelta;

		private float mXStart;

		private float mXEnd;

		private ulong mDisplayTime;

		private ulong mDisplayStart;

		private ulong mWaitTime;

		private LivesInfo.SLIDE_STATE mSlideState;

		private CurvedVal mSlideVal = new CurvedVal();

		private enum SLIDE_STATE
		{
			SLIDE_ON,
			SLIDE_ONSCREEN,
			SLIDE_OFF,
			SLIDE_OFFSCREEN,
			SLIDE_WAIT,
			NUM_SLIDE_STATES
		}
	}
}
