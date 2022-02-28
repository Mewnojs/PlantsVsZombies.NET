using System;
using Sexy.Misc;

namespace Lawn
{
    public/*internal*/ class MotionTrailFrame
    {
        public void SaveToFile(SexyBuffer b)
        {
            b.WriteFloat(mPosX);
            b.WriteFloat(mPosY);
            b.WriteFloat(mAnimTime);
        }

        public void LoadFromFile(SexyBuffer b)
        {
            mPosX = b.ReadFloat();
            mPosY = b.ReadFloat();
            mAnimTime = b.ReadFloat();
        }

        public float mPosX;

        public float mPosY;

        public float mAnimTime;
    }
}
