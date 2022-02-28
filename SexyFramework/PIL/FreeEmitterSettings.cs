using System;

namespace Sexy.PIL
{
	public class FreeEmitterSettings : KeyFrameData
	{
		public int mLife
		{
			get
			{
				return this.mIntData[0];
			}
			set
			{
				this.mIntData[0] = value;
			}
		}

		public int mNumber
		{
			get
			{
				return this.mIntData[1];
			}
			set
			{
				this.mIntData[1] = value;
			}
		}

		public int mVelocity
		{
			get
			{
				return this.mIntData[2];
			}
			set
			{
				this.mIntData[2] = value;
			}
		}

		public int mWeight
		{
			get
			{
				return this.mIntData[3];
			}
			set
			{
				this.mIntData[3] = value;
			}
		}

		public int mMotionRand
		{
			get
			{
				return this.mIntData[4];
			}
			set
			{
				this.mIntData[4] = value;
			}
		}

		public int mBounce
		{
			get
			{
				return this.mIntData[5];
			}
			set
			{
				this.mIntData[5] = value;
			}
		}

		public int mZoom
		{
			get
			{
				return this.mIntData[6];
			}
			set
			{
				this.mIntData[6] = value;
			}
		}

		public float mSpin
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

		public override void Init()
		{
		}

		public override KeyFrameData Clone()
		{
			return new FreeEmitterSettings(this);
		}

		protected void Reset()
		{
			this.mNumInts = 7;
			this.mIntData = new int[this.mNumInts];
			this.mNumFloats = 1;
			this.mFloatData = new float[this.mNumFloats];
			this.mLife = 0;
			this.mNumber = 0;
			this.mVelocity = 0;
			this.mWeight = 0;
			this.mSpin = 0f;
			this.mMotionRand = 0;
			this.mBounce = 0;
			this.mZoom = 100;
		}

		public FreeEmitterSettings()
		{
			this.Reset();
		}

		public FreeEmitterSettings(FreeEmitterSettings rhs)
		{
			this.Reset();
			base.CopyFrom(rhs);
		}

		public new static KeyFrameData Instantiate()
		{
			return new FreeEmitterSettings();
		}
	}
}
