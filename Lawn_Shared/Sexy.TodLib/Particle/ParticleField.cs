using System;

namespace Sexy.TodLib
{
	public/*internal*/ class ParticleField
	{
		public ParticleField()
		{
			this.mFieldType = ParticleFieldType.FIELD_INVALID;
		}

		public ParticleFieldType mFieldType;

		public FloatParameterTrack mX = new FloatParameterTrack();

		public FloatParameterTrack mY = new FloatParameterTrack();
	}
}
