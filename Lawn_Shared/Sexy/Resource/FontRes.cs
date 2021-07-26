using System;

namespace Sexy
{
	internal class FontRes : BaseRes
	{
		public FontRes()
		{
			this.mType = ResType.ResType_Font;
			this.mDefault = false;
		}

		public override void DeleteResource()
		{
			if (this.mFont != null)
			{
				this.mFont.Dispose();
				this.mFont = null;
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
