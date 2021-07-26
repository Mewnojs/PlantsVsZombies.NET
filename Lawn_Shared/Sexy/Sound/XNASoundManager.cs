using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Sexy
{
	internal class XNASoundManager : SoundManager
	{
		public XNASoundManager(SexyAppBase theApp)
		{
			this.mEnabled = false;
			this.mContent = theApp.mContentManager;
			this.mInstances = new List<XNASoundInstance>();
			int num = 0;
			while ((long)num < (long)((ulong)XNASoundConstants.MAX_SOUNDS))
			{
				this.mSounds[num] = null;
				num++;
			}
		}

		public override void Release()
		{
			this.ReleaseSounds();
			for (int i = 0; i < this.mInstances.Count; i++)
			{
				this.mInstances[i].Release();
			}
		}

		public override void Enable(bool enable)
		{
			this.mEnabled = enable;
		}

		public override bool Initialized()
		{
			return true;
		}

		public override bool LoadSound(uint theSfxID, string theFilename)
		{
			SoundEffect mSound = this.mContent.Load<SoundEffect>(theFilename);
			XNASoundEntry xnasoundEntry = new XNASoundEntry();
			xnasoundEntry.mSound = mSound;
			this.mSounds[(int)((UIntPtr)theSfxID)] = xnasoundEntry;
			return true;
		}

		public override int LoadSound(string theFilename)
		{
			int num = 0;
			while ((long)num < (long)((ulong)XNASoundConstants.MAX_SOUNDS))
			{
				if (this.mInstances[num] == null)
				{
					if (this.LoadSound((uint)num, theFilename))
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
			this.mSounds[(int)((UIntPtr)theSfxID)].Dispose();
			this.mSounds[(int)((UIntPtr)theSfxID)] = null;
		}

		public override void SetVolume(double theVolume)
		{
			this.SetMasterVolume(theVolume);
		}

		public override bool SetBaseVolume(uint theSfxID, double theBaseVolume)
		{
			if (theBaseVolume < 0.0 || theBaseVolume > 1.0)
			{
				return false;
			}
			this.mSounds[(int)((UIntPtr)theSfxID)].mBaseVolume = (float)theBaseVolume;
			for (int i = 0; i < this.mInstances.Count; i++)
			{
				if (this.mInstances[i].SoundId == theSfxID)
				{
					this.mInstances[i].SetBaseVolume((double)this.mSounds[(int)((UIntPtr)theSfxID)].mBaseVolume);
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
			this.mSounds[(int)((UIntPtr)theSfxID)].mBasePan = (float)theBasePan / 100f;
			for (int i = 0; i < this.mInstances.Count; i++)
			{
				if (this.mInstances[i].SoundId == theSfxID)
				{
					this.mInstances[i].SetBasePan(theBasePan);
				}
			}
			return true;
		}

		public override SoundInstance GetSoundInstance(uint theSfxID)
		{
			if (this.mInstances.Count >= 16)
			{
				return null;
			}
			SoundEffectInstance instance = this.mSounds[(int)((UIntPtr)theSfxID)].mSound.CreateInstance();
			XNASoundInstance newXNASoundInstance = XNASoundInstance.GetNewXNASoundInstance(theSfxID, instance);
			this.mInstances.Add(newXNASoundInstance);
			return newXNASoundInstance;
		}

		public override void ReleaseSounds()
		{
			for (uint num = 0U; num < XNASoundConstants.MAX_SOUNDS; num += 1U)
			{
				this.ReleaseSound(num);
			}
		}

		public override void ReleaseChannels()
		{
		}

		public override double GetMasterVolume()
		{
			return (double)SoundEffect.MasterVolume;
		}

		public override void SetMasterVolume(double theVolume)
		{
			SoundEffect.MasterVolume = (float)theVolume * 0.65f;
		}

		public override void Flush()
		{
		}

		public override void StopAllSounds()
		{
			for (int i = 0; i < this.mInstances.Count; i++)
			{
				this.mInstances[i].Stop();
			}
		}

		public override int GetFreeSoundId()
		{
			int num = 0;
			while ((long)num < (long)((ulong)XNASoundConstants.MAX_SOUNDS))
			{
				if (this.mSounds[num] == null)
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
			while ((long)num2 < (long)((ulong)XNASoundConstants.MAX_SOUNDS))
			{
				if (this.mSounds[num2] != null)
				{
					num++;
				}
				num2++;
			}
			return num;
		}

		public override void Update()
		{
			for (int i = this.mInstances.Count - 1; i >= 0; i--)
			{
				if (this.mInstances[i].IsReleased())
				{
					this.mInstances[i].PrepareForReuse();
					this.mInstances.RemoveAt(i);
				}
				else if (this.mInstances[i].IsDormant())
				{
					if (!this.mInstances[i].IsReleased())
					{
						this.mInstances[i].Release();
					}
					this.mInstances[i].PrepareForReuse();
					this.mInstances.RemoveAt(i);
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
