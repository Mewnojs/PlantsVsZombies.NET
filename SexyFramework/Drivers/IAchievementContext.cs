using System;
using System.Collections.Generic;

namespace Sexy.Drivers
{
	public abstract class IAchievementContext : IAsyncTask
	{
		public override void Dispose()
		{
			base.Dispose();
		}

		public abstract void SetListener(IAchievementListener listener);

		public abstract List<uint> GetUnlockedAchievements();
	}
}
