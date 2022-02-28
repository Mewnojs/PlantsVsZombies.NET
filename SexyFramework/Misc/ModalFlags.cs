using System;

namespace Sexy.Misc
{
	public class ModalFlags
	{
		public void ModFlags(FlagsMod theFlagsMod)
		{
			FlagsMod.ModFlags(ref this.mOverFlags, theFlagsMod);
			FlagsMod.ModFlags(ref this.mOverFlags, theFlagsMod);
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
