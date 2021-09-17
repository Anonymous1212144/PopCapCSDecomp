using System;
using JeffLib;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class LeaderBoardsButtonWidget : ExtraSexyButton
	{
		public LeaderBoardsButtonWidget(int theId, LeaderBoards theListener)
			: base(theId, theListener)
		{
			this.mUsesAnimators = false;
			this.mLeaderBoards = theListener;
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}

		public LeaderBoards mLeaderBoards;
	}
}
