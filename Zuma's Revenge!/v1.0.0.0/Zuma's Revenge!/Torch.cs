using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class Torch : IDisposable
	{
		public Torch()
		{
		}

		public Torch(Torch rhs)
		{
			this.mOverlayAlpha = rhs.mOverlayAlpha;
			this.mWasHit = rhs.mWasHit;
			this.mDraw = rhs.mDraw;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mActive = rhs.mActive;
		}

		public virtual void Dispose()
		{
			GameApp.gApp.ReleaseTorchEffect(this.mFlame);
			GameApp.gApp.ReleaseTorchEffect(this.mFlameOut);
		}

		public void Update()
		{
			if (this.mFlame == null)
			{
				this.mFlame = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_TORCHFLAME").Duplicate();
				this.mFlame.mEmitAfterTimeline = true;
				Common.SetFXNumScale(this.mFlame, GameApp.gApp.Is3DAccelerated() ? 1f : Common._M(0.5f));
			}
			if (this.mFlameOut == null)
			{
				this.mFlameOut = GameApp.gApp.mResourceManager.GetPIEffect("PIEFFECT_NONRESIZE_TORCHFLAMEOUT").Duplicate();
				Common.SetFXNumScale(this.mFlameOut, GameApp.gApp.Is3DAccelerated() ? 1f : Common._M(0.5f));
			}
			if (this.mDraw)
			{
				if (this.mActive)
				{
					this.mFlame.mDrawTransform.LoadIdentity();
					float num = GameApp.DownScaleNum(1f);
					this.mFlame.mDrawTransform.Scale(num, num);
					if (this.mX > Common._DS(600))
					{
						this.mFlame.mDrawTransform.RotateDeg((float)Common._M(-75));
					}
					this.mFlame.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(50))), (float)(Common._S(this.mY) + Common._DS(Common._M1(130))));
					this.mFlame.Update();
					return;
				}
				if (this.mFlameOut.mFrameNum <= (float)this.mFlameOut.mLastFrameNum)
				{
					this.mFlameOut.mDrawTransform.LoadIdentity();
					float num2 = GameApp.DownScaleNum(1f);
					this.mFlameOut.mDrawTransform.Scale(num2, num2);
					this.mFlameOut.mDrawTransform.Translate((float)(Common._S(this.mX) + Common._DS(Common._M(400))), (float)(Common._S(this.mY) + Common._DS(Common._M1(320))));
					this.mFlameOut.Update();
				}
			}
		}

		public void Draw(Graphics g)
		{
			if (this.mDraw && this.mActive && this.mFlame != null)
			{
				g.PushState();
				this.mFlame.Draw(g);
				g.PopState();
			}
		}

		public void DrawAbove(Graphics g)
		{
			if (this.mDraw && this.mFlameOut != null && !this.mActive && this.mFlameOut.mFrameNum <= (float)this.mFlameOut.mLastFrameNum)
			{
				g.PushState();
				this.mFlameOut.Draw(g);
				g.PopState();
			}
		}

		public bool CheckCollision(Rect r)
		{
			if (this.mActive && r.Intersects(new Rect(this.mX, this.mY, this.mWidth, this.mHeight)))
			{
				GameApp.gApp.PlaySample(Res.GetSoundByID(ResID.SOUND_NEW_TORCH_EXTINGUISHED));
				this.mActive = false;
				this.mWasHit = true;
				this.mFlame.mEmitAfterTimeline = false;
				this.mFlameOut.ResetAnim();
				return true;
			}
			return false;
		}

		public void SyncState(DataSync sync)
		{
			sync.SyncBoolean(ref this.mActive);
			sync.SyncLong(ref this.mX);
			sync.SyncLong(ref this.mY);
			sync.SyncLong(ref this.mWidth);
			sync.SyncLong(ref this.mHeight);
			sync.SyncBoolean(ref this.mWasHit);
			sync.SyncBoolean(ref this.mDraw);
			sync.SyncLong(ref this.mOverlayAlpha);
			if (sync.isRead() && this.mWasHit)
			{
				this.mDraw = (this.mActive = false);
			}
		}

		public PIEffect mFlame;

		public PIEffect mFlameOut;

		public int mX;

		public int mY;

		public int mWidth;

		public int mHeight;

		public int mOverlayAlpha;

		public bool mActive;

		public bool mDraw = true;

		public bool mWasHit;
	}
}
