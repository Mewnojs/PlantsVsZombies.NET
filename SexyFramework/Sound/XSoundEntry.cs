using System;
using Microsoft.Xna.Framework.Audio;

namespace Sexy.Sound
{
	internal class XSoundEntry
	{
		public void Dispose()
		{
			this.m_SoundEffect.Dispose();
		}

		public SoundEffect m_SoundEffect;

		public float m_BaseVolume;

		public float m_BasePan;
	}
}
