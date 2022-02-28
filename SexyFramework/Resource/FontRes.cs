using System;
using Sexy.GraphicsLib;

namespace Sexy.Resource
{
	public class FontRes : BaseRes
	{
		public FontRes()
		{
			this.mType = ResType.ResType_Font;
			this.mSysFont = false;
			this.mFont = null;
			this.mImage = null;
		}

		public override void DeleteResource()
		{
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				this.mResourceRef.Release();
			}
			if (this.mFont != null)
			{
				this.mFont.Dispose();
				this.mFont = null;
			}
			if (this.mImage != null)
			{
				this.mImage.Dispose();
				this.mImage = null;
			}
			if (this.mGlobalPtr != null)
			{
				this.mGlobalPtr.mResObject = null;
			}
		}

		public override void ApplyConfig()
		{
			if (this.mFont == null)
			{
				return;
			}
			if (!this.mSysFont)
			{
				ImageFont imageFont = (ImageFont)this.mFont;
				if (this.mTags.Length > 0)
				{
					this.mTags.ToCharArray();
					string[] array = this.mTags.Split(new char[] { ',', ' ', '\r', '\t', '\n' });
					foreach (string theTagName in array)
					{
						imageFont.AddTag(theTagName);
					}
					imageFont.Prepare();
				}
			}
		}

		public Font mFont;

		public Image mImage;

		public string mImagePath;

		public string mTags = "";

		public bool mSysFont;

		public bool mBold;

		public bool mItalic;

		public bool mUnderline;

		public bool mShadow;

		public int mSize;
	}
}
