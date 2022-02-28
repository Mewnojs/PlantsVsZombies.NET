using System;
using System.Collections.Generic;

namespace Sexy.GraphicsLib
{
	public class SimpleObjectPool<T> : IDisposable where T : new()
	{
		public SimpleObjectPool()
		{
			this.mNumPools = 0;
			this.mNumAvailObjects = 0;
			this.mFreeData = null;
			this.mDataPools = null;
			this.mDataPools = new List<T>();
			this.mFreeData = new List<T>();
		}

		public virtual void Dispose()
		{
			this.mDataPools.Clear();
			this.mFreeData.Clear();
		}

		public void GrowPool()
		{
		}

		public T Alloc()
		{
			T t = default(T);
			t = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
			this.mDataPools.Add(t);
			return t;
		}

		public void Free(T thePtr)
		{
			this.mDataPools.Remove(thePtr);
		}

		public static int OBJECT_POOL_SIZE = 8192;

		public int mNumPools;

		public int mNumAvailObjects;

		public List<T> mDataPools;

		public List<T> mFreeData;
	}
}
