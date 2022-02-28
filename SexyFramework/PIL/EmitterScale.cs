using System;

namespace Sexy.PIL
{
	public class EmitterScale : KeyFrameData
	{
		public float mLifeScale
		{
			get
			{
				return this.mFloatData[0];
			}
			set
			{
				this.mFloatData[0] = value;
			}
		}

		public float mNumberScale
		{
			get
			{
				return this.mFloatData[1];
			}
			set
			{
				this.mFloatData[1] = value;
			}
		}

		public float mSizeXScale
		{
			get
			{
				return this.mFloatData[2];
			}
			set
			{
				this.mFloatData[2] = value;
			}
		}

		public float mSizeYScale
		{
			get
			{
				return this.mFloatData[3];
			}
			set
			{
				this.mFloatData[3] = value;
			}
		}

		public float mVelocityScale
		{
			get
			{
				return this.mFloatData[4];
			}
			set
			{
				this.mFloatData[4] = value;
			}
		}

		public float mWeightScale
		{
			get
			{
				return this.mFloatData[5];
			}
			set
			{
				this.mFloatData[5] = value;
			}
		}

		public float mSpinScale
		{
			get
			{
				return this.mFloatData[6];
			}
			set
			{
				this.mFloatData[6] = value;
			}
		}

		public float mMotionRandScale
		{
			get
			{
				return this.mFloatData[7];
			}
			set
			{
				this.mFloatData[7] = value;
			}
		}

		public float mZoom
		{
			get
			{
				return this.mFloatData[8];
			}
			set
			{
				this.mFloatData[8] = value;
			}
		}

		public float mBounceScale
		{
			get
			{
				return this.mFloatData[9];
			}
			set
			{
				this.mFloatData[9] = value;
			}
		}

		public override void Init()
		{
		}

		public override KeyFrameData Clone()
		{
			return new EmitterScale(this);
		}

		protected void Reset()
		{
			this.mNumFloats = 10;
			this.mFloatData = new float[this.mNumFloats];
			this.mLifeScale = 1f;
			this.mNumberScale = 1f;
			this.mSizeXScale = 1f;
			this.mSizeYScale = 1f;
			this.mVelocityScale = 1f;
			this.mWeightScale = 1f;
			this.mSpinScale = 1f;
			this.mMotionRandScale = 1f;
			this.mZoom = 1f;
			this.mBounceScale = 1f;
		}

		public EmitterScale()
		{
			this.Reset();
		}

		public EmitterScale(EmitterScale rhs)
		{
			this.Reset();
			base.CopyFrom(rhs);
		}

		public new static KeyFrameData Instantiate()
		{
			return new EmitterScale();
		}
	}
}
