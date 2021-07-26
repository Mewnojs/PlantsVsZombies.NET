using System;

namespace Sexy
{
	internal struct TrialAchievementAlert
	{
		public TrialAchievementAlert(AchievementId achievement)
		{
			this.achievement = achievement;
		}

		private AchievementId achievement;
	}
}
