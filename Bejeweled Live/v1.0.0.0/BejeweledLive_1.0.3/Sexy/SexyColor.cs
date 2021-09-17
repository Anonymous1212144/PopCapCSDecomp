using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct SexyColor
	{
		public int mRed
		{
			get
			{
				return (int)this.mColor.R;
			}
			set
			{
				this.mColor.R = (byte)value;
			}
		}

		public int mGreen
		{
			get
			{
				return (int)this.mColor.G;
			}
			set
			{
				this.mColor.G = (byte)value;
			}
		}

		public int mBlue
		{
			get
			{
				return (int)this.mColor.B;
			}
			set
			{
				this.mColor.B = (byte)value;
			}
		}

		public int mAlpha
		{
			get
			{
				return (int)this.mColor.A;
			}
			set
			{
				float num = (float)value / 255f;
				this.mColor *= num;
			}
		}

		public static SexyColor Black
		{
			get
			{
				return new SexyColor(Color.Black);
			}
		}

		public static SexyColor White
		{
			get
			{
				return new SexyColor(Color.White);
			}
		}

		public Color Color
		{
			get
			{
				return this.mColor;
			}
			set
			{
				this.mColor = value;
			}
		}

		public SexyColor(int theRed, int theGreen, int theBlue)
		{
			this.mColor = new Color(theRed, theGreen, theBlue);
		}

		public SexyColor(int theRed, int theGreen, int theBlue, int theAlpha)
		{
			this.mColor = new Color(theRed, theGreen, theBlue, theAlpha);
		}

		public SexyColor(string theElements)
		{
			this.mColor = new Color((int)theElements.get_Chars(0), (int)theElements.get_Chars(1), (int)theElements.get_Chars(2), 255);
		}

		public SexyColor(Color theColor)
		{
			this.mColor = theColor;
		}

		public int this[int theIdx]
		{
			get
			{
				switch (theIdx)
				{
				case 0:
					return (int)this.mColor.R;
				case 1:
					return (int)this.mColor.G;
				case 2:
					return (int)this.mColor.B;
				case 3:
					return (int)this.mColor.A;
				default:
					return 0;
				}
			}
		}

		public static bool operator ==(SexyColor a, SexyColor b)
		{
			return object.ReferenceEquals(a, b) || (a != null && b != null && a.Equals(b));
		}

		public static bool operator !=(SexyColor a, SexyColor b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return obj is SexyColor && this.mColor == ((SexyColor)obj).mColor;
		}

		public override int GetHashCode()
		{
			return this.mColor.GetHashCode();
		}

		public override string ToString()
		{
			return this.mColor.ToString();
		}

		public static explicit operator SexyColor(Color color)
		{
			return new SexyColor
			{
				Color = color
			};
		}

		public static implicit operator Color(SexyColor aColor)
		{
			return aColor.Color;
		}

		public static SexyColor FromColor(Color c)
		{
			return new SexyColor(c);
		}

		internal void CopyFrom(Color c)
		{
			this.mColor = c;
		}

		private Color mColor;
	}
}
