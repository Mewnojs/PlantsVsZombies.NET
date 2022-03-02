using Sexy.Misc;
using System;

namespace Sexy.WidgetsLib
{
    public interface EditListener
    {
        void EditWidgetText(int theId, string theString);

        bool AllowChar(int theId, char theChar);

        public virtual bool AllowKey(int theId, KeyCode theKey)
        {
            return true;
        }

        bool AllowText(int theId, ref string theText);

        bool ShouldClear();
    }
}
