using System;

namespace Sexy.Misc
{
	public class SexyMath
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

		public static bool ApproxEquals(float inL, float inR, float inTol)
		{
			return SexyMath.Fabs(inL - inR) <= inTol;
		}

		public static bool ApproxEquals(double inL, double inR, double inTol)
		{
			return SexyMath.Fabs(inL - inR) <= inTol;
		}

		public static float Lerp(float inA, float inB, float inAlpha)
		{
			return inA + (inB - inA) * inAlpha;
		}

		public static double Lerp(double inA, double inB, double inAlpha)
		{
			return inA + (inB - inA) * inAlpha;
		}

		public static bool IsPowerOfTwo(uint inX)
		{
			return inX != 0U && (inX & (inX - 1U)) == 0U;
		}

		public static float SinF(float value)
		{
			return (float)Math.Sin((double)value);
		}

		public static float CosF(float value)
		{
			return (float)Math.Cos((double)value);
		}
	}
}
