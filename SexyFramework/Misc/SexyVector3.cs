using System;
using Microsoft.Xna.Framework;

namespace Sexy.Misc
{
	public struct SexyVector3
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

		public SexyVector3(SexyVector3 rhs)
		{
			this.mVector = rhs.mVector;
		}

		public SexyVector3(Vector3 rhs)
		{
			this.mVector = rhs;
		}

		public float Dot(SexyVector3 v)
		{
			return Vector3.Dot(this.mVector, v.mVector);
		}

		public SexyVector3 Cross(SexyVector3 v)
		{
			return new SexyVector3(Vector3.Cross(this.mVector, v.mVector));
		}

		public SexyVector3 CopyFrom(SexyVector3 v)
		{
			this.mVector = v.mVector;
			return this;
		}

		public static SexyVector3 operator -(SexyVector3 ImpliedObject)
		{
			return new SexyVector3(-ImpliedObject.x, -ImpliedObject.y, -ImpliedObject.z);
		}

		public static SexyVector3 operator +(SexyVector3 ImpliedObject, SexyVector3 v)
		{
			return new SexyVector3(ImpliedObject.x + v.x, ImpliedObject.y + v.y, ImpliedObject.z + v.z);
		}

		public static SexyVector3 operator -(SexyVector3 ImpliedObject, SexyVector3 v)
		{
			return new SexyVector3(ImpliedObject.x - v.x, ImpliedObject.y - v.y, ImpliedObject.z - v.z);
		}

		public static SexyVector3 operator *(SexyVector3 ImpliedObject, float t)
		{
			return new SexyVector3(t * ImpliedObject.x, t * ImpliedObject.y, t * ImpliedObject.z);
		}

		public static SexyVector3 operator *(SexyVector3 ImpliedObject, SexyVector3 v)
		{
			return new SexyVector3(Vector3.Multiply(ImpliedObject.mVector, v.mVector));
		}

		public static SexyVector3 operator /(SexyVector3 ImpliedObject, float t)
		{
			return new SexyVector3(Vector3.Divide(ImpliedObject.mVector, t));
		}

		public static SexyVector3 operator /(SexyVector3 ImpliedObject, SexyVector3 v)
		{
			return new SexyVector3(Vector3.Divide(ImpliedObject.mVector, v.mVector));
		}

		public float Magnitude()
		{
			return this.mVector.Length();
		}

		public SexyVector3 Normalize()
		{
			this.mVector.Normalize();
			return this;
		}

		public bool ApproxEquals(SexyVector3 inV)
		{
			return this.ApproxEquals(inV, 0.001f);
		}

		public bool ApproxEquals(SexyVector3 inV, float inTol)
		{
			return SexyMath.ApproxEquals(this.x, inV.x, inTol) && SexyMath.ApproxEquals(this.y, inV.y, inTol) && SexyMath.ApproxEquals(this.z, inV.z, inTol);
		}

		public bool ApproxZero()
		{
			return this.ApproxZero(0.001f);
		}

		public bool ApproxZero(float inTol)
		{
			return this.ApproxEquals(new SexyVector3(0f, 0f, 0f), inTol);
		}

		public SexyVector3 Enter(SexyAxes3 inAxes)
		{
			return new SexyVector3(this.Dot(inAxes.vX), this.Dot(inAxes.vY), this.Dot(inAxes.vZ));
		}

		public SexyVector3 Enter(SexyCoords3 inCoords)
		{
			return (this - inCoords.t.Enter(inCoords.r)) / inCoords.s;
		}

		public SexyVector3 Leave(SexyAxes3 inAxes)
		{
			return new SexyVector3(this.x * inAxes.vX.x + this.y * inAxes.vY.x + this.z * inAxes.vZ.x, this.x * inAxes.vX.y + this.y * inAxes.vY.y + this.z * inAxes.vZ.y, this.x * inAxes.vX.z + this.y * inAxes.vY.z + this.z * inAxes.vZ.z);
		}

		public SexyVector3 Leave(SexyCoords3 inCoords)
		{
			return this * inCoords.s.Leave(inCoords.r) + inCoords.t;
		}

		public Vector3 mVector;
	}
}
