using System;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Widget
{
	public interface Bej3EditListener : EditListener
	{
		void EditWidgetGotFocus(int theId);

		void EditWidgetLostFocus(int theId);
	}
}
