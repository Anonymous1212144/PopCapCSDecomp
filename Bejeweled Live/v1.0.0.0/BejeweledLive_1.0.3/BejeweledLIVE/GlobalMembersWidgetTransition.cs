using System;
using Sexy;

namespace BejeweledLIVE
{
	public static class GlobalMembersWidgetTransition
	{
		internal static TriVertex Project2D(float x, float y, float z, float theEyeScreenDist, float u, float v)
		{
			return new TriVertex(x * theEyeScreenDist / z, y * theEyeScreenDist / z, u, v);
		}

		internal static float DegToRad(float theDeg)
		{
			return (float)((double)theDeg * 3.14159 / 180.0);
		}
	}
}
