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
			Reset();
			KeyInterpolatorFloat.unusedObjects.Push(this);
		}

		public override float Tick(float tick)
		{
			bool flag = !mEaseFuncSet;
			int num = mKeys.GetKeyAfter(mKey);
			bool flag2 = false;
			while (!flag2 && tick >= num)
			{
				mKey = num;
				int keyAfter = mKeys.GetKeyAfter(num);
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
			int firstKey = mKeys.GetFirstKey();
			while (mKey != firstKey && tick < mKey)
			{
				num = mKey;
				mKey = mKeys.GetKeyBefore(mKey);
				flag = true;
			}
			if (num == mKeys.GetLastKey() + 1)
			{
				return mKeys[mKey].value;
			}
			if (tick < mKey)
			{
				return mKeys[mKey].value;
			}
			if (flag)
			{
				base.SetupEaseFunc<float>(mKeys[mKey], mKeys[num]);
			}
			float result;
			if (mKeys[num].tween)
			{
				float num2 = num - (float)mKey;
				float num3 = tick - mKey;
				float t = num3 / num2;
				float f = mEaseFunc.Tick(t);
				result = GetLerpResult(f, mKeys[mKey].value, mKeys[num].value);
			}
			else
			{
				result = mKeys[mKey].value;
			}
			return result;
		}

		private static Stack<KeyInterpolatorFloat> unusedObjects = new Stack<KeyInterpolatorFloat>();
	}
}
