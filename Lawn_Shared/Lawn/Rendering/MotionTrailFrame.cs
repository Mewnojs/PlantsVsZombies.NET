using System;
using Sexy;

namespace Lawn
{
    public/*internal*/ class MotionTrailFrame
    {
        public void SaveToFile(Sexy.Buffer b)
        {
            b.WriteFloat(mPosX);
            b.WriteFloat(mPosY);
            b.WriteFloat(mAnimTime);
        }

        public void LoadFromFile(Sexy.Buffer b)
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
