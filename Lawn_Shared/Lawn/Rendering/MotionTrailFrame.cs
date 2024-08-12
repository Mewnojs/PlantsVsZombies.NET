using System;
using Sexy;
using static Lawn.GlobalMembersSaveGame;

namespace Lawn
{
    public/*internal*/ class MotionTrailFrame
    {
        internal void Sync(SaveGameContext theContext)
        {
            theContext.SyncFloat(ref mPosX);
            theContext.SyncFloat(ref mPosY);
            theContext.SyncFloat(ref mAnimTime);
        }

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
