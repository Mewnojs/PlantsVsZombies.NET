using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Sexy.TodLib;

namespace Sexy
{
	public/*internal*/ class ResourceManager : IDisposable
	{
		public ResourceManager(SexyAppBase theApp)
		{
			mApp = theApp;
			mHasFailed = false;
			mXMLParser = null;
			mCurResGroupList = null;
			mResGroupMap = new Dictionary<string, List<BaseRes>>();
			mImageMap = new Dictionary<string, BaseRes>();
			mSoundMap = new Dictionary<string, BaseRes>();
			mFontMap = new Dictionary<string, BaseRes>();
			mMusicMap = new Dictionary<string, BaseRes>();
			mLevelMap = new Dictionary<string, BaseRes>();
			mReanimMap = new Dictionary<string, BaseRes>();
			mParticleMap = new Dictionary<string, BaseRes>();
			mTrailMap = new Dictionary<string, BaseRes>();
			mLoadedGroups = new List<string>();
			mContentManager = mApp.mContentManager;
			for (int i = 0; i < ResourceManager.mUnloadContentManager.Length; i++)
			{
				ResourceManager.mUnloadContentManager[i] = new ContentManager(SexyAppBase.XnaGame.Services);
				ResourceManager.mUnloadContentManager[i].RootDirectory = mApp.mContentManager.RootDirectory;
			}
			ResourceManager.mReanimContentManager = new ContentManager(SexyAppBase.XnaGame.Services);
			ResourceManager.mReanimContentManager.RootDirectory = mApp.mContentManager.RootDirectory;
			ResourceManager.mParticleContentManager = new ContentManager(SexyAppBase.XnaGame.Services);
			ResourceManager.mParticleContentManager.RootDirectory = mApp.mContentManager.RootDirectory;
			backDropContentManager = new ContentManager(SexyAppBase.XnaGame.Services);
			backDropContentManager.RootDirectory = mContentManager.RootDirectory;
			mAllowAlreadyDefinedResources = false;
			mAllowMissingProgramResources = false;
			mProgress = 0.0;
			mTotalResources = 0;
			mLoadedCount = 0;
			arial = mContentManager.Load<SpriteFont>("fonts/Arial");
		}

		public virtual string GetResourceDir()
		{
			return "";
		}

		private ContentManager GetContentManager(BaseRes res)
		{
			if (res.mUnloadGroup <= 0)
			{
				return mContentManager;
			}
			return ResourceManager.mUnloadContentManager[res.mUnloadGroup];
		}

		~ResourceManager()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public bool HadError()
		{
			return mHasFailed;
		}

		public bool IsGroupLoaded(string theResGroup)
		{
			return mLoadedGroups.Contains(theResGroup);
		}

		public bool LoadReanimation(string filename, ref ReanimatorDefinition def)
		{
			def = ResourceManager.mReanimContentManager.Load<ReanimatorDefinition>(filename);
			def.ExtractImages();
			return true;
		}

		public bool LoadParticle(string filename, ref TodParticleDefinition def)
		{
			def = ResourceManager.mParticleContentManager.Load<TodParticleDefinition>(filename);
			return true;
		}

		public bool LoadTrail(string filename, ref TrailDefinition def)
		{
			def = mContentManager.Load<TrailDefinition>(filename);
			return true;
		}

		public void ExtractReanimImages()
		{
			foreach (KeyValuePair<string, BaseRes> keyValuePair in mReanimMap)
			{
				((ReanimRes)keyValuePair.Value).mReanim.ExtractImages();
			}
		}

		public bool ParseResourcesFile(string theFilename)
		{
			mXMLParser = new XMLParser();
			if (!mXMLParser.OpenFile(Path.Combine(GetResourceDir(), theFilename)))
			{
				Fail("Resource file not found: " + theFilename);
			}
			XMLElement xmlelement = new XMLElement();
			while (!mXMLParser.HasFailed())
			{
				if (!mXMLParser.NextElement(ref xmlelement))
				{
					Fail(mXMLParser.GetErrorText());
				}
				if (xmlelement.mType == XMLElementType.TYPE_START)
				{
					if (!(xmlelement.mValue != "ResourceManifest"))
					{
						return DoParseResources();
					}
					break;
				}
			}
			Fail("Expecting ResourceManifest tag");
			return DoParseResources();
		}

		private bool DoParseResources()
		{
			if (!mXMLParser.HasFailed())
			{
				XMLElement xmlelement;
				for (;;)
				{
					xmlelement = new XMLElement();
					if (!mXMLParser.NextElement(ref xmlelement))
					{
						goto IL_F7;
					}
					if (xmlelement.mType == XMLElementType.TYPE_START)
					{
						if (!(xmlelement.mValue == "Resources"))
						{
							goto IL_B1;
						}
						mCurResGroup = xmlelement.mAttributes["id"];
						mResGroupMap.Add(mCurResGroup, new List<BaseRes>());
						mCurResGroupList = mResGroupMap[mCurResGroup];
						if (mCurResGroup.Length == 0)
						{
							break;
						}
						if (!ParseResources())
						{
							goto Block_5;
						}
					}
					else if (xmlelement.mType == XMLElementType.TYPE_ELEMENT)
					{
						goto Block_6;
					}
				}
				Fail("No id specified.");
				Block_5:
				goto IL_F7;
				IL_B1:
				Fail("Invalid Section '" + xmlelement.mValue + "'");
				goto IL_F7;
				Block_6:
				Fail("Element Not Expected '" + xmlelement.mValue + "'");
			}
			IL_F7:
			if (mXMLParser.HasFailed())
			{
				Fail(mXMLParser.GetErrorText());
			}
			mXMLParser = null;
			return !mHasFailed;
		}

		private bool ParseResources()
		{
			mDefaultPath = GetResourceDir();
			XMLElement xmlelement;
			for (;;)
			{
				xmlelement = new XMLElement();
				if (!mXMLParser.NextElement(ref xmlelement))
				{
					break;
				}
				if (xmlelement.mType == XMLElementType.TYPE_START)
				{
					if (xmlelement.mValue == "Image")
					{
						if (!ParseImageResource(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_6;
						}
					}
					else if (xmlelement.mValue == "Reanim")
					{
						if (!ParseReanimResource(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_10;
						}
					}
					else if (xmlelement.mValue == "Particle")
					{
						if (!ParseParticleResource(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_14;
						}
					}
					else if (xmlelement.mValue == "Trail")
					{
						if (!ParseTrailResource(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_18;
						}
					}
					else if (xmlelement.mValue == "Sound")
					{
						if (!ParseSoundResource(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_22;
						}
					}
					else if (xmlelement.mValue == "Font")
					{
						if (!ParseFontResource(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_26;
						}
					}
					else if (xmlelement.mValue == "Music")
					{
						if (!ParseMusicResource(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_30;
						}
					}
					else if (xmlelement.mValue == "Level")
					{
						if (!ParseLevelResource(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_34;
						}
					}
					else
					{
						if (!(xmlelement.mValue == "SetDefaults"))
						{
							goto IL_2A2;
						}
						if (!ParseSetDefaults(xmlelement))
						{
							return false;
						}
						if (!mXMLParser.NextElement(ref xmlelement))
						{
							return false;
						}
						if (xmlelement.mType != XMLElementType.TYPE_END)
						{
							goto Block_38;
						}
					}
				}
				else
				{
					if (xmlelement.mType == XMLElementType.TYPE_ELEMENT)
					{
						goto Block_39;
					}
					if (xmlelement.mType == XMLElementType.TYPE_END)
					{
						return true;
					}
				}
			}
			return false;
			Block_6:
			return Fail("Unexpected element found.");
			Block_10:
			return Fail("Unexpected element found.");
			Block_14:
			return Fail("Unexpected element found.");
			Block_18:
			return Fail("Unexpected element found.");
			Block_22:
			return Fail("Unexpected element found.");
			Block_26:
			return Fail("Unexpected element found.");
			Block_30:
			return Fail("Unexpected element found.");
			Block_34:
			return Fail("Unexpected element found.");
			Block_38:
			return Fail("Unexpected element found.");
			IL_2A2:
			Fail("Invalid Section '" + xmlelement.mValue + "'");
			return false;
			Block_39:
			Fail("Element Not Expected '" + xmlelement.mValue + "'");
			return false;
		}

		private bool ParseCommonResource(XMLElement theElement, BaseRes theRes, Dictionary<string, BaseRes> theMap)
		{
			mHadAlreadyDefinedError = false;
			string text = theElement.mAttributes["path"];
			theRes.mXMLAttributes = theElement.mAttributes;
			theRes.mFromProgram = false;
			if (text.Length > 0 && text[0] == '!')
			{
				theRes.mPath = text;
				if (text == "!program")
				{
					theRes.mFromProgram = true;
				}
			}
			else
			{
				theRes.mPath = mDefaultPath + text;
			}
			Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
			string text2 = mDefaultIdPrefix;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, string> keyValuePair = enumerator.Current;
				if (keyValuePair.Key == "id")
				{
					string text3 = mDefaultIdPrefix;
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					text2 = text3 + keyValuePair2.Value;
				}
			}
			theRes.mResGroup = mCurResGroup;
			theRes.mId = text2;
			enumerator = theElement.mAttributes.GetEnumerator();
			string text4 = null;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, string> keyValuePair3 = enumerator.Current;
				if (keyValuePair3.Key == "unloadGroup")
				{
					KeyValuePair<string, string> keyValuePair4 = enumerator.Current;
					text4 = keyValuePair4.Value;
				}
			}
			if (!string.IsNullOrEmpty(text4))
			{
				theRes.mUnloadGroup = int.Parse(text4);
			}
			enumerator = theElement.mAttributes.GetEnumerator();
			string text5 = null;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, string> keyValuePair5 = enumerator.Current;
				if (keyValuePair5.Key == "surface")
				{
					KeyValuePair<string, string> keyValuePair6 = enumerator.Current;
					text5 = keyValuePair6.Value;
				}
			}
			if (!string.IsNullOrEmpty(text5))
			{
				((ImageRes)theRes).lowMemorySurfaceFormat = (SurfaceFormat)Enum.Parse(typeof(SurfaceFormat), text5, true);
			}
			if (theMap.ContainsKey(text2))
			{
				mHadAlreadyDefinedError = true;
				return Fail("Resource already defined.");
			}
			theMap.Add(text2, theRes);
			mCurResGroupList.Add(theRes);
			return true;
		}

		private bool ParseSetDefaults(XMLElement theElement)
		{
			Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, string> current = enumerator.Current;
				if (current.Key == "path")
				{
					mDefaultPath = GetResourceDir() + enumerator.Current.Value + "/";
				}
				if (enumerator.Current.Key == "idprefix")
				{
					mDefaultIdPrefix = enumerator.Current.Value;
				}
			}
			return true;
		}


		private bool ParseFontResource(XMLElement theElement)
		{
			FontRes fontRes = new FontRes();
			if (!ParseCommonResource(theElement, fontRes, mFontMap))
			{
				if (!mHadAlreadyDefinedError || !mAllowAlreadyDefinedResources)
				{
					fontRes.DeleteResource();
					return false;
				}
				mError = "";
				mHasFailed = false;
				FontRes fontRes2 = fontRes;
				fontRes = (FontRes)mFontMap[fontRes2.mId];
				fontRes.mPath = fontRes2.mPath;
				fontRes.mXMLAttributes = fontRes2.mXMLAttributes;
				fontRes2.DeleteResource();
			}
			fontRes.mFont = null;
			foreach (KeyValuePair<string, string> keyValuePair in theElement.mAttributes)
			{
				Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
				if (keyValuePair.Key == "tags")
				{
					FontRes fontRes3 = fontRes;
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					fontRes3.mTags = keyValuePair2.Value;
				}
				KeyValuePair<string, string> keyValuePair3 = enumerator.Current;
				if (keyValuePair3.Key == "isDefault")
				{
					fontRes.mDefault = true;
				}
			}
			if (fontRes.mPath.Substring(0, 5) == "!sys:")
			{
				fontRes.mSysFont = true;
				fontRes.mPath = fontRes.mPath.Substring(5);
				Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
				fontRes.mSize = -1;
				fontRes.mBold = false;
				fontRes.mItalic = false;
				fontRes.mShadow = false;
				fontRes.mUnderline = false;
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, string> keyValuePair4 = enumerator.Current;
					if (keyValuePair4.Key == "size")
					{
						FontRes fontRes4 = fontRes;
						KeyValuePair<string, string> keyValuePair5 = enumerator.Current;
						fontRes4.mSize = Convert.ToInt32(keyValuePair5.Value, 10);
					}
					else
					{
						KeyValuePair<string, string> keyValuePair6 = enumerator.Current;
						if (keyValuePair6.Key == "bold")
						{
							fontRes.mBold = true;
						}
						else
						{
							KeyValuePair<string, string> keyValuePair7 = enumerator.Current;
							if (keyValuePair7.Key == "italic")
							{
								fontRes.mBold = true;
							}
							else
							{
								KeyValuePair<string, string> keyValuePair8 = enumerator.Current;
								if (keyValuePair8.Key == "shadow")
								{
									fontRes.mBold = true;
								}
								else
								{
									KeyValuePair<string, string> keyValuePair9 = enumerator.Current;
									if (keyValuePair9.Key == "underline")
									{
										fontRes.mBold = true;
									}
								}
							}
						}
					}
				}
				if (fontRes.mSize <= 0)
				{
					return Fail("SysFont needs point size");
				}
			}
			else
			{
				fontRes.mSysFont = false;
			}
			return true;
		}

		public bool ParseSoundResource(XMLElement theElement)
		{
			SoundRes soundRes = new SoundRes();
			if (!ParseCommonResource(theElement, soundRes, mSoundMap))
			{
				if (!mHadAlreadyDefinedError || !mAllowAlreadyDefinedResources)
				{
					soundRes.DeleteResource();
					return false;
				}
				mError = "";
				mHasFailed = false;
				SoundRes soundRes2 = soundRes;
				soundRes = (SoundRes)mSoundMap[soundRes2.mId];
				soundRes.mPath = soundRes2.mPath;
				soundRes.mXMLAttributes = soundRes2.mXMLAttributes;
				soundRes2.DeleteResource();
			}
			soundRes.mSoundId = -1;
			soundRes.mVolume = -1.0;
			soundRes.mPanning = 0;
			foreach (KeyValuePair<string, string> keyValuePair in theElement.mAttributes)
			{
				Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
				if (keyValuePair.Key == "volume")
				{
					SoundRes soundRes3 = soundRes;
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					soundRes3.mVolume = Convert.ToDouble(keyValuePair2.Value);
				}
				KeyValuePair<string, string> keyValuePair3 = enumerator.Current;
				if (keyValuePair3.Key == "pan")
				{
					SoundRes soundRes4 = soundRes;
					KeyValuePair<string, string> keyValuePair4 = enumerator.Current;
					soundRes4.mPanning = Convert.ToInt32(keyValuePair4.Value);
				}
			}
			return true;
		}

		public bool ParseMusicResource(XMLElement theElement)
		{
			MusicRes musicRes = new MusicRes();
			if (!ParseCommonResource(theElement, musicRes, mMusicMap))
			{
				if (!mHadAlreadyDefinedError || !mAllowAlreadyDefinedResources)
				{
					musicRes.DeleteResource();
					return false;
				}
				mError = "";
				mHasFailed = false;
				MusicRes musicRes2 = musicRes;
				musicRes = (MusicRes)mMusicMap[musicRes2.mId];
				musicRes.mPath = musicRes2.mPath;
				musicRes.mXMLAttributes = musicRes2.mXMLAttributes;
				musicRes2.DeleteResource();
			}
			musicRes.mSongId = -1;
			return true;
		}

		public bool ParseReanimResource(XMLElement theElement)
		{
			ReanimRes reanimRes = new ReanimRes();
			if (!ParseCommonResource(theElement, reanimRes, mReanimMap))
			{
				if (!mHadAlreadyDefinedError || !mAllowAlreadyDefinedResources)
				{
					reanimRes.DeleteResource();
					return false;
				}
				mError = "";
				mHasFailed = false;
				ReanimRes reanimRes2 = reanimRes;
				reanimRes = (ReanimRes)mReanimMap[reanimRes2.mId];
				reanimRes.mPath = reanimRes2.mPath;
				reanimRes.mXMLAttributes = reanimRes2.mXMLAttributes;
				reanimRes2.DeleteResource();
			}
			return true;
		}

		public bool ParseParticleResource(XMLElement theElement)
		{
			ParticleRes particleRes = new ParticleRes();
			if (!ParseCommonResource(theElement, particleRes, mParticleMap))
			{
				if (!mHadAlreadyDefinedError || !mAllowAlreadyDefinedResources)
				{
					particleRes.DeleteResource();
					return false;
				}
				mError = "";
				mHasFailed = false;
				ParticleRes particleRes2 = particleRes;
				particleRes = (ParticleRes)mParticleMap[particleRes2.mId];
				particleRes.mPath = particleRes2.mPath;
				particleRes.mXMLAttributes = particleRes2.mXMLAttributes;
				particleRes2.DeleteResource();
			}
			return true;
		}

		public bool ParseTrailResource(XMLElement theElement)
		{
			TrailRes trailRes = new TrailRes();
			if (!ParseCommonResource(theElement, trailRes, mTrailMap))
			{
				if (!mHadAlreadyDefinedError || !mAllowAlreadyDefinedResources)
				{
					trailRes.DeleteResource();
					return false;
				}
				mError = "";
				mHasFailed = false;
				TrailRes trailRes2 = trailRes;
				trailRes = (TrailRes)mTrailMap[trailRes2.mId];
				trailRes.mPath = trailRes2.mPath;
				trailRes.mXMLAttributes = trailRes2.mXMLAttributes;
				trailRes2.DeleteResource();
			}
			return true;
		}

		public bool ParseLevelResource(XMLElement theElement)
		{
			LevelRes levelRes = new LevelRes();
			if (!ParseCommonResource(theElement, levelRes, mLevelMap))
			{
				if (!mHadAlreadyDefinedError || !mAllowAlreadyDefinedResources)
				{
					levelRes.DeleteResource();
					return false;
				}
				mError = "";
				mHasFailed = false;
				LevelRes levelRes2 = levelRes;
				levelRes = (LevelRes)mLevelMap[levelRes2.mId];
				levelRes.mPath = levelRes2.mPath;
				levelRes.mXMLAttributes = levelRes2.mXMLAttributes;
				levelRes2.DeleteResource();
			}
			levelRes.mLevelNumber = Convert.ToInt32(levelRes.mId);
			return true;
		}

		public bool ParseImageResource(XMLElement theElement)
		{
			ImageRes imageRes = new ImageRes();
			if (!ParseCommonResource(theElement, imageRes, mImageMap))
			{
				if (!mHadAlreadyDefinedError || !mAllowAlreadyDefinedResources)
				{
					imageRes.DeleteResource();
					return false;
				}
				mError = "";
				mHasFailed = false;
				ImageRes imageRes2 = imageRes;
				imageRes = (ImageRes)mImageMap[imageRes2.mId];
				imageRes.mPath = imageRes2.mPath;
				imageRes.mXMLAttributes = imageRes2.mXMLAttributes;
				imageRes2.DeleteResource();
			}
			imageRes.mPalletize = !theElement.mAttributes.ContainsKey("nopal");
			imageRes.mA4R4G4B4 = theElement.mAttributes.ContainsKey("a4r4g4b4");
			imageRes.mDDSurface = theElement.mAttributes.ContainsKey("ddsurface");
			imageRes.mPurgeBits = (theElement.mAttributes.ContainsKey("nobits") || (mApp.Is3DAccelerated() && theElement.mAttributes.ContainsKey("nobits3d")) || (!mApp.Is3DAccelerated() && theElement.mAttributes.ContainsKey("nobits2d")));
			imageRes.mA8R8G8B8 = theElement.mAttributes.ContainsKey("a8r8g8b8");
			imageRes.mR5G6B5 = theElement.mAttributes.ContainsKey("r5g6b5");
			imageRes.mA1R5G5B5 = theElement.mAttributes.ContainsKey("a1r5g5b5");
			imageRes.mMinimizeSubdivisions = theElement.mAttributes.ContainsKey("minsubdivide");
			imageRes.mAutoFindAlpha = !theElement.mAttributes.ContainsKey("noalpha");
			Dictionary<string, string>.Enumerator enumerator = theElement.mAttributes.GetEnumerator();
			imageRes.mAlphaColor = 16777215U;
			imageRes.mRows = 1;
			imageRes.mCols = 1;
			AnimType animType = AnimType.AnimType_None;
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, string> keyValuePair = enumerator.Current;
				if (keyValuePair.Key == "alphaimage")
				{
					ImageRes imageRes3 = imageRes;
					string text = mDefaultPath;
					KeyValuePair<string, string> keyValuePair2 = enumerator.Current;
					imageRes3.mAlphaImage = text + keyValuePair2.Value;
				}
				else
				{
					KeyValuePair<string, string> keyValuePair3 = enumerator.Current;
					if (keyValuePair3.Key == "alphacolor")
					{
						ImageRes imageRes4 = imageRes;
						KeyValuePair<string, string> keyValuePair4 = enumerator.Current;
						imageRes4.mAlphaColor = Convert.ToUInt32(keyValuePair4.Value);
					}
					else
					{
						KeyValuePair<string, string> keyValuePair5 = enumerator.Current;
						if (keyValuePair5.Key == "variant")
						{
							ImageRes imageRes5 = imageRes;
							KeyValuePair<string, string> keyValuePair6 = enumerator.Current;
							imageRes5.mVariant = keyValuePair6.Value;
						}
						else
						{
							KeyValuePair<string, string> keyValuePair7 = enumerator.Current;
							if (keyValuePair7.Key == "alphagrid")
							{
								ImageRes imageRes6 = imageRes;
								string text2 = mDefaultPath;
								KeyValuePair<string, string> keyValuePair8 = enumerator.Current;
								imageRes6.mAlphaGridImage = text2 + keyValuePair8.Value;
							}
							else
							{
								KeyValuePair<string, string> keyValuePair9 = enumerator.Current;
								if (keyValuePair9.Key == "rows")
								{
									ImageRes imageRes7 = imageRes;
									KeyValuePair<string, string> keyValuePair10 = enumerator.Current;
									imageRes7.mRows = Convert.ToInt32(keyValuePair10.Value);
								}
								else
								{
									KeyValuePair<string, string> keyValuePair11 = enumerator.Current;
									if (keyValuePair11.Key == "cols")
									{
										ImageRes imageRes8 = imageRes;
										KeyValuePair<string, string> keyValuePair12 = enumerator.Current;
										imageRes8.mCols = Convert.ToInt32(keyValuePair12.Value);
									}
									else
									{
										KeyValuePair<string, string> keyValuePair13 = enumerator.Current;
										if (keyValuePair13.Key == "languageSpecific")
										{
											ImageRes imageRes9 = imageRes;
											KeyValuePair<string, string> keyValuePair14 = enumerator.Current;
											imageRes9.mLanguageSpecific = Convert.ToBoolean(keyValuePair14.Value);
										}
										else
										{
											KeyValuePair<string, string> keyValuePair15 = enumerator.Current;
											if (keyValuePair15.Key == "format")
											{
												KeyValuePair<string, string> keyValuePair16 = enumerator.Current;
												string text3 = Convert.ToString(keyValuePair16.Value);
												imageRes.mFormat = (ImageRes.TextureFormat)Enum.Parse(typeof(ImageRes.TextureFormat), text3, true);
											}
											else
											{
												KeyValuePair<string, string> keyValuePair17 = enumerator.Current;
												if (keyValuePair17.Key == "anim")
												{
													KeyValuePair<string, string> keyValuePair18 = enumerator.Current;
													string value = keyValuePair18.Value;
													if (value == "none")
													{
														animType = AnimType.AnimType_None;
													}
													else if (value == "once")
													{
														animType = AnimType.AnimType_Once;
													}
													else if (value == "loop")
													{
														animType = AnimType.AnimType_Loop;
													}
													else
													{
														if (!(value == "pingpong"))
														{
															Fail("Invalid animation type.");
															return false;
														}
														animType = AnimType.AnimType_PingPong;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			imageRes.mAnimInfo.mAnimType = animType;
			if (animType != AnimType.AnimType_None)
			{
				int theNumCels = Math.Max(imageRes.mRows, imageRes.mCols);
				int theBeginFrameTime = 0;
				int theEndFrameTime = 0;
				foreach (KeyValuePair<string, string> keyValuePair19 in theElement.mAttributes)
				{
					if (keyValuePair19.Key == "framedelay")
					{
						AnimInfo animInfo = imageRes.mAnimInfo;
						KeyValuePair<string, string> keyValuePair20 = enumerator.Current;
						animInfo.mFrameDelay = Convert.ToInt32(keyValuePair20.Value);
					}
					else
					{
						KeyValuePair<string, string> keyValuePair21 = enumerator.Current;
						if (keyValuePair21.Key == "begindelay")
						{
							KeyValuePair<string, string> keyValuePair22 = enumerator.Current;
							theBeginFrameTime = Convert.ToInt32(keyValuePair22.Value);
						}
						else
						{
							KeyValuePair<string, string> keyValuePair23 = enumerator.Current;
							if (keyValuePair23.Key == "enddelay")
							{
								KeyValuePair<string, string> keyValuePair24 = enumerator.Current;
								theEndFrameTime = Convert.ToInt32(keyValuePair24.Value);
							}
							else
							{
								KeyValuePair<string, string> keyValuePair25 = enumerator.Current;
								if (keyValuePair25.Key == "perframedelay")
								{
									KeyValuePair<string, string> keyValuePair26 = enumerator.Current;
									ResourceManager.ReadIntVector(keyValuePair26.Value, ref imageRes.mAnimInfo.mPerFrameDelay);
								}
								else
								{
									KeyValuePair<string, string> keyValuePair27 = enumerator.Current;
									if (keyValuePair27.Key == "framemap")
									{
										KeyValuePair<string, string> keyValuePair28 = enumerator.Current;
										ResourceManager.ReadIntVector(keyValuePair28.Value, ref imageRes.mAnimInfo.mFrameMap);
									}
								}
							}
						}
					}
				}
				imageRes.mAnimInfo.Compute(theNumCels, theBeginFrameTime, theEndFrameTime);
			}
			return true;
		}

		private static void ReadIntVector(string theVal, ref List<int> theVector)
		{
			theVector.Clear();
			char[] array = new char[]
			{
				','
			};
			string[] array2 = theVal.Split(array);
			for (int i = 0; i < array2.Length; i++)
			{
				theVector.Add(Convert.ToInt32(array2[i]));
			}
		}

		public void DeleteImage(string theName)
		{
			ReplaceImage(theName, null);
		}

		public void DeleteResources(Dictionary<string, BaseRes> theMap, string theGroup)
		{
			foreach (KeyValuePair<string, BaseRes> keyValuePair in theMap)
			{
				if (theGroup == string.Empty || keyValuePair.Value.mResGroup == theGroup)
				{
					keyValuePair.Value.DeleteResource();
				}
			}
		}

		public void DeleteResources(string theGroup)
		{
			DeleteResources(mImageMap, theGroup);
			DeleteResources(mSoundMap, theGroup);
			DeleteResources(mFontMap, theGroup);
			mLoadedGroups.Remove(theGroup);
			Resources.ExtractResourcesByName(this, theGroup);
		}

		public void UnloadInitResources()
		{
			foreach (BaseRes baseRes in unloadableResources)
			{
				baseRes.DeleteResource();
			}
			unloadableResources.Clear();
		}

		public void UnloadBackground(string theGroup)
		{
			GC.Collect();
			DeleteResources(theGroup);
			mLoadedGroups.Remove(theGroup);
			GC.Collect();
			SexyAppBase.XnaGame.CompensateForSlowUpdate();
		}

		public bool ReplaceImage(string theId, Image theImage)
		{
			return true;
		}

		public Font GetFontThrow(string theRes)
		{
			if (mFontMap.ContainsKey(theRes))
			{
				return ((FontRes)mFontMap[theRes]).mFont;
			}
			return null;
		}

		public ReanimatorDefinition GetReanimThrow(string theRes)
		{
			if (mReanimMap.ContainsKey(theRes))
			{
				return ((ReanimRes)mReanimMap[theRes]).mReanim;
			}
			return null;
		}

		public TodParticleDefinition GetParticleThrow(string theRes)
		{
			if (mParticleMap.ContainsKey(theRes))
			{
				return ((ParticleRes)mParticleMap[theRes]).mParticle;
			}
			return null;
		}

		public TrailDefinition GetTrailThrow(string theRes)
		{
			if (mTrailMap.ContainsKey(theRes))
			{
				return ((TrailRes)mTrailMap[theRes]).mTrail;
			}
			return null;
		}

		public Image GetImageThrow(string theRes)
		{
			if (mImageMap.ContainsKey(theRes))
			{
				return ((ImageRes)mImageMap[theRes]).mImage;
			}
			return null;
		}

		public int GetSoundThrow(string theRes)
		{
			if (mSoundMap.ContainsKey(theRes))
			{
				return ((SoundRes)mSoundMap[theRes]).mSoundId;
			}
			return -1;
		}

		public int GetMusicThrow(string theRes)
		{
			if (mMusicMap.ContainsKey(theRes))
			{
				return ((MusicRes)mMusicMap[theRes]).mSongId;
			}
			return -1;
		}

		public int GetNumResources(string theResGroup, Dictionary<string, BaseRes> theResMap)
		{
			if (string.IsNullOrEmpty(theResGroup))
			{
				return theResMap.Count;
			}
			int num = 0;
			foreach (KeyValuePair<string, BaseRes> keyValuePair in theResMap)
			{
				BaseRes value = keyValuePair.Value;
				if (value.mResGroup == theResGroup && !value.mFromProgram)
				{
					num++;
				}
			}
			return num;
		}

		public int GetNumResourcesGroupNameStartsWith(string theResGroup, Dictionary<string, BaseRes> theResMap)
		{
			if (string.IsNullOrEmpty(theResGroup))
			{
				return theResMap.Count;
			}
			int num = 0;
			foreach (KeyValuePair<string, BaseRes> keyValuePair in theResMap)
			{
				BaseRes value = keyValuePair.Value;
				if (value.mResGroup.StartsWith(theResGroup))
				{
					num++;
				}
			}
			return num;
		}

		public int GetNumResourcesGroupNameStartsWith(string theResGroup)
		{
			return GetNumResourcesGroupNameStartsWith(theResGroup, mImageMap);
		}

		public int GetNumResources(string theResGroup)
		{
			return GetNumImages(theResGroup) + GetNumSounds(theResGroup) + GetNumFonts(theResGroup) + GetNumSongs(theResGroup);
		}

		public int GetNumImages(string theResGroup)
		{
			return GetNumResources(theResGroup, mImageMap);
		}

		public int GetNumSounds(string theResGroup)
		{
			return GetNumResources(theResGroup, mSoundMap);
		}

		public int GetNumFonts(string theResGroup)
		{
			return GetNumResources(theResGroup, mFontMap);
		}

		public int GetNumSongs(string theResGroup)
		{
			return GetNumResources(theResGroup, mMusicMap);
		}

		public int GetNumReanims(string theResGroup)
		{
			return GetNumResources(theResGroup, mReanimMap);
		}

		public int GetNumParticles(string theResGroup)
		{
			return GetNumResources(theResGroup, mParticleMap);
		}

		public int GetNumTrails(string theResGroup)
		{
			return GetNumResources(theResGroup, mTrailMap);
		}

		public void LoadAllResources()
		{
			mTotalResources = GetNumResources("");
			mTotalResources -= GetNumResourcesGroupNameStartsWith("DelayLoad_");
			mLoadedCount = 0;
			mProgress = 0.0;
			Dictionary<string, List<BaseRes>>.Enumerator enumerator = mResGroupMap.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, List<BaseRes>> current = enumerator.Current;
				if ((current.Key != "Levels") && !enumerator.Current.Key.StartsWith("DelayLoad_"))
				{
					StartLoadResources(enumerator.Current.Key);
					LoadResources(enumerator.Current.Key);
					if (HadError())
					{
						return;
					}
				}
			}
		}



		public void StartLoadResources(string theResGroup)
		{
			mError = "";
			mHasFailed = false;
			mCurResGroup = theResGroup;
			mCurResGroupList = mResGroupMap[theResGroup];
			mCurResGroupListItr = mCurResGroupList.GetEnumerator();
		}

		public bool LoadResources(string theResGroup)
		{
			mError = "";
			mHasFailed = false;
			StartLoadResources(theResGroup);
			while (LoadNextResource())
			{
			}
			if (!HadError())
			{
				mLoadedGroups.Add(theResGroup);
				return true;
			}
			return false;
		}

		public bool LoadNextResource()
		{
			if (HadError())
			{
				return false;
			}
			if (mCurResGroupList == null)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			while (mCurResGroupListItr.MoveNext())
			{
				BaseRes baseRes = mCurResGroupListItr.Current;
				if (!baseRes.mFromProgram)
				{
					switch (baseRes.mType)
					{
					case ResType.ResType_Image:
					{
						ImageRes imageRes = (ImageRes)baseRes;
						if (imageRes.mImage != null)
						{
							continue;
						}
						flag = DoLoadImage(imageRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Sound:
					{
						SoundRes soundRes = (SoundRes)baseRes;
						if (soundRes.mSoundId != -1)
						{
							continue;
						}
						flag = DoLoadSound(soundRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Font:
					{
						FontRes fontRes = (FontRes)baseRes;
						if (fontRes.mFont != null)
						{
							continue;
						}
						flag = DoLoadFont(fontRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Music:
					{
						MusicRes musicRes = (MusicRes)baseRes;
						if (musicRes.mSongId != -1)
						{
							continue;
						}
						flag = DoLoadMusic(musicRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Reanim:
					{
						ReanimRes reanimRes = (ReanimRes)baseRes;
						if (reanimRes.mReanim != null)
						{
							continue;
						}
						flag = DoLoadReanim(ref reanimRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Particle:
					{
						ParticleRes particleRes = (ParticleRes)baseRes;
						if (particleRes.mParticle != null)
						{
							continue;
						}
						flag = DoLoadParticle(ref particleRes);
						flag2 = true;
						break;
					}
					case ResType.ResType_Trail:
					{
						TrailRes trailRes = (TrailRes)baseRes;
						if (trailRes.mTrail != null)
						{
							continue;
						}
						flag = DoLoadTrail(ref trailRes);
						flag2 = true;
						break;
					}
					}
					if (flag2)
					{
						break;
					}
				}
			}
			if (flag)
			{
				mLoadedCount++;
				mProgress = mLoadedCount / (double)mTotalResources;
				Debug.OutputDebug<string>(mProgress.ToString());
			}
			return flag;
		}

		public bool LoadLevelBackgrounds(int levelNumber, out Image verticalBackground, out Image horizontalBackground)
		{
			Dictionary<string, BaseRes>.Enumerator enumerator = mLevelMap.GetEnumerator();
			if (loadedBackdrop != levelNumber)
			{
				backDropContentManager.Unload();
			}
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, BaseRes> keyValuePair = enumerator.Current;
				LevelRes levelRes = (LevelRes)keyValuePair.Value;
				if (levelRes.mLevelNumber == levelNumber)
				{
					verticalBackground = new Image(backDropContentManager.Load<Texture2D>(levelRes.mPath + "v"));
					horizontalBackground = new Image(backDropContentManager.Load<Texture2D>(levelRes.mPath + "h"));
					loadedBackdrop = levelNumber;
					return true;
				}
			}
			verticalBackground = null;
			horizontalBackground = null;
			return false;
		}

		private Texture2D LoadTextureFromStream(string filename, bool premultiply, ImageRes.TextureFormat format, SurfaceFormat lowMemorySurfaceFormat)
		{
			Texture2D texture2D = null;
			GraphicsDevice graphicsDevice = GlobalStaticVars.g.GraphicsDevice;
			using (Stream stream = TitleContainer.OpenStream("Content/" + filename + "." + format.ToString()))
			{
				texture2D = Texture2D.FromStream(graphicsDevice, stream);
			}
			bool flag = false;
			if (!premultiply)
			{
				return texture2D;
			}
			lock (ResourceManager.DrawLocker)
			{
				if (texture2D.Width * texture2D.Height < 4194304)
				{
					RenderTarget2D renderTarget2D = null;
					try
					{
						flag = true;
						lock (SexyAppBase.SplashScreenDrawLock)
						{
							if (Main.DO_LOW_MEMORY_OPTIONS)
							{
								renderTarget2D = new RenderTarget2D(graphicsDevice, texture2D.Width, texture2D.Height, false, lowMemorySurfaceFormat, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
							}
							else
							{
								renderTarget2D = new RenderTarget2D(graphicsDevice, texture2D.Width, texture2D.Height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
							}
							graphicsDevice.SetRenderTarget(renderTarget2D);
							graphicsDevice.Clear(Color.Black);
							ResourceManager.imageLoadSpritebatch.Begin(SpriteSortMode.Immediate, BlendStates.blendColorLoadState);
							ResourceManager.imageLoadSpritebatch.Draw(texture2D, texture2D.Bounds, Color.White);
							ResourceManager.imageLoadSpritebatch.End();
							ResourceManager.imageLoadSpritebatch.Begin(SpriteSortMode.Immediate, BlendStates.imageLoadBlendAlpha);
							ResourceManager.imageLoadSpritebatch.Draw(texture2D, texture2D.Bounds, Color.White);
							ResourceManager.imageLoadSpritebatch.End();
							graphicsDevice.SetRenderTarget(null);
							texture2D.Dispose();
							return renderTarget2D;
						}
					}
					catch (Exception ex)
					{
						flag = false;
						string message = ex.Message;
						if (renderTarget2D != null)
						{
							renderTarget2D.Dispose();
						}
					}
				}
			}
			if (!flag)
			{
				Color[] array = new Color[texture2D.Width * texture2D.Height];
				texture2D.GetData<Color>(array);
				for (int i = 0; i < array.Length; i++)
				{
					PremultiplyPixel(ref array[i]);
				}
				if (Main.DO_LOW_MEMORY_OPTIONS)
				{
					Texture2D texture2D2 = new Texture2D(graphicsDevice, texture2D.Width, texture2D.Height, false, SurfaceFormat.Bgra4444);
					Bgra4444[] array2 = new Bgra4444[array.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = new Bgra4444(array[j].ToVector4());
					}
					texture2D2.SetData<Bgra4444>(array2);
					texture2D.Dispose();
					texture2D = texture2D2;
				}
				else
				{
					texture2D.SetData<Color>(array);
				}
			}
			return texture2D;
		}

		private void PremultiplyPixel(ref Color c)
		{
			c.R = (byte)(c.R * c.A / 255f);
			c.G = (byte)(c.G * c.A / 255f);
			c.B = (byte)(c.B * c.A / 255f);
		}

		private bool DoLoadImage(ImageRes theRes)
		{
			Texture2D texture2D;
			try
			{
				string text;
				if (theRes.mLanguageSpecific)
				{
					text = string.Concat(new string[]
					{
						Path.GetDirectoryName(theRes.mPath),
						Constants.ImageSubPath,
						Constants.LanguageSubDir,
						"/",
						Path.GetFileName(theRes.mPath)
					});
				}
				else
				{
					text = Path.GetDirectoryName(theRes.mPath) + Constants.ImageSubPath + Path.GetFileName(theRes.mPath);
				}
				if (theRes.mFormat == ImageRes.TextureFormat.Content)
				{
					texture2D = GetContentManager(theRes).Load<Texture2D>(text);
				}
				else
				{
					texture2D = LoadTextureFromStream(text, true, theRes.mFormat, theRes.lowMemorySurfaceFormat);
				}
			}
			catch (Exception ex)
			{
				return Fail("Failed to load image: " + theRes.mPath + ex.Message);
			}
			theRes.mImage = new Image(texture2D, 0, 0, texture2D.Width, texture2D.Height);
			if (theRes.mAnimInfo.mAnimType != AnimType.AnimType_None)
			{
				theRes.mImage.mAnimInfo = new AnimInfo(theRes.mAnimInfo);
			}
			theRes.mImage.mNumRows = theRes.mRows;
			theRes.mImage.mNumCols = theRes.mCols;
			if (theRes.mUnloadGroup > 0)
			{
				unloadableResources.Add(theRes);
			}
			ResourceLoadedHook(theRes);
			return true;
		}

		private bool DoLoadFont(FontRes fontRes)
		{
			Font font = new Font();
			if (fontRes.mSysFont)
			{
				return Fail("SysFont not supported");
			}
			if (fontRes.mDefault)
			{
				font.AddLayer(arial);
				fontRes.mFont = font;
				return true;
			}
			XmlReader xmlReader = XmlReader.Create(TitleContainer.OpenStream(fontRes.mPath));
			xmlReader.Read();
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element)
				{
					if (xmlReader.Name == "Offsets")
					{
						Vector2 zero = Vector2.Zero;
						while (xmlReader.NodeType != XmlNodeType.EndElement || !(xmlReader.Name == "Offsets"))
						{
							if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Offset")
							{
								string text = xmlReader["offsetX"];
								string text2 = xmlReader["offsetY"];
								char c = xmlReader["character"].ToCharArray()[0];
								if (!string.IsNullOrEmpty(text))
								{
									zero.X = Convert.ToInt32(text);
								}
								if (!string.IsNullOrEmpty(text2))
								{
									zero.Y = Convert.ToInt32(text2);
								}
								font.AddCharacterOffset(c, zero);
							}
							xmlReader.Read();
						}
					}
					if (xmlReader.Name == "Layers")
					{
						string text3 = xmlReader["magic"];
						if (!string.IsNullOrEmpty(text3))
						{
							font.characterOffsetMagic = Convert.ToInt32(text3);
						}
						else
						{
							font.characterOffsetMagic = 0;
						}
						Vector2 zero2 = Vector2.Zero;
						while (xmlReader.NodeType != XmlNodeType.EndElement || !(xmlReader.Name == "Layers"))
						{
							if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Layer")
							{
								if (!xmlReader.HasAttributes)
								{
									zero2 = Vector2.Zero;
								}
								else
								{
									string text4 = xmlReader["xOffset"];
									string text5 = xmlReader["yOffset"];
									if (!string.IsNullOrEmpty(text4))
									{
										zero2.X = Convert.ToInt32(text4);
									}
									if (!string.IsNullOrEmpty(text5))
									{
										zero2.Y = Convert.ToInt32(text5);
									}
								}
							}
							if (xmlReader.NodeType == XmlNodeType.Text)
							{
								string value = xmlReader.Value;
								font.AddLayer(mContentManager.Load<SpriteFont>(value), zero2);
							}
							xmlReader.Read();
						}
					}
					if (xmlReader.Name == "Ascent")
					{
						xmlReader.Read();
						font.mAscent = -1 * Convert.ToInt32(xmlReader.Value);
					}
					if (xmlReader.Name == "Height")
					{
						xmlReader.Read();
						font.mHeight = -1 * Convert.ToInt32(xmlReader.Value);
					}
					if (xmlReader.Name == "SpaceChar")
					{
						xmlReader.Read();
						font.SpaceChar = xmlReader.Value;
					}
					if (xmlReader.Name == "StringWidthCachingEnabled")
					{
						xmlReader.Read();
						font.StringWidthCachingEnabled = Convert.ToBoolean(xmlReader.Value);
					}
				}
			}
			fontRes.mFont = font;
			ResourceLoadedHook(fontRes);
			return true;
		}

		private bool DoLoadSound(SoundRes soundRes)
		{
			int freeSoundId = mApp.mSoundManager.GetFreeSoundId();
			if (freeSoundId < 0)
			{
				return Fail("Out of free sound ids");
			}
			if (!mApp.mSoundManager.LoadSound((uint)freeSoundId, soundRes.mPath))
			{
				return Fail("Failed to load sound: " + soundRes.mPath);
			}
			if (soundRes.mVolume >= 0.0)
			{
				mApp.mSoundManager.SetBaseVolume((uint)freeSoundId, soundRes.mVolume);
			}
			if (soundRes.mPanning != 0)
			{
				mApp.mSoundManager.SetBasePan((uint)freeSoundId, soundRes.mPanning);
			}
			soundRes.mSoundId = freeSoundId;
			ResourceLoadedHook(soundRes);
			return true;
		}

		private bool DoLoadMusic(MusicRes musicRes)
		{
			int freeMusicId = mApp.mMusicInterface.GetFreeMusicId();
			if (freeMusicId < 0)
			{
				return Fail("Out of free song ids");
			}
			if (!mApp.mMusicInterface.LoadMusic(freeMusicId, musicRes.mPath))
			{
				return Fail("Failed to load song: " + musicRes.mPath);
			}
			musicRes.mSongId = freeMusicId;
			ResourceLoadedHook(musicRes);
			return true;
		}

		private bool DoLoadReanim(ref ReanimRes reanimRes)
		{
			ReanimRes reanimRes2 = reanimRes;
			if (!LoadReanimation(reanimRes2.mPath, ref reanimRes2.mReanim))
			{
				return Fail("Failed to load reanim: " + reanimRes2.mPath);
			}
			ResourceLoadedHook(reanimRes);
			return true;
		}

		private bool DoLoadParticle(ref ParticleRes particleRes)
		{
			ParticleRes particleRes2 = particleRes;
			if (!LoadParticle(particleRes2.mPath, ref particleRes2.mParticle))
			{
				return Fail("Failed to load reanim: " + particleRes2.mPath);
			}
			ResourceLoadedHook(particleRes);
			return true;
		}

		private bool DoLoadTrail(ref TrailRes trailRes)
		{
			TrailRes trailRes2 = trailRes;
			if (!LoadTrail(trailRes2.mPath, ref trailRes2.mTrail))
			{
				return Fail("Failed to load reanim: " + trailRes2.mPath);
			}
			ResourceLoadedHook(trailRes);
			return true;
		}

		private void ResourceLoadedHook(BaseRes theRes)
		{
		}

		private bool Fail(string theErrorText)
		{
			if (!mHasFailed)
			{
				mHasFailed = true;
				if (mXMLParser == null)
				{
					mError = theErrorText;
					return false;
				}
				int currentLineNum = mXMLParser.GetCurrentLineNum();
				mError = theErrorText;
				if (currentLineNum > 0)
				{
					mError = mError + " on Line " + currentLineNum.ToString();
				}
				if (!string.IsNullOrEmpty(mXMLParser.GetFileName()))
				{
					mError = mError + " in File '" + mXMLParser.GetFileName() + "'";
				}
			}
			return false;
		}

		public bool TodLoadResources(string theGroup)
		{
			return TodLoadResources(theGroup, false);
		}

		public bool TodLoadResources(string theGroup, bool doUnpackAtlasImages)
		{
			if (IsGroupLoaded(theGroup))
			{
				return true;
			}
			PerfTimer perfTimer = default(PerfTimer);
			perfTimer.Start();
			StartLoadResources(theGroup);
			while (!GlobalStaticVars.gSexyAppBase.mShutdown && LoadNextResource())
			{
			}
			if (GlobalStaticVars.gSexyAppBase.mShutdown)
			{
				return false;
			}
			if (HadError())
			{
				GlobalStaticVars.gSexyAppBase.ShowResourceError(true);
				return false;
			}
			mLoadedGroups.Add(theGroup);
			Math.Max((int)perfTimer.GetDuration(), 0);
			return true;
		}

		private const string DELAYED_GROUP_NAME = "DelayLoad_";

		public double mProgress;

		public int mTotalResources;

		public int mLoadedCount;

		protected Dictionary<string, BaseRes> mImageMap;

		protected Dictionary<string, BaseRes> mSoundMap;

		protected Dictionary<string, BaseRes> mFontMap;

		protected Dictionary<string, BaseRes> mMusicMap;

		protected Dictionary<string, BaseRes> mReanimMap;

		protected Dictionary<string, BaseRes> mParticleMap;

		protected Dictionary<string, BaseRes> mTrailMap;

		protected Dictionary<string, BaseRes> mLevelMap;

		protected string mError;

		protected bool mHasFailed;

		protected SexyAppBase mApp;

		protected string mCurResGroup;

		protected string mDefaultPath;

		protected string mDefaultIdPrefix;

		protected XMLParser mXMLParser;

		protected bool mAllowMissingProgramResources;

		protected bool mAllowAlreadyDefinedResources;

		protected bool mHadAlreadyDefinedError;

		protected Dictionary<string, List<BaseRes>> mResGroupMap;

		protected List<BaseRes> mCurResGroupList;

		protected List<BaseRes>.Enumerator mCurResGroupListItr;

		protected List<string> mLoadedGroups;

		protected ContentManager mContentManager;

		public static ContentManager mReanimContentManager;

		public static ContentManager mParticleContentManager;

		public static ContentManager[] mUnloadContentManager = new ContentManager[5];

		private ContentManager mBackgroundContentmanager;

		private SpriteFont arial;

		public static object DrawLocker = new object();

		private static SpriteBatch imageLoadSpritebatch = new SpriteBatch(GlobalStaticVars.g.GraphicsDevice);

		private ContentManager backDropContentManager;

		private int loadedBackdrop = -1;

		private List<BaseRes> unloadableResources = new List<BaseRes>();
	}
}
