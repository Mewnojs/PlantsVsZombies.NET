using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class TodParticleHolder
	{
		public void Dispose()
		{
			this.DisposeHolder();
		}

		public void InitializeHolder()
		{
			this.mParticleSystems = new List<TodParticleSystem>(1024);
			this.mEmitters = new List<TodParticleEmitter>(1024);
			this.mParticles = new List<TodParticle>(1024);
		}

		public void DisposeHolder()
		{
			this.mParticleSystems.Clear();
			for (int i = 0; i < this.mEmitters.Count; i++)
			{
				this.mEmitters[i].PrepareForReuse();
			}
			this.mEmitters.Clear();
			for (int j = 0; j < this.mParticles.Count; j++)
			{
				this.mParticles[j].PrepareForReuse();
			}
			this.mParticles.Clear();
		}

		public TodParticleSystem AllocParticleSystemFromDef(float theX, float theY, int theRenderOrder, TodParticleDefinition theDefinition, ParticleEffect theParticleEffect)
		{
			if (this.mParticleSystems.Count == this.mParticleSystems.Capacity)
			{
				Debug.OutputDebug<string>("Too many particle systems\n");
				return null;
			}
			if (this.mEmitters.Count + theDefinition.mEmitterDefCount > this.mEmitters.Capacity)
			{
				Debug.OutputDebug<string>("Too many particle emitters\n");
				return null;
			}
			TodParticleSystem newTodParticleSystem = TodParticleSystem.GetNewTodParticleSystem();
			newTodParticleSystem.mActive = true;
			newTodParticleSystem.mParticleHolder = this;
			newTodParticleSystem.TodParticleInitializeFromDef(theX, theY, theRenderOrder, theDefinition, theParticleEffect);
			this.mParticleSystems.Add(newTodParticleSystem);
			return newTodParticleSystem;
		}

		public TodParticleSystem AllocParticleSystem(float theX, float theY, int theRenderOrder, ParticleEffect theParticleEffect)
		{
			TodParticleDefinition theDefinition = TodParticleGlobal.gParticleDefArray[(int)theParticleEffect];
			return this.AllocParticleSystemFromDef(theX, theY, theRenderOrder, theDefinition, theParticleEffect);
		}

		public bool IsOverLoaded()
		{
			return this.mParticleSystems.Count > 100 || this.mEmitters.Count > 100 || this.mParticles.Count > 100;
		}

		public List<TodParticleSystem> mParticleSystems = new List<TodParticleSystem>();

		public List<TodParticleEmitter> mEmitters = new List<TodParticleEmitter>();

		public List<TodParticle> mParticles = new List<TodParticle>();
	}
}
