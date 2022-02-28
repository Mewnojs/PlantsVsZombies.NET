using System;
using Microsoft.Xna.Framework;

namespace Sexy.Misc
{
	public struct SexyTransform2D : SexyMatrix3
	{
		public float m00
		{
			get
			{
				return this.mMatrix.M11;
			}
			set
			{
				this.mMatrix.M11 = value;
			}
		}

		public float m01
		{
			get
			{
				return this.mMatrix.M21;
			}
			set
			{
				this.mMatrix.M21 = value;
			}
		}

		public float m02
		{
			get
			{
				return this.mMatrix.M41;
			}
			set
			{
				this.mMatrix.M41 = value;
			}
		}

		public float m10
		{
			get
			{
				return this.mMatrix.M12;
			}
			set
			{
				this.mMatrix.M12 = value;
			}
		}

		public float m11
		{
			get
			{
				return this.mMatrix.M22;
			}
			set
			{
				this.mMatrix.M22 = value;
			}
		}

		public float m12
		{
			get
			{
				return this.mMatrix.M42;
			}
			set
			{
				this.mMatrix.M42 = value;
			}
		}

		public float m20
		{
			get
			{
				return this.mMatrix.M13;
			}
			set
			{
				this.mMatrix.M13 = value;
			}
		}

		public float m21
		{
			get
			{
				return this.mMatrix.M23;
			}
			set
			{
				this.mMatrix.M23 = value;
			}
		}

		public float m22
		{
			get
			{
				return this.mMatrix.M33;
			}
			set
			{
				this.mMatrix.M33 = value;
			}
		}

		public SexyTransform2D(bool init)
		{
			this.mMatrix = Matrix.Identity;
		}

		public SexyTransform2D(Matrix mat)
		{
			this.mMatrix = mat;
		}

		public void Swap(SexyTransform2D lhs)
		{
		}

		public void CopyTo(SexyTransform2D lhs)
		{
			lhs.mMatrix = this.mMatrix;
		}

		public SexyTransform2D(SexyMatrix3 rhs)
		{
			this.mMatrix = ((SexyTransform2D)rhs).mMatrix;
		}

		public SexyTransform2D(float in00, float in01, float in02, float in10, float in11, float in12, float in20, float in21, float in22)
		{
			this.mMatrix = new Matrix(in00, in10, in20, 0f, in01, in11, in21, 0f, 0f, 0f, in22, 0f, in02, in12, 0f, 1f);
		}

		public void ZeroMatrix()
		{
			this.m00 = (this.m01 = (this.m02 = (this.m10 = (this.m11 = (this.m12 = (this.m20 = (this.m21 = (this.m22 = 0f))))))));
		}

		public void LoadIdentity()
		{
			this.mMatrix = Matrix.Identity;
		}

		public void CopyFrom(SexyMatrix3 theMatrix)
		{
			this.mMatrix = ((SexyTransform2D)theMatrix).mMatrix;
		}

		public static SexyVector2 operator *(SexyTransform2D ImpliedObject, SexyVector2 theVec)
		{
			return new SexyVector2(false)
			{
				mVector = Vector2.Transform(theVec.mVector, ImpliedObject.mMatrix)
			};
		}

		public static Vector2 operator *(SexyTransform2D ImpliedObject, Vector2 theVec)
		{
			return Vector2.Transform(theVec, ImpliedObject.mMatrix);
		}

		public static SexyVector3 operator *(SexyTransform2D ImpliedObject, SexyVector3 theVec)
		{
			return new SexyVector3
			{
				mVector = Vector3.Transform(theVec.mVector, ImpliedObject.mMatrix)
			};
		}

		public void MulSelf(SexyMatrix3 theMat)
		{
			this.mMatrix *= ((SexyTransform2D)theMat).mMatrix;
		}

		public static SexyMatrix3 operator *(SexyTransform2D ImpliedObject, SexyMatrix3 theMat)
		{
			return new SexyTransform2D(false)
			{
				mMatrix = ((SexyTransform2D)theMat).mMatrix * ImpliedObject.mMatrix
			};
		}

		public static void Multiply(ref SexyTransform2D pOut, SexyMatrix3 pM1, SexyMatrix3 pM2)
		{
			pOut.mMatrix = ((SexyTransform2D)pM2).mMatrix * ((SexyTransform2D)pM1).mMatrix;
		}

		public void Translate(float tx, float ty)
		{
			this.mMatrix.M41 = this.mMatrix.M41 + tx;
			this.mMatrix.M42 = this.mMatrix.M42 + ty;
		}

		public void RotateRad(float rot)
		{
			this.mMatrix = Matrix.Multiply(this.mMatrix, Matrix.CreateRotationZ(-rot));
		}

		public void RotateDeg(float rot)
		{
			this.RotateRad(MathHelper.ToRadians(rot));
		}

		public void Scale(float sx, float sy)
		{
			this.mMatrix.M11 = this.mMatrix.M11 * sx;
			this.mMatrix.M21 = this.mMatrix.M21 * sx;
			this.mMatrix.M41 = this.mMatrix.M41 * sx;
			this.mMatrix.M22 = this.mMatrix.M22 * sy;
			this.mMatrix.M12 = this.mMatrix.M12 * sy;
			this.mMatrix.M42 = this.mMatrix.M42 * sy;
		}

		public void SkewRad(float sx, float sy)
		{
			SexyTransform2D impliedObject = new SexyTransform2D(false);
			impliedObject.LoadIdentity();
			impliedObject.m01 = (float)Math.Tan((double)sx);
			impliedObject.m02 = (float)Math.Tan((double)sy);
			(impliedObject * this).Swap(this);
		}

		public static SexyTransform2D operator *(SexyTransform2D ImpliedObject, SexyTransform2D theMat)
		{
			SexyTransform2D result = new SexyTransform2D(theMat.mMatrix * ImpliedObject.mMatrix);
			return result;
		}

		public Matrix mMatrix;

		public static SexyTransform2D DefaultMatrix = new SexyTransform2D(false);
	}
}
