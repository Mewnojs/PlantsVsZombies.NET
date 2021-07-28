using System;
using System.Collections.Generic;

namespace Sexy.TodLib
{
	internal class TodResourceManager : ResourceManager
	{
		public TodResourceManager() : base(GlobalStaticVars.gSexyAppBase)
		{
		}

		public bool FindImagePath(Image theImage, ref string thePath)
		{
			foreach (KeyValuePair<string, BaseRes> keyValuePair in this.mImageMap)
			{
				ImageRes imageRes = (ImageRes)keyValuePair.Value;
				if (imageRes.mImage == theImage)
				{
					Dictionary<string, BaseRes>.Enumerator enumerator = base.mImageMap.GetEnumerator();
					KeyValuePair<string, BaseRes> keyValuePair2 = enumerator.Current;
					thePath = keyValuePair2.Key;
					return true;
				}
			}
			return false;
		}

		public bool TodLoadNextResource()
		{
			bool flag = false;
			while (!flag)
			{
				flag = this.mCurResGroupListItr.MoveNext();
				BaseRes baseRes = this.mCurResGroupListItr.Current;
				if (!baseRes.mFromProgram)
				{
					switch (baseRes.mType)
					{
					case ResType.ResType_Image:
					{
						ImageRes imageRes = (ImageRes)baseRes;
						imageRes.mPalletize = false;
						if (imageRes.mImage == null)
						{
							goto IL_7F;
						}
						break;
					}
					case ResType.ResType_Sound:
					{
						SoundRes soundRes = (SoundRes)baseRes;
						if (soundRes.mSoundId == -1)
						{
							goto IL_7F;
						}
						break;
					}
					case ResType.ResType_Font:
					{
						FontRes fontRes = (FontRes)baseRes;
						if (fontRes.mFont == null)
						{
							goto IL_7F;
						}
						break;
					}
					default:
						goto IL_7F;
					}
				}
			}
			IL_7F:
			return !flag && base.LoadNextResource();
		}

		public new bool TodLoadResources(string theGroup, bool doUnpackAtlasImages)
		{
			if (base.IsGroupLoaded(theGroup))
			{
				return true;
			}
			PerfTimer perfTimer = default(PerfTimer);
			perfTimer.Start();
			base.StartLoadResources(theGroup);
			while (!GlobalStaticVars.gSexyAppBase.mShutdown && this.TodLoadNextResource())
			{
			}
			if (GlobalStaticVars.gSexyAppBase.mShutdown)
			{
				return false;
			}
			if (base.HadError())
			{
				return false;
			}
			this.mLoadedGroups.Add(theGroup);
			Math.Max((int)perfTimer.GetDuration(), 0);
			return true;
		}
	}
}
