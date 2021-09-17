using System;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class UnderDialogWidget : Widget
	{
		public UnderDialogWidget()
		{
			this.mMouseVisible = false;
			this.mHasAlpha = true;
			this.mShrunkScreen1 = null;
			this.mShrunkScreen2 = null;
			this.mClip = false;
		}

		~UnderDialogWidget()
		{
		}

		public void CreateImages()
		{
		}

		public void DrawPaused(Graphics g)
		{
		}

		public override void Update()
		{
			base.Update();
			if (GameApp.gApp.mDialogObscurePct > 0f && GlobalMembers.gSexyAppBase.mHasFocus)
			{
				this.MarkDirty();
			}
		}

		public override void Draw(Graphics g)
		{
		}

		public DeviceImage mShrunkScreen1;

		public DeviceImage mShrunkScreen2;
	}
}
