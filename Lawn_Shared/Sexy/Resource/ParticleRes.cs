using System;
using Sexy.TodLib;

namespace Sexy
{
	internal class ParticleRes : BaseRes
	{
		public ParticleRes()
		{
			this.mType = ResType.ResType_Particle;
		}

		public override void DeleteResource()
		{
			if (this.mParticle != null)
			{
				this.mParticle = null;
			}
			base.DeleteResource();
		}

		public TodParticleDefinition mParticle;
	}
}
