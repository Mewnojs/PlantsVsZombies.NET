using System;
using System.Collections.Generic;
using Sexy.Misc;
using Sexy.Resource;

namespace Sexy.GraphicsLib
{
	public class FontData : DescParser
	{
		public override bool Error(string theError)
		{
			return false;
		}

		public bool GetColorFromDataElement(DataElement theElement, ref SexyColor theColor)
		{
			if (theElement.mIsList)
			{
				List<double> list = new List<double>();
				if (!base.DataToDoubleVector(theElement, ref list) && list.Count == 4)
				{
					return false;
				}
				theColor = new SexyColor((int)(list[0] * 255.0), (int)(list[1] * 255.0), (int)(list[2] * 255.0), (int)(list[3] * 255.0));
				return true;
			}
			else
			{
				int theColor2 = 0;
				if (!Common.StringToInt(((SingleDataElement)theElement).mString.ToString(), ref theColor2))
				{
					return false;
				}
				theColor = new SexyColor(theColor2);
				return true;
			}
		}

		public bool DataToLayer(DataElement theSource, ref FontLayer theFontLayer)
		{
			theFontLayer = null;
			if (theSource.mIsList)
			{
				return false;
			}
			string text = ((SingleDataElement)theSource).mString.ToString().ToUpper();
			if (!this.mFontLayerMap.ContainsKey(text))
			{
				return false;
			}
			theFontLayer = this.mFontLayerMap[text];
			return true;
		}

		public override bool HandleCommand(ListDataElement theParams)
		{
			string text = ((SingleDataElement)theParams.mElementVector[0]).mString.ToString();
			if (text[0] == '\ufeff')
			{
				text = text.Substring(1);
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (text == "Define")
			{
				if (theParams.mElementVector.Count == 3)
				{
					if (!theParams.mElementVector[1].mIsList)
					{
						string text2 = ((SingleDataElement)theParams.mElementVector[1]).mString.ToString().ToUpper();
						if (!base.IsImmediate(text2))
						{
							if (this.mDefineMap.ContainsKey(text2))
							{
								this.mDefineMap.Remove(text2);
							}
							if (theParams.mElementVector[2].mIsList)
							{
								ListDataElement listDataElement = new ListDataElement();
								if (!base.GetValues((ListDataElement)theParams.mElementVector[2], listDataElement))
								{
									if (listDataElement != null)
									{
										listDataElement.Dispose();
									}
									return false;
								}
								this.mDefineMap.Add(text2, listDataElement);
							}
							else
							{
								SingleDataElement singleDataElement = (SingleDataElement)theParams.mElementVector[2];
								DataElement dataElement = this.Dereference(singleDataElement.mString.ToString());
								if (dataElement != null)
								{
									this.mDefineMap.Add(text2, dataElement.Duplicate());
								}
								else
								{
									this.mDefineMap.Add(text2, singleDataElement.Duplicate());
								}
							}
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "CreateHorzSpanRectList")
			{
				if (theParams.mElementVector.Count == 4)
				{
					List<int> list = new List<int>();
					List<int> list2 = new List<int>();
					if (!theParams.mElementVector[1].mIsList && base.DataToIntVector(theParams.mElementVector[2], ref list) && list.Count == 4 && base.DataToIntVector(theParams.mElementVector[3], ref list2))
					{
						string text3 = ((SingleDataElement)theParams.mElementVector[1]).mString.ToString().ToUpper();
						int num = 0;
						ListDataElement listDataElement2 = new ListDataElement();
						for (int i = 0; i < list2.Count; i++)
						{
							ListDataElement listDataElement3 = new ListDataElement();
							listDataElement2.mElementVector.Add(listDataElement3);
							string theString = (list[0] + num).ToString();
							listDataElement3.mElementVector.Add(new SingleDataElement(theString));
							theString = list[1].ToString();
							listDataElement3.mElementVector.Add(new SingleDataElement(theString));
							theString = list2[i].ToString();
							listDataElement3.mElementVector.Add(new SingleDataElement(theString));
							theString = list[3].ToString();
							listDataElement3.mElementVector.Add(new SingleDataElement(theString));
							num += list2[i];
						}
						if (this.mDefineMap.ContainsKey(text3))
						{
							this.mDefineMap.Remove(text3);
						}
						this.mDefineMap.Add(text3, listDataElement2);
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "SetDefaultPointSize")
			{
				if (theParams.mElementVector.Count == 2)
				{
					int num2 = 0;
					if (!theParams.mElementVector[1].mIsList && Common.StringToInt(((SingleDataElement)theParams.mElementVector[1]).mString.ToString(), ref num2))
					{
						this.mDefaultPointSize = num2;
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "SetCharMap")
			{
				if (theParams.mElementVector.Count == 3)
				{
					List<string> list3 = new List<string>();
					List<string> list4 = new List<string>();
					if (base.DataToStringVector(theParams.mElementVector[1], ref list3) && base.DataToStringVector(theParams.mElementVector[2], ref list4))
					{
						if (list3.Count == list4.Count)
						{
							for (int j = 0; j < list3.Count; j++)
							{
								if (list3[j].Length == 1 && list4[j].Length == 1)
								{
									this.mCharMap.Add(list3[j][0], list4[j][0]);
								}
								else
								{
									flag2 = true;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "CreateLayer")
			{
				if (theParams.mElementVector.Count == 2)
				{
					if (!theParams.mElementVector[1].mIsList)
					{
						string text4 = ((SingleDataElement)theParams.mElementVector[1]).mString.ToString().ToUpper();
						this.mFontLayerList.AddLast(new FontLayer(this));
						FontLayer value = this.mFontLayerList.Last.Value;
						value.mLayerName = text4;
						value.mBaseOrder = this.mFontLayerList.Count - 1;
						this.mFontLayerMap.Add(text4, value);
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "CreateLayerFrom")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer theFontLayer = new FontLayer();
					if (!theParams.mElementVector[1].mIsList && this.DataToLayer(theParams.mElementVector[2], ref theFontLayer))
					{
						string text5 = ((SingleDataElement)theParams.mElementVector[1]).mString.ToString().ToUpper();
						this.mFontLayerList.AddLast(new FontLayer(theFontLayer));
						FontLayer value2 = this.mFontLayerList.Last.Value;
						value2.mLayerName = text5;
						value2.mBaseOrder = this.mFontLayerList.Count - 1;
						this.mFontLayerMap.Add(text5, value2);
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerRequireTags")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer = null;
					List<string> list5 = new List<string>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer) && base.DataToStringVector(theParams.mElementVector[2], ref list5))
					{
						for (int k = 0; k < list5.Count; k++)
						{
							fontLayer.mRequiredTags.Add(list5[k].ToUpper());
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerExcludeTags")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer2 = null;
					List<string> list6 = new List<string>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer2) && base.DataToStringVector(theParams.mElementVector[2], ref list6))
					{
						for (int l = 0; l < list6.Count; l++)
						{
							fontLayer2.mExcludedTags.Add(list6[l].ToUpper());
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerPointRange")
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer3 = null;
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer3) && !theParams.mElementVector[2].mIsList && !theParams.mElementVector[3].mIsList)
					{
						int mMinPointSize = 0;
						int mMaxPointSize = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mMinPointSize) && Common.StringToInt(((SingleDataElement)theParams.mElementVector[3]).mString.ToString(), ref mMaxPointSize))
						{
							fontLayer3.mMinPointSize = mMinPointSize;
							fontLayer3.mMaxPointSize = mMaxPointSize;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerSetPointSize")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer4 = null;
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer4) && !theParams.mElementVector[2].mIsList)
					{
						int mPointSize = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mPointSize))
						{
							fontLayer4.mPointSize = mPointSize;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerSetHeight")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer5 = null;
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer5) && !theParams.mElementVector[2].mIsList)
					{
						int mHeight = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mHeight))
						{
							fontLayer5.mHeight = mHeight;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text == "LayerSetImage")
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer6 = null;
					string theRelPath = "";
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer6) && base.DataToString(theParams.mElementVector[2], ref theRelPath))
					{
						string pathFrom = Common.GetPathFrom(theRelPath, Common.GetFileDir(this.mSourceFile, false));
						bool mWriteToSexyCache = false;
						if (GlobalMembers.gSexyAppBase != null)
						{
							mWriteToSexyCache = GlobalMembers.gSexyAppBase.mWriteToSexyCache;
							GlobalMembers.gSexyAppBase.mWriteToSexyCache = false;
						}
						bool flag5 = false;
						bool flag6 = false;
						SharedImageRef sharedImageRef = new SharedImageRef();
						if (GlobalMembers.gSexyAppBase != null && GlobalMembers.gSexyAppBase.mResourceManager != null && string.IsNullOrEmpty(this.mImagePathPrefix))
						{
							string idByPath = GlobalMembers.gSexyAppBase.mResourceManager.GetIdByPath(pathFrom);
							if (!string.IsNullOrEmpty(idByPath))
							{
								sharedImageRef = GlobalMembers.gSexyAppBase.mResourceManager.GetImage(idByPath);
								if (sharedImageRef.GetDeviceImage() == null)
								{
									sharedImageRef = GlobalMembers.gSexyAppBase.mResourceManager.LoadImage(idByPath);
								}
								if (sharedImageRef.GetDeviceImage() != null)
								{
									flag6 = true;
								}
							}
						}
						if (GlobalMembers.gSexyAppBase != null && !flag6)
						{
							sharedImageRef = GlobalMembers.gSexyAppBase.GetSharedImage(this.mImagePathPrefix + pathFrom, "", ref flag5, false, false);
						}
						fontLayer6.mImageFileName = pathFrom;
						if (GlobalMembers.gSexyAppBase != null)
						{
							GlobalMembers.gSexyAppBase.mWriteToSexyCache = mWriteToSexyCache;
						}
						if (sharedImageRef.GetImage() == null)
						{
							this.Error("Failed to Load Image");
							return false;
						}
						if (!flag5 && sharedImageRef.GetMemoryImage().mColorTable != null)
						{
							fontLayer6.mImageIsWhite = true;
							for (int m = 0; m < 256; m++)
							{
								if ((sharedImageRef.GetMemoryImage().mColorTable[m] & 16777215U) != 16777215U && sharedImageRef.GetMemoryImage().mColorTable[m] != 0U)
								{
									fontLayer6.mImageIsWhite = false;
									break;
								}
							}
						}
						fontLayer6.mImage = new SharedImageRef(sharedImageRef);
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetDrawMode"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer7 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer7) && !theParams.mElementVector[2].mIsList)
					{
						int num3 = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref num3) && num3 >= 0 && num3 <= 1)
						{
							fontLayer7.mDrawMode = num3;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetColorMult"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer8 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer8))
					{
						if (!this.GetColorFromDataElement(theParams.mElementVector[2], ref fontLayer8.mColorMult))
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetColorAdd"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer9 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer9))
					{
						if (!this.GetColorFromDataElement(theParams.mElementVector[2], ref fontLayer9.mColorAdd))
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetAscent"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer10 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer10) && !theParams.mElementVector[2].mIsList)
					{
						int mAscent = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mAscent))
						{
							fontLayer10.mAscent = mAscent;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetAscentPadding"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer11 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer11) && !theParams.mElementVector[2].mIsList)
					{
						int mAscentPadding = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mAscentPadding))
						{
							fontLayer11.mAscentPadding = mAscentPadding;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetLineSpacingOffset"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer12 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer12) && !theParams.mElementVector[2].mIsList)
					{
						int mLineSpacingOffset = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mLineSpacingOffset))
						{
							fontLayer12.mLineSpacingOffset = mLineSpacingOffset;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetOffset"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer13 = new FontLayer();
					List<int> list7 = new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer13) && base.DataToIntVector(theParams.mElementVector[2], ref list7) && list7.Count == 2)
					{
						fontLayer13.mOffset.mX = list7[0];
						fontLayer13.mOffset.mY = list7[1];
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetCharWidths"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer14 = new FontLayer();
					List<string> list8 = new List<string>();
					List<int> list9 = new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer14) && base.DataToStringVector(theParams.mElementVector[2], ref list8) && base.DataToIntVector(theParams.mElementVector[3], ref list9))
					{
						if (list8.Count == list9.Count)
						{
							for (int n = 0; n < list8.Count; n++)
							{
								if (list8[n].Length == 1)
								{
									fontLayer14.GetCharData(list8[n][0]).mWidth = list9[n];
								}
								else
								{
									flag2 = true;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetSpacing"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer15 = new FontLayer();
					new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer15) && !theParams.mElementVector[2].mIsList)
					{
						int mSpacing = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mSpacing))
						{
							fontLayer15.mSpacing = mSpacing;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetImageMap"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer16 = null;
					List<string> list10 = new List<string>();
					ListDataElement listDataElement4 = new ListDataElement();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer16) && base.DataToStringVector(theParams.mElementVector[2], ref list10) && base.DataToList(theParams.mElementVector[3], ref listDataElement4))
					{
						if (list10.Count == listDataElement4.mElementVector.Count)
						{
							for (int num4 = 0; num4 < list10.Count; num4++)
							{
								List<int> list11 = new List<int>();
								if (list10[num4].Length == 1 && base.DataToIntVector(listDataElement4.mElementVector[num4], ref list11) && list11.Count == 4)
								{
									Rect mImageRect = new Rect(list11[0], list11[1], list11[2], list11[3]);
									fontLayer16.GetCharData(list10[num4][0]).mImageRect = mImageRect;
								}
								else
								{
									flag2 = true;
								}
							}
							fontLayer16.mDefaultHeight = 0;
							int num5 = fontLayer16.mCharDataHashTable.CharCount();
							CharData[] array = fontLayer16.mCharDataHashTable.ToArray();
							for (int num6 = 0; num6 < num5; num6++)
							{
								if (array[num6].mImageRect.mHeight + array[num6].mOffset.mY > fontLayer16.mDefaultHeight)
								{
									fontLayer16.mDefaultHeight = array[num6].mImageRect.mHeight + array[num6].mOffset.mY;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer17 = null;
					List<string> list12 = new List<string>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer17) && base.DataToStringVector(theParams.mElementVector[2], ref list12))
					{
						for (int num7 = 0; num7 < list12.Count; num7++)
						{
							new List<int>();
							if (list12[num7].Length == 1)
							{
								fontLayer17.GetCharData(list12[num7][0]).mImageRect = new Rect(0, 0, 0, 0);
							}
							else
							{
								flag2 = true;
							}
						}
						fontLayer17.mDefaultHeight = 0;
						int num8 = fontLayer17.mCharDataHashTable.CharCount();
						CharData[] array2 = fontLayer17.mCharDataHashTable.ToArray();
						for (int num9 = 0; num9 < num8; num9++)
						{
							if (array2[num9].mImageRect.mHeight + array2[num9].mOffset.mY > fontLayer17.mDefaultHeight)
							{
								fontLayer17.mDefaultHeight = array2[num9].mImageRect.mHeight + array2[num9].mOffset.mY;
							}
						}
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetCharOffsets"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer18 = new FontLayer();
					List<string> list13 = new List<string>();
					ListDataElement listDataElement5 = new ListDataElement();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer18) && base.DataToStringVector(theParams.mElementVector[2], ref list13) && base.DataToList(theParams.mElementVector[3], ref listDataElement5))
					{
						if (list13.Count == listDataElement5.mElementVector.Count)
						{
							for (int num10 = 0; num10 < list13.Count; num10++)
							{
								List<int> list14 = new List<int>();
								if (list13[num10].Length == 1 && base.DataToIntVector(listDataElement5.mElementVector[num10], ref list14) && list14.Count == 2)
								{
									fontLayer18.GetCharData(list13[num10][0]).mOffset = new SexyPoint(list14[0], list14[1]);
								}
								else
								{
									flag2 = true;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer19 = null;
					List<string> list15 = new List<string>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer19) && base.DataToStringVector(theParams.mElementVector[2], ref list15))
					{
						for (int num11 = 0; num11 < list15.Count; num11++)
						{
							new List<int>();
							fontLayer19.GetCharData(list15[num11][0]).mOffset = new SexyPoint(0, 0);
						}
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetKerningPairs"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer20 = new FontLayer();
					List<string> list16 = new List<string>();
					List<int> list17 = new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer20) && base.DataToStringVector(theParams.mElementVector[2], ref list16) && base.DataToIntVector(theParams.mElementVector[3], ref list17))
					{
						if (list16.Count == list17.Count)
						{
							List<SortedKern> list18 = new List<SortedKern>();
							for (int num12 = 0; num12 < list16.Count; num12++)
							{
								if (list16[num12].Length == 2)
								{
									list18.Add(new SortedKern(list16[num12][0], list16[num12][1], list17[num12]));
								}
								else
								{
									flag2 = true;
								}
							}
							if (list18.Count != 0)
							{
								list18.Sort();
							}
							fontLayer20.mKerningData.Clear();
							for (int num13 = 0; num13 < list18.Count; num13++)
							{
								SortedKern sortedKern = list18[num13];
								FontLayer.KerningValue kerningValue = default(FontLayer.KerningValue);
								kerningValue.mChar = (ushort)sortedKern.mValue;
								kerningValue.mOffset = (short)sortedKern.mOffset;
								kerningValue.mInt = ((int)kerningValue.mChar << 16) | (int)((ushort)kerningValue.mOffset);
								fontLayer20.mKerningData.Add(kerningValue);
								CharData charData = fontLayer20.GetCharData(sortedKern.mKey);
								if (charData.mKerningCount == 0)
								{
									charData.mKerningFirst = (ushort)num13;
								}
								CharData charData2 = charData;
								charData2.mKerningCount += 1;
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetBaseOrder"))
			{
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer21 = new FontLayer();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer21) && !theParams.mElementVector[2].mIsList)
					{
						int mBaseOrder = 0;
						if (Common.StringToInt(((SingleDataElement)theParams.mElementVector[2]).mString.ToString(), ref mBaseOrder))
						{
							fontLayer21.mBaseOrder = mBaseOrder;
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetCharOrders"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer22 = new FontLayer();
					List<string> list19 = new List<string>();
					List<int> list20 = new List<int>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer22) && base.DataToStringVector(theParams.mElementVector[2], ref list19) && base.DataToIntVector(theParams.mElementVector[3], ref list20))
					{
						if (list19.Count == list20.Count)
						{
							for (int num14 = 0; num14 < list19.Count; num14++)
							{
								if (list19[num14].Length == 1)
								{
									fontLayer22.GetCharData(list19[num14][0]).mOrder = list20[num14];
								}
								else
								{
									flag2 = true;
								}
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (text.Equals("LayerSetExInfo"))
			{
				if (theParams.mElementVector.Count == 4)
				{
					FontLayer fontLayer23 = new FontLayer();
					List<string> list21 = new List<string>();
					List<string> list22 = new List<string>();
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer23) && base.DataToStringVector(theParams.mElementVector[2], ref list21) && base.DataToStringVector(theParams.mElementVector[3], ref list22))
					{
						if (list21.Count == list22.Count)
						{
							for (int num15 = 0; num15 < list21.Count; num15++)
							{
								fontLayer23.mExtendedInfo.Add(list21[num15], list22[num15]);
							}
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				if (!text.Equals("LayerSetAlphaCorrection"))
				{
					this.Error("Unknown Command");
					return false;
				}
				if (theParams.mElementVector.Count == 3)
				{
					FontLayer fontLayer24 = new FontLayer();
					int num16 = 0;
					if (this.DataToLayer(theParams.mElementVector[1], ref fontLayer24) && base.DataToInt(theParams.mElementVector[2], ref num16))
					{
						fontLayer24.mUseAlphaCorrection = num16 != 0;
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.Error("Invalid Number of Parameters");
				return false;
			}
			if (flag2)
			{
				this.Error("Invalid Paramater Type");
				return false;
			}
			if (flag3)
			{
				this.Error("Undefined Value");
				return false;
			}
			if (flag4)
			{
				this.Error("List Size Mismatch");
				return false;
			}
			return true;
		}

		public FontData()
		{
			this.mInitialized = false;
			this.mApp = null;
			this.mRefCount = 0;
			this.mDefaultPointSize = 0;
		}

		public override void Dispose()
		{
			foreach (KeyValuePair<string, DataElement> keyValuePair in this.mDefineMap)
			{;
				DataElement value = keyValuePair.Value;
				if (value != null)
				{
					value.Dispose();
				}
			}
		}

		public void Ref()
		{
			this.mRefCount++;
		}

		public void DeRef()
		{
			if (--this.mRefCount == 0)
			{
				this.Dispose();
			}
		}

		public bool Load(byte[] buffer)
		{
			if (this.mInitialized)
			{
				return false;
			}
			bool flag = false;
			this.mCurrentLine.Clear();
			this.mFontErrorHeader = "Font Descriptor Error in Load\r\n";
			this.mSourceFile = "";
			this.mInitialized = this.LoadDescriptor(buffer);
			return !flag;
		}

		public bool Load(SexyAppBase theSexyApp, string theFontDescFileName)
		{
			if (this.mInitialized)
			{
				return false;
			}
			this.mApp = theSexyApp;
			bool flag = false;
			this.mCurrentLine.Clear();
			this.mFontErrorHeader = "Font Descriptor Error in " + theFontDescFileName + "\r\n";
			this.mSourceFile = theFontDescFileName;
			this.mInitialized = this.LoadDescriptor(theFontDescFileName);
			return !flag;
		}

		public bool LoadLegacy(Image theFontImage, string theFontDescFileName)
		{
			if (this.mInitialized)
			{
				return false;
			}
			this.mFontLayerList.AddLast(new FontLayer(this));
			FontLayer value = this.mFontLayerList.Last.Value;
			this.mFontLayerMap.Add("MAIN", value);
			value.mImage.mUnsharedImage = (MemoryImage)theFontImage;
			value.mDefaultHeight = value.mImage.GetImage().GetHeight();
			value.mAscent = value.mImage.GetImage().GetHeight();
			int num = 0;
			PFILE pfile = new PFILE(theFontDescFileName, "rb");
			if (pfile == null)
			{
				return false;
			}
			this.mSourceFile = theFontDescFileName;
			pfile.Open();
			byte[] data = pfile.GetData();
			int i = 0;
			value.GetCharData(' ').mWidth = BitConverter.ToInt32(data, i);
			i += 4;
			value.mAscent = BitConverter.ToInt32(data, i);
			i += 4;
			while (i < data.Length)
			{
				byte b = data[i];
				i++;
				int num2 = BitConverter.ToInt32(data, i);
				i += 4;
				byte b2 = b;
				if (b2 == 0)
				{
					break;
				}
				value.GetCharData((char)b2).mImageRect = new Rect(num, 0, num2, value.mImage.GetImage().GetHeight());
				value.GetCharData((char)b2).mWidth = num2;
				num += num2;
			}
			for (char c = 'A'; c <= 'Z'; c += '\u0001')
			{
				if (value.GetCharData(c).mWidth == 0 && value.GetCharData((char)(c - 'A' + 'a')).mWidth != 0)
				{
					this.mCharMap.Add(c, (char)(c - 'A' + 'a'));
				}
			}
			for (char c = 'a'; c <= 'z'; c += '\u0001')
			{
				if (value.GetCharData(c).mWidth == 0 && value.GetCharData((char)(c - 'a' + 'A')).mWidth != 0)
				{
					this.mCharMap.Add(c, (char)(c - 'a' + 'A'));
				}
			}
			this.mInitialized = true;
			pfile.Close();
			return true;
		}

		public SexyAppBase mApp;

		public bool mInitialized;

		public int mRefCount;

		public int mDefaultPointSize;

		public Dictionary<char, char> mCharMap = new Dictionary<char, char>();

		public LinkedList<FontLayer> mFontLayerList = new LinkedList<FontLayer>();

		public Dictionary<string, FontLayer> mFontLayerMap = new Dictionary<string, FontLayer>();

		public string mSourceFile;

		public string mFontErrorHeader;

		public string mImagePathPrefix;
	}
}
