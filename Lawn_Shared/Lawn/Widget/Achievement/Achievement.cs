using System;

namespace Lawn
{
    public/*internal*/ class Achievement
    {
        public Achievement(int aImageId, string aName, string aDesc)
        {
            mImageId = aImageId;
            mName = aName;
            mDesc = aDesc;
        }

        public int mImageId;

        public readonly string mName;

        public readonly string mDesc;
    }
}
