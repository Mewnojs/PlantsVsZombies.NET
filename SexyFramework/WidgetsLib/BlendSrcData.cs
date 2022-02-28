using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;

namespace Sexy.WidgetsLib
{
	public class BlendSrcData
	{
		public List<PAParticleEffect> mParticleEffectVector = new List<PAParticleEffect>();

		public PATransform mTransform = new PATransform();

		public SexyColor mColor = default(SexyColor);
	}
}
