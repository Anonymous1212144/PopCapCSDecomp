using System;
using System.Collections.Generic;

namespace Sexy
{
	internal static class stdC
	{
		public static double sqrtf(float x)
		{
			return Math.Sqrt((double)x);
		}

		public static int floorf(double x)
		{
			return (int)Math.Floor(x);
		}

		public static float fabsf(float x)
		{
			return Math.Abs(x);
		}

		internal static void swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		internal static void swapCollection<T>(IList<T> list, int indexA, int indexB)
		{
			T t = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = t;
		}
	}
}
