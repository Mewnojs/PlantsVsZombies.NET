using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class System
	{
		public static void KillParticlesFPSCallback(System s, int last_fps)
		{
			if (last_fps > s.mHighWatermark)
			{
				return;
			}
			int totalParticles = s.GetTotalParticles();
			int num = 0;
			Emitter nextEmitter;
			while ((nextEmitter = s.GetNextEmitter(ref num)) != null)
			{
				int num2 = nextEmitter.NumParticles();
				float num3 = (float)num2 / (float)totalParticles;
				int num4 = (int)((float)(s.mHighWatermark - last_fps) * ModVal.M(2f) * num3);
				if (last_fps < s.mLowWatermark)
				{
					num4 += (int)((float)num4 * (float)(s.mLowWatermark - last_fps) / (float)s.mLowWatermark);
				}
				List<Particle> list = new List<Particle>();
				s.GetOldestParticles(num4, ref list);
				for (int i = 0; i < list.size<Particle>(); i++)
				{
					if (list[i] != null)
					{
						list[i].mForceDeletion = true;
					}
				}
			}
		}

		public static void FadeParticlesFPSCallback(System s, int last_fps)
		{
			if (last_fps > s.mHighWatermark)
			{
				if (s.mMaxParticles > 0)
				{
					s.mMaxParticles++;
				}
				return;
			}
			int totalParticles = s.GetTotalParticles();
			if (s.mMaxParticles == -1)
			{
				s.mMaxParticles = totalParticles;
			}
			if (last_fps < (s.mLowWatermark + s.mHighWatermark) / 2)
			{
				s.mMaxParticles -= ((last_fps < s.mLowWatermark) ? 2 : 1);
			}
			int num = 0;
			Emitter nextEmitter;
			while ((nextEmitter = s.GetNextEmitter(ref num)) != null)
			{
				int num2 = nextEmitter.NumParticles();
				float num3 = (float)num2 / (float)totalParticles;
				float num4 = ModVal.M(10f);
				int num5 = (int)((float)(s.mHighWatermark - last_fps) * ModVal.M(2f) * num3);
				if (last_fps < s.mLowWatermark)
				{
					float num6 = (float)(s.mLowWatermark - last_fps) / (float)s.mLowWatermark;
					num5 += (int)((float)num5 * num6);
					num4 += num4 * num6;
				}
				List<Particle> list = new List<Particle>();
				s.GetOldestParticles(num5, ref list);
				for (int i = 0; i < list.size<Particle>(); i++)
				{
					if (list[i] != null)
					{
						list[i].mForceFadeoutRate = num4;
					}
				}
			}
		}

		protected void Clean()
		{
			for (int i = 0; i < this.mEmitters.size<EmitterUpdatePair>(); i++)
			{
				if (this.mEmitters[i].emitter != null)
				{
					this.mEmitters[i].emitter.Dispose();
				}
			}
			for (int j = 0; j < this.mForces.size<Force>(); j++)
			{
				if (this.mForces[j] != null)
				{
					this.mForces[j].Dispose();
				}
			}
			for (int k = 0; k < this.mDeflectors.size<Deflector>(); k++)
			{
				if (this.mDeflectors[k] != null)
				{
					this.mDeflectors[k].Dispose();
				}
			}
			for (int l = 0; l < this.mParticlePoolSize; l++)
			{
				if (this.mParticlePool[l] != null)
				{
					this.mParticlePool[l].Dispose();
				}
			}
			this.mEmitters.Clear();
			this.mForces.Clear();
			this.mDeflectors.Clear();
			this.mParticlePool.Clear();
			this.mParticlePool = null;
		}

		public System()
			: this(100, 200)
		{
		}

		public System(int particle_pool_size, int particle_pool_grow)
		{
			this.mUpdateCount = 0;
			this.mParticleScale2D = 1f;
			this.mWaitForEmitters = true;
			this.mLife = -1;
			this.mForceStopEmitting = false;
			this.mEmitterIteratorIndex = 0;
			this.mForceIteratorIndex = 0;
			this.mDeflectorIteratorIndex = 0;
			this.mParticlePoolGrowAmt = particle_pool_grow;
			this.mStartingParticlePoolSize = particle_pool_size;
			this.mParticlePoolSize = particle_pool_size;
			this.mParticlePoolIndex = 0;
			this.mLastX = 0f;
			this.mLastY = 0f;
			this.mMinSpawnFrame = -1;
			this.mAlphaPct = 1f;
			this.mScale = 1f;
			this.mMaxParticleCount = 0;
			this.mLowWatermark = -100;
			this.mHighWatermark = 100000;
			this.mFrameCount = 0;
			this.mFPSInterval = 100;
			this.mMaxParticles = -1;
			this.mLastEmitterHandle = 0;
			this.mParticlePool = new List<Particle>(this.mParticlePoolSize);
			for (int i = 0; i < particle_pool_size; i++)
			{
				Particle particle = new Particle();
				this.mParticlePool.Add(particle);
				particle.mPoolIndex = -1;
			}
		}

		public virtual void Dispose()
		{
			this.Clean();
		}

		public void ResetForReuse()
		{
			this.mUpdateCount = 0;
			this.mLife = -1;
			this.mForceStopEmitting = false;
			this.mEmitterIteratorIndex = 0;
			this.mForceIteratorIndex = 0;
			this.mDeflectorIteratorIndex = 0;
			this.mLastX = 0f;
			this.mLastY = 0f;
			this.mMinSpawnFrame = -1;
			this.mAlphaPct = 1f;
			this.mScale = 1f;
			this.mMaxParticleCount = 0;
			this.mFrameCount = 0;
			this.mMaxParticles = -1;
			this.mFPSTimer = new PerfTimer();
			for (int i = 0; i < this.mEmitters.Count; i++)
			{
				this.mEmitters[i].emitter.ResetForReuse();
				this.mEmitters[i].value = 0;
			}
			for (int j = 0; j < this.mDeflectors.Count; j++)
			{
				this.mDeflectors[j].ResetForReuse();
			}
			for (int k = 0; k < this.mForces.Count; k++)
			{
				this.mForces[k].ResetForReuse();
			}
		}

		public Particle AllocateParticle()
		{
			if (this.mParticlePoolIndex >= this.mParticlePoolSize)
			{
				this.mParticlePoolSize += this.mParticlePoolGrowAmt;
				for (int i = this.mParticlePoolIndex; i < this.mParticlePoolSize; i++)
				{
					this.mParticlePool.Add(new Particle());
				}
			}
			Particle particle = this.mParticlePool[this.mParticlePoolIndex];
			particle.mPoolIndex = this.mParticlePoolIndex++;
			if (this.mParticlePoolIndex > this.mMaxParticleCount)
			{
				this.mMaxParticleCount = this.mParticlePoolIndex;
			}
			return particle;
		}

		public void DeleteParticle(Particle p)
		{
			this.mParticlePool.Insert(this.mParticlePoolIndex - 1, p);
			p.mPoolIndex = this.mParticlePoolIndex - 1;
			this.mParticlePoolIndex--;
		}

		private void GetOldestParticles(int num, ref List<Particle> v)
		{
			if (num <= 0)
			{
				return;
			}
			List<Particle> list = new List<Particle>();
			for (int i = 0; i < this.mParticlePoolIndex; i++)
			{
				list.Add(this.mParticlePool[i]);
			}
			list.Sort(new SortOldestParticles());
			if (num > list.Count)
			{
				num = list.Count;
			}
			Particle[] array = new Particle[num];
			list.CopyTo(array);
			v.Clear();
			v.AddRange(array);
		}

		public int AddEmitter(Emitter e)
		{
			e.mSystem = this;
			e.Move(this.mLastX, this.mLastY);
			this.mEmitters.Add(new EmitterUpdatePair(e, 0));
			this.mLastEmitterHandle++;
			this.mEmitterHandleMap[this.mLastEmitterHandle] = e;
			e.mHandle = this.mLastEmitterHandle;
			e.mSerialIndex = this.mLastEmitterHandle;
			return this.mLastEmitterHandle;
		}

		public void DeleteEmitter(int handle)
		{
			if (!this.mEmitterHandleMap.ContainsKey(handle))
			{
				return;
			}
			for (int i = 0; i < this.mEmitters.size<EmitterUpdatePair>(); i++)
			{
				if (this.mEmitters[i].emitter.mHandle == handle)
				{
					this.mEmitters[i].emitter.Dispose();
					this.mEmitters[i].emitter = null;
					this.mEmitters.RemoveAt(i);
					return;
				}
			}
		}

		public void Update()
		{
			if (!this.mFPSTimer.IsRunning())
			{
				this.mFPSTimer.Start();
			}
			for (int i = 0; i < this.mForces.size<Force>(); i++)
			{
				this.mForces[i].Update(this.mUpdateCount);
				for (int j = 0; j < this.mEmitters.size<EmitterUpdatePair>(); j++)
				{
					this.mEmitters[j].emitter.ApplyForce(this.mForces[i]);
				}
			}
			for (int k = 0; k < this.mDeflectors.size<Deflector>(); k++)
			{
				this.mDeflectors[k].Update(this.mUpdateCount);
				for (int l = 0; l < this.mEmitters.size<EmitterUpdatePair>(); l++)
				{
					this.mEmitters[l].emitter.ApplyDeflector(this.mDeflectors[k]);
				}
			}
			for (int m = 0; m < this.mEmitters.size<EmitterUpdatePair>(); m++)
			{
				EmitterUpdatePair emitterUpdatePair = this.mEmitters[m];
				if (emitterUpdatePair.emitter.mPreloadFrames > 0)
				{
					for (int n = 0; n < emitterUpdatePair.emitter.mPreloadFrames; n++)
					{
						emitterUpdatePair.value++;
						bool allow_creation = (this.mLife < 0 && !this.mForceStopEmitting) || (emitterUpdatePair.value < this.mLife && !this.mForceStopEmitting);
						if (this.mUpdateCount < this.mMinSpawnFrame)
						{
							allow_creation = false;
						}
						emitterUpdatePair.emitter.Update(emitterUpdatePair.value, allow_creation);
					}
					emitterUpdatePair.emitter.mPreloadFrames = 0;
				}
			}
			int totalParticles = this.GetTotalParticles();
			this.mUpdateCount++;
			for (int num = 0; num < this.mEmitters.size<EmitterUpdatePair>(); num++)
			{
				this.mEmitters[num].value++;
				bool allow_creation2 = (this.mLife < 0 && !this.mForceStopEmitting) || (this.mEmitters[num].value + this.mEmitters[num].emitter.mStartFrame < this.mLife && !this.mForceStopEmitting);
				if (this.mUpdateCount < this.mMinSpawnFrame || (this.mMaxParticles > 0 && totalParticles > this.mMaxParticles))
				{
					allow_creation2 = false;
				}
				this.mEmitters[num].emitter.Update(this.mEmitters[num].value + this.mEmitters[num].emitter.mStartFrame, allow_creation2);
			}
			if (this.mFPSTimer.GetDuration() >= (double)this.mFPSInterval)
			{
				this.mFPSTimer.Stop();
				int last_fps = (int)((double)(this.mFrameCount * this.mFPSInterval) / this.mFPSTimer.GetDuration() + 0.5) * (1000 / this.mFPSInterval);
				if (this.mFPSCallback != null)
				{
					this.mFPSCallback(this, last_fps);
				}
				this.mFrameCount = 0;
				this.mFPSTimer.Start();
			}
		}

		public void Draw(Graphics g)
		{
			this.Draw(g, false, 0, 0);
		}

		public void Draw(Graphics g, bool draw_debug_info, int debugx, int debugy)
		{
			this.mFrameCount++;
			for (int i = 0; i < this.mEmitters.size<EmitterUpdatePair>(); i++)
			{
				this.mEmitters[i].emitter.Draw(g);
			}
		}

		public void SetLife(int life)
		{
			this.mLife = life;
		}

		public void WaitForEmitters(bool w)
		{
			this.mWaitForEmitters = w;
		}

		public bool Done()
		{
			if (this.mLife < 0 || this.mUpdateCount < this.mLife)
			{
				return false;
			}
			if (!this.mWaitForEmitters)
			{
				return true;
			}
			for (int i = 0; i < this.mEmitters.size<EmitterUpdatePair>(); i++)
			{
				if (this.mEmitters[i].emitter.NumParticles() - this.mEmitters[i].emitter.GetNumSingleParticles() > 0)
				{
					return false;
				}
			}
			return true;
		}

		public int GetUpdateCount()
		{
			return this.mUpdateCount;
		}

		public void ForceStopEmitting(bool f)
		{
			this.mForceStopEmitting = f;
			if (f)
			{
				this.mLife = 0;
			}
		}

		public void Move(float xamt, float yamt)
		{
			this.mLastX += xamt;
			this.mLastY += yamt;
			for (int i = 0; i < this.mEmitters.size<EmitterUpdatePair>(); i++)
			{
				this.mEmitters[i].emitter.Move(xamt, yamt);
			}
		}

		public void SetPos(float x, float y)
		{
			this.mLastX = x;
			this.mLastY = y;
			for (int i = 0; i < this.mEmitters.size<EmitterUpdatePair>(); i++)
			{
				this.mEmitters[i].emitter.SetPos(x, y);
			}
		}

		public void AddForce(Force f)
		{
			this.mForces.Add(f);
			f.mSystem = this;
		}

		public void AddDeflector(Deflector d)
		{
			this.mDeflectors.Add(d);
			d.mSystem = this;
			d.mSerialIndex = this.mDeflectors.size<Deflector>() - 1;
		}

		public void SetMinSpawnFrame(int f)
		{
			this.mMinSpawnFrame = f;
		}

		public Emitter GetEmitter(int emitter_handle)
		{
			if (this.mEmitterHandleMap.ContainsKey(emitter_handle))
			{
				return this.mEmitterHandleMap[emitter_handle];
			}
			return null;
		}

		public Emitter GetNextEmitter(ref int handle)
		{
			if (this.mEmitterIteratorIndex < this.mEmitters.size<EmitterUpdatePair>())
			{
				handle = this.mEmitterIteratorIndex;
				return this.mEmitters[this.mEmitterIteratorIndex++].emitter;
			}
			this.mEmitterIteratorIndex = 0;
			return null;
		}

		public Force GetNextForce()
		{
			if (this.mForceIteratorIndex < this.mForces.size<Force>())
			{
				return this.mForces[this.mForceIteratorIndex++];
			}
			this.mForceIteratorIndex = 0;
			return null;
		}

		public Deflector GetNextDeflector()
		{
			if (this.mDeflectorIteratorIndex < this.mDeflectors.size<Deflector>())
			{
				return this.mDeflectors[this.mDeflectorIteratorIndex++];
			}
			this.mDeflectorIteratorIndex = 0;
			return null;
		}

		public int GetTotalParticles()
		{
			int num = 0;
			for (int i = 0; i < this.mEmitters.size<EmitterUpdatePair>(); i++)
			{
				num += this.mEmitters[i].emitter.NumParticles();
			}
			return num;
		}

		public float GetLastX()
		{
			return this.mLastX;
		}

		public float GetLastY()
		{
			return this.mLastY;
		}

		public int GetMaxParticleCount()
		{
			return this.mMaxParticleCount;
		}

		public void ResetMaxParticleCount()
		{
			this.mMaxParticleCount = 0;
		}

		public bool LoadPINFile(string file_name, List<string> image_names)
		{
			SexyBuffer buffer = new SexyBuffer();
			if (!GlobalMembers.gSexyAppBase.ReadBufferFromFile(file_name, ref buffer))
			{
				return false;
			}
			long num = -1L;
			Emitter emitter = null;
			Emitter emitter2 = null;
			Dictionary<float, LifetimeSettings> dictionary = new Dictionary<float, LifetimeSettings>();
			while (!buffer.AtEnd())
			{
				dictionary.Clear();
				if (num == -1L)
				{
					num = buffer.ReadLong();
					if (num != (long)PINCommon.SECTION_EMITTER && num != (long)PINCommon.SECTION_FREE_EMITTER && num != (long)PINCommon.SECTION_PARTICLE_TYPE)
					{
						return false;
					}
				}
				else if (num == (long)PINCommon.SECTION_EMITTER)
				{
					Dictionary<int, EmitterSettings> dictionary2 = new Dictionary<int, EmitterSettings>();
					Dictionary<int, EmitterScale> dictionary3 = new Dictionary<int, EmitterScale>();
					string mName = buffer.ReadStringWithEncoding().Trim();
					bool flag = buffer.ReadBoolean();
					Emitter emitter3;
					if (flag)
					{
						emitter2 = new Emitter();
						emitter3 = emitter2;
					}
					else
					{
						emitter = new Emitter();
						emitter3 = emitter;
					}
					emitter3.mName = mName;
					long num2 = buffer.ReadLong();
					emitter3.SetEmitterType((int)num2);
					buffer.ReadBoolean();
					int theRed = buffer.ReadInt32();
					int theGreen = buffer.ReadInt32();
					int theBlue = buffer.ReadInt32();
					emitter3.mTintColor = new SexyColor(theRed, theGreen, theBlue);
					bool flag2 = false;
					while (!flag2 && !buffer.AtEnd())
					{
						int num3 = buffer.ReadInt32();
						if ((long)num3 != (long)((ulong)PINCommon.KEYFRAME_ENTRY_END))
						{
							if (num3 == PINCommon.SECTION_END)
							{
								break;
							}
							int num4 = Common.StrToInt(buffer.ReadStringWithEncoding());
							float num5 = Common.StrToFloat(buffer.ReadStringWithEncoding());
							buffer.ReadBoolean();
							buffer.ReadBoolean();
							Common.StrToFloat(buffer.ReadStringWithEncoding());
							Common.StrToFloat(buffer.ReadStringWithEncoding());
							Common.StrToFloat(buffer.ReadStringWithEncoding());
							Common.StrToFloat(buffer.ReadStringWithEncoding());
							if (num3 >= PINCommon.EM_FIRST_SCALE && num3 <= PINCommon.EM_LAST_SCALE)
							{
								EmitterScale emitterScale = new EmitterScale();
								switch (num3)
								{
								case 1:
									emitterScale.mLifeScale = num5 / 100f;
									break;
								case 2:
									emitterScale.mNumberScale = num5 / 100f;
									break;
								case 3:
									emitterScale.mSizeXScale = num5 / 100f;
									break;
								case 4:
									emitterScale.mSizeYScale = num5 / 100f;
									break;
								case 5:
									emitterScale.mVelocityScale = num5 / 100f;
									break;
								case 6:
									emitterScale.mWeightScale = num5 / 100f;
									break;
								case 7:
									emitterScale.mSpinScale = num5 / 100f;
									break;
								case 8:
									emitterScale.mMotionRandScale = num5 / 100f;
									break;
								case 9:
									emitterScale.mBounceScale = num5 / 100f;
									break;
								case 10:
									emitterScale.mZoom = num5 / 100f;
									break;
								}
								dictionary3[num4] = emitterScale;
							}
							else
							{
								EmitterSettings emitterSettings = new EmitterSettings();
								switch (num3)
								{
								case 11:
									emitterSettings.mVisibility = num5;
									break;
								case 12:
									emitterSettings.mTintStrength = num5;
									break;
								case 13:
									emitterSettings.mEmissionAngle = Common.DegreesToRadians(num5);
									break;
								case 14:
									emitterSettings.mEmissionRange = Common.DegreesToRadians(num5);
									break;
								case 15:
									emitterSettings.mActive = num5 != 0f;
									break;
								case 16:
									emitterSettings.mAngle = Common.DegreesToRadians(num5);
									break;
								case 17:
									emitterSettings.mXRadius = num5;
									break;
								case 18:
									emitterSettings.mYRadius = num5;
									break;
								}
								dictionary2[num4] = emitterSettings;
							}
						}
					}
					num = -1L;
					Dictionary<int, EmitterSettings>.Enumerator enumerator = dictionary2.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Emitter emitter4 = emitter3;
						KeyValuePair<int, EmitterSettings> keyValuePair = enumerator.Current;
						int key = keyValuePair.Key;
						keyValuePair = enumerator.Current;
						emitter4.AddSettingsKeyFrame(key, new EmitterSettings(keyValuePair.Value));
					}
					Dictionary<int, EmitterScale>.Enumerator enumerator2 = dictionary3.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						Emitter emitter5 = emitter3;
						KeyValuePair<int, EmitterScale> keyValuePair2 = enumerator2.Current;
						int key2 = keyValuePair2.Key;
						keyValuePair2 = enumerator2.Current;
						emitter5.AddScaleKeyFrame(key2, new EmitterScale(keyValuePair2.Value));
					}
				}
				else if (num == (long)PINCommon.SECTION_FREE_EMITTER)
				{
					FreeEmitter freeEmitter = new FreeEmitter();
					Dictionary<int, FreeEmitterSettings> dictionary4 = new Dictionary<int, FreeEmitterSettings>();
					Dictionary<int, FreeEmitterVariance> dictionary5 = new Dictionary<int, FreeEmitterVariance>();
					Dictionary<int, EmitterSettings> dictionary6 = new Dictionary<int, EmitterSettings>();
					Dictionary<int, EmitterScale> dictionary7 = new Dictionary<int, EmitterScale>();
					emitter = new Emitter();
					emitter.mName = buffer.ReadStringWithEncoding().Trim();
					buffer.ReadInt32();
					buffer.ReadBoolean();
					bool flag3 = false;
					while (!flag3 && !buffer.AtEnd())
					{
						int num6 = buffer.ReadInt32();
						if ((long)num6 != (long)((ulong)PINCommon.KEYFRAME_ENTRY_END))
						{
							if (num6 == PINCommon.SECTION_END)
							{
								break;
							}
							float num7 = Common.StrToFloat(buffer.ReadStringWithEncoding());
							float num8 = Common.StrToFloat(buffer.ReadStringWithEncoding());
							buffer.ReadBoolean();
							buffer.ReadBoolean();
							Common.StrToFloat(buffer.ReadStringWithEncoding());
							Common.StrToFloat(buffer.ReadStringWithEncoding());
							Common.StrToFloat(buffer.ReadStringWithEncoding());
							Common.StrToFloat(buffer.ReadStringWithEncoding());
							if (num6 >= PINCommon.FIRST_FE_SETTING && num6 <= PINCommon.LAST_FE_SETTING)
							{
								FreeEmitterSettings freeEmitterSettings = new FreeEmitterSettings();
								switch (num6)
								{
								case 3000:
									freeEmitterSettings.mLife = (int)num8;
									break;
								case 3001:
									freeEmitterSettings.mNumber = (int)num8;
									break;
								case 3002:
									freeEmitterSettings.mVelocity = (int)num8;
									break;
								case 3003:
									freeEmitterSettings.mWeight = (int)num8;
									break;
								case 3004:
									freeEmitterSettings.mSpin = Common.DegreesToRadians(num8);
									break;
								case 3005:
									freeEmitterSettings.mMotionRand = (int)num8;
									break;
								case 3006:
									freeEmitterSettings.mBounce = (int)num8;
									break;
								case 3007:
									freeEmitterSettings.mZoom = (int)num8;
									break;
								}
								dictionary4[(int)num7] = freeEmitterSettings;
							}
							else if (num6 >= PINCommon.FIRST_FE_VARIANCE && num6 <= PINCommon.LAST_FE_VARIANCE)
							{
								FreeEmitterVariance freeEmitterVariance = new FreeEmitterVariance();
								switch (num6)
								{
								case 3008:
									freeEmitterVariance.mLifeVar = (int)num8;
									break;
								case 3009:
									freeEmitterVariance.mSizeXVar = (int)num8;
									break;
								case 3010:
									freeEmitterVariance.mSizeYVar = (int)num8;
									break;
								case 3011:
									freeEmitterVariance.mVelocityVar = (int)num8;
									break;
								case 3012:
									freeEmitterVariance.mWeightVar = (int)num8;
									break;
								case 3013:
									freeEmitterVariance.mSpinVar = Common.DegreesToRadians(num8);
									break;
								case 3014:
									freeEmitterVariance.mMotionRandVar = (int)num8;
									break;
								case 3015:
									freeEmitterVariance.mBounceVar = (int)num8;
									break;
								case 3016:
									freeEmitterVariance.mZoomVar = (int)num8;
									break;
								}
								dictionary5[(int)num7] = freeEmitterVariance;
							}
							else if (num6 >= PINCommon.FIRST_FE_LIFE && num6 <= PINCommon.LAST_FE_LIFE)
							{
								LifetimeSettings lifetimeSettings = new LifetimeSettings();
								switch (num6)
								{
								case 3017:
									lifetimeSettings.mNumberMult = num8 / 100f;
									break;
								case 3018:
									lifetimeSettings.mSizeXMult = num8 / 100f;
									break;
								case 3019:
									lifetimeSettings.mSizeYMult = num8 / 100f;
									break;
								case 3020:
									lifetimeSettings.mVelocityMult = num8 / 100f;
									break;
								case 3021:
									lifetimeSettings.mWeightMult = num8 / 100f;
									break;
								case 3022:
									lifetimeSettings.mSpinMult = num8 / 100f;
									break;
								case 3023:
									lifetimeSettings.mMotionRandMult = num8 / 100f;
									break;
								case 3024:
									lifetimeSettings.mBounceMult = num8 / 100f;
									break;
								case 3025:
									lifetimeSettings.mZoomMult = num8 / 100f;
									break;
								}
								dictionary[(float)((int)num7)] = lifetimeSettings;
							}
							else if (num6 >= PINCommon.FIRST_FE_EMITTER_SCALE && num6 <= PINCommon.LAST_FE_EMITTER_SCALE)
							{
								EmitterScale emitterScale2 = new EmitterScale();
								switch (num6)
								{
								case 3026:
									emitterScale2.mLifeScale = num8 / 100f;
									break;
								case 3027:
									emitterScale2.mNumberScale = num8 / 100f;
									break;
								case 3028:
									emitterScale2.mSizeXScale = num8 / 100f;
									break;
								case 3029:
									emitterScale2.mSizeYScale = num8 / 100f;
									break;
								case 3030:
									emitterScale2.mVelocityScale = num8 / 100f;
									break;
								case 3031:
									emitterScale2.mWeightScale = num8 / 100f;
									break;
								case 3032:
									emitterScale2.mSpinScale = num8 / 100f;
									break;
								case 3033:
									emitterScale2.mMotionRandScale = num8 / 100f;
									break;
								case 3034:
									emitterScale2.mBounceScale = num8 / 100f;
									break;
								case 3035:
									emitterScale2.mZoom = num8 / 100f;
									break;
								}
								dictionary7[(int)num7] = emitterScale2;
							}
							else
							{
								EmitterSettings emitterSettings2 = new EmitterSettings();
								switch (num6)
								{
								case 3036:
									emitterSettings2.mVisibility = num8;
									break;
								case 3038:
									emitterSettings2.mTintStrength = num8;
									break;
								case 3039:
									emitterSettings2.mEmissionAngle = num8;
									break;
								case 3040:
									emitterSettings2.mEmissionRange = num8;
									break;
								}
								dictionary6[(int)num7] = emitterSettings2;
							}
						}
					}
					num = -1L;
					Dictionary<int, EmitterSettings>.Enumerator enumerator3 = dictionary6.GetEnumerator();
					while (enumerator3.MoveNext())
					{
						Emitter emitter6 = emitter;
						KeyValuePair<int, EmitterSettings> keyValuePair = enumerator3.Current;
						int key3 = keyValuePair.Key;
						keyValuePair = enumerator3.Current;
						emitter6.AddSettingsKeyFrame(key3, new EmitterSettings(keyValuePair.Value));
					}
					Dictionary<int, EmitterScale>.Enumerator enumerator4 = dictionary7.GetEnumerator();
					while (enumerator4.MoveNext())
					{
						Emitter emitter7 = emitter;
						KeyValuePair<int, EmitterScale> keyValuePair2 = enumerator4.Current;
						int key4 = keyValuePair2.Key;
						keyValuePair2 = enumerator4.Current;
						emitter7.AddScaleKeyFrame(key4, new EmitterScale(keyValuePair2.Value));
					}
					freeEmitter.mEmitter = emitter;
					Dictionary<float, LifetimeSettings>.Enumerator enumerator5 = dictionary.GetEnumerator();
					while (enumerator5.MoveNext())
					{
						FreeEmitter freeEmitter2 = freeEmitter;
						KeyValuePair<float, LifetimeSettings> keyValuePair3 = enumerator5.Current;
						float key5 = keyValuePair3.Key;
						keyValuePair3 = enumerator5.Current;
						freeEmitter2.AddLifetimeKeyFrame(key5, new LifetimeSettings(keyValuePair3.Value));
					}
					Dictionary<int, FreeEmitterSettings>.Enumerator enumerator6 = dictionary4.GetEnumerator();
					while (enumerator6.MoveNext())
					{
						FreeEmitter freeEmitter3 = freeEmitter;
						KeyValuePair<int, FreeEmitterSettings> keyValuePair4 = enumerator6.Current;
						int key6 = keyValuePair4.Key;
						keyValuePair4 = enumerator6.Current;
						freeEmitter3.AddSettingsKeyFrame(key6, new FreeEmitterSettings(keyValuePair4.Value));
					}
					Dictionary<int, FreeEmitterVariance>.Enumerator enumerator7 = dictionary5.GetEnumerator();
					while (enumerator7.MoveNext())
					{
						FreeEmitter freeEmitter4 = freeEmitter;
						KeyValuePair<int, FreeEmitterVariance> keyValuePair5 = enumerator7.Current;
						int key7 = keyValuePair5.Key;
						keyValuePair5 = enumerator7.Current;
						freeEmitter4.AddVarianceKeyFrame(key7, new FreeEmitterVariance(keyValuePair5.Value));
					}
					emitter2.AddFreeEmitter(freeEmitter);
					emitter = emitter2.GetEmitter(emitter2.GetNumFreeEmitters() - 1);
				}
				else if (num == (long)PINCommon.SECTION_PARTICLE_TYPE)
				{
					ParticleType particleType = new ParticleType();
					particleType.mName = buffer.ReadStringWithEncoding().Trim();
					particleType.mColorKeyManager.SetColorMode(1);
					particleType.mAlphaKeyManager.SetColorMode(1);
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					Dictionary<int, ParticleSettings> dictionary8 = new Dictionary<int, ParticleSettings>();
					Dictionary<int, ParticleVariance> dictionary9 = new Dictionary<int, ParticleVariance>();
					bool flag8 = false;
					while (!flag8 && !buffer.AtEnd())
					{
						int num9 = buffer.ReadInt32();
						if ((long)num9 != (long)((ulong)PINCommon.KEYFRAME_ENTRY_END) && num9 != PINCommon.MISC_PT_PROP_GRADIENT_END)
						{
							if (num9 == PINCommon.SECTION_END)
							{
								break;
							}
							if (num9 == PINCommon.MISC_PT_PROP_REF_PT)
							{
								particleType.mRefXOff = Common.StrToInt(buffer.ReadStringWithEncoding());
								particleType.mRefYOff = Common.StrToInt(buffer.ReadStringWithEncoding());
							}
							else if (num9 == PINCommon.MISC_PT_PROP_SHAPE_NAME)
							{
								string text = buffer.ReadStringWithEncoding().Trim();
								if (image_names != null)
								{
									bool flag9 = false;
									for (int i = 0; i < image_names.size<string>(); i++)
									{
										if (Common.StrEquals(image_names[i], text))
										{
											flag9 = true;
											break;
										}
									}
									if (!flag9)
									{
										image_names.Add(text);
									}
								}
								particleType.mImageName = text;
								particleType.mImage = GlobalMembers.gSexyAppBase.mResourceManager.LoadImage(text).GetImage();
								if (particleType.mImage != null)
								{
									particleType.mImageSetByPINLoader = true;
								}
							}
							else if (num9 == PINCommon.MISC_PT_PROP_COLOR_GRADIENT)
							{
								float pct = Common.StrToFloat(buffer.ReadStringWithEncoding());
								int theRed2 = buffer.ReadInt32();
								int theGreen2 = buffer.ReadInt32();
								int theBlue2 = buffer.ReadInt32();
								particleType.mColorKeyManager.AddColorKey(pct, new SexyColor(theRed2, theGreen2, theBlue2));
							}
							else if (num9 == PINCommon.MISC_PT_PROP_ALPHA_GRADIENT)
							{
								float pct2 = Common.StrToFloat(buffer.ReadStringWithEncoding());
								int alpha = buffer.ReadInt32();
								particleType.mAlphaKeyManager.AddAlphaKey(pct2, alpha);
							}
							else if (num9 == 1000)
							{
								particleType.mLockSizeAspect = buffer.ReadBoolean();
							}
							else if (num9 == 1001)
							{
								particleType.mSingle = buffer.ReadBoolean();
							}
							else if (num9 == 1002)
							{
								particleType.mAdditive = buffer.ReadBoolean();
							}
							else if (num9 == 1003)
							{
								buffer.ReadBoolean();
							}
							else if (num9 == 1004)
							{
								flag5 = buffer.ReadBoolean();
							}
							else if (num9 == 1005)
							{
								if (flag5)
								{
									particleType.mAngleRange = Common.DegreesToRadians((float)buffer.ReadLong());
								}
								else
								{
									buffer.ReadInt32();
								}
							}
							else if (num9 == 1006)
							{
								if (flag5)
								{
									particleType.mInitAngle = Common.DegreesToRadians((float)buffer.ReadLong());
								}
								else
								{
									buffer.ReadInt32();
								}
							}
							else if (num9 == 1007)
							{
								buffer.ReadBoolean();
							}
							else if (num9 == 1008)
							{
								flag4 = buffer.ReadBoolean();
								particleType.mAlignAngleToMotion = flag4;
							}
							else if (num9 == 1010)
							{
								if (flag4)
								{
									particleType.mMotionAngleOffset = Common.DegreesToRadians((float)buffer.ReadLong());
								}
								else
								{
									buffer.ReadInt32();
								}
							}
							else if (num9 == 1009)
							{
								if (!flag4 && !flag5)
								{
									particleType.mInitAngle = Common.DegreesToRadians((float)buffer.ReadLong());
								}
								else
								{
									buffer.ReadInt32();
								}
							}
							else if (num9 == 1011)
							{
								buffer.ReadBoolean();
							}
							else if (num9 == 1012)
							{
								particleType.mEmitterAttachPct = Common.StrToFloat(buffer.ReadStringWithEncoding());
							}
							else if (num9 == 1013)
							{
								buffer.ReadInt32();
							}
							else if (num9 == 1014)
							{
								particleType.mFlipX = buffer.ReadBoolean();
							}
							else if (num9 == 1015)
							{
								particleType.mFlipY = buffer.ReadBoolean();
							}
							else if (num9 == 1016)
							{
								flag6 = buffer.ReadBoolean();
							}
							else if (num9 == 1019)
							{
								bool flag10 = buffer.ReadBoolean();
								if (flag6)
								{
									particleType.mColorKeyManager.SetColorMode(flag10 ? 3 : 2);
								}
							}
							else if (num9 == 1017)
							{
								bool flag11 = buffer.ReadBoolean();
								if (flag11)
								{
									particleType.mColorKeyManager.SetColorMode(4);
								}
							}
							else if (num9 == 1018)
							{
								bool mUpdateImagePosColor = buffer.ReadBoolean();
								particleType.mColorKeyManager.mUpdateImagePosColor = mUpdateImagePosColor;
							}
							else if (num9 == 1020)
							{
								bool flag12 = buffer.ReadBoolean();
								if (flag12)
								{
									particleType.mAlphaKeyManager.SetColorMode(4);
								}
							}
							else if (num9 == 1021)
							{
								particleType.mAlphaKeyManager.mUpdateImagePosColor = buffer.ReadBoolean();
							}
							else if (num9 == 1022)
							{
								flag7 = buffer.ReadBoolean();
							}
							else if (num9 == 1023)
							{
								if (flag7)
								{
									particleType.mNumSameColorKeyInRow = buffer.ReadInt32();
								}
								else
								{
									buffer.ReadInt32();
								}
							}
							else if (num9 == 1024)
							{
								int num10 = buffer.ReadInt32();
								if (num10 < 1)
								{
									num10 = 1;
								}
								particleType.mColorKeyManager.mGradientRepeat = num10;
							}
							else if (num9 == 1025)
							{
								int num11 = buffer.ReadInt32();
								if (num11 < 1)
								{
									num11 = 1;
								}
								particleType.mAlphaKeyManager.mGradientRepeat = num11;
							}
							else if (num9 == 2010 || num9 == 2011 || num9 == 2012)
							{
								buffer.ReadStringWithEncoding();
								buffer.ReadStringWithEncoding();
								buffer.ReadBoolean();
								buffer.ReadBoolean();
								buffer.ReadStringWithEncoding();
								buffer.ReadStringWithEncoding();
								buffer.ReadStringWithEncoding();
								buffer.ReadStringWithEncoding();
							}
							else if (num9 >= 2000 && num9 <= 2029)
							{
								float num12 = Common.StrToFloat(buffer.ReadStringWithEncoding());
								float num13 = Common.StrToFloat(buffer.ReadStringWithEncoding());
								buffer.ReadBoolean();
								buffer.ReadBoolean();
								Common.StrToFloat(buffer.ReadStringWithEncoding());
								Common.StrToFloat(buffer.ReadStringWithEncoding());
								Common.StrToFloat(buffer.ReadStringWithEncoding());
								Common.StrToFloat(buffer.ReadStringWithEncoding());
								if (num9 >= PINCommon.PT_FIRST_SETTING && num9 <= PINCommon.PT_LAST_SETTING)
								{
									ParticleSettings particleSettings = new ParticleSettings();
									switch (num9)
									{
									case 2000:
										particleSettings.mLife = (int)num13;
										break;
									case 2001:
										particleSettings.mNumber = (int)num13;
										break;
									case 2002:
										particleSettings.mXSize = (int)num13;
										break;
									case 2003:
										particleSettings.mYSize = (int)num13;
										break;
									case 2004:
										particleSettings.mVelocity = (int)num13;
										break;
									case 2005:
										particleSettings.mWeight = num13;
										break;
									case 2006:
										particleSettings.mSpin = Common.DegreesToRadians(num13);
										break;
									case 2007:
										particleSettings.mMotionRand = num13;
										break;
									case 2008:
										particleSettings.mBounce = (int)num13;
										break;
									case 2009:
										particleSettings.mGlobalVisibility = num13 / 100f;
										break;
									}
									dictionary8[(int)num12] = particleSettings;
								}
								else if (num9 >= PINCommon.PT_FIRST_VARIANCE && num9 <= PINCommon.PT_LAST_VARIANCE)
								{
									ParticleVariance particleVariance = new ParticleVariance();
									switch (num9)
									{
									case 2013:
										particleVariance.mLifeVar = (int)num13;
										break;
									case 2014:
										particleVariance.mNumberVar = (int)num13;
										break;
									case 2015:
										particleVariance.mSizeXVar = (int)num13;
										break;
									case 2016:
										particleVariance.mSizeYVar = (int)num13;
										break;
									case 2017:
										particleVariance.mVelocityVar = (int)num13;
										break;
									case 2018:
										particleVariance.mWeightVar = (int)num13;
										break;
									case 2019:
										particleVariance.mSpinVar = Common.DegreesToRadians(num13);
										break;
									case 2020:
										particleVariance.mMotionRandVar = num13;
										break;
									case 2021:
										particleVariance.mBounceVar = (int)num13;
										break;
									}
									dictionary9[(int)num12] = particleVariance;
								}
								else
								{
									LifetimeSettings lifetimeSettings2 = new LifetimeSettings();
									switch (num9)
									{
									case 2022:
										lifetimeSettings2.mSizeXMult = num13 / 100f;
										break;
									case 2023:
										lifetimeSettings2.mSizeYMult = num13 / 100f;
										break;
									case 2024:
										lifetimeSettings2.mVelocityMult = num13 / 100f;
										break;
									case 2025:
										lifetimeSettings2.mWeightMult = num13 / 100f;
										break;
									case 2026:
										lifetimeSettings2.mSpinMult = num13 / 100f;
										break;
									case 2027:
										lifetimeSettings2.mMotionRandMult = num13 / 100f;
										break;
									case 2028:
										lifetimeSettings2.mBounceMult = num13 / 100f;
										break;
									}
								}
							}
						}
					}
					Dictionary<int, ParticleSettings>.Enumerator enumerator8 = dictionary8.GetEnumerator();
					while (enumerator8.MoveNext())
					{
						ParticleType particleType2 = particleType;
						KeyValuePair<int, ParticleSettings> keyValuePair6 = enumerator8.Current;
						int key8 = keyValuePair6.Key;
						keyValuePair6 = enumerator8.Current;
						particleType2.AddSettingsKeyFrame(key8, new ParticleSettings(keyValuePair6.Value));
					}
					Dictionary<int, ParticleVariance>.Enumerator enumerator9 = dictionary9.GetEnumerator();
					while (enumerator9.MoveNext())
					{
						ParticleType particleType3 = particleType;
						KeyValuePair<int, ParticleVariance> keyValuePair7 = enumerator9.Current;
						int key9 = keyValuePair7.Key;
						keyValuePair7 = enumerator9.Current;
						particleType3.AddVarianceKeyFrame(key9, new ParticleVariance(keyValuePair7.Value));
					}
					Dictionary<float, LifetimeSettings>.Enumerator enumerator10 = dictionary.GetEnumerator();
					while (enumerator10.MoveNext())
					{
						ParticleType particleType4 = particleType;
						KeyValuePair<float, LifetimeSettings> keyValuePair3 = enumerator10.Current;
						float key10 = keyValuePair3.Key;
						keyValuePair3 = enumerator10.Current;
						particleType4.AddSettingAtLifePct(key10, new LifetimeSettings(keyValuePair3.Value));
					}
					emitter.AddParticleType(particleType);
				}
			}
			if (emitter2 != null)
			{
				this.AddEmitter(emitter2);
			}
			else
			{
				this.AddEmitter(emitter);
			}
			return true;
		}

		public void SetImageName(string image_name, Image img)
		{
			for (int i = 0; i < this.mEmitters.size<EmitterUpdatePair>(); i++)
			{
				Emitter emitter = this.mEmitters[i].emitter;
				for (int j = 0; j < emitter.mParticleTypeInfo.size<ParticleTypeInfo>(); j++)
				{
					ParticleType first = emitter.mParticleTypeInfo[j].first;
					if (Common.StrEquals(first.mImageName, image_name))
					{
						if (first.mImageSetByPINLoader)
						{
							first.mImage.Dispose();
							first.mImage = null;
						}
						first.mImage = img;
						return;
					}
				}
			}
		}

		public void Serialize(SexyBuffer b, GlobalMembers.GetIdByImageFunc f)
		{
			b.WriteLong(this.mParticlePoolGrowAmt);
			b.WriteLong(this.mStartingParticlePoolSize);
			b.WriteFloat(this.mParticleScale2D);
			b.WriteLong(this.mEmitterIteratorIndex);
			b.WriteLong(this.mForceIteratorIndex);
			b.WriteLong(this.mDeflectorIteratorIndex);
			b.WriteLong(this.mUpdateCount);
			b.WriteLong(this.mLife);
			b.WriteLong(this.mMinSpawnFrame);
			b.WriteLong(this.mMaxParticleCount);
			b.WriteFloat(this.mLastX);
			b.WriteFloat(this.mLastY);
			b.WriteBoolean(this.mWaitForEmitters);
			b.WriteBoolean(this.mForceStopEmitting);
			b.WriteFloat(this.mAlphaPct);
			b.WriteFloat(this.mScale);
			b.WriteLong(this.mLastEmitterHandle);
			b.WriteLong(this.mForces.Count);
			for (int i = 0; i < this.mForces.Count; i++)
			{
				this.mForces[i].Serialize(b);
			}
			b.WriteLong(this.mDeflectors.Count);
			for (int j = 0; j < this.mDeflectors.Count; j++)
			{
				this.mDeflectors[j].Serialize(b);
			}
			b.WriteLong(this.mEmitters.Count);
			for (int k = 0; k < this.mEmitters.Count; k++)
			{
				this.mEmitters[k].emitter.Serialize(b, f);
				b.WriteLong(this.mEmitters[k].value);
				bool flag = false;
				foreach (KeyValuePair<int, Emitter> keyValuePair in this.mEmitterHandleMap)
				{
					if (keyValuePair.Value.Equals(this.mEmitters[k].emitter))
					{
						flag = true;
						b.WriteLong(keyValuePair.Key);
						break;
					}
				}
				if (!flag)
				{
					b.WriteLong(-1L);
				}
			}
		}

		public static System Deserialize(SexyBuffer b, GlobalMembers.GetImageByIdFunc f)
		{
			int particle_pool_grow = (int)b.ReadLong();
			int particle_pool_size = (int)b.ReadLong();
			System system = new System(particle_pool_size, particle_pool_grow);
			system.mParticleScale2D = b.ReadFloat();
			system.mEmitterIteratorIndex = (int)b.ReadLong();
			system.mForceIteratorIndex = (int)b.ReadLong();
			system.mDeflectorIteratorIndex = (int)b.ReadLong();
			system.mUpdateCount = (int)b.ReadLong();
			system.mLife = (int)b.ReadLong();
			system.mMinSpawnFrame = (int)b.ReadLong();
			system.mMaxParticleCount = (int)b.ReadLong();
			system.mLastX = b.ReadFloat();
			system.mLastY = b.ReadFloat();
			system.mWaitForEmitters = b.ReadBoolean();
			system.mForceStopEmitting = b.ReadBoolean();
			system.mAlphaPct = b.ReadFloat();
			system.mScale = b.ReadFloat();
			system.mLastEmitterHandle = (int)b.ReadLong();
			int num = (int)b.ReadLong();
			for (int i = 0; i < num; i++)
			{
				Force force = new Force();
				force.Deserialize(b);
				system.mForces.Add(force);
			}
			num = (int)b.ReadLong();
			Dictionary<int, Deflector> dictionary = new Dictionary<int, Deflector>();
			for (int j = 0; j < num; j++)
			{
				Deflector deflector = new Deflector();
				deflector.Deserialize(b);
				dictionary.Add(deflector.mSerialIndex, deflector);
				system.mDeflectors.Add(deflector);
			}
			num = (int)b.ReadLong();
			Dictionary<int, FreeEmitter> fe_ptr_map = new Dictionary<int, FreeEmitter>();
			for (int k = 0; k < num; k++)
			{
				Emitter emitter = new Emitter();
				emitter.mSystem = system;
				emitter.Deserialize(b, dictionary, fe_ptr_map, f);
				int value = (int)b.ReadLong();
				system.mEmitters.Add(new EmitterUpdatePair(emitter, value));
				int num2 = (int)b.ReadLong();
				if (num2 != -1)
				{
					system.mEmitterHandleMap.Add(num2, emitter);
				}
			}
			return system;
		}

		private int mEmitterIteratorIndex;

		private int mForceIteratorIndex;

		private int mDeflectorIteratorIndex;

		private int mParticlePoolSize;

		private int mParticlePoolIndex;

		private int mParticlePoolGrowAmt;

		private int mStartingParticlePoolSize;

		private int mLastEmitterHandle;

		private List<Particle> mParticlePool;

		protected PerfTimer mFPSTimer = new PerfTimer();

		protected int mFrameCount;

		protected List<EmitterUpdatePair> mEmitters = new List<EmitterUpdatePair>();

		protected List<Deflector> mDeflectors = new List<Deflector>();

		protected List<Force> mForces = new List<Force>();

		protected Dictionary<int, Emitter> mEmitterHandleMap = new Dictionary<int, Emitter>();

		protected int mUpdateCount;

		protected int mLife;

		protected int mMinSpawnFrame;

		protected int mMaxParticleCount;

		protected float mLastX;

		protected float mLastY;

		protected bool mWaitForEmitters;

		protected bool mForceStopEmitting;

		public float mAlphaPct;

		public float mScale;

		public float mParticleScale2D;

		public int mLowWatermark;

		public int mHighWatermark;

		public int mFPSInterval;

		public int mMaxParticles;

		public System.FPSCallback mFPSCallback;

		public delegate void FPSCallback(System s, int last_fps);
	}
}
