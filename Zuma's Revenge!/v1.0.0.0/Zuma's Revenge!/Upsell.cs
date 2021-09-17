using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class Upsell : Widget, ButtonListener, IDisposable
	{
		~Upsell()
		{
		}

		public override void Dispose()
		{
			base.RemoveAllWidgets(true, false);
			this.mBuyBtn.Dispose();
			this.mMenuBtn.Dispose();
		}

		public Upsell(bool from_exit)
		{
			this.mClip = false;
			this.mPriority = (this.mZOrder = int.MaxValue);
			Upsell.gZoomStart = Common._M(4f);
			this.mBlock2X = (float)(GameApp.gApp.mWidth + Common._DS(160));
			this.mState = 1;
			this.mZoom = Upsell.gZoomStart;
			this.mMenuBtn = new ButtonWidget(1, this);
			this.mMenuBtn.mDoFinger = true;
			this.mMenuBtn.mNormalRect = this.mMenuBtn.mButtonImage.GetCelRect(0);
			this.mMenuBtn.mOverRect = this.mMenuBtn.mButtonImage.GetCelRect(1);
			this.mMenuBtn.mDownRect = this.mMenuBtn.mButtonImage.GetCelRect(2);
			this.AddWidget(this.mMenuBtn);
			this.mBuyBtn = new ButtonWidget(2, this);
			this.mBuyBtn.mDoFinger = true;
			this.mBuyBtn.mNormalRect = this.mBuyBtn.mButtonImage.GetCelRect(0);
			this.mBuyBtn.mOverRect = this.mBuyBtn.mButtonImage.GetCelRect(1);
			this.mBuyBtn.mDownRect = this.mBuyBtn.mButtonImage.GetCelRect(2);
			this.AddWidget(this.mBuyBtn);
			this.mScreenshotTimer = Upsell.gScreenshotTimer;
			this.mScreenshotIdx = 0;
		}

		public override void Update()
		{
			float num = Common._M(50f);
			if (this.mState == 1)
			{
				this.mUpdateCnt++;
				if (this.mUpdateCnt >= Common._M(25))
				{
					this.mBlock1X += num;
					int num2 = 0;
					if (this.mBlock1X >= (float)num2)
					{
						this.mBlock1X = (float)num2;
						this.mState++;
						this.mUpdateCnt = 0;
					}
				}
			}
			else if (this.mState == 2)
			{
				this.mUpdateCnt++;
				int num3 = 0;
				if (this.mUpdateCnt >= Common._M(25))
				{
					this.mBlock2X -= num;
					if (this.mBlock2X <= (float)num3)
					{
						this.mBlock2X = (float)num3;
						this.mState++;
						this.mUpdateCnt = 0;
					}
				}
			}
			else if (this.mState == 3)
			{
				this.mUpdateCnt++;
				if (this.mUpdateCnt >= Common._M(25))
				{
					int num4 = Common._M(20);
					float num5 = (Upsell.gZoomStart - 1f) / (float)num4;
					this.mZoom -= num5;
					if (this.mZoom <= 1f)
					{
						this.mZoom = 1f;
						this.mState++;
						this.mUpdateCnt = 0;
					}
				}
			}
			else if (this.mState == 4)
			{
				this.mUpdateCnt++;
				if (this.mUpdateCnt >= Common._M(25))
				{
					int num6 = Common._M(15);
					this.mMenuBtn.Move(this.mMenuBtn.mX, this.mMenuBtn.mY - num6);
					this.mBuyBtn.Move(this.mBuyBtn.mX, this.mBuyBtn.mY - num6);
					int num7 = 0;
					int num8 = 0;
					int num9 = 0;
					if (this.mMenuBtn.mY <= num7)
					{
						this.mMenuBtn.mY = num7;
						num9++;
					}
					if (this.mBuyBtn.mY <= num8)
					{
						this.mBuyBtn.mY = num8;
						num9++;
					}
					if (num9 == 2)
					{
						this.mState++;
					}
				}
			}
			else if (this.mState == 5)
			{
				this.mScreenshotTimer--;
				if (this.mScreenshotTimer == 0)
				{
					this.mScreenshotTimer = Upsell.gScreenshotTimer;
					this.mScreenshotIdx = (this.mScreenshotIdx + 1) % Upsell.MAX_SCREENSHOTS;
				}
			}
			if (this.mState < 5 || this.mScreenshotTimer <= Upsell.gScreenshotFade)
			{
				this.MarkDirty();
			}
		}

		public override void Draw(Graphics g)
		{
		}

		public void ButtonDepress(int id)
		{
			if (id == this.mMenuBtn.mId && this.mFromExit)
			{
				GameApp.gApp.mDoingDRM = false;
				GameApp.gApp.Shutdown();
				return;
			}
			if (id == this.mMenuBtn.mId)
			{
				Board mBoard = GameApp.gApp.mBoard;
				GameApp.gApp.mWidgetManager.RemoveWidget(this);
				GameApp.gApp.mDoingDRM = false;
			}
		}

		public virtual void ButtonPress(int id)
		{
			if (id == this.mMenuBtn.mId && this.mFromExit)
			{
				GameApp.gApp.mDoingDRM = false;
				GameApp.gApp.Shutdown();
				return;
			}
			if (id == this.mMenuBtn.mId)
			{
				Board mBoard = GameApp.gApp.mBoard;
				GameApp.gApp.mWidgetManager.RemoveWidget(this);
				GameApp.gApp.mDoingDRM = false;
			}
		}

		public virtual void ButtonPress(int theId, int theClickCount)
		{
		}

		public virtual void ButtonDownTick(int theId)
		{
		}

		public virtual void ButtonMouseEnter(int theId)
		{
		}

		public virtual void ButtonMouseLeave(int theId)
		{
		}

		public virtual void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		private static float gZoomStart = 4f;

		private static int gScreenshotTimer = 300;

		private static int gScreenshotFade = 50;

		private static readonly int MAX_SCREENSHOTS = 9;

		protected float mBlock1X;

		protected float mBlock2X;

		protected float mZoom;

		protected ButtonWidget mMenuBtn;

		protected ButtonWidget mBuyBtn;

		protected int mState;

		protected int mScreenshotIdx;

		protected int mScreenshotTimer;

		public bool mFromExit;
	}
}
