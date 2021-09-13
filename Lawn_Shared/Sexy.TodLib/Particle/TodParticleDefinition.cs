using System;

namespace Sexy.TodLib
{
	public/*internal*/ class TodParticleDefinition
	{
		public TodParticleDefinition()
		{
			this.mEmitterDefs = null;
			this.mEmitterDefCount = 0;
		}

		public TodEmitterDefinition[] mEmitterDefs;

		public int mEmitterDefCount;
	}
}
