using System;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public class WayPoint
	{
		public WayPoint()
		{
			this.mHavePerpendicular = false;
			this.mHaveAvgRotation = false;
			this.mInTunnel = false;
			this.mHavePerpendicular = false;
			this.mPriority = 0;
		}

		public WayPoint(float theX, float theY)
		{
			this.x = theX;
			this.y = theY;
			this.mHavePerpendicular = false;
			this.mHaveAvgRotation = false;
			this.mInTunnel = false;
			this.mHavePerpendicular = false;
			this.mPriority = 0;
		}

		public static float GetCanonicalAngle(float r)
		{
			if (r > 0f)
			{
				while (r > 3.14159274f)
				{
					r -= 6.28318548f;
				}
			}
			else if (r < 0f)
			{
				while (r < -3.14159274f)
				{
					r += 6.28318548f;
				}
			}
			return r;
		}

		public float x;

		public float y;

		public bool mHavePerpendicular;

		public bool mHaveAvgRotation;

		public SexyVector3 mPerpendicular = default(SexyVector3);

		public float mRotation;

		public float mAvgRotation;

		public bool mInTunnel;

		public byte mPriority;
	}
}
