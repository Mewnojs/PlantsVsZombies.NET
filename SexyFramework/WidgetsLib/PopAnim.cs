using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sexy.GraphicsLib;
using Sexy.Misc;
using Sexy.Resource;

namespace Sexy.WidgetsLib
{
	public class PopAnim : Widget
	{
		public static int ClrR(int theColor)
		{
			return (theColor >> 16) & 255;
		}

		public static int ClrG(int theColor)
		{
			return (theColor >> 8) & 255;
		}

		public static int ClrB(int theColor)
		{
			return theColor & 255;
		}

		public static int ClrA(int theColor)
		{
			return (theColor >> 24) & 255;
		}

		internal static string WildcardExpand(string theValue, int theMatchStart, int theMatchEnd, string theReplacement)
		{
			string result;
			if (theReplacement.Length == 0)
			{
				result = "";
			}
			else if (theReplacement[0] == '*')
			{
				if (theReplacement.Length == 1)
				{
					result = theValue.Substring(0, theMatchStart) + theValue.Substring(theMatchEnd);
				}
				else if (theReplacement[theReplacement.Length - 1] == '*')
				{
					result = theValue.Substring(0, theMatchStart) + theReplacement.Substring(1, theReplacement.Length - 2) + theValue.Substring(theMatchEnd);
				}
				else
				{
					result = theValue.Substring(0, theMatchStart) + theReplacement.Substring(1, theReplacement.Length - 1);
				}
			}
			else if (theReplacement[theReplacement.Length - 1] == '*')
			{
				result = theReplacement.Substring(0, theReplacement.Length - 1) + theValue.Substring(theMatchEnd);
			}
			else
			{
				result = theReplacement;
			}
			return result;
		}

		internal static bool WildcardReplace(string theValue, string theWildcard, string theReplacement, ref string theResult)
		{
			if (theWildcard.Length == 0)
			{
				return false;
			}
			if (theWildcard[0] == '*')
			{
				if (theWildcard.Length == 1)
				{
					theResult = PopAnim.WildcardExpand(theValue, 0, theValue.Length, theReplacement);
					return true;
				}
				if (theWildcard[theWildcard.Length - 1] == '*')
				{
					int num = theWildcard.Length - 2;
					int num2 = theValue.Length - num;
					for (int i = 0; i <= num2; i++)
					{
						bool flag = true;
						for (int j = 0; j < num; j++)
						{
							if (char.ToUpper(theWildcard[j + 1]) != char.ToUpper(theValue[i + j]))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							theResult = PopAnim.WildcardExpand(theValue, i, i + num, theReplacement);
							return true;
						}
					}
				}
				else
				{
					if (theValue.Length < theWildcard.Length - 1)
					{
						return false;
					}
					if (theWildcard.Substring(1) == theValue.Substring(theValue.Length - theWildcard.Length + 1))
					{
						return false;
					}
					theResult = PopAnim.WildcardExpand(theValue, theValue.Length - theWildcard.Length + 1, theValue.Length, theReplacement);
					return true;
				}
			}
			else if (theWildcard[theWildcard.Length - 1] == '*')
			{
				if (theValue.Length < theWildcard.Length - 1)
				{
					return false;
				}
				if (theWildcard == theValue.Substring(0, theWildcard.Length - 1))
				{
					return false;
				}
				theResult = PopAnim.WildcardExpand(theValue, 0, theWildcard.Length - 1, theReplacement);
				return true;
			}
			else if (theWildcard == theValue)
			{
				if (theReplacement.Length > 0)
				{
					if (theReplacement[0] == '*')
					{
						theResult = theValue + theReplacement.Substring(1);
					}
					else if (theReplacement[theReplacement.Length - 1] == '*')
					{
						theResult = theReplacement.Substring(0, theReplacement.Length - 1) + theValue;
					}
					else
					{
						theResult = theReplacement;
					}
				}
				else
				{
					theResult = theReplacement;
				}
				return true;
			}
			return false;
		}

		internal static bool WildcardMatches(string theValue, string theWildcard)
		{
			if (theWildcard.Length == 0)
			{
				return false;
			}
			if (theWildcard[0] == '*')
			{
				if (theWildcard.Length == 1)
				{
					return true;
				}
				if (theWildcard[theWildcard.Length - 1] == '*')
				{
					int num = theWildcard.Length - 2;
					int num2 = theValue.Length - num;
					for (int i = 0; i <= num2; i++)
					{
						bool flag = true;
						for (int j = 0; j < num; j++)
						{
							if (char.ToUpper(theWildcard[j + 1]) != char.ToUpper(theValue[i + j]))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							return true;
						}
					}
					return false;
				}
				return theValue.Length >= theWildcard.Length - 1 && theWildcard.Substring(1) == theValue.Substring(theValue.Length - theWildcard.Length + 1);
			}
			else
			{
				if (theWildcard[theWildcard.Length - 1] == '*')
				{
					return theValue.Length >= theWildcard.Length - 1 && theWildcard == theValue.Substring(theWildcard.Length - 1);
				}
				return theWildcard == theValue;
			}
		}

		public bool Fail(string theError)
		{
			this.mError = theError;
			return false;
		}

		public void SetTransform(SexyTransform2D tran)
		{
			this.mTransform.CopyFrom(tran);
			this.mTransDirty = true;
		}

		public string Remap(string theString)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.mRemapList)
			{
				string value = keyValuePair.Value;
				if (PopAnim.WildcardReplace(theString, value, keyValuePair.Key, ref this.Remap_aString))
				{
					return this.Remap_aString;
				}
			}
			return theString;
		}

		public bool LoadSpriteDef(SexyBuffer theBuffer, PASpriteDef theSpriteDef)
		{
			Dictionary<int, PAObjectPos> dictionary = new Dictionary<int, PAObjectPos>();
			if (this.mVersion >= 4)
			{
				this.mMainAnimDef.mObjectNamePool.AddLast(theBuffer.ReadStringWithEncoding());
				theSpriteDef.mName = this.mMainAnimDef.mObjectNamePool.Last.Value;
				theSpriteDef.mAnimRate = (float)theBuffer.ReadLong() / 65536f;
				this.mCRCBuffer.WriteStringWithEncoding(theSpriteDef.mName);
			}
			else
			{
				theSpriteDef.mName = null;
				theSpriteDef.mAnimRate = (float)this.mAnimRate;
			}
			int num = (int)theBuffer.ReadShort();
			if (this.mVersion >= 5)
			{
				theSpriteDef.mWorkAreaStart = (int)theBuffer.ReadShort();
				theSpriteDef.mWorkAreaDuration = (int)theBuffer.ReadShort();
			}
			else
			{
				theSpriteDef.mWorkAreaStart = 0;
				theSpriteDef.mWorkAreaDuration = num - 1;
			}
			theSpriteDef.mWorkAreaDuration = Math.Min(theSpriteDef.mWorkAreaStart + theSpriteDef.mWorkAreaDuration, num - 1) - theSpriteDef.mWorkAreaStart;
			this.mCRCBuffer.WriteShort((short)num);
			theSpriteDef.mFrames.Clear();
			for (int i = 0; i < num; i++)
			{
				PAFrame paframe = new PAFrame();
				theSpriteDef.mFrames.Add(paframe);
				byte b = theBuffer.ReadByte();
				if (((int)b & PopAnim.FRAMEFLAGS_HAS_REMOVES) != 0)
				{
					int num2 = (int)theBuffer.ReadByte();
					if (num2 == 255)
					{
						num2 = (int)theBuffer.ReadShort();
					}
					for (int j = 0; j < num2; j++)
					{
						int num3 = (int)theBuffer.ReadShort();
						if (num3 >= 2047)
						{
							num3 = (int)theBuffer.ReadLong();
						}
						dictionary.Remove(num3);
					}
				}
				if (((int)b & PopAnim.FRAMEFLAGS_HAS_ADDS) != 0)
				{
					int num4 = (int)theBuffer.ReadByte();
					if (num4 == 255)
					{
						num4 = (int)theBuffer.ReadShort();
					}
					for (int k = 0; k < num4; k++)
					{
						PAObjectPos paobjectPos = new PAObjectPos();
						ushort num5 = (ushort)theBuffer.ReadShort();
						paobjectPos.mObjectNum = (int)(num5 & 2047);
						if (paobjectPos.mObjectNum == 2047)
						{
							paobjectPos.mObjectNum = (int)theBuffer.ReadLong();
						}
						paobjectPos.mIsSprite = (num5 & 32768) != 0;
						paobjectPos.mIsAdditive = (num5 & 16384) != 0;
						paobjectPos.mResNum = theBuffer.ReadByte();
						paobjectPos.mHasSrcRect = false;
						paobjectPos.mColorInt = -1;
						paobjectPos.mAnimFrameNum = 0;
						paobjectPos.mTimeScale = 1f;
						paobjectPos.mName = null;
						if ((num5 & 8192) != 0)
						{
							paobjectPos.mPreloadFrames = (int)theBuffer.ReadShort();
						}
						else
						{
							paobjectPos.mPreloadFrames = 0;
						}
						if ((num5 & 4096) != 0)
						{
							this.mMainAnimDef.mObjectNamePool.AddLast(theBuffer.ReadStringWithEncoding());
							paobjectPos.mName = this.mMainAnimDef.mObjectNamePool.Last.Value;
						}
						if ((num5 & 2048) != 0)
						{
							paobjectPos.mTimeScale = (float)theBuffer.ReadLong() / 65536f;
						}
						if (theSpriteDef.mObjectDefVector.Count < paobjectPos.mObjectNum + 1)
						{
							theSpriteDef.mObjectDefVector.Resize(paobjectPos.mObjectNum + 1);
						}
						theSpriteDef.mObjectDefVector[paobjectPos.mObjectNum].mName = paobjectPos.mName;
						if (paobjectPos.mIsSprite)
						{
							theSpriteDef.mObjectDefVector[paobjectPos.mObjectNum].mSpriteDef = this.mMainAnimDef.mSpriteDefVector[(int)paobjectPos.mResNum];
						}
						dictionary[paobjectPos.mObjectNum] = paobjectPos;
					}
				}
				if (((int)b & PopAnim.FRAMEFLAGS_HAS_MOVES) != 0)
				{
					int num6 = (int)theBuffer.ReadByte();
					if (num6 == 255)
					{
						num6 = (int)theBuffer.ReadShort();
					}
					for (int l = 0; l < num6; l++)
					{
						ushort num7 = (ushort)theBuffer.ReadShort();
						int num8 = (int)(num7 & 1023);
						if (num8 == 1023)
						{
							num8 = (int)theBuffer.ReadLong();
						}
						PAObjectPos paobjectPos2 = dictionary[num8];
						paobjectPos2.mTransform.mMatrix.LoadIdentity();
						if (((int)num7 & PopAnim.MOVEFLAGS_HAS_MATRIX) != 0)
						{
							paobjectPos2.mTransform.mMatrix.m00 = (float)theBuffer.ReadLong() / 65536f;
							paobjectPos2.mTransform.mMatrix.m01 = (float)theBuffer.ReadLong() / 65536f;
							paobjectPos2.mTransform.mMatrix.m10 = (float)theBuffer.ReadLong() / 65536f;
							paobjectPos2.mTransform.mMatrix.m11 = (float)theBuffer.ReadLong() / 65536f;
						}
						else if (((int)num7 & PopAnim.MOVEFLAGS_HAS_ROTATE) != 0)
						{
							float num9 = (float)theBuffer.ReadShort() / 1000f;
							float num10 = (float)Math.Sin((double)num9);
							float num11 = (float)Math.Cos((double)num9);
							if (this.mVersion == 2)
							{
								num10 = -num10;
							}
							paobjectPos2.mTransform.mMatrix.m00 = num11;
							paobjectPos2.mTransform.mMatrix.m01 = -num10;
							paobjectPos2.mTransform.mMatrix.m10 = num10;
							paobjectPos2.mTransform.mMatrix.m11 = num11;
						}
						SexyTransform2D impliedObject = new SexyTransform2D(false);
						impliedObject.LoadIdentity();
						if (((int)num7 & PopAnim.MOVEFLAGS_HAS_LONGCOORDS) != 0)
						{
							impliedObject.m02 = (float)theBuffer.ReadLong() / 20f;
							impliedObject.m12 = (float)theBuffer.ReadLong() / 20f;
						}
						else
						{
							impliedObject.m02 = (float)theBuffer.ReadShort() / 20f;
							impliedObject.m12 = (float)theBuffer.ReadShort() / 20f;
						}
						paobjectPos2.mTransform.mMatrix = impliedObject * paobjectPos2.mTransform.mMatrix;
						paobjectPos2.mHasSrcRect = ((int)num7 & PopAnim.MOVEFLAGS_HAS_SRCRECT) != 0;
						if (((int)num7 & PopAnim.MOVEFLAGS_HAS_SRCRECT) != 0)
						{
							paobjectPos2.mSrcRect.mX = (int)(theBuffer.ReadShort() / 20);
							paobjectPos2.mSrcRect.mY = (int)(theBuffer.ReadShort() / 20);
							paobjectPos2.mSrcRect.mWidth = (int)(theBuffer.ReadShort() / 20);
							paobjectPos2.mSrcRect.mHeight = (int)(theBuffer.ReadShort() / 20);
						}
						if (((int)num7 & PopAnim.MOVEFLAGS_HAS_COLOR) != 0)
						{
							paobjectPos2.mColorInt = ((int)theBuffer.ReadByte() << 16) | ((int)theBuffer.ReadByte() << 8) | (int)theBuffer.ReadByte() | ((int)theBuffer.ReadByte() << 24);
						}
						if (((int)num7 & PopAnim.MOVEFLAGS_HAS_ANIMFRAMENUM) != 0)
						{
							paobjectPos2.mAnimFrameNum = (int)theBuffer.ReadShort();
						}
					}
				}
				if (((int)b & PopAnim.FRAMEFLAGS_HAS_FRAME_NAME) != 0)
				{
					string text = theBuffer.ReadStringWithEncoding();
					text = this.Remap(text).ToUpper();
					theSpriteDef.mLabels.Add(text, i);
				}
				if (((int)b & PopAnim.FRAMEFLAGS_HAS_STOP) != 0)
				{
					paframe.mHasStop = true;
				}
				if (((int)b & PopAnim.FRAMEFLAGS_HAS_COMMANDS) != 0)
				{
					int num12 = (int)theBuffer.ReadByte();
					paframe.mCommandVector.Resize(num12);
					for (int m = 0; m < num12; m++)
					{
						paframe.mCommandVector[m].mCommand = this.Remap(theBuffer.ReadStringWithEncoding());
						paframe.mCommandVector[m].mParam = this.Remap(theBuffer.ReadStringWithEncoding());
					}
				}
				paframe.mFrameObjectPosVector.Resize(dictionary.Count);
				int num13 = 0;
				int[] array = Enumerable.ToArray<int>(dictionary.Keys);
				Array.Sort<int>(array);
				for (int n = 0; n < array.Length; n++)
				{
					PAObjectPos paobjectPos3 = dictionary[array[n]];
					paframe.mFrameObjectPosVector[num13] = paobjectPos3;
					paobjectPos3.mPreloadFrames = 0;
					num13++;
				}
			}
			if (num == 0)
			{
				theSpriteDef.mFrames.Resize(1);
			}
			for (int num14 = 0; num14 < theSpriteDef.mObjectDefVector.Count; num14++)
			{
				PAObjectDef paobjectDef = theSpriteDef.mObjectDefVector[num14];
				this.mCRCBuffer.WriteBoolean(paobjectDef.mSpriteDef != null);
			}
			return true;
		}

		public void InitSpriteInst(PASpriteInst theSpriteInst, PASpriteDef theSpriteDef)
		{
			theSpriteInst.mFrameRepeats = 0;
			theSpriteInst.mDelayFrames = 0;
			theSpriteInst.mDef = theSpriteDef;
			theSpriteInst.mLastUpdated = -1;
			theSpriteInst.mOnNewFrame = true;
			theSpriteInst.mFrameNum = 0f;
			theSpriteInst.mChildren.Resize(theSpriteDef.mObjectDefVector.Count);
			for (int i = 0; i < theSpriteDef.mObjectDefVector.Count; i++)
			{
				PAObjectDef paobjectDef = theSpriteDef.mObjectDefVector[i];
				PAObjectInst paobjectInst = theSpriteInst.mChildren[i];
				paobjectInst.mColorMult = new SexyColor(SexyColor.White);
				paobjectInst.mName = paobjectDef.mName;
				paobjectInst.mIsBlending = false;
				PASpriteDef mSpriteDef = paobjectDef.mSpriteDef;
				if (mSpriteDef != null)
				{
					PASpriteInst paspriteInst = new PASpriteInst();
					paspriteInst.mParent = theSpriteInst;
					this.InitSpriteInst(paspriteInst, mSpriteDef);
					paobjectInst.mSpriteInst = paspriteInst;
				}
			}
			if (theSpriteInst == this.mMainSpriteInst)
			{
				this.GetToFirstFrame();
			}
		}

		public void GetToFirstFrame()
		{
			while (this.mMainSpriteInst.mDef != null && this.mMainSpriteInst.mFrameNum < (float)this.mMainSpriteInst.mDef.mWorkAreaStart)
			{
				bool flag = this.mAnimRunning;
				bool flag2 = this.mPaused;
				this.mAnimRunning = true;
				this.mPaused = false;
				this.Update();
				if (GlobalMembers.gSexyAppBase.mVSyncUpdates)
				{
					this.UpdateF(1f);
				}
				this.mAnimRunning = flag;
				this.mPaused = flag2;
			}
		}

		public void FrameHit(PASpriteInst theSpriteInst, PAFrame theFrame, PAObjectPos theObjectPos)
		{
			theSpriteInst.mOnNewFrame = false;
			for (int i = 0; i < theFrame.mFrameObjectPosVector.Count; i++)
			{
				PAObjectPos paobjectPos = theFrame.mFrameObjectPosVector[i];
				if (paobjectPos.mIsSprite)
				{
					PASpriteInst mSpriteInst = theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
					if (mSpriteInst != null && theSpriteInst.mDef.mAnimRate != 0f)
					{
						for (int j = 0; j < paobjectPos.mPreloadFrames; j++)
						{
							this.IncSpriteInstFrame(mSpriteInst, paobjectPos, 1000f / GlobalMembers.gSexyAppBase.mFrameTime / theSpriteInst.mDef.mAnimRate);
						}
					}
				}
			}
			for (int k = 0; k < theFrame.mCommandVector.Count; k++)
			{
				PACommand pacommand = theFrame.mCommandVector[k];
				if (this.mListener == null || !this.mListener.PopAnimCommand(this.mId, theSpriteInst, pacommand.mCommand, pacommand.mParam))
				{
					if (pacommand.mCommand.ToLower() == "delay")
					{
						int num = pacommand.mParam.IndexOf(',');
						if (num != -1)
						{
							int num2 = Convert.ToInt32(pacommand.mParam.Substring(0, num).Trim());
							int num3 = Convert.ToInt32(pacommand.mParam.Substring(num + 1).Trim());
							if (num3 <= num2)
							{
								num3 = num2 + 1;
							}
							Random random = new Random(100);
							theSpriteInst.mDelayFrames = num2 + random.Next(num3 - num2);
						}
						else
						{
							int mDelayFrames = Convert.ToInt32(pacommand.mParam.Trim());
							theSpriteInst.mDelayFrames = mDelayFrames;
						}
					}
					else if (pacommand.mCommand.ToLower() == "playsample")
					{
						string text = pacommand.mParam;
						int thePan = 0;
						double theVolume = 1.0;
						double theNumSteps = 0.0;
						string theSampleName = "";
						bool flag = true;
						while (text.Length > 0)
						{
							int num4 = text.IndexOf(',');
							string text2;
							if (num4 == -1)
							{
								text2 = text;
							}
							else
							{
								text2 = text.Substring(0, num4);
							}
							if (flag)
							{
								theSampleName = text2;
								flag = false;
							}
							else
							{
								int num5;
								while ((num5 = text2.IndexOf(' ')) != -1)
								{
									text2 = text2.Remove(num5);
								}
								if (text2.Substring(0, 7) == "volume=")
								{
									theVolume = double.Parse(text2.Substring(7), NumberStyles.Float, CultureInfo.InvariantCulture);
								}
								else if (text2.Substring(0, 4) == "pan=")
								{
									thePan = int.Parse(text2.Substring(4));
								}
								else if (text2.Substring(0, 6) == "steps=")
								{
									theNumSteps = double.Parse(text2.Substring(6), NumberStyles.Float, CultureInfo.InvariantCulture);
								}
							}
							if (num4 == -1)
							{
								break;
							}
							text = text.Substring(num4 + 1);
						}
						if (this.mListener != null)
						{
							this.mListener.PopAnimPlaySample(theSampleName, thePan, theVolume, theNumSteps);
						}
					}
					else if (pacommand.mCommand.ToLower() == "addparticleeffect")
					{
						string text3 = pacommand.mParam;
						PAParticleEffect paparticleEffect = new PAParticleEffect();
						paparticleEffect.mXOfs = 0.0;
						paparticleEffect.mYOfs = 0.0;
						paparticleEffect.mBehind = false;
						paparticleEffect.mEffect = null;
						paparticleEffect.mAttachEmitter = false;
						paparticleEffect.mTransform = false;
						paparticleEffect.mLastUpdated = this.mUpdateCnt;
						bool flag2 = false;
						string text4 = "";
						bool flag3 = true;
						while (text3.Length > 0)
						{
							int num6 = text3.IndexOf(',');
							string text5;
							if (num6 == -1)
							{
								text5 = text3;
							}
							else
							{
								text5 = text3.Substring(0, num6);
							}
							text5 = text5.Trim();
							if (flag3)
							{
								paparticleEffect.mName = text5;
								text4 = text5;
								flag3 = false;
							}
							else
							{
								int num7;
								while ((num7 = text5.IndexOf(' ')) != -1)
								{
									text5 = text5.Remove(num7);
								}
								if (text5.StartsWith("x="))
								{
									paparticleEffect.mXOfs = double.Parse(text5.Substring(2), NumberStyles.Float, CultureInfo.InvariantCulture);
								}
								else if (text5.StartsWith("y="))
								{
									paparticleEffect.mYOfs = double.Parse(text5.Substring(2), NumberStyles.Float, CultureInfo.InvariantCulture);
								}
								else if (text5.ToLower() == "attachemitter")
								{
									paparticleEffect.mAttachEmitter = true;
								}
								else if (text5.ToLower() == "once")
								{
									flag2 = true;
								}
								else if (text5.ToLower() == "behind")
								{
									paparticleEffect.mBehind = true;
								}
								else if (text5.ToLower() == "transform")
								{
									paparticleEffect.mTransform = true;
								}
							}
							if (num6 == -1)
							{
								break;
							}
							text3 = text3.Substring(num6 + 1);
						}
						if (flag2)
						{
							for (int l = 0; l < theSpriteInst.mParticleEffectVector.Count; l++)
							{
								PAParticleEffect paparticleEffect2 = theSpriteInst.mParticleEffectVector[l];
								if (paparticleEffect2.mName == text4)
								{
									return;
								}
							}
						}
						string pathFrom = Common.GetPathFrom("..\\" + text4 + "\\" + text4, Common.GetFileDir(this.mLoadedPamFile, false));
						string pathFrom2 = Common.GetPathFrom(text4 + "\\" + text4, Common.GetFileDir(this.mLoadedPamFile, false));
						if (this.mListener != null)
						{
							paparticleEffect.mEffect = this.mListener.PopAnimLoadParticleEffect(text4);
						}
						if (paparticleEffect.mEffect == null)
						{
							ResourceRef resourceRef = GlobalMembers.gSexyAppBase.mResourceManager.GetResourceRefFromPath(pathFrom2 + ".ppf");
							if (resourceRef == null)
							{
								resourceRef = GlobalMembers.gSexyAppBase.mResourceManager.GetResourceRefFromPath(pathFrom + ".ppf");
							}
							if (resourceRef == null)
							{
								resourceRef = GlobalMembers.gSexyAppBase.mResourceManager.GetResourceRef(4, "PIEFFECT_" + text4.ToUpper());
							}
							if (resourceRef != null)
							{
								paparticleEffect.mEffect = resourceRef.GetPIEffect().Duplicate();
								paparticleEffect.mResourceRef = resourceRef;
							}
						}
						if (paparticleEffect.mEffect == null)
						{
							paparticleEffect.mEffect = new PIEffect();
							if (!paparticleEffect.mEffect.LoadEffect(pathFrom2 + ".ppf") && !paparticleEffect.mEffect.LoadEffect(pathFrom + ".ppf") && !paparticleEffect.mEffect.LoadEffect(pathFrom2 + ".ip3"))
							{
								bool flag4 = false;
								for (int m = 0; m < this.mImageSearchPathVector.Count; m++)
								{
									flag4 |= paparticleEffect.mEffect.LoadEffect(this.mImageSearchPathVector[m] + text4 + ".ip3");
									if (flag4)
									{
										break;
									}
								}
								if (!flag4)
								{
									if (paparticleEffect.mEffect != null)
									{
										paparticleEffect.mEffect.Dispose();
									}
									paparticleEffect.mEffect = null;
								}
							}
						}
						if (paparticleEffect.mEffect != null)
						{
							if (!this.mRandUsed)
							{
								this.mRandUsed = true;
								this.mRand.SRand(this.mRand.Next());
							}
							if (paparticleEffect.mEffect.mRandSeeds.Count > 0)
							{
								int seed = paparticleEffect.mEffect.mRandSeeds[(int)((ulong)this.mRand.Next() % (ulong)((long)paparticleEffect.mEffect.mRandSeeds.Count))];
								paparticleEffect.mEffect.mRand.SRand((uint)seed);
							}
							else
							{
								paparticleEffect.mEffect.mRand.SRand(this.mRand.Next());
							}
							paparticleEffect.mEffect.mWantsSRand = false;
							if (theObjectPos != null && theSpriteInst.mDef.mAnimRate != 0f)
							{
								int num8 = (int)(100.0 * (double)((float)theObjectPos.mPreloadFrames / theObjectPos.mTimeScale / theSpriteInst.mDef.mAnimRate) + 0.5);
								for (int n = 0; n < num8; n++)
								{
									paparticleEffect.mEffect.Update();
								}
							}
							theSpriteInst.mParticleEffectVector.Add(paparticleEffect);
						}
					}
				}
			}
		}

		public void DoFramesHit(PASpriteInst theSpriteInst, PAObjectPos theObjectPos)
		{
			PAFrame paframe = theSpriteInst.mDef.mFrames[(int)theSpriteInst.mFrameNum];
			this.FrameHit(theSpriteInst, paframe, theObjectPos);
			for (int i = 0; i < paframe.mFrameObjectPosVector.Count; i++)
			{
				PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[i];
				if (paobjectPos.mIsSprite)
				{
					PASpriteInst mSpriteInst = theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
					if (mSpriteInst != null)
					{
						this.DoFramesHit(mSpriteInst, paobjectPos);
					}
				}
			}
		}

		public void CalcObjectPos(PASpriteInst theSpriteInst, int theObjectPosIdx, bool frozen, out PATransform theTransform, out SexyColor theColor)
		{
			PAFrame paframe = theSpriteInst.mDef.mFrames[(int)theSpriteInst.mFrameNum];
			PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[theObjectPosIdx];
			PAObjectInst paobjectInst = theSpriteInst.mChildren[paobjectPos.mObjectNum];
			PAObjectPos[] array = new PAObjectPos[3];
			PAObjectPos[] array2 = array;
			int[] array3 = new int[]
			{
				theSpriteInst.mDef.mFrames.size<PAFrame>() - 1,
				1,
				2
			};
			if (theSpriteInst == this.mMainSpriteInst && theSpriteInst.mFrameNum >= (float)theSpriteInst.mDef.mWorkAreaStart)
			{
				array3[0] = theSpriteInst.mDef.mWorkAreaDuration - 1;
			}
			PATransform patransform = new PATransform();
			SexyColor color;
			if (this.mInterpolate && !frozen)
			{
				for (int i = 0; i < 3; i++)
				{
					PAFrame paframe2 = theSpriteInst.mDef.mFrames[((int)theSpriteInst.mFrameNum + array3[i]) % theSpriteInst.mDef.mFrames.size<PAFrame>()];
					if (theSpriteInst == this.mMainSpriteInst && theSpriteInst.mFrameNum >= (float)theSpriteInst.mDef.mWorkAreaStart)
					{
						paframe2 = theSpriteInst.mDef.mFrames[((int)theSpriteInst.mFrameNum + array3[i] - theSpriteInst.mDef.mWorkAreaStart) % (theSpriteInst.mDef.mWorkAreaDuration + 1) + theSpriteInst.mDef.mWorkAreaStart];
					}
					else
					{
						paframe2 = theSpriteInst.mDef.mFrames[((int)theSpriteInst.mFrameNum + array3[i]) % theSpriteInst.mDef.mFrames.size<PAFrame>()];
					}
					if (paframe.mHasStop)
					{
						paframe2 = paframe;
					}
					if (paframe2.mFrameObjectPosVector.size<PAObjectPos>() > theObjectPosIdx)
					{
						array2[i] = paframe2.mFrameObjectPosVector[theObjectPosIdx];
						if (array2[i].mObjectNum != paobjectPos.mObjectNum)
						{
							array2[i] = null;
						}
					}
					if (array2[i] == null)
					{
						for (int j = 0; j < paframe2.mFrameObjectPosVector.size<PAObjectPos>(); j++)
						{
							if (paframe2.mFrameObjectPosVector[j].mObjectNum == paobjectPos.mObjectNum)
							{
								array2[i] = paframe2.mFrameObjectPosVector[j];
								break;
							}
						}
					}
				}
				if (array2[1] != null)
				{
					float num = theSpriteInst.mFrameNum - (float)((int)theSpriteInst.mFrameNum);
					bool flag = false;
					SexyVector2 sexyVector = paobjectPos.mTransform.mMatrix * new SexyVector2(0f, 0f);
					SexyVector2 sexyVector2 = array2[1].mTransform.mMatrix * new SexyVector2(0f, 0f);
					if (array2[0] != null && array2[2] != null)
					{
						SexyVector2 v = array2[0].mTransform.mMatrix * new SexyVector2(0f, 0f);
						SexyVector2 impliedObject = array2[2].mTransform.mMatrix * new SexyVector2(0f, 0f);
						SexyVector2 v2 = sexyVector - v;
						SexyVector2 sexyVector3 = sexyVector2 - sexyVector;
						SexyVector2 impliedObject2 = impliedObject - sexyVector2;
						SexyVector2 sexyVector4 = impliedObject2 - sexyVector3;
						float num2 = Math.Max(v2.Magnitude(), impliedObject2.Magnitude()) * 0.5f + v2.Magnitude() * 0.25f + impliedObject2.Magnitude() * 0.25f;
						if (sexyVector4.Magnitude() > num2 * 4f)
						{
							flag = true;
						}
					}
					if (flag)
					{
						num = ((num < 0.5f) ? 0f : 1f);
					}
					paobjectPos.mTransform.InterpolateTo(array2[1].mTransform, num, ref patransform);
					color = new SexyColor((int)((float)PopAnim.ClrR(paobjectPos.mColorInt) * (1f - num) + (float)PopAnim.ClrR(array2[1].mColorInt) * num + 0.5f), (int)((float)PopAnim.ClrG(paobjectPos.mColorInt) * (1f - num) + (float)PopAnim.ClrG(array2[1].mColorInt) * num + 0.5f), (int)((float)PopAnim.ClrB(paobjectPos.mColorInt) * (1f - num) + (float)PopAnim.ClrB(array2[1].mColorInt) * num + 0.5f), (int)((float)PopAnim.ClrA(paobjectPos.mColorInt) * (1f - num) + (float)PopAnim.ClrA(array2[1].mColorInt) * num + 0.5f));
				}
				else
				{
					patransform.CopyFrom(paobjectPos.mTransform);
					color = new SexyColor(PopAnim.ClrR(paobjectPos.mColorInt), PopAnim.ClrG(paobjectPos.mColorInt), PopAnim.ClrB(paobjectPos.mColorInt), PopAnim.ClrA(paobjectPos.mColorInt));
				}
			}
			else
			{
				patransform.CopyFrom(paobjectPos.mTransform);
				color = new SexyColor(PopAnim.ClrR(paobjectPos.mColorInt), PopAnim.ClrG(paobjectPos.mColorInt), PopAnim.ClrB(paobjectPos.mColorInt), PopAnim.ClrA(paobjectPos.mColorInt));
			}
			patransform.mMatrix.CopyFrom(paobjectInst.mTransform * patransform.mMatrix);
			if (paobjectInst.mIsBlending && this.mBlendTicksTotal != 0f && theSpriteInst == this.mMainSpriteInst)
			{
				float num3 = this.mBlendTicksCur / this.mBlendTicksTotal;
				paobjectInst.mBlendSrcTransform.InterpolateTo(patransform, num3, ref patransform);
				color = new SexyColor((int)((float)paobjectInst.mBlendSrcColor.mRed * (1f - num3) + (float)color.mRed * num3 + 0.5f), (int)((float)paobjectInst.mBlendSrcColor.mGreen * (1f - num3) + (float)color.mGreen * num3 + 0.5f), (int)((float)paobjectInst.mBlendSrcColor.mBlue * (1f - num3) + (float)color.mBlue * num3 + 0.5f), (int)((float)paobjectInst.mBlendSrcColor.mAlpha * (1f - num3) + (float)color.mAlpha * num3 + 0.5f));
			}
			theTransform = patransform;
			theColor = color;
		}

		public void UpdateTransforms(PASpriteInst theSpriteInst, PATransform theTransform, SexyColor theColor, bool parentFrozen)
		{
			if (theTransform != null)
			{
				theSpriteInst.mCurTransform.CopyFrom(theTransform);
			}
			else
			{
				theSpriteInst.mCurTransform.mMatrix.CopyFrom(this.mTransform);
			}
			theSpriteInst.mCurColor = theColor.Clone();
			PAFrame paframe = theSpriteInst.mDef.mFrames[(int)theSpriteInst.mFrameNum];
			PATransform patransform = null;
			bool flag = parentFrozen || theSpriteInst.mDelayFrames > 0 || paframe.mHasStop;
			for (int i = 0; i < paframe.mFrameObjectPosVector.Count; i++)
			{
				PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[i];
				if (paobjectPos.mIsSprite)
				{
					SexyColor theColor2;
					this.CalcObjectPos(theSpriteInst, i, flag, out patransform, out theColor2);
					if (theTransform != null && patransform != null)
					{
						theTransform.TransformSrc(patransform, ref patransform);
					}
					this.UpdateTransforms(theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst, patransform, theColor2, flag);
				}
			}
			for (int j = 0; j < theSpriteInst.mParticleEffectVector.Count; j++)
			{
				PAParticleEffect paparticleEffect = theSpriteInst.mParticleEffectVector[j];
				if (paparticleEffect.mAttachEmitter)
				{
					if (paparticleEffect.mTransform)
					{
						SexyTransform2D theMat = new SexyTransform2D(false);
						theMat.Translate((float)paparticleEffect.mEffect.mWidth / 2f, (float)paparticleEffect.mEffect.mHeight / 2f);
						SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
						sexyTransform2D.CopyFrom(theSpriteInst.mCurTransform.mMatrix * theMat);
						paparticleEffect.mEffect.mEmitterTransform.CopyFrom(sexyTransform2D);
					}
					else
					{
						SexyVector2 sexyVector = theSpriteInst.mCurTransform.mMatrix * new SexyVector2((float)paparticleEffect.mXOfs, (float)paparticleEffect.mYOfs);
						SexyTransform2D sexyTransform2D2 = new SexyTransform2D(false);
						sexyTransform2D2.Translate(sexyVector.x, sexyVector.y);
						paparticleEffect.mEffect.mEmitterTransform.CopyFrom(sexyTransform2D2);
					}
					paparticleEffect.mEffect.mEmitterTransform.Translate(this.mParticleAttachOffset.mX, this.mParticleAttachOffset.mY);
				}
			}
		}

		public void UpdateParticles(PASpriteInst theSpriteInst, PAObjectPos theObjectPos)
		{
			if (theSpriteInst == null)
			{
				return;
			}
			for (int i = 0; i < theSpriteInst.mParticleEffectVector.Count; i++)
			{
				PAParticleEffect paparticleEffect = theSpriteInst.mParticleEffectVector[i];
				SexyVector2 sexyVector = default(SexyVector2);
				if (!paparticleEffect.mAttachEmitter)
				{
					sexyVector = theSpriteInst.mCurTransform.mMatrix * new SexyVector2((float)paparticleEffect.mXOfs, (float)paparticleEffect.mYOfs);
				}
				paparticleEffect.mEffect.mDrawTransform.LoadIdentity();
				paparticleEffect.mEffect.mDrawTransform.Translate(sexyVector.x, sexyVector.y);
				if (this.mMirror)
				{
					sexyVector.x = (float)this.mAnimRect.mWidth - sexyVector.x;
					paparticleEffect.mEffect.mDrawTransform.Translate((float)(-(float)(this.mAnimRect.mWidth / 2)), 0f);
					paparticleEffect.mEffect.mDrawTransform.Scale(-1f, 1f);
					paparticleEffect.mEffect.mDrawTransform.Translate((float)(this.mAnimRect.mWidth / 2), 0f);
				}
				paparticleEffect.mEffect.mDrawTransform.Scale(this.mDrawScale, this.mDrawScale);
				if (paparticleEffect.mTransform && theObjectPos != null)
				{
					paparticleEffect.mEffect.mAnimSpeed = 1f / theObjectPos.mTimeScale;
				}
				paparticleEffect.mEffect.Update();
				paparticleEffect.mLastUpdated = this.mUpdateCnt;
				if (!paparticleEffect.mEffect.IsActive())
				{
					if (paparticleEffect.mEffect != null)
					{
						paparticleEffect.mEffect.Dispose();
					}
					theSpriteInst.mParticleEffectVector.RemoveAt(i);
					i--;
				}
			}
			PAFrame paframe = theSpriteInst.mDef.mFrames[(int)theSpriteInst.mFrameNum];
			for (int j = 0; j < paframe.mFrameObjectPosVector.Count; j++)
			{
				PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[j];
				if (paobjectPos.mIsSprite)
				{
					PASpriteInst mSpriteInst = theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
					this.UpdateParticles(mSpriteInst, paobjectPos);
				}
			}
		}

		public void CleanParticles(PASpriteInst theSpriteInst)
		{
			this.CleanParticles(theSpriteInst, false);
		}

		public void CleanParticles(PASpriteInst theSpriteInst, bool force)
		{
			if (theSpriteInst == null)
			{
				return;
			}
			for (int i = 0; i < theSpriteInst.mParticleEffectVector.Count; i++)
			{
				PAParticleEffect paparticleEffect = theSpriteInst.mParticleEffectVector[i];
				if (paparticleEffect.mLastUpdated != this.mUpdateCnt || force)
				{
					if (paparticleEffect.mEffect != null)
					{
						paparticleEffect.mEffect.Dispose();
					}
					theSpriteInst.mParticleEffectVector.RemoveAt(i);
					i--;
				}
			}
			for (int j = 0; j < theSpriteInst.mChildren.Count; j++)
			{
				PASpriteInst mSpriteInst = theSpriteInst.mChildren[j].mSpriteInst;
				if (mSpriteInst != null)
				{
					this.CleanParticles(mSpriteInst, force);
				}
			}
		}

		public bool HasParticles(PASpriteInst theSpriteInst)
		{
			if (theSpriteInst == null)
			{
				return false;
			}
			if (theSpriteInst.mParticleEffectVector.Count != 0)
			{
				return true;
			}
			for (int i = 0; i < theSpriteInst.mChildren.Count; i++)
			{
				PASpriteInst mSpriteInst = theSpriteInst.mChildren[i].mSpriteInst;
				if (mSpriteInst != null && this.HasParticles(mSpriteInst))
				{
					return true;
				}
			}
			return false;
		}

		public void IncSpriteInstFrame(PASpriteInst theSpriteInst, PAObjectPos theObjectPos, float theFrac)
		{
			int num = (int)theSpriteInst.mFrameNum;
			PAFrame paframe = theSpriteInst.mDef.mFrames[num];
			if (paframe.mHasStop)
			{
				return;
			}
			float num2 = ((theObjectPos != null) ? theObjectPos.mTimeScale : 1f);
			theSpriteInst.mFrameNum += theFrac * (theSpriteInst.mDef.mAnimRate / (1000f / GlobalMembers.gSexyAppBase.mFrameTime)) / num2;
			if (theSpriteInst == this.mMainSpriteInst)
			{
				if (!theSpriteInst.mDef.mFrames[theSpriteInst.mDef.mFrames.Count - 1].mHasStop)
				{
					if ((int)theSpriteInst.mFrameNum >= theSpriteInst.mDef.mWorkAreaStart + theSpriteInst.mDef.mWorkAreaDuration + 1)
					{
						theSpriteInst.mFrameRepeats++;
						theSpriteInst.mFrameNum -= (float)(theSpriteInst.mDef.mWorkAreaDuration + 1);
					}
				}
				else if ((int)theSpriteInst.mFrameNum >= theSpriteInst.mDef.mWorkAreaStart + theSpriteInst.mDef.mWorkAreaDuration)
				{
					theSpriteInst.mOnNewFrame = true;
					theSpriteInst.mFrameNum = (float)(theSpriteInst.mDef.mWorkAreaStart + theSpriteInst.mDef.mWorkAreaDuration);
					if (theSpriteInst.mDef.mWorkAreaDuration != 0)
					{
						this.mAnimRunning = false;
						if (this.mListener != null)
						{
							this.mListener.PopAnimStopped(this.mId);
						}
						return;
					}
					theSpriteInst.mFrameRepeats++;
				}
			}
			else if ((int)theSpriteInst.mFrameNum >= theSpriteInst.mDef.mFrames.Count)
			{
				theSpriteInst.mFrameRepeats++;
				theSpriteInst.mFrameNum -= (float)theSpriteInst.mDef.mFrames.Count;
			}
			theSpriteInst.mOnNewFrame = (int)theSpriteInst.mFrameNum != num;
			if (theSpriteInst.mOnNewFrame && theSpriteInst.mDelayFrames > 0)
			{
				theSpriteInst.mOnNewFrame = false;
				theSpriteInst.mFrameNum = (float)num;
				theSpriteInst.mDelayFrames--;
				return;
			}
			for (int i = 0; i < paframe.mFrameObjectPosVector.Count; i++)
			{
				PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[i];
				if (paobjectPos.mIsSprite)
				{
					PASpriteInst mSpriteInst = theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
					this.IncSpriteInstFrame(mSpriteInst, paobjectPos, theFrac / num2);
				}
			}
		}

		public void PrepSpriteInstFrame(PASpriteInst theSpriteInst, PAObjectPos theObjectPos)
		{
			PAFrame paframe = theSpriteInst.mDef.mFrames[(int)theSpriteInst.mFrameNum];
			if (theSpriteInst.mOnNewFrame)
			{
				this.FrameHit(theSpriteInst, paframe, theObjectPos);
			}
			if (paframe.mHasStop)
			{
				if (theSpriteInst == this.mMainSpriteInst)
				{
					this.mAnimRunning = false;
					if (this.mListener != null)
					{
						this.mListener.PopAnimStopped(this.mId);
					}
				}
				return;
			}
			for (int i = 0; i < paframe.mFrameObjectPosVector.Count; i++)
			{
				PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[i];
				if (paobjectPos.mIsSprite)
				{
					PASpriteInst mSpriteInst = theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
					if (mSpriteInst != null)
					{
						int num = (int)theSpriteInst.mFrameNum + theSpriteInst.mFrameRepeats * theSpriteInst.mDef.mFrames.Count;
						int num2 = num - 1;
						if (mSpriteInst.mLastUpdated != num2 && mSpriteInst.mLastUpdated != num)
						{
							mSpriteInst.mFrameNum = 0f;
							mSpriteInst.mFrameRepeats = 0;
							mSpriteInst.mDelayFrames = 0;
							mSpriteInst.mOnNewFrame = true;
						}
						this.PrepSpriteInstFrame(mSpriteInst, paobjectPos);
						mSpriteInst.mLastUpdated = num;
					}
				}
			}
		}

		public void AnimUpdate(float theFrac)
		{
			if (!this.mAnimRunning)
			{
				return;
			}
			if (this.mBlendTicksTotal > 0f)
			{
				this.mBlendTicksCur += theFrac;
				if (this.mBlendTicksCur >= this.mBlendTicksTotal)
				{
					this.mBlendTicksTotal = 0f;
				}
			}
			this.mTransDirty = true;
			if (this.mBlendDelay > 0f)
			{
				this.mBlendDelay -= theFrac;
				if (this.mBlendDelay <= 0f)
				{
					this.mBlendDelay = 0f;
					this.DoFramesHit(this.mMainSpriteInst, null);
				}
				return;
			}
			this.IncSpriteInstFrame(this.mMainSpriteInst, null, theFrac);
			this.PrepSpriteInstFrame(this.mMainSpriteInst, null);
			this.MarkDirty();
		}

		public void ResetAnimHelper(PASpriteInst theSpriteInst)
		{
			theSpriteInst.mFrameNum = 0f;
			theSpriteInst.mFrameRepeats = 0;
			theSpriteInst.mDelayFrames = 0;
			theSpriteInst.mLastUpdated = -1;
			theSpriteInst.mOnNewFrame = true;
			for (int i = 0; i < theSpriteInst.mParticleEffectVector.Count; i++)
			{
				PAParticleEffect paparticleEffect = theSpriteInst.mParticleEffectVector[i];
				paparticleEffect.mEffect.ResetAnim();
			}
			for (int j = 0; j < theSpriteInst.mChildren.Count; j++)
			{
				PASpriteInst mSpriteInst = theSpriteInst.mChildren[j].mSpriteInst;
				if (mSpriteInst != null)
				{
					this.ResetAnimHelper(mSpriteInst);
				}
			}
			this.mTransDirty = true;
		}

		public void SaveStateSpriteInst(ref SexyBuffer theBuffer, PASpriteInst theSpriteInst)
		{
			theBuffer.WriteLong((theSpriteInst.mFrameNum * 65536f));
			theBuffer.WriteLong(theSpriteInst.mDelayFrames);
			theBuffer.WriteLong(theSpriteInst.mLastUpdated);
			theBuffer.WriteShort((short)theSpriteInst.mParticleEffectVector.Count);
			for (int i = 0; i < theSpriteInst.mParticleEffectVector.Count; i++)
			{
				PAParticleEffect paparticleEffect = theSpriteInst.mParticleEffectVector[i];
				paparticleEffect.mEffect.SaveState(theBuffer);
				theBuffer.WriteStringWithEncoding(paparticleEffect.mName);
				theBuffer.WriteBoolean(paparticleEffect.mBehind);
				theBuffer.WriteBoolean(paparticleEffect.mAttachEmitter);
				theBuffer.WriteBoolean(paparticleEffect.mTransform);
				theBuffer.WriteLong(((int)(paparticleEffect.mXOfs * 65536.0)));
				theBuffer.WriteLong(((int)(paparticleEffect.mYOfs * 65536.0)));
			}
			for (int j = 0; j < theSpriteInst.mChildren.Count; j++)
			{
				PAObjectInst paobjectInst = theSpriteInst.mChildren[j];
				if (paobjectInst.mSpriteInst != null)
				{
					this.SaveStateSpriteInst(ref theBuffer, paobjectInst.mSpriteInst);
				}
			}
		}

		public void LoadStateSpriteInst(SexyBuffer theBuffer, PASpriteInst theSpriteInst)
		{
			theSpriteInst.mFrameNum = (float)theBuffer.ReadLong() / 65536f;
			theSpriteInst.mFrameRepeats = 0;
			theSpriteInst.mDelayFrames = (int)theBuffer.ReadLong();
			theSpriteInst.mLastUpdated = (int)theBuffer.ReadLong();
			theSpriteInst.mOnNewFrame = false;
			int num = (int)theBuffer.ReadShort();
			for (int i = 0; i < num; i++)
			{
				PAParticleEffect paparticleEffect = new PAParticleEffect();
				paparticleEffect.mEffect.LoadState(theBuffer);
				paparticleEffect.mName = theBuffer.ReadStringWithEncoding();
				paparticleEffect.mBehind = theBuffer.ReadBoolean();
				paparticleEffect.mAttachEmitter = theBuffer.ReadBoolean();
				paparticleEffect.mTransform = theBuffer.ReadBoolean();
				paparticleEffect.mXOfs = (double)((float)theBuffer.ReadLong() / 65536f);
				paparticleEffect.mYOfs = (double)((float)theBuffer.ReadLong() / 65536f);
				theSpriteInst.mParticleEffectVector.Add(paparticleEffect);
			}
			for (int j = 0; j < theSpriteInst.mChildren.Count; j++)
			{
				PAObjectInst paobjectInst = theSpriteInst.mChildren[j];
				if (paobjectInst.mSpriteInst != null)
				{
					this.LoadStateSpriteInst(theBuffer, paobjectInst.mSpriteInst);
				}
			}
		}

		public void DrawParticleEffects(Graphics g, PASpriteInst theSpriteInst, PATransform theTransform, SexyColor theColor, bool front)
		{
			if (theSpriteInst.mParticleEffectVector.Count > 0)
			{
				for (int i = 0; i < theSpriteInst.mParticleEffectVector.Count; i++)
				{
					PAParticleEffect paparticleEffect = theSpriteInst.mParticleEffectVector[i];
					if (paparticleEffect.mTransform)
					{
						if (!paparticleEffect.mAttachEmitter)
						{
							SexyTransform2D theMat = new SexyTransform2D(false);
							theMat.Translate((float)paparticleEffect.mEffect.mWidth / 2f, (float)paparticleEffect.mEffect.mHeight / 2f);
							SexyTransform2D sexyTransform2D = new SexyTransform2D(false);
							sexyTransform2D.CopyFrom(theTransform.mMatrix * theMat);
							paparticleEffect.mEffect.mDrawTransform.CopyFrom(sexyTransform2D);
						}
						else
						{
							paparticleEffect.mEffect.mDrawTransform.LoadIdentity();
							paparticleEffect.mEffect.mDrawTransform.Translate(-this.mParticleAttachOffset.mX, -this.mParticleAttachOffset.mY);
						}
						paparticleEffect.mEffect.mColor = theColor;
					}
					if (paparticleEffect.mBehind == !front)
					{
						paparticleEffect.mEffect.Draw(g);
					}
				}
			}
		}

		public virtual void DrawSprite(Graphics g, PASpriteInst theSpriteInst, PATransform theTransform, SexyColor theColor, bool additive, bool parentFrozen)
		{
			this.DrawParticleEffects(g, theSpriteInst, theTransform, theColor, false);
			PAFrame paframe = theSpriteInst.mDef.mFrames[(int)theSpriteInst.mFrameNum];
			PATransform patransform = null;
			bool flag = parentFrozen || theSpriteInst.mDelayFrames > 0 || paframe.mHasStop;
			for (int i = 0; i < paframe.mFrameObjectPosVector.Count; i++)
			{
				PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[i];
				PAObjectInst paobjectInst = theSpriteInst.mChildren[paobjectPos.mObjectNum];
				if (this.mListener != null && paobjectInst.mPredrawCallback)
				{
					paobjectInst.mPredrawCallback = this.mListener.PopAnimObjectPredraw(this.mId, g, theSpriteInst, paobjectInst, theTransform, theColor);
				}
				SexyColor mCurColor;
				if (paobjectPos.mIsSprite)
				{
					PASpriteInst mSpriteInst = theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
					mCurColor = mSpriteInst.mCurColor;
					patransform = new PATransform();
					patransform.CopyFrom(mSpriteInst.mCurTransform);
				}
				else
				{
					this.CalcObjectPos(theSpriteInst, i, flag, out patransform, out mCurColor);
				}
				PATransform patransform2 = new PATransform();
				if (theTransform == null && this.mDrawScale != 1f)
				{
					PATransform patransform3 = new PATransform();
					patransform3.mMatrix.m00 = this.mDrawScale;
					patransform3.mMatrix.m11 = this.mDrawScale;
					patransform3.mMatrix.CopyFrom(this.mTransform * patransform3.mMatrix);
					patransform3.TransformSrc(patransform, ref patransform2);
				}
				else if (theTransform == null || paobjectPos.mIsSprite)
				{
					patransform2.CopyFrom(patransform);
					if (this.mDrawScale != 1f)
					{
						PATransform patransform4 = new PATransform();
						patransform4.mMatrix.m00 = this.mDrawScale;
						patransform4.mMatrix.m11 = this.mDrawScale;
						patransform2.mMatrix.CopyFrom(patransform4.mMatrix * patransform2.mMatrix);
					}
					patransform2.mMatrix.CopyFrom(this.mTransform * patransform2.mMatrix);
				}
				else
				{
					theTransform.TransformSrc(patransform, ref patransform2);
				}
				SexyColor color = new SexyColor(mCurColor.mRed * theColor.mRed * paobjectInst.mColorMult.mRed / 65025, mCurColor.mGreen * theColor.mGreen * paobjectInst.mColorMult.mGreen / 65025, mCurColor.mBlue * theColor.mBlue * paobjectInst.mColorMult.mBlue / 65025, mCurColor.mAlpha * theColor.mAlpha * paobjectInst.mColorMult.mAlpha / 65025);
				if (color.mAlpha != 0)
				{
					if (paobjectPos.mIsSprite)
					{
						PASpriteInst mSpriteInst2 = theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
						this.DrawSprite(g, mSpriteInst2, patransform2, color, paobjectPos.mIsAdditive || additive, flag);
					}
					else
					{
						int num = 0;
						for (;;)
						{
							PAImage paimage = this.mImageVector[(int)paobjectPos.mResNum];
							PATransform patransform5 = new PATransform();
							patransform2.TransformSrc(paimage.mTransform, ref patransform5);
							g.SetColorizeImages(true);
							g.SetColor(color);
							if (additive || paobjectPos.mIsAdditive)
							{
								g.SetDrawMode(1);
							}
							else
							{
								g.SetDrawMode(paimage.mDrawMode);
							}
							DeviceImage deviceImage = new DeviceImage();
							Rect theSrcRect = default(Rect);
							if (paobjectPos.mAnimFrameNum == 0 || Enumerable.Count<SharedImageRef>(paimage.mImages) == 1)
							{
								deviceImage = (DeviceImage)paimage.mImages[0].GetImage();
								theSrcRect = deviceImage.GetCelRect(paobjectPos.mAnimFrameNum);
							}
							else
							{
								deviceImage = (DeviceImage)paimage.mImages[paobjectPos.mAnimFrameNum].GetImage();
								theSrcRect = deviceImage.GetCelRect(0);
							}
							if (paobjectPos.mHasSrcRect)
							{
								theSrcRect = paobjectPos.mSrcRect;
							}
							if (this.mImgScale != 1f)
							{
								float m = patransform5.mMatrix.m02;
								float m2 = patransform5.mMatrix.m12;
								PATransform patransform6 = new PATransform();
								patransform6.mMatrix.m00 = 1f / this.mImgScale;
								patransform6.mMatrix.m11 = 1f / this.mImgScale;
								patransform6.TransformSrc(patransform5, ref patransform5);
								patransform5.mMatrix.m02 = m;
								patransform5.mMatrix.m12 = m2;
							}
							ImagePredrawResult imagePredrawResult = ImagePredrawResult.ImagePredraw_DontAsk;
							if (this.mListener != null && paobjectInst.mImagePredrawCallback)
							{
								imagePredrawResult = this.mListener.PopAnimImagePredraw(this.mId, theSpriteInst, paobjectInst, patransform5, deviceImage, g, num);
								if (imagePredrawResult == ImagePredrawResult.ImagePredraw_DontAsk)
								{
									paobjectInst.mImagePredrawCallback = false;
								}
								if (imagePredrawResult == ImagePredrawResult.ImagePredraw_Skip)
								{
									break;
								}
							}
							SexyTransform2D theMat = new SexyTransform2D(false);
							theMat.LoadIdentity();
							theMat.m02 = (float)theSrcRect.mWidth / 2f;
							theMat.m12 = (float)theSrcRect.mHeight / 2f;
							patransform5.mMatrix.CopyFrom(patransform5.mMatrix * theMat);
							if (this.mColorizeType)
							{
								g.SetColor(255, 0, 0);
							}
							g.DrawImageMatrix(deviceImage, patransform5.mMatrix, theSrcRect);
							if (imagePredrawResult != ImagePredrawResult.ImagePredraw_Repeat)
							{
								break;
							}
							num++;
						}
					}
					if (this.mListener != null && paobjectInst.mPostdrawCallback)
					{
						paobjectInst.mPostdrawCallback = this.mListener.PopAnimObjectPostdraw(this.mId, g, theSpriteInst, paobjectInst, theTransform, theColor);
					}
				}
			}
			this.DrawParticleEffects(g, theSpriteInst, theTransform, theColor, true);
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		public virtual void DrawSpriteMirrored(Graphics g, PASpriteInst theSpriteInst, PATransform theTransform, SexyColor theColor, bool additive, bool parentFrozen)
		{
			this.DrawParticleEffects(g, theSpriteInst, theTransform, new SexyColor(theColor), false);
			PAFrame paframe = theSpriteInst.mDef.mFrames[(int)theSpriteInst.mFrameNum];
			PATransform patransform = null;
			bool flag = parentFrozen || theSpriteInst.mDelayFrames > 0 || paframe.mHasStop;
			for (int i = 0; i < paframe.mFrameObjectPosVector.Count; i++)
			{
				PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[i];
				SexyColor mCurColor;
				if (paobjectPos.mIsSprite)
				{
					PASpriteInst mSpriteInst = theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
					mCurColor = mSpriteInst.mCurColor;
					patransform = mSpriteInst.mCurTransform;
				}
				else
				{
					this.CalcObjectPos(theSpriteInst, i, flag, out patransform, out mCurColor);
				}
				PATransform patransform2 = new PATransform();
				if (theTransform == null && this.mDrawScale != 1f)
				{
					PATransform patransform3 = new PATransform();
					patransform3.mMatrix.m00 = this.mDrawScale;
					patransform3.mMatrix.m11 = this.mDrawScale;
					patransform3.TransformSrc(patransform, ref patransform2);
				}
				else if (theTransform == null || paobjectPos.mIsSprite)
				{
					patransform2.CopyFrom(patransform);
				}
				else
				{
					theTransform.TransformSrc(patransform, ref patransform2);
				}
				SexyColor color = new SexyColor(mCurColor.mRed * theColor.mRed / 255, mCurColor.mGreen * theColor.mGreen / 255, mCurColor.mBlue * theColor.mBlue / 255, mCurColor.mAlpha * theColor.mAlpha / 255);
				if (color.mAlpha != 0)
				{
					if (paobjectPos.mIsSprite)
					{
						this.DrawSpriteMirrored(g, theSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst, patransform2, new SexyColor(color), paobjectPos.mIsAdditive || additive, flag);
					}
					else
					{
						PAImage paimage = this.mImageVector[(int)paobjectPos.mResNum];
						PATransform patransform4 = new PATransform();
						patransform2.TransformSrc(paimage.mTransform, ref patransform4);
						g.SetColorizeImages(true);
						g.SetColor(color);
						if (additive || paobjectPos.mIsAdditive)
						{
							g.SetDrawMode(1);
						}
						else
						{
							g.SetDrawMode(paimage.mDrawMode);
						}
						Rect theSrcRect = default(Rect);
						Image image;
						if (paobjectPos.mAnimFrameNum == 0 || paimage.mImages.Count == 1)
						{
							image = paimage.mImages[0].GetImage();
							theSrcRect = image.GetCelRect(paobjectPos.mAnimFrameNum);
						}
						else
						{
							image = paimage.mImages[paobjectPos.mAnimFrameNum].GetImage();
							theSrcRect = image.GetCelRect(0);
						}
						if (paobjectPos.mHasSrcRect)
						{
							theSrcRect = paobjectPos.mSrcRect;
						}
						if (this.mImgScale != 1f)
						{
							float m = patransform4.mMatrix.m02;
							float m2 = patransform4.mMatrix.m12;
							PATransform patransform5 = new PATransform();
							patransform5.mMatrix.m00 = 1f / this.mImgScale;
							patransform5.mMatrix.m11 = 1f / this.mImgScale;
							patransform5.TransformSrc(patransform4, ref patransform4);
							patransform4.mMatrix.m02 = m;
							patransform4.mMatrix.m12 = m2;
						}
						if (this.mDrawScale != 1f)
						{
							PATransform patransform6 = patransform4;
							patransform6.mMatrix.m02 = patransform6.mMatrix.m02 + (float)this.mAnimRect.mWidth * (1f - this.mDrawScale);
						}
						if ((double)patransform4.mMatrix.m00 == 1.0 && patransform4.mMatrix.m01 == 0f && patransform4.mMatrix.m10 == 0f && (double)patransform4.mMatrix.m11 == 1.0)
						{
							float theX = (float)(this.mAnimRect.mWidth - theSrcRect.mWidth) / 2f - (patransform4.mMatrix.m02 + (float)(theSrcRect.mWidth - this.mAnimRect.mWidth) / 2f);
							float m3 = patransform4.mMatrix.m12;
							g.DrawImageF(image, theX, m3, theSrcRect);
						}
						else if (this.mVersion == 1 || (patransform4.mMatrix.m00 == patransform4.mMatrix.m11 && patransform4.mMatrix.m01 == -patransform4.mMatrix.m10 && Math.Abs((double)(patransform4.mMatrix.m00 * patransform4.mMatrix.m00 + patransform4.mMatrix.m01 * patransform4.mMatrix.m01) - 1.0) < 0.01))
						{
							float num = (float)Math.Atan2((double)patransform4.mMatrix.m01, (double)patransform4.mMatrix.m00);
							float num2 = -num;
							float num3 = patransform4.mMatrix.m02 + (float)Math.Cos((double)num2) * (float)theSrcRect.mWidth / 2f - (float)Math.Sin((double)num2) * (float)theSrcRect.mHeight / 2f;
							float num4 = patransform4.mMatrix.m12 + (float)Math.Sin((double)num2) * (float)theSrcRect.mWidth / 2f + (float)Math.Cos((double)num2) * (float)theSrcRect.mHeight / 2f;
							float num5 = num3 - (float)theSrcRect.mWidth / 2f;
							float theY = num4 - (float)theSrcRect.mHeight / 2f;
							num5 = (float)(this.mAnimRect.mWidth - theSrcRect.mWidth) / 2f - (num5 + (float)(theSrcRect.mWidth - this.mAnimRect.mWidth) / 2f);
							g.DrawImageRotatedF(image, num5, theY, (double)(-(double)num), theSrcRect);
						}
						else
						{
							SexyTransform2D theMat = new SexyTransform2D(false);
							theMat.m02 = (float)theSrcRect.mWidth / 2f;
							theMat.m12 = (float)theSrcRect.mHeight / 2f;
							patransform4.mMatrix.CopyFrom(patransform4.mMatrix * theMat);
							patransform4.mMatrix.m02 = (float)this.mAnimRect.mWidth - patransform4.mMatrix.m02;
							patransform4.mMatrix.m01 = -patransform4.mMatrix.m01;
							patransform4.mMatrix.m10 = -patransform4.mMatrix.m10;
							g.DrawImageMatrix(image, patransform4.mMatrix, theSrcRect);
						}
					}
				}
			}
			this.DrawParticleEffects(g, theSpriteInst, theTransform, theColor, true);
			g.SetColorizeImages(false);
			g.SetDrawMode(0);
		}

		public virtual void Load_Init()
		{
			this.Clear();
		}

		public virtual void Load_SetModPamFile(string theFileName)
		{
			this.mModPamFile = theFileName;
		}

		public virtual void Load_AddRemap(string theWildcard, string theReplacement)
		{
			this.mRemapList.AddLast(new KeyValuePair<string, string>(theWildcard, theReplacement));
		}

		public virtual bool Load_LoadPam(string theFileName)
		{
			this.mLoadedPamFile = theFileName;
			if (this.mMainAnimDef != null)
			{
				return false;
			}
			this.mMainSpriteInst = new PASpriteInst();
			this.mMainAnimDef = new PopAnimDef();
			this.mMainSpriteInst.mParent = null;
			this.mMainSpriteInst.mDef = null;
			SexyBuffer buffer = new SexyBuffer();
			string fileDir = Common.GetFileDir(theFileName, false);
			if (!GlobalMembers.gSexyAppBase.ReadBufferFromStream(theFileName, ref buffer))
			{
				return this.Fail("Unable to load file: " + theFileName);
			}
			uint num = (uint)buffer.ReadLong();
			if ((ulong)num != PopAnim.PAM_MAGIC)
			{
				return this.Fail("Invalid header");
			}
			this.mVersion = (int)buffer.ReadLong();
			if ((ulong)this.mVersion > (ulong)((long)PopAnim.PAM_VERSION))
			{
				return this.Fail("Invalid version");
			}
			this.mAnimRate = (int)buffer.ReadByte();
			this.mAnimRect.mX = (int)(buffer.ReadShort() / 20);
			this.mAnimRect.mY = (int)(buffer.ReadShort() / 20);
			this.mAnimRect.mWidth = (int)((ushort)buffer.ReadShort() / 20);
			this.mAnimRect.mHeight = (int)((ushort)buffer.ReadShort() / 20);
			int num2 = (int)buffer.ReadShort();
			this.mImageVector.Resize(num2);
			for (int i = 0; i < num2; i++)
			{
				PAImage paimage = this.mImageVector[i];
				paimage.mDrawMode = 0;
				string theString = buffer.ReadStringWithEncoding();
				string text = this.Remap(theString);
				string text2 = "";
				int num3 = text.IndexOf('(');
				int num4 = text.IndexOf(')');
				if (num3 != -1 && num4 != -1 && num3 < num4)
				{
					text2 = text.Substring(num3 + 1, num4 - num3 - 1).ToLower();
					text = (text.Substring(0, num3) + text.Substring(num4 + 1)).Trim();
				}
				else
				{
					num4 = text.IndexOf('$');
					if (num4 != -1)
					{
						text2 = text.Substring(0, num4).ToLower();
						text = text.Substring(num4 + 1).Trim();
					}
				}
				paimage.mCols = 1;
				paimage.mRows = 1;
				num3 = text.IndexOf('[');
				num4 = text.IndexOf(']');
				if (num3 != -1 && num4 != -1 && num3 < num4)
				{
					string text3 = text.Substring(num3 + 1, num4 - num3 - 1).ToLower();
					text = (text.Substring(0, num3) + text.Substring(num4 + 1)).Trim();
					int num5 = text3.IndexOf(',');
					if (num5 != -1)
					{
						paimage.mCols = Convert.ToInt32(text3.Substring(0, num5));
						paimage.mRows = Convert.ToInt32(text3.Substring(num5 + 1));
					}
				}
				if (text2.IndexOf("add") != -1)
				{
					paimage.mDrawMode = 1;
				}
				if (this.mVersion >= 4)
				{
					paimage.mOrigWidth = (int)buffer.ReadShort();
					paimage.mOrigHeight = (int)buffer.ReadShort();
				}
				else
				{
					paimage.mOrigWidth = -1;
					paimage.mOrigHeight = -1;
				}
				if (this.mVersion == 1)
				{
					float num6 = (float)buffer.ReadShort() / 1000f;
					float num7 = (float)Math.Sin((double)num6);
					float num8 = (float)Math.Cos((double)num6);
					paimage.mTransform.mMatrix.m00 = num8;
					paimage.mTransform.mMatrix.m01 = -num7;
					paimage.mTransform.mMatrix.m10 = num7;
					paimage.mTransform.mMatrix.m11 = num8;
					paimage.mTransform.mMatrix.m02 = (float)buffer.ReadShort() / 20f;
					paimage.mTransform.mMatrix.m12 = (float)buffer.ReadShort() / 20f;
				}
				else
				{
					paimage.mTransform.mMatrix.m00 = (float)buffer.ReadLong() / 1310720f;
					paimage.mTransform.mMatrix.m01 = (float)buffer.ReadLong() / 1310720f;
					paimage.mTransform.mMatrix.m10 = (float)buffer.ReadLong() / 1310720f;
					paimage.mTransform.mMatrix.m11 = (float)buffer.ReadLong() / 1310720f;
					paimage.mTransform.mMatrix.m02 = (float)buffer.ReadShort() / 20f;
					paimage.mTransform.mMatrix.m12 = (float)buffer.ReadShort() / 20f;
				}
				paimage.mImageName = text;
				if (paimage.mImageName.Length == 0)
				{
					bool flag = false;
					SharedImageRef sharedImageRef = new SharedImageRef();
					sharedImageRef = GlobalMembers.gSexyAppBase.GetSharedImage("!whitepixel", "", ref flag, true, false);
					paimage.mImages.Add(sharedImageRef);
				}
				else
				{
					int num9;
					for (int j = 0; j < text.Length; j = num9 + 1)
					{
						num9 = text.IndexOf(',', j);
						string text4;
						if (num9 != -1)
						{
							text4 = text.Substring(j, num9 - j);
						}
						else
						{
							text4 = text.Substring(j);
						}
						this.Load_GetImage(paimage, fileDir, text4, text4);
						if (num9 == -1)
						{
							break;
						}
					}
				}
				if (this.mError.Length > 0)
				{
					return false;
				}
				if (this.mMirror && this.mLoadedImageIsNew)
				{
					for (int k = 0; k < paimage.mImages.Count; k++)
					{
						GlobalMembers.gSexyAppBase.MirrorImage(paimage.mImages[k].GetImage());
					}
				}
				this.Load_PostImageLoadHook(paimage);
			}
			this.mMotionFilePos = buffer.mReadBitPos / 8;
			int num10 = (int)buffer.ReadShort();
			this.mMainAnimDef.mSpriteDefVector.Resize(num10);
			for (int l = 0; l < num10; l++)
			{
				if (!this.LoadSpriteDef(buffer, this.mMainAnimDef.mSpriteDefVector[l]))
				{
					return false;
				}
			}
			bool flag2 = this.mVersion <= 3 || buffer.ReadBoolean();
			if (flag2 && !this.LoadSpriteDef(buffer, this.mMainAnimDef.mMainSpriteDef))
			{
				return false;
			}
			this.mLoaded = true;
			this.mRandUsed = false;
			return true;
		}

		public virtual bool Load_LoadMod(string theFileName)
		{
			PopAnimModParser popAnimModParser = new PopAnimModParser();
			popAnimModParser.mErrorHeader = "PopAnim Mod File Error in " + theFileName + "\r\n";
			popAnimModParser.mPopAnim = this;
			popAnimModParser.mPassNum = 1;
			if (!popAnimModParser.LoadDescriptor(theFileName))
			{
				return false;
			}
			if (this.mModPamFile.Length == 0)
			{
				return this.Fail("No Pam file specified");
			}
			string pathFrom = Common.GetPathFrom(this.mModPamFile, Common.GetFileDir(theFileName, false));
			if (!this.Load_LoadPam(pathFrom))
			{
				return popAnimModParser.Error("Failed to load Pam: " + this.mModPamFile + "\r\n\r\n" + this.mError);
			}
			popAnimModParser.mPassNum = 2;
			return popAnimModParser.LoadDescriptor(theFileName);
		}

		public virtual SharedImageRef Load_GetImageHook(string theFileDir, string theOrigName, string theRemappedName)
		{
			if (theRemappedName.Length == 0)
			{
				this.Fail("No image file name specified");
				return null;
			}
			for (int i = 0; i < this.mImageSearchPathVector.Count; i++)
			{
				string text = Common.GetPathFrom(this.mImageSearchPathVector[i], theFileDir);
				if (text.Length > 0 && text[text.Length - 1] != '\\' && text[text.Length - 1] != '/')
				{
					text += "\\";
				}
				text += theRemappedName;
				SharedImageRef sharedImage = GlobalMembers.gSexyAppBase.GetSharedImage(text, this.mMirror ? "MIRRORED" : "", ref this.mLoadedImageIsNew, true, false);
				if (sharedImage.GetImage() != null)
				{
					return sharedImage;
				}
			}
			this.Fail(string.Concat(new string[] { "Unable to load image: ", theRemappedName, " (", theOrigName, ")" }));
			return null;
		}

		public virtual bool Load_GetImage(PAImage theImage, string theFileDir, string theOrigName, string theRemappedName)
		{
			SharedImageRef sharedImageRef = this.Load_GetImageHook(theFileDir, theOrigName, theRemappedName);
			if (sharedImageRef.GetDeviceImage() == null)
			{
				return false;
			}
			sharedImageRef.GetImage().mNumCols = theImage.mCols;
			sharedImageRef.GetImage().mNumRows = theImage.mRows;
			if (theImage.mImages.Count == 0 && theImage.mOrigWidth != -1 && theImage.mOrigHeight != -1)
			{
				PATransform patransform = theImage.mTransform;
				patransform.mMatrix.m02 = patransform.mMatrix.m02 + -((float)sharedImageRef.mWidth - (float)theImage.mOrigWidth * this.mImgScale) / (float)(sharedImageRef.GetImage().mNumCols + 1);
				PATransform patransform2 = theImage.mTransform;
				patransform2.mMatrix.m12 = patransform2.mMatrix.m12 + -((float)sharedImageRef.mHeight - (float)theImage.mOrigHeight * this.mImgScale) / (float)(sharedImageRef.GetImage().mNumRows + 1);
			}
			theImage.mImages.Add(sharedImageRef);
			return true;
		}

		public virtual void Load_PostImageLoadHook(PAImage theImage)
		{
		}

		public PopAnim(int theId, PopAnimListener theListener)
		{
			GlobalMembers.gSexyAppBase.mPopAnimSet.Add(this);
			this.mId = theId;
			this.mListener = theListener;
			this.mMirror = false;
			this.mAdditive = false;
			this.mColor = new SexyColor(SexyColor.White);
			this.mAnimRate = 0;
			this.mLoaded = false;
			this.mAnimRunning = false;
			this.mMainSpriteInst = null;
			this.mMainAnimDef = null;
			this.mInterpolate = true;
			this.mLoadedImageIsNew = false;
			this.mRandUsed = false;
			this.Clear();
			this.mVersion = 0;
			this.mPaused = false;
			this.mColorizeType = false;
			this.mImgScale = 1f;
			this.mDrawScale = 1f;
			this.mTransDirty = true;
			this.mBlendTicksTotal = 0f;
			this.mBlendTicksCur = 0f;
			this.mBlendDelay = 0f;
			this.mImageSearchPathVector.Add("images\\");
			this.mImageSearchPathVector.Add("");
		}

		public PopAnim(PopAnim rhs)
		{
			GlobalMembers.gSexyAppBase.mPopAnimSet.Add(this);
			this.CopyForm(rhs);
			this.mMainSpriteInst = new PASpriteInst();
			this.mMainSpriteInst.mDef = null;
			this.mMainSpriteInst.mParent = null;
			this.mMainAnimDef.mRefCount++;
		}

		public override void Dispose()
		{
			GlobalMembers.gSexyAppBase.mPopAnimSet.Remove(this);
			this.Clear();
			base.Dispose();
		}

		public void CopyForm(PopAnim rhs)
		{
			base.CopyFrom(rhs);
			this.mId = rhs.mId;
			this.mListener = rhs.mListener;
			this.mVersion = rhs.mVersion;
			this.mCRCBuffer = rhs.mCRCBuffer;
			this.mDrawScale = rhs.mDrawScale;
			this.mImgScale = rhs.mImgScale;
			this.mLoaded = rhs.mLoaded;
			this.mMotionFilePos = rhs.mMotionFilePos;
			this.mModPamFile = rhs.mModPamFile;
			this.mLoadedPamFile = rhs.mLoadedPamFile;
			this.mAnimRate = rhs.mAnimRate;
			this.mError = rhs.mError;
			this.mLastPlayedFrameLabel = rhs.mLastPlayedFrameLabel;
			this.mMainAnimDef = rhs.mMainAnimDef;
			this.mBlendTicksTotal = rhs.mBlendTicksTotal;
			this.mBlendTicksCur = rhs.mBlendTicksCur;
			this.mBlendDelay = rhs.mBlendDelay;
			this.mRandUsed = rhs.mRandUsed;
			this.mAdditive = rhs.mAdditive;
			this.mTransDirty = rhs.mTransDirty;
			this.mAnimRunning = rhs.mAnimRunning;
			this.mMirror = rhs.mMirror;
			this.mInterpolate = rhs.mInterpolate;
			this.mLoadedImageIsNew = rhs.mLoadedImageIsNew;
			this.mPaused = rhs.mPaused;
			this.mColorizeType = rhs.mColorizeType;
			this.Remap_aString = rhs.Remap_aString;
			this.mParticleAttachOffset.mX = rhs.mParticleAttachOffset.mX;
			this.mParticleAttachOffset.mY = rhs.mParticleAttachOffset.mY;
			this.mTransform.CopyFrom(rhs.mTransform);
			this.mColor = new SexyColor(rhs.mColor);
			this.mAnimRect.SetValue(rhs.mAnimRect.mX, rhs.mAnimRect.mY, rhs.mAnimRect.mWidth, rhs.mAnimRect.mHeight);
			this.mImageSearchPathVector = rhs.mImageSearchPathVector;
			this.mRemapList = rhs.mRemapList;
			this.mImageVector = rhs.mImageVector;
		}

		public void Clear()
		{
			this.mMirror = false;
			this.mColor = new SexyColor(SexyColor.White);
			this.mLoaded = false;
			this.mRandUsed = false;
			this.mAnimRunning = false;
			this.mAnimRate = 0;
			this.mError = "";
			this.mImageVector.Clear();
			this.mModPamFile = "";
			this.mRemapList.Clear();
			this.mCRCBuffer.Clear();
			if (this.mMainAnimDef != null)
			{
				if (this.mMainAnimDef.mRefCount == 0)
				{
					this.mMainAnimDef.mSpriteDefVector.Clear();
					if (this.mMainAnimDef != null)
					{
						this.mMainAnimDef.Dispose();
					}
				}
				else
				{
					this.mMainAnimDef.mRefCount--;
				}
			}
			this.mMainAnimDef = null;
			this.mTransDirty = true;
			if (this.mMainSpriteInst != null)
			{
				this.mMainSpriteInst.Dispose();
			}
			this.mMainSpriteInst = null;
		}

		public PopAnim Duplicate()
		{
			return new PopAnim(this);
		}

		public bool LoadFile(string theFileName)
		{
			return this.LoadFile(theFileName, false);
		}

		public bool LoadFile(string theFileName, bool doMirror)
		{
			this.Load_Init();
			this.mMirror = doMirror;
			string text = "";
			int num = theFileName.LastIndexOf('.');
			if (num != -1)
			{
				text = theFileName.Substring(num).ToLower();
			}
			if (text == ".pam")
			{
				return this.Load_LoadPam(theFileName);
			}
			if (!(text == ".txt"))
			{
				return text.Length == 0 && (this.Load_LoadPam(theFileName + ".pam") || this.Load_LoadMod(theFileName + ".txt"));
			}
			if (this.Load_LoadMod(theFileName))
			{
				return true;
			}
			if (this.mError.Length == 0)
			{
				this.mError = "Mod file loading error";
			}
			return false;
		}

		public bool LoadState(SexyBuffer theBuffer)
		{
			theBuffer.mReadBitPos = (theBuffer.mReadBitPos + 7) & -8;
			int num = (int)theBuffer.ReadLong();
			int num2 = theBuffer.mReadBitPos / 8 + num;
			int num3 = (int)theBuffer.ReadShort();
			bool flag = theBuffer.ReadBoolean();
			if (flag)
			{
				string theFileName = theBuffer.ReadStringWithEncoding();
				int num4 = (int)theBuffer.ReadLong();
				this.mMirror = theBuffer.ReadBoolean();
				if (!this.mLoaded)
				{
					this.Load_LoadPam(theFileName);
				}
				else if (this.mMainSpriteInst != null)
				{
					this.ResetAnimHelper(this.mMainSpriteInst);
					this.CleanParticles(this.mMainSpriteInst, true);
					this.mAnimRunning = false;
				}
				this.mAnimRunning = theBuffer.ReadBoolean();
				this.mPaused = theBuffer.ReadBoolean();
				if (num4 != (int)this.mCRCBuffer.GetCRC32(0UL))
				{
					theBuffer.mReadBitPos = num2 * 8;
					return false;
				}
				string theName = theBuffer.ReadStringWithEncoding();
				this.SetupSpriteInst(theName);
				this.LoadStateSpriteInst(theBuffer, this.mMainSpriteInst);
				if (num3 >= 1)
				{
					this.mRandUsed = theBuffer.ReadBoolean();
					if (this.mRandUsed)
					{
						this.mRand.SRand(theBuffer.ReadStringWithEncoding());
					}
				}
			}
			return true;
		}

		public bool SaveState(ref SexyBuffer theBuffer)
		{
			theBuffer.mWriteBitPos = (theBuffer.mWriteBitPos + 7) & -8;
			int num = theBuffer.mWriteBitPos / 8;
			theBuffer.WriteLong(0L);
			theBuffer.WriteShort((short)PopAnim.PAM_STATE_VERSION);
			theBuffer.WriteBoolean(this.mLoaded);
			if (this.mLoaded)
			{
				theBuffer.WriteStringWithEncoding(this.mLoadedPamFile);
				theBuffer.WriteLong(this.mCRCBuffer.GetCRC32(0UL));
				theBuffer.WriteBoolean(this.mMirror);
				theBuffer.WriteBoolean(this.mAnimRunning);
				theBuffer.WriteBoolean(this.mPaused);
				this.SetupSpriteInst();
				theBuffer.WriteStringWithEncoding((this.mMainSpriteInst.mDef.mName != null) ? this.mMainSpriteInst.mDef.mName : "");
				this.SaveStateSpriteInst(ref theBuffer, this.mMainSpriteInst);
				theBuffer.WriteBoolean(this.mRandUsed);
				if (this.mRandUsed)
				{
					theBuffer.WriteStringWithEncoding(this.mRand.Serialize());
				}
			}
			int num2 = theBuffer.mWriteBitPos / 8 - num - 4;
			theBuffer.mData[num] = (byte)num2;
			return true;
		}

		public void ResetAnim()
		{
			this.ResetAnimHelper(this.mMainSpriteInst);
			this.CleanParticles(this.mMainSpriteInst, true);
			this.mAnimRunning = false;
			this.GetToFirstFrame();
			this.mBlendTicksTotal = 0f;
			this.mBlendTicksCur = 0f;
			this.mBlendDelay = 0f;
		}

		public bool SetupSpriteInst()
		{
			return this.SetupSpriteInst("");
		}

		public bool SetupSpriteInst(string theName)
		{
			if (this.mMainSpriteInst == null)
			{
				return false;
			}
			if (this.mMainSpriteInst.mDef != null && theName.Length == 0)
			{
				return true;
			}
			if (this.mMainAnimDef.mMainSpriteDef != null)
			{
				this.InitSpriteInst(this.mMainSpriteInst, this.mMainAnimDef.mMainSpriteDef);
				return true;
			}
			if (this.mMainAnimDef.mSpriteDefVector.Count == 0)
			{
				return false;
			}
			string text = theName;
			if (text.Length == 0)
			{
				text = "main";
			}
			PASpriteDef paspriteDef = null;
			for (int i = 0; i < this.mMainAnimDef.mSpriteDefVector.Count; i++)
			{
				if (this.mMainAnimDef.mSpriteDefVector[i].mName != null && this.mMainAnimDef.mSpriteDefVector[i].mName == text)
				{
					paspriteDef = this.mMainAnimDef.mSpriteDefVector[i];
				}
			}
			if (paspriteDef == null)
			{
				paspriteDef = this.mMainAnimDef.mSpriteDefVector[0];
			}
			if (paspriteDef != this.mMainSpriteInst.mDef)
			{
				if (this.mMainSpriteInst.mDef != null)
				{
					if (this.mMainSpriteInst != null)
					{
						this.mMainSpriteInst.Dispose();
					}
					this.mMainSpriteInst.mParent = null;
				}
				this.InitSpriteInst(this.mMainSpriteInst, paspriteDef);
				this.mTransDirty = true;
			}
			return true;
		}

		public bool Play(int theFrameNum)
		{
			return this.Play(theFrameNum, true);
		}

		public bool Play()
		{
			return this.Play(0, true);
		}

		public bool Play(int theFrameNum, bool resetAnim)
		{
			if (!this.SetupSpriteInst())
			{
				return false;
			}
			if (theFrameNum >= this.mMainSpriteInst.mDef.mFrames.Count)
			{
				this.mAnimRunning = false;
				return false;
			}
			if (this.mMainSpriteInst.mFrameNum != (float)theFrameNum && resetAnim)
			{
				this.ResetAnim();
			}
			this.mPaused = false;
			this.mAnimRunning = true;
			this.mMainSpriteInst.mDelayFrames = 0;
			this.mMainSpriteInst.mFrameNum = (float)theFrameNum;
			this.mMainSpriteInst.mFrameRepeats = 0;
			if (resetAnim)
			{
				this.CleanParticles(this.mMainSpriteInst, true);
			}
			if (this.mBlendDelay == 0f)
			{
				this.DoFramesHit(this.mMainSpriteInst, null);
			}
			this.MarkDirty();
			return true;
		}

		public bool Play(string theFrameLabel)
		{
			return this.Play(theFrameLabel, true);
		}

		public bool Play(string theFrameLabel, bool resetAnim)
		{
			this.mAnimRunning = false;
			if (this.mMainAnimDef.mMainSpriteDef == null)
			{
				this.SetupSpriteInst(theFrameLabel);
				return this.Play(this.mMainSpriteInst.mDef.mWorkAreaStart, resetAnim);
			}
			if (!this.SetupSpriteInst())
			{
				return false;
			}
			int labelFrame = this.mMainAnimDef.mMainSpriteDef.GetLabelFrame(theFrameLabel);
			if (labelFrame == -1)
			{
				return false;
			}
			this.mLastPlayedFrameLabel = theFrameLabel;
			return this.Play(labelFrame, resetAnim);
		}

		public bool BlendTo(string theFrameLabel, int theBlendTicks)
		{
			return this.BlendTo(theFrameLabel, theBlendTicks, 0);
		}

		public bool BlendTo(string theFrameLabel, int theBlendTicks, int theAnimStartDelay)
		{
			if (!this.SetupSpriteInst())
			{
				return false;
			}
			if (!this.mTransDirty)
			{
				this.UpdateTransforms(this.mMainSpriteInst, null, new SexyColor(this.mColor), false);
				this.mTransDirty = false;
			}
			Dictionary<string, BlendSrcData> dictionary = new Dictionary<string, BlendSrcData>();
			PAFrame paframe = this.mMainSpriteInst.mDef.mFrames[(int)this.mMainSpriteInst.mFrameNum];
			PATransform patransform = null;
			for (int i = 0; i < paframe.mFrameObjectPosVector.Count; i++)
			{
				PAObjectPos paobjectPos = paframe.mFrameObjectPosVector[i];
				PAObjectInst paobjectInst = this.mMainSpriteInst.mChildren[paobjectPos.mObjectNum];
				if (paobjectInst.mName != null && paobjectInst.mName[0] != '\0')
				{
					SexyColor color;
					if (paobjectPos.mIsSprite)
					{
						PASpriteInst mSpriteInst = this.mMainSpriteInst.mChildren[paobjectPos.mObjectNum].mSpriteInst;
						color = mSpriteInst.mCurColor.Clone();
						patransform = mSpriteInst.mCurTransform.Clone();
					}
					else
					{
						this.CalcObjectPos(this.mMainSpriteInst, i, false, out patransform, out color);
					}
					BlendSrcData blendSrcData = new BlendSrcData();
					blendSrcData.mTransform = patransform;
					blendSrcData.mColor = color;
					if (paobjectInst.mSpriteInst != null)
					{
						blendSrcData.mParticleEffectVector = paobjectInst.mSpriteInst.mParticleEffectVector;
						paobjectInst.mSpriteInst.mParticleEffectVector.Clear();
					}
					dictionary.Add(paobjectPos.mName, blendSrcData);
				}
			}
			List<PAParticleEffect> mParticleEffectVector = this.mMainSpriteInst.mParticleEffectVector;
			this.mMainSpriteInst.mParticleEffectVector.Clear();
			this.mBlendTicksTotal = (float)theBlendTicks;
			this.mBlendTicksCur = 0f;
			this.mBlendDelay = (float)theAnimStartDelay;
			if (this.mMainAnimDef.mMainSpriteDef != null)
			{
				if (!this.SetupSpriteInst())
				{
					return false;
				}
				int labelFrame = this.mMainAnimDef.mMainSpriteDef.GetLabelFrame(theFrameLabel);
				if (labelFrame == -1)
				{
					return false;
				}
				this.mLastPlayedFrameLabel = theFrameLabel;
				this.Play(labelFrame, false);
				this.mTransDirty = true;
			}
			else
			{
				this.SetupSpriteInst(theFrameLabel);
				this.Play(this.mMainSpriteInst.mDef.mWorkAreaStart, false);
			}
			this.mMainSpriteInst.mParticleEffectVector = mParticleEffectVector;
			mParticleEffectVector.Clear();
			for (int j = 0; j < this.mMainSpriteInst.mDef.mObjectDefVector.Count; j++)
			{
				PAObjectInst paobjectInst2 = this.mMainSpriteInst.mChildren[j];
				if (paobjectInst2.mName != null && paobjectInst2.mName[0] != '\0')
				{
					BlendSrcData blendSrcData2 = null;
					if (dictionary.TryGetValue(paobjectInst2.mName, out blendSrcData2))
					{
						paobjectInst2.mIsBlending = true;
						paobjectInst2.mBlendSrcColor = blendSrcData2.mColor;
						paobjectInst2.mBlendSrcTransform = blendSrcData2.mTransform;
						if (paobjectInst2.mSpriteInst != null)
						{
							if (Enumerable.Count<PAParticleEffect>(blendSrcData2.mParticleEffectVector) > 0)
							{
								paobjectInst2.mSpriteInst.mParticleEffectVector = blendSrcData2.mParticleEffectVector;
								blendSrcData2.mParticleEffectVector.Clear();
							}
						}
						else
						{
							List<PAParticleEffect> mParticleEffectVector2 = blendSrcData2.mParticleEffectVector;
							while (mParticleEffectVector2.Count > 0)
							{
								mParticleEffectVector2[mParticleEffectVector2.Count - 1].mEffect = null;
								mParticleEffectVector2.RemoveAt(mParticleEffectVector2.Count - 1);
							}
						}
						dictionary.Remove(paobjectInst2.mName);
					}
					else
					{
						paobjectInst2.mIsBlending = false;
					}
				}
			}
			while (dictionary.Count > 0)
			{
				List<PAParticleEffect> mParticleEffectVector3 = Enumerable.First<KeyValuePair<string, BlendSrcData>>(dictionary).Value.mParticleEffectVector;
				while (mParticleEffectVector3.Count > 0)
				{
					mParticleEffectVector3[mParticleEffectVector3.Count - 1].mEffect = null;
					mParticleEffectVector3.RemoveAt(mParticleEffectVector3.Count - 1);
				}
				dictionary.Remove(Enumerable.First<KeyValuePair<string, BlendSrcData>>(dictionary).Key);
			}
			return true;
		}

		public bool IsActive()
		{
			return this.mAnimRunning || this.HasParticles(this.mMainSpriteInst);
		}

		public void SetColor(SexyColor theColor)
		{
			this.mColor = theColor.Clone();
			this.MarkDirty();
		}

		public PAObjectInst GetObjectInst(string theName)
		{
			this.SetupSpriteInst();
			return this.mMainSpriteInst.GetObjectInst(theName);
		}

		public override void Draw(Graphics g)
		{
			if (!this.mLoaded)
			{
				return;
			}
			if (!this.SetupSpriteInst())
			{
				return;
			}
			if (this.mTransDirty)
			{
				this.UpdateTransforms(this.mMainSpriteInst, null, this.mColor, false);
				this.mTransDirty = false;
			}
			if (this.mMirror)
			{
				this.DrawSpriteMirrored(g, this.mMainSpriteInst, null, this.mColor, this.mAdditive, false);
				return;
			}
			this.DrawSprite(g, this.mMainSpriteInst, null, this.mColor, this.mAdditive, false);
		}

		public override void Update()
		{
			if (!this.mLoaded)
			{
				return;
			}
			base.Update();
			if (!this.SetupSpriteInst())
			{
				return;
			}
			if (!GlobalMembers.gSexyAppBase.mVSyncUpdates)
			{
				this.UpdateF(1f);
			}
			this.UpdateTransforms(this.mMainSpriteInst, null, this.mColor, false);
			this.mTransDirty = false;
			if (!this.mPaused)
			{
				this.UpdateParticles(this.mMainSpriteInst, null);
				this.CleanParticles(this.mMainSpriteInst);
			}
		}

		public override void UpdateF(float theFrac)
		{
			if (this.mPaused)
			{
				return;
			}
			this.AnimUpdate(theFrac);
		}

		public int GetLabelFrame(string theFrameLabel)
		{
			return this.mMainAnimDef.mMainSpriteDef.GetLabelFrame(theFrameLabel);
		}

		public PASpriteDef FindSpriteDef(string theAnimName)
		{
			if (this.mMainAnimDef != null)
			{
				for (int i = 0; i < this.mMainAnimDef.mSpriteDefVector.Count; i++)
				{
					if (this.mMainAnimDef.mSpriteDefVector[i].mName == theAnimName)
					{
						return this.mMainAnimDef.mSpriteDefVector[i];
					}
				}
			}
			return null;
		}

		public static uint PAM_MAGIC = 0xBAF01954;

		public static int PAM_VERSION = 5;

		public static int PAM_STATE_VERSION = 1;

		public static int FRAMEFLAGS_HAS_REMOVES = 1;

		public static int FRAMEFLAGS_HAS_ADDS = 2;

		public static int FRAMEFLAGS_HAS_MOVES = 4;

		public static int FRAMEFLAGS_HAS_FRAME_NAME = 8;

		public static int FRAMEFLAGS_HAS_STOP = 16;

		public static int FRAMEFLAGS_HAS_COMMANDS = 32;

		public static int MOVEFLAGS_HAS_SRCRECT = 32768;

		public static int MOVEFLAGS_HAS_ROTATE = 16384;

		public static int MOVEFLAGS_HAS_COLOR = 8192;

		public static int MOVEFLAGS_HAS_MATRIX = 4096;

		public static int MOVEFLAGS_HAS_LONGCOORDS = 2048;

		public static int MOVEFLAGS_HAS_ANIMFRAMENUM = 1024;

		public int mId;

		public PopAnimListener mListener;

		public List<string> mImageSearchPathVector = new List<string>();

		public int mVersion;

		public SexyBuffer mCRCBuffer = new SexyBuffer();

		public float mDrawScale;

		public float mImgScale;

		public bool mLoaded;

		public int mMotionFilePos;

		public string mModPamFile;

		public string mLoadedPamFile;

		public LinkedList<KeyValuePair<string, string>> mRemapList = new LinkedList<KeyValuePair<string, string>>();

		public int mAnimRate;

		public Rect mAnimRect = default(Rect);

		public string mError;

		public string mLastPlayedFrameLabel;

		public List<PAImage> mImageVector = new List<PAImage>();

		public PASpriteInst mMainSpriteInst;

		public PopAnimDef mMainAnimDef;

		public float mBlendTicksTotal;

		public float mBlendTicksCur;

		public float mBlendDelay;

		public MTRand mRand = new MTRand();

		public bool mRandUsed;

		public FPoint mParticleAttachOffset = new FPoint();

		private SexyTransform2D mTransform = new SexyTransform2D(false);

		public SexyColor mColor = default(SexyColor);

		public bool mAdditive;

		public bool mTransDirty;

		public bool mAnimRunning;

		public bool mMirror;

		public bool mInterpolate;

		public bool mLoadedImageIsNew;

		public bool mPaused;

		public bool mColorizeType;

		private string Remap_aString;
	}
}
