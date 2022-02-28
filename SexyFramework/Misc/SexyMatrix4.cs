using System;
using Microsoft.Xna.Framework;

namespace Sexy.Misc
{
	public class SexyMatrix4
	{
		public float m00
		{
			get
			{
				return this.m[0, 0];
			}
			set
			{
				this.m[0, 0] = value;
			}
		}

		public float m01
		{
			get
			{
				return this.m[0, 1];
			}
			set
			{
				this.m[0, 1] = value;
			}
		}

		public float m02
		{
			get
			{
				return this.m[0, 2];
			}
			set
			{
				this.m[0, 2] = value;
			}
		}

		public float m03
		{
			get
			{
				return this.m[0, 3];
			}
			set
			{
				this.m[0, 3] = value;
			}
		}

		public float m10
		{
			get
			{
				return this.m[1, 0];
			}
			set
			{
				this.m[1, 0] = value;
			}
		}

		public float m11
		{
			get
			{
				return this.m[1, 1];
			}
			set
			{
				this.m[1, 1] = value;
			}
		}

		public float m12
		{
			get
			{
				return this.m[1, 2];
			}
			set
			{
				this.m[1, 2] = value;
			}
		}

		public float m13
		{
			get
			{
				return this.m[1, 3];
			}
			set
			{
				this.m[1, 3] = value;
			}
		}

		public float m20
		{
			get
			{
				return this.m[2, 0];
			}
			set
			{
				this.m[2, 0] = value;
			}
		}

		public float m21
		{
			get
			{
				return this.m[2, 1];
			}
			set
			{
				this.m[2, 1] = value;
			}
		}

		public float m22
		{
			get
			{
				return this.m[2, 2];
			}
			set
			{
				this.m[2, 2] = value;
			}
		}

		public float m23
		{
			get
			{
				return this.m[2, 3];
			}
			set
			{
				this.m[2, 3] = value;
			}
		}

		public float m30
		{
			get
			{
				return this.m[3, 0];
			}
			set
			{
				this.m[3, 0] = value;
			}
		}

		public float m31
		{
			get
			{
				return this.m[3, 1];
			}
			set
			{
				this.m[3, 1] = value;
			}
		}

		public float m32
		{
			get
			{
				return this.m[3, 2];
			}
			set
			{
				this.m[3, 2] = value;
			}
		}

		public float m33
		{
			get
			{
				return this.m[3, 3];
			}
			set
			{
				this.m[3, 3] = value;
			}
		}

		public SexyMatrix4()
		{
		}

		public void Swap(SexyMatrix4 lhs)
		{
			float[,] array = lhs.m;
			lhs.m = this.m;
			this.m = array;
		}

		public void CopyTo(SexyMatrix4 lhs)
		{
			for (int i = 0; i < 4; i++)
			{
				lhs.m[i, 0] = this.m[i, 0];
				lhs.m[i, 1] = this.m[i, 1];
				lhs.m[i, 2] = this.m[i, 2];
				lhs.m[i, 3] = this.m[i, 3];
			}
		}

		public SexyMatrix4(SexyMatrix4 rhs)
		{
			rhs.CopyTo(this);
		}

		public SexyMatrix4(float in00, float in01, float in02, float in03, float in10, float in11, float in12, float in13, float in20, float in21, float in22, float in23, float in30, float in31, float in32, float in33)
		{
			this.m00 = in00;
			this.m01 = in00;
			this.m02 = in02;
			this.m03 = in03;
			this.m10 = in10;
			this.m11 = in11;
			this.m12 = in12;
			this.m13 = in13;
			this.m20 = in20;
			this.m21 = in21;
			this.m22 = in22;
			this.m23 = in23;
			this.m30 = in30;
			this.m31 = in31;
			this.m32 = in32;
			this.m32 = in33;
		}

		public void LoadIdentity()
		{
			this.m[0, 1] = (this.m[0, 2] = (this.m[0, 3] = (this.m[1, 0] = (this.m[1, 2] = (this.m[1, 3] = (this.m[2, 0] = (this.m[2, 1] = (this.m[2, 3] = (this.m[3, 0] = (this.m[3, 1] = (this.m[3, 2] = 0f)))))))))));
			this.m[0, 0] = (this.m[1, 1] = (this.m[2, 2] = (this.m[3, 3] = 1f)));
		}

		public static SexyVector3 operator *(SexyMatrix4 ImpliedObject, SexyVector2 theVec)
		{
			SexyVector3 result = default(SexyVector3);
			Vector3 vector = new Vector3(theVec.mVector, 0f);
			result.mVector = Vector3.Transform(vector, ImpliedObject.mMatrix);
			return result;
		}

		public static SexyVector3 operator *(SexyMatrix4 ImpliedObject, SexyVector3 theVec)
		{
			return new SexyVector3
			{
				mVector = Vector3.Transform(theVec.mVector, ImpliedObject.mMatrix)
			};
		}

		public static SexyMatrix4 operator *(SexyMatrix4 ImpliedObject, SexyMatrix4 theMat)
		{
			SexyMatrix4 sexyMatrix = new SexyMatrix4();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					float num = 0f;
					for (int k = 0; k < 4; k++)
					{
						num += ImpliedObject.m[i, k] * theMat.m[k, j];
					}
					sexyMatrix.m[i, j] = num;
				}
			}
			return sexyMatrix;
		}

		public Matrix mMatrix;

		public float[,] m = new float[4, 4];
	}
}
