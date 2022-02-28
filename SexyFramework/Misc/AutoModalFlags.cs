using System;

namespace Sexy.Misc
{
	public class AutoModalFlags
	{
		public AutoModalFlags(ModalFlags theFlags, FlagsMod mWidgetFlagsMod)
		{
			this.theFlags = theFlags;
			this.mWidgetFlagsMod = mWidgetFlagsMod;
		}

		private ModalFlags theFlags;

		private FlagsMod mWidgetFlagsMod;
	}
}
