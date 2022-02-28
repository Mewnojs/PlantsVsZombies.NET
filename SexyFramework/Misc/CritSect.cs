using System;

namespace Sexy.Misc
{
	public struct CritSect
	{
		public bool TryLock()
		{
			return false;
		}

		public void Lock()
		{
		}

		public void Unlock()
		{
		}
	}
}
