using System;
using Sexy;

namespace Bejeweled3
{
	public class FColor
	{
		public SexyColor GetColor()
		{
			return new SexyColor((int)this.mRed, (int)this.mGreen, (int)this.mBlue);
		}

		public void Lerp(SexyColor theColor)
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
