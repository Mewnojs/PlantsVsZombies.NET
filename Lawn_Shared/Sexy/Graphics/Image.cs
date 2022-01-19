using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
    public enum AnimType
    {
        AnimType_None,
        AnimType_Once,
        AnimType_PingPong,
        AnimType_Loop
    }

    public/*internal*/ class AnimInfo
    {
        public AnimInfo()
        {
            mAnimType = AnimType.AnimType_None;
            mFrameDelay = 1;
            mNumCels = 1;
            mTotalAnimTime = 0;
        }

        public AnimInfo(AnimInfo anotherAnim)
        {
            mAnimType = anotherAnim.mAnimType;
            mFrameDelay = anotherAnim.mFrameDelay;
            mNumCels = anotherAnim.mNumCels;
            mTotalAnimTime = anotherAnim.mTotalAnimTime;
            mPerFrameDelay = anotherAnim.mPerFrameDelay;
            mFrameMap = anotherAnim.mFrameMap;
        }

        public void Dispose()
        {
        }

        public void SetPerFrameDelay(int theFrame, int theTime)
        {
            if (mPerFrameDelay.Count <= theFrame)
            {
                mPerFrameDelay.Capacity = theFrame + 1;
            }
            mPerFrameDelay[theFrame] = theTime;
        }

        public void Compute(int theNumCels)
        {
            Compute(theNumCels, 0, 0);
        }

        public void Compute(int theNumCels, int theBeginFrameTime)
        {
            Compute(theNumCels, theBeginFrameTime, 0);
        }

        public void Compute(int theNumCels, int theBeginFrameTime, int theEndFrameTime)
        {
            mNumCels = theNumCels;
            if (mNumCels <= 0)
            {
                mNumCels = 1;
            }
            if (mFrameDelay <= 0)
            {
                mFrameDelay = 1;
            }
            if (mAnimType == AnimType.AnimType_PingPong && mNumCels > 1)
            {
                mFrameMap.Capacity = theNumCels * 2 - 2;
                int num = 0;
                for (int i = 0; i < theNumCels; i++)
                {
                    mFrameMap[num++] = i;
                }
                for (int i = theNumCels - 2; i >= 1; i--)
                {
                    mFrameMap[num++] = i;
                }
            }
            if (mFrameMap.Count != 0)
            {
                mNumCels = mFrameMap.Count;
            }
            if (theBeginFrameTime > 0)
            {
                SetPerFrameDelay(0, theBeginFrameTime);
            }
            if (theEndFrameTime > 0)
            {
                SetPerFrameDelay(mNumCels - 1, theEndFrameTime);
            }
            if (mPerFrameDelay.Count != 0)
            {
                mTotalAnimTime = 0;
                mPerFrameDelay.Capacity = mNumCels;
                for (int i = 0; i < mNumCels; i++)
                {
                    if (mPerFrameDelay[i] <= 0)
                    {
                        mPerFrameDelay[i] = mFrameDelay;
                    }
                    mTotalAnimTime += mPerFrameDelay[i];
                }
            }
            else
            {
                mTotalAnimTime = mFrameDelay * mNumCels;
            }
            if (mFrameMap.Count != 0)
            {
                mFrameMap.Capacity = mNumCels;
            }
        }

        public int GetPerFrameCel(int theTime)
        {
            for (int i = 0; i < mNumCels; i++)
            {
                theTime -= mPerFrameDelay[i];
                if (theTime < 0)
                {
                    return i;
                }
            }
            return mNumCels - 1;
        }

        public int GetCel(int theTime)
        {
            if (mAnimType == AnimType.AnimType_Once && theTime >= mTotalAnimTime)
            {
                if (mFrameMap.Count != 0)
                {
                    return mFrameMap[mFrameMap.Count - 1];
                }
                return mNumCels - 1;
            }
            else
            {
                theTime %= mTotalAnimTime;
                int num;
                if (mPerFrameDelay.Count != 0)
                {
                    num = GetPerFrameCel(theTime);
                }
                else
                {
                    num = theTime / mFrameDelay % mNumCels;
                }
                if (mFrameMap.Count == 0)
                {
                    return num;
                }
                return mFrameMap[num];
            }
        }

        public AnimType mAnimType;

        public int mFrameDelay;

        public int mNumCels;

        public List<int> mPerFrameDelay = new List<int>();

        public List<int> mFrameMap = new List<int>();

        public int mTotalAnimTime;
    }

    public/*internal*/ class Image
    {
        public int GetCelCount()
        {
            return mNumCols * mNumRows;
        }

        public int GetWidth()
        {
            return mWidth;
        }

        public int GetHeight()
        {
            return mHeight;
        }

        public int GetCelWidth()
        {
            if (celWidth == -1)
            {
                celWidth = mWidth / mNumCols;
            }
            return celWidth;
        }

        public int GetCelHeight()
        {
            if (celHeight == -1)
            {
                celHeight = mHeight / mNumRows;
            }
            return celHeight;
        }

        public int GetAnimCel(int theTime)
        {
            if (mAnimInfo == null)
            {
                return 0;
            }
            return mAnimInfo.GetCel(theTime);
        }

        public TRect GetAnimCelRect(int theTime)
        {
            int animCel = GetAnimCel(theTime);
            int num = GetCelWidth();
            int num2 = GetCelHeight();
            if (mNumCols > 1)
            {
                return new TRect(animCel * num, 0, num, mHeight);
            }
            return new TRect(0, animCel * num2, mWidth, num2);
        }

        public TRect GetCelRect(int theCel)
        {
            int num = GetCelHeight();
            int num2 = GetCelWidth();
            int theX = theCel % mNumCols * num2;
            int theY = theCel / mNumCols * num;
            return new TRect(theX, theY, num2, num);
        }

        public TRect GetCelRect(int theCol, int theRow)
        {
            int num = GetCelHeight();
            int num2 = GetCelWidth();
            int theX = theCol * num2;
            int theY = theRow * num;
            return new TRect(theX, theY, num2, num);
        }

        public void CopyAttributes(Image from)
        {
            mNumCols = from.mNumCols;
            mNumRows = from.mNumRows;
            mAnimInfo.Dispose();
            mAnimInfo = null;
            if (from.mAnimInfo != null)
            {
                mAnimInfo = new AnimInfo(from.mAnimInfo);
            }
        }

        public Image(Texture2D theTexture) : this(theTexture, 0, 0, theTexture.Width, theTexture.Height)
        {
        }

        public Image()
        {
            celWidth = -1;
            celHeight = -1;
            mWidth = 0;
            mHeight = 0;
            mNumRows = 1;
            mNumCols = 1;
            mAnimInfo = null;
            mS = (mT = 0);
            mMaxS = (mMaxT = 0f);
            mFormat = PixelFormat.kPixelFormat_Automatic;
            mTextureName = 0U;
            mParentWidth = mWidth;
            mParentHeight = mHeight;
            mOwnsTexture = false;
            mInAtlas = false;
        }

        public Image(Image theParent, int s, int t, int theWidth, int theHeight)
        {
            celWidth = -1;
            celHeight = -1;
            mNumRows = 1;
            mNumCols = 1;
            mAnimInfo = null;
            mTextureName = 0U;
            mParentImage = theParent;
            mS = s;
            mT = t;
            mMaxS = theParent.mMaxS;
            mMaxT = theParent.mMaxT;
            mFormat = theParent.mFormat;
            mWidth = theWidth;
            mHeight = theHeight;
            mParentWidth = theParent.mParentWidth;
            mParentHeight = theParent.mParentHeight;
            mOwnsTexture = false;
            mInAtlas = true;
            Texture = theParent.Texture;
            mInAtlas = false;
            mOwnsTexture = true;
        }

        public Image(Texture2D theTexture, int s, int t, int theWidth, int theHeight)
        {
            celWidth = -1;
            celHeight = -1;
            Reset(theTexture, s, t, theWidth, theHeight);
        }

        public Image(Texture2D theTexture, PixelFormat theFormat, float maxS, float maxT, int s, int t, int theWidth, int theHeight)
        {
            celWidth = -1;
            celHeight = -1;
            mNumRows = 1;
            mNumCols = 1;
            mAnimInfo = null;
            Texture = theTexture;
            mS = s;
            mT = t;
            mMaxS = maxS;
            mMaxT = maxT;
            mFormat = theFormat;
            mWidth = theWidth;
            mHeight = theHeight;
            mParentWidth = mWidth;
            mParentHeight = mHeight;
            mOwnsTexture = true;
            mInAtlas = false;
        }

        public void Reset(Texture2D theTexture, int s, int t, int theWidth, int theHeight)
        {
            mNumRows = 1;
            mNumCols = 1;
            mAnimInfo = null;
            Texture = theTexture;
            mS = s;
            mT = t;
            mWidth = theWidth;
            mHeight = theHeight;
            mParentWidth = mWidth;
            mParentHeight = mHeight;
            mOwnsTexture = true;
            mInAtlas = false;
        }

        public void Reset(Texture2D theTexture)
        {
            Reset(theTexture, 0, 0, theTexture.Width, theTexture.Height);
        }

        public static Image FromMemory(ushort[] info, int width, int height)
        {
            Image image = new Image();
            image.mFormat = PixelFormat.kPixelFormat_RGB565;
            image.mWidth = width;
            image.mHeight = height;
            image.mOwnsTexture = true;
            image.mInAtlas = false;
            image.Texture = new Texture2D(GlobalStaticVars.g.GraphicsDevice, width, height, false, SurfaceFormat.Bgr565);
            image.Texture.SetData<ushort>(info);
            return image;
        }

        public virtual void Dispose()
        {
            if (Texture != null)
            {
                Texture.Dispose();
                Texture = null;
            }
        }

        public static explicit operator Image(Texture2D aTexture)
        {
            return new Image(aTexture);
        }

        public static implicit operator Texture2D(Image anImage)
        {
            return anImage.Texture;
        }

        public int mWidth;

        public int mHeight;

        public int mNumRows;

        public int mNumCols;

        public AnimInfo mAnimInfo;

        public Texture2D Texture;

        public uint mTextureName;

        public float mMaxS;

        public float mMaxT;

        public int mS;

        public int mT;

        public PixelFormat mFormat;

        public bool mInAtlas;

        private Image mParentImage;

        protected int mParentWidth;

        protected int mParentHeight;

        protected bool mOwnsTexture;

        private int celWidth;

        private int celHeight;
    }
}
