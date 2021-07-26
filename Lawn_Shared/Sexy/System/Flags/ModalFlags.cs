using System;

namespace Sexy
{
	public struct ModalFlags
	{
		public void ModFlags(ref FlagsMod theFlagsMod)
		{
			GlobalMembersFlags.ModFlags(ref this.mOverFlags, theFlagsMod);
			GlobalMembersFlags.ModFlags(ref this.mUnderFlags, theFlagsMod);
		}

		public int GetFlags()
		{
			if (!this.mIsOver)
			{
				return this.mUnderFlags;
			}
			return this.mOverFlags;
		}

		public int mOverFlags;

		public int mUnderFlags;

		public bool mIsOver;
	}
}
