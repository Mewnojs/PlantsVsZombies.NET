using System;

namespace Sexy.Misc
{
	public class SexyPoint
	{
		public SexyPoint(int theX, int theY)
		{
			this.mX = theX;
			this.mY = theY;
		}

		public SexyPoint(SexyPoint theTPoint)
		{
			this.mX = theTPoint.mX;
			this.mY = theTPoint.mY;
		}

		public SexyPoint()
		{
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SexyPoint point;
			return obj != null && (point = obj as SexyPoint) != null && point.mX == this.mX && point.mY == this.mY;
		}

		public static bool operator ==(SexyPoint ImpliedObject, SexyPoint p)
		{
			if (ImpliedObject as Object == null)
			{
				return p as Object == null;
			}
			return ImpliedObject.Equals(p);
		}

		public static bool operator !=(SexyPoint ImpliedObject, SexyPoint p)
		{
			return !(ImpliedObject == p);
		}

		public int Magnitude()
		{
			return (int)Math.Sqrt((double)(this.mX * this.mX + this.mY * this.mY));
		}

		public static SexyPoint operator +(SexyPoint ImpliedObject, SexyPoint p)
		{
			ImpliedObject.mX += p.mX;
			ImpliedObject.mY += p.mY;
			return ImpliedObject;
		}

		public static SexyPoint operator -(SexyPoint ImpliedObject, SexyPoint p)
		{
			return new SexyPoint(ImpliedObject.mX - p.mX, ImpliedObject.mY - p.mY);
		}

		public static SexyPoint operator *(SexyPoint ImpliedObject, SexyPoint p)
		{
			return new SexyPoint(ImpliedObject.mX * p.mX, ImpliedObject.mY * p.mY);
		}

		public static SexyPoint operator /(SexyPoint ImpliedObject, SexyPoint p)
		{
			return new SexyPoint(ImpliedObject.mX / p.mX, ImpliedObject.mY / p.mY);
		}

		public static SexyPoint operator *(SexyPoint ImpliedObject, int s)
		{
			return new SexyPoint(ImpliedObject.mX * s, ImpliedObject.mY * s);
		}

		public static SexyPoint operator *(SexyPoint ImpliedObject, double s)
		{
			return new SexyPoint((int)((double)ImpliedObject.mX * s), (int)((double)ImpliedObject.mY * s));
		}

		public static SexyPoint operator *(SexyPoint ImpliedObject, float s)
		{
			return new SexyPoint((int)((float)ImpliedObject.mX * s), (int)((float)ImpliedObject.mY * s));
		}

		public static SexyPoint operator /(SexyPoint ImpliedObject, float s)
		{
			return new SexyPoint((int)((float)ImpliedObject.mX / s), (int)((float)ImpliedObject.mY / s));
		}

		public static SexyPoint operator /(SexyPoint ImpliedObject, double s)
		{
			return new SexyPoint((int)((double)ImpliedObject.mX / s), (int)((double)ImpliedObject.mY / s));
		}

		public static SexyPoint operator /(SexyPoint ImpliedObject, int s)
		{
			return new SexyPoint(ImpliedObject.mX / s, ImpliedObject.mY / s);
		}

		public int mX;

		public int mY;
	}
}
