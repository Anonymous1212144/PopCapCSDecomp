using System;
using System.Collections.Generic;

namespace Sexy
{
	public class BaseRes
	{
		~BaseRes()
		{
			this.DeleteResource();
		}

		public virtual void DeleteResource()
		{
		}

		public ResType mType;

		public string mId;

		public string mResGroup;

		public string mPath;

		public bool mFromProgram;

		public Dictionary<string, string> mXMLAttributes;

		public bool mDelayLoaded;
	}
}
