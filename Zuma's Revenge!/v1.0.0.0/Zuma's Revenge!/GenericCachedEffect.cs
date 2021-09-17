using System;
using SexyFramework.Graphics;

namespace ZumasRevenge
{
	public class GenericCachedEffect
	{
		public GenericCachedEffect(PIEffect e)
		{
			this.mInUse = false;
			this.mEffect = e;
		}

		public bool mInUse;

		public PIEffect mEffect;
	}
}
