using System;
using System.Collections.Generic;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class BossBerserkMovement
	{
		public BossBerserkMovement()
		{
			this.mStartX = 0;
			this.mStartY = 0;
			this.mEndX = 0;
			this.mEndY = 0;
			this.mHealthLimit = -1;
			this.mX = int.MaxValue;
			this.mY = int.MaxValue;
		}

		public BossBerserkMovement(BossBerserkMovement rhs)
		{
			this.mStartX = rhs.mStartX;
			this.mStartY = rhs.mStartY;
			this.mEndX = rhs.mEndX;
			this.mEndY = rhs.mEndY;
			this.mHealthLimit = rhs.mHealthLimit;
			this.mX = rhs.mX;
			this.mY = rhs.mY;
			this.mPoints.Clear();
			for (int i = 0; i < rhs.mPoints.Count; i++)
			{
				this.mPoints.Add(new Point(rhs.mPoints[i]));
			}
		}

		public int mStartX;

		public int mEndX;

		public int mStartY;

		public int mEndY;

		public int mX;

		public int mY;

		public int mHealthLimit;

		public List<Point> mPoints = new List<Point>();
	}
}
