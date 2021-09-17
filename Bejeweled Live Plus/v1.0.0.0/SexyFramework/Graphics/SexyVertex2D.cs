using System;

namespace SexyFramework.Graphics
{
	public struct SexyVertex2D : SexyVertex
	{
		public SexyVertex2D(float theX, float theY)
		{
			this.x = theX;
			this.y = theY;
			this.z = 0f;
			this.u = 0f;
			this.v = 0f;
			this.rhw = 1f;
			this.color = Color.Zero;
			this.specular = 0U;
			this.u2 = (this.v2 = 0f);
		}

		public SexyVertex2D(float theX, float theY, float theU, float theV)
		{
			this.x = theX;
			this.y = theY;
			this.u = theU;
			this.v = theV;
			this.z = 0f;
			this.rhw = 1f;
			this.color = Color.Zero;
			this.specular = 0U;
			this.u2 = (this.v2 = 0f);
		}

		public SexyVertex2D(float theX, float theY, float theU, float theV, uint theColor)
		{
			this.x = theX;
			this.y = theY;
			this.u = theU;
			this.v = theV;
			this.color = new Color((int)theColor);
			this.z = 0f;
			this.rhw = 1f;
			this.specular = 0U;
			this.u2 = (this.v2 = 0f);
		}

		public SexyVertex2D(float theX, float theY, float theZ, float theU, float theV, uint theColor)
		{
			this.x = theX;
			this.y = theY;
			this.z = theZ;
			this.u = theU;
			this.v = theV;
			this.color = new Color((int)theColor);
			this.z = 0f;
			this.rhw = 1f;
			this.specular = 0U;
			this.u2 = (this.v2 = 0f);
		}

		public SexyVertex2D(float px, float py, float pu, float pv, float pu2, float pv2)
		{
			this = new SexyVertex2D(px, py, pu, pv);
			this.u2 = pu2;
			this.v2 = pv2;
		}

		public SexyVertex2D(float px, float py, uint theColor, float pu, float pv, float pu2, float pv2)
		{
			this = new SexyVertex2D(px, py, pu, pv, theColor);
			this.u2 = pu2;
			this.v2 = pv2;
		}

		public const uint FVF = 452U;

		public float x;

		public float y;

		public float z;

		public float rhw;

		public Color color;

		public uint specular;

		public float u;

		public float v;

		public float u2;

		public float v2;
	}
}
