using System;

namespace Sexy.GraphicsLib
{
	public class SharedImage
	{
		public string ToString()
		{
			if (string.Concat(new object[] { "RefCount(", this.mRefCount, "):", this.mImage }) == null)
			{
				return "NULL";
			}
			return this.mImage.ToString();
		}

		public DeviceImage mImage;

		public int mRefCount;

		public bool mLoading;
	}
}
