using System;

namespace SexyFramework.PIL
{
	public class ParticleTypeInfo
	{
		public ParticleTypeInfo(ParticleType f, int s)
		{
			this.first = f;
			this.second = s;
		}

		public ParticleType first;

		public int second;
	}
}
