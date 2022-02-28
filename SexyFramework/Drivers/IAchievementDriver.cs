using System;
using Sexy.Drivers.Profile;

namespace Sexy.Drivers
{
	public abstract class IAchievementDriver
	{
		public virtual void Dispose()
		{
		}

		public abstract int Init();

		public abstract void Update();

		public abstract IAchievementContext StartReadUnlockedAchievements(UserProfile p);

		public abstract IAchievementContext StartUnlockAchievement(UserProfile p, uint id);
	}
}
