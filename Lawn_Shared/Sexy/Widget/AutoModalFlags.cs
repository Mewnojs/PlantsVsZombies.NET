using System;

namespace Sexy
{
	public struct AutoModalFlags
	{
		public AutoModalFlags(ModalFlags theModalFlags, FlagsMod theFlagMod)
		{
			this.mModalFlags = theModalFlags;
			this.mOldOverFlags = theModalFlags.mOverFlags;
			this.mOldUnderFlags = theModalFlags.mUnderFlags;
			theModalFlags.ModFlags(ref theFlagMod);
		}

		public void Dispose()
		{
			this.mModalFlags.mOverFlags = this.mOldOverFlags;
			this.mModalFlags.mUnderFlags = this.mOldUnderFlags;
		}

		public ModalFlags mModalFlags;

		public int mOldOverFlags;

		public int mOldUnderFlags;
	}
}
