using System;
using SexyFramework.Misc;

namespace SexyFramework.Widget
{
	public class PreModalInfo
	{
		public Widget mBaseModalWidget;

		public Widget mPrevBaseModalWidget;

		public Widget mPrevFocusWidget;

		public FlagsMod mPrevBelowModalFlagsMod = new FlagsMod();
	}
}
