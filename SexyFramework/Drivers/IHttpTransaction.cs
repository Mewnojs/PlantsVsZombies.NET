using System;

namespace Sexy.Drivers
{
	public abstract class IHttpTransaction
	{
		public abstract void SetListener(IHttpListener listener);

		public abstract void SetUserData(IntPtr userData);

		public abstract void SetRequestHeader(string name, string value);

		public abstract void SetRequestBody(IntPtr data, uint length);

		public abstract void SetTimeout(int seconds);

		public abstract void Start();

		public abstract void Release();

		public abstract IntPtr GetUserData();

		public abstract int GetStatusCode();

		public abstract string GetStatusLine();

		public abstract int GetResponseLength();

		public abstract string GetResponseHeader(string key);

		public abstract string GetSerializedRequest();

		public abstract string GetErrorMessage();

		public virtual void Dispose()
		{
		}
	}
}
