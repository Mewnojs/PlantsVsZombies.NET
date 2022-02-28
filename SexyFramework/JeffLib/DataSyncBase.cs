using System;
using System.Collections.Generic;

namespace JeffLib
{
	public class DataSyncBase
	{
		public virtual void SyncLong(ref int theInt)
		{
			throw new NotSupportedException();
		}

		public virtual void SyncFloat(ref float theInt)
		{
			throw new NotSupportedException();
		}

		public virtual void SyncListInt(List<int> theList)
		{
			throw new NotSupportedException();
		}

		public virtual void SyncListFloat(List<float> theList)
		{
			throw new NotSupportedException();
		}
	}
}
