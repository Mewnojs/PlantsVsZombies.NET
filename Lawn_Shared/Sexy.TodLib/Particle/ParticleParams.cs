using System;

namespace Sexy.TodLib
{
	internal class ParticleParams
	{
		public ParticleParams(ParticleEffect aParticleEffect, string aParticleName)
		{
			this.mParticleEffect = aParticleEffect;
			this.mParticleFileName = aParticleName;
		}

		public ParticleEffect mParticleEffect;

		public string mParticleFileName;
	}
}
