using System;

namespace Sexy
{
	internal class SexyMath
	{
		public static float Fabs(float inX)
		{
			return Math.Abs(inX);
		}

		public static double Fabs(double inX)
		{
			return Math.Abs(inX);
		}

		public static float DegToRad(float inX)
		{
			return inX * 3.1415927f / 180f;
		}

		public static float RadToDeg(float inX)
		{
			return inX * 180f / 3.1415927f;
		}

		public static bool IsPowerOfTwo(uint inX)
		{
			return inX != 0U && (inX & inX - 1U) == 0U;
		}
	}
}
