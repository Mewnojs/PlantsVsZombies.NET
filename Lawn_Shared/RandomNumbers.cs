using System;

internal static class RandomNumbers
{
	internal static int NextNumber()
	{
		if (RandomNumbers.r == null)
		{
			RandomNumbers.Seed();
		}
		return RandomNumbers.r.Next();
	}

	internal static int NextNumber(int ceiling)
	{
		if (RandomNumbers.r == null)
		{
			RandomNumbers.Seed();
		}
		return RandomNumbers.r.Next(ceiling);
	}

	internal static float NextNumber(float ceiling)
	{
		if (RandomNumbers.r == null)
		{
			RandomNumbers.Seed();
		}
		return (float)RandomNumbers.r.NextDouble() * ceiling;
	}

	internal static void Seed()
	{
		RandomNumbers.r = new Random();
	}

	internal static void Seed(int seed)
	{
		RandomNumbers.r = new Random(seed);
	}

	private static Random r;
}
