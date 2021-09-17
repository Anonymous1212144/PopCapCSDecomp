using System;

namespace Sexy
{
	public interface ScrollWidgetListener
	{
		void ScrollTargetReached(ScrollWidget scrollWidget);

		void ScrollTargetInterrupted(ScrollWidget scrollWidget);
	}
}
