using System;
using Microsoft.Xna.Framework;

namespace Sexy.Misc
{
	public struct FRect
	{
		public FRect(float theX, float theY, float theWidth, float theHeight)
		{
			this.mX = theX;
			this.mY = theY;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
		}

		public FRect(Rect theTRect)
		{
			this.mX = (float)theTRect.mX;
			this.mY = (float)theTRect.mY;
			this.mWidth = (float)theTRect.mWidth;
			this.mHeight = (float)theTRect.mHeight;
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is FRect)
			{
				FRect frect = (FRect)obj;
				return frect.mX == this.mX && frect.mY == this.mY && frect.mWidth == this.mWidth && frect.mHeight == this.mHeight;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public bool Intersects(FRect theTRect)
		{
			return theTRect.mX + theTRect.mWidth > this.mX && theTRect.mY + theTRect.mHeight > this.mY && theTRect.mX < this.mX + this.mWidth && theTRect.mY < this.mY + this.mHeight;
		}

		public FRect Intersection(FRect theTRect)
		{
			float num = Math.Max(this.mX, theTRect.mX);
			float num2 = Math.Min(this.mX + this.mWidth, theTRect.mX + theTRect.mWidth);
			float num3 = Math.Max(this.mY, theTRect.mY);
			float num4 = Math.Min(this.mY + this.mHeight, theTRect.mY + theTRect.mHeight);
			if (num2 - num < 0f || num4 - num3 < 0f)
			{
				return new FRect(0f, 0f, 0f, 0f);
			}
			return new FRect(num, num3, num2 - num, num4 - num3);
		}

		public FRect Union(FRect theTRect)
		{
			float num = Math.Min(this.mX, theTRect.mX);
			float num2 = Math.Max(this.mX + this.mWidth, theTRect.mX + theTRect.mWidth);
			float num3 = Math.Min(this.mY, theTRect.mY);
			float num4 = Math.Max(this.mY + this.mHeight, theTRect.mY + theTRect.mHeight);
			return new FRect(num, num3, num2 - num, num4 - num3);
		}

		public bool Contains(float theX, float theY)
		{
			return theX >= this.mX && theX < this.mX + this.mWidth && theY >= this.mY && theY < this.mY + this.mHeight;
		}

		public bool Contains(Vector2 thePoint)
		{
			return thePoint.X >= this.mX && thePoint.X < this.mX + this.mWidth && thePoint.Y >= this.mY && thePoint.Y < this.mY + this.mHeight;
		}

		public void Offset(float theX, float theY)
		{
			this.mX += theX;
			this.mY += theY;
		}

		public void Offset(Vector2 thePoint)
		{
			this.mX += thePoint.X;
			this.mY += thePoint.Y;
		}

		public FRect Inflate(float theX, float theY)
		{
			this.mX -= theX;
			this.mWidth += theX * 2f;
			this.mY -= theY;
			this.mHeight += theY * 2f;
			return this;
		}

		public void Scale(double theScaleX, double theScaleY)
		{
			this.mX = (float)((double)this.mX * theScaleX);
			this.mY = (float)((double)this.mY * theScaleY);
			this.mWidth = (float)((double)this.mWidth * theScaleX);
			this.mHeight = (float)((double)this.mHeight * theScaleY);
		}

		public void Scale(double theScaleX, double theScaleY, float theCenterX, float theCenterY)
		{
			this.Offset(-theCenterX, -theCenterY);
			this.Scale(theScaleX, theScaleY);
			this.Offset(theCenterX, theCenterY);
		}

		public static bool operator ==(FRect ImpliedObject, FRect theRect)
		{
			if (ImpliedObject == null)
			{
				return theRect == null;
			}
			return ImpliedObject.Equals(theRect);
		}

		public static bool operator !=(FRect ImpliedObject, FRect theRect)
		{
			return !(ImpliedObject == theRect);
		}

		public float mX;

		public float mY;

		public float mWidth;

		public float mHeight;

		public static FRect ZeroRect = new FRect(0f, 0f, 0f, 0f);
	}
}
