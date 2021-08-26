using System;

namespace Sexy.TodLib
{
	internal class EffectSystem
	{
		public EffectSystem()
		{
			this.mParticleHolder = null;
			this.mTrailHolder = null;
			this.mReanimationHolder = null;
			this.mAttachmentHolder = null;
		}

		public void Dispose()
		{
		}

		public void EffectSystemInitialize()
		{
			Debug.ASSERT(EffectSystem.gEffectSystem == null);
			Debug.ASSERT(this.mParticleHolder == null && this.mTrailHolder == null && this.mReanimationHolder == null && this.mAttachmentHolder == null);
			EffectSystem.gEffectSystem = this;
			this.mParticleHolder = new TodParticleHolder();
			this.mTrailHolder = new TrailHolder();
			this.mReanimationHolder = new ReanimationHolder();
			this.mAttachmentHolder = new AttachmentHolder();
			this.mParticleHolder.InitializeHolder();
			this.mTrailHolder.InitializeHolder();
			this.mReanimationHolder.InitializeHolder();
			this.mAttachmentHolder.InitializeHolder();
		}

		public void EffectSystemDispose()
		{
			if (this.mParticleHolder != null)
			{
				this.mParticleHolder.DisposeHolder();
				this.mParticleHolder.Dispose();
				this.mParticleHolder = null;
			}
			if (this.mTrailHolder != null)
			{
				this.mTrailHolder.DisposeHolder();
				this.mTrailHolder.Dispose();
				this.mTrailHolder = null;
			}
			if (this.mReanimationHolder != null)
			{
				this.mReanimationHolder.DisposeHolder();
				this.mReanimationHolder = null;
			}
			if (this.mAttachmentHolder != null)
			{
				this.mAttachmentHolder.DisposeHolder();
				this.mAttachmentHolder.Dispose();
				this.mAttachmentHolder = null;
			}
			EffectSystem.gEffectSystem = null;
		}

		public void EffectSystemFreeAll()
		{
			this.mParticleHolder.mParticleSystems.Clear();
			for (int i = 0; i < this.mParticleHolder.mEmitters.Count; i++)
			{
				this.mParticleHolder.mEmitters[i].PrepareForReuse();
			}
			this.mParticleHolder.mEmitters.Clear();
			this.mParticleHolder.mParticles.Clear();
			this.mTrailHolder.mTrails.Clear();
			for (int j = 0; j < this.mReanimationHolder.mReanimations.Count; j++)
			{
				this.mReanimationHolder.mReanimations[j].PrepareForReuse();
			}
			this.mReanimationHolder.mReanimations.Clear();
			this.mAttachmentHolder.mAttachments.Clear();
		}

		public void ProcessDeleteQueue()
		{
			for (int i = this.mParticleHolder.mParticleSystems.Count - 1; i >= 0; i--)
			{
				if (this.mParticleHolder.mParticleSystems[i].mDead)
				{
					this.mParticleHolder.mParticleSystems[i].ParticleSystemDie();
					this.mParticleHolder.mParticleSystems[i].PrepareForReuse();
					this.mParticleHolder.mParticleSystems.RemoveAt(i);
				}
			}
			for (int j = 0; j < this.mReanimationHolder.mReanimations.Count; j++)
			{
				Reanimation reanimation = this.mReanimationHolder.mReanimations[j];
				if (reanimation.mDead)
				{
					this.mReanimationHolder.mReanimations[j].PrepareForReuse();
					this.mReanimationHolder.mReanimations.RemoveAt(j);
					j--;
				}
			}
			for (int k = 0; k < this.mTrailHolder.mTrails.Count; k++)
			{
				Trail trail = this.mTrailHolder.mTrails[k];
				if (trail.mDead)
				{
					this.mTrailHolder.mTrails.RemoveAt(k);
					k--;
				}
			}
			for (int l = this.mAttachmentHolder.mAttachments.Count - 1; l >= 0; l--)
			{
				if (this.mAttachmentHolder.mAttachments[l] == null || this.mAttachmentHolder.mAttachments[l].mDead)
				{
					this.mAttachmentHolder.mAttachments[l].PrepareForReuse();
					this.mAttachmentHolder.mAttachments.RemoveAt(l);
				}
			}
		}

		public void Update()
		{
			foreach (TodParticleSystem todParticleSystem in this.mParticleHolder.mParticleSystems)
			{
				if (!todParticleSystem.mIsAttachment)
				{
					todParticleSystem.Update();
				}
			}
			foreach (Reanimation reanimation in this.mReanimationHolder.mReanimations)
			{
				if (reanimation != null && !reanimation.mIsAttachment)
				{
					reanimation.Update();
				}
			}
			foreach (Trail trail in this.mTrailHolder.mTrails)
			{
				if (!trail.mIsAttachment)
				{
					trail.Update();
				}
			}
		}

		public void SaveToFile(Sexy.Buffer b)
        {
			mParticleHolder.SaveToFile(b);
			//mTrailHolder.SaveToFile(b);
			//mReanimationHolder.SaveToFile(b);
			//mAttachmentHolder.SaveToFile(b);
		}

		public void LoadFromFile(Sexy.Buffer b)
		{
			mParticleHolder.LoadFromFile(b);
			//mTrailHolder.LoadFromFile(b);
			//mReanimationHolder.LoadFromFile(b);
			//mAttachmentHolder.LoadFromFile(b);
		}

		public static EffectSystem gEffectSystem;

		public TodParticleHolder mParticleHolder;

		public TrailHolder mTrailHolder;

		public ReanimationHolder mReanimationHolder;

		public AttachmentHolder mAttachmentHolder;
	}
}
