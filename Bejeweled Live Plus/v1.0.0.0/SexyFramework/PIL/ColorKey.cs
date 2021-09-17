using System;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace SexyFramework.PIL
{
	public class ColorKey
	{
		public ColorKey()
		{
		}

		public ColorKey(Color c)
		{
			this.mColor = c;
		}

		public virtual void Dispose()
		{
		}

		public Color GetInterpolatedColor(ColorKey next_color, float pct)
		{
			Color result = new Color(this.mColor);
			result.mRed += (int)((float)(next_color.mColor.mRed - this.mColor.mRed) * pct);
			result.mGreen += (int)((float)(next_color.mColor.mGreen - this.mColor.mGreen) * pct);
			result.mBlue += (int)((float)(next_color.mColor.mBlue - this.mColor.mBlue) * pct);
			result.mAlpha += (int)((float)(next_color.mColor.mAlpha - this.mColor.mAlpha) * pct);
			result.mRed = Math.Max(Math.Min(255, result.mRed), 0);
			result.mGreen = Math.Max(Math.Min(255, result.mGreen), 0);
			result.mBlue = Math.Max(Math.Min(255, result.mBlue), 0);
			result.mAlpha = Math.Max(Math.Min(255, result.mAlpha), 0);
			return result;
		}

		public Color GetColor()
		{
			return this.mColor;
		}

		public void SetColor(Color c)
		{
			this.mColor = c;
		}

		public void Serialize(Buffer b)
		{
			b.WriteLong((long)this.mColor.ToInt());
		}

		public void Deserialize(Buffer b)
		{
			this.mColor = new Color((int)b.ReadLong());
		}

		protected Color mColor;
	}
}
