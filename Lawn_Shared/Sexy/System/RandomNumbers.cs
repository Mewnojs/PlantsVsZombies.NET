using System;

namespace Sexy
{
    public/*internal*/ static class RandomNumbers
    {
        public/*internal*/ static int NextNumber()
        {
            if (RandomNumbers.r == null)
            {
                RandomNumbers.Seed();
            }
            return RandomNumbers.r.Next();
        }

        public/*internal*/ static int NextNumber(int ceiling)
        {
            if (RandomNumbers.r == null)
            {
                RandomNumbers.Seed();
            }
            return RandomNumbers.r.Next(ceiling);
        }

        public/*internal*/ static float NextNumber(float ceiling)
        {
            if (RandomNumbers.r == null)
            {
                RandomNumbers.Seed();
            }
            return (float)RandomNumbers.r.NextDouble() * ceiling;
        }

        public/*internal*/ static void Seed()
        {
            RandomNumbers.r = new Random();
        }

        public/*internal*/ static void Seed(int seed)
        {
            RandomNumbers.r = new Random(seed);
        }

        private static Random r;
    }
}
