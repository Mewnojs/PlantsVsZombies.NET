using System;
using Sexy.GraphicsLib;

namespace Sexy
{
	public class FastCurve
	{
		protected void InitFromCurveData()
		{
			this.mTriggered = false;
			this.mOutputSync = CurvedVal.gsFastCurveData.mOutputSync;
			this.mSingleTrigger = CurvedVal.gsFastCurveData.mSingleTrigger;
			this.mOutMin = (float)CurvedVal.gsFastCurveData.mOutMin;
			this.mOutMax = (float)CurvedVal.gsFastCurveData.mOutMax;
			this.mInVal = (float)CurvedVal.gsFastCurveData.mInVal;
			this.mInMin = (float)CurvedVal.gsFastCurveData.mInMin;
			this.mInMax = (float)CurvedVal.gsFastCurveData.mInMax;
			this.mIncRate = (float)CurvedVal.gsFastCurveData.mIncRate;
		}

		public FastCurve()
		{
			this.mOutMin = 0f;
			this.mOutMax = 1f;
			this.mInMin = 0f;
			this.mInMax = 1f;
			this.mTriggered = false;
			this.mIncRate = 0f;
			this.mInVal = 0f;
			this.mSingleTrigger = false;
			this.mOutputSync = false;
		}

		public FastCurve(string theData)
			: this(theData, null)
		{
		}

		public FastCurve(string theData, CurvedVal theLinkedVal)
		{
			CurvedVal.gsFastCurveData.SetCurve(theData);
			this.InitFromCurveData();
		}

		public void SetCurve(string theDataP)
		{
			this.SetCurve(theDataP, null);
		}

		public void SetCurve(string theDataP, CurvedVal theLinkedVal)
		{
			CurvedVal.gsFastCurveData.SetCurve(theDataP);
			this.InitFromCurveData();
		}

		public void SetConstant(float theValue)
		{
			this.mInVal = 0f;
			this.mTriggered = false;
			this.mInMin = (this.mInMax = 0f);
			this.mOutMax = theValue;
			this.mOutMin = theValue;
		}

		public float GetOutVal()
		{
			return this.GetOutVal(this.mInVal);
		}

		public float GetOutVal(float theInVal)
		{
			return this.mOutMax;
		}

		public float GetOutFinalVal()
		{
			return this.GetOutVal(this.mInMax);
		}

		public void SetOutRange(float theMin, float theMax)
		{
			this.mOutMin = theMin;
			this.mOutMax = theMax;
		}

		public void SetInRange(float theMin, float theMax)
		{
			this.mInMin = theMin;
			this.mInMax = theMax;
		}

		public float GetInVal()
		{
			return this.mInVal;
		}

		public bool SetInVal(float theVal)
		{
			return this.SetInVal(theVal, false);
		}

		public bool SetInVal(float theVal, bool theRealignAutoInc)
		{
			this.mInVal = theVal;
			return false;
		}

		public bool IncInVal(float theInc)
		{
			this.mInVal += theInc;
			bool flag = false;
			if (this.mInVal > this.mInMax)
			{
				this.mInVal = this.mInMax;
				flag = true;
			}
			else if (this.mInVal < this.mInMin)
			{
				this.mInVal = this.mInMin;
				flag = true;
			}
			if (!flag)
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
			return this.IncInVal(this.mIncRate);
		}

		public bool HasBeenTriggered()
		{
			return this.mTriggered;
		}

		public void ClearTrigger()
		{
			this.mTriggered = false;
		}

		public static implicit operator float(FastCurve ImpliedObject)
		{
			return ImpliedObject.GetOutVal();
		}

		public static implicit operator SexyColor(FastCurve ImpliedObject)
		{
			return new SexyColor(255, 255, 255, (int)(255f * ImpliedObject.GetOutVal()));
		}

		public float mOutMin;

		public float mOutMax;

		public float mInMin;

		public float mInMax;

		public float mIncRate;

		public float mInVal;

		public bool mTriggered;

		public bool mSingleTrigger;

		public bool mOutputSync;
	}
}
