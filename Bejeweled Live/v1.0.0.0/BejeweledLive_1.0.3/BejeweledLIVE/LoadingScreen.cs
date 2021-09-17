using System;
using Sexy;

namespace BejeweledLIVE
{
	public class LoadingScreen : InterfaceWidget
	{
		public LoadingScreen(GameApp app)
			: base(app)
		{
			this.mBarPercent = 0f;
			this.mApp.LockOrientation(true);
			this.mLogo = Image.FromMemory(GlobalMembersLoadingTextures.LOAD_LOGO_IMAGE_DATA, 256, 256);
			this.mLoadingText = Image.FromMemory(GlobalMembersLoadingTextures.LOADING_IMAGE_DATA, 128, 32);
			this.mShine = Image.FromMemory(GlobalMembersLoadingTextures.LOAD_RING_IMAGE_DATA, 256, 256);
			this.mCopyright = Image.FromMemory(GlobalMembersLoadingTextures.COPYRIGHT_IMAGE_DATA, 512, 32);
		}

		public new void Dispose()
		{
			this.Dealloc();
			base.Dispose();
		}

		public void Dealloc()
		{
			if (this.mLogo != null)
			{
				this.mLogo.Dispose();
				this.mLoadingText.Dispose();
				this.mShine.Dispose();
				this.mCopyright.Dispose();
				GlobalStaticVars.gSexyAppBase.LockOrientation(false);
				this.mLogo = null;
			}
		}

		public new void InterfaceTransactionEnd(int uiState)
		{
			base.InterfaceTransactionEnd(uiState);
			if (this.mInterfaceStateParam == 0)
			{
				this.Dealloc();
			}
		}

		public new void Update()
		{
			base.Update();
			float num = (float)this.mApp.GetLoadingThreadProgress();
			if (this.mBarPercent < num)
			{
				this.mBarPercent += 0.1f;
				if (this.mBarPercent > num)
				{
					this.mBarPercent = num;
				}
			}
		}

		public new void Draw(Graphics g)
		{
			int num = this.mHeight / 2 - this.mLogo.mHeight / 2 - this.mLoadingText.mHeight - 10;
			g.DrawImage(this.mLogo, this.mWidth / 2 - this.mLogo.mWidth / 2, num);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
			g.DrawImageRotated(this.mShine, this.mWidth / 2 - this.mShine.mWidth / 2, num, (double)(-(double)this.mBarPercent * 2f) * 3.14159);
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawImage(this.mLoadingText, this.mWidth / 2 - this.mLoadingText.mWidth / 2, num + this.mLogo.mHeight + 10);
			g.DrawImage(this.mCopyright, (this.mWidth - this.mCopyright.mWidth) / 2, this.mHeight - this.mCopyright.mHeight);
		}

		protected float mBarPercent;

		protected Image mLogo;

		protected Image mLoadingText;

		protected Image mShine;

		protected Image mCopyright;
	}
}
