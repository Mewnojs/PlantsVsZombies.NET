using System;

namespace Sexy.TodLib
{
    public/*internal*/ class ParticleParams
    {
        public ParticleParams(ParticleEffect aParticleEffect, string aParticleName)
        {
            mParticleEffect = aParticleEffect;
            mParticleFileName = aParticleName;
        }

        public ParticleEffect mParticleEffect;

        public string mParticleFileName;
    }
}
