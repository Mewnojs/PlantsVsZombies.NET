using System;

namespace Sexy.TodLib
{
	public/*internal*/ class TodParticleDefinition
	{
		public TodParticleDefinition()
		{
			mEmitterDefs = null;
			mEmitterDefCount = 0;
		}

		public TodEmitterDefinition[] mEmitterDefs;

		public int mEmitterDefCount;
	}
}
