using System;

namespace Sexy.TodLib
{
    public enum DataArrayFlags : uint
    {
        DATA_ARRAY_INDEX_MASK = 65535U,
        DATA_ARRAY_KEY_MASK = 4294901760U,
        DATA_ARRAY_KEY_SHIFT = 16U,
        DATA_ARRAY_MAX_SIZE = 65536U,
        DATA_ARRAY_KEY_FIRST = 1U,
        DATAID_NULL = 0U
    }
}
