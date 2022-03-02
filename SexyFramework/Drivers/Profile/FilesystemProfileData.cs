using System;
using Sexy.File;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.Drivers.Profile
{
	public class FilesystemProfileData : IProfileData
	{
		public FilesystemProfileData(UserProfile player)
		{
			this.mPlayer = player;
		}

		public override int GetId()
		{
			return this.mId;
		}

		public override string GetName()
		{
			return this.mName;
		}

		public override uint GetGamepadIndex()
		{
			return this.mGamepad;
		}

		public override void SetGamepadIndex(uint gamepad)
		{
			this.mGamepad = gamepad;
		}

		public override bool SignedIn()
		{
			return true;
		}

		public override bool IsSigningIn()
		{
			return false;
		}

		public override bool IsOnline()
		{
			return false;
		}

		public override void DeleteUserFiles()
		{
			string theFileName = string.Format("userdata/user{0}.dat", this.GetId());
			StorageFile.DeleteFile(theFileName);
		}

		public override EProfileIOState LoadDetails()
		{
			SexyBuffer buffer = new SexyBuffer();
			string theFileName = string.Format("userdata/user{0}.dat", this.GetId());
			if (!StorageFile.ReadBufferFromFile(theFileName, buffer))
			{
				if (!StorageFile.FileExists(theFileName))
				{
					this.mPlayer.Reset();
					this.mLoaded = true;
					return EProfileIOState.PROFILE_IO_SUCCESS;
				}
				this.mLoaded = false;
				return EProfileIOState.PROFILE_IO_ERROR;
			}
			else
			{
				if (!this.mPlayer.ReadProfileSettings(buffer))
				{
					this.mLoaded = false;
					return EProfileIOState.PROFILE_IO_ERROR;
				}
				this.mLoaded = true;
				return EProfileIOState.PROFILE_IO_SUCCESS;
			}
		}

		public override bool IsLoading()
		{
			return false;
		}

		public override bool IsLoaded()
		{
			return this.mLoaded;
		}

		public override EProfileIOState SaveDetails()
		{
			SexyBuffer buffer = new SexyBuffer();
			if (!this.mPlayer.WriteProfileSettings(buffer))
			{
				this.mSaved = false;
				return EProfileIOState.PROFILE_IO_ERROR;
			}
			StorageFile.MakeDir("userdata");
			string theFileName = string.Format("userdata/user{0}.dat", this.GetId());
			if (!StorageFile.WriteBufferToFile(theFileName, buffer))
			{
				this.mSaved = false;
				return EProfileIOState.PROFILE_IO_ERROR;
			}
			this.mSaved = true;
			return EProfileIOState.PROFILE_IO_SUCCESS;
		}

		public override bool IsSaving()
		{
			return false;
		}

		public override bool IsSaved()
		{
			return this.mSaved;
		}

		public override bool HasError()
		{
			return false;
		}

		public bool ReadSummary(SexyBuffer data)
		{
			this.mName = data.ReadStringWithEncoding();
			this.mId = (int)data.ReadLong();
			this.mUseSeq = (int)data.ReadLong();
			return true;
		}

		public bool WriteSummary(SexyBuffer data)
		{
			data.WriteStringWithEncoding(this.mName);
			data.WriteLong(this.mId);
			data.WriteLong(this.mUseSeq);
			return true;
		}

		public override Image GetPlayerIcon()
		{
			return null;
		}

		public override bool IsAchievementUnlocked(uint id)
		{
			return false;
		}

		public override IAchievementContext StartUnlockAchievement(uint id)
		{
			return null;
		}

		public void SetId(int id)
		{
			this.mId = id;
		}

		public void SetUseSeq(int useSeq)
		{
			this.mUseSeq = useSeq;
		}

		public void SetName(string name)
		{
			this.mName = name;
		}

		public int getId()
		{
			return this.mId;
		}

		public int getUseSeq()
		{
			return this.mUseSeq;
		}

		public string getName()
		{
			return this.mName;
		}

		private UserProfile mPlayer;

		private int mId = -1;

		private int mUseSeq = -1;

		private string mName = "";

		private uint mGamepad;

		private bool mLoaded;

		private bool mSaved;
	}
}
