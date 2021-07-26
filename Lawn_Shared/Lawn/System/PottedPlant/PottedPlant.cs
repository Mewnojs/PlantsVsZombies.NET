using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public class PottedPlant
	{
		public void InitializePottedPlant(SeedType theSeedType)
		{
			this.mSeedType = theSeedType;
			this.mDrawVariation = DrawVariation.VARIATION_NORMAL;
			this.mLastWateredTime.AddTicks(0L);
			this.mFacing = (PottedPlant.FacingDirection)TodCommon.RandRangeInt(0, 1);
			this.mPlantAge = PottedPlantAge.PLANTAGE_SPROUT;
			this.mTimesFed = 0;
			this.mWhichZenGarden = GardenType.GARDEN_MAIN;
			this.mFeedingsPerGrow = TodCommon.RandRangeInt(3, 5);
			this.mPlantNeed = PottedPlantNeed.PLANTNEED_NONE;
		}

		public void Save(Buffer b)
		{
			b.WriteLong((int)this.mSeedType);
			b.WriteLong((int)this.mWhichZenGarden);
			b.WriteLong(this.mX);
			b.WriteLong(this.mY);
			b.WriteLong((int)this.mFacing);
			b.WriteDateTime(this.mLastWateredTime);
			b.WriteLong((int)this.mDrawVariation);
			b.WriteLong((int)this.mPlantAge);
			b.WriteLong(this.mTimesFed);
			b.WriteLong(this.mFeedingsPerGrow);
			b.WriteLong((int)this.mPlantNeed);
			b.WriteDateTime(this.mLastNeedFulfilledTime);
			b.WriteDateTime(this.mLastFertilizedTime);
			b.WriteDateTime(this.mLastChocolateTime);
			b.WriteLongArray(this.mFutureAttribute);
		}

		public void Load(Buffer b)
		{
			this.mSeedType = (SeedType)b.ReadLong();
			this.mWhichZenGarden = (GardenType)b.ReadLong();
			this.mX = b.ReadLong();
			this.mY = b.ReadLong();
			this.mFacing = (PottedPlant.FacingDirection)b.ReadLong();
			this.mLastWateredTime = b.ReadDateTime();
			this.mDrawVariation = (DrawVariation)b.ReadLong();
			this.mPlantAge = (PottedPlantAge)b.ReadLong();
			this.mTimesFed = b.ReadLong();
			this.mFeedingsPerGrow = b.ReadLong();
			this.mPlantNeed = (PottedPlantNeed)b.ReadLong();
			this.mLastNeedFulfilledTime = b.ReadDateTime();
			this.mLastFertilizedTime = b.ReadDateTime();
			this.mLastChocolateTime = b.ReadDateTime();
			this.mFutureAttribute = b.ReadLongArray();
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

		public enum FacingDirection
		{
			FACING_RIGHT,
			FACING_LEFT
		}
	}
}
