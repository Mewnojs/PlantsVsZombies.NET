using System;
using System.Collections.Generic;
using System.Linq;
using Sexy;

namespace JeffLib
{
	public class QRand
	{
		private void Init()
		{
			QRand.RandomNumbers.Seed(0);
			this.mUpdateCnt = 0;
			this.mLastIndex = -1;
		}

		public QRand()
		{
			this.Init();
		}

		public QRand(float value)
		{
			this.Init();
			List<float> list = new List<float>();
			list.Add(value);
			this.SetWeights(list);
		}

		public QRand(List<float> initial_weights)
		{
			this.Init();
			this.SetWeights(initial_weights);
		}

		public void SetWeights(List<float> v)
		{
			this.mWeights.Clear();
			if (v.Count == 1)
			{
				this.mWeights.Add(1f - v[0]);
				this.mWeights.Add(v[0]);
			}
			else
			{
				float num = 0f;
				for (int i = 0; i < v.Count; i++)
				{
					this.mWeights.Add(v[i]);
					num += this.mWeights[i];
				}
				for (int j = 0; j < this.mWeights.Count; j++)
				{
					List<float> list;
					int num2;
					(list = this.mWeights)[num2 = j] = list[num2] / num;
				}
			}
			for (int k = this.mLastHitUpdate.Count; k < this.mWeights.Count; k++)
			{
				this.mLastHitUpdate.Add(0);
				this.mPrevLastHitUpdate.Add(0);
			}
			this.mCurSway.Clear();
			this.mCurSway.Resize(this.mWeights.Count);
		}

		public int Next()
		{
			this.mUpdateCnt++;
			float num = 0f;
			for (int i = 0; i < Enumerable.Count<float>(this.mWeights); i++)
			{
				float num2 = this.mWeights[i];
				if (num2 != 0f)
				{
					float num3 = 1f / num2;
					float num4 = 1f + ((float)(this.mUpdateCnt - this.mLastHitUpdate[i]) - num3) / num3;
					float num5 = 1f + ((float)(this.mUpdateCnt - this.mPrevLastHitUpdate[i]) - num3 * 2f) / (num3 * 2f);
					float num6 = num2 * Math.Max(Math.Min(num4 * 0.75f + num5 * 0.25f, 3f), 0.333f);
					this.mCurSway[i] = num6;
					num += num6;
				}
				else
				{
					this.mCurSway[i] = 0f;
				}
			}
			float num7 = (float)QRand.RandomNumbers.NextNumber(1, QRand.RAND_MAX) / (float)QRand.RAND_MAX * num;
			QRand.gDebugFirstRand = (int)num7;
			QRand.gSwaySize = this.mCurSway.Count;
			int num8 = 0;
			while (num8 < this.mCurSway.Count && num7 > this.mCurSway[num8])
			{
				num7 -= this.mCurSway[num8];
				num8++;
			}
			if (num8 >= this.mCurSway.Count)
			{
				num8--;
			}
			this.mPrevLastHitUpdate[num8] = this.mLastHitUpdate[num8];
			this.mLastHitUpdate[num8] = this.mUpdateCnt;
			this.mLastIndex = num8;
			return num8;
		}

		public int NumWeights()
		{
			return this.mWeights.Count;
		}

		public int NumNonZeroWeights()
		{
			int num = 0;
			for (int i = 0; i < this.mWeights.Count; i++)
			{
				if (this.mWeights[i] != 0f)
				{
					num++;
				}
			}
			return num;
		}

		public void Clear()
		{
			this.mWeights.Clear();
			this.mCurSway.Clear();
			this.mLastHitUpdate.Clear();
			this.mPrevLastHitUpdate.Clear();
		}

		public bool HasWeight(int idx)
		{
			return idx < this.mWeights.Count && this.mWeights[idx] > 0f;
		}

		public void SyncState(DataSyncBase sync)
		{
			sync.SyncLong(ref this.mUpdateCnt);
			sync.SyncLong(ref this.mLastIndex);
			sync.SyncListFloat(this.mWeights);
			sync.SyncListFloat(this.mCurSway);
			sync.SyncListInt(this.mLastHitUpdate);
			sync.SyncListInt(this.mPrevLastHitUpdate);
		}

		public static int RAND_MAX = 32767;

		public static int gDebugFirstRand = 0;

		public static int gSwaySize = 0;

		protected int mUpdateCnt;

		protected int mLastIndex;

		protected List<float> mWeights = new List<float>();

		protected List<float> mCurSway = new List<float>();

		protected List<int> mLastHitUpdate = new List<int>();

		protected List<int> mPrevLastHitUpdate = new List<int>();

		internal static class RandomNumbers
		{
			internal static int NextNumber()
			{
				if (QRand.RandomNumbers.r == null)
				{
					QRand.RandomNumbers.Seed();
				}
				return QRand.RandomNumbers.r.Next();
			}

			internal static int NextNumber(int ceiling)
			{
				if (QRand.RandomNumbers.r == null)
				{
					QRand.RandomNumbers.Seed();
				}
				return QRand.RandomNumbers.r.Next(ceiling);
			}

			internal static int NextNumber(int min, int max)
			{
				if (QRand.RandomNumbers.r == null)
				{
					QRand.RandomNumbers.Seed();
				}
				return QRand.RandomNumbers.r.Next(min, max);
			}

			internal static void Seed()
			{
				QRand.RandomNumbers.r = new Random();
			}

			internal static void Seed(int seed)
			{
				QRand.RandomNumbers.r = new Random(seed);
			}

			private static Random r;
		}
	}
}
