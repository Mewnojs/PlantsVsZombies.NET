using System;

namespace Sexy.Misc
{
	public class SexyTransform3D : SexyMatrix4
	{
		public SexyTransform3D()
		{
			base.LoadIdentity();
		}

		private SexyTransform3D(bool loadIdentity)
		{
			if (loadIdentity)
			{
				base.LoadIdentity();
			}
		}

		private SexyTransform3D(SexyMatrix4 theMatrix)
			: base(theMatrix)
		{
		}

		private void Translate(float tx, float ty, float tz)
		{
			base.m30 += tx;
			base.m31 += ty;
			base.m32 += tz;
		}

		private void RotateRadX(float rot)
		{
			float num = (float)Math.Sin((double)rot);
			float num2 = (float)Math.Cos((double)rot);
			SexyMatrix4 sexyMatrix = new SexyMatrix4();
			sexyMatrix.LoadIdentity();
			sexyMatrix.m11 = num2;
			sexyMatrix.m21 = -num;
			sexyMatrix.m12 = num;
			sexyMatrix.m22 = num2;
			(this * sexyMatrix).Swap(this);
		}

		private void RotateRadY(float rot)
		{
			float num = (float)Math.Sin((double)rot);
			float num2 = (float)Math.Cos((double)rot);
			SexyMatrix4 sexyMatrix = new SexyMatrix4();
			sexyMatrix.LoadIdentity();
			sexyMatrix.m00 = num2;
			sexyMatrix.m02 = -num;
			sexyMatrix.m20 = num;
			sexyMatrix.m22 = num2;
			(this * sexyMatrix).Swap(this);
		}

		private void RotateRadZ(float rot)
		{
			float num = (float)Math.Sin((double)rot);
			float num2 = (float)Math.Cos((double)rot);
			SexyMatrix4 sexyMatrix = new SexyMatrix4();
			sexyMatrix.LoadIdentity();
			sexyMatrix.m00 = num2;
			sexyMatrix.m01 = num;
			sexyMatrix.m10 = -num;
			sexyMatrix.m11 = num2;
			(this * sexyMatrix).Swap(this);
		}

		private void Scale(float sx, float sy, float sz)
		{
			base.m00 *= sx;
			base.m11 *= sy;
			base.m22 *= sz;
		}
	}
}
