using System;
using System.Collections.Generic;

namespace Sexy
{
	public/*internal*/ class BaseRes
	{
		~BaseRes()
		{
			DeleteResource();
		}

		public virtual void DeleteResource()
		{
		}

		public ResType mType;

		public string mId;

		public string mResGroup;

		public string mPath;

		public bool mFromProgram;

		public Dictionary<string, string> mXMLAttributes;

		public int mUnloadGroup;
	}
}
