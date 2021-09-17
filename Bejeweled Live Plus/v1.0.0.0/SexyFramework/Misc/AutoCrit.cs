using System;

namespace SexyFramework.Misc
{
	public class AutoCrit : IDisposable
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
