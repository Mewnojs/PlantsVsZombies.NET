using System;

namespace Sexy.TodLib
{
	public/*internal*/ class FloatParameterTrackNode
	{
		public float mTime;

		public float mLowValue;

		public float mHighValue;

		public TodCurves mCurveType;

		public TodCurves mDistribution;

		public static int SIZE = 20;
	}
}
