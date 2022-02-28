using System;
using Sexy.Drivers;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class DeviceImage : MemoryImage
	{
		protected void DeleteAllNonSurfaceData()
		{
			this.mBits = null;
			this.mBits = null;
			this.mNativeAlphaData = null;
			this.mNativeAlphaData = null;
			this.mRLAdditiveData = null;
			this.mRLAdditiveData = null;
			this.mRLAlphaData = null;
			this.mRLAlphaData = null;
			this.mColorTable = null;
			this.mColorTable = null;
			this.mColorIndices = null;
			this.mColorIndices = null;
		}

		private void Init()
		{
			this.mSurface = null;
			this.mNoLock = false;
			this.mWantDeviceSurface = false;
			this.mDrawToBits = false;
			this.mSurfaceSet = false;
			this.mLockCount = 0;
		}

		public override DeviceImage AsDeviceImage()
		{
			return this;
		}

		public bool GenerateDeviceSurface()
		{
			return this.mSurface != null && false;
		}

		public void DeleteDeviceSurface()
		{
			if (this.mSurface != null)
			{
				if (this.mColorTable == null && this.mBits == null && base.GetRenderData() == null)
				{
					base.GetBits();
				}
				this.mSurface = null;
				this.mSurface = null;
			}
		}

		public void ReInit()
		{
			if (this.mWantDeviceSurface)
			{
				this.GenerateDeviceSurface();
			}
		}

		public override void BitsChanged()
		{
			this.mSurface = null;
			this.mSurface = null;
		}

		public void CommitBits()
		{
			DeviceSurface deviceSurface = this.mSurface;
		}

		public virtual bool LockSurface()
		{
			return true;
		}

		public virtual bool UnlockSurface()
		{
			return true;
		}

		public virtual void SetSurface(IntPtr theSurface)
		{
			this.mSurfaceSet = true;
			if (this.mSurface != null)
			{
				int version = this.mDriver.GetVersion();
				if (this.mSurface.GetVersion() != version)
				{
					this.mSurface = null;
					this.mSurface = null;
				}
			}
			DeviceSurface deviceSurface = this.mSurface;
			this.mSurface.SetSurface(theSurface);
			this.mSurface.GetDimensions(this.mWidth, this.mHeight);
			this.mNoLock = false;
		}

		public override void Create(int theWidth, int theHeight)
		{
			base.Create(theWidth, theHeight);
			this.mBits = null;
			this.mWidth = theWidth;
			this.mHeight = theHeight;
			this.mBits = null;
			this.BitsChanged();
		}

		public void BltF(Image theImage, float theX, float theY, Rect theSrcRect, Rect theClipRect, SexyColor theColor, int theDrawMode)
		{
			theImage.mDrawn = true;
			this.CommitBits();
		}

		public void BltRotated(Image theImage, float theX, float theY, Rect theSrcRect, Rect theClipRect, SexyColor theColor, int theDrawMode, double theRot, float theRotCenterX, float theRotCenterY)
		{
		}

		public void BltStretched(Image theImage, Rect theDestRectOrig, Rect theSrcRectOrig, Rect theClipRect, SexyColor theColor, int theDrawMode, int fastStretch)
		{
		}

		public void BltStretched(Image theImage, Rect theDestRectOrig, Rect theSrcRectOrig, Rect theClipRect, SexyColor theColor, int theDrawMode, int fastStretch, int mirror)
		{
		}

		public override bool Palletize()
		{
			return false;
		}

		public override void PurgeBits()
		{
			if (this.mSurfaceSet)
			{
				return;
			}
			this.mPurgeBits = true;
			this.mBits = null;
			this.mBits = null;
			this.mColorIndices = null;
			this.mColorIndices = null;
			this.mColorTable = null;
			this.mColorTable = null;
		}

		public void DeleteNativeData()
		{
			if (this.mSurfaceSet)
			{
				return;
			}
			this.DeleteDeviceSurface();
		}

		public void DeleteExtraBuffers()
		{
			if (this.mSurfaceSet)
			{
				return;
			}
			this.DeleteDeviceSurface();
		}

		public static int CheckCache(string theSrcFile, string theAltData)
		{
			return 0;
		}

		public static int SetCacheUpToDate(string theSrcFile, string theAltData)
		{
			return 0;
		}

		public static DeviceImage ReadFromCache(string theSrcFile, string theAltData)
		{
			return null;
		}

		public virtual void WriteToCache(string theSrcFile, string theAltData)
		{
		}

		internal void AddImageFlags(ImageFlags imageFlags)
		{
			this.mImageFlags |= imageFlags;
		}

		public IGraphicsDriver mDriver;

		public bool mSurfaceSet;

		public bool mNoLock;

		public bool mWantDeviceSurface;

		public bool mDrawToBits;

		public int mLockCount;

		public DeviceSurface mSurface;
	}
}
