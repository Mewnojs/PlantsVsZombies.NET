using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct SexyTransform2D
	{
		public SexyTransform2D(bool loadIdentity)
		{
			if (loadIdentity)
			{
				mMatrix = Matrix.Identity;
			}
			else
			{
				mMatrix = default(Matrix);
			}
			isInitialised = true;
		}

		public void LoadIdentity()
		{
			mMatrix = Matrix.Identity;
			isInitialised = true;
		}

		public SexyTransform2D(Matrix theMatrix)
		{
			mMatrix = theMatrix;
			isInitialised = true;
		}

		private void Initiliase()
		{
			mMatrix = Matrix.Identity;
			isInitialised = true;
		}

		public void Translate(float tx, float ty)
		{
			if (!isInitialised)
			{
				Initiliase();
			}
			Matrix matrix = Matrix.CreateTranslation(tx, ty, 0f);
			Matrix.Multiply(ref mMatrix, ref matrix, out mMatrix);
		}

		public void RotateRad(float rot)
		{
			if (!isInitialised)
			{
				Initiliase();
			}
			Matrix matrix = Matrix.CreateRotationZ(rot);
			Matrix.Multiply(ref mMatrix, ref matrix, out mMatrix);
		}

		public void RotateDeg(float rot)
		{
			if (!isInitialised)
			{
				Initiliase();
			}
			RotateRad(3.1415927f * rot / 180f);
		}

		public void Scale(float sx, float sy)
		{
			if (!isInitialised)
			{
				Initiliase();
			}
			Matrix matrix = Matrix.CreateScale(sx, sy, 1f);
			Matrix.Multiply(ref mMatrix, ref matrix, out mMatrix);
		}

		public static bool operator ==(SexyTransform2D a, SexyTransform2D b)
		{
			return a.mMatrix == b.mMatrix;
		}

		public static bool operator !=(SexyTransform2D a, SexyTransform2D b)
		{
			return !(a.mMatrix == b.mMatrix);
		}

		public override int GetHashCode()
		{
			return mMatrix.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is SexyTransform2D))
			{
				return false;
			}
			SexyTransform2D a = (SexyTransform2D)obj;
			return a == this;
		}

		public static Vector2 operator *(SexyTransform2D transform, Vector2 v)
		{
			if (!transform.isInitialised)
			{
				transform.Initiliase();
			}
			Vector2 result;
			Vector2.Transform(ref v, ref transform.mMatrix, out result);
			return result;
		}

		public static Vector2 operator *(Vector2 v, SexyTransform2D transform)
		{
			return transform * v;
		}

		public static SexyTransform2D operator *(SexyTransform2D a, SexyTransform2D b)
		{
			if (!a.isInitialised)
			{
				a.Initiliase();
			}
			if (!b.isInitialised)
			{
				b.Initiliase();
			}
			SexyTransform2D result = new SexyTransform2D(true);
			Matrix.Multiply(ref a.mMatrix, ref b.mMatrix, out result.mMatrix);
			return result;
		}

		private bool isInitialised;

		public Matrix mMatrix;
	}
}
