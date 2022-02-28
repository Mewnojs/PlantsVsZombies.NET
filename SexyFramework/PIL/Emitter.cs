using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class Emitter : MovableObject
	{
		public void Serialize(SexyBuffer b, GlobalMembers.GetIdByImageFunc f)
		{
			base.Serialize(b);
			b.WriteLong((long)this.mHandle);
			b.WriteBoolean(this.mDisableQuadRep);
			b.WriteBoolean(this.mUseAlternateCalcMethod);
			b.WriteFloat(this.mNumSpawnAccumulator);
			b.WriteLong((long)this.mStartFrame);
			b.WriteBoolean(this.mParticlesMustHaveBeenVisible);
			b.WriteLong((long)this.mLineEmitterPoints.Count);
			for (int i = 0; i < this.mLineEmitterPoints.Count; i++)
			{
				this.mLineEmitterPoints[i].Serialize(b);
			}
			this.mScaleTimeLine.Serialize(b);
			this.mSettingsTimeLine.Serialize(b);
			b.WriteLong((long)this.mParticleTypeInfo.Count);
			for (int j = 0; j < this.mParticleTypeInfo.Count; j++)
			{
				this.mParticleTypeInfo[j].first.Serialize(b, f);
				b.WriteLong((long)this.mParticleTypeInfo[j].second);
			}
			b.WriteLong((long)this.mParticles.Count);
			for (int k = 0; k < this.mParticles.Count; k++)
			{
				this.mParticles[k].Serialize(b);
			}
			b.WriteLong((long)this.mFreeEmitterInfo.Count);
			for (int l = 0; l < this.mFreeEmitterInfo.Count; l++)
			{
				this.mFreeEmitterInfo[l].first.Serialize(b, f);
				b.WriteLong((long)this.mFreeEmitterInfo[l].second);
			}
			b.WriteLong((long)this.mEmitters.Count);
			for (int m = 0; m < this.mEmitters.Count; m++)
			{
				this.mEmitters[m].Serialize(b, f);
			}
			b.WriteLong((long)this.mLastPTFrameSetting.Count);
			foreach (KeyValuePair<int, ParticleSettings> keyValuePair in this.mLastPTFrameSetting)
			{
				b.WriteLong((long)keyValuePair.Key);
				keyValuePair.Value.Serialize(b);
			}
			b.WriteLong((long)this.mLastFEFrameSetting.Count);
			foreach (KeyValuePair<FreeEmitter, FreeEmitterSettings> keyValuePair2 in this.mLastFEFrameSetting)
			{
				b.WriteLong((long)keyValuePair2.Key.mSerialIndex);
				keyValuePair2.Value.Serialize(b);
			}
			this.mLastSettings.Serialize(b);
			this.mLastScale.Serialize(b);
			if (this.mAreaMask != null)
			{
				b.WriteLong((long)f(this.mAreaMask));
			}
			else
			{
				b.WriteLong(-1L);
			}
			b.WriteBoolean(this.mInvertAreaMask);
			b.WriteLong((long)this.mFrameOffset);
			b.WriteLong((long)this.mEmitterType);
			b.WriteLong((long)this.mLastEmitAtX);
			b.WriteLong((long)this.mLastEmitAtY);
			this.mWaypointManager.Serialize(b);
			b.WriteLong((long)this.mCullingRect.mX);
			b.WriteLong((long)this.mCullingRect.mY);
			b.WriteLong((long)this.mCullingRect.mWidth);
			b.WriteLong((long)this.mCullingRect.mHeight);
			b.WriteLong((long)this.mClipRect.mX);
			b.WriteLong((long)this.mClipRect.mY);
			b.WriteLong((long)this.mClipRect.mWidth);
			b.WriteLong((long)this.mClipRect.mHeight);
			b.WriteString(this.mName);
			b.WriteBoolean(this.mDrawNewestFirst);
			b.WriteBoolean(this.mWaitForParticles);
			b.WriteBoolean(this.mDeleteInvisParticles);
			b.WriteBoolean(this.mEmissionCoordsAreOffsets);
			b.WriteLong((long)this.mPreloadFrames);
			b.WriteLong((long)this.mEmitDir);
			b.WriteLong((long)this.mEmitAtXPoints);
			b.WriteLong((long)this.mEmitAtYPoints);
			b.WriteLong((long)this.mSerialIndex);
			b.WriteBoolean(this.mLinearEmitAtPoints);
			b.WriteLong((long)this.mTintColor.ToInt());
			if (this.mParentEmitter == null)
			{
				b.WriteLong(-1L);
			}
			else
			{
				b.WriteLong((long)this.mParentEmitter.mSerialIndex);
			}
			if (this.mSuperEmitter == null)
			{
				b.WriteLong(-1L);
				return;
			}
			b.WriteLong((long)this.mSuperEmitter.mSerialIndex);
		}

		public void Deserialize(SexyBuffer b, Dictionary<int, Deflector> deflector_ptr_map, Dictionary<int, FreeEmitter> fe_ptr_map, GlobalMembers.GetImageByIdFunc f)
		{
			base.Deserialize(b, deflector_ptr_map);
			this.Clear();
			this.mHandle = (int)b.ReadLong();
			this.mDisableQuadRep = b.ReadBoolean();
			this.mUseAlternateCalcMethod = b.ReadBoolean();
			this.mNumSpawnAccumulator = b.ReadFloat();
			this.mStartFrame = (int)b.ReadLong();
			this.mParticlesMustHaveBeenVisible = b.ReadBoolean();
			this.mSettingsTimeLine.mCurrentSettings = null;
			this.mScaleTimeLine.mCurrentSettings = null;
			this.Init();
			int num = (int)b.ReadLong();
			for (int i = 0; i < num; i++)
			{
				LineEmitterPoint lineEmitterPoint = new LineEmitterPoint();
				lineEmitterPoint.Deserialize(b);
				this.mLineEmitterPoints.Add(lineEmitterPoint);
			}
			this.mScaleTimeLine.Deserialize(b, new GlobalMembers.KFDInstantiateFunc(EmitterScale.Instantiate));
			this.mSettingsTimeLine.Deserialize(b, new GlobalMembers.KFDInstantiateFunc(EmitterSettings.Instantiate));
			num = (int)b.ReadLong();
			Dictionary<int, ParticleType> dictionary = new Dictionary<int, ParticleType>();
			for (int j = 0; j < num; j++)
			{
				ParticleType particleType = new ParticleType();
				particleType.Deserialize(b, f);
				dictionary.Add(particleType.mSerialIndex, particleType);
				int s = (int)b.ReadLong();
				this.mParticleTypeInfo.Add(new ParticleTypeInfo(particleType, s));
			}
			num = (int)b.ReadLong();
			for (int k = 0; k < num; k++)
			{
				Particle particle = this.mSystem.AllocateParticle();
				particle.Deserialize(b, deflector_ptr_map, dictionary);
				this.mParticles.Add(particle);
			}
			Dictionary<int, FreeEmitter> dictionary2 = new Dictionary<int, FreeEmitter>();
			num = (int)b.ReadLong();
			for (int l = 0; l < num; l++)
			{
				FreeEmitter freeEmitter = new FreeEmitter();
				freeEmitter.Deserialize(b, f);
				int s2 = (int)b.ReadLong();
				this.mFreeEmitterInfo.Add(new FreeEmitterInfo(freeEmitter, s2));
				dictionary2.Add(freeEmitter.mSerialIndex, freeEmitter);
			}
			num = (int)b.ReadLong();
			for (int m = 0; m < num; m++)
			{
				Emitter emitter = new Emitter();
				emitter.Deserialize(b, deflector_ptr_map, dictionary2, f);
				this.mEmitters.Add(emitter);
			}
			num = (int)b.ReadLong();
			for (int n = 0; n < num; n++)
			{
				int num2 = (int)b.ReadLong();
				ParticleSettings particleSettings = new ParticleSettings();
				particleSettings.Deserialize(b);
				if (dictionary.ContainsKey(num2))
				{
					ParticleType particleType2 = dictionary[num2];
					this.mLastPTFrameSetting.Add(num2, particleSettings);
				}
			}
			num = (int)b.ReadLong();
			for (int num3 = 0; num3 < num; num3++)
			{
				int num4 = (int)b.ReadLong();
				FreeEmitterSettings freeEmitterSettings = new FreeEmitterSettings();
				freeEmitterSettings.Deserialize(b);
				this.mLastFEFrameSetting.Add(this.mFreeEmitterInfo[num4].first, freeEmitterSettings);
			}
			this.mLastSettings.Deserialize(b);
			this.mLastScale.Deserialize(b);
			int num5 = (int)b.ReadLong();
			this.mInvertAreaMask = b.ReadBoolean();
			if (num5 != -1)
			{
				this.mAreaMask = (MemoryImage)f(num5);
				this.SetAreaMask(this.mAreaMask, this.mInvertAreaMask);
			}
			this.mFrameOffset = (int)b.ReadLong();
			this.mEmitterType = (int)b.ReadLong();
			this.mLastEmitAtX = (int)b.ReadLong();
			this.mLastEmitAtY = (int)b.ReadLong();
			this.mWaypointManager.Deserialize(b);
			this.mCullingRect.mX = (int)b.ReadLong();
			this.mCullingRect.mY = (int)b.ReadLong();
			this.mCullingRect.mWidth = (int)b.ReadLong();
			this.mCullingRect.mHeight = (int)b.ReadLong();
			this.mClipRect.mX = (int)b.ReadLong();
			this.mClipRect.mY = (int)b.ReadLong();
			this.mClipRect.mWidth = (int)b.ReadLong();
			this.mClipRect.mHeight = (int)b.ReadLong();
			this.mName = b.ReadString();
			this.mDrawNewestFirst = b.ReadBoolean();
			this.mWaitForParticles = b.ReadBoolean();
			this.mDeleteInvisParticles = b.ReadBoolean();
			this.mEmissionCoordsAreOffsets = b.ReadBoolean();
			this.mPreloadFrames = (int)b.ReadLong();
			this.mEmitDir = (int)b.ReadLong();
			this.mEmitAtXPoints = (int)b.ReadLong();
			this.mEmitAtYPoints = (int)b.ReadLong();
			this.mSerialIndex = (int)b.ReadLong();
			this.mLinearEmitAtPoints = b.ReadBoolean();
			int theColor = (int)b.ReadLong();
			this.mTintColor = new SexyColor(theColor);
			int num6 = (int)b.ReadLong();
			if (num6 != -1 && fe_ptr_map.ContainsKey(num6))
			{
				this.mParentEmitter = fe_ptr_map[num6];
			}
			this.mSuperEmitterIndex = (int)b.ReadLong();
		}

		protected void UpdateLineEmitter(int frame)
		{
			for (int i = 0; i < this.mLineEmitterPoints.size<LineEmitterPoint>(); i++)
			{
				LineEmitterPoint lineEmitterPoint = this.mLineEmitterPoints[i];
				PointKeyFrame pointKeyFrame = null;
				PointKeyFrame pointKeyFrame2 = null;
				for (int j = 0; j < lineEmitterPoint.mKeyFramePoints.size<PointKeyFrame>(); j++)
				{
					if (lineEmitterPoint.mKeyFramePoints[j].first > frame)
					{
						pointKeyFrame2 = lineEmitterPoint.mKeyFramePoints[j];
						break;
					}
					pointKeyFrame = lineEmitterPoint.mKeyFramePoints[j];
				}
				float num;
				if (pointKeyFrame2 == null)
				{
					num = 0f;
					pointKeyFrame2 = pointKeyFrame;
				}
				else
				{
					num = (float)(frame - pointKeyFrame.first) / (float)(pointKeyFrame2.first - pointKeyFrame.first);
				}
				lineEmitterPoint.mCurX = (float)pointKeyFrame.second.mX + (float)(pointKeyFrame2.second.mX - pointKeyFrame.second.mX) * num;
				lineEmitterPoint.mCurY = (float)pointKeyFrame.second.mY + (float)(pointKeyFrame2.second.mY - pointKeyFrame.second.mY) * num;
			}
		}

		protected int GetEmissionCoord(ref float x, ref float y, ref float angle)
		{
			int num = -1;
			if (this.mEmitterType == 0)
			{
				x = this.mX;
				y = this.mY;
				num = -1;
			}
			else if (this.mEmitterType == 1)
			{
				if (this.mEmitAtXPoints == 0 || this.mLineEmitterPoints.size<LineEmitterPoint>() == 1)
				{
					if (this.mLineEmitterPoints.size<LineEmitterPoint>() == 1)
					{
						x = this.mLineEmitterPoints[0].mCurX;
						y = this.mLineEmitterPoints[0].mCurY;
						num = 0;
					}
					else
					{
						int num2 = Common.Rand() % (this.mLineEmitterPoints.size<LineEmitterPoint>() - 1);
						this.GetXYFromLineIdx(num2, Common.FloatRange(0f, 1f), ref x, ref y);
						num = num2;
					}
				}
				else
				{
					int num3 = 0;
					List<int> list = new List<int>();
					for (int i = 1; i < this.mLineEmitterPoints.size<LineEmitterPoint>(); i++)
					{
						int num4 = (int)Common.Distance(this.mLineEmitterPoints[i].mCurX, this.mLineEmitterPoints[i].mCurY, this.mLineEmitterPoints[i - 1].mCurX, this.mLineEmitterPoints[i - 1].mCurY, false);
						list.Add(num4);
						num3 += num4;
					}
					float num5 = ((this.mEmitAtXPoints == 1) ? 0f : ((float)num3 / (float)(this.mEmitAtXPoints - 1)));
					int num6 = (this.mLinearEmitAtPoints ? (this.mLastEmitAtX++ % this.mEmitAtXPoints) : (Common.Rand() % this.mEmitAtXPoints));
					int num7 = (int)Math.Ceiling((double)((float)num6 * num5));
					int num8 = this.mLineEmitterPoints.size<LineEmitterPoint>() - 2;
					for (int j = 1; j < this.mLineEmitterPoints.size<LineEmitterPoint>(); j++)
					{
						if (num7 <= list[j - 1])
						{
							num8 = j - 1;
							break;
						}
						num7 -= list[j - 1];
					}
					float pct = (float)num7 / (float)list[num8];
					this.GetXYFromLineIdx(num8, pct, ref x, ref y);
					num = num8;
				}
			}
			else if (this.mEmitterType == 2)
			{
				if (this.mEmitAtXPoints == 0)
				{
					angle = -Common.DegreesToRadians((float)(Common.Rand() % 360));
				}
				else
				{
					int num9 = (this.mLinearEmitAtPoints ? (this.mLastEmitAtX++ % this.mEmitAtXPoints) : (Common.Rand() % this.mEmitAtXPoints));
					float num10 = 360f / (float)this.mEmitAtXPoints;
					angle = -Common.DegreesToRadians((float)num9 * num10);
				}
				float num11 = (float)Math.Sin((double)(-(double)this.mLastSettings.mAngle));
				float num12 = (float)Math.Cos((double)(-(double)this.mLastSettings.mAngle));
				float num13 = (float)Math.Sin((double)angle);
				float num14 = (float)Math.Cos((double)angle);
				x = this.mLastSettings.mXRadius * num14 * num12 - this.mLastSettings.mYRadius * num13 * num11;
				y = this.mLastSettings.mXRadius * num14 * num11 + this.mLastSettings.mYRadius * num13 * num12;
				angle *= -1f;
				num = 0;
			}
			else if (this.mEmitterType == 3)
			{
				if (this.mMaskPoints != null)
				{
					int num15 = Common.Rand() % this.mNumMaskPoints;
					x = (float)this.mMaskPoints[num15].mX + this.mX - (float)this.mDebugMaskImage.mWidth / 2f;
					y = (float)this.mMaskPoints[num15].mY + this.mY - (float)this.mDebugMaskImage.mHeight / 2f;
					Rect rect = new Rect((int)(this.mX - this.mLastSettings.mXRadius / 2f), (int)(this.mY - this.mLastSettings.mYRadius / 2f), (int)this.mLastSettings.mXRadius, (int)this.mLastSettings.mYRadius);
					if (!rect.Contains((int)x, (int)y))
					{
						num = -1;
					}
				}
				else if (this.mEmitAtXPoints == 0 || this.mEmitAtYPoints == 0)
				{
					x = this.mX - this.mLastSettings.mXRadius / 2f + (float)(Common.Rand() % (int)this.mLastSettings.mXRadius);
					y = this.mY - this.mLastSettings.mYRadius / 2f + (float)(Common.Rand() % (int)this.mLastSettings.mYRadius);
					num = 0;
				}
				else
				{
					int num16 = (this.mLinearEmitAtPoints ? (this.mLastEmitAtX++ % this.mEmitAtXPoints) : (Common.Rand() % this.mEmitAtXPoints));
					int num17 = (this.mLinearEmitAtPoints ? (this.mLastEmitAtY++ % this.mEmitAtYPoints) : (Common.Rand() % this.mEmitAtYPoints));
					if (this.mEmitAtXPoints == 1)
					{
						x = this.mX;
					}
					else
					{
						float num18 = this.mLastSettings.mXRadius / (float)(this.mEmitAtXPoints - 1);
						x = this.mX - this.mLastSettings.mXRadius / 2f + (float)num16 * num18;
					}
					if (this.mEmitAtYPoints == 1)
					{
						y = this.mY;
					}
					else
					{
						float num19 = this.mLastSettings.mYRadius / (float)(this.mEmitAtYPoints - 1);
						y = this.mY - this.mLastSettings.mYRadius / 2f + (float)num17 * num19;
					}
					num = 0;
				}
				if (num != -1)
				{
					Common.RotatePoint(this.mLastSettings.mAngle, ref x, ref y, this.mX, this.mY);
				}
			}
			if (num != -1 && this.mEmitterType != 3 && (this.mParentEmitter != null || this.mEmissionCoordsAreOffsets))
			{
				x += this.mX;
				y += this.mY;
			}
			return num;
		}

		protected void GetXYFromLineIdx(int idx, float pct, ref float x, ref float y)
		{
			int num = (int)(this.mLineEmitterPoints[idx + 1].mCurX - this.mLineEmitterPoints[idx].mCurX);
			int num2 = (int)(this.mLineEmitterPoints[idx + 1].mCurY - this.mLineEmitterPoints[idx].mCurY);
			x = this.mLineEmitterPoints[idx].mCurX + (float)num * pct;
			y = this.mLineEmitterPoints[idx].mCurY + (float)num2 * pct;
		}

		protected void Clear()
		{
			if (this.mWaypointManager != null)
			{
				this.mWaypointManager.Dispose();
			}
			for (int i = 0; i < this.mParticles.size<Particle>(); i++)
			{
				this.mSystem.DeleteParticle(this.mParticles[i]);
			}
			for (int j = 0; j < this.mEmitters.size<Emitter>(); j++)
			{
				if (this.mEmitters[j] != null)
				{
					this.mEmitters[j].Dispose();
				}
			}
			this.mParticles.Clear();
			this.mEmitters.Clear();
			this.mParticleTypeInfo.Clear();
			this.mFreeEmitterInfo.Clear();
			this.mLastFEFrameSetting.Clear();
			this.mLastFEFrameSetting.Clear();
			this.mWaypointManager = null;
		}

		protected void Init()
		{
			this.mSettingsTimeLine.mCurrentSettings = new EmitterSettings();
			this.mScaleTimeLine.mCurrentSettings = new EmitterScale();
			this.mLastSettings = (EmitterSettings)this.mSettingsTimeLine.mCurrentSettings;
			this.mLastScale = (EmitterScale)this.mScaleTimeLine.mCurrentSettings;
			this.mWaypointManager = new WaypointManager();
			this.mParticles.Capacity = 500;
			this.mEmitters.Capacity = 100;
		}

		protected void SpawnParticles(int frame)
		{
			for (int i = 0; i < this.mParticleTypeInfo.size<ParticleTypeInfo>(); i++)
			{
				ParticleTypeInfo particleTypeInfo = this.mParticleTypeInfo[i];
				ParticleType first = particleTypeInfo.first;
				if (!first.mSingle || first.mNumCreated <= 0)
				{
					int num = 0;
					float num2 = 0f;
					ParticleSettings particleSettings = null;
					ParticleVariance particleVariance = null;
					first.GetCreationParameters(frame, out num, out num2, out particleSettings, out particleVariance);
					this.mLastPTFrameSetting[first.mSerialIndex] = particleSettings;
					if ((float)(frame - particleTypeInfo.second) >= num2 || this.mUseAlternateCalcMethod)
					{
						particleTypeInfo.second = frame;
						int num3 = 0;
						if (!this.mUseAlternateCalcMethod)
						{
							num3 = (int)(Math.Ceiling((double)(1f / num2)) * (double)this.mLastScale.mNumberScale * (double)this.mCurrentLifetimeSettings.mNumberMult);
							if (!GlobalMembers.gSexyAppBase.Is3DAccelerated())
							{
								num3 = (int)((float)num3 * this.mSystem.mParticleScale2D);
							}
							if ((num3 <= 0 && this.mLastScale.mNumberScale > 0f && this.mCurrentLifetimeSettings.mNumberMult > 0f) || first.mSingle)
							{
								num3 = 1;
							}
						}
						else
						{
							float num4 = ((float)particleSettings.mNumber + Common.SAFE_RAND((float)particleVariance.mNumberVar)) * this.mLastScale.mNumberScale * this.mCurrentLifetimeSettings.mNumberMult / 6.6666665f;
							this.mNumSpawnAccumulator += num4 * (GlobalMembers.gSexyAppBase.Is3DAccelerated() ? 1f : this.mSystem.mParticleScale2D);
							if (this.mNumSpawnAccumulator >= 1f)
							{
								num3 = (int)this.mNumSpawnAccumulator;
								this.mNumSpawnAccumulator -= (float)num3;
							}
						}
						if (num != 0)
						{
							for (int j = 0; j < num3; j++)
							{
								first.mNumCreated++;
								num = first.GetRandomizedLife();
								float spawn_angle = 0f;
								float x = 0f;
								float y = 0f;
								if (this.GetLaunchSettings(ref spawn_angle, ref x, ref y))
								{
									float velocity = this.mLastScale.mVelocityScale * this.mLastScale.mZoom * ((float)particleSettings.mVelocity + Common.SAFE_RAND((float)particleVariance.mVelocityVar)) / 100f;
									Particle particle = this.mSystem.AllocateParticle();
									if (!first.mSingle)
									{
										particle.Reset(spawn_angle, velocity);
									}
									else
									{
										particle.Reset(0f, 0f);
									}
									particle.mColorKeyManager.CopyFrom(first.mColorKeyManager);
									particle.mAlphaKeyManager.CopyFrom(first.mAlphaKeyManager);
									particle.mParentType = first;
									particle.mLockSizeAspect = first.mLockSizeAspect;
									particle.mImage = first.mImage;
									particle.mImageRate = first.mImageRate;
									if (first.mRandomStartCel && first.mImage != null)
									{
										particle.mImageCel = ((particle.mImage.mNumCols > particle.mImage.mNumRows) ? (Common.Rand() % particle.mImage.mNumCols) : (Common.Rand() % particle.mImage.mNumRows));
									}
									particle.mAdditive = first.mAdditive;
									particle.mAdditiveWithNormal = first.mAdditiveWithNormal;
									if (num != -1)
									{
										particle.mLife = (first.mSingle ? (-1) : ((int)(this.mLastScale.mLifeScale * (float)num)));
									}
									else
									{
										particle.mLife = -1;
									}
									particle.mRefXOff = first.mRefXOff;
									particle.mRefYOff = first.mRefYOff;
									particle.mMotionAngleOffset = first.mMotionAngleOffset;
									particle.mAlignAngleToMotion = first.mAlignAngleToMotion;
									if (particle.mLife != -1)
									{
										particle.mColorKeyManager.SetLife(particle.mLife);
										particle.mAlphaKeyManager.SetLife(particle.mLife);
									}
									particle.mAngle = first.GetSpawnAngle();
									if (first.mNumSameColorKeyInRow > 0)
									{
										particle.mColorKeyManager.SetFixedColor(first.GetNextKeyColor());
									}
									particle.mMotionRand = (first.mSingle ? 0f : (this.mLastScale.mMotionRandScale * (particleSettings.mMotionRand + Common.FloatRange(0f, particleVariance.mMotionRandVar)) / 100f));
									particle.SetXY(x, y);
									particle.mParentName = first.mName;
									particle.mWeight = (first.mSingle ? 0f : (this.mLastScale.mWeightScale * (particleSettings.mWeight - Common.SAFE_RAND((float)(particleVariance.mWeightVar / 2)) + Common.SAFE_RAND((float)(particleVariance.mWeightVar / 2))) / ModVal.M(2000f)));
									particle.mSpin = this.mLastScale.mSpinScale * (particleSettings.mSpin - Common.FloatRange(0f, particleVariance.mSpinVar / 2f) + Common.FloatRange(0f, particleVariance.mSpinVar / 2f)) / 10f;
									particle.mBounce = this.mLastScale.mBounceScale * ((float)particleSettings.mBounce - Common.SAFE_RAND((float)(particleVariance.mBounceVar / 2)) + Common.SAFE_RAND((float)(particleVariance.mBounceVar / 2)));
									if (particle.mBounce < 0f)
									{
										particle.mBounce = 0f;
									}
									particle.mFlipX = first.mFlipX;
									particle.mFlipY = first.mFlipY;
									if (particle.mImage != null)
									{
										int num5 = (int)((float)particleSettings.mXSize * this.mSystem.mScale) + (int)Common.SAFE_RAND((float)particleVariance.mSizeXVar * this.mSystem.mScale);
										particle.mCurXSize = this.mLastScale.mSizeXScale * this.mLastScale.mZoom * ((float)num5 / (float)particle.mImage.GetCelWidth());
										if (!first.mLockSizeAspect)
										{
											num5 = (int)((float)particleSettings.mYSize * this.mSystem.mScale) + (int)Common.SAFE_RAND((float)particleVariance.mSizeYVar * this.mSystem.mScale);
										}
										particle.mCurYSize = this.mLastScale.mSizeYScale * this.mLastScale.mZoom * ((float)num5 / (float)particle.mImage.GetCelHeight());
									}
									for (int k = 0; k < first.mLifePctSettings.size<LifetimeSettingPct>(); k++)
									{
										particle.AddLifetimeKeyFrame(first.mLifePctSettings[k].first, new LifetimeSettings(first.mLifePctSettings[k].second));
									}
									this.mParticles.Add(particle);
								}
							}
						}
					}
				}
			}
		}

		protected void SpawnEmitters(int frame)
		{
			for (int i = 0; i < this.mFreeEmitterInfo.size<FreeEmitterInfo>(); i++)
			{
				FreeEmitterInfo freeEmitterInfo = this.mFreeEmitterInfo[i];
				FreeEmitter first = freeEmitterInfo.first;
				int num = 0;
				float num2 = 0f;
				FreeEmitterSettings freeEmitterSettings = null;
				FreeEmitterVariance freeEmitterVariance = null;
				first.GetCreationParams(frame, out num, out num2, out freeEmitterSettings, out freeEmitterVariance);
				float num3 = (float)freeEmitterSettings.mZoom / 100f;
				this.mLastFEFrameSetting[first] = freeEmitterSettings;
				if ((float)(frame - freeEmitterInfo.second) >= num2)
				{
					freeEmitterInfo.second = frame;
					int num4 = (int)(Math.Ceiling((double)(1f / num2)) * (double)this.mLastScale.mNumberScale * (double)this.mCurrentLifetimeSettings.mNumberMult);
					if (num4 <= 0 && this.mLastScale.mNumberScale > 0f && this.mCurrentLifetimeSettings.mNumberMult > 0f)
					{
						num4 = 1;
					}
					for (int j = 0; j < num4; j++)
					{
						num = first.GetRandomizedLife();
						float angle = 0f;
						float mX = 0f;
						float mY = 0f;
						if (this.GetLaunchSettings(ref angle, ref mX, ref mY))
						{
							float velocity = this.mLastScale.mVelocityScale * this.mLastScale.mZoom * num3 * ((float)freeEmitterSettings.mVelocity + Common.SAFE_RAND((float)freeEmitterVariance.mVelocityVar)) / 100f;
							Emitter emitter = new Emitter(first.mEmitter);
							emitter.mSystem = this.mSystem;
							this.mEmitters.Add(emitter);
							emitter.Launch(angle, velocity);
							emitter.mParentEmitter = first;
							emitter.mSuperEmitter = this;
							emitter.mLife = ((num == -1) ? (-1) : ((int)(this.mLastScale.mLifeScale * (float)num)));
							emitter.mX = mX;
							emitter.mY = mY;
							emitter.mMotionRand = this.mLastScale.mMotionRandScale * (float)(freeEmitterSettings.mMotionRand + Common.IntRange(0, freeEmitterVariance.mMotionRandVar)) / 100f;
							emitter.mWeight = this.mLastScale.mWeightScale * ((float)freeEmitterSettings.mWeight - Common.SAFE_RAND((float)(freeEmitterVariance.mWeightVar / 2)) + Common.SAFE_RAND((float)(freeEmitterVariance.mWeightVar / 2))) / ModVal.M(2000f);
							emitter.mSpin = this.mLastScale.mSpinScale * (freeEmitterSettings.mSpin - Common.FloatRange(0f, freeEmitterVariance.mSpinVar / 2f) + Common.FloatRange(0f, freeEmitterVariance.mSpinVar / 2f)) / 10f;
							emitter.mBounce = this.mLastScale.mBounceScale * ((float)freeEmitterSettings.mBounce - Common.SAFE_RAND((float)(freeEmitterVariance.mBounceVar / 2)) + Common.SAFE_RAND((float)freeEmitterVariance.mBounceVar));
							if (emitter.mBounce < 0f)
							{
								emitter.mBounce = 0f;
							}
							for (int k = 0; k < first.mLifePctSettings.size<LifetimeSettingPct>(); k++)
							{
								emitter.AddLifetimeKeyFrame(first.mLifePctSettings[k].second.mPct, new LifetimeSettings(first.mLifePctSettings[k].second));
							}
							for (int l = 0; l < emitter.mScaleTimeLine.mKeyFrames.size<KeyFrame>(); l++)
							{
								EmitterScale emitterScale = emitter.mScaleTimeLine.mKeyFrames[i].second as EmitterScale;
								emitterScale.mSizeXScale -= Common.SAFE_RAND((float)(freeEmitterVariance.mSizeXVar / 2)) / 100f;
								emitterScale.mSizeXScale += Common.SAFE_RAND((float)(freeEmitterVariance.mSizeXVar / 2)) / 100f;
								emitterScale.mSizeYScale -= Common.SAFE_RAND((float)((first.mAspectLocked ? freeEmitterVariance.mSizeXVar : freeEmitterVariance.mSizeYVar) / 2)) / 100f;
								emitterScale.mSizeYScale += Common.SAFE_RAND((float)((first.mAspectLocked ? freeEmitterVariance.mSizeXVar : freeEmitterVariance.mSizeYVar) / 2)) / 100f;
								emitterScale.mSizeXScale *= num3;
								emitterScale.mSizeYScale *= num3;
								if (emitterScale.mSizeXScale < 0f)
								{
									emitterScale.mSizeXScale = 0f;
								}
								if (emitterScale.mSizeYScale < 0f)
								{
									emitterScale.mSizeYScale = 0f;
								}
								emitterScale.mVelocityScale *= num3;
								emitterScale.mZoom -= Common.SAFE_RAND((float)(freeEmitterVariance.mZoomVar / 2)) / 100f;
								emitterScale.mZoom += Common.SAFE_RAND((float)(freeEmitterVariance.mZoomVar / 2)) / 100f;
								emitterScale.mZoom *= num3;
								if (emitterScale.mZoom < 0f)
								{
									emitterScale.mZoom = 0f;
								}
							}
						}
					}
				}
			}
		}

		protected bool GetLaunchSettings(ref float angle, ref float x, ref float y)
		{
			float num = Common.FloatRange(0f, this.mLastSettings.mEmissionRange / 2f);
			float num2 = Common.FloatRange(0f, this.mLastSettings.mEmissionRange / 2f);
			angle = this.mLastSettings.mEmissionAngle - num + num2;
			x = this.mX;
			y = this.mY;
			float num3 = 0f;
			int emissionCoord = this.GetEmissionCoord(ref x, ref y, ref num3);
			if (this.mEmitterType == 1 && emissionCoord >= 0 && this.mLineEmitterPoints.size<LineEmitterPoint>() > 1)
			{
				float mCurX = this.mLineEmitterPoints[emissionCoord].mCurX;
				float mCurY = this.mLineEmitterPoints[emissionCoord].mCurY;
				float mCurX2 = this.mLineEmitterPoints[emissionCoord + 1].mCurX;
				float mCurY2 = this.mLineEmitterPoints[emissionCoord + 1].mCurY;
				if (this.mEmitDir == 0 || (this.mEmitDir == 2 && Common.Rand() % 100 < 50))
				{
					angle += Common.AngleBetweenPoints(mCurX, mCurY, mCurX2, mCurY2);
				}
				else
				{
					angle = Common.AngleBetweenPoints(mCurX, mCurY, mCurX2, mCurY2) - angle;
				}
			}
			else if (this.mEmitterType == 2)
			{
				if (this.mEmitDir == 1 || (this.mEmitDir == 2 && Common.Rand() % 100 < 50))
				{
					num3 += Common.JL_PI / 2f;
				}
				else
				{
					num3 -= Common.JL_PI / 2f;
				}
				angle = num3 - angle;
			}
			else if (this.mEmitterType == 3 && emissionCoord == -1)
			{
				return false;
			}
			angle += this.mLastSettings.mAngle;
			return true;
		}

		public Emitter()
		{
			this.mPreloadFrames = 0;
			this.mDrawNewestFirst = false;
			this.mEmitterType = 0;
			this.mEmitAtXPoints = 0;
			this.mEmitAtYPoints = 0;
			this.mEmitDir = 2;
			this.mNumMaskPoints = 0;
			this.mMaskPoints = null;
			this.mDebugMaskImage = null;
			this.mLastSettings = null;
			this.mParentEmitter = null;
			this.mSuperEmitter = null;
			this.mFrameOffset = 0;
			this.mWaitForParticles = false;
			this.mSystem = null;
			this.mPoolIndex = -1;
			this.mEmissionCoordsAreOffsets = false;
			this.mLastEmitAtX = 0;
			this.mLastEmitAtY = 0;
			this.mDeleteInvisParticles = false;
			this.mParticlesMustHaveBeenVisible = false;
			this.mSerialIndex = -1;
			this.mAreaMask = null;
			this.mSuperEmitterIndex = -1;
			this.mHandle = -1;
			this.mDisableQuadRep = true;
			this.mUseAlternateCalcMethod = false;
			this.mNumSpawnAccumulator = 0f;
			this.mStartFrame = 0;
			this.Init();
		}

		public Emitter(Emitter rhs)
			: this()
		{
			this.CopyFrom(rhs);
		}

		public override void Dispose()
		{
			this.Clear();
			base.Dispose();
			this.mMaskPoints = null;
			this.mDebugMaskImage = null;
		}

		public void CopyFrom(Emitter rhs)
		{
			base.CopyFrom(rhs);
			this.mTintColor = rhs.mTintColor;
			this.mEmitAtXPoints = rhs.mEmitAtXPoints;
			this.mEmitAtYPoints = rhs.mEmitAtYPoints;
			this.mEmitDir = rhs.mEmitDir;
			this.mPreloadFrames = rhs.mPreloadFrames;
			this.mDrawNewestFirst = rhs.mDrawNewestFirst;
			this.mClipRect = rhs.mClipRect;
			this.mCullingRect = rhs.mCullingRect;
			this.mNumMaskPoints = rhs.mNumMaskPoints;
			this.mEmitterType = rhs.mEmitterType;
			this.mParentEmitter = rhs.mParentEmitter;
			this.mWaitForParticles = rhs.mWaitForParticles;
			this.mSystem = rhs.mSystem;
			this.mDisableQuadRep = rhs.mDisableQuadRep;
			this.mDeleteInvisParticles = rhs.mDeleteInvisParticles;
			this.mUseAlternateCalcMethod = rhs.mUseAlternateCalcMethod;
			this.mNumSpawnAccumulator = rhs.mNumSpawnAccumulator;
			this.mStartFrame = rhs.mStartFrame;
			this.mParticlesMustHaveBeenVisible = rhs.mParticlesMustHaveBeenVisible;
			this.mMaskPoints = null;
			this.mDebugMaskImage = null;
			this.mAreaMask = rhs.mAreaMask;
			this.mInvertAreaMask = rhs.mInvertAreaMask;
			this.mNumMaskPoints = rhs.mNumMaskPoints;
			if (this.mNumMaskPoints > 0)
			{
				this.mMaskPoints = new SexyPoint[this.mNumMaskPoints];
				for (int i = 0; i < this.mNumMaskPoints; i++)
				{
					this.mMaskPoints[i] = new SexyPoint(rhs.mMaskPoints[i]);
				}
				this.mDebugMaskImage = new MemoryImage(rhs.mDebugMaskImage);
			}
			this.mScaleTimeLine = rhs.mScaleTimeLine;
			this.mSettingsTimeLine = rhs.mSettingsTimeLine;
			this.mLastScale = (EmitterScale)this.mScaleTimeLine.mCurrentSettings;
			this.mLastSettings = (EmitterSettings)this.mSettingsTimeLine.mCurrentSettings;
			this.Clear();
			for (int j = 0; j < rhs.mParticleTypeInfo.size<ParticleTypeInfo>(); j++)
			{
				this.AddParticleType(new ParticleType(rhs.mParticleTypeInfo[j].first));
			}
			for (int k = 0; k < rhs.mFreeEmitterInfo.size<FreeEmitterInfo>(); k++)
			{
				this.AddFreeEmitter(new FreeEmitter(rhs.mFreeEmitterInfo[k].first));
			}
			this.mLineEmitterPoints = rhs.mLineEmitterPoints;
			this.mWaypointManager = new WaypointManager(rhs.mWaypointManager);
		}

		public void ResetForReuse()
		{
			this.mPoolIndex = -1;
			this.mLastEmitAtX = 0;
			this.mLastEmitAtY = 0;
			this.mNumSpawnAccumulator = 0f;
			for (int i = 0; i < this.mParticles.size<Particle>(); i++)
			{
				this.mSystem.DeleteParticle(this.mParticles[i]);
			}
			this.mParticles.Clear();
			for (int j = 0; j < this.mEmitters.Count; j++)
			{
				this.mEmitters[j].ResetForReuse();
			}
			for (int k = 0; k < this.mParticleTypeInfo.Count; k++)
			{
				this.mParticleTypeInfo[k].first.ResetForReuse();
				this.mParticleTypeInfo[k].second = 0;
			}
		}

		public EmitterScale AddScaleKeyFrame(int frame, EmitterScale scale, int second_frame_time, bool make_new)
		{
			this.mScaleTimeLine.AddKeyFrame(frame, scale);
			if (second_frame_time != -1)
			{
				this.mScaleTimeLine.AddKeyFrame(second_frame_time, new EmitterScale(scale));
				if (make_new)
				{
					return new EmitterScale(scale);
				}
			}
			return null;
		}

		public EmitterScale AddScaleKeyFrame(int frame, EmitterScale scale, int second_frame_time)
		{
			return this.AddScaleKeyFrame(frame, scale, second_frame_time, false);
		}

		public EmitterScale AddScaleKeyFrame(int frame, EmitterScale scale)
		{
			return this.AddScaleKeyFrame(frame, scale, -1, false);
		}

		public EmitterSettings AddSettingsKeyFrame(int frame, EmitterSettings settings, int second_frame_time, bool make_new)
		{
			this.mSettingsTimeLine.AddKeyFrame(frame, settings);
			if (second_frame_time != -1)
			{
				this.mSettingsTimeLine.AddKeyFrame(second_frame_time, new EmitterSettings(settings));
				if (make_new)
				{
					return new EmitterSettings(settings);
				}
			}
			return null;
		}

		public EmitterSettings AddSettingsKeyFrame(int frame, EmitterSettings settings, int second_frame_time)
		{
			return this.AddSettingsKeyFrame(frame, settings, second_frame_time, false);
		}

		public EmitterSettings AddSettingsKeyFrame(int frame, EmitterSettings settings)
		{
			return this.AddSettingsKeyFrame(frame, settings, -1, false);
		}

		public int AddParticleType(ParticleType pt)
		{
			if (pt.mImage != null)
			{
				if (this.mDisableQuadRep)
				{
					((MemoryImage)pt.mImage).AddImageFlags(128U);
				}
				else
				{
					((MemoryImage)pt.mImage).RemoveImageFlags(128U);
				}
			}
			if (pt.mSingle)
			{
				pt.mEmitterAttachPct = 1f;
			}
			if (pt.GetSettingsTimeLineSize() == 0)
			{
				pt.AddSettingsKeyFrame(0, new ParticleSettings());
			}
			if (pt.GetVarTimeLineSize() == 0)
			{
				pt.AddVarianceKeyFrame(0, new ParticleVariance());
			}
			if (pt.mColorKeyManager.GetColorMode() == 0 && pt.mColorKeyManager.GetNumKeys() > 0)
			{
				pt.mColorKeyManager.SetColorMode(1);
			}
			if (pt.mColorKeyManager.GetColorMode() == 1 && !pt.mColorKeyManager.HasMaxIndex())
			{
				pt.mColorKeyManager.AddColorKey(1f, pt.mColorKeyManager.GetColorByIndex(pt.mColorKeyManager.GetNumKeys() - 1));
			}
			if (pt.mAlphaKeyManager.GetColorMode() == 0 && pt.mAlphaKeyManager.GetNumKeys() > 0)
			{
				pt.mAlphaKeyManager.SetColorMode(1);
			}
			if (pt.mAlphaKeyManager.GetColorMode() == 1 && !pt.mAlphaKeyManager.HasMaxIndex())
			{
				pt.mAlphaKeyManager.AddColorKey(1f, pt.mAlphaKeyManager.GetColorByIndex(pt.mAlphaKeyManager.GetNumKeys() - 1));
			}
			this.mParticleTypeInfo.Add(new ParticleTypeInfo(pt, 0));
			pt.mSerialIndex = this.mParticleTypeInfo.size<ParticleTypeInfo>() - 1;
			this.mLastPTFrameSetting[pt.mSerialIndex] = new ParticleSettings();
			return this.mParticleTypeInfo.size<ParticleTypeInfo>() - 1;
		}

		public void AddFreeEmitter(FreeEmitter f)
		{
			this.mFreeEmitterInfo.Add(new FreeEmitterInfo(f, 0));
			this.mLastFEFrameSetting[f] = new FreeEmitterSettings();
			f.mSerialIndex = this.mFreeEmitterInfo.size<FreeEmitterInfo>() - 1;
		}

		public void Update(int frame, bool allow_creation)
		{
			if (this.mLife > 0 && this.mUpdateCount >= this.mLife)
			{
				allow_creation = false;
			}
			frame -= this.mFrameOffset;
			float mX = this.mX;
			float mY = this.mY;
			base.Update();
			float mX2 = this.mX;
			float mY2 = this.mY;
			if (this.Dead())
			{
				return;
			}
			if (this.mWaypointManager.GetNumPoints() > 0)
			{
				this.mWaypointManager.Update(frame);
				this.SetPos(this.mWaypointManager.GetLastPoint().X, this.mWaypointManager.GetLastPoint().Y);
			}
			else
			{
				this.mX = mX;
				this.mY = mY;
				this.Move(mX2 - mX, mY2 - mY);
			}
			this.mScaleTimeLine.Update(frame);
			this.mSettingsTimeLine.Update(frame);
			bool flag = this.mSettingsTimeLine.mKeyFrames.size<KeyFrame>() == 0 || frame >= this.mSettingsTimeLine.mKeyFrames.back<KeyFrame>().first;
			this.mLastScale.mSizeXScale *= this.mCurrentLifetimeSettings.mSizeXMult;
			this.mLastScale.mSizeYScale *= this.mCurrentLifetimeSettings.mSizeYMult;
			this.mLastScale.mZoom *= this.mCurrentLifetimeSettings.mZoomMult;
			if (!this.mLastSettings.mActive)
			{
				for (int i = 0; i < this.mParticles.size<Particle>(); i++)
				{
					this.mSystem.DeleteParticle(this.mParticles[i]);
				}
				this.mParticles.Clear();
			}
			if (this.mEmitterType == 1)
			{
				this.UpdateLineEmitter(frame);
			}
			if (allow_creation && this.mLastSettings.mActive)
			{
				this.SpawnEmitters(frame);
				this.SpawnParticles(frame);
			}
			for (int j = 0; j < this.mParticles.size<Particle>(); j++)
			{
				this.mParticles[j].Update();
				bool flag2 = flag && !this.mParticles[j].mLastFrameWasVisible;
				if (this.mParticles[j].Dead() || (this.mDeleteInvisParticles && (!this.mParticlesMustHaveBeenVisible || this.mParticles[j].mHasBeenVisible) && flag2) || (this.mCullingRect != Rect.ZERO_RECT && !this.mCullingRect.Intersects(this.mParticles[j].GetRect())))
				{
					this.mSystem.DeleteParticle(this.mParticles[j]);
					this.mParticles.RemoveAt(j);
					j--;
				}
			}
			for (int k = 0; k < this.mEmitters.size<Emitter>(); k++)
			{
				this.mEmitters[k].Update(frame, allow_creation);
				if (this.mEmitters[k].Dead())
				{
					this.mEmitters[k].Dispose();
					this.mEmitters.RemoveAt(k);
					k--;
				}
			}
		}

		public void Draw(Graphics g, float vis_mult, float tint_mult)
		{
			if (this.mClipRect != Rect.ZERO_RECT)
			{
				g.PushState();
				g.ClipRect(this.mClipRect);
			}
			int num = (this.mDrawNewestFirst ? (this.mParticles.size<Particle>() - 1) : 0);
			int num2 = num;
			while (this.mDrawNewestFirst ? (num2 >= 0) : (num2 < this.mParticles.size<Particle>()))
			{
				Particle particle = this.mParticles[num2];
				if (!particle.Dead() && (!particle.mAdditive || (particle.mAdditive && particle.mAdditiveWithNormal)))
				{
					float alpha_pct = this.mSystem.mAlphaPct * this.mLastPTFrameSetting[particle.mParentType.mSerialIndex].mGlobalVisibility * this.mLastSettings.mVisibility * vis_mult;
					particle.Draw(g, alpha_pct, this.mTintColor, this.mLastSettings.mTintStrength * tint_mult, this.mSystem.mScale);
				}
				num2 += (this.mDrawNewestFirst ? (-1) : 1);
			}
			int num3 = num;
			while (this.mDrawNewestFirst ? (num3 >= 0) : (num3 < this.mParticles.size<Particle>()))
			{
				Particle particle2 = this.mParticles[num3];
				if (!particle2.Dead() && particle2.mAdditive)
				{
					float alpha_pct2 = this.mSystem.mAlphaPct * this.mLastPTFrameSetting[particle2.mParentType.mSerialIndex].mGlobalVisibility * this.mLastSettings.mVisibility * vis_mult;
					particle2.Draw(g, alpha_pct2, this.mTintColor, this.mLastSettings.mTintStrength * tint_mult, this.mSystem.mScale);
				}
				num3 += (this.mDrawNewestFirst ? (-1) : 1);
			}
			num = (this.mDrawNewestFirst ? (this.mEmitters.size<Emitter>() - 1) : 0);
			int num4 = num;
			while (this.mDrawNewestFirst ? (num4 >= 0) : (num4 < this.mEmitters.size<Emitter>()))
			{
				Emitter emitter = this.mEmitters[num4];
				if (!emitter.Dead())
				{
					emitter.Draw(g, this.mLastSettings.mVisibility, this.mLastSettings.mTintStrength);
				}
				num4 += (this.mDrawNewestFirst ? (-1) : 1);
			}
			if (this.mClipRect != Rect.ZERO_RECT)
			{
				g.PopState();
			}
		}

		public void Draw(Graphics g, float vis_mult)
		{
			this.Draw(g, vis_mult, 1f);
		}

		public void Draw(Graphics g)
		{
			this.Draw(g, 1f, 1f);
		}

		public void Move(float xamt, float yamt)
		{
			if (this.mParentEmitter == null && this.mWaypointManager.GetNumPoints() > 0)
			{
				return;
			}
			this.mX += xamt;
			this.mY += yamt;
			for (int i = 0; i < this.mParticles.size<Particle>(); i++)
			{
				this.mParticles[i].SetX(this.mParticles[i].GetX() + xamt * this.mParticles[i].mParentType.mEmitterAttachPct);
				this.mParticles[i].SetY(this.mParticles[i].GetY() + yamt * this.mParticles[i].mParentType.mEmitterAttachPct);
			}
		}

		public void SetPos(float x, float y)
		{
			float mX = this.mX;
			float mY = this.mY;
			this.mX = x;
			this.mY = y;
			for (int i = 0; i < this.mParticles.size<Particle>(); i++)
			{
				Particle particle = this.mParticles[i];
				particle.SetX(particle.GetX() + (x - mX) * particle.mParentType.mEmitterAttachPct);
				particle.SetY(particle.GetY() + (y - mY) * particle.mParentType.mEmitterAttachPct);
			}
		}

		public void SetAreaMask(MemoryImage mask, bool invert)
		{
		}

		public void AddLineEmitterKeyFrame(int point_num, int frame, SexyPoint p)
		{
			if (point_num >= this.mLineEmitterPoints.size<LineEmitterPoint>())
			{
				this.mLineEmitterPoints.Resize(point_num + 1);
			}
			this.mLineEmitterPoints[point_num].mKeyFramePoints.Add(new PointKeyFrame(frame, p));
			this.mLineEmitterPoints[point_num].mKeyFramePoints.Sort(new SortPointKeyFrames());
		}

		public void DebugDraw(Graphics g, int size)
		{
		}

		public void ApplyForce(Force f)
		{
			for (int i = 0; i < this.mParticles.size<Particle>(); i++)
			{
				f.Apply(this.mParticles[i]);
			}
			for (int j = 0; j < this.mEmitters.size<Emitter>(); j++)
			{
				f.Apply(this.mEmitters[j]);
				this.mEmitters[j].ApplyForce(f);
			}
		}

		public void ApplyDeflector(Deflector d)
		{
			for (int i = 0; i < this.mParticles.size<Particle>(); i++)
			{
				d.Apply(this.mParticles[i]);
			}
			for (int j = 0; j < this.mEmitters.size<Emitter>(); j++)
			{
				d.Apply(this.mEmitters[j]);
				this.mEmitters[j].ApplyDeflector(d);
			}
		}

		public void GetParticlesOfType(int particle_type_handle, ref List<Particle> particles)
		{
			ParticleType particleType = this.GetParticleType(particle_type_handle);
			for (int i = 0; i < this.mParticles.size<Particle>(); i++)
			{
				if (this.mParticles[i].mParentType == particleType)
				{
					particles.Add(this.mParticles[i]);
				}
			}
		}

		public ParticleType GetParticleType(int particle_type_handle)
		{
			return this.mParticleTypeInfo[particle_type_handle].first;
		}

		public int GetNumParticleTypes()
		{
			return this.mParticleTypeInfo.size<ParticleTypeInfo>();
		}

		public ParticleType GetParticleTypeByIndex(int idx)
		{
			return this.mParticleTypeInfo[idx].first;
		}

		public int GetHandle()
		{
			return this.mHandle;
		}

		public int NumParticles()
		{
			int num = this.mParticles.size<Particle>();
			for (int i = 0; i < this.mEmitters.size<Emitter>(); i++)
			{
				num += this.mEmitters[i].NumParticles();
			}
			return num;
		}

		public void SetEmitterType(int t)
		{
			this.mEmitterType = t;
		}

		public void LoopSettingsTimeLine(bool l)
		{
			this.mSettingsTimeLine.mLoop = l;
		}

		public void LoopScaleTimeLine(bool l)
		{
			this.mScaleTimeLine.mLoop = l;
		}

		public override bool Dead()
		{
			bool flag = base.Dead();
			return (!this.mWaitForParticles || this.mParticles.size<Particle>() <= 0) && flag;
		}

		public virtual bool Active()
		{
			return this.mLastSettings != null && this.mLastSettings.mActive;
		}

		public void DisableQuadRep(bool val)
		{
			this.mDisableQuadRep = val;
			for (int i = 0; i < this.mParticleTypeInfo.size<ParticleTypeInfo>(); i++)
			{
				if (this.mParticleTypeInfo[i].first.mImage != null)
				{
					if (val)
					{
						((MemoryImage)this.mParticleTypeInfo[i].first.mImage).AddImageFlags(128U);
					}
					else
					{
						((MemoryImage)this.mParticleTypeInfo[i].first.mImage).RemoveImageFlags(128U);
					}
				}
			}
		}

		public override float GetX()
		{
			if (this.mWaypointManager.GetNumPoints() == 0 || this.mParentEmitter != null)
			{
				return base.GetX();
			}
			return this.mWaypointManager.GetLastPoint().X;
		}

		public override float GetY()
		{
			if (this.mWaypointManager.GetNumPoints() == 0 || this.mParentEmitter != null)
			{
				return base.GetY();
			}
			return this.mWaypointManager.GetLastPoint().Y;
		}

		public override bool CanInteract()
		{
			return (this.mWaypointManager.GetNumPoints() == 0 || this.mParentEmitter != null) && base.CanInteract();
		}

		public int GetNumFreeEmitters()
		{
			return this.mFreeEmitterInfo.size<FreeEmitterInfo>();
		}

		public int GetNumSingleParticles()
		{
			int num = 0;
			for (int i = 0; i < this.mParticles.size<Particle>(); i++)
			{
				if (this.mParticles[i].mParentType.mSingle)
				{
					num++;
				}
			}
			return num;
		}

		public Emitter GetEmitter(int idx)
		{
			return this.mFreeEmitterInfo[idx].first.mEmitter;
		}

		public System GetSystem()
		{
			return this.mSystem;
		}

		public int mSuperEmitterIndex;

		public int mPoolIndex;

		public MemoryImage mAreaMask;

		public bool mInvertAreaMask;

		public System mSystem;

		public List<LineEmitterPoint> mLineEmitterPoints = new List<LineEmitterPoint>();

		public TimeLine mScaleTimeLine = new TimeLine();

		public TimeLine mSettingsTimeLine = new TimeLine();

		public List<Particle> mParticles = new List<Particle>();

		public List<ParticleTypeInfo> mParticleTypeInfo = new List<ParticleTypeInfo>();

		public List<Emitter> mEmitters = new List<Emitter>();

		public List<FreeEmitterInfo> mFreeEmitterInfo = new List<FreeEmitterInfo>();

		public Dictionary<int, ParticleSettings> mLastPTFrameSetting = new Dictionary<int, ParticleSettings>();

		public Dictionary<FreeEmitter, FreeEmitterSettings> mLastFEFrameSetting = new Dictionary<FreeEmitter, FreeEmitterSettings>();

		public EmitterSettings mLastSettings;

		public EmitterScale mLastScale;

		public MemoryImage mDebugMaskImage;

		public SexyPoint[] mMaskPoints;

		public FreeEmitter mParentEmitter;

		public Emitter mSuperEmitter;

		public float mNumSpawnAccumulator;

		public bool mDisableQuadRep;

		public int mFrameOffset;

		public int mEmitterType;

		public int mNumMaskPoints;

		public int mLastEmitAtX;

		public int mLastEmitAtY;

		public int mHandle;

		public WaypointManager mWaypointManager;

		public Rect mCullingRect = default(Rect);

		public Rect mClipRect = default(Rect);

		public string mName = "";

		public bool mDrawNewestFirst;

		public bool mWaitForParticles;

		public bool mUseAlternateCalcMethod;

		public bool mDeleteInvisParticles;

		public bool mParticlesMustHaveBeenVisible;

		public bool mEmissionCoordsAreOffsets;

		public int mPreloadFrames;

		public int mStartFrame;

		public int mEmitDir;

		public int mEmitAtXPoints;

		public int mEmitAtYPoints;

		public int mSerialIndex;

		public bool mLinearEmitAtPoints;

		public SexyColor mTintColor = default(SexyColor);
	}
}
