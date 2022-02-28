using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Sexy.GraphicsLib;
using Sexy.Misc;

namespace Sexy.Resource
{
	public class ResourceXmlReader : ContentTypeReader<ResourceManager>
	{
		protected override ResourceManager Read(ContentReader input, ResourceManager existingInstance)
		{
			this.manager = new ResourceManager(null);
			int num = input.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				input.ReadString();
				CompositeResGroup compositeResGroup = new CompositeResGroup();
				string text = input.ReadString();
				int num2 = input.ReadInt32();
				for (int j = 0; j < num2; j++)
				{
					SubGroup subGroup = new SubGroup();
					subGroup.mGroupName = input.ReadString();
					subGroup.mArtRes = input.ReadInt32();
					subGroup.mLocSet = input.ReadUInt32();
					compositeResGroup.mSubGroups.Add(subGroup);
				}
				this.manager.mCompositeResGroupMap.Add(text, compositeResGroup);
			}
			int num3 = input.ReadInt32();
			for (int k = 0; k < num3; k++)
			{
				ResGroup resGroup = new ResGroup();
				input.ReadString();
				string text2 = input.ReadString();
				int num4 = input.ReadInt32();
				for (int l = 0; l < num4; l++)
				{
					input.ReadString();
					this.readRes(input, resGroup);
				}
				this.manager.mResGroupMap.Add(text2, resGroup);
			}
			return this.manager;
		}

		protected void readRes(ContentReader input, ResGroup group)
		{
			BaseRes baseRes = null;
			ResType resType = (ResType)input.ReadInt32();
			switch (resType)
			{
			case ResType.ResType_Image:
			{
				ImageRes imageRes = new ImageRes();
				baseRes = imageRes;
				this.readBaseRes(input, baseRes, this.manager.mResMaps[(int)resType], this.manager.mResFromPathMap);
				imageRes.mPalletize = input.ReadBoolean();
				imageRes.mA4R4G4B4 = input.ReadBoolean();
				imageRes.mDDSurface = input.ReadBoolean();
				imageRes.mPurgeBits = input.ReadBoolean();
				imageRes.mA8R8G8B8 = input.ReadBoolean();
				imageRes.mDither16 = input.ReadBoolean();
				imageRes.mMinimizeSubdivisions = input.ReadBoolean();
				imageRes.mAutoFindAlpha = input.ReadBoolean();
				imageRes.mCubeMap = input.ReadBoolean();
				imageRes.mVolumeMap = input.ReadBoolean();
				imageRes.mNoTriRep = input.ReadBoolean();
				imageRes.m2DBig = input.ReadBoolean();
				imageRes.mIsAtlas = input.ReadBoolean();
				imageRes.mAlphaImage = input.ReadString();
				imageRes.mAlphaColor = input.ReadInt32();
				imageRes.mOffset = new SexyPoint();
				imageRes.mOffset.mX = input.ReadInt32();
				imageRes.mOffset.mY = input.ReadInt32();
				imageRes.mVariant = input.ReadString();
				imageRes.mAlphaGridImage = input.ReadString();
				imageRes.mRows = input.ReadInt32();
				imageRes.mCols = input.ReadInt32();
				string text = input.ReadString();
				if (text.Length != 0)
				{
					imageRes.mAtlasName = text;
				}
				imageRes.mAtlasX = input.ReadInt32();
				imageRes.mAtlasY = input.ReadInt32();
				imageRes.mAtlasW = input.ReadInt32();
				imageRes.mAtlasH = input.ReadInt32();
				imageRes.mAnimInfo.mAnimType = (AnimType)input.ReadInt32();
				imageRes.mAnimInfo.mFrameDelay = input.ReadInt32();
				imageRes.mAnimInfo.mBeginDelay = input.ReadInt32();
				imageRes.mAnimInfo.mEndDelay = input.ReadInt32();
				int num = input.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					imageRes.mAnimInfo.mPerFrameDelay.Add(input.ReadInt32());
				}
				num = input.ReadInt32();
				for (int j = 0; j < num; j++)
				{
					imageRes.mAnimInfo.mFrameMap.Add(input.ReadInt32());
				}
				if (imageRes.mAnimInfo.mAnimType != AnimType.AnimType_None)
				{
					int theNumCels = Math.Max(imageRes.mRows, imageRes.mCols);
					imageRes.mAnimInfo.Compute(theNumCels, imageRes.mAnimInfo.mBeginDelay, imageRes.mAnimInfo.mEndDelay);
				}
				break;
			}
			case ResType.ResType_Sound:
			{
				SoundRes soundRes = new SoundRes();
				baseRes = soundRes;
				this.readBaseRes(input, baseRes, this.manager.mResMaps[(int)resType], this.manager.mResFromPathMap);
				input.ReadString();
				soundRes.mVolume = input.ReadDouble();
				soundRes.mPanning = input.ReadInt32();
				break;
			}
			case ResType.ResType_Font:
			{
				FontRes fontRes = new FontRes();
				baseRes = fontRes;
				this.readBaseRes(input, baseRes, this.manager.mResMaps[(int)resType], this.manager.mResFromPathMap);
				string text2 = input.ReadString();
				if (!text2.Equals(""))
				{
					fontRes.mImagePath = text2;
				}
				fontRes.mTags = input.ReadString();
				fontRes.mSize = input.ReadInt32();
				fontRes.mBold = input.ReadBoolean();
				fontRes.mItalic = input.ReadBoolean();
				fontRes.mShadow = input.ReadBoolean();
				fontRes.mUnderline = input.ReadBoolean();
				fontRes.mSysFont = input.ReadBoolean();
				break;
			}
			case ResType.ResType_PopAnim:
				baseRes = new PopAnimRes();
				this.readBaseRes(input, baseRes, this.manager.mResMaps[(int)resType], this.manager.mResFromPathMap);
				break;
			case ResType.ResType_PIEffect:
				baseRes = new PIEffectRes();
				this.readBaseRes(input, baseRes, this.manager.mResMaps[(int)resType], this.manager.mResFromPathMap);
				break;
			case ResType.ResType_RenderEffect:
			{
				RenderEffectRes renderEffectRes = new RenderEffectRes();
				baseRes = renderEffectRes;
				this.readBaseRes(input, baseRes, this.manager.mResMaps[(int)resType], this.manager.mResFromPathMap);
				string text3 = input.ReadString();
				if (!text3.Equals(""))
				{
					renderEffectRes.mSrcFilePath = text3;
				}
				break;
			}
			case ResType.ResType_GenericResFile:
				baseRes = new GenericResFileRes();
				this.readBaseRes(input, baseRes, this.manager.mResMaps[(int)resType], this.manager.mResFromPathMap);
				break;
			}
			baseRes.ApplyConfig();
			group.mResList.Add(baseRes);
		}

		protected void readBaseRes(ContentReader input, BaseRes res, Dictionary<string, BaseRes> theMap, Dictionary<string, BaseRes> resFromPathMap)
		{
			res.mId = input.ReadString();
			res.mResGroup = input.ReadString();
			res.mCompositeResGroup = input.ReadString();
			res.mArtRes = input.ReadInt32();
			res.mLocSet = input.ReadUInt32();
			res.mPath = input.ReadString();
			res.mFromProgram = input.ReadBoolean();
			res.mXMLAttributes = new Dictionary<string, string>();
			int num = input.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = input.ReadString();
				string text2 = input.ReadString();
				res.mXMLAttributes.Add(text, text2);
			}
			if (res.mPath.Length > 0)
			{
				string text3 = res.mPath.ToUpper();
				if (text3.IndexOf(".") != -1)
				{
					text3 = text3.Substring(0, text3.IndexOf("."));
				}
				resFromPathMap[text3] = res;
			}
			theMap.Add(res.mId, res);
		}

		protected ResourceManager manager;
	}
}
