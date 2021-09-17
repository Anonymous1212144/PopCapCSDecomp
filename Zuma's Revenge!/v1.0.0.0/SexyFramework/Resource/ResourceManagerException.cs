using System;

namespace SexyFramework.Resource
{
	public class ResourceManagerException : Exception
	{
		public ResourceManagerException(string p)
			: base(p)
		{
			this.msg = p;
		}

		private string msg;
	}
}
