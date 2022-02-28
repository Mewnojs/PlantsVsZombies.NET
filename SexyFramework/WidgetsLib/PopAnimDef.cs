using System;
using System.Collections.Generic;

namespace Sexy.WidgetsLib
{
	public class PopAnimDef
	{
		public PopAnimDef()
		{
			this.mRefCount = 0;
			this.mMainSpriteDef = null;
		}

		public void Dispose()
		{
			if (this.mMainSpriteDef != null)
			{
				this.mMainSpriteDef.Dispose();
			}
		}

		public PASpriteDef mMainSpriteDef;

		public List<PASpriteDef> mSpriteDefVector = new List<PASpriteDef>();

		public LinkedList<string> mObjectNamePool = new LinkedList<string>();

		public int mRefCount;
	}
}
