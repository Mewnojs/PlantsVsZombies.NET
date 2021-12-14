using System;
using System.Collections.Generic;

namespace Sexy
{
	public/*internal*/ class PreModalInfo
	{
		public void PrepareForReuse()
		{
			Reset();
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
			mBaseModalWidget = null;
			mPrevBaseModalWidget = null;
			mPrevFocusWidget = null;
			mPrevBelowModalFlagsMod = default(FlagsMod);
		}

		private PreModalInfo()
		{
			Reset();
		}

		public Widget mBaseModalWidget;

		public Widget mPrevBaseModalWidget;

		public Widget mPrevFocusWidget;

		public FlagsMod mPrevBelowModalFlagsMod = default(FlagsMod);

		private static Stack<PreModalInfo> unusedObjects = new Stack<PreModalInfo>();
	}
}
