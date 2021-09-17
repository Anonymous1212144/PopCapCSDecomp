using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class AchievementsButtonWidget : ExtraSexyButton
	{
		public AchievementsButtonWidget(int theId, Achievements theListener)
			: base(theId, theListener)
		{
			this.mUsesAnimators = false;
			this.mAchievements = theListener;
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public Achievements mAchievements;
	}
}
