using System;

namespace Sexy.WidgetsLib
{
	public interface ScrollWidgetListener
	{
		void ScrollTargetReached(ScrollWidget scrollWidget);

		void ScrollTargetInterrupted(ScrollWidget scrollWidget);
	}
}
