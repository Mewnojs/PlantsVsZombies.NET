using System;
using System.Collections.Generic;
using Sexy.Misc;

namespace Sexy.PIL
{
	public class TimeLine
	{
		public TimeLine()
		{
		}

		public TimeLine(TimeLine rhs)
		{
			if (rhs == null)
			{
				return;
			}
			this.mCurrentSettings = rhs.mCurrentSettings.Clone();
			this.mLoop = rhs.mLoop;
			this.mPrev = null;
			this.mNext = null;
			for (int i = 0; i < rhs.mKeyFrames.size<KeyFrame>(); i++)
			{
				KeyFrameData k = rhs.mKeyFrames[i].second.Clone();
				this.AddKeyFrame(rhs.mKeyFrames[i].first, k);
			}
		}

		public virtual void Dispose()
		{
			for (int i = 0; i < this.mKeyFrames.size<KeyFrame>(); i++)
			{
				this.mKeyFrames[i] = null;
			}
			this.mKeyFrames.Clear();
			this.mCurrentSettings = null;
		}

		public void Update(int frame)
		{
			this.mPrev = (this.mNext = null);
			if (this.mKeyFrames.size<KeyFrame>() == 0)
			{
				return;
			}
			frame = ((this.mLoop && this.mKeyFrames.size<KeyFrame>() > 1) ? (frame % this.mKeyFrames[this.mKeyFrames.size<KeyFrame>() - 1].first) : frame);
			for (int i = 0; i < this.mKeyFrames.size<KeyFrame>(); i++)
			{
				if (this.mKeyFrames[i].first > frame)
				{
					this.mNext = this.mKeyFrames[i];
					break;
				}
				this.mPrev = this.mKeyFrames[i];
			}
			this.mCurrentSettings.CopyFrom(this.mPrev.second);
			if (this.mNext != null)
			{
				int num = this.mNext.first - this.mPrev.first;
				float num2 = (float)(frame - this.mPrev.first) / (float)num;
				for (int j = 0; j < this.mCurrentSettings.mNumInts; j++)
				{
					this.mCurrentSettings.mIntData[j] += (int)((float)(this.mNext.second.mIntData[j] - this.mPrev.second.mIntData[j]) * num2);
				}
				for (int k = 0; k < this.mCurrentSettings.mNumFloats; k++)
				{
					this.mCurrentSettings.mFloatData[k] += (this.mNext.second.mFloatData[k] - this.mPrev.second.mFloatData[k]) * num2;
				}
			}
		}

		public void AddKeyFrame(int frame, KeyFrameData k)
		{
			this.mKeyFrames.Add(new KeyFrame(frame, k));
			this.mKeyFrames.Sort(new KeyFrameSort());
		}

		public virtual void Serialize(SexyBuffer b)
		{
			b.WriteBoolean(this.mLoop);
			this.mCurrentSettings.Serialize(b);
			b.WriteLong((long)this.mKeyFrames.Count);
			for (int i = 0; i < this.mKeyFrames.Count; i++)
			{
				b.WriteLong((long)this.mKeyFrames[i].first);
				this.mKeyFrames[i].second.Serialize(b);
			}
		}

		public virtual void Deserialize(SexyBuffer b, GlobalMembers.KFDInstantiateFunc f)
		{
			this.mKeyFrames.Clear();
			this.mLoop = b.ReadBoolean();
			this.mCurrentSettings.Deserialize(b);
			int num = (int)b.ReadLong();
			for (int i = 0; i < num; i++)
			{
				b.ReadLong();
				KeyFrameData keyFrameData = f();
				keyFrameData.Deserialize(b);
			}
		}

		public List<KeyFrame> mKeyFrames = new List<KeyFrame>();

		public KeyFrame mPrev;

		public KeyFrame mNext;

		public KeyFrameData mCurrentSettings;

		public bool mLoop;
	}
}
