using System;
using SexyFramework.Graphics;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public class Hyperspace : Widget
	{
		public override void Update()
		{
			base.Update();
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public virtual void DrawBackground(Graphics g)
		{
		}

		public virtual float GetPieceAlpha()
		{
			return 1f;
		}

		public virtual bool IsUsing3DTransition()
		{
			return false;
		}

		public virtual void SetBGImage(SharedImageRef inImage)
		{
		}
	}
}
