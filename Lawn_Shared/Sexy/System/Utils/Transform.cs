using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct Transform
	{
		private void MakeComplex()
		{
			if (!this.mComplex)
			{
				this.mComplex = true;
				this.CalcMatrix();
			}
		}

		private void CalcMatrix()
		{
			if (!this.isInitialised)
			{
				this.Reset();
			}
			if (this.mNeedCalcMatrix)
			{
				this.mNeedCalcMatrix = false;
				this.mMatrix = new SexyTransform2D(Matrix.Identity);
				this.mMatrix.mMatrix.M13 = this.mTransX1;
				this.mMatrix.mMatrix.M23 = this.mTransY1;
				this.mMatrix.mMatrix.M33 = 1f;
				if (this.mHaveScale)
				{
					this.mMatrix.mMatrix.M11 = this.mScaleX;
					this.mMatrix.mMatrix.M22 = this.mScaleY;
				}
				else if (this.mHaveRot)
				{
					this.mMatrix.RotateRad(this.mRot);
				}
				if (this.mTransX2 != 0f || this.mTransY2 != 0f)
				{
					this.mMatrix.Translate(this.mTransX2, this.mTransY2);
				}
			}
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
			this.isInitialised = true;
		}

		public void Translate(float tx, float ty)
		{
			if (!this.isInitialised)
			{
				this.Reset();
			}
			if (this.mComplex)
			{
				this.mMatrix.Translate(tx, ty);
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
			if (!this.isInitialised)
			{
				this.Reset();
			}
			if (this.mComplex)
			{
				this.mMatrix.RotateRad(rot);
				return;
			}
			if (this.mHaveScale)
			{
				this.MakeComplex();
				this.mMatrix.RotateRad(rot);
				return;
			}
			this.mNeedCalcMatrix = true;
			this.mHaveRot = true;
			this.mRot += rot;
		}

		public void RotateDeg(float rot)
		{
			if (!this.isInitialised)
			{
				this.Reset();
			}
			this.RotateRad(3.1415927f * rot / 180f);
		}

		public void Scale(float sx, float sy)
		{
			if (!this.isInitialised)
			{
				this.Reset();
			}
			if (this.mComplex)
			{
				this.mMatrix.Scale(sx, sy);
				return;
			}
			if (this.mHaveRot || this.mTransX1 != 0f || this.mTransY1 != 0f || (sx < 0f && this.mScaleX * sx != -1f) || sy < 0f)
			{
				this.MakeComplex();
				this.mMatrix.Scale(sx, sy);
				return;
			}
			this.mNeedCalcMatrix = true;
			this.mHaveScale = true;
			this.mScaleX *= sx;
			this.mScaleY *= sy;
		}

		public SexyTransform2D GetMatrix()
		{
			if (!this.isInitialised)
			{
				this.Reset();
			}
			this.CalcMatrix();
			return this.mMatrix;
		}

		private bool isInitialised;

		public static Transform Identity = new Transform
		{
			mMatrix = new SexyTransform2D(Matrix.Identity)
		};

		private SexyTransform2D mMatrix;

		private bool mNeedCalcMatrix;

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
