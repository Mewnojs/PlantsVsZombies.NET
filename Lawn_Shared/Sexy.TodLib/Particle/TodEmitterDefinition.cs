using System;

namespace Sexy.TodLib
{
	internal class TodEmitterDefinition
	{
		public TodEmitterDefinition()
		{
			this.mImageRow = 0;
			this.mImageCol = 0;
			this.mImageFrames = 1;
			this.mAnimated = 0;
			this.mEmitterType = EmitterType.EMITTER_BOX;
			this.mImage = null;
			this.mName = "";
			this.mOnDuration = "";
		}

		public void Dispose()
		{
		}

		public Image mImage;

		public int mImageCol;

		public int mImageRow;

		public int mImageFrames;

		public int mAnimated;

		public int mParticleFlags;

		public EmitterType mEmitterType;

		public string mName;

		public string mOnDuration;

		public FloatParameterTrack mSystemDuration = new FloatParameterTrack();

		public FloatParameterTrack mCrossFadeDuration = new FloatParameterTrack();

		public FloatParameterTrack mSpawnRate = new FloatParameterTrack();

		public FloatParameterTrack mSpawnMinActive = new FloatParameterTrack();

		public FloatParameterTrack mSpawnMaxActive = new FloatParameterTrack();

		public FloatParameterTrack mSpawnMaxLaunched = new FloatParameterTrack();

		public FloatParameterTrack mEmitterRadius = new FloatParameterTrack();

		public FloatParameterTrack mEmitterOffsetX = new FloatParameterTrack();

		public FloatParameterTrack mEmitterOffsetY = new FloatParameterTrack();

		public FloatParameterTrack mEmitterBoxX = new FloatParameterTrack();

		public FloatParameterTrack mEmitterBoxY = new FloatParameterTrack();

		public FloatParameterTrack mEmitterSkewX = new FloatParameterTrack();

		public FloatParameterTrack mEmitterSkewY = new FloatParameterTrack();

		public FloatParameterTrack mEmitterPath = new FloatParameterTrack();

		public FloatParameterTrack mParticleDuration = new FloatParameterTrack();

		public FloatParameterTrack mLaunchSpeed = new FloatParameterTrack();

		public FloatParameterTrack mLaunchAngle = new FloatParameterTrack();

		public FloatParameterTrack mSystemRed = new FloatParameterTrack();

		public FloatParameterTrack mSystemGreen = new FloatParameterTrack();

		public FloatParameterTrack mSystemBlue = new FloatParameterTrack();

		public FloatParameterTrack mSystemAlpha = new FloatParameterTrack();

		public FloatParameterTrack mSystemBrightness = new FloatParameterTrack();

		public ParticleField[] mParticleFields;

		public int mParticleFieldCount;

		public ParticleField[] mSystemFields;

		public int mSystemFieldCount;

		public FloatParameterTrack mParticleRed = new FloatParameterTrack();

		public FloatParameterTrack mParticleGreen = new FloatParameterTrack();

		public FloatParameterTrack mParticleBlue = new FloatParameterTrack();

		public FloatParameterTrack mParticleAlpha = new FloatParameterTrack();

		public FloatParameterTrack mParticleBrightness = new FloatParameterTrack();

		public FloatParameterTrack mParticleSpinAngle = new FloatParameterTrack();

		public FloatParameterTrack mParticleSpinSpeed = new FloatParameterTrack();

		public FloatParameterTrack mParticleScale = new FloatParameterTrack();

		public FloatParameterTrack mParticleStretch = new FloatParameterTrack();

		public FloatParameterTrack mCollisionReflect = new FloatParameterTrack();

		public FloatParameterTrack mCollisionSpin = new FloatParameterTrack();

		public FloatParameterTrack mClipTop = new FloatParameterTrack();

		public FloatParameterTrack mClipBottom = new FloatParameterTrack();

		public FloatParameterTrack mClipLeft = new FloatParameterTrack();

		public FloatParameterTrack mClipRight = new FloatParameterTrack();

		public FloatParameterTrack mAnimationRate = new FloatParameterTrack();
	}
}
