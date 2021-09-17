﻿using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class SexyMathHermite
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
			uint count = (uint)this.mPieces.Count;
			int num = 0;
			while ((long)num < (long)((ulong)count))
			{
				if (inX < this.mPoints[num + 1].mX)
				{
					return this.EvaluatePiece(inX, this.mPoints, num, this.mPieces[num]);
				}
				num++;
			}
			return this.mPoints[this.mPoints.Count - 1].mFx;
		}

		protected void CreatePiece(List<SexyMathHermite.SPoint> inPoints, int inPointsIndex, List<SexyMathHermite.SPiece> outPiece, int outPieceIndex)
		{
			float[][] array = new float[4][];
			for (uint num = 0U; num < 4U; num += 1U)
			{
				array[(int)((UIntPtr)num)] = new float[4];
			}
			float[] array2 = new float[4];
			for (int i = inPointsIndex; i <= inPointsIndex + 1; i++)
			{
				int num2 = 2 * i;
				array2[num2] = inPoints[i].mX;
				array2[num2 + 1] = inPoints[i].mX;
				array[num2][0] = inPoints[i].mFx;
				array[num2 + 1][0] = inPoints[i].mFx;
				array[num2 + 1][1] = inPoints[i].mFxPrime;
				if (i != 0)
				{
					array[num2][1] = (array[num2][0] - array[num2 - 1][0]) / (array2[num2] - array2[num2 - 1]);
				}
			}
			for (uint num3 = 2U; num3 < 4U; num3 += 1U)
			{
				for (uint num4 = 2U; num4 <= num3; num4 += 1U)
				{
					array[(int)((UIntPtr)num3)][(int)((UIntPtr)num4)] = (array[(int)((UIntPtr)num3)][(int)((UIntPtr)(num4 - 1U))] - array[(int)((UIntPtr)(num3 - 1U))][(int)((UIntPtr)(num4 - 1U))]) / (array2[(int)((UIntPtr)num3)] - array2[(int)((UIntPtr)(num3 - num4))]);
				}
			}
			for (uint num5 = 0U; num5 < 4U; num5 += 1U)
			{
				outPiece[outPieceIndex].mCoeffs[(int)((UIntPtr)num5)] = array[(int)((UIntPtr)num5)][(int)((UIntPtr)num5)];
			}
		}

		protected float EvaluatePiece(float inX, List<SexyMathHermite.SPoint> inPoints, int inPointsIndex, SexyMathHermite.SPiece inPiece)
		{
			float[] array = new float[]
			{
				inX - inPoints[0].mX,
				inX - inPoints[1].mX
			};
			float num = 1f;
			float num2 = inPiece.mCoeffs[0];
			for (uint num3 = 1U; num3 < 4U; num3 += 1U)
			{
				num *= array[(int)((UIntPtr)((num3 - 1U) / 2U))];
				num2 += num * inPiece.mCoeffs[(int)((UIntPtr)num3)];
			}
			return num2;
		}

		protected bool BuildCurve()
		{
			this.mPieces.Clear();
			int count = this.mPoints.Count;
			if (count < 2)
			{
				return false;
			}
			int num = count - 1;
			this.mPieces.Capacity = num;
			for (int i = 0; i < num; i++)
			{
				this.CreatePiece(this.mPoints, i, this.mPieces, i);
			}
			return true;
		}

		public List<SexyMathHermite.SPoint> mPoints = new List<SexyMathHermite.SPoint>();

		protected List<SexyMathHermite.SPiece> mPieces = new List<SexyMathHermite.SPiece>();

		protected bool mIsBuilt;

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

		protected class SPiece
		{
			public float[] mCoeffs = new float[4];
		}
	}
}
