using System;

namespace Sexy
{
    public/*internal*/ interface ListListener
    {
        void ListClicked(int theId, int theIdx, int theClickCount);

        void ListClosed(int theId);

        void ListHiliteChanged(int theId, int theOldIdx, int theNewIdx);
    }
}
