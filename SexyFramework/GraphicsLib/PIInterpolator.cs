using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public class PIInterpolator
	{
		public int GetValueAt(float theTime)
		{
			if (this.mInterpolatorPointVector.Count == 1)
			{
				return this.mInterpolatorPointVector[0].mValue;
			}
			PIInterpolatorPoint piinterpolatorPoint = this.mInterpolatorPointVector[0];
			PIInterpolatorPoint piinterpolatorPoint2 = this.mInterpolatorPointVector[this.mInterpolatorPointVector.Count - 1];
			float num = piinterpolatorPoint.mTime + theTime * (piinterpolatorPoint2.mTime - piinterpolatorPoint.mTime);
			int i = 1;
			while (i < this.mInterpolatorPointVector.Count)
			{
				PIInterpolatorPoint piinterpolatorPoint3 = this.mInterpolatorPointVector[i - 1];
				PIInterpolatorPoint piinterpolatorPoint4 = this.mInterpolatorPointVector[i];
				if (num > piinterpolatorPoint3.mTime && num < piinterpolatorPoint4.mTime)
				{
					return (int)GlobalPIEffect.InterpColor(piinterpolatorPoint3.mValue, piinterpolatorPoint4.mValue, (num - piinterpolatorPoint3.mTime) / (piinterpolatorPoint4.mTime - piinterpolatorPoint3.mTime));
				}
				if (i == this.mInterpolatorPointVector.Count - 1)
				{
					if (num >= piinterpolatorPoint4.mTime)
					{
						return piinterpolatorPoint4.mValue;
					}
					return piinterpolatorPoint3.mValue;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		public int GetKeyframeNum(int theIdx)
		{
			if (this.mInterpolatorPointVector.Count == 0)
			{
				return 0;
			}
			return this.mInterpolatorPointVector[theIdx % this.mInterpolatorPointVector.Count].mValue;
		}

		public float GetKeyframeTime(int theIdx)
		{
			if (this.mInterpolatorPointVector.Count == 0)
			{
				return 0f;
			}
			return this.mInterpolatorPointVector[theIdx % this.mInterpolatorPointVector.Count].mTime;
		}

		public List<PIInterpolatorPoint> mInterpolatorPointVector = new List<PIInterpolatorPoint>();
	}
}
