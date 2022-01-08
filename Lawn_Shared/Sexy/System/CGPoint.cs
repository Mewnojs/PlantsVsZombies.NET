using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct CGPoint
	{
		public CGPoint(float x, float y)
		{
			mVector = new Vector2(x, y);
		}

		public float X
		{
			get
			{
				return mVector.X;
			}
			set
			{
				mVector.X = value;
			}
		}

		public float x
		{
			get
			{
				return mVector.X;
			}
			set
			{
				mVector.X = value;
			}
		}

		public float mX
		{
			get
			{
				return mVector.X;
			}
			set
			{
				mVector.X = value;
			}
		}

		public float Y
		{
			get
			{
				return mVector.Y;
			}
			set
			{
				mVector.Y = value;
			}
		}

		public float y
		{
			get
			{
				return mVector.Y;
			}
			set
			{
				mVector.Y = value;
			}
		}

		public float mY
		{
			get
			{
				return mVector.Y;
			}
			set
			{
				mVector.Y = value;
			}
		}

		public static implicit operator TPoint(CGPoint a)
		{
			return new TPoint((int)a.X, (int)a.Y);
		}

		public static CGPoint operator +(CGPoint a, CGPoint b)
		{
			return new CGPoint(a.X + b.X, a.Y + b.Y);
		}

		public static CGPoint operator -(CGPoint a, CGPoint b)
		{
			return new CGPoint(a.X - b.X, a.Y - b.Y);
		}

		public override string ToString()
		{
			return mVector.ToString();
		}

		private Vector2 mVector;
	}
}
