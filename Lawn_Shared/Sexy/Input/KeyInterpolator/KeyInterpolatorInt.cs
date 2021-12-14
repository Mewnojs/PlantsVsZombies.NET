using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class KeyInterpolatorInt : KeyInterpolator<int>
	{
		protected override int GetLerpResult(float f, int a, int b)
		{
			return KeyInterpolatorInt.InterpolationMethodInt(f, a, b);
		}

		public static int InterpolationMethodInt(float f, int a, int b)
		{
			return GlobalMembersInterpolator.tlerp(f, a, b);
		}

		public static KeyInterpolatorInt GetNewKeyInterpolatorInt()
		{
			if (KeyInterpolatorInt.unusedObjects.Count > 0)
			{
				return KeyInterpolatorInt.unusedObjects.Pop();
			}
			return new KeyInterpolatorInt();
		}

		private KeyInterpolatorInt()
		{
		}

		public override void PrepareForReuse()
		{
			Reset();
			KeyInterpolatorInt.unusedObjects.Push(this);
		}

		public override int Tick(float tick)
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
				base.SetupEaseFunc<int>(mKeys[mKey], mKeys[num]);
			}
			int result;
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

		private static Stack<KeyInterpolatorInt> unusedObjects = new Stack<KeyInterpolatorInt>();
	}
}
