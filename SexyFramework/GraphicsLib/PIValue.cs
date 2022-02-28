using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class PIValue : IDisposable
	{
		public PIValue()
		{
			this.mLastTime = -1f;
			this.mLastCurveT = 0f;
			this.mLastCurveTDelta = 0.01f;
		}

		public virtual void Dispose()
		{
			this.mBezier.Dispose();
			this.mValuePointVector.Clear();
		}

		public void QuantizeCurve()
		{
			float mTime = this.mValuePointVector[0].mTime;
			float mTime2 = this.mValuePointVector[this.mValuePointVector.Count - 1].mTime;
			this.mQuantTable.Clear();
			this.mQuantTable.Resize(GlobalPIEffect.PI_QUANT_SIZE);
			bool flag = true;
			int num = 0;
			float num2 = 0f;
			float num3 = mTime;
			float num4 = (mTime2 - mTime) / (float)GlobalPIEffect.PI_QUANT_SIZE / 2f;
			int num5 = 0;
			for (;;)
			{
				Vector2 vector = this.mBezier.Evaluate(num3);
				int num6 = (int)GlobalPIEffect.TIME_TO_X(vector.X, mTime, mTime2);
				bool flag2 = false;
				while (vector.X >= this.mValuePointVector[num5 + 1].mTime)
				{
					num5++;
					if (num5 >= this.mValuePointVector.Count - 1)
					{
						flag2 = true;
						break;
					}
				}
				if (flag2)
				{
					break;
				}
				if (vector.X >= this.mValuePointVector[num5].mTime)
				{
					if (!flag && num6 > num + 1)
					{
						for (int i = num; i <= num6; i++)
						{
							float num7 = (float)(i - num) / (float)(num6 - num);
							float num8 = num7 * vector.Y + (1f - num7) * num2;
							this.mQuantTable[i] = num8;
						}
					}
					else
					{
						float y = vector.Y;
						this.mQuantTable[num6] = y;
					}
					num = num6;
					num2 = vector.Y;
				}
				flag = false;
				num3 += num4;
			}
			for (int j = 0; j < this.mValuePointVector.Count; j++)
			{
				this.mQuantTable[(int)GlobalPIEffect.TIME_TO_X(this.mValuePointVector[j].mTime, mTime, mTime2)] = this.mValuePointVector[j].mValue;
			}
		}

		public float GetValueAt(float theTime)
		{
			return this.GetValueAt(theTime, 0f);
		}

		public float GetValueAt(float theTime, float theDefault)
		{
			if (this.mLastTime == theTime)
			{
				return this.mLastValue;
			}
			float num = this.mLastTime;
			this.mLastTime = theTime;
			if (this.mValuePointVector.Count == 1)
			{
				return this.mLastValue = this.mValuePointVector[0].mValue;
			}
			if (!this.mBezier.IsInitialized())
			{
				for (int i = 1; i < this.mValuePointVector.Count; i++)
				{
					PIValuePoint pivaluePoint = this.mValuePointVector[i - 1];
					PIValuePoint pivaluePoint2 = this.mValuePointVector[i];
					if (theTime > pivaluePoint.mTime && theTime < pivaluePoint2.mTime)
					{
						return this.mLastValue = pivaluePoint.mValue + (pivaluePoint2.mValue - pivaluePoint.mValue) * (theTime - pivaluePoint.mTime) / (pivaluePoint2.mTime - pivaluePoint.mTime);
					}
					if (i == this.mValuePointVector.Count - 1)
					{
						if (theTime >= pivaluePoint2.mTime)
						{
							this.mLastValue = pivaluePoint2.mValue;
						}
						else
						{
							this.mLastValue = pivaluePoint.mValue;
						}
						return this.mLastValue;
					}
				}
				this.mLastValue = theDefault;
				return theDefault;
			}
			float mTime = this.mValuePointVector[0].mTime;
			float mTime2 = this.mValuePointVector[this.mValuePointVector.Count - 1].mTime;
			if (mTime2 <= 1.001f)
			{
				if (this.mQuantTable.Count == 0)
				{
					this.QuantizeCurve();
				}
				float num2 = GlobalPIEffect.TIME_TO_X(theTime, mTime, mTime2);
				if (num2 <= 0f)
				{
					return this.mLastValue = this.mValuePointVector[0].mValue;
				}
				if (num2 >= (float)(GlobalPIEffect.PI_QUANT_SIZE - 1))
				{
					return this.mLastValue = this.mValuePointVector[this.mValuePointVector.Count - 1].mValue;
				}
				int num3 = (int)num2;
				float num4 = num2 - (float)num3;
				this.mLastValue = this.mQuantTable[num3] * (1f - num4) + this.mQuantTable[num3 + 1] * num4;
				return this.mLastValue;
			}
			else
			{
				float num5 = Math.Min(0.1f, (mTime2 - mTime) / 1000f);
				if (theTime <= mTime)
				{
					return this.mLastValue = this.mValuePointVector[0].mValue;
				}
				if (theTime >= mTime2)
				{
					return this.mLastValue = this.mValuePointVector[this.mValuePointVector.Count - 1].mValue;
				}
				float num6 = mTime;
				float num7 = mTime2;
				Vector2 vector = default(Vector2);
				float num8 = 0f;
				bool flag = (theTime - num) / (mTime2 - mTime) > 0.05f;
				float[] array = new float[] { 0.1f, 0.1f, 0.1f, 0.5f };
				float[] array2 = new float[] { 1f, 0.75f, 1.25f };
				for (int j = 0; j < 1000; j++)
				{
					float num9 = num5;
					if (j < 4 && !flag)
					{
						num9 *= array[j];
					}
					if (j < 3 && this.mLastCurveTDelta != 0f && !flag)
					{
						num8 = this.mLastCurveT + this.mLastCurveTDelta * array2[j];
					}
					else
					{
						num8 = num6 + (num7 - num6) / 2f;
					}
					if (num8 >= num6 && num8 <= num7)
					{
						vector = this.mBezier.Evaluate(num8);
						float num10 = vector.X - theTime;
						if (Math.Abs(num10) <= num9)
						{
							break;
						}
						if (num10 < 0f)
						{
							num6 = num8;
						}
						else
						{
							num7 = num8;
						}
					}
				}
				this.mLastCurveTDelta = this.mLastCurveTDelta * 0.5f + (num8 - this.mLastCurveT) * 0.5f;
				this.mLastCurveT = num8;
				return this.mLastValue = vector.Y;
			}
		}

		public float GetLastKeyframe(float theTime)
		{
			for (int i = this.mValuePointVector.Count - 1; i >= 0; i--)
			{
				PIValuePoint pivaluePoint = this.mValuePointVector[i];
				if (theTime >= pivaluePoint.mTime)
				{
					return pivaluePoint.mValue;
				}
			}
			return 0f;
		}

		public float GetLastKeyframeTime(float theTime)
		{
			for (int i = this.mValuePointVector.Count - 1; i >= 0; i--)
			{
				PIValuePoint pivaluePoint = this.mValuePointVector[i];
				if (theTime >= pivaluePoint.mTime)
				{
					return pivaluePoint.mTime;
				}
			}
			return 0f;
		}

		public float GetNextKeyframeTime(float theTime)
		{
			for (int i = 0; i < this.mValuePointVector.Count; i++)
			{
				PIValuePoint pivaluePoint = this.mValuePointVector[i];
				if (pivaluePoint.mTime >= theTime)
				{
					return pivaluePoint.mTime;
				}
			}
			return 0f;
		}

		public int GetNextKeyframeIdx(float theTime)
		{
			for (int i = 0; i < this.mValuePointVector.Count; i++)
			{
				PIValuePoint pivaluePoint = this.mValuePointVector[i];
				if (pivaluePoint.mTime >= theTime)
				{
					return i;
				}
			}
			return -1;
		}

		public List<float> mQuantTable = new List<float>();

		public List<PIValuePoint> mValuePointVector = new List<PIValuePoint>();

		public Bezier mBezier = new Bezier();

		public float mLastTime;

		public float mLastValue;

		public float mLastCurveT;

		public float mLastCurveTDelta;
	}
}
