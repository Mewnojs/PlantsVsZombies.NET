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

		public void SaveToFile(Sexy.Buffer b)
        {
			b.WriteLong(mParticleSystems.Count);
			foreach (TodParticleSystem aParticleSystem in mParticleSystems)
            {
				aParticleSystem.SaveToFile(b);
			}
			b.WriteLong(mEmitters.Count);
			foreach (TodParticleEmitter aEmitter in mEmitters)
            {
				aEmitter.SaveToFile(b);
			}
			b.WriteLong(mParticles.Count);
			foreach (TodParticle aParticles in mParticles)
            {
				aParticles.SaveToFile(b);
            }
        }

		public void LoadFromFile(Sexy.Buffer b)
        {
			int aParticleSystemCount = b.ReadLong();
			mParticleSystems.Clear();
			for (int i = 0; i < aParticleSystemCount; i++)
            {
				TodParticleSystem aParticleSystem = TodParticleSystem.GetNewTodParticleSystem();
				aParticleSystem.LoadFromFile(b);
				mParticleSystems.Add(aParticleSystem);
			}
			int aEmitterCount = b.ReadLong();
			mEmitters.Clear();
			for (int i = 0; i < aEmitterCount; i++)
            {
				TodParticleEmitter aEmitter = TodParticleEmitter.GetNewTodParticleEmitter();
				aEmitter.LoadFromFile(b);
				mEmitters.Add(aEmitter);
			}
			int aParticleCount = b.ReadLong();
			mParticles.Clear();
			for (int i = 0; i < aParticleCount; i++)
            {
				TodParticle aParticle = TodParticle.GetNewTodParticle();
				aParticle.LoadFromFile(b);
				mParticles.Add(aParticle);
			}
			foreach (TodParticleSystem aParticleSystem in mParticleSystems)
            {
				aParticleSystem.LoadingComplete();
			}
			foreach(TodParticleEmitter aEmitter in mEmitters)
            {
				aEmitter.LoadingComplete();
            }
			foreach(TodParticle aParticle in mParticles)
            {
				aParticle.LoadingComplete();
			}
		}

		public List<TodParticleSystem> mParticleSystems = new List<TodParticleSystem>();

		public List<TodParticleEmitter> mEmitters = new List<TodParticleEmitter>();

		public List<TodParticle> mParticles = new List<TodParticle>();
	}
}
