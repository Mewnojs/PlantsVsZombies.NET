using System;

namespace Sexy.Resource
{
	public abstract class DataElement
	{
		public DataElement()
		{
			this.mIsList = false;
		}

		public virtual void Dispose()
		{
		}

		public abstract DataElement Duplicate();

		public bool mIsList;
	}
}
