using System;
using Microsoft.Xna.Framework;

namespace Sexy
{
	public struct TRect : IEquatable<TRect>
	{
		public Rectangle Rect
		{
			get
			{
				return mRect;
			}
			set
			{
				mRect = value;
			}
		}

		public int mX
		{
			get
			{
				return mRect.X;
			}
			set
			{
				mRect.X = value;
			}
		}

		public int mY
		{
			get
			{
				return mRect.Y;
			}
			set
			{
				mRect.Y = value;
			}
		}

		public int mWidth
		{
			get
			{
				return mRect.Width;
			}
			set
			{
				mRect.Width = value;
			}
		}

		public int mHeight
		{
			get
			{
				return mRect.Height;
			}
			set
			{
				mRect.Height = value;
			}
		}

		public TRect(int theX, int theY, int theWidth, int theHeight)
		{
			mRect = new Rectangle
			{
				X = theX,
				Y = theY,
				Width = theWidth,
				Height = theHeight
			};
		}

		public TRect(TRect theTRect)
		{
			mRect = theTRect.mRect;
		}

		public bool Intersects(TRect theTRect)
		{
			return mRect.Intersects(theTRect.mRect);
		}

		public TRect Intersection(TRect theTRect)
		{
			int num = Math.Max(mRect.X, theTRect.mRect.X);
			int num2 = Math.Min(mRect.X + mRect.Width, theTRect.mRect.X + theTRect.mRect.Width);
			int num3 = Math.Max(mRect.Y, theTRect.mRect.Y);
			int num4 = Math.Min(mRect.Y + mRect.Height, theTRect.mRect.Y + theTRect.mRect.Height);
			if (num2 - num < 0 || num4 - num3 < 0)
			{
				return new TRect(0, 0, 0, 0);
			}
			return new TRect(num, num3, num2 - num, num4 - num3);
		}

		public TRect Union(TRect theTRect)
		{
			int num = Math.Min(mRect.X, theTRect.mRect.X);
			int num2 = Math.Max(mRect.X + mRect.Width, theTRect.mRect.X + theTRect.mRect.Width);
			int num3 = Math.Min(mRect.Y, theTRect.mRect.Y);
			int num4 = Math.Max(mRect.Y + mRect.Height, theTRect.mRect.Y + theTRect.mRect.Height);
			return new TRect(num, num3, num2 - num, num4 - num3);
		}

		public bool Contains(int theX, int theY)
		{
			return mRect.Contains(theX, theY);
		}

		public bool Contains(TPoint thePoint)
		{
			return mRect.Contains(thePoint.Point);
		}

		public void Offset(int theX, int theY)
		{
			mRect.Offset(theX, theY);
		}

		public void Offset(TPoint thePoint)
		{
			mRect.Offset(thePoint.Point);
		}

		public TRect Inflate(int theX, int theY)
		{
			mRect.Inflate(theX, theY);
			return this;
		}

		public bool IsEmpty
		{
			get
			{
				return mRect.IsEmpty;
			}
		}

		public static bool operator ==(TRect a, TRect b)
		{
			return a.mRect.Equals(b.mRect);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is TRect))
			{
				return false;
			}
			TRect trect = (TRect)obj;
			return mRect == trect.mRect;
		}

		public override int GetHashCode()
		{
			return mRect.GetHashCode();
		}

		public override string ToString()
		{
			return mRect.ToString();
		}

		public static bool operator !=(TRect a, TRect b)
		{
			return !(a == b);
		}

		public static explicit operator TRect(Rectangle rect)
		{
			return new TRect
			{
				Rect = rect
			};
		}

		public static implicit operator Rectangle(TRect aRect)
		{
			return aRect.Rect;
		}

		bool IEquatable<TRect>.Equals(TRect other)
		{
			return this == other;
		}

		private Rectangle mRect;

		public static TRect Empty = new TRect(0, 0, 0, 0);
	}

	public struct TRectDouble
	{
		public Rectangle Rect
		{
			get
			{
				return mRect;
			}
			set
			{
				mRect = value;
			}
		}

		public int mX
		{
			get
			{
				return mRect.X;
			}
			set
			{
				mRect.X = value;
			}
		}

		public int mY
		{
			get
			{
				return mRect.Y;
			}
			set
			{
				mRect.Y = value;
			}
		}

		public int mWidth
		{
			get
			{
				return mRect.Width;
			}
			set
			{
				mRect.Width = value;
			}
		}

		public int mHeight
		{
			get
			{
				return mRect.Height;
			}
			set
			{
				mRect.Height = value;
			}
		}

		public TRectDouble(int theX, int theY, int theWidth, int theHeight)
		{
			mRect = new Rectangle(theX, theY, theWidth, theHeight);
		}

		public TRectDouble(TRectDouble theTRect)
		{
			mRect = theTRect.mRect;
		}

		public bool Intersects(TRectDouble theTRect)
		{
			return mRect.Intersects(theTRect.mRect);
		}

		public TRectDouble Intersection(TRectDouble theTRect)
		{
			int num = Math.Max(mRect.X, theTRect.mRect.X);
			int num2 = Math.Min(mRect.X + mRect.Width, theTRect.mRect.X + theTRect.mRect.Width);
			int num3 = Math.Max(mRect.Y, theTRect.mRect.Y);
			int num4 = Math.Min(mRect.Y + mRect.Height, theTRect.mRect.Y + theTRect.mRect.Height);
			if (num2 - num < 0 || num4 - num3 < 0)
			{
				return new TRectDouble(0, 0, 0, 0);
			}
			return new TRectDouble(num, num3, num2 - num, num4 - num3);
		}

		public TRectDouble Union(TRectDouble theTRect)
		{
			int num = Math.Min(mRect.X, theTRect.mRect.X);
			int num2 = Math.Max(mRect.X + mRect.Width, theTRect.mRect.X + theTRect.mRect.Width);
			int num3 = Math.Min(mRect.Y, theTRect.mRect.Y);
			int num4 = Math.Max(mRect.Y + mRect.Height, theTRect.mRect.Y + theTRect.mRect.Height);
			return new TRectDouble(num, num3, num2 - num, num4 - num3);
		}

		public bool Contains(int theX, int theY)
		{
			return mRect.Contains(theX, theY);
		}

		public bool Contains(TPoint thePoint)
		{
			return mRect.Contains(thePoint.Point);
		}

		public void Offset(int theX, int theY)
		{
			mRect.Offset(theX, theY);
		}

		public void Offset(TPoint thePoint)
		{
			mRect.Offset(thePoint.Point);
		}

		public TRectDouble Inflate(int theX, int theY)
		{
			mRect.Inflate(theX, theY);
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
			return mRect == trectDouble.mRect;
		}

		public override int GetHashCode()
		{
			return mRect.GetHashCode();
		}

		public override string ToString()
		{
			return mRect.ToString();
		}

		public static bool operator !=(TRectDouble a, TRectDouble b)
		{
			return !(a == b);
		}

		private Rectangle mRect;
	}
}
