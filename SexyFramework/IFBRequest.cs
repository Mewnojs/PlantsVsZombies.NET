using System;

namespace Sexy
{
	public abstract class IFBRequest
	{
		public virtual void Dispose()
		{
		}

		public abstract void Cancel();
	}
}
