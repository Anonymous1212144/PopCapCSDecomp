using System;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class DigBackground : Background
	{
		public DigBackground(DigGoal theGoal)
			: base(string.Empty, true, false)
		{
			this.mGoal = theGoal;
			this.mLastGridDepth = 0f;
		}

		public override void Draw(Graphics g)
		{
			this.DrawFull(g);
		}

		public virtual void DrawBack(Graphics g)
		{
			double num = GlobalMembers.M(0.0);
			g.SetColor(Utils.ColorLerp(new Color(GlobalMembers.M(190), GlobalMembers.M(150), GlobalMembers.M(95)), new Color(GlobalMembers.M(174), GlobalMembers.M(0), GlobalMembers.M(0)), (float)((int)((float)num))));
			g.FillRect(GlobalMembers.MS(620), GlobalMembers.MS(0), GlobalMembers.MS(1200), GlobalMembers.MS(1200));
			g.SetColor(new Color(-1));
		}

		public virtual void DrawFull(Graphics g)
		{
			this.DrawBack(g);
		}

		public override SharedImageRef GetBackgroundImage(bool wait)
		{
			return this.GetBackgroundImage(wait, true);
		}

		public override SharedImageRef GetBackgroundImage()
		{
			return this.GetBackgroundImage(true, true);
		}

		public override SharedImageRef GetBackgroundImage(bool wait, bool removeAnim)
		{
			this.mImage.mUnsharedImage = this.mSharedRenderTarget.Lock(GlobalMembers.gApp.mScreenBounds.mWidth, GlobalMembers.gApp.mScreenBounds.mHeight);
			Graphics graphics = new Graphics(this.mImage.GetImage());
			graphics.Translate(GlobalMembers.MS(-160) - GlobalMembers.gApp.mScreenBounds.mX, GlobalMembers.gApp.mScreenBounds.mY);
			this.DrawFull(graphics);
			this.mSharedRenderTarget.Unlock();
			return this.mImage;
		}

		public override void LoadImageProc()
		{
		}

		public DigGoal mGoal;

		public float mLastGridDepth;
	}
}
