using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class LavaShader : Effect
	{
		protected override void Init()
		{
			this.mDisabled = false;
			this.mFadeInFromDeath = (this.mFadeoutDistortion = false);
			if (this.mBuffer == null)
			{
				this.mBuffer = new DeviceImage();
				this.mBuffer.mApp = this.mApp;
				this.mBuffer.AddImageFlags(24U);
				this.mBuffer.SetImageMode(false, false);
			}
		}

		protected void DoShader(Graphics g, DeviceImage buffer)
		{
		}

		public LavaShader()
		{
			this.mResGroup = "GamePlay";
			this.mApp = GameApp.gApp;
			this.mDisabled = false;
			this.mOrgDistAmt = (this.mDistAmt = (this.mScale = (this.mScroll = 0f)));
			this.mNeedFadein = false;
			this.mFadeInFromDeath = false;
			this.mAffectSkull = true;
			this.mFadeoutDistortion = false;
			this.mApplyTunnels = true;
			this.mApplyFullScene = false;
		}

		public override void Dispose()
		{
			base.Dispose();
			if (this.mBuffer != null)
			{
				this.mBuffer.Dispose();
				this.mBuffer = null;
			}
		}

		public override void LevelStarted(bool from_load)
		{
			if (this.mApplyFullScene && this.mOrgDistAmt > 0f)
			{
				this.mFadeoutDistortion = true;
				return;
			}
			this.mFadeoutDistortion = false;
		}

		public override void Update()
		{
			this.Update(false);
		}

		public void Update(bool only_check_shaders_supported)
		{
			Board board = this.mApp.GetBoard();
			if (this.mActivateOnMuMu && !board.mDoMuMuMode)
			{
				return;
			}
			if (this.mNeedFadein)
			{
				if (this.mApp.mBoard == null || this.mApp.mBoard.mTransitionScreenImage == null)
				{
					this.mDistAmt = Math.Min(this.mOrgDistAmt, this.mDistAmt + Common._M(5E-06f));
				}
				this.mDisabled = (double)this.mDistAmt <= 1E-09;
				this.mNeedFadein = this.mDistAmt < this.mOrgDistAmt;
			}
			else if (this.mFadeoutDistortion && this.mDistAmt > 0f && !board.mDoMuMuMode)
			{
				this.mDistAmt -= Common._M(1E-06f);
				if (this.mDistAmt <= 0f)
				{
					this.mDistAmt = 0f;
					this.mDisabled = true;
				}
			}
			else if (board.mDoMuMuMode)
			{
				this.mDistAmt = (this.mOrgDistAmt = Common._M(0.0005f));
				this.mScroll = Common._M(0.08f);
				this.mScale = Common._M(0.2f);
				this.mDisabled = false;
			}
			if (only_check_shaders_supported)
			{
				if (!this.mApp.ShadersSupported())
				{
					return;
				}
			}
			else
			{
				bool flag = !this.mDisabled && this.mApp.ShadersSupported() && board != null && !board.DoingMainDarkFrogSequence();
				if (this.mApp.mLoadingThreadStarted && !this.mApp.mLoadingThreadCompleted)
				{
					flag = false;
				}
				if (!flag)
				{
					return;
				}
			}
			this.mUpdateCount++;
			this.mTimer += Common._M(0.02f);
		}

		public override void DrawUnderBackground(Graphics g)
		{
			Board board = this.mApp.GetBoard();
			bool flag = !this.mDisabled && this.mApp.ShadersSupported() && board != null && !board.DoingMainDarkFrogSequence() && (!this.mActivateOnMuMu || board.mDoMuMuMode);
			if (!this.mApp.mLoadingThreadStarted || !this.mApp.mLoadingThreadCompleted)
			{
			}
			int num = 1024;
			int theStretchedHeight = Common._DS(1200);
			g.DrawImage(this.mApp.mBoard.mBackgroundImage, (Common._S(800) - num) / 2 + GameApp.gScreenShakeX, GameApp.gScreenShakeY, num, theStretchedHeight);
		}

		public override bool DrawTunnel(Graphics g, Image img, int x, int y)
		{
			Board board = this.mApp.GetBoard();
			if (!this.mDisabled && this.mApp.ShadersSupported() && board != null && !board.DoingMainDarkFrogSequence() && this.mActivateOnMuMu)
			{
				bool mDoMuMuMode = board.mDoMuMuMode;
			}
			return true;
		}

		public bool DrawTunnel(Graphics g, Image img, int x, int y, float dist_amt, float scale, float scroll, float timer, float alpha_mult)
		{
			return false;
		}

		public override void DrawFullScene(Graphics g)
		{
		}

		public override void SetParams(string key, string value)
		{
		}

		public override void NukeParams()
		{
			this.mActivateOnMuMu = false;
			this.mApplyTunnels = true;
		}

		public override bool DrawSkullPit(Graphics g, HoleMgr hole)
		{
			Board board = this.mApp.GetBoard();
			if (!this.mDisabled && this.mApp.ShadersSupported() && board != null)
			{
				board.DoingMainDarkFrogSequence();
			}
			return false;
		}

		public override void UserDied()
		{
			if (this.mFadeoutDistortion && this.mDistAmt < this.mOrgDistAmt)
			{
				this.mDisabled = false;
				this.mFadeInFromDeath = true;
			}
		}

		public override string GetName()
		{
			return "LavaShader";
		}

		public override void CopyFrom(Effect e)
		{
		}

		public bool mActivateOnMuMu;

		public float mDistAmt;

		public float mOrgDistAmt;

		public float mScroll;

		public float mScale;

		public bool mAffectSkull;

		public bool mApplyFullScene;

		public bool mFadeoutDistortion;

		public bool mDisabled;

		public bool mFadeInFromDeath;

		public bool mNeedFadein;

		public bool mApplyTunnels;

		protected DeviceImage mBuffer;

		protected GameApp mApp;

		protected float mTimer;
	}
}
