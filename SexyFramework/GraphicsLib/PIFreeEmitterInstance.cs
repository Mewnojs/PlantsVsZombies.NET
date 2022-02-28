using System;

namespace Sexy.GraphicsLib
{
	public class PIFreeEmitterInstance : PIParticleInstance
	{
		public PIFreeEmitterInstance()
		{
			this.mEmitter.mParticleGroup.mWasEmitted = true;
		}

		public override void Dispose()
		{
			base.Dispose();
			this.mEmitter.Dispose();
		}

		public PIEmitterBase mEmitter = new PIEmitterBase();
	}
}
