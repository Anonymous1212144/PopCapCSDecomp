using System;

namespace SexyFramework.Resource
{
	public abstract class DataElement : IDisposable
	{
		public DataElement()
		{
			this.mIsList = false;
		}

		public virtual void Dispose()
		{
		}

		public abstract DataElement Duplicate();

		public bool mIsList;
	}
}
