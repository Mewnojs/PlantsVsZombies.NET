using System;
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
}
