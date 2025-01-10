using System;

namespace Sexy
{
    public interface ButtonListener
    {
        void ButtonPress(int theId);

        void ButtonPress(int theId, int theClickCount);

        void ButtonDepress(int theId);

        void ButtonDownTick(int theId);

        void ButtonMouseEnter(int theId);

        void ButtonMouseLeave(int theId);

        void ButtonMouseMove(int theId, int theX, int theY);
    }
}
