using System;
using System.Collections.Generic;

namespace SexyFramework
{
	public class Mesh : IDisposable
	{
		public Mesh()
		{
			this.mListener = null;
			this.mUserData = null;
			GlobalMembers.gSexyAppBase.mGraphicsDriver.AddMesh(this);
		}

		public void Dispose()
		{
			if (this.mListener != null)
			{
				this.mListener.MeshPreDeleted(this);
			}
			this.Cleanup();
		}

		public virtual void Cleanup()
		{
			foreach (MeshPiece meshPiece in this.mPieces)
			{
				if (meshPiece != null)
				{
					meshPiece.Dispose();
				}
			}
			this.mPieces.Clear();
		}

		public virtual void SetListener(MeshListener theListener)
		{
			this.mListener = theListener;
		}

		public string mFileName;

		public MeshListener mListener;

		public object mUserData;

		public List<MeshPiece> mPieces = new List<MeshPiece>();
	}
}
