using System;
using System.Collections.Generic;
using Sexy;

namespace Lawn
{
	internal class ProfileMgr
	{
		protected void DeleteOldestProfile()
		{
			if (this.mProfileMap.Count == 0)
			{
				return;
			}
			Dictionary<string, PlayerInfo>.Enumerator enumerator = this.mProfileMap.GetEnumerator();
			Dictionary<string, PlayerInfo>.Enumerator enumerator2 = enumerator;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, PlayerInfo> keyValuePair = enumerator.Current;
				uint mUseSeq = keyValuePair.Value.mUseSeq;
				KeyValuePair<string, PlayerInfo> keyValuePair2 = enumerator2.Current;
				if (mUseSeq < keyValuePair2.Value.mUseSeq)
				{
					enumerator2 = enumerator;
				}
			}
			KeyValuePair<string, PlayerInfo> keyValuePair3 = enumerator2.Current;
			keyValuePair3.Value.DeleteUserFiles();
			Dictionary<string, PlayerInfo> dictionary = this.mProfileMap;
			KeyValuePair<string, PlayerInfo> keyValuePair4 = enumerator2.Current;
			dictionary.Remove(keyValuePair4.Key);
		}

		protected void DeleteOldProfiles()
		{
			while (this.mProfileMap.Count > 200)
			{
				this.DeleteOldestProfile();
			}
		}

		public ProfileMgr()
		{
			this.Clear();
		}

		public void Dispose()
		{
		}

		public void Clear()
		{
			this.mProfileMap.Clear();
			ProfileMgr.mNextProfileId = 1U;
			this.mNextProfileUseSeq = 1U;
			this.mLastMoreGamesUpdate = DateTime.Now;
		}

		private static string GetSaveFile()
		{
			return GlobalStaticVars.GetDocumentsDir() + "userdata/users.dat";
		}

		public void Load()
		{
			try
			{
				string saveFile = ProfileMgr.GetSaveFile();
				Sexy.Buffer buffer = new Sexy.Buffer();
				if (GlobalStaticVars.gSexyAppBase.ReadBufferFromFile(saveFile, ref buffer, false))
				{
					int num = buffer.ReadLong();
					int num2 = buffer.ReadLong();
					for (int i = 0; i < num2; i++)
					{
						uint num3 = (uint)buffer.ReadLong();
						if (ProfileMgr.mNextProfileId <= num3)
						{
							ProfileMgr.mNextProfileId = num3 + 1U;
						}
						PlayerInfo playerInfo = new PlayerInfo(num3);
						if (playerInfo.LoadDetails())
						{
							this.mProfileMap.Add(playerInfo.mName, playerInfo);
						}
					}
					this.activeUser = buffer.ReadString();
					if (buffer.ReadLong() != 555)
					{
						throw new Exception("Profile Manager: Save check number mismatch");
					}
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				this.mProfileMap.Clear();
			}
		}

		public void Save()
		{
			try
			{
				string saveFile = ProfileMgr.GetSaveFile();
				Sexy.Buffer buffer = new Sexy.Buffer();
				buffer.WriteLong(1);
				buffer.WriteLong(this.mProfileMap.Count);
				foreach (PlayerInfo playerInfo in this.mProfileMap.Values)
				{
					buffer.WriteLong((int)playerInfo.mId);
					playerInfo.SaveDetails();
				}
				buffer.WriteString((GlobalStaticVars.gLawnApp.mPlayerInfo != null) ? GlobalStaticVars.gLawnApp.mPlayerInfo.mName : string.Empty);
				buffer.WriteLong(555);
				GlobalStaticVars.gSexyAppBase.WriteBufferToFile(saveFile, buffer);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
			}
		}

		public int GetNumProfiles()
		{
			return this.mProfileMap.Count;
		}

		public PlayerInfo GetProfile(string theName)
		{
			if (this.mProfileMap.ContainsKey(theName))
			{
				PlayerInfo playerInfo = this.mProfileMap[theName];
				playerInfo.LoadDetails();
				playerInfo.mUseSeq = this.mNextProfileUseSeq++;
				return playerInfo;
			}
			return null;
		}

		public PlayerInfo AddProfile(string theName)
		{
			if (this.mProfileMap.ContainsKey(theName))
			{
				return this.mProfileMap[theName];
			}
			this.mProfileMap.Add(theName, new PlayerInfo());
			PlayerInfo playerInfo = this.mProfileMap[theName];
			playerInfo.mName = theName;
			playerInfo.mId = ProfileMgr.GetNewProfileId();
			playerInfo.mUseSeq = this.mNextProfileUseSeq++;
			this.DeleteOldProfiles();
			return playerInfo;
		}

		public static uint GetNewProfileId()
		{
			return ProfileMgr.mNextProfileId++;
		}

		public PlayerInfo GetAnyProfile()
		{
			if (this.mProfileMap.Count == 0)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(this.activeUser))
			{
				foreach (PlayerInfo playerInfo in this.mProfileMap.Values)
				{
					if (playerInfo.mName == this.activeUser)
					{
						return playerInfo;
					}
				}
			}
			Dictionary<string, PlayerInfo>.Enumerator enumerator2 = this.mProfileMap.GetEnumerator();
			enumerator2.MoveNext();
			KeyValuePair<string, PlayerInfo> keyValuePair = enumerator2.Current;
			PlayerInfo value = keyValuePair.Value;
			value.LoadDetails();
			value.mUseSeq = this.mNextProfileUseSeq++;
			return value;
		}

		public bool DeleteProfile(string theName)
		{
			this.mProfileMap[theName].DeleteUserFiles();
			return this.mProfileMap.Remove(theName);
		}

		public bool RenameProfile(string theOldName, string theNewName)
		{
			if (theOldName == theNewName)
			{
				return true;
			}
			if (this.mProfileMap.ContainsKey(theOldName))
			{
				PlayerInfo playerInfo = this.mProfileMap[theOldName];
				this.mProfileMap.Remove(theOldName);
				playerInfo.mName = theNewName;
				this.mProfileMap.Add(theNewName, playerInfo);
				return true;
			}
			return false;
		}

		public Dictionary<string, PlayerInfo> GetProfileMap()
		{
			return this.mProfileMap;
		}

		private const int saveFileVersion = 1;

		private const int saveCheckNumber = 555;

		private Dictionary<string, PlayerInfo> mProfileMap = new Dictionary<string, PlayerInfo>();

		private static uint mNextProfileId;

		private uint mNextProfileUseSeq;

		public DateTime mLastMoreGamesUpdate = default(DateTime);

		private string activeUser;
	}
}
