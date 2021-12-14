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
				return mVector.X;
			}
			set
			{
				mVector.X = value;
			}
		}

		public float y
		{
			get
			{
				return mVector.Y;
			}
			set
			{
				mVector.Y = value;
			}
		}

		public float z
		{
			get
			{
				return mVector.Z;
			}
			set
			{
				mVector.Z = value;
			}
		}

		public SexyVector3(float theX, float theY, float theZ)
		{
			mVector = new Vector3(theX, theY, theZ);
		}

		public SexyVector3(Vector3 theVector)
		{
			mVector = theVector;
		}

		public float Dot(SexyVector3 rhs)
		{
			return Vector3.Dot(mVector, rhs.mVector);
		}

		public SexyVector3 Cross(SexyVector3 v)
		{
			Vector3 theVector = default(Vector3);
			Vector3.Cross(ref mVector, ref v.mVector, out theVector);
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
			return x * x + y * y + z * z;
		}

		public float Magnitude()
		{
			return (float)Math.Sqrt((double)Norm());
		}

		public SexyVector3 Normalize()
		{
			mVector.Normalize();
			return new SexyVector3(mVector);
		}

		public override string ToString()
		{
			return mVector.ToString();
		}

		public Vector3 mVector;
	}
}
