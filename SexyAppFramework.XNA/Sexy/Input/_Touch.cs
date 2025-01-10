using System;

namespace Sexy
{
    public struct _Touch
    {
        public CGPoint location;

        public CGPoint previousLocation;

        public int tapCount;

        public double timestamp;

        public _Phase phase;
    }
}
