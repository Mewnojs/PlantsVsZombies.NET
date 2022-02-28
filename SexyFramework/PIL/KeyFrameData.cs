using System;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class KeyFrameData
	{
		public void CopyFrom(KeyFrameData k)
		{
			if (this.mNumInts != k.mNumInts)
			{
				this.mNumInts = k.mNumInts;
				if (this.mNumInts > 0)
				{
					this.mIntData = new int[this.mNumInts];
				}
				else
				{
					this.mIntData = null;
				}
			}
			if (this.mNumFloats != k.mNumFloats)
			{
				this.mNumFloats = k.mNumFloats;
				if (this.mNumFloats > 0)
				{
					this.mFloatData = new float[this.mNumFloats];
				}
				else
				{
					this.mFloatData = null;
				}
			}
			if (this.mNumBools != k.mNumBools)
			{
				this.mNumBools = k.mNumBools;
				if (this.mNumBools > 0)
				{
					this.mBoolData = new bool[this.mNumBools];
				}
				else
				{
					this.mBoolData = null;
				}
			}
			if (this.mNumInts > 0)
			{
				Array.Copy(k.mIntData, this.mIntData, this.mNumInts);
			}
			if (this.mNumFloats > 0)
			{
				Array.Copy(k.mFloatData, this.mFloatData, this.mNumFloats);
			}
			if (this.mNumBools > 0)
			{
				Array.Copy(k.mBoolData, this.mBoolData, this.mNumBools);
			}
		}

		public virtual void Init()
		{
		}

		public virtual KeyFrameData Clone()
		{
			return new KeyFrameData(this);
		}

		public KeyFrameData()
		{
		}

		public KeyFrameData(KeyFrameData k)
		{
			this.CopyFrom(k);
		}

		public virtual void Dispose()
		{
			this.mIntData = null;
			this.mFloatData = null;
			this.mBoolData = null;
		}

		public virtual void Serialize(SexyBuffer b)
		{
			b.WriteLong((long)this.mNumInts);
			b.WriteLong((long)this.mNumFloats);
			b.WriteLong((long)this.mNumBools);
			for (int i = 0; i < this.mNumInts; i++)
			{
				b.WriteLong((long)this.mIntData[i]);
			}
			for (int j = 0; j < this.mNumFloats; j++)
			{
				b.WriteFloat(this.mFloatData[j]);
			}
			for (int k = 0; k < this.mNumBools; k++)
			{
				b.WriteBoolean(this.mBoolData[k]);
			}
		}

		public virtual void Deserialize(SexyBuffer b)
		{
			this.mNumInts = (int)b.ReadLong();
			this.mNumFloats = (int)b.ReadLong();
			this.mNumBools = (int)b.ReadLong();
			for (int i = 0; i < this.mNumInts; i++)
			{
				this.mIntData[i] = (int)b.ReadLong();
			}
			for (int j = 0; j < this.mNumFloats; j++)
			{
				this.mFloatData[j] = b.ReadFloat();
			}
			for (int k = 0; k < this.mNumBools; k++)
			{
				this.mBoolData[k] = b.ReadBoolean();
			}
		}

		public static KeyFrameData Instantiate()
		{
			return null;
		}

		public int[] mIntData;

		public float[] mFloatData;

		public bool[] mBoolData;

		public int mNumInts;

		public int mNumFloats;

		public int mNumBools;
	}
}
