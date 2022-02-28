using System;

namespace Sexy.PIL
{
	public class EmitterSettings : KeyFrameData
	{
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

		public float mVisibility
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

		public float mEmissionAngle
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

		public float mEmissionRange
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

		public float mTintStrength
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

		public float mAngle
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

		public float mXRadius
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

		public float mYRadius
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

		public override void Init()
		{
		}

		public override KeyFrameData Clone()
		{
			return new EmitterSettings(this);
		}

		protected void Reset()
		{
			this.mNumFloats = 7;
			this.mNumBools = 1;
			this.mFloatData = new float[this.mNumFloats];
			this.mBoolData = new bool[this.mNumBools];
			this.mVisibility = 1f;
			this.mEmissionAngle = 0f;
			this.mEmissionRange = 6.2831855f;
			this.mTintStrength = 0f;
			this.mActive = true;
			this.mAngle = 0f;
			this.mXRadius = 0f;
			this.mYRadius = 0f;
		}

		public EmitterSettings()
		{
			this.Reset();
		}

		public EmitterSettings(EmitterSettings emitterSettings)
		{
			this.Reset();
			base.CopyFrom(emitterSettings);
		}

		public new static KeyFrameData Instantiate()
		{
			return new EmitterSettings();
		}
	}
}
