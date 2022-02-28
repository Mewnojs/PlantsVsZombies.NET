using System;
using Sexy.Misc;

namespace Sexy.Drivers.Profile
{
	public interface ProfileEventListener
	{
		uint GetProfileVersion();

		void NotifyProfileChanged(UserProfile player);

		UserProfile CreateUserProfile();

		void OnProfileLoad(UserProfile player, SexyBuffer buffer);

		void OnProfileSave(UserProfile player, SexyBuffer buffer);
	}
}
