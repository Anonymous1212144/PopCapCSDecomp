using System;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public interface Bej3ScrollWidgetListener : ScrollWidgetListener
	{
		void PageChanged(Bej3ScrollWidget scrollWidget, int pageH, int pageV);
	}
}
