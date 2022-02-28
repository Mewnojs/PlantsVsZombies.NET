using System;

namespace JeffLib
{
	public class Bouncy
	{
		public Bouncy()
		{
			this.mCount = 0;
			this.mMaxBounces = 0;
			this.mPct = 0f;
			this.mRate = 0f;
			this.mStartingPct = 0f;
			this.mStartInc = true;
			this.mInc = true;
			this.mDone = false;
			this.mRateDivFactor = 2f;
			this.mStartingRate = 0f;
		}

		public void Dispose()
		{
		}

		public void Update()
		{
			if (this.mDone)
			{
				return;
			}
			this.mPct += (this.mInc ? this.mRate : (-this.mRate));
			float num;
			if (this.mCount == this.mMaxBounces)
			{
				num = this.mFinalPct;
			}
			else
			{
				num = (this.mInc ? this.mMaxPct : this.mMinPct);
			}
			if (this.mInc && this.mPct >= num)
			{
				this.mPct = num;
				this.mInc = false;
				this.mCount++;
				this.mRate /= this.mRateDivFactor;
			}
			else if (!this.mInc && this.mPct <= num)
			{
				this.mPct = num;
				this.mInc = true;
				this.mCount++;
				this.mRate /= this.mRateDivFactor;
			}
			if (this.mCount > this.mMaxBounces)
			{
				this.mDone = true;
			}
		}

		public void Reset()
		{
			this.mCount = 0;
			this.mPct = this.mStartingPct;
			this.mInc = this.mStartInc;
			this.mDone = false;
			this.mRate = this.mStartingRate;
		}

		public float GetPct()
		{
			return this.mPct;
		}

		public int GetCount()
		{
			return this.mCount;
		}

		public bool IsDone()
		{
			return this.mDone;
		}

		public void SetNumBounces(int b)
		{
			this.mMaxBounces = b;
		}

		public void SetPct(float p)
		{
			this.SetPct(p, true);
		}

		public void SetPct(float p, bool inc)
		{
			this.mStartingPct = p;
			this.mPct = p;
			this.mStartInc = inc;
			this.mInc = inc;
		}

		public void SetTargetPercents(float minp, float maxp, float finalp)
		{
			this.mMinPct = minp;
			this.mMaxPct = maxp;
			this.mFinalPct = finalp;
		}

		public void SetRate(float r)
		{
			this.mStartingRate = r;
			this.mRate = r;
		}

		public void SetRateDivFactor(float d)
		{
			this.mRateDivFactor = d;
		}

		protected int mCount;

		protected int mMaxBounces;

		protected float mPct;

		protected float mMaxPct;

		protected float mMinPct;

		protected float mFinalPct;

		protected float mRate;

		protected float mRateDivFactor;

		protected bool mInc;

		protected bool mDone;

		protected float mStartingPct;

		protected float mStartingRate;

		protected bool mStartInc;
	}
}
