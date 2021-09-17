using System;
using Microsoft.Xna.Framework;
using SexyFramework.Graphics;

namespace SexyFramework.Drivers.Graphics
{
	public class XNAVertex
	{
		public static float GetCoord(SexyVertex2D theVertex, int theCoord)
		{
			switch (theCoord)
			{
			case 0:
				return theVertex.x;
			case 1:
				return theVertex.y;
			case 2:
				return theVertex.z;
			case 3:
				return theVertex.u;
			case 4:
				return theVertex.v;
			default:
				return 0f;
			}
		}

		public static Color UnPackColor(uint color)
		{
			return new Color(((int)color >> 16) & 255, ((int)color >> 8) & 255, (int)(color & 255U), ((int)color >> 24) & 255);
		}

		public Color GetXNAColor()
		{
			return new Color(this.mColor.mRed, this.mColor.mGreen, this.mColor.mBlue, this.mColor.mAlpha);
		}

		public void SetPosition(float theX, float theY, float theZ)
		{
		}

		public static uint TexCoordOffset()
		{
			return 24U;
		}

		public Color mColor;
	}
}
