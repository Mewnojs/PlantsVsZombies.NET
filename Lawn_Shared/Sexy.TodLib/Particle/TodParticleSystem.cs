using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class TodParticleSystem
	{
		public static TodParticleSystem GetNewTodParticleSystem()
		{
			TodParticleSystem result;
			if (TodParticleSystem.unusedObjects.Count > 0)
			{
				result = TodParticleSystem.unusedObjects.Pop();
			}
			else
			{
				result = new TodParticleSystem();
			}
			return result;
		}

		public void PrepareForReuse()
		{
			this.Reset();
			TodParticleSystem.unusedObjects.Push(this);
		}

		private TodParticleSystem()
		{
			this.Reset();
		}

		private void Reset()
		{
			this.mEffectType = ParticleEffect.PARTICLE_NONE;
			for (int i = 0; i < this.mEmitterList.Count; i++)
			{
				this.mEmitterList[i].PrepareForReuse();
			}
			this.mEmitterList.Clear();
			this.mParticleDef = null;
			this.mParticleHolder = null;
			this.mDead = false;
			this.mDontUpdate = false;
			this.mIsAttachment = false;
			this.mRenderOrder = 0;
			this.mActive = false;
		}

		public void Dispose()
		{
			this.ParticleSystemDie();
		}

		public void TodParticleInitializeFromDef(float theX, float theY, int theRenderOrder, TodParticleDefinition theDefinition, ParticleEffect theEffectType)
		{
			this.mParticleDef = theDefinition;
			this.mRenderOrder = theRenderOrder;
			this.mEffectType = theEffectType;
			for (int i = 0; i < theDefinition.mEmitterDefCount; i++)
			{
				TodEmitterDefinition todEmitterDefinition = theDefinition.mEmitterDefs[i];
				if (!Definition.FloatTrackIsSet(ref todEmitterDefinition.mCrossFadeDuration))
				{
					if (TodCommon.TestBit((uint)todEmitterDefinition.mParticleFlags, 7) && this.mParticleHolder.IsOverLoaded())
					{
						this.ParticleSystemDie();
						return;
					}
					TodParticleEmitter newTodParticleEmitter = TodParticleEmitter.GetNewTodParticleEmitter();
					newTodParticleEmitter.mActive = true;
					this.mParticleHolder.mEmitters.Add(newTodParticleEmitter);
					newTodParticleEmitter.TodEmitterInitialize(theX, theY, this, todEmitterDefinition);
					TodParticleEmitter todParticleEmitter = newTodParticleEmitter;
					this.mEmitterList.Add(todParticleEmitter);
				}
			}
		}

		public void ParticleSystemDie()
		{
			for (int i = 0; i < this.mEmitterList.Count; i++)
			{
				TodParticleEmitter todParticleEmitter = this.mEmitterList[i];
				todParticleEmitter.DeleteAll();
				todParticleEmitter.PrepareForReuse();
				this.mParticleHolder.mEmitters.Remove(todParticleEmitter);
			}
			this.mEmitterList.Clear();
			this.mDead = true;
		}

		public void Update()
		{
			if (this.mDontUpdate)
			{
				return;
			}
			bool flag = false;
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				todParticleEmitter2.Update();
				todParticleEmitter2.Update();
				todParticleEmitter2.Update();
				if (Definition.FloatTrackIsSet(ref todParticleEmitter2.mEmitterDef.mCrossFadeDuration))
				{
					if (todParticleEmitter2.mParticleList.Count > 0)
					{
						flag = true;
					}
				}
				else if (!todParticleEmitter2.mDead)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.mDead = true;
			}
		}

		public void Draw(Graphics g, bool doScale)
		{
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				todParticleEmitter.Draw(g, doScale);
			}
		}

		public void SystemMove(float theX, float theY)
		{
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				todParticleEmitter2.SystemMove(theX, theY);
			}
		}

		public void OverrideColor(string theEmitterName, SexyColor theColor)
		{
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				if (theEmitterName == null || string.Compare(theEmitterName, todParticleEmitter2.mEmitterDef.mName) == 0)
				{
					todParticleEmitter2.mColorOverride = theColor;
				}
			}
		}

		public void OverrideExtraAdditiveDraw(string theEmitterName, bool theEnableExtraAdditiveDraw)
		{
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				if (theEmitterName == null || string.Compare(theEmitterName, todParticleEmitter2.mEmitterDef.mName) == 0)
				{
					todParticleEmitter2.mExtraAdditiveDrawOverride = theEnableExtraAdditiveDraw;
				}
			}
		}

		public void OverrideImage(string theEmitterName, Image theImage)
		{
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				if (theEmitterName == null || string.Compare(theEmitterName, todParticleEmitter2.mEmitterDef.mName) == 0)
				{
					todParticleEmitter2.mImageOverride = theImage;
				}
			}
		}

		public void OverrideFrame(string theEmitterName, int theFrame)
		{
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				if (theEmitterName == null || string.Compare(theEmitterName, todParticleEmitter2.mEmitterDef.mName) == 0)
				{
					todParticleEmitter2.mFrameOverride = theFrame;
				}
			}
		}

		public void OverrideScale(string theEmitterName, float theScale)
		{
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				if (theEmitterName == null || string.Compare(theEmitterName, todParticleEmitter2.mEmitterDef.mName) == 0)
				{
					todParticleEmitter2.mScaleOverride = theScale;
				}
			}
		}

		public void CrossFade(string theCrossFadeEmitter)
		{
			TodEmitterDefinition todEmitterDefinition = this.FindEmitterDefByName(theCrossFadeEmitter);
			if (todEmitterDefinition == null)
			{
				Debug.OutputDebug<string>(Common.StrFormat_("Can't find cross fade emitter: {0}\n", theCrossFadeEmitter));
				return;
			}
			if (!Definition.FloatTrackIsSet(ref todEmitterDefinition.mCrossFadeDuration))
			{
				Debug.OutputDebug<string>(Common.StrFormat_("Can't cross fade without duration set: {0}\n", theCrossFadeEmitter));
				return;
			}
			if (this.mParticleHolder.mEmitters.Count + this.mEmitterList.Count > this.mParticleHolder.mEmitters.Capacity)
			{
				Debug.OutputDebug<string>("Too many emitters to cross fade\n");
				this.ParticleSystemDie();
				return;
			}
			for (int i = 0; i < this.mEmitterList.Count; i++)
			{
				TodParticleEmitter todParticleEmitter = this.mEmitterList[i];
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				if (todParticleEmitter2.mEmitterDef != todEmitterDefinition)
				{
					TodParticleEmitter newTodParticleEmitter = TodParticleEmitter.GetNewTodParticleEmitter();
					newTodParticleEmitter.TodEmitterInitialize(todParticleEmitter2.mSystemCenter.x, todParticleEmitter2.mSystemCenter.y, this, todEmitterDefinition);
					newTodParticleEmitter.mActive = true;
					this.mParticleHolder.mEmitters.Add(newTodParticleEmitter);
					TodParticleEmitter todParticleEmitter3 = newTodParticleEmitter;
					this.mEmitterList.Add(todParticleEmitter3);
					todParticleEmitter2.CrossFadeEmitter(newTodParticleEmitter);
				}
			}
		}

		public TodParticleEmitter FindEmitterByName(string theEmitterName)
		{
			foreach (TodParticleEmitter todParticleEmitter in this.mEmitterList)
			{
				TodParticleEmitter todParticleEmitter2 = todParticleEmitter;
				if (string.Compare(todParticleEmitter2.mEmitterDef.mName, theEmitterName) == 0)
				{
					return todParticleEmitter2;
				}
			}
			return null;
		}

		public TodEmitterDefinition FindEmitterDefByName(string theEmitterName)
		{
			for (int i = 0; i < this.mParticleDef.mEmitterDefCount; i++)
			{
				TodEmitterDefinition todEmitterDefinition = this.mParticleDef.mEmitterDefs[i];
				if (string.Compare(todEmitterDefinition.mName, theEmitterName) == 0)
				{
					return todEmitterDefinition;
				}
			}
			return null;
		}

		public ParticleEffect mEffectType = ParticleEffect.PARTICLE_NONE;

		public TodParticleDefinition mParticleDef;

		public TodParticleHolder mParticleHolder;

		public List<TodParticleEmitter> mEmitterList = new List<TodParticleEmitter>();

		public bool mDead;

		public bool mIsAttachment;

		public int mRenderOrder;

		public bool mDontUpdate;

		public bool mActive;

		private static Stack<TodParticleSystem> unusedObjects = new Stack<TodParticleSystem>(50);
	}
}
