using System;
using Microsoft.Xna.Framework;

namespace Sexy.Misc
{
	public class Transform
	{
		protected void MakeComplex()
		{
			if (!this.mComplex)
			{
				this.mComplex = true;
				this.CalcMatrix();
			}
		}

		protected void CalcMatrix()
		{
			if (this.mNeedCalcMatrix)
			{
				this.mNeedCalcMatrix = false;
				this.mTransForm.LoadIdentity();
				this.mTransForm.Translate(this.mTransX1, this.mTransX2);
				this.mTransForm.m02 = this.mTransX1;
				this.mTransForm.m12 = this.mTransY1;
				this.mTransForm.m22 = 1f;
				if (this.mHaveScale)
				{
					this.mTransForm.m00 = this.mScaleX;
					this.mTransForm.m11 = this.mScaleY;
				}
				else if (this.mHaveRot)
				{
					this.mTransForm.RotateRad(this.mRot);
				}
				if (this.mTransX2 != 0f || this.mTransY2 != 0f)
				{
					this.mTransForm.Translate(this.mTransX2, this.mTransY2);
				}
			}
		}

		public Transform()
		{
			this.Reset();
		}

		public void Reset()
		{
			this.mNeedCalcMatrix = true;
			this.mComplex = false;
			this.mTransX1 = (this.mTransY1 = 0f);
			this.mTransX2 = (this.mTransY2 = 0f);
			this.mScaleX = (this.mScaleY = 1f);
			this.mRot = 0f;
			this.mHaveRot = false;
			this.mHaveScale = false;
			this.mTransForm.LoadIdentity();
		}

		public void Translate(float tx, float ty)
		{
			if (this.mComplex)
			{
				this.mTransForm.Translate(tx, ty);
				return;
			}
			this.mNeedCalcMatrix = true;
			if (this.mHaveRot || this.mHaveScale)
			{
				this.mTransX2 += tx;
				this.mTransY2 += ty;
				return;
			}
			this.mTransX1 += tx;
			this.mTransY1 += ty;
		}

		public void RotateRad(float rot)
		{
			if (this.mComplex)
			{
				this.mTransForm.RotateRad(rot);
				return;
			}
			if (this.mHaveScale)
			{
				this.MakeComplex();
				this.mTransForm.RotateRad(rot);
				return;
			}
			this.mNeedCalcMatrix = true;
			this.mHaveRot = true;
			this.mRot += rot;
		}

		public void RotateDeg(float rot)
		{
			this.RotateRad(MathHelper.ToRadians(rot));
		}

		public void Scale(float sx, float sy)
		{
			if (this.mComplex)
			{
				this.mTransForm.Scale(sx, sy);
				return;
			}
			if (this.mHaveRot || this.mTransX1 != 0f || this.mTransY1 != 0f || (sx < 0f && this.mScaleX * sx != -1f) || sy < 0f || ((this.mTransX2 != 0f || this.mTransY2 != 0f) && sx != sy))
			{
				this.MakeComplex();
				this.mTransForm.Scale(sx, sy);
				return;
			}
			this.mNeedCalcMatrix = true;
			this.mHaveScale = true;
			this.mScaleX *= sx;
			this.mScaleY *= sy;
			this.mTransX2 *= sx;
			this.mTransY2 *= sy;
		}

		public SexyTransform2D GetMatrix()
		{
			this.CalcMatrix();
			return this.mTransForm;
		}

		public void SetMatrix(SexyTransform2D mat)
		{
			this.mTransForm.mMatrix = mat.mMatrix;
			this.mNeedCalcMatrix = false;
			this.mComplex = true;
		}

		protected SexyTransform2D mTransForm = new SexyTransform2D(false);

		protected bool mNeedCalcMatrix;

		public bool mComplex;

		public bool mHaveRot;

		public bool mHaveScale;

		public float mTransX1;

		public float mTransY1;

		public float mTransX2;

		public float mTransY2;

		public float mScaleX;

		public float mScaleY;

		public float mRot;
	}
}
