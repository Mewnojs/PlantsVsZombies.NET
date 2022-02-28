using System;
using Sexy.Drivers.Leaderboard;
using Sexy.Drivers.Profile;

namespace Sexy
{
	public abstract class ILeaderboardDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract int Init();

		public abstract void Update();

		public abstract void RegisterSchema(string id, LeaderboardSchema schema);

		public abstract LeaderboardSchema GetSchema(string id);

		public abstract uint MaxReadEntries();

		public abstract LeaderboardWriteContext StartWriteScore(UserProfile player, string leaderboardId, string secondaryId, LeaderboardEntry entry);

		public abstract LeaderboardReadContext StartReadScores(UserProfile player, string leaderboardId, string secondaryId, Leaderboard.Type type, uint startRank, uint maxEntries);
	}
}
