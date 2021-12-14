using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
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
