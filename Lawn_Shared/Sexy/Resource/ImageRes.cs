using System;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	internal class ImageRes : BaseRes
	{
		public ImageRes()
		{
			mType = ResType.ResType_Image;
		}

		public override void DeleteResource()
		{
			if (mImage != null)
			{
				mImage.Dispose();
				if (mFormat == ImageRes.TextureFormat.Content && mUnloadGroup > 0)
				{
					ResourceManager.mUnloadContentManager[mUnloadGroup].Unload();
				}
				mImage = null;
			}
			base.DeleteResource();
		}

		public Image mImage;

		public string mAlphaImage;

		public string mAlphaGridImage;

		public string mVariant;

		public bool mAutoFindAlpha;

		public bool mPalletize;

		public bool mA4R4G4B4;

		public bool mA8R8G8B8;

		public bool mR5G6B5;

		public bool mA1R5G5B5;

		public bool mDDSurface;

		public bool mPurgeBits;

		public bool mMinimizeSubdivisions;

		public int mRows;

		public int mCols;

		public uint mAlphaColor;

		public AnimInfo mAnimInfo = new AnimInfo();

		public SurfaceFormat lowMemorySurfaceFormat = SurfaceFormat.Bgra4444;

		public bool mLanguageSpecific;

		public ImageRes.TextureFormat mFormat = ImageRes.TextureFormat.Png;

		public enum TextureFormat
		{
			Content,
			Png,
			Jpg
		}
	}
}
