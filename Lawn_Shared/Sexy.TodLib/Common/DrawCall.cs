using System;
using Microsoft.Xna.Framework;

namespace Sexy.TodLib
{
    internal struct DrawCall
    {
        public void SetTransform(ReanimatorTransform transform)
        {
            mPosition = new Vector2(transform.mTransX, transform.mTransY);
            mRotation = 0.0;
            mScale = new Vector2(transform.mScaleX, transform.mScaleY);
        }

        public Rect mClipRect;

        public SexyColor mColor;

        public Rect mSrcRect;

        public Vector2 mPosition;

        public double mRotation;

        public Vector2 mScale;
    }
}
