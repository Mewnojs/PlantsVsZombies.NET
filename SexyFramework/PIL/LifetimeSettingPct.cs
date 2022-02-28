using System;

namespace Sexy.PIL
{
	public class LifetimeSettingPct
	{
		public LifetimeSettingPct()
		{
		}

		public LifetimeSettingPct(float f, LifetimeSettings s)
		{
			this.first = f;
			this.second = s;
		}

		public float first;

		public LifetimeSettings second;
	}
}
