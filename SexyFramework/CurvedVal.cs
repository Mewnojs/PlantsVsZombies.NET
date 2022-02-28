using System;
using System.Collections.Generic;
using System.Globalization;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy
{
	public class CurvedVal
	{
		private static int SIGN(int aVal)
		{
			if (aVal < 0)
			{
				return -1;
			}
			if (aVal <= 0)
			{
				return 0;
			}
			return 1;
		}

		private static float SIGN(float aVal)
		{
			if (aVal < 0f)
			{
				return -1f;
			}
			if (aVal <= 0f)
			{
				return 0f;
			}
			return 1f;
		}

		private static float CVCharToFloat(sbyte theChar)
		{
			if (theChar >= 92)
			{
				theChar -= 1;
			}
			return (float)(theChar - 35) / 90f;
		}

		private static int CVCharToInt(sbyte theChar)
		{
			if (theChar >= 92)
			{
				theChar -= 1;
			}
			return (int)(theChar - 35);
		}

		private static float CVStrToAngle(string theStr)
		{
			int num = 0;
			num += CurvedVal.CVCharToInt((sbyte)theStr[0]);
			num *= 90;
			num += CurvedVal.CVCharToInt((sbyte)theStr[1]);
			num *= 90;
			num += CurvedVal.CVCharToInt((sbyte)theStr[2]);
			return (float)num * 360f / 729000f;
		}

		public static implicit operator double(CurvedVal ImpliedObject)
		{
			return ImpliedObject.GetOutVal();
		}

		public static implicit operator SexyColor(CurvedVal ImpliedObject)
		{
			return new SexyColor(255, 255, 255, (int)(255.0 * ImpliedObject.GetOutVal()));
		}

		public int mAppUpdateCountSrc
		{
			get
			{
				if (GlobalMembers.gSexyAppBase != null)
				{
					return GlobalMembers.gSexyAppBase.mUpdateCount;
				}
				return 0;
			}
			set
			{
				if (GlobalMembers.gSexyAppBase != null)
				{
					GlobalMembers.gSexyAppBase.mUpdateCount = value;
				}
			}
		}

		protected void InitVarDefaults()
		{
			this.mMode = 0;
			this.mRamp = 0;
			this.mCurveCacheRecord = null;
			this.mSingleTrigger = false;
			this.mNoClip = false;
			this.mOutputSync = false;
			this.mTriggered = false;
			this.mIsHermite = false;
			this.mAutoInc = false;
			this.mInitAppUpdateCount = 0;
			this.mOutMin = 0.0;
			this.mOutMax = 1.0;
			this.mInMin = 0.0;
			this.mInMax = 1.0;
			this.mLinkedVal = null;
			this.mCurOutVal = 0.0;
			this.mInVal = 0.0;
			this.mPrevInVal = 0.0;
			this.mIncRate = 0.0;
			this.mPrevOutVal = 0.0;
			this.mDataP = "";
			this.mCurDataPStr = null;
			if (CurvedVal.mCurveCacheMap != null)
			{
				CurvedVal.mCurveCacheMap.Clear();
			}
		}

		protected bool CheckCurveChange()
		{
			if (this.mDataP != null && this.mDataP != this.mCurDataPStr)
			{
				this.mCurDataPStr = this.mDataP;
				this.ParseDataString(this.mCurDataPStr);
				return true;
			}
			return false;
		}

		protected bool CheckClamping()
		{
			this.CheckCurveChange();
			if (this.mMode == 0)
			{
				if (this.mInVal < this.mInMin)
				{
					this.mInVal = this.mInMin;
					return false;
				}
				if (this.mInVal > this.mInMax)
				{
					this.mInVal = this.mInMax;
					return false;
				}
			}
			else if (this.mMode == 1 || this.mMode == 2)
			{
				double num = this.mInMax - this.mInMin;
				if (this.mInVal > this.mInMax || this.mInVal < this.mInMin)
				{
					this.mInVal = this.mInMin + Math.IEEERemainder(this.mInVal - this.mInMin + num, num);
				}
			}
			return true;
		}

		protected void GenerateTable(List<CurvedVal.DataPoint> theDataPointVector, float[] theBuffer, int theSize)
		{
			BSpline bspline = new BSpline();
			for (int i = 0; i < theDataPointVector.Count; i++)
			{
				CurvedVal.DataPoint dataPoint = theDataPointVector[i];
				bspline.AddPoint(dataPoint.mX, dataPoint.mY);
			}
			bspline.CalculateSpline();
			bool flag = true;
			int num = 0;
			float num2 = 0f;
			for (int j = 1; j < theDataPointVector.Count; j++)
			{
				CurvedVal.DataPoint dataPoint2 = theDataPointVector[j - 1];
				CurvedVal.DataPoint dataPoint3 = theDataPointVector[j];
				int num3 = (int)((double)(dataPoint2.mX * (float)(theSize - 1)) + 0.5);
				int num4 = (int)((double)(dataPoint3.mX * (float)(theSize - 1)) + 0.5);
				for (int k = num3; k <= num4; k++)
				{
					float t = (float)(j - 1) + (float)(k - num3) / (float)(num4 - num3);
					float ypoint = bspline.GetYPoint(t);
					float xpoint = bspline.GetXPoint(t);
					int num5 = (int)((double)(xpoint * (float)(theSize - 1)) + 0.5);
					if (num5 >= num && num5 <= num4)
					{
						if (!flag)
						{
							if (num5 > num + 1)
							{
								for (int l = num; l <= num5; l++)
								{
									float num6 = (float)(l - num) / (float)(num5 - num);
									float num7 = num6 * ypoint + (1f - num6) * num2;
									if (!this.mNoClip)
									{
										num7 = Math.Min(Math.Max(num7, 0f), 1f);
									}
									theBuffer[l] = num7;
								}
							}
							else
							{
								float num8 = ypoint;
								if (!this.mNoClip)
								{
									num8 = Math.Min(Math.Max(num8, 0f), 1f);
								}
								theBuffer[num5] = num8;
							}
						}
						num = num5;
						num2 = ypoint;
						flag = false;
					}
				}
			}
			for (int m = 0; m < theDataPointVector.Count; m++)
			{
				CurvedVal.DataPoint dataPoint4 = theDataPointVector[m];
				int num9 = (int)((double)(dataPoint4.mX * (float)(theSize - 1)) + 0.5);
				theBuffer[num9] = dataPoint4.mY;
			}
		}

		protected void ParseDataString(string theString)
		{
			this.mIncRate = 0.0;
			this.mOutMin = 0.0;
			this.mOutMax = 1.0;
			this.mSingleTrigger = false;
			this.mNoClip = false;
			this.mOutputSync = false;
			this.mIsHermite = false;
			this.mAutoInc = false;
			int i = 0;
			int num = 0;
			if (theString[0] >= 'a' && theString[0] <= 'b')
			{
				num = (int)(theString[0] - 'a');
			}
			i++;
			if (num >= 1)
			{
				int num2 = CurvedVal.CVCharToInt((sbyte)theString[i++]);
				this.mNoClip = (num2 & CurvedVal.DFLAG_NOCLIP) != 0;
				this.mSingleTrigger = (num2 & CurvedVal.DFLAG_SINGLETRIGGER) != 0;
				this.mOutputSync = (num2 & CurvedVal.DFLAG_OUTPUTSYNC) != 0;
				this.mIsHermite = (num2 & CurvedVal.DFLAG_HERMITE) != 0;
				this.mAutoInc = (num2 & CurvedVal.DFLAG_AUTOINC) != 0;
			}
			int num3 = theString.IndexOf(',', i);
			if (num3 == -1)
			{
				this.mIsHermite = true;
				return;
			}
			double num4 = 0.0;
			double.TryParse(theString.Substring(i, num3 - i), NumberStyles.Float, CultureInfo.InvariantCulture, out num4);
			this.mOutMin = (double)((float)num4);
			i = num3 + 1;
			num3 = theString.IndexOf(',', i);
			if (num3 == -1)
			{
				return;
			}
			num4 = 0.0;
			double.TryParse(theString.Substring(i, num3 - i), NumberStyles.Float, CultureInfo.InvariantCulture, out num4);
			this.mOutMax = (double)((float)num4);
			i = num3 + 1;
			num3 = theString.IndexOf(',', i);
			if (num3 == -1)
			{
				return;
			}
			num4 = 0.0;
			double.TryParse(theString.Substring(i, num3 - i), NumberStyles.Float, CultureInfo.InvariantCulture, out num4);
			this.mIncRate = (double)((float)num4);
			i = num3 + 1;
			if (num >= 1)
			{
				num3 = theString.IndexOf(',', i);
				if (num3 == -1)
				{
					return;
				}
				num4 = 0.0;
				double.TryParse(theString.Substring(i, num3 - i), NumberStyles.Float, CultureInfo.InvariantCulture, out num4);
				this.mInMax = (double)((float)num4);
				i = num3 + 1;
			}
			string text = theString.Substring(i);
			if (!CurvedVal.mCurveCacheMap.ContainsKey(text))
			{
				CurvedVal.CurveCacheRecord curveCacheRecord = new CurvedVal.CurveCacheRecord();
				CurvedVal.mCurveCacheMap.Add(text, curveCacheRecord);
				this.mCurveCacheRecord = curveCacheRecord;
				List<CurvedVal.DataPoint> list = new List<CurvedVal.DataPoint>();
				float num5 = 0f;
				while (i < theString.Length)
				{
					sbyte b = (sbyte)theString[i++];
					CurvedVal.DataPoint dataPoint = new CurvedVal.DataPoint();
					dataPoint.mX = num5;
					dataPoint.mY = CurvedVal.CVCharToFloat(b);
					if (this.mIsHermite)
					{
						string theStr = theString.Substring(i, 3);
						dataPoint.mAngleDeg = CurvedVal.CVStrToAngle(theStr);
						i += 3;
					}
					else
					{
						dataPoint.mAngleDeg = 0f;
					}
					list.Add(dataPoint);
					while (i < theString.Length)
					{
						b = (sbyte)theString[i++];
						if (b != 32)
						{
							num5 = Math.Min(num5 + CurvedVal.CVCharToFloat(b) * 0.1f, 1f);
							break;
						}
						num5 += 0.1f;
					}
				}
				this.GenerateTable(list, this.mCurveCacheRecord.mTable, CurvedVal.CV_NUM_SPLINE_POINTS);
				this.mCurveCacheRecord.mDataStr = theString;
				this.mCurveCacheRecord.mHermiteCurve.mPoints.Clear();
				for (int j = 0; j < list.Count; j++)
				{
					CurvedVal.DataPoint dataPoint2 = list[j];
					float inFxPrime = (float)Math.Tan((double)SexyMath.DegToRad(dataPoint2.mAngleDeg));
					this.mCurveCacheRecord.mHermiteCurve.mPoints.Add(new SexyMathHermite.SPoint(dataPoint2.mX, dataPoint2.mY, inFxPrime));
				}
				this.mCurveCacheRecord.mHermiteCurve.Rebuild();
				return;
			}
			this.mCurveCacheRecord = CurvedVal.mCurveCacheMap[text];
		}

		public CurvedVal()
		{
			this.InitVarDefaults();
		}

		public CurvedVal(string theData, CurvedVal theLinkedVal)
		{
			this.InitVarDefaults();
			this.SetCurve(theData, theLinkedVal);
		}

		public CurvedVal(string theDataP)
			: this(theDataP, null)
		{
		}

		public void SetCurve(string theData, CurvedVal theLinkedVal)
		{
			this.mDataP = theData;
			this.mCurDataPStr = theData;
			if (this.mAppUpdateCountSrc != 0)
			{
				this.mInitAppUpdateCount = this.mAppUpdateCountSrc;
			}
			this.mTriggered = false;
			this.mLinkedVal = theLinkedVal;
			this.mRamp = 6;
			this.ParseDataString(theData);
			this.mInVal = this.mInMin;
		}

		public void SetCurve(string theData)
		{
			this.SetCurve(theData, null);
		}

		public void SetCurveMult(string theData)
		{
			this.SetCurveMult(theData, null);
		}

		public void SetCurveMult(string theData, CurvedVal theLinkedVal)
		{
			double outVal = this.GetOutVal();
			this.SetCurve(theData, theLinkedVal);
			this.mOutMax *= outVal;
		}

		public void SetConstant(double theValue)
		{
			this.mInVal = 0.0;
			this.mTriggered = false;
			this.mLinkedVal = null;
			this.mRamp = 1;
			this.mInMin = (this.mInMax = 0.0);
			this.mOutMax = theValue;
			this.mOutMin = theValue;
		}

		public bool IsInitialized()
		{
			return this.mRamp != 0;
		}

		public void SetMode(int theMode)
		{
			this.mMode = (byte)theMode;
		}

		public void SetRamp(int theRamp)
		{
			this.mRamp = (byte)theRamp;
		}

		public void SetOutRange(double theMin, double theMax)
		{
			this.mOutMin = theMin;
			this.mOutMax = theMax;
		}

		public void SetInRange(double theMin, double theMax)
		{
			this.mInMin = theMin;
			this.mInMax = theMax;
		}

		public double GetOutVal()
		{
			double outVal = this.GetOutVal(this.GetInVal());
			this.mCurOutVal = outVal;
			return outVal;
		}

		public double GetOutVal(double theInVal)
		{
			switch (this.mRamp)
			{
			case 0:
			case 1:
				if (this.mMode == 2)
				{
					if (theInVal - this.mInMin <= (this.mInMax - this.mInMin) / 2.0)
					{
						return this.mOutMin + (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * (this.mOutMax - this.mOutMin) * 2.0;
					}
					return this.mOutMin + (1.0 - (theInVal - this.mInMin) / (this.mInMax - this.mInMin)) * (this.mOutMax - this.mOutMin) * 2.0;
				}
				else
				{
					if (this.mInMin == this.mInMax)
					{
						return this.mOutMin;
					}
					return this.mOutMin + (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * (this.mOutMax - this.mOutMin);
				}
				break;
			case 2:
			{
				double num = (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * CurvedVal.PI / 2.0;
				if (this.mMode == 2)
				{
					num *= 2.0;
				}
				if (num > CurvedVal.PI / 2.0)
				{
					num = CurvedVal.PI - num;
				}
				return this.mOutMin + (1.0 - Math.Cos(num)) * (this.mOutMax - this.mOutMin);
			}
			case 3:
			{
				double num2 = (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * CurvedVal.PI / 2.0;
				if (this.mMode == 2)
				{
					num2 *= 2.0;
				}
				return this.mOutMin + Math.Sin(num2) * (this.mOutMax - this.mOutMin);
			}
			case 4:
			{
				double num3 = (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * CurvedVal.PI;
				if (this.mMode == 2)
				{
					num3 *= 2.0;
				}
				return this.mOutMin + (-Math.Cos(num3) + 1.0) / 2.0 * (this.mOutMax - this.mOutMin);
			}
			case 5:
			{
				double num4 = (theInVal - this.mInMin) / (this.mInMax - this.mInMin) * CurvedVal.PI;
				if (this.mMode == 2)
				{
					num4 *= 2.0;
				}
				if (num4 > CurvedVal.PI)
				{
					num4 = CurvedVal.PI * 2.0 - num4;
				}
				if (num4 < CurvedVal.PI / 2.0)
				{
					return this.mOutMin + Math.Sin(num4) / 2.0 * (this.mOutMax - this.mOutMin);
				}
				return this.mOutMin + (2.0 - Math.Sin(num4)) / 2.0 * (this.mOutMax - this.mOutMin);
			}
			case 6:
			{
				this.CheckCurveChange();
				if (this.mCurveCacheRecord == null)
				{
					return 0.0;
				}
				if (this.mInMax - this.mInMin == 0.0)
				{
					return 0.0;
				}
				float num5 = (float)Math.Min((theInVal - this.mInMin) / (this.mInMax - this.mInMin), 1.0);
				if (this.mMode == 2)
				{
					if (num5 > 0.5f)
					{
						num5 = (1f - num5) * 2f;
					}
					else
					{
						num5 *= 2f;
					}
				}
				if (this.mIsHermite)
				{
					double num6 = this.mOutMin + (double)this.mCurveCacheRecord.mHermiteCurve.Evaluate(num5) * (this.mOutMax - this.mOutMin);
					if (!this.mNoClip)
					{
						if (this.mOutMin < this.mOutMax)
						{
							num6 = Math.Min(Math.Max(num6, this.mOutMin), this.mOutMax);
						}
						else
						{
							num6 = Math.Max(Math.Min(num6, this.mOutMin), this.mOutMax);
						}
					}
					return num6;
				}
				float num7 = num5 * (float)(CurvedVal.CV_NUM_SPLINE_POINTS - 1);
				int num8 = (int)num7;
				if (num8 == CurvedVal.CV_NUM_SPLINE_POINTS - 1)
				{
					return this.mOutMin + (double)this.mCurveCacheRecord.mTable[num8] * (this.mOutMax - this.mOutMin);
				}
				float num9 = num7 - (float)num8;
				return this.mOutMin + (double)(this.mCurveCacheRecord.mTable[num8] * (1f - num9) + this.mCurveCacheRecord.mTable[num8 + 1] * num9) * (this.mOutMax - this.mOutMin);
			}
			default:
				return this.mOutMin;
			}
		}

		public double GetOutValDelta()
		{
			return this.GetOutVal() - this.mPrevOutVal;
		}

		public double GetOutFinalVal()
		{
			return this.GetOutVal(this.mInMax);
		}

		public double GetInVal()
		{
			double num = this.mInVal;
			if (this.mLinkedVal != null)
			{
				if (this.mLinkedVal.mOutputSync)
				{
					num = this.mLinkedVal.GetOutVal();
				}
				else
				{
					num = this.mLinkedVal.GetInVal();
				}
			}
			else if (this.mAutoInc)
			{
				if (this.mAppUpdateCountSrc != 0)
				{
					num = this.mInMin + (double)(this.mAppUpdateCountSrc - this.mInitAppUpdateCount) * this.mIncRate;
				}
				if (this.mMode == 1 || this.mMode == 2)
				{
					num = Math.IEEERemainder(num - this.mInMin, this.mInMax - this.mInMin) + this.mInMin;
				}
				else
				{
					num = Math.Min(num, this.mInMax);
				}
			}
			if (this.mMode != 2)
			{
				return num;
			}
			double num2 = (double)((float)((num - this.mInMin) / (this.mInMax - this.mInMin)));
			if (num2 > 0.5)
			{
				return this.mInMin + (1.0 - num2) * 2.0 * (this.mInMax - this.mInMin);
			}
			return this.mInMin + num2 * 2.0 * (this.mInMax - this.mInMin);
		}

		public bool SetInVal(double theVal)
		{
			return this.SetInVal(theVal, false);
		}

		public bool SetInVal(double theVal, bool theRealignAutoInc)
		{
			this.mPrevOutVal = this.GetOutVal();
			this.mTriggered = false;
			this.mPrevInVal = theVal;
			if (this.mAutoInc && theRealignAutoInc)
			{
				this.mInitAppUpdateCount -= (int)((theVal - this.mInVal) * 100.0);
			}
			this.mInVal = theVal;
			if (this.CheckClamping())
			{
				return true;
			}
			if (!this.mTriggered)
			{
				this.mTriggered = true;
				return false;
			}
			return this.mSingleTrigger;
		}

		public bool IncInVal(double theInc)
		{
			this.mPrevOutVal = this.GetOutVal();
			this.mPrevInVal = this.mInVal;
			this.mInVal += theInc;
			if (this.CheckClamping())
			{
				return true;
			}
			if (!this.mTriggered)
			{
				this.mTriggered = true;
				return false;
			}
			return this.mSingleTrigger;
		}

		public bool IncInVal()
		{
			return this.mIncRate != 0.0 && this.IncInVal(this.mIncRate);
		}

		public void Intercept(string theDataP, CurvedVal theInterceptCv, double theCheckInIncrPct, bool theStopAtLocalMin)
		{
			double theTargetOutVal = ((theInterceptCv == null) ? this : theInterceptCv);
			this.SetCurve(theDataP);
			this.SetInVal(this.FindClosestInToOutVal(theTargetOutVal, theCheckInIncrPct, 0.0, 1.0, theStopAtLocalMin), true);
		}

		public void Intercept(string theData, CurvedVal theInterceptCv, double theCheckInIncrPct)
		{
			this.Intercept(theData, theInterceptCv, theCheckInIncrPct, false);
		}

		public void Intercept(string theData, CurvedVal theInterceptCv)
		{
			this.Intercept(theData, theInterceptCv, 0.01, false);
		}

		public void Intercept(string theData)
		{
			this.Intercept(theData, null, 0.01, false);
		}

		public double FindClosestInToOutVal(double theTargetOutVal, double theCheckInIncrPct, double theCheckInRangeMinPct, double theCheckInRangeMaxPct)
		{
			return this.FindClosestInToOutVal(theTargetOutVal, theCheckInIncrPct, theCheckInRangeMinPct, theCheckInRangeMaxPct, false);
		}

		public double FindClosestInToOutVal(double theTargetOutVal, double theCheckInIncrPct, double theCheckInRangeMinPct)
		{
			return this.FindClosestInToOutVal(theTargetOutVal, theCheckInIncrPct, theCheckInRangeMinPct, 1.0, false);
		}

		public double FindClosestInToOutVal(double theTargetOutVal, double theCheckInIncrPct)
		{
			return this.FindClosestInToOutVal(theTargetOutVal, theCheckInIncrPct, 0.0, 1.0, false);
		}

		public double FindClosestInToOutVal(double theTargetOutVal)
		{
			return this.FindClosestInToOutVal(theTargetOutVal, 0.01, 0.0, 1.0, false);
		}

		public double FindClosestInToOutVal(double theTargetOutVal, double theCheckInIncrPct, double theCheckInRangeMinPct, double theCheckInRangeMaxPct, bool theStopAtLocalMin)
		{
			double num = this.mInMax - this.mInMin;
			double num2 = this.mInMin + num * theCheckInRangeMaxPct;
			double num3 = 0.0;
			double num4 = -1.0;
			for (double num5 = this.mInMin + num * theCheckInRangeMinPct; num5 <= num2; num5 += num * theCheckInIncrPct)
			{
				double num6 = Math.Abs(theTargetOutVal - this.GetOutVal(num5));
				if (num4 < 0.0 || num6 < num3)
				{
					num3 = num6;
					num4 = num5;
				}
				else if (theStopAtLocalMin)
				{
					return num4;
				}
			}
			return num4;
		}

		public double GetInValAtUpdate(int theUpdateCount)
		{
			return this.mInMin + (double)theUpdateCount * this.mIncRate;
		}

		public int GetLengthInUpdates()
		{
			if (this.mIncRate == 0.0)
			{
				return -1;
			}
			return (int)Math.Ceiling((this.mInMax - this.mInMin) / this.mIncRate);
		}

		public bool CheckInThreshold(double theInVal)
		{
			double inVal = this.mInVal;
			double num = this.mPrevInVal;
			if (this.mAutoInc)
			{
				inVal = this.GetInVal();
				num = inVal - this.mIncRate * 1.5;
			}
			return theInVal > num && theInVal <= inVal;
		}

		public bool CheckUpdatesFromEndThreshold(int theUpdateCount)
		{
			return this.CheckInThreshold(this.GetInValAtUpdate(this.GetLengthInUpdates() - theUpdateCount));
		}

		public bool HasBeenTriggered()
		{
			if (this.mAutoInc)
			{
				this.mTriggered = this.GetInVal() == this.mInMax;
			}
			return this.mTriggered;
		}

		public void ClearTrigger()
		{
			this.mTriggered = false;
		}

		public bool IsDoingCurve()
		{
			return this.GetInVal() != this.mInMax && this.mRamp != 0;
		}

		public static int DFLAG_NOCLIP = 1;

		public static int DFLAG_SINGLETRIGGER = 2;

		public static int DFLAG_OUTPUTSYNC = 4;

		public static int DFLAG_HERMITE = 8;

		public static int DFLAG_AUTOINC = 16;

		public static int CV_NUM_SPLINE_POINTS = 256;

		public static double PI = 3.141590118408203;

		public static Dictionary<string, CurvedVal.CurveCacheRecord> mCurveCacheMap = new Dictionary<string, CurvedVal.CurveCacheRecord>();

		public static CurvedVal gsFastCurveData = new CurvedVal();

		public double mIncRate;

		public double mOutMin;

		public double mOutMax;

		public string mDataP;

		public string mCurDataPStr;

		public int mInitAppUpdateCount;

		public CurvedVal mLinkedVal;

		public CurvedVal.CurveCacheRecord mCurveCacheRecord;

		public double mCurOutVal;

		public double mPrevOutVal;

		public double mInMin;

		public double mInMax;

		public byte mMode;

		public byte mRamp;

		public bool mNoClip;

		public bool mSingleTrigger;

		public bool mOutputSync;

		public bool mTriggered;

		public bool mIsHermite;

		public bool mAutoInc;

		public double mPrevInVal;

		public double mInVal;

		public enum Mode
		{
			MODE_CLAMP,
			MODE_REPEAT,
			MODE_PING_PONG
		}

		public enum Ramp
		{
			RAMP_NONE,
			RAMP_LINEAR,
			RAMP_SLOW_TO_FAST,
			RAMP_FAST_TO_SLOW,
			RAMP_SLOW_FAST_SLOW,
			RAMP_FAST_SLOW_FAST,
			RAMP_CURVEDATA
		}

		public class DataPoint
		{
			public float mX;

			public float mY;

			public float mAngleDeg;
		}

		public class CurveCacheRecord
		{
			public float[] mTable = new float[CurvedVal.CV_NUM_SPLINE_POINTS];

			public SexyMathHermite mHermiteCurve = new SexyMathHermite();

			public string mDataStr;
		}
	}
}
