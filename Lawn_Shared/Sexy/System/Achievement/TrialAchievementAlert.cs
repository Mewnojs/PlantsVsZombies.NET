using System;

namespace Sexy
{
	public/*internal*/ struct TrialAchievementAlert
	{
		public TrialAchievementAlert(AchievementId achievement)
		{
			this.achievement = achievement;
		}

		private AchievementId achievement;
	}
}
