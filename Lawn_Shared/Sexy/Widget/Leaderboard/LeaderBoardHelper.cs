using System;

namespace Sexy
{
	internal static class LeaderBoardHelper
	{
		public static int GetLeaderboardNumber(LeaderboardGameMode mode)
		{
			return (int)mode;
		}

		public static int GetLeaderboardNumber(LeaderboardState state)
		{
			return (int)state;
		}

		public static bool IsModeSupported(LeaderboardGameMode mode)
		{
			return mode == LeaderboardGameMode.Adventure || mode == LeaderboardGameMode.IZombie || mode == LeaderboardGameMode.Vasebreaker;
		}
	}
}
