using System;
using SexyFramework.Misc;

namespace BejeweledLivePlus
{
	public class Lightning : IDisposable
	{
		public Lightning()
		{
			for (int i = 0; i < Bej3Com.NUM_LIGTNING_POINTS; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					this.mPoints[i, j] = default(FPoint);
				}
			}
		}

		public void Dispose()
		{
		}

		public FPoint[,] mPoints = new FPoint[Bej3Com.NUM_LIGTNING_POINTS, 2];

		public float mPercentDone;

		public float mPullX;

		public float mPullY;
	}
}
