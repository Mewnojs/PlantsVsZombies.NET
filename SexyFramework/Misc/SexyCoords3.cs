using System;

namespace Sexy.Misc
{
	public class SexyCoords3
	{
		public SexyCoords3()
		{
			this.t = new SexyVector3(0f, 0f, 0f);
			this.r = new SexyAxes3();
			this.s = new SexyVector3(1f, 1f, 1f);
		}

		public SexyCoords3(SexyCoords3 inC)
		{
			this.t = inC.t;
			this.r = new SexyAxes3(inC.r);
			this.s = inC.s;
		}

		public SexyCoords3(SexyAxes3 inR)
		{
			this.t = new SexyVector3(0f, 0f, 0f);
			this.r = new SexyAxes3(inR);
			this.s = new SexyVector3(1f, 1f, 1f);
		}

		public SexyCoords3(SexyVector3 inT, SexyAxes3 inR, SexyVector3 inS)
		{
			this.t = inT;
			this.r = new SexyAxes3(inR);
			this.s = inS;
		}

		public SexyCoords3 CopyFrom(SexyCoords3 inC)
		{
			this.t = inC.t;
			this.r = inC.r;
			this.s = inC.s;
			return this;
		}

		public SexyCoords3 Enter(SexyCoords3 inCoords)
		{
			return new SexyCoords3(this.t.Enter(inCoords), this.r.Enter(inCoords.r), this.s / inCoords.s);
		}

		public SexyCoords3 Leave(SexyCoords3 inCoords)
		{
			return new SexyCoords3(this.t.Leave(inCoords), this.r.Leave(inCoords.r), this.s * inCoords.s);
		}

		public SexyCoords3 Inverse()
		{
			return new SexyCoords3().Enter(this);
		}

		public SexyCoords3 DeltaTo(SexyCoords3 inCoords)
		{
			return inCoords.Inverse().Leave(this);
		}

		public void Translate(float inX, float inY, float inZ)
		{
			this.t += new SexyVector3(inX, inY, inZ);
		}

		public void RotateRadAxis(float inRot, SexyVector3 inNormalizedAxis)
		{
			this.r.RotateRadAxis(inRot, inNormalizedAxis);
		}

		public void RotateRadX(float inRot)
		{
			this.r.RotateRadX(inRot);
		}

		public void RotateRadY(float inRot)
		{
			this.r.RotateRadY(inRot);
		}

		public void RotateRadZ(float inRot)
		{
			this.r.RotateRadZ(inRot);
		}

		public void Scale(float inX, float inY, float inZ)
		{
			this.s *= new SexyVector3(inX, inY, inZ);
		}

		public bool LookAt(SexyVector3 inTargetPos, SexyVector3 inUpVector)
		{
			SexyVector3 sexyVector = this.t - inTargetPos;
			if (sexyVector.ApproxZero())
			{
				return false;
			}
			sexyVector = sexyVector.Normalize();
			if (SexyMath.Fabs(inUpVector.Dot(sexyVector)) > 1f - GlobalMembers.SEXYMATH_EPSILON)
			{
				return false;
			}
			this.r.vZ = sexyVector;
			this.r.vX = inUpVector.Cross(this.r.vZ).Normalize();
			this.r.vY = this.r.vZ.Cross(this.r.vX).Normalize();
			return true;
		}

		public bool LookAt(SexyVector3 inViewPos, SexyVector3 inTargetPos, SexyVector3 inUpVector)
		{
			this.t = inViewPos;
			return this.LookAt(inTargetPos, inUpVector);
		}

		public void GetInboundMatrix(SexyMatrix4 outM)
		{
			if (outM == null)
			{
				return;
			}
			SexyVector3 sexyVector = new SexyVector3(-this.t);
			SexyVector3 v = new SexyVector3(this.r.vX / this.s.x);
			SexyVector3 v2 = new SexyVector3(this.r.vY / this.s.y);
			SexyVector3 v3 = new SexyVector3(this.r.vZ / this.s.z);
			outM.m[0, 0] = v.x;
			outM.m[0, 1] = v2.x;
			outM.m[0, 2] = v3.x;
			outM.m[0, 3] = 0f;
			outM.m[1, 0] = v.y;
			outM.m[1, 1] = v2.y;
			outM.m[1, 2] = v3.y;
			outM.m[1, 3] = 0f;
			outM.m[2, 0] = v.z;
			outM.m[2, 1] = v2.z;
			outM.m[2, 2] = v3.z;
			outM.m[2, 3] = 0f;
			outM.m[3, 0] = sexyVector.Dot(v);
			outM.m[3, 1] = sexyVector.Dot(v2);
			outM.m[3, 2] = sexyVector.Dot(v3);
			outM.m[3, 3] = 1f;
		}

		public void GetOutboundMatrix(SexyMatrix4 outM)
		{
			if (outM == null)
			{
				return;
			}
			outM.m[0, 0] = this.r.vX.x * this.s.x;
			outM.m[0, 1] = this.r.vX.y * this.s.x;
			outM.m[0, 2] = this.r.vX.z * this.s.x;
			outM.m[0, 3] = 0f;
			outM.m[1, 0] = this.r.vY.x * this.s.y;
			outM.m[1, 1] = this.r.vY.y * this.s.y;
			outM.m[1, 2] = this.r.vY.z * this.s.y;
			outM.m[1, 3] = 0f;
			outM.m[2, 0] = this.r.vZ.x * this.s.z;
			outM.m[2, 1] = this.r.vZ.y * this.s.z;
			outM.m[2, 2] = this.r.vZ.z * this.s.z;
			outM.m[2, 3] = 0f;
			outM.m[3, 0] = this.t.x;
			outM.m[3, 1] = this.t.y;
			outM.m[3, 2] = this.t.z;
			outM.m[3, 3] = 1f;
		}

		public SexyVector3 t = default(SexyVector3);

		public SexyAxes3 r = new SexyAxes3();

		public SexyVector3 s = default(SexyVector3);
	}
}
