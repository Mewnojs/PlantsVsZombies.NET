using System;
using Sexy.Drivers.Profile;
using Sexy.GraphicsLib;

namespace Sexy.Drivers
{
	public abstract class IProfileData
	{
		public static IProfileData CreateProfileData(UserProfile player)
		{
			return new FilesystemProfileData(player);
		}

		public abstract int GetId();

		public abstract string GetName();

		public abstract Image GetPlayerIcon();

		public abstract uint GetGamepadIndex();

		public abstract void SetGamepadIndex(uint gamepad);

		public abstract bool SignedIn();

		public abstract bool IsSigningIn();

		public abstract bool IsOnline();

		public abstract EProfileIOState LoadDetails();

		public abstract bool IsLoading();

		public abstract bool IsLoaded();

		public abstract EProfileIOState SaveDetails();

		public abstract bool IsSaving();

		public abstract bool IsSaved();

		public abstract bool HasError();

		public abstract void DeleteUserFiles();

		public abstract bool IsAchievementUnlocked(uint id);

		public abstract IAchievementContext StartUnlockAchievement(uint id);
	}
}
