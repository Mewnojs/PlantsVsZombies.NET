using System;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public abstract class RenderDevice
	{
		public abstract RenderDevice3D Get3D();

		public abstract bool CanFillPoly();

		public HRenderContext CreateContext(Image theDestImage)
		{
			return this.CreateContext(theDestImage, null);
		}

		public abstract HRenderContext CreateContext(Image theDestImage, HRenderContext theSourceContext);

		public abstract void DeleteContext(HRenderContext theContext);

		public abstract void SetCurrentContext(HRenderContext theContext);

		public abstract HRenderContext GetCurrentContext();

		public abstract void PushState();

		public abstract void PopState();

		public abstract void ClearRect(Rect theRect);

		public abstract void FillRect(Rect theRect, SexyColor theColor, int theDrawMode);

		public abstract void FillScanLinesWithCoverage(RenderDevice.Span theSpans, int theSpanCount, SexyColor theColor, int theDrawMode, string theCoverage, int theCoverX, int theCoverY, int theCoverWidth, int theCoverHeight);

		public virtual void FillPoly(SexyPoint[] theVertices, int theNumVertices, Rect theClipRect, SexyColor theColor, int theDrawMode, int tx, int ty)
		{
		}

		public void DrawLine(double theStartX, double theStartY, double theEndX, double theEndY, SexyColor theColor, int theDrawMode)
		{
			this.DrawLine(theStartX, theStartY, theEndX, theEndY, theColor, theDrawMode, false);
		}

		public abstract void DrawLine(double theStartX, double theStartY, double theEndX, double theEndY, SexyColor theColor, int theDrawMode, bool antiAlias);

		public abstract void Blt(Image theImage, int theX, int theY, Rect theSrcRect, SexyColor theColor, int theDrawMode);

		public abstract void BltF(Image theImage, float theX, float theY, Rect theSrcRect, Rect theClipRect, SexyColor theColor, int theDrawMode);

		public abstract void BltRotated(Image theImage, float theX, float theY, Rect theSrcRect, Rect theClipRect, SexyColor theColor, int theDrawMode, double theRot, float theRotCenterX, float theRotCenterY);

		public abstract void BltMatrix(Image theImage, float x, float y, SexyMatrix3 theMatrix, Rect theClipRect, SexyColor theColor, int theDrawMode, Rect theSrcRect, bool blend);

		public abstract void DrawSprite(Image theImage, SexyColor theColor, int theDrawMode, SexyTransform2D theTransform, Rect theSrcRect, bool center);

		public abstract void BeginSprite();

		public abstract void EndSprite();

		public void BltTriangles(Image theImage, SexyVertex2D[,] theVertices, int theNumTriangles, SexyColor theColor, int theDrawMode, float tx, float ty, bool blend)
		{
			this.BltTriangles(theImage, theVertices, theNumTriangles, theColor, theDrawMode, tx, ty, blend, Rect.INVALIDATE_RECT);
		}

		public void BltTriangles(Image theImage, SexyVertex2D[,] theVertices, int theNumTriangles, SexyColor theColor, int theDrawMode, float tx, float ty)
		{
			this.BltTriangles(theImage, theVertices, theNumTriangles, theColor, theDrawMode, tx, ty, true, Rect.INVALIDATE_RECT);
		}

		public void BltTriangles(Image theImage, SexyVertex2D[,] theVertices, int theNumTriangles, SexyColor theColor, int theDrawMode, float tx)
		{
			this.BltTriangles(theImage, theVertices, theNumTriangles, theColor, theDrawMode, tx, 0f, true, Rect.INVALIDATE_RECT);
		}

		public void BltTriangles(Image theImage, SexyVertex2D[,] theVertices, int theNumTriangles, SexyColor theColor, int theDrawMode)
		{
			this.BltTriangles(theImage, theVertices, theNumTriangles, theColor, theDrawMode, 0f, 0f, true, Rect.INVALIDATE_RECT);
		}

		public abstract void BltTriangles(Image theImage, SexyVertex2D[,] theVertices, int theNumTriangles, SexyColor theColor, int theDrawMode, float tx, float ty, bool blend, Rect theClipRect);

		public abstract void BltMirror(Image theImage, int theX, int theY, Rect theSrcRect, SexyColor theColor, int theDrawMode);

		public void BltStretched(Image theImage, Rect theDestRect, Rect theSrcRect, Rect theClipRect, SexyColor theColor, int theDrawMode, bool fastStretch)
		{
			this.BltStretched(theImage, theDestRect, theSrcRect, theClipRect, theColor, theDrawMode, fastStretch, false);
		}

		public abstract void BltStretched(Image theImage, Rect theDestRect, Rect theSrcRect, Rect theClipRect, SexyColor theColor, int theDrawMode, bool fastStretch, bool mirror);

		public virtual void DrawRect(Rect theRect, SexyColor theColor, int theDrawMode)
		{
			this.FillRect(new Rect(theRect.mX, theRect.mY, theRect.mWidth + 1, 1), theColor, theDrawMode);
			this.FillRect(new Rect(theRect.mX, theRect.mY + theRect.mHeight, theRect.mWidth + 1, 1), theColor, theDrawMode);
			this.FillRect(new Rect(theRect.mX, theRect.mY + 1, 1, theRect.mHeight - 1), theColor, theDrawMode);
			this.FillRect(new Rect(theRect.mX + theRect.mWidth, theRect.mY + 1, 1, theRect.mHeight - 1), theColor, theDrawMode);
		}

		public virtual void FillScanLines(RenderDevice.Span[] theSpans, int theSpanCount, SexyColor theColor, int theDrawMode)
		{
			for (int i = 0; i < theSpanCount; i++)
			{
				RenderDevice.Span span = theSpans[i];
				this.FillRect(new Rect(span.mX, span.mY, span.mWidth, 1), theColor, theDrawMode);
			}
		}

		public class Span
		{
			public int mY;

			public int mX;

			public int mWidth;
		}
	}
}
