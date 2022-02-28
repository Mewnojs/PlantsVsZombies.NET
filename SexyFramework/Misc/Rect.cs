using System;
using Microsoft.Xna.Framework;

namespace Sexy.Misc
{
	public struct Rect
	{
		public Rect(int theX, int theY, int theWidth, int theHeight)
		{
			this.mX = theX;
			this.mY = theY;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
		}

		public Rect(Rect theTRect)
		{
			this.mX = theTRect.mX;
			this.mY = theTRect.mY;
			this.mWidth = theTRect.mWidth;
			this.mHeight = theTRect.mHeight;
		}

		public void SetValue(int theX, int theY, int theWidth, int theHeight)
		{
			this.mX = theX;
			this.mY = theY;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is Rect)
			{
				Rect rect = (Rect)obj;
				return rect.mX == this.mX && rect.mY == this.mY && rect.mWidth == this.mWidth && rect.mHeight == this.mHeight;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public void setValue(ref int x, ref int y, ref int width, ref int height)
		{
			this.mX = x;
			this.mY = y;
			this.mWidth = width;
			this.mHeight = height;
		}

		public void setValue(int x, int y, int width, int height)
		{
			this.mX = x;
			this.mY = y;
			this.mWidth = width;
			this.mHeight = height;
		}

		public bool Intersects(Rect theTRect)
		{
			return theTRect.mX + theTRect.mWidth > this.mX && theTRect.mY + theTRect.mHeight > this.mY && theTRect.mX < this.mX + this.mWidth && theTRect.mY < this.mY + this.mHeight;
		}

		public bool Intersects(int x, int y, int w, int h)
		{
			return x + w > this.mX && y + h > this.mY && x < this.mX + this.mWidth && y < this.mY + this.mHeight;
		}

		public Rect Intersection(Rect theTRect)
		{
			int num = Math.Max(this.mX, theTRect.mX);
			int num2 = Math.Min(this.mX + this.mWidth, theTRect.mX + theTRect.mWidth);
			int num3 = Math.Max(this.mY, theTRect.mY);
			int num4 = Math.Min(this.mY + this.mHeight, theTRect.mY + theTRect.mHeight);
			if (num2 - num < 0 || num4 - num3 < 0)
			{
				return new Rect(0, 0, 0, 0);
			}
			return new Rect(num, num3, num2 - num, num4 - num3);
		}

		public Rect Union(Rect theTRect)
		{
			int num = Math.Min(this.mX, theTRect.mX);
			int num2 = Math.Max(this.mX + this.mWidth, theTRect.mX + theTRect.mWidth);
			int num3 = Math.Min(this.mY, theTRect.mY);
			int num4 = Math.Max(this.mY + this.mHeight, theTRect.mY + theTRect.mHeight);
			return new Rect(num, num3, num2 - num, num4 - num3);
		}

		public bool Contains(int theX, int theY)
		{
			return theX >= this.mX && theX < this.mX + this.mWidth && theY >= this.mY && theY < this.mY + this.mHeight;
		}

		public bool Contains(SexyPoint thePoint)
		{
			return thePoint.mX >= this.mX && thePoint.mX < this.mX + this.mWidth && thePoint.mY >= this.mY && thePoint.mY < this.mY + this.mHeight;
		}

		public void Offset(int theX, int theY)
		{
			this.mX += theX;
			this.mY += theY;
		}

		public void Offset(Vector2 thePoint)
		{
			this.mX += (int)thePoint.X;
			this.mY += (int)thePoint.Y;
		}

		public Rect Inflate(int theX, int theY)
		{
			this.mX -= theX;
			this.mWidth += theX * 2;
			this.mY -= theY;
			this.mHeight += theY * 2;
			return this;
		}

		public void Scale(double theScaleX, double theScaleY)
		{
			this.mX = (int)((double)this.mX * theScaleX);
			this.mY = (int)((double)this.mY * theScaleY);
			this.mWidth = (int)((double)this.mWidth * theScaleX);
			this.mHeight = (int)((double)this.mHeight * theScaleY);
		}

		public void Scale(double theScaleX, double theScaleY, int theCenterX, int theCenterY)
		{
			this.Offset(-theCenterX, -theCenterY);
			this.Scale(theScaleX, theScaleY);
			this.Offset(theCenterX, theCenterY);
		}

		public static bool operator ==(Rect ImpliedObject, Rect theRect)
		{
			return ImpliedObject.mX == theRect.mX && ImpliedObject.mY == theRect.mY && ImpliedObject.mWidth == theRect.mWidth && ImpliedObject.mHeight == theRect.mHeight;
		}

		public static bool operator !=(Rect ImpliedObject, Rect theRect)
		{
			return !(ImpliedObject == theRect);
		}

		public static Rect ZERO_RECT = new Rect(0, 0, 0, 0);

		public static Rect INVALIDATE_RECT = new Rect(-1, -1, -1, -1);

		public int mX;

		public int mY;

		public int mWidth;

		public int mHeight;
	}
}
