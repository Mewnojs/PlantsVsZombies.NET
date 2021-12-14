using System;

namespace Sexy
{
	internal class FontRes : BaseRes
	{
		public FontRes()
		{
			mType = ResType.ResType_Font;
			mDefault = false;
		}

		public override void DeleteResource()
		{
			if (mFont != null)
			{
				mFont.Dispose();
				mFont = null;
			}
			base.DeleteResource();
		}

		public Font mFont;

		public string mTags;

		public bool mDefault;

		public bool mSysFont;

		public bool mBold;

		public bool mItalic;

		public bool mUnderline;

		public bool mShadow;

		public int mSize;
	}
}
