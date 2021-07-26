using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	internal class Image
	{
		public int GetCelCount()
		{
			return this.mNumCols * this.mNumRows;
		}

		public int GetWidth()
		{
			return this.mWidth;
		}

		public int GetHeight()
		{
			return this.mHeight;
		}

		public int GetCelWidth()
		{
			if (this.celWidth == -1)
			{
				this.celWidth = this.mWidth / this.mNumCols;
			}
			return this.celWidth;
		}

		public int GetCelHeight()
		{
			if (this.celHeight == -1)
			{
				this.celHeight = this.mHeight / this.mNumRows;
			}
			return this.celHeight;
		}

		public int GetAnimCel(int theTime)
		{
			if (this.mAnimInfo == null)
			{
				return 0;
			}
			return this.mAnimInfo.GetCel(theTime);
		}

		public TRect GetAnimCelRect(int theTime)
		{
			int animCel = this.GetAnimCel(theTime);
			int num = this.GetCelWidth();
			int num2 = this.GetCelHeight();
			if (this.mNumCols > 1)
			{
				return new TRect(animCel * num, 0, num, this.mHeight);
			}
			return new TRect(0, animCel * num2, this.mWidth, num2);
		}

		public TRect GetCelRect(int theCel)
		{
			int num = this.GetCelHeight();
			int num2 = this.GetCelWidth();
			int theX = theCel % this.mNumCols * num2;
			int theY = theCel / this.mNumCols * num;
			return new TRect(theX, theY, num2, num);
		}

		public TRect GetCelRect(int theCol, int theRow)
		{
			int num = this.GetCelHeight();
			int num2 = this.GetCelWidth();
			int theX = theCol * num2;
			int theY = theRow * num;
			return new TRect(theX, theY, num2, num);
		}

		public void CopyAttributes(Image from)
		{
			this.mNumCols = from.mNumCols;
			this.mNumRows = from.mNumRows;
			this.mAnimInfo.Dispose();
			this.mAnimInfo = null;
			if (from.mAnimInfo != null)
			{
				this.mAnimInfo = new AnimInfo(from.mAnimInfo);
			}
		}

		public Image(Texture2D theTexture) : this(theTexture, 0, 0, theTexture.Width, theTexture.Height)
		{
		}

		public Image()
		{
			this.celWidth = -1;
			this.celHeight = -1;
			base..ctor();
			this.mWidth = 0;
			this.mHeight = 0;
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.mS = (this.mT = 0);
			this.mMaxS = (this.mMaxT = 0f);
			this.mFormat = PixelFormat.kPixelFormat_Automatic;
			this.mTextureName = 0U;
			this.mParentWidth = this.mWidth;
			this.mParentHeight = this.mHeight;
			this.mOwnsTexture = false;
			this.mInAtlas = false;
		}

		public Image(Image theParent, int s, int t, int theWidth, int theHeight)
		{
			this.celWidth = -1;
			this.celHeight = -1;
			base..ctor();
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.mTextureName = 0U;
			this.mParentImage = theParent;
			this.mS = s;
			this.mT = t;
			this.mMaxS = theParent.mMaxS;
			this.mMaxT = theParent.mMaxT;
			this.mFormat = theParent.mFormat;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mParentWidth = theParent.mParentWidth;
			this.mParentHeight = theParent.mParentHeight;
			this.mOwnsTexture = false;
			this.mInAtlas = true;
			this.Texture = theParent.Texture;
			this.mInAtlas = false;
			this.mOwnsTexture = true;
		}

		public Image(Texture2D theTexture, int s, int t, int theWidth, int theHeight)
		{
			this.celWidth = -1;
			this.celHeight = -1;
			base..ctor();
			this.Reset(theTexture, s, t, theWidth, theHeight);
		}

		public Image(Texture2D theTexture, PixelFormat theFormat, float maxS, float maxT, int s, int t, int theWidth, int theHeight)
		{
			this.celWidth = -1;
			this.celHeight = -1;
			base..ctor();
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.Texture = theTexture;
			this.mS = s;
			this.mT = t;
			this.mMaxS = maxS;
			this.mMaxT = maxT;
			this.mFormat = theFormat;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mParentWidth = this.mWidth;
			this.mParentHeight = this.mHeight;
			this.mOwnsTexture = true;
			this.mInAtlas = false;
		}

		public void Reset(Texture2D theTexture, int s, int t, int theWidth, int theHeight)
		{
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.Texture = theTexture;
			this.mS = s;
			this.mT = t;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mParentWidth = this.mWidth;
			this.mParentHeight = this.mHeight;
			this.mOwnsTexture = true;
			this.mInAtlas = false;
		}

		public void Reset(Texture2D theTexture)
		{
			this.Reset(theTexture, 0, 0, theTexture.Width, theTexture.Height);
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
			if (this.Texture != null)
			{
				this.Texture.Dispose();
				this.Texture = null;
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
