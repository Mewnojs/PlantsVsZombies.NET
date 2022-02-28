using System;
using System.Collections.Generic;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class FontLayer
	{
		public FontLayer()
		{
			this.mFontData = null;
			this.mDrawMode = -1;
			this.mSpacing = 0;
			this.mPointSize = 0;
			this.mAscent = 0;
			this.mAscentPadding = 0;
			this.mMinPointSize = -1;
			this.mMaxPointSize = -1;
			this.mHeight = 0;
			this.mDefaultHeight = 0;
			this.mColorMult = new SexyColor(SexyColor.White);
			this.mColorAdd = new SexyColor(0, 0, 0, 0);
			this.mLineSpacingOffset = 0;
			this.mBaseOrder = 0;
			this.mImageIsWhite = false;
			this.mUseAlphaCorrection = true;
			this.mCharDataHashTable.mOrderedHash = ImageFont.mOrderedHash;
		}

		public FontLayer(FontData theFontData)
		{
			this.mFontData = theFontData;
			this.mDrawMode = -1;
			this.mSpacing = 0;
			this.mPointSize = 0;
			this.mAscent = 0;
			this.mAscentPadding = 0;
			this.mMinPointSize = -1;
			this.mMaxPointSize = -1;
			this.mHeight = 0;
			this.mDefaultHeight = 0;
			this.mColorMult = new SexyColor(SexyColor.White);
			this.mColorAdd = new SexyColor(0, 0, 0, 0);
			this.mLineSpacingOffset = 0;
			this.mBaseOrder = 0;
			this.mImageIsWhite = false;
			this.mUseAlphaCorrection = true;
			this.mCharDataHashTable.mOrderedHash = ImageFont.mOrderedHash;
		}

		public FontLayer(FontLayer theFontLayer)
		{
			this.mFontData = theFontLayer.mFontData;
			this.mRequiredTags = theFontLayer.mRequiredTags;
			this.mExcludedTags = theFontLayer.mExcludedTags;
			this.mImage = new SharedImageRef(theFontLayer.mImage);
			this.mImageIsWhite = theFontLayer.mImageIsWhite;
			this.mDrawMode = theFontLayer.mDrawMode;
			this.mOffset = theFontLayer.mOffset;
			this.mSpacing = theFontLayer.mSpacing;
			this.mMinPointSize = theFontLayer.mMinPointSize;
			this.mMaxPointSize = theFontLayer.mMaxPointSize;
			this.mPointSize = theFontLayer.mPointSize;
			this.mAscent = theFontLayer.mAscent;
			this.mAscentPadding = theFontLayer.mAscentPadding;
			this.mHeight = theFontLayer.mHeight;
			this.mDefaultHeight = theFontLayer.mDefaultHeight;
			this.mColorMult = new SexyColor(theFontLayer.mColorMult);
			this.mColorAdd = new SexyColor(theFontLayer.mColorAdd);
			this.mLineSpacingOffset = theFontLayer.mLineSpacingOffset;
			this.mBaseOrder = theFontLayer.mBaseOrder;
			this.mExtendedInfo = theFontLayer.mExtendedInfo;
			this.mKerningData = theFontLayer.mKerningData;
			this.mCharDataHashTable = theFontLayer.mCharDataHashTable;
			this.mUseAlphaCorrection = theFontLayer.mUseAlphaCorrection;
			this.mLayerName = theFontLayer.mLayerName;
		}

		public CharData GetCharData(char theChar)
		{
			return this.mCharDataHashTable.GetCharData(theChar, true);
		}

		public string mLayerName;

		public FontData mFontData;

		public Dictionary<string, string> mExtendedInfo = new Dictionary<string, string>();

		public List<string> mRequiredTags = new List<string>();

		public List<string> mExcludedTags = new List<string>();

		public List<FontLayer.KerningValue> mKerningData = new List<FontLayer.KerningValue>();

		public CharDataHashTable mCharDataHashTable = new CharDataHashTable();

		public SexyColor mColorMult = default(SexyColor);

		public SexyColor mColorAdd = default(SexyColor);

		public SharedImageRef mImage = new SharedImageRef();

		public bool mImageIsWhite;

		public string mImageFileName;

		public int mDrawMode;

		public SexyPoint mOffset = new SexyPoint();

		public int mSpacing;

		public int mMinPointSize;

		public int mMaxPointSize;

		public int mPointSize;

		public int mAscent;

		public int mAscentPadding;

		public int mHeight;

		public int mDefaultHeight;

		public int mLineSpacingOffset;

		public int mBaseOrder;

		public bool mUseAlphaCorrection;

		public struct KerningValue
		{
			public int mInt;

			public ushort mChar;

			public short mOffset;
		}
	}
}
