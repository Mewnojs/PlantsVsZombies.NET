using System;
using System.Collections.Generic;

namespace Sexy
{
	public/*internal*/ class AnimInfo
	{
		public AnimInfo()
		{
			this.mAnimType = AnimType.AnimType_None;
			this.mFrameDelay = 1;
			this.mNumCels = 1;
			this.mTotalAnimTime = 0;
		}

		public AnimInfo(AnimInfo anotherAnim)
		{
			this.mAnimType = anotherAnim.mAnimType;
			this.mFrameDelay = anotherAnim.mFrameDelay;
			this.mNumCels = anotherAnim.mNumCels;
			this.mTotalAnimTime = anotherAnim.mTotalAnimTime;
			this.mPerFrameDelay = anotherAnim.mPerFrameDelay;
			this.mFrameMap = anotherAnim.mFrameMap;
		}

		public void Dispose()
		{
		}

		public void SetPerFrameDelay(int theFrame, int theTime)
		{
			if (this.mPerFrameDelay.Count <= theFrame)
			{
				this.mPerFrameDelay.Capacity = theFrame + 1;
			}
			this.mPerFrameDelay[theFrame] = theTime;
		}

		public void Compute(int theNumCels)
		{
			this.Compute(theNumCels, 0, 0);
		}

		public void Compute(int theNumCels, int theBeginFrameTime)
		{
			this.Compute(theNumCels, theBeginFrameTime, 0);
		}

		public void Compute(int theNumCels, int theBeginFrameTime, int theEndFrameTime)
		{
			this.mNumCels = theNumCels;
			if (this.mNumCels <= 0)
			{
				this.mNumCels = 1;
			}
			if (this.mFrameDelay <= 0)
			{
				this.mFrameDelay = 1;
			}
			if (this.mAnimType == AnimType.AnimType_PingPong && this.mNumCels > 1)
			{
				this.mFrameMap.Capacity = theNumCels * 2 - 2;
				int num = 0;
				for (int i = 0; i < theNumCels; i++)
				{
					this.mFrameMap[num++] = i;
				}
				for (int i = theNumCels - 2; i >= 1; i--)
				{
					this.mFrameMap[num++] = i;
				}
			}
			if (this.mFrameMap.Count != 0)
			{
				this.mNumCels = this.mFrameMap.Count;
			}
			if (theBeginFrameTime > 0)
			{
				this.SetPerFrameDelay(0, theBeginFrameTime);
			}
			if (theEndFrameTime > 0)
			{
				this.SetPerFrameDelay(this.mNumCels - 1, theEndFrameTime);
			}
			if (this.mPerFrameDelay.Count != 0)
			{
				this.mTotalAnimTime = 0;
				this.mPerFrameDelay.Capacity = this.mNumCels;
				for (int i = 0; i < this.mNumCels; i++)
				{
					if (this.mPerFrameDelay[i] <= 0)
					{
						this.mPerFrameDelay[i] = this.mFrameDelay;
					}
					this.mTotalAnimTime += this.mPerFrameDelay[i];
				}
			}
			else
			{
				this.mTotalAnimTime = this.mFrameDelay * this.mNumCels;
			}
			if (this.mFrameMap.Count != 0)
			{
				this.mFrameMap.Capacity = this.mNumCels;
			}
		}

		public int GetPerFrameCel(int theTime)
		{
			for (int i = 0; i < this.mNumCels; i++)
			{
				theTime -= this.mPerFrameDelay[i];
				if (theTime < 0)
				{
					return i;
				}
			}
			return this.mNumCels - 1;
		}

		public int GetCel(int theTime)
		{
			if (this.mAnimType == AnimType.AnimType_Once && theTime >= this.mTotalAnimTime)
			{
				if (this.mFrameMap.Count != 0)
				{
					return this.mFrameMap[this.mFrameMap.Count - 1];
				}
				return this.mNumCels - 1;
			}
			else
			{
				theTime %= this.mTotalAnimTime;
				int num;
				if (this.mPerFrameDelay.Count != 0)
				{
					num = this.GetPerFrameCel(theTime);
				}
				else
				{
					num = theTime / this.mFrameDelay % this.mNumCels;
				}
				if (this.mFrameMap.Count == 0)
				{
					return num;
				}
				return this.mFrameMap[num];
			}
		}

		public AnimType mAnimType;

		public int mFrameDelay;

		public int mNumCels;

		public List<int> mPerFrameDelay = new List<int>();

		public List<int> mFrameMap = new List<int>();

		public int mTotalAnimTime;
	}
}
