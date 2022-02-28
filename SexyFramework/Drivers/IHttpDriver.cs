using System;

namespace Sexy.Drivers
{
	public abstract class IHttpDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract void Update();

		public abstract IHttpTransaction CreateHttpTransaction(string method, string url);

		public abstract IHttpDriver.NetworkStatus GetNetworkStatus();

		public abstract void AddNetworkStatusListener(INetworkStatusListener listener);

		public abstract void RemoveNetworkStatusListener(INetworkStatusListener listener);

		public abstract string GetPrimaryMACAddress();

		public enum NetworkStatus
		{
			NET_NOT_REACHABLE,
			NET_REACHABLE_WWAN,
			NET_REACHABLE_WIFI,
			NET_REACHABILITY_UNKNOWN
		}
	}
}
