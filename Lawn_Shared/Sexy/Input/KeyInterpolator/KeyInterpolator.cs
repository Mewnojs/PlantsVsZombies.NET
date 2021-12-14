using System;

namespace Sexy
{
	internal abstract class KeyInterpolator<T> : Interpolator where T : struct
	{
		public abstract void PrepareForReuse();

		protected virtual void Reset()
		{
			mKeys.Clear();
			mKey = 0;
		}

		public KeyInterpolator<T> CopyFrom(KeyInterpolator<T> rhs)
		{
			mKeys.Clear();
			foreach (TypedKey<T> item in rhs.mKeys)
			{
				mKeys.Add(item);
			}
			mKey = mKeys.GetLastKey();
			return this;
		}

		public abstract T Tick(float tick);

		protected abstract T GetLerpResult(float f, T a, T b);

		public void Clear()
		{
			mKeys.Clear();
			mKey = 0;
		}

		public bool Empty()
		{
			return mKeys.Count == 0;
		}

		public void SetKey(int tick, T value, bool ease, bool tween)
		{
			TypedKey<T> item = default(TypedKey<T>);
			item.tick = tick;
			item.value = value;
			item.ease = ease;
			item.tween = tween;
			item.KeyIdentifier = tick;
			if (mKeys.Contains(item.KeyIdentifier))
			{
				mKeys.Remove(item.KeyIdentifier);
			}
			mKeys.Add(item);
			mKey = tick;
		}

		public int FirstTick()
		{
			if (mKeys.empty())
			{
				return 0;
			}
			return mKeys.GetFirstKey();
		}

		public int LastTick()
		{
			if (mKeys.empty())
			{
				return 0;
			}
			return mKeys.GetLastKey();
		}

		protected MyTypedKeyCollection<T> mKeys = new MyTypedKeyCollection<T>();

		protected int mKey;
	}
}
