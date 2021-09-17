using System;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class WidescreenBoardWidget : Widget
	{
		public WidescreenBoardWidget()
		{
			this.mWidgetFlagsMod.mRemoveFlags |= 5;
			this.mApp = GameApp.gApp;
			this.mZOrder = 2147483646;
		}

		public override void MouseDown(int x, int y, int theClickCount)
		{
			if (this.mApp.GetBoard() != null)
			{
				this.mApp.GetBoard().MouseDown(x - Common._S(80), y, theClickCount);
			}
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			if (this.mApp.GetBoard() != null)
			{
				this.mApp.GetBoard().MouseUp(x - Common._S(80), y, theClickCount);
			}
		}

		public override bool IsPointVisible(int x, int y)
		{
			return this.mApp.GetBoard() != null && (x < Common._S(80) || x > this.mApp.mWidth + Common._S(80));
		}

		public override void MouseMove(int x, int y)
		{
			if (this.mApp.GetBoard() != null)
			{
				this.mApp.GetBoard().MouseMove(x - Common._S(80), y);
			}
		}

		public override void MouseDrag(int x, int y)
		{
			if (this.mApp.GetBoard() != null)
			{
				this.mApp.GetBoard().MouseMove(x - Common._S(80), y);
			}
		}

		public GameApp mApp;
	}
}
