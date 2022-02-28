using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public class PIEmitter
	{
		public PIEmitter()
		{
			this.mValues = new PIValue[42];
			this.mParticleDefVector = new List<PIParticleDef>();
			for (int i = 0; i < 42; i++)
			{
				this.mValues[i] = new PIValue();
			}
		}

		public string mName;

		public PIValue[] mValues;

		public List<PIParticleDef> mParticleDefVector;

		public bool mKeepInOrder;

		public bool mOldestInFront;

		public bool mIsSuperEmitter;

		public enum PIEmitterValue
		{
			VALUE_F_LIFE,
			VALUE_F_NUMBER,
			VALUE_F_VELOCITY,
			VALUE_F_WEIGHT,
			VALUE_F_SPIN,
			VALUE_F_MOTION_RAND,
			VALUE_F_BOUNCE,
			VALUE_F_ZOOM,
			VALUE_LIFE,
			VALUE_NUMBER,
			VALUE_SIZE_X,
			VALUE_SIZE_Y,
			VALUE_VELOCITY,
			VALUE_WEIGHT,
			VALUE_SPIN,
			VALUE_MOTION_RAND,
			VALUE_BOUNCE,
			VALUE_ZOOM,
			VALUE_VISIBILITY,
			VALUE_UNKNOWN3,
			VALUE_TINT_STRENGTH,
			VALUE_EMISSION_ANGLE,
			VALUE_EMISSION_RANGE,
			VALUE_F_LIFE_VARIATION,
			VALUE_F_NUMBER_VARIATION,
			VALUE_F_SIZE_X_VARIATION,
			VALUE_F_SIZE_Y_VARIATION,
			VALUE_F_VELOCITY_VARIATION,
			VALUE_F_WEIGHT_VARIATION,
			VALUE_F_SPIN_VARIATION,
			VALUE_F_MOTION_RAND_VARIATION,
			VALUE_F_BOUNCE_VARIATION,
			VALUE_F_ZOOM_VARIATION,
			VALUE_F_NUMBER_OVER_LIFE,
			VALUE_F_SIZE_X_OVER_LIFE,
			VALUE_F_SIZE_Y_OVER_LIFE,
			VALUE_F_VELOCITY_OVER_LIFE,
			VALUE_F_WEIGHT_OVER_LIFE,
			VALUE_F_SPIN_OVER_LIFE,
			VALUE_F_MOTION_RAND_OVER_LIFE,
			VALUE_F_BOUNCE_OVER_LIFE,
			VALUE_F_ZOOM_OVER_LIFE,
			NUM_VALUES
		}
	}
}
