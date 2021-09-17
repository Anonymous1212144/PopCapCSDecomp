using System;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public class Curve
	{
		public Curve()
		{
			for (int i = 0; i < 4; i++)
			{
				this.mPoint[i] = new FPoint(0f, 0f);
			}
		}

		public FPoint GetBezierPoint(float theT)
		{
			float num = 1f - theT;
			return new FPoint
			{
				mX = num * num * num * this.mPoint[0].mX + 3f * theT * (num * num) * this.mPoint[1].mX + 3f * theT * theT * num * this.mPoint[2].mX + theT * theT * theT * this.mPoint[3].mX,
				mY = num * num * num * this.mPoint[0].mY + 3f * theT * (num * num) * this.mPoint[1].mY + 3f * theT * theT * num * this.mPoint[2].mY + theT * theT * theT * this.mPoint[3].mY
			};
		}

		public FPoint GetSplinePoint(float t)
		{
			return new FPoint
			{
				mX = this.mPoint[0].mX + 3f * t * (this.mPoint[1].mX - this.mPoint[0].mX) + t * t / 2f * 6f * (this.mPoint[0].mX - 2f * this.mPoint[1].mX + this.mPoint[2].mX) + t * t * t * (-this.mPoint[0].mX + 3f * this.mPoint[1].mX - 3f * this.mPoint[2].mX + this.mPoint[3].mX),
				mY = this.mPoint[0].mY + 3f * t * (this.mPoint[1].mY - this.mPoint[0].mY) + t * t / 2f * 6f * (this.mPoint[0].mY - 2f * this.mPoint[1].mY + this.mPoint[2].mY) + t * t * t * (-this.mPoint[0].mY + 3f * this.mPoint[1].mY - 3f * this.mPoint[2].mY + this.mPoint[3].mY)
			};
		}

		public const int mPointCount = 4;

		public FPoint[] mPoint = new FPoint[4];
	}
}
