using System;
using System.Collections.Generic;

namespace Sexy.Resource
{
	public abstract class BaseRes
	{
		public BaseRes()
		{
		}

		public virtual void DeleteResource()
		{
		}

		public virtual void ApplyConfig()
		{
		}

		public ResourceManager mParent;

		public ResourceRef mResourceRef;

		public ResGlobalPtr mGlobalPtr = new ResGlobalPtr();

		public int mGlobalIndex;

		public ResType mType = ResType.Num_ResTypes;

		public int mRefCount;

		public int mReloadIdx;

		public int mArtRes;

		public uint mLocSet;

		public bool mDirectLoaded;

		public bool mFromProgram;

		public string mId;

		public string mResGroup;

		public string mCompositeResGroup;

		public string mPath;

		public Dictionary<string, string> mXMLAttributes;
	}
}
