using System;

namespace Sexy
{
	public struct AutoModalFlags
	{
		public AutoModalFlags(ModalFlags theModalFlags, FlagsMod theFlagMod)
		{
			mModalFlags = theModalFlags;
			mOldOverFlags = theModalFlags.mOverFlags;
			mOldUnderFlags = theModalFlags.mUnderFlags;
			theModalFlags.ModFlags(ref theFlagMod);
		}

		public void Dispose()
		{
			mModalFlags.mOverFlags = mOldOverFlags;
			mModalFlags.mUnderFlags = mOldUnderFlags;
		}

		public ModalFlags mModalFlags;

		public int mOldOverFlags;

		public int mOldUnderFlags;
	}
}
