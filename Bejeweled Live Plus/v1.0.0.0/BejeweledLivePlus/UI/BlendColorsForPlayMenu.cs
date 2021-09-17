using System;

namespace BejeweledLivePlus.UI
{
	public class BlendColorsForPlayMenu
	{
		public static int BlendColors(int theFromColor, int theToColor, float theLerp, bool theBlendAlpha)
		{
			float num = 1f - theLerp;
			return (int)((!theBlendAlpha) ? ((long)theFromColor & (long)((ulong)(-16777216))) : ((long)((long)((int)((float)(theFromColor >> 24) * num + (float)((theToColor >> 24) & 255) * theLerp)) << 24))) | ((int)((float)((theFromColor >> 16) & 255) * num + (float)((theToColor >> 16) & 255) * theLerp) << 16) | ((int)((float)((theFromColor >> 8) & 255) * num + (float)((theToColor >> 8) & 255) * theLerp) << 8) | (int)((float)(theFromColor & 255) * num + (float)(theToColor & 255) * theLerp);
		}
	}
}
