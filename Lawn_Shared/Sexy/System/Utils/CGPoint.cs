using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct CGPoint
	{
		public CGPoint(float x, float y)
		{
			this.mVector = new Vector2(x, y);
		}

		public float X
		{
			get
			{
				return this.mVector.X;
			}
			set
			{
				this.mVector.X = value;
			}
		}

		public float x
		{
			get
			{
				return this.mVector.X;
			}
			set
			{
				this.mVector.X = value;
			}
		}

		public float mX
		{
			get
			{
				return this.mVector.X;
			}
			set
			{
				this.mVector.X = value;
			}
		}

		public float Y
		{
			get
			{
				return this.mVector.Y;
			}
			set
			{
				this.mVector.Y = value;
			}
		}

		public float y
		{
			get
			{
				return this.mVector.Y;
			}
			set
			{
				this.mVector.Y = value;
			}
		}

		public float mY
		{
			get
			{
				return this.mVector.Y;
			}
			set
			{
				this.mVector.Y = value;
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
			return this.mVector.ToString();
		}

		private Vector2 mVector;
	}
}
