using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class PIEmitterInstance : PIEmitterBase
	{
		public PIEmitterInstance()
		{
			this.mWasActive = false;
			this.mWithinLifeFrame = true;
			this.mSuperEmitterGroup.mIsSuperEmitter = true;
			this.mTransform.LoadIdentity();
			this.mNumberScale = 1f;
			this.mVisible = true;
		}

		public void SetVisible(bool isVisible)
		{
			this.mVisible = isVisible;
		}

		public PIEmitterInstanceDef mEmitterInstanceDef;

		public bool mWasActive;

		public bool mWithinLifeFrame;

		public List<PIParticleDefInstance> mSuperEmitterParticleDefInstanceVector = new List<PIParticleDefInstance>();

		public PIParticleGroup mSuperEmitterGroup = new PIParticleGroup();

		public SexyColor mTintColor = default(SexyColor);

		public SharedImageRef mMaskImage = new SharedImageRef();

		public SexyTransform2D mTransform = new SexyTransform2D(false);

		public Vector2 mOffset = default(Vector2);

		public float mNumberScale;

		public bool mVisible;
	}
}
