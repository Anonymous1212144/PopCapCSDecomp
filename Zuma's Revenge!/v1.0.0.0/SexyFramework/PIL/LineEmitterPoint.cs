﻿using System;
using System.Collections.Generic;
using SexyFramework.Misc;

namespace SexyFramework.PIL
{
	public class LineEmitterPoint
	{
		public virtual void Serialize(Buffer b)
		{
			b.WriteFloat(this.mCurX);
			b.WriteFloat(this.mCurY);
			b.WriteLong((long)this.mKeyFramePoints.Count);
			for (int i = 0; i < this.mKeyFramePoints.Count; i++)
			{
				b.WriteLong((long)this.mKeyFramePoints[i].first);
				b.WriteLong((long)this.mKeyFramePoints[i].second.mX);
				b.WriteLong((long)this.mKeyFramePoints[i].second.mY);
			}
		}

		public virtual void Deserialize(Buffer b)
		{
			this.mCurX = b.ReadFloat();
			this.mCurY = b.ReadFloat();
			int num = (int)b.ReadLong();
			this.mKeyFramePoints.Clear();
			for (int i = 0; i < num; i++)
			{
				int f = (int)b.ReadLong();
				int theX = (int)b.ReadLong();
				int theY = (int)b.ReadLong();
				this.mKeyFramePoints.Add(new PointKeyFrame(f, new Point(theX, theY)));
			}
		}

		public List<PointKeyFrame> mKeyFramePoints = new List<PointKeyFrame>();

		public float mCurX;

		public float mCurY;
	}
}
