using System;
using Sexy;

public class DataArray<T> where T : class, new()
{
	public DataArray()
	{
		this.mBlock = null;
		this.mMaxUsedCount = 0U;
		this.mMaxSize = 0U;
		this.mFreeListHead = 0;
		this.mSize = 0U;
		this.mNextKey = 1U;
		this.mName = null;
	}

	public void Dispose()
	{
		this.DataArrayDispose();
	}

	public void DataArrayInitialize(uint theMaxSize, string theName)
	{
		Debug.ASSERT(this.mBlock == null);
		Debug.ASSERT(theMaxSize <= 65536U);
		if (this.mBlock == null || (long)this.mBlock.Length != (long)((ulong)theMaxSize))
		{
			this.mBlock = new T[theMaxSize];
		}
		for (int i = 0; i < this.mBlock.Length; i++)
		{
			this.mBlock[i] = default(T);
		}
		this.mMaxSize = theMaxSize;
		this.mName = theName;
		this.mNextKey = 1U;
	}

	public void DataArrayDispose()
	{
		if (this.mBlock != null)
		{
			this.DataArrayFreeAll();
			this.mBlock = null;
			this.mMaxUsedCount = 0U;
			this.mMaxSize = 0U;
			this.mFreeListHead = 0;
			this.mSize = 0U;
			this.mName = null;
		}
	}

	public void DataArrayFreeAll()
	{
		int num = 0;
		while ((long)num < (long)((ulong)this.mMaxSize))
		{
			this.mBlock[num] = default(T);
			num++;
		}
		this.mMaxUsedCount = 0U;
		this.mFreeListHead = 0;
	}

	public bool IterateNext(ref T theItem)
	{
		if (theItem == null)
		{
			theItem = this.DataArrayGet(1U);
			return true;
		}
		int nextValidIndex = this.GetNextValidIndex((uint)(this.DataArrayGetID(theItem) - 1));
		if ((long)nextValidIndex >= (long)((ulong)this.mMaxUsedCount))
		{
			theItem = default(T);
			return false;
		}
		theItem = this.mBlock[nextValidIndex];
		return true;
	}

	private int GetNextValidIndex(uint index)
	{
		int num = (int)(index + 1U);
		while ((long)num < (long)((ulong)this.mMaxSize))
		{
			if (this.mBlock[num] != null)
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	private int GetNextFreeIndex(uint index)
	{
		int num = (int)(index + 1U);
		while ((long)num < (long)((ulong)this.mMaxSize))
		{
			if (this.mBlock[num] == null)
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	public T DataArrayAlloc()
	{
		uint num;
		if ((long)this.mFreeListHead == (long)((ulong)this.mMaxUsedCount))
		{
			num = this.mMaxUsedCount;
			this.mMaxUsedCount += 1U;
			this.mFreeListHead = (int)this.mMaxUsedCount;
		}
		else
		{
			num = (uint)this.mFreeListHead;
			this.mFreeListHead = this.GetNextFreeIndex((uint)this.mFreeListHead);
		}
		T t = Activator.CreateInstance<T>();
		this.mBlock[(int)((UIntPtr)num)] = t;
		return t;
	}

	public void DataArrayFree(T theItem)
	{
		int num = 0;
		while ((long)num < (long)((ulong)this.mMaxSize))
		{
			if (this.mBlock[num] == theItem)
			{
				this.mBlock[num] = default(T);
				if (num < this.mFreeListHead)
				{
					this.mFreeListHead = num;
				}
				return;
			}
			num++;
		}
	}

	public T DataArrayGet(uint theId)
	{
		return this.mBlock[(int)((UIntPtr)theId)];
	}

	public T DataArrayTryToGet(uint theId)
	{
		if (theId >= this.mMaxSize)
		{
			return default(T);
		}
		return this.mBlock[(int)((UIntPtr)theId)];
	}

	public int DataArrayGetID(T theItem)
	{
		int num = 0;
		while ((long)num < (long)((ulong)this.mMaxSize))
		{
			if (this.mBlock[num] == theItem)
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	public T[] mBlock;

	public uint mMaxUsedCount;

	public uint mMaxSize;

	public int mFreeListHead;

	public uint mSize;

	public uint mNextKey;

	public string mName;
}
