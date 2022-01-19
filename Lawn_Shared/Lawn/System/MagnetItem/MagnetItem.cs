using System;
using Sexy;

namespace Lawn
{
    public/*internal*/ class MagnetItem
    {
        public void Reset()
        {
            mPosX = (mPosY = (mDestOffsetX = (mDestOffsetY = 0f)));
            mItemType = MagnetItemType.None;
        }

        public bool SaveToFile(Sexy.Buffer b)
        {
            b.WriteFloat(mDestOffsetX);
            b.WriteFloat(mDestOffsetY);
            b.WriteLong((int)mItemType);
            b.WriteFloat(mPosX);
            b.WriteFloat(mPosY);
            return true;
        }

        public bool LoadFromFile(Sexy.Buffer b)
        {
            mDestOffsetX = b.ReadFloat();
            mDestOffsetY = b.ReadFloat();
            mItemType = (MagnetItemType)b.ReadLong();
            mPosX = b.ReadFloat();
            mPosY = b.ReadFloat();
            return true;
        }

        public float mPosX;

        public float mPosY;

        public float mDestOffsetX;

        public float mDestOffsetY;

        public MagnetItemType mItemType;
    }
}
