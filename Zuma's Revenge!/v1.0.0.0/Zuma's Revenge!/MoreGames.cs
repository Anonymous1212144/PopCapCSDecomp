using System;
using SexyFramework.Widget;

namespace ZumasRevenge
{
	public class MoreGames : Widget, ButtonListener
	{
		public MoreGames(GameApp gameApp)
		{
			this.gameApp = gameApp;
		}

		public void ButtonPress(int theId)
		{
		}

		public void ButtonPress(int theId, int theClickCount)
		{
		}

		public void ButtonDepress(int theId)
		{
		}

		public void ButtonDownTick(int theId)
		{
		}

		public void ButtonMouseEnter(int theId)
		{
		}

		public void ButtonMouseLeave(int theId)
		{
		}

		public void ButtonMouseMove(int theId, int theX, int theY)
		{
		}

		internal bool IsReadyForDelete()
		{
			return true;
		}

		internal void DoSlide(bool p)
		{
		}

		internal void Init()
		{
		}

		private GameApp gameApp;
	}
}
