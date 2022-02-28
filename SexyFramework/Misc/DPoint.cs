using System;

namespace Sexy.Misc
{
	public class DPoint
	{
		public DPoint(double theX, double theY)
		{
			this.mX = theX;
			this.mY = theY;
		}

		public DPoint(DPoint theTPoint)
		{
			this.mX = theTPoint.mX;
			this.mY = theTPoint.mY;
		}

		public DPoint()
		{
			this.mX = 0.0;
			this.mY = 0.0;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is DPoint)
			{
				DPoint dpoint = (DPoint)obj;
				return dpoint.mX == this.mX && dpoint.mY == this.mY;
			}
			return false;
		}

		public static bool operator ==(DPoint ImpliedObject, DPoint p)
		{
			if (ImpliedObject == null)
			{
				return p == null;
			}
			return ImpliedObject.Equals(p);
		}

		public static bool operator !=(DPoint ImpliedObject, DPoint p)
		{
			return !(ImpliedObject == p);
		}

		public double Magnitude()
		{
			return Math.Sqrt(this.mX * this.mX + this.mY * this.mY);
		}

		public static DPoint operator +(DPoint ImpliedObject, DPoint p)
		{
			return new DPoint(ImpliedObject.mX + p.mX, ImpliedObject.mY + p.mY);
		}

		public static DPoint operator -(DPoint ImpliedObject, DPoint p)
		{
			return new DPoint(ImpliedObject.mX - p.mX, ImpliedObject.mY - p.mY);
		}

		public static DPoint operator *(DPoint ImpliedObject, DPoint p)
		{
			return new DPoint(ImpliedObject.mX * p.mX, ImpliedObject.mY * p.mY);
		}

		public static DPoint operator /(DPoint ImpliedObject, DPoint p)
		{
			return new DPoint(ImpliedObject.mX / p.mX, ImpliedObject.mY / p.mY);
		}

		public static DPoint operator *(DPoint ImpliedObject, int s)
		{
			return new DPoint(ImpliedObject.mX * (double)s, ImpliedObject.mY * (double)s);
		}

		public static DPoint operator /(DPoint ImpliedObject, float s)
		{
			return new DPoint(ImpliedObject.mX / (double)s, ImpliedObject.mY / (double)s);
		}

		public static DPoint operator *(DPoint ImpliedObject, double s)
		{
			return new DPoint(ImpliedObject.mX * s, ImpliedObject.mY * s);
		}

		public static DPoint operator /(DPoint ImpliedObject, double s)
		{
			return new DPoint(ImpliedObject.mX / s, ImpliedObject.mY / s);
		}

		public static DPoint operator *(DPoint ImpliedObject, float s)
		{
			return new DPoint(ImpliedObject.mX * (double)s, ImpliedObject.mY * (double)s);
		}

		public static DPoint operator /(DPoint ImpliedObject, int s)
		{
			return new DPoint(ImpliedObject.mX / (double)s, ImpliedObject.mY / (double)s);
		}

		public double mX;

		public double mY;
	}
}
