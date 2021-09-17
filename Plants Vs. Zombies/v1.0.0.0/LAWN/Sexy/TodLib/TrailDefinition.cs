﻿using System;

namespace Sexy.TodLib
{
	internal class TrailDefinition
	{
		public TrailDefinition()
		{
			this.mMaxPoints = 2;
			this.mMinPointDistance = 1f;
			this.mTrailFlags = 0;
			this.mImage = null;
		}

		public void Dispose()
		{
		}

		public Image mImage;

		public string mImageName;

		public int mMaxPoints;

		public float mMinPointDistance;

		public int mTrailFlags;

		public FloatParameterTrack mTrailDuration = new FloatParameterTrack();

		public FloatParameterTrack mWidthOverLength = new FloatParameterTrack();

		public FloatParameterTrack mWidthOverTime = new FloatParameterTrack();

		public FloatParameterTrack mAlphaOverLength = new FloatParameterTrack();

		public FloatParameterTrack mAlphaOverTime = new FloatParameterTrack();
	}
}
