using System;
using System.Collections.Generic;
using System.Linq;

namespace SexyFramework.Misc
{
	public class ObjectPool<T> where T : new()
	{
		public ObjectPool(int size)
		{
			this.mNumAvailObjects = 0;
			this.mNextAvailIndex = 0;
			this.mPoolSize = size;
			this.mFreePools = new List<T>();
			this.mNumAvailObjects += this.mPoolSize;
		}

		public virtual void Dispose()
		{
			this.mFreePools.Clear();
		}

		public void GrowPool()
		{
			this.mNumAvailObjects += this.mPoolSize;
			this.mDataPools.Capacity = this.mNumAvailObjects;
		}

		public T Alloc()
		{
			if (this.mFreePools.Count > 0)
			{
				T result = Enumerable.Last<T>(this.mFreePools);
				this.mFreePools.RemoveAt(this.mFreePools.Count - 1);
				return result;
			}
			return (default(T) == null) ? new T() : default(T);
		}

		public void Free(T thePtr)
		{
			this.mFreePools.Add(thePtr);
		}

		public int mPoolSize;

		public int mNumAvailObjects;

		public List<T> mDataPools;

		public List<T> mFreePools;

		public int mNextAvailIndex;
	}
}
