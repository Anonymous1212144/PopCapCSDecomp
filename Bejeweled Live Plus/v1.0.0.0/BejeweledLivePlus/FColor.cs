using System;
using SexyFramework.Graphics;

namespace BejeweledLivePlus
{
	public class FColor
	{
		public Color GetColor()
		{
			return new Color((int)this.mRed, (int)this.mGreen, (int)this.mBlue);
		}

		public void Lerp(Color theColor)
		{
			this.mRed += ((float)theColor.mRed - this.mRed) / 20f;
			this.mGreen += ((float)theColor.mGreen - this.mGreen) / 20f;
			this.mBlue += ((float)theColor.mBlue - this.mBlue) / 20f;
		}

		public float mRed;

		public float mGreen;

		public float mBlue;
	}
}
