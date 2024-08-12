using System;
using Sexy;
using Sexy.TodLib;
using static Lawn.GlobalMembersSaveGame;

namespace Lawn
{
    public class PottedPlant
    {
        internal void Sync(SaveGameContext theContext)
        {
            theContext.SyncEnum(ref mSeedType, theContext.aSeedTypeSaver);
            theContext.SyncEnum(ref mWhichZenGarden, theContext.aGardenTypeSaver);
            theContext.SyncInt(ref mX);
            theContext.SyncInt(ref mY);
            theContext.SyncEnum(ref mFacing, theContext.aFacingDirectionSaver);
            theContext.SyncDateTime(ref mLastWateredTime);
            theContext.SyncEnum(ref mDrawVariation, theContext.aDrawVariationSaver);
            theContext.SyncEnum(ref mPlantAge, theContext.aPottedPlantAgeSaver);
            theContext.SyncInt(ref mTimesFed);
            theContext.SyncInt(ref mFeedingsPerGrow);
            theContext.SyncEnum(ref mPlantNeed, theContext.aPottedPlantNeedSaver);
            theContext.SyncDateTime(ref mLastNeedFulfilledTime);
            theContext.SyncDateTime(ref mLastFertilizedTime);
            theContext.SyncDateTime(ref mLastChocolateTime);
            theContext.SyncInt(ref mFutureAttribute[0]);
        }

        public void InitializePottedPlant(SeedType theSeedType)
        {
            mSeedType = theSeedType;
            mDrawVariation = DrawVariation.Normal;
            mLastWateredTime.AddTicks(0L);
            mFacing = (PottedPlant.FacingDirection)TodCommon.RandRangeInt(0, 1);
            mPlantAge = PottedPlantAge.Sprout;
            mTimesFed = 0;
            mWhichZenGarden = GardenType.Main;
            mFeedingsPerGrow = TodCommon.RandRangeInt(3, 5);
            mPlantNeed = PottedPlantNeed.None;
        }

        public void Save(Sexy.Buffer b)
        {
            b.WriteLong((int)mSeedType);
            b.WriteLong((int)mWhichZenGarden);
            b.WriteLong(mX);
            b.WriteLong(mY);
            b.WriteLong((int)mFacing);
            b.WriteDateTime(mLastWateredTime);
            b.WriteLong((int)mDrawVariation);
            b.WriteLong((int)mPlantAge);
            b.WriteLong(mTimesFed);
            b.WriteLong(mFeedingsPerGrow);
            b.WriteLong((int)mPlantNeed);
            b.WriteDateTime(mLastNeedFulfilledTime);
            b.WriteDateTime(mLastFertilizedTime);
            b.WriteDateTime(mLastChocolateTime);
            b.WriteLongArray(mFutureAttribute);
        }

        public void Load(Sexy.Buffer b)
        {
            mSeedType = (SeedType)b.ReadLong();
            mWhichZenGarden = (GardenType)b.ReadLong();
            mX = b.ReadLong();
            mY = b.ReadLong();
            mFacing = (PottedPlant.FacingDirection)b.ReadLong();
            mLastWateredTime = b.ReadDateTime();
            mDrawVariation = (DrawVariation)b.ReadLong();
            mPlantAge = (PottedPlantAge)b.ReadLong();
            mTimesFed = b.ReadLong();
            mFeedingsPerGrow = b.ReadLong();
            mPlantNeed = (PottedPlantNeed)b.ReadLong();
            mLastNeedFulfilledTime = b.ReadDateTime();
            mLastFertilizedTime = b.ReadDateTime();
            mLastChocolateTime = b.ReadDateTime();
            mFutureAttribute = b.ReadLongArray();
        }

        public SeedType mSeedType;

        public GardenType mWhichZenGarden;

        public int mX;

        public int mY;

        public PottedPlant.FacingDirection mFacing;

        public DateTime mLastWateredTime = DateTime.MinValue;

        public DrawVariation mDrawVariation;

        public PottedPlantAge mPlantAge;

        public int mTimesFed;

        public int mFeedingsPerGrow;

        public PottedPlantNeed mPlantNeed;

        public DateTime mLastNeedFulfilledTime = DateTime.MinValue;

        public DateTime mLastFertilizedTime = DateTime.MinValue;

        public DateTime mLastChocolateTime = DateTime.MinValue;

        public int[] mFutureAttribute = new int[1];

        public enum FacingDirection //Prefix: FACING
        {
            Right,
            Left
        }
    }
}
