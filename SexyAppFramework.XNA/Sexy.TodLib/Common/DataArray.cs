using System;
using Sexy;

namespace Sexy.TodLib
{
    public class DataArray<T> where T : class, new()
    {
        public DataArray()
        {
            mBlock = null;
            mMaxUsedCount = 0U;
            mMaxSize = 0U;
            mFreeListHead = 0;
            mSize = 0U;
            mNextKey = 1U;
            mName = null;
        }

        public void Dispose()
        {
            DataArrayDispose();
        }

        public void DataArrayInitialize(uint theMaxSize, string theName)
        {
            Debug.ASSERT(mBlock == null);
            Debug.ASSERT(theMaxSize <= 65536U);
            if (mBlock == null || mBlock.Length != (long)((ulong)theMaxSize))
            {
                mBlock = new T[theMaxSize];
            }
            for (int i = 0; i < mBlock.Length; i++)
            {
                mBlock[i] = default(T);
            }
            mMaxSize = theMaxSize;
            mName = theName;
            mNextKey = 1U;
        }

        public void DataArrayDispose()
        {
            if (mBlock != null)
            {
                DataArrayFreeAll();
                mBlock = null;
                mMaxUsedCount = 0U;
                mMaxSize = 0U;
                mFreeListHead = 0;
                mSize = 0U;
                mName = null;
            }
        }

        public void DataArrayFreeAll()
        {
            int num = 0;
            while (num < (long)((ulong)mMaxSize))
            {
                mBlock[num] = default(T);
                num++;
            }
            mMaxUsedCount = 0U;
            mFreeListHead = 0;
        }

        public bool IterateNext(ref T theItem)
        {
            if (theItem == null)
            {
                theItem = DataArrayGet(1U);
                return true;
            }
            int nextValidIndex = GetNextValidIndex((uint)(DataArrayGetID(theItem) - 1));
            if (nextValidIndex >= (long)((ulong)mMaxUsedCount))
            {
                theItem = default(T);
                return false;
            }
            theItem = mBlock[nextValidIndex];
            return true;
        }

        private int GetNextValidIndex(uint index)
        {
            int num = (int)(index + 1U);
            while (num < (long)((ulong)mMaxSize))
            {
                if (mBlock[num] != null)
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
            while (num < (long)((ulong)mMaxSize))
            {
                if (mBlock[num] == null)
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
            if (mFreeListHead == (long)((ulong)mMaxUsedCount))
            {
                num = mMaxUsedCount;
                mMaxUsedCount += 1U;
                mFreeListHead = (int)mMaxUsedCount;
            }
            else
            {
                num = (uint)mFreeListHead;
                mFreeListHead = GetNextFreeIndex((uint)mFreeListHead);
            }
            T t = Activator.CreateInstance<T>();
            mBlock[(int)((UIntPtr)num)] = t;
            return t;
        }

        public void DataArrayFree(T theItem)
        {
            int num = 0;
            while (num < (long)((ulong)mMaxSize))
            {
                if (mBlock[num] == theItem)
                {
                    mBlock[num] = default(T);
                    if (num < mFreeListHead)
                    {
                        mFreeListHead = num;
                    }
                    return;
                }
                num++;
            }
        }

        public T DataArrayGet(uint theId)
        {
            return mBlock[(int)((UIntPtr)theId)];
        }

        public T DataArrayTryToGet(uint theId)
        {
            if (theId >= mMaxSize)
            {
                return default(T);
            }
            return mBlock[(int)((UIntPtr)theId)];
        }

        public int DataArrayGetID(T theItem)
        {
            int num = 0;
            while (num < (long)((ulong)mMaxSize))
            {
                if (mBlock[num] == theItem)
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
}
