using System;

namespace SexyFramework.PIL
{
	public class FreeEmitterInfo
	{
		public FreeEmitterInfo(FreeEmitter f, int s)
		{
			this.first = f;
			this.second = s;
		}

		public FreeEmitter first;

		public int second;
	}
}
