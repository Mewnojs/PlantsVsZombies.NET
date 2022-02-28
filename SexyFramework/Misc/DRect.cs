using System;
using Microsoft.Xna.Framework;

namespace Sexy.Misc
{
	public class DRect
	{
		public DRect(double theX, double theY, double theWidth, double theHeight)
		{
			this.mX = theX;
			this.mY = theY;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
		}

		public DRect(DRect theTRect)
		{
			this.mX = theTRect.mX;
			this.mY = theTRect.mY;
			this.mWidth = theTRect.mWidth;
			this.mHeight = theTRect.mHeight;
		}

		public DRect()
		{
			this.mX = 0.0;
			this.mY = 0.0;
			this.mWidth = 0.0;
			this.mHeight = 0.0;
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is DRect)
			{
				DRect drect = (DRect)obj;
				return drect.mX == this.mX && drect.mY == this.mY && drect.mWidth == this.mWidth && drect.mHeight == this.mHeight;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public bool Intersects(DRect theTRect)
		{
			return theTRect.mX + theTRect.mWidth > this.mX && theTRect.mY + theTRect.mHeight > this.mY && theTRect.mX < this.mX + this.mWidth && theTRect.mY < this.mY + this.mHeight;
		}

		public DRect Intersection(DRect theTRect)
		{
			double num = Math.Max(this.mX, theTRect.mX);
			double num2 = Math.Min(this.mX + this.mWidth, theTRect.mX + theTRect.mWidth);
			double num3 = Math.Max(this.mY, theTRect.mY);
			double num4 = Math.Min(this.mY + this.mHeight, theTRect.mY + theTRect.mHeight);
			if (num2 - num < 0.0 || num4 - num3 < 0.0)
			{
				return new DRect(0.0, 0.0, 0.0, 0.0);
			}
			return new DRect(num, num3, num2 - num, num4 - num3);
		}

		public DRect Union(DRect theTRect)
		{
			double num = Math.Min(this.mX, theTRect.mX);
			double num2 = Math.Max(this.mX + this.mWidth, theTRect.mX + theTRect.mWidth);
			double num3 = Math.Min(this.mY, theTRect.mY);
			double num4 = Math.Max(this.mY + this.mHeight, theTRect.mY + theTRect.mHeight);
			return new DRect(num, num3, num2 - num, num4 - num3);
		}

		public bool Contains(double theX, double theY)
		{
			return theX >= this.mX && theX < this.mX + this.mWidth && theY >= this.mY && theY < this.mY + this.mHeight;
		}

		public bool Contains(Vector2 thePoint)
		{
			return (double)thePoint.X >= this.mX && (double)thePoint.X < this.mX + this.mWidth && (double)thePoint.Y >= this.mY && (double)thePoint.Y < this.mY + this.mHeight;
		}

		public void Offset(double theX, double theY)
		{
			this.mX += theX;
			this.mY += theY;
		}

		public void Offset(Vector2 thePoint)
		{
			this.mX += (double)thePoint.X;
			this.mY += (double)thePoint.Y;
		}

		public DRect Inflate(double theX, double theY)
		{
			this.mX -= theX;
			this.mWidth += theX * 2.0;
			this.mY -= theY;
			this.mHeight += theY * 2.0;
			return this;
		}

		public void Scale(double theScaleX, double theScaleY)
		{
			this.mX *= theScaleX;
			this.mY *= theScaleY;
			this.mWidth *= theScaleX;
			this.mHeight *= theScaleY;
		}

		public void Scale(double theScaleX, double theScaleY, double theCenterX, double theCenterY)
		{
			this.Offset(-theCenterX, -theCenterY);
			this.Scale(theScaleX, theScaleY);
			this.Offset(theCenterX, theCenterY);
		}

		public static bool operator ==(DRect ImpliedObject, DRect theRect)
		{
			if (ImpliedObject == null)
			{
				return theRect == null;
			}
			return ImpliedObject.Equals(theRect);
		}

		public static bool operator !=(DRect ImpliedObject, DRect theRect)
		{
			return !(ImpliedObject == theRect);
		}

		public double mX;

		public double mY;

		public double mWidth;

		public double mHeight;
	}
}
