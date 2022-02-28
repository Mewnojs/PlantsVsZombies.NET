using System;

namespace Sexy.WidgetsLib
{
	public interface SliderListener
	{
		void SliderVal(int theId, double theVal);

		void SliderReleased(int theId, double theVal);
	}
}
