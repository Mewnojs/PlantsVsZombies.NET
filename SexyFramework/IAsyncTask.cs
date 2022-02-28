using System;

namespace Sexy
{
	public abstract class IAsyncTask
	{
		public virtual void Dispose()
		{
		}

		public abstract int IsDone();

		public abstract int HasError();

		public abstract void Destroy();
	}
}
