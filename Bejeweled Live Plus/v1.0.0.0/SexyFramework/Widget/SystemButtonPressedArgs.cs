using System;

namespace SexyFramework.Widget
{
	public class SystemButtonPressedArgs : EventArgs
	{
		public bool processed;

		public SystemButtons button;
	}
}
