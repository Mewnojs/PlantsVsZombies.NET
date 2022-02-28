using System;
using System.Collections.Generic;
using System.Linq;
using Sexy.File;
using Sexy.Misc;

namespace Sexy.Drivers.Profile
{
	public class FilesystemProfileDriver : IProfileDriver
	{
		public FilesystemProfileDriver()
		{
			this.ClearProfiles();
		}

		public override bool Init()
		{
			if (GlobalMembers.gSexyAppBase.mProfileManager == null)
			{
				return false;
			}
			this.Load();
			return true;
		}

		public override void Update()
		{
		}

		public bool HasError()
		{
			return false;
		}

		public override uint GetNumProfiles()
		{
			return (uint)this.mProfileMap.Count;
		}

		public override bool HasProfile(string theName)
		{
			return this.mProfileMap.ContainsKey(theName) && this.mProfileMap[theName] != null;
		}

		public override UserProfile GetProfile(int index)
		{
			if (index >= this.mProfileMap.Count)
			{
				return null;
			}
			UserProfile userProfile = Enumerable.ElementAt<UserProfile>(this.mProfileMap.Values, index);
			if (userProfile == null)
			{
				return null;
			}
			FilesystemProfileData filesystemProfileData = (FilesystemProfileData)userProfile.GetPlatformData();
			userProfile.LoadDetails();
			filesystemProfileData.SetUseSeq((int)this.mNextProfileUseSeq++);
			return userProfile;
		}

		public override UserProfile GetProfile(string theName)
		{
			if (!this.mProfileMap.ContainsKey(theName))
			{
				return null;
			}
			UserProfile userProfile = this.mProfileMap[theName];
			if (userProfile == null)
			{
				return null;
			}
			FilesystemProfileData filesystemProfileData = (FilesystemProfileData)userProfile.GetPlatformData();
			userProfile.LoadDetails();
			filesystemProfileData.SetUseSeq((int)this.mNextProfileUseSeq++);
			return userProfile;
		}

		public override UserProfile GetAnyProfile()
		{
			return this.GetProfile(0);
		}

		public override UserProfile AddProfile(string theName)
		{
			if (this.mProfileMap.ContainsKey(theName))
			{
				return null;
			}
			UserProfile userProfile = GlobalMembers.gSexyAppBase.mProfileManager.CreateUserProfile();
			FilesystemProfileData filesystemProfileData = (FilesystemProfileData)userProfile.GetPlatformData();
			filesystemProfileData.SetName(theName);
			filesystemProfileData.SetId((int)this.mNextProfileId++);
			filesystemProfileData.SetUseSeq((int)this.mNextProfileUseSeq++);
			this.mProfileMap.Add(theName, userProfile);
			this.DeleteOldProfiles();
			this.Save();
			return userProfile;
		}

		public override bool DeleteProfile(string theName)
		{
			if (this.mProfileMap.ContainsKey(theName))
			{
				this.mProfileMap[theName].DeleteUserFiles();
				this.mProfileMap.Remove(theName);
				this.Save();
				return true;
			}
			return false;
		}

		public override bool RenameProfile(string theOldName, string theNewName)
		{
			if (!this.mProfileMap.ContainsKey(theOldName))
			{
				return false;
			}
			if (string.Compare(theOldName.ToLower(), theNewName.ToLower()) == 0)
			{
				FilesystemProfileData filesystemProfileData = (FilesystemProfileData)this.mProfileMap[theOldName].GetPlatformData();
				filesystemProfileData.SetName(theNewName);
				return true;
			}
			if (this.mProfileMap.ContainsKey(theNewName))
			{
				return false;
			}
			this.mProfileMap.Add(theNewName, this.mProfileMap[theOldName]);
			this.mProfileMap.Remove(theOldName);
			FilesystemProfileData filesystemProfileData2 = (FilesystemProfileData)this.mProfileMap[theNewName].GetPlatformData();
			filesystemProfileData2.SetName(theNewName);
			this.Save();
			return true;
		}

		public override void ClearProfiles()
		{
			this.mProfileMap.Clear();
			this.mNextProfileId = 1U;
			this.mNextProfileUseSeq = 1U;
		}

		protected void Load()
		{
			SexyBuffer buffer = new SexyBuffer();
			string theFileName = "userdata/users.dat";
			if (!StorageFile.ReadBufferFromFile(theFileName, buffer))
			{
				return;
			}
			if (!this.ReadState(buffer))
			{
				this.ClearProfiles();
			}
		}

		protected void Save()
		{
			SexyBuffer buffer = new SexyBuffer();
			if (!this.WriteState(buffer))
			{
				return;
			}
			StorageFile.MakeDir("userdata");
			string theFileName = "userdata/users.dat";
			StorageFile.WriteBufferToFile(theFileName, buffer);
		}

		protected void DeleteOldestProfile()
		{
			if (this.mProfileMap.Count == 0)
			{
				return;
			}
			string text = null;
			FilesystemProfileData filesystemProfileData = null;
			foreach (KeyValuePair<string, UserProfile> keyValuePair in this.mProfileMap)
			{
				if (text == null)
				{
					text = keyValuePair.Key;
					filesystemProfileData = (FilesystemProfileData)keyValuePair.Value.GetPlatformData();
				}
				else
				{
					FilesystemProfileData filesystemProfileData2 = (FilesystemProfileData)keyValuePair.Value.GetPlatformData();
					if (filesystemProfileData2.getUseSeq() < filesystemProfileData.getUseSeq())
					{
						text = keyValuePair.Key;
						filesystemProfileData = filesystemProfileData2;
					}
				}
			}
			this.mProfileMap[text].DeleteUserFiles();
			this.mProfileMap.Remove(text);
		}

		protected void DeleteOldProfiles()
		{
			while (this.mProfileMap.Count > 200)
			{
				this.DeleteOldestProfile();
			}
			this.Save();
		}

		protected bool ReadState(SexyBuffer data)
		{
			int num = (int)data.ReadLong();
			if ((long)num != (long)((ulong)GlobalMembers.gSexyAppBase.mProfileManager.GetProfileVersion()))
			{
				return false;
			}
			this.mProfileMap.Clear();
			uint num2 = 0U;
			uint num3 = 0U;
			int num4 = (int)data.ReadShort();
			for (int i = 0; i < num4; i++)
			{
				UserProfile userProfile = GlobalMembers.gSexyAppBase.mProfileManager.CreateUserProfile();
				FilesystemProfileData filesystemProfileData = (FilesystemProfileData)userProfile.GetPlatformData();
				if (!filesystemProfileData.ReadSummary(data))
				{
					return false;
				}
				if ((long)filesystemProfileData.getUseSeq() > (long)((ulong)num2))
				{
					num2 = (uint)filesystemProfileData.getUseSeq();
				}
				if ((long)userProfile.GetId() > (long)((ulong)num3))
				{
					num3 = (uint)userProfile.GetId();
				}
				this.mProfileMap.Add(userProfile.GetName(), userProfile);
			}
			this.mNextProfileId = num3 + 1U;
			this.mNextProfileUseSeq = num2 + 1U;
			return true;
		}

		protected bool WriteState(SexyBuffer data)
		{
			data.WriteLong((long)((ulong)GlobalMembers.gSexyAppBase.mProfileManager.GetProfileVersion()));
			data.WriteShort((short)this.mProfileMap.Count);
			foreach (UserProfile userProfile in this.mProfileMap.Values)
			{
				FilesystemProfileData filesystemProfileData = (FilesystemProfileData)userProfile.GetPlatformData();
				if (!filesystemProfileData.WriteSummary(data))
				{
					return false;
				}
			}
			return true;
		}

		private Dictionary<string, UserProfile> mProfileMap = new Dictionary<string, UserProfile>();

		private uint mNextProfileId;

		private uint mNextProfileUseSeq;
	}
}
