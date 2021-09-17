using System;
using System.Collections.Generic;

namespace Sexy
{
	public class SexyMathHermite
	{
		public static SexyMathHermite GetNewSexyMathHermite()
		{
			if (SexyMathHermite.unusedObjects.Count > 0)
			{
				return SexyMathHermite.unusedObjects.Pop();
			}
			return new SexyMathHermite();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			SexyMathHermite.unusedObjects.Push(this);
		}

		public void Reset()
		{
			this.mIsBuilt = false;
			this.mPoints.Clear();
		}

		private SexyMathHermite()
		{
			this.Reset();
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
			for (int i = 0; i < this.mPieces.Count; i++)
			{
				if (inX < this.mPoints[i + 1].mX)
				{
					return this.EvaluatePiece(inX, this.mPoints, i, this.mPieces[i]);
				}
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
				int num2 = 2 * (i - inPointsIndex);
				array2[num2] = inPoints[i].mX;
				array2[num2 + 1] = inPoints[i].mX;
				array[num2][0] = inPoints[i].mFx;
				array[num2 + 1][0] = inPoints[i].mFx;
				array[num2 + 1][1] = inPoints[i].mFxPrime;
				if (i - inPointsIndex != 0)
				{
					array[num2][1] = (array[num2][0] - array[num2 - 1][0]) / (array2[num2] - array2[num2 - 1]);
				}
			}
			for (int j = 2; j < 4; j++)
			{
				for (int k = 2; k <= j; k++)
				{
					array[j][k] = (array[j][k - 1] - array[j - 1][k - 1]) / (array2[j] - array2[j - k]);
				}
			}
			for (int l = 0; l < 4; l++)
			{
				outPiece[outPieceIndex].mCoeffs[l] = array[l][l];
			}
		}

		protected float EvaluatePiece(float inX, List<SexyMathHermite.SPoint> inPoints, int inPointsIndex, SexyMathHermite.SPiece inPiece)
		{
			this.xSub[0] = inX - inPoints[inPointsIndex].mX;
			this.xSub[1] = inX - inPoints[inPointsIndex + 1].mX;
			float num = 1f;
			float num2 = inPiece.mCoeffs[0];
			for (uint num3 = 1U; num3 < 4U; num3 += 1U)
			{
				num *= this.xSub[(int)((UIntPtr)((num3 - 1U) / 2U))];
				num2 += num * inPiece.mCoeffs[(int)((UIntPtr)num3)];
			}
			return num2;
		}

		protected bool BuildCurve()
		{
			int count = this.mPoints.Count;
			if (count < 2)
			{
				return false;
			}
			int num = count - 1;
			while (this.mPieces.Count > num)
			{
				this.mPieces[this.mPieces.Count - 1].PrepareForReuse();
				this.mPieces.RemoveAt(this.mPieces.Count - 1);
			}
			while (this.mPieces.Count < num)
			{
				this.mPieces.Add(SexyMathHermite.SPiece.GetNewSPiece());
			}
			for (int i = 0; i < num; i++)
			{
				this.CreatePiece(this.mPoints, i, this.mPieces, i);
			}
			return true;
		}

		public List<SexyMathHermite.SPoint> mPoints = new List<SexyMathHermite.SPoint>();

		private static Stack<SexyMathHermite> unusedObjects = new Stack<SexyMathHermite>();

		protected List<SexyMathHermite.SPiece> mPieces = new List<SexyMathHermite.SPiece>();

		protected bool mIsBuilt;

		private float[] xSub = new float[2];

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
			private SPiece()
			{
			}

			public void PrepareForReuse()
			{
				SexyMathHermite.SPiece.unusedObjects.Push(this);
			}

			public static SexyMathHermite.SPiece GetNewSPiece()
			{
				if (SexyMathHermite.SPiece.unusedObjects.Count > 0)
				{
					return SexyMathHermite.SPiece.unusedObjects.Pop();
				}
				return new SexyMathHermite.SPiece();
			}

			public float[] mCoeffs = new float[4];

			private static Stack<SexyMathHermite.SPiece> unusedObjects = new Stack<SexyMathHermite.SPiece>();
		}
	}
}
