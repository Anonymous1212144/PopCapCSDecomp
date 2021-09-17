using System;

namespace SexyFramework.Misc
{
	public class Point
	{
		public Point(int theX, int theY)
		{
			this.mX = theX;
			this.mY = theY;
		}

		public Point(Point theTPoint)
		{
			this.mX = theTPoint.mX;
			this.mY = theTPoint.mY;
		}

		public Point()
		{
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Point point;
			return obj != null && (point = obj as Point) != null && point.mX == this.mX && point.mY == this.mY;
		}

		public static bool operator ==(Point ImpliedObject, Point p)
		{
			if (ImpliedObject == null)
			{
				return p == null;
			}
			return ImpliedObject.Equals(p);
		}

		public static bool operator !=(Point ImpliedObject, Point p)
		{
			return !(ImpliedObject == p);
		}

		public int Magnitude()
		{
			return (int)Math.Sqrt((double)(this.mX * this.mX + this.mY * this.mY));
		}

		public static Point operator +(Point ImpliedObject, Point p)
		{
			ImpliedObject.mX += p.mX;
			ImpliedObject.mY += p.mY;
			return ImpliedObject;
		}

		public static Point operator -(Point ImpliedObject, Point p)
		{
			return new Point(ImpliedObject.mX - p.mX, ImpliedObject.mY - p.mY);
		}

		public static Point operator *(Point ImpliedObject, Point p)
		{
			return new Point(ImpliedObject.mX * p.mX, ImpliedObject.mY * p.mY);
		}

		public static Point operator /(Point ImpliedObject, Point p)
		{
			return new Point(ImpliedObject.mX / p.mX, ImpliedObject.mY / p.mY);
		}

		public static Point operator *(Point ImpliedObject, int s)
		{
			return new Point(ImpliedObject.mX * s, ImpliedObject.mY * s);
		}

		public static Point operator *(Point ImpliedObject, double s)
		{
			return new Point((int)((double)ImpliedObject.mX * s), (int)((double)ImpliedObject.mY * s));
		}

		public static Point operator *(Point ImpliedObject, float s)
		{
			return new Point((int)((float)ImpliedObject.mX * s), (int)((float)ImpliedObject.mY * s));
		}

		public static Point operator /(Point ImpliedObject, float s)
		{
			return new Point((int)((float)ImpliedObject.mX / s), (int)((float)ImpliedObject.mY / s));
		}

		public static Point operator /(Point ImpliedObject, double s)
		{
			return new Point((int)((double)ImpliedObject.mX / s), (int)((double)ImpliedObject.mY / s));
		}

		public static Point operator /(Point ImpliedObject, int s)
		{
			return new Point(ImpliedObject.mX / s, ImpliedObject.mY / s);
		}

		public int mX;

		public int mY;
	}
}
