using System;

namespace Sexy.GraphicsLib
{
	public class SharedImageRef
	{
		public int mWidth
		{
			get
			{
				if (this.mSharedImage != null)
				{
					return this.mSharedImage.mImage.mWidth;
				}
				return 0;
			}
		}

		public int mHeight
		{
			get
			{
				if (this.mSharedImage != null)
				{
					return this.mSharedImage.mImage.mHeight;
				}
				return 0;
			}
		}

		public SharedImageRef()
		{
			this.mSharedImage = new SharedImage();
			this.mUnsharedImage = null;
			this.mOwnsUnshared = false;
		}

		public SharedImageRef(SharedImageRef theSharedImageRef)
		{
			this.mSharedImage = theSharedImageRef.mSharedImage;
			if (this.mSharedImage != null)
			{
				this.mSharedImage.mRefCount++;
			}
			this.mUnsharedImage = theSharedImageRef.mUnsharedImage;
			this.mOwnsUnshared = false;
		}

		public SharedImageRef(SharedImage theSharedImage)
		{
			this.mSharedImage = theSharedImage;
			if (theSharedImage != null)
			{
				this.mSharedImage.mRefCount++;
			}
			this.mUnsharedImage = null;
			this.mOwnsUnshared = false;
		}

		public virtual void Dispose()
		{
			this.Release();
		}

		public void CopyFrom(SharedImageRef theSharedImageRef)
		{
			this.Release();
			this.mSharedImage = theSharedImageRef.mSharedImage;
			this.mUnsharedImage = theSharedImageRef.mUnsharedImage;
			if (this.mSharedImage != null)
			{
				this.mSharedImage.mRefCount++;
			}
		}

		public void CopyFrom(SharedImage theSharedImage)
		{
			this.Release();
			this.mSharedImage = theSharedImage;
			this.mSharedImage.mRefCount++;
		}

		public void CopyFrom(MemoryImage theUnsharedImage)
		{
			this.Release();
			this.mUnsharedImage = theUnsharedImage;
		}

		public void Release()
		{
			if (this.mOwnsUnshared)
			{
				if (this.mUnsharedImage != null)
				{
					this.mUnsharedImage.Dispose();
				}
				this.mUnsharedImage = null;
			}
			if (this.mSharedImage != null && --this.mSharedImage.mRefCount == 0)
			{
				GlobalMembers.gSexyAppBase.mCleanupSharedImages = true;
			}
			this.mSharedImage = null;
		}

		internal DeviceImage GetDeviceImage()
		{
			if (this.mSharedImage != null)
			{
				return this.mSharedImage.mImage;
			}
			return null;
		}

		public Image GetImage()
		{
			return this.GetMemoryImage();
		}

		public MemoryImage GetMemoryImage()
		{
			if (this.mUnsharedImage != null)
			{
				return this.mUnsharedImage;
			}
			return this.GetDeviceImage();
		}

		public SharedImage mSharedImage;

		public MemoryImage mUnsharedImage;

		public bool mOwnsUnshared;
	}
}
