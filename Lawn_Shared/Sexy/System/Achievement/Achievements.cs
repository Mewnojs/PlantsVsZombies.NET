using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Sexy
{
	internal class AchievementItem
	{
		public AchievementId Id { get; private set; }

		public string Key { get; private set; }

		public bool IsEarned { get; private set; }

		public int GamerScore
		{
			get
			{
				return gamerScore;
			}
			protected set
			{
				gamerScore = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			protected set
			{
				name = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			protected set
			{
				description = value;
			}
		}

		public Image AchievementImage
		{
			get
			{
				return achievementImage;
			}
			protected set
			{
				achievementImage = value;
			}
		}

		public AchievementItem(Achievement a)
		{
			Name = a.Name;
			Description = a.Description;
			GamerScore = a.GamerScore;
			Key = a.Key;
			IsEarned = a.IsEarned;
			using (Stream picture = a.GetPicture())
			{
				AchievementImage = new Image(Texture2D.FromStream(GlobalStaticVars.g.GraphicsDevice, picture));
			}
		}

		public void Dispose()
		{
			AchievementImage.Dispose();
		}

		public static bool operator ==(AchievementItem a, AchievementItem b)
		{
			return (!(a is null) || !(b is null)) ? (!(a is null) && (!(b is null) && a.Equals(b))) : true;

		}

		public static bool operator !=(AchievementItem a, AchievementItem b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is AchievementItem))
			{
				return false;
			}
			AchievementItem achievementItem = obj as AchievementItem;
			return achievementItem.Name == Name;
		}

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}

		public override string ToString()
		{
			return name;
		}

		private int gamerScore;

		private string name;

		private string description;

		private Image achievementImage;
	}

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

	public/*internal*/ enum AchievementId
	{
		ACHIEVEMENT_HOME_SECURITY,
		ACHIEVEMENT_MORTICULTURALIST,
		BetterOffDead,
		ChinaShop,
		BeyondTheGrave,
		CrashoftheTitan,
		SoilYourPlants,
		ACHIEVEMENT_EXPLODONATOR,
		CloseShave,
		Shopaholic,
		NomNomNom,
		NoFungusAmongUs,
		DontPeainthePool,
		Grounded,
		GoodMorning,
		ACHIEVEMENT_POPCORN_PARTY,
		ACHIEVEMENT_ROLL_SOME_HEADS,
		DiscoisUndead,
		MAX_ACHIEVEMENTS
	}
}
