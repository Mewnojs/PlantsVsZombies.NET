using System;

namespace Sexy.Misc
{
	public class FPoint
	{
		public FPoint(float theX, float theY)
		{
			this.mX = theX;
			this.mY = theY;
		}

		public FPoint(FPoint theTPoint)
		{
			this.mX = theTPoint.mX;
			this.mY = theTPoint.mY;
		}

		public FPoint()
		{
			this.mX = 0f;
			this.mY = 0f;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is FPoint)
			{
				SexyPoint point = (SexyPoint)obj;
				return (float)point.mX == this.mX && (float)point.mY == this.mY;
			}
			return false;
		}

		public static bool operator ==(FPoint ImpliedObject, FPoint p)
		{
			if (ImpliedObject == null)
			{
				return p == null;
			}
			return ImpliedObject.Equals(p);
		}

		public static bool operator !=(FPoint ImpliedObject, FPoint p)
		{
			return !(ImpliedObject == p);
		}

		public float Magnitude()
		{
			return (float)Math.Sqrt((double)(this.mX * this.mX + this.mY * this.mY));
		}

		public static FPoint operator +(FPoint ImpliedObject, FPoint p)
		{
			return new FPoint(ImpliedObject.mX + p.mX, ImpliedObject.mY + p.mY);
		}

		public static FPoint operator -(FPoint ImpliedObject, FPoint p)
		{
			return new FPoint(ImpliedObject.mX - p.mX, ImpliedObject.mY - p.mY);
		}

		public static FPoint operator *(FPoint ImpliedObject, FPoint p)
		{
			return new FPoint(ImpliedObject.mX * p.mX, ImpliedObject.mY * p.mY);
		}

		public static FPoint operator /(FPoint ImpliedObject, FPoint p)
		{
			return new FPoint(ImpliedObject.mX / p.mX, ImpliedObject.mY / p.mY);
		}

		public static FPoint operator *(FPoint ImpliedObject, int s)
		{
			return new FPoint(ImpliedObject.mX * (float)s, ImpliedObject.mY * (float)s);
		}

		public static FPoint operator /(FPoint ImpliedObject, float s)
		{
			return new FPoint(ImpliedObject.mX / s, ImpliedObject.mY / s);
		}

		public static FPoint operator *(FPoint ImpliedObject, double s)
		{
			return new FPoint((float)((double)ImpliedObject.mX * s), (float)((double)ImpliedObject.mY * s));
		}

		public static FPoint operator /(FPoint ImpliedObject, double s)
		{
			return new FPoint((float)((double)ImpliedObject.mX / s), (float)((double)ImpliedObject.mY / s));
		}

		public static FPoint operator *(FPoint ImpliedObject, float s)
		{
			return new FPoint(ImpliedObject.mX * s, ImpliedObject.mY * s);
		}

		public static FPoint operator /(FPoint ImpliedObject, int s)
		{
			return new FPoint(ImpliedObject.mX / (float)s, ImpliedObject.mY / (float)s);
		}

		internal void SetValue(float p, float p_2)
		{
			this.mX = p;
			this.mY = p_2;
		}

		public float mX;

		public float mY;
	}
}
