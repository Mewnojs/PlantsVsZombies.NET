using System;

namespace Sexy.TodLib
{
    public/*internal*/ class TrailParams
    {
        public TrailParams(TrailType aTrailType, string aTrailFileName)
        {
            mTrailType = aTrailType;
            mTrailFileName = aTrailFileName;
        }

        public TrailType mTrailType;

        public string mTrailFileName;
    }
}
