using System;

namespace Sexy
{
	public struct ModalFlags
	{
		public void ModFlags(ref FlagsMod theFlagsMod)
		{
			GlobalMembersFlags.ModFlags(ref mOverFlags, theFlagsMod);
			GlobalMembersFlags.ModFlags(ref mUnderFlags, theFlagsMod);
		}

		public int GetFlags()
		{
			if (!mIsOver)
			{
				return mUnderFlags;
			}
			return mOverFlags;
		}

		public int mOverFlags;

		public int mUnderFlags;

		public bool mIsOver;
	}
}
