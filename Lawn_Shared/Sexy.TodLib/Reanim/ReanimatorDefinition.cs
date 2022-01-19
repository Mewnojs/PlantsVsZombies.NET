using System;

namespace Sexy.TodLib
{
    public/*internal*/ class ReanimatorDefinition
    {
        public ReanimatorDefinition()
        {
            mFPS = 12f;
            mTrackCount = 0;
            mReanimAtlas = null;
        }

        public void ExtractImages()
        {
            for (int i = 0; i < mTracks.Length; i++)
            {
                mTracks[i].ExtractImages();
            }
        }

        public ReanimatorTrack[] mTracks;

        public short mTrackCount;

        public float mFPS;

        public ReanimAtlas mReanimAtlas;
    }
}
