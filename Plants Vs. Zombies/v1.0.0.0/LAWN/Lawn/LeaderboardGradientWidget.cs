﻿using System;
using Sexy;

namespace Lawn
{
	internal class LeaderboardGradientWidget : Widget
	{
		public LeaderboardGradientWidget()
		{
			this.mDisabled = true;
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			g.DrawImage(AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_GRADIENT, 0, 0);
			g.DrawImageRotated(AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_GRADIENT, 0, this.mHeight - AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_GRADIENT.mHeight, 3.1415926535897931, AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_GRADIENT.mWidth / 2, AtlasResources.IMAGE_PILE_LEADERBOARDSCREEN_GRADIENT.mHeight / 2);
		}
	}
}
