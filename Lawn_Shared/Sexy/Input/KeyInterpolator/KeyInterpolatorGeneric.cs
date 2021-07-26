using System;

namespace Sexy
{
	internal class KeyInterpolatorGeneric<T> : KeyInterpolator<T> where T : struct
	{
		public KeyInterpolatorGeneric(KeyInterpolatorGeneric<T>.InterpolatorMethod m)
		{
			this.interpolationMethod = m;
		}

		protected override T GetLerpResult(float f, T a, T b)
		{
			return this.interpolationMethod(f, a, b);
		}

		public override void PrepareForReuse()
		{
		}

		public override T Tick(float tick)
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
				base.SetupEaseFunc<T>(this.mKeys[this.mKey], this.mKeys[num]);
			}
			T result;
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

		private KeyInterpolatorGeneric<T>.InterpolatorMethod interpolationMethod;

		public delegate T InterpolatorMethod(float f, T a, T b);
	}
}
