using System;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class DistortEffect
	{
		public DistortEffect()
		{
			this.mType = 0;
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_TEXTURE_POS, this.mTexturePos);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_RADIUS, this.mRadius);
			GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eEFFECTS_INTENSITY, this.mIntensity);
			this.mMovePct.SetConstant(0.0);
		}

		public int mType;

		public FPoint mCenter = default(FPoint);

		public FPoint mMoveDelta = default(FPoint);

		public CurvedVal mTexturePos = new CurvedVal();

		public CurvedVal mMovePct = new CurvedVal();

		public CurvedVal mIntensity = new CurvedVal();

		public CurvedVal mRadius = new CurvedVal();
	}
}
