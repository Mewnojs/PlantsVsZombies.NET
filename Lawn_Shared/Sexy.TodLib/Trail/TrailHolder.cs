using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	public/*internal*/ class TrailHolder
	{
		public void Dispose()
		{
			this.DisposeHolder();
		}

		public void InitializeHolder()
		{
			this.mTrails.Capacity = 128;
		}

		public void DisposeHolder()
		{
			if (this.mTrails != null)
			{
				this.mTrails.Clear();
			}
		}

		public Trail AllocTrail(int theRenderOrder, TrailType theTrailType)
		{
			TrailDefinition theDefinition = GlobalMembersTrail.gTrailDefArray[(int)theTrailType];
			return this.AllocTrailFromDef(theRenderOrder, theDefinition);
		}

		public Trail AllocTrailFromDef(int theRenderOrder, TrailDefinition theDefinition)
		{
			if (this.mTrails.Count == this.mTrails.Capacity)
			{
				return null;
			}
			Trail trail = new Trail();
			trail.mTrailHolder = this;
			trail.mDefinition = theDefinition;
			float theInterp = TodCommon.RandRangeFloat(0f, 1f);
			trail.mTrailDuration = (int)Definition.FloatTrackEvaluate(ref trail.mDefinition.mTrailDuration, 0f, theInterp);
			this.mTrails.Add(trail);
			return trail;
		}

		public List<Trail> mTrails = new List<Trail>();
	}
}
