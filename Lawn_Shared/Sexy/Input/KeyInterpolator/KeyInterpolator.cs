using System;

namespace Sexy
{
	internal abstract class KeyInterpolator<T> : Interpolator where T : struct
	{
		public abstract void PrepareForReuse();

		protected virtual void Reset()
		{
			this.mKeys.Clear();
			this.mKey = 0;
		}

		public KeyInterpolator<T> CopyFrom(KeyInterpolator<T> rhs)
		{
			this.mKeys.Clear();
			foreach (TypedKey<T> item in rhs.mKeys)
			{
				this.mKeys.Add(item);
			}
			this.mKey = this.mKeys.GetLastKey();
			return this;
		}

		public abstract T Tick(float tick);

		protected abstract T GetLerpResult(float f, T a, T b);

		public void Clear()
		{
			this.mKeys.Clear();
			this.mKey = 0;
		}

		public bool Empty()
		{
			return this.mKeys.Count == 0;
		}

		public void SetKey(int tick, T value, bool ease, bool tween)
		{
			TypedKey<T> item = default(TypedKey<T>);
			item.tick = tick;
			item.value = value;
			item.ease = ease;
			item.tween = tween;
			item.KeyIdentifier = tick;
			if (this.mKeys.Contains(item.KeyIdentifier))
			{
				this.mKeys.Remove(item.KeyIdentifier);
			}
			this.mKeys.Add(item);
			this.mKey = tick;
		}

		public int FirstTick()
		{
			if (this.mKeys.empty())
			{
				return 0;
			}
			return this.mKeys.GetFirstKey();
		}

		public int LastTick()
		{
			if (this.mKeys.empty())
			{
				return 0;
			}
			return this.mKeys.GetLastKey();
		}

		protected MyTypedKeyCollection<T> mKeys = new MyTypedKeyCollection<T>();

		protected int mKey;
	}
}
