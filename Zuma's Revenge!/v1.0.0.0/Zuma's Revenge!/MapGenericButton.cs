using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class MapGenericButton : ExtraSexyButton
	{
		public MapGenericButton(int theId, MapScreen theListener)
			: base(theId, theListener)
		{
			this.mUsesAnimators = false;
			this.mMapScreen = theListener;
		}

		public override void Draw(Graphics g)
		{
			g.SetColorizeImages(true);
			g.SetColor(this.mMapScreen.mAlpha);
			base.Draw(g);
		}

		public MapScreen mMapScreen;
	}
}
