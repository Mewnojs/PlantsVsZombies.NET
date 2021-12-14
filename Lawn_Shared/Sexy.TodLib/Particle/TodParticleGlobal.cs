using System;
using Microsoft.Xna.Framework;

namespace Sexy.TodLib
{
	internal static class TodParticleGlobal
	{
		public static void TodParticleLoadDefinitions(ref ParticleParams[] theParticleParamArray, int theParticleParamArraySize)
		{
			TodParticleGlobal.gParticleParamArraySize = theParticleParamArraySize;
			TodParticleGlobal.gParticleParamArray = theParticleParamArray;
			TodParticleGlobal.gParticleDefCount = theParticleParamArraySize;
			TodParticleGlobal.gParticleDefArray = new TodParticleDefinition[TodParticleGlobal.gParticleDefCount];
			for (int i = 0; i < TodParticleGlobal.gParticleDefArray.Length; i++)
			{
				TodParticleGlobal.gParticleDefArray[i] = new TodParticleDefinition();
			}
			for (int j = 0; j < TodParticleGlobal.gParticleParamArraySize; j++)
			{
				ParticleParams particleParams = theParticleParamArray[j];
				Debug.ASSERT(particleParams.mParticleEffect == (ParticleEffect)j);
				TodParticleDefinition todParticleDefinition = TodParticleGlobal.gParticleDefArray[j];
				if (!TodParticleGlobal.TodParticleLoadADef(ref todParticleDefinition, particleParams.mParticleFileName))
				{
					return;
				}
				TodParticleGlobal.gParticleDefArray[j] = todParticleDefinition;
				GlobalStaticVars.gSexyAppBase.mNumLoadingThreadTasks += 6;
			}
		}

		public static void TodParticleFreeDefinitions()
		{
			TodParticleGlobal.gParticleDefArray = null;
			TodParticleGlobal.gParticleDefCount = 0;
			TodParticleGlobal.gParticleParamArray = null;
			TodParticleGlobal.gParticleParamArraySize = 0;
		}

		public static bool TodParticleLoadADef(ref TodParticleDefinition theParticleDef, string aFilename)
		{
			if (!GlobalStaticVars.gSexyAppBase.mResourceManager.LoadParticle(aFilename, ref theParticleDef))
			{
				return false;
			}
			for (int i = 0; i < theParticleDef.mEmitterDefCount; i++)
			{
				TodEmitterDefinition todEmitterDefinition = theParticleDef.mEmitterDefs[i];
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSystemDuration, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSpawnRate, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSpawnMinActive, -1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSpawnMaxActive, -1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSpawnMaxLaunched, -1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mEmitterRadius, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mEmitterOffsetX, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mEmitterOffsetY, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mEmitterBoxX, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mEmitterBoxY, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mEmitterSkewX, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mEmitterSkewY, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleDuration, 100f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mLaunchSpeed, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSystemRed, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSystemGreen, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSystemBlue, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSystemAlpha, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mSystemBrightness, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mLaunchAngle, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mCrossFadeDuration, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleRed, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleGreen, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleBlue, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleAlpha, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleBrightness, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleSpinAngle, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleSpinSpeed, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleScale, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mParticleStretch, 1f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mCollisionReflect, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mCollisionSpin, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mClipTop, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mClipBottom, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mClipLeft, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mClipRight, 0f);
				Definition.FloatTrackSetDefault(ref todEmitterDefinition.mAnimationRate, 0f);
			}
			return true;
		}

		public static float FloatTrackEvaluateFromLastTime(ref FloatParameterTrack theTrack, float theTimeValue, float theInterp)
		{
			if (theTimeValue < 0f)
			{
				return 0f;
			}
			return Definition.FloatTrackEvaluate(ref theTrack, theTimeValue, theInterp);
		}

		internal static float CrossFadeLerp(float theFrom, float theTo, bool theFromIsSet, bool theToIsSet, float theFraction)
		{
			if (!theFromIsSet)
			{
				return theTo;
			}
			if (!theToIsSet)
			{
				return theFrom;
			}
			return theFrom + (theTo - theFrom) * theFraction;
		}

		internal static void RenderParticle(Graphics g, TodParticle theParticle, SexyColor theColor, ref ParticleRenderParams theParams)
		{
			TodParticleEmitter particleEmitter = theParticle.mParticleEmitter;
			TodEmitterDefinition emitterDef = particleEmitter.mEmitterDef;
			Image image;
			if (particleEmitter.mImageOverride != null)
			{
				image = particleEmitter.mImageOverride;
			}
			else
			{
				if (emitterDef.mImage == null)
				{
					return;
				}
				image = emitterDef.mImage;
			}
			int celWidth = image.GetCelWidth();
			int celHeight = image.GetCelHeight();
			int num;
			if (particleEmitter.mFrameOverride != -1)
			{
				num = particleEmitter.mFrameOverride;
			}
			else if (Definition.FloatTrackIsSet(ref emitterDef.mAnimationRate))
			{
				num = (int)(theParticle.mAnimationTimeValue * (float)emitterDef.mImageFrames);
				num = TodCommon.ClampInt(num, 0, emitterDef.mImageFrames - 1);
			}
			else if (emitterDef.mAnimated != 0)
			{
				num = (int)(theParticle.mParticleTimeValue * (float)emitterDef.mImageFrames);
				num = TodCommon.ClampInt(num, 0, emitterDef.mImageFrames - 1);
			}
			else
			{
				num = theParticle.mImageFrame;
			}
			num += emitterDef.mImageCol;
			if (num >= image.mNumCols)
			{
				num = image.mNumCols - 1;
			}
			int num2 = emitterDef.mImageRow;
			if (num2 >= image.mNumRows)
			{
				num2 = image.mNumRows - 1;
			}
			TRect trect = new TRect(celWidth * num, celHeight * num2, celWidth, celHeight);
			float num3 = Definition.FloatTrackEvaluate(ref emitterDef.mClipTop, theParticle.mParticleTimeValue, theParticle.mParticleInterp[11]);
			float num4 = Definition.FloatTrackEvaluate(ref emitterDef.mClipBottom, theParticle.mParticleTimeValue, theParticle.mParticleInterp[12]);
			float num5 = Definition.FloatTrackEvaluate(ref emitterDef.mClipLeft, theParticle.mParticleTimeValue, theParticle.mParticleInterp[13]);
			float num6 = Definition.FloatTrackEvaluate(ref emitterDef.mClipRight, theParticle.mParticleTimeValue, theParticle.mParticleInterp[14]);
			theParams.mPosX += num5 * (float)celWidth;
			theParams.mPosY += num3 * (float)celHeight;
			trect.mX += TodCommon.FloatRoundToInt(num5 * (float)celWidth);
			trect.mY += TodCommon.FloatRoundToInt(num3 * (float)celHeight);
			trect.mWidth -= TodCommon.FloatRoundToInt((num5 + num6) * (float)celWidth);
			trect.mHeight -= TodCommon.FloatRoundToInt((num3 + num4) * (float)celHeight);
			if (TodCommon.TestBit((uint)emitterDef.mParticleFlags, 2))
			{
				theParams.mPosX = (float)TodCommon.FloatRoundToInt(theParams.mPosX);
				theParams.mPosY = (float)TodCommon.FloatRoundToInt(theParams.mPosY);
			}
			Graphics.DrawMode drawMode = g.mDrawMode;
			if (TodCommon.TestBit((uint)emitterDef.mParticleFlags, 8))
			{
				drawMode = Graphics.DrawMode.DRAWMODE_ADDITIVE;
			}
			if (TodCommon.TestBit((uint)emitterDef.mParticleFlags, 9))
			{
				SexyColor aColor = g.GetColor();
				Graphics.DrawMode drawMode2 = g.mDrawMode;
				g.SetColor(theColor);
				g.SetDrawMode(drawMode);
				g.FillRect(-g.mTransX, -g.mTransY, 480, 320);
				g.SetColor(aColor);
				g.SetDrawMode(drawMode2);
				return;
			}
			trect.mX += image.mS;
			trect.mY += image.mT;
			int trectWidth = trect.mWidth;
			int trectHeight = trect.mHeight;
			g.SetDrawMode(drawMode);
			g.DrawImageRotatedScaled(image, new TRect((int)theParams.mPosX, (int)theParams.mPosY, trect.mWidth, trect.mHeight), new TRect(trect.mX, trect.mY, trect.mWidth, trect.mHeight), particleEmitter.mExtraAdditiveDrawOverride ? SexyColor.White : theColor, theParams.mSpinPosition, new Vector2(theParams.mParticleScale, theParams.mParticleScale * theParams.mParticleStretch), new Vector2((float)(trect.mWidth / 2), (float)(trect.mHeight / 2)));
			if (particleEmitter.mExtraAdditiveDrawOverride)
			{
				g.SetDrawMode(Graphics.DrawMode.DRAWMODE_ADDITIVE);
				g.DrawImageRotatedScaled(image, new TRect((int)theParams.mPosX, (int)theParams.mPosY, trect.mWidth, trect.mHeight), new TRect(trect.mX, trect.mY, trect.mWidth, trect.mHeight), theColor, theParams.mSpinPosition, new Vector2(theParams.mParticleScale, theParams.mParticleScale * theParams.mParticleStretch), new Vector2((float)(trect.mWidth / 2), (float)(trect.mHeight / 2)));
			}
		}

		public static int gParticleDefCount;

		public static TodParticleDefinition[] gParticleDefArray;

		public static int gParticleParamArraySize;

		public static ParticleParams[] gParticleParamArray;
	}
}
