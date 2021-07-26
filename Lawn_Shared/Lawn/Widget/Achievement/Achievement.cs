using System;

namespace Lawn
{
	internal class Achievement
	{
		public Achievement(int aImageId, string aName, string aDesc)
		{
			this.mImageId = aImageId;
			this.mName = aName;
			this.mDesc = aDesc;
		}

		public int mImageId;

		public readonly string mName;

		public readonly string mDesc;
	}
}
