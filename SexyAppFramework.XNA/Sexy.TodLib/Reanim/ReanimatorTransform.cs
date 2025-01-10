using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
    public/*internal*/ class ReanimatorTransform
    {
        public static void PreallocateMemory()
        {
            for (int i = 0; i < 1000; i++)
            {
                new ReanimatorTransform().PrepareForReuse();
            }
        }

        public static ReanimatorTransform GetNewReanimatorTransform()
        {
            if (ReanimatorTransform.unusedObjects.Count > 0)
            {
                ReanimatorTransform reanimatorTransform = ReanimatorTransform.unusedObjects.Pop();
                reanimatorTransform.Reset();
                return reanimatorTransform;
            }
            return new ReanimatorTransform();
        }

        public static ReanimatorTransform GetReanimatorTransformForLoadingThread()
        {
            return new ReanimatorTransform();
        }

        public override string ToString()
        {
            return "Image: " + mImageName;
        }

        public ReanimatorTransform()
        {
            Reset();
        }

        public void PrepareForReuse()
        {
            ReanimatorTransform.unusedObjects.Push(this);
        }

        private void Reset()
        {
            mTransX = 
                mTransY = 
                mSkewX = 
                mSkewY = 
                mScaleX = 
                mScaleY = 
                mFrame = 
                mAlpha = 
                mSkewXCos = 
                mSkewXSin = 
                mSkewYCos = 
                mSkewYSin = ReanimatorXnaHelpers.DEFAULT_FIELD_PLACEHOLDER;
            mFont = null;
            mImage = null;
            mText = (mImageName = (mFontName = string.Empty));
        }

        public void ExtractImages()
        {
            if (!string.IsNullOrEmpty(mImageName))
            {
                mImage = AtlasResources.GetImageInAtlasById(AtlasResources.GetAtlasIdByStringId(mImageName));
                if (mImage == null && mImageName == "IMAGE_REANIM_ZOMBIESWON")
                {
                    mImage = Resources.IMAGE_REANIM_ZOMBIESWON;
                }
            }
            if (!string.IsNullOrEmpty(mFontName))
            {
                mFont = Resources.GetFontById((int)Resources.GetIdByStringId(mFontName));
            }
        }

        public float mTransX;

        public float mTransY;

        public float mSkewX;

        public float mSkewY;

        public float mScaleX;

        public float mScaleY;

        public float mFrame;

        public float mAlpha;

        public Image mImage;

        public Font mFont;

        public string mImageName;

        public string mFontName;

        public string mText;

        public float mSkewXCos;

        public float mSkewXSin;

        public float mSkewYCos;

        public float mSkewYSin;

        private static Stack<ReanimatorTransform> unusedObjects = new Stack<ReanimatorTransform>(1000);
    }
}
