using System;

namespace BejeweledLivePlus.Widget
{
	public class Bej3SlideSelectorContainer : Bej3WidgetBase
	{
		public override void Dispose()
		{
			this.RemoveAllWidgets(true, true);
		}
	}
}
