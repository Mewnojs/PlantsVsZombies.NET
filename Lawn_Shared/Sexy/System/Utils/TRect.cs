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

		public TRect(int theX, int theY, int theWidth, int theHeight)
		{
			this.mRect = new Rectangle
			{
				X = theX,
				Y = theY,
				Width = theWidth,
				Height = theHeight
			};
		}

		public TRect(TRect theTRect)
		{
			this.mRect = theTRect.mRect;
		}

		public bool Intersects(TRect theTRect)
		{
			return this.mRect.Intersects(theTRect.mRect);
		}

		public TRect Intersection(TRect theTRect)
		{
			int num = Math.Max(this.mRect.X, theTRect.mRect.X);
			int num2 = Math.Min(this.mRect.X + this.mRect.Width, theTRect.mRect.X + theTRect.mRect.Width);
			int num3 = Math.Max(this.mRect.Y, theTRect.mRect.Y);
			int num4 = Math.Min(this.mRect.Y + this.mRect.Height, theTRect.mRect.Y + theTRect.mRect.Height);
			if (num2 - num < 0 || num4 - num3 < 0)
			{
				return new TRect(0, 0, 0, 0);
			}
			return new TRect(num, num3, num2 - num, num4 - num3);
		}

		public TRect Union(TRect theTRect)
		{
			int num = Math.Min(this.mRect.X, theTRect.mRect.X);
			int num2 = Math.Max(this.mRect.X + this.mRect.Width, theTRect.mRect.X + theTRect.mRect.Width);
			int num3 = Math.Min(this.mRect.Y, theTRect.mRect.Y);
			int num4 = Math.Max(this.mRect.Y + this.mRect.Height, theTRect.mRect.Y + theTRect.mRect.Height);
			return new TRect(num, num3, num2 - num, num4 - num3);
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

		public TRect Inflate(int theX, int theY)
		{
			this.mRect.Inflate(theX, theY);
			return this;
		}

		public bool IsEmpty
		{
			get
			{
				return this.mRect.IsEmpty;
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
			return this.mRect == trect.mRect;
		}

		public override int GetHashCode()
		{
			return this.mRect.GetHashCode();
		}

		public override string ToString()
		{
			return this.mRect.ToString();
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
}
