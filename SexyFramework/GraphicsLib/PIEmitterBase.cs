using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public class PIEmitterBase : IDisposable
	{
		public virtual void Dispose()
		{
			this.mParticleGroup = null;
			this.mParticleDefInstanceVector.Clear();
		}

		public List<PIParticleDefInstance> mParticleDefInstanceVector = new List<PIParticleDefInstance>();

		public PIParticleGroup mParticleGroup = new PIParticleGroup();
	}
}
