using System;
using SexyFramework.Misc;

namespace SexyFramework.Widget
{
	public interface EditListener
	{
		void EditWidgetText(int theId, string theString);

		bool AllowKey(int theId, KeyCode theKey);

		bool AllowChar(int theId, char theChar);

		bool AllowText(int theId, string theText);
	}
}
