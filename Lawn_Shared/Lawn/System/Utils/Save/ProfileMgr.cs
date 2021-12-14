using System;
using System.Collections.Generic;
using Sexy;

namespace Lawn
{
	public/*internal*/ class ProfileMgr
	{
		protected void DeleteOldestProfile()
		{
			if (mProfileMap.Count == 0)
			{
				return;
			}
			Dictionary<string, PlayerInfo>.Enumerator enumerator = mProfileMap.GetEnumerator();
			Dictionary<string, PlayerInfo>.Enumerator enumerator2 = enumerator;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, PlayerInfo> keyValuePair = enumerator.Current;
				uint useSeq = keyValuePair.Value.mUseSeq;
				KeyValuePair<string, PlayerInfo> keyValuePair2 = enumerator2.Current;
				if (useSeq < keyValuePair2.Value.mUseSeq)
				{
					enumerator2 = enumerator;
				}
			}
			KeyValuePair<string, PlayerInfo> keyValuePair3 = enumerator2.Current;
			keyValuePair3.Value.DeleteUserFiles();
			Dictionary<string, PlayerInfo> dictionary = mProfileMap;
			KeyValuePair<string, PlayerInfo> keyValuePair4 = enumerator2.Current;
			dictionary.Remove(keyValuePair4.Key);
		}

		protected void DeleteOldProfiles()
		{
			while (mProfileMap.Count > 200)
			{
				DeleteOldestProfile();
			}
		}

		public ProfileMgr()
		{
			Clear();
		}

		public void Dispose()
		{
		}

		public void Clear()
		{
			mProfileMap.Clear();
			ProfileMgr.mNextProfileId = 1U;
			mNextProfileUseSeq = 1U;
			mLastMoreGamesUpdate = DateTime.Now;
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
							mProfileMap.Add(playerInfo.mName, playerInfo);
						}
					}
					activeUser = buffer.ReadString();
					if (buffer.ReadLong() != 555)
					{
						throw new Exception("Profile Manager: Save check number mismatch");
					}
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				mProfileMap.Clear();
			}
		}

		public void Save()
		{
			try
			{
				string saveFile = ProfileMgr.GetSaveFile();
				Sexy.Buffer buffer = new Sexy.Buffer();
				buffer.WriteLong(1);
				buffer.WriteLong(mProfileMap.Count);
				foreach (PlayerInfo playerInfo in mProfileMap.Values)
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
			return mProfileMap.Count;
		}

		public PlayerInfo GetProfile(string theName)
		{
			if (mProfileMap.ContainsKey(theName))
			{
				PlayerInfo playerInfo = mProfileMap[theName];
				playerInfo.LoadDetails();
				playerInfo.mUseSeq = mNextProfileUseSeq++;
				return playerInfo;
			}
			return null;
		}

		public PlayerInfo AddProfile(string theName)
		{
			if (mProfileMap.ContainsKey(theName))
			{
				return mProfileMap[theName];
			}
			mProfileMap.Add(theName, new PlayerInfo());
			PlayerInfo playerInfo = mProfileMap[theName];
			playerInfo.mName = theName;
			playerInfo.mId = ProfileMgr.GetNewProfileId();
			playerInfo.mUseSeq = mNextProfileUseSeq++;
			DeleteOldProfiles();
			return playerInfo;
		}

		public static uint GetNewProfileId()
		{
			return ProfileMgr.mNextProfileId++;
		}

		public PlayerInfo GetAnyProfile()
		{
			if (mProfileMap.Count == 0)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(activeUser))
			{
				foreach (PlayerInfo playerInfo in mProfileMap.Values)
				{
					if (playerInfo.mName == activeUser)
					{
						return playerInfo;
					}
				}
			}
			Dictionary<string, PlayerInfo>.Enumerator enumerator2 = mProfileMap.GetEnumerator();
			enumerator2.MoveNext();
			KeyValuePair<string, PlayerInfo> keyValuePair = enumerator2.Current;
			PlayerInfo value = keyValuePair.Value;
			value.LoadDetails();
			value.mUseSeq = mNextProfileUseSeq++;
			return value;
		}

		public bool DeleteProfile(string theName)
		{
			mProfileMap[theName].DeleteUserFiles();
			return mProfileMap.Remove(theName);
		}

		public bool RenameProfile(string theOldName, string theNewName)
		{
			if (theOldName == theNewName)
			{
				return true;
			}
			if (mProfileMap.ContainsKey(theOldName))
			{
				PlayerInfo playerInfo = mProfileMap[theOldName];
				mProfileMap.Remove(theOldName);
				playerInfo.mName = theNewName;
				mProfileMap.Add(theNewName, playerInfo);
				return true;
			}
			return false;
		}

		public Dictionary<string, PlayerInfo> GetProfileMap()
		{
			return mProfileMap;
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
