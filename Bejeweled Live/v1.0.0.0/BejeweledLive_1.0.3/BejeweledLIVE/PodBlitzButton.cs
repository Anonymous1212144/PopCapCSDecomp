using System;
using Sexy;

namespace BejeweledLIVE
{
	public class PodBlitzButton : PodBigButton
	{
		public PodBlitzButton(int id, int cel, string label, ButtonListener listener, GameApp app)
			: base(id, cel, label, listener)
		{
			this.mApp = GlobalStaticVars.gSexyAppBase;
			this.mUserPic = null;
			this.mBadgeFrame.mWidth = this.mBadge.GetWidth();
			this.mBadgeFrame.mHeight = this.mBadge.GetHeight();
			this.mUserFrame.mWidth = 34;
			this.mUserFrame.mHeight = 34;
		}

		public new void Dispose()
		{
			base.Dispose();
		}

		public new void DrawPill(Graphics g)
		{
			if (this.mBadge != null)
			{
				base.DrawPill(g);
				g.DrawImage(this.mBadge, this.mBadgeFrame.mX, this.mBadgeFrame.mY);
				if (this.mUserPic != null)
				{
					g.DrawImage(this.mUserPic, this.mUserFrame.mX, this.mUserFrame.mY, this.mUserFrame.mWidth, this.mUserFrame.mHeight);
				}
			}
		}

		public new void DrawRing(Graphics g)
		{
			if (this.mBadge != null)
			{
				base.DrawRing(g);
			}
		}

		protected override void ComputeDrawFrames()
		{
			base.ComputeDrawFrames();
			this.mBadgeFrame.mX = this.mPillFrame.mX + this.mPillFrame.mHeight / 2;
			this.mBadgeFrame.mY = 2 + this.mPillFrame.mY + (this.mPillFrame.mHeight - this.mBadgeFrame.mHeight) / 2;
			this.mUserFrame.mX = this.mBadgeFrame.mX + 6;
			this.mUserFrame.mY = this.mBadgeFrame.mY + 6;
			if (this.mBadge != null)
			{
				this.mLabelOffset.mX = this.mLabelOffset.mX + this.mBadgeFrame.mWidth / 2;
			}
		}

		protected GameApp mApp;

		protected Image mBadge;

		protected Image mUserPic;

		protected TRect mBadgeFrame = default(TRect);

		protected TRect mUserFrame = default(TRect);
	}
}
