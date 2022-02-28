using System;
using Microsoft.Xna.Framework;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public static class GlobalPIEffect
	{
		internal static IntPtr GetData(PIEffect theEffect, IntPtr thePtr, int theSize)
		{
			return thePtr;
		}

		public static bool IsIdentityMatrix(SexyMatrix3 theMatrix)
		{
			return theMatrix.m01 == 0f && theMatrix.m02 == 0f && theMatrix.m10 == 0f && theMatrix.m12 == 0f && theMatrix.m20 == 0f && theMatrix.m21 == 0f && theMatrix.m00 == 1f && theMatrix.m11 == 1f && theMatrix.m22 == 1f;
		}

		public static float GetMatrixScale(SexyMatrix3 theMatrix)
		{
			return 0f;
		}

		public static Vector2 TransformFPoint(SexyTransform2D theMatrix, Vector2 thePoint)
		{
			return Vector2.Transform(thePoint, theMatrix.mMatrix);
		}

		internal static float WrapFloat(float theNum, int theRepeat)
		{
			if (theRepeat == 1)
			{
				return theNum;
			}
			theNum *= (float)theRepeat;
			return theNum - (float)((int)theNum);
		}

		public static float DegToRad(float theDeg)
		{
			return theDeg * GlobalPIEffect.M_PI / 180f;
		}

		public static uint InterpColor(int theColor1, int theColor2, float thePct)
		{
			uint num = (uint)(thePct * 255f);
			num = ((num < 0U) ? 0U : ((num > 255U) ? 255U : num));
			int num2 = (int)(255U - num);
			long num3 = (long)((((ulong)((uint)(theColor1 & -16777216) >> 24) * (ulong)((long)num2) + (ulong)(((uint)(theColor2 & -16777216) >> 24) * num) << 16) & 0xffffffffff000000) | (((ulong)((uint)(theColor1 & 16711680) >> 16) * (ulong)((long)num2) + (ulong)((long)((theColor2 & 16711680) >> 16) * (long)((ulong)num)) << 8) & 16711680UL) | (ulong)(((long)(((theColor1 & 65280) >> 8) * num2) + (long)((theColor2 & 65280) >> 8) * (long)((ulong)num)) & 65280L) | (ulong)(((long)((theColor1 & 255) * num2) + (long)(theColor2 & 255) * (long)((ulong)num) >> 8) & 255L));
			return (uint)num3;
		}

		public static bool LineSegmentIntersects(Vector2 aPtA1, Vector2 aPtA2, Vector2 aPtB1, Vector2 aPtB2, ref float thePos, Vector2 theIntersectionPoint)
		{
			double num = (double)((aPtB2.X - aPtB1.Y) * (aPtA2.X - aPtA1.X) - (aPtB2.X - aPtB1.X) * (aPtA2.Y - aPtA1.Y));
			if (num == 0.0)
			{
				return false;
			}
			double num2 = (double)((aPtB2.X - aPtB1.X) * (aPtA1.Y - aPtB1.Y) - (aPtB2.Y - aPtB1.Y) * (aPtA1.X - aPtB1.X)) / num;
			if (num2 < 0.0 || num2 > 1.0)
			{
				return false;
			}
			double num3 = (double)((aPtA2.X - aPtA1.X) * (aPtA1.Y - aPtB1.Y) - (aPtA2.Y - aPtA1.Y) * (aPtA1.X - aPtB1.Y)) / num;
			if (num3 >= 0.0 && num3 <= 1.0)
			{
				if (thePos != 0f)
				{
					thePos = (float)num2;
				}
				theIntersectionPoint = aPtA1 + (aPtA2 - aPtA1) * (float)num2;
				return true;
			}
			return false;
		}

		internal static void GetBestStripSize(int theCount, int theCelWidth, int theCelHeight, ref int theNumCols, ref int theNumRows)
		{
			float num = 100f;
			theNumCols = theCount;
			theNumRows = 1;
			for (int i = 1; i <= theCount; i++)
			{
				int num2 = theCount / i;
				if (num2 * i == theCount)
				{
					float num3 = (float)(theCelWidth * num2) / (float)(theCelHeight * i);
					float num4 = Math.Max(num3, 1f / num3);
					if (num4 + 0.0001f < num)
					{
						theNumRows = i;
						theNumCols = num2;
						num = num4;
					}
				}
			}
		}

		public static float TIME_TO_X(float theTime, float aMinTime, float aMaxTime)
		{
			return (float)((double)((theTime - aMinTime) / (aMaxTime - aMinTime) * (float)(GlobalPIEffect.PI_QUANT_SIZE - 1)) + 0.5);
		}

		public static float M_PI = 3.14159f;

		public static int PI_BUFSIZE = 1024;

		public static int PI_QUANT_SIZE = 256;
	}
}
