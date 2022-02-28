using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.AELib
{
	public class AECommon
	{
		public static bool LoadPAX(string file_name, List<Composition> compositions, AECommon.LoadCompImageFunc load_img_func, AECommon.PostLoadCompImageFunc post_load_img_func)
		{
			SexyBuffer buffer = new SexyBuffer();
			GlobalMembers.gSexyAppBase.ReadBufferFromStream(file_name + ".pax", ref buffer);
			ParseByteArray parseByteArray = new ParseByteArray(buffer.GetDataPtr());
			if (parseByteArray.isEnd())
			{
				return false;
			}
			int num = 0;
			parseByteArray.readInt32(ref num);
			int num2 = 0;
			parseByteArray.readInt32(ref num2);
			List<FootageDescriptor> list = new List<FootageDescriptor>();
			for (int i = 0; i < num2; i++)
			{
				int len = 0;
				parseByteArray.readInt32(ref len);
				string sn = "";
				parseByteArray.readString(ref sn, len);
				long id = 0L;
				parseByteArray.readLong(ref id);
				parseByteArray.readInt32(ref len);
				string text = "";
				parseByteArray.readString(ref text, len);
				long w = 0L;
				long h = 0L;
				parseByteArray.readLong(ref w);
				parseByteArray.readLong(ref h);
				string[] array = new string[] { ".psd", ".png", ".jp2", ".gif", ".jpg", ".j2k" };
				string text2 = text.ToLower();
				bool flag = false;
				for (int j = 0; j < array.Length; j++)
				{
					if (text2.Contains(array[j]))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					list.Add(new FootageDescriptor(sn, id, text, w, h));
				}
			}
			while (!parseByteArray.isEnd())
			{
				Composition composition = new Composition();
				int num3 = 0;
				parseByteArray.readInt32(ref num3);
				if (num3 != 1)
				{
					break;
				}
				compositions.Add(composition);
				int len2 = 0;
				parseByteArray.readInt32(ref len2);
				parseByteArray.readString(ref composition.mLayerName, len2);
				long num4 = 0L;
				long num5 = 0L;
				parseByteArray.readLong(ref num4);
				parseByteArray.readLong(ref num5);
				composition.mWidth = (int)num4;
				composition.mHeight = (int)num5;
				int num6 = 0;
				parseByteArray.readInt32(ref num6);
				int num7 = 0;
				parseByteArray.readInt32(ref num7);
				composition.SetMaxDuration(num7);
				for (int k = 0; k < num6; k++)
				{
					parseByteArray.readInt32(ref num3);
					bool flag2 = false;
					parseByteArray.readBoolean(ref flag2);
					if (flag2)
					{
						bool flag3 = false;
						long num8 = 0L;
						parseByteArray.readBoolean(ref flag3);
						parseByteArray.readLong(ref num8);
						int len3 = 0;
						parseByteArray.readInt32(ref len3);
						string text3 = "";
						parseByteArray.readString(ref text3, len3);
						len3 = 0;
						string text4 = "";
						parseByteArray.readInt32(ref len3);
						parseByteArray.readString(ref text4, len3);
						int start_frame = 0;
						parseByteArray.readInt32(ref start_frame);
						int num9 = 0;
						parseByteArray.readInt32(ref num9);
						int num10 = 0;
						parseByteArray.readInt32(ref num10);
						int layer_offset = 0;
						parseByteArray.readInt32(ref layer_offset);
						bool mAdditive = false;
						if (num >= 5)
						{
							parseByteArray.readBoolean(ref mAdditive);
						}
						Layer layer;
						if (flag3)
						{
							layer = new Layer();
						}
						else
						{
							layer = new Composition();
						}
						layer.mLayerName = text4;
						layer.mAdditive = mAdditive;
						string text5 = text4;
						if (flag3)
						{
							for (int l = 0; l < list.Count; l++)
							{
								if (list[l].mId == num8)
								{
									text5 = list[l].mFullName;
									int num11 = list[l].mShortName.IndexOf('/');
									if (num11 != -1)
									{
										text3 = list[l].mShortName.Substring(0, num11);
									}
									else
									{
										text3 = "";
									}
									layer.mWidth = (int)list[l].mWidth;
									layer.mHeight = (int)list[l].mHeight;
									break;
								}
							}
							int num12 = text5.LastIndexOf('\\');
							if (num12 == -1)
							{
								num12 = text5.LastIndexOf('/');
							}
							if (num12 != -1)
							{
								text5 = text5.Substring(num12 + 1);
							}
							text5 = Common.Trim(text5);
							int num13 = text5.LastIndexOf('.');
							if (num13 != -1)
							{
								text5 = text5.Substring(0, num13);
							}
							if (text3.Length > 0)
							{
								text5 = text5 + "\\" + text3;
							}
							SharedImageRef sharedImageRef = load_img_func("images\\", text5);
							if (sharedImageRef == null || sharedImageRef.mWidth == 0 || sharedImageRef.mHeight == 0)
							{
								throw new ParseFileException("Image is NULL!");
							}
							layer.SetImage(sharedImageRef);
							post_load_img_func(sharedImageRef, layer);
						}
						parseByteArray.readInt32(ref num3);
						while (num3 != 9)
						{
							double num14 = 0.0;
							double num15 = 0.0;
							parseByteArray.readDouble(ref num14);
							parseByteArray.readDouble(ref num15);
							int num16 = 0;
							parseByteArray.readInt32(ref num16);
							long num17 = 0L;
							parseByteArray.readLong(ref num17);
							bool flag4 = false;
							int mLoopType = 0;
							int mLoopFrame = 0;
							parseByteArray.readBoolean(ref flag4);
							parseByteArray.readInt32(ref mLoopType);
							parseByteArray.readInt32(ref mLoopFrame);
							if (flag4)
							{
								switch (num3)
								{
								case 3:
									layer.mAnchorPoint.mLoopFrame = mLoopFrame;
									layer.mAnchorPoint.mLoopType = mLoopType;
									break;
								case 4:
									layer.mPosition.mLoopFrame = mLoopFrame;
									layer.mPosition.mLoopType = mLoopType;
									break;
								case 5:
									layer.mScale.mLoopFrame = mLoopFrame;
									layer.mScale.mLoopType = mLoopType;
									break;
								case 6:
									layer.mRotation.mLoopFrame = mLoopFrame;
									layer.mRotation.mLoopType = mLoopType;
									break;
								case 7:
									layer.mOpacity.mLoopFrame = mLoopFrame;
									layer.mOpacity.mLoopType = mLoopType;
									break;
								}
							}
							int num18 = 0;
							if (num16 > 0)
							{
								parseByteArray.readInt32(ref num18);
								num17 = 0L;
							}
							for (long num19 = -1L; num19 < num17; num19 += 1L)
							{
								int num20 = 0;
								if (num19 >= 0L)
								{
									parseByteArray.readInt32(ref num20);
								}
								int num21 = 0;
								double num22 = 0.0;
								double num23 = 0.0;
								if (num19 >= 0L)
								{
									parseByteArray.readInt32(ref num21);
									parseByteArray.readDouble(ref num22);
									parseByteArray.readDouble(ref num23);
									num21 *= (int)((float)num10 / (float)num9);
								}
								else
								{
									num21 = 0;
									num22 = num14;
									num23 = num15;
								}
								switch (num3)
								{
								case 3:
									layer.AddAnchorPoint(num21, (float)num22, (float)num23);
									break;
								case 4:
									layer.AddPosition(num21, (float)num22, (float)num23);
									break;
								case 5:
									layer.AddScale(num21, (float)num22 / 100f, (float)num23 / 100f);
									break;
								case 6:
									layer.AddRotation(num21, (float)num22 * 3.14159f / 180f);
									break;
								case 7:
									layer.AddOpacity(num21, (float)num22 / 100f);
									break;
								}
							}
							for (int m = 0; m < num16; m++)
							{
								double num24 = 0.0;
								double num25 = 0.0;
								parseByteArray.readDouble(ref num24);
								parseByteArray.readDouble(ref num25);
								switch (num3)
								{
								case 3:
									layer.AddAnchorPoint(num18 + m, (float)num24, (float)num25);
									break;
								case 4:
									layer.AddPosition(num18 + m, (float)num24, (float)num25);
									break;
								case 5:
									layer.AddScale(num18 + m, (float)num24 / 100f, (float)num25 / 100f);
									break;
								case 6:
									layer.AddRotation(num18 + m, (float)num24 * 3.14159f / 180f);
									break;
								case 7:
									layer.AddOpacity(num18 + m, (float)num24 / 100f);
									break;
								}
							}
							parseByteArray.readInt32(ref num3);
						}
						layer.EnsureTimelineDefaults(num4, num5);
						composition.AddLayer(layer, start_frame, (num10 > num7) ? num7 : num10, layer_offset);
					}
				}
				composition.EnsureTimelineDefaults(num4, num5);
			}
			for (int n = 0; n < compositions.Count; n++)
			{
				Composition composition2 = compositions[n];
				for (int num26 = 0; num26 < composition2.GetNumLayers(); num26++)
				{
					Layer layerAtIdx = composition2.GetLayerAtIdx(num26);
					int num27 = 0;
					while (num27 < compositions.Count)
					{
						Composition composition3 = compositions[num27];
						if (composition3.mLayerName.ToLower().Equals(layerAtIdx.mLayerName.ToLower()))
						{
							Composition composition4 = layerAtIdx as Composition;
							if (composition4 != null)
							{
								composition4.CopyLayersFrom(composition3);
								composition4.mWidth = composition3.mWidth;
								composition4.mHeight = composition3.mHeight;
								break;
							}
							break;
						}
						else
						{
							num27++;
						}
					}
				}
			}
			return true;
		}

		public const int PAX_VERSION = 5;

		public const int COMPOSITION_HEADER = 1;

		public const int LAYER_HEADER = 2;

		public const int ANCHOR_POINT_HEADER = 3;

		public const int POSITION_HEADER = 4;

		public const int SCALE_HEADER = 5;

		public const int ROTATION_HEADER = 6;

		public const int OPACITY_HEADER = 7;

		public const int KEYFRAME_MARKER = 8;

		public const int END_OF_LAYER = 9;

		public const int END_OF_POPFX = 9999999;

		public const int LOOP_REPEAT = 10;

		public const int LOOP_PINGPONG = 11;

		public delegate void PostLoadCompImageFunc(SharedImageRef img, Layer l);

		public delegate SharedImageRef LoadCompImageFunc(string file_dir, string file_name);

		public delegate void PreLayerDrawFunc(Graphics g, Layer l, object data);
	}
}
