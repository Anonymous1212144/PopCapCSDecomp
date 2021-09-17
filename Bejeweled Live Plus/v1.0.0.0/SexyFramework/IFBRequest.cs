using System;

namespace SexyFramework
{
	public abstract class IFBRequest
	{
		public virtual void Dispose()
		{
		}

		public abstract void Cancel();
	}
}
