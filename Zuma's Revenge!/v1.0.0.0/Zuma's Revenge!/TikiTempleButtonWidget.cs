using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class TikiTempleButtonWidget : ExtraSexyButton
	{
		public TikiTempleButtonWidget(int theId, TikiTemple theListener)
			: base(theId, theListener)
		{
			this.mUsesAnimators = false;
			this.mTikiTemple = theListener;
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public TikiTemple mTikiTemple;
	}
}
