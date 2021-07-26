using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	internal static class CGMaths
	{
		public static CGPoint CGPointAddScaled(CGPoint augend, CGPoint addend, float factor)
		{
			return new CGPoint
			{
				X = augend.X + addend.X * factor,
				Y = augend.Y + addend.Y * factor
			};
		}

		internal static CGPoint CGPointMultiply(CGPoint multiplicand, CGPoint multiplier)
		{
			return new CGPoint
			{
				X = multiplicand.X * multiplier.X,
				Y = multiplicand.Y * multiplier.Y
			};
		}

		internal static CGPoint CGPointSubtract(CGPoint minuend, CGPoint subtrahend)
		{
			return new CGPoint
			{
				X = minuend.X - subtrahend.X,
				Y = minuend.Y - subtrahend.Y
			};
		}

		internal static CGPoint CGPointSubtract(TPoint minuend, TPoint subtrahend)
		{
			return new CGPoint
			{
				X = (float)(minuend.x - subtrahend.x),
				Y = (float)(minuend.y - subtrahend.y)
			};
		}

		internal static float CGVectorNorm(CGPoint v)
		{
			return v.X * v.X + v.Y * v.Y;
		}

		internal static CGPoint CGPointMake(float x, float y)
		{
			return new CGPoint(x, y);
		}

		internal static void CGPointTranslate(ref CGPoint point, float tx, float ty)
		{
			point.X += tx;
			point.Y += ty;
		}

		internal static void CGPointTranslate(ref CGPoint point, int tx, int ty)
		{
			point.X += (float)tx;
			point.Y += (float)ty;
		}

		internal static void CGPointTranslate(ref Point point, int tx, int ty)
		{
			point.X += tx;
			point.Y += ty;
		}

		internal static void CGPointTranslate(ref Point point, float tx, float ty)
		{
			point.X += (int)tx;
			point.Y += (int)ty;
		}
	}
}
