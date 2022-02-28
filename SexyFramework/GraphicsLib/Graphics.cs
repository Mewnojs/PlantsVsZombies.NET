using System;
using System.Collections.Generic;
using System.Text;
using Sexy.Drivers.Graphics;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class Graphics : GraphicsState
	{
		protected static bool PFCompareInd(IntPtr u, IntPtr v)
		{
			return false;
		}

		protected static bool PFCompareActive(IntPtr u, IntPtr v)
		{
			return false;
		}

		protected void PFDelete(int i)
		{
		}

		protected void PFInsert(int i, int y)
		{
		}

		protected void DrawImageTransformHelper(Image theImage, Transform theTransform, Rect theSrcRect, float x, float y, bool useFloat)
		{
			if (theTransform.mComplex || (this.Get3D() != null && useFloat))
			{
				this.DrawImageMatrix(theImage, theTransform.GetMatrix(), theSrcRect, x, y);
				return;
			}
			float num = (float)theSrcRect.mWidth / 2f;
			float num2 = (float)theSrcRect.mHeight / 2f;
			if (theTransform.mHaveRot)
			{
				float num3 = num - theTransform.mTransX1;
				float num4 = num2 - theTransform.mTransY1;
				x = x + theTransform.mTransX2 - num3 + 0.5f;
				y = y + theTransform.mTransY2 - num4 + 0.5f;
				if (useFloat)
				{
					this.DrawImageRotatedF(theImage, x, y, (double)theTransform.mRot, num3, num4, theSrcRect);
					return;
				}
				this.DrawImageRotated(theImage, (int)x, (int)y, (double)theTransform.mRot, (int)num3, (int)num4, theSrcRect);
				return;
			}
			else
			{
				if (theTransform.mHaveScale)
				{
					bool mirror = false;
					if (theTransform.mScaleX == -1f)
					{
						if (theTransform.mScaleY == 1f)
						{
							x = x + theTransform.mTransX1 + theTransform.mTransX2 - num + 0.5f;
							y = y + theTransform.mTransY1 + theTransform.mTransY2 - num2 + 0.5f;
							this.DrawImageMirror(theImage, (int)x, (int)y, theSrcRect);
							return;
						}
						mirror = true;
					}
					float num5 = num * theTransform.mScaleX;
					float num6 = num2 * theTransform.mScaleY;
					x = x + theTransform.mTransX2 - num5;
					y = y + theTransform.mTransY2 - num6;
					this.mDestRect.mX = (int)x;
					this.mDestRect.mY = (int)y;
					this.mDestRect.mWidth = (int)(num5 * 2f);
					this.mDestRect.mHeight = (int)(num6 * 2f);
					this.DrawImageMirror(theImage, this.mDestRect, theSrcRect, mirror);
					return;
				}
				x = x + theTransform.mTransX1 + theTransform.mTransX2 - num + 0.5f;
				y = y + theTransform.mTransY1 + theTransform.mTransY2 - num2 + 0.5f;
				if (useFloat)
				{
					this.DrawImageF(theImage, x, y, theSrcRect);
					return;
				}
				this.DrawImage(theImage, (int)x, (int)y, theSrcRect);
				return;
			}
		}

		protected void InitRenderInfo(Graphics theSourceGraphics)
		{
			this.mGraphics3D = null;
			this.mIs3D = false;
			RenderDevice3D renderDevice3D = GlobalMembers.gSexyAppBase.mGraphicsDriver.GetRenderDevice3D();
			if (renderDevice3D != null)
			{
				HRenderContext hrenderContext;
				if (theSourceGraphics != null)
				{
					hrenderContext = renderDevice3D.CreateContext(this.mDestImage, theSourceGraphics.mRenderContext);
				}
				else
				{
					hrenderContext = renderDevice3D.CreateContext(this.mDestImage);
				}
				this.mRenderDevice = renderDevice3D;
				this.mRenderContext = hrenderContext;
				this.mGraphics3D = new Graphics3D(this, renderDevice3D, this.mRenderContext);
				this.mIs3D = true;
				return;
			}
			if (!this.mRenderContext.IsValid())
			{
				RenderDevice renderDevice = GlobalMembers.gSexyAppBase.mGraphicsDriver.GetRenderDevice();
				if (renderDevice != null)
				{
					HRenderContext hrenderContext2 = new HRenderContext();
					if (theSourceGraphics != null)
					{
						hrenderContext2 = renderDevice.CreateContext(this.mDestImage, theSourceGraphics.mRenderContext);
					}
					else
					{
						hrenderContext2 = renderDevice.CreateContext(this.mDestImage);
					}
					if (hrenderContext2.IsValid())
					{
						this.mRenderDevice = renderDevice;
						this.mRenderContext = hrenderContext2;
						this.mGraphics3D = null;
						this.mIs3D = false;
					}
				}
			}
		}

		protected void SetAsCurrentContext()
		{
			if (this.mRenderDevice != null)
			{
				this.mRenderDevice.SetCurrentContext(this.mRenderContext);
			}
		}

		protected void CalcFinalColor()
		{
			if (this.mPushedColorVector.Count > 0)
			{
				SexyColor color = this.mPushedColorVector[this.mPushedColorVector.Count - 1];
				this.mFinalColor = new SexyColor(Math.Min(255, color.mRed * this.mColor.mRed / 255), Math.Min(255, color.mGreen * this.mColor.mGreen / 255), Math.Min(255, color.mBlue * this.mColor.mBlue / 255), Math.Min(255, color.mAlpha * this.mColor.mAlpha / 255));
				return;
			}
			this.mFinalColor = this.mColor;
		}

		protected SexyColor GetImageColor()
		{
			if (this.mPushedColorVector.Count > 0)
			{
				if (this.mColorizeImages)
				{
					return this.mFinalColor;
				}
				return this.mPushedColorVector[this.mPushedColorVector.Count - 1];
			}
			else
			{
				if (this.mColorizeImages)
				{
					return this.mColor;
				}
				return SexyColor.White;
			}
		}

		protected bool DrawLineClipHelper(ref double theStartX, ref double theStartY, ref double theEndX, ref double theEndY)
		{
			double num = theStartX;
			double num2 = theStartY;
			double num3 = theEndX;
			double num4 = theEndY;
			if (num > num3)
			{
				this.Swap<double>(ref num, ref num3);
				this.Swap<double>(ref num2, ref num4);
			}
			if (num < (double)this.mClipRect.mX)
			{
				if (num3 < (double)this.mClipRect.mX)
				{
					return false;
				}
				double num5 = (num4 - num2) / (num3 - num);
				num2 += ((double)this.mClipRect.mX - num) * num5;
				num = (double)this.mClipRect.mX;
			}
			if (num3 >= (double)(this.mClipRect.mX + this.mClipRect.mWidth))
			{
				if (num >= (double)(this.mClipRect.mX + this.mClipRect.mWidth))
				{
					return false;
				}
				double num6 = (num4 - num2) / (num3 - num);
				num4 += ((double)(this.mClipRect.mX + this.mClipRect.mWidth - 1) - num3) * num6;
				num3 = (double)(this.mClipRect.mX + this.mClipRect.mWidth - 1);
			}
			if (num2 > num4)
			{
				this.Swap<double>(ref num, ref num3);
				this.Swap<double>(ref num2, ref num4);
			}
			if (num2 < (double)this.mClipRect.mY)
			{
				if (num4 < (double)this.mClipRect.mY)
				{
					return false;
				}
				double num7 = (num3 - num) / (num4 - num2);
				num += ((double)this.mClipRect.mY - num2) * num7;
				num2 = (double)this.mClipRect.mY;
			}
			if (num4 >= (double)(this.mClipRect.mY + this.mClipRect.mHeight))
			{
				if (num2 >= (double)(this.mClipRect.mY + this.mClipRect.mHeight))
				{
					return false;
				}
				double num8 = (num3 - num) / (num4 - num2);
				num3 += ((double)(this.mClipRect.mY + this.mClipRect.mHeight - 1) - num4) * num8;
				num4 = (double)(this.mClipRect.mY + this.mClipRect.mHeight - 1);
			}
			theStartX = num;
			theStartY = num2;
			theEndX = num3;
			theEndY = num4;
			return true;
		}

		protected void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		public Graphics()
		{
			this.mTransX = 0f;
			this.mTransY = 0f;
			this.mScaleX = 1f;
			this.mScaleY = 1f;
			this.mScaleOrigX = 0f;
			this.mScaleOrigY = 0f;
			this.mDestImage = null;
			this.mDrawMode = 0;
			this.mColorizeImages = false;
			this.mFastStretch = false;
			this.mWriteColoredString = true;
			this.mLinearBlend = false;
			this.mClipRect = new Rect(0, 0, GlobalMembers.gSexyApp.mGraphicsDriver.GetScreenWidth(), GlobalMembers.gSexyApp.mGraphicsDriver.GetScreenHeight());
			this.InitRenderInfo(null);
		}

		public Graphics(Image theDestImage)
		{
			this.mTransX = 0f;
			this.mTransY = 0f;
			this.mScaleX = 1f;
			this.mScaleY = 1f;
			this.mScaleOrigX = 0f;
			this.mScaleOrigY = 0f;
			this.mDestImage = theDestImage;
			this.mDrawMode = 0;
			this.mColorizeImages = false;
			this.mFastStretch = false;
			this.mWriteColoredString = true;
			this.mLinearBlend = false;
			if (this.mDestImage == null)
			{
				this.mClipRect = new Rect(0, 0, GlobalMembers.gSexyApp.mGraphicsDriver.GetScreenWidth(), GlobalMembers.gSexyApp.mGraphicsDriver.GetScreenHeight());
			}
			else
			{
				this.mClipRect = new Rect(0, 0, this.mDestImage.GetWidth(), this.mDestImage.GetHeight());
			}
			this.InitRenderInfo(null);
		}

		public Graphics(Graphics theGraphics)
		{
			base.CopyStateFrom(theGraphics);
			this.InitRenderInfo(theGraphics);
		}

		public virtual void Dispose()
		{
			this.mRenderDevice.DeleteContext(this.mRenderContext);
		}

		public void ClearRenderContext()
		{
			XNAGraphicsDriver xnagraphicsDriver = (XNAGraphicsDriver)GlobalMembers.gSexyAppBase.mGraphicsDriver;
			xnagraphicsDriver.mXNARenderDevice.SetCurrentContext(null);
		}

		public Graphics3D Get3D()
		{
			return this.mGraphics3D;
		}

		public RenderDevice GetRenderDevice()
		{
			return this.mRenderDevice;
		}

		public HRenderContext GetRenderContext()
		{
			return this.mRenderContext;
		}

		public void PushState()
		{
			GraphicsState graphicsState = GraphicsStatePool.CreateState();
			graphicsState.CopyStateFrom(this);
			this.mStateStack.Push(graphicsState);
			if (this.mRenderDevice != null)
			{
				this.SetAsCurrentContext();
				this.mRenderDevice.PushState();
			}
		}

		public void PopState()
		{
			if (this.mStateStack.Count > 0)
			{
				base.CopyStateFrom(this.mStateStack.Peek());
				GraphicsStatePool.ReleaseState(this.mStateStack.Pop());
			}
			if (this.mRenderDevice != null)
			{
				this.SetAsCurrentContext();
				this.mRenderDevice.PopState();
			}
		}

		public void SetFont(Font theFont)
		{
			this.mFont = theFont;
		}

		public Font GetFont()
		{
			return this.mFont;
		}

		public void SetColor(SexyColor theColor)
		{
			this.mColor.mRed = theColor.mRed;
			this.mColor.mGreen = theColor.mGreen;
			this.mColor.mBlue = theColor.mBlue;
			this.mColor.mAlpha = theColor.mAlpha;
			this.CalcFinalColor();
		}

		public void SetColor(int red, int green, int blue, int alpha)
		{
			this.mColor.mRed = red;
			this.mColor.mGreen = green;
			this.mColor.mBlue = blue;
			this.mColor.mAlpha = alpha;
			this.CalcFinalColor();
		}

		public void SetColor(int red, int green, int blue)
		{
			this.mColor.mRed = red;
			this.mColor.mGreen = green;
			this.mColor.mBlue = blue;
			this.mColor.mAlpha = 255;
			this.CalcFinalColor();
		}

		public SexyColor GetColor()
		{
			return this.mColor;
		}

		public void PushColorMult()
		{
			this.mPushedColorVector.Add(this.mFinalColor);
			this.CalcFinalColor();
		}

		public void PopColorMult()
		{
			this.mPushedColorVector.RemoveAt(this.mPushedColorVector.Count - 1);
			this.CalcFinalColor();
		}

		public SexyColor GetFinalColor()
		{
			if (this.mPushedColorVector.Count > 0)
			{
				return this.mFinalColor;
			}
			return this.mColor;
		}

		public void SetDrawMode(int theDrawMode)
		{
			this.mDrawMode = theDrawMode;
		}

		public int GetDrawMode()
		{
			return this.mDrawMode;
		}

		public void SetColorizeImages(bool colorizeImages)
		{
			this.mColorizeImages = colorizeImages;
		}

		public bool GetColorizeImages()
		{
			return this.mColorizeImages;
		}

		public void SetFastStretch(bool fastStretch)
		{
			this.mFastStretch = fastStretch;
		}

		public bool GetFastStretch()
		{
			return this.mFastStretch;
		}

		public void SetLinearBlend(bool linear)
		{
			this.mLinearBlend = linear;
		}

		public bool GetLinearBlend()
		{
			return this.mLinearBlend;
		}

		public void FillRect(int theX, int theY, int theWidth, int theHeight)
		{
			SexyColor finalColor = this.GetFinalColor();
			if (finalColor.mAlpha == 0)
			{
				return;
			}
			this.SetAsCurrentContext();
			if (this.mRenderDevice != null)
			{
				this.mDestRect.mX = theX + (int)this.mTransX;
				this.mDestRect.mY = theY + (int)this.mTransY;
				this.mDestRect.mWidth = theWidth;
				this.mDestRect.mHeight = theHeight;
				Rect theRect = this.mDestRect.Intersection(this.mClipRect);
				this.mRenderDevice.FillRect(theRect, finalColor, this.mDrawMode);
			}
		}

		public void FillRect(Rect theRect)
		{
			this.FillRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public void DrawRect(int theX, int theY, int theWidth, int theHeight)
		{
			SexyColor finalColor = this.GetFinalColor();
			if (finalColor.mAlpha == 0)
			{
				return;
			}
			Rect theRect = new Rect(theX + (int)this.mTransX, theY + (int)this.mTransY, theWidth, theHeight);
			Rect rect = new Rect(theX + (int)this.mTransX, theY + (int)this.mTransY, theWidth + 1, theHeight + 1);
			Rect rect2 = rect.Intersection(this.mClipRect);
			if (rect.Equals(rect2))
			{
				this.SetAsCurrentContext();
				this.mRenderDevice.DrawRect(theRect, finalColor, this.mDrawMode);
				return;
			}
			this.FillRect(theX, theY, theWidth + 1, 1);
			this.FillRect(theX, theY + theHeight, theWidth + 1, 1);
			this.FillRect(theX, theY + 1, 1, theHeight - 1);
			this.FillRect(theX + theWidth, theY + 1, 1, theHeight - 1);
		}

		public void DrawRect(Rect theRect)
		{
			this.DrawRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public void ClearRect(int theX, int theY, int theWidth, int theHeight)
		{
			this.SetAsCurrentContext();
			this.mDestRect.mX = theX + (int)this.mTransX;
			this.mDestRect.mY = theY + (int)this.mTransY;
			this.mDestRect.mWidth = theWidth;
			this.mDestRect.mHeight = theHeight;
			Rect theRect = this.mDestRect.Intersection(this.mClipRect);
			this.mRenderDevice.ClearRect(theRect);
		}

		public void ClearRect(Rect theRect)
		{
			this.ClearRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public void DrawString(string theString, int theX, int theY)
		{
			if (this.mFont != null)
			{
				this.mFont.DrawString(this, theX, theY, theString, this.GetFinalColor(), this.mClipRect);
			}
		}

		public void DrawLine(int theStartX, int theStartY, int theEndX, int theEndY)
		{
			double theStartX2 = (double)((float)theStartX + this.mTransX);
			double theStartY2 = (double)((float)theStartY + this.mTransY);
			double theEndX2 = (double)((float)theEndX + this.mTransX);
			double theEndY2 = (double)((float)theEndY + this.mTransY);
			if (!this.DrawLineClipHelper(ref theStartX2, ref theStartY2, ref theEndX2, ref theEndY2))
			{
				return;
			}
			this.SetAsCurrentContext();
			this.mRenderDevice.DrawLine(theStartX2, theStartY2, theEndX2, theEndY2, this.GetFinalColor(), this.mDrawMode);
		}

		public void DrawLineAA(int theStartX, int theStartY, int theEndX, int theEndY)
		{
			double theStartX2 = (double)((float)theStartX + this.mTransX);
			double theStartY2 = (double)((float)theStartY + this.mTransY);
			double theEndX2 = (double)((float)theEndX + this.mTransX);
			double theEndY2 = (double)((float)theEndY + this.mTransY);
			if (!this.DrawLineClipHelper(ref theStartX2, ref theStartY2, ref theEndX2, ref theEndY2))
			{
				return;
			}
			this.SetAsCurrentContext();
			this.mRenderDevice.DrawLine(theStartX2, theStartY2, theEndX2, theEndY2, this.GetFinalColor(), this.mDrawMode, true);
		}

		public void PolyFill(SexyPoint[] theVertexList, int theNumVertices, bool convex)
		{
			this.SetAsCurrentContext();
			if (convex && this.mRenderDevice.CanFillPoly())
			{
				this.mRenderDevice.FillPoly(theVertexList, theNumVertices, this.mClipRect, this.GetFinalColor(), this.mDrawMode, (int)this.mTransX, (int)this.mTransY);
				return;
			}
			throw new NotSupportedException();
		}

		public void PolyFillAA(SexyPoint[] theVertexList, int theNumVertices)
		{
			this.PolyFillAA(theVertexList, theNumVertices, false);
		}

		public void PolyFillAA(SexyPoint[] theVertexList, int theNumVertices, bool convex)
		{
			this.SetAsCurrentContext();
			if (convex && this.mRenderDevice.CanFillPoly())
			{
				this.mRenderDevice.FillPoly(theVertexList, theNumVertices, this.mClipRect, this.GetFinalColor(), this.mDrawMode, (int)this.mTransX, (int)this.mTransY);
				return;
			}
			throw new NotSupportedException();
		}

		public void DrawImage(Image theImage, int theX, int theY)
		{
			if (this.mScaleX != 1f || this.mScaleY != 1f)
			{
				this.DrawImage(theImage, theX, theY, theImage.GetRect());
				return;
			}
			theX += (int)this.mTransX;
			theY += (int)this.mTransY;
			this.mDestRect.mX = theX;
			this.mDestRect.mY = theY;
			this.mDestRect.mWidth = theImage.GetWidth();
			this.mDestRect.mHeight = theImage.GetHeight();
			Rect rect = this.mDestRect.Intersection(this.mClipRect);
			this.mSrcRect.mX = rect.mX - theX;
			this.mSrcRect.mY = rect.mY - theY;
			this.mSrcRect.mWidth = rect.mWidth;
			this.mSrcRect.mHeight = rect.mHeight;
			if (this.mSrcRect.mWidth > 0 && this.mSrcRect.mHeight > 0)
			{
				this.SetAsCurrentContext();
				this.mRenderDevice.Blt(theImage, rect.mX, rect.mY, this.mSrcRect, this.GetImageColor(), this.mDrawMode);
			}
		}

		public void DrawImage(Image theImage, int theX, int theY, Rect theSrcRect)
		{
			if (theSrcRect.mX + theSrcRect.mWidth > theImage.GetWidth() || theSrcRect.mY + theSrcRect.mHeight > theImage.GetHeight())
			{
				return;
			}
			theX += (int)this.mTransX;
			theY += (int)this.mTransY;
			if (this.mScaleX != 1f || this.mScaleY != 1f)
			{
				Rect theDestRect = new Rect((int)((double)this.mScaleOrigX + Math.Floor((double)(((float)theX - this.mScaleOrigX) * this.mScaleX))), (int)((double)this.mScaleOrigY + Math.Floor((double)(((float)theY - this.mScaleOrigY) * this.mScaleY))), (int)Math.Ceiling((double)((float)theSrcRect.mWidth * this.mScaleX)), (int)Math.Ceiling((double)((float)theSrcRect.mHeight * this.mScaleY)));
				this.SetAsCurrentContext();
				this.mRenderDevice.BltStretched(theImage, theDestRect, theSrcRect, this.mClipRect, this.GetImageColor(), this.mDrawMode, this.mFastStretch);
				return;
			}
			this.mDestRect.mX = theX;
			this.mDestRect.mY = theY;
			this.mDestRect.mWidth = theSrcRect.mWidth;
			this.mDestRect.mHeight = theSrcRect.mHeight;
			Rect rect = this.mDestRect.Intersection(this.mClipRect);
			this.mSrcRect.mX = theSrcRect.mX + rect.mX - theX;
			this.mSrcRect.mY = theSrcRect.mY + rect.mY - theY;
			this.mSrcRect.mWidth = rect.mWidth;
			this.mSrcRect.mHeight = rect.mHeight;
			if (this.mSrcRect.mWidth > 0 && this.mSrcRect.mHeight > 0)
			{
				this.SetAsCurrentContext();
				this.mRenderDevice.Blt(theImage, rect.mX, rect.mY, this.mSrcRect, this.GetImageColor(), this.mDrawMode);
			}
		}

		public void DrawImage(Image theImage, Rect theDestRect, Rect theSrcRect)
		{
			this.mDestRect.mX = theDestRect.mX + (int)this.mTransX;
			this.mDestRect.mY = theDestRect.mY + (int)this.mTransY;
			this.mDestRect.mWidth = theDestRect.mWidth;
			this.mDestRect.mHeight = theDestRect.mHeight;
			if (this.mScaleX != 1f || this.mScaleY != 1f)
			{
				this.mDestRect = new Rect((int)((double)this.mScaleOrigX + Math.Floor((double)(((float)this.mDestRect.mX - this.mScaleOrigX) * this.mScaleX))), (int)((double)this.mScaleOrigY + Math.Floor((double)(((float)this.mDestRect.mY - this.mScaleOrigY) * this.mScaleY))), (int)Math.Ceiling((double)((float)this.mDestRect.mWidth * this.mScaleX)), (int)Math.Ceiling((double)((float)this.mDestRect.mHeight * this.mScaleY)));
			}
			this.SetAsCurrentContext();
			this.mRenderDevice.BltStretched(theImage, this.mDestRect, theSrcRect, this.mClipRect, this.GetImageColor(), this.mDrawMode, this.mFastStretch);
		}

		public void DrawImage(Image theImage, int theX, int theY, int theStretchedWidth, int theStretchedHeight)
		{
			this.mDestRect.mX = theX + (int)this.mTransX;
			this.mDestRect.mY = theY + (int)this.mTransY;
			this.mDestRect.mWidth = theStretchedWidth;
			this.mDestRect.mHeight = theStretchedHeight;
			this.mSrcRect.mX = 0;
			this.mSrcRect.mY = 0;
			this.mSrcRect.mWidth = theImage.mWidth;
			this.mSrcRect.mHeight = theImage.mHeight;
			this.SetAsCurrentContext();
			this.mRenderDevice.BltStretched(theImage, this.mDestRect, theImage.GetRect(), this.mClipRect, this.GetImageColor(), this.mDrawMode, this.mFastStretch);
		}

		public void DrawImageF(Image theImage, float theX, float theY)
		{
			theX += this.mTransX;
			theY += this.mTransY;
			this.SetAsCurrentContext();
			this.mRenderDevice.BltF(theImage, theX, theY, theImage.GetRect(), this.mClipRect, this.GetImageColor(), this.mDrawMode);
		}

		public void DrawImageF(Image theImage, float theX, float theY, Rect theSrcRect)
		{
			theX += this.mTransX;
			theY += this.mTransY;
			this.SetAsCurrentContext();
			this.mRenderDevice.BltF(theImage, theX, theY, theSrcRect, this.mClipRect, this.GetImageColor(), this.mDrawMode);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY)
		{
			this.DrawImageMirror(theImage, theX, theY, true);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY, int theStretchedWidth, int theStretchedHeight)
		{
			this.mDestRect.setValue(theX, theY, theStretchedWidth, theStretchedHeight);
			this.DrawImageMirror(theImage, this.mDestRect, theImage.GetRect(), true);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY, bool mirror)
		{
			this.DrawImageMirror(theImage, theX, theY, theImage.GetRect(), mirror);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY, Rect theSrcRect)
		{
			this.DrawImageMirror(theImage, theX, theY, theSrcRect, true);
		}

		public void DrawImageMirror(Image theImage, int theX, int theY, Rect theSrcRect, bool mirror)
		{
			if (!mirror)
			{
				this.DrawImage(theImage, theX, theY, theSrcRect);
				return;
			}
			theX += (int)this.mTransX;
			theY += (int)this.mTransY;
			if (theSrcRect.mX + theSrcRect.mWidth > theImage.GetWidth() || theSrcRect.mY + theSrcRect.mHeight > theImage.GetHeight())
			{
				return;
			}
			this.mDestRect.mX = theX;
			this.mDestRect.mY = theY;
			this.mDestRect.mWidth = theSrcRect.mWidth;
			this.mDestRect.mHeight = theSrcRect.mHeight;
			Rect rect = this.mDestRect.Intersection(this.mClipRect);
			int num = theSrcRect.mWidth - rect.mWidth;
			int num2 = rect.mX - theX;
			int num3 = num - num2;
			this.mSrcRect.mX = theSrcRect.mX + num3;
			this.mSrcRect.mY = theSrcRect.mY + rect.mY - theY;
			this.mSrcRect.mWidth = rect.mWidth;
			this.mSrcRect.mHeight = rect.mHeight;
			if (this.mSrcRect.mWidth > 0 && this.mSrcRect.mHeight > 0)
			{
				this.SetAsCurrentContext();
				this.mRenderDevice.BltMirror(theImage, rect.mX, rect.mY, this.mSrcRect, this.GetImageColor(), this.mDrawMode);
			}
		}

		public void DrawImageMirror(Image theImage, Rect theDestRect, Rect theSrcRect)
		{
			this.DrawImageMirror(theImage, theDestRect, theSrcRect, true);
		}

		public void DrawImageMirror(Image theImage, Rect theDestRect, Rect theSrcRect, bool mirror)
		{
			if (!mirror)
			{
				this.DrawImage(theImage, theDestRect, theSrcRect);
				return;
			}
			this.mDestRect.mX = theDestRect.mX + (int)this.mTransX;
			this.mDestRect.mY = theDestRect.mY + (int)this.mTransY;
			this.mDestRect.mWidth = theDestRect.mWidth;
			this.mDestRect.mHeight = theDestRect.mHeight;
			this.SetAsCurrentContext();
			this.mRenderDevice.BltStretched(theImage, this.mDestRect, theSrcRect, this.mClipRect, this.GetImageColor(), this.mDrawMode, this.mFastStretch, true);
		}

		public void DrawImageRotated(Image theImage, int theX, int theY, double theRot)
		{
			this.DrawImageRotated(theImage, theX, theY, theRot, Rect.INVALIDATE_RECT);
		}

		public void DrawImageRotated(Image theImage, int theX, int theY, double theRot, Rect theSrcRect)
		{
			if (theSrcRect == Rect.INVALIDATE_RECT)
			{
				int num = theImage.GetWidth() / 2;
				int num2 = theImage.GetHeight() / 2;
				this.DrawImageRotatedF(theImage, (float)theX, (float)theY, theRot, (float)num, (float)num2, theSrcRect);
				return;
			}
			int num3 = theSrcRect.mWidth / 2;
			int num4 = theSrcRect.mHeight / 2;
			this.DrawImageRotatedF(theImage, (float)theX, (float)theY, theRot, (float)num3, (float)num4, theSrcRect);
		}

		public void DrawImageRotated(Image theImage, int theX, int theY, double theRot, int theRotCenterX, int theRotCenterY)
		{
			this.DrawImageRotated(theImage, theX, theY, theRot, theRotCenterX, theRotCenterY, Rect.INVALIDATE_RECT);
		}

		public void DrawImageRotated(Image theImage, int theX, int theY, double theRot, int theRotCenterX, int theRotCenterY, Rect theSrcRect)
		{
			this.DrawImageRotatedF(theImage, (float)theX, (float)theY, theRot, (float)theRotCenterX, (float)theRotCenterY, theSrcRect);
		}

		public void DrawImageRotatedF(Image theImage, float theX, float theY, double theRot)
		{
			this.DrawImageRotatedF(theImage, theX, theY, theRot, Rect.INVALIDATE_RECT);
		}

		public void DrawImageRotatedF(Image theImage, float theX, float theY, double theRot, Rect theSrcRect)
		{
			if (theSrcRect == Rect.INVALIDATE_RECT)
			{
				float theRotCenterX = (float)theImage.GetWidth() / 2f;
				float theRotCenterY = (float)theImage.GetHeight() / 2f;
				this.DrawImageRotatedF(theImage, theX, theY, theRot, theRotCenterX, theRotCenterY, theSrcRect);
				return;
			}
			float theRotCenterX2 = (float)theSrcRect.mWidth / 2f;
			float theRotCenterY2 = (float)theSrcRect.mHeight / 2f;
			this.DrawImageRotatedF(theImage, theX, theY, theRot, theRotCenterX2, theRotCenterY2, theSrcRect);
		}

		public void DrawImageRotatedF(Image theImage, float theX, float theY, double theRot, float theRotCenterX, float theRotCenterY)
		{
			this.DrawImageRotatedF(theImage, theX, theY, theRot, theRotCenterX, theRotCenterY, Rect.INVALIDATE_RECT);
		}

		public void DrawImageRotatedF(Image theImage, float theX, float theY, double theRot, float theRotCenterX, float theRotCenterY, Rect theSrcRect)
		{
			theX += this.mTransX;
			theY += this.mTransY;
			this.SetAsCurrentContext();
			if (theSrcRect == Rect.INVALIDATE_RECT)
			{
				this.mRenderDevice.BltRotated(theImage, theX, theY, theImage.GetRect(), this.mClipRect, this.GetImageColor(), this.mDrawMode, theRot, theRotCenterX, theRotCenterY);
				return;
			}
			this.mRenderDevice.BltRotated(theImage, theX, theY, theSrcRect, this.mClipRect, this.GetImageColor(), this.mDrawMode, theRot, theRotCenterX, theRotCenterY);
		}

		public void DrawImageMatrix(Image theImage, SexyMatrix3 theMatrix, float x)
		{
			this.DrawImageMatrix(theImage, theMatrix, x, 0f);
		}

		public void DrawImageMatrix(Image theImage, SexyMatrix3 theMatrix)
		{
			this.DrawImageMatrix(theImage, theMatrix, 0f, 0f);
		}

		public void DrawImageMatrix(Image theImage, SexyMatrix3 theMatrix, float x, float y)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.BltMatrix(theImage, x + this.mTransX, y + this.mTransY, theMatrix, this.mClipRect, this.GetImageColor(), this.mDrawMode, theImage.GetRect(), this.mLinearBlend);
		}

		public void DrawImageMatrix(Image theImage, SexyMatrix3 theMatrix, Rect theSrcRect, float x)
		{
			this.DrawImageMatrix(theImage, theMatrix, theSrcRect, x, 0f);
		}

		public void DrawImageMatrix(Image theImage, SexyMatrix3 theMatrix, Rect theSrcRect)
		{
			this.DrawImageMatrix(theImage, theMatrix, theSrcRect, 0f, 0f);
		}

		public void DrawImageMatrix(Image theImage, SexyMatrix3 theMatrix, Rect theSrcRect, float x, float y)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.BltMatrix(theImage, x + this.mTransX, y + this.mTransY, theMatrix, this.mClipRect, this.GetImageColor(), this.mDrawMode, theSrcRect, this.mLinearBlend);
		}

		public void DrawImageTransform(Image theImage, Transform theTransform, float x, float y)
		{
			this.DrawImageTransformHelper(theImage, theTransform, theImage.GetRect(), x, y, false);
		}

		public void DrawImageTransform(Image theImage, Transform theTransform, Rect theSrcRect, float x, float y)
		{
			this.DrawImageTransformHelper(theImage, theTransform, theSrcRect, x, y, false);
		}

		public void DrawImageTransformF(Image theImage, Transform theTransform, float x, float y)
		{
			this.DrawImageTransformHelper(theImage, theTransform, theImage.GetRect(), x, y, true);
		}

		public void DrawImageTransformF(Image theImage, Transform theTransform, Rect theSrcRect, float x, float y)
		{
			this.DrawImageTransformHelper(theImage, theTransform, theSrcRect, x, y, true);
		}

		public void DrawTriangleTex(Image theTexture, SexyVertex2D v1, SexyVertex2D v2, SexyVertex2D v3)
		{
			SexyVertex2D[,] array = new SexyVertex2D[1, 3];
			array[0, 0] = v1;
			array[0, 1] = v2;
			array[0, 2] = v3;
			SexyVertex2D[,] theVertices = array;
			this.SetAsCurrentContext();
			this.mRenderDevice.BltTriangles(theTexture, theVertices, 1, this.GetImageColor(), this.mDrawMode, this.mTransX, this.mTransY, this.mLinearBlend, this.mClipRect);
		}

		public void DrawTrianglesTex(Image theTexture, SexyVertex2D[,] theVertices, int theNumTriangles)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.BltTriangles(theTexture, theVertices, theNumTriangles, this.GetImageColor(), this.mDrawMode, this.mTransX, this.mTransY, this.mLinearBlend, this.mClipRect);
		}

		public void DrawTrianglesTex(Image theTexture, SexyVertex2D[,] theVertices, int theNumTriangles, SexyColor theColor, int theDrawMode, float tx, float ty, bool blend, Rect theClipRect)
		{
			this.SetAsCurrentContext();
			this.mRenderDevice.BltTriangles(theTexture, theVertices, theNumTriangles, theColor, theDrawMode, tx, ty, blend, theClipRect);
		}

		public void DrawTrianglesTexStrip(Image theTexture, SexyVertex2D[] theVertices, int theNumTriangles)
		{
			this.DrawTrianglesTexStrip(theTexture, theVertices, theNumTriangles, this.GetImageColor(), this.mDrawMode, this.mTransX, this.mTransY, this.mLinearBlend);
		}

		public void DrawTrianglesTexStrip(Image theTexture, SexyVertex2D[] theVertices, int theNumTriangles, SexyColor theColor, int theDrawMode, float tx, float ty, bool blend)
		{
			this.SetAsCurrentContext();
			SexyVertex2D[,] array = new SexyVertex2D[100, 3];
			int i = 0;
			while (i < theNumTriangles)
			{
				int num = Math.Min(100, theNumTriangles - i);
				for (int j = 0; j < num; j++)
				{
					array[j, 0] = theVertices[i];
					array[j, 1] = theVertices[i + 1];
					array[j, 2] = theVertices[i + 2];
					i++;
				}
				this.mRenderDevice.BltTriangles(theTexture, array, num, theColor, theDrawMode, tx, ty, blend);
			}
		}

		public void DrawImageCel(Image theImageStrip, int theX, int theY, int theCel)
		{
			this.DrawImageCel(theImageStrip, theX, theY, theCel % theImageStrip.mNumCols, theCel / theImageStrip.mNumCols);
		}

		public void DrawImageCel(Image theImageStrip, Rect theDestRect, int theCel)
		{
			this.DrawImageCel(theImageStrip, theDestRect, theCel % theImageStrip.mNumCols, theCel / theImageStrip.mNumCols);
		}

		public void DrawImageCel(Image theImageStrip, int theX, int theY, int theCelCol, int theCelRow)
		{
			if (theCelRow < 0 || theCelCol < 0 || theCelRow >= theImageStrip.mNumRows || theCelCol >= theImageStrip.mNumCols)
			{
				return;
			}
			int num = theImageStrip.mWidth / theImageStrip.mNumCols;
			int num2 = theImageStrip.mHeight / theImageStrip.mNumRows;
			Rect theSrcRect = new Rect(num * theCelCol, num2 * theCelRow, num, num2);
			this.DrawImage(theImageStrip, theX, theY, theSrcRect);
		}

		public void DrawImageCel(Image theImageStrip, Rect theDestRect, int theCelCol, int theCelRow)
		{
			if (theCelRow < 0 || theCelCol < 0 || theCelRow >= theImageStrip.mNumRows || theCelCol >= theImageStrip.mNumCols)
			{
				return;
			}
			int num = theImageStrip.mWidth / theImageStrip.mNumCols;
			int num2 = theImageStrip.mHeight / theImageStrip.mNumRows;
			Rect theSrcRect = new Rect(num * theCelCol, num2 * theCelRow, num, num2);
			this.DrawImage(theImageStrip, theDestRect, theSrcRect);
		}

		public void DrawImageAnim(Image theImageAnim, int theX, int theY, int theTime)
		{
			this.DrawImageCel(theImageAnim, theX, theY, theImageAnim.GetAnimCel(theTime));
		}

		public void BeginDrawSprite()
		{
			this.mRenderDevice.BeginSprite();
		}

		public void EndDrawSprite()
		{
			this.mRenderDevice.EndSprite();
		}

		public void DrawSprite(Image theImage, SexyTransform2D theTransform, Rect theSrcRect)
		{
			this.mRenderDevice.DrawSprite(theImage, this.GetImageColor(), this.mDrawMode, theTransform, theSrcRect, true);
		}

		public void ClearClipRect()
		{
			if (this.mDestImage != null)
			{
				this.mClipRect.mX = 0;
				this.mClipRect.mY = 0;
				this.mClipRect.mWidth = this.mDestImage.GetWidth();
				this.mClipRect.mHeight = this.mDestImage.GetHeight();
				return;
			}
			this.mClipRect.mX = 0;
			this.mClipRect.mY = 0;
			this.mClipRect.mWidth = GlobalMembers.gSexyAppBase.mWidth;
			this.mClipRect.mHeight = GlobalMembers.gSexyAppBase.mHeight;
		}

		public void SetClipRect(int theX, int theY, int theWidth, int theHeight)
		{
			if (this.mDestImage != null)
			{
				this.mClipRect.mX = 0;
				this.mClipRect.mY = 0;
				this.mClipRect.mWidth = this.mDestImage.GetWidth();
				this.mClipRect.mHeight = this.mDestImage.GetHeight();
				this.mDestRect.mX = theX + (int)this.mTransX;
				this.mDestRect.mY = theY + (int)this.mTransY;
				this.mDestRect.mWidth = theWidth;
				this.mDestRect.mHeight = theHeight;
				this.mClipRect = this.mClipRect.Intersection(this.mDestRect);
				return;
			}
			this.mClipRect.mX = -1;
			this.mClipRect.mY = -1;
			this.mClipRect.mWidth = GlobalMembers.gSexyAppBase.mWidth + 1;
			this.mClipRect.mHeight = GlobalMembers.gSexyAppBase.mHeight + 1;
			this.mDestRect.mX = theX + (int)this.mTransX;
			this.mDestRect.mY = theY + (int)this.mTransY;
			this.mDestRect.mWidth = theWidth;
			this.mDestRect.mHeight = theHeight;
			this.mClipRect = this.mClipRect.Intersection(this.mDestRect);
		}

		public void SetClipRect(Rect theRect)
		{
			this.SetClipRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public void ClipRect(int theX, int theY, int theWidth, int theHeight)
		{
			this.mDestRect.mX = theX + (int)this.mTransX;
			this.mDestRect.mY = theY + (int)this.mTransY;
			this.mDestRect.mWidth = theWidth;
			this.mDestRect.mHeight = theHeight;
			this.mClipRect = this.mClipRect.Intersection(this.mDestRect);
		}

		public void ClipRect(Rect theRect)
		{
			this.ClipRect(theRect.mX, theRect.mY, theRect.mWidth, theRect.mHeight);
		}

		public void Translate(int theTransX, int theTransY)
		{
			this.mTransX += (float)theTransX;
			this.mTransY += (float)theTransY;
		}

		public void TranslateF(float theTransX, float theTransY)
		{
			this.mTransX += theTransX;
			this.mTransY += theTransY;
		}

		public void SetScale(float theScaleX, float theScaleY, float theOrigX, float theOrigY)
		{
			this.mScaleX = theScaleX;
			this.mScaleY = theScaleY;
			this.mScaleOrigX = theOrigX + this.mTransX;
			this.mScaleOrigY = theOrigY + this.mTransY;
		}

		public int StringWidth(string theString)
		{
			return this.mFont.StringWidth(theString);
		}

		public void DrawImageBox(Rect theDest, Image theComponentImage)
		{
			this.DrawImageBox(theComponentImage.GetRect(), theDest, theComponentImage);
		}

		public void DrawImageBox(Rect theSrc, Rect theDest, Image theComponentImage)
		{
			if (theSrc.mWidth <= 0 || theSrc.mHeight <= 0)
			{
				return;
			}
			int num = theSrc.mWidth / 3;
			int num2 = theSrc.mHeight / 3;
			int mX = theSrc.mX;
			int mY = theSrc.mY;
			int num3 = theSrc.mWidth - num * 2;
			int num4 = theSrc.mHeight - num2 * 2;
			int num5 = num;
			int num6 = num2;
			bool flag = false;
			if (theDest.mWidth < num * 2)
			{
				num5 = theDest.mWidth / 2;
				if ((theDest.mWidth & 1) == 1)
				{
					num5++;
				}
				flag = true;
			}
			if (theDest.mHeight < num2 * 2)
			{
				num6 = theDest.mHeight / 2;
				if ((theDest.mHeight & 1) == 1)
				{
					num6++;
				}
				flag = true;
			}
			Rect mClipRect = this.mClipRect;
			if (flag)
			{
				this.mDestRect.setValue(ref theDest.mX, ref theDest.mY, ref num5, ref num6);
				this.mSrcRect.setValue(ref mX, ref mY, ref num, ref num2);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
				this.mDestRect.setValue(theDest.mX + theDest.mWidth - num5, theDest.mY, num5, num6);
				this.mSrcRect.setValue(mX + num + num3, mY, num, num2);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
				this.mDestRect.setValue(theDest.mX, theDest.mY + theDest.mHeight - num6, num5, num6);
				this.mSrcRect.setValue(mX, mY + num2 + num4, num, num2);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
				this.mDestRect.setValue(theDest.mX + theDest.mWidth - num5, theDest.mY + theDest.mHeight - num6, num5, num6);
				this.mSrcRect.setValue(mX + num + num3, mY + num2 + num4, num, num2);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
				this.ClipRect(theDest.mX + num5, theDest.mY, theDest.mWidth - num5 * 2, theDest.mHeight);
				int i;
				for (i = 0; i < (theDest.mWidth - num * 2 + num3 - 1) / num3; i++)
				{
					this.mDestRect.setValue(theDest.mX + num5 + i * num3, theDest.mY, num3, num6);
					this.mSrcRect.setValue(mX + num, mY, num3, num2);
					this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
					this.mDestRect.setValue(theDest.mX + num5 + i * num3, theDest.mY + theDest.mHeight - num6, num3, num6);
					this.mSrcRect.setValue(mX + num, mY + num2 + num4, num3, num2);
					this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
				}
				this.mClipRect = mClipRect;
				this.ClipRect(theDest.mX, theDest.mY + num6, theDest.mWidth, theDest.mHeight - num6 * 2);
				for (int j = 0; j < (theDest.mHeight - num2 * 2 + num4 - 1) / num4; j++)
				{
					this.mDestRect.setValue(theDest.mX + num5 + i * num3, theDest.mY, num3, num6);
					this.mSrcRect.setValue(mX, mY + num2, num, num4);
					this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
					this.mDestRect.setValue(theDest.mX + theDest.mWidth - num5, theDest.mY + num6 + j * num4, num5, num4);
					this.mSrcRect.setValue(mX + num + num3, mY + num2, num, num4);
					this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
				}
				this.mClipRect = mClipRect;
				this.ClipRect(theDest.mX + num5, theDest.mY + num6, theDest.mWidth - num5 * 2, theDest.mHeight - num6 * 2);
				for (i = 0; i < (theDest.mWidth - num5 * 2 + num3 - 1) / num3; i++)
				{
					for (int j = 0; j < (theDest.mHeight - num6 * 2 + num4 - 1) / num4; j++)
					{
						this.mSrcRect.setValue(mX + num5, mY + num6, num3, num4);
						this.DrawImage(theComponentImage, theDest.mX + num5 + i * num3, theDest.mY + num6 + j * num4, this.mSrcRect);
					}
				}
				this.mClipRect = mClipRect;
				return;
			}
			this.mSrcRect.setValue(mX, mY, num, num2);
			this.DrawImage(theComponentImage, theDest.mX, theDest.mY, this.mSrcRect);
			this.mSrcRect.setValue(mX + num + num3, mY, num, num2);
			this.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num, theDest.mY, this.mSrcRect);
			this.mSrcRect.setValue(mX, mY + num2 + num4, num, num2);
			this.DrawImage(theComponentImage, theDest.mX, theDest.mY + theDest.mHeight - num2, this.mSrcRect);
			this.mSrcRect.setValue(mX + num + num3, mY + num2 + num4, num, num2);
			this.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num, theDest.mY + theDest.mHeight - num2, this.mSrcRect);
			this.ClipRect(theDest.mX + num, theDest.mY, theDest.mWidth - num * 2, theDest.mHeight);
			for (int k = 0; k < (theDest.mWidth - num * 2 + num3 - 1) / num3; k++)
			{
				this.mSrcRect.setValue(mX + num, mY, num3, num2);
				this.DrawImage(theComponentImage, theDest.mX + num + k * num3, theDest.mY, this.mSrcRect);
				this.mSrcRect.setValue(mX + num, mY + num2 + num4, num3, num2);
				this.DrawImage(theComponentImage, theDest.mX + num + k * num3, theDest.mY + theDest.mHeight - num2, this.mSrcRect);
			}
			this.mClipRect = mClipRect;
			this.ClipRect(theDest.mX, theDest.mY + num2, theDest.mWidth, theDest.mHeight - num2 * 2);
			for (int l = 0; l < (theDest.mHeight - num2 * 2 + num4 - 1) / num4; l++)
			{
				this.mSrcRect.setValue(mX, mY + num2, num, num4);
				this.DrawImage(theComponentImage, theDest.mX, theDest.mY + num2 + l * num4, this.mSrcRect);
				this.mSrcRect.setValue(mX + num + num3, mY + num2, num, num4);
				this.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num, theDest.mY + num2 + l * num4, this.mSrcRect);
			}
			this.mClipRect = mClipRect;
			this.ClipRect(theDest.mX + num, theDest.mY + num2, theDest.mWidth - num * 2, theDest.mHeight - num2 * 2);
			for (int k = 0; k < (theDest.mWidth - num * 2 + num3 - 1) / num3; k++)
			{
				for (int l = 0; l < (theDest.mHeight - num2 * 2 + num4 - 1) / num4; l++)
				{
					this.mSrcRect.setValue(mX + num, mY + num2, num3, num4);
					this.DrawImage(theComponentImage, theDest.mX + num + k * num3, theDest.mY + num2 + l * num4, this.mSrcRect);
				}
			}
			this.mClipRect = mClipRect;
		}

		public void DrawImageBoxStretch(Rect theDest, Image theComponentImage)
		{
			this.DrawImageBoxStretch(theComponentImage.GetRect(), theDest, theComponentImage);
		}

		public void DrawImageBoxStretch(Rect theSrc, Rect theDest, Image theComponentImage)
		{
			if (theSrc.mWidth <= 0 || theSrc.mHeight <= 0)
			{
				return;
			}
			int num = theSrc.mWidth / 3;
			int num2 = theSrc.mHeight / 3;
			int mX = theSrc.mX;
			int mY = theSrc.mY;
			int num3 = theSrc.mWidth - num * 2;
			int num4 = theSrc.mHeight - num2 * 2;
			int num5 = num;
			int num6 = num2;
			if (theDest.mWidth < num * 2)
			{
				num5 = theDest.mWidth / 2;
				if ((theDest.mWidth & 1) == 1)
				{
					num5++;
				}
			}
			if (theDest.mHeight < num2 * 2)
			{
				num6 = theDest.mHeight / 2;
				if ((theDest.mHeight & 1) == 1)
				{
					num6++;
				}
			}
			this.mDestRect.setValue(theDest.mX, theDest.mY, num5, num6);
			this.mSrcRect.setValue(mX, mY, num, num2);
			this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
			this.mDestRect.setValue(theDest.mX + theDest.mWidth - num5, theDest.mY, num5, num6);
			this.mSrcRect.setValue(mX + num + num3, mY, num, num2);
			this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
			this.mDestRect.setValue(theDest.mX, theDest.mY + theDest.mHeight - num6, num5, num6);
			this.mSrcRect.setValue(mX, mY + num2 + num4, num, num2);
			this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
			this.mDestRect.setValue(theDest.mX + theDest.mWidth - num5, theDest.mY + theDest.mHeight - num6, num5, num6);
			this.mSrcRect.setValue(mX + num + num3, mY + num2 + num4, num, num2);
			this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
			if (theDest.mWidth - num5 * 2 > 0)
			{
				this.mDestRect.setValue(theDest.mX + num5, theDest.mY, theDest.mWidth - num5 * 2, num6);
				this.mSrcRect.setValue(mX + num, mY, num3, num2);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
				this.mDestRect.setValue(theDest.mX + num5, theDest.mY + theDest.mHeight - num6, theDest.mWidth - num5 * 2, num6);
				this.mSrcRect.setValue(mX + num, mY + num2 + num4, num3, num2);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
			}
			if (theDest.mHeight - num6 * 2 > 0)
			{
				this.mDestRect.setValue(theDest.mX, theDest.mY + num6, num5, theDest.mHeight - num6 * 2);
				this.mSrcRect.setValue(mX, mY + num2, num, num4);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
				this.mDestRect.setValue(theDest.mX + theDest.mWidth - num5, theDest.mY + num6, num5, theDest.mHeight - num6 * 2);
				this.mSrcRect.setValue(mX + num + num3, mY + num2, num, num4);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
			}
			if (theDest.mWidth - num5 * 2 > 0 && theDest.mHeight - num6 * 2 > 0)
			{
				this.mDestRect.setValue(theDest.mX + num5, theDest.mY + num6, theDest.mWidth - num5 * 2, theDest.mHeight - num6 * 2);
				this.mSrcRect.setValue(mX + num, mY + num2, num3, num4);
				this.DrawImage(theComponentImage, this.mDestRect, this.mSrcRect);
			}
		}

		public int WriteString(string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset, int theLength)
		{
			return this.WriteString(theString, theX, theY, theWidth, theJustification, drawString, theOffset, theLength, -1);
		}

		public int WriteString(string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset)
		{
			return this.WriteString(theString, theX, theY, theWidth, theJustification, drawString, theOffset, -1, -1);
		}

		public int WriteString(string theString, int theX, int theY, int theWidth, int theJustification, bool drawString)
		{
			return this.WriteString(theString, theX, theY, theWidth, theJustification, drawString, 0, -1, -1);
		}

		public int WriteString(string theString, int theX, int theY, int theWidth, int theJustification)
		{
			return this.WriteString(theString, theX, theY, theWidth, theJustification, true, 0, -1, -1);
		}

		public int WriteString(string theString, int theX, int theY, int theWidth)
		{
			return this.WriteString(theString, theX, theY, theWidth, 0, true, 0, -1, -1);
		}

		public int WriteString(string theString, int theX, int theY)
		{
			return this.WriteString(theString, theX, theY, -1, 0, true, 0, -1, -1);
		}

		public int WriteString(string theString, int theX, int theY, int theWidth, int theJustification, bool drawString, int theOffset, int theLength, int theOldColor)
		{
			if (theOldColor == -1)
			{
				theOldColor = this.GetFinalColor().ToInt();
			}
			if (drawString)
			{
				switch (theJustification)
				{
				case 0:
					theX += (theWidth - this.WriteString(theString, theX, theY, theWidth, -1, false, theOffset, theLength, theOldColor)) / 2;
					break;
				case 1:
					theX += theWidth - this.WriteString(theString, theX, theY, theWidth, -1, false, theOffset, theLength, theOldColor);
					break;
				}
			}
			if (theLength < 0 || theOffset + theLength > theString.Length)
			{
				theLength = theString.Length;
			}
			else
			{
				theLength = theOffset + theLength;
			}
			this.mStringBuilder.Clear();
			int num = 0;
			for (int i = theOffset; i < theLength; i++)
			{
				if (theString[i] == '^' && this.mWriteColoredString)
				{
					if (i + 1 < theLength && theString[i + 1] == '^')
					{
						this.mStringBuilder.Append("^");
						i++;
					}
					else
					{
						if (i > theLength - 8)
						{
							break;
						}
						int num2 = 0;
						if (theString[i + 1] == 'o')
						{
							if (theString.Substring(i + 1).StartsWith("oldclr"))
							{
								num2 = theOldColor;
							}
						}
						else
						{
							for (int j = 0; j < 6; j++)
							{
								char c = theString[i + j + 1];
								int num3 = 0;
								if (c >= '0' && c <= '9')
								{
									num3 = (int)(c - '0');
								}
								else if (c >= 'A' && c <= 'F')
								{
									num3 = (int)(c - 'A' + '\n');
								}
								else if (c >= 'a' && c <= 'f')
								{
									num3 = (int)(c - 'a' + '\n');
								}
								num2 += num3 << (5 - j) * 4;
							}
						}
						string theString2 = this.mStringBuilder.ToString();
						if (drawString)
						{
							this.DrawString(theString2, theX + num, theY);
							this.SetColor((num2 >> 16) & 255, (num2 >> 8) & 255, num2 & 255, this.GetColor().mAlpha);
						}
						i += 7;
						num += this.mFont.StringWidth(theString2);
						this.mStringBuilder.Clear();
					}
				}
				else
				{
					this.mStringBuilder.Append(theString[i]);
				}
			}
			string theString3 = this.mStringBuilder.ToString();
			if (drawString)
			{
				this.DrawString(theString3, theX + num, theY);
			}
			return num + this.mFont.StringWidth(theString3);
		}

		public int WriteWordNoAutoWrapped(string theLine, int x, int y)
		{
			SexyColor color = this.GetColor();
			int num = color.ToInt();
			if (((long)num & (long)((-16777216))) == (long)((-16777216)))
			{
				num &= 16777215;
			}
			int length = theLine.Length;
			Font font = this.GetFont();
			int num2 = font.GetAscent() - font.GetAscentPadding();
			int lineSpacing = font.GetLineSpacing();
			int i = 0;
			int num3 = 0;
			char c = '\0';
			char thePrevChar = '\0';
			int num4 = -1;
			int num5 = 0;
			int num6 = 0;
			bool flag = false;
			int num7 = 0;
			int num8 = num7;
			while (i < theLine.Length)
			{
				c = theLine[i];
				if (c == '^' && this.mWriteColoredString)
				{
					if (i + 1 < theLine.Length)
					{
						if (theLine[i + 1] != '^')
						{
							i += 8;
							continue;
						}
						i++;
					}
				}
				else if (c == ' ')
				{
					num4 = i;
				}
				else if (c == '\n')
				{
					flag = true;
					num4 = i;
					i++;
				}
				num8 += font.CharWidthKern(c, thePrevChar);
				thePrevChar = c;
				if (flag)
				{
					flag = false;
					num6++;
					int num10;
					if (num4 != -1)
					{
						int num9 = y + num2 + (int)this.mTransY;
						if (num9 >= this.mClipRect.mY && num9 < this.mClipRect.mY + this.mClipRect.mHeight + lineSpacing)
						{
							GlobalMembersGraphics.WriteWordWrappedHelper(this, theLine, x + num7, y + num2, -1, -1, true, num3, num4 - num3, num, length);
						}
						num10 = num8 + num7;
						if (num10 < 0)
						{
							break;
						}
						i = num4 + 1;
						if (c != '\n')
						{
							while (i < theLine.Length && theLine[i] == ' ')
							{
								i++;
							}
						}
					}
					else
					{
						if (i < num3 + 1)
						{
							i++;
						}
						num10 = GlobalMembersGraphics.WriteWordWrappedHelper(this, theLine, x + num7, y + num2, -1, -1, true, num3, i - num3, num, length);
						if (num10 < 0)
						{
							break;
						}
					}
					if (num10 > num5)
					{
						num5 = num10;
					}
					num3 = i;
					num4 = -1;
					num8 = 0;
					thePrevChar = '\0';
					num7 = 0;
					num2 += lineSpacing;
				}
				else
				{
					i++;
				}
			}
			if (num3 < theLine.Length)
			{
				int num11 = GlobalMembersGraphics.WriteWordWrappedHelper(this, theLine, x + num7, y + num2, -1, -1, true, num3, theLine.Length - num3, num, length);
				if (num11 >= 0)
				{
					if (num11 > num5)
					{
					}
					num2 += lineSpacing;
				}
			}
			else if (c == '\n')
			{
				num2 += lineSpacing;
			}
			this.SetColor(color);
			return num2 + font.GetDescent() - lineSpacing;
		}

		public int WriteWordWrapped(Rect theRect, string theLine, int theLineSpacing, int theJustification, ref int theMaxWidth, int theMaxChars, ref int theLastWidth, ref int theLineCount, bool drawString)
		{
			SexyColor color = this.GetColor();
			int num = color.ToInt();
			if (((long)num & (long)((-16777216))) == (long)((-16777216)))
			{
				num &= 16777215;
			}
			if (theMaxChars < 0)
			{
				theMaxChars = theLine.Length;
			}
			Font font = this.GetFont();
			int num2 = font.GetAscent() - font.GetAscentPadding();
			if (theLineSpacing == -1)
			{
				theLineSpacing = font.GetLineSpacing();
			}
			int i = 0;
			int num3 = 0;
			char c = '\0';
			char thePrevChar = '\0';
			int num4 = -1;
			int num5 = 0;
			int num6 = 0;
			int num7 = theLastWidth;
			int num8 = num7;
			while (i < theLine.Length)
			{
				c = theLine[i];
				if (c == '^' && this.mWriteColoredString)
				{
					if (i + 1 < theLine.Length)
					{
						if (theLine[i + 1] != '^')
						{
							i += 8;
							continue;
						}
						i++;
					}
				}
				else if (c == ' ')
				{
					num4 = i;
				}
				else if (c == '\n')
				{
					num8 = theRect.mWidth + 1;
					num4 = i;
					i++;
				}
				num8 += font.CharWidthKern(c, thePrevChar);
				thePrevChar = c;
				if (num8 > theRect.mWidth)
				{
					num6++;
					int num10;
					if (num4 != -1)
					{
						int num9 = theRect.mY + num2 + (int)this.mTransY;
						if (num9 >= this.mClipRect.mY && num9 < this.mClipRect.mY + this.mClipRect.mHeight + theLineSpacing)
						{
							GlobalMembersGraphics.WriteWordWrappedHelper(this, theLine, theRect.mX + num7, theRect.mY + num2, theRect.mWidth, theJustification, drawString, num3, num4 - num3, num, theMaxChars);
						}
						num10 = num8 + num7;
						if (num10 < 0)
						{
							break;
						}
						i = num4 + 1;
						if (c != '\n')
						{
							while (i < theLine.Length && theLine[i] == ' ')
							{
								i++;
							}
						}
					}
					else
					{
						if (i < num3 + 1)
						{
							i++;
						}
						num10 = GlobalMembersGraphics.WriteWordWrappedHelper(this, theLine, theRect.mX + num7, theRect.mY + num2, theRect.mWidth, theJustification, drawString, num3, i - num3, num, theMaxChars);
						if (num10 < 0)
						{
							break;
						}
						if (num10 > theMaxWidth)
						{
							theMaxWidth = num10;
						}
						theLastWidth = num10;
					}
					if (num10 > num5)
					{
						num5 = num10;
					}
					num3 = i;
					num4 = -1;
					num8 = 0;
					thePrevChar = '\0';
					num7 = 0;
					num2 += theLineSpacing;
				}
				else
				{
					i++;
				}
			}
			if (num3 < theLine.Length)
			{
				int num11 = GlobalMembersGraphics.WriteWordWrappedHelper(this, theLine, theRect.mX + num7, theRect.mY + num2, theRect.mWidth, theJustification, drawString, num3, theLine.Length - num3, num, theMaxChars);
				if (num11 >= 0)
				{
					if (num11 > num5)
					{
						num5 = num11;
					}
					if (num11 > theMaxWidth)
					{
						theMaxWidth = num11;
					}
					theLastWidth = num11;
					num2 += theLineSpacing;
				}
			}
			else if (c == '\n')
			{
				num2 += theLineSpacing;
				theLastWidth = 0;
			}
			this.SetColor(color);
			theMaxWidth = num5;
			theLineCount = num6;
			return num2 + font.GetDescent() - theLineSpacing;
		}

		public int WriteWordWrapped(Rect theRect, string theLine, int theLineSpacing, int theJustification, ref int theMaxWidth, int theMaxChars, ref int theLastWidth, ref int theLineCount)
		{
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, ref theMaxWidth, theMaxChars, ref theLastWidth, ref theLineCount, true);
		}

		public int WriteWordWrapped(Rect theRect, string theLine, int theLineSpacing, int theJustification, ref int theMaxWidth, int theMaxChars, ref int theLastWidth)
		{
			int num = 0;
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, ref theMaxWidth, theMaxChars, ref theLastWidth, ref num, true);
		}

		public int WriteWordWrapped(Rect theRect, string theLine, int theLineSpacing, int theJustification, ref int theMaxWidth, int theMaxChars)
		{
			int num = 0;
			int num2 = 0;
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, ref theMaxWidth, theMaxChars, ref num2, ref num, true);
		}

		public int WriteWordWrapped(Rect theRect, string theLine, int theLineSpacing, int theJustification, ref int theMaxWidth)
		{
			int num = 0;
			int num2 = 0;
			int theMaxChars = -1;
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, ref theMaxWidth, theMaxChars, ref num2, ref num, true);
		}

		public int WriteWordWrapped(Rect theRect, string theLine, int theLineSpacing, int theJustification)
		{
			int num = 0;
			int num2 = 0;
			int theMaxChars = -1;
			int num3 = 0;
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, ref num3, theMaxChars, ref num2, ref num, true);
		}

		public int WriteWordWrapped(Rect theRect, string theLine, int theLineSpacing)
		{
			int num = 0;
			int num2 = 0;
			int theMaxChars = -1;
			int num3 = 0;
			int theJustification = -1;
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, ref num3, theMaxChars, ref num2, ref num, true);
		}

		public int WriteWordWrapped(Rect theRect, string theLine)
		{
			int num = 0;
			int num2 = 0;
			int theMaxChars = -1;
			int num3 = 0;
			int theJustification = -1;
			int theLineSpacing = -1;
			return this.WriteWordWrapped(theRect, theLine, theLineSpacing, theJustification, ref num3, theMaxChars, ref num2, ref num, true);
		}

		public int DrawStringColor(string theLine, int theX, int theY)
		{
			return this.DrawStringColor(theLine, theX, theY, -1);
		}

		public int DrawStringColor(string theLine, int theX, int theY, int theOldColor)
		{
			return this.WriteString(theLine, theX, theY, -1, -1, true, 0, -1, theOldColor);
		}

		public int DrawStringWordWrapped(string theLine, int theX, int theY, int theWrapWidth, int theLineSpacing, int theJustification, ref int theMaxWidth)
		{
			int num = this.mFont.GetAscent() - this.mFont.GetAscentPadding();
			this.mDestRect.setValue(theX, theY - num, theWrapWidth, 0);
			return this.WriteWordWrapped(this.mDestRect, theLine, theLineSpacing, theJustification, ref theMaxWidth);
		}

		public int GetWordWrappedHeight(int theWidth, string theLine, int theLineSpacing, ref int theMaxWidth, ref int theLineCount)
		{
			Graphics graphics = new Graphics();
			graphics.SetFont(this.mFont);
			theLineCount = 0;
			int num = 0;
			int theMaxChars = -1;
			theMaxWidth = 0;
			int theJustification = -1;
			this.mDestRect.setValue(0, 0, theWidth, 0);
			return graphics.WriteWordWrapped(this.mDestRect, theLine, theLineSpacing, theJustification, ref theMaxWidth, theMaxChars, ref num, ref theLineCount, false);
		}

		public bool Is3D()
		{
			return this.mIs3D;
		}

		protected RenderDevice mRenderDevice;

		protected HRenderContext mRenderContext = new HRenderContext();

		protected Graphics3D mGraphics3D;

		public Edge[] mPFActiveEdgeList;

		public int mPFNumActiveEdges;

		public new SexyPoint[] mPFPoints;

		public int mPFNumVertices;

		public Stack<GraphicsState> mStateStack = new Stack<GraphicsState>();

		protected StringBuilder mStringBuilder = new StringBuilder("");

		public enum DrawMode
		{
			Normal,
			Additive
		}
	}
}
