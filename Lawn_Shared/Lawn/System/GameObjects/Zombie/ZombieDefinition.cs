using System;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class ZombieDefinition
	{
		public ZombieDefinition(ZombieType aZombieType, ReanimationType aReanimationType, int aZombieValue, int aStartingLevel, int aFirstAllowedWave, int aPickWeight, string aZombieName)
		{
			mZombieType = aZombieType;
			mReanimationType = aReanimationType;
			mZombieValue = aZombieValue;
			mStartingLevel = aStartingLevel;
			mFirstAllowedWave = aFirstAllowedWave;
			mPickWeight = aPickWeight;
			mZombieName = aZombieName;
		}

		public ZombieType mZombieType;

		public ReanimationType mReanimationType;

		public int mZombieValue;

		public int mStartingLevel;

		public int mFirstAllowedWave;

		public int mPickWeight;

		public string mZombieName;
	}
}
