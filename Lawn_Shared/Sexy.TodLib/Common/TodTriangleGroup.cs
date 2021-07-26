using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class TodTriangleGroup
	{
		public static TodTriangleGroup GetNewTodTriangleGroup()
		{
			if (TodTriangleGroup.unusedObjects.Count > 0)
			{
				return TodTriangleGroup.unusedObjects.Pop();
			}
			return new TodTriangleGroup();
		}

		public void PrepareForReuse()
		{
			this.Reset();
			TodTriangleGroup.unusedObjects.Push(this);
		}

		private TodTriangleGroup()
		{
			this.Reset();
		}

		private void Reset()
		{
			this.mImage = null;
			this.mTriangleCount = 0;
			this.mDrawMode = Graphics.DrawMode.DRAWMODE_NORMAL;
		}

		public void DrawGroup(Graphics g)
		{
			if (this.mImage == null)
			{
				return;
			}
			if (this.mTriangleCount == 0)
			{
				return;
			}
			if (!GlobalStaticVars.gSexyAppBase.Is3DAccelerated() && this.mDrawMode == Graphics.DrawMode.DRAWMODE_ADDITIVE)
			{
				TodTriangleGroup.gTodTriangleDrawAdditive = true;
			}
			g.mDrawMode = this.mDrawMode;
			for (int i = 0; i < this.mTriangleCount; i++)
			{
				DrawCall drawCall = this.mDrawCalls[i];
				g.mClipRect = drawCall.mClipRect;
				g.DrawImageRotated(this.mImage, (int)drawCall.mPosition.X, (int)drawCall.mPosition.Y, drawCall.mRotation, drawCall.mSrcRect);
			}
			this.mTriangleCount = 0;
			TodTriangleGroup.gTodTriangleDrawAdditive = false;
		}

		public void AddTriangle(Graphics g, Image theImage, ReanimatorTransform theTransform, TRect theClipRect, SexyColor theColor, Graphics.DrawMode theDrawMode, TRect theSrcRect)
		{
			if (this.mTriangleCount > 0 && (this.mDrawMode != theDrawMode || this.mImage != theImage))
			{
				this.DrawGroup(g);
			}
			DrawCall drawCall = default(DrawCall);
			drawCall.SetTransform(theTransform);
			drawCall.mClipRect = theClipRect;
			drawCall.mColor = theColor;
			drawCall.mSrcRect = theSrcRect;
			this.mDrawCalls.Add(drawCall);
			this.mImage = theImage;
			this.mDrawMode = theDrawMode;
			this.mTriangleCount++;
		}

		public static bool gTodTriangleDrawAdditive = false;

		private Image mImage;

		private List<DrawCall> mDrawCalls = new List<DrawCall>();

		private int mTriangleCount;

		private Graphics.DrawMode mDrawMode;

		private static Stack<TodTriangleGroup> unusedObjects = new Stack<TodTriangleGroup>();
	}
}
