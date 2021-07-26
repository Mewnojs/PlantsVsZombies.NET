using System;
using Sexy;

namespace Lawn
{
	internal class StoreScreenOverlay : Widget
	{
		public StoreScreenOverlay(StoreScreen theParent)
		{
			this.mParent = theParent;
			this.mMouseVisible = false;
			this.mHasAlpha = true;
		}

		public override void Draw(Graphics g)
		{
			this.mParent.DrawOverlay(g);
		}

		public new StoreScreen mParent;
	}
}
