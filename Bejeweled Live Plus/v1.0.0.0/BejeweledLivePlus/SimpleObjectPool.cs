using System;
using System.Collections.Generic;
using System.Linq;

namespace BejeweledLivePlus
{
	public class SimpleObjectPool
	{
		public SimpleObjectPool(int size, Type contentType)
		{
			this.contentType_ = contentType;
			this.deadObjects_ = new List<object>();
		}

		public object alloc()
		{
			object result;
			if (this.deadObjects_.Count > 0)
			{
				result = Enumerable.Last<object>(this.deadObjects_);
				this.deadObjects_.RemoveAt(this.deadObjects_.Count - 1);
			}
			else
			{
				result = Activator.CreateInstance(this.contentType_);
			}
			return result;
		}

		public void release(object p)
		{
			this.deadObjects_.Add(p);
		}

		private List<object> deadObjects_;

		private Type contentType_;
	}
}
