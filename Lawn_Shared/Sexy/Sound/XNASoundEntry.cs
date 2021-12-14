using System;
using Microsoft.Xna.Framework.Audio;

namespace Sexy
{
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
}
