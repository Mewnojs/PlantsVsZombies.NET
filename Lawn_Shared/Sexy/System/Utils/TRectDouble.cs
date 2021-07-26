using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct TRectDouble
	{
		public Rectangle Rect
		{
			get
			{
				return this.mRect;
			}
			set
			{
				this.mRect = value;
			}
		}

		public int mX
		{
			get
			{
				return this.mRect.X;
			}
			set
			{
				this.mRect.X = value;
			}
		}

		public int mY
		{
			get
			{
				return this.mRect.Y;
			}
			set
			{
				this.mRect.Y = value;
			}
		}

		public int mWidth
		{
			get
			{
				return this.mRect.Width;
			}
			set
			{
				this.mRect.Width = value;
			}
		}

		public int mHeight
		{
			get
			{
				return this.mRect.Height;
			}
			set
			{
				this.mRect.Height = value;
			}
		}

		public TRectDouble(int theX, int theY, int theWidth, int theHeight)
		{
			this.mRect = new Rectangle(theX, theY, theWidth, theHeight);
		}

		public TRectDouble(TRectDouble theTRect)
		{
			this.mRect = theTRect.mRect;
		}

		public bool Intersects(TRectDouble theTRect)
		{
			return this.mRect.Intersects(theTRect.mRect);
		}

		public TRectDouble Intersection(TRectDouble theTRect)
		{
			int num = Math.Max(this.mRect.X, theTRect.mRect.X);
			int num2 = Math.Min(this.mRect.X + this.mRect.Width, theTRect.mRect.X + theTRect.mRect.Width);
			int num3 = Math.Max(this.mRect.Y, theTRect.mRect.Y);
			int num4 = Math.Min(this.mRect.Y + this.mRect.Height, theTRect.mRect.Y + theTRect.mRect.Height);
			if (num2 - num < 0 || num4 - num3 < 0)
			{
				return new TRectDouble(0, 0, 0, 0);
			}
			return new TRectDouble(num, num3, num2 - num, num4 - num3);
		}

		public TRectDouble Union(TRectDouble theTRect)
		{
			int num = Math.Min(this.mRect.X, theTRect.mRect.X);
			int num2 = Math.Max(this.mRect.X + this.mRect.Width, theTRect.mRect.X + theTRect.mRect.Width);
			int num3 = Math.Min(this.mRect.Y, theTRect.mRect.Y);
			int num4 = Math.Max(this.mRect.Y + this.mRect.Height, theTRect.mRect.Y + theTRect.mRect.Height);
			return new TRectDouble(num, num3, num2 - num, num4 - num3);
		}

		public bool Contains(int theX, int theY)
		{
			return this.mRect.Contains(theX, theY);
		}

		public bool Contains(TPoint thePoint)
		{
			return this.mRect.Contains(thePoint.Point);
		}

		public void Offset(int theX, int theY)
		{
			this.mRect.Offset(theX, theY);
		}

		public void Offset(TPoint thePoint)
		{
			this.mRect.Offset(thePoint.Point);
		}

		public TRectDouble Inflate(int theX, int theY)
		{
			this.mRect.Inflate(theX, theY);
			return this;
		}

		public static bool operator ==(TRectDouble a, TRectDouble b)
		{
			return object.ReferenceEquals(a, b) || object.ReferenceEquals(a.mRect, b.mRect) || (a != null && b != null && a.mRect.Equals(b));
		}

		public override bool Equals(object obj)
		{
			if (!(obj is TRectDouble))
			{
				return false;
			}
			TRectDouble trectDouble = (TRectDouble)obj;
			return this.mRect == trectDouble.mRect;
		}

		public override int GetHashCode()
		{
			return this.mRect.GetHashCode();
		}

		public override string ToString()
		{
			return this.mRect.ToString();
		}

		public static bool operator !=(TRectDouble a, TRectDouble b)
		{
			return !(a == b);
		}

		private Rectangle mRect;
	}
}
