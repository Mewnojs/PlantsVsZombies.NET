using System;

namespace Sexy.GraphicsLib
{
	public class PIParticleGroup
	{
		public PIParticleGroup()
		{
			this.mIsSuperEmitter = false;
			this.mWasEmitted = false;
			this.mHead = null;
			this.mTail = null;
			this.mCount = 0;
		}

		public PIParticleInstance mHead;

		public PIParticleInstance mTail;

		public int mCount;

		public bool mIsSuperEmitter;

		public bool mWasEmitted;
	}
}
