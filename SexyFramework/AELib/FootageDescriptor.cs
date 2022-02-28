using System;

namespace Sexy.AELib
{
	internal class FootageDescriptor
	{
		public FootageDescriptor()
		{
			this.mShortName = "";
			this.mId = -1L;
			this.mWidth = 0L;
			this.mHeight = 0L;
			this.mFullName = "";
		}

		public FootageDescriptor(string sn, long id, string fn, long w, long h)
		{
			this.mShortName = sn;
			this.mId = id;
			this.mFullName = fn;
			this.mWidth = w;
			this.mHeight = h;
		}

		public string mShortName;

		public long mId;

		public long mWidth;

		public long mHeight;

		public string mFullName;
	}
}
