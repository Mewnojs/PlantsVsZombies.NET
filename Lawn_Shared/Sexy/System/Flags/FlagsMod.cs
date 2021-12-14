using System;

namespace Sexy
{
	public struct FlagsMod
	{
		public void CopyFrom(FlagsMod source)
		{
			mAddFlags = source.mAddFlags;
			mRemoveFlags = source.mRemoveFlags;
		}

		public int mAddFlags;

		public int mRemoveFlags;
	}
}
