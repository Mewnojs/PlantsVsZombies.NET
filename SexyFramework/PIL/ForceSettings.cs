using System;

namespace Sexy.PIL
{
	public class ForceSettings : KeyFrameData
	{
		public float mWidth
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

		public float mHeight
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

		public float mStrength
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

		public float mDirection
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

		public override void Init()
		{
		}

		public override KeyFrameData Clone()
		{
			return new ForceSettings(this);
		}

		protected void Reset()
		{
			this.mNumFloats = 5;
			this.mFloatData = new float[this.mNumFloats];
			this.mWidth = 0f;
			this.mHeight = 0f;
			this.mStrength = 0f;
			this.mDirection = 0f;
			this.mAngle = 0f;
		}

		public ForceSettings()
		{
			this.Reset();
		}

		public ForceSettings(ForceSettings rhs)
		{
			this.Reset();
			base.CopyFrom(rhs);
		}

		public new static KeyFrameData Instantiate()
		{
			return new ForceSettings();
		}
	}
}
