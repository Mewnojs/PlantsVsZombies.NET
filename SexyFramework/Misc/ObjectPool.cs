using System;
using System.Collections.Generic;

namespace Sexy.Misc
{
	public class ObjectPool<T> where T : IDisposable, new()
	{
		public ObjectPool(int size)
		{
			this.mNumWant = 0;
			this.mNumAvailObjects = 0;
			this.mNextAvailIndex = 0;
			this.mPoolSize = size;
			this.mFreePools = new List<T>();
			this.mNumAvailObjects += this.mPoolSize;
		}

		public virtual void Dispose()
		{
			for (int i = 0; i < this.mFreePools.Count; i++)
			{
				if (this.mFreePools[i] != null)
				{
					T t = this.mFreePools[i];
					t.Dispose();
				}
			}
			this.mFreePools.Clear();
		}

		public T Alloc()
		{
			if (this.mFreePools.Count > 0)
			{
				T result = this.mFreePools[this.mFreePools.Count - 1];
				this.mFreePools.RemoveAt(this.mFreePools.Count - 1);
				return result;
			}
			this.mNumWant++;
			return (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
		}

		public void Free(T thePtr)
		{
			this.mFreePools.Add(thePtr);
		}

		public int mPoolSize;

		public int mNumWant;

		public int mNumAvailObjects;

		public List<T> mFreePools;

		public int mNextAvailIndex;
	}
}
