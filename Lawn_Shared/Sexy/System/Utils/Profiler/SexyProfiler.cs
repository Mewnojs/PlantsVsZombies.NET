using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class SexyProfiler
	{
		public SexyProfiler()
		{
			this.mActive = false;
			this.mProfiles = new List<Profile>();
		}

		public void Active(bool active)
		{
			this.mActive = active;
		}

		public void StartFrame()
		{
			this.mProfiles.Clear();
		}

		public void StartProfile(string name)
		{
			if (!this.mActive)
			{
				return;
			}
			Profile profile = new Profile();
			profile.mSectionName = name;
			profile.mStart = this.GetCurrentTime();
			this.mProfiles.Add(profile);
		}

		public void EndProfile(string name)
		{
			if (!this.mActive)
			{
				return;
			}
			for (int i = 0; i < this.mProfiles.Count; i++)
			{
				if (this.mProfiles[i].mSectionName == name)
				{
					this.mProfiles[i].mEnd = this.GetCurrentTime();
					return;
				}
			}
		}

		public int GetCurrentTime()
		{
			return Environment.TickCount;
		}

		public void PrintProfiles()
		{
			if (!this.mActive)
			{
				return;
			}
			for (int i = 0; i < this.mProfiles.Count; i++)
			{
				int num = this.mProfiles[i].mEnd - this.mProfiles[i].mStart;
				Debug.OutputDebug<string>("Section Name: " + this.mProfiles[i].mSectionName + "\n\tTotal Time(ms): " + num.ToString());
			}
		}

		private List<Profile> mProfiles;

		private bool mActive;
	}
}
