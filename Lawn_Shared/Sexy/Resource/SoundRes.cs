using System;

namespace Sexy
{
	internal class SoundRes : BaseRes
	{
		public SoundRes()
		{
			this.mType = ResType.ResType_Sound;
		}

		public override void DeleteResource()
		{
			if (this.mSoundId >= 0)
			{
				GlobalStaticVars.gSexyAppBase.mSoundManager.ReleaseSound((uint)this.mSoundId);
				this.mSoundId = -1;
			}
			base.DeleteResource();
		}

		public int mSoundId;

		public double mVolume;

		public int mPanning;
	}
}
