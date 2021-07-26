using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class PreModalInfo
	{
		public void PrepareForReuse()
		{
			this.Reset();
			PreModalInfo.unusedObjects.Push(this);
		}

		public static PreModalInfo GetNewPreModalInfo()
		{
			if (PreModalInfo.unusedObjects.Count > 0)
			{
				return PreModalInfo.unusedObjects.Pop();
			}
			return new PreModalInfo();
		}

		private void Reset()
		{
			this.mBaseModalWidget = null;
			this.mPrevBaseModalWidget = null;
			this.mPrevFocusWidget = null;
			this.mPrevBelowModalFlagsMod = default(FlagsMod);
		}

		private PreModalInfo()
		{
			this.Reset();
		}

		public Widget mBaseModalWidget;

		public Widget mPrevBaseModalWidget;

		public Widget mPrevFocusWidget;

		public FlagsMod mPrevBelowModalFlagsMod = default(FlagsMod);

		private static Stack<PreModalInfo> unusedObjects = new Stack<PreModalInfo>();
	}
}
