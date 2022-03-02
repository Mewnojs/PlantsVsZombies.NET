using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sexy.Misc;

namespace Sexy.GraphicsLib
{
	public class PIEffect : IDisposable
	{
		public bool Fail(string theError)
		{
			if (this.mError.Length == 0)
			{
				this.mError = theError;
			}
			return false;
		}

		public void Deref()
		{
			this.mDef.mRefCount--;
			if (this.mDef.mRefCount <= 0)
			{
				if (this.mDef != null)
				{
					this.mDef.Dispose();
				}
				this.mDef = null;
			}
			if (this.mParticlePool != null)
			{
				this.mParticlePool.Dispose();
				this.mParticlePool = null;
			}
			if (this.mFreeEmitterPool != null)
			{
				this.mFreeEmitterPool.Dispose();
				this.mFreeEmitterPool = null;
			}
		}

		public float GetRandFloat()
		{
			return this.mRand.Next() % 20000000U / 10000000f - 1f;
		}

		public float GetRandFloatU()
		{
			return this.mRand.Next() % 10000000U / 10000000f;
		}

		public float GetRandSign()
		{
			if (this.mRand.Next() % 2U == 0U)
			{
				return 1f;
			}
			return -1f;
		}

		public float GetVariationScalar()
		{
			return this.GetRandFloat() * this.GetRandFloat();
		}

		public float GetVariationScalarU()
		{
			return this.GetRandFloatU() * this.GetRandFloatU();
		}

		public string ReadString()
		{
			int num = (int)this.mReadBuffer.ReadByte();
			string text = "";
			for (int i = 0; i < num; i++)
			{
				text += (char)this.mReadBuffer.ReadByte();
			}
			return text;
		}

		public string ReadStringS()
		{
			int num = (int)this.mReadBuffer.ReadShort();
			if (num == -1)
			{
				this.mReadBuffer.ReadShort();
				num = (int)this.mReadBuffer.ReadShort();
				return "";
			}
			if ((num & 32768) != 0)
			{
				string text = this.mStringVector[num & 32767];
				this.mStringVector.Add(text);
				return text;
			}
			string text2 = "";
			for (int i = 0; i < num; i++)
			{
				text2 += (char)this.mReadBuffer.ReadByte();
			}
			this.mStringVector.Add(text2);
			this.mStringVector.Add(text2);
			return text2;
		}

		public bool ExpectCmd(string theCmdExpected)
		{
			if (this.mIsPPF)
			{
				return true;
			}
			string text = this.ReadStringS();
			return !(text != theCmdExpected) || this.Fail("Expected '" + theCmdExpected + "'");
		}

		public void ReadValue2D(PIValue2D theValue2D)
		{
			int num = (int)this.mReadBuffer.ReadShort();
			List<float> list = this.mTimes;
			List<Vector2> list2 = this.mPoints;
			List<Vector2> list3 = this.mControlPoints;
			bool flag = false;
			if (this.mIsPPF && num > 1)
			{
				flag = this.mReadBuffer.ReadBoolean();
			}
			for (int i = 0; i < num; i++)
			{
				this.ExpectCmd("CKey");
				float num2 = (float)this.mReadBuffer.ReadInt32();
				list.Add(num2);
				Vector2 vector = default(Vector2);
				vector.X = this.mReadBuffer.ReadFloat();
				vector.Y = this.mReadBuffer.ReadFloat();
				list2.Add(vector);
				if (!this.mIsPPF || flag)
				{
					Vector2 vector2 = default(Vector2);
					vector2.X = this.mReadBuffer.ReadFloat();
					vector2.Y = this.mReadBuffer.ReadFloat();
					if (i > 0)
					{
						list3.Add(vector + vector2);
					}
					Vector2 vector3 = default(Vector2);
					vector3.X = this.mReadBuffer.ReadFloat();
					vector3.Y = this.mReadBuffer.ReadFloat();
					list3.Add(vector + vector3);
				}
				if (!this.mIsPPF)
				{
					this.mReadBuffer.ReadInt32();
					int num3 = this.mReadBuffer.ReadInt32();
					flag |= (num3 & 1) == 0;
				}
				PIValuePoint2D pivaluePoint2D = new PIValuePoint2D();
				pivaluePoint2D.mValue = vector;
				pivaluePoint2D.mTime = num2;
				theValue2D.mValuePoint2DVector.Add(pivaluePoint2D);
			}
			if (num > 1 && flag)
			{
				theValue2D.mBezier.Init(list2.ToArray(), list3.ToArray(), list.ToArray(), num);
			}
			list2.Clear();
			list3.Clear();
			list.Clear();
		}

		public void ReadEPoint(PIValue2D theValue2D)
		{
			int num = (int)this.mReadBuffer.ReadShort();
			for (int i = 0; i < num; i++)
			{
				this.ExpectCmd("CPointKey");
				PIValuePoint2D pivaluePoint2D = new PIValuePoint2D();
				pivaluePoint2D.mTime = (float)this.mReadBuffer.ReadInt32();
				pivaluePoint2D.mValue.X = this.mReadBuffer.ReadFloat();
				pivaluePoint2D.mValue.Y = this.mReadBuffer.ReadFloat();
				theValue2D.mValuePoint2DVector.Add(pivaluePoint2D);
			}
		}

		public void ReadValue(ref PIValue theValue)
		{
			List<float> list = this.mTimes;
			List<Vector2> list2 = this.mPoints;
			List<Vector2> list3 = this.mControlPoints;
			int num = (int)(this.mIsPPF ? this.mReadBuffer.ReadByte() : 0);
			int num2 = num & 7;
			if (!this.mIsPPF || num2 == 7)
			{
				num2 = (int)this.mReadBuffer.ReadShort();
			}
			bool flag = false;
			if (num2 > 1)
			{
				flag |= (num & 8) != 0;
			}
			theValue.mValuePointVector.Resize(num2);
			for (int i = 0; i < num2; i++)
			{
				bool flag2 = true;
				string text = "";
				if (!this.mIsPPF)
				{
					text = this.ReadStringS();
					flag2 = text == "CDataKey" || text == "CDataOverLifeKey";
				}
				if (flag2)
				{
					float num3;
					if ((num & 16) != 0 && i == 0)
					{
						num3 = 0f;
					}
					else if (text == "CDataKey")
					{
						num3 = (float)this.mReadBuffer.ReadInt32();
					}
					else
					{
						num3 = this.mReadBuffer.ReadFloat();
					}
					list.Add(num3);
					float y;
					if (i != 0 || (num & 96) == 0)
					{
						y = this.mReadBuffer.ReadFloat();
					}
					else if ((num & 96) == 32)
					{
						y = 0f;
					}
					else if ((num & 96) == 64)
					{
						y = 1f;
					}
					else
					{
						y = 2f;
					}
					Vector2 vector = default(Vector2);
					vector.X = num3;
					vector.Y = y;
					list2.Add(vector);
					if (!this.mIsPPF || flag)
					{
						Vector2 vector2 = default(Vector2);
						vector2.X = this.mReadBuffer.ReadFloat();
						vector2.Y = this.mReadBuffer.ReadFloat();
						if (i > 0)
						{
							list3.Add(vector + vector2);
						}
						Vector2 vector3 = default(Vector2);
						vector3.X = this.mReadBuffer.ReadFloat();
						vector3.Y = this.mReadBuffer.ReadFloat();
						list3.Add(vector + vector3);
					}
					if (!this.mIsPPF)
					{
						this.mReadBuffer.ReadInt32();
						int num4 = this.mReadBuffer.ReadInt32();
						flag |= (num4 & 1) == 0;
					}
					PIValuePoint pivaluePoint = theValue.mValuePointVector[i];
					pivaluePoint.mValue = vector.Y;
					pivaluePoint.mTime = num3;
				}
				else
				{
					this.Fail("CDataKey or CDataOverLifeKey expected");
				}
			}
			if (!flag && theValue.mValuePointVector.Count == 2 && theValue.mValuePointVector[0].mValue == theValue.mValuePointVector[1].mValue)
			{
				theValue.mValuePointVector.RemoveAt(theValue.mValuePointVector.Count - 1);
			}
			if (num2 > 1 && flag)
			{
				theValue.mBezier.Init(list2.ToArray(), list3.ToArray(), list.ToArray(), num2);
			}
			list.Clear();
			list2.Clear();
			list3.Clear();
		}

		public void ReadEmitterType(PIEmitter theEmitter)
		{
			this.mReadBuffer.ReadInt32();
			theEmitter.mName = this.ReadString();
			theEmitter.mKeepInOrder = this.mReadBuffer.ReadBoolean();
			this.mReadBuffer.ReadInt32();
			theEmitter.mOldestInFront = this.mReadBuffer.ReadBoolean();
			short num = this.mReadBuffer.ReadShort();
			for (int i = 0; i < (int)num; i++)
			{
				PIParticleDef piparticleDef = new PIParticleDef();
				this.ExpectCmd("CEmParticleType");
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadFloat();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				piparticleDef.mIntense = this.mReadBuffer.ReadBoolean();
				piparticleDef.mSingleParticle = this.mReadBuffer.ReadBoolean();
				piparticleDef.mPreserveColor = this.mReadBuffer.ReadBoolean();
				piparticleDef.mAttachToEmitter = this.mReadBuffer.ReadBoolean();
				piparticleDef.mAttachVal = this.mReadBuffer.ReadFloat();
				piparticleDef.mFlipHorz = this.mReadBuffer.ReadBoolean();
				piparticleDef.mFlipVert = this.mReadBuffer.ReadBoolean();
				piparticleDef.mAnimStartOnRandomFrame = this.mReadBuffer.ReadBoolean();
				piparticleDef.mRepeatColor = this.mReadBuffer.ReadInt32();
				piparticleDef.mRepeatAlpha = this.mReadBuffer.ReadInt32();
				piparticleDef.mLinkTransparencyToColor = this.mReadBuffer.ReadBoolean();
				piparticleDef.mName = this.ReadString();
				piparticleDef.mAngleAlignToMotion = this.mReadBuffer.ReadBoolean();
				piparticleDef.mAngleRandomAlign = this.mReadBuffer.ReadBoolean();
				piparticleDef.mAngleKeepAlignedToMotion = this.mReadBuffer.ReadBoolean();
				piparticleDef.mAngleValue = this.mReadBuffer.ReadInt32();
				piparticleDef.mAngleAlignOffset = this.mReadBuffer.ReadInt32();
				piparticleDef.mAnimSpeed = this.mReadBuffer.ReadInt32();
				piparticleDef.mRandomGradientColor = this.mReadBuffer.ReadBoolean();
				this.mReadBuffer.ReadInt32();
				piparticleDef.mTextureIdx = this.mReadBuffer.ReadInt32();
				int num2 = (int)this.mReadBuffer.ReadShort();
				for (int j = 0; j < num2; j++)
				{
					this.ExpectCmd("CColorPoint");
					byte b = this.mReadBuffer.ReadByte();
					byte b2 = this.mReadBuffer.ReadByte();
					byte b3 = this.mReadBuffer.ReadByte();
					ulong num3 = (ulong)(0xffffffffff000000) | ((ulong)b << 16) | ((ulong)b2 << 8) | (ulong)b3;
					float mTime = this.mReadBuffer.ReadFloat();
					PIInterpolatorPoint piinterpolatorPoint = new PIInterpolatorPoint();
					piinterpolatorPoint.mValue = (int)num3;
					piinterpolatorPoint.mTime = mTime;
					piparticleDef.mColor.mInterpolatorPointVector.Add(piinterpolatorPoint);
				}
				int num4 = (int)this.mReadBuffer.ReadShort();
				for (int k = 0; k < num4; k++)
				{
					this.ExpectCmd("CAlphaPoint");
					byte mValue = this.mReadBuffer.ReadByte();
					float mTime2 = this.mReadBuffer.ReadFloat();
					PIInterpolatorPoint piinterpolatorPoint2 = new PIInterpolatorPoint();
					piinterpolatorPoint2.mValue = (int)mValue;
					piinterpolatorPoint2.mTime = mTime2;
					piparticleDef.mAlpha.mInterpolatorPointVector.Add(piinterpolatorPoint2);
				}
				for (int l = 0; l < 23; l++)
				{
					this.ReadValue(ref piparticleDef.mValues[l]);
				}
				piparticleDef.mRefPointOfs.X = this.mReadBuffer.ReadFloat();
				piparticleDef.mRefPointOfs.Y = this.mReadBuffer.ReadFloat();
				if (!this.mIsPPF)
				{
					Image image = this.mDef.mTextureVector[piparticleDef.mTextureIdx].mImageVector[0].GetImage();
					PIParticleDef piparticleDef2 = piparticleDef;
					piparticleDef2.mRefPointOfs.X = piparticleDef2.mRefPointOfs.X / (float)image.mWidth;
					PIParticleDef piparticleDef3 = piparticleDef;
					piparticleDef3.mRefPointOfs.Y = piparticleDef3.mRefPointOfs.Y / (float)image.mHeight;
				}
				this.mReadBuffer.ReadInt32();
				this.mReadBuffer.ReadInt32();
				piparticleDef.mLockAspect = this.mReadBuffer.ReadBoolean();
				this.ReadValue(ref piparticleDef.mValues[25]);
				this.ReadValue(ref piparticleDef.mValues[26]);
				this.ReadValue(ref piparticleDef.mValues[27]);
				piparticleDef.mAngleRange = this.mReadBuffer.ReadInt32();
				piparticleDef.mAngleOffset = this.mReadBuffer.ReadInt32();
				piparticleDef.mGetColorFromLayer = this.mReadBuffer.ReadBoolean();
				piparticleDef.mUpdateColorFromLayer = this.mReadBuffer.ReadBoolean();
				piparticleDef.mUseEmitterAngleAndRange = this.mReadBuffer.ReadBoolean();
				this.ReadValue(ref piparticleDef.mValues[23]);
				this.ReadValue(ref piparticleDef.mValues[24]);
				this.mReadBuffer.ReadInt32();
				PIValue pivalue = new PIValue();
				this.ReadValue(ref pivalue);
				piparticleDef.mUseKeyColorsOnly = this.mReadBuffer.ReadBoolean();
				piparticleDef.mUpdateTransparencyFromLayer = this.mReadBuffer.ReadBoolean();
				piparticleDef.mUseNextColorKey = this.mReadBuffer.ReadBoolean();
				piparticleDef.mNumberOfEachColor = this.mReadBuffer.ReadInt32();
				piparticleDef.mGetTransparencyFromLayer = this.mReadBuffer.ReadBoolean();
				if (theEmitter.mOldestInFront)
				{
					theEmitter.mParticleDefVector.Insert(0, piparticleDef);
				}
				else
				{
					theEmitter.mParticleDefVector.Add(piparticleDef);
				}
			}
			this.mReadBuffer.ReadInt32();
			for (int m = 0; m < 42; m++)
			{
				this.ReadValue(ref theEmitter.mValues[m]);
			}
			theEmitter.mIsSuperEmitter = theEmitter.mValues[0].mValuePointVector.Count != 0;
			this.mReadBuffer.ReadInt32();
			this.mReadBuffer.ReadInt32();
		}

		public void WriteByte(sbyte theByte)
		{
			throw new NotImplementedException();
		}

		public void WriteInt(int theInt)
		{
			throw new NotImplementedException();
		}

		public void WriteShort(short theShort)
		{
			throw new NotImplementedException();
		}

		public void WriteFloat(float theFloat)
		{
			throw new NotImplementedException();
		}

		public void WriteBool(int theValue)
		{
			throw new NotImplementedException();
		}

		public void WriteString(string theString)
		{
			throw new NotImplementedException();
		}

		public void WriteValue2D(PIValue2D theValue2D)
		{
			throw new NotImplementedException();
		}

		public void WriteValue(PIValue theValue)
		{
			throw new NotImplementedException();
		}

		public void WriteEmitterType(PIEmitter theEmitter)
		{
			throw new NotImplementedException();
		}

		public void SaveParticleDefInstance(SexyBuffer theBuffer, PIParticleDefInstance theParticleDefInstance)
		{
			theBuffer.WriteFloat(theParticleDefInstance.mNumberAcc);
			theBuffer.WriteFloat(theParticleDefInstance.mCurNumberVariation);
			theBuffer.WriteLong(theParticleDefInstance.mParticlesEmitted);
			theBuffer.WriteLong(theParticleDefInstance.mTicks);
		}

		public void SaveParticle(SexyBuffer theBuffer, PILayer theLayer, PIParticleInstance theParticle)
		{
			theBuffer.WriteFloat(theParticle.mTicks);
			theBuffer.WriteFloat(theParticle.mLife);
			theBuffer.WriteFloat(theParticle.mLifePct);
			theBuffer.WriteFloat(theParticle.mZoom);
			theBuffer.WriteFPoint(theParticle.mPos);
			theBuffer.WriteFPoint(theParticle.mVel);
			theBuffer.WriteFPoint(theParticle.mEmittedPos);
			if (theParticle.mParticleDef != null && theParticle.mParticleDef.mAttachToEmitter)
			{
				theBuffer.WriteFPoint(theParticle.mOrigPos);
				theBuffer.WriteFloat(theParticle.mOrigEmitterAng);
			}
			theBuffer.WriteFloat(theParticle.mImgAngle);
			int num = 0;
			for (int i = 0; i < 9; i++)
			{
				if (Math.Abs(theParticle.mVariationValues[i]) >= 1E-05f)
				{
					num |= 1 << i;
				}
			}
			theBuffer.WriteShort((short)num);
			for (int j = 0; j < 9; j++)
			{
				if ((num & (1 << j)) != 0)
				{
					theBuffer.WriteFloat(theParticle.mVariationValues[j]);
				}
			}
			theBuffer.WriteFloat(theParticle.mSrcSizeXMult);
			theBuffer.WriteFloat(theParticle.mSrcSizeYMult);
			if (theParticle.mParticleDef != null && theParticle.mParticleDef.mRandomGradientColor)
			{
				theBuffer.WriteFloat(theParticle.mGradientRand);
			}
			if (theParticle.mParticleDef != null && theParticle.mParticleDef.mAnimStartOnRandomFrame)
			{
				theBuffer.WriteShort((short)theParticle.mAnimFrameRand);
			}
			if (theLayer.mLayerDef.mDeflectorVector.Count > 0)
			{
				theBuffer.WriteFloat(theParticle.mThicknessHitVariation);
			}
		}

		public void LoadParticleDefInstance(SexyBuffer theBuffer, PIParticleDefInstance theParticleDefInstance)
		{
			theParticleDefInstance.mNumberAcc = theBuffer.ReadFloat();
			theParticleDefInstance.mCurNumberVariation = theBuffer.ReadFloat();
			theParticleDefInstance.mParticlesEmitted = (int)theBuffer.ReadLong();
			theParticleDefInstance.mTicks = (int)theBuffer.ReadLong();
		}

		public void LoadParticle(SexyBuffer theBuffer, PILayer theLayer, PIParticleInstance theParticle)
		{
			theParticle.mTicks = theBuffer.ReadFloat();
			theParticle.mLife = theBuffer.ReadFloat();
			theParticle.mLifePct = theBuffer.ReadFloat();
			theParticle.mZoom = theBuffer.ReadFloat();
			theParticle.mPos = theBuffer.ReadVector2();
			theParticle.mVel = theBuffer.ReadVector2();
			theParticle.mEmittedPos = theBuffer.ReadVector2();
			if (theParticle.mParticleDef != null && theParticle.mParticleDef.mAttachToEmitter)
			{
				theParticle.mOrigPos = theBuffer.ReadVector2();
				theParticle.mOrigEmitterAng = theBuffer.ReadFloat();
			}
			theParticle.mImgAngle = theBuffer.ReadFloat();
			int num = (int)theBuffer.ReadShort();
			for (int i = 0; i < 9; i++)
			{
				if ((num & (1 << i)) != 0)
				{
					theParticle.mVariationValues[i] = theBuffer.ReadFloat();
				}
				else
				{
					theParticle.mVariationValues[i] = 0f;
				}
			}
			theParticle.mSrcSizeXMult = theBuffer.ReadFloat();
			theParticle.mSrcSizeYMult = theBuffer.ReadFloat();
			if (theParticle.mParticleDef != null && theParticle.mParticleDef.mRandomGradientColor)
			{
				theParticle.mGradientRand = theBuffer.ReadFloat();
			}
			if (theParticle.mParticleDef != null && theParticle.mParticleDef.mAnimStartOnRandomFrame)
			{
				theParticle.mAnimFrameRand = (int)theBuffer.ReadShort();
			}
			if (theLayer.mLayerDef.mDeflectorVector.Count > 0)
			{
				theParticle.mThicknessHitVariation = theBuffer.ReadFloat();
			}
			if (theParticle.mParticleDef != null && theParticle.mParticleDef.mAnimStartOnRandomFrame)
			{
				theParticle.mAnimFrameRand = (int)(this.mRand.Next() & 32767U);
				return;
			}
			theParticle.mAnimFrameRand = 0;
		}

		public Vector2 GetGeomPos(PIEmitterInstance theEmitterInstance, PIParticleInstance theParticleInstance, float theTravelAngle)
		{
			return this.GetGeomPos(theEmitterInstance, theParticleInstance, theTravelAngle, false);
		}

		public Vector2 GetGeomPos(PIEmitterInstance theEmitterInstance, PIParticleInstance theParticleInstance)
		{
			return this.GetGeomPos(theEmitterInstance, theParticleInstance, 0f, false);
		}

		public Vector2 GetGeomPos(PIEmitterInstance theEmitterInstance, PIParticleInstance theParticleInstance, float theTravelAngle, bool isMaskedOut)
		{
			Vector2 vector = default(Vector2);
			PIEmitterInstanceDef mEmitterInstanceDef = theEmitterInstance.mEmitterInstanceDef;
			switch (mEmitterInstanceDef.mEmitterGeom)
			{
			case 1:
				if (mEmitterInstanceDef.mPoints.Count >= 2)
				{
					int num = 0;
					float num2 = 0f;
					int num3 = 0;
					for (int i = 0; i < mEmitterInstanceDef.mPoints.Count - 1; i++)
					{
						Vector2 valueAt = mEmitterInstanceDef.mPoints[i].GetValueAt(this.mFrameNum);
						Vector2 valueAt2 = mEmitterInstanceDef.mPoints[i + 1].GetValueAt(this.mFrameNum);
						Vector2 vector2 = valueAt2 - valueAt;
						float num4 = vector2.X * vector2.X + vector2.Y * vector2.Y;
						num3 += (int)num4;
					}
					float num6;
					if (mEmitterInstanceDef.mEmitAtPointsNum != 0)
					{
						int num5 = theParticleInstance.mNum % mEmitterInstanceDef.mEmitAtPointsNum;
						num6 = (float)(num5 * num3) / (float)(mEmitterInstanceDef.mEmitAtPointsNum - 1);
					}
					else
					{
						num6 = this.GetRandFloatU() * (float)num3;
					}
					num3 = 0;
					for (int j = 0; j < mEmitterInstanceDef.mPoints.Count - 1; j++)
					{
						Vector2 valueAt3 = mEmitterInstanceDef.mPoints[j].GetValueAt(this.mFrameNum);
						Vector2 valueAt4 = mEmitterInstanceDef.mPoints[j + 1].GetValueAt(this.mFrameNum);
						Vector2 vector3 = valueAt4 - valueAt3;
						float num7 = vector3.X * vector3.X + vector3.Y * vector3.Y;
						if (num6 >= (float)num3 && num6 <= (float)num3 + num7)
						{
							num2 = (num6 - (float)num3) / num7;
							num = j;
							break;
						}
						num3 += (int)num7;
					}
					Vector2 valueAt5 = mEmitterInstanceDef.mPoints[num].GetValueAt(this.mFrameNum);
					Vector2 valueAt6 = mEmitterInstanceDef.mPoints[num + 1].GetValueAt(this.mFrameNum);
					Vector2 vector4 = valueAt6 - valueAt5;
					vector = valueAt5 * (1f - num2) + valueAt6 * num2;
					float num8 = (mEmitterInstanceDef.mEmitIn ? (mEmitterInstanceDef.mEmitOut ? this.GetRandSign() : (-1f)) : 1f);
					if (theTravelAngle != 0f)
					{
						float num9 = (float)Math.Atan2((double)vector4.Y, (double)vector4.X) + GlobalPIEffect.M_PI / 2f + num8 * GlobalPIEffect.M_PI / 2f;
						theTravelAngle += num9;
					}
				}
				break;
			case 2:
			{
				float valueAt7 = theEmitterInstance.mEmitterInstanceDef.mValues[15].GetValueAt(this.mFrameNum);
				float valueAt8 = theEmitterInstance.mEmitterInstanceDef.mValues[16].GetValueAt(this.mFrameNum);
				float num11;
				if (mEmitterInstanceDef.mEmitAtPointsNum != 0)
				{
					int num10 = theParticleInstance.mNum % mEmitterInstanceDef.mEmitAtPointsNum;
					num11 = (float)num10 * GlobalPIEffect.M_PI * 2f / (float)mEmitterInstanceDef.mEmitAtPointsNum;
					if (num11 > GlobalPIEffect.M_PI)
					{
						num11 -= GlobalPIEffect.M_PI * 2f;
					}
				}
				else
				{
					num11 = this.GetRandFloat() * GlobalPIEffect.M_PI;
				}
				if (valueAt7 > valueAt8)
				{
					float num12 = 1f + (valueAt7 / valueAt8 - 1f) * 0.3f;
					if (num11 < -GlobalPIEffect.M_PI / 2f)
					{
						num11 = (float)((double)GlobalPIEffect.M_PI + Math.Pow((double)((num11 + GlobalPIEffect.M_PI) / (GlobalPIEffect.M_PI / 2f)), (double)num12) * (double)GlobalPIEffect.M_PI / 2.0);
					}
					else if (num11 < 0f)
					{
						num11 = (float)(-(float)Math.Pow((double)(-(double)num11 / (GlobalPIEffect.M_PI / 2f)), (double)num12) * (double)GlobalPIEffect.M_PI / 2.0);
					}
					else if (num11 < GlobalPIEffect.M_PI / 2f)
					{
						num11 = (float)(Math.Pow((double)(num11 / (GlobalPIEffect.M_PI / 2f)), (double)num12) * (double)GlobalPIEffect.M_PI / 2.0);
					}
					else
					{
						num11 = (float)((double)GlobalPIEffect.M_PI - Math.Pow((double)((GlobalPIEffect.M_PI - num11) / (GlobalPIEffect.M_PI / 2f)), (double)num12) * (double)GlobalPIEffect.M_PI / 2.0);
					}
				}
				else if (valueAt8 > valueAt7)
				{
					float num13 = 1f + (valueAt8 / valueAt7 - 1f) * 0.3f;
					if (num11 < -GlobalPIEffect.M_PI / 2f)
					{
						num11 = (float)((double)(-(double)GlobalPIEffect.M_PI / 2f) - Math.Pow((double)((-(double)GlobalPIEffect.M_PI / 2f - num11) / (GlobalPIEffect.M_PI / 2f)), (double)num13) * (double)GlobalPIEffect.M_PI / 2.0);
					}
					else if (num11 < 0f)
					{
						num11 = (float)((double)(-(double)GlobalPIEffect.M_PI / 2f) + Math.Pow((double)((num11 + GlobalPIEffect.M_PI / 2f) / (GlobalPIEffect.M_PI / 2f)), (double)num13) * (double)GlobalPIEffect.M_PI / 2.0);
					}
					else if (num11 < GlobalPIEffect.M_PI / 2f)
					{
						num11 = (float)((double)(GlobalPIEffect.M_PI / 2f) - Math.Pow((double)((GlobalPIEffect.M_PI / 2f - num11) / (GlobalPIEffect.M_PI / 2f)), (double)num13) * (double)GlobalPIEffect.M_PI / 2.0);
					}
					else
					{
						num11 = (float)((double)(GlobalPIEffect.M_PI / 2f) + Math.Pow((double)((num11 - GlobalPIEffect.M_PI / 2f) / (GlobalPIEffect.M_PI / 2f)), (double)num13) * (double)GlobalPIEffect.M_PI / 2.0);
					}
				}
				vector = new Vector2((float)(Math.Cos((double)num11) * (double)valueAt7), (float)(Math.Sin((double)num11) * (double)valueAt8));
				if (theTravelAngle != 0f)
				{
					float num14 = (mEmitterInstanceDef.mEmitIn ? (mEmitterInstanceDef.mEmitOut ? this.GetRandSign() : (-1f)) : 1f);
					float num15 = num11 + num14 * GlobalPIEffect.M_PI / 2f;
					theTravelAngle += num15;
				}
				break;
			}
			case 3:
			{
				float valueAt9 = theEmitterInstance.mEmitterInstanceDef.mValues[15].GetValueAt(this.mFrameNum);
				float valueAt10 = theEmitterInstance.mEmitterInstanceDef.mValues[16].GetValueAt(this.mFrameNum);
				if (mEmitterInstanceDef.mEmitAtPointsNum != 0)
				{
					float num16 = (float)(theParticleInstance.mNum % mEmitterInstanceDef.mEmitAtPointsNum);
					float num17 = (float)(theParticleInstance.mNum / mEmitterInstanceDef.mEmitAtPointsNum % mEmitterInstanceDef.mEmitAtPointsNum2);
					if (mEmitterInstanceDef.mEmitAtPointsNum > 1)
					{
						vector.X = (float)((double)(num16 / (float)(mEmitterInstanceDef.mEmitAtPointsNum - 1)) - 0.5) * valueAt9;
					}
					if (mEmitterInstanceDef.mEmitAtPointsNum2 > 1)
					{
						vector.Y = (float)((double)(num17 / (float)(mEmitterInstanceDef.mEmitAtPointsNum2 - 1)) - 0.5) * valueAt10;
					}
				}
				else
				{
					vector = new Vector2(this.GetRandFloat() * valueAt9 / 2f, this.GetRandFloat() * valueAt10 / 2f);
				}
				if (theEmitterInstance.mMaskImage.GetDeviceImage() != null && isMaskedOut)
				{
					float num18 = vector.X / valueAt9 + 0.5f;
					float num19 = vector.Y / valueAt10 + 0.5f;
					int num20 = theEmitterInstance.mMaskImage.mWidth;
					int num21 = theEmitterInstance.mMaskImage.mHeight;
					int num22 = Math.Min((int)(num18 * (float)num20), num20 - 1);
					int num23 = Math.Min((int)(num19 * (float)num21), num21 - 1);
					uint[] bits = theEmitterInstance.mMaskImage.GetDeviceImage().GetBits();
					uint num24 = bits[num22 + num23 * num20];
					if (((num24 & 2147483648U) == 0U) ^ mEmitterInstanceDef.mInvertMask)
					{
						isMaskedOut = true;
					}
				}
				break;
			}
			case 4:
			{
				float valueAt11 = theEmitterInstance.mEmitterInstanceDef.mValues[15].GetValueAt(this.mFrameNum);
				float num26;
				if (mEmitterInstanceDef.mEmitAtPointsNum != 0)
				{
					int num25 = theParticleInstance.mNum % mEmitterInstanceDef.mEmitAtPointsNum;
					num26 = (float)num25 * GlobalPIEffect.M_PI * 2f / (float)mEmitterInstanceDef.mEmitAtPointsNum;
				}
				else
				{
					num26 = this.GetRandFloat() * GlobalPIEffect.M_PI;
				}
				vector = new Vector2((float)Math.Cos((double)num26) * valueAt11, (float)Math.Sin((double)num26) * valueAt11);
				if (theTravelAngle != 0f)
				{
					float num27 = (mEmitterInstanceDef.mEmitIn ? (mEmitterInstanceDef.mEmitOut ? this.GetRandSign() : (-1f)) : 1f);
					float num28 = num26 + num27 * GlobalPIEffect.M_PI / 2f;
					theTravelAngle += num28;
				}
				break;
			}
			}
			vector += this.GetEmitterPos(theEmitterInstance, false);
			vector += theEmitterInstance.mOffset;
			vector = GlobalPIEffect.TransformFPoint(theEmitterInstance.mTransform, vector);
			return GlobalPIEffect.TransformFPoint(this.mEmitterTransform, vector);
		}

		public Vector2 GetEmitterPos(PIEmitterInstance theEmitterInstance, bool doTransform)
		{
			Vector2 vector = theEmitterInstance.mEmitterInstanceDef.mPosition.GetValueAt(this.mFrameNum);
			if (doTransform)
			{
				vector = GlobalPIEffect.TransformFPoint(theEmitterInstance.mTransform, vector);
				vector = GlobalPIEffect.TransformFPoint(this.mEmitterTransform, vector);
				vector += theEmitterInstance.mOffset;
			}
			return vector;
		}

		public int CountParticles(PIParticleInstance theStart)
		{
			int num = 0;
			while (theStart != null)
			{
				num++;
				theStart = theStart.mNext;
			}
			return num;
		}

		public void CalcParticleTransform(PILayer theLayer, PIEmitterInstance theEmitterInstance, PIEmitter theEmitter, PIParticleDef theParticleDef, PIParticleGroup theParticleGroup, PIParticleInstance theParticleInstance)
		{
			float mLifePct = theParticleInstance.mLifePct;
			float num = 1f;
			float num2 = 1f;
			Rect rect = Rect.ZERO_RECT;
			float num6;
			float num7;
			if (theParticleDef != null)
			{
				PITexture pitexture = this.mDef.mTextureVector[theParticleDef.mTextureIdx];
				if (pitexture.mImageVector.Count != 0)
				{
					DeviceImage deviceImage = pitexture.mImageVector[theParticleInstance.mImgIdx].GetDeviceImage();
					rect = new Rect(0, 0, deviceImage.mWidth, deviceImage.mHeight);
				}
				else
				{
					DeviceImage deviceImage = pitexture.mImageStrip.GetDeviceImage();
					if (deviceImage == null)
					{
						pitexture.mImageStrip = this.GetImage(pitexture.mName, pitexture.mFileName);
						deviceImage = pitexture.mImageStrip.GetDeviceImage();
					}
					rect = deviceImage.GetCelRect(theParticleInstance.mImgIdx);
					if (pitexture.mPadded)
					{
						rect.mX++;
						rect.mWidth -= 2;
						rect.mY++;
						rect.mHeight -= 2;
					}
				}
				if (theParticleDef.mSingleParticle)
				{
					theParticleInstance.mSrcSizeXMult = (theParticleGroup.mWasEmitted ? theEmitter.mValues[10].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[2].GetValueAt(this.mFrameNum)) * (theParticleDef.mValues[2].GetValueAt(this.mFrameNum) + theParticleInstance.mVariationValues[1]);
					theParticleInstance.mSrcSizeYMult = (theParticleGroup.mWasEmitted ? theEmitter.mValues[11].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[2].GetValueAt(this.mFrameNum)) * (theParticleDef.mValues[2].GetValueAt(this.mFrameNum) + theParticleInstance.mVariationValues[1]);
				}
				float num3 = Math.Max(theParticleDef.mValues[16].GetValueAt(mLifePct) * theParticleInstance.mSrcSizeXMult, 0.1f);
				float num4 = Math.Max(theParticleDef.mValues[27].GetValueAt(mLifePct) * theParticleInstance.mSrcSizeYMult, 0.1f);
				int num5 = Math.Max(rect.mWidth, rect.mHeight);
				num = (float)num5 / (float)rect.mWidth;
				num2 = (float)num5 / (float)rect.mHeight;
				num6 = num3 / (float)num5 * 2f;
				num7 = num4 / (float)num5 * 2f;
			}
			else
			{
				num6 = 1f;
				num7 = 1f;
			}
			SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
			float valueAt = theEmitterInstance.mEmitterInstanceDef.mValues[14].GetValueAt(this.mFrameNum);
			if (valueAt != 0f)
			{
				sexyTransform2D.RotateDeg(valueAt);
			}
			if (theParticleInstance.mParentFreeEmitter != null && theParticleInstance.mParentFreeEmitter.mImgAngle != 0f)
			{
				sexyTransform2D.RotateRad(-theParticleInstance.mParentFreeEmitter.mImgAngle);
			}
			SexyTransform2D mTransform = new SexyTransform2D(false);
			float num8 = 1f;
			if (theParticleDef != null)
			{
				mTransform.Translate(-theParticleDef.mRefPointOfs.X * num * (float)rect.mWidth, -theParticleDef.mRefPointOfs.Y * num2 * (float)rect.mHeight);
				if (theParticleDef.mFlipHorz)
				{
					mTransform.Scale(-1f, 1f);
				}
				if (theParticleDef.mFlipVert)
				{
					mTransform.Scale(1f, -1f);
				}
			}
			float num9 = 0f;
			num8 *= num6 * num7;
			if (num6 != 1f || num7 != 1f)
			{
				mTransform.Scale(num6, num7);
			}
			if (theParticleInstance.mImgAngle != 0f)
			{
				num9 += theParticleInstance.mImgAngle;
			}
			if (theParticleDef != null && theParticleDef.mAttachToEmitter)
			{
				float num10;
				if (theParticleInstance.mParentFreeEmitter != null)
				{
					num10 = (theParticleInstance.mParentFreeEmitter.mImgAngle - theParticleInstance.mOrigEmitterAng) * theParticleDef.mAttachVal;
				}
				else
				{
					num10 = MathHelper.ToRadians(theEmitterInstance.mEmitterInstanceDef.mValues[14].GetValueAt(this.mFrameNum)) * theParticleDef.mAttachVal;
				}
				if (num10 != 0f)
				{
					num9 += num10;
				}
			}
			if (theParticleDef != null && theParticleDef.mSingleParticle && (!theParticleDef.mAngleKeepAlignedToMotion || theParticleDef.mAttachToEmitter))
			{
				num9 += MathHelper.ToRadians(theEmitterInstance.mEmitterInstanceDef.mValues[14].GetValueAt(this.mFrameNum));
			}
			mTransform.RotateRad(num9);
			Vector2 vector = theParticleInstance.mPos;
			if (theParticleDef != null && theParticleDef.mAttachToEmitter)
			{
				SexyTransform2D impliedObject = new SexyTransform2D(false);
				impliedObject.RotateRad(theParticleInstance.mOrigEmitterAng);
				Vector2 theVec = impliedObject * vector;
				Vector2 vector2 = sexyTransform2D * theVec;
				vector = vector * (1f - theParticleDef.mAttachVal) + vector2 * theParticleDef.mAttachVal;
			}
			mTransform.Translate(vector.X, vector.Y);
			if (theParticleDef != null && theParticleDef.mSingleParticle)
			{
				theParticleInstance.mZoom = (theParticleGroup.mWasEmitted ? theEmitter.mValues[17].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[8].GetValueAt(this.mFrameNum)) * theEmitter.mValues[17].GetValueAt(this.mFrameNum, 1f);
			}
			num8 *= theParticleInstance.mZoom * theParticleInstance.mZoom;
			if (theParticleInstance.mZoom != 1f)
			{
				mTransform.Scale(theParticleInstance.mZoom, theParticleInstance.mZoom);
			}
			Vector2 vector3 = theParticleInstance.mEmittedPos;
			if (theParticleDef != null && theParticleDef.mSingleParticle)
			{
				Vector2 vector4 = sexyTransform2D * theParticleInstance.mOrigPos;
				vector4 += this.GetEmitterPos(theEmitterInstance, !theParticleGroup.mWasEmitted);
				vector3 = vector4;
			}
			else if (theParticleDef != null && theParticleDef.mAttachToEmitter && !theParticleGroup.mIsSuperEmitter)
			{
				Vector2 vector5;
				if (theParticleInstance.mParentFreeEmitter != null)
				{
					vector5 = theParticleInstance.mParentFreeEmitter.mLastEmitterPos + theParticleInstance.mParentFreeEmitter.mOrigPos + theParticleInstance.mParentFreeEmitter.mPos;
				}
				else
				{
					vector5 = GlobalPIEffect.TransformFPoint(sexyTransform2D, theParticleInstance.mOrigPos);
					vector5 += this.GetEmitterPos(theEmitterInstance, !theParticleGroup.mWasEmitted);
				}
				vector3 = vector3 * (1f - theParticleDef.mAttachVal) + vector5 * theParticleDef.mAttachVal;
			}
			theParticleInstance.mLastEmitterPos = vector3;
			mTransform.Translate(vector3.X, vector3.Y);
			Vector2 vector6 = theLayer.mLayerDef.mOffset.GetValueAt(this.mFrameNum) - theLayer.mLayerDef.mOrigOffset;
			mTransform.Translate(vector6.X, vector6.Y);
			float valueAt2 = theLayer.mLayerDef.mAngle.GetValueAt(this.mFrameNum);
			if (valueAt2 != 0f)
			{
				mTransform.RotateDeg(valueAt2);
			}
			theParticleInstance.mTransform = mTransform;
			theParticleInstance.mTransformScaleFactor = num8;
		}

		public void UpdateParticleDef(PILayer theLayer, PIEmitter theEmitter, PIEmitterInstance theEmitterInstance, PIParticleDef theParticleDef, PIParticleDefInstance theParticleDefInstance, PIParticleGroup theParticleGroup, PIFreeEmitterInstance theFreeEmitter)
		{
			PIEmitterInstanceDef mEmitterInstanceDef = theEmitterInstance.mEmitterInstanceDef;
			float num = 100f / this.mAnimSpeed;
			float num2 = 0f;
			if (theFreeEmitter != null)
			{
				num2 = theFreeEmitter.mLifePct;
			}
			if (theParticleDefInstance.mTicks % 25 == 0 && !theParticleGroup.mIsSuperEmitter)
			{
				if (theParticleDefInstance.mTicks == 0)
				{
					theParticleDefInstance.mCurNumberVariation = this.GetRandFloat() * 0.5f * theParticleDef.mValues[9].GetValueAt(this.mFrameNum) / 2f;
				}
				else
				{
					theParticleDefInstance.mCurNumberVariation = this.GetRandFloat() * 0.75f * theParticleDef.mValues[9].GetValueAt(this.mFrameNum) / 2f;
				}
			}
			theParticleDefInstance.mTicks++;
			float num3;
			if (theParticleGroup.mIsSuperEmitter)
			{
				num3 = theEmitter.mValues[1].GetValueAt(this.mFrameNum) * (theParticleGroup.mWasEmitted ? theEmitter.mValues[9].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[1].GetValueAt(this.mFrameNum));
			}
			else
			{
				num3 = (theParticleGroup.mWasEmitted ? theEmitter.mValues[9].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[1].GetValueAt(this.mFrameNum)) * (theParticleDef.mValues[1].GetValueAt(this.mFrameNum) + theParticleDefInstance.mCurNumberVariation) * theEmitter.mValues[33].GetValueAt(num2, 1f);
				num3 = Math.Max(0f, num3);
				if (theParticleGroup.mWasEmitted && num2 >= 1f)
				{
					num3 = 0f;
				}
			}
			num3 *= theEmitterInstance.mNumberScale;
			if (theParticleGroup.mIsSuperEmitter)
			{
				num3 *= 30f;
			}
			else if (!theParticleGroup.mWasEmitted)
			{
				switch (mEmitterInstanceDef.mEmitterGeom)
				{
				case 1:
					if (mEmitterInstanceDef.mEmitAtPointsNum != 0)
					{
						num3 *= (float)mEmitterInstanceDef.mEmitAtPointsNum;
					}
					else
					{
						int num4 = 0;
						for (int i = 0; i < mEmitterInstanceDef.mPoints.Count - 1; i++)
						{
							Vector2 valueAt = mEmitterInstanceDef.mPoints[i].GetValueAt(this.mFrameNum);
							Vector2 valueAt2 = mEmitterInstanceDef.mPoints[i + 1].GetValueAt(this.mFrameNum);
							Vector2 vector = valueAt2 - valueAt;
							float num5 = (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
							num4 += (int)num5;
						}
						num3 *= (float)num4 / 35f;
					}
					break;
				case 2:
				{
					float valueAt3 = theEmitterInstance.mEmitterInstanceDef.mValues[15].GetValueAt(this.mFrameNum);
					float valueAt4 = theEmitterInstance.mEmitterInstanceDef.mValues[16].GetValueAt(this.mFrameNum);
					if (mEmitterInstanceDef.mEmitAtPointsNum != 0)
					{
						num3 *= (float)mEmitterInstanceDef.mEmitAtPointsNum;
					}
					else
					{
						float num6 = 6.28318f * (float)Math.Sqrt((double)((valueAt3 * valueAt3 + valueAt4 * valueAt4) / 2f));
						num3 *= num6 / 35f;
					}
					break;
				}
				case 3:
					if (mEmitterInstanceDef.mEmitAtPointsNum != 0)
					{
						num3 *= (float)(mEmitterInstanceDef.mEmitAtPointsNum * mEmitterInstanceDef.mEmitAtPointsNum2);
					}
					else
					{
						float valueAt5 = theEmitterInstance.mEmitterInstanceDef.mValues[15].GetValueAt(this.mFrameNum);
						float valueAt6 = theEmitterInstance.mEmitterInstanceDef.mValues[16].GetValueAt(this.mFrameNum);
						num3 *= 1f + valueAt5 * valueAt6 / 900f / 4f;
					}
					break;
				case 4:
				{
					float valueAt7 = theEmitterInstance.mEmitterInstanceDef.mValues[15].GetValueAt(this.mFrameNum);
					if (mEmitterInstanceDef.mEmitAtPointsNum != 0)
					{
						num3 *= (float)mEmitterInstanceDef.mEmitAtPointsNum;
					}
					else
					{
						float num7 = 6.28318f * (float)Math.Sqrt((double)(valueAt7 * valueAt7));
						num3 *= num7 / 35f;
					}
					break;
				}
				}
			}
			theParticleDefInstance.mNumberAcc += num3 / num * 0.16f;
			if ((!mEmitterInstanceDef.mIsSuperEmitter && !theEmitterInstance.mWasActive) || !theEmitterInstance.mWithinLifeFrame)
			{
				theParticleDefInstance.mNumberAcc = 0f;
			}
			bool flag = true;
			if (!theParticleGroup.mIsSuperEmitter && theParticleDef.mSingleParticle)
			{
				int num8;
				if (mEmitterInstanceDef.mEmitterGeom == 1 || mEmitterInstanceDef.mEmitterGeom == 4)
				{
					num8 = mEmitterInstanceDef.mEmitAtPointsNum;
				}
				else if (mEmitterInstanceDef.mEmitterGeom == 3)
				{
					num8 = mEmitterInstanceDef.mEmitAtPointsNum * mEmitterInstanceDef.mEmitAtPointsNum2;
				}
				else
				{
					num8 = 1;
				}
				if (num8 == 0)
				{
					flag = false;
					num8 = 1;
				}
				int num9 = 0;
				for (PIParticleInstance piparticleInstance = theParticleGroup.mHead; piparticleInstance != null; piparticleInstance = piparticleInstance.mNext)
				{
					if (piparticleInstance.mParticleDef == theParticleDef)
					{
						num9++;
					}
				}
				theParticleDefInstance.mNumberAcc = (float)(num8 - num9);
			}
			while (theParticleDefInstance.mNumberAcc >= 1.1f)
			{
				theParticleDefInstance.mNumberAcc -= 1.1f;
				PIParticleInstance piparticleInstance2;
				if (theParticleGroup.mIsSuperEmitter)
				{
					PIFreeEmitterInstance pifreeEmitterInstance = this.mFreeEmitterPool.Alloc();
					pifreeEmitterInstance.Reset();
					pifreeEmitterInstance.mEmitter.mParticleDefInstanceVector.Resize(theEmitter.mParticleDefVector.Count);
					piparticleInstance2 = pifreeEmitterInstance;
				}
				else
				{
					piparticleInstance2 = this.mParticlePool.Alloc();
					piparticleInstance2.Reset();
				}
				piparticleInstance2.mParticleDef = theParticleDef;
				piparticleInstance2.mEmitterSrc = theEmitter;
				piparticleInstance2.mParentFreeEmitter = theFreeEmitter;
				piparticleInstance2.mNum = theParticleDefInstance.mParticlesEmitted++;
				float num10;
				if (!theParticleGroup.mIsSuperEmitter)
				{
					if (theParticleDef.mUseEmitterAngleAndRange)
					{
						if (theParticleGroup.mWasEmitted)
						{
							num10 = theEmitter.mValues[21].GetValueAt(this.mFrameNum) + theEmitter.mValues[22].GetValueAt(this.mFrameNum) * this.GetRandFloat() / 2f;
						}
						else
						{
							num10 = (theParticleGroup.mWasEmitted ? theEmitter.mValues[21].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[11].GetValueAt(this.mFrameNum)) + (theParticleGroup.mWasEmitted ? theEmitter.mValues[22].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[12].GetValueAt(this.mFrameNum)) * this.GetRandFloat() / 2f;
						}
					}
					else
					{
						num10 = theParticleDef.mValues[23].GetValueAt(this.mFrameNum) + theParticleDef.mValues[24].GetValueAt(this.mFrameNum) * this.GetRandFloat() / 2f;
					}
				}
				else
				{
					num10 = (theParticleGroup.mWasEmitted ? theEmitter.mValues[21].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[11].GetValueAt(this.mFrameNum)) + (theParticleGroup.mWasEmitted ? theEmitter.mValues[22].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[12].GetValueAt(this.mFrameNum)) * this.GetRandFloat() / 2f;
				}
				num10 = MathHelper.ToRadians(-num10);
				float num11;
				if (theFreeEmitter != null)
				{
					num11 = theFreeEmitter.mImgAngle;
				}
				else
				{
					num11 = MathHelper.ToRadians(-theEmitterInstance.mEmitterInstanceDef.mValues[14].GetValueAt(this.mFrameNum));
				}
				num10 += num11;
				piparticleInstance2.mOrigEmitterAng = num11;
				if (theParticleDef != null && theParticleDef.mAnimStartOnRandomFrame)
				{
					piparticleInstance2.mAnimFrameRand = (int)(this.mRand.Next() & 32767U);
				}
				else
				{
					piparticleInstance2.mAnimFrameRand = 0;
				}
				piparticleInstance2.mZoom = (theParticleGroup.mWasEmitted ? theEmitter.mValues[17].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[8].GetValueAt(this.mFrameNum)) * theEmitter.mValues[17].GetValueAt(this.mFrameNum, 1f);
				if (!theParticleGroup.mIsSuperEmitter)
				{
					piparticleInstance2.mVariationValues[0] = this.GetVariationScalar() * theParticleDef.mValues[8].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[1] = this.GetVariationScalar() * theParticleDef.mValues[10].GetValueAt(this.mFrameNum);
					if (theParticleDef == null || theParticleDef.mLockAspect)
					{
						piparticleInstance2.mVariationValues[2] = piparticleInstance2.mVariationValues[1];
					}
					else
					{
						piparticleInstance2.mVariationValues[2] = this.GetVariationScalar() * theParticleDef.mValues[26].GetValueAt(this.mFrameNum);
					}
					piparticleInstance2.mVariationValues[3] = this.GetVariationScalar() * theParticleDef.mValues[11].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[4] = this.GetVariationScalar() * theParticleDef.mValues[12].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[5] = this.GetVariationScalar() * theParticleDef.mValues[13].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[6] = this.GetVariationScalar() * theParticleDef.mValues[14].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[7] = this.GetVariationScalar() * theParticleDef.mValues[15].GetValueAt(this.mFrameNum);
					piparticleInstance2.mSrcSizeXMult = (theParticleGroup.mWasEmitted ? theEmitter.mValues[10].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[2].GetValueAt(this.mFrameNum)) * (theParticleDef.mValues[2].GetValueAt(this.mFrameNum) + piparticleInstance2.mVariationValues[1]);
					piparticleInstance2.mSrcSizeYMult = (theParticleGroup.mWasEmitted ? theEmitter.mValues[11].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[17].GetValueAt(this.mFrameNum)) * (theParticleDef.mValues[25].GetValueAt(this.mFrameNum) + piparticleInstance2.mVariationValues[2]);
					if (theParticleGroup.mWasEmitted)
					{
						piparticleInstance2.mSrcSizeXMult *= (1f + theFreeEmitter.mVariationValues[1]) * theEmitter.mValues[34].GetValueAt(num2, 1f);
						piparticleInstance2.mSrcSizeYMult *= (1f + theFreeEmitter.mVariationValues[2]) * theEmitter.mValues[34].GetValueAt(num2, 1f);
						piparticleInstance2.mZoom *= (1f + theFreeEmitter.mVariationValues[8]) * theEmitter.mValues[41].GetValueAt(num2, 1f);
					}
				}
				else
				{
					piparticleInstance2.mVariationValues[0] = this.GetVariationScalar() * theEmitter.mValues[23].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[1] = this.GetRandFloat() * theEmitter.mValues[25].GetValueAt(this.mFrameNum);
					if (theParticleDef == null || theParticleDef.mLockAspect)
					{
						piparticleInstance2.mVariationValues[2] = piparticleInstance2.mVariationValues[1];
					}
					else
					{
						piparticleInstance2.mVariationValues[2] = this.GetRandFloat() * theEmitter.mValues[26].GetValueAt(this.mFrameNum);
					}
					piparticleInstance2.mVariationValues[3] = this.GetVariationScalar() * theEmitter.mValues[27].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[4] = this.GetVariationScalar() * theEmitter.mValues[28].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[5] = this.GetVariationScalar() * theEmitter.mValues[29].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[6] = this.GetVariationScalar() * theEmitter.mValues[30].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[7] = this.GetVariationScalar() * theEmitter.mValues[31].GetValueAt(this.mFrameNum);
					piparticleInstance2.mVariationValues[8] = this.GetVariationScalar() * theEmitter.mValues[32].GetValueAt(this.mFrameNum);
				}
				float num12 = num10;
				piparticleInstance2.mGradientRand = this.GetRandFloatU();
				piparticleInstance2.mTicks = 0f;
				piparticleInstance2.mThicknessHitVariation = this.GetRandFloat();
				piparticleInstance2.mImgAngle = 0f;
				if (theParticleGroup.mIsSuperEmitter)
				{
					piparticleInstance2.mLife = (theEmitter.mValues[0].GetValueAt(this.mFrameNum) + piparticleInstance2.mVariationValues[0]) * 5f * (theParticleGroup.mWasEmitted ? theEmitter.mValues[8].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[0].GetValueAt(this.mFrameNum));
				}
				else
				{
					piparticleInstance2.mLife = (theParticleGroup.mWasEmitted ? theEmitter.mValues[8].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[0].GetValueAt(this.mFrameNum)) * (theParticleDef.mValues[0].GetValueAt(this.mFrameNum) + piparticleInstance2.mVariationValues[0]);
				}
				Vector2 vector2 = default(Vector2);
				if (theParticleGroup.mWasEmitted)
				{
					piparticleInstance2.mEmittedPos = theFreeEmitter.mLastEmitterPos + theFreeEmitter.mPos;
					piparticleInstance2.mLastEmitterPos = piparticleInstance2.mEmittedPos;
				}
				else
				{
					piparticleInstance2.mEmittedPos = this.GetEmitterPos(theEmitterInstance, true);
					piparticleInstance2.mLastEmitterPos = piparticleInstance2.mEmittedPos;
					bool flag2 = false;
					if (flag)
					{
						vector2 = this.GetGeomPos(theEmitterInstance, piparticleInstance2, num12, flag2) - piparticleInstance2.mEmittedPos;
					}
					if (flag2)
					{
						continue;
					}
				}
				piparticleInstance2.mVel = new Vector2((float)Math.Cos((double)num12), (float)Math.Sin((double)num12));
				if (theParticleGroup.mIsSuperEmitter)
				{
					piparticleInstance2.mVel = piparticleInstance2.mVel * ((theParticleGroup.mWasEmitted ? theEmitter.mValues[12].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[3].GetValueAt(this.mFrameNum)) * (theEmitter.mValues[2].GetValueAt(this.mFrameNum) + piparticleInstance2.mVariationValues[3])) * 160f;
				}
				else
				{
					piparticleInstance2.mVel *= (theParticleGroup.mWasEmitted ? theEmitter.mValues[12].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[3].GetValueAt(this.mFrameNum)) * (theParticleDef.mValues[3].GetValueAt(this.mFrameNum) + piparticleInstance2.mVariationValues[3]);
				}
				if (!theParticleGroup.mIsSuperEmitter)
				{
					if (theParticleDef.mAngleAlignToMotion)
					{
						if (piparticleInstance2.mVel.Length() == 0f)
						{
							num12 = 0f;
							if (Math.Cos((double)num12) > 0.0)
							{
								piparticleInstance2.mImgAngle = 0f;
							}
							else
							{
								piparticleInstance2.mImgAngle = GlobalPIEffect.M_PI;
							}
							if (theParticleDef.mSingleParticle && theParticleDef.mAngleKeepAlignedToMotion && !theParticleDef.mAttachToEmitter)
							{
								piparticleInstance2.mImgAngle += MathHelper.ToRadians(theEmitterInstance.mEmitterInstanceDef.mValues[14].GetValueAt(this.mFrameNum));
							}
						}
						else
						{
							piparticleInstance2.mImgAngle = -num12;
						}
						piparticleInstance2.mImgAngle += MathHelper.ToRadians((float)(-(float)theParticleDef.mAngleAlignOffset));
					}
					else if (theParticleDef.mAngleRandomAlign)
					{
						piparticleInstance2.mImgAngle = MathHelper.ToRadians(-((float)theParticleDef.mAngleOffset + this.GetRandFloat() * (float)theParticleDef.mAngleRange / 2f));
					}
					else
					{
						piparticleInstance2.mImgAngle = MathHelper.ToRadians((float)(-(float)theParticleDef.mAngleValue));
					}
				}
				piparticleInstance2.mOrigPos = vector2;
				SexyTransform2D theMatrix = new SexyTransform2D(false);
				theMatrix.RotateDeg(theEmitterInstance.mEmitterInstanceDef.mValues[14].GetValueAt(this.mFrameNum));
				piparticleInstance2.mEmittedPos += GlobalPIEffect.TransformFPoint(theMatrix, vector2);
				if (theEmitter.mOldestInFront)
				{
					if (theParticleGroup.mHead != null)
					{
						theParticleGroup.mHead.mPrev = piparticleInstance2;
					}
					piparticleInstance2.mNext = theParticleGroup.mHead;
					if (theParticleGroup.mTail == null)
					{
						theParticleGroup.mTail = piparticleInstance2;
					}
					theParticleGroup.mHead = piparticleInstance2;
				}
				else
				{
					if (theParticleGroup.mTail != null)
					{
						theParticleGroup.mTail.mNext = piparticleInstance2;
					}
					piparticleInstance2.mPrev = theParticleGroup.mTail;
					if (theParticleGroup.mHead == null)
					{
						theParticleGroup.mHead = piparticleInstance2;
					}
					theParticleGroup.mTail = piparticleInstance2;
				}
				theParticleGroup.mCount++;
			}
		}

		public void UpdateParticleGroup(PILayer theLayer, PIEmitterInstance theEmitterInstance, PIParticleGroup theParticleGroup)
		{
			float num = 100f / this.mAnimSpeed;
			PIParticleInstance piparticleInstance = theParticleGroup.mHead;
			PILayerDef mLayerDef = theLayer.mLayerDef;
			PIEmitterInstanceDef mEmitterInstanceDef = theEmitterInstance.mEmitterInstanceDef;
			while (piparticleInstance != null)
			{
				PIParticleInstance mNext = piparticleInstance.mNext;
				PIEmitter mEmitterSrc = piparticleInstance.mEmitterSrc;
				PIParticleDef mParticleDef = piparticleInstance.mParticleDef;
				if (piparticleInstance.mParentFreeEmitter != null)
				{
					float mLifePct = piparticleInstance.mParentFreeEmitter.mLifePct;
				}
				bool flag = piparticleInstance.mTicks == 0f;
				piparticleInstance.mTicks += 1f / num;
				float num2;
				if (mParticleDef != null && mParticleDef.mSingleParticle)
				{
					float nextKeyframeTime = theEmitterInstance.mEmitterInstanceDef.mValues[13].GetNextKeyframeTime(this.mFrameNum);
					int nextKeyframeIdx = theEmitterInstance.mEmitterInstanceDef.mValues[13].GetNextKeyframeIdx(this.mFrameNum);
					if (nextKeyframeTime >= this.mFrameNum && nextKeyframeIdx == 1)
					{
						num2 = Math.Min(1f, (this.mFrameNum + (float)mEmitterInstanceDef.mFramesToPreload) / Math.Max(1f, nextKeyframeTime));
					}
					else
					{
						num2 = 0.02f;
					}
				}
				else
				{
					num2 = piparticleInstance.mTicks / piparticleInstance.mLife;
				}
				piparticleInstance.mLifePct = num2;
				if (piparticleInstance.mLifePct >= 0.9999999f || piparticleInstance.mLife <= 1E-08f || (!theEmitterInstance.mWasActive && !mEmitterInstanceDef.mIsSuperEmitter))
				{
					if (theParticleGroup.mIsSuperEmitter && ((PIFreeEmitterInstance)piparticleInstance).mEmitter.mParticleGroup.mHead != null)
					{
						piparticleInstance = mNext;
						continue;
					}
					if (theParticleGroup.mIsSuperEmitter || !mParticleDef.mSingleParticle || !theEmitterInstance.mWasActive)
					{
						if (theParticleGroup.mIsSuperEmitter)
						{
							this.mFreeEmitterPool.Free((PIFreeEmitterInstance)piparticleInstance);
						}
						else
						{
							this.mParticlePool.Free(piparticleInstance);
						}
						if (piparticleInstance.mPrev != null)
						{
							piparticleInstance.mPrev.mNext = piparticleInstance.mNext;
						}
						if (piparticleInstance.mNext != null)
						{
							piparticleInstance.mNext.mPrev = piparticleInstance.mPrev;
						}
						if (theParticleGroup.mHead == piparticleInstance)
						{
							theParticleGroup.mHead = piparticleInstance.mNext;
						}
						if (theParticleGroup.mTail == piparticleInstance)
						{
							theParticleGroup.mTail = piparticleInstance.mPrev;
						}
						theParticleGroup.mCount--;
						piparticleInstance = mNext;
						continue;
					}
				}
				if (mParticleDef != null)
				{
					PITexture pitexture = this.mDef.mTextureVector[mParticleDef.mTextureIdx];
					if (mParticleDef.mAnimSpeed == -1)
					{
						piparticleInstance.mImgIdx = piparticleInstance.mAnimFrameRand % pitexture.mNumCels;
					}
					else
					{
						piparticleInstance.mImgIdx = ((int)(piparticleInstance.mTicks * (float)this.mFramerate / (float)(mParticleDef.mAnimSpeed + 1)) + piparticleInstance.mAnimFrameRand) % pitexture.mNumCels;
					}
				}
				if (theParticleGroup.mIsSuperEmitter || !mParticleDef.mSingleParticle)
				{
					if (this.mIsNewFrame)
					{
						float num3 = this.GetRandFloat() * this.GetRandFloat();
						float num4 = this.GetRandFloat() * this.GetRandFloat();
						float num5;
						if (theParticleGroup.mIsSuperEmitter)
						{
							num5 = Math.Max(0f, (theParticleGroup.mWasEmitted ? mEmitterSrc.mValues[15].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[6].GetValueAt(this.mFrameNum)) * mEmitterSrc.mValues[39].GetValueAt(num2, 1f) * (mEmitterSrc.mValues[5].GetValueAt(this.mFrameNum) + piparticleInstance.mVariationValues[6])) * 30f;
						}
						else
						{
							num5 = Math.Max(0f, (theParticleGroup.mWasEmitted ? mEmitterSrc.mValues[15].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[6].GetValueAt(this.mFrameNum)) * mParticleDef.mValues[20].GetValueAt(num2) * (mParticleDef.mValues[6].GetValueAt(this.mFrameNum) + piparticleInstance.mVariationValues[6]));
						}
						PIParticleInstance piparticleInstance2 = piparticleInstance;
						piparticleInstance2.mVel.X = piparticleInstance2.mVel.X + num3 * num5;
						PIParticleInstance piparticleInstance3 = piparticleInstance;
						piparticleInstance3.mVel.Y = piparticleInstance3.mVel.Y + num4 * num5;
					}
					float num6;
					if (theParticleGroup.mIsSuperEmitter)
					{
						num6 = (theParticleGroup.mWasEmitted ? mEmitterSrc.mValues[13].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[4].GetValueAt(this.mFrameNum)) * (mEmitterSrc.mValues[37].GetValueAt(num2, 1f) - 1f) * (mEmitterSrc.mValues[3].GetValueAt(this.mFrameNum) + piparticleInstance.mVariationValues[4]) / 2f * 100f;
					}
					else
					{
						num6 = (theParticleGroup.mWasEmitted ? mEmitterSrc.mValues[13].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[4].GetValueAt(this.mFrameNum)) * (mParticleDef.mValues[18].GetValueAt(num2) - 1f) * (mParticleDef.mValues[4].GetValueAt(this.mFrameNum) + piparticleInstance.mVariationValues[4]) * 100f;
					}
					num6 *= 1f + ((float)this.mFramerate - 100f) * 0.0005f;
					PIParticleInstance piparticleInstance4 = piparticleInstance;
					piparticleInstance4.mVel.Y = piparticleInstance4.mVel.Y + num6 / num;
					Vector2 vector = piparticleInstance.mVel / num;
					if (theParticleGroup.mIsSuperEmitter)
					{
						vector *= mEmitterSrc.mValues[36].GetValueAt(num2, 1f);
					}
					else
					{
						vector *= mParticleDef.mValues[17].GetValueAt(num2);
					}
					Vector2 aPtA = default(Vector2);
					if (!flag && mLayerDef.mDeflectorVector.Count > 0)
					{
						Vector2 aPtA2 = GlobalPIEffect.TransformFPoint(piparticleInstance.mTransform, new Vector2(0f, 0f));
						Vector2 mPos = piparticleInstance.mPos;
						piparticleInstance.mPos += vector;
						this.CalcParticleTransform(theLayer, theEmitterInstance, mEmitterSrc, mParticleDef, theParticleGroup, piparticleInstance);
						aPtA = GlobalPIEffect.TransformFPoint(piparticleInstance.mTransform, new Vector2(0f, 0f));
						for (int i = 0; i < mLayerDef.mDeflectorVector.Count; i++)
						{
							PIDeflector pideflector = mLayerDef.mDeflectorVector[i];
							if (pideflector.mActive.GetLastKeyframe(this.mFrameNum) >= 0.99f)
							{
								for (int j = 1; j < pideflector.mCurPoints.Count; j++)
								{
									Vector2 vector2 = pideflector.mCurPoints[j - 1] - new Vector2(this.mDrawTransform.m02, this.mDrawTransform.m12);
									Vector2 vector3 = pideflector.mCurPoints[j] - new Vector2(this.mDrawTransform.m02, this.mDrawTransform.m12);
									SexyVector2 sexyVector = new SexyVector2(vector3.X - vector2.X, vector3.Y - vector2.Y);
									SexyVector2 sexyVector2 = sexyVector.Normalize().Perp();
									Vector2 vector4;
									vector4 = new Vector2(sexyVector2.x, sexyVector2.y);
									vector4 = vector4 * pideflector.mThickness * piparticleInstance.mThicknessHitVariation;
									Vector2 theIntersectionPoint = default(Vector2);
									float num7 = 0f;
									if (GlobalPIEffect.LineSegmentIntersects(aPtA2, aPtA, vector2 + vector4, vector3 + vector4, ref num7, theIntersectionPoint) && this.GetRandFloatU() <= pideflector.mHits)
									{
										float num8 = pideflector.mBounce;
										if (theParticleGroup.mIsSuperEmitter)
										{
											num8 *= (theParticleGroup.mWasEmitted ? mEmitterSrc.mValues[6].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[7].GetValueAt(this.mFrameNum)) * mEmitterSrc.mValues[40].GetValueAt(num2, 1f) * (mEmitterSrc.mValues[6].GetValueAt(this.mFrameNum) + piparticleInstance.mVariationValues[9]);
										}
										else
										{
											num8 *= (theParticleGroup.mWasEmitted ? mEmitterSrc.mValues[16].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[7].GetValueAt(this.mFrameNum)) * mParticleDef.mValues[21].GetValueAt(num2) * (mParticleDef.mValues[7].GetValueAt(this.mFrameNum) + piparticleInstance.mVariationValues[9]);
										}
										SexyVector2 impliedObject = new SexyVector2(vector.X, vector.Y);
										float t = impliedObject.Dot(sexyVector2);
										SexyVector2 sexyVector3 = impliedObject - sexyVector2 * 2f * t;
										float num9 = Math.Min(1f, Math.Abs(sexyVector3.y / sexyVector3.x));
										sexyVector3.y *= 1f - num9 + num9 * (float)Math.Pow((double)num8, 0.5);
										piparticleInstance.mVel = new Vector2(sexyVector3.x, sexyVector3.y) * 100f;
										if (num8 > 0.001f)
										{
											piparticleInstance.mPos = mPos;
										}
										this.CalcParticleTransform(theLayer, theEmitterInstance, mEmitterSrc, mParticleDef, theParticleGroup, piparticleInstance);
										aPtA = GlobalPIEffect.TransformFPoint(piparticleInstance.mTransform, new Vector2(0f, 0f));
									}
								}
							}
						}
					}
					else
					{
						piparticleInstance.mPos += vector;
						if (mLayerDef.mForceVector.Count > 0)
						{
							this.CalcParticleTransform(theLayer, theEmitterInstance, mEmitterSrc, mParticleDef, theParticleGroup, piparticleInstance);
							aPtA = GlobalPIEffect.TransformFPoint(piparticleInstance.mTransform, new Vector2(0f, 0f));
						}
					}
					for (int k = 0; k < mLayerDef.mForceVector.Count; k++)
					{
						PIForce piforce = mLayerDef.mForceVector[k];
						if (piforce.mActive.GetLastKeyframe(this.mFrameNum) >= 0.99f)
						{
							bool flag2 = false;
							int l = 0;
							int num10 = 3;
							while (l < 4)
							{
								if (((piforce.mCurPoints[l].Y <= aPtA.Y && aPtA.Y < piforce.mCurPoints[num10].Y) || (piforce.mCurPoints[num10].Y <= aPtA.Y && aPtA.Y < piforce.mCurPoints[l].Y)) && aPtA.X < (piforce.mCurPoints[num10].X - piforce.mCurPoints[l].X) * (aPtA.Y - piforce.mCurPoints[l].Y) / (piforce.mCurPoints[num10].Y - piforce.mCurPoints[l].Y) + piforce.mCurPoints[l].X)
								{
									flag2 = !flag2;
								}
								num10 = l++;
							}
							if (flag2)
							{
								float num11 = MathHelper.ToRadians(-piforce.mDirection.GetValueAt(this.mFrameNum)) + MathHelper.ToRadians(-piforce.mAngle.GetValueAt(this.mFrameNum));
								float num12 = 0.085f * (float)this.mFramerate / 100f;
								num12 *= 1f + ((float)this.mFramerate - 100f) * 0.004f;
								float num13 = piforce.mStrength.GetValueAt(this.mFrameNum) * num12;
								PIParticleInstance piparticleInstance5 = piparticleInstance;
								piparticleInstance5.mVel.X = piparticleInstance5.mVel.X + (float)Math.Cos((double)num11) * num13 * 100f;
								PIParticleInstance piparticleInstance6 = piparticleInstance;
								piparticleInstance6.mVel.Y = piparticleInstance6.mVel.Y + (float)Math.Sin((double)num11) * num13 * 100f;
							}
						}
					}
					if (!theParticleGroup.mIsSuperEmitter && mParticleDef.mAngleAlignToMotion && mParticleDef.mAngleKeepAlignedToMotion)
					{
						piparticleInstance.mImgAngle = (float)(-(float)Math.Atan2((double)vector.Y, (double)vector.X)) + MathHelper.ToRadians((float)(-(float)mParticleDef.mAngleAlignOffset));
					}
				}
				else if (mParticleDef.mSingleParticle)
				{
					bool flag3 = false;
					if (mEmitterInstanceDef.mEmitterGeom == 1 || mEmitterInstanceDef.mEmitterGeom == 4)
					{
						flag3 = mEmitterInstanceDef.mEmitAtPointsNum != 0;
					}
					else if (mEmitterInstanceDef.mEmitterGeom == 3)
					{
						flag3 = mEmitterInstanceDef.mEmitAtPointsNum * mEmitterInstanceDef.mEmitAtPointsNum2 != 0;
					}
					if (flag3)
					{
						Vector2 geomPos = this.GetGeomPos(theEmitterInstance, piparticleInstance);
						piparticleInstance.mEmittedPos = this.GetEmitterPos(theEmitterInstance, true);
						piparticleInstance.mLastEmitterPos = piparticleInstance.mEmittedPos;
						piparticleInstance.mOrigPos = geomPos - piparticleInstance.mEmittedPos;
						SexyTransform2D theMatrix = new SexyTransform2D(false);
						theMatrix.RotateDeg(theEmitterInstance.mEmitterInstanceDef.mValues[14].GetValueAt(this.mFrameNum));
						piparticleInstance.mEmittedPos += GlobalPIEffect.TransformFPoint(theMatrix, geomPos);
					}
					if (mParticleDef.mAngleKeepAlignedToMotion && !mParticleDef.mAttachToEmitter)
					{
						Vector2 velocityAt = mEmitterInstanceDef.mPosition.GetVelocityAt(this.mFrameNum);
						if (velocityAt.Length() != 0f)
						{
							piparticleInstance.mImgAngle = (float)(-(float)Math.Atan2((double)velocityAt.Y, (double)velocityAt.X));
						}
						else
						{
							piparticleInstance.mImgAngle = 0f;
						}
						piparticleInstance.mImgAngle += MathHelper.ToRadians((float)(-(float)mParticleDef.mAngleAlignOffset));
					}
				}
				if (mParticleDef != null)
				{
					bool flag4 = (!piparticleInstance.mHasDrawn && mParticleDef.mGetColorFromLayer) || mParticleDef.mUpdateColorFromLayer;
					bool flag5 = (!piparticleInstance.mHasDrawn && mParticleDef.mGetTransparencyFromLayer) || mParticleDef.mUpdateTransparencyFromLayer;
					if (flag4 || flag5)
					{
						Vector2 vector5 = GlobalPIEffect.TransformFPoint(piparticleInstance.mTransform, new Vector2(0f, 0f));
						int num14 = (int)vector5.X + (int)theLayer.mBkgImgDrawOfs.X;
						int num15 = (int)vector5.Y + (int)theLayer.mBkgImgDrawOfs.Y;
						uint num16;
						if (theLayer.mBkgImage != null && num14 >= 0 && num15 >= 0 && num14 < theLayer.mBkgImage.mWidth && num15 < theLayer.mBkgImage.mHeight)
						{
							uint[] bits = theLayer.mBkgImage.GetBits();
							num16 = bits[num14 + num15 * theLayer.mBkgImage.mWidth];
						}
						else
						{
							num16 = 0U;
						}
						if (flag4)
						{
							piparticleInstance.mBkgColor = (piparticleInstance.mBkgColor & 4278190080U) | (num16 & 16777215U);
						}
						if (flag5)
						{
							piparticleInstance.mBkgColor = (piparticleInstance.mBkgColor & 16777215U) | (num16 & 4278190080U);
						}
					}
				}
				if (theParticleGroup.mIsSuperEmitter)
				{
					piparticleInstance.mImgAngle += MathHelper.ToRadians(-((theParticleGroup.mWasEmitted ? mEmitterSrc.mValues[4].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[5].GetValueAt(this.mFrameNum)) * (mEmitterSrc.mValues[38].GetValueAt(num2, 1f) - 1f) * (mEmitterSrc.mValues[4].GetValueAt(this.mFrameNum) + piparticleInstance.mVariationValues[5]))) / num * 160f;
				}
				else if (!mParticleDef.mAngleKeepAlignedToMotion)
				{
					piparticleInstance.mImgAngle += MathHelper.ToRadians(-((theParticleGroup.mWasEmitted ? mEmitterSrc.mValues[14].GetValueAt(this.mFrameNum) : theEmitterInstance.mEmitterInstanceDef.mValues[5].GetValueAt(this.mFrameNum)) * (mParticleDef.mValues[19].GetValueAt(num2) - 1f) * (mParticleDef.mValues[5].GetValueAt(this.mFrameNum) + piparticleInstance.mVariationValues[5]))) / num;
				}
				piparticleInstance = mNext;
			}
		}

		public void DrawParticleGroup(Graphics g, PILayer theLayer, PIEmitterInstance theEmitterInstance, PIParticleGroup theParticleGroup, bool isDarkeningPass)
		{
			if (!theEmitterInstance.mWasActive)
			{
				return;
			}
			PIParticleInstance piparticleInstance = theParticleGroup.mHead;
			while (piparticleInstance != null)
			{
				PIParticleInstance mNext = piparticleInstance.mNext;
				PIParticleDef mParticleDef = piparticleInstance.mParticleDef;
				if ((!mParticleDef.mIntense || !mParticleDef.mPreserveColor) && isDarkeningPass)
				{
					piparticleInstance = mNext;
				}
				else
				{
					if (mParticleDef.mIntense && !isDarkeningPass)
					{
						this.mAdditiveList.Add(piparticleInstance);
					}
					else
					{
						this.mNormalList.Add(piparticleInstance);
						if (isDarkeningPass)
						{
							this.mDarken = true;
						}
					}
					piparticleInstance = mNext;
				}
			}
		}

		public void DrawLayerNormal(Graphics g, PILayer theLayer)
		{
			g.PushState();
			g.SetColorizeImages(true);
			PILayerDef mLayerDef = theLayer.mLayerDef;
			for (int i = 0; i < theLayer.mEmitterInstanceVector.Count; i++)
			{
				PIEmitterInstanceDef piemitterInstanceDef = mLayerDef.mEmitterInstanceDefVector[i];
				PIEmitterInstance piemitterInstance = theLayer.mEmitterInstanceVector[i];
				if (piemitterInstance.mVisible)
				{
					if (piemitterInstanceDef.mIsSuperEmitter)
					{
						for (int j = 0; j < piemitterInstanceDef.mFreeEmitterIndices.Count; j++)
						{
							for (PIFreeEmitterInstance pifreeEmitterInstance = (PIFreeEmitterInstance)piemitterInstance.mSuperEmitterGroup.mHead; pifreeEmitterInstance != null; pifreeEmitterInstance = (PIFreeEmitterInstance)pifreeEmitterInstance.mNext)
							{
								this.DrawParticleGroupNomal(g, theLayer, piemitterInstance, pifreeEmitterInstance.mEmitter.mParticleGroup, this.mDarken);
							}
						}
					}
					else
					{
						this.DrawParticleGroupNomal(g, theLayer, piemitterInstance, piemitterInstance.mParticleGroup, this.mDarken);
					}
				}
			}
			g.PopState();
		}

		public void DrawLayerAdditive(Graphics g, PILayer theLayer)
		{
			g.PushState();
			g.SetColorizeImages(true);
			PILayerDef mLayerDef = theLayer.mLayerDef;
			for (int i = 0; i < theLayer.mEmitterInstanceVector.Count; i++)
			{
				PIEmitterInstanceDef piemitterInstanceDef = mLayerDef.mEmitterInstanceDefVector[i];
				PIEmitterInstance piemitterInstance = theLayer.mEmitterInstanceVector[i];
				if (piemitterInstance.mVisible)
				{
					if (piemitterInstanceDef.mIsSuperEmitter)
					{
						for (int j = 0; j < piemitterInstanceDef.mFreeEmitterIndices.Count; j++)
						{
							for (PIFreeEmitterInstance pifreeEmitterInstance = (PIFreeEmitterInstance)piemitterInstance.mSuperEmitterGroup.mHead; pifreeEmitterInstance != null; pifreeEmitterInstance = (PIFreeEmitterInstance)pifreeEmitterInstance.mNext)
							{
								this.DrawParticleGroupAdditive(g, theLayer, piemitterInstance, pifreeEmitterInstance.mEmitter.mParticleGroup, false);
							}
						}
					}
					else
					{
						this.DrawParticleGroupAdditive(g, theLayer, piemitterInstance, piemitterInstance.mParticleGroup, false);
					}
				}
			}
			g.PopState();
		}

		public void DrawParticleGroupNomal(Graphics g, PILayer theLayer, PIEmitterInstance theEmitterInstance, PIParticleGroup theParticleGroup, bool isDarkeningPass)
		{
			SexyColor theColor = new SexyColor(this.mColor.mRed * theLayer.mColor.mRed / 255, this.mColor.mGreen * theLayer.mColor.mGreen / 255, this.mColor.mBlue * theLayer.mColor.mBlue / 255, this.mColor.mAlpha * theLayer.mColor.mAlpha / 255);
			bool flag = theColor != SexyColor.White;
			int count = this.mNormalList.Count;
			g.SetDrawMode(0);
			for (int i = 0; i < count; i += this.mOptimizeValue)
			{
				PIParticleInstance piparticleInstance = this.mNormalList[i];
				PIParticleDef mParticleDef = piparticleInstance.mParticleDef;
				float mLifePct = piparticleInstance.mLifePct;
				PIEmitter mEmitterSrc = piparticleInstance.mEmitterSrc;
				PITexture pitexture = this.mDef.mTextureVector[mParticleDef.mTextureIdx];
				DeviceImage deviceImage;
				Rect celRect;
				if (pitexture.mImageVector.Count != 0)
				{
					deviceImage = pitexture.mImageVector[piparticleInstance.mImgIdx].GetDeviceImage();
					celRect = new Rect(0, 0, deviceImage.mWidth, deviceImage.mHeight);
				}
				else
				{
					deviceImage = pitexture.mImageStrip.GetDeviceImage();
					celRect = deviceImage.GetCelRect(piparticleInstance.mImgIdx);
				}
				int num;
				if (mParticleDef.mRandomGradientColor)
				{
					if (mParticleDef.mUseKeyColorsOnly)
					{
						int theIdx = (int)Math.Min(piparticleInstance.mGradientRand * (float)mParticleDef.mColor.mInterpolatorPointVector.Count, (float)(mParticleDef.mColor.mInterpolatorPointVector.Count - 1));
						num = mParticleDef.mColor.GetKeyframeNum(theIdx);
					}
					else
					{
						float mGradientRand = piparticleInstance.mGradientRand;
						num = mParticleDef.mColor.GetValueAt(mGradientRand);
					}
				}
				else if (mParticleDef.mUseNextColorKey)
				{
					int theIdx2 = piparticleInstance.mNum / mParticleDef.mNumberOfEachColor % mParticleDef.mColor.mInterpolatorPointVector.Count;
					num = mParticleDef.mColor.GetKeyframeNum(theIdx2);
				}
				else
				{
					float theTime = GlobalPIEffect.WrapFloat(mLifePct, 1 + mParticleDef.mRepeatColor);
					num = mParticleDef.mColor.GetValueAt(theTime);
				}
				if (mParticleDef.mGetColorFromLayer)
				{
					num = (num & -16777216) | (int)(piparticleInstance.mBkgColor & 16777215U);
				}
				if (mParticleDef.mGetTransparencyFromLayer)
				{
					num = (num & 16777215) | (int)(piparticleInstance.mBkgColor & 4278190080U);
				}
				float thePct = theEmitterInstance.mEmitterInstanceDef.mValues[10].GetValueAt(this.mFrameNum) * mEmitterSrc.mValues[20].GetValueAt(this.mFrameNum, 1f);
				num = (int)GlobalPIEffect.InterpColor(num, theEmitterInstance.mTintColor.ToInt(), thePct);
				int num2 = mParticleDef.mAlpha.GetValueAt(GlobalPIEffect.WrapFloat(mLifePct, 1 + mParticleDef.mRepeatAlpha));
				num2 = (int)((float)num2 * (theEmitterInstance.mEmitterInstanceDef.mValues[9].GetValueAt(this.mFrameNum) * mParticleDef.mValues[22].GetValueAt(this.mFrameNum) * mEmitterSrc.mValues[18].GetValueAt(this.mFrameNum, 1f)));
				if (isDarkeningPass)
				{
					num = (int)((long)num & (long)((-16777216)));
				}
				num &= 16777215;
				num |= num2 << 24;
				SexyColor color = new SexyColor((num >> 16) & 255, (num >> 8) & 255, num & 255, (num >> 24) & 255);
				if (flag)
				{
					color = new SexyColor(color.mRed * theColor.mRed / 255, color.mGreen * theColor.mGreen / 255, color.mBlue * theColor.mBlue / 255, color.mAlpha * theColor.mAlpha / 255);
				}
				if (color.mAlpha != 0)
				{
					g.SetColor(color);
					this.CalcParticleTransform(theLayer, theEmitterInstance, mEmitterSrc, mParticleDef, theParticleGroup, piparticleInstance);
					SexyTransform2D sexyTransform2D = this.mDrawTransform * piparticleInstance.mTransform;
					g.DrawImageMatrix(deviceImage, sexyTransform2D, celRect);
					piparticleInstance.mHasDrawn = true;
				}
			}
		}

		public void DrawParticleGroupAdditive(Graphics g, PILayer theLayer, PIEmitterInstance theEmitterInstance, PIParticleGroup theParticleGroup, bool isDarkeningPass)
		{
			SexyColor theColor = new SexyColor(this.mColor.mRed * theLayer.mColor.mRed / 255, this.mColor.mGreen * theLayer.mColor.mGreen / 255, this.mColor.mBlue * theLayer.mColor.mBlue / 255, this.mColor.mAlpha * theLayer.mColor.mAlpha / 255);
			bool flag = theColor != SexyColor.White;
			g.SetDrawMode(1);
			int count = this.mAdditiveList.Count;
			for (int i = 0; i < count; i += this.mOptimizeValue)
			{
				PIParticleInstance piparticleInstance = this.mAdditiveList[i];
				PIParticleDef mParticleDef = piparticleInstance.mParticleDef;
				float mLifePct = piparticleInstance.mLifePct;
				PIEmitter mEmitterSrc = piparticleInstance.mEmitterSrc;
				PITexture pitexture = this.mDef.mTextureVector[mParticleDef.mTextureIdx];
				DeviceImage deviceImage;
				Rect celRect;
				if (pitexture.mImageVector.Count != 0)
				{
					deviceImage = pitexture.mImageVector[piparticleInstance.mImgIdx].GetDeviceImage();
					celRect = new Rect(0, 0, deviceImage.mWidth, deviceImage.mHeight);
				}
				else
				{
					deviceImage = pitexture.mImageStrip.GetDeviceImage();
					celRect = deviceImage.GetCelRect(piparticleInstance.mImgIdx);
				}
				int num;
				if (mParticleDef.mRandomGradientColor)
				{
					if (mParticleDef.mUseKeyColorsOnly)
					{
						int theIdx = (int)Math.Min(piparticleInstance.mGradientRand * (float)mParticleDef.mColor.mInterpolatorPointVector.Count, (float)(mParticleDef.mColor.mInterpolatorPointVector.Count - 1));
						num = mParticleDef.mColor.GetKeyframeNum(theIdx);
					}
					else
					{
						float mGradientRand = piparticleInstance.mGradientRand;
						num = mParticleDef.mColor.GetValueAt(mGradientRand);
					}
				}
				else if (mParticleDef.mUseNextColorKey)
				{
					int theIdx2 = piparticleInstance.mNum / mParticleDef.mNumberOfEachColor % mParticleDef.mColor.mInterpolatorPointVector.Count;
					num = mParticleDef.mColor.GetKeyframeNum(theIdx2);
				}
				else
				{
					float theTime = GlobalPIEffect.WrapFloat(mLifePct, 1 + mParticleDef.mRepeatColor);
					num = mParticleDef.mColor.GetValueAt(theTime);
				}
				if (mParticleDef.mGetColorFromLayer)
				{
					num = (num & -16777216) | (int)(piparticleInstance.mBkgColor & 16777215U);
				}
				if (mParticleDef.mGetTransparencyFromLayer)
				{
					num = (num & 16777215) | (int)(piparticleInstance.mBkgColor & 4278190080U);
				}
				float thePct = theEmitterInstance.mEmitterInstanceDef.mValues[10].GetValueAt(this.mFrameNum) * mEmitterSrc.mValues[20].GetValueAt(this.mFrameNum, 1f);
				num = (int)GlobalPIEffect.InterpColor(num, theEmitterInstance.mTintColor.ToInt(), thePct);
				int num2 = mParticleDef.mAlpha.GetValueAt(GlobalPIEffect.WrapFloat(mLifePct, 1 + mParticleDef.mRepeatAlpha));
				num2 = (int)((float)num2 * (theEmitterInstance.mEmitterInstanceDef.mValues[9].GetValueAt(this.mFrameNum) * mParticleDef.mValues[22].GetValueAt(this.mFrameNum) * mEmitterSrc.mValues[18].GetValueAt(this.mFrameNum, 1f)));
				if (isDarkeningPass)
				{
					num = (int)((long)num & (long)((-16777216)));
				}
				num &= 16777215;
				num |= num2 << 24;
				SexyColor color = new SexyColor((num >> 16) & 255, (num >> 8) & 255, num & 255, (num >> 24) & 255);
				if (flag)
				{
					color = new SexyColor(color.mRed * theColor.mRed / 255, color.mGreen * theColor.mGreen / 255, color.mBlue * theColor.mBlue / 255, color.mAlpha * theColor.mAlpha / 255);
				}
				if (color.mAlpha != 0)
				{
					g.SetColor(color);
					this.CalcParticleTransform(theLayer, theEmitterInstance, mEmitterSrc, mParticleDef, theParticleGroup, piparticleInstance);
					SexyTransform2D sexyTransform2D = this.mDrawTransform * piparticleInstance.mTransform;
					g.DrawImageMatrix(deviceImage, sexyTransform2D, celRect);
					piparticleInstance.mHasDrawn = true;
				}
			}
		}

		public PIEffect()
		{
			this.mLoaded = false;
			this.mFileIdx = 0;
			this.mAutoPadImages = true;
			this.mFrameNum = 0f;
			this.mUpdateCnt = 0;
			this.mCurNumParticles = 0;
			this.mCurNumEmitters = 0;
			this.mLastDrawnPixelCount = 0;
			this.mFirstFrameNum = 0;
			this.mLastFrameNum = 0;
			this.mAnimSpeed = 1f;
			this.mColor = SexyColor.White;
			this.mDebug = false;
			this.mDrawBlockers = true;
			this.mEmitAfterTimeline = false;
			this.mDrawTransform.LoadIdentity();
			this.mEmitterTransform.LoadIdentity();
			this.mPoolSize = 256;
			this.mParticlePool = new ObjectPool<PIParticleInstance>(this.mPoolSize);
			this.mFreeEmitterPool = new ObjectPool<PIFreeEmitterInstance>(this.mPoolSize);
			this.mNormalList = new List<PIParticleInstance>();
			this.mAdditiveList = new List<PIParticleInstance>();
			this.mDef = new PIEffectDef();
		}

		public PIEffect(int poolSize)
		{
			this.mLoaded = false;
			this.mFileIdx = 0;
			this.mAutoPadImages = true;
			this.mFrameNum = 0f;
			this.mUpdateCnt = 0;
			this.mCurNumParticles = 0;
			this.mCurNumEmitters = 0;
			this.mLastDrawnPixelCount = 0;
			this.mFirstFrameNum = 0;
			this.mLastFrameNum = 0;
			this.mAnimSpeed = 1f;
			this.mColor = SexyColor.White;
			this.mDebug = false;
			this.mDrawBlockers = true;
			this.mEmitAfterTimeline = false;
			this.mDrawTransform.LoadIdentity();
			this.mEmitterTransform.LoadIdentity();
			this.mPoolSize = poolSize;
			this.mParticlePool = new ObjectPool<PIParticleInstance>(poolSize);
			this.mFreeEmitterPool = new ObjectPool<PIFreeEmitterInstance>(poolSize);
			this.mNormalList = new List<PIParticleInstance>();
			this.mAdditiveList = new List<PIParticleInstance>();
			this.mDef = new PIEffectDef();
		}

		public PIEffect(PIEffect rhs)
		{
			this.mFileChecksum = rhs.mFileChecksum;
			this.mSrcFileName = rhs.mSrcFileName;
			this.mVersion = rhs.mVersion;
			this.mStartupState = rhs.mStartupState;
			this.mNotes = rhs.mNotes;
			this.mWidth = rhs.mWidth;
			this.mHeight = rhs.mHeight;
			this.mBkgColor = rhs.mBkgColor;
			this.mFramerate = rhs.mFramerate;
			this.mFirstFrameNum = rhs.mFirstFrameNum;
			this.mLastFrameNum = rhs.mLastFrameNum;
			this.mNotesParams = rhs.mNotesParams;
			this.mLastLifePct = rhs.mLastLifePct;
			this.mError = rhs.mError;
			this.mLoaded = rhs.mLoaded;
			this.mAnimSpeed = rhs.mAnimSpeed;
			this.mColor = rhs.mColor;
			this.mDebug = rhs.mDebug;
			this.mDrawBlockers = rhs.mDrawBlockers;
			this.mEmitAfterTimeline = rhs.mEmitAfterTimeline;
			this.mRandSeeds = rhs.mRandSeeds;
			this.mDrawTransform.CopyFrom(rhs.mDrawTransform);
			this.mEmitterTransform.CopyFrom(rhs.mEmitterTransform);
			this.mFileIdx = 0;
			this.mFrameNum = 0f;
			this.mUpdateCnt = 0;
			this.mIsNewFrame = false;
			this.mHasEmitterTransform = false;
			this.mHasDrawTransform = false;
			this.mDrawTransformSimple = false;
			this.mCurNumParticles = 0;
			this.mCurNumEmitters = 0;
			this.mLastDrawnPixelCount = 0;
			this.mDef = rhs.mDef;
			this.mDef.mRefCount++;
			this.mPoolSize = rhs.mPoolSize;
			this.mParticlePool = new ObjectPool<PIParticleInstance>(this.mPoolSize);
			this.mFreeEmitterPool = new ObjectPool<PIFreeEmitterInstance>(this.mPoolSize);
			this.mNormalList = new List<PIParticleInstance>();
			this.mAdditiveList = new List<PIParticleInstance>();
			this.mLayerVector.Resize(rhs.mDef.mLayerDefVector.Count);
			this.mDef.mLayerDefVector.Resize(rhs.mDef.mLayerDefVector.Count);
			for (int i = 0; i < this.mLayerVector.Count; i++)
			{
				PILayerDef pilayerDef = this.mDef.mLayerDefVector[i];
				PILayer pilayer = this.mLayerVector[i];
				pilayer.mLayerDef = pilayerDef;
				pilayer.mEmitterInstanceVector.Resize(pilayerDef.mEmitterInstanceDefVector.Count);
				for (int j = 0; j < pilayerDef.mEmitterInstanceDefVector.Count; j++)
				{
					PIEmitterInstance piemitterInstance = rhs.mLayerVector[i].mEmitterInstanceVector[j];
					PIEmitterInstanceDef piemitterInstanceDef = pilayerDef.mEmitterInstanceDefVector[j];
					PIEmitterInstance piemitterInstance2 = pilayer.mEmitterInstanceVector[j];
					PIEmitter piemitter = this.mDef.mEmitterVector[piemitterInstanceDef.mEmitterDefIdx];
					piemitterInstance2.mEmitterInstanceDef = piemitterInstanceDef;
					piemitterInstance2.mTintColor = new SexyColor(piemitterInstance.mTintColor);
					piemitterInstance2.mParticleDefInstanceVector.Resize(piemitter.mParticleDefVector.Count);
					piemitterInstance2.mSuperEmitterParticleDefInstanceVector.Resize(piemitterInstance.mSuperEmitterParticleDefInstanceVector.Count);
				}
			}
			this.ResetAnim();
		}

		public virtual void Dispose()
		{
			this.ResetAnim();
			this.Deref();
		}

		public PIEffect Duplicate()
		{
			return new PIEffect(this);
		}

		public virtual SharedImageRef GetImage(string theName, string theFilename)
		{
			return GlobalMembers.gSexyAppBase.GetSharedImage(Common.GetPathFrom(theFilename, Common.GetFileDir(this.mSrcFileName, true)));
		}

		public virtual void SetImageOpts(DeviceImage theImage)
		{
		}

		public virtual string WriteImage(string theName, int theIdx, DeviceImage theImage)
		{
			return this.WriteImage(theName, theIdx, null);
		}

		public virtual string WriteImage(string theName, int theIdx, DeviceImage theImage, int hasPadding)
		{
			throw new NotImplementedException();
		}

		public bool LoadEffect(string theFileName)
		{
			if (this.mDef.mRefCount > 1)
			{
				this.Deref();
			}
			this.Clear();
			this.mVersion = 0;
			this.mFileChecksum = 0;
			this.mSrcFileName = theFileName;
			this.mReadBuffer = new SexyBuffer();
			if (!GlobalMembers.gSexyAppBase.ReadBufferFromStream(theFileName, ref this.mReadBuffer))
			{
				return this.Fail("Unable to open file: " + theFileName);
			}
			this.mIsPPF = true;
			this.mBufPos = 0;
			this.mChecksumPos = GlobalPIEffect.PI_BUFSIZE;
			this.ReadString();
			if (this.mIsPPF)
			{
				this.mVersion = this.mReadBuffer.ReadInt32();
			}
			if (this.mVersion < 0)
			{
				this.Fail("PPF version too old");
			}
			this.mNotes = this.ReadString();
			short num = this.mReadBuffer.ReadShort();
			for (int i = 0; i < (int)num; i++)
			{
				this.ExpectCmd("CMultiTexture");
				PITexture pitexture = new PITexture();
				pitexture.mName = this.ReadString();
				short num2 = this.mReadBuffer.ReadShort();
				pitexture.mNumCels = (int)num2;
				if (!this.mIsPPF)
				{
					throw new NotImplementedException();
				}
				short num3 = this.mReadBuffer.ReadShort();
				pitexture.mPadded = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
				string text = this.ReadString();
				pitexture.mFileName = text;
				pitexture.mImageStrip = this.GetImage(pitexture.mName, text);
				if (pitexture.mImageStrip.GetDeviceImage() == null)
				{
					this.Fail("Unable to load image: " + text);
				}
				else if (pitexture.mImageStrip.GetDeviceImage().mNumCols == 1 && pitexture.mImageStrip.GetDeviceImage().mNumRows == 1)
				{
					pitexture.mImageStrip.GetDeviceImage().mNumCols = (int)(num2 / num3);
					pitexture.mImageStrip.GetDeviceImage().mNumRows = (int)num3;
				}
				this.mDef.mTextureVector.Add(pitexture);
			}
			short num4 = this.mReadBuffer.ReadShort();
			this.mDef.mEmitterVector.Capacity = (int)num4;
			this.mDef.mEmitterVector.Resize((int)num4);
			for (int j = 0; j < (int)num4; j++)
			{
				this.ExpectCmd("CEmitterType");
				if (!this.mIsPPF)
				{
					this.mDef.mEmitterRefMap.Add(this.mStringVector.Count, j);
				}
				this.ReadEmitterType(this.mDef.mEmitterVector[j]);
			}
			List<bool> list = new List<bool>();
			list.Capacity = this.mDef.mEmitterVector.Count;
			list.Resize(this.mDef.mEmitterVector.Count);
			List<bool> list2 = new List<bool>();
			list2.Capacity = this.mDef.mTextureVector.Count;
			list2.Resize(this.mDef.mTextureVector.Count);
			short num5 = this.mReadBuffer.ReadShort();
			this.mLayerVector.Resize((int)num5);
			this.mDef.mLayerDefVector.Resize((int)num5);
			for (int k = 0; k < (int)num5; k++)
			{
				PILayerDef pilayerDef = this.mDef.mLayerDefVector[k];
				PILayer pilayer = this.mLayerVector[k];
				pilayer.mLayerDef = pilayerDef;
				this.ExpectCmd("CLayer");
				pilayerDef.mName = this.ReadString();
				num4 = this.mReadBuffer.ReadShort();
				pilayer.mEmitterInstanceVector.Capacity = (int)num4;
				pilayer.mEmitterInstanceVector.Resize((int)num4);
				pilayerDef.mEmitterInstanceDefVector.Capacity = (int)num4;
				pilayerDef.mEmitterInstanceDefVector.Resize((int)num4);
				for (int l = 0; l < (int)num4; l++)
				{
					PIEmitterInstanceDef piemitterInstanceDef = pilayerDef.mEmitterInstanceDefVector[l];
					PIEmitterInstance piemitterInstance = pilayer.mEmitterInstanceVector[l];
					piemitterInstance.mEmitterInstanceDef = piemitterInstanceDef;
					this.ExpectCmd("CEmitter");
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					piemitterInstanceDef.mFramesToPreload = this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					piemitterInstanceDef.mName = this.ReadString();
					piemitterInstanceDef.mEmitterGeom = this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
					bool flag = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
					if (flag && piemitterInstanceDef.mEmitterGeom == 2)
					{
						piemitterInstanceDef.mEmitterGeom = 4;
					}
					piemitterInstanceDef.mEmitIn = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
					piemitterInstanceDef.mEmitOut = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
					uint num6 = (uint)(((int)this.mReadBuffer.ReadByte() << 16) | -16777216);
					this.mReadBuffer.ReadByte();
					this.mReadBuffer.ReadByte();
					this.mReadBuffer.ReadByte();
					num6 |= (uint)((uint)this.mReadBuffer.ReadByte() << 8);
					this.mReadBuffer.ReadByte();
					this.mReadBuffer.ReadByte();
					this.mReadBuffer.ReadByte();
					num6 |= (uint)this.mReadBuffer.ReadByte();
					this.mReadBuffer.ReadByte();
					this.mReadBuffer.ReadByte();
					this.mReadBuffer.ReadByte();
					piemitterInstance.mTintColor = new SexyColor((int)num6);
					this.mReadBuffer.ReadInt32();
					piemitterInstanceDef.mEmitAtPointsNum = this.mReadBuffer.ReadInt32();
					piemitterInstanceDef.mEmitterDefIdx = this.mReadBuffer.ReadInt32();
					list[piemitterInstanceDef.mEmitterDefIdx] = true;
					PIEmitter piemitter = this.mDef.mEmitterVector[piemitterInstanceDef.mEmitterDefIdx];
					piemitterInstance.mParticleDefInstanceVector.Resize(piemitter.mParticleDefVector.Count);
					for (int m = 0; m < piemitter.mParticleDefVector.Count; m++)
					{
						list2[piemitter.mParticleDefVector[m].mTextureIdx] = true;
					}
					this.ReadValue2D(piemitterInstanceDef.mPosition);
					int num7 = (int)this.mReadBuffer.ReadShort();
					for (int n = 0; n < num7; n++)
					{
						this.ExpectCmd("CEPoint");
						this.mReadBuffer.ReadFloat();
						this.mReadBuffer.ReadFloat();
						PIValue2D pivalue2D = new PIValue2D();
						this.ReadEPoint(pivalue2D);
						piemitterInstanceDef.mPoints.Add(pivalue2D);
					}
					for (int num8 = 0; num8 < 17; num8++)
					{
						this.ReadValue(ref piemitterInstanceDef.mValues[num8]);
					}
					piemitterInstanceDef.mEmitAtPointsNum2 = this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					this.ReadValue(ref piemitterInstanceDef.mValues[17]);
					this.mReadBuffer.ReadInt32();
					this.ReadValue(ref piemitterInstanceDef.mValues[18]);
					short num9 = this.mReadBuffer.ReadShort();
					string theFilename = "";
					for (int num10 = 0; num10 < (int)num9; num10++)
					{
						theFilename = this.ReadString();
					}
					bool flag2 = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
					string theName = this.ReadString();
					if (flag2)
					{
						piemitterInstance.mMaskImage = this.GetImage(theName, theFilename);
					}
					this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					piemitterInstanceDef.mInvertMask = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
					this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					piemitterInstanceDef.mIsSuperEmitter = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
					int num11 = (int)this.mReadBuffer.ReadShort();
					for (int num12 = 0; num12 < num11; num12++)
					{
						if (!this.mIsPPF)
						{
							throw new NotImplementedException();
						}
						int num13 = (int)this.mReadBuffer.ReadShort();
						piemitterInstanceDef.mFreeEmitterIndices.Add(num13);
						list[l] = true;
					}
					piemitterInstance.mSuperEmitterParticleDefInstanceVector.Resize(num11);
					this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadFloat();
					this.mReadBuffer.ReadFloat();
				}
				short num14 = this.mReadBuffer.ReadShort();
				for (int num15 = 0; num15 < (int)num14; num15++)
				{
					PIDeflector pideflector = new PIDeflector();
					this.ExpectCmd("CDeflector");
					pideflector.mName = this.ReadString();
					pideflector.mBounce = (float)this.mReadBuffer.ReadInt32();
					pideflector.mHits = (float)this.mReadBuffer.ReadInt32();
					pideflector.mThickness = (float)this.mReadBuffer.ReadInt32();
					pideflector.mVisible = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
					this.ReadValue2D(pideflector.mPos);
					int num16 = (int)this.mReadBuffer.ReadShort();
					for (int num17 = 0; num17 < num16; num17++)
					{
						this.ExpectCmd("CEPoint");
						this.mReadBuffer.ReadFloat();
						this.mReadBuffer.ReadFloat();
						PIValue2D pivalue2D2 = new PIValue2D();
						this.ReadEPoint(pivalue2D2);
						pideflector.mPoints.Add(pivalue2D2);
					}
					pideflector.mCurPoints.Resize(pideflector.mPoints.Count);
					this.ReadValue(ref pideflector.mActive);
					this.ReadValue(ref pideflector.mAngle);
					pilayerDef.mDeflectorVector.Add(pideflector);
				}
				short num18 = this.mReadBuffer.ReadShort();
				for (int num19 = 0; num19 < (int)num18; num19++)
				{
					PIBlocker piblocker = new PIBlocker();
					this.ExpectCmd("CBlocker");
					piblocker.mName = this.ReadString();
					this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					this.mReadBuffer.ReadInt32();
					this.ReadValue2D(piblocker.mPos);
					int num20 = (int)this.mReadBuffer.ReadShort();
					for (int num21 = 0; num21 < num20; num21++)
					{
						this.ExpectCmd("CEPoint");
						this.mReadBuffer.ReadFloat();
						this.mReadBuffer.ReadFloat();
						PIValue2D pivalue2D3 = new PIValue2D();
						this.ReadEPoint(pivalue2D3);
						piblocker.mPoints.Add(pivalue2D3);
					}
					this.ReadValue(ref piblocker.mActive);
					this.ReadValue(ref piblocker.mAngle);
					pilayerDef.mBlockerVector.Add(piblocker);
				}
				this.ReadValue2D(pilayerDef.mOffset);
				pilayerDef.mOrigOffset = pilayerDef.mOffset.GetValueAt(0f);
				this.ReadValue(ref pilayerDef.mAngle);
				this.ReadString();
				for (int num22 = 0; num22 < 32; num22++)
				{
					this.mReadBuffer.ReadByte();
				}
				int num23 = (int)this.mReadBuffer.ReadShort();
				for (int num24 = 0; num24 < num23; num24++)
				{
					this.ReadString();
				}
				for (int num25 = 0; num25 < 36; num25++)
				{
					this.mReadBuffer.ReadByte();
				}
				short num26 = this.mReadBuffer.ReadShort();
				for (int num27 = 0; num27 < (int)num26; num27++)
				{
					this.ExpectCmd("CForce");
					PIForce piforce = new PIForce();
					piforce.mName = this.ReadString();
					piforce.mVisible = (this.mIsPPF ? (this.mReadBuffer.ReadByte() != 0) : (this.mReadBuffer.ReadInt32() != 0));
					this.ReadValue2D(piforce.mPos);
					this.ReadValue(ref piforce.mActive);
					PIValue pivalue = new PIValue();
					this.ReadValue(ref pivalue);
					this.ReadValue(ref piforce.mStrength);
					this.ReadValue(ref piforce.mWidth);
					this.ReadValue(ref piforce.mHeight);
					this.ReadValue(ref piforce.mAngle);
					this.ReadValue(ref piforce.mDirection);
					pilayerDef.mForceVector.Add(piforce);
				}
				for (int num28 = 0; num28 < 28; num28++)
				{
					this.mReadBuffer.ReadByte();
				}
			}
			List<int> list3 = new List<int>();
			list3.Resize(this.mDef.mEmitterVector.Count);
			int num29 = 0;
			for (int num30 = 0; num30 < this.mDef.mEmitterVector.Count; num30++)
			{
				if (list[num30])
				{
					list3[num30] = num29++;
				}
			}
			int num31 = 0;
			int num32 = 0;
			for (int num33 = 0; num33 < list.Count; num33++)
			{
				if (!list[num31])
				{
					this.mDef.mEmitterVector.RemoveAt(num32);
				}
				else
				{
					num32++;
				}
				num31++;
			}
			for (int num34 = 0; num34 < this.mDef.mLayerDefVector.Count; num34++)
			{
				PILayerDef pilayerDef2 = this.mDef.mLayerDefVector[num34];
				for (int num35 = 0; num35 < pilayerDef2.mEmitterInstanceDefVector.Count; num35++)
				{
					PIEmitterInstanceDef piemitterInstanceDef2 = pilayerDef2.mEmitterInstanceDefVector[num35];
					piemitterInstanceDef2.mEmitterDefIdx = list3[piemitterInstanceDef2.mEmitterDefIdx];
					for (int num36 = 0; num36 < piemitterInstanceDef2.mFreeEmitterIndices.Count; num36++)
					{
						piemitterInstanceDef2.mFreeEmitterIndices[num36] = list3[piemitterInstanceDef2.mFreeEmitterIndices[num36]];
					}
				}
			}
			List<int> list4 = new List<int>();
			list4.Resize(this.mDef.mTextureVector.Count);
			int num37 = 0;
			for (int num38 = 0; num38 < this.mDef.mTextureVector.Count; num38++)
			{
				if (list2[num38])
				{
					list4[num38] = num37++;
				}
			}
			num31 = 0;
			num32 = 0;
			for (int num39 = 0; num39 < list2.Count; num39++)
			{
				if (!list2[num31])
				{
					this.mDef.mTextureVector.RemoveAt(num32);
				}
				else
				{
					num32++;
				}
				num31++;
			}
			for (int num40 = 0; num40 < this.mDef.mEmitterVector.Count; num40++)
			{
				PIEmitter piemitter2 = this.mDef.mEmitterVector[num40];
				for (int num41 = 0; num41 < piemitter2.mParticleDefVector.Count; num41++)
				{
					PIParticleDef piparticleDef = piemitter2.mParticleDefVector[num41];
					piparticleDef.mTextureIdx = list4[piparticleDef.mTextureIdx];
				}
			}
			uint num42 = (uint)(((int)this.mReadBuffer.ReadByte() << 16) | -16777216);
			this.mReadBuffer.ReadByte();
			this.mReadBuffer.ReadByte();
			this.mReadBuffer.ReadByte();
			num42 |= (uint)((uint)this.mReadBuffer.ReadByte() << 8);
			this.mReadBuffer.ReadByte();
			this.mReadBuffer.ReadByte();
			this.mReadBuffer.ReadByte();
			num42 |= (uint)this.mReadBuffer.ReadByte();
			this.mReadBuffer.ReadByte();
			this.mReadBuffer.ReadByte();
			this.mReadBuffer.ReadByte();
			this.mBkgColor = new SexyColor((int)num42);
			this.mReadBuffer.ReadInt32();
			this.mReadBuffer.ReadInt32();
			this.mFramerate = (int)this.mReadBuffer.ReadShort();
			this.mReadBuffer.ReadShort();
			this.mReadBuffer.ReadShort();
			this.mReadBuffer.ReadShort();
			this.mWidth = this.mReadBuffer.ReadInt32();
			this.mHeight = this.mReadBuffer.ReadInt32();
			this.mReadBuffer.ReadInt32();
			this.mReadBuffer.ReadInt32();
			this.mReadBuffer.ReadInt32();
			this.mReadBuffer.ReadInt32();
			this.mReadBuffer.ReadInt32();
			this.mFirstFrameNum = this.mReadBuffer.ReadInt32();
			this.mLastFrameNum = this.mReadBuffer.ReadInt32();
			this.ReadString();
			this.mReadBuffer.ReadByte();
			this.mReadBuffer.ReadShort();
			this.mReadBuffer.ReadShort();
			if (this.mIsPPF && this.mVersion >= 1)
			{
				int num43 = this.mReadBuffer.ReadInt32();
				if (num43 > 0)
				{
					this.mStartupState.mData.Clear();
					this.mStartupState.mDataBitSize = num43 * 8;
					byte[] array = new byte[num43];
					this.mReadBuffer.ReadBytes(ref array, num43);
					this.mStartupState.mData.AddRange(array);
					array = null;
				}
			}
			else
			{
				this.mStartupState.Clear();
			}
			int num44 = 0;
			while (num44 < this.mNotes.Length)
			{
				int num45 = this.mNotes.IndexOf('\n', num44);
				string text2;
				if (num45 != -1)
				{
					text2 = this.mNotes.Substring(num44, num45 - num44).Trim();
					num44 = num45 + 1;
				}
				else
				{
					text2 = this.mNotes.Substring(num44).Trim();
					num44 = this.mNotes.Length;
				}
				if (text2.Length > 0)
				{
					int num46 = text2.IndexOf(':');
					if (num46 != -1)
					{
						this.mNotesParams.Add(text2.Substring(0, num46).ToUpper(), text2.Substring(num46 + 1).Trim());
					}
					else
					{
						this.mNotesParams.Add(text2.ToUpper(), "");
					}
				}
			}
			string notesParam = this.GetNotesParam("Rand");
			int num48;
			for (int num47 = 0; num47 < notesParam.Length; num47 = num48 + 1)
			{
				num48 = notesParam.IndexOf(',', num47);
				if (num48 == -1)
				{
					this.mRandSeeds.Add(Convert.ToInt32(notesParam.Substring(num47).Trim()));
					break;
				}
				this.mRandSeeds.Add(Convert.ToInt32(notesParam.Substring(num47, num48 - num47).Trim()));
			}
			this.mEmitAfterTimeline = this.GetNotesParam("EmitAfter", "no") != "no";
			if (this.mError.Length == 0 && !GlobalMembers.gSexyAppBase.mReloadingResources)
			{
				this.WriteToCache();
			}
			return this.mLoaded = this.mError.Length == 0;
		}

		public void RefreshImageRes()
		{
			for (int i = 0; i < this.mDef.mTextureVector.Count; i++)
			{
				PITexture pitexture = this.mDef.mTextureVector[i];
				pitexture.mImageStrip = (pitexture.mImageStrip = this.GetImage(pitexture.mName, pitexture.mFileName));
			}
		}

		public bool SaveAsPPF(string theFileName)
		{
			return this.SaveAsPPF(theFileName, true);
		}

		public bool SaveAsPPF(string theFileName, bool saveInitialState)
		{
			throw new NotImplementedException();
		}

		public bool LoadState(SexyBuffer theBuffer)
		{
			return this.LoadState(theBuffer, false);
		}

		public bool LoadState(SexyBuffer theBuffer, bool shortened)
		{
			if (this.mError.Length != 0)
			{
				return false;
			}
			this.ResetAnim();
			theBuffer.mReadBitPos = (theBuffer.mReadBitPos + 7) & -8;
			int num = (int)theBuffer.ReadLong();
			int num2 = theBuffer.mReadBitPos / 8 + num;
			int num3 = (int)theBuffer.ReadShort();
			if (!shortened)
			{
				string theFileName = theBuffer.ReadStringWithEncoding();
				if (!this.mLoaded)
				{
					this.LoadEffect(theFileName);
				}
				int num4 = (int)theBuffer.ReadLong();
				if (num4 != this.mFileChecksum)
				{
					theBuffer.mReadBitPos = num2 * 8;
					return false;
				}
			}
			this.mFrameNum = theBuffer.ReadFloat();
			if (!shortened)
			{
				this.mRand.SRand(theBuffer.ReadStringWithEncoding());
				this.mWantsSRand = false;
			}
			if (!shortened)
			{
				this.mEmitAfterTimeline = theBuffer.ReadBoolean();
				this.mEmitterTransform = theBuffer.ReadTransform2D();
				this.mDrawTransform = theBuffer.ReadTransform2D();
			}
			else if (num3 == 0)
			{
				theBuffer.ReadBoolean();
				theBuffer.ReadTransform2D();
				theBuffer.ReadTransform2D();
			}
			if (this.mFrameNum > 0f)
			{
				for (int i = 0; i < this.mDef.mLayerDefVector.Count; i++)
				{
					PILayer pilayer = this.mLayerVector[i];
					PILayerDef pilayerDef = this.mDef.mLayerDefVector[i];
					for (int j = 0; j < pilayerDef.mEmitterInstanceDefVector.Count; j++)
					{
						PIEmitterInstance piemitterInstance = pilayer.mEmitterInstanceVector[j];
						PIEmitterInstanceDef piemitterInstanceDef = pilayerDef.mEmitterInstanceDefVector[j];
						if (theBuffer.ReadBoolean())
						{
							piemitterInstance.mTransform = theBuffer.ReadTransform2D();
						}
						piemitterInstance.mWasActive = theBuffer.ReadBoolean();
						piemitterInstance.mWithinLifeFrame = theBuffer.ReadBoolean();
						PIEmitter piemitter = this.mDef.mEmitterVector[piemitterInstanceDef.mEmitterDefIdx];
						for (int k = 0; k < piemitter.mParticleDefVector.Count; k++)
						{
							PIParticleDefInstance theParticleDefInstance = piemitterInstance.mParticleDefInstanceVector[k];
							this.LoadParticleDefInstance(theBuffer, theParticleDefInstance);
						}
						for (int l = 0; l < piemitterInstanceDef.mFreeEmitterIndices.Count; l++)
						{
							PIParticleDefInstance theParticleDefInstance2 = piemitterInstance.mSuperEmitterParticleDefInstanceVector[l];
							this.LoadParticleDefInstance(theBuffer, theParticleDefInstance2);
						}
						int num5 = (int)theBuffer.ReadLong();
						for (int m = 0; m < num5; m++)
						{
							PIFreeEmitterInstance pifreeEmitterInstance = this.mFreeEmitterPool.Alloc();
							pifreeEmitterInstance.Reset();
							int num6 = (int)theBuffer.ReadShort();
							pifreeEmitterInstance.mEmitterSrc = this.mDef.mEmitterVector[piemitterInstanceDef.mFreeEmitterIndices[num6]];
							pifreeEmitterInstance.mParentFreeEmitter = null;
							pifreeEmitterInstance.mParticleDef = null;
							pifreeEmitterInstance.mNum = m;
							this.LoadParticle(theBuffer, pilayer, pifreeEmitterInstance);
							PIEmitter mEmitterSrc = pifreeEmitterInstance.mEmitterSrc;
							pifreeEmitterInstance.mEmitter.mParticleDefInstanceVector.Resize(mEmitterSrc.mParticleDefVector.Count);
							for (int n = 0; n < mEmitterSrc.mParticleDefVector.Count; n++)
							{
								PIParticleDefInstance theParticleDefInstance3 = pifreeEmitterInstance.mEmitter.mParticleDefInstanceVector[n];
								this.LoadParticleDefInstance(theBuffer, theParticleDefInstance3);
							}
							if (m > 0)
							{
								piemitterInstance.mSuperEmitterGroup.mTail.mNext = pifreeEmitterInstance;
								pifreeEmitterInstance.mPrev = piemitterInstance.mSuperEmitterGroup.mTail;
							}
							else
							{
								piemitterInstance.mSuperEmitterGroup.mHead = pifreeEmitterInstance;
							}
							piemitterInstance.mSuperEmitterGroup.mTail = pifreeEmitterInstance;
							piemitterInstance.mSuperEmitterGroup.mCount++;
							int num7 = (int)theBuffer.ReadLong();
							for (int num8 = 0; num8 < num7; num8++)
							{
								PIParticleInstance piparticleInstance = this.mParticlePool.Alloc();
								piparticleInstance.Reset();
								piparticleInstance.mEmitterSrc = pifreeEmitterInstance.mEmitterSrc;
								piparticleInstance.mParentFreeEmitter = pifreeEmitterInstance;
								int num9 = (int)theBuffer.ReadShort();
								piparticleInstance.mParticleDef = piparticleInstance.mEmitterSrc.mParticleDefVector[num9];
								piparticleInstance.mNum = num8;
								this.LoadParticle(theBuffer, pilayer, piparticleInstance);
								this.CalcParticleTransform(pilayer, piemitterInstance, piparticleInstance.mEmitterSrc, piparticleInstance.mParticleDef, pifreeEmitterInstance.mEmitter.mParticleGroup, piparticleInstance);
								if (num8 > 0)
								{
									pifreeEmitterInstance.mEmitter.mParticleGroup.mTail.mNext = piparticleInstance;
									piparticleInstance.mPrev = pifreeEmitterInstance.mEmitter.mParticleGroup.mTail;
								}
								else
								{
									pifreeEmitterInstance.mEmitter.mParticleGroup.mHead = piparticleInstance;
								}
								pifreeEmitterInstance.mEmitter.mParticleGroup.mTail = piparticleInstance;
								pifreeEmitterInstance.mEmitter.mParticleGroup.mCount++;
							}
						}
						int num10 = (int)theBuffer.ReadLong();
						for (int num11 = 0; num11 < num10; num11++)
						{
							PIParticleInstance piparticleInstance2 = this.mParticlePool.Alloc();
							piparticleInstance2.Reset();
							piparticleInstance2.mEmitterSrc = piemitter;
							piparticleInstance2.mParentFreeEmitter = null;
							int num12 = (int)theBuffer.ReadShort();
							piparticleInstance2.mParticleDef = piparticleInstance2.mEmitterSrc.mParticleDefVector[num12];
							piparticleInstance2.mNum = num11;
							this.LoadParticle(theBuffer, pilayer, piparticleInstance2);
							this.CalcParticleTransform(pilayer, piemitterInstance, piparticleInstance2.mEmitterSrc, piparticleInstance2.mParticleDef, piemitterInstance.mParticleGroup, piparticleInstance2);
							if (num11 > 0)
							{
								piemitterInstance.mParticleGroup.mTail.mNext = piparticleInstance2;
								piparticleInstance2.mPrev = piemitterInstance.mParticleGroup.mTail;
							}
							else
							{
								piemitterInstance.mParticleGroup.mHead = piparticleInstance2;
							}
							piemitterInstance.mParticleGroup.mTail = piparticleInstance2;
							piemitterInstance.mParticleGroup.mCount++;
						}
					}
				}
			}
			else
			{
				theBuffer.mReadBitPos = num2 * 8;
			}
			return true;
		}

		public bool SaveState(SexyBuffer theBuffer)
		{
			return this.SaveState(ref theBuffer, false);
		}

		public bool SaveState(ref SexyBuffer theBuffer, bool shortened)
		{
			if (this.mError.Length != 0)
			{
				return false;
			}
			theBuffer.mWriteBitPos = (theBuffer.mWriteBitPos + 7) & -8;
			int num = theBuffer.mWriteBitPos / 8;
			theBuffer.WriteLong(0L);
			theBuffer.WriteShort(1);
			if (!shortened)
			{
				theBuffer.WriteStringWithEncoding(this.mSrcFileName);
				theBuffer.WriteLong(this.mFileChecksum);
			}
			theBuffer.WriteFloat(this.mFrameNum);
			if (!shortened)
			{
				theBuffer.WriteStringWithEncoding(this.mRand.Serialize());
				theBuffer.WriteBoolean(this.mEmitAfterTimeline);
				theBuffer.WriteTransform2D(this.mEmitterTransform);
				theBuffer.WriteTransform2D(this.mDrawTransform);
			}
			if (this.mFrameNum > 0f)
			{
				for (int i = 0; i < this.mDef.mLayerDefVector.Count; i++)
				{
					PILayer pilayer = this.mLayerVector[i];
					PILayerDef pilayerDef = this.mDef.mLayerDefVector[i];
					for (int j = 0; j < pilayer.mEmitterInstanceVector.Count; j++)
					{
						PIEmitterInstance piemitterInstance = pilayer.mEmitterInstanceVector[j];
						PIEmitterInstanceDef piemitterInstanceDef = pilayerDef.mEmitterInstanceDefVector[j];
						if (!GlobalPIEffect.IsIdentityMatrix(piemitterInstance.mTransform))
						{
							theBuffer.WriteBoolean(true);
							theBuffer.WriteTransform2D(piemitterInstance.mTransform);
						}
						else
						{
							theBuffer.WriteBoolean(false);
						}
						theBuffer.WriteBoolean(piemitterInstance.mWasActive);
						theBuffer.WriteBoolean(piemitterInstance.mWithinLifeFrame);
						Dictionary<PIEmitter, Dictionary<PIParticleDef, int>> dictionary = new Dictionary<PIEmitter, Dictionary<PIParticleDef, int>>();
						PIEmitter piemitter = this.mDef.mEmitterVector[piemitterInstanceDef.mEmitterDefIdx];
						for (int k = 0; k < piemitter.mParticleDefVector.Count; k++)
						{
							PIParticleDef piparticleDef = piemitter.mParticleDefVector[k];
							PIParticleDefInstance theParticleDefInstance = piemitterInstance.mParticleDefInstanceVector[k];
							if (!dictionary.ContainsKey(piemitter))
							{
								dictionary.Add(piemitter, new Dictionary<PIParticleDef, int>());
							}
							if (!dictionary[piemitter].ContainsKey(piparticleDef))
							{
								dictionary[piemitter].Add(piparticleDef, k);
							}
							else
							{
								dictionary[piemitter][piparticleDef] = k;
							}
							this.SaveParticleDefInstance(theBuffer, theParticleDefInstance);
						}
						Dictionary<PIEmitter, int> dictionary2 = new Dictionary<PIEmitter, int>();
						for (int l = 0; l < piemitterInstanceDef.mFreeEmitterIndices.Count; l++)
						{
							PIEmitter piemitter2 = this.mDef.mEmitterVector[piemitterInstanceDef.mFreeEmitterIndices[l]];
							for (int m = 0; m < piemitter2.mParticleDefVector.Count; m++)
							{
								PIParticleDef piparticleDef2 = piemitter2.mParticleDefVector[m];
								if (!dictionary.ContainsKey(piemitter2))
								{
									dictionary.Add(piemitter2, new Dictionary<PIParticleDef, int>());
								}
								if (!dictionary[piemitter2].ContainsKey(piparticleDef2))
								{
									dictionary[piemitter2].Add(piparticleDef2, m);
								}
								else
								{
									dictionary[piemitter2][piparticleDef2] = m;
								}
							}
							PIParticleDefInstance theParticleDefInstance2 = piemitterInstance.mSuperEmitterParticleDefInstanceVector[l];
							this.SaveParticleDefInstance(theBuffer, theParticleDefInstance2);
							dictionary2[piemitter2] = l;
						}
						PIFreeEmitterInstance pifreeEmitterInstance = (PIFreeEmitterInstance)piemitterInstance.mSuperEmitterGroup.mHead;
						theBuffer.WriteLong(this.CountParticles(pifreeEmitterInstance));
						while (pifreeEmitterInstance != null)
						{
							theBuffer.WriteShort((short)dictionary2[pifreeEmitterInstance.mEmitterSrc]);
							this.SaveParticle(theBuffer, pilayer, pifreeEmitterInstance);
							PIEmitter mEmitterSrc = pifreeEmitterInstance.mEmitterSrc;
							for (int n = 0; n < mEmitterSrc.mParticleDefVector.Count; n++)
							{
								PIParticleDefInstance theParticleDefInstance3 = pifreeEmitterInstance.mEmitter.mParticleDefInstanceVector[n];
								this.SaveParticleDefInstance(theBuffer, theParticleDefInstance3);
							}
							PIParticleInstance piparticleInstance = pifreeEmitterInstance.mEmitter.mParticleGroup.mHead;
							theBuffer.WriteLong(this.CountParticles(piparticleInstance));
							while (piparticleInstance != null)
							{
								theBuffer.WriteShort((short)dictionary[piparticleInstance.mEmitterSrc][piparticleInstance.mParticleDef]);
								this.SaveParticle(theBuffer, pilayer, piparticleInstance);
								piparticleInstance = piparticleInstance.mNext;
							}
							pifreeEmitterInstance = (PIFreeEmitterInstance)pifreeEmitterInstance.mNext;
						}
						PIParticleInstance piparticleInstance2 = piemitterInstance.mParticleGroup.mHead;
						int num2 = this.CountParticles(piparticleInstance2);
						theBuffer.WriteLong(num2);
						while (piparticleInstance2 != null)
						{
							short theShort = (short)dictionary[piparticleInstance2.mEmitterSrc][piparticleInstance2.mParticleDef];
							theBuffer.WriteShort(theShort);
							this.SaveParticle(theBuffer, pilayer, piparticleInstance2);
							piparticleInstance2 = piparticleInstance2.mNext;
						}
					}
				}
			}
			int num3 = theBuffer.mWriteBitPos / 8 - num - 4;
			int mWriteBitPos = theBuffer.mWriteBitPos;
			theBuffer.mWriteBitPos = num;
			theBuffer.WriteLong(num3);
			theBuffer.mWriteBitPos = mWriteBitPos;
			return true;
		}

		public void ResetAnim()
		{
			this.mFrameNum = 0f;
			for (int i = 0; i < this.mDef.mLayerDefVector.Count; i++)
			{
				PILayerDef pilayerDef = this.mDef.mLayerDefVector[i];
				PILayer pilayer = this.mLayerVector[i];
				for (int j = 0; j < pilayer.mEmitterInstanceVector.Count; j++)
				{
					PIEmitterInstanceDef piemitterInstanceDef = pilayerDef.mEmitterInstanceDefVector[j];
					PIEmitterInstance piemitterInstance = pilayer.mEmitterInstanceVector[j];
					PIFreeEmitterInstance pifreeEmitterInstance2;
					for (PIFreeEmitterInstance pifreeEmitterInstance = (PIFreeEmitterInstance)piemitterInstance.mSuperEmitterGroup.mHead; pifreeEmitterInstance != null; pifreeEmitterInstance = pifreeEmitterInstance2)
					{
						pifreeEmitterInstance2 = (PIFreeEmitterInstance)pifreeEmitterInstance.mNext;
						PIParticleInstance mNext;
						for (PIParticleInstance piparticleInstance = pifreeEmitterInstance.mEmitter.mParticleGroup.mHead; piparticleInstance != null; piparticleInstance = mNext)
						{
							mNext = piparticleInstance.mNext;
							this.mParticlePool.Free(piparticleInstance);
						}
						this.mFreeEmitterPool.Free(pifreeEmitterInstance);
					}
					piemitterInstance.mSuperEmitterGroup.mHead = null;
					piemitterInstance.mSuperEmitterGroup.mTail = null;
					piemitterInstance.mSuperEmitterGroup.mCount = 0;
					PIParticleInstance mNext2;
					for (PIParticleInstance piparticleInstance2 = piemitterInstance.mParticleGroup.mHead; piparticleInstance2 != null; piparticleInstance2 = mNext2)
					{
						mNext2 = piparticleInstance2.mNext;
						this.mParticlePool.Free(piparticleInstance2);
					}
					piemitterInstance.mParticleGroup.mHead = null;
					piemitterInstance.mParticleGroup.mTail = null;
					piemitterInstance.mParticleGroup.mCount = 0;
					for (int k = 0; k < piemitterInstanceDef.mFreeEmitterIndices.Count; k++)
					{
						PIParticleDefInstance piparticleDefInstance = piemitterInstance.mSuperEmitterParticleDefInstanceVector[k];
						piparticleDefInstance.Reset();
					}
					PIEmitter piemitter = this.mDef.mEmitterVector[piemitterInstanceDef.mEmitterDefIdx];
					for (int l = 0; l < piemitter.mParticleDefVector.Count; l++)
					{
						PIParticleDefInstance piparticleDefInstance2 = piemitterInstance.mParticleDefInstanceVector[l];
						piparticleDefInstance2.Reset();
					}
					piemitterInstance.mWithinLifeFrame = true;
					piemitterInstance.mWasActive = false;
				}
			}
			this.mCurNumEmitters = 0;
			this.mCurNumParticles = 0;
			this.mLastDrawnPixelCount = 0;
			this.mWantsSRand = true;
		}

		public void Clear()
		{
			this.mError = "";
			this.ResetAnim();
			this.mStringVector.Clear();
			this.mNotesParams.Clear();
			this.mDef.mEmitterVector.Clear();
			this.mDef.mTextureVector.Clear();
			this.mDef.mLayerDefVector.Clear();
			this.mDef.mEmitterRefMap.Clear();
			this.mRandSeeds.Clear();
			this.mVersion = 0;
			this.mLoaded = false;
		}

		public PILayer GetLayer(int theIdx)
		{
			if (theIdx < this.mDef.mLayerDefVector.Count)
			{
				return this.mLayerVector[theIdx];
			}
			return null;
		}

		public PILayer GetLayer(string theName)
		{
			for (int i = 0; i < this.mDef.mLayerDefVector.Count; i++)
			{
				if (theName.Length == 0 || this.mDef.mLayerDefVector[i].mName == theName)
				{
					return this.mLayerVector[i];
				}
			}
			return null;
		}

		public bool HasTimelineExpired()
		{
			return this.mFrameNum >= (float)this.mLastFrameNum;
		}

		public bool IsActive()
		{
			for (int i = 0; i < this.mDef.mLayerDefVector.Count; i++)
			{
				PILayerDef pilayerDef = this.mDef.mLayerDefVector[i];
				PILayer pilayer = this.mLayerVector[i];
				if (pilayer.mVisible)
				{
					for (int j = 0; j < pilayer.mEmitterInstanceVector.Count; j++)
					{
						PIEmitterInstanceDef piemitterInstanceDef = pilayerDef.mEmitterInstanceDefVector[j];
						PIEmitterInstance piemitterInstance = pilayer.mEmitterInstanceVector[j];
						if (piemitterInstance.mVisible)
						{
							if (piemitterInstanceDef.mValues[13].GetNextKeyframeTime(this.mFrameNum) >= this.mFrameNum)
							{
								return true;
							}
							if (piemitterInstance.mWithinLifeFrame)
							{
								return true;
							}
							if (piemitterInstance.mSuperEmitterGroup.mHead != null)
							{
								return true;
							}
							if (piemitterInstance.mParticleGroup.mHead != null)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		public string GetNotesParam(string theName)
		{
			return this.GetNotesParam(theName, "");
		}

		public string GetNotesParam(string theName, string theDefault)
		{
			if (this.mNotesParams.ContainsKey(theName.ToUpper()))
			{
				return this.mNotesParams[theName.ToUpper()];
			}
			return theDefault;
		}

		public void Update()
		{
			if (this.mError.Length > 0)
			{
				return;
			}
			this.mUpdateCnt++;
			bool flag = this.mFrameNum == 0f;
			if (this.mWantsSRand)
			{
				if (this.mRandSeeds.Count > 0)
				{
					this.mRand.SRand((uint)this.mRandSeeds[Common.Rand() % this.mRandSeeds.Count]);
				}
				else
				{
					this.mRand.SRand((uint)Common.Rand());
				}
				this.mWantsSRand = false;
			}
			if (flag && this.mStartupState.GetDataLen() != 0)
			{
				this.mStartupState.SeekFront();
				this.LoadState(this.mStartupState, true);
				this.mWantsSRand = false;
				return;
			}
			bool flag2 = true;
			while (this.mFrameNum < (float)this.mFirstFrameNum || flag2)
			{
				flag2 = false;
				this.mCurNumEmitters = 0;
				this.mCurNumParticles = 0;
				float num = 100f / this.mAnimSpeed;
				int num2 = (int)this.mFrameNum;
				if (flag)
				{
					this.mFrameNum += 0.0001f;
				}
				else
				{
					this.mFrameNum += (float)this.mFramerate / num;
				}
				this.mIsNewFrame = num2 != (int)this.mFrameNum;
				for (int i = 0; i < this.mDef.mLayerDefVector.Count; i++)
				{
					PILayerDef pilayerDef = this.mDef.mLayerDefVector[i];
					PILayer pilayer = this.mLayerVector[i];
					if (pilayer.mVisible)
					{
						for (int j = 0; j < pilayerDef.mDeflectorVector.Count; j++)
						{
							PIDeflector pideflector = pilayerDef.mDeflectorVector[j];
							SexyTransform2D theMat = new SexyTransform2D(false);
							float valueAt = pideflector.mAngle.GetValueAt(this.mFrameNum);
							if (valueAt != 0f)
							{
								theMat.RotateDeg(valueAt);
							}
							Vector2 valueAt2 = pideflector.mPos.GetValueAt(this.mFrameNum);
							theMat.Translate(valueAt2.X, valueAt2.Y);
							Vector2 valueAt3 = pilayerDef.mOffset.GetValueAt(this.mFrameNum);
							theMat.Translate(valueAt3.X, valueAt3.Y);
							float valueAt4 = pilayerDef.mAngle.GetValueAt(this.mFrameNum);
							if (valueAt4 != 0f)
							{
								theMat.RotateDeg(valueAt4);
							}
							SexyTransform2D theMatrix = this.mDrawTransform * theMat;
							for (int k = 0; k < pideflector.mPoints.Count; k++)
							{
								pideflector.mCurPoints[k] = GlobalPIEffect.TransformFPoint(theMatrix, pideflector.mPoints[k].GetValueAt(this.mFrameNum));
							}
						}
						for (int l = 0; l < pilayerDef.mForceVector.Count; l++)
						{
							PIForce piforce = pilayerDef.mForceVector[l];
							SexyTransform2D theMat2 = new SexyTransform2D(false);
							theMat2.Scale(piforce.mWidth.GetValueAt(this.mFrameNum) / 2f, piforce.mHeight.GetValueAt(this.mFrameNum) / 2f);
							float valueAt5 = piforce.mAngle.GetValueAt(this.mFrameNum);
							if (valueAt5 != 0f)
							{
								theMat2.RotateDeg(valueAt5);
							}
							Vector2 valueAt6 = piforce.mPos.GetValueAt(this.mFrameNum);
							theMat2.Translate(valueAt6.X, valueAt6.Y);
							Vector2 valueAt7 = pilayerDef.mOffset.GetValueAt(this.mFrameNum);
							theMat2.Translate(valueAt7.X, valueAt7.Y);
							float valueAt8 = pilayerDef.mAngle.GetValueAt(this.mFrameNum);
							if (valueAt8 != 0f)
							{
								theMat2.RotateDeg(valueAt8);
							}
							SexyTransform2D theMatrix2 = this.mDrawTransform * theMat2;
							Vector2[] array = new Vector2[]
							{
								new Vector2(-1f, -1f),
								new Vector2(1f, -1f),
								new Vector2(1f, 1f),
								new Vector2(-1f, 1f),
								new Vector2(0f, 0f)
							};
							for (int m = 0; m < 5; m++)
							{
								piforce.mCurPoints[m] = GlobalPIEffect.TransformFPoint(theMatrix2, array[m]);
							}
						}
						for (int n = 0; n < pilayer.mEmitterInstanceVector.Count; n++)
						{
							PIEmitterInstanceDef piemitterInstanceDef = pilayerDef.mEmitterInstanceDefVector[n];
							PIEmitterInstance piemitterInstance = pilayer.mEmitterInstanceVector[n];
							int num3 = 0;
							int num4 = 0;
							int num5 = 1;
							while (piemitterInstance.mVisible && num5 > 0)
							{
								num3 = 0;
								num4 = 0;
								num5--;
								bool flag3 = piemitterInstanceDef.mValues[13].GetLastKeyframe(this.mFrameNum) > 0.99f;
								if (!flag3)
								{
									num5 = 0;
								}
								else if (!piemitterInstance.mWasActive)
								{
									num5 += (int)((float)piemitterInstanceDef.mFramesToPreload * num / (float)this.mFramerate);
								}
								piemitterInstance.mWasActive = flag3;
								float nextKeyframeTime = piemitterInstanceDef.mValues[13].GetNextKeyframeTime(0f);
								float lastKeyframeTime = piemitterInstanceDef.mValues[13].GetLastKeyframeTime((float)this.mLastFrameNum + 1f);
								float lastKeyframe = piemitterInstanceDef.mValues[13].GetLastKeyframe((float)this.mLastFrameNum + 1f);
								piemitterInstance.mWithinLifeFrame = this.mFrameNum >= nextKeyframeTime && (this.mFrameNum < lastKeyframeTime || lastKeyframe > 0.99f) && (this.mEmitAfterTimeline || this.mFrameNum < (float)this.mLastFrameNum);
								if (flag3 || (piemitterInstanceDef.mIsSuperEmitter && piemitterInstance.mWithinLifeFrame))
								{
									num3++;
								}
								if (piemitterInstanceDef.mIsSuperEmitter)
								{
									for (int num6 = 0; num6 < piemitterInstanceDef.mFreeEmitterIndices.Count; num6++)
									{
										PIEmitter theEmitter = this.mDef.mEmitterVector[piemitterInstanceDef.mFreeEmitterIndices[num6]];
										PIParticleDefInstance theParticleDefInstance = piemitterInstance.mSuperEmitterParticleDefInstanceVector[num6];
										this.UpdateParticleDef(pilayer, theEmitter, piemitterInstance, null, theParticleDefInstance, piemitterInstance.mSuperEmitterGroup, null);
									}
									this.UpdateParticleGroup(pilayer, piemitterInstance, piemitterInstance.mSuperEmitterGroup);
									PIFreeEmitterInstance pifreeEmitterInstance2;
									for (PIFreeEmitterInstance pifreeEmitterInstance = (PIFreeEmitterInstance)piemitterInstance.mSuperEmitterGroup.mHead; pifreeEmitterInstance != null; pifreeEmitterInstance = pifreeEmitterInstance2)
									{
										pifreeEmitterInstance2 = (PIFreeEmitterInstance)pifreeEmitterInstance.mNext;
										PIEmitter mEmitterSrc = pifreeEmitterInstance.mEmitterSrc;
										for (int num7 = 0; num7 < mEmitterSrc.mParticleDefVector.Count; num7++)
										{
											PIParticleDef theParticleDef = mEmitterSrc.mParticleDefVector[num7];
											PIParticleDefInstance theParticleDefInstance2 = pifreeEmitterInstance.mEmitter.mParticleDefInstanceVector[num7];
											this.UpdateParticleDef(pilayer, mEmitterSrc, piemitterInstance, theParticleDef, theParticleDefInstance2, pifreeEmitterInstance.mEmitter.mParticleGroup, pifreeEmitterInstance);
										}
										this.UpdateParticleGroup(pilayer, piemitterInstance, pifreeEmitterInstance.mEmitter.mParticleGroup);
										num4 += pifreeEmitterInstance.mEmitter.mParticleGroup.mCount;
										num3++;
									}
								}
								else
								{
									PIEmitter piemitter = this.mDef.mEmitterVector[piemitterInstanceDef.mEmitterDefIdx];
									for (int num8 = 0; num8 < piemitter.mParticleDefVector.Count; num8++)
									{
										PIParticleGroup mParticleGroup = piemitterInstance.mParticleGroup;
										PIParticleDef theParticleDef2 = piemitter.mParticleDefVector[num8];
										PIParticleDefInstance theParticleDefInstance3 = piemitterInstance.mParticleDefInstanceVector[num8];
										this.UpdateParticleDef(pilayer, piemitter, piemitterInstance, theParticleDef2, theParticleDefInstance3, mParticleGroup, null);
									}
									this.UpdateParticleGroup(pilayer, piemitterInstance, piemitterInstance.mParticleGroup);
									num4 += piemitterInstance.mParticleGroup.mCount;
								}
							}
							this.mCurNumEmitters += num3;
							this.mCurNumParticles += num4;
						}
					}
				}
				flag = false;
			}
		}

		public void DrawDarkenLayer(Graphics g, PILayer theLayer)
		{
			g.PushState();
			g.SetColorizeImages(true);
			PILayerDef mLayerDef = theLayer.mLayerDef;
			for (int i = 0; i < theLayer.mEmitterInstanceVector.Count; i++)
			{
				PIEmitterInstanceDef piemitterInstanceDef = mLayerDef.mEmitterInstanceDefVector[i];
				PIEmitterInstance piemitterInstance = theLayer.mEmitterInstanceVector[i];
				if (piemitterInstance.mVisible)
				{
					if (piemitterInstanceDef.mIsSuperEmitter)
					{
						for (int j = 0; j < piemitterInstanceDef.mFreeEmitterIndices.Count; j++)
						{
							for (PIFreeEmitterInstance pifreeEmitterInstance = (PIFreeEmitterInstance)piemitterInstance.mSuperEmitterGroup.mHead; pifreeEmitterInstance != null; pifreeEmitterInstance = (PIFreeEmitterInstance)pifreeEmitterInstance.mNext)
							{
								this.DrawParticleGroup(g, theLayer, piemitterInstance, pifreeEmitterInstance.mEmitter.mParticleGroup, true);
							}
						}
					}
					else
					{
						this.DrawParticleGroup(g, theLayer, piemitterInstance, piemitterInstance.mParticleGroup, true);
					}
				}
			}
			g.PopState();
		}

		public void DrawLayer(Graphics g, PILayer theLayer)
		{
			g.PushState();
			this.mNormalList.Clear();
			this.mAdditiveList.Clear();
			PILayerDef mLayerDef = theLayer.mLayerDef;
			for (int i = 0; i < theLayer.mEmitterInstanceVector.Count; i++)
			{
				PIEmitterInstanceDef piemitterInstanceDef = mLayerDef.mEmitterInstanceDefVector[i];
				PIEmitterInstance piemitterInstance = theLayer.mEmitterInstanceVector[i];
				if (piemitterInstance.mVisible)
				{
					this.mDarken = false;
					for (int j = 0; j < 2; j++)
					{
						if (piemitterInstanceDef.mIsSuperEmitter)
						{
							for (int k = 0; k < piemitterInstanceDef.mFreeEmitterIndices.Count; k++)
							{
								for (PIFreeEmitterInstance pifreeEmitterInstance = (PIFreeEmitterInstance)piemitterInstance.mSuperEmitterGroup.mHead; pifreeEmitterInstance != null; pifreeEmitterInstance = (PIFreeEmitterInstance)pifreeEmitterInstance.mNext)
								{
									this.DrawParticleGroup(g, theLayer, piemitterInstance, pifreeEmitterInstance.mEmitter.mParticleGroup, j == 0);
								}
							}
						}
						else
						{
							this.DrawParticleGroup(g, theLayer, piemitterInstance, piemitterInstance.mParticleGroup, j == 0);
						}
					}
				}
			}
			g.PopState();
		}

		public void DrawPhisycalLayer(Graphics g, PILayer theLayer)
		{
			g.PushState();
			g.SetColorizeImages(true);
			PILayerDef mLayerDef = theLayer.mLayerDef;
			g.SetDrawMode(0);
			for (int i = 0; i < mLayerDef.mBlockerVector.Count; i++)
			{
				PIBlocker piblocker = mLayerDef.mBlockerVector[i];
				bool flag = piblocker.mActive.GetLastKeyframe(this.mFrameNum) > 0.99f;
				if (this.mDebug || flag)
				{
					SexyTransform2D theMat = new SexyTransform2D(false);
					float valueAt = piblocker.mAngle.GetValueAt(this.mFrameNum);
					if (valueAt != 0f)
					{
						theMat.RotateDeg(valueAt);
					}
					Vector2 valueAt2 = piblocker.mPos.GetValueAt(this.mFrameNum);
					theMat.Translate(valueAt2.X, valueAt2.Y);
					Vector2 valueAt3 = mLayerDef.mOffset.GetValueAt(this.mFrameNum);
					theMat.Translate(valueAt3.X, valueAt3.Y);
					float valueAt4 = mLayerDef.mAngle.GetValueAt(this.mFrameNum);
					if (valueAt4 != 0f)
					{
						theMat.RotateDeg(valueAt4);
					}
					SexyTransform2D theMatrix = this.mDrawTransform * theMat;
					Vector2[] array = new Vector2[512];
					int num = Math.Min(512, piblocker.mPoints.Count);
					for (int j = 0; j < num; j++)
					{
						array[j] = GlobalPIEffect.TransformFPoint(theMatrix, piblocker.mPoints[j].GetValueAt(this.mFrameNum));
					}
					Vector2[,] array2 = new Vector2[256, 3];
					int num2 = 0;
					Common.DividePoly(array, num, array2, 256, ref num2);
					if (flag)
					{
						for (int k = 0; k < num2; k++)
						{
							if (theLayer.mBkgImage != null)
							{
								SexyVertex2D[] array3 = new SexyVertex2D[3];
								for (int l = 0; l < 3; l++)
								{
									array3[l] = new SexyVertex2D(array2[k, l].X, array2[k, l].Y, (array2[k, l].X + theLayer.mBkgImgDrawOfs.X) / (float)theLayer.mBkgImage.mWidth, (array2[k, l].Y + theLayer.mBkgImgDrawOfs.Y) / (float)theLayer.mBkgImage.mHeight);
								}
								g.SetColor(SexyColor.White);
								g.DrawTriangleTex(theLayer.mBkgImage, array3[0], array3[1], array3[2]);
							}
							else
							{
								Vector2[] array4 = new Vector2[3];
								for (int m = 0; m < 3; m++)
								{
									array4[m] = array2[k, m];
								}
								g.SetColor(this.mBkgColor);
							}
						}
					}
				}
			}
			for (int n = 0; n < mLayerDef.mDeflectorVector.Count; n++)
			{
				PIDeflector pideflector = mLayerDef.mDeflectorVector[n];
				bool flag2 = pideflector.mActive.GetLastKeyframe(this.mFrameNum) > 0.99f;
				if ((pideflector.mVisible && flag2) || this.mDebug)
				{
					if (flag2)
					{
						g.SetColor(255, 0, 0);
					}
					else
					{
						g.SetColor(64, 0, 0);
					}
					for (int num3 = 1; num3 < pideflector.mCurPoints.Count; num3++)
					{
						Vector2 vector = pideflector.mCurPoints[num3 - 1];
						Vector2 vector2 = pideflector.mCurPoints[num3];
						if (pideflector.mThickness <= 1.5f)
						{
							g.DrawLine((int)vector.X, (int)vector.Y, (int)vector2.X, (int)vector2.Y);
						}
						else
						{
							SexyVector2 sexyVector = new SexyVector2(vector2.X - vector.X, vector2.Y - vector.Y);
							SexyVector2 sexyVector2 = sexyVector.Normalize().Perp();
							Vector2 vector3;
							vector3 = new Vector2(sexyVector2.x, sexyVector2.y);
							vector3 = GlobalPIEffect.TransformFPoint(this.mDrawTransform, vector3);
							Vector2[] array5 = new Vector2[]
							{
								vector + vector3 * pideflector.mThickness,
								vector2 + vector3 * pideflector.mThickness,
								vector2 - vector3 * pideflector.mThickness,
								vector - vector3 * pideflector.mThickness
							};
							for (int num4 = 0; num4 < 4; num4++)
							{
								vector = array5[num4];
								vector2 = array5[(num4 + 1) % 4];
								g.DrawLine((int)vector.X, (int)vector.Y, (int)vector2.X, (int)vector2.Y);
							}
						}
					}
				}
			}
			for (int num5 = 0; num5 < mLayerDef.mForceVector.Count; num5++)
			{
				PIForce piforce = mLayerDef.mForceVector[num5];
				bool flag3 = piforce.mActive.GetLastKeyframe(this.mFrameNum) > 0.99f;
				if ((piforce.mVisible && flag3) || this.mDebug)
				{
					if (flag3)
					{
						g.SetColor(255, 0, 255);
					}
					else
					{
						g.SetColor(64, 0, 64);
					}
					for (int num6 = 0; num6 < 4; num6++)
					{
						Vector2 vector4 = piforce.mCurPoints[num6];
						Vector2 vector5 = piforce.mCurPoints[(num6 + 1) % 4];
						g.DrawLine((int)vector4.X, (int)vector4.Y, (int)vector5.X, (int)vector5.Y);
					}
					float num7 = MathHelper.ToRadians(-piforce.mDirection.GetValueAt(this.mFrameNum)) + MathHelper.ToRadians(-piforce.mAngle.GetValueAt(this.mFrameNum));
					Transform transform = new Transform();
					transform.RotateRad(-num7);
					Vector2[] array6 = new Vector2[]
					{
						new Vector2(5f, 0f),
						new Vector2(-5f, -10f),
						new Vector2(-5f, 10f)
					};
					for (int num8 = 0; num8 < 3; num8++)
					{
						Vector2 vector6 = GlobalPIEffect.TransformFPoint(transform.GetMatrix(), array6[num8]) + piforce.mCurPoints[4];
						Vector2 vector7 = GlobalPIEffect.TransformFPoint(transform.GetMatrix(), array6[(num8 + 1) % 3]) + piforce.mCurPoints[4];
						g.DrawLine((int)vector6.X, (int)vector6.Y, (int)vector7.X, (int)vector7.Y);
					}
				}
			}
			g.PopState();
		}

		public void Draw(Graphics g)
		{
			this.mLastDrawnPixelCount = 0;
			for (int i = 0; i < this.mDef.mLayerDefVector.Count; i++)
			{
				PILayer pilayer = this.mLayerVector[i];
				if (pilayer.mVisible)
				{
					this.DrawLayer(g, pilayer);
					this.DrawLayerNormal(g, pilayer);
					this.DrawLayerAdditive(g, pilayer);
					this.DrawPhisycalLayer(g, pilayer);
				}
			}
			this.mLastDrawnPixelCount *= (int)GlobalPIEffect.GetMatrixScale(this.mDrawTransform);
		}

		public void Draw(Graphics g, bool isDarkenise)
		{
			this.mLastDrawnPixelCount = 0;
			for (int i = 0; i < this.mDef.mLayerDefVector.Count; i++)
			{
				PILayer pilayer = this.mLayerVector[i];
				if (pilayer.mVisible)
				{
					if (isDarkenise)
					{
						this.DrawDarkenLayer(g, pilayer);
					}
					else
					{
						this.DrawLayer(g, pilayer);
					}
				}
			}
			this.mLastDrawnPixelCount *= (int)GlobalPIEffect.GetMatrixScale(this.mDrawTransform);
		}

		public bool CheckCache()
		{
			return true;
		}

		public bool SetCacheUpToDate()
		{
			return true;
		}

		public void WriteToCache()
		{
		}

		public SexyBuffer mReadBuffer = new SexyBuffer();

		public int mFileChecksum;

		public bool mIsPPF;

		public bool mAutoPadImages;

		public bool mInUse = true;

		public int mVersion;

		public string mSrcFileName;

		public string mDestFileName;

		public MTRand mRand = new MTRand();

		public SexyBuffer mStartupState = new SexyBuffer();

		public static int mNeedUpdate;

		public int mOptimizeValue = 1;

		public float mLastLifePct = -1f;

		public int mBufTemp;

		public int mBufPos;

		public int mChecksumPos;

		public string mNotes;

		public int mFileIdx;

		public List<string> mStringVector = new List<string>();

		public int mWidth;

		public int mHeight;

		public SexyColor mBkgColor = default(SexyColor);

		public int mFramerate;

		public int mFirstFrameNum;

		public int mLastFrameNum;

		public DeviceImage mThumbnail;

		public Dictionary<string, string> mNotesParams = new Dictionary<string, string>();

		public PIEffectDef mDef;

		public List<PILayer> mLayerVector = new List<PILayer>();

		public List<PIParticleInstance> mNormalList;

		public List<PIParticleInstance> mAdditiveList;

		public bool mDarken;

		public List<float> mTimes = new List<float>();

		public List<Vector2> mPoints = new List<Vector2>();

		public List<Vector2> mControlPoints = new List<Vector2>();

		public string mError = "";

		public bool mLoaded;

		public int mUpdateCnt;

		public float mFrameNum;

		public bool mIsNewFrame;

		public ObjectPool<PIParticleInstance> mParticlePool;

		public ObjectPool<PIFreeEmitterInstance> mFreeEmitterPool;

		public int mPoolSize;

		public bool mHasEmitterTransform;

		public bool mHasDrawTransform;

		public bool mDrawTransformSimple;

		public int mCurNumParticles;

		public int mCurNumEmitters;

		public int mLastDrawnPixelCount;

		public float mAnimSpeed;

		public SexyColor mColor = default(SexyColor);

		public bool mDebug;

		public bool mDrawBlockers;

		public bool mEmitAfterTimeline;

		public List<int> mRandSeeds = new List<int>();

		public bool mWantsSRand;

		private SpriteBatch mSpriteBatch;

		public SexyTransform2D mDrawTransform = new SexyTransform2D(false);

		public SexyTransform2D mEmitterTransform = new SexyTransform2D(false);
	}
}
