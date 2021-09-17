using System;
using System.Collections.Generic;

namespace SexyFramework.Misc
{
	public class SexyMathHermite
	{
		public SexyMathHermite()
		{
			this.mIsBuilt = false;
		}

		public void Rebuild()
		{
			this.mIsBuilt = false;
		}

		public float Evaluate(float inX)
		{
			if (!this.mIsBuilt)
			{
				if (!this.BuildCurve())
				{
					return 0f;
				}
				this.mIsBuilt = true;
			}
			int count = this.mPieces.Count;
			for (int i = 0; i < count; i++)
			{
				if (inX < this.mPoints[i + 1].mX)
				{
					this.E_inPoints[0] = this.mPoints[i];
					this.E_inPoints[1] = this.mPoints[i + 1];
					return this.EvaluatePiece(inX, this.E_inPoints, this.mPieces[i]);
				}
			}
			return this.mPoints[this.mPoints.Count - 1].mFx;
		}

		protected void CreatePiece(SexyMathHermite.SPoint[] inPoints, ref SexyMathHermite.SPiece outPiece)
		{
			float[,] array = new float[(int)((UIntPtr)4), (int)((UIntPtr)4)];
			float[] array2 = new float[4];
			for (uint num = 0U; num <= 1U; num += 1U)
			{
				uint num2 = 2U * num;
				array2[(int)((UIntPtr)num2)] = inPoints[(int)((UIntPtr)num)].mX;
				array2[(int)((UIntPtr)(num2 + 1U))] = inPoints[(int)((UIntPtr)num)].mX;
				array[(int)((UIntPtr)num2), (int)((UIntPtr)0)] = inPoints[(int)((UIntPtr)num)].mFx;
				array[(int)((UIntPtr)(num2 + 1U)), (int)((UIntPtr)0)] = inPoints[(int)((UIntPtr)num)].mFx;
				array[(int)((UIntPtr)(num2 + 1U)), (int)((UIntPtr)1)] = inPoints[(int)((UIntPtr)num)].mFxPrime;
				if (num != 0U)
				{
					array[(int)((UIntPtr)num2), (int)((UIntPtr)1)] = (array[(int)((UIntPtr)num2), (int)((UIntPtr)0)] - array[(int)((UIntPtr)(num2 - 1U)), (int)((UIntPtr)0)]) / (array2[(int)((UIntPtr)num2)] - array2[(int)((UIntPtr)(num2 - 1U))]);
				}
			}
			for (uint num3 = 2U; num3 < 4U; num3 += 1U)
			{
				for (uint num4 = 2U; num4 <= num3; num4 += 1U)
				{
					array[(int)((UIntPtr)num3), (int)((UIntPtr)num4)] = (array[(int)((UIntPtr)num3), (int)((UIntPtr)(num4 - 1U))] - array[(int)((UIntPtr)(num3 - 1U)), (int)((UIntPtr)(num4 - 1U))]) / (array2[(int)((UIntPtr)num3)] - array2[(int)((UIntPtr)(num3 - num4))]);
				}
			}
			for (uint num5 = 0U; num5 < 4U; num5 += 1U)
			{
				outPiece.mCoeffs[(int)((UIntPtr)num5)] = array[(int)((UIntPtr)num5), (int)((UIntPtr)num5)];
			}
		}

		protected float EvaluatePiece(float inX, SexyMathHermite.SPoint[] inPoints, SexyMathHermite.SPiece inPiece)
		{
			this.EP_xSub[0] = inX - inPoints[0].mX;
			this.EP_xSub[1] = inX - inPoints[1].mX;
			float num = 1f;
			float num2 = inPiece.mCoeffs[0];
			for (uint num3 = 1U; num3 < 4U; num3 += 1U)
			{
				num *= this.EP_xSub[(int)((UIntPtr)((num3 - 1U) / 2U))];
				num2 += num * inPiece.mCoeffs[(int)((UIntPtr)num3)];
			}
			return num2;
		}

		protected bool BuildCurve()
		{
			this.mPieces.Clear();
			uint count = (uint)this.mPoints.Count;
			if (count < 2U)
			{
				return false;
			}
			uint num = count - 1U;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				SexyMathHermite.SPiece spiece = default(SexyMathHermite.SPiece);
				spiece.mCoeffs = new float[4];
				this.BC_inPoints[0] = this.mPoints[num2];
				this.BC_inPoints[1] = this.mPoints[num2 + 1];
				this.CreatePiece(this.BC_inPoints, ref spiece);
				this.mPieces.Add(spiece);
				num2++;
			}
			return true;
		}

		public List<SexyMathHermite.SPoint> mPoints = new List<SexyMathHermite.SPoint>();

		protected List<SexyMathHermite.SPiece> mPieces = new List<SexyMathHermite.SPiece>();

		protected bool mIsBuilt;

		private SexyMathHermite.SPoint[] E_inPoints = new SexyMathHermite.SPoint[2];

		private float[] EP_xSub = new float[2];

		private SexyMathHermite.SPoint[] BC_inPoints = new SexyMathHermite.SPoint[2];

		public struct SPoint
		{
			public SPoint(float inX, float inFx, float inFxPrime)
			{
				this.mX = inX;
				this.mFx = inFx;
				this.mFxPrime = inFxPrime;
			}

			public float mX;

			public float mFx;

			public float mFxPrime;
		}

		protected struct SPiece
		{
			public float[] mCoeffs;
		}
	}
}
