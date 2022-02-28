using System;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.Resource
{
	public class ImageRes : BaseRes
	{
		public ImageRes()
		{
			this.mType = ResType.ResType_Image;
			this.mAtlasName = null;
		}

		public override void DeleteResource()
		{
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				this.mResourceRef.Release();
			}
			if (this.mGlobalPtr != null)
			{
				this.mGlobalPtr.mResObject = null;
			}
			this.mImage.Release();
		}

		public override void ApplyConfig()
		{
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				return;
			}
			DeviceImage deviceImage = this.mImage.GetDeviceImage();
			if (deviceImage == null)
			{
				return;
			}
			deviceImage.ReplaceImageFlags(0U);
			if (this.mNoTriRep)
			{
				deviceImage.AddImageFlags(ImageFlags.ImageFlag_NoTriRep);
			}
			deviceImage.mNumRows = this.mRows;
			deviceImage.mNumCols = this.mCols;
			if (this.mDither16)
			{
				deviceImage.mDither16 = true;
			}
			if (this.mA4R4G4B4)
			{
				deviceImage.AddImageFlags(ImageFlags.ImageFlag_UseA4R4G4B4);
			}
			if (this.mA8R8G8B8)
			{
				deviceImage.AddImageFlags(ImageFlags.ImageFlag_UseA8R8G8B8);
			}
			if (this.mMinimizeSubdivisions)
			{
				deviceImage.AddImageFlags(ImageFlags.ImageFlag_MinimizeNumSubdivisions);
			}
			if (this.mCubeMap)
			{
				deviceImage.AddImageFlags(ImageFlags.ImageFlag_CubeMap);
			}
			else if (this.mVolumeMap)
			{
				deviceImage.AddImageFlags(ImageFlags.ImageFlag_VolumeMap);
			}
			if (this.mAnimInfo.mAnimType != AnimType.AnimType_None)
			{
				deviceImage.mAnimInfo = new AnimInfo(this.mAnimInfo);
			}
			if (this.mIsAtlas)
			{
				deviceImage.AddImageFlags(513U);
			}
			if (this.mAtlasName != null)
			{
				deviceImage.mAtlasImage = GlobalMembers.gSexyAppBase.mResourceManager.LoadImage(this.mAtlasName).GetImage();
				deviceImage.mAtlasStartX = this.mAtlasX;
				deviceImage.mAtlasStartY = this.mAtlasY;
				deviceImage.mAtlasEndX = this.mAtlasX + this.mAtlasW;
				deviceImage.mAtlasEndY = this.mAtlasY + this.mAtlasH;
			}
			deviceImage.CommitBits();
			deviceImage.mPurgeBits = this.mPurgeBits;
			if (this.mDDSurface)
			{
				deviceImage.CommitBits();
				if (!deviceImage.mHasAlpha)
				{
					deviceImage.mWantDeviceSurface = true;
					deviceImage.mPurgeBits = true;
				}
			}
			if (deviceImage.mPurgeBits)
			{
				new AutoCrit(GlobalMembers.gSexyAppBase.mImageSetCritSect);
				deviceImage.PurgeBits();
			}
		}

		public SharedImageRef mImage = new SharedImageRef();

		public string mAlphaImage = "";

		public string mAlphaGridImage = "";

		public string mVariant = "";

		public SexyPoint mOffset;

		public bool mAutoFindAlpha;

		public bool mPalletize;

		public bool mA4R4G4B4;

		public bool mA8R8G8B8;

		public bool mDither16;

		public bool mDDSurface;

		public bool mPurgeBits;

		public bool mMinimizeSubdivisions;

		public bool mCubeMap;

		public bool mVolumeMap;

		public bool mNoTriRep;

		public bool m2DBig;

		public bool mIsAtlas;

		public int mRows = 1;

		public int mCols = 1;

		public int mAlphaColor;

		public AnimInfo mAnimInfo = new AnimInfo();

		public string mAtlasName;

		public int mAtlasX;

		public int mAtlasY;

		public int mAtlasW;

		public int mAtlasH;
	}
}
