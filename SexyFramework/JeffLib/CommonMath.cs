using System;
using Sexy.Misc;

namespace JeffLib
{
	public static class CommonMath
	{
		public static float AngleBetweenPoints(float p1x, float p1y, float p2x, float p2y)
		{
			return (float)Math.Atan2((double)(-(double)(p2y - p1y)), (double)(p2x - p1x));
		}

		public static float AngleBetweenPoints(SexyPoint p1, SexyPoint p2)
		{
			return CommonMath.AngleBetweenPoints((float)p1.mX, (float)p1.mY, (float)p2.mX, (float)p2.mY);
		}

		public static bool CircleCircleIntersection(float x0, float y0, float r0, float x1, float y1, float r1, ref float pt1_x, ref float pt1_y, ref float pt2_x, ref float pt2_y)
		{
			float num = x1 - x0;
			float num2 = y1 - y0;
			float num3 = (float)Math.Sqrt(Math.Pow((double)num, 2.0) + Math.Pow((double)num2, 2.0));
			if (num3 > r0 + r1)
			{
				return false;
			}
			if (num3 < Math.Abs(r0 - r1))
			{
				return false;
			}
			float num4 = (float)((double)(r0 * r0 - r1 * r1 + num3 * num3) / (2.0 * (double)num3));
			float num5 = x0 + num * num4 / num3;
			float num6 = y0 + num2 * num4 / num3;
			float num7 = (float)Math.Sqrt((double)(r0 * r0 - num4 * num4));
			float num8 = -num2 * (num7 / num3);
			float num9 = num * (num7 / num3);
			pt1_x = num5 + num8;
			pt2_x = num5 - num8;
			pt1_y = num6 + num9;
			pt2_y = num6 - num9;
			return true;
		}
	}
}
