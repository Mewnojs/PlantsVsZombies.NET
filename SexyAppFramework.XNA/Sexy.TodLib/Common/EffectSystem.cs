using System;

namespace Sexy.TodLib
{
    public/*internal*/ class EffectSystem
    {
        public EffectSystem()
        {
            mParticleHolder = null;
            mTrailHolder = null;
            mReanimationHolder = null;
            mAttachmentHolder = null;
        }

        public void Dispose()
        {
        }

        public void EffectSystemInitialize()
        {
            Debug.ASSERT(EffectSystem.gEffectSystem == null);
            Debug.ASSERT(mParticleHolder == null && mTrailHolder == null && mReanimationHolder == null && mAttachmentHolder == null);
            EffectSystem.gEffectSystem = this;
            mParticleHolder = new TodParticleHolder();
            mTrailHolder = new TrailHolder();
            mReanimationHolder = new ReanimationHolder();
            mAttachmentHolder = new AttachmentHolder();
            mParticleHolder.InitializeHolder();
            mTrailHolder.InitializeHolder();
            mReanimationHolder.InitializeHolder();
            mAttachmentHolder.InitializeHolder();
        }

        public void EffectSystemDispose()
        {
            if (mParticleHolder != null)
            {
                mParticleHolder.DisposeHolder();
                mParticleHolder.Dispose();
                mParticleHolder = null;
            }
            if (mTrailHolder != null)
            {
                mTrailHolder.DisposeHolder();
                mTrailHolder.Dispose();
                mTrailHolder = null;
            }
            if (mReanimationHolder != null)
            {
                mReanimationHolder.DisposeHolder();
                mReanimationHolder = null;
            }
            if (mAttachmentHolder != null)
            {
                mAttachmentHolder.DisposeHolder();
                mAttachmentHolder.Dispose();
                mAttachmentHolder = null;
            }
            EffectSystem.gEffectSystem = null;
        }

        public void EffectSystemFreeAll()
        {
            mParticleHolder.mParticleSystems.Clear();
            for (int i = 0; i < mParticleHolder.mEmitters.Count; i++)
            {
                mParticleHolder.mEmitters[i].PrepareForReuse();
            }
            mParticleHolder.mEmitters.Clear();
            mParticleHolder.mParticles.Clear();
            mTrailHolder.mTrails.Clear();
            for (int j = 0; j < mReanimationHolder.mReanimations.Count; j++)
            {
                mReanimationHolder.mReanimations[j].PrepareForReuse();
            }
            mReanimationHolder.mReanimations.Clear();
            mAttachmentHolder.mAttachments.Clear();
        }

        public void ProcessDeleteQueue()
        {
            for (int i = mParticleHolder.mParticleSystems.Count - 1; i >= 0; i--)
            {
                if (mParticleHolder.mParticleSystems[i].mDead)
                {
                    mParticleHolder.mParticleSystems[i].ParticleSystemDie();
                    mParticleHolder.mParticleSystems[i].PrepareForReuse();
                    mParticleHolder.mParticleSystems.RemoveAt(i);
                }
            }
            for (int j = 0; j < mReanimationHolder.mReanimations.Count; j++)
            {
                Reanimation reanimation = mReanimationHolder.mReanimations[j];
                if (reanimation.mDead)
                {
                    mReanimationHolder.mReanimations[j].PrepareForReuse();
                    mReanimationHolder.mReanimations.RemoveAt(j);
                    j--;
                }
            }
            for (int k = 0; k < mTrailHolder.mTrails.Count; k++)
            {
                Trail trail = mTrailHolder.mTrails[k];
                if (trail.mDead)
                {
                    mTrailHolder.mTrails.RemoveAt(k);
                    k--;
                }
            }
            for (int l = mAttachmentHolder.mAttachments.Count - 1; l >= 0; l--)
            {
                if (mAttachmentHolder.mAttachments[l] == null || mAttachmentHolder.mAttachments[l].mDead)
                {
                    mAttachmentHolder.mAttachments[l].PrepareForReuse();
                    mAttachmentHolder.mAttachments.RemoveAt(l);
                }
            }
        }

        public void Update()//3update
        {
            foreach (TodParticleSystem todParticleSystem in mParticleHolder.mParticleSystems)
            {
                if (!todParticleSystem.mIsAttachment)
                {
                    todParticleSystem.Update();
                }
            }
            foreach (Reanimation reanimation in mReanimationHolder.mReanimations)
            {
                if (reanimation != null && !reanimation.mIsAttachment)
                {
                    reanimation.Update();
                }
            }
            foreach (Trail trail in mTrailHolder.mTrails)
            {
                if (!trail.mIsAttachment)
                {
                    trail.Update();
                }
            }
        }

        public static EffectSystem gEffectSystem;

        public TodParticleHolder mParticleHolder;

        public TrailHolder mTrailHolder;

        public ReanimationHolder mReanimationHolder;

        public AttachmentHolder mAttachmentHolder;
    }
}
