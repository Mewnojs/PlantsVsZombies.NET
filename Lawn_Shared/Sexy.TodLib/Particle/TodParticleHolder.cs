using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	public/*internal*/ class TodParticleHolder
	{
		public void Dispose()
		{
			DisposeHolder();
		}

		public void InitializeHolder()
		{
			mParticleSystems = new List<TodParticleSystem>(1024);
			mEmitters = new List<TodParticleEmitter>(1024);
			mParticles = new List<TodParticle>(1024);
		}

		public void DisposeHolder()
		{
			mParticleSystems.Clear();
			for (int i = 0; i < mEmitters.Count; i++)
			{
				mEmitters[i].PrepareForReuse();
			}
			mEmitters.Clear();
			for (int j = 0; j < mParticles.Count; j++)
			{
				mParticles[j].PrepareForReuse();
			}
			mParticles.Clear();
		}

		public TodParticleSystem AllocParticleSystemFromDef(float theX, float theY, int theRenderOrder, TodParticleDefinition theDefinition, ParticleEffect theParticleEffect)
		{
			if (mParticleSystems.Count == mParticleSystems.Capacity)
			{
				Debug.OutputDebug<string>("Too many particle systems\n");
				return null;
			}
			if (mEmitters.Count + theDefinition.mEmitterDefCount > mEmitters.Capacity)
			{
				Debug.OutputDebug<string>("Too many particle emitters\n");
				return null;
			}
			TodParticleSystem newTodParticleSystem = TodParticleSystem.GetNewTodParticleSystem();
			newTodParticleSystem.mActive = true;
			newTodParticleSystem.mParticleHolder = this;
			newTodParticleSystem.TodParticleInitializeFromDef(theX, theY, theRenderOrder, theDefinition, theParticleEffect);
			mParticleSystems.Add(newTodParticleSystem);
			return newTodParticleSystem;
		}

		public TodParticleSystem AllocParticleSystem(float theX, float theY, int theRenderOrder, ParticleEffect theParticleEffect)
		{
			TodParticleDefinition theDefinition = TodParticleGlobal.gParticleDefArray[(int)theParticleEffect];
			return AllocParticleSystemFromDef(theX, theY, theRenderOrder, theDefinition, theParticleEffect);
		}

		public bool IsOverLoaded()
		{
			return mParticleSystems.Count > 100 || mEmitters.Count > 100 || mParticles.Count > 100;
		}

		public List<TodParticleSystem> mParticleSystems = new List<TodParticleSystem>();

		public List<TodParticleEmitter> mEmitters = new List<TodParticleEmitter>();

		public List<TodParticle> mParticles = new List<TodParticle>();
	}
}
