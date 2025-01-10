using System;

namespace Sexy.TodLib
{
    public/*internal*/ static class GlobalMembersTrail
    {
        public static void TrailLoadDefinitions(TrailParams[] theTrailParamArray, int theTrailParamArraySize)
        {
            GlobalMembersTrail.gTrailParamArraySize = theTrailParamArraySize;
            GlobalMembersTrail.gTrailParamArray = theTrailParamArray;
            GlobalMembersTrail.gTrailDefCount = theTrailParamArraySize;
            GlobalMembersTrail.gTrailDefArray = new TrailDefinition[GlobalMembersTrail.gTrailDefCount];
            for (int i = 0; i < GlobalMembersTrail.gTrailParamArraySize; i++)
            {
                TrailParams trailParams = theTrailParamArray[i];
                ref TrailDefinition trailDefinition = ref GlobalMembersTrail.gTrailDefArray[i];
                if (!GlobalMembersTrail.TrailLoadADef(ref trailDefinition, trailParams.mTrailFileName))
                {
                    new string(new char[256]);
                    string.Format("Failed to load trail '{0}'", trailParams.mTrailFileName);
                }
            }
        }

        public static void TrailFreeDefinitions()
        {
            GlobalMembersTrail.gTrailDefArray = null;
            GlobalMembersTrail.gTrailDefArray = null;
            GlobalMembersTrail.gTrailDefCount = 0;
            GlobalMembersTrail.gTrailParamArray = null;
            GlobalMembersTrail.gTrailParamArraySize = 0;
        }

        public static bool TrailLoadADef(ref TrailDefinition theTrailDef, string theTrailFileName)
        {
            if (!SexyGlobal.gSexyAppBase.mResourceManager.LoadTrail(theTrailFileName, ref theTrailDef))
            {
                return false;
            }
            Definition.FloatTrackSetDefault(ref theTrailDef.mWidthOverLength, 1f);
            Definition.FloatTrackSetDefault(ref theTrailDef.mWidthOverTime, 1f);
            Definition.FloatTrackSetDefault(ref theTrailDef.mTrailDuration, 100f);
            Definition.FloatTrackSetDefault(ref theTrailDef.mAlphaOverLength, 1f);
            Definition.FloatTrackSetDefault(ref theTrailDef.mAlphaOverTime, 1f);
            return true;
        }

        public static int gTrailDefCount;

        public static TrailDefinition[] gTrailDefArray;

        public static int gTrailParamArraySize;

        public static TrailParams[] gTrailParamArray;
    }
}
