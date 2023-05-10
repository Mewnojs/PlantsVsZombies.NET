
using Microsoft.Xna.Framework;
using System;

namespace Sexy {
    public class ScreenScales {

        public void Init(int screenWidth, int screenHeight, int virtualWidth, int virtualHeight) 
        {
            double W, H;
            if ((screenHeight * virtualWidth) >= (screenWidth * virtualHeight))
            {
                W = screenWidth;
                H = (virtualHeight * screenWidth / virtualWidth);
                mTransX = 0;
                mTransY = (int)((screenHeight - H) / 2);
                mScaleFactor = (float)(W) / virtualWidth;
            }
            else
            {
                W = (virtualWidth * screenHeight / virtualHeight);
                H = screenHeight;
                mTransX = (int)((screenWidth - W) / 2);
                mTransY = 0;
                mScaleFactor = (float)(H) / virtualHeight;
            }
            mWidth = (int)W;
            mHeight = (int)H;
        }
        public void Init(int screenWidth, int screenHeight, int virtualWidth, int virtualHeight, int transX, int transY)
        {
            mScaleFactor = MathHelper.Min((float)(screenHeight) / virtualHeight, (float)(screenWidth) / virtualWidth);
            mTransX = transX;
            mTransY = transY;
            mWidth = screenWidth;
            mHeight = screenHeight;
        }

        public float TranslationX
        {
            get
            {
                return mTransX;
            }
        }

        public float TranslationY
        {
            get
            {
                return mTransY;
            }
        }

        public float ScaleRatioMin
        {
            get
            {
                return mScaleFactor;
            }
        }

        

        public int mTransX = 0;
        public int mTransY = 0;
        public int mWidth;
        public int mHeight;
        public float mScaleFactor = 1.0f;
        public CGPoint InvMapTouch(CGPoint cGPoint)
        {
            return new CGPoint(InvMapTouchX(cGPoint.X), InvMapTouchY(cGPoint.Y));
        }
        public float InvMapTouchX(float x) 
        {
            return (x - mTransX) / ScaleRatioMin;
        }

        public float InvMapTouchY(float y)
        {
            return (y - mTransY) / ScaleRatioMin;
        }

        public void Scale(ref TRect inRect) 
        {
            inRect.mX = (int)Math.Round(ScaleRatioMin * inRect.mX + TranslationX);
            inRect.mY = (int)Math.Round(ScaleRatioMin * inRect.mY + TranslationY);
            inRect.mWidth = (int)Math.Round(ScaleRatioMin * inRect.mWidth);
            inRect.mHeight = (int)Math.Round(ScaleRatioMin * inRect.mHeight);
        }

        public TRect Scale(TRect inRect)
        {
            this.Scale(ref inRect);
            return inRect;
        }

        public void InvScale(ref TRect inRect)
        {
            inRect.mX = (int)Math.Round((inRect.mX - TranslationX) / ScaleRatioMin);
            inRect.mY = (int)Math.Round((inRect.mY - TranslationY) / ScaleRatioMin);
            inRect.mWidth = (int)Math.Round(inRect.mWidth / ScaleRatioMin);
            inRect.mHeight = (int)Math.Round(inRect.mHeight / ScaleRatioMin);
        }

        public TRect InvScale(TRect inRect)
        {
            this.InvScale(ref inRect);
            return inRect;
        }

        public Matrix ScaleMatrix 
        {
            get 
            {
                return Matrix.CreateScale(ScaleRatioMin, ScaleRatioMin, 1.0f);
            }
        }

        public Matrix IdentityMatrix
        {
            get
            {
                return Matrix.Identity; //ScaleMatrix * TranslationMatrix;
            }
        }

        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(TranslationX, TranslationY, 0.0f);
            }
        }
    }
}