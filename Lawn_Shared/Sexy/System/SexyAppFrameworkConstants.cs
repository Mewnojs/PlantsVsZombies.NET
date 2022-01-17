using System;

namespace Sexy
{
	internal class SexyAppFrameworkConstants
	{
		public static int ticksForSeconds(float seconds)
		{
			return (int)(100f * seconds);
		}

		public const int FRAMEWORK_UPDATE_RATE = 100;

		public const int NSTIMER_UPDATE_RATE = 30;

		public const int ASSOCIATED_ALPHA = 1;

		public const int MAX_VERTICES = 4096;

		public const long POLYNOMIAL = 79764919L;

		public const int MTRAND_N = 624;

		public const int MAX_SOURCE_SOUNDS = 256;

		public const int MAX_CHANNELS = 32;

		public const int Flushes = 1;

		public const long FONT_MAGIC = 4278190267L;

		public const int INCLUDE_WINDOWS_1252_EXTENSIONS = 1;

		public const int MTRAND_M = 397;

		public const ulong MATRIX_A = 2567483615UL;

		public const ulong UPPER_MASK = 2147483648UL;

		public const ulong LOWER_MASK = 2147483647UL;

		public const long TEMPERING_MASK_B = 2636928640L;

		public const long TEMPERING_MASK_C = 4022730752L;

		public const int VERBOSE_LOADING = 0;

		public const int SERIALIZE_FONTS = 0;

		public const int THREAD_PRINT = 1;
	}
}
