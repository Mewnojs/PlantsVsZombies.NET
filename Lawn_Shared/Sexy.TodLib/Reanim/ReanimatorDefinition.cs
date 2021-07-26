using System;

namespace Sexy.TodLib
{
	internal class ReanimatorDefinition
	{
		public ReanimatorDefinition()
		{
			this.mFPS = 12f;
			this.mTrackCount = 0;
			this.mReanimAtlas = null;
		}

		public void ExtractImages()
		{
			for (int i = 0; i < this.mTracks.Length; i++)
			{
				this.mTracks[i].ExtractImages();
			}
		}

		public ReanimatorTrack[] mTracks;

		public short mTrackCount;

		public float mFPS;

		public ReanimAtlas mReanimAtlas;
	}
}
