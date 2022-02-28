using System;

namespace Sexy.GraphicsLib
{
	public class PIParticleDefInstance
	{
		public PIParticleDefInstance()
		{
			this.Reset();
		}

		public void Reset()
		{
			this.mNumberAcc = 0f;
			this.mCurNumberVariation = 0f;
			this.mParticlesEmitted = 0;
			this.mTicks = 0;
		}

		public float mNumberAcc;

		public float mCurNumberVariation;

		public int mParticlesEmitted;

		public int mTicks;
	}
}
