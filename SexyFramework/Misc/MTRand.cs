using System;

namespace Sexy.Misc
{
	public class MTRand
	{
		public MTRand(string theSerialData)
		{
			this.SRand(theSerialData);
			this.mti = MTRand.MTRAND_N + 1;
		}

		public MTRand(uint seed)
		{
			this.SRand(seed);
		}

		public MTRand()
		{
			this.SRand(4357U);
		}

		public void SRand(string theSerialData)
		{
			if (theSerialData.Length == MTRand.MTRAND_N * 4 + 4)
			{
				string text = theSerialData.Substring(0, 4);
				this.mti = int.Parse(text);
				return;
			}
			this.SRand(4357U);
		}

		public void SRand(uint seed)
		{
			if (seed == 0U)
			{
				seed = 4357U;
			}
			this.mt[0] = seed & uint.MaxValue;
			this.mti = 1;
			while (this.mti < MTRand.MTRAND_N)
			{
				this.mt[this.mti] = 1812433253U * (this.mt[this.mti - 1] ^ (this.mt[this.mti - 1] >> 30)) + (uint)this.mti;
				this.mt[this.mti] &= uint.MaxValue;
				this.mti++;
			}
		}

		private uint NextNoAssert()
		{
			uint num;
			if (this.mti >= MTRand.MTRAND_N)
			{
				int i;
				for (i = 0; i < MTRand.MTRAND_N - MTRand.MTRAND_M; i++)
				{
					num = (this.mt[i] & MTRand.UPPER_MASK) | (this.mt[i + 1] & MTRand.LOWER_MASK);
					this.mt[i] = this.mt[i + MTRand.MTRAND_M] ^ (num >> 1) ^ MTRand.mag01[(int)(checked((IntPtr)(unchecked((ulong)num) & 1UL)))];
				}
				while (i < MTRand.MTRAND_N - 1)
				{
					num = (this.mt[i] & MTRand.UPPER_MASK) | (this.mt[i + 1] & MTRand.LOWER_MASK);
					this.mt[i] = this.mt[i + (MTRand.MTRAND_M - MTRand.MTRAND_N)] ^ (num >> 1) ^ MTRand.mag01[(int)(checked((IntPtr)(unchecked((ulong)num) & 1UL)))];
					i++;
				}
				num = (this.mt[MTRand.MTRAND_N - 1] & MTRand.UPPER_MASK) | (this.mt[0] & MTRand.LOWER_MASK);
				this.mt[MTRand.MTRAND_N - 1] = this.mt[MTRand.MTRAND_M - 1] ^ (num >> 1) ^ MTRand.mag01[(int)(checked((IntPtr)(unchecked((ulong)num) & 1UL)))];
				this.mti = 0;
			}
			num = this.mt[this.mti++];
			num ^= num >> 11;
			num ^= (num << 7) & MTRand.TEMPERING_MASK_B;
			num ^= (num << 15) & MTRand.TEMPERING_MASK_C;
			num ^= num >> 18;
			return num & 2147483647U;
		}

		public uint Next()
		{
			return this.NextNoAssert();
		}

		public uint NextNoAssert(uint range)
		{
			return this.NextNoAssert() % range;
		}

		public uint Next(uint range)
		{
			return this.NextNoAssert(range);
		}

		public float NextNoAssert(float range)
		{
			return (float)(this.NextNoAssert() / 2147483647.0 * (double)range);
		}

		public float Next(float range)
		{
			return this.NextNoAssert(range);
		}

		public string Serialize()
		{
			return "";
		}

		public static void SetRandAllowed(bool allowed)
		{
		}

		public static int MTRAND_N = 624;

		public static int MTRAND_M = 397;

		public static uint MATRIX_A = 2567483615U;

		public static uint UPPER_MASK = 2147483648U;

		public static uint LOWER_MASK = 2147483647U;

		public static uint TEMPERING_MASK_B = 2636928640U;

		public static uint TEMPERING_MASK_C = 4022730752U;

		public static uint[] mag01 = new uint[]
		{
			default(uint),
			MTRand.MATRIX_A
		};

		private uint[] mt = new uint[MTRand.MTRAND_N];

		private int mti;
	}
}
