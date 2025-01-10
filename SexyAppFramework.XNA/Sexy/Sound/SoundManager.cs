using System;

namespace Sexy
{
    public abstract class SoundManager
    {
        public abstract void Release();

        public abstract void Enable(bool enable);

        public abstract bool Initialized();

        public abstract bool LoadSound(uint theSfxID, string theFilename);

        public abstract int LoadSound(string theFilename);

        public abstract void ReleaseSound(uint theSfxID);

        public abstract void SetVolume(double theVolume);

        public abstract bool SetBaseVolume(uint theSfxID, double theBaseVolume);

        public abstract bool SetBasePan(uint theSfxID, int theBasePan);

        public abstract SoundInstance GetSoundInstance(uint theSfxID);

        public abstract void ReleaseSounds();

        public abstract void ReleaseChannels();

        public abstract double GetMasterVolume();

        public abstract void SetMasterVolume(double theVolume);

        public abstract void Flush();

        public abstract void StopAllSounds();

        public abstract int GetFreeSoundId();

        public abstract int GetNumSounds();

        public abstract void Update();
    }
}
