using System;

namespace Sexy
{
	internal class MusicRes : BaseRes
	{
		public MusicRes()
		{
			mType = ResType.ResType_Music;
		}

		public override void DeleteResource()
		{
			if (mSongId >= 0)
			{
				GlobalStaticVars.gSexyAppBase.mMusicInterface.UnloadMusic(mSongId);
				mSongId = -1;
			}
			base.DeleteResource();
		}

		public int mSongId;
	}
}
