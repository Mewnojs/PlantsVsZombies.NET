using System;

namespace Lawn
{
    public/*internal*/ class ChosenSeed
    {
        public int mX;

        public int mY;

        public int mTimeStartMotion;

        public int mTimeEndMotion;

        public int mStartX;

        public int mStartY;

        public int mEndX;

        public int mEndY;

        public SeedType mSeedType;

        public ChosenSeedState mSeedState;

        public int mSeedIndexInBank;

        public bool mRefreshing;

        public int mRefreshCounter;

        public SeedType mImitaterType;

        public bool mCrazyDavePicked;
    }
}
