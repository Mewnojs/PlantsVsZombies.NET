using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	internal struct SexyVector3
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

		public float z
		{
			get
			{
				return this.mVector.Z;
			}
			set
			{
				this.mVector.Z = value;
			}
		}

		public SexyVector3(float theX, float theY, float theZ)
		{
			this.mVector = new Vector3(theX, theY, theZ);
		}

		public SexyVector3(Vector3 theVector)
		{
			this.mVector = theVector;
		}

		public float Dot(SexyVector3 rhs)
		{
			return Vector3.Dot(this.mVector, rhs.mVector);
		}

		public SexyVector3 Cross(SexyVector3 v)
		{
			Vector3 theVector = default(Vector3);
			Vector3.Cross(ref this.mVector, ref v.mVector, out theVector);
			return new SexyVector3(theVector);
		}

		public static SexyVector3 operator +(SexyVector3 lhs, SexyVector3 rhs)
		{
			return new SexyVector3(lhs.mVector + rhs.mVector);
		}

		public static SexyVector3 operator -(SexyVector3 lhs, SexyVector3 rhs)
		{
			return new SexyVector3(lhs.mVector - rhs.mVector);
		}

		public static SexyVector3 operator *(float t, SexyVector3 rhs)
		{
			return new SexyVector3(t * rhs.x, t * rhs.y, t * rhs.z);
		}

		public static SexyVector3 operator /(float t, SexyVector3 rhs)
		{
			return new SexyVector3(rhs.x / t, rhs.y / t, rhs.z / t);
		}

		public float Norm()
		{
			return this.x * this.x + this.y * this.y + this.z * this.z;
		}

		public float Magnitude()
		{
			return (float)Math.Sqrt((double)this.Norm());
		}

		public SexyVector3 Normalize()
		{
			this.mVector.Normalize();
			return new SexyVector3(this.mVector);
		}

		public override string ToString()
		{
			return this.mVector.ToString();
		}

		public Vector3 mVector;
	}
}
