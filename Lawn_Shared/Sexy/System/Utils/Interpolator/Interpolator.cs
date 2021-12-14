using System;

namespace Sexy
{
	internal abstract class Interpolator
	{
		protected Interpolator()
		{
			mEaseFuncSet = false;
		}

		protected void SetupEaseFunc<T>(TypedKey<T> from, TypedKey<T> to) where T : struct
		{
			float v = from.ease ? 0f : 1f;
			float v2 = to.ease ? 0f : 1f;
			mEaseFunc.setup(v, v2, 0.5f, 0.5f);
			mEaseFuncSet = true;
		}

		protected EaseFunction mEaseFunc = new EaseFunction();

		protected bool mEaseFuncSet;
	}
}
