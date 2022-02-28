using System;
using System.Collections.Generic;

namespace Sexy.AELib
{
	public class Timeline
	{
		public Timeline()
		{
		}

		public Timeline(Timeline rhs)
		{
			this.CopyFrom(rhs);
		}

		public void CopyFrom(Timeline rhs)
		{
			this.mPingForward = rhs.mPingForward;
			this.mLastPingPongFrame = rhs.mLastPingPongFrame;
			this.mLoopType = rhs.mLoopType;
			this.mLoopFrame = rhs.mLoopFrame;
			this.mKeyframes.Clear();
			for (int i = 0; i < rhs.mKeyframes.Count; i++)
			{
				this.mKeyframes.Add(new Keyframe(rhs.mKeyframes[i]));
			}
		}

		public void Reset()
		{
			this.mPingForward = false;
			this.mLastPingPongFrame = -1;
		}

		public bool HasInitialValue()
		{
			return this.mKeyframes.Count > 0 && this.mKeyframes[0].mFrame == 0;
		}

		public void AddKeyframe(int frame, float value1)
		{
			this.AddKeyframe(frame, value1, 0f);
		}

		public void AddKeyframe(int frame, float value1, float value2)
		{
			for (int i = 0; i < this.mKeyframes.Count; i++)
			{
				if (this.mKeyframes[i].mFrame == frame)
				{
					this.mKeyframes[i].mValue1 = value1;
					this.mKeyframes[i].mValue2 = value2;
					return;
				}
			}
			this.mKeyframes.Add(new Keyframe(frame, value1, value2));
			this.mKeyframes.Sort(new Keyframe.KeyFrameSort());
		}

		public void GetValue(int frame, ref float value1)
		{
			float num = 0f;
			this.GetValue(frame, ref value1, ref num);
		}

		public void GetValue(int frame, ref float value1, ref float value2)
		{
			if (this.mKeyframes.Count == 0)
			{
				return;
			}
			if (this.mKeyframes.Count == 1)
			{
				value1 = this.mKeyframes[0].mValue1;
				value2 = this.mKeyframes[0].mValue2;
				return;
			}
			int mFrame = this.mKeyframes[this.mKeyframes.Count - 1].mFrame;
			if (this.mLoopType == 10 && frame > mFrame)
			{
				frame = this.mLoopFrame + frame % (mFrame - this.mLoopFrame);
			}
			else if (this.mLoopType == 11 && frame > mFrame)
			{
				frame = this.mLoopFrame + frame % (mFrame - this.mLoopFrame);
				if (frame < this.mLastPingPongFrame)
				{
					this.mPingForward = !this.mPingForward;
				}
				this.mLastPingPongFrame = frame;
				if (!this.mPingForward)
				{
					frame = mFrame - frame + this.mLoopFrame;
				}
				else if (frame >= mFrame || frame < this.mLastPingPongFrame)
				{
					this.mPingForward = !this.mPingForward;
				}
			}
			for (int i = 1; i < this.mKeyframes.Count; i++)
			{
				Keyframe keyframe = this.mKeyframes[i];
				if (keyframe.mFrame > frame)
				{
					Keyframe keyframe2 = this.mKeyframes[i - 1];
					float num = (float)(frame - keyframe2.mFrame) / (float)(keyframe.mFrame - keyframe2.mFrame);
					value1 = (1f - num) * keyframe2.mValue1 + num * keyframe.mValue1;
					value2 = (1f - num) * keyframe2.mValue2 + num * keyframe.mValue2;
					return;
				}
			}
			value1 = this.mKeyframes[this.mKeyframes.Count - 1].mValue1;
			value2 = this.mKeyframes[this.mKeyframes.Count - 1].mValue2;
		}

		protected List<Keyframe> mKeyframes = new List<Keyframe>();

		protected bool mPingForward;

		protected int mLastPingPongFrame = -1;

		public int mLoopType = -1;

		public int mLoopFrame = -1;
	}
}
