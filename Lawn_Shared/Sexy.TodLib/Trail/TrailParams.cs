using System;

namespace Sexy.TodLib
{
	internal class TrailParams
	{
		public TrailParams(TrailType aTrailType, string aTrailFileName)
		{
			this.mTrailType = aTrailType;
			this.mTrailFileName = aTrailFileName;
		}

		public TrailType mTrailType;

		public string mTrailFileName;
	}
}
