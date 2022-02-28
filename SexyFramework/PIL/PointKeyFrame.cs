using System;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class PointKeyFrame
	{
		public PointKeyFrame(int f, SexyPoint s)
		{
			this.first = f;
			this.second = s;
		}

		public int first;

		public SexyPoint second;
	}
}
