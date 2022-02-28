using System;

namespace Sexy.Drivers.Profile
{
	public class ProfileManager
	{
		public ProfileManager(ProfileEventListener listener)
		{
			this.mListener = listener;
		}

		public virtual bool Init()
		{
			return true;
		}

		public virtual void Update()
		{
			GlobalMembers.gSexyAppBase.mProfileDriver.Update();
		}

		public virtual uint GetNumProfiles()
		{
			return GlobalMembers.gSexyAppBase.mProfileDriver.GetNumProfiles();
		}

		public virtual bool HasProfile(string theName)
		{
			return GlobalMembers.gSexyAppBase.mProfileDriver.HasProfile(theName);
		}

		public virtual UserProfile GetProfile(int index)
		{
			return GlobalMembers.gSexyAppBase.mProfileDriver.GetProfile(index);
		}

		public virtual UserProfile GetProfile(string theName)
		{
			return GlobalMembers.gSexyAppBase.mProfileDriver.GetProfile(theName);
		}

		public virtual UserProfile GetAnyProfile()
		{
			return GlobalMembers.gSexyAppBase.mProfileDriver.GetAnyProfile();
		}

		public virtual void ClearProfiles()
		{
			GlobalMembers.gSexyAppBase.mProfileDriver.ClearProfiles();
		}

		public virtual UserProfile AddProfile(string theName)
		{
			return GlobalMembers.gSexyAppBase.mProfileDriver.AddProfile(theName);
		}

		public virtual bool DeleteProfile(string theName)
		{
			return GlobalMembers.gSexyAppBase.mProfileDriver.DeleteProfile(theName);
		}

		public virtual bool RenameProfile(string theOldName, string theNewName)
		{
			return GlobalMembers.gSexyAppBase.mProfileDriver.RenameProfile(theOldName, theNewName);
		}

		public uint GetProfileVersion()
		{
			return this.mListener.GetProfileVersion();
		}

		public ProfileEventListener GetListener()
		{
			return this.mListener;
		}

		public UserProfile CreateUserProfile()
		{
			return this.mListener.CreateUserProfile();
		}

		protected ProfileEventListener mListener;
	}
}
