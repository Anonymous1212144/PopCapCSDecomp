using System;
using SexyFramework.Graphics;
using SexyFramework.Resource;

namespace SexyFramework.Widget
{
	public class PAParticleEffect
	{
		public ResourceRef mResourceRef = new ResourceRef();

		public PIEffect mEffect;

		public string mName;

		public int mLastUpdated;

		public bool mBehind;

		public bool mAttachEmitter;

		public bool mTransform;

		public double mXOfs;

		public double mYOfs;
	}
}
