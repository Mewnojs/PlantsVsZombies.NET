using System;

namespace Sexy.TodLib
{
    public/*internal*/ class ParticleField
    {
        public ParticleField()
        {
            mFieldType = ParticleFieldType.Invalid;
        }

        public ParticleFieldType mFieldType;

        public FloatParameterTrack mX = new FloatParameterTrack();

        public FloatParameterTrack mY = new FloatParameterTrack();
    }
}
