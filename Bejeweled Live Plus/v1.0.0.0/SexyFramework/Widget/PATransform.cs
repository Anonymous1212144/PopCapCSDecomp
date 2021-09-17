using System;
using SexyFramework.Misc;

namespace SexyFramework.Widget
{
	public struct PATransform
	{
		public PATransform Clone()
		{
			PATransform result = new PATransform(true);
			this.mMatrix.CopyTo(result.mMatrix);
			return result;
		}

		public PATransform(bool init)
		{
			this.mMatrix = new SexyTransform2D(true);
			this.mMatrix.LoadIdentity();
		}

		public void CopyFrom(PATransform rhs)
		{
			this.mMatrix.CopyFrom(rhs.mMatrix);
		}

		public void TransformSrc(PATransform theSrcTransform, ref PATransform outTran)
		{
			outTran.mMatrix.CopyFrom(this.mMatrix * theSrcTransform.mMatrix);
		}

		public void InterpolateTo(PATransform theNextTransform, float thePct, ref PATransform outTran)
		{
			outTran.mMatrix.mMatrix = this.mMatrix.mMatrix * (1f - thePct) + theNextTransform.mMatrix.mMatrix * thePct;
		}

		public SexyTransform2D mMatrix;
	}
}
