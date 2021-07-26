using System;

namespace Sexy
{
	public struct FlagsMod
	{
		public void CopyFrom(FlagsMod source)
		{
			this.mAddFlags = source.mAddFlags;
			this.mRemoveFlags = source.mRemoveFlags;
		}

		public int mAddFlags;

		public int mRemoveFlags;
	}
}
