using System;
using Sexy.Drivers.Profile;

namespace Sexy.Drivers
{
	public abstract class IProfileDriver
	{
		public static IProfileDriver CreateProfileDriver()
		{
			return new FilesystemProfileDriver();
		}

		public abstract bool Init();

		public abstract void Update();

		public abstract uint GetNumProfiles();

		public abstract bool HasProfile(string theName);

		public abstract UserProfile GetProfile(int index);

		public abstract UserProfile GetProfile(string theName);

		public abstract UserProfile GetAnyProfile();

		public abstract UserProfile AddProfile(string theName);

		public abstract bool DeleteProfile(string theName);

		public abstract bool RenameProfile(string theOldName, string theNewName);

		public abstract void ClearProfiles();
	}
}
