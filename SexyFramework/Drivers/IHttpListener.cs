using System;

namespace Sexy.Drivers
{
	public class IHttpListener
	{
		public virtual void Dispose()
		{
		}

		public virtual void HttpReceivedResponse(IHttpTransaction http)
		{
		}

		public virtual void HttpReceivedData(IHttpTransaction http, IntPtr data, uint length)
		{
		}

		public virtual void HttpTransactionComplete(IHttpTransaction http)
		{
		}

		public virtual void HttpTransactionError(IHttpTransaction http)
		{
		}
	}
}
