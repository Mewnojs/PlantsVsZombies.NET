using System;

namespace Sexy.TodLib
{
	internal static class Definition
	{
		public static float FloatTrackEvaluate(ref FloatParameterTrack theTrack, float theTimeValue, float theInterp)
		{
			if (theTrack.mCountNodes == 0)
			{
				return 0f;
			}
			FloatParameterTrackNode floatParameterTrackNode = theTrack.mNodes[0];
			if (theTimeValue < floatParameterTrackNode.mTime)
			{
				return TodCommon.TodCurveEvaluate(theInterp, floatParameterTrackNode.mLowValue, floatParameterTrackNode.mHighValue, floatParameterTrackNode.mDistribution);
			}
			for (int i = 1; i < theTrack.mCountNodes; i++)
			{
				FloatParameterTrackNode floatParameterTrackNode2 = theTrack.mNodes[i];
				if (theTimeValue <= floatParameterTrackNode2.mTime)
				{
					FloatParameterTrackNode floatParameterTrackNode3 = theTrack.mNodes[i - 1];
					float theTime = (theTimeValue - floatParameterTrackNode3.mTime) / (floatParameterTrackNode2.mTime - floatParameterTrackNode3.mTime);
					float thePositionStart = TodCommon.TodCurveEvaluate(theInterp, floatParameterTrackNode3.mLowValue, floatParameterTrackNode3.mHighValue, floatParameterTrackNode3.mDistribution);
					float thePositionEnd = TodCommon.TodCurveEvaluate(theInterp, floatParameterTrackNode2.mLowValue, floatParameterTrackNode2.mHighValue, floatParameterTrackNode2.mDistribution);
					return TodCommon.TodCurveEvaluate(theTime, thePositionStart, thePositionEnd, floatParameterTrackNode3.mCurveType);
				}
			}
			FloatParameterTrackNode floatParameterTrackNode4 = theTrack.mNodes[theTrack.mCountNodes - 1];
			return TodCommon.TodCurveEvaluate(theInterp, floatParameterTrackNode4.mLowValue, floatParameterTrackNode4.mHighValue, floatParameterTrackNode4.mDistribution);
		}

		public static void FloatTrackSetDefault(ref FloatParameterTrack theTrack, float theValue)
		{
			if (theTrack.mNodes == null && theValue != 0f)
			{
				theTrack.mCountNodes = 1;
				theTrack.mNodes = new FloatParameterTrackNode[1];
				theTrack.mNodes[0] = new FloatParameterTrackNode();
				theTrack.mNodes[0].mTime = 0f;
				theTrack.mNodes[0].mLowValue = theValue;
				theTrack.mNodes[0].mHighValue = theValue;
				theTrack.mNodes[0].mCurveType = TodCurves.Constant;
				theTrack.mNodes[0].mDistribution = TodCurves.Linear;
			}
		}

		public static bool FloatTrackIsSet(ref FloatParameterTrack theTrack)
		{
			return theTrack.mCountNodes != 0 && theTrack.mNodes[0].mCurveType != TodCurves.Constant;
		}

		public static bool FloatTrackIsConstantZero(ref FloatParameterTrack theTrack)
		{
			if (theTrack.mCountNodes == 0)
			{
				return true;
			}
			if (theTrack.mCountNodes != 1)
			{
				return false;
			}
			FloatParameterTrackNode floatParameterTrackNode = theTrack.mNodes[0];
			return floatParameterTrackNode.mLowValue == 0f && floatParameterTrackNode.mHighValue == 0f;
		}

		public static bool IsPowerOf2(int n)
		{
			return n >= 1 && (n == 1 || (n & n - 1) == 0);
		}
	}
}
