using System;
using SexyFramework.Graphics;

namespace SexyFramework.Resource
{
	public class PIEffectRes : BaseRes
	{
		public PIEffectRes()
		{
			this.mType = ResType.ResType_PIEffect;
			this.mPIEffect = null;
		}

		public override void DeleteResource()
		{
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				this.mResourceRef.Release();
			}
			this.mPIEffect = null;
			if (this.mGlobalPtr != null)
			{
				this.mGlobalPtr.mResObject = null;
			}
		}

		public PIEffect mPIEffect;
	}
}
