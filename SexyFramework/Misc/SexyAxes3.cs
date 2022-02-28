using System;

namespace Sexy.Misc
{
	public class SexyAxes3
	{
		public SexyAxes3()
		{
			this.vX = new SexyVector3(1f, 0f, 0f);
			this.vY = new SexyVector3(0f, 1f, 0f);
			this.vZ = new SexyVector3(0f, 0f, 1f);
		}

		public SexyAxes3(SexyAxes3 inA)
		{
			this.vX = inA.vX;
			this.vY = inA.vY;
			this.vZ = inA.vZ;
		}

		public SexyAxes3(SexyVector3 inX, SexyVector3 inY, SexyVector3 inZ)
		{
			this.vX = inX;
			this.vY = inY;
			this.vZ = inZ;
		}

		public void CopyFrom(SexyAxes3 inA)
		{
			this.vX = inA.vX;
			this.vY = inA.vY;
			this.vZ = inA.vZ;
		}

		public SexyAxes3 Enter(SexyAxes3 inAxes)
		{
			return new SexyAxes3(this.vX.Enter(inAxes), this.vY.Enter(inAxes), this.vZ.Enter(inAxes));
		}

		public SexyAxes3 Leave(SexyAxes3 inAxes)
		{
			return new SexyAxes3(this.vX.Leave(inAxes), this.vY.Leave(inAxes), this.vZ.Leave(inAxes));
		}

		private void EndterSelf(SexyAxes3 inAxes)
		{
			this.vX = this.vX.Enter(inAxes);
			this.vY = this.vX.Enter(inAxes);
			this.vZ = this.vZ.Enter(inAxes);
		}

		private void LeaveSelf(SexyAxes3 inAxes)
		{
			this.vX = this.vX.Leave(inAxes);
			this.vY = this.vX.Leave(inAxes);
			this.vZ = this.vZ.Leave(inAxes);
		}

		public SexyAxes3 Inverse()
		{
			return new SexyAxes3().Enter(this);
		}

		public SexyAxes3 OrthoNormalize()
		{
			SexyAxes3 sexyAxes = new SexyAxes3(this);
			sexyAxes.vX = sexyAxes.vY.Cross(sexyAxes.vZ).Normalize();
			sexyAxes.vY = sexyAxes.vZ.Cross(sexyAxes.vX).Normalize();
			sexyAxes.vZ = sexyAxes.vX.Cross(sexyAxes.vY).Normalize();
			return sexyAxes;
		}

		public SexyAxes3 DeltaTo(SexyAxes3 inAxes)
		{
			return inAxes.Inverse().Leave(this);
		}

		public SexyAxes3 SlerpTo(SexyAxes3 inAxes, float inAlpha)
		{
			return this.SlerpTo(inAxes, inAlpha, false);
		}

		public SexyAxes3 SlerpTo(SexyAxes3 inAxes, float inAlpha, bool inFastButLessAccurate)
		{
			return SexyQuat3.Slerp(new SexyQuat3(this), new SexyQuat3(inAxes), inAlpha, inFastButLessAccurate);
		}

		public void RotateRadAxis(float inRot, SexyVector3 inNormalizedAxis)
		{
			SexyAxes3 inAxes = SexyQuat3.AxisAngle(inNormalizedAxis, inRot);
			this.LeaveSelf(inAxes);
		}

		public void RotateRadX(float inRot)
		{
			double num = Math.Sin((double)inRot);
			double num2 = Math.Cos((double)inRot);
			SexyAxes3 sexyAxes = new SexyAxes3();
			sexyAxes.vY.y = (float)num2;
			sexyAxes.vZ.y = (float)(-(float)num);
			sexyAxes.vY.z = (float)num;
			sexyAxes.vZ.z = (float)num2;
			this.LeaveSelf(sexyAxes);
		}

		public void RotateRadY(float inRot)
		{
			double num = Math.Sin((double)inRot);
			double num2 = Math.Cos((double)inRot);
			SexyAxes3 sexyAxes = new SexyAxes3();
			sexyAxes.vX.x = (float)num2;
			sexyAxes.vX.z = (float)(-(float)num);
			sexyAxes.vZ.x = (float)num;
			sexyAxes.vZ.z = (float)num2;
			this.LeaveSelf(sexyAxes);
		}

		public void RotateRadZ(float inRot)
		{
			double num = Math.Sin((double)inRot);
			double num2 = Math.Cos((double)inRot);
			SexyAxes3 sexyAxes = new SexyAxes3();
			sexyAxes.vX.x = (float)num2;
			sexyAxes.vX.y = (float)num;
			sexyAxes.vY.x = (float)(-(float)num);
			sexyAxes.vY.y = (float)num2;
			this.LeaveSelf(sexyAxes);
		}

		public void LookAt(SexyVector3 inTargetDir, SexyVector3 inUpVector)
		{
			SexyVector3 v = inTargetDir.Normalize();
			if (SexyMath.Fabs(inUpVector.Dot(v)) > 1f - GlobalMembers.SEXYMATH_EPSILON)
			{
				return;
			}
			SexyAxes3 sexyAxes = new SexyAxes3();
			sexyAxes.vZ = v;
			sexyAxes.vX = inUpVector.Cross(sexyAxes.vZ).Normalize();
			sexyAxes.vY = sexyAxes.vZ.Cross(sexyAxes.vX).Normalize();
			this.LeaveSelf(sexyAxes);
		}

		public SexyVector3 vX = default(SexyVector3);

		public SexyVector3 vY = default(SexyVector3);

		public SexyVector3 vZ = default(SexyVector3);
	}
}
