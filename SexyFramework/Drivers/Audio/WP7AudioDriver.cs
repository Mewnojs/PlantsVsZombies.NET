using System;
using Sexy.Drivers.App;
using Sexy.Sound;

namespace Sexy.Drivers.Audio
{
	public class WP7AudioDriver : IAudioDriver
	{
		public static WP7AudioDriver CreateAudioDriver(SexyAppBase app)
		{
			return new WP7AudioDriver(app);
		}

		public WP7AudioDriver(SexyAppBase app)
		{
		}

		public override void Dispose()
		{
		}

		public override bool InitAudioDriver()
		{
			return true;
		}

		public override SoundManager CreateSoundManager()
		{
			return new XSoundManager(WP7AppDriver.sWP7AppDriverInstance.mContentManager);
		}

		public override MusicInterface CreateMusicInterface()
		{
			return null;
		}
	}
}
