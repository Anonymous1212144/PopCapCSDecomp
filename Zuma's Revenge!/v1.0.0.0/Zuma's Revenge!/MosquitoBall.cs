using System;
using System.Collections.Generic;

namespace ZumasRevenge
{
	public class MosquitoBall : IDisposable
	{
		public virtual void Dispose()
		{
			this.mMosquitoes.Clear();
		}

		public List<Mosquito> mMosquitoes = new List<Mosquito>();
	}
}
