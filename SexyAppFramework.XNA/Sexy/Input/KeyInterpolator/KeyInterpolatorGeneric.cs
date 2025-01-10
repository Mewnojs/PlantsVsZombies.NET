using System;

namespace Sexy
{
    internal class KeyInterpolatorGeneric<T> : KeyInterpolator<T> where T : struct
    {
        public KeyInterpolatorGeneric(KeyInterpolatorGeneric<T>.InterpolatorMethod m)
        {
            interpolationMethod = m;
        }

        protected override T GetLerpResult(float f, T a, T b)
        {
            return interpolationMethod(f, a, b);
        }

        public override void PrepareForReuse()
        {
        }

        public override T Tick(float tick)
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
                base.SetupEaseFunc<T>(mKeys[mKey], mKeys[num]);
            }
            T result;
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

        private KeyInterpolatorGeneric<T>.InterpolatorMethod interpolationMethod;

        public delegate T InterpolatorMethod(float f, T a, T b);
    }
}
