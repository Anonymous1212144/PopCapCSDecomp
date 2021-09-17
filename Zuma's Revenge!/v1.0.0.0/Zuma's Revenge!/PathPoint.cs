using System;

namespace ZumasRevenge
{
	public class PathPoint
	{
		public PathPoint(float tx, float ty, float dist)
		{
			this.x = tx;
			this.y = ty;
			this.mDist = dist;
			this.t = 0f;
			this.mPriority = 0;
			this.mInTunnel = false;
			this.mEndPoint = false;
			this.mSplinePoint = false;
			this.mSelected = false;
		}

		public PathPoint(float tx, float ty)
		{
			this.x = tx;
			this.y = ty;
			this.mDist = 0f;
			this.t = 0f;
			this.mPriority = 0;
			this.mInTunnel = false;
			this.mEndPoint = false;
			this.mSplinePoint = false;
			this.mSelected = false;
		}

		public PathPoint()
		{
			this.x = 0f;
			this.y = 0f;
			this.mDist = 0f;
			this.t = 0f;
			this.mPriority = 0;
			this.mInTunnel = false;
			this.mEndPoint = false;
			this.mSplinePoint = false;
			this.mSelected = false;
		}

		public float x;

		public float y;

		public float mDist;

		public float t;

		public byte mPriority;

		public bool mInTunnel;

		public bool mEndPoint;

		public bool mSplinePoint;

		public bool mSelected;
	}
}
