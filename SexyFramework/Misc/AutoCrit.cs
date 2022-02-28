using System;

namespace Sexy.Misc
{
	public struct AutoCrit
	{
		public AutoCrit(CritSect theCritSect)
		{
			this.mCritSec = theCritSect;
			this.mCritSec.Lock();
		}

		public void Dispose()
		{
			this.mCritSec.Unlock();
		}

		private CritSect mCritSec;
	}
}
