using System;

namespace SexyFramework.Widget
{
	public interface ScrollWidgetListener
	{
		void ScrollTargetReached(ScrollWidget scrollWidget);

		void ScrollTargetInterrupted(ScrollWidget scrollWidget);
	}
}
