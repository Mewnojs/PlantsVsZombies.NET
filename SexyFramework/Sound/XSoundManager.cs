using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Sexy.Sound
{
	public class XSoundManager : SoundManager
	{
		public XSoundManager(ContentManager cmgr)
		{
			this.mContent = cmgr;
			this.mInstances = new List<XSoundInstance>();
			for (int i = 0; i < XSoundManager.MAX_SOURCE_SOUNDS; i++)
			{
				this.m_sounds[i] = null;
			}
		}

		public override int GetFreeSoundId()
		{
			for (int i = 0; i < XSoundManager.MAX_SOURCE_SOUNDS; i++)
			{
				if (this.m_sounds[i] == null)
				{
					return i;
				}
			}
			return -1;
		}

		public override void ReleaseSound(int theSfxID)
		{
			if (this.m_sounds[theSfxID] != null)
			{
				this.m_sounds[theSfxID] = null;
			}
		}

		public override bool LoadSound(uint theSfxID, string theFilename)
		{
			SoundEffect soundEffect = this.mContent.Load<SoundEffect>(theFilename);
			XSoundEntry xsoundEntry = new XSoundEntry();
			xsoundEntry.m_SoundEffect = soundEffect;
			this.m_sounds[(int)((UIntPtr)theSfxID)] = xsoundEntry;
			return true;
		}

		public override int LoadSound(string theFilename)
		{
			int i = 0;
			while (i < XSoundManager.MAX_SOURCE_SOUNDS)
			{
				if (this.mInstances[i] == null)
				{
					if (this.LoadSound((uint)i, theFilename))
					{
						return i;
					}
					return -1;
				}
				else
				{
					i++;
				}
			}
			return -1;
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

		public override bool Initialized()
		{
			return true;
		}

		public override void SetVolume(double theVolume)
		{
			this.SetMasterVolume(theVolume);
		}

		public override void SetBasePan(uint theSfxID, int theBasePan)
		{
			if (theBasePan < -100 || theBasePan > 100)
			{
				return;
			}
			this.m_sounds[(int)((UIntPtr)theSfxID)].m_BasePan = (float)theBasePan / 100f;
			for (int i = 0; i < this.mInstances.Count; i++)
			{
				if (this.mInstances[i].m_SoundID == (int)theSfxID)
				{
					this.mInstances[i].SetBasePan(theBasePan);
				}
			}
		}

		public override void SetBaseVolume(uint theSfxID, double theBaseVolume)
		{
			if (theBaseVolume < 0.0 || theBaseVolume > 1.0)
			{
				return;
			}
			this.m_sounds[(int)((UIntPtr)theSfxID)].m_BaseVolume = (float)theBaseVolume;
			for (int i = 0; i < this.mInstances.Count; i++)
			{
				if (this.mInstances[i].m_SoundID == (int)theSfxID)
				{
					this.mInstances[i].SetBaseVolume((double)this.m_sounds[(int)((UIntPtr)theSfxID)].m_BaseVolume);
				}
			}
		}

		public void ReleaseFreeChannels()
		{
		}

		public override SoundInstance GetSoundInstance(int theSfxID)
		{
			if (this.mInstances.Count >= XSoundManager.ACTIVE_SOUNDS_LIMIT)
			{
				return null;
			}
			SoundEffectInstance instance = this.m_sounds[theSfxID].m_SoundEffect.CreateInstance();
			XSoundInstance newXSoundInstance = XSoundInstance.GetNewXSoundInstance(theSfxID, instance);
			this.mInstances.Add(newXSoundInstance);
			return newXSoundInstance;
		}

		public override SoundInstance GetExistSoundInstance(int theSfxID)
		{
			if (theSfxID > XSoundManager.MAX_SOURCE_SOUNDS)
			{
				return null;
			}
			if (this.m_sounds[theSfxID] == null)
			{
				return null;
			}
			for (int i = 0; i < this.mInstances.Count; i++)
			{
				if (this.mInstances[i] != null && this.mInstances[i].m_SoundID == theSfxID)
				{
					return this.mInstances[i];
				}
			}
			return null;
		}

		public override void ReleaseSounds()
		{
			for (int i = 0; i < XSoundManager.MAX_SOURCE_SOUNDS; i++)
			{
				if (this.m_sounds[i] != null)
				{
					this.m_sounds[i] = null;
				}
			}
		}

		public override void ReleaseChannels()
		{
		}

		public override double GetMasterVolume()
		{
			return (double)this.m_MasterVolume;
		}

		public override void SetMasterVolume(double theVolume)
		{
			this.m_MasterVolume = (float)theVolume;
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

		public override int GetNumSounds()
		{
			int num = 0;
			for (int i = 0; i < XSoundManager.MAX_SOURCE_SOUNDS; i++)
			{
				if (this.m_sounds[i] != null)
				{
					num++;
				}
			}
			return num;
		}

		public static int MAX_SOURCE_SOUNDS = 4096;

		public static int ACTIVE_SOUNDS_LIMIT = 16;

		private ContentManager mContent;

		private XSoundEntry[] m_sounds = new XSoundEntry[XSoundManager.MAX_SOURCE_SOUNDS];

		private List<XSoundInstance> mInstances;
	}
}
