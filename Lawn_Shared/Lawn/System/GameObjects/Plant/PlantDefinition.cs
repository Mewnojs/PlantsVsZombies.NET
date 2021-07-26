using System;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	internal class PlantDefinition
	{
		public PlantDefinition(SeedType aSeedType, Image[] aPlantImage, ReanimationType aReanimationType, int aPacketIndex, int aSeedCost, int aRefreshTime, PlantSubClass aSubClass, int aLaunchRate, string aPlantName)
		{
			this.mSeedType = aSeedType;
			this.mPlantImage = aPlantImage;
			this.mReanimationType = aReanimationType;
			this.mPacketIndex = aPacketIndex;
			this.mSeedCost = aSeedCost;
			this.mRefreshTime = aRefreshTime;
			this.mSubClass = aSubClass;
			this.mLaunchRate = aLaunchRate;
			this.mPlantName = aPlantName;
		}

		public SeedType mSeedType;

		public Image[] mPlantImage;

		public ReanimationType mReanimationType;

		public int mPacketIndex;

		public int mSeedCost;

		public int mRefreshTime;

		public PlantSubClass mSubClass;

		public int mLaunchRate;

		public string mPlantName;
	}
}
