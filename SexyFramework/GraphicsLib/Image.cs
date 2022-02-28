using System;
using Microsoft.Xna.Framework;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class Image : IDisposable
	{
		public Image()
		{
			this.mImageFlags = ImageFlags.ImageFlag_NONE;
			this.mRenderData = null;
			this.mAtlasImage = null;
			this.mAtlasStartX = 0;
			this.mAtlasStartY = 0;
			this.mAtlasEndX = 0;
			this.mAtlasEndY = 0;
			this.mWidth = 0;
			this.mHeight = 0;
			this.mNumRows = 1;
			this.mNumCols = 1;
			this.mAnimInfo = null;
			this.mDrawn = false;
		}

		public Image(Image theImage)
		{
			this.mImageFlags = theImage.mImageFlags;
			this.mRenderData = null;
			this.mWidth = theImage.mWidth;
			this.mHeight = theImage.mHeight;
			this.mNumRows = theImage.mNumRows;
			this.mNumCols = theImage.mNumCols;
			this.mAtlasImage = theImage.mAtlasImage;
			this.mAtlasStartX = theImage.mAtlasStartX;
			this.mAtlasStartY = theImage.mAtlasStartY;
			this.mAtlasEndX = theImage.mAtlasEndX;
			this.mAtlasEndY = theImage.mAtlasEndY;
			this.mDrawn = false;
			if (theImage.mAnimInfo != null)
			{
				this.mAnimInfo = theImage.mAnimInfo;
				return;
			}
			this.mAnimInfo = null;
		}

		public void InitAtalasState()
		{
			if (this.mAtlasValidate)
			{
				return;
			}
			if (this.mAtlasImage != null)
			{
				float num = (float)this.mAtlasStartX / (float)this.mAtlasImage.mWidth;
				float num2 = (float)this.mAtlasStartY / (float)this.mAtlasImage.mHeight;
				float num3 = (float)this.mAtlasEndX / (float)this.mAtlasImage.mWidth;
				float num4 = (float)this.mAtlasEndY / (float)this.mAtlasImage.mHeight;
				this.mVectorBase = new Vector2(num, num2);
				if (num4 < num2)
				{
					this.mVectorU = new Vector2(num, num4) - this.mVectorBase;
					this.mVectorV = new Vector2(num3, num2) - this.mVectorBase;
				}
				else
				{
					this.mVectorU = new Vector2(num3, num2) - this.mVectorBase;
					this.mVectorV = new Vector2(num, num4) - this.mVectorBase;
				}
			}
			this.mAtlasValidate = true;
		}

		public virtual void Dispose()
		{
			if (this.mAnimInfo != null)
			{
				this.mAnimInfo.Dispose();
			}
			this.mAnimInfo = null;
		}

		public virtual MemoryImage AsMemoryImage()
		{
			return null;
		}

		public virtual DeviceImage AsDeviceImage()
		{
			return null;
		}

		public int GetWidth()
		{
			return this.mWidth;
		}

		public int GetHeight()
		{
			return this.mHeight;
		}

		public Rect GetRect()
		{
			return new Rect(0, 0, this.mWidth, this.mHeight);
		}

		public int GetCelWidth()
		{
			if (this.mCelWidth == -1)
			{
				this.mCelWidth = this.mWidth / this.mNumCols;
			}
			return this.mCelWidth;
		}

		public int GetCelHeight()
		{
			if (this.mCelHeight == -1)
			{
				this.mCelHeight = this.mHeight / this.mNumRows;
			}
			return this.mCelHeight;
		}

		public int GetCelCount()
		{
			return this.mNumCols * this.mNumRows;
		}

		public int GetAnimCel(int theTime)
		{
			if (this.mAnimInfo == null)
			{
				return 0;
			}
			return this.mAnimInfo.GetCel(theTime);
		}

		public Rect GetAnimCelRect(int theTime)
		{
			int animCel = this.GetAnimCel(theTime);
			int celWidth = this.GetCelWidth();
			int celHeight = this.GetCelHeight();
			if (this.mNumCols > 1)
			{
				return new Rect(animCel * celWidth, 0, celWidth, this.mHeight);
			}
			return new Rect(0, animCel * celHeight, this.mWidth, celHeight);
		}

		public Rect GetCelRect(int theCel)
		{
			this.mCelRect.mHeight = this.GetCelHeight();
			this.mCelRect.mWidth = this.GetCelWidth();
			this.mCelRect.mX = theCel % this.mNumCols * this.mCelRect.mWidth;
			this.mCelRect.mY = theCel / this.mNumCols * this.mCelRect.mHeight;
			return this.mCelRect;
		}

		public Rect GetCelRect(int theCol, int theRow)
		{
			this.mCelRect.mHeight = this.GetCelHeight();
			this.mCelRect.mWidth = this.GetCelWidth();
			this.mCelRect.mX = theCol * this.mCelRect.mWidth;
			this.mCelRect.mY = theRow * this.mCelRect.mHeight;
			return this.mCelRect;
		}

		public void CopyAttributes(Image from)
		{
			this.mNumCols = from.mNumCols;
			this.mNumRows = from.mNumRows;
			this.mAnimInfo = null;
			if (from.mAnimInfo != null)
			{
				this.mAnimInfo = new AnimInfo(from.mAnimInfo);
			}
		}

		public void ReplaceImageFlags(uint inFlags)
		{
			this.mImageFlags = (ImageFlags)inFlags;
		}

		public void AddImageFlags(uint inFlags)
		{
			this.mImageFlags |= (ImageFlags)inFlags;
		}

		public void RemoveImageFlags(uint inFlags)
		{
			this.mImageFlags &= (ImageFlags)(~(ImageFlags)inFlags);
		}

		public bool HasImageFlag(uint inFlag)
		{
			return (this.mImageFlags & (ImageFlags)inFlag) != ImageFlags.ImageFlag_NONE;
		}

		public ulong GetImageFlags()
		{
			return (ulong)((long)this.mImageFlags);
		}

		public object GetRenderData()
		{
			return this.mRenderData;
		}

		public void SetRenderData(object inRenderData)
		{
			this.mRenderData = inRenderData;
		}

		public bool CreateRenderData()
		{
			MemoryImage memoryImage = this.AsMemoryImage();
			if (memoryImage != null && GlobalMembers.gSexyAppBase.mGraphicsDriver != null && GlobalMembers.gSexyAppBase.mGraphicsDriver.GetRenderDevice3D() != null)
			{
				this.mRenderData = GlobalMembers.gSexyAppBase.mAppDriver.GetOptimizedRenderData(memoryImage.mFileName);
				if (this.mRenderData != null)
				{
					return true;
				}
			}
			return false;
		}

		protected ImageFlags mImageFlags;

		public object mRenderData;

		public string mFileName;

		public string mNameForRes = "";

		public bool mDrawn;

		public string mFilePath;

		public int mWidth;

		public int mHeight;

		public Rect mCelRect = default(Rect);

		public Rect mRect = Rect.ZERO_RECT;

		public int mNumRows = 1;

		public int mNumCols = 1;

		public int mCelWidth = -1;

		public int mCelHeight = -1;

		public AnimInfo mAnimInfo;

		public Image mAtlasImage;

		public int mAtlasStartX;

		public int mAtlasStartY;

		public int mAtlasEndX;

		public int mAtlasEndY;

		public Vector2 mVectorU = new Vector2(1f, 0f);

		public Vector2 mVectorV = new Vector2(0f, 1f);

		public Vector2 mVectorBase = new Vector2(0f, 0f);

		public bool mAtlasValidate;
	}
}
