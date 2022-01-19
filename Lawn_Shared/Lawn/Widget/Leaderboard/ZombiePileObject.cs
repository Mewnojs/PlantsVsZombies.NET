using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ struct ZombiePileObject
    {
        public ZombiePileObject(int aHeight, ZombiePileObjectType aType)
        {
            mHeight = aHeight;
            mType = aType;
            mOffsetX = (mOffsetY = 0f);
            mCounter = 0f;
            mY = -1;
            gemSpeedX = ((float)ZombiePileObject.rand.NextDouble() - 0.5f) * 5f;
            if (Math.Abs(gemSpeedX) < 0.7f)
            {
                gemSpeedX = 0.7f;
            }
            gemSpeedY = ((float)ZombiePileObject.rand.NextDouble() - 0.5f) * 0.5f;
            gemRotationSpeed = ((float)ZombiePileObject.rand.NextDouble() - 0.5f) * 0.1f;
            switch (aType)
            {
            case ZombiePileObjectType.Gem0:
                gemImage = AtlasResources.IMAGE_PILE_GEM0;
                break;
            case ZombiePileObjectType.Gem1:
                gemImage = AtlasResources.IMAGE_PILE_GEM1;
                break;
            case ZombiePileObjectType.Gem2:
                gemImage = AtlasResources.IMAGE_PILE_GEM2;
                break;
            case ZombiePileObjectType.Gem3:
                gemImage = AtlasResources.IMAGE_PILE_GEM3;
                break;
            case ZombiePileObjectType.Gem4:
                gemImage = AtlasResources.IMAGE_PILE_GEM4;
                break;
            case ZombiePileObjectType.Gem5:
                gemImage = AtlasResources.IMAGE_PILE_GEM5;
                break;
            case ZombiePileObjectType.Gem6:
                gemImage = AtlasResources.IMAGE_PILE_GEM6;
                break;
            default:
                gemImage = null;
                break;
            }
            if (aType == ZombiePileObjectType.YellowCloud)
            {
                mCounter = ZombiePileObject.rand.Next(Constants.BOARD_WIDTH);
            }
            if (aType == ZombiePileObjectType.Astronaut)
            {
                mReanim = Reanimation.GetNewReanimation();
                mReanim.ReanimationInitializeType(0f, 0f, ReanimationType.Astronaut);
                mReanim.mLoopType = ReanimLoopType.Loop;
                mReanim.mOverlayMatrix.mMatrix.M11 = 0.85f;
                mReanim.mOverlayMatrix.mMatrix.M22 = 0.85f;
                mOffsetX = 400f;
                return;
            }
            mReanim = null;
            mOffsetX = ZombiePileObject.rand.Next(Constants.BOARD_WIDTH);
        }

        public const float maxGemSpeedX = 5f;

        public int mHeight;

        public ZombiePileObjectType mType;

        public float mOffsetX;

        public float mOffsetY;

        public float mCounter;

        public int mY;

        public Reanimation mReanim;

        public float gemSpeedX;

        public float gemSpeedY;

        public float gemRotationSpeed;

        public Image gemImage;

        private static Random rand = new Random();
    }
}
