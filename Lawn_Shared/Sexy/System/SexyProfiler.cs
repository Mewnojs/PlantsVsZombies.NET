using System;
using System.Collections.Generic;

namespace Sexy
{
    internal class Profile
    {
        public string mSectionName;

        public int mStart;

        public int mEnd;
    }

    internal class SexyProfiler
    {
        public SexyProfiler()
        {
            mActive = false;
            mProfiles = new List<Profile>();
        }

        public void Active(bool active)
        {
            mActive = active;
        }

        public void StartFrame()
        {
            mProfiles.Clear();
        }

        public void StartProfile(string name)
        {
            if (!mActive)
            {
                return;
            }
            Profile profile = new Profile();
            profile.mSectionName = name;
            profile.mStart = GetCurrentTime();
            mProfiles.Add(profile);
        }

        public void EndProfile(string name)
        {
            if (!mActive)
            {
                return;
            }
            for (int i = 0; i < mProfiles.Count; i++)
            {
                if (mProfiles[i].mSectionName == name)
                {
                    mProfiles[i].mEnd = GetCurrentTime();
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
            if (!mActive)
            {
                return;
            }
            for (int i = 0; i < mProfiles.Count; i++)
            {
                int num = mProfiles[i].mEnd - mProfiles[i].mStart;
                Debug.OutputDebug<string>("Section Name: " + mProfiles[i].mSectionName + "\n\tTotal Time(ms): " + num.ToString());
            }
        }

        private List<Profile> mProfiles;

        private bool mActive;
    }
}
