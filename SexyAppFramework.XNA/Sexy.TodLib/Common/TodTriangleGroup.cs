using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
    public/*internal*/ class TodTriangleGroup
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
            Reset();
            TodTriangleGroup.unusedObjects.Push(this);
        }

        private TodTriangleGroup()
        {
            Reset();
        }

        private void Reset()
        {
            mImage = null;
            mTriangleCount = 0;
            mDrawMode = Graphics.DrawMode.DRAWMODE_NORMAL;
        }

        public void DrawGroup(Graphics g)
        {
            if (mImage == null)
            {
                return;
            }
            if (mTriangleCount == 0)
            {
                return;
            }
            if (!SexyGlobal.gSexyAppBase.Is3DAccelerated() && mDrawMode == Graphics.DrawMode.DRAWMODE_ADDITIVE)
            {
                TodTriangleGroup.gTodTriangleDrawAdditive = true;
            }
            g.mDrawMode = mDrawMode;
            for (int i = 0; i < mTriangleCount; i++)
            {
                DrawCall drawCall = mDrawCalls[i];
                g.mClipRect = drawCall.mClipRect;
                g.DrawImageRotated(mImage, (int)drawCall.mPosition.X, (int)drawCall.mPosition.Y, drawCall.mRotation, drawCall.mSrcRect);
            }
            mTriangleCount = 0;
            TodTriangleGroup.gTodTriangleDrawAdditive = false;
        }

        public void AddTriangle(Graphics g, Image theImage, ReanimatorTransform theTransform, TRect theClipRect, SexyColor theColor, Graphics.DrawMode theDrawMode, TRect theSrcRect)
        {
            if (mTriangleCount > 0 && (mDrawMode != theDrawMode || mImage != theImage))
            {
                DrawGroup(g);
            }
            DrawCall drawCall = default(DrawCall);
            drawCall.SetTransform(theTransform);
            drawCall.mClipRect = theClipRect;
            drawCall.mColor = theColor;
            drawCall.mSrcRect = theSrcRect;
            mDrawCalls.Add(drawCall);
            mImage = theImage;
            mDrawMode = theDrawMode;
            mTriangleCount++;
        }

        public static bool gTodTriangleDrawAdditive = false;

        private Image mImage;

        private List<DrawCall> mDrawCalls = new List<DrawCall>();

        private int mTriangleCount;

        private Graphics.DrawMode mDrawMode;

        private static Stack<TodTriangleGroup> unusedObjects = new Stack<TodTriangleGroup>();
    }
}
