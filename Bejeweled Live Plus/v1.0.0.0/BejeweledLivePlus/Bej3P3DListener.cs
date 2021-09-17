using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;

namespace BejeweledLivePlus
{
	public class Bej3P3DListener : MeshListener
	{
		public Bej3P3DListener()
		{
			this.g = null;
		}

		public override void MeshPreLoad(Mesh theMesh)
		{
		}

		public override void MeshHandleProperty(Mesh theMesh, string theMeshName, string theSetName, string thePropertyName, string thePropertyValue)
		{
		}

		public override SharedImageRef MeshLoadTex(Mesh theMesh, string theMeshName, string theSetName, string theTexType, string theFileName)
		{
			bool flag = false;
			SharedImageRef sharedImage = GlobalMembers.gApp.GetSharedImage(string.Format("images\\{0}\\tex\\{1}", GlobalMembers.gApp.mArtRes, BejeweledLivePlus.Misc.Common.GetFileName(theFileName, true)), "", ref flag, false, false);
			if (sharedImage == null)
			{
				sharedImage = GlobalMembers.gApp.GetSharedImage(string.Format("images\\NonResize\\tex\\{0}", BejeweledLivePlus.Misc.Common.GetFileName(theFileName, true)), "", ref flag, false, false);
			}
			if (sharedImage != null)
			{
				if (theFileName.IndexOf("nebula1") == -1)
				{
					sharedImage.GetImage().ReplaceImageFlags(8U);
				}
				sharedImage.GetMemoryImage().mPurgeBits = true;
			}
			return sharedImage;
		}

		public override void MeshPreDraw(Mesh theMesh)
		{
		}

		public override void MeshPostDraw(Mesh theMesh)
		{
		}

		public override void MeshPreDrawSet(Mesh theMesh, string theMeshName, string theSetName, bool hasBump)
		{
		}

		public override void MeshPostDrawSet(Mesh theMesh, string theMeshName, string theSetName)
		{
		}

		public override void MeshPreDeleted(Mesh theMesh)
		{
		}

		public Graphics g;
	}
}
