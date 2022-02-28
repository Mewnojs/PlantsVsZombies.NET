using System;

namespace Sexy.PIL
{
	public class ParticleSettings : KeyFrameData
	{
		public float mWeight
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

		public float mSpin
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

		public float mMotionRand
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

		public float mGlobalVisibility
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

		public int mXSize
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

		public int mYSize
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

		public int mVelocity
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

		public int mBounce
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

		public int mNumber
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

		public override void Init()
		{
		}

		public override KeyFrameData Clone()
		{
			return new ParticleSettings(this);
		}

		protected void Reset()
		{
			this.mNumInts = 6;
			this.mNumFloats = 4;
			this.mIntData = new int[this.mNumInts];
			this.mFloatData = new float[this.mNumFloats];
			this.mLife = 0;
			this.mXSize = 0;
			this.mYSize = 0;
			this.mVelocity = 0;
			this.mBounce = 0;
			this.mNumber = 0;
			this.mWeight = 0f;
			this.mSpin = 0f;
			this.mMotionRand = 0f;
			this.mGlobalVisibility = 1f;
		}

		public ParticleSettings()
		{
			this.Reset();
		}

		public ParticleSettings(ParticleSettings rhs)
		{
			this.Reset();
			base.CopyFrom(rhs);
		}

		public new static KeyFrameData Instantiate()
		{
			return new ParticleSettings();
		}
	}
}
