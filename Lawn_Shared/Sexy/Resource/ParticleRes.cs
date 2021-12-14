using System;
using Sexy.TodLib;

namespace Sexy
{
	internal class ParticleRes : BaseRes
	{
		public ParticleRes()
		{
			mType = ResType.ResType_Particle;
		}

		public override void DeleteResource()
		{
			if (mParticle != null)
			{
				mParticle = null;
			}
			base.DeleteResource();
		}

		public TodParticleDefinition mParticle;
	}
}
