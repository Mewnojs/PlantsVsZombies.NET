using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Sexy.GraphicsLib;
using Sexy.Misc;
using Sexy.WidgetsLib;

namespace Sexy.Resource
{
	public class ResourceManager
	{
		public bool Fail(string theErrorText)
		{
			if (!this.mHasFailed)
			{
				this.mHasFailed = true;
				if (this.mXMLParser == null)
				{
					this.mError = theErrorText;
					return false;
				}
				int currentLineNum = this.mXMLParser.GetCurrentLineNum();
				this.mError = theErrorText;
				if (currentLineNum > 0)
				{
					this.mError = this.mError + " on Line " + currentLineNum;
				}
				if (this.mXMLParser.GetFileName().Length > 0)
				{
					this.mError = this.mError + " in File '" + this.mXMLParser.GetFileName() + "'";
				}
			}
			return false;
		}

		protected virtual bool ParseCommonResource(XMLElement theElement, BaseRes theRes, Dictionary<string, BaseRes> theMap)
		{
			this.mHadAlreadyDefinedError = false;
			theRes.mParent = this;
			theRes.mGlobalPtr = null;
			string attribute = theElement.GetAttribute("path");
			if (attribute.Length <= 0)
			{
				return this.Fail("No path specified.");
			}
			theRes.mXMLAttributes = theElement.GetAttributeMap();
			theRes.mFromProgram = false;
			if (attribute[0] == '!')
			{
				theRes.mPath = attribute;
				if (attribute == "!program")
				{
					theRes.mFromProgram = true;
				}
			}
			else
			{
				theRes.mPath = this.mDefaultPath + attribute;
				this.mResFromPathMap[theRes.mPath.ToUpper()] = theRes;
			}
			string text;
			if (theElement.GetAttribute("id").Length > 0)
			{
				text = this.mDefaultIdPrefix + theElement.GetAttribute("id");
			}
			else
			{
				text = this.mDefaultIdPrefix + Common.GetFileName(theRes.mPath, true);
			}
			if (this.mCurResGroupArtRes != 0)
			{
				text = text + "|" + this.mCurResGroupArtRes;
			}
			if (this.mCurResGroupLocSet != 0U)
			{
				text = text + "||" + string.Format("{0:x}", this.mCurResGroupLocSet);
			}
			theRes.mResGroup = this.mCurResGroup;
			theRes.mCompositeResGroup = this.mCurCompositeResGroup;
			theRes.mId = text;
			theRes.mArtRes = this.mCurResGroupArtRes;
			theRes.mLocSet = this.mCurResGroupLocSet;
			if (theMap.ContainsKey(text))
			{
				this.mHadAlreadyDefinedError = true;
				return this.Fail("Resource already defined.");
			}
			theMap[theRes.mId] = theRes;
			this.mCurResGroupList.mResList.Add(theRes);
			return true;
		}

		protected virtual bool ParseSoundResource(XMLElement theElement)
		{
			SoundRes soundRes = new SoundRes();
			soundRes.mSoundId = -1;
			soundRes.mVolume = -1.0;
			soundRes.mPanning = 0;
			if (!this.ParseCommonResource(theElement, soundRes, this.mResMaps[1]))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				SoundRes soundRes2 = soundRes;
				soundRes = (SoundRes)this.mResMaps[1][soundRes2.mId];
				soundRes.mPath = soundRes2.mPath;
				soundRes.mXMLAttributes = soundRes2.mXMLAttributes;
			}
			if (theElement.HasAttribute("volume"))
			{
				double.TryParse(theElement.GetAttribute("volume"), NumberStyles.Float, CultureInfo.InvariantCulture, out soundRes.mVolume);
			}
			if (theElement.HasAttribute("pan"))
			{
				int.TryParse(theElement.GetAttribute("pan"), out soundRes.mPanning);
			}
			soundRes.ApplyConfig();
			soundRes.mReloadIdx = this.mReloadIdx;
			return true;
		}

		protected virtual bool ParseImageResource(XMLElement theElement)
		{
			string attribute = theElement.GetAttribute("id");
			if (attribute.Length <= 0)
			{
				return true;
			}
			string attribute2 = theElement.GetAttribute("path");
			if (attribute2.Length <= 0)
			{
				return true;
			}
			ImageRes imageRes = new ImageRes();
			if (!this.ParseCommonResource(theElement, imageRes, this.mResMaps[0]))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				ImageRes imageRes2 = imageRes;
				imageRes = (ImageRes)this.mResMaps[0][imageRes2.mId];
				imageRes.mPath = imageRes2.mPath;
				imageRes.mXMLAttributes = imageRes2.mXMLAttributes;
			}
			imageRes.mPalletize = !theElement.GetAttributeBool("nopal", false);
			imageRes.mA4R4G4B4 = theElement.GetAttributeBool("a4r4g4b4", false);
			imageRes.mDDSurface = theElement.GetAttributeBool("ddsurface", false);
			bool flag = true;
			imageRes.mPurgeBits = theElement.GetAttributeBool("nobits", false) || (flag && theElement.GetAttributeBool("nobits3d", false)) || (!flag && theElement.GetAttributeBool("nobits2d", false));
			imageRes.mA8R8G8B8 = theElement.GetAttributeBool("a8r8g8b8", false);
			imageRes.mDither16 = theElement.GetAttributeBool("dither16", false);
			imageRes.mMinimizeSubdivisions = theElement.GetAttributeBool("minsubdivide", false);
			imageRes.mAutoFindAlpha = !theElement.GetAttributeBool("noalpha", false);
			imageRes.mCubeMap = theElement.GetAttributeBool("cubemap", false);
			imageRes.mVolumeMap = theElement.GetAttributeBool("volumemap", false);
			imageRes.mNoTriRep = theElement.GetAttributeBool("notrirep", false) || theElement.GetAttributeBool("noquadrep", false);
			imageRes.m2DBig = theElement.GetAttributeBool("2dbig", false);
			imageRes.mIsAtlas = theElement.GetAttributeBool("atlas", false);
			if (theElement.HasAttribute("alphaimage"))
			{
				imageRes.mAlphaImage = this.mDefaultPath + theElement.GetAttribute("alphaimage");
			}
			imageRes.mAlphaColor = 16777215;
			if (theElement.HasAttribute("alphacolor"))
			{
				imageRes.mAlphaColor = int.Parse(string.Format("x", theElement.GetAttribute("alphacolor")));
			}
			imageRes.mOffset = new SexyPoint(0, 0);
			if (theElement.HasAttribute("x"))
			{
				imageRes.mOffset.mX = int.Parse(theElement.GetAttribute("x"));
			}
			if (theElement.HasAttribute("y"))
			{
				imageRes.mOffset.mY = int.Parse(theElement.GetAttribute("y"));
			}
			if (theElement.HasAttribute("variant"))
			{
				imageRes.mVariant = theElement.GetAttribute("variant");
			}
			if (theElement.HasAttribute("alphagrid"))
			{
				imageRes.mAlphaGridImage = this.mDefaultPath + theElement.GetAttribute("alphagrid");
			}
			if (theElement.HasAttribute("rows"))
			{
				imageRes.mRows = int.Parse(theElement.GetAttribute("rows"));
			}
			if (theElement.HasAttribute("cols"))
			{
				imageRes.mCols = int.Parse(theElement.GetAttribute("cols"));
			}
			if (theElement.HasAttribute("parent"))
			{
				imageRes.mAtlasName = theElement.GetAttribute("parent");
				imageRes.mAtlasX = int.Parse(theElement.GetAttribute("ax"));
				imageRes.mAtlasY = int.Parse(theElement.GetAttribute("ay"));
				imageRes.mAtlasW = int.Parse(theElement.GetAttribute("aw"));
				imageRes.mAtlasH = int.Parse(theElement.GetAttribute("ah"));
			}
			if (imageRes.mCubeMap)
			{
				if (imageRes.mRows * imageRes.mCols != 6)
				{
					this.Fail("Invalid CubeMap definition; must have 6 cells (check rows & cols values).");
					return false;
				}
			}
			else if (imageRes.mVolumeMap)
			{
				int num = imageRes.mRows * imageRes.mCols;
				if (num == 0 || (num & (num - 1)) != 0)
				{
					this.Fail("Invalid VolumeMap definition; must have a pow2 cell count (check rows & cols values).");
					return false;
				}
			}
			AnimType animType = AnimType.AnimType_None;
			if (theElement.HasAttribute("anim"))
			{
				string attribute3 = theElement.GetAttribute("anim");
				if (attribute3.ToLower() == "none")
				{
					animType = AnimType.AnimType_None;
				}
				else if (attribute3.ToLower() == "once")
				{
					animType = AnimType.AnimType_Once;
				}
				else if (attribute3.ToLower() == "loop")
				{
					animType = AnimType.AnimType_Loop;
				}
				else
				{
					if (!(attribute3.ToLower() == "pingpong"))
					{
						this.Fail("Invalid animation type.");
						return false;
					}
					animType = AnimType.AnimType_PingPong;
				}
			}
			imageRes.mAnimInfo.mAnimType = animType;
			if (animType != AnimType.AnimType_None)
			{
				int theNumCels = Math.Max(imageRes.mRows, imageRes.mCols);
				int theBeginFrameTime = 0;
				int theEndFrameTime = 0;
				if (theElement.HasAttribute("framedelay"))
				{
					imageRes.mAnimInfo.mFrameDelay = int.Parse(theElement.GetAttribute("framedelay"));
				}
				if (theElement.HasAttribute("begindelay"))
				{
					theBeginFrameTime = (imageRes.mAnimInfo.mBeginDelay = int.Parse(theElement.GetAttribute("begindelay")));
				}
				if (theElement.HasAttribute("enddelay"))
				{
					theEndFrameTime = (imageRes.mAnimInfo.mEndDelay = int.Parse(theElement.GetAttribute("enddelay")));
				}
				if (theElement.HasAttribute("perframedelay"))
				{
					ResourceManager.ReadIntVector(theElement.GetAttribute("perframedelay"), imageRes.mAnimInfo.mPerFrameDelay);
				}
				if (theElement.HasAttribute("framemap"))
				{
					ResourceManager.ReadIntVector(theElement.GetAttribute("framemap"), imageRes.mAnimInfo.mFrameMap);
				}
				imageRes.mAnimInfo.Compute(theNumCels, theBeginFrameTime, theEndFrameTime);
			}
			imageRes.ApplyConfig();
			imageRes.mReloadIdx = this.mReloadIdx;
			return true;
		}

		protected virtual bool ParseFontResource(XMLElement theElement)
		{
			FontRes fontRes = new FontRes();
			fontRes.mFont = null;
			fontRes.mImage = null;
			if (!this.ParseCommonResource(theElement, fontRes, this.mResMaps[2]))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				FontRes fontRes2 = fontRes;
				fontRes = (FontRes)this.mResMaps[2][fontRes2.mId];
				fontRes.mPath = fontRes2.mPath;
				fontRes.mXMLAttributes = fontRes2.mXMLAttributes;
			}
			fontRes.mImagePath = "";
			if (theElement.HasAttribute("image"))
			{
				fontRes.mImagePath = theElement.GetAttribute("image");
			}
			if (theElement.HasAttribute("tags"))
			{
				fontRes.mTags = theElement.GetAttribute("tags");
			}
			if (fontRes.mImagePath.StartsWith("!sys:"))
			{
				fontRes.mSysFont = true;
				string mPath = fontRes.mPath;
				fontRes.mPath = mPath.Substring(5);
				if (!theElement.HasAttribute("size"))
				{
					return this.Fail("SysFont needs point size");
				}
				fontRes.mSize = int.Parse(theElement.GetAttribute("size"));
				if (fontRes.mSize <= 0)
				{
					return this.Fail("SysFont needs point size");
				}
				fontRes.mBold = theElement.GetAttributeBool("bold", false);
				fontRes.mItalic = theElement.GetAttributeBool("italic", false);
				fontRes.mShadow = theElement.GetAttributeBool("shadow", false);
				fontRes.mUnderline = theElement.GetAttributeBool("underline", false);
			}
			else
			{
				fontRes.mSysFont = false;
			}
			fontRes.ApplyConfig();
			fontRes.mReloadIdx = this.mReloadIdx;
			return true;
		}

		protected virtual bool ParsePopAnimResource(XMLElement theElement)
		{
			PopAnimRes popAnimRes = new PopAnimRes();
			if (!this.ParseCommonResource(theElement, popAnimRes, this.mResMaps[3]))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				PopAnimRes popAnimRes2 = popAnimRes;
				popAnimRes = (PopAnimRes)this.mResMaps[3][popAnimRes2.mId];
				popAnimRes.mPath = popAnimRes2.mPath;
				popAnimRes.mXMLAttributes = popAnimRes2.mXMLAttributes;
			}
			popAnimRes.ApplyConfig();
			popAnimRes.mReloadIdx = this.mReloadIdx;
			return true;
		}

		protected virtual bool ParsePIEffectResource(XMLElement theElement)
		{
			PIEffectRes pieffectRes = new PIEffectRes();
			if (!this.ParseCommonResource(theElement, pieffectRes, this.mResMaps[4]))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				PIEffectRes pieffectRes2 = pieffectRes;
				pieffectRes = (PIEffectRes)this.mResMaps[4][pieffectRes2.mId];
				pieffectRes.mPath = pieffectRes2.mPath;
				pieffectRes.mXMLAttributes = pieffectRes2.mXMLAttributes;
			}
			pieffectRes.ApplyConfig();
			pieffectRes.mReloadIdx = this.mReloadIdx;
			return true;
		}

		protected virtual bool ParseRenderEffectResource(XMLElement theElement)
		{
			RenderEffectRes renderEffectRes = new RenderEffectRes();
			if (!this.ParseCommonResource(theElement, renderEffectRes, this.mResMaps[5]))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				RenderEffectRes renderEffectRes2 = renderEffectRes;
				renderEffectRes = (RenderEffectRes)this.mResMaps[5][renderEffectRes2.mId];
				renderEffectRes.mPath = renderEffectRes2.mPath;
				renderEffectRes.mXMLAttributes = renderEffectRes2.mXMLAttributes;
			}
			renderEffectRes.mSrcFilePath = "";
			if (theElement.HasAttribute("srcpath") && theElement.GetAttribute("srcpath").Length > 0)
			{
				renderEffectRes.mSrcFilePath = this.mDefaultPath + theElement.GetAttribute("srcpath");
			}
			renderEffectRes.ApplyConfig();
			renderEffectRes.mReloadIdx = this.mReloadIdx;
			return true;
		}

		protected virtual bool ParseGenericResFileResource(XMLElement theElement)
		{
			GenericResFileRes genericResFileRes = new GenericResFileRes();
			if (!this.ParseCommonResource(theElement, genericResFileRes, this.mResMaps[6]))
			{
				if (!this.mHadAlreadyDefinedError || !this.mAllowAlreadyDefinedResources)
				{
					return false;
				}
				this.mError = "";
				this.mHasFailed = false;
				GenericResFileRes genericResFileRes2 = genericResFileRes;
				genericResFileRes = (GenericResFileRes)this.mResMaps[6][genericResFileRes2.mId];
				genericResFileRes.mPath = genericResFileRes2.mPath;
				genericResFileRes.mXMLAttributes = genericResFileRes2.mXMLAttributes;
			}
			genericResFileRes.ApplyConfig();
			genericResFileRes.mReloadIdx = this.mReloadIdx;
			return true;
		}

		protected virtual bool ParseSetDefaults(XMLElement theElement)
		{
			if (theElement.HasAttribute("path"))
			{
				this.mDefaultPath = Common.RemoveTrailingSlash(theElement.GetAttribute("path")) + "/";
			}
			if (theElement.HasAttribute("idprefix"))
			{
				this.mDefaultIdPrefix = Common.RemoveTrailingSlash(theElement.GetAttribute("idprefix"));
			}
			return true;
		}

		public virtual bool ParseResources()
		{
			XMLElement xmlelement;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!this.mXMLParser.NextElement(xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					if (xmlelement.mValue.ToString() == "Image")
					{
						if (!this.ParseImageResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElement.XMLElementType.TYPE_END)
						{
							goto Block_5;
						}
					}
					else if (xmlelement.mValue.ToString() == "Sound")
					{
						if (!this.ParseSoundResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElement.XMLElementType.TYPE_END)
						{
							goto Block_9;
						}
					}
					else if (xmlelement.mValue.ToString() == "Font")
					{
						if (!this.ParseFontResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElement.XMLElementType.TYPE_END)
						{
							goto Block_13;
						}
					}
					else if (xmlelement.mValue.ToString() == "PopAnim")
					{
						if (!this.ParsePopAnimResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElement.XMLElementType.TYPE_END)
						{
							goto Block_17;
						}
					}
					else if (xmlelement.mValue.ToString() == "PIEffect")
					{
						if (!this.ParsePIEffectResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElement.XMLElementType.TYPE_END)
						{
							goto Block_21;
						}
					}
					else if (xmlelement.mValue.ToString() == "RenderEffect")
					{
						if (!this.ParseRenderEffectResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElement.XMLElementType.TYPE_END)
						{
							goto Block_25;
						}
					}
					else if (xmlelement.mValue.ToString() == "File")
					{
						if (!this.ParseGenericResFileResource(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElement.XMLElementType.TYPE_END)
						{
							goto Block_29;
						}
					}
					else
					{
						if (!(xmlelement.mValue.ToString() == "SetDefaults"))
						{
							goto IL_26F;
						}
						if (!this.ParseSetDefaults(xmlelement))
						{
							return false;
						}
						if (!this.mXMLParser.NextElement(xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElement.XMLElementType.TYPE_END)
						{
							goto Block_33;
						}
					}
				}
				else
				{
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
					{
						goto Block_34;
					}
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
			return false;
			Block_5:
			return this.Fail("Unexpected element found.");
			Block_9:
			return this.Fail("Unexpected element found.");
			Block_13:
			return this.Fail("Unexpected element found.");
			Block_17:
			return this.Fail("Unexpected element found.");
			Block_21:
			return this.Fail("Unexpected element found.");
			Block_25:
			return this.Fail("Unexpected element found.");
			Block_29:
			return this.Fail("Unexpected element found.");
			Block_33:
			return this.Fail("Unexpected element found.");
			IL_26F:
			this.Fail("Invalid Section '" + xmlelement.mValue + "'");
			return false;
			Block_34:
			this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			return false;
		}

		public bool DoParseResources()
		{
			if (!this.mXMLParser.HasFailed())
			{
				XMLElement xmlelement;
				XMLElement xmlelement2;
				for (;;)
				{
					xmlelement = new XMLElement();
					if (!this.mXMLParser.NextElement(xmlelement))
					{
						goto IL_395;
					}
					if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
					{
						if (xmlelement.mValue.ToString() == "Resources")
						{
							this.mCurResGroup = xmlelement.GetAttribute("id");
							if (this.mCurResGroup.Length <= 0)
							{
								break;
							}
							if (this.mResGroupMap.ContainsKey(this.mCurResGroup))
							{
								this.mCurResGroupList = this.mResGroupMap[this.mCurResGroup];
							}
							else
							{
								this.mCurResGroupList = new ResGroup();
								this.mResGroupMap[this.mCurResGroup] = this.mCurResGroupList;
							}
							this.mCurCompositeResGroup = xmlelement.GetAttribute("parent");
							string attribute = xmlelement.GetAttribute("res");
							this.mCurResGroupArtRes = ((attribute.Length <= 0) ? 0 : int.Parse(attribute));
							string attribute2 = xmlelement.GetAttribute("loc");
							this.mCurResGroupLocSet = (uint)((attribute2.Length < 4) ? '\0' : (((uint)attribute2[0] << 24) | ((uint)attribute2[1] << 16) | ((uint)attribute2[2] << 8) | attribute2[3]));
							if (!this.ParseResources())
							{
								goto Block_8;
							}
						}
						else
						{
							if (!(xmlelement.mValue.ToString() == "CompositeResources"))
							{
								goto IL_34F;
							}
							string attribute3 = xmlelement.GetAttribute("id");
							if (attribute3.Length <= 0)
							{
								goto Block_10;
							}
							CompositeResGroup compositeResGroup;
							if (this.mCompositeResGroupMap.ContainsKey(attribute3))
							{
								compositeResGroup = this.mCompositeResGroupMap[attribute3];
							}
							else
							{
								compositeResGroup = new CompositeResGroup();
								this.mCompositeResGroupMap[attribute3] = compositeResGroup;
							}
							for (;;)
							{
								xmlelement2 = new XMLElement();
								if (!this.mXMLParser.NextElement(xmlelement2))
								{
									return false;
								}
								if (xmlelement2.mType == XMLElement.XMLElementType.TYPE_START)
								{
									if (!(xmlelement2.mValue.ToString() == "Group"))
									{
										goto IL_2ED;
									}
									string attribute4 = xmlelement2.GetAttribute("id");
									int mArtRes = 0;
									string attribute5 = xmlelement2.GetAttribute("res");
									if (attribute5.Length > 0)
									{
										mArtRes = int.Parse(attribute5);
									}
									uint mLocSet = 0U;
									string attribute6 = xmlelement2.GetAttribute("loc");
									if (attribute6.Length >= 4)
									{
										mLocSet = (uint)(((uint)attribute6[0] << 24) | ((uint)attribute6[1] << 16) | ((uint)attribute6[2] << 8) | attribute6[3]);
									}
									SubGroup subGroup = new SubGroup();
									subGroup.mGroupName = attribute4;
									subGroup.mArtRes = mArtRes;
									subGroup.mLocSet = mLocSet;
									compositeResGroup.mSubGroups.Add(subGroup);
									if (!this.mXMLParser.NextElement(xmlelement2))
									{
										goto Block_17;
									}
									if (xmlelement2.mType != XMLElement.XMLElementType.TYPE_END)
									{
										goto Block_18;
									}
								}
								else
								{
									if (xmlelement2.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
									{
										goto Block_19;
									}
									if (xmlelement2.mType == XMLElement.XMLElementType.TYPE_END)
									{
										break;
									}
								}
							}
							IL_342:
							if (this.mHasFailed)
							{
								goto Block_20;
							}
							continue;
							Block_18:
							this.Fail("Unexpected element found.");
							goto IL_342;
							Block_17:
							this.Fail("Group end expected");
							goto IL_342;
							IL_2ED:
							this.Fail("Invalid Section '" + xmlelement2.mValue + "' within CompositeGroup");
							goto IL_342;
						}
					}
					else if (xmlelement.mType == XMLElement.XMLElementType.TYPE_ELEMENT)
					{
						goto Block_21;
					}
				}
				this.Fail("No id specified.");
				Block_8:
				goto IL_395;
				Block_10:
				this.Fail("No id specified on CompositeGroup.");
				goto IL_395;
				Block_19:
				this.Fail("Element Not Expected '" + xmlelement2.mValue + "'");
				return false;
				Block_20:
				goto IL_395;
				IL_34F:
				this.Fail("Invalid Section '" + xmlelement.mValue + "'");
				goto IL_395;
				Block_21:
				this.Fail("Element Not Expected '" + xmlelement.mValue + "'");
			}
			IL_395:
			if (this.mXMLParser.HasFailed())
			{
				this.Fail(this.mXMLParser.GetErrorText());
			}
			this.mXMLParser.Dispose();
			this.mXMLParser = null;
			return !this.mHasFailed;
		}

		public void DeleteMap(Dictionary<string, BaseRes> theMap)
		{
			foreach (KeyValuePair<string, BaseRes> keyValuePair in theMap)
			{
				keyValuePair.Value.DeleteResource();
			}
			theMap.Clear();
		}

		public virtual void DeleteResources(Dictionary<string, BaseRes> theMap, string theGroup)
		{
			if (theGroup.Length <= 0)
			{
				foreach (KeyValuePair<string, BaseRes> keyValuePair in theMap)
				{
					if (keyValuePair.Value.mDirectLoaded)
					{
						keyValuePair.Value.mDirectLoaded = false;
						this.Deref(keyValuePair.Value);
					}
				}
				return;
			}
			if (this.mCompositeResGroupMap.ContainsKey(theGroup))
			{
				CompositeResGroup compositeResGroup = this.mCompositeResGroupMap[theGroup];
				int count = compositeResGroup.mSubGroups.Count;
				using (List<SubGroup>.Enumerator enumerator2 = compositeResGroup.mSubGroups.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						SubGroup subGroup = enumerator2.Current;
						if (subGroup.mGroupName.Length > 0)
						{
							foreach (KeyValuePair<string, BaseRes> keyValuePair4 in theMap)
							{
								if (keyValuePair4.Value.mDirectLoaded)
								{
									if (keyValuePair4.Value.mResGroup == subGroup.mGroupName)
									{
										keyValuePair4.Value.mDirectLoaded = false;
										this.Deref(keyValuePair4.Value);
									}
								}
							}
						}
					}
					return;
				}
			}
			foreach (KeyValuePair<string, BaseRes> keyValuePair8 in theMap)
			{
				if (keyValuePair8.Value.mDirectLoaded)
				{
					Dictionary<string, BaseRes>.Enumerator enumerator4;
					if (keyValuePair8.Value.mResGroup == theGroup)
					{
						keyValuePair8.Value.mDirectLoaded = false;
						this.Deref(keyValuePair8.Value);
					}
				}
			}
		}

		public BaseRes GetBaseRes(int theType, string theId)
		{
			if (this.mCurArtResKey.Length <= 0)
			{
				this.mCurArtResKey = "|" + this.mCurArtRes;
				this.mCurLocSetKey = "||" + string.Format("x", this.mCurLocSet);
				this.mCurArtResAndLocSetKey = this.mCurArtRes + "|||" + string.Format("x", this.mCurLocSet);
			}
			if (this.mResMaps[theType].ContainsKey(theId + this.mCurArtResAndLocSetKey))
			{
				return this.mResMaps[theType][theId + this.mCurArtResAndLocSetKey];
			}
			if (this.mResMaps[theType].ContainsKey(theId + this.mCurArtResKey))
			{
				return this.mResMaps[theType][theId + this.mCurArtResKey];
			}
			if (this.mResMaps[theType].ContainsKey(theId + this.mCurLocSetKey))
			{
				return this.mResMaps[theType][theId + this.mCurLocSetKey];
			}
			if (this.mResMaps[theType].ContainsKey(theId))
			{
				return this.mResMaps[theType][theId];
			}
			return null;
		}

		public void Deref(BaseRes theRes)
		{
			theRes.mRefCount--;
			if (theRes.mRefCount == 0)
			{
				theRes.DeleteResource();
			}
		}

		public bool LoadAlphaGridImage(ImageRes theRes, DeviceImage theImage)
		{
			throw new NotImplementedException();
		}

		public bool LoadAlphaImage(ImageRes theRes, DeviceImage theImage)
		{
			throw new NotImplementedException();
		}

		public virtual bool DoLoadImage(ImageRes theRes)
		{
			new AutoCrit(this.mLoadCrit);
			string mPath = theRes.mPath;
			if (mPath.StartsWith("!ref:"))
			{
				string text = mPath.Substring(5);
				theRes.mResourceRef = this.GetImageRef(text);
				SharedImageRef sharedImageRef = theRes.mResourceRef.GetSharedImageRef();
				if (sharedImageRef.GetImage() == null)
				{
					sharedImageRef = this.LoadImage(text);
				}
				if (sharedImageRef.GetImage() == null)
				{
					return this.Fail("Ref Image not found: " + text);
				}
				theRes.mImage = sharedImageRef;
				theRes.mGlobalPtr = this.RegisterGlobalPtr(text);
				return true;
			}
			else
			{
				bool flag = theRes.mAtlasName != null;
				bool flag2 = false;
				SharedImageRef sharedImageRef2 = this.mApp.CheckSharedImage(mPath, theRes.mVariant);
				if (sharedImageRef2.GetDeviceImage() != null)
				{
					flag2 = true;
				}
				else if (!flag)
				{
					DeviceImage deviceImage = DeviceImage.ReadFromCache(Common.GetFullPath(mPath), "ResMan");
					if (deviceImage != null)
					{
						sharedImageRef2 = this.mApp.SetSharedImage(mPath, theRes.mVariant, deviceImage);
						theRes.mImage = sharedImageRef2;
						flag2 = true;
					}
				}
				bool flag3 = false;
				bool mWriteToSexyCache = this.mApp.mWriteToSexyCache;
				this.mApp.mWriteToSexyCache = false;
				if (!flag2)
				{
					sharedImageRef2 = this.mApp.GetSharedImage(mPath, theRes.mVariant, ref flag3, !theRes.mNoTriRep, flag);
				}
				this.mApp.mWriteToSexyCache = mWriteToSexyCache;
				DeviceImage deviceImage2 = sharedImageRef2.GetDeviceImage();
				if (deviceImage2 == null)
				{
					return this.Fail("Failed to load image: " + mPath);
				}
				if (flag3)
				{
					if (flag)
					{
						deviceImage2.mWidth = theRes.mAtlasW;
						deviceImage2.mHeight = theRes.mAtlasH;
					}
					if (theRes.mAlphaImage.Length > 0 && !this.LoadAlphaImage(theRes, deviceImage2))
					{
						return false;
					}
					if (theRes.mAlphaGridImage.Length > 0 && !this.LoadAlphaGridImage(theRes, deviceImage2))
					{
						return false;
					}
				}
				if (theRes.mPalletize && !flag2)
				{
					if (deviceImage2.mSurface == null)
					{
						deviceImage2.Palletize();
					}
					else
					{
						deviceImage2.mWantPal = true;
					}
				}
				theRes.mImage = sharedImageRef2;
				theRes.ApplyConfig();
				theRes.mImage.GetImage().mNameForRes = theRes.mId;
				if (theRes.mGlobalPtr != null)
				{
					theRes.mGlobalPtr.mResObject = deviceImage2;
				}
				if (!flag2 && !flag)
				{
					deviceImage2.WriteToCache(Common.GetFullPath(mPath), "ResMan");
				}
				this.ResourceLoadedHook(theRes);
				return true;
			}
		}

		public virtual bool DoLoadFont(FontRes theRes)
		{
			new AutoCrit(this.mLoadCrit);
			Font font = null;
			string text = theRes.mPath;
			string text2 = string.Format("path{0}", this.mCurArtRes);
			if (theRes.mXMLAttributes.ContainsKey(text2))
			{
				text = theRes.mXMLAttributes[text2];
			}
			if (!theRes.mSysFont)
			{
				if (string.IsNullOrEmpty(theRes.mImagePath))
				{
					if (string.Compare(text, 0, "!ref:", 0, 5) == 0)
					{
						string text3 = text.Substring(5);
						theRes.mResourceRef = this.GetFontRef(text3);
						Font font2 = theRes.mResourceRef.GetFont();
						if (font2 == null)
						{
							return this.Fail("Ref Font not found: " + text3);
						}
						font = (theRes.mFont = font2.Duplicate());
					}
					else
					{
						ImageFont imageFont = ImageFont.ReadFromCache(Common.GetFullPath(text), "ResMan");
						if (imageFont != null)
						{
							font = imageFont;
						}
						else
						{
							imageFont = new ImageFont(this.mApp, text, "");
							font = imageFont;
						}
					}
				}
				else
				{
					Image image = this.mApp.GetImage(theRes.mImagePath, false, false, false);
					if (image == null)
					{
						return this.Fail(string.Format("Failed to load image: {0}", theRes.mImagePath));
					}
					theRes.mImage = image;
				}
			}
			ImageFont imageFont2 = font.AsImageFont();
			if (imageFont2 != null)
			{
				if (imageFont2.mFontData == null || !imageFont2.mFontData.mInitialized)
				{
					if (font != null)
					{
						font.Dispose();
					}
					return this.Fail(string.Format("Failed to load font: {0}", text));
				}
				imageFont2.mTagVector.Clear();
				imageFont2.mActiveListValid = false;
				if (!string.IsNullOrEmpty(theRes.mTags))
				{
					string[] array = theRes.mTags.Split(", \r\n\t".ToCharArray());
					for (int i = 0; i < array.Length; i++)
					{
						imageFont2.AddTag(array[i]);
					}
					imageFont2.Prepare();
				}
			}
			theRes.mFont = imageFont2;
			if (theRes.mGlobalPtr != null)
			{
				theRes.mGlobalPtr.mResObject = font;
			}
			theRes.ApplyConfig();
			this.ResourceLoadedHook(theRes);
			return true;
		}

		public virtual bool DoLoadSound(SoundRes theRes)
		{
			new AutoCrit(this.mLoadCrit);
			string mPath = theRes.mPath;
			if (theRes.mPath.StartsWith("!ref:"))
			{
				string text = mPath.Substring(5);
				theRes.mResourceRef = this.GetSoundRef(text);
				int soundID = theRes.mResourceRef.GetSoundID();
				if (soundID == -1)
				{
					return this.Fail("Ref sound not found: " + text);
				}
				theRes.mSoundId = soundID;
				return true;
			}
			else
			{
				int freeSoundId = this.mApp.mSoundManager.GetFreeSoundId();
				if (freeSoundId < 0)
				{
					return this.Fail("Out of free sound ids");
				}
				if (!this.mApp.mSoundManager.LoadSound((uint)freeSoundId, theRes.mPath))
				{
					return this.Fail("Failed to load sound: " + theRes.mPath);
				}
				theRes.mSoundId = freeSoundId;
				if (theRes.mGlobalPtr != null)
				{
					theRes.mGlobalPtr.mResObject = freeSoundId;
				}
				theRes.ApplyConfig();
				this.ResourceLoadedHook(theRes);
				return true;
			}
		}

		public virtual bool DoLoadPopAnim(PopAnimRes theRes)
		{
			PopAnim popAnim = new PopAnim(0, null);
			popAnim.mImgScale = (float)this.mCurArtRes / (float)this.mLeadArtRes;
			popAnim.mDrawScale = (float)this.mCurArtRes / (float)this.mLeadArtRes;
			popAnim.LoadFile(theRes.mPath);
			if (popAnim.mError.Length > 0)
			{
				this.Fail("PopAnim loading error: " + popAnim.mError + " on file " + theRes.mPath);
				popAnim.Dispose();
				return false;
			}
			if (theRes.mGlobalPtr != null)
			{
				theRes.mGlobalPtr.mResObject = popAnim;
			}
			theRes.mPopAnim = popAnim;
			return true;
		}

		public virtual bool DoLoadPIEffect(PIEffectRes theRes)
		{
			PIEffect pieffect = new PIEffect();
			pieffect.LoadEffect(theRes.mPath);
			if (pieffect.mError.Length > 0)
			{
				this.Fail("PIEffect loading error: " + pieffect.mError + " on file " + theRes.mPath);
				pieffect.Dispose();
				return false;
			}
			if (theRes.mGlobalPtr != null)
			{
				theRes.mGlobalPtr.mResObject = pieffect;
			}
			theRes.mPIEffect = pieffect;
			return true;
		}

		public virtual bool DoLoadRenderEffect(RenderEffectRes theRes)
		{
			return true;
		}

		public virtual bool DoLoadGenericResFile(GenericResFileRes theRes)
		{
			return true;
		}

		public int GetNumResources(string theGroup, Dictionary<string, BaseRes> theMap, bool curArtResOnly, bool curLocSetOnly)
		{
			int num = 0;
			if (theGroup.Length <= 0)
			{
				if (!curArtResOnly && !curLocSetOnly)
				{
					return theMap.Count;
				}
				foreach (KeyValuePair<string, BaseRes> keyValuePair in theMap)
				{
					BaseRes value = keyValuePair.Value;
					if ((!curArtResOnly || value.mArtRes == 0 || value.mArtRes == this.mCurArtRes) && (!curLocSetOnly || value.mLocSet == 0U || value.mLocSet == this.mCurLocSet) && !value.mFromProgram)
					{
						num++;
					}
				}
			}
			else
			{
				foreach (KeyValuePair<string, BaseRes> keyValuePair2 in theMap)
				{
					BaseRes value2 = keyValuePair2.Value;
					if ((!curArtResOnly || value2.mArtRes == 0 || value2.mArtRes == this.mCurArtRes) && (!curLocSetOnly || value2.mLocSet == 0U || value2.mLocSet == this.mCurLocSet) && (value2.mResGroup == theGroup || value2.mCompositeResGroup == theGroup) && !value2.mFromProgram)
					{
						num++;
					}
				}
			}
			return num;
		}

		public ResourceManager(SexyAppBase theApp)
		{
			this.mApp = theApp;
			for (int i = 0; i < 7; i++)
			{
				this.mResMaps[i] = new Dictionary<string, BaseRes>();
			}
			this.mBaseArtRes = 0;
			this.mLeadArtRes = 0;
			this.mCurArtRes = 0;
			this.mCurLocSet = 1162761555U;
			this.mHasFailed = false;
			this.mXMLParser = null;
			this.mResGenMajorVersion = 0;
			this.mResGenMinorVersion = 0;
			this.mAllowMissingProgramResources = false;
			this.mAllowAlreadyDefinedResources = false;
			this.mCurResGroupList = null;
			this.mReloadIdx = 0;
		}

		public virtual void Dispose()
		{
			for (int i = 0; i < 7; i++)
			{
				this.DeleteMap(this.mResMaps[i]);
			}
		}

		public bool ParseResourcesFileBinary(byte[] data)
		{
			this.mXMLParser = new XMLParser();
			this.mXMLParser.checkEncodingType(data);
			this.mXMLParser.SetBytes(data);
			XMLElement xmlelement = new XMLElement();
			while (!this.mXMLParser.HasFailed())
			{
				if (!this.mXMLParser.NextElement(xmlelement))
				{
					this.Fail(this.mXMLParser.GetErrorText());
				}
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					if (!(xmlelement.mValue.ToString() != "ResourceManifest"))
					{
						if (xmlelement.GetAttribute("version").Length > 0)
						{
							int.Parse(xmlelement.GetAttribute("version"));
						}
						return this.DoParseResources();
					}
					break;
				}
			}
			this.Fail("Expecting ResourceManifest tag");
			return this.DoParseResources();
		}

		public bool ParseResourcesFile(string theFilename)
		{
			this.mLastXMLFileName = theFilename;
			if (this.mApp.mResStreamsManager != null && this.mApp.mResStreamsManager.IsInitialized())
			{
				return this.mApp.mResStreamsManager.LoadResourcesManifest(this);
			}
			this.mXMLParser = new XMLParser();
			if (!this.mXMLParser.OpenFile(theFilename))
			{
				this.Fail("Resource file not found: " + theFilename);
			}
			XMLElement xmlelement = new XMLElement();
			while (!this.mXMLParser.HasFailed())
			{
				if (!this.mXMLParser.NextElement(xmlelement))
				{
					this.Fail(this.mXMLParser.GetErrorText());
				}
				if (xmlelement.mType == XMLElement.XMLElementType.TYPE_START)
				{
					if (!(xmlelement.mValue.ToString() != "ResourceManifest"))
					{
						if (xmlelement.GetAttribute("version").Length > 0)
						{
							int.Parse(xmlelement.GetAttribute("version"));
						}
						return this.DoParseResources();
					}
					break;
				}
			}
			this.Fail("Expecting ResourceManifest tag");
			return this.DoParseResources();
		}

		public bool ReparseResourcesFile(string theFilename)
		{
			bool flag = this.mAllowAlreadyDefinedResources;
			this.mAllowAlreadyDefinedResources = true;
			this.mReloadIdx++;
			bool result = this.ParseResourcesFile(theFilename);
			for (int i = 0; i < 7; i++)
			{
				foreach (KeyValuePair<string, BaseRes> keyValuePair in this.mResMaps[i])
				{
					BaseRes value = keyValuePair.Value;
					if (value.mReloadIdx != this.mReloadIdx)
					{
						value.DeleteResource();
					}
				}
			}
			this.mAllowAlreadyDefinedResources = flag;
			return result;
		}

		public ResGlobalPtr RegisterGlobalPtr(string theId)
		{
			for (int i = 0; i < 7; i++)
			{
				BaseRes baseRes = this.GetBaseRes(i, theId);
				if (baseRes != null)
				{
					return baseRes.mGlobalPtr;
				}
			}
			return null;
		}

		public void ReapplyConfigs()
		{
			for (int i = 0; i < 7; i++)
			{
				foreach (KeyValuePair<string, BaseRes> keyValuePair in this.mResMaps[i])
				{
					keyValuePair.Value.ApplyConfig();
				}
			}
		}

		public string GetErrorText()
		{
			return this.mError;
		}

		public bool HadError()
		{
			return this.mHasFailed;
		}

		public bool IsGroupLoaded(string theGroup)
		{
			return this.mLoadedGroups.Contains(theGroup);
		}

		public bool IsResourceLoaded(string theId)
		{
			return this.GetImage(theId).GetDeviceImage() != null || this.GetFont(theId) != null || this.GetSound(theId) != -1;
		}

		public int GetNumImages(string theGroup, bool curArtResOnly, bool curLocSetOnly)
		{
			return this.GetNumResources(theGroup, this.mResMaps[0], curArtResOnly, curLocSetOnly);
		}

		public int GetNumSounds(string theGroup, bool curArtResOnly, bool curLocSetOnly)
		{
			return this.GetNumResources(theGroup, this.mResMaps[1], curArtResOnly, curLocSetOnly);
		}

		public int GetNumFonts(string theGroup, bool curArtResOnly, bool curLocSetOnly)
		{
			return this.GetNumResources(theGroup, this.mResMaps[2], curArtResOnly, curLocSetOnly);
		}

		public int GetNumResources(string theGroup, bool curArtResOnly, bool curLocSetOnly)
		{
			int num = 0;
			for (int i = 0; i < 7; i++)
			{
				num += this.GetNumResources(theGroup, this.mResMaps[i], curArtResOnly, curLocSetOnly);
			}
			return num;
		}

		public virtual bool DoLoadResource(BaseRes theRes, out bool skipped)
		{
			skipped = false;
			if (theRes.mFromProgram)
			{
				skipped = true;
				return true;
			}
			switch (theRes.mType)
			{
			case ResType.ResType_Image:
			{
				ImageRes theRes2 = (ImageRes)theRes;
				return this.DoLoadImage(theRes2);
			}
			case ResType.ResType_Sound:
			{
				SoundRes theRes3 = (SoundRes)theRes;
				return this.DoLoadSound(theRes3);
			}
			case ResType.ResType_Font:
			{
				FontRes theRes4 = (FontRes)theRes;
				return this.DoLoadFont(theRes4);
			}
			case ResType.ResType_PopAnim:
			{
				PopAnimRes theRes5 = (PopAnimRes)theRes;
				return this.DoLoadPopAnim(theRes5);
			}
			case ResType.ResType_PIEffect:
			{
				PIEffectRes theRes6 = (PIEffectRes)theRes;
				return this.DoLoadPIEffect(theRes6);
			}
			case ResType.ResType_RenderEffect:
			{
				RenderEffectRes theRes7 = (RenderEffectRes)theRes;
				return this.DoLoadRenderEffect(theRes7);
			}
			case ResType.ResType_GenericResFile:
			{
				GenericResFileRes theRes8 = (GenericResFileRes)theRes;
				return this.DoLoadGenericResFile(theRes8);
			}
			default:
				return false;
			}
		}

		public virtual bool LoadNextResource()
		{
			if (this.HadError())
			{
				return false;
			}
			if (this.mCurResGroupList == null)
			{
				return false;
			}
			while (this.mCurResGroupListItr.MoveNext())
			{
				bool result = true;
				bool flag = true;
				BaseRes baseRes = this.mCurResGroupListItr.Current;
				if (baseRes.mRefCount == 0)
				{
					result = this.DoLoadResource(baseRes, out flag);
				}
				baseRes.mDirectLoaded = true;
				baseRes.mRefCount = 1;
				if (!flag)
				{
					return result;
				}
			}
			if (this.mCurCompositeResGroup.Length > 0 && this.mCompositeResGroupMap.ContainsKey(this.mCurCompositeResGroup))
			{
				CompositeResGroup compositeResGroup = this.mCompositeResGroupMap[this.mCurCompositeResGroup];
				int count = compositeResGroup.mSubGroups.Count;
				for (int i = this.mCurCompositeSubGroupIndex + 1; i < count; i++)
				{
					SubGroup subGroup = compositeResGroup.mSubGroups[i];
					if (subGroup.mGroupName.Length > 0 && (subGroup.mArtRes == 0 || subGroup.mArtRes == this.mCurArtRes) && (subGroup.mLocSet == 0U || subGroup.mLocSet == this.mCurLocSet))
					{
						this.mCurCompositeSubGroupIndex = i;
						this.StartLoadResources(subGroup.mGroupName, true);
						return this.LoadNextResource();
					}
				}
			}
			return false;
		}

		public virtual void ResourceLoadedHook(BaseRes theRes)
		{
		}

		public virtual void PrepareLoadResources(string theGroup)
		{
			if (this.mApp.mResStreamsManager != null && this.mApp.mResStreamsManager.IsInitialized())
			{
				this.mApp.mResStreamsManager.LoadGroup(theGroup);
			}
		}

		public virtual void StartLoadResources(string theGroup, bool fromComposite)
		{
			if (!fromComposite)
			{
				this.mError = "";
				this.mHasFailed = false;
				this.mCurCompositeResGroup = "";
				this.mCurCompositeSubGroupIndex = 0;
				if (this.mCompositeResGroupMap.ContainsKey(theGroup))
				{
					this.mCurResGroup = "";
					this.mCurResGroupList = null;
					this.mCurCompositeResGroup = theGroup;
					CompositeResGroup compositeResGroup = this.mCompositeResGroupMap[theGroup];
					int count = compositeResGroup.mSubGroups.Count;
					for (int i = 0; i < count; i++)
					{
						SubGroup subGroup = compositeResGroup.mSubGroups[i];
						if (subGroup.mGroupName.Length > 0 && (subGroup.mArtRes == 0 || subGroup.mArtRes == this.mCurArtRes) && (subGroup.mLocSet == 0U || subGroup.mLocSet == this.mCurLocSet))
						{
							this.mCurCompositeSubGroupIndex = i;
							this.StartLoadResources(subGroup.mGroupName, true);
							return;
						}
					}
					return;
				}
			}
			if (this.mResGroupMap.ContainsKey(theGroup))
			{
				this.mCurResGroup = theGroup;
				this.mCurResGroupList = this.mResGroupMap[theGroup];
				this.mCurResGroupListItr = this.mCurResGroupList.mResList.GetEnumerator();
			}
		}

		public virtual bool LoadResources(string theGroup)
		{
			if (this.mApp.mResStreamsManager != null && this.mApp.mResStreamsManager.IsInitialized())
			{
				this.mApp.mResStreamsManager.ForceLoadGroup(theGroup);
			}
			this.mError = "";
			this.mHasFailed = false;
			this.StartLoadResources(theGroup, false);
			while (this.LoadNextResource())
			{
			}
			if (!this.HadError())
			{
				this.mLoadedGroups.Add(theGroup);
				return true;
			}
			return false;
		}

		public bool ReplaceImage(string theId, Image theImage)
		{
			throw new NotImplementedException();
		}

		public bool ReplaceSound(string theId, int theSound)
		{
			throw new NotImplementedException();
		}

		public bool ReplaceFont(string theId, Font theFont)
		{
			throw new NotImplementedException();
		}

		public bool ReplacePopAnim(string theId, PopAnim theFont)
		{
			throw new NotImplementedException();
		}

		public bool ReplacePIEffect(string theId, PIEffect theFont)
		{
			throw new NotImplementedException();
		}

		public bool ReplaceRenderEffect(string theId, RenderEffectDefinition theDefinition)
		{
			throw new NotImplementedException();
		}

		public bool ReplaceGenericResFile(string theId, GenericResFile theFile)
		{
			throw new NotImplementedException();
		}

		public void DeleteImage(string theName)
		{
			BaseRes baseRes = this.GetBaseRes(0, theName);
			if (baseRes != null && baseRes.mDirectLoaded)
			{
				baseRes.mDirectLoaded = false;
				this.Deref(baseRes);
			}
		}

		public SharedImageRef LoadImage(string theName)
		{
			new AutoCrit(this.mLoadCrit);
			ImageRes imageRes = (ImageRes)this.GetBaseRes(0, theName);
			if (imageRes == null)
			{
				return null;
			}
			if (!imageRes.mDirectLoaded)
			{
				imageRes.mRefCount++;
				imageRes.mDirectLoaded = true;
			}
			if (imageRes.mImage.GetDeviceImage() != null)
			{
				return imageRes.mImage;
			}
			if (imageRes.mFromProgram)
			{
				return null;
			}
			if (!this.DoLoadImage(imageRes))
			{
				return null;
			}
			return imageRes.mImage;
		}

		public SexyPoint GetImageOffset(string theName)
		{
			ImageRes imageRes = (ImageRes)this.GetBaseRes(0, theName);
			if (imageRes != null)
			{
				return imageRes.mOffset;
			}
			return ResourceManager.aEmptyPoint;
		}

		public void DeleteFont(string theName)
		{
			throw new NotImplementedException();
		}

		public Font LoadFont(string theName)
		{
			new AutoCrit(this.mLoadCrit);
			FontRes fontRes = (FontRes)this.GetBaseRes(2, theName);
			if (fontRes == null)
			{
				return null;
			}
			if (!fontRes.mDirectLoaded)
			{
				fontRes.mRefCount++;
				fontRes.mDirectLoaded = true;
			}
			if (fontRes.mFont != null)
			{
				return fontRes.mFont;
			}
			if (fontRes.mFromProgram)
			{
				return null;
			}
			if (!this.DoLoadFont(fontRes))
			{
				return null;
			}
			return fontRes.mFont;
		}

		public int LoadSound(string theName)
		{
			new AutoCrit(this.mLoadCrit);
			SoundRes soundRes = (SoundRes)this.GetBaseRes(1, theName);
			if (soundRes == null)
			{
				return -1;
			}
			if (!soundRes.mDirectLoaded)
			{
				soundRes.mRefCount++;
				soundRes.mDirectLoaded = true;
			}
			if (soundRes.mSoundId != 0)
			{
				return soundRes.mSoundId;
			}
			if (soundRes.mFromProgram)
			{
				return -1;
			}
			if (!this.DoLoadSound(soundRes))
			{
				return -1;
			}
			return soundRes.mSoundId;
		}

		public void DeleteSound(string theName)
		{
			throw new NotImplementedException();
		}

		public void DeletePopAnim(string theName)
		{
			throw new NotImplementedException();
		}

		public PopAnim LoadPopAnim(string theName)
		{
			PopAnimRes popAnimRes = (PopAnimRes)this.GetBaseRes(3, theName);
			if (popAnimRes == null)
			{
				return null;
			}
			if (!popAnimRes.mDirectLoaded)
			{
				popAnimRes.mRefCount++;
				popAnimRes.mDirectLoaded = true;
			}
			if (popAnimRes.mPopAnim != null)
			{
				return popAnimRes.mPopAnim;
			}
			if (popAnimRes.mFromProgram)
			{
				return null;
			}
			if (!this.DoLoadPopAnim(popAnimRes))
			{
				return null;
			}
			return popAnimRes.mPopAnim;
		}

		public void DeletePIEffect(string theName)
		{
			throw new NotImplementedException();
		}

		public PIEffect LoadPIEffect(string theName)
		{
			PIEffectRes pieffectRes = (PIEffectRes)this.GetBaseRes(4, theName);
			if (pieffectRes == null)
			{
				return null;
			}
			if (!pieffectRes.mDirectLoaded)
			{
				pieffectRes.mRefCount++;
				pieffectRes.mDirectLoaded = true;
			}
			if (pieffectRes.mPIEffect != null)
			{
				return pieffectRes.mPIEffect;
			}
			if (pieffectRes.mFromProgram)
			{
				return null;
			}
			if (!this.DoLoadPIEffect(pieffectRes))
			{
				return null;
			}
			return pieffectRes.mPIEffect;
		}

		public void DeleteRenderEffect(string theName)
		{
			throw new NotImplementedException();
		}

		public RenderEffectDefinition LoadRenderEffect(string theName)
		{
			throw new NotImplementedException();
		}

		public void DeleteGenericResFile(string theName)
		{
			throw new NotImplementedException();
		}

		public GenericResFile LoadGenericResFile(string theName)
		{
			throw new NotImplementedException();
		}

		public SharedImageRef GetImage(string theId)
		{
			ImageRes imageRes = (ImageRes)this.GetBaseRes(0, theId);
			if (imageRes == null)
			{
				return null;
			}
			return imageRes.mImage;
		}

		public int GetSound(string theId)
		{
			SoundRes soundRes = (SoundRes)this.GetBaseRes(1, theId);
			if (soundRes == null)
			{
				return 0;
			}
			return soundRes.mSoundId;
		}

		public Font GetFont(string theId)
		{
			FontRes fontRes = (FontRes)this.GetBaseRes(2, theId);
			if (fontRes == null)
			{
				return null;
			}
			return fontRes.mFont;
		}

		public PopAnim GetPopAnim(string theId)
		{
			PopAnimRes popAnimRes = (PopAnimRes)this.GetBaseRes(3, theId);
			if (popAnimRes == null)
			{
				return null;
			}
			return popAnimRes.mPopAnim;
		}

		public PIEffect GetPIEffect(string theId)
		{
			PIEffectRes pieffectRes = (PIEffectRes)this.GetBaseRes(4, theId);
			if (pieffectRes == null)
			{
				return null;
			}
			return pieffectRes.mPIEffect;
		}

		public RenderEffectDefinition GetRenderEffect(string theId)
		{
			RenderEffectRes renderEffectRes = (RenderEffectRes)this.GetBaseRes(5, theId);
			if (renderEffectRes == null)
			{
				return null;
			}
			return renderEffectRes.mRenderEffectDefinition;
		}

		public GenericResFile GetGenericResFile(string theId)
		{
			GenericResFileRes genericResFileRes = (GenericResFileRes)this.GetBaseRes(6, theId);
			if (genericResFileRes == null)
			{
				return null;
			}
			return genericResFileRes.mGenericResFile;
		}

		public string GetIdByPath(string thePath)
		{
			string text = thePath.Replace('/', '\\');
			for (int i = 0; i < 7; i++)
			{
				foreach (KeyValuePair<string, BaseRes> keyValuePair in this.mResMaps[i])
				{
					if (keyValuePair.Value.mPath == text)
					{
						return keyValuePair.Value.mId;
					}
				}
			}
			text = text.ToUpper();
			for (int j = 0; j < 7; j++)
			{
				foreach (KeyValuePair<string, BaseRes> keyValuePair3 in this.mResMaps[j])
				{
					if (keyValuePair3.Value.mPath.ToUpper() == text)
					{
						return keyValuePair3.Value.mId;
					}
				}
			}
			return "";
		}

		public Dictionary<string, string> GetImageAttributes(string theId)
		{
			ImageRes imageRes = (ImageRes)this.GetBaseRes(0, theId);
			if (imageRes != null)
			{
				return imageRes.mXMLAttributes;
			}
			return null;
		}

		public virtual SharedImageRef GetImageThrow(string theId, int artRes, bool optional)
		{
			if (this.mApp.mShutdown)
			{
				return null;
			}
			if (artRes != 0 && artRes != this.mCurArtRes)
			{
				this.Fail(string.Concat(new object[] { "Attempted to load image of incorrect art resolution ", artRes, " (expected ", this.mCurArtRes, "):", theId }));
				throw new ResourceManagerException(this.GetErrorText());
			}
			ImageRes imageRes = (ImageRes)this.GetBaseRes(0, theId);
			if (imageRes != null)
			{
				if (imageRes.mImage.GetMemoryImage() != null)
				{
					return imageRes.mImage;
				}
				if (this.mAllowMissingProgramResources && imageRes.mFromProgram)
				{
					return null;
				}
			}
			else if (optional)
			{
				return null;
			}
			this.Fail("Image resource not found:" + theId);
			imageRes = (ImageRes)this.GetBaseRes(0, theId);
			throw new ResourceManagerException(this.GetErrorText());
		}

		public virtual int GetSoundThrow(string theId)
		{
			throw new NotImplementedException();
		}

		public virtual Font GetFontThrow(string theId, int artRes)
		{
			throw new NotImplementedException();
		}

		public virtual PopAnim GetPopAnimThrow(string theId)
		{
			throw new NotImplementedException();
		}

		public virtual PIEffect GetPIEffectThrow(string theId)
		{
			throw new NotImplementedException();
		}

		public virtual RenderEffectDefinition GetRenderEffectThrow(string theId)
		{
			throw new NotImplementedException();
		}

		public virtual GenericResFile GetGenericResFileThrow(string theId)
		{
			throw new NotImplementedException();
		}

		public ResourceRef GetResourceRef(BaseRes theBaseRes)
		{
			ResourceRef resourceRef = new ResourceRef();
			bool flag = false;
			resourceRef.mBaseResP = theBaseRes;
			if (theBaseRes.mRefCount == 0)
			{
				this.DoLoadResource(theBaseRes, out flag);
			}
			theBaseRes.mRefCount = 1;
			return resourceRef;
		}

		public ResourceRef GetResourceRef(int theType, string theId)
		{
			BaseRes baseRes = this.GetBaseRes(theType, theId);
			if (baseRes != null)
			{
				return this.GetResourceRef(baseRes);
			}
			return null;
		}

		public ResourceRef GetResourceRefFromPath(string theFileName)
		{
			string text = theFileName.ToUpper();
			if (text.IndexOf(".") != -1)
			{
				text = text.Substring(0, text.IndexOf("."));
			}
			if (this.mResFromPathMap.ContainsKey(text))
			{
				return this.GetResourceRef(this.mResFromPathMap[text]);
			}
			return null;
		}

		public SexyPoint GetOffsetOfImage(string theId)
		{
			ImageRes imageRes = (ImageRes)this.GetBaseRes(0, theId);
			if (imageRes == null)
			{
				return null;
			}
			return imageRes.mOffset;
		}

		public ResourceRef GetImageRef(string theId)
		{
			return this.GetResourceRef(0, theId);
		}

		public ResourceRef GetImageRef(Image theGlobalPtrRef)
		{
			throw new NotImplementedException();
		}

		public ResourceRef GetSoundRef(string theId)
		{
			return this.GetResourceRef(1, theId);
		}

		public ResourceRef GetSoundRef(int theGlobalPtrRef)
		{
			throw new NotImplementedException();
		}

		public ResourceRef GetFontRef(string theId)
		{
			return this.GetResourceRef(2, theId);
		}

		public ResourceRef GetPopAnimRef(string theId)
		{
			return this.GetResourceRef(3, theId);
		}

		public ResourceRef GetPopAnimRef(PopAnim theGlobalPtrRef)
		{
			throw new NotImplementedException();
		}

		public ResourceRef GetPIEffectRef(string theId)
		{
			return this.GetResourceRef(4, theId);
		}

		public ResourceRef GetPIEffectRef(PIEffect theGlobalPtrRef)
		{
			throw new NotImplementedException();
		}

		public ResourceRef GetRenderEffectRef(string theId)
		{
			return this.GetResourceRef(5, theId);
		}

		public ResourceRef GetRenderEffectRef(RenderEffectDefinition theGlobalPtrRef)
		{
			throw new NotImplementedException();
		}

		public ResourceRef GetGenericResFileRef(string theId)
		{
			return this.GetResourceRef(6, theId);
		}

		public ResourceRef GetGenericResFileRef(GenericResFile theGlobalPtrRef)
		{
			throw new NotImplementedException();
		}

		public void SetAllowMissingProgramImages(bool allow)
		{
			this.mAllowMissingProgramResources = allow;
		}

		public virtual void DeleteResources(string theGroup)
		{
			for (int i = 0; i < 7; i++)
			{
				this.DeleteResources(this.mResMaps[i], theGroup);
			}
			this.mLoadedGroups.Remove(theGroup);
			if (this.mApp.mResStreamsManager != null && this.mApp.mResStreamsManager.IsInitialized())
			{
				this.mApp.mResStreamsManager.DeleteGroup(theGroup);
			}
		}

		public void DeleteExtraImageBuffers(string theGroup)
		{
		}

		public ResGroup GetCurResGroupList()
		{
			return this.mCurResGroupList;
		}

		public string GetCurResGroup()
		{
			return this.mCurResGroup;
		}

		public void DumpCurResGroup(ref string theDestStr)
		{
			ResGroup resGroup = this.mResGroupMap[this.mCurResGroup];
			List<BaseRes>.Enumerator enumerator = resGroup.mResList.GetEnumerator();
			object obj = theDestStr;
			theDestStr = string.Concat(new object[]
			{
				obj,
				"\n About to dump ",
				resGroup.mResList.Count,
				" elements from current res group name \r\n"
			});
			while (enumerator.MoveNext())
			{
				BaseRes baseRes = enumerator.Current;
				string text = baseRes.mId + ":" + baseRes.mPath + "\r\n";
				theDestStr += text;
				if (baseRes.mFromProgram)
				{
					theDestStr += "     res is from program\r\n";
				}
				else if (baseRes.mType == ResType.ResType_Image)
				{
					theDestStr += "     res is an image\r\n";
				}
				else if (baseRes.mType == ResType.ResType_Sound)
				{
					theDestStr += "     res is a sound\r\n";
				}
				else if (baseRes.mType == ResType.ResType_Font)
				{
					theDestStr += "     res is a font\r\n";
				}
				else if (baseRes.mType == ResType.ResType_PopAnim)
				{
					theDestStr += "     res is a popanim\r\n";
				}
				else if (baseRes.mType == ResType.ResType_PIEffect)
				{
					theDestStr += "     res is a pieffect\r\n";
				}
				else if (baseRes.mType == ResType.ResType_RenderEffect)
				{
					theDestStr += "     res is a rendereffectdefinition\r\n";
				}
				else if (baseRes.mType == ResType.ResType_GenericResFile)
				{
					theDestStr += "     res is a genericresfile\r\n";
				}
				if (enumerator.Current == this.mCurResGroupListItr.Current)
				{
					theDestStr += "iterator has reached mCurResGroupItr\r\n";
				}
			}
			theDestStr += "Done dumping resources\r\n";
		}

		public void DumpAllGroup(ref string theDestStr)
		{
			string text = this.mCurResGroup;
			foreach (KeyValuePair<string, ResGroup> keyValuePair in this.mResGroupMap)
			{
				this.mCurResGroup = keyValuePair.Key;
				this.DumpCurResGroup(ref theDestStr);
			}
			this.mCurResGroup = text;
		}

		public string GetLocaleFolder(bool addTrailingSlash)
		{
			uint num = this.mCurLocSet;
			if (num == 0U)
			{
				return "";
			}
			string text = string.Concat(new object[]
			{
				"locales/",
				(num >> 24) & 255U,
				(num >> 16) & 255U,
				"-",
				(num >> 8) & 255U,
				num & 255U
			});
			if (addTrailingSlash)
			{
				text += '/';
			}
			return text;
		}

		public uint GetLocSetForLocaleName(string theLocaleName)
		{
			throw new NotImplementedException();
		}

		public void PrepareLoadResourcesList(string[] theGroups)
		{
			if (this.mApp.mResStreamsManager != null && this.mApp.mResStreamsManager.IsInitialized())
			{
				int num = 0;
				while (theGroups[num] != null)
				{
					int num2 = this.mApp.mResStreamsManager.LookupGroup(theGroups[num]);
					if (num2 != -1)
					{
						this.mApp.mResStreamsManager.LoadGroup(num2);
					}
					num++;
				}
			}
			this.LoadGroupAsyn(theGroups);
		}

		public float GetLoadResourcesListProgress(string[] theGroups)
		{
			if (this.mTotalResNum == 0)
			{
				return 0f;
			}
			if (!this.mLoadFinished && this.mCurLoadedResNum != this.mTotalResNum)
			{
				return (float)this.mCurLoadedResNum / (float)this.mTotalResNum;
			}
			if (!this.mLoadFinished)
			{
				return 0.99f;
			}
			return 1f;
		}

		public bool IsLoadSuccess()
		{
			return this.mLoadSuccess;
		}

		public void LoadGroupAsyn(string[] group)
		{
			if (this.mIsLoading)
			{
				return;
			}
			this.mError = "";
			this.mHasFailed = false;
			this.mLoadFinished = false;
			this.mGroupToLoad.Clear();
			this.mTotalResNum = 0;
			this.mCurLoadedResNum = 0;
			for (int i = 0; i < group.Length; i++)
			{
				this.GetLoadingGroup(group[i]);
				foreach (ResourceManager.GroupLoadInfo groupLoadInfo in this.mGroupToLoad)
				{
					this.mTotalResNum += groupLoadInfo.mTotalFile;
				}
			}
			this.mIsLoading = true;
			this.mLoadSuccess = false;
			this.mLoadingProc = new ParameterizedThreadStart(this.LoadingProc);
			this.mLoadingThread = new Thread(this.mLoadingProc);
			this.mLoadingThread.Start(group);
		}

		private void GetLoadingGroup(string theGroup)
		{
			this.mGroupToLoad.Clear();
			this.mError = "";
			this.mHasFailed = false;
			if (theGroup == null)
			{
				return;
			}
			if (this.mCompositeResGroupMap.ContainsKey(theGroup))
			{
				CompositeResGroup compositeResGroup = this.mCompositeResGroupMap[theGroup];
				int count = compositeResGroup.mSubGroups.Count;
				for (int i = 0; i < count; i++)
				{
					SubGroup subGroup = compositeResGroup.mSubGroups[i];
					if (subGroup.mGroupName.Length > 0 && (subGroup.mArtRes == 0 || subGroup.mArtRes == this.mCurArtRes) && (subGroup.mLocSet == 0U || subGroup.mLocSet == this.mCurLocSet))
					{
						this.mCurCompositeSubGroupIndex = i;
						if (this.mResGroupMap.ContainsKey(subGroup.mGroupName))
						{
							ResGroup resGroup = this.mResGroupMap[subGroup.mGroupName];
							this.mGroupToLoad.Add(new ResourceManager.GroupLoadInfo(subGroup.mGroupName, resGroup.mResList.Count));
						}
					}
				}
				return;
			}
			if (this.mResGroupMap.ContainsKey(theGroup))
			{
				ResGroup resGroup2 = this.mResGroupMap[theGroup];
				this.mGroupToLoad.Add(new ResourceManager.GroupLoadInfo(theGroup, resGroup2.mResList.Count));
			}
		}

		private void LoadingProc(object theGroup)
		{
			string[] array = (string[])theGroup;
			for (int i = 0; i < array.Length - 1; i++)
			{
				this.StartLoadResources(array[i], false);
				while (this.LoadNextResource())
				{
					this.mCurLoadedResNum++;
					Thread.Sleep(3);
				}
				if (this.HadError())
				{
					this.mLoadSuccess = false;
					break;
				}
				this.mLoadedGroups.Add(array[i]);
				this.mLoadSuccess = true;
			}
			this.mIsLoading = false;
			this.mLoadFinished = true;
		}

		public static void ReadIntVector(string theVal, List<int> theVector)
		{
			theVector.Clear();
			string[] array = theVal.Split(new char[] { ',' });
			foreach (string text in array)
			{
				theVector.Add(int.Parse(text));
			}
		}

		public List<string> mLoadedGroups = new List<string>();

		public Dictionary<string, BaseRes>[] mResMaps = new Dictionary<string, BaseRes>[7];

		public Dictionary<string, BaseRes> mResFromPathMap = new Dictionary<string, BaseRes>();

		public Dictionary<string, ResGroup> mResGroupMap = new Dictionary<string, ResGroup>();

		public Dictionary<string, CompositeResGroup> mCompositeResGroupMap = new Dictionary<string, CompositeResGroup>();

		public List<int> mSupportedLocSets = new List<int>();

		public ResGroup mCurResGroupList;

		public List<BaseRes>.Enumerator mCurResGroupListItr;

		public CritSect mLoadCrit = default(CritSect);

		public SexyAppBase mApp;

		public string mLastXMLFileName;

		public string mResGenExePath;

		public string mResPropsUsed;

		public string mResWatchFileUsed;

		public string mResGenTargetName;

		public string mResGenRelSrcRootFromDist;

		public string mError;

		public string mCurCompositeResGroup;

		public string mCurResGroup;

		public string mDefaultPath;

		public string mDefaultIdPrefix;

		public int mResGenMajorVersion;

		public int mResGenMinorVersion;

		public int mCurResGroupArtRes;

		public int mReloadIdx;

		public int mCurCompositeSubGroupIndex;

		public int mBaseArtRes;

		public int mCurArtRes;

		public int mLeadArtRes;

		public uint mCurLocSet;

		public uint mCurResGroupLocSet;

		public int mTotalResNum;

		public int mCurLoadedResNum;

		public bool mHasFailed;

		public bool mAllowMissingProgramResources;

		public bool mAllowAlreadyDefinedResources;

		public bool mHadAlreadyDefinedError;

		public XMLParser mXMLParser;

		private string mCurArtResKey = "";

		private string mCurLocSetKey = "";

		private string mCurArtResAndLocSetKey = "";

		private static SexyPoint aEmptyPoint = new SexyPoint();

		public bool mIsLoading;

		private Thread mLoadingThread;

		private ParameterizedThreadStart mLoadingProc;

		private bool mLoadSuccess;

		private List<ResourceManager.GroupLoadInfo> mGroupToLoad = new List<ResourceManager.GroupLoadInfo>();

		private bool mLoadFinished;

		private class GroupLoadInfo
		{
			public GroupLoadInfo(string name, int totalFiles)
			{
				this.mName = name;
				this.mTotalFile = totalFiles;
				this.mCurrentFile = 0;
			}

			public string mName;

			public int mTotalFile;

			public int mCurrentFile;
		}
	}
}
