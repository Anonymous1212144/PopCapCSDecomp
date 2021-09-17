using System;
using SexyFramework.Graphics;

namespace SexyFramework
{
	public abstract class MeshListener
	{
		public abstract void MeshPreLoad(Mesh theMesh);

		public abstract void MeshHandleProperty(Mesh theMesh, string theMeshName, string theSetName, string thePropertyName, string thePropertyValue);

		public abstract SharedImageRef MeshLoadTex(Mesh theMesh, string theMeshName, string theSetName, string theTexType, string theFileName);

		public abstract void MeshPreDraw(Mesh theMesh);

		public abstract void MeshPostDraw(Mesh theMesh);

		public abstract void MeshPreDrawSet(Mesh theMesh, string theMeshName, string theSetName, bool hasBump);

		public abstract void MeshPostDrawSet(Mesh theMesh, string theMeshName, string theSetName);

		public abstract void MeshPreDeleted(Mesh theMesh);
	}
}
