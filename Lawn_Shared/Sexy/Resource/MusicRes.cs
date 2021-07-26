using System;

namespace Sexy
{
	internal class MusicRes : BaseRes
	{
		public MusicRes()
		{
			this.mType = ResType.ResType_Music;
		}

		public override void DeleteResource()
		{
			if (this.mSongId >= 0)
			{
				GlobalStaticVars.gSexyAppBase.mMusicInterface.UnloadMusic(this.mSongId);
				this.mSongId = -1;
			}
			base.DeleteResource();
		}

		public int mSongId;
	}
}
