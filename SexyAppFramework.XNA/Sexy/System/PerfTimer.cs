using System;

namespace Sexy
{
    public struct PerfTimer
    {
        private void CalcDuration()
        {
            mEnd = Environment.TickCount;
            mDuration = mEnd - mStart;
        }

        public void Start()
        {
            mRunning = true;
            mStart = Environment.TickCount;
        }

        public void Stop()
        {
            if (mRunning)
            {
                CalcDuration();
                mRunning = false;
            }
        }

        public double GetDuration()
        {
            if (mRunning)
            {
                CalcDuration();
            }
            return mDuration;
        }

        private bool mRunning;

        private int mStart;

        private int mEnd;

        private int mDuration;
    }
}
