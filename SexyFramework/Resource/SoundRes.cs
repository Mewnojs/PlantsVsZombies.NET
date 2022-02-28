using System;

namespace Sexy.Resource
{
	public class SoundRes : BaseRes
	{
		public SoundRes()
		{
			this.mType = ResType.ResType_Sound;
			this.mSoundId = -1;
			this.mVolume = -1.0;
			this.mPanning = 0;
		}

		public override void DeleteResource()
		{
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				this.mResourceRef.Release();
			}
			else if (this.mSoundId >= 0)
			{
				GlobalMembers.gSexyAppBase.mSoundManager.ReleaseSound(this.mSoundId);
			}
			this.mSoundId = -1;
			if (this.mGlobalPtr != null)
			{
				this.mGlobalPtr.mResObject = -1;
			}
		}

		public override void ApplyConfig()
		{
			if (this.mSoundId == -1)
			{
				return;
			}
			if (this.mResourceRef != null && this.mResourceRef.HasResource())
			{
				return;
			}
			if (this.mVolume >= 0.0)
			{
				GlobalMembers.gSexyAppBase.mSoundManager.SetBaseVolume((uint)this.mSoundId, this.mVolume);
			}
			if (this.mPanning != 0)
			{
				GlobalMembers.gSexyAppBase.mSoundManager.SetBasePan((uint)this.mSoundId, this.mPanning);
			}
		}

		public int mSoundId;

		public double mVolume;

		public int mPanning;
	}
}
