using System;

namespace SexyFramework.Widget
{
	public interface SliderListener
	{
		void SliderVal(int theId, double theVal);

		void SliderReleased(int theId, double theVal);
	}
}
