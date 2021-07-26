using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class TodParticleEmitter
	{
		public static void PreallocateMemory()
		{
			for (int i = 0; i < 100; i++)
			{
				new TodParticleEmitter().PrepareForReuse();
			}
		}

		public static TodParticleEmitter GetNewTodParticleEmitter()
		{
			TodParticleEmitter result;
			if (TodParticleEmitter.unusedObjects.Count > 0)
			{
				result = TodParticleEmitter.unusedObjects.Pop();
			}
			else
			{
				result = new TodParticleEmitter();
			}
			return result;
		}

		public void PrepareForReuse()
		{
			this.Reset();
			TodParticleEmitter.unusedObjects.Push(this);
		}

		private TodParticleEmitter()
		{
			this.Reset();
		}

		private void Reset()
		{
			this.mEmitterDef = null;
			this.mParticleSystem = null;
			for (int i = 0; i < this.mParticleList.Count; i++)
			{
				this.mParticleList[i].PrepareForReuse();
			}
			this.mParticleList.Clear();
			this.mSpawnAccum = 0f;
			this.mSystemCenter = default(SexyVector2);
			this.mParticlesSpawned = 0;
			this.mSystemAge = 0;
			this.mSystemDuration = 0;
			this.mSystemTimeValue = 0f;
			this.mSystemLastTimeValue = 0f;
			this.mDead = false;
			this.mColorOverride = default(SexyColor);
			this.mExtraAdditiveDrawOverride = false;
			this.mScaleOverride = 0f;
			this.mImageOverride = null;
			this.mCrossFadeEmitterID = null;
			this.mEmitterCrossFadeCountDown = 0;
			this.mFrameOverride = 0;
			this.mActive = false;
			for (int j = 0; j < this.mTrackInterp.Length; j++)
			{
				this.mTrackInterp[j] = 0f;
			}
			for (int k = 0; k < 5; k++)
			{
				for (int l = 0; l < 2; l++)
				{
					this.mSystemFieldInterp[k, l] = 0f;
				}
			}
		}

		public void TodEmitterInitialize(float theX, float theY, TodParticleSystem theSystem, TodEmitterDefinition theEmitterDef)
		{
			this.mSpawnAccum = 0f;
			this.mParticlesSpawned = 0;
			this.mSystemAge = -1;
			this.mDead = false;
			this.mSystemTimeValue = -1f;
			this.mSystemLastTimeValue = -1f;
			this.mColorOverride = SexyColor.White;
			this.mExtraAdditiveDrawOverride = false;
			this.mImageOverride = null;
			this.mFrameOverride = -1;
			this.mSystemDuration = 0;
			this.mParticleSystem = theSystem;
			this.mEmitterDef = theEmitterDef;
			this.mParticleSystem = theSystem;
			this.mSystemCenter.x = theX;
			this.mSystemCenter.y = theY;
			this.mScaleOverride = 1f;
			if (Definition.FloatTrackIsSet(ref this.mEmitterDef.mSystemDuration))
			{
				float theInterp = RandomNumbers.NextNumber(1f);
				this.mSystemDuration = (int)Definition.FloatTrackEvaluate(ref this.mEmitterDef.mSystemDuration, 0f, theInterp);
			}
			else
			{
				this.mSystemDuration = (int)Definition.FloatTrackEvaluate(ref this.mEmitterDef.mParticleDuration, 0f, 1f);
			}
			this.mSystemDuration = Math.Max(1, this.mSystemDuration);
			for (int i = 0; i < this.mEmitterDef.mSystemFieldCount; i++)
			{
				this.mSystemFieldInterp[i, 0] = RandomNumbers.NextNumber(1f);
				this.mSystemFieldInterp[i, 1] = RandomNumbers.NextNumber(1f);
			}
			for (int j = 0; j < 10; j++)
			{
				this.mTrackInterp[j] = RandomNumbers.NextNumber(1f);
			}
			this.Update();
		}

		public void Update()
		{
			if (this.mDead)
			{
				return;
			}
			this.mSystemAge++;
			bool flag = false;
			if (this.mSystemAge >= this.mSystemDuration)
			{
				if (TodCommon.TestBit((uint)this.mEmitterDef.mParticleFlags, 3))
				{
					this.mSystemAge = 0;
				}
				else
				{
					this.mSystemAge = this.mSystemDuration - 1;
					flag = true;
				}
			}
			if (this.mEmitterCrossFadeCountDown > 0)
			{
				this.mEmitterCrossFadeCountDown--;
				if (this.mEmitterCrossFadeCountDown == 0)
				{
					flag = true;
				}
			}
			if (this.mCrossFadeEmitterID != null)
			{
				TodParticleEmitter todParticleEmitter = this.mCrossFadeEmitterID;
				if (todParticleEmitter == null || todParticleEmitter.mDead)
				{
					flag = true;
				}
			}
			this.mSystemTimeValue = (float)this.mSystemAge / (float)(this.mSystemDuration - 1);
			for (int i = 0; i < this.mEmitterDef.mSystemFieldCount; i++)
			{
				ParticleField theParticleField = this.mEmitterDef.mSystemFields[i];
				this.UpdateSystemField(theParticleField, this.mSystemTimeValue, i);
			}
			for (int j = this.mParticleList.Count - 1; j >= 0; j--)
			{
				TodParticle todParticle = this.mParticleList[j];
				TodParticle theParticle = todParticle;
				if (!this.UpdateParticle(theParticle))
				{
					this.DeleteParticle(theParticle);
				}
			}
			this.UpdateSpawning();
			if (flag)
			{
				this.DeleteNonCrossFading();
				if (this.mParticleList.Count == 0)
				{
					this.mDead = true;
					return;
				}
			}
			this.mSystemLastTimeValue = this.mSystemTimeValue;
		}

		public void Draw(Graphics g, bool doScale)
		{
			bool flag = true;
			if (TodCommon.TestBit((uint)this.mEmitterDef.mParticleFlags, 10) && flag)
			{
				return;
			}
			if (TodCommon.TestBit((uint)this.mEmitterDef.mParticleFlags, 11) && !flag)
			{
				return;
			}
			for (int i = 0; i < this.mParticleList.Count; i++)
			{
				TodParticle theParticle = this.mParticleList[i];
				this.DrawParticle(g, theParticle, doScale);
			}
		}

		public void SystemMove(float theX, float theY)
		{
			float num = theX - this.mSystemCenter.x;
			float num2 = theY - this.mSystemCenter.y;
			if (TodCommon.FloatApproxEqual(num, 0f) && TodCommon.FloatApproxEqual(num2, 0f))
			{
				return;
			}
			this.mSystemCenter.x = theX;
			this.mSystemCenter.y = theY;
			if (!TodCommon.TestBit((uint)this.mEmitterDef.mParticleFlags, 5))
			{
				foreach (TodParticle todParticle in this.mParticleList)
				{
					TodParticle todParticle2 = todParticle;
					TodParticle todParticle3 = todParticle2;
					todParticle3.mPosition.x = todParticle3.mPosition.x + num;
					TodParticle todParticle4 = todParticle2;
					todParticle4.mPosition.y = todParticle4.mPosition.y + num2;
				}
			}
		}

		public static bool GetRenderParams(TodParticle theParticle, ref ParticleRenderParams theParams)
		{
			TodParticleEmitter mParticleEmitter = theParticle.mParticleEmitter;
			TodEmitterDefinition todEmitterDefinition = mParticleEmitter.mEmitterDef;
			theParams.mRedIsSet = false;
			theParams.mRedIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mSystemRed);
			theParams.mRedIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleRed);
			theParams.mRedIsSet |= ((float)mParticleEmitter.mColorOverride.mRed != 1f);
			theParams.mGreenIsSet = false;
			theParams.mGreenIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mSystemGreen);
			theParams.mGreenIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleGreen);
			theParams.mGreenIsSet |= ((float)mParticleEmitter.mColorOverride.mGreen != 1f);
			theParams.mBlueIsSet = false;
			theParams.mBlueIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mSystemBlue);
			theParams.mBlueIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleBlue);
			theParams.mBlueIsSet |= ((float)mParticleEmitter.mColorOverride.mBlue != 1f);
			theParams.mAlphaIsSet = false;
			theParams.mAlphaIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mSystemAlpha);
			theParams.mAlphaIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleAlpha);
			theParams.mAlphaIsSet |= ((float)mParticleEmitter.mColorOverride.mAlpha != 1f);
			theParams.mParticleScaleIsSet = false;
			theParams.mParticleScaleIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleScale);
			theParams.mParticleScaleIsSet |= (mParticleEmitter.mScaleOverride != 1f);
			theParams.mParticleStretchIsSet = Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleStretch);
			theParams.mSpinPositionIsSet = false;
			theParams.mSpinPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleSpinSpeed);
			theParams.mSpinPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleSpinAngle);
			theParams.mSpinPositionIsSet |= TodCommon.TestBit((uint)todEmitterDefinition.mParticleFlags, 0);
			theParams.mSpinPositionIsSet |= TodCommon.TestBit((uint)todEmitterDefinition.mParticleFlags, 1);
			theParams.mPositionIsSet = false;
			theParams.mPositionIsSet |= ((float)todEmitterDefinition.mParticleFieldCount > 0f);
			theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterRadius);
			theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterOffsetX);
			theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterOffsetY);
			theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterBoxX);
			theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterBoxY);
			float num = mParticleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemRed, ParticleSystemTracks.TRACK_SYSTEM_RED);
			float num2 = mParticleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemGreen, ParticleSystemTracks.TRACK_SYSTEM_GREEN);
			float num3 = mParticleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemBlue, ParticleSystemTracks.TRACK_SYSTEM_BLUE);
			float num4 = mParticleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemAlpha, ParticleSystemTracks.TRACK_SYSTEM_ALPHA);
			float num5 = mParticleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemBrightness, ParticleSystemTracks.TRACK_SYSTEM_BRIGHTNESS);
			float num6 = mParticleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleRed, theParticle, ParticleTracks.TRACK_PARTICLE_RED);
			float num7 = mParticleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleGreen, theParticle, ParticleTracks.TRACK_PARTICLE_GREEN);
			float num8 = mParticleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleBlue, theParticle, ParticleTracks.TRACK_PARTICLE_BLUE);
			float num9 = mParticleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleAlpha, theParticle, ParticleTracks.TRACK_PARTICLE_ALPHA);
			float num10 = mParticleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleBrightness, theParticle, ParticleTracks.TRACK_PARTICLE_BRIGHTNESS);
			float num11 = num10 * num5;
			theParams.mRed = num6 * num * (float)mParticleEmitter.mColorOverride.mRed * num11;
			theParams.mGreen = num7 * num2 * (float)mParticleEmitter.mColorOverride.mGreen * num11;
			theParams.mBlue = num8 * num3 * (float)mParticleEmitter.mColorOverride.mBlue * num11;
			theParams.mAlpha = num9 * num4 * (float)mParticleEmitter.mColorOverride.mAlpha;
			theParams.mPosX = theParticle.mPosition.x;
			theParams.mPosY = theParticle.mPosition.y;
			theParams.mParticleScale = mParticleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleScale, theParticle, ParticleTracks.TRACK_PARTICLE_SCALE);
			theParams.mParticleStretch = mParticleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleStretch, theParticle, ParticleTracks.TRACK_PARTICLE_STRETCH);
			theParams.mParticleScale *= mParticleEmitter.mScaleOverride;
			theParams.mSpinPosition = theParticle.mSpinPosition;
			TodParticle todParticle = null;
			if (mParticleEmitter.mParticleSystem.mParticleHolder.mParticles.Contains(theParticle.mCrossFadeParticleID))
			{
				todParticle = mParticleEmitter.mParticleSystem.mParticleHolder.mParticles[mParticleEmitter.mParticleSystem.mParticleHolder.mParticles.IndexOf(theParticle.mCrossFadeParticleID)];
			}
			if (todParticle != null)
			{
				ParticleRenderParams particleRenderParams = default(ParticleRenderParams);
				if (TodParticleEmitter.GetRenderParams(todParticle, ref particleRenderParams))
				{
					float theFraction = (float)theParticle.mParticleAge / (float)(todParticle.mCrossFadeDuration - 1);
					theParams.mRed = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mRed, theParams.mRed, particleRenderParams.mRedIsSet, theParams.mRedIsSet, theFraction);
					theParams.mGreen = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mGreen, theParams.mGreen, particleRenderParams.mGreenIsSet, theParams.mGreenIsSet, theFraction);
					theParams.mBlue = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mBlue, theParams.mBlue, particleRenderParams.mBlueIsSet, theParams.mBlueIsSet, theFraction);
					theParams.mAlpha = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mAlpha, theParams.mAlpha, particleRenderParams.mAlphaIsSet, theParams.mAlphaIsSet, theFraction);
					theParams.mParticleScale = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mParticleScale, theParams.mParticleScale, particleRenderParams.mParticleScaleIsSet, theParams.mParticleScaleIsSet, theFraction);
					theParams.mParticleStretch = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mParticleStretch, theParams.mParticleStretch, particleRenderParams.mParticleStretchIsSet, theParams.mParticleStretchIsSet, theFraction);
					theParams.mSpinPosition = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mSpinPosition, theParams.mSpinPosition, particleRenderParams.mSpinPositionIsSet, theParams.mSpinPositionIsSet, theFraction);
					theParams.mPosX = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mPosX, theParams.mPosX, particleRenderParams.mPositionIsSet, theParams.mPositionIsSet, theFraction);
					theParams.mPosY = TodParticleGlobal.CrossFadeLerp(particleRenderParams.mPosY, theParams.mPosY, particleRenderParams.mPositionIsSet, theParams.mPositionIsSet, theFraction);
					theParams.mRedIsSet |= particleRenderParams.mRedIsSet;
					theParams.mGreenIsSet |= particleRenderParams.mGreenIsSet;
					theParams.mBlueIsSet |= particleRenderParams.mBlueIsSet;
					theParams.mAlphaIsSet |= particleRenderParams.mAlphaIsSet;
					theParams.mParticleScaleIsSet |= particleRenderParams.mParticleScaleIsSet;
					theParams.mParticleStretchIsSet |= particleRenderParams.mParticleStretchIsSet;
					theParams.mSpinPositionIsSet |= particleRenderParams.mSpinPositionIsSet;
					theParams.mPositionIsSet |= particleRenderParams.mPositionIsSet;
				}
			}
			return true;
		}

		public void DrawParticle(Graphics g, TodParticle theParticle, bool doScale)
		{
			if (theParticle.mCrossFadeDuration > 0)
			{
				return;
			}
			ParticleRenderParams particleRenderParams = default(ParticleRenderParams);
			if (!TodParticleEmitter.GetRenderParams(theParticle, ref particleRenderParams))
			{
				return;
			}
			if (doScale)
			{
				particleRenderParams.mPosX *= Constants.S;
				particleRenderParams.mPosY *= Constants.S;
			}
			SexyColor theColor = new SexyColor(TodCommon.ClampInt(TodCommon.FloatRoundToInt(particleRenderParams.mRed), 0, 255), TodCommon.ClampInt(TodCommon.FloatRoundToInt(particleRenderParams.mGreen), 0, 255), TodCommon.ClampInt(TodCommon.FloatRoundToInt(particleRenderParams.mBlue), 0, 255), TodCommon.ClampInt(TodCommon.FloatRoundToInt(particleRenderParams.mAlpha), 0, 255));
			if (theColor.mAlpha == 0)
			{
				return;
			}
			particleRenderParams.mPosX += (float)g.mTransX;
			particleRenderParams.mPosY += (float)g.mTransY;
			TodParticle theParticle2;
			if (this.mImageOverride != null || this.mEmitterDef.mImage != null)
			{
				theParticle2 = theParticle;
			}
			else
			{
				TodParticle mCrossFadeParticleID = theParticle.mCrossFadeParticleID;
				if (mCrossFadeParticleID == null)
				{
					return;
				}
				theParticle2 = mCrossFadeParticleID;
			}
			TodParticleGlobal.RenderParticle(g, theParticle2, theColor, ref particleRenderParams);
		}

		public void UpdateSpawning()
		{
			TodParticleEmitter todParticleEmitter = this;
			TodParticleEmitter todParticleEmitter2 = null;
			if (this.mCrossFadeEmitterID != null && this.mCrossFadeEmitterID.mActive)
			{
				todParticleEmitter2 = this.mCrossFadeEmitterID;
			}
			if (todParticleEmitter2 != null)
			{
				todParticleEmitter = todParticleEmitter2;
			}
			this.mSpawnAccum += Definition.FloatTrackEvaluate(ref todParticleEmitter.mEmitterDef.mSpawnRate, todParticleEmitter.mSystemTimeValue, todParticleEmitter.mTrackInterp[0]) * 0.01f;
			int num = (int)this.mSpawnAccum;
			this.mSpawnAccum -= (float)num;
			int num2 = (int)Definition.FloatTrackEvaluate(ref todParticleEmitter.mEmitterDef.mSpawnMinActive, todParticleEmitter.mSystemTimeValue, todParticleEmitter.mTrackInterp[1]);
			if (num2 >= 0)
			{
				int num3 = num2 - this.mParticleList.Count;
				if (num < num3)
				{
					num = num3;
				}
			}
			int num4 = (int)Definition.FloatTrackEvaluate(ref todParticleEmitter.mEmitterDef.mSpawnMaxActive, todParticleEmitter.mSystemTimeValue, todParticleEmitter.mTrackInterp[2]);
			if (num4 >= 0)
			{
				int num5 = num4 - this.mParticleList.Count;
				if (num > num5)
				{
					num = num5;
				}
			}
			if (Definition.FloatTrackIsSet(ref todParticleEmitter.mEmitterDef.mSpawnMaxLaunched))
			{
				int num6 = (int)Definition.FloatTrackEvaluate(ref todParticleEmitter.mEmitterDef.mSpawnMaxLaunched, todParticleEmitter.mSystemTimeValue, todParticleEmitter.mTrackInterp[3]);
				int num7 = num6 - this.mParticlesSpawned;
				if (num > num7)
				{
					num = num7;
				}
			}
			for (int i = 0; i < num; i++)
			{
				TodParticle theParticle = this.SpawnParticle(i, num);
				if (todParticleEmitter2 != null)
				{
					this.CrossFadeParticle(theParticle, todParticleEmitter2);
				}
			}
		}

		public bool UpdateParticle(TodParticle theParticle)
		{
			if (theParticle.mParticleAge >= theParticle.mParticleDuration)
			{
				if (TodCommon.TestBit((uint)this.mEmitterDef.mParticleFlags, 4))
				{
					theParticle.mParticleAge = 0;
				}
				else if (theParticle.mCrossFadeDuration > 0)
				{
					theParticle.mParticleAge = theParticle.mParticleDuration - 1;
				}
				else
				{
					if (this.mEmitterDef.mOnDuration.Length <= 0)
					{
						return false;
					}
					char c = this.mEmitterDef.mOnDuration.get_Chars(0);
					if (!this.CrossFadeParticleToName(theParticle, this.mEmitterDef.mOnDuration))
					{
						return false;
					}
				}
			}
			if (theParticle.mCrossFadeParticleID != null && this.mParticleSystem.mParticleHolder.mParticles[this.mParticleSystem.mParticleHolder.mParticles.IndexOf(theParticle.mCrossFadeParticleID)] == null)
			{
				return false;
			}
			theParticle.mParticleTimeValue = (float)theParticle.mParticleAge / (float)(theParticle.mParticleDuration - 1);
			for (int i = 0; i < this.mEmitterDef.mParticleFieldCount; i++)
			{
				ParticleField theParticleField = this.mEmitterDef.mParticleFields[i];
				this.UpdateParticleField(theParticle, theParticleField, theParticle.mParticleTimeValue, i);
			}
			theParticle.mPosition += theParticle.mVelocity;
			float num = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mParticleSpinSpeed, theParticle.mParticleTimeValue, theParticle.mParticleInterp[5]) * 0.01f;
			float num2 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mParticleSpinAngle, theParticle.mParticleTimeValue, theParticle.mParticleInterp[6]);
			float num3 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref this.mEmitterDef.mParticleSpinAngle, theParticle.mParticleLastTimeValue, theParticle.mParticleInterp[6]);
			theParticle.mSpinPosition += TodCommon.DegToRad(num + num2 - num3);
			theParticle.mSpinPosition += theParticle.mSpinVelocity;
			if (Definition.FloatTrackIsSet(ref this.mEmitterDef.mAnimationRate))
			{
				float num4 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mAnimationRate, theParticle.mParticleTimeValue, theParticle.mParticleInterp[15]) * 0.01f;
				theParticle.mAnimationTimeValue += num4;
				while (theParticle.mAnimationTimeValue >= 1f)
				{
					theParticle.mAnimationTimeValue -= 1f;
				}
				while (theParticle.mAnimationTimeValue < 0f)
				{
					theParticle.mAnimationTimeValue += 0f;
				}
			}
			theParticle.mParticleLastTimeValue = theParticle.mParticleTimeValue;
			theParticle.mParticleAge++;
			return true;
		}

		public TodParticle SpawnParticle(int theIndex, int theSpawnCount)
		{
			if (this.mParticleSystem.mParticleHolder.mParticles.Count == this.mParticleSystem.mParticleHolder.mParticles.Capacity)
			{
				Debug.OutputDebug<string>(this.mEmitterDef.mName);
				return null;
			}
			TodParticle newTodParticle = TodParticle.GetNewTodParticle();
			this.mParticleSystem.mParticleHolder.mParticles.Add(newTodParticle);
			for (int i = 0; i < this.mEmitterDef.mParticleFieldCount; i++)
			{
				newTodParticle.mParticleFieldInterp[i, 0] = RandomNumbers.NextNumber(1f);
				newTodParticle.mParticleFieldInterp[i, 1] = RandomNumbers.NextNumber(1f);
			}
			for (int j = 0; j < 16; j++)
			{
				newTodParticle.mParticleInterp[j] = RandomNumbers.NextNumber(1f);
			}
			float theInterp = RandomNumbers.NextNumber(1f);
			float theInterp2 = RandomNumbers.NextNumber(1f);
			float theInterp3 = RandomNumbers.NextNumber(1f);
			float theInterp4 = RandomNumbers.NextNumber(1f);
			newTodParticle.mParticleDuration = (int)Definition.FloatTrackEvaluate(ref this.mEmitterDef.mParticleDuration, this.mSystemTimeValue, theInterp);
			newTodParticle.mParticleDuration = Math.Max(1, newTodParticle.mParticleDuration);
			newTodParticle.mParticleAge = 0;
			newTodParticle.mParticleEmitter = this;
			newTodParticle.mParticleTimeValue = -1f;
			newTodParticle.mParticleLastTimeValue = -1f;
			if (TodCommon.TestBit((uint)this.mEmitterDef.mParticleFlags, 6))
			{
				newTodParticle.mParticleAge = RandomNumbers.NextNumber(newTodParticle.mParticleDuration);
			}
			else
			{
				newTodParticle.mParticleAge = 0;
			}
			float num = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mLaunchSpeed, this.mSystemTimeValue, theInterp2) * 0.01f;
			float theInterp5 = RandomNumbers.NextNumber(1f);
			float num2;
			if (this.mEmitterDef.mEmitterType == EmitterType.EMITTER_CIRCLE_PATH)
			{
				num2 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterPath, this.mSystemTimeValue, this.mTrackInterp[4]) * 3.1415927f * 2f;
				num2 += TodCommon.DegToRad(Definition.FloatTrackEvaluate(ref this.mEmitterDef.mLaunchAngle, this.mSystemTimeValue, theInterp5));
			}
			else if (this.mEmitterDef.mEmitterType == EmitterType.EMITTER_CIRCLE_EVEN_SPACING)
			{
				num2 = 6.2831855f * (float)theIndex / (float)theSpawnCount;
				num2 += TodCommon.DegToRad(Definition.FloatTrackEvaluate(ref this.mEmitterDef.mLaunchAngle, this.mSystemTimeValue, theInterp5));
			}
			else if (Definition.FloatTrackIsConstantZero(ref this.mEmitterDef.mLaunchAngle))
			{
				num2 = RandomNumbers.NextNumber(6.2831855f);
			}
			else
			{
				num2 = TodCommon.DegToRad(Definition.FloatTrackEvaluate(ref this.mEmitterDef.mLaunchAngle, this.mSystemTimeValue, theInterp5));
			}
			float num3 = 0f;
			float num4 = 0f;
			if (this.mEmitterDef.mEmitterType == EmitterType.EMITTER_CIRCLE || this.mEmitterDef.mEmitterType == EmitterType.EMITTER_CIRCLE_PATH || this.mEmitterDef.mEmitterType == EmitterType.EMITTER_CIRCLE_EVEN_SPACING)
			{
				float theInterp6 = RandomNumbers.NextNumber(1f);
				float num5 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterRadius, this.mSystemTimeValue, theInterp6);
				num3 = (float)Math.Sin((double)num2) * num5;
				num4 = (float)Math.Cos((double)num2) * num5;
			}
			else if (this.mEmitterDef.mEmitterType == EmitterType.EMITTER_BOX)
			{
				float theInterp7 = RandomNumbers.NextNumber(1f);
				float theInterp8 = RandomNumbers.NextNumber(1f);
				num3 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterBoxX, this.mSystemTimeValue, theInterp7);
				num4 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterBoxY, this.mSystemTimeValue, theInterp8);
			}
			else if (this.mEmitterDef.mEmitterType == EmitterType.EMITTER_BOX_PATH)
			{
				float num6 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterPath, this.mSystemTimeValue, this.mTrackInterp[4]);
				float num7 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterBoxX, this.mSystemTimeValue, 0f);
				float num8 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterBoxX, this.mSystemTimeValue, 1f);
				float num9 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterBoxY, this.mSystemTimeValue, 0f);
				float num10 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterBoxY, this.mSystemTimeValue, 1f);
				float num11 = num8 - num7;
				float num12 = num10 - num9;
				float num13 = num12 + num11 + num12 + num11;
				float num14 = num13 * num6;
				if (num14 < num12)
				{
					num3 = num7;
					num4 = num9 + (num10 - num9) * (num14 / num12);
				}
				else if (num14 < num12 + num11)
				{
					num3 = num7 + (num8 - num7) * ((num14 - num12) / num11);
					num4 = num10;
				}
				else if (num14 < num12 + num11 + num12)
				{
					num3 = num8;
					num4 = num10 + (num9 - num10) * ((num14 - num12 - num11) / num12);
				}
				else
				{
					num3 = num8 + (num7 - num8) * ((num14 - num12 - num11 - num12) / num11);
					num4 = num9;
				}
			}
			float theInterp9 = RandomNumbers.NextNumber(1f);
			float theInterp10 = RandomNumbers.NextNumber(1f);
			float num15 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterSkewX, this.mSystemTimeValue, theInterp9);
			float num16 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterSkewY, this.mSystemTimeValue, theInterp10);
			newTodParticle.mPosition.x = this.mSystemCenter.x + num3 + num4 * num15;
			newTodParticle.mPosition.y = this.mSystemCenter.y + num4 + num3 * num16;
			newTodParticle.mVelocity.x = (float)Math.Sin((double)num2) * num;
			newTodParticle.mVelocity.y = (float)Math.Cos((double)num2) * num;
			float num17 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterOffsetX, this.mSystemTimeValue, theInterp3);
			float num18 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mEmitterOffsetY, this.mSystemTimeValue, theInterp4);
			TodParticle todParticle = newTodParticle;
			todParticle.mPosition.x = todParticle.mPosition.x + num17;
			TodParticle todParticle2 = newTodParticle;
			todParticle2.mPosition.y = todParticle2.mPosition.y + num18;
			newTodParticle.mAnimationTimeValue = 0f;
			if (this.mEmitterDef.mAnimated != 0 || Definition.FloatTrackIsSet(ref this.mEmitterDef.mAnimationRate))
			{
				newTodParticle.mImageFrame = 0;
			}
			else
			{
				newTodParticle.mImageFrame = RandomNumbers.NextNumber(this.mEmitterDef.mImageFrames);
			}
			if (TodCommon.TestBit((uint)this.mEmitterDef.mParticleFlags, 0))
			{
				newTodParticle.mSpinPosition = RandomNumbers.NextNumber(6.2831855f);
			}
			else if (TodCommon.TestBit((uint)this.mEmitterDef.mParticleFlags, 1))
			{
				newTodParticle.mSpinPosition = num2;
			}
			else
			{
				newTodParticle.mSpinPosition = 0f;
			}
			newTodParticle.mSpinVelocity = 0f;
			newTodParticle.mCrossFadeDuration = 0;
			newTodParticle.mCrossFadeParticleID = null;
			TodParticle todParticle3 = newTodParticle;
			this.mParticleList.Insert(0, todParticle3);
			this.mParticlesSpawned++;
			this.UpdateParticle(newTodParticle);
			return newTodParticle;
		}

		public bool CrossFadeParticle(TodParticle theParticle, TodParticleEmitter theToEmitter)
		{
			if (theParticle.mCrossFadeDuration > 0)
			{
				Debug.OutputDebug<string>("We don't support cross fading more than one at a time\n");
				return false;
			}
			if (!Definition.FloatTrackIsSet(ref theToEmitter.mEmitterDef.mCrossFadeDuration))
			{
				Debug.OutputDebug<string>("Can't cross fade to emitter that doesn't have CrossFadeDuration");
				return false;
			}
			TodParticle todParticle = theToEmitter.SpawnParticle(0, 1);
			if (todParticle == null)
			{
				return false;
			}
			if (this.mEmitterCrossFadeCountDown > 0)
			{
				theParticle.mCrossFadeDuration = this.mEmitterCrossFadeCountDown;
			}
			else
			{
				float theInterp = RandomNumbers.NextNumber(1f);
				theParticle.mCrossFadeDuration = (int)Definition.FloatTrackEvaluate(ref theToEmitter.mEmitterDef.mCrossFadeDuration, this.mSystemTimeValue, theInterp);
				theParticle.mCrossFadeDuration = Math.Max(1, theParticle.mCrossFadeDuration);
			}
			if (!Definition.FloatTrackIsSet(ref theToEmitter.mEmitterDef.mParticleDuration))
			{
				todParticle.mParticleDuration = theParticle.mCrossFadeDuration;
			}
			todParticle.mCrossFadeParticleID = theParticle;
			return true;
		}

		public void CrossFadeEmitter(TodParticleEmitter theToEmitter)
		{
			if (this.mEmitterCrossFadeCountDown > 0)
			{
				Debug.OutputDebug<string>("We don't support cross fading emitters more than one at a time\n");
				return;
			}
			if (!Definition.FloatTrackIsSet(ref theToEmitter.mEmitterDef.mCrossFadeDuration))
			{
				Debug.OutputDebug<string>("Can't cross fade to emitter that doesn't have CrossFadeDuration");
				return;
			}
			float theInterp = RandomNumbers.NextNumber(1f);
			this.mEmitterCrossFadeCountDown = (int)Definition.FloatTrackEvaluate(ref theToEmitter.mEmitterDef.mCrossFadeDuration, this.mSystemTimeValue, theInterp);
			this.mEmitterCrossFadeCountDown = Math.Max(1, this.mEmitterCrossFadeCountDown);
			this.mCrossFadeEmitterID = theToEmitter;
			if (!Definition.FloatTrackIsSet(ref theToEmitter.mEmitterDef.mSystemDuration))
			{
				theToEmitter.mSystemDuration = this.mEmitterCrossFadeCountDown;
			}
			foreach (TodParticle todParticle in this.mParticleList)
			{
				TodParticle theParticle = todParticle;
				this.CrossFadeParticle(theParticle, theToEmitter);
			}
		}

		public bool CrossFadeParticleToName(TodParticle theParticle, string theEmitterName)
		{
			TodEmitterDefinition todEmitterDefinition = this.mParticleSystem.FindEmitterDefByName(theEmitterName);
			if (todEmitterDefinition == null)
			{
				Debug.OutputDebug<string>(Common.StrFormat_("Can't find emitter to cross fade: {0}\n", theEmitterName));
				return false;
			}
			if (this.mParticleSystem.mParticleHolder.mEmitters.Count == this.mParticleSystem.mParticleHolder.mEmitters.Capacity)
			{
				Debug.OutputDebug<string>("Too many emitters to cross fade\n");
				return false;
			}
			TodParticleEmitter todParticleEmitter = new TodParticleEmitter();
			todParticleEmitter.TodEmitterInitialize(this.mSystemCenter.x, this.mSystemCenter.y, this.mParticleSystem, todEmitterDefinition);
			todParticleEmitter.mActive = true;
			this.mParticleSystem.mParticleHolder.mEmitters.Add(todParticleEmitter);
			TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
			this.mParticleSystem.mEmitterList.Add(todParticleEmitter2);
			return this.CrossFadeParticle(theParticle, todParticleEmitter);
		}

		public void DeleteAll()
		{
			for (int i = 0; i < this.mParticleList.Count; i++)
			{
				TodParticle todParticle = this.mParticleList[i];
				this.mParticleSystem.mParticleHolder.mParticles.Remove(todParticle);
				todParticle.PrepareForReuse();
			}
			this.mParticleList.Clear();
		}

		public void UpdateParticleField(TodParticle theParticle, ParticleField theParticleField, float theParticleTimeValue, int theFieldIndex)
		{
			float theInterp = theParticle.mParticleFieldInterp[theFieldIndex, 0];
			float theInterp2 = theParticle.mParticleFieldInterp[theFieldIndex, 1];
			float num = Definition.FloatTrackEvaluate(ref theParticleField.mX, theParticleTimeValue, theInterp);
			float num2 = Definition.FloatTrackEvaluate(ref theParticleField.mY, theParticleTimeValue, theInterp2);
			switch (theParticleField.mFieldType)
			{
			case ParticleFieldType.FIELD_INVALID:
			case ParticleFieldType.FIELD_SYSTEM_POSITION:
				break;
			case ParticleFieldType.FIELD_FRICTION:
				theParticle.mVelocity.x = theParticle.mVelocity.x * (1f - num);
				theParticle.mVelocity.y = theParticle.mVelocity.y * (1f - num2);
				return;
			case ParticleFieldType.FIELD_ACCELERATION:
				theParticle.mVelocity.x = theParticle.mVelocity.x + num * 0.01f;
				theParticle.mVelocity.y = theParticle.mVelocity.y + num2 * 0.01f;
				return;
			case ParticleFieldType.FIELD_ATTRACTOR:
			{
				float num3 = num - (theParticle.mPosition.x - this.mSystemCenter.x);
				float num4 = num2 - (theParticle.mPosition.y - this.mSystemCenter.y);
				theParticle.mVelocity.x = theParticle.mVelocity.x + num3 * 0.01f;
				theParticle.mVelocity.y = theParticle.mVelocity.y + num4 * 0.01f;
				return;
			}
			case ParticleFieldType.FIELD_MAX_VELOCITY:
				theParticle.mVelocity.x = TodCommon.ClampFloat(theParticle.mVelocity.x, -num, num);
				theParticle.mVelocity.y = TodCommon.ClampFloat(theParticle.mVelocity.y, -num2, num2);
				return;
			case ParticleFieldType.FIELD_VELOCITY:
				theParticle.mPosition.x = theParticle.mPosition.x + num * 0.01f;
				theParticle.mPosition.y = theParticle.mPosition.y + num2 * 0.01f;
				return;
			case ParticleFieldType.FIELD_POSITION:
			{
				float num5 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mX, theParticle.mParticleLastTimeValue, theInterp);
				float num6 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mY, theParticle.mParticleLastTimeValue, theInterp2);
				theParticle.mPosition.x = theParticle.mPosition.x + (num - num5);
				theParticle.mPosition.y = theParticle.mPosition.y + (num2 - num6);
				return;
			}
			case ParticleFieldType.FIELD_GROUND_CONSTRAINT:
				if (theParticle.mPosition.y >= this.mSystemCenter.y + num2)
				{
					theParticle.mPosition.y = this.mSystemCenter.y + num2;
					float num7 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mCollisionReflect, theParticleTimeValue, theParticle.mParticleInterp[9]);
					float num8 = Definition.FloatTrackEvaluate(ref this.mEmitterDef.mCollisionSpin, theParticleTimeValue, theParticle.mParticleInterp[10]) / 1000f;
					theParticle.mSpinVelocity = theParticle.mVelocity.y * num8;
					theParticle.mVelocity.x = theParticle.mVelocity.x * num7;
					theParticle.mVelocity.y = -theParticle.mVelocity.y * num7;
					return;
				}
				break;
			case ParticleFieldType.FIELD_SHAKE:
			{
				float num9 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mX, theParticle.mParticleLastTimeValue, theInterp);
				float num10 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mY, theParticle.mParticleLastTimeValue, theInterp2);
				int num11 = theParticle.mParticleAge - 1;
				if (num11 == -1)
				{
					num11 = theParticle.mParticleDuration - 1;
				}
				theParticle.mPosition.x = theParticle.mPosition.x - num9 * ((float)RandomNumbers.NextNumber() * 0f * 2f - 1f);
				theParticle.mPosition.y = theParticle.mPosition.y - num10 * ((float)RandomNumbers.NextNumber() * 0f * 2f - 1f);
				theParticle.mPosition.x = theParticle.mPosition.x + num * ((float)RandomNumbers.NextNumber() * 0f * 2f - 1f);
				theParticle.mPosition.y = theParticle.mPosition.y + num2 * ((float)RandomNumbers.NextNumber() * 0f * 2f - 1f);
				return;
			}
			case ParticleFieldType.FIELD_CIRCLE:
			{
				float num12 = num * 0.01f;
				float num13 = num2 * 0.01f;
				SexyVector2 sexyVector = theParticle.mPosition - this.mSystemCenter;
				float num14 = sexyVector.Magnitude();
				SexyVector2 rhs = sexyVector.Perp();
				rhs = rhs.Normalize();
				rhs.mVector *= num12 + num14 * num13;
				theParticle.mPosition += rhs;
				return;
			}
			case ParticleFieldType.FIELD_AWAY:
			{
				float num15 = num * 0.01f;
				float num16 = num2 * 0.01f;
				SexyVector2 sexyVector2 = theParticle.mPosition - this.mSystemCenter;
				float num17 = sexyVector2.Magnitude();
				SexyVector2 rhs2 = sexyVector2.Normalize();
				rhs2.mVector *= num15 + num17 * num16;
				theParticle.mPosition += rhs2;
				break;
			}
			default:
				return;
			}
		}

		public void UpdateSystemField(ParticleField theParticleField, float theParticleTimeValue, int theFieldIndex)
		{
			float theInterp = this.mSystemFieldInterp[theFieldIndex, 0];
			float theInterp2 = this.mSystemFieldInterp[theFieldIndex, 1];
			float num = Definition.FloatTrackEvaluate(ref theParticleField.mX, theParticleTimeValue, theInterp);
			float num2 = Definition.FloatTrackEvaluate(ref theParticleField.mY, theParticleTimeValue, theInterp2);
			ParticleFieldType mFieldType = theParticleField.mFieldType;
			if (mFieldType != ParticleFieldType.FIELD_INVALID)
			{
				if (mFieldType != ParticleFieldType.FIELD_SYSTEM_POSITION)
				{
					return;
				}
				float num3 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mX, this.mSystemLastTimeValue, theInterp);
				float num4 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mY, this.mSystemLastTimeValue, theInterp2);
				this.mSystemCenter.x = this.mSystemCenter.x + (num - num3);
				this.mSystemCenter.y = this.mSystemCenter.y + (num2 - num4);
			}
		}

		public float SystemTrackEvaluate(ref FloatParameterTrack theTrack, ParticleSystemTracks theInterp)
		{
			return Definition.FloatTrackEvaluate(ref theTrack, this.mSystemTimeValue, this.mTrackInterp[(int)theInterp]);
		}

		public float ParticleTrackEvaluate(ref FloatParameterTrack theTrack, TodParticle theParticle, ParticleTracks theInterp)
		{
			return Definition.FloatTrackEvaluate(ref theTrack, theParticle.mParticleTimeValue, theParticle.mParticleInterp[(int)theInterp]);
		}

		public void DeleteParticle(TodParticle theParticle)
		{
			TodParticle todParticle = null;
			if (this.mParticleSystem.mParticleHolder.mParticles.Contains(theParticle.mCrossFadeParticleID))
			{
				todParticle = this.mParticleSystem.mParticleHolder.mParticles[this.mParticleSystem.mParticleHolder.mParticles.IndexOf(theParticle.mCrossFadeParticleID)];
			}
			if (todParticle != null)
			{
				todParticle.mParticleEmitter.DeleteParticle(todParticle);
				theParticle.mCrossFadeParticleID = null;
			}
			theParticle.PrepareForReuse();
			this.mParticleList.Remove(theParticle);
			this.mParticleSystem.mParticleHolder.mParticles.Remove(theParticle);
		}

		public void DeleteNonCrossFading()
		{
			for (int i = this.mParticleList.Count - 1; i >= 0; i--)
			{
				TodParticle todParticle = this.mParticleList[i];
				TodParticle todParticle2 = todParticle;
				if (todParticle2.mCrossFadeDuration <= 0)
				{
					this.DeleteParticle(todParticle2);
				}
			}
		}

		public TodEmitterDefinition mEmitterDef;

		public TodParticleSystem mParticleSystem;

		public List<TodParticle> mParticleList = new List<TodParticle>();

		public float mSpawnAccum;

		public SexyVector2 mSystemCenter = default(SexyVector2);

		public int mParticlesSpawned;

		public int mSystemAge;

		public int mSystemDuration;

		public float mSystemTimeValue;

		public float mSystemLastTimeValue;

		public bool mDead;

		public SexyColor mColorOverride = default(SexyColor);

		public bool mExtraAdditiveDrawOverride;

		public float mScaleOverride;

		public Image mImageOverride;

		public TodParticleEmitter mCrossFadeEmitterID;

		public int mEmitterCrossFadeCountDown;

		public int mFrameOverride;

		public bool mActive;

		public float[] mTrackInterp = new float[10];

		public float[,] mSystemFieldInterp = new float[5, 2];

		private static Stack<TodParticleEmitter> unusedObjects = new Stack<TodParticleEmitter>(100);
	}
}
