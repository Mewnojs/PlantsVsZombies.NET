using System;

namespace Sexy.Misc
{
	public class FlagsMod
	{
		public static int GetModFlags(int theFlags, FlagsMod theFlagMod)
		{
			return (theFlags | theFlagMod.mAddFlags) & ~theFlagMod.mRemoveFlags;
		}

		public static void ModFlags(ref int theFlags, FlagsMod theFlagMod)
		{
			theFlags = (theFlags | theFlagMod.mAddFlags) & ~theFlagMod.mRemoveFlags;
		}

		public int mRemoveFlags;

		public int mAddFlags;
	}
}
