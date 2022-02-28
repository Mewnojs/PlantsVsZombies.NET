using System;
using System.Collections.Generic;
using Sexy.GraphicsLib;

namespace JeffLib
{
	public class Component
	{
		public Component()
		{
			this.mValue = 0f;
			this.mOriginalValue = 0f;
			this.mTargetValue = 0f;
			this.mStartFrame = 0;
			this.mEndFrame = 0;
			this.mValueDelta = 0f;
		}

		public Component(float val)
		{
			this.mTargetValue = val;
			this.mOriginalValue = val;
			this.mValue = val;
			this.mStartFrame = 0;
			this.mEndFrame = 0;
		}

		public Component(float val, float target)
		{
			this.mOriginalValue = val;
			this.mValue = val;
			this.mTargetValue = target;
			this.mStartFrame = 0;
			this.mEndFrame = 0;
			this.mValueDelta = ((this.mEndFrame - this.mStartFrame == 0) ? 0f : ((target - val) / (float)(this.mEndFrame - this.mStartFrame)));
		}

		public Component(float val, float target, int start)
		{
			this.mOriginalValue = val;
			this.mValue = val;
			this.mTargetValue = target;
			this.mStartFrame = start;
			this.mEndFrame = 0;
			this.mValueDelta = ((this.mEndFrame - this.mStartFrame == 0) ? 0f : ((target - val) / (float)(this.mEndFrame - this.mStartFrame)));
		}

		public Component(float val, float target, int start, int end)
		{
			this.mOriginalValue = val;
			this.mValue = val;
			this.mTargetValue = target;
			this.mStartFrame = start;
			this.mEndFrame = end;
			this.mValueDelta = ((this.mEndFrame - this.mStartFrame == 0) ? 0f : ((target - val) / (float)(this.mEndFrame - this.mStartFrame)));
		}

		public bool Active(int count)
		{
			return count >= this.mStartFrame && count <= this.mEndFrame;
		}

		public void SyncState(DataSyncBase sync)
		{
		}

		public void Update()
		{
			this.mValue += this.mValueDelta;
			if ((this.mValueDelta > 0f && this.mValue > this.mTargetValue) || (this.mValueDelta < 0f && this.mValue < this.mTargetValue))
			{
				this.mValue = this.mTargetValue;
			}
		}

		public static bool UpdateComponentVec(List<Component> vec, int update_count)
		{
			bool result = true;
			for (int i = 0; i < vec.Count; i++)
			{
				Component component = vec[i];
				if (component.Active(update_count))
				{
					component.Update();
					return false;
				}
				if (update_count < component.mStartFrame)
				{
					result = false;
				}
			}
			return result;
		}

		public static bool UpdateComponentVec(List<KeyValuePair<Component, Image>> vec, int update_count)
		{
			bool result = true;
			for (int i = 0; i < vec.Count; i++)
			{
				Component key = vec[i].Key;
				if (key.Active(update_count))
				{
					key.Update();
					return false;
				}
				if (update_count < key.mStartFrame)
				{
					result = false;
				}
			}
			return result;
		}

		public static float GetComponentValue(List<Component> v, float def_value, int update_count)
		{
			for (int i = 0; i < v.Count; i++)
			{
				Component component = v[i];
				if (update_count < component.mStartFrame)
				{
					return component.mValue;
				}
				if (component.Active(update_count))
				{
					return component.mValue;
				}
				if (i == v.Count - 1)
				{
					return component.mValue;
				}
			}
			return def_value;
		}

		public float mValue;

		public float mOriginalValue;

		public float mTargetValue;

		public int mStartFrame;

		public int mEndFrame;

		public float mValueDelta;
	}
}
