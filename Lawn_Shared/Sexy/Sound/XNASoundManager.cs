using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Sexy
{
    internal class XNASoundConstants
    {
        public static uint MAX_SOUNDS = 256U;
    }

    internal class XNASoundEntry
    {
        public void Dispose()
        {
            mSound.Dispose();
        }

        public float mBaseVolume = 1f;

        public float mBasePan;

        public SoundEffect mSound;
    }

    internal class XNASoundManager : SoundManager
    {
        public XNASoundManager(SexyAppBase theApp)
        {
            mEnabled = false;
            mContent = theApp.mContentManager;
            mInstances = new List<XNASoundInstance>();
            int num = 0;
            while (num < (long)((ulong)XNASoundConstants.MAX_SOUNDS))
            {
                mSounds[num] = null;
                num++;
            }
        }

        public override void Release()
        {
            ReleaseSounds();
            for (int i = 0; i < mInstances.Count; i++)
            {
                mInstances[i].Release();
            }
        }

        public override void Enable(bool enable)
        {
            mEnabled = enable;
        }

        public override bool Initialized()
        {
            return true;
        }

        public override bool LoadSound(uint theSfxID, string theFilename)
        {
            SoundEffect aSound = mContent.Load<SoundEffect>(theFilename);
            XNASoundEntry xnasoundEntry = new XNASoundEntry();
            xnasoundEntry.mSound = aSound;
            mSounds[(int)((UIntPtr)theSfxID)] = xnasoundEntry;
            return true;
        }

        public override int LoadSound(string theFilename)
        {
            int num = 0;
            while (num < (long)((ulong)XNASoundConstants.MAX_SOUNDS))
            {
                if (mInstances[num] == null)
                {
                    if (LoadSound((uint)num, theFilename))
                    {
                        return num;
                    }
                    return -1;
                }
                else
                {
                    num++;
                }
            }
            return -1;
        }

        public override void ReleaseSound(uint theSfxID)
        {
            mSounds[(int)((UIntPtr)theSfxID)].Dispose();
            mSounds[(int)((UIntPtr)theSfxID)] = null;
        }

        public override void SetVolume(double theVolume)
        {
            SetMasterVolume(theVolume);
        }

        public override bool SetBaseVolume(uint theSfxID, double theBaseVolume)
        {
            if (theBaseVolume < 0.0 || theBaseVolume > 1.0)
            {
                return false;
            }
            mSounds[(int)((UIntPtr)theSfxID)].mBaseVolume = (float)theBaseVolume;
            for (int i = 0; i < mInstances.Count; i++)
            {
                if (mInstances[i].SoundId == theSfxID)
                {
                    mInstances[i].SetBaseVolume(mSounds[(int)((UIntPtr)theSfxID)].mBaseVolume);
                }
            }
            return true;
        }

        public override bool SetBasePan(uint theSfxID, int theBasePan)
        {
            if (theBasePan < -100 || theBasePan > 100)
            {
                return false;
            }
            mSounds[(int)((UIntPtr)theSfxID)].mBasePan = theBasePan / 100f;
            for (int i = 0; i < mInstances.Count; i++)
            {
                if (mInstances[i].SoundId == theSfxID)
                {
                    mInstances[i].SetBasePan(theBasePan);
                }
            }
            return true;
        }

        public override SoundInstance GetSoundInstance(uint theSfxID)
        {
            if (mInstances.Count >= ACTIVE_SOUNDS_LIMIT)
            {
                return null;
            }
            SoundEffectInstance instance = mSounds[(int)((UIntPtr)theSfxID)].mSound.CreateInstance();
            XNASoundInstance newXNASoundInstance = XNASoundInstance.GetNewXNASoundInstance(theSfxID, instance);
            mInstances.Add(newXNASoundInstance);
            return newXNASoundInstance;
        }

        public override void ReleaseSounds()
        {
            for (uint num = 0U; num < XNASoundConstants.MAX_SOUNDS; num += 1U)
            {
                ReleaseSound(num);
            }
        }

        public override void ReleaseChannels()
        {
        }

        public override double GetMasterVolume()
        {
            return SoundEffect.MasterVolume;
        }

        public override void SetMasterVolume(double theVolume)
        {
            SoundEffect.MasterVolume = (float)theVolume * SOUND_LIMIT;
        }

        public override void Flush()
        {
        }

        public override void StopAllSounds()
        {
            for (int i = 0; i < mInstances.Count; i++)
            {
                mInstances[i].Stop();
            }
        }

        public override int GetFreeSoundId()
        {
            int num = 0;
            while (num < (long)((ulong)XNASoundConstants.MAX_SOUNDS))
            {
                if (mSounds[num] == null)
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        public override int GetNumSounds()
        {
            int num = 0;
            int num2 = 0;
            while (num2 < (long)((ulong)XNASoundConstants.MAX_SOUNDS))
            {
                if (mSounds[num2] != null)
                {
                    num++;
                }
                num2++;
            }
            return num;
        }

        public override void Update()
        {
            for (int i = mInstances.Count - 1; i >= 0; i--)
            {
                if (mInstances[i].IsReleased())
                {
                    mInstances[i].PrepareForReuse();
                    mInstances.RemoveAt(i);
                }
                else if (mInstances[i].IsDormant())
                {
                    if (!mInstances[i].IsReleased())
                    {
                        mInstances[i].Release();
                    }
                    mInstances[i].PrepareForReuse();
                    mInstances.RemoveAt(i);
                }
            }
        }

        private const int ACTIVE_SOUNDS_LIMIT = 16;

        private const float SOUND_LIMIT = 0.65f;

        private bool mEnabled;

        private ContentManager mContent;

        private XNASoundEntry[] mSounds = new XNASoundEntry[XNASoundConstants.MAX_SOUNDS];

        private List<XNASoundInstance> mInstances;
    }
}
