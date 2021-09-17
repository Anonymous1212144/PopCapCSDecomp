using System;
using SexyFramework.Graphics;

namespace SexyFramework
{
	public class MeshPiece : IDisposable
	{
		public virtual void Dispose()
		{
		}

		public string mObjectName;

		public string mSetName;

		public SharedImageRef mTexture = new SharedImageRef();

		public SharedImageRef mBumpTexture = new SharedImageRef();
	}
}
