using System;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class LifetimeSettings
	{
		public LifetimeSettings()
		{
		}

		public LifetimeSettings(LifetimeSettings rhs)
			: this()
		{
			if (rhs == null)
			{
				return;
			}
			this.mSizeXMult = rhs.mSizeXMult;
			this.mVelocityMult = rhs.mVelocityMult;
			this.mWeightMult = rhs.mWeightMult;
			this.mSpinMult = rhs.mSpinMult;
			this.mMotionRandMult = rhs.mMotionRandMult;
			this.mBounceMult = rhs.mBounceMult;
			this.mZoomMult = rhs.mZoomMult;
			this.mNumberMult = rhs.mNumberMult;
			this.mPct = rhs.mPct;
		}

		public void Reset()
		{
			this.mSizeXMult = 1f;
			this.mVelocityMult = 1f;
			this.mWeightMult = 1f;
			this.mSpinMult = 1f;
			this.mMotionRandMult = 1f;
			this.mBounceMult = 1f;
			this.mZoomMult = 1f;
			this.mNumberMult = 1f;
			this.mPct = 0f;
		}

		public void Serialize(SexyBuffer b)
		{
			b.WriteFloat(this.mSizeXMult);
			b.WriteFloat(this.mSizeYMult);
			b.WriteFloat(this.mVelocityMult);
			b.WriteFloat(this.mWeightMult);
			b.WriteFloat(this.mSpinMult);
			b.WriteFloat(this.mMotionRandMult);
			b.WriteFloat(this.mBounceMult);
			b.WriteFloat(this.mZoomMult);
			b.WriteFloat(this.mNumberMult);
			b.WriteFloat(this.mPct);
		}

		public void Deserialize(SexyBuffer b)
		{
			this.mSizeXMult = b.ReadFloat();
			this.mSizeYMult = b.ReadFloat();
			this.mVelocityMult = b.ReadFloat();
			this.mWeightMult = b.ReadFloat();
			this.mSpinMult = b.ReadFloat();
			this.mMotionRandMult = b.ReadFloat();
			this.mBounceMult = b.ReadFloat();
			this.mZoomMult = b.ReadFloat();
			this.mNumberMult = b.ReadFloat();
			this.mPct = b.ReadFloat();
		}

		public float mSizeXMult = 1f;

		public float mSizeYMult = 1f;

		public float mVelocityMult = 1f;

		public float mWeightMult = 1f;

		public float mSpinMult = 1f;

		public float mMotionRandMult = 1f;

		public float mBounceMult = 1f;

		public float mZoomMult = 1f;

		public float mNumberMult = 1f;

		public float mPct;
	}
}
