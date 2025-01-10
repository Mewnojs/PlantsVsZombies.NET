using System;

namespace Sexy
{
    public/*internal*/ interface ScrollWidgetListener
    {
        void ScrollTargetReached(ScrollWidget scrollWidget);

        void ScrollTargetInterrupted(ScrollWidget scrollWidget);
    }
}
