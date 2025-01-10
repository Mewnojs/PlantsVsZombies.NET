using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
    public/*internal*/ class TrailHolder
    {
        public void Dispose()
        {
            DisposeHolder();
        }

        public void InitializeHolder()
        {
            mTrails.Capacity = TodCommon.MAX_GLOBAL_ALLOCATORS;
        }

        public void DisposeHolder()
        {
            if (mTrails != null)
            {
                mTrails.Clear();
            }
        }

        public Trail AllocTrail(int theRenderOrder, TrailType theTrailType)
        {
            TrailDefinition theDefinition = GlobalMembersTrail.gTrailDefArray[(int)theTrailType];
            return AllocTrailFromDef(theRenderOrder, theDefinition);
        }

        public Trail AllocTrailFromDef(int theRenderOrder, TrailDefinition theDefinition)
        {
            if (mTrails.Count == mTrails.Capacity)
            {
                return null;
            }
            Trail trail = new Trail();
            trail.mTrailHolder = this;
            trail.mDefinition = theDefinition;
            float theInterp = TodCommon.RandRangeFloat(0f, 1f);
            trail.mTrailDuration = (int)Definition.FloatTrackEvaluate(ref trail.mDefinition.mTrailDuration, 0f, theInterp);
            mTrails.Add(trail);
            return trail;
        }

        public List<Trail> mTrails = new List<Trail>();
    }
}
