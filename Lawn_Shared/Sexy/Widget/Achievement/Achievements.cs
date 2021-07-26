using System;
using System.Collections.Generic;

namespace Sexy
{
	internal static class Achievements
	{
		static Achievements()
		{
			Achievements.ACHIEVEMENT_KEYS = new string[18];
			Achievements.ACHIEVEMENT_KEYS[0] = "Home Lawn Security";
			Achievements.ACHIEVEMENT_KEYS[1] = "Master of Mosticulture";
			Achievements.ACHIEVEMENT_KEYS[2] = "Better Off Dead";
			Achievements.ACHIEVEMENT_KEYS[3] = "China Shop";
			Achievements.ACHIEVEMENT_KEYS[4] = "Beyond the Grave";
			Achievements.ACHIEVEMENT_KEYS[5] = "Crash of the Titan";
			Achievements.ACHIEVEMENT_KEYS[6] = "Soil Your Plants";
			Achievements.ACHIEVEMENT_KEYS[7] = "Explodonator";
			Achievements.ACHIEVEMENT_KEYS[8] = "Close Shave";
			Achievements.ACHIEVEMENT_KEYS[9] = "Shopaholic";
			Achievements.ACHIEVEMENT_KEYS[10] = "Nom Nom Nom";
			Achievements.ACHIEVEMENT_KEYS[11] = "No Fungus Among Us";
			Achievements.ACHIEVEMENT_KEYS[12] = "Dont Pea in the Pool";
			Achievements.ACHIEVEMENT_KEYS[13] = "Grounded";
			Achievements.ACHIEVEMENT_KEYS[14] = "Good Morning";
			Achievements.ACHIEVEMENT_KEYS[15] = "Popcorn Party";
			Achievements.ACHIEVEMENT_KEYS[16] = "Roll Some Heads";
			Achievements.ACHIEVEMENT_KEYS[17] = "Disco is Undead";
		}

		public static AchievementItem GetAchievementItem(AchievementId index)
		{
			AchievementItem result;
			lock (ReportAchievement.achievementLock)
			{
				string achievementKey = Achievements.GetAchievementKey(index);
				for (int i = 0; i < Achievements.gAchievementList.Count; i++)
				{
					if (Achievements.gAchievementList[i].Key == achievementKey)
					{
						return Achievements.gAchievementList[i];
					}
				}
				result = null;
			}
			return result;
		}

		public static string GetAchievementKey(AchievementId index)
		{
			return Achievements.ACHIEVEMENT_KEYS[(int)index];
		}

		public static void ClearAchievements()
		{
			foreach (AchievementItem achievementItem in Achievements.gAchievementList)
			{
				achievementItem.Dispose();
			}
			Achievements.gAchievementList.Clear();
		}

		public static int GetNumberOfAchievements()
		{
			return Achievements.gAchievementList.Count;
		}

		public static void AddAchievement(AchievementItem item)
		{
			Achievements.gAchievementList.Add(item);
		}

		public static readonly string[] ACHIEVEMENT_KEYS;

		private static List<AchievementItem> gAchievementList = new List<AchievementItem>();
	}
}
