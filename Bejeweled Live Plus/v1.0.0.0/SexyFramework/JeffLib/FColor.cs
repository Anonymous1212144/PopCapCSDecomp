using System;
using SexyFramework;
using SexyFramework.Graphics;

namespace JeffLib
{
	public class FColor
	{
		public FColor()
		{
			this.mRed = (this.mGreen = (this.mBlue = 0f));
			this.mAlpha = 255f;
		}

		public FColor(float r, float g, float b)
		{
			this.mRed = r;
			this.mGreen = g;
			this.mBlue = b;
			this.mAlpha = 255f;
		}

		public FColor(float r, float g, float b, float a)
		{
			this.mRed = r;
			this.mGreen = g;
			this.mBlue = b;
			this.mAlpha = a;
		}

		public Color ToColor()
		{
			return new Color((int)this.mRed, (int)this.mGreen, (int)this.mBlue, (int)this.mAlpha);
		}

		public FColor(Color rhs)
		{
			this.CopyFrom(rhs);
		}

		private void CopyFrom(Color rhs)
		{
			this.mRed = (float)rhs.mRed;
			this.mGreen = (float)rhs.mGreen;
			this.mBlue = (float)rhs.mBlue;
			this.mAlpha = (float)rhs.mAlpha;
		}

		public static implicit operator Color(FColor ImpliedObject)
		{
			return new Color((int)ImpliedObject.mRed, (int)ImpliedObject.mGreen, (int)ImpliedObject.mBlue, (int)ImpliedObject.mAlpha);
		}

		public static bool operator ==(FColor a, FColor b)
		{
			if (a == null)
			{
				return b == null;
			}
			return a.Equals(b);
		}

		public static bool operator !=(FColor a, FColor b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is FColor)
			{
				FColor fcolor = (FColor)obj;
				return Common._eq(this.mAlpha, fcolor.mAlpha) && Common._eq(this.mGreen, fcolor.mGreen) && Common._eq(this.mBlue, fcolor.mBlue) && Common._eq(this.mRed, fcolor.mRed);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public float mRed;

		public float mGreen;

		public float mBlue;

		public float mAlpha;
	}
}
