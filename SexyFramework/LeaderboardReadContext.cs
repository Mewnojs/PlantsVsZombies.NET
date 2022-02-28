using System;
using System.Collections.Generic;
using Sexy.Drivers.Leaderboard;

namespace Sexy
{
	public abstract class LeaderboardReadContext : IAsyncTask
	{
		public override void Dispose()
		{
			base.Dispose();
		}

		public uint GetStartRow()
		{
			return this.mStartRank;
		}

		public uint GetNumRows()
		{
			return this.mNumEntries;
		}

		public uint GetTotalNumRows()
		{
			return this.mTotalNumEntries;
		}

		public virtual int GetUserRow()
		{
			return -1;
		}

		public LeaderboardEntry GetRow(int index)
		{
			return this.mData[index];
		}

		protected uint mStartRank;

		protected uint mNumEntries;

		protected uint mTotalNumEntries;

		protected List<LeaderboardEntry> mData = new List<LeaderboardEntry>();
	}
}
