using System;
using Sexy.Misc;

namespace Sexy.Drivers.Profile
{
	public class UserProfile
	{
		public UserProfile()
		{
			this.mPlatformData = IProfileData.CreateProfileData(this);
		}

		public virtual int GetId()
		{
			return this.mPlatformData.GetId();
		}

		public virtual string GetName()
		{
			return this.mPlatformData.GetName();
		}

		public virtual uint GetGamepadIndex()
		{
			return this.mPlatformData.GetGamepadIndex();
		}

		public virtual void SetGamepadIndex(uint gamepad)
		{
			this.mPlatformData.SetGamepadIndex(gamepad);
		}

		public virtual void Reset()
		{
		}

		public virtual void DeleteUserFiles()
		{
			this.mPlatformData.DeleteUserFiles();
		}

		public virtual EProfileIOState LoadDetails()
		{
			return this.mPlatformData.LoadDetails();
		}

		public virtual bool IsLoading()
		{
			return this.mPlatformData.IsLoading();
		}

		public virtual bool IsLoaded()
		{
			return this.mPlatformData.IsLoaded();
		}

		public virtual EProfileIOState SaveDetails()
		{
			return this.mPlatformData.SaveDetails();
		}

		public virtual bool IsSaving()
		{
			return this.mPlatformData.IsSaving();
		}

		public virtual bool IsSaved()
		{
			return this.mPlatformData.IsSaved();
		}

		public virtual bool HasError()
		{
			return this.mPlatformData.HasError();
		}

		public virtual bool SignedIn()
		{
			return this.mPlatformData.SignedIn();
		}

		public virtual bool IsSigningIn()
		{
			return this.mPlatformData.IsSigningIn();
		}

		public virtual bool IsOnline()
		{
			return this.mPlatformData.IsOnline();
		}

		public virtual bool ReadProfileSettings(SexyBuffer theData)
		{
			theData.ReadInt32();
			return true;
		}

		public virtual bool WriteProfileSettings(SexyBuffer theData)
		{
			theData.WriteInt32(1234);
			return true;
		}

		public bool IsAchievementUnlocked(uint id)
		{
			return this.mPlatformData.IsAchievementUnlocked(id);
		}

		public IAchievementContext StartUnlockAchievement(uint id)
		{
			return this.mPlatformData.StartUnlockAchievement(id);
		}

		public IProfileData GetPlatformData()
		{
			return this.mPlatformData;
		}

		private IProfileData mPlatformData;
	}
}
