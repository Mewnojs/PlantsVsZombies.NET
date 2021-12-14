using System;

namespace Sexy
{
	internal class SoundRes : BaseRes
	{
		public SoundRes()
		{
			mType = ResType.ResType_Sound;
		}

		public override void DeleteResource()
		{
			if (mSoundId >= 0)
			{
				GlobalStaticVars.gSexyAppBase.mSoundManager.ReleaseSound((uint)mSoundId);
				mSoundId = -1;
			}
			base.DeleteResource();
		}

		public int mSoundId;

		public double mVolume;

		public int mPanning;
	}
}
