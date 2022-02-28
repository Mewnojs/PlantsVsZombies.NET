using System;
using Sexy.Misc;

namespace Sexy.WidgetsLib
{
	public class PATransform
	{
		public PATransform Clone()
		{
			PATransform patransform = new PATransform();
			this.mMatrix.CopyTo(patransform.mMatrix);
			return patransform;
		}

		public PATransform()
		{
			this.mMatrix.LoadIdentity();
		}

		public void CopyFrom(PATransform rhs)
		{
			this.mMatrix.CopyFrom(rhs.mMatrix);
		}

		public void TransformSrc(PATransform theSrcTransform, ref PATransform outTran)
		{
			outTran.mMatrix.CopyFrom(this.mMatrix * theSrcTransform.mMatrix);
		}

		public void InterpolateTo(PATransform theNextTransform, float thePct, ref PATransform outTran)
		{
			outTran.mMatrix.mMatrix = this.mMatrix.mMatrix * (1f - thePct) + theNextTransform.mMatrix.mMatrix * thePct;
		}

		public SexyTransform2D mMatrix = new SexyTransform2D(false);
	}
}
