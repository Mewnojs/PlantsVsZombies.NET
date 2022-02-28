using System;
using Sexy.GraphicsLib;
using Sexy.Resource;

namespace Sexy.WidgetsLib
{
	public class PAParticleEffect
	{
		public ResourceRef mResourceRef = new ResourceRef();

		public PIEffect mEffect;

		public string mName;

		public int mLastUpdated;

		public bool mBehind;

		public bool mAttachEmitter;

		public bool mTransform;

		public double mXOfs;

		public double mYOfs;
	}
}
