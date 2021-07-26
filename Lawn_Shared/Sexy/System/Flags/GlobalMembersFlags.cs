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
}
