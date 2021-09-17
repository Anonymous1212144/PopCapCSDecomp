﻿using System;

namespace Sexy
{
	internal static class GlobalMembersInterpolator
	{
		public static float tlerp(float t, float a, float b)
		{
			return a + t * (b - a);
		}

		public static TPoint tlerp(float t, TPoint a, TPoint b)
		{
			return new TPoint(GlobalMembersInterpolator.tlerp(t, a.mX, b.mX), GlobalMembersInterpolator.tlerp(t, a.mY, b.mY));
		}

		public static SexyColor tlerp(float t, SexyColor a, SexyColor b)
		{
			return new SexyColor(GlobalMembersInterpolator.tlerp(t, a.mRed, b.mRed), GlobalMembersInterpolator.tlerp(t, a.mGreen, b.mGreen), GlobalMembersInterpolator.tlerp(t, a.mBlue, b.mBlue), GlobalMembersInterpolator.tlerp(t, a.mAlpha, b.mAlpha));
		}

		public static int tlerp(float t, int a, int b)
		{
			return (int)((float)a + t * (float)(b - a));
		}

		internal const bool kTween = true;

		internal const bool kNoTween = false;

		internal const bool kEase = true;

		internal const bool kNoEase = false;
	}
}
