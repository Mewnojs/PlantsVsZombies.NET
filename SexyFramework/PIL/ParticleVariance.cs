using System;

namespace Sexy.PIL
{
	public class ParticleVariance : KeyFrameData
	{
		public int mLifeVar
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

		public int mNumberVar
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

		public int mSizeXVar
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

		public int mSizeYVar
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

		public int mVelocityVar
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

		public int mWeightVar
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

		public int mBounceVar
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

		public float mMotionRandVar
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

		public float mSpinVar
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

		public override void Init()
		{
		}

		public override KeyFrameData Clone()
		{
			return new ParticleVariance(this);
		}

		protected void Reset()
		{
			this.mNumInts = 7;
			this.mNumFloats = 2;
			this.mIntData = new int[this.mNumInts];
			this.mFloatData = new float[this.mNumFloats];
			this.mLifeVar = 0;
			this.mNumberVar = 0;
			this.mSizeXVar = 0;
			this.mSizeYVar = 0;
			this.mVelocityVar = 0;
			this.mWeightVar = 0;
			this.mSpinVar = 0f;
			this.mMotionRandVar = 0f;
			this.mBounceVar = 0;
		}

		public ParticleVariance()
		{
			this.Reset();
		}

		public ParticleVariance(ParticleVariance rhs)
		{
			this.Reset();
			base.CopyFrom(rhs);
		}

		public new static KeyFrameData Instantiate()
		{
			return new ParticleVariance();
		}
	}
}
