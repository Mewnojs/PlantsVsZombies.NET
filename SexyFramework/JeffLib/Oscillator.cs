using System;

namespace JeffLib
{
	public class Oscillator
	{
		public void Init(float min_val, float max_val, bool start_at_max, float accel)
		{
			this.mAccel = accel;
			this.mMinVal = min_val;
			this.mMaxVal = max_val;
			this.mInc = 0f;
			if (start_at_max)
			{
				this.mVal = this.mMaxVal;
				this.mForward = false;
				return;
			}
			this.mVal = this.mMinVal;
			this.mForward = true;
		}

		public void Update()
		{
			if (this.mForward)
			{
				this.mInc += this.mAccel;
				this.mVal += this.mInc;
				if (this.mVal >= this.mMaxVal)
				{
					this.mVal = this.mMaxVal;
					this.mForward = false;
					return;
				}
			}
			else
			{
				this.mInc -= this.mAccel;
				this.mVal += this.mInc;
				if (this.mVal <= this.mMinVal)
				{
					this.mVal = this.mMinVal;
					this.mForward = true;
				}
			}
		}

		public float GetVal()
		{
			return this.mVal;
		}

		public float mVal;

		public float mMinVal;

		public float mMaxVal;

		public float mInc;

		public float mAccel;

		public bool mForward;
	}
}
