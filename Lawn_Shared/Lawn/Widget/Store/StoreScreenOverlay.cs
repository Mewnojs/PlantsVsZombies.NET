using System;
using Sexy;
using Sexy.GraphicsLib;
using Sexy.WidgetsLib;

namespace Lawn
{
    internal class StoreScreenOverlay : Widget
    {
        public StoreScreenOverlay(StoreScreen theParent)
        {
            mParent = theParent;
            mMouseVisible = false;
            mHasAlpha = true;
        }

        public override void Draw(Graphics g)
        {
            mParent.DrawOverlay(g);
        }

        public new StoreScreen mParent;
    }
}
