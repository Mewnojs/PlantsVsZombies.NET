using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class KeyInterpolatorTPoint : KeyInterpolator<TPoint>
	{
		protected override TPoint GetLerpResult(float f, TPoint a, TPoint b)
		{
			return KeyInterpolatorTPoint.InterpolationMethodTPoint(f, a, b);
		}

		public static TPoint InterpolationMethodTPoint(float f, TPoint a, TPoint b)
		{
			return GlobalMembersInterpolator.tlerp(f, a, b);
		}

		public static KeyInterpolatorTPoint GetNewKeyInterpolatorTPoint()
		{
			if (KeyInterpolatorTPoint.unusedObjects.Count > 0)
			{
				return KeyInterpolatorTPoint.unusedObjects.Pop();
			}
			return new KeyInterpolatorTPoint();
		}

		private KeyInterpolatorTPoint()
		{
		}

		public override void PrepareForReuse()
		{
			Reset();
			KeyInterpolatorTPoint.unusedObjects.Push(this);
		}

		public override TPoint Tick(float tick)
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
				base.SetupEaseFunc<TPoint>(mKeys[mKey], mKeys[num]);
			}
			TPoint result;
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

		private static Stack<KeyInterpolatorTPoint> unusedObjects = new Stack<KeyInterpolatorTPoint>();
	}
}
