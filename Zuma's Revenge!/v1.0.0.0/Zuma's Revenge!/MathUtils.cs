using System;
using SexyFramework.Misc;

namespace ZumasRevenge
{
	public static class MathUtils
	{
		public static int SafeRand()
		{
			return MathUtils.mRandomGen.Next();
		}

		public static int Rand(int range)
		{
			return MathUtils.mRandomGen.Next() % range;
		}

		public static void Seed(int seed)
		{
			MathUtils.mRandomGen = new Random(seed);
		}

		public static int Rand()
		{
			return MathUtils.mRandomGen.Next();
		}

		public static float RadiansToDegrees(float pRads)
		{
			return pRads * 57.29694f;
		}

		public static float DegreesToRadians(float pDegs)
		{
			return pDegs * 0.0174529385f;
		}

		public static int Sign(int val)
		{
			if (val >= 0)
			{
				return 1;
			}
			return -1;
		}

		public static float Sign(float val)
		{
			if (val >= 0f)
			{
				return 1f;
			}
			return -1f;
		}

		public static bool _eq(float n1, float n2, float tolerance)
		{
			return Math.Abs(n1 - n2) <= tolerance;
		}

		public static bool _leq(float n1, float n2, float tolerance)
		{
			return MathUtils._eq(n1, n2, tolerance) || n1 < n2;
		}

		public static bool _geq(float n1, float n2, float tolerance)
		{
			return MathUtils._eq(n1, n2, tolerance) || n1 > n2;
		}

		public static bool _eq(float n1, float n2)
		{
			return Math.Abs(n1 - n2) <= float.Epsilon;
		}

		public static bool _leq(float n1, float n2)
		{
			return MathUtils._eq(n1, n2, float.Epsilon) || n1 < n2;
		}

		public static bool _geq(float n1, float n2)
		{
			return MathUtils._eq(n1, n2, float.Epsilon) || n1 > n2;
		}

		public static int IntRange(int min_val, int max_val)
		{
			if (min_val == max_val)
			{
				return min_val;
			}
			if (min_val < 0 && max_val < 0)
			{
				return min_val + MathUtils.SafeRand() % (Math.Abs(min_val) - Math.Abs(max_val));
			}
			return min_val + MathUtils.SafeRand() % (max_val - min_val + 1);
		}

		public static float FloatRange(float min_val, float max_val)
		{
			if (min_val == max_val)
			{
				return min_val;
			}
			if (min_val < 0f && max_val < 0f)
			{
				return min_val + (float)(MathUtils.SafeRand() % (int)((Math.Abs(min_val) - Math.Abs(max_val)) * 1E+08f + 1f)) / 1E+08f;
			}
			return min_val + (float)(MathUtils.SafeRand() % (int)((max_val - min_val) * 1E+08f + 1f)) / 1E+08f;
		}

		public static void Clamp(ref int value, int min_val, int max_val)
		{
			if (value < min_val)
			{
				value = min_val;
				return;
			}
			if (value > max_val)
			{
				value = max_val;
			}
		}

		public static bool IncrementAndClamp(ref float val, float target, float inc)
		{
			val += inc;
			if (inc > 0f && val >= target)
			{
				val = target;
				return true;
			}
			if (inc < 0f && val <= target)
			{
				val = target;
				return true;
			}
			return false;
		}

		public static int GetClosestPowerOf2Above(int theNum)
		{
			int i;
			for (i = 1; i < theNum; i <<= 1)
			{
			}
			return i;
		}

		public static bool IsPowerOf2(int theNum)
		{
			int num = 0;
			while (theNum > 0)
			{
				num += theNum & 1;
				theNum >>= 1;
			}
			return num == 1;
		}

		public static float Distance(Point p1, Point p2, bool sqrt)
		{
			float num = (float)(p2.mX - p1.mX);
			float num2 = (float)(p2.mY - p1.mY);
			float num3 = num * num + num2 * num2;
			if (!sqrt)
			{
				return num3;
			}
			return (float)Math.Sqrt((double)num3);
		}

		public static float Distance(Point p1, Point p2)
		{
			return MathUtils.Distance(p1, p2, true);
		}

		public static float Distance(float p1x, float p1y, float p2x, float p2y, bool sqrt)
		{
			float num = p2x - p1x;
			float num2 = p2y - p1y;
			float num3 = num * num + num2 * num2;
			if (!sqrt)
			{
				return num3;
			}
			return (float)Math.Sqrt((double)num3);
		}

		public static float Distance(float p1x, float p1y, float p2x, float p2y)
		{
			return MathUtils.Distance(p1x, p1y, p2x, p2y, true);
		}

		public static bool CirclesIntersect(float x1, float y1, float x2, float y2, float total_radius, ref float seperation)
		{
			float num = x1 - x2;
			float num2 = y1 - y2;
			float num3 = num * num + num2 * num2;
			seperation = num3;
			return num3 < total_radius * total_radius;
		}

		public static bool CirclesIntersect(float x1, float y1, float x2, float y2, float total_radius)
		{
			float num = 0f;
			return MathUtils.CirclesIntersect(x1, y1, x2, y2, total_radius, ref num);
		}

		public const float EPSILON = 1E-06f;

		public const float JL_PI = 3.14159274f;

		private static Random mRandomGen = new Random();
	}
}
