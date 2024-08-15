using Sexy;
using Sexy.TodLib;
using System;
using System.Collections.Generic;
using static Lawn.GlobalMembersSaveGame;

namespace Lawn
{
    internal static class TodLibObjSyncer
    {
        public static void SyncList<T, TH>(
            List<T> lst,
            TH helper,
            SaveGameContext theContext,
            Func<T> getNew,
            Action<T, TH, SaveGameContext> sync
            )
            where T : class
        {
            int lstCount = lst.Count;
            theContext.SyncListLength(ref lstCount);
            if (theContext.mReading)
            {
                lst.Clear();
            }
            for (int i = 0; i < lstCount; i++)
            {
                if (theContext.mReading)
                {
                    lst.Add(getNew());
                }
                sync(lst[i], helper, theContext);
            }
        }

        public static void ClearObj(this EffectSystem theEffectSystem)
        {
            for (int i = 0; i < theEffectSystem.mParticleHolder.mParticleSystems.Count; i++)
            {
                TodParticleSystem particleSystem = theEffectSystem.mParticleHolder.mParticleSystems[i];
                particleSystem.mParticleHolder = theEffectSystem.mParticleHolder;
                particleSystem.ParticleSystemDie();
                particleSystem.PrepareForReuse();
            }
            theEffectSystem.mParticleHolder.mParticleSystems.Clear();
            theEffectSystem.mParticleHolder.mEmitters.Clear();
            theEffectSystem.mParticleHolder.mParticles.Clear();
            for (int i = 0; i < theEffectSystem.mReanimationHolder.mReanimations.Count; i++)
            {
                theEffectSystem.mReanimationHolder.mReanimations[i].PrepareForReuse();
            }
            theEffectSystem.mReanimationHolder.mReanimations.Clear();
            theEffectSystem.mTrailHolder.mTrails.Clear();
            for (int i = 0; i < theEffectSystem.mAttachmentHolder.mAttachments.Count; i++)
            {
                theEffectSystem.mAttachmentHolder.mAttachments[i].PrepareForReuse();
            }
            theEffectSystem.mAttachmentHolder.mAttachments.Clear();
        }

        public static void Sync(this EffectSystem theEffectSystem, SaveGameContext theContext)
        {
            SyncList(theEffectSystem.mParticleHolder.mParticles, theEffectSystem, theContext, TodParticle.GetNewTodParticle, SyncTodParticle);
            SyncList(theEffectSystem.mParticleHolder.mEmitters, theEffectSystem, theContext, TodParticleEmitter.GetNewTodParticleEmitter, SyncTodParticleEmitter);
            SyncList(theEffectSystem.mParticleHolder.mParticleSystems, theEffectSystem, theContext, TodParticleSystem.GetNewTodParticleSystem, SyncTodParticleSystem);
            SyncList(theEffectSystem.mTrailHolder.mTrails, theEffectSystem, theContext, static () => new Trail(), SyncTrail);
            SyncList(theEffectSystem.mReanimationHolder.mReanimations, theEffectSystem, theContext, Reanimation.GetNewReanimation, SyncReanimation);
            SyncList(theEffectSystem.mAttachmentHolder.mAttachments, theEffectSystem, theContext, Attachment.GetNewAttachment, SyncAttachment);
        }

        public static void SyncTodParticle(TodParticle theParticle, EffectSystem theEffectSystem, SaveGameContext theContext)
        {
            theContext.SyncInt(ref theParticle.mParticleEmitter_Save);
            theContext.SyncInt(ref theParticle.mParticleDuration);
            theContext.SyncInt(ref theParticle.mParticleAge);
            theContext.SyncFloat(ref theParticle.mParticleTimeValue);
            theContext.SyncFloat(ref theParticle.mParticleLastTimeValue);
            theContext.SyncFloat(ref theParticle.mAnimationTimeValue);
            theContext.SyncSexyVector2(ref theParticle.mVelocity);
            theContext.SyncSexyVector2(ref theParticle.mPosition);
            theContext.SyncInt(ref theParticle.mImageFrame);
            theContext.SyncFloat(ref theParticle.mSpinPosition);
            theContext.SyncFloat(ref theParticle.mSpinVelocity);
            theContext.SyncInt(ref theParticle.mCrossFadeParticleID_Save);
            theContext.SyncInt(ref theParticle.mCrossFadeDuration);
            for (int i = 0; i < 16; i++)
            {
                theContext.SyncFloat(ref theParticle.mParticleInterp[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    theContext.SyncFloat(ref theParticle.mParticleFieldInterp[i, j]);
                }
            }
        }

        public static void SyncTodParticleEmitter(TodParticleEmitter theParticleEmitter, EffectSystem theEffectSystem, SaveGameContext theContext)
        {
            theContext.SyncInt(ref theParticleEmitter.mEmitterDef_Save);
            theContext.SyncInt(ref theParticleEmitter.mParticleSystem_Save);
            int particleCount = theParticleEmitter.mParticleList.Count;
            theContext.SyncInt(ref particleCount);
            if (theContext.mReading)
            {
                theParticleEmitter.mParticleList.Clear();
            }
            for (int i = 0; i < particleCount; i++)
            {
                if (theContext.mReading)
                {
                    int index = 0;
                    theContext.SyncInt(ref index);
                    TodParticle particle = null;
                    if (index >= 0 && index < theEffectSystem.mParticleHolder.mParticles.Count)
                    {
                        particle = theEffectSystem.mParticleHolder.mParticles[index];
                    }
                    theParticleEmitter.mParticleList.Add(particle);
                }
                else
                {
                    int index = theEffectSystem.mParticleHolder.mParticles.IndexOf(theParticleEmitter.mParticleList[i]);
                    theContext.SyncInt(ref index);
                }
            }
            theContext.SyncFloat(ref theParticleEmitter.mSpawnAccum);
            theContext.SyncSexyVector2(ref theParticleEmitter.mSystemCenter);
            theContext.SyncInt(ref theParticleEmitter.mParticlesSpawned);
            theContext.SyncInt(ref theParticleEmitter.mSystemAge);
            theContext.SyncInt(ref theParticleEmitter.mSystemDuration);
            theContext.SyncFloat(ref theParticleEmitter.mSystemTimeValue);
            theContext.SyncFloat(ref theParticleEmitter.mSystemLastTimeValue);
            theContext.SyncBool(ref theParticleEmitter.mDead);
            theContext.SyncSexyColor(ref theParticleEmitter.mColorOverride);
            theContext.SyncBool(ref theParticleEmitter.mExtraAdditiveDrawOverride);
            theContext.SyncFloat(ref theParticleEmitter.mScaleOverride);
            theContext.SyncImage(ref theParticleEmitter.mImageOverride);
            theContext.SyncInt(ref theParticleEmitter.mCrossFadeEmitterID_Save);
            theContext.SyncInt(ref theParticleEmitter.mEmitterCrossFadeCountDown);
            theContext.SyncInt(ref theParticleEmitter.mFrameOverride);
            theContext.SyncBool(ref theParticleEmitter.mActive);
            for (int i = 0; i < 10; i++)
            {
                theContext.SyncFloat(ref theParticleEmitter.mTrackInterp[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    theContext.SyncFloat(ref theParticleEmitter.mSystemFieldInterp[i, j]);
                }
            }
        }

        public static void SyncTodParticleSystem(TodParticleSystem theParticleSystem, EffectSystem theEffectSystem, SaveGameContext theContext)
        {
            theContext.SyncEnum(ref theParticleSystem.mEffectType, theContext.aParticleEffectSaver);
            if (theContext.mReading)
            {
                int effectIndex = (int)theParticleSystem.mEffectType;
                theParticleSystem.mParticleDef =
                    (effectIndex >= 0 && effectIndex < TodParticleGlobal.gParticleDefArray.Length)
                    ? TodParticleGlobal.gParticleDefArray[effectIndex] : null;
                theParticleSystem.mParticleHolder = theEffectSystem.mParticleHolder;
            }
            int emitterCount = theParticleSystem.mEmitterList.Count;
            theContext.SyncInt(ref emitterCount);
            if (theContext.mReading)
            {
                theParticleSystem.mEmitterList.Clear();
            }
            for (int i = 0; i < emitterCount; i++)
            {
                if (theContext.mReading)
                {
                    int index = 0;
                    theContext.SyncInt(ref index);
                    TodParticleEmitter emitter = null;
                    if (index >= 0 && index < theEffectSystem.mParticleHolder.mEmitters.Count)
                    {
                        emitter = theEffectSystem.mParticleHolder.mEmitters[index];
                    }
                    theParticleSystem.mEmitterList.Add(emitter);
                }
                else
                {
                    int index = theEffectSystem.mParticleHolder.mEmitters.IndexOf(theParticleSystem.mEmitterList[i]);
                    theContext.SyncInt(ref index);
                }
            }
            theContext.SyncBool(ref theParticleSystem.mDead);
            theContext.SyncBool(ref theParticleSystem.mIsAttachment);
            theContext.SyncInt(ref theParticleSystem.mRenderOrder);
            theContext.SyncBool(ref theParticleSystem.mDontUpdate);
            theContext.SyncBool(ref theParticleSystem.mActive);
        }

        public static void SyncTrail(Trail theTrail, EffectSystem theEffectSystem, SaveGameContext theContext)
        {
            if (theContext.mReading)
            {
                TrailType trailType = TrailType.None;
                theContext.SyncEnum(ref trailType, theContext.aTrailTypeSaver);
                int trailIndex = (int)trailType;
                theTrail.mDefinition =
                    (trailIndex >= 0 && trailIndex < GlobalMembersTrail.gTrailDefArray.Length)
                    ? GlobalMembersTrail.gTrailDefArray[trailIndex] : null;
                theTrail.mTrailHolder = theEffectSystem.mTrailHolder;
            }
            else
            {
                int trailIndex = ((IList<TrailDefinition>)GlobalMembersTrail.gTrailDefArray).IndexOf(theTrail.mDefinition);
                TrailType trailType = (TrailType)trailIndex;
                theContext.SyncEnum(ref trailType, theContext.aTrailTypeSaver);
            }
            if (theContext.mVersion >= 1)
            {
                for (int i = 0; i < 20; i++)
                {
                    TrailPoint trailPoint = theTrail.mTrailPoints[i];
                    theContext.SyncSexyVector2(ref trailPoint.aPos);
                    theTrail.mTrailPoints[i] = trailPoint;
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    bool hasInstance = true;
                    theContext.SyncBool(ref hasInstance);
                    if (hasInstance)
                    {
                        TrailPoint trailPoint = theTrail.mTrailPoints[i];
                        theContext.SyncSexyVector2(ref trailPoint.aPos);
                        theTrail.mTrailPoints[i] = trailPoint;
                    }
                    else
                    {
                        theTrail.mTrailPoints[i] = default;
                    }
                }
            }
            theContext.SyncInt(ref theTrail.mNumTrailPoints);
            theContext.SyncBool(ref theTrail.mDead);
            theContext.SyncInt(ref theTrail.mRenderOrder);
            theContext.SyncInt(ref theTrail.mTrailAge);
            theContext.SyncInt(ref theTrail.mTrailDuration);
            for (int i = 0; i < 4; i++)
            {
                theContext.SyncFloat(ref theTrail.mTrailInterp[i]);
            }
            theContext.SyncSexyVector2(ref theTrail.mTrailCenter);
            theContext.SyncBool(ref theTrail.mIsAttachment);
            theContext.SyncSexyColor(ref theTrail.mColorOverride);
        }

        public static void SyncReanimation(Reanimation theReanimation, EffectSystem theEffectSystem, SaveGameContext theContext)
        {
            theContext.SyncEnum(ref theReanimation.mReanimationType, theContext.aReanimationTypeSaver);
            if (theContext.mReading)
            {
                int reanimIndex = (int)theReanimation.mReanimationType;
                if (reanimIndex >= 0 && reanimIndex < ReanimatorXnaHelpers.gReanimatorDefArray.Length)
                {
                    ReanimatorXnaHelpers.ReanimatorEnsureDefinitionLoaded(theReanimation.mReanimationType, true);
                    theReanimation.mDefinition = ReanimatorXnaHelpers.gReanimatorDefArray[reanimIndex];
                }
                theReanimation.mReanimationHolder = theEffectSystem.mReanimationHolder;
            }
            theContext.SyncFloat(ref theReanimation.mAnimTime);
            theContext.SyncFloat(ref theReanimation.mAnimRate);
            theContext.SyncEnum(ref theReanimation.mLoopType, theContext.aReanimLoopTypeSaver);
            theContext.SyncBool(ref theReanimation.mDead);
            theContext.SyncShort(ref theReanimation.mFrameStart);
            theContext.SyncShort(ref theReanimation.mFrameCount);
            theContext.SyncShort(ref theReanimation.mFrameBasePose);
            theContext.SyncSexyTransform2D(ref theReanimation.mOverlayMatrix);
            theContext.SyncSexyColor(ref theReanimation.mColorOverride);
            for (int i = 0; i < 100; i++)
            {
                bool hasInstance = theReanimation.mTrackInstances[i] != null;
                theContext.SyncBool(ref hasInstance);
                if (hasInstance)
                {
                    theReanimation.mTrackInstances[i] ??= ReanimatorTrackInstance.GetNewReanimatorTrackInstance();
                    ReanimatorTrackInstance trackInstance = theReanimation.mTrackInstances[i];
                    theContext.SyncByte(ref trackInstance.mBlendCounter);
                    theContext.SyncByte(ref trackInstance.mBlendTime);
                    bool hasTransform = trackInstance.mBlendTransform != null;
                    theContext.SyncBool(ref hasTransform);
                    if (hasTransform)
                    {
                        trackInstance.mBlendTransform ??= ReanimatorTransform.GetNewReanimatorTransform();
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mTransX);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mTransY);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mSkewX);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mSkewY);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mScaleX);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mScaleY);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mFrame);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mAlpha);
                        theContext.SyncImage(ref trackInstance.mBlendTransform.mImage);
                        // mFont mText
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mSkewXCos);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mSkewXSin);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mSkewYCos);
                        theContext.SyncFloat(ref trackInstance.mBlendTransform.mSkewYSin);
                    }
                    theContext.SyncFloat(ref trackInstance.mShakeOverride);
                    theContext.SyncFloat(ref trackInstance.mShakeX);
                    theContext.SyncFloat(ref trackInstance.mShakeY);
                    theContext.SyncInt(ref trackInstance.mAttachmentID_Save);
                    theContext.SyncImage(ref trackInstance.mImageOverride);
                    theContext.SyncInt(ref trackInstance.mRenderGroup);
                    theContext.SyncSexyColor(ref trackInstance.mTrackColor);
                    theContext.SyncBool(ref trackInstance.mIgnoreClipRect);
                    theContext.SyncBool(ref trackInstance.mTruncateDisappearingFrames);
                    theContext.SyncBool(ref trackInstance.mIgnoreColorOverride);
                    theContext.SyncBool(ref trackInstance.mIgnoreExtraAdditiveColor);
                }
            }
            theContext.SyncInt(ref theReanimation.mLoopCount);
            theContext.SyncBool(ref theReanimation.mIsAttachment);
            theContext.SyncInt(ref theReanimation.mRenderOrder);
            theContext.SyncSexyColor(ref theReanimation.mExtraAdditiveColor);
            theContext.SyncBool(ref theReanimation.mEnableExtraAdditiveDraw);
            theContext.SyncSexyColor(ref theReanimation.mExtraOverlayColor);
            theContext.SyncBool(ref theReanimation.mEnableExtraOverlayDraw);
            theContext.SyncFloat(ref theReanimation.mLastFrameTime);
            theContext.SyncEnum(ref theReanimation.mFilterEffect, theContext.aFilterEffectTypeSaver);
            theContext.SyncBool(ref theReanimation.mClip);
            theContext.SyncBool(ref theReanimation.mActive);
            theContext.SyncBool(ref theReanimation.mGetFrameTime);
            theContext.SyncFloat(ref theReanimation.mFrameTime.mFraction);
            theContext.SyncShort(ref theReanimation.mFrameTime.mAnimFrameBeforeInt);
            theContext.SyncShort(ref theReanimation.mFrameTime.mAnimFrameAfterInt);
            theContext.SyncSexyTransform2D(ref theReanimation.aOverlayMatrix);
            theContext.SyncMatrix(ref theReanimation.aBasePoseMatrix);
            theContext.SyncMatrix(ref theReanimation.tempOverlayMatrix);
            theContext.SyncSexyTransform2D(ref theReanimation.basePose);
        }

        public static void SyncAttachment(Attachment theAttachment, EffectSystem theEffectSystem, SaveGameContext theContext)
        {
            for (int i = 0; i < 16; i++)
            {
                bool hasInstance = theAttachment.mEffectArray[i] != null;
                theContext.SyncBool(ref hasInstance);
                if (hasInstance)
                {
                    theAttachment.mEffectArray[i] ??= AttachEffect.GetNewAttachEffect();
                    AttachEffect trackInstance = theAttachment.mEffectArray[i];
                    theContext.SyncEnum(ref trackInstance.mEffectType, theContext.aEffectTypeSaver);
                    theContext.SyncInt(ref trackInstance.mEffectID_Save);
                    theContext.SyncSexyTransform2D(ref trackInstance.mOffset);
                    theContext.SyncBool(ref trackInstance.mDontDrawIfParentHidden);
                    theContext.SyncBool(ref trackInstance.mDontPropogateColor);
                }
            }
            theContext.SyncInt(ref theAttachment.mNumEffects);
            theContext.SyncBool(ref theAttachment.mDead);
            theContext.SyncBool(ref theAttachment.mActive);
            theContext.SyncBool(ref theAttachment.mUsesClipping);
            theContext.SyncBool(ref theAttachment.reused);
        }

        public static void SyncObj(this EffectSystem theEffectSystem, SaveGameContext theContext)
        {
            List<TodParticle> particles = theEffectSystem.mParticleHolder.mParticles;
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].SyncObj(theContext);
            }
            List<TodParticleEmitter> particleEmitters = theEffectSystem.mParticleHolder.mEmitters;
            for (int i = 0; i < particleEmitters.Count; i++)
            {
                particleEmitters[i].SyncObj(theContext);
            }
            List<Reanimation> reanimations = theEffectSystem.mReanimationHolder.mReanimations;
            for (int i = 0; i < reanimations.Count; i++)
            {
                reanimations[i].SyncObj(theContext);
            }
            List<Attachment> attachments = theEffectSystem.mAttachmentHolder.mAttachments;
            for (int i = 0; i < attachments.Count; i++)
            {
                attachments[i].SyncObj(theContext);
            }
        }

        public static void SyncObj(this TodParticle theParticle, SaveGameContext theContext)
        {
            LawnApp aApp = GlobalStaticVars.gLawnApp;
            SyncObjFromList(ref theParticle.mParticleEmitter_Save, ref theParticle.mParticleEmitter, aApp.mEffectSystem.mParticleHolder.mEmitters, theContext.mReading);
            SyncObjFromList(ref theParticle.mCrossFadeParticleID_Save, ref theParticle.mCrossFadeParticleID, aApp.mEffectSystem.mParticleHolder.mParticles, theContext.mReading);
        }

        public static void SyncObj(this TodParticleEmitter theParticleEmitter, SaveGameContext theContext)
        {
            LawnApp aApp = GlobalStaticVars.gLawnApp;
            SyncObjFromList(ref theParticleEmitter.mParticleSystem_Save, ref theParticleEmitter.mParticleSystem, aApp.mEffectSystem.mParticleHolder.mParticleSystems, theContext.mReading);
            SyncObjFromList(ref theParticleEmitter.mCrossFadeEmitterID_Save, ref theParticleEmitter.mCrossFadeEmitterID, aApp.mEffectSystem.mParticleHolder.mEmitters, theContext.mReading);
            if (theContext.mReading)
            {
                if (theParticleEmitter.mEmitterDef_Save >= 0 && theParticleEmitter.mEmitterDef_Save < theParticleEmitter.mParticleSystem.mParticleDef.mEmitterDefs.Length)
                {
                    theParticleEmitter.mEmitterDef = theParticleEmitter.mParticleSystem.mParticleDef.mEmitterDefs[theParticleEmitter.mEmitterDef_Save];
                }
                else
                {
                    theParticleEmitter.mEmitterDef = null;
                }
            }
            else
            {
                theParticleEmitter.mEmitterDef_Save = ((IList<TodEmitterDefinition>)theParticleEmitter.mParticleSystem.mParticleDef.mEmitterDefs).IndexOf(theParticleEmitter.mEmitterDef);
            }
        }

        public static void SyncObj(this Reanimation theReanimation, SaveGameContext theContext)
        {
            LawnApp aApp = GlobalStaticVars.gLawnApp;
            for (int i = 0; i < theReanimation.mTrackInstances.Length; i++)
            {
                ReanimatorTrackInstance instance = theReanimation.mTrackInstances[i];
                if (instance != null)
                {
                    SyncObjFromList(ref instance.mAttachmentID_Save, ref instance.mAttachmentID, aApp.mEffectSystem.mAttachmentHolder.mAttachments, theContext.mReading);
                }
            }
        }

        public static void SyncObj(this Attachment theAttachment, SaveGameContext theContext)
        {
            LawnApp aApp = GlobalStaticVars.gLawnApp;
            for (int i = 0; i < theAttachment.mEffectArray.Length; i++)
            {
                AttachEffect effect = theAttachment.mEffectArray[i];
                if (effect != null)
                {
                    if (effect.mEffectType == EffectType.Particle)
                    {
                        TodParticleSystem particleSystem = effect.mEffectID as TodParticleSystem;
                        SyncObjFromList(ref effect.mEffectID_Save, ref particleSystem, aApp.mEffectSystem.mParticleHolder.mParticleSystems, theContext.mReading);
                        effect.mEffectID = particleSystem;
                    }
                    else if (effect.mEffectType == EffectType.Trail)
                    {
                        Trail trail = effect.mEffectID as Trail;
                        SyncObjFromList(ref effect.mEffectID_Save, ref trail, aApp.mEffectSystem.mTrailHolder.mTrails, theContext.mReading);
                        effect.mEffectID = trail;
                    }
                    else if (effect.mEffectType == EffectType.Reanim)
                    {
                        Reanimation reanimation = effect.mEffectID as Reanimation;
                        SyncObjFromList(ref effect.mEffectID_Save, ref reanimation, aApp.mEffectSystem.mReanimationHolder.mReanimations, theContext.mReading);
                        effect.mEffectID = reanimation;
                    }
                    else if (effect.mEffectType == EffectType.Attachment)
                    {
                        Attachment attachment = effect.mEffectID as Attachment;
                        SyncObjFromList(ref effect.mEffectID_Save, ref attachment, aApp.mEffectSystem.mAttachmentHolder.mAttachments, theContext.mReading);
                        effect.mEffectID = attachment;
                    }
                    else
                    {
                        Debug.ASSERT(false);
                    }
                }
            }
        }

        public static void SyncObjFromList<T>(ref int id, ref T obj, List<T> lst, bool id2obj)
        {
            if (id2obj)
            {
                if (id >= 0 && id < lst.Count)
                {
                    obj = lst[id];
                }
                else
                {
                    obj = default;
                }
            }
            else
            {
                id = obj == null ? -1 : lst.IndexOf(obj);
            }
        }
    }
}
