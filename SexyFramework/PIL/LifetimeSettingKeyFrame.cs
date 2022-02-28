using System;

namespace Sexy.PIL
{
	public class LifetimeSettingKeyFrame
	{
		public LifetimeSettingKeyFrame()
		{
		}

		public LifetimeSettingKeyFrame(int f, LifetimeSettings s)
		{
			this.first = f;
			this.second = s;
		}

		public int first;

		public LifetimeSettings second;
	}
}
