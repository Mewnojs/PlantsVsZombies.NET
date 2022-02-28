using System;

namespace Sexy.PIL
{
	public class DeflectorCollInfo
	{
		public DeflectorCollInfo()
		{
		}

		public DeflectorCollInfo(int f, bool b)
		{
			this.mLastCollFrame = f;
			this.mIgnoresDeflector = b;
		}

		public int mLastCollFrame;

		public bool mIgnoresDeflector;
	}
}
