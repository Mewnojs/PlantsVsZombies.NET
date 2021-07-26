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
			this.Reset();
			KeyInterpolatorTPoint.unusedObjects.Push(this);
		}

		public override TPoint Tick(float tick)
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
				base.SetupEaseFunc<TPoint>(this.mKeys[this.mKey], this.mKeys[num]);
			}
			TPoint result;
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

		private static Stack<KeyInterpolatorTPoint> unusedObjects = new Stack<KeyInterpolatorTPoint>();
	}
}
