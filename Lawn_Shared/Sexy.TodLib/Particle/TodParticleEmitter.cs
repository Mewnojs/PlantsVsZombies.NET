using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
    public/*internal*/ class TodParticleEmitter
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
            Reset();
            TodParticleEmitter.unusedObjects.Push(this);
        }

        private TodParticleEmitter()
        {
            Reset();
        }

        private void Reset()
        {
            mEmitterDef = null;
            mParticleSystem = null;
            for (int i = 0; i < mParticleList.Count; i++)
            {
                mParticleList[i].PrepareForReuse();
            }
            mParticleList.Clear();
            mSpawnAccum = 0f;
            mSystemCenter = default(SexyVector2);
            mParticlesSpawned = 0;
            mSystemAge = 0;
            mSystemDuration = 0;
            mSystemTimeValue = 0f;
            mSystemLastTimeValue = 0f;
            mDead = false;
            mColorOverride = default(SexyColor);
            mExtraAdditiveDrawOverride = false;
            mScaleOverride = 0f;
            mImageOverride = null;
            mCrossFadeEmitterID = null;
            mEmitterCrossFadeCountDown = 0;
            mFrameOverride = 0;
            mActive = false;
            for (int j = 0; j < mTrackInterp.Length; j++)
            {
                mTrackInterp[j] = 0f;
            }
            for (int k = 0; k < 5; k++)
            {
                for (int l = 0; l < 2; l++)
                {
                    mSystemFieldInterp[k, l] = 0f;
                }
            }
        }

        public void TodEmitterInitialize(float theX, float theY, TodParticleSystem theSystem, TodEmitterDefinition theEmitterDef)
        {
            mSpawnAccum = 0f;
            mParticlesSpawned = 0;
            mSystemAge = -1;
            mDead = false;
            mSystemTimeValue = -1f;
            mSystemLastTimeValue = -1f;
            mColorOverride = SexyColor.White;
            mExtraAdditiveDrawOverride = false;
            mImageOverride = null;
            mFrameOverride = -1;
            mSystemDuration = 0;
            mParticleSystem = theSystem;
            mEmitterDef = theEmitterDef;
            mParticleSystem = theSystem;
            mSystemCenter.x = theX;
            mSystemCenter.y = theY;
            mScaleOverride = 1f;
            if (Definition.FloatTrackIsSet(ref mEmitterDef.mSystemDuration))
            {
                float theInterp = RandomNumbers.NextNumber(1f);
                mSystemDuration = (int)Definition.FloatTrackEvaluate(ref mEmitterDef.mSystemDuration, 0f, theInterp);
            }
            else
            {
                mSystemDuration = (int)Definition.FloatTrackEvaluate(ref mEmitterDef.mParticleDuration, 0f, 1f);
            }
            mSystemDuration = Math.Max(1, mSystemDuration);
            for (int i = 0; i < mEmitterDef.mSystemFieldCount; i++)
            {
                mSystemFieldInterp[i, 0] = RandomNumbers.NextNumber(1f);
                mSystemFieldInterp[i, 1] = RandomNumbers.NextNumber(1f);
            }
            for (int j = 0; j < 10; j++)
            {
                mTrackInterp[j] = RandomNumbers.NextNumber(1f);
            }
            Update();
        }

        public void Update()//1update
        {
            if (mDead)
            {
                return;
            }
            mSystemAge++;
            bool flag = false;
            if (mSystemAge >= mSystemDuration)
            {
                if (TodCommon.TestBit((uint)mEmitterDef.mParticleFlags, 3))
                {
                    mSystemAge = 0;
                }
                else
                {
                    mSystemAge = mSystemDuration - 1;
                    flag = true;
                }
            }
            if (mEmitterCrossFadeCountDown > 0)
            {
                mEmitterCrossFadeCountDown--;
                if (mEmitterCrossFadeCountDown == 0)
                {
                    flag = true;
                }
            }
            if (mCrossFadeEmitterID != null)
            {
                TodParticleEmitter todParticleEmitter = mCrossFadeEmitterID;
                if (todParticleEmitter == null || todParticleEmitter.mDead)
                {
                    flag = true;
                }
            }
            mSystemTimeValue = mSystemAge / (float)(mSystemDuration - 1);
            for (int i = 0; i < mEmitterDef.mSystemFieldCount; i++)
            {
                ParticleField theParticleField = mEmitterDef.mSystemFields[i];
                UpdateSystemField(theParticleField, mSystemTimeValue, i);
            }
            for (int j = mParticleList.Count - 1; j >= 0; j--)
            {
                TodParticle todParticle = mParticleList[j];
                TodParticle theParticle = todParticle;
                if (!UpdateParticle(theParticle))
                {
                    DeleteParticle(theParticle);
                }
            }
            UpdateSpawning();
            if (flag)
            {
                DeleteNonCrossFading();
                if (mParticleList.Count == 0)
                {
                    mDead = true;
                    return;
                }
            }
            mSystemLastTimeValue = mSystemTimeValue;
        }

        public void Draw(Graphics g, bool doScale)
        {
            bool flag = true;
            if (TodCommon.TestBit((uint)mEmitterDef.mParticleFlags, 10) && flag)
            {
                return;
            }
            if (TodCommon.TestBit((uint)mEmitterDef.mParticleFlags, 11) && !flag)
            {
                return;
            }
            for (int i = 0; i < mParticleList.Count; i++)
            {
                TodParticle theParticle = mParticleList[i];
                DrawParticle(g, theParticle, doScale);
            }
        }

        public void SystemMove(float theX, float theY)
        {
            float num = theX - mSystemCenter.x;
            float num2 = theY - mSystemCenter.y;
            if (TodCommon.FloatApproxEqual(num, 0f) && TodCommon.FloatApproxEqual(num2, 0f))
            {
                return;
            }
            mSystemCenter.x = theX;
            mSystemCenter.y = theY;
            if (!TodCommon.TestBit((uint)mEmitterDef.mParticleFlags, 5))
            {
                foreach (TodParticle todParticle in mParticleList)
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
            TodParticleEmitter particleEmitter = theParticle.mParticleEmitter;
            TodEmitterDefinition todEmitterDefinition = particleEmitter.mEmitterDef;
            theParams.mRedIsSet = false;
            theParams.mRedIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mSystemRed);
            theParams.mRedIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleRed);
            theParams.mRedIsSet |= (particleEmitter.mColorOverride.mRed != 1f);
            theParams.mGreenIsSet = false;
            theParams.mGreenIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mSystemGreen);
            theParams.mGreenIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleGreen);
            theParams.mGreenIsSet |= (particleEmitter.mColorOverride.mGreen != 1f);
            theParams.mBlueIsSet = false;
            theParams.mBlueIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mSystemBlue);
            theParams.mBlueIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleBlue);
            theParams.mBlueIsSet |= (particleEmitter.mColorOverride.mBlue != 1f);
            theParams.mAlphaIsSet = false;
            theParams.mAlphaIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mSystemAlpha);
            theParams.mAlphaIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleAlpha);
            theParams.mAlphaIsSet |= (particleEmitter.mColorOverride.mAlpha != 1f);
            theParams.mParticleScaleIsSet = false;
            theParams.mParticleScaleIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleScale);
            theParams.mParticleScaleIsSet |= (particleEmitter.mScaleOverride != 1f);
            theParams.mParticleStretchIsSet = Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleStretch);
            theParams.mSpinPositionIsSet = false;
            theParams.mSpinPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleSpinSpeed);
            theParams.mSpinPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mParticleSpinAngle);
            theParams.mSpinPositionIsSet |= TodCommon.TestBit((uint)todEmitterDefinition.mParticleFlags, 0);
            theParams.mSpinPositionIsSet |= TodCommon.TestBit((uint)todEmitterDefinition.mParticleFlags, 1);
            theParams.mPositionIsSet = false;
            theParams.mPositionIsSet |= (todEmitterDefinition.mParticleFieldCount > 0f);
            theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterRadius);
            theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterOffsetX);
            theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterOffsetY);
            theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterBoxX);
            theParams.mPositionIsSet |= Definition.FloatTrackIsSet(ref todEmitterDefinition.mEmitterBoxY);
            float num = particleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemRed, ParticleSystemTracks.SystemRed);
            float num2 = particleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemGreen, ParticleSystemTracks.SystemGreen);
            float num3 = particleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemBlue, ParticleSystemTracks.SystemBlue);
            float num4 = particleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemAlpha, ParticleSystemTracks.SystemAlpha);
            float num5 = particleEmitter.SystemTrackEvaluate(ref todEmitterDefinition.mSystemBrightness, ParticleSystemTracks.SystemBrightness);
            float num6 = particleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleRed, theParticle, ParticleTracks.ParticleRed);
            float num7 = particleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleGreen, theParticle, ParticleTracks.ParticleGreen);
            float num8 = particleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleBlue, theParticle, ParticleTracks.ParticleBlue);
            float num9 = particleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleAlpha, theParticle, ParticleTracks.ParticleAlpha);
            float num10 = particleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleBrightness, theParticle, ParticleTracks.ParticleBrightness);
            float num11 = num10 * num5;
            theParams.mRed = num6 * num * particleEmitter.mColorOverride.mRed * num11;
            theParams.mGreen = num7 * num2 * particleEmitter.mColorOverride.mGreen * num11;
            theParams.mBlue = num8 * num3 * particleEmitter.mColorOverride.mBlue * num11;
            theParams.mAlpha = num9 * num4 * particleEmitter.mColorOverride.mAlpha;
            theParams.mPosX = theParticle.mPosition.x;
            theParams.mPosY = theParticle.mPosition.y;
            theParams.mParticleScale = particleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleScale, theParticle, ParticleTracks.ParticleScale);
            theParams.mParticleStretch = particleEmitter.ParticleTrackEvaluate(ref todEmitterDefinition.mParticleStretch, theParticle, ParticleTracks.ParticleStretch);
            theParams.mParticleScale *= particleEmitter.mScaleOverride;
            theParams.mSpinPosition = theParticle.mSpinPosition;
            TodParticle todParticle = null;
            if (particleEmitter.mParticleSystem.mParticleHolder.mParticles.Contains(theParticle.mCrossFadeParticleID))
            {
                todParticle = particleEmitter.mParticleSystem.mParticleHolder.mParticles[particleEmitter.mParticleSystem.mParticleHolder.mParticles.IndexOf(theParticle.mCrossFadeParticleID)];
            }
            if (todParticle != null)
            {
                ParticleRenderParams particleRenderParams = default(ParticleRenderParams);
                if (TodParticleEmitter.GetRenderParams(todParticle, ref particleRenderParams))
                {
                    float theFraction = theParticle.mParticleAge / (float)(todParticle.mCrossFadeDuration - 1);
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
            particleRenderParams.mPosX += g.mTransX;
            particleRenderParams.mPosY += g.mTransY;
            TodParticle theParticle2;
            if (mImageOverride != null || mEmitterDef.mImage != null)
            {
                theParticle2 = theParticle;
            }
            else
            {
                TodParticle crossFadeParticleID = theParticle.mCrossFadeParticleID;
                if (crossFadeParticleID == null)
                {
                    return;
                }
                theParticle2 = crossFadeParticleID;
            }
            TodParticleGlobal.RenderParticle(g, theParticle2, theColor, ref particleRenderParams);
        }

        public void UpdateSpawning()
        {
            TodParticleEmitter todParticleEmitter = this;
            TodParticleEmitter todParticleEmitter2 = null;
            if (mCrossFadeEmitterID != null && mCrossFadeEmitterID.mActive)
            {
                todParticleEmitter2 = mCrossFadeEmitterID;
            }
            if (todParticleEmitter2 != null)
            {
                todParticleEmitter = todParticleEmitter2;
            }
            mSpawnAccum += Definition.FloatTrackEvaluate(ref todParticleEmitter.mEmitterDef.mSpawnRate, todParticleEmitter.mSystemTimeValue, todParticleEmitter.mTrackInterp[0]) * 0.01f;
            int num = (int)mSpawnAccum;
            mSpawnAccum -= num;
            int num2 = (int)Definition.FloatTrackEvaluate(ref todParticleEmitter.mEmitterDef.mSpawnMinActive, todParticleEmitter.mSystemTimeValue, todParticleEmitter.mTrackInterp[1]);
            if (num2 >= 0)
            {
                int num3 = num2 - mParticleList.Count;
                if (num < num3)
                {
                    num = num3;
                }
            }
            int num4 = (int)Definition.FloatTrackEvaluate(ref todParticleEmitter.mEmitterDef.mSpawnMaxActive, todParticleEmitter.mSystemTimeValue, todParticleEmitter.mTrackInterp[2]);
            if (num4 >= 0)
            {
                int num5 = num4 - mParticleList.Count;
                if (num > num5)
                {
                    num = num5;
                }
            }
            if (Definition.FloatTrackIsSet(ref todParticleEmitter.mEmitterDef.mSpawnMaxLaunched))
            {
                int num6 = (int)Definition.FloatTrackEvaluate(ref todParticleEmitter.mEmitterDef.mSpawnMaxLaunched, todParticleEmitter.mSystemTimeValue, todParticleEmitter.mTrackInterp[3]);
                int num7 = num6 - mParticlesSpawned;
                if (num > num7)
                {
                    num = num7;
                }
            }
            for (int i = 0; i < num; i++)
            {
                TodParticle theParticle = SpawnParticle(i, num);
                if (todParticleEmitter2 != null)
                {
                    CrossFadeParticle(theParticle, todParticleEmitter2);
                }
            }
        }

        public bool UpdateParticle(TodParticle theParticle)
        {
            if (theParticle.mParticleAge >= theParticle.mParticleDuration)
            {
                if (TodCommon.TestBit((uint)mEmitterDef.mParticleFlags, 4))
                {
                    theParticle.mParticleAge = 0;
                }
                else if (theParticle.mCrossFadeDuration > 0)
                {
                    theParticle.mParticleAge = theParticle.mParticleDuration - 1;
                }
                else
                {
                    if (mEmitterDef.mOnDuration.Length <= 0)
                    {
                        return false;
                    }
                    char c = mEmitterDef.mOnDuration[0];
                    if (!CrossFadeParticleToName(theParticle, mEmitterDef.mOnDuration))
                    {
                        return false;
                    }
                }
            }
            if (theParticle.mCrossFadeParticleID != null && mParticleSystem.mParticleHolder.mParticles[mParticleSystem.mParticleHolder.mParticles.IndexOf(theParticle.mCrossFadeParticleID)] == null)
            {
                return false;
            }
            theParticle.mParticleTimeValue = theParticle.mParticleAge / (float)(theParticle.mParticleDuration - 1);
            for (int i = 0; i < mEmitterDef.mParticleFieldCount; i++)
            {
                ParticleField theParticleField = mEmitterDef.mParticleFields[i];
                UpdateParticleField(theParticle, theParticleField, theParticle.mParticleTimeValue, i);
            }
            theParticle.mPosition += theParticle.mVelocity;
            float num = Definition.FloatTrackEvaluate(ref mEmitterDef.mParticleSpinSpeed, theParticle.mParticleTimeValue, theParticle.mParticleInterp[5]) * 0.01f;
            float num2 = Definition.FloatTrackEvaluate(ref mEmitterDef.mParticleSpinAngle, theParticle.mParticleTimeValue, theParticle.mParticleInterp[6]);
            float num3 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref mEmitterDef.mParticleSpinAngle, theParticle.mParticleLastTimeValue, theParticle.mParticleInterp[6]);
            theParticle.mSpinPosition += TodCommon.DegToRad(num + num2 - num3);
            theParticle.mSpinPosition += theParticle.mSpinVelocity;
            if (Definition.FloatTrackIsSet(ref mEmitterDef.mAnimationRate))
            {
                float num4 = Definition.FloatTrackEvaluate(ref mEmitterDef.mAnimationRate, theParticle.mParticleTimeValue, theParticle.mParticleInterp[15]) * 0.01f;
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
            if (mParticleSystem.mParticleHolder.mParticles.Count == mParticleSystem.mParticleHolder.mParticles.Capacity)
            {
                Debug.OutputDebug<string>(mEmitterDef.mName);
                return null;
            }
            TodParticle newTodParticle = TodParticle.GetNewTodParticle();
            mParticleSystem.mParticleHolder.mParticles.Add(newTodParticle);
            for (int i = 0; i < mEmitterDef.mParticleFieldCount; i++)
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
            newTodParticle.mParticleDuration = (int)Definition.FloatTrackEvaluate(ref mEmitterDef.mParticleDuration, mSystemTimeValue, theInterp);
            newTodParticle.mParticleDuration = Math.Max(1, newTodParticle.mParticleDuration);
            newTodParticle.mParticleAge = 0;
            newTodParticle.mParticleEmitter = this;
            newTodParticle.mParticleTimeValue = -1f;
            newTodParticle.mParticleLastTimeValue = -1f;
            if (TodCommon.TestBit((uint)mEmitterDef.mParticleFlags, 6))
            {
                newTodParticle.mParticleAge = RandomNumbers.NextNumber(newTodParticle.mParticleDuration);
            }
            else
            {
                newTodParticle.mParticleAge = 0;
            }
            float num = Definition.FloatTrackEvaluate(ref mEmitterDef.mLaunchSpeed, mSystemTimeValue, theInterp2) * 0.01f;
            float theInterp5 = RandomNumbers.NextNumber(1f);
            float num2;
            if (mEmitterDef.mEmitterType == EmitterType.CirclePath)
            {
                num2 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterPath, mSystemTimeValue, mTrackInterp[4]) * 3.1415927f * 2f;
                num2 += TodCommon.DegToRad(Definition.FloatTrackEvaluate(ref mEmitterDef.mLaunchAngle, mSystemTimeValue, theInterp5));
            }
            else if (mEmitterDef.mEmitterType == EmitterType.CircleEvenSpacing)
            {
                num2 = 6.2831855f * theIndex / theSpawnCount;
                num2 += TodCommon.DegToRad(Definition.FloatTrackEvaluate(ref mEmitterDef.mLaunchAngle, mSystemTimeValue, theInterp5));
            }
            else if (Definition.FloatTrackIsConstantZero(ref mEmitterDef.mLaunchAngle))
            {
                num2 = RandomNumbers.NextNumber(6.2831855f);
            }
            else
            {
                num2 = TodCommon.DegToRad(Definition.FloatTrackEvaluate(ref mEmitterDef.mLaunchAngle, mSystemTimeValue, theInterp5));
            }
            float num3 = 0f;
            float num4 = 0f;
            if (mEmitterDef.mEmitterType == EmitterType.Circle || mEmitterDef.mEmitterType == EmitterType.CirclePath || mEmitterDef.mEmitterType == EmitterType.CircleEvenSpacing)
            {
                float theInterp6 = RandomNumbers.NextNumber(1f);
                float num5 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterRadius, mSystemTimeValue, theInterp6);
                num3 = (float)Math.Sin(num2) * num5;
                num4 = (float)Math.Cos(num2) * num5;
            }
            else if (mEmitterDef.mEmitterType == EmitterType.Box)
            {
                float theInterp7 = RandomNumbers.NextNumber(1f);
                float theInterp8 = RandomNumbers.NextNumber(1f);
                num3 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterBoxX, mSystemTimeValue, theInterp7);
                num4 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterBoxY, mSystemTimeValue, theInterp8);
            }
            else if (mEmitterDef.mEmitterType == EmitterType.BoxPath)
            {
                float num6 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterPath, mSystemTimeValue, mTrackInterp[4]);
                float num7 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterBoxX, mSystemTimeValue, 0f);
                float num8 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterBoxX, mSystemTimeValue, 1f);
                float num9 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterBoxY, mSystemTimeValue, 0f);
                float num10 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterBoxY, mSystemTimeValue, 1f);
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
            float num15 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterSkewX, mSystemTimeValue, theInterp9);
            float num16 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterSkewY, mSystemTimeValue, theInterp10);
            newTodParticle.mPosition.x = mSystemCenter.x + num3 + num4 * num15;
            newTodParticle.mPosition.y = mSystemCenter.y + num4 + num3 * num16;
            newTodParticle.mVelocity.x = (float)Math.Sin(num2) * num;
            newTodParticle.mVelocity.y = (float)Math.Cos(num2) * num;
            float num17 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterOffsetX, mSystemTimeValue, theInterp3);
            float num18 = Definition.FloatTrackEvaluate(ref mEmitterDef.mEmitterOffsetY, mSystemTimeValue, theInterp4);
            TodParticle todParticle = newTodParticle;
            todParticle.mPosition.x = todParticle.mPosition.x + num17;
            TodParticle todParticle2 = newTodParticle;
            todParticle2.mPosition.y = todParticle2.mPosition.y + num18;
            newTodParticle.mAnimationTimeValue = 0f;
            if (mEmitterDef.mAnimated != 0 || Definition.FloatTrackIsSet(ref mEmitterDef.mAnimationRate))
            {
                newTodParticle.mImageFrame = 0;
            }
            else
            {
                newTodParticle.mImageFrame = RandomNumbers.NextNumber(mEmitterDef.mImageFrames);
            }
            if (TodCommon.TestBit((uint)mEmitterDef.mParticleFlags, 0))
            {
                newTodParticle.mSpinPosition = RandomNumbers.NextNumber(6.2831855f);
            }
            else if (TodCommon.TestBit((uint)mEmitterDef.mParticleFlags, 1))
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
            mParticleList.Insert(0, todParticle3);
            mParticlesSpawned++;
            UpdateParticle(newTodParticle);
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
            if (mEmitterCrossFadeCountDown > 0)
            {
                theParticle.mCrossFadeDuration = mEmitterCrossFadeCountDown;
            }
            else
            {
                float theInterp = RandomNumbers.NextNumber(1f);
                theParticle.mCrossFadeDuration = (int)Definition.FloatTrackEvaluate(ref theToEmitter.mEmitterDef.mCrossFadeDuration, mSystemTimeValue, theInterp);
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
            if (mEmitterCrossFadeCountDown > 0)
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
            mEmitterCrossFadeCountDown = (int)Definition.FloatTrackEvaluate(ref theToEmitter.mEmitterDef.mCrossFadeDuration, mSystemTimeValue, theInterp);
            mEmitterCrossFadeCountDown = Math.Max(1, mEmitterCrossFadeCountDown);
            mCrossFadeEmitterID = theToEmitter;
            if (!Definition.FloatTrackIsSet(ref theToEmitter.mEmitterDef.mSystemDuration))
            {
                theToEmitter.mSystemDuration = mEmitterCrossFadeCountDown;
            }
            foreach (TodParticle todParticle in mParticleList)
            {
                TodParticle theParticle = todParticle;
                CrossFadeParticle(theParticle, theToEmitter);
            }
        }

        public bool CrossFadeParticleToName(TodParticle theParticle, string theEmitterName)
        {
            TodEmitterDefinition todEmitterDefinition = mParticleSystem.FindEmitterDefByName(theEmitterName);
            if (todEmitterDefinition == null)
            {
                Debug.OutputDebug<string>(Common.StrFormat_("Can't find emitter to cross fade: {0}\n", theEmitterName));
                return false;
            }
            if (mParticleSystem.mParticleHolder.mEmitters.Count == mParticleSystem.mParticleHolder.mEmitters.Capacity)
            {
                Debug.OutputDebug<string>("Too many emitters to cross fade\n");
                return false;
            }
            TodParticleEmitter todParticleEmitter = new TodParticleEmitter();
            todParticleEmitter.TodEmitterInitialize(mSystemCenter.x, mSystemCenter.y, mParticleSystem, todEmitterDefinition);
            todParticleEmitter.mActive = true;
            mParticleSystem.mParticleHolder.mEmitters.Add(todParticleEmitter);
            TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
            mParticleSystem.mEmitterList.Add(todParticleEmitter2);
            return CrossFadeParticle(theParticle, todParticleEmitter);
        }

        public void DeleteAll()
        {
            for (int i = 0; i < mParticleList.Count; i++)
            {
                TodParticle todParticle = mParticleList[i];
                mParticleSystem.mParticleHolder.mParticles.Remove(todParticle);
                todParticle.PrepareForReuse();
            }
            mParticleList.Clear();
        }

        public void UpdateParticleField(TodParticle theParticle, ParticleField theParticleField, float theParticleTimeValue, int theFieldIndex)
        {
            float theInterp = theParticle.mParticleFieldInterp[theFieldIndex, 0];
            float theInterp2 = theParticle.mParticleFieldInterp[theFieldIndex, 1];
            float num = Definition.FloatTrackEvaluate(ref theParticleField.mX, theParticleTimeValue, theInterp);
            float num2 = Definition.FloatTrackEvaluate(ref theParticleField.mY, theParticleTimeValue, theInterp2);
            switch (theParticleField.mFieldType)
            {
            case ParticleFieldType.Invalid:
            case ParticleFieldType.SystemPosition:
                break;
            case ParticleFieldType.Friction:
                theParticle.mVelocity.x = theParticle.mVelocity.x * (1f - num);
                theParticle.mVelocity.y = theParticle.mVelocity.y * (1f - num2);
                return;
            case ParticleFieldType.Acceleration:
                theParticle.mVelocity.x = theParticle.mVelocity.x + num * 0.01f;
                theParticle.mVelocity.y = theParticle.mVelocity.y + num2 * 0.01f;
                return;
            case ParticleFieldType.Attractor:
            {
                float num3 = num - (theParticle.mPosition.x - mSystemCenter.x);
                float num4 = num2 - (theParticle.mPosition.y - mSystemCenter.y);
                theParticle.mVelocity.x = theParticle.mVelocity.x + num3 * 0.01f;
                theParticle.mVelocity.y = theParticle.mVelocity.y + num4 * 0.01f;
                return;
            }
            case ParticleFieldType.MaxVelocity:
                theParticle.mVelocity.x = TodCommon.ClampFloat(theParticle.mVelocity.x, -num, num);
                theParticle.mVelocity.y = TodCommon.ClampFloat(theParticle.mVelocity.y, -num2, num2);
                return;
            case ParticleFieldType.Velocity:
                theParticle.mPosition.x = theParticle.mPosition.x + num * 0.01f;
                theParticle.mPosition.y = theParticle.mPosition.y + num2 * 0.01f;
                return;
            case ParticleFieldType.Position:
            {
                float num5 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mX, theParticle.mParticleLastTimeValue, theInterp);
                float num6 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mY, theParticle.mParticleLastTimeValue, theInterp2);
                theParticle.mPosition.x = theParticle.mPosition.x + (num - num5);
                theParticle.mPosition.y = theParticle.mPosition.y + (num2 - num6);
                return;
            }
            case ParticleFieldType.GroundConstraint:
                if (theParticle.mPosition.y >= mSystemCenter.y + num2)
                {
                    theParticle.mPosition.y = mSystemCenter.y + num2;
                    float num7 = Definition.FloatTrackEvaluate(ref mEmitterDef.mCollisionReflect, theParticleTimeValue, theParticle.mParticleInterp[9]);
                    float num8 = Definition.FloatTrackEvaluate(ref mEmitterDef.mCollisionSpin, theParticleTimeValue, theParticle.mParticleInterp[10]) / 1000f;
                    theParticle.mSpinVelocity = theParticle.mVelocity.y * num8;
                    theParticle.mVelocity.x = theParticle.mVelocity.x * num7;
                    theParticle.mVelocity.y = -theParticle.mVelocity.y * num7;
                    return;
                }
                break;
            case ParticleFieldType.Shake:
            {
                float num9 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mX, theParticle.mParticleLastTimeValue, theInterp);
                float num10 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mY, theParticle.mParticleLastTimeValue, theInterp2);
                int num11 = theParticle.mParticleAge - 1;
                if (num11 == -1)
                {
                    num11 = theParticle.mParticleDuration - 1;
                }
                theParticle.mPosition.x = theParticle.mPosition.x - num9 * (RandomNumbers.NextNumber() * 0f * 2f - 1f);
                theParticle.mPosition.y = theParticle.mPosition.y - num10 * (RandomNumbers.NextNumber() * 0f * 2f - 1f);
                theParticle.mPosition.x = theParticle.mPosition.x + num * (RandomNumbers.NextNumber() * 0f * 2f - 1f);
                theParticle.mPosition.y = theParticle.mPosition.y + num2 * (RandomNumbers.NextNumber() * 0f * 2f - 1f);
                return;
            }
            case ParticleFieldType.Circle:
            {
                float num12 = num * 0.01f;
                float num13 = num2 * 0.01f;
                SexyVector2 sexyVector = theParticle.mPosition - mSystemCenter;
                float num14 = sexyVector.Magnitude();
                SexyVector2 rhs = sexyVector.Perp();
                rhs = rhs.Normalize();
                rhs.mVector *= num12 + num14 * num13;
                theParticle.mPosition += rhs;
                return;
            }
            case ParticleFieldType.Away:
            {
                float num15 = num * 0.01f;
                float num16 = num2 * 0.01f;
                SexyVector2 sexyVector2 = theParticle.mPosition - mSystemCenter;
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
            float theInterp = mSystemFieldInterp[theFieldIndex, 0];
            float theInterp2 = mSystemFieldInterp[theFieldIndex, 1];
            float num = Definition.FloatTrackEvaluate(ref theParticleField.mX, theParticleTimeValue, theInterp);
            float num2 = Definition.FloatTrackEvaluate(ref theParticleField.mY, theParticleTimeValue, theInterp2);
            ParticleFieldType fieldType = theParticleField.mFieldType;
            if (fieldType != ParticleFieldType.Invalid)
            {
                if (fieldType != ParticleFieldType.SystemPosition)
                {
                    return;
                }
                float num3 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mX, mSystemLastTimeValue, theInterp);
                float num4 = TodParticleGlobal.FloatTrackEvaluateFromLastTime(ref theParticleField.mY, mSystemLastTimeValue, theInterp2);
                mSystemCenter.x = mSystemCenter.x + (num - num3);
                mSystemCenter.y = mSystemCenter.y + (num2 - num4);
            }
        }

        public float SystemTrackEvaluate(ref FloatParameterTrack theTrack, ParticleSystemTracks theInterp)
        {
            return Definition.FloatTrackEvaluate(ref theTrack, mSystemTimeValue, mTrackInterp[(int)theInterp]);
        }

        public float ParticleTrackEvaluate(ref FloatParameterTrack theTrack, TodParticle theParticle, ParticleTracks theInterp)
        {
            return Definition.FloatTrackEvaluate(ref theTrack, theParticle.mParticleTimeValue, theParticle.mParticleInterp[(int)theInterp]);
        }

        public void DeleteParticle(TodParticle theParticle)
        {
            TodParticle todParticle = null;
            if (mParticleSystem.mParticleHolder.mParticles.Contains(theParticle.mCrossFadeParticleID))
            {
                todParticle = mParticleSystem.mParticleHolder.mParticles[mParticleSystem.mParticleHolder.mParticles.IndexOf(theParticle.mCrossFadeParticleID)];
            }
            if (todParticle != null)
            {
                todParticle.mParticleEmitter.DeleteParticle(todParticle);
                theParticle.mCrossFadeParticleID = null;
            }
            theParticle.PrepareForReuse();
            mParticleList.Remove(theParticle);
            mParticleSystem.mParticleHolder.mParticles.Remove(theParticle);
        }

        public void DeleteNonCrossFading()
        {
            for (int i = mParticleList.Count - 1; i >= 0; i--)
            {
                TodParticle todParticle = mParticleList[i];
                TodParticle todParticle2 = todParticle;
                if (todParticle2.mCrossFadeDuration <= 0)
                {
                    DeleteParticle(todParticle2);
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
