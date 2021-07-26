using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class KeyInterpolatorFloat : KeyInterpolator<float>
	{
		protected override float GetLerpResult(float f, float a, float b)
		{
			return KeyInterpolatorFloat.InterpolationMethodFloat(f, a, b);
		}

		public static float InterpolationMethodFloat(float f, float a, float b)
		{
			return GlobalMembersInterpolator.tlerp(f, a, b);
		}

		public static KeyInterpolatorFloat GetNewKeyInterpolatorFloat()
		{
			if (KeyInterpolatorFloat.unusedObjects.Count > 0)
			{
				return KeyInterpolatorFloat.unusedObjects.Pop();
			}
			return new KeyInterpolatorFloat();
		}

		private KeyInterpolatorFloat()
		{
		}

		public override void PrepareForReuse()
		{
			this.Reset();
			KeyInterpolatorFloat.unusedObjects.Push(this);
		}

		public override float Tick(float tick)
		{
			bool flag = !this.mEaseFuncSet;
			int num = this.mKeys.GetKeyAfter(this.mKey);
			bool flag2 = false;
			while (!flag2 && tick >= (float)num)
			{
				this.mKey = num;
				int keyAfter = this.mKeys.GetKeyAfter(num);
				if (num == keyAfter)
				{
					flag2 = true;
					num++;
				}
				else
				{
					num = keyAfter;
				}
				flag = true;
			}
			int firstKey = this.mKeys.GetFirstKey();
			while (this.mKey != firstKey && tick < (float)this.mKey)
			{
				num = this.mKey;
				this.mKey = this.mKeys.GetKeyBefore(this.mKey);
				flag = true;
			}
			if (num == this.mKeys.GetLastKey() + 1)
			{
				return this.mKeys[this.mKey].value;
			}
			if (tick < (float)this.mKey)
			{
				return this.mKeys[this.mKey].value;
			}
			if (flag)
			{
				base.SetupEaseFunc<float>(this.mKeys[this.mKey], this.mKeys[num]);
			}
			float result;
			if (this.mKeys[num].tween)
			{
				float num2 = (float)num - (float)this.mKey;
				float num3 = tick - (float)this.mKey;
				float t = num3 / num2;
				float f = this.mEaseFunc.Tick(t);
				result = this.GetLerpResult(f, this.mKeys[this.mKey].value, this.mKeys[num].value);
			}
			else
			{
				result = this.mKeys[this.mKey].value;
			}
			return result;
		}

		private static Stack<KeyInterpolatorFloat> unusedObjects = new Stack<KeyInterpolatorFloat>();
	}
}
