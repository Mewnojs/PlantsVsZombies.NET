using System;

namespace Sexy
{
	internal interface ScrollWidgetListener
	{
		void ScrollTargetReached(ScrollWidget scrollWidget);

		void ScrollTargetInterrupted(ScrollWidget scrollWidget);
	}
}
