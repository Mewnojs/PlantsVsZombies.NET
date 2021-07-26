using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct SexyVector2
	{
		public float x
		{
			get
			{
				return this.mVector.X;
			}
			set
			{
				this.mVector.X = value;
			}
		}

		public float y
		{
			get
			{
				return this.mVector.Y;
			}
			set
			{
				this.mVector.Y = value;
			}
		}

		public SexyVector2(float theX, float theY)
		{
			this.mVector = new Vector2(theX, theY);
		}

		public SexyVector2(Vector2 theVector)
		{
			this.mVector = theVector;
		}

		public float Dot(SexyVector2 v)
		{
			return Vector2.Dot(this.mVector, v.mVector);
		}

		public static SexyVector2 operator +(SexyVector2 lhs, SexyVector2 rhs)
		{
			return new SexyVector2(lhs.mVector + rhs.mVector);
		}

		public static SexyVector2 operator -(SexyVector2 lhs, SexyVector2 rhs)
		{
			return new SexyVector2(lhs.mVector - rhs.mVector);
		}

		public static SexyVector2 operator -(SexyVector2 rhs)
		{
			return new SexyVector2(-rhs.x, -rhs.y);
		}

		public static SexyVector2 operator *(float t, SexyVector2 rhs)
		{
			return new SexyVector2(t * rhs.x, t * rhs.y);
		}

		public static SexyVector2 operator /(float t, SexyVector2 rhs)
		{
			return new SexyVector2(rhs.x / t, rhs.y / t);
		}

		public static bool operator ==(SexyVector2 lhs, SexyVector2 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}

		public static bool operator !=(SexyVector2 lhs, SexyVector2 rhs)
		{
			return !(lhs == rhs);
		}

		public float Magnitude()
		{
			return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y));
		}

		public float MagnitudeSquared()
		{
			return this.x * this.x + this.y * this.y;
		}

		public SexyVector2 Normalize()
		{
			this.mVector.Normalize();
			return this;
		}

		public override string ToString()
		{
			return this.mVector.ToString();
		}

		public SexyVector2 Perp()
		{
			return new SexyVector2(-this.y, this.x);
		}

		public override int GetHashCode()
		{
			return this.mVector.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return this.mVector.Equals(obj);
		}

		public Vector2 mVector;
	}
}
