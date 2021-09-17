using System;

namespace SexyFramework.Drivers
{
	public abstract class INetworkStatusListener
	{
		public virtual void Dispose()
		{
		}

		public abstract void NetworkStatusChanged(IHttpDriver.NetworkStatus status);
	}
}
