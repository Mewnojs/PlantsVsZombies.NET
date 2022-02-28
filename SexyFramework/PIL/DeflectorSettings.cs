using System;

namespace Sexy.PIL
{
	public class DeflectorSettings : KeyFrameData
	{
		public int mThickness
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

		public bool mActive
		{
			get
			{
				return this.mBoolData[0];
			}
			set
			{
				this.mBoolData[0] = value;
			}
		}

		public float mAngle
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

		public float mBounceMult
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

		public float mHitChance
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

		public float mCollisionMult
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

		public override void Init()
		{
		}

		public override KeyFrameData Clone()
		{
			return new DeflectorSettings(this);
		}

		protected void Reset()
		{
			this.mNumInts = 1;
			this.mNumBools = 1;
			this.mNumFloats = 4;
			this.mIntData = new int[this.mNumInts];
			this.mFloatData = new float[this.mNumFloats];
			this.mBoolData = new bool[this.mNumBools];
			this.mThickness = 2;
			this.mAngle = 0f;
			this.mBounceMult = 1f;
			this.mHitChance = 1f;
			this.mActive = true;
			this.mCollisionMult = 1f;
		}

		public DeflectorSettings()
		{
			this.Reset();
		}

		public DeflectorSettings(DeflectorSettings rhs)
		{
			this.Reset();
			base.CopyFrom(rhs);
		}

		public new static KeyFrameData Instantiate()
		{
			return new DeflectorSettings();
		}
	}
}
