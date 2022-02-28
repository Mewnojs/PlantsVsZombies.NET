using System;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class PreModalInfo
	{
		public Widget mBaseModalWidget;

		public Widget mPrevBaseModalWidget;

		public Widget mPrevFocusWidget;

		public FlagsMod mPrevBelowModalFlagsMod = new FlagsMod();
	}
}
