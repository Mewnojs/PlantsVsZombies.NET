using System;

namespace Sexy.TodLib
{
	public/*internal*/ class ReanimAtlas
	{
		public ReanimAtlas()
		{
			mImageCount = 0;
		}

		public void ReanimAtlasCreate(ReanimatorDefinition theReanimDef)
		{
		}

		public void ReanimAtlasDispose()
		{
		}

		public void AddImage(Image theImage)
		{
		}

		public int FindImage(Image theImage)
		{
			return -1;
		}

		public bool ImageFits(int theImageCount, TRect rectTest, int theMaxWidth)
		{
			return true;
		}

		public bool ImageFindPlaceOnSide(ReanimAtlasImage theAtlasImageToPlace, int theImageCount, int theMaxWidth, bool theToRight)
		{
			return false;
		}

		public bool ImageFindPlace(ReanimAtlasImage theAtlasImage, int theImageIndex, int theMaxWidth)
		{
			return false;
		}

		public bool PlaceAtlasImage(ReanimAtlasImage theAtlasImage, int theImageIndex, int theMaxWidth)
		{
			return true;
		}

		public int PickAtlasWidth()
		{
			return 0;
		}

		public void ArrangeImages(ref int theAtlasWidth, ref int theAtlasHeight)
		{
		}

		public ReanimAtlasImage GetEncodedReanimAtlas(Image theImage)
		{
			return null;
		}

		public ReanimAtlasImage[] mImageArray = new ReanimAtlasImage[64];

		public int mImageCount;

		public MemoryImage mMemoryImage;
	}
}
