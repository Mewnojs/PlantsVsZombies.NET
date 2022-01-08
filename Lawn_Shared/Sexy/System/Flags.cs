using System;

namespace Sexy
{
	internal static class GlobalMembersFlags
	{
		public static void ModFlags(ref int theFlags, FlagsMod theFlagMod)
		{
			theFlags = ((theFlags | theFlagMod.mAddFlags) & ~theFlagMod.mRemoveFlags);
		}

		public static int GetModFlags(int theFlags, FlagsMod theFlagMod)
		{
			return (theFlags | theFlagMod.mAddFlags) & ~theFlagMod.mRemoveFlags;
		}
	}

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
