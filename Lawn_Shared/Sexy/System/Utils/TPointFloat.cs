using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct TPointFloat
	{
		public float mX
		{
			get
			{
				return this.mPoint.X;
			}
			set
			{
				this.mPoint.X = value;
			}
		}

		public float x
		{
			get
			{
				return this.mPoint.X;
			}
			set
			{
				this.mPoint.X = value;
			}
		}

		public float mY
		{
			get
			{
				return this.mPoint.Y;
			}
			set
			{
				this.mPoint.Y = value;
			}
		}

		public float y
		{
			get
			{
				return this.mPoint.Y;
			}
			set
			{
				this.mPoint.Y = value;
			}
		}

		public TPointFloat(float theX, float theY)
		{
			this.mPoint = new Vector2(theX, theY);
		}

		public TPointFloat(TPointFloat theTPoint)
		{
			this.mPoint = theTPoint.mPoint;
		}

		public static bool operator ==(TPointFloat a, TPointFloat b)
		{
			return a.mPoint.X == b.mPoint.X && a.mPoint.Y == b.mPoint.Y;
		}

		public static bool operator !=(TPointFloat a, TPointFloat b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			TPoint tpoint = (TPoint)obj;
			return this.mX == (float)tpoint.mX && this.mY == (float)tpoint.mY;
		}

		public override int GetHashCode()
		{
			return this.mPoint.GetHashCode();
		}

		public static TPointFloat operator +(TPointFloat a, TPointFloat b)
		{
			return new TPointFloat(a.mPoint.X + b.mPoint.X, a.mPoint.Y + b.mPoint.Y);
		}

		public static TPointFloat operator -(TPointFloat a, TPointFloat b)
		{
			return new TPointFloat(a.mPoint.X - b.mPoint.X, a.mPoint.Y - b.mPoint.Y);
		}

		public static TPointFloat operator *(TPointFloat a, TPointFloat b)
		{
			return new TPointFloat(a.mPoint.X * b.mPoint.X, a.mPoint.Y * b.mPoint.Y);
		}

		public static TPointFloat operator /(TPointFloat a, TPointFloat b)
		{
			return new TPointFloat(a.mPoint.X / b.mPoint.X, a.mPoint.Y / b.mPoint.Y);
		}

		public static TPointFloat operator *(TPointFloat a, int s)
		{
			return new TPointFloat(a.mPoint.X * (float)s, a.mPoint.Y * (float)s);
		}

		public static TPointFloat operator /(TPointFloat a, int s)
		{
			return new TPointFloat(a.mPoint.X / (float)s, a.mPoint.Y / (float)s);
		}

		public override string ToString()
		{
			return this.mPoint.ToString();
		}

		private Vector2 mPoint;
	}
}
